using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
public partial class appAdministrator_UnitRenewalStep1_Online : System.Web.UI.Page
{
    DataTable Mydt;
    DataSet MyDs;
    int ErrorId, CompanyId, UnitId, Formtypeid, BranchId;
    int UserId, RoleId, DepartmentId, Edit;
    DataTable dt;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    
    DataTable MyDt;
    FormInputReplace objInputReplace;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            // btnSubmit.Attributes.Add("onclick", " this.disabled = true; " + ClientScript.GetPostBackEventReference(btnSubmit, null) + ";");
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    protected void btnFeeDetails_Click(object sender, EventArgs e)
    {
        trCompanyValidityDetails.Visible = false;
        trFeeCalculator.Visible = true;
    }
    protected void btnRenewalRange_Click(object sender, EventArgs e)
    {
        trCompanyValidityDetails.Visible = true;
        trFeeCalculator.Visible = false;

        DataTable dt = new DataTable();

        dt = CompanyRenewal_Validity_Details(DepartmentId);
        DataSet ds = new DataSet();
        ds.Tables.Add(dt);

        lblCStatus.Text = ds.Tables[0].Rows[0]["CompanyStatus"].ToString() ; // dt.Columns["CompanyStatus"].ToString();
        lblCValidity.Text = ds.Tables[0].Rows[0]["Validity_Range"].ToString();// dt.Columns["Validity_Range"].ToString();
        lblExpiredIn.Text = ds.Tables[0].Rows[0]["Validity_gap"].ToString();// dt.Columns["Validity_gap"].ToString();
        lblRenewalFeeWithDiscount.Text = ds.Tables[0].Rows[0]["Registration_Date_50_Percent_Discount"].ToString();// dt.Columns["Registration_Date_50_Percent_Discount"].ToString();
        lblRenewalFeeWithoutDiscount.Text = ds.Tables[0].Rows[0]["Registration_Date_Without_Discount"].ToString(); //dt.Columns["Registration_Date_Without_Discount"].ToString();

    }


    protected void btnf_Click(object sender, EventArgs e)
    {
        Loadpaymentgrid();
        trFeeCalculator.Visible = true;
        trCompanyValidityDetails.Visible = false;
    }
    private void Loadpaymentgrid()
    {      

        if (txtAmount.Text == string.Empty)
        {
            lblMsg.Text = "Please Enter Turnover";
            return;
        }
        else
        {
            lblMsg.Text = "";
            // txtAmount.Text = "0.0";
        }
        SqlDataReader Mydr = Company_FetchSlabFee_Renewal_Step6(Convert.ToDecimal(txtAmount.Text.Trim()), DepartmentId);
        FunfillPaymentSlab(Mydr);
    }

    public SqlDataReader Company_FetchSlabFee_Renewal_Step6(decimal turnover, int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_Renewal", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            // MySqlComm.Parameters.Add("@Provisional", SqlDbType.Int).Value = _mprovision;
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
                tcell.Text = "S.No.";
                tcell.CssClass = "clstblCellHead";
                trow.Cells.Add(tcell);

                //tcell = new TableCell();
                //tcell.Text = "";
                //tcell.CssClass = "clstblCellHead";
                //trow.Cells.Add(tcell);

                //tcell = new TableCell();
                //tcell.Text = "Payment Slab";
                //tcell.CssClass = "clstblCellHead";
                //trow.Cells.Add(tcell);

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
            tcell.Width = Unit.Percentage(15);
            tcell.CssClass = "clstblCell";
            trow.Cells.Add(tcell);

            //tcell = new TableCell();
            //tcell.Text = "<input id='rdReg' name='rdReg' value='" + MyDr["fGPtbl_MFee_Id"].ToString() + "' type='radio' checked='checked' />";
            //tcell.Width = Unit.Percentage(2);
            //tcell.CssClass = "clstblCell";
            //trow.Cells.Add(tcell);

            //tcell = new TableCell();
            //tcell.Text = "<b>Payment</b>";
            //tcell.Width = Unit.Percentage(28);
            //tcell.CssClass = "clstblCell";
            //trow.Cells.Add(tcell);

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
    #region Function

    public DataTable CompanyRenewal_Validity_Details(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_ValidityRange", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 2400;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

  

    public DataTable CompanyRenewal_Fee_Details(decimal turnover, int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_RenewalFee_Slab", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 2400;
            MyAdp.SelectCommand.Parameters.Add("@amount", SqlDbType.Float).Value = turnover;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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



    #endregion


}