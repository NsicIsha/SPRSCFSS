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

public partial class appAdministrator_BranchwiseliveCompany : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany;
    DataTable dt;


    string TotalCount = "";

    int totalplus = 0;
    int totalfreshregistraton = 0;
    int totalbacklogregistrion = 0;
    int totalamedment = 0;
    int totalrenewal = 0;
    string oldValue = string.Empty;
    string newValue = string.Empty;
    int BranchId;
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds, ds;
    int UserId, RoleId, DepartmentId;
    string UserFullName, password;
 
    string Branch_Name = "";

    string dateFrom = "";
    string dateto;
    int Formtype = 0;
    int Category;
    int UnitType;
    int AadhaarStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (UserId != 1)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());
                Branch_Name = objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString();
            }
            else
            {
                Branch_Name = "text";
            }
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 2 || RoleId == 3 || RoleId == 5 || RoleId == 1)
            {

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                   // GridViewDataBind();
                    if (RoleId == 3)
                    {

                    }
                    else
                    {

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

        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bind_GridView();
    }

    public void Bind_GridView()
    {
        //try
        //{
        string fromdate = "";
        string todate = "";
        string Reson = "";
        if (txt_fromdate.Text.ToString() != "") fromdate = Convert.ToString(txt_todate.Text.ToString());
        if (txt_todate.Text.ToString() != "") todate = Convert.ToString(Convert.ToDateTime(txt_todate.Text.ToString()).AddDays(1));

        //dateFrom = txt_from_date.Text.ToString();
        //dateto = txt_todate.Text.ToString();


        Formtype = Convert.ToInt32(ddl_reson.SelectedValue.ToString().Trim());
        DataTable dt_text = new DataTable();
        dt_text = dt_bind_grdid_text();
        if (dt_text.Rows.Count > 0)
        {

            //DataTable dt_filter = new DataTable();
            //dt_filter.Columns.Add("CompanyName");
            //dt_filter.Columns.Add("CompanyPan");
            //dt_filter.Columns.Add("ResonforDelete");
            //dt_filter.Columns.Add("BranchName");
            //dt_filter.Columns.Add("EntryDate");
            //dt_filter.Columns.Add("Deletiondate");
            //dt_filter.Columns.Add("NSICFee");
            //dt_filter.Columns.Add("AgencyFee");
            //dt_filter.Columns.Add("TotalUnitFee");
            //dt_filter.Columns.Add("PendingWith");

            //DataTable dt_text = new DataTable();
            //dt_text = dt_bind_grdid_text();
            //if (dt_text.Rows.Count > 0)
            //{
            //    for (int i = 0; i <= dt_text.Rows.Count - 1; i++)
            //    {
            //        if (dt_text.Rows[i]["PendingWith"].ToString() == "HO" || dt_text.Rows[i]["PendingWith"].ToString() == "ADMIN")
            //        {
            //            //if (((Convert.ToDateTime(dt_text.Rows[i]["CompanyHoDate"].ToString()) >= Convert.ToDateTime(fromdate.ToString())
            //            //     && Convert.ToDateTime(dt_text.Rows[i]["CompanyHoDate"].ToString()) <= Convert.ToDateTime(todate.ToString()))

            //            //    || (Convert.ToDateTime(dt_text.Rows[i]["CompanyAdminDate"].ToString()) >= Convert.ToDateTime(fromdate.ToString())
            //            //     && Convert.ToDateTime(dt_text.Rows[i]["CompanyAdminDate"].ToString()) <= Convert.ToDateTime(todate.ToString()))))
            //            //{
            //            if (Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) == Convert.ToString(Reson))
            //            {
            //                dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
            //                dt_text.Rows[i]["CompanyPan"].ToString(),
            //                dt_text.Rows[i]["ResonforDelete"].ToString(),
            //                dt_text.Rows[i]["BranchName"].ToString(),
            //                 dt_text.Rows[i]["EntryDate"].ToString(),
            //                 dt_text.Rows[i]["Deletiondate"].ToString(),
            //                  dt_text.Rows[i]["NSICFee"].ToString(),
            //                   dt_text.Rows[i]["AgencyFee"].ToString(),
            //                    dt_text.Rows[i]["TotalUnitFee"].ToString(),
            //                    dt_text.Rows[i]["PendingWith"].ToString());

            //            }
            //            else if (Convert.ToString(ddl_reson.SelectedValue) == "0")
            //            {
            //                dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
            //                dt_text.Rows[i]["CompanyPan"].ToString(),
            //                dt_text.Rows[i]["ResonforDelete"].ToString(),
            //                dt_text.Rows[i]["BranchName"].ToString(),
            //                dt_text.Rows[i]["EntryDate"].ToString(),
            //                dt_text.Rows[i]["Deletiondate"].ToString(),
            //                dt_text.Rows[i]["NSICFee"].ToString(),
            //                dt_text.Rows[i]["AgencyFee"].ToString(),
            //               dt_text.Rows[i]["TotalUnitFee"].ToString(),
            //                    dt_text.Rows[i]["PendingWith"].ToString());
            //            }
            //            else if (Convert.ToString(ddl_reson.SelectedValue) == "9")
            //            {
            //                if (Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Inspection Failed"
            //               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Closed"
            //                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Pending Documents"
            //               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Unit Expired"
            //                 && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Typo Error"
            //               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Double Entry"
            //                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Inspection Pending"
            //                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Case Rejected")
            //                {
            //                    dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
            //                    dt_text.Rows[i]["CompanyPan"].ToString(),
            //                    dt_text.Rows[i]["ResonforDelete"].ToString(),
            //                    dt_text.Rows[i]["BranchName"].ToString(),
            //                    dt_text.Rows[i]["EntryDate"].ToString(),
            //                    dt_text.Rows[i]["Deletiondate"].ToString(),
            //                    dt_text.Rows[i]["NSICFee"].ToString(),
            //                    dt_text.Rows[i]["AgencyFee"].ToString(),
            //                   dt_text.Rows[i]["TotalUnitFee"].ToString(),
            //                    dt_text.Rows[i]["PendingWith"].ToString());
            //                }

            //            }



            //            //} ////
            //            else
            //            {
            //                /*
            //                dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
            //                                         dt_text.Rows[i]["CompanyPan"].ToString(),
            //                                         dt_text.Rows[i]["CompanyAddress"].ToString(),
            //                                         dt_text.Rows[i]["ResonforDelete"].ToString(),
            //                                         dt_text.Rows[i]["PendingWith"].ToString());*/
            //            }
            //        }
            //        else
            //        {
            //            /*
            //            dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
            //                                           dt_text.Rows[i]["CompanyPan"].ToString(),
            //                                           dt_text.Rows[i]["CompanyAddress"].ToString(),
            //                                           dt_text.Rows[i]["ResonforDelete"].ToString(),
            //                                           dt_text.Rows[i]["PendingWith"].ToString());*/
            //        }

            //    }

            int numberOfRecordsSearch = dt_text.Rows.Count;
            lblCountMsg.Text = numberOfRecordsSearch.ToString();
            grd_report.DataSource = dt_text;
            grd_report.DataBind();

        }
        else
        {
            lblCountMsg.Text = "".ToString();
            dt_text.Dispose();
            grd_report.DataBind();
        }


        //}
        //catch (Exception ex)
        //{
        //}
    }


    public DataTable dt_bind_grdid_text()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
            // MyAdp = new SqlDataAdapter("GP_sp_delete_report_deleteCompany", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.CommandTimeout = 60000;
            dateFrom = txt_fromdate.Text.ToString();
            dateto = txt_todate.Text.ToString();
            if (ddl_reson.SelectedValue == "0")
            {
                Formtype = 0;
            }
            else
            {
                Formtype = Convert.ToInt32(ddl_reson.SelectedValue.ToString().Trim());
                //ReasonforDelete = Convert.ToString(ddl_reson.SelectedItem.Text.ToString().Trim().ToString());
            }

            //MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = BranchId;
            MyAdp.SelectCommand.Parameters.Add("@startdate", SqlDbType.VarChar, 50).Value = dateFrom;
            MyAdp.SelectCommand.Parameters.Add("@enddate", SqlDbType.VarChar, 50).Value = dateto;
            MyAdp.SelectCommand.Parameters.Add("@Formtype", SqlDbType.VarChar, 50).Value = Formtype;


            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
        }
        return dt;
    }
}