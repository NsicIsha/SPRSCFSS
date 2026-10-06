using System;
using System.Data;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_BranchWiseSearchReport : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;

    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            USERID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            ROLEID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            USERID = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";sfdf
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();

            //USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
        if (!IsPostBack)
        {
            BindGrid();
        }

        if (ROLEID == 3 || ROLEID == 8 || ROLEID == 4)
        {
            btnBackZonewiseSearch.Visible = false;
        }

    }

    public void BindGrid()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            ds = objclsCompany.Bind_zonewise_data(USERID, ROLEID);
            dt = new DataTable();
            dt = ds.Tables[0];
            if (dt.Rows.Count > 0)
            {
                if (Request.QueryString.HasKeys())
                {
                    if (Request.QueryString["bid"].ToString() != null || Request.QueryString["bid"].ToString() != "undefined")
                    {
                        string BranchId = Request.QueryString["bid"].ToString();
                        DataView dtcopytable = new DataView(dt);
                        dtcopytable.RowFilter = "BranchID = '" + BranchId + "'";

                        grd_branchwise.DataSource = dtcopytable;
                        grd_branchwise.DataBind();
                    }
                }
                else
                {
                    grd_branchwise.DataSource = dt;
                    grd_branchwise.DataBind();
                }
            }
            else
            {
                ds.Dispose();
                dt.Dispose();
                grd_branchwise.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void grd_branchwise_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // grd_branchwise.Rows[0].Visible = false;

        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_underprocessunit = (Label)e.Row.FindControl("lbl_Underprocess_Unit");
                Label lbl_Branch_Id = (Label)e.Row.FindControl("lbl_Branch_Id");
                string imageid = string.Empty;
                imageid = "Companystatustrack_By_User.aspx?id=" + Convert.ToInt32(lbl_Branch_Id.Text.ToString());
                lbl_underprocessunit.Text = "<a href='" + imageid + "' target='_blank' title='CompanyStatustrack'>" + lbl_underprocessunit.Text + "</a>";

                Label lbl_live_unit = (Label)e.Row.FindControl("lbl_live_unit");
                Label lbl_expired_unit = (Label)e.Row.FindControl("lbl_Expire_Unit");
                string imageid_live = string.Empty;
                imageid_live = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_Branch_Id.Text.ToString()) + "&Status=" + "1";
                string imageid_expired = string.Empty;
                imageid_expired = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_Branch_Id.Text.ToString()) + "&Status=" + "4";
                lbl_live_unit.Text = "<a href='" + imageid_live + "' target='_blank' title='CompanyStatustrack'>" + lbl_live_unit.Text + "</a>";
                lbl_expired_unit.Text = "<a href='" + imageid_expired + "' target='_blank' title='CompanyStatustrack'>" + lbl_expired_unit.Text + "</a>";
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + " " + ex.StackTrace);
        }
    }

    protected void btnBackZonewiseSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("ZoneBranchwiseReport.aspx", false);
    }
}
