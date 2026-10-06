using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_Default3 : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    string PageUrl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
           // PageUrl = GetCurrentPageName();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        if (!IsPostBack)
        {
          
        }
    }

    //    protected DataSet AnnexureDQuery()
    //    {
    //        DataSet ds = new DataSet();
    //        DateTime startDate = Convert.ToDateTime(txt_fromdate.Text);
    //        DateTime enddate = Convert.ToDateTime(txt_todate.Text);
    //        using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString()))
    //        {
    //            string query = "select fGPtbl_MZ_Name as Zone, fGPtbl_MB_Name, fGPtbl_MB_Id, fGPtbl_MB_ZoneId,(select dbo.GETFRESHREGISTRAIONCOUNT_modified(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "' , 1) ) as fresh, " +
    //"(select dbo.GETAMENDMENTBRANCHWISE_Renewal_RenewalAmendment(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1) ) as Amend," +
    //"(select dbo.GETRENEWALBRANCHWISE_Renewal_RenewalAmendment(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1) ) as renewal," +
    //"(select dbo.GETFRESHREGISTRAIONCOUNT_RevenueReport_RA(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1, -1) ) as freshRevenue," +
    //"(select dbo.GETAMENDMENTBRANCHWISE_RevenueReport_RA(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1, -1) ) as AmendRevenue," +
    //"(select dbo.GETRENEWALBRANCHWISE_RevenueReport_RA(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1, -1) ) as renewalRevenue " +
    //"from dbo.GPtbl_MasterBranch MB inner join dbo.GPtbl_MasterZone MZ on MB.fGPtbl_MB_ZoneId = MZ.fGPtbl_MZ_Id and MZ.fGPtbl_MZ_Display = 1" +
    //"and MZ.fGPtbl_MZ_Delete = 0 where MB.fGPtbl_MB_Display = 1 and MB.fGPtbl_MB_Delete = 0 order by fGPtbl_MZ_Name,fGPtbl_MB_Name";
    //            SqlCommand cmd = new SqlCommand(query, con);
    //            SqlDataAdapter ad = new SqlDataAdapter(cmd);
    //            con.Open();
    //            ad.Fill(ds);
    //        }
    //        return ds;

    //    }
    protected DataSet AnnexureDQuery()
    {
        DataSet ds = new DataSet();
        DateTime startDate = Convert.ToDateTime(txt_fromdate.Text);
        DateTime enddate = Convert.ToDateTime(txt_todate.Text);
        using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString()))
        {
            string query = "select fGPtbl_MZ_Name as Zone, fGPtbl_MB_Name, fGPtbl_MB_Id, fGPtbl_MB_ZoneId,(select dbo.GETFRESHREGISTRAIONCOUNT_modified(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "' , 1) ) as fresh, " +

     "(select dbo.GET_TotalRevenue_For_Fresh_R_RA_Amnd(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "') ) as Total_Revenue_CurrentFY," +
     "(select dbo.GETFRESHREGISTRAIONCOUNT_modified_FY_2425(fGPtbl_MB_Name, '" + startDate + "','" + enddate + "') ) as Total_Revenue_FY2425," +


"(select dbo.GETAMENDMENTBRANCHWISE_Renewal_RenewalAmendment_With_RDate_PID(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1) ) as Amend," +
"(select dbo.GETRENEWALBRANCHWISE_Renewal_RenewalAmendment_With_RDate_PID(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1) ) as renewal," +
"(select dbo.GETFRESHREGISTRAIONCOUNT_RevenueReport_RA(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1, -1) ) as freshRevenue," +
"(select dbo.GETAMENDMENTBRANCHWISE_RevenueReport_RA_PID(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "',1, -1) ) as AmendRevenue," +
"(select dbo.GETRENEWALBRANCHWISE_RevenueReport_WebService_PID(fGPtbl_MB_Id, '" + startDate + "','" + enddate + "') ) as renewalRevenue " +
"from dbo.GPtbl_MasterBranch MB inner join dbo.GPtbl_MasterZone MZ on MB.fGPtbl_MB_ZoneId = MZ.fGPtbl_MZ_Id and MZ.fGPtbl_MZ_Display = 1" +
"and MZ.fGPtbl_MZ_Delete = 0 where MB.fGPtbl_MB_Display = 1 and MB.fGPtbl_MB_Delete = 0 order by fGPtbl_MZ_Name,fGPtbl_MB_Name";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            con.Open();
            ad.Fill(ds);
        }
        return ds;

    }
    public void Button1_Click()
    {
        rptVw80.ProcessingMode = ProcessingMode.Local;
        rptVw80.LocalReport.ReportPath = Server.MapPath("~/appAdministrator/MISReport.rdlc");
        DataSet ds = new DataSet();
        ds = AnnexureDQuery();
        if (ds.Tables[0].Rows.Count > 0)        
        {
            rptVw80.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.rptVw80.LocalReport.DataSources.Clear();
            rptVw80.LocalReport.DataSources.Add(datasource);
            rptVw80.LocalReport.Refresh();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Button1_Click();
    }
}