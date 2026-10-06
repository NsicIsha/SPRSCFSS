using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_UnitRenewalStep2 : System.Web.UI.Page
{
    int CompanyId, UnitId, Formtypeid, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    clsCompany objCompany;
    clsAmendment objAmendment;
    clsRenewal objRenewal;
    string prevPage = String.Empty;
    DataSet Myds;
    DataRow[] cityRows;

    DataSet AllstepchangeDs = new DataSet();
    DataRow[] AllstepchangeRow;

    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            txt_3cb.Text = "N.A1";
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
            checkButtonDoubleClick(btnSubmit);
            ddlCity_3ca1.Items.Insert(0, new ListItem("--Select City--"));
            ddlCity_3ca2.Items.Insert(0, new ListItem("--Select City--"));
            ddlCity_3ca3.Items.Insert(0, new ListItem("--Select City--"));
            ddlCity_3ca4.Items.Insert(0, new ListItem("--Select City--"));
            ddlCity_3ca5.Items.Insert(0, new ListItem("--Select City--"));

            if (txt_3ca2Name.Text == "")
            {
                RequiredFieldValidator1.Enabled = false;
            }
            else if (ddlCity_3ca2.SelectedItem.Text.ToString() == "--Select City--" && ddlState_3ca2.SelectedItem.Text.ToString() != "--Select State--")
            {
                RequiredFieldValidator1.Enabled = true;
            }

            if (txt_3ca3Name.Text == "")
            {
                RequiredFieldValidator2.Enabled = false;
            }
            else if (ddlCity_3ca3.SelectedItem.Text.ToString() == "--Select City--" && ddlState_3ca3.SelectedItem.Text.ToString() != "--Select State--")
            {
                RequiredFieldValidator2.Enabled = true;
            }

            if (txt_3ca4Name.Text == "")
            {
                RequiredFieldValidator7.Enabled = false;
            }
            else if (ddlCity_3ca4.SelectedItem.Text.ToString() == "--Select City--" && ddlState_3ca4.SelectedItem.Text.ToString() != "--Select State--")
            {
                RequiredFieldValidator7.Enabled = true;
            }

            if (txt_3ca5Name.Text == "")
            {
                RequiredFieldValidator8.Enabled = false;
            }
            else if (ddlCity_3ca5.SelectedItem.Text.ToString() == "--Select City--" && ddlState_3ca5.SelectedItem.Text.ToString() != "--Select State--")
            {
                RequiredFieldValidator8.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        FunRequest();
        FunFillDataset();
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            //UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
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
            prevPage = "";
        }
    }

    protected void FunFillDataset()
    {
        try
        {

            if (!IsPostBack)
            {
                objRenewal = new clsRenewal();
                Myds = new DataSet();
                DataTable MydtSinglevalue, MydtFactory, MydtDirector = new DataTable();

                //TO FETCH DATA SET VALUE
                Myds = objRenewal.CompanyRenewal_Fetch_Step2_ByCompanyId(CompanyId);


                //ASSIGNING IN MULTIPLE TABLES
                MydtSinglevalue = Myds.Tables[0];
                MydtFactory = Myds.Tables[1];
                MydtDirector = Myds.Tables[2];

                //ASSIGNING IN CACHE
                if (Cache["State"] == null || Cache["City"] == null)
                {
                    Cache.Insert("City", clsDDL.ddlCity_Activated(0), null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                    Cache.Insert("State", clsDDL.ddlState_Activated(), null, DateTime.Now.AddDays(1), TimeSpan.Zero, System.Web.Caching.CacheItemPriority.High, null);
                }

                //FILLING UP ALL DROPDOWN LIST
                FunFillDDl();

                //FILLING UP ALL CONTROLS
                FunFillcontrols(MydtSinglevalue, MydtFactory, MydtDirector);

                FunctionShowHide();
                //Writedatainxml();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //TO BIND CONTROLS
    protected void FunFillcontrols(DataTable MydtSinglevalue, DataTable MydtFactory, DataTable MydtDirector)
    {
        try
        {
            //BINDING FACTORY GRIDVIEW
            grdSSI.DataSource = MydtFactory;
            grdSSI.DataBind();

            //BINDING SINGLE VALUE CONTROLS
            txt_3a.Text = MydtSinglevalue.Rows[0]["fGPtbl_CUnit_3A"].ToString();
            txt_3b.Text = MydtSinglevalue.Rows[0]["fGPtbl_CUnit_3B"].ToString();
            //BIND CONSTITUTION ID
            rd3c.SelectedValue = MydtSinglevalue.Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString();
            txt_3cb.Text = MydtSinglevalue.Rows[0]["fGPtbl_CUnit_3C5"].ToString();

            //BINDING DIRECTORS INFORMATION
            if (MydtDirector.Rows.Count > 0)
            {
                hdnfDirectorId1.Value = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Id"].ToString();
                txt_3ca1Name.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Name"].ToString();
                txt_3ca1Address.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Address"].ToString();
                txt_3ca1Pin.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Zip"].ToString();
                txt_3ca1Tele.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Phone"].ToString();

                txt_3ca1Email.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Email"].ToString();
                txt_3ca1Exp.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Experience"].ToString();
                txt_3ca1Adhar.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Adhar"].ToString();
                
                txt_3ca1share.Text = MydtDirector.Rows[0]["fGPtbl_Company_Directors_Shareholder"].ToString();
                ddlState_3ca1.SelectedValue = MydtDirector.Rows[0]["fGPtbl_Company_Directors_StateId"].ToString();
                ddlBindCity(ddlCity_3ca1, MydtDirector.Rows[0]["fGPtbl_Company_Directors_StateId"].ToString());
                ddlCity_3ca1.SelectedValue = MydtDirector.Rows[0]["fGPtbl_Company_Directors_CityId"].ToString();
            }

            if (MydtDirector.Rows.Count > 1)
            {
                hdnfDirectorId2.Value = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Id"].ToString();
                txt_3ca2Name.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Name"].ToString();
                txt_3ca2Address.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Address"].ToString();
                txt_3ca2Pin.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Zip"].ToString();
                txt_3ca2Tele.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Phone"].ToString();

                txt_3ca2Email.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Email"].ToString();
                txt_3ca2Exp.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Experience"].ToString();
                txt_3ca2Adhar.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Adhar"].ToString();
               
                txt_3ca2share.Text = MydtDirector.Rows[1]["fGPtbl_Company_Directors_Shareholder"].ToString();
                ddlState_3ca2.SelectedValue = MydtDirector.Rows[1]["fGPtbl_Company_Directors_StateId"].ToString();
                ddlBindCity(ddlCity_3ca2, MydtDirector.Rows[1]["fGPtbl_Company_Directors_StateId"].ToString());
                ddlCity_3ca2.SelectedValue = MydtDirector.Rows[1]["fGPtbl_Company_Directors_CityId"].ToString();
            }

            if (MydtDirector.Rows.Count > 2)
            {
                hdnfDirectorId3.Value = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Id"].ToString();
                txt_3ca3Name.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Name"].ToString();
                txt_3ca3Address.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Address"].ToString();
                txt_3ca3Pin.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Zip"].ToString();
                txt_3ca3Tele.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Phone"].ToString();


                txt_3ca3Email.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Email"].ToString();
                txt_3ca3Exp.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Experience"].ToString();
                txt_3ca3Adhar.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Adhar"].ToString();
             
                
                txt_3ca3share.Text = MydtDirector.Rows[2]["fGPtbl_Company_Directors_Shareholder"].ToString();
                ddlState_3ca3.SelectedValue = MydtDirector.Rows[2]["fGPtbl_Company_Directors_StateId"].ToString();
                ddlBindCity(ddlCity_3ca3, MydtDirector.Rows[2]["fGPtbl_Company_Directors_StateId"].ToString());
                ddlCity_3ca3.SelectedValue = MydtDirector.Rows[2]["fGPtbl_Company_Directors_CityId"].ToString();
            }

            if (MydtDirector.Rows.Count > 3)
            {
                hdnfDirectorId4.Value = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Id"].ToString();
                txt_3ca4Name.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Name"].ToString();
                txt_3ca4Address.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Address"].ToString();
                txt_3ca4Pin.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Zip"].ToString();
                txt_3ca4Tele.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Phone"].ToString();


                txt_3ca4Email.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Email"].ToString();
                txt_3ca4Exp.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Experience"].ToString();
                txt_3ca4Adhar.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Adhar"].ToString();
               
                txt_3ca4share.Text = MydtDirector.Rows[3]["fGPtbl_Company_Directors_Shareholder"].ToString();
                ddlState_3ca4.SelectedValue = MydtDirector.Rows[3]["fGPtbl_Company_Directors_StateId"].ToString();
                ddlBindCity(ddlCity_3ca4, MydtDirector.Rows[3]["fGPtbl_Company_Directors_StateId"].ToString());
                ddlCity_3ca4.SelectedValue = MydtDirector.Rows[3]["fGPtbl_Company_Directors_CityId"].ToString();
            }

            if (MydtDirector.Rows.Count > 4)
            {
                hdnfDirectorId5.Value = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Id"].ToString();
                txt_3ca5Name.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Name"].ToString();
                txt_3ca5Address.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Address"].ToString();
                txt_3ca5Pin.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Zip"].ToString();
                txt_3ca5Tele.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Phone"].ToString();

                txt_3ca5Email.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Email"].ToString();
                txt_3ca5Exp.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Experience"].ToString();
                txt_3ca5Adhar.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Adhar"].ToString();
               
                
                txt_3ca5share.Text = MydtDirector.Rows[4]["fGPtbl_Company_Directors_Shareholder"].ToString();
                ddlState_3ca5.SelectedValue = MydtDirector.Rows[4]["fGPtbl_Company_Directors_StateId"].ToString();
                ddlBindCity(ddlCity_3ca5, MydtDirector.Rows[4]["fGPtbl_Company_Directors_StateId"].ToString());
                ddlCity_3ca5.SelectedValue = MydtDirector.Rows[4]["fGPtbl_Company_Directors_CityId"].ToString();
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
            dropdownbindState(ddlState_3ca1);
            //dropdownbindCity(ddlCity_3ca1);
            dropdownbindState(ddlState_3ca2);
            //dropdownbindCity(ddlCity_3ca2);
            dropdownbindState(ddlState_3ca3);
            //dropdownbindCity(ddlCity_3ca3);
            dropdownbindState(ddlState_3ca4);
            //dropdownbindCity(ddlCity_3ca4);
            dropdownbindState(ddlState_3ca5);
            //dropdownbindCity(ddlCity_3ca5);

            //FILL RADIOBUTTON LIST
            objCompany = new clsCompany();
            DataTable Dtrd3c = new DataTable();
            Dtrd3c = objCompany.Company_Fetch_Constitution();
            rd3c.DataTextField = "fGPtbl_CUnit_Constitution_Name";
            rd3c.DataValueField = "fGPtbl_CUnit_Constitution_Id";
            rd3c.DataSource = Dtrd3c;
            rd3c.DataBind();
            rd3c.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objAmendment = new clsAmendment();
        List<string> grdssiList = new List<string>();
        List<string> directorList = new List<string>();
        string grssivalue = string.Empty;
        string strdirectorlist = string.Empty;
        string dateofInc = string.Empty, dateofCOB = string.Empty, partenerDetail = string.Empty;
        int constid;
        Random random = new Random();

        try
        {
            //FOR MULTIPLE VALUES--GRIDVIEW GRDSSI
            foreach (GridViewRow ssirow in grdSSI.Rows)
            {
                if (ssirow.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnunit = (HiddenField)ssirow.FindControl("hdnunit");
                    TextBox ssinumber = (TextBox)ssirow.FindControl("txt_21");
                    TextBox ssiregdate = (TextBox)ssirow.FindControl("txt_22");
                    grssivalue = hdnunit.Value.Trim() + "^" + ssinumber.Text.Trim() + "^" + ssiregdate.Text.Trim() + "@";
                    grdssiList.Add(grssivalue);
                }
            }

            //FOR MULTIPLE VALUES ---DIRECTORS
            //DIRECTOR 1
            if (txt_3ca1Name.Text.Trim() != "")
            {
                if (hdnfDirectorId1.Value == "0" || hdnfDirectorId1.Value == "" || hdnfDirectorId1.Value == null)
                {
                    hdnfDirectorId1.Value = (random.Next(20000) * -1).ToString();
                }
                //strdirectorlist = hdnfDirectorId1.Value + "^" + txt_3ca1Name.Text.Trim() + "^" + txt_3ca1Address.Text.Trim() + "^" + txt_3ca1Pin.Text.Trim() + "^" +
                //                ddlState_3ca1.SelectedValue.Trim() + "^" + ddlCity_3ca1.SelectedValue.Trim() + "^" + txt_3ca1Tele.Text.Trim() + "^" + txt_3ca1share.Text.Trim() + "@";
                //directorList.Add(strdirectorlist);
                //strdirectorlist = "";

                strdirectorlist = hdnfDirectorId1.Value + "^" + txt_3ca1Name.Text.Trim() + "^" + txt_3ca1Address.Text.Trim() + "^" + txt_3ca1Pin.Text.Trim() + "^" +
                    ddlState_3ca1.SelectedValue.Trim() + "^" + ddlCity_3ca1.SelectedValue.Trim()
                    + "^" + (txt_3ca1Tele.Text.Trim() == "" ? "N.A." : txt_3ca1Tele.Text.Trim()) + "^" + txt_3ca1Email.Text.Trim()
                    + "^" + txt_3ca1Exp.Text.Trim() + "^" + txt_3ca1Adhar.Text.Trim() + "^" + txt_3ca1share.Text.Trim() + "|";
                directorList.Add(strdirectorlist);
                strdirectorlist = "";
            }

            //DIRECTOR 2
            if (txt_3ca2Name.Text.Trim() != "")
            {
                if (hdnfDirectorId2.Value == "0" || hdnfDirectorId2.Value == "" || hdnfDirectorId2.Value == null)
                {
                    hdnfDirectorId2.Value = (random.Next(20000) * -1).ToString();
                }
                strdirectorlist = hdnfDirectorId2.Value + "^" + txt_3ca2Name.Text.Trim() + "^" + (txt_3ca2Address.Text.Trim() == "" ? "N.A." : txt_3ca2Address.Text.Trim()) + "^"
                     + (txt_3ca2Pin.Text.Trim() == "" ? "N.A." : txt_3ca2Pin.Text.Trim()) + "^" + ddlState_3ca2.SelectedValue.Trim() + "^" + ddlCity_3ca2.SelectedValue.Trim() + "^"
                     + (txt_3ca2Tele.Text.Trim() == "" ? "N.A." : txt_3ca2Tele.Text.Trim()) + "^" +
                     txt_3ca2Email.Text.Trim() + "^" + txt_3ca2Exp.Text.Trim() + "^" + txt_3ca2Adhar.Text.Trim() + "^" + txt_3ca2share.Text.Trim() + "|";
                directorList.Add(strdirectorlist);
                strdirectorlist = "";
            }

            //DIRECTOR 3
            if (txt_3ca3Name.Text.Trim() != "")
            {
                if (hdnfDirectorId3.Value == "0" || hdnfDirectorId3.Value == "" || hdnfDirectorId3.Value == null)
                {
                    hdnfDirectorId3.Value = (random.Next(20000) * -1).ToString();
                }
                strdirectorlist = hdnfDirectorId3.Value + "^" + txt_3ca3Name.Text.Trim() + "^" + (txt_3ca3Address.Text.Trim() == "" ? "N.A" : txt_3ca3Address.Text.Trim()) + "^" + (txt_3ca3Pin.Text.Trim() == "" ? "N.A." : txt_3ca3Pin.Text.Trim()) + "^"
                    + ddlState_3ca3.SelectedValue.Trim() + "^" + ddlCity_3ca3.SelectedValue.Trim() + "^" +
                    (txt_3ca3Tele.Text.Trim() == "" ? "N.A." : txt_3ca3Tele.Text.Trim()) + "^" + txt_3ca3Email.Text.Trim() + "^" +
                    txt_3ca3Exp.Text.Trim() + "^" + txt_3ca3Adhar.Text.Trim() + "^" + txt_3ca3share.Text.Trim() + "|";
                directorList.Add(strdirectorlist);
                strdirectorlist = "";
            }

            //DIRECTOR 4
            if (txt_3ca4Name.Text.Trim() != "")
            {
                if (hdnfDirectorId4.Value == "0" || hdnfDirectorId4.Value == "" || hdnfDirectorId4.Value == null)
                {
                    hdnfDirectorId4.Value = (random.Next(20000) * -1).ToString();
                }
                strdirectorlist = hdnfDirectorId4.Value + "^" + txt_3ca4Name.Text.Trim() + "^" + (txt_3ca4Address.Text.Trim() == "" ? "N.A" : txt_3ca4Address.Text.Trim()) + "^" + (txt_3ca4Pin.Text.Trim() == "" ? "N.A." : txt_3ca4Pin.Text.Trim()) + "^"
                    + ddlState_3ca4.SelectedValue.Trim() + "^" + ddlCity_3ca4.SelectedValue.Trim() + "^"
                    + (txt_3ca4Tele.Text.Trim() == "" ? "N.A." : txt_3ca4Tele.Text.Trim()) + "^" + txt_3ca4Email.Text.Trim() + "^" +
                    txt_3ca4Exp.Text.Trim() + "^" + txt_3ca4Adhar.Text.Trim() + "^" + txt_3ca4share.Text.Trim() + "|";
                directorList.Add(strdirectorlist);
                strdirectorlist = "";
            }

            //DIRECTOR 5
            if (txt_3ca5Name.Text.Trim() != "")
            {
                if (hdnfDirectorId5.Value == "0" || hdnfDirectorId5.Value == "" || hdnfDirectorId5.Value == null)
                {
                    hdnfDirectorId5.Value = (random.Next(20000) * -1).ToString();
                }
                strdirectorlist = hdnfDirectorId5.Value + "^" + txt_3ca5Name.Text.Trim() + "^" + (txt_3ca5Address.Text.Trim() == "" ? "N.A" : txt_3ca5Address.Text.Trim()) + "^" + (txt_3ca5Pin.Text.Trim() == "" ? "N.A." : txt_3ca5Pin.Text.Trim()) + "^"
                    + ddlState_3ca5.SelectedValue.Trim() + "^" + ddlCity_3ca5.SelectedValue.Trim()
                    + "^" + (txt_3ca5Tele.Text.Trim() == "" ? "N.A." : txt_3ca5Tele.Text.Trim()) + "^" + txt_3ca5Email.Text.Trim() + "^" +
                    txt_3ca5Exp.Text.Trim() + "^" + txt_3ca5Adhar.Text.Trim() + "^" + txt_3ca5share.Text.Trim() + "|";
                directorList.Add(strdirectorlist);
                strdirectorlist = "";
            }


            // FOR SINGLE VALUES
            dateofInc = txt_3a.Text.Trim();
            dateofCOB = txt_3b.Text.Trim();
            constid = Convert.ToInt32(rd3c.SelectedValue.Trim());
            partenerDetail = txt_3cb.Text.Trim();

            int successId = objAmendment.CompanyAmendment_InsertUpdate_Step2(directorList, grdssiList, dateofInc, dateofCOB, constid, partenerDetail, CompanyId, UserId, "renewal");

            DecryptEncrypt objDecEnc = new DecryptEncrypt();

            string EncryptCompany = string.Empty, EncryptFormtype = string.Empty, EncryptBranchId = string.Empty;

            if (successId == 2) //FOR SUCCESSFUL INSERT
            {
                EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                EncryptFormtype = objDecEnc.Encrypt("2");
                EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                Response.Redirect("UnitRenewalStep3.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&b=" + EncryptBranchId, false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void ddlState_3ca1_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable MydtCity, MydtCityClone = new DataTable();
            MydtCity = (DataTable)Cache["City"];
            MydtCityClone = MydtCity.Clone();
            DropDownList cityddl = (DropDownList)sender;
            string StateddlId = cityddl.ID;

            //cityRows = MydtCity.Select("fGPtbl_MCty_StateId=" + ddlState_3ca1.SelectedValue.ToString());
            cityRows = MydtCity.Select("fGPtbl_MCty_StateId=" + cityddl.SelectedValue.ToString());

            foreach (DataRow r1 in cityRows)
            {
                MydtCityClone.ImportRow(r1);
            }

            if (StateddlId == "ddlState_3ca1")
                dropdownbindCity(ddlCity_3ca1, MydtCityClone);

            else if (StateddlId == "ddlState_3ca2")
                dropdownbindCity(ddlCity_3ca2, MydtCityClone);

            else if (StateddlId == "ddlState_3ca3")
                dropdownbindCity(ddlCity_3ca3, MydtCityClone);

            else if (StateddlId == "ddlState_3ca4")
                dropdownbindCity(ddlCity_3ca4, MydtCityClone);

            else if (StateddlId == "ddlState_3ca5")
                dropdownbindCity(ddlCity_3ca5, MydtCityClone);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btn_back_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();

        string EncryptCompany = string.Empty, EncryptFormtype = string.Empty, EncryptBranchId = string.Empty;

        EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        EncryptFormtype = objDecEnc.Encrypt("2");
        EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        Response.Redirect("UnitRenewalStep1.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&b=" + EncryptBranchId, false);
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
    protected void dropdownbindState(DropDownList ddlState)
    {
        try
        {
            DataTable MydtState = new DataTable();
            MydtState = (DataTable)Cache["State"];

            ddlState.DataSource = MydtState;
            ddlState.DataTextField = MydtState.Columns["fGPtbl_MS_Name"].ToString();
            ddlState.DataValueField = MydtState.Columns["fGPtbl_MS_Id"].ToString();
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", ""));

            // MydtState.Clear();  // Comeented By Ravis kuamar pandey
            //  MydtState.Dispose();// Comeented By Ravis kuamar pandey
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void dropdownbindCity(DropDownList ddlCity)
    {
        try
        {
            DataTable MydtCity = new DataTable();
            MydtCity = (DataTable)Cache["City"];

            ddlCity.DataSource = MydtCity;
            ddlCity.DataTextField = MydtCity.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity.DataValueField = MydtCity.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity.DataBind();
            ddlCity.Items.Insert(0, new ListItem("--Select City--", ""));

           
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void dropdownbindCity(DropDownList CityDDL, DataTable MydtCity)
    {
        try
        {
            CityDDL.DataSource = MydtCity;
            CityDDL.DataTextField = MydtCity.Columns["fGPtbl_MCty_Name"].ToString();
            CityDDL.DataValueField = MydtCity.Columns["fGPtbl_MCty_Id"].ToString();
            CityDDL.DataBind();
            CityDDL.Items.Insert(0, new ListItem("--Select City--", ""));

            MydtCity.Clear();
            MydtCity.Dispose();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void FunctionShowHide()
    {

        try
        {
            AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3A' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
               txt_3a.ReadOnly = false;
            }
            else
            {
               txt_3a.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3B' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {

                ////      txt_3b.ReadOnly = false;
            }
            else
            {
                ////////  txt_3b.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3C123_ConstitutionId' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
               rd3c.Enabled = true;
            }
            else
            {
            rd3c.Enabled = false;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3C5' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3cb.ReadOnly = false;
            }
            else
            {
                txt_3cb.ReadOnly = true;
            }
            if (AllstepchangeDs.Tables[2].Rows.Count > 0)
            {
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Name' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Name.ReadOnly = false;
                    txt_3ca2Name.ReadOnly = false;
                    txt_3ca3Name.ReadOnly = false;
                    txt_3ca3Name.ReadOnly = false;
                    txt_3ca4Name.ReadOnly = false;
                    txt_3ca5Name.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Name.ReadOnly = true;
                    txt_3ca2Name.ReadOnly = true;
                    txt_3ca3Name.ReadOnly = true;
                    txt_3ca3Name.ReadOnly = true;
                    txt_3ca4Name.ReadOnly = true;
                    txt_3ca5Name.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Address' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Address.ReadOnly = false;
                    txt_3ca2Address.ReadOnly = false;
                    txt_3ca3Address.ReadOnly = false;
                    txt_3ca4Address.ReadOnly = false;
                    txt_3ca5Address.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Address.ReadOnly = true;
                    txt_3ca2Address.ReadOnly = (txt_3ca2Address.Text == "") ? false : true;
                    txt_3ca3Address.ReadOnly = (txt_3ca3Address.Text == "") ? false : true;
                    txt_3ca4Address.ReadOnly = (txt_3ca4Address.Text == "") ? false : true;
                    txt_3ca5Address.ReadOnly = (txt_3ca5Address.Text == "") ? false : true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname ='fGPtbl_Company_Directors_Zip' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Pin.ReadOnly = false;
                    txt_3ca2Pin.ReadOnly = false;
                    txt_3ca3Pin.ReadOnly = false;
                    txt_3ca4Pin.ReadOnly = false;
                    txt_3ca5Pin.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Pin.ReadOnly = true;
                    txt_3ca2Pin.ReadOnly = true;
                    txt_3ca3Pin.ReadOnly = true;
                    txt_3ca4Pin.ReadOnly = true;
                    txt_3ca5Pin.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Phone' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Tele.ReadOnly = false;
                    txt_3ca2Tele.ReadOnly = false;
                    txt_3ca3Tele.ReadOnly = false;
                    txt_3ca4Tele.ReadOnly = false;
                    txt_3ca5Tele.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Tele.ReadOnly = true;
                    txt_3ca2Tele.ReadOnly = true;
                    txt_3ca3Tele.ReadOnly = true;
                    txt_3ca4Tele.ReadOnly = true;
                    txt_3ca5Tele.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_StateId' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    ddlState_3ca1.Enabled = true;
                    ddlState_3ca2.Enabled = true;
                    ddlState_3ca3.Enabled = true;
                    ddlState_3ca4.Enabled = true;
                    ddlState_3ca5.Enabled = true;
                }
                else
                {
                    ddlState_3ca1.Enabled = false;
                    ddlState_3ca2.Enabled = false;
                    ddlState_3ca3.Enabled = false;
                    ddlState_3ca4.Enabled = false;
                    ddlState_3ca5.Enabled = false;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_CityId' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    ddlCity_3ca1.Enabled = true;
                    ddlCity_3ca2.Enabled = true;
                    ddlCity_3ca3.Enabled = true;
                    ddlCity_3ca4.Enabled = true;
                    ddlCity_3ca5.Enabled = true;
                }
                else
                {
                    ddlCity_3ca1.Enabled = false;
                    ddlCity_3ca2.Enabled = false;
                    ddlCity_3ca3.Enabled = false;
                    ddlCity_3ca4.Enabled = false;
                    ddlCity_3ca5.Enabled = false;
                }


                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Phone' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Email.ReadOnly = false;
                    txt_3ca2Email.ReadOnly = false;
                    txt_3ca3Email.ReadOnly = false;
                    txt_3ca4Email.ReadOnly = false;
                    txt_3ca5Email.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Email.ReadOnly = true;
                    txt_3ca2Email.ReadOnly = true;
                    txt_3ca3Email.ReadOnly = true;
                    txt_3ca4Email.ReadOnly = true;
                    txt_3ca5Email.ReadOnly = true;
                }


                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Phone' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Exp.ReadOnly = false;
                    txt_3ca2Exp.ReadOnly = false;
                    txt_3ca3Exp.ReadOnly = false;
                    txt_3ca4Exp.ReadOnly = false;
                    txt_3ca5Exp.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Exp.ReadOnly = true;
                    txt_3ca2Exp.ReadOnly = true;
                    txt_3ca3Exp.ReadOnly = true;
                    txt_3ca4Exp.ReadOnly = true;
                    txt_3ca5Exp.ReadOnly = true;
                }


                AllstepchangeRow = AllstepchangeDs.Tables[2].Select("Colname='fGPtbl_Company_Directors_Phone' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt_3ca1Adhar.ReadOnly = false;
                    txt_3ca2Adhar.ReadOnly = false;
                    txt_3ca3Adhar.ReadOnly = false;
                    txt_3ca4Adhar.ReadOnly = false;
                    txt_3ca5Adhar.ReadOnly = false;
                }
                else
                {
                    txt_3ca1Adhar.ReadOnly = true;
                    txt_3ca2Adhar.ReadOnly = true;
                    txt_3ca3Adhar.ReadOnly = true;
                    txt_3ca4Adhar.ReadOnly = true;
                    txt_3ca5Adhar.ReadOnly = true;
                }


            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    protected void grdSSI_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_SSIRegistrationNo'and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtssi = (TextBox)e.Row.FindControl("txt_21");
                    txtssi.ReadOnly = false;
                }
                else
                {
                    TextBox txtssi = (TextBox)e.Row.FindControl("txt_21");
                    txtssi.ReadOnly = true;

                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_SSIRegistrationDate' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    TextBox txtDate = (TextBox)e.Row.FindControl("txt_22");
                    txtDate.ReadOnly = false;
                }
                else
                {
                    TextBox txtDate = (TextBox)e.Row.FindControl("txt_22");
                    txtDate.ReadOnly = true;
                }
            }
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

}
