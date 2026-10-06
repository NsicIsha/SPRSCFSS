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
using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Collections.Generic;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_SearchHSCODEREPORT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static HsCodeList[] Bind_Searchgrid(string searchitem, string codetype)
    {
        DataTable dt = new DataTable();
        List<HsCodeList> details = new List<HsCodeList>();
        //    clsCompanyBacklog objback = new clsCompanyBacklog();
        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);
        try
        {
            SqlCommand cmd = new SqlCommand("GP_sp_searchHSCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Product", SqlDbType.VarChar, 5000).Value = searchitem == "" ? null : searchitem;
            cmd.Parameters.Add("@tppe", SqlDbType.Int).Value = codetype == "" ? null : codetype;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        HsCodeList hs = new HsCodeList();
                        hs.HSID = dtrow["Id"].ToString();
                        hs.HSCode = dtrow["Productcode"].ToString();
                        hs.HSCodedescription = dtrow["Productdescription"].ToString();
                        details.Add(hs);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        return details.ToArray();

    }

    public class HsCodeList
    {
        public string HSID { get; set; }
        public string HSCode { get; set; }
        public string HSCodedescription { get; set; }
    }

    public class ZoneList
    {
        public string ZoneID { get; set; }
        public string ZoneName { get; set; }
    }
    public class BranchList
    {
        public string BranchID { get; set; }
        public string ZoneID { get; set; }
        public string BranchName { get; set; }

    }
    public class CountryList
    {
        public string CountryID { get; set; }
        public string CountryName { get; set; }
    }
    public class StateList
    {
        public string StateID { get; set; }
        public string CountryID { get; set; }
        public string StateName { get; set; }
    }
    public class CityList
    {
           public string CityID {get;set;}
             public string StateID {get;set;}
             public string CityName {get; set;}
    }
}
