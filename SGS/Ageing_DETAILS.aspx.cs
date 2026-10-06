using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Data.SqlClient;

using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Security.Cryptography;

public partial class SGS_Ageing_DETAILS : System.Web.UI.Page
{
    SqlConnection cnn;
    string connectionString = null; string sql, varOffice, varFY, varPara;
    int nosTotal = 0; int TotalBottom = 0; int flagcycle = 0;
    string wherecl = "", ord = "", status = "", FinYear = "";
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }

        if (USERID == 335) //CDC
        {
            AGENCYID = 10;
        }
        else if (USERID == 336)//Rites
        {
            AGENCYID = 9;
        }

        else if (USERID == 337)//MSMEDI
        {
            AGENCYID = 8;
        }

        else if (USERID == 345)//SGS
        {
            AGENCYID = 11;
        } 

        connectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        cnn = new SqlConnection(connectionString);

        // varOffice = Request.QueryString["Office"].ToString(); varFY = Request.QueryString["FY"].ToString();

        ////if (Session["GenResuser"] == null) { Response.Redirect("LoginResource.aspx"); }
        ////varOffice = ""; varFY = "0"; // varPara = "NoU";


        if (!IsPostBack)
        {

            loadGridView_Macro();
        }


        //if (Request.QueryString["Off"] != null) { pnl_PartsDetails.Visible = true; loadGridView_NoofParts(); }

    }
    
    public void loadGridView_Macro()
    {
        sql = "select   fGPtbl_Company_Name, mb1.fGPtbl_MB_Name, fGPtbl_CUnit_Cdate,fGPtbl_CUnit_RegistrationStatus,CASE WHEN fGPtbl_CUnit_RegistrationStatus='2' THEN 'Inspection Pending' WHEN fGPtbl_CUnit_RegistrationStatus='3' THEN 'Documents Awaited' WHEN fGPtbl_CUnit_RegistrationStatus='7' Then 'Inspection Passed' END as 'ApplicationStatus' " +
                ",fGPtbl_CUnit_Cdate as dddd,fGPtbl_IS_DateStatus2, DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) as Age ,DateDiff(day, fGPtbl_IS_DateStatus2, getdate()) as Age_From_Inspection_Alloted , isnull(  fGPtbl_PD_UnitAmount,0) as NSICFee ,isnull(  fGPtbl_PD_UnitServiceTax,0) NSICSerTax,isnull(  fGPtbl_PD_AgencyAmount,0) as AgencyFee ,isnull(  fGPtbl_PD_AgencyServiceTax,0) AgencySerTax, fGPtbl_MIA_Name,fGPtbl_TIAB_Name,fGPtbl_PD_BankName,fGPtbl_PD_BankBranch,fGPtbl_PD_ChequeDDNo,fGPtbl_PD_Cheque_DD_Date,fGPtbl_PD_PaidAmount  " +
                "from   dbo.GPtbl_CUnit  CU1  inner join dbo.GPtbl_Company cm  on cm.fGPtbl_Company_Id = cu1.fGPtbl_CUnit_CompanyId   and CU1.fGPtbl_CUnit_Delete =0  Inner Join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId=CU1.fGPtbl_CUnit_Id and InsSta.fGPtbl_IS_Display=1 and InsSta.fGPtbl_IS_Delete=0 Inner join GPtbl_Trans_InspectionagecnyBranch On GPtbl_Trans_InspectionagecnyBranch.fGPtbl_TIAB_Id=InsSta.fGPtbl_IS_AgencyUserId and fGPtbl_TIAB_AgencyId=" + AGENCYID +
                 "and CU1.fGPtbl_CUnit_AccessId = 0  and (fGPtbl_CUnit_FormtypeId =1 ) and cu1.fGPtbl_CUnit_BranchId in (Select fGPtbl_MB_ID from dbo.GPtbl_MasterBranch MB inner join  dbo.GPtbl_MasterZone MZ on MB.fGPtbl_MB_ZoneId = MZ.fGPtbl_MZ_Id and fGPtbl_MZ_Display = 1 and fGPtbl_MZ_Delete=0 where fGPtbl_MB_Display = 1 and fGPtbl_MB_Delete=0 " +
                ") and CU1.fGPtbl_CUnit_Display =1  and  CU1.fGPtbl_CUnit_RegistrationStatus=2 and cm.fGPtbl_Company_Display =1  and cm.fGPtbl_Company_Delete = 0  and cm.fGPtbl_Company_AccessId =0 " +
                " inner join dbo.GPtbl_MasterBranch MB1 on MB1.fGPtbl_MB_Id = cu1.fGPtbl_CUnit_BranchId inner join dbo.GPtbl_PaymentDetail PD on PD.fGPtbl_PD_UnitId = CU1.fGPtbl_CUnit_Id inner join dbo.GPtbl_MasterInspectionAgency IA on IA.fGPtbl_MIA_Id = PD.fGPtbl_PD_AgencyId where fgptbl_pd_display ='1' and fgptbl_pd_FormTypeId ='1'  ";

        //2 Feb 17 " inner join dbo.GPtbl_MasterBranch MB1 on MB1.fGPtbl_MB_Id = cu1.fGPtbl_CUnit_BranchId inner join dbo.GPtbl_PaymentDetail PD on PD.fGPtbl_PD_UnitId = CU1.fGPtbl_CUnit_Id inner join dbo.GPtbl_MasterInspectionAgency IA on IA.fGPtbl_MIA_Id = PD.fGPtbl_PD_AgencyId where fgptbl_pd_display ='1' and fgptbl_pd_FormTypeId ='1' and fgptbl_pd_UnitFeeId ='1' " + wherecl.ToString() + FinYear.ToString() + ord.ToString();
        lbl_Title.Text = "The underprocess GP case as on " + System.DateTime.Today.ToShortDateString();
        cnn.Open();
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommand sqlCmd;
        DataSet ds = new DataSet();
        sqlCmd = new SqlCommand(sql, cnn);
        adapter.SelectCommand = sqlCmd;
        adapter.Fill(ds);
        cnn.Close();
        gdv_Macro.DataSource = ds.Tables[0];
        gdv_Macro.DataBind();
    }
    
  
}