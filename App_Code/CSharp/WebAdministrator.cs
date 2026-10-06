using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WebAdministrator
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebAdministrator : System.Web.Services.WebService {

    DataSet Myds;
    clsMasterUser objUser;

    public WebAdministrator () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        Myds = new DataSet();
        objUser = new clsMasterUser();
        Myds = objUser.MasterUser_FetchAutocomplete_DataSet(prefixText);
        DataTable dt = Myds.Tables[0];
        var res = from n in dt.AsEnumerable()
                  where n.Field<string>("fGPtbl_MU_Name").ToUpper().Contains(prefixText.ToUpper())
                  select n.Field<string>("fGPtbl_MU_Name");
        return res.ToArray<string>();
    }


  
    
}

