using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
public partial class appAdministrator_RejectedCompanies : System.Web.UI.Page
{
    clsRenewal ObjRenewal; 

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
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }


    public void GridViewDataBind()
    {
        try
        {
            ObjRenewal = new clsRenewal();
            gvCertificateDetails.DataSource = ObjRenewal.Company_Fetch_RejectedList(Branchid);
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

    protected void gvCertificateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
               
                string Formtypeid_renewal = objDecEnc.Encrypt("2");
                string BranchId = objDecEnc.Encrypt(Branchid.ToString());
               
               
                LinkButton lnkrenewal = (LinkButton)e.Row.FindControl("lnkRenewal");
                HiddenField hdnRenewal = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdRenewal");
                string Company_Id = objDecEnc.Encrypt(lnkrenewal.CommandArgument.ToString());
              
                string imageidRenewal = "UnitRenewalStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + BranchId.ToString();
              
               
                lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");

                //TO CAPTURE PRINT ACCESS AND ACCEPT BY ADMIN STATUS
              

              
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
}
