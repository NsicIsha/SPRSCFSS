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

public partial class appAdministrator_BacklogUnitStep2 : System.Web.UI.Page
{

    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    string prevPage = String.Empty;

    short tabindx = 1;

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
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //if (!Page.IsPostBack)
        //{ 
        // btnSubmit.Attributes.Add(" getGrRow
        //}

        FunRequest();
        FunFillControls();
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
            prevPage = (objDecEnc.Decrypt(Request.QueryString["PanCard"].Replace(" ", "+")).ToString());
            Edit = Convert.ToInt32(Request.QueryString["Edit"].Replace(" ", "+"));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in FunRequest() " + ex.StackTrace + "  " + ex.Message);
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
            prevPage = "";
            Edit = 0;
        }
    }

    public void FunFillControls()
    {
        if (!Page.IsPostBack)
        {
            //FILL RADIOBUTTON LIST
            objCompany = new clsCompany();
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = objCompany.Company_Fetch_Constitution();
            rd3c.DataTextField = "fGPtbl_CUnit_Constitution_Name";
            rd3c.DataValueField = "fGPtbl_CUnit_Constitution_Id";
            rd3c.DataSource = Dt;
            rd3c.DataBind();

            rd3c.SelectedIndex = 0;
        }
        FunfillDropdown();

        if (!Page.IsPostBack)
        {
            FunFillData();
            FunTab();
        }

    }

    private void FunTab()
    {

        txt_3a.TabIndex = (short)(tabindx + 1);
        txt_3b.TabIndex = (short)(tabindx + 2);
        rd3c.TabIndex = (short)(tabindx + 3);
        txt_3cb.TabIndex = (short)(tabindx + 34);
        btnSubmit.TabIndex = (short)(tabindx + 35);


    }

    private void FunFillData()
    {
        try
        {
            if (Status == 5 || Status == 123 || Edit == 1)    //INCOMPLETE
            {
                objCompany = new clsCompany();


                cls_director objdir = new cls_director();
                List<Director> director = new List<Director>();
                director = objdir.FetchCompanyDirector(CompanyId);

                Bind_Director_gridview(director);


                if (director[0].Address == "" && director[0].Name == "" && director[0].Pincode == "" && director[0].State == "")
                {
                    gv_director.Rows[0].Visible = false;
                }

                System.Data.SqlClient.SqlDataReader MyDrGrid;
                MyDrGrid = objCompany.Company_Fetch_ByCompany_Step2(CompanyId);

                grdSSI.DataSource = MyDrGrid;
                grdSSI.DataBind();

                MyDrGrid.Close();


                int fCnt = 0;
                //UNIT TABLE
                objCompany = new clsCompany();
                System.Data.SqlClient.SqlDataReader MyDr;
                MyDr = objCompany.Company_Fetch_ByCompany_Step2(CompanyId);
                while (MyDr.Read())
                {
                    fCnt += 1;
                    //txt_21.Text = MyDr["fGPtbl_CUnit_SSIRegistrationNo"].ToString();
                    //txt_22.Text = MyDr["fGPtbl_CUnit_SSIRegistrationDate"].ToString();
                    txt_3a.Text = MyDr["fGPtbl_CUnit_3A"].ToString();
                    txt_3b.Text = MyDr["fGPtbl_CUnit_3B"].ToString();
                    txt_3cb.Text = MyDr["fGPtbl_CUnit_3C5"].ToString();
                    try
                    {
                        rd3c.SelectedValue = MyDr["fGPtbl_CUnit_3C123_ConstitutionId"].ToString();
                    }
                    catch { }
                    break;
                }
                MyDr.Close();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    private void FunfillDropdown()
    {
        if (!Page.IsPostBack)
        {
            funfill_ddlState();
        }
    }

    private void funfill_ddlState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        //ddlState_3ca1.DataSource = dt;
        //ddlState_3ca1.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        //ddlState_3ca1.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        //ddlState_3ca1.DataBind();
        //ddlState_3ca1.Items.Insert(0, new ListItem("--Select State--", ""));

        //ddlState_3ca2.DataSource = dt;
        //ddlState_3ca2.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        //ddlState_3ca2.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        //ddlState_3ca2.DataBind();
        //ddlState_3ca2.Items.Insert(0, new ListItem("--Select State--", ""));

        //ddlState_3ca3.DataSource = dt;
        //ddlState_3ca3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        //ddlState_3ca3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        //ddlState_3ca3.DataBind();
        //ddlState_3ca3.Items.Insert(0, new ListItem("--Select State--", ""));

        //ddlState_3ca4.DataSource = dt;
        //ddlState_3ca4.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        //ddlState_3ca4.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        //ddlState_3ca4.DataBind();
        //ddlState_3ca4.Items.Insert(0, new ListItem("--Select State--", ""));

        //ddlState_3ca5.DataSource = dt;
        //ddlState_3ca5.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        //ddlState_3ca5.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        //ddlState_3ca5.DataBind();
        //ddlState_3ca5.Items.Insert(0, new ListItem("--Select State--", ""));




        dt.Clear();
        dt.Dispose();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            //ListtoXml();
            //return ;

            List<Director> mydir = (List<Director>)ViewState["director"];

            if (gv_director.Rows.Count == 1)
            {
                if (mydir[0].Name == "" && mydir[0].Address == "" && mydir[0].Pincode == "" && mydir[0].State == "" && mydir[0].City == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
                    return;
                }
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
                //return;
            }


            //string SSIRegistrationNo = txt_21.Text.ToString().Trim();
            //string SSIRegistrationdate = txt_22.Text.ToString().Trim();
            string DateofIncorporation = txt_3a.Text.ToString().Trim();
            string DateofCommencementofProductionBusiness = txt_3b.Text.ToString().Trim();
            string threeC5 = txt_3cb.Text.ToString().Trim();
            int Constitution = 0;
            try
            {
                Constitution = Convert.ToInt32(rd3c.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }


            int i, j;
            i = 0;
            j = grdSSI.Rows.Count;

            while (i < j)
            {

                TextBox txtSSINo = (TextBox)grdSSI.Rows[i].FindControl("txt_21");
                TextBox txtSSIRegDate = (TextBox)grdSSI.Rows[i].FindControl("txt_22");
                HiddenField hdnunt = (HiddenField)grdSSI.Rows[i].FindControl("hdnunit");
                string SSIRegistrationNo = txtSSINo.Text.Trim();
                string SSIRegistrationdate = txtSSIRegDate.Text.Trim();
                UnitId = Convert.ToInt32(hdnunt.Value.ToString());

                objCompany = new clsCompany();
                ErrorId = objCompany.Company_Insert_Step2(UnitId, CompanyId, SSIRegistrationNo,
                                                            SSIRegistrationdate, DateofIncorporation, DateofCommencementofProductionBusiness,
                                                            Constitution, threeC5, UserId);
                i++;
            }



            //objCompany = new clsCompany();

            ListtoXml();
            if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
            {
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                string EncryptPrvpage = objDecEnc.Encrypt(prevPage.ToString());

                Response.Redirect("BacklogUnitStep3.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPrvpage + "&Edit=0", false);

            }
            else               //INSERT/UPDATE FAIL
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string PanCardNewRegistration = objDecEnc.Encrypt(prevPage.ToString());

        Response.Redirect("BacklogUnitStep1.aspx?pcd=" + PanCardNewRegistration, false);
      
    }

    protected void grdSSI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtSSIno = (TextBox)e.Row.FindControl("txt_21");
            TextBox txtSSIdate = (TextBox)e.Row.FindControl("txt_22");
            txtSSIno.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
            txtSSIdate.TabIndex = (short)(tabindx);
            tabindx = (short)(tabindx + 1);
        }

    }


    protected void bind_city_footer_statechange(object sender, EventArgs e)
    {
        DropDownList ddlcityfooter = (DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city");
        DropDownList ddlstatefooter = (DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state");
        if (ddlcityfooter != null && ddlcityfooter != null)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            string val = ddlstatefooter.SelectedValue;
            dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddlstatefooter.SelectedValue));
            ddlcityfooter.DataSource = dt_c;
            ddlcityfooter.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
            ddlcityfooter.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlcityfooter.DataBind();
            ddlcityfooter.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();

        }

    }

    protected void bind_city_edit_statechange(object sender, EventArgs e)
    {

        var ddl1 = ((DropDownList)(gv_director.FindControl("ddl_director_state")));
        ddl1 = (DropDownList)sender;
        GridViewRow gvr = (GridViewRow)ddl1.NamingContainer;

        DropDownList ddleditcity = (DropDownList)gvr.FindControl("ddl_director_city");

        if (ddl1 != null && ddleditcity != null)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();

            dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl1.SelectedValue));
            ddleditcity.DataSource = dt_c;
            ddleditcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
            ddleditcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddleditcity.DataBind();
            ddleditcity.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();
        }

    }



    protected void gv_director_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            DropDownList ddlstate = (DropDownList)e.Row.FindControl("ddl_footer_state");// (gv_director.FooterRow.FindControl("ddl_footer_state"));
            DropDownList ddlcity = (DropDownList)e.Row.FindControl("ddl_footer_city");//(gv_director.FooterRow.FindControl("ddl_footer_city"));

            if (ddlstate != null)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsDDL.ddlState_Activated();
                ddlstate.DataSource = dt;
                ddlstate.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
                ddlstate.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
                ddlstate.DataBind();
                ddlstate.Items.Insert(0, new ListItem("--Select State--", ""));
            }

            if (ddlstate != null && ddlcity != null)
            {
                //System.Data.DataTable dt_c = new System.Data.DataTable();
                //string val = ddlstate.SelectedValue;
                //dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddlstate.SelectedValue));
                ddlcity.DataSource = null;
                //ddlcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
                //ddlcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                ddlcity.DataBind();
                ddlcity.Items.Insert(0, new ListItem("--Select City--", ""));
                //dt_c.Clear();
                //dt_c.Dispose();
            }


        }

        if (e.Row.RowType == DataControlRowType.DataRow && gv_director.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddleditstate = (DropDownList)e.Row.FindControl("ddl_director_state");
            DropDownList ddleditcity = (DropDownList)e.Row.FindControl("ddl_director_city");
            if (ddleditstate != null)
            {
                System.Data.DataTable dt = new System.Data.DataTable();
                dt = clsDDL.ddlState_Activated();
                ddleditstate.DataSource = dt;
                ddleditstate.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
                ddleditstate.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
                ddleditstate.DataBind();

                ddleditstate.Items.FindByValue((e.Row.FindControl("lbledit_state") as Label).Text).Selected = true;
                ddleditstate.Items.Insert(0, new ListItem("--Select State--", ""));
            }

            if (ddleditstate != null && ddleditcity != null)
            {
                System.Data.DataTable dt_c = new System.Data.DataTable();
                string val = ((Label)e.Row.FindControl("lbledit_state")).Text;
                dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(val));
                ddleditcity.DataSource = dt_c;
                ddleditcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
                ddleditcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                ddleditcity.DataBind();

                ddleditcity.Items.FindByValue((e.Row.FindControl("lbledit_city") as Label).Text).Selected = true;
                //  ddleditcity.Items.Insert(0, new ListItem("--Select City--", ""));
                //dt_c.Clear();
                //dt_c.Dispose();
            }
        }
    }

    protected void AddnewDirector(object sender, EventArgs e)
    {
        string name, address, zipcode, state, city, phone, share, stateid, cityid;

        name = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directorname")).Text;
        address = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directoraddress")).Text;
        zipcode = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directorzipcode")).Text;
        state = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedItem.Text;
        stateid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedValue;
        city = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedItem.Text;
        cityid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedValue;
        phone = ((TextBox)gv_director.FooterRow.FindControl("txt_director_phone")).Text;
        share = ((TextBox)gv_director.FooterRow.FindControl("txt_director_share")).Text;

        List<Director> mydir = (List<Director>)ViewState["director"];

        if (mydir == null)
        {
            List<Director> mydir1 = new List<Director>();
            Director dir = new Director();
            dir.rowno = 1;
            dir.Name = name;
            dir.Address = address;
            dir.Pincode = zipcode;
            dir.stateid = stateid;
            dir.State = state;
            dir.cityId = cityid;
            dir.City = city;
            dir.Telephone = phone;
            dir.ShareHolding = share;
            mydir1.Add(dir);
            Bind_Director_gridview(mydir1);
        }
        else
        {
            Director dir = new Director();
            dir.rowno = mydir.Count + 1;
            dir.Name = name;
            dir.Address = address;
            dir.Pincode = zipcode;
            dir.stateid = stateid;
            dir.State = state;
            dir.cityId = cityid;
            dir.City = city;
            dir.Telephone = phone;
            dir.ShareHolding = share;
            mydir.Add(dir);
            Bind_Director_gridview(mydir);
        }

    }

    private void Bind_Director_gridview(List<Director> Directors)
    {

        gv_director.DataSource = Directors;
        gv_director.DataBind();
        ViewState["director"] = Directors;

        if (Directors[0].Name == "" && Directors[0].Address == "" && Directors[0].Pincode == "" && Directors[0].State == "" && Directors[0].City == "")
        {
            gv_director.Rows[0].Visible = false;

        }
    }

    protected void gv_director_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_director.EditIndex = e.NewEditIndex;
        List<Director> mydir = (List<Director>)ViewState["director"];
        gv_director.DataSource = mydir;
        gv_director.DataBind();
    }
    protected void gv_director_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
        // Response.Write(userid);
        // Fin Gridview Record 
        string name, address, pincode, state, stateid, city, cityid, telephone, share;

        name = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_directorname")).Text.Trim();
        address = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_address")).Text.Trim();
        pincode = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_pincode")).Text.Trim();
        state = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedItem.Text.Trim();
        stateid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedValue.Trim();
        city = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedItem.Text.Trim();
        cityid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedValue.Trim();
        telephone = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_phone")).Text.Trim();
        share = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_share")).Text.Trim();




        List<Director> mydir = (List<Director>)ViewState["director"];

        if (mydir != null)
        {
            var item = mydir.Where(d => d.rowno == userid).FirstOrDefault();
            if (item != null)
            {
                item.Name = name;
                item.Address = address;
                item.Pincode = pincode;
                item.stateid = stateid;
                item.State = state;
                item.cityId = cityid;
                item.City = city;
                item.Telephone = telephone;
                item.ShareHolding = share;
            }
        }
        gv_director.EditIndex = -1;
        Bind_Director_gridview(mydir);

    }

    private void ListtoXml()
    {
        List<Director> mydir = (List<Director>)ViewState["director"];
        if (mydir != null)
        {
            var xDir = new XElement("Directors", from dir in mydir
                                                 select new XElement("Director", new XAttribute("ID", dir.rowno),
                                                     new XElement("Name", dir.Name),
                                                     new XElement("Address", dir.Address),
                                                     new XElement("Pincode", dir.Pincode),
                                                     new XElement("StateId", dir.stateid),
                                                     new XElement("CityId", dir.cityId),
                                                     new XElement("Telephone", dir.Telephone),
                                                     new XElement("Share", dir.ShareHolding),
                                                     new XElement("UserId", UserId)));


            //objCompany = new clsCompany();
            clsCompanyBacklog objCompanyBacklog = new clsCompanyBacklog();
            objCompanyBacklog.Company_Director_insert(xDir.ToString(), CompanyId);
        }

    }


}
