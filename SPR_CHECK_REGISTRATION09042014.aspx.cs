using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SPR_CHECK_REGISTRATION : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        clsCompany objcom = new clsCompany();
        DataSet ds = new DataSet();
        string registrationno;
        registrationno = txtSearchbox.Text;
        if(registrationno != string.Empty)
        {
            ds = objcom.Company_SPR_CHECK_REGISTRATION(registrationno);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gv_companies.DataSource = ds;
                    gv_companies.DataBind();
                }
                else
                {
                    gv_companies.DataSource = null;
                    gv_companies.DataBind();
                    gv_companies.EmptyDataText = "No record found";
                }
            }
        }
        
    }
}
