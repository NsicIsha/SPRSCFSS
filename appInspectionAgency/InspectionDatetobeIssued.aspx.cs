using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appInspectionAgency_InspectionDatetobeIssued : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID;
    string USERFULLNAME, BRANCHNAME;
    int successId, amendmentid = 0, urlId;
    public int issueAction = 0;
    string compname, compid, branchname;
    

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
                // 2 FOR DATE TO BE ISSUED
                Dt = ObjAgency.Agency_Fetch_DatetobeIssued(urlId, AGENCYID, BRANCHID, USERID);
                if (Dt.Rows.Count != 0)
                {
                    MultiView1.SetActiveView(viewData);
                    GridView1.DataSource = Dt;
                    GridView1.DataBind();
                }
                else
                {
                    MultiView1.SetActiveView(viewMessage);
                    lblMsg.Text = "No pending request for inspection date !";
                    Dt.Dispose();
                }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["issueAction"] != null)
            {
                LabelAction.Text = Session["issueAction"].ToString();
                issueAction = 1;
                Session["issueAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                issueAction = 0;
                Session["issueAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["issueAction"] = null;
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            clsAmendment objAmendment = new clsAmendment();
            if (e.CommandName == "ISSUEDATE")
            {
                txtDate.Text = "";
                lblMesgPnl.Text = "Issue INSPECTION DATE";
                ModalPopupExtender2.Show();
                amendmentid = objAmendment.CompanyAmendment_WaitingCompany_FetchAmendmentId(Convert.ToInt32(e.CommandArgument.ToString()));
                HiddenFieldUnit.Value = e.CommandArgument.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int rdbUnitId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnUnitId"))).Value.ToString());
                int rdbCompanyId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnCompanyId"))).Value.ToString());
                int rdbFormtypeId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnFormtypeId"))).Value.ToString());

                HiddenFieldFormtype.Value = rdbFormtypeId.ToString();

                //DecryptEncrypt objDecEnc = new DecryptEncrypt();
                //string EncryptUnitId = objDecEnc.Encrypt(rdbUnitId.ToString());
                //string EncryptCompanyId = objDecEnc.Encrypt(rdbCompanyId.ToString());
                //string EncryptFormtypeId = objDecEnc.Encrypt(rdbFormtypeId.ToString());

                //ImageButton imgViewRecord = (ImageButton)(e.Row.FindControl("btnView"));
                //imgViewRecord.PostBackUrl = "InspectionDatetobeIssued_View.aspx?u=" + EncryptUnitId + "&c=" + EncryptCompanyId + "&f=" + EncryptFormtypeId;
                DecryptEncrypt objDecr = new DecryptEncrypt();

                HiddenField hdn = (HiddenField)e.Row.FindControl("hdncid");
                //HiddenField hdnbrn = (HiddenField)e.Row.FindControl("hdnbranch");
                compid = objDecr.Encrypt(hdn.Value.ToString());
                compname = objDecr.Encrypt(e.Row.Cells[2].Text.ToString());
                //branchname = objDecr.Encrypt(hdnbrn.Value.ToString());
                //string EncryptCompanyid = objDecr.Encrypt(CompanyId.ToString());
                //string EncryptCompanyname = objDecr.Encrypt(UnitId.ToString());

                if (rdbFormtypeId == 1)
                {
                    HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");
                    hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=";// +branchname;
                }
                else if (rdbFormtypeId == 3)
                {
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string CompanyID = Convert.ToString(hdn.Value);
                    string EncryptCompany = objDecEnc.Encrypt(CompanyID.ToString());
                    string EncryptUnit = objDecEnc.Encrypt(rdbUnitId.ToString());
                    string EncryptStatus = objDecEnc.Encrypt("0");
                    string EncryptBranchId = objDecEnc.Encrypt("0");
                    string EncryptUrlid = objDecEnc.Encrypt("3");
                    HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");
                    hyplnk.NavigateUrl = "~/appAdministrator/UnitAmendment_ViewChanges.aspx?&c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlid;

                }         
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        try
        {
            ObjAgency = new clsMasterInspectionAgency();
            successId = ObjAgency.Agency_Update_InspectionStatus(amendmentid, Convert.ToInt32(HiddenFieldUnit.Value),
                AGENCYID, Convert.ToInt32(HiddenFieldFormtype.Value), 4, txtDate.Text, USERID);

            if (successId == 1)
            {
                Session["issueAction"] = "Date alredy exist!";
            }
            else if (successId == 2)
            {
                Session["issueAction"] = "Date issued successfully!";
            }
            else
            {
                Session["issueAction"] = "Server is busy!";
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        FunfillGridView();
    }


    protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
    {

    }
}
