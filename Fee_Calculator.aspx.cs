using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
public partial class Fee_Calculator : System.Web.UI.Page
{
    clsCompany objCompany;

    DataTable dt;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindDDl();
        }
    }


    void BindDDl()
    {
        //Category Unit Size DROPDOWNLIST
        dt = clsDDL.ddlCategoryUnitSize();
        ddl_1f.DataSource = dt;
        ddl_1f.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
        ddl_1f.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
        ddl_1f.DataBind();
        ddl_1f.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

        dt.Clear();
        dt.Dispose();
    }

    protected void btnf_Click(object sender, EventArgs e)
    {
        Loadpaymentgrid();
        divAfterResult.Visible = true;
    }
    private void Loadpaymentgrid()
    {
        objCompany = new clsCompany();
         
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
        SqlDataReader Mydr = Company_FetchSlabFee_Step6(Convert.ToDecimal(txtAmount.Text.Trim()), Convert.ToInt32(ddl_1f.SelectedValue));
        FunfillPaymentSlab(Mydr);
    }


    public SqlDataReader Company_FetchSlabFee_Step6(decimal turnover, int CategoryUnitSize)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_FeeCalculator", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
            MySqlComm.Parameters.Add("@categoryunit", SqlDbType.Int).Value = CategoryUnitSize;
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
}