using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_CompanyLogin : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int successId;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmitFGP_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable hashtbl = new Hashtable();
            objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            
            string FromMail, ccMail, bccMail, subject, bodyMail;

            hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text,"none");

            //TO FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(6);

            successId = Convert.ToInt32(hashtbl["Output"]);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString() + hashtbl["pwd"].ToString()+".";

            switch (successId)
            {
                case 1://COMPANY NOT EXIST
                    lblMsgFGP.Visible = true;
                    lblMsgFGP.Text = "User not exist";
                    txtUserNameFGP.Text = "";

                    ModalPopupExtender1.Show();
                    break;
                case 2://Password successfully changed
                   // SendMail.Mail_WithOUTAttachment(hashtbl["Email"].ToString(), 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
                    lblMessage.Visible = true;
                    lblMessage.Text = "Password sent to your email";
                    break;
                case 3://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later";
                    break;
                case 4://User is inactive
                    lblMsgFGP.Visible = true;
                    lblMsgFGP.Text = "This user account is DEACTIVATED !";
                    txtUserNameFGP.Text = "";

                    ModalPopupExtender1.Show();
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            Hashtable hashtblLogin = new Hashtable();
            hashtblLogin = objCompany.Company_ForgotPassword(txtUserName.Text, txtPassword.Text);

            successId = Convert.ToInt32(hashtblLogin["Output"]);

            switch (successId)
            {
                case 1://COMPANY NOT EXIST
                    lblMessage.Visible = true;
                    lblMessage.Text = "User not exist!";
                    txtUserName.Text = "";
                    txtUserName.Focus();
                    txtPassword.Text = "";
                    break;
                case 3://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later!";
                    break;
                case 4://User is inactive
                    lblMessage.Visible = true;
                    lblMessage.Text = "This user account is DEACTIVATED!";
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    break;
                case 5://PASSWORD MATCHED
                    //TO SEND TO HOME PAGE
                    HttpCookie aCookie = new HttpCookie("OSUYULUKR8");
                    aCookie.Values["USERID"] = Convert.ToString(txtUserName.Text);//username in company
                    aCookie.Values["ROLEID"] = Convert.ToString(7);// roleid from rolemaster
                    aCookie.Values["DEPARTMENTID"] = Convert.ToString(0);//not required
                    aCookie.Values["USERFULLNAME"] = hashtblLogin["compname"].ToString();//company name from company
                    aCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(aCookie);
                    Session["OSUYULUKR8-ERUYIRE"] = "GP";
                    break;
                case 6://PASSWORD MISMATCHED
                    lblMessage.Visible = true;
                    lblMessage.Text = "Invalid User Name or Password!";
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtUserName.Focus();
                    break;
                default://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later!";
                    break;

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


}
