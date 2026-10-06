using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Online_Unit_Ren_Amnd_Step1 : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;


    clsCompany objCompany;
    clsAmendment objAmendment;
    int ErrorId, CompanyId, UnitId, Formtypeid, BranchId;
    int fStatus;
    public int unitbrAction = 0;
    short tabindx = 9;

    DataRow[] cityRows;
    string expression = string.Empty;
    DataTable ddlCityClone = new DataTable();
    DataTable MydtState = new DataTable();
    DataSet Myds;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    
    protected void Page_Load(object sender, EventArgs e)
    {

        

        if (!Page.IsPostBack)
        {
            Session["RenewalAmedmentTypeofChange"] = null;
        }
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //try
        //{
        //    checkButtonDoubleClick(btnSubmit);
        //    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        //}
        //catch (Exception ex)
        //{
        //    UserId = 0;
        //    RoleId = 0;
        //    DepartmentId = 0;
        //    UserFullName = "";
        //}
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            ddlCity_c4.Items.Insert(0, new ListItem("--Select City--"));

            if (txt_1c1.Text == "")
            {
                RequiredFieldValidator11.Enabled = false;
            }
            else if (ddlCity_c4.SelectedItem.Text.ToString() == "--Select City--" && ddlState_c3.SelectedItem.Text.ToString() != "--Select State--")
            {
                RequiredFieldValidator11.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        //ACCORDING TO BRANCH
        //if (RoleId == 8)
        //{
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            try
            {
                CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
           // UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());

            Formtypeid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
                BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            }
            catch
            {
                CompanyId = 0;
                BranchId = 0;
            }
        ////}
        //else if (RoleId == 7)//ACCORDING TO COMPANY LOGIN
        //{
        //    try
        //    {
        //        CompanyId = DepartmentId;
        //    }
        //    catch
        //    {
        //        CompanyId = 0;
        //        BranchId = 0;
        //    }
        //}

        FunRequest();
        try
        {
            if (!Page.IsPostBack)
            {
                //ASSIGNING IN CACHE
                Cache.Insert("City", clsDDL.ddlCity_Activated(0));// null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                Cache.Insert("State", clsDDL.ddlState_Activated());//, null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);


              

                fillChangesType();


                FunFillDataset();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmitBack_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        string EncryptCompany = string.Empty, EncryptFormtype = string.Empty, EncryptBranchId = string.Empty;

        //EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        //string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        //EncryptFormtype = objDecEnc.Encrypt("3");
        //EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());


        EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        EncryptFormtype = objDecEnc.Encrypt("3");
        EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        Response.Redirect("Online_UnitRenewalStep1.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&b=" + EncryptBranchId, false);


        //Response.Redirect("Online_UnitRenewalStep1.aspx.aspx");
    }
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
          //  UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Formtypeid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            //prevPage = (objDecEnc.Decrypt(Request.QueryString["PanCard"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Formtypeid = 0;
            BranchId = 0;
            //prevPage = "";
        }
    }
    public void fillChangesType()
    {
        if (!IsPostBack)
        {
            string query = "Select Condition as Condition,ID as ID from GPtbl_RenewalAmendmentConditions where Con_Display=1 and Con_Delete=0";
            int count = 0;
            string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            //CheckIfCompanyIDExists(ref count, constr);
            //if (count > 0)
            {

                DataSet ds = null;
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        try
                        {
                            using (SqlDataAdapter sda = new SqlDataAdapter())
                            {
                                cmd.Connection = conn;
                                sda.SelectCommand = cmd;
                                using (ds = new DataSet())
                                {
                                    sda.Fill(ds);
                                }

                            }
                            grdCondition.DataSource = ds;
                            grdCondition.DataBind();

                            PoplulateOldCheckbox(constr);
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }

        }
    }

    private void PoplulateOldCheckbox(string constr)
    {
        string condition = "Select ConditionID from Gptbl_RenAmendCombination where CompanyID=" + CompanyId + "and RA_Display=1 and RA_Delete=0 ";
        DataSet dd = null;
        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(condition, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (dd = new DataSet())
                        {
                            sda.Fill(dd);
                            foreach (DataRow dr in dd.Tables[0].Rows)
                            {
                                foreach (GridViewRow gvr in grdCondition.Rows)
                                {
                                    Label lblID = gvr.FindControl("lblID") as Label;
                                    if (lblID.Text.Equals(dr[0].ToString()))
                                    {
                                        CheckBox chkSelect = gvr.FindControl("chkSelect") as CheckBox;
                                        chkSelect.Checked = true;
                                    }

                                }
                            }
                        }
                    }


                }
                catch (Exception ex)
                {

                }
                finally
                {
                    conn.Close();
                }
            }
        }

        // return ds;
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
                Myds = Company_UnitDetails_Fetch_ByCompanyId(CompanyId);
                //Myds = objAmendment.Company_UnitDetails_Fetch_ByCompanyId(97);

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

                //Writedatainxml(); 

                #region Bind Bank Details
                try
                {
                    string BankType = "";
                    if (MydtCompany.Rows[0]["fGPtbl_BankType"].ToString() !="")
                    {
                         BankType = MydtCompany.Rows[0]["fGPtbl_BankType"].ToString();
                    }
                  
                    int BanTypeId = 0;
                    if (BankType == "Private Sector Banks")
                    {
                        BanTypeId = 1;
                    }
                    else if (BankType == "Public Sector Banks")
                    {
                        BanTypeId = 2;
                    }
                    else if (BankType == "Regional Rural Banks")
                    {
                        BanTypeId = 3;
                    }
                    else if (BankType == "Small Finance Banks")
                    {
                        BanTypeId = 4;
                    }
                    ddlBankType.SelectedValue = BankType.ToString();
                }
                catch { }
                DataSet dsbank = new DataSet();
                dsbank = Fetch_Bank(ddlBankType.SelectedItem.Text);
                dsbank = Fetch_Bank(ddlBankType.SelectedItem.Text);
                ddlBankName.DataSource = dsbank;
                ddlBankName.DataTextField = "BankName";
                ddlBankName.DataValueField = "BankID";
                ddlBankName.DataBind();
                ddlBankName.Items.Insert(0, new ListItem("--Select Bank--", "0"));

                try
                {
                    string bankName = MydtCompany.Rows[0]["fGPtbl_BankName"].ToString();
                    ListItem item = ddlBankName.Items.FindByText(bankName);
                    if (item != null)
                    {
                        ddlBankName.ClearSelection();
                        item.Selected = true;
                    }
                }
                catch { }
                try
                {
                   // ddlBankName.SelectedItem.Text = MydtCompany.Rows[0]["fGPtbl_BankName"].ToString();// MyDr["fGPtbl_BankName"].ToString();
                
                txtIFSCCode.Text = MydtCompany.Rows[0]["fGPtbl_IFSCCOde"].ToString();// MyDr["fGPtbl_IFSCCOde"].ToString();
                txtBranchName.Text = MydtCompany.Rows[0]["fGPtbl_BranchName"].ToString();// MyDr["fGPtbl_BranchName"].ToString();
                txtBranchAddress.Text = MydtCompany.Rows[0]["fGPtbl_BranchName"].ToString();// MyDr["fGPtbl_BranchName"].ToString();
                }
                catch { }
                try
                {
                    string AccountType = MydtCompany.Rows[0]["fGPtbl_AccountType"].ToString();
                    ListItem itemAccountType = ddlAccountType.Items.FindByText(AccountType);
                    if (itemAccountType != null)
                    {
                        ddlAccountType.ClearSelection();
                        itemAccountType.Selected = true;
                    }
                }
                catch { }

                txtAccountNumber.Text = MydtCompany.Rows[0]["fGPtbl_AccountNumber"].ToString();// MyDr["fGPtbl_AccountNumber"].ToString();
                txtAccountHolderName.Text = MydtCompany.Rows[0]["fGPtbl_AccountHolderName"].ToString();// MyDr["fGPtbl_AccountHolderName"].ToString();


                #endregion
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void FunFillDDl()
    {
        try
        {
            ddlBindState(ddlState_a3);
            // ddlBindCity(ddlCity_a4);
            ddlBindState(ddlState_c3);
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
            if (CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString() == "          ")
            {
                txt_1.Text = "";

            }
            else
            {
                txt_1.Text = CompanyTable.Rows[0]["fGPtbl_Company_PAN"].ToString();

                txt_1.Attributes.Add("Readonly", "Readonly");

            }
            txt_1p1.Text = CompanyTable.Rows[0]["fGPtbl_Company_Name"].ToString();
            txt_1a1.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
            txt_1a2.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
            ddlState_a3.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString();
            ddlBindCity(ddlCity_a4, CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString());
            ddlCity_a4.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_CityId"].ToString();
            txt_1a5.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
            txt_mob.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Mobile"].ToString();
            txt_1a6.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
            txt_1a7.Text = CompanyTable.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
            rdo_declarationMSME.SelectedValue = CompanyTable.Rows[0]["fGPtbl_Company_MSME"].ToString() == "1" ? "1" : "2";

            if (CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString() == "")
            {
                txt_proposal_date.Text = "";
            }
            else
            {
                txt_proposal_date.Text = CompanyTable.Rows[0]["fGPtbl_Company_ProposalDate"].ToString();
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

            //FOR BRANCH
            if (BranchTable.Rows.Count != 0)
            {
                txt_1c1.Text = BranchTable.Rows[0]["fGPtbl_CUnitBranch_Address"].ToString();
                txt_1c2.Text = BranchTable.Rows[0]["fGPtbl_CUnitBranch_Zip"].ToString();
                ddlState_c3.SelectedValue = BranchTable.Rows[0]["fGPtbl_CUnitBranch_StateId"].ToString();
                ddlBindCity(ddlCity_c4, BranchTable.Rows[0]["fGPtbl_CUnitBranch_StateId"].ToString());
                ddlCity_c4.SelectedValue = BranchTable.Rows[0]["fGPtbl_CUnitBranch_CityId"].ToString();
                txt_1c5.Text = BranchTable.Rows[0]["fGPtbl_CUnitBranch_Phone"].ToString();
                txt_1c6.Text = BranchTable.Rows[0]["fGPtbl_CUnitBranch_Fax"].ToString();
                txt_1c7.Text = BranchTable.Rows[0]["fGPtbl_CUnitBranch_Email"].ToString();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    //TO BIND FACTORY GRID VIEW
    protected void FunFillFactory(DataTable FactoryTable)
    {
        try
        {
            DataTable FactoryTableClone = new DataTable();
            FactoryTableClone = FactoryTable.Copy();
            int rwcount = FactoryTableClone.Rows.Count;
            int colcount = FactoryTableClone.Columns.Count;

            int i = 0;
            if (colcount == 13)
            {
                while (i < (5 - rwcount))
                {
                    FactoryTableClone.Rows.Add(0, 0, 0, 0, 0, "", "", "", 0, 0, "", "", "");
                    i++;
                }
            }

            // Added By DHiraj 09-11-2017

            if (colcount == 14)
            {
                while (i < (5 - rwcount))
                {
                    FactoryTableClone.Rows.Add(0, 0, 0, 0, 0, "", "", "", 0, 0, "", "", "", 0);
                    i++;
                }
            }
            // Added By DHiraj 09-11-2017


            else if (colcount == 15)
            {
                while (i < (5 - rwcount))
                {
                    FactoryTableClone.Rows.Add(0, 0, 0, 0, 0, 0, "", "", "", 0, 0, "", "", "", 0);
                    i++;
                }
            }

            else if (colcount == 16)
            {
                while (i < (5 - rwcount))
                {
                    FactoryTableClone.Rows.Add(0, 0, 0, 0, 0, 0, "", "", "", 0, 0, "", "", "", 0, 0);
                    i++;
                }
            }

            GridFactory.DataSource = FactoryTableClone;
            GridFactory.DataBind();

            //BIND CATEGORY ID
            ddl_1d.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString();
            ddl_1e.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString();
            ddl_1f.SelectedValue = FactoryTable.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();

            //BIND BRANCH NAME
            lblBranch.Text = FactoryTable.Rows[0]["BranchName"].ToString();
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


    protected void txt_1_TextChanged(object sender, EventArgs e)
    {

        try
        {
            lblPANmsg.Visible = false;

            string fPANno = "";
            fPANno = txt_1.Text.ToString().ToUpper();
            int fPANlength = 0;
            fPANlength = fPANno.Length;
            if (fPANlength != 10)
            {
                txt_1.Focus();
            }

            objAmendment = new clsAmendment();
            fStatus = objAmendment.CompanyAmendment_CheckPanNo(CompanyId, fPANno);

            if (fStatus == 2)  //PAN EXIST WITH OTHER COMPANY
            {
                lblPANmsg.Visible = true;
                btnSubmit.Enabled = false;
                lblPANmsg.Text = "PAN exists with other company";
                txt_1.Focus();
            }
            else
            {
                lblPANmsg.Visible = false;
                btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void txt_aahar_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lbl_adhar_number.Visible = false;

            string Adhar_number = "";
            Adhar_number = txt_aahar.Text.ToString().ToUpper();
            int fPANlength = 0;
            fPANlength = Adhar_number.Length;
            if (fPANlength != 12)
            {
                txt_aahar.Focus();
            }

            objAmendment = new clsAmendment();
            fStatus = objAmendment.CompanyAmendment_checkadharNo(CompanyId, Adhar_number);

            if (fStatus == 2)  //PAN EXIST WITH OTHER COMPANY
            {
                lbl_adhar_number.Visible = true;
                //  btnSubmit.Enabled = false;
                lbl_adhar_number.Text = "PAN exists with other company";
                txt_aahar.Focus();
            }
            else
            {
                lbl_adhar_number.Visible = false;
                //btnSubmit.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
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
                HiddenField hdnNatureOfOperation = (HiddenField)e.Row.FindControl("hdnNatureOfOperation");
                DropDownList ddlftNatureofOperation = (DropDownList)e.Row.FindControl("ddlNatureOfOperation_b8");
                DropDownList ddlGridcity = (DropDownList)e.Row.FindControl("ddlCity_b4");
                ddlftNatureofOperation.SelectedValue = hdnNatureOfOperation.Value;
                ddlBindCity(ddlGridcity, hdnstate.Value);

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

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    protected void ddlState_a3_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable MydtCity, MydtCityClone = new DataTable();
            MydtCity = (DataTable)Cache["City"];
            MydtCityClone = MydtCity.Clone();
            DropDownList stateddl = (DropDownList)sender;
            string StateddlId = stateddl.ID;

            cityRows = MydtCity.Select("fGPtbl_MCty_StateId=" + stateddl.SelectedValue.ToString());

            foreach (DataRow r1 in cityRows)
            {
                MydtCityClone.ImportRow(r1);
            }

            if (StateddlId == "ddlState_a3")
                ddlBindCity(ddlCity_a4, MydtCityClone);

            if (StateddlId == "ddlState_c3")
                ddlBindCity(ddlCity_c4, MydtCityClone);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void ddlState_b3_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable MydtCity, MydtCityClone = new DataTable();
        DropDownList myDropDown = (DropDownList)sender;

        GridViewRow gridViewRowDdlBelongsTo = (GridViewRow)(((Control)sender).NamingContainer);
        DropDownList secondDropDownList = (DropDownList)gridViewRowDdlBelongsTo.FindControl("ddlCity_b4");

        RequiredFieldValidator reqCity = (RequiredFieldValidator)gridViewRowDdlBelongsTo.FindControl("RequiredFieldValidator12");
        try
        {

            if (myDropDown.SelectedValue == "")
            {
                secondDropDownList.Items.Clear();
                secondDropDownList.Items.Insert(0, new ListItem("--Select City--", ""));
                reqCity.Enabled = false;
            }
            else
            {
                //======UTTAM======================================
                expression = "fGPtbl_MCty_StateId=" + Convert.ToInt32(myDropDown.SelectedItem.Value);

                MydtCity = (DataTable)Cache["City"];
                MydtCityClone = MydtCity.Clone();

                cityRows = MydtCity.Select(expression);

                foreach (DataRow r1 in cityRows)
                {
                    MydtCityClone.ImportRow(r1);
                }

                reqCity.Enabled = true;

                secondDropDownList.DataSource = MydtCityClone;
                secondDropDownList.DataTextField = MydtCityClone.Columns["fGPtbl_MCty_Name"].ToString();
                secondDropDownList.DataValueField = MydtCityClone.Columns["fGPtbl_MCty_Id"].ToString();
                secondDropDownList.DataBind();
                secondDropDownList.Items.Insert(0, new ListItem("--Select City--", ""));
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
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
        string HOFax = txt_1a6.Text.Trim();
        string HOEmail = txt_1a7.Text.Trim();
        string ProposalDate_text = txt_proposal_date.Text.ToString() == "" ? "" : txt_proposal_date.Text.ToString();
        string Aadhar_number = txt_aahar.Text.ToString() == "" ? "" : txt_aahar.Text.ToString();
        int CategoryAreaId = 0;
        bool bsDeclarationMSME = rdo_declarationMSME.SelectedValue == "1" ? true : false;
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

        string BRAddress = "";
        string BRPin = "";
        string BRPhone = "";
        string BRFax = "";
        string BREmail = "";

        if (txt_1c1.Text.Trim().ToString() != "")
        {
            BRAddress = txt_1c1.Text.Trim().ToString();
        }

        if (txt_1c2.Text.Trim().ToString() != "")
        {
            BRPin = txt_1c2.Text.Trim().ToString();
        }

        if (txt_1c5.Text.Trim().ToString() != "")
        {
            BRPhone = txt_1c5.Text.Trim().ToString();
        }

        if (txt_1c6.Text.Trim().ToString() != "")
        {
            BRFax = txt_1c6.Text.Trim().ToString();
        }

        if (txt_1c7.Text.Trim().ToString() != "")
        {
            BREmail = txt_1c7.Text.Trim().ToString();
        }

        int BRState = 0;
        int BRCity = 0;

        try
        {
            if (ddlState_c3.SelectedIndex > 0)
            {
                BRState = Convert.ToInt32(ddlState_c3.SelectedValue);
            }

        }
        catch { }

        try
        {
            if (ddlCity_c4.SelectedIndex > 0)
            {
                BRCity = Convert.ToInt32(ddlCity_c4.SelectedValue);
            }

        }
        catch { }

        string AllowStorepara = (Session["AmedmentTypeofChange"] != null && Session["AmedmentTypeofChange"].ToString() == "3") ? "3" : null;


        try
        {
            //objAmendment.CompanyAmendment_InsertUpdate_CompanyBranch_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
            //      HOCity, HOPinCode, HOTelephone, HOFax, HOEmail, BRAddress, BRPin, BRState, BRCity, BRPhone, BRFax, BREmail, UserId, CompanyId)
            if (StepCompletion >= 1)
            {
                ErrorId = CompanyAmendment_InsertUpdate_CompanyBranch_Step1(2, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, HOFax, HOEmail, BRAddress, BRPin, BRState, BRCity, BRPhone, BRFax, BREmail, UserId, CompanyId, "amendment", "", "", txt_mob.Text.Trim(), ProposalDate_text, Aadhar_number, bsDeclarationMSME, AllowStorepara);
            }
            else
            {
                ErrorId = CompanyAmendment_InsertUpdate_CompanyBranch_Step1(1, PANno, NameofApplicantFirm, HOAddress, HOState,
                    HOCity, HOPinCode, HOTelephone, HOFax, HOEmail, BRAddress, BRPin, BRState, BRCity, BRPhone, BRFax, BREmail, UserId, CompanyId, "amendment", "", "", txt_mob.Text.Trim(), ProposalDate_text, Aadhar_number, bsDeclarationMSME, AllowStorepara);
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
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1(2, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                    CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "amendment", bsFTNatureOfOperation);


                        InsertChangeType(CompanyId);
                    }
                    else
                    {
                        objAmendment.CompanyAmendment_InsertUpdateFactory_step1(1, FTAddress, FTPinCode, FTState, FTCity, FTTelephone, FTFax, FTEmail,
                                CategoryAreaId, CategoryCastId, CategoryUnitSizeId, UnitId, CompanyId, UserId, BranchId, "amendment", bsFTNatureOfOperation);

                        InsertChangeType(CompanyId);
                    }
                }
                else if (UnitId == 0 && txtadd.Text.Trim() != "")
                {
                    // FTState = Convert.ToInt32(ddlftstate.SelectedValue);
                    //  FTCity = Convert.ToInt32(ddlftcity.SelectedValue);
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

        if (ErrorId == 2)   //INSERT/UPDATE SUCCESSFULL
        {


            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = string.Empty, EncryptFormtype = string.Empty, EncryptBranchId = string.Empty;




            try
            {
                int ErrorId_Bank = 0;
                ErrorId_Bank = Company_AccountDetails(CompanyId, ddlBankType.SelectedValue, ddlBankName.SelectedItem.Text, txtIFSCCode.Text, txtBranchName.Text, txtBranchName.Text, ddlAccountType.SelectedItem.Text, txtAccountNumber.Text, txtAccountHolderName.Text);

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message + "Company_AccountDetails");
            }





            //FOR BRANCH
            //if (RoleId == 8)
            //{
            EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                EncryptFormtype = objDecEnc.Encrypt("3");
                EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                //string PanCard = objDecEnc.Encrypt(txt_1.Text.ToString());
          //  }
            //else if (RoleId == 7) //FOR REGISTERED COMPANY
            //{
            //    EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            //    EncryptFormtype = objDecEnc.Encrypt("3");
            //    EncryptBranchId = objDecEnc.Encrypt(ViewState["BranchId"].ToString());
            //}
            //Server.Transfer("NewUnitRegistrationStep2.aspx?c=" + CompanyId.ToString() + "&u=" + UnitId.ToString(), true);
            ViewState["BranchId"] = "";
            ViewState["StepCompletion"] = "";
            Response.Redirect("Online_Unit_Ren_Amnd_Step2.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&b=" + EncryptBranchId, false);


        }
        else //INSERT/UPDATE FAIL
        {

        }

    }


    public void InsertChangeType(int CompanyId)
    {
      
        string query = "Update Gptbl_RenAmendCombination Set RA_Display=0,RA_Delete=1 Where CompanyID='" + CompanyId + "'";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection cnn = new SqlConnection(constr);
        try
        {
            cnn.Open();
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cnn.Close();

        }
        catch (Exception ex)
        {
            //MessageBox.Show("Can not open connection ! ");
        }



        foreach (GridViewRow gvrow in grdCondition.Rows)
        {
            CheckBox chk = (CheckBox)gvrow.FindControl("chkSelect");
            if (chk != null & chk.Checked)
            {
                Label lblID = (Label)gvrow.FindControl("lblID");
                DateTime dbt = DateTime.Now;
                 query = "INSERT INTO Gptbl_RenAmendCombination (ConditionID,CompanyID,RA_Display,RA_Delete) values('" + lblID.Text + "','" + CompanyId + "','1','0')";
                 constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
                 cnn = new SqlConnection(constr);
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand(query, cnn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    cnn.Close();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Can not open connection ! ");
                }

            }
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


    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Save & Continue >>';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }


    //protected void rdo_typeofChange_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rdo_typeofChange.SelectedValue == "2")
    //        GridFactory.Enabled = false;
    //    else
    //        GridFactory.Enabled = true;
    //}


    //TO FETCH ALL DETAILS OF FIRST PAGE
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


    //==========GAURAV SRIVASTAV=========

    //TO INSERT INTO COMPANY TABLE
    public int CompanyAmendment_InsertUpdate_CompanyBranch_Step1(int insertupdateflag, string pan, string compname, string compadd, int stateid, int city,
                      string compzip, string compphone, string compfax, string compemail, string braddress, string brzip, int brstateid, int brcityid, string brphone,
                      string brfax, string bremail, int userid, int companyid, string amdrenewal, string proposaldate, string proposalnumber,
                     string commobno, string Proposaldate_text, string AadharNumber, bool fGPtbl_Company_MSME, string IsOnlyStoreUpdate)
    {
        try
        {   
            if (amdrenewal == "amendment")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Step1_RA_Rennewal_Amendment_Online", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyRenewal_Insert_Step1_RA", DBManager.ConnectToDB());
            }

            MySqlComm.CommandType = CommandType.StoredProcedure;



            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = insertupdateflag;
            MySqlComm.Parameters.Add("@fGPtbl_Company_PAN", SqlDbType.VarChar, 500).Value = pan;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 100).Value = compname;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Address", SqlDbType.VarChar, 100).Value = compadd;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = stateid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = city;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Zip", SqlDbType.VarChar, 50).Value = compzip;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Fax", SqlDbType.VarChar, 50).Value = compfax;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Phone", SqlDbType.VarChar, 50).Value = compphone;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Mobile", SqlDbType.VarChar, 50).Value = commobno;
            MySqlComm.Parameters.Add("@fGPtbl_Company_HeadOffice_Email", SqlDbType.VarChar, 50).Value = compemail;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Address", SqlDbType.VarChar, 100).Value = braddress;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Zip", SqlDbType.VarChar, 100).Value = brzip;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_StateId", SqlDbType.Int).Value = brstateid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_CityId", SqlDbType.Int).Value = brcityid;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Phone", SqlDbType.VarChar, 100).Value = brphone;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Fax", SqlDbType.VarChar, 50).Value = brfax;
            MySqlComm.Parameters.Add("@fGPtbl_CUnitBranch_Email", SqlDbType.VarChar, 50).Value = bremail;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@fGPtbl_Company_MSME", SqlDbType.Bit).Value = fGPtbl_Company_MSME;



            if (amdrenewal == "amendment")
            {
                if (Proposaldate_text.ToString() != "") MySqlComm.Parameters.Add("@fGPtbl_Company_ProposalDate", SqlDbType.VarChar, 50).Value = Proposaldate_text;
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;

                if (IsOnlyStoreUpdate != null)
                {
                    MySqlComm.Parameters.Add("@IsOnlyStoreUpdate", SqlDbType.Bit).Value = 0; //will Set Pending Accounts to 0

                }


            }
            if (amdrenewal == "renewal")
            {
                MySqlComm.Parameters.Add("@Proposaldate", SqlDbType.VarChar).Value = proposaldate;
                MySqlComm.Parameters.Add("@Proposalnumber", SqlDbType.VarChar, 50).Value = proposalnumber;
                if (AadharNumber.ToString() != "") MySqlComm.Parameters.Add("@fGtbl_Company_AdharNumber", SqlDbType.VarChar, 50).Value = AadharNumber;
            }


            SqlParameter Outputval = new SqlParameter("@ErrorId", SqlDbType.Int);
            Outputval.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(Outputval);

            MySqlComm.ExecuteNonQuery();

            ErrorId = Convert.ToInt32(Outputval.Value);
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


    public DataSet Fetch_Bank(string BankType)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchBank", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Banktype", SqlDbType.VarChar).Value = BankType;
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


    public int Company_AccountDetails(int CompanyId, string BankType, string BankName, string IFSCCOde, string BranchName, string BranchAddress, string AccountType, string AccountNumber, string AccountHolderName)
    {
        int Status = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_UpdateCompany_AccountDetails", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_BankType", SqlDbType.VarChar).Value = BankType;
            MySqlComm.Parameters.Add("@fGPtbl_BankName", SqlDbType.VarChar).Value = BankName;
            MySqlComm.Parameters.Add("@fGPtbl_IFSCCOde", SqlDbType.VarChar).Value = IFSCCOde;
            MySqlComm.Parameters.Add("@fGPtbl_BranchName", SqlDbType.VarChar).Value = BranchName;
            MySqlComm.Parameters.Add("@fGPtbl_BranchAddress", SqlDbType.VarChar).Value = BranchAddress;
            MySqlComm.Parameters.Add("@fGPtbl_AccountType", SqlDbType.VarChar).Value = AccountType;
            MySqlComm.Parameters.Add("@fGPtbl_AccountNumber", SqlDbType.VarChar).Value = AccountNumber;
            MySqlComm.Parameters.Add("@fGPtbl_AccountHolderName", SqlDbType.VarChar).Value = AccountHolderName;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);


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
        return ErrorId;
    }
    protected void ddlBankType_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        if (ddlBankType.SelectedValue != "0")
        {
            ds = Fetch_Bank(ddlBankType.SelectedItem.Text);
            ddlBankName.DataSource = ds;
            ddlBankName.DataTextField = "BankName";// ds.Tables[0].Rows[0]["BankName"].ToString();  
            ddlBankName.DataValueField = "BankID";// ds.Tables[0].Rows[0]["BankID"].ToString();             
            ddlBankName.DataBind();
            ddlBankName.Items.Insert(0, new ListItem("--Select Bank--", "0"));

        }
    }


}