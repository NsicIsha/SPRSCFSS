using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appCompany_CompanyChangePassword : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        txtUsername.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        txtNewPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        txtOldPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        txtConfirmPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

        if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtUsername');", true);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsCompany objCompany = new clsCompany();

        try
        {
            int successId = objCompany.Company_ChangePassword(txtNewPassword.Text.Trim() , txtOldPassword.Text.Trim(), txtUsername.Text.Trim());

            switch (successId)
            {
                case 1://password mismatch
                    lblMsg.Text = "Password provided by you is not correct!";
                    break;
                case 2://Successfully changed
                    lblMsg.Text = "Password successfully changed!";
                    //Changepasswordsendmail();
                    break;
                case 3://Error in operation
                    lblMsg.Text = "Server is busy, please try later!";
                    break;
                default: lblMsg.Text = "Server is busy, please try later!";
                    break;
            }
            txtUsername.Text = "";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

   /* public void Changepasswordsendmail()
    {
        try
        {
            objUser = new clsMasterUser();
            objMailer = new clsMasterMailer();
            Myds = new DataSet();
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
            // arl = objUser.MasterUser_ForgotPassword(txtUserNameFGP.Text);
            //  id = Convert.ToInt32(arl[0]);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";
            if (ds.Tables[0].Rows.Count > 0)
            {
                string bodystr = bodyMail.Replace("$$password$$", ds.Tables[0].Rows[0]["password"].ToString());
                lblMessage.Visible = true;w
                lblMessage.Text = "Password sent to your email";
                SendMail.Mail_WithOUTAttachment(ds.Tables[0].Rows[0]["EmailAddress"].ToString(), 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
                ds.Dispose();
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Server Busy";
                ds.Dispose();
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    */ 
    }