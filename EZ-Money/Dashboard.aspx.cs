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
            loadUserTransactions();
            getWalletBalance();
        }

        public void loadUserTransactions()
        {
            List<Transaction> SortedList = DB.getUserTransactions(Global.sessionUser.id).OrderByDescending(o => o.id).ToList();

            TransactionsTable.Controls.Add(setHeaders());

            for (int i = 0; i < SortedList.Count; i++)
            {
                string toUser = "";
                bool sent = SortedList[i].toUserId == Global.sessionUser.id;
                string txType = sent == true ? "Receive" : "Send";

                if (sent == true)
                {
                    toUser = DB.getUserById(SortedList[i].fromUserId).username;
                }

                TableRow tr = new TableRow();
                TableCell tc0 = new TableCell();
                TableCell tc1 = new TableCell();
                TableCell tc2 = new TableCell();
                TableCell tc3 = new TableCell();
                TableCell tc4 = new TableCell();
                TableCell tc5 = new TableCell();

                tc0.Controls.Add(new LiteralControl("<span>" + SortedList[i].id.ToString() + "</span>"));
                tc1.Controls.Add(new LiteralControl("<span>" + (sent == true ? toUser : Global.sessionUser.username) + "</span>"));
                tc4.Controls.Add(new LiteralControl("<span>" + txType + "</span>"));
                tc2.Controls.Add(new LiteralControl("<span>" + String.Format("{0:C2}", SortedList[i].amount) + "</span>"));
                tc3.Controls.Add(new LiteralControl("<span>" + SortedList[i].transactionDate.ToString() + "</span>"));
                tc5.Controls.Add(new LiteralControl("<span>" + SortedList[i].memo.ToString() + "</span>"));

                tr.Controls.Add(tc0);
                tr.Controls.Add(tc4);
                tr.Controls.Add(tc1);
                tr.Controls.Add(tc2);
                tr.Controls.Add(tc3);
                tr.Controls.Add(tc5);

                TransactionsTable.Controls.Add(tr);
            }
        }

        public TableRow setHeaders()
        {
            TableRow headers = new TableRow();
            TableHeaderCell h0 = new TableHeaderCell();
            TableHeaderCell h1 = new TableHeaderCell();
            TableHeaderCell h2 = new TableHeaderCell();
            TableHeaderCell h3 = new TableHeaderCell();
            TableHeaderCell h4 = new TableHeaderCell();
            TableHeaderCell h5 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>Transaction ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>To / From</span>"));
            h2.Controls.Add(new LiteralControl("<span>Amount</span>"));
            h3.Controls.Add(new LiteralControl("<span>Transaction Date</span>"));
            h4.Controls.Add(new LiteralControl("<span>Type</span>"));
            h5.Controls.Add(new LiteralControl("<span>Memo</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h4);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h3);
            headers.Controls.Add(h5);

            return headers;
        }

        public void getWalletBalance()
        {
            Global.sessionUser.wallet = DB.getUserWallet(Global.sessionUser.id);
            walletBalance.InnerText = "Current Balance: " + String.Format("{0:C2}", Global.sessionUser.wallet.currentAmount);
        }

    }
}