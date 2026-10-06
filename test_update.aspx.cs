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
using System.Data.SqlClient;
using System.IO;

public partial class test_update : System.Web.UI.Page
{
    clsCompany objcompany;


    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btn_update_Click(object sender, EventArgs e)
    {
        fetch_datatable();
    }

    public void fetch_datatable()
    {
        objcompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = objcompany.dt_fetch_hs_code_test();
        if (dt.Rows.Count > 0)
        {
            int hscodeid;
            string hscode = "";
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {
                hscodeid = Convert.ToInt32(dt.Rows[i]["HscodeID"].ToString());
                hscode ="0"+ Convert.ToString(dt.Rows[i]["HsCode"].ToString());
                objcompany = new clsCompany();
            int k=  objcompany.insert_upadte_HsCode_test(hscodeid, hscode);
            if (k == 1)
            {
            }
            else
            {
            }
            }

        }
    }
}
