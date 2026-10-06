using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appInspectionAgency_InspectionDatetobeIssued_View : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID;
    string USERFULLNAME, BRANCHNAME;
    clsMasterInspectionAgency ObjAgency;
    int unit, company, formtype, amendmentid;

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

        FunRequest();
        

        if (!Page.IsPostBack)
        {
            try
            {
                FunfillGridView(unit,company,formtype);
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
    }


    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            company = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            unit = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            formtype = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
        }
        catch
        {
            company = 0;
            unit = 0;
            formtype = 0;
        }
    }

    private void FunfillGridView(int unit, int company, int formtype)
    {
        try
        {
            ObjAgency = new clsMasterInspectionAgency();

            System.Data.DataTable Dt = new System.Data.DataTable();
            clsAmendment objAmendment = new clsAmendment();
            amendmentid = objAmendment.CompanyAmendment_WaitingCompany_FetchAmendmentId(unit);
            Dt = ObjAgency.Agency_Fetch_DatetobeIssuedByUnitCompanyFormtype(amendmentid,unit, company, formtype);
            if (Dt.Rows.Count != 0)
            {
                lblType.Text = Dt.Rows[0]["fGPtbl_MF_Name"].ToString();
                lblCompany.Text = Dt.Rows[0]["fGPtbl_Company_Name"].ToString();
                lblRegno.Text = Dt.Rows[0]["fGPtbl_CUnit_SSIRegistrationNo"].ToString();
                lblPan.Text = Dt.Rows[0]["fGPtbl_Company_PAN"].ToString();
                lblRequest.Text = Dt.Rows[0]["fGPtbl_IS_DateStatus2"].ToString();
            }
            else
            {
                lblType.Text = "";
                lblCompany.Text = "";
                lblRegno.Text = "";
                lblPan.Text = "";
                lblRequest.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void lbBack_Click(object sender, EventArgs e)
    {
        if (formtype == 1)
        {
            Response.Redirect("InspectionDatetobeIssued.aspx?i=1", false);
        }
        else if(formtype==3)
        {
            Response.Redirect("InspectionDatetobeIssued.aspx?i=3", false);
        }
    }
}
