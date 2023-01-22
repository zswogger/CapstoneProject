using EZMoney.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Web.Util;
using MySqlX.XDevAPI;

namespace EZMoney.Models
{
    public class DB
    {
        public static string connectionString = "Server=localhost;Port=3306;Database=ez-money;Uid=root;Pwd=root;";

        #region User

        public static List<User> getAllUsers(int start, int stop)
        {
            List<User> users = new List<User>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getAllUsers", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("startRow", MySqlDbType.Int64).Value = start;
                    cmd.Parameters.Add("stopRow", MySqlDbType.Int64).Value = stop;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                users.Add(new User(rdr.GetInt32(0),rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetBoolean(6), rdr.GetBoolean(7)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return users;
        }

        public bool saveUser(User user)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("saveUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = user.username;
                    cmd.Parameters.Add("@Password", MySqlDbType.VarChar).Value = user.password;
                    cmd.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = user.firstName;
                    cmd.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = user.lastName;
                    cmd.Parameters.Add("@PhoneNumber", MySqlDbType.VarChar).Value = user.phoneNumber;
                    cmd.Parameters.Add("@Email", MySqlDbType.VarChar).Value = user.email;
                    cmd.Parameters.Add("@Admin", MySqlDbType.Int64).Value = user.isAdmin;
                    cmd.Parameters.Add("@Deleted", MySqlDbType.Int64).Value = user.deleted;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public static User loginUser(string username, string password)
        {
            User user = null;

            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = "SELECT id, username, firstName, lastName, phoneNumber, emailAddress, isAdmin FROM users WHERE username = '" + username + "' AND password = '" + password + "';";

            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            while (rdr.Read())
            {
                user = new User();
                user.id = rdr.GetInt32(0);
                user.username = rdr.GetString(1);
                user.firstName = rdr.GetString(2);
                user.lastName = rdr.GetString(3);
                user.phoneNumber = rdr.GetString(4);
                user.email = rdr.GetString(5);
                user.isAdmin = rdr.GetBoolean(6);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            Global.setGlobalUser(user);
            return user;
        }

        public static User getUserById(int id)
        {
            User user = new User();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("userId", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        Console.WriteLine();
                        while (rdr.Read())
                        {
                            user.id = rdr.GetInt32(0);
                            user.username = rdr.GetString(1);
                            user.firstName = rdr.GetString(2);
                            user.lastName = rdr.GetString(3);
                            user.phoneNumber = rdr.GetString(4);
                            user.email = rdr.GetString(5);
                            user.isAdmin = rdr.GetBoolean(6);
                        }
                        rdr.NextResult();
                    }
                }
            }
            return user;
        }

        public static User getUserByUsername(string username)
        {
            User user = new User();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getUserByUsername", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("usernameToFind", MySqlDbType.VarChar).Value = username;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        Console.WriteLine();
                        while (rdr.Read())
                        {
                            user.id = rdr.GetInt32(0);
                            user.username = rdr.GetString(1);
                            user.firstName = rdr.GetString(2);
                            user.lastName = rdr.GetString(3);
                            user.phoneNumber = rdr.GetString(4);
                            user.email = rdr.GetString(5);
                            user.isAdmin = rdr.GetBoolean(6);
                        }
                        rdr.NextResult();
                    }
                }
            }
            return user;
        }
        #endregion


        #region Transactions

        public static List<Transaction> getAllUserTransactions(int start, int stop)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getAllUserTransactions", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("startRow", MySqlDbType.Int64).Value = start;
                    cmd.Parameters.Add("stopRow", MySqlDbType.Int64).Value = stop;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                transactions.Add(new Transaction(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetDecimal(3), rdr.GetDecimal(4), rdr.GetString(5), rdr.GetString(6)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return transactions;
        }

        public static List<Transaction> getUserTransactions(int id)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getUserTransactions", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("userId", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while(rdr.HasRows)
                        {
                            while(rdr.Read())
                            {
                                transactions.Add(new Transaction(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetDecimal(3), rdr.GetDecimal(4), rdr.GetString(5), rdr.GetString(6)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return transactions;
        }

        public static bool saveTransaction(Transaction tx)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("saveTransaction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@fromUserIdInc", MySqlDbType.Int32).Value = tx.fromUserId;
                    cmd.Parameters.Add("@toUserIdInc", MySqlDbType.Int32).Value = tx.toUserId;
                    cmd.Parameters.Add("@amountInc", MySqlDbType.Decimal).Value = tx.amount;
                    cmd.Parameters.Add("@feeAmountInc", MySqlDbType.Decimal).Value = tx.profit.profitAmount;
                    cmd.Parameters.Add("@transactionDateInc", MySqlDbType.VarChar).Value = tx.transactionDate;
                    cmd.Parameters.Add("@memoInc", MySqlDbType.VarChar).Value = tx.memo;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        #endregion

        #region Wallet
        public static Wallet getUserWallet(int id)
        {
            Wallet wallet = new Wallet();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getWallet", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("usersId", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                wallet.id= rdr.GetInt32(0);
                                wallet.userId= rdr.GetInt32(1);
                                wallet.currentAmount= rdr.GetDecimal(2);
                            }
                            rdr.NextResult();
                        }
                    }
                }
                return wallet;
            }
        }

        public static bool saveWalletBalance(int id, decimal currentBalance)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("saveWalletBalance", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@userIdInc", MySqlDbType.Int32).Value = id;
                    cmd.Parameters.Add("@currentAmountInc", MySqlDbType.Decimal).Value = currentBalance;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        #endregion

        #region Misc
        public bool checkAttributeAvailability(string procedure, string attribute, string toCheck)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand(procedure, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(attribute, MySqlDbType.VarChar).Value = toCheck;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (!rdr.HasRows)
                    {
                        success = true;
                    }
                }
            }
            return success;
        }
        #endregion

        #region Profit
        public static bool saveProfit(Profit profit)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("saveProfit", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@profitAmountIncoming", MySqlDbType.Decimal).Value = profit.profitAmount;
                    cmd.Parameters.Add("@profitRefunded", MySqlDbType.Bit).Value = profit.refunded;
                    cmd.Parameters.Add("@profitDateIncoming", MySqlDbType.VarChar).Value = profit.profitDate;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public static List<Profit> getAllProfits(int start, int stop)
        {
            List<Profit> profits = new List<Profit>();

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getAllProfits", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("startRow", MySqlDbType.Int64).Value = start;
                    cmd.Parameters.Add("stopRow", MySqlDbType.Int64).Value = stop;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                profits.Add(new Profit(rdr.GetInt32(0), rdr.GetDecimal(1), rdr.GetString(3), rdr.GetBoolean(2)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return profits;
        }
        #endregion
    }
}