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
        /// <summary>
        /// Generates messages on the given web page
        /// </summary>
        /// <param name="message"></param>
        /// <param name="clientScriptManager"></param>
        public void generateToast(string message, ClientScriptManager clientScriptManager)
        {
            string toastMessage = message;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onbeforeunload = function(){");
            sb.Append("alert('");
            sb.Append(toastMessage);
            sb.Append("')};");
            sb.Append("</script>");
            clientScriptManager.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());
        }

        /// <summary>
        /// Returns the time of day as morning, afternoon, or evening
        /// </summary>
        /// <returns></returns>
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