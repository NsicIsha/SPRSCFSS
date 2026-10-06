using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Data.SqlClient;
using System.Drawing;
using NSICGP_VBClassLibrary;
using System.Text.RegularExpressions;
public partial class appAdministrator_ChangePwd : System.Web.UI.Page
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
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int successId;
    int MessageFlage = 0;
    string userid_chek;
    string UserEmailID = string.Empty;
    #region(CHECK SESSION)
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
    #endregion
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).Replace(" ", "+")).ToString();

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
        try
        {
            FunRequest();
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

        if (!IsPostBack)
        {
           // btnPassword.Attributes.Add("onclick", "javascript:Validate();");
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


    #region(CHANGE PASSWORD)

    protected void btnPassword_Click(object sender, EventArgs e)
    {
        try
        {
            //if (Convert.ToString(Session["Code"]).ToLower() != txtCaptcha.Text.Trim().ToLower())
            //{
            //    Session["Code"] = null;
            //    txtCaptcha.Text = "";
            //    divCaptcha.Visible = true;
            //    txtCaptcha.Focus();
            //    lblCaptcha.Text = "Incorrect verification code. Please try again.";
            //    lblCaptcha.ForeColor = Color.Red;

            //}
            //else
            //{


            string dbPassword = "";



            Int32 userId = ABMiscFunction.GetUserID();
            string strUserName = ABMiscFunction.GetUserName();

            string strPassword = txtCurrentPassword.Value.Trim();

            DataSet dslogin = new DataSet();
            dslogin = Login_Fetch_DataSet(strUserName);
            dbPassword = Convert.ToString(dslogin.Tables[0].Rows[0]["fGPtbl_MU_EncryptedPwd"].ToString());

            string strNewPassword = txtNewPassword.Value.Trim();
            // string strNewPasswordVal = txtNewPassword.Value.Trim()+ "A@*";

            string strNewPasswordVal = txtNewPassword.Value.Trim() + "SPRS!@#$%^&*";


            string strClearFormat = strNewPassword;// "";// hdn_Clr_Pwd.Value.Trim();


            string sPattern = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@.#*$=)!,&(^+%-]).{8,35})";

            bool status = false;

            string sMatchValue = string.Empty;

            string ErrorMessage;

            if (txtCurrentPassword.Value.Equals(dbPassword))
            {    
              //  status = (System.Text.RegularExpressions.Regex.IsMatch(strNewPasswordVal, sPattern));
                if (ValidatePassword(strNewPasswordVal, out ErrorMessage) == false)
                {
                    FailureText.Text = ErrorMessage;
                    return;
                }
                else
                {
                    status = true;
                }

                if (status == true)
                {
                    int row = ChangePassword(userId, strPassword, strNewPassword, strClearFormat, out sMatchValue);
                    if (sMatchValue.Equals("1"))
                    {
                        //FailureText.Text = "Password should different from your last three password.";
                        FailureText.Text = "Your password has been changed successfully!!!";
                    }
                    else if (sMatchValue.Equals("0"))
                    {
                        FailureText.Text = "Old password is wrong, Please try again.";
                    }
                }
            }
            else
            {
                //FailureText.Text = "Old password is wrong, Please try again.";
                FailureText.Text = "Password format is incorrect.Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=]";
            }
        
            ////string strNewPassword = txtNewPassword.Value.Trim();
            ////    string sPattern = @"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@.#*$=)!,&(^+%-]).{8,35})";
            ////    bool status = false;
            ////    status = (System.Text.RegularExpressions.Regex.IsMatch(strNewPassword, sPattern));
            ////    string sMatchValue = string.Empty;
            ////    if (status == true)
            ////    {
            ////   // strNewPassword= strNewPassword;// = strNewPassword.Substring(0, 32);

            ////        //int row = oABUserBLL.ChangePassword(userId, strPassword, strNewPassword, txtNewPassword.Value.Trim(), out status);

            ////        //if (status == true)
            ////        //{
            ////        //    FailureText.Text = "Your password has been changed successfully";
            ////        //}
            ////        //else
            ////        //{
            ////        //    FailureText.Text = "Old password is wrong, please try again";
            ////        //}
            ////        int row = ChangePassword(userId, strPassword, strNewPassword, txtNewPassword.Value.Trim(), out sMatchValue);
            ////        if (sMatchValue.Equals("1"))
            ////        {
            ////            //FailureText.Text = "Password should different from your last three password.";
            ////            FailureText.Text = "Your password has been changed successfully!!!";
            ////        }
            ////        //else if (sMatchValue.Equals("YES"))
            ////        //{
            ////        //    FailureText.Text = "Your password has been changed successfully!!!";
            ////        //}
            ////        else if (sMatchValue.Equals("0"))
            ////        {
            ////            FailureText.Text = "Old password is wrong, Please try again.";
            ////        }
            ////    }
            ////    else
            ////    {
            ////        FailureText.Text = "Password format is incorrect";
            ////    }
            //}
        }
        catch (Exception ex)
        {
             
        }
    }
    #endregion

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
    public int ChangePassword(int userId, string oldPassword, string newPassword, string clearFormatPassword, out string sMatchValue)
    {
        int iRowAffected = 0;
        //bool status = false;
        string MatchValue = string.Empty;

        bool status = false;
        try
        {
            MySqlComm = new SqlCommand("GP_Sp_ChangePassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.VarChar, 1000).Value = userId;
            MySqlComm.Parameters.Add("@OldPassword", SqlDbType.VarChar, 8000).Value = oldPassword;
            MySqlComm.Parameters.Add("@NewPassword", SqlDbType.VarChar, 8000).Value = newPassword;
            MySqlComm.Parameters.Add("@ClearFormatPassword", SqlDbType.VarChar, 8000).Value = clearFormatPassword;            
            SqlParameter outError = new SqlParameter("@Status", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            MatchValue = Convert.ToString(outError.Value);
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
        sMatchValue = MatchValue;
        return iRowAffected;
    }
}