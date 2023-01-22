using EZMoney;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EZMoney
{
    public partial class LoggedIn : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            if (Global.sessionUser.isAdmin)
            {
                AdminLink.Style.Remove("display");
                AdminLink.Style.Add("display", "inline-block");
            }

        }
    }
}