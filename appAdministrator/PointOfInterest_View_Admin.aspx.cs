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
using NSICGP_VBClassLibrary;
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;

using System.Data.SqlClient;

public partial class appAdministrator_PointOfInterest_View_Admin : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;
    int UserId, RoleId, DepartmentId, Branchid;
    string dateFrom = "";
    string dateto = "";
    int FormType = 0;
    string UserFullName;
    int PrimaryId_POI;
    string fromdate;
    string todate;
    int status;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            ////UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            //clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            //Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            if (Request.QueryString["PrimaryId_POI"] != "")
            {
                PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
            }

            //if (Request.QueryString["FromDate"] != "")
            //{
            //    fromdate = Request.QueryString["FromDate"].ToString();
            //}
            //if (Request.QueryString["ToDate"] != "")
            //{
            //    todate = Request.QueryString["ToDate"].ToString();
            //}
            if (Request.QueryString["Status"] != "")
            {
                status = Convert.ToInt32(Request.QueryString["Status"]);
            }


            DataSet dsss = new DataSet();

            dsss = POI_Details(PrimaryId_POI);

            // dsss = POI_Details(Branchid, Convert.ToInt32(ddlFormType.SelectedValue), txt_from_date.Text, txt_todate.Text);

            if (dsss.Tables[0].Rows.Count > 0)
            {
                txtUAM.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_UAM"].ToString();
                txtPan.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_PAN"].ToString();
                txtCompanyName.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_Name"].ToString();
                txtNatureofOperation.Text = dsss.Tables[0].Rows[0]["NatureOperation"].ToString();
                txtEM2.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_EM2"].ToString();
                txtAuthorizedName.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_PersonName"].ToString();

                txtConstituion.Text = dsss.Tables[0].Rows[0]["FirmConstitution"].ToString();
                txtSocialCategory.Text = dsss.Tables[0].Rows[0]["SocialCategory"].ToString();
                txtGST.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_GST"].ToString();
                txtEmail.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_Email"].ToString();

                txtMobile.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_Mobile"].ToString();
                txtState.Text = dsss.Tables[0].Rows[0]["fGPtbl_MS_Name"].ToString();

                txtBranch.Text = dsss.Tables[0].Rows[0]["fGPtbl_MB_Name"].ToString();
                txtInspectionAgency.Text = dsss.Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString();
                txtProduct.Text = dsss.Tables[0].Rows[0]["fGPtbl_POI_ProductDetails"].ToString();
            }
            else
            {
                txtUAM.Text = "";
                txtPan.Text = "";
                txtCompanyName.Text = "";
                txtNatureofOperation.Text = "";
                txtEM2.Text = "";
                txtAuthorizedName.Text = "";

                txtConstituion.Text = "";
                txtSocialCategory.Text = "";
                txtGST.Text = "";
                txtEmail.Text = "";

                txtMobile.Text = "";
                txtState.Text = "";

                txtBranch.Text = "";
                txtInspectionAgency.Text = "";
                txtProduct.Text = "";
            }


        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }
    public DataSet POI_Details(  int ID)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gp_sp_Fetch_POI_Details_By_ID_Admin", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
           // MyAdp.SelectCommand.Parameters.Add("@branchid", SqlDbType.Int).Value = Branch;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_POI_Id", SqlDbType.VarChar).Value = ID;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }

        return MyDs;
    }
    protected void btnEnroll_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["PrimaryId_POI"] != "")
        {
            PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
        }

        //if (Request.QueryString["FromDate"] != "")
        //{
        //    fromdate = Request.QueryString["FromDate"].ToString();
        //}
        //if (Request.QueryString["ToDate"] != "")
        //{
        //    todate =Request.QueryString["ToDate"].ToString();
        //}
        if (Request.QueryString["Status"] != "")
        {
            status = Convert.ToInt32(Request.QueryString["Status"]);
        }
        int a = Company_Update_Status_Step_POI(PrimaryId_POI, txtRemarks.Text, 3);

        ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data has been updated successfully.');", true);

        Response.Redirect("PointOfInterest_Admin.aspx?PrimaryId_POI=" + PrimaryId_POI + "&FromDate=" + fromdate + "&ToDate=" + todate + "&Status=" + status);
    }
    protected void Converted_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["PrimaryId_POI"] != "")
        {
            PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
        }

        //if (Request.QueryString["FromDate"] != "")
        //{
        //    fromdate = Request.QueryString["FromDate"].ToString();
        //}
        //if (Request.QueryString["ToDate"] != "")
        //{
        //    todate = Request.QueryString["ToDate"].ToString();
        //}
        if (Request.QueryString["Status"] != "")
        {
            status = Convert.ToInt32(Request.QueryString["Status"]);
        }

        int a = Company_Update_Status_Step_POI(PrimaryId_POI, txtRemarks.Text, 2);

        ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Data has been updated successfully.');", true);

        //Response.Redirect("PointOfInterest_Branch.aspx?PrimaryId_POI=" + PrimaryId_POI + "&FromDate=" + fromdate + "&ToDate=" + todate + "&Status=" + status);

        Response.Redirect("PointOfInterest_Admin.aspx?PrimaryId_POI=" + PrimaryId_POI + "&Status=" + status);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["PrimaryId_POI"] != "")
        {
            PrimaryId_POI = Convert.ToInt32(Request.QueryString["PrimaryId_POI"]);
        }

        ////if (Request.QueryString["FromDate"] != "")
        ////{
        ////    fromdate = Request.QueryString["FromDate"].ToString();
        ////}
        ////if (Request.QueryString["ToDate"] != "")
        ////{
        ////    todate = Request.QueryString["ToDate"].ToString();
        ////}
        if (Request.QueryString["Status"] != "")
        {
            status = Convert.ToInt32(Request.QueryString["Status"]);
        }


        //  Response.Redirect("PointOfInterest_Branch.aspx?PrimaryId_POI=" + PrimaryId_POI + "&FromDate=" + fromdate + "&ToDate=" + todate + "&Status=" + status);

        Response.Redirect("PointOfInterest_Admin.aspx?PrimaryId_POI=" + PrimaryId_POI + "&Status=" + status);
    }
    public int Company_Update_Status_Step_POI(int PrimaryId, string Remarks, int Status)
    {
        int a = 0;
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Update_Factory_Status_POI", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_POI_Id", SqlDbType.Int).Value = PrimaryId;
            MySqlComm.Parameters.Add("@Remarks", SqlDbType.VarChar, 15).Value = Remarks;
            MySqlComm.Parameters.Add("@fGPtbl_POI_Status", SqlDbType.Int).Value = Status;


            //SqlParameter fGPtbl_POI_Id = new SqlParameter("@Id", SqlDbType.Int);
            //fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(fGPtbl_POI_Id);

            a = MySqlComm.ExecuteNonQuery();
            //Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }

        return a;
    }
}