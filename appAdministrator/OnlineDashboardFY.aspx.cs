
using System;

using System.Configuration;

using System.Data;

using System.Data.SqlClient;
using System.IO;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
public partial class appAdministrator_OnlineDashboardFY : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblDate.Text = "ONLINE SPRS APPLICATIONS";// AS ON " + txt_todate.Text; //DateTime.Today.ToShortDateString();

        }

    }
    DataSet ds = null;
    public void printReport()
    {

    }


    private void FillgrdOnline(string query, GridView grd)
    {

        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 60000;

                        sda.SelectCommand = cmd;
                       
                        using (ds = new DataSet())
                        {
                            sda.Fill(ds);
                        }
                    }
                    grd.DataSource = ds;
                    grd.DataBind();
                    lblCount.Text = string.Empty;
                    lblCount.Text = "Total unit count:" + grd.Rows.Count.ToString();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }

    public string fetchquery(string status, string GrdName)
    {
        string query = string.Empty;

        //DateTime fromDate = Convert.ToDateTime("01/01/1900");
        //DateTime toDate = Convert.ToDateTime("01/01/1900");
        string fromDate = "";// Convert.ToString("01/01/1900");
        string toDate = "";// Convert.ToString("01/01/1900");

        if (txt_from_date.Text != "" && txt_todate.Text != "")
        {
            fromDate = Convert.ToString(txt_from_date.Text);
            toDate = Convert.ToString(txt_todate.Text.ToString());
        }


        switch (GrdName)
        {
            case "grdOnline":
                //query = "Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,fGPtbl_Company_CDate as ApplDate,[fGPtbl_MB_Name] as BranchName,Count([fGPtbl_Company_Id]) as ApplicationCount," +

                //" CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' " + "WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' " + "WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' " +
                //"WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' " +  "WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' " +
                //"WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' " + "END as Status " + "from[dbo].[GPtbl_Company] " + "inner join[dbo].[GPtbl_CUnit] " +
                //"on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] " + "and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0 " +
                //"Inner join[dbo].[GPtbl_MasterBranch] " + "on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] " + "and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 " +
                //"where[fGPtbl_ApplicationType] in(1,2,3,4) " + "and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 " + "and fGPtbl_Company_Status in (" + status + ") " +
                //"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status,fGPtbl_Company_PAN,fGPtbl_Company_CDate " + "order by [fGPtbl_Company_CDate] Desc";

                query = "Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM," +
                    "fGPtbl_Company_PAN as PAN," +
                    "isnull(fGPtbl_ApplicationApprovalDt, fGPtbl_Company_CDate) as ApplDate,[fGPtbl_MB_Name] as BranchName," +
                    "Count([fGPtbl_Company_Id]) as ApplicationCount, CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' " +
                    "WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' " +
"WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' " +
"WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status" +
",[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA " +
"from[dbo].[GPtbl_Company] inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0" +
"inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 " +
"left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 and fGPtbl_PD_FormtypeId=1" +
"left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 " +
"left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] where[fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and fGPtbl_IS_FormtypeId=1" +
"where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 and  [fGPtbl_Company_Status] in (" + status + ") " +

"and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_EDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101))) as Datetime)between cast((Convert(varchar(50)," + "'"+ fromDate +"'" + " , 101)) as datetime)and cast((Convert(varchar(50), "+ "'" + toDate + "'"   + ", 101)) as datetime)" +

"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status, fGPtbl_Company_PAN, fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt " +
"order by isnull(fGPtbl_ApplicationApprovalDt, fGPtbl_Company_CDate) Desc";
                ;
                break;
            case "grdRenewal":
                query = "Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,fGPtbl_Company_CDate as RDate, " +
" Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' " +
"WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject' WHEN fGPtbl_Company_RenewalType = 7 THEN 'Renewal Completed' END as Status from GPtbl_CompanyRenewal " +
"inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 " +
"Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] " +
"and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 where fGPtbl_Company_RenewalType in(" + status + ") " + //and[fGPtbl_Company_Display]=0 and[fGPtbl_Company_Delete]=0 

"and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_EDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101))) as Datetime)between cast((Convert(varchar(50)," + "'" + fromDate + "'" + " , 101)) as datetime)and cast((Convert(varchar(50), " + "'" + toDate + "'" + ", 101)) as datetime)" +

"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc";
                break;
        }

        return query;
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStatus.SelectedIndex > 0)
        {
            string query = ddlStatus.SelectedValue.Equals("0") ? fetchquery("1,2,3,6,7", "grdOnline") : fetchquery(ddlStatus.SelectedValue, "grdOnline");
            FillgrdOnline(query, grdOnline);
        }
        if (ddlStatusRenewal.SelectedIndex > 0)
        {
            string query = ddlStatusRenewal.SelectedValue.Equals("0") ? fetchquery("1,2,3", "grdRenewal") : fetchquery(ddlStatusRenewal.SelectedValue, "grdRenewal");
            FillgrdOnline(query, grdRenewal);
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        //using (XLWorkbook wb = new XLWorkbook())
        //{
        //    wb.Worksheets.Add(ds.Tables[0], "SPRSUnits");
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    Response.AddHeader("content-disposition", "attachment;filename=SPRSUnitsReport.xlsx");
        //    using (MemoryStream MyMemoryStream = new MemoryStream())
        //    {
        //        wb.SaveAs(MyMemoryStream);
        //        MyMemoryStream.WriteTo(Response.OutputStream);
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }
    public string query(GridView grd)
    {
        string query = string.Empty;
        switch (grd.ID)
        {
            case "grdOnline":
                //                query = "Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN, fGPtbl_Company_CDate  as ApplDate,[fGPtbl_MB_Name] as BranchName,Count([fGPtbl_Company_Id]) as ApplicationCount," +
                //" CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' " + "WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' " +
                //"WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' " + "WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' " +
                //"WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' " + "WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' " +
                //"WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' " + "END as Status " + "from[dbo].[GPtbl_Company] " +
                //"inner join[dbo].[GPtbl_CUnit] " + "on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] " +
                //"and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0 " + "Inner join[dbo].[GPtbl_MasterBranch] " + "on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] " +
                //"and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 " + "where[fGPtbl_ApplicationType] in(1,2,3,4) " + "and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 and fGPtbl_Company_Status in (1,2,3,4,6,7)" +
                //"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status,fGPtbl_Company_PAN,fGPtbl_Company_CDate " + "order by [fGPtbl_Company_CDate] Desc";

                query = "Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM," +
                   "fGPtbl_Company_PAN as PAN,isnull(fGPtbl_ApplicationApprovalDt, fGPtbl_Company_CDate) as ApplDate,[fGPtbl_MB_Name] as BranchName," +
                   "Count([fGPtbl_Company_Id]) as ApplicationCount, CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' " +
                   "WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' " +
"WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' " +
"WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status" +
",[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA " +
"from[dbo].[GPtbl_Company] inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0" +
"inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 " +
"left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 and fGPtbl_PD_FormtypeId=1" +
"left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 " +
"left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] where[fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and fGPtbl_IS_FormtypeId=1" +
"where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 and  [fGPtbl_Company_Status] in (1,2,3,6,7) " +
"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status, fGPtbl_Company_PAN, fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt " +
"order by fGPtbl_Company_CDate Desc";

                break;
            case "grdRenewal":
                query = "Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName, fGPtbl_Company_CDate   as RDate," +
" Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' " +
"WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject'  END as Status from GPtbl_CompanyRenewal " +
"inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 " +
"Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] " +
"and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 where fGPtbl_Company_RenewalType in(0,1,2,3,7) " + //and[fGPtbl_Company_Display] = 1 and[fGPtbl_Company_Delete] = 0
"group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc";
                break;
        }
        FillgrdOnline(query, grd);
        return query;
    }

    public DataSet Fetch_datewise_LiveUnit_OnlineDashBoard(  string Formadte, string todate)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_OnlineDashBoard", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
           // if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            //if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public DataSet Renewal_datewise_LiveUnit_OnlineDashBoard(int BranchID, string Formadte, string todate, int CastID)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_RenewalUnit_OnlineDashBoard", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        if (Convert.ToInt32(ddlStatus.SelectedValue) != 1000)
        {
            if(Convert.ToInt32(ddlStatus.SelectedValue) == 1)
            {
                string fromdate = ""; string todate = "";
                int CastID = 0;
                
                    fromdate = txt_from_date.Text.ToString();
                    todate = txt_todate.Text.ToString();
                DataSet dsss = new DataSet();
                dsss = Fetch_datewise_LiveUnit_OnlineDashBoard(fromdate, todate);
                grdOnline.DataSource = dsss;
                grdOnline.DataBind();
                lblCount.Text = string.Empty;
                lblCount.Text = "Total unit count:" + grdOnline.Rows.Count.ToString();

                rfdddlRenewalVal.Enabled = false;
                grdOnline.Visible = true;
                grdRenewal.Visible = false;
            }
            else
            {
                if (Convert.ToInt32(ddlStatus.SelectedValue) == 1)
                {
                    string fromdate = ""; string todate = "";
                    int CastID = 0;

                    fromdate = txt_from_date.Text.ToString();
                    todate = txt_todate.Text.ToString();
                    DataSet ds = new DataSet();
                    ds = Renewal_datewise_LiveUnit_OnlineDashBoard(0, fromdate, todate, 0);

                    grdRenewal.DataSource = ds;
                    grdOnline.DataBind();
                    lblCount.Text = string.Empty;
                    lblCount.Text = "Total unit count:" + grdOnline.Rows.Count.ToString();

                    rfdddlRenewalVal.Enabled = false;
                    grdOnline.Visible = true;
                    grdRenewal.Visible = false;
                }

                else
                {

                    
                rfdddlRenewalVal.Enabled = false;
                    string query = ddlStatus.SelectedValue.Equals("0") ? fetchquery("1,2,3,6,7", "grdOnline") : fetchquery(ddlStatus.SelectedValue, "grdOnline");
                    FillgrdOnline(query, grdOnline);
                    grdOnline.Visible = true;
                    grdRenewal.Visible = false;
                }
            }
            


        }
        if (Convert.ToInt32(ddlStatusRenewal.SelectedValue) != 1000)
           // if (ddlStatusRenewal.SelectedIndex >= 0)
        {
            rfdDDlfresh.Enabled = false;
            string query = ddlStatusRenewal.SelectedValue.Equals("0") ? fetchquery("1,2,3", "grdRenewal") : fetchquery(ddlStatusRenewal.SelectedValue, "grdRenewal");
            FillgrdOnline(query, grdRenewal);
            grdOnline.Visible = false;
            grdRenewal.Visible = true;
        }

    }
    protected void rdFresh_CheckedChanged(object sender, EventArgs e)
    {
        if ((sender as RadioButton).ID.Equals("rdRenewal"))
        {
            ddlStatusRenewal.Visible = grdRenewal.Visible = true;
            ddlStatus.Visible = grdOnline.Visible = false;
           // query(grdRenewal);
            ddlStatus.SelectedIndex = 0;
        }
        if ((sender as RadioButton).ID.Equals("rdFresh"))
        {
            ddlStatusRenewal.Visible = grdRenewal.Visible = false;
            ddlStatus.Visible = grdOnline.Visible = true;
          //  query(grdOnline);
            ddlStatusRenewal.SelectedIndex = 0;
        }
    }

    protected void ddlStatus_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}