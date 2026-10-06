using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;


public partial class appAdministrator_Convertprovisonaltofreshregistration : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName = string.Empty;
    string date = "";
    clsMasterInspectionAgency objInspAgency;
    clsCompany objcompany;
    int type;
    DataSet ds;
     string PageUrl = string.Empty;
     private string SearchString = "";
          
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // READ COOKIES
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            // END READ COOKIES
            PageUrl = GetCurrentPageName();
        }
           
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        if (!IsPostBack)
        {
            if (Request.QueryString.HasKeys())
            {
                int typeback = Convert.ToInt32(Request.QueryString["i"]);
                tbl_search.Visible = true;
               // ddltype.SelectedItem.Value = Convert.ToString( typeback);
                ddltype.SelectedValue = Convert.ToString(typeback);
                BindGridview();
            }
            else
            {

                tbl_search.Visible = false;
            }
        }

    }
    protected void grd_listofpro_reg_companies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_company_id_text = (Label)e.Row.FindControl("lbl_company_id");
                Label lbl_company_name_text = (Label)e.Row.FindControl("lbl_company_name");
                Label lbl_branch_id_text = (Label)e.Row.FindControl("lbl_branch_id");
                Label lbl_branch_name_text = (Label)e.Row.FindControl("lbl_branch_name");
                Label lbl_view = (Label)e.Row.FindControl("lbl_view_text");
                LinkButton lbl_registration_prov = (LinkButton)e.Row.FindControl("linkbutton_show");
                if (ddltype.SelectedItem.Value == "1")
                {
                    lbl_registration_prov.Text = "Convert to Regular Reg.";
                    lbl_registration_prov.CommandName = "pro";
                }
                else if (ddltype.SelectedItem.Value == "2")
                {
                    lbl_registration_prov.Text = "Convert to Provisional Reg.";
                    lbl_registration_prov.CommandName = "reg";
                }
                else
                {
                    lbl_registration_prov.Text = "";
                    lbl_registration_prov.CommandName = "None";
                }

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany_Name = "";
                string EncryptCompanyID = "";
                string EncryptBranchName = "";
                string imageid = string.Empty;
                string EncryptID = objDecEnc.Encrypt("1");
                EncryptCompany_Name = objDecEnc.Encrypt(lbl_company_name_text.Text.ToString());
                EncryptCompanyID = objDecEnc.Encrypt(lbl_company_id_text.Text.ToString());
                EncryptBranchName = objDecEnc.Encrypt(lbl_branch_name_text.Text.ToString());
                imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                lbl_view.Text = "<a href=" + imageid + " target='_blank'>" + "<b>" + "View" + "</b>" + "</a>"; 
            }
        }
        catch (Exception ex)

        {
            ErrHandler.WriteError("" + ex.StackTrace.ToString() + "" + ex.Message.ToString());
        }

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddltype.SelectedItem.Value == "0")
            {
                lbl_prov_regcompanies.Text = "";
                grd_listofpro_reg_companies.Visible = false;
                tbl_search.Visible = false;
            }
            else
            {
                grd_listofpro_reg_companies.Visible = true;
              
                tbl_search.Visible = true;
                txt_search.Text = "";
                BindGridview();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }
    protected void grd_listofpro_reg_companies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
         grd_listofpro_reg_companies.PageIndex=e.NewPageIndex ;
        BindGridview();
    }

    public void BindGridview()
    {
        try
        {
            string search = "";
            if (txt_search.Text != "")
            {
                search = txt_search.Text.ToString();
            }
            else
            {
                search = "";
            }
                     
                    if(ddltype.SelectedItem.Value=="1")
                    {
                        type = 1;
                        lbl_prov_regcompanies.Text = "List Of Provisional Companies"; 
                          
                    }
                    if (ddltype.SelectedItem.Value == "2")
                    {
                        type = 2;
                        lbl_prov_regcompanies.Text = "List Of Regular Companies"; 
                    }
                    if (ddltype.SelectedItem.Value == "0")
                    {
                        type = 0;
                        lbl_prov_regcompanies.Text = "";
                        

                    }
                    if (type != 0)
                    {
                        objcompany = new clsCompany();
                        ds = new DataSet();
                        ds = objcompany.Convert_Company_To_Regular(type, search);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grd_listofpro_reg_companies.Visible = true;
                            grd_listofpro_reg_companies.DataSource = ds.Tables[0];
                            grd_listofpro_reg_companies.DataBind();
                        }
                        else
                        {
                            ds.Dispose();
                            grd_listofpro_reg_companies.Visible = true;
                            grd_listofpro_reg_companies.DataBind();
                        }

                    }
                    else
                    {
                        grd_listofpro_reg_companies.Visible = false; 
                    }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
    protected void grd_listofpro_reg_companies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            

            objcompany=new  clsCompany();
           
            if (e.CommandName == "pro")// convert Regular Registration
            {
                string companyidtext;
                companyidtext = e.CommandArgument.ToString();
                string[] cmdbreak = companyidtext.Split(',');
                int CompanyId = Convert.ToInt32(cmdbreak[0].ToString());
                int CompanyStatus = Convert.ToInt32(cmdbreak[1].ToString());
               // CompanyId = Convert.ToInt32(e.CommandArgument.ToString());
              
                    // ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Convert Provisional Registration!!!');", true); 
                    int k = objcompany.Update_Comapmy_chng_byadmin(CompanyId, PageUrl, 1);
                    if (k == 1)
                    {
                        // window.open('Calculate_Monetary_Limit.aspx?id=" + companyidtext + "&urlid=1')
                        if (CompanyStatus == 1 || CompanyStatus == 7)
                        {
                            Response.Redirect("Calculate_Monetary_Limit.aspx?id=" + CompanyId + "&urlid=1" + "&type=1");
                        }

                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Convert Regular  Registration successfully!!!');", true);
                            BindGridview();
                        }
                        //   BindGridview();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Server Busy!!!');", true);
                        BindGridview();
                    }
            }
            else if (e.CommandName == "reg")  // Conver Provisional
            {
                string companyidtext;
                companyidtext = e.CommandArgument.ToString();
                string[] cmdbreak = companyidtext.Split(',');
                int CompanyId = Convert.ToInt32(cmdbreak[0].ToString());
                int CompanyStatus = Convert.ToInt32(cmdbreak[1].ToString());
                
                //
                int k = objcompany.Update_Comapmy_chng_byadmin(CompanyId, PageUrl, 2);
             
                    if (k == 1)
                    {
                        //   ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Convert Regular Registration!!!');", true); // window.open('Provisional_MonetaryLimit.aspx?id=" + companyidtext + " ')
                        if (CompanyStatus == 1 || CompanyStatus == 7)
                        {
                            Response.Redirect("Provisional_MonetaryLimit.aspx?id=" + CompanyId + " &urlid=1 &type=2");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Convert Provisional Registration successfully!!!');", true);
                            BindGridview();
                        }

                        // BindGridview();
                     }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Server Busy!!!');", true);
                        BindGridview();
                    }
            }
             else if(e.CommandName=="None")
            {
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }

    public string GetCurrentPageName()
    {
        string urlPath = Request.Url.AbsolutePath;
        FileInfo fileInfo = new FileInfo(urlPath);
        string pageName = fileInfo.Name;
        return pageName;
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridview();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        try
        {
            txt_search.Text = "";
            BindGridview();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    } 
}
