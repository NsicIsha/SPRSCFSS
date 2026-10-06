using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Unitregistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            HttpCookie aCookie = new HttpCookie("OSUYULUKR8");
            aCookie.Values["USERID"] = "0";
            aCookie.Values["ROLEID"] = "0";
            aCookie.Values["DEPARTMENTID"] = "0";
            aCookie.Values["USERFULLNAME"] = "Guest";
            aCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(aCookie);

            Session["OSUYULUKR8-ERUYIRE"] = "GP";


            Response.Redirect("NewUnitRegistration.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
}
