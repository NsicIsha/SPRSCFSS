using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Drawing;

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
        trRenewalUpload.Visible = false;


        txtAmount.Text = "";
        lblRenewalFee.Text = "";
    }
    protected void btnRenewalRange_Click(object sender, EventArgs e)
    {
        trCompanyValidityDetails.Visible = true;
        trFeeCalculator.Visible = false;
        trRenewalUpload.Visible = false;
        // trRenewalUpload.Visible = true;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        ds = CompanyRenewal_Validity_Details(DepartmentId);
       
        

        lblCStatus.Text = ds.Tables[0].Rows[0]["CompanyStatus"].ToString(); // dt.Columns["CompanyStatus"].ToString();
        lblCValidity.Text = ds.Tables[0].Rows[0]["Validity_Range"].ToString();// dt.Columns["Validity_Range"].ToString();
        lblExpiredIn.Text = ds.Tables[0].Rows[0]["Validity_gap"].ToString();// dt.Columns["Validity_gap"].ToString();
        lblRenewalFeeWithDiscount.Text = ds.Tables[0].Rows[0]["Registration_Date_50_Percent_Discount"].ToString();// dt.Columns["Registration_Date_50_Percent_Discount"].ToString();
        lblRenewalFeeWithoutDiscount.Text = ds.Tables[0].Rows[0]["Registration_Date_Without_Discount"].ToString(); //dt.Columns["Registration_Date_Without_Discount"].ToString();


        
        //DataSet dss = new DataSet();
        //dss.Tables.Add(dt.table[10);


        int DateRangeCheck = 0;
        DateRangeCheck = Convert.ToInt32(ds.Tables[1].Rows[0]["RecordCheckFound"].ToString());

        DateRangeCheck = 1;
        if (DateRangeCheck ==1 )
        {
            //btnRenewalProcess.Enabled = true;   
           btnRenewalProcess.Visible = true;
        }
        else if(DateRangeCheck == 0)
        {
            btnRenewalProcess.Enabled = false;
            btnRenewalProcess.ForeColor = Color.Red;
            //btnRenewalProcess.Text = "";
            btnRenewalProcess.Visible = false;
        }
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
        DataTable mydt = CompanyRenewal_Fee_Details(Convert.ToDecimal(txtAmount.Text.Trim()), DepartmentId);
        DataSet ds = new DataSet();
        ds.Tables.Add(mydt);
        lblRenewalFee.Text = "Rs." + " " + ds.Tables[0].Rows[0]["fGPtbl_MFee_Amount"].ToString();
        //FunfillPaymentSlab(Mydr);
    }

    public SqlDataReader Company_FetchSlabFee_Renewal_Step6(decimal turnover, int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchSlabFee_OnlineRenewal", DBManager.ConnectToDB());
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
     
    #region Function

    public DataSet CompanyRenewal_Validity_Details(int CompanyId)
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
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchSlabFee_OnlineRenewal", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 2400;
            MyAdp.SelectCommand.Parameters.Add("@Turnover", SqlDbType.Float).Value = turnover;
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



    protected void btnRenewalProcess_Click(object sender, EventArgs e)
    {

        trRenewalUpload.Visible = true;
        trFeeCalculator.Visible = false;


        Session["DepartmentId_CompanyId"] = DepartmentId;
        //Response.Redirect("UnitRenewalStep2_Online.aspx");

        clsCompany objCompany = new clsCompany();
        BranchId = MasterBranch_Fetch_BranchId(DepartmentId);
        DataTable dt = new DataTable();
        dt = Company_Fetch_BranchAddress(Convert.ToInt32(BranchId));
        //ddlConstitution.DataTextField = "fGPtbl_CUnit_Constitution_Name";
        //ddlConstitution.DataValueField = "fGPtbl_CUnit_Constitution_Id";
        grdFactories.DataSource = dt;
        grdFactories.DataBind();
    }

    public int MasterBranch_Fetch_BranchId(int CompanyId)
    {
        int BranchId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Fetch_BranchId_By_CompanyId", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Char, 10).Value = CompanyId;
            BranchId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
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
        return BranchId;
    }

    public DataTable Company_Fetch_BranchAddress(int BranchId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_BranchAddressFetch_POI", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MB_Id", SqlDbType.Int).Value = BranchId;


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


}