using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data;


public partial class appAdministrator_printpagecount : System.Web.UI.Page
{
    clsCompany objcompany;
    DataSet ds ;
    int ddlselectcrieteria;
    string textsearch = "";
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

        if (!IsPostBack)
        {
            bindSearchDetails();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bindSearchDetails();
    }
    protected void grd_view_pageindexchanging(object sender, GridViewPageEventArgs e)
    {

        grd_printcount_certificate.PageIndex = e.NewPageIndex;
        bindSearchDetails();
    }
    protected void grd_view_onrowdatabound(object sender, GridViewRowEventArgs e)
    {

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label CompanyID = (Label)e.Row.FindControl("lbl_comapny_id");
                Label lbl_Company_Name = (Label)e.Row.FindControl("lbl_company_name");
                int companyid = Convert.ToInt32(CompanyID.Text.ToString());
                string CompanyName = lbl_Company_Name.Text.Trim().ToString();
                string Page1 = "";
                string imageid = string.Empty;
                imageid = "CompanyDetails.aspx?cid=" + companyid + "&cname=" + CompanyName + "&page1=" + Page1;
                lbl_Company_Name.Text = "<a href='" + imageid + "' >" + lbl_Company_Name.Text.Trim().ToString() + "</a>";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }

    public void bindSearchDetails()
    {
        try
        {
            if (ddlSearchCriteria.SelectedValue == "1")
            {
                ddlselectcrieteria=Convert.ToInt32("1");
            }
            else if (ddlSearchCriteria.SelectedValue == "2")
            {
                ddlselectcrieteria = Convert.ToInt32("2");
            }
            if (txtSearchbox.Text == "")
            {
                textsearch = "";
            }
            else
            {
                textsearch = Convert.ToString(txtSearchbox.Text.Trim().ToString());
            }
            objcompany = new clsCompany();
            ds = new DataSet();
            ds = objcompany.Fetch_printcountCertificate(ddlselectcrieteria, textsearch);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_printcount_certificate.DataSource = ds.Tables[0];
                grd_printcount_certificate.DataBind();
            }
            else
            {
                grd_printcount_certificate.Dispose();
                grd_printcount_certificate.DataBind();
                ds.Dispose();
                ltlMessage.Text = "No Record Found!!!!";
            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());   
        }
    }


}
