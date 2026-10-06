using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
public partial class appAdministrator_InspectionReportPath : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    clsCompany objcompany;
    DataTable dt;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            scriptManager.RegisterPostBackControl(this.btn_Excel);
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            Bind_gridview();

            if (!IsPostBack)
            {

                System.Data.DataTable dt = new DataTable();

                //Category Cast DROPDOWNLIST
                dt = clsDDL.ddlInspectionAgency();
                ddl_InspectionAgency.DataSource = dt;
                ddl_InspectionAgency.DataTextField = dt.Columns["fGPtbl_MIA_Name"].ToString();
                ddl_InspectionAgency.DataValueField = dt.Columns["fGPtbl_MIA_Id"].ToString();
                ddl_InspectionAgency.DataBind();
                ddl_InspectionAgency.Items.Insert(0, new ListItem("-- Select Inspection Agency--", ""));

                //Category Area DROPDOWNLIST
                dt.Clear();
            }
        }

        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }
    protected void grd_inspection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_inspection.PageIndex = e.NewPageIndex;
        Bind_gridview();
    }

    public void Bind_gridview()
    {

        int agency = -1;
        string fromdate = "";
        string todate = "";
        try
        {


            if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            }
            if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            }
            agency = ddl_InspectionAgency.SelectedValue != "" ? Convert.ToInt32(ddl_InspectionAgency.SelectedValue) : -1;

            objcompany = new clsCompany();
            DataSet ds = new DataSet();
            //ds = objcompany.Fetch_Inspection_Report_nullpath_mod();
            ds = objcompany.Fetch_Inspection_Report_nullpath_mod_MOD(fromdate,todate, agency);
            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                grd_inspection.DataSource = dt;
                grd_inspection.DataBind();
            }
            else
            {
                ds.Dispose();
                dt.Dispose();
                grd_inspection.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "  " + ex.StackTrace.ToString());
        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bind_gridview();
    }

    public string SetDays(string dt)
    {

        DateTime D = Convert.ToDateTime(dt);

        int dd = (DateTime.Now - D).Days;
        return dd.ToString();

    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {


        int agency = -1;
        string fromdate = "";
        string todate = "";
        try
        {


            if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            }
            if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            }
            agency = ddl_InspectionAgency.SelectedValue != "" ? Convert.ToInt32(ddl_InspectionAgency.SelectedValue) : -1;

            objcompany = new clsCompany();
            DataSet ds = new DataSet();

            ds = objcompany.Fetch_Inspection_EmailList(fromdate, todate, agency);
            dt = new DataTable();
            dt = ds.Tables[0];
            ArrayList listEmails = new ArrayList();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (listEmails.Contains(dr["InspectionAgencyEmail"]) == false)
                        listEmails.Add(dr["InspectionAgencyEmail"].ToString());
                }
            }

            string MailFrom = "nsicclient@nsic.co.in";
            string MailSubject = "List of Pending Inspection";
            string MailTo = "";
            string strMailBody = "";
            StringBuilder sb;
            StringBuilder emails = new StringBuilder();
            emails.AppendLine("");
            foreach (string email in listEmails)
            {
                sb = new StringBuilder();
                int count = 1;
                MailTo = "gaurav@nagsoft.com"; //email;
                emails.AppendLine(email);
                string branch = "";
                foreach (DataRow dr in dt.Select("InspectionAgencyEmail ='" + email + "'"))
                {
                   
                    if (count == 1)
                    {
                        sb.AppendLine("Dear " + dr["InspectionAgencyName"].ToString() + ",");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("The issuance of inspection date or inspection report is pending for the following companies:");
                    sb.AppendLine("<br/>");
                    sb.AppendLine("<br/>");
                    
                    
                        sb.AppendLine("<table border='1'>");
                        sb.AppendLine("<tr>");
                        sb.AppendLine("<td>SNo.</td><td>Company Name</td><td>Branch Name</td><td>Reason for Pendency</td><td>Pending Since</td>");
                        sb.AppendLine("</tr>");

                    }
                    int inspectionStatus = Convert.ToInt16(dr["InspectionStatus"]);
                    string reason = inspectionStatus == 2 ? "Inspection date to be issued" : "Inspection report pending";
                    branch = dr["BranchName"].ToString();
                    sb.AppendLine("<tr>");
                    sb.AppendLine("<td>" + count + "</td><td>" + dr["CompanyUnitAddress"].ToString() + "</td><td>" + dr["BranchName"].ToString() + "</td><td>" + reason + "</td><td>" + Convert.ToDateTime( dr["fGPtbl_IS_DateStatus1"]).ToString("dd/MM/yyyy") + "</td>");
                    sb.AppendLine("</tr>");

                    count++;
                }


                sb.AppendLine("</table>");
                sb.AppendLine("<br/>");
                sb.AppendLine("<br/>");
               
                sb.AppendLine("<br/>");
                sb.AppendLine("Regards,");
                sb.AppendLine("<br/>");
                sb.AppendLine("<br/>");
                sb.AppendLine("NSIC " + branch);
                sb.AppendLine("");
                strMailBody = sb.ToString();

                if (MailTo != "")
                {
                  string result=  clsScheduleMail.SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);
                    //INSERT INTO SENT MAILER
                  if (result != "1")
                  {

                      var message1 = new JavaScriptSerializer().Serialize(result);
                      var script1 = string.Format("alert({0});", message1);
                      ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script1, true);
                      clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, "XXXXXException OccuredXXXXX", result.ToString(), 1);
                  }
                  else
                  {
                      clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);
                  }
                }

            }
                       
            string msg = "Mail Sent to Below Agencies: " + emails.ToString();
            var message = new JavaScriptSerializer().Serialize(msg);
            var script = string.Format("alert({0});", message);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", script, true);
           // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + msg + "');", true);
        
        }


        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "  " + ex.StackTrace.ToString());
        }


    }
    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        ////int agency = -1;
        ////string fromdate = "";
        ////string todate = "";
        ////try
        ////{


        ////    if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
        ////    {
        ////        fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
        ////    }
        ////    if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
        ////    {
        ////        todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
        ////    }
        ////    agency = ddl_InspectionAgency.SelectedValue != "" ? Convert.ToInt32(ddl_InspectionAgency.SelectedValue) : -1;

        ////    objcompany = new clsCompany();
        ////    DataSet ds = new DataSet();
        ////    //ds = objcompany.Fetch_Inspection_Report_nullpath_mod();
        ////    ds = objcompany.Fetch_Inspection_Report_nullpath_mod_MOD(fromdate, todate, agency);
        ////    dt = new DataTable();
        ////    dt = ds.Tables[0];
        ////    if (dt.Rows.Count > 0)
        ////    {
        ////        DataTable Dt1 = new DataTable();
        ////        Dt1.Columns.Add("S No.");
        ////        Dt1.Columns.Add("Companyname/UnitAddress");
        ////        Dt1.Columns.Add("Zone/Branch");
        ////        Dt1.Columns.Add("StatusDate");
        ////        Dt1.Columns.Add("Type");
        ////        Dt1.Columns.Add("Inspection Agency/UserName");
        ////        Dt1.Columns.Add("No. of Days Pendency");

        ////        for (int i = 0; i < dt.Rows.Count; i++)
        ////        {
        ////            DateTime D = Convert.ToDateTime(dt.Rows[i][4]);
        ////            int dd = (DateTime.Now - D).Days;

        ////            Dt1.Rows.Add(i + 1, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][3].ToString(),
        ////            dt.Rows[i][2].ToString(), dd);

        ////        }
        ////        Dt1.AcceptChanges();
        ////        string fromdatetodate = string.Empty;
        ////        fromdatetodate = "<b><span >   Inspection Report " + fromdate + " " + todate + "</span></b>" + "<br/><br/>";

        ////        ExportDataSetToExcel(Dt1, "InspectionReport.xls", fromdatetodate);
        ////    }

        ////}
        ////catch (Exception ex)
        ////{
        ////    ErrHandler.WriteError(ex.Message.ToString() + "  " + ex.StackTrace.ToString());
        ////}


        try
        {
            clsCompany objcompany = new clsCompany();
            DataSet ds = new DataSet();
            //ds = objcompany.Fetch_Inspection_Report_nullpath_mod();
            ds = objcompany.POI_Details(1);
            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataTable Dt1 = new DataTable();
                Dt1.Columns.Add("S No.");
                Dt1.Columns.Add("PAN");
                Dt1.Columns.Add("Company Name");
                //Dt1.Columns.Add("StatusDate");
                //Dt1.Columns.Add("Type");
                //Dt1.Columns.Add("Inspection Agency/UserName");


                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    //Dt1.Rows.Add(i + 1, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][3].ToString(),
                    //dt.Rows[i][2].ToString());

                    Dt1.Rows.Add(i + 1, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString());

                }
                Dt1.AcceptChanges();
                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Inspection Report " + "" + " " + "" + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(Dt1, "InspectionReport.xls", fromdatetodate);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "  " + ex.StackTrace.ToString());
        }


    }

    public static void ExportDataSetToExcel(DataTable dsInput, string ExcelFileName, string text)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object 
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel 
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelFileName + "\"");
            response.Cache.SetCacheability(HttpCacheability.NoCache);



            GridView dg1 = new GridView();
            dg1.DataSource = dsInput;
            dg1.DataBind();


            // create a string writer 
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid

                    htw.Write("<table><tr><td colspan='9' align='center'>" + text + "</td></tr></table>");
                    htw.Write("<table><tr><td colspan='9'></td></tr></table>");
                    dg1.RenderControl(htw);
                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
                    response.End();
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }
}
