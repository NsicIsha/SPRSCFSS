using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;


public partial class appInspectionAgency_InspectionisPending : System.Web.UI.Page
{

    int USERID, ROLEID, AGENCYID, BRANCHID;
    string USERFULLNAME, BRANCHNAME;
    public int pendingAction = 0;
    int urlId;

    clsMasterInspectionAgency ObjAgency;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            AGENCYID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["AGENCYID"]).ToString());
            BRANCHID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BRANCHNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHNAME"]).ToString();
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            AGENCYID = 0;
            BRANCHID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        if (Request.QueryString["i"] != null)
        {
            urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
        }

        if (!Page.IsPostBack)
        {
            try
            {
                FunfillGridView();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
    }


    private void FunfillGridView()
    {
        try
        {
            ObjAgency = new clsMasterInspectionAgency();
            System.Data.DataTable Dt = new System.Data.DataTable();

            //4 FOR DATE ISSUED BUT INSPECTION IS PENDING

            if (urlId != 3)
            {

                Dt = ObjAgency.Agency_Fetch_InspectionisPending(urlId, AGENCYID, BRANCHID, USERID);
            }
            else
            {
                Dt = Agency_Fetch_InspectionisPending_amed(urlId, AGENCYID, BRANCHID, USERID);
            }

            if (Dt.Rows.Count != 0)
            {
                MultiView1.SetActiveView(viewData);
                gvPending.DataSource = Dt;
                gvPending.DataBind();
            }
            else
            {
                MultiView1.SetActiveView(viewMessage);
                lblMsg.Text = "No pending request units for inspection";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["pendingAction"] != null)
            {
                LabelAction.Text = Session["pendingAction"].ToString();
                pendingAction = 1;
                Session["pendingAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                pendingAction = 0;
                Session["pendingAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["pendingAction"] = null;
        }
    }


    public DataTable Agency_Fetch_InspectionisPending_amed(int id, int AgencyId, int BranchId, int InspUserId)
    {
        DataTable MyDt;
        SqlDataAdapter MyAdp;
        MyDt = new DataTable();
        MyAdp = new SqlDataAdapter();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_AgencyAmendment_Fetch_InspectionisPending_through_Amedment", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@AgencyId", SqlDbType.Int).Value = AgencyId;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
            MyAdp.SelectCommand.Parameters.Add("@InspUserId", SqlDbType.Int).Value = InspUserId;
            MyAdp.Fill(MyDt);
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
        return MyDt;
    }

    protected void gvPending_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rdbUnitId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnUnitId"))).Value.ToString());
                int rdbCompanyId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnCompanyId"))).Value.ToString());
                int rdbFormtypeId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnFormtypeId"))).Value.ToString());

                HiddenFieldFormtype.Value = rdbCompanyId.ToString();

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptUnitId = objDecEnc.Encrypt(rdbUnitId.ToString());
                string EncryptCompanyId = objDecEnc.Encrypt(rdbCompanyId.ToString());
                string EncryptFormtypeId = objDecEnc.Encrypt(rdbFormtypeId.ToString());

                ImageButton imgViewRecord = (ImageButton)(e.Row.FindControl("btnView"));
                imgViewRecord.PostBackUrl = "InspectionisPending_View.aspx?u=" + EncryptUnitId + "&c=" + EncryptCompanyId + "&f=" + EncryptFormtypeId;
                // imgViewRecord.PostBackUrl = "~/appAdministrator/WaitingCompanies_View.aspx?i=1&id=" + rdbCompanyId;
                // Response.Redirect("WaitingCompanies_View.aspx?i=1&id=" + e.CommandArgument, false);

                Button btnupload = (Button)e.Row.FindControl("btnUpload");
                btnupload.PostBackUrl = "InspectionisPending_View.aspx?u=" + EncryptUnitId + "&c=" + EncryptCompanyId + "&f=" + EncryptFormtypeId + "&id=1";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
