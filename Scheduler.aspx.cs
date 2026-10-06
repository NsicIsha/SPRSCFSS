using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

public partial class Scheduler : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet MyDs;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCompany obj=new clsCompany();
        DataSet Myds = new DataSet();
        Myds = obj.InspectionAgncy_Reminde_sche1();

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
                MailTo = MailDs.Tables[0].Rows[rowcount]["Agency_Email"].ToString();

                MailFrom = MailDs.Tables[0].Rows[rowcount]["Branch_Email"].ToString();
                // string Bodyhtml = txtSub.Text;

                //MailSubject = txtSub.Text;
                //MailBody = ((txtbody.Text.Replace(Environment.NewLine, "<br />")));
                //Inspection Date already Issued but Inspection is Pending

                clsCompany objCompany = new clsCompany();
                MyDs = objCompany.InspectionAgncy_Reminde_sche1();
                DataTable dt = new DataTable(); 
                dt = MyDs.Tables[0];

                lbl_InspectionDate.Text = MyDs.Tables[0].Rows[rowcount]["InspectionDate"].ToString();
                lbl_companyName.Text = MyDs.Tables[0].Rows[rowcount]["CompanyName"].ToString();

                MailTo = "anupam.sen@nagsoft.com";
                MailSubject = "Reminder for pending inspection";
               // MailBody = " This mail is regarding to your agency that company  " + lbl_companyName.Text + " inspection date is already issued on " + lbl_InspectionDate.Text +  " but inspection is still pending." + "<br/><br/><br/><b>From</b><br/><b>Branch Head</b><br /><b>NSIC GP</b><br/>";
                MailBody = "Dear Sir/Mam, <br/><br/>This is a reminder email for pending inspection on your part. Detail are given below :- <br/><br/><b>Reference Number</b> - " + MyDs.Tables[0].Rows[rowcount]["Referenceno"].ToString() + "<br/><b>Company Name</b> - " + lbl_companyName.Text + "<br/><b>Head Office Address</b> - " + MyDs.Tables[0].Rows[rowcount]["Headofficeaddress"].ToString() + "<br/><b>Factory Address</b> -" + MyDs.Tables[0].Rows[rowcount]["Factoryaddress"].ToString() + "<br/><b>Factory Phone</b> -" + MyDs.Tables[0].Rows[rowcount]["Factoryphone"].ToString() + "<br/><b>Factory Email</b> -" + MyDs.Tables[0].Rows[rowcount]["Factoryemail"].ToString() + "<br/><b>Inspection Date</b> - " + lbl_InspectionDate.Text + "<br/><br/><br/><b>From</b><br/><b>Branch Head</b><br /><b>NSIC GP</b><br/>";


                MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = SmtpServerName;

                message.IsBodyHtml = true;
                smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

                smtpClient.Send(message);

                //TO INSERT MAIL TO SENTMAILER
                clsDDL.Insert_MailDetails(MailTo, 6, 2, MailFrom, 3, 1, MailSubject, MailBody, 3);

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
