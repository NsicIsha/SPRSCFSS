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


public partial class appAdministrator_TraceSubPageDataStatusReport_Renewal_Fresh : System.Web.UI.Page
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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

            //   scriptManager.RegisterPostBackControl(this.btn_chek);


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

     

    public void Bindlabel(string Formdate, string Todate, int CastId, int Formtypid, int BranchID)
    {
        try
        {
            DataSet ds = new DataSet();
            string Btn_value = Request.QueryString["Btn_value"].ToString();
            if (Btn_value == "1")
            {
                ds = FetchData(Formdate, Todate, CastId, Formtypid, BranchID);
            }
            else
            {
                ds = FetchData_1415(Formdate, Todate, CastId, Formtypid, BranchID);
            }
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptID = objDecEnc.Encrypt("1");
  
            string imageid = string.Empty;
            StringBuilder sb = new StringBuilder();
            HyperLink hyper = new HyperLink();
            if (ds.Tables[0].Rows.Count > 0)
            {

                grd_search_result.DataSource = ds;
                grd_search_result.DataBind();

                 

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
            MyAdp = new SqlDataAdapter("Gpsp_Feth_datewise_liveRegisterStatus", DBManager.ConnectToDB());
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