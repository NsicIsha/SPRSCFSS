using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using NSICGP_VBClassLibrary;
using System.IO;
public partial class appAdministrator_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object 
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel 
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + "fddsfdsf.xls" + "\"");
            response.Cache.SetCacheability(HttpCacheability.NoCache);

            DataSet dsexport = new DataSet();
            dsexport = POI_Details(Convert.ToInt32(1), "", "");

            GridView dg1 = new GridView();
            dg1.DataSource = dsexport;
            dg1.DataBind();


            // create a string writer 
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid

                    htw.Write("<table><tr><td colspan='9' align='center'>" + "jj" + "</td></tr></table>");
                    htw.Write("<table><tr><td colspan='9'></td></tr></table>");
                    dg1.RenderControl(htw);
                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
                    //response.End();
                    response.Close();
                }

            }
        }
        catch (Exception ex)
        {

        }
    }

    public DataSet POI_Details(int Status, string fromdate, string todate)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_PointOfInterest_Admin", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@Status", SqlDbType.VarChar).Value = Status;

            MyAdp.SelectCommand.Parameters.Add("@fromDate", SqlDbType.VarChar).Value = fromdate;
            MyAdp.SelectCommand.Parameters.Add("@toDate", SqlDbType.VarChar).Value = todate;

            MyAdp.Fill(MyDs);


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }

        return MyDs;

    }
}