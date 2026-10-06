using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;

public partial class appAdministrator_MIS_FY : System.Web.UI.Page
{
    string connectionString = WebConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
    string selectedYear = "0", selectedMonth = "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGrid("0", "0");
        }
    }
    protected void YearList_SelectedIndexChanged(object sender, EventArgs e)
    {
        MonthList.Enabled = !string.IsNullOrEmpty(YearList.SelectedValue);

        selectedYear = YearList.SelectedValue;
        MonthList.ClearSelection();
        if (MonthList.Items.Count > 0)
        {
            MonthList.SelectedIndex = 0;
        }

        selectedMonth = "0";

        LoadGrid(selectedYear, selectedMonth);

    }
    protected void MonthList_SelectedIndexChanged(object sender, EventArgs e)
    {
        selectedYear = YearList.SelectedValue;
        selectedMonth = MonthList.SelectedValue;
        LoadGrid(selectedYear, selectedMonth);

    }

    private void LoadGrid(string year, string month)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("sp_GetBranchApplicationCounts", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Year", year);
            cmd.Parameters.AddWithValue("@Month", month);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            int totalFresh = 0, totalRenewal = 0, totalAmendment = 0;

            foreach (DataRow row in dt.Rows)
            {
                totalFresh += row.Field<int>("Fresh");
                totalRenewal += row.Field<int>("Renewal");
                totalAmendment += row.Field<int>("Amendment");
            }

            // ➕ Add TOTAL row
            DataRow totalRow = dt.NewRow();
            totalRow["Branch"] = "TOTAL";
            totalRow["Fresh"] = totalFresh;
            totalRow["Renewal"] = totalRenewal;
            totalRow["Amendment"] = totalAmendment;
            dt.Rows.Add(totalRow);

            grdView1.DataSource = dt;
            grdView1.DataBind();
        }
    }

    protected void grdView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Make sure we're processing a data row (not header/footer)
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string branch = e.Row.Cells[0].Text;

            for (int i = 1; i < e.Row.Cells.Count; i++) // Assuming 0 is Branch, others are numbers
            {
                string type = grdView1.HeaderRow.Cells[i].Text; // "Fresh", "Renewal", etc.
                string value = e.Row.Cells[i].Text;

                if (branch != "TOTAL")
                {
                    try
                    {
                        string url = "details.aspx?branch="+ branch + "&type="+type+"&year="+selectedYear+ "&month="+selectedMonth+"";
                        string path = Server.MapPath("details.aspx");

                        if (File.Exists(path))
                        {
                            e.Row.Cells[i].Text = "<a target=\"_blank\" href=' " + url+"'>" +value+ "</a>";
                        }
                        else
                        {
                            // Just show plain text
                          //  e.Row.Cells[i].Text = value;
                            e.Row.Cells[i].Text = "<a href=' " + url + "'>" + value + "</a>";
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error if needed
                        e.Row.Cells[i].Text = value; // fallback to plain text
                    }
                }
            }
        }
    }
}