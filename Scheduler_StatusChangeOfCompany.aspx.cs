using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
public partial class Scheduler_StatusChangeOfCompany : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet MyDs;

    protected void Page_Load(object sender, EventArgs e)
    {
        clsCompany objCompany = new clsCompany();
        DataSet MyDs = new DataSet();
        objCompany.StatusChangeOfCompany_whoseRegExp();       
    }

    
}
