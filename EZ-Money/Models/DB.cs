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

        /// <summary>
        /// Return all users within start and stop range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Save a user on registration
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static User loginUser(string username, string password)
        {
            User user = null;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("loginUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("usernameToCheck", MySqlDbType.VarChar).Value = username;
                    cmd.Parameters.Add("passwordToCheck", MySqlDbType.VarChar).Value = password;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        user = new User();
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
            if (user != null)
            {
                user.wallet = Wallet.getWalletByUserId(user.id);
                user.company = Company.getCompanyByUserId(user.id);
                Global.setGlobalUser(user);
            }
            return user;
        }

        /// <summary>
        /// Return a user based on their specific user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return a user based on their username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User getUserByUsername(string username)
        {
            User user = null;
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
                        user = new User();
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
                            user.deleted = rdr.GetBoolean(7);
                        }
                        rdr.NextResult();
                    }
                }
            }
            return user;
        }

        /// <summary>
        /// Check if a password matches what is on file
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool checkPassword(string password)
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("checkPassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("passToCheck", MySqlDbType.VarChar).Value = password;
                    cmd.Parameters.Add("userIdToCheck", MySqlDbType.Int64).Value = Global.sessionUser.id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Update a users information
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool updateUser(User user)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("updateUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = Global.sessionUser.id;
                    cmd.Parameters.Add("FirstName", MySqlDbType.VarChar).Value = user.firstName;
                    cmd.Parameters.Add("LastName", MySqlDbType.VarChar).Value = user.lastName;
                    cmd.Parameters.Add("PhoneNumber", MySqlDbType.VarChar).Value = user.phoneNumber;
                    cmd.Parameters.Add("Email", MySqlDbType.VarChar).Value = user.email;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Update a users password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool updatePassword(string password)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("updatePassword", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = Global.sessionUser.id;
                    cmd.Parameters.Add("newPass", MySqlDbType.VarChar).Value = password;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static bool deleteUser(int userId)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("deleteUser", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = userId;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }
        #endregion


        #region Transactions
        /// <summary>
        /// Return all transactions within a given range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
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
                                transactions.Add(new Transaction(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetDecimal(3), rdr.GetDecimal(4), rdr.GetString(5), rdr.GetString(6), rdr.GetInt32(7)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return transactions;
        }

        /// <summary>
        /// Return a user transaction with a specific user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                                transactions.Add(new Transaction(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetDecimal(3), rdr.GetDecimal(4), rdr.GetString(5), rdr.GetString(6), rdr.GetInt32(7)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return transactions;
        }

        /// <summary>
        /// Save a transaction
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
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
                    cmd.Parameters.Add("@txComplete", MySqlDbType.Int32).Value = tx.complete;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Complete a requested transaction
        /// </summary>
        /// <param name="tx"></param>
        /// <returns></returns>
        public static bool completeTransaction(Transaction tx)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("completeTransaction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@txIdInc", MySqlDbType.Int32).Value = tx.id;
                    cmd.Parameters.Add("@feeAmountInc", MySqlDbType.Decimal).Value = tx.profit.profitAmount;
                    cmd.Parameters.Add("@transactionDateInc", MySqlDbType.VarChar).Value = tx.transactionDate;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Deny a requested transaction
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool denyTransaction(int id)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("denyTransaction", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int32).Value = id;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Get all transactions requested of a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Transaction> getPendingTransactions(int id)
        {
            List<Transaction> transactions = new List<Transaction>();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getPendingTransactions", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                transactions.Add(new Transaction(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetDecimal(3), rdr.GetDecimal(4), rdr.GetString(5), rdr.GetString(6), rdr.GetInt32(7)));
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return transactions;
        }

        /// <summary>
        /// Return a transaction based on the transaction ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Transaction getTransactionByID(int id)
        {
            Transaction tx = new Transaction();
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getTransactionById", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                tx.id = rdr.GetInt32(0);
                                tx.fromUserId = rdr.GetInt32(1);
                                tx.toUserId= rdr.GetInt32(2);
                                tx.amount= rdr.GetDecimal(3);
                                tx.memo = rdr.GetString(6);
                                tx.complete = rdr.GetInt32(7);
                            }
                            rdr.NextResult();
                        }
                    }
                }
            }
            return tx;
        }

        #endregion

        #region Wallet

        public static bool saveNewWallet(int userId)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("saveNewWallet", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@userIdInc", MySqlDbType.Int32).Value = userId;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        /// <summary>
        /// Return a users wallet based on the users id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Save the current balance of a users wallet
        /// </summary>
        /// <param name="id"></param>
        /// <param name="currentBalance"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check availability of an attribute such as email or username
        /// </summary>
        /// <param name="procedure"></param>
        /// <param name="attribute"></param>
        /// <param name="toCheck"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Save a profit
        /// </summary>
        /// <param name="profit"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Return all profits within a range
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
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

        #region Company
        public static bool registerCompany(Company company)
        {
            bool success = false;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("registerCompany", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("companyUserId", MySqlDbType.Int32).Value = company.userId;
                    cmd.Parameters.Add("companyName", MySqlDbType.VarChar).Value = company.name;
                    cmd.Parameters.Add("companySite", MySqlDbType.VarChar).Value = company.website;
                    cmd.Parameters.Add("companyLogo", MySqlDbType.VarChar).Value = company.logoUrl;
                    cmd.Parameters.Add("companyAddress1", MySqlDbType.VarChar).Value = company.address1;
                    cmd.Parameters.Add("companyAddress2", MySqlDbType.VarChar).Value = company.address2;
                    cmd.Parameters.Add("companyCity", MySqlDbType.VarChar).Value = company.city;
                    cmd.Parameters.Add("companyState", MySqlDbType.VarChar).Value = company.state;
                    cmd.Parameters.Add("companyZip", MySqlDbType.VarChar).Value = company.zip;
                    cmd.Parameters.Add("companyEIN", MySqlDbType.VarChar).Value = company.ein;

                    con.Open();
                    success = cmd.ExecuteNonQuery() > 0;
                }
            }
            return success;
        }

        public static Company getCompanyByUserId(int id)
        {
            Company company = null;
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand("getCompanyByUserId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("idToCheck", MySqlDbType.Int64).Value = id;

                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
                    if (rdr.HasRows)
                    {
                        company = new Company();
                        while (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                company.id = rdr.GetInt32(0);
                                company.userId = rdr.GetInt32(1);
                                company.name = rdr.GetString(2);
                                company.website= rdr.GetString(3);
                                company.logoUrl= rdr.GetString(4);
                                company.address1= rdr.GetString(5);
                                company.address2 = rdr.GetString(6);
                                company.city= rdr.GetString(7);
                                company.state= rdr.GetString(8);
                                company.zip= rdr.GetString(9);
                                company.ein= rdr.GetString(10);
                            }
                            rdr.NextResult();
                        }
                    }
                }
                return company;
            }
        }
        #endregion
    }
}