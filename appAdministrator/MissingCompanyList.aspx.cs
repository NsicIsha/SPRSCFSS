using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;


public partial class appAdministrator_MissingCompanyList : System.Web.UI.Page
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
         
           // clsTrans_UserBranch objtub = new clsTrans_UserBranch();
      //  Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);


            if (RoleId == 1)
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
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    public void GridViewDataBind()
    {
        try
        {
            objCompany = new clsCompany();
            gvCertificateDetails.DataSource = objCompany.Company_Fetch_Certificate_SearchByAdmin(1,"");
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
  
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        try
        {
            string SearchCriteria = string.Empty;
            SearchCriteria = txtSearchbox.Text.Trim().ToString();
            objCompany = new clsCompany();
            gvCertificateDetails.DataSource = objCompany.Company_Fetch_Certificate_SearchByAdmin(Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria);
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
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    protected void CertificateListPageindexChanging(object sender, GridViewPageEventArgs e)
    {

        gvCertificateDetails.PageIndex = e.NewPageIndex;
        GridViewDataBind();

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
                Label lbl_compan = (Label)e.Row.FindControl("lbl_Company_id");
                LinkButton lnk_view=(LinkButton)e.Row.FindControl("lnkview"); 

                HiddenField hdncompany_id = (HiddenField)e.Row.FindControl("HiddenFieldCompanyId");
                
                string imageid = "";
                   DecryptEncrypt objEncrypt = new DecryptEncrypt();
                   imageid = "CompanyMissingInformation.aspx?Company_ID=" + objEncrypt.Encrypt(hdncompany_id.Value.ToString());
                   lnk_view.PostBackUrl = imageid;
                  //lnk_view.Text="<a href="+imageid+">"+"View" +"</a>";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
}
