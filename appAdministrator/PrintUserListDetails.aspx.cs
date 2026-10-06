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

using System.IO;
using System.Text;
using System.Diagnostics;

public partial class appAdministrator_PrintUserListDetails : System.Web.UI.Page
{
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    string datefrom;
    string dateto;
    int zoneid;
    int branchid;
    int companyd;
    string Ctype = "";
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;

 protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["dt"] == "")
        {
            datefrom = "";
        }
        else
        {
            datefrom = Request.QueryString["dt"].ToString();
        }
        if (Request.QueryString["dto"] == "")
        {
            dateto = "";

        }
        else
        {
            dateto = Request.QueryString["dto"].ToString();
        }
        if (Request.QueryString["brid"] == "0")
        {
            branchid = 0;
        }
        else
        {
            branchid = Convert.ToInt32(Request.QueryString["brid"].ToString());
        }
        if (Request.QueryString["zid"] == "0")
        {
            zoneid = 0;
        }
        else
        {
            zoneid = Convert.ToInt32(Request.QueryString["zid"]);
        }
        bindGridView();
    }

    // Bind Grid UserList-------------------------
 public void bindGridView()
 {
     objcompany = new clsCompany();
     DataSet ds_user_list = new DataSet();
     //
     ds_user_list = objcompany.Company_UserList_Report(datefrom, dateto, branchid, zoneid);
     if (ds_user_list.Tables[0].Rows.Count > 0)
     {
         grd_user_list_report.DataSource = ds_user_list.Tables[0];
         grd_user_list_report.DataBind();

     }
     else
     {
        
         grd_user_list_report.Dispose();
         grd_user_list_report.DataBind();
         ds_user_list.Dispose();
     }
             
 }


 protected void grd_user_list_onRowDataBound(object sender, GridViewRowEventArgs e)
 {
     if (e.Row.RowType == DataControlRowType.DataRow)
     {
         if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
         {
             e.Row.Attributes.Add("style", @"page-break-after:always;");
             this.intCurrentRecordNumber = 0;
         }
     }
 }
}
