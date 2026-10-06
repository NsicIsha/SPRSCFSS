using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_Admin_delete_Incomplete_companies : System.Web.UI.Page
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
            Branch_ID = 0;
            Zone_ID = 0;
            if ( RoleId == 1)
            {
                if (!Page.IsPostBack)
                {
                   //  Bind_Gridview();
                }             
            }
            else
            {
                // btnSearch.Visible = false;
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }
    private void Bind_Gridview()
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        ds = objcom.ExpiredList(txtSearchbox.Text.Trim(),ddlSearchCriteria.SelectedValue);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_company.DataSource = ds;
                    gv_company.DataBind();
                    btn_delete.Visible = false;
                }
                else
                {
                    gv_company.DataSource = null;
                    gv_company.DataBind();
                    btn_delete.Visible = false;
                }
            }            
        }
        else
        {
            btn_delete.Visible = false;
        }
    }
    protected void gv_company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_company.PageIndex = e.NewPageIndex;
        Bind_Gridview();
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
        string str="";
        foreach (GridViewRow gvrow in gv_company.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chk_select");
            if (chk != null & chk.Checked)
            {
                str += gv_company.DataKeys[gvrow.RowIndex].Value.ToString() + ',';
            }
        }
       // ltlMessage.Text = str.TrimEnd(',');

        clsCompany objcom = new clsCompany();
        int errorid = objcom.delete_Incomplete_Companies(str.ToString());
        ltlMessage.Text = errorid.ToString();
        if (errorid == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            Bind_Gridview();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
    }
    protected void gv_company_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string companyid = gv_company.DataKeys[e.RowIndex].Value.ToString();
        clsCompany objcom = new clsCompany();
        int errorid = objcom.delete_Incomplete_Companies(companyid.ToString());
        if (errorid == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            Bind_Gridview();
        }
    }
    public int delete_Incomplete_Companies_Branch(string companyies, int BranchID, int ZoneID, string Reson)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_delete_companies_Incomplete_Branch_By_admin", DBManager.ConnectToDB());
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
     
        int company_id = 0;
        var k = Request.Form["text1"];
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

       // int errorid = objcom.delete_Incomplete_Companies(Convert.ToString(company_id));
        if (errorid == 1)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            gv_company.PageIndex = 0;
            Bind_Gridview();
        }
    }
}
