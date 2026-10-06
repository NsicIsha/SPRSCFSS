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

public partial class TM_ListProductUnit : System.Web.UI.Page
{
    SqlConnection cnn;
    string connectionString = null; string sql, varOffice, varFY, varPara;
    int nosTotal = 0; int TotalBottom = 0;int flagcycle=0;
    string wherecl = "", ord = "", status = "", FinYear="";

    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
         cnn = new SqlConnection(connectionString);
        
       // varOffice = Request.QueryString["Office"].ToString(); varFY = Request.QueryString["FY"].ToString();

         ////if (Session["GenResuser"] == null) { Response.Redirect("LoginResource.aspx"); }
         ////varOffice = ""; varFY = "0"; // varPara = "NoU";


        if (!IsPostBack)
        {
            
        
        }
        

        //if (Request.QueryString["Off"] != null) { pnl_PartsDetails.Visible = true; loadGridView_NoofParts(); }
      
    }

    //public void loadGridView_Product_Unit(string varOffice, string varFY)
    //{
    //    if (varFY == "0") { varFY = "  "; } else { varFY = " and ( fBD_TA_Track_TenderApplyDate >= '04/01/" + varFY.ToString() + "'  and fBD_TA_Track_TenderApplyDate <='03/31/" + (Int32.Parse(varFY)+1).ToString() + "'  ) "; }
        
    //    if (varOffice != "") { varOffice = " and fBD_TA_Track_OfficeId = '" + varOffice + "'"; }

    //    sql = "select   distinct  Description Office,ltrim(rtrim(fBD_TA_Track_TenderProduct)) Product, fBD_TA_UD_UnitName 'Unit Name',fBD_TA_UD_CorpAddress Address,convert(varchar(12), fBD_TA_Track_TenderApplyDate,110) Part_Date  from BD_Tender_App_Track a inner join BD_Tender_App_UnitsDetails b on a.fBD_TA_Track_Id = b.fBD_TA_UD_Track_Id inner join mistrobo_dec05 c on a.fBD_TA_Track_OfficeId = c.code " + varOffice + varFY + " where fBD_TA_UD_Unitname <>''  order by product, description, Part_Date ";
    //   //lbl_Title.Text = sql.ToString();  
    //    cnn.Open();

    //    SqlDataAdapter adapter = new SqlDataAdapter();
    //    SqlCommand sqlCmd;
    //    DataSet ds = new DataSet();
    //    sqlCmd = new SqlCommand(sql, cnn);
    //    adapter.SelectCommand = sqlCmd;
    //    adapter.Fill(ds);
    //    cnn.Close();

    //    gdv_Product_Unit.DataSource = ds.Tables[0];
    //    gdv_Product_Unit.DataBind();


    //    lbl_Title.Font.Bold = true;
    //    lbl_Title.Text = "<br>The Product-Unit list of Tender Marketing System;Last updated time : " + DateTime.Now.ToString() + "</br></br></br>";
    //}

    protected void Button1_Click(object sender, EventArgs e)
    {


        switch (ddlStatus.SelectedValue.ToString())
        {
            case "DA":
                status = "and CU1.fGPtbl_CUnit_RegistrationStatus=3  ";  break;
            case "IPe":
                status = " and CU1.fGPtbl_CUnit_RegistrationStatus=2   "; break;
            case "IPs":
                status = " and CU1.fGPtbl_CUnit_RegistrationStatus=7   "; break;
            default:
                status = " and (CU1.fGPtbl_CUnit_RegistrationStatus=3  or CU1.fGPtbl_CUnit_RegistrationStatus=2 or CU1.fGPtbl_CUnit_RegistrationStatus=7 ) ";  break;
        }
        switch (ddlFY.SelectedValue.ToString())
        {
            case "2015":
            case "2016":
                FinYear = " and CU1.fGPtbl_CUnit_CDate >= '04/01/" + ddlFY.SelectedValue.ToString() + "' and CU1.fGPtbl_CUnit_CDate <= '03/31/" + (Convert.ToInt32(ddlFY.SelectedValue)+1).ToString() + "' "; break;
            default:
                FinYear = "  "; break;
        }
        switch (ddlWhere.SelectedValue.ToString())
            {
                case "3":
                    wherecl = "and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) > 90 "; break;
                case "<2":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) <= 60 "; break;
                case ">2":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) > 60 "; break;
                case "<6":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) <= 180 "; break;
                case "6":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) > 180 "; break;
                case "1y":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) > 365 "; break;
                case "2y":
                    wherecl = " and DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) > 730 "; break;

                default :
                    wherecl = " "; break;
            }

            switch (ddlOrd.SelectedValue.ToString())
            {
                case "Age":
                    ord  = " Order by DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) desc, mb1.fGPtbl_MB_Name, fGPtbl_CUnit_Cdate desc  "; break; 
                case "Office":
                    ord = " Order by mb1.fGPtbl_MB_Name,DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) desc  "; break; 
                case "Fee":
                    ord = " Order by fGPtbl_PD_UnitAmount desc, mb1.fGPtbl_MB_Name  "; break;
                case "CAmt":
                    ord = " Order by fGPtbl_PD_PaidAmount desc, mb1.fGPtbl_MB_Name  "; break;

                default:
                    ord = " order by  mb1.fGPtbl_MB_Name, fGPtbl_CUnit_Cdate desc, fgptbl_cunit_id ";
                    break; 
            }
            loadGridView_Macro();
            
    }

    public void loadGridView_Macro()
    {
        sql = "select   fGPtbl_Company_Name, mb1.fGPtbl_MB_Name, fGPtbl_CUnit_Cdate,CASE WHEN fGPtbl_CUnit_RegistrationStatus='2' THEN 'Inspection Pending' WHEN fGPtbl_CUnit_RegistrationStatus='3' THEN 'Documents Awaited' WHEN fGPtbl_CUnit_RegistrationStatus='7' Then 'Inspection Passed' END as 'Appl. Status' " +
                ",fGPtbl_CUnit_Cdate as dddd,fGPtbl_IS_DateStatus2, DateDiff(day, fGPtbl_CUnit_Cdate, getdate()) as Age ,DateDiff(day, fGPtbl_IS_DateStatus2, getdate()) as Age_From_Inspection_Alloted , isnull(  fGPtbl_PD_UnitAmount,0) as NSICFee ,isnull(  fGPtbl_PD_UnitServiceTax,0) NSICSerTax,isnull(  fGPtbl_PD_AgencyAmount,0) as AgencyFee ,isnull(  fGPtbl_PD_AgencyServiceTax,0) AgencySerTax, fGPtbl_MIA_Name,fGPtbl_TIAB_Name,fGPtbl_PD_BankName,fGPtbl_PD_BankBranch,fGPtbl_PD_ChequeDDNo,fGPtbl_PD_Cheque_DD_Date,fGPtbl_PD_PaidAmount  " +
                "from   dbo.GPtbl_CUnit  CU1  inner join dbo.GPtbl_Company cm  on cm.fGPtbl_Company_Id = cu1.fGPtbl_CUnit_CompanyId   and CU1.fGPtbl_CUnit_Delete =0  Inner Join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId=CU1.fGPtbl_CUnit_Id and InsSta.fGPtbl_IS_Display=1 and InsSta.fGPtbl_IS_Delete=0 Inner join GPtbl_Trans_InspectionagecnyBranch On GPtbl_Trans_InspectionagecnyBranch.fGPtbl_TIAB_Id=InsSta.fGPtbl_IS_AgencyUserId" + status.ToString() +
               
                "and CU1.fGPtbl_CUnit_AccessId = 0  and (fGPtbl_CUnit_FormtypeId =1 ) and cu1.fGPtbl_CUnit_BranchId in (Select fGPtbl_MB_ID from dbo.GPtbl_MasterBranch MB inner join  dbo.GPtbl_MasterZone MZ on MB.fGPtbl_MB_ZoneId = MZ.fGPtbl_MZ_Id and fGPtbl_MZ_Display = 1 and fGPtbl_MZ_Delete=0 where fGPtbl_MB_Display = 1 and fGPtbl_MB_Delete=0 " +
                ") and CU1.fGPtbl_CUnit_Display =1  and cm.fGPtbl_Company_Display =1  and cm.fGPtbl_Company_Delete = 0  and cm.fGPtbl_Company_AccessId =0 " +
                " inner join dbo.GPtbl_MasterBranch MB1 on MB1.fGPtbl_MB_Id = cu1.fGPtbl_CUnit_BranchId inner join dbo.GPtbl_PaymentDetail PD on PD.fGPtbl_PD_UnitId = CU1.fGPtbl_CUnit_Id inner join dbo.GPtbl_MasterInspectionAgency IA on IA.fGPtbl_MIA_Id = PD.fGPtbl_PD_AgencyId where fgptbl_pd_display ='1' and fgptbl_pd_FormTypeId ='1'  " + wherecl.ToString() + FinYear.ToString() + ord.ToString();

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

        //int record = ds.Tables[0].Rows.Count;
        //Response.Write(record);
        gdv_Macro.DataSource = ds.Tables[0];
        gdv_Macro.DataBind();

        // gdv_Product_Unit.Columns[0].ItemStyle.Wrap = true ;

        //for (int i = 0; i < 5;  i++)        //gdv_Product_Unit.Columns.Count;
        //{
        //    gdv_Product_Unit.Columns[0].ItemStyle.Width=20;
        //    gdv_Product_Unit.Columns[0].ItemStyle.Width = 10;
        //}
        ////lbl_Title.Text = gdv_Product_Unit.Columns.Count.ToString() ;




       // lbl_Title.Font.Bold = true;
       // if (Session["EOP_ddlYr"] == null)
       // {
       //     lbl_Macro.Text = "<br>EOP position under NSIC Offices.</br></br>Fin.Year: " + ddl_FilterEOP.SelectedItem.ToString() + "</br></br>";
       // }
       // else { lbl_Macro.Text = "<br>EOP position under NSIC Offices.</br></br>Fin.Year: " + Session["EOP_ddlYr"].ToString() + "-" + (Convert.ToInt32(Session["EOP_ddlYr"])+Convert.ToInt32(1)).ToString()  + " </br></br>"; }

       //// if ( Session["EOP_ddlYr"] != null & Session["EOP_ddlYr"].ToString() == "0") { lbl_Macro.Text = "<br>EOP position under NSIC Offices.</br></br>"; } 

       // //lbl_Macro.Text = varFinYear.ToString(); 
    }


    //public void loadGridView_NoofParts()
    //{
    //    flagcycle = 1;
    //    //sql= "select   distinct  Description Office , fBD_TA_UD_UnitName 'Unit Name' ,convert(varchar(12), fBD_TA_Track_TenderApplyDate,110) Part_Date  from BD_Tender_App_Track a inner join BD_Tender_App_UnitsDetails b on a.fBD_TA_Track_Id = b.fBD_TA_UD_Track_Id inner join mistrobo_dec05 c on a.fBD_TA_Track_OfficeId = c.code " + varOffice + varFY + " where fBD_TA_UD_Unitname <>''  order by  description, Part_Date ";
    //    string EOP_FinYr; if (Session["EOP_FinYr"] == null) { EOP_FinYr = " "; } else { EOP_FinYr = Session["EOP_FinYr"].ToString(); }
    //    sql = "select      Name+' / '+Fname as Name,DOB,Gender,Category,	Email+' / '+	MobileNo as Contact,	Address,	EduQualification,CollegeName,	RegDate,	BranchName+'-'+upper(User1) as BranchName, Zone  from UserReg inner join membergp on BranchName = membergp.id  where fEop_UR_display=1 and user1 = '" + Request.QueryString["Off"].ToString() + "'  " +  EOP_FinYr.ToString() + "  order by userreg.id, Zone,User1, CollegeName    ";
    //    cnn.Open();

    //    SqlDataAdapter adapter = new SqlDataAdapter();
    //    SqlCommand sqlCmd;
    //    DataSet ds = new DataSet();
    //    sqlCmd = new SqlCommand(sql, cnn);
    //    adapter.SelectCommand = sqlCmd;
    //    adapter.Fill(ds);
    //    cnn.Close();

    //    gdv_PartsDetails.DataSource = ds.Tables[0];
    //    gdv_PartsDetails.DataBind();
 
    //    lbl_PartsDetails.Font.Bold = true;
    //    lbl_PartsDetails.Text = "Print Time : " + DateTime.Now.ToString() + "</br></br></br>";
    //   // lbl_PartsDetails.Text = Session["EOP_FinYr"].ToString(); 
    //}
}
