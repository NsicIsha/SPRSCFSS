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
using System.IO;
using System.Text;
using System.Diagnostics;
using NSICGP_VBClassLibrary;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections.Generic;
public partial class appAdministrator_ZonewiseliveCompany_New :  System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    string TotalCount = "";

    int totalplus = 0;
    int totalfreshregistraton = 0;

    int totalProvfreshregistraton = 0;

    int totalbacklogregistrion = 0;
    int totalamedment = 0;
    int totalrenewal = 0;
    string oldValue = string.Empty;
    string newValue = string.Empty;
    int BranchId, DepartmentId;
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            USERID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            ROLEID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            USERID = 0;
            ROLEID = 0;
            DepartmentId = 0;
            USERFULLNAME = "";
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        try
        {
            FunRequest();
            //USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

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
            Btn_value.Value = "1";
            BindCategoryCast();
            BindGridview();
            Show_total_backlog_Registraion();
        }

    }


    public DataSet Fetch_zone_branch_wise_Datewise_report(string Formadte, string todate, int CastID)
    {
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
                                 // MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("GPsp_Fetch_Datewise_DataStatusLiveReport_With_RDate", DBManager.ConnectToDB());

            //  MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified", DBManager.ConnectToDB());

            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
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

        Btn_value.Value = "1";
        BindGridview();
        Show_total_backlog_Registraion();
    }

    public void BindGridview()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            string fromdate = ""; string todate = "";
            int CastID = 0;
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();

            }
            else
            {
                fromdate = "04/01/2022".ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();

            }
            else
            {
                todate = "03/31/2023".ToString();
            }
            if (ddl_category_cast.SelectedValue != "0" && ddl_category_cast.SelectedValue != "")
            {
                CastID = Convert.ToInt32(ddl_category_cast.SelectedValue);
            }
            else
            {
                CastID = 0;
            }

            //ds = objclsCompany.Fetch_zone_wise_excel_modified(fromdate, todate, ROLEID);

            if (ROLEID != 8)
            {


                ds = Fetch_zone_branch_wise_Datewise_report(fromdate, todate, CastID);// objclsCompany.Fetch_zone_branch_wise_Datewise_report(fromdate, todate, CastID);
            }
            else
            {
                ds = Fetch_zone_branch_wise_Datewise_report_BranchWise(BranchId, fromdate, todate, CastID);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_report.DataSource = ds.Tables[0];
                grd_report.DataBind();
                /*
                  lbl_labelcount.Text = TotalCount.ToString();
                lbl_total_new_company.Text = TotalCount.ToString();
                 */
                // GenerateUniqueData();
            }
            else
            {
                ds.Dispose();
                grd_report.Dispose();
                grd_report.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
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
            else
            {
                fromdate = "04/01/2022".ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();
            }
            else
            {
                todate = "03/31/2023".ToString();
            }
            if (ddl_category_cast.SelectedValue != "0")
            {
                CastID = Convert.ToInt32(ddl_category_cast.SelectedValue);
            }
            else
            {
                CastID = 0;
            }



            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // -------
                HiddenField hdn_branch_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                Label lblZonename = (Label)e.Row.FindControl("lbl_Zonename");

                Label liveCompany = (Label)e.Row.FindControl("lbl_register_Company");

                Label ProvliveCompany = (Label)e.Row.FindControl("lbl_register_CompanyProv");


                Label Backlog_com = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label lbl_amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label lbl_Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");



                //newValue = lblZonename.Text;
                //if (newValue != string.Empty)
                //{
                //    if (oldValue == newValue)
                //    {
                //        lblZonename.Text = "";
                //    }
                //    else
                //    {
                //        lblZonename.Text = newValue;
                //    }
                //}
                //oldValue = newValue;

                Label lblBranchname = (Label)e.Row.FindControl("lbl_Branch_name");


                int TotalLabel = 0;
                Label freshresitration = (Label)e.Row.FindControl("lbl_register_Company");

                Label Provfreshresitration = (Label)e.Row.FindControl("lbl_register_CompanyProv");


                Label backlogregistration = (Label)e.Row.FindControl("lbl_Back_lock_company");
                Label Amedment = (Label)e.Row.FindControl("lbl_amed_Company");
                Label Renewal = (Label)e.Row.FindControl("lbl_Renewal_Company");
                Label Total = (Label)e.Row.FindControl("lbl_total");

                if (lblBranchname.Text == "")
                {
                    freshresitration.Visible = false;
                    // Provfreshresitration.Visible = false;
                    backlogregistration.Visible = false;
                    Amedment.Visible = false;
                    Renewal.Visible = false;
                    Total.Visible = false;
                }

                //TotalLabel = Convert.ToInt32(Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text) + Convert.ToInt32(Provfreshresitration.Text == "" ? "0" : Provfreshresitration.Text) + Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text) + Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text) + Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text));

                TotalLabel = Convert.ToInt32(Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text) + Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text) + Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text) + Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text));
                Total.Text = Convert.ToString(TotalLabel);
                totalplus += Convert.ToInt32(Total.Text == "" ? "0" : Total.Text);
                totalfreshregistraton += Convert.ToInt32(freshresitration.Text == "" ? "0" : freshresitration.Text);

                // totalProvfreshregistraton += Convert.ToInt32(Provfreshresitration.Text == "" ? "0" : Provfreshresitration.Text);

                totalbacklogregistrion += Convert.ToInt32(backlogregistration.Text == "" ? "0" : backlogregistration.Text);
                totalamedment += Convert.ToInt32(Amedment.Text == "" ? "0" : Amedment.Text);
                totalrenewal += Convert.ToInt32(Renewal.Text == "" ? "0" : Renewal.Text);



                int totalbaklogregistration = 0;

                int totalRenewalregistration = 0;


                totalbaklogregistration = Convert.ToInt32(liveCompany.Text == "0" ? 0 : Convert.ToInt32(liveCompany.Text)) + Convert.ToInt32(Backlog_com.Text == "0" ? 0 : Convert.ToInt32(Backlog_com.Text));
                hdn_total_fresh_backlog.Value = Convert.ToString(totalbaklogregistration);
                totalRenewalregistration = Convert.ToInt32(lbl_Renewal.Text == "0" ? 0 : Convert.ToInt32(lbl_Renewal.Text));

                hdn_total_Renewal_backlog.Value = Convert.ToString(totalRenewalregistration);


                if (hdn_branch_id.Value != "0")
                {
                    Branch_id_text = Convert.ToInt32(hdn_branch_id.Value);
                    if (liveCompany.Text != "0" && liveCompany.Text != "")
                    {
                        string url = "TraceSubPageDataStatusReportNew.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value;
                        liveCompany.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + liveCompany.Text + "</a>";
                    }

                    if (Backlog_com.Text != "0" && Backlog_com.Text != "")
                    {
                        string url = "TraceSubPageDataStatusReportNew.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 14 + "&Btn_value=" + Btn_value.Value;  // Registerd and statusId 4
                        Backlog_com.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + Backlog_com.Text + "</a>";
                    }

                    if (lbl_amedment.Text != "0" && lbl_amedment.Text != "")
                    {
                        string url = "TraceSubPageDataStatusReportNew.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 3 + "&Btn_value=" + Btn_value.Value;
                        lbl_amedment.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_amedment.Text + "</a>";
                    }

                    if (lbl_Renewal.Text != "0" && lbl_Renewal.Text != "")
                    {
                        string url = "TraceSubPageDataStatusReportNew.aspx?id=" + Convert.ToInt32(Branch_id_text) + "&FromDate=" + fromdate + "&ToDate=" + todate + "&CateID=" + CastID + "&FormType=" + 2 + "&Btn_value=" + Btn_value.Value;
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



    public void BindCategoryCast()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet dsCast = new DataSet();
            dsCast = objclsCompany.Fetch_Castwise_reportCastId();
            if (dsCast.Tables[0].Rows.Count > 0)
            {
                ddl_category_cast.Items.Clear();
                ddl_category_cast.DataSource = dsCast.Tables[0];
                ddl_category_cast.DataTextField = "CastName";
                ddl_category_cast.DataValueField = "CastID";
                ddl_category_cast.DataBind();
                ddl_category_cast.Items.Insert(0, new ListItem("Select", "0"));
                ddl_category_cast.SelectedIndex = 0;

            }
            else
            {
                dsCast.Dispose();
                ddl_category_cast.Items.Insert(0, new ListItem("Select", "0"));
                ddl_category_cast.SelectedIndex = 0;
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
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

            if (hdn_total_fresh_backlog.Value != "")
            {
                lbl_total_fresh_backlog.Text = hdn_total_fresh_backlog.Value.ToString();
                string url = "FreshCompanyDatastatusReport.aspx?id=" + Convert.ToInt32("0") + "&FromDate=" + fromdate_show + "&ToDate=" + todate_show + "&CateID=" + Convert.ToInt32("0") + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value;
                lbl_total_fresh_backlog.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_total_fresh_backlog.Text + "</a>";


            }

            if (hdn_total_Renewal_backlog.Value != "")
            {
                lbl_total_Renewal_backlog.Text = hdn_total_Renewal_backlog.Value.ToString();
                string url = "TraceSubPageDataStatusReportNew.aspx?id=" + Convert.ToInt32("0") + "&FromDate=" + fromdate_show + "&ToDate=" + todate_show + "&CateID=" + Convert.ToInt32("0") + "&FormType=" + 1 + "&Btn_value=" + Btn_value.Value;
                lbl_total_Renewal_backlog.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_total_Renewal_backlog.Text + "</a>";


            }
        }
        catch (Exception ex)
        {
        }
    }


    public DataSet Fetch_zone_branch_wise_Datewise_report_BranchWise(int BranchID, string Formadte, string todate, int CastID)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
                                 ////            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_BranchWise", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("GPsp_Fetch_Datewise_DataStatusLiveReport_With_RDate_BranchWise", DBManager.ConnectToDB());

            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
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
    protected void grd_report_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}