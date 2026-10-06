using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Collections;

/// <summary>
/// Summary description for clsAmendment
/// </summary>
public class clsAmendment
{
    public clsAmendment()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public Hashtable CompanyAmendment_Insert_Step1(string fGPtbl_Company_Name, string fGPtbl_Company_PAN, string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax, string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address, string fGPtbl_CUnit_1B_Factory_Zip, int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax, string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId, int fGPtbl_CUnit_BranchId, string fGPtbl_CUnitBranch_Address, string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId, string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax, string fGPtbl_CUnitBranch_Email, int UserId, int UnitId)
    {
        int AmendmentId = 0;
        int CompanyId = 0;


        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step1", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_Company_GPReferenceNo", SqlDbType.VarChar, 500).Value = fGPtbl_Company_GPReferenceNo;
            //// MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.Char, 10).Value = fGPtbl_Company_PAN;
            // MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            // MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            // MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            // MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            //if (fGPtbl_Company_HeadOffice_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Fax;
            // MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Email;
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
            //
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outAmendment = new SqlParameter("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int);
            outAmendment.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outAmendment);

            SqlParameter outCompany = new SqlParameter("@CompanyId", SqlDbType.Int);
            outCompany.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompany);

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            AmendmentId = Convert.ToInt32(outAmendment.Value);
            //UnitId = Convert.ToInt32(outUnit.Value);
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

    public int Company_SSInoCheck(string SSIno)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANnoSSIno", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

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

    //TO CHECK REGISTRATION NO OF UNIT OF COMPANY
    public int Company_RegistrationNoCheck(string RegistrationNo)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckRegistratioNo", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_RegistrationNo", SqlDbType.Char, 10).Value = RegistrationNo;
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


    public SqlDataReader Company_Fetch_ByUnit_Step3(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_FetchByCompany_Step3", DBManager.ConnectToDB());
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

    public int Company_Insert_Step3(int fGPtbl_CUnit_CompanyId, int fGPtbl_CUnit_3D, string fGPtbl_CUnit_3E, string fGPtbl_CUnit_3F, string fGPtbl_CUnit_3G, string fGPtbl_CUnit_3H, string fGPtbl_CUnit_3I, string fGPtbl_CUnit_3J, string fGPtbl_CUnit_3K1, string fGPtbl_CUnit_3L, string fGPtbl_CUnit_3M, string fGPtbl_CUnit_3N, string fGPtbl_CUnit_3O, string fGPtbl_CUnit_4A, string fGPtbl_CUnit_4B, int UserId, string amdrenewal)
    {
        try
        {
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step3", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step3", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Id;
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

    public DataTable Company_Fetch_FactoryLocation(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Fetch_CityNameOfFactory_ByCompanyId", DBManager.ConnectToDB());
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


    public DataTable CompanyAmendment_Fetch_StoreByCompanyId(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Fetch_StoreByCompanyId", DBManager.ConnectToDB());
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

    public int CompanyAmendment_Update_Step3_Store(int fGPtbl_CUnitAmendment_Store_Id, int fGPtbl_CUnit_Store_UnitId, string storecode, string fGPtbl_CUnit_Store_DescriptionStoresProducts,
        string fGPtbl_CUnit_Store_RefOfBISSpecification, string fGPtbl_CUnit_Store_ModelBrand, string fGPtbl_CUnit_Store_LimitingSizeCapacityRating, string fGPtbl_CUnit_Store_AnnualProductionCapacity,
        int fGPtbl_CUnit_Store_NoOfShifts, string fGPtbl_CUnit_Store_QualitativeCapacity, string fGPtbl_CUnit_Store_QuantitativeCapacity, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Update_Store_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnitAmendment_Store_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_SNo", SqlDbType.VarChar, 200).Value = storecode;
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


    public int CompanyAmendment_Update_Step3_Store_RA(int fGPtbl_CUnitAmendment_Store_Id, int fGPtbl_CUnit_Store_UnitId, string storecode, string fGPtbl_CUnit_Store_DescriptionStoresProducts,
       string fGPtbl_CUnit_Store_RefOfBISSpecification, string fGPtbl_CUnit_Store_ModelBrand, string fGPtbl_CUnit_Store_LimitingSizeCapacityRating, string fGPtbl_CUnit_Store_AnnualProductionCapacity,
       int fGPtbl_CUnit_Store_NoOfShifts, string fGPtbl_CUnit_Store_QualitativeCapacity, string fGPtbl_CUnit_Store_QuantitativeCapacity, int UserId, string IsOnlyStoreUpdate)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Update_Store_Step3_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnitAmendment_Store_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_UnitId", SqlDbType.Int).Value = fGPtbl_CUnit_Store_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_SNo", SqlDbType.VarChar, 200).Value = storecode;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_DescriptionStoresProducts", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_DescriptionStoresProducts;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_RefOfBISSpecification", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_RefOfBISSpecification;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_ModelBrand", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_ModelBrand;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_LimitingSizeCapacityRating", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_LimitingSizeCapacityRating;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_AnnualProductionCapacity", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_Store_AnnualProductionCapacity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_NoOfShifts", SqlDbType.Int).Value = fGPtbl_CUnit_Store_NoOfShifts;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_QualitativeCapacity", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Store_QualitativeCapacity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Store_QuantitativeCapacity", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_Store_QuantitativeCapacity;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            if (IsOnlyStoreUpdate != null)
            {
                MySqlComm.Parameters.Add("@IsOnlyStoreUpdate", SqlDbType.Bit).Value = 0; //will Set Pending Accounts to 0

            }

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

            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Store_Step3_All", DBManager.ConnectToDB());
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

    public void Store_Insert_RA(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int Insertupdate, int userid, string isOnlyStoreEdit)
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

            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Store_Step3_All_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = Insertupdate;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            if (isOnlyStoreEdit != null)
            {
                MySqlComm.Parameters.Add("@IsStoreUpdateOnly", SqlDbType.Bit).Value = 0; //will Set Pending Accounts to 0
            }
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

    public DataSet CompanyAmendment_Fetch_ByCompany_Step4(int companyid)  // Step  4
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchByCompanyId_Step4_RA", DBManager.ConnectToDB());
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

    public int CompanyAmendment_Insert_Step4(int fGPtbl_CUnit_Id, int fGPtbl_CUnit_CompanyId, string fGPtbl_CUnit_5A1, string fGPtbl_CUnit_5A2, string fGPtbl_CUnit_5BB1, string fGPtbl_CUnit_5BB2, string fGPtbl_CUnit_5C, float fGPtbl_CUnit_6A1, float fGPtbl_CUnit_6A2, float fGPtbl_CUnit_6A3, float fGPtbl_CUnit_6B, float fGPtbl_CUnit_6B2, string fGPtbl_CUnit_7A, string fGPtbl_CUnit_7B, string fGPtbl_CUnit_7C, string fGPtbl_CUnit_7D, string fGPtbl_CUnit_7E, string fGPtbl_CUnit_8, string fGPtbl_CUnit_9, string fGPtbl_CUnit_10, string fGPtbl_CUnit_11, string fGPtbl_CUnit_12A, string fGPtbl_CUnit_12B, string fGPtbl_CUnit_12C, string fGPtbl_CUnit_13A, string fGPtbl_CUnit_13B, int UserId, string amdrenewal)
    {
        try
        {
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step4_RA", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step4_RA", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;

            //MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int).Value = fGPtbl_CUnitAmendment_Id;
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







    #region AMENDMENT PROCESS
    //=============UTTAM

    //TO FETCH DATASET TO FILL STEP 2 AMENDMENT FORM
    public DataSet CompanyAmendment_FetchDataset_Step2(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchByCompanyId_Step2", DBManager.ConnectToDB());
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
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;

    }

    //TO INSERT INTO AMENDMENT TABLE STEP 2
    public int CompanyAmendment_InsertUpdate_Step2(List<string> directorList, List<string> grdssiList,
                            string dateofInc, string dateofCOB, int constid, string partenerDetail, int companyid, int userid, string amdrenewal)
    {
        try
        {
            string strDirectorList = string.Empty;
            string strSsilist = string.Empty;

            foreach (string strDir in directorList)
            {
                strDirectorList += strDir;
            }

            foreach (string strSSi in grdssiList)
            {
                strSsilist += strSSi;
            }

            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step2_RA", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step2_RA", DBManager.ConnectToDB());
            }


            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@directorList", SqlDbType.Text).Value = strDirectorList;
            MySqlComm.Parameters.Add("@grdssiList", SqlDbType.VarChar, 1000).Value = strSsilist;
            MySqlComm.Parameters.Add("@dateofInc", SqlDbType.VarChar, 50).Value = dateofInc;
            MySqlComm.Parameters.Add("@dateofCOB", SqlDbType.VarChar, 50).Value = dateofCOB;
            MySqlComm.Parameters.Add("@constid", SqlDbType.Int).Value = constid;
            MySqlComm.Parameters.Add("@partenerDetail", SqlDbType.VarChar, 1000).Value = partenerDetail;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid;

            SqlParameter successId = new SqlParameter("@Output", SqlDbType.Int);
            successId.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(successId);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(successId.Value);
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
    // check addhar number  functtion
    public int CompanyAmendment_checkadharNo(int companyid, string aadharnumber)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckAddhar_Amendment", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.Char, 12).Value = aadharnumber;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

            SqlParameter OutVal = new SqlParameter("@Output", SqlDbType.Int);
            OutVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(OutVal);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(OutVal.Value);
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



    //TO CHECK WHETHER PAN IS REGISTER WITH OTHER COMPANY
    public int CompanyAmendment_CheckPanNo(int companyid, string pan)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANno_Amendment", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Pan_no", SqlDbType.Char, 10).Value = pan;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

            SqlParameter OutVal = new SqlParameter("@Output", SqlDbType.Int);
            OutVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(OutVal);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(OutVal.Value);
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


    //TO INSERT INTO COMPANY TABLE
    public int CompanyAmendment_InsertUpdate_CompanyBranch_Step1(int insertupdateflag, string pan, string compname, string compadd, int stateid, int city,
                      string compzip, string compphone, string compfax, string compemail, string braddress, string brzip, int brstateid, int brcityid, string brphone,
                      string brfax, string bremail, int userid, int companyid, string amdrenewal, string proposaldate, string proposalnumber,
                     string commobno, string Proposaldate_text, string AadharNumber, bool fGPtbl_Company_MSME, string IsOnlyStoreUpdate)
    {
        try
        {
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step1_RA", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step1_RA", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;



            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = insertupdateflag;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar, 500).Value = pan;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 100).Value = compname;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 100).Value = compadd;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = stateid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = city;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 50).Value = compzip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 50).Value = compfax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 50).Value = compphone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = commobno;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 50).Value = compemail;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Address", SqlDbType.VarChar, 100).Value = braddress;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Zip", SqlDbType.VarChar, 100).Value = brzip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_StateId", SqlDbType.Int).Value = brstateid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_CityId", SqlDbType.Int).Value = brcityid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Phone", SqlDbType.VarChar, 100).Value = brphone;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Fax", SqlDbType.VarChar, 50).Value = brfax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Email", SqlDbType.VarChar, 50).Value = bremail;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_MSME", SqlDbType.Bit).Value = fGPtbl_Company_MSME;



            if (amdrenewal == "amendment")
            {
                if (Proposaldate_text.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_ProposalDate", SqlDbType.VarChar, 50).Value = Proposaldate_text;
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;

                if (IsOnlyStoreUpdate != null)
                {
                    MySqlComm.Parameters.Add("@IsOnlyStoreUpdate", SqlDbType.Bit).Value = 0; //will Set Pending Accounts to 0

                }


            }
            if (amdrenewal == "renewal")
            {
                MySqlComm.Parameters.Add("@Proposaldate", SqlDbType.VarChar).Value = proposaldate;
                MySqlComm.Parameters.Add("@Proposalnumber", SqlDbType.VarChar, 50).Value = proposalnumber;
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;
            }


            SqlParameter Outputval = new SqlParameter("@ErrorId", SqlDbType.Int);
            Outputval.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(Outputval);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(Outputval.Value);
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



    //TO INSERT INTO FACTORY AMENDMENT TABLE
    public void CompanyAmendment_InsertUpdateFactory_step1(int insertupdateflag, string factaddress, string factpin, int factstateid, int factcity,
                        string factphone, string factfax, string factemail, int catid, int castid, int sizeid, int unitid, int companyid, int userid, int branchid, string amdrenewal, int natureOfOperation)
    {
        try
        {
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_InserFactory_Step1_RA", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_InserFactory_Step1_RA", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = insertupdateflag;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 500).Value = factaddress;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = factpin;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = factstateid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = factcity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 50).Value = factphone;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Fax", SqlDbType.VarChar, 50).Value = factfax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Email", SqlDbType.VarChar, 50).Value = factemail;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = catid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = sizeid;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Enterprise", SqlDbType.Int).Value = natureOfOperation;
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


    //TO FETCH ALL RECORDS TO SHOW ON PAGE
    public DataSet CompanyAmendment_CompanyDetails_Fetch_ByCompanyId(int companyid, int formtypeid)
    {
        try
        {
            MyDs = new DataSet();
            if (formtypeid == 3)//FOR AMENDMENT
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Waiting_Fetch_ForView_ByCompanyid", DBManager.ConnectToDB());
            }
            else if (formtypeid == 2)//FOR RENEWAL
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_ForView_ByCompanyid", DBManager.ConnectToDB());
            }

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


    //TO FETCH CHANGES ON RECORDS
    public DataSet CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(int companyid, int formtypeid)
    {
        try
        {
            MyDs = new DataSet();
            if (formtypeid == 3)//FOR AMENDMENT
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchChanges_ByCompanyId", DBManager.ConnectToDB());
            }
            else if (formtypeid == 2)//FOR RENEWAL
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_FetchChanges_ByCompanyId", DBManager.ConnectToDB());
            }

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
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    //TO FETCH ALL STEPS CHANGES ALLOWED
    public DataTable CompanyAmendment_CompanyChangesAllowed_Fetch()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Fetch_AllStepChanges", DBManager.ConnectToDB());
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

    //TO FETCH THOSE UNIT ID WHOSE VALUE CHANGED
    public DataTable CompanyAmendment_Fetch_UnitChanged_Datatable(int companyid)
    {
        DataSet MydsChange = new DataSet();
        //objAmendment = new clsAmendment();
        List<int> UnitIdchanged = new List<int>();
        DataTable dt, dt2 = new DataTable();
        try
        {
            MydsChange = CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(companyid, 3);

            dt = MydsChange.Tables[1];
            if (dt.Rows.Count > 0)
            {
                UnitIdchanged = (from r in dt.AsEnumerable()
                                 where r.Field<int>("fGPtbl_CUnit_3E") == 0 || r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_SSIRegistrationNo") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_SSIRegistrationDate") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_CategoryAreaId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_CategoryCastId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_CategoryUnitSizeId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Address") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Zip") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_StateId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_CityId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3C123_ConstitutionId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3C5") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3D") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3F") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3G") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3H") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3I") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3J") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3K1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3K2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3L") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3M") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3N") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_3O") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_4A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_4B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BA") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BB1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5BB2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_5C") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6A1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6A2") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_6B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7C") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7D") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_7E") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_8") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_9") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_10") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_11") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_12A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_12B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_13A") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_13B") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_13C") == 0
                                 select r.Field<int>("fGPtbl_CUnit_Id")).ToList();

                /* foreach (var onl in UnitId)
                         {
                     unitid1 += onl.ToString() + ",";
               
                      }*/
            }

            dt2 = CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(companyid, UnitIdchanged);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return dt2;

    }

    public DataTable CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(int companyid)
    {
        DataSet MydsChange = new DataSet();
        List<int> UnitIdchangedStore = new List<int>();
        List<int> UnitIdchanged = new List<int>();
        DataTable dt, dtStore = new DataTable();
        DataTable dt2 = new DataTable();


        try
        {
            MydsChange = CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(companyid, 3);

            dt = MydsChange.Tables[1];
            if (dt.Rows.Count > 0)
            {
                UnitIdchanged = (from r in dt.AsEnumerable()
                                 where
                                 r.Field<int>("fGPtbl_CUnit_5A1") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Address") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_Zip") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_StateId") == 0 ||
                                 r.Field<int>("fGPtbl_CUnit_1B_Factory_CityId") == 0 
                                 //||
                                 //r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_5BA") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_5BB1") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_5BB2") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_5C") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_6A1") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_6A2") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_6B") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_7A") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_7B") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_7C") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_7D") == 0 ||
                                 //r.Field<int>("fGPtbl_CUnit_7E") == 0
                                 select r.Field<int>("fGPtbl_CUnit_Id")).ToList();

                /* foreach (var onl in UnitId)
                         {
                     unitid1 += onl.ToString() + ",";
               
                      }*/
            }

            dtStore = MydsChange.Tables[2];

            if (dtStore.Rows.Count > 0)
            {
                UnitIdchangedStore = (from r in dtStore.AsEnumerable()
                                      where
                                      r.Field<int>("fGPtbl_CUnit_Store_DescriptionStoresProducts") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_RefOfBISSpecification") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_ModelBrand") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_AnnualProductionCapacity") == 0 ||
                                      r.Field<int>("fGPtbl_CUnit_Store_NoOfShifts") == 0
                                      select r.Field<int>("fGPtbl_CUnit_Store_UnitId")).ToList();

                /* foreach (var onl in UnitId)
                         {
                     unitid1 += onl.ToString() + ",";
               
                      }*/
            }

            UnitIdchanged.AddRange(UnitIdchangedStore);

            IEnumerable<int> distinctUnit = UnitIdchanged.Distinct();
            List<int> distinctUnit1 = new List<int>();
            foreach (int un in distinctUnit)
            {
                distinctUnit1.Add(un);
            }

            dt2 = CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(companyid, distinctUnit1);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return dt2;

    }


    //TO CHANGE COMPANY STATUS
    public int CompanyAmendment_BranchAccount_ChangecompanyStatus(int companyid, string monetarylimit, int userid)
    {
        string password = string.Empty;
        try
        {
            RandomKeyGenerator objRKG = new RandomKeyGenerator();
            password = objRKG.Generate();
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Monetary_Update_ById_zone_modified", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = companyid;
            if (monetarylimit != "a") MySqlComm.Parameters.Add("@MonetaryLimit", SqlDbType.VarChar, 1000).Value = monetarylimit;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@PasswordAMD", SqlDbType.NVarChar, 4000).Value = password;

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


    //==============UTTAM


    //==============GAURAV SRIVASTAV=======


    //TO FETCH ALL DETAILS OF FIRST PAGE
    public DataSet Company_UnitDetails_Fetch_ByCompanyId(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchByCompanyId_Step1", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
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


    //==========GAURAV SRIVASTAV=========
    #endregion



    #region WAITING COMPANIES AMENDMENT

    //TO FETCH CUNITAMENDMENT ID BASED ON UNIT ID
    public int CompanyAmendment_WaitingCompany_FetchAmendmentId(int unitId)// Company Id
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Waiting_Fetch_AmendmentId_ByUnitId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = unitId;

            SqlParameter outVal = new SqlParameter("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int);
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

    // To Fetch  Countment Waiting List As Company  ID 

    public DataSet CompanyAmendment_WaitingCompany_FetchAmendmentId_Modifiede(int CompanyId)// Company Id
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Amdment_Company_Waiting_Fetch_CUnit_ByCompId_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
            //SqlParameter outVal = new SqlParameter("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int);
            //outVal.Direction = ParameterDirection.Output;
            //ErrorId = Convert.ToInt32(outVal.Value);

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


    //TO FETCH ALL DATA OF AMENDMENT WITH WAITING STATUS
    public DataSet CompanyAmendment_WaitingCompany_FetchAllCompanies_Dataset(int userid, string amdrenewal)
    {
        try
        {
            MyDs = new DataSet();
            if (amdrenewal == "amendment")
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Waiting_Fetch_Company_All", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_Company_All", DBManager.ConnectToDB());
            }

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



    //TO FETCH DETAILS OF CUNIT AMENDMENT WITH WAITING STATUS // Uttam sir 
    public DataTable CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(int companyid, List<int> Changedunit)
    {
        int unit1 = 0, unit2 = 0, unit3 = 0, unit4 = 0, unit5 = 0;
        try
        {
            for (int i = 0; i < Changedunit.Count(); i++)
            {
                if (i == 0)
                    unit1 = Changedunit[0];
                else if (i == 1)
                    unit2 = Changedunit[1];
                else if (i == 2)
                    unit3 = Changedunit[2];
                else if (i == 3)
                    unit4 = Changedunit[3];
                else if (i == 4)
                    unit5 = Changedunit[4];
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Waiting_Fetch_CUnit_ByCompanyid", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            if (unit1 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit1", SqlDbType.Int).Value = unit1;
            if (unit2 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit2", SqlDbType.Int).Value = unit2;
            if (unit3 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit3", SqlDbType.Int).Value = unit3;
            if (unit4 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit4", SqlDbType.Int).Value = unit4;
            if (unit5 != 0)
                MyAdp.SelectCommand.Parameters.Add("@Unit5", SqlDbType.Int).Value = unit5;

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



    //TO ENTER REASON OF REJECTION BY BRANCH
    public int CompanyAmendment_WaitingCompany_Rejection(int CUnitamendmentid, int CUnitid, int Formtypeid, string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Waiting_Update_RejectUnit_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = CUnitid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int).Value = CUnitamendmentid;
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


    //WHEN BRANCH ACCEPT UPDATE THE UNIT AND INSPECTION TABLE
    public Hashtable CompanyAmendment_WaitingCompany_Accept(int CUnitid, int InspUserId, int UserId)
    {
        string email = "";

        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Waiting_Update_AcceptCompany_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = CUnitid;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@InspUserId", SqlDbType.Int).Value = InspUserId;


            SqlParameter outValue = new SqlParameter("@Output", SqlDbType.Int);
            outValue.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outValue);

            SqlParameter outEmail = new SqlParameter("@fGPtbl_TIAB_EmailId", SqlDbType.VarChar, 500);
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




    #endregion


    //=====================================================
    //===================================================

    #region ACCEPTED COMPANIES AMENDMENT


    //INSPECTION DATE TO BE ISSUED 
    public DataSet CompanyAmendment_AcceptedCompany_InspectionDatetobeIssued_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Accepted_Fetch_DatetobeIssued", DBManager.ConnectToDB());
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


    //DATE ALREADY ISSUED  
    public DataSet CompanyAmendment_AcceptedCompany_InspectionDatealreadyIssued_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Accepted_Fetch_DatealreadyIssued", DBManager.ConnectToDB());
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


    //TO FETCH ALL UNITS WHOSE INSPECTION IS COMPLETE FOR AMENDMENT
    public DataSet CompanyAmendment_ICompleteAmendment_Dataset(int Userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_ICompleteRegistration_Fetch_All", DBManager.ConnectToDB());
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

    //TO FETCH ALL UNITS OF COMPANY WHOSE INSPECTION IS COMPLETE FOR AMENDMENT FOR VIEWPAGE
    public DataTable CompanyAmendment_ICompleteAmendment_FetchByCompanyId(int companyid, int userid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_ICompleteRegistration_Fetch_ByCompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = userid;
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


    //TO UPDATE TABLE FOR UNITAMENDMENT BY BRANCH IN CASE OF REJECTION
    public int CompanyAmendment_ICompleteAmendment_Rejection(int CUnitamendmentid, int CUnitid, int Formtypeid, string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_ICompleteRegistration_Update_Reject_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Id", SqlDbType.Int).Value = CUnitamendmentid;
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


    //TO UPDATE TABLE FOR UNITAMENDMENT BY ID IN CASE OF ACCEPT BY BRANCH
    public int CompanyAmendment_ICompleteAmendment_Accept(int companyid, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_ICompleteRegistration_Update_CUnitRegStatus_ById_RA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@companyid", SqlDbType.Int).Value = companyid;
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


    //TO FETCH DATA TO SET MONETARY LIMIT FOR BRANCH ACCOUNT
    public DataSet CompanyAmendment_MonetaryLimit_Fetch_Dataset(int UserId)
    {
        try
        {

            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Monetary_Fetch_All_RA", DBManager.ConnectToDB());
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



    //TO INSERT DATA FOR MONETARY LIMIT BY BRANCH ACCOUNT
    public int CompanyAmendment_MonetaryLimit_Update(int CUnitamendmentid, int Monetaryid, int Formtypeid, int Unitid, double Monetarylimit, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Monetary__Update_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_RenewalAmendmentId", SqlDbType.Int).Value = CUnitamendmentid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_Id", SqlDbType.Int).Value = Monetaryid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_FormtypeId", SqlDbType.Int).Value = Formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_UnitId", SqlDbType.Int).Value = Unitid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
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
    public DataSet CompanyAmendment_MonetaryLimit_FetchZonalHead_Dataset(int UserId, int Roleid, int deptid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_ZonalHead_Amendment_FetchFor_BHZHO", DBManager.ConnectToDB());
            //MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Monetary_ZonalHead_Fetch_All_Modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
            MyAdp.SelectCommand.Parameters.Add("@RoleId", SqlDbType.Int).Value = Roleid;
            MyAdp.SelectCommand.Parameters.Add("@DeptId", SqlDbType.Int).Value = deptid;

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
    // public DataSet Company_MonetaryLimit_FetchById_Dataset(int monetary)
    //USE SAME FUNCTION AS IN clsCOMPANY


    #endregion

    #region Gaurav

    public SqlDataReader CompanyAmendment_FetchByCompanyId_Step1(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public SqlDataReader CompanyAmenment_FetchByCompanyId_Step1_Grid(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmenment_FetchByCompanyId_Step1_Grid", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public DataTable Fetch_Max_Factory()
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

        int i = 1;
        while (i <= Status)
        {
            dt.Rows.Add("", "", 0, 0, "", "", "", 0);
            i = i + 1;

        }
        return dt;
    }

    #endregion

    // Anupam Sir changes
    public DataTable Agency_Fetch_DatetobeIssued(int id, int AgencyId, int BranchId)
    {
        try
        {
            MyDt = new DataTable();
            if (id == 1)
            {
                MyAdp = new SqlDataAdapter("GPsp_Agency_Fetch_DatetobeIssued", DBManager.ConnectToDB());
            }
            else if (id == 3)
            {
                MyAdp = new SqlDataAdapter("GPsp_AgencyAmendment_Fetch_DatetobeIssued", DBManager.ConnectToDB());
            }
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@AgencyId", SqlDbType.Int).Value = AgencyId;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyAdp.SelectCommand.Parameters.Add("@Formtypeid", SqlDbType.Int).Value = id;
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


    public DataTable Company_Fetch_StoreByCompanyId(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Fetch_StoreByCompanyId", DBManager.ConnectToDB());
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
    // Amedment Certificate---------------------------------------- Bind Address

    public DataSet Company_AmmedMent_CertificateForm_Dataset_AddressFactory(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPtblCertificate_Amedment_Address", DBManager.ConnectToDB());
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


    public DataSet Fetch_Amedment_StoreReport_Modified(int UnitID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_AmdmentReport_Store_Modified", DBManager.ConnectToDB());
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

    public DataSet Fetch_Amedment_StoreReport_Modified_Count(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_AmdmentReport_Store_Modified_Count", DBManager.ConnectToDB());
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

    public DataSet Company_AmedmentReport_Dataset(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPtblAmdment_Report", DBManager.ConnectToDB());
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



    public DataSet Company_AmedmentReport_PartnerName(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Amendment_Director_Name", DBManager.ConnectToDB());
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

    public DataSet Company_AmedmentReport_MonoteryLimit(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsP_Fetch_MonetoryLimit_AmdementReport", DBManager.ConnectToDB());
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

    public DataSet Company_ReportView_TUrnOver_Amdment_Report(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_TurnOver_Details_modified_For_Amdment", DBManager.ConnectToDB());
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

    /* Code added by Ravish on 11 July 2012 */

    public SqlDataReader Company_Fetch_UnconfirmedTransactionAmedment_ByPaymentid(int Paymentid)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_UnconfirmedPaymentAmedment_FetchByPaymentid", DBManager.ConnectToDB());
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

    public DataSet CompanyRenewal_Waiting_BranchHead_Dataset(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Wating_Branch_Head", DBManager.ConnectToDB());
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

    public int CompanyAmendment_Delete_Step3_Store(int fGPtbl_CUnitAmendment_Store_Id)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Delete_Store_Step3", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitAmendment_Store_Id", SqlDbType.Int).Value = fGPtbl_CUnitAmendment_Store_Id;
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

    public DataTable CompanyAmendment_Fetch_StoreByCompanyId_Inspectionpassed(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Fetch_StoreByCompanyId_Inspectionpassed_RA", DBManager.ConnectToDB());
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

    public DataTable Company_ProfileHscodeAmendment(int companyid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("Gp_sp_fetch_companyprofilehscode", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
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




    public int CompanyRenewal_Insert_Step1_RA_CFSS_Step1(int insertupdateflag, string pan, string compname, string compadd, int stateid, int city,
                      string compzip, string compphone,string commobno,   string compemail,   
                       int userid, int companyid, string amdrenewal,   string proposaldate,  
                        string AadharNumber )
    {
        try
        {
            if (amdrenewal == "renewal_RA")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step1_RA_Rennewal_Amendment_Online_CFSS", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step1_RA_CFSS", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;



            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = insertupdateflag;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar, 500).Value = pan;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 100).Value = compname;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 100).Value = compadd;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = stateid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = city;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 50).Value = compzip;
            
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 50).Value = compphone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = commobno;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 50).Value = compemail;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

            if (amdrenewal == "renewal_RA")
            {
                MySqlComm.Parameters.Add("@fGPtbl_Company_ProposalDate", SqlDbType.VarChar).Value = proposaldate;
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;
            }



            if (amdrenewal == "renewal")
            {
                MySqlComm.Parameters.Add("@Proposaldate", SqlDbType.VarChar).Value = proposaldate; 
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;
            }


            SqlParameter Outputval = new SqlParameter("@ErrorId", SqlDbType.Int);
            Outputval.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(Outputval);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(Outputval.Value);
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

    public void CompanyAmendment_InsertUpdateFactory_step1_CFSS(int insertupdateflag, string factaddress, string factpin, int factstateid, int factcity,
                       string factphone, string factfax, string factemail, int catid, int castid, int sizeid, int unitid, int companyid, int userid, int branchid, string amdrenewal, int natureOfOperation)
    {
        try
        {
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_InserFactory_Step1_RA", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_InserFactory_Step1_RA_CFSS", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = insertupdateflag;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 500).Value = factaddress;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = factpin;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = factstateid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = factcity;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 50).Value = factphone;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Fax", SqlDbType.VarChar, 50).Value = factfax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Email", SqlDbType.VarChar, 50).Value = factemail;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = catid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = sizeid;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Enterprise", SqlDbType.Int).Value = natureOfOperation;
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


}
