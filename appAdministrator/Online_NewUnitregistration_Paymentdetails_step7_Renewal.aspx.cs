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

public partial class appAdministrator_Online_NewUnitregistration_Paymentdetails_step7_Renewal : System.Web.UI.Page
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


            pnlChqDD.Visible = true;



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

        InspectionfeeStatus = 240;
        if (InspectionfeeStatus == 240)
        {
          
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
           // lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
          // lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
          ////  txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

           // txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total



            //AgencyAmount = 0;
            //AgencyServiceTax = 0;
            //Agncyamnt = 0;
           // lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
           // lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
           // lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
           // txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

          //  txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total



        }
        else
        {
           // tdInspectionAgencyFee.Visible = false;

            AgencyAmount = 0;
            AgencyServiceTax = 0;
            Agncyamnt = 0;
           // lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
           // lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
           // lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
           // txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

           // txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total

        }








        clsCompany clsObjN = new clsCompany();
        DataSet ds = new DataSet();


        ds = CategoryCast_Details_ByCompanyId(CompanyId);


        CategoryCastId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryCastId"]);
        //CategoryCastId = 5;
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


           


           
        }
        else
        {
           
        }
        //////}
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
            //lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));

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

            if (Convert.ToInt32(Session["PaymentType"].ToString()) == 4)
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
            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());

            int PaymentType_Inspection_Online = 0;


            



            string PaybalAt_Inspection, BankName_Inspection, BranchName_Inspection, ChqDdNo_Inspection, ChqDdate_Inspection, ChqDDAmount_Inspection;
            PaybalAt_Inspection = "";
            BankName_Inspection = "";
            BranchName_Inspection = "";

            ChqDdNo_Inspection = "";
            ChqDdate_Inspection = "";
            ChqDDAmount_Inspection = "";


            


         





            if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
            {
                CashReceiptNo = txtReceipt.Text.ToString();
            }
            else
            {
                CashReceiptNo = "";
            }




            bool TF = FunUploadFile_1();
            

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
                        ReferenceNo = objCompany.Company_Insert_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                            AmmountPaybale_all, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                            CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                            PaymentTypeInspection,
                            ChqDdNo_Inspection, BankName_Inspection,
                            BranchName_Inspection, ChqDdate_Inspection,
                            PaybalAt_Inspection,
                            NewFileName_inspection,
                            OriginalFilePath,
                            fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number,

                           PaymentType_Inspection_Online, PaymentRemarks
                            );


                        //Dhiraj NSIC 07 September 

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

                    ReferenceNo = objCompany.Company_CompanyAmdment_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                    AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath, CashReceiptNo,
                    fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                     PaymentTypeInspection,
                        ChqDdNo_Inspection, BankName_Inspection,
                        BranchName_Inspection, ChqDdate_Inspection,
                        PaybalAt_Inspection,
                        NewFileName_inspection,
                        OriginalFilePath,
                        fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number, PaymentRemarks);





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

                Response.Redirect("Thankyou.aspx?id=" + comid, false);
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