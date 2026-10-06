using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

using System.Collections;



using System.Net.Mail;

using System.Web.UI.HtmlControls;

public partial class appAdministrator_Online_Renewal_VerifyPaymentDetails_View : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailsId, FormTypeID, fGPtbl_PD_BranchConfirmation, fGPtbl_PD_BranchConfirmation_inspection;
    int successId, urlId = 0, CUnitAmendmentId = 0;
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
    int PaymentType = 0;
    int PaymentType_inspection = 0;
    int Fund_transfer_type = 0;
    string emailid;
    int CategoryCastId = 0;
    int PaymentId = 0;
    string MailTo, MailFrom;
    DataSet Myds;
    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    Hashtable hashtbl = new Hashtable();
    string UserEmailID = string.Empty;
    public delegate Hashtable waitcompAccepDel(int unitid, int inspuserid, int userid);

    clsAmendment objAmendment;
    clsMasterMailer objMailer;
    clsRenewal objRenewal;
    clsMasterInspectionAgency objInsUser;// = new clsMasterInspectionAgency();


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
        FunFillData(PaymentId);
        if (!Page.IsPostBack)
        {

            DataSet dss = new DataSet();
            int AgencyId = 0;
            dss = DataSetAgency_Details_ByUnitId(UnitId);
            // AgencyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_PD_AgencyId"].ToString());

            // AgencyId = 10;
            // 8 MSMEDI, 9 RITES, 10 CDC
            //if (AgencyId == 9)
            //{
            //    //////rdl_inspectionmode_payment.Items.Clear();
            //    //////rdl_inspectionmode_payment.Visible = true;
            //    //////ListItem item;
            //    //////item = new ListItem("DD", "2");
            //    //////rdl_inspectionmode_payment.Items.Add(item);

            //    //////item = new ListItem("Cheque", "3");
            //    //////rdl_inspectionmode_payment.Items.Add(item);

            //    //////item = new ListItem("Fund Transfer", "4");
            //    //////rdl_inspectionmode_payment.Items.Add(item);


            //    //////rdl_inspectionmode_payment.Items.FindByValue("2").Selected = true;
            //    ///// PanelChequeDD.Visible = true;

            //}
            //else
            //{
            //    ////rdl_inspectionmode_payment.Items.Clear();
            //    ////rdl_inspectionmode_payment.Visible = true;
            //    ////ListItem item;
            //    ////item = new ListItem("DD", "2");
            //    ////rdl_inspectionmode_payment.Items.Add(item);

            //    ////item = new ListItem("Cheque", "3");
            //    ////rdl_inspectionmode_payment.Items.Add(item);

            //    ////item = new ListItem("Fund Transfer", "4");
            //    ////rdl_inspectionmode_payment.Items.Add(item);


            //    ////rdl_inspectionmode_payment.Items.FindByValue("2").Selected = true;
            //    /////  PanelChequeDD.Visible = true;

            //}

        }

    }

    public DataSet Company_Status_Fetch_ByCompanyId_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Status_Fetch_ByCompanyId_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["cid"].Replace(" ", "+")).ToString());
            PaymentId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["pid"].Replace(" ", "+")).ToString());

            DataSet ds = new DataSet();
            ds = Company_Status_Fetch_ByCompanyId_Online(CompanyId);
            Status = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString());
            UnitId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString());
            //PaymentDetailsId = (objDecEnc.Decrypt(Request.QueryString["pd"].Replace(" ", "+")).ToString());

        }
        catch
        {
            CompanyId = 0;
            PaymentId = 0;

        }
    }
    public SqlDataReader Company_Fetch_ByUnit_Step7(int PaymentDetailId) //int CompanyId,
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_PaymentDetail_ByPaymentDetailId_Online", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_Id", SqlDbType.Int).Value = PaymentDetailId;
            //MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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
    private void FunFillData(int PaymentId)
    {
        string Cheque_DD_Number = "";
        string Cheque_DD_BankName = "";
        string Cheque_DD_Branch = "";
        string Cheque_DD_PayableAt = "";
        string Cheque_DD_Amount = "";
        string Cheque_DD_Date = "";
        string Cheque_DD_Remarks = "";

        string Ins_Cheque_DD_Number = "";
        string Ins_Cheque_DD_BankName = "";
        string Ins_Cheque_DD_Branch = "";
        string Ins_Cheque_DD_PayableAt = "";
        string Ins_Cheque_DD_Amount = "";
        string Ins_Cheque_DD_Date = "";
        string Ins_Cheque_DD_Remarks = "";


        string UTR_Cheque_DD_Number = "";
        string UTR_Cheque_DD_BankName = "";
        string UTR_Cheque_DD_Branch = "";
        string UTR_Cheque_DD_PayableAt = "";
        string UTR_Cheque_DD_Amount = "";
        string UTR_Cheque_DD_Date = "";
        string UTR_Cheque_DD_Remarks = "";


        //if (Status == 4 ||  Status == 5 || Status == 123 || Status == 3)    //INCOMPLETE
        //{
        //UNIT TABLE
        //string[] strArr = null;
        int count = 0;
        int Unitamnt = 0, Agncyamnt = 0;
        double servicetax = 0.0, AgencyServiceTax = 0.0;
        // char[] splitchar = { ',' };
        // strArr = PaymentDetailsIdstring.Split(splitchar);

        // PaymentDetailsId = Convert.ToInt32(strArr[count]);
        objCompany = new clsCompany();
        System.Data.SqlClient.SqlDataReader MyDr;
        MyDr = Company_Fetch_ByUnit_Step7(PaymentId);// objCompany.Company_Fetch_ByUnit_Step7(PaymentId);
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


            //  Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
            servicetax = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString());
            // Servictax_all = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

            //  AgencyServiceTax = AgencyServiceTax + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyServiceTax"].ToString());

            // servicetax = Convert.ToDouble((MyDr["fGPtbl_PD_UnitServiceTax"]).ToString());// +Convert.ToDouble(AgencyServiceTax);

            // //lblTotAmnt.Text = "Rs." + "" + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())))));
            //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));
            //AgencyAmount = (Convert.ToDouble(Agncyamnt));


            PaymentType = Convert.ToInt32(MyDr["fGPtbl_PD_PaymentType"].ToString());
            if (PaymentType == 2)
            {
                pnlChqDD.Visible = true;
            }


            //PaymentType_inspection = Convert.ToInt32(MyDr["fGPtbl_PD_PaymentType_inspection"].ToString());
            //Fund_transfer_type = Convert.ToInt32(MyDr["fGPtbl_PD_Fund_transfer_type"].ToString());


            Cheque_DD_Number = MyDr["fGPtbl_PD_ChequeDDNo"].ToString();
            Cheque_DD_BankName = MyDr["fGPtbl_PD_BankName"].ToString();
            Cheque_DD_Branch = MyDr["fGPtbl_PD_BankBranch"].ToString();
            Cheque_DD_PayableAt = MyDr["fGPtbl_PD_PaybalAt"].ToString();
            Cheque_DD_Amount = MyDr["TotalNsicAmount"].ToString();
            Cheque_DD_Date = MyDr["fGPtbl_PD_Date"].ToString();
            Cheque_DD_Remarks = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();


            Ins_Cheque_DD_Number = MyDr["fGPtbl_PD_ChequeDDNo_inspection"].ToString();
            Ins_Cheque_DD_BankName = MyDr["fGPtbl_PD_BankName_inspection"].ToString();
            Ins_Cheque_DD_Branch = MyDr["fGPtbl_PD_BankBranch_inspection"].ToString();
            Ins_Cheque_DD_PayableAt = MyDr["fGPtbl_PD_PaybalAt_inspection"].ToString();
            Ins_Cheque_DD_Amount = MyDr["TotalInspectionAmount"].ToString(); ;
            Ins_Cheque_DD_Date = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();


            UTR_Cheque_DD_Number = MyDr["fGPtbl_PD_Utr_number"].ToString();
            //UTR_Cheque_DD_BankName = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();
            //UTR_Cheque_DD_Branch = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();
            //UTR_Cheque_DD_PayableAt = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();
            //UTR_Cheque_DD_Amount = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();
            //UTR_Cheque_DD_Date = MyDr["fGPtbl_PD_Cheque_DD_Date_inspection"].ToString();
            UTR_Cheque_DD_Remarks = MyDr["fGPtbl_PD_PaymentRemarks"].ToString();

        }
        MyDr.Close();

        //  PaymentType = 4;
        if (PaymentType == 2 || PaymentType == 3) //DD //Cheque
        {
            pnlChqDD.Visible = true;
            pnlOrtgs.Visible = false;
            pnlFundTransfer.Visible = false;


            radPay.Items.FindByValue(PaymentType.ToString()).Selected = true;
            txt_chqddNo.Text = Cheque_DD_Number;
            txt_BankName.Text = Cheque_DD_BankName;
            txt_BankBranch.Text = Cheque_DD_Branch;
            txt_PayableAt.Text = Cheque_DD_PayableAt;
            txt_chqddAmnt.Text = Cheque_DD_Amount;
            txt_chqddate.Text = Cheque_DD_Date;
            txtChequeDDPaymentRemarks.Text = Cheque_DD_Remarks;
        }

        else if (PaymentType == 4) //Fund Transfer
        {
            pnlOrtgs.Visible = true;
            pnlChqDD.Visible = false;
            pnlFundTransfer.Visible = true;
            radPay.Items.FindByValue(PaymentType.ToString()).Selected = true;

            txt_utr_no.Text = UTR_Cheque_DD_Number;
            txtNeftPaymentRemarks.Text = Cheque_DD_Remarks;

            txt_bank_name.Text = Cheque_DD_BankName;
            txt_branch_name.Text = Cheque_DD_Branch;

            txt_amount.Text = Cheque_DD_Amount;
            txt_date_utr.Text = Cheque_DD_Date;


        }
        //PaymentType_inspection = 3;






        // lblAgencyAmnt.Text = Convert.ToString(Agncyamnt);
        // Agncyamnt = Agncyamnt + Convert.ToInt32(MyDr["fGPtbl_PD_AgencyAmount"].ToString());
        //lblSubTot.Text = (Unitamnt + Agncyamnt).ToString();
        // (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString())).ToString();
        //AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_PD_UnitAmount"].ToString()) + Convert.ToDouble(MyDr["fGPtbl_PD_AgencyAmount"].ToString()));

        // lblAgencyAmnt.Text = "Rs." + (Math.Round(AgencyAmount, 2)).ToString("0.00");
        lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2)).ToString("0.00"); // Registration Fee
        AmountPayable = Convert.ToDouble(Math.Round(Convert.ToDouble(Unitamnt), 2) + Math.Round(servicetax, 2));  //Agncyamnt +
        lblServiceTax.Text = "Rs." + (Math.Round(servicetax, 2)).ToString("0.00");
        //    lbl_service_tax.Text = "Rs." + (Math.Round(AgencyServiceTax, 2)).ToString("0.00");
        //   lbl_total_amount_payable.Text = "Rs." + (Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString("0.00");
        //  txt_DD_Inspection.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total
        AmmountPaybale_all = Convert.ToDouble(Unitamnt + Agncyamnt + Math.Round(Servictax_all, 2));  //Agncyamnt +
        txt_amount.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));



        //  txtAmount_InspectionAgency.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal((Agncyamnt + Math.Round(AgencyServiceTax, 2)).ToString())));   //inspection amount total

        clsCompany clsObjN = new clsCompany();
        DataSet ds = new DataSet();


        ds = CategoryCast_Details_ByCompanyId(CompanyId);


        CategoryCastId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryCastId"]);
        //CategoryCastId = 5;
        //////////if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
        //////////{
        //////////    // tbInspectionAgencyPaymentDetails.Visible = false;

        //////////    //  lblAgencyAmnt.Text = "Rs." + "0.00";
        //////////    //lblTotAmnt.Text = "Rs." + "0.00";

        //////////    // AmountPayable =Convert.ToDouble("0.00");
        //////////    // lblServiceTax.Text = "Rs." + "0.00";
        //////////    //  lbl_service_tax.Text = "Rs." + "0.00";
        //////////    //  lbl_total_amount_payable.Text = "Rs." + "0.00";
        //////////    // txt_DD_Inspection.Text = "0.00";
        //////////    //  AmmountPaybale_all = Convert.ToDouble("0.00");
        //////////    //txt_amount.Text = "Rs." + "0.00";


        //////////    Double unitamount_SC_ST = 100.0;
        //////////    Double Servicetax_SC_ST = 18.0;
        //////////    lblUnitAmnt.Text = "Rs." + "100.00";
        //////////    lblServiceTax.Text = "Rs." + (Math.Round(Convert.ToDouble(Servicetax_SC_ST), 2)).ToString("0.00"); // "Rs." + "18.00";

        //////////    lblTotAmnt.Text = "Rs." + (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00"); // Registration Fee

        //////////    txt_amount.Text = (Math.Round(Convert.ToDouble(unitamount_SC_ST), 2) + Math.Round(Servicetax_SC_ST, 2)).ToString("0.00");


        //////////    //RequiredFieldValidator5.Enabled = false;
        //////////    //RequiredFieldValidator6.Enabled = false;
        //////////    //RequiredFieldValidator7.Enabled = false;
        //////////    //RequiredFieldValidator8.Enabled = false;
        //////////    //CValidatortxt_cheq1.Enabled = false;
        //////////    //RequiredFieldValidator9454.Enabled = false;


        //////////    //txt_chqddNo_inspection.Enabled = false;
        //////////    //txt_BankNam_inspection.Enabled = false;
        //////////    //txt_BankBranch_inspection.Enabled = false;
        //////////    //txt_PayableAt_inspection.Enabled = false;
        //////////    //txt_DD_Inspection.Enabled = false;
        //////////    //  txt_chqddate_inspection.Enabled = false;
        //////////    //  FileUpload2.Enabled = false;

        //////////}
        //////////else
        //////////{
        //////////    //RequiredFieldValidator5.Enabled = true;
        //////////    //RequiredFieldValidator6.Enabled = true;
        //////////    //RequiredFieldValidator7.Enabled = true;
        //////////    //RequiredFieldValidator8.Enabled = true;
        //////////    //CValidatortxt_cheq1.Enabled = true;

        //////////    //RequiredFieldValidator9454.Enabled = true;



        //////////    //txt_chqddNo_inspection.Enabled = true;
        //////////    //txt_BankNam_inspection.Enabled = true;
        //////////    //txt_BankBranch_inspection.Enabled = true;
        //////////    //txt_PayableAt_inspection.Enabled = true;
        //////////    //txt_DD_Inspection.Enabled = true;
        //////////    //  txt_chqddate_inspection.Enabled = true;
        //////////    // FileUpload2.Enabled = true;
        //////////}
        //////////// }
    }

    public void AcceptCompanies(int Unitid, int InsUserId)
    {
        try
        {
            objCompany = new clsCompany();
            //  objAmendment = new clsAmendment();
            //clsMasterMailer objMailer = new clsMasterMailer();
            waitcompAccepDel waitCompAccept = new waitcompAccepDel(objCompany.Company_WaitingCompany_Registration_Accept);

            //   string FromMail = string.Empty, ccMail = string.Empty, bccMail = string.Empty, subject = string.Empty, bodyMail = string.Empty;
            DataSet Myds = new DataSet();
            urlId = 1;
            if (urlId == 1)
            {

                //hashtbl = objCompany.Company_WaitingCompany_Registration_Accept(Unitid, UserId);
                //calling through delegate
                hashtbl = objCompany.Company_WaitingCompany_Registration_Accept(Unitid, InsUserId, UserId);
                emailid = hashtbl["email"].ToString();
                successId = Convert.ToInt32(hashtbl["ErrorId"].ToString());
            }

            /*  if (urlId == 2)
              {
                  objRenewal = new clsRenewal();
                  successId = objRenewal.CompanyRenewal_WaitingCompanies_Accept(Unitid,InsUserId, UserId);
              }*/

            //else if (urlId == 3)
            //{

            //    hashtbl = objAmendment.CompanyAmendment_WaitingCompany_Accept(Unitid, InsUserId, UserId);
            //    emailid = hashtbl["email"].ToString();
            //    successId = Convert.ToInt32(hashtbl["ErrorId"].ToString());
            //}

            //MAIL HAS TO HANDLE ACCORDING TO RENEWAL
            if (successId == 2)
            {

                Myds = new DataSet();
                objMailer = new clsMasterMailer();


                Myds = objMailer.MasterMailer_Fetch_DataSet(1); //Change Here
                MailTo = emailid; //Myds.Tables[0].Rows[0]["fGPtbl_MM_To"].ToString();  // hdnbranchmail  hdnBrEmail //
                MailFrom = UserEmailID; //Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
                ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
                bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
                subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
                bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +txtReason.Text + ".";


                string bodystr = string.Empty;
                if (hashtbl["CompanyName"].ToString() != null)
                {
                    string str = bodyMail.Replace("$$CompanyName$$", hashtbl["CompanyName"].ToString());
                    bodystr = str.Replace("$$Branchname$$", hashtbl["BranchName"].ToString());
                    //SendMail.Mail_WithOUTAttachment(emailid, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
                }


                bodyMail = "Dear" + ' ' + "Sir/Mam," + "<br/><br/>" + bodystr;
                sendmail(MailFrom, MailTo, bccMail, ccMail, subject, bodyMail);
                Session["companyAction"] = "Status changed successfully!";
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public static void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailMessage message = new MailMessage();
            message.From = new MailAddress(MailFrom);  // MailFrom
            // Set the recepient address of the mail message
            message.To.Add(new MailAddress(MailTo));

            // Check if the bcc value is null or an empty string
            if ((bccMail != null) && (bccMail != string.Empty))
            {
                // Set the Bcc address of the mail message
                message.Bcc.Add(new MailAddress(bccMail));
            }      // Check if the cc value is null or an empty value
            if ((ccMail != null) && (ccMail != string.Empty))
            {
                // Set the CC address of the mail message
                message.CC.Add(new MailAddress(ccMail));
            }       // Set the subject of the mail message
            message.Subject = subject;
            // Set the body of the mail message
            message.Body = bodyMail;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, subject, bodyMail, 1);

        }
        catch (Exception ex)
        {
            //ex.Message("Server Busy!!!!!!!!!");
        }
    }

    protected void rptFactory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        urlId = 1;
        objInsUser = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnunit_Id = (HiddenField)e.Item.FindControl("hdnFactId");
                string untid = hdnunit_Id.Value;

                LinkButton lnk_Cover_letter = (LinkButton)e.Item.FindControl("lnk_cover");
                string imageid = string.Empty;
                imageid = "CoverLetterofInspectionAgency.aspx?id=" + untid + "&option=" + urlId;
                Myds = objInsUser.MasterInspectionAgency_User_FetchByUnit_DataSet(Convert.ToInt32(hdnunit_Id.Value), 1); //1 for fresh registration
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddlInspUser = (DropDownList)e.Item.FindControl("ddlInspagency");
                    ddlInspUser.DataSource = Myds;// MyDsInspUser;
                    ddlInspUser.DataValueField = Myds.Tables[0].Columns["AgencyUserId"].ToString();
                    ddlInspUser.DataTextField = Myds.Tables[0].Columns["AgencyUsername"].ToString();
                    ddlInspUser.DataBind();
                }
                lnk_Cover_letter.Attributes.Add("onclick", "return newdocumentopen('" + imageid + "');");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            Myds.Dispose();
        }
    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Please wait.';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            int result = 0;
            result = UpdateApprovalStatus(Convert.ToInt32(CompanyId));

            Response.Redirect("Online_RenewalPaymentDetails.aspx?i=2");


            /*foreach (ListItem item in ChkListFactory.Items)
            {
                if (item.Selected)
                {
                    AcceptCompanies(Convert.ToInt32(item.Value));
                }
            }*/

            //foreach (RepeaterItem itm in rptFactory.Items)
            //{
            //    HiddenField hdnunitId = (HiddenField)itm.FindControl("hdnFactId");
            //    DropDownList ddlInsUserId = (DropDownList)itm.FindControl("ddlInspagency");
            //    AcceptCompanies(Convert.ToInt32(hdnunitId.Value), Convert.ToInt32(ddlInsUserId.SelectedValue));


            //    int result = 0;
            //    result = UpdateBranchConfirmation(Convert.ToInt32(hdnunitId.Value));
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        Response.Redirect("WaitingCompaniesPaymentDetailsOnline.aspx?i=1");

        //BINDING GRIDVIEW

        ////////if (urlId == 1)
        ////////{
        ////////    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
        ////////    GridViewDataBind(1);
        ////////}
        ////////else if (urlId == 3)
        ////////{
        ////////    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
        ////////    GridViewDataBind(3);
        ////////}

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

    ////protected void rbl_selectindexchanged_InspectionAgency(object sender, EventArgs e)
    ////{
    ////    //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
    ////    if (Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 2 || Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 3)
    ////    {
    ////        PanelChequeDD.Visible = true;
    ////        PanelInspectionOnlieRites.Visible = false;




    ////    }
    ////    else if (Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == 4)
    ////    {
    ////        PanelInspectionOnlieRites.Visible = true;
    ////        PanelChequeDD.Visible = false;



    ////    }

    ////}



    ////protected void rdbInspectionPaymentType_Online_selectindexchanged(object sender, EventArgs e)
    ////{

    ////    int AgencyId = 0;
    ////    DataSet dss = DataSetAgency_Details_ByUnitId(UnitId);
    ////    AgencyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_PD_AgencyId"].ToString());

    ////    //  AgencyId = 10;

    ////    if (AgencyId == 9)
    ////    {
    ////        HyperLink1.Visible = true;
    ////        //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
    ////        if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 1)
    ////        {
    ////            lbl_OnlineMsg.Visible = true;

    ////            lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Online_Payment_Form.aspx";

    ////            HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
    ////            HyperLink1.Target = "_blank";

    ////            lblOnlineNorthernMsg.Visible = true;
    ////            lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";

    ////        }
    ////        else if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 2)
    ////        {
    ////            lbl_OnlineMsg.Visible = true;


    ////            lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Bank%20Details%20NR.PDF";

    ////            HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
    ////            HyperLink1.Target = "_blank";

    ////            lblOnlineNorthernMsg.Visible = false;
    ////            // lblOnlineNorthernMsg.Text = "Other region can use only NEFT/RTGS option for payment";

    ////        }
    ////    }

    ////    else
    ////    {
    ////        HyperLink1.Text = ""; lbl_OnlineMsg.Text.ToString();
    ////        HyperLink1.Target = "_blank";

    ////        HyperLink1.Visible = false;

    ////        lblOnlineNorthernMsg.Visible = true;
    ////        lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";
    ////        lblOnlineNorthernMsg.Visible = false;
    ////    }

    ////}

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

                //  lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));

                lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));
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

                //  txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));

            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }
           // txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));


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

               // txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }
          //  txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));


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
        ////try
        ////{
        ////    if (FileUpload1.HasFile)
        ////    {
        ////        object postedFile = FileUpload1.PostedFile;
        ////        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
        ////        string fileExtn = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName);

        ////        if (filenameWithoutExtn != "")
        ////        {
        ////            string contentType = FileUpload1.PostedFile.ContentType.ToString();
        ////            int contentLength = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());
        ////            newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

        ////            int AllowedContentLengthInKB = 1000;
        ////            int AllowedContentLengthInBytes = AllowedContentLengthInKB * 1024;
        ////            if (contentLength > AllowedContentLengthInBytes)
        ////            {
        ////                Label1.Text = "Document size can't be greater than 1 MB.";
        ////                FileStatus_1 = 1;
        ////                return false;
        ////            }
        ////            else
        ////            {
        ////                Label1.Text = "";
        ////                FileStatus_1 = 2;
        ////            }

        ////            if (FileStatus_1 == 2)
        ////            {
        ////                FileUpload1.PostedFile.SaveAs(filePath + newFileName);
        ////            }
        ////        }
        ////    }
        ////}
        ////catch (Exception ex)
        ////{
        ////    ErrHandler.WriteError(ex.Message);
        ////}
        return true;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        Response.Redirect("Online_BAPendingMonetaryLimit.aspx?i=2");
    }

    public int UpdateApprovalStatus(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_RenewalPaymentDetails_Online_renewal", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;



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

    private bool FunUploadFile_2()
    {
        //////try
        //////{
        //////    if (FileUpload2.HasFile)
        //////    {
        //////        object postedFile = FileUpload2.PostedFile;
        //////        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        //////        string fileExtn = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName);

        //////        if (filenameWithoutExtn != "")
        //////        {
        //////            string contentType = FileUpload2.PostedFile.ContentType.ToString();
        //////            int contentLength = Convert.ToInt32(FileUpload2.PostedFile.ContentLength.ToString());
        //////            NewFileName_inspection = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

        //////            int AllowedContentLengthInKB = 1000;
        //////            int AllowedContentLengthInBytes = AllowedContentLengthInKB * 1024;
        //////            if (contentLength > AllowedContentLengthInBytes)
        //////            {
        //////                Label2.Text = "Document size can't be greater than 1 MB.";
        //////                // FileStatus_1 = 1;
        //////                FileStatus_1_inspection = 1;
        //////                return false;
        //////            }
        //////            else
        //////            {
        //////                Label2.Text = "";
        //////                FileStatus_1_inspection = 2;
        //////            }

        //////            if (FileStatus_1_inspection == 2)
        //////            {
        //////                FileUpload2.PostedFile.SaveAs(NewFileName_inspection);
        //////            }
        //////        }
        //////    }
        //////}
        //////catch (Exception ex)
        //////{
        //////    ErrHandler.WriteError(ex.Message);
        //////}
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