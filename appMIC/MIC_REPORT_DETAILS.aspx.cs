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
public partial class appMIC_MIC_REPORT_DETAILS : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;

    string dateFrom = "";
    string dateto="";
    int FormType = 0;


    string Option1;
    string Option2;
    string Option3 ;
    string Option4 ;

    string Searchkeyword;


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


    public DataSet MCIReportDetails()
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        //Option1 = "NO";
        //Option2 = "NO";
        //Option3 = "NO";
        //Option4 = "NO";
        //Searchkeyword = txtProductName.Text;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GP_sp_MIC_Report_Search", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@Option1", SqlDbType.VarChar).Value = Option1;
            MyAdp.SelectCommand.Parameters.Add("@Option2", SqlDbType.VarChar).Value = Option2;
            MyAdp.SelectCommand.Parameters.Add("@Option3", SqlDbType.VarChar).Value = Option3;
            MyAdp.SelectCommand.Parameters.Add("@Option4", SqlDbType.VarChar).Value = Option4;
            MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar).Value = Searchkeyword;
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


   
    protected void grdserch_pageindex_changing(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_search_result.PageIndex = e.NewPageIndex;
            MCIReportDetails();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void Btn_Search_Click(object sender, EventArgs e)
    {
         Option1 = "NO";
         Option2 = "NO";
         Option3 = "NO";
         Option4 = "NO";
         Searchkeyword = txtProductName.Text;

        if(chkOption.SelectedValue=="1")
        {
            Option1 = "YES";
        }
        else
        {
            Option1 = "NO";
        }
        if (chkOption.SelectedValue == "2")
        {
            Option2 = "YES";
        }
        else
        {
            Option2 = "NO";
        }

        if (chkOption.SelectedValue == "3")
        {
            Option3 = "YES";
        }
        else
        {
            Option3 = "NO";
        }
        if (chkOption.SelectedValue == "4")
        {
            Option4 = "YES";
        }
        else
        {
            Option4 = "NO";
        }


        //dateFrom = txt_from_date.Text.ToString();
        //dateto = txt_todate.Text.ToString();
        //FormType =Convert.ToInt32(ddlFormType.SelectedValue);
        DataSet dsss = new DataSet();

        dsss = MCIReportDetails();
        Session["DatasetValue"] = dsss;

        Session["SearchValue"] = txtProductName.Text;

        //DataSet Dt = (DataSet)session["sessiondata"];
        //and bind the gridview with dataset object as
        //gridview.datasource = dt;
        //gridview.databind();


        Response.Redirect("MIC_REPORT_DETAILS_Details.aspx");

        if (dsss.Tables[0].Rows.Count > 0)
        {
            lbl_totalcount_buying.Text ="Total Unit Count : " +  dsss.Tables[0].Rows.Count.ToString();
            grd_search_result.DataSource = dsss;
            grd_search_result.DataBind();
            // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
        }
        else
        {
            lbl_searchcompany.Text = "Data Not Found";
            grd_search_result.DataBind();
            grd_search_result.PageIndex = 0;
            dt.Dispose();
        }
    }
}