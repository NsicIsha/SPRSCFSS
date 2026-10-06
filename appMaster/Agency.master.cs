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

public partial class appMaster_Agency : System.Web.UI.MasterPage
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
