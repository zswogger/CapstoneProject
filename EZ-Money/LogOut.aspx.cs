﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EZMoney;

namespace EZMoney
{
    public partial class LogOut : System.Web.UI.Page
    {
        /// <summary>
        /// Log out the current user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Global.sessionUser= null;
            Response.Redirect("/Login");
        }
    }
}