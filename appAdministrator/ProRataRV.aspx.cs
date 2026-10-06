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

using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class appAdministrator_ProRataRV : System.Web.UI.Page
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

        //if (!IsPostBack)
        //{
             

        //    BindGridview();

        //}

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


        rptVw80.ProcessingMode = ProcessingMode.Local;
        rptVw80.LocalReport.ReportPath = Server.MapPath("~/appAdministrator/Report2.rdlc");
        DataSet ds = new DataSet();

        

        ds = BindGridview();
        // rptVw80.Visible = false;
        if (ds.Tables[0].Rows.Count > 0)
        {
            rptVw80.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.rptVw80.LocalReport.DataSources.Clear();
            rptVw80.LocalReport.DataSources.Add(datasource);
            // rptVw80.LocalReport.DisplayName = "hello";
            rptVw80.LocalReport.Refresh();

        }

    }

    public DataSet BindGridview()
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

        return ds;
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


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        rptVw80.ProcessingMode = ProcessingMode.Local;
        rptVw80.LocalReport.ReportPath = Server.MapPath("~/appAdministrator/Report2.rdlc");
        DataSet ds = new DataSet();
        ds = BindGridview();
        // rptVw80.Visible = false;
        if (ds.Tables[0].Rows.Count > 0)
        {
            rptVw80.Visible = true;
            ReportDataSource datasource = new ReportDataSource("DataSet1", ds.Tables[0]);
            this.rptVw80.LocalReport.DataSources.Clear();
            
            rptVw80.LocalReport.DataSources.Add(datasource);
            // rptVw80.LocalReport.DisplayName = "hello";
            rptVw80.LocalReport.Refresh();

        }

    }
}