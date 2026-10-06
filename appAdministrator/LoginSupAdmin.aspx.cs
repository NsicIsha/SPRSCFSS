using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;//M
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


using System.Web.Security;
using System.Text;
using System.Drawing;
public partial class appAdministrator_LoginSupAdmin : System.Web.UI.Page
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
    string seedValue = string.Empty;
    int Index = 0;

    #region(GENERATECOUPON)
    private string GenerateCoupon(int length)
    {
        Random random = new Random();
        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(characters[random.Next(characters.Length)]);
        }
        if (!IsPostBack)
        {
            Session["SeedValue"] = result.ToString();
        }
        Session["NewSeedValue"] = result.ToString();
        return result.ToString();
    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        int num = random.Next(min, max);
        if (!IsPostBack)
        {
            Session["SeedValue"] = num;
        }
        Session["NewSeedValue"] = num;
        return num;
    }


    #endregion

    protected void Page_Load(object sender, EventArgs e)

    {
        string url = Request.Url.AbsoluteUri.ToString();
        Index = url.IndexOf('?');
        if (Index > 0)
        {
            Response.Redirect("ErrorPages/FileNotFound.aspx");
        }

        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1d);
        Response.Expires = -1500;
        Response.CacheControl = "no-cache";
        Response.AppendHeader("Cache-Control", "no-store");
        Response.AppendHeader("Cache-Control", "must-revalidate");
        Response.AppendHeader("Cache-Control", "pre-check=0");
        Response.AppendHeader("Cache-Control", "postcheck=0");
        Response.AppendHeader("Cache-Control", "max-age=0");
        Response.AppendHeader("Cache-Control", "s-maxage=0");
        Response.AppendHeader("Pragma", "no-cache");
        Response.Cache.SetNoStore();
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoServerCaching();

        seedValue = GenerateCoupon(16);

      btnSubmit.Attributes.Add("onclick", "javascript:ValidateSeed('" + seedValue + "');");

        txtUserName.Focus();

        ////try
        ////{
        ////    lblMessage.Visible = false;
        ////    //lblMsgFGP.Visible = false;

        ////    txtUserName.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        ////    txtPassword.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        ////    // txtUserNameFGP.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmitFGP');");
        ////    btnSubmit.Attributes.Add("onclick", "checkVersion();");
        ////    if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
        ////    {
        ////        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunFocusTextbox('ctl00_ContentPlaceHolder1_txtUserName');", true);
        ////    }

        ////    if (!IsPostBack)
        ////    {
        ////        try
        ////        {
        ////            int i;
        ////            HttpCookie acookie = new HttpCookie("OSUYULUKR8");
        ////            int limit = (Request.Cookies.Count - 1);
        ////            for (i = 0; (i <= limit); i++)
        ////            {
        ////                acookie = Request.Cookies[i];
        ////                acookie.Expires = DateTime.Now.AddHours(-1);
        ////                Response.Cookies.Add(acookie);
        ////            }
        ////            //Disable back in browser -----------------
        ////            Session.Abandon();
        ////            Application.RemoveAll();
        ////            Response.Buffer = false;
        ////            Response.ExpiresAbsolute = DateTime.Now.AddHours(-1);
        ////            Response.Expires = 0;
        ////            Response.CacheControl = "no-cache";
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        ////        }
        ////    }
        ////}
        ////catch (Exception ex)
        ////{
        ////    ErrHandler.WriteError(ex.Message);
        ////}
    }


    public DataSet Login_Fetch_DataSet(string UserName)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);


            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_login_Details_by_LoginName_Audit", con);
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


    public int Insert_logDetails(int LoginId, string IpAddress, string MachineName, string RoleName, string LoginStatus, string UserName, string ABUserId)
    {
        SqlCommand MySqlComm = new SqlCommand();


        int ErrorIdNew = 0;
        int UniqueId = 0;
        try
        {
            MySqlComm = new SqlCommand("Sp_SPRS_CheckLoginUser_STATUS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;


            MySqlComm.Parameters.Add("@LoginId", SqlDbType.VarChar, 256).Value = LoginId;
            MySqlComm.Parameters.Add("@IpAddress", SqlDbType.VarChar, 256).Value = IpAddress;
            MySqlComm.Parameters.Add("@MachineName", SqlDbType.VarChar, 256).Value = MachineName;
            MySqlComm.Parameters.Add("@RoleName", SqlDbType.VarChar, 256).Value = RoleName;
            MySqlComm.Parameters.Add("@LoginStatus", SqlDbType.VarChar, 256).Value = LoginStatus;
            MySqlComm.Parameters.Add("@UserName", SqlDbType.VarChar, 256).Value = UserName;
            MySqlComm.Parameters.Add("@ABUserId", SqlDbType.VarChar, 256).Value = ABUserId;

            MySqlComm.Parameters.Add("@UniqueId", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;

            ErrorIdNew = MySqlComm.ExecuteNonQuery();

            UniqueId = Convert.ToInt32(MySqlComm.Parameters["@UniqueId"].Value.ToString());
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

        return UniqueId;
    }


    public SqlDataReader MasterUser_CheckCredentials_Audit(string UserName, string Password)
    {
        try
        {
            //DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptedPwd = Password.ToString().Trim();

            MySqlComm = new SqlCommand("GPsp_Fetch_UserByName_Audit", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MU_Name", SqlDbType.VarChar, 200).Value = UserName;
            MySqlComm.Parameters.Add("@fGPtbl_MU_Pwd", SqlDbType.VarChar, 2000).Value = EncryptedPwd;

            MyDr = MySqlComm.ExecuteReader();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        return MyDr;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        //try
        //{

        //string sCaptcha = this.txtCaptcha.Text.Trim();

        //captcha1.ValidateCaptcha(sCaptcha);

        //if (!captcha1.UserValidated)
        //{
        //    lblMessage.Text = "Please enter valid Captcha.";
        //    lblMessage.Visible = true;
        //    return;
        //}
        // aCookie = Request.Cookies;  // HttpContext.Current.Request.Cookies;

        //if (Convert.ToString(Session["Code"]).ToLower() != txtCaptcha.Text.Trim().ToLower())
        //{
        //    Session["Code"] = null;
        //    txtCaptcha.Text = "";
        //    divCaptcha.Visible = true;
        //    txtCaptcha.Focus();
        //    lblCaptcha.Text = "Incorrect verification code. Please try again.";
        //    lblCaptcha.ForeColor = Color.Red;
        //    return;
        //}

        int UserId = 0;
        int RoleId = 0;
        string UserFullname = "";
        int DepartmentId = 0;
        int ActiveFlag = 0;
        int LoginNo = 0;
        string UserEmailId = "";

        string ipAddress = Request.UserHostAddress;


        //string ipAddress = Request.UserHostAddress;
        //string strPassword = txtPassword.Text.Trim();
        //string saltedHash = hdnSetPassword.Value.Trim();
        //string Username = txtUserName.Text.ToString().Trim();
        //string Password = txtPassword.Text.ToString().Trim();


        //string ss= HiddenField1.Value.Trim();

        //// string textPassword = ABUserBLL.GetPasswordByLoginName(txtUserName.Text.Trim());
        //DataSet dslogin = new DataSet();
        //dslogin = Login_Fetch_DataSet(Username);
        //string encryptedpasswaord = "";
        //string textPassword = Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_EncryptedPwd"].ToString());
        //string username = Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString());

        //int Cnt = 0;
        //HttpCookie aCookie;

        //string svalue = Convert.ToString(Session["SeedValue"]);
        //string NewSeedValue = Convert.ToString(Session["NewSeedValue"]);
        //Session["SeedValue"] = Session["NewSeedValue"].ToString();
        //string EncryptUserId = string.Empty, EncryptRoleId = string.Empty, EncryptDepartmentId = string.Empty;
        //DecryptEncrypt objDecEnc = new DecryptEncrypt();
        //string[] iUserData = new string[3];

        //string newhashh = ABMiscFunction.GenerateSHA512String(ss + svalue);


        Session["Code"] = null;
        //string saltedHash = "5f3b36674475d8c0c99d0cc37bcbd6f82d59312c92955be34922f5b98e29a732";// hdnSetPassword.Value.Trim();

        string saltedHash =hdnSetPassword.Value.Trim();


        int iRoleId = 0;
        int iLoginId = 0;
        string iMU_FName = "";
        int iDepartment = 0;
        string iUserFullname = "";
        string iUserMlname = "";
        string iUserLlname = "";
        int ifGPtbl_MU_AccessId = 0;
        int ifGPtbl_MU_LoginNo = 0;
        string ifGPtbl_MU_Email = "";
        int ifGPtbl_MU_EncryptedPwdStatus = 0;

        DataSet dslogin1 = new DataSet();
        dslogin1 = Login_Fetch_DataSet(txtUserName.Text);
        if (dslogin1.Tables[0].Rows.Count != 0)
        {
            iRoleId = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_RoleId"].ToString());
            iLoginId = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_Id"].ToString());
            iMU_FName = Convert.ToString(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_FName"].ToString());
            iDepartment = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_DepartmentId"].ToString());
            iUserMlname = Convert.ToString(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_MName"].ToString());
            iUserLlname = Convert.ToString(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_LName"].ToString());
            ifGPtbl_MU_AccessId = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_AccessId"].ToString());
            ifGPtbl_MU_LoginNo = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_LoginNo"].ToString());
            ifGPtbl_MU_Email = Convert.ToString(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString());
            ifGPtbl_MU_EncryptedPwdStatus = Convert.ToInt32(dslogin1.Tables[0].Rows[0]["fGPtbl_MU_EncryptedPwdStatus"].ToString());

        }
        else
        {
            iRoleId = 0;
            iLoginId = 0;


            lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";
            lblMessage.Visible = true;
            int Chk = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Failure", txtUserName.Text, "");

            txtUserName.Text = "";
            txtPassword.Value = "";

            return;
        }






        if (txtPassword.Value != "" && txtPassword.Value.Equals(saltedHash))
        {
            // string ipAddress = Request.UserHostAddress;
            string strPassword = txtPassword.Value.Trim();
            string dbPassword = "";
            DataSet dslogin = new DataSet();
            dslogin = Login_Fetch_DataSet(txtUserName.Text);
            string encryptedpasswaord = "";

            //Hash of Test@1234
            //dbPassword = "849f1575ccfbf3a4d6cf00e6c5641b7fd4da2ed3e212c2d79ba9161a5a432ff0";// "5f3b36674475d8c0c99d0cc37bcbd6f82d59312c92955be34922f5b98e29a732";// "849f1575ccfbf3a4d6cf00e6c5641b7fd4da2ed3e212c2d79ba9161a5a432ff0";// Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_EncryptedPwd"].ToString());

            //Keep hash of first time generated hash pass
            dbPassword = "bf639a82f93635342cd42e997adb88ef384a1351d78429099356ac59d0130e82";// "aac08dc710e006cb6708ad256812aae886bf658c64ad26aa47cfd0f73b2451bf";// "83b1f2844379244e4c56925166d82fd39dc283d7ea06e72baa02886171d132c6";// "5f3b36674475d8c0c99d0cc37bcbd6f82d59312c92955be34922f5b98e29a732";


            string strHdnpwd = hdnSetPassword.Value;// "5f3b36674475d8c0c99d0cc37bcbd6f82d59312c92955be34922f5b98e29a732";// hdnSetPassword.Value;

            string strPasswordTxtBox = txtPassword.Value;



            string sValue = Convert.ToString(Session["SeedValue"]);
            Session["SeedValue"] = Session["NewSeedValue"].ToString();
            System.Data.SqlClient.SqlDataReader Dr;
            objUser = new clsMasterUser();

            string newhash = ABMiscFunction.EncryptPasswordSha(dbPassword + sValue);

            if (strPassword == newhash)
            {
                Dr = MasterUser_CheckCredentials_Audit(txtUserName.Text, dbPassword); // objUser.MasterUser_CheckCredentials(txtUserName.Text, dbPassword);
                //iUserData = oDcUserBLL.CheckLogin((AntiXss.HtmlEncode(txtUserName.Text.Trim())), dbPassword, MiscFunction.GetIpAndMachine()[0], MiscFunction.GetIpAndMachine()[1]);
                // Session["GetAllValue"] = iUserData;
            }
            else
            {
                Dr = MasterUser_CheckCredentials_Audit(txtUserName.Text, dbPassword); //objUser.MasterUser_CheckCredentials(txtUserName.Text, strPassword);
                // iUserData = oDcUserBLL.CheckLogin(AntiXss.HtmlEncode(txtUserName.Text.Trim()), strPassword, "", "");
            }

            string EncryptUserId = string.Empty, EncryptRoleId = string.Empty, EncryptDepartmentId = string.Empty;

            string[] iUserData = new string[4];
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            int Cnt = 0;
            HttpCookie aCookie;


            ////string newhash = ABMiscFunction.GenerateSHA512String(textPassword + svalue);

            ////System.Data.SqlClient.SqlDataReader Dr;
            ////objUser = new clsMasterUser();
            ////if (saltedHash == newhashh)
            ////{               
            ////    Dr = objUser.MasterUser_CheckCredentials(Username, textPassword);
            ////}
            ////else
            ////{
            ////    //Dr = objUser.MasterUser_CheckCredentials(Username, saltedHash + "d2f*jk#g&h");
            ////    Dr = objUser.MasterUser_CheckCredentials(Username, textPassword);
            ////    return;
            ////}
            //if (saltedHash == newhash)
            //{

            // iUserData = ABUserBLL.CheckLogin(txtUserName.Text.Trim(), textPassword, System.Net.Dns.GetHostAddresses(ipAddress)[0].ToString(), "");

            //objUser = new clsMasterUser();
            //System.Data.SqlClient.SqlDataReader Dr = objUser.MasterUser_CheckCredentials(Username, Password);

            //strHdnpwd
            if (String.Equals(newhash, strPassword )) //dbPassword
            {

                //    while (Dr.Read())
                //{
                iUserData[0] = Convert.ToInt32(iRoleId).ToString();  //Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]).ToString();
                iUserData[1] = Convert.ToInt32(iLoginId).ToString(); //Convert.ToInt32(Dr["fGPtbl_MU_Id"]).ToString();
                iUserData[2] = Convert.ToString(iMU_FName) + " ";   //Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";
                iUserData[3] = Convert.ToString(txtUserName.Text);

                int UniqueId = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Success", txtUserName.Text, "");

                Session["UniqueId"] = UniqueId;

                FormsAuthenticationTicket tkt;
                string cookiestr;
                HttpCookie ck;
                Session["LoginId"] = txtUserName.Text;
                tkt = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now,
                    DateTime.Now.AddMinutes(60), false, iUserData[0].ToString() + ";" + iUserData[1].ToString() + ";" + iUserData[2].ToString() + ";" + iUserData[3].ToString() + ";" + iUserData[3].ToString(), FormsAuthentication.FormsCookiePath);

                cookiestr = FormsAuthentication.Encrypt(tkt);
                ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
                ck.Path = FormsAuthentication.FormsCookiePath;
                Response.Cookies.Add(ck);
                ck.HttpOnly = true;
                string guid = Guid.NewGuid().ToString();
                Session["AuthToken"] = guid;
                // now create a new cookie with this guid value
                HttpCookie authcookie = new HttpCookie("AuthToken", guid);
                authcookie.Path = "/ab";
                // Session["UniqueId"] = iUserData[4];

                Response.Cookies.Add(authcookie);
                authcookie.HttpOnly = true;


                Cnt += 1;

                iUserData[0] = Convert.ToInt32(iRoleId).ToString();  //Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]).ToString();
                iUserData[1] = Convert.ToInt32(iLoginId).ToString(); //Convert.ToInt32(Dr["fGPtbl_MU_Id"]).ToString();
                iUserData[2] = Convert.ToString(iMU_FName) + " ";   //Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";
                iUserData[3] = Convert.ToString(txtUserName.Text);

                UserId = Convert.ToInt32(iLoginId);
                RoleId = Convert.ToInt32(iRoleId);
                DepartmentId = Convert.ToInt32(iDepartment);
                UserFullname = Convert.ToString(iMU_FName) + " ";

                //UserId = Convert.ToInt32(Dr["fGPtbl_MU_Id"]);
                //RoleId = Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]);
                //DepartmentId = Convert.ToInt32(Dr["fGPtbl_MU_DepartmentId"]);
                //UserFullname = Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";

                EncryptUserId = objDecEnc.Encrypt(UserId.ToString());
                EncryptRoleId = objDecEnc.Encrypt(RoleId.ToString());
                EncryptDepartmentId = objDecEnc.Encrypt(DepartmentId.ToString());

                if (Convert.ToString(iUserMlname).Trim() != "")
                {
                    UserFullname += Convert.ToString(iUserMlname) + " ";
                }
                UserFullname += Convert.ToString(iUserLlname) + " ";



                ActiveFlag = Convert.ToInt32(ifGPtbl_MU_AccessId);
                LoginNo = Convert.ToInt32(ifGPtbl_MU_LoginNo);
                UserEmailId = Convert.ToString(ifGPtbl_MU_Email);
                LoginNo += 1;
            }
            //Dr.Close();

            NSICGP_VBClassLibrary.DBManager.CloseConnection();
            MessageFlage = 0;
            //if (HttpContext.Current.Request.Cookies["OSUYULUKR8"] != null)
            //    {
            //        userid_chek = HttpContext.Current.Request.Cookies["OSUYULUKR8"]["USERFULLNAME"].ToString();
            //        if (userid_chek != UserFullname.ToString())
            //        {
            //        //MessageFlage = 1;
            //        MessageFlage = 0;
            //    }
            // }

            if (Cnt == 0)
            {
                int Chk = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Failure", txtUserName.Text, "");

                txtUserName.Text = "";
                txtPassword.Value = "";
                //  lblMessage.Text = "INVALID User Name or Password !";
                lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";

                lblMessage.Visible = true;
            }
            else if (Cnt == 1)
            {
                if (ActiveFlag == 0 && MessageFlage == 0)
                {
                    aCookie = new HttpCookie("OSUYULUKR8");
                    aCookie.Values["USERID"] = Convert.ToString(EncryptUserId);
                    aCookie.Values["ROLEID"] = Convert.ToString(EncryptRoleId);
                    aCookie.Values["DEPARTMENTID"] = Convert.ToString(EncryptDepartmentId);
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

                            Session["ifGPtbl_MU_EncryptedPwdStatus"] = ifGPtbl_MU_EncryptedPwdStatus;

                            Response.Redirect("./Home.aspx", false);


                            //if (ifGPtbl_MU_EncryptedPwdStatus==0)
                            //{
                            //    Response.Redirect("Forgot_Pwd.aspx", false);  
                            //}
                            //else
                            //{
                            //    Response.Redirect("./Home.aspx", false);
                            //}

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
                    }
                    else if (RoleId == 1 || RoleId == 2 || RoleId == 3 || RoleId == 4 || RoleId == 5 || RoleId == 8 || RoleId == 20)
                    {
                        if (LoginNo == 1)
                        {
                        }
                        else
                        {
                            string strvalue = "You are already logged in  as  " + userid_chek.ToString() + "";
                            // ShowMessageBox(strvalue);
                        }
                    }
                }
                else
                {

                    // lblMessage.Text = "This user account is DEACTIVATED !";
                    lblMessage.Text = "This user account is DEACTIVATED, Please contact site ADMINISTRATOR !";

                    lblMessage.Visible = true;

                    int Chk = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "User Account is DEACTIVATED", txtUserName.Text, "");

                    txtUserName.Text = "";
                    txtPassword.Value = "";

                }
            }
        }
        else
        {

            lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";
            lblMessage.Visible = true;
            int Chk = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Failure", txtUserName.Text, "");

            txtUserName.Text = "";
            txtPassword.Value = "";

        }
        //    Login(txtUserName.Text);
        // FormsAuthentication.RedirectFromLoginPage(txtUserName.Text,true);
        // }



    }
    //else
    //{

    //     iUserData = ABUserBLL.CheckLogin(txtUserName.Text.Trim(), saltedHash + "d2f*jk#g&h", System.Net.Dns.GetHostAddresses(ipAddress)[0].ToString(), "");

    //}
    // }

    //    string Username = txtUserName.Text.ToString().Trim();
    //string Password = txtPassword.Text.ToString().Trim();
    //hdValue.Value = Helper.ComputeHash(txtPassword.Text, "SHA512", null);
    //txtPassword.Text = hdValue.Value;
    //string sCaptcha = this.txtCaptcha.Text.Trim();

    //captcha1.ValidateCaptcha(sCaptcha);
    //if (!captcha1.UserValidated)
    //{
    //    lblMessage.Text = "Please enter valid Captcha.";
    //    lblMessage.Visible = true;
    //    return;
    //}
    //int Cnt = 0;
    //HttpCookie aCookie;

    //DataSet dslogin = new DataSet();
    //dslogin = Login_Fetch_DataSet(Username);
    //string encryptedpasswaord = "";
    //encryptedpasswaord = Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_EncryptedPwd"].ToString());
    //string username = Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString());


    // bool flag = Helper.VerifyHash(txtPassword.Text, "SHA512", encryptedpasswaord);
    // flag = true;

    //string EncryptUserId = string.Empty, EncryptRoleId = string.Empty, EncryptDepartmentId = string.Empty;
    //DecryptEncrypt objDecEnc = new DecryptEncrypt();

    // string[] iUserData = new string[3];


    //EncryptFormtype = objDecEnc.Encrypt("3");
    //EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());

    ////if (flag == true)
    ////{
    ////    ////        objUser = new clsMasterUser();
    ////    ////        System.Data.SqlClient.SqlDataReader Dr = objUser.MasterUser_CheckCredentials(Username, Password);

    ////    ////        while (Dr.Read())
    ////    ////        {
    ////    ////            iUserData[0] = Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]).ToString(); 
    ////    ////            iUserData[1] = Convert.ToInt32(Dr["fGPtbl_MU_Id"]).ToString();
    ////    ////            iUserData[2]= Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";

    ////    ////            FormsAuthenticationTicket tkt;
    ////    ////            string cookiestr;
    ////    ////            HttpCookie ck;
    ////    ////            Session["LoginId"] = txtUserName.Text;
    ////    ////            tkt = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now,
    ////    ////                DateTime.Now.AddMinutes(60), false, iUserData[0].ToString() + ";" + iUserData[1].ToString() + ";" + iUserData[2].ToString() + ";" + iUserData[2].ToString() + ";" + iUserData[2].ToString(), FormsAuthentication.FormsCookiePath);

    ////    ////            cookiestr = FormsAuthentication.Encrypt(tkt);
    ////    ////            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
    ////    ////            ck.Path = FormsAuthentication.FormsCookiePath;
    ////    ////            Response.Cookies.Add(ck);
    ////    ////            ck.HttpOnly = true;
    ////    ////            string guid = Guid.NewGuid().ToString();
    ////    ////            Session["AuthToken"] = guid;
    ////    ////            // now create a new cookie with this guid value
    ////    ////            HttpCookie authcookie = new HttpCookie("AuthToken", guid);
    ////    ////            authcookie.Path = "/ab";
    ////    ////           // Session["UniqueId"] = iUserData[4];

    ////    ////            Response.Cookies.Add(authcookie);
    ////    ////            authcookie.HttpOnly = true;


    ////    ////            Cnt += 1;
    ////    ////            UserId = Convert.ToInt32(Dr["fGPtbl_MU_Id"]);
    ////    ////            RoleId = Convert.ToInt32(Dr["fGPtbl_MU_RoleId"]);
    ////    ////            DepartmentId = Convert.ToInt32(Dr["fGPtbl_MU_DepartmentId"]);
    ////    ////            UserFullname = Convert.ToString(Dr["fGPtbl_MU_FName"]) + " ";

    ////    ////            EncryptUserId = objDecEnc.Encrypt(UserId.ToString());
    ////    ////            EncryptRoleId = objDecEnc.Encrypt(RoleId.ToString());
    ////    ////            EncryptDepartmentId = objDecEnc.Encrypt(DepartmentId.ToString());

    ////    ////            if (Convert.ToString(Dr["fGPtbl_MU_MName"]).Trim() != "")
    ////    ////            {
    ////    ////                UserFullname += Convert.ToString(Dr["fGPtbl_MU_MName"]) + " ";
    ////    ////            }
    ////    ////            UserFullname += Convert.ToString(Dr["fGPtbl_MU_LName"]) + " ";
    ////    ////            ActiveFlag = Convert.ToInt32(Dr["fGPtbl_MU_AccessId"]);
    ////    ////            LoginNo = Convert.ToInt32(Dr["fGPtbl_MU_LoginNo"]);
    ////    ////            UserEmailId = Convert.ToString(Dr["fGPtbl_MU_Email"]);
    ////    ////            LoginNo += 1;
    ////    ////        }
    ////    ////        Dr.Close();

    ////    ////        NSICGP_VBClassLibrary.DBManager.CloseConnection();

    ////    ////        if (HttpContext.Current.Request.Cookies["OSUYULUKR8"] != null)
    ////    ////        {
    ////    ////            userid_chek = HttpContext.Current.Request.Cookies["OSUYULUKR8"]["USERFULLNAME"].ToString();
    ////    ////            if (userid_chek != UserFullname.ToString())
    ////    ////            {
    ////    ////                MessageFlage = 1;
    ////    ////            }
    ////    ////        }

    ////    ////        if (Cnt == 0)
    ////    ////        {
    ////    ////            txtUserName.Text = "";
    ////    ////            txtPassword.Text = "";
    ////    ////            lblMessage.Text = "INVALID User Name or Password !";
    ////    ////            lblMessage.Visible = true;
    ////    ////        }
    ////    ////        else if (Cnt == 1)
    ////    ////        {
    ////    ////            if (ActiveFlag == 0 && MessageFlage == 0)
    ////    ////            {
    ////    ////                aCookie = new HttpCookie("OSUYULUKR8");
    ////    ////                aCookie.Values["USERID"] = Convert.ToString(EncryptUserId);
    ////    ////                aCookie.Values["ROLEID"] = Convert.ToString(EncryptRoleId);
    ////    ////                aCookie.Values["DEPARTMENTID"] = Convert.ToString(EncryptDepartmentId);
    ////    ////                aCookie.Values["USERFULLNAME"] = UserFullname;
    ////    ////                aCookie.Values["UserEmailId"] = UserEmailId;
    ////    ////                aCookie.Expires = DateTime.Now.AddDays(1);
    ////    ////                Response.Cookies.Add(aCookie);
    ////    ////                Session["OSUYULUKR8-ERUYIRE"] = "GP";
    ////    ////                objUser = new clsMasterUser();
    ////    ////                //IP CAPTURE
    ////    ////                string LoginIP = string.Empty, strHttpref = string.Empty;
    ////    ////                try
    ////    ////                {
    ////    ////                    LoginIP = Request.ServerVariables["REMOTE_ADDR"];
    ////    ////                    strHttpref = Request.ServerVariables["http_referer"];
    ////    ////                }
    ////    ////                catch (Exception ex)
    ////    ////                {
    ////    ////                    ErrHandler.WriteError(ex.Message);
    ////    ////                }
    ////    ////                if (strHttpref.Length == 0)
    ////    ////                    LoginIP = "N.A.";
    ////    ////                //IP CAPTURE

    ////    ////                ErrorId = objUser.MasterUser_UpdateLoginNo(UserId, LoginNo, LoginIP);

    ////    ////                if (RoleId == 6)
    ////    ////                {
    ////    ////                    Response.Redirect("~/appInspectionAgency/Home.aspx", false);
    ////    ////                }
    ////    ////                else if (RoleId == 17)
    ////    ////                {
    ////    ////                    Response.Redirect("~/appSCST/SCST-SUBSIDY-DETAILS.aspx", false);
    ////    ////                }
    ////    ////                else if (RoleId == 18)
    ////    ////                {
    ////    ////                    Response.Redirect("~/appMIC/MIC_REPORT_DETAILS.aspx", false);
    ////    ////                }
    ////    ////                else if (RoleId == 19)
    ////    ////                {
    ////    ////                    Response.Redirect("~/appCDCRITES/Ageing_DETAILS.aspx", false);
    ////    ////                }
    ////    ////                else if (RoleId == 10 || RoleId == 1 || RoleId == 2 || RoleId == 3 || RoleId == 4 || RoleId == 5 || RoleId == 8 || RoleId == 20 || RoleId == 21)
    ////    ////                {
    ////    ////                    if (LoginNo == 1)
    ////    ////                    {

    ////    ////                    }
    ////    ////                    else
    ////    ////                    {
    ////    ////                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('Please logout user already login!!!!'); window.location ='~/appInspectionAgency/Home.aspx';", true);
    ////    ////                        Response.Redirect("./Home.aspx", false);
    ////    ////                    }
    ////    ////                }
    ////    ////            }

    ////    ////            else if (ActiveFlag == 0 && MessageFlage == 1)
    ////    ////            {
    ////    ////                objUser = new clsMasterUser();
    ////    ////                //IP CAPTURE
    ////    ////                string LoginIP = string.Empty, strHttpref = string.Empty;
    ////    ////                try
    ////    ////                {
    ////    ////                    LoginIP = Request.ServerVariables["REMOTE_ADDR"];
    ////    ////                    strHttpref = Request.ServerVariables["http_referer"];
    ////    ////                }
    ////    ////                catch (Exception ex)
    ////    ////                {
    ////    ////                    ErrHandler.WriteError(ex.Message);
    ////    ////                }
    ////    ////                if (strHttpref.Length == 0)
    ////    ////                    LoginIP = "N.A.";
    ////    ////                //IP CAPTURE

    ////    ////                //  ErrorId = objUser.MasterUser_UpdateLoginNo(UserId, LoginNo, LoginIP);
    ////    ////                if (RoleId == 6)
    ////    ////                {
    ////    ////                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowStatus", "javascript:alert('You are already logged in !!!!'); window.location ='~/appInspectionAgency/Home.aspx';", true); //'" + aCookie.Values["USERFULLNAME"].ToString() + "'
    ////    ////                                                                                                                                                                                                              // Response.Redirect("~/appInspectionAgency/Home.aspx", false);
    ////    ////                }
    ////    ////                else if (RoleId == 1 || RoleId == 2 || RoleId == 3 || RoleId == 4 || RoleId == 5 || RoleId == 8 || RoleId == 20)
    ////    ////                {
    ////    ////                    if (LoginNo == 1)
    ////    ////                    {
    ////    ////                    }
    ////    ////                    else
    ////    ////                    {
    ////    ////                        string strvalue = "You are already logged in  as  " + userid_chek.ToString() + "";
    ////    ////                        ShowMessageBox(strvalue);
    ////    ////                    }
    ////    ////                }
    ////    ////            }

    ////    ////            else
    ////    ////            {
    ////    ////                txtUserName.Text = "";
    ////    ////                txtPassword.Text = "";
    ////    ////                lblMessage.Text = "This user account is DEACTIVATED !";
    ////    ////                lblMessage.Visible = true;
    ////    ////            }
    ////    ////        }
    ////    ////    }
    ////    ////    else
    ////    ////    {
    ////    ////        txtUserName.Text = "";
    ////    ////        txtPassword.Text = "";
    ////    ////        lblMessage.Text = "INVALID User Name or Password, Please contact site ADMINISTRATOR !";
    ////    ////        lblMessage.Visible = true;
    ////    ////    }
    ////    ////    //    Login(txtUserName.Text);
    ////    ////    // FormsAuthentication.RedirectFromLoginPage(txtUserName.Text,true);
    ////    ////}
    ////    ////catch (Exception ex)
    ////    ////{

    ////    ////}

    ////}


    private void ShowMessageBox(string Message)
    {
        Label lblMessageBox = new Label();
        lblMessageBox.Text =
            "<script language='javascript'>" + Environment.NewLine +
            "window.alert('" + Message + "')</script>";
        Page.Controls.Add(lblMessageBox);
    }
}