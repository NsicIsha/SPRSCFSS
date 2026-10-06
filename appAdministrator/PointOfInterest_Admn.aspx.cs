using NSICGP_VBClassLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_PointOfInterest_Admn : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    clsCompany objcompany;
    DataTable dt;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             Bind_gridview();
            if (!IsPostBack)
            {
                System.Data.DataTable dt = new DataTable();
                //Category Cast DROPDOWNLIST
                ds = BindBranch();
                dt = new DataTable();
                dt = ds.Tables[0];
                ddl_InspectionAgency.DataSource = dt;
                ddl_InspectionAgency.DataTextField = dt.Columns["Branch_Name"].ToString();
                ddl_InspectionAgency.DataValueField = dt.Columns["ID"].ToString();
                ddl_InspectionAgency.DataBind();
                ddl_InspectionAgency.Items.Insert(0, new ListItem("-- Select Branch --", "999"));
                ddl_InspectionAgency.Items.Insert(1, new ListItem("All Branch", ""));

                //Category Area DROPDOWNLIST
                ////dt.Clear();
            }
        }

        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }

    public DataSet BindBranch()
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_BindBranch_Admin", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            //MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    protected void grd_inspection_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_inspection.PageIndex = e.NewPageIndex;
        Bind_gridview();
    }

    public void Bind_gridview()
    {
        try
        {
            //int agency = -1;
            //string fromdate = "";
            //string todate = "";
            //try
            //{


            //    if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            //    {
            //        fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            //    }
            //    if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            //    {
            //        todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            //    }
            //    agency = ddl_InspectionAgency.SelectedValue != "" ? Convert.ToInt32(ddl_InspectionAgency.SelectedValue) : -1;

            objcompany = new clsCompany();
            DataSet ds = new DataSet();
            //ds = objcompany.Fetch_Inspection_Report_nullpath_mod();
            ////ds = objcompany.POI_Details(Convert.ToInt32(1));

            ds = POI_Details(Convert.ToInt32(ddlStatus.SelectedValue));
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

    public void Bind_gridview_Search()
    {
        

            string fromdate = "";
            string todate = "";
            int branchid = 0;
            int status = 0;
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
                branchid = ddl_InspectionAgency.SelectedValue != "" ? Convert.ToInt32(ddl_InspectionAgency.SelectedValue) : -1;
                status = ddlStatus.SelectedValue != "" ? Convert.ToInt32(ddlStatus.SelectedValue) : -1;

                objcompany = new clsCompany();
                DataSet ds = new DataSet();
                ds = POI_Details_Search(fromdate, todate, Convert.ToInt32(ddl_InspectionAgency.SelectedValue),  Convert.ToInt32(ddlStatus.SelectedValue));
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
                grd_inspection.DataSource = null;
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
        Bind_gridview_Search();
    }

    public DataSet POI_Details_Search(string fromdate, string todate,int branchid, int status)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_PointOfInterest_Admin_Search", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;

            if (fromdate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@fromdate", SqlDbType.DateTime).Value = fromdate;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@todate", SqlDbType.DateTime).Value = todate;
            MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.Int).Value = status;

            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public DataSet POI_Details(Int32 Status)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_PointOfInterest_Admin", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.Int).Value = Status;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }



    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grd_inspection.AllowPaging = false;
                this.Bind_gridview(); 
                grd_inspection.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grd_inspection.HeaderRow.Cells)
                {
                    cell.BackColor = grd_inspection.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grd_inspection.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grd_inspection.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grd_inspection.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grd_inspection.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError(" " + ex.Message.ToString());
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }





    protected void btn_Excel_Click(object sender, EventArgs e)
    {
        try
        {
            clsCompany objcompany = new clsCompany();
            DataSet ds = new DataSet();
            //ds = objcompany.Fetch_Inspection_Report_nullpath_mod();
            // ds = objcompany.POI_Details(1);

            ds = POI_Details(Convert.ToInt32(ddlStatus.SelectedValue));

            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataTable Dt1 = new DataTable();
                Dt1.Columns.Add("S No.");
                
                Dt1.Columns.Add("Company Name");
                Dt1.Columns.Add("PAN");
                Dt1.Columns.Add("Nature of Operation");
                Dt1.Columns.Add("Constitution");

                Dt1.Columns.Add("State");
                Dt1.Columns.Add("Branch");
                Dt1.Columns.Add("UAM");
                Dt1.Columns.Add("Contact Person");

                Dt1.Columns.Add("Social Category");
                Dt1.Columns.Add("GST");
                Dt1.Columns.Add("Email");
                Dt1.Columns.Add("Mobile");


                Dt1.Columns.Add("Product Details");
                Dt1.Columns.Add("EM II");
                Dt1.Columns.Add("Agency Name");
                Dt1.Columns.Add("Point of Intrest Date");

                //Dt1.Columns.Add("Inspection Agency/UserName");


                for (int i = 0; i < dt.Rows.Count; i++)
                {


                    //Dt1.Rows.Add(i + 1, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][3].ToString(),
                    //dt.Rows[i][2].ToString());

                    Dt1.Rows.Add(i + 1, dt.Rows[i][1].ToString(), dt.Rows[i][0].ToString(),
                        dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString(),
                        dt.Rows[i][6].ToString(), dt.Rows[i][7].ToString(), dt.Rows[i][8].ToString(), dt.Rows[i][9].ToString(),
                        dt.Rows[i][10].ToString(), dt.Rows[i][11].ToString(), dt.Rows[i][12].ToString(), dt.Rows[i][13].ToString(),
                        dt.Rows[i][14].ToString(), dt.Rows[i][15].ToString()





                        );

                }
                Dt1.AcceptChanges();
                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Point of Interest Report " + "" + " " + "" + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(Dt1, "PointofInterest.xls", fromdatetodate);
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


    protected void Button1_Init(object sender, EventArgs e)
    {
        Button btnPrint = (Button)sender;
        ScriptManager.GetCurrent(this).RegisterPostBackControl(btnPrint);
    }
}