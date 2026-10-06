using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appInspectionAgency_InspectionComplete : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            AGENCYID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["AGENCYID"]).ToString());
            BRANCHID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BRANCHNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHNAME"]).ToString();
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            AGENCYID = 0;
            BRANCHID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        urlId=Convert.ToInt32(Request.QueryString["i"].ToString());

        if(ROLEID==6)
        {
            if(!Page.IsPostBack)
            {
                GridViewdataBind();
            }
        }

    }

    private void GridViewdataBind()
    {
        try
        {
            clsMasterInspectionAgency objAgecy = new clsMasterInspectionAgency();
            if (urlId == 1)
            {
                gvIComplete.DataSource = objAgecy.Agency_Fetch_InspectionisComplete(AGENCYID);
                gvIComplete.DataBind();
            }
            else if (urlId == 3)
            {
                gvIComplete.DataSource = objAgecy.Agency_Fetch_InspectionComlete_for_Amedment(AGENCYID);
                gvIComplete.DataBind();
            }

            if (gvIComplete.Rows.Count == 0)
            {
                lblMessage.Text = "No Data Found";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void gvIComplete_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvIComplete.PageIndex = e.NewPageIndex;
        GridViewdataBind();
    }
}
