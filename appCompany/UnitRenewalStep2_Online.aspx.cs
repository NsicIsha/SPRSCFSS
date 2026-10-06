using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Drawing;

public partial class appCompany_UnitRenewalStep2_Online : System.Web.UI.Page
{
    DataTable Mydt;
    DataSet MyDs;
    int ErrorId, CompanyId, UnitId, Formtypeid, BranchId;
    int UserId, RoleId, DepartmentId, Edit;
    DataTable dt;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;

    DataTable MyDt;
    FormInputReplace objInputReplace;
    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            // btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;

        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

       clsCompany objCompany = new clsCompany();
        BranchId = MasterBranch_Fetch_BranchId(DepartmentId);
        DataTable dt = new DataTable();
        dt = Company_Fetch_BranchAddress(Convert.ToInt32(BranchId));
        //ddlConstitution.DataTextField = "fGPtbl_CUnit_Constitution_Name";
        //ddlConstitution.DataValueField = "fGPtbl_CUnit_Constitution_Id";
        grdFactories.DataSource = dt;
        grdFactories.DataBind();
    }

    public int MasterBranch_Fetch_BranchId(int CompanyId)
    {
        int BranchId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_BranchId_By_CompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Char, 10).Value = CompanyId;
            BranchId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return BranchId;
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
}