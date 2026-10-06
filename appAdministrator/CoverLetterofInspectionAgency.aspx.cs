using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;


public partial class appAdministrator_CoverLetterofInspectionAgency : System.Web.UI.Page
{
    clsCompany objclscomapny;
    
    string UserFullName = string.Empty;
    int UnitID;
    int UserID;
    int BranchID;
    int option;
    clsTrans_UserBranch objtub;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString.HasKeys())
            {

                UnitID = Convert.ToInt32(Request.QueryString["id"].ToString());
                option = Convert.ToInt32(Request.QueryString["option"].ToString());


                BindDatalabeltext(UnitID, option);
            }
           
        }

        catch (Exception ex)
        {
            UnitID = 0;
            UserID = 0;
            BranchID = 0;

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    public void BindDatalabeltext(int CompanyId, int option)
    {
        try
        {
            objclscomapny = new clsCompany();
            DataSet ds = new DataSet();
            ds = objclscomapny.Company_fetch_Coverletter_inspection(CompanyId, option);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lbl_branch_office_name.Text = ds.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                lbl_branch_address.Text = ds.Tables[0].Rows[0]["fGtbl_MB_Address"].ToString();
                lbl_ref_no.Text = ds.Tables[0].Rows[0]["RefreenceNumber"].ToString();
                lbl_inspection_agengy_address.Text = ds.Tables[0].Rows[0]["fGPtbl_MIAU_Address"].ToString();
                lbl_Company_name.Text = ds.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                lbl_pay_order_no.Text = ds.Tables[0].Rows[0]["fGPtbl_PD_ChequeDDNo"].ToString();
                lbl_paydate.Text = ds.Tables[0].Rows[0]["fGPtbl_PD_Cheque_DD_Date"].ToString();
                lbl_drawn_on.Text = ds.Tables[0].Rows[0]["fGPtbl_PD_PaybalAt"].ToString();
                lbl_ruppees.Text = ds.Tables[0].Rows[0]["fGPtbl_PD_AgencyAmount"].ToString();
                lbl_Manager_name.Text = ds.Tables[0].Rows[0]["Branchhead"].ToString();
                lbl_phone_text.Text = ds.Tables[0].Rows[0]["fGtbl_MB_Phone"].ToString();
                lbl_fax_text.Text = ds.Tables[0].Rows[0]["fGtbl_MB_Fax"].ToString();
                lbl_email_text.Text = ds.Tables[0].Rows[0]["fGtbl_MB_Email"].ToString();
                lbl_date_time.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
            else
            {
                ds.Tables[0].Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
}
