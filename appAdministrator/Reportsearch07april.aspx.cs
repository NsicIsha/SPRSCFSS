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
                Label lbl_company_validity = (Label)e.Row.FindControl("lbl_validity");

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
                    lbl_company_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
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
