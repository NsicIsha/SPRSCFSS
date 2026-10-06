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
//using System.Web. HttpUtilites.HtmlEncode;
using NSICGP_VBClassLibrary;

public partial class UserControl_Header_Admin : System.Web.UI.UserControl
{
    string UserFullName;
    DecryptEncrypt objDecEnc = new DecryptEncrypt();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //  if (Request.Cookies["OSUYULUKR8"] != null)
        //  {
        try
        {


            if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString() == "0")
            {
                UserFullName = "";
            }
            else
            {
                //  UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
                UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            }

            //if (Request.Cookies["OSUYULUKR8"]["USERID"].ToString() == "0")
            //    {
            //        UserFullName = "";
            //    }
            //    else
            //    {
            //        UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //    }

        }
        catch (Exception ex)
        {
            UserFullName = "";
            //Response.Redirect("../../appAdministrator/Logout.aspx");
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        try
        {
            if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString() == "0")
                lblLink.Visible = false;
            else
                lblLink.Visible = true;
        }
        catch (Exception ex)
        {
        }

        //try
        //{
        //    if (Request.Cookies["OSUYULUKR8"]["USERID"].ToString() == "0")
        //        lblLink.Visible = false;
        //    else
        //        lblLink.Visible = true;
        //}
        //catch (Exception ex)
        //{
        //}

        lblUser.Text = "Welcome <b><i>" + UserFullName.ToUpper() + "</i></b>";

         try
         {
            if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "7")
                lblLink.Text = Server.HtmlDecode("<a href='../appCompany/CompanyHome.aspx'>Home</a>&nbsp;|&nbsp;<a href='../appCompany/CompanyChangePassword.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../appCompany/CompanyLogout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            else if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "3" || objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "4" || objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "8" || objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "5")
                lblLink.Text = Server.HtmlDecode("<a href='./Home.aspx'>Home</a>&nbsp;|&nbsp; <a href='./Branch_Change_Address.aspx'>Change Branch Address </a>&nbsp;|&nbsp; <a href='./UpdateProfile.aspx'>Update Profile</a>&nbsp;|&nbsp;<a href='./ChangePwd.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='./Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
           
            
            else if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "10")
            {
                lblLink.Text = Server.HtmlDecode("<a href='./Home.aspx'>Home</a>&nbsp;|&nbsp;<a href='./ChangePassword.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='./Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            }

            else if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "17")
            {
                lblLink.Text = Server.HtmlDecode("<a href='./ChangePwd.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../../appAdministrator/Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            }

            else if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "18")
            {
                lblLink.Text = Server.HtmlDecode("<a href='./ChangePwd.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../../appAdministrator/Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            }

            else if (objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString() == "19")
            {
                lblLink.Text = Server.HtmlDecode("<a href='./ChangePwd.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='../../appAdministrator/Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
            }

            else
                lblLink.Text = Server.HtmlDecode("<a href='./Home.aspx'>Home</a>&nbsp;|&nbsp;<a href='./UpdateProfile.aspx'>Update Profile</a>&nbsp;|&nbsp;<a href='./ChangePwd.aspx'>Change Password</a>&nbsp;|&nbsp;<a href='./Logout.aspx' onclick=&quot;return confirm('Are you sure you want to leave');&quot;>Logout</a>");
        }
        catch { }
        //}
        //else
        //  {
        //  lblUser.Text = "Welcome <b><i>" + " GUEST"+ "</i></b>";
        // }
    }

}
