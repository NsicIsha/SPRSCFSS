using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using RestSharp;
 
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Net;
using System.Xml.Linq;
public partial class appAdministrator_NewUnitRegistrationStep2Online : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit, ConstitutionId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    string prevPage = String.Empty;
    string pagename;
    short tabindx = 1;
    int Constitution = 0;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    DataTable MyDtt;
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


        if (!Page.IsPostBack)
        {
            if (Session["step2"] != null && Session["step2"].ToString() != "")
            {

                hid_ppage.Value = Session["step2"].ToString();
            }
            else
            {
                string purl = Request.UrlReferrer.AbsolutePath.ToString();
                string[] strarry = purl.Split('/');
                int lengh = strarry.Length;
                pagename = strarry[lengh - 1];
                hid_ppage.Value = pagename;
                Session["step2"] = pagename;
            }


           

        }

        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //if (!Page.IsPostBack)
        //{ 
        // btnSubmit.Attributes.Add(" getGrRow
        //}

        FunRequest();
        FunFillControls();
    }

    public DataTable Company_Fetch_Constitution(int Companyid)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("Gp_sp_ConstitutionId_By_CompanyID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@companyid", SqlDbType.Int).Value = Companyid;
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

            ConstitutionId = Convert.ToInt32((objDecEnc.Decrypt(Request.QueryString["con"].Replace(" ", "+")).ToString()));

           

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


        //ConstitutionId = Convert.ToInt32(Request.QueryString["con"].Replace(" ", "+"));

        

        



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
        //txt_3ca1Name.TabIndex = (short)(tabindx + 4);
        //txt_3ca1Address.TabIndex = (short)(tabindx + 5);
        //txt_3ca1Pin.TabIndex = (short)(tabindx + 6);
        //ddlState_3ca1.TabIndex = (short)(tabindx + 7);
        //ddlCity_3ca1.TabIndex = (short)(tabindx + 8);
        //txt_3ca1Tele.TabIndex = (short)(tabindx + 9);
        //txt_3ca2Name.TabIndex = (short)(tabindx + 10);
        //txt_3ca2Address.TabIndex = (short)(tabindx + 11);
        //txt_3ca2Pin.TabIndex = (short)(tabindx + 12);
        //ddlState_3ca2.TabIndex = (short)(tabindx + 13);
        //ddlCity_3ca2.TabIndex = (short)(tabindx + 14);
        //txt_3ca2Tele.TabIndex = (short)(tabindx + 15);
        //txt_3ca3Name.TabIndex = (short)(tabindx + 16);
        //txt_3ca3Address.TabIndex = (short)(tabindx + 17);
        //txt_3ca3Pin.TabIndex = (short)(tabindx + 18);
        //ddlState_3ca3.TabIndex = (short)(tabindx + 19);
        //ddlCity_3ca3.TabIndex = (short)(tabindx + 20);
        //txt_3ca3Tele.TabIndex = (short)(tabindx + 21);
        //txt_3ca4Name.TabIndex = (short)(tabindx + 22);
        //txt_3ca4Address.TabIndex = (short)(tabindx + 23);
        //txt_3ca4Pin.TabIndex = (short)(tabindx + 24);
        //ddlState_3ca4.TabIndex = (short)(tabindx + 25);
        //ddlCity_3ca4.TabIndex = (short)(tabindx + 26);
        //txt_3ca4Tele.TabIndex = (short)(tabindx + 27);
        //txt_3ca5Name.TabIndex = (short)(tabindx + 28);
        //txt_3ca5Address.TabIndex = (short)(tabindx + 29);
        //txt_3ca5Pin.TabIndex = (short)(tabindx + 30);
        //ddlState_3ca5.TabIndex = (short)(tabindx + 31);
        //ddlCity_3ca5.TabIndex = (short)(tabindx + 32);
        //txt_3ca5Tele.TabIndex = (short)(tabindx + 33);
        txt_3cb.TabIndex = (short)(tabindx + 34);
        btnSubmit.TabIndex = (short)(tabindx + 35);


    }

    private void FunFillData()
    {

        //cls_director objdir1 = new cls_director();
        //List<PerformanceSheet> director1 = new List<PerformanceSheet>();
        //director1 = objdir1.FetchPerformanceSheet(CompanyId);

        //Bind_PerformanceSheet_gridview(director1);
       

        if (Status == 5 || Status == 123 || Edit == 1)    //INCOMPLETE
        {
            objCompany = new clsCompany();

            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt = objCompany.Company_FetchDirectorList(CompanyId);

            //if (dt != null)
            //{
            //    if (dt.Rows.Count > 0)
            //    {
            //        gv_director.DataSource = dt;
            //        gv_director.DataBind();
            //    }
            //}


            




            cls_director objdir = new cls_director();
            List<Director> director = new List<Director>();
            director = objdir.FetchCompanyDirector(CompanyId);

            Bind_Director_gridview(director);



            ///////// Auto select Constitution
            DataTable Dtt = new DataTable();
            Dtt = Company_Fetch_Constitution(CompanyId);
            int ConstitutionIddd = 0;
            ConstitutionIddd = Convert.ToInt32(Dtt.Rows[0]["fGPtbl_ConstitutionId"].ToString());
            rd3c.Items.FindByValue(ConstitutionIddd.ToString()).Selected = true;

            ////////////////////////





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

            //DIRECTOR TABLE
            if (fCnt != 0)
            {
                objCompany = new clsCompany();




                System.Data.SqlClient.SqlDataReader MyDr1;
                MyDr1 = objCompany.Company_FetchDirectorByUnit_Step2(CompanyId);
                int fCnt2 = 0;

                while (MyDr1.Read())
                {

                    fCnt2 += 1;
                    if (fCnt2 == 1)
                    {
                        //DIRECTORS - 1
                        //txt_3ca1Name.Text = MyDr1["fGPtbl_Company_Directors_Name"].ToString();
                        //txt_3ca1Address.Text = MyDr1["fGPtbl_Company_Directors_Address"].ToString();
                        //txt_3ca1Pin.Text = MyDr1["fGPtbl_Company_Directors_Zip"].ToString();
                        //txt_3ca1Tele.Text = MyDr1["fGPtbl_Company_Directors_Phone"].ToString();
                        //txt_3ca1share.Text = MyDr1["fGPtbl_Company_Directors_Shareholder"].ToString();
                        try
                        {
                            //ddlState_3ca1.SelectedValue = MyDr1["fGPtbl_Company_Directors_StateId"].ToString();

                            //funfill_ddlCity_3ca1(Convert.ToInt32(MyDr1["fGPtbl_Company_Directors_StateId"].ToString()));
                            //ddlCity_3ca1.SelectedValue = MyDr1["fGPtbl_Company_Directors_CityId"].ToString();
                        }
                        catch { }
                    }
                    else if (fCnt2 == 2)
                    {
                        //DIRECTORS - 2
                        //txt_3ca2Name.Text = MyDr1["fGPtbl_Company_Directors_Name"].ToString();
                        //txt_3ca2Address.Text = MyDr1["fGPtbl_Company_Directors_Address"].ToString();
                        //txt_3ca2Pin.Text = MyDr1["fGPtbl_Company_Directors_Zip"].ToString();
                        //txt_3ca2Tele.Text = MyDr1["fGPtbl_Company_Directors_Phone"].ToString();
                        //txt_3ca2share.Text = MyDr1["fGPtbl_Company_Directors_Shareholder"].ToString();
                        try
                        {
                            //{
                            //    ddlState_3ca2.SelectedValue = MyDr1["fGPtbl_Company_Directors_StateId"].ToString();
                            //    funfill_ddlCity_3ca2(Convert.ToInt32(MyDr1["fGPtbl_Company_Directors_StateId"].ToString()));
                            //    ddlCity_3ca2.SelectedValue = MyDr1["fGPtbl_Company_Directors_CityId"].ToString();
                        }
                        catch { }
                    }
                    else if (fCnt2 == 3)
                    {
                        //DIRECTORS - 3
                        //txt_3ca3Name.Text = MyDr1["fGPtbl_Company_Directors_Name"].ToString();
                        //txt_3ca3Address.Text = MyDr1["fGPtbl_Company_Directors_Address"].ToString();
                        //txt_3ca3Pin.Text = MyDr1["fGPtbl_Company_Directors_Zip"].ToString();
                        //txt_3ca3Tele.Text = MyDr1["fGPtbl_Company_Directors_Phone"].ToString();
                        //txt_3ca3share.Text = MyDr1["fGPtbl_Company_Directors_Shareholder"].ToString();
                        try
                        {
                            //ddlState_3ca3.SelectedValue = MyDr1["fGPtbl_Company_Directors_StateId"].ToString();
                            //funfill_ddlCity_3ca3(Convert.ToInt32(MyDr1["fGPtbl_Company_Directors_StateId"].ToString()));
                            //ddlCity_3ca3.SelectedValue = MyDr1["fGPtbl_Company_Directors_CityId"].ToString();
                        }
                        catch { }
                    }
                    else if (fCnt2 == 4)
                    {
                        //DIRECTORS - 4
                        //txt_3ca4Name.Text = MyDr1["fGPtbl_Company_Directors_Name"].ToString();
                        //txt_3ca4Address.Text = MyDr1["fGPtbl_Company_Directors_Address"].ToString();
                        //txt_3ca4Pin.Text = MyDr1["fGPtbl_Company_Directors_Zip"].ToString();
                        //txt_3ca4Tele.Text = MyDr1["fGPtbl_Company_Directors_Phone"].ToString();
                        //txt_3ca4share.Text = MyDr1["fGPtbl_Company_Directors_Shareholder"].ToString();
                        try
                        {
                            //ddlState_3ca4.SelectedValue = MyDr1["fGPtbl_Company_Directors_StateId"].ToString();
                            //funfill_ddlCity_3ca4(Convert.ToInt32(MyDr1["fGPtbl_Company_Directors_StateId"].ToString()));
                            //ddlCity_3ca4.SelectedValue = MyDr1["fGPtbl_Company_Directors_CityId"].ToString();
                        }
                        catch { }
                    }
                    else if (fCnt2 == 5)
                    {
                        //DIRECTORS - 5
                        //txt_3ca5Name.Text = MyDr1["fGPtbl_Company_Directors_Name"].ToString();
                        //txt_3ca5Address.Text = MyDr1["fGPtbl_Company_Directors_Address"].ToString();
                        //txt_3ca5Pin.Text = MyDr1["fGPtbl_Company_Directors_Zip"].ToString();
                        //txt_3ca5Tele.Text = MyDr1["fGPtbl_Company_Directors_Phone"].ToString();
                        //txt_3ca5share.Text = MyDr1["fGPtbl_Company_Directors_Shareholder"].ToString();
                        try
                        {
                            //ddlState_3ca5.SelectedValue = MyDr1["fGPtbl_Company_Directors_StateId"].ToString();
                            //funfill_ddlCity_3ca5(Convert.ToInt32(MyDr1["fGPtbl_Company_Directors_StateId"].ToString()));
                            //ddlCity_3ca5.SelectedValue = MyDr1["fGPtbl_Company_Directors_CityId"].ToString();
                        }
                        catch { }
                    }
                }

                MyDr1.Close();

            }


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

    //protected void ddlState_3ca1_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int fStateId = 0;
    //    try
    //    {
    //        fStateId = Convert.ToInt32(ddlState_3ca1.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        fStateId = 0;
    //    }
    //    funfill_ddlCity_3ca1(fStateId);
    //}

    //private void funfill_ddlCity_3ca1(int fStateId)
    //{
    //    //CITY DROPDOWNLIST
    //    if (fStateId != 0)
    //    {
    //        System.Data.DataTable dt_c = new System.Data.DataTable();
    //        dt_c = clsDDL.ddlCity_Activated(fStateId);
    //        ddlCity_3ca1.DataSource = dt_c;
    //        ddlCity_3ca1.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    //        ddlCity_3ca1.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    //        ddlCity_3ca1.DataBind();
    //        ddlCity_3ca1.Items.Insert(0, new ListItem("--Select City--", ""));
    //        dt_c.Clear();
    //        dt_c.Dispose();
    //    }
    //    else
    //    {
    //        ddlCity_3ca1.Items.Clear();
    //    }
    //}

    //protected void ddlState_3ca2_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int fStateId = 0;
    //    try
    //    {
    //        fStateId = Convert.ToInt32(ddlState_3ca2.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        fStateId = 0;
    //    }
    //    funfill_ddlCity_3ca2(fStateId);
    //}

    //private void funfill_ddlCity_3ca2(int fStateId)
    //{
    //    //CITY DROPDOWNLIST
    //    if (fStateId != 0)
    //    {
    //        System.Data.DataTable dt_c = new System.Data.DataTable();
    //        dt_c = clsDDL.ddlCity_Activated(fStateId);
    //        ddlCity_3ca2.DataSource = dt_c;
    //        ddlCity_3ca2.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    //        ddlCity_3ca2.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    //        ddlCity_3ca2.DataBind();
    //        ddlCity_3ca2.Items.Insert(0, new ListItem("--Select City--", ""));
    //        dt_c.Clear();
    //        dt_c.Dispose();
    //    }
    //    else
    //    {
    //        ddlCity_3ca2.Items.Clear();
    //    }
    //}

    //protected void ddlState_3ca3_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int fStateId = 0;
    //    try
    //    {
    //        fStateId = Convert.ToInt32(ddlState_3ca3.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        fStateId = 0;
    //    }
    //    funfill_ddlCity_3ca3(fStateId);
    //}

    //private void funfill_ddlCity_3ca3(int fStateId)
    //{
    //    //CITY DROPDOWNLIST
    //    if (fStateId != 0)
    //    {
    //        System.Data.DataTable dt_c = new System.Data.DataTable();
    //        dt_c = clsDDL.ddlCity_Activated(fStateId);
    //        ddlCity_3ca3.DataSource = dt_c;
    //        ddlCity_3ca3.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    //        ddlCity_3ca3.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    //        ddlCity_3ca3.DataBind();
    //        ddlCity_3ca3.Items.Insert(0, new ListItem("--Select City--", ""));
    //        dt_c.Clear();
    //        dt_c.Dispose();
    //    }
    //    else
    //    {
    //        ddlCity_3ca3.Items.Clear();
    //    }
    //}

    //protected void ddlState_3ca4_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int fStateId = 0;
    //    try
    //    {
    //        fStateId = Convert.ToInt32(ddlState_3ca4.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        fStateId = 0;
    //    }
    //    funfill_ddlCity_3ca4(fStateId);
    //}

    //private void funfill_ddlCity_3ca4(int fStateId)
    //{
    //    //CITY DROPDOWNLIST
    //    if (fStateId != 0)
    //    {
    //        System.Data.DataTable dt_c = new System.Data.DataTable();
    //        dt_c = clsDDL.ddlCity_Activated(fStateId);
    //        ddlCity_3ca4.DataSource = dt_c;
    //        ddlCity_3ca4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    //        ddlCity_3ca4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    //        ddlCity_3ca4.DataBind();
    //        ddlCity_3ca4.Items.Insert(0, new ListItem("--Select City--", ""));
    //        dt_c.Clear();
    //        dt_c.Dispose();
    //    }
    //    else
    //    {
    //        ddlCity_3ca4.Items.Clear();
    //    }
    //}

    //protected void ddlState_3ca5_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    int fStateId = 0;
    //    try
    //    {
    //        fStateId = Convert.ToInt32(ddlState_3ca5.SelectedValue.ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        fStateId = 0;
    //    }
    //    funfill_ddlCity_3ca5(fStateId);
    //}

    //private void funfill_ddlCity_3ca5(int fStateId)
    //{
    //    //CITY DROPDOWNLIST
    //    if (fStateId != 0)
    //    {
    //        System.Data.DataTable dt_c = new System.Data.DataTable();
    //        dt_c = clsDDL.ddlCity_Activated(fStateId);
    //        ddlCity_3ca5.DataSource = dt_c;
    //        ddlCity_3ca5.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString(); ;
    //        ddlCity_3ca5.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
    //        ddlCity_3ca5.DataBind();
    //        ddlCity_3ca5.Items.Insert(0, new ListItem("--Select City--", ""));
    //        dt_c.Clear();
    //        dt_c.Dispose();
    //    }
    //    else
    //    {
    //        ddlCity_3ca5.Items.Clear();
    //    }
    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
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
         //int Constitution = 0;
        try
        {
            Constitution = Convert.ToInt32(rd3c.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        ////DIRECTORS - 1
        //string DirectorName_1=txt_3ca1Name.Text.ToString().Trim();
        //string DirectorAddress_1 = txt_3ca1Address.Text.ToString().Trim();
        //string DirectorPin_1 = txt_3ca1Pin.Text.ToString().Trim();
        //string DirectorTele_1 = txt_3ca1Tele.Text.ToString().Trim();
        //string DirectorShare_1 = txt_3ca1share.Text.Trim();
        //int DirectorState_1 = 0;
        //int DirectorCity_1 = 0;
        //try
        //{
        //    DirectorState_1 = Convert.ToInt32(ddlState_3ca1.SelectedValue.ToString());
        //    DirectorCity_1 = Convert.ToInt32(ddlCity_3ca1.SelectedValue.ToString());
        //}
        //catch { }

        ////DIRECTORS - 2
        //string DirectorName_2 = txt_3ca2Name.Text.ToString().Trim();
        //string DirectorAddress_2 = txt_3ca2Address.Text.ToString().Trim();
        //string DirectorPin_2 = txt_3ca2Pin.Text.ToString().Trim();
        //string DirectorTele_2 = txt_3ca2Tele.Text.ToString().Trim();
        //string DirectorShare_2 = txt_3ca2share.Text.Trim();
        //int DirectorState_2 = 0;
        //int DirectorCity_2 = 0;
        //try
        //{
        //    DirectorState_2 = Convert.ToInt32(ddlState_3ca2.SelectedValue.ToString());
        //    DirectorCity_2 = Convert.ToInt32(ddlCity_3ca2.SelectedValue.ToString());
        //}
        //catch { }

        ////DIRECTORS - 3
        //string DirectorName_3 = txt_3ca3Name.Text.ToString().Trim();
        //string DirectorAddress_3 = txt_3ca3Address.Text.ToString().Trim();
        //string DirectorPin_3 = txt_3ca3Pin.Text.ToString().Trim();
        //string DirectorTele_3 = txt_3ca3Tele.Text.ToString().Trim();
        //string DirectorShare_3 = txt_3ca3share.Text.Trim();
        //int DirectorState_3 = 0;
        //int DirectorCity_3 = 0;
        //try
        //{
        //    DirectorState_3 = Convert.ToInt32(ddlState_3ca3.SelectedValue.ToString());
        //    DirectorCity_3 = Convert.ToInt32(ddlCity_3ca3.SelectedValue.ToString());
        //}
        //catch { }

        ////DIRECTORS - 4
        //string DirectorName_4=string.Empty;
        //if (txt_3ca4Name.Text.ToString() == "")
        //{
        //}
        //else
        //{
        //    DirectorName_4 = txt_3ca4Name.Text.ToString().Trim();
        //}
        //string DirectorAddress_4 = string.Empty;
        //if (txt_3ca4Address.Text.ToString() == "")
        //{
        //}
        //else
        //{
        //    DirectorAddress_4 = txt_3ca4Address.Text.ToString().Trim();
        //}
        //string DirectorPin_4 = string.Empty;
        //if (txt_3ca4Pin.Text.ToString() == "")
        //{

        //}
        //else
        //{
        //    DirectorPin_4 = txt_3ca4Pin.Text.ToString().Trim();
        //}
        //string DirectorTele_4 = string.Empty;
        //if (txt_3ca4Pin.Text.ToString() == "")
        //{
        //}
        //else
        //{

        //  DirectorTele_4 = txt_3ca4Tele.Text.ToString().Trim();
        //}

        //string DirectorShare_4=string.Empty;
        //if (txt_3ca4share.Text.Trim() == "")
        //{
        //}
        //else
        //{
        //    DirectorShare_4 = txt_3ca4share.Text.Trim();
        //}


        //int DirectorState_4 = 0;
        //int DirectorCity_4 = 0;
        //try
        //{
        //    DirectorState_4 = Convert.ToInt32(ddlState_3ca4.SelectedValue.ToString());
        //    DirectorCity_4 = Convert.ToInt32(ddlCity_3ca4.SelectedValue.ToString());
        //}
        //catch { }

        ////DIRECTORS - 5
        //string DirectorName_5 = txt_3ca5Name.Text.ToString().Trim();
        //string DirectorAddress_5 = txt_3ca5Address.Text.ToString().Trim();
        //string DirectorPin_5 = txt_3ca5Pin.Text.ToString().Trim();
        //string DirectorTele_5 = txt_3ca5Tele.Text.ToString().Trim();
        //string DirectorShare_5 = txt_3ca5share.Text.Trim();
        //int DirectorState_5 = 0;
        //int DirectorCity_5 = 0;
        //try
        //{
        //    DirectorState_5 = Convert.ToInt32(ddlState_3ca5.SelectedValue.ToString());
        //    DirectorCity_5 = Convert.ToInt32(ddlCity_3ca5.SelectedValue.ToString());
        //}
        //catch { }

        int i, j;
        i = 0;
        j = grdSSI.Rows.Count;

        while (i < j)
        {

            TextBox txtSSINo = (TextBox)grdSSI.Rows[i].FindControl("txt_21");
            TextBox txtSSIRegDate = (TextBox)grdSSI.Rows[i].FindControl("txt_22");
            HiddenField hdnunt = (HiddenField)grdSSI.Rows[i].FindControl("hdnunit");
            string SSIRegistrationNo = "";// txtSSINo.Text.Trim();
            string SSIRegistrationdate = "01/01/1990";// txtSSIRegDate.Text.Trim();
            UnitId = Convert.ToInt32(hdnunt.Value.ToString());

            objCompany = new clsCompany();
            ErrorId = objCompany.Company_Insert_Step2(UnitId, CompanyId, SSIRegistrationNo,
                                                        SSIRegistrationdate, DateofIncorporation, DateofCommencementofProductionBusiness,
                                                        Constitution, threeC5, UserId);
            i++;
        }



        objCompany = new clsCompany();
        //ErrorId = objCompany.Company_InsertDirector_Step2 (CompanyId,
        //    DirectorName_1, DirectorAddress_1, DirectorPin_1, DirectorState_1, DirectorCity_1, DirectorTele_1, DirectorShare_1,
        //    DirectorName_2, DirectorAddress_2, DirectorPin_2, DirectorState_2, DirectorCity_2, DirectorTele_2, DirectorShare_2,
        //    DirectorName_3, DirectorAddress_3, DirectorPin_3, DirectorState_3, DirectorCity_3, DirectorTele_3, DirectorShare_3,
        //    DirectorName_4, DirectorAddress_4, DirectorPin_4, DirectorState_4, DirectorCity_4, DirectorTele_4, DirectorShare_4,
        //    DirectorName_5, DirectorAddress_5, DirectorPin_5, DirectorState_5, DirectorCity_5, DirectorTele_5, DirectorShare_5,
        //                                            UserId);


        ListtoXml();

        //ListtoXml_PerFormanceSheet();

        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptPrvpage = objDecEnc.Encrypt(prevPage.ToString());

            string EncryConstitutionId = objDecEnc.Encrypt(Constitution.ToString()); //+ "&con=" + EncryConstitutionId + //"&con=" + EncryConstitutionId +
            if (Edit == 1)
            {
                Response.Redirect("NewUnitRegistrationStep3Online.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&con=" + EncryConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPrvpage + "&Edit=1", false);
            }
            else
            {
                Response.Redirect("NewUnitRegistrationStep3Online.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&con=" + EncryConstitutionId +  "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPrvpage + "&Edit=0", false);
            }
        }
        else               //INSERT/UPDATE FAIL
        {

        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        string PanCardNewRegistration = objDecEnc.Encrypt(prevPage.ToString());
        string ConsId = objDecEnc.Encrypt(ConstitutionId.ToString());
        if (Edit == 1)
        {
            Response.Redirect("AdminEditRegistration.aspx?cid=" + CompanyId + "&back=" + "b", false);
        }
        else
        {
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            Response.Redirect("NewUnitRegistrationOnline_WithoutUAM.aspx?pcd=" + PanCardNewRegistration + "&cid=" + EncryptCompany + "&con=" + ConsId, false);            
        }
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnk_delete = (LinkButton)e.Row.FindControl("lnk_delete");
            try
            {
                if (hid_ppage.Value != null)
                {

                    if (hid_ppage.Value.ToUpper().ToString() == "ADMINEDITREGISTRATION.ASPX")
                    {
                        lnk_delete.Visible = true;
                    }
                    else
                    {
                        lnk_delete.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {

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
        string name, address, zipcode, state, city, phone, share, stateid, cityid, email, experiance, adhar;

        name = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directorname")).Text;
        address = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directoraddress")).Text;
        zipcode = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directorzipcode")).Text;
        state = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedItem.Text;
        stateid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_state")).SelectedValue;
        city = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedItem.Text;
        cityid = ((DropDownList)gv_director.FooterRow.FindControl("ddl_footer_city")).SelectedValue;
        phone = ((TextBox)gv_director.FooterRow.FindControl("txt_director_phone")).Text;
        share = ((TextBox)gv_director.FooterRow.FindControl("txt_director_share")).Text;
        email = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_directorEmail")).Text.Trim();
        experiance = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_director_exp")).Text.Trim();
        adhar = ((TextBox)gv_director.FooterRow.FindControl("txt_director_Adhar")).Text.Trim();

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
            dir.email = email;
            dir.adhar = adhar;
            dir.experience = Convert.ToInt16(experiance);
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
            dir.email = email;
            dir.adhar = adhar;
            dir.experience = experiance.Trim() != "" ? Convert.ToInt16(experiance) : 0;
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
        string name, address, pincode, state, stateid, city, cityid, telephone, share, email, adhar, experiance;

        name = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_directorname")).Text.Trim();
        address = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_address")).Text.Trim();
        pincode = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_pincode")).Text.Trim();
        state = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedItem.Text.Trim();
        stateid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_state")).SelectedValue.Trim();
        city = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedItem.Text.Trim();
        cityid = ((DropDownList)gv_director.Rows[e.RowIndex].FindControl("ddl_director_city")).SelectedValue.Trim();
        telephone = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_phone")).Text.Trim();
        share = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_share")).Text.Trim();
        email = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_directorEmail")).Text.Trim();
        experiance = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_exp")).Text.Trim();
        adhar = ((TextBox)gv_director.Rows[e.RowIndex].FindControl("txt_director_Adhar")).Text.Trim();



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
                item.email = email;
                item.experience = Convert.ToInt16(experiance);
                item.adhar = adhar;
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
                                                     new XElement("UserId", UserId),
             new XElement("Email", dir.email),
             new XElement("Experience", dir.experience),
             new XElement("Adhar", dir.adhar)));


            objCompany = new clsCompany();
            objCompany.Company_Director_insert(xDir.ToString(), CompanyId);
        }

    }
    protected void gv_director_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
        List<Director> mydir = (List<Director>)ViewState["director"];
        Director dir = mydir.Where(s => s.rowno == userid).Single();
        if (mydir != null)
        {
            mydir.Remove(dir);
        }
        Bind_Director_gridview(mydir);
    }


    //protected void AddnewPerformanceSheet(object sender, EventArgs e)
    //{
    //    string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


    //    ordernumber = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_OrderNumber")).Text;
    //    Date_of_ReceiptOrder = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Date_of_ReceiptOrder")).Text;
    //    Store_Produt_Service_Desc = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Store_Produt_Service_Desc")).Text;
    //    Quantity = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Quantity")).Text;
    //    Value = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Value")).Text;
    //    Name_Consignee = ((TextBox)gv_director.FooterRow.FindControl("txt_Name_Consignee")).Text;
    //    DeliveryDueDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DeliveryDueDate")).Text;
    //    MaterialDeliveryDate = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_MaterialDeliveryDate")).Text;
    //    DespatchingParticulars = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_DespatchingParticulars")).Text;
    //    Remarks = ((TextBox)gv_director.FooterRow.FindControl("txt_foot_Remarks")).Text.Trim();


    //    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

    //    if (mydir == null)
    //    {
    //        List<PerformanceSheet> mydir1 = new List<PerformanceSheet>();
    //        PerformanceSheet dir = new PerformanceSheet();
    //        dir.PrimaryId = 1;
    //        dir.OrderNumber = ordernumber;
    //        dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    //        dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    //        dir.Quantity = Quantity;
    //        dir.Value = Value;
    //        dir.Name_Consignee = Name_Consignee;
    //        dir.DeliveryDueDate = DeliveryDueDate;
    //        dir.MaterialDeliveryDate = MaterialDeliveryDate;
    //        dir.DespatchingParticulars = DespatchingParticulars;
    //        dir.Remarks = Remarks;

    //        mydir1.Add(dir);
    //        Bind_PerformanceSheet_gridview(mydir1);
    //    }
    //    else
    //    {
    //        PerformanceSheet dir = new PerformanceSheet();
    //        dir.PrimaryId = mydir.Count + 1;
    //        dir.OrderNumber = ordernumber;
    //        dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    //        dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    //        dir.Quantity = Quantity;
    //        dir.Value = Value;
    //        dir.Name_Consignee = Name_Consignee;
    //        dir.DeliveryDueDate = DeliveryDueDate;
    //        dir.MaterialDeliveryDate = MaterialDeliveryDate;
    //        dir.DespatchingParticulars = DespatchingParticulars;
    //        dir.Remarks = Remarks;
    //        mydir.Add(dir);
    //        Bind_PerformanceSheet_gridview(mydir);
    //    }

    //}

    //private void Bind_PerformanceSheet_gridview(List<PerformanceSheet> PerformanceSheet)
    //{

    //    gv_PerformanceSheet.DataSource = PerformanceSheet;
    //    gv_PerformanceSheet.DataBind();
    //    ViewState["PerformanceSheet"] = PerformanceSheet;

    //    if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")
    //    {
    //        gv_PerformanceSheet.Rows[0].Visible = false;

    //    }
    //}

    //protected void gv_PerformanceSheet_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gv_PerformanceSheet.EditIndex = e.NewEditIndex;
    //    List<PerformanceSheet> myPS = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    //    gv_PerformanceSheet.DataSource = myPS;
    //    gv_PerformanceSheet.DataBind();


    //}
    //protected void gv_PerformanceSheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    int userid = Convert.ToInt32(gv_PerformanceSheet.DataKeys[e.RowIndex].Value.ToString());

    //    // Response.Write(userid);
    //    // Fin Gridview Record 
    //    string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


    //    ordernumber = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_OrderNumber")).Text.Trim(); 
    //    Date_of_ReceiptOrder = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Date_of_ReceiptOrder")).Text.Trim(); 
    //    Store_Produt_Service_Desc = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Store_Produt_Service_Desc")).Text.Trim(); 
    //    Quantity = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Quantity")).Text.Trim();  
    //    Value = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Value")).Text.Trim();  
    //    Name_Consignee = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Name_Consignee")).Text.Trim(); 
    //    DeliveryDueDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DeliveryDueDate")).Text.Trim();  
    //    MaterialDeliveryDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_MaterialDeliveryDate")).Text.Trim();  
    //    DespatchingParticulars = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DespatchingParticulars")).Text.Trim(); 
    //    Remarks = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Remarks")).Text.Trim(); 



    //    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

    //    if (mydir != null)
    //    {
    //        var item = mydir.Where(d => d.PrimaryId == userid).FirstOrDefault();
    //        if (item != null)
    //        {
    //            item.OrderNumber = ordernumber;
    //            item.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
    //            item.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
    //            item.Quantity = Quantity;
    //            item.Value = Value;
    //            item.Name_Consignee = Name_Consignee;
    //            item.DeliveryDueDate = DeliveryDueDate;
    //            item.MaterialDeliveryDate = MaterialDeliveryDate;
    //            item.DespatchingParticulars = DespatchingParticulars;
    //            item.Remarks = Remarks;

    //        }
    //    }
    //    gv_PerformanceSheet.EditIndex = -1;
    //    Bind_PerformanceSheet_gridview(mydir);

    //}
    //private void ListtoXml_PerFormanceSheet()
    //{
    //    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    //    if (mydir != null)
    //    {
    //        var xDir = new XElement("PerformanceSheets", from dir in mydir
    //                                                     select new XElement("PerformanceSheet", new XAttribute("ID", dir.PrimaryId),
    //                                                         new XElement("OrderNumber", dir.OrderNumber),
    //                                                         new XElement("Date_of_ReceiptOrder", dir.Date_of_ReceiptOrder),
    //                                                         new XElement("Store_Produt_Service_Desc", dir.Store_Produt_Service_Desc),
    //                                                         new XElement("Quantity", dir.Quantity),
    //                                                         new XElement("Value", dir.Value),
    //                                                         new XElement("Name_Consignee", dir.Name_Consignee),
    //                                                         new XElement("DeliveryDueDate", dir.DeliveryDueDate),
    //                                                         new XElement("MaterialDeliveryDate", dir.MaterialDeliveryDate),
    //                 new XElement("DespatchingParticulars", dir.DespatchingParticulars),

    //                 new XElement("Remarks", dir.Remarks)));


    //        objCompany = new clsCompany();
    //        objCompany.Company_PerformanceSheet_insert(xDir.ToString(), CompanyId);
    //    }

    //}
    //protected void gv_PerformanceSheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    int userid = Convert.ToInt32(gv_director.DataKeys[e.RowIndex].Value.ToString());
    //    List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
    //    PerformanceSheet dir = mydir.Where(s => s.rowno == userid).Single();
    //    if (mydir != null)
    //    {
    //        mydir.Remove(dir);
    //    }
    //    Bind_PerformanceSheet_gridview(mydir);
    //}


}