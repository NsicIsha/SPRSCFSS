using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Admin_Edit_Search_Online : System.Web.UI.Page
{
    int RoleId, UserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            if (RoleId == 8)
            {
                btnSearch.Visible = true;
            }
            else
            {
                // btnSearch.Visible = false;
                Response.Redirect("logout.aspx");
            }
            if (!Page.IsPostBack)
            {
                //BindGridview();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        BindGridview();
    }

    private void BindGridview()
    {
        try
        {
            clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
            DataSet ds = new DataSet();
            //int processid = Convert.ToInt32(ddlSearchCriteria.SelectedValue);
            int processid = 1;
            string searchtext = txtSearchbox.Text.Trim();
            ds = objcom.Company_Details(processid, searchtext, UserId);
            /*if (ds.Tables[0].Rows.Count>0)
            {*/
            gv_companies.DataSource = ds;
            gv_companies.DataBind();

            //}
            //else
            //{
            //    gv_companies.DataSource = null;
            //    gv_companies.DataBind();
            //    gv_companies.EmptyDataText = 
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_companies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "EDITSTORE")
            {
                //      GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //      LinkButton lnkeditstore =(LinkButton) row.FindControl("lnkeditstore");
                //      string uid = ddlSearchCriteria.SelectedValue;
                ////      "Edit_company_store.aspx?id="+txtUserId.Text+"&UserName="+txtUserName.Text
                //     // lnkeditstore.PostBackUrl = "Edit_company_store.aspx?id="+e.CommandArgument+"&urlid="+ddlSearchCriteria.SelectedValue;
                // //    Server.Transfer("Edit_company_store.aspx?id="+e.CommandArgument+"&urlid="+ddlSearchCriteria.SelectedValue");
            }
            if (e.CommandName.ToUpper() == "EDITMONLIMIT")
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_companies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkeditstore = (LinkButton)e.Row.FindControl("lnkeditstore");
                LinkButton lnkeditmolmt = (LinkButton)e.Row.FindControl("lnkeditmonlmt");
                LinkButton lnkeditcomid = (LinkButton)e.Row.FindControl("lnkeditcompany");
                HiddenField hdnprintaccess = (HiddenField)e.Row.FindControl("hdnPrintAccess");
                //HiddenField hdnadminaccept = (HiddenField)e.Row.FindControl("hdnAdminAccept");
                string hdnprintstr = hdnprintaccess.Value;
                //string hdnadacceptstr = hdnadminaccept.Value;
                /*  if (ddlSearchCriteria.SelectedValue == "2")
                  {
                      lnkeditstore.Enabled = false;
                  }
                  else
                  {
                      lnkeditstore.Enabled = true;

                      lnkeditstore.PostBackUrl = "Admin_Edit_Store.aspx?id=" + lnkeditstore.CommandArgument + "&urlid=" + ddlSearchCriteria.SelectedValue;
                  }*/

                if (hdnprintaccess.Value != "1")
                {
                    lnkeditstore.Enabled = false;
                    lnkeditmolmt.Enabled = false;
                    lnkeditcomid.Enabled = false;
                }
                lnkeditstore.PostBackUrl = "Admin_Edit_Store.aspx?id=" + lnkeditstore.CommandArgument + "&urlid=1";
                lnkeditcomid.PostBackUrl = "AdminEditRegistration.aspx?cid=" + lnkeditcomid.CommandArgument;
                //lnkeditmolmt.PostBackUrl = "Admin_Edit_monetary.aspx?id=" + lnkeditmolmt.CommandArgument + "&urlid=1";
                clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
                DataTable dt = new DataTable();
                dt = objcom.Fetch_Edit_monetarylimit(Convert.ToInt32(lnkeditmolmt.CommandArgument.ToString()), 1);
                if (dt.Rows.Count > 1)
                {
                    lnkeditmolmt.Text = "Edit";
                    lnkeditmolmt.Enabled = true;
                }
                else
                {
                    lnkeditmolmt.Text = "Monetary Data not available";
                    lnkeditmolmt.Enabled = false;
                }
                if (hdnprintaccess.Value != "1")
                {
                    lnkeditmolmt.Enabled = false;
                }
                else
                {
                    lnkeditmolmt.Enabled = true;
                }
                lnkeditmolmt.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + lnkeditmolmt.CommandArgument + "&urlid=1" + "&edit=E";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_companies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_companies.PageIndex = e.NewPageIndex;
        BindGridview();
    }

}