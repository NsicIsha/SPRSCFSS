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

public partial class appAdministrator_ZoneBranchWiseDetailReport : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;

    int TotalApplied = 0;
    int totalRegisterd = 0;
    int totlincomplete = 0;
    int totalwaiting = 0;
    int totalinprocess = 0;
    int totalinspectionpassed = 0;
    int totalunderprocess = 0;
    int applied = 0;
    string TotalCount = "";
    int BranchId;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        scriptManager.RegisterPostBackControl(this.btn_chek);
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
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

            if (ROLEID == 8)
            {
                btn_chek.Visible = false;
                lbl_labelcount.Visible = false;
                td_total_company.Visible = false;
                lbl_total_new_company.Visible = false;
                span_total_com.Visible = false;
                BindGridview();
            }
            else
            {
                btn_chek.Visible = true;
                lbl_labelcount.Visible = true;
                lbl_total_new_company.Visible = true;
                countcompany();

                BindGridview();
            }

        }
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindGridview();
    }

    public void BindGridview()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            string fromdate = ""; string todate = "";
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();
                lbl_fromcdate.Text = "From Date" + fromdate.ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();
                lbl_tcdate.Text = "To date" + todate.ToString();
            }
            //ds = objclsCompany.Fetch_zone_wise_excel_modified(fromdate, todate, ROLEID);
            if (ROLEID == 8)
            {
                ds = Fetch_zone_branch_wise_excel_report_branchwise(fromdate, todate, ROLEID, 2, BranchId);
              
            }
            else
            {
                ds = objclsCompany.Fetch_zone_branch_wise_excel_report(fromdate, todate, ROLEID, 2);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_excel_type.DataSource = ds.Tables[0];
                grd_excel_type.DataBind();
                /*
                  lbl_labelcount.Text = TotalCount.ToString();
                lbl_total_new_company.Text = TotalCount.ToString();
                 */
                // GenerateUniqueData();
            }
            else
            {
                ds.Dispose();
                grd_excel_type.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }
    protected void grd_exceltype_rowdatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lbl_underprocess = (Label)e.Row.FindControl("lbl_underproces");
                Label lbl_branch_id = (Label)e.Row.FindControl("lbl_branch_id");
                Label lbl_registerd_unit = (Label)e.Row.FindControl("lbl_registerd");
                Label lbl_expired_unit = (Label)e.Row.FindControl("lbl_expired");
                Label lblregistrationplusexpired = (Label)e.Row.FindControl("lbl_reg_expired");
                if (lbl_registerd_unit.Text != "" && lbl_expired_unit.Text != "")
                {
                    lblregistrationplusexpired.Text = Convert.ToString(Convert.ToInt32(HttpUtility.HtmlDecode(lbl_registerd_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))) + Convert.ToInt32(HttpUtility.HtmlDecode(lbl_expired_unit.Text.ToString().Replace("<b>", "").Replace("</b>", ""))));

                    string url = "Companystatustrack_By_User.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString());
                    lbl_underprocess.Text = "<a href='" + url + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_underprocess.Text + "</a>";
                }

                Label lbl_applied = (Label)e.Row.FindControl("lbl_applied");
                Label lbl_incomplete = (Label)e.Row.FindControl("lbl_Incomplete");
                Label lbl_waiting = (Label)e.Row.FindControl("lbl_waiting");
                Label lbl_inspection_passed = (Label)e.Row.FindControl("lbl_inspection_passed");
                Label lbl_inprocess = (Label)e.Row.FindControl("lbl_inprocess");
                if (lbl_applied.Text != "" && lbl_applied.Text != "0")
                {
                    string url_applied = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "1,2,3,5,7";
                    lbl_applied.Text = "<a href='" + url_applied + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_applied.Text + "</a>";
                }
                else
                {
                    lbl_applied.Text = "0";
                }
                if (lbl_incomplete.Text != "" && lbl_incomplete.Text != "0")
                {
                    string url_incomplete = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "5";
                    //lbl_incomplete.Text = "<a href='" + url_incomplete + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_incomplete.Text + "</a>";

                   // lbl_incomplete.Text = "<a href='" + url_incomplete + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_incomplete.Text + "";
                    lbl_incomplete.Text = "0";
                }
                else
                {
                    lbl_incomplete.Text = "0";
                }
                if (lbl_waiting.Text != "" &&  lbl_waiting.Text != "0")
                {
                    string url_waiting = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "3";
                    lbl_waiting.Text = "<a href='" + url_waiting + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_waiting.Text + "</a>";
                }
                else
                {
                    lbl_waiting.Text = "0";
                }
                if (lbl_inprocess.Text != "" && lbl_inprocess.Text != "0")
                {
                    string url_inprocess = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "2";
                    lbl_inprocess.Text = "<a href='" + url_inprocess + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inprocess.Text + "</a>";
                }

                else
                {
                    lbl_inprocess.Text = "0";
                }
                if (lbl_inspection_passed.Text != "" && lbl_inspection_passed.Text != "0")
                {
                    string url_inspection_passed = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "7";
                    lbl_inspection_passed.Text = "<a href='" + url_inspection_passed + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_inspection_passed.Text + "</a>";
                }
                else
                {
                    lbl_inspection_passed.Text = "0";
                }
                if (lbl_registerd_unit.Text != "" && lbl_registerd_unit.Text != "0")
                {
                    string url_register_unit = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "1";
                    lbl_registerd_unit.Text = "<a href='" + url_register_unit + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_registerd_unit.Text + "</a>";
                }
                else
                {
                    lbl_registerd_unit.Text = "0";
                }

                if (lbl_expired_unit.Text != "" && lbl_expired_unit.Text != "0")
                {
                    string urlexpiredunit = "traceliveexpiredunitcompanywise.aspx?id=" + Convert.ToInt32(lbl_branch_id.Text.ToString()) + "&Status=" + "4";
                    lbl_expired_unit.Text = "<a href='" + urlexpiredunit + "' target='_blank' title='CompanyStatustrack' style='text-decoration:none;'>" + lbl_expired_unit.Text + "</a>";
                }
                else
                {
                    lbl_expired_unit.Text = "0";
                }



                /* Label zonename = (Label)e.Row.FindControl("lbl_zonename");
                 Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");
                 string zonenmae1 = zonename.Text;
                 if (zonename.Text != "" && zonename.Text!=null)
                 {

                     Label totalapplied = (Label)e.Row.FindControl("lbl_applied");
                     applied = Convert.ToInt32(totalapplied.Text);
                     Label totalRegisterd = (Label)e.Row.FindControl("lbl_registerd");
                     Label totalincomplete = (Label)e.Row.FindControl("lbl_Incomplete");
                     Label totalwaiting = (Label)e.Row.FindControl("lbl_waiting");
                     Label totaltotalinprocess = (Label)e.Row.FindControl("lbl_inprocess");
                     Label totalinspectionpased = (Label)e.Row.FindControl("lbl_inspection_passed");
                    
               }
                 TotalApplied += applied;*/
            }

            /* if (e.Row.RowType == DataControlRowType.Footer)
             {
                 Label lbl_Appliedtotal = (Label)e.Row.FindControl("lbl_applied_total");
                 Label lbl_Registerdtotal = (Label)e.Row.FindControl("lbl_registerd_total");
                 Label lbl_registerdtotal = (Label)e.Row.FindControl("lbl_registerd_total");
                 Label lbl_waitingtotal = (Label)e.Row.FindControl("lbl_waiting_total");
                 Label lbl_incompletetotal = (Label)e.Row.FindControl("lbl_incomplete_total");
                 Label lbl_inprocesstoral = (Label)e.Row.FindControl("lbl_inprocess_total");
                 Label lbl_inspectionpassedtotal = (Label)e.Row.FindControl("lbl_inspection_passed_total");
                 Label lbl_underprocesstotal = (Label)e.Row.FindControl("lbl_underprocess_total");
                 lbl_Appliedtotal.Text = Convert.ToString(TotalApplied);
                 //Label lblbrancname = (Label)e.Row.FindControl("lbl_branchname");
             }*/


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }



    public override void VerifyRenderingInServerForm(Control control)
    {
        return;
        /* Verifies that the control is rendered */
    }



    public DataSet Fetch_zone_branch_wise_excel_report_branchwise(string Formadte, string todate, int RoleId, int show, int BranchID)
    {

        SqlDataAdapter MyAdp = new SqlDataAdapter();
        DataSet MyDs = new DataSet();
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Sp_Gbtbl_fetch_dashBoard_reportDatewise_forexcel_Branch_wise_modified", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.DateTime).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.DateTime).Value = todate;
            MyAdp.SelectCommand.Parameters.Add("@RoleID", SqlDbType.Int).Value = RoleId;
            MyAdp.SelectCommand.Parameters.Add("@GridViewexcel", SqlDbType.Int).Value = show;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;

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


    // Make Excel Report













    public void MakeExcelSheet()
    {
        try
        {
            objclsCompany = new clsCompany();
            DataSet ds = new DataSet();
            DataTable dtemp = new DataTable();
            DataTable dttest = new DataTable();
            string fromdate = ""; string todate = "";
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();
                lbl_fromcdate.Text = "From Date" + fromdate.ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();
                lbl_tcdate.Text = "To date" + todate.ToString();
            }
            ds = objclsCompany.Fetch_zone_branch_wise_excel_report(fromdate, todate, ROLEID, 1);  // 1 for excelsheet
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.RemoveAt(6);
                
                ds.Tables[0].AcceptChanges();

                string fromdatetodate = string.Empty;
                fromdatetodate = "<b><span >   Dashboard Report" + fromdate + " " + todate + "</span></b>" + "<br/><br/>";

                ExportDataSetToExcel(ds, "Export.xls", fromdatetodate);


            }
            else
            {
                ds.Dispose();
            }



        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }


    public static void ExportDataSetToExcel(DataSet dsInput, string ExcelFileName, string text)
    {
        try
        {
            HttpResponse response = HttpContext.Current.Response;

            // first let's clean up the response.object 
            response.Clear();
            response.Charset = "";

            // set the response mime type for excel 
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", "attachment;filename=\"" + ExcelFileName + "\"");
            response.Cache.SetCacheability(HttpCacheability.NoCache);



            GridView dg1 = new GridView();
            dg1.DataSource = dsInput.Tables[0];
            dg1.DataBind();


            // create a string writer 
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    // instantiate a datagrid

                    htw.Write("<table><tr><td colspan='9' align='center'>" + text + "</td></tr></table>");
                    htw.Write("<table><tr><td colspan='9'></td></tr></table>");
                    dg1.RenderControl(htw);
                    response.Write(HttpUtility.HtmlDecode(sw.ToString()));
                    response.End();
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }


    protected void btn_chek_Click(object sender, EventArgs e)
    {
        try
        {
            MakeExcelSheet();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString());
        }

    }

    public void countcompany()
    {
        SqlCommand sqlcom = new SqlCommand();
        try
        {
            sqlcom.Connection = DBManager.ConnectToDB();
            sqlcom.CommandText = "select count(fGPtbl_Company_Id) from dbo.GPtbl_Company where fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0 and fGPtbl_Company_Status!=5";
            string comcount = Convert.ToString(sqlcom.ExecuteScalar());
            TotalCount = comcount;
            lbl_labelcount.Text = TotalCount.ToString();

            lbl_total_new_company.Text = TotalCount.ToString();
            sqlcom.Connection.Close();
        }
        catch { }
        finally
        {
            sqlcom.Dispose();
            DBManager.CloseConnection();
        }
    }
}
