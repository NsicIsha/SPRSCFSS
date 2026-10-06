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
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;

using System.Data.SqlClient;

public partial class appAdministrator_PointOfInterest_Zonal : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    SqlConnection cnn;
    string connectionString = null;
    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;
    int UserId, RoleId, DepartmentId, Branchid;
    string dateFrom = "";
    string dateto = "";
    int FormType = 0;
    string UserFullName;

    int PrimaryId_POI = 0;
    string  fromdate;
    string todate;
    int status;
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsTrans_UserBranch objTUB;
    DataSet Myds;
    int ZoneId;

    protected void Page_Load(object sender, EventArgs e)
    {
        connectionString = ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        cnn = new SqlConnection(connectionString);

        UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
        clsTrans_UserBranch objtub = new clsTrans_UserBranch();
        Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);

        //txt_todate.Text = DateTime.Now.ToString("MM/dd/yyyy");
        //DateTime dt = DateTime.Now;
        //txt_todate.Text = DateTime.ParseExact("09/25/2013", "MM/dd/yyyy", null).ToString("MM/dd/yyyy");// dt.ToString("MM/dd/yyyy");

        //txt_todate.Text = DateTime.ParseExact(DateTime.Now.ToString(), "DD/MM/YYYY", CultureInfo.InvariantCulture).ToString("MM/DD/YYYY");


        if (!Page.IsPostBack)
        {

            try
            {

                if (Request.QueryString["PrimaryId_POI"] != "")
                {
                    PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
                }

                //if (Request.QueryString["FromDate"] != "")
                //{
                //    fromdate = DateTime.ParseExact(Request.QueryString["FromDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);  
                //}
                //if (Request.QueryString["ToDate"] != "")
                //{
                //    todate = DateTime.ParseExact(Request.QueryString["ToDate"].ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);                     
                //}


                //if (fromdate.ToString() != "" && todate.ToString() != "" && status != 0)
                
                    ////txt_from_date.Text = fromdate.ToString();
                    ////txt_todate.Text = todate.ToString();

                   

                    //ddlFormType.Items.FindByValue(status.ToString());

                   

                    DataSet dsss = new DataSet();
                    dsss = POI_Details(UserId, "1/1/2015", "03/31/2020");
                    if (dsss.Tables[0].Rows.Count > 0)
                    {
                        lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
                        grd_search_result.DataSource = dsss;
                        grd_search_result.DataBind();
                        // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
                    }
                    else
                    {
                        // lbl_searchcompany.Text = "Data Not Found";
                        grd_search_result.DataBind();
                        grd_search_result.PageIndex = 0;
                        //dt.Dispose();
                        lbl_totalcount_buying.Text = "";
                    }
                }
            catch (Exception ex)
            {
                USERID = 0;
                ROLEID = 0;
                USERFULLNAME = "";
            }
        }


    }

    public DataSet Bind_Zone_ById(int UserId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterZone_ById_New", connectionString); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MZ_Id", SqlDbType.VarChar).Value = UserId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {

        }
        finally
        {

            MyAdp.Dispose();
        }

        return MyDs;

    }
    public DataSet POI_Details(int userid,  string  fromdate, string todate)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_POI_DETAILS_Zone", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@userid", SqlDbType.Int).Value = userid;
            MyAdp.SelectCommand.Parameters.Add("@fromDate", SqlDbType.VarChar).Value = fromdate;
            MyAdp.SelectCommand.Parameters.Add("@toDate", SqlDbType.VarChar).Value = todate;

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
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        DataSet dsss = new DataSet();

        if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
        {
            fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
        }
        if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
        {
            todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
        }

        //fromdate = Convert.ToDateTime(txt_fromdate.Text);// DateTime.ParseExact(txt_fromdate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //todate = Convert.ToDateTime(txt_todate.Text);//DateTime.ParseExact(txt_todate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                                                       // todate = Convert.ToDateTime(txt_todate.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);



        dsss = POI_Details(UserId, fromdate, todate);
        if (dsss.Tables[0].Rows.Count > 0)
        {
            lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
            grd_search_result.DataSource = dsss;
            grd_search_result.DataBind();
            // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
        }
        else
        {
            // lbl_searchcompany.Text = "Data Not Found";
            grd_search_result.DataBind();
            grd_search_result.PageIndex = 0;
            //dt.Dispose();

            lbl_totalcount_buying.Text = "";
        }
    }
    protected void grdserch_pageindex_changing(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_search_result.PageIndex = e.NewPageIndex;

            //fromdate = DateTime.ParseExact(txt_fromdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //todate = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            }
            if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            }
            POI_Details(UserId, fromdate, todate);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ActionPOI")
        {
            int PrimaryId = 0;
            PrimaryId = Convert.ToInt32(e.CommandArgument);
          Response.Redirect("PointOfInterest_View_Zone.aspx?PrimaryId_POI=" + PrimaryId + "&FromDate=" + txt_fromdate.Text +"&ToDate=" + txt_todate.Text );

            //Response.Redirect("PointOfInterest_View.aspx?PrimaryId_POI=" + PrimaryId + "&Status=" + ddlFormType.SelectedValue);

        }
    }
}