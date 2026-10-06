using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Drawing;
using System.Net.Mail;

public partial class appAdministrator_PerformanceSheet_TurnoverReport : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;


    string fromAddress;
    string toAddress;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;


    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;

    DataSet MyDs;
    DataTable MyDt;
    int totalqty = 0;
    int totalvalue = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

            if (Page.IsPostBack != true)
            {
                //FILLING GRIDVIEW
                GridViewDataBind("04/01/2020", "03/31/2021", 0);
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind(string startdate, string enddate, int SearchType)
    {
        objCompany = new clsCompany();
        DataSet ds = new DataSet();
        ds = Company_Fetch_SinceInception(startdate, enddate, SearchType);

        // lblCurrentStatus.Text=DateTime.Now.ToString("dd.MM.yyy");
        //Since inception
        if (txt_from_date.Text != "" && txt_todate.Text != "")
        {
            lblCurrentStatus.Text = txt_from_date.Text + " to " + txt_todate.Text;
        }
        else
        {
            lblCurrentStatus.Text = "04-01-2020 to " + DateTime.Now.ToString("MM/dd/yyyy");
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            //lbl_totalcount_buying.Text = "Total Unit Count : " + ds.Tables[0].Rows.Count.ToString();
            grd_search_result.DataSource = ds;

            //  grd_search_result.Columns[2].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<double>("Turnover")).Sum().ToString();

            //grd_search_result.Columns[3].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("Unit_Count")).Sum().ToString();
            //grd_search_result.Columns[4].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Double>("FY_2018_19")).Sum().ToString();
            //grd_search_result.Columns[5].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Double>("FY_2019_20")).Sum().ToString();
            //grd_search_result.Columns[6].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Double>("TotalBothFY")).Sum().ToString();

            grd_search_result.DataBind();
            DataTable dt = ds.Tables[0];
            
        }
        else
        {
            // lbl_searchcompany.Text = "Data Not Found";
            grd_search_result.DataSource = null;
            grd_search_result.DataBind();
            grd_search_result.PageIndex = 0;
            //dt.Dispose();

            lbl_totalcount_buying.Text = "";
        }
    }


    public DataSet Company_Fetch_SinceInception(string Startdate, string Enddate, int SearchType)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            ////MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_PerformanceSheetData", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_fetch_PerformanceSheet_Trunover_Data_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar).Value = Startdate;
            MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar).Value = Enddate;

            MyAdp.SelectCommand.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = SearchType;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "";
        cell.ColumnSpan = 3;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 3;
        cell.Text = "Performance";
        cell.HorizontalAlign = HorizontalAlign.Center;
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        grd_search_result.HeaderRow.Parent.Controls.AddAt(0, row);
    }


    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        int SearchCondition = 0;


        //  GridViewDataBind(Convert.ToDateTime(txt_from_date.Text), Convert.ToDateTime(txt_todate.Text),Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(1));

        if (txt_from_date.Text != "" && txt_todate.Text != "")
        {
            GridViewDataBind(txt_from_date.Text, txt_todate.Text, 1);
        }
        else
        {
            GridViewDataBind("01/01/1999", "01/01/2099", 0);
            //GridViewDataBind("", "", 0);
        }
    }
}