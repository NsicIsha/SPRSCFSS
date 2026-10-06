using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;
 
using System.Xml.Linq;
using System.Net;
using System.Data.SqlClient;
 
using System.Xml.Schema;
using System.Xml;
using System.Collections;

public partial class appAdministrator_ThankyouOnline : System.Web.UI.Page
{
    int UserId, RoleId, Companyid;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string sub;
    string BranchName = string.Empty, CompanyName = string.Empty;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    string fromAddress;
    string toAddress;

 
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;

    string MailBodyBranch = string.Empty;
    string strOriginalMailBodyBranch = string.Empty, strMailBodyBranch = string.Empty;
    string MailSubjectBranch = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            DecryptEncrypt objencrypt = new DecryptEncrypt();
           
            if (Request.QueryString["id"] != null)
            {
                //Companyid = Convert.ToInt32(objencrypt.Decrypt(Request.QueryString["id"].ToString()));
                 Companyid = Convert.ToInt32(objencrypt.Decrypt(Request.QueryString["id"].Replace(" ", "+")).ToString());
                //CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());

             //   Companyid = Convert.ToInt32(Request.QueryString["id"]);

            }
        }
        catch { }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        string msg;
        //if (Session["ReferenceNo"].ToString() == "1")
        //{
        //    msg = "Ammendment form successfully submited<br>";
        //    lblMsg.Text = msg;
        //}
        //else if (Session["ReferenceNo"].ToString() == "2")
        //{
        //    msg = "Renewal form successfully submited<br>";
        //    lblMsg.Text = msg;
        //}
          if (Session["ReferenceNo"].ToString() == "8" || Session["ReferenceNo"].ToString() == "7")
        {
            lblMsg.Text = "Application data successfully updated";
        }


        else
        {
            msg = "Thank You for registering with us.<br>";
            msg = msg + "Your unique reference no. is <i>'" + Session["ReferenceNo"].ToString() + "'</i><br>";
            msg = msg + "Please remember this for future use";

            lblMsg.Text = msg;
        }

        clsMasterBranch objBranch = new clsMasterBranch();
        DataTable Mydt = new DataTable();
        Mydt = objBranch.Fetch_BranchAddress_ByCompanyId(Companyid);
        try
        {
            if (Mydt.Rows.Count > 0)
            {
                ltlBranchAddress.Text = Mydt.Rows[0]["ADDRESS"].ToString();
                ltlBranchPhone.Text = Mydt.Rows[0]["PHONE"].ToString();
                ltlBranchFax.Text = Mydt.Rows[0]["FAXNO"].ToString();
                ltlBranchEmail.Text = Mydt.Rows[0]["EMAILID"].ToString();
                BranchName = Mydt.Rows[0]["BranchName"].ToString();
                CompanyName = Mydt.Rows[0]["CompName"].ToString();
            }
            if (RoleId == 7 || RoleId == 0)
            {
                SendMail(); // For branch 
                SendEmail(); // For Unit
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }


    }//page load
    public void SendMail()
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



            MailTo = ltlBranchEmail.Text;// txtTo.Text.ToString();
            MailFrom = SmtpUser;// "no-reply@nsicnet.in";

            MailSubject = "New company has applied for SPR certificate.";//txtSub.Text;

            MailBody = " Dear " + BranchName.ToString() + " Branch user, <br/><br/>A new company " + CompanyName
                + " has applied for SPR certificate.<br/>Please login at http://www.nsicspronline.com/appadministrator/login.aspx to process it." +
                "<br/><br/>Thanks & regards<br/>SPR Administrator.";


            // ((txtbody.Text.Replace(Environment.NewLine, "<br />")));
            MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            smtpClient.Send(message);

            //INSERT INTO SENT MAILER (MAY BE SOME INTEGER VALUE ARE WRONG BUT MAIL BODY COULD GUIDE)
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 3, 1, MailSubject, MailBody, 3);

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Send SuccessFully to Branch....'); window.opener.location.reload(true); window.close();</script>");
        }
        catch (Exception ex)
        {
            // ex.Message = "Server Busy.....................";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Does Not Send SuccessFully. Because Server Busy ..........'); window.opener.location.reload(true); window.close();</script>");
        }


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


    public void SendEmail()
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();

        DataSet MyDsBranch = new DataSet();

        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(22);

        MyDsBranch = objMailer.MasterMailer_Fetch_DataSet(24);

        string CompName = string.Empty, BrName = string.Empty, BrAddress = string.Empty, BrPhone = string.Empty, BrEmail = string.Empty;


        string AuthPName = string.Empty, AuthPMobile = string.Empty, AuthPEmail = string.Empty, ProductDetails = string.Empty;


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



        strOriginalMailBodyBranch = MyDsBranch.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        //MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubjectBranch = MyDsBranch.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

        clsMasterBranch objBranch = new clsMasterBranch();
        DataTable Mydtt = new DataTable();
        Mydtt = objBranch.Fetch_BranchAddress_ByCompanyId(Companyid);
        string CompanyEmail= Convert.ToString(Mydtt.Rows[0]["CompEmail"].ToString());
        string CompanyName = Convert.ToString(Mydtt.Rows[0]["CompName"].ToString());
        int BranchId= Convert.ToInt32(Mydtt.Rows[0]["BranchID"].ToString());
        string AuthorisedPersonEmail = Convert.ToString(Mydtt.Rows[0]["CompEmail"].ToString());
        string AuthorisedPersonName = Convert.ToString(Mydtt.Rows[0]["AuthPersonName"].ToString());
        string AuthorisedPersonMobile = Convert.ToString(Mydtt.Rows[0]["compAutPersonMobile"].ToString());

        System.Data.DataTable MyDt = new System.Data.DataTable();
        MyDt = Company_Fetch_BranchAddress(Convert.ToInt32(BranchId));


        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(CompanyEmail);
                //BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                CompName = Convert.ToString(CompanyName);

                BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                BrAddress = Convert.ToString(drIA["fGtbl_MB_Address"]);
                BrPhone = Convert.ToString(drIA["fGtbl_MB_Phone"]);
                BrEmail = Convert.ToString(drIA["fGtbl_MB_Email"]);

                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", CompName);
                strMailBody = strMailBody.Replace("$$Name$$", BrName);
                strMailBody = strMailBody.Replace("$$Address$$", BrAddress);
                strMailBody = strMailBody.Replace("$$Phone$$", BrPhone);
                strMailBody = strMailBody.Replace("$$Email$$", BrEmail);
                strMailBody = strMailBody.Replace("$$BranchName$$", BrName);


                strMailBodyBranch = strOriginalMailBodyBranch.Replace("$$BranchName$$", BrName);
                strMailBodyBranch = strMailBodyBranch.Replace("$$COMPANY_NAME$$", CompName);

                AuthPName = Convert.ToString(AuthorisedPersonName);
                AuthPMobile = Convert.ToString(AuthorisedPersonMobile);
                AuthPEmail = Convert.ToString(AuthorisedPersonEmail);
                ProductDetails = Convert.ToString("");

                strMailBodyBranch = strMailBodyBranch.Replace("$$AutorizedName$$", AuthPName);
                strMailBodyBranch = strMailBodyBranch.Replace("$$Mobile$$", AuthPMobile);
                strMailBodyBranch = strMailBodyBranch.Replace("$$Email$$", AuthPEmail);
                strMailBodyBranch = strMailBodyBranch.Replace("$$ProductDetails$$", ProductDetails);

                // BrEmail = "itsupport2@nsic.co.in";
                if (MailTo != "")
                {
                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);

                    SendMailToBranch(BrEmail, strMailBodyBranch, MailFrom, MailSubjectBranch);


                    //INSERT INTO SENT MAILER
                    // clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);

                    // clsDDL.Insert_MailDetails(BrEmail, UserId, 1, MailFrom, 1, 1, MailSubjectBranch, strMailBodyBranch, 1);

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
            // message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));
           // message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));
            //'Isha Singh' <>

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;587;
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


}