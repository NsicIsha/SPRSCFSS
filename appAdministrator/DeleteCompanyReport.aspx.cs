using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.IO;


public partial class appAdministrator_DeleteCompanyReport : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds, ds;
    int UserId, RoleId, DepartmentId;
    string UserFullName, password;
    int BranchId;
    string Branch_Name = "";

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

            if (RoleId == 2 || RoleId == 3 || RoleId == 5  || RoleId==1)
            {

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                    GridViewDataBind();
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

    public void GridViewDataBind()
    {
        try
        {
            DataTable dt_1 = new DataTable();

            dt_1 = dt_bind_grdid(BranchId);
            if (dt_1.Rows.Count > 0)
            {
                grd_delete_company.DataSource = dt_1;
                grd_delete_company.DataBind();
            }
            else
            {
                grd_delete_company.DataBind();
                dt_1.Dispose();
            }

        }
        catch (Exception ex)
        {

        }
    }

    public DataTable dt_bind_grdid(int Branch_Id)
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
           // MyAdp = new SqlDataAdapter("GP_sp_delete_report_deleteCompany", DBManager.ConnectToDB());

            MyAdp = new SqlDataAdapter("GP_SP_Deleted_Company_report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
          if(Branch_Id!=0)  MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch_Id;


            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

        return dt;
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        Bind_GridView();
    }
    protected void ddl_reson_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    public void Bind_GridView()
    {
        try
        {
            string fromdate = "";
            string todate = "";
            string Reson = "";
            fromdate = Convert.ToString(txt_from_date.Text.ToString());
            todate = Convert.ToString(Convert.ToDateTime( txt_todate.Text.ToString()).AddDays(1));
            Reson = Convert.ToString(ddl_reson.SelectedItem.Text.ToString().Trim().ToString());
            DataTable dt_filter = new DataTable();
            dt_filter.Columns.Add("CompanyName");
            dt_filter.Columns.Add("CompanyPan");
            dt_filter.Columns.Add("CompanyAddress");
            dt_filter.Columns.Add("ResonforDelete");
            dt_filter.Columns.Add("PendingWith");
            DataTable dt_text = new DataTable();
            dt_text = dt_bind_grdid_text();
            if (dt_text.Rows.Count > 0)
            {
                for (int i = 0; i <= dt_text.Rows.Count - 1; i++)
                {
                    if (dt_text.Rows[i]["PendingWith"].ToString() == "HO" || dt_text.Rows[i]["PendingWith"].ToString() == "ADMIN" || dt_text.Rows[i]["PendingWith"].ToString() == "BRANCH")
                    {
                        string sssss = Convert.ToDateTime(dt_text.Rows[i]["CompanyHoDate"]).ToString();

                        if (((Convert.ToDateTime(dt_text.Rows[i]["CompanyHoDate"].ToString()) >= Convert.ToDateTime(fromdate.ToString())
                             && Convert.ToDateTime(dt_text.Rows[i]["CompanyHoDate"].ToString()) <= Convert.ToDateTime(todate.ToString()))

                            || (Convert.ToDateTime(dt_text.Rows[i]["CompanyAdminDate"].ToString()) >= Convert.ToDateTime(fromdate.ToString())
                             && Convert.ToDateTime(dt_text.Rows[i]["CompanyAdminDate"].ToString()) <= Convert.ToDateTime(todate.ToString()))))
                        {
                            string ss = Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString());
                            if (ss == "Expired Hard Coded Cases")
                            {

                                Response.Write("fdf");
                            }
                            if (Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) == Convert.ToString(Reson))
                            {
                                dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
                                                        dt_text.Rows[i]["CompanyPan"].ToString(),
                                                        dt_text.Rows[i]["CompanyAddress"].ToString(),
                                                        dt_text.Rows[i]["ResonforDelete"].ToString(),
                                                        dt_text.Rows[i]["PendingWith"].ToString());
                            }
                            else if(Convert.ToString(ddl_reson.SelectedValue)=="0")
                            {
                                dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
                                                       dt_text.Rows[i]["CompanyPan"].ToString(),
                                                       dt_text.Rows[i]["CompanyAddress"].ToString(),
                                                       dt_text.Rows[i]["ResonforDelete"].ToString(),
                                                       dt_text.Rows[i]["PendingWith"].ToString());
                            }
                            else if (Convert.ToString(ddl_reson.SelectedValue) == "9")
                            {

                               
                                if (Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Inspection Failed"
                               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Closed"
                                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Pending Documents"
                               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Unit Expired"
                                 && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Typo Error"
                               && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Double Entry"
                                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Inspection Pending"
                                && Convert.ToString(dt_text.Rows[i]["ResonforDelete"].ToString()) != "Case Rejected")
                                {
                                                            dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
                                                            dt_text.Rows[i]["CompanyPan"].ToString(),
                                                            dt_text.Rows[i]["CompanyAddress"].ToString(),
                                                            dt_text.Rows[i]["ResonforDelete"].ToString(),
                                                            dt_text.Rows[i]["PendingWith"].ToString());
                                }

                            }

                            

                        }
                        else
                        {
                            /*
                            dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
                                                     dt_text.Rows[i]["CompanyPan"].ToString(),
                                                     dt_text.Rows[i]["CompanyAddress"].ToString(),
                                                     dt_text.Rows[i]["ResonforDelete"].ToString(),
                                                     dt_text.Rows[i]["PendingWith"].ToString());*/
                        }
                    }
                    else
                    {
                        /*
                        dt_filter.Rows.Add(dt_text.Rows[i]["CompanyName"].ToString(),
                                                       dt_text.Rows[i]["CompanyPan"].ToString(),
                                                       dt_text.Rows[i]["CompanyAddress"].ToString(),
                                                       dt_text.Rows[i]["ResonforDelete"].ToString(),
                                                       dt_text.Rows[i]["PendingWith"].ToString());*/
                    }
                
                }
                grd_delete_company.DataSource = dt_filter;
                grd_delete_company.DataBind();

            }
            else
            {
                dt_text.Dispose();
                grd_delete_company.DataBind();
            }


        }
        catch (Exception ex)
        {
        }
    }

    public DataTable dt_bind_grdid_text()
    {
        DataTable dt = new DataTable();
        try
        {
            SqlDataAdapter MyAdp;
            // MyAdp = new SqlDataAdapter("GP_sp_delete_report_deleteCompany", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GP_SP_Deleted_Company_report_selecttext", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //if (Branch_Id != 0) MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch_Id;
            MyAdp.Fill(dt);
        }
        catch (Exception ex)
        {
        }
        return dt;
    }
}
