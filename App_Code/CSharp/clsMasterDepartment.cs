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
/// Summary description for clsMasterDepartment
/// </summary>
public class clsMasterDepartment
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public DataSet MasterDepartment_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterDepartment_All", DBManager.ConnectToDB());
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

    public DataSet MasterDepartment_Fetch_DataSet(int DepartmentId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterDepartment_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MD_Id", SqlDbType.Int).Value = DepartmentId;

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

    public DataSet MasterDepartment_FetchAllDesc_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterDepartment_All_OBDesc", DBManager.ConnectToDB());
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

    public int MasterDepartment_DeleteById(int DepartmentId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterDepartment_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MD_Id", SqlDbType.Int).Value = DepartmentId;
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

    public int MasterDepartment_InsertUpdate(int DepartmentId, string fGPtbl_MD_Name, string fGPtbl_MD_Desc, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterDepartment", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MD_Id", SqlDbType.Int).Value = DepartmentId;
            if (fGPtbl_MD_Name.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MD_Name", SqlDbType.VarChar, 200).Value = fGPtbl_MD_Name;
            if (fGPtbl_MD_Desc.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MD_Desc", SqlDbType.VarChar, 2000).Value = fGPtbl_MD_Desc;
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

    public int MasterDepartment_ActivateById(int MasterDeptId, int UserId, string Activate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterDepartment_ActivateById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_MD_Id", SqlDbType.Int).Value = MasterDeptId;
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
}
