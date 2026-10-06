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



using System.Net;

public partial class appAdministrator_Home : System.Web.UI.Page
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


    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["ifGPtbl_MU_EncryptedPwdStatus"] != null)
        {
            ifGPtbl_MU_EncryptedPwdStatus = Convert.ToInt32(Session["ifGPtbl_MU_EncryptedPwdStatus"]);
            if (ifGPtbl_MU_EncryptedPwdStatus == 0)
            {
                Response.Redirect("ChangePwdMandatory.aspx", false);
            }
        }

        //    if (Session["ifGPtbl_MU_EncryptedPwdStatus"] != null &&
        //(Session["ifGPtbl_MU_EncryptedPwdStatus"].ToString() == "0" ||
        // Session["ifGPtbl_MU_EncryptedPwdStatus"].ToString() == "1"))
        //    {
        //        int pwdStatus = Convert.ToInt32(Session["ifGPtbl_MU_EncryptedPwdStatus"]);
        //    }

        //    if (ifGPtbl_MU_EncryptedPwdStatus == 0)
        //            {
        //               Response.Redirect("ChangePwdMandatory.aspx", false);
        //            }


        clsCompany objCompany = new clsCompany();
        DataSet MyDs = new DataSet();
        objCompany.StatusChangeOfCompany_whoseRegExp();

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString().Replace(" ", "+")).ToString());
            //RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString().Replace(" ", "+")).ToString());
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

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

                //if (ChkStatus == 240)
                //{
                //    SendEmail(Branchid, 30);
                //    SendEmail(Branchid, 15);
                //    SendEmail(Branchid, 7);
                //}

                DataSet ds7Days = new DataSet();
                ds7Days = Check7Days();
                string CheckStatus_Friday = "";
                CheckStatus_Friday = Convert.ToString(ds7Days.Tables[0].Rows[0]["Day_Name"].ToString());

                //if (CheckStatus_Friday.Equals("FRIDAY"))
                //{
                //    EmailFreshMoreThan60Days();
                //    EmailFreshMoreThan90Days();
                //    EmailRenewalMoreThan60Days();
                //    EmailRenewalMoreThan90Days();
                //}
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
            // message.Bcc.Add(new MailAddress("isha.nsic2013@gmail.com"));
            // message.Bcc.Add(new MailAddress("dhirajb4u1981@gmail.com"));
            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;
            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;

            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;

            //   ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
            //| SecurityProtocolType.Tls
            // | SecurityProtocolType.Ssl3;

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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


    public DataSet Check7Days()
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_PendancyList_Schedular_EveryFriday", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;

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

            // message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));




            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
            smtpClient.EnableSsl = true;
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

    private static void EmailFreshMoreThan60Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("EmailID"));

        //// Step 3: Loop and send mail
        //foreach (var group in grouped)
        //{
        //    string email = group.Key;

        //    // Start HTML body
        //    string body = "<html><body>";
        //    body += "<p>Dear Branch,</p>";
        //    body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 60 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch for document verification:</p>";

        //    // Start table
        //    body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
        //    body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th></tr>";

        //    int serialNo = 1;
        //    foreach (var row in group)
        //    {
        //        string company = row.Field<string>("CompanyName");
        //        string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); ;
        //        body += "<tr>";
        //        body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td>";
        //        body += "</tr>";
        //        serialNo++;
        //    }

        //    // Close table and email body
        //    body += "</table>";
        //    body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
        //    body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
        //    body += "</body></html>";

        //    SendEmail(email, "Units pending for Document Verification (More Than 60 days) ", body);
        //}

        clsMasterUser objUser = new clsMasterUser();
        clsCompany objCompany = new clsCompany();
        clsMasterMailer objMailer = new clsMasterMailer();

        System.Data.DataSet Myds = new System.Data.DataSet();
        //TO BRANCH ACCOUNT MAILER
        Myds = objMailer.MasterMailer_Fetch_DataSet(39); //Change Here

        string bodyMail_data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        string bodystr = string.Empty;
        DataTable companydt = new DataTable();

        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");

                if (serialNo == 1)
                {
                    bodystr = bodyMail_data.Replace("$$CompanyName$$", company);
                    bodystr = bodystr.Replace("$$ApplicationDate$$", applDate);

                }
                else
                {
                    company = row.Field<string>("CompanyName");
                    applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(bodystr);
                    // Find all tables
                    var tables = doc.DocumentNode.SelectNodes("//table");

                    if (tables != null)
                    {
                        // Get the second table
                        var secondTable = tables[0];

                        // Create new row
                        var newRow = doc.CreateElement("tr");

                        // Add three cells (Company Name, Application Date, Branch Name)
                        var td1 = doc.CreateElement("td");
                        td1.InnerHtml = company;

                        var td2 = doc.CreateElement("td");
                        td2.InnerHtml = applDate;


                        // Append cells to row
                        newRow.AppendChild(td1);
                        newRow.AppendChild(td2);


                        // Append row to table
                        var tbody = secondTable.SelectSingleNode(".//tbody");
                        if (tbody != null)
                        {
                            tbody.AppendChild(newRow);
                        }
                        bodystr = doc.DocumentNode.OuterHtml;
                    }
                }
                // Close table and email body
                serialNo++;
            }
            SendEmail(email, "Units pending for Document Verification (More Than 60 days)", bodystr);
        }
    }
    private static void EmailFreshMoreThan90Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData90DaysFresh();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("ZoEmailID"));


        clsMasterUser objUser = new clsMasterUser();
        clsCompany objCompany = new clsCompany();
        clsMasterMailer objMailer = new clsMasterMailer();

        System.Data.DataSet Myds = new System.Data.DataSet();
        //TO BRANCH ACCOUNT MAILER
        Myds = objMailer.MasterMailer_Fetch_DataSet(35); //Change Here

        string bodyMail_data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        string bodystr = string.Empty;
        DataTable companydt = new DataTable();

        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                string branchName = row.Field<string>("BranchName").ToString();
                if (serialNo == 1)
                {
                    bodystr = bodyMail_data.Replace("$$CompanyName$$", company);
                    bodystr = bodystr.Replace("$$ApplicationDate$$", applDate);
                    bodystr = bodystr.Replace("$$BranchName$$", branchName);
                }
                else
                {
                    company = row.Field<string>("CompanyName");
                    applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                    branchName = row.Field<string>("BranchName").ToString();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(bodystr);
                    // Find all tables
                    var tables = doc.DocumentNode.SelectNodes("//table");

                    if (tables != null)
                    {
                        // Get the second table
                        var secondTable = tables[0];

                        // Create new row
                        var newRow = doc.CreateElement("tr");

                        // Add three cells (Company Name, Application Date, Branch Name)
                        var td1 = doc.CreateElement("td");
                        td1.InnerHtml = company;

                        var td2 = doc.CreateElement("td");
                        td2.InnerHtml = applDate;

                        var td3 = doc.CreateElement("td");
                        td3.InnerHtml = branchName;

                        // Append cells to row
                        newRow.AppendChild(td1);
                        newRow.AppendChild(td2);
                        newRow.AppendChild(td3);

                        // Append row to table
                        var tbody = secondTable.SelectSingleNode(".//tbody");
                        if (tbody != null)
                        {
                            tbody.AppendChild(newRow);
                        }
                        bodystr = doc.DocumentNode.OuterHtml;
                    }
                }
                // Close table and email body
                serialNo++;
            }
            SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", bodystr);
        }
    }
    private static void EmailRenewalMoreThan60Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData60DaysRenewal();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("BranchEmailID"));

        clsMasterUser objUser = new clsMasterUser();
        clsCompany objCompany = new clsCompany();
        clsMasterMailer objMailer = new clsMasterMailer();

        System.Data.DataSet Myds = new System.Data.DataSet();
        //TO BRANCH ACCOUNT MAILER
        Myds = objMailer.MasterMailer_Fetch_DataSet(43); //Change Here

        string bodyMail_data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        string bodystr = string.Empty;
        DataTable companydt = new DataTable();

        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");

                if (serialNo == 1)
                {
                    bodystr = bodyMail_data.Replace("$$CompanyName$$", company);
                    bodystr = bodystr.Replace("$$ApplicationDate$$", applDate);

                }
                else
                {
                    company = row.Field<string>("CompanyName");
                    applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(bodystr);
                    // Find all tables
                    var tables = doc.DocumentNode.SelectNodes("//table");

                    if (tables != null)
                    {
                        // Get the second table
                        var secondTable = tables[0];

                        // Create new row
                        var newRow = doc.CreateElement("tr");

                        // Add three cells (Company Name, Application Date, Branch Name)
                        var td1 = doc.CreateElement("td");
                        td1.InnerHtml = company;

                        var td2 = doc.CreateElement("td");
                        td2.InnerHtml = applDate;


                        // Append cells to row
                        newRow.AppendChild(td1);
                        newRow.AppendChild(td2);


                        // Append row to table
                        var tbody = secondTable.SelectSingleNode(".//tbody");
                        if (tbody != null)
                        {
                            tbody.AppendChild(newRow);
                        }
                        bodystr = doc.DocumentNode.OuterHtml;
                    }
                }
                // Close table and email body
                serialNo++;
            }
            SendEmail(email, "Units pending for Renewal Document Verification (More Than 60 days)", bodystr);
        }
    }
    private static void EmailRenewalMoreThan90Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData90DaysRenewal();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("ZoEmailID"));

        clsMasterUser objUser = new clsMasterUser();
        clsCompany objCompany = new clsCompany();
        clsMasterMailer objMailer = new clsMasterMailer();

        System.Data.DataSet Myds = new System.Data.DataSet();
        //TO BRANCH ACCOUNT MAILER
        Myds = objMailer.MasterMailer_Fetch_DataSet(41); //Change Here

        string bodyMail_data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        string bodystr = string.Empty;
        DataTable companydt = new DataTable();

        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                string branchName = row.Field<string>("BranchName").ToString();
                if (serialNo == 1)
                {
                    bodystr = bodyMail_data.Replace("$$CompanyName$$", company);
                    bodystr = bodystr.Replace("$$ApplicationDate$$", applDate);
                    bodystr = bodystr.Replace("$$BranchName$$", branchName);
                }
                else
                {
                    company = row.Field<string>("CompanyName");
                    applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                    branchName = row.Field<string>("BranchName").ToString();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(bodystr);
                    // Find all tables
                    var tables = doc.DocumentNode.SelectNodes("//table");

                    if (tables != null)
                    {
                        // Get the second table
                        var secondTable = tables[0];

                        // Create new row
                        var newRow = doc.CreateElement("tr");

                        // Add three cells (Company Name, Application Date, Branch Name)
                        var td1 = doc.CreateElement("td");
                        td1.InnerHtml = company;

                        var td2 = doc.CreateElement("td");
                        td2.InnerHtml = applDate;

                        var td3 = doc.CreateElement("td");
                        td3.InnerHtml = branchName;

                        // Append cells to row
                        newRow.AppendChild(td1);
                        newRow.AppendChild(td2);
                        newRow.AppendChild(td3);

                        // Append row to table
                        var tbody = secondTable.SelectSingleNode(".//tbody");
                        if (tbody != null)
                        {
                            tbody.AppendChild(newRow);
                        }
                        bodystr = doc.DocumentNode.OuterHtml;
                    }
                }
                // Close table and email body
                serialNo++;
            }
            SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", bodystr);
        }
    }

    //private static void EmailFreshMoreThan60Days()
    //{
    //    // Step 1: Get data from SQL
    //    DataTable unitData = GetUnitEmailData();

    //    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    //    string strOriginalMailBodytb;
    //    // Step 2: Group units by EmailID
    //    var grouped = unitData.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("EmailID"));


    //    clsMasterMailer objMailer = new clsMasterMailer();
    //    DataSet MyDs = new DataSet();
    //    DataSet MyDsTb = new DataSet();
    //    Hashtable hashtbl = new Hashtable();
    //    int successId;
    //    string MailFrom, ccMail, bccMail, MailSubject, bodyMail;
    //    string strbodytb;

    //    //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

    //    MyDs = objMailer.MasterMailer_Fetch_DataSet(35);

    //    strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
    //    MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
    //    MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();




    //    // Step 3: Loop and send mail
    //    foreach (var group in grouped)
    //    {
    //        string email = group.Key;

    //        // Start HTML body
    //        string body = "<html><body>";
    //        body += "<p>Dear Branch,</p>";
    //        body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 60 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch for document verification:</p>";

    //        // Start table
    //        body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
    //        body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th></tr>";

    //        int serialNo = 1;
    //        foreach (var row in group)
    //        {

    //            strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", row.Field<string>("CompanyName"));
    //            strMailBody = strOriginalMailBody.Replace("$$ApplicationDate$$", row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"));
    //            strMailBody = strOriginalMailBody.Replace("$$BranchName$$", row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"));

    //            MyDsTb = objMailer.MasterMailer_Fetch_DataSet(37);

    //            //string company = row.Field<string>("CompanyName");
    //            //string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); ;
    //            //body += "<tr>";
    //            //body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td>";
    //            //body += "</tr>";
    //            //serialNo++;
    //        }

    //        // Close table and email body
    //        body += "</table>";
    //        body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
    //        body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
    //        body += "</body></html>";

    //        SendEmail(email, "Units pending for Document Verification (More Than 60 days) ", body);
    //    }
    //}

    //private static void EmailFreshMoreThan90Days()
    //{
    //    // Step 1: Get data from SQL
    //    DataTable unitData = GetUnitEmailData90DaysFresh();

    //    // Step 2: Group units by EmailID
    //    var grouped = unitData.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("ZoEmailID"));

    //    // Step 3: Loop and send mail
    //    foreach (var group in grouped)
    //    {
    //        string email = group.Key;

    //        // Start HTML body
    //        string body = "<html><body>";
    //        body += "<p>Dear ZonalHead,</p>";
    //        body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 90 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch(s) for document verification:</p>";

    //        // Start table
    //        body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
    //        body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th><th>Branch Name</th></tr>";

    //        int serialNo = 1;
    //        foreach (var row in group)
    //        {
    //            string company = row.Field<string>("CompanyName");
    //            string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
    //            string branchName = row.Field<string>("BranchName").ToString();
    //            body += "<tr>";
    //            body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td><td>" + branchName + "</td>";
    //            body += "</tr>";
    //            serialNo++;
    //        }

    //        // Close table and email body
    //        body += "</table>";
    //        body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
    //        body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
    //        body += "</body></html>";

    //        SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", body);
    //    }
    //}

    //private static void EmailRenewalMoreThan60Days()
    //{
    //    // Step 1: Get data from SQL
    //    DataTable unitData = GetUnitEmailData60DaysRenewal();

    //    // Step 2: Group units by EmailID
    //    var grouped = unitData.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("BranchEmailID"));

    //    // Step 3: Loop and send mail
    //    foreach (var group in grouped)
    //    {
    //        string email = group.Key;

    //        // Start HTML body
    //        string body = "<html><body>";
    //        body += "<p>Dear Branch,</p>";
    //        body += "<p>You need to look into below mentioned units in Renewal as they have crossed that \"More than 60 days\" limit in the software. Kindly take appropriate action and clear this list.The following renewal units are pending with your branch :</p>";

    //        // Start table
    //        body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
    //        body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th></tr>";

    //        int serialNo = 1;
    //        foreach (var row in group)
    //        {
    //            string company = row.Field<string>("CompanyName");
    //            string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); ;
    //            body += "<tr>";
    //            body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td>";
    //            body += "</tr>";
    //            serialNo++;
    //        }

    //        // Close table and email body
    //        body += "</table>";
    //        body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
    //        body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
    //        body += "</body></html>";

    //        SendEmail(email, "Units pending for Document Verification (More Than 60 days) ", body);
    //    }
    //}

    //private static void EmailRenewalMoreThan90Days()
    //{
    //    // Step 1: Get data from SQL
    //    DataTable unitData = GetUnitEmailData90DaysFresh();

    //    // Step 2: Group units by EmailID
    //    var grouped = unitData.AsEnumerable()
    //        .GroupBy(row => row.Field<string>("ZoEmailID"));

    //    // Step 3: Loop and send mail
    //    foreach (var group in grouped)
    //    {
    //        string email = group.Key;

    //        // Start HTML body
    //        string body = "<html><body>";
    //        body += "<p>Dear ZonalHead,</p>";
    //        body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 90 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch(s) for document verification:</p>";

    //        // Start table
    //        body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
    //        body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th><th>Branch Name</th></tr>";

    //        int serialNo = 1;
    //        foreach (var row in group)
    //        {
    //            string company = row.Field<string>("CompanyName");
    //            string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
    //            string branchName = row.Field<string>("BranchName").ToString();
    //            body += "<tr>";
    //            body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td><td>" + branchName + "</td>";
    //            body += "</tr>";
    //            serialNo++;
    //        }

    //        // Close table and email body
    //        body += "</table>";
    //        body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
    //        body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
    //        body += "</body></html>";

    //        SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", body);
    //    }
    //}

    static DataTable GetUnitEmailData()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM,fGPtbl_Company_PAN as PAN,
 fGPtbl_Company_CDate as ApplDate,
datediff(day,fGPtbl_Company_CDate,getdate()) as Aging,
[fGPtbl_MB_Name] as BranchName,fGtbl_MB_Email as EmailID,Count([fGPtbl_Company_Id]) as ApplicationCount, 
CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' 
WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' 
WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' 
WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' 
WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' 
WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' 
WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status,
[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,
DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA from[dbo].[GPtbl_Company] 
inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0
inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 
left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 
and fGPtbl_PD_FormtypeId=1
left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 
left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] 
where [fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) 
and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and 
fGPtbl_IS_FormtypeId=1 where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 
and  [fGPtbl_Company_Status] in (3) 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2025' , 101)) as datetime)
and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],fGtbl_MB_Email,[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status, fGPtbl_Company_PAN, fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt order by Aging Desc ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }

    static DataTable GetUnitEmailData90DaysFresh()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM,fGPtbl_Company_PAN as PAN,
 fGPtbl_Company_CDate as ApplDate,
datediff(day,fGPtbl_Company_CDate,getdate()) as Aging,
[fGPtbl_MB_Name] as BranchName,fGtbl_MB_Email as EmailID,Count([fGPtbl_Company_Id]) as ApplicationCount, fGPtbl_MZ_EmailID as ZoEmailID,
CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' 
WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' 
WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' 
WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' 
WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' 
WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' 
WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status,
[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,
DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA from[dbo].[GPtbl_Company] 
inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0
inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 
inner join [dbo].[GPtbl_MasterZone] on [fGPtbl_MZ_Id]=[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_ZoneId] and [fGPtbl_MZ_Display]=1 and [fGPtbl_MZ_Delete]=0
left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 
and fGPtbl_PD_FormtypeId=1
left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 
left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] 
where [fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) 
and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and 
fGPtbl_IS_FormtypeId=1 where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 
and  [fGPtbl_Company_Status] in (3) 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2025' , 101)) as datetime)
and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],fGtbl_MB_Email,[fGPtbl_Company_Name],[fGPtbl_MB_Name],fGPtbl_MZ_EmailID, fGPtbl_Company_Status, fGPtbl_Company_PAN,fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt order by Aging Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }
    static DataTable GetUnitEmailData60DaysRenewal()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,
fGPtbl_Company_CDate as RDate,  Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' 
WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject' 
WHEN fGPtbl_Company_RenewalType = 7 THEN 'Renewal Completed' END as Status from GPtbl_CompanyRenewal 
inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_RenewalType in(1) and fGPtbl_Company_Status=7 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023' , 101)) as datetime) and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],[fGtbl_MB_Email],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate  
union  
Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,fGPtbl_Company_CDate  as RDate,  
Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_Ren_Amnd_Type = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_Ren_Amnd_Type = 2 
THEN 'Approved by Branch' WHEN fGPtbl_Company_Ren_Amnd_Type = 3 THEN 'Reject' WHEN fGPtbl_Company_Ren_Amnd_Type = 7 THEN 'Renewal Completed' END as Status 
from GPtbl_CompanyAmendment inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyAmendment.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] 
and [fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_Ren_Amnd_Type in (1) and fGPtbl_Company_Status=7 and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023', 101)) as datetime)and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], [fGtbl_MB_Email],fGPtbl_Company_Ren_Amnd_Type, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }

    static DataTable GetUnitEmailData90DaysRenewal()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,
fGPtbl_Company_CDate as RDate,  Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' 
WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject' 
WHEN fGPtbl_Company_RenewalType = 7 THEN 'Renewal Completed' END as Status from GPtbl_CompanyRenewal 
inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_RenewalType in(1) and fGPtbl_Company_Status=7 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023' , 101)) as datetime) and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],[fGtbl_MB_Email],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate  
union  
Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,fGPtbl_Company_CDate  as RDate,  
Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_Ren_Amnd_Type = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_Ren_Amnd_Type = 2 
THEN 'Approved by Branch' WHEN fGPtbl_Company_Ren_Amnd_Type = 3 THEN 'Reject' WHEN fGPtbl_Company_Ren_Amnd_Type = 7 THEN 'Renewal Completed' END as Status 
from GPtbl_CompanyAmendment inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyAmendment.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] 
and [fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_Ren_Amnd_Type in (1) and fGPtbl_Company_Status=7 and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023', 101)) as datetime)and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], [fGtbl_MB_Email],fGPtbl_Company_Ren_Amnd_Type, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }
    static void SendEmail(string toEmail, string subject, string body)
    {

        string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
        string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
        string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
        string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];
        string messagefrom = System.Configuration.ConfigurationManager.AppSettings["msgfrom"];


        MailMessage message = new MailMessage();
        // message.From = new MailAddress("emailer@support.nsicspronline.com", "Support-SinglePointRegistration");
        message.From = new MailAddress(messagefrom, "Support-SinglePointRegistration");
        message.To.Add(new MailAddress(toEmail));
        //  message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));
        // message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));

        message.Subject = subject;
        // Set the body of the mail message
        message.Body = body;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = SmtpServerName;

        message.IsBodyHtml = true;
        smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
        smtpClient.UseDefaultCredentials = false;


        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


        smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
        smtpClient.Send(message);

    }



}
