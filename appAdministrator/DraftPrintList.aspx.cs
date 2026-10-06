using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
  

public partial class appAdministrator_DraftPrintList : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);


            if (RoleId == 8)
            {
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                    GridViewDataBind();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
    }
    public void GridViewDataBind()
    {
        try
        {
            objCompany = new clsCompany();
            gvCertificateDetails.DataSource = objCompany.Company_Fetch_CertificateAll_fetchDraftPrint(Branchid);
            gvCertificateDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (gvCertificateDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
    }

    protected void SearchGridviewBind()
    {
        string SearchCriteria = string.Empty;
        SearchCriteria = txtSearchbox.Text.Trim();
        objCompany = new clsCompany();
        gvCertificateDetails.DataSource = objCompany.Company_Fetch_Certificate_Search_DraftPrint(Branchid, Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria);
        gvCertificateDetails.DataBind();

        if (gvCertificateDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
        else
        {
            ltlMessage.Text = " ";
        }
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
    protected void CertificateListPageindexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCertificateDetails.PageIndex = e.NewPageIndex;
            if (txtSearchbox.Text.Trim() != "")
            {
                SearchGridviewBind();
            }
            else
            {
                GridViewDataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void gvCertificateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {



    }
    protected void gvCertificateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnPrint = (LinkButton)e.Row.FindControl("lbnPrint");
                HiddenField hdncompId = (HiddenField)e.Row.FindControl("HiddenFieldCompanyId");
                string imageid = string.Empty;
                imageid = "CompanyCertificatePrintPreviewPage.aspx?id=" + hdncompId.Value;
                btnPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
               //  imageid=

            }
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
}
