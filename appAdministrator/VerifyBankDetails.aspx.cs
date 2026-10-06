using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Schema;
using System.Xml;
using System.Collections;
 
using System.Drawing;

public partial class appAdministrator_VerifyBankDetails :  System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    string prevPage = String.Empty;
    string pagename;
    short tabindx = 1;
    int Primary_Id;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    SqlConnection con;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    DataSet MyDs;
     
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();


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
        FunRequest();
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        MyDs = new DataSet();
        MyDs = FetchBranchId_By_userId(UserId);
        BranchId = Convert.ToInt32(MyDs.Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"].ToString());
        int RResponse_Id = Company_Insert_BankDetails(BranchId, UserId, txtContactPersonName.Text, txtAddress.Text,txtContactNumber.Text, txtBankName.Text, txtBankbranch.Text, txtBankAddress.Text, ddlAccountType.SelectedValue, txtIFSCCODE.Text, txtAccountNumber.Text, txtMICRCode.Text);
    }

    public DataSet FetchBranchId_By_userId(int  UserId)
    {
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_BranchId_By_UserId", con);
            con.Open();
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
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
    public int Company_Insert_BankDetails(int BranchId,int UserId, string ContactPersonName, string Address, string ContactNumber,string BankName, string Branch,string BankAddress,string AccountType, string IFSCCODE, string AccountNumber, string MICRCode)
    {
        try
        {
            MySqlComm = new SqlCommand("GPSp_Update_Branch_BankDetails", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure; 
            MySqlComm.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchId; 
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = BranchId;
            MySqlComm.Parameters.Add("@fGPtbl_MB_ContactPerson", SqlDbType.VarChar, 50).Value = ContactPersonName;
            MySqlComm.Parameters.Add("@fGPtbl_MB_Address", SqlDbType.VarChar, 5000).Value = Address;
            MySqlComm.Parameters.Add("@fGPtbl_MB_ContactNumber", SqlDbType.VarChar, 50).Value = ContactNumber;
            MySqlComm.Parameters.Add("@fGPtbl_MB_BankName", SqlDbType.VarChar, 500).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_MB_Branch", SqlDbType.VarChar, 500).Value = Branch;
            MySqlComm.Parameters.Add("@fGPtbl_MB_BankAddress", SqlDbType.VarChar, 5000).Value = BankAddress;
            MySqlComm.Parameters.Add("@fGPtbl_MB_AccountType", SqlDbType.VarChar, 50).Value = AccountType;
            MySqlComm.Parameters.Add("@fGPtbl_MB_IFSCCode", SqlDbType.VarChar, 100).Value = IFSCCODE;
            MySqlComm.Parameters.Add("@fGPtbl_MB_AccountNumber", SqlDbType.VarChar, 50).Value = AccountNumber;
            MySqlComm.Parameters.Add("@fGPtbl_MB_MICR", SqlDbType.VarChar,50).Value = MICRCode;
            SqlParameter fGPtbl_POI_Id = new SqlParameter("@ErrorID", SqlDbType.Int);
            fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(fGPtbl_POI_Id);
            MySqlComm.ExecuteNonQuery();
            Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
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
        return Primary_Id;
    }

    void Clear()
    {
        txtContactPersonName.Text = "";
        txtAddress.Text = "";
        txtContactNumber.Text = "";
        txtBankName.Text = "";
        txtBankbranch.Text = "";
        txtBankAddress.Text = "";
        txtAccountNumber.Text = "";
        txtIFSCCODE.Text = "";
        txtMICRCode.Text = "";
    }
}