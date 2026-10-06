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
using System.Globalization;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

public partial class appAdministrator_PointOfInterest_Admin : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

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
    string fromdate;
    string todate;
    int status;

    protected void Page_Load(object sender, EventArgs e)
    {

        UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        ////UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
        //clsTrans_UserBranch objtub = new clsTrans_UserBranch();
        //Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);

        if (!Page.IsPostBack)
        {

            try
            {

                if (Request.QueryString["PrimaryId_POI"] != "")
                {
                    PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
                }

                ////if (Request.QueryString["FromDate"] != "")
                ////{
                ////    fromdate = Request.QueryString["FromDate"].ToString();
                ////}
                ////if (Request.QueryString["ToDate"] != "")
                ////{
                ////    todate = Request.QueryString["ToDate"].ToString();
                ////}
                if (Request.QueryString["Status"] != "")
                {
                    status = Convert.ToInt32(Request.QueryString["Status"]);
                }

                //if (fromdate.ToString() != "" && todate.ToString() != "" && status != 0)
                if (status != 0)
                {
                    ////txt_from_date.Text = fromdate.ToString();
                    ////txt_todate.Text = todate.ToString();

                    ddlFormType.SelectedIndex = -1;

                    //ddlFormType.Items.FindByValue(status.ToString());

                    ddlFormType.Items.FindByValue(status.ToString()).Selected = true;

                    DataSet dsss = new DataSet();
                    dsss = POI_Details(Convert.ToInt32(ddlFormType.SelectedValue), "", "");
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

                else
                {
                    DataSet dsss = new DataSet();
                    dsss = POI_Details( 1, "", "");
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


            }
            catch (Exception ex)
            {
                USERID = 0;
                ROLEID = 0;
                USERFULLNAME = "";
            }
        }


    }



    public DataSet POI_Details(  int Status, string fromdate, string todate)
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
            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

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

        //fromdate = DateTime.ParseExact(txt_from_date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        ////todate = DateTime.ParseExact(txt_todate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
        //todate= Convert.ToDateTime(txt_todate.Text, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat);



        dsss = POI_Details( Convert.ToInt32(ddlFormType.SelectedValue), "", "");
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

            // fromdate = DateTime.ParseExact(txt_from_date.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            // todate = DateTime.ParseExact(txt_todate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            POI_Details( Convert.ToInt32(ddlFormType.SelectedValue), "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btn_chek_Click(object sender, EventArgs e)
    {
        //DataSet dsa = new DataSet();
        //dsa = POI_Details(Convert.ToInt32(1),"","");
        //ExportDataSetToExcel(dsa, "RevenueReport.xls", "");

        Response.ClearContent();
        Response.AppendHeader("content-disposition", "attachment;filename=EmployeeDetails.xls");
        Response.ContentType = "application/excel";

        StringWriter stringwriter = new StringWriter();
        HtmlTextWriter htmtextwriter = new HtmlTextWriter(stringwriter);

        grd_search_result.HeaderRow.Style.Add("background-color", "#ffffff");

        foreach (TableCell tableCell in grd_search_result.HeaderRow.Cells)
        {
            tableCell.Style["background-color"] = "#ffffff";
        }

        foreach (GridViewRow gridviewrow in grd_search_result.Rows)
        {
            gridviewrow.BackColor = System.Drawing.Color.White;
            foreach (TableCell gridviewrowtablecell in gridviewrow.Cells)
            {
                gridviewrowtablecell.Style["background-color"] = "#ffffff";
            }
        }

        grd_search_result.RenderControl(htmtextwriter);
        Response.Write(stringwriter.ToString());
        // Response.End();
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.SuppressContent = true;
        HttpContext.Current.ApplicationInstance.CompleteRequest();


    }


    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    public static void ExportDataSetToExcel(DataSet dsInput, string ExcelFileName, string text)
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
            dg1.DataSource = dsInput.Tables[0];
            dg1.DataBind();

            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();


            // create a string writer 
            ////using (StringWriter sw = new StringWriter())
            ////{
            ////    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            ////    {
            ////        // instantiate a datagrid

            ////        htw.Write("<table><tr><td colspan='9' align='center'>" + "dsadas" + "</td></tr></table>");
            ////        htw.Write("<table><tr><td colspan='9'></td></tr></table>");
            ////        grd_search_result.RenderControl(htw);
            ////        response.Write(HttpUtility.HtmlDecode(sw.ToString()));

            ////        HttpContext.Current.Response.Flush();
            ////        HttpContext.Current.Response.SuppressContent = true;
            ////        HttpContext.Current.ApplicationInstance.CompleteRequest();


            ////       // response.End();
            ////    }

            ////}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }
        finally
        {
            //Sends the response buffer
            HttpContext.Current.Response.Flush();
            // Prevents any other content from being sent to the browser
            HttpContext.Current.Response.SuppressContent = true;
            //Directs the thread to finish, bypassing additional processing
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //Suspends the current thread
            //thr.Sleep(1);
        }

    }

 
    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ActionPOI")
        {
            int PrimaryId = 0;
            PrimaryId = Convert.ToInt32(e.CommandArgument);
            //   Response.Redirect("PointOfInterest_View.aspx?PrimaryId_POI=" + PrimaryId + "&FromDate=" + txt_from_date.Text +"&ToDate=" + txt_todate.Text + "&Status=" + ddlFormType.SelectedValue);
            Response.Redirect("PointOfInterest_View_Admin.aspx?PrimaryId_POI=" + PrimaryId + "&Status=" + ddlFormType.SelectedValue);
        }
    }
}