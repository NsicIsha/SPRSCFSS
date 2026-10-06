using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsTrans_RoleApplication
/// </summary>
public class clsTrans_RoleApplication
{
     SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

	public clsTrans_RoleApplication()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /*TO FETCH ALL DATA FROM TABLE*/
    public DataSet Trans_RoleApplication_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransRoleApplication_All", DBManager.ConnectToDB());
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

    //TO FETCH ALL DATA FROM TABLE USING ID
    public DataSet Trans_RoleApplication_Fetch_DataSet(int TransRoleApplicationId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransRoleApplication_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_Id", SqlDbType.Int).Value = TransRoleApplicationId;

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

    //TO DELETE USING ID
    public int Trans_RoleApplication_DeleteById(int TransRoleApplicationId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_TransRoleApplication_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TRA_Id", SqlDbType.Int).Value = TransRoleApplicationId;
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

    //TO INSERT OR UPDATE
    public int Trans_RoleApplication_InsertUpdate(int TransRoleApplicationId, int fGPtbl_TRA_RoleId, int fGPtbl_TRA_ApplicationId, byte fGPtbl_TRA_ARView, byte fGPtbl_TRA_ARInsert, byte fGPtbl_TRA_ARUpdate, byte fGPtbl_TRA_ARDelete, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_TransRoleApplication", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TRA_Id", SqlDbType.Int).Value = TransRoleApplicationId;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_RoleId", SqlDbType.Int).Value = fGPtbl_TRA_RoleId;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_ApplicationId", SqlDbType.Int).Value = fGPtbl_TRA_ApplicationId;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_ARView", SqlDbType.TinyInt).Value = fGPtbl_TRA_ARView;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_ARInsert", SqlDbType.TinyInt).Value = fGPtbl_TRA_ARInsert;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_ARUpdate", SqlDbType.TinyInt).Value = fGPtbl_TRA_ARUpdate;
            MySqlComm.Parameters.Add("@fGPtbl_TRA_ARDelete", SqlDbType.TinyInt).Value = fGPtbl_TRA_ARDelete;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;

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


}
