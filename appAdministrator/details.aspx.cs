using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.Configuration;

public partial class appAdministrator_details : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        detailsGrid.RowDataBound += new GridViewRowEventHandler(detailsGrid_RowDataBound); 

        try
        {
            if (!IsPostBack)
            {
                string branch = Request.QueryString["branch"];
                string type = Request.QueryString["type"];
                string year = Request.QueryString["year"];
                string month = Request.QueryString["month"];
                if (!string.IsNullOrEmpty(branch) && !string.IsNullOrEmpty(type) &&
                    !string.IsNullOrEmpty(year) && !string.IsNullOrEmpty(month))
                {
                    LoadFilteredData(branch, type, year, month);
                }
            }
        }
        catch (Exception ex)
        {
            lblTitle.Text = "Error: " + ex.Message;
            detailsGrid.Visible = false;
        }
    }

    private void LoadFilteredData(string branch, string type, string year, string month)
    {

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            SqlCommand cmd = new SqlCommand("sp_GetFilteredDataStatusReport", conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Branch", branch);
            if (type == "Renewal")
                cmd.Parameters.AddWithValue("@TypeFilter", "Renewal%");
            else
                cmd.Parameters.AddWithValue("@TypeFilter", type);
            if (month != "0")
                cmd.Parameters.AddWithValue("@Month", month);
            else
                cmd.Parameters.AddWithValue("@Month", month);

            cmd.Parameters.AddWithValue("@Year", year);

            // Normalize type filter




            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            lblTitle.Text = "Filtered Data -" + branch+ "-" + type + "-" + year+ "-" + "Month:" + (month == "0" ? "All" : month) + "";

            decimal totalNSIC_fee = 0, totalServie_Tax = 0, totalNSIC_Surplus_Amount = 0, totalNSIC_Fee_after_Surplus_Adjustment = 0, totalServie_Tax_on_Adjustment_Amount = 0, totalAgency_Fee = 0, totalAgency_Service_Tax = 0;


            foreach (DataRow row in dt.Rows)
            {
                totalNSIC_fee += row.IsNull("NSIC_fee") ? 0 : Convert.ToDecimal(row["NSIC_fee"]);
                totalServie_Tax += row.IsNull("Servie_Tax") ? 0 : Convert.ToDecimal(row["Servie_Tax"]);
                totalNSIC_Surplus_Amount += row.IsNull("NSIC_Surplus_Amount") ? 0 : Convert.ToDecimal(row["NSIC_Surplus_Amount"]);
                totalNSIC_Fee_after_Surplus_Adjustment += row.IsNull("NSIC_Fee_after_Surplus_Adjustment") ? 0 : Convert.ToDecimal(row["NSIC_Fee_after_Surplus_Adjustment"]);
                totalServie_Tax_on_Adjustment_Amount += row.IsNull("Servie_Tax_on_Adjustment_Amount") ? 0 : Convert.ToDecimal(row["Servie_Tax_on_Adjustment_Amount"]);
                totalAgency_Fee += row.IsNull("Agency_Fee") ? 0 : Convert.ToDecimal(row["Agency_Fee"]);
                totalAgency_Service_Tax += row.IsNull("Agency_Service_Tax") ? 0 : Convert.ToDecimal(row["Agency_Service_Tax"]);

            }

            // ➕ Add TOTAL row
            DataRow totalRow = dt.NewRow();
            totalRow["NSIC_fee"] = totalNSIC_fee;
            totalRow["Servie_Tax"] = totalServie_Tax;
            totalRow["NSIC_Surplus_Amount"] = totalNSIC_Surplus_Amount;
            totalRow["NSIC_Fee_after_Surplus_Adjustment"] = totalNSIC_Fee_after_Surplus_Adjustment;
            totalRow["Servie_Tax_on_Adjustment_Amount"] = totalServie_Tax_on_Adjustment_Amount;
            totalRow["Agency_Fee"] = totalAgency_Fee;
            totalRow["Agency_Service_Tax"] = totalAgency_Service_Tax;
            dt.Rows.Add(totalRow);

            detailsGrid.DataSource = dt;
            detailsGrid.DataBind();
        }
    }

    protected void detailsGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;

            // Detect TOTAL row by checking if "Company_Name" or another key column is null/empty
            if (drv != null && drv.Row.IsNull("Company_Name"))
            {
                e.Row.Cells[0].Text = "TOTAL"; // 1st column (SerialNumber)
                e.Row.Font.Bold = true;
                e.Row.BackColor = System.Drawing.Color.LightYellow; // Optional: highlight row
            }
        }
    }


}