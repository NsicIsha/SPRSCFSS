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
public partial class appAdministrator_BranchWiseSearch : System.Web.UI.Page
{
    clsCompany objcompany;
    DataSet dsbranch_grid;

    DataTable DTZone;
    decimal Registartion_amount;
    decimal Inspection_amount;
    decimal Renewal_Amount;
    decimal Amedment_amount;
    decimal total_amount;
    decimal Registartion_amount_1;
    decimal Inspection_amount_1;
    decimal Renewal_Amount_1;
    decimal Amedment_amount_1;

    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ZName"].ToString() != "" && Request.QueryString["ZName"].ToString()!=null) 
        {
            lblReportName.Text = Request.QueryString["ZName"].ToString();
        }
        else
        {
            lblReportName.Text = "";
        }
        if (!IsPostBack)
        {
            bindGrid();
        }

    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["ZName"].ToString() == "")
        {
            Response.Redirect("SpecificListOfReportPage.aspx");
        }
        else
        {
            Response.Redirect("ListOfReport.aspx");
        }
    }

    public void bindGrid()
    {

        int ZonID = Convert.ToInt32(Request.QueryString["id1"].ToString());
        string dateFrom = Request.QueryString["datefrom"];
        string dateto = Request.QueryString["dateTo"];
        objcompany = new clsCompany();
        dsbranch_grid = new DataSet();
        //dsbranch_grid = objcompany.Company_PaymentDetails_Fetch_ZoneWise(dateFrom, dateto, ZonID);
        //if (dateFrom != "" && dateto != "" && ZonID !=0)
       // {
            dsbranch_grid = objcompany.Fetch_PaymentData_BranchWise(ZonID, dateFrom, dateto);
            dsbranch_grid.Tables[0].DefaultView.Sort = dsbranch_grid.Tables[0].Columns["BranchName"].ToString() + " " + "ASC";
       // }
       
        if (dsbranch_grid.Tables[0].Rows.Count > 0)
        {
            grd_Branch_Wise.DataSource = dsbranch_grid.Tables[0];
            grd_Branch_Wise.DataBind();
        }
        else
        {
            grd_Branch_Wise.Dispose();
            grd_Branch_Wise.DataBind();
           // dsbranch_grid.Dispose();
        }
       

    }

    protected void grd_GridCompany_onRowdataBound(object sender, GridViewRowEventArgs e)
    {

        int ZonID = Convert.ToInt32(Request.QueryString["id1"].ToString());
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            decimal total_amount_add = Convert.ToDecimal("0.00");
            Label lblBranchID = (Label)e.Row.FindControl("lbl_Branch_ID");
            int branchid1 = Convert.ToInt32(lblBranchID.Text.ToString());
            string dateFrom = Request.QueryString["datefrom"];
            string dateto = Request.QueryString["dateTo"];
            //string branchid1="BranchPage";
            Label lblBrancName = (Label)e.Row.FindControl("lnk_Branc_Name");
            string url = "CompanyWiseRecord.aspx?id=" + branchid1 + "&ZoneID=" + ZonID + "&datefrom=" + dateFrom + "&dateTo=" + dateto + "&ZName=" + lblReportName.Text;

            lblBrancName.Text = "<a href='" + url + "' >" + lblBrancName.Text + "</a>";
            this.intCurrentRecordNumber++;
            if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
            {
                e.Row.Attributes.Add("style", @"page-break-after:always;");
                this.intCurrentRecordNumber = 0;
            }

            Label lbl_registration = (Label)e.Row.FindControl("lblregistrationAmmount");
            Label lbl_amedment = (Label)e.Row.FindControl("lblinspectionammount");
            Label lbl_inspection_amount = (Label)e.Row.FindControl("lbl_Renewal");
            Label lbl_renewal_amount = (Label)e.Row.FindControl("lbl_amedment");
            Label lbl_total_amount = (Label)e.Row.FindControl("lblTotalAmount");
            if (lbl_registration.Text == "" || lbl_registration.Text == "")
            {
                lbl_registration.Text = "0.0";
                Registartion_amount_1 = Convert.ToDecimal(lbl_registration.Text);
            }
            else
            {
                Registartion_amount_1 = Convert.ToDecimal(lbl_registration.Text);
            }
            if (lbl_amedment.Text == "" || lbl_amedment.Text == null)
            {
                lbl_amedment.Text = "0.0";
                Amedment_amount_1 = Convert.ToDecimal(lbl_amedment.Text);
            }
            else
            {
                Amedment_amount_1 = Convert.ToDecimal(lbl_amedment.Text);
            }
            if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
            {
                lbl_renewal_amount.Text = "0.0";
                Renewal_Amount_1 = Convert.ToDecimal(lbl_renewal_amount.Text);
            }
            else
            {
                Renewal_Amount_1 = Convert.ToDecimal(lbl_renewal_amount.Text);
            }
            if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
            {
                lbl_inspection_amount.Text = "0.0";
                Inspection_amount_1 = Convert.ToDecimal(lbl_inspection_amount.Text);
            }
            else
            {
                Inspection_amount_1 = Convert.ToDecimal(lbl_inspection_amount.Text);
            }
            total_amount_add = Convert.ToDecimal(Registartion_amount_1 + Inspection_amount_1 + Amedment_amount_1 + Renewal_Amount_1);
            Registartion_amount += Convert.ToDecimal(lbl_registration.Text);
            Inspection_amount += Convert.ToDecimal(lbl_inspection_amount.Text);
            Amedment_amount += Convert.ToDecimal(lbl_amedment.Text);
            Renewal_Amount += Convert.ToDecimal(lbl_renewal_amount.Text);
            total_amount = Convert.ToDecimal(Registartion_amount + Inspection_amount + Amedment_amount + Renewal_Amount);

            lbl_total_amount.Text = total_amount_add.ToString();
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl_registration_foter = (Label)e.Row.FindControl("lblRegistrationTotal");
            Label lbl_Amedment_footer = (Label)e.Row.FindControl("lbl_footer_amedment");
            Label lbl_inspection_footer = (Label)e.Row.FindControl("lbltotalinspection");
            Label lbl_Renewal_footer = (Label)e.Row.FindControl("lbl_renewal_total");
            Label lbl_total_amount_footer = (Label)e.Row.FindControl("totalAmmount");

            if (lbl_registration_foter.Text == "" || lbl_registration_foter.Text == null)
            {
                lbl_registration_foter.Text = Registartion_amount.ToString();
            }
            if (lbl_Amedment_footer.Text == "" || lbl_Amedment_footer.Text == null)
            {
                lbl_Amedment_footer.Text = Renewal_Amount.ToString();
            }
            if (lbl_Renewal_footer.Text == "" || lbl_Renewal_footer.Text == null)
            {
                lbl_Renewal_footer.Text = Inspection_amount.ToString(); //Renewal_Amount.ToString();
            }
            if (lbl_inspection_footer.Text == "" || lbl_inspection_footer.Text == null)
            {
                lbl_inspection_footer.Text = Amedment_amount.ToString();
            }
            if (lbl_total_amount_footer.Text == "" || lbl_total_amount_footer.Text == null)
            {
                lbl_total_amount_footer.Text = total_amount.ToString();
            }
        }
    }
    /*
    protected void grdbranchWise_Data(object sender, EventArgs e)
    {
        try
        {
            if (grd_Branch_Wise.Rows.Count > 0)
            {
                Label lblTotalAmount2 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblRegistrationTotal");
                lblTotalAmount2.Text = String.Format("{0:n2}",
                                        grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblregistrationAmmount")).Text)));

                Label lblTotal2 = (Label)grd_Branch_Wise.FooterRow.FindControl("lbltotalinspection");
                lblTotal2.Text = String.Format("{0:n2}",
                                       grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblinspectionammount")).Text)));
                Label lbltotalCompanyWise = (Label)grd_Branch_Wise.FooterRow.FindControl("totalAmmount");
                lbltotalCompanyWise.Text = (Convert.ToDecimal(lblTotalAmount2.Text) + Convert.ToDecimal(lblTotal2.Text)).ToString();


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
     */
}
