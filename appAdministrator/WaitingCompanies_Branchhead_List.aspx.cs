using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_WaitingCompanies_Branchhead_List : System.Web.UI.Page
{
    clsCompany objCompany;
    clsRenewal objRenewal;
    DataSet Myds;
    string compname, compid, branchname;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    int successId, urlId = 0, CUnitAmendmentId = 0;
    string UserFullName;
    string emailid;
    string UserEmailID = string.Empty;
    clsAmendment objAmendment;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            if (RoleId == 3)
            {
                if (!Page.IsPostBack)
                {
                    F_BindGridview();
                }
            }
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError(EX.Message);
        }
    }

    private void F_BindGridview()
    {
        try
        {
            objAmendment = new clsAmendment();
            DataSet ds_renewal = new DataSet();
            ds_renewal = objAmendment.CompanyRenewal_Waiting_BranchHead_Dataset(UserId);
            if (ds_renewal.Tables[0].Rows.Count > 0)
            {
                gvRegistrationDetails.DataSource = ds_renewal.Tables[0];
                gvRegistrationDetails.DataBind();
            }
            else
            {
                gvRegistrationDetails.Dispose();
                gvRegistrationDetails.DataBind();
                ds_renewal.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gvRegistrationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltlsno = (Literal)e.Row.FindControl("ltlSno");
                ltlsno.Text = count.ToString();
                count++;

                DecryptEncrypt objDecr = new DecryptEncrypt();

                HiddenField hdn = (HiddenField)e.Row.FindControl("hdncid");
                HiddenField hdnbrn = (HiddenField)e.Row.FindControl("hdnbranch");

                compid = objDecr.Encrypt(hdn.Value.ToString());
                compname = objDecr.Encrypt(e.Row.Cells[2].Text.ToString());
                branchname = objDecr.Encrypt(hdnbrn.Value.ToString());

                //string EncryptCompanyid = objDecr.Encrypt(CompanyId.ToString());
                //string EncryptCompanyname = objDecr.Encrypt(UnitId.ToString());


                HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string CompanyID = Convert.ToString(hdn.Value);
                string EncryptCompany = objDecEnc.Encrypt(CompanyID.ToString());
                string EncryptUnit = objDecEnc.Encrypt("0");
                string EncryptStatus = objDecEnc.Encrypt("0");
                string EncryptBranchId = objDecEnc.Encrypt("0");
                string EncryptUrlId = objDecEnc.Encrypt("3");



                hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname;

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gvRegistrationDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "accept")
            {
                objRenewal = new clsRenewal();
                successId = objRenewal.CompanyRenewal_WatingCom_Branch_Head_Acccept(Convert.ToInt32(e.CommandArgument), UserId);
            }
            if (successId == 2)
            {
                F_BindGridview();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
