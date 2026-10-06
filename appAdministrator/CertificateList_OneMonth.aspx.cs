using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
 
using System.Net.Mail;

public partial class appAdministrator_CertificateList_OneMonth : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;


    string fromAddress;
    string toAddress;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;


    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;

    DataSet MyDs;
    DataTable MyDt;

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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);


            if (RoleId == 8)
            {
                dtold = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["fixeddate"]);
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                    GridViewDataBind();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }


    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
            string SearchCriteria = string.Empty;
            SearchCriteria = txtSearchbox.Text.Trim();

        int SearchDuration = 0;
        if (rdbDuration.SelectedValue == "1")
        {
            SearchDuration = 1;
        }
        else if (rdbDuration.SelectedValue == "6")
        {
            SearchDuration = 6;
        }

        objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = Company_Fetch_Certificate_Search_OneMonth(Branchid, 0, "", 1);
            gvCertificateDetails.DataSource = dt;
            gvCertificateDetails.DataBind();
            int k = 0;
            if (dt.Rows.Count > 0)
            {
                k = Convert.ToInt32(dt.Rows.Count.ToString());
            }
            lbl_count_text.Text = "Total Count :" + Convert.ToString(k);

            if (gvCertificateDetails.Rows.Count == 0)
            {
                ltlMessage.Text = "No Data Found";
            }
            else
            {
                ltlMessage.Text = " ";
            }
    }

    protected void gvCertificateDetails_RowDataBound(object o, GridViewRowEventArgs e)
    {
        // Set the alignment of cells containing numeric data.
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
           // e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Center;
        }
    }

    protected void CertificateListPageindexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCertificateDetails.PageIndex = e.NewPageIndex;
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void SearchGridviewBind()
    {
        string SearchCriteria = string.Empty;
        SearchCriteria = txtSearchbox.Text.Trim();
        int SearchDuration=0;
        if(rdbDuration.SelectedValue=="1")
        {
            SearchDuration = 1;
        }
        else if(rdbDuration.SelectedValue == "6")
        {
            SearchDuration = 6;
        }
        objCompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = Company_Fetch_Certificate_Search_OneMonth(Branchid, Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria, SearchDuration);
        gvCertificateDetails.DataSource = dt;
        gvCertificateDetails.DataBind();

        int k = 0;
        if (dt.Rows.Count > 0)
        {
            k = Convert.ToInt32(dt.Rows.Count.ToString());
        }
        lbl_count_text.Text = "Total Count :" + Convert.ToString(k);

        if (gvCertificateDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
        else
        {
            ltlMessage.Text = " ";
        }
    }


    /* MAIL TO PROVIDE COMPANY LIST WHOSE INSPECTION DATE TO BE ISSUED */
    ////public void Mail_InspectionDateTobeIssued()
    ////{
    ////    try
    ////    {
    ////        string CompName = string.Empty, BrName = string.Empty, IAname = string.Empty;
    ////        MyDt = new DataTable();
    ////        MyAdp = new SqlDataAdapter("GPsp_Scheduler_CompanyListInspectionDateToBeIssued", DBManager.ConnectToDB());
    ////        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

    ////        MyAdp.Fill(MyDt);

    ////        MyDs = new DataSet();
    ////        objMailer = new clsMasterMailer();
    ////        MyDs = objMailer.MasterMailer_Fetch_DataSet(9);

    ////        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
    ////        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
    ////        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

    ////        if (MyDt.Rows.Count > 0)
    ////        {
    ////            foreach (DataRow drIA in MyDt.Rows)
    ////            {
    ////                MailTo = Convert.ToString(drIA["BrEmail"]);
    ////                BrName = Convert.ToString(drIA["BrName"]);
    ////                IAname = Convert.ToString(drIA["IAName"]);
    ////                CompName = Convert.ToString(drIA["CompName"]);
    ////                strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
    ////                strMailBody = strMailBody.Replace("$$inspagency$$", IAname);
    ////                strMailBody = strMailBody.Replace("$$compname$$", CompName);
    ////                if (MailTo != "")
    ////                {
    ////                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
    ////                    //INSERT INTO SENT MAILER
    ////                    clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
    ////                }
    ////            }//foreach
    ////        }//if
    ////    }//try
    ////    catch (Exception ex)
    ////    {
    ////        ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
    ////    }
    ////    finally
    ////    {
    ////        MyAdp.Dispose();
    ////        DBManager.CloseConnection();
    ////    }

    ////}

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


                if (MailTo != "")
                {
                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                    //INSERT INTO SENT MAILER
                    clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                }
            }//foreach
        }//if

    }
    protected void btnSend_Click(object sender, EventArgs e)
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
        MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = Branchid;
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
                ExpiryDate= Convert.ToString(drIA["ExpiryDate_Registration_Date_Minus_One"]);

                NumberOfDays= Convert.ToString(drIA["Validity_gap"]);

                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", CompName);
                strMailBody = strMailBody.Replace("$$ExpiryDate$$", ExpiryDate);

                if(NumberOfDays== "0 Days".ToString().Trim())
                {
                    NumberOfDays="Today, 0 Day";
                }
                else
                {
                    NumberOfDays= NumberOfDays.ToString();
                }
                strMailBody = strMailBody.Replace("$$numberofdays$$", NumberOfDays);


                if (MailTo != "")
                {
                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                    //INSERT INTO SENT MAILER
                    clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                }
            }//foreach
        }//if

       

    }

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
            //message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
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


    public DataTable Company_Fetch_Certificate_Search_OneMonth(int branchid, int ddlcriteria, string searchcriteria,int SearchDuration)
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


    public DataTable CompanyRenewal_EmailStatus_Branch(int branchid)
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

    protected void rdbDuration_SelectedIndexChanged(object sender, EventArgs e)
    {
        string SearchCriteria = string.Empty;
        SearchCriteria = txtSearchbox.Text.Trim();
        int SearchDuration = 0;
        if (rdbDuration.SelectedValue == "1")
        {
            SearchDuration = 1;
        }
        else if (rdbDuration.SelectedValue == "6")
        {
            SearchDuration = 6;
        }
        objCompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = Company_Fetch_Certificate_Search_OneMonth(Branchid, Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria, SearchDuration);
        gvCertificateDetails.DataSource = dt;
        gvCertificateDetails.DataBind();

        int k = 0;
        if (dt.Rows.Count > 0)
        {
            k = Convert.ToInt32(dt.Rows.Count.ToString());
        }
        lbl_count_text.Text = "Total Count :" + Convert.ToString(k);

        if (gvCertificateDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
        else
        {
            ltlMessage.Text = " ";
        }
    }
}




