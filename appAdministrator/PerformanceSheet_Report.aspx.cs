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

using System.Net.Mail;

public partial class appAdministrator_PerformanceSheet_Report : System.Web.UI.Page
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


    protected void gvEmp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //totalqty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));

            totalvalue += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Value"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalQty = (Label)e.Row.FindControl("lblTotalQty");
            lblTotalQty.Text = "Total Value : "; // totalqty.ToString();

            Label lbltotalvalue = (Label)e.Row.FindControl("lblTotalValue");
            lbltotalvalue.Text = totalvalue.ToString();
        }
    }


    //BINDING GRIDVIEW
    public void GridViewDataBind(string startdate, string enddate,int SearchType)
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
            lblCurrentStatus.Text = "since inception";
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl_totalcount_buying.Text = "Total Unit Count : " + ds.Tables[0].Rows.Count.ToString();
            grd_search_result.DataSource = ds;
            grd_search_result.DataBind();
            DataTable dt = ds.Tables[0];

            //here add code for column total sum and show in footer  
            //Calculate Sum and display in Footer Row
            ////decimal total = dt.AsEnumerable().Sum(row => row.Field<decimal>("Quantity"));
            ////grd_search_result.FooterRow.Cells[1].Text = "Total";
            ////grd_search_result.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Right;
            ////grd_search_result.FooterRow.Cells[2].Text = total.ToString("N2");
            ////grd_search_result.FooterRow.BackColor = System.Drawing.Color.Beige;
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

    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        int SearchCondition = 0;
         

        //  GridViewDataBind(Convert.ToDateTime(txt_from_date.Text), Convert.ToDateTime(txt_todate.Text),Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(1));

        if (txt_from_date.Text != "" && txt_todate.Text != "")
        {
            GridViewDataBind(txt_from_date.Text, txt_todate.Text,1);
        }
        else
        {
             GridViewDataBind("01/01/1999", "01/01/2099",0);
            //GridViewDataBind("", "", 0);
        }

    }
    public DataSet Company_Fetch_SinceInception(string Startdate, string Enddate,int SearchType)
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
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_PerformanceSheetData_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar).Value = Startdate;
            MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar).Value = Enddate;
            MyAdp.SelectCommand.Parameters.Add("@SearchType", SqlDbType.VarChar).Value = SearchType;
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
}