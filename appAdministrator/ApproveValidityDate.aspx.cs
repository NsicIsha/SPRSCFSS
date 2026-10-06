using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;

public partial class appAdministrator_ApproveValidityDate : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;


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
             lblRegistration.Text = "Edit Validity start date :: Expired companies";
            FunRequest();

          
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //string roleidstr =RoleId.ToString();
        //if (!roleidstr.Contains("1") || roleidstr.Contains("8"))
        //{
        //    Response.Redirect("Home.aspx");
        //}

        //cHANGE bY vINAY RESON ACCESS THIS PAGE TO BRANCH USER ALSO 21FEB 2014
        if (RoleId == 1 )
        {
            Bind_GridView();
        }
        else
        {
            Response.Redirect("Home.aspx");
        }
    }
    //protected void btnSearch_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
           
    //    }
    //    catch (Exception EX)
    //    {
    //        ErrHandler.WriteError(EX.Message);
    //    }
    //}

    private void Bind_GridView()
    {
        try
        {
            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objCompany.Company_Fetch_Approve_validitydate();
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    gv_company.DataSource = dt;
                    gv_company.DataBind();
                }
                else
                {
                    gv_company.DataSource = null;
                    gv_company.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "APPROVE")
        {
            objCompany = new clsCompany();
            int outid = objCompany.Approve_validity_request(Convert.ToInt32(e.CommandArgument.ToString()));
            if (outid == 1)
            {
                Bind_GridView();
            }
        }
        else if (e.CommandName.ToString().ToUpper() == "VIEW")
        {
           // LinkButton b = (LinkButton)sender;
            //GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            //LinkButton b = (LinkButton)e.("lnk_view");

            LinkButton lbtnAction;
     GridViewRow row;
     row = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
 //  lbtnAction = gv_company.Rows[row.RowIndex].FindControl("lnk_view");
            LinkButton b = (LinkButton)row.FindControl("lnk_view");
     string arguments = b.CommandArgument.ToString();
     string[] args = arguments.Split(';');
     
     b.PostBackUrl = "Login.aspx";
  // funopen(Url);

   //  b.Attributes.Add("onclick", "return FunOpenPopup950x600('" + Url + "');");
ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('Login.aspx');", true);


 //  Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenWindow", "window.open('" + Url + "','_newtab');", true);
        }
    }
    protected void gv_company_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        gv_company.PageIndex = e.NewSelectedIndex;
        Bind_GridView();
    }
    protected void gv_company_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void funopen(string URL)
    {
        string imageid = URL; // "RegistrationCertificatePageView.aspx?id=" + companyid;
       ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = 0;var Mtop = 0;window.open( '" + imageid + "', null, 'status=yes,toolbar=yes,scrollbars=yes,menubar=no,resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
       

    }
    protected void gv_company_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton b = (LinkButton) e.Row.FindControl("lnk_view");
            string arguments = b.CommandArgument.ToString();
            string[] args = arguments.Split(';');
            string Company_Name, Unit_ID, Compny_Id, BranchName, Url;
            Unit_ID = "1";
            Compny_Id = args[0].ToString();
            Company_Name = args[1].ToString();
            BranchName = args[2].ToString();
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany_Name = objDecEnc.Encrypt(Company_Name.ToString());
            string EncryptID = objDecEnc.Encrypt(Unit_ID.ToString());
            string EncryptCompanyID = objDecEnc.Encrypt(Compny_Id.ToString());
            string EncryptBranchName = objDecEnc.Encrypt(BranchName.ToString());

            Url = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name +

       "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;

            b.Attributes.Add("onclick", "return FunOpenPopup950x600('" + Url + "');");
        }
    }
}
