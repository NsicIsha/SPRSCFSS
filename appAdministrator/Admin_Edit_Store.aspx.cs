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
using System.Collections.Generic;
using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Drawing;



//[System.Web.Script.Services.ScriptService]
public partial class appAdministrator_Admin_Edit_Store : System.Web.UI.Page
{
    public delegate DataTable RepeaterDelegate(int compid);
    int UserId, RoleId = 0, DepartmentId;
    string UserFullName = string.Empty;

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

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            FunRequest();
            ltlMessage.Visible = false;
            clsCompany objCompany = new clsCompany();
            DataTable dtenterpriseval = new DataTable();
            //if (Request.QueryString.HasKeys())
            //{
            //    int companyid = Convert.ToInt32(Request.QueryString["id"]);
            //    dtenterpriseval = objCompany.Fetch_Unit_Enterprise(companyid.ToString());
            //    string enterpriseval = dtenterpriseval.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
            //    if (enterpriseval == "0")
            //    {
            //        enterpriseval = "1";
            //    }
            //    hdn_enterprise.Value = enterpriseval;
            //}

            if (!IsPostBack)
            {
                clsCompany objcom = new clsCompany();
                if (RoleId == 8)
                {

                    BindRepeaterPmps();


                    BindGridView();

                    //int rwcount=gv_storedetails.Rows.Count;
                }
                else
                {
                    Response.Redirect("logout.aspx");
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }
    protected void gv_storedetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandArgument == "INSERT")
            {
                GridViewRow gvrow = gv_storedetails.FooterRow;

                //  To insert CUnitStore_History Table
                clsCompany objcom1 = new clsCompany();
                int k = objcom1.Company_Insert_HistoryTableCunitStore(Convert.ToInt32(Request.QueryString["id"]));

                // TextBox txtName = (TextBox)testGrid.FooterRow.FindControl("txtSName");

                //GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                TextBox textBox = ((TextBox)gvrow.FindControl("txt_footproduct"));

                string strName = textBox.Text;
                DropDownList DDLSTORE = ((DropDownList)gv_storedetails.FooterRow.FindControl("ddl_footlocation"));
                TextBox txt_sno = gv_storedetails.FooterRow.FindControl("txt_footSno") as TextBox;
                TextBox txt_seqno = gv_storedetails.FooterRow.FindControl("txt_footSseqno") as TextBox;
                TextBox txt_store_des = ((TextBox)gv_storedetails.FooterRow.FindControl("txt_footproduct"));
                TextBox txt_ref_bis = gv_storedetails.FooterRow.FindControl("txt_footrefbisspecification") as TextBox;
                TextBox txt_model_brand = gv_storedetails.FooterRow.FindControl("txt_footmodel") as TextBox;
                TextBox txt_limitingsize = gv_storedetails.FooterRow.FindControl("txt_footlimitingsize") as TextBox;
                TextBox txt_anual_ptodcapicity = gv_storedetails.FooterRow.FindControl("txt_footannualcapacity") as TextBox;
                TextBox txt_noofshifts = gv_storedetails.FooterRow.FindControl("txt_footnoofshifts") as TextBox;
                TextBox txt_qualitaive = gv_storedetails.FooterRow.FindControl("txt_footqualitativecapcity") as TextBox;
                TextBox txt_quantaive = gv_storedetails.FooterRow.FindControl("txt_footquantitativecapacity") as TextBox;
                //DropDownList ddlhscode = gv_storedetails.FooterRow.FindControl("ddl_foothscode") as DropDownList;
                TextBox txthscode = gv_storedetails.FooterRow.FindControl("txt_foothscode") as TextBox;
                HiddenField hdnhscode = gv_storedetails.FooterRow.FindControl("hid_foothscodeId") as HiddenField;

                TextBox txtRemarks = gv_storedetails.FooterRow.FindControl("txt_Remarks") as TextBox;
                TextBox txt_footNIC = gv_storedetails.FooterRow.FindControl("txt_footNIC") as TextBox;

                //RadioButtonList rdlPmps = gv_storedetails.FooterRow.FindControl("rdl_footpmps") as RadioButtonList;

                clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
                string seqno = txt_seqno.Text;

                if (seqno == string.Empty)
                {
                    seqno = "0";
                }

                string remarks = "";
                if (txtRemarks.Text != "")
                {
                    remarks = txtRemarks.Text;
                }
                else
                {
                    remarks = "";
                }

                DataTable dt = new DataTable();
                //dt = objcom.Check_store_sequence(Convert.ToInt32(DDLSTORE.SelectedValue), Convert.ToInt32(txt_seqno.Text));
                //if (dt != null)
                //{
                //    if (dt.Rows.Count > 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Sequence No  " + seqno + " Already exists  ');", true);
                //        return;
                //    }
                //}
                string hscodeornpc = "";
                hscodeornpc = hdn_enterprise.Value;
                if (hscodeornpc == "3")
                {
                    RadioButtonList rdo = gv_storedetails.FooterRow.FindControl("rdooption_footer") as RadioButtonList;
                    hscodeornpc = rdo.SelectedValue;
                }
                int txt_sno_new = 0;
                try
                {

                    txt_sno_new = Convert.ToInt32(txt_sno.Text);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Sequence No  will be numeric only!  ');", true);
                    return;
                }
                int result = f_Insert_Update_Store_details_admin(Convert.ToInt32(Request.QueryString["urlid"]),
                    Convert.ToInt32(hndamendmentid.Value), 0, Convert.ToString(txt_sno.Text),
                    Convert.ToInt32(DDLSTORE.SelectedValue), txt_store_des.Text, txt_ref_bis.Text,
                    txt_model_brand.Text, txt_limitingsize.Text, txt_anual_ptodcapicity.Text,
                    Convert.ToInt32(txt_noofshifts.Text == "" ? "0" : txt_noofshifts.Text), txt_qualitaive.Text, txt_quantaive.Text,
                    hdnhscode.Value,
                    //rdlPmps.SelectedValue, 
                    UserId, UserId, Convert.ToInt32(seqno), hscodeornpc, remarks, txt_footNIC.Text);
                gv_storedetails.EditIndex = -1;
                BindGridView();
            }
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }

    }
    private void BindGridView()
    {
        try
        {
            clsCompanyInformationEdit company = new clsCompanyInformationEdit();
            int amendmentid = 0;// 
                                // int amendmentid = company.f_get_amendmentid(Convert.ToInt32(Request.QueryString["id"]));
            hndamendmentid.Value = amendmentid.ToString();
            DataSet ds = new DataSet();
            int companyid, processid;
            companyid = Convert.ToInt32(Request.QueryString["id"]);
            processid = Convert.ToInt32(Request.QueryString["urlid"]);

            if (Request.QueryString.HasKeys())
            {
                companyid = Convert.ToInt32(Request.QueryString["id"]);
                DataTable dtenterpriseval = new DataTable();
                clsCompany objCompany = new clsCompany();
                dtenterpriseval = objCompany.Fetch_Unit_Enterprise(companyid.ToString());
                string enterpriseval = dtenterpriseval.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
                if (enterpriseval == "0")
                {
                    enterpriseval = "1";
                }
                hdn_enterprise.Value = enterpriseval;
            }


            ds = company.Company_Store_Details(companyid, processid);
            if (ds.Tables[0].Rows.Count != 0)
            {
                gv_storedetails.DataSource = ds;
                gv_storedetails.DataBind();
                if (ds.Tables[1].Rows.Count != 0)
                {
                    txt_hscode1.Text = ds.Tables[1].Rows[0]["hscode1"].ToString();
                    txt_hscode2.Text = ds.Tables[1].Rows[0]["hscode2"].ToString();
                    txt_hscode3.Text = ds.Tables[1].Rows[0]["hscode3"].ToString();
                    txt_hscode4.Text = ds.Tables[1].Rows[0]["hscode4"].ToString();
                }
                if (hdn_enterprise.Value == "3")
                {
                    rdo_option1.SelectedValue = ds.Tables[1].Rows[0]["type1"].ToString();
                    rdo_option2.SelectedValue = ds.Tables[1].Rows[0]["type2"].ToString();
                    rdo_option3.SelectedValue = ds.Tables[1].Rows[0]["type3"].ToString();
                    rdo_option4.SelectedValue = ds.Tables[1].Rows[0]["type4"].ToString();
                }
            }
            else
            {
                DataTable dtblank = new DataTable();
                dtblank.Columns.Add("GPtbl_Company_Id");
                dtblank.Columns.Add("fGPtbl_CUnitAmendment_Id");
                dtblank.Columns.Add("fGPtbl_CUnit_Id");
                dtblank.Columns.Add("fGPtbl_CUnit_1B_Factory_Address");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_Id");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_DescriptionStoresProducts");

                dtblank.Columns.Add("fGPtbl_CUnit_Store_Sequence");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_SNo");

                dtblank.Columns.Add("fGPtbl_CUnit_Store_RefOfBISSpecification");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_ModelBrand");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_LimitingSizeCapacityRating");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_AnnualProductionCapacity");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_NoOfShifts");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_QualitativeCapacity");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_QuantitativeCapacity");
                dtblank.Columns.Add("fGPtbl_CUnit_Store_HsCode");
                //-->Added by Isha

                dtblank.Columns.Add("fGPtbl_Cunit_Store_NIC");
                
                dtblank.Columns.Add("fGPtbl_CUnit_Store_ManRemarks");
                dtblank.Columns.Add("fGPtbl_CUnit_Enterprise");
                //<--(for store crash)
                //dtblank.Columns.Add("fGPtbl_CUnit_Store_Pmps");

                DataRow drnew = dtblank.NewRow();
                drnew["GPtbl_Company_Id"] = "test";
                drnew["fGPtbl_CUnitAmendment_Id"] = "test";
                dtblank.Rows.Add(drnew);
                gv_storedetails.DataSource = dtblank;
                gv_storedetails.DataBind();
                gv_storedetails.Rows[0].Visible = false;
            }
            Setenterprise();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }
    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    int checkManufacturing = 0;
    protected void gv_storedetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow &&
        (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
        {
            // Here you will get the Control you need like:
            DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddl_editlocation");

            DataTable dtloc = new DataTable();
            clsCompany company = new clsCompany();
            clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
            // dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
            if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
            {
                dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                dtloc = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
            }

            ddllocation.DataTextField = dtloc.Columns["fAddress"].ToString();
            ddllocation.DataValueField = dtloc.Columns["fGPtbl_CUnit_Id"].ToString();
            ddllocation.DataSource = dtloc;
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new ListItem("---Select---", "0"));



        }




        try
        {
            int cellcount = 0;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnEnterprise = (HiddenField)e.Row.FindControl("hdnEnterprise");
                int EnterpriseType = Convert.ToInt32(hdnEnterprise.Value);
                // EnterpriseType = 1;
                if (EnterpriseType == 2 && checkManufacturing == 0)//added by isha if unit have both Manf+Serv
                {
                    e.Row.Cells[12].Visible = false;
                    //gv_storedetails.Columns[12].Visible =true ;  
                }
                else
                {
                    gv_storedetails.Columns[12].Visible = true;//added by isha if unit have both Manf+Serv
                    checkManufacturing += 1;//added by isha if unit have both Manf+Serv

                }


                if (gv_storedetails.EditIndex == -1)
                {
                    Label lblProduct = (Label)e.Row.FindControl("lbl_product");
                    Label lblSpecification = (Label)e.Row.FindControl("lbl_refbisspecification");
                    //Literal lblSpecification = (Literal)e.Row.FindControl("lbl_refbisspecification");
                    Label lblModel = (Label)e.Row.FindControl("lbl_model");
                    Label lblLimiting = (Label)e.Row.FindControl("lbl_limitingsize");
                    Label lblCapacity = (Label)e.Row.FindControl("lbl_annualcapacity");
                    Label lblQuality = (Label)e.Row.FindControl("lbl_qualitativecapcity");
                    Label lblQuantity = (Label)e.Row.FindControl("lbl_quantitativecapacity");

                    Label lblRemarks = (Label)e.Row.FindControl("lbl_Remarks");


                    if (lblProduct.Text.Contains("\r\n"))
                    {
                        lblProduct.Text = lblProduct.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblSpecification.Text.Contains("\r\n"))
                    {
                        lblSpecification.Text = lblSpecification.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblModel.Text.Contains("\r\n"))
                    {
                        lblModel.Text = lblModel.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblLimiting.Text.Contains("\r\n"))
                    {
                        lblLimiting.Text = lblLimiting.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblCapacity.Text.Contains("\r\n"))
                    {
                        lblCapacity.Text = lblCapacity.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblQuality.Text.Contains("\r\n"))
                    {
                        lblQuality.Text = lblQuality.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblQuantity.Text.Contains("\r\n"))
                    {
                        lblQuantity.Text = lblQuantity.Text.Replace("\r\n", "<br/>");
                    }

                    if (lblRemarks.Text.Contains("\r\n"))
                    {
                        lblRemarks.Text = lblRemarks.Text.Replace("\r\n", "<br/>");
                    }




                    //===========CODE FOR MERGING START=========================
                    DataRowView drv = (DataRowView)e.Row.DataItem;

                    if (e.Row.RowIndex > 0)
                    {
                        if (drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() == "0")
                        {
                            //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                            if (gv_storedetails.Rows[firstrowspecification].Cells[4].RowSpan == 0)
                            {
                                gv_storedetails.Rows[firstrowspecification].Cells[4].RowSpan = 2;
                            }
                            else
                            {
                                gv_storedetails.Rows[firstrowspecification].Cells[4].RowSpan += 1;
                            }

                            //Remove the cell
                            //e.Row.Cells.RemoveAt(3);
                            e.Row.Cells[4].Visible = false;
                        }

                        else //IT'S NEW VALUE
                        {
                            //Set the vertical alignment to top
                            e.Row.VerticalAlign = VerticalAlign.Top;
                            firstrowspecification = e.Row.RowIndex;
                        }

                        if (drv["fGPtbl_CUnit_Store_QualitativeCapacity"].ToString() == "0")
                        {
                            //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                            if (gv_storedetails.Rows[firstrowquality].Cells[9].RowSpan == 0)
                            {
                                gv_storedetails.Rows[firstrowquality].Cells[9].RowSpan = 2;
                            }
                            else
                            {
                                gv_storedetails.Rows[firstrowquality].Cells[9].RowSpan += 1;
                            }
                            //Remove the cell
                            cellcount = e.Row.Cells.Count;
                            //e.Row.Cells.RemoveAt(cellcount - 4);
                            e.Row.Cells[cellcount - 4].Visible = false;
                        }

                        else //IT'S NEW VALUE
                        {
                            //Set the vertical alignment to top
                            e.Row.VerticalAlign = VerticalAlign.Top;
                            firstrowquality = e.Row.RowIndex;
                        }

                        if (drv["fGPtbl_CUnit_Store_QuantitativeCapacity"].ToString() == "0")
                        {
                            //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                            if (gv_storedetails.Rows[firstrowquantity].Cells[10].RowSpan == 0)
                            {
                                gv_storedetails.Rows[firstrowquantity].Cells[10].RowSpan = 2;
                            }
                            else
                            {
                                gv_storedetails.Rows[firstrowquantity].Cells[10].RowSpan += 1;
                            }
                            //Remove the cell

                            cellcount = e.Row.Cells.Count;
                            //e.Row.Cells.RemoveAt(cellcount-3);
                            e.Row.Cells[cellcount - 3].Visible = false;


                        }

                        else //IT'S A NEW VALUE
                        {
                            //Set the vertical alignment to top
                            e.Row.VerticalAlign = VerticalAlign.Top;
                            firstrowquantity = e.Row.RowIndex;
                        }
                    }

                    //===========CODE FOR MERGING END

                }

                if (gv_storedetails.EditIndex == e.Row.RowIndex)
                {

                    DataTable dthscode = new DataTable();
                    dthscode.Clear();
                    dthscode.Columns.Add("ID");
                    dthscode.Columns.Add("HSTYPE");
                    DataRow drow = dthscode.NewRow();
                    drow["ID"] = "1";
                    drow["HSTYPE"] = "HS Code";
                    dthscode.Rows.Add(drow);

                    DataRow drow1 = dthscode.NewRow();
                    drow1["ID"] = "2";
                    drow1["HSTYPE"] = "NPC Code";
                    dthscode.Rows.Add(drow1);

                    RepeaterDelegate repeatdelreg1;
                    clsCompany company = new clsCompany();
                    repeatdelreg1 = company.Company_Fetch_FactoryLocation;
                    DropDownList ddllocation = (DropDownList)e.Row.FindControl("ddl_editlocation");
                    DataTable dt = new DataTable();
                    //if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
                    if (Convert.ToInt32(Request.QueryString["id"]) != null)
                    {
                        dt = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
                    }
                    /*else
                    {
                        clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
                        dt = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
                    }*/

                    ddllocation.DataTextField = dt.Columns["fAddress"].ToString();
                    ddllocation.DataValueField = dt.Columns["fGPtbl_CUnit_Id"].ToString();
                    ddllocation.DataSource = dt;
                    ddllocation.DataBind();
                    ddllocation.Items.Insert(0, new ListItem("---Select---", "0"));
                    HiddenField hdnuntid = (HiddenField)e.Row.FindControl("hdnUnitid");
                    if (Convert.ToInt32(hdnuntid.Value) != 0)
                    {
                        ddllocation.SelectedValue = hdnuntid.Value;
                    }

                    RadioButtonList hstype = (RadioButtonList)e.Row.FindControl("rdooption_edit");
                    HiddenField hdn_hstype = (HiddenField)e.Row.FindControl("hdnfHScode");

                    if (hdn_enterprise.Value != "3")
                    {
                        hstype.Visible = false;
                        hdn_hstype.Value = hdn_enterprise.Value;
                    }

                    hstype.DataValueField = "ID";
                    hstype.DataTextField = "HSTYPE";
                    hstype.DataSource = dthscode;
                    hstype.DataBind();
                    if (hdn_hstype.Value != "")
                    {
                        hstype.SelectedValue = hdn_hstype.Value;
                    }
                    else
                    {
                        hstype.SelectedValue = "1";
                    }

                    //   DropDownList ddhscode = (DropDownList)e.Row.FindControl("ddl_hscode");
                    //DataTable dthscode = new DataTable();
                    //dthscode = clsDDL.ddlHscode();
                    //ddhscode.DataSource = dthscode;
                    //ddhscode.DataValueField = "fGPtbl_HS_HScode";
                    //ddhscode.DataTextField = "fGPtbl_HS_HScode";
                    //ddhscode.DataBind();
                    //ddhscode.Width = 40;
                    //ddhscode.Items.Insert(0, new ListItem("---Select---", "0"));
                    //   HiddenField hdnHScode = e.Row.FindControl("hdnfHScode") as HiddenField;
                    // if (Convert.ToInt32(hdnHScode.Value) != 0)
                    //{
                    //  ddhscode.SelectedValue = hdnHScode.Value;
                    //}

                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //DropDownList ddfootloc = gv_storedetails.FooterRow.FindControl("ddl_footlocation") as DropDownList;
                //DropDownList ddlfoothscode = e.Row.FindControl("ddl_foothscode") as DropDownList;
                //DataTable dt = new DataTable();
                //dt = clsDDL.ddlHscode();
                //ddlfoothscode.DataSource = dt;
                //ddlfoothscode.DataBind();

                //   ddlfoothscode.Items.Insert(0, new ListItem("---Select---", "0"));

                DropDownList ddlfootloc;
                ddlfootloc = e.Row.FindControl("ddl_footlocation") as DropDownList;
                DataTable dtloc = new DataTable();
                clsCompany company = new clsCompany();
                clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
                // dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
                if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
                {
                    dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    dtloc = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
                }
                ddlfootloc.DataSource = dtloc;
                ddlfootloc.DataBind();

                ddlfootloc.Items.Insert(0, new ListItem("---Select---", "0"));
                this.ViewState.Add("FactoryList", dtloc);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_storedetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        hdn_edittemplate.Value = (Convert.ToInt32(e.NewEditIndex.ToString()) + 1).ToString();
        gv_storedetails.EditIndex = e.NewEditIndex;

        ////GridViewRow editingRow = gv_storedetails.Rows[e.NewEditIndex];
        ////DropDownList ddlPbx = (editingRow.FindControl("ddl_editlocation") as DropDownList);



        ////DataTable dtloc = new DataTable();
        ////clsCompany company = new clsCompany();
        ////clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
        ////// dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
        ////if (Convert.ToInt32(Request.QueryString["urlid"]) == 1)
        ////{
        ////    dtloc = company.Company_Fetch_FactoryLocation(Convert.ToInt32(Request.QueryString["id"]));
        ////}
        ////else
        ////{
        ////    dtloc = objCompanyEdit.Company_Fetch_FactoryLocation_AMENDMENT(Convert.ToInt32(Request.QueryString["id"]));
        ////}
        ////ddlPbx.Items.Clear();
        ////ddlPbx.DataSource = dtloc;
        ////ddlPbx.DataBind();

        ////ddlPbx.Items.Insert(0, new ListItem("---Select---", "0"));
        ////this.ViewState.Add("FactoryList", dtloc);


        BindGridView();
    }

    protected void btn_add_Click(object sender, EventArgs e)
    {

    }

    public void Setenterprise()
    {
        var val = hdn_enterprise.Value;
        //  RadioButtonList rdo = gv_storedetails.FooterRow.FindControl("rdooption_footer") as RadioButtonList;
        if (val == "1")
        {
            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile HS Code";
            gv_storedetails.FooterRow.FindControl("rdooption_footer").Visible = false;
        }
        else if (val == "2")
        {

            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile NPC Code";
            gv_storedetails.FooterRow.FindControl("rdooption_footer").Visible = false;
        }
        else
        {
            rdo_option1.Visible = true;
            rdo_option2.Visible = true;
            rdo_option3.Visible = true;
            rdo_option4.Visible = true;
            lbl_heading.Text = "Profile HS/NPC Code";
            gv_storedetails.FooterRow.FindControl("rdooption_footer").Visible = true;
        }
    }

    protected void gv_storedetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //try
        //{

        //To Insert  Cunit_store_history

        clsCompany objcom1 = new clsCompany();
        int k = objcom1.Company_Insert_HistoryTableCunitStore(Convert.ToInt32(Request.QueryString["id"]));

        GridViewRow row = (GridViewRow)gv_storedetails.Rows[e.RowIndex];
        int storeid = Convert.ToInt32(gv_storedetails.DataKeys[e.RowIndex].Value.ToString());
        int storevalue = Convert.ToInt32(((DropDownList)row.FindControl("ddl_editlocation")).SelectedValue);
        string sno = ((TextBox)row.FindControl("txt_Sno")).Text;
        string seqno = ((TextBox)row.FindControl("txt_Seqno")).Text;
        int unitid = Convert.ToInt32(((DropDownList)row.FindControl("ddl_editlocation")).SelectedValue);
        string productdes = ((TextBox)row.FindControl("txt_product")).Text;
        string refbisspecification = ((TextBox)row.FindControl("txt_refbisspecification")).Text;
        string modelbrand = ((TextBox)row.FindControl("txt_model")).Text;
        string limitsize = ((TextBox)row.FindControl("txt_limitingsize")).Text;
        string anualcapcity = ((TextBox)row.FindControl("txt_annualcapacity")).Text;
        string noofshift = ((TextBox)row.FindControl("txt_noofshifts")).Text;
        string txtEditNIC = ((TextBox)row.FindControl("txtEditNIC")).Text;

        string Remarks_man = "";
        Remarks_man = ((TextBox)row.FindControl("txt_RemarksEdit")).Text;

        int noofshiftint;
        if (noofshift == "")
        {
            noofshiftint = 0;
        }
        else
        {
            noofshiftint = Convert.ToInt32(noofshift);
        }
        //string qualitaive =HttpUtility.HtmlEncode(((TextBox)row.FindControl("txt_qualitativecapcity")).Text);
        //string quantitaive =HttpUtility.HtmlEncode(((TextBox)row.FindControl("txt_quantitativecapacity")).Text);


        string qualitaive = ((TextBox)row.FindControl("txt_qualitativecapcity")).Text;
        string quantitaive = ((TextBox)row.FindControl("txt_quantitativecapacity")).Text;



        string hscode = ((HiddenField)row.FindControl("hid_editscodeId")).Value;
        string hstype = hdn_enterprise.Value;
        if (hstype == "3")
        {
            hstype = ((RadioButtonList)row.FindControl("rdooption_edit")).SelectedValue;
        }
        //string pmps = ((RadioButtonList)row.FindControl("rdl_pmps")).SelectedValue;

        //clsCompany objcom = new clsCompany();
        clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();


        //    clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
        //  string seqno = txt_seqno.Text;
        DataTable dt = new DataTable();
        //dt = objCompanyEdit.Check_store_sequence(storevalue, Convert.ToInt32(seqno));
        //if (dt != null)
        //{
        //    if (dt.Rows.Count > 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Sequence No  " + seqno + " Already exists  ');", true);
        //        return;
        //    }
        //}
        if (seqno == string.Empty)
        {
            seqno = "0";
        }
        if (unitid == Convert.ToInt32("0"))
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Please Select Location  ');", true);
            return;
        }
        if (productdes == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Please enter Product  ');", true);
            return;
        }
        int sno_new = 0;
        try
        {
            sno_new = Convert.ToInt32(sno);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Please numeric value only in Serial no.  ');", true);
            return;
        }

        int result = f_Insert_Update_Store_details_admin(Convert.ToInt32(Request.QueryString["urlid"]),
            Convert.ToInt32(hndamendmentid.Value), storeid, sno,
            unitid, productdes, refbisspecification,
           modelbrand, limitsize, anualcapcity,
            Convert.ToInt32(noofshiftint), qualitaive, quantitaive, hscode, //pmps,
            UserId, UserId, Convert.ToInt32(seqno), hstype, Remarks_man, txtEditNIC);
        // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(result);", true);


        if (result == 1)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "alertmessage", "alert('Store Updated Successfully');", true);
            gv_storedetails.EditIndex = -1;
            this.BindGridView();

            return;
        }


        // Response.Write(result);
        // gv_storedetails.EditIndex = 4;
        //// gv_storedetails.EditIndex = e.NewEditIndex;
        // BindGridView();



        // BindGridView();



        // ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert( store updated successfully')", true);
        //  gv_storedetails.EditIndex = -1;
        //  this.BindGridView();

        //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert(' Please Select Location  ');", true);
        //   return;






        // gv_storedetails_RowCancelingEdit(gv_storedetails, new GridViewCancelEditEventArgs());

        // gv_storedetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e);
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        //}
    }

    protected void gv_storedetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            GridViewRow gvrow = (GridViewRow)gv_storedetails.Rows[e.RowIndex];
            int storeid = Convert.ToInt32(gv_storedetails.DataKeys[gvrow.RowIndex].Value.ToString());
            //clsCompany comobj = new clsCompany();
            clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
            int result = objCompanyEdit.f_deactivatestore(Convert.ToInt32(Request.QueryString["urlid"]), storeid);
            if (result != 0)
            {
                BindGridView();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmessage", "javascript:alert( store deleted successfully')", true);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }


    protected void gv_storedetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gv_storedetails.EditIndex = -1;
        BindGridView();
    }

    protected void BindRepeaterPmps()
    {
        DataTable Mydt = new DataTable();
        clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
        int companyid;
        companyid = Convert.ToInt32(Request.QueryString["id"]);

        Mydt = objCompanyEdit.Fetch_CompanyLocationPmps(companyid);
        if (Mydt.Rows.Count > 0)
        {
            repeaterPmps.DataSource = Mydt;
            repeaterPmps.DataBind();
            hdn_enterprise.Value = Mydt.Rows[0]["enterprise"].ToString();
        }
    }

    protected void btnpmpssubmit_Click(object sender, EventArgs e)
    {
        HiddenField Hfunitidpmps;
        RadioButtonList rdlist;
        string allvalue = string.Empty;
        foreach (RepeaterItem item in repeaterPmps.Items)
        {
            Hfunitidpmps = (HiddenField)(item.FindControl("hfUnitidpmps"));

            rdlist = (RadioButtonList)(item.FindControl("radlist"));

            // CONCATENATE CHECKBOX VALUES IN A STRING
            if (rdlist.SelectedItem.Value == "pm")
            {
                allvalue = allvalue + Hfunitidpmps.Value + ",pm@";
            }
            else if (rdlist.SelectedItem.Value == "pmps")
            {
                allvalue = allvalue + Hfunitidpmps.Value + ",pmps@";
            }
            else if (rdlist.SelectedItem.Value == "PA")
            {
                allvalue = allvalue + Hfunitidpmps.Value + ",PA@";
            }
        }
        clsCompanyInformationEdit objCompanyEdit = new clsCompanyInformationEdit();
        string comid;
        StringBuilder str = new StringBuilder();
        if (Request.QueryString.HasKeys())
        {
            comid = Request.QueryString["id"].ToString();
            str.Append("<CompanyHSData>");
            //if (Request.Form[txt_hscode1.UniqueID] != "")
            //{
            str.Append("<Company>");
            str.Append("<ID>1</ID>");
            str.Append("<HSCODE>" + Request.Form[txt_hscode1.UniqueID] + "</HSCODE>");
            if (hdn_enterprise.Value == "3")
            {
                str.Append("<HSTYPE>" + rdo_option1.SelectedValue + "</HSTYPE>");
            }
            else
            {
                str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
            }
            str.Append("</Company>");
            // }
            // if (Request.Form[txt_hscode2.UniqueID] != "")
            //{
            str.Append("<Company>");
            str.Append("<ID>2</ID>");
            str.Append("<HSCODE>" + Request.Form[txt_hscode2.UniqueID] + "</HSCODE>");
            if (hdn_enterprise.Value == "3")
            {
                str.Append("<HSTYPE>" + rdo_option2.SelectedValue + "</HSTYPE>");
            }
            else
            {
                str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
            }
            str.Append("</Company>");
            //}
            // if (Request.Form[txt_hscode3.UniqueID] != "")
            //{
            str.Append("<Company>");
            str.Append("<ID>3</ID>");
            str.Append("<HSCODE>" + Request.Form[txt_hscode3.UniqueID] + "</HSCODE>");
            if (hdn_enterprise.Value == "3")
            {
                str.Append("<HSTYPE>" + rdo_option3.SelectedValue + "</HSTYPE>");
            }
            else
            {
                str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
            }
            str.Append("</Company>");
            //}
            // if (Request.Form[txt_hscode4.UniqueID] != "")
            //{
            str.Append("<Company>");
            str.Append("<ID>4</ID>");
            str.Append("<HSCODE>" + Request.Form[txt_hscode4.UniqueID] + "</HSCODE>");
            if (hdn_enterprise.Value == "3")
            {
                str.Append("<HSTYPE>" + rdo_option4.SelectedValue + "</HSTYPE>");
            }
            else
            {
                str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
            }
            str.Append("</Company>");
            //  }
            str.Append("</CompanyHSData>");

            int success = Update_FactoryPmps(allvalue, UserId, comid, str.ToString());
            if (success == 2)
            {
                BindGridView();
                ltlMessage.Text = "Successfully Saved";
                ltlMessage.Visible = true;
            }
            else
            {
                ltlMessage.Text = "Server busy, Please try later";
                ltlMessage.Visible = true;
            }

        }
    }

    private int Update_FactoryPmps(string Pmpsvalue, int userid, string comapnyId, string xmldata)
    {
        SqlCommand MySqlComm;
        int result = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyEdit_UpdateStorePMPS", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@PmpsValue", SqlDbType.VarChar, 2000).Value = Pmpsvalue;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.VarChar, 200).Value = comapnyId;
            MySqlComm.Parameters.Add("@xmldata", SqlDbType.VarChar, 5000).Value = xmldata;
            SqlParameter outvar = new SqlParameter("@OutResult", SqlDbType.Int);
            outvar.Direction = ParameterDirection.Output;

            MySqlComm.Parameters.Add(outvar);
            MySqlComm.ExecuteNonQuery();
            result = Convert.ToInt32(outvar.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            // MySqlComm.Dispose();
        }
        return result;
    }

    private int f_Insert_Update_Store_details_admin(int proceeid, int amendid, int storid, string sno, int storeunitid, string productdes,
string ref_bis_specification, string modelbrand, string limitingsize, string anualprodcapacity,
int noofshifts, string qualitativecapcity, string quantative, string hscode, //string pmps,
int addby, int editby, int seqno, string hsnpctype, string Remarks_Man, string NIC)
    {
        SqlCommand MySqlComm;
        int outresult = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_update_store_admin_04Oct22", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.AddWithValue("@processid", proceeid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_Id", storid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_SNo", sno);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_SeqNo", seqno);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_UnitId", storeunitid);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_DescriptionStoresProducts", productdes);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_RefOfBISSpecification", ref_bis_specification);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_ModelBrand", modelbrand);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_LimitingSizeCapacityRating", limitingsize);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_AnnualProductionCapacity", anualprodcapacity);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_NoOfShifts", noofshifts);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_QualitativeCapacity", qualitativecapcity);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_QuantitativeCapacity", quantative);
            //MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_Pmps", pmps);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_HsCode", hscode);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_CUser", addby);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_EUser", editby);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnitAmendment_Id", amendid);
            //MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnitAmendment_Id", amendid);
            MySqlComm.Parameters.AddWithValue("@HS_NPCtype", hsnpctype);

            MySqlComm.Parameters.AddWithValue("@Remarks_Man", Remarks_Man);
            MySqlComm.Parameters.AddWithValue("@fGPtbl_CUnit_Store_NIC", NIC);

            SqlParameter outVal = new SqlParameter("@RESULT", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);
            MySqlComm.ExecuteNonQuery();
            outresult = Convert.ToInt32(outVal.Value);
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            //MySqlComm.Dispose();
        }

        return outresult;
    }

    protected void repeaterPmps_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //BINDING PMPS
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField hfpmps = (HiddenField)e.Item.FindControl("hfRdlPmps");
            RadioButtonList rdblpmps = (RadioButtonList)e.Item.FindControl("radlist");
            rdblpmps.SelectedValue = hfpmps.Value;
        }
    }

    protected void ddl_footlocation_change(object sender, EventArgs e)
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

        var ddlFactory = ((DropDownList)(gv_storedetails.FindControl("ddl_footlocation")));
        ddlFactory = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddlFactory.NamingContainer;

        TextBox txt_seqno = (TextBox)gvr.FindControl("txt_footSseqno") as TextBox;
        TextBox txt_store_des = (TextBox)gvr.FindControl("txt_footproduct");
        TextBox txt_ref_bis = (TextBox)gvr.FindControl("txt_footrefbisspecification") as TextBox;
        TextBox txt_model_brand = (TextBox)gvr.FindControl("txt_footmodel") as TextBox;
        TextBox txt_limitingsize = (TextBox)gvr.FindControl("txt_footlimitingsize") as TextBox;
        TextBox txt_anual_ptodcapicity = (TextBox)gvr.FindControl("txt_footannualcapacity") as TextBox;
        TextBox txt_noofshifts = (TextBox)gvr.FindControl("txt_footnoofshifts") as TextBox;
        TextBox txt_qualitaive = (TextBox)gvr.FindControl("txt_footqualitativecapcity") as TextBox;
        TextBox txt_quantaive = (TextBox)gvr.FindControl("txt_footquantitativecapacity") as TextBox;
        TextBox txthscode = (TextBox)gvr.FindControl("txt_foothscode") as TextBox;

        int FactoryId = Convert.ToInt32(ddlFactory.SelectedValue);
        if (FactoryId == 0)
            return;
        DataRow dr = dtFactoryDetails.Select("fGPtbl_CUnit_Id=" + FactoryId)[0];
        int enterpriseType = dr != null ? Convert.ToInt32(dr["fGPtbl_CUnit_Enterprise"]) : -1;
        hdn_enterprise.Value = enterpriseType.ToString();
        if (enterpriseType == 1 || enterpriseType == -1)  //Manufacturing
        {
            if (txt_seqno != null) txt_seqno.ReadOnly = false;
            if (txt_store_des != null) txt_store_des.ReadOnly = false;
            if (txt_ref_bis != null) txt_ref_bis.ReadOnly = false;
            if (txt_model_brand != null) txt_model_brand.ReadOnly = false;
            if (txt_limitingsize != null) txt_limitingsize.ReadOnly = false;
            if (txt_anual_ptodcapicity != null) txt_anual_ptodcapicity.ReadOnly = false;
            if (txt_noofshifts != null) txt_noofshifts.ReadOnly = false;
            if (txt_qualitaive != null) txt_qualitaive.ReadOnly = false;
            if (txt_quantaive != null) txt_quantaive.ReadOnly = false;
            if (txthscode != null) txthscode.ReadOnly = false;



            if (txt_store_des != null) txt_store_des.BackColor = Color.White;
            if (txt_ref_bis != null) txt_ref_bis.BackColor = Color.White;
            if (txt_model_brand != null) txt_model_brand.BackColor = Color.White;
            if (txt_limitingsize != null) txt_limitingsize.BackColor = Color.White;
            if (txt_anual_ptodcapicity != null) txt_anual_ptodcapicity.BackColor = Color.White;
            if (txt_noofshifts != null) txt_noofshifts.BackColor = Color.White;
            if (txt_qualitaive != null) txt_qualitaive.BackColor = Color.White;
            if (txt_quantaive != null) txt_quantaive.BackColor = Color.White;
            if (txthscode != null) txthscode.BackColor = Color.White;



        }
        else if (enterpriseType == 2)  //Service
        {
            if (txt_seqno != null) txt_seqno.ReadOnly = false;
            if (txt_store_des != null) txt_store_des.ReadOnly = false;
            if (txt_ref_bis != null) txt_ref_bis.ReadOnly = false;
            if (txt_model_brand != null)
            {
                txt_model_brand.ReadOnly = true;
                txt_model_brand.BackColor = Color.Silver;
                txt_model_brand.Text = "";
            }
            if (txt_limitingsize != null)
            {
                txt_limitingsize.ReadOnly = true;
                txt_limitingsize.BackColor = Color.Silver;
                txt_limitingsize.Text = "";
            }
            if (txt_anual_ptodcapicity != null)
            {
                txt_anual_ptodcapicity.ReadOnly = true;
                txt_anual_ptodcapicity.BackColor = Color.Silver;
                txt_anual_ptodcapicity.Text = "";
            }
            if (txt_noofshifts != null)
            {
                txt_noofshifts.ReadOnly = true;
                txt_noofshifts.Text = "";
                txt_noofshifts.BackColor = Color.Silver;
            }
            if (txt_qualitaive != null)
            {
                txt_qualitaive.ReadOnly = true;
                txt_qualitaive.Text = "";
                txt_qualitaive.BackColor = Color.Silver;
            }
            if (txt_quantaive != null)
            {
                txt_quantaive.ReadOnly = false;

            }
            if (txthscode != null)
            {
                txthscode.ReadOnly = false;

            }


        }

















    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static HsCodeList[] Bind_Searchgrid(string searchitem, string codetype)
    {
        DataTable dt = new DataTable();
        List<HsCodeList> details = new List<HsCodeList>();
        //    clsCompanyBacklog objback = new clsCompanyBacklog();
        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);
        try
        {
            SqlCommand cmd = new SqlCommand("GP_sp_searchHSCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Product", SqlDbType.VarChar, 5000).Value = searchitem == "" ? null : searchitem;
            cmd.Parameters.Add("@tppe", SqlDbType.Int).Value = codetype == "" ? null : codetype;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        HsCodeList hs = new HsCodeList();
                        hs.HSID = dtrow["Id"].ToString();
                        hs.HSCode = dtrow["Productcode"].ToString();
                        hs.HSCodedescription = dtrow["Productdescription"].ToString();
                        details.Add(hs);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        return details.ToArray();

    }

    public class HsCodeList
    {
        public string HSID { get; set; }
        public string HSCode { get; set; }
        public string HSCodedescription { get; set; }
    }




}
