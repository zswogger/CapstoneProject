using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using EZMoney.Models;

namespace EZMoney.Models
{
    public class General
    {
        public void generateToast(string message, ClientScriptManager clientScriptManager)
        {
            string toastMessage = message;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload=function(){");
            sb.Append("alert('");
            sb.Append(toastMessage);
            sb.Append("')};");
            sb.Append("</script>");
            clientScriptManager.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        public User refreshUser()
        {
            return (User)HttpContext.Current.Session["User"];
        }

        public string timeOfDay()
        {
            TimeSpan timeSinceMidnight = DateTime.Now - DateTime.Now.Date;
            int hours = timeSinceMidnight.Hours;

            if (hours < 12)
            {
                return "Morning";
            }
            else if (hours > 12 && hours < 6)
            {
                return "Afternon";
            }
            else
            {
                return "Evening";
            }
        }
    }
}