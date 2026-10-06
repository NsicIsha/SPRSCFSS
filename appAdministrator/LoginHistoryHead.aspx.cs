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
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Net;
public partial class appAdministrator_LoginHistoryHead : System.Web.UI.Page
{
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    clsCompany ObjCompany;
    string UserFullName;
    string Privioustext = string.Empty;
    string Lastext = string.Empty;
    string Searchtext = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            if (!IsPostBack)
            {
                Bind_Gridview();
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            UserFullName = "";
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }

    }

    public void Bind_Gridview()
    {
        try
        {
            ObjCompany = new clsCompany();
            Myds = new DataSet();
            if (txt_serach.Text == "")
            {
                Searchtext = "";
            }
            else
            {
                Searchtext = txt_serach.Text.ToString();
            }
            Myds = ObjCompany.Fetch_Login_History( Searchtext);
            if (Myds.Tables[0].Rows.Count > 0)
            {
                grd_history.DataSource = Myds.Tables[0];
                grd_history.DataBind();
            }
            else
            {
                Myds.Dispose();
                grd_history.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }
    protected void grd_history_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_history.PageIndex = e.NewPageIndex;
        Bind_Gridview();
    }
    protected void grd_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
   
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblusername = (Label)e.Row.FindControl("lbl_Login_Name");
               
                Privioustext = lblusername.Text.ToString();
                if (Privioustext == Lastext)
                {
                    lblusername.Visible = false;
                }
                Lastext = Privioustext;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        Bind_Gridview();
    }
    protected void btn_cancle_Click(object sender, EventArgs e)
    {
        txt_serach.Text = "";
        Bind_Gridview();
    }
}
