using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Data.SqlClient;
using System.Xml.Schema;
using System.Xml;
using System.Collections;
using System.Net;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using RestSharp;
using Newtonsoft.Json.Linq;
using RestSharp.Deserializers;
using NSICGP_VBClassLibrary;
public partial class APICheck : System.Web.UI.Page
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public DataSet Company_DetailsBy_PAN(string PAN)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Details_For_Unique_Code", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar).Value = PAN;


            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }


    public DataSet Fetch_PAN()
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_PAN_For_Unique_Code", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public void Company_Update_Unique_Code(int CompanyId, string UniqueCode)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_Unique_Code", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.VarChar, 5000).Value = CompanyId;
            MySqlComm.Parameters.Add("@UniqueCode", SqlDbType.VarChar, 5000).Value = UniqueCode;
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        var client = new RestClient("http://api.nsicnet.in/KYCAPI/api/KYC");
        var request = new RestRequest(Method.POST);
        //request.AddHeader("cache-control", "no-cache");
        //request.AddHeader("Connection", "keep-alive");
        //request.AddHeader("Content-Length", "172");
        //request.AddHeader("Accept-Encoding", "gzip, deflate");
        //request.AddHeader("Host", "api.nsicnet.in");
        //request.AddHeader("Cache-Control", "no-cache");
        //request.AddHeader("Accept", "*/*");

        //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("application/json", "BranchCode=C04&KYCNo=AAFFJ1304F&KycDocName=pan&LastName=JANA ENGINEERING WORKS&SocialCategory=general&EnterpriseCategory=micro&SchemeCode=SPR&SchemeStartDate=01-01-2019", ParameterType.RequestBody);
        IRestResponse response = client.Execute(request);
        HttpStatusCode statusCode = response.StatusCode;
        int numericStatusCode = (int)statusCode;
        var content = response.Content;
        if (numericStatusCode.ToString() != "400" && numericStatusCode.ToString() != "417" && numericStatusCode.ToString() != "500")
        {
            var Message = JObject.Parse(content).SelectToken("Message");
            var UniqueCode = JObject.Parse(content).SelectToken("UniqueCode");

            // Company_Update_Unique_Code(intCompanyId, UniqueCode.ToString());
            lblUniqueVCode.Text = UniqueCode.ToString();
            lblMsg.Text = Message.ToString();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet dsPan = new DataSet();
        dsPan = Fetch_PAN();
        string PAN = "";
        int intCompanyId = 0;

        int countrecord = dsPan.Tables[0].Rows.Count;// dsPan.Tables.Count;
        foreach (DataTable table in dsPan.Tables)
        {
            foreach (DataRow dr in table.Rows)
            {
                PAN = dr["fGPtbl_Company_PAN"].ToString();// dsPan.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
                intCompanyId = Convert.ToInt32(dr["fGPtbl_Company_Id"].ToString());




                //    PAN = dsPan.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
                //intCompanyId=Convert.ToInt32(dsPan.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());
                MyDs = new DataSet();
                MyDs = Company_DetailsBy_PAN(PAN);

                RestClient client = new RestClient("http://api.nsicnet.in/KYCAPI/api/KYC");

                RestRequest request = new RestRequest("login", Method.POST);

                request.AddHeader("Accept", "application/json");
                string bCode = MyDs.Tables[0].Rows[0]["fGtbl_MB_UniCode"].ToString();
                string strKYCNo = PAN;
                string strKycDocName = "pan";
                string strLastName = MyDs.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                string strSocialCategory = MyDs.Tables[0].Rows[0]["Enterprise_Social_Category"].ToString(); //"general";
                string strEnterpriseCategory = MyDs.Tables[0].Rows[0]["Category_Unit_Size"].ToString(); //"micro";
                string strSchemeCode = "SPR";
                string SchemeStartDate = MyDs.Tables[0].Rows[0]["ValidityFrom"].ToString(); //"01-01-2019";
                string SchemeEndDate = MyDs.Tables[0].Rows[0]["ValidityTo"].ToString();


                request.AddParameter(
            "application/json", "{\"BranchCode\": \"" + bCode + "\", \"KYCNo\": \" " + strKYCNo + "\",\"KycDocName\": \"" + strKycDocName + "\",\"LastName\": \"" + strLastName + "\",\"SocialCategory\": \"" + strSocialCategory + "\",\"EnterpriseCategory\": \"" + strEnterpriseCategory + "\",\"SchemeCode\": \"" + strSchemeCode + "\",\"SchemeStartDate\": \"" + SchemeStartDate + "\",\"SchemeEndDate\": \"" + SchemeEndDate + "\" }", // <- your JSON string
            ParameterType.RequestBody);

                //              request.AddParameter(
                //"application/json", "{ \"BranchCode\": \"" + bCode + "\", \"KYCNo\": \" " + strKYCNo + "\",\"KycDocName\": \"" + strKycDocName + "\",\"LastName\": \"" + strLastName + "\",\"SocialCategory\": \"" + strSocialCategory + "\",\"EnterpriseCategory\": \"" + strEnterpriseCategory + "\",\"SchemeCode\": \"" + strSchemeCode + "\",\"SchemeStartDate\": \"" + SchemeStartDate + "\"}", ParameterType.RequestBody);



                IRestResponse response = client.Execute(request);
                HttpStatusCode statusCode = response.StatusCode;
                int numericStatusCode = (int)statusCode;
                string statusDescription = response.StatusDescription;
                string rawResponse = response.Content;
                var content = response.Content;
               
                if (numericStatusCode.ToString() != "400" && numericStatusCode.ToString() != "500")
                {
                    var Message = JObject.Parse(content).SelectToken("Message");
                    var UniqueCode = JObject.Parse(content).SelectToken("UniqueCode");
                    Company_Update_Unique_Code(intCompanyId, UniqueCode.ToString());
                    lblUniqueVCode.Text = UniqueCode.ToString();
                    lblMsg.Text = Message.ToString();
                }
            }
        }
    }
}





