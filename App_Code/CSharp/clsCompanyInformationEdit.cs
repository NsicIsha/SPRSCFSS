using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

/// <summary>
/// Summary description for clsCompanyInformationEdit
/// </summary>
public class clsCompanyInformationEdit
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    int outVariable = 0;
    object objforDBnull;

	public clsCompanyInformationEdit()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //TO FETCH LAST TURNOVER OF COMPANY
    public string Fetch_LastyearTurnover(int companyid)
    {
        string turnover = string.Empty;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_FetchLastyearTurnover_ByCompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            objforDBnull = MySqlComm.ExecuteScalar();
            turnover=objforDBnull != null ? Convert.ToString(MySqlComm.ExecuteScalar()) : string.Empty; 
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in Fetch_LastyearTurnover " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return turnover;
    }

    public DataSet Fetch_FeepaidTurnoverFee(Decimal Turnover, int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyEdit_FetchSlabFeeandPayment", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Turnover", SqlDbType.Float).Value = Turnover;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Exception in Fetch_FeepaidTurnoverFee() " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    public float Fetch_ServiceTax()
    {
        float servicetax = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_FetchServiceTax", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            servicetax =Convert.ToSingle(MySqlComm.ExecuteScalar());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in Fetch_ServiceTax() " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return servicetax;
    }

    /*
    public int Insert_PaymentDetail(int formtypeid, int companyid, int unitid, float unitamount, float servicetax,
        float totalamount, int paymenttype, string chequeddno, string cashreceiptno, string bankname, string bankbranch,
        string chequedddate, string filename, string filepath, string payableat, int userid)
    {
        try
        {
            MySqlComm=new SqlCommand("GPsp_CompanyEdit_Insert_Paymentdetail", DBManager.ConnectToDB());
            MySqlComm.CommandType=CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PDA_FormTypeId", SqlDbType.Int).Value=formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_CompanyId", SqlDbType.Int).Value=companyid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitId", SqlDbType.Int).Value=unitid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitAmount", SqlDbType.Float).Value=unitamount;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitServiceTax", SqlDbType.Float).Value=servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_PaidAmount", SqlDbType.Float).Value=totalamount;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_PaymentType", SqlDbType.Int).Value=paymenttype;
            if(paymenttype==1)
            {
                MySqlComm.Parameters.Add("@fGPtbl_PDA_CashReceiptNo", SqlDbType.VarChar, 50).Value=cashreceiptno;
            }
            else if(paymenttype==2 || paymenttype==3)
            {
                MySqlComm.Parameters.Add("@fGPtbl_PDA_ChequeDDNo", SqlDbType.VarChar, 50).Value=chequeddno;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_BankName", SqlDbType.VarChar, 200).Value=bankname;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_BankBranch", SqlDbType.VarChar, 200).Value=bankbranch;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_ChequeDD_Date", SqlDbType.DateTime).Value=chequedddate;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_Filename", SqlDbType.VarChar, 200).Value=filename;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_Filepath", SqlDbType.VarChar, 200).Value=filepath;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_PayableAt", SqlDbType.VarChar, 100).Value=payableat;
            }
            MySqlComm.Parameters.Add("@userid", SqlDbType.Int).Value=userid;

            SqlParameter outVar=new SqlParameter("@Varoutput", SqlDbType.Int);
            outVar.Direction=ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVar);
            MySqlComm.ExecuteNonQuery();
            outVariable=Convert.ToInt32(outVar.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message +"  "+ ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return outVariable;
    }
    */

    public int Insert_PaymentDetail(int formtypeid, int companyid, int unitid, float unitamount, float servicetax,
float totalamount, int paymenttype, string chequeddno, string cashreceiptno, string bankname, string bankbranch,
string chequedddate, string filename, string filepath, string payableat, int userid, int FundTransferType, string UtrNumber)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_Insert_Paymentdetail", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PDA_FormTypeId", SqlDbType.Int).Value = formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitAmount", SqlDbType.Float).Value = unitamount;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_UnitServiceTax", SqlDbType.Float).Value = servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_PaidAmount", SqlDbType.Float).Value = totalamount;
            MySqlComm.Parameters.Add("@fGPtbl_PDA_PaymentType", SqlDbType.Int).Value = paymenttype;
            if (paymenttype == 1)
            {
                MySqlComm.Parameters.Add("@fGPtbl_PDA_CashReceiptNo", SqlDbType.VarChar, 50).Value = cashreceiptno;
            }
            else if (paymenttype == 2 || paymenttype == 3)
            {
                MySqlComm.Parameters.Add("@fGPtbl_PDA_ChequeDDNo", SqlDbType.VarChar, 50).Value = chequeddno;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_BankName", SqlDbType.VarChar, 200).Value = bankname;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_BankBranch", SqlDbType.VarChar, 200).Value = bankbranch;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_ChequeDD_Date", SqlDbType.DateTime).Value = chequedddate;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_Filename", SqlDbType.VarChar, 200).Value = filename;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_Filepath", SqlDbType.VarChar, 200).Value = filepath;
                MySqlComm.Parameters.Add("@fGPtbl_PDA_PayableAt", SqlDbType.VarChar, 100).Value = payableat;
            }



            else if (paymenttype == 4)
            {
                if (bankname != "") MySqlComm.Parameters.Add("@fGPtbl_PDA_BankName", SqlDbType.VarChar, 200).Value = bankname;
                if (bankbranch != "") MySqlComm.Parameters.Add("@fGPtbl_PDA_BankBranch", SqlDbType.VarChar, 200).Value = bankbranch;
                if (chequedddate != "") MySqlComm.Parameters.Add("@fGPtbl_PDA_ChequeDD_Date", SqlDbType.DateTime).Value = chequedddate;
                if (FundTransferType != 0) MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = FundTransferType;
                if (UtrNumber != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 200).Value = UtrNumber;
            }
            MySqlComm.Parameters.Add("@userid", SqlDbType.Int).Value = userid;
            SqlParameter outVar = new SqlParameter("@Varoutput", SqlDbType.Int);
            outVar.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVar);
            MySqlComm.ExecuteNonQuery();
            outVariable = Convert.ToInt32(outVar.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return outVariable;
    }


    public DataSet Fetch_CompanyPAN_Status_ByCompanyId(int companyid)
    {

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchCompanyStatus", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in Fetch_CompanyPAN_Status_ByCompanyId() " + ex.Message+ "   "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    #region VINAY SHUKLA
    public DataSet Company_Details(int processid, string Searchtext, int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetchdata", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@ProcessId", SqlDbType.Int).Value = processid;
            MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar).Value = Searchtext;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
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


    public DataSet Company_Store_Details(int companyid, int processid)
    {
        try
        {
            MyDs = new DataSet();
            //MyAdp = new SqlDataAdapter("gp_sp_test_store_19march205", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("Gpsp_fetchstorebyCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = companyid;

            MyAdp.SelectCommand.Parameters.Add("@processid", SqlDbType.Int).Value = processid;
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


    //  Function to Insert ,update Unitstore details or unitstoredetails of amendment    On  22/5/2013


    public int f_Insert_Update_Store_details_admin(int proceeid, int amendid, int storid, string sno, int storeunitid, string productdes,
  string ref_bis_specification, string modelbrand, string limitingsize, string anualprodcapacity,
  int noofshifts, string qualitativecapcity, string quantative, string hscode, //string pmps,
  int addby, int editby, int seqno,string hsnpctype)
    {
        int outresult = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_update_store_admin", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.AddWithValue("@processid", proceeid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_Id", storid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_SNo", sno);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_SeqNo", seqno);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_UnitId", storeunitid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_DescriptionStoresProducts", productdes);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_RefOfBISSpecification", ref_bis_specification);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_ModelBrand", modelbrand);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_LimitingSizeCapacityRating", limitingsize);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_AnnualProductionCapacity", anualprodcapacity);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_NoOfShifts", noofshifts);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_QualitativeCapacity", qualitativecapcity);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_QuantitativeCapacity", quantative);
            //MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_Pmps", pmps);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_HsCode", hscode);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_CUser", addby);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_EUser", editby);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnitAmendment_Id", amendid);
              //MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnitAmendment_Id", amendid);
              MySqlComm.Parameters.AddWithValue("@HS_NPCtype", hsnpctype);
            SqlParameter outVal = new SqlParameter("@RESULT", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            outresult = Convert.ToInt32(outVal.Value);
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }

        return outresult;
    }

    public int f_get_amendmentid(int companyid)
    {
        int amendid = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_cunit_amendmentid", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.AddWithValue("@CompanyId", companyid);
            amendid = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return amendid;
    }

    public DataTable Company_Fetch_FactoryLocation_AMENDMENT(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByAmendCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDt;
    }

    public int f_deactivatestore(int processid, int storeid)
    {
        int result = 0;
        try
        {
            MySqlComm = new SqlCommand("Gpsp_deactivatestore", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.AddWithValue("@processid", processid);
            MySqlComm.Parameters.AddWithValue("@storeid", storeid);
            SqlParameter outVal = new SqlParameter("@RESULT", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            result = Convert.ToInt32(outVal.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return result;
    }

    // Fetch Monetary Limit according to process id 

    public DataSet Company_Fetch_Monetary_dynamic(int CompanyId, int processid)
    {
        try
        {
            MyDs = new DataSet();
            // gp_sp_monetryhistory_20140327
            MyAdp = new SqlDataAdapter("gp_sp_monetryhistory_20140327", DBManager.ConnectToDB());
       //    MyAdp = new SqlDataAdapter("gp_sp_monetryhistory", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.SelectCommand.Parameters.Add("@processid", SqlDbType.Int).Value = processid;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  " +ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public int f_update_monetary_trunover(string dynamicquery)
    {
        int result = 0;
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Update_turnover_monetary", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.AddWithValue("@str ", dynamicquery);
            result = MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return result;
    }

    #endregion

    public void Update_LastTurnover(int companyid, float newturnover)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_Update_LastYearTurnover_ByCompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@newturnover", SqlDbType.Float).Value = newturnover;
            MySqlComm.ExecuteNonQuery();
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
    }

    public DataSet Fetch_Factorydata_BycompanyId(int companyid)
    {
        MyDs=new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CompanyEdit_FetchFactorydata_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public int Fetch_Factorycount_BycompanyId(int companyid)
    {
        int result = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_FetchCompanyCount_CompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value=companyid;
            result = Convert.ToInt32(MySqlComm.ExecuteScalar());
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
        return result;
    }

    public int Update_FactoryPmps(string Pmpsvalue, int userid,string comapnyId,string xmldata)
    {
        int result = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_UpdateStorePMPS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PmpsValue", SqlDbType.VarChar, 2000).Value = Pmpsvalue;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.VarChar, 200).Value = comapnyId;
            MySqlComm.Parameters.Add("@xmldata", SqlDbType.VarChar, 5000).Value = xmldata;
            SqlParameter outvar = new SqlParameter("@OutResult", SqlDbType.Int);
            outvar.Direction = ParameterDirection.Output;

            MySqlComm.Parameters.Add(outvar);
            MySqlComm.ExecuteNonQuery();
            result=Convert.ToInt32(outvar.Value);
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
        return result;
    }

    public DataTable Fetch_CompanyLocationPmps(int companyid)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CompanyEdit_Fetch_CompanyLocationPmps", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"   " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs.Tables[0];
    }

    //FUNCTION TO CHECK EDIT MONETARY LIMIT WHETHER DATA EXISTS IN ML & TO TABLE
    public DataTable Fetch_Edit_monetarylimit(int companyid, int processid)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GP_SP_view_modified_monetaryturnoverdetails", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@compid", SqlDbType.Int).Value = companyid;
            MyAdp.SelectCommand.Parameters.Add("@formtypeid", SqlDbType.Int).Value = processid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "   " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs.Tables[1];
    }

    //FUNCTION TO CHECK Store Sequence
    public DataTable Check_store_sequence(int unitid, int seqno)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("Gp_sp_CheckStoresequence", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@unitid", SqlDbType.Int).Value = unitid;
            MyAdp.SelectCommand.Parameters.Add("@seqno", SqlDbType.Int).Value = seqno;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs.Tables[0];
    }


    public DataSet Fetch_Castwise_reportCastId()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GpSp_Fetch_CategoryCast_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
}
