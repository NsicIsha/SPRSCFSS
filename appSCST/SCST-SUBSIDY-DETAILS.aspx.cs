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
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Drawing;

public partial class appSCST_SCST_SUBSIDY_DETAILS : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;

    string dateFrom = "";
    string dateto = "";
    int FormType = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }
    ////private void Bind_Buying_Agencies()
    ////{
    ////    try
    ////    {
    ////        if (ROLEID == 17)
    ////        {
    ////            objAgency = new clsMasterInspectionAgency();
    ////            dt = new DataTable();
    ////            ////SearchText = txt_serch_Criteria.Text.Trim().ToString();

    ////            ////if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
    ////            ////{
    ////            ////    SearchText = "";
    ////            ////}
    ////            ////else
    ////            ////{
    ////            ////    SearchText = txt_serch_Criteria.Text.Trim().ToString();
    ////            ////}

    ////            ////if (SearchText != "")
    ////            ////{
    ////            ////    lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + "" + SearchText.ToString();
    ////            ////}
    ////            ////else
    ////            ////{
    ////            ////    lbl_searchcompany.Text = "";
    ////            ////}
    ////            //  System.Data.SqlClient.SqlDataReader dr = objAgency.BuyingAgency_Fetch_Search_test(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
    ////            dt = objAgency.BuyingAgency_Fetch_Search(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
    ////            //dt.Load(dr);
    ////            if (dt.Rows.Count > 0)
    ////            {

    ////                grd_search_result.DataSource = dt;
    ////                grd_search_result.DataBind();
    ////               // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
    ////            }
    ////            else
    ////            {
    ////                // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
    ////                //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
    ////               //// txt_serch_Criteria.Text = "";
    ////                //  grd_search_result.Dispose();
    ////                grd_search_result.DataBind();
    ////                grd_search_result.PageIndex = 0;
    ////                dt.Dispose();
    ////            }
    ////        }
    ////    }
    ////    catch (Exception ex)
    ////    {
    ////        ErrHandler.WriteError(ex.Message);
    ////    }

    ////}
    public DataSet SCSTSubsidyDetails()
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        dateFrom = txt_from_date.Text.ToString();
        dateto = txt_todate.Text.ToString();
        FormType = Convert.ToInt32(ddlFormType.SelectedValue);

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_New_18102017", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 6000000;
            MyAdp.SelectCommand.Parameters.AddWithValue("@startdate", SqlDbType.DateTime).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.AddWithValue("@enddate", SqlDbType.DateTime).Value = dateto;
            MyAdp.SelectCommand.Parameters.AddWithValue("@Formtype", SqlDbType.Int).Value = FormType;
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
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btn_chek_Click(object sender, EventArgs e)
    {
        try
        {

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "SC/ST_UNIT_DETAILS" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //grd_search_result.GridLines = GridLines.Both;
            //grd_search_result.HeaderStyle.Font.Bold = true;
            //grd_search_result.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            ////Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest();

            //HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            //HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            //HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.

            grd_search_result.HeaderRow.BackColor = System.Drawing.Color.FromArgb(0, 255, 255, 204);
            grd_search_result.HeaderRow.Font.Bold = false;
            grd_search_result.HeaderRow.Height = Unit.Pixel(30);
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + "SC/ST_UNIT_DETAILS.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            grd_search_result.RenderControl(new HtmlTextWriter(sw));
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

        ////clsTrans_UserBranch objtub = new clsTrans_UserBranch();
        //////try
        //////{

        ////        DataSet dsInput = new DataSet();
        ////        StringBuilder sb = new StringBuilder();

        ////        dateFrom = txt_from_date.Text.ToString();
        ////        dateto = txt_todate.Text.ToString();
        ////        FormType = Convert.ToInt32(ddlFormType.SelectedValue);
        ////        DataSet dsss = new DataSet();
        ////        dsss = SCSTSubsidyDetails();


        ////        if (dsss.Tables[0].Rows.Count > 0)
        ////        {
        ////            //dsInput.Tables[0].Columns.Remove("tblid");
        ////            //dsInput.Tables[0].Columns.Remove("ZoneID");
        ////            //dsInput.Tables[0].Columns.Remove("BranchID");
        ////            //dsInput.Tables[0].AcceptChanges();

        ////            HttpResponse response = HttpContext.Current.Response;

        ////            //if (FormTypeval == 1)
        ////            //{
        ////            //    string ExcelFileName = "Fresh_GP_Units_Details_" + From_date + "_to_ " + To_date + ".xls";
        ////            //}
        ////            //else if (FormTypeval == 2)
        ////            //{
        ////            //    string ExcelFileName = "Renewal_GP_Units_Details_" + From_date + "_to_ " + To_date + ".xls";
        ////            //}

        ////            string ExcelFileName = "SC/ST_UNIT_DETAILS_" + txt_from_date + "_to_ " + txt_todate + ".xls";
        ////            string text = "<b><span >SC/ST Company Wise Status Report  " + txt_from_date + "  to  " + txt_todate + "</span></b>" + "<br/><br/>";
        ////            // first let's clean up the response.object 
        ////            response.Clear();
        ////            response.Charset = "";

        ////            // set the response mime type for excel 
        ////            response.ContentType = "application/vnd.ms-excel";
        ////            response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelFileName + "\"");
        ////            response.Cache.SetCacheability(HttpCacheability.NoCache);


        ////            //for Old Code type we can uncomment below code
        ////            // GridView dg1 = new GridView();
        ////            //dg1.DataSource = dsInput.Tables[0];
        ////            //dg1.DataBind();
        ////            //For GridView Added on 12.04.2019 by Dhiraj
        ////            grd_search_result.DataSource = dsss.Tables[0];
        ////            grd_search_result.DataBind();
        ////            // grd_search_result.ShowFooter = false;

        ////            grd_search_result.FooterRow.Visible = false;

        ////            // create a string writer 
        ////            using (StringWriter sw = new StringWriter())
        ////            {
        ////                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        ////                {
        ////                    // instantiate a datagrid

        ////                    htw.Write("<table border=1><tr><td colspan='9' align='center'>" + text + "</td></tr></table>");
        ////                    htw.Write("<table border=1><tr><td colspan='9'></td></tr></table>");
        ////                    //For GridView Added on 12.04.2019 by Dhiraj
        ////                    grd_search_result.RenderControl(htw);
        ////                    string style = @"<style> TABLE { border: 1px solid black; } TD { border: 1px solid black; } </style> ";
        ////                    Response.Write(style);

        ////                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
        ////                    response.End();
        ////                }

        ////            }
        ////        }

        //////}
        //////catch (Exception ex)
        //////{
        //////    ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        //////}
    }
    protected void grdserch_pageindex_changing(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_search_result.PageIndex = e.NewPageIndex;

            dateFrom = txt_from_date.Text.ToString();
            dateto = txt_todate.Text.ToString();
            FormType = Convert.ToInt32(ddlFormType.SelectedValue);
            DataSet dsss = new DataSet();
            dsss = SCSTSubsidyDetails();
            if (dsss.Tables[0].Rows.Count > 0)
            {
                lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
                grd_search_result.DataSource = dsss;
                grd_search_result.DataBind();
                // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
            }
            else
            {
                lbl_searchcompany.Text = "Data Not Found";
                grd_search_result.DataBind();
                grd_search_result.PageIndex = 0;
                //dt.Dispose();
            }


            // SCSTSubsidyDetails();
            //grd_search_result.DataBind();


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        dateFrom = txt_from_date.Text.ToString();
        dateto = txt_todate.Text.ToString();
        FormType = Convert.ToInt32(ddlFormType.SelectedValue);
        DataSet dsss = new DataSet();
        dsss = SCSTSubsidyDetails();
        if (dsss.Tables[0].Rows.Count > 0)
        {
            lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
            grd_search_result.DataSource = dsss;
            grd_search_result.DataBind();
            // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
        }
        else
        {
            lbl_searchcompany.Text = "Data Not Found";
            grd_search_result.DataBind();
            grd_search_result.PageIndex = 0;
            //dt.Dispose();
        }
    }
}