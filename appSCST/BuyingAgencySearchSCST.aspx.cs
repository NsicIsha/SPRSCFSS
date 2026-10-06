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
using NSICGP_VBClassLibrary;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;

public partial class appBuyingAgency_BuyingAgencySearchSCST : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


    protected void Btn_Search_Click(object sender, EventArgs e)
    {

        if (USERID == 3)
        {
            pnl1.Visible = false;
            pnl_dg.Visible = true;
            //  red_box.Visible = false;
            //    red_box.Visible = false;
            //    lbl_blacklist_company.Visible = false;
            int crit;
            crit = Convert.ToInt32(ddlSearchCriteria.SelectedValue);
            SearchText = txt_serch_Criteria.Text.Trim().ToString();
            if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
            {
                SearchText = "";
            }
            else
            {
                SearchText = txt_serch_Criteria.Text.Trim().ToString();
            }
            if (SearchText != "")
            {
                lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + "" + ":" + "" + SearchText.ToString();
            }

            Bind_Buying_Agencies_DG(crit, SearchText);

        }
        else
        {

            //   red_box.Visible = true;
            //    lbl_blacklist_company.Visible = true;

            pnl1.Visible = true;
            pnl_dg.Visible = false;
            Bind_Buying_Agencies();

        }
    }

    private void Bind_Buying_Agencies()
    {
        try
        {
            if (ROLEID == 17)
            {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();
                SearchText = txt_serch_Criteria.Text.Trim().ToString();

                if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
                {
                    SearchText = "";
                }
                else
                {
                    SearchText = txt_serch_Criteria.Text.Trim().ToString();
                }

                if (SearchText != "")
                {
                    lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + "" + SearchText.ToString();
                }
                else
                {
                    lbl_searchcompany.Text = "";
                }
                //  System.Data.SqlClient.SqlDataReader dr = objAgency.BuyingAgency_Fetch_Search_test(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
                dt = objAgency.BuyingAgency_Fetch_Search(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
                //dt.Load(dr);
                if (dt.Rows.Count > 0)
                {

                    grd_search_result.DataSource = dt;
                    grd_search_result.DataBind();
                    lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
                    //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
                    txt_serch_Criteria.Text = "";
                    //  grd_search_result.Dispose();
                    grd_search_result.DataBind();
                    grd_search_result.PageIndex = 0;
                    dt.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void grd_search_onrowdatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk_view = (LinkButton)e.Row.FindControl("lnkview");
                HiddenField hdn_company_id = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview");
                HiddenField Hdn_Unit_id = (HiddenField)e.Row.FindControl("HiddenFieldUnitId");
                HiddenField Hdn_Branch_name = (HiddenField)e.Row.FindControl("HiddenFieldBranch");
                Label lbl_compnay_name = (Label)e.Row.FindControl("lbl_Company");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyID = objDecEnc.Encrypt(hdn_company_id.Value.ToString());
                string EncryptCompany_Name = objDecEnc.Encrypt(lbl_compnay_name.Text.ToString());
                string EncryptID = objDecEnc.Encrypt(Hdn_Unit_id.Value.ToString());
                string EncryptBranchName = objDecEnc.Encrypt(Hdn_Branch_name.Value.ToString());
                Label lbl_companystatus = (Label)e.Row.FindControl("lbl_company_status");
                if (Convert.ToInt32(lbl_companystatus.Text) == 6)
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    lnk_view.Text = "View" + "<br/>" + "<b>" + "BlackList" + "</b>";
                }
                else
                {
                    lnk_view.Text = "View";
                }
                objclscompany = new clsCompany();
                Label lbl_validity = (Label)e.Row.FindControl("lbl_validity");
                Label lbl_renewalcheck = (Label)e.Row.FindControl("lbl_renewal_date");
                Label lblfreshprovisinalcheck = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                Label lbl_checkvalidity_text = (Label)e.Row.FindControl("lbl_validity_text");
                Label lbl_chevalidity_show = (Label)e.Row.FindControl("lbl_validity_text_show");

                DataSet ds_chekvality = new DataSet();
                ds_chekvality = objclscompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id.Value));
                if (ds_chekvality.Tables[0].Rows.Count > 0)
                {
                    // lbl_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();

                    lbl_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
                    lbl_renewalcheck.Text = ds_chekvality.Tables[0].Rows[0]["RenewalDate_Check"].ToString();
                    lblfreshprovisinalcheck.Text = ds_chekvality.Tables[0].Rows[0]["fresh_provisional"].ToString();
                    lbl_registration_date.Text = ds_chekvality.Tables[0].Rows[0]["RegistrationDate"].ToString();

                    if (lbl_renewalcheck.Text == "1/1/1900 12:00:00 AM")
                    {
                        lbl_checkvalidity_text.Visible = false;
                        lbl_chevalidity_show.Visible = false;

                    }
                    else
                    {
                        CultureInfo ci = new CultureInfo("en-GB");
                        DateTime RegistrationStartDate = Convert.ToDateTime(lbl_registration_date.Text);
                        if (lblfreshprovisinalcheck.Text == "0")
                        {
                            DateTime RegistrationendDate = RegistrationStartDate.AddYears(2).AddDays(-1);
                            DateTime now = System.DateTime.Today.Date;
                            if ((now > RegistrationStartDate) && (now < RegistrationendDate))
                            {
                                lbl_checkvalidity_text.Visible = true;
                                lbl_chevalidity_show.Visible = true;
                                lbl_chevalidity_show.Text = RegistrationendDate.ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            DateTime RegistrationendDate_pro = RegistrationStartDate.AddYears(1).AddDays(-1);
                            DateTime now = System.DateTime.Today.Date;
                            if ((now > RegistrationStartDate) && (now < RegistrationendDate_pro))
                            {
                                lbl_checkvalidity_text.Visible = true;
                                lbl_chevalidity_show.Visible = true;
                                lbl_chevalidity_show.Text = RegistrationendDate_pro.ToString("dd/MM/yyyy");
                            }
                        }
                    }



                }
                else
                {
                    ds_chekvality.Dispose();
                }


                //string imageid = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + EncryptCompanyID + "&Com_Name=" + EncryptCompany_Name + "&id=" + EncryptID + "&BranchName=" + EncryptBranchName;

                // lnk_view.Attributes.Add("onclick", "return FunOpenPopup('" + imageid + "');");
                string imageid = "../appAdministrator/CompanyCertificatePrintPreviewPage.aspx?id=" + hdn_company_id.Value;
                lnk_view.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void grdserch_pageindex_changing(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_search_result.PageIndex = e.NewPageIndex;
            Bind_Buying_Agencies();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void Bind_Buying_Agencies_DG(int searchcriteria, string searchtext)
    {
        try
        {

            if (ROLEID == 9)
            {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();


                // dt = objAgency.DG_Fetch_Search(searchcriteria, searchtext);
                dt = objAgency.DG_Fetch_Search_modified(searchcriteria, searchtext);
                if (dt.Rows.Count > 0)
                {

                    grd_search_result_dg.DataSource = dt;
                    grd_search_result_dg.DataBind();
                    grd_search_result.PageIndex = 0;
                    lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();

                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
                    //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
                    txt_serch_Criteria.Text = "";
                    //  grd_search_result.Dispose();
                    dt.Dispose();
                    grd_search_result.DataBind();
                    lbl_totalcount_buying.Text = "";


                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grd_search_result_dg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Literal litstore = (Literal)e.Row.FindControl("lbl_store");
            string Address = litstore.Text.Replace(",", "<br/>");

            //string decodedText = HttpUtility.HtmlDecode(Address);
            // e.Row.Cells[3].Text = decodedText;
            //   e.Row.Cells[6].Style.Add(HtmlTextWriterStyle.Width, "250px");
            LinkButton lnkview_dg = (LinkButton)e.Row.FindControl("lnkview_dg");
            HiddenField hdn_company_id_dg = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview_dg");
            string imageid1 = "../appAdministrator/CompanyCertificatePrintPreviewPage.aspx?id=" + hdn_company_id_dg.Value;
            lnkview_dg.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid1 + "');");

            Label lbl_company_status = (Label)e.Row.FindControl("lbl_company_status");
            //if(Convert.ToInt32(lbl_company_status.Text) == 6)
            //{
            //    e.Row.BackColor = System.Drawing.Color.Red;  
            //}

            objclscompany = new clsCompany();
            Label lbl_validity_dg = (Label)e.Row.FindControl("lbl_validity_dg");
            Label lbl_renewalcheck_dg = (Label)e.Row.FindControl("lbl_renewal_date_dg");
            Label lblfreshprovisinalcheck_dg = (Label)e.Row.FindControl("lbl_provisinol_fresh_dg");
            Label lbl_registration_date_dg = (Label)e.Row.FindControl("lbl_registratondate_dg");
            Label lbl_checkvalidity_text_dg = (Label)e.Row.FindControl("lbl_validity_text_dg");
            Label lbl_chevalidity_show_dg = (Label)e.Row.FindControl("lbl_validity_text_show_dg");


            DataSet ds_dg = new DataSet();
            ds_dg = objclscompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id_dg.Value));
            if (ds_dg.Tables[0].Rows.Count > 0)
            {
                lbl_validity_dg.Text = ds_dg.Tables[0].Rows[0]["validity"].ToString();
                lbl_renewalcheck_dg.Text = ds_dg.Tables[0].Rows[0]["RenewalDate_Check"].ToString();
                lblfreshprovisinalcheck_dg.Text = ds_dg.Tables[0].Rows[0]["fresh_provisional"].ToString();
                lbl_registration_date_dg.Text = ds_dg.Tables[0].Rows[0]["RegistrationDate"].ToString();

                if (lbl_renewalcheck_dg.Text == "1/1/1900 12:00:00 AM")
                {
                    lbl_checkvalidity_text_dg.Visible = false;
                    lbl_chevalidity_show_dg.Visible = false;

                }
                else
                {
                    CultureInfo ci = new CultureInfo("en-GB");
                    DateTime RegistrationStartDate_dg = Convert.ToDateTime(lbl_registration_date_dg.Text);
                    if (lblfreshprovisinalcheck_dg.Text == "0")
                    {
                        DateTime RegistrationendDate_dg = RegistrationStartDate_dg.AddYears(2).AddDays(-1);
                        DateTime now_dg = System.DateTime.Today.Date;
                        if ((now_dg > RegistrationStartDate_dg) && (now_dg < RegistrationendDate_dg))
                        {
                            lbl_checkvalidity_text_dg.Visible = true;
                            lbl_chevalidity_show_dg.Visible = true;
                            lbl_chevalidity_show_dg.Text = RegistrationendDate_dg.ToString("dd/MM/yyyy");
                        }
                    }
                    else
                    {
                        DateTime RegistrationendDate_pro_dg = RegistrationStartDate_dg.AddYears(1).AddDays(-1);
                        DateTime now_dg = System.DateTime.Today.Date;
                        if ((now_dg > RegistrationStartDate_dg) && (now_dg < RegistrationendDate_pro_dg))
                        {
                            lbl_checkvalidity_text_dg.Visible = true;
                            lbl_chevalidity_show_dg.Visible = true;
                            lbl_chevalidity_show_dg.Text = RegistrationendDate_pro_dg.ToString("dd/MM/yyyy");
                        }
                    }
                }
            }
            else
            {
                ds_dg.Dispose();
            }
        }

    }

    protected void grd_search_result_dg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grd_search_result_dg.PageIndex = e.NewPageIndex;

            int crit;
            crit = Convert.ToInt32(ddlSearchCriteria.SelectedValue);
            SearchText = txt_serch_Criteria.Text.Trim().ToString();
            if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
            {
                SearchText = "";
            }

            else
            {
                SearchText = txt_serch_Criteria.Text.Trim().ToString();
            }

            if (SearchText != "")
            {
                lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + "" + SearchText.ToString();
            }
            else
            {
                lbl_searchcompany.Text = "";
            }
            Bind_Buying_Agencies_DG(crit, SearchText);

        }
        catch (Exception Ex)
        {
            ErrHandler.WriteError(Ex.Message);
        }
    }
}