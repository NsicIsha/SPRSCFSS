using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NSICGP_VBClassLibrary;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Runtime;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

public partial class appAdministrator_NewUnitRegistrationOnline : System.Web.UI.Page
{
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;


    int UserId, RoleId, DepartmentId;
    string UserFullName;

    int BranchId;
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


    #region(GENERATECOUPON)
    private string GenerateCoupon(int length)
    {
        Random random = new Random();
        string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        StringBuilder result = new StringBuilder(length);
        for (int i = 0; i < length; i++)
        {
            result.Append(characters[random.Next(characters.Length)]);
        }
        if (!IsPostBack)
        {
            Session["SeedValue"] = result.ToString();
        }
        Session["NewSeedValue"] = result.ToString();
        return result.ToString();
    }
    private int RandomNumber(int min, int max)
    {
        Random random = new Random();
        int num = random.Next(min, max);
        if (!IsPostBack)
        {
            Session["SeedValue"] = num;
        }
        Session["NewSeedValue"] = num;
        return num;
    }


    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //try
        //{
        //    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        //    Session.Remove("step2");
        //    txt_proposal_date.Text = DateTime.Now.ToString("d/M/yyyy");
        //}
        //catch (Exception ex)
        //{
        //    UserId = 0;
        //    RoleId = 0;
        //    DepartmentId = 0;
        //    UserFullName = "";
        //}





        // RESTRICT FROM OUTSIDE 13 Sep  2018

        /*       
         objCompany = new clsCompany();
                BranchId = objCompany.MasterBranch_Fetch_BranchId(UserId);
                if (BranchId <= 0)
                {
                    Response.Redirect("http://www.nsicspronline.com/home.aspx");
                }
        */

        // txt_aahar.Text = Request.Form["ctl00$ContentPlaceHolder1$txt_aahar"];



        //TO CONTROL DDL BRANCH FOR UNIT REGISTRATION
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

                    //DropDownListBranch.Visible = true;
                    //////trbranch1.Visible = true;
                    //////trbranch2.Visible = true;
                    //////trbranch3.Visible = true;
                }
            }
            else
            {
                //FETCH BRANCHID FOR LOGIN USER
                objCompany = new clsCompany();
                BranchId = objCompany.MasterBranch_Fetch_BranchId(UserId);

                //DropDownListBranch.Visible = false;
                ////trbranch1.Visible = false;
                ////trbranch2.Visible = false;
                ////trbranch3.Visible = false;

                Mydtcity = clsDDL.ddlCity_Activated(0);

                ddlCityClone = Mydtcity.Clone();
                ViewState["mydtcity"] = Mydtcity;

            }
            //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (Request.QueryString.HasKeys())
            {
                int Provinl = Convert.ToInt32(Request.QueryString["i"]);
                if (Provinl == 0)
                {
                    ltlProvisionalNotice.Text = "For Provisional registration please use Provisional Registration link.";
                }
                else
                {
                    ltlProvisionalNotice.Text = "For PROVISIONSAL REGISTRATIONS ONLY.";
                }
            }

            FunfillDropdown();
            if (!IsPostBack)
            {
                filldataform();
            }

            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ScriptKey2", "javascript:UpdateProgressBar_DroDown();", true);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //protected void rdbUAMSelection_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdbUAMSelection.SelectedValue == "YES")
    //    {
    //        trMSMEDBSEARCH.Visible = true;
    //        trMSMEDBREGISTRATION.Visible = false;
    //    }
    //    else
    //    {
    //        trMSMEDBSEARCH.Visible = false;
    //        trMSMEDBREGISTRATION.Visible = true;
    //        //  Response.Redirect("NewUnitRegistrationOnline_WithoutUAM.aspx");
    //    }
    //}


    protected void btnf_Click(object sender, EventArgs e)
    {



        if (Convert.ToString(Session["Code"]).ToLower() != txtCaptcha.Text.Trim().ToLower())
        {
            Session["Code"] = null;
            txtCaptcha.Text = "";
            divCaptcha.Visible = true;
            txtCaptcha.Focus();
            lblCaptcha.Text = "Incorrect verification code. Please try again.";
            lblCaptcha.ForeColor = Color.Red;
            return;
        }

        Session["Code"] = null;

        dt = new DataTable();
        dt = clsDDL.ddlBranch_Activated(0);

        DropDownListBranch.DataSource = dt;
        DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
        DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
        DropDownListBranch.DataBind();
        DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
        trbranch1.Visible = true;
        trMainData.Visible = true;
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

        //  fStatus = objCompany.Company_PANnoCheck(fPANno);
        //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);
        objCompany = new clsCompany();
        DataSet ds_status = new DataSet();
        //ds_status = objCompany.Company_PANnoCheck_Modified(fPANno);
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
        //ds_status = Company_PANnoCheck_Modified(fPANno, Convert.ToInt32(ddlConstitution.SelectedValue), strUAM);
        ds_status = Company_PANnoCheck_Modified_Online(fPANno, strUAM);

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
            ////{

            ////    lbl_branchName_chek.Visible = true;
            ////    lbl_branchName_chek.Text = "Branch Not exist !!!!";
            ////    btnSubmit.Enabled = false;
            ////    // (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
            ////}

        }
        Session["Status"] = fStatus.ToString();
        if (fStatus == 1)  //CONFIRM
        {
            //lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;

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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = true;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
                btnSubmit.Enabled = true;

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
                txt_proposal_date.Text = DateTime.Now.ToString("M/d/yyyy");// dateTime.ToString();


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

                txt_1c1.Text = "";
                txt_1c2.Text = "";
                txt_1c5.Text = "";
                //  txt_1c6.Text = "";
                txt_1c7.Text = "";

                try
                {
                    ddlState_c3.SelectedValue = "";
                    ddlCity_c4.Items.Clear();
                }
                catch { }

                GridFactory.DataSource = objCompany.Fetch_Max_Factory();

                GridFactory.DataBind();

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
                    //Given By JAYAM Team
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
                    if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content) && strName != "DHIRAJ")
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
                        string strPINCode= GetNodeValue(basic, "PINCode");
                        string strDISTRICT_NAME = GetNodeValue(basic, "DISTRICT_NAME");

                        trbranch1.Visible = true;
                        trMainData.Visible = true;
                        lblMsg.Text = "";
                        txt_1p1.Text = strEnterpriseName;
                        string strOperation = "";
                        strOperation = strMajorActivityName;// xn["MajorActivity"].InnerText;
                        txt_1.Text = txtPan.Text;
                        txt_aahar.Text = txtUAM.Text;
                        ddlState_a3.SelectedValue = null;

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
                        //ddlCity_a4.Items.FindByValue(strDistrict_Id).Selected = true;


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



                //if (txtUAM.Text != "" && txtPan.Text != "")
                //{
                //    // ddlOperation.SelectedIndex = -1;
                //    ddl_1e.SelectedIndex = -1;
                //    ddlConstitution.SelectedIndex = -1;

                //    //string xmltext = ConfigurationManager.AppSettings["ServerSettings"] + txtUAM.Text + "," + txtPan.Text + "," + ConfigurationManager.AppSettings["SPRSKey"];
                //    //XmlDocument objxml = new XmlDocument();
                //    //objxml.Load(xmltext);

                //    //string sCompleteval = objxml.OuterXml;


                //    //XmlNodeList xnList = objxml.SelectNodes("//MSMEDataBank");

                //    string xmltext = ConfigurationManager.AppSettings["UAMServerSettings"] + txtUAM.Text + "," + txtUAMMobile.Text + "," + ConfigurationManager.AppSettings["UAMSPRSKey"];
                //    XmlDocument objxml = new XmlDocument();

                //    ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;


                //    objxml.Load(xmltext);


                //    XmlNodeList xnList = objxml.SelectNodes("//BasicDetail");


                //    foreach (XmlNode xn in xnList)
                //    {

                //        //if (xn["UAN"].InnerText.Equals("NO"))
                //        //{
                //        if (xn["UdyamNo"].InnerText.Equals("NO"))
                //        {
                //            lblMsg.Text = "This Unit is not registered with UDYAM Registration";


                //            trbranch1.Visible = false;
                //            trMainData.Visible = false;


                //            //txtEmail.Text = "";
                //            //txtMobile.Text = "";
                //            //txtProduct.Text = "";
                //            //ddlOperation.SelectedIndex = -1;
                //            //ddlCategory.SelectedIndex = -1;
                //            //txtCompanyName.Text = "";
                //            //txtAuthorizedName.Text = "";
                //            break;
                //        }



                //        trbranch1.Visible = true;
                //        trMainData.Visible = true;


                //        lblMsg.Text = "";

                //        txt_1p1.Text = xn["EnterpriseName"].InnerText;
                //        string strOperation = "";
                //        strOperation = xn["MajorActivity"].InnerText;

                //        //////strOperation = "2";
                //        ////if (strOperation.Trim().ToUpper() == "Manufacturing".ToUpper())
                //        ////{
                //        ////    strOperation = "1";
                //        ////}
                //        ////else if (strOperation.Trim().ToUpper() == "Services".ToUpper())
                //        ////{
                //        ////    strOperation = "2";
                //        ////}
                //        ////ddlOperation.Items.FindByValue(strOperation).Selected = true;
                //        txt_1.Text = txtPan.Text;
                //        txt_aahar.Text = txtUAM.Text;

                //        //  txtAuthorizedName.Text = xn["AuthorisedPersonName"].InnerText;

                //        ddlState_a3.SelectedValue = null;

                //        DataSet dsStates = new DataSet();
                //        string LGStateCode = "";
                //        LGStateCode = xn["LG_ST_Code"].InnerText;

                //        dsStates = Fetch_StateId_By_State_LgCode(Convert.ToInt32(LGStateCode));
                //        string strStateIdUam = Convert.ToString(dsStates.Tables[0].Rows[0]["fGPtbl_MS_Id"].ToString());
                //        ddlState_a3.Items.FindByValue(strStateIdUam).Selected = true;

                //        funfill_ddlCity_a4(Convert.ToInt32(strStateIdUam));



                //        string SocialCaterogy = "";
                //        SocialCaterogy = xn["SocialCategory"].InnerText;
                //        if (SocialCaterogy.Trim().ToUpper() == "GENERAL".ToUpper())
                //        {
                //            SocialCaterogy = "1";
                //        }
                //        else if (SocialCaterogy.Trim().ToUpper() == "OBC".ToUpper())
                //        {
                //            SocialCaterogy = "5";
                //        }
                //        else if (SocialCaterogy.Trim().ToUpper() == "SC".ToUpper())
                //        {
                //            SocialCaterogy = "10";
                //        }
                //        else if (SocialCaterogy.Trim().ToUpper() == "ST".ToUpper())
                //        {
                //            SocialCaterogy = "11";
                //        }

                //        ddl_1e.Items.FindByValue(SocialCaterogy).Selected = true;

                //        txt_1a7.Text = xn["EmailId"].InnerText;
                //        txt_mob.Text = txtUAMMobile.Text;// xn["Mobile"].InnerText;

                //        txt_1a2.Text = xn["PINCode"].InnerText;

                //        string strUnitCategory = "";
                //        string strUnitType = "";
                //        strUnitCategory = xn["EnterpriseType"].InnerText;

                //        if (strUnitCategory.Trim().ToUpper() == "Micro".ToUpper())
                //        {
                //            strUnitType = "3";
                //        }
                //        else if (strUnitCategory.Trim().ToUpper() == "Small".ToUpper().Trim())
                //        {
                //            strUnitType = "4";
                //        }
                //        else if (strUnitCategory.Trim().ToUpper() == "Medium".ToUpper().Trim())   //  zdv
                //        {

                //            //  strUnitType = "4";


                //            trMainData.Visible = false;
                //            lblMsg.Text = "This Unit comes under Medium Category. In SPRS application only Micro or Small unit can register";
                //            return;
                //            //  (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");

                //        }


                //        ddl_1f.SelectedValue = null;
                //        ddl_1f.DataBind();

                //        dt = clsDDL.ddlCategoryUnitSize();
                //        ddl_1f.DataSource = dt;
                //        ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
                //        ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
                //        ddl_1f.DataBind();
                //        ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));


                //        ddl_1f.Items.FindByValue(strUnitType).Selected = true;


                //        ddl_1d.SelectedValue = null;
                //        ddl_1d.DataBind();

                //        dt = clsDDL.ddlCategoryArea();
                //        ddl_1d.DataSource = dt;
                //        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
                //        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
                //        ddl_1d.DataBind();
                //        ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));




                //        //string strSpecialCategory = "";
                //        //if (xn["PwDEntrepreneur"].InnerText.Equals("True"))
                //        //{
                //        //    strSpecialCategory = "8";
                //        //}
                //        //else if (xn["WomanEntrepreneur"].InnerText.Equals("True"))
                //        //{
                //        //    strSpecialCategory = "6";
                //        //}
                //        //else if (xn["Minority"].InnerText.Equals("True"))
                //        //{
                //        //    strSpecialCategory = "9";
                //        //}
                //        //else if (xn["Ex-Serviceman"].InnerText.Equals("True"))
                //        //{
                //        //    strSpecialCategory = "7";
                //        //}
                //        //else
                //        //{
                //        //    strSpecialCategory = "1";
                //        //}

                //        //  ddl_1d.Items.FindByValue(strSpecialCategory).Selected = true;

                //        txt_1a1.Text = xn["CommunicationAddress"].InnerText;

                //        //funfill_ddlState();

                //        //string strState = "";
                //        //string strDistrict = "";
                //        //strState = xn["StateID"].InnerText;
                //        //strDistrict = xn["DistrictID"].InnerText;
                //        //ddlState_a3.Items.FindByValue(strState).Selected = true;

                //        //funfill_ddlCity_a4(Convert.ToInt32(strState));

                //        // ddlCity_a4.Items.FindByValue(strDistrict).Selected = true;









                //        string OrganisationType = "";
                //        OrganisationType = xn["OrganisationType"].InnerText;




                //        if (OrganisationType.Trim().ToUpper() == "Proprietary".ToUpper())
                //        {
                //            OrganisationType = "4";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Limited Liability Partnership".ToUpper().Trim())
                //        {
                //            OrganisationType = "10";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Partnership".ToUpper())
                //        {
                //            OrganisationType = "5";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Public Limited Company".ToUpper().Trim())
                //        {
                //            OrganisationType = "6";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Private Limited Company".ToUpper().Trim())
                //        {
                //            OrganisationType = "7";
                //        }

                //        else if (OrganisationType.Trim().ToUpper() == "Hindu Undivided Family".ToUpper().Trim())
                //        {
                //            OrganisationType = "9";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Co-Operative".ToUpper().Trim())
                //        {
                //            OrganisationType = "8";
                //        }
                //        else if (OrganisationType.Trim().ToUpper() == "Society".ToUpper().Trim())
                //        {
                //            OrganisationType = "8";
                //        }
                //        // 
                //        else if (OrganisationType.Trim().ToUpper() == "Trust".ToUpper().Trim())
                //        {
                //            OrganisationType = "11";
                //        }


                //        ddlConstitution.Items.FindByValue(OrganisationType).Selected = true;

                //    }
                //}





            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message + "Dhiraj");
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


            // if (ddlConstitution.SelectedValue != "4")
            if (Constitution_id != 4)
            {
                MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);
            }
            else
            {
                MyDrGrid = Company_Fetch_ByPANno_Step1_Grid(fPANno, strUAM);
            }


            GridFactory.DataSource = MyDrGrid;
            GridFactory.DataBind();
            // Added on 11 Sep 2020 Dhiraj

            funfill_ddlState();
            funfill_ddlCategory();

            // Added on 11 Sep 2020 Dhiraj

            ////DropDownListBranch.Enabled = false;
            ////trbranch1.Visible = false;
            ////trbranch2.Visible = false;
            ////trbranch3.Visible = false;
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
                #region Bind Bank Details
                try
                {
                    string BankType = MyDr["fGPtbl_BankType"].ToString();
                    int BanTypeId = 0;
                    if (BankType == "Private Sector Banks")
                    {
                        BanTypeId = 1;
                    }
                    else if (BankType == "Public Sector Banks")
                    {
                        BanTypeId = 2;
                    }
                    else if (BankType == "Regional Rural Banks")
                    {
                        BanTypeId = 3;
                    }
                    else if (BankType == "Small Finance Banks")
                    {
                        BanTypeId = 4;
                    }
                    ddlBankType.SelectedValue = BankType.ToString();
                }
                catch { }
                DataSet dsbank = new DataSet();
                dsbank = Fetch_Bank(ddlBankType.SelectedItem.Text);
                dsbank = Fetch_Bank(ddlBankType.SelectedItem.Text);
                ddlBankName.DataSource = dsbank;
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataValueField = "BankID";
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("--Select Bank--", "0"));


               

                try
                {
                    string bankName = MyDr["fGPtbl_BankName"].ToString();
                    ListItem item = ddlBankName.Items.FindByText(bankName);
                    if (item != null)
                    {
                        ddlBankName.ClearSelection();
                        item.Selected = true;
                    }
                    //ddlBankName.SelectedItem.Text = MyDr["fGPtbl_BankName"].ToString();
                }
                catch { }

                txtIFSCCode.Text = MyDr["fGPtbl_IFSCCOde"].ToString();
                txtBranchName.Text = MyDr["fGPtbl_BranchName"].ToString();
                txtBranchAddress.Text = MyDr["fGPtbl_BranchName"].ToString();

                try
                {
                    string AccountType = MyDr["fGPtbl_AccountType"].ToString();
                    ListItem itemAccountType = ddlAccountType.Items.FindByText(AccountType);
                    if (itemAccountType != null)
                    {
                        ddlAccountType.ClearSelection();
                        itemAccountType.Selected = true;
                    }
                }
                catch { }


                //try
                //{
                //    string AccountType = MyDr["fGPtbl_AccountType"].ToString();
                //    ddlAccountType.SelectedItem.Text = AccountType.ToString();
                //}
                //catch { }
                txtAccountNumber.Text = MyDr["fGPtbl_AccountNumber"].ToString();
                txtAccountHolderName.Text = MyDr["fGPtbl_AccountHolderName"].ToString();


                #endregion


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
                txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString() == "" ? "" : MyDr["fGtbl_Company_AdharNumber"].ToString();
                //dhiraj
                txtGstNumber.Text = MyDr["fGtbl_Company_GSTNumber"].ToString() == "" ? "" : MyDr["fGtbl_Company_GSTNumber"].ToString();

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

                //rdo_declarationMSME.SelectedValue = MyDr["fGPtbl_Company_MSME"].ToString() == "1" ? "1" : "2";
                //Isha: Ayush addition start-- >
                rdo_AyushDeclaration.SelectedValue = MyDr["fGPtbl_IsAyush"].ToString() == "1" ? "1" : "0";
                //Isha: Ayush addition end<-- 


                rdoShareInfo.SelectedValue = MyDr["fGPtbl_Company_ShareInfo"].ToString() == "1" ? "1" : "2";
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

                    ////if (strUnitTypes.ToString() != "3" || strUnitTypes.ToString() != "4")
                    ////{
                    ////    trMainData.Visible = false;
                    ////    lblMsg.Text = "This Unit comes under Medium Category. In SPRS application only Micro or Small unit can register";
                    ////    return;
                    ////    //  (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");

                    ////}
                    ///
                    //ddl_1f.Items.FindByValue(strUnitTypes).Selected = true;


                    ddl_1f.SelectedValue = MyDr["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();

                }
                catch { }

                txt_1c1.Text = MyDr["fGPtbl_CUnitBranch_Address"].ToString();
                txt_1c2.Text = MyDr["fGPtbl_CUnitBranch_Zip"].ToString();
                txt_1c5.Text = MyDr["fGPtbl_CUnitBranch_Phone"].ToString();
                //  txt_1c6.Text = MyDr["fGPtbl_CUnitBranch_Fax"].ToString();
                txt_1c7.Text = MyDr["fGPtbl_CUnitBranch_Email"].ToString();
                try
                {
                    ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                }
                catch { }
                try
                {
                    if (MyDr["fGPtbl_CUnitBranch_StateId"].ToString() == "")
                    {

                    }
                    else
                    {
                        funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                    }
                    if (MyDr["fGPtbl_CUnitBranch_CityId"].ToString() == "")
                    {
                    }
                    else
                    {
                        ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                    }
                }
                catch { }

                if (BranchId != (Convert.ToInt32(MyDr["fGPtbl_CUnit_BranchId"].ToString())))
                {
                    btnSubmit.Visible = true;
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
    public class Root
    {
        public List<UnitDetail> Unit_Detail { get; set; }
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
    private void FunfillDropdown()
    {
        if (!Page.IsPostBack)
        {
            funfill_ddlState();
            funfill_ddlCategory();
            objCompany = new clsCompany();
            GridFactory.DataSource = objCompany.Fetch_Max_Factory();
            GridFactory.DataBind();
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
        ddlState_c3.DataSource = dt;
        ddlState_c3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_c3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_c3.DataBind();
        ddlState_c3.Items.Insert(0, new ListItem("--Select State--", ""));
        dt.Clear();
        dt.Dispose();
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
    private DataTable funfill_ddlCity_b4(int fStateId, DropDownList ddl)
    {
        //CITY DROPDOWNLIST
        System.Data.DataTable dt_c = new System.Data.DataTable();
        if (fStateId != 0)
        {

            dt_c = clsDDL.ddlCity_Activated(fStateId);
            return dt_c;
        }
        else
        {
            //ddlCity_b4.Items.Clear();
            return dt_c;
        }
    }
    private void funfill_ddlCity_c4(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlCity_c4.DataSource = dt_c;
            ddlCity_c4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity_c4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity_c4.DataBind();
            ddlCity_c4.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();
        }
        else
        {
            ddlCity_c4.Items.Clear();
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

    protected void ddlState_b3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList myDropDown = (sender as DropDownList);

        int fStateId = 0;
        try
        {/*
            //======UTTAM======================================
            expression = "fGPtbl_MCty_StateId=" + Convert.ToInt32(myDropDown.SelectedItem.Value);

            Mydtcity = (DataTable)ViewState["mydtcity"];
            //ddlCityClone = Mydtcity.Clone();
            cityRows = Mydtcity.Select(expression);

            foreach (DataRow r1 in cityRows)
            {
                ddlCityClone.ImportRow(r1);
            }
            //===============================================
            */
            fStateId = Convert.ToInt32(myDropDown.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }

        GridViewRow gridViewRowDdlBelongsTo = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList secondDropDownList = (DropDownList)gridViewRowDdlBelongsTo.FindControl("ddlCity_b4");

        System.Data.DataTable dt_c = new System.Data.DataTable();

        dt_c = funfill_ddlCity_b4(fStateId, secondDropDownList);
        secondDropDownList.DataSource = dt_c;
        secondDropDownList.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
        secondDropDownList.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();

        /*
       secondDropDownList.DataSource = ddlCityClone;
       secondDropDownList.DataTextField = ddlCityClone.Columns["fGPtbl_MCty_Name"].ToString();
       secondDropDownList.DataValueField = ddlCityClone.Columns["fGPtbl_MCty_Id"].ToString();
       */
        secondDropDownList.DataBind();
        secondDropDownList.Items.Insert(0, new ListItem("--Select City--", ""));

    }

    protected void ddlState_c3_SelectedIndexChanged(object sender, EventArgs e)
    {
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlState_c3.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlCity_c4(fStateId);
    }

    //protected void Page_Unload(object sender, EventArgs e)
    //{

    //    ViewState["txtUAM"] = txt_aahar.Text;
    //    txt_aahar.Attributes.Add("value", ViewState["txtUAM"].ToString());


    //}



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
                btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;

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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
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
            btnSubmit.Enabled = false;
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
                btnSubmit.Enabled = false;
            }
            else
            {
                btnSubmit.Enabled = true;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = false;
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
            btnSubmit.Enabled = true;

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

            txt_1c1.Text = "";
            txt_1c2.Text = "";
            txt_1c5.Text = "";
            // txt_1c6.Text = "";
            txt_1c7.Text = "";

            try
            {
                ddlState_c3.SelectedValue = "";
                ddlCity_c4.Items.Clear();
            }
            catch { }

            GridFactory.DataSource = objCompany.Fetch_Max_Factory();
            GridFactory.DataBind();

        }

        if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1 || fStatus == 7 || fStatus == 6)
        {
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDrGrid;
            if (ddlConstitution.SelectedValue != "4")
            {
                MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);
            }
            else
            {
                MyDrGrid = Company_Fetch_ByPANno_Step1_Grid(fPANno, strUAM);
            }



            GridFactory.DataSource = MyDrGrid;
            GridFactory.DataBind();


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


                //dhiraj
                txtGstNumber.Text = MyDr["fGtbl_Company_GSTNumber"].ToString() == "" ? "" : MyDr["fGtbl_Company_GSTNumber"].ToString();

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

                // rdo_declarationMSME.SelectedValue = MyDr["fGPtbl_Company_MSME"].ToString() == "1" ? "1" : "2";
                //Isha: Ayush addition start-- >
                rdo_AyushDeclaration.SelectedValue = MyDr["fGPtbl_IsAyush"].ToString() == "1" ? "1" : "0";
                //Isha: Ayush addition end<-- 


                rdoShareInfo.SelectedValue = MyDr["fGPtbl_Company_ShareInfo"].ToString() == "1" ? "1" : "2";
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

                txt_1c1.Text = MyDr["fGPtbl_CUnitBranch_Address"].ToString();
                txt_1c2.Text = MyDr["fGPtbl_CUnitBranch_Zip"].ToString();
                txt_1c5.Text = MyDr["fGPtbl_CUnitBranch_Phone"].ToString();
                // txt_1c6.Text = MyDr["fGPtbl_CUnitBranch_Fax"].ToString();
                txt_1c7.Text = MyDr["fGPtbl_CUnitBranch_Email"].ToString();

                try
                {
                    ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                }
                catch { }

                try
                {
                    if (MyDr["fGPtbl_CUnitBranch_StateId"].ToString() == "")
                    {

                    }
                    else
                    {
                        funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                    }
                    if (MyDr["fGPtbl_CUnitBranch_CityId"].ToString() == "")
                    {
                    }
                    else
                    {
                        ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                    }
                }
                catch { }

                if (BranchId != (Convert.ToInt32(MyDr["fGPtbl_CUnit_BranchId"].ToString())))
                {
                    btnSubmit.Visible = true;
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

    protected void btnSubmit_Click(object sender, EventArgs e)
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
        int MandiBranchID = 107;
        int RajpuraBranchID = 22; //Now will go to Ludhiyana branch instead of Rajpura 53
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
                //Change done for NSIC Mandi

                BranchId = Convert.ToInt32(DropDownListBranch.SelectedValue) == MandiBranchID ? RajpuraBranchID : Convert.ToInt32(DropDownListBranch.SelectedValue);

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
            BranchID_Check = Convert.ToInt32(ds_status.Tables[0].Rows[0]["BranchID"].ToString()) == MandiBranchID ? RajpuraBranchID : Convert.ToInt32(ds_status.Tables[0].Rows[0]["BranchID"].ToString());
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
                btnSubmit.Enabled = false;
                // (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
            }

        }

        Session["Status"] = fStatus.ToString();



        try
        {
            if (Session["Status"] != null || Session["Status"] != "")
            {
                fStatus = Convert.ToInt32(Session["Status"].ToString());
            }
        }
        catch { }



        //FOR PROVISIONAL COMPANY
        int provision = Convert.ToInt32(Request.QueryString["i"]);

        string bsPANno = txt_1.Text.Trim().ToUpper();
        // Session["PANNumber"] = bsPANno.ToString();
        string bsNameofApplicantFirm = txt_1p1.Text.Trim();
        string bsHOAddress = txt_1a1.Text.Trim();
        string bsHOPinCode = txt_1a2.Text.Trim();
        string Proposal_date = txt_proposal_date.Text.ToString() == "" ? "" : txt_proposal_date.Text.ToString();
        int bsHOState = 0;
        bool bsDeclarationMSME = true;// rdo_declarationMSME.SelectedValue == "1" ? true : false;
        bool bsShareInfo = rdoShareInfo.SelectedValue == "1" ? true : false;
        //Isha: Ayush addition start-- >
        bool bsIsAyush = rdo_AyushDeclaration.SelectedValue == "1" ? true : false;
        //Isha: Ayush addition end<-- 
        //FOR BRANCH ID
        try
        {
            if (fStatus == 123)
            {
                if (BranchId == 0)
                {
                    BranchId = Convert.ToInt32(DropDownListBranch.SelectedValue) == MandiBranchID ? RajpuraBranchID : Convert.ToInt32(DropDownListBranch.SelectedValue);
                }
            }
        }
        catch { }
        if (Convert.ToInt32(DropDownListBranch.SelectedValue) == MandiBranchID)
        {
            UpdateMandiToRajpura();
        }
        try
        {
            bsHOState = Convert.ToInt32(ddlState_a3.SelectedValue);
        }
        catch { }
        int bsHOCity = 0;
        try
        {
            bsHOCity = Convert.ToInt32(ddlCity_a4.SelectedValue);
        }
        catch { }
        string bsHOTelephone = txt_1a5.Text.Trim();
        string bsHOFax = "";// txt_1a6.Text.Trim();
        string bsHOEmail = txt_1a7.Text.Trim();

        int bsFTState = 0;
        try
        {
            // bsFTState = Convert.ToInt32(ddlState_b3.SelectedValue);
        }
        catch { }
        int bsFTCity = 0;
        try
        {
            // bsFTCity = Convert.ToInt32(ddlCity_b4.SelectedValue);
        }
        catch { }


        //Code Added by Anupam 18 January 2012

        int bsCategoryAreaId = 0;
        try
        {
            bsCategoryAreaId = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        catch { }

        int bsCategoryCastId = 0;
        try
        {
            bsCategoryCastId = Convert.ToInt32(ddl_1e.SelectedValue);
        }
        catch { }

        int bsCategoryUnitSizeId = 0;
        try
        {
            bsCategoryUnitSizeId = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        catch { }

        string bsBRAddress = "";
        string bsBRPin = "";
        string bsBRPhone = "";
        string bsBRFax = "";
        string bsBREmail = "";
        string Aadhar_card = "";

        //Dhiraj NSIC
        string GstNumber = "";

        if (txtGstNumber.Text.Trim().ToString() != "")
        {
            GstNumber = txtGstNumber.Text.Trim().ToString();
        }

        //Dhiraj NSIC 19/07/2017
        int bsSCSTInitiaveInfo = 0;
        /*
        if (rdbSCSTInitiative.SelectedValue == "1")
        {
            bsSCSTInitiaveInfo = 1;
        }
        else
        {
            bsSCSTInitiaveInfo = 2;
        }
        */

        if (txt_aahar.Text.Trim().ToString() != "")
        {
            Aadhar_card = txt_aahar.Text.Trim().ToString();
        }

        if (txt_1c1.Text.Trim().ToString() != "")
        {
            bsBRAddress = txt_1c1.Text.Trim().ToString();
        }

        if (txt_1c2.Text.Trim().ToString() != "")
        {
            bsBRPin = txt_1c2.Text.Trim().ToString();
        }

        if (txt_1c5.Text.Trim().ToString() != "")
        {
            bsBRPhone = txt_1c5.Text.Trim().ToString();
        }

        //if (txt_1c6.Text.Trim().ToString() != "")
        //{
        //    bsBRFax = txt_1c6.Text.Trim().ToString();
        //}

        bsBRFax = "N.A.";
        if (txt_1c7.Text.Trim().ToString() != "")
        {
            bsBREmail = txt_1c7.Text.Trim().ToString();
        }

        int bsBRState = 0;
        int bsBRCity = 0;

        try
        {
            if (ddlState_c3.SelectedIndex > 0)
            {
                bsBRState = Convert.ToInt32(ddlState_c3.SelectedValue);
            }

        }
        catch { }

        try
        {
            if (ddlCity_c4.SelectedIndex > 0)
            {
                bsBRCity = Convert.ToInt32(ddlCity_c4.SelectedValue);
            }

        }
        catch { }


        objCompany = new clsCompany();
        if (fStatus == 123) //NEW ENTRY
        {
            clsGenerateNo objGenerate = new clsGenerateNo();


            //30-11-2017
            //BranchId = 0;
            if (DropDownListBranch.SelectedValue == "" && BranchId == 0)
            {
                Response.Redirect("Login.aspx");
            }
            else if (DropDownListBranch.SelectedValue != "" && BranchId == 0)
            {
                Response.Redirect("../home.aspx");
            }
            //30-11-2017


            string ReferenceNo = objGenerate.Company_ReferenceNo(BranchId);

            int i = 0;
            while (i < 5)
            {
                Hashtable hstbl = new Hashtable();
                string bsFTAddress = "", bsFTPinCode = "", bsFTTelephone = "", bsFTFax = "", bsFTEmail = "";
                int bsFTNatureOfOperation;

                //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");

                TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");

                if ((txtadd.Text).Trim() != "")
                {

                    TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
                    TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
                    // TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
                    TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
                    DropDownList ddlftstate = (DropDownList)GridFactory.Rows[i].FindControl("ddlState_b3");
                    DropDownList ddlftcity = (DropDownList)GridFactory.Rows[i].FindControl("ddlCity_b4");
                    //RA
                    DropDownList ddlftNatureofOperation = (DropDownList)GridFactory.Rows[i].FindControl("ddlNatureOfOperation_b8");
                    //RA

                    bsFTAddress = txtadd.Text;
                    bsFTPinCode = txtpin.Text;
                    bsFTTelephone = txtphn.Text;
                    bsFTFax = "0000";// txtfax.Text;
                    bsFTEmail = txtemail.Text;
                    bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);

                    if (ddlConstitution.SelectedValue != "4")
                    {
                        hstbl = objCompany.Company_Insert_Step1(ReferenceNo, bsNameofApplicantFirm, bsPANno, bsHOAddress,
                       bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone, bsHOFax, bsHOEmail,
                       bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail,
                       bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, BranchId, bsBRAddress, bsBRPin,
                       bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail, UserId, provision, bsFTNatureOfOperation, txt_mob.Text.Trim(), Proposal_date, Aadhar_card, bsDeclarationMSME, bsShareInfo, bsIsAyush, GstNumber, bsSCSTInitiaveInfo);

                    }
                    else
                    {
                        hstbl = Company_Insert_Step1_Properwritership(ReferenceNo, bsNameofApplicantFirm, bsPANno, bsHOAddress,
                        bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone, bsHOFax, bsHOEmail,
                        bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail,
                        bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, BranchId, bsBRAddress, bsBRPin,
                        bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail, UserId, provision, bsFTNatureOfOperation, txt_mob.Text.Trim(), Proposal_date, Aadhar_card, bsDeclarationMSME, bsShareInfo, bsIsAyush, GstNumber, bsSCSTInitiaveInfo);

                    }





                    CompanyId = Convert.ToInt32(hstbl["CompanyId"].ToString());
                    UnitId = Convert.ToInt32(hstbl["UnitId"].ToString());
                    ErrorId = Convert.ToInt32(hstbl["ErrorId"].ToString());

                    //try
                    //{
                    //    int errorid_bank = 0;
                    //    errorid_bank = company_accountdetails(companyid, ddlbanktype.selecteditem.text, ddlbankname.selecteditem.text, txtifsccode.text, txtbranchname.text, txtbranchname.text, ddlaccounttype.selecteditem.text, txtaccountnumber.text, txtaccountholdername.text);

                    //}
                    //catch (exception ex)
                    //{
                    //    errhandler.writeerror(ex.message + "company_accountdetails");
                    //}

                    //Added on 22 June 2020
                    //  hdnCompany.Value = CompanyId.ToString();
                    // ss
                }
                i = i + 1;
            }
        }
        else if (fStatus == 5 || fStatus == 6)
        {

            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDr;

            if (ddlConstitution.SelectedValue != "4")
            {
                MyDr = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);
            }
            else
            {
                MyDr = Company_Fetch_ByPANno_Step1_Grid(fPANno, txtUAM.Text);
            }
            int cnt = 0;

            clsGenerateNo objGenerate = new clsGenerateNo();
            string ReferenceNo = objGenerate.Company_ReferenceNo(BranchId);


            while (MyDr.Read())
            {
                if (cnt == 0)
                {
                    cnt = cnt + 1;
                    CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                    UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                    hdnCompany.Value = CompanyId.ToString();

                    Company_Update_RefNumber(CompanyId, ReferenceNo);
                }
            }

            int i = 0;
            while (i < 5)
            {
                Hashtable hstbl = new Hashtable();
                string bsFTAddress = "", bsFTPinCode = "", bsFTTelephone = "", bsFTFax = "", bsFTEmail = "";

                //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");

                TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");
                HiddenField hdnfld = (HiddenField)GridFactory.Rows[i].FindControl("hdnunit");
                CompanyId = Convert.ToInt32(hdnCompany.Value.ToString());
                UnitId = Convert.ToInt32(hdnfld.Value.ToString());

                TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
                TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
                //   TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
                TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
                DropDownList ddlftstate = (DropDownList)GridFactory.Rows[i].FindControl("ddlState_b3");
                DropDownList ddlftcity = (DropDownList)GridFactory.Rows[i].FindControl("ddlCity_b4");
                //RA
                DropDownList ddlftNatureofOperation = (DropDownList)GridFactory.Rows[i].FindControl("ddlNatureOfOperation_b8");
                //RA
                bsFTAddress = txtadd.Text;
                bsFTPinCode = txtpin.Text;
                bsFTTelephone = txtphn.Text;
                bsFTFax = "0000";// txtfax.Text;
                bsFTEmail = txtemail.Text;//================================================

                if (UnitId != 0 && txtadd.Text.Trim() != "")
                {
                    //bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    //bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);

                    if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        bsFTState = 0;
                    }
                    else
                    {
                        bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    }
                    if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        bsFTCity = 0;
                    }
                    else
                    {
                        bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    }
                    int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);
                    ErrorId = objCompany.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                        bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                        bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail,
                        UserId, CompanyId, UnitId, provision, bsFTNatureOfOperation, txt_mob.Text.Trim(), Proposal_date, Aadhar_card, bsDeclarationMSME, bsShareInfo, GstNumber, bsSCSTInitiaveInfo);
                }
                else if (UnitId == 0 && txtadd.Text.Trim() != "")
                {
                    //bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    // bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        bsFTState = 0;
                    }
                    else
                    {
                        bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    }
                    if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        bsFTCity = 0;

                    }
                    else
                    {
                        bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    }
                    int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);
                    ErrorId = objCompany.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                       bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                       bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity,
                       bsBRPhone, bsBRFax, bsBREmail, UserId, CompanyId, UnitId, provision,
                       bsFTNatureOfOperation, txt_mob.Text.Trim(), Proposal_date, Aadhar_card, bsDeclarationMSME, bsShareInfo, GstNumber, bsSCSTInitiaveInfo);

                }

                if (BranchId == 0)
                {
                    BranchId = objCompany.Company_Fetch_BranchId(UnitId);
                }

                i = i + 1;

            }

        }

        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            string mobileno = txt_mob.Text;
            if (mobileno != string.Empty)
            {
                string message = "Dear Customer, your registration process for SPRS has been initiated." + System.Environment.NewLine + "NSIC Team";
                if (fStatus == 123)
                {
                    sendsms.Send(message, mobileno);
                }

                //Update Msme Registration Type
                int MsmeRegType = 0;
                //if (rdbMsmeRegtype.SelectedValue == "1")
                //{
                //    MsmeRegType = 1;
                //}
                //else if (rdbMsmeRegtype.SelectedValue == "2")
                //{
                //    MsmeRegType = 2;
                //}
                //else
                //{
                //    MsmeRegType = 0;
                //}

                Company_Update_MsmeRegType(Convert.ToInt32(CompanyId.ToString()), Convert.ToInt32(MsmeRegType));

                //Update Msme Registration Type


            }


            objCompany = new clsCompany();
            DataSet ds_statusss = new DataSet();
            //ds_statusss = CompanyStatus_Rejected_StatusChange(CompanyId);
            if (fStatus == 6)
            {
                ds_statusss = CompanyStatus_Rejected_StatusChange(CompanyId);
                if (ds_status.Tables[0].Rows.Count > 0)
                {
                    fStatus = Convert.ToInt32(ds_statusss.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString());
                    Session["Status"] = fStatus.ToString();
                }
                else
                {
                    ds_status.Dispose();
                }
            }

            clsCompany objCons = new clsCompany();
            int aa = UpadteConstitution_Properitership(CompanyId, Convert.ToInt32(ddlConstitution.SelectedValue));





            try
            {
                int ErrorId_Bank = 0;
                ErrorId_Bank = Company_AccountDetails(CompanyId, ddlBankType.SelectedValue, ddlBankName.SelectedItem.Text, txtIFSCCode.Text, txtBranchName.Text, txtBranchName.Text, ddlAccountType.SelectedItem.Text, txtAccountNumber.Text, txtAccountHolderName.Text);

            }
            catch (Exception ex)
            {
           
                ErrHandler.WriteError(ex.Message + "Company_AccountDetails");
            }



            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(fStatus.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string PanCard = objDecEnc.Encrypt(txt_1.Text.ToString());
            string ConstitutionId = objDecEnc.Encrypt(ddlConstitution.SelectedValue.ToString());
            //Response.Write(ErrorId);
            Response.Redirect("NewUnitRegistrationStep2Online.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + PanCard + "&con=" + ConstitutionId + "&Edit=0", false);
        }
        else               //INSERT/UPDATE FAIL
        {
            // Response.Write(ErrorId);
        }
    }


    public void Company_Update_RefNumber(int CompanyId, string  RefNumber)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Update_RefNumber_CompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.VarChar, 5000).Value = CompanyId;
            MySqlComm.Parameters.Add("@RefNumber", SqlDbType.VarChar, 5000).Value = RefNumber;
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
    private void UpdateMandiToRajpura()
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_UpdateMandiToRajpura", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = txt_1.Text;
            MySqlComm.Parameters.Add("@UAM", SqlDbType.VarChar, 50).Value = txt_aahar.Text;
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

    public DataSet CompanyStatus_Rejected_StatusChange(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CheckCompanyStatus_Rejected_StatusChange", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public void Company_Update_MsmeRegType(int CompanyId, int MsmeRegType)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Update_MSMERegType_CompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.VarChar, 5000).Value = CompanyId;
            MySqlComm.Parameters.Add("@MsmeRegType", SqlDbType.VarChar, 5000).Value = MsmeRegType;
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
    public Hashtable Company_Insert_Step1_Properwritership(string fGPtbl_Company_GPReferenceNo, string fGPtbl_Company_Name, string fGPtbl_Company_PAN,
       string fGPtbl_Company_HeadOffice_Address, string fGPtbl_Company_HeadOffice_Zip, int fGPtbl_Company_HeadOffice_StateId,
       int fGPtbl_Company_HeadOffice_CityId, string fGPtbl_Company_HeadOffice_Phone, string fGPtbl_Company_HeadOffice_Fax,
       string fGPtbl_Company_HeadOffice_Email, string fGPtbl_CUnit_1B_Factory_Address, string fGPtbl_CUnit_1B_Factory_Zip,
       int fGPtbl_CUnit_1B_Factory_StateId, int fGPtbl_CUnit_1B_Factory_CityId, string fGPtbl_CUnit_1B_Factory_Phone, string fGPtbl_CUnit_1B_Factory_Fax,
       string fGPtbl_CUnit_1B_Factory_Email, int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
       int fGPtbl_CUnit_BranchId, string fGPtbl_CUnitBranch_Address, string fGPtbl_CUnitBranch_Zip, int fGPtbl_CUnitBranch_StateId, int fGPtbl_CUnitBranch_CityId,
       string fGPtbl_CUnitBranch_Phone, string fGPtbl_CUnitBranch_Fax, string fGPtbl_CUnitBranch_Email, int UserId, int provision, int enterprisedefnition,
       string comapnymobile, string Proposal_date, string Aadhar_number, bool fGPtbl_Company_MSME, bool fGPtbl_Company_ShareInfo, bool fGPtbl_IsAyush, string GstNumber, int fGtbl_Company_IsSCST_Initiative)
    {
        int UnitId = 0;
        int CompanyId = 0;


        Hashtable hstbl = new Hashtable();
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step1_RA_Properitership", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_GPReferenceNo", SqlDbType.VarChar, 500).Value = fGPtbl_Company_GPReferenceNo;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.Char, 10).Value = fGPtbl_Company_PAN;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_HeadOffice_Address;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_Company_HeadOffice_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Phone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = comapnymobile;
            if (fGPtbl_Company_HeadOffice_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 500).Value = fGPtbl_Company_HeadOffice_Email;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnit_1B_Factory_Address;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnit_1B_Factory_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_StateId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_CityId", SqlDbType.Int).Value = fGPtbl_CUnit_1B_Factory_CityId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Phone;
            if (fGPtbl_CUnit_1B_Factory_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Fax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_1B_Factory_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnit_1B_Factory_Email;

            //Code added by Anupam 18 January 2012

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
            MySqlComm.Parameters.Add("fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = fGPtbl_CUnit_BranchId;
            if (fGPtbl_CUnitBranch_Address.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Address", SqlDbType.VarChar, 5000).Value = fGPtbl_CUnitBranch_Address;
            if (fGPtbl_CUnitBranch_Zip.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Zip", SqlDbType.VarChar, 10).Value = fGPtbl_CUnitBranch_Zip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_StateId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_StateId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_CityId", SqlDbType.Int).Value = fGPtbl_CUnitBranch_CityId;
            if (fGPtbl_CUnitBranch_Phone.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Phone", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Phone;
            if (fGPtbl_CUnitBranch_Fax.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Fax", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Fax;
            if (fGPtbl_CUnitBranch_Email.Trim() != "") MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Email", SqlDbType.VarChar, 500).Value = fGPtbl_CUnitBranch_Email;

            MySqlComm.Parameters.Add("@fGPtbl_Company_MSME", SqlDbType.Bit).Value = fGPtbl_Company_MSME;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ShareInfo", SqlDbType.Bit).Value = fGPtbl_Company_ShareInfo;
            MySqlComm.Parameters.Add("@fGPtbl_IsAyush", SqlDbType.Bit).Value = fGPtbl_IsAyush;

            //FOR PROVISIONAL COMPANY
            MySqlComm.Parameters.Add("@fGPtbl_Company_provision", SqlDbType.Int).Value = provision;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            MySqlComm.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            if (Proposal_date != "") MySqlComm.Parameters.Add("@proposaldate", SqlDbType.VarChar, 50).Value = Proposal_date;
            if (Aadhar_number != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = Aadhar_number;

            //Dhiraj
            if (GstNumber != "") MySqlComm.Parameters.Add("@fGtbl_Company_GSTNumber", SqlDbType.VarChar, 50).Value = GstNumber;

            //Dhiraj NSIC 19072017
            MySqlComm.Parameters.Add("@fGtbl_Company_IsSCST_Initiative", SqlDbType.Int).Value = fGtbl_Company_IsSCST_Initiative;



            SqlParameter outCompany = new SqlParameter("@CompanyId", SqlDbType.Int);
            outCompany.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outCompany);

            SqlParameter outUnit = new SqlParameter("@UnitId", SqlDbType.Int);
            outUnit.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outUnit);

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            CompanyId = Convert.ToInt32(outCompany.Value);
            UnitId = Convert.ToInt32(outUnit.Value);
            ErrorId = Convert.ToInt32(outError.Value);

            hstbl.Add("CompanyId", CompanyId);
            hstbl.Add("UnitId", UnitId);
            hstbl.Add("ErrorId", ErrorId);

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

        return hstbl;
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

    protected void txt_21_TextChanged(object sender, EventArgs e)
    {
        lblPANmsg.Visible = false;

        //System.Threading.Thread.Sleep(5000);
        string fPANno = "";
        string fUam = "";
        string ssiNo = "";
        fPANno = txt_1.Text.ToString().ToUpper();

        fUam = txt_aahar.Text.ToString().ToUpper();

        int fPANlength = 0;
        fPANlength = fPANno.Length;
        if (fPANlength != 10)
        {
            txt_1.Focus();
        }
        //ssiNo = txt_21.Text.ToString();

        objCompany = new clsCompany();

        DataSet ds_status_update = new DataSet();

        //ds_status_update = objCompany.Company_PANnoCheck_Modified(fPANno);

        ds_status_update = Company_PANnoCheck_Modified(fPANno, Convert.ToInt32(ddlConstitution.SelectedValue), fUam);

        if (ds_status_update.Tables[0].Rows.Count > 0)
        {
            fStatus = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["Status"].ToString());
            AccessId = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["AccessId"].ToString());
            BranchID_Check = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["BranchID"].ToString());
        }
        else
        {
            ds_status_update.Dispose();
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
                btnSubmit.Enabled = false;
                lbl_branchName_chek.Visible = true;
                lbl_branchName_chek.Text = "Branch Not exist !!!!";
                //(HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
            }

        }

        //  fStatus = objCompany.Company_PANnoCheck(fPANno);
        //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);
        //fStatus = objCompany.Company_PanSSInoCheck(ssiNo, fPANno);
        //objAmendment = new clsAmendment();
        // ssistatus = objAmendment.Company_SSInoCheck(ssiNo);
        Session["Status"] = fStatus.ToString();
        if (fStatus == 1)  //CONFIRM
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already confirmed";
        }
        else if (fStatus == 2)  //INPROCESS
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with INPROCESS status";
        }
        else if (fStatus == 3)  //WAITING
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with WAITING status";
        }
        else if (fStatus == 4)  //EXPIRED
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with EXPIRED status";
        }
        else if (fStatus == 5 && AccessId == 0)  //INCOMPLETE
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = true;
            lblPANmsg.Text = "PAN already exists with INCOMPLETE status.";
        }
        else if (fStatus == 5 && AccessId == 4)
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using BackLog Registration link";
        }
        else if (fStatus == 123)  //NEW ENTRY
        {
            lblPANmsg.Visible = false;
            btnSubmit.Enabled = true;

            txt_1p1.Text = "";
            txt_1a1.Text = "";
            txt_1a2.Text = "";
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

            //txt_1b1.Text = "";
            //txt_1b2.Text = "";
            try
            {
                //ddlState_b3.SelectedValue = "";
                //ddlCity_b4.Items.Clear();
            }
            catch { }

            //txt_1b5.Text = "";
            //txt_1b6.Text = "";
            //txt_1b7.Text = "";
            try
            {
                ddl_1d.SelectedValue = "";
            }
            catch { }

            try
            {
                ddl_1f.SelectedValue = "";
            }
            catch { }

            txt_1c1.Text = "";
            txt_1c2.Text = "";
            txt_1c5.Text = "";
            // txt_1c6.Text = "";
            txt_1c7.Text = "";

            try
            {
                ddlState_c3.SelectedValue = "";
                ddlCity_c4.Items.Clear();
            }
            catch { }

        }

        if (fStatus == 5)
        {
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDr;

            if (ddlConstitution.SelectedValue != "4")
            {
                MyDr = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);
            }
            else
            {
                MyDr = Company_Fetch_ByPANno_Step1_Grid(fPANno, fUam);
            }




            ////DropDownListBranch.Enabled = false;
            ////trbranch1.Visible = false;
            ////trbranch2.Visible = false;
            ////trbranch3.Visible = false;

            while (MyDr.Read())
            {
                CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                hdnCompany.Value = CompanyId.ToString();
                // rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();
                txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString();

                //Dhiraj
                txtGstNumber.Text = MyDr["fGtbl_Company_GSTNumber"].ToString();

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

                txt_1c1.Text = MyDr["fGPtbl_CUnitBranch_Address"].ToString();
                txt_1c2.Text = MyDr["fGPtbl_CUnitBranch_Zip"].ToString();
                txt_1c5.Text = MyDr["fGPtbl_CUnitBranch_Phone"].ToString();
                // txt_1c6.Text = MyDr["fGPtbl_CUnitBranch_Fax"].ToString();
                txt_1c7.Text = MyDr["fGPtbl_CUnitBranch_Email"].ToString();


                try
                {
                    ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                }
                catch { }

                try
                {
                    funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                    ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                }
                catch { }


            }
            MyDr.Close();
        }

    }

    private void funfill_ddlState_Factory()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlState_c3.DataSource = dt;
        ddlState_c3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_c3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_c3.DataBind();
        ddlState_c3.Items.Insert(0, new ListItem("--Select State--", ""));
        dt.Clear();
        dt.Dispose();
    }

    protected void GridFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (!IsPostBack)
        //{


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Bind drop down to cities           
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlState_b3");

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsDDL.ddlState_Activated();

            ddl.DataSource = dt;
            ddl.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
            ddl.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select State--", ""));

            dt.Clear();
            dt.Dispose();

            HiddenField hdnstate = (HiddenField)e.Row.FindControl("hdnstate");
            HiddenField hdncity = (HiddenField)e.Row.FindControl("hdncity");
            HiddenField hdnNatureOfOperation = (HiddenField)e.Row.FindControl("hdnNatureOfOperation");

            DropDownList ddlcity = (DropDownList)e.Row.FindControl("ddlCity_b4");
            DropDownList ddlftNatureofOperation = (DropDownList)e.Row.FindControl("ddlNatureOfOperation_b8");
            ddlftNatureofOperation.SelectedValue = hdnNatureOfOperation.Value;

            if (string.IsNullOrEmpty(hdnstate.Value))
            {
                // Value is null or empty
            }
            else
            {
                // Value exists
                if (Convert.ToInt32(hdnstate.Value) != 0)
                {
                    ddl.SelectedValue = (hdnstate.Value.ToString());

                    System.Data.DataTable dt_c = new System.Data.DataTable();
                    dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl.SelectedValue));
                    ddlcity.DataSource = dt_c;
                    ddlcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
                    ddlcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                    ddlcity.DataBind();
                    ddlcity.Items.Insert(0, new ListItem("--Select City--", ""));
                    dt_c.Clear();
                    dt_c.Dispose();

                    ddlcity.SelectedValue = (hdncity.Value.ToString());

                }
                else
                {
                    ddlcity.Items.Insert(0, new ListItem("--Select City--", ""));
                }
            }



            

            int index;
            index = e.Row.RowIndex;

            TextBox txtadd = (TextBox)e.Row.FindControl("txt_1b1");
            TextBox txtpin = (TextBox)e.Row.FindControl("txt_1b2");
            TextBox txtph = (TextBox)e.Row.FindControl("txt_1b5");
            // TextBox txtfax = (TextBox)e.Row.FindControl("txt_1b6");
            TextBox txteml = (TextBox)e.Row.FindControl("txt_1b7");

            txtadd.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            txtpin.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            ddl.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            ddlcity.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            txtph.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            //   txtfax.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            txteml.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);


            if (index > 0)
            {
                RequiredFieldValidator reqAdd = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator9");
                reqAdd.Enabled = false;

                RequiredFieldValidator reqZip = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator10");
                reqZip.Enabled = false;

                RequiredFieldValidator reqState = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator19");
                reqState.Enabled = false;

                RequiredFieldValidator reqCity = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator12");
                reqCity.Enabled = false;

                //RequiredFieldValidator reqPhone = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator13");
                //reqPhone.Enabled = false;

                //RequiredFieldValidator reqFax = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator14");
                //reqFax.Enabled = false;


            }

        }


    }

    // Pass Step2 to step  1 when click back button of new unit resigstration part 1 

    public DataTable Company_Fetch_Branch(int Companyid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("Gp_sp_BranchId_By_CompanyID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@companyid", SqlDbType.Int).Value = Companyid;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    public void filldataform()
    {
        // lblPANmsg.Visible = false;
        if (Request.QueryString["pcd"] != null)
        {

            trbranch1.Visible = true;
            trMainData.Visible = true;
            trMSMEDBSEARCH.Visible = false;

            //////   divQuery.Visible = false;


            DecryptEncrypt objDecEnc = new DecryptEncrypt();

            string prevPage = (objDecEnc.Decrypt(Request.QueryString["pcd"].Replace(" ", "+")).ToString());
            string prevPageCompanyId = (objDecEnc.Decrypt(Request.QueryString["cid"].Replace(" ", "+")).ToString());

            string prevPageConsId = (objDecEnc.Decrypt(Request.QueryString["con"].Replace(" ", "+")).ToString());

            ddlConstitution.Items.Clear();

            objCompany = new clsCompany();
            System.Data.DataTable Dtt = new System.Data.DataTable();
            Dtt = objCompany.Company_Fetch_Constitution();
            ddlConstitution.DataTextField = Dtt.Columns["fGPtbl_CUnit_Constitution_Name_POI"].ToString();
            ddlConstitution.DataValueField = Dtt.Columns["fGPtbl_CUnit_Constitution_Id"].ToString();
            ddlConstitution.DataSource = Dtt;
            ddlConstitution.DataBind();
            ddlConstitution.Items.Insert(0, new ListItem("--Select Constitution of the firm--", "0"));

            ddlConstitution.Items.FindByValue(prevPageConsId).Selected = true;




            // System.Threading.Thread.Sleep(5000);
            string fPANnoNew = "";
            string fPANno = "";
            string fUamNo = "";
            int ConstitutionId = 0;

            DataSet dss = new DataSet();
            clsCompany objComp = new clsCompany();
            dss = FetchCompanyPAN_By_CompanyId(Convert.ToInt32(prevPageCompanyId));

            fPANnoNew = dss.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
            fUamNo = dss.Tables[0].Rows[0]["fGtbl_Company_AdharNumber"].ToString();

            if (dss.Tables[0].Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString() != "")
            {
                ConstitutionId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString());
            }
            else
            {
                ConstitutionId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_ConstitutionId"].ToString());
            }
            //string ssiNo = "";
            fPANno = prevPage;
            txt_1.Text = prevPage.ToString();
            int fPANlength = 0;
            fPANlength = fPANno.Length;
            if (fPANlength != 10)
            {
                txt_1.Focus();
            }
            //ssiNo = txt_21.Text.ToString();

            objCompany = new clsCompany();
            //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);

            if (ConstitutionId != 4)
            {
                fStatus = objCompany.Company_PANnoCheck(fPANno);
            }
            else
            {
                fStatus = Company_PANnoCheck(fPANno, fUamNo);
            }
            Session["Status"] = fStatus.ToString();
            if (fStatus == 1)  //CONFIRM
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already confirmed";
            }
            else if (fStatus == 2)  //INPROCESS
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with INPROCESS status";
            }
            else if (fStatus == 3)  //WAITING
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with WAITING status";
            }
            else if (fStatus == 4)  //EXPIRED
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with EXPIRED status";
            }
            else if (fStatus == 5)  //INCOMPLETE
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = true;
                lblPANmsg.Text = "PAN already exists with INCOMPLETE status";
            }
            else if (fStatus == 123)  //NEW ENTRY
            {
                lblPANmsg.Visible = false;
                btnSubmit.Enabled = true;

                txt_1p1.Text = "";
                txt_1a1.Text = "";
                txt_1a2.Text = "";
                try
                {
                    ddlState_a3.SelectedValue = "";
                    ddlCity_a4.Items.Clear();
                }
                catch { }

                txt_1a5.Text = "";
                //  txt_1a6.Text = "";
                txt_1a7.Text = "";
                txt_mob.Text = "";

                try
                {
                    ddl_1d.SelectedValue = "";
                }
                catch { }

                try
                {
                    ddl_1f.SelectedValue = "";
                }
                catch { }

                txt_1c1.Text = "";
                txt_1c2.Text = "";
                txt_1c5.Text = "";
                // txt_1c6.Text = "";
                txt_1c7.Text = "";

                try
                {
                    ddlState_c3.SelectedValue = "";
                    ddlCity_c4.Items.Clear();
                }
                catch { }

                GridFactory.DataSource = objCompany.Fetch_Max_Factory();
                GridFactory.DataBind();

            }

            if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1)
            {
                objCompany = new clsCompany();
                System.Data.SqlClient.SqlDataReader MyDrGrid;

                if (ddlConstitution.SelectedValue != "4")
                {
                    MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);
                }
                else
                {
                    MyDrGrid = Company_Fetch_ByPANno_Step1(fPANno, fUamNo);
                }
                GridFactory.DataSource = MyDrGrid;
                GridFactory.DataBind();


                //DropDownListBranch.Enabled = false;
                //trbranch1.Visible = false;
                //trbranch2.Visible = false;
                //trbranch3.Visible = false;
                System.Data.SqlClient.SqlDataReader MyDr;

                if (ddlConstitution.SelectedValue != "4")
                {
                    MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);
                }
                else
                {
                    MyDr = Company_Fetch_ByPANno_Step1(fPANno, fUamNo);
                }


                DataTable Dttt = new DataTable();
                Dttt = Company_Fetch_Branch(Convert.ToInt32(prevPageCompanyId));


                int BranchId = 0;
                BranchId = Convert.ToInt32(Dttt.Rows[0]["fGPtbl_CUnit_BranchId"].ToString());
                DropDownListBranch.Items.FindByValue(BranchId.ToString()).Selected = true;



                while (MyDr.Read())
                {



                    //rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();
                    //   Response.Write(MyDr["fGPtbl_CUnit_Enterprise"].ToString());
                    CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                    UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                    hdnCompany.Value = CompanyId.ToString();
                    txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                    txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                    txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                    txt_proposal_date.Text = MyDr["fGPtbl_Company_ProposalDate"].ToString();
                    txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString();






                    //dhiraj
                    txtGstNumber.Text = MyDr["fGtbl_Company_GSTNumber"].ToString();

                    // dhiraj 19072017
                    string ss = MyDr["fGtbl_Company_IsSCST_Initiative"].ToString();
                    /*
                    if (MyDr["fGtbl_Company_IsSCST_Initiative"].ToString() == "1")
                    {
                        rdbSCSTInitiative.SelectedValue = "1";
                    }
                    else if (MyDr["fGtbl_Company_IsSCST_Initiative"].ToString() == "2")
                    {
                        rdbSCSTInitiative.SelectedValue = "2";
                    }

                    //rdbSCSTInitiative.SelectedValue = MyDr["fGtbl_Company_IsSCST_Initiative"].ToString() == "1" ? "1" : "2";

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

                    txt_1c1.Text = MyDr["fGPtbl_CUnitBranch_Address"].ToString();
                    txt_1c2.Text = MyDr["fGPtbl_CUnitBranch_Zip"].ToString();
                    txt_1c5.Text = MyDr["fGPtbl_CUnitBranch_Phone"].ToString();
                    //  txt_1c6.Text = MyDr["fGPtbl_CUnitBranch_Fax"].ToString();
                    txt_1c7.Text = MyDr["fGPtbl_CUnitBranch_Email"].ToString();
                    //   txt_1p1.Text = MyDr["fGPtbl_CUnit_Enterprise"].ToString();


                    try
                    {
                        ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                    }
                    catch { }

                    try
                    {
                        funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                        ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                    }
                    catch { }

                }
                MyDr.Close();
            }


        }

        else
        {

        }

    }



    public DataSet Company_DetailsBy_CompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Details_For_Unique_Code_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    public void CheckUniqueCode()
    {
        RestClient client = new RestClient("http://api.nsicnet.in/KYCAPI/api/KYC");
        RestRequest request = new RestRequest("login", Method.POST);
        request.AddHeader("Accept", "application/json");
        string bCode = MyDs.Tables[0].Rows[0]["fGtbl_MB_UniCode"].ToString();
        string strKYCNo = txt_1.Text.Trim(); //PAN
        string strKycDocName = "pan";
        string strLastName = txt_1p1.Text.Trim();// MyDs.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
        int SocialCategory = 0;
        SocialCategory = Convert.ToInt32(ddl_1e.SelectedValue);
        string strSocialCategory = "";// MyDs.Tables[0].Rows[0]["Enterprise_Social_Category"].ToString(); //"general";

        if (SocialCategory == 1)
        {
            strSocialCategory = "GENERAL";
        }
        else if (SocialCategory == 5)
        {
            strSocialCategory = "OBC";
        }
        else if (SocialCategory == 10)
        {
            strSocialCategory = "SC";
        }
        else if (SocialCategory == 11)
        {
            strSocialCategory = "ST";
        }

        int UnitSize = 0;
        UnitSize = Convert.ToInt32(ddl_1f.SelectedValue);
        string strEnterpriseCategory = "";
        if (UnitSize == 3)
        {
            strEnterpriseCategory = "Micro";
        }
        else if (UnitSize == 4)
        {
            strEnterpriseCategory = "Small";
        }

        // string strEnterpriseCategory = MyDs.Tables[0].Rows[0]["Category_Unit_Size"].ToString(); //"micro";
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
            // Company_Update_Unique_Code(intCompanyId, UniqueCode.ToString());
        }
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

    public SqlDataReader Company_Fetch_ByPANno_Step1_Grid(string PANno, string AadhaarNo)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Grid_Proprietorship", DBManager.ConnectToDB());
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


    public DataSet FetchCompanyPAN_By_CompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchCompanyPAN_By_CompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    //public int Company_PANnoCheck(string PANno, int branchid)
    public int Company_PANnoCheck(string PANno, string Uam)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANno_Proprietorship", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MySqlComm.Parameters.Add("@Uam", SqlDbType.Char, 50).Value = Uam;
            //MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return Status;
    }



    public int UpadteConstitution_Properitership(int CompanyId, int ConstitutionId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_Constitution_Properitership", DBManager.ConnectToDB());//GP_SP_insert_update_company_HSCode
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@ConstitutionId", SqlDbType.VarChar, 50).Value = ConstitutionId;
            ErrorId = MySqlComm.ExecuteNonQuery();
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
    }
    protected void rdbMsmeRegtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMsmeRegtype.SelectedValue == "1")
        {
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;
            RegularExpressionValidatorAAdhar.Enabled = true;
            // ValidatorCalloutExtender9.Enabled = true;

            RegularExpressionValidator1.Enabled = false;
            // ValidatorCalloutExtender12.Enabled = false;

            Requ_addhar.Enabled = true;
            //  Validator_aadhar_number.Enabled = true;
            RegularExpressionValidator3.Enabled = true;

            RequiredFieldValidator18.Enabled = false;

            RequiredFieldValidator14.Enabled = true;
            //  ValidatorCalloutExtender15.Enabled = false;

            divUAMPAN.Visible = true;
            divUAMPANMobileButton.Visible = true;
            divUAMPANMobileButton1.Visible = true;
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;
            RegularExpressionValidator3.Enabled = true;
            RegularExpressionValidator4.Enabled = false;

            trMainData.Visible = false;
        }
        else if (rdbMsmeRegtype.SelectedValue == "2")
        {
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
            divUAMPANMobileButton.Visible = true;
            divUAMPANMobileButton1.Visible = true;
            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;
            trMainData.Visible = false;
        }
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


    public int Company_AccountDetails(int CompanyId, string BankType, string BankName, string IFSCCOde, string BranchName, string BranchAddress, string AccountType, string AccountNumber, string AccountHolderName)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_UpdateCompany_AccountDetails", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_BankType", SqlDbType.VarChar).Value = BankType;
            MySqlComm.Parameters.Add("@fGPtbl_BankName", SqlDbType.VarChar).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_IFSCCOde", SqlDbType.VarChar).Value = IFSCCOde;
            MySqlComm.Parameters.Add("@fGPtbl_BranchName", SqlDbType.VarChar).Value = BranchName;
            MySqlComm.Parameters.Add("@fGPtbl_BranchAddress", SqlDbType.VarChar).Value = BranchAddress;
            MySqlComm.Parameters.Add("@fGPtbl_AccountType", SqlDbType.VarChar).Value = AccountType;
            MySqlComm.Parameters.Add("@fGPtbl_AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
            MySqlComm.Parameters.Add("@fGPtbl_AccountHolderName", SqlDbType.VarChar).Value = AccountHolderName;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return ErrorId;
    }
    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ddlBankType.SelectedValue != "0")
        {
            ds = Fetch_Bank(ddlBankType.SelectedItem.Text);
            ddlBankName.DataSource = ds;
            ddlBankName.DataTextField = "BankName";// ds.Tables[0].Rows[0]["BankName"].ToString();  
            ddlBankName.DataValueField = "BankID";// ds.Tables[0].Rows[0]["BankID"].ToString();             
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("--Select Bank--", "0"));

        }
    }
}