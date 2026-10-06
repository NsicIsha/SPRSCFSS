using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using NSICGP_VBClassLibrary;
using System.Linq;

public partial class appAdministrator_CompanyMissingInformation : System.Web.UI.Page
{
    public DataSet Myds;
    public int rowcnt = 0;
    public int txtid = 100;
    public List<string> MissingValuesList = new List<string>();
    public List<string> MissingValuesDirectorsList = new List<string>();
    public List<string> MissingValuesFactoryList = new List<string>();
    public List<string> TextboxIdListforCompany = new List<string>();
    public List<string> TextboxIdListforFactory = new List<string>();
    public List<string> TextboxIdListforDirector = new List<string>();
    string companystr = string.Empty;
    string factorystr = string.Empty;
    string directorstr = string.Empty;

    int companyid;
    int Unitid;
    int Director_id;

    internal Literal getliteral(string text)
    {
        Literal rv = new Literal();
        rv.Text = text;
        return rv;
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        clsCompany objCompany = new clsCompany();

        //string Director_Id = "<strong style=visibility:hidden;>" + '@' + directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString() + "</strong>";
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        Company_id.Text = objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(" ", "+")).ToString();
        companyid = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(" ", "+")).ToString());

        Myds = new DataSet();
        Myds = clsDDL.UnavailableData_Fetch_ByCompanyId_Dataset(Convert.ToInt32(Company_id.Text));
        DataTable companyDt, factoryDt, directorDt = new DataTable();
        try
        {
            if (Myds.Tables.Count > 0)
            {
                companyDt = Myds.Tables[0];
                factoryDt = Myds.Tables[1];
                directorDt = Myds.Tables[2];

                if (companyDt.Rows.Count > 0)
                {
                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_PAN"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Permanent Account Number");
                        TextboxIdListforCompany.Add("fGPtbl_Company_PAN");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_Name"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Name");
                        TextboxIdListforCompany.Add("fGPtbl_Company_Name");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office Address");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_Address");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office Address Pincode");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_Zip");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office State");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_StateIdD");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_CityId"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office City");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_CityIdD");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office Telephone Number");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_Phone");
                    }

                    if (Equals(companyDt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString(), "1"))
                    {
                        MissingValuesList.Add("Company Head Office Email");
                        TextboxIdListforCompany.Add("fGPtbl_Company_HeadOffice_Email");
                    }
                }

                /*  if (!Equals(factoryDt.Rows[0]["fGPtbl_CUnit_Id"].ToString(), "0"))
                  {


                      Unitid = Convert.ToInt32(factoryDt.Rows[0]["fGPtbl_CUnit_Id"].ToString());

                  }


                  if (!Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString(), "0"))
                  {

                      Director_id = Convert.ToInt32(directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString());
                  }*/


                //FOR FACTORY
                if (factoryDt.Rows.Count > 0)
                {
                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Factory Category Area");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_CategoryAreaIdD");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Factor Category Caste");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_CategoryCastIdD");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Factory Category Unit Size");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_CategoryUnitSizeIdD");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Constitution of firm");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3C123_ConstitutionId");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3A"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Date of Incorporation");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3A");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3B"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Date of commencement of production/business");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3B");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3C5"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether you are a Partner/Director of any large industries or units registered with NSIC/DGS&D. If so, give details.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3C5");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3D"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether your firm is registered under which ACT");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3DD");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3E"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("The firm is a subsidiary/Associate of an Indian/foreign/large scale company, give particulars of Parent/holding Company:");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3E");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3F"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("If there are other subsidiaries of the same parent company in India, give full particulars.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3F");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3G"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("If the Directors/Partners/Proprietor have financial interests or are represented on Board(s) of other companies, give details.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3G");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3H"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether any portion of your capital is held by any large unit? If some, give details.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3H");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3I"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether there is any inter-locking of capital or loan funds with any large unit under the same management? If so, give details.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3I");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3J"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether any loan or advance has been under written by any large unit or persons possessing large means? If so, give details.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3J");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3K1"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Income-tax Circle/Ward/District.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3K1");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3L"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Sales Tax Registration No(s)");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3L");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3M"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Did you applied for registration with NSIC before under this or any other name? If so, with what result?");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3M");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3N"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Are you already doing business with NSIC under this or some other name? If so, give name and address.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3N");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_3O"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether your firm or any of its Directors/Partners were prosecuted at any time for any offence under any Court for Civil/Criminal/Revenue or Economic Offence? If so, give particulars of offence and penalty.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_3O");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_4A"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Whether you are registered with DGS&D? If so, give registration No. and date, attach a copy of registration certificate and specify the item for which registered.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_4A");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_4B"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Have you supplied any stores to the Government through DGS&D, Direct to the Government Department or through any other Supplier? If so, give details of the contract if any, during the last Three years in Annexure-‘G’");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_4B");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_8"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("State if the products have been fully tested, including type tests where required in specifications. If so, attach copy of test reports:");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_8");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_9"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("If the products require after sales service, give Names & Addresses of places where such facilities are available. Indicate if your products are directly marketed by you in whole of India. If not, indicate names of firms with whom marketing arrangements have been made product-wise/area-wise:");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_9");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_10"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Statement indicating the Result of Operations and Financial Position of the SSI Unit for the last three years (year-wise) duly supported by relevant Audited Balance Sheets (Annexure-C).");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_10");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_11"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Name and address of the Bankers (Indicate Account No. with each bank and submit Bankers Report in Original as per proforma in Annexure-F):");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_11");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_12A"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("State if the products are BIS Marked. If so, attach a copy of valid BIS License.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_12A");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_12B"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("State if the firm has ISO 9000 Certification. If yes, attach copy of the same.");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_12B");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_13A"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Name and designation of the signatory of this application (enclose a copy of Power of Attorney where applicable) NAME");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_13A");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_13B"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Name and designation of the signatory of this application (enclose a copy of Power of Attorney where applicable) DESIGNATION");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_13B");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_1B_Factory_Zip"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Pincode");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_1B_Factory_Zip");
                    }

                    if (Equals(factoryDt.Rows[0]["fGPtbl_CUnit_1B_Factory_Address"].ToString(), "1"))
                    {
                        MissingValuesFactoryList.Add("Factory Address");
                        TextboxIdListforFactory.Add("fGPtbl_CUnit_1B_Factory_Address");
                    }

                    if (!Equals(factoryDt.Rows[0]["fGPtbl_CUnit_Id"].ToString(), "0"))
                    {
                        //MissingValuesFactoryList.Add("UnitId");
                        // TextboxIdListforFactory.Add("fGPtbl_CUnit_Id");
                        HiddenField hdn_unit = new HiddenField();
                        hdn_unit.Value = factoryDt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
                        Unitid = Convert.ToInt32(factoryDt.Rows[0]["fGPtbl_CUnit_Id"].ToString());
                        //TextboxIdListforFactory.Add(hdn_unit.Value);
                    }
                }


                //FOR DIRECTORS
                if (directorDt.Rows.Count > 0)
                {
                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Name"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director Name");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_Name");
                    }

                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Address"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director Address");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_Address");
                    }

                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Zip"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director Pincode");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_Zip");
                    }

                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_StateId"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director State");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_StateIdD");
                    }

                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_CityId"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director City");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_CityIdD");
                    }

                    if (Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Phone"].ToString(), "1"))
                    {
                        MissingValuesDirectorsList.Add("Director Phone number");
                        TextboxIdListforDirector.Add("fGPtbl_Company_Directors_Phone");
                    }

                    if (!Equals(directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString(), "0"))
                    {
                        // MissingValuesDirectorsList.Add("Directorid");
                        //TextboxIdListforDirector.Add("fGPtbl_Company_Directors_Id");
                        HiddenField hdn_directorid = new HiddenField();
                        hdn_directorid.Value = directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString();
                        // TextboxIdListforDirector.Add(hdn_directorid.Value.ToString());
                        //   MissingValuesDirectorsList.Add(directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString());
                        Director_id = Convert.ToInt32(directorDt.Rows[0]["fGPtbl_Company_Directors_Id"].ToString());
                    }
                }




                TableRow trow;
                TableCell tcell;
                int Cnt = 0;

                Table tbl_1 = new Table();
                tbl_1.CellPadding = 1;
                tbl_1.CellSpacing = 1;
                tbl_1.Width = Unit.Percentage(100);
                TextBox tcellbox;
                DropDownList ddl_compnystate_id;
                DropDownList ddl_companies_city_id;

                foreach (string str in MissingValuesList)
                {
                    tcellbox = new TextBox();
                    ddl_compnystate_id = new DropDownList();
                    ddl_companies_city_id = new DropDownList();
                    trow = new TableRow();
                    //trow.Style.Add("padding-top", "10px");

                    tcell = new TableCell();
                    tcell.Text = Cnt.ToString() + ".";
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.HorizontalAlign = HorizontalAlign.Right;
                    tcell.Width = Unit.Percentage(5);
                    //tcell.CssClass = "clstblCell";
                    //  trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = str;
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.Width = Unit.Percentage(60);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcellbox.ID = TextboxIdListforCompany[Cnt];

                    //var  Company_state=MissingValuesList.Where(item=>item.ToString()=="Company Head Office State").ToList();
                    // var Company_City = MissingValuesList.Where(item => item.ToString() == "Company Head Office City").ToList();


                    if (str == "Company Head Office State")
                    {
                        ddl_compnystate_id.AutoPostBack = true;
                        ddl_compnystate_id.Attributes.Add("runat", "server");
                        //   ddl_compnystate_id.ID = "ddl_" + str.ToString().Replace(" ", "").Trim().ToString();
                        ddl_compnystate_id.ID = "fGPtbl_Company_HeadOffice_StateId ";
                        ddl_compnystate_id.CssClass = "clsDDL";
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = clsDDL.ddlState_Activated();
                        ddl_compnystate_id.DataSource = dt;
                        ddl_compnystate_id.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
                        ddl_compnystate_id.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
                        ddl_compnystate_id.DataBind();
                        ddl_compnystate_id.Items.Insert(0, new ListItem("--Select State--", ""));

                        tcell.Controls.Add(ddl_compnystate_id);
                    }


                    if (str == "Company Head Office City")
                    {
                        ddl_companies_city_id.AutoPostBack = false;
                        ddl_compnystate_id.Attributes.Add("runat", "server");
                        // ddl_companies_city_id.ID = "ddl_" + str.ToString().Replace(" ", "").Trim().ToString();
                        ddl_compnystate_id.ID = "fGPtbl_Company_HeadOffice_CityId";
                        ddl_companies_city_id.CssClass = "clsDDL";

                        System.Data.DataTable dt_c = new System.Data.DataTable();
                        dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl_compnystate_id.SelectedValue));
                        ddl_companies_city_id.DataSource = dt_c;
                        ddl_companies_city_id.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
                        ddl_companies_city_id.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                        ddl_companies_city_id.DataBind();
                        ddl_companies_city_id.Items.Insert(0, new ListItem("--Select City--", ""));
                        dt_c.Clear();
                        dt_c.Dispose();
                        tcell.Controls.Add(ddl_companies_city_id);

                    }


                    else
                    {
                        tcell.Controls.Add(tcellbox);
                    }
                    //tcell.Text = "<input id='"+TextboxIdListforCompany[Cnt]+"' name='rdReg' type='text' />";
                    tcell.Width = Unit.Percentage(20);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);

                    //tbl_1.Rows.Add(trow);
                    dyntblControl.Rows.Add(trow);

                    Cnt++;
                }


                int valueat = 0;
                foreach (string str1 in MissingValuesFactoryList)
                {
                    tcellbox = new TextBox();
                    trow = new TableRow();

                    //Whether your firm is registered under which ACT
                    DropDownList ddl_factory_state_id = new DropDownList();
                    DropDownList ddl_factory_city_id = new DropDownList();
                    DropDownList ddl_register_under_act = new DropDownList();

                    DropDownList ddl_F_C_Area = new DropDownList();
                    DropDownList ddl_F_C_cast = new DropDownList();
                    DropDownList ddl_F_C_unit_size = new DropDownList();

                    trow.Style.Add("padding-top", "10px");

                    tcell = new TableCell();
                    tcell.Text = Cnt.ToString() + ".";
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.HorizontalAlign = HorizontalAlign.Right;
                    tcell.Width = Unit.Percentage(5);
                    //tcell.CssClass = "clstblCell";
                    //  trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = str1;
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.Width = Unit.Percentage(60);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);
                    //  var Register_Under_act = MissingValuesFactoryList.Where(item => item.ToString() == "Whether your firm is registered under which ACT").ToList();







                    tcell = new TableCell();
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcellbox.ID = TextboxIdListforFactory[valueat];
                    tcellbox.Attributes.Add("TextMode", "MultiLine");


                    if (str1 == "Whether your firm is registered under which ACT")
                    {
                        ddl_register_under_act.AutoPostBack = false;
                        ddl_register_under_act.Attributes.Add("runat", "server");
                        ddl_register_under_act.ID = "fGPtbl_CUnit_3D";
                        ddl_register_under_act.CssClass = "clsDDL";
                        objCompany = new clsCompany();
                        System.Data.DataTable Dt = new System.Data.DataTable();

                        Dt = objCompany.Company_Fetch_RegisteredUnder();
                        ddl_register_under_act.DataTextField = "fGPtbl_CUnit_RegisteredUnder_Name";

                        ddl_register_under_act.DataValueField = "fGPtbl_CUnit_RegisteredUnder_Id";
                        ddl_register_under_act.DataSource = Dt;
                        ddl_register_under_act.DataBind();

                        ddl_register_under_act.SelectedIndex = 0;



                        tcell.Controls.Add(ddl_register_under_act);



                    }
                    if (str1 == "Factory Category Area")
                    {
                        ddl_F_C_Area.AutoPostBack = false;
                        ddl_F_C_Area.Attributes.Add("runat", "server");
                        //ddl_F_C_Area.ID = "ddl_" + str1.ToString().Replace(" ", "").Trim().ToString();
                        ddl_F_C_Area.ID = "fGPtbl_CUnit_CategoryAreaId";
                        ddl_F_C_Area.CssClass = "clsDDL";
                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = clsDDL.ddlCategoryArea();
                        ddl_F_C_Area.DataSource = dt;
                        ddl_F_C_Area.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
                        ddl_F_C_Area.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
                        ddl_F_C_Area.DataBind();
                        ddl_F_C_Area.Items.Insert(0, new ListItem("--Select Category Area--", ""));
                        dt.Dispose();
                        tcell.Controls.Add(ddl_F_C_Area);
                    }
                    if (str1 == "Factor Category Caste")
                    {
                        ddl_F_C_cast.AutoPostBack = false;
                        ddl_F_C_cast.Attributes.Add("runat", "server");
                        // ddl_F_C_cast.ID = "ddl_" + str1.ToString().Replace(" ", "").Trim().ToString();
                        ddl_F_C_cast.ID = "fGPtbl_CUnit_CategoryCastId";
                        ddl_F_C_cast.CssClass = "clsDDL";

                        System.Data.DataTable dt1 = new System.Data.DataTable();
                        dt1 = clsDDL.ddlCategoryCast(); ;
                        dt1 = clsDDL.ddlCategoryCast();
                        ddl_F_C_cast.DataSource = dt1;
                        ddl_F_C_cast.DataTextField = dt1.Columns["fGPtbl_CUnit_CC_Name"].ToString();
                        ddl_F_C_cast.DataValueField = dt1.Columns["fGPtbl_CUnit_CC_Id"].ToString();
                        ddl_F_C_cast.DataBind();
                        ddl_F_C_cast.Items.Insert(0, new ListItem("--Select Category Cast--", ""));
                        dt1.Dispose();
                        tcell.Controls.Add(ddl_F_C_cast);
                    }
                    if (str1 == "Factory Category Unit Size")
                    {
                        System.Data.DataTable dt2 = new System.Data.DataTable();
                        dt2 = clsDDL.ddlCategoryUnitSize();
                        ddl_F_C_unit_size.AutoPostBack = false;
                        ddl_F_C_unit_size.Attributes.Add("runat", "server");
                        // ddl_F_C_unit_size.ID = "ddl_" + str1.ToString().Replace(" ", "").Trim().ToString();
                        ddl_F_C_unit_size.ID = "fGPtbl_CUnit_CategoryUnitSizeId";
                        ddl_F_C_unit_size.CssClass = "clsDDL";
                        ddl_F_C_unit_size.DataSource = dt2;
                        ddl_F_C_unit_size.DataTextField = dt2.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
                        ddl_F_C_unit_size.DataValueField = dt2.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
                        ddl_F_C_unit_size.DataBind();
                        ddl_F_C_unit_size.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

                        dt2.Dispose();

                        tcell.Controls.Add(ddl_F_C_unit_size);

                    }



                    else
                    {
                        tcell.Controls.Add(tcellbox);
                    }

                    //tcell.Text = "<input id='" + TextboxIdListforFactory[valueat] + "' name='rdReg' type='text' />";
                    tcell.Width = Unit.Percentage(20);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);

                    //tbl_1.Rows.Add(trow);
                    dyntblControl.Rows.Add(trow);

                    Cnt++;
                    valueat++;
                }

                valueat = 0;
                foreach (string str2 in MissingValuesDirectorsList)
                {
                    tcellbox = new TextBox();
                    DropDownList ddl_director_state = new DropDownList();
                    DropDownList ddl_director_city = new DropDownList();
                    trow = new TableRow();
                    trow.Style.Add("padding-top", "10px");

                    tcell = new TableCell();
                    tcell.Text = Cnt.ToString() + ".";
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.HorizontalAlign = HorizontalAlign.Right;
                    tcell.Width = Unit.Percentage(5);
                    //tcell.CssClass = "clstblCell";
                    //   trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.Text = str2;
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcell.Width = Unit.Percentage(60);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);

                    tcell = new TableCell();
                    tcell.VerticalAlign = VerticalAlign.Top;
                    tcellbox.ID = TextboxIdListforDirector[valueat];
                    if (str2 == "Director State")
                    {
                        ddl_director_state.AutoPostBack = false;
                        ddl_director_state.Attributes.Add("runat", "server");
                        // ddl_director_state.ID = "ddl_" + str2.ToString().Replace(" ", "").Trim().ToString();
                        ddl_director_state.ID = "fGPtbl_Company_Directors_StateId";
                        ddl_director_state.CssClass = "clsDDL";

                        System.Data.DataTable dt = new System.Data.DataTable();
                        dt = clsDDL.ddlState_Activated();
                        ddl_director_state.DataSource = dt;
                        ddl_director_state.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
                        ddl_director_state.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
                        ddl_director_state.DataBind();
                        ddl_director_state.Items.Insert(0, new ListItem("--Select State--", ""));

                        tcell.Controls.Add(ddl_director_state);
                    }
                    if (str2 == "Director City")
                    {
                        ddl_director_city.AutoPostBack = false;
                        ddl_director_city.Attributes.Add("runat", "server");
                        //ddl_director_city.ID = "ddl_" + str2.ToString().Replace(" ", "").Trim().ToString();
                        ddl_director_city.ID = "fGPtbl_Company_Directors_CityId";
                        ddl_director_city.CssClass = "clsDDL";

                        System.Data.DataTable dt_c = new System.Data.DataTable();
                        dt_c = clsDDL.ddlCity_Activated(Convert.ToInt32(ddl_director_state.SelectedValue));
                        ddl_director_city.DataSource = dt_c;
                        ddl_director_city.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
                        ddl_director_city.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
                        ddl_director_city.DataBind();
                        ddl_director_city.Items.Insert(0, new ListItem("--Select City--", ""));
                        dt_c.Clear();
                        dt_c.Dispose();

                        tcell.Controls.Add(ddl_director_city);
                    }

                    tcell.Controls.Add(tcellbox);
                    tcell.Width = Unit.Percentage(20);
                    //tcell.CssClass = "clstblCell";
                    trow.Cells.Add(tcell);

                    //tbl_1.Rows.Add(trow);
                    dyntblControl.Rows.Add(trow);

                    Cnt++;
                    valueat++;
                }

                //CODE FOR DYNAMIC TABLE ENDS
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No Data to update for this company";
            }

            if (MissingValuesList.Count == 0 && MissingValuesFactoryList.Count == 0 && MissingValuesDirectorsList.Count == 0)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "No Data to update for this company";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void Page_Load(object sender, EventArgs e)
    {
        //lblMessage.Visible = false;
        //if (MissingValuesList.Count == 0 && MissingValuesFactoryList.Count == 0 && MissingValuesDirectorsList.Count == 0)
        //{
        //    lblMessage.Visible = true;
        //    lblMessage.Text = "No Data to update for this company";
        //}
    }

    protected void btnclick_Click(object sender, EventArgs e)
    {
        StringBuilder sbCompany=new StringBuilder();
        StringBuilder sbFactory = new StringBuilder();
        StringBuilder sbDirector = new StringBuilder();
       

        StringBuilder bsRDreg = new StringBuilder();
        int rwcount=dyntblControl.Rows.Count;
        string tbtext, tbid;
        TextBox tb;
         DropDownList ddl;

         try
         {
             foreach (TableRow tr in dyntblControl.Rows)
             {//5
                 foreach (TableCell tc in tr.Cells)
                 {//4
                     foreach (Control c in tc.Controls)
                     {//3
                         if (c.GetType() == typeof(DropDownList))
                         {
                             ddl = (DropDownList)c;
                             tbid = ddl.ID;
                             tbtext = ddl.SelectedValue;
                             if (tbid.Contains("Company") && !tbid.Contains("Company_Directors"))
                             {
                                 sbCompany.Append(tbid);
                                 sbCompany.Append("='" + tbtext + "',");
                             }
                             else if (tbid.Contains("CUnit"))
                             {

                                 sbFactory.Append(tbid);
                                 sbFactory.Append("='" + tbtext + "', ");
                             }
                             else if (tbid.Contains("Company_Directors"))
                             {
                                 sbDirector.Append(tbid);
                                 sbDirector.Append("='" + tbtext + "', ");
                             }

                         }

                         if (c.GetType() == typeof(TextBox))
                         {//2
                             tb = (TextBox)c;
                             tbid = tb.ID;
                             tbtext = tb.Text;


                             if (tbid.Contains("Company") && !tbid.Contains("Company_Directors"))
                             {
                                 if (tbid == "fGPtbl_Company_HeadOffice_StateIdD" || tbid == "fGPtbl_Company_HeadOffice_CityIdD")
                                 {
                                 }
                                 else
                                 {
                                     sbCompany.Append(tbid);
                                     sbCompany.Append("='" + tbtext + "', ");
                                 }
                             }
                             else if (tbid.Contains("CUnit"))
                             {
                                 if (tbid == "fGPtbl_CUnit_3DD" || tbid == "fGPtbl_CUnit_CategoryAreaIdD" || tbid == "fGPtbl_CUnit_CategoryCastIdD" || tbid == "fGPtbl_CUnit_CategoryUnitSizeIdD")
                                 {
                                 }
                                 else
                                 {
                                     sbFactory.Append(tbid);
                                     sbFactory.Append("='" + tbtext + "', ");
                                 }
                             }
                             else if (tb.ID.Contains("Company_Directors"))
                             {
                                 if (tbid == "fGPtbl_Company_Directors_StateIdD" || tbid == "fGPtbl_Company_Directors_CityIdD")
                                 {
                                 }
                                 else
                                 {
                                     sbDirector.Append(tbid);
                                     sbDirector.Append("='" + tbtext + "', ");
                                 }
                             }
                         }//2
                     }//3
                 }//4
             }//5




             if (sbCompany.Length > 0)
             {
                 companystr = sbCompany.Remove(sbCompany.Length - 2, 2).ToString();
             }

             if (sbFactory.Length > 0)
             {
                 factorystr = sbFactory.Remove(sbFactory.Length - 2, 2).ToString();
             }
             if (sbDirector.Length > 0)
             {
                 directorstr = sbDirector.Remove(sbDirector.Length - 2, 2).ToString();
             }
             int Unit_ID = 0;
             Unit_ID = Unitid;
             int DirctorId = 0;//Director_id;
             DirctorId = Director_id;

             clsRenewal objRenewal = new clsRenewal();
             // int successId = objRenewal.CompanyRenewal_InsertMissingValueByAdmin(companystr, factorystr, directorstr);
             int SuccessId = objRenewal.CompanyRenewal_InsertMissingValueByAdmin(companystr, factorystr, directorstr, companyid, Unit_ID, DirctorId);
             if (SuccessId == 2)
             {
                 //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Data Update Sucessfully!!!!');window.opener.location.reload(true);</script>"); 
                 // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Data Inserted Sucessfully!!!!');window.location.reload(true);</script>");
                 lblPANmsg.Text = "Data Update Sucessfully!!!!";
                 Response.Redirect("MissingCompanyList.aspx");
             }
             else
             {
                 //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Server Busy!!!!');window.location.reload(true);</script>");  
                 lblPANmsg.Text = "Server Busy!!!!";
                 Response.Redirect("MissingCompanyList.aspx");
             }

         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
    }

}
