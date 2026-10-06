using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

/// <summary>
/// Summary description for HSCODE
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class HSCODE : System.Web.Services.WebService
{

    public HSCODE()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public HsCodeList[] Bind_Searchgrid(string searchitem, string codetype)
    {
        DataTable dt = new DataTable();
        List<HsCodeList> details = new List<HsCodeList>();
        clsCompanyBacklog objback = new clsCompanyBacklog();
        try
        {

        }
        catch (Exception ex)
        {

        }
        dt = objback.Company_Fetch_StoreHSCode(searchitem, codetype);
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
        return details.ToArray();
    }

}

public class HsCodeList
{
    public string HSID { get; set; }
    public string HSCode { get; set; }
    public string HSCodedescription { get; set; }
}

