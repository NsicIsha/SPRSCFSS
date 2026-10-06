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
using System.IO;
using System.Text;
using System.Diagnostics;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_PRINTREPORTACESSPRINT : System.Web.UI.Page
{
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    string datefrom;
    string dateto;
    int zoneid;
    int branchid;
    int companyd;
    string Ctype = "";
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    string OrderBy = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["dt"] == "")
        {
            datefrom = "";
        }
        else
        {
            datefrom = Request.QueryString["dt"].ToString();
        }
        if (Request.QueryString["dto"] == "")
        {
            dateto = "";

        }
        else
        {
            dateto = Request.QueryString["dto"].ToString();
        }
        if (Request.QueryString["brid"] == "0")
        {
            branchid = 0;
        }
        else
        {
            branchid = Convert.ToInt32(Request.QueryString["brid"].ToString());
        }
        if (Request.QueryString["zid"] == "0")
        {
            zoneid = 0;
        }
        else
        {
            zoneid = Convert.ToInt32(Request.QueryString["zid"]);
        }
        if (Request.QueryString["cid"] == "0")
        {
            companyd = 0;

        }
        else
        {
            companyd = Convert.ToInt32(Request.QueryString["cid"]);
        }
        BindGridView();
    }


    public void BindGridView()
    {

        try
        {
           string dateFrom = datefrom;
           
           int BranchID = branchid;
           int ZoneID = zoneid;
           int CompanyId = companyd;
           OrderBy = Request.QueryString["orderby"].ToString();

            objcompany = new clsCompany();
            DataSet ds_bind_print_report = new DataSet();
           /* if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID, OrderBy);
            }

            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID,OrderBy);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID == 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWiseModified(dateFrom, dateto, OrderBy);
            }
            else if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId);
            }
            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID,OrderBy);
            }

            else if (dateFrom == "" && dateto == "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID,OrderBy);
            }
            else if (dateFrom == "" && dateto == "" && ZoneID == 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID, OrderBy);
            }*/
            ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(datefrom, dateto, branchid, zoneid, companyd, OrderBy);

            if (ds_bind_print_report.Tables[0].Rows.Count > 0)
            {
                grd_print_certificate.DataSource = ds_bind_print_report.Tables[0];
                grd_print_certificate.DataBind();
            }
            else
            {

               // Report_Type.Text = "N0 Record Found!!!";
                grd_print_certificate.Dispose();
                grd_print_certificate.DataBind();
                ds_bind_print_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
         
    }


    protected void grd_print_certificate_onrowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbl_address = (Label)e.Row.FindControl("lbl_Zone_Branch");  //For Zone Branch
            string Address = lbl_address.Text.Trim().ToString();//Replace(",", "<br/>");
            string decodedText = HttpUtility.HtmlDecode(Address);
            e.Row.Cells[1].Text = decodedText;
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
            Label lbl_Request = (Label)e.Row.FindControl("lbl_request_date");  //For Zone Branch
            string RequestDate = lbl_Request.Text.Trim().ToString();//Replace(" ", "<br/>");
            string decodedText1 = HttpUtility.HtmlDecode(RequestDate);
            e.Row.Cells[2].Text = decodedText1;
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            Label lbl_Approved_Date = (Label)e.Row.FindControl("lbl_RequsetDate_userName");  //For Zone Branch
            string ApprovedDate = lbl_Approved_Date.Text.Trim().ToString();//.Replace(" ", "<br/>");
            string decodedText2 = HttpUtility.HtmlDecode(ApprovedDate);
            e.Row.Cells[4].Text = decodedText2;
            e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
        }
    }
}
