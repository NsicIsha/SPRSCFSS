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


public partial class appAdministrator_BacklogUnitStep1 : System.Web.UI.Page
{

    int UserId, RoleId, DepartmentId;
    string UserFullName;

    int BranchId;
    int BranchID_Check;
    int BranchFlage;

    clsCompany objCompany;

    DataTable dt;
    int ErrorId, CompanyId, UnitId;
    int fStatus;
    int AccessId;
    //int ssistatus;
    public int unitbrAction = 0;
    short tabindx = 9;

    //DataRow[] cityRows;
    string expression = string.Empty;
    DataTable Mydtcity;
    DataTable ddlCityClone = new DataTable();


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

        //TO CONTROL DDL BRANCH FOR UNIT REGISTRATION
        try
        {

         
                //FETCH BRANCHID FOR LOGIN USER
                objCompany = new clsCompany();
                BranchId = objCompany.MasterBranch_Fetch_BranchId(UserId);

                Mydtcity = clsDDL.ddlCity_Activated(0);

                ddlCityClone = Mydtcity.Clone();
                ViewState["mydtcity"] = Mydtcity;

            
            //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


            FunfillDropdown();
            if (!IsPostBack)
            {
                filldataform();
            }

            //ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "ScriptKey2", "javascript:UpdateProgressBar_DroDown();", true);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
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

    private void funfill_ddlCategory()
    {
        //Category Area DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlCategoryArea();
        ddl_1d.DataSource = dt;
        ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
        ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
        ddl_1d.DataBind();
        ddl_1d.Items.Insert(0, new ListItem("--Select Category Area--", ""));

        //Category Cast DROPDOWNLIST
        dt.Clear();
        dt = clsDDL.ddlCategoryCast();
        ddl_1e.DataSource = dt;
        ddl_1e.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
        ddl_1e.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
        ddl_1e.DataBind();
        ddl_1e.Items.Insert(0, new ListItem("--Select Category Cast--", ""));

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
            //ddlCity_b4.Items.Clear();
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
        {/*
            //======UTTAM======================================
            expression = "fGPtbl_MCty_StateId=" + Convert.ToInt32(myDropDown.SelectedItem.Value);

            Mydtcity = (DataTable)ViewState["mydtcity"];
            //ddlCityClone = Mydtcity.Clone();
            cityRows = Mydtcity.Select(expression);

            foreach (DataRow r1 in cityRows)
            {
                ddlCityClone.ImportRow(r1);
            }
            //===============================================
            */
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

        /*
       secondDropDownList.DataSource = ddlCityClone;
       secondDropDownList.DataTextField = ddlCityClone.Columns["fGPtbl_MCty_Name"].ToString();
       secondDropDownList.DataValueField = ddlCityClone.Columns["fGPtbl_MCty_Id"].ToString();
       */
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

    protected void txt_1_TextChanged(object sender, EventArgs e)
    {
        lblPANmsg.Visible = false;

        //System.Threading.Thread.Sleep(5000);
        string fPANno = "";
        //string ssiNo = "";
        fPANno = txt_1.Text.ToString().ToUpper();
        int fPANlength = 0;
        fPANlength = fPANno.Length;
        if (fPANlength != 10)
        {
            txt_1.Focus();
        }
        //ssiNo = txt_21.Text.ToString();
       
        objCompany = new clsCompany();
        DataSet ds_status = new DataSet();
        ds_status = objCompany.Company_PANnoCheck_Modified(fPANno);
        if (ds_status.Tables[0].Rows.Count > 0)
        {
            fStatus = Convert.ToInt32(ds_status.Tables[0].Rows[0]["Status"].ToString());
            AccessId = Convert.ToInt32(ds_status.Tables[0].Rows[0]["AccessId"].ToString());
            BranchID_Check = Convert.ToInt32(ds_status.Tables[0].Rows[0]["BranchID"].ToString());
        }
        else
        {
            ds_status.Dispose();
        }

        if (BranchId == BranchID_Check)
        {
            BranchFlage = 0;
        }
        else
        {
            BranchFlage = 1;
            if (fStatus == 123 && BranchFlage == 1)
            {
                lbl_branchName_chek.Visible = false;
                lbl_branchName_chek.Text = "";
            }
            else
            {
                btnSubmit.Enabled = false;
              //  (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "<script>alert('Branch Not exist !!!!');</script>", true);
                lbl_branchName_chek.Visible = true;
                lbl_branchName_chek.Text = "Branch Not exist !!!!";
            }

        }
       // fStatus = objCompany.Company_PANnoCheck(fPANno);  //1Pan 
        //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);

        Session["Status"] = fStatus.ToString();
       
        if (fStatus == 1)  //CONFIRM
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already confirmed";
        }
        else if (fStatus == 2)  //INPROCESS
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with INPROCESS status";
        }
        else if (fStatus == 3)  //WAITING
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with WAITING status";
        }
        else if (fStatus == 4)  //EXPIRED
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with EXPIRED status";
        }
        else if (fStatus == 5 && AccessId==0)  //INCOMPLETE
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using New Unit Registration link.";
        }
        else if (fStatus == 5 && AccessId == 4)
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = true;
            lblPANmsg.Text = "PAN already exists with INCOMPLETE status.";     
        }

        else if (fStatus == 6) //INSPECTION PASSED
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "Company has been blacklisted";
        }

        else if (fStatus == 7) //INSPECTION PASSED
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exist with INSPECTION PASSED status";
        }
        else if (fStatus == 10) //EXISTS WITH OTHER BRANCH
        {
            lblPANmsg.Visible = true;
            btnSubmit.Enabled = false;
            lblPANmsg.Text = "PAN already exists with other branch";
        }
        else if (fStatus == 123)  //NEW ENTRY
        {
            lblPANmsg.Visible = false;
            btnSubmit.Enabled = true;

            //txt_1p1.Text = "";
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
            txt_mob.Text = "";
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

            GridFactory.DataSource = objCompany.Fetch_Max_Factory();

            GridFactory.DataBind();

        }

        if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1 || fStatus == 7)
        {
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDrGrid;
            MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);

            GridFactory.DataSource = MyDrGrid;
            GridFactory.DataBind();

           
            System.Data.SqlClient.SqlDataReader MyDr;
            MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);

            while (MyDr.Read())
            {
                CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                hdnCompany.Value = CompanyId.ToString();
                
                txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
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
                rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();
                try
                {
                    ddlState_c3.SelectedValue = MyDr["fGPtbl_CUnitBranch_StateId"].ToString();
                }
                catch { }

                try
                {
                    if (MyDr["fGPtbl_CUnitBranch_StateId"].ToString() == "")
                    {

                    }
                    else
                    {
                        funfill_ddlCity_c4(Convert.ToInt32(MyDr["fGPtbl_CUnitBranch_StateId"].ToString()));
                    }
                    if (MyDr["fGPtbl_CUnitBranch_CityId"].ToString() == "")
                    {
                    }
                    else
                    {
                        ddlCity_c4.SelectedValue = MyDr["fGPtbl_CUnitBranch_CityId"].ToString();
                    }
                }
                catch { }

                if (BranchId != (Convert.ToInt32(MyDr["fGPtbl_CUnit_BranchId"].ToString())))
                {
                    btnSubmit.Visible = true;
                }

            }
            MyDr.Close();
        }

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
            DropDownList ddlcity = (DropDownList)e.Row.FindControl("ddlCity_b4");


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

    // Pass Step2 to step  1 when click back button of new unit resigstration part 1 

    public void filldataform()
    {
        // lblPANmsg.Visible = false;
        if (Request.QueryString["pcd"] != null)
        {

            DecryptEncrypt objDecEnc = new DecryptEncrypt();

            string prevPage = (objDecEnc.Decrypt(Request.QueryString["pcd"].Replace(" ", "+")).ToString());


            // System.Threading.Thread.Sleep(5000);
            string fPANno = "";
            //string ssiNo = "";
            fPANno = prevPage;
            txt_1.Text = prevPage.ToString();
            int fPANlength = 0;
            fPANlength = fPANno.Length;
            if (fPANlength != 10)
            {
                txt_1.Focus();
            }
            //ssiNo = txt_21.Text.ToString();

            objCompany = new clsCompany();
            //fStatus = objCompany.Company_PANnoCheck(fPANno, BranchId);
            DataSet ds_status_update = new DataSet();
            ds_status_update = objCompany.Company_PANnoCheck_Modified(fPANno);
            if (ds_status_update.Tables[0].Rows.Count > 0)
            {
                fStatus = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["Status"].ToString());
                AccessId = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["AccessId"].ToString());
                BranchID_Check = Convert.ToInt32(ds_status_update.Tables[0].Rows[0]["BranchID"].ToString());
            }
            else
            {
                ds_status_update.Dispose();
            }

            if (BranchId == BranchID_Check)
            {
                BranchFlage = 0;
            }
            else
            {
                BranchFlage = 1;
                if (fStatus == 123 && BranchFlage == 1)
                {
                    lbl_branchName_chek.Visible = false;
                    lbl_branchName_chek.Text = "";
                }
                else
                {
                    btnSubmit.Enabled = false;
                   // (HttpContext.Current.CurrentHandler as Page).RegisterClientScriptBlock("S", "<script>alert('Branch Not exist !!!!'); window.close();</script>");
                    lbl_branchName_chek.Visible = true;
                    lbl_branchName_chek.Text="Branch Not exist !!!!";

                }

            }

           // fStatus = objCompany.Company_PANnoCheck(fPANno); //2Pan

            Session["Status"] = fStatus.ToString();
            if (fStatus == 1)  //CONFIRM
            {
                  
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already confirmed";
            }
            else if (fStatus == 2)  //INPROCESS
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with INPROCESS status";
            }
            else if (fStatus == 3)  //WAITING
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with WAITING status";
            }
            else if (fStatus == 4)  //EXPIRED
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with EXPIRED status";
            }
            else if (fStatus == 5 && AccessId==0)  //INCOMPLETE
            {
               // string url = HttpContext.Current.Request.Url.AbsoluteUri;
               
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN already exists with INCOMPLETE status.<br>This has been filled using New Unit Registration link.";
            }
            else if (fStatus == 5 && AccessId == 4)
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = true;
                lblPANmsg.Text = "PAN already exists with INCOMPLETE status.";  
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
                txt_mob.Text = "";
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

                GridFactory.DataSource = objCompany.Fetch_Max_Factory();

                GridFactory.DataBind();

            }

            if (fStatus == 5 || fStatus == 3 || fStatus == 2 || fStatus == 1)
            {
                objCompany = new clsCompany();
                System.Data.SqlClient.SqlDataReader MyDrGrid;
                MyDrGrid = objCompany.Company_Fetch_ByPANno_Step1_Grid(fPANno);

                GridFactory.DataSource = MyDrGrid;
                GridFactory.DataBind();


               
                System.Data.SqlClient.SqlDataReader MyDr;
                MyDr = objCompany.Company_Fetch_ByPANno_Step1(fPANno);

                while (MyDr.Read())
                {
                    CompanyId = Convert.ToInt32(MyDr["fGPtbl_Company_Id"].ToString());
                    UnitId = Convert.ToInt32(MyDr["fGPtbl_CUnit_Id"].ToString());
                    hdnCompany.Value = CompanyId.ToString();
                    txt_1p1.Text = MyDr["fGPtbl_Company_Name"].ToString();
                    txt_1a1.Text = MyDr["fGPtbl_Company_HeadOffice_Address"].ToString();
                    txt_1a2.Text = MyDr["fGPtbl_Company_HeadOffice_Zip"].ToString();
                    rdo_manufacturlist.SelectedValue = MyDr["fGPtbl_CUnit_Enterprise"].ToString();
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
                    //   txt_1p1.Text = MyDr["fGPtbl_CUnit_Enterprise"].ToString();


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

        else
        {

        }

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["Status"] != null || Session["Status"] != "")
            {
                fStatus = Convert.ToInt32(Session["Status"].ToString());
            }
        }
        catch { }

        //FOR PROVISIONAL COMPANY
        int provision = 0;// Convert.ToInt32(Request.QueryString["i"]);

        string bsPANno = txt_1.Text.Trim().ToUpper();
        // Session["PANNumber"] = bsPANno.ToString();
        string bsNameofApplicantFirm = txt_1p1.Text.Trim();
        string bsHOAddress = txt_1a1.Text.Trim();
        string bsHOPinCode = txt_1a2.Text.Trim();
        int bsHOState = 0;


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
        try
        {
            // bsFTState = Convert.ToInt32(ddlState_b3.SelectedValue);
        }
        catch { }
        int bsFTCity = 0;
        try
        {
            // bsFTCity = Convert.ToInt32(ddlCity_b4.SelectedValue);
        }
        catch { }


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


        objCompany = new clsCompany();
        clsCompanyBacklog objCompanyBacklog = new clsCompanyBacklog();
        if (fStatus == 123) //NEW ENTRY
        {
            clsGenerateNo objGenerate = new clsGenerateNo();
            string ReferenceNo = objGenerate.Company_ReferenceNo(BranchId);

            int i = 0;
            while (i < 5)
            {
                Hashtable hstbl = new Hashtable();
                string bsFTAddress = "", bsFTPinCode = "", bsFTTelephone = "", bsFTFax = "", bsFTEmail = "";

                //TextBox txtadd = (TextBox)GridFactory.Rows[0].Cells[0].FindControl("txt_1b1");

                TextBox txtadd = (TextBox)GridFactory.Rows[i].FindControl("txt_1b1");

                if ((txtadd.Text).Trim() != "")
                {

                    TextBox txtpin = (TextBox)GridFactory.Rows[i].FindControl("txt_1b2");
                    TextBox txtphn = (TextBox)GridFactory.Rows[i].FindControl("txt_1b5");
                    TextBox txtfax = (TextBox)GridFactory.Rows[i].FindControl("txt_1b6");
                    TextBox txtemail = (TextBox)GridFactory.Rows[i].FindControl("txt_1b7");
                    DropDownList ddlftstate = (DropDownList)GridFactory.Rows[i].FindControl("ddlState_b3");
                    DropDownList ddlftcity = (DropDownList)GridFactory.Rows[i].FindControl("ddlCity_b4");

                    bsFTAddress = txtadd.Text;
                    bsFTPinCode = txtpin.Text;
                    bsFTTelephone = txtphn.Text;
                    bsFTFax = txtfax.Text;
                    bsFTEmail = txtemail.Text;
                    bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);


                    hstbl = objCompanyBacklog.Company_Insert_Step1(ReferenceNo, bsNameofApplicantFirm, bsPANno, bsHOAddress,
                        bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone, bsHOFax, bsHOEmail,
                        bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail,
                        bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, BranchId, bsBRAddress, bsBRPin,
                        bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail, UserId, provision, Convert.ToInt32(rdo_manufacturlist.SelectedValue), txt_mob.Text.Trim());

                    CompanyId = Convert.ToInt32(hstbl["CompanyId"].ToString());
                    UnitId = Convert.ToInt32(hstbl["UnitId"].ToString());
                    ErrorId = Convert.ToInt32(hstbl["ErrorId"].ToString());
                    
                }

                i = i + 1;

            }

        }
        else if (fStatus == 5)
        {

            int i = 0;
            while (i < 5)
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

                bsFTAddress = txtadd.Text;
                bsFTPinCode = txtpin.Text;
                bsFTTelephone = txtphn.Text;
                bsFTFax = txtfax.Text;
                bsFTEmail = txtemail.Text;//================================================



                if (UnitId != 0 && txtadd.Text.Trim() != "")
                {
                    //bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    //bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);

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


                    ErrorId = objCompanyBacklog.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                        bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                        bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity, bsBRPhone, bsBRFax, bsBREmail,
                        UserId, CompanyId, UnitId, provision, Convert.ToInt32(rdo_manufacturlist.SelectedValue), txt_mob.Text.Trim());
                      
                     
                }
                else if (UnitId == 0 && txtadd.Text.Trim() != "")
                {
                    //bsFTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    // bsFTCity = Convert.ToInt32(ddlftcity.SelectedValue);
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


                    ErrorId = objCompanyBacklog.Company_Update_Step1(bsNameofApplicantFirm, bsHOAddress, bsHOPinCode, bsHOState, bsHOCity, bsHOTelephone,
                       bsHOFax, bsHOEmail, bsFTAddress, bsFTPinCode, bsFTState, bsFTCity, bsFTTelephone, bsFTFax, bsFTEmail, bsCategoryAreaId,
                       bsCategoryCastId, bsCategoryUnitSizeId, bsBRAddress, bsBRPin, bsBRState, bsBRCity,
                       bsBRPhone, bsBRFax, bsBREmail, UserId, CompanyId, UnitId, provision, Convert.ToInt32(rdo_manufacturlist.SelectedValue), txt_mob.Text.Trim());

                    
                }

                if (BranchId == 0)
                {
                    BranchId = objCompany.Company_Fetch_BranchId(UnitId);
                }

                i = i + 1;

            }

        }

        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(fStatus.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string PanCard = objDecEnc.Encrypt(txt_1.Text.ToString());
            Response.Redirect("BacklogUnitStep2.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + PanCard + "&Edit=0", false);
        }
        else               //INSERT/UPDATE FAIL
        {

        }
    }



}
