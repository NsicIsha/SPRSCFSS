using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
public partial class appAdministrator_Login_New : System.Web.UI.Page
{
    dalVariable objDal;
    DataSet Myds;
    clsMasterUser objUser;
    clsMasterMailer objMailer;
    int ErrorId = 0;

    string MailTo, MailFrom;

    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;

    int MessageFlage = 0;
    string userid_chek;


    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            lblMessage.Visible = false;
            //lblMsgFGP.Visible = false;

            txtUserName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
           // txtUserNameFGP.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmitFGP');");
            btnSubmit.Attributes.Add("onclick", "checkVersion();");
            if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtUserName');", true);
            }

            if (!IsPostBack)
            {
                try
                {
                    Response.Redirect("Login_New_Encrypted.aspx");

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



        if (Convert.ToString(Session["Code"]).ToLower() != txtCaptcha.Text.Trim().ToLower())
        {
            Session["Code"] = null;
            txtCaptcha.Text = "";
            divCaptcha.Visible = true;
            txtCaptcha.Focus();
            lblCaptcha.Text = "Incorrect verification code. Please try again.";
            lblCaptcha.ForeColor = Color.Red;
            return;
        }


        try
        {
            if (txtUserName.Text.ToString().Trim().ToUpper() != "KPMG".Trim())
            {

                // aCookie = Request.Cookies;  // HttpContext.Current.Request.Cookies;


                int UserId = 0;
                int RoleId = 0;
                string UserFullname = "";
                int DepartmentId = 0;
                int ActiveFlag = 0;
                int LoginNo = 0;
                string UserEmailId = "";

                string Username = txtUserName.Text.ToString().Trim();
                string Password = txtPassword.Text.ToString().Trim();
                int Cnt = 0;
                HttpCookie aCookie;
                objUser = new clsMasterUser();
                System.Data.SqlClient.SqlDataReader Dr = objUser.MasterUser_CheckCredentials(Username, Password);
                while (Dr.Read())
                {
                    Cnt += 1;
                    UserId = Convert.ToInt32(Dr["fGPtbl_MU_Id"]);
                    RoleId = Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]);
                    DepartmentId = Convert.ToInt32(Dr["fGPtbl_MU_DepartmentId"]);
                    UserFullname = Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";
                    if (Convert.ToString(Dr["fGPtbl_MU_MName"]).Trim() != "")
                    {
                        UserFullname += Convert.ToString(Dr["fGPtbl_MU_MName"]) + " ";
                    }
                    UserFullname += Convert.ToString(Dr["fGPtbl_MU_LName"]) + " ";
                    ActiveFlag = Convert.ToInt32(Dr["fGPtbl_MU_AccessId"]);
                    LoginNo = Convert.ToInt32(Dr["fGPtbl_MU_LoginNo"]);
                    UserEmailId = Convert.ToString(Dr["fGPtbl_MU_Email"]);
                    LoginNo += 1;
                }
                Dr.Close();

                NSICGP_VBClassLibrary.DBManager.CloseConnection();

                if (HttpContext.Current.Request.Cookies["OSUYULUKR8"] != null)
                {

                    userid_chek = HttpContext.Current.Request.Cookies["OSUYULUKR8"]["USERFULLNAME"].ToString();
                    if (userid_chek != UserFullname.ToString())
                    {
                        MessageFlage = 1;
                    }
                }

                if (Cnt == 0)
                {
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    lblMessage.Text = "INVALID User Name or Password !";
                    lblMessage.Visible = true;
                }
                else if (Cnt == 1)
                {
                    if (ActiveFlag == 0 && MessageFlage == 0)
                    {
                        aCookie = new HttpCookie("OSUYULUKR8");
                        aCookie.Values["USERID"] = Convert.ToString(UserId);
                        aCookie.Values["ROLEID"] = Convert.ToString(RoleId);
                        aCookie.Values["DEPARTMENTID"] = Convert.ToString(DepartmentId);
                        aCookie.Values["USERFULLNAME"] = UserFullname;
                        aCookie.Values["UserEmailId"] = UserEmailId;
                        aCookie.Expires = DateTime.Now.AddDays(1);
                        Response.Cookies.Add(aCookie);

                        Session["OSUYULUKR8-ERUYIRE"] = "GP";

                        objUser = new clsMasterUser();

                        //IP CAPTURE

                        string LoginIP = string.Empty, strHttpref = string.Empty;
                        try
                        {
                            LoginIP = Request.ServerVariables["REMOTE_ADDR"];
                            strHttpref = Request.ServerVariables["http_referer"];
                        }
                        catch (Exception ex)
                        {
                            ErrHandler.WriteError(ex.Message);
                        }


                        if (strHttpref.Length == 0)
                            LoginIP = "N.A.";
                        //IP CAPTURE

                        ErrorId = objUser.MasterUser_UpdateLoginNo(UserId, LoginNo, LoginIP);

                        if (RoleId == 6)
                        {

                            Response.Redirect("~/appInspectionAgency/Home.aspx", false);
                        }



                        else if (RoleId == 17)
                        {
                            Response.Redirect("~/appSCST/SCST-SUBSIDY-DETAILS.aspx", false);
                        }

                        else if (RoleId == 18)
                        {
                            Response.Redirect("~/appMIC/MIC_REPORT_DETAILS.aspx", false);
                        }

                        else if (RoleId == 19)
                        {
                            Response.Redirect("~/appCDCRITES/Ageing_DETAILS.aspx", false);
                        }

                        else if (RoleId == 10 || RoleId == 1 || RoleId == 2 || RoleId == 3 || RoleId == 4 || RoleId == 5 || RoleId == 8 || RoleId == 20 || RoleId == 21)
                        {
                            if (LoginNo == 1)
                            {

                            }
                            else
                            {
                                // ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Please logout user already login!!!!'); window.location ='~/appInspectionAgency/Home.aspx';", true);
                                Response.Redirect("./Home.aspx", false);
                            }
                        }
                    }

                    else if (ActiveFlag == 0 && MessageFlage == 1)
                    {
                        objUser = new clsMasterUser();

                        //IP CAPTURE

                        string LoginIP = string.Empty, strHttpref = string.Empty;
                        try
                        {
                            LoginIP = Request.ServerVariables["REMOTE_ADDR"];
                            strHttpref = Request.ServerVariables["http_referer"];
                        }
                        catch (Exception ex)
                        {
                            ErrHandler.WriteError(ex.Message);
                        }


                        if (strHttpref.Length == 0)
                            LoginIP = "N.A.";
                        //IP CAPTURE

                        //  ErrorId = objUser.MasterUser_UpdateLoginNo(UserId, LoginNo, LoginIP);
                        if (RoleId == 6)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('You are already logged in !!!!'); window.location ='~/appInspectionAgency/Home.aspx';", true); //'" + aCookie.Values["USERFULLNAME"].ToString() + "'
                                                                                                                                                                                                                      // Response.Redirect("~/appInspectionAgency/Home.aspx", false);
                        }
                        else if (RoleId == 1 || RoleId == 2 || RoleId == 3 || RoleId == 4 || RoleId == 5 || RoleId == 8 || RoleId == 20)
                        {
                            if (LoginNo == 1)
                            {

                            }
                            else
                            {

                                string strvalue = "You are already logged in  as  " + userid_chek.ToString() + "";

                                ShowMessageBox(strvalue);


                            }
                        }
                    }

                    else
                    {
                        txtUserName.Text = "";
                        txtPassword.Text = "";
                        lblMessage.Text = "This user account is DEACTIVATED !";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    txtUserName.Text = "";
                    txtPassword.Text = "";
                    lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";
                    lblMessage.Visible = true;
                }
                //    Login(txtUserName.Text);
                // FormsAuthentication.RedirectFromLoginPage(txtUserName.Text,true);
            }
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);

        }
    }


    //protected void btnSubmitFGP_Click(object sender, EventArgs e)
    //{

    //    try
    //    {
    //        objUser = new clsMasterUser();
    //        objMailer = new clsMasterMailer();
    //        Myds = new DataSet();
    //        int id;
    //        string FromMail, ccMail, bccMail, subject, bodyMail;
    //        ArrayList arl = new ArrayList();

    //        //TO FETCH MAILER
    //        Myds = objMailer.MasterMailer_Fetch_DataSet(6);

    //        arl = objUser.MasterUser_ForgotPassword(txtUserNameFGP.Text);
    //        id = Convert.ToInt32(arl[0]);
    //        MailTo = arl[1].ToString();

    //        string[] words = MailTo.Split('-');
    //        MailTo = words[0].ToString();
    //        string bran = words[1].ToString();


    //        FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
    //        ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
    //        bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
    //        subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
    //        bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";

    //        //string bodystr = bodyMail.Replace("$$password$$", arl[2].ToString());

    //        string bodystr = bodyMail.Replace("$$password$$", arl[2].ToString());
    //        bodystr = bodystr.Replace("$$username$$", txtUserNameFGP.Text);
    //        bodystr = bodystr.Replace("$$branch$$", bran);

    //        switch (id)
    //        {
    //            case 1://User not exist
    //                lblMsgFGP.Visible = true;
    //                lblMsgFGP.Text = "User not exist";
    //                txtUserNameFGP.Text = "";

    //                ModalPopupExtender1.Show();
    //                break;
    //            case 2://Password successfully changed
    //                sendmail(FromMail, MailTo, bccMail, ccMail, subject, bodystr);
    //                //sendmail(FromMail, "uttam.kumar@nagsoft.com", bccMail, ccMail, subject, bodystr);
    //                lblMessage.Visible = true;
    //                lblMessage.Text = "Password sent to your email";
    //                break;
    //            case 3://Error in operation
    //                lblMessage.Visible = true;
    //                lblMessage.Text = "Server is busy please try later";
    //                break;
    //            case 4://User is inactive
    //                lblMsgFGP.Visible = true;
    //                lblMsgFGP.Text = "This user account is DEACTIVATED !";
    //                txtUserNameFGP.Text = "";

    //                ModalPopupExtender1.Show();
    //                break;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }

    //}

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

    private void ShowMessageBox(string Message)
    {
        Label lblMessageBox = new Label();
        lblMessageBox.Text =
            "<script language='javascript'>" + Environment.NewLine +
            "window.alert('" + Message + "')</script>";
        Page.Controls.Add(lblMessageBox);
    }

}

