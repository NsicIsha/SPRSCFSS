using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
 
using System.Xml.Linq;
 
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Schema;
using System.Xml;
using System.Collections;
public partial class appAdministrator_WaitingCompanies_Accept : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds;
    int count = 1;
    int successId, urlId = 0, amendmentid;
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
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    string MailBodyBranch = string.Empty;
    string strOriginalMailBodyBranch = string.Empty, strMailBodyBranch = string.Empty;
    string MailSubjectBranch = string.Empty;


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
        //txtInstr.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 8)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
                }
                if (Request.QueryString["aid"] != null)
                {
                    amendmentid = Convert.ToInt32(Request.QueryString["aid"].ToString());
                }

                if (Page.IsPostBack != true)
                {
                    txtReason.Visible = true;
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                txtReason.Visible = false;
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


    public int Company_WaitingCompany_Registration_Accept_Online(int CompanyId, int ApplicationType,string Approveremarks)
    {
        int ErrorId=0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Waiting_Update_AcceptCompany_ById_Online", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Company_Id", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@ApplicationType", SqlDbType.Int).Value = ApplicationType;
            MySqlComm.Parameters.Add("@Approveremarks", SqlDbType.VarChar,500).Value = Approveremarks;


            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }

    //TO MAKE ENTRIES IN REJECTION TABLE
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            int formtypeid, cunit, companyid;
            string FromMail, ccMail, bccMail, subject, bodyMail, companymail;

            //ACCESSING FORMTYPEID, COMPANY ID, COMPANY EMAIL
            cunit = Convert.ToInt32(Request.QueryString["uid"]);
            if (urlId == 1)//FOR REGISTRATION
            {
                objCompany = new clsCompany();
                Myds = Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(cunit);
            }
            else if (urlId == 3)//FOR AMENDMENT
            {
                objAmendment = new clsAmendment();
                //   Myds = objAmendment.CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(amendmentid);
            }

            //formtypeid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_FormtypeId"]);
            companyid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_Company_Id"]);
            companymail = Myds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();

       // successId = objCompany.Company_WaitingCompany_Registration_Rejection_(cunit, urlId, txtReason.Text, UserId);

            int ApplicationType=0;
            if(rdbApplicationType.SelectedValue=="0")
            {
                ApplicationType = 0;
            }
            if (rdbApplicationType.SelectedValue == "1")
            {
                ApplicationType = 1;
            }
            else
            {
                ApplicationType = 0;
            }


            successId = Company_WaitingCompany_Registration_Accept_Online(companyid, ApplicationType, txtReason.Text);
            //TO FETCH MAILER
            ////////DataSet dsmail = new DataSet();
            ////////dsmail = objMailer.MasterMailer_Fetch_DataSet(26); // 26 for Acceptance 
            ////////FromMail = dsmail.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ////////ccMail = dsmail.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            ////////bccMail = dsmail.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            ////////subject = dsmail.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            ////////bodyMail = dsmail.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString() + txtReason.Text + ".";

            SendEmail(Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_BranchId"]), companyid);



            if (successId == 2)
            {
                ////////SendMail.Mail_WithOUTAttachment(companymail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
                Session["companyAction"] = "Status changed successfully!";
                if (urlId == 1)
                {
                    Response.Redirect("WaitingCompaniesOnline.aspx?i=1", false);
                }
                else if (urlId == 3)
                {
                    Response.Redirect("WaitingCompaniesOnline.aspx?i=3", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public DataSet Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid_Online", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    public DataTable Company_Fetch_BranchAddress(int BranchId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_BranchAddressFetch_POI", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = BranchId;


            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDt;
    }
    public void SendEmail(int BranchId,int CompanyId)
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();

        DataSet MyDsBranch = new DataSet();

        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(26);

       // MyDsBranch = objMailer.MasterMailer_Fetch_DataSet(19);

        string CompName = string.Empty, BrName = string.Empty, BrAddress = string.Empty, BrPhone = string.Empty, BrEmail = string.Empty;

        string CompPan = string.Empty; string MailTo = string.Empty;
        string AuthPName = string.Empty, AuthPMobile = string.Empty, AuthPEmail = string.Empty, Address = string.Empty;


        //MyDt = new DataTable();
        //MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_OneMonth_Mailar", DBManager.ConnectToDB());
        //MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //MyAdp.SelectCommand.CommandTimeout = 50000;
        //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
        //MyAdp.SelectCommand.Parameters.Add("@Days", SqlDbType.Int).Value = Days;
        //MyAdp.Fill(MyDt);



        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

        DataSet dsCompany = new DataSet();


        dsCompany = Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(CompanyId);
        MailTo= dsCompany.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
        CompName = dsCompany.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        CompPan= dsCompany.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
        //AuthPName = dsCompany.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        //AuthPMobile = dsCompany.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        //AuthPEmail = dsCompany.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        //Address = dsCompany.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        System.Data.DataTable MyDt = new System.Data.DataTable();
        MyDt = Company_Fetch_BranchAddress(Convert.ToInt32(BranchId));


        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(MailTo);
                //BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                CompName =  Convert.ToString(CompName);  

                CompPan = Convert.ToString(CompPan); 

                BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                BrAddress = Convert.ToString(drIA["fGtbl_MB_Address"]);
                BrPhone = Convert.ToString(drIA["fGtbl_MB_Phone"]);
                BrEmail = Convert.ToString(drIA["fGtbl_MB_Email"]);

                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$branchName$$", BrName);
                strMailBody = strMailBody.Replace("$$PAN$$", CompPan);
                strMailBody = strMailBody.Replace("$$AutorizedName$$", BrName);
                strMailBody = strMailBody.Replace("$$Mobile$$", BrPhone);
                strMailBody = strMailBody.Replace("$$Email$$", BrEmail);
                strMailBody = strMailBody.Replace("$$Address$$", BrAddress); 
                

                // BrEmail = "itsupport2@nsic.co.in";
                if (MailTo != "")
                {
                    SendMailToUnit(MailTo, strMailBody, MailFrom, MailSubject);

                    


                    //INSERT INTO SENT MAILER
                    // clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);

                    // clsDDL.Insert_MailDetails(BrEmail, UserId, 1, MailFrom, 1, 1, MailSubjectBranch, strMailBodyBranch, 1);

                }
            }//foreach
        }//if

    }

    public static string SendMailToUnit(string tomail, string mailbody, string mailfrom, string mailsubject)
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
            // message.Bcc.Add(new MailAddress("ctms@nsic.co.in"));
           // message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));
            //'Isha Singh' <>

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

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (urlId == 1)
            {
                Response.Redirect("WaitingCompanies.aspx?i=1", false);
            }
            else if (urlId == 3)
            {
                Response.Redirect("WaitingCompanies.aspx?i=3", false);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}