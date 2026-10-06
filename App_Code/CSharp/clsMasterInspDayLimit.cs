using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsMasterInspDayLimit
/// </summary>
public class clsMasterInspDayLimit
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

	public clsMasterInspDayLimit()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH ALL DATA FROM TABLE
    public DataSet MasterInspDayLimit_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterInspDayLimit_All", DBManager.ConnectToDB());
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
    public DataSet MasterInspDayLimit_Fetch_DataSet(int MasterInspDayLimitId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterInspDayLimit_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MIDL_Id", SqlDbType.Int).Value = MasterInspDayLimitId;

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
    public int MasterInspDayLimit_DeleteById(int MasterInspDayLimit, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterInspDayLimit_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MIDL_Id", SqlDbType.Int).Value = MasterInspDayLimit;
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
    public int MasterInspDayLimit_InsertUpdate(int MasterInspDayLimitId, int fGPtbl_MIDL_DateIssue, int fGPtbl_MIDL_ReportStart, int fGPtbl_MIDL_ReportSubmit,int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterInspDayLimit", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MIDL_Id", SqlDbType.Int).Value = MasterInspDayLimitId;
            MySqlComm.Parameters.Add("@fGPtbl_MIDL_DateIssue", SqlDbType.Int).Value = fGPtbl_MIDL_DateIssue;
            MySqlComm.Parameters.Add("@fGPtbl_MIDL_ReportStart", SqlDbType.Int).Value = fGPtbl_MIDL_ReportStart;
            MySqlComm.Parameters.Add("@fGPtbl_MIDL_ReportSubmit", SqlDbType.Int).Value = fGPtbl_MIDL_ReportSubmit;
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
}
