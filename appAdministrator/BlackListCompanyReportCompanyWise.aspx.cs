using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data;

public partial class appAdministrator_BlackListCompanyReportCompanyWise : System.Web.UI.Page
{

    clsCompany objcompany;
    DataSet ds;
    int ddlselectcrieteria;
    string textsearch = "";
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        FunRequest();

        //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        if (!IsPostBack)
        {
            bindSearchDetails();
        }
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
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

    }
    public void bindSearchDetails()
    {
        try
        {
            if (ddlSearchCriteria.SelectedValue == "1")
            {
                ddlselectcrieteria = Convert.ToInt32("1");
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
            ds = objcompany.Fetch_Balck_list_comapny(ddlselectcrieteria, textsearch);
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
