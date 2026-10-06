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

public partial class appAdministrator_Dashboard_Super : System.Web.UI.Page
{
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    string UserFullName = "";
    string UserEmailID = "";
    clsCompany objCompany;
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
                    GridViewDataBind("01/01/1999", "01/01/1999", 3, 0);
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
    public void GridViewDataBind(string startdate, string enddate,int searchtype,int searchfrom)
    {             

        objCompany = new clsCompany();
        DataSet ds = new DataSet();
        ds = Company_Fetch_DashBoard_SinceInception(startdate, enddate, searchtype, searchfrom);

        // lblCurrentStatus.Text=DateTime.Now.ToString("dd.MM.yyy");
        //Since inception
        if (txt_from_date.Text !="" && txt_todate.Text !="")
        { 
        lblCurrentStatus.Text = txt_from_date.Text + " to " + txt_todate.Text;
        }
        else
        {
            lblCurrentStatus.Text = "since inception";
        }
        lblTotalCount.Text = ds.Tables[0].Rows[0]["All_Total"].ToString();
        lblLiveUnitValue.Text = ds.Tables[0].Rows[0]["AllUnits_Live"].ToString();
        lblExUnitsValue.Text = ds.Tables[0].Rows[0]["AllUnits_Exp"].ToString();
        lblMicroValue.Text = ds.Tables[0].Rows[0]["AllMicro"].ToString();
        lblSmallValue.Text = ds.Tables[0].Rows[0]["AllSmall"].ToString();
        lblSCValue.Text = ds.Tables[0].Rows[0]["AllSC"].ToString();
        lblSTValue.Text = ds.Tables[0].Rows[0]["AllST"].ToString();
        lblMicroPer.Text = ds.Tables[0].Rows[0]["AllMicroPer"].ToString() + " " + "%";
        lblSmallPer.Text = ds.Tables[0].Rows[0]["AllSmallPer"].ToString() + " " + "%";
    }

    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        int SearchCondition = 0;
        if (rdbStatus.SelectedValue == "1")
        {
            SearchCondition = 1;
        }
        else if (rdbStatus.SelectedValue == "2")
        {
            SearchCondition = 2;
        }
        else if (rdbStatus.SelectedValue == "3")
        {
            SearchCondition = 3;
        }
        

        //  GridViewDataBind(Convert.ToDateTime(txt_from_date.Text), Convert.ToDateTime(txt_todate.Text),Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(1));

        if (txt_from_date.Text != "" && txt_todate.Text != "")
        {
            GridViewDataBind(txt_from_date.Text, txt_todate.Text, Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(1));
        }
        else
        {
            GridViewDataBind("01/01/1999", "01/01/1999", Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(0));
        }

    }
    public DataSet Company_Fetch_DashBoard_SinceInception(string Startdate,string Enddate,int SearchType, int SearchFrom)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_DashboardData", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;

            MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar).Value = Startdate;
            MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar).Value = Enddate;
            MyAdp.SelectCommand.Parameters.Add("@SearchType", SqlDbType.Int).Value = SearchType;
            MyAdp.SelectCommand.Parameters.Add("@SearchFrom", SqlDbType.Int).Value = SearchFrom;

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

    protected void rdbDuration_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(txt_from_date.Text !="" && txt_todate.Text !="")
        { 
        GridViewDataBind(txt_from_date.Text, txt_todate.Text, Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(1));
        }
        else
        {            
        GridViewDataBind("01/01/1999", "01/01/1999", Convert.ToInt32(rdbStatus.SelectedValue), Convert.ToInt32(0));
        }
    }
}