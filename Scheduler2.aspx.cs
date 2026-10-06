using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Scheduler2 : System.Web.UI.Page
{

    clsCompany objCompany;
    DataTable MyDt;
    DataSet MyDs;
    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCompany obj = new clsCompany();
        DataSet Myds = new DataSet();
        Myds = obj.NextRrenewalDateNearsUp_Reminder_sche2();

        SendMail(Myds);
    }

    public void SendMail(DataSet MailDs)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            //MailTo = lblto.Text.ToString();
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];

            for (int rowcount = 0; rowcount < MailDs.Tables[0].Rows.Count; rowcount++)
            {
                MailTo = MailDs.Tables[0].Rows[rowcount]["To"].ToString();

                MailFrom = MailDs.Tables[0].Rows[rowcount]["fGPtbl_MU_Email"].ToString();
                // string Bodyhtml = txtSub.Text;

                //MailSubject = txtSub.Text;
                //MailBody = ((txtbody.Text.Replace(Environment.NewLine, "<br />")));

                clsCompany objCompany = new clsCompany();
                MyDs = objCompany.NextRrenewalDateNearsUp_Reminder_sche2();
                //DataTable MyDt = new DataTable();
                //MyDt = MyDs.Tables[0];

                //lbl_regDate.Text = MyDs.Tables[0].Rows[0]["Registration_Date"].ToString();

                lbl_regDate.Text = MyDs.Tables[0].Rows[0]["Registration_Date"].ToString();


                MailTo = "anupam.sen@nagsoft.com";
                MailSubject = "Reminder that your's company next renewal date of NSIC SPR scheme nears up";
                //MailBody = " This mail is regarding to next renewal date nears up.";
                //MailBody = " This mail is regarding to your company that your registration on NSIC GP will expires soon. Your company is registered under NSIC GP on " + lbl_regDate.Text + " (YY/MM/DD)." + "Your registration is valied upto two years from the date of registration."+ "<br/><br/><br/><br/><b>From</b><br/><b>Branch Head</b><br /><b>NSIC GP</b><br/>";
                MailBody = "Dear Sir/Mam, <br/><br/>This is a reminder that your companies renewal date for NSIC SPR scheme nears up. Details are given below :- <br/><br/><b>Reference Number</b> - " + MyDs.Tables[0].Rows[rowcount]["fGPtbl_Company_GPReferenceNo"].ToString() + "<br/><b>Registration Number - </b>" + MyDs.Tables[0].Rows[rowcount]["fGPtbl_CUnit_RegistrationNo"].ToString() + "<br/><b>Company Name</b> - " + MyDs.Tables[0].Rows[rowcount]["fGPtbl_Company_Name"].ToString() + "<br/><b>Head Office Address</b> - " + MyDs.Tables[0].Rows[rowcount]["Company head office address"].ToString() + "<br/><b>Registration Date</b> -" + MyDs.Tables[0].Rows[rowcount]["Registration_Date"].ToString() + "<br/><b>Renewal Date</b> -" + MyDs.Tables[0].Rows[rowcount]["Renewal_Date"].ToString() + "<br/><br/><br/><b>From</b><br/><b>Branch Head</b><br /><b>NSIC GP</b><br/>";

                MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = SmtpServerName;

                message.IsBodyHtml = true;
                smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

                smtpClient.Send(message);

                //INSERT INTO SENT MAILER
                clsDDL.Insert_MailDetails(MailTo, 7, 3, MailFrom, 3, 1, MailSubject, MailBody, 3);

            }
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Send SuccessFully....'); window.opener.location.reload(true); window.close();</script>");
        }
        catch (Exception ex)
        {
            // ex.Message = "Server Busy.....................";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Does Not Send SuccessFully. Because Server Busy ..........'); window.opener.location.reload(true); window.close();</script>");
        }
    }
}
