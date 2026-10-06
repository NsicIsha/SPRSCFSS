using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;

public partial class Scheduler5 : System.Web.UI.Page
{

    clsCompany objCompany;
    DataSet MyDs;
    SqlDataReader MyDr;
    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCompany objCompany = new clsCompany();
        DataSet MyDs = new DataSet();

        MyDs = objCompany.CompaniesWhoseMonetaryLimitIsYetToBeSet_Reminder_sche6();

        SendMail(MyDs);

    }

    public void SendMail(DataSet MailDs)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            for (int rowcount = 0; rowcount < MailDs.Tables[0].Rows.Count; rowcount++)
            {
                MailTo = MailDs.Tables[0].Rows[rowcount]["fGPtbl_Company_HeadOffice_Email"].ToString();

                MailFrom = MailDs.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();

                MailSubject = "Reminder list of companies whose monetary limit is yet to be set";


                var rowresult = from result in MailDs.Tables[0].AsEnumerable()
                                where result.Field<int>("fGPtbl_CUnit_BranchId") == Convert.ToInt32(MailDs.Tables[0].Rows[rowcount]["fGPtbl_CUnit_BranchId"])
                                select new
                                {
                                    fGPtbl_Company_Name = result.Field<string>("fGPtbl_Company_Name"),
                                    fGPtbl_Company_GPReferenceNo = result.Field<string>("fGPtbl_Company_GPReferenceNo"),
                                    Date_of_agency_selected = result.Field<string>("Date_of_agency_selected")
                                };

                StringBuilder sb1 = new StringBuilder();
                //sb1.Append("<table border='1' width='40%'>");
                sb1.Append("<table border='1' width='80%' class=tblborder><th class=rowHeader>Company Name</th><th class=rowHeader>Reference Number</th><th class=rowHeader>Application Date</th>");
                foreach (var field in rowresult)
                {

                    sb1.Append("<tr>");

                    sb1.Append("<td width=30% align=left style=padding-left:4px>");
                    sb1.Append(field.fGPtbl_Company_Name);
                    sb1.Append("</td>");
                    sb1.Append("<td width=25% align=center>");
                    sb1.Append(field.fGPtbl_Company_GPReferenceNo);
                    sb1.Append("</td>");
                    sb1.Append("<td width=25% align=center>");
                    sb1.Append(field.Date_of_agency_selected);
                    sb1.Append("</td>");

                    sb1.Append("</tr>");

                }
                sb1.Append("</table>");

                // for regards===========================


                // ===========================================

                //String strOutput = sb.ToString();

                lblcompany.Text = sb1.ToString();

                //========================================================
                MailTo = "gaurav.srivastava@nagsoft.com";
                //MailTo = "anupam.sen@nagsoft.com";
                MailBody = "This mail is regarding to set monetary limit of companies whose monetary limit is yet to be set. These are list of companies name with their reference number and date on which agency is selected."
                  + lblcompany.Text + "<br/><br/>" + "<b>From</b><br/><b>Admin</b><br /><b>NSIC GP</b><br/>";

                MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = SmtpServerName;

                message.IsBodyHtml = true;
                smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

                smtpClient.Send(message);

                //INSERT INTO SENT MAILER
                clsDDL.Insert_MailDetails(MailTo, 8, 1, MailFrom, 3, 1, MailSubject, MailBody, 3);


            }
        }
        catch (Exception ex)
        {
 
        }
    }
}
