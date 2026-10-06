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
using System.Globalization;

public partial class appAdministrator_MonthlyPerformanceSheet : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    int BranchId, DepartmentId;

    clsCompany objclsCompany;
    DataTable dt;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
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

        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }

        if (!IsPostBack)
        {
            //BindGridview();           
        }
    }
    public DataSet PerformanceSheetDateWise(string Formadte, string todate)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_fetch_datewise_PerformanceSheetDateWise", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (Formadte.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@Startdate", SqlDbType.VarChar).Value = Formadte;
            if (todate.ToString() != "") MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar).Value = todate;
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

            if (DateTime.Parse(txt_fromdate.Text, CultureInfo.InvariantCulture) < DateTime.Parse("04/01/2023", CultureInfo.InvariantCulture))
            {
                ScriptManager.RegisterStartupScript(this.Update_bton, typeof(string), "Alert", "alert('From Date Can Not be Before 1st April 2023 .');", true);
                return;
            }
            //if (Convert.ToDateTime(txt_fromdate.Text) > Convert.ToDateTime("04/01/2023".ToString()))
            //{
            //    ScriptManager.RegisterStartupScript(this.Update_bton, typeof(string), "Alert", "alert('From Date Can Not be Before 1st April 2023 .');", true);
            //    return;
            //}
            if (txt_fromdate.Text != "")
            {
                fromdate = txt_fromdate.Text.ToString();
            }
            else
            {
                fromdate = "04/01/2024".ToString();
            }
            if (txt_todate.Text != "")
            {
                todate = txt_todate.Text.ToString();
            }
            else
            {
                todate = "03/31/2025".ToString();
            }

            ds = PerformanceSheetDateWise(fromdate, todate);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd_report.DataSource = ds.Tables[0];
                grd_report.DataBind();
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
}