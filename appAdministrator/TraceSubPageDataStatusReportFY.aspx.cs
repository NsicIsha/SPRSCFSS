using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Drawing;

public partial class appAdministrator_TraceSubPageDataStatusReportFY : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string UserFullName = "";
    StringBuilder sb;

    string RefrenceNumber = "";
    string CompanyName = "";
    string CompanyStatus = "";
    int ZoneId;
    int BranchId;
    string From_date = "";
    string To_date = "";
    int Branch_Id_query;
    string CastId = "";
    int FormTypeval;

    float totalnsic = 0;
    float totalagency = 0;

    float totalnsic_ST = 0;
    float totalagency_ST = 0;


    float totalnsic_Surplus = 0;
    float totalnsic_Surplus_Adjustment = 0;
    float totalnsic_Surplus_AdjustmentTax = 0;



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
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            //   scriptManager.RegisterPostBackControl(this.btn_chek);


            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId != 1 && RoleId != 10)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                ZoneId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
            }
            txt_Ref_no.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btn_status_track');");
            txt_Ref_no.Focus();

            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["id"].ToString() != "0" && Request.QueryString["id"].ToString() != null)
                {
                    Branch_Id_query = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                else
                {
                    Branch_Id_query = 0;
                }

                if (Request.QueryString["FromDate"].ToString() != null && Request.QueryString["FromDate"].ToString() != "")
                {
                    From_date = Request.QueryString["FromDate"].ToString();
                }
                else
                {
                    From_date = "";
                }
                if (Request.QueryString["ToDate"].ToString() != null && Request.QueryString["ToDate"].ToString() != "")
                {
                    To_date = Request.QueryString["ToDate"].ToString();
                }
                else
                {
                    To_date = "";
                }
                if (Request.QueryString["CateID"].ToString() != null && Request.QueryString["CateID"].ToString() != "0")
                {
                    CastId = Request.QueryString["CateID"].ToString();
                }
                else
                {
                    CastId = "0";
                }
                if (Request.QueryString["FormType"].ToString() != null && Request.QueryString["FormType"].ToString() != "")
                {
                    FormTypeval = Convert.ToInt32(Request.QueryString["FormType"].ToString());

                    //  1 -- For Fresh Registration , 14 ------ Backlog Registration  , 3 -- Amendment -- 2 -- Renewal
                }
                else
                {
                    FormTypeval = 0;
                }


                if (FormTypeval == 1 || FormTypeval == 14 || FormTypeval == 3 || FormTypeval == 2)
                {

                    string Btn_value = Request.QueryString["Btn_value"].ToString();
                    if (Btn_value.ToString() == "1")
                    {

                        Bindlabel(From_date, To_date, Convert.ToInt32(CastId), FormTypeval, Branch_Id_query);
                    }
                    else
                    {
                        Bindlabel(From_date, To_date, Convert.ToInt32(CastId), FormTypeval, Branch_Id_query);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            ErrHandler.WriteError("" + ex.StackTrace);
        }

    }

    protected void grd_search_result_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //totalnsic,totalagency,totalnsic_ST,totalagency_ST
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //totalqty += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Quantity"));

            totalnsic += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnitAmount"));
            totalagency += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AgencyAmount"));

            totalnsic_ST += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnitST"));
            totalagency_ST += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AgencyST"));


            totalnsic_Surplus += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "SurplusAmount"));
            totalnsic_Surplus_Adjustment += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnitAmount_AdjustedWithSurplus"));
            totalnsic_Surplus_AdjustmentTax += Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "UnitSTNew"));

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotalNSICValue = (Label)e.Row.FindControl("lblTotalNSICValue");
            lblTotalNSICValue.Text = totalnsic.ToString();

            Label lblTotalNSICValue_ST = (Label)e.Row.FindControl("lblTotalNSICValue_ST");
            lblTotalNSICValue_ST.Text = totalnsic_ST.ToString();

            Label lblTotalAgency = (Label)e.Row.FindControl("lblTotalAgency");
            lblTotalAgency.Text = totalagency.ToString();

            Label lblTotalAgency_ST = (Label)e.Row.FindControl("lblTotalAgency_ST");
            lblTotalAgency_ST.Text = totalagency_ST.ToString();





            Label lblTotalNSICValueSurplus = (Label)e.Row.FindControl("lblTotalNSICValueSurplus");
            lblTotalNSICValueSurplus.Text = totalnsic_Surplus.ToString();

            Label lblTotalNSICValueSurplusAdjustment = (Label)e.Row.FindControl("lblTotalNSICValueSurplusAdjustment");
            lblTotalNSICValueSurplusAdjustment.Text = totalnsic_Surplus_Adjustment.ToString();

            Label lblTotalNSICValue_ST_AdjustmentAmount = (Label)e.Row.FindControl("lblTotalNSICValue_ST_AdjustmentAmount");
            lblTotalNSICValue_ST_AdjustmentAmount.Text = totalnsic_Surplus_AdjustmentTax.ToString();

        }
    }

    public void Bindlabel(string Formdate, string Todate, int CastId, int Formtypid, int BranchID)
    {
        try
        {
            DataSet ds = new DataSet();
            string Btn_value = Request.QueryString["Btn_value"].ToString();
            if (Btn_value == "1")
            {
                ds = FetchData(Formdate, Todate, CastId, Formtypid, BranchID);

                lblUnitCount.Text = "Total Unit Count : " + ds.Tables[0].Rows.Count;
            }
            else
            {
                ds = FetchData_1415(Formdate, Todate, CastId, Formtypid, BranchID);
            }
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptID = objDecEnc.Encrypt("1");
            string EncryptCompany_Name = "";
            string EncryptCompanyID = "";
            string EncryptBranchName = "";
            string imageid = string.Empty;
            StringBuilder sb = new StringBuilder();
            HyperLink hyper = new HyperLink();
            if (ds.Tables[0].Rows.Count > 0)
            {

                grd_search_result.DataSource = ds;

                //grd_search_result.Columns[2].FooterText = "Total Count";
                //grd_search_result.Columns[4].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("UnitAmount")).Sum().ToString();

                //grd_search_result.Columns[5].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("UnitST")).Sum().ToString();
                //grd_search_result.Columns[8].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("AgencyAmount")).Sum().ToString();
                //grd_search_result.Columns[9].FooterText = ds.Tables[0].AsEnumerable().Select(x => x.Field<Int32>("AgencyST")).Sum().ToString();
                grd_search_result.DataBind();

                /*

                sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:100%; ' >");  // main table we have closed it
                  sb.Append("<tr class='GrdviewHeader'><th>SNO</th><th >Company Name</th><th >Reference Number /<br/>Registration Number </th>");
                  sb.Append("<th > Zone Branch </th> <th>  Validity</th> <th> NSIC Fee /<br/>Service Tax</th> <th> Agency Fee /<br/>Service Tax</th> <th style='display:none;'> </th></tr>");
                  for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                  {

                      sb.Append("<tr> <td align='left' valign='top' style='width:10px;'>" + Convert.ToInt32(Convert.ToInt32(i) + 1) + "</td> <td align='left'  valign='top'> " + ds.Tables[0].Rows[i]["ComapnyAddress"].ToString() + " </td>");
                      sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["GPRefrenceNo"].ToString() + "<br/>" + ds.Tables[0].Rows[i]["RegistrationNumber"].ToString() + "</td>");
                      sb.Append("<td align='left' valign='top'>" +ds.Tables[0].Rows[i]["ZoneBranchName"].ToString() + "</td>");
                      sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");


                    sb.Append("<td align='left' valign='top'>Rs." + ds.Tables[0].Rows[i]["UnitAmount"].ToString() + "<br/>Rs." + ds.Tables[0].Rows[i]["UnitST"].ToString() + "</td>");
                    sb.Append("<td align='left' valign='top'>Rs." + ds.Tables[0].Rows[i]["AgencyAmount"].ToString() + "<br/>Rs." + ds.Tables[0].Rows[i]["AgencyST"].ToString() + "</td>");



                      EncryptCompany_Name = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["CompanyName"].ToString());
                      EncryptCompanyID = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["CompanyId"].ToString());
                      EncryptBranchName = objDecEnc.Encrypt(ds.Tables[0].Rows[i]["BranchName"].ToString());
                      hyper.NavigateUrl = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                      imageid = "ReportViewPage.aspx?id=" + EncryptID + "&Com_name=" + EncryptCompany_Name + "&Company_ID=" + EncryptCompanyID + "&BranchName=" + EncryptBranchName;
                      hyper.Text = "";

                      hyper.Attributes.Add("display", "none");
                      sb.Append("<td align='left' valign='middle' style='text-decoration:underline; color:blue;display:none;'>" + "<a href=" + imageid + " target='_blank'>" + "<b>" + hyper.Text + "</b>" + "</a>" + "</td>");
                      sb.Append("</tr>");
                  }
                  sb.Append("</table>");


    */
                ////   lbl_report_status_track.Text = sb.ToString();

            }
            else
            {
                ds.Dispose();
                lbl_report_status_track.Text = "<span style='color:red'>" + "<b>" + "No record Found!" + "</b></span>";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }


    public void Bind_Serch_Data(int CategoryId, string searchtext)
    {

        DataSet ds = new DataSet();
        try
        {
            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["id"].ToString() != "0" && Request.QueryString["id"].ToString() != null)
                {
                    Branch_Id_query = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                else
                {
                    Branch_Id_query = 0;
                }

                if (Request.QueryString["FromDate"].ToString() != null && Request.QueryString["FromDate"].ToString() != "")
                {
                    From_date = Request.QueryString["FromDate"].ToString();
                }
                else
                {
                    From_date = "";
                }
                if (Request.QueryString["ToDate"].ToString() != null && Request.QueryString["ToDate"].ToString() != "")
                {
                    To_date = Request.QueryString["ToDate"].ToString();
                }
                else
                {
                    To_date = "";
                }
                if (Request.QueryString["CateID"].ToString() != null && Request.QueryString["CateID"].ToString() != "0")
                {
                    CastId = Request.QueryString["CateID"].ToString();
                }
                else
                {
                    CastId = "0";
                }
                if (Request.QueryString["FormType"].ToString() != null && Request.QueryString["FormType"].ToString() != "")
                {
                    FormTypeval = Convert.ToInt32(Request.QueryString["FormType"].ToString());

                    //  1 -- For Fresh Registration , 14 ------ Backlog Registration  , 3 -- Amendment -- 2 -- Renewal
                }
                else
                {
                    FormTypeval = 0;
                }
                if (FormTypeval == 1 || FormTypeval == 14 || FormTypeval == 3)
                {
                    ds = FetchData(From_date, To_date, Convert.ToInt32(CastId), FormTypeval, Branch_Id_query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (CategoryId == 1)
                        {
                            DataSet ds1 = new DataSet();
                            string strQuery = "CompanyName like '%" + searchtext.ToString() + "%'";
                            //  ds.Tables[0].DefaultView.RowFilter = "CompanyName like '%"+searchtext.ToString()+"+%'";
                            DataRow[] drFilterRows = ds.Tables[0].Select(strQuery);
                            //ds1 = ds.Tables[0].DataSet.Copy();  
                        }
                    }
                    else
                    {
                        ds.Dispose();
                    }

                }
            }

        }
        catch (Exception ex)
        {
        }
    }

    public DataSet FetchData(string Formdate, string Todate, int CastId, int Formtypeid, int BranchID)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_FY", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 18000;
            if (Formdate != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar, 50).Value = Formdate;
            if (Todate != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar, 50).Value = Todate;
            if (CastId != 0) MyAdp.SelectCommand.Parameters.Add("@CastID", SqlDbType.Int).Value = CastId;
            if (Formtypeid != 0) MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = Formtypeid;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.Fill(ds);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return ds;
    }

    public DataSet FetchData_1415(string Formdate, string Todate, int CastId, int Formtypeid, int BranchID)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_registration_finacial_1415", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formdate != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar, 50).Value = Formdate;
            if (Todate != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar, 50).Value = Todate;
            if (CastId != 0) MyAdp.SelectCommand.Parameters.Add("@CastID", SqlDbType.Int).Value = CastId;
            if (Formtypeid != 0) MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = Formtypeid;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            MyAdp.Fill(ds);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();

        }
        return ds;
    }
    protected void btn_status_track_Click(object sender, EventArgs e)
    {
        try
        {
            int Cateid = 0;
            string searchtext = "";
            if (txt_Ref_no.Text != "")
            {
                searchtext = txt_Ref_no.Text.ToString();
            }
            if (ddl_list.SelectedValue != "0")
            {
                Cateid = Convert.ToInt32(ddl_list.SelectedValue.ToString());
            }
            if (txt_Ref_no.Text != "")
            {
                Bind_Serch_Data(Cateid, searchtext);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }
    }

}