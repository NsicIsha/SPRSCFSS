using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;


public partial class RegistrationValidity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlCommand sqlcom = new SqlCommand();
        try
        {
            sqlcom.Connection = DBManager.ConnectToDB();
            sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0";
            string comcount = Convert.ToString(sqlcom.ExecuteScalar());
            lblCount.Text = "Current Registered Units: " + comcount;
            sqlcom.Connection.Close();
        }
        catch { }
        finally
        {
            sqlcom.Dispose();
            DBManager.CloseConnection();
        }

    }
}
