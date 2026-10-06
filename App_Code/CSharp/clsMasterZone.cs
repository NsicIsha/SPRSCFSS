using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsMasterZone
/// </summary>
public class clsMasterZone
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

	public clsMasterZone()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH ALL DATA FROM TABLE
    public DataSet MasterZone_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_All", DBManager.ConnectToDB());
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
    public DataSet MasterZone_Fetch_DataSet_Activated()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_Activated", DBManager.ConnectToDB());
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
    //TO FETCH ACTIVATED ZONE
    public DataSet MasterZone_FetchActivated_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_Activated", DBManager.ConnectToDB());
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

    //TO FETCH SELECTED DATA FROM TABLE USING ID
    public DataSet MasterZone_Fetch_Dataset(int MasterZoneId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MZ_Id", SqlDbType.Int).Value = MasterZoneId;

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

    //TO FETCH ALL DATA FROM TABLE IN DESCENDING ORDER
    public DataSet MasterZone_FetchDesc_Dataset()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_All_OBDesc", DBManager.ConnectToDB());
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

    //TO DELETE USING ID
    public int MasterZone_DeleteById(int MasterZoneId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterZone_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MZ_Id", SqlDbType.Int).Value = MasterZoneId;
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
    public int MasterZone_InsertUpdate(int MasterZoneId, string fGPtbl_MZ_Name, string fGPtbl_MZ_Desc, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterZone", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MZ_Id", SqlDbType.Int).Value = MasterZoneId;
            MySqlComm.Parameters.Add("@fGPtbl_MZ_Name", SqlDbType.VarChar, 200).Value = fGPtbl_MZ_Name;
            if (fGPtbl_MZ_Desc.Trim() != "")
                MySqlComm.Parameters.Add("@fGPtbl_MZ_Desc", SqlDbType.VarChar, 2000).Value = fGPtbl_MZ_Desc;
            else
                MySqlComm.Parameters.Add("@fGPtbl_MZ_Desc", SqlDbType.VarChar, 2000).Value = "";

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

    //TO ACTIVATE/DEACTIVATE USING ID
    public int MasterZone_ActivateById(int MasterZoneId, int UserId, string Activate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterZone_ActivateById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_MZ_Id", SqlDbType.Int).Value = MasterZoneId;
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
