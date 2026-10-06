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

public partial class appAdministrator_CompanyWiseRecord : System.Web.UI.Page
{
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    int accessId;
    int count = 1;
    decimal Registration_Amount;
    decimal Inspection_amount;
    decimal Renewal_amount;
    decimal Amedment_amount;
    decimal total_amount;
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Request.QueryString["BName"].ToString() != "")
        //{
        //if (Request.QueryString["ZName"].ToString() != null)
        //{
        //    lblZoneName.Text = Request.QueryString["ZName"].ToString();
        //}
        //else
        //{
        //    lblZoneName.Text = "";
        //}
        //}
        //else
        //{
        //    lblReportName.Text = "";
        //}
        try
        {
            int BranchIDGet = Convert.ToInt32(Request.QueryString["id"]);
            objcompany = new clsCompany();
            DataSet ds_branch_get = new DataSet();
            ds_branch_get = objcompany.Company_PaymentDetails_BranchName(BranchIDGet);
            if (ds_branch_get.Tables[0].Rows.Count > 0)
            {
                lblReportName.Text = ds_branch_get.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                lblReportName.Text = "";
            }
            Bind_GridView();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    // Changes by Ravish Kumar Pandey 13 Feb 2013
    public void Bind_GridView()
    {
        try
        {
            int ZoneID = Convert.ToInt32(Request.QueryString["ZoneID"]);
            int BranchID = Convert.ToInt32(Request.QueryString["id"]);
            string Fromdate = "";
            string ToDate = "";
            if (Request.QueryString.HasKeys())
            {
                Fromdate = Convert.ToString(Request.QueryString["DateFrom"].ToString());
                ToDate = Convert.ToString(Request.QueryString["DateTo"].ToString());
            }
            objcompany = new clsCompany();
            Myds = new DataSet();
            //Myds = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified(ZoneID, BranchID);
           Myds = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_datetime_Modified(ZoneID, BranchID, Fromdate, ToDate);
         // Myds = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(ZoneID, BranchID, Fromdate, ToDate);
           // Myds=objcompany.Fetch_PaymentData_BranchWise(ZoneID, 
            if (Myds.Tables[0].Rows.Count > 0)
            {
                grd_Company_wise.DataSource = Myds.Tables[0];
                grd_Company_wise.DataBind();
            }
            else
            {
                grd_Company_wise.Dispose();
                grd_Company_wise.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
          
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        try
        {
            //  string Page = Request.QueryString["Page"].ToString();
            int ZoneID = Convert.ToInt32(Request.QueryString["ZoneID"]);
            int BranchID = Convert.ToInt32(Request.QueryString["id"]);
            lblid.Text = Request.QueryString["id"].ToString();
            int id1 = Convert.ToInt32(lblid.Text.ToString());
            lbldatefrom.Text = Request.QueryString["datefrom"];
            lblto.Text = Request.QueryString["dateto"];
            string ZName = "";
            //  lbldatefrom=
            //if (lbldatefrom.Text!="" && lblto.Text!="")
            //{
            // +"&ZName=" + lblZoneName.Text;
            Response.Redirect("BranchWiseSearch.aspx?id1=" + ZoneID + "&datefrom=" + lbldatefrom.Text + "&dateto=" + lblto.Text + "&ZName=" + ZName);
            // }
            //  else
            // {
            //Response.Redirect("ListOfReport.aspx");
            // }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void Grd_Report_companyWise(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblcompanyid = (Label)e.Row.FindControl("lblCompanyId");
            Label lblcompanyName = (Label)e.Row.FindControl("lblCompanyName");
            int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
            string CompanyName = lblcompanyName.Text;
            string Page = "Comapny";
            int ZoneID = Convert.ToInt32(Request.QueryString["ZoneID"]);
            int BranchID = Convert.ToInt32(Request.QueryString["id"]);
            string url = "CompanyWiseSeparateRecord.aspx?cid=" + companyid + "&cname=" + CompanyName + "&Page=" + Page + "&ZoneID=" + ZoneID + "&id=" +BranchID  ;

            lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";
            this.intCurrentRecordNumber++;
            if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
            {
                e.Row.Attributes.Add("style", @"page-break-after:always;");
                this.intCurrentRecordNumber = 0;
            }
            Label lbl_Registration_Amount = (Label)e.Row.FindControl("lblregistration");
            if (lbl_Registration_Amount.Text == "" || lbl_Registration_Amount.Text == null)
            {
                lbl_Registration_Amount.Text = "0.0";   
            }
            Label lbl_inspection_amount = (Label)e.Row.FindControl("lblInspection");
            if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
            {
                lbl_inspection_amount.Text = "0.0";
            }
            Label lbl_Amedment_amount = (Label)e.Row.FindControl("lbl_amedment_amount");
            if (lbl_Amedment_amount.Text == "" || lbl_Amedment_amount.Text == null)
            {
                lbl_Amedment_amount.Text = "0.0";
            }
            Label lbl_renewal_amount = (Label)e.Row.FindControl("lblrenewalamount");
            if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
            {
                lbl_renewal_amount.Text = "0.0";
            }

            Label lbl_total_amount = (Label)e.Row.FindControl("lblTotalAmount");
            if (lbl_total_amount.Text == "0.00" || lbl_total_amount.Text == null)
            {

                decimal totalbranchwise;
                totalbranchwise = Convert.ToDecimal(lbl_Registration_Amount.Text.ToString()) + Convert.ToDecimal(lbl_inspection_amount.Text.ToString()) + Convert.ToDecimal(lbl_Amedment_amount.Text.ToString()) + Convert.ToDecimal(lbl_renewal_amount.Text.ToString());
                lbl_total_amount.Text = totalbranchwise.ToString();

            }
            Registration_Amount += Convert.ToDecimal(lbl_Registration_Amount.Text);
            Inspection_amount += Convert.ToDecimal(lbl_inspection_amount.Text);
            Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
            Amedment_amount += Convert.ToDecimal(lbl_Amedment_amount.Text);
            total_amount += Convert.ToDecimal(lbl_total_amount.Text);

            
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl_registration_footer = (Label)e.Row.FindControl("lblTotalRegistration");
            Label lbl_inspection_footer = (Label)e.Row.FindControl("lblinspectionTotal");
            Label lbl_Amedment_Footer = (Label)e.Row.FindControl("lbl_amedment_footer");
            Label lbl_renewal_footer = (Label)e.Row.FindControl("lbl_renewal_footer");
            Label lbl_total_footer = (Label)e.Row.FindControl("lbltotalregistration_inspaction");
            lbl_registration_footer.Text = Convert.ToString(Registration_Amount);
            lbl_inspection_footer.Text = Convert.ToString(Inspection_amount);
            lbl_Amedment_Footer.Text = Convert.ToString(Amedment_amount);
            lbl_renewal_footer.Text = Convert.ToString(Renewal_amount);
            lbl_total_footer.Text = Convert.ToString(total_amount);

        }
    }
    /*
    protected void company_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (grd_Company_wise.Rows.Count > 0)
            {
                Label lblTotalAmount2 = (Label)grd_Company_wise.FooterRow.FindControl("lblTotalRegistration");
                Label lblRegistration_Amount = (Label)grd_Company_wise.FindControl("lblregistration");
                if (lblRegistration_Amount.Text == "")
                {
                    lblRegistration_Amount.Text = "0.0";
                }
                else
                {
                }

                lblTotalAmount2.Text = String.Format("{0:n2}",
                                        grd_Company_wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(lblRegistration_Amount.Text)));
                 

                Label lblTotal2 = (Label)grd_Company_wise.FooterRow.FindControl("lblinspectionTotal");
                Label lbl_Inspection_amount = (Label)grd_Company_wise.FindControl("lblInspection");
                if (lbl_Inspection_amount.Text == "")
                {
                    lbl_Inspection_amount.Text = "0.0";
                }
                lblTotal2.Text = String.Format("{0:n2}",
                                       grd_Company_wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(lbl_Inspection_amount.Text)));
                Label lbltotalCompanyWise = (Label)grd_Company_wise.FooterRow.FindControl("lbltotalregistration_inspaction");
                lbltotalCompanyWise.Text = (Convert.ToDecimal(lblTotalAmount2.Text) + Convert.ToDecimal(lblTotal2.Text)).ToString();


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);   
        }


    }*/
}
