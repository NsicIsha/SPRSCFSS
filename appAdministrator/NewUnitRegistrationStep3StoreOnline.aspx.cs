using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Drawing;

public partial class appAdministrator_NewUnitRegistrationStep3StoreOnline : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    DataTable Dt = new DataTable();
    DataSet Myds = new DataSet();
    DataRow[] foundrow;
    int ConstitutionId = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //try
        //{
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

        //mvStore.SetActiveView(viewRepeater);
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
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            ConstitutionId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["con"].Replace(" ", "+")).ToString());

            objCompany = new clsCompany();
            Myds.Tables.Add(objCompany.Company_Fetch_FactoryLocation(CompanyId));
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
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
            //TO SHOW NUMBER OF ROWS
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

    //BINDING GRIDVIEW
    private void GridViewBind()
    {
        Dt = new DataTable();
        objCompany = new clsCompany();
        mvStore.SetActiveView(viewGrid);

        Dt = objCompany.Company_Fetch_StoreByCompanyId(CompanyId);
        gvStoreDetails.DataSource = Dt;
        gvStoreDetails.DataBind();


    }

    //
    protected void btnStoreSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            //clsStore objStore = new clsStore();
            objCompany = new clsCompany();
            TextBox description, specification, brand, rating, production, shift, sno;
            DropDownList location;//Hscode
            string allvalue = string.Empty;
            Dictionary<int, string> vall = new Dictionary<int, string>();
            Dictionary<int, string> vallpmps = new Dictionary<int, string>();
            vallpmps.Add(0, "0");
            int i = 0;

            foreach (RepeaterItem item in StoreRepeater.Items)
            {

                location = (DropDownList)(item.FindControl("ddlFactory"));
                description = (TextBox)(item.FindControl("txtDescription"));
                specification = (TextBox)(item.FindControl("txtBis"));
                brand = (TextBox)(item.FindControl("txtModel"));
                rating = (TextBox)(item.FindControl("txtLimiting"));
                production = (TextBox)(item.FindControl("txtAnnual"));
                shift = (TextBox)(item.FindControl("txtShift"));
                sno = (TextBox)(item.FindControl("txtSnoCode"));

                //SPECIAL CHARACTER USED '^'
                allvalue = location.SelectedValue.ToString() + "^" + description.Text + "^" + specification.Text
                    + "^" + brand.Text + "^" + rating.Text + "^" + production.Text + "^" + shift.Text + "^" + sno.Text + "@";

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
            objCompany.Store_Insert(vall.Values, vallpmps.Values, 1, UserId);

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
                objCompany = new clsCompany();
                //Dt = new DataTable();
                DropDownList ddllocation = (DropDownList)e.Item.FindControl("ddlFactory");

                Dt = Myds.Tables[0];
                ddllocation.DataSource = Dt;
                ddllocation.DataTextField = Dt.Columns["fAddress"].ToString();
                ddllocation.DataValueField = Dt.Columns["fGPtbl_CUnit_Id"].ToString();

                ddllocation.DataBind();
                this.ViewState.Add("FactoryList", Dt);
                if (Convert.ToInt32(Dt.Rows[0]["FactoryCount"]) > 1)
                {
                    ddllocation.Items.Insert(0, new ListItem("--Select Factory--", "0"));
                }

                /*
                Dt.Clear();
                Dt.Dispose();
                */
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }


    }


    protected void gvStoreDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvStoreDetails.EditIndex = e.NewEditIndex;
        GridViewBind();

        btnAddStore.Enabled = false;
        BtnBackRegistration.Enabled = false;
        btnSave.Enabled = false;

        objCompany = new clsCompany();
        DataTable Dt1 = objCompany.Company_Fetch_FactoryLocation(CompanyId);
        this.ViewState.Add("FactoryList", Dt1);
    }


    protected void gvStoreDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvStoreDetails.EditIndex = -1;
        GridViewBind();

        btnAddStore.Enabled = true;
        BtnBackRegistration.Enabled = true;
        btnSave.Enabled = true;
    }


    protected void gvStoreDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        HiddenField storeid = (HiddenField)gvStoreDetails.Rows[e.RowIndex].FindControl("hfStoreid");
        DropDownList ddlFactory = (DropDownList)gvStoreDetails.Rows[e.RowIndex].FindControl("ddlFactory");
        TextBox description = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtDescription");
        TextBox bis = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtBis");
        TextBox model = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtModel");
        TextBox size = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtSize");
        TextBox production = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtProduction");
        TextBox shift = (TextBox)gvStoreDetails.Rows[e.RowIndex].FindControl("txtShift");
        int s = shift.Text != "" ? Convert.ToInt32(shift.Text) : 0;
        if (Convert.ToInt32(storeid.Value.ToString()) != 0)
        {
            objCompany = new clsCompany();

            int ErrorId = objCompany.Company_Update_Step3_Store(Convert.ToInt32(storeid.Value),
                                                                       Convert.ToInt32(ddlFactory.SelectedValue),
                                                                       description.Text,
                                                                       bis.Text,
                                                                       model.Text,
                                                                       size.Text,
                                                                       production.Text,
                                                                       s,
                                                                       "0", "0",
                                                                       UserId);

        }

        gvStoreDetails.EditIndex = -1;
        GridViewBind();

        btnAddStore.Enabled = true;
        BtnBackRegistration.Enabled = true;
        btnSave.Enabled = true;
    }

    string previous = "";
    int firstrow = -1;
    protected void gvStoreDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string strtext = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Dt = new DataTable();
                objCompany = new clsCompany();

                Dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);

                this.ViewState.Add("FactoryList", Dt);

                HiddenField hdnuntid = (HiddenField)e.Row.FindControl("hdnUnitid");

                HiddenField hdnEnterPrise = (HiddenField)e.Row.FindControl("hdnEnterPrise");


                if (hdnuntid != null)
                {
                    Literal location = (Literal)e.Row.FindControl("ltlLocation");
                    string expression = "fGPtbl_CUnit_Id=" + hdnuntid.Value.ToString();

                    foundrow = Dt.Select(expression);
                    location.Text = foundrow[0][1].ToString();
                }

                DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddlFactory");
                HiddenField hfnuntid = (HiddenField)e.Row.FindControl("hfUnitid");

                if (ddllocation != null)
                {
                    ddllocation.DataSource = Dt;
                    ddllocation.DataTextField = Dt.Columns["fAddress"].ToString();
                    ddllocation.DataValueField = Dt.Columns["fGPtbl_CUnit_Id"].ToString();
                    ddllocation.DataBind();

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

                if (ltlproduction == null)
                {
                }
                else
                {
                    if (ltldescription.Text.Contains("\r\n"))
                    {
                        ltldescription.Text.Replace("\r\n", "<br/>");
                    }
                }

                if (ltlbis == null)
                {
                }
                else
                {
                    if (ltlbis.Text.Contains("\r\n"))
                    {
                        ltlbis.Text.Replace("\r\n", "<br/>");
                    }
                }

                if (ltlbrand == null)
                {
                }
                else
                {
                    if (ltlbrand.Text.Contains("\r\n"))
                    {
                        ltlbrand.Text = ltlbrand.Text.Replace("\r\n", "<br/>");
                    }
                }

                if (ltlsize == null)
                {
                }
                else
                {
                    if (ltlsize.Text.Contains("\r\n"))
                    {
                        ltlsize.Text = ltlsize.Text.Replace("\r\n", "<br/>");
                    }
                }

                if (ltlproduction == null)
                {
                }
                else
                {
                    if (ltlproduction.Text.Contains("\r\n"))
                    {
                        ltlproduction.Text = ltlproduction.Text.Replace("\r\n", "<br/>");
                    }
                }

                //===========CODE FOR MERGING START=========================
                DataRowView drv = (DataRowView)e.Row.DataItem;

                if (drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() == "0")
                {
                    //If it's the same category as the previous one
                    //Increment the rowspan


                    if (gvStoreDetails.Rows[firstrow].Cells[5].RowSpan == 0)

                        gvStoreDetails.Rows[firstrow].Cells[5].RowSpan = 2;

                    else
                        gvStoreDetails.Rows[firstrow].Cells[5].RowSpan += 1;

                    //Remove the cell
                    e.Row.Cells.RemoveAt(5);

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
                    //e.Row.Cells[9].Text = "";
                    //e.Row.Cells[9].BackColor = Color.Silver;
                    //e.Row.Cells[8].Text = "";
                    //e.Row.Cells[8].BackColor = Color.Silver;
                    //e.Row.Cells[7].Text = "";
                    //e.Row.Cells[7].BackColor = Color.Silver;
                    //e.Row.Cells[6].Text = "";
                    //e.Row.Cells[6].BackColor = Color.Silver;
                    ltlbrand.Text = "";
                    ltlsize.Text = "";
                    ltlproduction.Text = "";
                    ltlShift.Text = "";
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
                //===========CODE FOR MERGING END

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in gvStoreDetails_RowDataBound() " + ex.Message);
        }
    }


    protected void btnAddStore_Click(object sender, EventArgs e)
    {
        mvStore.SetActiveView(viewRepeater);
        RepeaterBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        //TO RESTRICT USER TO FILL STORE DETAILS
        Dt = new DataTable();
        objCompany = new clsCompany();
        //mvStore.SetActiveView(viewGrid);
        Dt = objCompany.Company_Fetch_StoreByCompanyId(CompanyId);
        if (Dt.Rows.Count > 0)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();
            string EncryptConstitutionId = objDecEnc.Encrypt(ConstitutionId.ToString());


            Response.Redirect("NewUnitRegistrationStep4Online.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        GridViewBind();
    }

    protected void BtnBackRegistration_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        string EncryptFormtype = objDecEnc.Encrypt("1");

        string EncryptConstitutionId = objDecEnc.Encrypt(ConstitutionId.ToString());

        string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();

        //Server.Transfer("NewUnitRegistrationStep2.aspx?c=" + CompanyId.ToString() + "&u=" + UnitId.ToString(), true);
        Response.Redirect("NewUnitRegistrationStep3Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);

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
            dtFactoryDetails = (DataTable)this.ViewState["FactoryList"];
        }

        var ddlFactory = ((DropDownList)(StoreRepeater.FindControl("ddlFactory")));
        ddlFactory = (DropDownList)sender;
        RepeaterItem item = (RepeaterItem)ddlFactory.NamingContainer;


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