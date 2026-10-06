using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appMaster_AgencyUpload : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["OSUYULUKR8"] == null) Response.Redirect("~/appInspectionAgency/Login.aspx", false);

            if (Session["OSUYULUKR8-INSERYER"] == null) Response.Redirect("~/appInspectionAgency/Login.aspx", false);
        }
        catch (Exception ex)
        {
            Response.Redirect("~/appInspectionAgency/Login.aspx", false);
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
