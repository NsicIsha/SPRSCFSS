using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Admin_delete_Incomplete_companies : System.Web.UI.Page
{
    int RoleId, UserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
           // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
          //  UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

          //  RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
          //  UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
                       
            if ( RoleId != 1|| RoleId!=10)
            {
                if (!Page.IsPostBack)
                {
                  Bind_Gridview();
                }             
            }
            else
            {
                Bind_Gridview();
                // btnSearch.Visible = false;
               // Response.Redirect("logout.aspx");
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
}
