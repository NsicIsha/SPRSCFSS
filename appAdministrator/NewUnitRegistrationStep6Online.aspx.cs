using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Configuration;
using System.Data;
using System.Data.Linq;

public partial class appAdministrator_NewUnitRegistrationStep6Online : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, PaymentDetailId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string UnitIdMultiple = string.Empty;
    string[] SI;
    int i = 1;
    clsCompany objCompany;
    int ErrorId;
    string CnCount = string.Empty;
    DataSet ds;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int fStatus = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        //////////Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////////try
        ////////{
        ////////    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        ////////    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        ////////    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        ////////    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        ////////}
        ////////catch (Exception ex)
        ////////{
        ////////    UserId = 0;
        ////////    RoleId = 0;
        ////////    DepartmentId = 0;
        ////////    UserFullName = "";
        ////////}
        //////////End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        ////////try
        ////////{
        ////////    if (!IsPostBack)
        ////////    {
        ////////        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        ////////        CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
        ////////        //   ModalPopupExtender2.Show();
        ////////        Loadpaymentgrid();
        ////////    }

        ////////    FunRequest();
        ////////    //FunfillPaymentSlab();

        ////////    objCompany = new clsCompany();

        ////////    System.Data.DataTable dt = new System.Data.DataTable();
        ////////    dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);  // Commented on 27.11.2019

        ////////    //  dt = Company_Fetch_FactoryLocation(CompanyId);
        ////////    if (dt.Rows.Count > 0)
        ////////    {
        ////////        CnCount = dt.Rows.Count.ToString();
        ////////    }
        ////////    int RecordCount = 0;


        ////////    foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
        ////////    {
        ////////        int a = 0;
        ////////        int RecordCheck = 0;
        ////////        foreach (var item in row.ItemArray) // Loop over the items.
        ////////        {
        ////////            string address = "";
        ////////            if (a == 1)
        ////////            {
        ////////                ds = new DataSet();


        ////////                address = Convert.ToString(item);
        ////////                FunfillAgency(address, UnitId);
        ////////                ////ds = Check_Company_Check_Address(UnitId, address, CompanyId);
        ////////                ////RecordCheck = Convert.ToInt32(ds.Tables[0].Rows[0]["Checkresult"].ToString());

        ////////                ////if (RecordCount == 0 && RecordCheck == 420)
        ////////                ////{
        ////////                ////    FunfillAgency(address, UnitId);
        ////////                ////    RecordCount = 1;
        ////////                ////}
        ////////                ////else if (RecordCheck == 240)
        ////////                ////{
        ////////                ////    FunfillAgency(address, UnitId);
        ////////                ////}

        ////////            }
        ////////            else if (a == 0)
        ////////            {
        ////////                UnitId = Convert.ToInt32(item);
        ////////                UnitIdMultiple += UnitId.ToString() + ',';
        ////////                hdn_UnitID.Value = UnitIdMultiple;
        ////////                string AllValue = hdn_UnitID.Value;
        ////////                string sub = AllValue.Substring(0, AllValue.Length - 1);
        ////////                SI = sub.Split(',');
        ////////                hdn_unitID_Count.Value = SI.Length.ToString();
        ////////            }
        ////////            a++;
        ////////        }
        ////////    }

        ////////    dt.Clear();
        ////////    dt.Dispose();

        ////////}
        ////////catch (Exception ex)
        ////////{
        ////////    ErrHandler.WriteError(ex.Message);
        ////////}
    }
    public DataSet Check_Company_Check_Address(int UnitId, string Address, int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Check_Address", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@CunitId", SqlDbType.Int).Value = UnitId;
            MyAdp.SelectCommand.Parameters.Add("@Address", SqlDbType.VarChar).Value = Address;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    public DataTable Company_FetchManufacturingAddress(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByCompanyId_Manufacturing", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }
    public DataTable Company_Fetch_FactoryLocation(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByCompanyId_ForSingleFee", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;

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
    public DataSet Company_FetchServicesAddress(int CompanyId, int Type)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByCompanyId_Services", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.SelectCommand.Parameters.Add("@Type", SqlDbType.Int).Value = Type;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    private void FunRequest()
    {



        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            DataSet ds = new DataSet();
            ds = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
            fStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
            CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());
            UnitId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString());
            Status = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString());
            BranchId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_CUnit_BranchId"].ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }
    private void FunfillPaymentSlab(SqlDataReader MyDr)
    {
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

            tcell = new TableCell();
            tcell.Text = "<b> Rs. " + MyDr["fGPtbl_MFee_Amount"].ToString() + "</b>";
            HdnFieldTurnover.Value = MyDr["fGPtbl_MFee_Amount"].ToString();
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
    private void FunfillAgency(string Address, int unitid)
    {
        clsMasterInspectionAgency objAgency = new clsMasterInspectionAgency();
        try
        {
            int AgencyId = 0;
            int SlabId = 0;
            int tablesub = 1;
            TableRow trow;
            TableCell tcell;
            int Cnt = 0;
            System.Data.SqlClient.SqlDataReader MyDr;
            objCompany = new clsCompany();
            //MyDr = objCompany.Company_FetchAgency_ByBranch_Step6(BranchId);
            //MyDr = objAgency.MasterInspectionAgency_User_Fetch_DataReader();
            string query = "select fGPtbl_IsAyush from [GPtbl_Company] where [fGPtbl_Company_Id]='" + CompanyId + "'";
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();


            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            bool result = false;
            while (reader.Read())
            {
                result = (reader[0].ToString().Equals("True")) ? true : false;
            }
            MyDr = objAgency.MasterInspectionAgency_User_Fetch_step6(result);
            //int i = 1;

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
                    tcell.Text = "Select inspection agency for " + Address;
                    //for (int i = 1; i < Convert.ToInt32(CnCount); i++)
                    //{
                    tcell.ID = "SN0id" + i.ToString();
                    i++;
                    //}
                    tcell.CssClass = "clstblCellHead";
                    tcell.ColumnSpan = 3;
                    trow.Cells.Add(tcell);

                    tbl_1.Rows.Add(trow);

                    trow = new TableRow();
                    trow.CssClass = "dynRowHeader";

                    tcell = new TableCell();
                    tcell.Text = "S.<br/>No.";

                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = "Inspection Agency";
                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = "Payment Slab";
                    tcell.CssClass = "clstblCellHead";
                    trow.Cells.Add(tcell);

                    tbl_1.Rows.Add(trow);
                }



                Cnt += 1;
                AgencyId = Convert.ToInt32(MyDr["AgencyId"].ToString());

                trow = new TableRow();
                trow.Style.Add("padding-top", "10px");

                tcell = new TableCell();
                tcell.Text = Cnt.ToString() + ".";
                tcell.Width = Unit.Percentage(5);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                tcell = new TableCell();
                tcell.Text = "<b>" + MyDr["AgencyName"].ToString() + "</b><br />";
                tcell.Text += MyDr["fGPtbl_TIAB_Instructions"];
                tcell.Text = tcell.Text.ToString().Replace("\r\n", "<br />");
                tcell.Width = Unit.Percentage(40);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                Table tbl = new Table();
                tbl.CellPadding = 0;
                tbl.CellSpacing = 0;
                tbl.Width = Unit.Percentage(100);
                TableRow tblRow;
                TableCell tblCell;
                clsCompany objCmp = new clsCompany();
                System.Data.SqlClient.SqlDataReader Dr;
                Dr = objCmp.Company_FetchAgencyPaymentSlab_Step6(AgencyId, 1);
                int Cntr = 0;
                while (Dr.Read())
                {
                    Cntr += 1;
                    //++++++++++++++++++++++
                    if (Cntr != 1)
                    {
                        tblRow = new TableRow();
                        tblCell = new TableCell();
                        tblCell.Text = "";

                        tblCell.ColumnSpan = 3;
                        tblCell.Width = Unit.Percentage(100);
                        tblCell.Height = Unit.Pixel(1);
                        tblCell.BackColor = System.Drawing.Color.FromName("#22518F");
                        tblRow.Cells.Add(tblCell);
                        tbl.Rows.Add(tblRow);
                    }

                    //++++++++++++++++++++++
                    tblRow = new TableRow();

                    SlabId = Convert.ToInt32(Dr["fGPtbl_IPS_Id"].ToString());
                    string rdVal = AgencyId.ToString() + "/" + SlabId.ToString();
                    tblCell = new TableCell();
                    tblCell.Text = "<input id='rd_" + unitid + "' name='rd_" + unitid + "' value='" + rdVal + "' type='radio' />";
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Text = "<b>" + Dr["fGPtbl_IPS_SlabName"].ToString() + "</b>";
                    tblCell.Width = Unit.Percentage(80);
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    tblCell.Text = "<b>Rs. " + Dr["fGPtbl_IPS_Amount"].ToString() + "</b>";
                    tblCell.Width = Unit.Percentage(20);
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);

                    tbl.Rows.Add(tblRow);

                    //++++++++++++++++++++++
                    tblRow = new TableRow();
                    tblCell = new TableCell();
                    tblCell.Text = "";
                    tblRow.Cells.Add(tblCell);

                    tblCell = new TableCell();
                    string slbInst = Dr["fGPtbl_IPS_Instructions"].ToString();
                    slbInst = FormInputReplace.FormStrInputReplace(slbInst);
                    slbInst = slbInst.ToString().Replace("\r\n", "<br />");
                    if (slbInst.Length > 150)
                    {
                        tblCell.Text = slbInst.Substring(0, 150) + "...";
                    }
                    else
                    {
                        tblCell.Text = slbInst;
                    }

                    tblCell.Attributes.Add("onmouseover", "Tip('" + slbInst + "')");
                    tblCell.Attributes.Add("onmouseout", "UnTip()");
                    tblCell.ColumnSpan = 2;
                    tblCell.CssClass = "clstblCell";
                    tblRow.Cells.Add(tblCell);
                    tbl.Rows.Add(tblRow);
                }
                Dr.Close();

                tcell = new TableCell();
                tcell.Controls.Add(tbl);
                tcell.Width = Unit.Percentage(55);
                tcell.CssClass = "clstblCell";
                trow.Cells.Add(tcell);

                tbl_1.Rows.Add(trow);

                TableRow trowMain = new TableRow();
                TableCell tcellMain = new TableCell();
                tcellMain.Controls.Add(tbl_1);
                trowMain.Controls.Add(tcellMain);
                Table1.Controls.Add(trowMain);
            }
            MyDr.Close();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        FunRequest();


        int tblId = 0;
        string bsRDreg = "";
        int FeeId = 0;
        int AgencyId = 0;
        int SlabId = 0;
        string bsRDagency = "";
        char[] separator = new char[] { '/' };
        string[] strSplitArr;
        string paymentid = "";
        try
        {
            MyDr = Company_Fetch_ByPANno_Step1(txtPan.Text, txtUAM.Text);
            //////}

            int i = 0;

            while (MyDr.Read())
            {
                if (i == 0)
                {
                    i = i + 1;
                    CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                }


            }

            objCompany = new clsCompany();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);


            int cnt = 0;

            foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
            {
                int a = 0;
                foreach (var item in row.ItemArray) // Loop over the items.
                {
                    string address = "";
                    cnt++;
                    if (a == 1)
                    {
                    }
                    else if (a == 0)
                    {
                        UnitId = Convert.ToInt32(item);
                        bsRDreg = Request["rdReg"].ToString();
                        FeeId = Convert.ToInt32(bsRDreg);
                        bsRDagency = Request["rd_" + UnitId].ToString();
                        strSplitArr = bsRDagency.Split(separator);
                        AgencyId = Convert.ToInt32(strSplitArr[0].ToString());
                        SlabId = Convert.ToInt32(strSplitArr[1].ToString());

                        objCompany = new clsCompany();
                        if (cnt == 1)
                        {
                            tblId = objCompany.Company_Insert_Step6(UnitId, FeeId, AgencyId, SlabId, 1, UserId, BranchId, Convert.ToDecimal(HdnFieldTurnover.Value));
                            paymentid = tblId.ToString();
                        }
                        else
                        {
                            tblId = objCompany.Company_Insert_Step6(UnitId, 0, AgencyId, SlabId, 1, UserId, BranchId, Convert.ToDecimal(HdnFieldTurnover.Value));
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
            AgencyId = 0;
            SlabId = 0;
        }

        if (tblId != 0)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptPaymentDetailId = objDecEnc.Encrypt(paymentid.ToString());
            string EncryptFormtype = objDecEnc.Encrypt("1");
            // Response.Redirect("NewUnitRegistrationStep7.aspx?c=" + EncryptCompany +"&f="+ EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pd=" + EncryptPaymentDetailId, false);
            Response.Redirect("NewUnitregistration_paymentdetails_step7Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&pd=" + EncryptPaymentDetailId, false);
        }
    }
    public DataSet CheckCompanyPaymentStatus(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_Company_All_Online_New_CheckConditionToAvoidMultiplePaymentEntries", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        DataSet dss = new DataSet();

        int intEnterpriseType = 0;

        if (object.Equals(Session["CompanyId"], null))
        {
            if (txtPan.Text != "" && txtUAM.Text != "")
            {
                // DataSet dss = new DataSet();
                dss = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
                fStatus = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
                CompanyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

                intEnterpriseType= Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString());

                Session["CompanyId"] = CompanyId;
            }
        }
        else
        {
            CompanyId = Convert.ToInt32(Session["CompanyId"]);
        }

        if (Convert.ToDecimal(txtTurnover.Text) > 0)
        {

            //if (object.Equals(Session["CompanyId"], null))
            //{
            //    if (txtPan.Text != "" && txtUAM.Text != "")
            //    {
            //        DataSet dss = new DataSet();
            //        dss = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
            //        fStatus = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
            //        CompanyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

            //        Session["CompanyId"] = CompanyId;
            //    }
            //}
            //else
            //{
            //    CompanyId = Convert.ToInt32(Session["CompanyId"]);

            //}


            DataSet ds = new DataSet();
            int PaymentStatus = 0;
            ds = CheckCompanyPaymentStatus(CompanyId);
            PaymentStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["PaymentStatus"].ToString());

            if (PaymentStatus == 240)
            {
                lblpopmsg.Text = "Unit is available in Branch login for payment verification. Payment Can not be added again.";  // negative value
                ModalPopupExtender2.Show();
                return;
            }
            else
            {
                if (dss.Tables.Count > 0 && Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString()) == 1)
                {
                    lblpopmsg.Text = "Payment data is already present. Payment Can not be added again.";
                    ModalPopupExtender2.Show();
                    return;
                }

            }
        }
        else
        {
            lblpopmsg.Text = "Please Enter Positive Turnover value";  // negative value
            ModalPopupExtender2.Show();
            return;
        }

        Double Sum;
        Sum = Convert.ToDouble(txtTurnover.Text);


        if (intEnterpriseType == 3)
        {
            if (Sum > 500 || Sum == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Micro : Last Year Turnover Should be up to ₹5 crore.');", true);
                return;

            }
        }

        if (intEnterpriseType == 4)
        {
            if (Sum > 5000 || Sum < 500)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Small : Last Year Turnover Should be up to ₹50 crore and It should be more than 5 Crore.');", true);
                return;
            }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: More than 25 lakh rupee but not exceed 5 core');", true);
            //    return;
            //}
        }



        Loadpaymentgrid();


        trInspectionAgency.Visible = true;
        trPaymentSlab.Visible = true;
        trButton.Visible = true;



        ////FunRequest();
        //////FunfillPaymentSlab();

        objCompany = new clsCompany();



        System.Data.DataTable dt = new System.Data.DataTable();
        dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);  // Commented on 27.11.2019

        //  dt = Company_Fetch_FactoryLocation(CompanyId);
        if (dt.Rows.Count > 0)
        {
            CnCount = dt.Rows.Count.ToString();
        }
        int RecordCount = 0;


        foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
        {
            int a = 0;
            int RecordCheck = 0;
            foreach (var item in row.ItemArray) // Loop over the items.
            {
                string address = "";
                if (a == 1)
                {
                    ds = new DataSet();
                    address = Convert.ToString(item);
                    FunfillAgency(address, UnitId);
                }
                else if (a == 0)
                {
                    UnitId = Convert.ToInt32(item);
                    UnitIdMultiple += UnitId.ToString() + ',';
                    hdn_UnitID.Value = UnitIdMultiple;
                    string AllValue = hdn_UnitID.Value;
                    string sub = AllValue.Substring(0, AllValue.Length - 1);
                    SI = sub.Split(',');
                    hdn_unitID_Count.Value = SI.Length.ToString();
                }
                a++;
            }
        }

        dt.Clear();
        dt.Dispose();
    }


    public DataSet FetchCompany_LastYearTurnOver(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_lastyearTurnOver_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.VarChar, 50).Value = CompanyId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    private void Loadpaymentgrid()
    {
        // pnlOuterEdit.Visible = true;
        objCompany = new clsCompany();

        if (object.Equals(Session["CompanyId"], null))
        {
            if (txtPan.Text != "" && txtUAM.Text != "")
            {
                DataSet ds = new DataSet();
                ds = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
                fStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
                CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

                Session["CompanyId"] = CompanyId;
            }
        }
        else
        {
            CompanyId = Convert.ToInt32(Session["CompanyId"]);
        }


        int provisionstatus = objCompany.Company_Provisional_Status(CompanyId);
        if (provisionstatus == 0 && txtTurnover.Text == string.Empty)//
        {

            DataSet dsLastYT = new DataSet();
            dsLastYT = FetchCompany_LastYearTurnOver(CompanyId);
            decimal LYTurnOver = 0;
            LYTurnOver = Convert.ToDecimal(dsLastYT.Tables[0].Rows[0]["PaymentStatus"].ToString());

            if (LYTurnOver == 420)
            {
                txtTurnover.Enabled = true;
                lblpopmsg.Text = "Please Enter Turnover";
                ModalPopupExtender2.Show();
                return;
            }
            else
            {
                txtTurnover.Text = LYTurnOver.ToString();
                txtTurnover.Enabled = false;
                lblpopmsg.Text = "Please Enter Turnover";
                ModalPopupExtender2.Show();
                return;
            }


        }
        else if (provisionstatus == 0 && txtTurnover.Text != string.Empty)
        {
            //THIS ELSE IF STATEMENT FOR CALCULATING AMOUNT DO NO REMOVE IT.
        }
        else
        {
            lblpopmsg.Text = "";
            txtTurnover.Text = "0.0";
        }
        SqlDataReader Mydr = objCompany.Company_FetchSlabFee_Step6(Convert.ToDecimal(txtTurnover.Text.Trim()), CompanyId);
        FunfillPaymentSlab(Mydr);
        ModalPopupExtender2.Hide();
        try
        {
            objCompany = new clsCompany();
            int K = objCompany.Company_insert_turnover_limit(CompanyId, 1, Convert.ToDecimal(txtTurnover.Text.Trim()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }

    }
    public DataSet CheckCompanyStatus_PAN_UAM(string PANno, string Uam)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_CheckCompanyStatus_PAN_UAM", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@PANno", SqlDbType.VarChar, 50).Value = PANno;
            MyAdp.SelectCommand.Parameters.Add("@Uam", SqlDbType.VarChar, 50).Value = Uam;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }
    protected void rdbMsmeRegtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMsmeRegtype.SelectedValue == "1")
        {
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;

            // RegularExpressionValidatorAAdhar.Enabled = true;
            // ValidatorCalloutExtender9.Enabled = true;

            RegularExpressionValidator1.Enabled = false;
            // ValidatorCalloutExtender12.Enabled = false;

            //  Requ_addhar.Enabled = true;
            //  Validator_aadhar_number.Enabled = true;
            RegularExpressionValidator3.Enabled = true;

            RequiredFieldValidator18.Enabled = false;

            RequiredFieldValidator14.Enabled = true;
            //  ValidatorCalloutExtender15.Enabled = false;

            divUAMPAN.Visible = true;

            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;
            RegularExpressionValidator3.Enabled = true;
            RegularExpressionValidator4.Enabled = false;

            //  trMainData.Visible = false;
        }
        else if (rdbMsmeRegtype.SelectedValue == "2")
        {
            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;

            //  RegularExpressionValidatorAAdhar.Enabled = false;
            // ValidatorCalloutExtender9.Enabled = false;

            RegularExpressionValidator1.Enabled = true;
            //  ValidatorCalloutExtender12.Enabled = true;
            RegularExpressionValidator3.Enabled = false;
            RegularExpressionValidator4.Enabled = true;

            //    Requ_addhar.Enabled = false;
            //Validator_aadhar_number.Enabled = false;

            RequiredFieldValidator18.Enabled = true;
            RequiredFieldValidator14.Enabled = false;
            //  ValidatorCalloutExtender15.Enabled = true;

            divUAMPAN.Visible = true;

            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;
            //trMainData.Visible = false;
        }
    }
    protected void btnClose_Click(object sender, EventArgs e)
    {
        ModalPopupExtender2.Hide();
        return;
    }
    protected void btnf_Click(object sender, EventArgs e)
    {

        DataSet ds = new DataSet();

        if (txtPan.Text != "" && txtUAM.Text != "")
        {
           // DataSet ds = new DataSet();
            ds = CheckCompanyStatus_PAN_UAM(txtPan.Text.ToString(), txtUAM.Text.ToString());
            fStatus = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_ApplicationType"].ToString());
            CompanyId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

            Session["CompanyId"] = CompanyId;
        }

        DataSet dss = new DataSet();
        int PaymentStatus = 0;
        dss = CheckCompanyPaymentStatus(CompanyId);
        PaymentStatus = Convert.ToInt32(dss.Tables[0].Rows[0]["PaymentStatus"].ToString());
         
        if (PaymentStatus == 240)
        {
            //lblpopmsg.Text = "Unit is available in Branch login for payment verification. Payment Can not be added again.";  // negative value
            //ModalPopupExtender2.Show();
            //return;


            lblpopmsg.Text = "Unit is available in Branch login for payment verification. Payment Can not be added again.";  // negative value
            ModalPopupExtender2.Show();
            txtTurnover.Visible = false;
            btnSubmitEdit.Visible = false;
            return;


        }
        else
        {
            //if (Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString()) == 1)
            //{
            //    lblpopmsg.Text = "Payment data is already present. Payment Can not be added again.";
            //    ModalPopupExtender2.Show();
            //    return;
            //}

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString()) == 1 ||
               Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString()) == 2 ||
               Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString()) == 7)
            {
                lblpopmsg.Text = "Payment data is already present. Payment Can not be added again.";
                ModalPopupExtender2.Show();
                txtTurnover.Visible = false;
                btnSubmitEdit.Visible = false;
                return;
            }
            else
            {
               
                txtTurnover.Visible = true;
                btnSubmitEdit.Visible = true;
            }
        }



        if (fStatus == 2)
        {
            lblMsg.Text = "";

            //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            try
            {


                Loadpaymentgrid();

                trButton.Visible = true;


                /////FunRequest();
                //////FunfillPaymentSlab();

                objCompany = new clsCompany();



                System.Data.DataTable dt = new System.Data.DataTable();
                dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);  // Commented on 27.11.2019

                //  dt = Company_Fetch_FactoryLocation(CompanyId);
                if (dt.Rows.Count > 0)
                {
                    CnCount = dt.Rows.Count.ToString();
                }
                int RecordCount = 0;


                foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                {
                    int a = 0;
                    int RecordCheck = 0;
                    foreach (var item in row.ItemArray) // Loop over the items.
                    {
                        string address = "";
                        if (a == 1)
                        {
                            ds = new DataSet();


                            address = Convert.ToString(item);
                            FunfillAgency(address, UnitId);
                            ////ds = Check_Company_Check_Address(UnitId, address, CompanyId);
                            ////RecordCheck = Convert.ToInt32(ds.Tables[0].Rows[0]["Checkresult"].ToString());

                            ////if (RecordCount == 0 && RecordCheck == 420)
                            ////{
                            ////    FunfillAgency(address, UnitId);
                            ////    RecordCount = 1;
                            ////}
                            ////else if (RecordCheck == 240)
                            ////{
                            ////    FunfillAgency(address, UnitId);
                            ////}

                        }
                        else if (a == 0)
                        {
                            UnitId = Convert.ToInt32(item);
                            UnitIdMultiple += UnitId.ToString() + ',';
                            hdn_UnitID.Value = UnitIdMultiple;
                            string AllValue = hdn_UnitID.Value;
                            string sub = AllValue.Substring(0, AllValue.Length - 1);
                            SI = sub.Split(',');
                            hdn_unitID_Count.Value = SI.Length.ToString();
                        }
                        a++;
                    }
                }

                dt.Clear();
                dt.Dispose();

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
        else
        {
            lblMsg.Text = "Company with given PAN & UAM is not approved till now by Branch.";
            // pnlOuterEdit.Visible = false;
        }

    }
}