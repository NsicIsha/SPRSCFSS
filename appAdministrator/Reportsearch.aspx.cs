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
using System.Globalization;

public partial class appAdministrator_Reportsearch : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    string SearchText = string.Empty;
    clsMasterInspectionAgency objAgency;
    clsCompany objcompany;
    DataTable dt;
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
    }
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        //update_panel_grid.Update();
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
                lbl_search_company.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + SearchText.ToString();
              
            }
            else
            {

                lbl_search_company.Text = "";
            }

            Bind_Buying_Agencies_DG(crit, SearchText);
          
            update_panel_grid.Update();
    

    }
    private void Bind_Buying_Agencies_DG(int searchcriteria, string searchtext)
    {
        try
        {

          //  if (ROLEID == 9)
           // {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();


                dt = objAgency.Report_Fetch_Search(searchcriteria, searchtext);
                if (dt.Rows.Count > 0)
                {

                    grd_search_result.DataSource = dt;
                    grd_search_result.DataBind();
                    lbl_company_count.Text = "Total Companies" + " " + ":" + " " + dt.Rows.Count.ToString();
                  

                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
                    //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
                    txt_serch_Criteria.Text = "";
                    //  grd_search_result.Dispose();
                    grd_search_result.DataBind();
                    dt.Dispose();
                    lbl_company_count.Visible=false;
                }
           // }
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
              /*  Label lbl_company_validity = (Label)e.Row.FindControl("lbl_validititytext");
                Label lbl_company_validity_text_show = (Label)e.Row.FindControl("lbl_validity_show");
                Label lbl_renewalontext = (Label)e.Row.FindControl("lbl_renewal_date");
                Label lblrenewalontextshow = (Label)e.Row.FindControl("lbl_renewal_date_show");
                Label lblrenewalvalidity = (Label)e.Row.FindControl("lbl_renwal_validity");
                Label lblrenewalvaliditytextshow = (Label)e.Row.FindControl("lbl_renwal_validityshow");
                Label lbl_last_upadte_on = (Label)e.Row.FindControl("lbl_last_validity_text");
                Label lbl_last_updateontextshow = (Label)e.Row.FindControl("lbl_last_validity_showtext");*/
                Label lbl_validity = (Label)e.Row.FindControl("lbl_validity");
                Label lbl_renewalcheck = (Label)e.Row.FindControl("lbl_renewal_date");
                Label lblfreshprovisinalcheck = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                Label lbl_checkvalidity_text=(Label)e.Row.FindControl("lbl_validity_text");
                Label lbl_chevalidity_show=(Label)e.Row.FindControl("lbl_validity_text_show");

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyID = objDecEnc.Encrypt(hdn_company_id.Value.ToString());
                string EncryptCompany_Name = objDecEnc.Encrypt(lbl_compnay_name.Text.ToString());
                string EncryptID = objDecEnc.Encrypt(Hdn_Unit_id.Value.ToString());
                string EncryptBranchName = objDecEnc.Encrypt(Hdn_Branch_name.Value.ToString());
                string imageid = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + EncryptCompanyID + "&Com_Name=" + EncryptCompany_Name + "&id=" + EncryptID + "&BranchName=" + EncryptBranchName;
                DataSet ds_chekvality = new DataSet();
                objcompany = new clsCompany();
                ds_chekvality = objcompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id.Value));
                if (ds_chekvality.Tables[0].Rows.Count > 0)
                {
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


                    /*
                    lbl_company_validity_text_show.Visible = true;

                    lbl_company_validity_text_show.Text = ds_chekvality.Tables[0].Rows[0]["RegistrationFresh"].ToString();
                    if (ds_chekvality.Tables[0].Rows[0]["Renewal_Date"].ToString() == "01/01/1900")
                    {

                        lbl_renewalontext.Visible = false;
                        lblrenewalontextshow.Text = "";
                        lblrenewalvalidity.Visible = false;
                        lblrenewalvaliditytextshow.Text = "";
                       
                    }
                    else
                    {
                        lbl_renewalontext.Visible = true;
                        lblrenewalontextshow.Text = ds_chekvality.Tables[0].Rows[0]["Renewal_Date"].ToString();
                        lblrenewalvalidity.Visible = true;
                        lblrenewalvaliditytextshow.Text = ds_chekvality.Tables[0].Rows[0]["REnealvalidity"].ToString();
                    }
                    if (ds_chekvality.Tables[0].Rows[0]["last_validity"].ToString() != "")
                    {
                        if (System.DateTime.Today.Date > Convert.ToDateTime(ds_chekvality.Tables[0].Rows[0]["last_validity"].ToString()))
                        {
                            lbl_last_updateontextshow.Text = Convert.ToString(ds_chekvality.Tables[0].Rows[0]["last_validity"].ToString());
                            lbl_last_upadte_on.Visible = true;
                        }
                        else
                        {
                            lbl_last_updateontextshow.Text = "";
                            lbl_last_upadte_on.Visible = false;
                        }
                    }
                    else
                    {
                        lbl_last_updateontextshow.Text = "";
                        lbl_last_upadte_on.Visible = false;
                    } */
                   
                      

                     
                }
                else
                {
                    ds_chekvality.Dispose();
                }
              
                lnk_view.Attributes.Add("onclick", "return FunOpenPopup('" + imageid + "');");
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

            if(ROLEID==1)
            {
                Bind_Buying_Agencies_DG(Convert.ToInt32(ddlSearchCriteria.SelectedValue), txt_serch_Criteria.Text);
            }
            else
            {
                Bind_Buying_Agencies();
            }

            update_panel_grid.Update();
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void Bind_Buying_Agencies()
    {
        try
        {
            if (ROLEID == 9)
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
                    lbl_search_company.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + SearchText.ToString();
                }
                else
                {

                    lbl_search_company.Text = "";
                }


                dt = objAgency.Report_Fetch_Search(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
                if (dt.Rows.Count > 0)
                {
                    grd_search_result.DataSource = dt;
                    grd_search_result.DataBind();
                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
                    //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
                    txt_serch_Criteria.Text = "";
                    //  grd_search_result.Dispose();
                    grd_search_result.DataBind();
                    dt.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

}
