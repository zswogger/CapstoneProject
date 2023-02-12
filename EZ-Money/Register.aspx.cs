using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney.Models;

namespace EZMoney
{
    public partial class Register : System.Web.UI.Page
    {
        General gen = new General();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Attempt to register a user based on provided information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void registerUser(object sender, EventArgs e)
        {
            string firstName = FirstName.Text;
            string lastName = LastName.Text;

            string phone = PhoneNumber.Text;
            string stripped = Regex.Replace(phone, "[^0-9]", "");
            if (stripped.Length != 10)
            {
                gen.generateToast("You must enter a valid US phone number", ClientScript);
                return;
            }

            string email = Email.Text;
            if (!isValidEmail(email))
            {
                gen.generateToast("You must enter a valid email!", ClientScript);
                return;
            }

            if (!emailAvailable(email))
            {
                gen.generateToast("Email is already in use!", ClientScript);
                return;
            }

            string username = UserName.Text.ToLower();
            //TODO: ADD VALIDATION FOR USERNAME AND PASSWORD!

            string password = Password.Text;

            User newUser = new User(firstName, lastName, stripped, email, username, password, false, false);

            if (!EZMoney.Models.User.save(newUser))
            {
                gen.generateToast("Oops! Something went wrong! Please refresh and try again.", ClientScript);
                return;
            }

            Wallet wallet = new Wallet();
            wallet.userId = EZMoney.Models.User.getUserByUsername(username).id;
            wallet.currentAmount = 0;

            if (!Wallet.saveNewWallet(wallet.userId))
            {
                gen.generateToast("Oops! Something went wrong! Please refresh and try again.", ClientScript);
                return;
            }

            Response.Redirect("/Login");
        }

        /// <summary>
        /// Check if email is in a valid format
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private static bool isValidEmail(string email)
        {
            string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

            return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Check if the given email is available
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public static bool emailAvailable(string email)
        {
            DB db = new DB();

            return db.checkAttributeAvailability("checkEmail", "@Email", email);
        }
    }
}