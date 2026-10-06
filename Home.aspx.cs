using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Data;
 
public partial class Home : System.Web.UI.Page
{

    clsCompany objCompany;
    DataSet MyDs;



    public DataSet Get_Schedular_Status()
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GP_Sp_Get_Schedular_Status", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("HomeNew.aspx");
       // SqlCommand sqlcom = new SqlCommand();
        try
        {
            //sqlcom.Connection = DBManager.ConnectToDB();
            //sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
            //string comcount = Convert.ToString(sqlcom.ExecuteScalar());
            //lblCount.Text = "Current Registered Units: " + comcount;
            //sqlcom.Connection.Close();
        }
        catch { }
        finally
        {
            //sqlcom.Dispose();
            //DBManager.CloseConnection();
        }

        ////DataSet dst = new DataSet();
        ////dst = Get_Schedular_Status(); ;
       
        ////int status = 0;
        ////if (dst.Tables[0].Rows.Count > 0)
        ////{
        ////    status = Convert.ToInt32(dst.Tables[0].Rows[0]["StatusFlag"].ToString());
        ////}

        ////if (status == 0)
        ////{
        ////    clsCompany objCompany = new clsCompany();
        ////    DataSet MyDs = new DataSet();
        ////    objCompany.StatusChangeOfCompany_whoseRegExp();
        ////}



    }
}
