using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_CompanyStatusTrack : System.Web.UI.Page
{
    DataTable Mydt;
    clsCompany objCompany;
    int UserId, RoleId, DepartmentId, Edit;
    string UserFullName;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
    }
    protected void btnReference_Click(object sender, EventArgs e)
    {
        try
        {
            Mydt = new DataTable();
            objCompany = new clsCompany();
            DecryptEncrypt objDecr = new DecryptEncrypt();
            string companyid;
            string companyname;
            string branchname;

            Mydt = objCompany.Company_CompanyStatus_Fetch_ByGprefno(txtReference.Text);
            if (Mydt.Rows.Count != 0)
            {
                companyid = objDecr.Encrypt(Mydt.Rows[0]["fGPtbl_Company_Id"].ToString());
                companyname = objDecr.Encrypt(Mydt.Rows[0]["fGPtbl_Company_Name"].ToString());
                branchname = objDecr.Encrypt(Mydt.Rows[0]["Branchname"].ToString());

                Label1.Visible = false;

                HttpCookie aCookie = new HttpCookie("OSUYULUKR8");

                aCookie.Values["ROLEID"] = Convert.ToString(7);// roleid from rolemaster 7
                int roleID =  Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
                if (roleID == 7)
                {
                    aCookie.Values["USERID"] = Convert.ToString(1000);
                    aCookie.Values["DEPARTMENTID"] = Convert.ToString(objDecr.Decrypt(companyid));
                }
                else
                {
                    aCookie.Values["USERID"] = Convert.ToString(0); //username in company   first it is zero 0
                    aCookie.Values["DEPARTMENTID"] = Convert.ToString(0);
                }
                aCookie.Values["USERFULLNAME"] = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();//company name from company
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                Session["OSUYULUKR8-ERUYIRE"] = "GP";
                string ImageId = "ReportViewPage.aspx?Company_ID=" + companyid + "&Com_name=" + companyname + "&BranchName=" + branchname.ToString();
                string tempstring = "<script language=\"JavaScript\">window.open('" + ImageId + "','_blank','menubar=1,width=850,height=500, toolbar=yes,scrollbars=yes,resize=yes');</script>";
                ResponseHelper.Redirect(ImageId, "_blank", "menubar=1,width=850,height=500, toolbar=yes,scrollbars=yes,resize=yes");

            }
            else
            {
                Label1.Text= "No Company found";
                Label1.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
