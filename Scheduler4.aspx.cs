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

public partial class Scheduler4 : System.Web.UI.Page
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

        MyDs = objCompany.SentdateOfInspectionNotIssued_sche4();
        //Fill_CompanyDetails();
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
                MailTo = MailDs.Tables[1].Rows[rowcount]["Email"].ToString();

                MailFrom = MailDs.Tables[2].Rows[0]["fGPtbl_MU_Email"].ToString();

                MailSubject = "Reminder of pending inspection";
                
                //=====================================================

                var rowresult=from result in MailDs.Tables[0].AsEnumerable()
                          where result.Field<int>("fGPtbl_CUnit_BranchId")==Convert.ToInt32(MailDs.Tables[1].Rows[rowcount]["fGPtbl_CUnit_BranchId"])
                           select new
                          {
                              CompanyName=result.Field<string>("Comapany Name"),
                              CompanyReferenceno=result.Field<string>("Company Reference Number"),
                             // RegistrationDate=result.Field<string>("Registration date"),
                              InspectionDateIssued=result.Field<string>("Inspection date Issued")
                          };

                StringBuilder sb1 = new StringBuilder();
                //sb1.Append("<table border='1' width='40%'>");
                sb1.Append("<table border='1' width='80%' class=tblborder><th class=rowHeader>Company Name</th><th class=rowHeader>Reference Number</th><th class=rowHeader>Date on which company accepted by branch</th>");
                foreach (var field in rowresult)
                {
                    
                    sb1.Append("<tr>");

                    sb1.Append("<td width=30% align=left style=padding-left:4px>");
                    sb1.Append(field.CompanyName);
                    sb1.Append("</td>");
                    sb1.Append("<td width=25% align=center>");
                    sb1.Append(field.CompanyReferenceno);
                    sb1.Append("</td>");
                   /* sb1.Append("<td width=20% align=left>");
                    sb1.Append(field.RegistrationDate);
                    sb1.Append("</td>");*/

                    sb1.Append("<td width=25% align=center>");
                    sb1.Append(field.InspectionDateIssued);
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
                MailBody = "This mail is regarding to issue inspection date for the companies whose form has been accepted but their inspection dates yet to be issued. These are list of companies name with their reference number and date on which they are accepted." 
                  + lblcompany.Text + "<br/>" + "<b>From</b><br/><b>Admin</b><br /><b>NSIC GP</b><br/>";
                   
                MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = SmtpServerName;

                message.IsBodyHtml = true;
                smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
                smtpClient.UseDefaultCredentials = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

                smtpClient.Send(message);

                //INSERT INTO SENT MAILER
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

    public void Fill_CompanyDetails()
    {
        try
        {
           /* clsCompany objCompany = new clsCompany();
            //MyDr = objCompany.SentdateOfInspectionNotIssued_sche4();
            MyDs = objCompany.SentdateOfInspectionNotIssued_sche4();

            DataTable dt = new DataTable();
            dt = MyDs.Tables[0];
            int RowCount = dt.Rows.Count;

            int ColCount = dt.Columns.Count;

            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='1' width='40%'>");
            for (int i = 0; i < RowCount; i++)
            {
                sb.Append("<tr>");

                for (int k = 0; k < ColCount; k++)
                {
                    sb.Append("<td width=20% align=left>");
                    sb.Append(dt.Rows[i][k].ToString());
                    sb.Append("</td>");
                }


                DataRowVersion.Current.ToString();

                sb.Append("</tr>");


            }
            sb.Append("</table>");

            String strOutput = sb.ToString();

            lblcompany.Text = strOutput;*/

            


        }
        catch (Exception ex)
        {
        }

    }

    


}
