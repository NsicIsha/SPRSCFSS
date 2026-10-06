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

public partial class appAdministrator_ZoneBranchWiseDetailReport : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    string FILTERS = "|-1|-1|-1|-1|-1|-1|||";
    clsCompany objclsCompany;
    DataTable dt;

    int TotalApplied = 0;
    int totalRegisterd = 0;
    int totlincomplete = 0;
    int totalwaiting = 0;
    int totalinprocess = 0;
    int totalinspectionpassed = 0;
    int totalunderprocess = 0;
    int applied = 0;
    string TotalCount = "";
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
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
       
        scriptManager.RegisterPostBackControl(this.btn_chek);
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
            FunFillDDL();
            if (ROLEID == 8)
            {
                btn_chek.Visible = false;
                lbl_labelcount.Visible = false;
                td_total_company.Visible = false;
                lbl_total_new_company.Visible = false;
                span_total_com.Visible = false;
                BindGridview();
            }
            else
            {
                btn_chek.Visible = true;
                lbl_labelcount.Visible = true;
                lbl_total_new_company.Visible = true;
                countcompany();

                BindGridview();
            }

        }
    }

    private void FunFillDDL()
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
        dt.Dispose();
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


    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindGridview();
    }

    public void BindGridview()
    {
        int bsCategoryAreaId = -1;
        int bsCategoryCastId = -1;
        int bsCategoryUnitSizeId = -1;
        int natureOfOperation = -1;
        int headOfficeState = -1;
        int headOfficeCity = -1;
        string fromdate = ""; string todate = "";
        if (txt_fromdate.Text != "" && txt_fromdate.Text != "__/__/____")
        {
            fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");
            lbl_fromcdate.Text = "From Date" + fromdate.ToString();
        }
        if (txt_todate.Text != "" && txt_todate.Text != "__/__/____")
        {
            todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
            lbl_tcdate.Text = "To date" + todate.ToString();
        }
        bsCategoryCastId = ddl_1e.SelectedValue != "" ? Convert.ToInt32(ddl_1e.SelectedValue) : -1;

        bsCategoryAreaId = ddl_1d.SelectedValue != "" ? Convert.ToInt32(ddl_1d.SelectedValue) : -1;


        bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;
        natureOfOperation =  Convert.ToInt32(rdo_manufacturlist.SelectedValue);

        headOfficeState = ddlState.SelectedValue != "" ? Convert.ToInt32(ddlState.SelectedValue) : -1;

        headOfficeCity = Ddl_City_Wise.SelectedValue != "" ? Convert.ToInt32(Ddl_City_Wise.SelectedValue) : -1;

        FILTERS = "|" + bsCategoryAreaId + "|" + bsCategoryCastId + "|" + bsCategoryUnitSizeId + "|" + natureOfOperation +
            "|" + headOfficeState + "|" + headOfficeCity + "|"+ fromdate + "|" + todate +"|";
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
           
            
            //ds = objclsCompany.Fetch_zone_wise_excel_modified(fromdate, todate, ROLEID);

            if (ROLEID == 8)
            {
                ds = Fetch_zone_branch_wise_excel_report_branchwise(fromdate, todate, ROLEID, 2, BranchId,
                    bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    headOfficeState, headOfficeCity
                    );
            }
            else
            {
                ds = objclsCompany.Fetch_zone_branch_wise_excel_report(fromdate, todate, ROLEID, 2,
                     bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    headOfficeState, headOfficeCity

                    );
            }
            if (ds.Tables[0].Rows.Count > 0)
            {               

                grd_excel_type.DataSource = ds.Tables[0];
                grd_excel_type.DataBind();
                /*
                  lbl_labelcount.Text = TotalCount.ToString();
                lbl_total_new_company.Text = TotalCount.ToString();
                 */
                // GenerateUniqueData();
            }
            else
            {
                ds.Dispose();
                grd_excel_type.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }
    protected void grd_exceltype_rowdatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_underprocess = (Label)e.Row.FindControl("lbl_underproces");
                Label lbl_branch_id = (Label)e.Row.FindControl("lbl_branch_id");
                Label lbl_registerd_unit = (Label)e.Row.FindControl("lbl_registerd");
                Label lbl_expired_unit = (Label)e.Row.FindControl("lbl_expired");
                Label lblregistrationplusexpired = (Label)e.Row.FindControl("lbl_reg_expired");
                Label lblzonename_id = (Label)e.Row.FindControl("lbl_zonename_id");
                
                if (lbl_registerd_unit.Text != "" && lbl_expired_unit.Text != "")
                {
                    //lblregistrationplusexpired.Text = Convert.ToString(Convert.ToInt32(HttpUtility.HtmlDecode(lbl_registerd_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))) + Convert.ToInt32(HttpUtility.HtmlDecode(lbl_expired_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))));

                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string totalUnit_url = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "1,4&FILTER=" + FILTERS;
                        string value = Convert.ToString(Convert.ToInt32(HttpUtility.HtmlDecode(lbl_registerd_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))) + Convert.ToInt32(HttpUtility.HtmlDecode(lbl_expired_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))));
                        lblregistrationplusexpired.Text = "<a style='color:red' href='" + totalUnit_url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + value + "</a>";

                    }

                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string totalUnit_url = "traceliveexpiredunitcompanywise.aspx?Id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "1,4&FILTER=" + FILTERS;
                            string value = Convert.ToString(Convert.ToInt32(HttpUtility.HtmlDecode(lbl_registerd_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))) + Convert.ToInt32(HttpUtility.HtmlDecode(lbl_expired_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))));
                            lblregistrationplusexpired.Text = "<a style='color:red' href='" + totalUnit_url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + value + "</a>";
                        }
                    }

                     
                    
                    
                    string url = "Companystatustrack_By_User.aspx?id=" + (lbl_branch_id.Text.ToString());
                    lbl_underprocess.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_underprocess.Text + "</a>";
                }

                Label lbl_applied = (Label)e.Row.FindControl("lbl_applied");
                Label lbl_incomplete = (Label)e.Row.FindControl("lbl_Incomplete");
                Label lbl_waiting = (Label)e.Row.FindControl("lbl_waiting");
                Label lbl_inspection_passed = (Label)e.Row.FindControl("lbl_inspection_passed");
                Label lbl_inprocess = (Label)e.Row.FindControl("lbl_inprocess");

                

                if (lbl_applied.Text != "" && lbl_applied.Text != "0")
                {

                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "1,2,3,7&FILTER=" + FILTERS;
                        lbl_applied.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_applied.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string url_applied = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "1,2,3,7&FILTER=" + FILTERS;
                            lbl_applied.Text = "<a href='" + url_applied + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_applied.Text + "</a>";
                        }
                    }
                    }
                else
                {
                    lbl_applied.Text = "0";
                }


                if (lbl_incomplete.Text != "" && lbl_incomplete.Text != "0")
                {
                    string url_incomplete = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "5&FILTER=" + FILTERS;
                    //lbl_incomplete.Text = "<a href='" + url_incomplete + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_incomplete.Text + "</a>";

                    // lbl_incomplete.Text = "<a href='" + url_incomplete + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_incomplete.Text + "";
                    lbl_incomplete.Text = "0";
                }
                else
                {
                    lbl_incomplete.Text = "0";
                }


                if (lbl_waiting.Text != "" && lbl_waiting.Text != "0")
                {
                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "3&FILTER=" + FILTERS;
                        lbl_waiting.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_waiting.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string url_waiting = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "3&FILTER=" + FILTERS; ;
                            lbl_waiting.Text = "<a href='" + url_waiting + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_waiting.Text + "</a>";
                        }
                    }
                }
                else
                {
                    lbl_waiting.Text = "0";
                }


                if (lbl_inprocess.Text != "" && lbl_inprocess.Text != "0")
                {

                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "2&FILTER=" + FILTERS;
                        lbl_inprocess.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inprocess.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string url_inprocess = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "2&FILTER=" + FILTERS;
                            lbl_inprocess.Text = "<a href='" + url_inprocess + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inprocess.Text + "</a>";
                        }
                    }
                }

                else
                {
                    lbl_inprocess.Text = "0";
                }


                if (lbl_inspection_passed.Text != "" && lbl_inspection_passed.Text != "0")
                {
                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "7&FILTER=" + FILTERS;
                        lbl_inspection_passed.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inspection_passed.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string url_inspection_passed = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "7&FILTER=" + FILTERS;
                            lbl_inspection_passed.Text = "<a href='" + url_inspection_passed + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inspection_passed.Text + "</a>";
                        }
                    }
                }
                else
                {
                    lbl_inspection_passed.Text = "0";
                }


                if (lbl_registerd_unit.Text != "" && lbl_registerd_unit.Text != "0")
                {

                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "1&FILTER=" + FILTERS;
                        lbl_registerd_unit.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_registerd_unit.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string url_register_unit = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "1&FILTER=" + FILTERS;
                            lbl_registerd_unit.Text = "<a href='" + url_register_unit + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_registerd_unit.Text + "</a>";
                        }
                    }
                }
                else
                {
                    lbl_registerd_unit.Text = "0";
                }

                if (lbl_expired_unit.Text != "" && lbl_expired_unit.Text != "0")
                {

                    if (lblzonename_id.Text != "" && lblzonename_id.Text != "0") //RA Zone wise list
                    {
                        string url_Zone = "traceliveexpiredunitcompanywise.aspx?ZoneId=" + Convert.ToInt32(lblzonename_id.Text.ToString()) + "&Status=" + "4&FILTER=" + FILTERS;
                        lbl_expired_unit.Text = "<a href='" + url_Zone + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_expired_unit.Text + "</a>";
                    }
                    else
                    {
                        if (lbl_branch_id.Text.ToString() != "")
                        {
                            string urlexpiredunit = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "4&FILTER=" + FILTERS;
                            lbl_expired_unit.Text = "<a href='" + urlexpiredunit + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_expired_unit.Text + "</a>";
                        }
                    }
                }
                else
                {
                    lbl_expired_unit.Text = "0";
                }


                Label zonename = (Label)e.Row.FindControl("lbl_zonename");
                bool isZoneTotal = false;
                if (zonename.Text == "<b><span style=\"color:Red;\">Total</span></b>")
                {
                    isZoneTotal = true;
                    Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");
                    string url = "traceliveexpiredunitcompanywise.aspx?Status=" + "1,2,3,4,7&FILTER=" + FILTERS;
                    lblbrancname.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lblbrancname.Text + "</a>";
                 
              //
                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "1,2,3,7&FILTER=" + FILTERS;
                    lbl_applied.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_applied.Text + "</a>";
                 

                    //

                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "1&FILTER=" + FILTERS;
                     lbl_registerd_unit.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_registerd_unit.Text + "</a>";
                   
                    
                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "3&FILTER=" + FILTERS;
                     lbl_waiting.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_waiting.Text + "</a>";
                 //

                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "2&FILTER=" + FILTERS;
                     lbl_inprocess.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inprocess.Text + "</a>";
                 


                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "7&FILTER=" + FILTERS;
                     lbl_inspection_passed.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inspection_passed.Text + "</a>";
                 
                //
                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "1,2,3,7&FILTER=" + FILTERS;
                     lbl_underprocess.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_underprocess.Text + "</a>";
                 

                     url = "traceliveexpiredunitcompanywise.aspx?Status=" + "4&FILTER=" + FILTERS;
                     lbl_expired_unit.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_expired_unit.Text + "</a>";
                 //

                  

                }
                
                /* Label zonename = (Label)e.Row.FindControl("lbl_zonename");
                 Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");
                 string zonenmae1 = zonename.Text;
                 if (zonename.Text != "" && zonename.Text!=null)
                 {

                     Label totalapplied = (Label)e.Row.FindControl("lbl_applied");
                     applied = Convert.ToInt32(totalapplied.Text);
                     Label totalRegisterd = (Label)e.Row.FindControl("lbl_registerd");
                     Label totalincomplete = (Label)e.Row.FindControl("lbl_Incomplete");
                     Label totalwaiting = (Label)e.Row.FindControl("lbl_waiting");
                     Label totaltotalinprocess = (Label)e.Row.FindControl("lbl_inprocess");
                     Label totalinspectionpased = (Label)e.Row.FindControl("lbl_inspection_passed");
                    
               }
                 TotalApplied += applied;*/
            }

            /* if (e.Row.RowType == DataControlRowType.Footer)
             {
                 Label lbl_Appliedtotal = (Label)e.Row.FindControl("lbl_applied_total");
                 Label lbl_Registerdtotal = (Label)e.Row.FindControl("lbl_registerd_total");
                 Label lbl_registerdtotal = (Label)e.Row.FindControl("lbl_registerd_total");
                 Label lbl_waitingtotal = (Label)e.Row.FindControl("lbl_waiting_total");
                 Label lbl_incompletetotal = (Label)e.Row.FindControl("lbl_incomplete_total");
                 Label lbl_inprocesstoral = (Label)e.Row.FindControl("lbl_inprocess_total");
                 Label lbl_inspectionpassedtotal = (Label)e.Row.FindControl("lbl_inspection_passed_total");
                 Label lbl_underprocesstotal = (Label)e.Row.FindControl("lbl_underprocess_total");
                 lbl_Appliedtotal.Text = Convert.ToString(TotalApplied);
                 //Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");
             }*/


            if (e.Row.RowType == DataControlRowType.Footer)
            {
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
        /* Verifies that the control is rendered */
    }



    public DataSet Fetch_zone_branch_wise_excel_report_branchwise(string Formadte, string todate, int RoleId, int show, int BranchID,
        int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
          int enterprisedefnition, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId

        )
    {

        SqlDataAdapter MyAdp = new SqlDataAdapter();
        DataSet MyDs = new DataSet();
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Sp_Gbtbl_fetch_dashBoard_reportDatewise_forexcel_Branch_wise_modified_RA1", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;
            MyAdp.SelectCommand.Parameters.Add("@GridViewexcel", SqlDbType.Int).Value = show;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;

            //March 30 RA
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
            MyAdp.SelectCommand.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
            //



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


    // Make Excel Report













    public void MakeExcelSheet()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            DataTable dtemp = new DataTable();
            DataTable dttest = new DataTable();
            int bsCategoryAreaId = -1;
            int bsCategoryCastId = -1;
            int bsCategoryUnitSizeId = -1;
            int natureOfOperation = -1;
            int headOfficeState = -1;
            int headOfficeCity = -1;
            string fromdate = ""; string todate = "";
            if (txt_fromdate.Text != "" && txt_fromdate.Text != "__/__/____")
            {
                fromdate = Convert.ToDateTime(txt_fromdate.Text).ToString("yyyy-MM-dd");
                lbl_fromcdate.Text = "From Date" + fromdate.ToString();
            }
            if (txt_todate.Text != "" && txt_todate.Text != "__/__/____")
            {
                todate = Convert.ToDateTime(txt_todate.Text).ToString("yyyy-MM-dd");
                lbl_tcdate.Text = "To date" + todate.ToString();
            }
            bsCategoryCastId = ddl_1e.SelectedValue != "" ? Convert.ToInt32(ddl_1e.SelectedValue) : -1;

            bsCategoryAreaId = ddl_1d.SelectedValue != "" ? Convert.ToInt32(ddl_1d.SelectedValue) : -1;


            bsCategoryUnitSizeId = DdlCategoryUnitSize.SelectedValue != "" ? Convert.ToInt32(DdlCategoryUnitSize.SelectedValue) : -1;
            natureOfOperation = Convert.ToInt32(rdo_manufacturlist.SelectedValue);

            headOfficeState = ddlState.SelectedValue != "" ? Convert.ToInt32(ddlState.SelectedValue) : -1;

            headOfficeCity = Ddl_City_Wise.SelectedValue != "" ? Convert.ToInt32(Ddl_City_Wise.SelectedValue) : -1;

            FILTERS = "|" + bsCategoryAreaId + "|" + bsCategoryCastId + "|" + bsCategoryUnitSizeId + "|" + natureOfOperation +
                "|" + headOfficeState + "|" + headOfficeCity + "|" + fromdate + "|" + todate + "|";
         
            ds = objclsCompany.Fetch_zone_branch_wise_excel_report(fromdate, todate, ROLEID, 1,
                bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    headOfficeState, headOfficeCity
                
                );  // 1 for excelsheet
            if (ds.Tables[0].Rows.Count > 0)
            {

                //try
                //{
                //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                //    {
                //        string a = ds.Tables[0].Rows[i]["Applied"].ToString();
                //        string b = ds.Tables[0].Rows[i]["Registerd"].ToString();
                //        string c = ds.Tables[0].Rows[i]["waiting"].ToString();
                //        string d = ds.Tables[0].Rows[i]["Inprocess"].ToString();
                //        string e = ds.Tables[0].Rows[i]["InspectionPassed"].ToString();
                //        string f = ds.Tables[0].Rows[i]["UnderprocessUnit"].ToString();
                //        string g = ds.Tables[0].Rows[i]["expired"].ToString();



                //        if ((a == "0" && b == "0" && c == "0" && d == "0" && e == "0" && f == "0" && g == "0") ||
                //            a == "" && b == "" && c == "" && d == "" && e == "" && f == "" && g == ""
                //            )
                //        {
                //            DataRow dr = ds.Tables[0].Rows[i];
                //            ds.Tables[0].Rows.Remove(dr);
                //            ds.AcceptChanges();

                //        }

                //    }

                //}
                //catch (Exception err) { }
                ds.Tables[0].Columns.RemoveAt(6);

                ds.Tables[0].AcceptChanges();

                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Dashboard Report" + fromdate + " " + todate + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(ds, "Export.xls", fromdatetodate);


            }
            else
            {
                ds.Dispose();
            }



        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
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


    protected void btn_chek_Click(object sender, EventArgs e)
    {
        try
        {
            MakeExcelSheet();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }

    public void countcompany()
    {
        SqlCommand sqlcom = new SqlCommand();
        try
        {
            sqlcom.Connection = DBManager.ConnectToDB();
            sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
            string comcount = Convert.ToString(sqlcom.ExecuteScalar());
            TotalCount = comcount;
            lbl_labelcount.Text = TotalCount.ToString();

            lbl_total_new_company.Text = TotalCount.ToString();
            sqlcom.Connection.Close();
        }
        catch { }
        finally
        {
            sqlcom.Dispose();
            DBManager.CloseConnection();
        }
    }
    protected void DdlCategoryUnitSize_SelectedIndexChanged(object sender, EventArgs e)
    {

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
