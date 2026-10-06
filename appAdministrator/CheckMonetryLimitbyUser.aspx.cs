using Newtonsoft.Json;
using NSICGP_VBClassLibrary;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_CheckMonetryLimitbyUser : System.Web.UI.Page
{

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    clsCompany objCompany;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int BranchId = 0;
    string OriginalFilePath;
    string filePath;
    string file1;
    int FileStatus_1;
    string Date = string.Empty;
    string BranchName = string.Empty;
    public static int CompanyId { get; set; }



    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        FunRequest();

        //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        clsTrans_UserBranch objtub = new clsTrans_UserBranch();
        if (RoleId != 1 && RoleId != 10)
        {
            BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            BranchName = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"]);
        }
        OriginalFilePath = "./Document/InspectionMonetry/";
        filePath = Server.MapPath("~").ToString() + "/Document/InspectionMonetry/";
        try
        {
            if (!Page.IsPostBack)
            {
                txt_last_date_visited.Text = "";
                Bind_gridview();



                #region Fetch Bank Details From CFSS


                string APIUrl = System.Configuration.ConfigurationManager.AppSettings["CFSS_BankDeailsAPIUrl"];
                string User_Name = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_UserName"];
                string Password = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_Password"];

                var client = new RestClient(APIUrl);
                // BASIC AUTH
                //client.Authenticator = new HttpBasicAuthenticator("Admin", "Jayam@123");
                client.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");

                int BranchId_MBRICode = 0;
                objCompany = new clsCompany();
                BranchId_MBRICode = objCompany.CFSS_MasterBranch_Fetch_BranchId(UserId);


                request.AddJsonBody(new
                {
                    BranchID = BranchId_MBRICode
                });


                ServicePointManager.SecurityProtocol =
                        SecurityProtocolType.Tls12 |
                        SecurityProtocolType.Tls11 |
                        SecurityProtocolType.Tls13;

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                {
                    string json = response.Content;
                    Root_Bank data = JsonConvert.DeserializeObject<Root_Bank>(json);

                    //if (data != null && data.Bank_Details != null && data.Bank_Details.Count > 0)
                    //{
                    //    GridView1.DataSource = data.Bank_Details;
                    //    GridView1.DataBind();
                    //}

                    Session["BankList"] = data.Bank_Details;

                    var bankList = data.Bank_Details
                    .Select(x => new
                    {
                        LedgerID = x.LedgerID,
                        BankAccount_Name = x.BankAccountNo + " - " + x.Bank_Name
                    }).ToList();

                    ddlBank.DataSource = bankList;
                    ddlBank.DataTextField = "BankAccount_Name";
                    ddlBank.DataValueField = "LedgerID";
                    ddlBank.DataBind();

                    ddlBank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
                }
                else
                {
                    return;

                }
                #endregion


            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            BranchId = 0;
            BranchName = "";
        }
    }
    protected void grd_checkMonetryLimit_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_checkMonetryLimit.PageIndex = e.NewPageIndex;
        Bind_gridview();
    }

    void Clear()
    {
        txt_chqddNo.Text = "";
        txt_BankName.Text = "";
        txt_BankBranch.Text = "";
        txt_PayableAt.Text = "";
        txt_chqddAmnt.Text = "";

        txt_chqddate.Text = "";
        txtChequeDDPaymentRemarks.Text = "";
        ddlBank.SelectedIndex = -1;
        
    }
    protected void grd_checkMonetryLimit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edt")
        {
            int companyId = Convert.ToInt32(e.CommandArgument);

            // Same as:
            // hdn_CompanyId.value = id;
            hdn_CompanyId.Value = companyId.ToString();

           


            // Same as:
            // lbl_hidden_companyid.innerHTML = id;
            lbl_hidden_companyid.Text = companyId.ToString();

            // Same as:
            // td_submitbutton.style.display = "block";
            // nsicradio.style.display = "block";
            // inspection_agencytable_tr.style.display = "block";

            ScriptManager.RegisterStartupScript(
                this,
                this.GetType(),
                "ShowCompanySection",
                @"
                document.getElementById('td_submitbutton').style.display = 'block';
                document.getElementById('nsicradio').style.display = 'block';
                document.getElementById('inspection_agencytable_tr').style.display = 'block';
            ",
                true
            );
        }
    }

    public class ApiResponse_CAM
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string data { get; set; } // XML string
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

    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(
                 this,
                 this.GetType(),
                 "ShowCompanySection",
                 @"
                document.getElementById('td_submitbutton').style.display = 'block';
                document.getElementById('nsicradio').style.display = 'block';
                document.getElementById('inspection_agencytable_tr').style.display = 'block';
            ",
                 true
             );


        List<BankDetails> bankList = (List<BankDetails>)Session["BankList"];

        string ledgerID = ddlBank.SelectedValue;

        var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

        if (selectedBank != null)
        {
            Session["SelectedBank"] = selectedBank;
        }


    }
    public void Bind_gridview()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds = new DataSet();
            ds = objCompany.Fetch_MonetryLimitByUser(BranchId);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_checkMonetryLimit.DataSource = ds.Tables[0];
                grd_checkMonetryLimit.DataBind();
            }
            else
            {
                ds.Dispose();
                grd_checkMonetryLimit.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
    // Function For Upload File
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        try
        {
            string companyidtext = lbl_hidden_companyid.Text;

            string Companyidvalue = Convert.ToString(hdn_CompanyId.Value);

            if (hdn_CompanyId.Value == "0")
            {
                // Bind_gridview();
                return;
            }
            else
            {
                objCompany = new clsCompany();
                object postedFile = "";
                string filenameWithoutExtn = "";
                string fileExtn = "";
                string newFileName = "";
                string contentType = "";
                string TotalPath = "";
                if (txt_last_date_visited.Text == "")
                {
                    Date = "";
                }
                else
                {
                    Date = txt_last_date_visited.Text.ToString();
                }
                bool file = ftp_upload.HasFile;
                // return;
                if (ftp_upload.HasFile)
                {
                    Date = "";
                    postedFile = ftp_upload.PostedFile;
                    filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(ftp_upload.PostedFile.FileName);
                    fileExtn = System.IO.Path.GetExtension(ftp_upload.PostedFile.FileName);
                    if (filenameWithoutExtn != "")
                    {
                        contentType = ftp_upload.PostedFile.ContentType.ToString();
                        int contentLength = Convert.ToInt32(ftp_upload.PostedFile.ContentLength.ToString());
                        newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration
                        int AllowedContentLengthInMB = 3;
                        int AllowedContentLengthInBytes = AllowedContentLengthInMB * 1024 * 1024;
                        if (contentLength > AllowedContentLengthInBytes)
                        {
                            Label1.Text = "Document size can't be greater then 3 MB.";
                            FileStatus_1 = 1;
                        }
                        else
                        {
                            Label1.Text = "";
                            FileStatus_1 = 2;
                        }
                        if (FileStatus_1 == 2)
                        {
                            ftp_upload.PostedFile.SaveAs(filePath + newFileName);
                        }
                    }
                }
                TotalPath = newFileName;
                // return;



                #region Capture Payment Details




                List<BankDetails> bankList = (List<BankDetails>)Session["BankList"];
                 

                string ledgerID = ddlBank.SelectedValue;

                var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

                //if (selectedBank != null)
                //{
                //    Session["SelectedBank"] = selectedBank;
                //}

                bool selected = false;
                string Bank_BranchID = "";
                string Bank_AccountNo = "";
                string Bank_BankName = "";
                string Bank_IFSC = "";
                string Bank_Branchname = "";
                string Bank_LedgerId = "";
                string Bank_Ledgername = "";

                BankDetails bank = selectedBank;
                Bank_BranchID = bank.BranchID;
                Bank_AccountNo = bank.BankAccountNo;
                Bank_BankName = bank.Bank_Name;
                Bank_IFSC = bank.Bank_IFSC_Code;
                Bank_Branchname = bank.BranchName;
                Bank_LedgerId = bank.LedgerID;
                Bank_Ledgername = bank.Ledger_Name;

                clsCompany objCompCFSS = new clsCompany();
                bool TF = FunUploadFile_1();
                int PaymentType;
                string PaybalAt, BankName, BranchName, ChqDdNo, CashReceiptNo;
                string ChqDdate = null;
                PaybalAt = "";
                BankName = "";
                BranchName = "";
                ChqDdNo = "";
                CashReceiptNo = "";
                string PaymentRemarks = "";
                PaybalAt = txt_PayableAt.Text;
                BankName = txt_BankName.Text;
                BranchName = txt_BankBranch.Text;
                ChqDdNo = txt_chqddNo.Text;
                ChqDdate = txt_chqddate.Text.Trim();
                PaymentRemarks = txtChequeDDPaymentRemarks.Text;
                PaymentType = Convert.ToInt32(radPay.SelectedValue);   //Convert.ToInt32(Session["PaymentType"].ToString());
                Decimal UnitAmount = 0, PaidAmount = 0;
                if (txt_chqddAmnt.Text.Trim().ToString() != "")
                {
                    PaidAmount = Convert.ToDecimal(txt_chqddAmnt.Text.Trim().ToString());
                }

                objCompany = new clsCompany();
                BranchId = objCompany.CFSS_MasterBranch_Fetch_BranchId(UserId);


                #region  If Unit Is not Registered with CFSS then call API to push data to CFSS Portal
                int CFSSUNITID = 0;
                int CompanyId = 0;
                DataSet dsComp = new DataSet();
                CFSSService obj = new CFSSService();
                dsComp = obj.FetchCompanyUAM_PAN_CFSS(Convert.ToInt32(hdn_CompanyId.Value));
                string UAM = "";
                string PAN = "";
                UAM = dsComp.Tables[0].Rows[0]["fGtbl_Company_AdharNumber"].ToString();
                PAN = dsComp.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

                CFSSUNITID = obj.CallApi_CFSSUNITID_Company(UAM, "", PAN);

                //dsComp = obj.FetchCompanyId_CFSS(txt_aahar.Text, txt_1.Text);
                //CompanyId = Convert.ToInt32(dsComp.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());
                 
                if (CFSSUNITID != 0)
                {
                    bool isUpdated = obj.UpdateCFSSUnitID_IfNull(Convert.ToInt32(hdn_CompanyId.Value), CFSSUNITID);
                }
                else
                {
                    string json = CFSSService.GetUnitDetailJson_R_RA(Convert.ToInt32(hdn_CompanyId.Value));
                    string errorMessage;
                    bool isSuccess = obj.PostUnitDetailsToCFSS(Convert.ToInt32(hdn_CompanyId.Value), json, out errorMessage);
                    if (!isSuccess)
                    {
                        lblMsg.Text = errorMessage;
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                    else
                    {
                        dsComp = obj.FetchCompanyUAM_PAN_CFSS(Convert.ToInt32(hdn_CompanyId.Value));
                        CFSSUNITID = Convert.ToInt32(dsComp.Tables[0].Rows[0]["CFSSUnitID"].ToString());
                    }
                }

                #endregion

                clsTrans_UserBranch objtub = new clsTrans_UserBranch();
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);



                //2 For Renewal & 3 For RA
                int Errorid = Company_Insert_Step1_CFSS(Convert.ToInt32(hdn_CompanyId.Value), PaymentType, ChqDdNo, BankName, BranchName, PaybalAt, PaidAmount,
         newFileName, OriginalFilePath, ChqDdate, PaymentRemarks,
        Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername, CFSSUNITID, 2, BranchId);

                #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB


                int FormType = 50; // For Renewal and for RA=51
                string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
                CFSSService service = new CFSSService();
                string result = service.ProcessPayment(Convert.ToInt32(hdn_CompanyId.Value), FormType, apiUrl, "RenewalML10CR");
                if (result == "SUCCESS")
                {
                    int K = objCompany.Fetch_BrachUser_Inspection_tencore(Convert.ToInt32(hdn_CompanyId.Value), Date, TotalPath);
                    if (K == 1)
                    {
                        Bind_gridview();
                        if (ftp_upload.HasFile)
                        {
                            filePath = "";
                            newFileName = "";
                            txt_last_date_visited.Text = "";
                            rdl_inspaction.SelectedValue = "1";
                            rdl_inspaction.SelectedValue = "1";
                            hdn_CompanyId.Value = "0";
                        }
                        hdn_CompanyId.Value = "0";
                        //   inspection_agencytable_tr.Visible = false;
                        // btn_submit.Visible = false;
                        //  nsicradio.Visible = false;
                        //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Submit Suceesfully!!!');", true);
                    }
                    else
                    {
                        Bind_gridview();
                        if (ftp_upload.HasFile)
                        {
                            filePath = "";
                            newFileName = "";
                            hdn_CompanyId.Value = "0";
                        }
                        hdn_CompanyId.Value = "0";
                        // inspection_agencytable_tr.Visible = false;
                        //    btn_submit.Visible = false;
                        //  nsicradio.Visible = false;
                        //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Server Busy!!!');", true);
                    }

                }
                else
                {
                    lblMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }


                 

                #endregion

                Clear();

                #endregion Capture Payment Details

                //////int K = objCompany.Fetch_BrachUser_Inspection_tencore(Convert.ToInt32(hdn_CompanyId.Value), Date, TotalPath);


                //////if (K == 1)
                //////{
                //////    Bind_gridview();
                //////    if (ftp_upload.HasFile)
                //////    {
                //////        filePath = "";
                //////        newFileName = "";
                //////        txt_last_date_visited.Text = "";
                //////        rdl_inspaction.SelectedValue = "1";
                //////        rdl_inspaction.SelectedValue = "1";
                //////        hdn_CompanyId.Value = "0";
                //////    }
                //////    hdn_CompanyId.Value = "0";
                //////    //   inspection_agencytable_tr.Visible = false;
                //////    // btn_submit.Visible = false;
                //////    //  nsicradio.Visible = false;
                //////    //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Submit Suceesfully!!!');", true);
                //////}
                //////else
                //////{
                //////    Bind_gridview();
                //////    if (ftp_upload.HasFile)
                //////    {
                //////        filePath = "";
                //////        newFileName = "";
                //////        hdn_CompanyId.Value = "0";
                //////    }
                //////    hdn_CompanyId.Value = "0";
                //////    // inspection_agencytable_tr.Visible = false;
                //////    //    btn_submit.Visible = false;
                //////    //  nsicradio.Visible = false;
                //////    //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Server Busy!!!');", true);
                //////}
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }

    public int Company_Insert_Step1_CFSS(int CompanyId, int PaymentType, string CHQDD_UTR,
       string BankName, string BranchName, string PayableAt, decimal Amount, string FileName,
       string FilePath, string PaymentDate, string Remarks,

       string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername,
       int CFSSUnitID, int FormTypeId, int BranchId

       )
    {
        int ErrorId = 0;

        CompanyInsertResult result = new CompanyInsertResult();


        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_GreaterThan10CR_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_UTR", SqlDbType.VarChar).Value = CHQDD_UTR;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar).Value = BranchName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar).Value = PayableAt;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Decimal).Value = Amount;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar).Value = FileName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar).Value = FilePath;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.VarChar).Value = PaymentDate;
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar).Value = Remarks;
            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;
            MySqlComm.Parameters.Add("@CFSSUnitID", SqlDbType.Int).Value = CFSSUnitID;
            MySqlComm.Parameters.Add("@FormTypeId", SqlDbType.VarChar).Value = FormTypeId;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();

            ErrorId = outError.Value != DBNull.Value ? Convert.ToInt32(outError.Value) : 0;
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


        return ErrorId;

        //return tblId;
    }
    public class CompanyInsertResult
    {
        public int ErrorId { get; set; }
        public int CompanyId { get; set; }
    }
    private bool FunUploadFile_1()
    {
        string newFileName = "";
        bool returnvalue = false;
        try
        {
            if (FileUpload1.HasFile)
            {
                object postedFile = FileUpload1.PostedFile;
                string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                string fileExtn = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (filenameWithoutExtn != "")
                {
                    string contentType = FileUpload1.PostedFile.ContentType.ToString();
                    int contentLength = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());
                    newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

                    int AllowedContentLengthInKB = 1000;
                    int AllowedContentLengthInBytes = AllowedContentLengthInKB * 1024;
                    if (contentLength > AllowedContentLengthInBytes)
                    {
                        Label1.Text = "Document size can't be greater than 1 MB.";
                        FileStatus_1 = 1;
                        return false;
                    }
                    else
                    {
                        Label1.Text = "";
                        FileStatus_1 = 2;
                    }

                    if (FileStatus_1 == 2)
                    {
                        FileUpload1.PostedFile.SaveAs(filePath + newFileName);
                        returnvalue = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return returnvalue;
    }
    protected void grd_checkMonetryLimit_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string compid = string.Empty; string compname = string.Empty; string branchname = string.Empty;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCompanyName = (Label)e.Row.FindControl("lbl_Company_Name");
                Label hdn_Companyid = (Label)e.Row.FindControl("hdn_Company_id");
                HyperLink lnkview = (HyperLink)e.Row.FindControl("imbButton_edit");
                DecryptEncrypt objDecr = new DecryptEncrypt();
                compid = objDecr.Encrypt(hdn_Companyid.Text.ToString());
                compname = objDecr.Encrypt(lblCompanyName.Text.ToString());
                branchname = objDecr.Encrypt(BranchName.ToString());
                lnkview.NavigateUrl = "~/appAdministrator/RenewalformField.aspx?Company_ID=" + compid + "&Com_name=" + compname;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }
}
