using System;

public partial class appMaster_Admin : System.Web.UI.MasterPage
{
    int ifGPtbl_MU_EncryptedPwdStatus = 0;
    protected void Page_Load(object sender, EventArgs e)
    {


        //ifGPtbl_MU_EncryptedPwdStatus = Convert.ToInt32(Session["ifGPtbl_MU_EncryptedPwdStatus"]);

        //if (ifGPtbl_MU_EncryptedPwdStatus == 0)
        //{
        //    Response.Redirect("ChangePwd.aspx", false);
        //   // lblmenu.Visible = false;
        //}
        //else
        //{
        //   // lblmenu.Visible = true;
        //}


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
