using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;

public partial class appAdministrator_Sendmail : System.Web.UI.Page
{
    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string sub;

    protected void Page_Load(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        //Showdata(Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["id"])));
        /*lblfrom.Text =objDecEnc.Decrypt(Request.QueryString["Branchmail"].Replace(" ", "+")).ToString();
        lblto.Text = objDecEnc.Decrypt(Request.QueryString["Compemail"].Replace(" ", "+")).ToString();*/
        if (!IsPostBack)
        {
            txtfrom.Text = objDecEnc.Decrypt(Request.QueryString["Branchmail"].Replace(" ", "+")).ToString();
            txtTo.Text = objDecEnc.Decrypt(Request.QueryString["Compemail"].Replace(" ", "+")).ToString();
        }
  
    }
    protected void btnsendmail_Click(object sender, EventArgs e)
    {
        sub = txtSub.Text;
        SendMail();
        //SendMail.Mail_WithOUTAttachment(lblto.Text, 0, 0, lblfrom.Text, 0, 0, "", "", sub, txtbody.Text, "", 0, "", "", "");
        //SendMail.Mail_WithOUTAttachment(  emailid, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
       // SendMail.Mail_WithOUTAttachment(lblto.Text, 0, 0, lblfrom.Text, 0, 0, "", "", sub.ToString(), txtbody.Text, "", 0, "", "", "");
    }


    public void SendMail()
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];

            /*MailTo = lblto.Text.ToString();
            MailFrom = lblfrom.Text.ToString();*/
            MailTo = txtTo.Text.ToString();
            MailFrom = txtfrom.Text.ToString();
           // string Bodyhtml = txtSub.Text;

            MailSubject = txtSub.Text;

            MailBody =((txtbody.Text.Replace(Environment.NewLine, "<br />")));
            
            MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);

            //INSERT INTO SENT MAILER (MAY BE SOME INTEGER VALUE ARE WRONG BUT MAIL BODY COULD GUIDE)
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 3, 1, MailSubject, MailBody, 3);

            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Send SuccessFully....'); window.opener.location.reload(true); window.close();</script>");
        }
        catch (Exception ex)
        {
           // ex.Message = "Server Busy.....................";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Does Not Send SuccessFully. Because Server Busy ..........'); window.opener.location.reload(true); window.close();</script>");
        }
 

    }

}
