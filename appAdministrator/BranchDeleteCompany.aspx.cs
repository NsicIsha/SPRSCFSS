using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_BranchDeleteCompany : System.Web.UI.Page
{
    int RoleId, UserId, Branch_ID, Zone_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();           
            if ( RoleId == 1 || RoleId==8 || RoleId==10)
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
    private void Bind_Gridview()
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        int BranchId = 0;
        BranchId = Convert.ToInt32(Branch_ID);
        ds = objcom.ExpiredList_user_Delete(txtSearchbox.Text.Trim(), ddlSearchCriteria.SelectedValue, BranchId);
        if (ds != null)
        {
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_company2.DataSource = ds;
                    gv_company2.DataBind();
                    btn_delete.Visible = false;
                }
                else
                {
                    gv_company2.DataSource = null;
                    gv_company2.DataBind();
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
        gv_company2.PageIndex = e.NewPageIndex;
        Bind_Gridview();
    }
    protected void btn_delete_Click(object sender, EventArgs e)
    {
       /* string str = "";
        foreach (GridViewRow gvrow in gv_company.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chk_select");
            if (chk != null & chk.Checked)
            {
                str += gv_company.DataKeys[gvrow.RowIndex].Value.ToString() + ',';
            }
        }*/
        // ltlMessage.Text = str.TrimEnd(',');

       /* clsCompany objcom = new clsCompany();
        int errorid = objcom.delete_Incomplete_Companies_Branch(str.ToString(), Branch_ID, Zone_ID, "");
        ltlMessage.Text = errorid.ToString();
        if (errorid == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            Bind_Gridview();
        }*/
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
    }
    //protected void gv_company_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
       
    //}
    protected void btnSubmitpan_Click(object sender, EventArgs e)
    {
       // string companyid = gv_company.DataKeys[e.RowIndex].Value.ToString();

        int company_id = Convert.ToInt32(lbl_company_id_onpnl.Value);
        string Reson = "";
        Reson = txt_reson_delete.Text.ToString();
        clsCompany objcom = new clsCompany();
        int errorid = objcom.delete_Incomplete_Companies_Branch(Convert.ToString(company_id), Branch_ID, Zone_ID, Reson);
        if (errorid == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Selected companies  deleted....  ');", true);
            gv_company2.PageIndex = 0;
            Bind_Gridview();


        }
    }
    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper().Trim().ToString() == "DELETE")  //delete
            {
                lblmsg.Text = string.Empty;
                hid_comapnyId.Value = e.CommandArgument.ToString();
                txt_reson_delete.Text = "";

                lbl_company_id_onpnl.Value = hid_comapnyId.Value;
                // Bindtexbox(Convert.ToInt32(hid_comapnyId.Value));
                ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }


    protected void gv_company2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}

