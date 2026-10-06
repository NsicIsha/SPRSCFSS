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
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Net.Mail;

public partial class appAdministrator_HomeEnc : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    string fromAddress;
    string toAddress;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;

    string strRegistrationNumber = string.Empty;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;

    DataSet MyDs;
    DataTable MyDt;


    clsCompany objCompany;
    DataSet MyDs_Status;
    int ifGPtbl_MU_EncryptedPwdStatus = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        ifGPtbl_MU_EncryptedPwdStatus = Convert.ToInt32(Session["ifGPtbl_MU_EncryptedPwdStatus"]);


        Response.Write(ifGPtbl_MU_EncryptedPwdStatus.ToString());
        if (ifGPtbl_MU_EncryptedPwdStatus == 0)
        {
            Response.Redirect("ChangePwdMandatory.aspx", false);
        }
        //else
        //{
        //    Response.Redirect("./Home.aspx", false);
        //}



        clsCompany objCompany = new clsCompany();
        DataSet MyDs = new DataSet();
        objCompany.StatusChangeOfCompany_whoseRegExp();

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (Session["updateProfile"] != null)
            {
                if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertTrue('" + Session["updateProfile"].ToString() + "');", true);
                }
            }
            if (RoleId == 1)
            {
                tr_delete.Visible = true;
                lbl_delete_company.Text = GetDeleteRequest(RoleId);
                lbl_delete_company.Visible = true;
                tr_print_request.Visible = true;
                lbl_print_request.Text = GetPrintRequest();
                lbl_print_request.Visible = true;
            }
            if (RoleId == 2)
            {
                tr_delete.Visible = true;
                lbl_delete_company.Text = GetDeleteRequest(RoleId);
                lbl_delete_company.Visible = true;
            }
            if (RoleId == 5)
            {
                tr_delete.Visible = true;
                lbl_delete_company.Text = GetDeleteRequest(RoleId);
                lbl_delete_company.Visible = true;
            }
            if (RoleId == 3)
            {
                tr_delete.Visible = true;
                lbl_delete_company.Text = GetDeleteRequest(RoleId);
                lbl_delete_company.Visible = true;
            }


            if (RoleId == 8)
            {
                // AutoMailor to Units For Renewal
                int Branchid, ChkStatus, ChkStatus_GPPendingCase;
                clsTrans_UserBranch objtub = new clsTrans_UserBranch();
                Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                DataSet ds = new DataSet();
                ds = CompanyRenewal_EmailStatus_Branch(Branchid);

                ChkStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["CheckStatus"].ToString());

                //Email for Underprocess_GP_Case
                GetCompanyId_ForBranch_Underprocess_GP_Case(Branchid);



                if (ChkStatus == 240)
                {
                    SendEmail(Branchid, 30);
                    SendEmail(Branchid, 15);
                    SendEmail(Branchid, 7);
                }
            }

        }


        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        finally
        {
            Session["updateProfile"] = null;
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }

    #region Autoemail to Unit for Renewal
    public static string SendMailToBranch(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailfrom, "Support-SinglePointRegistration");
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));
          //  message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            return "1";

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
            return ex.Message;
        }
    }


    public DataTable Company_Fetch_Certificate_Search_OneMonth(int branchid, int ddlcriteria, string searchcriteria, int SearchDuration)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_OneMonth", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;
            MyAdp.SelectCommand.Parameters.Add("@MounthCount", SqlDbType.Int).Value = SearchDuration;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public DataSet CompanyRenewal_EmailStatus_Branch(int branchid)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_EmailStatus_Branch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }


    public DataSet CompanyGPPendingCase_EmailStatus_Branch(int branchid, int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CompanyGPPendingCase_EmailStatus_Branch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }


    public void SendEmail(int BranchId, int Days)
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();
        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(16);

        string CompName = string.Empty, BrName = string.Empty, ExpiryDate = string.Empty, NumberOfDays = string.Empty;
        MyDt = new DataTable();
        MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_OneMonth_Mailar", DBManager.ConnectToDB());
        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        MyAdp.SelectCommand.CommandTimeout = 50000;
        MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
        MyAdp.SelectCommand.Parameters.Add("@Days", SqlDbType.Int).Value = Days;
        MyAdp.Fill(MyDt);

        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(drIA["fGPtbl_Company_HeadOffice_Email"]);
                //BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                CompName = Convert.ToString(drIA["fGPtbl_Company_Name"]);
                ExpiryDate = Convert.ToString(drIA["ExpiryDate_Registration_Date_Minus_One"]);
                strRegistrationNumber = Convert.ToString(drIA["fGPtbl_Company_GPReferenceNo"]);
                NumberOfDays = Convert.ToString(drIA["Validity_gap"]);
                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", CompName);
                strMailBody = strMailBody.Replace("$$ExpiryDate$$", ExpiryDate);
                if (NumberOfDays == "0 Days".ToString().Trim())
                {
                    NumberOfDays = "Today, 0 Day";
                }
                else
                {
                    NumberOfDays = NumberOfDays.ToString();
                }
                strMailBody = strMailBody.Replace("$$numberofdays$$", NumberOfDays);
                strMailBody = strMailBody.Replace("$$REGNUMBER$$", strRegistrationNumber);
                if (MailTo != "")
                {
                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                    //INSERT INTO SENT MAILER
                    clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                }
            }//foreach
        }//if
    }
    #endregion
    public string GetDeleteRequest(int RoleID)
    {
        string Count = "0";
        DataTable dt_DeleteRequest = new DataTable();
        SqlDataAdapter MyADp;
        try
        {


            MyADp = new SqlDataAdapter("GP_SP_Get_DeleteCompaniesList", DBManager.ConnectToDB());
            MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (RoleID != 0) MyADp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;

            MyADp.Fill(dt_DeleteRequest);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();

        }

        if (dt_DeleteRequest.Rows.Count > 0)
        {
            Count = Convert.ToString(dt_DeleteRequest.Rows[0]["Count1"].ToString());
        }
        else
        {
            Count = "0";
            dt_DeleteRequest.Dispose();
        }
        return Count;
    }

    // Function For Print Request Count 
    public string GetPrintRequest()
    {
        string Count = "0";
        DataTable dt_DeleteRequest = new DataTable();
        SqlDataAdapter MyADp;
        try
        {
            MyADp = new SqlDataAdapter("GP_Sp_Fetch_Print_request_Count", DBManager.ConnectToDB());
            MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyADp.Fill(dt_DeleteRequest);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
        }

        if (dt_DeleteRequest.Rows.Count > 0)
        {
            Count = Convert.ToString(dt_DeleteRequest.Rows[0]["Countprint"].ToString());
        }
        else
        {
            Count = "0";
            dt_DeleteRequest.Dispose();
        }
        return Count;
    }



    // Automailor for underprocess GP case

    #region Autoemail to Unit for Renewal
    public static string SendMailToBranch_Underprocess_GP_Case(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailMessage message = new MailMessage();
            // message.From = new MailAddress(mailfrom);
            message.From = new MailAddress(mailfrom, "Support-SinglePointRegistration");
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));

            //message.Bcc.Add(new MailAddress("ctms@nsic.co.in"));

          //  message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));




            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
            smtpClient.EnableSsl = false;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            return "1";

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
            return ex.Message;
        }
    }

    public void GetCompanyId_ForBranch_Underprocess_GP_Case(int BranchId)
    {
        int CompId;
        int ChkStatus_GPPendingCase;
        MyDt = new DataTable();
        MyAdp = new SqlDataAdapter("GPsp_Company_fetch_Company_Details_Underprocess_GP_Case_Mailar", DBManager.ConnectToDB());
        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        MyAdp.SelectCommand.CommandTimeout = 50000;
        MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
        MyAdp.Fill(MyDt);
        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                CompId = Convert.ToInt32(drIA["fGPtbl_Company_Id"]);
                DataSet dsGPPendingCase = new DataSet();
                dsGPPendingCase = CompanyGPPendingCase_EmailStatus_Branch(BranchId, CompId);
                ChkStatus_GPPendingCase = Convert.ToInt32(dsGPPendingCase.Tables[0].Rows[0]["CheckStatus"].ToString());
                if (ChkStatus_GPPendingCase == 240)
                {
                    SendEmail_Underprocess_GP_Case(CompId, BranchId);
                }
            }
        }
    }
    public void SendEmail_Underprocess_GP_Case(int CompId, int BranchId)
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();
        Hashtable hashtbl = new Hashtable();
        int successId;
        int CompanyId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(21);

        string CompName = string.Empty, BrName = string.Empty, ExpiryDate = string.Empty, NumberOfDays = string.Empty;

        string strBranchName = string.Empty, strAddress = string.Empty, strMobile = string.Empty, strPhone = string.Empty, strEmail = string.Empty;
        string strAgeing = string.Empty;
        MyDt = new DataTable();
        MyAdp = new SqlDataAdapter("GPsp_Company_fetch_Company_Details_Underprocess_GP_Case_Mailar_CompId", DBManager.ConnectToDB());
        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        MyAdp.SelectCommand.CommandTimeout = 50000;
        MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
        MyAdp.SelectCommand.Parameters.Add("@CompId", SqlDbType.Int).Value = CompId;
        MyAdp.Fill(MyDt);
        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(drIA["fGtbl_MB_Email"]); //Mail will go to Branch
                //BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                CompName = Convert.ToString(drIA["fGPtbl_Company_Name"]);
                CompId = Convert.ToInt32(drIA["fGPtbl_Company_Id"]);
                strBranchName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                strAddress = Convert.ToString(drIA["CompanyUnitAddress"]);
                strMobile = Convert.ToString(drIA["fGPtbl_Company_HeadOffice_Mobile"]);
                strPhone = Convert.ToString(drIA["fGPtbl_Company_HeadOffice_Phone"]);
                strEmail = Convert.ToString(drIA["fGPtbl_Company_HeadOffice_Email"]);
                strAgeing = Convert.ToString(drIA["Age_From_Inspection_Alloted"]);
                //strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", CompName);
                //strMailBody = strMailBody.Replace("$$ExpiryDate$$", ExpiryDate);
                strMailBody = strOriginalMailBody.Replace("$$BranchName$$", strBranchName);
                strMailBody = strMailBody.Replace("$$NameofUnit$$", CompName);
                strMailBody = strMailBody.Replace("$$Address$$", strAddress);
                strMailBody = strMailBody.Replace("$$Mobile$$", strMobile);
                strMailBody = strMailBody.Replace("$$Phone$$", strPhone);
                strMailBody = strMailBody.Replace("$$Email$$", strEmail);
                strMailBody = strMailBody.Replace("$$Ageing$$", strAgeing);
                if (MailTo != "")
                {
                    SendMailToBranch_Underprocess_GP_Case(MailTo, strMailBody, MailFrom, MailSubject);
                    Insert_MailDetails_CompanyGPPendingCase_EmailStatus_Branch(CompId, BranchId, 1);
                    //INSERT INTO SENT MAILER
                    clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strOriginalMailBody, 1);
                }
            }//foreach
        }//if
    }
    #endregion



    public static void Insert_MailDetails_CompanyGPPendingCase_EmailStatus_Branch(int CompanyId, int BranchId, int EmailSentStatus)
    {
        SqlCommand MyCmd = new SqlCommand("GPSp_Insert_CompanyGPPendingCase_EmailStatus_Branch", DBManager.ConnectToDB());
        try
        {
            MyCmd.CommandType = CommandType.StoredProcedure;

            MyCmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyCmd.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyCmd.Parameters.Add("@EmailSentStatus", SqlDbType.Int).Value = EmailSentStatus;
            MyCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyCmd.Dispose();
            DBManager.CloseConnection();
        }
    }
}