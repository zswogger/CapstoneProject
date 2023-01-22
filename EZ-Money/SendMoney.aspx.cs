
using EZMoney.Models;
using System;
using EZMoney;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EZMoney
{
    public partial class SendMoney : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }
            getWalletBalance();
        }

        protected void sendTransaction(object sender, EventArgs e)
        {
            General gen = new General();

            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            Transaction tx = new Transaction();
            try
            {
                // Set sending user id
                tx.fromUserId = Global.sessionUser.id;

                User toUser = DB.getUserByUsername(Username.Text);
                toUser.wallet = Wallet.getWalletByUserId(toUser.id);

                if (toUser.id == 0 || toUser.wallet == null)
                {
                    gen.generateToast("Recipient not found, please try again with a valid username.", ClientScript);
                }

                // Set receiving user id if the user exists
                tx.toUserId = toUser.id;

                
                Decimal.TryParse(Amount.Text, out Decimal amount);

                // Validate amount is > 0 and set tx amount
                if (amount > 0)
                {
                    tx.amount = amount;
                }
                else
                {
                    gen.generateToast("Transaction amount must be greater than $0.01.", ClientScript);
                }

                // Generate profit
                Profit profit = new Profit();
                profit.profitAmount = profit.calculateProfit(tx);
                profit.profitDate = DateTime.Now.ToString();
                profit.refunded = false;
                tx.profit = profit;

                //Validate user can cover amount
                if (Global.sessionUser.wallet.currentAmount < amount + profit.profitAmount)
                {
                    gen.generateToast("You do not have enough funds to cover the transaction. Please deposit more funds and try again.", ClientScript);
                }

                // Set tx date to right now
                tx.transactionDate = DateTime.Now.ToString();

                // If memo exists, set memo
                if (Memo.Text != null)
                {
                    tx.memo = Memo.Text;
                }

                Global.sessionUser.wallet.currentAmount -= amount + profit.profitAmount;
                toUser.wallet.currentAmount += amount;

                if (!profit.save())
                {
                    gen.generateToast("Something went wrong saving profit!", ClientScript);
                }

                if (!tx.save())
                {
                    gen.generateToast("Something went wrong saving transaction!", ClientScript);
                }

                if (!Global.sessionUser.wallet.saveCurrentBalance(Global.sessionUser.id, Global.sessionUser.wallet.currentAmount))
                {
                    gen.generateToast("Something went wrong saving wallet balance!", ClientScript);
                }

                if (!toUser.wallet.saveCurrentBalance(toUser.id, toUser.wallet.currentAmount))
                {
                    gen.generateToast("Something went wrong saving recipient wallet balance!", ClientScript);
                }

                gen.generateToast("Successfully sent $" + tx.amount.ToString() + " to " + toUser.username, ClientScript); ;
                getWalletBalance();
                cleanControls();
            }
            catch (Exception ex)
            {
                gen.generateToast(ex.Message, ClientScript);
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