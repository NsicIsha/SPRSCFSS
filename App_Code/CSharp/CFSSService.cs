
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSICGP_VBClassLibrary;
using Org.BouncyCastle.Asn1.Ocsp;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;





public class CFSSService
{
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

    public CFSSService()
    {

    }
    public class UnitDetail
    {
        public string CFSSUnitID { get; set; }
        public string UnitName { get; set; }
        public string UnitAddress { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string Email_ID { get; set; }

        public string Pin_Code { get; set; }
        public string Mobile_Number { get; set; }
        public string UnitPAN { get; set; }
        public string SchemeID { get; set; }
        public string Unit_Udyam { get; set; }
        public string SocialCategory { get; set; }
        public string Constitution { get; set; }
        public string IndustryType { get; set; }


    }

    public class Root
    {
        public List<UnitDetail> Unit_Detail { get; set; }
    }
    //    int cfssUnitId = CallApi_CFSSUNITID(uam, mobile, pan);

    //    if (cfssUnitId > 0)
    //    {
    //        lblResult.Text = "CFSS Unit ID: " + cfssUnitId;
    //    }
    //    else
    //{
    //    lblResult.Text = "No data found or API failed.";
    //    lblResult.ForeColor = System.Drawing.Color.Red;
    //}
    public int CallApi_CFSSUNITID_Company(string UAM, string Mobile, string PAN)
    {
        try
        {
            string url = ConfigurationManager.AppSettings["CFSS_APIUrl"];
            string user = ConfigurationManager.AppSettings["CFSS_API_UserName"];
            string pass = ConfigurationManager.AppSettings["CFSS_API_Password"];

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls13;

            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(user, pass);

            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                Udyam = UAM,
                Mobile = Mobile,
                Pan = PAN
            });

            IRestResponse response = client.Execute(request);

            if (!response.IsSuccessful || string.IsNullOrEmpty(response.Content))
                return 0;

            string json = response.Content;

            Root data = JsonConvert.DeserializeObject<Root>(json);


            if (data == null || data.Unit_Detail == null || data.Unit_Detail.Count == 0)
                return 0;

            var unit = data.Unit_Detail[0];

            int cfssUnitId;
            if (unit != null && int.TryParse(Convert.ToString(unit.CFSSUnitID), out cfssUnitId))
            {
                return cfssUnitId;
            }

            return 0;
        }
        catch (Exception ex)
        {

            return 0;
        }
    }
    public int CallApi_CFSSUNITID(string UAM, string Mobile, string PAN)
    {
        string url = ConfigurationManager.AppSettings["CFSS_APIUrl"];
        string user = ConfigurationManager.AppSettings["CFSS_API_UserName"];
        string pass = ConfigurationManager.AppSettings["CFSS_API_Password"];

        ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12 |
            SecurityProtocolType.Tls11 |
            SecurityProtocolType.Tls13;

        var client = new RestClient(url);
        client.Authenticator = new HttpBasicAuthenticator(user, pass);

        var request = new RestRequest("", Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(new
        {
            Udyam = UAM,
            Mobile = Mobile,
            Pan = PAN
        });

        IRestResponse response = client.Execute(request);

        if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
        {
            string json = response.Content;
            Root data = JsonConvert.DeserializeObject<Root>(json);
            var unit = data.Unit_Detail[0];

            return Convert.ToInt32(unit.CFSSUnitID);
        }
        else
        {
            return 0;
        }
    }

    public string ProcessPayment(int CompanyId, int FormType, string apiUrl, string AppType)
    {
        try
        {
            DataTable dt;
            dt = new DataTable();
            if (AppType == "Fresh")
            {
                dt = GetData(CompanyId, FormType);
            }
            else if (AppType == "FreshPayVerification")
            {
                // dt = GetData_FreshPayVerification(CompanyId, FormType);
                dt = GetData(CompanyId, FormType);
            }
            else if (AppType == "Renewal")
            {
                dt = GetData(CompanyId, FormType);
                // dt = GetData_Renewal(CompanyId, FormType);
            }
            else if (AppType == "RA")
            {
                dt = GetData(CompanyId, FormType);
                // dt = GetData_Renewal(CompanyId, FormType);
            }
            else if (AppType == "RenewalPayVerification")
            {
                dt = GetData_PayVerification_R(CompanyId, FormType);
            }
            else if (AppType == "RAPayVerification")
            {
                dt = GetData_PayVerification_RA(CompanyId, FormType);
            }

            else if (AppType == "Amendment")
            {
                dt = GetData(CompanyId, FormType);
            }
            else if (AppType == "RenewalML10CR")
            {
                dt = GetData(CompanyId, FormType);
            }
            else if (AppType == "RAML10CR")
            {
                dt = GetData(CompanyId, FormType);
            }
            else if (AppType == "FRESH_R_RA_HEAD_APPROVAL")
            {
                dt = GetData_FRESH_R_RA_HEAD_APPROVAL(CompanyId, FormType);
            }



            string jsonRequest = BuildBranchApiJson(dt);

            string response = CallApi(jsonRequest, apiUrl);


            //dynamic obj = JsonConvert.DeserializeObject(response);

            //string errMsg = obj.Message.ToString();

            JObject obj = JObject.Parse(response);

            string msg = "";
            if (obj["message"] != null)
            {
                msg = obj["message"].ToString();
            }
            else if (obj["Message"] != null)
            {
                msg = obj["Message"].ToString();
            }


            if (msg == "Payment Received Successfully")
            {
                SavePaymentResponse(response, CompanyId);
                SaveAPIResponseDetails(response, CompanyId);
                return "SUCCESS";
            }
            else
            {
                SaveErrorLog(CompanyId, response, "Payment API failed");
                return "FAILED";
            }
        }
        catch (Exception ex)
        {
            SaveErrorLog(CompanyId, ex.Message, "Exception");
            return "ERROR: " + ex.Message;
        }
    }



    public int CheckPaymentSentToCFSS(int companyId, int formType, out int statusCode, out string message)
    {
        statusCode = 500;
        message = "Unknown error";

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CFSS_SP_Check_PaymentSentTOCFSS_Status", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", companyId);
                    cmd.Parameters.AddWithValue("@FormType", formType);

                    SqlParameter outId = new SqlParameter("@OutId", SqlDbType.Int);
                    outId.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outId);

                    SqlParameter outMsg = new SqlParameter("@OutMsg", SqlDbType.NVarChar, 200);
                    outMsg.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outMsg);

                    con.Open();
                    cmd.ExecuteNonQuery();

                    statusCode = Convert.ToInt32(outId.Value);
                    message = Convert.ToString(outMsg.Value);

                    //return statusCode == 420;
                }
            }
        }
        catch (Exception ex)
        {
            statusCode = 500;
            message = ex.Message;
            //return false;
        }

        return statusCode;
    }



    public DataTable GetData(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }
        return dt;
    }

    public DataTable GetData_FRESH_R_RA_HEAD_APPROVAL(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Head_Approval", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }
        return dt;
    }

    public DataTable GetData_FreshPayVerification(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch_VerifyPayment", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }
        return dt;
    }


    public DataTable GetData_PayVerification_R(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch_VerifyPayment_R", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }
        return dt;
    }

    public DataTable GetData_PayVerification_RA(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch_VerifyPayment_RA", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }

        return dt;
    }
    public DataTable GetData_Renewal(int CompanyId, int FormType)
    {
        DataTable dt = new DataTable();

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch_VerifyPayment", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@FormType", FormType);

            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                con.Open();
                da.Fill(dt);
            }
        }

        return dt;
    }
    public static string BuildBranchApiJson(DataTable dt)
    {
        //var finalResult = new List<object>();
        object finalResult = null;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {

                string branchState = GetString(row, "fGPtbl_MS_Id");

                string TranRefNo = GetString(row, "TranRefNo");


                string unitState = GetString(row, "fGPtbl_Company_HeadOffice_StateId");
                decimal fees = GetDecimal(row, "collectionAmount");
                decimal cgst = GetDecimal(row, "CGST");
                decimal sgst = GetDecimal(row, "SGST");
                decimal igst = GetDecimal(row, "IGST");
                decimal grantamount = GetDecimal(row, "Grant_Collection_Amount_UG");
                int category = GetInt(row, "fGPtbl_CUnit_CategoryCastId");
                var transactions = new List<object>();
                if (category == 10 || category == 11)
                {
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        // collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = 100,
                        Narration = row["Narration_Grant"].ToString()
                    });
                }
                else
                {
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_FE"].ToString(),
                        //  collectionType = row["collectionType_FE"].ToString(),
                        collectionAmount = fees,
                        Narration = row["Narration"].ToString()
                    });

                }
                if (branchState == unitState)
                {

                    //if (cgst > 0)
                    //{
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_CGST"].ToString(),
                        // collectionType = row["collectionType_CGST"].ToString(),
                        collectionAmount = cgst,
                        Narration = row["Narration_CGST"].ToString()
                    });
                    // }


                    //if (sgst > 0)
                    //{
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_SGST"].ToString(),
                        // collectionType = row["collectionType_SGST"].ToString(),
                        collectionAmount = sgst,
                        Narration = row["Narration_SGST"].ToString()
                    });


                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_IGST"].ToString(),
                        // collectionType = row["collectionType_IGST"].ToString(),
                        collectionAmount = 0,
                        Narration = row["Narration_IGST"].ToString()
                    });

                    // }
                }
                else
                {

                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_CGST"].ToString(),
                        // collectionType = row["collectionType_CGST"].ToString(),
                        collectionAmount = 0,
                        Narration = row["Narration_CGST"].ToString()
                    });

                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_SGST"].ToString(),
                        // collectionType = row["collectionType_SGST"].ToString(),
                        collectionAmount = 0,
                        Narration = row["Narration_SGST"].ToString()
                    });


                    //if (igst > 0)
                    //{
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_IGST"].ToString(),
                        // collectionType = row["collectionType_IGST"].ToString(),
                        collectionAmount = igst,
                        Narration = row["Narration_IGST"].ToString()
                    });
                    //}
                }


                //if (grantamount > 0)
                //{
                //transactions.Add(new
                //{
                //    collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                //   // collectionType = row["collectionType_Grant"].ToString(),
                //    collectionAmount = grantamount,
                //    Narration = row["Narration_Grant"].ToString()
                //});
                // }


                if (category == 10 || category == 11)
                {
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        // collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = grantamount,
                        Narration = row["Narration_Grant"].ToString()
                    });
                }

                else
                {
                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        // collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = 0,
                        Narration = row["Narration_Grant"].ToString()
                    });
                }



                //var data = new
                //{
                //    BranchID = GetString(row, "fGPtbl_CUnit_BranchId"),
                //    CFSSUnitID = GetString(row, "CFSSUnitID"),
                //    SchemeID = GetString(row, "SchemeID"),
                //    CollectionBankID = GetString(row, "Bank_LedgerId"),
                //    TransactionDate = GetString(row, "TransactionDate"),
                //    TransactionDetails = transactions
                //};

                //finalResult.Add(data);

                finalResult = new
                {
                    BranchCode = row["MBRI_Code"].ToString(),
                    Version = row["Version"].ToString(),
                    CFSSUnitID = row["CFSSUnitID"].ToString(),
                    SchemeID = row["SchemeID"].ToString(),
                    CollectionBankID = row["Bank_LedgerId"].ToString(),
                    TransactionDate = row["TransactionDate"].ToString(),
                    ApplicationTypeID= row["ApplicationTypeID"].ToString(),
                    TranRefNo = row["TranRefNo"].ToString(),
                    Narration= row["Narration"].ToString(),
                    Type = row["Type"].ToString(),
                    TransactionDetails = transactions
                };
                break;

                //  return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            }
        }
        else
        {
            // finalResult.Add(new { message = "Record not found" });
            finalResult = new { message = "Record not found" };
            return JsonConvert.SerializeObject(finalResult, Newtonsoft.Json.Formatting.Indented);
        }

        return JsonConvert.SerializeObject(finalResult, Newtonsoft.Json.Formatting.Indented);


    }

    private static string GetString(DataRow row, string column)
    {
        return row.Table.Columns.Contains(column) && row[column] != DBNull.Value
            ? row[column].ToString()
            : "";
    }

    private static decimal GetDecimal(DataRow row, string column)
    {
        return row.Table.Columns.Contains(column) && row[column] != DBNull.Value
            ? Convert.ToDecimal(row[column])
            : 0;
    }

    private static int GetInt(DataRow row, string column)
    {
        return row.Table.Columns.Contains(column) && row[column] != DBNull.Value
            ? Convert.ToInt32(row[column])
            : 0;
    }

    private string CallApi(string json, string URL)
    {
        //string url = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        string user = ConfigurationManager.AppSettings["CFSS_API_UserName"];
        string pass = ConfigurationManager.AppSettings["CFSS_API_Password"];

        ServicePointManager.SecurityProtocol =
            SecurityProtocolType.Tls12 |
            SecurityProtocolType.Tls11 |
            SecurityProtocolType.Tls13;

        var client = new RestClient(URL);
        client.Authenticator = new HttpBasicAuthenticator(user, pass);

        var request = new RestRequest("", Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddParameter("application/json", json, ParameterType.RequestBody);

        IRestResponse response = client.Execute(request);

        if (response != null)
        {
            return !string.IsNullOrEmpty(response.Content)
                ? response.Content
                : response.ErrorMessage;
        }

        return "No Response";


        // return response.IsSuccessful ? response.Content : response.Content;

    }

    private void SavePaymentResponse(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<PaymentResponse>(responseString);

        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIPaymentResponse", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            cmd.Parameters.AddWithValue("@JsonData_responseString", responseString);
            cmd.Parameters.AddWithValue("@Message", result.message);
            cmd.Parameters.AddWithValue("@fGPtbl_PD_CFSS_PaymentId", result.Id);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    private void SaveErrorLog(int? companyId, string responseString, string eventName)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        using (SqlCommand cmd = new SqlCommand("CFSS_SP_Insert_CFSSAPI_Error", con))
        {
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@CompanyId", (object)companyId ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ResponseString", (object)responseString ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@EventName", eventName);

            con.Open();
            cmd.ExecuteNonQuery();
        }
    }


    public class BranchApiRequest
    {
        public string BranchID { get; set; }
        public string BranchCode { get; set; }
        public string Version { get; set; }
        public string CFSSUnitID { get; set; }
        public string SchemeID { get; set; }
        public string CollectionBankID { get; set; }
        public string TransactionDate { get; set; }

        public string Narration { get; set; }

        public string Type { get; set; }
        
        public string TranRefNo { get; set; }
        
        public List<TransactionDetail> TransactionDetails { get; set; }
    }

    public class TransactionDetail
    {
        public string collectionLedgerID { get; set; }
        public decimal collectionAmount { get; set; }
        public string Narration { get; set; }
    }

    public class PaymentResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string Id { get; set; }
    }
    public class BankDetails
    {
        public string BranchID { get; set; }
        public string BankAccountNo { get; set; }
        public string Bank_Name { get; set; }
        public string Bank_IFSC_Code { get; set; }
        public string BranchName { get; set; }
        public string LedgerID { get; set; }
        public string Ledger_Name { get; set; }
    }

    public class Root_Bank
    {
        public List<BankDetails> Bank_Details { get; set; }
    }

    public List<BankDetails> GetBankList(string branchId)
    {
        try
        {
            string APIUrl = ConfigurationManager.AppSettings["CFSS_BankDeailsAPIUrl"];
            string User_Name = ConfigurationManager.AppSettings["CFSS_API_UserName"];
            string Password = ConfigurationManager.AppSettings["CFSS_API_Password"];

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls13;

            var client = new RestClient(APIUrl);
            client.Authenticator =
                new HttpBasicAuthenticator(User_Name, Password);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");

            request.AddJsonBody(new
            {
                BranchID = branchId
            });

            IRestResponse response = client.Execute(request);

            if (response != null &&
                response.IsSuccessful &&
                !string.IsNullOrEmpty(response.Content))
            {
                Root_Bank data =
                    JsonConvert.DeserializeObject<Root_Bank>(response.Content);

                if (data != null && data.Bank_Details != null)
                {
                    return data.Bank_Details;
                }
            }
        }
        catch
        {
        }

        return new List<BankDetails>();
    }

    public void BindBankDropDown(DropDownList ddlBank, string branchId)
    {
        List<BankDetails> list = GetBankList(branchId);

        ddlBank.DataSource = list.Select(x => new
        {
            LedgerID = x.LedgerID,
            BankAccount_Name = x.BankAccountNo + " - " + x.Bank_Name
        }).ToList();

        ddlBank.DataTextField = "BankAccount_Name";
        ddlBank.DataValueField = "LedgerID";
        ddlBank.DataBind();

        ddlBank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
    }


    public DataTable GetBranchDetails(int branchId)
    {
        DataTable dt = new DataTable();

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CFSS_SP_CFSS_BranchId_By_SPRS_BRANCHID", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BranchId", branchId);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
        }
        catch
        {
        }

        return dt;
    }

    public string GetCFSSBranchCode(int branchId)
    {
        DataTable dt = GetBranchDetails(branchId);

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["MBRI_Code"].ToString();
        }

        return "";
    }

    public string GetBranchName(int branchId)
    {
        DataTable dt = GetBranchDetails(branchId);

        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["fGPtbl_MB_Name"].ToString();
        }

        return "";
    }


    public class RootResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DataItem> Data { get; set; }
    }
    public class DataItem
    {
        public string UniqueID { get; set; }
        public string Message { get; set; }
    }


    public void SaveAPIResponseDetails(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<PaymentResponse>(responseString);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            //cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
            //cmd.Parameters.AddWithValue("@JsonData_responseString", responseString);
            //cmd.Parameters.AddWithValue("@Message", result.message);
            //cmd.Parameters.AddWithValue("@fGPtbl_PD_CFSS_PaymentId", result.Id);


            //if (result.Data != null && result.Data.Count > 0)
            //{
            //var item = result.Data[0];

            using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIUnitResponse_InsideClass", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@JsonData_responseString", responseString);
                cmd.Parameters.AddWithValue("@UniqueID", result.Id); //item.UniqueID
                cmd.Parameters.AddWithValue("@Message", result.message);
                cmd.Parameters.AddWithValue("@IsSuccess", result.success);
                cmd.Parameters.AddWithValue("@MainMessage", result.message);
                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                cmd.ExecuteNonQuery();
            }
            //}
        }
    }


    public DataSet FetchBankDetails_CFSS(int PID)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_PaymentDetail_ByPaymentDetailId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.VarChar, 50).Value = PID;
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


    public DataSet FetchCompanyId_CFSS(string UAM, string PAN)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_FetchCompanyId_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@UAM", SqlDbType.VarChar, 50).Value = UAM;
            MyAdp.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar, 50).Value = PAN;
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

    public DataSet FetchCompanyUAM_PAN_CFSS(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_FetchCompanyUAM_PAN_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId; 
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


    //public bool UpdateCFSSUnitID_IfNull(int companyId, int cfssUnitId)
    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

    //    try
    //    {
    //        using (SqlConnection con = new SqlConnection(connectionString))
    //        {

    //                using (SqlCommand cmd = new SqlCommand("CFSS_GPSP_Update_CFSSUnitID_IfNull", con))
    //            //using (SqlCommand cmd = new SqlCommand("GPsp_Update_CFSSUnitID_IfNull", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                cmd.Parameters.AddWithValue("@fGPtbl_Company_Id", companyId);
    //                cmd.Parameters.AddWithValue("@CFSSUnitID", cfssUnitId);
    //                con.Open();
    //                int rowsAffected = cmd.ExecuteNonQuery();
    //                return rowsAffected > 0;
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        return false;
    //    }
    //}


    public bool UpdateCFSSUnitID_IfNull(int companyId, int cfssUnitId)
    {
        string connectionString =
            ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(
                "CFSS_GPSP_Update_CFSSUnitID_IfNull", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@CFSSUnitID", SqlDbType.Int).Value = cfssUnitId;

                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int companyRows = Convert.ToInt32(reader["CompanyRows"]);
                        int paymentRows = Convert.ToInt32(reader["PaymentRows"]);

                        // True if either table was updated
                        return companyRows > 0 || paymentRows > 0;
                    }
                }

                return false;
            }
        }
        catch (Exception ex)
        {
            // Log exception here
            return false;
        }
    }


    public bool PostUnitDetailsToCFSS(int company_Id, string json, out string errorMessage)
    {
        errorMessage = string.Empty;

        try
        {
            string userName = ConfigurationManager.AppSettings["CFSS_API_UserName"];
            string password = ConfigurationManager.AppSettings["CFSS_API_Password"];
            string url = ConfigurationManager.AppSettings["CFSS_UNITPostingAPIUrl"];

            if (string.IsNullOrWhiteSpace(json))
            {
                errorMessage = "JSON is empty.";
                return false;
            }

            ServicePointManager.SecurityProtocol =
                SecurityProtocolType.Tls12 |
                SecurityProtocolType.Tls11 |
                SecurityProtocolType.Tls13;

            var client = new RestClient(url);
            client.Authenticator = new HttpBasicAuthenticator(userName, password);

            var request = new RestRequest("", Method.POST);
            request.AddHeader("Content-Type", "application/json"); 
            request.AddParameter("application/json", json, ParameterType.RequestBody); 
            IRestResponse response = client.Execute(request); 
            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                SaveUnitDetailsResponse(response.Content, company_Id);
                return true;
            }
            else
            {
                errorMessage = "CFSS API failed: " + response.Content; 
                SaveErrorLog(company_Id, response.Content,
                    "Unit Details Response failed from CFSS Server.");
                return false;
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
            SaveErrorLog(company_Id, ex.ToString(), "Exception in CFSS API call");
            return false;
        }
    }

    public void SaveUnitDetailsResponse(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<RootResponse>(responseString);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            if (result.Data != null && result.Data.Count > 0)
            {
                var item = result.Data[0];

                using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIUnitResponse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@JsonData_responseString", responseString);
                    cmd.Parameters.AddWithValue("@UniqueID", item.UniqueID);
                    cmd.Parameters.AddWithValue("@Message", item.Message);
                    cmd.Parameters.AddWithValue("@IsSuccess", result.Success);
                    cmd.Parameters.AddWithValue("@MainMessage", result.Message);
                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }


    public static string GetUnitDetailJson_R_RA(int companyId)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        string jsonResult = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CFSS_PULL_UNITDATA_FOR_CFSS_POSTING_R_RA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", companyId);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    StringBuilder sb = new StringBuilder();

                    while (dr.Read())
                    {
                        sb.Append(dr[0].ToString());
                    }

                    jsonResult = sb.ToString();
                }
            }
        }

        return jsonResult;
    }

}





//string cs = ConfigurationManager.ConnectionStrings["YourConnection"].ConnectionString;

//CFSSService service = new CFSSService(cs);

//string response = service.CallApiExample();

//lblMessage.Text = response;
