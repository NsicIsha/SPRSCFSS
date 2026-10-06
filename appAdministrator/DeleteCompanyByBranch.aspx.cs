using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_DeleteCompanyByBranch : System.Web.UI.Page
{
    int RoleId, UserId, Branch_ID, Zone_ID;
    SqlCommand MySqlComm;
    int ErrorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId == 1 || RoleId == 8 || RoleId == 10)
            {
                Branch_ID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                Zone_ID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
                if (!Page.IsPostBack)
                {
                    Bind_Gridview();
                }

            }
            else
            {
                // Bind_Gridview();
                // btnSearch.Visible = false;
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }

    }
    public DataSet ExpiredList_user_Delete(string searchterm, string critiera, int BranchId)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp;
        try
        {
            MyAdp = new SqlDataAdapter("GP_Sp_IncompleteCompanies_Branch", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchId != 0) MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = BranchId;

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
      //      MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    private void Bind_Gridview()
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        int BranchId = 0;
        BranchId = Convert.ToInt32(Branch_ID);
        ds = ExpiredList_user_Delete(txtSearchbox.Text.Trim(), ddlSearchCriteria.SelectedValue, BranchId);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_company.DataSource = ds;
                    gv_company.DataBind();
                   
                }
                else
                {
                    gv_company.DataSource = null;
                    gv_company.DataBind();
                  
                }
            }

        }
        else
        {
          
        }
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

    protected void btnSubmitpan_Click(object sender, EventArgs e)
    {
          int company_id =0;
          var k=Request.Form["text1"];
          if (k != null)
          {
              lbl_company_id_onpnl.Value = k;
              company_id = Convert.ToInt32(lbl_company_id_onpnl.Value);
          }
          string dropdown_text = "";
          dropdown_text = ddl_delete_reson.Value.ToString();

        string Reson = "";
        if (dropdown_text.Trim() == "Other")
        {
            Reson = txt_reson_delete.Text.ToString();
        }
        else
        {
            Reson = dropdown_text.Trim().ToString();
        }
        clsCompany objcom = new clsCompany();
        int errorid = delete_Incomplete_Companies_Branch(Convert.ToString(company_id), Branch_ID, Zone_ID, Reson);
        if (errorid == 1)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            gv_company.PageIndex = 0;
            Bind_Gridview();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
    }
    protected void gv_company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        gv_company.PageIndex = e.NewPageIndex;
        Bind_Gridview();
    }
  //  protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
  //  {
  //if (e.CommandName.ToUpper().Trim().ToString() == "EDP")
  // {
  //    //lblmsg.Text = string.Empty;
  //    hid_comapnyId.Value = e.CommandArgument.ToString();
  //     txt_reson_delete.Text = "";

  //     lbl_company_id_onpnl.Value = hid_comapnyId.Value;
  //  //    //    // Bindtexbox(Convert.ToInt32(hid_comapnyId.Value));
  //  //    //    ModalPopupExtender2.Show();
  //      }
  //  }
}
