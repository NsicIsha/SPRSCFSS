using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

public partial class appAdministrator_NewUnitRegistrationStep7Online : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailsId, FormTypeID, fGPtbl_PD_BranchConfirmation;

    double AmountPayable;
    int UserId, RoleId, DepartmentId;
    string PaymentDetailsIdstring;
    string UserFullName;

    clsCompany objCompany;

    int ErrorId;
    string ReferenceNo, compstr = string.Empty;
    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";

    int CategoryCastId = 0;
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
                        lblUnitAmnt.Text = "Rs." + " " + string.Format("{0:0.00}", Convert.ToDecimal(MyDr["fGPtbl_PD_UnitAmount"]));
                        Unitamnt = Convert.ToInt32(MyDr["fGPtbl_PD_UnitAmount"].ToString());
                    }
                    if (count == 0)
                    {
                        lblAgencySlabName.Text = MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                    }
                    else
                    {
                        lblAgencySlabName.Text = lblAgencySlabName.Text + " , " + MyDr["fGPtbl_PD_AgencySlabName"].ToString();
                    }

                    //lblAgencySlabInst.Text = MyDr["fGPtbl_PD_AgencySlabInstruction"].ToString();

                    lblAgencyAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(MyDr["fGPtbl_PD_AgencyAmount"]).ToString()));
                    Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
                    //  servicetax = servicetax + Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());
                    // servicetax = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

                    AgencyServiceTax = AgencyServiceTax + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

                    servicetax = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(AgencyServiceTax);
                    lblTotAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:00}", Convert.ToDecimal((Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()))).ToString()));
                    AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));

                }
                MyDr.Close();
            }
            lblAgencyAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(Convert.ToString(Agncyamnt))));
            // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
            lblSubTot.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Unitamnt + Agncyamnt).ToString())));
            // (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
            //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));
            lblTotAmnt.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round((Unitamnt + Agncyamnt + servicetax), 0)).ToString())));
            AmountPayable = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(servicetax, 2));
            lblServiceTax.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Math.Round(servicetax, 0)).ToString())));




            clsCompany clsObjN = new clsCompany();
            DataSet ds = new DataSet();


            ds = CategoryCast_Details_ByCompanyId(CompanyId);


            CategoryCastId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryCastId"]);
            //CategoryCastId = 5;
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {
                // tbInspectionAgencyPaymentDetails.Visible = false;

                lblAgencyAmnt.Text = "Rs." + "0.00";
                //lblTotAmnt.Text = "Rs." + "0.00";

                // AmountPayable =Convert.ToDouble("0.00");
                // lblServiceTax.Text = "Rs." + "0.00";
                lblServiceTax.Text = "Rs." + "0.00";
                lblTotAmnt.Text = "Rs." + "0.00";
                // txt_DD_Inspection.Text = "0.00";
                AmmountPaybale_all = Convert.ToDouble("0.00");
                //txt_amount.Text = "Rs." + "0.00";


                Double unitamount_SC_ST = 100.0;
                Double Servicetax_SC_ST = 18.0;

                Double Agencyamount_SC_ST = 0.0;
                lblUnitAmnt.Text = "Rs." + "100.00";

                lblSubTot.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((unitamount_SC_ST + Agencyamount_SC_ST).ToString())));


                lblServiceTax.Text = "Rs." + (Math.Round(Convert.ToDouble(Servicetax_SC_ST), 2)).ToString("0.00"); // "Rs." + "18.00";

                lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00"); // Registration Fee

                txt_chqddAmnt.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");


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
            else
            {
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
    }

    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            //lblCashAmnt.Text = (Math.Round(AmountPayable,0)).ToString();


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


            //  lblCashAmnt.Text="Rs."+" "+ Convert.ToString( string.Format( "{0:0.00}", Convert.ToDecimal(AmountPayable)));

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            pnlChqDD.Visible = true;

            pnlCash.Visible = false;
            Session["PaymentType"] = "2";
            //txt_chqddAmnt.Text = (Math.Round(AmountPayable, 0)).ToString();
            ////  txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable)));

            //Dhiraj 06 Sep
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }
           // txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));

            pnlChqDD.Visible = true;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            pnlChqDD.Visible = true;
            pnlCash.Visible = false;
            Session["PaymentType"] = "3";
            // txt_chqddAmnt.Text = (Math.Round(AmountPayable, 0)).ToString();
            //// txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable)));

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

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {
            Session["PaymentType"] = "4";

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            int PaymentType;
            string PaybalAt, BankName, BranchName, ChqDdNo, ChqDdate, CashReceiptNo;
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
            // ChqDdate = "07/25/2012";
            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());

            bool TF = FunUploadFile_1();

            //if(TF)  OLD
            if (true)// BECAUSE UPLOADING OF FILE IS NOT NECESSARY
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
                        ReferenceNo = objCompany.Company_Insert_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                            AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath,
                            CashReceiptNo, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId, PaymentRemarks);
                    }
                }
                else
                {
                    if (RoleId == 0 || RoleId == 7)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                    }
                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                    }

                    ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                        AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, newFileName, OriginalFilePath, CashReceiptNo,
                        fGPtbl_PD_BranchConfirmation, FormTypeID, UserId, PaymentRemarks);


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

                DecryptEncrypt objdecrypt = new DecryptEncrypt();
                compstr = objdecrypt.Encrypt(Convert.ToString(CompanyId));
                Session["ReferenceNo"] = ReferenceNo;
                Response.Redirect("Thankyou.aspx?id=" + compstr, false);
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
            int PaymentType;
            string PaybalAt, BankName, BranchName, ChqDdNo, ChqDdate, Receiptno;
            PaybalAt = "";
            BankName = "";
            BranchName = "";
            ChqDdNo = "";
            ChqDdate = "";
            Receiptno = "";
            string PaymentRemarks = "";

            PaymentRemarks = txtChequeDDPaymentRemarks.Text;

            PaymentType = Convert.ToInt32(Session["PaymentType"].ToString());

            if (FormTypeID != 3)
            {
                if (RoleId == 8 || RoleId == 0)
                {
                    Receiptno = txtReceipt.Text.Trim();
                    if (RoleId == 0)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                    }
                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                    }
                    ReferenceNo = objCompany.Company_Insert_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, "", "", Receiptno, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId, PaymentRemarks);
                }
                else
                {
                    if (RoleId == 0)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                    }
                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                    }
                    ReferenceNo = objCompany.Company_Insert_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType, AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, "", "", Receiptno, fGPtbl_PD_BranchConfirmation, FormTypeID, UserId, PaymentRemarks);

                }
            }
            else
            {
                if (RoleId == 8 || RoleId == 0)
                {
                    Receiptno = txtReceipt.Text.Trim();
                    if (RoleId == 0 || RoleId == 7)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                    }
                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                    }

                    ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                        AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, "", "", Receiptno, fGPtbl_PD_BranchConfirmation,
                        FormTypeID, UserId, PaymentRemarks);
                }
                else
                {
                    if (RoleId == 0 || RoleId == 7)
                    {
                        fGPtbl_PD_BranchConfirmation = 0;
                    }
                    else
                    {
                        fGPtbl_PD_BranchConfirmation = 1;
                    }
                    ReferenceNo = objCompany.Company_CompanyAmdment_Step7(CompanyId, UnitId, PaymentDetailsId, PaymentType,
                        AmountPayable, PaybalAt, BranchName, BankName, ChqDdNo, ChqDdate, "", "", Receiptno, fGPtbl_PD_BranchConfirmation,
                        FormTypeID, UserId, PaymentRemarks);

                }
            }
            DecryptEncrypt objdecrypt = new DecryptEncrypt();
            compstr = objdecrypt.Encrypt(Convert.ToString(CompanyId));
            Session["ReferenceNo"] = ReferenceNo;
            Response.Redirect("Thankyou.aspx?id=" + compstr, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


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


}