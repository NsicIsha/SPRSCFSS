using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_AdminEditRegistration : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int BranchId;
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataTable dt;
    int ErrorId, CompanyId, UnitId;
    int fStatus;
    int ssistatus;
    public int unitbrAction = 0;
    short tabindx = 9;
    DataRow[] cityRows;
    string expression = string.Empty;
    DataTable Mydtcity;
    DataTable ddlCityClone = new DataTable();
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            Session.Remove("step2");
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
            if (Request.QueryString["cid"] != null)
            {
                CompanyId = Convert.ToInt32(Request.QueryString["cid"]);
            }
            // HARD CODED TO BE REMOVED
            //CompanyId = 18942;

            FunfillDropdown();
            if (!IsPostBack)
            {
                filldataform();
            }

            ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ScriptKey2", "javascript:UpdateProgressBar_DroDown();", true);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in page_load() " + ex.StackTrace + "  " + ex.Message);
        }
    }//PAGE LOAD

    protected void ddlConstitution_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////txt_1.Text = "";
        ////txt_aahar.Text = "";
        //if (ddlConstitution.SelectedValue != "0")
        //{
        //    txt_1.Enabled = true;
        //}
        //else
        //{
        //    txt_1.Enabled = false;
        //    txt_1.Text = "";
        //    txt_aahar.Text = "";
        //}

        //if (ddlConstitution.SelectedValue != "4")
        //{
        //    Requ_addhar.Enabled = false;
        //}
        //else
        //{
        //    Requ_addhar.Enabled = true;
        //}
    }
    private void FunfillDropdown()
    {
        if (!Page.IsPostBack)
        {

            funfill_ddlState();
            funfill_ddlCategory();
            objCompany = new clsCompany();
            GridFactory.DataSource = objCompany.Fetch_Max_Factory();
            GridFactory.DataBind();

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

        ddlState_c3.DataSource = dt;
        ddlState_c3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_c3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_c3.DataBind();
        ddlState_c3.Items.Insert(0, new ListItem("--Select State--", ""));

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

    private DataTable funfill_ddlCity_b4(int fStateId, DropDownList ddl)
    {
        //CITY DROPDOWNLIST
        System.Data.DataTable dt_c = new System.Data.DataTable();
        if (fStateId != 0)
        {
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            return dt_c;
        }
        else
        {
            return dt_c;
        }
    }

    private void funfill_ddlCity_c4(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlCity_c4.DataSource = dt_c;
            ddlCity_c4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity_c4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity_c4.DataBind();
            ddlCity_c4.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();
        }
        else
        {
            ddlCity_c4.Items.Clear();
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

    protected void ddlState_b3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList myDropDown = (sender as DropDownList);

        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(myDropDown.SelectedItem.Value);
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }

        GridViewRow gridViewRowDdlBelongsTo = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList secondDropDownList = (DropDownList)gridViewRowDdlBelongsTo.FindControl("ddlCity_b4");

        System.Data.DataTable dt_c = new System.Data.DataTable();

        dt_c = funfill_ddlCity_b4(fStateId, secondDropDownList);
        secondDropDownList.DataSource = dt_c;
        secondDropDownList.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
        secondDropDownList.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();

        secondDropDownList.DataBind();
        secondDropDownList.Items.Insert(0, new ListItem("--Select City--", ""));
    }

    protected void ddlState_c3_SelectedIndexChanged(object sender, EventArgs e)
    {
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlState_c3.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlCity_c4(fStateId);
    }

    private void funfill_ddlState_Factory()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();

        ddlState_c3.DataSource = dt;
        ddlState_c3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_c3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_c3.DataBind();
        ddlState_c3.Items.Insert(0, new ListItem("--Select State--", ""));

        dt.Clear();
        dt.Dispose();
    }

    protected void GridFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (!IsPostBack)
        //{


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Bind drop down to cities           
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlState_b3");

            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsDDL.ddlState_Activated();

            ddl.DataSource = dt;
            ddl.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
            ddl.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("--Select State--", ""));

            dt.Clear();
            dt.Dispose();

            HiddenField hdnstate = (HiddenField)e.Row.FindControl("hdnstate");
            HiddenField hdncity = (HiddenField)e.Row.FindControl("hdncity");
            HiddenField hdnenterprise = (HiddenField)e.Row.FindControl("hdnenterprise");
            DropDownList ddlcity = (DropDownList)e.Row.FindControl("ddlCity_b4");
            DropDownList ddlNatureOfOperation_b8 = (DropDownList)e.Row.FindControl("ddlNatureOfOperation_b8");
            ddlNatureOfOperation_b8.SelectedValue = hdnenterprise.Value;

            if (Convert.ToInt32(hdnstate.Value) != 0)
            {
                ddl.SelectedValue = (hdnstate.Value.ToString());

                System.Data.DataTable dt_c = new System.Data.DataTable();
                dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl.SelectedValue));
                ddlcity.DataSource = dt_c;
                ddlcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
                ddlcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select City--", ""));
                dt_c.Clear();
                dt_c.Dispose();

                ddlcity.SelectedValue = (hdncity.Value.ToString());

            }
            else
            {
                ddlcity.Items.Insert(0, new ListItem("--Select City--", ""));
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
            ddl.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            ddlcity.TabIndex = (short)(tabindx);
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

        }


    }

    public DataSet FetchCompanyPAN_By_CompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchCompanyPAN_By_CompanyId", DBManager.ConnectToDB());
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

    //public int Company_PANnoCheck(string PANno, int branchid)
    public int Company_PANnoCheck(string PANno, string Uam)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CheckPANno_Proprietorship", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@PANno", SqlDbType.Char, 10).Value = PANno;
            MySqlComm.Parameters.Add("@Uam", SqlDbType.Char, 50).Value = Uam;
            //MySqlComm.Parameters.Add("@Branchid", SqlDbType.Int).Value = branchid;
            Status = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return Status;
    }
    public void filldataform()
    {
        objCompany = new clsCompany();
        DataSet panDS = new DataSet();
        clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string fPANno = string.Empty;


        

        int ConstitutionId = 0;
        string fPANnoNew = "";
   
        string fUamNo = "";

        DataSet dss = new DataSet();
        clsCompany objComp = new clsCompany();
        dss = FetchCompanyPAN_By_CompanyId(Convert.ToInt32(CompanyId));

        fPANnoNew = dss.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
        fUamNo = dss.Tables[0].Rows[0]["fGtbl_Company_AdharNumber"].ToString();

        if (dss.Tables[0].Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString() != "")
        {
            ConstitutionId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString());
        }
        else
        {
            ConstitutionId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_ConstitutionId"].ToString());
        }

       // ConstitutionId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString());

        ddlConstitution.Items.Clear();

        objCompany = new clsCompany();
        System.Data.DataTable Dtt = new System.Data.DataTable();
        Dtt = objCompany.Company_Fetch_Constitution();
        ddlConstitution.DataTextField = Dtt.Columns["fGPtbl_CUnit_Constitution_Name_POI"].ToString();
        ddlConstitution.DataValueField = Dtt.Columns["fGPtbl_CUnit_Constitution_Id"].ToString();
        ddlConstitution.DataSource = Dtt;
        ddlConstitution.DataBind();
        ddlConstitution.Items.Insert(0, new ListItem("--Select Constitution of the firm--", "0"));

        ddlConstitution.Items.FindByValue(ConstitutionId.ToString()).Selected = true;


        panDS = objCompanyEdit.Fetch_CompanyPAN_Status_ByCompanyId(CompanyId);
        fPANno = panDS.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();
        txt_1.Text = fPANno;
        int fPANlength = 0;
        fPANlength = fPANno.Length;
        if (fPANlength != 10)
        {
            txt_1.Focus();
        }

       // fStatus = objCompany.Company_PANnoCheck(fPANno);
        //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);

        if (ConstitutionId!=4)
        {
            fStatus = objCompany.Company_PANnoCheck(fPANno);
        }
        else
        {
            fStatus = Company_PANnoCheck(fPANno, fUamNo);
        }



        Session["Status"] = fStatus.ToString();
        if (fStatus == 1)  //CONFIRM
        {
            lblPANmsg.Visible = true;
            //btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already confirmed";
        }
        else if (fStatus == 2)  //INPROCESS
        {
            lblPANmsg.Visible = true;
            //btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with INPROCESS status";
        }
        else if (fStatus == 3)  //WAITING
        {
            lblPANmsg.Visible = true;
            //btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with WAITING status";
        }
        else if (fStatus == 4)  //EXPIRED
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with EXPIRED status";
        }
        else if (fStatus == 5)  //INCOMPLETE
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = true;
            lblPANmsg.Text = "PAN already exists with INCOMPLETE status";
        }
        else if (fStatus == 123)  //NEW ENTRY
        {
            lblPANmsg.Visible = false;
            btnSubmit.Enabled = true;

            txt_1p1.Text = "";
            txt_1a1.Text = "";
            txt_1a2.Text = "";
            try
            {
                ddlState_a3.SelectedValue = "";
                ddlCity_a4.Items.Clear();
            }
            catch { }

            txt_1a5.Text = "";
            txt_1a6.Text = "";
            txt_1a7.Text = "";

            try
            {
                ddl_1d.SelectedValue = "";
            }
            catch { }

            try
            {
                ddl_1f.SelectedValue = "";
            }
            catch { }

            txt_1c1.Text = "";
            txt_1c2.Text = "";
            txt_1c5.Text = "";
            txt_1c6.Text = "";
            txt_1c7.Text = "";

            try
            {
                ddlState_c3.SelectedValue = "";
                ddlCity_c4.Items.Clear();
            }
            catch { }

            /*  GridFactory.DataSource = objCompany.Fetch_Max_Factory();

              GridFactory.DataBind();*/

        }

        if (fStatus != 5 || fStatus != 4)
        {
            objCompany = new clsCompany();
            clsCompanyInformationEdit objComEdit = new clsCompanyInformationEdit();
            DataSet MydsGrid = new DataSet();
            //System.Data.SqlClient.SqlDataReader MyDrGrid;
            //MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);

            MydsGrid = objComEdit.Fetch_Factorydata_BycompanyId(CompanyId);

            GridFactory.DataSource = MydsGrid;
            GridFactory.DataBind();

            System.Data.SqlClient.SqlDataReader MyDr;
           // MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);
            if(ddlConstitution.SelectedValue != "4")
                {
                MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);
            }
                else
                {
                MyDr = Company_Fetch_ByPANno_Step1(fPANno, fUamNo);
            }



            while (MyDr.Read())
            {
                CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                hdnCompany.Value = CompanyId.ToString();
                txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                if (txt_1p1.Text != "")
                {
                    //txt_1p1.Attributes.Add("readonly", "readonly");removed readonly to make company name edit possible
                }
                bool fGPtbl_Company_MSME = MyDr["fGPtbl_Company_MSME"].ToString() != "" ? Convert.ToBoolean(MyDr["fGPtbl_Company_MSME"]) : false;
                rdo_declarationMSME.SelectedValue = fGPtbl_Company_MSME == true ? "1" : "2";
                bool fGPtbl_Company_ShareInfo = MyDr["fGPtbl_Company_ShareInfo"].ToString() != "" ? Convert.ToBoolean(MyDr["fGPtbl_Company_ShareInfo"]) : false;
                rdoShareInfo.SelectedValue = fGPtbl_Company_ShareInfo == true ? "1" : "2";
                txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                txt_proposal_date.Text = MyDr["fGPtbl_Company_ProposalDate"].ToString();
                txt_aahar.Text = MyDr["fGtbl_Company_AdharNumber"].ToString();
                try
                {
                    ddlState_a3.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString();
                }
                catch { }
                try
                {
                    funfill_ddlCity_a4(Convert.ToInt32(MyDr["fGPtbl_Company_HeadOffice_StateId"].ToString()));
                    ddlCity_a4.SelectedValue = MyDr["fGPtbl_Company_HeadOffice_CityId"].ToString();
                }
                catch { }
                txt_1a5.Text = MyDr["fGPtbl_Company_HeadOffice_Phone"].ToString();
                txt_1a6.Text = MyDr["fGPtbl_Company_HeadOffice_Fax"].ToString();
                txt_1a7.Text = MyDr["fGPtbl_Company_HeadOffice_Email"].ToString();
                txt_mob.Text = MyDr["fGPtbl_Company_HeadOffice_Mobile"].ToString();

                try
                {
                    ddl_1d.SelectedValue = MyDr["fGPtbl_CUnit_CategoryAreaId"].ToString();
                }
                catch { }
                try
                {
                    ddl_1e.SelectedValue = MyDr["fGPtbl_CUnit_CategoryCastId"].ToString();
                }
                catch { }
                try
                {
                    ddl_1f.SelectedValue = MyDr["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
                }
                catch { }

                txt_1c1.Text = MyDr["fGPtbl_CUnitBranch_Address"].ToString();
                txt_1c2.Text = MyDr["fGPtbl_CUnitBranch_Zip"].ToString();
                txt_1c5.Text = MyDr["fGPtbl_CUnitBranch_Phone"].ToString();
                txt_1c6.Text = MyDr["fGPtbl_CUnitBranch_Fax"].ToString();
                txt_1c7.Text = MyDr["fGPtbl_CUnitBranch_Email"].ToString();
                //rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();

                try
                {
                    ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                }
                catch { }

                try
                {
                    funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                    ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                }
                catch { }

            }
            MyDr.Close();
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

    public SqlDataReader Company_Fetch_ByPANno_Step1_Grid(string PANno, string AadhaarNo)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_FetchByPAN_Step1_Grid_Proprietorship", DBManager.ConnectToDB());
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
        objCompany = new clsCompany();
        try
        {
            if (Session["Status"] != null || Convert.ToString(Session["Status"]) != "")
            {
                fStatus = Convert.ToInt32(Session["Status"].ToString());
            }
        }
        catch { }

        // To insert Data in Company_history_table
        if (Request.QueryString.HasKeys())
        {
            if (Request.QueryString["back"] != null)
            {

            }
            else
            {
                objCompany = new clsCompany();
                int m = objCompany.Company_Insert_HistoryTableCompanyEditData(CompanyId);
                if (m == 1)
                {
                }
            }
        }

        //FOR PROVISIONAL COMPANY
        int provision = objCompany.Company_Provisional_Status(CompanyId);

        string bsPANno = txt_1.Text.Trim().ToUpper();
        string bsNameofApplicantFirm = txt_1p1.Text.Trim();
        string bsHOAddress = txt_1a1.Text.Trim();
        string bsHOPinCode = txt_1a2.Text.Trim();
        string ProposalDate = txt_proposal_date.Text.ToString() == "" ? "" : txt_proposal_date.Text.ToString();
        int bsHOState = 0;
        bool bsDeclarationMSME = rdo_declarationMSME.SelectedValue == "1" ? true : false;
        bool bsShareInfo = rdoShareInfo.SelectedValue == "1" ? true : false;
        try
        {
            bsHOState = Convert.ToInt32(ddlState_a3.SelectedValue);
        }
        catch { }
        int bsHOCity = 0;
        try
        {
            bsHOCity = Convert.ToInt32(ddlCity_a4.SelectedValue);
        }
        catch { }
        string bsHOTelephone = txt_1a5.Text.Trim();
        string bsHOFax = txt_1a6.Text.Trim();
        string bsHOEmail = txt_1a7.Text.Trim();

        int bsFTState = 0;
        int bsFTCity = 0;

        //Code Added by Anupam 18 January 2012
        int bsCategoryAreaId = 0;
        try
        {
            bsCategoryAreaId = Convert.ToInt32(ddl_1d.SelectedValue);
        }
        catch { }

        int bsCategoryCastId = 0;
        try
        {
            bsCategoryCastId = Convert.ToInt32(ddl_1e.SelectedValue);
        }
        catch { }

        int bsCategoryUnitSizeId = 0;
        try
        {
            bsCategoryUnitSizeId = Convert.ToInt32(ddl_1f.SelectedValue);
        }
        catch { }

        string bsBRAddress = "";
        string bsBRPin = "";
        string bsBRPhone = "";
        string bsBRFax = "";
        string bsBREmail = "";
        string Aadhar_card = "";

        //dhiraj
        string GST_Number = "";

        if (txtGstNumber.Text.Trim().ToString() != "")
        {
            GST_Number = txtGstNumber.Text.Trim().ToString();
        }

        //Dhiraj NSIC 19/07/2017
        //bool bsSCSTInitiaveInfo = rdbSCSTInitiative.SelectedValue == "1" ? true : false;
        //Dhiraj NSIC 19/07/2017
        int bsSCSTInitiaveInfo = 0;
        if (rdbSCSTInitiative.SelectedValue == "1")
        {
            bsSCSTInitiaveInfo = 1;
        }
        else
        {
            bsSCSTInitiaveInfo = 2;
        }

        if (txt_aahar.Text.Trim().ToString() != "")
        {
            Aadhar_card = txt_aahar.Text.Trim().ToString();
        }




        if (txt_1c1.Text.Trim().ToString() != "")
        {
            bsBRAddress = txt_1c1.Text.Trim().ToString();
        }

        if (txt_1c2.Text.Trim().ToString() != "")
        {
            bsBRPin = txt_1c2.Text.Trim().ToString();
        }

        if (txt_1c5.Text.Trim().ToString() != "")
        {
            bsBRPhone = txt_1c5.Text.Trim().ToString();
        }

        if (txt_1c6.Text.Trim().ToString() != "")
        {
            bsBRFax = txt_1c6.Text.Trim().ToString();
        }

        if (txt_1c7.Text.Trim().ToString() != "")
        {
            bsBREmail = txt_1c7.Text.Trim().ToString();
        }

        int bsBRState = 0;
        int bsBRCity = 0;

        try
        {
            if (ddlState_c3.SelectedIndex > 0)
            {
                bsBRState = Convert.ToInt32(ddlState_c3.SelectedValue);
            }

        }
        catch { }

        try
        {
            if (ddlCity_c4.SelectedIndex > 0)
            {
                bsBRCity = Convert.ToInt32(ddlCity_c4.SelectedValue);
            }

        }
        catch { }
        clsCompanyInformationEdit objcomedit = new clsCompanyInformationEdit();
        int i = 0;
        int factcount = objcomedit.Fetch_Factorycount_BycompanyId(CompanyId);
        while (i < factcount)
        {
            Hashtable hstbl = new Hashtable();
            string bsFTAddress = "", bsFTPinCode = "", bsFTTelephone = "", bsFTFax = "", bsFTEmail = "";

            //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");

            TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");
            HiddenField hdnfld = (HiddenField)GridFactory.Rows[i].FindControl("hdnunit");
            CompanyId = Convert.ToInt32(hdnCompany.Value.ToString());
            UnitId = Convert.ToInt32(hdnfld.Value.ToString());

            TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
            TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
            TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
            TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
            DropDownList ddlftstate = (DropDownList)GridFactory.Rows[i].FindControl("ddlState_b3");
            DropDownList ddlftcity = (DropDownList)GridFactory.Rows[i].FindControl("ddlCity_b4");
            //RA
            DropDownList ddlftNatureofOperation = (DropDownList)GridFactory.Rows[i].FindControl("ddlNatureOfOperation_b8");
            //RA
            bsFTAddress = txtadd.Text;
            bsFTPinCode = txtpin.Text;
            bsFTTelephone = txtphn.Text;
            bsFTFax = txtfax.Text;
            bsFTEmail = txtemail.Text;//================================================

            if (UnitId != 0 && txtadd.Text.Trim() != "")
            {

                if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "") // Changes By Ravish Kumar Pandey
                {
                    bsFTState = 0;
                }
                else
                {
                    bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                }
                if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "") // Changes By Ravish Kumar Pandey
                {
                    bsFTCity = 0;
                }
                else
                {
                    bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                }

                int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);
                ErrorId = objCompany.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                    bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                    bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail, UserId, CompanyId, UnitId, provision, bsFTNatureOfOperation
                    , txt_mob.Text.Trim(), ProposalDate, Aadhar_card, bsDeclarationMSME, bsShareInfo, GST_Number, bsSCSTInitiaveInfo);
            }
            else if (UnitId == 0 && txtadd.Text.Trim() != "")
            {
                if (ddlftstate.SelectedValue == "0" || ddlftstate.SelectedValue == "") // Changes By Ravish Kumar Pandey
                {
                    bsFTState = 0;
                }
                else
                {
                    bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                }
                if (ddlftcity.SelectedValue == "0" || ddlftcity.SelectedValue == "") // Changes By Ravish Kumar Pandey
                {
                    bsFTCity = 0;
                }
                else
                {
                    bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
                }

                int bsFTNatureOfOperation = Convert.ToInt32(ddlftNatureofOperation.SelectedValue);

                ErrorId = objCompany.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                   bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                   bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail, UserId, CompanyId, UnitId, provision, bsFTNatureOfOperation
                    , txt_mob.Text.Trim(), ProposalDate, Aadhar_card, bsDeclarationMSME, bsShareInfo, GST_Number, bsSCSTInitiaveInfo);

            }

            if (BranchId == 0)
            {
                BranchId = objCompany.Company_Fetch_BranchId(UnitId);
            }

            i = i + 1;

        }



        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(fStatus.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string PanCard = objDecEnc.Encrypt(txt_1.Text.ToString());

            clsCompany objCons = new clsCompany();
            int aa = UpadteConstitution_Properitership(CompanyId, Convert.ToInt32(ddlConstitution.SelectedValue));

            string ConstitutionId = objDecEnc.Encrypt(ddlConstitution.SelectedValue.ToString());

          //  Response.Redirect("NewUnitRegistrationStep2.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + PanCard + "&Edit=1", false);
            Response.Redirect("NewUnitRegistrationStep2.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + PanCard + "&con=" + ConstitutionId + "&Edit=1", false);

        }
        else               //INSERT/UPDATE FAIL
        {

        }
    }

    public int UpadteConstitution_Properitership(int CompanyId, int ConstitutionId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_Constitution_Properitership", DBManager.ConnectToDB());//GP_SP_insert_update_company_HSCode
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@ConstitutionId", SqlDbType.VarChar, 50).Value = ConstitutionId;
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


    protected void txt_aahar_TextChanged(object sender, EventArgs e)
    {
        lbl_adhar_number.Visible = false;
        string Aadharnumber = "";
        string checkstatus = "";
        Aadharnumber = txt_aahar.Text.ToString();
        try
        {
            if (Aadharnumber.Length != 12)
            {
                txt_aahar.Focus();
            }
            objCompany = new clsCompany();
            DataSet ds_status = new DataSet();
            ds_status = objCompany.Company_adharCheck_Modified(Aadharnumber.ToString());
            if (ds_status != null && ds_status.Tables[0].Rows.Count > 0)
            {
                checkstatus = Convert.ToString(ds_status.Tables[0].Rows[0]["Status"].ToString());
            }
            else
            {
                ds_status.Dispose();
            }
            if (checkstatus != "123")
            {
                lbl_adhar_number.Visible = true;
                lbl_adhar_number.Text = "Please enter difrent Aadhar number.";
                //  btnSubmit.Enabled = false;
                txt_aahar.Text = "";
                txt_aahar.Focus();
            }

        }
        catch (Exception ex)
        {
        }
    }
}
