using EZMoney;
using System;

namespace EZMoney.Models
{
    public class User
    {

        /// <summary>
        /// Unique Id for the user
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string firstName { get; set; } = "";

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string lastName { get; set; } = "";

        /// <summary>
        /// Username of the user
        /// </summary>
        public string username { get; set; } = "";

        /// <summary>
        /// Email of the user
        /// </summary>
        public string email { get; set; } = "";

        /// <summary>
        /// Phone number of the user
        /// </summary>
        public string phoneNumber { get; set; } = "";

        /// <summary>
        /// If the user is an administrator
        /// </summary>
        public bool isAdmin { get; set; } = false;

        /// <summary>
        /// If the user has been deleted
        /// </summary>
        public bool deleted { get; set; } = false;

        /// <summary>
        /// Users wallet
        /// </summary>
        public Wallet wallet { get; set; }

        /// <summary>
        /// Users company
        /// </summary>
        public Company company { get; set; }

        /// <summary>
        /// Users password
        /// </summary>
        public string password { get; set; }

        public User(string firstName, string lastName, string phone, string email, string username, string password, bool isAdmin, bool deleted)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phone;
            this.email = email;
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
            this.deleted = deleted;
        }

        public User(int id, string firstName, string lastName, string phone, string email, string username, bool isAdmin, bool deleted)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phone;
            this.email = email;
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
            this.deleted = deleted;
        }

        public User()
        {

        }

        /// <summary>
        /// Returns a user based on user id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static User getUserById(int id)
        {
            User user = DB.getUserById(id);
            if (user != null)
            {
                user.wallet = Wallet.getWalletByUserId(user.id);
                user.company = Company.getCompanyByUserId(user.id);
            }
            return user;
        }

        /// <summary>
        /// Returns a user based on username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User getUserByUsername(string username)
        {
            return DB.getUserByUsername(username);
        }

        /// <summary>
        /// Determines if the given password matches the password on record
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool checkPassword(string password)
        {
            return DB.checkPassword(password);
        }

        /// <summary>
        /// Save the user in the database
        /// </summary>
        public static bool save(User user)
        {
            DB db = new DB();

            return db.saveUser(user); 
        }

        /// <summary>
        /// Update the user in the database
        /// </summary>
        public static bool update(User user)
        {
            return DB.updateUser(user);
        }

        /// <summary>
        /// Updates the current users password
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool updatePassword(string password)
        {
            return DB.updatePassword(password);
        }

        /// <summary>
        /// Delete the user
        /// </summary>
        public static bool delete(int id)
        {
            return DB.deleteUser(id);
        }
    }
}
