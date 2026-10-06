using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsSentMailer
/// </summary>
public class clsSentMailer
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;
	public clsSentMailer()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH ALL DATA FROM TABLE
    public DataSet SentMaile_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_SentMailer_All", DBManager.ConnectToDB());
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
    public DataSet SentMailer_Fetch_DataSet(int SentMailerId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_SentMailer_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_SM_Id", SqlDbType.Int).Value = SentMailerId;

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
    public int SentMailer_DeleteById(int SentMailerId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_SentMailer_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_TUB_Id", SqlDbType.Int).Value = SentMailerId;
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

    /*INSERT INTO TABLE
    public int SentMailer_InsertUpdate(string fGPtbl_SM_To, int fGPtbl_SM_ToUserId, int fGPtbl_SM_ToUserTable, string fGPtbl_SM_Cc, string fGPtbl_SM_Bcc, string fGPtbl_SM_From, int fGPtbl_SM_FromUserId, int fGPtbl_SM_FromUserTable, string fGPtbl_SM_Subject, string fGPtbl_SM_Body, string fGPtbl_SM_Attachment, string fGPtbl_SM_Link, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_SentMailer", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            if (fGPtbl_SM_To.Trim()!="") MySqlComm.Parameters.Add("@fGPtbl_SM_To", SqlDbType.VarChar, 2000).Value = fGPtbl_SM_To;
            MySqlComm.Parameters.Add("@fGPtbl_SM_ToUserId", SqlDbType.Int).Value = fGPtbl_SM_ToUserId;
            MySqlComm.Parameters.Add("@fGPtbl_SM_ToUserTable", SqlDbType.Int).Value = fGPtbl_SM_ToUserTable;
            if (fGPtbl_SM_Cc.Trim()!="") MySqlComm.Parameters.Add("@fGPtbl_SM_Cc", SqlDbType.VarChar, 2000).Value = fGPtbl_SM_Cc;
            if (fGPtbl_SM_Bcc.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_Bcc", SqlDbType.VarChar, 2000).Value = fGPtbl_SM_Bcc;
            if (fGPtbl_SM_From.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_From", SqlDbType.VarChar, 2000).Value = fGPtbl_SM_From;
            MySqlComm.Parameters.Add("@fGPtbl_SM_FromUserId", SqlDbType.Int).Value = fGPtbl_SM_FromUserId;
            MySqlComm.Parameters.Add("@fGPtbl_SM_FromUserTable", SqlDbType.Int).Value = fGPtbl_SM_FromUserTable;
            if (fGPtbl_SM_Subject.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_Subject", SqlDbType.VarChar, 2000).Value = fGPtbl_SM_Subject;
            if (fGPtbl_SM_Body.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_Body", SqlDbType.Text).Value = fGPtbl_SM_Body;
            if (fGPtbl_SM_Attachment.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_Attachment", SqlDbType.VarChar, 8000).Value = fGPtbl_SM_Attachment;
            if (fGPtbl_SM_Link.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_SM_Link", SqlDbType.VarChar, 8000).Value = fGPtbl_SM_Link;

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
    }   */



}
