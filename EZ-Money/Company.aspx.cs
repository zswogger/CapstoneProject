using EZMoney;
using System;
using EZMoney.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EZ_Money
{
    public partial class Company : System.Web.UI.Page
    {
        General gen = new General();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Global.sessionUser == null)
            {
                Response.Redirect("/Login");
            }

            if (Global.sessionUser.company == null)
            {
                NoCompany.Visible = true;
            }
            else
            {
                loadCompanyInfo();
                YesCompany.Visible = true;
            }
        }

        protected void loadCompanyInfo()
        {
            CompanyLogo.Text += "<img src=" + '"' + Global.sessionUser.company.logoUrl + '"' + "/>";
            DisplayCompanyName.Text = Global.sessionUser.company.name;
            DisplayCompanySite.Text = Global.sessionUser.company.website;
            DisplayAdd1.Text = Global.sessionUser.company.address1;
            if (Global.sessionUser.company.address2 != "")
            {
                DisplayAdd2.Text = Global.sessionUser.company.address2;
                DisplayAdd2.Visible = true;
            }
            DisplayCity.Text = Global.sessionUser.company.city;
            DisplayState.Text = Global.sessionUser.company.state;
            DisplayZip.Text = Global.sessionUser.company.zip;
            DisplayEIN.Text = Global.sessionUser.company.ein;
        }

        protected void registerCompany(object sender, EventArgs e)
        {
            EZMoney.Models.Company company = new EZMoney.Models.Company();
            company.name = CompanyName.Text;
            company.website = CompanyURL.Text;
            company.logoUrl = LogoUrl.Text;
            company.address1 = Address1.Text;
            company.address2 = Address2.Text;
            company.city = City.Text;
            company.state = State.Text;
            company.zip = Zip.Text;
            company.ein = EIN.Text;
            company.userId = Global.sessionUser.id;

            if (!company.register())
            {
                gen.generateToast("Something went wrong! Please refresh and try again.", ClientScript);
            }
            Global.sessionUser.company= company;
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}