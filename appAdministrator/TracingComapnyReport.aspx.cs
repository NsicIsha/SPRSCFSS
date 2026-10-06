using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_TracingComapnyReport : System.Web.UI.Page
{

    clsCompany objclscompany;
    DataSet ds = new DataSet();
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BindTracing_Comapny();

        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void BindTracing_Comapny()
    {
        try
        {
            ds = new DataSet();
            objclscompany = new clsCompany();
            ds = objclscompany.Bind_tracecompany();

            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_trace_company.DataSource = ds;
                grd_trace_company.DataBind();
            }
            else
            {
                ds.Dispose();
                grd_trace_company.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grdtrace_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        grd_trace_company.PageIndex = e.NewPageIndex;
       // BindTracing_Comapny();
        SearchGridviewBind();
    }
    protected void grdtrace_on_Rowdatabound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void SearchGridviewBind()
    {
        string SearchCriteria = string.Empty;
        SearchCriteria = txtSearchbox.Text.Trim();
        objclscompany = new clsCompany();
       // grd_trace_company.DataSource = objclscompany.Company_trace_Company_Search( Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria);
        grd_trace_company.DataSource = objclscompany.Company_trace_Company_Search_modified(Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria);
        grd_trace_company.DataBind();

        if (grd_trace_company.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
        else
        {
            ltlMessage.Text = " ";
        }
    }
}
