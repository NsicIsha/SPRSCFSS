using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Data.SqlClient;
public partial class appAdministrator_CompanyHistory : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string filepath;
    int urlId = 1;
    string UserFullName;
    string GpRefrenceNumber = "";
    int Refrence_Id = 0;
    decimal Monetary_Limit;
    string ZonalAcceptStaus = "";
    string CompanyStatus = "";
    int Branch_headAutorised_id;
    int Zonal_head_branch_id;
    int Branch_Monetary_limitmax;
    int Zonal_monatrylimitmax;
    string Inspectionstatus;
    string IncompleteStatus;
    string ExpiredStatus;
    string CompanyName;
    int BranchID_monetry = 0;
    int RoleID_montery = 0;
    int MinMonetryLimit = 0;
    int MaxMonetrylimit = 0;
    StringBuilder sb;

    int CompantId;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            txt_Ref_no.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btn_status_track');");
            txt_Ref_no.Focus();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            ErrHandler.WriteError("" + ex.StackTrace);
        }



    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        //  BindData();


        if (ddl_list.SelectedValue == "1")
        {
           if(txt_Ref_no.Text !="")
            {
                DataSet ds = new DataSet();
                ds = CompanyID_ByPAN(txt_Ref_no.Text.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    CompantId = Convert.ToInt32(ds.Tables[0].Rows[0]["fGPtbl_Company_Id"].ToString());

                    DataSet dsdata = new DataSet();
                    dsdata = CompanyHistory_ByID(CompantId);
                    if (dsdata.Tables[0].Rows.Count > 0)
                    {

                        tbl_show_multiple_record_tr.Visible = true;

                        lblUnitNameValue.Text = dsdata.Tables[0].Rows[0]["Company_Name"].ToString();
                        lblConstitutionValue.Text = dsdata.Tables[0].Rows[0]["Step2_FIRM_Constitution_Name"].ToString();

                    //    lblPlantMachineryValue.Text = dsdata.Tables[0].Rows[0]["Plant & Machinery(in Lakh)"].ToString();

                        double PlantMachinary=Convert.ToDouble(dsdata.Tables[0].Rows[0]["Plant & Machinery(in Lakh)"].ToString());
                        if(PlantMachinary==0)
                        {
                            lblPlantMachineryValue.Text = dsdata.Tables[0].Rows[0]["Plant & Machinery(in Lakh)"].ToString() + " (Provisional) ";
                        }
                        else
                        {
                            lblPlantMachineryValue.Text = dsdata.Tables[0].Rows[0]["Plant & Machinery(in Lakh)"].ToString();
                        }


                        lblSocialCategoryValue.Text = dsdata.Tables[0].Rows[0]["Enterprise_Social_Category"].ToString();
                        lblEnterprisecategoryValue.Text = dsdata.Tables[0].Rows[0]["Unit_Type"].ToString();

                        lblUnitSizes.Text = dsdata.Tables[0].Rows[0]["Category_Unit_Size"].ToString();

                        lblApplicationdateValue.Text = dsdata.Tables[0].Rows[0]["ApplicationDate"].ToString();
                        lblPanValue.Text = dsdata.Tables[0].Rows[0]["PAN_Card_Number"].ToString();
                        lblUAMValue.Text = dsdata.Tables[0].Rows[0]["UAM"].ToString();

                        lblValidityRangeValue.Text = dsdata.Tables[0].Rows[0]["RegistrationDate"].ToString();
                        lblMonetaryLimitValue.Text = dsdata.Tables[0].Rows[0]["fGPtbl_CUnit_ML_MonetoryLimit"].ToString();

                        lblRenewalCOuntValue.Text = dsdata.Tables[0].Rows[0]["RenewalCount1"].ToString();
                        if(dsdata.Tables[0].Rows[0]["RenewalCount1"].ToString()=="0")
                        {
                            lblProposalDateValue.Text = "";
                        }
                        else
                        {
                            lblProposalDateValue.Text = dsdata.Tables[0].Rows[0]["ProposalDate"].ToString();
                        }
                        

                        lblAmendmentCountValue.Text = dsdata.Tables[0].Rows[0]["AmendmentCount"].ToString();


                        if (dsdata.Tables[0].Rows[0]["AmendmentCount"].ToString() == "0")
                        {
                            lblAmendmentPDateValue.Text = "";
                        }
                        else
                        {
                            lblAmendmentPDateValue.Text = dsdata.Tables[0].Rows[0]["ProposalDate"].ToString();
                        }

                       

                        lblAgencyNameValue.Text = dsdata.Tables[0].Rows[0]["AgencyName"].ToString();
                        lblInspectionAllotmentDateValue.Text = dsdata.Tables[0].Rows[0]["Inspection Allotment Date"].ToString();
                        lblInspectionPassedDateValue.Text = dsdata.Tables[0].Rows[0]["Inspection Passed Date"].ToString();

                        lblFromBranchValue.Text = dsdata.Tables[0].Rows[0]["OldBranch"].ToString();
                        lblToBranchValue.Text = dsdata.Tables[0].Rows[0]["BranchName"].ToString();

                        lblCurrentStatus.Text= dsdata.Tables[0].Rows[0]["CompanyCurrentStatus"].ToString(); 

                    }

                    if (dsdata.Tables[1].Rows.Count > 0)
                    {
                        grd_search_result.DataSource = dsdata.Tables[1];
                        grd_search_result.DataBind();
                    }
                    else
                    {
                        grd_search_result.DataSource = null;
                        grd_search_result.DataBind();
                    }
                    //Registration Payment Details
                    if (dsdata.Tables[2].Rows.Count > 0)
                    {
                        gvRegPaymentDetails.DataSource = dsdata.Tables[2];
                        gvRegPaymentDetails.DataBind();
                    }
                    else
                    {
                        gvRegPaymentDetails.DataSource = null;
                        gvRegPaymentDetails.DataBind();
                    }

                    //Renewal Payment Details
                    if (dsdata.Tables[3].Rows.Count > 0)
                    {
                        gvRenewalPaymentDetails.DataSource = dsdata.Tables[3];
                        gvRenewalPaymentDetails.DataBind();
                    }
                    else
                    {
                        gvRenewalPaymentDetails.DataSource = null;
                        gvRenewalPaymentDetails.DataBind();
                    }

                    //Amendment Payment Details
                    if (dsdata.Tables[4].Rows.Count > 0)
                    {
                        gvAmendmentPaymentDetails.DataSource = dsdata.Tables[4];
                        gvAmendmentPaymentDetails.DataBind();
                    }
                    else
                    {
                        gvAmendmentPaymentDetails.DataSource = null;
                        gvAmendmentPaymentDetails.DataBind();
                    }
                }
            }
                 
           // BindTableCompanytable();
        }
        else
        {

          //  BindTable();
        }

    }

    public DataSet CompanyID_ByPAN(string PAN)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_fetch_CompanyID_ByPAN", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar).Value = PAN;
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

    public DataSet CompanyHistory_ByID(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_fetch_Company_History", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.VarChar).Value = CompanyId;
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










    protected void ddl_list_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_Ref_no.Text = "";
    }
}