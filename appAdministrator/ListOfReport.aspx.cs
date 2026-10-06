using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.Linq;
using System.ComponentModel;
using System.Text.RegularExpressions;

public partial class appAdministrator_ListOfReport : System.Web.UI.Page
{
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsCompany objcompany;
    clsDDL objddl;
    DataSet Myds;
    int accessId;
    int count = 1;
    decimal totalprice = 0; 
    int totalItems = 0;
    string dateFrom="";
    string dateto;
    int BranchID;
    int ZoneID;
    int CompanyId;
    int CategoryID;
    int SubCategoryID;
    int CastID;
    int FormType_Id;
    DataTable DTZone;
      int UserId, RoleId, DepartmentId;
      string UserFullName;
      decimal Registration_Amount = 0;
      decimal Insepection_amount = 0;
      decimal Amedment_amount = 0;
      decimal Renewal_amount = 0;
      decimal total_ammount = 0;
      string FieldName="";
    // cheking
      int Typeforpayment;
      int m = 1; // For Serial number generate
      //DataTable Temp;
      int Total_No_Of_record=0;
      int TotalPages = 0;
      int Page_size;
      Image sortImage = new Image();
    protected void Page_Load(object sender, EventArgs e)
    {
       //  btn_print.Attributes.Add( ClientScript.RegisterStartupScript( "onclick", "OpenPrintPage();"));
       // btn_print.Attributes.Add("onclick", "OpenPrintPage();");

        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 1 || RoleId == 5 || RoleId == 10)
            {

                if (Page.IsPostBack !=true)
                {
                    ddlBranchWise.Items.Insert(0, new ListItem("--Select Branch--", ""));
                     ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", ""));

                    BindZone();
                   
                    funfill_ddlCategory();
                    Bind_All_Control();
                  //  Update_bton.Update();
                    Bind_ddl_custom_paging();
                   
                }
            }
            else if(RoleId==2 || RoleId==3 || RoleId==4)
                {
                    //ddlBranchWise.Items.Insert(0, new ListItem("--Select Branch--", ""));
                    //ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", ""));
                    ddlZonewise.Attributes.Add("readonly", "readonly");
                    BindZoneByID();
                   
                    //Update_bton.Update();
                    Bind_ddl_custom_paging();
                    
                }

           
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

            
      
    }


    public void BindZoneByID()
    {
        try
        {
            objcompany = new clsCompany();
            DataTable dtDropdown = new DataTable();
            dtDropdown = objcompany.BindDropdownlist(UserId, RoleId);
            if (dtDropdown.Rows.Count > 0)
            {
                ddlZonewise.DataTextField = dtDropdown.Rows[0]["ZoneName"].ToString();
                ddlZonewise.DataValueField = dtDropdown.Rows[0]["ZoneID"].ToString();
                ddlZonewise.DataSource = dtDropdown;
                ddlZonewise.DataBind();
            }
            else
            {
                ZoneID = 0;
                dtDropdown.Dispose();

            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void BindZone()
    {
        try
        {

            DTZone = new DataTable();

            DTZone = clsDDL.ddlZone_Activated();

            ddlZonewise.DataTextField = DTZone.Columns[1].Caption;
            ddlZonewise.DataValueField = DTZone.Columns[0].Caption;
            ddlZonewise.DataSource = DTZone;
            ddlZonewise.DataBind();
            ddlZonewise.Items.Insert(0, new ListItem("--Select Zone--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        
    }



    public void BindType()
    {
    }

    protected void ddlZonewise_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlZonewise.SelectedValue != "")
            {
                int ZoneId = Convert.ToInt32(ddlZonewise.SelectedValue);
                DTZone = new DataTable();
                DTZone = clsDDL.ddlBranch_Activated(ZoneId);
                ddlBranchWise.DataTextField = DTZone.Columns[4].Caption;
                ddlBranchWise.DataValueField = DTZone.Columns[0].Caption;
                ddlBranchWise.DataSource = DTZone;
                ddlBranchWise.DataBind();
                ddlBranchWise.Items.Insert(0, new ListItem("--Select Branch--", ""));
                //ddlZonewise.AutoPostBack = false;
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



    protected void ddlBranchWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlBranchWise.SelectedValue != "")
            {
                int BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
                DTZone = new DataTable();
                Myds = new DataSet();
                objcompany = new clsCompany();

                Myds = objcompany.Company_ddl_getCompany_Dataset(BranchID);
                DTZone = Myds.Tables[0];
                ddlcomapnywise.DataTextField = DTZone.Columns["fGPtbl_Company_Name"].Caption;
                ddlcomapnywise.DataValueField = DTZone.Columns["fGPtbl_Company_Id"].Caption;
                ddlcomapnywise.DataSource = DTZone;
                ddlcomapnywise.DataBind();
                ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", "0"));
                // ddlZonewise.AutoPostBack = false;
                ddlBranchWise.AutoPostBack = true;

                BindStatewise(BranchID);


            }
            else
            {
                ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", "0"));
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void ddlcomapnywise_SelectedIndexChanged(object sender, EventArgs e)
    {
      //  ddlBranchWise.AutoPostBack = false;
    }


    public void Bind_All_Control()
   {
        try
        {
            Selection_crietria.Visible = false;
          //  lst_stauswise_div.Visible = false;
            tr_search_company.Visible = false;
            txt_search_company.Text = "";

            user_list_type.Visible = false;
            Remainder_Type_Div.Visible = false;

            DateWise.Visible = false;

            Zonewise.Visible = false; ;

            BranchWise.Visible = false; ;
               // ddlBranchWise.SelectedValue = "0";
            ddlBranchWise.Items.Clear();
            ddlBranchWise.Items.Insert(0, new ListItem("--Select Branch--", "0"));
             //ddlZonewise.SelectedValue="0";
            ddlZonewise.Items.Clear();
            ddlZonewise.Items.Insert(0, new ListItem("--Select Zone--", "0"));
            txt_21.Text = "";
            TextBox1.Text = "";
            lbl_total_record.Text = "";
            State_wise_Report.Visible = false;
            ddl_state_wise.Items.Clear();
            ddl_state_wise.Items.Insert(0, new ListItem("--Select State--", ""));
            City_wise_report.Visible = false;
            Ddl_City_Wise.Items.Clear();
            Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
            Turn_over_wise_tr.Visible = false;
            txt_turn_over_wise.Text = "";
            txt_turnover_wise_to.Text = "";
            tr_monetary_limit.Visible=false;
            txt_monetary_limit_from.Text="";
            txt_monetarylimit_to.Text = "";
            CompanyWise.Visible = false;
            CompanyWise.Visible = false;
            Remainder_Type.Visible = false;
            user_list.Visible = false;
            lst_type.Visible = false;
            lst_statuswise.Visible = false;
            Report_Page.Visible = false;
            print_button.Visible = false;
            btn_Report.Visible = false;
            ddlcomapnywise.Items.Clear();
             ddlstatuswise.Items.Clear();
            ddlstatuswise.Items.Insert(0, new ListItem("--Select--", "0"));
            Cateteogry_wise.Visible = false;
            DivCategoryWise.Visible = false;
            ddl_category_wise_report.Items.Clear();
            ddl_category_wise_report.Items.Insert(0, new ListItem("--Select Category Area--", "0"));
         // ddl_category_wise_report.SelectedItem.Text ="--Select Category Area--";

            Sub_Category_wise_ddl.Visible = false;
            Div_SubCategoryWise.Visible = false;
            Ddl_category_cast.Items.Clear();
            Ddl_category_cast.Items.Insert(0, new ListItem("--Select Category Cast--", "0"));
            //Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

            Category_unit_size.Visible = false;
            Div_Category_unit_size.Visible = false;
            DdlCategoryUnitSize.Items.Clear();
            DdlCategoryUnitSize.Items.Insert(0, new  ListItem("--Select Category Unit Size--","0"));
           // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
            tr_order_by.Visible = false;
            ddl_custom_paging.Items.Clear();
            ddl_dropdownlist_custompageing.Items.Clear();
            Bind_ddl_custom_paging();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
   
    }

    protected void ddlSelectCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            print_button.Visible = false;


            if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment" || ddlSelectCriteria.SelectedItem.Text == "Renewal")
            {
                try
                {

                    //  lst_stauswise_div.Visible = false;
                    txtName.Text = "";
                    Bind_All_Control();
                    Update_div.Update();
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = true;
                    txt_search_company.Text = "";
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    // DateWise_div.Visible = true;
                    DateWise.Visible = true;
                    // Zonwise_Div.Visible = true;
                    Zonewise.Visible = true;
                    //Branchwise_div.Visible = true;
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = true;
                    Div_companyWise.Visible = false;
                    CompanyWise.Visible = false;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = false;
                    btn_Report.Visible = true;
                    Report_Page.Visible = false;
                    ddlcomapnywise.Items.Clear();
                    // ddlstatuswise.SelectedItem.Text = "--Select--";

                    Cateteogry_wise.Visible = true;
                    DivCategoryWise.Visible = true;
                    // ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";
                    Sub_Category_wise_ddl.Visible = true;
                    Div_SubCategoryWise.Visible = true;
                    //Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                    Category_unit_size.Visible = true;
                    Div_Category_unit_size.Visible = true;
                    tr_order_by.Visible = true;
                    State_wise_Report.Visible = true;
                    City_wise_report.Visible = true;
                    Turn_over_wise_tr.Visible = true;
                    tr_monetary_limit.Visible = true;

                    DetailsView1.PageIndex = 0;
                    ddl_dropdownlist_custompageing.Visible = true;
                    ddl_custom_paging.Visible = true;
                    BindZone();
                    
                    funfill_ddlCategory();
                    //  DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                    //  lblReport.Text = "Registration Forms Filled";

                }


                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);

                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "--Select--")
            {
                try
                {
                    Selection_crietria.Visible = false;
                    txtName.Text = "";
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = false;
                    txt_search_company.Text = "";
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;

                    DateWise.Visible = false;

                    Zonewise.Visible = false; ;

                    BranchWise.Visible = false; ;
                    Cateteogry_wise.Visible = false;
                    DivCategoryWise.Visible = false;
                    //  ddl_category_wise_report.SelectedItem.Text = "--Select Category Area-";

                    Sub_Category_wise_ddl.Visible = false;
                    Div_SubCategoryWise.Visible = false;
                    //Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                    Category_unit_size.Visible = false;
                    Div_Category_unit_size.Visible = false;
                    // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                    tr_order_by.Visible = false;
                    
                    CompanyWise.Visible = false;
                    CompanyWise.Visible = false;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = false;
                    Report_Page.Visible = false;
                    print_button.Visible = false;
                    btn_Report.Visible = false;
                    Bind_All_Control();
                    BindZone();
                    State_wise_Report.Visible = false;
                    City_wise_report.Visible = false;
                    Turn_over_wise_tr.Visible = false;
                    tr_monetary_limit.Visible = false;
                    ddl_dropdownlist_custompageing.Visible = false;
                    ddl_custom_paging.Visible = false;
                  
                    funfill_ddlCategory();
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                try
                {
                    DetailsView1.PageIndex = 0;
                    // lst_stauswise_div.Visible = false;
                    Bind_All_Control();
                    txtName.Text = "";
                    tr_search_company.Visible = true;
                    txt_search_company.Text = "";
                   // Bind_status_wise();
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    // DateWise_div.Visible = true;
                    DateWise.Visible = true;
                    // Zonwise_Div.Visible = true;
                    Zonewise.Visible = true;
                    //Branchwise_div.Visible = true;
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = true;
                    Div_companyWise.Visible = false;
                    CompanyWise.Visible = false;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = true;
                    provisionalReport.Visible = true;
                    btn_Report.Visible = true;
                    Report_Page.Visible = false;
                    Cateteogry_wise.Visible = true;
                    DivCategoryWise.Visible = true;
                    //ddl_category_wise_report.SelectedItem.Text = "--Select Category Area-";

                    Sub_Category_wise_ddl.Visible = true;
                    Div_SubCategoryWise.Visible = true;
                    //Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                    Category_unit_size.Visible = true;
                    Div_Category_unit_size.Visible = true;
                    //DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                    tr_order_by.Visible = true;
                    BindZone();
                    State_wise_Report.Visible = true;
                    City_wise_report.Visible = true;
                    Turn_over_wise_tr.Visible = true;
                    tr_monetary_limit.Visible = true;
                    ddl_dropdownlist_custompageing.Visible = true;
                    ddl_custom_paging.Visible = true;
                    Detailedviewid.Visible=true;

                    detailview1_td.Visible = true;
                    funfill_ddlCategory();
                    Bind_status_wise();

                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                try
                {
                    txtName.Text = "";
                    // lst_stauswise_div.Visible = false;
                    Bind_All_Control();
                    //  ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", ""));
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = false;
                    txt_search_company.Text = "";
                    // DateWise_div.Visible = true;
                    DateWise.Visible = true;
                    // Zonwise_Div.Visible = true;
                    Zonewise.Visible = true;
                    //Branchwise_div.Visible = true;
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = true;
                    Div_companyWise.Visible = true;
                    CompanyWise.Visible = true;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = true;
                    lst_statuswise.Visible = false;
                    btn_Report.Visible = true;

                    Report_Page.Visible = false;
                    Cateteogry_wise.Visible = false;
                    DivCategoryWise.Visible = false;
                    //  ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                    Sub_Category_wise_ddl.Visible = false;
                    Div_SubCategoryWise.Visible = false;
                    //Ddl_category_cast.SelectedItem.Text=  "--Select Category Cast--";

                    Category_unit_size.Visible = false;
                    Div_Category_unit_size.Visible = false;
                    /// DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                    tr_order_by.Visible = true;
                    lbl_search_company_name.Visible=false;
                    txtName.Visible=false;
                   cmdSearch.Visible=false;
                   cmdClear.Visible = false;
                   BindZone();
                   State_wise_Report.Visible =false;
                   City_wise_report.Visible = false;
                   Turn_over_wise_tr.Visible = false;
                   tr_monetary_limit.Visible = false;
                
                   funfill_ddlCategory();
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "User List")
            {
                try
                {
                    txtName.Text = "";
                    Bind_All_Control();
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    // DateWise_div.Visible = true;
                    DateWise.Visible = true;
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = false;
                    txt_search_company.Text = "";
                    // Zonwise_Div.Visible = true;
                    Zonewise.Visible = true;
                    //Branchwise_div.Visible = true;
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = true;
                    Div_companyWise.Visible = false;
                    CompanyWise.Visible = false;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = false;
                    btn_Report.Visible = true;
                    Report_Page.Visible = false;
                    ddlcomapnywise.Items.Clear();
                    //  ddlstatuswise.SelectedItem.Text = "--Select--";
                    Cateteogry_wise.Visible = false;
                    DivCategoryWise.Visible = false;
                    //   ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                    Sub_Category_wise_ddl.Visible = false;
                    Div_SubCategoryWise.Visible = false;
                    //  Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                    Category_unit_size.Visible = false;
                    Div_Category_unit_size.Visible = false;
                    tr_order_by.Visible = false;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;
                    BindZone();
                    State_wise_Report.Visible = false;
                    City_wise_report.Visible = false;
                    Turn_over_wise_tr.Visible = false;
                    tr_monetary_limit.Visible = false;
                    ddl_dropdownlist_custompageing.Visible = false;
                    ddl_custom_paging.Visible = false;
                    //   DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }


            }


            else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
            {
                try
                {
                    txtName.Text = "";
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    DateWise.Visible = true;
                    Zonewise.Visible = true;
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = false;
                    txt_search_company.Text = "";
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = true;
                    Div_companyWise.Visible = false;
                    CompanyWise.Visible = false;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = false;
                    btn_Report.Visible = true;
                    Report_Page.Visible = false;
                    ddlcomapnywise.Items.Clear();
                    //ddlstatuswise.SelectedItem.Text = "--Select--";
                    Cateteogry_wise.Visible = false;
                    DivCategoryWise.Visible = false;
                    //   ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                    Sub_Category_wise_ddl.Visible = false;
                    Div_SubCategoryWise.Visible = false;
                    // Ddl_category_cast.SelectedItem.Text= "--Select Category Cast--";

                    Category_unit_size.Visible = false;
                    Div_Category_unit_size.Visible = false;
                    // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;
                    BindZone();
                    State_wise_Report.Visible = false;
                    City_wise_report.Visible = false;
                    Turn_over_wise_tr.Visible = false;
                    tr_monetary_limit.Visible = false;
                  
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }
            }

            else if (ddlSelectCriteria.SelectedItem.Text == "Duplicate Certificate Print Request")
            {
                try
                {
                    txtName.Text = "";
                    Bind_All_Control();
                    provisionalReport.Visible = false;
                    tr_search_company.Visible = false;
                    txt_search_company.Text = "";
                    user_list_type.Visible = false;
                    Remainder_Type_Div.Visible = false;
                    DateWise.Visible = true;
                    Zonewise.Visible = true;
                    BranchWise.Visible = true;
                    Selection_crietria.Visible = false;
                    Div_companyWise.Visible = true;
                    CompanyWise.Visible = true;
                    Remainder_Type.Visible = false;
                    user_list.Visible = false;
                    lst_type.Visible = false;
                    lst_statuswise.Visible = false;
                    btn_Report.Visible = true;
                    Report_Page.Visible = false;
                    ddlcomapnywise.Items.Clear();
                    // ddlstatuswise.SelectedItem.Text = "--Select--";
                    Cateteogry_wise.Visible = false;
                    DivCategoryWise.Visible = false;
                    //  ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                    Sub_Category_wise_ddl.Visible = false;
                    Div_SubCategoryWise.Visible = false;
                    // Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                    Category_unit_size.Visible = false;
                    Div_Category_unit_size.Visible = false;
                    tr_order_by.Visible = true;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;
                    BindZone();
                    State_wise_Report.Visible = false;
                    City_wise_report.Visible = false;
                    Turn_over_wise_tr.Visible = false;
                    tr_monetary_limit.Visible = false;
                    ddl_dropdownlist_custompageing.Visible = false;
                    ddl_custom_paging.Visible = false;
                    // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message);
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    public void BIND_Detailesview()
    {

        try
        {
            lbl_show_message.Text = "";
            string Status;
            string FormType;
            string  orderby="1";
            string turn_over_from = " ";
            string turn_over_to = " ";
            string Monetary_limit_from = " ";
            string monetary_limit_to = " ";
            int State_id = 0;
            int City_id = 0;
            string SearchCompany_name = "";
            if (ddl_state_wise.SelectedValue == "0" || ddl_state_wise.SelectedValue=="")
            {
                State_id = 0;
            }
            else
            {
                State_id = Convert.ToInt32(ddl_state_wise.SelectedValue.ToString());
            }
            if (Ddl_City_Wise.SelectedValue == "0" || Ddl_City_Wise.SelectedValue=="")
            {
                City_id = 0;
            }
            else
            {
                City_id = Convert.ToInt32(Ddl_City_Wise.SelectedValue.ToString());
            }
            if (txt_turn_over_wise.Text == "")
            {
                turn_over_from = " ";
            }
            else
            {
                turn_over_from = txt_turn_over_wise.Text.ToString();
            }
            if (txt_turnover_wise_to.Text == "")
            {
                turn_over_to = " ";
            }
            else
            {
                turn_over_to = txt_turnover_wise_to.Text.ToString();
            }
            if (txt_monetary_limit_from.Text == "")
            {
                Monetary_limit_from = " ";
            }
            else
            {
                Monetary_limit_from = txt_monetary_limit_from.Text.ToString();
            }
            if (txt_monetarylimit_to.Text == "")
            {
                monetary_limit_to = " ";
            }
            else
            {
                  monetary_limit_to=txt_monetarylimit_to.Text.ToString();
            }
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                orderby = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                orderby = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                orderby = "3";
            }

            if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
            {
                FormType = "3";
            }
            else
            {
                FormType = "1";
            }
            //dateFrom = txt_21.Text.ToString();
            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();
            if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
            {
                BranchID = 0;
            }
            else
            {
                BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
            }
            if (ddlZonewise.SelectedValue == "")
            {
                ZoneID = 0;

            }
            else
            {
                ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            }
            if (ddl_category_wise_report.SelectedItem.Text == "--Select Category Area--")
            {
                CategoryID = 0;
            }
            else
            {
                CategoryID = Convert.ToInt32(ddl_category_wise_report.SelectedValue);
            }
            if (Ddl_category_cast.SelectedItem.Text == "--Select Category Cast--")
            {
                SubCategoryID = 0;
            }
            else
            {
                SubCategoryID = Convert.ToInt32(Ddl_category_cast.SelectedValue);
            }
            if (DdlCategoryUnitSize.SelectedItem.Text == "--Select Category Unit Size--")
            {
                CastID = 0;
            }
            else
            {
                CastID = Convert.ToInt32(DdlCategoryUnitSize.SelectedValue);
            }
            if (txt_search_company.Text == "")
            {
                SearchCompany_name = "";
            }
            else
            {
                SearchCompany_name = txt_search_company.Text.Trim().ToString();
            }

             Status = ddlstatuswise.SelectedItem.Text;
          /*  if (Status == "Registered")
            {
                DetailsView1.Columns[1].HeaderText = "Registration Date";

                BoundField boundField = new BoundField();

            }
            else
            {
                DetailsView1.Columns[1].HeaderText = DetailsView1.Columns[1].HeaderText;

            }*/

            /// Status = ddlstatuswise.SelectedItem.Text;


            objcompany = new clsCompany();
            DataSet dsgridview = new DataSet();
           
            if (ddlSelectCriteria.SelectedItem.Text == "Renewal")
            {
                dsgridview = objcompany.Company_WaitingCompany_Renewal_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name, orderby);

            }

            else if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
            {

                dsgridview = objcompany.Company_WaitingCompany_Amedment_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id,City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name, orderby);


            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" )
            {
               
                dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name ,orderby);
                // dsgridview.Tables[0].DefaultView.Sort = "fGPtbl_Company_Name DESC";

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                if (ddlstatuswise.SelectedValue == "--Select--" && DdlProvisinal.SelectedValue == "0")
                {
                    dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name, orderby);
                }




                else
                {

                    string strExpr;// change if registerd hai 
                    if (DdlProvisinal.SelectedValue == "0")
                    {
                        strExpr = "Provisional=0";
                        dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name, orderby);
                       // var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).Count();
                        var count1 = dsgridview.Tables[0].AsEnumerable().Count();
                        if (count1 > 0)
                        {
                            // dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0);
                            dsgridview.Tables[0].AsEnumerable();
                        }
                        else
                        {
                            dsgridview.Dispose();
                        }
                    }
                    else
                    {
                        try
                        {
                            strExpr = "Provisional=1";
                            dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID, SearchCompany_name, orderby);
                            var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).Count();
                            if (count1 > 0)
                            {
                                dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1);
                            }
                            else
                            {
                                dsgridview.Dispose();
                            }



                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "";
                            lbl_total_record.Text = "0";
                            DetailsView1.Dispose();
                            DetailsView1.DataBind();
                            dsgridview.Dispose();
                            Report_Type.Text = "N0 Record Found!!!";
                            print_button.Visible = false;
                            tr_print_button.Visible = false;
                            Report_Page.Attributes.Add("class", "displayshowhide");
                            print_button.Attributes.Add("class", "displayshowhide");
                          //  Label lbl_show_data = new Label();
                            lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                        }

                    }

                }
            }  //  if

            if (Status != "--Select--")
            {
                DataTable dt = new DataTable();
                // string strExpr;
                 if (DdlProvisinal.SelectedValue == "0")
                 {
                    
                     dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to,   CategoryID, SubCategoryID, CastID, SearchCompany_name ,orderby);
                    // var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).Count();
                     var count1 = dsgridview.Tables[0].AsEnumerable().Count();
                     if (count1 > 0)
                     {
                        // dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0);
                         dsgridview.Tables[0].AsEnumerable();
                       //  dt = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).CopyToDataTable();
                         dt = dsgridview.Tables[0].AsEnumerable().CopyToDataTable();
                     }
                     else
                     {
                         dsgridview.Dispose();
                     }
                 }
                 else
                 {
                     try
                     {
                         dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_id, City_id, turn_over_from, turn_over_to, Monetary_limit_from, monetary_limit_to, CategoryID, SubCategoryID, CastID,SearchCompany_name ,orderby);
                         var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).Count();
                         if (count1 > 0)
                         {
                             dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1);
                             dt = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).CopyToDataTable();
                         }
                         else
                         {
                             dsgridview.Dispose();
                         }
                     }
                     catch (Exception ex)
                     {
                         lblMessage.Text = "";
                         lbl_total_record.Text = "0";
                         DetailsView1.Dispose();
                         DetailsView1.DataBind();
                         dsgridview.Dispose();
                         Report_Type.Text = "N0 Record Found!!!";
                         Report_Page.Attributes.Add("class", "displayshowhide");
                         print_button.Attributes.Add("class", "displayshowhide");
                        //  Label lbl_show_data = new Label();
                          lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";

	

                         print_button.Visible = false;
                         tr_print_button.Visible = false;
                     }
                 }

                 // dt.DefaultView.Sort = "fGPtbl_Company_Id DESC";DetailsView1

                 try
                 {

                  //   DataTable DtfilterShowTable = new DataTable();
                    // DtfilterShowTable = dt.AsEnumerable().CopyToDataTable();
                 }
                 catch { }
                //dt.DefaultView.Sort = FieldName+" " + "DESC";
                if (dt.Rows.Count > 0)
                {

                    DataTable dtFilteredTable = new DataTable();
                    var count = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status).Count();
                    int countStatus = Convert.ToInt32(count);
                    if (count > 0)
                    {
                        dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status).OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                       
                        lblMessage.Text = "";
                        lbl_total_record.Text = dtFilteredTable.Rows.Count.ToString();
                       
                        
                        // For Sorting 

                        Temp = dtFilteredTable.Copy();
                      //  ViewState["Temp"] = null;
                        DataView dv = new DataView(dtFilteredTable);
                        if (dv.Count != 0)
                        {
                            if (!this.SortField.Equals(String.Empty))
                            {
                                string strDirect = String.Empty;
                                if (this.SortDirection_1.Equals("D"))
                                    strDirect = " DESC";

                                dv.Sort = SortField + strDirect;
                            }
                          
                            DetailsView1.DataSource = dv;
                            DetailsView1.DataBind();
                          //  udtedate_griddview.Update();
                        }
                        else
                        {
                            Temp.Dispose();
                            dv.Dispose();
                            DetailsView1.DataBind();
                        }
                        
                        
                        
                        // End Of Sorting
                        
                        
                        
                        //DetailsView1.DataSource = dtFilteredTable;
                        //DetailsView1.DataBind();


                        Report_Page.Attributes.Add("class", "dispalyshowhidegrid");
                        print_button.Attributes.Add("class", "dispalyshowhidegrid");
                        
                        tr_print_button.Visible = true;
                    }
                    else
                    {
                        lblMessage.Text = "";
                        lbl_total_record.Text = "0";
                        DetailsView1.Dispose();
                        DetailsView1.DataBind();
                        dsgridview.Dispose();
                        Report_Type.Text = "N0 Record Found!!!";
                        print_button.Visible = false;
                        tr_print_button.Visible = false;
                        Report_Page.Attributes.Add("class", "displayshowhide");
                        print_button.Attributes.Add("class", "displayshowhide");

                        lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                        lbl_show_message.Visible = true;
                    }
                }
                else
                {
                    Report_Type.Text = "No Record Found!!!";
                    lbl_total_record.Text = "0";
                    DetailsView1.Dispose();
                    DetailsView1.DataBind();
                    dsgridview.Dispose();
                    //  Report_Type.Text = "";
                    print_button.Visible = false;
                    tr_print_button.Visible = false;
                   // Report_Page.Visible = false;
                    Report_Page.Attributes.Add("class", "displayshowhide");
                    print_button.Attributes.Add("class", "displayshowhide");
                   // print_button.Visible = false;
                    //Label lbl_show_data = new Label();
                    lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                }



            }

            else
            {
                if (dsgridview.Tables[0].Rows.Count > 0)
                
                
                                {
                    lblMessage.Text = "";
                    // For Amdemnet and Registration 
                    DataTable dtFilterReg = new DataTable();
                    if (ddlSelectCriteria.SelectedItem.Text == "Companies List")
                    {  // chenge here 

                        if (ddlstatuswise.SelectedValue == "--Select--" && DdlProvisinal.SelectedValue == "0")
                        {
                            dtFilterReg = dsgridview.Tables[0].AsEnumerable().CopyToDataTable(); 
                        }

                        else
                        {
                        if (DdlProvisinal.SelectedValue == "0")
                        {
                           var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).Count();
                            //var count1 = dsgridview.Tables[0].AsEnumerable().Count();
                            if (count1 > 0)
                            {
                              // dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0);
                                dsgridview.Tables[0].AsEnumerable();
                               // dtFilterReg = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).CopyToDataTable();
                                dtFilterReg = dsgridview.Tables[0].AsEnumerable().CopyToDataTable();
                            }
                            else
                            {
                                dtFilterReg.Dispose();
                            }
                        }
                        else
                        {
                            var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).Count();
                            if (count1 > 0)
                            {
                                dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1);
                                dtFilterReg = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).CopyToDataTable();
                            }
                            else
                            {
                                dtFilterReg.Dispose();
                            }
                        }

                    }// band
                    }
                    else
                    {
                        dtFilterReg = dsgridview.Tables[0];
                    }
                   
                //    dtFilterReg.DefaultView.Sort = "fGPtbl_Company_Id DESC";
                    if (dtFilterReg.Rows.Count > 0)
                    {
                        
                        var count = dtFilterReg.AsEnumerable().Count();
                        int countStatus = Convert.ToInt32(count);
                        if (count > 0)
                        {
                            DataTable dtFilterRegFilter = new DataTable();
                            if (FormType == "3")
                            {
                                dtFilterRegFilter = dtFilterReg.AsEnumerable().OrderByDescending(a => a.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                            }
                            else
                            {
                                dtFilterRegFilter = dtFilterReg.AsEnumerable().CopyToDataTable();
                            }
                            lblMessage.Text = "";
                            lbl_total_record.Text = dtFilterRegFilter.Rows.Count.ToString();
                            // for sorting

                                Temp = dtFilterRegFilter.Copy();
                               // ViewState["Temp"] = null;
                            DataView dv = new DataView(dtFilterRegFilter);
                            if (dv.Count != 0)
                            {
                                if (!this.SortField.Equals(String.Empty))
                                {
                                    string strDirect = String.Empty;
                                    if (this.SortDirection_1.Equals("D"))
                                        strDirect = " DESC";

                                    dv.Sort = SortField + strDirect;
                                }
                             
                                DetailsView1.DataSource = dv;
                                DetailsView1.DataBind();
                               // udtedate_griddview.Update();
                            }

                            //DetailsView1.DataSource = dtFilterRegFilter;
                            //DetailsView1.DataBind();

                           // End Of Sorting
                          //  DetailsView1.PageIndex = 0;
                            Report_Page.Attributes.Add("class", "dispalyshowhidegrid");
                            print_button.Attributes.Add("class", "dispalyshowhidegrid");
                            tr_print_button.Visible = true;
                            Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                            // for sorting

                           Temp = dtFilterRegFilter.Copy();
                          // ViewState["Temp"] = null;
                        }
                        else
                        {
                            // lblMessage.Text = "N0 Record Found!!!";
                            Report_Type.Text = "No Record Found!!!";
                            lbl_total_record.Text = "0";
                            DetailsView1.Dispose();
                            DetailsView1.DataBind();
                            dsgridview.Dispose();
                           

                            // Report_Type.Text = "";
                            print_button.Visible = false;
                            tr_print_button.Visible = false;
                            Report_Page.Attributes.Add("class", "displayshowhide");
                            print_button.Attributes.Add("class", "displayshowhide");

                          //  Label lbl_show_data = new Label();
                            lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                            
                        }
                    }
                    else
                    {
                        dtFilterReg.Dispose();
                        Temp.Dispose();
                    }

                }
                else
                {
                    Report_Type.Text = "N0 Record Found!!!";
                    lbl_total_record.Text = "0";
                    lblMessage.Text = "";
                    DetailsView1.Dispose();
                    DetailsView1.DataBind();
                    dsgridview.Dispose();
                    Temp.Dispose();
                    print_button.Visible = false;
                    tr_print_button.Visible = false;
                   
                    Report_Page.Attributes.Add("class", "displayshowhide");
                    print_button.Attributes.Add("class", "displayshowhide");
                  
                   // Label lbl_show_data = new Label();
                    lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";

                }
                if(ddl_custom_paging.SelectedValue!="10")
                {

                DetailsView1.PageSize = Convert.ToInt32( ddl_custom_paging.SelectedValue);
                }
                else
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);  
                }
             
            }

            // For Sorting Table
           // DataTable dtgridview = new DataTable();
           // dtgridview = dsgridview.Tables[0];
            //Temp = dtgridview.Copy();
           
           


            //End Of Sorting table
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


  

    protected void Report_PageChanging(object sender, DetailsViewPageEventArgs e)
    {
        try
        {
            DetailsView1.PageIndex = e.NewPageIndex;
            BIND_Detailesview();
           
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

   
   
    protected void Report_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
           
            string imgAsc = @"<img src='../Images/up_arrow.png' border='0' title='Ascending' />";
            string imgDes = @" <img src='../Images/down_arrow.png' border='0' title='Descendng' />";
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
                {
                    // Application Date/Validity
                    e.Row.Cells[3].Text = "Registration No./Validity";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

               // Label lblSerial = (Label)e.Row.FindControl("lblSerial");
               //lblSerial.Text = ((DetailsView1.PageIndex * DetailsView1.PageSize) + e.Row.RowIndex + 1).ToString();
               // lblSerial.Text = m.ToString();
               // m++;
                string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = decodedText;
                e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Width, "350px");
                string DecodeText2 = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = DecodeText2;
               // e.Row.Cells[4].Text = HttpUtility.HtmlDecode(e.Row.Cells[4].Text);
                e.Row.Cells[3].Text = HttpUtility.HtmlDecode(e.Row.Cells[3].Text);
                string Decodetext_applicationdate = HttpUtility.HtmlDecode(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = Decodetext_applicationdate.ToString();
                Label lblcompany_name = (Label)e.Row.FindControl("LblComapnY_Name");
                Label lblCompanyId = (Label)e.Row.FindControl("lblCompany_Id");
                Label lblBranchName = (Label)e.Row.FindControl("lblBranch_Name");
                int Compny_Id = Convert.ToInt32(lblCompanyId.Text);
                string Company_Name = Convert.ToString(lblcompany_name.Text);
                Label lbluid = (Label)e.Row.FindControl("lblunitid");
                int Unit_ID = Convert.ToInt32(lbluid.Text);

                Label lblFormtype = (Label)e.Row.FindControl("lblFormtype");

                if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
                {
                    FormType_Id = 3;
                    DetailsView1.Columns[5].Visible = true;
                }
                else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
                {
                    FormType_Id = 1;//Convert.ToInt32(lblFormtype.Text);
                    DetailsView1.Columns[5].Visible = true;
                }
                else if (ddlSelectCriteria.SelectedItem.Text == "Renewal")
                {
                    FormType_Id = 4;
                    DetailsView1.Columns[5].Visible = false;
                }
                Label lblBranchId = (Label)e.Row.FindControl("lblBranchId");
                int BranchId_Amedment = Convert.ToInt32(lblBranchId.Text.ToString());
                ImageButton btnview = (ImageButton)e.Row.FindControl("imbView");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany_Name = objDecEnc.Encrypt(Company_Name.ToString());
                string EncryptID = objDecEnc.Encrypt(Unit_ID.ToString());
                string EncryptCompanyID = objDecEnc.Encrypt(Compny_Id.ToString());
                string EncryptBranchName = objDecEnc.Encrypt(lblBranchName.Text.ToString());
                string EncryptFormType = objDecEnc.Encrypt(FormType_Id.ToString());
                string EncryptBranchid = objDecEnc.Encrypt(BranchId_Amedment.ToString());

                // this code is useless 
                HyperLink hyper = (HyperLink)e.Row.FindControl("HLName");
                // hyper.NavigateUrl = "ReportViewPage.aspx?id=" + Unit_ID + "&Com_name=" + Company_Name + "&Company_ID=" +Compny_Id;
                if (FormType_Id == 1)
                {
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    hyper.Text = "view";
                }
                else if (FormType_Id == 3)
                {

                    string EncryptStatus = objDecEnc.Encrypt("1");
                    string EncryptUrlId = objDecEnc.Encrypt("999");
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                   // hyper.NavigateUrl = "UnitAmendment_ViewChanges.aspx?c=" + EncryptCompanyID + "&u=" + EncryptID + "&s=" + EncryptStatus + "&b=" + EncryptBranchid + "&i=" + EncryptUrlId;
                    hyper.Text = "view";

                }
                else if (FormType_Id == 4)
                {
                    string EncryptStatus = objDecEnc.Encrypt("4");
                    string EncryptUrlId = objDecEnc.Encrypt("2");
                    // hyper.NavigateUrl = "UnitAmendment_ViewChanges.aspx?c=" + EncryptCompanyID + "&u=" + EncryptID + "&s=" + EncryptStatus + "&b=" + EncryptBranchid + "&i=" + EncryptUrlId;
                    //hyper.NavigateUrl = "RenewalformField.aspx?id=" + EncryptID + "&Com_Name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                    hyper.Text = "view";
                }




            }

         /*  if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    LinkButton lnk_company = (LinkButton)cell.Controls[1];
                    if (lnk_company.Text == "Company")
                    {
                        if (DetailsView1.SortDirection == SortDirection.Ascending)
                        {
                            lnk_company.Text += imgAsc;
                        }
                        else
                        {
                            lnk_company.Text += imgDes;
                        }
                    }

                }
            }*/
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void DetailsViewPageindexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
           
            DetailsView1.PageIndex = e.NewPageIndex;
           // DetailsView1.DataBind();
             BIND_Detailesview();
            DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedItem.Text);
        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
        }
       
     
    }

    protected void DetailsView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {
                Response.Redirect("WaitingCompanies_View.aspx?id=" + e.CommandArgument, false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btn_report_ReportPage_Click1(object sender, EventArgs e)  // cahnges Ravish Kumar pandey 13 Feb
    {
        try
        {
            
            if (TextBox1.Text != "" && txt_21.Text != "")
            {
                DateTime startdt, enddt;
                startdt = Convert.ToDateTime(TextBox1.Text);
                enddt = Convert.ToDateTime(txt_21.Text);
                if (startdt > DateTime.Now)
                {
                
                    TextBox1.Text = "";
                    txt_21.Text = "";
                 
                    return;
                }
                if (startdt > enddt)
                {
                 
                    TextBox1.Text = "";
                    txt_21.Text = "";
                  
                    return;
                }
            }

            if (txt_21.Text.Trim() != "" || TextBox1.Text.Trim() != "" || ddlstatuswise.SelectedItem.Text != "--Select--" || ddlBranchWise.SelectedItem.Text != "--Select Branch--" || ddlZonewise.SelectedItem.Text != "--Select Zone--" || txt_21.Text.Trim() == "" || TextBox1.Text.Trim() == "" || ddl_category_wise_report.SelectedItem.Text == "--Select Category Area--" || Ddl_category_cast.SelectedItem.Text == "--Select Category Cast--" || DdlCategoryUnitSize.SelectedItem.Text == "--Select Category Unit Size--")
            {
                Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
                {
                    if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
                    {
                        Bind_Branch_Wise();
                        DetailsView1.Visible = false;
                        grd_print_certificate.Visible = false;
                        grd_report_paymentDetails.Visible = false;
                        grd_user_list_report.Visible = false;
                        grd_report_paymentDetails.Dispose();
                        grd_Branch_Wise.Visible = true;
                        print_button.Visible = true;
                        lst_report_type.Visible = true;
                        Report_Page.Visible = true;
                        detailview1_td.Visible = false;
                        lbl_show_text.Visible=false;

                        lbl_text_show.Visible = false;
                        Detailedviewid.Visible = false;
                        lbl_search_company_name.Visible=false;
                        txtName.Visible=false;
                        cmdSearch.Visible=false;
                        cmdClear.Visible = false;
                    }

                    else
                    {
                        //if (ddlBranchWise.SelectedItem.Text == "Select Branch--" && ddlcomapnywise.SelectedItem.Text= )
                        //{

                        DetailsView1.Visible = false;
                        if (ddlcomapnywise.SelectedItem.Text != "--Select Company--" || ddlcomapnywise.SelectedItem.Text!="")
                        {
                            Bind_Payment_Details();
                            grd_report_paymentDetails.Visible = true;
                            grd_print_certificate.Visible = false;
                            detailview1_td.Visible = false;

                            Detailedviewid.Visible = false;
                            lbl_search_company_name.Visible = false;
                            txtName.Visible = false;
                            cmdSearch.Visible = false;
                            cmdClear.Visible = false;
                         //   udtedate_griddview.Update();
                        }
                        else
                        {
                            Bind_GridView_BranchZoneWise();
                            grd_report_paymentDetails.Visible = false;
                            grd_Company_wiseData.Visible = true;
                            grd_user_list_report.Visible = false;
                            grd_print_certificate.Visible = false;
                            detailview1_td.Visible = false;
                            lbl_show_text.Visible = false;

                            lbl_text_show.Visible = false;
                            Detailedviewid.Visible = false;
                            lbl_search_company_name.Visible = false;
                            txtName.Visible = false;
                            cmdSearch.Visible = false;
                            cmdClear.Visible = false;
                        }
                        grd_Branch_Wise.Visible = false;
                        print_button.Visible = true;
                        lst_report_type.Visible = true;
                        grd_print_certificate.Visible = false;
                        Report_Page.Visible = true;
                        grd_user_list_report.Visible = false;
                        detailview1_td.Visible = false;

                        Detailedviewid.Visible = false;
                        lbl_search_company_name.Visible = false;
                        txtName.Visible = false;
                        cmdSearch.Visible = false;
                        cmdClear.Visible = false;
                    }
                }
                else if (ddlSelectCriteria.SelectedItem.Text == "User List")
                {

                    DetailsView1.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    grd_report_paymentDetails.Visible = false;
                    grd_inspectionAgency.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    grd_print_certificate.Visible = false;
                    detailview1_td.Visible = false;
                    bindUserList();
                    grd_user_list_report.Visible = true;
                    print_button.Visible = true;
                    lst_report_type.Visible = true;
                    Report_Page.Visible = true;
                    detailview1_td.Visible = false;
                    lbl_show_text.Visible = false;
                    lbl_text_show.Visible = false;
                    Detailedviewid.Visible = false;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;
                }
                else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
                {
                    DetailsView1.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    grd_report_paymentDetails.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    detailview1_td.Visible = false;
                    BindInspectionAgency();
                    grd_user_list_report.Visible = false;
                    grd_print_certificate.Visible = false;
                    grd_inspectionAgency.Visible = true;
                    print_button.Visible = true;
                    lst_report_type.Visible = true;
                    Report_Page.Visible = true;
                    detailview1_td.Visible = false;
                    lbl_show_text.Visible = false;

                    lbl_text_show.Visible = false;
                    Detailedviewid.Visible = false;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;

                }

                else if (ddlSelectCriteria.SelectedItem.Text == "Duplicate Certificate Print Request")
                {
                    DetailsView1.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    grd_report_paymentDetails.Visible = false;
                    grd_Branch_Wise.Visible = false;
                    detailview1_td.Visible = false;
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                    grd_user_list_report.Visible = false;
                    grd_inspectionAgency.Visible = false;
                    print_button.Visible = true;
                    lst_report_type.Visible = true;
                    Report_Page.Visible = true;
                    detailview1_td.Visible = false;
                    lbl_show_text.Visible = false;

                    lbl_text_show.Visible = false;
                    Detailedviewid.Visible = false;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;
                }

                else
                {

                    if (ddlSelectCriteria.SelectedItem.Text != "Companies List")
                    {
                        // ddlstatuswise.SelectedItem.Text = "--Select--";

                    }
                    //ddlType.SelectedItem.Text = "--Select--";

                  //  udtedate_griddview.Update();
                      
                    BIND_Detailesview();

                   // DetailsView1.PageIndex =0;

                  //  DetailsView1.PagerSettings.Position="1";
                    DetailsView1.Visible = true;
                   // DetailsView1.up
                    detailview1_td.Visible = true;
                    lbl_show_text.Visible = true;

                    lbl_text_show.Visible = true;
                    Detailedviewid.Visible = true;
                    grd_report_paymentDetails.Visible = false;
                    print_button.Visible = true;
                    lst_report_type.Visible = true;
                    grd_print_certificate.Visible = false;
                    Report_Page.Visible = true;
                    grd_user_list_report.Visible = false;
                    lbl_search_company_name.Visible = true;
                    txtName.Visible =true;
                    cmdSearch.Visible = true;
                    cmdClear.Visible = true;
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


    public void Bind_Payment_Details()
    {
        try
        {

            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();
            if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
            {
                BranchID = 0;
            }
            else
            {
                BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
            }
            if (ddlZonewise.SelectedValue == "")
            {
                ZoneID = 0;

            }
            else
            {
                ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            }
            if (ddlcomapnywise.SelectedItem.Text == "--Select Company--")
            {
                CompanyId = 0;
            }
            else
            {
                CompanyId = Convert.ToInt32(ddlcomapnywise.SelectedValue);
            }



            if (ddlType.SelectedItem.Text == "--Select--")
            {
                Typeforpayment = 0;
            }
            else
            {
                if (ddlType.SelectedItem.Text == "Amendment")
                {
                    Typeforpayment = 3;
                }
                if (ddlType.SelectedItem.Text == "Renewal")
                {
                    Typeforpayment = 2;
                }
                if (ddlType.SelectedItem.Text == "Inspection")
                {
                    Typeforpayment = 4;
                }

            }




            objcompany = new clsCompany();
            DataSet dsgridviewPayment = new DataSet();

            if (Typeforpayment == 4 || Typeforpayment == 0)
            {
                dsgridviewPayment = objcompany.Company_PaymentDetails_Modified(dateFrom, dateto, BranchID, ZoneID, CompanyId);
            }
            else
            {
                dsgridviewPayment = objcompany.Company_PaymentDetails_Modified_Fetch_By_Formtype(dateFrom, dateto, BranchID, ZoneID, CompanyId, Typeforpayment);
            }
            if (dsgridviewPayment.Tables[0].Rows.Count > 0)
            {

                lbl_total_record.Text = dsgridviewPayment.Tables[0].Rows.Count.ToString();
                grd_report_paymentDetails.DataSource = dsgridviewPayment.Tables[0];
                grd_report_paymentDetails.DataBind();
                grd_report_paymentDetails.PageIndex = 0;
                print_button.Visible = true;
            }
            else
            {
                Report_Type.Text = "N0 Record Found!!!";
                lbl_total_record.Text = "0";
                dsgridviewPayment.Dispose();
                //Report_Type.Text = "";
                print_button.Visible = false;
                tr_print_button.Visible = false;
                grd_report_paymentDetails.Dispose();
                grd_report_paymentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void grd_Report_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

       

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlType.SelectedItem.Text == "Registration")
            {

                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;
                grd_report_paymentDetails.Columns[7].Visible = false;

            }
            else if (ddlType.SelectedItem.Text == "Inspection")
            {
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;
                grd_report_paymentDetails.Columns[7].Visible = false;
            }
            else if (ddlType.SelectedItem.Text == "Amendment")
            {
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible =true;
                grd_report_paymentDetails.Columns[7].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = false;
            }

            else if (ddlType.SelectedItem.Text == "Renewal")
            {

                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = false;
                grd_report_paymentDetails.Columns[6].Visible = true;
                grd_report_paymentDetails.Columns[7].Visible = false;
            }
            Label lblcompanyid = (Label)e.Row.FindControl("lblCompanyID");
            Label lblcompanyName = (Label)e.Row.FindControl("lblcomapnyName");
              int companyid=Convert.ToInt32(lblcompanyid.Text.ToString());
               string CompanyName=lblcompanyName.Text;
               string Page = "Comapnywise";
             string Page2="";
             string url = "CompanyWiseSeparateRecord.aspx?cid=" + companyid + "&cname=" + CompanyName + "&Page=" + Page + "&page1="+Page2;

               lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";   
        }
         if (e.Row.RowType == DataControlRowType.DataRow)
        {
         Label lbl_registration_Amount = (Label)e.Row.FindControl("lblRegiatrationAmmount");
        if (lbl_registration_Amount.Text == "" || lbl_registration_Amount.Text == null)
        {
            lbl_registration_Amount.Text = "0.0";

        }
        Label lbl_inspection_amount = (Label)e.Row.FindControl("lbl_unit_ammount");
        if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
        {
            lbl_inspection_amount.Text = "0.0";
        }
        Label lbl_amedment_amount = (Label)e.Row.FindControl("lblAmedmentAmount");
        if (lbl_amedment_amount.Text == "" || lbl_amedment_amount.Text == null)
        {
            lbl_amedment_amount.Text = "0.0";
        }
        Label lbl_renewal_amount = (Label)e.Row.FindControl("lblRenewalAmount");
        if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
        {
            lbl_renewal_amount.Text = "0.0";
        }
        decimal total;
        Label lbl_total_amount = (Label)e.Row.FindControl("lbltotalamount");    
        total = Convert.ToDecimal(lbl_registration_Amount.Text.ToString()) + Convert.ToDecimal(lbl_inspection_amount.Text.ToString()) + Convert.ToDecimal(lbl_amedment_amount.Text.ToString()) + Convert.ToDecimal(lbl_renewal_amount.Text.ToString());
        lbl_total_amount.Text = total.ToString();
        Registration_Amount += Convert.ToDecimal(lbl_registration_Amount.Text.ToString());
        Insepection_amount += Convert.ToDecimal(lbl_inspection_amount.Text.ToString());
        Amedment_amount += Convert.ToDecimal(lbl_amedment_amount.Text.ToString());
        Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text.ToString());
        total_ammount += Convert.ToDecimal(lbl_total_amount.Text.ToString());
       }

      /*  if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //switch for first row 
            if (e.Row.RowIndex == 1)
            {
                GridViewRow Gprev = grd_report_paymentDetails.Rows[e.Row.RowIndex - 1];
                if (Gprev.Cells[0].Text.Equals(e.Row.Cells[0].Text))
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Visible = false;
                }
            }
            //now continue with the rest of the rows

            if (e.Row.RowIndex > 1)
            {
                //set reference to the row index and the current value
                int intC = e.Row.RowIndex;
                string lookfor = e.Row.Cells[0].Text;

                //now loop back through checking previous entries for matches 
                do
                {
                    GridViewRow Gprev = grd_report_paymentDetails.Rows[intC - 1];

                    if (Gprev.Cells[0].Text.Equals(e.Row.Cells[0].Text))
                    {
                        e.Row.Cells[0].Text = "";
                    }

                    intC = intC - 1;
                } while (intC > 0);

            }

        }*/


        if (e.Row.RowType == DataControlRowType.Footer)
        {

            Label lbltotalAmmount = (Label)e.Row.FindControl("lbltotalfooter");
            Label lbltotalRegistration = (Label)e.Row.FindControl("lblRegistrationTotal");
            Label lbltotalInspection = (Label)e.Row.FindControl("lblTotal");
            Label lblAmendment = (Label)e.Row.FindControl("lblAmedmentAmountTotal");
            Label lblRenewal = (Label)e.Row.FindControl("lblRenewalAmountTotal");
            
            lbltotalAmmount.Text = Convert.ToString(total_ammount.ToString());
            lbltotalRegistration.Text = Convert.ToString(Registration_Amount.ToString());
            lbltotalInspection.Text =Convert.ToString( Insepection_amount.ToString());
            lblAmendment.Text =Convert.ToString( Amedment_amount.ToString());
            lblRenewal.Text = Convert.ToString( Renewal_amount.ToString());




           // lbltotalAmmount.Text = "<b>Total Ammount</b>" + ' ' + "<b>" + totalprice.ToString() + "</b>";
           
        }
        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void grd_report_onRowCommand(object sender, GridViewCommandEventArgs e)
    {



    }

    public void Bind_Branch_Wise()  // changes Ravish Kumar Pandey
    {
        try
        {
            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();
            if (ddlZonewise.SelectedItem.Text != "")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    ZoneID = 0;
                }
                else
                {
                    ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
                }
                objcompany = new clsCompany();
                DataSet Ds_Branchwise = new DataSet();
                // DataTable dt_Branchwise = new DataTable();
                if (dateFrom != null && dateto != null && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[1].Visible = false;
                    //Ds_Branchwise = objcompany.Company_paymentetails_Pass_value_fromdate_Modified(dateFrom, dateto);
                    Ds_Branchwise = objcompany.Fetch_PaymentData(dateFrom, dateto);
                    Ds_Branchwise.Tables[0].DefaultView.Sort = Ds_Branchwise.Tables[0].Columns["ZoneName"].ToString() + " " + "ASC";

                    grd_Branch_Wise.Columns[1].Visible = false;

                }
                else
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    //Ds_Branchwise = objcompany.Company_PaymentDetails_Fetch_ZoneWise(dateFrom, dateto, ZoneID);
                    Ds_Branchwise = objcompany.Fetch_PaymentData_BranchWise(ZoneID, dateFrom, dateto);
                    Ds_Branchwise.Tables[0].DefaultView.Sort = Ds_Branchwise.Tables[0].Columns["BranchName"].ToString() + " " + "ASC";
                }
                if (Ds_Branchwise.Tables[0].Rows.Count > 0)
                {
                    lbl_total_record.Text = Ds_Branchwise.Tables[0].Rows.Count.ToString();
                    grd_Branch_Wise.DataSource = Ds_Branchwise.Tables[0];
                 
                    // this.grd_Branch_Wise.Sort(this.grd_Branch_Wise.Columns[0].ToString(), ListSortDirection.Ascending);
                    grd_Branch_Wise.DataBind();

                    print_button.Visible = true;

                }
                else
                {

                    Report_Type.Text = "N0 Record Found!!!";
                    lbl_total_record.Text = "0";
                    // Report_Type.Text = "";
                    print_button.Visible = false;
                    tr_print_button.Visible = false;
                    grd_Branch_Wise.Dispose();
                    grd_Branch_Wise.DataBind();
                    Ds_Branchwise.Dispose();
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void onRowGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {

                ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
                Response.Redirect("CompanyWiseRecord.aspx?id=" + e.CommandArgument + "&ZoneID=" + ZoneID, false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //Ravish Kumar 
    protected void grd_GridCompany_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
            {
                ZoneID = 0;
            }
            else
            {
                ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            }

            if ((txt_21.Text != "" && TextBox1.Text != "") && ddlZonewise.SelectedItem.Text == "--Select Zone--")
            {
                grd_Branch_Wise.Columns[1].Visible = false;
            }
            if ((txt_21.Text == "" && TextBox1.Text == "") && ddlZonewise.SelectedItem.Text == "--Select Zone--")
            {
                grd_Branch_Wise.Columns[1].Visible = false;
            }
            if ((txt_21.Text == "" && TextBox1.Text == "") && ddlZonewise.SelectedItem.Text != "--Select Zone--")
            {
                grd_Branch_Wise.Columns[1].Visible = true;
            }

            if (ddlType.SelectedItem.Text == "Registration")
            {
                if (dateto != "" && dateFrom != "" && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;

                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto == "" && dateFrom == "" && ZoneID == 0 && ddlType.SelectedItem.Text == "Registration")
                {
                    grd_Branch_Wise.Columns[0].Visible = true;

                    grd_Branch_Wise.Columns[1].Visible = false;


                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else
                {
                    grd_Branch_Wise.Columns[0].Visible = false;

                    grd_Branch_Wise.Columns[1].Visible = true;


                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[6].Visible = false;

                }
            }
            /*
        else if (ddlType.SelectedItem.Text == "Renewal")
         {
             grd_Branch_Wise.Columns[2].Visible = false;
             grd_Branch_Wise.Columns[4].Visible = true;
             grd_Branch_Wise.Columns[1].Visible = false;
             grd_Branch_Wise.Columns[6].Visible = false;
             grd_Branch_Wise.Columns[5].Visible = false;
             grd_Branch_Wise.Columns[3].Visible = false;
         }
        else if (ddlType.SelectedItem.Text == "Amendment")
          {
             grd_Branch_Wise.Columns[1].Visible = false;
             grd_Branch_Wise.Columns[2].Visible = false;
             grd_Branch_Wise.Columns[3].Visible = false;
             grd_Branch_Wise.Columns[6].Visible = false;
             grd_Branch_Wise.Columns[5].Visible = true;
             grd_Branch_Wise.Columns[4].Visible = false;
        }*/
            else if (ddlType.SelectedItem.Text == "Inspection")
            {
                if (dateto != "" && dateFrom != "" && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;

                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto == "" && dateFrom == "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;

                }
                else if (dateto != "" && dateFrom != "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
            }

       // Changes By Ravish Kumar Pandey  on 19 FEB (Tuesday) 2013
            else if (ddlType.SelectedItem.Text == "Amendment")
            {
                if (dateto != "" && dateFrom != "" && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = true;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto != "" && dateFrom != "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = true;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateFrom == "" && dateto == "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = false;
                    grd_Branch_Wise.Columns[5].Visible = true;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
            }
            else if (ddlType.SelectedItem.Text == "Renewal")
            {
                if (dateto != "" && dateFrom != "" && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = true;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateto != "" && dateFrom != "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = true;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;
                }
                else if (dateFrom == "" && dateto == "" && ZoneID != 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = false;
                    grd_Branch_Wise.Columns[4].Visible = true;
                    grd_Branch_Wise.Columns[5].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = false;
                    grd_Branch_Wise.Columns[6].Visible = false;

                }
            }
            // END   Changes By Ravish Kumar Pandey  on 19 FEB (Tuesday) 2013
            else if (ddlType.SelectedItem.Text == "--Select--")
            {
                if (dateFrom != " " && dateto != "" && ZoneID == 0)
                {
                    grd_Branch_Wise.Columns[0].Visible = true;
                    grd_Branch_Wise.Columns[1].Visible = false;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = true;
                }
                else
                {
                    grd_Branch_Wise.Columns[1].Visible = true;
                    grd_Branch_Wise.Columns[2].Visible = true;
                    grd_Branch_Wise.Columns[3].Visible = true;
                    grd_Branch_Wise.Columns[4].Visible = true;
                }
            }
            if (grd_Branch_Wise.Columns[0].Visible == true)
            {
                grd_Branch_Wise.Columns[1].Visible = false;
            }




            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Decimal total;
                if (dateFrom != null && dateto != null && ZoneID == 0)
                {
                    Label lblZoneId = (Label)e.Row.FindControl("lnkZone_ID");
                    int zoneIDLabel = Convert.ToInt32(lblZoneId.Text);
                    Label lblZoneName = (Label)e.Row.FindControl("lnkZone_Name");
                    string Zone_Name = "";
                    int index;
                    Zone_Name = lblZoneName.Text;
                    string Page1 = "";
                    string UrlZone = "BranchWiseSearch.aspx?id1=" + zoneIDLabel + "&datefrom=" + dateFrom + "&dateTo=" + dateto + "&ZName=" + lblZoneName.Text + "&Page=" + Page1;
                    lblZoneName.Text = "<a href='" + UrlZone + "'>" + lblZoneName.Text + "</a>";
                    //if ( e.Row.RowIndex>0)
                 //   {
                        // index=grd_Branch_Wise.Rows.Count-1;
                        Label lbl_zonename_hide = new Label();
                        //lbl_zonename_hide.Text = grd_Branch_Wise.Rows[e.Row.RowIndex - 1].ToString();  //e.Row.DataItem.GetType(Items.Values) e.Row.Cells[index].Text;
                        //if (e.Row.RowIndex == 1)
                        //{
                        //   GridViewRow gvr=grd_Branch_Wise.Rows[e.Row.RowIndex-1];
                        //   if (gvr.Cells[2].Text.Equals(e.Row.Cells[1].Text))
                        //   {

                        //       lblZoneName.Text = string.Empty;
                        //       lblZoneName.Visible = false;
                        //   }
                        decimal Registration= Convert.ToDecimal("0.0");
                        decimal Inspection=Convert.ToDecimal("0.0");
                        decimal Amedment = Convert.ToDecimal("0.0"); ;
                        decimal renewal=Convert.ToDecimal("0.0");
                        decimal Total_test = Convert.ToDecimal("0.0");
                        Registration += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "RegistrationAmount"));
                        Inspection += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "InspectionAmount"));
                        Amedment += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AmedmentAmount"));
                        renewal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "RenewalAmount"));
                        Total_test = Convert.ToDecimal(Registration + Inspection + Amedment + renewal);
                        Label lbl_registrationAmount = (Label)e.Row.FindControl("lblRegiatrationAmmount1");
                        if (lbl_registrationAmount.Text == "" || lbl_registrationAmount.Text==null)
                        {
                           lbl_registrationAmount.Text = "0.0";
                        }
                        Label lbl_Inspection_Amount = (Label)e.Row.FindControl("lbl_unit_ammount1");
                        if (lbl_Inspection_Amount.Text == "" || lbl_Inspection_Amount.Text == null)
                        {
                            lbl_Inspection_Amount.Text = "0.0";
                        }
                        Label lbl_renewal_amount = (Label)e.Row.FindControl("lbl_Renewal");
                        if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
                        {
                            lbl_renewal_amount.Text = "0.0";
                        }
                        Label lbl_Amedment_amount = (Label)e.Row.FindControl("lbl_amedment");
                        if (lbl_Amedment_amount.Text == "" || lbl_Amedment_amount.Text == null)
                        {
                            lbl_Amedment_amount.Text = "0.0";
                        }
                        Label lbl_total_amount = (Label)e.Row.FindControl("lblTotalAmmount");

                        total = Convert.ToDecimal((lbl_registrationAmount.Text).ToString()) + Convert.ToDecimal((lbl_Inspection_Amount.Text).ToString()) + Convert.ToDecimal((lbl_renewal_amount.Text).ToString()) + Convert.ToDecimal((lbl_Amedment_amount.Text).ToString());
                        lbl_total_amount.Text = total.ToString();
                        Registration_Amount += Convert.ToDecimal(lbl_registrationAmount.Text);
                        Amedment_amount += Convert.ToDecimal(lbl_Amedment_amount.Text);
                        Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
                        Insepection_amount += Convert.ToDecimal(lbl_Inspection_Amount.Text);
                        total_ammount += Convert.ToDecimal(lbl_total_amount.Text);

                    }

                //}

            else
                {

                    // Changes Ravish Kumar Pandey 13 feb 2013
                    Label lblBranchID = (Label)e.Row.FindControl("lbl_Branch_ID");
                    int branchid1 = Convert.ToInt32(lblBranchID.Text);
                    string url = "";
                    Label lblBrancName = (Label)e.Row.FindControl("lnk_Branc_Name");

                    url = "CompanyWiseRecord.aspx?id=" + branchid1 + "&ZoneID=" + ZoneID + "&BName=" + lblBrancName.Text + "&DateFrom=" + dateFrom + "&DateTo=" + dateto;

                    lblBrancName.Text = "<a href='" + url + "' >" + lblBrancName.Text + "</a>";


                    Label lbl_registrationAmount = (Label)e.Row.FindControl("lblRegiatrationAmmount1");
                    if (lbl_registrationAmount.Text == "" || lbl_registrationAmount.Text==null)
                    {
                        lbl_registrationAmount.Text = "0.0";
                    }
                   
                      
                    Label lbl_Inspection_Amount = (Label)e.Row.FindControl("lbl_unit_ammount1");
                    if (lbl_Inspection_Amount.Text == "" || lbl_Inspection_Amount.Text == null)
                    {
                        lbl_Inspection_Amount.Text = "0.0";
                    }
                    Label lbl_renewal_amount = (Label)e.Row.FindControl("lbl_Renewal");
                    if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
                    {
                        lbl_renewal_amount.Text = "0.0";
                    }
                    Label lbl_Amedment_amount = (Label)e.Row.FindControl("lbl_amedment");
                    if (lbl_Amedment_amount.Text == "" || lbl_Amedment_amount.Text == null)
                    {
                        lbl_Amedment_amount.Text = "0.0";
                    }
                    Label lbl_total_amount = (Label)e.Row.FindControl("lblTotalAmmount");
                   
                    total = Convert.ToDecimal((lbl_registrationAmount.Text).ToString()) + Convert.ToDecimal((lbl_Inspection_Amount.Text).ToString()) + Convert.ToDecimal((lbl_renewal_amount.Text).ToString()) + Convert.ToDecimal((lbl_Amedment_amount.Text).ToString());
                    lbl_total_amount.Text = total.ToString();
                    Registration_Amount += Convert.ToDecimal(lbl_registrationAmount.Text);
                    Amedment_amount += Convert.ToDecimal(lbl_Amedment_amount.Text);
                    Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
                    Insepection_amount += Convert.ToDecimal(lbl_Inspection_Amount.Text);
                    total_ammount += Convert.ToDecimal(lbl_total_amount.Text);



                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_registrationFooter = (Label)e.Row.FindControl("lblRegistrationTotal1");
                lbl_registrationFooter.Text = Convert.ToString(Registration_Amount);
                Label lbl_inspection_footer = (Label)e.Row.FindControl("lblTotal1");
                lbl_inspection_footer.Text = Convert.ToString(Insepection_amount);
                Label lbl_Amedment_amount = (Label)e.Row.FindControl("lbl_total_amedment");
                lbl_Amedment_amount.Text = Convert.ToString(Amedment_amount);
                Label lbl_renewalamount_footer = (Label)e.Row.FindControl("lbl_total_Renewal");
                lbl_renewalamount_footer.Text = Convert.ToString(Renewal_amount);
                Label lbl_total_amount_footer = (Label)e.Row.FindControl("lblTotalAmmountbranchwise");
                lbl_total_amount_footer.Text = Convert.ToString(total_ammount);

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    // Bind GridView If Select Zone And Branch  Bind Grid For payment List---

    public void Bind_GridView_BranchZoneWise()
    {
        try
        {
            int ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            int BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
            string formdate = TextBox1.Text.ToString();
            string todate = txt_21.Text.ToString();
            string orderby = "";
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                orderby = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                orderby = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                orderby = "3";
            }
            objcompany = new clsCompany();
            DataSet Myds_company = new DataSet();
            DataTable dt_fetch_datewiserecord = new DataTable();
            DataTable dt_filtertable = new DataTable();
            Myds_company = new DataSet();
            if (ZoneID != 0 && BranchID != 0 && formdate == "" && todate == "")
            {
              Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified(ZoneID, BranchID, orderby);
               // Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(ZoneID, BranchID, formdate, todate);
            }
            else
            {
                if (ZoneID != 0 && BranchID != 0 && formdate != "" && todate != "")
                {
                   // Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_datetime(ZoneID, BranchID, formdate, todate);
                    // Not Uploaded  method Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified Ravish
                     
                    Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(ZoneID, BranchID, formdate, todate, orderby);
                }
            }
            dt_filtertable = Myds_company.Tables[0];

            // }
            if (Myds_company.Tables[0].Rows.Count > 0)
            {

                if (formdate != "" && todate != "")
                {
                    lbl_total_record.Text = dt_filtertable.Rows.Count.ToString();
                    grd_Company_wiseData.DataSource = dt_filtertable;
                    grd_Company_wiseData.DataBind();
                    print_button.Visible = true;
                }

                lbl_total_record.Text = Myds_company.Tables[0].Rows.Count.ToString();

                grd_Company_wiseData.DataSource = Myds_company.Tables[0];
                grd_Company_wiseData.DataBind();
                grd_Company_wiseData.PageIndex = 0;
                print_button.Visible = true;
            }
            else
            {
                Report_Type.Text = "N0 Record Found!!!";
                lbl_total_record.Text = "0";

                // Report_Type.Text = "";
                print_button.Visible = false;
                tr_print_button.Visible = false;
                grd_Company_wiseData.Dispose();
                grd_Company_wiseData.DataBind();
                Myds_company.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void grdCompany_Wise_Data(object sender, GridViewRowEventArgs e)
    {

        try
        {



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlType.SelectedItem.Text.ToString() == "Registration")
                {
                    grd_Company_wiseData.Columns[2].Visible = false;
                    grd_Company_wiseData.Columns[5].Visible = false;
                    grd_Company_wiseData.Columns[6].Visible = false;
                    grd_Company_wiseData.Columns[7].Visible = false;
                    grd_Company_wiseData.Columns[3].Visible = false;
                    grd_Company_wiseData.Columns[4].Visible = false;
                }
                else if (ddlType.SelectedItem.Text.ToString() == "Inspection")
                {
                    grd_Company_wiseData.Columns[1].Visible = false;
                    grd_Company_wiseData.Columns[5].Visible = false;
                    grd_Company_wiseData.Columns[6].Visible = false;
                    grd_Company_wiseData.Columns[7].Visible = false;
                    grd_Company_wiseData.Columns[3].Visible = false;
                    grd_Company_wiseData.Columns[4].Visible = false;
                }

                else if (ddlType.SelectedItem.Text.ToString() == "Amendment")
                {
                    grd_Company_wiseData.Columns[1].Visible = false;
                    grd_Company_wiseData.Columns[2].Visible = false;
                    grd_Company_wiseData.Columns[5].Visible = true;
                    grd_Company_wiseData.Columns[7].Visible = false;
                    grd_Company_wiseData.Columns[3].Visible = false;
                    grd_Company_wiseData.Columns[4].Visible = false;
                }
                else if (ddlType.SelectedItem.Text.ToString() == "Renewal")
                {
                    grd_Company_wiseData.Columns[1].Visible = false;
                    grd_Company_wiseData.Columns[2].Visible = false;
                    grd_Company_wiseData.Columns[5].Visible = false;
                    grd_Company_wiseData.Columns[6].Visible = true;
                    grd_Company_wiseData.Columns[3].Visible = false;
                    grd_Company_wiseData.Columns[4].Visible = false;
                    grd_Company_wiseData.Columns[7].Visible = false;
                }
                Label lblcompanyid = (Label)e.Row.FindControl("LBL_Company_Id_Data");
                Label lblcompanyName = (Label)e.Row.FindControl("LBl_company_Name_Data");
                int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
                string CompanyName = lblcompanyName.Text;
                string Page = "Comapnywise";
                string page1 = "";
                string url = "CompanyWiseSeparateRecord.aspx?cid=" + companyid + "&cname=" + CompanyName + "&Page=" + Page + "&page1=" + page1;

                lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";

                Label lblregistration_amount = (Label)e.Row.FindControl("lblRegiatrationAmmount2");
                if (lblregistration_amount.Text == null || lblregistration_amount.Text == "")
                {

                    lblregistration_amount.Text = "0.0";
                }
                Label lbl_inspection_amount = (Label)e.Row.FindControl("lbl_unit_ammount2");
                if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
                {
                    lbl_inspection_amount.Text = "0.0";
                }
                Label lbl_amedment_amount = (Label)e.Row.FindControl("lblAmedmentAmount");
                if (lbl_amedment_amount.Text == "" || lbl_amedment_amount.Text == null)
                {
                    lbl_amedment_amount.Text = "0.0";
                }

                Label lbl_renewal_amount = (Label)e.Row.FindControl("lblRenewalAmountText");
                if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
                {

                    lbl_renewal_amount.Text = "0.0";
                }

                Label lbl_total_amount = (Label)e.Row.FindControl("lbl_total_amount");
                if (lbl_total_amount.Text == "" || lbl_total_amount.Text == null)
                {
                    decimal total = 0;
                    total = Convert.ToDecimal(lbl_amedment_amount.Text.ToString()) + Convert.ToDecimal(lbl_inspection_amount.Text.ToString()) + Convert.ToDecimal(lbl_renewal_amount.Text.ToString()) + Convert.ToDecimal(lblregistration_amount.Text.ToString());
                    lbl_total_amount.Text = total.ToString();

                }
                Registration_Amount += Convert.ToDecimal(lblregistration_amount.Text);
                Insepection_amount += Convert.ToDecimal(lbl_inspection_amount.Text);
                Amedment_amount += Convert.ToDecimal(lbl_amedment_amount.Text);
                Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
                total_ammount += Convert.ToDecimal(lbl_total_amount.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl_regisrtartion_footer = (Label)e.Row.FindControl("lblRegistrationTotal2");
                lbl_regisrtartion_footer.Text = Convert.ToString(Registration_Amount);
                Label lbl_inspection_Footer = (Label)e.Row.FindControl("lblTotal2");
                lbl_inspection_Footer.Text = Convert.ToString(Insepection_amount);
                Label lbl_amedment_footer = (Label)e.Row.FindControl("lblAmedmentAmountText");
                lbl_amedment_footer.Text = Convert.ToString(Amedment_amount);
                Label lbl_renewal_footer = (Label)e.Row.FindControl("lblRenewalAmount");

                lbl_renewal_footer.Text = Convert.ToString(Renewal_amount);

            }
        }
        
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
        }
    }
    /*
    protected void grd_report_paymentDetails_DataBound(object sender, EventArgs e)
    {
        if (grd_report_paymentDetails.Rows.Count > 0)
        {
            try
            {
                Label lblTotalAmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblRegistrationTotal");
                lblTotalAmount.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Int32.Parse(((Label)a.FindControl("lblRegiatrationAmmount")).Text)));

                Label lblTotal = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblTotal");
                lblTotal.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Int32.Parse(((Label)a.FindControl("lbl_unit_ammount")).Text)));
                Label labelTotalAmmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lbltotalfooter");
                labelTotalAmmount.Text = (Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(lblTotal.Text)).ToString();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }

    }*/
   /* protected void grdBranch_wise_dataBound(object sender, EventArgs e)
    {
        if (grd_Branch_Wise.Rows.Count > 0)
        {
            try
            {
                Label lblTotalAmount1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblRegistrationTotal1");
                    
                lblTotalAmount1.Text = String.Format("{0:n2}",
                                        grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount1")).Text)));

                Label lblTotal1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblTotal1");
                lblTotal1.Text = String.Format("{0:n2}",
                                        grd_Branch_Wise.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount1")).Text)));

                Label lblRenewalAmount = (Label)grd_Branch_Wise.FooterRow.FindControl("lbl_total_Renewal");
               
                lblRenewalAmount.Text = String.Format("{0:n2}",
                                                 grd_Branch_Wise.Rows.Cast<GridView>()
                                                .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_Renewal")).Text)));
                if (lblRenewalAmount.Text == "")
                {
                    lblRenewalAmount.Text = "0.0"; 
                }

                Label lblAmedmentAmount = (Label)grd_Branch_Wise.FooterRow.FindControl("lbl_total_amedment");
                
                
                lblAmedmentAmount.Text = String.Format("{0:n2}",
                                           grd_Branch_Wise.Rows.Cast<GridView>()
                                            .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_amedment")).Text)));
                if (lblAmedmentAmount.Text == "")
                {
                    lblAmedmentAmount.Text = "0.0";  
                }
                Label lbltotalbranchwise1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblTotalAmmountbranchwise");
                lbltotalbranchwise1.Text = (Convert.ToDecimal(lblTotalAmount1.Text) + Convert.ToDecimal(lblTotal1.Text).ToString() +Convert.ToDecimal(lblRenewalAmount.Text).ToString()+Convert.ToDecimal(lblAmedmentAmount.Text).ToString());


               

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }

        }
    }*/
    /*
    protected void grdcompany_branchDatabound(object sender, EventArgs e)
    {
        if (grd_Company_wiseData.Rows.Count > 0)
        {
            try
            {
                Label lblTotalAmount2 = (Label)grd_Company_wiseData.FooterRow.FindControl("lblRegistrationTotal2");
                lblTotalAmount2.Text = String.Format("{0:n2}",
                                        grd_Company_wiseData.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount2")).Text)));

                Label lblTotal2 = (Label)grd_Company_wiseData.FooterRow.FindControl("lblTotal2");
                lblTotal2.Text = String.Format("{0:n2}",
                                        grd_Company_wiseData.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount2")).Text)));
                Label lbltotalCompanyWise = (Label)grd_Company_wiseData.FooterRow.FindControl("lblTotal_companywise");
                lbltotalCompanyWise.Text = (Convert.ToDecimal(lblTotalAmount2.Text) + Convert.ToDecimal(lblTotal2.Text)).ToString();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
                
        }
    }
    */



    // For user List text to show data in gridview -----------

    public void bindUserList()
    {
        try
        {
            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();

            if (ddlZonewise.SelectedItem.Text != "")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    ZoneID = 0;
                }
                else
                {
                    ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
                }
            }
            if (ddlBranchWise.SelectedItem.Text != "")
            {
                if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
                {
                    BranchID = 0;
                }
                else
                {
                    BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
                }
            }
            objcompany = new clsCompany();
            DataSet ds_user_list = new DataSet();
            //
            ds_user_list = objcompany.Company_UserList_Report(dateFrom, dateto, BranchID, ZoneID);
            if (ds_user_list.Tables[0].Rows.Count > 0)
            {
                lbl_total_record.Text = ds_user_list.Tables[0].Rows.Count.ToString();
                grd_user_list_report.DataSource = ds_user_list.Tables[0];
                grd_user_list_report.DataBind();
                grd_user_list_report.PageIndex = 0;

            }
            else
            {
                Report_Type.Text = "N0 Record Found!!!";
                lbl_total_record.Text = "0";
                grd_user_list_report.Dispose();
                grd_user_list_report.DataBind();
                ds_user_list.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    // For Inspection Agency List

    public void BindInspectionAgency()
    {
        try
        {

       
        dateFrom = TextBox1.Text.ToString();
        dateto = txt_21.Text.ToString();

        if (ddlZonewise.SelectedItem.Text != "")
        {
            if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
            {
                ZoneID = 0;
            }
            else
            {
                ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            }
        }
        if (ddlBranchWise.SelectedItem.Text != "")
        {
            if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
            {
                BranchID = 0;
            }
            else
            {
                BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
            }
        }
        objcompany = new clsCompany();
        DataSet Ds_Inspection_agency = new DataSet();
          Ds_Inspection_agency=objcompany.Company_Inspection_Report(dateFrom, dateto, BranchID, ZoneID);
          if(Ds_Inspection_agency.Tables[0].Rows.Count>0)
              {
                  lbl_total_record.Text = Ds_Inspection_agency.Tables[0].Rows.Count.ToString();
                  grd_inspectionAgency.DataSource = Ds_Inspection_agency.Tables[0];
                  grd_inspectionAgency.DataBind();
                  grd_inspectionAgency.PageIndex = 0;
               
              }
          else
           {
               Report_Type.Text = "N0 Record Found!!!";
               lbl_total_record.Text = "0";
               grd_inspectionAgency.Dispose();
               grd_inspectionAgency.DataBind();
               Ds_Inspection_agency.Dispose();
           }
        }
        catch (Exception ex)
        {

           ErrHandler.WriteError(ex.Message);
        }
         }

    protected void grd_user_list_report_onrowDatabound(object sender, GridViewRowEventArgs e)
    {
        //if (ddlZonewise.SelectedItem.Text != "")
        //{
        //    grd_user_list_report.Columns[0].Visible = false;
        //}
    }
    protected void grd_inspectionagency_onrowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_address = (Label)e.Row.FindControl("lbl_Address");
           string Address=  lbl_address.Text.Replace(",","<br/>");
           string decodedText = HttpUtility.HtmlDecode(Address);
           e.Row.Cells[6].Text = decodedText;
           e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Width, "250px");

        }
        }
        catch (Exception ex)
        {

           ErrHandler.WriteError(ex.Message);
        }
    }

    // BIND REport GRid

    public void BindGrid_Report_print_access()
    {
        try
        {
            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();
            if (ddlZonewise.SelectedItem.Text != "")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    ZoneID = 0;
                }
                else
                {
                    ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
                }
            }
            if (ddlBranchWise.SelectedItem.Text != "")
            {
                if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
                {
                    BranchID = 0;
                }
                else
                {
                    BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
                }
            }
            if (ddlcomapnywise.SelectedValue != "")
            {
                if (ddlcomapnywise.SelectedItem.Text == "--Select Company--")
                {
                    CompanyId = 0;
                }
                else
                {
                    CompanyId = Convert.ToInt32(ddlcomapnywise.SelectedValue);
                }
            }
            else
            {
                CompanyId = 0;
            }
            string orderby = "";
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                orderby = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                orderby = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                orderby = "3";
            }
           
            objcompany = new clsCompany();
            DataSet ds_bind_print_report = new DataSet();
           /* if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID,orderby);
            }

            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID,orderby);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID == 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWiseModified(dateFrom, dateto, orderby);
            }
            else if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId, orderby);
            }
            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId, orderby);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID, orderby);
            }
           
            else if (dateFrom == "" && dateto == "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID,orderby);
            }
            else if (dateFrom == "" && dateto == "" && ZoneID == 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID,orderby);
            }*/
            ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId, orderby);
            if (ds_bind_print_report.Tables[0].Rows.Count > 0)
            {
                lbl_total_record.Text = ds_bind_print_report.Tables[0].Rows.Count.ToString();
                grd_print_certificate.DataSource = ds_bind_print_report.Tables[0];
                grd_print_certificate.DataBind();
                grd_print_certificate.PageIndex = 0;
              //  grd_print_certificate.Dispose();
               
            }
            else
            {

                Report_Type.Text = "No Record Found!!!";
                lbl_total_record.Text = "0";
                grd_print_certificate.Dispose();
                grd_print_certificate.DataBind();
                ds_bind_print_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gridprint_certificate_ON_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_address = (Label)e.Row.FindControl("lbl_Zone_Branch");  //For Zone Branch
                string Address = lbl_address.Text.Trim().ToString();//Replace(",", "<br/>");
                string decodedText = HttpUtility.HtmlDecode(Address);
                e.Row.Cells[2].Text = decodedText;
                e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.Width, "100px");
                e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
                Label lbl_Request = (Label)e.Row.FindControl("lbl_request_date");  //For Zone Branch
                string RequestDate = lbl_Request.Text.Trim().ToString();//Replace(",", "<br/>");
                string decodedText1 = HttpUtility.HtmlDecode(RequestDate);
                e.Row.Cells[3].Text = decodedText1;
                e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Width, "100px");
                e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
                e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
                Label lbl_Approved_Date = (Label)e.Row.FindControl("lbl_RequsetDate_userName");  //For Zone Branch
                string ApprovedDate = lbl_Approved_Date.Text.Trim().ToString();//Replace(",", "<br/>");
                string decodedText2 = HttpUtility.HtmlDecode(ApprovedDate);
                e.Row.Cells[5].Text = decodedText2;
                e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.Width, "100px");
                e.Row.Cells[5].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
                Label lblcompanyid = (Label)e.Row.FindControl("lbl_companyId_print");
                Label lblcompanyName = (Label)e.Row.FindControl("lbl_comapny_print_name");
                //lblcompanyName.Text.IndexOf("<br/");
                //Chenges By Ravish 20 FEB 2013

                int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
                string CompanyName_text = lblcompanyName.Text;
                string[] Company_Name_RemoveBr = Regex.Split(CompanyName_text, "<br/>");
                string CompanyName = Company_Name_RemoveBr[0].ToString();


                //string CompanyName = lblcompanyName.Text.i;
                string Page = "Comapnywise";
                string Page1 = "";
                string url = "CompanyDetails.aspx?cid=" + companyid + "&cname=" + CompanyName + "&page1=" + Page1;

                lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddl_category_wise_report_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Ddl_category_cast_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void DdlCategoryUnitSize_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



      // Bind All List In Report pages---
    private void funfill_ddlCategory()
    {
        try
        {
            //Category Area DROPDOWNLIST
            System.Data.DataTable dt = new System.Data.DataTable();
            dt = clsDDL.ddlCategoryArea();

            ddl_category_wise_report.DataSource = dt;
            ddl_category_wise_report.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
            ddl_category_wise_report.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
            ddl_category_wise_report.DataBind();
            ddl_category_wise_report.Items.Insert(0, new ListItem("--Select Category Area--", "0"));

            //Category Cast DROPDOWNLIST
            dt.Clear();
            dt = clsDDL.ddlCategoryCast();

            Ddl_category_cast.DataSource = dt;
            Ddl_category_cast.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
            Ddl_category_cast.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
            Ddl_category_cast.DataBind();
            Ddl_category_cast.Items.Insert(0, new ListItem("--Select Category Cast--", "0"));

            //Category Unit Size DROPDOWNLIST
            dt.Clear();
            dt = clsDDL.ddlCategoryUnitSize();

            DdlCategoryUnitSize.DataSource = dt;
            DdlCategoryUnitSize.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
            DdlCategoryUnitSize.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
            DdlCategoryUnitSize.DataBind();
            DdlCategoryUnitSize.Items.Insert(0, new ListItem("--Select Category Unit Size--", "0"));

            dt.Clear();
            dt.Dispose();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }




    protected void grd_report_paymentDetails_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_report_paymentDetails.PageIndex = e.NewPageIndex;
            Bind_Payment_Details();
            grd_report_paymentDetails.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grd_Branch_Wise_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_Branch_Wise.PageIndex = e.NewPageIndex;
            Bind_Branch_Wise();
            grd_Branch_Wise.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grd_Company_wiseDataPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_Company_wiseData.PageIndex = e.NewPageIndex;
            Bind_GridView_BranchZoneWise();
            grd_Company_wiseData.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grd_user_list_report_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_user_list_report.PageIndex = e.NewPageIndex;
            bindUserList();
            grd_user_list_report.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grd_inspectionAgency_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        try
        {


            grd_inspectionAgency.PageIndex = e.NewPageIndex;
            BindInspectionAgency();
            grd_inspectionAgency.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grd_print_certificate_pageindexchanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_print_certificate.PageIndex = e.NewPageIndex;
            BindGrid_Report_print_access();
            grd_print_certificate.Visible = true;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlstatuswise_SelectedIndexChanged(object sender, EventArgs e)
    {
        Report_Page.Visible = false;
    }


    public void Bind_status_wise()
    {
        try
        {
           // DropDownList ddlstatuswise = new DropDownList();
            List<string> Statuswise = new List<string>();
            Statuswise.Add("Incomplete");
            Statuswise.Add("Waiting");
            Statuswise.Add("Inprocess");
            Statuswise.Add("Inspection Passed");
            Statuswise.Add("Registered");
            Statuswise.Add("Expired");
            //Statuswise.Add("Rejected");
           
           
           
          

            ddlstatuswise.DataSource = Statuswise;
            ddlstatuswise.DataBind();
            ddlstatuswise.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void Detaileview1_sorting(object sender, GridViewSortEventArgs e)
    {
      
        if (e.SortExpression.Trim() == this.SortField)
            this.SortDirection_1 = (this.SortDirection_1 == "D" ? "A" : "D");
        else
            this.SortDirection_1 = "A";

        this.SortField = e.SortExpression;
        SetSortDirection(this.SortDirection_1);
        BIND_Detailesview();

         int columnIndex=0;
                foreach (DataControlFieldHeaderCell headerCell in   DetailsView1.HeaderRow.Cells)
                {
                    if (headerCell.ContainingField.SortExpression==e.SortExpression)
                    {
                        columnIndex = DetailsView1.HeaderRow.Cells.GetCellIndex(headerCell);
                    }
                }

                DetailsView1.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            
 }
    protected void SetSortDirection(string sortDirection)
    {
        if (sortDirection == "A")
        {
            //_sortDirection = "DESC";
            sortImage.ImageUrl = "../Images/view_sort_ascending.png";
            sortImage.Attributes.Add("Title", "Ascending");
            sortImage.ImageAlign = ImageAlign.Middle;

        }
        else
        {
           // _sortDirection = "A";
            sortImage.ImageUrl = "../Images/view_sort_descending.png";
            sortImage.Attributes.Add("Title", "Descending");
            sortImage.ImageAlign = ImageAlign.Middle;

        }
    }
   public DataTable Temp
    {
        get
        {
            object o = Session["Temp"];  // ViewState["Temp"];
            if (o == null)
            {
                DataTable dt = new DataTable();
                return dt;
            }
            else
                return (DataTable)o;
        }
        set
        {
           // ViewState["Temp"] = value;
            Session["Temp"] = value;
        }
    }

    string SortDirection_1
    {
        get
        {
            object o = ViewState["SortDirection"];
            if (o == null)
                return String.Empty;
            else
                return (string)o;
        }
        set
        {
            ViewState["SortDirection"] = value;
        }
    }
    string SortField
    {
      


        get
        {
            object o = ViewState["SortField"];
            if (o == null)
                return String.Empty;
            else
                return (string)o;
        }
        set
        {
            ViewState["SortField"] = value;
        }
    }
    protected void Detailesview1RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {

          
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }

    //public void AddSortImage(GridViewRow grdheader)
    //{
    //    try
    //    {
    //        int icol = GetSortColumnIndex(this.SortField);
    //    }
    //    catch (Exception  ex)
    //    {

    //        ErrHandler.WriteError(" " + ex.Message.ToString());
    //    }
    //}

    protected void cmdSearch_Click(object sender, EventArgs e)
    {
        string Searchtext = "";
        try
        {
            Searchtext = txtName.Text.ToString().Trim();
            SearchString(Searchtext);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }

    public void SearchString(string Serchtext)
    {
        try
        {
            int Countrow = DetailsView1.Rows.Count;
            DataTable dttemp = new DataTable();
           // dttemp = Temp.Copy();
            if (Temp.Rows.Count > 0)
            {
                //((DataView)DetailsView1.DataContext).RowFilter = string.Concat("Name LIKE '%", SearchFor, "%'");
                Temp.DefaultView.RowFilter = string.Concat("Company LIKE '%", Serchtext.ToString().Trim(), "%'"); //"Company = " + Serchtext.ToString();
                Temp.AcceptChanges();
                DetailsView1.DataSource = Temp;
                DetailsView1.DataBind();
                lbl_total_record.Text = DetailsView1.Rows.Count.ToString();

            }
            else
            {
                Temp.Dispose();
                lbl_total_record.Text = "0";
                Temp.Dispose();
              DetailsView1.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }
    }
    protected void cmdClear_Click(object sender, EventArgs e)
    {
        try
        {
            txtName.Text = "";
            //DataTable dttemp = new DataTable();
           // dttemp = Temp.Copy();
            if (Temp.Rows.Count > 0)
            {
             //   DetailsView1.DataSource = Temp;
                //DetailsView1.DataBind();
                BIND_Detailesview();
              //  lbl_total_record.Text = DetailsView1.Rows.Count.ToString();  
            }
            else
            {
                Temp.Dispose();
                lbl_total_record.Text = "0";
                Temp.Dispose();
                DetailsView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(""+ex.Message.ToString());
        }
    }
    protected void ddl_custom_paging_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_dropdownlist_custompageing.SelectedValue = ddl_custom_paging.SelectedValue;
            if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--" )
                {
                    if (ddl_custom_paging.SelectedItem.Text == "All")
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                    }
                    else
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                        
                    }
                }
               /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" )
                {
                    if (ddl_custom_paging.SelectedItem.Text == "All")
                    {
                        grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_GridView_BranchZoneWise();
                        grd_Company_wiseData.Visible = true;
                    }
                    else
                    {
                        grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                        Bind_GridView_BranchZoneWise();
                        grd_Company_wiseData.Visible = true;
                    }
                }*/
               /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" && ddlcomapnywise.SelectedItem.Text != "--Select Company--")
                {
                    if (ddl_custom_paging.SelectedItem.Text == "All")
                    {
                        grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_GridView_BranchZoneWise();
                        grd_Company_wiseData.Visible = true;
                    }
                    else
                    {
                        grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                        Bind_GridView_BranchZoneWise();
                        grd_Company_wiseData.Visible = true;
                    }
                }*/
                else
                {

                    if (ddl_custom_paging.SelectedItem.Text == "All")
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;

                    }
                    else
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;
                    }
                }
            }
            else if(ddlSelectCriteria.SelectedItem.Text == "User List")
            {
                if (ddl_custom_paging.SelectedItem.Text == "All")
                {
                    grd_user_list_report.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }
                else
                {
                    grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
            {
                if (ddl_custom_paging.SelectedItem.Text == "All")
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }
                else
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Duplicate Certificate Print Request")
            {
                if (ddl_custom_paging.SelectedItem.Text == "All")
                {
                    grd_print_certificate.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
                else
                {

                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment" || ddlSelectCriteria.SelectedItem.Text == "Renewal" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                if (ddl_custom_paging.SelectedItem.Text == "All")
                {
                    DetailsView1.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());

                    BIND_Detailesview();
                }
                else
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);

                    BIND_Detailesview();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }

    public void Bind_ddl_custom_paging()
    {
        try
        {
           
            ddl_custom_paging.Items.Add("10");
            ddl_custom_paging.Items.Add("20");
            ddl_custom_paging.Items.Add("30");
            ddl_custom_paging.Items.Add("40");
            ddl_custom_paging.Items.Add("50");
            ddl_custom_paging.Items.Add("60");
            ddl_custom_paging.Items.Add("70");
            ddl_custom_paging.Items.Add("All");
            ddl_custom_paging.AutoPostBack = true;
            ddl_dropdownlist_custompageing.Items.Add("10");
            ddl_dropdownlist_custompageing.Items.Add("20");
            ddl_dropdownlist_custompageing.Items.Add("30");
            ddl_dropdownlist_custompageing.Items.Add("40");
            ddl_dropdownlist_custompageing.Items.Add("50");
            ddl_dropdownlist_custompageing.Items.Add("60");
            ddl_dropdownlist_custompageing.Items.Add("70");
            ddl_dropdownlist_custompageing.Items.Add("All");
            ddl_dropdownlist_custompageing.AutoPostBack = true;

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
    protected void detailview1_pagesizechanged(object sender, EventArgs e)
    {
        //DetailsView1.PageSize = e.NewPageSize;
    }
    protected void ddl_dropdownlist_custompageing_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
            if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                    }
                    else
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;

                    }
                }
                /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" )
                 {
                     if (ddl_custom_paging.SelectedItem.Text == "All")
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                     else
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                 }*/
                /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" && ddlcomapnywise.SelectedItem.Text != "--Select Company--")
                 {
                     if (ddl_custom_paging.SelectedItem.Text == "All")
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                     else
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                 }*/
                else
                {

                    if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;

                    }
                    else
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;
                    }
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "User List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_user_list_report.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }
                else
                {
                    grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }
                else
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Duplicate Certificate Print Request")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_print_certificate.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
                else
                {

                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment" || ddlSelectCriteria.SelectedItem.Text == "Renewal" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    DetailsView1.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());

                    BIND_Detailesview();
                }
                else
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);

                    BIND_Detailesview();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }

    /*  dropdown bind State Wise  */
    public void BindStatewise( int BranchID )
    {
        DataSet ds_bind_state = new DataSet();
        try
        {
            objcompany = new clsCompany();
           
            ds_bind_state = objcompany.Fetch_Company_State_wise_dropdown( BranchID);
            if (ds_bind_state.Tables[0].Rows.Count > 0)
            {
                ddl_state_wise.DataTextField = ds_bind_state.Tables[0].Columns[1].Caption;
                ddl_state_wise.DataValueField = ds_bind_state.Tables[0].Columns[0].Caption;
                ddl_state_wise.DataSource = ds_bind_state.Tables[0];
                ddl_state_wise.DataBind();
                ddl_state_wise.Items.Insert(0, new ListItem("--Select State--", ""));
            }
            else
            {
                ds_bind_state.Tables[0].Dispose();
                ddl_state_wise.Items.Insert(0, new ListItem("--Select State--", ""));
                ddl_state_wise.DataBind();
            }

            // DTZone = new DataTable();

            //  DTZone = clsDDL.ddlZone_Activated();


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            ds_bind_state.Tables[0].Dispose();   
        }

    }
   /* ------------------- Bind city wise dropdownlist in change of State wise   --------*/
    protected void ddl_state_wise_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds_city = new DataSet();
        try
        {
            objcompany = new clsCompany();
            if (ddl_state_wise.SelectedValue != "")
            {
                int StateID = Convert.ToInt32(ddl_state_wise.SelectedValue);
                ds_city = objcompany.Fetch_Company_city_wise(StateID);
                if (ds_city.Tables[0].Rows.Count > 0)
                {
                    Ddl_City_Wise.DataTextField = ds_city.Tables[0].Columns[1].Caption;
                    Ddl_City_Wise.DataValueField = ds_city.Tables[0].Columns[0].Caption;
                    Ddl_City_Wise.DataSource = ds_city.Tables[0];
                    Ddl_City_Wise.DataBind();
                    Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                }
                else
                {
                    ds_city.Tables[0].Dispose();
                    Ddl_City_Wise.Items.Clear();
                    Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                    Ddl_City_Wise.DataBind();
                }
                   
            }
            else
            {
                 // ds_city.Tables[0].Dispose();
                 //Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                Ddl_City_Wise.Items.Clear();
                Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                    Ddl_City_Wise.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
           // ds_city.Tables[0].Dispose();
        }


    }
    protected void ddl_dropdownlist_custompageing_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
           // ddl_dropdownlist_custompageing
           ddl_custom_paging.SelectedValue =  ddl_dropdownlist_custompageing.SelectedValue;
           if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                    }
                    else
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                        Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;

                    }
                }
                /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" )
                 {
                     if (ddl_custom_paging.SelectedItem.Text == "All")
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                     else
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                 }*/
                /* else if (ddlZonewise.SelectedItem.Text != "--Select Zone--" && ddlBranchWise.SelectedItem.Text != "--Select Branch--" && ddlcomapnywise.SelectedItem.Text != "--Select Company--")
                 {
                     if (ddl_custom_paging.SelectedItem.Text == "All")
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                     else
                     {
                         grd_Company_wiseData.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                         Bind_GridView_BranchZoneWise();
                         grd_Company_wiseData.Visible = true;
                     }
                 }*/
                else
                {

                    if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;

                    }
                    else
                    {
                        grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                        Bind_Payment_Details();
                        grd_report_paymentDetails.Visible = true;
                    }
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "User List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_user_list_report.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }
                else
                {
                    grd_report_paymentDetails.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    bindUserList();
                    grd_user_list_report.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }
                else
                {
                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    BindInspectionAgency();
                    grd_inspectionAgency.Visible = true;
                }

            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Duplicate Certificate Print Request")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    grd_print_certificate.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
                else
                {

                    grd_inspectionAgency.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    BindGrid_Report_print_access();
                    grd_print_certificate.Visible = true;
                }
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment" || ddlSelectCriteria.SelectedItem.Text == "Renewal" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    DetailsView1.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());

                    BIND_Detailesview();
                }
                else
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);

                    BIND_Detailesview();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }
}
  

