using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

public partial class appAdministrator_UnitRenewalStep6 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string UnitIdMultiple = string.Empty;
    string[] SI;
    int i = 1;
    clsCompany objCompany;
    clsAmendment objAmendment;
    clsRenewal objRenewal;
    string CnCount = string.Empty;
    int Urlid;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    public delegate int step6delegate(int UnitId, int FeeId,  int FormTypeId, int UserId, decimal Amount);
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

        try
        {
            FunRequest();
            //TO CHECK WHETHER LAST REGISTRATION FEE PAID OR NOT
            objRenewal = new clsRenewal();
            if (!IsPostBack)
            {
                int unitamount = objRenewal.CompanyRenewal_CheckPaymentSlabFee_ByCompanyId_Step6(CompanyId);
                //if (unitamount == 0)//REGISTRATION FEE NOT AVAILABLE
                //{
                //    //SHOW MODAL POPUP
                //    ModalPopupExtender1.Show();
                //    pnlInnerEdit.DefaultButton = "btnSubmitEdit";
                //}
                //else
                //{
                //    FunfillPaymentSlab(0.0);
                //}
                ModalPopupExtender1.Show();
                pnlInnerEdit.DefaultButton = "btnSubmitEdit";
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    private void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            Urlid=Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["i"].Replace(" ", "+")).ToString());

            //c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlId
        }
        catch
        {
            CompanyId = 0;
            //UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }

    private void FunfillPaymentSlab(double amount)
    {
        TableRow trow;
        TableCell tcell;
        int Cnt = 0;
        System.Data.SqlClient.SqlDataReader MyDr;
        objCompany = new clsCompany();
        objRenewal = new clsRenewal();
        DataTable Dt_Renewal = new DataTable();
        string companystatus = string.Empty;

        try
        {
            Dt_Renewal = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
            if (Dt_Renewal.Rows.Count > 0)
            {
                companystatus = Dt_Renewal.Rows[0]["fGPtbl_Company_Status"].ToString();
            }

            MyDr = objRenewal.CompanyRenewal_FetchPaymentSlabFee_ByCompanyId_Step6(CompanyId,amount);

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
                    //TO STORE FORMTYPE ID 8:EXPIRED, 2:RENEWAL
                    HDNFormtype.Value = companystatus; // MyDr["fGPtbl_MFee_FormTypeId"].ToString();
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

                tcell = new TableCell();
                tcell.Text = "<b>Rs. " + MyDr["fGPtbl_MFee_Amount"].ToString() + "</b>";
                HdnFieldAmount.Value = MyDr["fGPtbl_MFee_Amount"].ToString();
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
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int tblId = 0;

        string bsRDreg = "";
        int FeeId = 0;

        string bsRDagency = "";
        char[] separator = new char[] { '/' };
        string[] strSplitArr;
        string paymentid = "";

        try
        {

            objCompany = new clsCompany();
            objRenewal = new clsRenewal();
            step6delegate step6del = new step6delegate(objRenewal.CompanyRenewal_Insert_Step6);

            System.Data.DataTable dt = new System.Data.DataTable();

            dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);

            int cnt = 0;

            foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
            {
                //TO INPUT UNIT AMOUNT ONCE IN PAYMENT DETAIL TABLE
                cnt++;

                int a = 0;
                foreach (var item in row.ItemArray) // Loop over the items.
                {

                    if (a == 1)
                    {

                    }
                    else if (a == 0)
                    {
                        UnitId = Convert.ToInt32(item);
                        bsRDreg = Request["rdReg"].ToString();
                        FeeId = Convert.ToInt32(bsRDreg);


                        if (cnt == 1)
                        {
                            //TO USE DELEGATE TO CALL THIS FUNCTION
                            if (Convert.ToInt32(HDNFormtype.Value) == 1)
                            {//RENEWAL
                                tblId = step6del(UnitId, FeeId, 2, UserId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            else if (Convert.ToInt32(HDNFormtype.Value) == 4)
                            {//EXPIRED COMPANY
                                tblId = step6del(UnitId, FeeId, 2, UserId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            paymentid = tblId.ToString();
                        }
                        else
                        {
                            if (Convert.ToInt32(HDNFormtype.Value) == 1)
                            {//RENEWAL
                                tblId = step6del(UnitId, 0, 2, UserId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            else if (Convert.ToInt32(HDNFormtype.Value) == 4)
                            {//EXPIRED COMPANY
                                tblId = step6del(UnitId, 0, 2, UserId, Convert.ToDecimal(HdnFieldAmount.Value));
                            }
                            paymentid = paymentid + "," + tblId;
                        }

                    }
                    a++;
                }

            }

            dt.Clear();
            dt.Dispose();


        }
        catch
        {
            FeeId = 0;


        }      

        if (tblId != 0)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString()); //UnitId.ToString()
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptPaymentDetailId = objDecEnc.Encrypt(paymentid.ToString());

            string EncryptFormtype = objDecEnc.Encrypt("2");

            Response.Redirect("UnitRenewalStep7.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype
                + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pd=" + EncryptPaymentDetailId, false);
        }

    }

    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
      //  FunfillPaymentSlab(Convert.ToDouble(txtRegfee.Text.Trim()));
      //  ModalPopupExtender1.Hide();
        Loadpaymentgrid();
    }

    private void Loadpaymentgrid()
    {
        clsRenewal objrenew = new clsRenewal();
        SqlDataReader Mydr = objrenew.Company_FetchSlabFee_Renewal_Step6(Convert.ToDecimal(txtRegfee.Text.Trim()), CompanyId);

        DataSet ds = new DataSet();
        ds = Company_Fetch_Datasetrenew_Covid19Discount(CompanyId);

      int CovidExistStatus= Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyExits"].ToString());

        if(CovidExistStatus == 240)
        {
            lblMsg_Covid19.Text = "Unit is getting 50% discount on Renewal fee";
        }
        else
        {
            lblMsg_Covid19.Text = " ";
        }
        //  FunfillPaymentSlab(Mydr);
        FunfillPaymentSlabrenewal(Mydr);
        ModalPopupExtender1.Hide();

        try
        {

            objCompany = new clsCompany();
            int K = objCompany.Company_insert_turnover_limit(CompanyId, 2, Convert.ToDecimal(txtRegfee.Text.ToString()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }


    public DataSet Company_Fetch_Datasetrenew_Covid19Discount(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Covid19Discount", DBManager.ConnectToDB());
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

    private void FunfillPaymentSlabrenewal(SqlDataReader MyDr)
    {
        TableRow trow;
        TableCell tcell;
        int Cnt = 0;
        /*System.Data.SqlClient.SqlDataReader MyDr;
        objCompany = new clsCompany();
        MyDr = objCompany.Company_FetchBranchPayment_ByFormtypeId_Step6(1);*/
        DataTable Dt_Renewal;
        string companystatus = string.Empty;
        Dt_Renewal = objRenewal.Company_Fetch_CompanyStaus(CompanyId);
        if (Dt_Renewal.Rows.Count > 0)
        {
            companystatus = Dt_Renewal.Rows[0]["fGPtbl_Company_Status"].ToString();
        }

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
                HDNFormtype.Value = companystatus;
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

            tcell = new TableCell();
            tcell.Text = "<b>Rs. " + MyDr["fGPtbl_MFee_Amount"].ToString() + "</b>";
            HdnFieldAmount.Value = MyDr["fGPtbl_MFee_Amount"].ToString();
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
}
