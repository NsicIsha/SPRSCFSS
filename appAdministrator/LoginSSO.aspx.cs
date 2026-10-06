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

using Org.BouncyCastle.Ocsp;

using System.Configuration;

using System.Security.Policy;
using System.Runtime.Remoting.Metadata.W3cXsd2001;


public partial class appAdministrator_LoginSSO : System.Web.UI.Page
{
    int UserId = 0;
    int RoleId = 0;
    string UserFullname = "";
    int DepartmentId = 0;
    int ActiveFlag = 0;
    int LoginNo = 0;
    string UserEmailId = "";
    clsMasterUser objUser;
    clsMasterMailer objMailer;
    int ErrorId = 0;

    string ipAddress = "";// Request.UserHostAddress;

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

    string EncryptUserId = string.Empty, EncryptRoleId = string.Empty, EncryptDepartmentId = string.Empty;

    string[] iUserData = new string[4];
    DecryptEncrypt objDecEnc = new DecryptEncrypt();
    int Cnt = 0;
    HttpCookie aCookie;

    string sOfficeID = string.Empty;
    string sEmpID = string.Empty;
    string sOfficeType = string.Empty;
    string sEmpName = string.Empty;
    SqlConnection con;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    protected void Page_Load(object sender, EventArgs e)

    {
        string URL = string.Empty;
        bool bIncorrect = true;
        string userID = string.Empty;
        string userRole = string.Empty;

        if (!IsPostBack)
        {
            try
            {
                string sValue = "NSIC5365";// Request.QueryString["enc"];
                if (!string.IsNullOrEmpty(sValue))
                {
                    //if (string.IsNullOrEmpty(sValue))
                    //{ return; }

                    //using (SqlConnection connection = ReturnConnStringDIGIHR())
                    //{
                    //    connection.Open();
                    //    string sQueryString = NewCryptoo.DecryptStr(sValue, GetEncryptionKey(connection));
                    //    if (!string.IsNullOrEmpty(sQueryString))
                    //    {
                    //        string[] args = sQueryString.Split('&');
                    //        if (args.Length > 0)
                    //        {
                    //            foreach (var arg in args)
                    //            {
                    //                if (arg.ToLower().StartsWith("e="))
                    //                    sEmpID = arg.Substring(2);
                    //                else if (arg.ToLower().StartsWith("b="))
                    //                    sOfficeID = arg.Substring(2);
                    //                else if (arg.ToLower().StartsWith("o="))
                    //                    sOfficeType = arg.Substring(2);
                    //                else if (arg.ToLower().StartsWith("n="))
                    //                    sEmpName = arg.Substring(2);
                    //            }
                    //        }
                    //    }

                    //}

                     sEmpID = "NSIC5365";
                    Response.Write(sEmpID);

                    // sEmpID = sEmpID.Remove(0, 4); //Remove First 4 Charector NSIC

                    string sEmpNames = string.Empty;
                    int count = 0;
                    using (SqlConnection connections = ReturnConnStringSPRS())
                    {
                        connections.Open();
                        string strcmd = "SELECT * FROM [dbo].[GPtbl_MasterUser_SSO] WHERE EMPID=@epf";


                        SqlCommand command = new SqlCommand(strcmd, connections);
                        command.Parameters.Add("@epf", System.Data.SqlDbType.VarChar).Value = sEmpID;
                        //command.Parameters.Add("@mob", System.Data.SqlDbType.VarChar).Value = MobileNo;
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            iLoginId = Convert.ToInt32(reader["fGPtbl_MU_Id"]);
                            iUserData[0] = Convert.ToInt32(reader["fGPtbl_MU_RoleId"]).ToString();
                            iUserData[1] = Convert.ToInt32(reader["fGPtbl_MU_Id"]).ToString();
                            iUserData[2] = Convert.ToString(reader["fGPtbl_MU_FName"]) + " ";
                            iUserData[3] = Convert.ToString(reader["fGPtbl_MU_Name"].ToString());
                           
                            ifGPtbl_MU_EncryptedPwdStatus= Convert.ToInt32(reader["fGPtbl_MU_EncryptedPwdStatus"]);

                            count++;
                        }


                        string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
                        con = new SqlConnection(connectionstring);
                        MyDs = new DataSet();
                        string strcmds = "SELECT * FROM [dbo].[GPtbl_MasterUser_SSO] WHERE EMPID=@EMPID";
                        MyAdp = new SqlDataAdapter(strcmds, con);
                        con.Open();
                        MyAdp.SelectCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = sEmpID;
                        MyAdp.Fill(MyDs);

                        int recordCount = MyDs.Tables[0].Rows.Count;


                        if (recordCount == 1)
                        {
                            if (!string.IsNullOrEmpty(iLoginId.ToString()))
                            {
                                int UniqueId = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Success", Convert.ToString(reader["fGPtbl_MU_Name"].ToString()), "");

                                Session["UniqueId"] = UniqueId;

                                FormsAuthenticationTicket tkt;
                                string cookiestr;
                                HttpCookie ck;
                                Session["LoginId"] = Convert.ToString(reader["fGPtbl_MU_Name"].ToString());// txtUserName.Text;
                                tkt = new FormsAuthenticationTicket(1, Convert.ToString(reader["fGPtbl_MU_Name"].ToString()), DateTime.Now,
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
                                UserId = Convert.ToInt32(reader["fGPtbl_MU_Id"]);
                                RoleId = Convert.ToInt32(reader["fGPtbl_MU_RoleId"]);
                                DepartmentId = Convert.ToInt32(reader["fGPtbl_MU_DepartmentId"]);
                                UserFullname = Convert.ToString(reader["fGPtbl_MU_FName"]) + " ";

                                EncryptUserId = objDecEnc.Encrypt(UserId.ToString());
                                EncryptRoleId = objDecEnc.Encrypt(RoleId.ToString());
                                EncryptDepartmentId = objDecEnc.Encrypt(DepartmentId.ToString());

                                if (Convert.ToString(reader["fGPtbl_MU_MName"]).Trim() != "")
                                {
                                    UserFullname += Convert.ToString(reader["fGPtbl_MU_MName"]) + " ";
                                }
                                UserFullname += Convert.ToString(reader["fGPtbl_MU_LName"]) + " ";
                                ActiveFlag = Convert.ToInt32(reader["fGPtbl_MU_AccessId"]);
                                LoginNo = Convert.ToInt32(reader["fGPtbl_MU_LoginNo"]);
                                UserEmailId = Convert.ToString(reader["fGPtbl_MU_Email"]);
                                LoginNo += 1;


                                reader.Close();

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
                                string LoginIP = string.Empty, strHttpref = "";
                                try
                                {
                                    LoginIP = Request.ServerVariables["REMOTE_ADDR"];
                                    strHttpref = Request.ServerVariables["http_referer"];
                                }
                                catch (Exception ex)
                                {
                                    ErrHandler.WriteError(ex.Message);
                                }
                                //if (strHttpref.Contains(null)) //if (strHttpref.Length == 0)
                                // LoginIP = "N.A.";


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
                                    }
                                }

                            }
                            else
                            {
                                ShowAlertMessage("Invalid username / password or You are not Authorised to Proceed...!. Please Contact Administrator..!");
                            }

                        }
                        else //if (count == 1)
                        {
                            DataSet ds = new DataSet();
                            //  sEmpID = "NSIC4741";
                            ds = UserRole_BranchName_ByEMPID(sEmpID);
                            DataTable dt = ds.Tables[0];
                            lblName.Text = sEmpName;
                            //lblNamee.Text = "SPRHYDEaccounts";// Convert.ToString(reader["fGPtbl_MU_Name"].ToString()); 
                            //var distinctValues = dt.AsEnumerable()
                            //   .Select(row => row.Field<string>("fGPtbl_MB_Name"))
                            //   .Distinct()
                            //   .ToList();
                            //rdbBranch.DataSource = distinctValues;
                            //rdbBranch.DataBind();

                            DataView view = new DataView(dt);
                            DataTable distinctTable = view.ToTable(true, "fGPtbl_MB_Id", "fGPtbl_MB_Name");
                            rdbBranch.DataSource = distinctTable;
                            rdbBranch.DataTextField = "fGPtbl_MB_Name";
                            rdbBranch.DataValueField = "fGPtbl_MB_Id";
                            rdbBranch.DataBind();
                            rdbBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));

                            DataView views = new DataView(dt);
                            DataTable distinctTable2 = view.ToTable(true, "fGPtbl_MU_RoleId", "fGPtbl_MR_Name");

                            ddlRole.DataSource = distinctTable2;
                            ddlRole.DataTextField = "fGPtbl_MR_Name";
                            ddlRole.DataValueField = "fGPtbl_MU_RoleId";
                            ddlRole.DataBind();
                            ddlRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);

                //Common.LogError(ex);
                ShowAlertMessage("Invalid username / password or You are not Authorised to Proceed...!. Please Contact Administrator..!");

            }
        }

    }


    public DataSet UserRole_BranchName_ByEMPID(string EMPID)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GpSp_UserRole_BranchName_ByEMPID", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@EMPID", SqlDbType.VarChar).Value = EMPID;
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

    public enum eRoles { BRANCh, ACCOUNTS, HEAD, ZONAHEAD, ADMIN }


    internal string GetEncryptionKey(SqlConnection connection)
    {
        try
        {
            string strcmd = "SELECT Masters.GetEncryptionValue(@key) 'TY'";
            SqlCommand command = new SqlCommand(strcmd, connection);
            command.Parameters.AddWithValue("@key", SqlDbType.VarChar).Value = ConfigurationManager.AppSettings["SPRSKey"].ToString();
            string sValue = command.ExecuteScalar().ToString();
            return sValue;
        }
        catch (Exception ex)
        {
            //Common.LogError(ex);
            return string.Empty;
        }
    }
    private void ShowAlertMessage(string Message, string sAlertType = "alert-danger")
    {
        if (string.IsNullOrEmpty(Message))
            divErrorMsg.Visible = false;
        else
            divErrorMsg.Visible = true;
        divErrorMsg.InnerHtml = "<div class=\"alert " + sAlertType + "\">" + Message + "</div>";
    }

    public static SqlConnection ReturnConnStringSPRS()
    { return new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ToString()); }

    public static SqlConnection ReturnConnStringDIGIHR()
    { return new SqlConnection(ConfigurationManager.ConnectionStrings["connStringDigiHR"].ToString()); }



    protected void btnGoAhead_Click(object sender, EventArgs e)
    {
        string URL = string.Empty;
        bool bIncorrect = true;
        string userID = string.Empty;
        string userRole = string.Empty;

        //if (!IsPostBack)
        //{

        try
        {

            string sValue = Request.QueryString["enc"];
            if (!string.IsNullOrEmpty(sValue))
            {
                if (string.IsNullOrEmpty(sValue))
                { return; }

                using (SqlConnection connection = ReturnConnStringDIGIHR())
                {
                    connection.Open();
                    string sQueryString = NewCryptoo.DecryptStr(sValue, GetEncryptionKey(connection));
                    if (!string.IsNullOrEmpty(sQueryString))
                    {
                        string[] args = sQueryString.Split('&');
                        if (args.Length > 0)
                        {
                            foreach (var arg in args)
                            {
                                if (arg.ToLower().StartsWith("e="))
                                    sEmpID = arg.Substring(2);
                                else if (arg.ToLower().StartsWith("b="))
                                    sOfficeID = arg.Substring(2);
                                else if (arg.ToLower().StartsWith("o="))
                                    sOfficeType = arg.Substring(2);
                                else if (arg.ToLower().StartsWith("n="))
                                    sEmpName = arg.Substring(2);
                            }
                        }
                    }
                    //
                }
            }



            int BranchId = 0; int Role = 0;
            BranchId = Convert.ToInt32(rdbBranch.SelectedValue);
            Role = Convert.ToInt32(ddlRole.SelectedValue);

            string sEmpNames = string.Empty;

            using (SqlConnection connections = ReturnConnStringSPRS())
            {
                connections.Open();
                string strcmd = "SELECT * FROM [dbo].[GPtbl_MasterUser_SSO] Inner join [dbo].[GPtbl_MasterRole] on [dbo].[GPtbl_MasterRole].fGPtbl_MR_Id=[dbo].[GPtbl_MasterUser_SSO].fGPtbl_MU_RoleId\r\nAND fGPtbl_MR_Display=1 AND fGPtbl_MR_Delete=0\r\n\r\nInner Join GPtbl_Trans_UserBranch On [dbo].[GPtbl_Trans_UserBranch].fGPtbl_TUB_UserId=[dbo].[GPtbl_MasterUser_SSO].fGPtbl_MU_Id AND fGPtbl_TUB_Display=1 AND fGPtbl_TUB_Delete=0\r\n\r\nInner Join [dbo].[GPtbl_MasterBranch] On [dbo].[GPtbl_MasterBranch].fGPtbl_MB_Id=[dbo].[GPtbl_Trans_UserBranch].fGPtbl_TUB_BrabchId\r\nAND fGPtbl_MR_Display=1 AND fGPtbl_MR_Delete=0\r\nAND fGPtbl_MB_Id=@BranchId\r\nWHERE EMPID=@epf and fGPtbl_MU_RoleId=@Role";


                SqlCommand command = new SqlCommand(strcmd, connections);
                command.Parameters.Add("@epf", System.Data.SqlDbType.VarChar).Value = sEmpID; 
                command.Parameters.Add("@BranchId", System.Data.SqlDbType.VarChar).Value = BranchId;
                command.Parameters.Add("@Role", System.Data.SqlDbType.VarChar).Value = Role;
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    iLoginId = Convert.ToInt32(reader["fGPtbl_MU_Id"]);

                    iUserData[0] = Convert.ToInt32(reader["fGPtbl_MU_RoleId"]).ToString();
                    iUserData[1] = Convert.ToInt32(reader["fGPtbl_MU_Id"]).ToString();
                    iUserData[2] = Convert.ToString(reader["fGPtbl_MU_FName"]) + " ";
                    iUserData[3] = Convert.ToString(reader["fGPtbl_MU_Name"].ToString());

                    ifGPtbl_MU_EncryptedPwdStatus = Convert.ToInt32(reader["fGPtbl_MU_EncryptedPwdStatus"]);

                }

                if (!string.IsNullOrEmpty(iLoginId.ToString()))
                {
                    int UniqueId = Insert_logDetails(iLoginId, ipAddress, "", Convert.ToString(iRoleId), "Login Success", Convert.ToString(reader["fGPtbl_MU_Name"].ToString()), "");

                    Session["UniqueId"] = UniqueId;

                    FormsAuthenticationTicket tkt;
                    string cookiestr;
                    HttpCookie ck;
                    Session["LoginId"] = Convert.ToString(reader["fGPtbl_MU_Name"].ToString());// txtUserName.Text;
                    tkt = new FormsAuthenticationTicket(1, Convert.ToString(reader["fGPtbl_MU_Name"].ToString()), DateTime.Now,
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
                    UserId = Convert.ToInt32(reader["fGPtbl_MU_Id"]);
                    RoleId = Convert.ToInt32(reader["fGPtbl_MU_RoleId"]);
                    DepartmentId = Convert.ToInt32(reader["fGPtbl_MU_DepartmentId"]);
                    UserFullname = Convert.ToString(reader["fGPtbl_MU_FName"]) + " ";

                    EncryptUserId = objDecEnc.Encrypt(UserId.ToString());
                    EncryptRoleId = objDecEnc.Encrypt(RoleId.ToString());
                    EncryptDepartmentId = objDecEnc.Encrypt(DepartmentId.ToString());

                    if (Convert.ToString(reader["fGPtbl_MU_MName"]).Trim() != "")
                    {
                        UserFullname += Convert.ToString(reader["fGPtbl_MU_MName"]) + " ";
                    }
                    UserFullname += Convert.ToString(reader["fGPtbl_MU_LName"]) + " ";
                    ActiveFlag = Convert.ToInt32(reader["fGPtbl_MU_AccessId"]);
                    LoginNo = Convert.ToInt32(reader["fGPtbl_MU_LoginNo"]);
                    UserEmailId = Convert.ToString(reader["fGPtbl_MU_Email"]);
                    LoginNo += 1;


                    reader.Close();

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
                    string LoginIP = string.Empty, strHttpref = "";
                    try
                    {
                        LoginIP = Request.ServerVariables["REMOTE_ADDR"];
                        strHttpref = Request.ServerVariables["http_referer"];
                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.Message);
                    }
                    //if (strHttpref.Contains(null)) //if (strHttpref.Length == 0)
                    // LoginIP = "N.A.";
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
                            Session["ifGPtbl_MU_EncryptedPwdStatus"] = ifGPtbl_MU_EncryptedPwdStatus;
                            Response.Redirect("./Home.aspx", false);
                        }
                    }
                }
                else
                {
                    ShowAlertMessage("Invalid username / password or You are not Authorised to Proceed...!. Please Contact Administrator..!");
                }
            }
        }
        catch (Exception ex)
        {
            //Common.LogError(ex);
            ShowAlertMessage("Invalid username / password or You are not Authorised to Proceed...!. Please Contact Administrator..!");
        }
        //}
    }
}