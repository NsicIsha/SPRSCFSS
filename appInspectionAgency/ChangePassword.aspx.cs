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
using NSICGP_VBClassLibrary;

public partial class appInspectionAgency_ChangePassword : System.Web.UI.Page
{
    clsMasterInspectionAgency objInspectionAgency;
    int SuccessId;
    int USERID, ROLEID, AGENCYID, BRANCHID,DepartmentId;
    string USERFULLNAME, BRANCHNAME;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            AGENCYID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["AGENCYID"]).ToString());
            BRANCHID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BRANCHNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHNAME"]).ToString();
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            AGENCYID = 0;
            BRANCHID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        txtNewPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        txtOldPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        txtConfirmPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

        if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtOldPassword');", true);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       objInspectionAgency = new clsMasterInspectionAgency();

        try
        {
       SuccessId = objInspectionAgency.InspectionAgency_ChangePassword(USERID, txtNewPassword.Text.Trim() , txtOldPassword.Text.Trim(), USERID);
           
            switch (SuccessId)
            {
                case 1://password mismatch
                    lblMsg.Text = "Password provided by you is not correct !";
                    break;
                case 2://Successfully changed
                    lblMsg.Text = "Password successfully changed !";
                    break;
                case 3://Error in operation
                    lblMsg.Text = "Server is busy, please try later !";
                    break;

                default: lblMsg.Text = "Server is busy, please try later !";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
       
    

