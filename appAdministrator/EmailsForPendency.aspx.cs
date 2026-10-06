using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IdentityModel.Tokens;

public partial class appAdministrator_EmailsForPendency : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

       
            EmailFreshMoreThan60Days();
            EmailFreshMoreThan90Days();
            EmailRenewalMoreThan60Days();
            EmailRenewalMoreThan90Days();
       

    }

    private static void EmailFreshMoreThan60Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("EmailID"));

        // Step 3: Loop and send mail
        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            string body = "<html><body>";
            body += "<p>Dear Branch,</p>";
            body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 60 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch for document verification:</p>";

            // Start table
            body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
            body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th></tr>";

            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); ;
                body += "<tr>";
                body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td>";
                body += "</tr>";
                serialNo++;
            }

            // Close table and email body
            body += "</table>";
            body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
            body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
            body += "</body></html>";

            SendEmail(email, "Units pending for Document Verification (More Than 60 days) ", body);
        }
    }

    private static void EmailFreshMoreThan90Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData90DaysFresh();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("ZoEmailID"));

        // Step 3: Loop and send mail
        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            string body = "<html><body>";
            body += "<p>Dear ZonalHead,</p>";
            body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 90 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch(s) for document verification:</p>";

            // Start table
            body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
            body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th><th>Branch Name</th></tr>";

            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); 
                string branchName=row.Field<string>("BranchName").ToString();
                body += "<tr>";
                body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td><td>" + branchName + "</td>";
                body += "</tr>";
                serialNo++;
            }

            // Close table and email body
            body += "</table>";
            body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
            body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
            body += "</body></html>";

            SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", body);
        }
    }

    private static void EmailRenewalMoreThan60Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData60DaysRenewal();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("BranchEmailID"));

        // Step 3: Loop and send mail
        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            string body = "<html><body>";
            body += "<p>Dear Branch,</p>";
            body += "<p>You need to look into below mentioned units in Renewal as they have crossed that \"More than 60 days\" limit in the software. Kindly take appropriate action and clear this list.The following renewal units are pending with your branch :</p>";

            // Start table
            body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
            body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th></tr>";

            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy"); ;
                body += "<tr>";
                body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td>";
                body += "</tr>";
                serialNo++;
            }

            // Close table and email body
            body += "</table>";
            body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
            body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
            body += "</body></html>";

            SendEmail(email, "Units pending for Document Verification (More Than 60 days) ", body);
        }
    }

    private static void EmailRenewalMoreThan90Days()
    {
        // Step 1: Get data from SQL
        DataTable unitData = GetUnitEmailData90DaysFresh();

        // Step 2: Group units by EmailID
        var grouped = unitData.AsEnumerable()
            .GroupBy(row => row.Field<string>("ZoEmailID"));

        // Step 3: Loop and send mail
        foreach (var group in grouped)
        {
            string email = group.Key;

            // Start HTML body
            string body = "<html><body>";
            body += "<p>Dear ZonalHead,</p>";
            body += "<p>You need to look into below mentioned units in Document Verification stage (Fresh) as they have crossed that \"More than 90 days\" limit in the software. Kindly take appropriate action and clear this list.The following fresh units are pending with your branch(s) for document verification:</p>";

            // Start table
            body += "<table border='1' cellpadding='5' cellspacing='0' style='border-collapse: collapse; font-family: Arial;'>";
            body += "<tr><th>S.No.</th><th>Company Name</th><th>Application Date</th><th>Branch Name</th></tr>";

            int serialNo = 1;
            foreach (var row in group)
            {
                string company = row.Field<string>("CompanyName");
                string applDate = row.Field<DateTime>("ApplDate").ToString("dd/MM/yyyy");
                string branchName = row.Field<string>("BranchName").ToString();
                body += "<tr>";
                body += "<td>" + serialNo + "</td><td>" + company + "</td><td>" + applDate + "</td><td>" + branchName + "</td>";
                body += "</tr>";
                serialNo++;
            }

            // Close table and email body
            body += "</table>";
            body += "<br/> Please report at SPRS DIVISION after proper follow ups\r\nThanking you, \r\n";
            body += "<p>Regards,<br/>Head Office <br/>\r\nNational Small Industries Corporation Ltd. \r\n</p>";
            body += "</body></html>";

            SendEmail(email, "Units pending for Document Verification (More Than 90 days) ", body);
        }
    }

    static DataTable GetUnitEmailData()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM,fGPtbl_Company_PAN as PAN,
 fGPtbl_Company_CDate as ApplDate,
datediff(day,fGPtbl_Company_CDate,getdate()) as Aging,
[fGPtbl_MB_Name] as BranchName,fGtbl_MB_Email as EmailID,Count([fGPtbl_Company_Id]) as ApplicationCount, 
CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' 
WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' 
WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' 
WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' 
WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' 
WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' 
WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status,
[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,
DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA from[dbo].[GPtbl_Company] 
inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0
inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 
left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 
and fGPtbl_PD_FormtypeId=1
left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 
left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] 
where [fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) 
and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and 
fGPtbl_IS_FormtypeId=1 where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 
and  [fGPtbl_Company_Status] in (3) 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2025' , 101)) as datetime)
and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],fGtbl_MB_Email,[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_Status, fGPtbl_Company_PAN, fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt order by Aging Desc ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }

    static DataTable GetUnitEmailData90DaysFresh()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select  [fGPtbl_Company_Id],[fGPtbl_Company_Name] as CompanyName,[fGtbl_Company_AdharNumber] as UAM,fGPtbl_Company_PAN as PAN,
 fGPtbl_Company_CDate as ApplDate,
datediff(day,fGPtbl_Company_CDate,getdate()) as Aging,
[fGPtbl_MB_Name] as BranchName,fGtbl_MB_Email as EmailID,Count([fGPtbl_Company_Id]) as ApplicationCount, fGPtbl_MZ_EmailID as ZoEmailID,
CASE WHEN[fGPtbl_Company_Status] = 1 THEN 'Confirm' 
WHEN[fGPtbl_Company_Status] = 2 THEN 'InProcess' 
WHEN[fGPtbl_Company_Status] = 3 THEN 'Waiting' 
WHEN[fGPtbl_Company_Status] = 4 THEN 'Expired' 
WHEN[fGPtbl_Company_Status] = 5 THEN 'Incomplete' 
WHEN[fGPtbl_Company_Status] = 6 THEN 'Reject' 
WHEN[fGPtbl_Company_Status] = 7 THEN 'Inspection Passed' END as Status,
[fGPtbl_MIA_Name] as Agency,fGPtbl_IS_DateStatus2 as InspectionAllotmentDate,
DateDiff(day,  fGPtbl_IS_DateStatus2, getdate()) as AgeFromIA from[dbo].[GPtbl_Company] 
inner join[dbo].[GPtbl_CUnit] on[dbo].[GPtbl_Company].[fGPtbl_Company_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and [fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0
inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0 
inner join [dbo].[GPtbl_MasterZone] on [fGPtbl_MZ_Id]=[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_ZoneId] and [fGPtbl_MZ_Display]=1 and [fGPtbl_MZ_Delete]=0
left outer join [dbo].[GPtbl_PaymentDetail] on [fGPtbl_PD_UnitId]=(select top 1 [fGPtbl_CUnit_Id] from  [GPtbl_CUnit] where [fGPtbl_CUnit_CompanyId]=[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display]=1 and[fGPtbl_CUnit_Delete]=0) and fGPtbl_PD_Display=1 and fGPtbl_PD_Delete=0 
and fGPtbl_PD_FormtypeId=1
left outer  join[dbo].[GPtbl_MasterInspectionAgency] on[fGPtbl_MIA_Id] =[GPtbl_PaymentDetail].fGPtbl_PD_AgencyId and fGPtbl_MIA_Display = 1 and fGPtbl_MIA_Delete = 0 
left outer join GPtbl_InspectionStatus InsSta on InsSta.fGPtbl_IS_UnitId = (select top 1[fGPtbl_CUnit_Id] from[GPtbl_CUnit] 
where [fGPtbl_CUnit_CompanyId] =[fGPtbl_Company_Id] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0) 
and InsSta.fGPtbl_IS_Display = 1 and InsSta.fGPtbl_IS_Delete = 0 and 
fGPtbl_IS_FormtypeId=1 where[fGPtbl_ApplicationType] in(0,1,2,3,4) and[fGPtbl_Company_Display]=1 and[fGPtbl_Company_Delete]=0 
and  [fGPtbl_Company_Status] in (3) 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_Company].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2025' , 101)) as datetime)
and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],fGtbl_MB_Email,[fGPtbl_Company_Name],[fGPtbl_MB_Name],fGPtbl_MZ_EmailID, fGPtbl_Company_Status, fGPtbl_Company_PAN,fGPtbl_Company_CDate,[fGPtbl_MIA_Name], fGPtbl_IS_DateStatus2,[fGtbl_Company_AdharNumber],fGPtbl_ApplicationApprovalDt order by Aging Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }
    static DataTable GetUnitEmailData60DaysRenewal()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,
fGPtbl_Company_CDate as RDate,  Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' 
WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject' 
WHEN fGPtbl_Company_RenewalType = 7 THEN 'Renewal Completed' END as Status from GPtbl_CompanyRenewal 
inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_RenewalType in(1) and fGPtbl_Company_Status=7 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023' , 101)) as datetime) and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],[fGtbl_MB_Email],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate  
union  
Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,fGPtbl_Company_CDate  as RDate,  
Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_Ren_Amnd_Type = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_Ren_Amnd_Type = 2 
THEN 'Approved by Branch' WHEN fGPtbl_Company_Ren_Amnd_Type = 3 THEN 'Reject' WHEN fGPtbl_Company_Ren_Amnd_Type = 7 THEN 'Renewal Completed' END as Status 
from GPtbl_CompanyAmendment inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyAmendment.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] 
and [fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_Ren_Amnd_Type in (1) and fGPtbl_Company_Status=7 and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023', 101)) as datetime)and datediff(day,fGPtbl_Company_CDate,getdate())>=60
group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], [fGtbl_MB_Email],fGPtbl_Company_Ren_Amnd_Type, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }

    static DataTable GetUnitEmailData90DaysRenewal()
    {
        DataTable dt = new DataTable();

        string connStr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(connStr))
        {
            string query = @"
               Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,
fGPtbl_Company_CDate as RDate,  Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_RenewalType = 1 THEN 'Applied by MSE' 
WHEN fGPtbl_Company_RenewalType = 2 THEN 'Approved by Branch' WHEN fGPtbl_Company_RenewalType = 3 THEN 'Reject' 
WHEN fGPtbl_Company_RenewalType = 7 THEN 'Renewal Completed' END as Status from GPtbl_CompanyRenewal 
inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyRenewal.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] and[fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_RenewalType in(1) and fGPtbl_Company_Status=7 
and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyRenewal].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023' , 101)) as datetime) and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],[fGtbl_MB_Email],[fGPtbl_Company_Name],[fGPtbl_MB_Name], fGPtbl_Company_RenewalType, fGPtbl_Company_PAN,fGPtbl_Company_CDate  
union  
Select [fGPtbl_Company_Name] as CompanyName,fGPtbl_Company_PAN as PAN,[fGPtbl_MB_Name] as BranchName,[fGtbl_MB_Email] as BranchEmailID,fGPtbl_Company_CDate  as RDate,  
Count([fGPtbl_Company_Id]) as ApplicationCount,CASE WHEN fGPtbl_Company_Ren_Amnd_Type = 1 THEN 'Applied by MSE' WHEN fGPtbl_Company_Ren_Amnd_Type = 2 
THEN 'Approved by Branch' WHEN fGPtbl_Company_Ren_Amnd_Type = 3 THEN 'Reject' WHEN fGPtbl_Company_Ren_Amnd_Type = 7 THEN 'Renewal Completed' END as Status 
from GPtbl_CompanyAmendment inner join[dbo].[GPtbl_CUnit] on[dbo].GPtbl_CompanyAmendment.[fGPtbl_Company_Id] =[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_CompanyId] 
and [fGPtbl_CUnit_Display] = 1 and[fGPtbl_CUnit_Delete] = 0 
Inner join[dbo].[GPtbl_MasterBranch] on[dbo].[GPtbl_MasterBranch].[fGPtbl_MB_Id]=[dbo].[GPtbl_CUnit].[fGPtbl_CUnit_BranchId] and[fGPtbl_MB_Display]=1 and[fGPtbl_MB_Delete]=0
where fGPtbl_Company_Ren_Amnd_Type in (1) and fGPtbl_Company_Status=7 and cast(ISNULL((Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101)),(Convert(varchar(50),[dbo].[GPtbl_CompanyAmendment].fGPtbl_Company_CDate, 101))) as Datetime)>= cast((Convert(varchar(50),'04/01/2023', 101)) as datetime)and datediff(day,fGPtbl_Company_CDate,getdate())>=90
group by[fGPtbl_Company_Id],[fGPtbl_Company_Name],[fGPtbl_MB_Name], [fGtbl_MB_Email],fGPtbl_Company_Ren_Amnd_Type, fGPtbl_Company_PAN,fGPtbl_Company_CDate order by [fGPtbl_Company_CDate] Desc  ";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
            }
        }

        return dt;
    }
    static void SendEmail(string toEmail, string subject, string body)
    {

        string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
        string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
        string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
        string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


        MailMessage message = new MailMessage();
        message.From = new MailAddress("emailer@support.nsicspronline.com", "Support-SinglePointRegistration");
       
        message.To.Add(new MailAddress(toEmail));
        message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));

        message.Subject = subject;
        // Set the body of the mail message
        message.Body = body;

        SmtpClient smtpClient = new SmtpClient();
        smtpClient.Host = SmtpServerName;

        message.IsBodyHtml = true;
        smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
        smtpClient.Send(message);

    }
}
