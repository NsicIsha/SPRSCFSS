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


/// <summary>
/// Summary description for clsDDL
/// </summary>
public class clsDDL
{
    static SqlDataAdapter MyAdp;

	public clsDDL()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable ddlZone()
    {
        //Text = fGPtbl_MZ_Name
        //ID = fGPtbl_MZ_Id
        clsMasterZone obj = new clsMasterZone();
        DataTable dt = new DataTable();
        dt = obj.MasterZone_Fetch_DataSet().Tables[0];

        return dt;
    }

    public static DataTable ddlZone_Activated()
    {
        //Text = fGPtbl_MZ_Name
        //ID = fGPtbl_MZ_Id
        clsMasterZone obj = new clsMasterZone();
        DataTable dt = new DataTable();
        dt = obj.MasterZone_FetchActivated_DataSet().Tables[0];

        return dt;
    }

    public static DataTable ddlBranch(int ZoneId)
    {
        //Text = fGPtbl_MB_Name
        //ID = fGPtbl_MB_Id
        clsMasterBranch obj = new clsMasterBranch();
        DataTable dt = new DataTable();
        if (ZoneId == 0)
        {
            dt = obj.MasterBranch_Fetch_DataSet().Tables[0];
        }
        else
        {
            dt = obj.MasterBranch_FetchByZoneId_DataSet(ZoneId).Tables[0];
        }

        return dt;
    }

    public static DataTable ddlBranch_Activated(int ZoneId)
    {
        //Text = fGPtbl_MB_Name
        //ID = fGPtbl_MB_Id
        clsMasterBranch obj = new clsMasterBranch();
        DataTable dt = new DataTable();
        if (ZoneId == 0)
        {
            dt = obj.MasterBranch_FetchActivated_DataSet().Tables[0];
        }
        else
        {
            dt = obj.MasterBranch_FetchByZoneIdActivated_DataSet(ZoneId).Tables[0];
        }

        return dt;
    }

    public static DataTable ddlState()
    {
        //Text = fGPtbl_MS_Name
        //ID = fGPtbl_MS_Id
        clsMasterState obj = new clsMasterState();
        DataTable dt = new DataTable();
        dt = obj.MasterState_Fetch_DataSet().Tables[0];

        return dt;
    }

    public static DataTable ddlState_Activated()
    {
        //Text = fGPtbl_MS_Name
        //ID = fGPtbl_MS_Id
        clsMasterState obj = new clsMasterState();
        DataTable dt = new DataTable();
        dt = obj.MasterState_FetchActivated_DataSet().Tables[0];

        return dt;
    }

    public static DataTable ddlCity(int StateId)
    {
        //Text = fGPtbl_MCty_Name
        //ID = fGPtbl_MCty_Id
        clsMasterCity obj = new clsMasterCity();
        DataTable dt = new DataTable();
        if (StateId == 0)
        {
            dt = obj.MasterCity_Fetch_DataSet().Tables[0];
        }
        else
        {
            dt = obj.MasterCity_FetchByStateId_DataSet(StateId).Tables[0];
        }

        return dt;
    }

    public static DataTable ddlCity_Activated(int StateId)
    {
        //Text = fGPtbl_MCty_Name
        //ID = fGPtbl_MCty_Id
        clsMasterCity obj = new clsMasterCity();
        DataTable dt = new DataTable();
        if (StateId == 0)
        {
            dt = obj.MasterCity_FetchActivated_DataSet().Tables[0];
        }
        else
        {
            dt = obj.MasterCity_FetchByStateIdActivated_DataSet(StateId).Tables[0];
        }

        return dt;
    }

    public static DataTable ddlRole()
    {
        //Text = fGPtbl_MR_Name
        //ID = fGPtbl_MR_Id
        clsMasterRole obj = new clsMasterRole();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.MasterRole_Fetch_DataSet().Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    public static DataTable ddlRoleExceptAdmin()
    {
        //Text = fGPtbl_MR_Name
        //ID = fGPtbl_MR_Id
        clsMasterRole obj = new clsMasterRole();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.MasterRole_FetchExceptAdmin_DataSet().Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    public static DataTable ddlDepartment()
    {
        //Text = fGPtbl_MD_Name
        //ID = fGPtbl_MD_Id
        clsMasterDepartment obj = new clsMasterDepartment();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.MasterDepartment_Fetch_DataSet().Tables[0];
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    public static DataTable ddlCategoryArea()
    {
        //Text = fGPtbl_CUnit_CA_Name
        //ID = fGPtbl_CUnit_CA_Id
        clsCompany obj = new clsCompany();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.Company_Fetch_CategoryArea();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }
    public static DataTable ddlInspectionAgency()
    {
        //fGPtbl_MIA_Id,
        //fGPtbl_MIA_Name
        clsCompany obj = new clsCompany();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.Fetch_InspectionAgency();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }
    public static DataTable ddlCategoryCast()
    {
        //Text = fGPtbl_CUnit_CC_Name
        //ID = fGPtbl_CUnit_CC_Id
        clsCompany obj = new clsCompany();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.Company_Fetch_CategoryCast();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    public static DataTable ddlCategoryUnitSize()
    {
        //Text = fGPtbl_CUnit_CUS_Name
        //ID = fGPtbl_CUnit_CUS_Id
        clsCompany obj = new clsCompany();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.Company_Fetch_CategoryUnitSize();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;
    }

    /* Code added by Ravish on 17 April 2012*/

    // Bind ZoneID Throgh Pass RolId
    public static DataTable ddlBranchReport(int RoleID)  // This is user Id
    {
        //Text = fGPtbl_MB_Name
        //ID = fGPtbl_MB_Id
        clsMasterBranch obj = new clsMasterBranch();
        DataTable dt = new DataTable();
        if (RoleID != 0)
        {
            dt = obj.MasterZone_Fetch_Dataset_Report(RoleID).Tables[0];
        }
        else
        {

        }

        return dt;
    }
    // created By Ravish Kumar Pandey

    public static DataTable ddlBranch_Activated_user_specific(int UserId, int ZoneID)
    {
        //Text = fGPtbl_MB_Name
        //ID = fGPtbl_MB_Id
        clsMasterBranch obj = new clsMasterBranch();
        DataTable dt = new DataTable();

        dt = obj.MasterBranch_Fetch_Dataset_Report(UserId, ZoneID).Tables[0];


        return dt;
    }


    public static DataTable ddlHscode()
    {
        //Text = fGPtbl_MB_Name
        //ID = fGPtbl_MB_Id
        clsCompany obj = new clsCompany();
        DataTable dt = new DataTable();
        try
        {
            dt = obj.Company_Fetch_HscodeAll();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            dt.Dispose();
        }
        return dt;

    }

    public static DataSet Allfieldsbyrows_Fetch_Dataset(int companyid)
    {
        DataSet MyDs = new DataSet();

        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Fetch_Readonlydata_ByCompanyId", DBManager.ConnectToDB());
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


    public static DataSet UnavailableData_Fetch_ByCompanyId_Dataset(int companyid)
    {
        DataSet MyDs = new DataSet();
        try
        {
        MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_UnavailableData_ByCompanyId", DBManager.ConnectToDB());
        MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value=companyid;
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



    public static void Insert_MailDetails(string MailTo, int ToUserId, int ToUserTable, string MailFrom,
        int FromUserId, int FromUserTable, string MailSubject, string MailBody, int CreateUserId)
    {
        SqlCommand MyCmd = new SqlCommand("GPsp_Update_SentMailer", DBManager.ConnectToDB());
        try
        {
            MyCmd.CommandType = CommandType.StoredProcedure;

            MyCmd.Parameters.Add("@fGPtbl_SM_To", SqlDbType.VarChar, 2000).Value = MailTo;
            MyCmd.Parameters.Add("@fGPtbl_SM_ToUserId", SqlDbType.Int).Value = ToUserId;
            MyCmd.Parameters.Add("@fGPtbl_SM_ToUserTable", SqlDbType.Int).Value = ToUserTable;
            MyCmd.Parameters.Add("@fGPtbl_SM_From", SqlDbType.VarChar, 2000).Value = MailFrom;
            MyCmd.Parameters.Add("@fGPtbl_SM_FromUserId", SqlDbType.Int).Value = FromUserId;
            MyCmd.Parameters.Add("@fGPtbl_SM_FromUserTable", SqlDbType.Int).Value = FromUserTable;
            MyCmd.Parameters.Add("@fGPtbl_SM_Subject", SqlDbType.VarChar, 2000).Value = MailSubject;
            MyCmd.Parameters.Add("@fGPtbl_SM_Body", SqlDbType.Text).Value = MailBody;
            MyCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = CreateUserId;

            MyCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyCmd.Dispose();
            DBManager.CloseConnection();
        }
    }

}
