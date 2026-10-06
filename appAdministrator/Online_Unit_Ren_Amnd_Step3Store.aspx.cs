using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Drawing;

public partial class appAdministrator_Online_Unit_Ren_Amnd_Step3Store : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    bool AllowStoreEdit = true;
    clsCompany objCompany;
    clsAmendment objAmendment;

    DataTable Dt;
    DataRow[] foundrow;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //AllowStoreEdit = (Session["AmedmentTypeofChange"] != null && Session["AmedmentTypeofChange"].ToString() == "2") ? false : true;
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //mvStore.SetActiveView(viewRepeater);
        //   checkButtonDoubleClick(btnSave);
        if (!AllowStoreEdit)
            btnAddStore.Enabled = false;
        FunRequest();
        if (!IsPostBack)
        {
            //  RepeaterBind();
            GridViewBind();
        }


    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            // UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString()); // Formtype id
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }

    //BINDING GRIDVIEW
    private void GridViewBind()
    {
        try
        {
            Dt = new DataTable();
            objAmendment = new clsAmendment();

            Dt = objAmendment.CompanyAmendment_Fetch_StoreByCompanyId(CompanyId);
            gvStoreDetails.DataSource = Dt;
            System.Threading.Thread.Sleep(500);
            gvStoreDetails.DataBind();

            mvStore.SetActiveView(viewGrid);
        }
        catch (Exception ex)
        {
            if (Session["reload"] == null)
            {
                Session.Add("reload", "1");
                Server.TransferRequest(Request.Url.AbsolutePath, false);

            }
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    private void RepeaterBind()
    {
        try
        {
            Dt = new DataTable();

            Dt.Rows.Clear();
            Dt.Columns.Clear();
            Dt.Clear();

            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();
            Dt.Columns.Add();

            int i = 0;
            while (i < 40)
            {
                Dt.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0);
                i++;
            }

            StoreRepeater.DataSource = Dt;
            StoreRepeater.DataBind();

            mvStore.SetActiveView(viewRepeater);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnAddStore_Click(object sender, EventArgs e)
    {
        try
        {
            mvStore.SetActiveView(viewRepeater);
            RepeaterBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            Session.Remove("reload");
            // Response.Redirect("UnitAmendmentStep4.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
            Response.Redirect("Online_Unit_Ren_Amnd_Step4.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }

    protected void btnStoreSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //clsStore objStore = new clsStore();
            objAmendment = new clsAmendment();
            TextBox description, specification, brand, rating, production, shift, storecode;
            DropDownList location;//Hscode
            string allvalue = string.Empty;
            Dictionary<int, string> vall = new Dictionary<int, string>();
            Dictionary<int, string> vallpmps = new Dictionary<int, string>();
            vallpmps.Add(0, "0");
            int i = 0;


            foreach (RepeaterItem item in StoreRepeater.Items)
            {

                location = (DropDownList)(item.FindControl("ddlFactory"));
                storecode = (TextBox)(item.FindControl("txtSnoCode"));
                description = (TextBox)(item.FindControl("txtDescription"));
                specification = (TextBox)(item.FindControl("txtBis"));
                brand = (TextBox)(item.FindControl("txtModel"));
                rating = (TextBox)(item.FindControl("txtLimiting"));
                production = (TextBox)(item.FindControl("txtAnnual"));
                shift = (TextBox)(item.FindControl("txtShift"));

                //SPECIAL CHARACTER SUBSTITUTED '^' INSTEAD OF ','
                allvalue = location.SelectedValue.ToString() + "^" + description.Text.Trim() + "^" + specification.Text.Trim() + "^" + brand.Text.Trim()
                    + "^" + rating.Text.Trim() + "^" + production.Text.Trim() + "^" + shift.Text.Trim() + "^" + storecode.Text.Trim() + "@";

                if (allvalue != "0^0^0^0^0^0^0^0@")
                {
                    if (allvalue != location.SelectedValue.ToString() + "^0^0^0^0^0^0^0@")
                    {
                        vall.Add(i, allvalue);
                        i++;
                    }
                }
            }
            //string allvalue1 = allvalue;
            string AllowStorepara = (Session["AmedmentTypeofChange"] != null && Session["AmedmentTypeofChange"].ToString() == "3") ? "3" : null;

            objAmendment.Store_Insert_RA(vall.Values, vallpmps.Values, 1, UserId, AllowStorepara);

            GridViewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    protected void StoreRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                objAmendment = new clsAmendment();
                Dt = new DataTable();

                DropDownList ddllocation = (DropDownList)e.Item.FindControl("ddlFactory");

                Dt = objAmendment.Company_Fetch_FactoryLocation(CompanyId);

                if (this.ViewState["FactoryList"] == null)
                    this.ViewState.Add("FactoryList", Dt);

                ddllocation.DataSource = Dt;
                ddllocation.DataTextField = Dt.Columns["fAddress"].ToString();
                ddllocation.DataValueField = Dt.Columns["fGPtbl_CUnit_Id"].ToString();
                ddllocation.DataBind();
                //if (Convert.ToInt32(Dt.Rows[0]["FactoryCount"]) > 1)
                //{
                ddllocation.Items.Insert(0, new ListItem("--Select Factory--", "0"));
                //}

                Dt.Clear();
                Dt.Dispose();

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }


    }

    protected void gvStoreDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvStoreDetails.EditIndex = -1;
            GridViewBind();

            btnAddStore.Enabled = true;
            Button1.Enabled = true;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    string previous = "";
    int firstrow = -1;
    protected void gvStoreDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {//1
                Button EditButton = (Button)e.Row.FindControl("EditButton");
                if (!AllowStoreEdit)
                    e.Row.Cells[0].Enabled = false;

                Dt = new DataTable();
                objAmendment = new clsAmendment();

                Dt = objAmendment.Company_Fetch_FactoryLocation(CompanyId);
                if (this.ViewState["FactoryList"] == null)
                    this.ViewState.Add("FactoryList", Dt);
                HiddenField hdnuntid = (HiddenField)e.Row.FindControl("hdnUnitid");
                if (hdnuntid != null)
                {
                    Literal location = (Literal)e.Row.FindControl("ltlLocation");
                    string expression = "fGPtbl_CUnit_Id=" + hdnuntid.Value.ToString();

                    foundrow = Dt.Select(expression);
                    location.Text = foundrow[0][1].ToString();
                }

                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlFactory");
                HiddenField hfnuntid = (HiddenField)e.Row.FindControl("hfUnitid");
                HiddenField hdnEnterPrise = (HiddenField)e.Row.FindControl("hdnEnterPrise");

                if (ddllocation != null)
                {
                    ddllocation.DataSource = Dt;
                    ddllocation.DataTextField = Dt.Columns["fAddress"].ToString();
                    ddllocation.DataValueField = Dt.Columns["fGPtbl_CUnit_Id"].ToString();
                    ddllocation.DataBind();
                    ddllocation.Items.Insert(0, new ListItem("--Select Factory--", "0"));
                    ddllocation.SelectedIndex = 0;
                    if (Convert.ToInt32(hfnuntid.Value) != 0)
                    {
                        ddllocation.SelectedValue = hfnuntid.Value;
                    }

                    Dt.Clear();
                    Dt.Dispose();
                }

                Literal ltldescription = (Literal)e.Row.FindControl("ltlDescription");
                Literal ltlbis = (Literal)e.Row.FindControl("ltlBis");
                Literal ltlbrand = (Literal)e.Row.FindControl("ltlModel");
                Literal ltlsize = (Literal)e.Row.FindControl("ltlSize");
                Literal ltlproduction = (Literal)e.Row.FindControl("ltlProduction");
                Literal ltlShift = (Literal)e.Row.FindControl("ltlShift");
                if (ltldescription.Text.Contains("\r\n"))
                {
                    ltldescription.Text.Replace("\r\n", "<br/>");
                }
                if (ltlbis.Text.Contains("\r\n"))
                {
                    ltlbis.Text.Replace("\r\n", "<br/>");
                }
                if (ltlbrand.Text.Contains("\r\n"))
                {
                    ltlbrand.Text = ltlbrand.Text.Replace("\r\n", "<br/>");
                }
                if (ltlsize.Text.Contains("\r\n"))
                {
                    ltlsize.Text = ltlsize.Text.Replace("\r\n", "<br/>");
                }
                if (ltlproduction.Text.Contains("\r\n"))
                {
                    ltlproduction.Text = ltlproduction.Text.Replace("\r\n", "<br/>");
                }

                //===========CODE FOR MERGING START=========================
                DataRowView drv = (DataRowView)e.Row.DataItem;

                if (drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() == "0")
                {
                    //If it's the same category as the previous one
                    //Increment the rowspan
                    if (gvStoreDetails.Rows[firstrow].Cells[6].RowSpan == 0)
                        gvStoreDetails.Rows[firstrow].Cells[6].RowSpan = 2;
                    else
                        gvStoreDetails.Rows[firstrow].Cells[6].RowSpan += 1;

                    //Remove the cell
                    e.Row.Cells[6].Visible = false;//.RemoveAt(5);
                }

                else //It's a new category
                {
                    //Set the vertical alignment to top
                    e.Row.VerticalAlign = VerticalAlign.Top;
                    //Maintain the category in memory
                    previous = drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString();
                    firstrow = e.Row.RowIndex;
                }

                if (hdnEnterPrise != null && hdnEnterPrise.Value == "2")
                {
                    ltlbrand.Text = "";
                    ltlsize.Text = "";
                    ltlproduction.Text = "";
                    ltlShift.Text = "";
                    //e.Row.Cells[9].Text = "";
                    //e.Row.Cells[9].BackColor = Color.Silver;
                    //e.Row.Cells[8].Text = "";
                    //e.Row.Cells[8].BackColor = Color.Silver;
                    //e.Row.Cells[7].Text = "";
                    //e.Row.Cells[7].BackColor = Color.Silver;
                    //e.Row.Cells[6].Text = "";
                    //e.Row.Cells[6].BackColor = Color.Silver;
                }
                HiddenField hfenterprise = (HiddenField)e.Row.FindControl("hfenterprise");


                if (hfenterprise != null && hfenterprise.Value == "2")
                {
                    TextBox txtModel = (TextBox)e.Row.FindControl("txtModel");
                    TextBox txtSize = (TextBox)e.Row.FindControl("txtSize");
                    TextBox txtProduction = (TextBox)e.Row.FindControl("txtProduction");
                    TextBox txtShift = (TextBox)e.Row.FindControl("txtShift");


                    //
                    if (txtModel != null)
                    {
                        txtModel.ReadOnly = true;
                        txtModel.Text = "";
                        txtModel.BackColor = Color.Silver;
                    }
                    //
                    if (txtSize != null)
                    {
                        txtSize.ReadOnly = true;
                        txtSize.Text = "";
                        txtSize.BackColor = Color.Silver;
                    }
                    //
                    if (txtProduction != null)
                    {
                        txtProduction.ReadOnly = true;
                        txtProduction.Text = "";
                        txtProduction.BackColor = Color.Silver;
                    }
                    //
                    if (txtShift != null)
                    {
                        txtShift.ReadOnly = true;
                        txtShift.Text = "";
                        txtShift.BackColor = Color.Silver;
                    }
                }

            }//1

        }//try
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void gvStoreDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            gvStoreDetails.EditIndex = e.NewEditIndex;
            GridViewBind();

            btnAddStore.Enabled = false;
            Button1.Enabled = false;
            btnSave.Enabled = false;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void gvStoreDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            HiddenField storeid = (HiddenField)gvStoreDetails.Rows[e.RowIndex].FindControl("hfStoreid");
            HiddenField Amdid = (HiddenField)gvStoreDetails.Rows[e.RowIndex].FindControl("hdnAmendmentId");
            DropDownList ddlFactory = (DropDownList)gvStoreDetails.Rows[e.RowIndex].FindControl("ddlFactory");
            TextBox storecode = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtSnoCode");
            TextBox description = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtDescription");
            TextBox bis = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtBis");
            TextBox model = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtModel");
            TextBox size = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtSize");
            TextBox production = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtProduction");
            TextBox shift = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtShift");

            string strstorecode = string.Empty, strdescription = string.Empty, strbis = string.Empty, strmodel = string.Empty,
                strsize = string.Empty, strproduction = string.Empty, strshift = string.Empty;

            strstorecode = storecode.Text.Trim() == "" ? "0" : storecode.Text.Trim();
            strdescription = description.Text.Trim() == "" ? "0" : description.Text.Trim();
            strbis = bis.Text.Trim() == "" ? "0" : bis.Text.Trim();
            strmodel = model.Text.Trim() == "" ? "0" : model.Text.Trim();
            strsize = size.Text.Trim() == "" ? "0" : size.Text.Trim();
            strproduction = production.Text.Trim() == "" ? "0" : production.Text.Trim();
            strshift = shift.Text.Trim() == "" ? "0" : shift.Text.Trim();
            string AllowStorepara = (Session["AmedmentTypeofChange"] != null && Session["AmedmentTypeofChange"].ToString() == "3") ? "3" : null;

            if (Convert.ToInt32(Amdid.Value.ToString()) != 0)
            {
                objAmendment = new clsAmendment();

                int ErrorId = objAmendment.CompanyAmendment_Update_Step3_Store_RA(Convert.ToInt32(Amdid.Value),
                                      Convert.ToInt32(ddlFactory.SelectedValue), strstorecode, strdescription, strbis, strmodel,
                                      strsize, strproduction, Convert.ToInt32(strshift), "0", "0", UserId, AllowStorepara);

            }

            gvStoreDetails.EditIndex = -1;
            GridViewBind();

            btnAddStore.Enabled = true;
            Button1.Enabled = true;
            btnSave.Enabled = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            GridViewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void btnBackstep3_Click(object sender, EventArgs e)
    {
        try
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            //Response.Redirect("UnitAmendmentStep3Store.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
            Response.Redirect("Online_Unit_Ren_Amnd_Step3.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }
    protected void gvStoreDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        HiddenField Amdid_del = (HiddenField)gvStoreDetails.Rows[e.RowIndex].FindControl("hdnAmendmentId_del");
        if (Convert.ToInt32(Amdid_del.Value.ToString()) != 0)
        {
            objAmendment = new clsAmendment();

            int ErrorId_del = objAmendment.CompanyAmendment_Delete_Step3_Store(Convert.ToInt32(Amdid_del.Value));

        }
        gvStoreDetails.EditIndex = -1;
        GridViewBind();
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

    protected void ddl_location_changeR(object sender, EventArgs e)
    {

        TextBox description, specification, brand, rating, production, shift, sno;
        DropDownList location;//Hscode
        DataTable dtFactoryDetails = new DataTable();
        if (this.ViewState["FactoryList"] == null)
        {
            clsCompany company = new clsCompany();
            if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
            {
                dtFactoryDetails = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();

                dtFactoryDetails = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
        else
        {
            //Added by Dhiraj
            /*
                        DataTable Dt = objAmendment.Company_Fetch_FactoryLocation(CompanyId);

                        if (this.ViewState["FactoryList"] == null)
                            this.ViewState.Add("FactoryList", Dt);
                        else
                        { 

                        dtFactoryDetails = (DataTable)this.ViewState["FactoryList"];
                        }
                        */

            dtFactoryDetails = (DataTable)this.ViewState["FactoryList"];
        }

        var ddlFactory = ((DropDownList)(StoreRepeater.FindControl("ddlFactory")));
        ddlFactory = (DropDownList)sender;
        RepeaterItem item = (RepeaterItem)ddlFactory.NamingContainer;
        if (ddlFactory.SelectedValue == "0")
            return;

        location = (DropDownList)(item.FindControl("ddlFactory"));
        description = (TextBox)(item.FindControl("txtDescription"));
        specification = (TextBox)(item.FindControl("txtBis"));
        brand = (TextBox)(item.FindControl("txtModel"));
        rating = (TextBox)(item.FindControl("txtLimiting"));
        production = (TextBox)(item.FindControl("txtAnnual"));
        shift = (TextBox)(item.FindControl("txtShift"));
        sno = (TextBox)(item.FindControl("txtSnoCode"));

        int FactoryId = Convert.ToInt32(ddlFactory.SelectedValue);

        DataRow dr = dtFactoryDetails.Select("fGPtbl_CUnit_Id=" + FactoryId)[0];


        int enterpriseType = dr != null ? Convert.ToInt32(dr["fGPtbl_CUnit_Enterprise"]) : -1;
        // hdn_enterprise.Value = enterpriseType.ToString();
        if (enterpriseType == 1 || enterpriseType == -1)  //Manufacturing
        {
            if (description != null) description.ReadOnly = false;
            if (specification != null) specification.ReadOnly = false;
            if (brand != null) brand.ReadOnly = false;
            if (rating != null) rating.ReadOnly = false;
            if (production != null) production.ReadOnly = false;
            if (shift != null) shift.ReadOnly = false;
            //if (txt_noofshifts != null) txt_noofshifts.ReadOnly = false;
            //if (txt_qualitaive != null) txt_qualitaive.ReadOnly = false;
            //if (txt_quantaive != null) txt_quantaive.ReadOnly = false;
            //if (txthscode != null) txthscode.ReadOnly = false;



            if (description != null) description.BackColor = Color.White;
            if (specification != null) specification.BackColor = Color.White;
            if (brand != null) brand.BackColor = Color.White;
            if (rating != null) rating.BackColor = Color.White;
            if (production != null) production.BackColor = Color.White;
            if (shift != null) shift.BackColor = Color.White;
            //if (txt_qualitaive != null) txt_qualitaive.BackColor = Color.White;
            //if (txt_quantaive != null) txt_quantaive.BackColor = Color.White;
            //if (txthscode != null) txthscode.BackColor = Color.White;



        }
        else if (enterpriseType == 2)  //Service
        {
            if (description != null) description.ReadOnly = false;
            if (specification != null) specification.ReadOnly = false;
            if (brand != null) brand.ReadOnly = true;
            if (rating != null) rating.ReadOnly = true;
            if (production != null) production.ReadOnly = true;
            if (shift != null) shift.ReadOnly = true;


            if (brand != null) brand.BackColor = Color.Silver;
            if (rating != null) rating.BackColor = Color.Silver; ;
            if (production != null) production.BackColor = Color.Silver; ;
            if (shift != null) shift.BackColor = Color.Silver; ;

        }







    }
    protected void ddl_location_changeG(object sender, EventArgs e)
    {

        DataTable dtFactoryDetails = new DataTable();
        if (this.ViewState["FactoryList"] == null)
        {
            clsCompany company = new clsCompany();
            if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
            {
                dtFactoryDetails = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();

                dtFactoryDetails = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
            }
        }
        else
        {
            dtFactoryDetails = (DataTable)this.ViewState["FactoryList"];
        }

        var ddlFactory = ((DropDownList)(gvStoreDetails.FindControl("ddlFactory")));
        ddlFactory = (DropDownList)sender;
        GridViewRow item = (GridViewRow)ddlFactory.NamingContainer;
        if (ddlFactory.SelectedValue == "0")
            return;
        TextBox description = (TextBox)item.FindControl("txtDescription");
        TextBox bis = (TextBox)item.FindControl("txtBis");
        TextBox model = (TextBox)item.FindControl("txtModel");
        TextBox size = (TextBox)item.FindControl("txtSize");
        TextBox production = (TextBox)item.FindControl("txtProduction");
        TextBox shift = (TextBox)item.FindControl("txtShift");
        int FactoryId = Convert.ToInt32(ddlFactory.SelectedValue);

        DataRow dr = dtFactoryDetails.Select("fGPtbl_CUnit_Id=" + FactoryId)[0];
        int enterpriseType = dr != null ? Convert.ToInt32(dr["fGPtbl_CUnit_Enterprise"]) : -1;
        //hdn_enterprise.Value = enterpriseType.ToString();
        if (enterpriseType == 1 || enterpriseType == -1)  //Manufacturing
        {
            if (description != null) description.ReadOnly = false;
            if (bis != null) bis.ReadOnly = false;
            if (model != null) model.ReadOnly = false;
            if (size != null) size.ReadOnly = false;
            if (production != null) production.ReadOnly = false;
            if (shift != null) shift.ReadOnly = false;
            //if (txt_noofshifts != null) txt_noofshifts.ReadOnly = false;
            //if (txt_qualitaive != null) txt_qualitaive.ReadOnly = false;
            //if (txt_quantaive != null) txt_quantaive.ReadOnly = false;
            //if (txthscode != null) txthscode.ReadOnly = false;



            if (description != null) description.BackColor = Color.White;
            if (bis != null) bis.BackColor = Color.White;
            if (model != null) model.BackColor = Color.White;
            if (size != null) size.BackColor = Color.White;
            if (production != null) production.BackColor = Color.White;
            if (shift != null) shift.BackColor = Color.White;
            //if (txt_qualitaive != null) txt_qualitaive.BackColor = Color.White;
            //if (txt_quantaive != null) txt_quantaive.BackColor = Color.White;
            //if (txthscode != null) txthscode.BackColor = Color.White;



        }
        else if (enterpriseType == 2)  //Service
        {
            if (description != null) description.ReadOnly = false;
            if (bis != null) bis.ReadOnly = false;
            if (model != null) model.ReadOnly = true;
            if (size != null) size.ReadOnly = true;
            if (production != null) production.ReadOnly = true;
            if (shift != null) shift.ReadOnly = true;

            if (model != null) model.Text = "";
            if (size != null) size.Text = "";
            if (production != null) production.Text = "";
            if (shift != null) shift.Text = "";



            if (model != null) model.BackColor = Color.Silver;
            if (size != null) size.BackColor = Color.Silver; ;
            if (production != null) production.BackColor = Color.Silver; ;
            if (shift != null) shift.BackColor = Color.Silver; ;

        }







    }
}