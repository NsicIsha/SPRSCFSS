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

                DataSet ds = new DataSet();
                StringBuilder sb = new StringBuilder();
                ds = FetchData(From_date, To_date, Convert.ToInt32( CastId),  Convert.ToInt32( FormTypeval), Convert.ToInt32( Branch_Id_query));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    sb.Append("<table border='1' cellpadding='3'cellspacing='3' style='background-color: #ffffff; margin-left:0px; width:100%; ' >");  // main table we have closed it
                    sb.Append("<tr class='GrdviewHeader'><th>SNO</th><th >Company Name</th><th >Reference Number /<br/>Registration Number </th>");
                    sb.Append("<th > Zone Branch </th> <th>  Validity</th>  <th > Current status </th>");
                    if (FormTypeval == 3 || FormTypeval == 2)
                    {
                        sb.Append("<th>Date</th>");
                    }

                 
                    sb.Append("</tr>");
                    for (int i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                    {
                        sb.Append("<tr> <td align='left' valign='top' style='width:10px;'>" + Convert.ToInt32(Convert.ToInt32(i) + 1) + "</td> <td align='left'  valign='top'> " + ds.Tables[0].Rows[i]["ComapnyAddress"].ToString() + " </td>");
                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["GPRefrenceNo"].ToString() + "<br/>" + ds.Tables[0].Rows[i]["RegistrationNumber"].ToString() + "</td>");
                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["ZoneBranchName"].ToString() + "</td>");
                        sb.Append("<td align='left' valign='top'>" + ds.Tables[0].Rows[i]["RegistrationDate"].ToString() + "</td>");
                        if (ds.Tables[0].Rows[i]["StatusID"].ToString() == "4")
                        {
                            sb.Append("<td align='left' valign='middle' style=' color:red;'>" + "<b>" + "Expired " + "</b>" + "</td>");
                        }
                        else
                        {
                            sb.Append("<td align='left' valign='middle' style=' color:red;'>" + "<b>" + "Live " + "</b>" + "</td>");
                        }
                        if (FormTypeval == 3 || FormTypeval ==2)
                        {
                            sb.Append("<td align='left' valign='top' style=' color:red;'>" + returnformat( ds.Tables[0].Rows[i]["EDitDATE"].ToString()) + "</td>");
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
     catch(Exception ex)
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

    public DataSet FetchData(string Formdate, string Todate, int CastId, int Formtypeid, int BranchID)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter MyAdp = new SqlDataAdapter();

        try
        {
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus_live_status_date_wise_new", DBManager.ConnectToDB());
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

}
