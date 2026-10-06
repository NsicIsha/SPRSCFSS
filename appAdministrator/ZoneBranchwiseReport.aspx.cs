using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_ZoneBranchwiseReport : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;
    int UnitTotal = 0, LiveTotal=0, ExpiredTotal=0, UnderprocessTotal=0, DepartmentId;

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            USERID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            ROLEID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            USERID = 0;
            ROLEID = 0;
            DepartmentId = 0;
            USERFULLNAME = "";
            //UserEmailID = "";
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
            
            if (ROLEID == 1 || ROLEID == 5 || ROLEID==10)
            {
                mvZoneListing.SetActiveView(viewZoneListing);
                ZonelistingGridBind();
            }
            else if (ROLEID == 2 || ROLEID == 3 )
            {
                BindGridData();
                mvZoneListing.SetActiveView(viewBranchListing);
                btnBackView.Visible = false;
            }
        }
    }

    private void ZonelistingGridBind()
    {
        try
        {
            objclsCompany = new clsCompany();
            dt = new DataTable();
            dt = objclsCompany.Fetch_LiveExpiredUnit_Zonewise();
            if (dt.Rows.Count > 0)
            {
                grdZonelisting.DataSource = dt;
                grdZonelisting.DataBind();
            }
            else
            {
                dt.Dispose();
                grdZonelisting.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    public void BindGridData(int zoneid)
    {
        try
        {
            objclsCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objclsCompany.Bind_zonewise_data(USERID, ROLEID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                DataView dtcopytable = new DataView(dt);
                dtcopytable.RowFilter = "ZoneId = '" + zoneid + "'";

                grd_zonewisedata.DataSource = dtcopytable;
                grd_zonewisedata.DataBind();
            }
            else
            {
                dt.Dispose();
                grd_zonewisedata.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    public void BindGridData()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objclsCompany.Bind_zonewise_data(USERID, ROLEID).Tables[0];
            if (dt.Rows.Count > 0)
            {
                grd_zonewisedata.DataSource = dt;
                grd_zonewisedata.DataBind();
            }
            else
            {
                dt.Dispose();
                grd_zonewisedata.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void grd_Zonewise_onRowDataBound(object sender, GridViewRowEventArgs e)
    {
       

        for (int rowindex = grd_zonewisedata.Rows.Count - 2; rowindex >= 0; rowindex--)
        {
            GridViewRow gvrow = grd_zonewisedata.Rows[rowindex];
            GridViewRow gvrowpre = grd_zonewisedata.Rows[rowindex + 1];
        
            int cellcount = 0;
                if (gvrow.Cells[cellcount].Text == gvrowpre.Cells[cellcount].Text)
                {
                    if (gvrowpre.Cells[cellcount].RowSpan < 2)
                    {
                        gvrow.Cells[cellcount].RowSpan = 2;
                    }
                    else
                    {
                        gvrow.Cells[cellcount].RowSpan = gvrowpre.Cells[cellcount].RowSpan + 1;
                    }
                    gvrowpre.Cells[cellcount].Visible = false;
                }
         
        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // add the UnitPrice and QuantityTotal to the running total variables
            LiveTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "LiveUNit"));
            ExpiredTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "ExpiredUnit"));
            UnderprocessTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnderProcessUnit"));
            UnitTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalCount"));

            Label lbl_Branch_Name = (Label)e.Row.FindControl("lbl_Branch_Name");
            Label lbl_Branch_id = (Label)e.Row.FindControl("lbl_Branch_Id");
            string url = "BranchWiseSearchReport.aspx?bid=" + Convert.ToInt32(lbl_Branch_id.Text.ToString());
            lbl_Branch_Name.Text = "<a href='" + url + "'>" + lbl_Branch_Name.Text + "</a>";
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {            
            e.Row.Cells[0].Text = "Totals:";
            // for the Footer, display the running totals
            e.Row.Cells[2].Text = LiveTotal.ToString();
            e.Row.Cells[3].Text = ExpiredTotal.ToString();
            e.Row.Cells[4].Text = UnderprocessTotal.ToString();
            e.Row.Cells[5].Text = UnitTotal.ToString();
            e.Row.Font.Bold = true;
        }

    }
  
    protected void grd_Zonewise_Data_Bound(object sender, EventArgs e)
    {
        /*if (grd_zonewisedata.Rows.Count > 0)
        {
            try
            {
                Label lbl_total_Unit = (Label)grd_zonewisedata.FooterRow.FindControl("lbl_total");
                lbl_total_Unit.Text = "<b>"+ string.Format("{0:n2}", grd_zonewisedata.Rows.Cast<GridViewRow>()
                                       .Sum(a => Convert.ToInt32(((Label)a.FindControl("lbl_total_unit")).Text))).Replace(".00", "").ToString() +"</b>"; //
                Label lbl_total_expire_unit = (Label)grd_zonewisedata.FooterRow.FindControl("lbl_Expire_Unit_Footer");
                lbl_total_expire_unit.Text = "<b>"+ string.Format("{0:n2}", grd_zonewisedata.Rows.Cast<GridViewRow>()
                                          .Sum(a => Convert.ToInt32(((Label)a.FindControl("lbl_Expire_Unit")).Text))).Replace(".00","").ToString() +"</b>";
                Label lbl_total_live_unit = (Label)grd_zonewisedata.FooterRow.FindControl("lbl_live_unit_footer");
                lbl_total_live_unit.Text = "<b>"+ string.Format("{0:n2}", grd_zonewisedata.Rows.Cast<GridViewRow>()
                                            .Sum(a => Convert.ToInt32(((Label)a.FindControl("lbl_live_unit")).Text))).Replace(".00","").ToString() +"</b>";
                //Label lbl_total_Name = (Label)grd_zonewisedata.FooterRow.FindControl("lbl_Total");
               // lbl_total_Name.Text = "<b>" + "Total" + "</b>";


             }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }*/

    }

    protected void grdZonelisting_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // add the UnitPrice and QuantityTotal to the running total variables
                LiveTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Livecount"));
                ExpiredTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Expiredcount"));
                UnderprocessTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Underprocesscount"));
                UnitTotal += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalCount"));
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[0].Text = "Totals:";
                // for the Footer, display the running totals
                e.Row.Cells[1].Text = LiveTotal.ToString();
                e.Row.Cells[2].Text = ExpiredTotal.ToString();
                e.Row.Cells[3].Text = UnderprocessTotal.ToString();
                e.Row.Cells[4].Text = UnitTotal.ToString();
                e.Row.Font.Bold = true;
            }
        }
        catch(Exception ex)
        {
            ErrHandler.WriteError(ex.Message+"  " +ex.StackTrace);
        }
    }

    protected void grdZonelisting_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "zone")
        {
            BindGridData(Convert.ToInt32(e.CommandArgument));
            mvZoneListing.SetActiveView(viewBranchListing);
        }
    }

    protected void btnBackView_Click(object sender, EventArgs e)
    {
        mvZoneListing.SetActiveView(viewZoneListing);
        ZonelistingGridBind();
    }


}
