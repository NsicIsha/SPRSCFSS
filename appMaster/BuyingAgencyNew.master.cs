using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class appMaster_BuyingAgencyNew :  System.Web.UI.MasterPage
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
                lblLink.Text = Server.HtmlDecode("<a href=' <a href='./BuyingAgencyUpdateProfileNew.aspx'>Update Profile</a>&nbsp;|&nbsp; <a href='./BuyingAgencyChangePasswordNew.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../appBuyingAgency/BuyingAgencyLogout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            else
                lblLink.Text = Server.HtmlDecode("<a href='./BuyingAgencyUpdateProfileNew.aspx'>Update Profile</a>&nbsp;|&nbsp;<a href='./BuyingAgencyChangePasswordNew.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../appBuyingAgency/BuyingAgencyLogout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
        }
        catch { }




        try
        {
            if (Request.Cookies["OSUYULUKR8"] == null)
            {
                Response.Redirect("~/appBuyingAgency/BuyingAgencyLogin.aspx", false);
            }

            if (Session["OSUYULUKR8-BUYINSERYER"] == null)
            {
                Response.Redirect("~/appBuyingAgency/BuyingAgencyLogin.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/appBuyingAgency/BuyingAgencyLogin.aspx", false);
        }

        try
        {
            Response.Expires = 0;
            Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";
        }
        catch (Exception ex)
        { }
    }
}
