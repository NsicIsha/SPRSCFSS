using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Summary description for clsCompanyBacklog
/// </summary>
public class clsCompanyBacklog
{

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

	public clsCompanyBacklog()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Hashtable Company_Insert_Step1(string fGPtbl_Company_GPReferenceNo, string fGPtbl_Company_Name, string fGPtbl_Company_PAN,
      string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId,
      int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax,
      string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address, string fGPtbl_CUnit_1B_Factory_Zip,
      int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax,
      string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
      int fGPtbl_CUnit_BranchId, string fGPtbl_CUnitBranch_Address, string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId,
      string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax,
      string fGPtbl_CUnitBranch_Email, int UserId, int provision, int enterprisedefnition
      , string mobno)
    {
        int UnitId = 0;
        int CompanyId = 0;


        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step1_Backlog", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_GPReferenceNo", SqlDbType.VarChar, 500).Value = fGPtbl_Company_GPReferenceNo;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.Char, 10).Value = fGPtbl_Company_PAN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = mobno;

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

            //FOR PROVISIONAL COMPANY
            MySqlComm.Parameters.Add("@fGPtbl_Company_provision", SqlDbType.Int).Value = provision;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;

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


    public int Company_Update_Step1(string fGPtbl_Company_Name, string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId,
          int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax, string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address,
          string fGPtbl_CUnit_1B_Factory_Zip, int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax,
          string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId, string fGPtbl_CUnitBranch_Address,
          string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId, string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax, string fGPtbl_CUnitBranch_Email,
          int UserId, int CompanyId, int UnitId, int provision, int enterprisedefnition, string companymob)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Step1_Regi_Backlog", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            if (fGPtbl_Company_HeadOffice_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Email;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_1B_Factory_Address;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_1B_Factory_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = companymob;
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


    public int Company_Director_insert(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_directordata", con);
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

    public void Store_Insert_Modified(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int userid,string str,string companyID)
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

            //MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified_Backlog", DBManager.ConnectToDB());
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified_Backlog_27feb", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
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

    public DataTable Company_Fetch_StoreByCompanyId(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_StoreByCompanyId_Backlog", DBManager.ConnectToDB());
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

    //  BacklogUnit rEGISTRATION TrunOver & Monetary Limit

    public int BacklogUnit_insert(string xmldata, double basicfee, double servicetax, double total, string registrationdate, string referenceno, string registrationno)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("BacklogUnit_sp_Insert_Trunover_Monetarylimit", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);

            cmd.Parameters.AddWithValue("@Basicfee", basicfee);
            cmd.Parameters.AddWithValue("@ServiceTax", servicetax);
            cmd.Parameters.AddWithValue("@Total", total);

            cmd.Parameters.AddWithValue("@RegistrationDate", registrationdate);
            cmd.Parameters.AddWithValue("@ReferenceNo", referenceno);
            cmd.Parameters.AddWithValue("@Registrationno", registrationno);


            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;

            cmd.Parameters.Add(outError);

            cmd.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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


    public DataSet Company_Fetch_ByUnit_Step4(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchByCompanyId_Step4_Backlog", DBManager.ConnectToDB());
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


    public DataTable Company_Fetch_StoreHSCode(string productdescription,string type)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GP_sp_searchHSCode", DBManager.ConnectToDB());
           
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@Product", SqlDbType.VarChar,5000).Value = productdescription == ""?null:productdescription;
            MyAdp.SelectCommand.Parameters.Add("@tppe", SqlDbType.Int).Value = Convert.ToInt32(type);
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


}
