using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

public partial class appAdministrator_DeleteCompanyByHO : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds, ds;
    int UserId, RoleId, DepartmentId;
    string UserFullName, password;
    int BranchId;
    string Branch_Name = "";
    int ZoneID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (UserId != 231)
            {
               // BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());
                //Branch_Name = objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString();
            }
            else
            {
                Branch_Name = "text";
            }
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 2 || RoleId == 3 || RoleId == 5)
            {

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                   // GridViewDataBind();
                    if (RoleId == 3)
                    {

                    }
                    else
                    {

                    }
                }
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";

        }
    }
    public void GridViewDataBind()
    {
        try
        {
            
            grd_delete_company.DataSource = null; grd_delete_company.DataBind();
           objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objCompany.Company_Fetch_DeleteByAdmin(txtSearchbox.Text.Trim());
          
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    HDCompanyid.Value = dt.Rows[0]["fGPtbl_Company_Id"].ToString();
                    grd_delete_company.DataSource = dt;
                    grd_delete_company.DataBind();
                }
                else
                {
                    dt.Dispose();
                    grd_delete_company.DataBind();
                }
            }            

        }
        catch (Exception ex)
        {

        }
        
    }

    public DataTable dt_bind_grdid()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
            MyAdp = new SqlDataAdapter("GP_Sp_CompanyDeleteHOHead", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            // MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch_Id;


            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
        return dt;




    }

    public void UpdateDeleteCompanyReject(int companyID)
    {
        try
        {
            int k = 0;

            SqlCommand MySqlComm;
            MySqlComm = new SqlCommand("GP_sp_delete_companies_Incomplete_HO_head_companydelete", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Companyid", SqlDbType.Int).Value = companyID;
           // MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
    protected void onrowcommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int k = 0;
            if (e.CommandName.ToString() == "edp")
            {
                k = Convert.ToInt32(e.CommandArgument.ToString());
                Update_companyDeleteforAccept(k);
                GridViewDataBind();
            }

            if (e.CommandName.ToString() == "reject")
            {
                k = Convert.ToInt32(e.CommandArgument.ToString());
                UpdateDeleteCompanyReject(k);
                GridViewDataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }


    //  Update table GPtbl_Company_Delete for Accepted 
    public void Update_companyDeleteforAccept(int companyID)
    {
        int k = 0;
        try
        {

            SqlCommand MySqlComm;
            MySqlComm = new SqlCommand("GP_Sp_UpdateCompanyHOHeadDeleteAccept", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompnyID", SqlDbType.Int).Value = companyID;
            SqlParameter outError = new SqlParameter("@ErrorID", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewDataBind();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        using (SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"]))
        {
            sqlConn.Open();
            using (SqlTransaction transaction = sqlConn.BeginTransaction())
            {
                try
                {
                    SqlCommand command = sqlConn.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GP_companies_deletion_UsingQuery";
                    command.CommandTimeout = 120;

                    command.Parameters.AddWithValue("@Companyid", HDCompanyid.Value);
                    command.Parameters.AddWithValue("@ErrorId", 0);
                    command.Parameters.AddWithValue("@Reason", txt_Reason.Text);

                    command.ExecuteNonQuery();
                    transaction.Commit();
                                        
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
                }
                finally
                {
                    grd_delete_company.DataSource = null; grd_delete_company.DataBind();
                    txt_Reason.Text = string.Empty;
                    txtSearchbox.Text = string.Empty;
                }
            }
        }
    }     
     
    

    protected void lbl_companyname_Command(object sender, CommandEventArgs e)
    {
        ModalPopupExtender1.Show();
    }
}
