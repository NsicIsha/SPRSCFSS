using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;

public partial class appAdministrator_RenewalStatusTrack : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string filepath;
    int urlId = 1;
    string UserFullName;
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
    int BranchID_monetry = 0;
    int RoleID_montery = 0;
    int MinMonetryLimit = 0;
    int MaxMonetrylimit = 0;

    StringBuilder sb;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            txt_Ref_no.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btn_status_track');");
            txt_Ref_no.Focus();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            ErrHandler.WriteError("" + ex.StackTrace);
        }

    }

    public void bindDatamonetarylimit(int BranchID)
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds_test = new DataSet();
            //ds_test = objCompany.Fetc_maxMonetary_limit(2, );
            ds_test = objCompany.Fetch_Renewal_Company_status_track_UnderMonetryLimit(BranchID, 2);
            DataSet Ds_test1 = new DataSet();
            // Ds_test1=  objCompany.Fetc_maxMonetary_limit(3);
            Ds_test1 = objCompany.Fetch_Renewal_Company_status_track_UnderMonetryLimit(BranchID, 3);
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
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace);
        }
    }
    protected void txt_Ref_no_TextChanged(object sender, EventArgs e)
    {

    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        try
        {
           
            string Company_Name = string.Empty;
            if (txt_Ref_no.Text != "")
            {
                Company_Name = txt_Ref_no.Text.ToString();
            }
            objCompany = new clsCompany();
            Myds = new DataSet();
            Myds = objCompany.Fetch_Renewal_Company_status_track_notRegisterd(Company_Name);
            if (Myds.Tables[0].Rows.Count > 0)
            {
                sb = new StringBuilder();
                sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:800px; ' >");  // main table we have closed it
                sb.Append("<tr class='GrdviewHeader'><th width='100px'>Company Name</th><th width='100px'>PAN Number</th>");
               sb.Append("<th width='100px'>Branch Name</th><th>Application Status</th> <th> Status Date</th></tr>");  //<th></th>
                for (int i = 0; i <= Myds.Tables[0].Rows.Count - 1; i++)
                {
                    RoleID_montery = Convert.ToInt32(Myds.Tables[0].Rows[i]["BranchID"].ToString());
                    bindDatamonetarylimit(RoleID_montery);
                    Monetary_Limit = Convert.ToDecimal(Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString());
                    ZonalAcceptStaus = Myds.Tables[0].Rows[i]["ZonalAcceptStatus"].ToString();
                    CompanyStatus = Myds.Tables[0].Rows[i]["CompanyStatus"].ToString();
                    IncompleteStatus = Myds.Tables[0].Rows[i]["CompanyStatus"].ToString();
                    Inspectionstatus = Myds.Tables[0].Rows[i]["InspectionStatus"].ToString();
                    sb.Append("<tr><td align='left'  valign='top'> " + Myds.Tables[0].Rows[i]["CompanyName"].ToString() + " </td>");    // nOT CLOSE  this tr 
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["PANNO"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["BranchName"].ToString() + "</td>");
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
                           /* case "Inprocess":
                                if (Myds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Agency Selected")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Application Form sent to Inspection Agency for inspection.</b></td></tr>");
                                }
                                if (Myds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Request Generated")
                                {
                                    //show username and agency id
                                    DataSet ds_agency = new DataSet();
                                    int AgencyUserId = Convert.ToInt32(Myds.Tables[0].Rows[i]["AgencyUserid"].ToString());
                                    string AgencyName = "N.A";
                                    string AgencyUsername = "N.A";
                                    if (AgencyUserId != 0)
                                    {
                                        ds_agency = objCompany.Fetch_Inspection_Agency_and_user_name(AgencyUserId);
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
                                if (Myds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Inspection Date already Issued but Inspection is Pending")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2' valign='top'> <b>Inspection Date already issued but Inspection is pending.</b></td></tr>");
                                }
                                if (Myds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Inspection Complete")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td> Status:</td>");
                                    sb.Append("<td colspan='2'> <b>Inspection Complete.  Waiting for branch office approval for store details.</b></td></tr>");
                                }
                                if (Myds.Tables[0].Rows[i]["InspectionStatus"].ToString() == "Not complete Inspection")
                                {
                                    sb.Append("<tr>");
                                    sb.Append("<td valign='top'></td>");
                                    sb.Append("<td colspan='2'valign='top'> <b>Server Error.</b></td></tr>");
                                }

                                break;*/

                            case "Monetary Limit more than 10 crore":
                                sb.Append("<tr>");
                                sb.Append("<td Colspan='3'valign='top'><b> Waiting for approval by Branch User.</b></td></tr>");
                                sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                break;

                            case "VerifyPayment":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'valign='top'> <b> Waiting for verification of payment by Branch User.</b></td></tr>");                              
                                break;
                            case "InAccounts":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'valign='top'> <b> Waiting for ML fixation in Accounts Login.</b></td></tr>");
                                break;
                            case "RejectedApp":
                                sb.Append("<tr>");
                                sb.Append("<td valign='top'></td>");
                                sb.Append("<td colspan='2'valign='top'> <b> Application is rejected by Branch User.</b></td></tr>");
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
                                        sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by Branch Head  awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit >= Branch_headAutorised_id && Monetary_Limit <= Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Zonal Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        // sb.Append("<tr><td style='color:red;'><b> Approval by Zonal Head awaited<b> </td></tr>");
                                    }

                                    if (Monetary_Limit > Zonal_monatrylimitmax && ZonalAcceptStaus != "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by HO.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3' valign='top'><b>Monetary Limit set by Branch Accounts: Rs. " + Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
                                        //sb.Append("<tr><td style='color:red;'><b> Approval by HO HR awaited<b> </td></tr>");
                                    }
                                    if (Monetary_Limit > 0 && ZonalAcceptStaus == "ACCEPTED" && CompanyStatus != "Confirm")
                                    {
                                        sb.Append("<tr>");
                                        sb.Append("<td Colspan='3'valign='top'><b>Monetary Limit  has been set by Branch Account. Waiting for approval by Branch Head.</b></td></tr>");
                                        sb.Append("<tr><td colspan='3'valign='top' ><b> Monetary Limit set by Branch Accounts: Rs. " + Myds.Tables[0].Rows[i]["MonetaryLimit"].ToString() + "(in lakhs)</b></td></tr>");
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
                        sb.Append("<tr>");
                        sb.Append("<td valign='top'></td>");
                        sb.Append("<td colspan='2' valign='top'> <b>Expired</b></td></tr>");
                    }
                    sb.Append("</table>");
                    sb.Append("</td>");
                    sb.Append("<td align='left' valign='top'>" + Myds.Tables[0].Rows[i]["StatusDate"].ToString() + "</td>");
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptID = objDecEnc.Encrypt("1");
                    string EncryptCompany_Name = "";
                    string EncryptCompanyID = "";
                    string EncryptBranchName = "";
                    string imageid = string.Empty;
                    EncryptCompany_Name = objDecEnc.Encrypt(Myds.Tables[0].Rows[i]["CompanyName"].ToString());
                    EncryptCompanyID = objDecEnc.Encrypt(Myds.Tables[0].Rows[i]["CompanyId"].ToString());
                    EncryptBranchName = objDecEnc.Encrypt(Myds.Tables[0].Rows[i]["BranchName"].ToString());
                    HyperLink hyper = new HyperLink();
                   // hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                   // imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    //  sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[0]["RejectReson"].ToString() + "</td>");
                    //  hyper.Text = "View";
                    imageid = "RenewalformField.aspx?Company_ID=" + EncryptCompanyID + "&Com_name=" + EncryptCompany_Name;
                    hyper.Text = "View";
                 //   sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                    sb.Append("</tr>");

                }

                sb.Append("</table>");

                lbl_report_status_track.Text = sb.ToString();
            }
            else
            {
                lbl_report_status_track.Text = "<span style='color:red;'>" + "No Record found !!!" + "</span>";
                Myds.Dispose();

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }
    protected void ddl_list_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
