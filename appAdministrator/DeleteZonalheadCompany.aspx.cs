using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.IO;

public partial class appAdministrator_DeleteZonalheadCompany : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds, ds;
    int UserId, RoleId, DepartmentId;
    string UserFullName, password;
    int BranchId;
    string Branch_Name = "";
    int ZoneID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (UserId != 231)
            {
                //BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());

                ZoneID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"].ToString());

                Branch_Name = objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString();
            }
            else
            {
                Branch_Name = "text";
            }
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 2 || RoleId == 3 || RoleId == 5)
            {

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                    GridViewDataBind();
                    if (RoleId == 3)
                    {

                    }
                    else
                    {

                    }
                }
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";

        }
    }


    public void GridViewDataBind()
    {
        try
        {
            DataTable dt_1 = new DataTable();

            // dt_1 = dt_bind_grdid(BranchId); //Commented by Dhiraj on 29.08.2017


            dt_1 = dt_bind_grdid_ZoneId(ZoneID);

            if (dt_1.Rows.Count > 0)
            {
                grd_delete_company.DataSource = dt_1;
                grd_delete_company.DataBind();
            }
            else
            {
                grd_delete_company.DataBind();
                dt_1.Dispose();
            }

        }
        catch (Exception ex)
        {

        }
    }

    public DataTable dt_bind_grdid(int Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
            MyAdp = new SqlDataAdapter("GP_Sp_CompanyDeleteZonalHead", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch_Id;


            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

        return dt;
    }



    public DataTable dt_bind_grdid_ZoneId(int ZoneId)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
            MyAdp = new SqlDataAdapter("GP_Sp_CompanyDeleteZonalHead_ByZoneId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Zoneid", SqlDbType.Int).Value = ZoneId;


            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

        return dt;
    }



    public void UpdateDeleteCompanyReject(int companyID)
    {
        try
        {
            int k = 0;

            SqlCommand MySqlComm;
            MySqlComm = new SqlCommand("GP_sp_delete_companies_Incomplete_Branch_head_companydelete", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Companyid", SqlDbType.Int).Value = companyID;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
    protected void onrowcommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int k = 0;
            if (e.CommandName.ToString() == "edp")
            {
                k = Convert.ToInt32(e.CommandArgument.ToString());
                Update_companyDeleteforAccept(k);
                GridViewDataBind();
            }

            if (e.CommandName.ToString() == "reject")
            {
                k = Convert.ToInt32(e.CommandArgument.ToString());
                UpdateDeleteCompanyReject(k);
                GridViewDataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }


    //  Update table GPtbl_Company_Delete for Accepted 
    public void Update_companyDeleteforAccept(int companyID)
    {
        int k = 0;
        try
        {

            SqlCommand MySqlComm;
            MySqlComm = new SqlCommand("GP_Sp_UpdateCompanyZonalHeadDeleteAccept", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompnyID", SqlDbType.Int).Value = companyID;
            SqlParameter outError = new SqlParameter("@ErrorID", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
}
