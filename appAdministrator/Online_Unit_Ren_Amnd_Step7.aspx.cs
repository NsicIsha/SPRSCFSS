using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Online_Unit_Ren_Amnd_Step7 : System.Web.UI.Page
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
            lbl_registration_amedment.Text = "Amendment";
            lbl_registration_fees_text.Text = "Amendment";
            lbl_payment_details_nsic.Text = "";
            lbl_select_payment.Text = "for Amendment fee";
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
          //  PaymentDetailsIdstring = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());
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
        if (Status == 5 || Status == 123 || Status == 3)    //INCOMPLETE
        {
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

                    lblTotAmnt.Text = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
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

            lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
            lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
            AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
            lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
            lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
            txt_DD_Inspection.Text = (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString();   //inspection amount total
            AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
            txt_amount.Text = AmountPayable.ToString();
        }
    }

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
            lblCashAmnt.Text = AmountPayable.ToString();

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            pnlChqDD.Visible = true;

            pnlCash.Visible = false;
            Session["PaymentType"] = "2";
            txt_chqddAmnt.Text = AmountPayable.ToString();
            pnlChqDD.Visible = true;
            pnlFundTransfer.Visible = false;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            Session["PaymentType"] = "3";
            txt_chqddAmnt.Text = AmountPayable.ToString();
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
            PaymentTypeInspection = 2;
            string PaybalAt_Inspection, BankName_Inspection, BranchName_Inspection, ChqDdNo_Inspection, ChqDdate_Inspection, ChqDDAmount_Inspection;
            PaybalAt_Inspection = "";
            BankName_Inspection = "";
            BranchName_Inspection = "";

            ChqDdNo_Inspection = "";
            ChqDdate_Inspection = "";
            ChqDDAmount_Inspection = "";

            PaybalAt_Inspection = txt_PayableAt_inspection.Text;
            BankName_Inspection = txt_BankNam_inspection.Text;
            BranchName_Inspection = txt_BankBranch_inspection.Text;
            ChqDdNo_Inspection = txt_chqddNo_inspection.Text;
            ChqDdate_Inspection = txt_chqddate_inspection.Text.Trim();
            ChqDDAmount_Inspection = txt_DD_Inspection.Text.ToString();
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
                        ReferenceNo = objCompany.Company_Insert_Step7_Modified(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                            AmmountPaybale_all, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                            CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId,
                            PaymentTypeInspection,
                            ChqDdNo_Inspection, BankName_Inspection,
                            BranchName_Inspection, ChqDdate_Inspection,
                            PaybalAt_Inspection,
                            NewFileName_inspection,
                            OriginalFilePath,
                            fGPtbl_PD_BranchConfirmation, Fund_Transfer_type, Utr_Number, 0, PaymentRemarks);

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

                Response.Redirect("Thankyou.aspx", false);
            }


        }
        catch (Exception ex)
        {
        }



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
    protected void rdl_fund_transfer_select_index_change(object sender, EventArgs e)
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
    }
}