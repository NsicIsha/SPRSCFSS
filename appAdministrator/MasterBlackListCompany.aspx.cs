using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterBlackListCompany : System.Web.UI.Page
{

    int UserId, RoleId, DepartmentId;
    string UserFullName;
    clsMasterApplication objMApplication;

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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        if (!Page.IsPostBack)
        {
            if (RoleId == 1)
            {
                try
                {
                    FunfillGridView();
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
                }
            }
        }
        
    }

    protected void FunfillGridView()
    {
        
        objMApplication=new clsMasterApplication();
        DataSet ds = new DataSet();
        try
        {
            ds = objMApplication.Fetch_Blacklistcompany_Dataset(null,null,null,null,null,null);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBlacklist.DataSource = ds;
                gvBlacklist.DataBind();
            }
            else
            {
                gvBlacklist.EmptyDataText = "No record found";
                gvBlacklist.DataSource = null;
                gvBlacklist.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            ds.Dispose();
        }
      
    }

    protected void gvBlacklist_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnchkblacklist = (HiddenField)(e.Row.FindControl("hdn_blacklist"));
                LinkButton lnk1 = (LinkButton)(e.Row.FindControl("lnkAct"));
                ImageButton imgbtn = (ImageButton)(e.Row.FindControl("btnActivateDe"));
                if (hdnchkblacklist.Value != "0")
                {
                    lnk1.Visible = true;
                    imgbtn.Visible = false;
                    e.Row.CssClass = "dynRowDeactivate";

                }
                else
                {
                    lnk1.Visible = false;
                    imgbtn.Visible = true;
                }
                //ImageButton imgActive = (ImageButton)(e.Row.FindControl("btnActivateDe"));
                //imgActive.ImageUrl = "~/Images/Admin/Active.png";
                //string[] btnArgument= imgActive.CommandArgument.ToString().Split(',');
                //string companyIdArgument=btnArgument[0];
                //string blacklistArgument = btnArgument[1];
                //if (blacklistArgument == "1")  //Deactivated
                //{
                //    e.Row.CssClass = "GrdviewRowDeactive";
                //    ((Literal)(e.Row.FindControl("ltlActiveDeactive"))).Text = "Blacklisted";

                //    imgActive.ImageUrl = "../Images/Admin/Active.png";
                //    imgActive.AlternateText = "ACTIVATE THIS COMPANY";
                //    imgActive.ToolTip = "ACTIVATE THIS COMPANY";
                //    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to ACTIVATE this company?);");
                //    imgActive.CommandName = "ACTIVATE";
                //}
                //else
                //{
                //    ((Literal)(e.Row.FindControl("ltlActiveDeactive"))).Text = "Active";

                //    imgActive.ImageUrl = "../Images/Admin/Deactive.png";
                //    imgActive.AlternateText = "DEACTIVATE THIS COMPANY";
                //    imgActive.ToolTip = "DEACTIVATE THIS COMPANY";
                //    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to DEACTIVATE this company?);");
                //    imgActive.CommandName = "DEACTIVATE";
                //}
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void gvBlacklist_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string[] btnArgument = e.CommandArgument.ToString().Split(',');
            int companyIdArgument = Convert.ToInt32(btnArgument[0]);
            objMApplication=new clsMasterApplication();
            int ErrorId = 0;
            if (e.CommandName == "blacklist")
            {
                string companyID = e.CommandArgument.ToString();
                DataSet ds = new DataSet();
                  objMApplication = new clsMasterApplication();
                  hdn_companyid.Value = companyID;
                  mp1.Show();
            
              ds = objMApplication.Fetch_Blacklistcompany_Dataset(companyID, null, null, null, null, null);

              if (ds != null)
              {
                  if (ds.Tables[0].Rows.Count > 0)
                  {
                      hdn_previousstatus.Value = ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString();
                  }
                  else
                  {
                      hdn_previousstatus.Value = "5";
                  }
              }
    
            }
            else if (e.CommandName == "Act")
            {
                string companyID = e.CommandArgument.ToString();
                objMApplication = new clsMasterApplication();
                int result = objMApplication.RevertBack_Blacklistcomapny(Convert.ToInt32(companyID));

            }

            FunfillGridView();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string companyname, companypan, companyreferenceno, comapnyregistrationno ;
        companyname = companypan = companyreferenceno = comapnyregistrationno = null;
        string selectedvalue = ddlSearchCriteria.SelectedValue;
        if (selectedvalue == "1")
        {
            companyname = txtSearchbox.Text.Trim();
        }
        else if (selectedvalue == "2")
        {
            companypan = txtSearchbox.Text.Trim();
        }
        else if (selectedvalue == "3")
        {
            companyreferenceno = txtSearchbox.Text.Trim();
        }
        else if (selectedvalue == "4")
        {
            comapnyregistrationno = txtSearchbox.Text.Trim();
        }

        objMApplication = new clsMasterApplication();
        DataSet ds = new DataSet();

        try
        {
            ds = objMApplication.Fetch_Blacklistcompany_Dataset(null,null,companyname,companypan,companyreferenceno,comapnyregistrationno);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvBlacklist.DataSource = ds;
                    gvBlacklist.DataBind();
                }
                else
                {
                    gvBlacklist.DataSource = null;
                    gvBlacklist.DataBind();
                }
            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            ds.Dispose();
        }

    }
    protected void gvBlacklist_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvBlacklist.PageIndex = e.NewPageIndex;
        FunfillGridView();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       objMApplication = new clsMasterApplication();
       int retval = objMApplication.UpdateBlacklistcompany(Convert.ToInt32(hdn_companyid.Value),
           txt_authority_text.Text.Trim(), txt_iomcertificate.Text.Trim(), txt_reason.Text.Trim(),
           Convert.ToInt32(hdn_previousstatus.Value));
       txt_authority_text.Text = "";
       txt_iomcertificate.Text = "";
       txt_reason.Text = "";
       FunfillGridView();
    }
}
