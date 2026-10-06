using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

public partial class NsicIntranet : System.Web.UI.Page
{
    clsCompany objCompany;

    DataTable dt;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    SqlConnection con;

    protected void Page_Load(object sender, EventArgs e)
    {
        //SqlCommand sqlcom = new SqlCommand();
        //try
        //{
        //    sqlcom.Connection = DBManager.ConnectToDB();
        //    sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
        //    string comcount = Convert.ToString(sqlcom.ExecuteScalar());
        //    lblCount.Text = "Current Registered Units: " + comcount;
        //    sqlcom.Connection.Close();
        //}
        //catch { }
        //finally
        //{
        //    sqlcom.Dispose();
        //    DBManager.CloseConnection();
        //}



        GridView1.DataSource = MasterFees_Slab_Fetch_DataSet();
        GridView1.DataBind();



    }


    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        Label lblGId = (Label)e.Row.FindControl("lblfInFTrack_1GeneratedNo");
        Repeater rt = (Repeater)e.Row.FindControl("rpEmpSearch");
        Repeater rpEmpEmp = (Repeater)e.Row.FindControl("rpEmpEmp");
        Repeater rpEmpPrpEd = (Repeater)e.Row.FindControl("rpEmpPrpEd");

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int Gid = Convert.ToInt32(lblGId.Text);
            DataSet dsss = new DataSet();
            dsss= MasterFees_Slab_Fetch_DataSet_byId(Gid);


            rt.DataSource = dsss;
            rt.DataBind();

            DataSet dsEmp = new DataSet();
            dsEmp = MasterFees_Slab_Fetch_DataSet_EmpDetails_byId(Gid);
            rpEmpEmp.DataSource = dsEmp;
            rpEmpEmp.DataBind();

            DataSet dsPrpEd = new DataSet();
            dsPrpEd = MasterFees_Slab_Fetch_DataSet_Prop_Degree_byId(Gid);
            rpEmpPrpEd.DataSource = dsPrpEd;
            rpEmpPrpEd.DataBind();
        }
    }

     


    public DataSet MasterFees_Slab_Fetch_DataSet()
    {
        try
        {
            string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
            SqlConnection con = new SqlConnection(connectionstr);

            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Nsic_Intranet_Record", con);
            //MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            con.Open();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
           // con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public DataSet MasterFees_Slab_Fetch_DataSet_byId(int GId)
    {
        try
        {
            string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
            SqlConnection con = new SqlConnection(connectionstr);

            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Nsic_Intranet_Record_inner_Degree", con);
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fInFTrack_1GeneratedNo", SqlDbType.Int).Value = GId;
            con.Open();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            // con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public DataSet MasterFees_Slab_Fetch_DataSet_Prop_Degree_byId(int GId)
    {
        try
        {
            string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
            SqlConnection con = new SqlConnection(connectionstr);

            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Nsic_Intranet_Record_inner_Prop_Degree", con);
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fInFTrack_1GeneratedNo", SqlDbType.Int).Value = GId;
            con.Open();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            // con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public DataSet MasterFees_Slab_Fetch_DataSet_EmpDetails_byId(int GId)
    {
        try
        {
            string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionStringIntranet"].ToString();
            SqlConnection con = new SqlConnection(connectionstr);

            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Nsic_Intranet_Record_inner_Employment", con);
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fInFTrack_1GeneratedNo", SqlDbType.Int).Value = GId;
            con.Open();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            //  con.Close();
            MyAdp.Dispose();
        }
        return MyDs;
    }


}