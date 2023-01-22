using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney.Models;
using EZMoney;

namespace EZMoney
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void loginUser(object sender, EventArgs e)
        {
            DB db = new DB();
            General gen = new General();
            DB.loginUser(UserName.Text, Password.Text);
            if(Global.sessionUser != null)
            {
                Response.Redirect("/Dashboard");
            }
            gen.generateToast("Incorrect username or password!", ClientScript);
        }
    }
}