using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

using System.Web.UI.HtmlControls;

public partial class appAdministrator_Online_NewUnitregistration_paymentdetails_step7 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailsId, FormTypeID, fGPtbl_PD_BranchConfirmation, fGPtbl_PD_BranchConfirmation_inspection;

    double AmountPayable;
    int UserId, RoleId, DepartmentId;
    string PaymentDetailsIdstring;
    string UserFullName;
    double AgencyAmount = 0.0;
    clsCompany objCompany;
    int PaymentTypeInspection;
    int ErrorId;
    string ReferenceNo;
    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";
    string NewFileName_inspection = "";
    string filePath_inspection;
    string OriginalFilePath_inspection;
    int FileStatus_1_inspection;
    double AmmountPaybale_all = 0.0;
    double Servictax_all = 0.0;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int CategoryCastId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        FunRequest();
        FunFillData();
        if (FormTypeID != 3)
        {
            OriginalFilePath = "./Document/Upload/Registration/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            lbl_registration_amedment.Text = "Registration";
            lbl_registration_fees_text.Text = "Registration";
            lbl_payment_details_nsic.Text = "";
            lbl_select_payment.Text = "for Registration Fees";
        }
        else
        {
            OriginalFilePath = "./Document/Upload/Amendment/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Amendment/";
            lbl_registration_amedment.Text = "Renewal / Amendment";
            lbl_registration_fees_text.Text = "Renewal / Amendment";
            lbl_payment_details_nsic.Text = "";
            lbl_select_payment.Text = "for Renewal / Amendment fee";
        }


        if (!Page.IsPostBack)
        {
            DataSet dss = new DataSet();
            int AgencyId = 0;
            dss = DataSetAgency_Details_ByUnitId(UnitId);
            AgencyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_PD_AgencyId"].ToString());

            // AgencyId = 10;
            // 8 MSMEDI, 9 RITES, 10 CDC
            if (AgencyId == 9)
            {
                rdl_inspectionmode_payment.Items.Clear();
                rdl_inspectionmode_payment.Visible = true;
                ListItem item;
                item = new ListItem("DD", "2");
                rdl_inspectionmode_payment.Items.Add(item);

                item = new ListItem("Cheque", "3");
                rdl_inspectionmode_payment.Items.Add(item);

                item = new ListItem("Fund Transfer", "4");
                rdl_inspectionmode_payment.Items.Add(item);


                rdl_inspectionmode_payment.Items.FindByValue("2").Selected = true;

                PanelInspectionOnlieRites.Visible = false;
                PanelChequeDD.Visible = true;
                //PanelChequeDD.Visible = true;

            }
            else
            {
                rdl_inspectionmode_payment.Items.Clear();
                rdl_inspectionmode_payment.Visible = true;
                ListItem item;
                item = new ListItem("DD", "2");
                rdl_inspectionmode_payment.Items.Add(item);

                item = new ListItem("Cheque", "3");
                rdl_inspectionmode_payment.Items.Add(item);

                item = new ListItem("Fund Transfer", "4");
                rdl_inspectionmode_payment.Items.Add(item);


                rdl_inspectionmode_payment.Items.FindByValue("4").Selected = true;
                PanelInspectionOnlieRites.Visible = true;
                PanelChequeDD.Visible = false;

                //PanelChequeDD.Visible = true;

                //pnlChqDD.Visible = true;

            }
        }


        #region Bind Bank based on Branch

        if (!Page.IsPostBack)
        {
            CFSSService obj = new CFSSService();
            string cfssBranchCode = obj.GetCFSSBranchCode(BranchId);

            //  List<CFSSService.BankDetails> banks = obj.GetBankList(cfssBranchCode);
            List<CFSSService.BankDetails> banks = obj.GetBankList("2562");
            Session["BankList"] = banks;

            ddlBank.DataSource = banks.Select(x => new
            {
                LedgerID = x.LedgerID,
                BankAccount_Name = x.BankAccountNo + " - " + x.Bank_Name
            }).ToList();

            ddlBank.DataTextField = "BankAccount_Name";
            ddlBank.DataValueField = "LedgerID";
            ddlBank.DataBind();

            ddlBank.Items.Insert(0, new ListItem("--Select Bank--", "0"));
        }
        #endregion

    }

    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<CFSSService.BankDetails> bankList = (List<CFSSService.BankDetails>)Session["BankList"];

        string ledgerID = ddlBank.SelectedValue;

        var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

        if (selectedBank != null)
        {
            Session["SelectedBank"] = selectedBank;

            List<CFSSService.BankDetails> selectedList = new List<CFSSService.BankDetails>();
            selectedList.Add(selectedBank);
            GridView1.DataSource = selectedList;
            GridView1.DataBind();
        }
    }
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            PaymentDetailsIdstring = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());

            //PaymentDetailsIdstring = "111622,111623";
            FormTypeID = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());

            //PaymentDetailsId = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());

        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }

    private void FunFillData()
    {
        ////////if (Status == 5 || Status == 123 || Status == 3)    //INCOMPLETE
        ////////{
        //UNIT TABLE



        int PaymentType = 0;
        string[] strArr = null;
        int count = 0;
        int Unitamnt = 0, Agncyamnt = 0;
        double servicetax = 0.0, AgencyServiceTax = 0.0;
        char[] splitchar = { ',' };
        strArr = PaymentDetailsIdstring.Split(splitchar);
        for (count = 0; count <= (strArr.Length - 1); count++)
        {
            PaymentDetailsId = Convert.ToInt32(strArr[count]);
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDr;
            MyDr = objCompany.Company_Fetch_ByUnit_Step7(PaymentDetailsId);
            while (MyDr.Read())
            {


                #region If leadger Id is already Present
                string ledger_ID = MyDr["LedgerID"] == DBNull.Value ? "" : MyDr["LedgerID"].ToString();

                if (!string.IsNullOrEmpty(ledger_ID))
                {

                    DataSet dsbank = new DataSet();
                    CFSSService obj = new CFSSService();


                    string cfssBranchCode = obj.GetCFSSBranchCode(BranchId);
                    obj.BindBankDropDown(ddlBank, "2562");


                    //  List<CFSSService.BankDetails> banks = obj.GetBankList(cfssBranchCode);
                    //////List<CFSSService.BankDetails> banks = obj.GetBankList("2562");
                    //////Session["BankList"] = banks;
                    //////ddlBank.DataSource = banks.Select(x => new
                    //////{
                    //////    LedgerID = x.LedgerID,
                    //////    BankAccount_Name = x.BankAccountNo + " - " + x.Bank_Name
                    //////}).ToList();
                    //////ddlBank.DataTextField = "BankAccount_Name";
                    //////ddlBank.DataValueField = "LedgerID";
                    //////ddlBank.DataBind();
                    //////ddlBank.Items.Insert(0, new ListItem("--Select Bank--", "0"));


                    dsbank = obj.FetchBankDetails_CFSS(PaymentDetailsId);

                    if (ddlBank.Items.FindByValue(ledger_ID) != null)
                    {
                        ddlBank.SelectedValue = ledger_ID.Trim();
                        ddlBank.Enabled = false;
                    }
                    GridView1.DataSource = dsbank;
                    GridView1.DataBind();
                }

                #endregion



                if (count == 0)
                {
                    //lblUnitSlabName.Text = MyDr["fGPtbl_PD_UnitSlabName"].ToString();
                    //lblUnitInstruction.Text = MyDr["fGPtbl_PD_UnitSlabInstruction"].ToString();
                    lblUnitAmnt.Text = "Rs." + Convert.ToDecimal(MyDr["fGPtbl_PD_UnitAmount"].ToString()).ToString("0.00");
                    Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());
                }
                if (count == 0)
                {
                    //lblAgencySlabName.Text = MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                }
                else
                {
                    //  lblAgencySlabName.Text = lblAgencySlabName.Text + " , " + MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                }

                //lblAgencySlabInst.Text = MyDr["fGPtbl_PD_AgencySlabInstruction"].ToString();





                Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
                //  servicetax = servicetax + Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());
                Servictax_all = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

                AgencyServiceTax = AgencyServiceTax + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

                servicetax = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString());// +Convert.ToDouble(AgencyServiceTax);

                lblTotAmnt.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())))));
                AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));
                AgencyAmount = (Convert.ToDouble(Agncyamnt));



                #region CFSS Code


                if (MyDr["fGPtbl_PD_PaymentType"].ToString() != "")
                {
                    PaymentType = Convert.ToInt32(MyDr["fGPtbl_PD_PaymentType"].ToString());
                    radPay.SelectedValue = PaymentType.ToString();
                }
                if (PaymentType == 3 || PaymentType == 2)
                {
                    pnlChqDD.Visible = true;
                    pnlFundTransfer.Visible = false;
                    pnlOrtgs.Visible = false;
                    if (MyDr["fGPtbl_PD_ChequeDDNo"].ToString() != "")
                    {
                        txt_chqddNo.Text = MyDr["fGPtbl_PD_ChequeDDNo"].ToString();
                        txt_chqddNo.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_BankName"].ToString() != "")
                    {
                        txt_BankName.Text = MyDr["fGPtbl_PD_BankName"].ToString();
                        txt_BankName.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_BankBranch"].ToString() != "")
                    {
                        txt_BankBranch.Text = MyDr["fGPtbl_PD_BankBranch"].ToString();
                        txt_BankBranch.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_PaybalAt"].ToString() != "")
                    {
                        txt_PayableAt.Text = MyDr["fGPtbl_PD_PaybalAt"].ToString();
                        txt_PayableAt.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_PaybalAt"].ToString() != "")
                    {
                        txt_PayableAt.Text = MyDr["fGPtbl_PD_PaybalAt"].ToString();
                        txt_PayableAt.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_UnitAmount"].ToString() != "")
                    {
                        txt_chqddAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        txt_chqddAmnt.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString() != "")
                    {
                        txt_chqddate.Text = MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString();
                        txt_chqddate.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_PaymentRemarks"].ToString() != "")
                    {
                        txtChequeDDPaymentRemarks.Text = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();
                        txtChequeDDPaymentRemarks.Enabled = false;
                    }

                }
                else if (PaymentType == 4)
                {
                    pnlChqDD.Visible = false;
                    pnlFundTransfer.Visible = true;
                    pnlOrtgs.Visible = true;
                    if (MyDr["fGPtbl_PD_Utr_number"].ToString() != "")
                    {
                        txt_utr_no.Text = MyDr["fGPtbl_PD_Utr_number"].ToString();
                        txt_utr_no.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_BankName"].ToString() != "")
                    {
                        txt_bank_name.Text = MyDr["fGPtbl_PD_BankName"].ToString();
                        txt_bank_name.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_BankBranch"].ToString() != "")
                    {
                        txt_branch_name.Text = MyDr["fGPtbl_PD_BankBranch"].ToString();
                        txt_branch_name.Enabled = false;
                    }

                    if (MyDr["fGPtbl_PD_UnitAmount"].ToString() != "")
                    {
                        txt_amount.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        txt_amount.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString() != "")
                    {
                        txt_date_utr.Text = MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString();
                        txt_date_utr.Enabled = false;
                    }
                    if (MyDr["fGPtbl_PD_PaymentRemarks"].ToString() != "")
                    {
                        txtNeftPaymentRemarks.Text = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();
                        txtNeftPaymentRemarks.Enabled = false;
                    }
                }
                else
                {
                    pnlChqDD.Visible = true;


                }




                #endregion

            }
            MyDr.Close();
        }
        // lblAgencyAmnt.Text = Convert.ToString(Agncyamnt);
        // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
        //lblSubTot.Text = (Unitamnt + Agncyamnt).ToString();
        // (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
        //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));



        DataSet dsf = new DataSet();
        dsf = Fetch_ChangesType_Renewal_Amendment_Online(CompanyId);
        int InspectionfeeStatus = 0;
        InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

        string formType = InspectionfeeStatus == 240 ? "3" : "2";


        if (InspectionfeeStatus == 240)
        {
            tdInspectionAgencyFee.Visible = true;
            lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
            lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

            txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total



            //AgencyAmount = 0;
            //AgencyServiceTax = 0;
            //Agncyamnt = 0;
            lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
            lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

            txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total



        }
        else
        {
            tdInspectionAgencyFee.Visible = false;

            //AgencyAmount = 0;
            //AgencyServiceTax = 0;
            //Agncyamnt = 0;
            lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
            lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

            txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total


        }

        clsCompany clsObjN = new clsCompany();
        DataSet ds = new DataSet();


        ds = CategoryCast_Details_ByCompanyId(CompanyId);


        CategoryCastId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryCastId"]);





        if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
        {
            // tbInspectionAgencyPaymentDetails.Visible = false;

            // lblAgencyAmnt.Text = "Rs." + "0.00";
            //lblTotAmnt.Text = "Rs." + "0.00";

            // AmountPayable =Convert.ToDouble("0.00");
            // lblServiceTax.Text = "Rs." + "0.00";
            //  lbl_service_tax.Text = "Rs." + "0.00";
            // lbl_total_amount_payable.Text = "Rs." + "0.00";
            // txt_DD_Inspection.Text = "0.00";
            AmmountPaybale_all = Convert.ToDouble("0.00");
            //txt_amount.Text = "Rs." + "0.00";


            Double unitamount_SC_ST = 100.0;
            Double Servicetax_SC_ST = 18.0;
            lblUnitAmnt.Text = "Rs." + "100.00";
            lblServiceTax.Text = "Rs." + (Math.Round(Convert.ToDouble(Servicetax_SC_ST), 2)).ToString("0.00"); // "Rs." + "18.00";

            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00"); // Registration Fee

            txt_amount.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");

            txt_chqddAmnt.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");




        }
        else
        {
            if (PaymentType == 3 || PaymentType == 2)
            {

                txt_chqddAmnt.Text = AmountPayable.ToString();
            }
            else if (PaymentType == 4)
            {
                txt_amount.Text = AmountPayable.ToString();
            }
        }


        //CategoryCastId = 5;
        if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
        {

            int result = SCST_IsGracePeriodOver(CompanyId, formType);

            // tbInspectionAgencyPaymentDetails.Visible = false;
            if (result == 0)
            {


                // tbInspectionAgencyPaymentDetails.Visible = false;

                lblAgencyAmnt.Text = "Rs." + "0.00";
                //lblTotAmnt.Text = "Rs." + "0.00";

                // AmountPayable =Convert.ToDouble("0.00");
                // lblServiceTax.Text = "Rs." + "0.00";
                lbl_service_tax.Text = "Rs." + "0.00";
                lbl_total_amount_payable.Text = "Rs." + "0.00";
                txt_DD_Inspection.Text = "0.00";
                AmmountPaybale_all = Convert.ToDouble("0.00");
                //txt_amount.Text = "Rs." + "0.00";


                Double unitamount_SC_ST = 100.0;
                Double Servicetax_SC_ST = 18.0;
                lblUnitAmnt.Text = "Rs." + "100.00";
                lblServiceTax.Text = "Rs." + (Math.Round(Convert.ToDouble(Servicetax_SC_ST), 2)).ToString("0.00"); // "Rs." + "18.00";

                lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00"); // Registration Fee

                txt_amount.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");
            }

            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
            RequiredFieldValidator8.Enabled = false;
            //  CValidatortxt_cheq1.Enabled = false;
            RequiredFieldValidator9454.Enabled = false;

            RequiredFieldValidatorrdbInspectionPaymentType_Online.Enabled = false;
            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator11.Enabled = false;
            RequiredFieldValidator12.Enabled = false;
            RequiredFieldValidator13.Enabled = false;
            // RegularExpressionValidator3.Enabled = false;

            txtUTRNO_InspectionAgency.Enabled = false;
            txtBankName_InspectionAgency.Enabled = false;
            txtBranchName_InspectionAgency.Enabled = false;
            txtDate_InspectionAgency.Enabled = false;
            txtAmount_InspectionAgency.Enabled = false;

            txt_chqddNo_inspection.Enabled = false;
            txt_BankNam_inspection.Enabled = false;
            txt_BankBranch_inspection.Enabled = false;
            txt_PayableAt_inspection.Enabled = false;
            txt_DD_Inspection.Enabled = false;
            // txt_chqddate_inspection.Enabled = false;
            FileUpload2.Enabled = false;

        }
        else
        {
            RequiredFieldValidator5.Enabled = true;
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
            RequiredFieldValidator8.Enabled = true;
            //  CValidatortxt_cheq1.Enabled = true;

            RequiredFieldValidator9454.Enabled = true;

            RequiredFieldValidatorrdbInspectionPaymentType_Online.Enabled = true;
            RequiredFieldValidator10.Enabled = true;
            RequiredFieldValidator11.Enabled = true;
            RequiredFieldValidator12.Enabled = true;
            RequiredFieldValidator13.Enabled = true;
            //  RegularExpressionValidator3.Enabled = true;

            txtUTRNO_InspectionAgency.Enabled = true;
            txtBankName_InspectionAgency.Enabled = true;
            txtBranchName_InspectionAgency.Enabled = true;
            // txtDate_InspectionAgency.Enabled = true;
            txtAmount_InspectionAgency.Enabled = true;

            txt_chqddNo_inspection.Enabled = true;
            txt_BankNam_inspection.Enabled = true;
            txt_BankBranch_inspection.Enabled = true;
            txt_PayableAt_inspection.Enabled = true;
            txt_DD_Inspection.Enabled = true;
            // txt_chqddate_inspection.Enabled = true;
            FileUpload2.Enabled = true;
        }
        //////}
    }

    public int SCST_IsGracePeriodOver(int CompanyId, string FormType)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        DataSet MyDs = new DataSet();
        int result = 2;
        try
        {
            MySqlComm = new SqlCommand("GPsp_SCST_IsGracePeriodOver", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@FormType", SqlDbType.VarChar, 1500).Value = FormType;
            SqlParameter resultValue = new SqlParameter("@result", SqlDbType.Int);
            resultValue.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(resultValue);
            MySqlComm.ExecuteNonQuery();
            result = Convert.ToInt32(resultValue.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();

        }

        return result;
    }


    public DataSet CategoryCast_Details_ByCompanyId(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_CategoryCast_Details_ByCompanyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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



    public DataSet ProposalDate_ByCompanyId(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_ProposalDate_ByCompanyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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




    public DataSet Fetch_ChangesType_Renewal_Amendment_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_ChangesType_Renewal_Amendment_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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


    #region RITES ONLINE PAYMENT
    public DataSet DataSetAgency_Details_ByUnitId(int UnitId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Insert_Step7_Fetch_AgencyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
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

    protected void rbl_selectindexchanged_InspectionAgency(object sender, EventArgs e)
    {
        //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
        if (Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 2 || Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 3)
        {
            PanelChequeDD.Visible = true;
            PanelInspectionOnlieRites.Visible = false;
        }
        else if (Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 4)
        {
            PanelInspectionOnlieRites.Visible = true;
            PanelChequeDD.Visible = false;
        }

    }



    protected void rdbInspectionPaymentType_Online_selectindexchanged(object sender, EventArgs e)
    {

        int AgencyId = 0;
        DataSet dss = DataSetAgency_Details_ByUnitId(UnitId);
        AgencyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_PD_AgencyId"].ToString());

        //  AgencyId = 10;

        if (AgencyId == 9)
        {
            HyperLink1.Visible = true;
            //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
            if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 1)
            {
                lbl_OnlineMsg.Visible = true;

                lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Online_Payment_Form.aspx";

                HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
                HyperLink1.Target = "_blank";

                lblOnlineNorthernMsg.Visible = true;
                lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";

            }
            else if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 2)
            {
                lbl_OnlineMsg.Visible = true;


                lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Bank%20Details%20NR.PDF";

                HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
                HyperLink1.Target = "_blank";

                lblOnlineNorthernMsg.Visible = false;
                // lblOnlineNorthernMsg.Text = "Other region can use only NEFT/RTGS option for payment";

            }
        }

        else
        {
            HyperLink1.Text = ""; lbl_OnlineMsg.Text.ToString();
            HyperLink1.Target = "_blank";

            HyperLink1.Visible = false;

            lblOnlineNorthernMsg.Visible = true;
            lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";
            lblOnlineNorthernMsg.Visible = false;
        }

    }

    #endregion




    protected void rbl_selectindexchanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
        {
            pnlCash.Visible = true;
            pnlChqDD.Visible = false;
            pnlFundTransfer.Visible = false;
            if (RoleId == 8)
            {
                receipt.Visible = true;

            }
            else
            {
                receipt.Visible = false;
            }
            Session["PaymentType"] = "1";


            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));
            }
            // lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));


        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            pnlChqDD.Visible = true;

            pnlCash.Visible = false;
            Session["PaymentType"] = "2";

            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }
            //txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));


            pnlChqDD.Visible = true;
            pnlFundTransfer.Visible = false;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            Session["PaymentType"] = "3";

            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }

            //txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));

            pnlChqDD.Visible = true;
            pnlFundTransfer.Visible = false;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {
            pnlFundTransfer.Visible = true;
            Session["PaymentType"] = "4";
            pnlChqDD.Visible = false;
            pnlCash.Visible = false;
        }
    }

    public string Company_Insert_Step7_Modified(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType, double @fGPtbl_PD_PaidAmount,
       string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName, string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate,
       string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath, string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,
        int PaymentTypeInspection, string PaymentDD_Inspection, string BankeName_Inspection, string Branch_Name_Inspection,
         string paymentDD_date_inspection, string Payableat_inspection,
        string Paymetfilename_inspection, string Filepath_inspection,
         int Branch_confirmation_inspection, int Fundtransfer_type, string utrnumber, int fGPtbl_PD_PaymentType_RITES_Type_inspection, string PaymentRemarks,

          string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername


         )
    {
        string Returnvalue = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step7_Modified", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

            if (fGPtbl_PD_ChequeDDNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
            }
            if (fGPtbl_PD_BankName != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
            }
            if (fGPtbl_PD_BankBranch != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
            }
            if (fGPtbl_PD_PaybalAt != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
            }
            if (fGPtbl_PD_ChequeDDate != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
            }
            if (fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
            }
            if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
            }
            if (fGPtbl_PD_CashReceiptNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            }

            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
            MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
            // if (PaymentTypeInspection != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_inspection", SqlDbType.Int).Value = PaymentTypeInspection;
            // if (PaymentDD_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_inspection", SqlDbType.VarChar, 50).Value = PaymentDD_Inspection;
            // if (BankeName_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankName_inspection", SqlDbType.VarChar, 500).Value = BankeName_Inspection;
            // if (Branch_Name_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch_inspection", SqlDbType.VarChar, 500).Value = Branch_Name_Inspection;
            // // MySqlComm.Parameters.Add("@fGPtbl_PD_Date_inspection", SqlDbType.DateTime).Value = PaymentDate_inspection;
            // if (paymentDD_date_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Cheque_DD_Date_inspection", SqlDbType.DateTime).Value = paymentDD_date_inspection;
            //  if (Payableat_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt_inspection", SqlDbType.VarChar, 500).Value = Payableat_inspection;
            //  if (Paymetfilename_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filename_inspection", SqlDbType.VarChar, 200).Value = Paymetfilename_inspection;
            //if (Filepath_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath_inspection", SqlDbType.VarChar, 500).Value = Filepath_inspection;
            // if (Branch_confirmation_inspection != null)
            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation_inspection", SqlDbType.Int).Value = Branch_confirmation_inspection;
            //  if (Fundtransfer_type != null)
            MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = Fundtransfer_type;
            // if (utrnumber != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 200).Value = utrnumber;
            //   if (fGPtbl_PD_PaymentType_RITES_Type_inspection != null)
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_RITES_Type_inspection", SqlDbType.Int).Value = fGPtbl_PD_PaymentType_RITES_Type_inspection;
            //  if (PaymentRemarks != null) 
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 500).Value = PaymentRemarks;


            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;



            SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            Returnvalue = outError.Value.ToString();

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
        return Returnvalue;
    }


    public string Company_CompanyAmdment_Step7_Modified(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType,
     double @fGPtbl_PD_PaidAmount, string fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName,
     string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate, string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath,
     string fGPtbl_PD_CashReceiptNo, int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId,
     int PaymentTypeInspection, string PaymentDD_Inspection, string BankeName_Inspection, string Branch_Name_Inspection,
          string paymentDD_date_inspection, string Payableat_inspection,
         string Paymetfilename_inspection, string Filepath_inspection,
          int Branch_confirmation_inspection, int Fundtransfertype, string UtrNumber, string PaymentRemarks,

          string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername



         )
    {
        string Returnvalue = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step7_Modified", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

            if (fGPtbl_PD_ChequeDDNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
            }

            if (fGPtbl_PD_ChequeDDate != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
            }

            if (fGPtbl_PD_BankName != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankName", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankName;
            }

            if (fGPtbl_PD_BankBranch != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch", SqlDbType.VarChar, 500).Value = fGPtbl_PD_BankBranch;
            }

            if (fGPtbl_PD_PaybalAt != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt", SqlDbType.VarChar, 500).Value = fGPtbl_PD_PaybalAt;
            }

            if (fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filename", SqlDbType.VarChar, 200).Value = fGPtbl_PD_Filename;
            }

            if (fGPtbl_PD_Filepath != "" && fGPtbl_PD_Filename != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
            }

            if (fGPtbl_PD_CashReceiptNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            }

            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;


            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
            MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;


            //if (PaymentTypeInspection != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType_inspection", SqlDbType.Int).Value = PaymentTypeInspection;
            // if (PaymentDD_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo_inspection", SqlDbType.VarChar, 50).Value = PaymentDD_Inspection;
            // if (BankeName_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankName_inspection", SqlDbType.VarChar, 500).Value = BankeName_Inspection;
            //  if (Branch_Name_Inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_BankBranch_inspection", SqlDbType.VarChar, 500).Value = Branch_Name_Inspection;
            // //  MySqlComm.Parameters.Add("@fGPtbl_PD_Date_inspection", SqlDbType.DateTime).Value = PaymentDate_inspection;
            //  if (paymentDD_date_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Cheque_DD_Date_inspection", SqlDbType.DateTime).Value = paymentDD_date_inspection;
            // if (Payableat_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaybalAt_inspection", SqlDbType.VarChar, 500).Value = Payableat_inspection;
            //if (Paymetfilename_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filename_inspection", SqlDbType.VarChar, 200).Value = Paymetfilename_inspection;
            // if (Filepath_inspection != "") 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath_inspection", SqlDbType.VarChar, 500).Value = Filepath_inspection;
            // if (Branch_confirmation_inspection != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation_inspection", SqlDbType.Int).Value = Branch_confirmation_inspection;
            //  if (Fundtransfertype != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = Fundtransfertype;
            //  if (UtrNumber != null) 
            MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 200).Value = UtrNumber;

            // if (PaymentRemarks != null)
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 500).Value = PaymentRemarks;

            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;


            SqlParameter outError = new SqlParameter("@Referenece", SqlDbType.VarChar, 500);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();

            Returnvalue = outError.Value.ToString();
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
        return Returnvalue;

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //DataSet dsPdate = new DataSet();
        //DateTime proposaldate;
        //dsPdate = ProposalDate_ByCompanyId(CompanyId);
        //proposaldate= Convert.ToDateTime(dsPdate.Tables[0].Rows[0]["fGPtbl_Company_ProposalDate"]);

        //DateTime dt1_ProposalDate = proposaldate;
        //DateTime dt2_ChkDate = Convert.ToDateTime(txt_chqddate.Text.Trim()).Date;

        //////If Cheque date is less than proposal date Check
        ////if (dt2_ChkDate  >= dt1_ProposalDate)
        ////{
        ////    lblDateMsg.Text = "";


        List<CFSSService.BankDetails> bankList = (List<CFSSService.BankDetails>)Session["BankList"];

        string ledgerID = ddlBank.SelectedValue;

        var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

        if (selectedBank != null)
        {
            Session["SelectedBank"] = selectedBank;
        }


        string Bank_BranchID = "";
        string Bank_AccountNo = "";
        string Bank_BankName = "";
        string Bank_IFSC = "";
        string Bank_Branchname = "";
        string Bank_LedgerId = "";
        string Bank_Ledgername = "";

        CFSSService.BankDetails bank = (CFSSService.BankDetails)Session["SelectedBank"];
        Bank_BranchID = bank.BranchID;
        Bank_AccountNo = bank.BankAccountNo;
        Bank_BankName = bank.Bank_Name;
        Bank_IFSC = bank.Bank_IFSC_Code;
        Bank_Branchname = bank.BranchName;
        Bank_LedgerId = bank.LedgerID;
        Bank_Ledgername = bank.Ledger_Name;




        try
        {
            objCompany = new clsCompany();
            int PaymentType;
            string PaybalAt, BankName, BranchName, ChqDdNo, ChqDdate, CashReceiptNo, Utr_Number;
            int Fund_Transfer_type;
            PaybalAt = "";

            BankName = "";
            BranchName = "";
            ChqDdNo = "";
            CashReceiptNo = "";
            CashReceiptNo = "";
            Utr_Number = "";
            Fund_Transfer_type = 0;

            string PaymentRemarks = "";

            if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 1)
            {
                pnlChqDD.Visible = false;
                pnlCash.Visible = false;
                Session["PaymentType_fundtransfer"] = "1";
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
                pnlChqDD.Visible = false;
                pnlFundTransfer.Visible = true;

            }

            else if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 2)
            {
                pnlChqDD.Visible = false;
                pnlCash.Visible = false;
                Session["PaymentType_fundtransfer"] = "2";
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
                pnlChqDD.Visible = false;
                pnlFundTransfer.Visible = true;

            }

            // if (Convert.ToInt32(Session["PaymentType"].ToString()) == 4)
            if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
            {
                BankName = txt_bank_name.Text.ToString();
                BranchName = txt_branch_name.Text.ToString();
                ChqDdate = txt_date_utr.Text.ToString();
                // AmountPayable 
                Fund_Transfer_type = Convert.ToInt32(Session["PaymentType_fundtransfer"].ToString());
                Utr_Number = txt_utr_no.Text.ToString();

                PaymentRemarks = txtNeftPaymentRemarks.Text.ToString();

            }

            else
            {
                BankName = txt_BankName.Text;
                BranchName = txt_BankBranch.Text;
                ChqDdate = txt_chqddate.Text.Trim();

                PaymentRemarks = txtChequeDDPaymentRemarks.Text.ToString();
            }


            ChqDdNo = txt_chqddNo.Text;
            PaybalAt = txt_PayableAt.Text;

            // ChqDdate = "07/25/2012";
            PaymentType = Convert.ToInt32(radPay.SelectedItem.Value.ToString());// Convert.ToInt32(Session["PaymentType"].ToString());

            int PaymentType_Inspection_Online = 0;


            if (rdbInspectionPaymentType_Online.SelectedIndex != -1)
            {

                if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 1)
                {
                    // Session["PaymentType_Inspection_Online"] = "1"; //Online Payment Gateway
                    PaymentType_Inspection_Online = 1;
                }

                else if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 2)
                {
                    //Session["PaymentType_Inspection_Online"] = "2"; //RTGS/NEFT
                    PaymentType_Inspection_Online = 2;
                }
                else
                {
                    PaymentType_Inspection_Online = 0;
                }

            }



            if (rdl_inspectionmode_payment.SelectedValue == "2")
            {
                PaymentTypeInspection = 2; //INSPETION FEE DD
            }
            else if (rdl_inspectionmode_payment.SelectedValue == "3")
            {
                PaymentTypeInspection = 3; //INSPETION FEE CHEQUE
            }
            else if (rdl_inspectionmode_payment.SelectedValue == "4")
            {
                PaymentTypeInspection = 4; //INSPETION FEE ONLINE
            }



            string PaybalAt_Inspection, BankName_Inspection, BranchName_Inspection, ChqDdNo_Inspection, ChqDdate_Inspection, ChqDDAmount_Inspection;
            PaybalAt_Inspection = "";
            BankName_Inspection = "";
            BranchName_Inspection = "";

            ChqDdNo_Inspection = "";
            ChqDdate_Inspection = "";
            ChqDDAmount_Inspection = "";


            //Inspection Agency RITES ONLINE PAYMENT DETAILS DHIRAJ 26032018
            if (PaymentTypeInspection == 4) // For Fund Transfer
            {
                ChqDdNo_Inspection = txtUTRNO_InspectionAgency.Text.ToString();
                BankName_Inspection = txtBankName_InspectionAgency.Text;
                BranchName_Inspection = txtBranchName_InspectionAgency.Text;
                ChqDdate_Inspection = txtDate_InspectionAgency.Text.Trim();
            }

            else // For Cheque/DD
            {
                PaybalAt_Inspection = txt_PayableAt_inspection.Text;
                BankName_Inspection = txt_BankNam_inspection.Text;
                BranchName_Inspection = txt_BankBranch_inspection.Text;
                ChqDdNo_Inspection = txt_chqddNo_inspection.Text;
                ChqDdate_Inspection = txt_chqddate_inspection.Text.Trim();
                ChqDDAmount_Inspection = txt_DD_Inspection.Text.ToString();
                //BankName = txt_BankName.Text;
                //BranchName = txt_BankBranch.Text;
                //ChqDdate = txt_chqddate.Text.Trim();
            }


            ////PaybalAt_Inspection = txt_PayableAt_inspection.Text;
            ////BankName_Inspection = txt_BankNam_inspection.Text;
            ////BranchName_Inspection = txt_BankBranch_inspection.Text;
            ////ChqDdNo_Inspection = txt_chqddNo_inspection.Text;
            ////ChqDdate_Inspection = txt_chqddate_inspection.Text.Trim();
            ////ChqDDAmount_Inspection = txt_DD_Inspection.Text.ToString();





            if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
            {
                CashReceiptNo = txtReceipt.Text.ToString();
            }
            else
            {
                CashReceiptNo = "";
            }




            bool TF = FunUploadFile_1();
            bool TF_Inspection = FunUploadFile_2();

            if (TF)
            {

                if (FormTypeID != 3)
                {

                    if (RoleId == 8 || RoleId == 0)
                    {
                        if (RoleId == 0)
                        {
                            fGPtbl_PD_BranchConfirmation = 0;

                        }

                        else
                        {
                            fGPtbl_PD_BranchConfirmation = 1;
                        }
                        /* ReferenceNo = objCompany.Company_Insert_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                             AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                             CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId);*/
                        ////ReferenceNo = objCompany.Company_Insert_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                        ////    AmmountPaybale_all, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                        ////    CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                        ////    PaymentTypeInspection,
                        ////    ChqDdNo_Inspection, BankName_Inspection,
                        ////    BranchName_Inspection, ChqDdate_Inspection,
                        ////    PaybalAt_Inspection,
                        ////    NewFileName_inspection,
                        ////    OriginalFilePath,
                        ////    fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number,

                        ////   PaymentType_Inspection_Online, PaymentRemarks
                        ////    );
                        ///


                        ReferenceNo = Company_Insert_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                           AmmountPaybale_all, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                           CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                           PaymentTypeInspection,
                           ChqDdNo_Inspection, BankName_Inspection,
                           BranchName_Inspection, ChqDdate_Inspection,
                           PaybalAt_Inspection,
                           NewFileName_inspection,
                           OriginalFilePath,
                           fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number,

                          PaymentType_Inspection_Online, PaymentRemarks,


                           Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername
                           );


                        //Dhiraj NSIC 07 September 

                        //Dhiraj 06 Sep
                        if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
                        {

                            Double SCSTAmout = 100.00;
                            Double ServiceTax = 18.00;

                            if (radPay.SelectedValue == "1")
                            {
                                SCSTAmout = Convert.ToDouble(lblCashAmnt.Text.ToString());
                            }
                            else if (radPay.SelectedValue == "2")
                            {
                                SCSTAmout = Convert.ToDouble(txt_chqddAmnt.Text.ToString());
                            }
                            else if (radPay.SelectedValue == "3")
                            {
                                SCSTAmout = Convert.ToDouble(txt_chqddAmnt.Text.ToString());
                            }
                            else if (radPay.SelectedValue == "4")
                            {
                                SCSTAmout = Convert.ToDouble(txt_amount.Text.ToString());
                            }
                            int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));

                            //Dhiraj NSIC 07 September 

                        }


                    }
                }
                else
                {
                    if (RoleId == 0 || RoleId == 7)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                        fGPtbl_PD_BranchConfirmation_inspection = 0;
                    }

                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                        fGPtbl_PD_BranchConfirmation_inspection = 1;
                    }


                    /* ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                        AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath, CashReceiptNo,
                        fGPtbl_PD_BranchConfirmation, FormTypeID, UserId);*/

                    ////ReferenceNo = objCompany.Company_CompanyAmdment_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                    ////AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath, CashReceiptNo,
                    ////fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                    //// PaymentTypeInspection,
                    ////    ChqDdNo_Inspection, BankName_Inspection,
                    ////    BranchName_Inspection, ChqDdate_Inspection,
                    ////    PaybalAt_Inspection,
                    ////    NewFileName_inspection,
                    ////    OriginalFilePath,
                    ////    fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number, PaymentRemarks);


                    ReferenceNo = Company_CompanyAmdment_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                    AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath, CashReceiptNo,
                    fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                     PaymentTypeInspection,
                        ChqDdNo_Inspection, BankName_Inspection,
                        BranchName_Inspection, ChqDdate_Inspection,
                        PaybalAt_Inspection,
                        NewFileName_inspection,
                        OriginalFilePath,
                        fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number, PaymentRemarks,

                             Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername

                        );



                    //Dhiraj 06 Sep
                    if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
                    {

                        Double SCSTAmout = 100.00;
                        Double ServiceTax = 18.00;
                        //if(radPay.SelectedValue == "1")
                        //{
                        //    SCSTAmout =Convert.ToDouble(lblCashAmnt.Text.ToString());
                        //}
                        //else if (radPay.SelectedValue == "2")
                        //{
                        //    SCSTAmout = Convert.ToDouble(txt_chqddAmnt.Text.ToString());
                        //}
                        //else if (radPay.SelectedValue == "3")
                        //{
                        //    SCSTAmout = Convert.ToDouble(txt_chqddAmnt.Text.ToString());
                        //}
                        //else if (radPay.SelectedValue == "4")
                        //{
                        //    SCSTAmout = Convert.ToDouble(txt_amount.Text.ToString());
                        //}
                        int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));

                        //Dhiraj NSIC 07 September 

                    }
                }

                //else
                //{
                //    if (RoleId == 8)
                //    {
                //        ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, newFileName, OriginalFilePath, CashReceiptNo, 1, FormTypeID, UserId);
                //    }
                //    else
                //    {
                //        ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, newFileName, OriginalFilePath, CashReceiptNo, 0, FormTypeID, UserId);

                //    }
                //}




                Session["ReferenceNo"] = ReferenceNo;
                DecryptEncrypt objdecrypt = new DecryptEncrypt();
                string comid = objdecrypt.Encrypt(Convert.ToString(CompanyId));

                Response.Redirect("ThankyouOnline.aspx?id=" + comid, false);
            }


        }
        catch (Exception ex)
        {
        }

        //////}

        //////else
        //////{
        //////    //If Cheque date is less than proposal date
        //////    lblDateMsg.Text = "Cheque date can not be less than Proposal Date" + proposaldate.ToString(); 

        //////}




    }


    public int Update_SC_ST_FeeDetails(int Companyid, Double Amount, Double ServiceTax)
    {
        SqlCommand MySqlComm = new SqlCommand();


        int ErrorIdNew = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Step7_SC_ST_FeeDetails", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;


            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_PD_SCST_Amount", SqlDbType.VarChar, 50).Value = Amount;
            MySqlComm.Parameters.Add("@fGPtbl_PD_ServiceTax", SqlDbType.VarChar, 50).Value = ServiceTax;

            //SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            //outError.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(outError);

            ErrorIdNew = MySqlComm.ExecuteNonQuery();
            //ErrorIdNew = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }

        return ErrorIdNew;
    }


    private bool FunUploadFile_1()
    {
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
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return true;
    }


    private bool FunUploadFile_2()
    {
        try
        {
            if (FileUpload2.HasFile)
            {
                object postedFile = FileUpload2.PostedFile;
                string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
                string fileExtn = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);

                if (filenameWithoutExtn != "")
                {
                    string contentType = FileUpload2.PostedFile.ContentType.ToString();
                    int contentLength = Convert.ToInt32(FileUpload2.PostedFile.ContentLength.ToString());
                    NewFileName_inspection = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

                    int AllowedContentLengthInKB = 1000;
                    int AllowedContentLengthInBytes = AllowedContentLengthInKB * 1024;
                    if (contentLength > AllowedContentLengthInBytes)
                    {
                        Label2.Text = "Document size can't be greater than 1 MB.";
                        // FileStatus_1 = 1;
                        FileStatus_1_inspection = 1;
                        return false;
                    }
                    else
                    {
                        Label2.Text = "";
                        FileStatus_1_inspection = 2;
                    }

                    if (FileStatus_1_inspection == 2)
                    {
                        FileUpload2.PostedFile.SaveAs(NewFileName_inspection);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        return true;
    }
    /*  protected void rdl_fund_transfer_select_index_change(object sender, EventArgs e)
      {
          if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 1)
          {
              pnlChqDD.Visible = false;
              pnlCash.Visible = false;
              Session["PaymentType_fundtransfer"] = "1";
              txt_chqddAmnt.Text = AmountPayable.ToString();
              pnlChqDD.Visible = false;
              pnlFundTransfer.Visible = true;

          }

          else if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 2)
          {
              pnlChqDD.Visible = false;
              pnlCash.Visible = false;
              Session["PaymentType_fundtransfer"] = "2";
              txt_chqddAmnt.Text = AmountPayable.ToString();
              pnlChqDD.Visible = false;
              pnlFundTransfer.Visible = true;

          }
      }*/
}