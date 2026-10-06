using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsMasterBranch
/// </summary>
public class clsMasterBranch
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public DataSet MasterBranch_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_Fetch_DataSet(int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = BranchId;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_FetchActivated_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_Activated", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_FetchByZoneId_DataSet(int ZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ByZoneId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_ZoneId", SqlDbType.Int).Value = ZoneId;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_FetchByZoneIdActivated_DataSet(int ZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ByZoneId_Activated", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_ZoneId", SqlDbType.Int).Value = ZoneId;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_FetchByZoneIdDesc_DataSet(int ZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ByZoneId_OBDesc", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_ZoneId", SqlDbType.Int).Value = ZoneId;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public int MasterBranch_DeleteById(int BranchId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterBranch_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = BranchId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outVal = new SqlParameter("@OutPut", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    public int MasterBranch_InsertUpdate(int BranchId, int fGPtbl_MB_ZoneId, string fGPtbl_MB_Name, string fGPtbl_MB_Code, string fGPtbl_MB_Desc, int UserId, string Branch_address, string phone_number, string fax_number, string Email)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterBranch", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = BranchId;
            MySqlComm.Parameters.Add("@fGPtbl_MB_ZoneId", SqlDbType.Int).Value = fGPtbl_MB_ZoneId;
            if (fGPtbl_MB_Name.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MB_Name", SqlDbType.VarChar, 200).Value = fGPtbl_MB_Name;
            if (fGPtbl_MB_Code.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MB_Code", SqlDbType.VarChar, 3).Value = fGPtbl_MB_Code;
            if (fGPtbl_MB_Desc.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MB_Desc", SqlDbType.VarChar, 2000).Value = fGPtbl_MB_Desc;
            if (Branch_address.Trim() != "") MySqlComm.Parameters.Add("@fGtbl_MB_Address", SqlDbType.VarChar, 2000).Value = Branch_address;
            if (phone_number.Trim() != "") MySqlComm.Parameters.Add("@fGtbl_MB_Phone", SqlDbType.VarChar, 50).Value = phone_number;
            if (fax_number.Trim() != "") MySqlComm.Parameters.Add("@fGtbl_MB_Fax", SqlDbType.VarChar, 50).Value = fax_number;
            if (Email.Trim() != "") MySqlComm.Parameters.Add("@fGtbl_MB_Email", SqlDbType.VarChar, 100).Value = Email;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    public int MasterBranch_ActivateById(int MasterBranchId, int UserId, string Activate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterBranch_ActivateById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = MasterBranchId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@Activate", SqlDbType.VarChar, 15).Value = Activate;

            SqlParameter outVal = new SqlParameter("@OutPut", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }


    // Bind Zone Through RoleID Means UserID
    public DataSet MasterZone_Fetch_Dataset_Report(int RoleID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_ActivatedByRolId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleID;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataSet MasterBranch_Fetch_Dataset_Report(int UserId, int ZoneID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterBranch_ByZoneId_Activated_ByUserID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneID;

            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public DataTable MasterBranch_Fetch_Address(int UserId)
    {
        DataTable DT;
        DT = new DataTable();
        try
        {

            MyAdp = new SqlDataAdapter("Gpsp_Fetch_BranchChangeAddress", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            MyAdp.Fill(DT);
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
        return DT;
    }


    public int F_BRANCH_ADDRESSUPDATE(int BranchId, string ADDRESS, string PHONE, string FAX, string MAILID)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_Branch_Address", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_BranchID", SqlDbType.Int).Value = BranchId;
            MySqlComm.Parameters.Add("@fGtbl_MB_Address", SqlDbType.VarChar, 2000).Value = ADDRESS;
            MySqlComm.Parameters.Add("@fGtbl_MB_Phone", SqlDbType.VarChar, 50).Value = PHONE;
            MySqlComm.Parameters.Add("@fGtbl_MB_Fax", SqlDbType.VarChar, 50).Value = FAX;
            MySqlComm.Parameters.Add("@fGtbl_MB_Email", SqlDbType.VarChar, 100).Value = MAILID;



            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    public DataTable Fetch_BranchAddress_ByCompanyId(int CompanyId)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_Fetch_BranchAddress_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public DataSet Fetch_Companydetails_ByBranchid(int branchid, string search)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Update_BranchChangeofCompanySearch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            if (search != "") MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = search;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    //FOR SHIFTING BRANCH
    public int Update_ShiftBranch(int companyid, int branchid, int userid)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Waiting_Update_BranchChangeofCompany", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            SqlParameter outval = new SqlParameter("@Outvalue", SqlDbType.Int);
            outval.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outval);
            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outval.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }
}
