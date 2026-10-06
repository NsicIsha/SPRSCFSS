using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_AdminEditPaymentDetail : System.Web.UI.Page
{

    float TotalAmount;
    float servicetaxrs=0;
    int UserId, RoleId, DepartmentId;
    string Payback;
    string UserFullName;
    int CompanyId, UnitId, Status, BranchId, FormTypeID;
    int ErrorId;
    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";
    clsCompanyInformationEdit objCompanyedit;

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
        }
        else
        {
            OriginalFilePath = "./Document/Upload/Amendment/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Amendment/";
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
            Payback = (objDecEnc.Decrypt(Request.QueryString["pb"].Replace(" ", "+")).ToString().Remove(0,3));
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
        try
        {
            if (Status == 5 || Status == 123 || Status == 3 || Payback!= "0")    //INCOMPLETE
            {
                objCompanyedit = new clsCompanyInformationEdit();
                //float servicetaxper = 0;

                /*lblUnitAmnt.Text = Payback;
                servicetaxper = objCompanyedit.Fetch_ServiceTax();
                servicetaxrs = (Convert.ToSingle(Payback) * Convert.ToSingle(servicetaxper)) / 100;
                lblServiceTax.Text = Convert.ToString(Math.Round(servicetaxrs,1));
                TotalAmount = Convert.ToSingle(Payback) + Convert.ToSingle(Math.Round(servicetaxrs,1));*/
                int AddSub = Convert.ToInt32(Session["AddSubFees"]);
                TotalAmount =  (Convert.ToSingle(Payback)*AddSub);
               // lblTotAmnt.Text = Convert.ToString(Math.Round(TotalAmount,0));
                lblTotAmnt.Text="Rs."+" "+Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(TotalAmount)));
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in FunFillData() " + ex.StackTrace);
        }
    }

    protected void btnCash_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyedit = new clsCompanyInformationEdit();
            int PaymentType, OutResult=0, FundtransferType=0;
            string Receiptno = string.Empty;
            string UtrNumber = "";
            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());

            if (RoleId == 8)
            {
                Receiptno = txtReceipt.Text.Trim();
                
                OutResult = objCompanyedit.Insert_PaymentDetail(1,CompanyId, UnitId, Convert.ToSingle(Payback),
                    0,TotalAmount, PaymentType, "", Receiptno,"", "", "", "", "", "", UserId, FundtransferType, UtrNumber);
            }
            if (OutResult == 2)
            {
                Session["ReferenceNo"] = 8;
                Response.Redirect("Thankyou.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.StackTrace+"  "+ex.Message);
        }
    }

    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
        {
            pnlCash.Visible = true;
            pnlChqDD.Visible = false;
            pnlFundTransfer.Visible = false;
            Session["PaymentType"] = "1";
            lblCashAmnt.Text = "Rs."+" "+ Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(TotalAmount)));
        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            pnlFundTransfer.Visible = false;
            Session["PaymentType"] = "2";
            txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal (TotalAmount)));
        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            pnlFundTransfer.Visible = false;
            Session["PaymentType"] = "3";
            txt_chqddAmnt.Text =Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(TotalAmount)));
        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {
            pnlChqDD.Visible = false;
             pnlCash.Visible=false;
             pnlFundTransfer.Visible = true;
             Session["PaymentType"] = "4";
             txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(TotalAmount)));

        }
        
    }

    protected void btnCheqDd_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyedit = new clsCompanyInformationEdit();
            int PaymentType, OutResult=0, fundtransfertype=0;
            string PaybalAt, BankName, BranchName, ChqDdNo, ChqDdate, CashReceiptNo, UtrNumber;
            PaybalAt = "";
            BankName = "";
            BranchName = "";
            ChqDdNo = "";
            CashReceiptNo = "";
            PaybalAt = txt_PayableAt.Text;
            BankName = txt_BankName.Text;
            BranchName = txt_BankBranch.Text;
            ChqDdNo = txt_chqddNo.Text;
            ChqDdate = txt_chqddate.Text.Trim();
            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());
            UtrNumber = "";
            bool TF = FunUploadFile_1();

            if (TF)
            {
                    if (RoleId == 8)
                    {
                        OutResult= objCompanyedit.Insert_PaymentDetail(1,CompanyId, UnitId, Convert.ToSingle(Payback), 0,
                            TotalAmount, PaymentType, ChqDdNo, "", BankName, BranchName, ChqDdate, newFileName,
                            OriginalFilePath, PaybalAt, UserId, fundtransfertype,UtrNumber);
                    }
             }
            if (OutResult == 2)
            {
                Session["ReferenceNo"] = 8;
                Response.Redirect("Thankyou.aspx", false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  "+ ex.StackTrace);
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



    protected void btn_fundtransfer_Click(object sender, EventArgs e)
    {
        try
        {
            objCompanyedit = new clsCompanyInformationEdit();
            int PaymentType, OutResult = 0;
            string UTRNO, BankName, BranchName, PD_Date;
            UTRNO = "";
            BankName = "";
            BranchName = "";
            PD_Date = "";
            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());
            BankName = txt_bank_name.Text.ToString();
            BranchName = txt_branch_name.Text.ToString();
            UTRNO = txt_utr_no.Text.ToString();
            PD_Date = Convert.ToString(txt_date_utr.Text.ToString());
            if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 1)
            {
                pnlChqDD.Visible = false;
                pnlCash.Visible = false;
                Session["PaymentType_fundtransfer"] = "1";
                txt_amount.Text = Convert.ToString(Math.Round(TotalAmount, 0)); 
                pnlChqDD.Visible = false;
                pnlFundTransfer.Visible = true;

            }

            else if (Convert.ToInt32(rdl_fund_transfer.SelectedItem.Value.ToString()) == 2)
            {
                pnlChqDD.Visible = false;
                pnlCash.Visible = false;
                Session["PaymentType_fundtransfer"] = "2";
                txt_amount.Text = Convert.ToString(Math.Round(TotalAmount, 0));
                pnlChqDD.Visible = false;
                pnlFundTransfer.Visible = true;

            }
            objCompanyedit = new clsCompanyInformationEdit();
            if (RoleId == 8)
            {
                OutResult = objCompanyedit.Insert_PaymentDetail(1, CompanyId, UnitId, Convert.ToSingle(Payback), 0,
                    TotalAmount, PaymentType, "", "", BankName, BranchName, PD_Date, newFileName,
                    "", "", UserId, Convert.ToInt32(Session["PaymentType_fundtransfer"].ToString()), UTRNO);
            }
            if (OutResult == 2)
            {
                Session["ReferenceNo"] = 8;
                Response.Redirect("Thankyou.aspx", false);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.StackTrace + " " + ex.Message);
        }

    }
}
