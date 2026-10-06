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
/// Summary description for clsMasterApplication
/// </summary>
public class clsMasterApplication
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;

    public DataSet MasterApplication_Fetch_DataSet()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterApplication_All", DBManager.ConnectToDB());
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

    public DataSet MasterApplication_Fetch_DataSet(int ApplicationId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterApplication_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MApl_Id", SqlDbType.Int).Value = ApplicationId;
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

    public int MasterApplication_DeleteById(int ApplicationId, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Delete_MasterApplication_ById", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MApl_Id", SqlDbType.Int).Value = ApplicationId;
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

    public int MasterApplication_InsertUpdate(int ApplicationId, string fGPtbl_MApl_Name, string fGPtbl_MApl_PageName, int fGPtbl_MApl_levelId, int fGPtbl_MApl_MasterId, string fGPtbl_MApl_Path, int fGPtbl_MApl_Link, int fGPtbl_MApl_LinkType, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterApplication", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_MApl_Id", SqlDbType.Int).Value = ApplicationId;
            if (fGPtbl_MApl_Name.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MApl_Name", SqlDbType.VarChar, 200).Value = fGPtbl_MApl_Name;
            if (fGPtbl_MApl_PageName.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MApl_PageName", SqlDbType.VarChar, 200).Value = fGPtbl_MApl_PageName;
            MySqlComm.Parameters.Add("@fGPtbl_MApl_levelId", SqlDbType.Int).Value = fGPtbl_MApl_levelId;
            MySqlComm.Parameters.Add("@fGPtbl_MApl_MasterId", SqlDbType.Int).Value = fGPtbl_MApl_MasterId;
            if (fGPtbl_MApl_Path.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_MApl_Path", SqlDbType.VarChar, 2000).Value = fGPtbl_MApl_Path;
            MySqlComm.Parameters.Add("@fGPtbl_MApl_Link", SqlDbType.TinyInt).Value = fGPtbl_MApl_Link;
            MySqlComm.Parameters.Add("@fGPtbl_MApl_LinkType", SqlDbType.TinyInt).Value = fGPtbl_MApl_LinkType;
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

    //=========UTTAM===========
    public int MasterMonetaryLimit_Update(float branchHead, float zonalHead, float HO, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_MasterMonetaryLimit", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@MonetaryBranchHead", SqlDbType.Float).Value = branchHead;
            MySqlComm.Parameters.Add("@MonetaryZonalHead", SqlDbType.Float).Value = zonalHead;
            MySqlComm.Parameters.Add("@MonetaryHO", SqlDbType.Float).Value = HO;

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
    //=========UTTAM===========

    // Vinay  31-01-2013
    public int MasterMonetaryLimit_Insert_update(int roleid, int desigid, double amt, int userid)
    {
        try
        {
            MySqlComm = new SqlCommand("Gpsp_Insert_update_MasterMonetaryLimit", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@MonetaryAmount", SqlDbType.Float).Value = amt;
            MySqlComm.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleid;
            MySqlComm.Parameters.Add("@DESIGID", SqlDbType.Int).Value = desigid;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

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

    public DataTable Fetch_Userrole()
    {
        DataTable dt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterRole_BranchZonalHead", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(dt);
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
        return dt;
    }

    public DataTable Fetch_Monetarylimit()
    {
        DataTable dt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Monetary_limit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.Fill(dt);

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
        return dt;
    }

    public DataTable Fetch_Monetarylimit_byID(int mid)
    {
        DataTable dt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MonetaryLimit_ById", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Id", SqlDbType.Int).Value = mid;
            MyAdp.Fill(dt);
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
        return dt;
    }

    public bool IsValidmONETARY_iNPUT(int mroleid, int mdesgid)
    {

        bool ret = false;



        try
        {

            MySqlComm = new SqlCommand("GPsp_Update_MasterApplication", DBManager.ConnectToDB());

            MySqlComm.CommandType = System.Data.CommandType.StoredProcedure;

            MySqlComm.CommandText = "GPsp_Valid_Monetary";

            MySqlComm.Parameters.AddWithValue("@RoleId", mroleid);

            MySqlComm.Parameters.AddWithValue("@DesgId", mdesgid);

            SqlDataReader dr = MySqlComm.ExecuteReader();

            if (dr.Read())
            {

                ret = dr.GetBoolean(0);

            }

        }

        catch (Exception EX)
        {

            ErrHandler.WriteError(EX.Message);
        }
        finally
        {
            DBManager.CloseConnection();

        }

        return ret;

    }

    public static void MergeGridRow(GridView gridViewStore)
    {
        string rwtext = string.Empty;
        for(int rowIndex = gridViewStore.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        //for (int rowIndex = 0; rowIndex < gridViewStore.Rows.Count - 1; rowIndex++)
        {
            GridViewRow row = gridViewStore.Rows[rowIndex];
            GridViewRow nextrow = gridViewStore.Rows[rowIndex + 1];

            for (int i = 3; i < row.Cells.Count; i++)
            {
                rwtext = row.Cells[i].Text;
                if (nextrow.Cells[i].Text == "0")
                {
                    row.Cells[i].RowSpan = row.Cells[i].RowSpan < 2 ? 2 :
                                           row.Cells[i].RowSpan + 1;
                    nextrow.Cells[i].Visible = false;
                }
            }
        }
    }

    //TO FETCH ALL COMPANIES FOR BLACK LIST GRIDVIEW
    public DataSet Fetch_Blacklistcompany_Dataset(string companyId, string companystatus, string companyname,
        string companypanno, string companyreferenceno, string companyregistrationno)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_BlacklistCompanies", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@companyId", SqlDbType.VarChar, 50).Value = companyId;
            MyAdp.SelectCommand.Parameters.Add("@companystatus", SqlDbType.VarChar, 50).Value = companystatus;
            MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 500).Value = companyname;
            MyAdp.SelectCommand.Parameters.Add("@companypanno", SqlDbType.VarChar, 10).Value = companypanno;
            MyAdp.SelectCommand.Parameters.Add("@companyreferenceno", SqlDbType.VarChar, 50).Value = companyreferenceno;
            MyAdp.SelectCommand.Parameters.Add("@companyregistrationnumber", SqlDbType.VarChar, 50).Value = companyregistrationno;
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

    public int RevertBack_Blacklistcomapny(int companyid)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Revert_balcklistCompany", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

            SqlParameter outVal = new SqlParameter("@out", SqlDbType.Int);
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

    public int UpdateBlacklistcompany(int companyid, string authortiytext, string IomCertificateno, string blacklistreason, int previousstatus)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Blacklist_Company_Update", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@AuthorityText", SqlDbType.VarChar, 500).Value = authortiytext;
            MySqlComm.Parameters.Add("@IOMCertificateno", SqlDbType.VarChar, 50).Value = IomCertificateno;
            MySqlComm.Parameters.Add("@Blacklistreason", SqlDbType.VarChar, 500).Value = blacklistreason;
            MySqlComm.Parameters.Add("@Previousstatus", SqlDbType.Int).Value = previousstatus;

            SqlParameter outVal = new SqlParameter("@out", SqlDbType.Int);
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
