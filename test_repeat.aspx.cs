using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class test_repeat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[2] { new DataColumn("Name"), new DataColumn("Age") });
        dt.Rows.Add("John", "12");
        dt.Rows.Add("Sam", "42");
        dt.Rows.Add("Mike", "31");
        dt.Rows.Add("Jerry", "19");
        dt.Rows.Add("Thomas", "77");
        
        rpt.DataSource = dt;
        rpt.DataBind();
    }
}
