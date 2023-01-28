using EZMoney;
using EZMoney.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EZ_Money
{
    public partial class Settings : System.Web.UI.Page
    {

        static int passwordAttempts = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            Global.sessionUser = EZMoney.Models.User.getUserById(Global.sessionUser.id);

            if (!Page.IsPostBack)
            {
                loadUserInfo();
            }
        }

        public void loadUserInfo()
        {
            UserName.Text = Global.sessionUser.username;
            FirstName.Text = Global.sessionUser.firstName;
            LastName.Text = Global.sessionUser.lastName;
            Email.Text = Global.sessionUser.email;
            PhoneNumber.Text = Global.sessionUser.phoneNumber;
        }

        /// <summary>
        /// Update user information
        /// </summary>
        public void saveUser(object sender, EventArgs e)
        {
            General gen = new General();
            User user = new User();
            user.firstName = FirstName.Text;
            user.lastName = LastName.Text;
            user.email = Email.Text;
            user.phoneNumber = String.Format("{0:(###)-###-####}", PhoneNumber.Text);

            if (!EZMoney.Models.User.update(user))
            {
                gen.generateToast("Oops! Something went wrong! Please try again!", ClientScript);
            }

            bool changingPassword = CurrentPass.Text != "" || NewPass1.Text != "" || NewPass2.Text != "";

            if (changingPassword)
            {
                // Ensure all fields are filled out
                if (CurrentPass.Text != "" && NewPass1.Text != "" && NewPass2.Text != "")
                {
                    // Ensure old password matches password on file. After 3 attempts this will log the user out.
                    if (EZMoney.Models.User.checkPassword(CurrentPass.Text))
                    {
                        // Ensure both new pass fields are the same
                        if (NewPass1.Text == NewPass2.Text)
                        {
                            // Ensure new password and old are not the same
                            if (NewPass1.Text != CurrentPass.Text)
                            {
                                if (!EZMoney.Models.User.updatePassword(NewPass1.Text))
                                {
                                    gen.generateToast("Oops! Something went wrong updating password!", ClientScript);
                                    return;
                                }
                            }
                            else
                            {
                                gen.generateToast("New password cannot be the same as the old!", ClientScript);
                            }
                        }
                        else
                        {
                            gen.generateToast("New passwords must match!", ClientScript);
                            return;
                        }
                    }
                    else
                    {
                        gen.generateToast("Current password was incorrect. Please try again.", ClientScript);
                        passwordAttempts++;

                        if (passwordAttempts == 2)
                        {
                            passwordAttempts = 0;
                            Global.sessionUser = null;
                            Response.Redirect("/Login");
                        }
                        return;
                    }
                }
                else
                {
                    gen.generateToast("You must fill out all password fields to change your password!", ClientScript);
                    return;
                }
            }

            string toastString = "Successfully updated ";
            toastString += FirstName.Text == Global.sessionUser.firstName ? "" : "First Name ";
            toastString += LastName.Text == Global.sessionUser.lastName ? "" : "Last Name ";
            toastString += PhoneNumber.Text == Global.sessionUser.phoneNumber ? "" : "Phone Number ";
            toastString += Email.Text == Global.sessionUser.email ? "" : "Email ";
            toastString += changingPassword ? "Password" : "";

            CurrentPass.Text = String.Empty;
            NewPass1.Text = String.Empty;
            NewPass2.Text = String.Empty;

            Global.sessionUser = EZMoney.Models.User.getUserById(Global.sessionUser.id);
            gen.generateToast(toastString, ClientScript);
        }
        protected void deleteUser(object sender, EventArgs e)
        {
            EZMoney.Models.User.delete(Global.sessionUser.id);
            Global.sessionUser = null;
            Response.Redirect("/Login");
        }
    }
}