using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appCompany_CompanyStatusTrack : System.Web.UI.Page
{
    DataTable Mydt;
    clsCompany objCompany;
    protected void Page_Load(object sender, EventArgs e)
    {
        txtReference.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnReference');");

        if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtReference');", true);
        }

        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "javascript:hide();", true);


        Label1.Visible = false;





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
                aCookie.Values["USERID"] = Convert.ToString(0);//username in company
                aCookie.Values["ROLEID"] = Convert.ToString(0);// roleid from rolemaster 7
                aCookie.Values["DEPARTMENTID"] = Convert.ToString(0);//not required
                aCookie.Values["USERFULLNAME"] = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();//company name from company
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);
                Session["OSUYULUKR8-ERUYIRE"] = "GP";


               // Response.Redirect("../appAdministrator/ReportViewPage.aspx?Company_ID=" + companyid + "&Com_name=" + companyname + "&BranchName=" + branchname, false);
                string ImageId = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + companyid + "&Com_name=" + companyname + "&BranchName=" + branchname.ToString();

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "openwindow", "window.open('" + ImageId + "','','left=100,top=100,width=700px,height=600px,toolbar=yes,scrollbars=yes,status=0');", true);
                ResponseHelper.Redirect(ImageId,"_blank", "menubar=1,width=850,height=500, toolbar=yes,scrollbars=yes,resize=yes");

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
