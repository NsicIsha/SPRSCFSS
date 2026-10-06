using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 

public partial class appAdministrator_Online_UnitRenewalStep7 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailsId, FormTypeID;

    double AmountPayable;
    int UserId, RoleId, DepartmentId;
    string PaymentDetailsIdstring, proposaldate, renewaldate;
    string UserFullName;

    clsCompany objCompany;
    clsRenewal objRenewal;
    int ErrorId;
    string ReferenceNo;
    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";


    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    //FormInputReplace objInputReplace;
    int outVariable = 0;


    int CategoryCastId = 0;
    double AmmountPaybale_all = 0.0;
    double Servictax_all = 0.0;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            // checkButtonDoubleClick
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
        }
        else
        {
            OriginalFilePath = "./Document/Upload/Amendment/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Amendment/";
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
            FormTypeID = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());



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
        if (Status == 5 || Status == 123 || Status == 3 || Status == 2)    //INCOMPLETE
        {
            //UNIT TABLE
            string str = null;
            string[] strArr = null;
            int count = 0;
            int Unitamnt = 0;
            double servicetax = 0.0;
            char[] splitchar = { ',' };
            strArr = PaymentDetailsIdstring.Split(splitchar);
            int PaymentType = 0;

            //for (count = 0; count <= (strArr.Length - 1); count++)
            for (count = 0; count < 1; count++)
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
                        lblUnitAmnt.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(MyDr["fGPtbl_PD_UnitAmount"]).ToString()));
                        Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());
                    }

                    servicetax = servicetax + Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString());// +Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

                    lblTotAmnt.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()))).ToString()));
                    AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()));



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
                        }
                        if (MyDr["fGPtbl_PD_BankName"].ToString() != "")
                        {
                            txt_BankName.Text = MyDr["fGPtbl_PD_BankName"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_BankBranch"].ToString() != "")
                        {
                            txt_BankBranch.Text = MyDr["fGPtbl_PD_BankBranch"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_PaybalAt"].ToString() != "")
                        {
                            txt_PayableAt.Text = MyDr["fGPtbl_PD_PaybalAt"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_PaybalAt"].ToString() != "")
                        {
                            txt_PayableAt.Text = MyDr["fGPtbl_PD_PaybalAt"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_UnitAmount"].ToString() != "")
                        {
                            txt_chqddAmnt.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString() != "")
                        {
                            txt_chqddate.Text = MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_PaymentRemarks"].ToString() != "")
                        {
                            txtChequeDDPaymentRemarks.Text = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();
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
                        }
                        if (MyDr["fGPtbl_PD_BankName"].ToString() != "")
                        {
                            txt_bank_name.Text = MyDr["fGPtbl_PD_BankName"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_BankBranch"].ToString() != "")
                        {
                            txt_branch_name.Text = MyDr["fGPtbl_PD_BankBranch"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_PaybalAt"].ToString() != "")
                        {
                            txt_PayableAt.Text = MyDr["fGPtbl_PD_PaybalAt"].ToString();
                        }         
                        if (MyDr["fGPtbl_PD_UnitAmount"].ToString() != "")
                        {
                        txt_amount.Text = MyDr["fGPtbl_PD_UnitAmount"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString() != "")
                        {
                        txt_date_utr.Text = MyDr["fGPtbl_PD_Cheque_DD_Date"].ToString();
                        }
                        if (MyDr["fGPtbl_PD_PaymentRemarks"].ToString() != "")
                        {
                        txtNeftPaymentRemarks.Text = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();
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
            lblSubTot.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Unitamnt).ToString())));
            lblTotAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal((Unitamnt + Math.Round(servicetax, 0))));
            AmountPayable = Convert.ToDouble(Unitamnt + Math.Round(servicetax, 1));
            lblServiceTax.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(servicetax, 0))).ToString()));



            //Dhiraj 
            clsCompany clsObjN = new clsCompany();
            DataSet ds = new DataSet();


            ds = CategoryCast_Details_ByCompanyId(CompanyId);


            CategoryCastId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryCastId"]);
 

             // CategoryCastId = 10;
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {
                objRenewal = new clsRenewal();
                DataTable dtPropdate = new DataTable();
                dtPropdate = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
                proposaldate = dtPropdate.Rows[0]["proposaldate"].ToString();
                renewaldate = dtPropdate.Rows[0]["renewaldate"].ToString();

               // renewaldate = "08/11/2019";
               //proposaldate = "09/12/2021";
                bool Isprovisional = Convert.ToBoolean(dtPropdate.Rows[0]["isProvisional"]);

                DateTime newdate = Isprovisional ? Convert.ToDateTime(renewaldate).AddDays(395) : Convert.ToDateTime(renewaldate).AddDays(760);

                //Response.Write("proposaldate" + proposaldate);
                //Response.Write("renewaldate" + renewaldate);
                //Response.Write("newdate" + newdate);
                // DateTime newdate = Convert.ToDateTime(renewaldate).AddDays(760);
                if (newdate > Convert.ToDateTime(proposaldate))
                {

                    Double unitamount_SC_ST = 100.0;
                    Double Servicetax_SC_ST = 18.0;
                    lblUnitAmnt.Text = "Rs." + "100.00";
                    lblServiceTax.Text = "Rs." + (Math.Round(Convert.ToDouble(Servicetax_SC_ST), 2)).ToString("0.00"); // "Rs." + "18.00";

                    lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00"); // Registration Fee

                    lblSubTot.Text = "Rs." + "100.00";
                    txt_chqddAmnt.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");
                    txt_amount.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");

                    //RequiredFieldValidator5.Enabled = false;
                    //RequiredFieldValidator6.Enabled = false;
                    //RequiredFieldValidator7.Enabled = false;
                    //RequiredFieldValidator8.Enabled = false;
                    //CValidatortxt_cheq1.Enabled = false;
                    //RequiredFieldValidator9454.Enabled = false;


                    //txt_chqddNo_inspection.Enabled = false;
                    //txt_BankNam_inspection.Enabled = false;
                    //txt_BankBranch_inspection.Enabled = false;
                    //txt_PayableAt_inspection.Enabled = false;
                    //txt_DD_Inspection.Enabled = false;
                    //txt_chqddate_inspection.Enabled = false;
                    //FileUpload2.Enabled = false;
                }

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


                //RequiredFieldValidator5.Enabled = true;
                //RequiredFieldValidator6.Enabled = true;
                //RequiredFieldValidator7.Enabled = true;
                //RequiredFieldValidator8.Enabled = true;
                //CValidatortxt_cheq1.Enabled = true;

                //RequiredFieldValidator9454.Enabled = true;



                //txt_chqddNo_inspection.Enabled = true;
                //txt_BankNam_inspection.Enabled = true;
                //txt_BankBranch_inspection.Enabled = true;
                //txt_PayableAt_inspection.Enabled = true;
                //txt_DD_Inspection.Enabled = true;
                //txt_chqddate_inspection.Enabled = true;
                //FileUpload2.Enabled = true;
            }
            
        }


        //objRenewal = new clsRenewal();
        //DataTable dtPropdate = new DataTable();
        //dtPropdate = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
        //proposaldate = dtPropdate.Rows[0]["proposaldate"].ToString();
        txtpropdate.Text = proposaldate;

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

    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {
        objRenewal = new clsRenewal();
        DataTable dtPropdate = new DataTable();
        dtPropdate = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
        proposaldate = dtPropdate.Rows[0]["proposaldate"].ToString();
        renewaldate = dtPropdate.Rows[0]["renewaldate"].ToString();

        //renewaldate = "08/11/2019";
        //proposaldate = "09/12/2021";
        bool Isprovisional = Convert.ToBoolean(dtPropdate.Rows[0]["isProvisional"]);

        DateTime newdate = Isprovisional ? Convert.ToDateTime(renewaldate).AddDays(395) : Convert.ToDateTime(renewaldate).AddDays(760);
        
        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
        {
            pnlCash.Visible = true;
            pnlChqDD.Visible = false;
            if (RoleId == 8)
            {
                receipt.Visible = true;

            }
            else
            {
                receipt.Visible = false;
            }
            Session["PaymentType"] = "1";
            //lblCashAmnt.Text = "Rs."+ Convert.ToString(string.Format( "{0:0.00}",  Convert.ToDecimal( (AmountPayable))));





            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {
                if (newdate > Convert.ToDateTime(proposaldate))
                {
                    lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                }
                else
                {
                    lblCashAmnt.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));
                }


            }
            else
            {
                lblCashAmnt.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));
                //  lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));
            }

            //lblCashAmnt.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));


        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            Session["PaymentType"] = "2";
            //txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal ((Math.Round(AmountPayable, 0)).ToString())));


            //lblCashAmnt.Text = "Rs."+ Convert.ToString(string.Format( "{0:0.00}",  Convert.ToDecimal( (AmountPayable))));

            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                //  txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));

                if (newdate > Convert.ToDateTime(proposaldate))
                {
                    txt_chqddAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                }
                else
                {
                    txt_chqddAmnt.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));
                }

            }
            else
            {
                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));
                // txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal ((Math.Round(AmountPayable, 0)).ToString())));
            }

            //txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));

            pnlChqDD.Visible = true;
            pnlFundTransfer.Visible = false;


        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            Session["PaymentType"] = "3";
            // txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal( (Math.Round(AmountPayable, 0)).ToString())));


            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                //  txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));

                if (newdate > Convert.ToDateTime(proposaldate))
                {
                    txt_chqddAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                }
                else
                {
                    txt_chqddAmnt.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));
                }
            }
            else
            {
                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));
                // txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal ((Math.Round(AmountPayable, 0)).ToString())));
            }

            //txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));

            pnlChqDD.Visible = true;
            pnlFundTransfer.Visible = false;

        }

        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {

            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                //txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));

                if (newdate > Convert.ToDateTime(proposaldate))
                {
                    txt_amount.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                }
                else
                {
                    txt_amount.Text = "Rs." + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((AmountPayable))));
                }
            }
            else
            {
                txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));
                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));
            }

          //  txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(AmountPayable, 0)).ToString())));

            pnlFundTransfer.Visible = true;
            Session["PaymentType"] = "4";
            pnlChqDD.Visible = false;
            pnlCash.Visible = false;
        }


        /* else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
         {
             Session["PaymentType"] = "4";

         }*/

    }


    public string CompanyRenewal_Insert_Step7_OnlineAddition(int Companyid, int Unitid, int fGPtbl_PD_Id, int fGPtbl_PD_PaymentType,
          double @fGPtbl_PD_PaidAmount, string  fGPtbl_PD_PaybalAt, string fGPtbl_PD_BankBranch, string fGPtbl_PD_BankName,
          string fGPtbl_PD_ChequeDDNo, string fGPtbl_PD_ChequeDDate, string fGPtbl_PD_Filename, string fGPtbl_PD_Filepath, string fGPtbl_PD_CashReceiptNo,
          int fGPtbl_PD_BranchConfirmation, int FormTypeID, int UserId, string fGPtbl_PD_Utr_number, int fGPtbl_PD_Fund_transfer_type, string Paymentremrks,

          string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername


          )
    {
        string Returnvalue = "";
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step7_OnlineAddition_Online_Renewal", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = fGPtbl_PD_Id;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = fGPtbl_PD_PaymentType;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = Unitid;

            if (fGPtbl_PD_ChequeDDNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_ChequeDDNo;
            }

            if (fGPtbl_PD_Utr_number != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Utr_number", SqlDbType.VarChar, 100).Value = fGPtbl_PD_Utr_number;
            }


            MySqlComm.Parameters.Add("@fGPtbl_PD_Fund_transfer_type", SqlDbType.Int).Value = fGPtbl_PD_Fund_transfer_type;
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

            if (fGPtbl_PD_Filepath != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Filepath", SqlDbType.VarChar, 500).Value = fGPtbl_PD_Filepath;
            }

            if (fGPtbl_PD_CashReceiptNo != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_CashReceiptNo", SqlDbType.VarChar, 50).Value = fGPtbl_PD_CashReceiptNo;
            }
            if (fGPtbl_PD_ChequeDDate != "")
            {
                MySqlComm.Parameters.Add("@fGPtbl_PD_Date", SqlDbType.DateTime).Value = Convert.ToDateTime(fGPtbl_PD_ChequeDDate);
            }
            MySqlComm.Parameters.Add("@fGPtbl_PD_BranchConfirmation", SqlDbType.Int).Value = fGPtbl_PD_BranchConfirmation;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaidAmount", SqlDbType.Float).Value = fGPtbl_PD_PaidAmount;
            MySqlComm.Parameters.Add("@FormTypeID", SqlDbType.Int).Value = FormTypeID;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            if (Paymentremrks != "")
            {
                MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar, 500).Value = Paymentremrks;
            }


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


    protected void Button1_Click(object sender, EventArgs e)
    {


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
            objRenewal = new clsRenewal();
            int PaymentType;
            string PaybalAt, BankName, BranchName, ChqDdNo, ChqDdate, CashReceiptNo;

            string Utr_Number = "";
            int Fund_Transfer_type = 0;
            string PaymentRemarks = "";

            PaybalAt = "";
            BankName = "";
            BranchName = "";
            ChqDdNo = "";
            CashReceiptNo = "";
            //// Dhiraj 24042018

            ////PaybalAt = txt_PayableAt.Text;
            ////BankName = txt_BankName.Text;
            ////BranchName = txt_BankBranch.Text;
            ////ChqDdNo = txt_chqddNo.Text;
            ////ChqDdate = txt_chqddate.Text.Trim();



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
            if (Convert.ToInt32(radPay.SelectedValue.ToString()) == 4)
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
            PaymentType = Convert.ToInt32(radPay.SelectedValue.ToString());// Convert.ToInt32(Session["PaymentType"].ToString());
            FunUploadFile_1();

            if (FormTypeID == 2)
            {

                //if (RoleId == 8)
                //{
                    //ReferenceNo = objRenewal.CompanyRenewal_Insert_Step7(CompanyId, UnitId, PaymentDetailsId,
                    //    PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName,
                    //    OriginalFilePath, CashReceiptNo, 1, FormTypeID, UserId);


                    ReferenceNo = CompanyRenewal_Insert_Step7_OnlineAddition(CompanyId, UnitId, PaymentDetailsId,
                        PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName,
                        OriginalFilePath, CashReceiptNo, 1, FormTypeID, UserId, Utr_Number, Fund_Transfer_type, PaymentRemarks,


                        Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername

                        );


                    // Company_Insert_Step7_Modified


                    //Dhiraj NSIC 07 September  
                    if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
                    {
                        Double SCSTAmout = 100.00;
                        Double ServiceTax = 18.00;
                        int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));

                    }


               // }
    //////            else if (RoleId == 7)
    //////            {
    //////                //ReferenceNo = objRenewal.CompanyRenewal_Insert_Step7(CompanyId, UnitId, PaymentDetailsId,
    //////                //    PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName,
    //////                //    OriginalFilePath, CashReceiptNo, 0, FormTypeID, UserId);


    //////                ReferenceNo = CompanyRenewal_Insert_Step7_OnlineAddition(CompanyId, UnitId, PaymentDetailsId,
    //////PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName,
    //////OriginalFilePath, CashReceiptNo, 0, FormTypeID, UserId, Utr_Number, Fund_Transfer_type, PaymentRemarks);



    //////                //Dhiraj NSIC 07 September  
    //////                if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
    //////                {
    //////                    Double SCSTAmout = 100.00;
    //////                    Double ServiceTax = 18.00;
    //////                    int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));
    //////                }
    //////            }
            }

            if (ReferenceNo == "1")
            {
                Session["ReferenceNo"] = "2";
                Response.Redirect("Thankyou.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
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

    protected void btnCash_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            objRenewal = new clsRenewal();
            int PaymentType;
            string PaybalAt, BankName, BranchName, ChqDdNo, Receiptno;
            PaybalAt = "";
            BankName = "";
            BranchName = "";
            ChqDdNo = "";
            Receiptno = "";


            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());

            if (FormTypeID == 2)
            {
                if (RoleId == 8)
                {
                    Receiptno = txtReceipt.Text.Trim();
                    ReferenceNo = objRenewal.CompanyRenewal_Insert_Step7(CompanyId, UnitId,
                        PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName,
                        ChqDdNo, "", "", "", Receiptno, 1, FormTypeID, UserId);


                    //Dhiraj NSIC 07 September  
                    if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
                    {
                        Double SCSTAmout = 100.00;
                        Double ServiceTax = 18.00;
                        int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));

                    }


                }
                else if (RoleId == 7)
                {
                    ReferenceNo = objRenewal.CompanyRenewal_Insert_Step7(CompanyId, UnitId,
                        PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName,
                        ChqDdNo, "", "", "", Receiptno, 0, FormTypeID, UserId);


                    //Dhiraj NSIC 07 September  
                    if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
                    {
                        Double SCSTAmout = 100.00;
                        Double ServiceTax = 18.00;
                        int result = Update_SC_ST_FeeDetails(CompanyId, Convert.ToDouble(SCSTAmout), Convert.ToDouble(ServiceTax));

                    }
                }

            }
            if (ReferenceNo == "1")
            {
                Session["ReferenceNo"] = "2";
            }

            Response.Redirect("Thankyou.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }



    }

    private void FunUploadFile_1()
    {
        try
        {
            if (!(FileUpload1.PostedFile == null))
            {
                object postedFile = FileUpload1.PostedFile;
                string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                string fileExtn = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

                if (filenameWithoutExtn != "")
                {
                    string contentType = FileUpload1.PostedFile.ContentType.ToString();
                    int contentLength = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());
                    newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

                    int AllowedContentLengthInKB = 50;
                    int AllowedContentLengthInBytes = AllowedContentLengthInKB * 1024;
                    if (contentLength > AllowedContentLengthInBytes)
                    {
                        Label1.Text = "Document size can't be greater then 1 MB.";
                        FileStatus_1 = 1;
                    }
                    else
                    {
                        Label1.Text = "";
                        FileStatus_1 = 2;
                    }

                    if (FileStatus_1 == 2)
                    {

                        FileUpload1.PostedFile.SaveAs(filePath + newFileName);
                        // objCompany = new clsCompany();
                        // ErrorId = 0;
                        // ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 1, TextBox1.Text.ToString().Trim(), newFileName, OriginalFilePath, UserId);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Save & Continue >>';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }

}