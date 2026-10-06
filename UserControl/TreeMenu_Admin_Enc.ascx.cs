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
using System.IO;
using NSICGP_VBClassLibrary;

public partial class UserControl_TreeMenu_Admin_Enc : System.Web.UI.UserControl
{
    int UserId, RoleId, DepartmentId;
    int ifGPtbl_MU_EncryptedPwdStatus = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

       


        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            // UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            // RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());

            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString().Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString().Replace(" ", "+")).ToString());


            if (HttpContext.Current.Response.Cookies.AllKeys.Contains(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]))
            {
                // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());

                DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString().Replace(" ", "+")).ToString());

            }
            else
            {
                DepartmentId = 0;
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++




        try
        {
            string filepath, filename;
            if (UserId == 346 && RoleId == 8)
            {
                filename = "88".ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            else if (UserId == 347 && RoleId == 4)
            {
                filename = "44".ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            else if (UserId == 348 && RoleId == 3)
            {
                filename = "33".ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            else if (UserId == 362 && RoleId == 8)
            {
                filename = "8".ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            else if (UserId == 363 && RoleId == 8)
            {
                filename = "8".ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            else
            {
                filename = RoleId.ToString() + ".htm";
                filepath = Server.MapPath("../").ToString() + "TreeMenu/" + filename;
            }
            if (File.Exists(filepath) == true)
            {
                StreamReader Sreader;
                Sreader = File.OpenText(filepath);
                string content = Sreader.ReadToEnd();
                //content = content.Replace("href='../", "href='./");
                //content = content.Replace("src='../", "src='../");
                lblmenu.Text = content;
                Sreader.Close();
            }
        }
        catch (Exception ex)
        { }



    }
}
