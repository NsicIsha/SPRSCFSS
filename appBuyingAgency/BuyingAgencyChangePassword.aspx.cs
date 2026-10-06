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

public partial class appBuyingAgency_BuyingAgencyChangePassword : System.Web.UI.Page
{
    clsMasterInspectionAgency objagency;
    int UserId, RoleId;
    string UserFullName;
    int successId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());

            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;

            UserFullName = "";
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
        objagency = new clsMasterInspectionAgency();
        try
        {
            successId = objagency.MasterBuyingAgency_ChangePassword(UserId, txtNewPassword.Text, txtOldPassword.Text, UserId);
            switch (successId)
            {
                case 1://password mismatch
                    lblMsg.Text = "Password provided by you is not correct !";
                    break;
                case 2://Successfully changed
                    lblMsg.Text = "Password successfully changed !";
                    Changepasswordsendmail();
                    break;
                case 3://Error in operation
                    lblMsg.Text = "Server is busy, please try later !";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnback_Click(object sender, EventArgs e)
    {
        Response.Redirect("BuyingAgencySearch.aspx", false);
    }

    public void Changepasswordsendmail()
    {
        try
        {
            clsMasterMailer objMailer = new clsMasterMailer();
            DataSet Myds = new DataSet();
            int id;
            string FromMail, ccMail, bccMail, subject, bodyMail;
            ArrayList arl = new ArrayList();

            //TO FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(9);
            //MasterBuyingAgenciesUser_ForgotPassword
            objagency = new clsMasterInspectionAgency();
            //arl = objagency.MasterBuyingAgenciesUser_ForgotPassword(UserFullName.ToString().Trim());
            DataSet ds = new DataSet();
            ds = objagency.MasterBuyingAgency_ChangePassword_send_mail(UserId);
            //  id = Convert.ToInt32(arl[0]);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";
            if (ds.Tables[0].Rows.Count > 0)
            {
                string bodystr = bodyMail.Replace("$$password$$", ds.Tables[0].Rows[0]["password"].ToString());
                lblMsg.Visible = true;
                lblMsg.Text = "Password sent to your email";
                SendMail.Mail_WithOUTAttachment(ds.Tables[0].Rows[0]["EmailAddress"].ToString(), 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
                ds.Dispose();
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "Server Busy";
                ds.Dispose();
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
