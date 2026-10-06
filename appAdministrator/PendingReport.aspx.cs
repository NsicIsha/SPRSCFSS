using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;

public partial class appAdministrator_PendingReport : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    int BranchID = 0;
    int ZoneID = 0;
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (ROLEID != 1 && ROLEID != 10)
            {
                BranchID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                ZoneID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
            }
            else
            {
                if (!IsPostBack)
                {
                    bindGridView();
                }
            }
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }
    protected void ddl_pending_report_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddl_pending_report.SelectedValue == "0")
        //{
        //    grd_pending_report.Visible = false;

        //}
        if (ddl_pending_report.SelectedValue == "1")
        {
            lbl_text_msg.Text = "";
            grd_pending_report.Visible = true;
            ddl_Inspection.Items.Clear();
            //ddl_Inspection.DataValueField = "7";
            //ddl_Inspection.DataTextField = "Inspection Passed";
             ddl_Inspection.Items.Insert(0, new ListItem("All","0"));
            ddl_Inspection.Items.Insert(1, new ListItem("Document awaited", "3"));
            ddl_Inspection.Items.Insert(2, new ListItem("Inspection Pending", "2"));
            ddl_Inspection.Items.Insert(3, new ListItem("Inspection Passed", "7"));
           
            ddl_Inspection.DataBind();
            bindGridView();
        }
        else if (ddl_pending_report.SelectedValue == "2")
        {
            lbl_text_msg.Text = "";
            grd_pending_report.Visible = true;

             ddl_Inspection.Items.Clear();
            //ddl_Inspection.DataValueField = "7";
            //ddl_Inspection.DataTextField = "Inspection Passed";
           ddl_Inspection.Items.Insert(0, new ListItem("All", "0"));
             ddl_Inspection.Items.Insert(1, new ListItem("Document awaited","3" ));
             ddl_Inspection.Items.Insert(2, new ListItem("Inspection Pending", "2"));
             ddl_Inspection.Items.Insert(3, new ListItem( "Inspection Passed", "7"));
             
             ddl_Inspection.DataBind();
            bindGridView();

        }
        else if (ddl_pending_report.SelectedValue == "3")
        {
            lbl_text_msg.Text = "";
            grd_pending_report.Visible = true;
            ddl_Inspection.Items.Clear();
            //ddl_Inspection.DataValueField = "7";
            //ddl_Inspection.DataTextField = "Inspection Passed";
             ddl_Inspection.Items.Insert(0, new ListItem("All","0"));
            ddl_Inspection.Items.Insert(1, new ListItem( "Inspection Passed", "7"));

           
            ddl_Inspection.DataBind();
            bindGridView();

        }
    }

    public void bindGridView()
    {
        try
        {

            DataTable dt_grid = new DataTable();
            dt_grid = dt_result();
            if (dt_grid != null && dt_grid.Rows.Count > 0)
            {
                grd_pending_report.DataSource = dt_grid;
                grd_pending_report.DataBind();
            }
            else
            {
                dt_grid.Dispose();
                grd_pending_report.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }

    public DataTable dt_result()
    {
        DataTable dt;
        SqlDataAdapter MyADp;
        dt = new DataTable();
        try
        {
            int statusId = Convert.ToInt32(ddl_Inspection.SelectedValue);
            if (ddl_pending_report.SelectedValue == "1")
            {
                if (statusId == 0)
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_Sub", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                   //MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }

                else
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_All", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }

            }
            else if (ddl_pending_report.SelectedValue == "2")
            {
                if (statusId != 0)
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_Amedment", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }
                else
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_Amedment_All", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                   // MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }

            }
            else if (ddl_pending_report.SelectedValue == "3")
            {
                if (statusId != 0)
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_Renewal", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }
                else
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_Renewal_All", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //MyADp.SelectCommand.Parameters.Add("@statustid", SqlDbType.Int).Value = statusId;
                    MyADp.Fill(dt);
                }
            }
           
         
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();

        }
        return dt;

    }

    protected void grd_pending_report_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbl_zone = (Label)e.Row.FindControl("lbl_zonename");
                Label lbl_branch_name = (Label)e.Row.FindControl("lbl_branchname");
                Label lbl_branch_id = (Label)e.Row.FindControl("lbl_branch_id");
                Label lbl30 = (Label)e.Row.FindControl("lbl_less30");
                Label lbl60 = (Label)e.Row.FindControl("lbl_less60");
                Label lbl90 = (Label)e.Row.FindControl("lbl_less90");
                Label lbl180 = (Label)e.Row.FindControl("lbl_less180");
                Label lbl365 = (Label)e.Row.FindControl("lbl_less365");
                string url = "";
                if (Convert.ToInt32(lbl30.Text == ""? "0" : lbl30.Text) == 0)
                {
                }
                else
                {
                    if (lbl_branch_name.Text != "")
                    {
                        url = url = "PendencyReportCompany.aspx?BranchID=" + Convert.ToInt32(lbl_branch_id.Text == "" ? "0" : lbl_branch_id.Text) + "&datefrom="+0+"&dateto="+30+"&type="+ddl_pending_report.SelectedValue +"&Statusid="+ddl_Inspection.SelectedValue;
                        lbl30.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl30.Text + "</a>";
                    }
                }
                if (Convert.ToInt32(lbl60.Text == "" ? "0" : lbl60.Text) == 0)
                {

                }
                else
                {
                    if (lbl_branch_name.Text != "")
                    {
                        url = url = "PendencyReportCompany.aspx?BranchID=" + Convert.ToInt32(lbl_branch_id.Text == "" ? "0" : lbl_branch_id.Text) + "&datefrom=" + 31 + "&dateto=" + 90 + "&type=" + ddl_pending_report.SelectedValue + "&Statusid=" + ddl_Inspection.SelectedValue;
                        lbl60.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl60.Text + "</a>";
                    }
                }
                if (Convert.ToInt32(lbl90.Text == ""? "0" : lbl90.Text) == 0)
                {
                }
                else
                {
                    if (lbl_branch_name.Text != "")
                    {
                        url = url = "PendencyReportCompany.aspx?BranchID=" + Convert.ToInt32(lbl_branch_id.Text == "" ? "0" : lbl_branch_id.Text) + "&datefrom=" + 91 + "&dateto=" + 180 + "&type=" + ddl_pending_report.SelectedValue + "&Statusid=" + ddl_Inspection.SelectedValue;
                        lbl90.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl90.Text + "</a>";
                    }
                }
                if (Convert.ToInt32(lbl180.Text ==""? "0" : lbl180.Text) == 0)
                {
                }
                else
                {
                    if (lbl_branch_name.Text != "")
                    {
                        url = url = "PendencyReportCompany.aspx?BranchID=" + Convert.ToInt32(lbl_branch_id.Text == "" ? "0" : lbl_branch_id.Text) + "&datefrom=" + 181 + "&dateto=" + 365 + "&type=" + ddl_pending_report.SelectedValue + "&Statusid=" + ddl_Inspection.SelectedValue;
                        lbl180.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl180.Text + "</a>";
                    }
                }

                if (Convert.ToInt32(lbl365.Text == ""? "0" : lbl365.Text) == 0)
                {
                }
                else
                {
                    if (lbl_branch_name.Text != "")
                    {
                        url = url = "PendencyReportCompany.aspx?BranchID=" + Convert.ToInt32(lbl_branch_id.Text == "" ? "0" : lbl_branch_id.Text) + "&datefrom=" + 366 + "&dateto=" + 4460 + "&type=" + ddl_pending_report.SelectedValue + "&Statusid=" + ddl_Inspection.SelectedValue;
                        lbl365.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl365.Text + "</a>";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }


    protected void ddl_Inspection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGridView();
    }
}
