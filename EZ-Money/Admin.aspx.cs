﻿using EZMoney.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney;
using System.Web.Services;

namespace EZMoney
{
    public partial class Admin : System.Web.UI.Page
    {
        public static int userStart = 1, userEnd = 50, transactionStart = 1, transactionEnd = 50, profitStart = 1, profitEnd = 50;
        General gen = new General();

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

        public void nextPage(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;
            switch (id)
            {
                case "nextUser":
                    userStart += 50;
                    userEnd += 50;
                    checkPage(userStart, userEnd, 0);
                    loadUsersTable();
                    tabSelector.Value = "0";
                    break;

                case "nextTransaction":
                    transactionStart += 50;
                    transactionEnd += 50;
                    loadTransactionTable();
                    tabSelector.Value = "1";
                    break;

                case "nextProfit":
                    profitStart += 50;
                    profitEnd += 50;
                    loadProfitTable();
                    tabSelector.Value = "2";
                    break;
            }
        }

        public void checkPage(int start, int stop, int tab)
        {
            if (start <= 0 || stop < 50)
            {
                switch(tab)
                {
                    case 0:
                        userStart = 1;
                        userEnd = 50;
                        break;
                    case 1:
                        transactionStart = 1;
                        transactionEnd = 50;
                        break;
                    case 2:
                        profitStart = 1;
                        profitEnd = 50;
                        break;
                }
                gen.generateToast("Something went wrong! Moving back to first page", ClientScript);
            }
        }

        public void prevPage(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID;
            switch (id)
            {
                case "prevUser":
                    if (userStart <= 1)
                    {
                        gen.generateToast("No previous records.", ClientScript);
                        return;
                    }
                    userStart -= 50;
                    userEnd -= 50;
                    checkPage(userStart, userEnd, 0);
                    loadUsersTable();
                    tabSelector.Value = "0";
                    break;

                case "prevTransaction":
                    if (transactionStart <= 1)
                    {
                        gen.generateToast("No previous records.", ClientScript);
                        return;
                    }
                    transactionStart -= 50;
                    transactionEnd -= 50;
                    loadTransactionTable();
                    tabSelector.Value = "1";
                    break;

                case "prevProfit":
                    if (profitStart <= 1)
                    {
                        gen.generateToast("No previous records.", ClientScript);
                        return;
                    }
                    profitStart -= 50;
                    profitEnd -= 50;
                    loadProfitTable();
                    tabSelector.Value = "2";
                    break;
            }
        }

        public void searchUserName(Object sender, EventArgs e)
        {
            string username = SearchUsername.Text;
            
            if (username != "")
            {
                User user = DB.getUserByUsername(username);
                if (user != null)
                {
                    List<User> users = new List<User>() { user };
                    loadUsersTable(users);
                    SearchUsername.Text = "";
                }
                else
                {
                    gen.generateToast("Could not find a user with that name", ClientScript);
                    loadUsersTable();
                }
            }
            else
            {
                loadUsersTable();
            }
            tabSelector.Value = "0";
        }

        public void searchTxUsername(Object sender, EventArgs e)
        {
            if(SearchTxUsername.Text != "")
            {
                User user = DB.getUserByUsername(SearchTxUsername.Text);
                if (user != null)
                {
                    List<Transaction> SortedList = DB.getUserTransactions(user.id).OrderByDescending(o => o.id).ToList();
                    loadTransactionTable(SortedList);
                }
                else
                {
                    gen.generateToast("Could not find a user with that name", ClientScript);
                    loadTransactionTable();
                }
            }
            else
            {
                loadTransactionTable();
            }
            SearchTxUsername.Text = "";
            tabSelector.Value = "1";
        }


        public void loadUsersTable(List<User> users = null)
        {
            if (UsersTable != null)
            {
                UsersTable.Rows.Clear();
                UsersTable.Controls.Clear();
            }

            // Set Headers for table
            setUsersHeaders();
            if (users == null)
            {
                users = DB.getAllUsers(userStart, userEnd);
            }
            foreach (User user in users)
            {
                user.wallet = Wallet.getWalletByUserId(user.id);
                setUserInfo(user);
            }
        }

        public void loadTransactionTable(List<Transaction> transactions = null)
        {
            if (TransactionsTable != null)
            {
                TransactionsTable.Rows.Clear();
                TransactionsTable.Controls.Clear();
            }

            if (transactions == null)
            {
                transactions = DB.getAllUserTransactions(transactionStart, transactionEnd);
            }

            TransactionsTable.Controls.Add(setHeaders());

            foreach (Transaction tx in transactions)
            {
                setTransactionInfo(tx);
            }
        }

        public void loadProfitTable()
        {
            decimal totalProfit = 0;
            if (ProfitsTable != null)
            {
                ProfitsTable.Rows.Clear();
                ProfitsTable.Controls.Clear();
            }

            List<Profit> profits = DB.getAllProfits(profitStart, profitEnd);

            setProfitHeaders();

            foreach (Profit profit in profits)
            {
                setProfitInfo(profit);
                totalProfit += profit.profitAmount;
            }
            TotalProfit.Text = "Total Profit On Page: " + totalProfit.ToString();
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
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h3);
            headers.Controls.Add(h4);
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
            tc6.Controls.Add(new LiteralControl("<span>" + String.Format("{0:C2}", user.wallet.currentAmount) + "</span>"));
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
            TableHeaderCell h7 = new TableHeaderCell();

            h0.Controls.Add(new LiteralControl("<span>Transaction ID</span>"));
            h1.Controls.Add(new LiteralControl("<span>To</span>"));
            h2.Controls.Add(new LiteralControl("<span>From</span>"));
            h4.Controls.Add(new LiteralControl("<span>Amount</span>"));
            h5.Controls.Add(new LiteralControl("<span>Transaction Date</span>"));
            h6.Controls.Add(new LiteralControl("<span>Memo</span>"));
            h7.Controls.Add(new LiteralControl("<span>Status</span>"));

            headers.Controls.Add(h0);
            headers.Controls.Add(h1);
            headers.Controls.Add(h2);
            headers.Controls.Add(h4);
            headers.Controls.Add(h5);
            headers.Controls.Add(h6);
            headers.Controls.Add(h7);

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
            TableCell tc6 = new TableCell();

            tc0.Controls.Add(new LiteralControl("<span>" + tx.id + "</span>"));
            tc1.Controls.Add(new LiteralControl("<span>(" + tx.toUserId + ") " + EZMoney.Models.User.getUserById(tx.toUserId).username + "</span>"));
            tc2.Controls.Add(new LiteralControl("<span>(" + tx.fromUserId +") " + EZMoney.Models.User.getUserById(tx.fromUserId).username + "</span>"));
            tc3.Controls.Add(new LiteralControl("<span>" + String.Format("{0:C2}", tx.amount) + "</span>"));
            tc4.Controls.Add(new LiteralControl("<span>" + tx.transactionDate + "</span>"));
            tc5.Controls.Add(new LiteralControl("<span>" + tx.memo + "</span>"));

            switch (tx.complete)
            {
                case 0:
                    tc6.Controls.Add(new LiteralControl("<span>Complete</span>"));
                    break;
                case 1:
                    tc6.Controls.Add(new LiteralControl("<span>Pending</span>"));
                    break;
                case 2:
                    tc6.Controls.Add(new LiteralControl("<span>Denied</span>"));
                    break;
            }

            tr.Controls.Add(tc0);
            tr.Controls.Add(tc1);
            tr.Controls.Add(tc2);
            tr.Controls.Add(tc3);
            tr.Controls.Add(tc4);
            tr.Controls.Add(tc5);
            tr.Controls.Add(tc6);

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
            tc1.Controls.Add(new LiteralControl("<span>" + String.Format("{0:C2}", profit.profitAmount) + "</span>"));
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