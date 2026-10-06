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
using System.IO;
using System.Text;
using System.Diagnostics;
using NSICGP_VBClassLibrary;
using System.Drawing;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Threading;

public partial class appAdministrator_ProRataReport : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    string TotalCount = "";

    int totalplus = 0;
    int totalfreshregistraton = 0;

    int totalProvfreshregistraton = 0;

    int totalbacklogregistrion = 0;
    int totalamedment = 0;
    int totalrenewal = 0;
    string oldValue = string.Empty;
    string newValue = string.Empty;
    int BranchId, DepartmentId;
    string lastZoneId = null;
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

        


        //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        try
        {
            FunRequest();
            //USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (ROLEID != 1 && ROLEID != 10)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            }
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }

        if (!IsPostBack)
        {
            Btn_value.Value = "1";

            BindGridview();

        }

    }


    public DataSet Fetch_zone_branch_wise_Datewise_report(string Formadte, string todate, int CastID)
    {
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
                                 // MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("GPsp_Fetch_Datewise_DataStatusLiveReport_With_RDate_PID_ProRataTarget", DBManager.ConnectToDB());

            //  MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified", DBManager.ConnectToDB());

            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
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

    protected void btn_search_Click(object sender, EventArgs e)
    {

        Btn_value.Value = "1";
        BindGridview();

    }

    public void BindGridview()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            string fromdate = ""; string todate = "";
            int CastID = 0;
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();

            }
            else
            {
                fromdate = "04/01/2025".ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();
            }
            else
            {
                todate = "09/12/2025".ToString();
            }


            //ds = objclsCompany.Fetch_zone_wise_excel_modified(fromdate, todate, ROLEID);

            if (ROLEID == 1 || ROLEID == 5)
            {
                ds = Fetch_zone_branch_wise_Datewise_report(fromdate, todate, 1);// 1 Cast id  objclsCompany.Fetch_zone_branch_wise_Datewise_report(fromdate, todate, CastID);
            }
            else
            {
                ds = Fetch_zone_branch_wise_Datewise_report_BranchWise(BranchId, fromdate, todate, 1);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_report.DataSource = ds.Tables[0];
                grd_report.DataBind();
                /*
                  lbl_labelcount.Text = TotalCount.ToString();
                lbl_total_new_company.Text = TotalCount.ToString();
                 */
                // GenerateUniqueData();
            }
            else
            {
                ds.Dispose();
                grd_report.Dispose();
                grd_report.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }


    Dictionary<string, List<GridViewRow>> zoneRowGroups = new Dictionary<string, List<GridViewRow>>();
    string previousZoneId = null;

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        // Achievement_Zone is the LAST column (adjust if needed)
        int zoneColumnIndex = grd_report.Columns.Count - 1;

        foreach (var zoneGroup in zoneRowGroups)
        {
            List<GridViewRow> rows = zoneGroup.Value;

            if (rows.Count > 0)
            {
                // First row where we'll show the Achievement_Zone value
                GridViewRow firstRow = rows[0];

                // Set RowSpan on the first row
                firstRow.Cells[zoneColumnIndex].RowSpan = rows.Count;

                // Center align
                firstRow.Cells[zoneColumnIndex].HorizontalAlign = HorizontalAlign.Center;
                firstRow.Cells[zoneColumnIndex].VerticalAlign = VerticalAlign.Middle;

                // Hide the same cell in the remaining rows
                for (int i = 1; i < rows.Count; i++)
                {
                    rows[i].Cells[zoneColumnIndex].Visible = false;
                }
            }
        }

        // Clear for next binding
        zoneRowGroups.Clear();
    }




    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        // Merge both Zone (0) and ZoneAchievement (6) columns based on Zone
        MergeZoneCells(grd_report, 6,6);
        base.Render(writer);
    }

    private void MergeZoneCells(GridView grid, int zoneColIndex, int zoneAchColIndex)
    {
        for (int rowIndex = grid.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow currentRow = grid.Rows[rowIndex];
            GridViewRow nextRow = grid.Rows[rowIndex + 1];

            string currentZone = currentRow.Cells[zoneColIndex].Text.Trim();
            string nextZone = nextRow.Cells[zoneColIndex].Text.Trim();

            if (currentZone == nextZone)
            {
                // Merge Zone column
                if (nextRow.Cells[zoneColIndex].RowSpan < 2)
                    currentRow.Cells[zoneColIndex].RowSpan = 2;
                else
                    currentRow.Cells[zoneColIndex].RowSpan = nextRow.Cells[zoneColIndex].RowSpan + 1;

                nextRow.Cells[zoneColIndex].Visible = false;

                // Merge Zone Achievement column
                if (nextRow.Cells[zoneAchColIndex].RowSpan < 2)
                    currentRow.Cells[zoneAchColIndex].RowSpan = 2;
                else
                    currentRow.Cells[zoneAchColIndex].RowSpan = nextRow.Cells[zoneAchColIndex].RowSpan + 1;

                nextRow.Cells[zoneAchColIndex].Visible = false;
            }

            
        }
    }


    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //// Get ZoneID from hidden field
            //HiddenField hdnZoneId = (HiddenField)e.Row.FindControl("hdnZoneId");
            //string zoneId = hdnZoneId.Value;

            //if (!string.IsNullOrEmpty(zoneId))
            //{
            //    if (!zoneRowGroups.ContainsKey(zoneId))
            //        zoneRowGroups[zoneId] = new List<GridViewRow>();

            //    zoneRowGroups[zoneId].Add(e.Row);
            //}



            // -------
            HiddenField hdn_branch_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
            Label lblZonename = (Label)e.Row.FindControl("lbl_Zonename");
            Label lblBranch = (Label)e.Row.FindControl("lbl_Branch_name");

            Label lblAchievementZone = (Label)e.Row.FindControl("lbl_AchievementZone");

            HiddenField currentZoneId = (HiddenField)e.Row.FindControl("hdn_Zone_id");

            if (lastZoneId == currentZoneId.Value)
            {
                // Same zone as previous — hide value
                lblAchievementZone.Text = "";
            }
            else
            {
                // New zone — show value
                lastZoneId = currentZoneId.Value;
                // Keep text as-is
            }


        }
    }

    protected void grd_report_Databound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            //string fromdate = ""; string todate = "";
            //int CastID = 0;
            //int Branch_id_text;
            //if (txt_fromdate.Text != "")
            //{
            //    fromdate = txt_fromdate.Text.ToString();
            //}
            //else
            //{
            //    fromdate = "04/01/2025".ToString();
            //}
            //if (txt_todate.Text != "")
            //{
            //    todate = txt_todate.Text.ToString();
            //}
            //else
            //{
            //    todate = "09/12/2025".ToString();
            //}

           // CastID = 0;


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //// -------
                //HiddenField hdn_branch_id = (HiddenField)e.Row.FindControl("hdn_branch_id");
                //Label lblZonename = (Label)e.Row.FindControl("lbl_Zonename");
                //Label lblBranch = (Label)e.Row.FindControl("lbl_Branch_name");

                //Label lblAchievementZone = (Label)e.Row.FindControl("lbl_AchievementZone");

                //HiddenField currentZoneId = (HiddenField)e.Row.FindControl("hdn_Zone_id");

                //if (lastZoneId == currentZoneId.Value)
                //{
                //    // Same zone as previous — hide value
                //    lblAchievementZone.Text = "";
                //}
                //else
                //{
                //    // New zone — show value
                //    lastZoneId = currentZoneId.Value;
                //    // Keep text as-is
                //}

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }


    public DataSet Fetch_zone_branch_wise_Datewise_report_BranchWise(int BranchID, string Formadte, string todate, int CastID)
    {
        DataSet MyDs = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        try
        {
            MyDs = new DataSet();//GPsp_fetch_datewise_LiveUnit_renewal_amedment
                                 ////MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_LiveUnit_renewal_amedment_modified_BranchWise", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Datewise_DataStatusLiveReport_With_RDate_PID_BranchWise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            if (CastID != 0) MyAdp.SelectCommand.Parameters.Add("@CastId", SqlDbType.Int).Value = CastID;
            MyAdp.SelectCommand.CommandTimeout = 60000;
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


    protected void grd_report_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    

     
}