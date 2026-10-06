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

public partial class TestApplications_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        //System.Data.DataTable dt = new DataTable();
        //dt = clsDDL.ddlZone();
        //ddlZone.DataSource = dt;
        //ddlZone.DataTextField = dt.Columns["fGPtbl_MZ_Name"].ToString(); ;
        //ddlZone.DataValueField = dt.Columns["fGPtbl_MZ_Id"].ToString();
        //ddlZone.DataBind();
        
        
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        string str = "";
        str = Request["rd"].ToString();
    }


    protected void btntext_Click(object sender, EventArgs e)
    {
        string strtxt1 = string.Empty, strtxt2 = string.Empty;
        strtxt1 = TextBox1.Text;
        strtxt2 = TextBox2.Text;
    }
}
