using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Text;
using System.Data.SqlClient;
public partial class appAdministrator_PendencyReportCompany : System.Web.UI.Page
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
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId != 1 && RoleId != 10)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                ZoneId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
            }

            if (Request.QueryString.HasKeys())
            {
                if (Request.QueryString["BranchID"].ToString() != "0" && Request.QueryString["BranchID"].ToString() != null)
                {
                    Branch_Id_query = Convert.ToInt32(Request.QueryString["BranchID"].ToString());
                    int fromdate = 0;
                    int todate = 0;
                    int type = 0;
                    int status_id = 0;
                    fromdate = Convert.ToInt32(Request.QueryString["datefrom"].ToString());
                    todate = Convert.ToInt32(Request.QueryString["dateto"].ToString());
                    type = Convert.ToInt32(Request.QueryString["type"].ToString());
                    status_id=Convert.ToInt32(Request.QueryString["Statusid"].ToString());

                    bind_gridview(Branch_Id_query, fromdate, todate, type, status_id);
                }
                else
                {
                    Branch_Id_query = 0;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }


    }

    public void bind_gridview(int Branch_Id_query, int fromdate, int todate, int type, int status_id
  )
    {
        DataTable dt1 = new DataTable();
    
        dt1 = dt_result(Branch_Id_query, fromdate, todate, type,status_id);
        if (dt1.Rows.Count > 0)
        {
            grd_company_wise.DataSource = dt1;
            grd_company_wise.DataBind();
        }
        else
        {
            grd_company_wise.DataBind();
            dt1.Dispose();
        }
    }

    public DataTable dt_result( int BranchId, int datefrom , int dateto, int type, int status_id)
    {
        DataTable dt;
        SqlDataAdapter MyADp;
        dt = new DataTable();
        try
        {

            if (type== 1)
           {

               if (status_id != 0)
               {
                   MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetCompany", DBManager.ConnectToDB());

                   MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                   MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                   MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                   MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                   MyADp.SelectCommand.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
                   MyADp.Fill(dt);
               }
               else
               {
                   MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetCompany_All ", DBManager.ConnectToDB());

                   MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                   MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                   MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                   MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                   
                   MyADp.Fill(dt);
               }

           }
            else if (type == 2)
            {
                if (status_id != 0)
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetAmedmentCompany", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                    MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                    MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                    MyADp.SelectCommand.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
                    MyADp.Fill(dt);
                }
                else
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetAmedmentCompany_All", DBManager.ConnectToDB());
                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                    MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                    MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                   // MyADp.SelectCommand.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
                    MyADp.Fill(dt);
                }
            }
            else if (type == 3)
            {

                if (status_id != 0)
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetRenewalCompany", DBManager.ConnectToDB());

                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                    MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                    MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                    MyADp.SelectCommand.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
                    MyADp.Fill(dt);
                }
                else
                {
                    MyADp = new SqlDataAdapter("GP_Sp_Fetch_Company_PendingReport_GetRenewalCompany_All ", DBManager.ConnectToDB());

                    MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    MyADp.SelectCommand.Parameters.Add("@branchID", SqlDbType.Int).Value = BranchId;
                    MyADp.SelectCommand.Parameters.Add("@datefrom", SqlDbType.Int).Value = datefrom;
                    MyADp.SelectCommand.Parameters.Add("@dateto", SqlDbType.Int).Value = dateto;
                   //s MyADp.SelectCommand.Parameters.Add("@status_id", SqlDbType.Int).Value = status_id;
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
}
