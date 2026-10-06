using NSICGP_VBClassLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_STATUS_REPORT_AS_ON_DATE : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string FILTERS = "|-1|-1|-1|-1|-1|-1|||";
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    //DataTable dt;
    DataSet MyDs;

    string TotalCount = "";

    int totalplus = 0;
    int totalfreshregistraton = 0;

    int totalDeletedfreshregistraton = 0;


    int totalbacklogregistrion = 0;
    int totalamedment = 0;
    int totalrenewal = 0;
    string oldValue = string.Empty;
    string newValue = string.Empty;
    int BranchId;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            scriptManager.RegisterPostBackControl(this.btn_chek);

            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (ROLEID != 1 && ROLEID != 10)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            }
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
        if (!IsPostBack)
        {

            System.Data.DataTable dt = new DataTable();

            //Category Cast DROPDOWNLIST
            dt = clsDDL.ddlCategoryCast();
            ddl_1e.DataSource = dt;
            ddl_1e.DataTextField = dt.Columns["fGPtbl_CUnit_CC_Name"].ToString();
            ddl_1e.DataValueField = dt.Columns["fGPtbl_CUnit_CC_Id"].ToString();
            ddl_1e.DataBind();
            ddl_1e.Items.Insert(0, new ListItem("-- Select Enterprise Social Category--", ""));

            //Category Area DROPDOWNLIST
            dt.Clear();
            dt = clsDDL.ddlCategoryArea();
            ddl_1d.DataSource = dt;
            ddl_1d.DataTextField = dt.Columns["fGPtbl_CUnit_CA_Name"].ToString();
            ddl_1d.DataValueField = dt.Columns["fGPtbl_CUnit_CA_Id"].ToString();
            ddl_1d.DataBind();
            ddl_1d.Items.Insert(0, new ListItem("--Select Special Category of Enterpreneur--", ""));
            //ddl_1d.SelectedItem.Value = "1";


            //Category Unit Size DROPDOWNLIST
            dt.Clear();
            dt = clsDDL.ddlCategoryUnitSize();
            DdlCategoryUnitSize.DataSource = dt;
            DdlCategoryUnitSize.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
            DdlCategoryUnitSize.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
            DdlCategoryUnitSize.DataBind();
            DdlCategoryUnitSize.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

            dt.Clear();

            dt = clsDDL.ddlState();
            ddlState.DataSource = dt;
            ddlState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
            ddlState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select State--", ""));

            dt.Clear();
            dt.Dispose();
            Ddl_City_Wise.Items.Clear();
            Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));




            dt.Dispose();

            Btn_value.Value = "1";
            // Bind_Gridview();
        }

    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        Btn_value.Value = "1";
        Bind_Gridview();
    }

    public void Bind_Gridview()
    {
        int bsCategoryAreaId = -1;
        int bsCategoryCastId = -1;
        int bsCategoryUnitSizeId = -1;
        int natureOfOperation = -1;
        int status = -1;
        string fromdate = "";
        string todate = "";
        try
        {


            if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            }
            if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            }

            bsCategoryCastId = ddl_1e.SelectedValue != "" ? Convert.ToInt32(ddl_1e.SelectedValue) : -1;

            bsCategoryAreaId = ddl_1d.SelectedValue != "" ? Convert.ToInt32(ddl_1d.SelectedValue) : -1;


            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;
            natureOfOperation = Convert.ToInt32(rdo_manufacturlist.SelectedValue);

            status = Convert.ToInt32(rdo_Status.SelectedValue);
            int headOfficeState = ddlState.SelectedValue != "" ? Convert.ToInt32(ddlState.SelectedValue) : -1;

            int headOfficeCity = Ddl_City_Wise.SelectedValue != "" ? Convert.ToInt32(Ddl_City_Wise.SelectedValue) : -1;

            FILTERS = "|" + bsCategoryAreaId + "|" + bsCategoryCastId + "|" + bsCategoryUnitSizeId + "|" + natureOfOperation +
                "|" + status + "|-1|" + fromdate + "|" + todate + "|" + headOfficeState + "|" + headOfficeCity + "|";



            DataSet ds_fetch = new DataSet();
            if (ROLEID != 8)
            {
                ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin(fromdate, todate, 0,
                    bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    status, headOfficeState, headOfficeCity);
            }

            else
            {
                ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(Convert.ToInt32(BranchId), fromdate, todate, 0);
            }

            if (ds_fetch.Tables[0].Rows.Count > 0)
            {

                grd_report.DataSource = ds_fetch.Tables[0];
                grd_report.DataBind();
                Show_total_backlog_Registraion();


            }
            else
            {
                ds_fetch.Dispose();
                grd_report.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
    protected void grd_report_Databound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string fromdate = ""; string todate = "";
            int CastID = 0;
            int Branch_id_text;
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();

            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();

            }




            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // -------




                HiddenField hdn_branch_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                Label lblZonename = (Label)e.Row.FindControl("lbl_Zonename");

                Label liveCompany = (Label)e.Row.FindControl("lbl_register_Company");

                Label freshDeletedresitrationCompany = (Label)e.Row.FindControl("lbl_Dregister_Company");



                Label Backlog_com = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label lbl_amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label lbl_Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");


                Label lblBranchname = (Label)e.Row.FindControl("lbl_Branch_name");


                int TotalLabel = 0;
                Label freshresitration = (Label)e.Row.FindControl("lbl_register_Company");

                Label freshDeletedresitration = (Label)e.Row.FindControl("lbl_Dregister_Company");



                Label backlogregistration = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label Amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");
                Label Total = (Label)e.Row.FindControl("lbl_total");

                Label lblzonename_id = (Label)e.Row.FindControl("lbl_zonename_id");
                HiddenField lblBranchname_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                Label zonename = (Label)e.Row.FindControl("lbl_Zonename");






                if (lblBranchname.Text == "")
                {
                    freshresitration.Visible = false;
                    freshDeletedresitration.Visible = false;
                    backlogregistration.Visible = false;
                    Amedment.Visible = false;
                    Renewal.Visible = false;
                    Total.Visible = false;
                }

                TotalLabel = Convert.ToInt32(Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text) + Convert.ToInt32(freshDeletedresitration.Text == "" ? "0" : freshDeletedresitration.Text) + Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text) + Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text) + Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text));
                Total.Text = Convert.ToString(TotalLabel);
                totalplus += Convert.ToInt32(Total.Text == "" ? "0" : Total.Text);
                totalfreshregistraton += Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text);


                totalDeletedfreshregistraton += Convert.ToInt32(freshDeletedresitration.Text == "" ? "0" : freshDeletedresitration.Text);


                totalbacklogregistrion += Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text);
                totalamedment += Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text);
                totalrenewal += Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text);
                int totalbaklogregistration = 0;
                totalbaklogregistration = Convert.ToInt32(liveCompany.Text == "0" ? 0 : Convert.ToInt32(liveCompany.Text)) + Convert.ToInt32(freshDeletedresitrationCompany.Text == "0" ? 0 : Convert.ToInt32(freshDeletedresitrationCompany.Text)) + Convert.ToInt32(Backlog_com.Text == "0" ? 0 : Convert.ToInt32(Backlog_com.Text));




                hdn_total_fresh_backlog.Value = Convert.ToString(totalbaklogregistration);
                //RA

                bool isZoneTotal = false;
                if (zonename.Text == "<b>Total</b>"
                        && lblzonename_id.Text == "0" && lblBranchname_id.Value == "0")
                {
                    isZoneTotal = true;
                    Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");

                    string url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";
                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4


                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 111 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    freshDeletedresitrationCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + freshDeletedresitrationCompany.Text + "</a>";




                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";


                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";

                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_Renewal.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_Renewal.Text + "</a>";


                }
                if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                {
                    string url = "TraceSubPageDataStatusReport_status_datewise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";


                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 111 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    freshDeletedresitrationCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + freshDeletedresitrationCompany.Text + "</a>";



                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4
                    Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";


                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";

                    url = "TraceSubPageDataStatusReport_status_datewise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_Renewal.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_Renewal.Text + "</a>";


                }




                if (hdn_branch_id.Value != "0")
                {
                    Branch_id_text = Convert.ToInt32(hdn_branch_id.Value);
                    if (liveCompany.Text != "0" && liveCompany.Text != "")
                    {
                        string url;



                        if (Btn_value.Value == "2")
                        {
                            url = "TraceSubPageDataStatusReport.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "TraceSubPageDataStatusReport_status_datewise.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }

                        liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";
                    }

                    if (freshDeletedresitrationCompany.Text != "0" && freshDeletedresitrationCompany.Text != "")
                    {
                        string url;



                        if (Btn_value.Value == "2")
                        {
                            url = "TraceSubPageDataStatusReport.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 111 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "TraceSubPageDataStatusReport_status_datewise.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 111 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }

                        freshDeletedresitrationCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + freshDeletedresitrationCompany.Text + "</a>";
                    }



                    if (Backlog_com.Text != "0" && Backlog_com.Text != "")
                    {
                        string url;
                        if (Btn_value.Value == "2")
                        {
                            url = "TraceSubPageDataStatusReport.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "TraceSubPageDataStatusReport_status_datewise.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4
                        }
                        Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";
                    }

                    if (lbl_amedment.Text != "0" && lbl_amedment.Text != "")
                    {
                        string url;
                        if (Btn_value.Value == "2")
                        {
                            url = "TraceSubPageDataStatusReport.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "TraceSubPageDataStatusReport_status_datewise.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";
                    }

                    if (lbl_Renewal.Text != "0" && lbl_Renewal.Text != "")
                    {
                        string url;
                        if (Btn_value.Value == "2")
                        {
                            url = "TraceSubPageDataStatusReport.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "TraceSubPageDataStatusReport_status_datewise.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        lbl_Renewal.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_Renewal.Text + "</a>";
                    }
                }


            }
            /*
              if (e.Row.RowType == DataControlRowType.Footer)
              {
                  Label lbl_Footer_total = (Label)e.Row.FindControl("lbl_total_footer");
                  lbl_Footer_total.Text = Convert.ToString(totalplus);
                  Label lbl_footer_fresh = (Label)e.Row.FindControl("lbl_fresh_register_company");
                  lbl_footer_fresh.Text = Convert.ToString(totalfreshregistraton);
                  Label lbl_backlog_registration = (Label)e.Row.FindControl("lbl_total_back_lock_company");
                  lbl_backlog_registration.Text = Convert.ToString(totalbacklogregistrion);
                  Label lbl_amdement_registration = (Label)e.Row.FindControl("lbl_total_amedment");
                  lbl_amdement_registration.Text = Convert.ToString(totalamedment);
                  Label lbl_renewal_registration = (Label)e.Row.FindControl("lbl_total_renewal");
                  lbl_renewal_registration.Text = Convert.ToString(totalrenewal);
              }*/
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }

    public DataSet Fetch_zone_branch_wise_Datewise_report_datewise_admin(string Formadte, string todate, int CastID,
         int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
          int enterprisedefnition, int fGPtbl_Company__StatusId, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId
        )
    {

        SqlDataAdapter MyAdp;
        MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_Status_Datewise_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;


            //March 30 RA
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
            MyAdp.SelectCommand.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Status", SqlDbType.Int).Value = fGPtbl_Company__StatusId;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;

            //

            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.Fill(MyDs);

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
        return MyDs;
    }

    public DataSet Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(int Branchid, string Formadte, string todate, int CastID)
    {
        SqlDataAdapter MyAdp;
        MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_Status_Datewise_branchwise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Branchid != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = Branchid;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }
    protected void btn_Serach_fourApril_Click(object sender, EventArgs e)
    {
        Btn_value.Value = "2";
        string fromdate_12 = "";
        string todate_12 = "";
        int CastID = 0;
        DataSet DS_fetch = new DataSet();
        try
        {
            if (ROLEID != 8)
            {
                DS_fetch = Fetch_zone_branch_wise_Datewise_report_14_15(fromdate_12, todate_12, CastID);
            }
            else
            {
                DS_fetch = Fetch_zone_branch_wise_Datewise_report_BranchWise_fincial_14_15(BranchId, fromdate_12, todate_12, CastID);
            }
            if (DS_fetch.Tables[0].Rows.Count > 0)
            {
                grd_report.DataSource = DS_fetch.Tables[0];
                grd_report.DataBind();
                Show_total_backlog_Registraion();
            }
            else
            {
                DS_fetch.Dispose();
                grd_report.DataBind();
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }

    }

    public DataSet Fetch_zone_branch_wise_Datewise_report_14_15(string Formadte, string todate, int CastID)
    {
        DataSet MyDs_12 = new DataSet();
        SqlDataAdapter MyAdp_12 = new SqlDataAdapter();
        try
        {
            MyDs_12 = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp_12 = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_fincial201415", DBManager.ConnectToDB());
            MyAdp_12.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp_12.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp_12.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp_12.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp_12.SelectCommand.CommandTimeout = 240;
            MyAdp_12.Fill(MyDs_12);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp_12.Dispose();
        }
        return MyDs_12;
    }

    public DataSet Fetch_zone_branch_wise_Datewise_report_BranchWise_fincial_14_15(int BranchID, string Formadte, string todate, int CastID)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_BranchWise_finacial_year1415", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }

    public void Show_total_backlog_Registraion()
    {
        try
        {
            string fromdate_show = ""; string todate_show = "";
            if (txt_fromdate.Text != "")
            {
                fromdate_show = txt_fromdate.Text.ToString();

            }
            if (txt_todate.Text != "")
            {
                todate_show = txt_todate.Text.ToString();

            }

            string Branch_id_text = "0";


            if (hdn_total_fresh_backlog.Value != "")
            {
                if (ROLEID == 8)
                {
                    Branch_id_text = Convert.ToString(BranchId);
                }
                else
                {
                    Branch_id_text = "0";
                }
                lbl_total_fresh_backlog.Text = hdn_total_fresh_backlog.Value.ToString();
                string url = "FreshCompanyDatastatusReport_DataStatus.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate_show + "&ToDate=" + todate_show + "&CateID=" + Convert.ToInt32("0") + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value;
                lbl_total_fresh_backlog.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_total_fresh_backlog.Text + "</a>";


            }
        }
        catch (Exception ex)
        {
        }
    }


    protected void grd_report_RowCreated(object sender, GridViewRowEventArgs e)
    {


    }
    protected void btn_chek_Click(object sender, EventArgs e)
    {
        int bsCategoryAreaId = -1;
        int bsCategoryCastId = -1;
        int bsCategoryUnitSizeId = -1;
        int natureOfOperation = -1;
        int status = -1;
        string fromdate = "";
        string todate = "";
        try
        {


            if (txt_fromdate.Text.ToString() != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");// txt_fromdate.Text.ToString();
            }
            if (txt_todate.Text.ToString() != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            }

            bsCategoryCastId = ddl_1e.SelectedValue != "" ? Convert.ToInt32(ddl_1e.SelectedValue) : -1;

            bsCategoryAreaId = ddl_1d.SelectedValue != "" ? Convert.ToInt32(ddl_1d.SelectedValue) : -1;


            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;
            natureOfOperation = Convert.ToInt32(rdo_manufacturlist.SelectedValue);

            status = Convert.ToInt32(rdo_Status.SelectedValue);

            int headOfficeState = ddlState.SelectedValue != "" ? Convert.ToInt32(ddlState.SelectedValue) : -1;

            int headOfficeCity = Ddl_City_Wise.SelectedValue != "" ? Convert.ToInt32(Ddl_City_Wise.SelectedValue) : -1;


            FILTERS = "|" + bsCategoryAreaId + "|" + bsCategoryCastId + "|" + bsCategoryUnitSizeId + "|" + natureOfOperation +
                "|" + status + "|-1|" + fromdate + "|" + todate + "|" + headOfficeState + "|" + headOfficeCity + "|";



            DataSet ds_fetch = new DataSet();
            if (ROLEID != 8)
            {
                ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin(fromdate, todate, 0,
                    bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    status, headOfficeState, headOfficeCity);
            }

            else
            {
                ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(Convert.ToInt32(BranchId), fromdate, todate,
                    0);
            }

            if (ds_fetch.Tables[0].Rows.Count > 0)
            {
                ds_fetch.Tables[0].Columns.Remove("tblid");
                ds_fetch.Tables[0].Columns.Remove("ZoneID");
                ds_fetch.Tables[0].Columns.Remove("BranchID");
                ds_fetch.Tables[0].AcceptChanges();
                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Status Report" + fromdate + " " + todate + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(ds_fetch, "StatusReport.xls", fromdatetodate);

            }
            else
            {

            }
        }
        catch (Exception rr) { }
    }

    public static void ExportDataSetToExcel(DataSet dsInput, string ExcelFileName, string text)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object 
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel 
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelFileName + "\"");
            response.Cache.SetCacheability(HttpCacheability.NoCache);



            GridView dg1 = new GridView();
            dg1.DataSource = dsInput.Tables[0];
            dg1.DataBind();


            // create a string writer 
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid

                    htw.Write("<table><tr><td colspan='9' align='center'>" + text + "</td></tr></table>");
                    htw.Write("<table><tr><td colspan='9'></td></tr></table>");
                    dg1.RenderControl(htw);
                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
                    response.End();
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Response.Write("sssdsdsdsdsds");
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds_city = new DataSet();
        clsCompany objcompany;
        try
        {
            objcompany = new clsCompany();
            if (ddlState.SelectedValue != "")
            {
                int StateID = Convert.ToInt32(ddlState.SelectedValue);
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
        //FunFillGridView(ddlStateId);
    }

}
