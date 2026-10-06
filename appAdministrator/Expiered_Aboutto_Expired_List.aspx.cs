using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class appAdministrator_Expiered_Aboutto_Expired_List : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            DD_Monthbind();
            DD_Year();
        }
    }

    private void DD_Monthbind()
    {
        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);
        for (int i = 1; i < 13; i++)
        {
            ddl_month.Items.Add(new ListItem(info.GetMonthName(i), i.ToString()));

        }

    }

    private void DD_Year()
    {

        DateTimeFormatInfo info = DateTimeFormatInfo.GetInstance(null);

        int year = DateTime.Now.Year - 1;

        for (int Y = year; Y <= DateTime.Now.Year +1 ; Y++)
        {

            ddl_year.Items.Add(new ListItem(Y.ToString(), Y.ToString()));

        }

        ddl_year.SelectedValue = DateTime.Now.Year.ToString();

    }

    private void Bind_Gridview()
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        //ds = objcom.Company_About_To_Expiredlist(ddl_month.SelectedValue, ddl_year.SelectedValue);
        //if (ds != null)
        //{
        //    if (ds.Tables.Count > 0)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            gv_display.DataSource = ds;
        //            gv_display.DataBind();
        //        }
        //    }
        //}
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
    }
    protected void gv_display_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_display.PageIndex = e.NewPageIndex;
        Bind_Gridview();
    }
}
