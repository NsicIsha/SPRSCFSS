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
using System.IO;
using System.Text;
using System.Diagnostics;
public partial class appAdministrator_PrintPaymentDetails : System.Web.UI.Page
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
    decimal Registration_amount;
    decimal Inspection_amount;
    decimal Amedment_amount;
    decimal Renewal_amount;
    decimal Total_amount;

    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Ctype = Request.QueryString["Ctype"].ToString();
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
            if (Request.QueryString["CompanyID"] == "0" || Request.QueryString["CompanyID"] == "NaN")
            {
                companyd = 0;
            }
            else
            {
                companyd = Convert.ToInt32(Request.QueryString["CompanyID"].ToString());
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
         ShowOrderGrid();
    }
    public void ShowOrderGrid()
    {
        try
        {
            if (Ctype == "--Select--" && datefrom != "" && dateto != "" && zoneid == 0 && branchid == 0 && companyd == 0)
            {
                Bind_Branch_Wise();
                grd_Company_wiseData.Visible = false;
                grd_report_paymentDetails.Visible = false;
            }
            else if (Ctype == "Registration" && datefrom != "" && dateto != "" && zoneid == 0 && branchid == 0 && companyd == 0)
            {
                grd_Branch_Wise.Columns[3].Visible = false;
                grd_Branch_Wise.Columns[4].Visible = false;
                grd_Branch_Wise.Columns[5].Visible = false;
                grd_Branch_Wise.Columns[6].Visible = false;
                Bind_Branch_Wise();

            }
            else if (Ctype == "Inspection" && datefrom != "" && dateto != "" && zoneid == 0 && branchid == 0 && companyd == 0)
            {
                grd_Branch_Wise.Columns[2].Visible = false;
                grd_Branch_Wise.Columns[3].Visible = true;
                grd_Branch_Wise.Columns[4].Visible = false;
                grd_Branch_Wise.Columns[5].Visible = false;
                grd_Branch_Wise.Columns[6].Visible = false;
                Bind_Branch_Wise();

            }
            else if (datefrom != "" && dateto != "" && zoneid != 0 && branchid == 0 && companyd == 0)
            {
                Bind_Branch_Wise();
                grd_Company_wiseData.Visible = false;
                grd_report_paymentDetails.Visible = false;
            }
            else if (datefrom != "" && dateto != "" && zoneid != 0 && branchid != 0 && companyd == 0)
            {
                Bind_GridView_BranchZoneWise();
                grd_Branch_Wise.Visible = false;
                grd_report_paymentDetails.Visible = false;

            }
            else if (datefrom != "" && dateto != "" && zoneid != 0 && branchid != 0 && companyd != 0)
            {
                Bind_Payment_Details();
                grd_Branch_Wise.Visible = false;
                grd_Company_wiseData.Visible = false;

            }
            else if (datefrom == "" && dateto == "" && zoneid != 0 && branchid == 0 && companyd == 0)
            {
                Bind_Branch_Wise();
                grd_Company_wiseData.Visible = false;
                grd_report_paymentDetails.Visible = false;
            }
            else if (datefrom == "" && dateto == "" && zoneid != 0 && branchid != 0 && companyd == 0)
            {
                Bind_GridView_BranchZoneWise();
                grd_Branch_Wise.Visible = false;
                grd_report_paymentDetails.Visible = false;
            }
            else if (datefrom == "" && dateto == "" && zoneid != 0 && branchid != 0 && companyd != 0)
            {
                Bind_Payment_Details();
                grd_Branch_Wise.Visible = false;
                grd_Company_wiseData.Visible = false;
            }
            else
            {
                Bind_Branch_Wise();
                grd_Company_wiseData.Visible = false;
                grd_report_paymentDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public void Bind_GridView_BranchZoneWise()
    {
        try
        {
            objcompany = new clsCompany();
            DataSet Myds_company = new DataSet();
            Myds_company = new DataSet();
            string Orderby = "";
            if (Request.QueryString["Orderby"].ToString() == "1")
            {
                Orderby = "1";
            }
            if (Request.QueryString["Orderby"].ToString() == "2")
            {
                Orderby = "2";
            }
            if (Request.QueryString["Orderby"].ToString() == "3")
            {
                Orderby = "3";
            }
            if (datefrom != "" && dateto != "")
            {
                //Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise(zoneid, branchid, datefrom, dateto);
                Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(zoneid, branchid, datefrom, dateto, Orderby);
            }
            else
            {
                Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified(zoneid, branchid, Orderby);
            }
            if (Myds_company.Tables[0].Rows.Count > 0)
            {
                grd_Company_wiseData.DataSource = Myds_company.Tables[0];
                grd_Company_wiseData.DataBind();
            }
            else
            {

                grd_Company_wiseData.Dispose();
                grd_Company_wiseData.DataBind();
                Myds_company.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    // Branch wise-------------
    public void Bind_Branch_Wise()
    {
        try
        {

            objcompany = new clsCompany();
            DataSet Ds_Branchwise = new DataSet();
            if (datefrom != null && dateto != null && zoneid == 0)
            {
              //  Ds_Branchwise = objcompany.Company_paymentetails_Pass_value_fromdate(datefrom, dateto);
               // Ds_Branchwise = objcompany.Company_paymentetails_Pass_value_fromdate_Modified(datefrom, dateto);
                Ds_Branchwise = objcompany.Fetch_PaymentData(datefrom, dateto);
                Ds_Branchwise.Tables[0].DefaultView.Sort = Ds_Branchwise.Tables[0].Columns["ZoneName"].ToString() + " " + "ASC";
                grd_Branch_Wise.Columns[1].Visible = false;
            }

            else
            {
                grd_Branch_Wise.Columns[0].Visible = false;
                //Ds_Branchwise = objcompany.Company_PaymentDetails_Fetch_ZoneWise(datefrom, dateto, zoneid);
                //Ds_Branchwise = objcompany.Company_PaymentDetails_Fetch_ZoneWise(datefrom, dateto, zoneid);
                Ds_Branchwise = objcompany.Fetch_PaymentData_BranchWise(zoneid, datefrom, dateto);
                Ds_Branchwise.Tables[0].DefaultView.Sort = Ds_Branchwise.Tables[0].Columns["BranchName"].ToString() + " " + "ASC";

            }
            if (Ds_Branchwise.Tables[0].Rows.Count > 0)
            {
                grd_Branch_Wise.DataSource = Ds_Branchwise.Tables[0];
                grd_Branch_Wise.DataBind();

            }
            else
            {



                // Report_Type.Text = "";

                grd_Branch_Wise.Dispose();
                grd_Branch_Wise.DataBind();
                //Ds_Branchwise.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        }
   

// comapny wise---

    public void Bind_Payment_Details()
    {





        try
        {

            objcompany = new clsCompany();
            DataSet dsgridviewPayment = new DataSet();

            dsgridviewPayment = objcompany.Company_PaymentDetails_Modified(datefrom, dateto, branchid, zoneid, companyd);
            if (dsgridviewPayment.Tables[0].Rows.Count > 0)
            {


                grd_report_paymentDetails.DataSource = dsgridviewPayment.Tables[0];
                grd_report_paymentDetails.DataBind();

            }
            else
            {

                dsgridviewPayment.Dispose();

                grd_report_paymentDetails.Dispose();
                grd_report_paymentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void grd_GridCompany_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (Ctype == "Registration")
            {
                if (dateto != "" && datefrom != "" && zoneid == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto != "" && datefrom != "" && zoneid != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[6].Visible = false;

                }
            }

            else if (Ctype == "Inspection")
            {
                if (dateto != "" && datefrom != "" && zoneid == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto == "" && datefrom == "" && zoneid != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = true;
                }
                else if (dateto != "" && datefrom != "" && zoneid != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {




                this.intCurrentRecordNumber++;
                if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
                {
                    e.Row.Attributes.Add("style", @"page-break-after:always;");
                    this.intCurrentRecordNumber = 0;
                }

                Label lbl_registration = (Label)e.Row.FindControl("lblRegiatrationAmmount1");
                if (lbl_registration.Text == "" || lbl_registration.Text == null)
                {
                    lbl_registration.Text = "0.0";
                }
                Label lbl_inspection = (Label)e.Row.FindControl("lbl_unit_ammount1");
                if (lbl_inspection.Text == "" || lbl_inspection.Text == null)
                {
                    lbl_inspection.Text = "0.0";
                }
                Label lbl_amedment = (Label)e.Row.FindControl("lbl_amedment");
                if (lbl_amedment.Text == "" || lbl_amedment.Text == null)
                {
                    lbl_amedment.Text = "0.0";
                }
                Label lbl_renewal_amount = (Label)e.Row.FindControl("lbl_Renewal");
                if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
                {
                    lbl_renewal_amount.Text = "0.0";
                }

                Label lbl_total_amount = (Label)e.Row.FindControl("lblTotalAmmount");
                if (lbl_total_amount.Text == "" || lbl_total_amount.Text == null || lbl_total_amount.Text == "0.00")
                {
                    decimal totalrecord;
                    totalrecord = Convert.ToDecimal(lbl_registration.Text.ToString()) + Convert.ToDecimal(lbl_inspection.Text.ToString()) + Convert.ToDecimal(lbl_amedment.Text.ToString()) + Convert.ToDecimal(lbl_renewal_amount.Text.ToString());
                    lbl_total_amount.Text = totalrecord.ToString();
                }
                else
                {
                    decimal totalrecord;
                    totalrecord = Convert.ToDecimal(lbl_registration.Text.ToString()) + Convert.ToDecimal(lbl_inspection.Text.ToString()) + Convert.ToDecimal(lbl_amedment.Text.ToString()) + Convert.ToDecimal(lbl_renewal_amount.Text.ToString());
                    lbl_total_amount.Text = totalrecord.ToString();
                }
                Registration_amount += Convert.ToDecimal(lbl_registration.Text);
                Inspection_amount += Convert.ToDecimal(lbl_inspection.Text);
                Amedment_amount += Convert.ToDecimal(lbl_amedment.Text);
                Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
                Total_amount += Convert.ToDecimal(lbl_total_amount.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_registration_footer = (Label)e.Row.FindControl("lblRegistrationTotal1");
                Label lbl_inspection_footer = (Label)e.Row.FindControl("lblTotal1");
                Label lbl_Amedment_footer = (Label)e.Row.FindControl("lbl_amedment_footer");
                Label LBL_renewal_footer = (Label)e.Row.FindControl("lbl_footer_renewal");
                Label lbl_total_footer = (Label)e.Row.FindControl("lblTotalAmmountbranchwise");

                lbl_registration_footer.Text = Convert.ToString(Registration_amount);
                lbl_inspection_footer.Text = Convert.ToString(Inspection_amount);
                lbl_Amedment_footer.Text = Convert.ToString(Amedment_amount);
                LBL_renewal_footer.Text = Convert.ToString(Renewal_amount);
                lbl_total_footer.Text = Convert.ToString(Total_amount);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void onRowGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grdBranch_wise_dataBound(object sender, EventArgs e)
    {
          // To write Total Branch_wise _Data
        try
        {
            if (grd_Branch_Wise.Rows.Count > 0)
            {
                Label lblTotalAmount1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblRegistrationTotal1");
                lblTotalAmount1.Text = String.Format("{0:n2}",
                                        grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount1")).Text)));

                Label lblTotal1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblTotal1");
                lblTotal1.Text = String.Format("{0:n2}",
                                        grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount1")).Text)));
                Label lbltotalbranchwise1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblTotalAmmountbranchwise");
                lbltotalbranchwise1.Text = (Convert.ToDecimal(lblTotalAmount1.Text) + Convert.ToDecimal(lblTotal1.Text)).ToString();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
  

    }
    protected void grdCompany_Wise_Data(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (Ctype == "Registration")
            {
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[5].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;
                grd_Company_wiseData.Columns[6].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            else if (Ctype == "Inspection")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
                grd_Company_wiseData.Columns[5].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;
                grd_Company_wiseData.Columns[6].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }

            else if (Ctype == "Amendent")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;

                grd_Company_wiseData.Columns[6].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            else if (Ctype == "Renewal")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;

                grd_Company_wiseData.Columns[5].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;

            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                this.intCurrentRecordNumber++;
                if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
                {
                    e.Row.Attributes.Add("style", @"page-break-after:always;");
                    this.intCurrentRecordNumber = 0;
                }

                Label lbl_registration = (Label)e.Row.FindControl("lblRegiatrationAmmount2");
                if (lbl_registration.Text == "" || lbl_registration.Text == null)
                {
                    lbl_registration.Text = "0.0";
                }
                Label lbl_inspection = (Label)e.Row.FindControl("lbl_unit_ammount2");
                if (lbl_inspection.Text == "" || lbl_inspection.Text == null)
                {
                    lbl_inspection.Text = "0.0";
                }
                Label lbl_amedment = (Label)e.Row.FindControl("lbl_amedment_amount");
                if (lbl_amedment.Text == "" || lbl_amedment.Text == null)
                {
                    lbl_amedment.Text = "0.0";
                }
                Label lbl_renewal = (Label)e.Row.FindControl("lbl_renewal");
                if (lbl_renewal.Text == "" || lbl_renewal.Text == null)
                {
                    lbl_renewal.Text = "0.0";
                }
                Label lbl_total_amount = (Label)e.Row.FindControl("lbl_total_amount");
                if (lbl_total_amount.Text == null || lbl_total_amount.Text == "" || lbl_total_amount.Text=="0")
                {
                    decimal total_amount;
                    total_amount = Convert.ToDecimal(lbl_registration.Text.ToString()) + Convert.ToDecimal(lbl_inspection.Text.ToString()) + Convert.ToDecimal(lbl_amedment.Text.ToString()) + Convert.ToDecimal(lbl_renewal.Text.ToString());
                    lbl_total_amount.Text = total_amount.ToString();
                }
                Registration_amount += Convert.ToDecimal(lbl_registration.Text);
                Inspection_amount += Convert.ToDecimal(lbl_inspection.Text);
                Amedment_amount += Convert.ToDecimal(lbl_amedment.Text);
                Renewal_amount += Convert.ToDecimal(lbl_renewal.Text);
                Total_amount += Convert.ToDecimal(lbl_total_amount.Text);

            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_registration_footer = (Label)e.Row.FindControl("lblRegistrationTotal2");
                Label lbl_inspection_footer = (Label)e.Row.FindControl("lblTotal2");
                Label lbl_Amedment_footer = (Label)e.Row.FindControl("lbl_footer_amedment");
                Label LBL_renewal_footer = (Label)e.Row.FindControl("lbl_footer_renewal");
                Label lbl_total_footer = (Label)e.Row.FindControl("lblTotal_companywise");

                lbl_registration_footer.Text = Convert.ToString(Registration_amount);
                lbl_inspection_footer.Text = Convert.ToString(Inspection_amount);
                lbl_Amedment_footer.Text = Convert.ToString(Amedment_amount);
                LBL_renewal_footer.Text = Convert.ToString(Renewal_amount);
                lbl_total_footer.Text = Convert.ToString(Total_amount);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void grdcompany_branchDatabound(object sender, EventArgs e)
    {
        try
        {
            if (grd_Company_wiseData.Rows.Count > 0)
            {
                Label lblTotalAmount2 = (Label)grd_Company_wiseData.FooterRow.FindControl("lblRegistrationTotal2");
                lblTotalAmount2.Text = String.Format("{0:n2}",
                                        grd_Company_wiseData.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount2")).Text)));

                Label lblTotal2 = (Label)grd_Company_wiseData.FooterRow.FindControl("lblTotal2");
                lblTotal2.Text = String.Format("{0:n2}",
                                        grd_Company_wiseData.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount2")).Text)));


                Label lbltotalCompanyWise = (Label)grd_Company_wiseData.FooterRow.FindControl("lblTotal_companywise");
                lbltotalCompanyWise.Text = (Convert.ToDecimal(lblTotalAmount2.Text) + Convert.ToDecimal(lblTotal2.Text)).ToString();


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }



    }

    protected void grd_report_onRowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void grd_Report_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (Ctype == "Registration")
            {
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;
                grd_report_paymentDetails.Columns[7].Visible = false;

            }
            else if (Ctype == "Inspection")
            {
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;
                grd_report_paymentDetails.Columns[7].Visible = false;
            }

            else if (Ctype == "Renewal")
            {
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;

                grd_report_paymentDetails.Columns[7].Visible = false;
            }
            else if (Ctype == "Amendent")
            {
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;

                grd_report_paymentDetails.Columns[7].Visible = false;
            }


            this.intCurrentRecordNumber++;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
                {
                    e.Row.Attributes.Add("style", @"page-break-after:always;");
                    this.intCurrentRecordNumber = 0;
                }

                Label lbl_registration_amount = (Label)e.Row.FindControl("lblRegiatrationAmmount");
                if (lbl_registration_amount.Text == "" || lbl_registration_amount.Text == null)
                {
                    lbl_registration_amount.Text = "0.0";
                }
                Label lbl_inspection_amount = (Label)e.Row.FindControl("lbl_unit_ammount");
                if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
                {
                    lbl_inspection_amount.Text = "0.0";
                }
                Label lbl_amedment_amount = (Label)e.Row.FindControl("lblAmedmentamount");
                if (lbl_amedment_amount.Text == "" || lbl_amedment_amount.Text == null)
                {
                    lbl_amedment_amount.Text = "0.0";
                }
                Label lblRenewalAmount = (Label)e.Row.FindControl("lblRenewalAmount");
                if (lblRenewalAmount.Text == "" || lblRenewalAmount.Text == null)
                {
                    lblRenewalAmount.Text = "0.0";
                }
                Label lbl_total_amount = (Label)e.Row.FindControl("lbltotalamount");
                if (lbl_total_amount.Text == "" || lbl_total_amount.Text == null || lbl_total_amount.Text=="0")
                {
                    decimal TotalAmount;
                    TotalAmount = Convert.ToDecimal(lbl_registration_amount.Text.ToString()) + Convert.ToDecimal(lbl_inspection_amount.Text.ToString()) + Convert.ToDecimal(lbl_amedment_amount.Text.ToString()) + Convert.ToDecimal(lblRenewalAmount.Text);
                    lbl_total_amount.Text = TotalAmount.ToString();
                }


                Registration_amount += Convert.ToDecimal(lbl_registration_amount.Text);
                Inspection_amount += Convert.ToDecimal(lbl_inspection_amount.Text);
                Amedment_amount += Convert.ToDecimal(lbl_amedment_amount.Text);
                Renewal_amount += Convert.ToDecimal(lblRenewalAmount.Text);
                Total_amount += Convert.ToDecimal(lbl_total_amount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_registration_footer = (Label)e.Row.FindControl("lblRegistrationTotal");
                Label lbl_inspection_footer = (Label)e.Row.FindControl("lblTotal");
                Label lbl_Amedment_footer = (Label)e.Row.FindControl("lbl_amendment_total");
                Label LBL_renewal_footer = (Label)e.Row.FindControl("lbl_renewal_amountFooter");
                Label lbl_total_footer = (Label)e.Row.FindControl("lbltotalfooter");

                lbl_registration_footer.Text = Convert.ToString(Registration_amount);
                lbl_inspection_footer.Text = Convert.ToString(Inspection_amount);
                lbl_Amedment_footer.Text = Convert.ToString(Amedment_amount);
                LBL_renewal_footer.Text = Convert.ToString(Renewal_amount);
                lbl_total_footer.Text = Convert.ToString(Total_amount);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void grd_report_paymentDetails_DataBound(object sender, EventArgs e)
    {
        try
        {
            if (grd_report_paymentDetails.Rows.Count > 0)
            {
                Label lblTotalAmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblRegistrationTotal");
                lblTotalAmount.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount")).Text)));

                Label lblTotal = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblTotal");
                lblTotal.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount")).Text)));
                Label lbl_Amedment = (Label)grd_report_paymentDetails.FooterRow.FindControl("lbl_amendment_total");
                //Label lbl_Amedment_amount = (Label)grd_report_paymentDetails.FindControl("lblAmedmentamount");


                //if (lbl_Amedment_amount.Text == "" || lbl_Amedment_amount.Text == null)
                //{
                //    lbl_Amedment_amount.Text = Convert.ToDecimal("0.0").ToString();   //Convert.ToDecimal(((Label)a.FindControl("lblAmedmentamount"))
                //}
                //lbl_Amedment.Text = String.Format("{0:n2}",
                //                                 grd_report_paymentDetails.Rows.Cast<GridView>()
                //                                  .Sum(a=>Convert.ToDecimal(lbl_Amedment_amount.Text)));

                //Label lbl_Renewal_Amount = (Label)grd_report_paymentDetails.FindControl("lblRenewalAmount");
                //if (lbl_Renewal_Amount.Text == "" || lbl_Renewal_Amount.Text == null)
                //{
                //    lbl_Renewal_Amount.Text = Convert.ToDecimal("0.0").ToString(); 
                //}
                //Label lbl_Renewal = (Label)grd_report_paymentDetails.FooterRow.FindControl("lbl_renewal_amountFooter");
                //lbl_Renewal.Text = String.Format("{0:n2}",
                //                              grd_report_paymentDetails.Rows.Cast<GridView>()
                //                               .Sum(a => Convert.ToDecimal(lbl_Renewal_Amount.Text.ToString())));

                  
                Label labelTotalAmmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lbltotalfooter");
                labelTotalAmmount.Text = (Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(lblTotal.Text)).ToString();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
