using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Configuration;
using System.Xml.Schema;
using System.Xml;
using System.Collections;

public partial class PointOfInterest : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    string prevPage = String.Empty;
    string pagename;
    short tabindx = 1;
    int Primary_Id;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    string fromAddress;
    string toAddress;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;

    string MailBodyBranch = string.Empty;
    string strOriginalMailBodyBranch = string.Empty, strMailBodyBranch = string.Empty;
    string MailSubjectBranch = string.Empty;

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
           

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
             FunRequest();
            FunFillControls();

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
        ddlNsicState.DataSource = dt;
        ddlNsicState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        ddlNsicState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlNsicState.DataBind();
        ddlNsicState.Items.Insert(0, new ListItem("--Select State--", "0"));



        //dt = new DataTable();
        //dt = clsDDL.ddlBranch_Activated(0);

        //ddlNsicDistrict.DataSource = dt;
        //ddlNsicDistrict.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
        //ddlNsicDistrict.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
        //ddlNsicDistrict.DataBind();
        //ddlNsicDistrict.Items.Insert(0, new ListItem("--Select Branch--", "0"));




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


        DataTable Dtt = new DataTable();
        Dtt = Company_Fetch_InspectionAgency();

        ddlInspectionAgency.DataSource = Dtt;
        ddlInspectionAgency.DataTextField = Dtt.Columns["fGPtbl_MIA_Name"].ToString(); ;
        ddlInspectionAgency.DataValueField = Dtt.Columns["fGPtbl_MIA_Id"].ToString();
        ddlInspectionAgency.DataBind();
        ddlInspectionAgency.Items.Insert(0, new ListItem("--Select Agency--", "0"));

    }

    public void FunFillControls()
    {
        if (!Page.IsPostBack)
        {
            //FILL RADIOBUTTON LIST
            objCompany = new clsCompany();
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = Company_Fetch_Constitution();
            ddlConstitution.DataTextField = "fGPtbl_CUnit_Constitution_Name";
            ddlConstitution.DataValueField = "fGPtbl_CUnit_Constitution_Id";
            ddlConstitution.DataSource = Dt;
            ddlConstitution.DataBind();

            //ddlConstitution.SelectedIndex = 0;

            ddlConstitution.Items.Insert(0, new ListItem("--Select --", "0"));


            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsDDL.ddlCategoryCast();
            ddlCategory.DataSource = dt;
            ddlCategory.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
            ddlCategory.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem("-- Select Social Category--", "0"));



        }
        FunfillDropdown();

        if (!Page.IsPostBack)
        {
            FunFillData();

        }

    }

    private void FunFillData()
    {

        ////objCompany = new clsCompany();

        //////System.Data.DataTable dt = new System.Data.DataTable();
        //////dt = objCompany.Company_FetchDirectorList(CompanyId);

        //////if (dt != null)
        //////{
        //////    if (dt.Rows.Count > 0)
        //////    {
        //////        gv_director.DataSource = dt;
        //////        gv_director.DataBind();
        //////    }
        //////}

        ////cls_director objdir = new cls_director();
        ////List<Director> director = new List<Director>();
        ////director = objdir.FetchCompanyDirector(CompanyId);

        ////Bind_Director_gridview(director);


        ////if (director[0].Address == "" && director[0].Name == "" && director[0].Pincode == "" && director[0].State == "")
        ////{
        ////    gv_director.Rows[0].Visible = false;
        ////}


    }

    public DataTable Company_Fetch_Constitution()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_ConstitutionFetch_POI", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
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

    public DataTable Company_Fetch_InspectionAgency()
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterInspectionAgency_Activated", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
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

    //private void funfill_ddlState()
    //{
    //    //STATE DROPDOWNLIST
    //    System.Data.DataTable dt = new System.Data.DataTable();
    //    dt = clsDDL.ddlState_Activated();
    //    ddlNsicState.DataSource = dt;
    //    ddlNsicState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
    //    ddlNsicState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
    //    ddlNsicState.DataBind();
    //    ddlNsicState.Items.Insert(0, new ListItem("--Select State--", ""));

    //    //ddlState_c3.DataSource = dt;
    //    //ddlState_c3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
    //    //ddlState_c3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
    //    //ddlState_c3.DataBind();
    //    //ddlState_c3.Items.Insert(0, new ListItem("--Select State--", ""));

    //    dt.Clear();
    //    dt.Dispose();
    //}

    //protected void gv_director_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gv_director.EditIndex = e.NewEditIndex;
    //    List<Director> mydir = (List<Director>)ViewState["director"];
    //    gv_director.DataSource = mydir;
    //    gv_director.DataBind();
    //}
    ////protected void gv_director_RowUpdating(object sender, GridViewUpdateEventArgs e)
    ////{
    ////int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());

    ////string state, stateid, city, cityid;
    ////state = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedItem.Text.Trim();
    ////stateid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedValue.Trim();
    ////city = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedItem.Text.Trim();
    ////cityid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedValue.Trim();
    ////List<Director> mydir = (List<Director>)ViewState["director"];

    ////if (mydir != null)
    ////{
    ////    var item = mydir.Where(d => d.rowno == userid).FirstOrDefault();
    ////    if (item != null)
    ////    {

    ////        item.stateid = stateid;
    ////        item.State = state;
    ////        item.cityId = cityid;
    ////        item.City = city;
    ////    }
    ////}
    ////gv_director.EditIndex = -1;
    ////Bind_Director_gridview(mydir);

    ////}
    ////protected void gv_director_RowDataBound(object sender, GridViewRowEventArgs e)
    ////{
    ////    if (e.Row.RowType == DataControlRowType.Footer)
    ////    {
    ////        DropDownList ddlstate = (DropDownList)e.Row.FindControl("ddl_footer_state");// (gv_director.FooterRow.FindControl("ddl_footer_state"));
    ////        DropDownList ddlcity = (DropDownList)e.Row.FindControl("ddl_footer_city");//(gv_director.FooterRow.FindControl("ddl_footer_city"));

    ////        if (ddlstate != null)
    ////        {
    ////            System.Data.DataTable dt = new System.Data.DataTable();
    ////            dt = clsDDL.ddlState_Activated();
    ////            ddlstate.DataSource = dt;
    ////            ddlstate.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
    ////            ddlstate.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
    ////            ddlstate.DataBind();
    ////            ddlstate.Items.Insert(0, new ListItem("--Select State--", "0"));
    ////        }

    ////        if (ddlstate != null && ddlcity != null)
    ////        {
    ////            //System.Data.DataTable dt_c = new System.Data.DataTable();
    ////            //string val = ddlstate.SelectedValue;
    ////            //dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddlstate.SelectedValue));
    ////            ddlcity.DataSource = null;
    ////            //ddlcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    ////            //ddlcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    ////            ddlcity.DataBind();
    ////            ddlcity.Items.Insert(0, new ListItem("--Select City--", "0"));
    ////            //dt_c.Clear();
    ////            //dt_c.Dispose();
    ////        }


    ////    }

    ////    if (e.Row.RowType == DataControlRowType.DataRow)
    ////    {
    ////        //LinkButton lnk_delete = (LinkButton)e.Row.FindControl("lnk_delete");

    ////        //lnk_delete.Visible = false;

    ////        //try
    ////        //{
    ////        //    if (hid_ppage.Value != null)
    ////        //    {

    ////        //        if (hid_ppage.Value.ToUpper().ToString() == "ADMINEDITREGISTRATION.ASPX")
    ////        //        {
    ////        //            lnk_delete.Visible = true;
    ////        //        }
    ////        //        else
    ////        //        {
    ////        //            lnk_delete.Visible = false;
    ////        //        }
    ////        //    }
    ////   // }
    ////        //catch (Exception ex)
    ////        //{

    ////        //}

    ////    }

    ////    if (e.Row.RowType == DataControlRowType.DataRow && gv_director.EditIndex == e.Row.RowIndex)
    ////    {
    ////        DropDownList ddleditstate = (DropDownList)e.Row.FindControl("ddl_director_state");
    ////        DropDownList ddleditcity = (DropDownList)e.Row.FindControl("ddl_director_city");
    ////        if (ddleditstate != null)
    ////        {
    ////            System.Data.DataTable dt = new System.Data.DataTable();
    ////            dt = clsDDL.ddlState_Activated();
    ////            ddleditstate.DataSource = dt;
    ////            ddleditstate.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
    ////            ddleditstate.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
    ////            ddleditstate.DataBind();

    ////            ddleditstate.Items.FindByValue((e.Row.FindControl("lbledit_state") as Label).Text).Selected = true;
    ////            ddleditstate.Items.Insert(0, new ListItem("--Select State--", ""));
    ////        }

    ////        if (ddleditstate != null && ddleditcity != null)
    ////        {
    ////            System.Data.DataTable dt_c = new System.Data.DataTable();
    ////            string val = ((Label)e.Row.FindControl("lbledit_state")).Text;
    ////            dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(val));
    ////            ddleditcity.DataSource = dt_c;
    ////            ddleditcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    ////            ddleditcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    ////            ddleditcity.DataBind();

    ////            ddleditcity.Items.FindByValue((e.Row.FindControl("lbledit_city") as Label).Text).Selected = true;
    ////            //  ddleditcity.Items.Insert(0, new ListItem("--Select City--", ""));
    ////            //dt_c.Clear();
    ////            //dt_c.Dispose();
    ////        }
    ////    }
    ////}

    ////protected void gv_director_RowDeleting(object sender, GridViewDeleteEventArgs e)
    ////{
    ////    int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
    ////    List<Director> mydir = (List<Director>)ViewState["director"];
    ////    Director dir = mydir.Where(s => s.rowno == userid).Single();
    ////    if (mydir != null)
    ////    {
    ////        mydir.Remove(dir);
    ////    }
    ////    Bind_Director_gridview(mydir);
    ////}


    ////private void Bind_Director_gridview(List<Director> Directors)
    ////{

    ////    gv_director.DataSource = Directors;
    ////    gv_director.DataBind();
    ////    ViewState["director"] = Directors;

    ////    if (Directors[0].Name == "" && Directors[0].Address == "" && Directors[0].Pincode == "" && Directors[0].State == "" && Directors[0].City == "")
    ////    {
    ////        gv_director.Rows[0].Visible = false;

    ////    }
    ////}


    protected void bind_city_footer_statechange(object sender, EventArgs e)
    {
        ////DropDownList ddlcityfooter = (DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city");
        ////DropDownList ddlstatefooter = (DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state");
        ////if (ddlcityfooter != null && ddlcityfooter != null)
        ////{
        ////    System.Data.DataTable dt_c = new System.Data.DataTable();
        ////    string val = ddlstatefooter.SelectedValue;
        ////    dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddlstatefooter.SelectedValue));
        ////    ddlcityfooter.DataSource = dt_c;
        ////    ddlcityfooter.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
        ////    ddlcityfooter.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
        ////    ddlcityfooter.DataBind();
        ////    ddlcityfooter.Items.Insert(0, new ListItem("--Select City--", ""));
        ////    dt_c.Clear();
        ////    dt_c.Dispose();

        ////}

    }

    protected void bind_city_edit_statechange(object sender, EventArgs e)
    {

        ////var ddl1 = ((DropDownList)(gv_director.FindControl("ddl_director_state")));
        ////ddl1 = (DropDownList)sender;
        ////GridViewRow gvr = (GridViewRow)ddl1.NamingContainer;

        ////DropDownList ddleditcity = (DropDownList)gvr.FindControl("ddl_director_city");

        ////if (ddl1 != null && ddleditcity != null)
        ////{
        ////    System.Data.DataTable dt_c = new System.Data.DataTable();

        ////    dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl1.SelectedValue));
        ////    ddleditcity.DataSource = dt_c;
        ////    ddleditcity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
        ////    ddleditcity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
        ////    ddleditcity.DataBind();
        ////    ddleditcity.Items.Insert(0, new ListItem("--Select City--", ""));
        ////    dt_c.Clear();
        ////    dt_c.Dispose();
        ////}

    }

    protected void AddnewDirector(object sender, EventArgs e)
    {
        ////string name, address, zipcode, state, city, phone, share, stateid, cityid, email, experiance, adhar;


        ////state = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedItem.Text;
        ////stateid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedValue;
        ////city = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedItem.Text;
        ////cityid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedValue;


        ////List<Director> mydir = (List<Director>)ViewState["director"];

        ////if (mydir == null)
        ////{
        ////    List<Director> mydir1 = new List<Director>();
        ////    Director dir = new Director();
        ////    dir.rowno = 1;

        ////    dir.stateid = stateid;
        ////    dir.State = state;
        ////    dir.cityId = cityid;
        ////    dir.City = city;

        ////    mydir1.Add(dir);
        ////    Bind_Director_gridview(mydir1);
        ////}
        ////else
        ////{
        ////    Director dir = new Director();
        ////    dir.rowno = mydir.Count + 1;

        ////    dir.stateid = stateid;
        ////    dir.State = state;
        ////    dir.cityId = cityid;
        ////    dir.City = city;

        ////    mydir.Add(dir);
        ////    Bind_Director_gridview(mydir);
        ////}

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

    protected void ddlNsicDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNsicDistrict.SelectedValue != "0" || ddlNsicDistrict.SelectedValue != "")
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = Company_Fetch_BranchAddress(Convert.ToInt32(ddlNsicDistrict.SelectedValue));
            //ddlConstitution.DataTextField = "fGPtbl_CUnit_Constitution_Name";
            //ddlConstitution.DataValueField = "fGPtbl_CUnit_Constitution_Id";
            grdFactories.DataSource = Dt;
            grdFactories.DataBind();

        }
        else
        {
            grdFactories.DataSource = null;
            grdFactories.DataBind();
        }
    }

    protected void ddlNsicState_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlNsicState.SelectedValue != "0" || ddlNsicState.SelectedValue != "")
        {
            System.Data.DataTable Dt = new System.Data.DataTable();
            Dt = Company_Fetch_ZoneId_By_StateId(Convert.ToInt32(ddlNsicState.SelectedValue));


            ddlNsicDistrict.DataSource = Dt;
            ddlNsicDistrict.DataTextField = Dt.Columns["fGPtbl_MB_Name"].ToString();
            ddlNsicDistrict.DataValueField = Dt.Columns["fGPtbl_MB_Id"].ToString();
            ddlNsicDistrict.DataBind();
            ddlNsicDistrict.Items.Insert(0, new ListItem("--Select Branch--", "0"));

        }
        //else
        //{
        //    grdFactories.DataSource = null;
        //    grdFactories.DataBind();
        //}
    }
    public DataTable Company_Fetch_ZoneId_By_StateId(int StateId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_ZoneId_By_StateId_POI", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_MS_Id", SqlDbType.Int).Value = StateId;


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

    private void ListtoXml(int POI_Id)
    {
        List<Director> mydir = (List<Director>)ViewState["director"];
        if (mydir != null)
        {
            var xDir = new XElement("Directors", from dir in mydir
                                                 select new XElement("Director",

                                                     //new XAttribute("ID", dir.rowno),

                                                     new XElement("StateId", dir.stateid),
                                                     new XElement("CityId", dir.cityId)));



            Company_Insert_Step_Factory_Details_POI(POI_Id, xDir.ToString());
        }

    }

    public int Company_Insert_Step_Factory_Details_POI(int fGPtbl_POI_Id, string xmldata)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step_Factory_Details_POI", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_POI_Id", SqlDbType.Int).Value = fGPtbl_POI_Id;
            //xmldata = "<Directors><Director> <StateId> 20 </StateId> <CityId>491 </CityId></Director><Director><StateId>2</StateId>< CityId>87</CityId></ Director></Directors>";
            MySqlComm.Parameters.Add("@Xmldata", SqlDbType.VarChar).Value = xmldata;

            //SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            //outError.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
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




    public int Company_Insert_Step_POI(string fGPtbl_Company_Name, string PAN, int NatureofOperation, string UAM, string APersonName, int Constitution,
        int SocialCategory, string GSTNumber, string Email, string Mobile, int StateId, int BranchId, string ProductDetails, string EM2, int InspectionAgency
       , int POI_CameFrom)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Insert_Factory_POI", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;


            //List<Director> mydir = (List<Director>)ViewState["director"];
            //if (mydir != null)
            //{
            //    var xDir = new XElement("Directors", from dir in mydir
            //                                         select new XElement("Director", new XAttribute("ID", dir.rowno),

            //                                             new XElement("StateId", dir.stateid),
            //                                             new XElement("CityId", dir.cityId)));
            //}


            MySqlComm.Parameters.Add("@fGPtbl_Company_Name", SqlDbType.VarChar, 5000).Value = fGPtbl_Company_Name;
            MySqlComm.Parameters.Add("@PAN", SqlDbType.VarChar, 15).Value = PAN;
            MySqlComm.Parameters.Add("@NatureofOperation", SqlDbType.Int).Value = NatureofOperation;
            MySqlComm.Parameters.Add("@UAM", SqlDbType.VarChar, 15).Value = UAM;
            MySqlComm.Parameters.Add("@APersonName", SqlDbType.VarChar, 500).Value = APersonName;
            MySqlComm.Parameters.Add("@Constitution", SqlDbType.Int).Value = Constitution;
            MySqlComm.Parameters.Add("@SocialCategory", SqlDbType.Int).Value = SocialCategory;
            MySqlComm.Parameters.Add("@GSTNumber", SqlDbType.VarChar, 50).Value = GSTNumber;
            MySqlComm.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = Email;
            MySqlComm.Parameters.Add("@Mobile", SqlDbType.VarChar, 15).Value = Mobile;
            MySqlComm.Parameters.Add("@StateId", SqlDbType.Int).Value = StateId;
            MySqlComm.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            // int camefrom = String.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : Convert.ToInt32(Request.QueryString["ID"]);
            MySqlComm.Parameters.Add("@fGPtbl_POI_CameFrom", SqlDbType.Int).Value = POI_CameFrom;
            MySqlComm.Parameters.Add("@fGPtbl_POI_ProductDetails", SqlDbType.VarChar, 500).Value = ProductDetails;


            MySqlComm.Parameters.Add("@InspectionAgency", SqlDbType.Int).Value = InspectionAgency;
            MySqlComm.Parameters.Add("@EM2", SqlDbType.VarChar, 100).Value = EM2;

            //  MySqlComm.Parameters.Add("@Xmldata", SqlDbType.VarChar, 8000).Value = xmldata;

            SqlParameter fGPtbl_POI_Id = new SqlParameter("@Id", SqlDbType.Int);
            fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(fGPtbl_POI_Id);

            MySqlComm.ExecuteNonQuery();
            Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
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

        return Primary_Id;
    }

    void Clear()
    {
        txtAuthorizedName.Text = "";
        txtCompanyName.Text = "";
        txtEmail.Text = "";
        txtGST.Text = "";
        txtMobile.Text = "";
        txtPan.Text = "";
        txtProduct.Text = "";
        txtUAM.Text = "";
        ddlCategory.SelectedIndex = -1;
        ddlConstitution.SelectedIndex = -1;
        ddlNsicState.SelectedIndex = -1;
        ddlNsicDistrict.SelectedIndex = -1;
        ddlOperation.SelectedIndex = -1;

        //gv_director.DataSource = null;
        //gv_director.DataBind();

        grdFactories.DataSource = null;
        grdFactories.DataBind();

    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        int camefrom = String.IsNullOrEmpty(Request.QueryString["ID"]) ? 0 : Convert.ToInt32(Request.QueryString["ID"]);
        int POI_Id = Company_Insert_Step_POI(txtCompanyName.Text, txtPan.Text, Convert.ToInt32(ddlOperation.SelectedValue), txtUAM.Text, txtAuthorizedName.Text, Convert.ToInt32(ddlConstitution.SelectedValue), Convert.ToInt32(ddlCategory.SelectedValue), txtGST.Text, txtEmail.Text, txtMobile.Text, Convert.ToInt32(ddlNsicState.SelectedValue), Convert.ToInt32(ddlNsicDistrict.SelectedValue), txtProduct.Text, txtEM2.Text, Convert.ToInt32(ddlInspectionAgency.SelectedValue), camefrom);
        //ListtoXml(POI_Id);
        if (POI_Id > 0)
        {
            SendEmail();
            ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data has been added successfully.');", true);
            Clear();
        }
        {
            ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data already exists with the given PAN Number or there may be some error.');", true);
        }



        //Response.Redirect("PointOfInterest.aspx");

    }
    public void SendEmail()
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();

        DataSet MyDsBranch = new DataSet();

        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(17);

        MyDsBranch = objMailer.MasterMailer_Fetch_DataSet(19);

        string CompName = string.Empty, BrName = string.Empty, BrAddress = string.Empty, BrPhone = string.Empty, BrEmail = string.Empty;


        string AuthPName = string.Empty, AuthPMobile = string.Empty, AuthPEmail = string.Empty, ProductDetails = string.Empty;


        //MyDt = new DataTable();
        //MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_OneMonth_Mailar", DBManager.ConnectToDB());
        //MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
        //MyAdp.SelectCommand.CommandTimeout = 50000;
        //MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
        //MyAdp.SelectCommand.Parameters.Add("@Days", SqlDbType.Int).Value = Days;
        //MyAdp.Fill(MyDt);



        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();



        strOriginalMailBodyBranch = MyDsBranch.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        //MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubjectBranch = MyDsBranch.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();

        System.Data.DataTable MyDt = new System.Data.DataTable();
        MyDt = Company_Fetch_BranchAddress(Convert.ToInt32(ddlNsicDistrict.SelectedValue));


        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(txtEmail.Text);
                //BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                CompName = Convert.ToString(txtCompanyName.Text);

                BrName = Convert.ToString(drIA["fGPtbl_MB_Name"]);
                BrAddress = Convert.ToString(drIA["fGtbl_MB_Address"]);
                BrPhone = Convert.ToString(drIA["fGtbl_MB_Phone"]);
                BrEmail = Convert.ToString(drIA["fGtbl_MB_Email"]);

                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$CompanyName$$", CompName);
                strMailBody = strMailBody.Replace("$$Name$$", BrName);
                strMailBody = strMailBody.Replace("$$Address$$", BrAddress);
                strMailBody = strMailBody.Replace("$$Phone$$", BrPhone);
                strMailBody = strMailBody.Replace("$$Email$$", BrEmail);
                strMailBody = strMailBody.Replace("$$BranchName$$", BrName);


                strMailBodyBranch = strOriginalMailBodyBranch.Replace("$$BranchName$$", BrName);
                strMailBodyBranch = strMailBodyBranch.Replace("$$COMPANY_NAME$$", CompName);

                AuthPName = Convert.ToString(txtAuthorizedName.Text);
                AuthPMobile = Convert.ToString(txtMobile.Text);
                AuthPEmail = Convert.ToString(txtEmail.Text);
                ProductDetails = Convert.ToString(txtProduct.Text);

                strMailBodyBranch = strMailBodyBranch.Replace("$$AutorizedName$$", AuthPName);
                strMailBodyBranch = strMailBodyBranch.Replace("$$Mobile$$", AuthPMobile);
                strMailBodyBranch = strMailBodyBranch.Replace("$$Email$$", AuthPEmail);
                strMailBodyBranch = strMailBodyBranch.Replace("$$ProductDetails$$", ProductDetails);

                // BrEmail = "itsupport2@nsic.co.in";
                if (MailTo != "")
                {
                    SendMailToBranch(MailTo, strMailBody, MailFrom, MailSubject);

                    SendMailToBranch(BrEmail, strMailBodyBranch, MailFrom, MailSubjectBranch);


                    //INSERT INTO SENT MAILER
                    // clsDDL.Insert_MailDetails(MailTo, UserId, 1, MailFrom, 1, 1, MailSubject, strMailBody, 1);

                    // clsDDL.Insert_MailDetails(BrEmail, UserId, 1, MailFrom, 1, 1, MailSubjectBranch, strMailBodyBranch, 1);

                }
            }//foreach
        }//if

    }

    public static string SendMailToBranch(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];

            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailfrom, "Support-SinglePointRegistration");
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));
            // message.Bcc.Add(new MailAddress("ctms@nsic.co.in"));
           // message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));
            //'Isha Singh' <>

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;25;587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            return "1";

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
            return ex.Message;
        }
    }



    protected void btnf_Click(object sender, EventArgs e)
    {
        if (txtUAM.Text != "" && txtPan.Text != "")
        {
            ddlOperation.SelectedIndex = -1;
            ddlCategory.SelectedIndex = -1;
            ddlConstitution.SelectedIndex = -1;

            string xmltext = ConfigurationManager.AppSettings["ServerSettings"] + txtUAM.Text + "," + txtPan.Text + "," + ConfigurationManager.AppSettings["SPRSKey"];
            XmlDocument objxml = new XmlDocument();
            objxml.Load(xmltext);

            string sCompleteval = objxml.OuterXml;


            XmlNodeList xnList = objxml.SelectNodes("//MSMEDataBank");
            foreach (XmlNode xn in xnList)
            {

                if (xn["UAN"].InnerText.Equals("NO"))
                {
                    lblMsg.Text = "This Unit is not registered with MSMEDATABANK";

                    txtEmail.Text = "";
                    txtMobile.Text = "";
                    txtProduct.Text = "";
                    ddlOperation.SelectedIndex = -1;
                    ddlCategory.SelectedIndex = -1;
                    txtCompanyName.Text = "";
                    txtAuthorizedName.Text = "";
                    break;
                }
                lblMsg.Text = "";

                txtCompanyName.Text = xn["EnterpriseName"].InnerText;
                string strOperation = "";
                strOperation = xn["MajorActivity"].InnerText;

                //strOperation = "2";
                if (strOperation.Trim().ToUpper() == "Manufacturing".ToUpper())
                {
                    strOperation = "1";
                }
                else if (strOperation.Trim().ToUpper() == "Services".ToUpper())
                {
                    strOperation = "2";
                }
                ddlOperation.Items.FindByValue(strOperation).Selected = true;

                txtAuthorizedName.Text = xn["AuthorisedPersonName"].InnerText;

                string SocialCaterogy = "";
                SocialCaterogy = xn["SocialCaterogy"].InnerText;
                if (SocialCaterogy.Trim().ToUpper() == "GENERAL".ToUpper())
                {
                    SocialCaterogy = "1";
                }
                else if (SocialCaterogy.Trim().ToUpper() == "OBC".ToUpper())
                {
                    SocialCaterogy = "5";
                }
                else if (SocialCaterogy.Trim().ToUpper() == "SC".ToUpper())
                {
                    SocialCaterogy = "10";
                }
                else if (SocialCaterogy.Trim().ToUpper() == "ST".ToUpper())
                {
                    SocialCaterogy = "11";
                }

                ddlCategory.Items.FindByValue(SocialCaterogy).Selected = true;

                txtEmail.Text = xn["Email"].InnerText;
                txtMobile.Text = xn["Mobile"].InnerText;
                txtProduct.Text = xn["ProductDesc"].InnerText;


                string OrganisationType = "";
                OrganisationType = xn["OrganisationType"].InnerText;
                if (OrganisationType.Trim().ToUpper() == "Proprietary".ToUpper())
                {
                    OrganisationType = "4";
                }
                else if (OrganisationType.Trim().ToUpper() == "Limited Liability Partnership".ToUpper().Trim())
                {
                    OrganisationType = "10";
                }
                else if (OrganisationType.Trim().ToUpper() == "Partnership".ToUpper())
                {
                    OrganisationType = "5";
                }
                else if (OrganisationType.Trim().ToUpper() == "Public Limited Company".ToUpper().Trim())
                {
                    OrganisationType = "6";
                }
                else if (OrganisationType.Trim().ToUpper() == "Private Limited Company".ToUpper().Trim())
                {
                    OrganisationType = "7";
                }

                else if (OrganisationType.Trim().ToUpper() == "Hindu Undivided Family".ToUpper().Trim())
                {
                    OrganisationType = "9";
                }

                ddlConstitution.Items.FindByValue(OrganisationType).Selected = true;

            }
        }

    }
}


//<GetXMLResponse xmlns = "http://tempuri.org/" >< GetXMLResult >< MSMEDataBank xmlns=""><UAN>AP03A0001023</UAN><EnterpriseName>M/s Naga Lakshmi Coir Rope Industry</EnterpriseName><AadhaarNo>311454039657</AadhaarNo><UnitCategory>Micro</UnitCategory><OrganisationType>Proprietary</OrganisationType><NatureOfOperation>Perennial</NatureOfOperation><State>ANDHRA PRADESH</State><District>EAST GODAVARI</District><Pincode>533247</Pincode><Mobile>9948263250</Mobile><Email>marlapudibaby @gmail.com</Email><AuthorisedPersonName>MARLAPUDI BABY</AuthorisedPersonName><CommunicationAddress>R S 121 8A, Pasarlapudi</CommunicationAddress><SocialCaterogy>SC</SocialCaterogy><CommencementDate>2015-11-16</CommencementDate><MajorActivity>Manufacturing</MajorActivity><PwDEntrepreneur>False</PwDEntrepreneur><WomanEntrepreneur>False</WomanEntrepreneur><Minority>False</Minority><Ex-Serviceman>False</Ex-Serviceman><ProductDesc>13943-Manufacture of cordage or rope made of coir</ProductDesc><Factories><Factory><Address>R S 121 8A Pasarlapudi</Address><State>ANDHRA PRADESH</State><District>EAST GODAVARI</District><Pincode>533247</Pincode></Factory></Factories></MSMEDataBank></GetXMLResult></GetXMLResponse>



