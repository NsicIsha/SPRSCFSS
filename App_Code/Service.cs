using System.Data;
using System.Data.SqlClient;

// NOTE: If you change the class name "Service" here, you must also update the reference to "Service" in Web.config.
public class Service : IService
{
    string Connectionstring = "";
    SqlConnection sqlcon;
    SqlCommand sqlcmd;
    SqlDataAdapter sqladp;
    DataSet ds;


    #region IService Members

    public DataSet Getdata()
    {
        // Connectionstring = "server=220.156.188.70,2001; database=nsicspronline; UID=nsicspronline; Pwd=N$pr34L1n3; Max Pool Size=10000; Connection Timeout=100000;";
        Connectionstring = "server=220.156.188.70,2001; database=nsicspronline; UID=nsicspronline; Pwd=N$pr34L1n3; Max Pool Size=10000; Connection Timeout=100000;";
        sqlcon = new SqlConnection(Connectionstring);
        sqlcon.Open();
        ds = new DataSet();

        sqladp = new SqlDataAdapter("GPSp_WcfServiceProcedute", sqlcon);
        sqladp.SelectCommand.CommandType = CommandType.StoredProcedure;
        sqladp.SelectCommand.CommandTimeout = 120 * 60;
        sqladp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {

        }
        return ds;

    }


    #endregion
    #region IService1 Members
    public DataSet FethData()
    {
        DataSet ds_getdata = new DataSet();
        ds_getdata = Getdata();
        return ds_getdata;
        // throw new NotImplementedException();
    }

    #endregion
}

