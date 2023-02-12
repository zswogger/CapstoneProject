using EZMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney.Models;
using System.Data;
using System.Text;
using MySqlX.XDevAPI.Relational;
using System.Security.Cryptography;
using Org.BouncyCastle.Asn1.X509;
using System.Web.Services;
using Microsoft.Ajax.Utilities;
using System.Threading;

namespace EZMoney
{
    public partial class Dashboard : System.Web.UI.Page
    {

        General gen = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }
            
            welcome.InnerText = "Good " + gen.timeOfDay() + ", ";
            usersName.InnerText = Global.sessionUser.firstName + " " + Global.sessionUser.lastName;

            getWalletBalance();
            loadUserTransactions();
            if (!Page.IsPostBack)
            {
                getPendingTransactions();
            }
        }

        /// <summary>
        /// Loads the transactions for the current user and sets the table
        /// </summary>
        public void loadUserTransactions()
        {
            List<Transaction> SortedList = DB.getUserTransactions(Global.sessionUser.id).OrderByDescending(o => o.id).ToList();

            TransactionsTable.Controls.Add(setHeaders());

            for (int i = 0; i < SortedList.Count; i++)
            {
                string toUser = "";
                string txType = "";

                if (SortedList[i].toUserId != Global.sessionUser.id)
                {
                    toUser = DB.getUserById(SortedList[i].toUserId).username;
                    txType = "Send";
                }
                else
                {
                    toUser = DB.getUserById(SortedList[i].fromUserId).username;
                    txType = "Receive";
                }

                TableRow tr = new TableRow();
                TableCell tc0 = new TableCell();
                TableCell tc1 = new TableCell();
                TableCell tc2 = new TableCell();
                TableCell tc3 = new TableCell();
                TableCell tc4 = new TableCell();
                TableCell tc5 = new TableCell();
                TableCell tc6 = new TableCell();

                tc0.Controls.Add(new LiteralControl("<span>" + SortedList[i].id.ToString() + "</span>"));
                tc1.Controls.Add(new LiteralControl("<span>" + toUser == "" ? "N/A" : toUser + "</span>"));
                tc4.Controls.Add(new LiteralControl("<span>" + txType + "</span>"));
                tc2.Controls.Add(new LiteralControl("<span>" + String.Format("{0:C2}", SortedList[i].amount) + "</span>"));
                tc3.Controls.Add(new LiteralControl("<span>" + SortedList[i].transactionDate.ToString() + "</span>"));
                tc5.Controls.Add(new LiteralControl("<span>" + SortedList[i].memo.ToString() + "</span>"));
                switch (SortedList[i].complete)
                {
                    case 0:
                        tc6.Controls.Add(new LiteralControl("<span>Pending</span>"));
                        break;
                    case 1:
                        tc6.Controls.Add(new LiteralControl("<span>Complete</span>"));
                        break;
                    case 2:
                        tc6.Controls.Add(new LiteralControl("<span>Denied</span>"));
                        break;
                }

                tr.Controls.Add(tc0);
                tr.Controls.Add(tc4);
                tr.Controls.Add(tc1);
                tr.Controls.Add(tc2);
                tr.Controls.Add(tc3);
                tr.Controls.Add(tc5);
                tr.Controls.Add(tc6);

                TransactionsTable.Controls.Add(tr);
            }
        }

        /// <summary>
        /// Set headers for user transaction table
        /// </summary>
        /// <returns></returns>
        public TableRow setHeaders()
        {
            TableRow headers = new TableRow();
            TableHeaderCell h0 = new TableHeaderCell();
            TableHeaderCell h1 = new TableHeaderCell();
            TableHeaderCell h2 = new TableHeaderCell();
            TableHeaderCell h3 = new TableHeaderCell();
            TableHeaderCell h4 = new TableHeaderCell();
            TableHeaderCell h5 = new TableHeaderCell();
            TableHeaderCell h6 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>Transaction ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>To / From</span>"));
            h2.Controls.Add(new LiteralControl("<span>Amount</span>"));
            h3.Controls.Add(new LiteralControl("<span>Transaction Date</span>"));
            h4.Controls.Add(new LiteralControl("<span>Type</span>"));
            h5.Controls.Add(new LiteralControl("<span>Memo</span>"));
            h6.Controls.Add(new LiteralControl("<span>Status</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h4);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h3);
            headers.Controls.Add(h5);
            headers.Controls.Add(h6);
            return headers;
        }

        /// <summary>
        /// Get the wallet balance for the current user
        /// </summary>
        public void getWalletBalance()
        {
            Global.sessionUser.wallet = DB.getUserWallet(Global.sessionUser.id);
            walletBalance.InnerText = "Current Balance: " + String.Format("{0:C2}", Global.sessionUser.wallet.currentAmount);
        }

        /// <summary>
        /// Get requested transactions for the current user
        /// </summary>
        public void getPendingTransactions()
        {
            List<Transaction> pendingTransactions = Transaction.getPendingTransactions(Global.sessionUser.id);
            PendingTransactionsBadge.Value = pendingTransactions.Count.ToString();

            foreach (Transaction tx in pendingTransactions)
            {
                User requestingUser = EZMoney.Models.User.getUserById(tx.toUserId);
                string htmlString = string.Format("<div class='txData'><h3 style='color: var(--accent);'>{0}</h3> is requesting <h3 style='color: var(--accent);'>${1}</h3>", EZMoney.Models.User.getUserById(tx.toUserId).username, tx.amount.ToString());
                if (tx.memo!= null)
                {
                    htmlString += string.Format("for <h5 style='color: var(--accent);'>{0}</h3>", tx.memo);
                }
                htmlString += string.Format("<button type='button' class='btn btn-primary confirmTransaction' data-id='{0}'>Send</button>", tx.id);
                htmlString += string.Format("<button type='button' class='btn btn-danger denyTransaction' data-id='{0}' style='margin-left: 5px'>Deny</button>", tx.id);
                htmlString += "</div> <hr style='border-color: var(--accent);' />";
                pendingTxData.Controls.Add(new LiteralControl(htmlString));
            }
        }

        /// <summary>
        /// Complete a requested transaction for the current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string completeTransaction(int id)
        {
            General gen = new General();
            if (Global.sessionUser == null)
            {
                return "";
            }
            Transaction tx = Transaction.getTransationById(id);

            // Get profit amount
            Profit profit = new Profit();
            if (Company.getCompanyByUserId(tx.toUserId) != null)
            {
                profit.profitAmount = profit.calculateProfit(tx);
                profit.profitDate = DateTime.Now.ToString();
                profit.refunded = false;
            }
            else
            {
                profit.profitAmount = 0;
            }
            tx.profit = profit;

            if (Global.sessionUser.wallet.currentAmount < tx.amount)
            {
                return "You do not have enough funds to cover the transaction. Please deposit more funds and try again.";
            }

            tx.transactionDate = DateTime.Now.ToString();

            User toUser = EZMoney.Models.User.getUserById(tx.toUserId);
            toUser.wallet = Wallet.getWalletByUserId(toUser.id);

            Global.sessionUser.wallet.currentAmount -= tx.amount;
            if (profit.profitAmount > 0)
            {
                toUser.wallet.currentAmount -= profit.profitAmount;
            }
            toUser.wallet.currentAmount += tx.amount;

            tx.complete = 1;

            if (profit.profitAmount > 0)
            {
                if (!profit.save())
                {
                    return "fail";
                }
            }

            if (!tx.completeTransaction())
            {
                return "fail";
            }

            if (!Global.sessionUser.wallet.saveCurrentBalance(Global.sessionUser.id, Global.sessionUser.wallet.currentAmount))
            {
                return "fail";
            }

            if (!toUser.wallet.saveCurrentBalance(toUser.id, toUser.wallet.currentAmount))
            {
                return "fail";
            }
            Thread.Sleep(2);

            return "success";
        }

        /// <summary>
        /// Deny a requested transaction for the current user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public static string denyTransaction(int id)
        {
            General gen = new General();
            if (Global.sessionUser == null)
            {
                return "";
            }

            return Transaction.denyTransaction(id) ? "Success" : "Failed";
        }

        protected void ConfirmFunds(Object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Page.Response.Redirect("/Login", true);
                return;
            }
            Decimal.TryParse(Amount.Text, out Decimal amount);
            if (depositSelector.Value == "true")
            {
                if (amount > 0)
                {
                    Global.sessionUser.wallet.currentAmount += amount;
                    Global.sessionUser.wallet.saveCurrentBalance(Global.sessionUser.id, Global.sessionUser.wallet.currentAmount);
                    
                }
                gen.generateToast("Successfully deposited funds!", ClientScript);
            }
            else
            {
                if (amount > 0)
                {
                    Global.sessionUser.wallet.currentAmount -= amount;
                    Global.sessionUser.wallet.saveCurrentBalance(Global.sessionUser.id, Global.sessionUser.wallet.currentAmount);
                    gen.generateToast("Successfully withdrew funds!", ClientScript);
                }
            }

            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }

}