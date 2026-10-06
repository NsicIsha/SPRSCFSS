using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Globalization;
public partial class appAdministrator_EditturnoverHistory : System.Web.UI.Page
{

    clsCompany objCompany;
    int UserId, RoleId, companyId, CompanyID_PK, DepartmentId, UnitId, Status, BranchId, FormType;
    //  int successId, urlId = 0, amendmentid;
    int Unitid, urlId = 1;
    string UserFullName;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            FunRequest();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            UserFullName = "";
        }
    }

    public void FunRequest()
    {
        try
        {
            if (Request.QueryString.HasKeys())
            {
                companyId = Convert.ToInt32(Request.QueryString["Company_ID"].ToString());
                CompanyID_PK = Convert.ToInt32(Request.QueryString["CompanyIdPk"].ToString());
                //Amedmentid = Convert.ToInt32(Request.QueryString["Amedmentid"].ToString());
               // Showdata(companyId, Amedmentid);
                Bindturnover();
                Bindturnover_old();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    public void Bindturnover()
    {
        try
        {
            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(companyId);//3;
            DataSet ds_report = new DataSet();
            // objCompany.FetchTurnover_history(CompanyID2, 1);
            // ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            ds_report = objCompany.FetchTurnover_history(CompanyID2, 1);
            if (ds_report.Tables[1].Rows.Count > 0)
            {
                //if (ds_report.Tables[0].Rows.Count > 1)
                // {
                grdTurnOver.DataSource = ds_report.Tables[1];
                grdTurnOver.DataBind();
                // }
            }
            else
            {
                grdTurnOver.Dispose();
                grdTurnOver.DataBind();
                ds_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public void Bindturnover_old()
    {
        try
        {
            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(companyId);//3;
            DataSet ds_report = new DataSet();
            // objCompany.FetchTurnover_history(CompanyID2, 1);
            // ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            ds_report = objCompany.FetchTurnover_history_Turnover_CompanyeditData(CompanyID2, 1, CompanyID_PK);
            if (ds_report.Tables[1].Rows.Count > 0)
            {
                //if (ds_report.Tables[0].Rows.Count > 1)
                // {
                grdTurnOver_old.DataSource = ds_report.Tables[1];
                grdTurnOver_old.DataBind();
                // }
            }
            else
            {
                grdTurnOver_old.Dispose();
                grdTurnOver_old.DataBind();
                ds_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }
}
