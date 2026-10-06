
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Newtonsoft.Json;
using NSICGP_VBClassLibrary;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
 

public partial class appAdministrator_Online_UnitRenewalStep1_CFSS : System.Web.UI.Page
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
    string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    public DataSet Company_Man_Service_Status(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GpSp_Fetch_Company_Man_Service_Status", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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
    protected void Page_Load(object sender, EventArgs e)
    {
        FunRequest();




        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        if (!string.IsNullOrEmpty(Request.QueryString["c"]))
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            Formtypeid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());

            try
            {

                DataSet dsComCheck = new DataSet();
                dsComCheck = Company_Man_Service_Status(CompanyId);

                int Company_Man_Service_Check = 0;
                Company_Man_Service_Check = Convert.ToInt32(dsComCheck.Tables[0].Rows[0]["Statusval"]);

               // Company_Man_Service_Check = 420;

                if (Company_Man_Service_Check == 240)
                {

                    foreach (ListItem item in rdbRenewaltype.Items)
                    {
                        if (item.Value == "1")
                        {

                            item.Enabled = false;
                        }

                        else
                        {
                            item.Enabled = true;
                        }
                    }

                }
                else
                {
                    foreach (ListItem item in rdbRenewaltype.Items)
                    {
                        if (item.Value == "1")
                        {

                            item.Enabled = true;
                        }

                        else
                        {
                            item.Enabled = true;
                        }
                    }
                }

            }
            catch
            {
                CompanyId = 0;
                BranchId = 0;
            }



            OriginalFilePath = "./Document/Upload/Registration/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            // trMainData.Visible = false;

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



                //DropDownListBranch.Visible = false;
                ////trbranch1.Visible = false;
                ////trbranch2.Visible = false;
                ////trbranch3.Visible = false;

                Cache.Insert("City", clsDDL.ddlCity_Activated(0));// null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                Cache.Insert("State", clsDDL.ddlState_Activated());//, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);


                Mydtcity = clsDDL.ddlCity_Activated(0);

                ddlCityClone = Mydtcity.Clone();
                ViewState["mydtcity"] = Mydtcity;

            }
            FunfillDropdown();
        }
        else
        {
            trMainData.Visible = false;
            trRenewalType.Visible = false;
        }




        if (!string.IsNullOrEmpty(Request.QueryString["StatusR"]))
        {
            string ChkStatus = "";
            ChkStatus = Request.QueryString["StatusR"];
            if (ChkStatus == "1")
            {
                TableMain.Visible = false;
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
            }
            else if (ChkStatus == "2")
            {
                TableMain.Visible = false;
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data is already Present for the Given Unit. Please Visit https://nsicspronline.com/ and Click on Renewal Online to fill all Details till 7th Steps.');", true);
            }
            else if (ChkStatus == "3")
            {
                TableMain.Visible = false;
                ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Please Contact Administrator.');", true);
            }
        }


        ////if (!string.IsNullOrEmpty(Request.QueryString["Status"]))
        ////{
        ////    string ChkStatus = "";
        ////    ChkStatus = Request.QueryString["Status"];
        ////    if(ChkStatus=="2")
        ////    {
        ////        TableMain.Visible = false;
        ////        ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
        ////    }
        ////    else if(ChkStatus=="1")
        ////    {
        ////        TableMain.Visible = false;
        ////        ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Please Contact Administrator.');", true);
        ////    }
        ////}
    }

    private void FunfillDropdown()
    {
        if (!Page.IsPostBack)
        {
            funfill_ddlState();
            funfill_ddlCategory();
            objCompany = new clsCompany();
        }
    }
    private void funfill_ddlState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlState_a3.DataSource = dt;
        ddlState_a3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_a3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_a3.DataBind();
        ddlState_a3.Items.Insert(0, new ListItem("--Select State--", ""));
        dt.Clear();
        dt.Dispose();
    }
    private void funfill_ddlCity_a4(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlCity_a4.DataSource = dt_c;
            ddlCity_a4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity_a4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity_a4.DataBind();
            ddlCity_a4.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();
        }
        else
        {
            ddlCity_a4.Items.Clear();
        }
    }
    private void funfill_ddlCategory()
    {
        //Category Area DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlCategoryArea();
        ddl_1d.DataSource = dt;
        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
        ddl_1d.DataBind();
        ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));
        //ddl_1d.SelectedItem.Value = "1";

        //Category Cast DROPDOWNLIST
        dt.Clear();
        dt = clsDDL.ddlCategoryCast();
        ddl_1e.DataSource = dt;
        ddl_1e.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
        ddl_1e.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
        ddl_1e.DataBind();
        ddl_1e.Items.Insert(0, new ListItem("-- Select Enterprise Social Category--", ""));


        //ListItem i = ddl_1e.Items.FindByValue("4");
        //i.Attributes.Add("style", "color:gray;");
        //i.Attributes.Add("disabled", "true");
        //i.Value = "-1";
        ddl_1e.Items.Remove(ddl_1e.Items.FindByValue("4"));







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
    public DataSet Fetch_ChangesType_Renewal_Amendment_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Renewal_Amendment_Amendment_Type_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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
    public DataSet Fetch_ChangesType_Renewal_Check_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Renewal_Type_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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

    protected void FunFillDDl()
    {
        try
        {
            //  ddlBindState(ddlState_a3);
            funfill_ddlState();

            //ddlBindCity(ddlCity_c4);
            funfill_ddlCategory();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    public void ddlBindState(DropDownList ddlState)
    {
        try
        {
            MydtState = (DataTable)Cache["State"];

            ddlState.DataSource = MydtState;
            ddlState.DataTextField = MydtState.Columns["fGPtbl_MS_Name"].ToString();
            ddlState.DataValueField = MydtState.Columns["fGPtbl_MS_Id"].ToString();
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void ddlBindCity(DropDownList CityDDL, DataTable MydtCity)
    {
        try
        {
            CityDDL.DataSource = MydtCity;
            CityDDL.DataTextField = MydtCity.Columns["fGPtbl_MCty_Name"].ToString();
            CityDDL.DataValueField = MydtCity.Columns["fGPtbl_MCty_Id"].ToString();
            CityDDL.DataBind();
            CityDDL.Items.Insert(0, new ListItem("--Select City--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void ddlBindCity(DropDownList CityDDL, string Statevalue)
    {
        DataTable MydtCity, MydtCityClone = new DataTable();
        try
        {
            MydtCity = (DataTable)Cache["City"];
            MydtCityClone = MydtCity.Clone();

            cityRows = MydtCity.Select("fGPtbl_MCty_StateId=" + Statevalue);

            foreach (DataRow r1 in cityRows)
            {
                MydtCityClone.ImportRow(r1);
            }

            CityDDL.DataSource = MydtCityClone;
            CityDDL.DataTextField = MydtCityClone.Columns["fGPtbl_MCty_Name"].ToString();
            CityDDL.DataValueField = MydtCityClone.Columns["fGPtbl_MCty_Id"].ToString();
            CityDDL.DataBind();
            CityDDL.Items.Insert(0, new ListItem("--Select City--", ""));

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void FunFillcontrols(DataTable CompanyTable, DataTable BranchTable)
    {
        try
        {
            //FOR COMPANY
            if (CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString() == "")
            {
                string str = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString().Trim();
                txt_1.Text = "";

            }
            else
            {
                txt_1.Text = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString().Trim();

                //  txt_1.Attributes.Add("Readonly", "Readonly");

            }


            //     txt_proposal_date.Text = DateTime.Now.ToString("M/d/yyyy");// dateTime.ToString();



            if (CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString() == "")
            {
                txt_aahar.Text = "";// CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
            }
            else
            {
                txt_aahar.Text = CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
                txt_aahar.Attributes.Add("Readonly", "Readonly");
            }
            txt_1p1.Text = CompanyTable.Rows[0]["fGPtbl_Company_Name"].ToString();
            txt_1a1.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
            txt_1a2.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
            ddlState_a3.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString();

            funfill_ddlCity_a4(Convert.ToInt32(CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString()));
            // ddlBindCity(ddlCity_a4, CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString());


            ddlCity_a4.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_CityId"].ToString();
            txt_1a5.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
            txt_mob.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Mobile"].ToString();

            txt_1a7.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();


            if (CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString().Trim() != "")
            {
                txtproposaldate.Text = CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
            }
            else
            {
                txtproposaldate.Text = DateTime.Now.ToString("d/M/yyyy");// dateTime.ToString();//CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
            }

            txt_mob.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Mobile"].ToString();

            int Con_Id = 0;
            if (CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString() != "")
            {
                ddlConstitution.SelectedIndex = -1;
                Con_Id = Convert.ToInt32(CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString());
                ddlConstitution.Items.FindByValue(Con_Id.ToString()).Selected = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void FunFillFactory(DataTable FactoryTable)
    {
        try
        {
            DataTable FactoryTableClone = new DataTable();
            FactoryTableClone = FactoryTable.Copy();


            GridFactory.DataSource = FactoryTableClone;
            GridFactory.DataBind();

            //BIND CATEGORY ID
            ddl_1d.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString();
            HdnCatarea.Value = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString();
            ddl_1e.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString();

            //Dhiraj NSIC 10.08.2017 August
            CategoryCastId = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString());

            ddl_1f.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
            HdnCatunitsize.Value = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();

            //BIND BRANCH NAME
            // lblBranch.Text = FactoryTable.Rows[0]["BranchName"].ToString();
            ViewState["BranchId"] = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_BranchId"]);

            //FOR STEP COMPLETION
            if (FactoryTable.Columns.Contains("fGPtbl_CUnit_StepCompletion"))
            {
                ViewState["StepCompletion"] = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_StepCompletion"].ToString().Trim());
            }
            else
            {
                ViewState["StepCompletion"] = 0;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void FunctionShowHide()
    {
        //ifpageis not postback
        try
        {
            AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_PAN' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1.ReadOnly = false;
            }
            else
            {
                txt_1.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_Name' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1p1.ReadOnly = false;
            }

            else
            {
                txt_1p1.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Address' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a1.ReadOnly = false;
            }

            else
            {
                txt_1a1.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Zip' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a2.ReadOnly = false;
            }

            else
            {
                txt_1a2.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_StateId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddlState_a3.Enabled = true;
            }

            else
            {
                ddlState_a3.Enabled = false;
            }


            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_CityId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddlCity_a4.Enabled = true;
            }

            else
            {
                ddlCity_a4.Enabled = false;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Phone'and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a5.ReadOnly = false;
            }

            else
            {
                txt_1a5.ReadOnly = false;
            }



            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Email' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1a7.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1a7.ReadOnly = false;
            //}


            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Address' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c1.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c1.ReadOnly = true;
            //}


            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Zip' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c2.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c2.ReadOnly = true;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_StateId' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    ddlState_c3.Enabled = true;
            //}

            //else
            //{
            //    ddlState_c3.Enabled = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_CityId'and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    ddlCity_c4.Enabled = true;
            //}

            //else
            //{
            //    ddlCity_c4.Enabled = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Phone' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c5.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c5.ReadOnly = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Email' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c7.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c7.ReadOnly = false;
            //}

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryAreaId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1d.Enabled = true;
            }
            else
            {
                ddl_1d.Enabled = true;
                // Commented on 03 Sep 2020  ddl_1d.Enabled = false;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryCastId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1e.Enabled = true;
            }
            else
            {

                if (CategoryCastId.ToString() == "4")
                {
                    ddl_1e.Enabled = true;
                }
                else
                {
                    ddl_1e.Enabled = false;
                }

            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryUnitSizeId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1f.Enabled = true;
            }

            else
            {
                ddl_1f.Enabled = true;

                // Commented on 03 Sep 2020  ddl_1f.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void FunFillDataset()
    {
        try
        {

            objRenewal = new clsRenewal();
            Myds = new DataSet();

            DataTable MydtCompany, MydtFactory, MydtBranch = new DataTable();

            //TO FETCH DATA SET VALUE
            //Myds = objAmendment.Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
            Myds = objRenewal.CompanyRenewal_Fetch_Step1_ByCompanyId(CompanyId);

            //ASSIGNING IN MULTIPLE TABLES
            MydtCompany = Myds.Tables[0];
            MydtBranch = Myds.Tables[1];
            MydtFactory = Myds.Tables[2];

            //FILLING UP ALL DROPDOWN LIST
            FunFillDDl();

            //FILLING UP ALL CONTROLS
            FunFillcontrols(MydtCompany, MydtBranch);

            //FILLING FACTORY GRIDVIEW
            FunFillFactory(MydtFactory);

            FunctionShowHide();






        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void rdbRenewaltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbRenewaltype.SelectedValue == "1")
        {
            txtTurnover.Text = "";
            DataSet dsf = new DataSet();
            dsf = Fetch_ChangesType_Renewal_Amendment_Online(Convert.ToInt32(CompanyId));
            int RenewalCumAmendment_AmendmentStatus = 0;
            RenewalCumAmendment_AmendmentStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);
            if (RenewalCumAmendment_AmendmentStatus == 240) //Exists in Renewal table, Can not do Renewal Cum Ammendment
            {
                lblErrorMeg.Text = "Given Unit has already initiated Renewal Cum Ammendment Process and not finalised yet. Both Renewal and Renewal Cum Amendment Process can not be started Simultaneously.";
                return;
            }
            lblErrorMeg.Text = "";
            trWithoutChangesrenewal.Visible = true;
            trMainData.Visible = true;
            pnlMain.Visible = false;
            FunFillDataset();

            Clear();
            disabled();


        }
        else if (rdbRenewaltype.SelectedValue == "2")
        {

            Clear();
            disabled();
            txtTurnover.Text = "";

            DataSet dsf = new DataSet();
            dsf = Fetch_ChangesType_Renewal_Check_Online(Convert.ToInt32(CompanyId));
            int RenewalCumAmendment_AmendmentStatus = 0;
            RenewalCumAmendment_AmendmentStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

            if (RenewalCumAmendment_AmendmentStatus == 240) //Exists in Renewal table, Can do Renewal Cum Ammendment
            {
                lblErrorMeg.Text = "Given Unit has already initiated Renewal Process and not finalised yet. Both Renewal and Renewal Cum Amendment Process can not started Simultaneously.";
                trWithoutChangesrenewal.Visible = false;
                return;
            }
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = string.Empty, EncryptFormtype = string.Empty, EncryptBranchId = string.Empty;
            //FOR BRANCH
            //if (RoleId == 8)
            //{
            ////EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            //////string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            ////EncryptFormtype = objDecEnc.Encrypt("3");
            ////EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            //string PanCard = objDecEnc.Encrypt(txt_1.Text.ToString());
            //}
            //else if (RoleId == 7) //FOR REGISTERED COMPANY
            //{
            //    EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            //    EncryptFormtype = objDecEnc.Encrypt("3");
            //    EncryptBranchId = objDecEnc.Encrypt(ViewState["BranchId"].ToString());
            //}
            ViewState["BranchId"] = "";
            ViewState["StepCompletion"] = "";

            pnlMain.Visible = false;
            trMainData.Visible = true;
            FunFillDataset_RA();

            // trWithoutChangesrenewal.Visible = true;
            // Response.Redirect("Online_Unit_Ren_Amnd_Step1.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&b=" + EncryptBranchId, false);



            // Response.Redirect("Online_Unit_Ren_Amnd_Step1.aspx");
        }
    }

    void Clear()
    {
        txt_chqddNo.Text = "";
        txt_BankName.Text = "";
        txt_BankBranch.Text = "";
        txt_PayableAt.Text = "";
        txt_chqddAmnt.Text = "";
        txtChequeDDPaymentRemarks.Text = "";

        lbl_registration_fees_text.Text = "";
        lblUnitAmnt.Text = "";
        lblServiceTax.Text = "";
        lblTotAmnt.Text = "";

        lbl_payment_details_nsic.Text = "";
        lbl_registration_fees_text.Text = "";
        lblUnitAmnt.Text = "";
        lblServiceTax.Text = "";
        lblTotAmnt.Text = "";
        txt_chqddate.Text = "";
        radPay.SelectedIndex = -1;
    }

    void disabled()
    {
        DropDownListBranch.Enabled = false;
        ddlConstitution.Enabled = false;
        txt_1.Enabled = false;
        txt_aahar.Enabled = false;
        txt_1p1.Enabled = false;
        txt_1a1.Enabled = false;
        txt_1a2.Enabled = false;
        ddlState_a3.Enabled = false;
        txt_1a5.Enabled = false;
        txt_mob.Enabled = false;
        txt_1a7.Enabled = false;
        ddl_1e.Enabled = false;
        // ddl_1f.Enabled = false;

    }
    protected void btnCalculateFeess_Click(object sender, EventArgs e)
    {

        Clear();


        #region Bind Bank based on Branch
        CFSSService obj = new CFSSService();
        string cfssBranchCode = obj.GetCFSSBranchCode(BranchId);

        //  List<CFSSService.BankDetails> banks = obj.GetBankList(cfssBranchCode);
        List<CFSSService.BankDetails> banks = obj.GetBankList("101");
        Session["BankList"] = banks;

        ddlBank.DataSource = banks.Select(x => new
        {
            LedgerID = x.LedgerID,
            BankAccount_Name = x.BankAccountNo + " - " + x.Bank_Name
        }).ToList();

        ddlBank.DataTextField = "BankAccount_Name";
        ddlBank.DataValueField = "LedgerID";
        ddlBank.DataBind();

        ddlBank.Items.Insert(0, new ListItem("--Select Bank--", "0"));

        #endregion



        pnlMain.Visible = true;
        int categoryunit = 0;
        int CatArea = 0;
        int CatCast = 0;

        radPay.Items.FindByValue("4".ToString()).Selected = true;
        trPayableat.Visible = false;
        txt_PayableAt.Text = "";

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

    protected void radPay_SelectedIndexChanged(object sender, EventArgs e)
    {

        trbranch1.Visible = true;
        trMainData.Visible = true;
        pnlMain.Visible = true;

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
            AmountPayable = (Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString())) + ((Convert.ToDouble(MyDr["fGPtbl_MFee_Amount"].ToString()) * 18) / 100);

        }



        if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 1)

        {
            // pnlCash.Visible = true;
            pnlChqDD.Visible = false;
            //if (RoleId == 8)
            //{
            //    receipt.Visible = true;

            //}
            //else
            //{
            //    receipt.Visible = false;
            //}
            Session["PaymentType"] = "1";
            //lblCashAmnt.Text = (Math.Round(AmountPayable,0)).ToString();


            //Dhiraj 06 Sep 
            //if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            //{

            //    lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            //}
            //else
            //{
            //    lblCashAmnt.Text = "Rs." + " " + Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString())));
            //}

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 2)
        {
            // pnlChqDD.Visible = true;

            //  pnlCash.Visible = false;
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

            trPayableat.Visible = true;

            pnlChqDD.Visible = true;

        }
        else if (Convert.ToInt32(radPay.SelectedItem.Value.ToString()) == 3)
        {
            trPayableat.Visible = true;

            //pnlChqDD.Visible = true;
            //pnlCash.Visible = false;
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

            trPayableat.Visible = false;
            txt_PayableAt.Text = "";

            if (CategoryCastId == 4 || CategoryCastId == 10 || CategoryCastId == 11)
            {

                txt_chqddAmnt.Text = Convert.ToString(string.Format("{0:0.00}", Convert.ToDecimal("118.00".ToString())));
            }
            else
            {
                txt_chqddAmnt.Text = string.Format("{0:0.00}", Convert.ToDecimal(AmountPayable.ToString()));
            }


            Session["PaymentType"] = "4";
            pnlChqDD.Visible = true;
        }

    }

    protected void btnfeeSlab_Click(object sender, EventArgs e)
    {
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

        trbranch1.Visible = true;
        trMainData.Visible = true;


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

    protected void ddlConstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        //txt_1.Text = "";
        //txt_aahar.Text = "";
        if (ddlConstitution.SelectedValue != "0")
        {
            txt_1.Enabled = true;
        }
        else
        {
            txt_1.Enabled = false;
            txt_1.Text = "";
            txt_aahar.Text = "";
        }

        if (ddlConstitution.SelectedValue != "4")
        {
            Requ_addhar.Enabled = false;
        }
        else
        {
            Requ_addhar.Enabled = true;
        }
    }


    protected void ddlState_a3_SelectedIndexChanged(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlState_a3.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlCity_a4(fStateId);
        ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "UpdateProgressBar_DroDown();", true);
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


    protected void GridFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                // Bind drop down to cities           
                DropDownList ddlGridState = (DropDownList)e.Row.FindControl("ddlState_b3");

                ddlBindState(ddlGridState);

                HiddenField hdnstate = (HiddenField)e.Row.FindControl("hdnstate");
                HiddenField hdncity = (HiddenField)e.Row.FindControl("hdncity");

                DropDownList ddlGridcity = (DropDownList)e.Row.FindControl("ddlCity_b4");
                ddlBindCity(ddlGridcity, hdnstate.Value);
                HiddenField hdnNatureOfOperation = (HiddenField)e.Row.FindControl("hdnNatureOfOperation");
                DropDownList ddlftNatureofOperation = (DropDownList)e.Row.FindControl("ddlNatureOfOperation_b8");
                ddlftNatureofOperation.SelectedValue = hdnNatureOfOperation.Value;
                if (Convert.ToInt32(hdnstate.Value) != 0)
                {
                    ddlGridState.SelectedValue = (hdnstate.Value.ToString());
                    ddlGridcity.SelectedValue = (hdncity.Value.ToString());
                }
                else
                {
                    ddlGridcity.Items.Insert(0, new ListItem("--Select City--", ""));
                }

                int index;
                index = e.Row.RowIndex;

                TextBox txtadd = (TextBox)e.Row.FindControl("txt_1b1");
                TextBox txtpin = (TextBox)e.Row.FindControl("txt_1b2");
                TextBox txtph = (TextBox)e.Row.FindControl("txt_1b5");
                TextBox txtfax = (TextBox)e.Row.FindControl("txt_1b6");
                TextBox txteml = (TextBox)e.Row.FindControl("txt_1b7");

                txtadd.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                txtpin.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                ddlGridState.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                ddlGridcity.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                txtph.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                txtfax.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);
                txteml.TabIndex = (short)(tabindx);
                tabindx = (short)(tabindx + 1);


                if (index > 0)
                {
                    RequiredFieldValidator reqAdd = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator9");
                    reqAdd.Enabled = false;

                    RequiredFieldValidator reqZip = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator10");
                    reqZip.Enabled = false;

                    RequiredFieldValidator reqState = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator19");
                    reqState.Enabled = false;

                    RequiredFieldValidator reqCity = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator12");
                    reqCity.Enabled = false;

                    //RequiredFieldValidator reqPhone = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator13");
                    //reqPhone.Enabled = false;

                    //RequiredFieldValidator reqFax = (RequiredFieldValidator)e.Row.FindControl("RequiredFieldValidator14");
                    //reqFax.Enabled = false;


                }

                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_Address' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtAddress = (TextBox)e.Row.FindControl("txt_1b1");
                    txtAddress.ReadOnly = false;
                }
                else
                {
                    TextBox txtAddress = (TextBox)e.Row.FindControl("txt_1b1");
                    txtAddress.ReadOnly = true;

                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_Zip' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtPin = (TextBox)e.Row.FindControl("txt_1b2");
                    txtPin.ReadOnly = false;
                }
                else
                {
                    TextBox txtPin = (TextBox)e.Row.FindControl("txt_1b2");
                    txtPin.ReadOnly = true;
                }

                //DROP DOWN AND OTHER FIELDS CHANGE AS READ ONLY
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_StateId' and COlval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlState_b3");
                    ddlState.Enabled = true;
                }
                else
                {
                    DropDownList ddlState = (DropDownList)e.Row.FindControl("ddlState_b3");
                    ddlState.Enabled = false;
                }

                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_CityId' and COlval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    DropDownList ddlCity = (DropDownList)e.Row.FindControl("ddlCity_b4");
                    ddlCity.Enabled = true;
                }
                else
                {
                    DropDownList ddlCity = (DropDownList)e.Row.FindControl("ddlCity_b4");
                    ddlCity.Enabled = false;
                }

                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_Phone' and COlval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtFactPhone = (TextBox)e.Row.FindControl("txt_1b5");
                    txtFactPhone.ReadOnly = false;
                }
                else
                {
                    TextBox txtFactPhone = (TextBox)e.Row.FindControl("txt_1b5");
                    txtFactPhone.ReadOnly = true;
                }

                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_Fax' and COlval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtFactFax = (TextBox)e.Row.FindControl("txt_1b6");
                    txtFactFax.ReadOnly = false;
                }
                else
                {
                    TextBox txtFactFax = (TextBox)e.Row.FindControl("txt_1b6");
                    txtFactFax.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_1B_Factory_Email'  and COlval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtFactEmail = (TextBox)e.Row.FindControl("txt_1b7");
                    txtFactEmail.ReadOnly = false;
                }
                else
                {
                    TextBox txtFactEmail = (TextBox)e.Row.FindControl("txt_1b7");
                    txtFactEmail.ReadOnly = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public DataSet Company_UnitDetails_Fetch_ByCompanyId(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_FetchByCompanyId_Step1_Renewal_Amendment_Online", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;

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
    protected void FunFillDataset_RA()
    {
        try
        {

            objRenewal = new clsRenewal();
            Myds = new DataSet();

            DataTable MydtCompany, MydtFactory, MydtBranch = new DataTable();

            //TO FETCH DATA SET VALUE
            //Myds = objAmendment.Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
            //   Myds = objRenewal.CompanyRenewal_Fetch_Step1_ByCompanyId(CompanyId);

            Myds = Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
            //ASSIGNING IN MULTIPLE TABLES
            MydtCompany = Myds.Tables[0];
            MydtBranch = Myds.Tables[1];
            MydtFactory = Myds.Tables[2];

            //FILLING UP ALL DROPDOWN LIST
            FunFillDDl_RA();

            //FILLING UP ALL CONTROLS
            FunFillcontrols_RA(MydtCompany, MydtBranch);

            //FILLING FACTORY GRIDVIEW
            FunFillFactory_RA(MydtFactory);

            // FunctionShowHide_RA();






        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void FunFillDDl_RA()
    {
        try
        {


            funfill_ddlState();
            // ddlBindCity(ddlCity_a4);
            // ddlBindState(ddlState_c3);
            //ddlBindCity(ddlCity_c4);
            funfill_ddlCategory();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void FunFillcontrols_RA(DataTable CompanyTable, DataTable BranchTable)
    {
        try
        {
            //FOR COMPANY
            if (CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString() == "")
            {
                string str = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString().Trim();
                txt_1.Text = "";

            }
            else
            {
                txt_1.Text = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString().Trim();

                //  txt_1.Attributes.Add("Readonly", "Readonly");

            }


            //     txt_proposal_date.Text = DateTime.Now.ToString("M/d/yyyy");// dateTime.ToString();



            if (CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString() == "")
            {
                txt_aahar.Text = "";// CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
            }
            else
            {
                txt_aahar.Text = CompanyTable.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
                txt_aahar.Attributes.Add("Readonly", "Readonly");
            }
            txt_1p1.Text = CompanyTable.Rows[0]["fGPtbl_Company_Name"].ToString();
            txt_1a1.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
            txt_1a2.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
            ddlState_a3.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString();

            funfill_ddlCity_a4(Convert.ToInt32(CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString()));

            //ddlBindCity(ddlCity_a4, CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString());
            ddlCity_a4.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_CityId"].ToString();
            txt_1a5.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
            txt_mob.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Mobile"].ToString();

            txt_1a7.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();


            if (CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString().Trim() != "")
            {
                txtproposaldate.Text = CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
            }
            else
            {
                txtproposaldate.Text = DateTime.Now.ToString("d/M/yyyy");// dateTime.ToString();//CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
            }

            int Con_Id = 0;
            if (CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString() != "")
            {
                ddlConstitution.SelectedIndex = -1;
                Con_Id = Convert.ToInt32(CompanyTable.Rows[0]["fGPtbl_ConstitutionId"].ToString());
                ddlConstitution.Items.FindByValue(Con_Id.ToString()).Selected = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void FunFillFactory_RA(DataTable FactoryTable)
    {
        try
        {
            DataTable FactoryTableClone = new DataTable();
            FactoryTableClone = FactoryTable.Copy();


            GridFactory.DataSource = FactoryTableClone;
            GridFactory.DataBind();

            //BIND CATEGORY ID
            ddl_1d.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString();
            HdnCatarea.Value = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString();
            ddl_1e.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString();

            //Dhiraj NSIC 10.08.2017 August
            CategoryCastId = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString());

            ddl_1f.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
            HdnCatunitsize.Value = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();

            //BIND BRANCH NAME
            //  lblBranch.Text = FactoryTable.Rows[0]["BranchName"].ToString();
            ViewState["BranchId"] = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_BranchId"]);

            //FOR STEP COMPLETION
            if (FactoryTable.Columns.Contains("fGPtbl_CUnit_StepCompletion"))
            {
                ViewState["StepCompletion"] = Convert.ToInt32(FactoryTable.Rows[0]["fGPtbl_CUnit_StepCompletion"].ToString().Trim());
            }
            else
            {
                ViewState["StepCompletion"] = 0;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void FunctionShowHide_RA()
    {
        //ifpageis not postback
        try
        {
            AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_PAN' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1.ReadOnly = false;
            }
            else
            {
                txt_1.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_Name' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1p1.ReadOnly = false;
            }

            else
            {
                txt_1p1.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Address' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a1.ReadOnly = false;
            }

            else
            {
                txt_1a1.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Zip' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a2.ReadOnly = false;
            }

            else
            {
                txt_1a2.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_StateId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddlState_a3.Enabled = true;
            }

            else
            {
                ddlState_a3.Enabled = false;
            }


            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_CityId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddlCity_a4.Enabled = true;
            }

            else
            {
                ddlCity_a4.Enabled = false;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Phone'and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_1a5.ReadOnly = false;
            }

            else
            {
                txt_1a5.ReadOnly = false;
            }



            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_Company_HeadOffice_Email' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1a7.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1a7.ReadOnly = false;
            //}


            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Address' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c1.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c1.ReadOnly = true;
            //}


            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Zip' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c2.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c2.ReadOnly = true;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_StateId' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    ddlState_c3.Enabled = true;
            //}

            //else
            //{
            //    ddlState_c3.Enabled = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_CityId'and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    ddlCity_c4.Enabled = true;
            //}

            //else
            //{
            //    ddlCity_c4.Enabled = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Phone' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c5.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c5.ReadOnly = false;
            //}

            //AllstepchangeRow = AllstepchangeDs.Tables[0].Select("Colname='fGPtbl_CUnitBranch_Email' and Colval = 1 ");
            //if (AllstepchangeRow.Length > 0 && RoleId == 8)
            //{
            //    txt_1c7.ReadOnly = false;
            //}

            //else
            //{
            //    txt_1c7.ReadOnly = false;
            //}

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryAreaId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1d.Enabled = true;
            }
            else
            {
                ddl_1d.Enabled = true;
                // Commented on 03 Sep 2020  ddl_1d.Enabled = false;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryCastId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1e.Enabled = true;
            }
            else
            {

                if (CategoryCastId.ToString() == "4")
                {
                    ddl_1e.Enabled = true;
                }
                else
                {
                    ddl_1e.Enabled = false;
                }

            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_CategoryUnitSizeId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                ddl_1f.Enabled = true;
            }

            else
            {
                ddl_1f.Enabled = true;

                // Commented on 03 Sep 2020  ddl_1f.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    void Renewal_insert()
    {
        //TO HOLD THE STEP OF COMPLETION
        int StepCompletion = (int)ViewState["StepCompletion"];
        objAmendment = new clsAmendment();
        string PANno = txt_1.Text.Trim().ToUpper();
        string NameofApplicantFirm = txt_1p1.Text.Trim();
        string HOAddress = txt_1a1.Text.Trim();
        string HOPinCode = txt_1a2.Text.Trim();
        int HOState = 0, HOCity = 0;
        string Aadhar_number = "";
        string hoMob = txt_mob.Text.Trim();
        if (txt_aahar.Text != "")
        {
            Aadhar_number = txt_aahar.Text.ToString();
        }

        try
        {
            HOState = Convert.ToInt32(ddlState_a3.SelectedValue);
        }
        catch { }

        try
        {
            HOCity = Convert.ToInt32(ddlCity_a4.SelectedValue);
        }
        catch { }
        string HOTelephone = txt_1a5.Text.Trim();
        string HOEmail = txt_1a7.Text.Trim();
        int CategoryAreaId = 0;
        try
        {
            CategoryAreaId = Convert.ToInt32(HdnCatarea.Value);
        }
        catch { }

        int CategoryCastId = 0;
        try
        {
            CategoryCastId = Convert.ToInt32(ddl_1e.SelectedValue);
        }
        catch { }

        int CategoryUnitSizeId = 0;
        try
        {
            CategoryUnitSizeId = Convert.ToInt32(HdnCatunitsize.Value);
        }
        catch { }





        try
        {
            //objAmendment.CompanyAmendment_InsertUpdate_CompanyBranch_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
            //      HOCity, HOPinCode, HOTelephone, HOFax, HOEmail, BRAddress, BRPin, BRState, BRCity, BRPhone, BRFax, BREmail, UserId, CompanyId)
            if (StepCompletion >= 1)
            {
                ErrorId = objAmendment.CompanyRenewal_Insert_Step1_RA_CFSS_Step1(2, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, hoMob, HOEmail, UserId, CompanyId,
                    "renewal", Convert.ToString(txtproposaldate.Text.Trim()), Aadhar_number);
            }
            else
            {
                ErrorId = objAmendment.CompanyRenewal_Insert_Step1_RA_CFSS_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, hoMob, HOEmail, UserId, CompanyId,
                    "renewal", Convert.ToString(txtproposaldate.Text.Trim()), Aadhar_number);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


        string FTAddress = "", FTPinCode = "", FTTelephone = "", FTFax = "", FTEmail = "";
        int i = 0, FTState = 0, FTCity = 0;
        if (BranchId == null || BranchId == 0)
        {
            BranchId = Convert.ToInt32(ViewState["BranchId"]);
        }
        while (i < GridFactory.Rows.Count)
        {
            //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");

            TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");
            HiddenField hdnfld = (HiddenField)GridFactory.Rows[i].FindControl("hdnunit");
            //CompanyId = Convert.ToInt32(hdnCompany.Value.ToString());
            UnitId = Convert.ToInt32(hdnfld.Value.ToString());

            TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
            TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
            TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
            TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
            HiddenField hdnfstate = (HiddenField)GridFactory.Rows[i].FindControl("hdnstate");
            HiddenField hdnfcity = (HiddenField)GridFactory.Rows[i].FindControl("hdncity");
            //RA
            DropDownList ddlftNatureofOperation = (DropDownList)GridFactory.Rows[i].FindControl("ddlNatureOfOperation_b8");
            //RA
            FTAddress = txtadd.Text;
            FTPinCode = txtpin.Text;
            FTTelephone = txtphn.Text;
            FTFax = txtfax.Text;
            FTEmail = txtemail.Text;//================================================


            try
            {
                if (UnitId != 0 && txtadd.Text.Trim() != "")
                {
                    FTState = Convert.ToInt32(hdnfstate.Value);
                    FTCity = Convert.ToInt32(hdnfcity.Value);
                    int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);
                    if (StepCompletion >= 1)
                    {//TO UPDATE
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1_CFSS(2, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                    CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "renewal", bsFTNatureOfOperation);
                    }
                    else
                    {
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1_CFSS(1, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "renewal", bsFTNatureOfOperation);
                    }
                }

                i++;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }


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
        PaymentType = Convert.ToInt32(radPay.SelectedValue);// Convert.ToInt32(Session["PaymentType"].ToString());
        Decimal ftLastYearTurnOver = 0, UnitAmount = 0, UnitServicetax = 0;

        if (lblUnitAmnt.Text.Trim().ToString() != "")
        {
            UnitAmount = Convert.ToDecimal(lblUnitAmnt.Text.Trim().ToString());
        }
        if (lblServiceTax.Text.Trim().ToString() != "")
        {
            UnitServicetax = Convert.ToDecimal(lblServiceTax.Text.Trim().ToString());
        }
        if (txtTurnover.Text.Trim().ToString() != "")
        {
            ftLastYearTurnOver = Convert.ToDecimal(txtTurnover.Text.Trim().ToString());
        }

        try
        {
            int K = objCompany.Company_insert_turnover_limit(CompanyId, 2, Convert.ToDecimal(txtTurnover.Text.ToString()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }  // 

        int tblId = 0;


        bool selected = false;

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

        int intBranchId = 0;
        int intConst = 0;
         
        int intCastId = 0;
        int intSpecCastid = 0, intunitsize = 0;

        try
        {
            if (DropDownListBranch.SelectedIndex > 0)
            {
                intBranchId = Convert.ToInt32(DropDownListBranch.SelectedValue);
            }

        }
        catch { }
        try
        {
            if (ddlConstitution.SelectedIndex > 0)
            {
                intConst = Convert.ToInt32(ddlConstitution.SelectedValue);
            }

        }
        catch { }
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




        int CFSSUNITID = 0;

        #region  If Unit Is not Registered with CFSS then call API to push data to CFSS Portal

        CFSSService obj = new CFSSService();
        CFSSUNITID = obj.CallApi_CFSSUNITID_Company(txt_aahar.Text, "", txt_1.Text);
        if (CFSSUNITID != 0)
        {
            bool isUpdated = obj.UpdateCFSSUnitID_IfNull(CompanyId, CFSSUNITID);
        }

        #endregion




        tblId = Company_Insert_PaymentDetails_CFSS(CompanyId,UnitId, 2,
            PaymentType, ChqDdNo, BankName, BranchName, PaybalAt, UnitAmount, UnitServicetax, Fee_Id,
            Fee_Slab, newFileName, OriginalFilePath, ChqDdate, PaymentRemarks, ftLastYearTurnOver,
            Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername,
            intSpecCastid, intCastId, intunitsize, intBranchId, intConst, CFSSUNITID
            );



        



        #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB
        int FormType = 2;

        string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        CFSSService service = new CFSSService();
        string result = service.ProcessPayment(CompanyId, FormType, apiUrl, "Renewal");
        if (result == "SUCCESS")
        {
            if (tblId == 1)
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=1");
            }
            else if (tblId == 2)
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=2");
            }
            else
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=3");
            }
        }
        else
        {

            lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
            lblErrorMsg.ForeColor = System.Drawing.Color.Red;
        }

        ////////DataTable dt = new DataTable();
        ////////try
        ////////{
        ////////    using (SqlConnection con = new SqlConnection(connectionString))
        ////////    {
        ////////        using (SqlCommand cmd = new SqlCommand("CFSS_Push_PaymentResponse_Branch", con))
        ////////        {
        ////////            cmd.CommandType = CommandType.StoredProcedure;
        ////////            cmd.Parameters.AddWithValue("@CompanyId", CompanyId);
        ////////            cmd.Parameters.AddWithValue("@FormType", FormType);
        ////////            cmd.CommandTimeout = 6000000;
        ////////            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
        ////////            {
        ////////                con.Open();
        ////////                da.Fill(dt);
        ////////            }
        ////////        }
        ////////    }
        ////////}
        ////////catch (Exception ex)
        ////////{

        ////////    throw new Exception("Error fetching data from DB: " + ex.Message);
        ////////}

        ////////string jsonResponse = BuildBranchApiJson(dt);
        ////////string CFSS_UNITPostingAPIUrl = System.Configuration.ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        ////////string User_Name = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_UserName"];
        ////////string Password = System.Configuration.ConfigurationManager.AppSettings["CFSS_API_Password"];
        ////////ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls13;
        ////////var clients = new RestClient(CFSS_UNITPostingAPIUrl);
        ////////clients.Authenticator = new HttpBasicAuthenticator(User_Name, Password);
        ////////var requests = new RestRequest("", Method.POST);
        ////////requests.AddHeader("Content-Type", "application/json");
        ////////requests.AddParameter("application/json", jsonResponse, ParameterType.RequestBody);
        ////////IRestResponse responses = clients.Execute(requests);
        ////////if (responses.IsSuccessful && !string.IsNullOrEmpty(responses.Content))
        ////////{
        ////////    SavePaymentResponse(responses.Content, CompanyId);
        ////////}
        ////////else
        ////////{
        ////////    string msg = SaveErrorLog(CompanyId, responses.Content, "Payment Response failed from CFSS Server. Please try again or contact support.");
        ////////    lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
        ////////    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
        ////////    return;
        ////////}

        #endregion






        ////////if (ErrorId == 2)   //INSERT/UPDATE SUCCESSFULL
        ////////{
        ////////    Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?Status=2");
        ////////}
        ////////else //INSERT/UPDATE FAIL
        ////////{
        ////////    Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?Status=1");
        ////////}

    }


    public class PaymentResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public string Id { get; set; }
    }







    void RenewalCum_Amendment_insert()
    {
        //Session.Add("AmedmentTypeofChange", rdo_typeofChange.SelectedValue.ToString());
        //TO HOLD THE STEP OF COMPLETION
        int StepCompletion = (int)ViewState["StepCompletion"];
        objAmendment = new clsAmendment();
        string PANno = txt_1.Text.Trim().ToUpper();
        string NameofApplicantFirm = txt_1p1.Text.Trim();
        string HOAddress = txt_1a1.Text.Trim();
        string HOPinCode = txt_1a2.Text.Trim();
        int HOState = 0, HOCity = 0;
        string hoMob = txt_mob.Text.Trim();

        try
        {
            HOState = Convert.ToInt32(ddlState_a3.SelectedValue);
        }
        catch { }

        try
        {
            HOCity = Convert.ToInt32(ddlCity_a4.SelectedValue);
        }
        catch { }
        string HOTelephone = txt_1a5.Text.Trim();

        string HOEmail = txt_1a7.Text.Trim();
        string ProposalDate_text = txtproposaldate.Text.ToString() == "" ? "" : txtproposaldate.Text.ToString();
        string Aadhar_number = txt_aahar.Text.ToString() == "" ? "" : txt_aahar.Text.ToString();
        int CategoryAreaId = 0;

        try
        {
            CategoryAreaId = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        catch { }

        int CategoryCastId = 0;
        try
        {
            CategoryCastId = Convert.ToInt32(ddl_1e.SelectedValue);
        }
        catch { }

        int CategoryUnitSizeId = 0;
        try
        {
            CategoryUnitSizeId = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        catch { }




        try
        {
            //objAmendment.CompanyAmendment_InsertUpdate_CompanyBranch_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
            //      HOCity, HOPinCode, HOTelephone, HOFax, HOEmail, BRAddress, BRPin, BRState, BRCity, BRPhone, BRFax, BREmail, UserId, CompanyId)
            if (StepCompletion >= 1)
            {
                ErrorId = objAmendment.CompanyRenewal_Insert_Step1_RA_CFSS_Step1(2, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, hoMob, HOEmail, UserId, CompanyId,
                    "renewal_RA", Convert.ToString(txtproposaldate.Text.Trim()), Aadhar_number);
            }
            else
            {
                ErrorId = objAmendment.CompanyRenewal_Insert_Step1_RA_CFSS_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, hoMob, HOEmail, UserId, CompanyId,
                    "renewal_RA", Convert.ToString(txtproposaldate.Text.Trim()), Aadhar_number);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


        if (ErrorId != 2)
        {
            return;
        }

        string FTAddress = "", FTPinCode = "", FTTelephone = "", FTFax = "", FTEmail = "";
        int i = 0, FTState = 0, FTCity = 0;
        if (BranchId == null || BranchId == 0)
        {
            BranchId = Convert.ToInt32(ViewState["BranchId"]);
        }
        while (i < 5)
        {
            //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");
            if (i >= GridFactory.Rows.Count)
            {
                i++;
                continue;
            }
            TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");
            HiddenField hdnfld = (HiddenField)GridFactory.Rows[i].FindControl("hdnunit");
            //CompanyId = Convert.ToInt32(hdnCompany.Value.ToString());
            UnitId = Convert.ToInt32(hdnfld.Value.ToString());

            TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
            TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
            TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
            TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
            DropDownList ddlftstate = (DropDownList)GridFactory.Rows[i].FindControl("ddlState_b3");
            DropDownList ddlftcity = (DropDownList)GridFactory.Rows[i].FindControl("ddlCity_b4");
            DropDownList ddlftNatureofOperation = (DropDownList)GridFactory.Rows[i].FindControl("ddlNatureOfOperation_b8");
            int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);
            FTAddress = txtadd.Text;
            FTPinCode = txtpin.Text;
            FTTelephone = txtphn.Text;
            FTFax = txtfax.Text;
            FTEmail = txtemail.Text;//================================================





            try
            {
                if (UnitId != 0 && txtadd.Text.Trim() != "")
                {
                    // FTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    // FTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        FTState = 0;
                    }
                    else
                    {
                        FTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    }
                    if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "") // Changes By Ravish Kumar Pandey
                    {
                        FTCity = 0;

                    }
                    else
                    {
                        FTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    }


                    if (StepCompletion >= 1)
                    {//TO UPDATE
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1_CFSS(2, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                    CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "amendment", bsFTNatureOfOperation);

                    }
                    else
                    {
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1_CFSS(1, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "amendment", bsFTNatureOfOperation);

                    }
                }
                else if (UnitId == 0 && txtadd.Text.Trim() != "")
                {
                    // FTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    //  FTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "")
                    {
                        FTState = 0;
                    }
                    else
                    {
                        FTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    }
                    if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "")
                    {
                        FTCity = 0;

                    }
                    else
                    {
                        FTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                    }


                    /* if (StepCompletion >= 1)
                     {//TO UPDATE
                         objAmendment.CompanyAmendment_InsertUpdateFactory_step1(2, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                     CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId);
                     }
                     else
                     {*/
                    objAmendment.CompanyAmendment_InsertUpdateFactory_step1(1, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                            CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "amendment", bsFTNatureOfOperation);
                    //}


                }
                i++;
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }




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
        PaymentType = PaymentType = Convert.ToInt32(radPay.SelectedValue);// Convert.ToInt32(Session["PaymentType"].ToString());
        Decimal ftLastYearTurnOver = 0, UnitAmount = 0, UnitServicetax = 0;

        if (lblUnitAmnt.Text.Trim().ToString() != "")
        {
            UnitAmount = Convert.ToDecimal(lblUnitAmnt.Text.Trim().ToString());
        }
        if (lblServiceTax.Text.Trim().ToString() != "")
        {
            UnitServicetax = Convert.ToDecimal(lblServiceTax.Text.Trim().ToString());
        }
        if (txtTurnover.Text.Trim().ToString() != "")
        {
            ftLastYearTurnOver = Convert.ToDecimal(txtTurnover.Text.Trim().ToString());
        }

        try
        {
            int K = objCompany.Company_insert_turnover_limit(CompanyId, 3, Convert.ToDecimal(txtTurnover.Text.ToString()), UserId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }  // positive



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

        int intBranchId = 0;
        int intConst = 0;

        int intCastId = 0;
        int intSpecCastid = 0, intunitsize = 0;

        try
        {
            if (DropDownListBranch.SelectedIndex > 0)
            {
                intBranchId = Convert.ToInt32(DropDownListBranch.SelectedValue);
            }

        }
        catch { }
        try
        {
            if (ddlConstitution.SelectedIndex > 0)
            {
                intConst = Convert.ToInt32(ddlConstitution.SelectedValue);
            }

        }
        catch { }
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
        int tblId = 0;





        #region  If Unit Is not Registered with CFSS then call API to push data to CFSS Portal
        int CFSSUNITID = 0;
        CFSSService obj = new CFSSService();
        CFSSUNITID = obj.CallApi_CFSSUNITID_Company(txt_aahar.Text, "", txt_1.Text);
        if (CFSSUNITID != 0)
        {
            bool isUpdated = obj.UpdateCFSSUnitID_IfNull(CompanyId, CFSSUNITID);
        }

        #endregion

        //CFSSUNITID =Convert.ToInt32(hdnCFFSSUnitId.Value);

        tblId = Company_Insert_PaymentDetails_CFSS(CompanyId, UnitId, 3,
            PaymentType, ChqDdNo, BankName, BranchName, PaybalAt, UnitAmount, UnitServicetax, Fee_Id,
            Fee_Slab, newFileName, OriginalFilePath, ChqDdate, PaymentRemarks, ftLastYearTurnOver,
            Bank_BranchID, Bank_AccountNo, Bank_BankName, Bank_IFSC, Bank_Branchname, Bank_LedgerId, Bank_Ledgername,
            intSpecCastid, intCastId, intunitsize, intBranchId, intConst, CFSSUNITID
            );



         
    




        #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB
        int FormType = 3;
        string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
        CFSSService service = new CFSSService();
        string result = service.ProcessPayment(CompanyId, FormType, apiUrl, "RA");
        if (result == "SUCCESS")
        {
            if (tblId == 1)
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=1");
            }
            else if (tblId == 2)
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=2");
            }
            else
            {
                Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?StatusR=3");
            }
        }
        else
        {

            lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
            lblErrorMsg.ForeColor = System.Drawing.Color.Red;
        }

         


        #endregion

        /*
                if (ErrorId == 2)   //INSERT/UPDATE SUCCESSFULL
                {
                    Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?Status=2");
                }
                else //INSERT/UPDATE FAIL
                {
                    Response.Redirect("Online_UnitRenewalStep1_CFSS.aspx?Status=1");
                }
        */
    }

    public class RootResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<DataItem> Data { get; set; }
    }

    public class DataItem
    {
        public string UniqueID { get; set; }
        public string Message { get; set; }
    }

    public void SaveUnitDetailsResponse(string responseString, int CompanyId)
    {
        var result = JsonConvert.DeserializeObject<RootResponse>(responseString);

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();

            if (result.Data != null && result.Data.Count > 0)
            {
                var item = result.Data[0];

                hdnCFFSSUnitId.Value = item.UniqueID;

                using (SqlCommand cmd = new SqlCommand("CFSS_Update_APIUnitResponse", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@JsonData_responseString", responseString);
                    cmd.Parameters.AddWithValue("@UniqueID", item.UniqueID);
                    cmd.Parameters.AddWithValue("@Message", item.Message);
                    cmd.Parameters.AddWithValue("@IsSuccess", result.Success);
                    cmd.Parameters.AddWithValue("@MainMessage", result.Message);
                    cmd.Parameters.AddWithValue("@CompanyId", CompanyId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    public string SaveErrorLog(int? companyId, string responseString, string eventName)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CFSS_SP_Insert_CFSSAPI_Error", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@CompanyId", SqlDbType.Int).Value = (object)companyId ?? DBNull.Value;
                cmd.Parameters.Add("@ResponseString", SqlDbType.NVarChar).Value = (object)responseString ?? DBNull.Value;
                cmd.Parameters.Add("@EventName", SqlDbType.NVarChar).Value = (object)eventName ?? DBNull.Value;
                SqlParameter outId = new SqlParameter("@OutId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outId);

                SqlParameter outMsg = new SqlParameter("@OutMsg", SqlDbType.NVarChar, 200)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(outMsg);

                con.Open();
                cmd.ExecuteNonQuery();
                int insertedId = Convert.ToInt32(cmd.Parameters["@OutId"].Value);
                string message = cmd.Parameters["@OutMsg"].Value.ToString();
                return message;


            }
        }
    }
    public static string GetUnitDetailJson(int companyId, int FormType)
    {

        string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        string jsonResult = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CFSS_PULL_UNITDATA_FOR_CFSS_POSTING_RA", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CompanyId", companyId);
                cmd.Parameters.AddWithValue("@Formtype", FormType);

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    StringBuilder sb = new StringBuilder();

                    while (dr.Read())
                    {
                        sb.Append(dr[0].ToString());
                    }

                    jsonResult = sb.ToString();
                }
            }
        }

        return jsonResult;
    }
    protected void ddlBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<CFSSService.BankDetails> bankList = (List<CFSSService.BankDetails>)Session["BankList"];

        string ledgerID = ddlBank.SelectedValue;

        var selectedBank = bankList.FirstOrDefault(x => x.LedgerID == ledgerID);

        if (selectedBank != null)
        {
            Session["SelectedBank"] = selectedBank;
        }

        trbranch1.Visible = true;
        trMainData.Visible = true;
    }
    //public class BankDetails
    //{
    //    public string BranchID { get; set; }
    //    public string BankAccountNo { get; set; }
    //    public string Bank_Name { get; set; }
    //    public string Bank_IFSC_Code { get; set; }
    //    public string BranchName { get; set; }
    //    public string LedgerID { get; set; }
    //    public string Ledger_Name { get; set; }
    //}
    //public class Root_Bank
    //{
    //    public List<BankDetails> Bank_Details { get; set; }
    //}
    public int Company_Insert_PaymentDetails_CFSS(int CompanyId,int CUnit, int FormType, int PaymentType, string CHQDD_UTR,
        string BankName, string BranchName, string PayableAt, decimal Amount, decimal Servicetax, int Feeid, string FeeDesc, string FileName,
        string FilePath, string PaymentDate, string Remarks, decimal ftLastYearTurnOver,

        string Bank_BranchID, string Bank_AccountNo, string Bank_BankName, string Bank_IFSC, string Bank_Branchname, string Bank_LedgerId , string Bank_Ledgername,
        int SpclCategory, int Castid, int UnitSizeId,
       int Branchid, int ConstId,int CFSSUnitID
        )
    {
        int tblId = 0;
        try
        {

            MySqlComm = new SqlCommand("GPsp_Company_Insert_PaymentDetails_CFSS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_PD_PaymentType", SqlDbType.Int).Value = PaymentType;
            MySqlComm.Parameters.Add("@CUnit", SqlDbType.Int).Value = CUnit;
            MySqlComm.Parameters.Add("@Company_Id", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@FormType", SqlDbType.Int).Value = FormType;

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
            MySqlComm.Parameters.Add("@ftLastYearTurnOver", SqlDbType.Decimal).Value = ftLastYearTurnOver;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_3C123_ConstitutionId", SqlDbType.Int).Value = ConstId;
            MySqlComm.Parameters.Add("@Bank_BranchID", SqlDbType.VarChar).Value = Bank_BranchID;
            MySqlComm.Parameters.Add("@Bank_AccountNo", SqlDbType.VarChar).Value = Bank_AccountNo;
            MySqlComm.Parameters.Add("@Bank_BankName", SqlDbType.VarChar).Value = Bank_BankName;
            MySqlComm.Parameters.Add("@Bank_IFSC", SqlDbType.VarChar).Value = Bank_IFSC;
            MySqlComm.Parameters.Add("@Bank_Branchname", SqlDbType.VarChar).Value = Bank_Branchname;
            MySqlComm.Parameters.Add("@Bank_LedgerId", SqlDbType.VarChar).Value = Bank_LedgerId;
            MySqlComm.Parameters.Add("@Bank_Ledgername", SqlDbType.VarChar).Value = Bank_Ledgername;
            MySqlComm.Parameters.Add("@CFSSUnitID", SqlDbType.Int).Value = CFSSUnitID;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = SpclCategory;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = Castid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = UnitSizeId;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_BranchId", SqlDbType.Int).Value = Branchid;


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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (rdbRenewaltype.SelectedValue == "1")
        {
            Renewal_insert();
            trMainData.Visible = false;
            trRenewalType.Visible = false;
           // ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);

        }
        else if (rdbRenewaltype.SelectedValue == "2")
        {
            RenewalCum_Amendment_insert();

            trMainData.Visible = false;
            trRenewalType.Visible = false;
         //  ScriptManager.RegisterStartupScript(this.UpdatePanel3, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
        }

    }

}