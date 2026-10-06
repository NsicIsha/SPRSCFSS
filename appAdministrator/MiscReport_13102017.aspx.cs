using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;

public partial class appAdministrator_MiscReport_13102017 : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;
    int UnitTotal = 0, LiveTotal = 0, ExpiredTotal = 0, UnderprocessTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        if (!Page.IsPostBack)
        {
            BindGridData();
        }

    }


    public void BindGridData()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objclsCompany.Bind_Misc_data_New().Tables[0];
            if (dt.Rows.Count > 0)
            {
                grdData.DataSource = dt;
                grdData.DataBind();
            }
            else
            {
                dt.Dispose();
                grdData.Dispose();
            }
        }
        catch (Exception ex)
        {

        }
    }


    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    //protected void btnBackView_Click(object sender, EventArgs e)
    //{
    //    Response.ClearContent();
    //    Response.Buffer = true;
    //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", "Customers.xls"));
    //    Response.ContentType = "application/ms-excel";
    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter htw = new HtmlTextWriter(sw);
    //    grdData.AllowPaging = false;
    //    BindGridData();
    //    //Change the Header Row back to white color
    //    grdData.HeaderRow.Style.Add("background-color", "#FFFFFF");
    //    //Applying stlye to gridview header cells
    //    for (int i = 0; i < grdData.HeaderRow.Cells.Count; i++)
    //    {
    //        grdData.HeaderRow.Cells[i].Style.Add("background-color", "#df5015");
    //    }
    //    grdData.RenderControl(htw);
    //    Response.Write(sw.ToString());
    //    Response.End();
    //}


    //protected void btnBackView_Click(object sender, EventArgs e)
    //{
    //    ExportGridToExcel();
    //}

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    public static void ExportDataSetToExcel(DataTable dsInput)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object 
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel 
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + "fddsfdsf.xls" + "\"");
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

                    htw.Write("<table><tr><td colspan='9' align='center'>" + "jj" + "</td></tr></table>");
                    htw.Write("<table><tr><td colspan='9'></td></tr></table>");
                    dg1.RenderControl(htw);
                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
                    //response.End();
                    response.Close();
                }

            }
        }
        catch (Exception ex)
        {

        }

    }
    protected void btnBackView_Click(object sender, EventArgs e)
    {
        objclsCompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = objclsCompany.Bind_Misc_data().Tables[0];
        ExportDataSetToExcel(dt);
    }
}