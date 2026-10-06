using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appCompany_CompanyLogout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int i;
            HttpCookie acookie = new HttpCookie("OSUYULUKR8");
            int limit = (Request.Cookies.Count - 1);
            for (i = 0; (i <= limit); i++)
            {
                acookie = Request.Cookies[i];
                acookie.Expires = DateTime.Now.AddHours(-1);
                Response.Cookies.Add(acookie);
            }
            //Disable back in browser -----------------
            Session.Abandon();
            Application.RemoveAll();
            Response.Buffer = false;
            Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
            Response.Expires = 0;
            Response.CacheControl = "no-cache";
        }
        catch (Exception ex)
        { 
        }
        finally
        {
            //  Response.Redirect("~/index.htm", false);
            Response.Redirect("~/HomeNew.aspx", false);
        }
    }
}
