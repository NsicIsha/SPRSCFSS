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
using System.Data.SqlClient;
using PerformanceAndCreditRating.Class;
using System.Configuration;
using System.Web.Security;
using System.Text;
using System.Drawing;
using System.Text.RegularExpressions;
public partial class Forgot_Pwd : System.Web.UI.Page
{
    dalVariable objDal;
    DataSet Myds;
    clsMasterUser objUser;
    clsMasterMailer objMailer;
    int ErrorId = 0;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    SqlConnection con;

    string MailTo, MailFrom;

    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;

    int MessageFlage = 0;
    string userid_chek;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int successId;

    string UserEmailID = string.Empty;

    //#region(CHECK SESSION)
    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    if (Request.UrlReferrer != null)
    //    {
    //        if (ABMiscFunction.CheckUserSession())
    //        {
    //            if (ABMiscFunction.GetUserRole() != "8")
    //            {
    //                Response.Redirect("~/Logout.aspx");
    //            }
    //        }
    //        else
    //            Response.Redirect("~/Logout.aspx");
    //    }
    //    else
    //        Response.Redirect("~/Logout.aspx");
    //}
    //#endregion
    //public void FunRequest()
    //{
    //    DecryptEncrypt objDecEnc = new DecryptEncrypt();
    //    try
    //    {
    //        UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
    //        RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
    //        DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
    //        UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
    //        UserEmailID = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).Replace(" ", "+")).ToString();

    //    }
    //    catch
    //    {
    //        UserId = 0;
    //        RoleId = 0;
    //        DepartmentId = 0;
    //        UserFullName = "";
    //        //UserEmailID = "";
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        // FunRequest();

    }
    public DataSet Login_Fetch_DataSet(string UserName)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_login_Details_by_LoginName", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, -1).Value = UserName;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    public SqlDataReader MasterUser_CheckCredentials(string UserName)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MySqlComm = new SqlCommand("GPsp_Fetch_UserByName_Encrypted", con);
            con.Open();
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar, 200).Value = UserName;
            MyDr = MySqlComm.ExecuteReader();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return MyDr;
    }

    protected string GenerateRandomNumber()
    {
        string number = "";
        Random random = new Random();
        string strcount = "1";
        if (!string.IsNullOrEmpty(strcount))
        {
            for (int i = 0; i < Convert.ToInt32(strcount); i++)
            {
                int n = random.Next(2, 9999999);
                number += n.ToString("D6");
            }
        }
        return number;
    }

    public DataSet Fetch_OTPDetails_Count(int LoginId)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Otp_For_Fgt_pwd_OTP_Count", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = LoginId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public int Insert_Otp_WrongAttempt_For_Fgt_pwd(int LoginId, int OTP)
    {
        SqlCommand MySqlComm = new SqlCommand();


        int ErrorIdNew = 0;
        int UniqueId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_Otp_WrongAttempt_For_Fgt_pwd", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UsernId", SqlDbType.Int).Value = LoginId;
            MySqlComm.Parameters.Add("@OTP", SqlDbType.Int).Value = OTP;

            //MySqlComm.Parameters.Add("@UniqueId", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

            ErrorIdNew = MySqlComm.ExecuteNonQuery();

            // UniqueId = Convert.ToInt32(MySqlComm.Parameters["@UniqueId"].Value.ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }

        return ErrorIdNew;
    }

    public int Insert_OTPDetails(int LoginId, int OTP)
    {
        SqlCommand MySqlComm = new SqlCommand();


        int ErrorIdNew = 0;
        int UniqueId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_Otp_For_Fgt_pwd", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UsernId", SqlDbType.Int).Value = LoginId;
            MySqlComm.Parameters.Add("@OTP", SqlDbType.Int).Value = OTP;

            //MySqlComm.Parameters.Add("@UniqueId", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

            ErrorIdNew = MySqlComm.ExecuteNonQuery();

            // UniqueId = Convert.ToInt32(MySqlComm.Parameters["@UniqueId"].Value.ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }

        return ErrorIdNew;
    }
    public DataSet Fetch_OTPDetails(int LoginId)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Otp_For_Fgt_pwd", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = LoginId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string sCaptcha = this.txtCaptcha.Text.Trim();

        captcha1.ValidateCaptcha(sCaptcha);

        if (!captcha1.UserValidated)
        {
            lblMessage.Text = "Please enter valid Captcha.";
            lblMessage.Visible = true;
            return;
        }
        else
        {
            lblMessage.Visible = false;
            string isexist = null;
            string UserName = txtUserName.Text.Trim();
            string RegEmailId = txtRegEmailID.Text.Trim();
            int iUserId = 0;
            isexist = CheckUserName(UserName, RegEmailId);
            if (isexist == "Yes")
            {
                DataSet ds = new DataSet();
                ds = Login_Fetch_DataSet(txtUserName.Text.Trim());
                string randomnumber = "";
                randomnumber = GenerateRandomNumber();
                if (ds != null)
                {
                    iUserId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_MU_Id"].ToString());
                    DataSet dsCount = new DataSet();
                    dsCount = Fetch_OTPDetails_Count(iUserId);
                    int OtpCount = 0;

                    OtpCount = Convert.ToInt32(dsCount.Tables[0].Rows[0]["Counts"].ToString());

                    DataSet dsWrongotp = new DataSet();
                    dsWrongotp = Fetch_WrongOTPDetails_Count(iUserId);

                    int WrongOtpCount = 0;

                    string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
                    string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
                    string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
                    string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



                    WrongOtpCount = Convert.ToInt32(dsWrongotp.Tables[0].Rows[0]["Counts"].ToString());
                    if (WrongOtpCount < 3)
                    {
                        if (OtpCount < 3 || OtpCount == 111111)
                        {

                            int chk = Insert_OTPDetails(iUserId, Convert.ToInt32(randomnumber));


                            string strEmailBody = "";
                            string strSubject = "OTP for Password change request for online application";
                            string strToEmailAddress = ds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                            string strEmailAddressFrom = SmtpUser;// "no-reply@nsicnet.in";// ConfigurationSettings.AppSettings["UName"].ToString();
                            string strEmailDisplayFrom = "OTP to change password for Single Point Registration";
                            string strCC = null;
                            string strBCC = null;
                            strEmailBody = ClsTxtFileRead.MailContentsRead("ChangePwdMailorMsg.txt");
                            strEmailBody = strEmailBody.Replace("$$NAME$$", ds.Tables[0].Rows[0]["Name"].ToString());
                            strEmailBody = strEmailBody.Replace("$$OTP$$", randomnumber);
                            //strEmailBody = strEmailBody.Replace("$$UserId$$", ds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString());
                            // strEmailBody = strEmailBody.Replace("$$UserPassword$$", inpHide.Value);

                            SendMail(strEmailBody, strEmailAddressFrom, strEmailDisplayFrom, strToEmailAddress, strSubject, strCC, strBCC);

                            pwdcontinue.Visible = true;
                            forgetpwd.Visible = false;
                        }

                        else
                        {
                            ABMiscFunction.PopupMessage("You have exceeded maximum count limit of OTP generation. You can not generated more OTP today. Please try next day or contact Administrator", Page);
                            pwdcontinue.Visible = false;
                            forgetpwd.Visible = true;
                            txtUserName.Text = txtRegEmailID.Text = "";
                        }


                    }

                    else
                    {
                        ABMiscFunction.PopupMessage("You have entered wrong OTP for more than 3 times. You can not change password today. Please try next day or contact Administrator", Page);
                        pwdcontinue.Visible = false;
                        forgetpwd.Visible = true;
                        txtUserName.Text = txtRegEmailID.Text = "";
                    }
                }



            }
            else
            {
                ABMiscFunction.PopupMessage("Please enter valid UserId and Email-Id.", Page);
                pwdcontinue.Visible = false;
                forgetpwd.Visible = true;
                txtUserName.Text = txtRegEmailID.Text = "";
            }
        }

    }


    private bool ValidatePassword(string password, out string ErrorMessage) //, out string ErrorMessage
    {
        var input = password;
        ErrorMessage = string.Empty;

        //if (string.IsNullOrWhiteSpace(input))
        //{
        //    throw new Exception("Password should not be empty");
        //}

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower case letter.";
            return false;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one upper case letter.";
            return false;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            ErrorMessage = "Password should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasNumber.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one numeric value.";
            return false;
        }

        else if (!hasSymbols.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one special case character.";
            return false;
        }
        else
        {
            return true;
        }
    }


    #region(CONTINUE BUTTON CLICK EVENT)
    protected void btnContinue_Click(object sender, EventArgs e)
    {

        if (Convert.ToString(Session["Code"]).ToLower() != txtCaptchaOTP.Text.Trim().ToLower())
        {
            Session["Code"] = null;
            txtCaptcha.Text = "";
            divCaptcha.Visible = true;
            txtCaptcha.Focus();
            lblCaptcha.Text = "Incorrect verification code. Please try again.";
            lblCaptcha.ForeColor = Color.Red;
            return;
        }
         
            FailureText.Text = "";

            try
            {
                string strNewPassword = txtNewPassword.Value.Trim();

                string strNewPasswordVal = txtNewPassword.Value.Trim() + "SPRS!@#$%^&*";// + "ADDE*[!@#$%^&*";

                string sPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";


                bool checkstatus = false;
                string strClearFormat = strNewPassword;// "";// hdn_Clr_Pwd.Value.Trim();

                // checkstatus = System.Text.RegularExpressions.Regex.IsMatch(strNewPasswordVal, sPattern);

                string ErrorMessage;
                // checkstatus = ValidatePassword(strNewPasswordVal, "");
                if (ValidatePassword(strNewPasswordVal, out ErrorMessage) == false)
                {
                    FailureText.Text = ErrorMessage;
                    return;
                }
                else
                {
                    checkstatus = true;
                }
                DataSet dss = new DataSet();
                dss = CheckUserNameNew(txtUserName.Text, txtRegEmailID.Text);
                DataSet dsOtp = new DataSet();
                if (checkstatus == true)
                {
                    Int32 userId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_MU_Id"].ToString());// ABMiscFunction.GetUserID();

                    //string UserId = txtUserName.Text.Trim();
                    //string Password = strNewPassword.Substring(0, 32);

                    dsOtp = Fetch_OTPDetails(userId);
                    int iOtp = 0;
                    iOtp = Convert.ToInt32(dsOtp.Tables[0].Rows[0]["OTP"].ToString());

                    if (iOtp != Convert.ToInt32(txtOTP.Value.ToString()))
                    {
                        int chk = Insert_Otp_WrongAttempt_For_Fgt_pwd(userId, Convert.ToInt32(txtOTP.Value.ToString()));

                        FailureText.Text = "Please enter correct OTP received on your email.";

                        DataSet dsWrongotp = new DataSet();
                        dsWrongotp = Fetch_WrongOTPDetails_Count(userId);

                        int OtpCount = 0;

                        OtpCount = Convert.ToInt32(dsWrongotp.Tables[0].Rows[0]["Counts"].ToString());
                        if (OtpCount > 3)
                        {
                            Response.Redirect("appAdministrator/Login_New.aspx");
                        }

                    }
                    else
                    {

                        DataSet dsWrongotp = new DataSet();
                        dsWrongotp = Fetch_WrongOTPDetails_Count(userId);

                        int OtpCount = 0;

                        //OtpCount = Convert.ToInt32(dsWrongotp.Tables[0].Rows[0]["Counts"].ToString());
                        //if (OtpCount > 3)
                        //{
                        //    Response.Redirect("appAdministrator/Login_New.aspx");
                        //}


                        int row = RecoverPassword(userId, strNewPassword, strClearFormat, out checkstatus);
                        DataSet ds = new DataSet();

                    string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
                    string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
                    string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
                    string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];

                    if (checkstatus == true)
                        {
                            ds = Login_Fetch_DataSet(txtUserName.Text.Trim());
                            if (ds != null)
                            {
                                string strEmailBody = "";
                                string strSubject = "Password change request for online application done successfully";
                                string strToEmailAddress = ds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                            string strEmailAddressFrom = SmtpUser;// "no-reply@nsicnet.in";// ConfigurationSettings.AppSettings["UName"].ToString();
                                string strEmailDisplayFrom = "Password change request for online application done successfully";
                                string strCC = null;
                                string strBCC = null;
                                strEmailBody = ClsTxtFileRead.MailContentsRead("ChangePwdMailor.txt");
                                strEmailBody = strEmailBody.Replace("$$NAME$$", ds.Tables[0].Rows[0]["Name"].ToString());
                                strEmailBody = strEmailBody.Replace("$$UserId$$", ds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString());
                                strEmailBody = strEmailBody.Replace("$$UserPassword$$", inpHide.Value);

                                SendMail(strEmailBody, strEmailAddressFrom, strEmailDisplayFrom, strToEmailAddress, strSubject, strCC, strBCC);
                            }
                            ABMiscFunction.PopupMessage("Your password has been changed successfully. Relogin Please...", Page);
                            //pwdcontinue.Visible = false;
                            //forgetpwd.Visible = true;
                            ClearData();
                            Response.Redirect("appAdministrator/Login_New.aspx");
                        }
                    }
                }
                else
                {
                    //FailureText.Text = "Password format is incorrect.Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=]";
                    FailureText.Text = "Password must be between 6 and 20 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.";
                }
            }
            catch (Exception ex)
            {

            }
        
    }
    #endregion



    public DataSet Fetch_WrongOTPDetails_Count(int LoginId)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_WrongOtp_For_Fgt_pwd_OTP_Count", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = LoginId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    #region(CLEAR DATA)
    void ClearData()
    {
        txtUserName.Text = txtRegEmailID.Text = txtNewPassword.Value = txtConfirmNewPassword.Value = "";
    }
    #endregion
    public void SendMail(string strEmailBody, string strEmailAddressFrom, string strEmailDisplayFrom, string strToEmailAddress, string strSubject, string strCC, string strBCC)
    {
        try
        {
            string MailBody = "";
            string MailSubject = "";
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];

            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



            /*MailTo = lblto.Text.ToString();
            MailFrom = lblfrom.Text.ToString();*/
            MailTo = strToEmailAddress.ToString();
            MailFrom = strEmailAddressFrom.ToString();
            // string Bodyhtml = txtSub.Text;

            MailSubject = strSubject;

            MailBody = ((strEmailBody.Replace(Environment.NewLine, "<br />")));

            MailMessage message = new MailMessage(MailFrom, MailTo, strSubject, strEmailBody);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            //INSERT INTO SENT MAILER (MAY BE SOME INTEGER VALUE ARE WRONG BUT MAIL BODY COULD GUIDE)
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 3, 1, MailSubject, MailBody, 3);


        }
        catch (Exception ex)
        {

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


    public DataSet CheckUserNameNew(string UserName, string EmailId)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_Sp_ForgetPwd_CheckLoginUserNew", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserName", SqlDbType.VarChar, 1000).Value = UserName;
            MyAdp.SelectCommand.Parameters.Add("@EmailId", SqlDbType.VarChar, 1000).Value = EmailId;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public string CheckUserName(string UserName, string EmailId)
    {
        string isexist = null;
        try
        {
            MySqlComm = new SqlCommand("GP_Sp_ForgetPwd_CheckLoginUser", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UserName", SqlDbType.VarChar, 1000).Value = UserName;
            MySqlComm.Parameters.Add("@EmailId", SqlDbType.VarChar, 1000).Value = EmailId;
            SqlParameter outError = new SqlParameter("@IsExist", SqlDbType.VarChar, 1000);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            isexist = Convert.ToString(outError.Value);
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
        return isexist;
    }

    public int RecoverPassword(int UserId, string password, string Clearpassword, out bool checkstatus)
    {
        int iRowAffected = 0;
        bool status = false;
        try
        {
            MySqlComm = new SqlCommand("Gp_Sp_RecoverPassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@NewPassword", SqlDbType.VarChar, 5000).Value = password;
            MySqlComm.Parameters.Add("@Clearpassword", SqlDbType.VarChar, 5000).Value = Clearpassword;
            SqlParameter outError = new SqlParameter("@Status", SqlDbType.Bit);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            status = Convert.ToBoolean(outError.Value);
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
        checkstatus = status;
        return iRowAffected;
    }
}