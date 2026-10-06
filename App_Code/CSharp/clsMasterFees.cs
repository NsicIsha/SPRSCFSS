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
/// Summary description for clsMasterFees
/// </summary>
public class clsMasterFees
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public DataSet MasterFees_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFees_All", DBManager.ConnectToDB());
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

    public DataSet MasterFees_Fetch_DataSet(int FeeId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFees_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;

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

    public DataSet MasterFees_FetchByFormTypeId_DataSet(int FormTypeId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFees_ByFormTypeId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = FormTypeId;

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

    public DataSet MasterFees_FetchByFormTypeIdDesc_DataSet(int FormTypeId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFees_ByFormTypeId_OBDesc", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = FormTypeId;

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

    public int MasterFees_DeleteById(int FeeId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterFees_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;
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

    public int MasterFees_InsertUpdate(int FeeId, int fGPtbl_MFee_FormTypeId, string fGPtbl_MFee_SlabName, double fGPtbl_MFee_Amount, string fGPtbl_MFee_Desc, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterFees", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;
            MySqlComm.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = fGPtbl_MFee_FormTypeId;
            MySqlComm.Parameters.Add("@fGPtbl_MFee_SlabName", SqlDbType.VarChar, 500).Value = fGPtbl_MFee_SlabName;
            MySqlComm.Parameters.Add("@fGPtbl_MFee_Amount", SqlDbType.Float).Value = fGPtbl_MFee_Amount;
            if (fGPtbl_MFee_Desc.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MFee_Desc", SqlDbType.VarChar, 2000).Value = fGPtbl_MFee_Desc;
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

    public int MasterFees_ActivateById(int FeeId, int UserId, string Activate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterFees_ActivateById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_MD_Id", SqlDbType.Int).Value = FeeId;
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

    #region Ravish Pandey

    public DataSet MasterFees_Slab_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFees_All_slab", DBManager.ConnectToDB());
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

    public DataSet MasterFeesFormula_Fetch_DataSet(int FeeId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterFeeFormula_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;

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

    public int MasterFeeFormula_DeleteById(int FeeId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterFeeFormula_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;
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


    public int MasterFees_InsertUpdate_MasterFeeFormula(int FeeId, int fGPtbl_MFee_FormTypeId, double BaseSlab, double BaseSlabFee, double slabIncrement, double slabIncrementFee, string fGPtbl_MFee_Desc, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterFees_Formula", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_Id", SqlDbType.Int).Value = FeeId;
            MySqlComm.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = fGPtbl_MFee_FormTypeId;
            if (BaseSlab.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_MFF_BaseSlab", SqlDbType.Float).Value = BaseSlab;
            if (BaseSlabFee.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_MFF_BaseSlabFee", SqlDbType.Float).Value = BaseSlabFee;
            if (slabIncrement.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_MFF_ExceedSlab", SqlDbType.Float).Value = slabIncrement;
            if (slabIncrementFee.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_MFF_ExceedSlabFee", SqlDbType.Float).Value = slabIncrementFee;

            if (fGPtbl_MFee_Desc.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MFee_Desc", SqlDbType.VarChar, 2000).Value = fGPtbl_MFee_Desc;
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

    #endregion

}
