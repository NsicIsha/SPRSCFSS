using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Text;




public partial class appAdministrator_REPORTEditCompanyMonetryStore : System.Web.UI.Page
{
    clsCompany objCompany = new clsCompany();
    DataSet ds;
    DataSet dsAmedment;
    DataSet dsrenewal;
    string Amedment = "";
    string Renewal = "";
    int UserId, RoleId;
    StringBuilder sb;
    StringBuilder sb1;
    string Text = "";
    string Text_Date = "";
    string Text_Renewal = "";
    string Text_renewal_Date = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }

    }
    protected void btn_serch_Click(object sender, EventArgs e)
    {

        try
        {
            BindGridView();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public void BindGridView()
    {
        try
        {
            int ddlvalue = Convert.ToInt32(ddlSearchCriteria.SelectedValue.ToString());
            string serachtext = string.Empty;
            if (txt_search.Text == "")
            {
                serachtext = "";
            }
            else
            {
                serachtext = txt_search.Text.ToString();
            }
            objCompany = new clsCompany();
            ds = objCompany.Company_fetch_history_Companywise(ddlvalue, serachtext);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_view_history.DataSource = ds.Tables[0];
                grd_view_history.DataBind();
            }
            else
            {
                ds.Dispose();
                grd_view_history.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    protected void ddlSearchCriteria_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (txt_search.Text != "")
            {
                txt_search.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

       
    }
    protected void grd_view_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_view_history.PageIndex = e.NewPageIndex;
        BindGridView();

    }
    protected void grd_view_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_companyid = (Label)e.Row.FindControl("lbl_Company_id");
                Label lbl_company_name = (Label)e.Row.FindControl("lbl_Company_Name");
                Label lbl_statusttext = (Label)e.Row.FindControl("lbl_comapny_name_text");
                if (lbl_company_name.Text != "")
                {
                    lbl_company_name.Text = "<b>" + lbl_company_name.Text.ToString() + "</b>";
                }
                Label lbl_Company_Address = (Label)e.Row.FindControl("lbl_Comapny_Address");
                if (lbl_Company_Address.Text != "")
                {
                    lbl_Company_Address.Text = lbl_Company_Address.Text.ToString();
                }
                Label lbl_phone = (Label)e.Row.FindControl("lbl_phone");
                if (lbl_phone.Text != "")
                {
                    lbl_phone.Text = "<b>" + "Phone :</b>" + lbl_phone.Text.ToString();
                }
                Label lbl_email = (Label)e.Row.FindControl("lbl_email");
                if (lbl_email.Text != "")
                {
                    lbl_email.Text = "<b>" + "Email :</b>" + lbl_email.Text.ToString();
                }
                Label lbl_status = (Label)e.Row.FindControl("lbl_status");
                if (lbl_status.Text != "")
                {
                    if (lbl_status.Text.ToString() == "Incomplete")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form incomplete.";
                    }
                    if (lbl_status.Text.ToString() == "Waiting")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form waiting to be accepted by Branch Office .";
                    }
                    if (lbl_status.Text.ToString() == "Inprocess")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Application Form sent to Inspection Agency for inspection.";
                    }
                    if (lbl_status.Text.ToString() == "Inspection Passed")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Monetary Limit to be set by Branch Account.";
                    }
                    if (lbl_status.Text.ToString() == "Registerd")
                    {
                        lbl_status.Text = "<b>" + "Status :</b>" + "Registered";
                    }
                }
                Label lbl_Registrationno = (Label)e.Row.FindControl("lbl_registration_number");
                if (lbl_Registrationno.Text != "")
                {
                    lbl_Registrationno.Text = "<b>" + "Registration No:</b>" + lbl_Registrationno.Text.ToString();
                }
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registration_date");
                if (lbl_registration_date.Text != "01/01/1900" && lbl_registration_date.Text != "")
                {
                    lbl_registration_date.Text = "<b>" + "Registration Date :</b>" + lbl_registration_date.Text.ToString();
                }
                Label lbl_EditDate = (Label)e.Row.FindControl("lbl_EditDateWise_date");
                sb = new StringBuilder();

                objCompany = new clsCompany();
                DataSet ds_Edit_Data = new DataSet();
                ds_Edit_Data = objCompany.Company_fetch_EditCompanyData(Convert.ToInt32(lbl_companyid.Text));
                if (ds_Edit_Data.Tables[0].Rows.Count > 0)
                {

                    sb.Append("<table cellpadding='5' cellspacing='5' border='0'>");
                    for (int m = 0; m <= ds_Edit_Data.Tables[0].Rows.Count - 1; m++)
                    {
                        string imageid = "";
                        string imageidedit_turnover = "";
                        string imageidCunit_Store_History = "";
                        string Companyidpkinput = ds_Edit_Data.Tables[0].Rows[m]["Company_Id_pk"].ToString();
                        DateTime dttime = new DateTime();
                        if ((ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString() != "") && (ds_Edit_Data.Tables[0].Rows[m]["CompanyEditData"].ToString()=="C"))
                          {
                              
                        dttime = DateTime.Parse(ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString());
                        imageid = "../appAdministrator/EditCompanyDataHistory.aspx?Company_ID=" + Convert.ToInt32(lbl_companyid.Text.ToString()) + "&CompanyIdPk=" + Convert.ToInt32(Companyidpkinput.ToString());
                        sb.Append("<tr><td align='left' valign='top'>" + "<b>" + "Edit on" + ":" + " " + "</b>" + "<a href=" + imageid + " target='_blank'>" + String.Format("{0:f}", dttime) + "</a>" + "</td></tr>");
                          }
                        DateTime dtEditturnover=new DateTime();
                        if ((ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString() != "") && (ds_Edit_Data.Tables[0].Rows[m]["CompanyEditData"].ToString()=="T"))
                        {
                            dtEditturnover = DateTime.Parse(ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString());
                            imageidedit_turnover = "../appAdministrator/EditturnoverHistory.aspx?Company_ID=" + Convert.ToInt32(lbl_companyid.Text.ToString()) + "&CompanyIdPk=" + Convert.ToInt32(Companyidpkinput.ToString());
                            sb.Append("<tr><td align='left' valign='top'>" + "<b>" + "Edit TurnOver on" + ":" + " " + "</b>" + "<a href=" + imageidedit_turnover + " target='_blank'>" + String.Format("{0:f}", dtEditturnover) + "</a>" + "</td></tr>");
                        }
                        DateTime dtStoreOver=new DateTime();
                        if ((ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString() != "") && (ds_Edit_Data.Tables[0].Rows[m]["CompanyEditData"].ToString()=="S"))
                        {
                            dtStoreOver = DateTime.Parse(ds_Edit_Data.Tables[0].Rows[m]["EditDate"].ToString());
                            imageidCunit_Store_History = "../appAdministrator/EditCunit_StoreHistory.aspx?Company_ID=" + Convert.ToInt32(lbl_companyid.Text.ToString()) + "&CompanyIdPk=" + Convert.ToInt32(Companyidpkinput.ToString());
                           sb.Append("<tr><td align='left' valign='top'>" + "<b>" + "Edit StoreDetails on" + ":" + " " + "</b>" + "<a href="+imageidCunit_Store_History +" target='_blank'>" + String.Format("{0:f}", dtStoreOver) + "</a>" + "</td></tr>"); 
                        }
                      
                       
                    
                      
                      
                    }
                    sb.Append("</table>");
                    lbl_EditDate.Text = sb.ToString();

                }

                else
                {
                    lbl_EditDate.Text = "";
                    ds_Edit_Data.Dispose();
                }
            }
                
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }

    }
}
