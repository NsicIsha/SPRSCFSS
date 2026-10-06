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
/// Summary description for clsTrans_UserBranch
/// </summary>
public class clsTrans_UserBranch
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

	public clsTrans_UserBranch()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /*TO FETCH ALL DATA FROM TABLE*/
    public DataSet Trans_UserBranch_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransUserBranch_All", DBManager.ConnectToDB());
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

    // TO FETCH SELECTIVE DATA FROM TABLE BY USING ID
    public DataSet Trans_UserBranch_Fetch_DataSet(int TransUserBranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransUserBranch_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_Id", SqlDbType.Int).Value = TransUserBranchId;

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

    // TO DELETE USING ID
    public int Trans_UserBranch_DeleteById(int TransUserBranchId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_TransUserBranch_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TUB_Id", SqlDbType.Int).Value = TransUserBranchId;
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

    //  TO INSERT OR UPDATE
    public int Trans_UserBranch_InsertUpdate(int TransUserBranchId, int fGPtbl_TUB_UserId, int RoleId, int fGPtbl_TUB_ZoneId, int fGPtbl_TUB_BrabchId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_TransUserBranch", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TUB_Id", SqlDbType.Int).Value = TransUserBranchId;
            MySqlComm.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = fGPtbl_TUB_UserId;
            MySqlComm.Parameters.Add("@fGPtbl_TUB_ZoneId", SqlDbType.Int).Value = fGPtbl_TUB_ZoneId;
            MySqlComm.Parameters.Add("@fGPtbl_TUB_BrabchId", SqlDbType.Int).Value = fGPtbl_TUB_BrabchId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;
            
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

    //TO FETCH ZONE ID BY USING USER ID
    public DataSet Trans_UserBranch_FetchZoneId_DataSet(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TransUserBranch_ByUserId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = userid;

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

}
