
using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_UnitAmendmentStep1_CFSS :  System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId, Edit;
    string UserFullName;

    clsAmendment objAmendment;
    clsCompany objCompany;
    clsRenewal objRenewal;
    int ErrorId, CompanyId, UnitId, Formtypeid, BranchId;
    int fStatus;
    public int unitbrAction = 0;
    short tabindx = 9;

    string OriginalFilePath;
    string filePath;
    int FileStatus_1;
    string newFileName = "";
    DataRow[] cityRows;
    string expression = string.Empty;
    DataTable ddlCityClone = new DataTable();
    DataTable MydtState = new DataTable();
    DataSet Myds;

    int Fee_Id = 0;
    string Fee_Slab = string.Empty;
    double AmountPayable;
    double AmountPayableST;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    DataTable dt;


    DataTable Mydtcity;

    int Constitutionid = 0;

    DataSet AllstepchangeDs = new DataSet();
    DataRow[] AllstepchangeRow;

    int CategoryCastId = 0;

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

        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        if (!string.IsNullOrEmpty(Request.QueryString["c"]))
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            Formtypeid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
                         

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

            }

            

        }
        else
        {
            trMainData.Visible = false;
            
        }

        try
        {
            if (!Page.IsPostBack)
            {              
                FunFillDataset();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
        {
            string ChkStatus = "";
            ChkStatus = Request.QueryString["Status"];
            if (ChkStatus == "1")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
            }
            else if (ChkStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Please Contact Administrator.');", true);
            }
        }
    }
    protected void FunFillDataset()
    {
        try
        {
            if (!IsPostBack)
            {
                objAmendment = new clsAmendment();
                Myds = new DataSet();
                DataTable MydtCompany, MydtFactory, MydtBranch = new DataTable();

                //TO FETCH DATA SET VALUE
                Myds = objAmendment.Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
                //Myds = objAmendment.Company_UnitDetails_Fetch_ByCompanyId(97);

                //ASSIGNING IN MULTIPLE TABLES
                MydtCompany = Myds.Tables[0];
                MydtBranch = Myds.Tables[1];
                MydtFactory = Myds.Tables[2];

                //FILLING UP ALL DROPDOWN LIST
                funfill_ddlCategory();

                //FILLING UP ALL CONTROLS
                FunFillcontrols(MydtCompany, MydtFactory);

                

                string fPANno = "";
                string strUAM = "";// fGtbl_Company_AdharNumber=""
                fPANno = Myds.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
                strUAM = Myds.Tables[0].Rows[0]["fGtbl_Company_AdharNumber"].ToString();

                ////Writedatainxml(); 
                //DataSet dsCons = new DataSet();
                //dsCons = FetchConstitution_By_CompanyPAN(fPANno);
                //int Constitution_id = 0;
                //Constitution_id = Convert.ToInt32(dsCons.Tables[0].Rows[0]["fGPtbl_ConstitutionId"].ToString());
                 
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void FunFillcontrols(DataTable CompanyTable, DataTable FactoryTable)
    {
        try
        {
             
            if (CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString() == "")
            {
                txt_1.Text = "";
            }
            else
            {
                txt_1.Text = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString();
                txt_1.Attributes.Add("Readonly", "Readonly");
            }

            int ConstitutionId = 0;
            int Castid = 0;
            int CastidArea = 0;
                int UnitSize = 0;
            if (CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString()!="")
            {
                ConstitutionId = Convert.ToInt32(CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString());
                ddlConstitution.SelectedValue = ConstitutionId.ToString();
                DropDownListBranch.SelectedValue = BranchId.ToString();

                Castid = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString());
                CastidArea = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString());
                UnitSize = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString());

                ddl_1e.SelectedValue = Castid.ToString();
                ddl_1d.SelectedValue = CastidArea.ToString();
                ddl_1f.SelectedValue = UnitSize.ToString();

            }

            txt_1p1.Text = CompanyTable.Rows[0]["fGPtbl_Company_Name"].ToString();
             
            if (CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString() == "")
            {
                txtproposaldate.Text = "";
            }
            else
            {
                txtproposaldate.Text = CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
            }

            if (CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString() == "")
            {
                txt_aahar.Text = "";
            }
            else
            {
                txt_aahar.Text = CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
                txt_aahar.Attributes.Add("Readonly", "Readonly");
            }
             


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    private void funfill_ddlCategory()
    {
        try
        {
            //Category Area DROPDOWNLIST
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsDDL.ddlCategoryArea();
            ddl_1d.DataSource = dt;
            ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
            ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
            ddl_1d.DataBind();
            ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));

            //Category Cast DROPDOWNLIST
            dt.Clear();
            dt = clsDDL.ddlCategoryCast();
            ddl_1e.DataSource = dt;
            ddl_1e.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
            ddl_1e.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
            ddl_1e.DataBind();
            ddl_1e.Items.Insert(0, new ListItem("--Select Enterprise Social Category--", ""));

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
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnCalculateFeess_Click(object sender, EventArgs e)
    {
        pnlMain.Visible = true;
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

        //trbranch1.Visible = true;
        //trMainData.Visible = true;


        Loadpaymentgrid(categoryunit, CatArea, CatCast);

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
            AmountPayableST = ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100);



            lblUnitAmnt.Text = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString())).ToString();
            lblServiceTax.Text = ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100).ToString();
            lblTotAmnt.Text = (Convert.ToDouble(lblUnitAmnt.Text) + Convert.ToDouble(lblServiceTax.Text)).ToString();


            if (ddl_1e.SelectedValue == "10" || ddl_1e.SelectedValue == "11")
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = (Convert.ToDouble(lblUnitAmnt.Text) + Convert.ToDouble(lblServiceTax.Text)).ToString();
            }




            //  txt_chqddAmnt.Text = (Convert.ToDouble(lblUnitAmnt.Text) + Convert.ToDouble(lblServiceTax.Text)).ToString();

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
    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {

        trbranch1.Visible = true;
        trMainData.Visible = true;
        pnlMain.Visible = true;

        double AmountPayable_A = 0;
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
            AmountPayable_A = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString())) + ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100);

        }



        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)
        {           

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
             
            Session["PaymentType"] = "2";
             
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable_A.ToString()));
            }
            pnlChqDD.Visible = true;
        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
           
            Session["PaymentType"] = "3";
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable_A.ToString()));
            }
            pnlChqDD.Visible = true;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 4)
        {
            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {
                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable_A.ToString()));
            }
            Session["PaymentType"] = "4";
            pnlChqDD.Visible = true;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        int tblId = 0;
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
        PaymentType =Convert.ToInt32(radPay.SelectedValue);// Convert.ToInt32(Session["PaymentType"].ToString());
        Decimal ftLastYearTurnOver = 0, UnitAmount = 0, UnitServicetax = 0;
        string dtProposalDate = null;
        int intBranchId = 0;
        int intCastId = 0;
        int intSpecCastid = 0, intConstitutionId=0, intunitsize = 0;

        if (txtTurnover.Text.Trim().ToString() != "")
        {
            ftLastYearTurnOver = Convert.ToDecimal(txtTurnover.Text.Trim().ToString());
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

        try
        {
            if (ddlConstitution.SelectedIndex > 0)
            {
                intConstitutionId = Convert.ToInt32(ddlConstitution.SelectedValue);
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

        if (lblUnitAmnt.Text.Trim().ToString() != "")
        {
            UnitAmount = Convert.ToDecimal(lblUnitAmnt.Text.Trim().ToString());
        }
        if (lblServiceTax.Text.Trim().ToString() != "")
        {
            UnitServicetax = Convert.ToDecimal(lblServiceTax.Text.Trim().ToString());
        }
        if (txtproposaldate.Text.Trim().ToString() != "")
        {
            dtProposalDate = txtproposaldate.Text.Trim().ToString();
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



        int CFSSUNITID = 25971;
        try
        {
            tblId = Company_Insert_PaymentDetails_CFSS(CompanyId, 3,
           PaymentType, ChqDdNo, BankName, BranchName, PaybalAt, AmountPayable, AmountPayableST, Fee_Id,
           Fee_Slab, newFileName, OriginalFilePath, ChqDdate, PaymentRemarks, ftLastYearTurnOver, dtProposalDate,

           intSpecCastid, intCastId, intunitsize, intBranchId, intConstitutionId,

            Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername,
            CFSSUNITID
           );

            int K = objCompany.Company_insert_turnover_limit(CompanyId, 3, Convert.ToDecimal(txtTurnover.Text.ToString()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }


        #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB
        int FormType = 4; // 4 for Amendment

        string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        CFSSService service = new CFSSService();
        string result = service.ProcessPayment(CompanyId, FormType, apiUrl, "Amendment");
        if (result == "SUCCESS")
        {
            if (tblId == 1)
            {
                Response.Redirect("UnitAmendmentStep1_CFSS.aspx?Status=1");
            }
            else if (tblId == 2)
            {
                Response.Redirect("UnitAmendmentStep1_CFSS.aspx?Status=2");
            }
            
        }
        else
        {

            lblErrorMsgs.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
            lblErrorMsgs.ForeColor = System.Drawing.Color.Red;
        }

         

        #endregion



        //int intBranchId = 0;
        //int intConst = 0;

        //int intCastId = 0;
        //int intSpecCastid = 0, intunitsize = 0;



        ////if (tblId == 1)   //INSERT/UPDATE SUCCESSFULL
        ////{
        ////    Response.Redirect("UnitAmendmentStep1_CFSS.aspx?Status=1");
        ////}
        ////else //INSERT/UPDATE FAIL
        ////{
        ////    Response.Redirect("UnitAmendmentStep1_CFSS.aspx?Status=2");
        ////}


    }

    public int Company_Insert_PaymentDetails_CFSS(int CompanyId, int FormType, int PaymentType, string CHQDD_UTR,
        string BankName, string BranchName, string PayableAt, double Amount, double Servicetax, int Feeid, string FeeDesc, string FileName,
        string FilePath, string PaymentDate, string Remarks, decimal ftLastYearTurnOver, string ProposalDate,
         int SpclCategory, int Castid, int UnitSizeId, int Branchid,int ConstitutionId,

         string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId, string Bank_Ledgername,
         int CFSSUnitID
        )
    {
        int tblId = 0;
        try
        {

            MySqlComm = new SqlCommand("GPsp_Company_Insert_Amnd_PaymentDetails_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_PD_CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@FormType", SqlDbType.Int).Value = FormType;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = PaymentType;
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
            MySqlComm.Parameters.Add("@fGPtbl_PD_ChequeDDate", SqlDbType.DateTime).Value = PaymentDate;
            MySqlComm.Parameters.Add("@PaymentRemarks", SqlDbType.VarChar).Value = Remarks;

            MySqlComm.Parameters.Add("@proposaldate", SqlDbType.DateTime).Value = ProposalDate;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = SpclCategory;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = Castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = UnitSizeId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = Branchid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C123_ConstitutionId", SqlDbType.Int).Value = ConstitutionId;

            MySqlComm.Parameters.Add("@ftLastYearTurnOver", SqlDbType.Decimal).Value = ftLastYearTurnOver;


            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;
            MySqlComm.Parameters.Add("@CFSSUnitID", SqlDbType.Int).Value = CFSSUnitID;



            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            tblId = Convert.ToInt32(outError.Value);
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
        return tblId;
    }
}