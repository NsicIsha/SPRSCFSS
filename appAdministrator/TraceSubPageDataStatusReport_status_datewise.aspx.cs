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

public partial class appAdministrator_TraceSubPageDataStatusReport_status_datewise : System.Web.UI.Page
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
    string Filter = "";

    int Branch_Id_query;
    string CastId = "";
    int FormTypeval;

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

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            scriptManager.RegisterPostBackControl(this.btn_chek);

            FunRequest();

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

            if (!IsPostBack)
            {
                bindlabel_in_status();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    // Bind Label  in track view 
    public void bindlabel_in_status()
    {

        int bsCategoryAreaId = -1;
        int bsCategoryCastId = -1;
        int bsCategoryUnitSizeId = -1;
        int natureOfOperation = -1;
        int status = -1;
        int headOfficeState = -1;
        int headOfficeCity = -1;

        try
        {


            if (Request.QueryString.HasKeys())
            {

                Filter = Convert.ToString(Request.QueryString["FILTER"].ToString());
                if (Filter.Contains('|') == true)
                {
                    string[] param = Filter.Split('|');
                    bsCategoryAreaId = Convert.ToInt32(param[1]);
                    bsCategoryCastId = Convert.ToInt32(param[2]); ;
                    bsCategoryUnitSizeId = Convert.ToInt32(param[3]); ;
                    natureOfOperation = Convert.ToInt32(param[4]); ;
                    status = Convert.ToInt32(param[5]);
                    headOfficeState = Convert.ToInt32(param[9]);
                    headOfficeCity = Convert.ToInt32(param[10]);

                    // fromdate = param[7].ToString() != "" ? param[7] : null;
                    // todate = param[8].ToString() != "" ? param[8] : null;
                }



                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "0" && Request.QueryString["id"].ToString() != null)
                {
                    Branch_Id_query = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                else
                {
                    Branch_Id_query = 0;
                }
                int zoneId = -1;
                if (Request.QueryString["zoneid"] != null && Request.QueryString["zoneid"].ToString() != "0" && Request.QueryString["zoneid"].ToString() != null)
                {
                    zoneId = Convert.ToInt32(Request.QueryString["zoneid"].ToString());
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

                DataSet ds = new DataSet();
                StringBuilder sb = new StringBuilder();
                ds = FetchData(From_date, To_date, Convert.ToInt32(CastId), Convert.ToInt32(FormTypeval), Convert.ToInt32(Branch_Id_query),
                    bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    status,headOfficeState, headOfficeCity,  zoneId, -1

                    );
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:100%; ' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th>SNO</th><th >Company Name</th><th >PAN</th><th >Reference Number /<br/>Registration Number </th>");
                    sb.Append("<th > Zone Branch </th> <th>  Validity</th>  <th > Current status </th>");
                    if (FormTypeval == 3 || FormTypeval == 2)
                    {
                        sb.Append("<th>Date</th>");
                    }


                    sb.Append("</tr>");
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        sb.Append("<tr> <td align='left' valign='top' style='width:10px;'>" + Convert.ToInt32(Convert.ToInt32(i) + 1) + "</td> <td align='left'  valign='top'> " + ds.Tables[0].Rows[i]["ComapnyAddress"].ToString() + " </td>");
                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["PAN"].ToString()   + "</td>");

                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["GPRefrenceNo"].ToString() + "<br/>" + ds.Tables[0].Rows[i]["RegistrationNumber"].ToString() + "</td>");
                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["ZoneBranchName"].ToString() + "</td>");


                        

                        if (FormTypeval == 111)
                        {
                            sb.Append("<td align='left' valign='top'>" + "----" + "</td>");
                            sb.Append("<td align='left' valign='middle' style=' color:red;'>" + "<b>" + "Deleted " + "</b>" + "</td>");
                        }
                        else
                        { 
                        if (ds.Tables[0].Rows[i]["StatusID"].ToString() == "4")
                        {
                                sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                                sb.Append("<td align='left' valign='middle' style=' color:red;'>" + "<b>" + "Expired " + "</b>" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td align='left' valign='middle' style=' color:red;'>" + "<b>" + "Live " + "</b>" + "</td>");
                        }
                        }



                        if (FormTypeval == 3 || FormTypeval == 2)
                        {
                            sb.Append("<td align='left' valign='top' style=' color:red;'>" + returnformat(ds.Tables[0].Rows[i]["EDitDATE"].ToString()) + "</td>");
                        }
                        sb.Append("</tr>");
                    }
                    sb.Append("</table>");
                    lbl_report_status_track.Text = sb.ToString();
                }
                else
                {
                    ds.Dispose();
                    lbl_report_status_track.Text = "<span style='color:red'>" + "<b>" + "No record Found!" + "</b></span>";
                }
            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public string returnformat(string edditddate)
    {
        string Datere = "";
        try
        {

            DateTime dateTime;


            dateTime = new DateTime();
            dateTime = Convert.ToDateTime(edditddate);
            Datere = String.Format("{0:MMMM d, yyyy}", dateTime);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(" " + ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
        return Datere;
    }

    public DataSet FetchData(string Formdate, string Todate, int CastId, int Formtypeid, int BranchID,
        int fGPtbl_CUnit_CategoryAreaId, int fGPtbl_CUnit_CategoryCastId, int fGPtbl_CUnit_CategoryUnitSizeId,
          int enterprisedefnition, int fGPtbl_Company__StatusId, int fGPtbl_Company_HeadOffice_StateId, int fGPtbl_Company_HeadOffice_CityId, int zoneid, int forExcel)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_live_status_date_wise_new_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formdate != "") MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar, 50).Value = Formdate;
            if (Todate != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar, 50).Value = Todate;
            if (CastId != 0) MyAdp.SelectCommand.Parameters.Add("@CastID", SqlDbType.Int).Value = CastId;
            if (Formtypeid != 0) MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.Int).Value = Formtypeid;
            if (BranchID != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchID;
            //March 30 RA
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryAreaId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryAreaId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryCastId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryCastId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CategoryUnitSizeId", SqlDbType.Int).Value = fGPtbl_CUnit_CategoryUnitSizeId;
            MyAdp.SelectCommand.Parameters.Add("@enterprisedefnition", SqlDbType.Int).Value = enterprisedefnition;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Status", SqlDbType.Int).Value = fGPtbl_Company__StatusId;
            MyAdp.SelectCommand.Parameters.Add("@ParamZoneId", SqlDbType.Int).Value = zoneid;
            MyAdp.SelectCommand.Parameters.Add("@isForExecl", SqlDbType.Int).Value = forExcel;     
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_StateId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_StateId;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_HeadOffice_CityId", SqlDbType.Int).Value = fGPtbl_Company_HeadOffice_CityId;
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

    protected void btn_chek_Click(object sender, EventArgs e)
    {
        int bsCategoryAreaId = -1;
        int bsCategoryCastId = -1;
        int bsCategoryUnitSizeId = -1;
        int natureOfOperation = -1;
        int status = -1;
        int headOfficeState = -1;
        int headOfficeCity = -1;

        try
        {


            if (Request.QueryString.HasKeys())
            {

                Filter = Convert.ToString(Request.QueryString["FILTER"].ToString());
                if (Filter.Contains('|') == true)
                {
                    string[] param = Filter.Split('|');
                    bsCategoryAreaId = Convert.ToInt32(param[1]);
                    bsCategoryCastId = Convert.ToInt32(param[2]); ;
                    bsCategoryUnitSizeId = Convert.ToInt32(param[3]); ;
                    natureOfOperation = Convert.ToInt32(param[4]); ;
                    status = Convert.ToInt32(param[5]);
                    headOfficeState = Convert.ToInt32(param[9]);
                    headOfficeCity = Convert.ToInt32(param[10]);

                    // fromdate = param[7].ToString() != "" ? param[7] : null;
                    // todate = param[8].ToString() != "" ? param[8] : null;
                }



                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "0" && Request.QueryString["id"].ToString() != null)
                {
                    Branch_Id_query = Convert.ToInt32(Request.QueryString["id"].ToString());
                }
                else
                {
                    Branch_Id_query = 0;
                }
                int zoneId = -1;
                if (Request.QueryString["zoneid"] != null && Request.QueryString["zoneid"].ToString() != "0" && Request.QueryString["zoneid"].ToString() != null)
                {
                    zoneId = Convert.ToInt32(Request.QueryString["zoneid"].ToString());
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

                DataSet dsInput = new DataSet();
                StringBuilder sb = new StringBuilder();
                dsInput = FetchData(From_date, To_date, Convert.ToInt32(CastId), Convert.ToInt32(FormTypeval), Convert.ToInt32(Branch_Id_query),
                    bsCategoryAreaId, bsCategoryCastId, bsCategoryUnitSizeId, natureOfOperation,
                    status, headOfficeState,headOfficeCity, zoneId, 1

                    );
                if (dsInput.Tables[0].Rows.Count > 0)
                {
                    //dsInput.Tables[0].Columns.Remove("tblid");
                    //dsInput.Tables[0].Columns.Remove("ZoneID");
                    //dsInput.Tables[0].Columns.Remove("BranchID");
                    //dsInput.Tables[0].AcceptChanges();

                    HttpResponse response = HttpContext.Current.Response;
                    string ExcelFileName = "CompanyWiseStatusReport.xls";
                    string text = "<b><span >   Company Wise Status Report" + From_date + " " + To_date + "</span></b>" + "<br/><br/>";
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

            }


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
}
