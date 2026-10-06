using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using NSICGP_VBClassLibrary;


/// <summary>
/// Summary description for clsScheduleMail
/// </summary>
public class clsScheduleMail
{

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
 
    DataSet MyDs;
    DataTable MyDt;

   
    string fromAddress;
    string toAddress;
   
    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate=string.Empty;
    clsMasterMailer objMailer;

	public clsScheduleMail()
	{
		
	}
       

    /* MAIL TO PROVIDE COMPANY LIST WHOSE INSPECTION DATE TO BE ISSUED */
    public void Mail_InspectionDateTobeIssued()
    {
        try
        {
            string CompName = string.Empty, BrName = string.Empty, IAname = string.Empty;
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListInspectionDateToBeIssued", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(MyDt);

            MyDs = new DataSet();
            objMailer = new clsMasterMailer();
            MyDs = objMailer.MasterMailer_Fetch_DataSet(9);

            strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drIA in MyDt.Rows)
                {
                    MailTo = Convert.ToString(drIA["BrEmail"]);
                    BrName = Convert.ToString(drIA["BrName"]);
                    IAname = Convert.ToString(drIA["IAName"]);
                    CompName = Convert.ToString(drIA["CompName"]);
                    strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                    strMailBody = strMailBody.Replace("$$inspagency$$", IAname);
                    strMailBody = strMailBody.Replace("$$compname$$", CompName);
                    if (MailTo != "")
                    {
                        SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                        //INSERT INTO SENT MAILER
                        clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                    }
                }//foreach
            }//if
        }//try
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }

    }

    public static string SendMailToBranch(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {
          
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailfrom);
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));
            message.Bcc.Add(new MailAddress("uttam.kumar@nagsoft.com"));

            message.Subject=mailsubject;
            // Set the body of the mail message
            message.Body =mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            return "1";
            
        }
        catch(Exception ex)
        {
           
            ErrHandler.WriteError(ex.Message);
            return ex.Message;
        }
    }

    /*MAIL TO PROVIDE COMPANY LIST WHOSE MONETARY LIMIT TO BE SET */
    public void Mail_MonetaryLimitTobeSet()
    {
        try
        {
            string BrName=string.Empty, CompName=string.Empty;
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListMonetaryLimitToBeSet", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(MyDt);

            MyDs = new DataSet();
            objMailer = new clsMasterMailer();
            MyDs = objMailer.MasterMailer_Fetch_DataSet(10);

            strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

             if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drIA in MyDt.Rows)
                {
                    MailTo = Convert.ToString(drIA["Bemail"]);
                    BrName = Convert.ToString(drIA["BName"]);
                    CompName = Convert.ToString(drIA["CName"]);
                    strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                    strMailBody = strMailBody.Replace("$$compname$$", CompName);
                    if (MailTo != "")
                    {
                        SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                        //INSERT INTO SENT MAILER
                        clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                    }
                }//foreach
            }//if
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
    }

    /*MAIL TO PROVIDE COMPANY LIST WAITING FOR FINAL APPROVAL */
    public void Mail_MonetaryLimitTobeApproved()
    {
        try
        {
            string BrName = string.Empty, CompName = string.Empty;
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListMonetaryLimitToBeApproved", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(MyDt);
            MyDs = new DataSet();
            objMailer = new clsMasterMailer();
            MyDs = objMailer.MasterMailer_Fetch_DataSet(11);

            strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drIA in MyDt.Rows)
                {
                    MailTo = Convert.ToString(drIA["Bemail"]);
                    BrName = Convert.ToString(drIA["BName"]);
                    CompName = Convert.ToString(drIA["CName"]);
                    strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                    strMailBody = strMailBody.Replace("$$compname$$", CompName);
                    if (MailTo != "")
                    {
                        SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                        //INSERT INTO SENT MAILER
                        clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                    }
                }//foreach
            }//if
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
    }

    /*MAIL FOR RENEWAL/EXPIRED DAYS RANGE BASED ON 
    DAYSVALUE (IN NEGATIVE) TO SEND MAIL ON 7 DAYS, 14 DAYS, 21 DAYS ETC.*/
    public void Mail_RenewalExpiredDateRange(int daysvalue, int comstatus)
    {
        try
        {
            string CompName = string.Empty;

            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListRenewalExpiredDateRange", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@daysvalue", SqlDbType.Int).Value = daysvalue;
            MyAdp.SelectCommand.Parameters.Add("@comstatus", SqlDbType.Int).Value = comstatus;

            MyAdp.Fill(MyDt);

            MyDs = new DataSet();
            objMailer = new clsMasterMailer();
            //FETCH MAILER FOR REGISTERED AND EXPIRED COMPANY
            MyDs =comstatus==1? objMailer.MasterMailer_Fetch_DataSet(12):objMailer.MasterMailer_Fetch_DataSet(13);

            strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drIA in MyDt.Rows)
                {
                    MailTo = Convert.ToString(drIA["ComEmail"]);
                    CompName = Convert.ToString(drIA["ComName"]);
                    
                    strExpiringDate = Convert.ToString(drIA["AddDate"]);
                    strMailBody = strOriginalMailBody.Replace("$$compname$$", CompName);
                    if (strMailBody.Contains("$$daysvalue$$"))
                    {
                        strMailBody = strMailBody.Replace("$$daysvalue$$", strExpiringDate);
                    }
                    if (MailTo != "")
                    {
                        SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                        //INSERT INTO SENT MAILER
                        clsDDL.Insert_MailDetails(MailTo, 7, 3, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                    }
                }//foreach
            }//if
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
    }

    /*MAIL FOR RENEWAL/EXPIRED DAYS RANGE BASED ON 
    DAYSVALUE (IN NEGATIVE TO SEND MAIL TO BRANCH ON 7 DAYS, 14 DAYS, 21 DAYS ETC.*/
    public void Mail_RenewalExpiredDateRange_ForBranch(int daysvalue, int comstatus)
    {
        try
        {
            string CompName = string.Empty;
            string strBranchName = string.Empty;
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListRenewalExpiredDateRange_ForBranch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@daysvalue", SqlDbType.Int).Value = daysvalue;
            MyAdp.SelectCommand.Parameters.Add("@comstatus", SqlDbType.Int).Value = comstatus;

            MyAdp.Fill(MyDt);

            MyDs = new DataSet();
            objMailer = new clsMasterMailer();
            
            //FETCH MAILER FOR REGISTERED AND EXPIRED COMPANY
            //FOR LOCAL
            //MyDs = comstatus == 1 ? objMailer.MasterMailer_Fetch_DataSet(18) : objMailer.MasterMailer_Fetch_DataSet(19);
            //FOR ONLINE
            MyDs = comstatus == 1 ? objMailer.MasterMailer_Fetch_DataSet(14) : objMailer.MasterMailer_Fetch_DataSet(15);

            strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drIA in MyDt.Rows)
                {
                    MailTo = Convert.ToString(drIA["BranchEmail"]);
                    CompName = Convert.ToString(drIA["ComName"]);
                    strBranchName = Convert.ToString(drIA["BranchName"]);
                    strExpiringDate = Convert.ToString(drIA["AddDate"]);
                    strMailBody = strOriginalMailBody.Replace("$$branchname$$", strBranchName);
                    strMailBody = strMailBody.Replace("$$compname$$", CompName);
                    if (strMailBody.Contains("$$daysvalue$$"))
                    {
                        strMailBody = strMailBody.Replace("$$daysvalue$$", strExpiringDate);
                    }
                    if (MailTo != "")
                    {
                        SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                        //INSERT INTO SENT MAILER
                        clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                    }
                }//foreach
            }//if
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
    }


  /*NEED TO RECREATE THIS PROCEDURE  
   * public void Mail_insert()
    {
        try
        {
            //MATCH THE CONCERN PROCEDURE
            MySqlComm = new SqlCommand("testtimer", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
    }*/



}
