using EZMoney;
using EZMoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EZ_Money
{
    public partial class RequestMoney : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }
            getWalletBalance();
        }

        protected void requestTransaction(object sender, EventArgs e)
        {
            General gen = new General();

            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            Transaction tx = new Transaction();
            try
            {
                // Set requested user id
                tx.toUserId = Global.sessionUser.id;

                // Get requested user
                User fromUser = DB.getUserByUsername(Username.Text);
                if (fromUser == null)
                {
                    gen.generateToast("Recipient not found, please try again with a valid username.", ClientScript);
                    return;
                }

                tx.fromUserId = fromUser.id;

                Decimal.TryParse(Amount.Text, out Decimal amount);

                // Validate amount is > 0 and set tx amount
                if (amount > 0)
                {
                    tx.amount = amount;
                }
                else
                {
                    gen.generateToast("Transaction amount must be greater than $0.01.", ClientScript);
                    return;
                }

                // Generate profit
                Profit profit = new Profit();
                profit.profitAmount = profit.calculateProfit(tx);
                profit.profitDate = DateTime.Now.ToString();
                profit.refunded = false;
                tx.profit = profit;
                profit.complete = false;

                // Set tx date to right now
                tx.transactionDate = DateTime.Now.ToString();

                // If memo exists, set memo
                if (Memo.Text != null)
                {
                    tx.memo = Memo.Text;
                }

                if (!profit.save())
                {
                    gen.generateToast("Something went wrong saving profit!", ClientScript);
                }
                tx.complete = 0;
                if (!tx.save())
                {
                    gen.generateToast("Something went wrong saving transaction!", ClientScript);
                    return;
                }

                gen.generateToast("Successfully requested $" + tx.amount.ToString() + " from " + fromUser.username, ClientScript);
                cleanControls();
            }
            catch (Exception ex)
            {
                gen.generateToast(ex.Message, ClientScript);
                return;
            }
        }

        public void getWalletBalance()
        {
            decimal balance = DB.getUserWallet(Global.sessionUser.id).currentAmount;
            string formatted = balance.ToString();
            walletBalance.InnerText = "Current Balance: $" + formatted;
        }

        public void cleanControls()
        {
            Username.Text = "";
            Amount.Text = "";
            Memo.Text = "";
        }
    }
}