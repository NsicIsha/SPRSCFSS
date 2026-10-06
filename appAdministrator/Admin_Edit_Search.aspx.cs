using NSICGP_VBClassLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class appAdministrator_Admin_Edit_Search : System.Web.UI.Page
{

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();

            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            if (RoleId == 8)
            {
                btnSearch.Visible = true;
            }
            else
            {
                // btnSearch.Visible = false;
                Response.Redirect("logout.aspx");
            }
            if (!Page.IsPostBack)
            {
                //BindGridview();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        BindGridview();
    }

    private void BindGridview()
    {
        try
        {
            clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
            DataSet ds = new DataSet();
            //int processid = Convert.ToInt32(ddlSearchCriteria.SelectedValue);
            int processid = 1;
            string searchtext = txtSearchbox.Text.Trim();
            ds = objcom.Company_Details(processid, searchtext, UserId);
            /*if (ds.Tables[0].Rows.Count>0)
            {*/
            gv_companies.DataSource = ds;
            gv_companies.DataBind();

            //}
            //else
            //{
            //    gv_companies.DataSource = null;
            //    gv_companies.DataBind();
            //    gv_companies.EmptyDataText = 
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    public DataSet Company_Edit_Check(int CompanyId)
    {
        try
        {

            MyDs = new DataSet();
            //MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_All_Online_renewal", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_Company_Check_EditRequest", DBManager.ConnectToDB());

            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

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

    protected void gv_companies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToUpper() == "EDITSTORE")
            {
                //      GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                //      LinkButton lnkeditstore =(LinkButton) row.FindControl("lnkeditstore");
                //      string uid = ddlSearchCriteria.SelectedValue;
                ////      "Edit_company_store.aspx?id="+txtUserId.Text+"&UserName="+txtUserName.Text
                //     // lnkeditstore.PostBackUrl = "Edit_company_store.aspx?id="+e.CommandArgument+"&urlid="+ddlSearchCriteria.SelectedValue;
                // //    Server.Transfer("Edit_company_store.aspx?id="+e.CommandArgument+"&urlid="+ddlSearchCriteria.SelectedValue");
            }
            if (e.CommandName.ToUpper() == "EDITMONLIMIT")
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_companies_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkeditstore = (LinkButton)e.Row.FindControl("lnkeditstore");
                LinkButton lnkeditmolmt = (LinkButton)e.Row.FindControl("lnkeditmonlmt");
                LinkButton lnkeditcomid = (LinkButton)e.Row.FindControl("lnkeditcompany");
                HiddenField hdnprintaccess = (HiddenField)e.Row.FindControl("hdnPrintAccess");
                HiddenField hdnPrintFlag = (HiddenField)e.Row.FindControl("hdnPrintFlag");
                bool printCheck = false;
                int printCheckval = 0;
                if (hdnPrintFlag.Value != "")
                {
                    printCheckval = Convert.ToInt32(hdnPrintFlag.Value);
                }
                if (printCheckval == 0)
                {
                    printCheck = false;
                }
                else
                {
                    printCheck = true;
                }
                //HiddenField hdnadminaccept = (HiddenField)e.Row.FindControl("hdnAdminAccept");
                string hdnprintstr = hdnprintaccess.Value;
                //string hdnadacceptstr = hdnadminaccept.Value;
                /*  if (ddlSearchCriteria.SelectedValue == "2")
                  {
                      lnkeditstore.Enabled = false;
                  }
                  else
                  {
                      lnkeditstore.Enabled = true;

                      lnkeditstore.PostBackUrl = "Admin_Edit_Store.aspx?id=" + lnkeditstore.CommandArgument + "&urlid=" + ddlSearchCriteria.SelectedValue;
                  }*/

                DataSet dsCEditCheck = new DataSet();
                dsCEditCheck = Company_Edit_Check(Convert.ToInt32(lnkeditcomid.CommandArgument));
                int IsActive = 0;
                int RequestType = 0;

                int DataCheckinCertificate = 0;
                if (dsCEditCheck.Tables[1].Rows.Count > 0)
                {
                    DataCheckinCertificate = Convert.ToInt32(dsCEditCheck.Tables[1].Rows[0]["NoCompanyId"].ToString());
                }
                if (dsCEditCheck.Tables[0].Rows.Count > 0)
                {
                    IsActive = Convert.ToInt32(dsCEditCheck.Tables[0].Rows[0]["IsActive"].ToString());
                    RequestType = Convert.ToInt32(dsCEditCheck.Tables[0].Rows[0]["RequestType"].ToString());
                }

                if (IsActive == 1 && RequestType == 1)
                {
                    if (hdnprintaccess.Value != "1")
                    {
                        lnkeditstore.Enabled = false;
                        lnkeditmolmt.Enabled = false;
                        lnkeditcomid.Enabled = false;
                    }
                }
                else if (IsActive == 4)
                {
                    //lnkeditstore.Enabled = false;
                    //lnkeditmolmt.Enabled = false;
                    //lnkeditcomid.Enabled = false;  
                    lnkeditstore.Enabled = true;
                    lnkeditmolmt.Enabled = true;
                    lnkeditcomid.Enabled = true;
                }
                else if (IsActive == 3)
                {
                    lnkeditstore.Enabled = false;
                    lnkeditmolmt.Enabled = false;
                    lnkeditcomid.Enabled = false;
                }
                else if (printCheck == true)
                {
                    lnkeditstore.Enabled = true;
                    lnkeditmolmt.Enabled = true;
                    lnkeditcomid.Enabled = true;
                }
                else if (DataCheckinCertificate == 420)
                {
                    lnkeditstore.Enabled = true;
                    lnkeditmolmt.Enabled = true;
                    lnkeditcomid.Enabled = true;
                }
                else
                {
                    lnkeditstore.Enabled = false;
                    lnkeditmolmt.Enabled = false;
                    lnkeditcomid.Enabled = false;
                }
                //commented on 9 September 2021

                //if (hdnprintaccess.Value != "1")
                //{
                //    lnkeditstore.Enabled = false;
                //    lnkeditmolmt.Enabled = false;
                //    lnkeditcomid.Enabled = false;
                //}

                lnkeditstore.PostBackUrl = "Admin_Edit_Store.aspx?id=" + lnkeditstore.CommandArgument + "&urlid=1";
                lnkeditcomid.PostBackUrl = "AdminEditRegistration.aspx?cid=" + lnkeditcomid.CommandArgument;
                //lnkeditmolmt.PostBackUrl = "Admin_Edit_monetary.aspx?id=" + lnkeditmolmt.CommandArgument + "&urlid=1";
                clsCompanyInformationEdit objcom = new clsCompanyInformationEdit();
                DataTable dt = new DataTable();
                dt = objcom.Fetch_Edit_monetarylimit(Convert.ToInt32(lnkeditmolmt.CommandArgument.ToString()), 1);
                if (dt.Rows.Count > 1)
                {
                    lnkeditmolmt.Text = "Edit";
                    lnkeditmolmt.Enabled = true;
                }
                else
                {
                    lnkeditmolmt.Text = "Monetary Data not available";
                    lnkeditmolmt.Enabled = false;
                }
                if (hdnprintaccess.Value != "1")
                {
                    lnkeditmolmt.Enabled = false;
                }
                else
                {
                    lnkeditmolmt.Enabled = true;
                }
                lnkeditmolmt.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + lnkeditmolmt.CommandArgument + "&urlid=1" + "&edit=E";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }

    protected void gv_companies_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_companies.PageIndex = e.NewPageIndex;
        BindGridview();
    }

}
