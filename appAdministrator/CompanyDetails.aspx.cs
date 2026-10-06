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

public partial class appAdministrator_CompanyDetails : System.Web.UI.Page
{
    clsCompany objcompany;

    DataSet ds;
     int CompanyId;
     int  TotalPrint =0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblReportName.Text = Request.QueryString["cname"].ToString();
       CompanyId=Convert.ToInt32(Request.QueryString["cid"].ToString());
       BindGrid();
       lbl_number_ofprint.Text = Convert.ToString( TotalPrint);
    }
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("ListOfReport.aspx");
    }

    public void BindGrid()
    {
        objcompany = new clsCompany();
        ds = new DataSet();
        CompanyId = Convert.ToInt32(Request.QueryString["cid"].ToString());
        ds = objcompany.Company_PrintAcess_Certificate_companywise(CompanyId);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_print_company.DataSource = ds.Tables[0];
            grd_print_company.DataBind();
        }
        else
        {
            grd_print_company.Dispose();
            grd_print_company.DataBind();
            ds.Dispose();
        }
    }
    protected void grdDataBound(object sender, EventArgs e)
    {

        if (grd_print_company.Rows.Count > 0)
        {
            Label lblTotalAmount2 = (Label)grd_print_company.FooterRow.FindControl("totalAmmount");
            lblTotalAmount2.Text = String.Format("{0:n2}",
                                    grd_print_company.Rows.Cast<GridViewRow>()
                                    .Sum(a => Int32.Parse(((Label)a.FindControl("lbl_TotalText")).Text)));
           // Label lbltotalCompanyWise = (Label)grd_print_company.FooterRow.FindControl("totalAmmount");
            string TotalPrint_Total = lblTotalAmount2.Text;
            TotalPrint = Convert.ToInt32(grd_print_company.Rows.Count);


        }


    }
}
