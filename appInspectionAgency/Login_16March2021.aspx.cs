using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Net.Mail;



public partial class appInspectionAgency_Login : System.Web.UI.Page
{
    dalVariable objDal;
    System.Data.DataSet Myds;
    clsMasterInspectionAgency objAgency;
    clsMasterMailer objMailer;
    int ErrorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Visible = false;
            lblMsgFGP.Visible = false;

            txtUserName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
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
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int UserId = 0;
            int RoleId = 0;
            string UserFullname = "";
            int AgencyId = 0;
            int BranchId = 0;
            string BranchName = "";
            string EmailID = "";

            string Username = txtUserName.Text.ToString().Trim();
            string Password = txtPassword.Text.ToString().Trim();
            int Cnt = 0;

            objAgency = new clsMasterInspectionAgency();
            //IP CAPTURE
            string LoginIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(LoginIP))
            {
                string[] ipRange = LoginIP.Split(',');
                //int le = ipRange.Length - 1;
                //string trueIP = ipRange[0];
                LoginIP = ipRange[0];
            }
            else
            {
                LoginIP = Request.ServerVariables["REMOTE_ADDR"];
            }
            //IP CAPTURE
            //System.Data.SqlClient.SqlDataReader Dr = objAgency.Fetch_AgencyByName(Username, Password);
            System.Data.SqlClient.SqlDataReader Dr = objAgency.MasterInspectionAgency_User_Login(Username, Password, LoginIP);
            while (Dr.Read())
            {
                Cnt += 1;
                UserId = Convert.ToInt32(Dr["fGPtbl_MIAU_Id"]);
                RoleId = Convert.ToInt32(Dr["RoleId"]);
                AgencyId = Convert.ToInt32(Dr["fGPtbl_MIAU_AgencyId"]);
                BranchId =  Convert.ToInt32(Dr["fGPtbl_TIAB_BranchId"]);
                UserFullname = Convert.ToString(Dr["fGPtbl_MIAU_Name"]) + " ";
                BranchName = "";// Convert.ToString(Dr["fGPtbl_MB_Name"]);
                EmailID = Convert.ToString(Dr["fGPtbl_MIAU_Email"]);
            }
            Dr.Close();
            NSICGP_VBClassLibrary.DBManager.CloseConnection();

            if (Cnt == 0)
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                lblMessage.Text = "INVALID User Name or Password !";
                lblMessage.Visible = true;
            }
            else if (Cnt == 1)
            {
                HttpCookie aCookie = new HttpCookie("OSUYULUKR8");
                aCookie.Values["USERID"] = Convert.ToString(UserId);
                aCookie.Values["ROLEID"] = Convert.ToString(RoleId);
                aCookie.Values["AGENCYID"] = Convert.ToString(AgencyId);
                aCookie.Values["BRANCHID"] = Convert.ToString(BranchId);
                aCookie.Values["USERFULLNAME"] = UserFullname;
                aCookie.Values["BRANCHNAME"] = BranchName;
                aCookie.Values["EmailID"] = Convert.ToString(EmailID);
                aCookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Add(aCookie);

                Session["OSUYULUKR8-INSERYER"] = "GP";

                Response.Redirect("~/appInspectionAgency/Home.aspx", false);
            }
            else
            {
                txtUserName.Text = "";
                txtPassword.Text = "";
                lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";
                lblMessage.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
   
    protected void btnSubmitFGP_Click(object sender, EventArgs e)
    {
        try
        {
            objAgency = new clsMasterInspectionAgency();
            objMailer = new clsMasterMailer();
            Myds = new DataSet();
            int id;
            string FromMail, ccMail, bccMail, subject, bodyMail;
            ArrayList arl = new ArrayList();

            //TO FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(6);

            arl = objAgency.InspectionAgency_ForgotPassword(txtUserNameFGP.Text);
            id = Convert.ToInt32(arl[0]);

            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";

            string bodystr = bodyMail.Replace("$$password$$", arl[2].ToString());
            bodystr = bodystr.Replace("$$username$$", txtUserNameFGP.Text.Trim());
            bodystr = bodystr.Replace("Branch Name:", string.Empty);
            bodystr = bodystr.Replace("$$branch$$.", string.Empty);

            switch (id)
            {
                case 1://User not exist
                    lblMsgFGP.Visible = true;
                    lblMsgFGP.Text = "User not exist";
                    txtUserNameFGP.Text = "";

                    ModalPopupExtender1.Show();
                    break;
                case 2://Password successfully changed
                    sendmail(FromMail,arl[1].ToString(), bccMail, bccMail, subject, bodystr);
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

    public static void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];

            
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
            smtpClient.Port = 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);





        }
        catch (Exception ex)
        {
            //ex.Message("Server Busy!!!!!!!!!");
        }
    }

}
