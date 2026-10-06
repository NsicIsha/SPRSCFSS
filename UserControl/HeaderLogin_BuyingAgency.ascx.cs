using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UserControl_HeaderLogin_BuyingAgency : System.Web.UI.UserControl
{
    string UserFullName;
    protected void Page_Load(object sender, EventArgs e)
    {
         try
        {
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        try
        {
            if (Request.Cookies["OSUYULUKR8"]["USERID"].ToString() == "0")
                lblLink.Visible = false;
            else
                lblLink.Visible = true;
        }
        catch (Exception ex)
        {
        }

        lblUser.Text = "Welcome <b><i>" + UserFullName.ToUpper() + "</i></b>";
        try
        {
            if (Request.Cookies["OSUYULUKR8"]["ROLEID"].ToString() == "7")
                lblLink.Text = Server.HtmlDecode("<a href=' <a href='./BuyingAgencyUpdateProfile.aspx'>Update Profile</a>&nbsp;|&nbsp; <a href='./BuyingAgencyChangePassword.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../appBuyingAgency/BuyingAgencyLogout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            else
                lblLink.Text = Server.HtmlDecode("<a href='./BuyingAgencyUpdateProfile.aspx'>Update Profile</a>&nbsp;|&nbsp;<a href='./BuyingAgencyChangePassword.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../appBuyingAgency/BuyingAgencyLogout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
        }
        catch { }

    }
}
