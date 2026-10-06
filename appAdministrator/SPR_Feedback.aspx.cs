using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;

public partial class appAdministrator_SPR_Feedback : System.Web.UI.Page
{
    int ManPowerYear1 = 0;
    int ManPowerYear2 = 0;
    int ManPowerYear3 = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnFetchdata_Click(object sender, EventArgs e)
    {

        DataTable ds = new DataTable();
        
        string strPAN="";
        string strRegistration="";
        if(rdbType.SelectedValue=="1")
        {
            strPAN = txtPanNumber.Text;
            strRegistration = "";
        }
        else if (rdbType.SelectedValue == "2")
        {
            strPAN = "";
            strRegistration = txtregistration.Text;
        }

        ds = Company_Fetch_CompnayData_ForFeedback(strPAN, strRegistration);
        if (ds.Rows.Count > 0)
        {
            txtYear1.Text= ds.Rows[0]["fGPtbl_CUnit_TO_Year1"].ToString();
            txtYear2.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Year2"].ToString();
            txtYear3.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Year3"].ToString();

            txtTOYear1.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year1"].ToString();
            txtTOYear2.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year2"].ToString();
            txtTOYear3.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year3"].ToString();


            //txtTOYear1.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year1"].ToString();
            //txtTOYear2.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year2"].ToString();
            //txtTOYear3.Text = ds.Rows[0]["fGPtbl_CUnit_TO_Amount_Year3"].ToString();
        }
        else
        {
             
        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }

    public DataTable Company_Fetch_CompnayData_ForFeedback(string PAN, string Registration)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter sqldap = new SqlDataAdapter();
        try
        {

            sqldap = new SqlDataAdapter("Gp_sp_SPR_Fetch_FY_Turonover_Details", DBManager.ConnectToDB());
            sqldap.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldap.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar).Value = PAN;
            sqldap.SelectCommand.Parameters.Add("@Registration", SqlDbType.VarChar).Value = Registration;
            sqldap.Fill(dt);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            sqldap.Dispose();
        }
        return dt;
    }

    
}