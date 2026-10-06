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

public partial class appMIC_MIC_REPORT_DETAILS_Details : System.Web.UI.Page
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


    string Option1;
    string Option2;
    string Option3;
    string Option4;

    string Searchkeyword;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (!Page.IsPostBack)
            {
                MCIReportDetails();
            }
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
            Response.Redirect("MIC_REPORT_DETAILS.aspx");
        }
    }

    public void MCIReportDetails()
    {

        DataSet dsss = (DataSet)Session["DatasetValue"];
        if (dsss.Tables[0].Rows.Count > 0)
        {
            lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
            lbl_searchcompany.Text = Session["SearchValue"].ToString();
            grd_search_result.DataSource = dsss;
            grd_search_result.DataBind();
        }
        else
        {
            lbl_searchcompany.Text = "Data Not Found";
            grd_search_result.DataBind();
            grd_search_result.PageIndex = 0;
            dt.Dispose();
        }

        //if (dsss.Tables[0].Rows.Count > 0)
        //{
        //    lbl_totalcount_buying.Text = "Total Unit Count : " + dsss.Tables[0].Rows.Count.ToString();
        //    grd_search_result.DataSource = dsss;
        //    grd_search_result.DataBind();
        //    // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
        //}
        //else
        //{
        //    lbl_searchcompany.Text = "Data Not Found";
        //    grd_search_result.DataBind();
        //    grd_search_result.PageIndex = 0;
        //    dt.Dispose();
        //}

    }
}