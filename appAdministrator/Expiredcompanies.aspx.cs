using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
 

public partial class appAdministrator_Expiredcompanies : System.Web.UI.Page
{
    clsRenewal ObjRenewal;

    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string UserEmailID = string.Empty;
        try
        {
            UserId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();//objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

        }
        catch
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        FunRequest();
        try
        {

            ////UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ////RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            ////DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            ////UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            checkButtonDoubleClick(btnSubmit);

            if (RoleId == 8)
            {
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                    GridViewDataBind();
                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }
    public void GridViewDataBind()
    {
        try
        {
            ObjRenewal = new clsRenewal();
            DataTable dt = new DataTable();
            dt = ObjRenewal.Company_Fetch_RejectedListcount(Branchid);
            if (dt.Rows.Count > 0)
            {
                lbl_count_text.Text = " Expired Companies " + dt.Rows[0]["expierdcount"].ToString();
            }
            //if (hdn_show.Value == "1")
            //{
            //    dt = ObjRenewal.Company_Fetch_RejectedList(Branchid);
            //    gvCertificateDetails.DataSource = dt;
            //    gvCertificateDetails.DataBind();
            //    lbl_count_text.Text = " Expired Companies " + dt.Rows.Count.ToString();
            //}
            //else
            //{
            //    dt = ObjRenewal.Company_Fetch_RejectedListcount(Branchid);
            //    if (dt.Rows.Count > 0)
            //    {
            //        lbl_count_text.Text = " Expired Companies " + dt.Rows[0]["expierdcount"].ToString();
            //    }
            //}


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (gvCertificateDetails.Rows.Count == 0)
        {
            if (hdn_show.Value == "0")
            {
                ltlMessage.Text = "";
            }
            else
            {
                ltlMessage.Text = "No Data Found";
            }
        }
    }

    protected void gvCertificateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DecryptEncrypt objDecEnc = new DecryptEncrypt();

                string Formtypeid_renewal = objDecEnc.Encrypt("2");
                string BranchId = objDecEnc.Encrypt(Branchid.ToString());

                Literal ltlmess = (Literal)e.Row.FindControl("ltlrenmessage");
                LinkButton lnkrenewal = (LinkButton)e.Row.FindControl("lnkRenewal");
                LinkButton lnkvalidtityreq = (LinkButton)e.Row.FindControl("lnk_requestvalidity");
                HiddenField hdnRenewal = (HiddenField)e.Row.FindControl("HFRenewalFlag");
                HiddenField hdnapprove = (HiddenField)e.Row.FindControl("hdn_approve");
                HiddenField hdnrequest = (HiddenField)e.Row.FindControl("hdn_request");

                string Company_Id = objDecEnc.Encrypt(lnkrenewal.CommandArgument.ToString());

                string imageidRenewal = "UnitRenewalStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + BranchId.ToString();

                //TO ENABLE RENEWAL LINK POST 2 YEARS
                if (hdnRenewal.Value == "1")
                {
                    lnkrenewal.Visible = true;
                    ltlmess.Visible = false;
                    lnkvalidtityreq.Visible = false;
                }
                //TO SHOW RENEWAL PROCESS INITIATED
                else if (hdnRenewal.Value == "2")
                {
                    ltlmess.Visible = true;
                    lnkrenewal.Visible = false;
                    lnkvalidtityreq.Visible = false;

                    //if (Convert.ToInt32(hdnrequest.Value) > 0)
                    //{
                    //    lnkvalidtityreq.Text = "Request";
                    //    lnkvalidtityreq.Enabled = false;
                    //}
                    //else if (Convert.ToInt32(hdnapprove.Value) > 0)
                    //{
                    //    lnkrenewal.Visible = true;
                    //    ltlmess.Visible = false;
                    //    lnkvalidtityreq.Enabled = false;
                    //}


                }
                else
                {
                    if (hdnRenewal.Value == "5")
                    {
                        lnkrenewal.Visible = true;
                    }
                    else
                    {
                        lnkrenewal.Visible = false;
                    }

                    ltlmess.Visible = false;
                    lnkvalidtityreq.Enabled = true;

                    if (Convert.ToInt32(hdnrequest.Value) > 0)
                    {
                        lnkvalidtityreq.Text = "Waiting";
                        lnkvalidtityreq.Enabled = false;
                    }
                    else
                    {
                        lnkvalidtityreq.Text = "Validity ";
                        lnkvalidtityreq.Enabled = true;
                    }

                    if (Convert.ToInt32(hdnapprove.Value) > 0)
                    {
                        lnkrenewal.Visible = true;
                        ltlmess.Visible = false;
                        lnkvalidtityreq.Enabled = false;
                    }

                }

                lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");
                //TO CAPTURE PRINT ACCESS AND ACCEPT BY ADMIN STATUS

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void CertificateListPageindexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCertificateDetails.PageIndex = e.NewPageIndex;
        // Comment on 15 May 2019   GridViewDataBind();

        Bind_Grid_search_condtion();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsgBlacklisted.Text = "";
        Bind_Grid_search_condtion();
    }

    private void Bind_Grid_search_condtion()
    {
        string searchcriteria = string.Empty;
        try
        {
            searchcriteria = txtSearchbox.Text;
            ObjRenewal = new clsRenewal();
            gvCertificateDetails.DataSource = ObjRenewal.CompanyRenewal_Fetch_Rejected_Search(Branchid, Convert.ToInt32(ddlSearchCriteria.SelectedValue), searchcriteria);
            gvCertificateDetails.DataBind();

            if (gvCertificateDetails.Rows.Count == 0)
            {
                ltlMessage.Text = "No Data Found";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void gvCertificateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "VALIDITYREQ")
        {
            txt_registartiondate.Text = string.Empty;
            popup_editvalidity.Show();
            // hid_company_id.Value = e.CommandArgument.ToString();

            string arguments = e.CommandArgument.ToString();
            string[] args = arguments.Split(';');
            string id = args[0];
            string date = args[1].ToString();
            string chkregular = args[2].ToString();
            if (chkregular == "0")
            {
                string enddate = Convert.ToDateTime(date).AddYears(2).AddDays(-1).ToShortDateString();
                date = date + " - " + enddate;
            }
            else
            {
                string enddate = Convert.ToDateTime(date).AddYears(1).AddDays(-1).ToShortDateString();
                date = date + " - " + enddate;
            }
            //date = String.Format("{0:MM/dd/yyyy}", args[1].ToString());
            lbl_existing.Text = date;
            popup_editvalidity.Show();
            hid_company_id.Value = id;
        }



        else if (e.CommandName.Equals("Blacklistunit"))
        {

            lblMsgBlacklisted.Text = "";
            HiddenFieldCompanyID.Value = e.CommandArgument.ToString();

            // ShowExistingUam();
            ModalPopupExtenderBlackList.Show();
            DataSet ds = new DataSet();
            clsMasterApplication objMApplication = new clsMasterApplication();

            ds = Fetch_Blacklistcompany_Dataset(HiddenFieldCompanyID.Value);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdn_previousstatus.Value = ds.Tables[0].Rows[0]["fGPtbl_Company_Status"].ToString();
                }
                else
                {
                    hdn_previousstatus.Value = "5";
                }
            }

        }
    }


    public DataSet Fetch_Blacklistcompany_Dataset(string companyId)
    {
        MyDs = new DataSet();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_BlacklistCompanies_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@companyId", SqlDbType.VarChar, 50).Value = companyId;
            //MyAdp.SelectCommand.Parameters.Add("@companystatus", SqlDbType.VarChar, 50).Value = companystatus;
            //MyAdp.SelectCommand.Parameters.Add("@companyname", SqlDbType.VarChar, 500).Value = companyname;
            //MyAdp.SelectCommand.Parameters.Add("@companypanno", SqlDbType.VarChar, 10).Value = companypanno;
            //MyAdp.SelectCommand.Parameters.Add("@companyreferenceno", SqlDbType.VarChar, 50).Value = companyreferenceno;
            //MyAdp.SelectCommand.Parameters.Add("@companyregistrationnumber", SqlDbType.VarChar, 50).Value = companyregistrationno;
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

    public int UpdateBlacklistcompany(int companyid, string authortiytext, string IomCertificateno, string blacklistreason, int previousstatus, DateTime BlackListDate)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Blacklist_Company_Update_New", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@AuthorityText", SqlDbType.VarChar, 500).Value = authortiytext;
            MySqlComm.Parameters.Add("@IOMCertificateno", SqlDbType.VarChar, 50).Value = IomCertificateno;
            MySqlComm.Parameters.Add("@Blacklistreason", SqlDbType.VarChar, 500).Value = blacklistreason;
            MySqlComm.Parameters.Add("@Previousstatus", SqlDbType.Int).Value = previousstatus;

            MySqlComm.Parameters.Add("@BlackListDate", SqlDbType.DateTime).Value = BlackListDate;

            SqlParameter outVal = new SqlParameter("@out", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);

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
        return ErrorId;
    }
    protected void btnBlackList_Click(object sender, EventArgs e)
    {

        clsMasterApplication objMApplication = new clsMasterApplication();
        int retval = UpdateBlacklistcompany(Convert.ToInt32(HiddenFieldCompanyID.Value),
            txtAuthorityName.Text.Trim(), txtIOM.Text.Trim(), txtBlackListReason.Text.Trim(),
            Convert.ToInt32(hdn_previousstatus.Value), Convert.ToDateTime(txtBlacklistDate.Text));
        txtAuthorityName.Text = "";
        txtIOM.Text = "";
        txtBlackListReason.Text = "";
        txtBlacklistDate.Text = "";

        lblMsgBlacklisted.Text = "The company has been blacklisted successfully and the changes have been applied. "; 
        try
        {
            Bind_Grid_search_condtion();
            ltlMessage.Text = "";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsRenewal objrenew = new clsRenewal();
        int errorid = objrenew.f_Request_changevaliditydate(Convert.ToInt32(hid_company_id.Value), txt_registartiondate.Text);
        GridViewDataBind();
    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Please Wait';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }

    protected void lnk_showall_Click(object sender, EventArgs e)
    {
        txtSearchbox.Text = "";
        hdn_show.Value = "1";
        Bind_Grid_search_condtion();
    }
}
