using AjaxControlToolkit.HTMLEditor.ToolbarButton;

using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSICGP_VBClassLibrary;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Bcpg.OpenPgp;
//using PerformanceAndCreditRating.Class;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;


using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using ZXing;
public partial class appAdministrator_NewUnitRegistration_CFSS : System.Web.UI.Page
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    double AmountPayable;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    int BranchId;
    int Fee_Id = 0;
    string Fee_Slab = string.Empty;

    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";

    int BranchID_Check;
    int BranchFlage;
    clsCompany objCompany;

    DataTable dt;
    int ErrorId, CompanyId, UnitId;
    int fStatus;
    int AccessId;
    //int ssistatus;
    public int unitbrAction = 0;
    short tabindx = 9;

    //DataRow[] cityRows;
    string expression = string.Empty;
    DataTable Mydtcity;
    DataTable ddlCityClone = new DataTable();
    int Constitutionid = 0;

    string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
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
        int intStatus = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
        {
            intStatus = Convert.ToInt32(Request.QueryString["Status"]);
            if (intStatus == 1)
            {
                ddlApplicationType.SelectedValue = intStatus.ToString();
                trMSMEDBSEARCH.Visible = true;
                lblMsg.Text = "This Unit is either eligible for Fresh Registration because Unit is Expired for More than Two years or Unit want to Register as Fresh.";
            }
            else if (intStatus == 2)
            {
                ddlApplicationType.SelectedValue = "1".ToString();
                trMSMEDBSEARCH.Visible = true;
                lblMsg.Text = "";
            }
        }
        {
            if (intStatus == 1)
            {
            }
            else
            {
                lblMsg.Text = "";
            }
        }

        FunRequest();
        OriginalFilePath = "./Document/Upload/Registration/";
        filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";



        lblUamNameDisplay.Visible = false;
        lblUamiReigistrationNameDisplay.Visible = true;

        RegularExpressionValidatorAAdhar.Enabled = false;
        // ValidatorCalloutExtender9.Enabled = false;

        RegularExpressionValidator1.Enabled = true;
        //  ValidatorCalloutExtender12.Enabled = true;
        RegularExpressionValidator3.Enabled = false;
        RegularExpressionValidator4.Enabled = true;

        Requ_addhar.Enabled = false;
        //Validator_aadhar_number.Enabled = false;

        RequiredFieldValidator18.Enabled = true;
        RequiredFieldValidator14.Enabled = false;
        //  ValidatorCalloutExtender15.Enabled = true;

        divUAMPAN.Visible = true;
        //divUAMPANMobileButton.Visible = true;
        divUAMPANMobileButton1.Visible = true;
        lblUamNameDisplay.Visible = false;
        lblUamiReigistrationNameDisplay.Visible = true;
        trMainData.Visible = false;



        try
        {
            if (!Page.IsPostBack)
            {
                #region Fill Constitution Dropdown
                objCompany = new clsCompany();
                System.Data.DataTable Dtt = new System.Data.DataTable();
                Dtt = objCompany.Company_Fetch_Constitution();
                ddlConstitution.DataTextField = Dtt.Columns["fGPtbl_CUnit_Constitution_Name_POI"].ToString();
                ddlConstitution.DataValueField = Dtt.Columns["fGPtbl_CUnit_Constitution_Id"].ToString();
                ddlConstitution.DataSource = Dtt;
                ddlConstitution.DataBind();
                ddlConstitution.Items.Insert(0, new ListItem("--Select Constitution of the firm--", "0"));
                #endregion
            }


            if (UserId == 0)
            {
                if (!Page.IsPostBack)
                {

                    dt = new DataTable();
                    dt = clsDDL.ddlBranch_Activated(0);

                    DropDownListBranch.DataSource = dt;
                    DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
                    DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
                    DropDownListBranch.DataBind();
                    DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));


                }
            }
            else
            {
                //FETCH BRANCHID FOR LOGIN USER


                dt = new DataTable();
                dt = clsDDL.ddlBranch_Activated(0);

                DropDownListBranch.DataSource = dt;
                DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
                DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
                DropDownListBranch.DataBind();
                DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));


                objCompany = new clsCompany();
                BranchId = objCompany.MasterBranch_Fetch_BranchId(UserId);


                DropDownListBranch.Items.FindByValue(BranchId.ToString()).Selected = true;



                //DropDownListBranch.Visible = false;
                ////trbranch1.Visible = false;
                ////trbranch2.Visible = false;
                ////trbranch3.Visible = false;

                Mydtcity = clsDDL.ddlCity_Activated(0);

                ddlCityClone = Mydtcity.Clone();
                ViewState["mydtcity"] = Mydtcity;

            }
            //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            //if (Request.QueryString.HasKeys())
            //{
            //    int Provinl = Convert.ToInt32(Request.QueryString["i"]);
            //    if (Provinl == 0)
            //    {
            //        ltlProvisionalNotice.Text = "For Provisional registration please use Provisional Registration link.";
            //    }
            //    else
            //    {
            //        ltlProvisionalNotice.Text = "For PROVISIONSAL REGISTRATIONS ONLY.";
            //    }
            //}

            FunfillDropdown();
            //if (!IsPostBack)
            //{
            //    filldataform();
            //}

            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ScriptKey2", "javascript:UpdateProgressBar_DroDown();", true);



            if (!string.IsNullOrEmpty(Request.QueryString["StatusR"]))
            {
                string ChkStatus = "";
                ChkStatus = Request.QueryString["StatusR"];
                if (ChkStatus == "1")
                {
                    TableMain.Visible = false;
                    ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
                }
                else if (ChkStatus == "2")
                {
                    TableMain.Visible = false;
                    ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data is already Present for the Given Unit. Please Visit https://nsicspronline.com/ and Click on Apply Online to fill all Details till 7th Steps.');", true);
                }
                else if (ChkStatus == "3")
                {
                    TableMain.Visible = false;
                    ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Please Contact Administrator.');", true);
                }
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    private void FunfillDropdown()
    {
        if (!Page.IsPostBack)
        {
            funfill_ddlState();
            funfill_ddlCategory();
            objCompany = new clsCompany();

        }
    }
    private void funfill_ddlState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlState_a3.DataSource = dt;
        ddlState_a3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_a3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_a3.DataBind();
        ddlState_a3.Items.Insert(0, new ListItem("--Select State--", ""));

        dt.Clear();
        dt.Dispose();
    }

    private void funfill_ddlCategory()
    {
        //Category Area DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlCategoryArea();
        ddl_1d.DataSource = dt;
        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
        ddl_1d.DataBind();
        ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));
        //ddl_1d.SelectedItem.Value = "1";

        //Category Cast DROPDOWNLIST
        dt.Clear();
        dt = clsDDL.ddlCategoryCast();
        ddl_1e.DataSource = dt;
        ddl_1e.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
        ddl_1e.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
        ddl_1e.DataBind();
        ddl_1e.Items.Insert(0, new ListItem("-- Select Enterprise Social Category--", ""));


        //ListItem i = ddl_1e.Items.FindByValue("4");
        //i.Attributes.Add("style", "color:gray;");
        //i.Attributes.Add("disabled", "true");
        //i.Value = "-1";
        ddl_1e.Items.Remove(ddl_1e.Items.FindByValue("4"));







        //Category Unit Size DROPDOWNLIST
        dt.Clear();
        dt = clsDDL.ddlCategoryUnitSize();
        ddl_1f.DataSource = dt;
        ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
        ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
        ddl_1f.DataBind();
        ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

        dt.Clear();
        dt.Dispose();
    }

    public DataSet Company_PANnoCheck_Modified_Online(string PANno, string AadhaarNo)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CheckPANno_modified_Proprietorship_Online", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MyAdp.SelectCommand.Parameters.Add("@UAM", SqlDbType.Char, 50).Value = AadhaarNo;
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

    public DataSet Fetch_StateId_By_State_LgCode(int StateLgCode)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_Fetch_StateId_By_State_LgCode", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@LgStateCode", SqlDbType.Int).Value = StateLgCode;
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

    private void funfill_ddlCity_a4(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlCity_a4.DataSource = dt_c;
            ddlCity_a4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity_a4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity_a4.DataBind();
            ddlCity_a4.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();
        }
        else
        {
            ddlCity_a4.Items.Clear();
        }
    }

    public DataSet UpdateConstitutionInCunit(string PANno)
    {
        DataSet ds = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_UpdateConstitutionInCunit", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MyAdp.Fill(ds);
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
        return ds;
    }

    public DataSet FetchConstitution_By_CompanyPAN(string PAN)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchConstitution_By_CompanyPAN", DBManager.ConnectToDB());
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

    public SqlDataReader Company_Fetch_ByPANno_Step1(string PANno, string AadhaarNo)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Proprietorship", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MySqlComm.Parameters.Add("@UAM", SqlDbType.Char, 50).Value = AadhaarNo;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }

    public DataSet Fetch_Bank(string BankType)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchBank", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Banktype", SqlDbType.VarChar).Value = BankType;
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

    public DataSet Fetch_StateLgCode(string StateName)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_Fetch_StateLgCode", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@StateName", SqlDbType.VarChar).Value = StateName;
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


    void Clear()
    {
        txt_chqddNo.Text = "";
        txt_BankName.Text = "";
        txt_BankBranch.Text = "";
        txt_PayableAt.Text = "";
        txt_chqddAmnt.Text = "";
        txtChequeDDPaymentRemarks.Text = "";
        lbl_registration_fees_text.Text = "";
        lblUnitAmnt.Text = "";
        lblServiceTax.Text = "";
        lblTotAmnt.Text = "";
    }

    void disabled()
    {
        DropDownListBranch.Enabled = false;
        ddlConstitution.Enabled = false;
        txt_1.Enabled = false;
        txt_aahar.Enabled = false;
        txt_1p1.Enabled = false;
        txt_1a1.Enabled = false;
        txt_1a2.Enabled = false;
        ddlState_a3.Enabled = false;
        txt_1a5.Enabled = false;
        txt_mob.Enabled = false;
        txt_1a7.Enabled = false;
        ddl_1e.Enabled = false;
        //  ddl_1f.Enabled = false;

    }


    public class FactoryDetail
    {
        public string Pin_Code { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }

    public class BankDetail
    {
        public string BankType { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string AccountType { get; set; }
        public string AccountNumber { get; set; }
        public string AccountHolderName { get; set; }
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

        public List<FactoryDetail> Factory_Detail { get; set; }
        public List<BankDetail> Bank_Details { get; set; }
    }

    public class Root
    {
        public List<UnitDetail> Unit_Detail { get; set; }
    }

    public DataSet GetCFSSMultiplePayment(int companyId, int formType)
    {
        DataSet ds = new DataSet();

        string connectionString =
            ConfigurationManager.AppSettings["DBConnectionString"];

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(
                "CFSS_Get_ML_MultiplePayment", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyId;
                cmd.Parameters.Add("@FormType", SqlDbType.Int).Value = formType;

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(ds);
                }
            }
        }

        return ds;
    }
    protected void btnf_Click(object sender, EventArgs e)
    {

        Clear();


        txtTurnover.Text = "";

        dt = new DataTable();
        dt = clsDDL.ddlBranch_Activated(0);

        DropDownListBranch.DataSource = dt;
        DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
        DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
        DropDownListBranch.DataBind();
        DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
        trbranch1.Visible = true;
        trMainData.Visible = true;

        objCompany = new clsCompany();
        BranchId = objCompany.MasterBranch_Fetch_BranchId(UserId);

        DropDownListBranch.Items.FindByValue(BranchId.ToString()).Selected = true;

        if (ddlConstitution.SelectedValue != "4")
        {
            Requ_addhar.Enabled = false;
        }
        else
        {
            if (txt_aahar.Text == "")
            {
                Requ_addhar.Enabled = true;
                Requ_addhar.ErrorMessage = "Please Enter Valid aadher Numberr";
                Requ_addhar.Visible = true;
                Requ_addhar.Validate();
                return;
            }
        }
        lblPANmsg.Visible = false;
        //System.Threading.Thread.Sleep(5000);
        string fPANno = "";
        //string ssiNo = "";
        fPANno = txtPan.Text.ToString().ToUpper();
        int fPANlength = 0;
        fPANlength = fPANno.Length;
        if (fPANlength != 10)
        {
            txtPan.Focus();
        }
        //ssiNo = txt_21.Text.ToString();
        if (UserId == 0)
        {
            if (DropDownListBranch.SelectedValue == "")
            {
                //lblPANmsg.Visible = true;
                //lblPANmsg.Text = "Please select branch";
                //txt_1.Text = "";
                //return;
            }
            else
            {
                BranchId = Convert.ToInt32(DropDownListBranch.SelectedValue);
            }
        }

        objCompany = new clsCompany();
        DataSet ds_status = new DataSet();

        string strUAM = "";
        if (txtUAM.Text != "")
        {
            strUAM = txtUAM.Text.ToString().Trim();
        }
        else
        {
            strUAM = "NO";
        }


        txt_1.Text = fPANno;
        txt_aahar.Text = strUAM;
        int CompanyId_Check = 0;

        //ds_status = Company_PANnoCheck_Modified(fPANno, Convert.ToInt32(ddlConstitution.SelectedValue), strUAM);
        ds_status = Company_PANnoCheck_Modified_Online(fPANno, strUAM);

        if (ds_status.Tables[0].Rows.Count > 0)
        {
            fStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            BranchID_Check = Convert.ToInt32(ds_status.Tables[0].Rows[0]["BranchID"].ToString());
            CompanyId_Check = Convert.ToInt32(ds_status.Tables[0].Rows[0]["CompanyId"].ToString());
        }
        else
        {
            ds_status.Dispose();
        }
        if (BranchId == BranchID_Check)
        {
            BranchFlage = 0;
        }
        else
        {
            BranchFlage = 1;
            if (fStatus == 123 && BranchFlage == 1)
            {
                lbl_branchName_chek.Visible = false;
                lbl_branchName_chek.Text = "";
            }


        }

        #region Check ML in case of Multiple Payment
        int FormType = 1;
        DataSet dsML = GetCFSSMultiplePayment(CompanyId_Check, FormType);
        if (dsML.Tables.Count > 0 && dsML.Tables[0].Rows.Count > 0)
        {
            decimal turnover = Convert.ToDecimal(dsML.Tables[0].Rows[0]["fGPtbl_Company_TO_LastYearTurnover"]);
            decimal TotalAmount = Convert.ToDecimal(dsML.Tables[0].Rows[0]["fGPtbl_PD_UnitAmount"]);
            decimal PaidByUnit = Convert.ToDecimal(dsML.Tables[0].Rows[0]["fGPtbl_PD_PaidByUnit"]);
            decimal Balance_Surplus_Amount = Convert.ToDecimal(dsML.Tables[0].Rows[0]["fGPtbl_PD_Balance_Surplus_Amount"]);

            txtTurnover.Text = turnover.ToString();
            lblMsgMultiplePayment.Text = "You are making another payment for a fresh registration. Earlier, you had entered the last-year turnover as " + txtTurnover.Text + " lakh.";
            trMLMessage.Visible = true;
            lblMsgMultiplePaymentAmount.Text = "Based on the last-year turnover declared earlier, the total applicable fee is ₹ " + TotalAmount + ".You have already paid ₹ " + PaidByUnit + ", and ₹ " + Balance_Surplus_Amount + " is pending for payment.";


            decimal GSTAmount = Math.Round(Balance_Surplus_Amount * 0.18m, 2);
            decimal TotalAmount_Balance_Surplus = Math.Round(Balance_Surplus_Amount + GSTAmount,0);


            txt_chqddAmnt.Text = TotalAmount_Balance_Surplus.ToString();

        }
        else
        {
            txtTurnover.Text = "";
            lblMsgMultiplePayment.Text = "";
            trMLMessage.Visible = false;
            lblMsgMultiplePaymentAmount.Text = "";
            txt_chqddAmnt.Text = "";
        }
        #endregion



        Session["Status"] = fStatus.ToString();
        if (fStatus == 1)  //CONFIRM
        {
            //lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already confirmed ";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 2)  //INPROCESS
        {
            //  lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = false;

            //lblPANmsg.Text = "PAN already exists with INPROCESS status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }

            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }

            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 3)  //WAITING
        {
            // lblPANmsg.Visible = true;
            // btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with WAITING status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 4)  //EXPIRED
        {
            //  lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with EXPIRED status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 5 && AccessId == 0)  //INCOMPLETE
        {
            //   lblPANmsg.Visible = true;
            ////if (lbl_branchName_chek.Text == "Branch Not exist !!!!")
            ////{
            ////    btnSubmit.Enabled = false;
            ////}
            ////else
            ////{
            ////    btnSubmit.Enabled = true;
            ////}

            //Nsic Dhiraj 06122017

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

            //  lblPANmsg.Text = "PAN already exists with INCOMPLETE status.";

        }
        else if (fStatus == 5 && AccessId == 4)
        {
            //  lblPANmsg.Visible = true;
            // btnSubmit.Enabled = false;
            //  lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using BackLog Registration link";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch. <br>This has been filled using BackLog Registration link";
        }

        else if (fStatus == 6)  //INCOMPLETE
        {
            //  lblPANmsg.Visible = true;
            // btnSubmit.Enabled = true;
            //  lblPANmsg.Text = "Company has been blacklisted";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            //  lbl_branchName_chek.Text = "This PAN exists with " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";

            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }

        else if (fStatus == 7) //INSPECTION PASSED
        {
            //lblPANmsg.Visible = true;
            //   btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exist with INSPECTION PASSED status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";
        }
        else if (fStatus == 10) //EXISTS WITH OTHER BRANCH
        {
            //  lblPANmsg.Visible = true;
            //   btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with other branch";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";

        }

        else if (fStatus == 123)  //NEW ENTRY
        {
            try
            {



                lblPANmsg.Visible = false;
                //  btnSubmit.Enabled = true;

                //txt_1p1.Text = "";
                txt_1a1.Text = "";
                txt_1a2.Text = "";
                // txt_aahar.Text = "";
                txt_1p1.Text = "";
                txt_proposal_date.Text = "";


                //string[] formats = { "MM/dd/yyyy" };
                //var dateTime = DateTime.ParseExact("11/15/2001", formats, new CultureInfo("en-US"), DateTimeStyles.None);


                //string date = DateTime.UtcNow.ToString("MM/dd/yyyy");
                // DateTime.ParseExact(DateTime.UtcNow.ToString(), "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                //var dateee = DateTime.ParseExact(DateTime.Now.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture);
                ///// txt_proposal_date.Text = DateTime.Now.ToString("MM/dd/yyyy");// DateTime.Now.ToString("M/d/yyyy");// dateTime.ToString();
                //  txt_proposal_date.Text = DateTime.ParseExact(DateTime.Now.ToString(), "MM/dd/yyyy", CultureInfo.InvariantCulture).ToString();
                txt_proposal_date.Text = DateTime.Now.ToString("MM/dd/yyyy");
                try
                {
                    ddlState_a3.SelectedValue = "";
                    ddlCity_a4.Items.Clear();
                }
                catch { }

                txt_1a5.Text = "";
                // txt_1a6.Text = "";
                txt_1a7.Text = "";
                txt_mob.Text = "";
                try
                {
                    ddl_1e.SelectedValue = "";
                    ddl_1d.SelectedValue = "";
                }
                catch { }

                try
                {
                    ddl_1f.SelectedValue = "";
                }
                catch { }
                Session[""] = txtUAM.Text;


                if (txtUAM.Text != "" && txtPan.Text != "")
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;

                    //string APIUrl = "https://jspl.jayamsolutions.com:9011/NSIC_DEV_API/api/Unit_Registration_Udyam/Unit_Registration_Udyam";
                    string APIUrl = System.Configuration.ConfigurationManager.AppSettings["CFSS_APIUrl"];
                    string User_Name = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_UserName"];
                    string Password = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_Password"];

                    var client = new RestClient(APIUrl);
                    // BASIC AUTH
                    //client.Authenticator = new HttpBasicAuthenticator("Admin", "Jayam@123");
                    client.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Content-Type", "application/json");
                    //request.AddJsonBody(new
                    //{
                    //    Udyam = "UDYAM-WB-14-00734895555",
                    //    Mobile = "",
                    //    Pan = "AAVFD2499R555"
                    //});

                    request.AddJsonBody(new
                    {
                        Udyam = txtUAM.Text,
                        Mobile = txtUAMMobile.Text,
                        Pan = txtPan.Text
                    });

                    IRestResponse response = client.Execute(request);

                    //Console.WriteLine("Status: " + response.StatusCode);
                    //Console.WriteLine("Response: " + response.Content);




                    //dynamic data = JsonConvert.DeserializeObject(response.Content);
                    //string UnitName = data.Unit_Detail[0].UnitName;
                    //string PAN = data.Unit_Detail[0].UnitPAN;
                    //string FactoryCity = data.Unit_Detail[0].Factory_Detail[0].City;
                    //string BankName = data.Unit_Detail[0].Bank_Details[0].BankName;
                    string strName = "DHIRAJ";
                    if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
                    {
                        trbranch1.Visible = true;
                        trMainData.Visible = true;
                        lblMsg.Text = "";

                        // API returned data successfully
                        string json = response.Content;
                        Root data = JsonConvert.DeserializeObject<Root>(json);
                        var unit = data.Unit_Detail[0];

                        hdnCFFSSUnitId.Value = unit.CFSSUnitID;

                        ddl_1e.SelectedIndex = -1;
                        ddlConstitution.SelectedIndex = -1;

                        txt_1p1.Text = unit.UnitName;

                        txt_1.Text = txtPan.Text;
                        txt_aahar.Text = txtUAM.Text;
                        ddlState_a3.SelectedValue = null;

                        DataSet dsStates = new DataSet();
                        DataSet dsLgCode = new DataSet();
                        dsLgCode = Fetch_StateLgCode(unit.State.Trim());

                        string LGStateCode = "";
                        LGStateCode = Convert.ToString(dsLgCode.Tables[0].Rows[0]["fGPtbl_MS_LG_Code"].ToString());

                        dsStates = Fetch_StateId_By_State_LgCode(Convert.ToInt32(LGStateCode));
                        string strStateIdUam = Convert.ToString(dsStates.Tables[0].Rows[0]["fGPtbl_MS_Id"].ToString());
                        ddlState_a3.Items.FindByValue(strStateIdUam).Selected = true;

                        funfill_ddlCity_a4(Convert.ToInt32(strStateIdUam));



                        string SocialCaterogy = "";
                        SocialCaterogy = unit.SocialCategory;
                        if (SocialCaterogy.Trim().ToUpper() == "GENERAL".ToUpper())
                        {
                            SocialCaterogy = "1";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "OBC".ToUpper())
                        {
                            SocialCaterogy = "5";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "SC".ToUpper())
                        {
                            SocialCaterogy = "10";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "ST".ToUpper())
                        {
                            SocialCaterogy = "11";
                        }

                        ddl_1e.Items.FindByValue(SocialCaterogy).Selected = true;

                        txt_1a7.Text = unit.Email_ID;
                        txt_mob.Text = txtUAMMobile.Text;// xn["Mobile"].InnerText;
                        txt_1a5.Text = txtUAMMobile.Text;// xn["Mobile"].InnerText;
                        txt_1a2.Text = unit.Pin_Code;

                        string strUnitCategory = "";
                        string strUnitType = "";
                        strUnitCategory = unit.IndustryType;

                        if (strUnitCategory.Trim().ToUpper() == "Micro".ToUpper())
                        {
                            strUnitType = "3";
                        }
                        else if (strUnitCategory.Trim().ToUpper() == "Small".ToUpper().Trim())
                        {
                            strUnitType = "4";
                        }
                        else if (strUnitCategory.Trim().ToUpper() == "Medium".ToUpper().Trim())   //  zdv
                        {
                            trMainData.Visible = false;
                            lblMsg.Text = "This Unit comes under Medium Category. In SPRS application only Micro or Small unit can register";
                            return;
                        }

                        ddl_1f.SelectedValue = null;
                        ddl_1f.DataBind();
                        dt = clsDDL.ddlCategoryUnitSize();
                        ddl_1f.DataSource = dt;
                        ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
                        ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
                        ddl_1f.DataBind();
                        ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));
                        ddl_1f.Items.FindByValue(strUnitType).Selected = true;
                        ddl_1d.SelectedValue = null;
                        ddl_1d.DataBind();
                        dt = clsDDL.ddlCategoryArea();
                        ddl_1d.DataSource = dt;
                        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
                        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
                        ddl_1d.DataBind();
                        ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));
                        txt_1a1.Text = unit.UnitAddress;
                        string OrganisationType = "";
                        OrganisationType = unit.Constitution;

                        if (OrganisationType.Trim().ToUpper() == "Proprietary".ToUpper())
                        {
                            OrganisationType = "4";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Limited Liability Partnership".ToUpper().Trim())
                        {
                            OrganisationType = "10";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Partnership".ToUpper())
                        {
                            OrganisationType = "5";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Public Limited Company".ToUpper().Trim())
                        {
                            OrganisationType = "6";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Private Limited Company".ToUpper().Trim())
                        {
                            OrganisationType = "7";
                        }

                        else if (OrganisationType.Trim().ToUpper() == "Hindu Undivided Family".ToUpper().Trim())
                        {
                            OrganisationType = "9";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Co-Operative".ToUpper().Trim())
                        {
                            OrganisationType = "8";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Society".ToUpper().Trim())
                        {
                            OrganisationType = "8";
                        }
                        // 
                        else if (OrganisationType.Trim().ToUpper() == "Trust".ToUpper().Trim())
                        {
                            OrganisationType = "11";
                        }
                        ddlConstitution.Items.FindByValue(OrganisationType).Selected = true;




                        //string UnitName = unit.UnitName;
                        //string UnitAddress = unit.UnitAddress;
                        //string PAN = unit.UnitPAN;
                        //string SchemeId = unit.SchemeID;
                        //string Udyam = unit.Unit_Udyam;
                        //string SocialCategory = unit.SocialCategory;
                        //string Constitution = unit.Constitution;
                        //string IndustryType = unit.IndustryType;

                        //string Factory_Pin = unit.Factory_Detail[0].Pin_Code;
                        //string Factory_District = unit.Factory_Detail[0].District;
                        //string Factory_Address = unit.Factory_Detail[0].Address;
                        //string FactoryCity = unit.Factory_Detail[0].City;
                        //string FactoryState = unit.Factory_Detail[0].State;

                        //string BankType = unit.Bank_Details[0].BankType;
                        //string BankName = unit.Bank_Details[0].BankName;
                        //string BranchName = unit.Bank_Details[0].BranchName;
                        //string BranchAddress = unit.Bank_Details[0].BranchAddress;
                        //string AccountType = unit.Bank_Details[0].AccountType;
                        //string AccountNumber = unit.Bank_Details[0].AccountNumber;
                        //string AccountHolderName = unit.Bank_Details[0].AccountHolderName;

                    }
                    else
                    {


                        // ddlOperation.SelectedIndex = -1;
                        ddl_1e.SelectedIndex = -1;
                        ddlConstitution.SelectedIndex = -1;

                        // string xmltext = ConfigurationManager.AppSettings["UAMServerSettings"] + txtUAM.Text + "," + txtUAMMobile.Text + "," + ConfigurationManager.AppSettings["UAMSPRSKey"];
                        string APIUrl_CAM = System.Configuration.ConfigurationManager.AppSettings["CAM_API_Url"];
                        string User_Name_CAM = System.Configuration.ConfigurationManager.AppSettings["CAM_API_UserName"];
                        string Password_CAM = System.Configuration.ConfigurationManager.AppSettings["CAM_API_Password"];
                        string API_TOKEN_CAM = System.Configuration.ConfigurationManager.AppSettings["CAM_API_Token"];


                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;

                        var client_CAM = new RestClient(APIUrl_CAM);

                        client_CAM.Authenticator = new HttpBasicAuthenticator(User_Name_CAM, Password_CAM);
                        var request_CAM = new RestRequest("", Method.POST);
                        request_CAM.AddHeader("Content-Type", "application/json");

                        request_CAM.AddJsonBody(new
                        {
                            UdyamNo = txtUAM.Text,
                            MobileNo = txtUAMMobile.Text,
                            TokenNo = API_TOKEN_CAM
                        });
                        //var response_CAM = client_CAM.Execute(request_CAM);

                        IRestResponse response_CAM = client_CAM.Execute(request_CAM);


                        XmlDocument doc = new XmlDocument();
                        if (response_CAM.IsSuccessful)
                        {
                            string result = response_CAM.Content;

                            var apiResponse_CAM = JsonConvert.DeserializeObject<ApiResponse_CAM>(response_CAM.Content);

                            string xmlString = apiResponse_CAM.data.Replace("&lt;", "<").Replace("&gt;", ">");

                            doc.LoadXml(xmlString);

                        }
                        else
                        {
                            string error = response_CAM.ErrorMessage;
                        }


                        XmlNode basic = doc.SelectSingleNode("//BasicDetail");

                        string applicationId = GetNodeValue(basic, "ApplicationId");
                        string ownerName = GetNodeValue(basic, "OwnerName");
                        string pan = GetNodeValue(basic, "PanNo");
                        string strEnterpriseName = GetNodeValue(basic, "EnterpriseName");
                        string strMajorActivityName = GetNodeValue(basic, "MajorActivityName");
                        string strLG_ST_Code = GetNodeValue(basic, "State_Id");
                        string strSocialCategoryName = GetNodeValue(basic, "SocialCategoryName");
                        string strEmailId = GetNodeValue(basic, "EmailId");
                        string strEnterpriseTypee = GetNodeValue(basic, "EnterpriseTypee");
                        string strAddress = GetNodeValue(basic, "Address");
                        string strOrganisationTypeName = GetNodeValue(basic, "OrganisationTypeName");
                        string strPINCode = GetNodeValue(basic, "PINCode");
                        string strDISTRICT_NAME = GetNodeValue(basic, "DISTRICT_NAME");
                        string strDOInconrporation = GetNodeValue(basic, "IncorporationDate");

                        trbranch1.Visible = true;
                        trMainData.Visible = true;
                        lblMsg.Text = "";
                        txt_1p1.Text = strEnterpriseName;
                        string strOperation = "";
                        strOperation = strMajorActivityName;// xn["MajorActivity"].InnerText;
                        txt_1.Text = txtPan.Text;
                        txt_aahar.Text = txtUAM.Text;
                        ddlState_a3.SelectedValue = null;

                        txtIncorporationDate.Text = strDOInconrporation;

                        DataSet dsStates = new DataSet();
                        string LGStateCode = "";
                        LGStateCode = strLG_ST_Code;// xn["LG_ST_Code"].InnerText;

                        dsStates = Fetch_StateId_By_State_LgCode(Convert.ToInt32(LGStateCode));
                        string strStateIdUam = Convert.ToString(dsStates.Tables[0].Rows[0]["fGPtbl_MS_Id"].ToString());
                        ddlState_a3.Items.FindByValue(strStateIdUam).Selected = true;

                        funfill_ddlCity_a4(Convert.ToInt32(strStateIdUam));

                        if (!string.IsNullOrEmpty(strDISTRICT_NAME))
                        {
                            ListItem item = ddlCity_a4.Items.FindByText(strDISTRICT_NAME);

                            if (item != null)
                            {
                                ddlCity_a4.ClearSelection();
                                item.Selected = true;
                            }
                        }


                        string SocialCaterogy = "";
                        SocialCaterogy = strSocialCategoryName;// xn["SocialCategory"].InnerText;
                        if (SocialCaterogy.Trim().ToUpper() == "GENERAL".ToUpper())
                        {
                            SocialCaterogy = "1";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "OBC".ToUpper())
                        {
                            SocialCaterogy = "5";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "SC".ToUpper())
                        {
                            SocialCaterogy = "10";
                        }
                        else if (SocialCaterogy.Trim().ToUpper() == "ST".ToUpper())
                        {
                            SocialCaterogy = "11";
                        }

                        ddl_1e.Items.FindByValue(SocialCaterogy).Selected = true;

                        txt_1a7.Text = strEmailId;// xn["EmailId"].InnerText;
                        txt_mob.Text = txtUAMMobile.Text;// xn["Mobile"].InnerText;
                        txt_1a5.Text = txtUAMMobile.Text;// xn["Mobile"].InnerText;
                        txt_1a2.Text = strPINCode;// xn["PINCode"].InnerText;

                        string strUnitCategory = "";
                        string strUnitType = "";
                        strUnitCategory = strEnterpriseTypee;// xn["EnterpriseType"].InnerText;

                        if (strUnitCategory.Trim().ToUpper() == "Micro".ToUpper())
                        {
                            strUnitType = "3";
                        }
                        else if (strUnitCategory.Trim().ToUpper() == "Small".ToUpper().Trim())
                        {
                            strUnitType = "4";
                        }
                        else if (strUnitCategory.Trim().ToUpper() == "Medium".ToUpper().Trim())   //  zdv
                        {
                            trMainData.Visible = false;
                            lblMsg.Text = "This Unit comes under Medium Category. In SPRS application only Micro or Small unit can register";
                            return;
                        }

                        ddl_1f.SelectedValue = null;
                        ddl_1f.DataBind();
                        dt = clsDDL.ddlCategoryUnitSize();
                        ddl_1f.DataSource = dt;
                        ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
                        ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
                        ddl_1f.DataBind();
                        ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));
                        ddl_1f.Items.FindByValue(strUnitType).Selected = true;
                        ddl_1d.SelectedValue = null;
                        ddl_1d.DataBind();
                        dt = clsDDL.ddlCategoryArea();
                        ddl_1d.DataSource = dt;
                        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
                        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
                        ddl_1d.DataBind();
                        ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));
                        txt_1a1.Text = strAddress;// xn["CommunicationAddress"].InnerText;
                        string OrganisationType = "";
                        OrganisationType = strOrganisationTypeName;// xn["OrganisationType"].InnerText;

                        if (OrganisationType.Trim().ToUpper() == "Proprietary".ToUpper())
                        {
                            OrganisationType = "4";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Limited Liability Partnership".ToUpper().Trim())
                        {
                            OrganisationType = "10";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Partnership".ToUpper())
                        {
                            OrganisationType = "5";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Public Limited Company".ToUpper().Trim())
                        {
                            OrganisationType = "6";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Private Limited Company".ToUpper().Trim())
                        {
                            OrganisationType = "7";
                        }

                        else if (OrganisationType.Trim().ToUpper() == "Hindu Undivided Family".ToUpper().Trim())
                        {
                            OrganisationType = "9";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Co-Operative".ToUpper().Trim())
                        {
                            OrganisationType = "8";
                        }
                        else if (OrganisationType.Trim().ToUpper() == "Society".ToUpper().Trim())
                        {
                            OrganisationType = "8";
                        }
                        // 
                        else if (OrganisationType.Trim().ToUpper() == "Trust".ToUpper().Trim())
                        {
                            OrganisationType = "11";
                        }
                        ddlConstitution.Items.FindByValue(OrganisationType).Selected = true;


                    }


                }





            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message + "Dhiraj");
            }

            DataSet ds = new DataSet();
            if (txtPan.Text != "" && txtUAM.Text != "")
            {
                txtTurnover.Visible = true;
            }
        }

        if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1 || fStatus == 7 || fStatus == 6)
        {
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDrGrid;

            DataSet dsCons = new DataSet();
            dsCons = FetchConstitution_By_CompanyPAN(fPANno);
            int Constitution_id = 0;
            Constitution_id = Convert.ToInt32(dsCons.Tables[0].Rows[0]["fGPtbl_ConstitutionId"].ToString());




            funfill_ddlState();
            funfill_ddlCategory();
            System.Data.SqlClient.SqlDataReader MyDr;

            //  if (ddlConstitution.SelectedValue != "4")
            if (Constitution_id != 4)
            {
                MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);
            }
            else
            {
                MyDr = Company_Fetch_ByPANno_Step1(fPANno, strUAM);
            }
            while (MyDr.Read())
            {
                CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                // Constitutionid= Convert.ToInt32(MyDr["fGPtbl_CUnit_3C123_ConstitutionId"].ToString());
                if (string.IsNullOrEmpty(MyDr["fGPtbl_CUnit_3C123_ConstitutionId"].ToString()))
                {
                    DataSet ds = new DataSet();
                    ds = UpdateConstitutionInCunit(fPANno);
                    Constitutionid = Convert.ToInt32(ds.Tables[0].Rows[0]["ConstitutionID"].ToString());
                }
                else
                {
                    Constitutionid = Convert.ToInt32(MyDr["fGPtbl_CUnit_3C123_ConstitutionId"].ToString());
                }
                BranchId = Convert.ToInt32(MyDr["fGPtbl_CUnit_BranchId"].ToString());
                ddlConstitution.Items.Clear();
                objCompany = new clsCompany();
                System.Data.DataTable Dtt = new System.Data.DataTable();
                Dtt = objCompany.Company_Fetch_Constitution();
                ddlConstitution.DataTextField = Dtt.Columns["fGPtbl_CUnit_Constitution_Name_POI"].ToString();
                ddlConstitution.DataValueField = Dtt.Columns["fGPtbl_CUnit_Constitution_Id"].ToString();
                ddlConstitution.DataSource = Dtt;
                ddlConstitution.DataBind();
                ddlConstitution.Items.Insert(0, new ListItem("--Select Constitution of the firm--", "0"));
                ddlConstitution.Items.FindByValue(Constitutionid.ToString()).Selected = true;
                dt = new DataTable();
                dt = clsDDL.ddlBranch_Activated(0);

                DropDownListBranch.DataSource = dt;
                DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
                DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
                DropDownListBranch.DataBind();
                DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
                DropDownListBranch.Items.FindByValue(BranchId.ToString()).Selected = true;
                UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                hdnCompany.Value = CompanyId.ToString();
                // rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();

                txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();




                if (txt_1p1.Text.Contains("M/S"))
                {
                    txt_1p1.Text = txt_1p1.Text.Remove(0, 3);
                }
                txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                txt_proposal_date.Text = MyDr["fGPtbl_Company_ProposalDate"].ToString() == "" ? "" : MyDr["fGPtbl_Company_ProposalDate"].ToString();

                txtIncorporationDate.Text = MyDr["DATEOFINCORPORATION"].ToString();

                txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString() == "" ? "" : MyDr["fGtbl_Company_AdharNumber"].ToString();


                try
                {
                    ddlState_a3.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString();
                }
                catch { }
                try
                {
                    funfill_ddlCity_a4(Convert.ToInt32(MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString()));
                    ddlCity_a4.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_CityId"].ToString();
                }
                catch { }
                txt_1a5.Text = MyDr["fGPtbl_Company_HeadOffice_Phone"].ToString();
                //  txt_1a6.Text = MyDr["fGPtbl_Company_HeadOffice_Fax"].ToString();
                txt_1a7.Text = MyDr["fGPtbl_Company_HeadOffice_Email"].ToString();
                txt_mob.Text = MyDr["fGPtbl_Company_HeadOffice_Mobile"].ToString();
                try
                {
                    ddl_1d.SelectedValue = MyDr["fGPtbl_CUnit_CategoryAreaId"].ToString();
                }
                catch { }
                try
                {
                    ddl_1e.SelectedValue = MyDr["fGPtbl_CUnit_CategoryCastId"].ToString();
                }
                catch { }
                try
                {

                    ddl_1f.SelectedValue = null;
                    ddl_1f.DataBind();

                    dt = clsDDL.ddlCategoryUnitSize();
                    ddl_1f.DataSource = dt;
                    ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
                    ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
                    ddl_1f.DataBind();
                    ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

                    string strUnitTypes = MyDr["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();




                    ddl_1f.SelectedValue = MyDr["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();

                }
                catch { }

            }
            MyDr.Close();


        }
        ////// earlier false
        btnf.Enabled = true;


    }


    public string GetNodeValue(XmlNode node, string childName)
    {
        return node[childName] != null ? node[childName].InnerText : null;
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
        List<BankDetails> bankList = (List<BankDetails>)Session["BankList"];

        string ledgerID = ddlBank.SelectedValue;

        var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

        if (selectedBank != null)
        {
            Session["SelectedBank"] = selectedBank;
        }

        trbranch1.Visible = true;
        trMainData.Visible = true;
    }
    protected void btnCalculateFeess_Click(object sender, EventArgs e)
    {

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

        objCompany = new clsCompany();
        BranchId = objCompany.CFSS_MasterBranch_Fetch_BranchId(UserId);


        request.AddJsonBody(new
        {
            BranchID = BranchId
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






        pnlMain.Visible = true;
        int categoryunit = 0;
        int CatArea = 0;
        int CatCast = 0;

        Clear();

        if (ddl_1f.SelectedValue != "0")
        {
            categoryunit = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        if (ddl_1d.SelectedValue != "0")
        {
            CatArea = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        if (ddl_1e.SelectedValue != "0")
        {
            CatCast = Convert.ToInt32(ddl_1e.SelectedValue);
        }

        trbranch1.Visible = true;
        trMainData.Visible = true;


        Loadpaymentgrid(categoryunit, CatArea, CatCast);

    }

    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {

        trbranch1.Visible = true;
        trMainData.Visible = true;
        pnlMain.Visible = true;

        int categoryunit = 0;
        int CatArea = 0;
        int CategoryCastId = 0;

        if (ddl_1f.SelectedValue != "0")
        {
            categoryunit = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        if (ddl_1d.SelectedValue != "0")
        {
            CatArea = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        if (ddl_1e.SelectedValue != "0")
        {
            CategoryCastId = Convert.ToInt32(ddl_1e.SelectedValue);
        }

        SqlDataReader Mydr = Company_FetchSlabFee_Step6_CFSS(Convert.ToDecimal(txtTurnover.Text.Trim()), categoryunit, CatArea, CategoryCastId);

        while (MyDr.Read())
        {
            AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString())) + ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100);

        }

        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)

        {
            // pnlCash.Visible = true;
            pnlChqDD.Visible = false;

            Session["PaymentType"] = "1";


        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {

            Session["PaymentType"] = "2";

            ////if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            ////{

            ////    txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            ////}
            ////else
            ////{
            ////    txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            ////}
            pnlChqDD.Visible = true;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {

            Session["PaymentType"] = "3";


            //Dhiraj 06 Sep
            ////if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            ////{

            ////    txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            ////}
            ////else
            ////{
            ////    txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            ////}



            pnlChqDD.Visible = true;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {
            Session["PaymentType"] = "4";

            ////if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            ////{

            ////    txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            ////}
            ////else
            ////{
            ////    txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            ////}


            pnlChqDD.Visible = true;
        }

    }

    protected void btnfeeSlab_Click(object sender, EventArgs e)
    {
        int categoryunit = 0;
        int CatArea = 0;
        int CatCast = 0;

        if (ddl_1f.SelectedValue != "0")
        {
            categoryunit = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        if (ddl_1d.SelectedValue != "0")
        {
            CatArea = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        if (ddl_1e.SelectedValue != "0")
        {
            CatCast = Convert.ToInt32(ddl_1e.SelectedValue);
        }

        trbranch1.Visible = true;
        trMainData.Visible = true;


        Loadpaymentgrid(categoryunit, CatArea, CatCast);

    }

    protected void btnClose_Click(object sender, EventArgs e)
    {


    }


    public SqlDataReader Company_FetchSlabFee_Step6_CFSS(decimal turnover, int categoryunit, int CatArea, int CatCast)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            MySqlComm.Parameters.Add("@categoryunit", SqlDbType.Int).Value = categoryunit;
            MySqlComm.Parameters.Add("@CatArea", SqlDbType.Int).Value = CatArea;
            MySqlComm.Parameters.Add("@CatCast", SqlDbType.Int).Value = CatCast;
            MyDr = MySqlComm.ExecuteReader();
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
        return MyDr;
    }


    private void Loadpaymentgrid(int categoryunit, int CatArea, int CatCast)
    {
        // pnlOuterEdit.Visible = true;
        objCompany = new clsCompany();

        //if (object.Equals(Session["CompanyId"], null))
        //{
        //    if (txtPan.Text != "" && txtUAM.Text != "")
        //    {
        //        DataSet ds = new DataSet();
        //        ds = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
        //        fStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
        //        CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

        //        Session["CompanyId"] = CompanyId;
        //    }
        //}
        //else
        //{
        //    CompanyId = Convert.ToInt32(Session["CompanyId"]);
        //}


        //int provisionstatus = objCompany.Company_Provisional_Status(CompanyId);
        //if (provisionstatus == 0 && txtTurnover.Text == string.Empty)//
        //{
        //    lblpopmsg.Text = "Please Enter Turnover";
        //    ModalPopupExtender2.Show();
        //    return;
        //}
        //else if (provisionstatus == 0 && txtTurnover.Text != string.Empty)
        //{
        //    //THIS ELSE IF STATEMENT FOR CALCULATING AMOUNT DO NO REMOVE IT.
        //}
        //else
        //{
        //    lblpopmsg.Text = "";
        //    txtTurnover.Text = "0.0";
        //}
        SqlDataReader Mydr = Company_FetchSlabFee_Step6_CFSS(Convert.ToDecimal(txtTurnover.Text.Trim()), categoryunit, CatArea, CatCast);
        FunfillPaymentSlab(Mydr);

        //try
        //{
        //    objCompany = new clsCompany();
        //    int K = objCompany.Company_insert_turnover_limit(CompanyId, 1, Convert.ToDecimal(txtTurnover.Text.Trim()), UserId);
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        //}

    }

    private void FunfillPaymentSlab(SqlDataReader MyDr)
    {

        trbranch1.Visible = true;
        trMainData.Visible = true;


        TableRow trow;
        TableCell tcell;
        int Cnt = 0;
        //System.Data.SqlClient.SqlDataReader MyDr;
        //objCompany = new clsCompany();
        //MyDr = objCompany.Company_FetchBranchPayment_ByFormtypeId_Step6(1);
        while (MyDr.Read())
        {
            Table tbl_1 = new Table();
            tbl_1.CellPadding = 0;
            tbl_1.CellSpacing = 0;
            tbl_1.Width = Unit.Percentage(100);
            if (Cnt == 0)
            {
                trow = new TableRow();
                trow.CssClass = "dynRowHeader";

                tcell = new TableCell();
                tcell.Text = "S.<br/>No.";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Payment Slab";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Amount";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "Instruction";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                tbl_1.Rows.Add(trow);
            }
            Cnt += 1;

            trow = new TableRow();
            trow.Style.Add("padding-top", "10px");

            tcell = new TableCell();
            tcell.Text = Cnt.ToString() + ".";
            tcell.Width = Unit.Percentage(5);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = "<input id='rdReg' name='rdReg' value='" + MyDr["fGPtbl_MFee_Id"].ToString() + "' type='radio' checked='checked' />";
            tcell.Width = Unit.Percentage(2);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = "<b>Payment</b>";
            tcell.Width = Unit.Percentage(28);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);


            AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()));

            lblUnitAmnt.Text = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString())).ToString();
            lblServiceTax.Text = ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100).ToString();
            lblTotAmnt.Text = (Convert.ToDouble(lblUnitAmnt.Text) + Convert.ToDouble(lblServiceTax.Text)).ToString();

            //////txt_chqddAmnt.Text = (Convert.ToDouble(lblUnitAmnt.Text) + Convert.ToDouble(lblServiceTax.Text)).ToString();

            Fee_Id = (Convert.ToInt32(MyDr["fGPtbl_MFee_Id"].ToString()));
            Fee_Slab = (Convert.ToString(MyDr["fGPtbl_MFee_Desc"].ToString()));


            tcell = new TableCell();
            tcell.Text = "<b> Rs. " + MyDr["fGPtbl_MFee_Amount"].ToString() + "</b>";

            tcell.Width = Unit.Percentage(10);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tcell = new TableCell();
            tcell.Text = MyDr["fGPtbl_MFee_Desc"].ToString();
            tcell.Width = Unit.Percentage(55);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            tbl_1.Rows.Add(trow);

            TableRow trowMain = new TableRow();
            TableCell tcellMain = new TableCell();
            tcellMain.Controls.Add(tbl_1);
            trowMain.Controls.Add(tcellMain);
            Table2.Controls.Add(trowMain);
        }
        MyDr.Close();
    }
    protected void txt_1_TextChanged(object sender, EventArgs e)
    {

        if (ddlConstitution.SelectedValue != "4")
        {
            Requ_addhar.Enabled = false;
        }
        else
        {
            if (txt_aahar.Text == "")
            {
                Requ_addhar.Enabled = true;
                Requ_addhar.ErrorMessage = "Please Enter Valid aadher Numberr";
                Requ_addhar.Visible = true;
                Requ_addhar.Validate();
                return;
            }
        }

        lblPANmsg.Visible = false;
        //System.Threading.Thread.Sleep(5000);
        string fPANno = "";
        //string ssiNo = "";
        fPANno = txt_1.Text.ToString().ToUpper();
        int fPANlength = 0;
        fPANlength = fPANno.Length;
        if (fPANlength != 10)
        {
            txt_1.Focus();
        }
        //ssiNo = txt_21.Text.ToString();
        if (UserId == 0)
        {
            if (DropDownListBranch.SelectedValue == "")
            {
                lblPANmsg.Visible = true;
                lblPANmsg.Text = "Please select branch";
                txt_1.Text = "";
                return;
            }
            else
            {
                BranchId = Convert.ToInt32(DropDownListBranch.SelectedValue);
            }
        }

        objCompany = new clsCompany();

        //  fStatus = objCompany.Company_PANnoCheck(fPANno);
        //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);
        objCompany = new clsCompany();
        DataSet ds_status = new DataSet();
        //ds_status = objCompany.Company_PANnoCheck_Modified(fPANno);
        string strUAM = "";
        if (txt_aahar.Text != "")
        {
            strUAM = txt_aahar.Text.ToString().Trim();
        }
        else
        {
            strUAM = "NO";
        }
        ds_status = Company_PANnoCheck_Modified(fPANno, Convert.ToInt32(ddlConstitution.SelectedValue), strUAM);

        if (ds_status.Tables[0].Rows.Count > 0)
        {
            fStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            BranchID_Check = Convert.ToInt32(ds_status.Tables[0].Rows[0]["BranchID"].ToString());
        }
        else
        {
            ds_status.Dispose();
        }


        if (BranchId == BranchID_Check)
        {
            BranchFlage = 0;
        }
        else
        {
            BranchFlage = 1;
            if (fStatus == 123 && BranchFlage == 1)
            {
                lbl_branchName_chek.Visible = false;
                lbl_branchName_chek.Text = "";
            }
            else
            {

                lbl_branchName_chek.Visible = true;
                lbl_branchName_chek.Text = "Branch Not exist !!!!";
                //   btnSubmit.Enabled = false;
                // (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
            }

        }

        Session["Status"] = fStatus.ToString();
        // if (BranchId == BranchID_Check)
        //  {

        ////// if (fStatus == 1 )  //CONFIRM
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already confirmed in your login";
        ////// }
        ////// else if (fStatus == 2)  //INPROCESS
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exists with INPROCESS status in your login";
        ////// }
        ////// else if (fStatus == 3)  //WAITING
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exists with WAITING status in your login";
        ////// }
        ////// else if (fStatus == 4)  //EXPIRED
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exists with EXPIRED status in your login";
        ////// }
        ////// else if (fStatus == 5 && AccessId == 0)  //INCOMPLETE
        ////// {
        //////     lblPANmsg.Visible = true;
        ////////if (lbl_branchName_chek.Text == "This PAN exists in" + "INCOMPLETE Status" + " at " + "Branch Name");
        //////if (lbl_branchName_chek.Text == "Branch Not exist !!!!")
        //////{
        //////         btnSubmit.Enabled = false;
        //////     }
        //////     else
        //////     {
        //////         btnSubmit.Enabled = true;
        //////     }
        //////     lblPANmsg.Text = "PAN already exists with INCOMPLETE status in your login.";
        ////// }
        ////// else if (fStatus == 5 && AccessId == 4)
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using BackLog Registration link";
        ////// }

        ////// else if (fStatus == 6)  //INCOMPLETE
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "Company has been blacklisted";
        ////// }

        ////// else if (fStatus == 7) //INSPECTION PASSED
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exist with INSPECTION PASSED status in your login";
        ////// }
        ////// else if (fStatus == 10) //EXISTS WITH OTHER BRANCH
        ////// {
        //////     lblPANmsg.Visible = true;
        //////     btnSubmit.Enabled = false;
        //////     lblPANmsg.Text = "PAN already exists with other branch";
        ////// }


        if (fStatus == 1)  //CONFIRM
        {
            //lblPANmsg.Visible = true;
            // btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already confirmed ";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 2)  //INPROCESS
        {
            //  lblPANmsg.Visible = true;
            //   btnSubmit.Enabled = false;

            //lblPANmsg.Text = "PAN already exists with INPROCESS status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 3)  //WAITING
        {
            // lblPANmsg.Visible = true;
            // btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with WAITING status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 4)  //EXPIRED
        {
            //  lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with EXPIRED status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

        }
        else if (fStatus == 5 && AccessId == 0)  //INCOMPLETE
        {
            //   lblPANmsg.Visible = true;
            if (lbl_branchName_chek.Text == "Branch Not exist !!!!")
            {
                //  btnSubmit.Enabled = false;
            }
            else
            {
                //  btnSubmit.Enabled = true;
            }

            //Nsic Dhiraj 06122017

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch ";

            //  lblPANmsg.Text = "PAN already exists with INCOMPLETE status.";

        }
        else if (fStatus == 5 && AccessId == 4)
        {
            //  lblPANmsg.Visible = true;
            //btnSubmit.Enabled = false;
            //  lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using BackLog Registration link";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch. <br>This has been filled using BackLog Registration link";
        }

        else if (fStatus == 6)  //INCOMPLETE
        {

            if (lbl_branchName_chek.Text == "Branch Not exist !!!!")
            {
                // btnSubmit.Enabled = false;
            }
            else
            {
                //  btnSubmit.Enabled = true;
            }


            //  lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = true;
            //  lblPANmsg.Text = "Company has been blacklisted";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }
            else if (intStatus == 6)
            {
                strStatusMsg = "Rejected";
            }
            //  lbl_branchName_chek.Text = "Company has been blacklisted.<br> This PAN exists at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";

            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";

        }

        else if (fStatus == 7) //INSPECTION PASSED
        {
            //lblPANmsg.Visible = true;
            //btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exist with INSPECTION PASSED status";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }

            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";
        }
        else if (fStatus == 10) //EXISTS WITH OTHER BRANCH
        {
            //  lblPANmsg.Visible = true;
            //  btnSubmit.Enabled = false;
            // lblPANmsg.Text = "PAN already exists with other branch";

            int intStatus = 0;
            intStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            string strStatusMsg = "";
            if (intStatus == 1)
            {
                strStatusMsg = "Confirm";
            }
            else if (intStatus == 2)
            {
                strStatusMsg = "Inprocess";
            }
            else if (intStatus == 3)
            {
                strStatusMsg = "Waiting";
            }
            else if (intStatus == 4)
            {
                strStatusMsg = "Expired";
            }
            else if (intStatus == 5)
            {
                strStatusMsg = "Incomplete";
            }

            lbl_branchName_chek.Text = "This PAN exists with  " + strStatusMsg + " Status at " + ds_status.Tables[0].Rows[0]["BranchName"].ToString() + "  Branch.";

        }

        else if (fStatus == 123)  //NEW ENTRY
        {
            lblPANmsg.Visible = false;
            // btnSubmit.Enabled = true;

            //txt_1p1.Text = "";
            txt_1a1.Text = "";
            txt_1a2.Text = "";
            // txt_aahar.Text = "";
            txt_1p1.Text = "";
            txt_proposal_date.Text = "";

            try
            {
                ddlState_a3.SelectedValue = "";
                ddlCity_a4.Items.Clear();
            }
            catch { }

            txt_1a5.Text = "";
            // txt_1a6.Text = "";
            txt_1a7.Text = "";
            txt_mob.Text = "";
            try
            {
                ddl_1e.SelectedValue = "";
                ddl_1d.SelectedValue = "";
            }
            catch { }

            try
            {
                ddl_1f.SelectedValue = "";
            }
            catch { }



        }

        if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1 || fStatus == 7 || fStatus == 6)
        {
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDrGrid;



            ////DropDownListBranch.Enabled = false;
            ////trbranch1.Visible = false;
            ////trbranch2.Visible = false;
            ////trbranch3.Visible = false;
            System.Data.SqlClient.SqlDataReader MyDr;

            if (ddlConstitution.SelectedValue != "4")
            {
                MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);
            }
            else
            {
                MyDr = Company_Fetch_ByPANno_Step1(fPANno, strUAM);
            }



            while (MyDr.Read())
            {
                CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                hdnCompany.Value = CompanyId.ToString();
                // rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();
                txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                txt_proposal_date.Text = MyDr["fGPtbl_Company_ProposalDate"].ToString() == "" ? "" : MyDr["fGPtbl_Company_ProposalDate"].ToString();
                txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString() == "" ? "" : MyDr["fGtbl_Company_AdharNumber"].ToString();



                // dhiraj 19072017
                // rdbSCSTInitiative.SelectedValue = MyDr["fGtbl_Company_IsSCST_Initiative"].ToString() == "1" ? "1" : "2";
                /*
                if (MyDr["fGtbl_Company_IsSCST_Initiative"].ToString() == "1")
                {
                    rdbSCSTInitiative.SelectedValue = "1";
                }
                else
                {
                    rdbSCSTInitiative.SelectedValue = "2";
                }
                */


                try
                {
                    ddlState_a3.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString();
                }
                catch { }
                try
                {
                    funfill_ddlCity_a4(Convert.ToInt32(MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString()));
                    ddlCity_a4.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_CityId"].ToString();
                }
                catch { }
                txt_1a5.Text = MyDr["fGPtbl_Company_HeadOffice_Phone"].ToString();
                // txt_1a6.Text = MyDr["fGPtbl_Company_HeadOffice_Fax"].ToString();
                txt_1a7.Text = MyDr["fGPtbl_Company_HeadOffice_Email"].ToString();
                txt_mob.Text = MyDr["fGPtbl_Company_HeadOffice_Mobile"].ToString();

                try
                {
                    ddl_1d.SelectedValue = MyDr["fGPtbl_CUnit_CategoryAreaId"].ToString();
                }
                catch { }
                try
                {
                    ddl_1e.SelectedValue = MyDr["fGPtbl_CUnit_CategoryCastId"].ToString();
                }
                catch { }
                try
                {
                    ddl_1f.SelectedValue = MyDr["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
                }
                catch { }






                if (BranchId != (Convert.ToInt32(MyDr["fGPtbl_CUnit_BranchId"].ToString())))
                {
                    //  btnSubmit.Visible = true;
                }

            }
            MyDr.Close();
        }
        //  }
        //else
        //  {
        // lblPANmsg.Text = "Pan Number exist in Diffrent Branch"; 
        //  btnSubmit.Enabled = false; 
        // }

    }

    protected void txt_aahar_TextChanged(object sender, EventArgs e)
    {
        lbl_aadharnumber.Visible = false;
        string Aadharnumber = "";
        string checkstatus = "";
        Aadharnumber = txt_aahar.Text.ToString();
        try
        {
            if (Aadharnumber.Length != 12)
            {
                txt_aahar.Focus();
            }
            objCompany = new clsCompany();
            DataSet ds_status = new DataSet();
            ds_status = objCompany.Company_adharCheck_Modified(Aadharnumber.ToString());
            if (ds_status != null && ds_status.Tables[0].Rows.Count > 0)
            {
                checkstatus = Convert.ToString(ds_status.Tables[0].Rows[0]["Status"].ToString());
            }
            else
            {
                ds_status.Dispose();
            }
            if (checkstatus != "123")
            {
                /*  Commented on 23Jan 2020
                lbl_aadharnumber.Visible = true;
                lbl_aadharnumber.Text = "Please enter difrent Aadhar number.";
                // btnSubmit.Enabled = false;
                txt_aahar.Text = "";
                txt_aahar.Focus();


    */
            }




        }
        catch (Exception ex)
        {

        }



        txt_1_TextChanged(sender, e);
    }

    protected void ddlConstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txt_1.Text = "";
        //txt_aahar.Text = "";
        if (ddlConstitution.SelectedValue != "0")
        {
            txt_1.Enabled = true;
        }
        else
        {
            txt_1.Enabled = false;
            txt_1.Text = "";
            txt_aahar.Text = "";
        }

        if (ddlConstitution.SelectedValue != "4")
        {
            Requ_addhar.Enabled = false;
        }
        else
        {
            Requ_addhar.Enabled = true;
        }
    }

    protected void ddlState_a3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlState_a3.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlCity_a4(fStateId);
        ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "UpdateProgressBar_DroDown();", true);
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
    }

    private bool FunUploadFile_1()
    {
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


    public CompanyInsertResult Company_Insert_Step1_CFSS(string Companyname, string PAN, string Address, string PIN,
       int StateId, int CityId, string Phone, string Email, string Mobile, int SpclCategory, int Castid, int UnitSizeId,
       int Branchid, string ProposalDate, string Aadhaar, int PaymentType, int ConstId, string CHQDD_UTR,
       string BankName, string BranchName, string PayableAt, decimal Amount, decimal Servicetax, int Feeid, string FeeDesc, string FileName,
       string FilePath, string PaymentDate, string Remarks, decimal ftLastYearTurnOver,

       string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername,
       string CFSSUnitID, string IncorporationDate, decimal PaidAmount

       )
    {
        int tblId = 0;

        CompanyInsertResult result = new CompanyInsertResult();


        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step1_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar).Value = Companyname;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar).Value = PAN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar).Value = Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar).Value = PIN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar).Value = Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar).Value = Email;

            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar).Value = Mobile;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = SpclCategory;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = Castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = UnitSizeId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = Branchid;
            MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar).Value = ProposalDate;

            MySqlComm.Parameters.Add("@IncorporationDate", SqlDbType.VarChar).Value = IncorporationDate;


            MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar).Value = Aadhaar;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = PaymentType;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C123_ConstitutionId", SqlDbType.Int).Value = ConstId;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_UTR", SqlDbType.VarChar).Value = CHQDD_UTR;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar).Value = BranchName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar).Value = PayableAt;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Decimal).Value = Amount;
            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitServiceTax", SqlDbType.Decimal).Value = Servicetax;
            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitFeeId", SqlDbType.Int).Value = Feeid;

            MySqlComm.Parameters.Add("@fGPtbl_PD_UnitSlabInstruction", SqlDbType.VarChar).Value = FeeDesc;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar).Value = FileName;
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar).Value = FilePath;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.VarChar).Value = PaymentDate;
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar).Value = Remarks;

            MySqlComm.Parameters.Add("@ftLastYearTurnOver", SqlDbType.Decimal).Value = ftLastYearTurnOver;

            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;

            MySqlComm.Parameters.Add("@CFSSUnitID", SqlDbType.VarChar).Value = CFSSUnitID;

            MySqlComm.Parameters.Add("@PaidAmountbyunit", SqlDbType.Decimal).Value = PaidAmount;


            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);


            SqlParameter outCompanyId = new SqlParameter("@outCompanyId", SqlDbType.Int);
            outCompanyId.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompanyId);



            MySqlComm.ExecuteNonQuery();

            result.ErrorId = outError.Value != DBNull.Value ? Convert.ToInt32(outError.Value) : 0;
            result.CompanyId = outCompanyId.Value != DBNull.Value ? Convert.ToInt32(outCompanyId.Value) : 0;


            //tblId = Convert.ToInt32(outError.Value);
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


        return result;

        //return tblId;
    }


    public class CompanyInsertResult
    {
        public int ErrorId { get; set; }
        public int CompanyId { get; set; }
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

    //public void SaveUnitDetailsResponse(string responseString, int CompanyId)
    //{
    //    var result = JsonConvert.DeserializeObject<RootResponse>(responseString);
    //    int status = 0;
    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        con.Open();

    //        foreach (var item in result.Data)
    //        {
    //            using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIUnitResponse", con))
    //            {
    //                cmd.CommandType = CommandType.StoredProcedure;

    //                cmd.Parameters.AddWithValue("@UniqueID", item.UniqueID);
    //                cmd.Parameters.AddWithValue("@Message", item.Message);
    //                cmd.Parameters.AddWithValue("@IsSuccess", result.Success);
    //                cmd.Parameters.AddWithValue("@MainMessage", result.Message);
    //                cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
    //                //SqlParameter outStatus = new SqlParameter("@ErrorId", SqlDbType.Int)
    //                //{
    //                //    Direction = ParameterDirection.Output
    //                //};
    //                //cmd.Parameters.Add(outStatus);
    //                //status = Convert.ToInt32(cmd.Parameters["@ErrorId"].Value);

    //                cmd.ExecuteNonQuery();
    //            }
    //        }
    //    }

    //    //return status;
    //}

    public class PaymentResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string Id { get; set; }
    }

    public void SavePaymentResponse(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<PaymentResponse>(responseString);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
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
    }

    /*
    public void SavePaymentResponse(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<PaymentResponse>(responseString);

        int status = 0;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            foreach (var item in result.Data)
            {
                using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIPaymentResponse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
                    cmd.Parameters.AddWithValue("@IsSuccess", result.success);
                    cmd.Parameters.AddWithValue("@Message", result.message);
                    cmd.Parameters.AddWithValue("@fGPtbl_PD_CFSS_PaymentId", result.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //return status;
    }
    */


    public string SaveErrorLog(int? companyId, string responseString, string eventName)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CFSS_SP_Insert_CFSSAPI_Error", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = (object)companyId ?? DBNull.Value;
                cmd.Parameters.Add("@ResponseString", SqlDbType.NVarChar).Value = (object)responseString ?? DBNull.Value;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar).Value = (object)eventName ?? DBNull.Value;
                SqlParameter outId = new SqlParameter("@OutId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outId);

                SqlParameter outMsg = new SqlParameter("@OutMsg", SqlDbType.NVarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outMsg);

                con.Open();
                cmd.ExecuteNonQuery();
                int insertedId = Convert.ToInt32(cmd.Parameters["@OutId"].Value);
                string message = cmd.Parameters["@OutMsg"].Value.ToString();
                return message;


            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        bool selected = false;

        string Bank_BranchID = "";
        string Bank_AccountNo = "";
        string Bank_BankName = "";
        string Bank_IFSC = "";
        string Bank_Branchname = "";
        string Bank_LedgerId = "";
        string Bank_Ledgername = "";


        BankDetails bank = (BankDetails)Session["SelectedBank"];
        Bank_BranchID = bank.BranchID;
        Bank_AccountNo = bank.BankAccountNo;
        Bank_BankName = bank.Bank_Name;
        Bank_IFSC = bank.Bank_IFSC_Code;
        Bank_Branchname = bank.BranchName;
        Bank_LedgerId = bank.LedgerID;
        Bank_Ledgername = bank.Ledger_Name;




        clsCompany objCompany = new clsCompany();

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

        int intBranchId = 0;
        int intConst = 0;
        int intStateid = 0;
        int intDistrictId = 0;
        int intCastId = 0;
        int intSpecCastid = 0, intunitsize = 0;
        string strPAN = "", strUdyam = "", strAddress = "", strPIN = "", strPhone = "", strMobile = "", strEmail = "", strCompName = "";
        string dtProposalDate = null;
        Decimal ftLastYearTurnOver = 0, UnitAmount = 0, UnitServicetax = 0, PaidAmount = 0;
        string dtIncorporationDate = null;


        if (lblUnitAmnt.Text.Trim().ToString() != "")
        {
            PaidAmount = Convert.ToDecimal(txt_chqddAmnt.Text.Trim().ToString());
        }

        if (lblUnitAmnt.Text.Trim().ToString() != "")
        {
            UnitAmount = Convert.ToDecimal(lblUnitAmnt.Text.Trim().ToString());
        }
        if (lblServiceTax.Text.Trim().ToString() != "")
        {
            UnitServicetax = Convert.ToDecimal(lblServiceTax.Text.Trim().ToString());
        }

        if (txt_aahar.Text.Trim().ToString() != "")
        {
            strUdyam = txt_aahar.Text.Trim().ToString();
        }
        try
        {
            if (ddlState_a3.SelectedIndex > 0)
            {
                intStateid = Convert.ToInt32(ddlState_a3.SelectedValue);
            }

        }
        catch { }
        try
        {
            if (ddlCity_a4.SelectedIndex > 0)
            {
                intDistrictId = Convert.ToInt32(ddlCity_a4.SelectedValue);
            }

        }
        catch { }
        try
        {
            if (DropDownListBranch.SelectedIndex > 0)
            {
                intBranchId = Convert.ToInt32(DropDownListBranch.SelectedValue);
            }

        }
        catch { }
        try
        {
            if (ddlConstitution.SelectedIndex > 0)
            {
                intConst = Convert.ToInt32(ddlConstitution.SelectedValue);
            }

        }
        catch { }
        if (txt_1.Text.Trim().ToString() != "")
        {
            strPAN = txt_1.Text.Trim().ToString();
        }
        if (txt_1p1.Text.Trim().ToString() != "")
        {
            strCompName = txt_1p1.Text.Trim().ToString();
        }
        if (txt_proposal_date.Text.Trim().ToString() != "")
        {
            dtProposalDate = txt_proposal_date.Text.Trim().ToString();
        }
        if (txt_1a1.Text.Trim().ToString() != "")
        {
            strAddress = txt_1a1.Text.Trim().ToString();
        }
        if (txt_1a2.Text.Trim().ToString() != "")
        {
            strPIN = txt_1a2.Text.Trim().ToString();
        }
        if (txt_1a5.Text.Trim().ToString() != "")
        {
            strPhone = txt_1a5.Text.Trim().ToString();
        }
        if (txt_mob.Text.Trim().ToString() != "")
        {
            strMobile = txt_mob.Text.Trim().ToString();
        }
        if (txt_1a7.Text.Trim().ToString() != "")
        {
            strEmail = txt_1a7.Text.Trim().ToString();
        }

        try
        {
            if (ddl_1e.SelectedIndex > 0)
            {
                intCastId = Convert.ToInt32(ddl_1e.SelectedValue);
            }
        }
        catch { }
        try
        {
            if (ddl_1d.SelectedIndex > 0)
            {
                intSpecCastid = Convert.ToInt32(ddl_1d.SelectedValue);
            }
        }
        catch { }
        try
        {
            if (ddl_1f.SelectedIndex > 0)
            {
                intunitsize = Convert.ToInt32(ddl_1f.SelectedValue);
            }
        }
        catch { }
        if (txtTurnover.Text.Trim().ToString() != "")
        {
            ftLastYearTurnOver = Convert.ToDecimal(txtTurnover.Text.Trim().ToString());
        }

        if (txtIncorporationDate.Text.Trim().ToString() != "")
        {
            dtIncorporationDate = txtIncorporationDate.Text.Trim().ToString();
        }


        int tblId = 0;

        SqlDataReader Mydr = Company_FetchSlabFee_Step6_CFSS(Convert.ToDecimal(txtTurnover.Text.Trim()), intunitsize, intSpecCastid, intCastId);




        while (MyDr.Read())
        {
            Fee_Id = (Convert.ToInt32(MyDr["fGPtbl_MFee_Id"].ToString()));
            Fee_Slab = (Convert.ToString(MyDr["fGPtbl_MFee_Desc"].ToString()));
        }




        //tblId = Company_Insert_Step1_CFSS(strCompName, strPAN, strAddress, strPIN, intStateid, intDistrictId,
        //  strPhone, strEmail, strMobile, intSpecCastid, intCastId, intunitsize, intBranchId, dtProposalDate, strUdyam,
        //  PaymentType, intConst, ChqDdNo, BankName, BranchName, PaybalAt, UnitAmount, UnitServicetax, Fee_Id,
        //  Fee_Slab, newFileName, OriginalFilePath, ChqDdate, PaymentRemarks, ftLastYearTurnOver,
        //  Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername
        //  );


        #region  If Unit Is not Registered with CFSS then call API to push data to CFSS Portal
        int CFSSUNITID = 0;
        int CompanyId = 0;
        DataSet dsComp = new DataSet();
        CFSSService obj = new CFSSService();
        CFSSUNITID = obj.CallApi_CFSSUNITID_Company(txt_aahar.Text, "", txt_1.Text);
        dsComp = obj.FetchCompanyId_CFSS(txt_aahar.Text, txt_1.Text);

        CompanyId = Convert.ToInt32(dsComp.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

        if (CFSSUNITID != 0)
        {
            bool isUpdated = obj.UpdateCFSSUnitID_IfNull(CompanyId, CFSSUNITID);
        }

        else
        {
            string json = CFSSService.GetUnitDetailJson_R_RA(CompanyId);

            string errorMessage;
            bool isSuccess = obj.PostUnitDetailsToCFSS(CompanyId, json, out errorMessage);

            if (!isSuccess)
            {
                lblMsg.Text = errorMessage;
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                dsComp = obj.FetchCompanyUAM_PAN_CFSS(CompanyId);
                CFSSUNITID = Convert.ToInt32(dsComp.Tables[0].Rows[0]["CFSSUnitID"].ToString());
            }
        }






        #endregion



        CompanyInsertResult res = Company_Insert_Step1_CFSS(strCompName, strPAN, strAddress, strPIN, intStateid, intDistrictId,
          strPhone, strEmail, strMobile, intSpecCastid, intCastId, intunitsize, intBranchId, dtProposalDate, strUdyam,
         PaymentType, intConst, ChqDdNo, BankName, BranchName, PaybalAt, UnitAmount, UnitServicetax, Fee_Id,
          Fee_Slab, newFileName, OriginalFilePath, ChqDdate, PaymentRemarks, ftLastYearTurnOver,
          Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername, CFSSUNITID.ToString(),
          dtIncorporationDate, PaidAmount
          );

        tblId = res.ErrorId;
        int company_Id = res.CompanyId;

      

        if (tblId == 2) //INSERT/UPDATE FAIL
        {
            Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=2");
        }

        try
        {
            objCompany = new clsCompany();
            int K = objCompany.Company_insert_turnover_limit(company_Id, 1, Convert.ToDecimal(txtTurnover.Text.Trim()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
            return;
        }


        ////#region  If Unit Is not Registered with CFSS then call API to push data to CFSS Portal

        ////string User_Name = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_UserName"];
        ////string Password = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_Password"];
        ////if (string.IsNullOrEmpty(hdnCFFSSUnitId.Value))
        ////{
        ////    DataTable dtUnitDetails = new DataTable();
        ////    string json = GetUnitDetailJson(company_Id);
        ////    if (!string.IsNullOrWhiteSpace(json))
        ////    {                

        ////        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;
        ////        string url = System.Configuration.ConfigurationManager.AppSettings["CFSS_UNITPostingAPIUrl"];

        ////        var client = new RestClient(url);
        ////        client.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
        ////        var request = new RestRequest("", Method.POST);
        ////        request.AddHeader("Content-Type", "application/json");
        ////        request.AddParameter("application/json", json, ParameterType.RequestBody);
        ////        IRestResponse response = client.Execute(request);


        ////        if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
        ////        {
        ////            SaveUnitDetailsResponse(response.Content, company_Id);                   
        ////        }
        ////        else
        ////        {
        ////            string msg = SaveErrorLog(company_Id, response.Content, "Unit Details Response failed from CFSS Server. Please try again or contact support.");
        ////            lblMsg.Text = "Unit Details Response failed from CFSS Server. Please try again or contact support.";
        ////            lblMsg.ForeColor = System.Drawing.Color.Red;
        ////            return;
        ////        }
        ////    }
        ////}

        ////#endregion



        #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB


        int FormType = 1;
        string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        CFSSService service = new CFSSService();
        string result = service.ProcessPayment(company_Id, FormType, apiUrl, "Fresh");
        if (result == "SUCCESS")
        {
            if (tblId == 1)
            {
                Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=1");
            }
            else if (tblId == 2)
            {
                Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=2");
            }
            else
            {
                Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=3");
            }
        }
        else
        {
            lblMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
            lblMsg.ForeColor = System.Drawing.Color.Red;
        }


        //////int FormType = 1;
        //////DataTable dt = new DataTable();
        //////try
        //////{
        //////    using (SqlConnection con = new SqlConnection(connectionString))
        //////    {
        //////        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch", con))
        //////        {
        //////            cmd.CommandType = CommandType.StoredProcedure;
        //////            cmd.Parameters.AddWithValue("@CompanyId", company_Id);
        //////            cmd.Parameters.AddWithValue("@FormType", FormType);
        //////            cmd.CommandTimeout = 6000000;
        //////            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        //////            {
        //////                con.Open();
        //////                da.Fill(dt);
        //////            }
        //////        }
        //////    }
        //////}
        //////catch (Exception ex)
        //////{

        //////    throw new Exception("Error fetching data from DB: " + ex.Message);
        //////}

        //////string jsonResponse = BuildBranchApiJson(dt);

        //////string CFSS_UNITPostingAPIUrl = System.Configuration.ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        //////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;
        //////var clients = new RestClient(CFSS_UNITPostingAPIUrl);
        //////clients.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
        //////var requests = new RestRequest("", Method.POST);
        //////requests.AddHeader("Content-Type", "application/json");
        //////requests.AddParameter("application/json", jsonResponse, ParameterType.RequestBody);
        //////IRestResponse responses = clients.Execute(requests);

        ////////var clients = new RestClient(CFSS_UNITPostingAPIUrl);
        ////////clients.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
        ////////var requests = new RestRequest(Method.POST);
        ////////requests.AddHeader("Content-Type", "application/json");
        ////////// string jsonBody = BuildBranchApiJson(dt);       
        ////////requests.AddParameter("application/json", jsonResponse, ParameterType.RequestBody);
        ////////IRestResponse responses = clients.Execute(requests);
        ////////string responseString = responses.Content;

        //////if (responses.IsSuccessful && !string.IsNullOrEmpty(responses.Content))
        //////{
        //////    SavePaymentResponse(responses.Content, company_Id);
        //////}
        //////else
        //////{
        //////    string msg = SaveErrorLog(company_Id, responses.Content, "Payment Response failed from CFSS Server. Please try again or contact support.");
        //////    lblMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
        //////    lblMsg.ForeColor = System.Drawing.Color.Red;
        //////    return;
        //////}

        #endregion



        //if (tblId == 1)   //INSERT/UPDATE SUCCESSFULL
        //{
        //    Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=1");
        //}
        //else if (tblId == 2) //INSERT/UPDATE FAIL
        //{
        //    Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=2");
        //}
        //else
        //{
        //    Response.Redirect("NewUnitRegistration_CFSS.aspx?StatusR=3");
        //}
    }

    public DataSet Company_PANnoCheck_Modified(string PANno, int ConstitutionId, string AadhaarNo)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CheckPANno_modified_Proprietorship", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MyAdp.SelectCommand.Parameters.Add("@ConstitutionId", SqlDbType.Char, 10).Value = ConstitutionId;
            MyAdp.SelectCommand.Parameters.Add("@UAM", SqlDbType.Char, 50).Value = AadhaarNo;
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


    protected void ddlApplicationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlApplicationType.SelectedValue == "1")
        {
            trMSMEDBSEARCH.Visible = true;
        }
        else if (ddlApplicationType.SelectedValue == "2")
        {
            Response.Redirect("Online_CertificateList_Renewal_CFSS.aspx");
        }
        else if (ddlApplicationType.SelectedValue == "3")
        {
            Response.Redirect("Online_CertificateList_Amnd_CFSS.aspx");
        }
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
    public static string BuildBranchApiJson(DataTable dt)
    {
        //var finalResult = new List<object>();
        object finalResult = null;
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {

                string branchState = GetString(row, "fGPtbl_MS_Id");
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
                    decimal grantAmount = fees - 100;

                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        // collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = grantAmount,
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
                    BranchID = row["fGPtbl_CUnit_BranchId"].ToString(),
                    CFSSUnitID = row["CFSSUnitID"].ToString(),
                    SchemeID = row["SchemeID"].ToString(),
                    CollectionBankID = row["Bank_LedgerId"].ToString(),
                    TransactionDate = row["TransactionDate"].ToString(),
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


    public static string BuildBranchApiJsons(DataTable dt)
    {
        var finalResult = new List<object>();

        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                string branchState = GetString(row, "fGPtbl_MS_Id");
                string unitState = GetString(row, "fGPtbl_Company_HeadOffice_StateId");

                decimal fees = GetDecimal(row, "collectionAmount");
                decimal cgst = GetDecimal(row, "CGST");
                decimal sgst = GetDecimal(row, "SGST");
                decimal igst = GetDecimal(row, "IGST");

                int category = GetInt(row, "fGPtbl_CUnit_CategoryCastId");

                var transactions = new List<object>();

                // Fees
                transactions.Add(new
                {
                    collectionLedgerID = row["collectionLedgerID_FE"].ToString(),
                    collectionType = row["collectionType_FE"].ToString(),
                    collectionAmount = fees,
                    Narration = row["Narration"].ToString()
                });

                // Tax Logic
                if (branchState == unitState)
                {
                    if (cgst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_CGST"].ToString(),
                            collectionType = row["collectionType_CGST"].ToString(),
                            collectionAmount = cgst,
                            Narration = row["Narration_CGST"].ToString()
                        });
                    }

                    if (sgst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_SGST"].ToString(),
                            collectionType = row["collectionType_SGST"].ToString(),
                            collectionAmount = sgst,
                            Narration = row["Narration_SGST"].ToString()
                        });
                    }
                }
                else
                {
                    if (igst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_IGST"].ToString(),
                            collectionType = row["collectionType_IGST"].ToString(),
                            collectionAmount = igst,
                            Narration = row["Narration_IGST"].ToString()
                        });
                    }
                }


                if (category == 10 || category == 11)
                {
                    decimal grantAmount = fees - 100;

                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = grantAmount,
                        Narration = row["Narration_Grant"].ToString()
                    });
                }

                var data = new
                {
                    BranchID = GetString(row, "fGPtbl_CUnit_BranchId"),
                    CFSSUnitID = GetString(row, "CFSSUnitID"),
                    SchemeID = GetString(row, "SchemeID"),
                    CollectionBankID = GetString(row, "Bank_LedgerId"),
                    TransactionDate = GetString(row, "TransactionDate"),
                    TransactionDetails = transactions
                };

                finalResult.Add(data);
            }
        }
        else
        {
            finalResult.Add(new { message = "Record not found" });
        }

        return JsonConvert.SerializeObject(finalResult, Newtonsoft.Json.Formatting.Indented);
    }

    public static string GetUnitDetailJson(int companyId)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        string jsonResult = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CFSS_PULL_UNITDATA_FOR_CFSS_POSTING", con))
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


    public static string Build_UnitDetails_POSTING_ApiJson(DataTable dtUnitDetails)
    {
        var finalResult = new List<object>();

        if (dtUnitDetails != null && dtUnitDetails.Rows.Count > 0)
        {
            foreach (DataRow row in dtUnitDetails.Rows)
            {

                string branchState = GetString(row, "fGPtbl_MS_Id");
                string unitState = GetString(row, "fGPtbl_Company_HeadOffice_StateId");

                decimal fees = GetDecimal(row, "collectionAmount");
                decimal cgst = GetDecimal(row, "CGST");
                decimal sgst = GetDecimal(row, "SGST");
                decimal igst = GetDecimal(row, "IGST");
                decimal grantamount = GetDecimal(row, "Grant_Collection_Amount_UG");

                int category = GetInt(row, "fGPtbl_CUnit_CategoryCastId");


                var transactions = new List<object>();

                transactions.Add(new
                {
                    collectionLedgerID = row["collectionLedgerID_FE"].ToString(),
                    collectionType = row["collectionType_FE"].ToString(),
                    collectionAmount = fees,
                    Narration = row["Narration"].ToString()
                });


                if (branchState == unitState)
                {

                    if (cgst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_CGST"].ToString(),
                            collectionType = row["collectionType_CGST"].ToString(),
                            collectionAmount = cgst,
                            Narration = row["Narration_CGST"].ToString()
                        });
                    }


                    if (sgst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_SGST"].ToString(),
                            collectionType = row["collectionType_SGST"].ToString(),
                            collectionAmount = sgst,
                            Narration = row["Narration_SGST"].ToString()
                        });
                    }
                }
                else
                {

                    if (igst > 0)
                    {
                        transactions.Add(new
                        {
                            collectionLedgerID = row["collectionLedgerID_IGST"].ToString(),
                            collectionType = row["collectionType_IGST"].ToString(),
                            collectionAmount = igst,
                            Narration = row["Narration_IGST"].ToString()
                        });
                    }
                }


                //if (grantamount > 0)
                //{
                //    transactions.Add(new
                //    {
                //        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                //        collectionType = row["collectionType_Grant"].ToString(),
                //        collectionAmount = grantamount,
                //        Narration = row["Narration_Grant"].ToString()   
                //    });
                //}


                if (category == 10 || category == 11)
                {
                    decimal grantAmount = fees - 100;

                    transactions.Add(new
                    {
                        collectionLedgerID = row["collectionLedgerID_Grant"].ToString(),
                        collectionType = row["collectionType_Grant"].ToString(),
                        collectionAmount = grantAmount,
                        Narration = row["Narration_Grant"].ToString()
                    });
                }


                var data = new
                {
                    BranchID = GetString(row, "fGPtbl_CUnit_BranchId"),
                    CFSSUnitID = GetString(row, "CFSSUnitID"),
                    SchemeID = GetString(row, "SchemeID"),
                    CollectionBankID = GetString(row, "Bank_LedgerId"),
                    TransactionDate = GetString(row, "TransactionDate"),
                    TransactionDetails = transactions
                };

                // finalResult.Add(obj);
                return JsonConvert.SerializeObject(data, Newtonsoft.Json.Formatting.Indented);
            }
        }
        else
        {
            finalResult.Add(new { message = "Record not found" });
            //return JsonConvert.SerializeObject(finalResult, Newtonsoft.Json.Formatting.Indented);
        }

        //var finalJson = new
        //{
        //    PaymentResponse_BranchApiData = finalResult
        //};


        return JsonConvert.SerializeObject(finalResult, Newtonsoft.Json.Formatting.Indented);
    }
}