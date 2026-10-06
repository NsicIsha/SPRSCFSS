using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_RevenueReport : System.Web.UI.Page
{

    int USERID, ROLEID;
    string USERFULLNAME;
    string FILTERS = "|-1|||";
    string SearchText = string.Empty;
    int totalplus = 0;
    int totalfreshregistraton = 0;
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


            dt = clsDDL.ddlCategoryUnitSize();
            DdlCategoryUnitSize.DataSource = dt;
            DdlCategoryUnitSize.DataTextField = dt.Columns["fGPtbl_CUnit_CUS_Name"].ToString();
            DdlCategoryUnitSize.DataValueField = dt.Columns["fGPtbl_CUnit_CUS_Id"].ToString();
            DdlCategoryUnitSize.DataBind();
            DdlCategoryUnitSize.Items.Insert(0, new ListItem("--Select Category Unit Size--", ""));

            dt.Clear();
            dt.Dispose();

            Btn_value.Value = "1";
          //  Bind_Gridview();
        }
    }

    public void Bind_Gridview()
    {

        int bsCategoryUnitSizeId = -1;
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


            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;

            FILTERS = "|" + bsCategoryUnitSizeId + "|" + fromdate + "|" + todate + "|";



            DataSet ds_fetch = new DataSet();
            if (ROLEID != 8)
            {
                ds_fetch = Fetch_ReveneueReport_admin(fromdate, todate, 0,
                     bsCategoryUnitSizeId);
            }

            else
            {
                //ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(Convert.ToInt32(BranchId), fromdate, todate, 0);
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

    public DataSet Fetch_ReveneueReport_admin(string Formadte, string todate, int CastID,
          int fGPtbl_CUnit_CategoryUnitSizeId

        )
    {

        SqlDataAdapter MyAdp;
        DataSet MyDs = new DataSet();
        MyAdp = new SqlDataAdapter();
        try
        {
            //GPsp_fetch_ReveneueReport_RA
            MyAdp = new SqlDataAdapter("GPsp_fetch_ReveneueReport_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
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
    protected void btn_search_Click(object sender, EventArgs e)
    {

        int bsCategoryUnitSizeId = -1;
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



            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;




            FILTERS = "|" + bsCategoryUnitSizeId + "|" + fromdate + "|" + todate + "|";



            DataSet ds_fetch = new DataSet();
            if (ROLEID != 8)
            {
                ds_fetch = Fetch_ReveneueReport_admin(fromdate, todate, 0, bsCategoryUnitSizeId);
            }

            else
            {
                // ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(Convert.ToInt32(BranchId), fromdate, todate, 0);
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

    protected void btn_chek_Click(object sender, EventArgs e)
    {
        int bsCategoryUnitSizeId = -1;
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



            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;




            FILTERS = "|" + bsCategoryUnitSizeId + "|" + fromdate + "|" + todate + "|";



            DataSet ds_fetch = new DataSet();
            if (ROLEID != 8)
            {
                ds_fetch = Fetch_ReveneueReport_admin(fromdate, todate, 0, bsCategoryUnitSizeId);
            }

            else
            {
                // ds_fetch = Fetch_zone_branch_wise_Datewise_report_datewise_admin_branch_wise(Convert.ToInt32(BranchId), fromdate, todate, 0);
            }

            if (ds_fetch.Tables[0].Rows.Count > 0)
            {
                ds_fetch.Tables[0].Columns.Remove("tblid");
                ds_fetch.Tables[0].Columns.Remove("ZoneID");
                ds_fetch.Tables[0].Columns.Remove("BranchID");
                ds_fetch.Tables[0].Columns.Remove("Companybacklog");
                ds_fetch.Tables[0].AcceptChanges();
                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Revenue Report " + fromdate + " " + todate + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(ds_fetch, "RevenueReport.xls", fromdatetodate);

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

                HiddenField hdn_branch_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                Label lblZonename = (Label)e.Row.FindControl("lbl_Zonename");

                Label liveCompany = (Label)e.Row.FindControl("lbl_register_Company");
                //Label Backlog_com = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label lbl_amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label lbl_Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");


                Label lblBranchname = (Label)e.Row.FindControl("lbl_Branch_name");


                int TotalLabel = 0;
                Label freshresitration = (Label)e.Row.FindControl("lbl_register_Company");
                //Label backlogregistration = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label Amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");
                Label Total = (Label)e.Row.FindControl("lbl_total");

                Label lblzonename_id = (Label)e.Row.FindControl("lbl_zonename_id");
                HiddenField lblBranchname_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                Label zonename = (Label)e.Row.FindControl("lbl_Zonename");

                //RA


                if (lblBranchname.Text == "")
                {
                    freshresitration.Visible = false;
                    //backlogregistration.Visible = false;
                    Amedment.Visible = false;
                    Renewal.Visible = false;
                    Total.Visible = false;
                }

                TotalLabel = Convert.ToInt32(Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text) + Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text) + Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text));
                Total.Text = Convert.ToString(TotalLabel);
                totalplus += Convert.ToInt32(Total.Text == "" ? "0" : Total.Text);
                totalfreshregistraton += Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text);
                // totalbacklogregistrion += Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text);
                totalamedment += Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text);
                totalrenewal += Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text);
                int totalbaklogregistration = 0;
                totalbaklogregistration = Convert.ToInt32(liveCompany.Text == "" ? 0 : Convert.ToInt32(liveCompany.Text));
                hdn_total_fresh_backlog.Value = Convert.ToString(totalbaklogregistration);
                ////
                bool isZoneTotal = false;
                if (zonename.Text == "<b>Total</b>"
                        && lblzonename_id.Text == "0" && lblBranchname_id.Value == "0")
                {
                    isZoneTotal = true;
                    Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");

                    string url = "RevenueReportCompanyList.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";


                    url = "RevenueReportCompanyList.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4

                    // Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";


                    url = "RevenueReportCompanyList.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";

                    url = "RevenueReportCompanyList.aspx?FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_Renewal.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_Renewal.Text + "</a>";


                }

                ////
                if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                {
                    string url = "RevenueReportCompanyList.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";


                    //url = "RevenueReportCompanyList.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4

                    //Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";


                    url = "RevenueReportCompanyList.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

                    lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";

                    url = "RevenueReportCompanyList.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;

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
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }

                        liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";
                    }

                    //if (Backlog_com.Text != "0" && Backlog_com.Text != "")
                    //{
                    //    string url;
                    //    if (Btn_value.Value == "2")
                    //    {
                    //        url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                    //    }
                    //    else
                    //    {
                    //        url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;  // Registerd and statusId 4
                    //    }
                    //    Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";
                    //}

                    if (lbl_amedment.Text != "0" && lbl_amedment.Text != "")
                    {
                        string url;
                        if (Btn_value.Value == "2")
                        {
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";
                    }

                    if (lbl_Renewal.Text != "0" && lbl_Renewal.Text != "")
                    {
                        string url;
                        if (Btn_value.Value == "2")
                        {
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        else
                        {
                            url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value + "&FILTER=" + FILTERS; ;
                        }
                        lbl_Renewal.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_Renewal.Text + "</a>";
                    }
                }


            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

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
                string url = "RevenueReportCompanyList.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate_show + "&ToDate=" + todate_show + "&CateID=" + Convert.ToInt32("0") + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value;
                lbl_total_fresh_backlog.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_total_fresh_backlog.Text + "</a>";


            }
        }
        catch (Exception ex)
        {
        }
    }
}