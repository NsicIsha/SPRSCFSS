using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appMaster_AdminOnline : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
        //    if (Request.Cookies["OSUYULUKR8"] == null)
        //    {
        //        Response.Redirect("~/appAdministrator/Login.aspx", false);
        //    }

        //    if (Session["OSUYULUKR8-ERUYIRE"] == null)
        //    {
        //        Response.Redirect("~/appAdministrator/Login.aspx", false);
        //    }
        //}
        //catch (Exception ex) 
        //{
        //    try
        //    {
        //        Response.Redirect("~/appAdministrator/Logout.aspx", false);
        //    }
        //    catch { }
        //}

        //try
        //{
        //    Response.Expires = 0;
        //    Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        //    Response.AddHeader("cache-control", "private");
        //    Response.CacheControl = "no-cache";
        //}
        //catch (Exception ex)
        //{ }
    }
}
