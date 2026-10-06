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

public partial class appBuyingAgency_BuyingAgencySearch : System.Web.UI.Page
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
                lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text +""+""+ ":" +""+ SearchText.ToString();
            }

            Bind_Buying_Agencies_DG(crit, SearchText);

        }
        else
        {
            pnl1.Visible = true;
            pnl_dg.Visible = false;
            Bind_Buying_Agencies();
           
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
                }
                objclscompany = new clsCompany();
                Label lbl_validity = (Label)e.Row.FindControl("lbl_validity");
                DataSet ds_chekvality = new DataSet();
                ds_chekvality = objclscompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id.Value));
                if (ds_chekvality.Tables[0].Rows.Count > 0)
                {
                    lbl_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
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
            DataSet ds_dg = new DataSet();
            ds_dg = objclscompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id_dg.Value));
            if (ds_dg.Tables[0].Rows.Count > 0)
            {
                lbl_validity_dg.Text = ds_dg.Tables[0].Rows[0]["validity"].ToString();
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
