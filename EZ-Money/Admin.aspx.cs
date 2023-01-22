using EZMoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney;

namespace EZMoney
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            if (!Global.sessionUser.isAdmin)
            {
                Response.Redirect("/Dashboard");
            }

            loadUsersTable();
            loadTransactionTable();
            loadProfitTable();
        }

        public void loadUsersTable()
        {
            // Set Headers for table
            setUsersHeaders();
            List<User> users = DB.getAllUsers(0, 50);
            foreach (User user in users)
            {
                user.wallet = Wallet.getWalletByUserId(user.id);
                setUserInfo(user);
            }
        }

        public void loadTransactionTable()
        {
            List<Transaction> transactions = DB.getAllUserTransactions(0, 50);

            TransactionsTable.Controls.Add(setHeaders());

            foreach (Transaction tx in transactions)
            {
                setTransactionInfo(tx);
            }
        }

        public void loadProfitTable()
        {
            List<Profit> profits = DB.getAllProfits(0, 50);

            setProfitHeaders();

            foreach (Profit profit in profits)
            {
                setProfitInfo(profit);
            }
        }

        public void setUsersHeaders()
        {

            TableRow headers = new TableRow();
            TableHeaderCell h0 = new TableHeaderCell();
            TableHeaderCell h1 = new TableHeaderCell();
            TableHeaderCell h2 = new TableHeaderCell();
            TableHeaderCell h3 = new TableHeaderCell();
            TableHeaderCell h4 = new TableHeaderCell();
            TableHeaderCell h5 = new TableHeaderCell();
            TableHeaderCell h6 = new TableHeaderCell();
            TableHeaderCell h7 = new TableHeaderCell();
            TableHeaderCell h8 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>User ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>Username</span>"));
            h2.Controls.Add(new LiteralControl("<span>First Name</span>"));
            h3.Controls.Add(new LiteralControl("<span>Last Name</span>"));
            h4.Controls.Add(new LiteralControl("<span>Email</span>"));
            h5.Controls.Add(new LiteralControl("<span>Phone</span>"));
            h6.Controls.Add(new LiteralControl("<span>Wallet Balance</span>"));
            h7.Controls.Add(new LiteralControl("<span>Admin</span>"));
            h8.Controls.Add(new LiteralControl("<span>Deleted</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h4);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h3);
            headers.Controls.Add(h5);
            headers.Controls.Add(h6);
            headers.Controls.Add(h7);
            headers.Controls.Add(h8);

            UsersTable.Controls.Add(headers);
        }

        public void setUserInfo(User user)
        {
            TableRow tr = new TableRow();
            TableCell tc0 = new TableCell();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();
            TableCell tc4 = new TableCell();
            TableCell tc5 = new TableCell();
            TableCell tc6 = new TableCell();
            TableCell tc7 = new TableCell();
            TableCell tc8 = new TableCell();

            tc0.Controls.Add(new LiteralControl("<span>" + user.id.ToString() + "</span>"));
            tc1.Controls.Add(new LiteralControl("<span>" + user.username + "</span>"));
            tc2.Controls.Add(new LiteralControl("<span>" + user.firstName + "</span>"));
            tc3.Controls.Add(new LiteralControl("<span>" + user.lastName + "</span>"));
            tc4.Controls.Add(new LiteralControl("<span>" + user.email + "</span>"));
            tc5.Controls.Add(new LiteralControl("<span>" + user.phoneNumber + "</span>"));
            tc6.Controls.Add(new LiteralControl("<span>$" + user.wallet.currentAmount + "</span>"));
            tc7.Controls.Add(new LiteralControl("<span>" + (user.isAdmin ? "Yes" : "No") + "</span>"));
            tc8.Controls.Add(new LiteralControl("<span>" + (user.deleted ? "Yes" : "No") + "</span>"));

            tr.Controls.Add(tc0);
            tr.Controls.Add(tc1);
            tr.Controls.Add(tc2);
            tr.Controls.Add(tc3);
            tr.Controls.Add(tc4);
            tr.Controls.Add(tc5);
            tr.Controls.Add(tc6);
            tr.Controls.Add(tc7);
            tr.Controls.Add(tc8);

            UsersTable.Controls.Add(tr);
        }

        public TableRow setHeaders()
        {
            TableRow headers = new TableRow();
            TableHeaderCell h0 = new TableHeaderCell();
            TableHeaderCell h1 = new TableHeaderCell();
            TableHeaderCell h2 = new TableHeaderCell();
            TableHeaderCell h4 = new TableHeaderCell();
            TableHeaderCell h5 = new TableHeaderCell();
            TableHeaderCell h6 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>Transaction ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>To</span>"));
            h2.Controls.Add(new LiteralControl("<span>From</span>"));
            h4.Controls.Add(new LiteralControl("<span>Amount</span>"));
            h5.Controls.Add(new LiteralControl("<span>Transaction Date</span>"));
            h6.Controls.Add(new LiteralControl("<span>Memo</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h4);
            headers.Controls.Add(h5);
            headers.Controls.Add(h6);

            return headers;
        }

        public void setTransactionInfo(Transaction tx)
        {

            TableRow tr = new TableRow();
            TableCell tc0 = new TableCell();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();
            TableCell tc4 = new TableCell();
            TableCell tc5 = new TableCell();

            tc0.Controls.Add(new LiteralControl("<span>" + tx.id + "</span>"));
            tc1.Controls.Add(new LiteralControl("<span>" + tx.toUserId + "</span>"));
            tc2.Controls.Add(new LiteralControl("<span>" + tx.fromUserId + "</span>"));
            tc3.Controls.Add(new LiteralControl("<span>" + tx.amount + "</span>"));
            tc4.Controls.Add(new LiteralControl("<span>" + tx.transactionDate + "</span>"));
            tc5.Controls.Add(new LiteralControl("<span>" + tx.memo + "</span>"));

            tr.Controls.Add(tc0);
            tr.Controls.Add(tc1);
            tr.Controls.Add(tc2);
            tr.Controls.Add(tc3);
            tr.Controls.Add(tc4);
            tr.Controls.Add(tc5);

            TransactionsTable.Controls.Add(tr);
        }

        public void setProfitHeaders()
        {
            TableRow headers = new TableRow();
            TableHeaderCell h0 = new TableHeaderCell();
            TableHeaderCell h1 = new TableHeaderCell();
            TableHeaderCell h2 = new TableHeaderCell();
            TableHeaderCell h3 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>Amount</span>"));
            h2.Controls.Add(new LiteralControl("<span>Date</span>"));
            h3.Controls.Add(new LiteralControl("<span>Refunded</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h3);

            ProfitsTable.Controls.Add(headers);
        }

        public void setProfitInfo(Profit profit)
        {
            TableRow tr = new TableRow();
            TableCell tc0 = new TableCell();
            TableCell tc1 = new TableCell();
            TableCell tc2 = new TableCell();
            TableCell tc3 = new TableCell();

            tc0.Controls.Add(new LiteralControl("<span>" + profit.id + "</span>"));
            tc1.Controls.Add(new LiteralControl("<span>" + profit.profitAmount + "</span>"));
            tc2.Controls.Add(new LiteralControl("<span>" + profit.profitDate + "</span>"));
            tc3.Controls.Add(new LiteralControl("<span>" + (profit.refunded ? "Yes" : "No") + "</span>"));

            tr.Controls.Add(tc0);
            tr.Controls.Add(tc1);
            tr.Controls.Add(tc2);
            tr.Controls.Add(tc3);

            ProfitsTable.Controls.Add(tr);
        }
    }
}