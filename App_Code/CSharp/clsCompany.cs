using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Web.UI;
/// <summary>
/// Summary description for clsCompany
/// </summary>
public class clsCompany
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

	public clsCompany()
	{
		//
		// TODO: Add constructor logic here
		//
	}

     // check Aadhar number 
    public int Company_AadhernoCheck(string  Aadharnumber)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_checkAadharnumber", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.Char, 12).Value = Aadharnumber;
            //MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return Status;
    }

    //public int Company_PANnoCheck(string PANno, int branchid)
    public int Company_PANnoCheck(string PANno)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANno", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            //MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return Status;
    }



    public DataSet Company_PANnoCheck_Modified(string PANno)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CheckPANno_modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.Char,10).Value =PANno;
         
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

    //  Check Addhar number which is not duplicate in table 

    public DataSet Company_adharCheck_Modified(string Aadharnumber)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_Sp_Check_Adhar_number", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.Char, 20).Value = Aadharnumber;

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


    public Hashtable Company_Insert_Step1(string fGPtbl_Company_GPReferenceNo, string fGPtbl_Company_Name, string fGPtbl_Company_PAN,
       string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId,
       int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax,
       string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address, string fGPtbl_CUnit_1B_Factory_Zip,
       int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax,
       string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
       int fGPtbl_CUnit_BranchId, string fGPtbl_CUnitBranch_Address, string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId,
       string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax, string fGPtbl_CUnitBranch_Email, int UserId, int provision, int enterprisedefnition,
       string comapnymobile, string Proposal_date, string Aadhar_number, bool fGPtbl_Company_MSME, bool fGPtbl_Company_ShareInfo, bool fGPtbl_IsAyush, string GstNumber, int fGtbl_Company_IsSCST_Initiative)
    {
        int UnitId = 0;
        int CompanyId = 0;


        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step1_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_GPReferenceNo", SqlDbType.VarChar, 500).Value = fGPtbl_Company_GPReferenceNo;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.Char, 10).Value = fGPtbl_Company_PAN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = comapnymobile;
            if (fGPtbl_Company_HeadOffice_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Email;
                                                                                                          
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_1B_Factory_Address;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_1B_Factory_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Phone;
            if (fGPtbl_CUnit_1B_Factory_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Email;

            //Code added by Anupam 18 January 2012

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
            MySqlComm.Parameters.Add("fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = fGPtbl_CUnit_BranchId;
            if (fGPtbl_CUnitBranch_Address.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnitBranch_Address;
            if (fGPtbl_CUnitBranch_Zip.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnitBranch_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_StateId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_CityId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_CityId;
            if (fGPtbl_CUnitBranch_Phone.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Phone;
            if (fGPtbl_CUnitBranch_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Fax;
            if (fGPtbl_CUnitBranch_Email.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Email;

            MySqlComm.Parameters.Add("@fGPtbl_Company_MSME", SqlDbType.Bit).Value = fGPtbl_Company_MSME;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ShareInfo", SqlDbType.Bit).Value = fGPtbl_Company_ShareInfo;
            MySqlComm.Parameters.Add("@fGPtbl_IsAyush", SqlDbType.Bit).Value = fGPtbl_IsAyush;
            
            //FOR PROVISIONAL COMPANY
            MySqlComm.Parameters.Add("@fGPtbl_Company_provision", SqlDbType.Int).Value = provision;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            if (Proposal_date != "") MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar, 50).Value = Proposal_date;
            if (Aadhar_number != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = Aadhar_number;

            //Dhiraj
            if (GstNumber != "") MySqlComm.Parameters.Add("@fGtbl_Company_GSTNumber", SqlDbType.VarChar, 50).Value = GstNumber;

            //Dhiraj NSIC 19072017
            MySqlComm.Parameters.Add("@fGtbl_Company_IsSCST_Initiative", SqlDbType.Int).Value = fGtbl_Company_IsSCST_Initiative;



            SqlParameter outCompany = new SqlParameter("@CompanyId", SqlDbType.Int);
            outCompany.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompany);

            SqlParameter outUnit = new SqlParameter("@UnitId", SqlDbType.Int);
            outUnit.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outUnit);

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            CompanyId = Convert.ToInt32(outCompany.Value);
            UnitId = Convert.ToInt32(outUnit.Value);
            ErrorId = Convert.ToInt32(outError.Value);

            hstbl.Add("CompanyId", CompanyId);
            hstbl.Add("UnitId", UnitId);
            hstbl.Add("ErrorId", ErrorId);

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

        return hstbl;
    }

    public int MasterBranch_Fetch_BranchId(int UserId)
    {
        int BranchId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterBranch_ByUserId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Char, 10).Value = UserId;
            BranchId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return BranchId;
    }

    public int Company_Fetch_MaxId()
    {
        int MaxId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Fetch_MaxId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MaxId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        MaxId = MaxId + 1;
        return MaxId;
    }

    public string MasterBranch_Fetch_Code(int BranchId)
    {
        string BranchCode = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterBranch_Code", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            BranchCode = MySqlComm.ExecuteScalar().ToString();
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
        return BranchCode;
    }

    public SqlDataReader Company_Fetch_ByPANno_Step1(string PANno)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_Fetch_ByPANno_Step1_Grid(string PANno)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Grid", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }



    public int Company_Fetch_BranchId(int Unitid)
    {
        int BranchId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Fetch_BranchByUnitId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Char, 10).Value = Unitid;
            BranchId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return BranchId;
    }


    public int Company_Update_Step1(string fGPtbl_Company_Name, string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId,
         int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax, string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address,
         string fGPtbl_CUnit_1B_Factory_Zip, int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax,
         string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId, string fGPtbl_CUnitBranch_Address,
         string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId, string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax, string fGPtbl_CUnitBranch_Email,
         int UserId, int CompanyId, int UnitId, int provision, int enterprisedefnition, string Companymobile, string Proposal_date, string Aadhar_number, bool fGPtbl_Company_MSME, bool fGPtbl_Company_ShareInfo, string GST_number, int fGtbl_Company_IsSCST_Initiative)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Step1_Regi_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = Companymobile;

            if (fGPtbl_Company_HeadOffice_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Email;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_1B_Factory_Address;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_1B_Factory_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Phone;
            if (fGPtbl_CUnit_1B_Factory_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Email;

            //Code added by Anupam 20 January 2012
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;

            if (fGPtbl_CUnitBranch_Address.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnitBranch_Address;
            if (fGPtbl_CUnitBranch_Zip.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnitBranch_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_StateId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_CityId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_CityId;
            if (fGPtbl_CUnitBranch_Phone.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Phone;
            if (fGPtbl_CUnitBranch_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Fax;
            if (fGPtbl_CUnitBranch_Email.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Email;

            //FOR PROVISIONAL COMPANY
            MySqlComm.Parameters.Add("@fGPtbl_Provisional", SqlDbType.Int).Value = provision;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MySqlComm.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            if (Proposal_date != "") MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar, 50).Value = Proposal_date;
            if (Aadhar_number!="") MySqlComm.Parameters.Add("@Aadhar_number", SqlDbType.VarChar, 50).Value = Aadhar_number;

            if (GST_number != "") MySqlComm.Parameters.Add("@GST_number", SqlDbType.VarChar, 50).Value = GST_number;


            MySqlComm.Parameters.Add("@fGPtbl_Company_MSME", SqlDbType.Bit).Value = fGPtbl_Company_MSME;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ShareInfo", SqlDbType.Bit).Value = fGPtbl_Company_ShareInfo;

            //Dhiraj NSIC 19072017
            MySqlComm.Parameters.Add("@fGtbl_Company_IsSCST_Initiative", SqlDbType.Int).Value = fGtbl_Company_IsSCST_Initiative;
            

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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


    public DataTable Company_Fetch_Constitution()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_ConstitutionFetch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public int Company_Insert_Step2(int fGPtbl_CUnit_Id, int fGPtbl_CUnit_CompanyId, string fGPtbl_CUnit_SSIRegistrationNo, string fGPtbl_CUnit_SSIRegistrationDate, string fGPtbl_CUnit_3A, string fGPtbl_CUnit_3B, int fGPtbl_CUnit_3C123_ConstitutionId, string fGPtbl_CUnit_3C5,  int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step2", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = fGPtbl_CUnit_CompanyId;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_SSIRegistrationNo", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_SSIRegistrationNo;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_SSIRegistrationDate", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_SSIRegistrationDate;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3A", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_3A;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3B", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_3B;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C123_ConstitutionId", SqlDbType.Int).Value = fGPtbl_CUnit_3C123_ConstitutionId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C5", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3C5;

            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Name_1", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_1;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Address_1", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_1;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Zip_1", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_1;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_StateId_1", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_1;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_CityId_1", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_1;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Phone_1", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_1;

            //if (fGPtbl_CUnit_Directors_Name_2.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_2.ToString().Trim() != "")
            //{
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Name_2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_2;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Address_2", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_2;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Zip_2", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_2;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_StateId_2", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_2;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_CityId_2", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_2;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Phone_2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_2;
            //}

            //if (fGPtbl_CUnit_Directors_Name_3.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_3.ToString().Trim() != "")
            //{
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Name_3", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_3;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Address_3", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_3;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Zip_3", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_3;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_StateId_3", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_3;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_CityId_3", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_3;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Phone_3", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_3;
            //}

            //if (fGPtbl_CUnit_Directors_Name_4.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_4.ToString().Trim() != "")
            //{
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Name_4", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_4;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Address_4", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_4;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Zip_4", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_4;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_StateId_4", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_4;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_CityId_4", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_4;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Phone_4", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_4;
            //}

            //if (fGPtbl_CUnit_Directors_Name_5.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_5.ToString().Trim() != "")
            //{
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Name_5", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_5;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Address_5", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_5;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Zip_5", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_5;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_StateId_5", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_5;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_CityId_5", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_5;
            //    MySqlComm.Parameters.Add("@fGPtbl_CUnit_Directors_Phone_5", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_5;
            //}

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public SqlDataReader Company_Fetch_ByUnit_Step2(int UnitId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByUnit_Step2", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = UnitId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_FetchDirectorByUnit_Step2(int UnitId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchDirectorByComapny_Step2", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CompanyId", SqlDbType.Int).Value = UnitId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public DataTable Company_Fetch_RegisteredUnder()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_RegisteredUnderFetch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;

    }

    public DataTable Company_Fetch_StoreByUnitId(int UnitId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_StoreByUnitId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;

            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public DataTable Company_Fetch_CategoryArea()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CategoryArea", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }
    
    public DataTable Company_Fetch_CategoryCast()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CategoryCast", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public DataTable Company_Fetch_CategoryUnitSize()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CategoryUnitSize", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public SqlDataReader Company_Fetch_ByUnit_Step3(int UnitId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByUnit_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = UnitId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }


    public DataTable Fetch_InspectionAgency()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_InspectionAgency", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public int Company_Insert_Step3(int fGPtbl_CUnit_Id, int fGPtbl_CUnit_CompanyId, int fGPtbl_CUnit_3D, string fGPtbl_CUnit_3E, string fGPtbl_CUnit_3F, string fGPtbl_CUnit_3G, string fGPtbl_CUnit_3H, string fGPtbl_CUnit_3I, string fGPtbl_CUnit_3J, string fGPtbl_CUnit_3K1, string fGPtbl_CUnit_3L, string fGPtbl_CUnit_3M, string fGPtbl_CUnit_3N, string fGPtbl_CUnit_3O, string fGPtbl_CUnit_4A, string fGPtbl_CUnit_4B, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = fGPtbl_CUnit_CompanyId;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3D", SqlDbType.Int).Value = fGPtbl_CUnit_3D;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3E", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3E;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3F", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3F;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3G", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3G;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3H", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3H;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3I", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3I;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3J", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3J;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3K1", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3K1;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3L", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3L;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3M", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3M;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3N", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3N;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3O", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_3O;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_4A", SqlDbType.VarChar, 8000).Value = fGPtbl_CUnit_4A;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_4B", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_4B;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Update_Step3_Store(int fGPtbl_CUnit_Store_Id, int fGPtbl_CUnit_Store_UnitId, string fGPtbl_CUnit_Store_DescriptionStoresProducts, string fGPtbl_CUnit_Store_RefOfBISSpecification, string fGPtbl_CUnit_Store_ModelBrand, string fGPtbl_CUnit_Store_LimitingSizeCapacityRating, string fGPtbl_CUnit_Store_AnnualProductionCapacity, int fGPtbl_CUnit_Store_NoOfShifts, string fGPtbl_CUnit_Store_QualitativeCapacity, string fGPtbl_CUnit_Store_QuantitativeCapacity, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Store_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Store_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_DescriptionStoresProducts", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_DescriptionStoresProducts;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_RefOfBISSpecification", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_RefOfBISSpecification;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_ModelBrand", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_ModelBrand;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_LimitingSizeCapacityRating", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_LimitingSizeCapacityRating;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_AnnualProductionCapacity", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_AnnualProductionCapacity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_NoOfShifts", SqlDbType.Int).Value = fGPtbl_CUnit_Store_NoOfShifts;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_QualitativeCapacity", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Store_QualitativeCapacity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_QuantitativeCapacity", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Store_QuantitativeCapacity;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Insert_Step3_Store(int fGPtbl_CUnit_Store_UnitId, string fGPtbl_CUnit_Store_DescriptionStoresProducts, string fGPtbl_CUnit_Store_RefOfBISSpecification, string fGPtbl_CUnit_Store_ModelBrand, string fGPtbl_CUnit_Store_LimitingSizeCapacityRating, string fGPtbl_CUnit_Store_AnnualProductionCapacity, int fGPtbl_CUnit_Store_NoOfShifts, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_DescriptionStoresProducts", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_DescriptionStoresProducts;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_RefOfBISSpecification", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_RefOfBISSpecification;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_ModelBrand", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_ModelBrand;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_LimitingSizeCapacityRating", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_LimitingSizeCapacityRating;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_AnnualProductionCapacity", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_AnnualProductionCapacity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_NoOfShifts", SqlDbType.Int).Value = fGPtbl_CUnit_Store_NoOfShifts;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Delete_Step3_Store(int StoreId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Delete_Store_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@StoreId", SqlDbType.Int).Value = StoreId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Step3_Store_SequenceUp(int fGPtbl_CUnit_Store_Id, int fGPtbl_CUnit_Store_UnitId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Store_SequenceUp", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Store_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError);

        }

        catch(Exception ex)
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

    public int Company_Step3_Store_SequenceDown(int fGPtbl_CUnit_Store_Id, int fGPtbl_CUnit_Store_UnitId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Store_SequenceDown", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Store_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError);

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


    public int Company_Insert_Step5_Documents(int fGPtbl_DU_RenewalAmendmentId, int fGPtbl_DU_UnitId, int fGPtbl_DU_FormtypeId, string fGPtbl_DU_Name, string fGPtbl_DU_FileName, string fGPtbl_DU_FilePath, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step5_Documents", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_DU_RenewalAmendmentId", SqlDbType.Int).Value = fGPtbl_DU_RenewalAmendmentId;
            MySqlComm.Parameters.Add("@fGPtbl_DU_UnitId", SqlDbType.Int).Value = fGPtbl_DU_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FormtypeId", SqlDbType.Int).Value = fGPtbl_DU_FormtypeId;

            MySqlComm.Parameters.Add("@fGPtbl_DU_Name", SqlDbType.VarChar,1000).Value = fGPtbl_DU_Name;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FileName", SqlDbType.VarChar,1000).Value = fGPtbl_DU_FileName;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FilePath", SqlDbType.VarChar, 2000).Value = fGPtbl_DU_FilePath;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Insert_Step4(int fGPtbl_CUnit_Id, int fGPtbl_CUnit_CompanyId, string fGPtbl_CUnit_5A1, string fGPtbl_CUnit_5A2, string fGPtbl_CUnit_5BB1, string fGPtbl_CUnit_5BB2, string fGPtbl_CUnit_5C, float fGPtbl_CUnit_6A1, float fGPtbl_CUnit_6A2,float fGPtbl_CUnit_6A3, float fGPtbl_CUnit_6B, float fGPtbl_CUnit_6B2, string fGPtbl_CUnit_7A, string fGPtbl_CUnit_7B, string fGPtbl_CUnit_7C, string fGPtbl_CUnit_7D, string fGPtbl_CUnit_7E, string fGPtbl_CUnit_8, string fGPtbl_CUnit_9, string fGPtbl_CUnit_10, string fGPtbl_CUnit_11, string fGPtbl_CUnit_12A, string fGPtbl_CUnit_12B,string fGPtbl_CUnit_12C, string fGPtbl_CUnit_13A, string fGPtbl_CUnit_13B, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step4_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = fGPtbl_CUnit_CompanyId;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_5A1", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_5A1;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_5A2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_5A2;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_5BB1", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_5BB1;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_5BB2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_5BB2;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_5C", SqlDbType.VarChar, 8000).Value = fGPtbl_CUnit_5C;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_6A1", SqlDbType.Float).Value = fGPtbl_CUnit_6A1;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_6A2", SqlDbType.Float).Value = fGPtbl_CUnit_6A2;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_6A3", SqlDbType.Float).Value = fGPtbl_CUnit_6A3;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_6B", SqlDbType.Float).Value = fGPtbl_CUnit_6B;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_6B2", SqlDbType.Float).Value = fGPtbl_CUnit_6B2;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_7A", SqlDbType.VarChar, 8000).Value = fGPtbl_CUnit_7A;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_7B", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_7B;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_7C", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_7C;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_7D", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_7D;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_7E", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_7E;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_8", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_8;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_9", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_9;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_10", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_10;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_11", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_11;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_12A", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_12A;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_12B", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_12B;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_12C", SqlDbType.VarChar, 50).Value = fGPtbl_CUnit_12C;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_13A", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_13A;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_13B", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_13B;


            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public DataSet Company_Fetch_ByUnit_Step4(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchByCompanyId_Step4_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = companyid;

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


    public SqlDataReader Company_FetchAgency_ByBranch_Step6(int BranchId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchAgencyByBranch_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_FetchAgencyPaymentSlab_Step6(int AgencyId, int FormtypeId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchInspectionSlab_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_IPS_AgencyId", SqlDbType.Int).Value = AgencyId;
            MySqlComm.Parameters.Add("@fGPtbl_IPS_FormtypeId", SqlDbType.Int).Value = FormtypeId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_FetchBranchPayment_ByFormtypeId_Step6(int FormTypeId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchBranchSlab_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = FormTypeId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_FetchBranchPayment_ById_Step6(int FormTypeId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchBranchSlabById_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MFee_FormTypeId", SqlDbType.Int).Value = FormTypeId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public SqlDataReader Company_Fetch_ByBranch_Step6(int BranchId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByBranch_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    //INSERT PAYMENT DETAILS
    public int Company_Insert_Step6(int UnitId, int FeeId, int AgencyId, int SlabId, int FormTypeId,
       int UserId, int Branchid, decimal Amount)
    {
        int tblId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step6", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MySqlComm.Parameters.Add("@FeeId", SqlDbType.Int).Value = FeeId;
            MySqlComm.Parameters.Add("@AgencyId", SqlDbType.Int).Value = AgencyId;
            MySqlComm.Parameters.Add("@SlabId", SqlDbType.Int).Value = SlabId;
            MySqlComm.Parameters.Add("@FormTypeId", SqlDbType.Int).Value = FormTypeId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = Branchid;
            SqlParameter outError = new SqlParameter("@tblId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            tblId = Convert.ToInt32(outError.Value);
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
        return tblId;
    }


    public SqlDataReader Company_Fetch_ByUnit_Step7(int PaymentDetailId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_PaymentDetail_ByPaymentDetailId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = PaymentDetailId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public string Company_Insert_Step7(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType, double @fGPtbl_PD_PaidAmount,
        string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName, string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate,
        string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath, string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,
        string PaymentRemarks
        )
    {
        string Returnvalue = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step7", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

            if (fGPtbl_PD_ChequeDDNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
            }

            if (fGPtbl_PD_BankName != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
            }

            if (fGPtbl_PD_BankBranch != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
            }

            if (fGPtbl_PD_PaybalAt != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
            }

            if (fGPtbl_PD_ChequeDDate != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
            }

            if (fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
            }

            if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
            }

            if (fGPtbl_PD_CashReceiptNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            }

            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
            MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            if (PaymentRemarks != "")
            {
                MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 50).Value = PaymentRemarks;
            }           

            SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();

            Returnvalue = outError.Value.ToString();
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
        return Returnvalue;
    }



    #region WAITING COMPANIES

    //FETCH ALL COMPANY AND ITS UNIT
    public DataSet Company_WaitingCompany_Registration_FetchAllCompanies_Dataset(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_Company_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;

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

    //FETCH PARTICULAR COMPANY AND IT'S UNITS BY USING COMPANY ID
    public DataSet Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;

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

    public int Company_WaitingCompany_Registration_Rejection_(int CUnitid, int Formtypeid, string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Waiting_Update_RejectCompany_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = CUnitid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_RR_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_RR_Reason", SqlDbType.VarChar, 5000).Value = Reason;
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

    public Hashtable Company_WaitingCompany_Registration_Accept(int CUnitid, int InspUserId, int UserId)
    {
        string email = "";

        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Waiting_Update_AcceptCompany_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = CUnitid;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@InspUserId", SqlDbType.Int).Value = InspUserId;


            SqlParameter outValue = new SqlParameter("@Output", SqlDbType.Int);
            outValue.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outValue);

            SqlParameter outEmail = new SqlParameter("@AgencyEmailId", SqlDbType.VarChar, 500);
            outEmail.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outEmail);

            SqlParameter CompanyName = new SqlParameter("@CompanyName", SqlDbType.VarChar, 2000);
            CompanyName.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(CompanyName);

            SqlParameter BranchName = new SqlParameter("@BranchName", SqlDbType.VarChar, 1000);
            BranchName.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(BranchName);

            MySqlComm.ExecuteNonQuery();

            email = outEmail.Value.ToString();
            ErrorId = Convert.ToInt32(outValue.Value);

            hstbl.Add("email", email);
            hstbl.Add("ErrorId", ErrorId);
            hstbl.Add("CompanyName", CompanyName.Value.ToString());
            hstbl.Add("BranchName", BranchName.Value.ToString());

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

        return hstbl;
    }

    //TO FETCH UPLOADED DOCUMENT BY UNIT ID
    public DataSet Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(int CUnitid, int Formtypeid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_DocumentUploaded_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_DU_UnitId", SqlDbType.Int).Value = CUnitid;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_DU_FormtypeId", SqlDbType.Int).Value = Formtypeid;

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


    public int Company_WaitingCompany_Insert_DocumentSubmitted(string documentstr, int fGPtbl_DocumentSubmitted_CompanyId, int fGPtbl_DocumentSubmitted_FormtypeId, int Userid)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Waiting_Insert_CunitDocumentSubmited", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            /*MySqlComm.Parameters.Add("@fGPtbl_DocumentSubmitted_DocId", SqlDbType.Int).Value = fGPtbl_DocumentSubmitted_DocId;
            MySqlComm.Parameters.Add("@fGPtbl_DocumentSubmitted_DocName", SqlDbType.VarChar, 200).Value = fGPtbl_DocumentSubmitted_DocName;
             */
            MySqlComm.Parameters.Add("@Alldocument", SqlDbType.Text).Value = documentstr;
            MySqlComm.Parameters.Add("@fGPtbl_DocumentSubmitted_CompanyId", SqlDbType.Int).Value = fGPtbl_DocumentSubmitted_CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_DocumentSubmitted_FormtypeId", SqlDbType.Int).Value = fGPtbl_DocumentSubmitted_FormtypeId;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = Userid;
            //MySqlComm.Parameters.Add("@fGPtbl_DocumentSubmitted_EUser", SqlDbType.Int).Value = fGPtbl_DocumentSubmitted_EUser;



            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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


    //FETCH FACTORY ADDRESS BY COMPANY ID FOR THE POP
    public DataSet Company_WaitingCompany_Registration_FetchByCompanyIdPopup_Dataset(int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = Companyid;

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

    #endregion

    #region ACCEPTED COMPANIES

    //INSPECTION DATE TO BE ISSUED 13FEB
    public DataSet Company_AcceptedCompany_InspectionDatetobeIssued_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Accepted_Fetch_DatetobeIssued", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;
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


    //DATE ALREADY ISSUED  13FEB
    public DataSet Company_AcceptedCompany_InspectionDatealreadyIssued_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Accepted_Fetch_DatealreadyIssued", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;

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


    //TO FETCH ALL UNITS WHOSE INSPECTION IS COMPLETE FOR REGISTRATION
    public DataSet Company_ICompleteRegistration_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_ICompleteRegistration_Fetch_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;

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


    //TO UPDATE TABLE FOR UNIT BY BRANCH IN CASE OF REJECTION
    public int Company_ICompleteRegistration_Rejection(int CUnitid, int Formtypeid, string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_ICompleteRegistration_Update_Reject_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = CUnitid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_RR_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_RR_Reason", SqlDbType.VarChar, 5000).Value = Reason;
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



    //TO UPDATE TABLE FOR UNIT BY ID IN CASE OF ACCEPT
    public int Company_ICompleteRegistration_Accept(int Companyid, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_ICompleteRegistration_Update_CUnitRegStatus_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
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
    //

    //TO FETCH DATA TO SET MONETARY LIMIT FOR BRANCH ACCOUNT
    public DataSet Company_MonetaryLimit_Fetch_Dataset(int UserId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Registration_Fetch_All", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
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

    //TO INSERT DATA TO FOR MONETARY LIMIT FOR BRANCH ACCOUNT
    public int Company_MonetaryLimit_Update(int Monetaryid, int Formtypeid, int CompanyId, double Monetarylimit, int UserId)
    {

        try
        {
            RandomKeyGenerator objkey = new RandomKeyGenerator();
            MySqlComm = new SqlCommand("GPsp_Company_Monetary_Registration_Update_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Password", SqlDbType.NVarChar).Value = objkey.Generate();
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


    //TO FETCH DATA MONETARY LIMIT ALREADY SET BY BRANCH ACCOUNT FOR ZONAL HEAD
    public DataSet Company_MonetaryLimit_FetchZonalHeadRegistration_Dataset(int UserId, int roleid, int deptid)
    {
        try
        {
            MyDs = new DataSet();
            //MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_ZonalHead_Registration_Fetch_All", DBManager.ConnectToDB());
            //MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_ZonalHead_Registration_Fetch_All_Modified", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_ZonalHead_FReshRegistration_FetchFor_BHZHO", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
            MyAdp.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = deptid;
            //MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = FormtypeId;
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


    //TO FETCH DATA OF MONETARY LIMIT
    public DataSet Company_MonetaryLimit_FetchById_Dataset(int monetaryid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Fetch_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = monetaryid;
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

    //TO FETCH ALL COMPANIES WHOSE INSPECTION REPORT IS SUBMITTED
    public DataSet Company_ICompleteRegistrationCompany_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_ICompleteRegistration_Fetch_AllCompany", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;
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

    //TO INSERT DATA TO FOR MONETARY LIMIT FOR BRANCH ACCOUNT
    public int Company_MonetaryLimitBranchAccount_Update(int amendmentid, int CompanyId, int Monetaryid, int Formtypeid, string strunit1, string strunit2, string strunit3, string strunit4, string strunit5, double Monetarylimit, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Monetary_Registration_Update_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_UnitId", SqlDbType.Int).Value = Unitid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = CompanyId;
            if (amendmentid != 0) MySqlComm.Parameters.Add("@fGPtbl_Company_ML_RenewalAmendmentId", SqlDbType.Int).Value = amendmentid;
            MySqlComm.Parameters.Add("@strunit1", SqlDbType.VarChar, 500).Value = strunit1;
            MySqlComm.Parameters.Add("@strunit2", SqlDbType.VarChar, 500).Value = strunit2;
            MySqlComm.Parameters.Add("@strunit3", SqlDbType.VarChar, 500).Value = strunit3;
            MySqlComm.Parameters.Add("@strunit4", SqlDbType.VarChar, 500).Value = strunit4;
            MySqlComm.Parameters.Add("@strunit5", SqlDbType.VarChar, 500).Value = strunit5;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
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

    public DataTable Company_ICompleteRegistration_Dataset(int companyid, int Userid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_ICompleteRegistration_Fetch_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = Userid;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyid;

            MyAdp.Fill(MyDt);
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
        return MyDt;
    }




    #endregion


    #region Upload Documents


    public int CountMasterDoc()
    {
        int MaxId = 0;
        try
        {
            MySqlComm = new SqlCommand("select count(*) From dbo.GPtbl_MasterDocuments Where fGPtbl_MD_Delete=0", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.Text ;

            MaxId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        
        return MaxId;
    
    }

    public DataSet  Fetch_MasterDoc()
    {
       
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUploadDocument", DBManager.ConnectToDB());
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

    #endregion

    #region PanSSICheck
    public int Company_PanSSInoCheck(string SSIno, string PANno)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANnoSSIno", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
           // Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());

            MySqlComm.Parameters.Add("@SSIno", SqlDbType.Char, 500).Value = SSIno;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return Status;
    }

    #endregion

    public DataTable  Fetch_Max_Factory()
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_FactoryAllowed", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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

        DataTable dt = new DataTable();
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_Address", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_Zip", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_StateId", typeof(int));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_CityId", typeof(int));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_Phone", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_Fax", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_1B_Factory_Email", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_Id", typeof(string));
        dt.Columns.Add("fGPtbl_CUnit_Enterprise", typeof(int));
        int i = 1;
        while (i <= Status)
        {
            dt.Rows.Add("","",0,0,"","","",0,0);
            //dt.Rows.Add(" "," "," "," "," "," "," ");
            //dt.Rows.Add(" "," "," "," "," "," "," ");
            //dt.Rows.Add(" "," "," "," "," "," "," ");
            //dt.Rows.Add(" "," "," "," "," "," "," ");
            i = i + 1;

         }
        return dt;
    }

    public SqlDataReader Company_Fetch_ByCompany_Step2(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByCompany_Step2", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }


    public int Company_InsertDirector_Step2(int fGPtbl_CUnit_CompanyId, string fGPtbl_CUnit_Directors_Name_1, string fGPtbl_CUnit_Directors_Address_1, string fGPtbl_CUnit_Directors_Zip_1, int fGPtbl_CUnit_Directors_StateId_1, int fGPtbl_CUnit_Directors_CityId_1, string fGPtbl_CUnit_Directors_Phone_1, string fGPtbl_Company_Directors_Shareholder_1,
        string fGPtbl_CUnit_Directors_Name_2, string fGPtbl_CUnit_Directors_Address_2, string fGPtbl_CUnit_Directors_Zip_2, int fGPtbl_CUnit_Directors_StateId_2, int fGPtbl_CUnit_Directors_CityId_2, string fGPtbl_CUnit_Directors_Phone_2, string fGPtbl_Company_Directors_Shareholder_2,
        string fGPtbl_CUnit_Directors_Name_3, string fGPtbl_CUnit_Directors_Address_3, string fGPtbl_CUnit_Directors_Zip_3, int fGPtbl_CUnit_Directors_StateId_3, int fGPtbl_CUnit_Directors_CityId_3, string fGPtbl_CUnit_Directors_Phone_3, string fGPtbl_Company_Directors_Shareholder_3,
        string fGPtbl_CUnit_Directors_Name_4, string fGPtbl_CUnit_Directors_Address_4, string fGPtbl_CUnit_Directors_Zip_4, int fGPtbl_CUnit_Directors_StateId_4, int fGPtbl_CUnit_Directors_CityId_4, string fGPtbl_CUnit_Directors_Phone_4, string fGPtbl_Company_Directors_Shareholder_4,
        string fGPtbl_CUnit_Directors_Name_5, string fGPtbl_CUnit_Directors_Address_5, string fGPtbl_CUnit_Directors_Zip_5, int fGPtbl_CUnit_Directors_StateId_5, int fGPtbl_CUnit_Directors_CityId_5, string fGPtbl_CUnit_Directors_Phone_5, string fGPtbl_Company_Directors_Shareholder_5, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyDirector_Insert_Step2", DBManager.ConnectToDB());
            /* THIS PROCEDURE SHOULD IMPLEMENT
            MySqlComm = new SqlCommand("GPsp_Company_Director_Insert_Step2", DBManager.ConnectToDB());*/
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = fGPtbl_CUnit_CompanyId;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Name_1", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Address_1", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Zip_1", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_StateId_1", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CityId_1", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Phone_1", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_1;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Shareholder_1", SqlDbType.VarChar, 50).Value = fGPtbl_Company_Directors_Shareholder_1;

            if (fGPtbl_CUnit_Directors_Name_2.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_2.ToString().Trim() != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Name_2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Address_2", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Zip_2", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_StateId_2", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CityId_2", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Phone_2", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_2;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Shareholder_2", SqlDbType.VarChar, 50).Value = fGPtbl_Company_Directors_Shareholder_2;
            }

            if (fGPtbl_CUnit_Directors_Name_3.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_3.ToString().Trim() != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Name_3", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Address_3", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Zip_3", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_StateId_3", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CityId_3", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Phone_3", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_3;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Shareholder_3", SqlDbType.VarChar, 50).Value = fGPtbl_Company_Directors_Shareholder_3;
            }

            if (fGPtbl_CUnit_Directors_Name_4.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_4.ToString().Trim() != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Name_4", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Address_4", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Zip_4", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_StateId_4", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CityId_4", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Phone_4", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_4;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Shareholder_4", SqlDbType.VarChar, 50).Value = fGPtbl_Company_Directors_Shareholder_4;
            }

            if (fGPtbl_CUnit_Directors_Name_5.ToString().Trim() != null && fGPtbl_CUnit_Directors_Name_5.ToString().Trim() != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Name_5", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Name_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Address_5", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Directors_Address_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Zip_5", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_Directors_Zip_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_StateId_5", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_StateId_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_CityId_5", SqlDbType.Int).Value = fGPtbl_CUnit_Directors_CityId_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Phone_5", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Directors_Phone_5;
                MySqlComm.Parameters.Add("@fGPtbl_Company_Directors_Shareholder_5", SqlDbType.VarChar, 50).Value = fGPtbl_Company_Directors_Shareholder_5;
            }

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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


    public DataTable Company_Fetch_StoreByCompanyId(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_StoreByCompanyId_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Companyid", SqlDbType.Int).Value = CompanyId;

            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public DataTable Company_Fetch_FactoryLocation(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }


    //FETCH ALL CERTIFICATE LIST
    public DataTable Company_Fetch_CertificateAll(int branchid)
    {
        try
        {
            MyDt = new DataTable();
            //MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_count", DBManager.ConnectToDB());
          
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
          
            MyAdp.Fill(MyDt);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    //FETCH REGISTRATION NO. OF FACTORY BY COMPANY ID
    public Hashtable Company_Fetch_RegNo_ByCompanyId(int companyid)
    {
        string regno = "";
        int pntaccess;
        Hashtable hstbl=new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Certificate_fetch_RegNo_ByCompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyid;

            SqlParameter Outval = new SqlParameter("@Output", SqlDbType.VarChar, 35);
            Outval.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(Outval);

            SqlParameter printaccess = new SqlParameter("@fGPtbl_CERTIFICATE_PrintAccess", SqlDbType.Int);
            printaccess.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(printaccess);


            MySqlComm.ExecuteScalar();
            regno = Outval.Value.ToString();
            pntaccess=Convert.ToInt32(printaccess.Value);




            hstbl.Add("regno", regno);
            hstbl.Add("pntaccess", pntaccess);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }
        return hstbl;
    }


    public DataSet Company_WaitingCompany_Registration_Report_Dataset(string dateFrom, string dateto, int BranchID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
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


    public DataSet Company_ddl_getCompany_Dataset(int BranchID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Accepted_Fetch_CompanyID_Name", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
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


    // Created By Ravish Kumar Pandey TO Industrial Corporation
    public DataSet Company_WaitingCompany_Registration_CertificateForm_Dataset(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPtblCertificate_Registration", DBManager.ConnectToDB());
            //MyAdp = new SqlDataAdapter("GPtblCertificate_Registration_onlinetesting", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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

    // Get PartNerName

    public DataSet Company_Report_Registration_CertificateForm_PartnerName(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyDirector_Name", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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



    public DataSet Company_WaitingCompany_Registration_CertificateForm_Dataset_TURNover(string RefNo)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Turn_overCertificate_Registration", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@RegistrationNumber", SqlDbType.VarChar).Value = RefNo;
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

    // Create By Ravish Kumar Pandey for address

    public DataSet Company_WaitingCompany_Registration_CertificateForm_Dataset_AddressFactory(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("AGPtblCertificate_Registration_Address", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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


    public DataSet Fetch_company_Store_Certificate(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_CertificateResigaration_Store", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    // Created By Ravish Kumar Pandey TO Industrial Corporation
    public DataSet Branch_Industrial_Corporation(int Unit_id)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("dsp_IndustrialCorporation", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@unitID", SqlDbType.Int).Value = Unit_id;
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

    // Insert into GPtbl_CERTIFICATE


    public int Company_Certificate_Insert(int fGPtbl_CERTIFICATE_FormtypeId, int fGPtbl_CERTIFICATE_CompanyId, string fGPtbl_CERTIFICATE_No, int UserId, DateTime FirstPrintDate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Certificate_Insert", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_FormtypeId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_FormtypeId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_CompanyId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_No", SqlDbType.VarChar, 30).Value = fGPtbl_CERTIFICATE_No;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@FirstPrintDate", SqlDbType.DateTime).Value = FirstPrintDate;
            SqlParameter outError = new SqlParameter("@Output", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_Certificate_Insert_offline(int fGPtbl_CERTIFICATE_FormtypeId, int fGPtbl_CERTIFICATE_CompanyId, string fGPtbl_CERTIFICATE_No, int UserId, DateTime FirstPrintDate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Certificate_Insert", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_FormtypeId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_FormtypeId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_CompanyId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_No", SqlDbType.VarChar, 30).Value = fGPtbl_CERTIFICATE_No;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@FirstPrintDate", SqlDbType.DateTime).Value = FirstPrintDate;
            SqlParameter outError = new SqlParameter("@Output", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    public int Company_CertificateRequest_Insert(int fGPtbl_CR_Id, int fGPtbl_CR_CertificateId, string fGPtbl_CR_Request, string fGPtbl_CR_RejectionReason, int fGPtbl_CR_AcceptedByAdmin, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_CertificateRequest_Insert", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CR_Id", SqlDbType.Int).Value = fGPtbl_CR_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CR_CertificateId", SqlDbType.Int).Value = fGPtbl_CR_CertificateId;
            MySqlComm.Parameters.Add("@fGPtbl_CR_Request", SqlDbType.VarChar, 5000).Value = fGPtbl_CR_Request;
            MySqlComm.Parameters.Add("@fGPtbl_CR_RejectionReason", SqlDbType.VarChar, 5000).Value = fGPtbl_CR_RejectionReason;
            MySqlComm.Parameters.Add("@fGPtbl_CR_AcceptedByAdmin", SqlDbType.Int).Value = fGPtbl_CR_AcceptedByAdmin;
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
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }
        return ErrorId;
    }

    //TO FETCH DATA OF CERTIFICATE REQUEST
    public DataTable Company_CertificateRequest_Fetch(int UserID)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_CertificateRequest_Fetch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = UserID;
           // MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserID;
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    //TO FETCH DATA OF CERTIFICATE REQUEST BY ID
    public DataTable Company_CertificateRequest_Fetch_ById(int requestId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_CertificateRequest_Fetch_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CR_Id", SqlDbType.Int).Value = requestId;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    // Created By ravish Kumar Pandey TO fetch Status Track---

    public DataSet Company_StatusTrack_Status(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPs_CompanyStatusTrack_Display", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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

    public DataSet Company_StatusTrack_UserName(int UserID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUser_Name_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = UserID;
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

    public DataSet Company_WaitingCompany_Registration_Report_Modified(string dateFrom, string dateto, int BranchID, int ZoneID, int StateID, int CityID, string turnoverfrom, string turnoverto, string Moneterylimitfrom, string Monetetarylimitto, int CategoryId, int SubCategoryId, int CastId, string SearchCompanyName, string orderby)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_Report_Modified_1_modified", DBManager.ConnectToDB());  //GPsp_Company_Waiting_Fetch_CUnit_Report_Modified_1
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            MyAdp.SelectCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            if (turnoverfrom != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverFrom", SqlDbType.VarChar, 50).Value = turnoverfrom;
            if (turnoverto != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverTo", SqlDbType.VarChar, 50).Value = turnoverto;
            if (Moneterylimitfrom != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitFrom", SqlDbType.VarChar, 50).Value = Moneterylimitfrom;
            if (Monetetarylimitto != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitTo", SqlDbType.VarChar, 50).Value = Monetetarylimitto;

            MyAdp.SelectCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryId;
            MyAdp.SelectCommand.Parameters.Add("@subCategoryId", SqlDbType.Int).Value = SubCategoryId;
            MyAdp.SelectCommand.Parameters.Add("@CunitStatusId", SqlDbType.Int).Value = CastId;
            if (SearchCompanyName != "") MyAdp.SelectCommand.Parameters.Add("@SearchCompanyName", SqlDbType.VarChar, 50).Value = SearchCompanyName;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = orderby;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }



    //TO FETCH COMPANY NAME AND ID BY GP REFERENCE NO.
    public DataTable Company_CompanyStatus_Fetch_ByGprefno(string gpreferenceno)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_StatusTrack", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_GPReferenceNo", SqlDbType.VarChar, 500).Value = gpreferenceno;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    //TO FETCH UNIT DETAILS BY COMPANY ID
    public DataSet Company_UnitDetails_Fetch_ByCompanyId(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid_new", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyid;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    //TO FETCH COMPANY DETAILS BY ID
    public DataTable Company_Detail_Fetch_ById(int companyid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Registration_Fetch_CompanyDetails", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyid;
            MyAdp.Fill(MyDt);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    //TO CHANGE PASSWORD OF COMPANY
    //public int Company_ChangePassword(int UserId, string Newpassword, string Oldpassword, int Edituser)
    public int Company_ChangePassword(string Newpassword, string Oldpassword, string Username)
    {
        try
        {


            MySqlComm = new SqlCommand("GPsp_Company_ChangePassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 2000).Value = Newpassword;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 2000).Value = Oldpassword;
            //MySqlComm.Parameters.Add("@fGPtbl_MU_EUser", SqlDbType.Int).Value = Edituser;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Username", SqlDbType.VarChar, 15).Value = Username;

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

    //COMPANY FORGOT PASSWORD
    public Hashtable Company_ForgotPassword(string Username, string Password)
    {
        Hashtable hashtbl = new Hashtable();
        string Email, Companyname;
        Email = "";
        Companyname = "";
        int companyid = 0;

        try
        {
            RandomKeyGenerator objRnd = new RandomKeyGenerator();
            string passwordFgp = objRnd.Generate();

            MySqlComm = new SqlCommand("GPsp_Company_ForgotPassword", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Username", SqlDbType.VarChar, 200).Value = Username;

            if (Password == "none")//FORGOT PASSWORD
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 2000).Value = passwordFgp;
                MySqlComm.Parameters.Add("@ToIdentify", SqlDbType.Int).Value = 2;
            }
            else
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 2000).Value = Password;
                MySqlComm.Parameters.Add("@ToIdentify", SqlDbType.Int).Value = 1;
            }


            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            SqlParameter outEmail = new SqlParameter("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 200);
            outEmail.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outEmail);

            SqlParameter outCompanyname = new SqlParameter("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000);
            outCompanyname.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompanyname);

            SqlParameter outCompanyid = new SqlParameter("@fGPtbl_Company_Id", SqlDbType.Int);
            outCompanyid.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompanyid);

            MySqlComm.ExecuteNonQuery();

            if (!DBNull.Value.Equals(outEmail.Value))
            {
                Email = outEmail.Value.ToString();
            }
            if (!DBNull.Value.Equals(outCompanyname.Value))
            {
                Companyname = outCompanyname.Value.ToString();
            }
            //string Password = outPwd.ToString();
            int output = Convert.ToInt32(outVal.Value);


            if (!DBNull.Value.Equals(outCompanyid.Value))
            {
                companyid = Convert.ToInt32(outCompanyid.Value);
            }


            hashtbl.Add("email", Email);
            hashtbl.Add("pwd", passwordFgp);
            hashtbl.Add("Output", output);
            hashtbl.Add("compname", Companyname);
            hashtbl.Add("companyid", companyid);

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
        return hashtbl;
    }


    //Insert Update Turnover and Monetary Limit

    public int Company_InsertUpdate_TurnoverMonetaryLimit(int Formtypeid, string stryear1, string stryear2, string stryear3, string stryear4, int UserId,int Roleid)
    {
        try
        {
            if (Formtypeid == 3)
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_InsertTurnoverMonetaryLimit", DBManager.ConnectToDB());
            }
            else if (Formtypeid == 2)
            {
                MySqlComm = new SqlCommand("GPsp_InsertTurnoverMoniteryLimit", DBManager.ConnectToDB());
            }
            else
            {
                MySqlComm = new SqlCommand("GPsp_InsertTurnoverMoniteryLimit", DBManager.ConnectToDB());
            }
            MySqlComm.CommandType = CommandType.StoredProcedure;

           //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_UnitId", SqlDbType.Int).Value = Unitid;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = CompanyId;
            if (stryear1 != "") MySqlComm.Parameters.Add("@year1", SqlDbType.VarChar, 1000).Value = stryear1;
            if (stryear2 != "") MySqlComm.Parameters.Add("@year2", SqlDbType.VarChar, 1000).Value = stryear2;
            if (stryear3 != "") MySqlComm.Parameters.Add("@year3", SqlDbType.VarChar, 1000).Value = stryear3;
            if (stryear4 != "") MySqlComm.Parameters.Add("@limit", SqlDbType.VarChar, 1000).Value = stryear4;
            MySqlComm.Parameters.Add("@user", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@roleid", SqlDbType.Int).Value = Roleid;
         //  MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
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

    //Created by Ravish 29 March 2012

    public DataSet Company_PaymentDetails_Modified(string dateFrom, string dateto, int BranchID, int ZoneID, int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Payment_Fetch_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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
    // Created By Ravish Kumar Pandey To Fetch ZoneID Table----
    public DataSet Company_PaymentDetails_Fetch_ZoneWise(string dateFrom, string dateto, int ZoneID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_Branch_Wise", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
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


    // To Show Data in Grid with Zone wise-------

    public DataSet Company_paymentetails_Pass_value_fromdate(string dateFrom, string dateto)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_ZoneWise_Wise_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
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

    // Created By ravish Kumar Pandey To show Only If search  Zone wise data click  branch Name
    /*public DataSet Company_PaymentDetails_Fetch_ZoneWise_Modified(int ZoneID, int BranchID, string OrderBY_User)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Record", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@zoneId", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@order_by_user", SqlDbType.VarChar, 10).Value = OrderBY_User;
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
    }*/

    // To Fetch CompanyID Wise Record Because see show Company Wise Data--
    public DataSet Company_PaymentDetails_CompanyWise_RECORD(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Details", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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

    // Get Branch Id//

    public DataSet Company_PaymentDetails_BranchName(int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Get_Branch_Name", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;

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

    // Turn Over Table 

    public DataSet Company_ReportView_TUrnOver(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TurnOver_Details_modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;

            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Timeout"))
            {
                (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Server Busy!!!!'); window.close();</script>");
                // ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Server Busy!!!!');", true);
            }
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    /* Fetch Monetary Limit */

    public DataSet Company_MonetaryLimit_FetchByCompId_Dataset(int Compid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Registration_Fetch_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Compid;

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

    public string Company_InsertUpdate_Zone(int companyid, string monetarylimit, int userid)
    {
        string passwd = "";
        try
        {
            RandomKeyGenerator objkey = new RandomKeyGenerator();
            passwd = objkey.Generate();
            MySqlComm = new SqlCommand("GPsp_Company_Monetary_Registration_Update_ById_zone", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = companyid;

            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            if (monetarylimit != "") MySqlComm.Parameters.Add("@MonetaryLimit", SqlDbType.VarChar, 1000).Value = monetarylimit;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 4000).Value = passwd;

            //  MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;


            MySqlComm.ExecuteNonQuery();
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
        return passwd;
    }
    

    //public string Company_InsertUpdate_Zone_Validity(int companyid, string approvaldate, string validitydate, int userid)
     public string Company_InsertUpdate_Zone_Validity(int companyid,int userid)
    {
        string passwd = "";
        try
        {
            RandomKeyGenerator objkey = new RandomKeyGenerator();
            passwd = objkey.Generate();
            MySqlComm = new SqlCommand("GPsp_Company_Monetary_Registration_Update_ZoneDate", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
           // MySqlComm.CommandTimeout = 6000000;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            /*if (approvaldate != "") MySqlComm.Parameters.Add("@Approvaldate", SqlDbType.DateTime).Value = approvaldate;
            if (validitydate != "") MySqlComm.Parameters.Add("@Validitystartdate", SqlDbType.DateTime).Value = validitydate;
             */
            MySqlComm.Parameters.Add("@CompanyPassword", SqlDbType.NVarChar, 100).Value = passwd;

            MySqlComm.ExecuteNonQuery();
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
        return passwd;
    }

    /* Code added by ravish on 2 April 2012*/

    public DataSet Company_PaymentDetails_Fetch_ZoneWise_Modified(int ZoneID, int BranchID, string OrderBY_User)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Record", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@zoneId", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@order_by_user", SqlDbType.VarChar, 10).Value = OrderBY_User;
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

    public DataSet Company_Certificate_MonoteryLimit(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSp_Fetch_MonetoryLimit_Certificate", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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


    public SqlDataReader FetchMasterDocument()
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterUploadDocument", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

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
        return MyDr;
                    
    }

    /* Code added by Ravish on 4-4-2012*/

    public DataSet Company_UserList_Report(string dateFrom, string dateto, int BranchID, int ZoneID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_UserList_fetch_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
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

    public DataSet Company_Inspection_Report(string dateFrom, string dateto, int BranchID, int ZoneID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_INspectionAgency_fetch_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
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


    //TO FETCH LIST OF COMPANIES WITH UNCONFIRMED TRANSACTION
    public DataSet Company_UnconfirmedTransaction_Fetch_Dataset(int UserId, int fGPtbl_PD_FormtypeId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_UnconfirmedPayment_FetchAll", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_PD_FormtypeId", SqlDbType.Int).Value = fGPtbl_PD_FormtypeId;

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


    /* Fetch Unconfirmed Transaction details using Paymentid*/

    public SqlDataReader Company_Fetch_UnconfirmedTransaction_ByPaymentid(int Paymentid)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_UnconfirmedPayment_FetchByPaymentid", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Paymentid", SqlDbType.Int).Value = Paymentid;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    /* Update the status of unconfirmed transaction using Payment Id  */

    public int Company_Update_UnconfirmedTransaction(int Formtypeid, int fGPtbl_PD_Id, int fGPtbl_PD_BranchConfirmation, string fGPtbl_PD_CashReceiptNo, int UserId)
    {
        try
        {

            //            @fGPtbl_PD_Id Int,
            //@fGPtbl_PD_BranchConfirmation int,
            //@fGPtbl_PD_CashReceiptNo varchar(50)=Null

            MySqlComm = new SqlCommand("GPsp_Company_Update_UnconfirmedTransaction", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@Formtypeid", SqlDbType.Int).Value = Formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_UnitId", SqlDbType.Int).Value = Unitid;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;
            if (fGPtbl_PD_CashReceiptNo != "") MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            //if (stryear2 != "") MySqlComm.Parameters.Add("@year2", SqlDbType.VarChar, 1000).Value = stryear2;
            //if (stryear3 != "") MySqlComm.Parameters.Add("@year3", SqlDbType.VarChar, 1000).Value = stryear3;
            //if (stryear4 != "") MySqlComm.Parameters.Add("@limit", SqlDbType.VarChar, 1000).Value = stryear4;
            MySqlComm.Parameters.Add("@user", SqlDbType.Int).Value = UserId;
            // MySqlComm.Parameters.Add("@roleid", SqlDbType.Int).Value = Roleid;

            //  MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;

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



    public void Store_Insert(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int Insertupdate, int userid)
    {

        try
        {
            string allval = string.Empty, allvalpmps = string.Empty;//, all1;
            foreach (string s in vall1)
            {
                allval += s;
            }
            //all1 = allval;

            foreach (string s1 in vallpmps)
            {
                allvalpmps += s1;
            }

            MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = Insertupdate;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

            SqlParameter outVal = new SqlParameter("@ErrorId", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();

            int errorid = Convert.ToInt32(outVal.Value);

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

    }

    // Print Acess Details Report 09/04/2012
    public DataSet Company_PrintAcess_Certificate_DateWise(string dateFrom, string dateto, int BranchID, int ZoneID, string OrderByUser)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_GetCompany_AccesssReson", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = OrderByUser;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"   "+ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    // Only Final this Date Between Dated

    public DataSet Company_PrintAcess_Certificate_DateWiseModified(string dateFrom, string dateto, string OrderByUser)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_OnlyForDatePRintAccess", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = OrderByUser;
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
    // Pass All The Data In the List To get  Company Id Data

  /*  public DataSet Company_PrintAcess_Certificate_companywiseDatezonebranch(string dateFrom, string dateto, int BranchID, int ZoneID, int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_PrintAccessCompanyWise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyId;

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
    }*/
    public DataSet Company_PrintAcess_Certificate_companywiseDatezonebranch(string dateFrom, string dateto, int BranchID, int ZoneID, int CompanyId, string OrderBy)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_PrintAccessCompanyWise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyId;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = OrderBy;

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
    // Pass Datewise and zonewise Data
    /*public DataSet Company_PrintAcess_Certificate_companywiseDatezonewise(string dateFrom, string dateto, int ZoneID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_PrintAccessZoneWisedate", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (dateFrom != "") MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            if (dateto != "") MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;

            if (ZoneID != 0) MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;


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
    }*/

    public DataSet Company_PrintAcess_Certificate_companywiseDatezonewise(string dateFrom, string dateto, int ZoneID, string orderby)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_PrintAccessZoneWisedate", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (dateFrom != "") MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            if (dateto != "") MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;

            if (ZoneID != 0) MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = orderby;


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


    // IF Click Company Button


    public DataSet Company_PrintAcess_Certificate_companywise(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GpSp_Companywise_Print_access", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;


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


    /* Code added by Ravish on 17 April 2012*/

    // To Show Report Name in Gridview
    public DataTable Fetch_ReportName()
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("GPsp_fetch_Report_Name", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    // To Insert Data Of The Master Report Table  Master Role and Report Access Page
    public int Report_Insert_Name_Description(int MR_ID, string RoleNameId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("Gpsp_Insert_MasterRoleRecord_ReportAccess", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_MU_Id", SqlDbType.Int).Value = UserId;
            //MySqlComm.Parameters.Add("@fgtbl_MR_Name", SqlDbType.VarChar, 200).Value = Name;
            // MySqlComm.Parameters.Add("@fgtbl_MR_Desc", SqlDbType.VarChar, 2000).Value = Description;
            // MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = Flag;
            MySqlComm.Parameters.Add("@fGPtbl_MR_Id", SqlDbType.Int).Value = MR_ID;
            MySqlComm.Parameters.Add("@Nameid", SqlDbType.VarChar).Value = RoleNameId;
            //  MySqlComm.Parameters.Add("@fgtbl_Status_Id", SqlDbType.Int).Value = statusId;
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



    // To all Fetch Role in grid 
    public DataTable Fetch_RoleName_Description()
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterRoleData", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    // To Fetch Report  Role And Description

    // To all Fetch Role in grid 
    public DataTable Fetch_RoleName_Description_Accoroding_To_RoleID(int RoleId)
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("GPsp_fetch_Report_Name_Fetch_According_RoleID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public DataTable Fetch_Report_Name_reportId(int RoleId)
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("GPsp_Feth_Report_Name_By_Role_ID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    // To Fetch All Report Criteria Through SElectCriteria in a list

    public DataTable Fetch_Report_Name_ReportCriteria(int UserId)
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("GPsp_fetch_selection_Crieteria_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserId;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public DataTable Company_Fetch_HscodeAll()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Hscode", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    // Certificate Store Table-------------- clsCompany

    public DataSet Fetch_company_Store_Certificate_Modified(int UnitID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_CertificateResigaration_Store_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UnitID", SqlDbType.Int).Value = UnitID;

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


    // Fetch All Company Amedment Created By Ravish Kumar Pandey

    public DataSet Company_WaitingCompany_Registration_FetchAllCompanies_Dataset_Modified(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Compny_Waiting_Fetch_Company_All_MKodified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;

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

    //Code added by Ravish

    // Created By Ravish Kumar Pandey For UnitAmdment step- 7-----------------------------------------------

    public string Company_CompanyAmdment_Step7(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType,
        double @fGPtbl_PD_PaidAmount, string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName,
        string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate, string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath,
        string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,string  PaymentRemarks)
    {
        string Returnvalue = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step7", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

            if (fGPtbl_PD_ChequeDDNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
            }

            if (fGPtbl_PD_ChequeDDate != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
            }

            if (fGPtbl_PD_BankName != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
            }

            if (fGPtbl_PD_BankBranch != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
            }

            if (fGPtbl_PD_PaybalAt != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
            }

            if (fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
            }

            if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
            }

            if (fGPtbl_PD_CashReceiptNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            }

            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
            MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            if (PaymentRemarks != "")
            {
                MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 50).Value = PaymentRemarks;
            }

            


            SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();

            Returnvalue = outError.Value.ToString();
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
        return Returnvalue;
    }



    //  Created By Ravish Kumar Pandey For Fetch monetory limit through nit amdment table

    public DataSet Company_MonetaryLimit_FetchByCompId_Dataset_Amedment_Process(int Compid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Monetary_Fetch_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Compid;

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


    // Created By Ravish Kumar Pandey For amedment Table update 09 may 2012
    public string Company_InsertUpdate_ZoneAmedment(int companyid, int userid)
    {
        string passwd = "";
        try
        {
            RandomKeyGenerator objkey = new RandomKeyGenerator();
            passwd = objkey.Generate();
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Monetary_Update_ById_zone", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            //  MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MySqlComm.ExecuteNonQuery();
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
        return passwd;
    }

    // Created By Ravish Kumar Pandey  09 may 2012 ------------------------

    // They are Not Proper Work Because i  do not   take GPtbl_CUnitAmendment_Turnover  and year are onle one not 3 so it is wrong way .. 


    public int Company_InsertUpdate_TurnoverMonetaryLimit_Amedment(int amendmentid, int Formtypeid, string stryear1, string stryear2, string stryear3, string stryear4, int UserId, int Roleid)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_InsertTurnoverMoniteryLimit_Amedment", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_UnitId", SqlDbType.Int).Value = Unitid;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_RenewalAmendmentId", SqlDbType.Int).Value = amendmentid;
            if (stryear1 != "") MySqlComm.Parameters.Add("@year1", SqlDbType.VarChar, 1000).Value = stryear1;
            if (stryear2 != "") MySqlComm.Parameters.Add("@year2", SqlDbType.VarChar, 1000).Value = stryear2;
            if (stryear3 != "") MySqlComm.Parameters.Add("@year3", SqlDbType.VarChar, 1000).Value = stryear3;
            if (stryear4 != "") MySqlComm.Parameters.Add("@limit", SqlDbType.VarChar, 1000).Value = stryear4;
            MySqlComm.Parameters.Add("@user", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@roleid", SqlDbType.Int).Value = Roleid;

            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;

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


    //Code added by Ravish on 13th June

    public DataTable BindDropdownlist(int UserId, int RoleId)
    {
        try
        {
            MyDt = new DataTable();

            MyAdp = new SqlDataAdapter("Gpsp_Feth_Zone_NamewithID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UserID", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public int Company_Certificate_Insert_Amedment(int fGPtbl_CERTIFICATE_FormtypeId, int fGPtbl_CERTIFICATE_CompanyId, string fGPtbl_CERTIFICATE_No, int UserId, int fgtbl_Certificate_Amedment)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Certificate_Insert_Amedment", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_FormtypeId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_FormtypeId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_CompanyId", SqlDbType.Int).Value = fGPtbl_CERTIFICATE_CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_CERTIFICATE_No", SqlDbType.VarChar, 30).Value = fGPtbl_CERTIFICATE_No;

            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@fgtbl_Certificate_Amedment", SqlDbType.Int).Value = fgtbl_Certificate_Amedment;

            SqlParameter outError = new SqlParameter("@Output", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

    /* Code added by Ravish on 20 July 2012*/

    public DataSet Company_WaitingCompany_Amedment_Report_Modified(string dateFrom, string dateto, int BranchID, int ZoneID, int StateID, int CityId, string turnoverfrom, string turnoverto, string MonetarylimitFrom, string Monetarylimitto, int CategoryId, int SubCategoryId, int CastId, string SearchCompanyName, string OrderBY)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_UnitAmedmentReport_modified", DBManager.ConnectToDB());  //GPsp_Fetch_UnitAmedmentReport
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 7200;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            MyAdp.SelectCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityId;
            if (turnoverfrom != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverFrom", SqlDbType.VarChar, 50).Value = turnoverfrom;
            if (turnoverto != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverTo", SqlDbType.VarChar, 50).Value = turnoverto;
            if (MonetarylimitFrom != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitFrom", SqlDbType.VarChar, 50).Value = MonetarylimitFrom;
            if (Monetarylimitto != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitTo", SqlDbType.VarChar, 50).Value = Monetarylimitto;
            MyAdp.SelectCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryId;
            MyAdp.SelectCommand.Parameters.Add("@subCategoryId", SqlDbType.Int).Value = SubCategoryId;
            MyAdp.SelectCommand.Parameters.Add("@CunitStatusId", SqlDbType.Int).Value = CastId;
            if (SearchCompanyName != "") MyAdp.SelectCommand.Parameters.Add("@SearchCompanyName", SqlDbType.VarChar, 50).Value = SearchCompanyName;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = OrderBY;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "   " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public DataSet Fetch_Company_city_wise(int StateID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_Bind_City", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;

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

    public DataSet Fetch_Company_State_wise_dropdown(int Branch_ID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Bind_State", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Branch_Id", SqlDbType.Int).Value = Branch_ID;
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
    
    //Code added by Ravish on 29 August 2012

    public DataSet Company_WaitingCompany_Renewal_Report_Modified(string dateFrom, string dateto, int BranchID, int ZoneID, int StateID, int CityID, string turnoverfrom, string turnoverto, string monetarylimitfrom, string monetarylimitto, int CategoryId, int SubCategoryId, int CastId, string searchCompany, string orderby)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_UnitRenewalReport_modified", DBManager.ConnectToDB());// GPsp_Fetch_UnitRenewalReport
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 7200;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@StateID", SqlDbType.Int).Value = StateID;
            MyAdp.SelectCommand.Parameters.Add("@CityID", SqlDbType.Int).Value = CityID;
            if (turnoverfrom != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverFrom", SqlDbType.VarChar, 50).Value = turnoverfrom;
            if (turnoverto != " ") MyAdp.SelectCommand.Parameters.Add("@TurnoverTo", SqlDbType.VarChar, 50).Value = turnoverto;
            if (monetarylimitfrom != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitFrom", SqlDbType.VarChar, 50).Value = monetarylimitfrom;
            if (monetarylimitto != " ") MyAdp.SelectCommand.Parameters.Add("@MonetaryLimitTo", SqlDbType.VarChar, 50).Value = monetarylimitto;
            MyAdp.SelectCommand.Parameters.Add("@CategoryID", SqlDbType.Int).Value = CategoryId;
            MyAdp.SelectCommand.Parameters.Add("@subCategoryId", SqlDbType.Int).Value = SubCategoryId;
            MyAdp.SelectCommand.Parameters.Add("@CunitStatusId", SqlDbType.Int).Value = CastId;
            if (searchCompany != "") MyAdp.SelectCommand.Parameters.Add("@SearchCompanyName", SqlDbType.VarChar, 50).Value = searchCompany;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = orderby;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }



    // For Schedular by GS
    //------------------------------------

    //Status change for companies whose registration expires.
    public void StatusChangeOfCompany_whoseRegExp()
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_regstatus_by_regdate", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.ExecuteNonQuery();
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

    }


    //Mail alert to inspection agencies for reminder of pending inspection.
    public DataSet InspectionAgncy_Reminde_sche1()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Mail_to_InspectionAgncy_Reminder", DBManager.ConnectToDB());
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

    //Mail alert to companies whose next renewal date nears up.
    public DataSet NextRrenewalDateNearsUp_Reminder_sche2()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Mail_to_Company_WhoseNextRenewalDateNearsUp_Reminder", DBManager.ConnectToDB());
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

    //Mail alert to companies whose registration expires.
    public DataSet companiesWhoseRegistrationExpires_sche3()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Mail_to_Company_WhoseRegistrationExpires_Reminder", DBManager.ConnectToDB());
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


    //Mail alert to be sent to branch with list of companies whose form has been sent to inspection agency but date of inspection not issued.
    public DataSet SentdateOfInspectionNotIssued_sche4()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Mail_to_Branch_5_Reminder", DBManager.ConnectToDB());
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


    // Mail alert to be sent to branch with list of companies whose monetary limit is yet to be set.
    public DataSet CompaniesWhoseMonetaryLimitIsYetToBeSet_Reminder_sche6()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Mail_to_Branch_CompaniesWhoseMonetaryLimitIsYetToBeSet_Reminder", DBManager.ConnectToDB());
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

    public DataTable Company_Fetch_Certificate_Search(int branchid, int ddlcriteria, string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;          
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public DataTable Company_Fetch_Certificate_SearchByAdmin(int ddlcriteria, string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_SearchByAdmin", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            if(searchcriteria!="") MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


   


    public SqlDataReader Company_FetchSlabFee_Step6(decimal turnover, int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            // MySqlComm.Parameters.Add("@Provisional", SqlDbType.Int).Value = _mprovision;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    //TO INSERT INTO REQUEST CERTIFICATE FOR TRACKING SERIAL NO
    public int Track_Registraton_Certificate(int CompanyId, string TrackSerialNo, string BranchCode, int BranchId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_Request_Certificate_Track", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Gptbl_TrackCompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@Gptbl_TrackSerialNo", SqlDbType.VarChar, 50).Value = TrackSerialNo;
            MySqlComm.Parameters.Add("@Gptbl_TrackBranchCode", SqlDbType.VarChar, 50).Value = BranchCode;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            SqlParameter outVal = new SqlParameter("@ErrorId", SqlDbType.Int);
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

    // fetch Data if exist in Registration
    public DataSet Company_Registration_Certificate_fetch_serial_no(int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_fetch_Serial_Number", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchId != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
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

    public DataSet Fetch_Branch_office_address_details(int BranchID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSp_Fetch_Branch_Information", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
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


    // New Procedure  For Get PaymentId and FormTypeId Through Company Id  30 Feb 2013 Ravish 
    public DataSet Fetch_Branch_Payment_Type_andFormTypeId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Details_FormTypePayment", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public DataSet Fetch_Branch_Payment_Type_andFormTypeIdList(int FormType, int PDType)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_fetch_separate_record_companywise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = FormType;
            MyAdp.SelectCommand.Parameters.Add("@PdId", SqlDbType.Int).Value = PDType;
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

    // Make new procedure for fetch Company Data 11 Feb 2013
    public DataSet Fetch_Branch_Payment_Type_andFormTypeIdList_Modified(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPSP_Fetch_Company_Details_By_Comapany_Id", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public DataSet Company_paymentetails_Pass_value_fromdate_Modified(string dateFrom, string dateto)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_ZoneWise_Wise_Modified_Zonewise_Data", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
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

    public DataSet Company_PaymentDetails_Fetch_ZoneWise_Modified_datetime_Modified(int ZoneID, int BranchID, string Fromdate, string Todate)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Record_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@zoneId", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            if (Fromdate != "") MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = Fromdate;
            if (Todate != "") MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = Todate;
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

    public DataSet Company_PaymentDetails_Modified_Fetch_By_Formtype(string dateFrom, string dateto, int BranchID, int ZoneID, int CompanyID, int FormType)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Payment_Report_Formtype", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
            MyAdp.SelectCommand.Parameters.Add("@FormType", SqlDbType.Int).Value = FormType;
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


    //make blank record for Payment Details // 15 Feb 2013 suggest by Vinay shukal
    public DataSet Fetch_PaymentData(string FromDate, string ToDate)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_Fetch_paymentdata", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (FromDate != "") MyAdp.SelectCommand.Parameters.Add("@Fromdate", SqlDbType.VarChar, 50).Value = FromDate;
            if (ToDate != "") MyAdp.SelectCommand.Parameters.Add("@Todate", SqlDbType.VarChar, 50).Value = ToDate;
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

    public DataSet Fetch_PaymentData_BranchWise(int ZoneID, string FromDate, string ToDate)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_Sp_fetch_PaymentData_BranchWise", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneID;
            if (FromDate != "") MyAdp.SelectCommand.Parameters.Add("@Fromdate", SqlDbType.VarChar, 50).Value = FromDate;
            if (ToDate != "") MyAdp.SelectCommand.Parameters.Add("@Todate", SqlDbType.VarChar, 50).Value = ToDate;
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



    public DataSet Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(int ZoneID, int BranchID, string FromDate, string TODate, string orderby)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Record_withDate_Wise_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@zoneId", SqlDbType.Int).Value = ZoneID;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = FromDate;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = TODate;
            MyAdp.SelectCommand.Parameters.Add("@orderby_user", SqlDbType.VarChar, 50).Value = orderby;
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

    public int Company_Provisional_Status(int CompanyId)
    {
        int retval=0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchProvisionStatus", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            retval = Convert.ToInt32(MySqlComm.ExecuteScalar());
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

        return retval;
    }


    public DataSet Bind_zonewise_data(int userid, int roleid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_SP_ZonwiseCountLive_expireUnit", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 10000;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
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

    public DataSet Bind_tracecompany()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_Sp_TraceCompanywiseReport", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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

    public DataTable Company_trace_Company_Search(int ddlcriteria, string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_serach_tracecompanyReport", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public DataTable Company_trace_Company_Search_modified(int ddlcriteria, string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_serach_tracecompanyReport_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public void Store_Insert_Modified(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int Insertupdate, int userid, string str, string companyID)
    {

        try
        {
            string allval = string.Empty, allvalpmps = string.Empty;//, all1;
            foreach (string s in vall1)
            {
                allval += s;
            }
            //all1 = allval;

            foreach (string s1 in vallpmps)
            {
                allvalpmps += s1;
            }

            MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = Insertupdate;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = str;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Convert.ToInt32(companyID);
            SqlParameter outVal = new SqlParameter("@ErrorId", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);
            MySqlComm.ExecuteNonQuery();
            int errorid = Convert.ToInt32(outVal.Value);

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

    }


    public DataTable Company_Fetch_CertificateAll_fetchDraftPrint(int branchid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_draftprint", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }


    public DataTable Company_Fetch_Certificate_Search_DraftPrint(int branchid, int ddlcriteria, string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public DataTable Fetch_LiveExpiredUnit_Zonewise()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_LiveExpiredUnit_Zonewise", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Timeout"))
            {
                (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Server Busy Please Try after some time!!!!'); </script>");
                // ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Server Busy!!!!');", true);
            }
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs.Tables[0];
    }  

    public DataSet Fetch_Branch_Payment_Type_CompanywiseSeparateRecord(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_fetch_companywise_separate_recordfetch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public DataSet Fetch_Company_status_track(string RefrenceNo)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_company_status_track_for_admin", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@GPReferenceNo", SqlDbType.VarChar, 200).Value = RefrenceNo;
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

    public DataSet Fetc_maxMonetary_limit(int Authorised)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_feth_maxMonetary_limit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@AutorisedId", SqlDbType.Int).Value = Authorised;
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

    public DataSet Fetch_Company_status_track_Companywise(string CompanyName)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_company_status_track_for_admin_companywise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;
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


    public DataSet Fetch_Company_status_track_byUser(string RefrenceNo, int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_company_status_track_for_BranchUser", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@GPReferenceNo", SqlDbType.VarChar, 200).Value = RefrenceNo;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
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

    public DataSet Fetch_Company_status_track_Companywise_By_User(int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_company_status_track_for_admin_companywise_byuser", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BrancId", SqlDbType.Int).Value = BranchId;
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

    public DataSet Fetch_Company_status_track_Companywise_By_User_ByCompanyName(string CompanyName, int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_company_status_track_for_Company_companywise_byuser", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;
            MyAdp.SelectCommand.Parameters.Add("@BrancId", SqlDbType.Int).Value = BranchId;
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

     public int Company_insert_turnover_limit(int CompanyId, int FormtypeId, decimal lastyearturnover, int UserId)
    {
        int ErrorId = 0;
        try
        {
            MySqlComm = new SqlCommand("Gpsp_insert_Company_Turnover_limit", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_TO_CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_TO_FormTypeId", SqlDbType.Int).Value = FormtypeId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_TO_LastYearTurnover", SqlDbType.Decimal,2).Value = lastyearturnover;
            MySqlComm.Parameters.Add("@fGPtbl_Company_TO_CUser", SqlDbType.Int).Value = UserId;
           

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);                          
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
        return ErrorId;
    }

     public string Company_Insert_Step7_Modified(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType, double @fGPtbl_PD_PaidAmount,
        string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName, string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate,
        string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath, string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,
         int PaymentTypeInspection, string PaymentDD_Inspection, string BankeName_Inspection, string Branch_Name_Inspection,
          string paymentDD_date_inspection, string Payableat_inspection,
         string Paymetfilename_inspection, string Filepath_inspection,
          int Branch_confirmation_inspection, int Fundtransfer_type, string utrnumber, int fGPtbl_PD_PaymentType_RITES_Type_inspection, string PaymentRemarks)
     {
         string Returnvalue = "";
         try
         {
             MySqlComm = new SqlCommand("GPsp_Company_Insert_Step7_Modified", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;

             MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
             MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
             MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
             MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

             if (fGPtbl_PD_ChequeDDNo != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
             }
             if (fGPtbl_PD_BankName != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
             }
             if (fGPtbl_PD_BankBranch != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
             }
             if (fGPtbl_PD_PaybalAt != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
             }
             if (fGPtbl_PD_ChequeDDate != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
             }
             if (fGPtbl_PD_Filename != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
             }
             if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
             }
             if (fGPtbl_PD_CashReceiptNo != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
             }

             MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


             MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
             MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

             MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
             if (PaymentTypeInspection != null) MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_inspection", SqlDbType.Int).Value = PaymentTypeInspection;
             if (PaymentDD_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_inspection", SqlDbType.VarChar, 50).Value = PaymentDD_Inspection;
             if (BankeName_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_BankName_inspection", SqlDbType.VarChar, 500).Value = BankeName_Inspection;
             if (Branch_Name_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch_inspection", SqlDbType.VarChar, 500).Value = Branch_Name_Inspection;
             //  MySqlComm.Parameters.Add("@fGPtbl_PD_Date_inspection", SqlDbType.DateTime).Value = PaymentDate_inspection;
             if (paymentDD_date_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Cheque_DD_Date_inspection", SqlDbType.DateTime).Value = paymentDD_date_inspection;
             if (Payableat_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt_inspection", SqlDbType.VarChar, 500).Value = Payableat_inspection;
             if (Paymetfilename_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Filename_inspection", SqlDbType.VarChar, 200).Value = Paymetfilename_inspection;
             if (Filepath_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath_inspection", SqlDbType.VarChar, 500).Value = Filepath_inspection;
             if (Branch_confirmation_inspection != null) MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation_inspection", SqlDbType.Int).Value = Branch_confirmation_inspection;
             if (Fundtransfer_type != null) MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = Fundtransfer_type;
             if (utrnumber != null) MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 200).Value = utrnumber;
             if (fGPtbl_PD_PaymentType_RITES_Type_inspection != null) MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_RITES_Type_inspection", SqlDbType.Int).Value = fGPtbl_PD_PaymentType_RITES_Type_inspection;
             if (PaymentRemarks != null) MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 500).Value = PaymentRemarks;
             SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);
             MySqlComm.ExecuteNonQuery();
             Returnvalue = outError.Value.ToString();
            
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
         return Returnvalue;
     }

     public string Company_CompanyAmdment_Step7_Modified(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType,
     double @fGPtbl_PD_PaidAmount, string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName,
     string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate, string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath,
     string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,
     int PaymentTypeInspection, string PaymentDD_Inspection, string BankeName_Inspection, string Branch_Name_Inspection,
          string paymentDD_date_inspection, string Payableat_inspection,
         string Paymetfilename_inspection, string Filepath_inspection,
          int Branch_confirmation_inspection, int Fundtransfertype, string UtrNumber, string PaymentRemarks
         )
     {
         string Returnvalue = "";
         try
         {
             MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step7_Modified", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;

             MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
             MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
             MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
             MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

             if (fGPtbl_PD_ChequeDDNo != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
             }

             if (fGPtbl_PD_ChequeDDate != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
             }

             if (fGPtbl_PD_BankName != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
             }

             if (fGPtbl_PD_BankBranch != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
             }

             if (fGPtbl_PD_PaybalAt != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
             }

             if (fGPtbl_PD_Filename != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
             }

             if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
             }

             if (fGPtbl_PD_CashReceiptNo != "")
             {
                 MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
             }

             MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


             MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
             MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

             MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;


             if (PaymentTypeInspection != null) MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_inspection", SqlDbType.Int).Value = PaymentTypeInspection;
             if (PaymentDD_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_inspection", SqlDbType.VarChar, 50).Value = PaymentDD_Inspection;
             if (BankeName_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_BankName_inspection", SqlDbType.VarChar, 500).Value = BankeName_Inspection;
             if (Branch_Name_Inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch_inspection", SqlDbType.VarChar, 500).Value = Branch_Name_Inspection;
             //  MySqlComm.Parameters.Add("@fGPtbl_PD_Date_inspection", SqlDbType.DateTime).Value = PaymentDate_inspection;
             if (paymentDD_date_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Cheque_DD_Date_inspection", SqlDbType.DateTime).Value = paymentDD_date_inspection;
             if (Payableat_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt_inspection", SqlDbType.VarChar, 500).Value = Payableat_inspection;
             if (Paymetfilename_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Filename_inspection", SqlDbType.VarChar, 200).Value = Paymetfilename_inspection;
             if (Filepath_inspection != "") MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath_inspection", SqlDbType.VarChar, 500).Value = Filepath_inspection;
             if (Branch_confirmation_inspection != null) MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation_inspection", SqlDbType.Int).Value = Branch_confirmation_inspection;
             if (Fundtransfertype != null) MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = Fundtransfertype;
             if (UtrNumber != null) MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 200).Value = UtrNumber;

            if (PaymentRemarks != null) MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 500).Value = PaymentRemarks;

            SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();

             Returnvalue = outError.Value.ToString();
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
         return Returnvalue;

     }


     public DataSet fetch_company_valitiy(int CompanyID)
     {

         try
         {
             MyDs = new DataSet();
          //   MyAdp = new SqlDataAdapter("sp_chekvalidity_online", DBManager.ConnectToDB());

             MyAdp = new SqlDataAdapter("sp_chekvalidity_online_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyID;
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

     public DataSet Fetch_zone_branch_wise_excel_report(string Formadte, string todate, int RoleId, int show)
     {
        try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Sp_Gbtbl_fetch_dashBoard_reportDatewise_forexcel", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = Formadte;
             if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
             MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;
             MyAdp.SelectCommand.Parameters.Add("@GridViewexcel", SqlDbType.Int).Value = show;
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

    //Added March 30 RA
     public DataSet Fetch_zone_branch_wise_excel_report(string Formadte, string todate, int RoleId, int show, 
         int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
          int enterprisedefnition, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId)
     {

         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Sp_Gbtbl_fetch_dashBoard_reportDatewise_forexcel_RA", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = Formadte;
             if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
             MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;
             MyAdp.SelectCommand.Parameters.Add("@GridViewexcel", SqlDbType.Int).Value = show;

             //March 30 RA
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
             MyAdp.SelectCommand.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
             //
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
    //


     public DataTable Company_FetchDirectorList(int companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_Company_FetchDirectorByComapny_Step2_RA", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Directors_CompanyId", SqlDbType.Int).Value = companyid;
             // MyDr = MySqlComm.ExecuteReader();
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             DBManager.CloseConnection();
         }
         return MyDs.Tables[0];
     }

     public int Company_Director_insert(string xmldata, int companyid)
     {
         SqlConnection con = new SqlConnection();
         SqlCommand cmd = new SqlCommand();
         try
         {
             string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
             con = new SqlConnection(connectionstring);
             con.Open();
             cmd = new SqlCommand("GP_sp_Insert_directordata_RA", con);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@Xmldata", xmldata);
             cmd.Parameters.AddWithValue("@CompanyId", companyid);
             ErrorId = cmd.ExecuteNonQuery();
             // ErrorId = Convert.ToInt32(outError.Value);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             cmd.Dispose();
             con.Close();

         }

         return ErrorId;
     }

     public DataSet Company_SPR_CHECK_REGISTRATION(string RegistrartionNo)
     {

         try
         {
             MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_CheckDetails_ByRegno_changes_modified", DBManager.ConnectToDB());  
         // MyAdp = new SqlDataAdapter("GPsp_Company_CheckDetails_ByRegno", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@RegistrationNo", SqlDbType.VarChar, 50).Value = RegistrartionNo;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             DBManager.CloseConnection();
         }
         return MyDs;

     }

     // ------------------  fOR REnewal form Display All Record we use

     public DataSet fetchRenewalViewPageThroughCompanyId(int companyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_ForView_ByCompanyid", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyId;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }



     ////  Fetch making report company status on the per page live unit and expired unit

     public DataSet Fetch_Company_status_track_Live_Unit_Expired_unit(string RefrenceNo, string CompanyName, string CompanyStatus, int ZoneId, int BranchId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_Fetch_liveUnit_expired_unit", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (RefrenceNo != "") MyAdp.SelectCommand.Parameters.Add("@RefrenceNumber", SqlDbType.VarChar, 500).Value = RefrenceNo;
             if (CompanyName != "") MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;
             if (CompanyStatus != "") MyAdp.SelectCommand.Parameters.Add("@CompanyStatus", SqlDbType.VarChar, 10).Value = CompanyStatus;
             if (ZoneId != 0) MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
             if (BranchId != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;


                     
             
             
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


    //RA
     public DataSet Fetch_Company_status_track_Live_Unit_Expired_unit(string RefrenceNo, string CompanyName, string CompanyStatus, int ZoneId, int BranchId
         ,int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
          int enterprisedefnition, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId,
         string Fromadte, string todate, int forExecl
         )
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_Fetch_liveUnit_expired_unit_RA", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (RefrenceNo != "") MyAdp.SelectCommand.Parameters.Add("@RefrenceNumber", SqlDbType.VarChar, 500).Value = RefrenceNo;
             if (CompanyName != "") MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;
             if (CompanyStatus != "") MyAdp.SelectCommand.Parameters.Add("@CompanyStatus", SqlDbType.VarChar, 10).Value = CompanyStatus;
             if (ZoneId != 0) MyAdp.SelectCommand.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneId;
             if (BranchId != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;


             //March 30 RA
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
             MyAdp.SelectCommand.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;

             MyAdp.SelectCommand.Parameters.Add("@isForExecl", SqlDbType.Int).Value = forExecl;


             if (Fromadte !=null) MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = Fromadte;
             if (todate !=null) MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
             //             


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
    

         public DataTable Company_Fetch_DeleteByAdmin(string searchcriteria)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_SearchToDelete", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = 2;
            if (searchcriteria != "") MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    //FUNCTION FOR EDIT VALIDITY DATE OF COMPANY
    public DataTable Company_Fetch_EditValiditydate_SearchByAdmin(int ddlcriteria, string searchcriteria)
     {
         MyDt = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("GPsp_Company_Editvalidity_fetch_Search", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
             MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
             if (searchcriteria != "") MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;

             MyAdp.Fill(MyDt);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             MyAdp.Dispose();
             DBManager.CloseConnection();
         }
         return MyDt;
     }

     public int Update_ComapnY_Registartion(int comopanyId, string validitydate)
     {
         try
         {
             MySqlComm = new SqlCommand("Gp_sp_UpdatecompanyRegistrationdate", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@companyID", SqlDbType.Int).Value = comopanyId;
             MySqlComm.Parameters.Add("@Registartiondate", SqlDbType.VarChar, 50).Value = validitydate;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     //TO ENTER REASON TO REJECT FOR ZONAL HEAD
     public void Company_Update_reasontoreject(int companyId, string rejectreason,int formtype, int userid)
     {
         try
         {
             MySqlComm = new SqlCommand("GPsp_Company_Monetary_Registration_Update_ZonalHeadRejectReason", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
             MySqlComm.Parameters.Add("@Rejectreason", SqlDbType.VarChar, 2000).Value = rejectreason;
             MySqlComm.Parameters.Add("@formtype", SqlDbType.Int).Value = formtype;
             MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
             MySqlComm.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
         }
         finally
         {
             MySqlComm.Dispose();
             DBManager.CloseConnection();
         }
     }

     public DataSet Company_Turnover_Monetary(string xmldata, int companyid, int options, int formtype, int userid)
     {
         try
         {
             MyDs = new DataSet();
             // GP_SP_CalulateMonetary_Modified
             MyAdp = new SqlDataAdapter("GP_SP_CalulateMonetary", DBManager.ConnectToDB());
            // MyAdp = new SqlDataAdapter("GP_SP_CalulateMonetary_test", DBManager.ConnectToDB());    
            //MyAdp = new SqlDataAdapter("GP_SP_CalulateMonetary_Modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@xmldata", SqlDbType.Xml).Value = xmldata;
             MyAdp.SelectCommand.Parameters.Add("@companyId", SqlDbType.Int).Value = companyid;
             MyAdp.SelectCommand.Parameters.Add("@options", SqlDbType.Int).Value = options;
             MyAdp.SelectCommand.Parameters.Add("@FormtypeId", SqlDbType.Int).Value = formtype;
             MyAdp.SelectCommand.Parameters.Add("@SetByUser", SqlDbType.Int).Value = userid;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             DBManager.CloseConnection();
         }
         return MyDs;

     }

    ///    create by Ravish Kumar Pandey---------------------------------------------
    ///    

     public DataSet Fetc_Branch_History_branch_table(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_feth_history_paymentDetailsTable", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;


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
     //make blank record for Payment Details // 15 Feb 2013 suggest by Vinay shukal
     //public DataSet Fetch_PaymentData(string FromDate, string ToDate)
     //{
     //    try
     //    {
     //        MyDs = new DataSet();
     //        MyAdp = new SqlDataAdapter("Gpsp_Fetch_paymentdata", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
     //        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

     //        if (FromDate != "") MyAdp.SelectCommand.Parameters.Add("@Fromdate", SqlDbType.VarChar, 50).Value = FromDate;
     //        if (ToDate != "") MyAdp.SelectCommand.Parameters.Add("@Todate", SqlDbType.VarChar, 50).Value = ToDate;



     //        MyAdp.Fill(MyDs);

     //    }
     //    catch (Exception ex)
     //    {

     //        ErrHandler.WriteError(ex.Message);
     //    }
     //    finally
     //    {
     //        DBManager.CloseConnection();
     //        MyAdp.Dispose();
     //    }
     //    return MyDs;
     //}


     // To Fetch Print Out Number Of Count Certificate
     public DataSet Fetch_printcountCertificate(int ddlselectCriteria, string SearchCriteria)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gpsp_Fetch_PrintCountresult", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlselectCriteria;
             MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 1000).Value = SearchCriteria;


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


     // dataset of Fetch Black list company 

     public DataSet Fetch_Balck_list_comapny(int ddlselectCriteria, string SearchCriteria)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gpsp_Fetch_BlacklistCompany", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlselectCriteria;
             MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 1000).Value = SearchCriteria;


             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();

         }
         return MyDs;

     }


     /// Fetch Inspection Agency Name And User Name  only Request Generated case for inspection
     public DataSet Fetch_Inspection_Agency_and_user_name(int AgencyuserId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("sp_fetch_agencynameagencyuser", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@Agencyuserid", SqlDbType.Int).Value = AgencyuserId;

             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();

         }
         return MyDs;

     }

     public DataSet Fetch_printout_date(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_fetchCompany_wise_firstprintdate", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@Companyid", SqlDbType.Int).Value = CompanyId;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }



     public DataSet Fetch_printout_date_modified(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_Fetc_Certificate_First_Print_date", DBManager.ConnectToDB());
             //MyAdp = new SqlDataAdapter("GP_sp_Fetc_Certificate_First_Print_date_onlinetest", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }

     public DataSet Fetch_ConsolidatedFor_MultipleMonetryLimit(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_GetConsolidateofMonetryLimit", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }


    //TO FETCH TO HISTORY
     public DataSet FetchTurnover_history(int CompanyId, int processid)
     {
         try
         {
             MyDs = new DataSet();
             //MyAdp = new SqlDataAdapter("GP_SP_view_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp = new SqlDataAdapter("GP_SP_view_modified_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@compid", SqlDbType.Int).Value = CompanyId;
             MyAdp.SelectCommand.Parameters.Add("@formtypeid", SqlDbType.Int).Value = processid;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }

     // Conditional Dataset for open popup for print page 

     public DataSet Get_PopUpPagedata(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             //MyAdp = new SqlDataAdapter("GP_SP_view_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp = new SqlDataAdapter("Gp_sp_CheckPrintpopup", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@companyid", SqlDbType.Int).Value = CompanyId;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }

     public int Update_ComapnY_Validityproposaldate(int comopanyId, string issuedate, string validitydate, string proposaldate, int options, string Comments)
     {
         try
         {
             MySqlComm = new SqlCommand("Gp_sp_Updatevalidityissuedate", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@companyid", SqlDbType.Int).Value = comopanyId;
             MySqlComm.Parameters.Add("@issuedate", SqlDbType.VarChar, 50).Value = issuedate;
             MySqlComm.Parameters.Add("@validitydate", SqlDbType.VarChar, 50).Value = validitydate;
             MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar, 50).Value = proposaldate;
             MySqlComm.Parameters.Add("@options", SqlDbType.VarChar, 50).Value = options;

 

            if (Comments != "") MySqlComm.Parameters.Add("@comment", SqlDbType.VarChar, 8000).Value = Comments;
             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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



   // ----------------------------------------------------------
     public DataSet Fetch_zone_branch_wise_Datewise_report(string Formadte, string todate, int CastID)
     {
         try
         {
             MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
             MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
             if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
             if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
             MyAdp.SelectCommand.CommandTimeout = 60000;
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
     public int F_insert_provisional_monetary_turnover(int comopanyId,int userid,int formtypeid)
     {
         try
         {
           MySqlComm = new SqlCommand("Gp_sp_Provisionalturnovermonetary", DBManager.ConnectToDB());
             //MySqlComm = new SqlCommand("Gp_sp_Provisionalturnovermonetary_test", DBManager.ConnectToDB()); 
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@companyId", SqlDbType.Int).Value = comopanyId;
             MySqlComm.Parameters.Add("@formtypeid", SqlDbType.Int).Value = formtypeid;
             MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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


     // ----------Enter Companyid to take Registration and Refrence Number for Renewal Company------------


     public DataSet Fetch_RegistrationnumberthroughCompanyid(int Companyid)
     {
         try
         {
             MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
             MyAdp = new SqlDataAdapter("GP_Sp_Fetch_registrationNumberCompanyid", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Companyid;
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

//--------------------------------------------



     public int f_Update_Comapny_PAN(int companyId, string PanNo, int typeddl, string PageUrl)
     {
         try
         {
             MySqlComm = new SqlCommand("Gp_sp_adminUpdatePANNO", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
             MySqlComm.Parameters.Add("@PANNO", SqlDbType.VarChar, 50).Value = PanNo;
             MySqlComm.Parameters.Add("@typeddl", SqlDbType.Int).Value = typeddl;
             MySqlComm.Parameters.Add("@PageUrl", SqlDbType.VarChar, 50).Value = PageUrl;
             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);

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



     public int F_Update_MonetaryLimit(string xmldata, int formtype, int userID, int companyid)
     {
         try
         {

             if (formtype == 1)
             {
                 MySqlComm = new SqlCommand("Gp_sp_UpdateMl", DBManager.ConnectToDB());
                 MySqlComm.CommandType = CommandType.StoredProcedure;
                 string passwd = "";
                 RandomKeyGenerator objkey = new RandomKeyGenerator();
                 passwd = objkey.Generate();

                 MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = xmldata;
                 MySqlComm.Parameters.Add("@FormtypeId", SqlDbType.Int).Value = formtype;
                 MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
                 MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
                 MySqlComm.Parameters.Add("@CompanyPassword", SqlDbType.VarChar, 100).Value = passwd;
                 SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
                 outError.Direction = ParameterDirection.Output;
                 MySqlComm.Parameters.Add(outError);

                 MySqlComm.ExecuteNonQuery();
                 ErrorId = Convert.ToInt32(outError.Value);
             }
             else if (formtype == 3)
             {
                 MySqlComm = new SqlCommand("Gp_sp_UpdateMl_Amendment", DBManager.ConnectToDB());
                 MySqlComm.CommandType = CommandType.StoredProcedure;
                 string passwd = "";
                 RandomKeyGenerator objkey = new RandomKeyGenerator();
                 passwd = objkey.Generate();

                 MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = xmldata;
                 // MySqlComm.Parameters.Add("@FormtypeId", SqlDbType.Int).Value = formtype;
                 MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
                 MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
                 MySqlComm.Parameters.Add("@CompanyPassword", SqlDbType.VarChar, 100).Value = passwd;
                 SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
                 outError.Direction = ParameterDirection.Output;
                 MySqlComm.Parameters.Add(outError);

                 MySqlComm.ExecuteNonQuery();
                 ErrorId = Convert.ToInt32(outError.Value);

             }

             else if (formtype == 2)
             {
                 MySqlComm = new SqlCommand("Gp_sp_UpdateMl_Renewal", DBManager.ConnectToDB());
                 MySqlComm.CommandType = CommandType.StoredProcedure;
                 string passwd = "";
                 RandomKeyGenerator objkey = new RandomKeyGenerator();
                 passwd = objkey.Generate();

                 MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = xmldata;
                 // MySqlComm.Parameters.Add("@FormtypeId", SqlDbType.Int).Value = formtype;
                 MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
                 MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
                 MySqlComm.Parameters.Add("@CompanyPassword", SqlDbType.VarChar, 100).Value = passwd;
                 SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
                 outError.Direction = ParameterDirection.Output;
                 MySqlComm.Parameters.Add(outError);

                 MySqlComm.ExecuteNonQuery();
                 ErrorId = Convert.ToInt32(outError.Value);
             }

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
     //-- select list for convert company to provisional to Regular
     public DataSet Convert_Company_To_Regular(int type, string Search)
     {
         try
         {
             MyDs = new DataSet();
             //MyAdp = new SqlDataAdapter("GP_SP_view_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp = new SqlDataAdapter("GP_sp_change_provisinal_regular_registration", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = type;
             if (Search != "") MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar, 500).Value = Search;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }

     // Update table Company provisional and Registration

     public int Update_Comapmy_chng_byadmin(int CompanyId, string PageUrl, int Type)
     {
         try
         {
             MySqlComm = new SqlCommand("Gp_sp_convert_regular_provisonal_insert_GPtbl_Company_change_byadmin", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;

             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             MySqlComm.Parameters.Add("@PageUrl", SqlDbType.VarChar, 500).Value = PageUrl;
             MySqlComm.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
             SqlParameter outError = new SqlParameter("@Output", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError("" + ex.Message.ToString());
         }
         return ErrorId;
     }


     public DataSet Fetch_Login_History(string Searchtext)
     {

         try
         {
             MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
             MyAdp = new SqlDataAdapter("GP_sp_showroleidten_login_history", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (Searchtext != "") MyAdp.SelectCommand.Parameters.Add("@SearchText", SqlDbType.VarChar, 500).Value = Searchtext;

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


     public DataSet Fetch_PanRegistration_old(int CompanyId)
     {

         try
         {
             MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
             MyAdp = new SqlDataAdapter("gp_sp_fetchPanregistrationRefreenceno", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

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


     public DataSet Fetch_MonetryLimitByUser(int BranchID)
     {

         try
         {
             MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
             MyAdp = new SqlDataAdapter("GP_SP_Cheack_MOnetry_Limit_By_User", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;

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


     // Upload For Renewal  

     public int Fetch_BrachUser_Inspection_tencore(int CompanyID, string Date, string InspactionPath)
     {
         int ErrorId = 0;
         try
         {
             MySqlComm = new SqlCommand("GP_sp_BrachUser_Inspection_tencore", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;

             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyID;
             if (Date != "") MySqlComm.Parameters.Add("@lastInspectiondate", SqlDbType.DateTime).Value = Date;
             if (InspactionPath != "") MySqlComm.Parameters.Add("@Inspectiondocpath", SqlDbType.VarChar, 5000).Value = InspactionPath;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     public DataSet Company_Mobile_Registersation(int CompanyID)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_Getmobile_registrationno", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyID;

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
    // For Cover Letter
     public DataSet Company_fetch_Coverletter_inspection(int CompanyID, int option)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_print_Inspection_Coverletter_Modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@UnitID", SqlDbType.Int).Value = CompanyID;
             MyAdp.SelectCommand.Parameters.Add("@options", SqlDbType.Int).Value = option;

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

     public int Approve_validity_request(int comopanyId)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_sp_approve_validity_request", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@companyID", SqlDbType.Int).Value = comopanyId;
             // MySqlComm.Parameters.Add("@Registartiondate", SqlDbType.VarChar, 50).Value = validitydate;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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


     public DataTable Company_Fetch_Approve_validitydate()
     {
         MyDt = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("GPsp_Company_requestvalidity_fetch_Search", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
             //MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlcriteria;
             //if (searchcriteria != "") MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 50).Value = searchcriteria;

             MyAdp.Fill(MyDt);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             MyAdp.Dispose();
             DBManager.CloseConnection();
         }
         return MyDt;
     }

    //Adding By Ravish Kumar Pandey on 16 june 2014 for show history table 

     public DataSet Company_fetch_history_AmedmentRenewalDate(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_historyAmendent_renewal_date", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;

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
     //Not Required
     // Amedment Table Change Date wise data new value
     public DataSet Company_fetch_history_AmedmentChange_new_value(int Companyid, int AmedmentId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_feth_Data_in_Amedment_table", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
             MyAdp.SelectCommand.Parameters.Add("@AmedmentID", SqlDbType.Int).Value = AmedmentId;
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

     // Not required
     public DataSet Company_fetch_history_AmedmentChange_new_value_historytable(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid_new_History", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = Companyid;
             // MyAdp.SelectCommand.Parameters.Add("@AmedmentID", SqlDbType.Int).Value = AmedmentId;
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


     public DataSet Company_fetch_history_AmedmentChange_new_value_historytable_modified(int Companyid, int Amedmentid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid_new_History_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = Companyid;
             // if(Amedmentdate!="01/01/1900")  MyAdp.SelectCommand.Parameters.Add("@AmedmentDate", SqlDbType.VarChar, 500).Value = Amedmentdate;
             MyAdp.SelectCommand.Parameters.Add("@AmedmentID", SqlDbType.Int).Value = Amedmentid;
             // MyAdp.SelectCommand.Parameters.Add("@AmedmentID", SqlDbType.Int).Value = AmedmentId;
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

     public DataSet Company_fetch_Chek_amedment_firsttime(int Companyid, int AmedmentId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_valueexistAmedmenttable", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
             MyAdp.SelectCommand.Parameters.Add("@AmedmentId", SqlDbType.Int).Value = AmedmentId;
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

     public DataSet Company_fetch_Chek_amedment_firsttime_modified(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_valueexistAmedmenttable_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;

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
     // Not Required
     public DataSet Company_fetch_Amedmentchange_view_history_table(int Companyid, int AmedmentID)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_feth_Data_in_Amedment_table_history", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
             MyAdp.SelectCommand.Parameters.Add("@AmedmentId", SqlDbType.Int).Value = AmedmentID;

             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());

         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }
     // End of Not required
     // Insert All data in History Table Before Amedment
     public int Company_Insert_HistoryTable(int CompanyId)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_Sp_Fetch_all_history_registerdCompany", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     //To fetch  bind history turnover

     //TO FETCH TO HISTORY
     public DataSet FetchTurnover_history_Turnover(int CompanyId, int processid, int AmedmentID)
     {
         try
         {
             MyDs = new DataSet();
             //MyAdp = new SqlDataAdapter("GP_SP_view_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp = new SqlDataAdapter("GP_SP_view_modified_monetaryturnoverdetails_ForHistorytable", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@compid", SqlDbType.Int).Value = CompanyId;
             MyAdp.SelectCommand.Parameters.Add("@formtypeid", SqlDbType.Int).Value = processid;
             MyAdp.SelectCommand.Parameters.Add("@AmedmentID", SqlDbType.Int).Value = AmedmentID;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }
     // insert CompanyEditData

     public int Company_Insert_HistoryTableCompanyEditData(int CompanyId)
     {
         try
         {
             MySqlComm = new SqlCommand("Gp_Sp_Insert_EditCompanyData", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);
             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     // Fetch Company Edit data  edit store Monetry Limit
     public DataSet Company_fetch_EditCompanyData(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPSp_Fetch_History_date_changebyCompany", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());

         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }


     public DataSet Company_fetch_history_EditCompanyData(int Companyid, string CompanyHistoryId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GPsp_Company_EditCompanyData_new_History_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = Companyid;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId_Pk", SqlDbType.Int).Value = Convert.ToInt32(CompanyHistoryId);
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


     // insert Data Into cunit turnOver and MonetryLimit data

     public int Company_Insert_HistoryTableTurnOverandMonetryLimit(int CompanyId)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_Sp_Insert_TurnoverHistoryCompanyEditData", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);
             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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
     // Fetch Company data edit TurnOver CompanyData 

     //TO FETCH TO HISTORY
     public DataSet FetchTurnover_history_Turnover_CompanyeditData(int CompanyId, int processid, int CompnayID_PK)
     {
         try
         {
             MyDs = new DataSet();
             //MyAdp = new SqlDataAdapter("GP_SP_view_monetaryturnoverdetails", DBManager.ConnectToDB());
             MyAdp = new SqlDataAdapter("GP_SP_view_modified_monetaryturnoverdetails_ForHistorytable_EditCompanyData", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@compid", SqlDbType.Int).Value = CompanyId;
             MyAdp.SelectCommand.Parameters.Add("@formtypeid", SqlDbType.Int).Value = processid;
             MyAdp.SelectCommand.Parameters.Add("@CompanyIDPK", SqlDbType.Int).Value = CompnayID_PK;
             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
         }
         finally
         {
             DBManager.CloseConnection();
             MyAdp.Dispose();
         }
         return MyDs;
     }

     //InsertCompany Edit Data Cunit Store

     public int Company_Insert_HistoryTableCunitStore(int CompanyId)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_Sp_Insert_CunitStoreHistoryCompanyEditData", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);
             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     // Fetch CunitStoreHistory_table_dateWise    

     public DataSet Company_fetch_history_CunitStores(int Companyid, int CompanyId_Pk)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_Sp_Fetch_Cunit_StoreDetails_History", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;

             MyAdp.SelectCommand.Parameters.Add("@CompanyId_Pk", SqlDbType.Int).Value = Convert.ToInt32(CompanyId_Pk);

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

     //Fetch Amedment Status----

     public DataSet Fetch_Amedment_Company_status_track_notRegisterd(string CompanyName)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gpsp_Amedment_company_status_track_for_admin_companywise", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;


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

     //Fetch Renewal Status----
     public DataSet Fetch_Renewal_Company_status_track_notRegisterd(string CompanyName)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gpsp_Renewal_company_status_track_for_admin_companywise", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = CompanyName;
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


     public DataSet Company_fetch_history_Companywise(int ddlsearch, string searchtext)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_Fetch_Histrory_Report_CompanyWise", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@ddlCriteriaSerch", SqlDbType.Int).Value = ddlsearch;
             if (searchtext != "") MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar, 500).Value = searchtext;
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


     public DataSet Company_fetch_history_AmedmentDate(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_Amedment_date", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;

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

     public DataSet Company_fetch_history_RenewalDate(int Companyid)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_Renewal_date", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Companyid;

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
	

    // End 

     public DataSet Fetch_Comment_on_certificatetable(int CompanyId)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gp_sp_fetch_Comments", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@ComapnayId", SqlDbType.Int).Value = CompanyId;
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

     public DataSet ExpiredList(string searchterm, string critiera)
     {
         MyDs = new DataSet();
         try
         {
             MyAdp = new SqlDataAdapter("GP_Sp_IncompleteCompanies", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

             if (critiera == "2")
             {
                 MyAdp.SelectCommand.Parameters.Add("@panno", SqlDbType.VarChar, 50).Value = searchterm;
             }
             else
             {
                 MyAdp.SelectCommand.Parameters.Add("@panno", SqlDbType.VarChar, 50).Value = null;
             }

             if (critiera == "1")
                 MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 50).Value = null;

             if (critiera == "3")
                 MyAdp.SelectCommand.Parameters.Add("@companyrefno", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyrefno", SqlDbType.VarChar, 50).Value = null;

             if (critiera == "4")
                 MyAdp.SelectCommand.Parameters.Add("@companyregisno", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyregisno", SqlDbType.VarChar, 50).Value = null;



             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             MyAdp.Dispose();
             DBManager.CloseConnection();
         }
         return MyDs;
     }

    // Changes by Ravish 28 Aug 2016 
     public DataSet ExpiredList_user_Delete(string searchterm, string critiera, int BranchId)
     {
         MyDs = new DataSet();
         try
         {
             MyAdp = new SqlDataAdapter("GP_Sp_IncompleteCompanies_Branch", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
           if(BranchId!=0)  MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value=BranchId;

             if (critiera == "2")
             {
                 MyAdp.SelectCommand.Parameters.Add("@panno", SqlDbType.VarChar, 50).Value = searchterm;
             }
             else
             {
                 MyAdp.SelectCommand.Parameters.Add("@panno", SqlDbType.VarChar, 50).Value = null;
             }

             if (critiera == "1")
                 MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 50).Value = null;

             if (critiera == "3")
                 MyAdp.SelectCommand.Parameters.Add("@companyrefno", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyrefno", SqlDbType.VarChar, 50).Value = null;

             if (critiera == "4")
                 MyAdp.SelectCommand.Parameters.Add("@companyregisno", SqlDbType.VarChar, 50).Value = searchterm;
             else
                 MyAdp.SelectCommand.Parameters.Add("@companyregisno", SqlDbType.VarChar, 50).Value = null;



             MyAdp.Fill(MyDs);
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             MyAdp.Dispose();
             DBManager.CloseConnection();
         }
         return MyDs;
     }



     public int delete_Incomplete_Companies_Branch(string companyies, int BranchID, int ZoneID, string Reson)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_sp_delete_companies_Incomplete_Branch", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@Companyid", SqlDbType.VarChar, 500).Value = companyies;
             if (BranchID != 0) MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
             if (ZoneID != 0) MySqlComm.Parameters.Add("@ZoneId", SqlDbType.Int).Value = ZoneID;
             if (Reson != "") MySqlComm.Parameters.Add("@Reson", SqlDbType.VarChar, 8000).Value = Reson;

             // MySqlComm.Parameters.Add("@Registartiondate", SqlDbType.VarChar, 50).Value = validitydate;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     public int delete_Incomplete_Companies(string companyies)
     {
         try
         {
             MySqlComm = new SqlCommand("GP_sp_delete_companies", DBManager.ConnectToDB());
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@Companyid", SqlDbType.VarChar, 500).Value = companyies;

             // MySqlComm.Parameters.Add("@Registartiondate", SqlDbType.VarChar, 50).Value = validitydate;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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

     public DataSet Fetch_Renewal_Company_status_track_UnderMonetryLimit(int BranchID, int RoleID)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("Gpsp_feth_maxMonetary_limit_modified", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
             MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleID;
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


    //Report for check  inspection Report Not Upload

     public DataSet Fetch_Inspection_Report_nullpath()
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("gp_sp_fetchuploadImage", DBManager.ConnectToDB());
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



     public DataSet Fetch_Inspection_Report_nullpath_mod()
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_fetchreportpath", DBManager.ConnectToDB());
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


     public DataSet Fetch_Inspection_Report_nullpath_mod_MOD(string Formadte, string todate, int agency)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_fetchreportpathMOD_RA", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

             if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
             if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;

              MyAdp.SelectCommand.Parameters.Add("@agency", SqlDbType.Int).Value = agency;

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


     public DataSet Fetch_Inspection_EmailList(string Formadte, string todate, int agency)
     {
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("GP_sp_fetchreportpathMOD_GetEmails_RA", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

             if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
             if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
             MyAdp.SelectCommand.Parameters.Add("@agency", SqlDbType.Int).Value = agency;

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

     public DataSet checkRenewalAmedmentCertificate(int CompanyID)
     {
           
         try
         {
             MyDs = new DataSet();
             MyAdp = new SqlDataAdapter("CheckRenewal_Amedment_For_Certificate", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;


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

     public DataTable Fetch_Hscode_Company(string Product, string Type)
     {
         DataTable dt_product = new DataTable();
         try
         { 
             MyAdp = new SqlDataAdapter("GPsp_store_fetch_Reportsearch_fetch_hs_code", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
          if(Product!="")   MyAdp.SelectCommand.Parameters.Add("@Product", SqlDbType.VarChar, 500).Value = Product;
          MyAdp.SelectCommand.Parameters.Add("@type", SqlDbType.VarChar, 50).Value = Type;

            MyAdp.Fill(dt_product);
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
         return dt_product;

     }


     public DataTable Fetch_Hscode_Company_by_Company_Id(int CompanyID)
     {
         DataTable dt_product = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("GP_Sp_Fetch_HSNPCCode", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (CompanyID != 0) MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;

             MyAdp.Fill(dt_product);
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
         return dt_product;

     }

     public DataTable fetch_master_product(int type, string product)
     {
         DataTable dt_product = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("GP_sp_searchHSCode ", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
             if (type != 0) MyAdp.SelectCommand.Parameters.Add("@tppe", SqlDbType.Int).Value = type;
             if (product != "") MyAdp.SelectCommand.Parameters.Add("@Product", SqlDbType.VarChar, 500).Value = product;

             MyAdp.Fill(dt_product);
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
         return dt_product;
     }


     public int insert_upadte_HsCode(int  Companyid, string HSCode, int type)
     {
         try
         {

             
             MySqlComm = new SqlCommand("test_sp_insert_update_hs_code", DBManager.ConnectToDB());//GP_SP_insert_update_company_HSCode
             MySqlComm.CommandType = CommandType.StoredProcedure;


             MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Companyid;
             MySqlComm.Parameters.Add("@HSCode", SqlDbType.VarChar, 50).Value = HSCode;
             MySqlComm.Parameters.Add("@type", SqlDbType.Int).Value = type;

             // MySqlComm.Parameters.Add("@Registartiondate", SqlDbType.VarChar, 50).Value = validitydate;

             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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


     public DataTable dt_fetch_hs_code_test()
     {
         DataTable dt_product = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("test_update_master_hs_codetest", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
          

             MyAdp.Fill(dt_product);
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
         return dt_product;
     }

     public int insert_upadte_HsCode_test( int hscodeID, string Hscode )
     {
         try
         {


             MySqlComm = new SqlCommand("test_update_hs_code", DBManager.ConnectToDB());//GP_SP_insert_update_company_HSCode
             MySqlComm.CommandType = CommandType.StoredProcedure;
             MySqlComm.Parameters.Add("@fGPtbl_HS_code_Id", SqlDbType.Int).Value =hscodeID;
             MySqlComm.Parameters.Add("@fGPtbl_HS_Code", SqlDbType.VarChar, 50).Value = Hscode;


             SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
             outError.Direction = ParameterDirection.Output;
             MySqlComm.Parameters.Add(outError);

             MySqlComm.ExecuteNonQuery();
             ErrorId = Convert.ToInt32(outError.Value);
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


    public DataTable Fetch_Unit_Enterprise( string companyId )
     {
         DataTable dt_product = new DataTable();
         try
         {
             MyAdp = new SqlDataAdapter("gp_sp_fetch_enterprise", DBManager.ConnectToDB());
             MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = Convert.ToInt32(companyId);
           //  MySqlComm.Parameters

             MyAdp.Fill(dt_product);
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
         return dt_product;
     }
    //public DataTable Fetch_Company_Branchwise(int BranchID)
    //{
    //    DataTable dt_product = new DataTable();
    //    try
    //    {
    //        MyAdp = new SqlDataAdapter("NSIC_Fetch_Company", DBManager.ConnectToDB());
    //        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = Convert.ToInt32(BranchID);
    //        //  MySqlComm.Parameters

    //        MyAdp.Fill(dt_product);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MyAdp.Dispose();
    //    }
    //    return dt_product;
    //}

    //public int Company_shift_Branch_update( int CompanyID, int BranchID, int StatusID)
    //{
    //    int  Returnvalue = 0;
    //    try
    //    {
    //        MySqlComm = new SqlCommand("GP_sp_Shift_Branch_Company", DBManager.ConnectToDB());
    //        MySqlComm.CommandType = CommandType.StoredProcedure;

    //        MySqlComm.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
    //        MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
    //        MySqlComm.Parameters.Add("@status", SqlDbType.Int).Value = StatusID;

    //        SqlParameter outError = new SqlParameter("@Outvalue", SqlDbType.Int);
    //        outError.Direction = ParameterDirection.Output;
    //        MySqlComm.Parameters.Add(outError);

    //      MySqlComm.ExecuteNonQuery();

    //        Returnvalue =  Convert.ToInt32( outError.Value.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }
    //    return Returnvalue;
    //}
    #region Created by Dhiraj
    public DataSet StateWise_CompanyDetails(string dateFrom, string dateto, int CompanyStatus, int Category, int UnitType, int AadhaarStatus)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_SP_StateWise_Company_report", DBManager.ConnectToDB());  //GPsp_Fetch_UnitAmedmentReport
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 7200;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@CompanyStatus", SqlDbType.Int).Value = CompanyStatus;
            MyAdp.SelectCommand.Parameters.Add("@Category", SqlDbType.Int).Value = Category;
            MyAdp.SelectCommand.Parameters.Add("@UnitType", SqlDbType.Int).Value = UnitType;
            MyAdp.SelectCommand.Parameters.Add("@AadhaarStatus", SqlDbType.Int).Value = AadhaarStatus;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "   " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }



    public DataSet Deleted_CompanyDetails(string dateFrom, string dateto, string ReasonforDelete, int Branch_Id)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_SP_Deleted_Company_report_New", DBManager.ConnectToDB());  //GPsp_Fetch_UnitAmedmentReport
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 7200;
            MyAdp.SelectCommand.Parameters.Add("@FromDate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@ToDate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@ResonforDelete", SqlDbType.VarChar, 500).Value = ReasonforDelete;
            MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch_Id;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "   " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    #endregion
    #region
    public DataSet Bind_Misc_data()
    {
        try
        {
            MyDs = new DataSet();
            /* MyAdp = new SqlDataAdapter("sp_Take_AllCompanyData", DBManager.ConnectToDB());*/ // this procedure if pass Between date than show error so please Modified again and again

            MyAdp = new SqlDataAdapter("sp_SCSTPRODUCTSNCR", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
           
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    public DataSet Bind_Misc_data_New()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("sp_Take_AllCompanyData_New_With_Product_Details", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    #endregion  

    #region Dhiraj NSIC For SC-ST Fee Changes
    public DataSet CategoryCast_Details_ByCompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CategoryCast_Details_ByCompanyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }



    public int Update_SC_ST_FeeDetails(int Companyid, Double Amount, Double ServiceTax)
    {
        int ErrorIdNew = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Step7_SC_ST_FeeDetails", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;


            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_PD_SCST_Amount", SqlDbType.VarChar, 50).Value = Amount;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ServiceTax", SqlDbType.VarChar, 50).Value = ServiceTax;

            //SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            //outError.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(outError);

            ErrorIdNew= MySqlComm.ExecuteNonQuery();
            //ErrorIdNew = Convert.ToInt32(outError.Value);
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

        return ErrorIdNew;
    }


    #endregion
    public DataSet POI_Details(int Status)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_PointOfInterest_Admin", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    public int Company_PerformanceSheet_insert(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            ErrorId = cmd.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }

        return ErrorId;
    }
    public DataTable Company_FetchPerformanceSheetList(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchPerformanceSheetByComapny_Step3_RA_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Directors_CompanyId", SqlDbType.Int).Value = companyid;
            // MyDr = MySqlComm.ExecuteReader();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
        }
        return MyDs.Tables[0];
    }
    public int Company_PerformanceSheet_insert_Edit(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_Edit_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            ErrorId = cmd.ExecuteNonQuery();
           // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();

        }

        return ErrorId;
    }





    public int Company_Insert_Step1_CFSS(string  Companyname,string PAN, string Address, string PIN,
        int StateId, int CityId, string Phone, string Email, string Mobile, int SpclCategory, int Castid, int UnitSizeId,
        int Branchid, string ProposalDate, string Aadhaar, int PaymentType, int ConstId, string CHQDD_UTR,
        string BankName,string BranchName, string PayableAt, decimal Amount, decimal Servicetax, int Feeid, string FeeDesc, string FileName,
        string FilePath, string PaymentDate ,string Remarks, decimal ftLastYearTurnOver
        )
    {
        int tblId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step1_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar).Value = Companyname;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar).Value = PAN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar).Value = Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar).Value = PIN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar).Value = Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar).Value = Email;

            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar).Value = Mobile;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = SpclCategory;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = Castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = UnitSizeId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = Branchid;
            MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar).Value = ProposalDate;
            MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar).Value = Aadhaar;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = PaymentType;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C123_ConstitutionId", SqlDbType.Int).Value = ConstId;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_UTR", SqlDbType.VarChar).Value = CHQDD_UTR;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar).Value = BranchName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar).Value = PayableAt;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Decimal).Value = Amount;
            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitServiceTax", SqlDbType.Decimal).Value = Servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitFeeId", SqlDbType.Int).Value = Feeid;

            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitSlabInstruction", SqlDbType.VarChar).Value = FeeDesc;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar).Value = FileName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar).Value = FilePath;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.VarChar).Value = PaymentDate;
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar).Value = Remarks;

            MySqlComm.Parameters.Add("@ftLastYearTurnOver", SqlDbType.Decimal).Value = ftLastYearTurnOver;

            
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            tblId = Convert.ToInt32(outError.Value);
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
        return tblId;
    }


    public int CFSS_MasterBranch_Fetch_BranchId(int UserId)
    {
        int BranchId = 0;
        try
        {
            MySqlComm = new SqlCommand("CFSS_GPsp_Fetch_MasterBranch_ByUserId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Char, 10).Value = UserId;
            BranchId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return BranchId;
    }


    //public DataSet Company_PANnoCheck_Modified(string PANno, int ConstitutionId, string AadhaarNo)
    //{
    //    try
    //    {
    //        MyDs = new DataSet();
    //        MyAdp = new SqlDataAdapter("GPsp_CheckPANno_modified_Proprietorship", DBManager.ConnectToDB());
    //        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
    //        MyAdp.SelectCommand.Parameters.Add("@ConstitutionId", SqlDbType.Char, 10).Value = ConstitutionId;
    //        MyAdp.SelectCommand.Parameters.Add("@UAM", SqlDbType.Char, 15).Value = AadhaarNo;

    //        MyAdp.Fill(MyDs);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MyAdp.Dispose();
    //    }
    //    return MyDs;
    //}

    //public SqlDataReader Company_Fetch_ByPANno_Step1(string PANno, string AadhaarNo)
    //{
    //    try
    //    {
    //        MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Proprietorship", DBManager.ConnectToDB());
    //        MySqlComm.CommandType = CommandType.StoredProcedure;
    //        MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
    //        MySqlComm.Parameters.Add("@UAM", SqlDbType.Char, 15).Value = AadhaarNo;
    //        MyDr = MySqlComm.ExecuteReader();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }
    //    return MyDr;
    //}

    //public SqlDataReader Company_Fetch_ByPANno_Step1_Grid(string PANno, string AadhaarNo)
    //{
    //    try
    //    {
    //        MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Grid_Proprietorship", DBManager.ConnectToDB());
    //        MySqlComm.CommandType = CommandType.StoredProcedure;
    //        MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
    //        MySqlComm.Parameters.Add("@UAM", SqlDbType.Char, 15).Value = AadhaarNo;
    //        MyDr = MySqlComm.ExecuteReader();
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }
    //    return MyDr;
    //}


    //public DataSet FetchCompanyPAN_By_CompanyId(int CompanyId)
    //{
    //    try
    //    {
    //        MyDs = new DataSet();
    //        MyAdp = new SqlDataAdapter("Gpsp_FetchCompanyPAN_By_CompanyId", DBManager.ConnectToDB());
    //        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
    //        MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;


    //        MyAdp.Fill(MyDs);
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MyAdp.Dispose();
    //    }
    //    return MyDs;
    //}

    //public int Company_PANnoCheck(string PANno, int branchid)
    //public int Company_PANnoCheck(string PANno, string Uam)
    //{
    //    int Status = 0;
    //    try
    //    {
    //        MySqlComm = new SqlCommand("GPsp_CheckPANno_Proprietorship", DBManager.ConnectToDB());
    //        MySqlComm.CommandType = CommandType.StoredProcedure;

    //        MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
    //        MySqlComm.Parameters.Add("@Uam", SqlDbType.Char, 15).Value = Uam;
    //        //MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
    //        Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }
    //    return Status;
    //}



    //public int UpadteConstitution_Properitership(int CompanyId,int ConstitutionId)
    //{
    //    try
    //    {
    //        MySqlComm = new SqlCommand("GPsp_Update_Constitution_Properitership", DBManager.ConnectToDB());//GP_SP_insert_update_company_HSCode
    //        MySqlComm.CommandType = CommandType.StoredProcedure;
    //        MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
    //        MySqlComm.Parameters.Add("@ConstitutionId", SqlDbType.VarChar, 50).Value = ConstitutionId;
    //        ErrorId= MySqlComm.ExecuteNonQuery();

    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }

    //    return ErrorId;
    //}
}
