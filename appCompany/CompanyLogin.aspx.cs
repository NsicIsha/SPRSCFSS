using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class appCompany_CompanyLogin : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int successId;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    int BranchId;
    int CompanyId;
    dalVariable objDal;
    
    clsMasterUser objUser;
    clsMasterMailer objMailer;
     

    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        lblMsgFGP.Visible = false;

        txtUserName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnLogin');");
        txtPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnLogin');");
        txtUserNameFGP.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmitFGP');");

        if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtUserName');", true);
        }

        if (!IsPostBack)
        {
            try
            {
                int i;
                HttpCookie acookie = new HttpCookie("OSUYULUKR8");
                int limit = (Request.Cookies.Count - 1);
                for (i = 0; (i <= limit); i++)
                {
                    acookie = Request.Cookies[i];
                    acookie.Expires = DateTime.Now.AddHours(-1);
                    Response.Cookies.Add(acookie);
                }
                //Disable back in browser -----------------
                Session.Abandon();
                Application.RemoveAll();
                Response.Buffer = false;
                Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
                Response.Expires = 0;
                Response.CacheControl = "no-cache";
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
            }
        }
         
    }

    protected void btnSubmitFGP_Click(object sender, EventArgs e)
    {
        try
        {
            Hashtable hashtbl = new Hashtable();
            objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            
            string FromMail, ccMail, bccMail, subject, bodyMail;

            hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text,"none");

            //TO FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(6);

            successId = Convert.ToInt32(hashtbl["Output"]);
           

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +hashtbl["pwd"].ToString() + ".";

            string bodystr = bodyMail.Replace("$$password$$", hashtbl["pwd"].ToString());
            bodystr = bodystr.Replace("$$username$$", txtUserNameFGP.Text.Trim());
            bodystr = bodystr.Replace("Branch Name:", string.Empty);
            bodystr = bodystr.Replace("$$branch$$.", string.Empty);
           // Branch Name: $$branch$$.
            switch (successId)
            {
                case 1://COMPANY NOT EXIST
                    lblMsgFGP.Visible = true;
                    lblMsgFGP.Text = "User not exist";
                    txtUserNameFGP.Text = "";

                    ModalPopupExtender1.Show();
                    break;
                case 2://Password successfully changed
                    sendmail(FromMail,hashtbl["email"].ToString(),bccMail, bccMail, subject, bodystr);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Password sent to your email";
                    break;
                case 3://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later";
                    break;
                case 4://User is inactive
                    lblMsgFGP.Visible = true;
                    lblMsgFGP.Text = "This user account is DEACTIVATED !";
                    txtUserNameFGP.Text = "";

                    ModalPopupExtender1.Show();
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public Hashtable Company_ForgotPassword(string Username, string Password)
    {
        Hashtable hashtbl = new Hashtable();
        string Email, Companyname;
        Email = "";
        Companyname = "";
        int companyid = 0;

        try
        {
            RandomKeyGenerator objRnd = new RandomKeyGenerator();
            string passwordFgp = objRnd.Generate();

            MySqlComm = new SqlCommand("GPsp_Company_ForgotPassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Username", SqlDbType.VarChar, 200).Value = Username;

            if (Password == "none")//FORGOT PASSWORD
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 2000).Value = passwordFgp;
                MySqlComm.Parameters.Add("@ToIdentify", SqlDbType.Int).Value = 1;
            }
            else
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 2000).Value = Password;
                MySqlComm.Parameters.Add("@ToIdentify", SqlDbType.Int).Value = 1;
            }


            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            SqlParameter outEmail = new SqlParameter("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 200);
            outEmail.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outEmail);

            SqlParameter outCompanyname = new SqlParameter("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000);
            outCompanyname.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompanyname);

            SqlParameter outCompanyid = new SqlParameter("@fGPtbl_Company_Id", SqlDbType.Int);
            outCompanyid.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompanyid);

            //SqlParameter outBranchId = new SqlParameter("@BranchId", SqlDbType.Int);
            //outCompanyid.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(outBranchId);

            MySqlComm.ExecuteNonQuery();

            if (!DBNull.Value.Equals(outEmail.Value))
            {
                Email = outEmail.Value.ToString();
            }
            if (!DBNull.Value.Equals(outCompanyname.Value))
            {
                Companyname = outCompanyname.Value.ToString();
            }
            //string Password = outPwd.ToString();
            int output = Convert.ToInt32(outVal.Value);


            if (!DBNull.Value.Equals(outCompanyid.Value))
            {
                companyid = Convert.ToInt32(outCompanyid.Value);
            }
            //if (!DBNull.Value.Equals(outBranchId.Value))
            //{
            //    BranchId = Convert.ToInt32(outBranchId.Value);
            //}


            hashtbl.Add("email", Email);
            hashtbl.Add("pwd", passwordFgp);
            hashtbl.Add("Output", output);
            hashtbl.Add("compname", Companyname);
            hashtbl.Add("companyid", companyid);
            //hashtbl.Add("BranchId", BranchId);

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
        return hashtbl;
    }

     
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            Hashtable hashtblLogin = new Hashtable();
            hashtblLogin = Company_ForgotPassword(txtUserName.Text, txtPassword.Text);// objCompany.Company_ForgotPassword(txtUserName.Text, txtPassword.Text);

            successId = Convert.ToInt32(hashtblLogin["Output"]);
            CompanyId = Convert.ToInt32(hashtblLogin["companyid"]);
            BranchId = Convert.ToInt32(hashtblLogin["BranchId"]);


            string EncryptUserId = string.Empty, EncryptRoleId = string.Empty, EncryptDepartmentId = string.Empty;
                 string EncryptBranchId = string.Empty, EncryptCompany_id = string.Empty;
            
            switch (successId)
            {
                case 1://COMPANY NOT EXIST
                    lblMessage.Visible = true;
                    lblMessage.Text = "User not exist!";
                    txtUserName.Text = "";
                    txtUserName.Focus();
                    txtPassword.Text = "";
                    break;
                case 3://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later!";
                    break;
                case 4://User is inactive
                    lblMessage.Visible = true;
                    lblMessage.Text = "This user account is DEACTIVATED!";
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    break;
                case 5://PASSWORD MATCHED
                    //TO SEND TO HOME PAGE
                    HttpCookie aCookie = new HttpCookie("OSUYULUKR8");
                    //aCookie.Values["USERID"] = Convert.ToString(1000);//Convert.ToString(txtUserName.Text);//username in company
                    //aCookie.Values["ROLEID"] = Convert.ToString(7);// roleid from rolemaster
                    //aCookie.Values["DEPARTMENTID"] = Convert.ToString(hashtblLogin["companyid"]);//INCASE OF COMPANY IT KEEPS COMPANY ID
                    //aCookie.Values["USERFULLNAME"] = hashtblLogin["compname"].ToString();//company name from company
                    //aCookie.Expires = DateTime.Now.AddDays(1);
                    //Response.Cookies.Add(aCookie);
                    //Session["OSUYULUKR8-ERUYIRE"] = "GP";
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();

                    string struserid = "1000";
                    EncryptUserId = objDecEnc.Encrypt(Convert.ToString(struserid));// objDecEnc.Encrypt(Convert.ToString(txtUserName.Text));
                    EncryptRoleId = objDecEnc.Encrypt(Convert.ToString(7));


                    EncryptCompany_id = objDecEnc.Encrypt(Convert.ToString(hashtblLogin["companyid"]));
                    EncryptBranchId= objDecEnc.Encrypt(Convert.ToString(hashtblLogin["BranchId"]));

                   // aCookie = new HttpCookie("OSUYULUKR8");
                    aCookie.Values["USERID"] = Convert.ToString(EncryptUserId);
                    aCookie.Values["ROLEID"] = Convert.ToString(EncryptRoleId);
                    aCookie.Values["DEPARTMENTID"] = Convert.ToString(EncryptDepartmentId);
                    aCookie.Values["USERFULLNAME"] = hashtblLogin["compname"].ToString();

                    aCookie.Values["EncryptCompany_id"] = Convert.ToString(EncryptCompany_id);
                    aCookie.Values["EncryptBranchId"] = Convert.ToString(EncryptBranchId);


                    aCookie.Values["UserEmailId"] = txtUserName.Text;
                    aCookie.Expires = DateTime.Now.AddDays(1);
                    Response.Cookies.Add(aCookie);
                    Session["OSUYULUKR8-ERUYIRE"] = "GP";

                    Response.Redirect("CompanyHome.aspx", false);
                    break;
                case 6://PASSWORD MISMATCHED
                    lblMessage.Visible = true;
                    lblMessage.Text = "Invalid User Name or Password!";
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    txtUserName.Focus();
                    break;
                default://Error in operation
                    lblMessage.Visible = true;
                    lblMessage.Text = "Server is busy please try later!";
                    break;

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public static void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];
            //MailFrom = MailFrom;  //"nsicclient@nsic.co.in";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(MailFrom);  // MailFrom
            // Set the recepient address of the mail message
            message.To.Add(new MailAddress(MailTo));

            // Check if the bcc value is null or an empty string
            if ((bccMail != null) && (bccMail != string.Empty))
            {
                // Set the Bcc address of the mail message
                message.Bcc.Add(new MailAddress(bccMail));
            }      // Check if the cc value is null or an empty value
            if ((ccMail != null) && (ccMail != string.Empty))
            {
                // Set the CC address of the mail message
                message.CC.Add(new MailAddress(ccMail));
            }       // Set the subject of the mail message
            message.Subject = subject;
            // Set the body of the mail message
            message.Body = bodyMail;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);

            // smtpClient.Send(message);
            // smtpClient.Timeout = 500;




        }
        catch (Exception ex)
        {
            //ex.Message("Server Busy!!!!!!!!!");
        }
    }

    protected void lnkForgetPassword_Click(object sender, EventArgs e)
    {

    }
}
