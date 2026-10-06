using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;


public partial class appAdministrator_Companystatustrack_By_User : System.Web.UI.Page
{

    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string Status;
    int successId;
    int Unitid, Formtypeid, count = 0;
    
    int urlId = 1;
   
    string GpRefrenceNumber = "";
    int Refrence_Id = 0;
    decimal Monetary_Limit;
    string ZonalAcceptStaus = "";
    string CompanyStatus = "";
    int Branch_headAutorised_id;
    int Zonal_head_branch_id;
    int Branch_Monetary_limitmax;
    int Zonal_monatrylimitmax;
    string Inspectionstatus;
    string IncompleteStatus;
    string ExpiredStatus;
    string CompanyName;
    StringBuilder sb;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId != 1 && RoleId != 10)
            {
                Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            }
            txt_Ref_no.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btn_status_track');");
            txt_Ref_no.Focus();
            if (!IsPostBack)
            {
                BindTableBranchwise();
            }
           // BindTable();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";

        }

    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        if (ddl_list.SelectedValue == "2")
        {
            BindTable();
        }
        else
        {
            BindCompanyWise();
        }
    }
    // Get all Monetary Limit to max size;
    public void bindMonetaryLimit()
    {
        try
        {
            objcompany = new clsCompany();
            DataSet ds_test = new DataSet();
            ds_test = objcompany.Fetc_maxMonetary_limit(2);
            DataSet Ds_test1 = objcompany.Fetc_maxMonetary_limit(3);
            if (ds_test.Tables[0].Rows.Count > 0)
            {

                Zonal_monatrylimitmax = Convert.ToInt32(ds_test.Tables[0].Rows[0]["MaxMonetaryLimit"].ToString());
            }
            else
            {
                ds_test.Dispose();
            }

            if (Ds_test1.Tables[0].Rows.Count > 0)
            {
                Branch_headAutorised_id = Convert.ToInt32(Ds_test1.Tables[0].Rows[0]["MaxMonetaryLimit"].ToString());
            }
            else
            {
                Ds_test1.Dispose();
            }
            

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError("" + ex.StackTrace);
        }
    }
    public void BindTable()
    {
        try
        {
            bindMonetaryLimit();
            GpRefrenceNumber = txt_Ref_no.Text.ToString();
            if (Request.QueryString.HasKeys())
            {
                  Branchid=Convert.ToInt32(Request.QueryString["id"].ToString());
            }
               
            sb = new StringBuilder();
            if (GpRefrenceNumber != "")
            {
                objcompany = new clsCompany();
                DataSet ds = new DataSet();
                ds = objcompany.Fetch_Company_status_track_byUser(GpRefrenceNumber, Branchid);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:800px; ' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th width='100px'>Company Name</th><th width='100px'>Reference Number</th>");
                    sb.Append("<th width='100px'>Branch Name</th><th>Application Status</th> <th> Status Date</th> <th> </th></tr>");

                    Monetary_Limit = Convert.ToDecimal(ds.Tables[0].Rows[0]["MonetaryLimit"].ToString());
                    ZonalAcceptStaus = ds.Tables[0].Rows[0]["ZonalAcceptStatus"].ToString();
                    CompanyStatus = ds.Tables[0].Rows[0]["CompanyStatus"].ToString();
                    IncompleteStatus = ds.Tables[0].Rows[0]["CompanyStatus"].ToString();
                    Inspectionstatus = ds.Tables[0].Rows[0]["InspectionStatus"].ToString();
                    sb.Append("<tr><td align='left'  valign='top'> " + ds.Tables[0].Rows[0]["CompanyName"].ToString() + " </td>");    // nOT CLOSE  this tr 
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["GPRefrenceNo"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["BranchName"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>");  // Note this in not close all status are track then we close it and make a table
                    sb.Append("<table border='0'cellpadding='2'cellspacing='2' style='background-color: #ffffff; margin-left:0px; width:350px;'>");

                    if (IncompleteStatus != "Expired" )
                    {
                        switch (IncompleteStatus)
                        {
                            case "Incomplete":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b>Application Form incomplete.</b></td></tr>");
                                break;
                            case "Waiting":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b> Application Form waiting to be accepted by Branch Office .</b></td></tr>");

                                break;
                            case "Inprocess":
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Agency Selected")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Application Form sent to Inspection Agency for inspection.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Request Generated")
                                {
                                    DataSet ds_agency = new DataSet();
                                    int AgencyUserId = Convert.ToInt32(ds.Tables[0].Rows[0]["AgencyUserid"].ToString());
                                    string AgencyName = string.Empty;
                                    string AgencyUsername = string.Empty;
                                    if (AgencyUserId != 0)
                                    {
                                        ds_agency = objcompany.Fetch_Inspection_Agency_and_user_name(AgencyUserId);
                                        if (ds_agency.Tables[0].Rows.Count > 0)
                                        {
                                            AgencyName = ds_agency.Tables[0].Rows[0]["InspectionAgencyName"].ToString();
                                            AgencyUsername = ds_agency.Tables[0].Rows[0]["UserName"].ToString();
                                        }
                                        else
                                        {
                                            ds_agency.Dispose();
                                        }
                                    }

                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Application Form forwarded to Inspection Agency for Inspection Date to be allocated.</b><br/><b>Inspection Agency:</b> <b>" + AgencyName.ToString() + ".<br> Agency User:" + AgencyUsername.ToString() + " </b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Date already Issued but Inspection is Pending")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Inspection Date already issued but Inspection is pending.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Complete")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td> </td>");
                                    sb.Append("<td colspan='2'> <b>Inspection Complete.  Waiting for branch office approval for store details.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Not complete Inspection")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Server Error.</b></td></tr>");
                                }

                                break;

                            case "Inspection Passed":
                                //lbl_branch_account_chek.Text = "You not set Monetary limit";
                                //break;
                                if (Monetary_Limit == 0)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Monetary Limit to be set by Branch Account.</b></td></tr>");
                                }


                                else
                                {
                                    if (Monetary_Limit > 0 && Monetary_Limit <= Branch_headAutorised_id && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Branch Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by Branch Head  awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit >= Branch_headAutorised_id && Monetary_Limit <= Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Zonal Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        // sb.Append("<tr><td style='color:red;'><b> Approval by Zonal Head awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit > Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by HO.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by HO HR awaited<b> </td></tr>");
                                    }
                                }
                                break;

                            case "Confirm":

                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'> <b> Registration Complete .</b></td></tr>");
                                break;


                        }



                        sb.Append("</table>");
                        sb.Append("</td>");
                        // else if(Inspectionstatus
                    }




                    else
                    {
                    }
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptID = objDecEnc.Encrypt("1");
                    string EncryptCompany_Name = "";
                    string EncryptCompanyID = "";
                    string EncryptBranchName = "";
                    string imageid = string.Empty;
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["StatusDate"].ToString()+"</td>");
                    EncryptCompany_Name = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["CompanyName"].ToString());
                    EncryptCompanyID = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["CompanyId"].ToString());
                    EncryptBranchName = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["BranchName"].ToString());
                    HyperLink hyper = new HyperLink();
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                  //  sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["RejectReson"].ToString() + "</td>");
                    hyper.Text = "View";
                    sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                    sb.Append("</tr>");

                    sb.Append("</table>");

                    lbl_report_status_track.Text = sb.ToString();

                }

                else
                {
                    ds.Dispose();
                    lbl_report_status_track.Text = "<b>There are no Record</b>";
                    lbl_report_status_track.CssClass = "Class_Red";

                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.StackTrace);
        }


    }

    public void BindTableBranchwise()
    {
        try
        {
            bindMonetaryLimit();
            if (Request.QueryString.HasKeys())
            {
                Branchid = Convert.ToInt32(Request.QueryString["id"].ToString());
            }
            sb = new StringBuilder();
            objcompany = new clsCompany();
            DataSet ds = new DataSet();
            ds = objcompany.Fetch_Company_status_track_Companywise_By_User(Branchid);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:800px; ' id='tbl_main" + i + "' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th width='100px'>Company Name</th><th width='100px'>Reference Number</th>");
                    sb.Append("<th width='100px'>Branch Name</th><th>Application Status</th><th>Status Date </th><th></th></tr>");

                    Monetary_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["MonetaryLimit"].ToString());
                    ZonalAcceptStaus = ds.Tables[0].Rows[i]["ZonalAcceptStatus"].ToString();
                    CompanyStatus = ds.Tables[0].Rows[i]["CompanyStatus"].ToString();
                    IncompleteStatus = ds.Tables[0].Rows[i]["CompanyStatus"].ToString();
                    Inspectionstatus = ds.Tables[0].Rows[i]["InspectionStatus"].ToString();
                    sb.Append("<tr><td align='left'  valign='top'> " + ds.Tables[0].Rows[i]["CompanyName"].ToString() + " </td>");    // nOT CLOSE  this tr 
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["GPRefrenceNo"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["BranchName"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>");  // Note this in not close all status are track then we close it and make a table
                    sb.Append("<table border='0'cellpadding='2'cellspacing='2' style='background-color: #ffffff; margin-left:0px; width:350px;' id='tbl_sub" + i + "'>");
                    if (IncompleteStatus != "Expired" )
                    {
                        switch (IncompleteStatus)
                        {
                            case "Incomplete":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b>Application Form incomplete.</b></td></tr>");
                                break;
                            case "Waiting":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b> Application Form waiting to be accepted by Branch Office .</b></td></tr>");

                                break;
                            case "Inprocess":
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Agency Selected")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Application Form sent to Inspection Agency for inspection.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Request Generated")
                                {

                                    DataSet ds_agency = new DataSet();
                                    int AgencyUserId = Convert.ToInt32(ds.Tables[0].Rows[i]["AgencyUserid"].ToString());
                                    string AgencyName = string.Empty;
                                    string AgencyUsername = string.Empty;
                                    if (AgencyUserId != 0)
                                    {
                                        ds_agency = objcompany.Fetch_Inspection_Agency_and_user_name(AgencyUserId);
                                        if (ds_agency.Tables[0].Rows.Count > 0)
                                        {
                                            AgencyName = ds_agency.Tables[0].Rows[0]["InspectionAgencyName"].ToString();
                                            AgencyUsername = ds_agency.Tables[0].Rows[0]["UserName"].ToString();
                                        }
                                        else
                                        {
                                            ds_agency.Dispose();
                                        }
                                    }

                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Application Form forwarded to Inspection Agency for Inspection Date to be allocated.</b><br/><b>Inspection Agency:</b> <b>" + AgencyName.ToString() + ".<br> Agency User:" + AgencyUsername.ToString() + " </b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Inspection Date already Issued but Inspection is Pending")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Inspection Date already issued but Inspection is pending.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Inspection Complete")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'> </td>");
                                    sb.Append("<td colspan='2'> <b>Inspection Complete.  Waiting for branch office approval for store details.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Not complete Inspection")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Server Error.</b></td></tr>");
                                }

                                break;

                            case "Inspection Passed":
                                //lbl_branch_account_chek.Text = "You not set Monetary limit";
                                //break;
                                if (Monetary_Limit == 0)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Monetary Limit to be set by Branch Account.</b></td></tr>");
                                }


                                else
                                {
                                    if (Monetary_Limit > 0 && Monetary_Limit <= Branch_headAutorised_id && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Branch Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by Branch Head  awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit >= Branch_headAutorised_id && Monetary_Limit <= Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Zonal Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        // sb.Append("<tr><td style='color:red;'><b> Approval by Zonal Head awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit > Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by HO.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by HO HR awaited<b> </td></tr>");
                                    }
                                }
                                break;

                            case "Confirm":

                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'> <b> Registration Complete .</b></td></tr>");
                                break;


                        }




                        // else if(Inspectionstatus
                    }




                    else
                    {

                        sb.Append("<tr><td align='left' valign='top' colspan='3'>" + "Expired" + "</td></tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["StatusDate"].ToString() + "</td>");
                       DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptID = objDecEnc.Encrypt("1");
                    string EncryptCompany_Name = "";
                    string EncryptCompanyID = "";
                    string EncryptBranchName = "";
                    string imageid = string.Empty;
                    EncryptCompany_Name = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["CompanyName"].ToString());
                    EncryptCompanyID = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                    EncryptBranchName = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["BranchName"].ToString());
                    HyperLink hyper = new HyperLink();
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    //  sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["RejectReson"].ToString() + "</td>");
                    hyper.Text = "View";
                    sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                 // sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["RejectReson"].ToString() + "</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("<br/>");






                    //End Of ForLoop
                }
                lbl_report_status_track.Text = sb.ToString();
            }
            else
            {
                lbl_report_status_track.Text = "<b>There are no record</b>";
                lbl_report_status_track.CssClass="Class_Red";
                ds.Dispose();
            }
     


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
    }


    public void BindCompanyWise()
    {
         bindMonetaryLimit();
            CompanyName = txt_Ref_no.Text.ToString();
            if (Request.QueryString.HasKeys())
            {
                  Branchid=Convert.ToInt32(Request.QueryString["id"].ToString());
            }
               
            sb = new StringBuilder();
            if (CompanyName != "")
            {
                objcompany = new clsCompany();
                DataSet ds = new DataSet();
                ds = objcompany.Fetch_Company_status_track_Companywise_By_User_ByCompanyName(CompanyName, Branchid);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:800px; ' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th width='100px'>Company Name</th><th width='100px'>Reference Number</th>");
                    sb.Append("<th width='100px'>Branch Name</th><th>Application Status</th> <th> Status Date</th> <th></th></tr>");

                    Monetary_Limit = Convert.ToDecimal(ds.Tables[0].Rows[0]["MonetaryLimit"].ToString());
                    ZonalAcceptStaus = ds.Tables[0].Rows[0]["ZonalAcceptStatus"].ToString();
                    CompanyStatus = ds.Tables[0].Rows[0]["CompanyStatus"].ToString();
                    IncompleteStatus = ds.Tables[0].Rows[0]["CompanyStatus"].ToString();
                    Inspectionstatus = ds.Tables[0].Rows[0]["InspectionStatus"].ToString();
                    sb.Append("<tr><td align='left'  valign='top'> " + ds.Tables[0].Rows[0]["CompanyName"].ToString() + " </td>");    // nOT CLOSE  this tr 
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["GPRefrenceNo"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["BranchName"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>");  // Note this in not close all status are track then we close it and make a table
                    sb.Append("<table border='0'cellpadding='2'cellspacing='2' style='background-color: #ffffff; margin-left:0px; width:350px;'>");

                    if (IncompleteStatus != "Expired")
                    {
                        switch (IncompleteStatus)
                        {
                            case "Incomplete":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b>Application Form incomplete.</b></td></tr>");
                                break;
                            case "Waiting":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2' valign='top'> <b> Application Form waiting to be accepted by Branch Office .</b></td></tr>");

                                break;
                            case "Inprocess":
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Agency Selected")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Application Form sent to Inspection Agency for inspection.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Request Generated")
                                {
                                    DataSet ds_agency = new DataSet();
                                    int AgencyUserId = Convert.ToInt32(ds.Tables[0].Rows[0]["AgencyUserid"].ToString());
                                    string AgencyName = string.Empty;
                                    string AgencyUsername = string.Empty;
                                    if (AgencyUserId != 0)
                                    {
                                        ds_agency = objcompany.Fetch_Inspection_Agency_and_user_name(AgencyUserId);
                                        if (ds_agency.Tables[0].Rows.Count > 0)
                                        {
                                            AgencyName = ds_agency.Tables[0].Rows[0]["InspectionAgencyName"].ToString();
                                            AgencyUsername = ds_agency.Tables[0].Rows[0]["UserName"].ToString();
                                        }
                                        else
                                        {
                                            ds_agency.Dispose();
                                        }
                                    }

                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Application Form forwarded to Inspection Agency for Inspection Date to be allocated.</b><br/><b>Inspection Agency:</b> <b>" + AgencyName.ToString() + ".<br> Agency User:" + AgencyUsername.ToString() + " </b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Date already Issued but Inspection is Pending")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Inspection Date already issued but Inspection is pending.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Inspection Complete")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'> </td>");
                                    sb.Append("<td colspan='2'> <b>Inspection Complete.  Waiting for branch office approval for store details.</b></td></tr>");
                                }
                                if (ds.Tables[0].Rows[0]["InspectionStatus"].ToString() == "Not complete Inspection")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Server Error.</b></td></tr>");
                                }

                                break;

                            case "Inspection Passed":
                                //lbl_branch_account_chek.Text = "You not set Monetary limit";
                                //break;
                                if (Monetary_Limit == 0)
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Monetary Limit to be set by Branch Account.</b></td></tr>");
                                }


                                else
                                {
                                    if (Monetary_Limit > 0 && Monetary_Limit <= Branch_headAutorised_id && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Branch Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by Branch Head  awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit >= Branch_headAutorised_id && Monetary_Limit <= Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Zonal Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        // sb.Append("<tr><td style='color:red;'><b> Approval by Zonal Head awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit > Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by HO.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + ds.Tables[0].Rows[0]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by HO HR awaited<b> </td></tr>");
                                    }
                                }
                                break;

                            case "Confirm":

                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'> <b> Registration Complete .</b></td></tr>");
                                break;


                        }



                        sb.Append("</table>");
                        sb.Append("</td>");
                        // else if(Inspectionstatus
                    }




                    else
                    {
                    }
                    sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["StatusDate"].ToString() + "</td>");
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptID = objDecEnc.Encrypt("1");
                    string EncryptCompany_Name = "";
                    string EncryptCompanyID = "";
                    string EncryptBranchName = "";
                    string imageid = string.Empty;
                   // sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["StatusDate"].ToString() + "</td>");
                    EncryptCompany_Name = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["CompanyName"].ToString());
                    EncryptCompanyID = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["CompanyId"].ToString());
                    EncryptBranchName = objDecEnc.Encrypt(ds.Tables[0].Rows[0]["BranchName"].ToString());
                    HyperLink hyper = new HyperLink();
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    //  sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["RejectReson"].ToString() + "</td>");
                    hyper.Text = "View";
                    sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                 //   sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["RejectReson"].ToString() + "</td>");  
                    sb.Append("</tr>");
                    sb.Append("</table>");

                    lbl_report_status_track.Text = sb.ToString();
                }
                else
                {
                    ds.Dispose();
                    lbl_report_status_track.Text = "<b>There are no record</b>";
                    lbl_report_status_track.CssClass = "Class_Red";
                }
            }
    }

    protected void ddl_list_SelectedIndexChanged(object sender, EventArgs e)
    {

        txt_Ref_no.Text = "";

    }
}
