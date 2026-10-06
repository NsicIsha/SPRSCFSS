using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;


/// <summary>
/// Summary description for clsSentSMS
/// </summary>
public class clsSentSMS
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;
	public clsSentSMS()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH ALL DATA FROM TABLE
    public DataSet SentSMS_Fetch_DataSet()
    {

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_SentSMS_All", DBManager.ConnectToDB());
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
    public DataSet SentSMS_Fetch_DataSet(int SentSMSId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_SentSMS_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_SS_Id", SqlDbType.Int).Value = SentSMSId;

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
    public int SentSMS_DeleteById(int SentSMSId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_SentSMS_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_SS_Id", SqlDbType.Int).Value = SentSMSId;
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

    //UPDATE SP FOR SENTSMS NOT PRESENT


}
