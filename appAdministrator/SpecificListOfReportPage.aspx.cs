using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;

public partial class appAdministrator_SpecificListOfReportPage : System.Web.UI.Page
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
    string dateFrom = "";
    string dateto;
    int BranchID;
    int ZoneID;
    int CategoryID;
    int SubCategoryID;
    int CastID;
    int State_Id=0;
    int City_Id=0;
    int ErrorId, CompanyId, UnitId;
    DataTable DTZone;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string Status;
    string FormType;
    int FormType_Id;
    int Typeforpayment;
    decimal Registration_amount;
    decimal Amedment_amount;
    decimal Renewal_amount;
    decimal Inspection_amount;
    decimal Total_amount;
    string Orderby_User = "1";
    int Total_No_Of_record = 0;
    int TotalPages = 0;
    int Page_size;
    Image sortImage = new Image();

    public void FunRequest_Load()
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
            //UserEmailID = "";
        }
    }
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
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();


            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            
        }
       // BindSelectCriteria();
        if (!IsPostBack)
        {

           
            BindSelectCriteria();
            funfill_ddlCategory();
            Bind_All_Control();
            Bind_ddl_custom_paging();
            Bindstatuswise();
        }
   
    }

    public void BindSelectCriteria()
    {
        try
        {
            objcompany = new clsCompany();
            DataTable dtselect_Criteria = new DataTable();
            dtselect_Criteria = objcompany.Fetch_Report_Name_ReportCriteria(UserId);
            if (dtselect_Criteria.Rows.Count > 0)
            {
                ddlSelectCriteria.DataSource = dtselect_Criteria;
                ddlSelectCriteria.DataTextField = "ReportName";
                ddlSelectCriteria.DataValueField = "ReportId";
                ddlSelectCriteria.DataBind();
                ddlSelectCriteria.Items.Insert(0, new ListItem("--Select--", ""));

                BindZoneByRoleID();
                BindZonewise();
                BindCompany();
            }
            else
            {
               // ddlSelectCriteria.Items.Insert(0, new ListItem("--Select--", ""));
                dtselect_Criteria.Dispose();

            }
        }
        catch (Exception ex)
        {
            //ex.Message(ErrHandler.WriteError);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlSelectCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlSelectCriteria.SelectedItem.Text == "--Select--")
        {
            try
            {
                txtName.Text = "";
               // Selection_crietria.Visible = false;
                lst_report_type.Visible = false;
                provisionalReport.Visible = false;
                tr_search_company.Visible = false;
                txt_search_company.Text = "";
                DateWise.Visible = false;
                Zonewise.Visible = false;
                BranchWise.Visible = false; 
                print_button.Visible = false;
                lst_statuswise.Visible = false;
                lst_report_type.Visible = false;
                lst_type.Visible = false;
                print_button.Visible = false;
                CompanyWise.Visible = false;
                btn_report_ReportPage.Visible = false;
                Report_Page.Visible = false;
                print_button.Visible = false;
                Cateteogry_wise.Visible = false;
                DivCategoryWise.Visible = false;
              //  ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = false;
                Div_SubCategoryWise.Visible = false;
               // Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = false;
                Div_Category_unit_size.Visible = false;
                tr_order_by.Visible = false;
                State_wise_Report.Visible=false;
                City_wise_report.Visible=false;
                Turn_over_wise_tr.Visible=false;
                tr_monetary_limit.Visible = false;
                tr_page.Visible = false;
              //  DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }

        }
        else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text=="Amendment" || ddlSelectCriteria.SelectedItem.Text=="Renewal")
        {
            try
            {
               // Bind_All_Control();
                txtName.Text = "";
                ddlType.Visible = false;
                tr_search_company.Visible =true;
                txt_search_company.Text = "";
                provisionalReport.Visible = false;
                lst_type.Visible = false;
                lst_statuswise.Visible = false;
                DateWise.Visible = true;
                BranchWise.Visible = true;
                Zonewise.Visible = true;
                DateWise_div.Visible = true;
                Zonwise_Div.Visible = true;
                Branchwise_div.Visible = true;
                Report_Page.Visible = false;
                print_button.Visible = false;
                CompanyWise.Visible = false;
                grd_print_certificate.Visible = false;
                print_button.Visible = false;
                Report_Page.Visible = false;
                btn_report_ReportPage.Visible = true;
                Cateteogry_wise.Visible = true;
                DivCategoryWise.Visible = true;
               // ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = true;
                Div_SubCategoryWise.Visible = true;
              //  Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = true;
                Div_Category_unit_size.Visible = true;
              //  DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                tr_order_by.Visible = true;
                State_wise_Report.Visible = true;
                City_wise_report.Visible = true;
                Turn_over_wise_tr.Visible = true;
                tr_monetary_limit.Visible = true;
                tr_page.Visible = false;
                DetailsView1.PageIndex = 0;

                ddl_dropdownlist_custompageing.Items.Clear();

                        ddl_custom_paging.Items.Clear();
                        Bind_ddl_custom_paging();
                        ddlstatuswise.Items.Clear();
                        Bindstatuswise();

                if (ddlBranchWise.SelectedValue != "0" || ddlBranchWise.SelectedValue != "")
                {
                    int Branch_Id = Convert.ToInt32(ddlBranchWise.SelectedValue);
                    BindStatewise(Branch_Id);
                }
               /* if (ddl_state_wise.SelectedValue != "0" || ddl_state_wise.SelectedValue != "")
                {
                    int State_Id = Convert.ToInt32(ddl_state_wise.SelectedValue);
                    Bind_City_wise_data(State_Id);
                }*/
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
                txtName.Text = "";
            //  Bind_All_Control();
                // lst_stauswise_div.Visible = false;
                tr_search_company.Visible = true;
                txt_search_company.Text = "";
                provisionalReport.Visible = true;
                lst_statuswise.Visible = true;
                DateWise.Visible = true;
                DateWise_div.Visible = true;
                Zonewise.Visible = true;
                Zonwise_Div.Visible = true;
                BranchWise.Visible = true;
                Branchwise_div.Visible = true;
                Report_Page.Visible = false;
                CompanyWise.Visible = false;
                lst_type.Visible = false;
                grd_print_certificate.Visible = false;
                print_button.Visible = false;
                btn_report_ReportPage.Visible = true;
                Cateteogry_wise.Visible = true;
                DivCategoryWise.Visible = true;
             //   ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = true;
                Div_SubCategoryWise.Visible = true;
              //  Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";
                tr_order_by.Visible = true;
                Category_unit_size.Visible = true;
                Div_Category_unit_size.Visible = true;
                State_wise_Report.Visible = true;
                City_wise_report.Visible = true;
                Turn_over_wise_tr.Visible = true;
                tr_monetary_limit.Visible = true;
                tr_page.Visible = false;
                DetailsView1.PageIndex = 0;
                ddl_dropdownlist_custompageing.Items.Clear();

                ddl_custom_paging.Items.Clear();
                Bind_ddl_custom_paging();
                ddlstatuswise.Items.Clear();
                Bindstatuswise();

               // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                if (ddlBranchWise.SelectedValue != "0" || ddlBranchWise.SelectedValue != "")
                {
                    int Branch_Id = Convert.ToInt32(ddlBranchWise.SelectedValue);
                    BindStatewise(Branch_Id);
                }
               /* if (ddl_state_wise.SelectedValue != "0" || ddl_state_wise.SelectedValue != "")
                {
                    int State_Id = Convert.ToInt32(ddl_state_wise.SelectedValue);
                    Bind_City_wise_data(State_Id);
                }*/
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
                Bind_All_Control();
                tr_search_company.Visible = false;
                txt_search_company.Text = "";
                txtName.Text = "";
                DateWise.Visible = true;

                Zonewise.Visible = true;
                provisionalReport.Visible = false;
                BranchWise.Visible = true;
                Selection_crietria.Visible = true;
                Div_companyWise.Visible = true;
                CompanyWise.Visible = true;
                ddlType.Visible = true;
                lst_type.Visible = true;
                grd_print_certificate.Visible = false;
                lst_statuswise.Visible = false;
                print_button.Visible = false;
                btn_report_ReportPage.Visible = true;
                Report_Page.Visible = false;
                Cateteogry_wise.Visible = false;
                DivCategoryWise.Visible = false;
                //ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = false;
                Div_SubCategoryWise.Visible = false;
               // Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = false;
                Div_Category_unit_size.Visible = false;
                tr_order_by.Visible = true;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                tr_page.Visible = false;
               // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
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
                tr_search_company.Visible =false;
                txt_search_company.Text = "";
                ddlType.Visible = false;
                lst_type.Visible = false;
                lst_statuswise.Visible = false;
                provisionalReport.Visible = false;
                DateWise_div.Visible = true;
                Zonwise_Div.Visible = true;
                Branchwise_div.Visible = true;
                Report_Page.Visible = false;
                print_button.Visible = false;
                grd_print_certificate.Visible = false;
                CompanyWise.Visible = false;
                print_button.Visible = false;
                btn_report_ReportPage.Visible = true;
                Report_Page.Visible = false;
                Cateteogry_wise.Visible = false;
                DivCategoryWise.Visible = false;
                //ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = false;
                Div_SubCategoryWise.Visible = false;
               // Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = false;
                Div_Category_unit_size.Visible = false;
                tr_order_by.Visible = true;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                tr_page.Visible = false;
               // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
                ddlstatuswise.Items.Clear();
               // Bindstatuswise();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
        else if (ddlSelectCriteria.SelectedItem.Text == "Print Certificate")
        {
            try
            {
                txtName.Text = "";
                tr_search_company.Visible = false;
                txt_search_company.Text = "";
                CompanyWise.Visible = true;
                Div_companyWise.Visible = true;
                provisionalReport.Visible = false;
                BindCompany();
                DateWise.Visible = true;
                DateWise_div.Visible = true;
                Zonewise.Visible = true;
                Zonwise_Div.Visible = true;
                BranchWise.Visible = true;
                Branchwise_div.Visible = true;
    // BindGrid_Report_print_access();
                ddlType.Visible = false;
                lst_type.Visible = false;
                lst_statuswise.Visible = false;
               
                Report_Page.Visible = false;
                print_button.Visible = false;
              
                grd_print_certificate.Visible = true;
                btn_report_ReportPage.Visible = true;
                Cateteogry_wise.Visible = false;
                DivCategoryWise.Visible = false;
           //     ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = false;
                Div_SubCategoryWise.Visible = false;
              //  Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = false;
                Div_Category_unit_size.Visible = false;
                tr_order_by.Visible = true;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                tr_page.Visible = false;
               // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
            }
            catch(Exception ex)
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
                tr_search_company.Visible = false;
                txt_search_company.Text = "";
                // DateWise_div.Visible = true;
                DateWise.Visible = true;
                // Zonwise_Div.Visible = true;
                Zonewise.Visible = true;
                //Branchwise_div.Visible = true;
                BranchWise.Visible = true;
                Selection_crietria.Visible = true;
                Div_companyWise.Visible = false;
                CompanyWise.Visible = false;
                lst_type.Visible = false;
                provisionalReport.Visible = false;
                lst_statuswise.Visible = false;
                btn_report_ReportPage.Visible = true;
                Report_Page.Visible = false;
                ddlcomapnywise.Items.Clear();
                //ddlstatuswise.SelectedItem.Text = "--Select--";
                Cateteogry_wise.Visible = false;
                DivCategoryWise.Visible = false;
             //   ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

                Sub_Category_wise_ddl.Visible = false;
                Div_SubCategoryWise.Visible = false;
              //  Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

                Category_unit_size.Visible = false;
                Div_Category_unit_size.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                tr_page.Visible = false;
              // DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }

        }
    }
    protected void ddlZonewise_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlZonewise.SelectedValue != "")
            {
                int ZoneId = Convert.ToInt32(ddlZonewise.SelectedValue);
                DTZone = new DataTable();
                DTZone = clsDDL.ddlBranch_Activated_user_specific(UserId, ZoneId);
                ddlBranchWise.DataTextField = DTZone.Columns[2].Caption;
                ddlBranchWise.DataValueField = DTZone.Columns[0].Caption;
                ddlBranchWise.DataSource = DTZone;
                ddlBranchWise.DataBind();
            }
            else
            {
                ddlBranchWise.Items.Insert(0, new ListItem("--Select Branch--", ""));
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void BindZonewise()
    {
        try
        {
            int ZoneId = Convert.ToInt32(ddlZonewise.SelectedValue);
            DTZone = new DataTable();
            DTZone = clsDDL.ddlBranch_Activated_user_specific(UserId, ZoneId);
            ddlBranchWise.DataTextField = DTZone.Columns[2].Caption;
            ddlBranchWise.DataValueField = DTZone.Columns[0].Caption;
            ddlBranchWise.DataSource = DTZone;
            ddlBranchWise.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void BindZoneByRoleID()
    {
        try
        {
            DTZone = new DataTable();
           //// UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());

            DTZone = clsDDL.ddlBranchReport(UserId);

            ddlZonewise.DataTextField = DTZone.Columns[1].Caption;
            ddlZonewise.DataValueField = DTZone.Columns[0].Caption;
            ddlZonewise.DataSource = DTZone;
            ddlZonewise.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        

    }

    public void BindGridView()
    {
        string turnoverfrom = "";
        string turnoverto = "";
        string Monetarylimitfrom = "";
        string Monetarylimitto = "";
        string SearchCompany_name = "";
        lbl_show_message.Text = "";
        try
        {
          if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
            {
                FormType = "3";
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled") 
            {
                FormType = "1";
            }
            else if(ddlSelectCriteria.SelectedItem.Text=="Renewal")
            {
                 FormType="4";
                
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
            if (ddl_state_wise.SelectedValue == "" || ddl_state_wise.SelectedItem.Text == "--Select State--" || ddl_state_wise.SelectedValue == "0")
            {
                State_Id = 0;
            }
            else
            {
                State_Id = Convert.ToInt32(ddl_state_wise.SelectedValue);
            }
            if (Ddl_City_Wise.SelectedValue == "" || Ddl_City_Wise.SelectedItem.Text == "--Select City--" || Ddl_City_Wise.SelectedValue == "0")
            {
                City_Id = 0;
            }
            else
            {
                City_Id = Convert.ToInt32(Ddl_City_Wise.SelectedValue);
            }
            if (txt_turn_over_wise.Text == "")
            {
                turnoverfrom = " ";
            }
            else
            {
                turnoverfrom = txt_turn_over_wise.Text;
            }
            if (txt_turnover_wise_to.Text == "")
            {
                turnoverto = " ";
            }
            else
            {
                turnoverto = txt_turnover_wise_to.Text;
            }
            if (txt_monetary_limit_from.Text == "")
            {
                Monetarylimitfrom = " ";
            }
            else
            {
                Monetarylimitfrom = txt_monetary_limit_from.Text;
            }
            if (txt_monetarylimit_to.Text == "")
            {
                Monetarylimitto = " ";
            }
            else
            {
                Monetarylimitto = txt_monetarylimit_to.Text;
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
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                Orderby_User = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                Orderby_User = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                Orderby_User = "3";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Transfer Cases")
            {
                Orderby_User = "4";
            }
            Status = ddlstatuswise.SelectedItem.Text;
            if (Status == "Registered")
            {
                DetailsView1.Columns[2].Visible = true;// = "Registration Date";
                DetailsView1.Columns[1].Visible = true;

            }
            else
            {
                DetailsView1.Columns[1].Visible = true;// = DetailsView1.Columns[1].HeaderText;

            }
            // Status = ddlstatuswise.SelectedItem.Text;

            if (txt_search_company.Text == "")
            {
                SearchCompany_name = "";
            }
            else
            {
                SearchCompany_name = txt_search_company.Text.Trim().ToString();
            }


            objcompany = new clsCompany();
            DataSet dsgridview = new DataSet();
            if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
            {

                dsgridview = objcompany.Company_WaitingCompany_Amedment_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_Id, City_Id, turnoverfrom, turnoverto, Monetarylimitfrom, Monetarylimitto, CategoryID, SubCategoryID, CastID, SearchCompany_name, Orderby_User);
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_Id, City_Id, turnoverfrom, turnoverto, Monetarylimitfrom, Monetarylimitto, CategoryID, SubCategoryID, CastID, SearchCompany_name, Orderby_User);
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Renewal")
            {
                dsgridview = objcompany.Company_WaitingCompany_Renewal_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_Id, City_Id, turnoverfrom, turnoverto, Monetarylimitfrom, Monetarylimitto, CategoryID, SubCategoryID, CastID, SearchCompany_name, Orderby_User);
               // DetailsView1.Columns[5].HeaderText = "";
               // DetailsView1.Columns[5].Visible = false;
                   
            }

            //dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, CategoryID, SubCategoryID, CastID);

            if (Status != "--Select--")
            {
                DataTable dt = new DataTable();
                DataTable dtresult = new DataTable();
                if (DdlProvisinal.SelectedValue == "0")
                {

                    // Change here  tommarow--------------------------------------

                    dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_Id, City_Id, turnoverfrom, turnoverto, Monetarylimitfrom, Monetarylimitto, CategoryID, SubCategoryID, CastID, SearchCompany_name, Orderby_User);
                  //  var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).Count();
                    var count1 = dsgridview.Tables[0].AsEnumerable().Count();
                    if (count1 > 0)
                    {
                       // dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0);

                        dsgridview.Tables[0].AsEnumerable();
                       // dt = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).CopyToDataTable();
                        dt = dsgridview.Tables[0].AsEnumerable().CopyToDataTable();
                    }
                    else
                    {
                        dsgridview.Dispose();
                    }
                }
                else
                {

                    dsgridview = objcompany.Company_WaitingCompany_Registration_Report_Modified(dateFrom, dateto, BranchID, ZoneID, State_Id, City_Id, turnoverfrom, turnoverto, Monetarylimitfrom, Monetarylimitto, CategoryID, SubCategoryID, CastID, SearchCompany_name, Orderby_User);
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
              //  dt = dsgridview.Tables[0];
                if (dt.Rows.Count > 0)
                {

                    DataTable dtFilteredTable = new DataTable();
                    if (Status == "Expired list company")
                    {
                        //Status = "Registered";
                       /* var count1 = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status)
                           .Where(a => DateTime.Parse( a["RegistraionDate"].ToString()) > DateTime.Parse(dateFrom.ToString()))
                              .Where(a => (DateTime.Parse( a["RegistraionDate"].ToString()).AddDays(29) < DateTime.Parse(dateto.ToString()))).ToList();*/
                         //  var count2=dt.AsEnumerable().Where(a => Convert.ToString(a["RegistraionDate"].ToString())= Convert.ToString( dateto.ToString()))
                              // .Where(a => Convert.ToDateTime(a["RegistraionDate"].ToString().AddDay(729))=Convert.ToDateTime(dateFrom.ToString())).Count();
                        /* string[] formats= { "MM/dd/yyyy" };
                        var Count4 = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status)
                         .Where(a => DateTime.ParseExact(a["RegistraionDate"].ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None) > DateTime.ParseExact(TextBox1.Text.ToString(), formats, new CultureInfo("en-US"), DateTimeStyles.None)).ToList();*/
                       // DataRow [] result=dt.Select( Status = "Registered" && 
                       /* var count1 = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status)
                           .Where(a => a["RegistraionDate"] > dateFrom)
                              .Where(a => (DateTime.Parse( a["RegistraionDate"].ToString()).AddDays(29) < DateTime.Parse(dateto.ToString()))).ToList();*/
                      
                        try
                        {
                            dtresult = dt.AsEnumerable().Where(row => row.Field<string>("STATUS") == "Registered"
                                            && row.Field<DateTime>("RegistraionDate") > Convert.ToDateTime(dateFrom.ToString())
                                             && row.Field<DateTime>("RegistraionDate_expiered") < Convert.ToDateTime(dateto.ToString())).OrderByDescending(row => row.Field<String>("STATUS")).CopyToDataTable();
                        }
                        catch (Exception ex)
                        {
                            ErrHandler.WriteError(ex.Message);
                        }
                         // DataRow[] result = dt.Select("STATUS" == Status &&  Convert.ToDateTime( "RegistraionDate") <  Convert.ToDateTime( dateFrom.ToString()) &&  Convert.ToDateTime("RegistraionDate_expiered") > Convert.ToDateTime( dateto.ToString()));
                    }
                    else
                    {
                    }
                    var count=0;
                    if (Status == "Expired list company")
                    {
                        dt = dtresult.Copy();
                        count = dt.AsEnumerable().Count();
                    }
                    else
                    {
                        count = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status).Count();
                    }
                    int countStatus = Convert.ToInt32(count);
                    if (count > 0)
                    {
                      //  dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status).OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                        if (Status == "Expired list company")
                        {

                            dtFilteredTable = dt.Copy();
                            Temp = dtFilteredTable.Copy();
                        }
                        else
                        {
                            dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToString(a["STATUS"].ToString()) == Status).CopyToDataTable();
                            Temp = dtFilteredTable.Copy();
                        }

                        //var qu = from n in dt.AsEnumerable().Where(Status==
                        //         select n;



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
                           // DetailsView1.PageIndex = 0;
                            DetailsView1.DataSource = dv;
                            DetailsView1.DataBind();
                            lbl_search_company_name.Visible = true;
                            txtName.Visible = true;
                            cmdSearch.Visible = true;
                            cmdClear.Visible = true;

                            Detailedviewid.Visible = true;

                            lbl_show_text.Visible = true;

                            detailview1_td.Visible = true;
                            lbl_textshow.Visible = true;
                            Report_Page.Attributes.Add("class", "dispalyshowhidegrid");
                            print_button.Attributes.Add("class", "dispalyshowhidegrid");
                            tr_page.Attributes.Add("class", "dispalyshowhidegrid");
                        }
                        else
                        {
                            Temp.Dispose();
                            dv.Dispose();
                            DetailsView1.DataBind();

                            Report_Page.Attributes.Add("class", "displayshowhide");
                            print_button.Attributes.Add("class", "displayshowhide");
                            tr_page.Attributes.Add("class", "displayshowhide");
                        }



                        lblMessage.Text = "";
                        if (ddlSelectCriteria.SelectedItem.Text == "Renewal Forms Filled")
                        {
                            FormType = "4";
                            //DetailsView1.Columns[5].HeaderText = "";
                           // DetailsView1.Columns[5].Visible = false;
                        }
                        lbl_total_record.Text = dtFilteredTable.Rows.Count.ToString();
                       // DetailsView1.DataSource = dtFilteredTable;
                      //  DetailsView1.DataBind();
                        DetailsView1.PageIndex = 0;
                        print_button.Visible = true;
                        tr_print_button.Visible = true;
                        Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                    }
                    else
                    {
                        // lblMessage.Text = "N0 Record Found!!!";
                        Report_Type.Text = "N0 Record Found!!!";
                        lbl_total_record.Text = "0";
                        DetailsView1.Dispose();
                        DetailsView1.DataBind();
                        dsgridview.Dispose();
                        Temp.Dispose();
                         lbl_search_company_name.Visible=false;
     txtName.Visible=false;
	cmdSearch.Visible=false;
      cmdClear.Visible=false;

    Detailedviewid.Visible=false;

lbl_show_text.Visible=false;

detailview1_td.Visible=false;
lbl_textshow.Visible = false;
                        // Report_Type.Text = "";
                        print_button.Visible = false;
                        tr_print_button.Visible = false;
                        Report_Page.Attributes.Add("class", "displayshowhide");
                        print_button.Attributes.Add("class", "displayshowhide");
                        tr_page.Attributes.Add("class", "displayshowhide");
                        lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                    }
                }
                else
                {
                    Report_Type.Text = "N0 Record Found!!!";
                    lbl_total_record.Text = "0";
                    DetailsView1.Dispose();
                    DetailsView1.DataBind();
                    dsgridview.Dispose();
                    Temp.Dispose();
                    print_button.Visible = false;
                    tr_print_button.Visible = false;
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;

                    Detailedviewid.Visible = false;

                    lbl_show_text.Visible = false;

                    detailview1_td.Visible = false;
                    lbl_textshow.Visible = false;
                    Report_Page.Attributes.Add("class", "displayshowhide");
                    print_button.Attributes.Add("class", "displayshowhide");
                    tr_page.Attributes.Add("class", "displayshowhide");
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
                    {
                        if (DdlProvisinal.SelectedValue == "0")
                        {
                          //  var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).Count();
                            var count1 = dsgridview.Tables[0].AsEnumerable().Count();
                            if (count1 > 0)
                            {
                               // dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0);
                                dsgridview.Tables[0].AsEnumerable();
                               // dtFilterReg = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 0).CopyToDataTable();
                                dtFilterReg = dsgridview.Tables[0].AsEnumerable().CopyToDataTable();//.Where(a => Convert.ToInt32(a["Provisional"]) == 0).CopyToDataTable();

                                Temp = dtFilterReg.Copy();
                            }
                            else
                            {
                                dtFilterReg.Dispose();
                                Temp.Dispose();
                            }
                        }
                        else
                        {
                            var count1 = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).Count();
                            if (count1 > 0)
                            {
                                dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1);
                                dtFilterReg = dsgridview.Tables[0].AsEnumerable().Where(a => Convert.ToInt32(a["Provisional"]) == 1).CopyToDataTable();
                                Temp = dtFilterReg.Copy();
                            }
                            else
                            {
                                dtFilterReg.Dispose();
                                Temp.Dispose();
                            }
                        }
                    }
                    else
                    {
                        dtFilterReg = dsgridview.Tables[0];
                    }

                    //dtFilterReg = dsgridview.Tables[0];

                    if (dtFilterReg.Rows.Count > 0)
                    {

                        var count = dtFilterReg.AsEnumerable().Count();
                        int countStatus = Convert.ToInt32(count);
                        if (count > 0)
                        {
                            DataTable dtFilterRegFilter = new DataTable();
                            if (FormType == "3")
                            {
                               // dtFilterRegFilter = dtFilterReg.AsEnumerable().OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                                dtFilterRegFilter = dtFilterReg.AsEnumerable().CopyToDataTable();
                                Temp = dtFilterRegFilter.Copy();
                            }
                            else
                            {
                                //dtFilterRegFilter = dtFilterReg.AsEnumerable().OrderByDescending(row => row.Field<int>("fGPtbl_Company_Id")).CopyToDataTable();
                                dtFilterRegFilter = dtFilterReg.AsEnumerable().CopyToDataTable();
                                Temp = dtFilterRegFilter.Copy();
                            }

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
                                if (FormType == "4")
                                {
                                   // DetailsView1.Columns[5].Visible = false;
                                }

                                //DetailsView1.PageIndex = 0;
                                DetailsView1.DataSource = dv;
                                if (FormType == "4")
                                {
                                    //DetailsView1.Columns[5].HeaderText = "";
                                   // DetailsView1.Columns[5].HeaderStyle.Width = Unit.Pixel(0);
                                   // DetailsView1.Columns[5].Visible = false;
                                }
                                DetailsView1.DataBind();
                            }
                            else
                            {
                                Temp.Dispose();
                                dv.Dispose();
                                DetailsView1.DataBind();
                            }
                            lblMessage.Text = "";
                            lbl_total_record.Text = dtFilterRegFilter.Rows.Count.ToString();
                           // DetailsView1.DataSource = dtFilterRegFilter;
                           // DetailsView1.DataBind();
                            //DetailsView1.PageIndex = 0;
                            print_button.Visible = true;
                            tr_print_button.Visible = true;
                            Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                            if (FormType == "4")
                            {
                               // DetailsView1.Columns[5].HeaderText = "";
                               // DetailsView1.Columns[5].Visible = false;
                            }


                            DetailsView1.DataBind();
                            lbl_search_company_name.Visible = true;
                            txtName.Visible = true;
                            cmdSearch.Visible = true;
                            cmdClear.Visible = true;

                            Detailedviewid.Visible = true;

                            lbl_show_text.Visible = true;

                            detailview1_td.Visible = true;
                            lbl_textshow.Visible = true;
                            Report_Page.Attributes.Add("class", "dispalyshowhidegrid");
                            print_button.Attributes.Add("class", "dispalyshowhidegrid");
                            tr_page.Attributes.Add("class", "dispalyshowhidegrid");
                        }
                        else
                        {
                            // lblMessage.Text = "N0 Record Found!!!";
                            Report_Type.Text = "N0 Record Found!!!";
                            lbl_total_record.Text = "0";
                            DetailsView1.Dispose();
                            DetailsView1.DataBind();
                            dsgridview.Dispose();
                            // Report_Type.Text = "";
                            print_button.Visible = false;
                            tr_print_button.Visible = false;
                            Temp.Dispose();
                            lbl_search_company_name.Visible = false;
                            txtName.Visible = false;
                            cmdSearch.Visible = false;
                            cmdClear.Visible = false;

                            Detailedviewid.Visible = false;

                            lbl_show_text.Visible = false;

                            detailview1_td.Visible = false;
                            lbl_textshow.Visible = false;
                            Report_Page.Attributes.Add("class", "displayshowhide");
                            print_button.Attributes.Add("class", "displayshowhide");
                            tr_page.Attributes.Add("class", "displayshowhide");
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
                    lbl_search_company_name.Visible = false;
                    txtName.Visible = false;
                    cmdSearch.Visible = false;
                    cmdClear.Visible = false;

                    Detailedviewid.Visible = false;

                    lbl_show_text.Visible = false;

                    detailview1_td.Visible = false;
                    lbl_textshow.Visible = false;
                    Report_Page.Attributes.Add("class", "displayshowhide");
                    print_button.Attributes.Add("class", "displayshowhide");
                    tr_page.Attributes.Add("class", "displayshowhide");
                    lbl_show_message.Text = "<span style='color:red;'>" + "No Record Found" + "</span>";
                }
            }
            if (ddl_custom_paging.SelectedValue != "")
            {
                if (ddl_custom_paging.SelectedValue != "10")
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                   // DetailsView1.DataBind();
                }
                else if (ddl_dropdownlist_custompageing.SelectedValue != "10")
                {
                    DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                 //  DetailsView1.DataBind();
                }

               // DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
            }
            else
            {
               // DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
            }
            //if (DetailsView1.PageIndex == 0)
            //{
            //    lbl_pageindex.Text = "1";
            //    lbl_page_count_deatailsview.Text = DetailsView1.PageCount.ToString();
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void Report_onRowdataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                 // Application Date/Validity
                e.Row.Cells[3].Text = "Registration No/Validity";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[1].Text);
            e.Row.Cells[1].Text = decodedText;
            string decodeText_contact_details = HttpUtility.HtmlDecode(e.Row.Cells[2].Text);
            e.Row.Cells[2].Text = decodeText_contact_details;
            string DecodeText_zone_branch = HttpUtility.HtmlDecode(e.Row.Cells[5].Text);
            e.Row.Cells[5].Text = DecodeText_zone_branch.ToString();
            e.Row.Cells[3].Text = HttpUtility.HtmlDecode(e.Row.Cells[3].Text);
              
            Label lblcompany_name = (Label)e.Row.FindControl("LblComapnY_Name");
            Label lblCompanyId = (Label)e.Row.FindControl("lblCompany_Id");
            Label lblBranchName = (Label)e.Row.FindControl("lblBranch_Name");
            int Compny_Id = Convert.ToInt32(lblCompanyId.Text);
            string Company_Name = Convert.ToString(lblcompany_name.Text);
            Label lbluid = (Label)e.Row.FindControl("lblunitid");
            int Unit_ID = Convert.ToInt32(lbluid.Text);
           // Label lblFormtype = (Label)e.Row.FindControl("lblFormtype");
           
            if (ddlSelectCriteria.SelectedItem.Text == "Amendment")
            {
                FormType_Id = 3;
                DetailsView1.Columns[6].Visible = true;
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text=="Companies List")
            {
                FormType_Id = 1;//Convert.ToInt32(lblFormtype.Text);
                DetailsView1.Columns[6].Visible = true;
            }
            else if (ddlSelectCriteria.SelectedItem.Text == "Renewal")
            {
                FormType_Id = 4;
                
                DetailsView1.Columns[6].Visible = false;
            }
          //  int FormType_Id = Convert.ToInt32(lblFormtype.Text);
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

                string EncryptStatus=objDecEnc.Encrypt("1");
                  string EncryptUrlId=objDecEnc.Encrypt("999");
                 // hyper.NavigateUrl = "UnitAmendment_ViewChanges.aspx?c=" + EncryptCompanyID + "&u=" + EncryptID + "&s=" + EncryptStatus + "&b=" + EncryptBranchid + "&i=" + EncryptUrlId;
                  hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                  hyper.Text = "view";

            }

            else if (FormType_Id == 4)
            {
                string EncryptStatus = objDecEnc.Encrypt("1");
                string EncryptUrlId = objDecEnc.Encrypt("2");
                // hyper.NavigateUrl = "UnitAmendment_ViewChanges.aspx?c=" + EncryptCompanyID + "&u=" + EncryptID + "&s=" + EncryptStatus + "&b=" + EncryptBranchid + "&i=" + EncryptUrlId;
                //hyper.NavigateUrl = "RenewalformField.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;

                hyper.Text = "view";
            }

        }


    }
    protected void btn_report_ReportPage_Click(object sender, EventArgs e)
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
 //  if (ddlcomapnywise.SelectedItem.Text == "--Select Company--")
          if(ddlcomapnywise.SelectedValue=="")
        {
            CompanyId = 0;
        }
        else
        {
            CompanyId = Convert.ToInt32(ddlcomapnywise.SelectedValue);
        }
        if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List" || ddlSelectCriteria.SelectedItem.Text=="Amendment" || ddlSelectCriteria.SelectedItem.Text=="Renewal" )
        {
            Report_Page.Visible = true;
            print_button.Visible = true;
            if (ddlSelectCriteria.SelectedItem.Text != "Companies List")
            {
               // ddlstatuswise.SelectedItem.Text = "--Select--";
                ddlstatuswise.Items.Clear();
                Bindstatuswise();
                
            }
            ddlType.SelectedItem.Text = "--Select--";
        // ddlcomapnywise.SelectedItem.Text = "--Select Company--";
            grd_Branch_Wise.Visible = false;
            grd_Branch_Wise.Visible = false;
            grd_Company_wiseData.Visible = false;
            grd_inspectionAgency.Visible = false;
            grd_user_list_report.Visible = false;
            grd_print_certificate.Visible = false;
            State_wise_Report.Visible=true;
            City_wise_report.Visible=true;
            Turn_over_wise_tr.Visible=true;
            tr_monetary_limit.Visible = true;
           // DetailsView1.PageIndex = 0;

            BindGridView();
            tr_page.Visible = true;
             lbl_search_company_name.Visible=true;
     txtName.Visible=true;
	cmdSearch.Visible=true;
      cmdClear.Visible=true;

    Detailedviewid.Visible=true;

lbl_show_text.Visible=true;

detailview1_td.Visible=true;
lbl_textshow.Visible = true;

            //Bind_ddl_custom_paging();
        }
        else if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
        {
           
            if (txt_21.Text != "" && TextBox1.Text != "" && ZoneID != 0 && BranchID==0 && CompanyId==0)
            {
                DetailsView1.Visible = false;
                grd_Company_wiseData.Visible = true ;
                grd_Branch_Wise.Visible = true;
                grd_report_paymentDetails.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_inspectionAgency.Visible = false;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                BindgridviewBranchwiseGrid();
                tr_page.Visible = false;
                lbl_search_company_name.Visible=false;
                txtName.Visible=false;
                cmdSearch.Visible=false;
                cmdClear.Visible=false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;


            }
            else if (txt_21.Text != "" && TextBox1.Text != "" && ZoneID==0 && BranchID==0 && CompanyId==0)
            {
                DetailsView1.Visible = false;
                grd_Branch_Wise.Visible = true;
                grd_Company_wiseData.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_report_paymentDetails.Visible = false;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                BindgridviewBranchwiseGrid();
                tr_page.Visible = false;
                lbl_search_company_name.Visible = false;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;

            }
            else if (txt_21.Text != "" && TextBox1.Text != "" && ZoneID != 0 && BranchID != 0 && CompanyId == 0)
            {
                DetailsView1.Visible = false;
                grd_Branch_Wise.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_Company_wiseData.Visible = true;
                grd_inspectionAgency.Visible = false;
                grd_report_paymentDetails.Visible = false;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                Bind_GridView_BranchZoneWise();
                tr_page.Visible = false;
                lbl_search_company_name.Visible = false;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;

            }
            else if (txt_21.Text == "" && TextBox1.Text == "" && ZoneID != 0 && BranchID != 0 && CompanyId == 0)
            {
                DetailsView1.Visible = false;
                grd_Branch_Wise.Visible = false;
                grd_report_paymentDetails.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_Company_wiseData.Visible = true;
                grd_inspectionAgency.Visible = false;
                grd_report_paymentDetails.Visible = false;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                Bind_GridView_BranchZoneWise();
                tr_page.Visible = false;
                lbl_search_company_name.Visible = false;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;

            }
            else if (txt_21.Text == "" && TextBox1.Text == "" && ZoneID != 0 && BranchID != 0 && CompanyId != 0)
            {
                DetailsView1.Visible = false;
                grd_Branch_Wise.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_Company_wiseData.Visible = false;
                grd_inspectionAgency.Visible = false;
                grd_report_paymentDetails.Visible = true;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                Bind_Payment_Details();
                tr_page.Visible = false;
                lbl_search_company_name.Visible = false;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;

            }
            else if (txt_21.Text != "" && TextBox1.Text != "" && ZoneID != 0 && BranchID != 0 && CompanyId != 0)
            {
                DetailsView1.Visible = false;
                grd_Branch_Wise.Visible = false;
                grd_print_certificate.Visible = false;
                grd_user_list_report.Visible = false;
                Report_Page.Visible = true;
                print_button.Visible = true;
                grd_Company_wiseData.Visible = false;
                grd_report_paymentDetails.Visible = true;
                grd_inspectionAgency.Visible = false;
                State_wise_Report.Visible = false;
                City_wise_report.Visible = false;
                Turn_over_wise_tr.Visible = false;
                tr_monetary_limit.Visible = false;
                Bind_Payment_Details();
                tr_page.Visible = false;
                lbl_search_company_name.Visible = false;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;
                ddl_dropdownlist_custompageing.Visible = false;
                lbl_search_company_name.Visible = true;
                txtName.Visible = false;
                cmdSearch.Visible = false;
                cmdClear.Visible = false;

                Detailedviewid.Visible = false;

                lbl_show_text.Visible = false;

                detailview1_td.Visible = false;
                lbl_textshow.Visible = false;
            }
                 
        }
        else if (ddlSelectCriteria.SelectedItem.Text == "Inspection Agency List")
        {
            DetailsView1.Visible = false;
            grd_Branch_Wise.Visible = false;
            grd_user_list_report.Visible = false;
            grd_print_certificate.Visible = false;
            Report_Page.Visible = true;
            print_button.Visible = true;
            grd_Company_wiseData.Visible = false;
            grd_report_paymentDetails.Visible = false;
            grd_inspectionAgency.Visible = true;
            State_wise_Report.Visible = false;
            City_wise_report.Visible = false;
            Turn_over_wise_tr.Visible = false;
            tr_monetary_limit.Visible = false;
            BindInspectionAgency();
            tr_page.Visible = false;
            lbl_search_company_name.Visible = false;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;
            ddl_dropdownlist_custompageing.Visible = false;
            lbl_search_company_name.Visible = true;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;

            Detailedviewid.Visible = false;

            lbl_show_text.Visible = false;

            detailview1_td.Visible = false;
            lbl_textshow.Visible = false;

        }

        else if (ddlSelectCriteria.SelectedItem.Text == "Print Certificate")
        {
            DetailsView1.Visible = false;
            BindGrid_Report_print_access();
            grd_Branch_Wise.Visible = false;
            Report_Page.Visible = true;
            print_button.Visible = true;
            grd_Company_wiseData.Visible = false;
            grd_report_paymentDetails.Visible = false;
            grd_inspectionAgency.Visible = false;
            grd_print_certificate.Visible = true;
            grd_user_list_report.Visible = false;
            State_wise_Report.Visible = false;
            City_wise_report.Visible = false;
            Turn_over_wise_tr.Visible = false;
            tr_monetary_limit.Visible = false;
            tr_page.Visible = false;
            lbl_search_company_name.Visible = false;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;
            ddl_dropdownlist_custompageing.Visible = false;

            lbl_search_company_name.Visible = true;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;

            Detailedviewid.Visible = false;

            lbl_show_text.Visible = false;

            detailview1_td.Visible = false;
            lbl_textshow.Visible = false;
            
        }

        else if (ddlSelectCriteria.SelectedItem.Text == "User List")
        {
            DetailsView1.Visible = false;
            grd_Branch_Wise.Visible = false;
            grd_report_paymentDetails.Visible = false;
            grd_inspectionAgency.Visible = false;
            grd_Branch_Wise.Visible = false;
            grd_print_certificate.Visible = false;
            bindUserList();
            grd_user_list_report.Visible = true;
            print_button.Visible = true;
            lst_report_type.Visible = true;
            Report_Page.Visible = true;
            State_wise_Report.Visible = false;
            City_wise_report.Visible = false;
            Turn_over_wise_tr.Visible = false;
            tr_monetary_limit.Visible = false;
            tr_page.Visible = false;
            lbl_search_company_name.Visible = false;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;
            ddl_dropdownlist_custompageing.Visible = false;
            lbl_search_company_name.Visible = true;
            txtName.Visible = false;
            cmdSearch.Visible = false;
            cmdClear.Visible = false;

            Detailedviewid.Visible = false;

            lbl_show_text.Visible = false;

            detailview1_td.Visible = false;
            lbl_textshow.Visible = false;

        }

    }
    protected void DetailsViewPageindexChanging(object sender, GridViewPageEventArgs e)
    {

        DetailsView1.PageIndex = e.NewPageIndex;
                 
       // lbl_pageindex.Text = (DetailsView1.PageIndex + 1).ToString();
        //lbl_page_count_deatailsview.Text = DetailsView1.PageCount.ToString();
        detailviewpage_run.Visible = true;
        dtailview1_static.Visible = false;
        BindGridView();
        DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedItem.Text);
    }

    // Bind All controls 
    public void Bind_All_Control()
    {
        try
        {
         Selection_crietria.Visible = false;
         tr_search_company.Visible = false;
         txt_search_company.Text = "";
            CompanyWise.Visible = false;
           // user_list_type.Visible = false;
            //Remainder_Type_Div.Visible = false;

            DateWise.Visible = false;

            Zonewise.Visible = false; ;
           
            //ddlstatuswise.SelectedValue = "0";
            //txt_21.Text = "";
            //TextBox1.Text = "";
            
          
          //  ddlType.SelectedItem.Text == "--Select--";
            BranchWise.Visible = false; ;
            lst_type.Visible = false;
            btn_report_ReportPage.Visible = false;
            lst_statuswise.Visible = false;
            Report_Page.Visible = false;
            print_button.Visible = false;
            lbl_total_record.Text = "";
           // ddlstatuswise.Items.Clear();
           // ddlstatuswise.SelectedItem.Text = "--Select--";
            ddlstatuswise.Items.Clear();
            Bindstatuswise();
            Cateteogry_wise.Visible = false;
            DivCategoryWise.Visible = false;
            ddl_category_wise_report.SelectedItem.Text = "--Select Category Area--";

            Sub_Category_wise_ddl.Visible = false;
            Div_SubCategoryWise.Visible = false;
            Ddl_category_cast.SelectedItem.Text = "--Select Category Cast--";

            Category_unit_size.Visible = false;
            Div_Category_unit_size.Visible = false;
            DdlCategoryUnitSize.SelectedItem.Text = "--Select Category Unit Size--";
            tr_order_by.Visible = false;
            State_wise_Report.Visible = false;
            ddl_state_wise.Items.Clear();
            ddl_state_wise.Items.Insert(0, new ListItem("--Select State--", ""));
            City_wise_report.Visible = false;
            Ddl_City_Wise.Items.Clear();
            Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
            Turn_over_wise_tr.Visible = false;
            txt_turn_over_wise.Text = "";
            txt_turnover_wise_to.Text = "";
            tr_monetary_limit.Visible = false;
            txt_monetary_limit_from.Text = "";
            txt_monetarylimit_to.Text = "";
            tr_page.Visible = false;

           
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void onRowGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {

            ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            Response.Redirect("CompanyWiseRecord.aspx?id=" + e.CommandArgument + "&ZoneID=" + ZoneID, false);
        }


    }
    protected void grd_GridCompany_onRowdataBound(object sender, GridViewRowEventArgs e)
    {
        if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
        {
            ZoneID = 0;
        }
        else
        {
            ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
        }


        if (ddlType.SelectedItem.Text == "Registration")
        {
            if (dateto != "" && dateFrom != "" && ZoneID == 0)
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
        else if (ddlType.SelectedItem.Text == "Renewal")
        {
            grd_Branch_Wise.Columns[2].Visible = false;
            grd_Branch_Wise.Columns[4].Visible = false;
            grd_Branch_Wise.Columns[1].Visible = false;
        }
        else if (ddlType.SelectedItem.Text == "Amendment")
        {
            grd_Branch_Wise.Columns[1].Visible = false;
            grd_Branch_Wise.Columns[2].Visible = false;
            grd_Branch_Wise.Columns[3].Visible = false;
            grd_Branch_Wise.Columns[6].Visible = false;
        }
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




        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            if (dateFrom != null && dateto != null && ZoneID == 0)
            {
                Label lblZoneId = (Label)e.Row.FindControl("lnkZone_ID");
                int zoneIDLabel = Convert.ToInt32(lblZoneId.Text);
                Label lblZoneName = (Label)e.Row.FindControl("lnkZone_Name");
                string Page1 = "SeparateRecord";
                string UrlZone = "BranchWiseSearch.aspx?id1=" + zoneIDLabel + "&datefrom=" + dateFrom + "&dateTo=" + dateto + "&ZName=" + lblZoneName.Text + "&Page=" +Page1;
                lblZoneName.Text = "<a href='" + UrlZone + "'>" + lblZoneName.Text + "</a>";
            }
            else
            {
                Label lblBranchID = (Label)e.Row.FindControl("lbl_Branch_ID");
                int branchid1 = Convert.ToInt32(lblBranchID.Text);

                Label lblBrancName = (Label)e.Row.FindControl("lnk_Branc_Name");
                string url = "CompanyWiseRecord.aspx?id=" + branchid1 + "&ZoneID=" + ZoneID + "&BName=" + lblBrancName.Text;

                lblBrancName.Text = "<a href='" + url + "' >" + lblBrancName.Text + "</a>";
            }

        }


    }
    protected void Branch_wise_Paymeny_report_pageindexchange(object sender, GridViewPageEventArgs e)
    {
        grd_Branch_Wise.PageIndex = e.NewPageIndex;

        BindgridviewBranchwiseGrid();
        

    }

    public void BindgridviewBranchwiseGrid()
    {
        try
        {
            dateFrom = TextBox1.Text.ToString();
            dateto = txt_21.Text.ToString();
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                Orderby_User = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                Orderby_User = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                Orderby_User = "3";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Transfer Cases")
            {
                Orderby_User = "4";
            }
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
                if (dateFrom != null && dateto != null && ZoneID == 0)
                {
                    Ds_Branchwise = objcompany.Company_paymentetails_Pass_value_fromdate(dateFrom, dateto);
                    grd_Branch_Wise.Columns[1].Visible = false;
                }
                else
                {
                    grd_Branch_Wise.Columns[0].Visible = false;
                    Ds_Branchwise = objcompany.Company_PaymentDetails_Fetch_ZoneWise(dateFrom, dateto, ZoneID);
                }
                if (Ds_Branchwise.Tables[0].Rows.Count > 0)
                {
                    Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                    lbl_total_record.Text = Ds_Branchwise.Tables[0].Rows.Count.ToString();
                    grd_Branch_Wise.DataSource = Ds_Branchwise.Tables[0];
                    grd_Branch_Wise.DataBind();
                    grd_Branch_Wise.PageIndex = 0;
                    print_button.Visible = true;
                    Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;

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

    public void Bind_GridView_BranchZoneWise()
    {
        try
        {
            int ZoneID = Convert.ToInt32(ddlZonewise.SelectedValue);
            int BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);
            string formdate = TextBox1.Text.ToString();
            string todate = txt_21.Text.ToString();
            if (DDl_Order_BY.SelectedItem.Text == "Companywise")
            {
                Orderby_User = "1";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Registration  Date")
            {
                Orderby_User = "2";
            }
            if (DDl_Order_BY.SelectedItem.Text == "ZoneWise")
            {
                Orderby_User = "3";
            }
            if (DDl_Order_BY.SelectedItem.Text == "Transfer Cases")
            {
                Orderby_User = "4";
            }

            objcompany = new clsCompany();
            DataSet Myds_company = new DataSet();
            Myds_company = new DataSet();
            if (ZoneID != 0 && BranchID != 0 && formdate == "" && todate == "")
            {
                Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified(ZoneID, BranchID,Orderby_User);
            }
            else
            {
              //  Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise(ZoneID, BranchID, formdate, todate);
                Myds_company = objcompany.Company_PaymentDetails_Fetch_ZoneWise_Modified_DateWise_Modified(ZoneID, BranchID, formdate, todate, Orderby_User);
            }
            if (Myds_company.Tables[0].Rows.Count > 0)
            {
                lbl_total_record.Text = Myds_company.Tables[0].Rows.Count.ToString();
          
                grd_Company_wiseData.DataSource = Myds_company.Tables[0];
                grd_Company_wiseData.DataBind();
                grd_Company_wiseData.PageIndex = 0;
                print_button.Visible = true;
                Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
            }
            else
            {
                Report_Type.Text = "No Record Found!!!";
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
    protected void grdBranch_wise_dataBound(object sender, EventArgs e)
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
                Label lbltotalbranchwise1 = (Label)grd_Branch_Wise.FooterRow.FindControl("lblTotalAmmountbranchwise");
                lbltotalbranchwise1.Text = (Convert.ToDecimal(lblTotalAmount1.Text) + Convert.ToDecimal(lblTotal1.Text)).ToString();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }

        }
    }
    protected void grd_Company_wise_page_Index_changing(object sender, GridViewPageEventArgs e)
    {
        grd_Company_wiseData.PageIndex = e.NewPageIndex;

        Bind_GridView_BranchZoneWise();
        grd_Company_wiseData.Visible = true;
    }
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
    protected void grdCompany_Wise_Data(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (ddlType.SelectedItem.Text.ToString() == "Registration")
            {
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[5].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;
               
                grd_Company_wiseData.Columns[3].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            else if (ddlType.SelectedItem.Text.ToString() == "Inspection")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
               
                grd_Company_wiseData.Columns[4].Visible = false;

                grd_Company_wiseData.Columns[3].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            else if (ddlType.SelectedItem.Text == "Amendment")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[4].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            else if (ddlType.SelectedItem.Text == "Renewal")
            {
                grd_Company_wiseData.Columns[1].Visible = false;
                grd_Company_wiseData.Columns[2].Visible = false;
                grd_Company_wiseData.Columns[3].Visible = false;
                grd_Company_wiseData.Columns[7].Visible = false;
            }
            Label lblcompanyid = (Label)e.Row.FindControl("LBL_Company_Id_Data");
            Label lblcompanyName = (Label)e.Row.FindControl("LBl_company_Name_Data");
            int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
            string CompanyName = lblcompanyName.Text;
            string Page = "Comapnywise";
            string Page1 = "SeparateRecord";
            string url = "CompanyWiseSeparateRecord.aspx?cid=" + companyid + "&cname=" + CompanyName + "&Page1="+Page1;

            lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";


            // Ravish Some error label value not match so match the  label value

            Label lbl_registration_amount = (Label)e.Row.FindControl("lblRegiatrationAmmount2");
            Label lbl_inspection_amount = (Label)e.Row.FindControl("lbl_unit_ammount2");
            Label lbl_amedment_amount = (Label)e.Row.FindControl("lbl_amedment");
            Label lbl_renewal_amount = (Label)e.Row.FindControl("lbl_renewal");
            Label lbl_total_amount = (Label)e.Row.FindControl("lbl_total_amount");
            if (lbl_registration_amount.Text == "" || lbl_registration_amount.Text == null)
            {
                lbl_registration_amount.Text = "0.0"; 
            }
            if (lbl_inspection_amount.Text == "" || lbl_inspection_amount.Text == null)
            {
                lbl_inspection_amount.Text = "0.0";  
            }
            if (lbl_renewal_amount.Text == "" || lbl_renewal_amount.Text == null)
            {
                lbl_renewal_amount.Text = "0.0";
            }
            if (lbl_amedment_amount.Text == "" || lbl_amedment_amount.Text == null)
            {
                lbl_amedment_amount.Text = "0.0";
            }
            if (lbl_total_amount.Text == "" || lbl_total_amount.Text == null)
            {
                decimal total;
                total = Convert.ToDecimal(lbl_registration_amount.Text) + Convert.ToDecimal(lbl_inspection_amount.Text) + Convert.ToDecimal(lbl_amedment_amount.Text) + Convert.ToDecimal(lbl_renewal_amount.Text);
                lbl_total_amount.Text = Convert.ToString(total);
            }
            Registration_amount += Convert.ToDecimal(lbl_registration_amount.Text);
            Inspection_amount += Convert.ToDecimal(lbl_inspection_amount.Text);
            Amedment_amount += Convert.ToDecimal(lbl_amedment_amount.Text);
            Renewal_amount += Convert.ToDecimal(lbl_renewal_amount.Text);
            Total_amount += Convert.ToDecimal(lbl_total_amount.Text); 
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl_registration_footer = (Label)e.Row.FindControl("lblRegistrationTotal2");
            Label lbl_Inspection_footer = (Label)e.Row.FindControl("lblTotal2");
            Label lbl_amedment_footer = (Label)e.Row.FindControl("lbl_amedment_footer");
            Label lbl_renewal_footer = (Label)e.Row.FindControl("lbl_Renewal_footer");
            Label lbl_total_footer = (Label)e.Row.FindControl("lblTotal_companywise");


            lbl_registration_footer.Text = Convert.ToString(Registration_amount);
            lbl_Inspection_footer.Text = Convert.ToString(Inspection_amount);
            lbl_amedment_footer.Text = Convert.ToString(Amedment_amount);
            lbl_renewal_footer.Text = Convert.ToString(Renewal_amount);
            lbl_total_footer.Text = Convert.ToString(Total_amount);
        }

    }


    public void BindCompany()
    {
        try
        {

            if (Div_companyWise.Visible == false)
            {
                ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", "0"));
            }


            else
            {
                if (ddlBranchWise.SelectedItem.Text == "--Select Branch--")
                {
                    BranchID = 0;
                    ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", "0"));
                }
                else
                {
                   
                    BranchID = Convert.ToInt32(ddlBranchWise.SelectedValue);

                    DTZone = new DataTable();
                    Myds = new DataSet();
                    objcompany = new clsCompany();

                    Myds = objcompany.Company_ddl_getCompany_Dataset(BranchID);
                    DTZone = Myds.Tables[0];
                    ddlcomapnywise.DataTextField = DTZone.Columns["fGPtbl_Company_Name"].Caption;
                    ddlcomapnywise.DataValueField = DTZone.Columns["fGPtbl_Company_Id"].Caption;
                    ddlcomapnywise.DataSource = DTZone;
                    ddlcomapnywise.DataBind();
                    ddlcomapnywise.Items.Insert(0, new ListItem("--Select Company--", ""));
                    // ddlZonewise.AutoPostBack = false;
                    ddlBranchWise.AutoPostBack = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grd_report_paymentDetails_DataBound(object sender, EventArgs e)
     {
                
        if (grd_report_paymentDetails.Rows.Count > 0)
        {
            try
            {
                Label lblTotalAmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblRegistrationTotal");
                lblTotalAmount.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lblRegiatrationAmmount")).Text)));

                Label lblTotal = (Label)grd_report_paymentDetails.FooterRow.FindControl("lblTotal");
                lblTotal.Text = String.Format("{0:n2}",
                                        grd_report_paymentDetails.Rows.Cast<GridViewRow>()
                                        .Sum(a => Convert.ToDecimal(((Label)a.FindControl("lbl_unit_ammount")).Text)));
                 // Label lbl_Amedment=(Label)grd_report_paymentDetails.FooterRow.FindControl("

                Label labelTotalAmmount = (Label)grd_report_paymentDetails.FooterRow.FindControl("lbltotalfooter");
                labelTotalAmmount.Text = (Convert.ToDecimal(lblTotalAmount.Text) + Convert.ToDecimal(lblTotal.Text)).ToString();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }

    }

    protected void grd_report_onRowCommand(object sender, GridViewCommandEventArgs e)
    {
              
    }

    protected void grd_Report_onRowdataBound(object sender, GridViewRowEventArgs e)
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
                grd_report_paymentDetails.Columns[3].Visible = false;
                grd_report_paymentDetails.Columns[4].Visible = false;
                grd_report_paymentDetails.Columns[5].Visible = true;
                grd_report_paymentDetails.Columns[6].Visible = false;
                grd_report_paymentDetails.Columns[7].Visible = false;
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
            int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
            string CompanyName = lblcompanyName.Text;
            string Page = "Comapnywise";
            string page1 = "SeprateRecord";
            string url = "CompanyWiseSeparateRecord.aspx?cid=" + companyid + "&cname=" + CompanyName + "&Page=" + Page + "&page1="+page1;

            lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";

            Label lbl_registraton = (Label)e.Row.FindControl("lblRegiatrationAmmount");
            Label lbl_inspection = (Label)e.Row.FindControl("lbl_unit_ammount");
            Label lbl_Amedment = (Label)e.Row.FindControl("lbl_Amedment_amount");
            Label lbl_renewal = (Label)e.Row.FindControl("lbl_Renewal_Amount");
            Label lbl_total_amount = (Label)e.Row.FindControl("lbltotalamount");
            if (lbl_registraton.Text == "" || lbl_registraton.Text == null)
            {
                lbl_registraton.Text = "0.0";
            }
            if (lbl_inspection.Text == "" || lbl_inspection.Text == null)
            {
                lbl_inspection.Text = "0.0";
            }
            if (lbl_Amedment.Text == "" || lbl_Amedment.Text == null)
            {
                lbl_Amedment.Text = "0.0";
            }
            if (lbl_renewal.Text == "" || lbl_renewal.Text == null)
            {
                lbl_renewal.Text = "0.0";
            }
            if (lbl_total_amount.Text == "" || lbl_total_amount.Text == null)
            {
                decimal total_amount;
                total_amount = Convert.ToDecimal(lbl_registraton.Text) + Convert.ToDecimal(lbl_inspection.Text) + Convert.ToDecimal(lbl_Amedment.Text) + Convert.ToDecimal(lbl_renewal.Text);
                lbl_total_amount.Text = total_amount.ToString();
            }
            Registration_amount += Convert.ToDecimal(lbl_registraton.Text);
            Inspection_amount += Convert.ToDecimal(lbl_inspection.Text);
            Amedment_amount += Convert.ToDecimal(lbl_Amedment.Text);
            Renewal_amount += Convert.ToDecimal(lbl_renewal.Text);
            Total_amount += Convert.ToDecimal(lbl_total_amount.Text);

        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            GridViewRow gvr = e.Row;
              //switch for first row 
            if (e.Row.RowIndex == 1)
            {
                GridViewRow Gprev = grd_report_paymentDetails.Rows[e.Row.RowIndex - 1];
                if (Gprev.Cells[0].Text.Equals(e.Row.Cells[0].Text))
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[3].Text = "";
                    e.Row.Cells[4].Text = "";
                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";
                    e.Row.Cells[7].Text = "";
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
                        e.Row.Visible = false;
                        gvr.Parent.Controls.RemoveAt(gvr.RowIndex);
                    }

                    intC = intC - 1;
                } while (intC > 0);

            }

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lbl_registration_footer = (Label)e.Row.FindControl("lblRegistrationTotal");
            Label lbl_inspection_footer = (Label)e.Row.FindControl("lblTotal");
            Label lbl_amedment_footer = (Label)e.Row.FindControl("lblTotal_amedment");
            Label lbl_Renewal_footer = (Label)e.Row.FindControl("lblTotal_Renewal");
            Label lbl_total_footer = (Label)e.Row.FindControl("lbltotalfooter");
            lbl_registration_footer.Text = Convert.ToString(Registration_amount.ToString());
            lbl_amedment_footer.Text = Convert.ToString(Amedment_amount.ToString());
            lbl_Renewal_footer.Text = Convert.ToString(Renewal_amount.ToString());
            lbl_total_footer.Text = Convert.ToString( Total_amount.ToString());

        }


    }

    protected void grd_report_payment_Details_Page_Index_changing(object sender, GridViewPageEventArgs e)
    {
        grd_report_paymentDetails.PageIndex = e.NewPageIndex;
        Bind_Payment_Details();
        grd_report_paymentDetails.Visible = true;

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
                    Typeforpayment=4;   
                }
              
           }

            objcompany = new clsCompany();
            DataSet dsgridviewPayment = new DataSet();
            // Ravish
               
                if(Typeforpayment==0 || Typeforpayment==4)
                {
            dsgridviewPayment = objcompany.Company_PaymentDetails_Modified(dateFrom, dateto, BranchID, ZoneID, CompanyId);
                }
                else
                {
                  dsgridviewPayment=  objcompany.Company_PaymentDetails_Modified_Fetch_By_Formtype(dateFrom, dateto, BranchID, ZoneID, CompanyId, Typeforpayment); 
                }
            if (dsgridviewPayment.Tables[0].Rows.Count > 0)
            {
               
                        lbl_total_record.Text = dsgridviewPayment.Tables[0].Rows.Count.ToString();
                        Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
                        grd_report_paymentDetails.DataSource =  dsgridviewPayment.Tables[0];//.Select("FormType=" + Typeforpayment).ToList();
                        grd_report_paymentDetails.DataBind();
                        grd_report_paymentDetails.PageIndex = 0;
                        print_button.Visible = true;
                        tr_print_button.Visible = true;
                        //Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
         }
                   
             
              
           
            else
            {
                Report_Type.Text = "No Record Found!!!";
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
            Ds_Inspection_agency = objcompany.Company_Inspection_Report(dateFrom, dateto, BranchID, ZoneID);
            if (Ds_Inspection_agency.Tables[0].Rows.Count > 0)
            {
                lbl_total_record.Text = Ds_Inspection_agency.Tables[0].Rows.Count.ToString();
                grd_inspectionAgency.DataSource = Ds_Inspection_agency.Tables[0];
                grd_inspectionAgency.DataBind();
                grd_inspectionAgency.PageIndex = 0;
                Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;

            }
            else
            {
                Report_Type.Text = "No Record Found!!!";
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
      
    protected void grd_inspectionagency_onrowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl_address = (Label)e.Row.FindControl("lbl_Address");
            string Address = lbl_address.Text.Replace(",", "<br/>");
            string decodedText = HttpUtility.HtmlDecode(Address);
            e.Row.Cells[6].Text = decodedText;
            e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Width, "250px");

        }

    }

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

            objcompany = new clsCompany();
            DataSet ds_bind_print_report = new DataSet();
           /* if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID, Orderby_User);
            }

            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWise(dateFrom, dateto, BranchID, ZoneID, Orderby_User);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID == 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_DateWiseModified(dateFrom, dateto, Orderby_User);
            }
            else if (dateFrom != "" && dateto != "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId);
            }
            else if (dateFrom == "" && dateto == "" && BranchID != 0 && ZoneID != 0 && CompanyId != 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId);
            }
            else if (dateFrom != "" && dateto != "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID, Orderby_User);
            }

            else if (dateFrom == "" && dateto == "" && ZoneID != 0 && BranchID == 0 && CompanyId == 0)
            {
                ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonewise(dateFrom, dateto, ZoneID, Orderby_User);
            }*/
            ds_bind_print_report = objcompany.Company_PrintAcess_Certificate_companywiseDatezonebranch(dateFrom, dateto, BranchID, ZoneID, CompanyId, Orderby_User);
            if (ds_bind_print_report.Tables[0].Rows.Count > 0)
            {
                lbl_total_record.Text = ds_bind_print_report.Tables[0].Rows.Count.ToString();
                grd_print_certificate.DataSource = ds_bind_print_report.Tables[0];
                grd_print_certificate.DataBind();
                grd_print_certificate.PageIndex = 0;
                Report_Type.Text = ddlSelectCriteria.SelectedItem.Text;
            }
            else
            {

                Report_Type.Text = "N0 Record Found!!!";
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lbl_address = (Label)e.Row.FindControl("lbl_Zone_Branch");  //For Zone Branch
            string Address = lbl_address.Text.Trim().ToString();//Replace(",", "<br/>");
            string decodedText = HttpUtility.HtmlDecode(Address);
            e.Row.Cells[1].Text = decodedText;
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[1].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
            Label lbl_Request = (Label)e.Row.FindControl("lbl_request_date");  //For Zone Branch
            string RequestDate = lbl_Request.Text.Trim().ToString();//Replace(",", "<br/>");
            string decodedText1 = HttpUtility.HtmlDecode(RequestDate);
            e.Row.Cells[2].Text = decodedText1;
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
            e.Row.Cells[2].Style.Add(HtmlTextWriterStyle.WhiteSpace, "nowrap");
            Label lbl_Approved_Date = (Label)e.Row.FindControl("lbl_RequsetDate_userName");  //For Zone Branch
            string ApprovedDate = lbl_Approved_Date.Text.Trim().ToString();//Replace(",", "<br/>");
            string decodedText2 = HttpUtility.HtmlDecode(ApprovedDate);
            e.Row.Cells[4].Text = decodedText2;
            e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.Width, "100px");
            e.Row.Cells[4].Style.Add(HtmlTextWriterStyle.FontSize, "14px");
            Label lblcompanyid = (Label)e.Row.FindControl("lbl_companyId_print");
            Label lblcompanyName = (Label)e.Row.FindControl("lbl_comapny_print_name");
            int companyid = Convert.ToInt32(lblcompanyid.Text.ToString());
            string CompanyName = lblcompanyName.Text;
            string Page = "Comapnywise";
            string page2 = "separateRecord";
             string page1=string.Empty;
             page1 = page2;
            string url = "CompanyDetails.aspx?cid=" + companyid + "&cname=" + CompanyName + "&page1="+page1;

            lblcompanyName.Text = "<a href='" + url + "' >" + lblcompanyName.Text + "</a>";
        }

    }


    // Bind User lIst----
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
                Report_Type.Text = "No Record Found!!!";
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

    // Bind all dropdownlist category sub category and castid data---------------
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


    protected void paymentDetailsZone(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void grd_inspectionAgency_changing(object sender, GridViewPageEventArgs e)
    {
        grd_inspectionAgency.PageIndex = e.NewPageIndex;
        BindInspectionAgency();
        grd_inspectionAgency.Visible = true;
    }
    protected void grd_print_certificate_changing(object sender, GridViewPageEventArgs e)
    {
        grd_print_certificate.PageIndex = e.NewPageIndex;
        BindGrid_Report_print_access();
        grd_print_certificate.Visible = true;
    }
    protected void ddlstatuswise_SelectedIndexChanged(object sender, EventArgs e)
    {
        Report_Page.Visible = false;
        tr_page.Visible = false;
    }
    protected void btn_grid_print_Click(object sender, EventArgs e)
    {
        if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment" || ddlSelectCriteria.SelectedItem.Text == "Renewal")
        {
            DetailsView1.AllowPaging = false;
            DetailsView1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            DetailsView1.RenderControl(hw);
            string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type = 'text/javascript'>");
            sb.Append("window.onload = new function(){");
            sb.Append("var printWin = window.open('', '', 'left=0");
            sb.Append(",top=0,width=1000,height=600,status=0');");
            sb.Append("printWin.document.write(\"");
            sb.Append(gridHTML);
            sb.Append("\");");
            sb.Append("printWin.document.close();");
            sb.Append("printWin.focus();");
            sb.Append("printWin.print();");
            sb.Append("printWin.close();};");
            sb.Append("</script>");
            ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
            DetailsView1.AllowPaging = true;
            DetailsView1.DataBind();
        }
    }
    protected void cmdSearch_Click(object sender, EventArgs e)
    {


        try
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
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
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
          DataTable dttemp = new DataTable();
            dttemp = Temp.Copy();
             if (Temp.Rows.Count > 0)
          {
             DetailsView1.DataSource = Temp;
             DetailsView1.DataBind();
            BindGridView();
            lbl_total_record.Text = DetailsView1.Rows.Count.ToString();
           }
             else
            {
           Temp.Dispose();
            lbl_total_record.Text = "0";
            // Temp.Dispose();
            DetailsView1.DataBind();
           }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("  " + ex.Message.ToString());
        }
    }
    protected void ddl_dropdownlist_custompageing_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddl_dropdownlist_custompageing.SelectedValue != "")
            {
                ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
            }
           
            if (ddlSelectCriteria.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    if (ddl_custom_paging.SelectedItem.Text == "All")
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                       // Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                    }
                    else
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                        //Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;

                    }
                }
              
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
            else if (ddlSelectCriteria.SelectedItem.Text == "User List")
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
            else if (ddlSelectCriteria.SelectedItem.Text == "Print Certificate")
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
                    ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
                    DetailsView1.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());

                    BindGridView();
                }
                else
                {
                    ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
                    DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    BindGridView();
                      
                }
            }
            //ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }


    }
    protected void ddl_custom_paging_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            //if (ddl_custom_paging.SelectedValue != "")
            //{
            //    ddl_dropdownlist_custompageing.SelectedValue = ddl_custom_paging.SelectedValue;
            //}
        // ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
            if (ddl_dropdownlist_custompageing.SelectedItem.Text == "Payment list by(Payment Report)")
            {
                if (ddlZonewise.SelectedItem.Text == "--Select Zone--")
                {
                    if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());
                        //Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;
                    }
                    else
                    {
                        grd_Branch_Wise.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                      //  Bind_Branch_Wise();
                        grd_Branch_Wise.Visible = true;

                    }
                }
              
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
            else if (ddlSelectCriteria.SelectedItem.Text == "Print Certificate")
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
            else if (ddlSelectCriteria.SelectedItem.Text == "Registration Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Amendment Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Renewal Forms Filled" || ddlSelectCriteria.SelectedItem.Text == "Companies List")
            {
                if (ddl_dropdownlist_custompageing.SelectedItem.Text == "All")
                {
                    ddl_dropdownlist_custompageing.SelectedValue = ddl_custom_paging.SelectedValue;
                    DetailsView1.PageSize = Convert.ToInt32(lbl_total_record.Text.ToString());

                    BindGridView();
                }
                else
                {
                    ddl_dropdownlist_custompageing.SelectedValue = ddl_custom_paging.SelectedValue;
                    if (ddl_custom_paging.SelectedValue != "")
                    {
                        if (ddl_custom_paging.SelectedValue != "10")
                        {
                            ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
                            DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                            BindGridView();
                        }
                        else if (ddl_dropdownlist_custompageing.SelectedValue != "10")
                        {
                            ddl_custom_paging.SelectedValue = ddl_dropdownlist_custompageing.SelectedValue;
                            DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                            BindGridView();
                        }

                        // DetailsView1.PageSize = Convert.ToInt32(ddl_custom_paging.SelectedValue);
                    }
                    else
                    {
                        // DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);
                    } 

                  //  DetailsView1.PageSize = Convert.ToInt32(ddl_dropdownlist_custompageing.SelectedValue);

                   
                }
            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }


    }

    public void Bind_City_wise_data(int State_id)
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
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
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

    //-------------------------/* Bind statewise---------------------------------------------------*/
    public void BindStatewise(int BranchID)
    {
        DataSet ds_bind_state = new DataSet();
        try
        {
            objcompany = new clsCompany();

            ds_bind_state = objcompany.Fetch_Company_State_wise_dropdown(BranchID);
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
    protected void ddlBranchWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int BranchId = Convert.ToInt32(ddlBranchWise.SelectedValue);
            BindStatewise(BranchId);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
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

    public void Bindstatuswise()
    {
        try
        {

            ddlstatuswise.Items.Add("--Select--");
            ddlstatuswise.Items.Add("Registered");
            ddlstatuswise.Items.Add("Inprocess");
            ddlstatuswise.Items.Add("Waiting");
            ddlstatuswise.Items.Add("Expired");
            ddlstatuswise.Items.Add("Incomplete");
            ddlstatuswise.Items.Add("Inspection Passed");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
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
    protected void detailsview_sorting(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (e.SortExpression.Trim() == this.SortField)
                this.SortDirection_1 = (this.SortDirection_1 == "D" ? "A" : "D");
            else
                this.SortDirection_1 = "A";

            this.SortField = e.SortExpression;
            SetSortDirection(this.SortDirection_1);
            BindGridView();

            int columnIndex = 0;
            foreach (DataControlFieldHeaderCell headerCell in DetailsView1.HeaderRow.Cells)
            {
                if (headerCell.ContainingField.SortExpression == e.SortExpression)
                {
                    columnIndex = DetailsView1.HeaderRow.Cells.GetCellIndex(headerCell);
                }
            }

            DetailsView1.HeaderRow.Cells[columnIndex].Controls.Add(sortImage);
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }
    }
}
