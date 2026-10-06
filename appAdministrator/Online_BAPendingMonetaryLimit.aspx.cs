using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_Online_BAPendingMonetaryLimit : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int monetaryAction = 0;
    int successId, urlId, amendmentid = 0;
    string ToMail = string.Empty;
    string FromMail = string.Empty;
    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    string UserEmailID = string.Empty;
    string compname, compid, branchname;
    Hashtable hashtbl = new Hashtable();
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    //FormInputReplace objInputReplace;
    int outVariable = 0;


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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 4)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
                }

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvMonetary.SetActiveView(ViewMonetaryDetail);
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
            ErrHandler.WriteError("Error in Page load() " + ex.StackTrace);
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


    public DataSet Company_MonetaryLimit_Fetch_Datasetrenew(int UserId)
    {
        try
        {
            
            MyDs = new DataSet();
            //MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_All_Online_renewal", DBManager.ConnectToDB());
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_All_Online_renewal_Accountview", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = UserId;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }


    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        try
        {
            if (urlId == 1)
            {
                objCompany = new clsCompany();

                gvMonetaryDetails.DataSource = objCompany.Company_MonetaryLimit_Fetch_Dataset(UserId);
                gvMonetaryDetails.DataBind();
            }
            else if (urlId == 2)
            {
                clsRenewal objrenewal = new clsRenewal();
                gvMonetaryDetails.DataSource = Company_MonetaryLimit_Fetch_Datasetrenew(UserId);
                gvMonetaryDetails.DataBind();
            }
            else if (urlId == 3)
            {
                objAmendment = new clsAmendment();

                gvMonetaryDetails.DataSource = objAmendment.CompanyAmendment_MonetaryLimit_Fetch_Dataset(UserId);
                gvMonetaryDetails.DataBind();
            }

            if (gvMonetaryDetails.Rows.Count == 0)
            {
                mvMonetary.SetActiveView(ViewMonetaryNotFound);
                lblMessage.Text = "No Monetary limit to be issued!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["monetaryAction"] != null)
            {
                LabelAction.Text = Session["monetaryAction"].ToString();
                monetaryAction = 1;
                Session["monetaryAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                monetaryAction = 0;
                Session["monetaryAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["monetaryAction"] = null;
        }
    }


    protected void gvMonetaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lbcalmntlm = (LinkButton)e.Row.FindControl("lbcalculate");
                //  lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();

                int companyId = Convert.ToInt32(lbcalmntlm.CommandArgument.ToString());
                int isprovisional;
                objCompany = new clsCompany();
                isprovisional = objCompany.Company_Provisional_Status(companyId);

                string url = urlId.ToString();

                //////
                DecryptEncrypt objDecr = new DecryptEncrypt();

                HiddenField hdn = (HiddenField)e.Row.FindControl("hdncid");
                HiddenField hdnbrn = (HiddenField)e.Row.FindControl("hdnbranch");
                HiddenField hdnBrEmail = (HiddenField)e.Row.FindControl("hdnbranchmail");
                HiddenField hdnCompEmail = (HiddenField)e.Row.FindControl("hdncompanyemail");
                compid = objDecr.Encrypt(hdn.Value.ToString());
                compname = objDecr.Encrypt(e.Row.Cells[2].Text.ToString());
                branchname = objDecr.Encrypt(hdnbrn.Value.ToString());
                string brnchmail = objDecr.Encrypt(hdnBrEmail.Value.ToString());
                string coemail = objDecr.Encrypt(hdnCompEmail.Value.ToString());


                HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string CompanyID = Convert.ToString(hdn.Value);
                string EncryptCompany = objDecEnc.Encrypt(CompanyID.ToString());
                string EncryptUnit = objDecEnc.Encrypt("0");
                string EncryptStatus = objDecEnc.Encrypt("0");
                string EncryptBranchId = objDecEnc.Encrypt("0");


                if (urlId == 1)
                {
                    
                    string EncryptUrlId = objDecEnc.Encrypt("1");
                    // hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname ;
                    hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPageNew.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname + "&url=" + EncryptUrlId;
                }
                else if (urlId == 2)
                {
                    string EncryptUrlId = objDecEnc.Encrypt("2");
                    // hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname ;                 }
                    hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPageNew.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname + "&url=" + EncryptUrlId;
                }




                //////



                ////// OLD ONE COMMENTED ON 04 DECEMBER 2018  K C INDIA LABORATRY LLP(IN CASE OF PROVISIONAL RENEWAL FY ENTRY PANEL WAS NOT VISIBLE)
                //////if (isprovisional == 0)
                //////{
                //////    lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();
                //////}
                //////else
                //////{
                //////    lbcalmntlm.PostBackUrl = "Provisional_MonetaryLimit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString(); ;
                //////}

                if (url == "2")
                {
                    lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();
                }
                else
                {
                    if (isprovisional == 0)
                    {
                        lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();
                    }
                    else
                    {
                        lbcalmntlm.PostBackUrl = "Provisional_MonetaryLimit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString(); ;
                    }
                }



                //if (url == "1" || url == "2")
                //{
                //    if (isprovisional == 0)
                //    {
                //        lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();
                //    }
                //    else
                //    {
                //        lbcalmntlm.PostBackUrl = "Provisional_MonetaryLimit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString(); ;
                //    }
                //}
                //else
                //{
                //    lbcalmntlm.PostBackUrl = "Calculate_Monetary_Limit.aspx?id=" + Convert.ToString(lbcalmntlm.CommandArgument) + "&urlid=" + urlId.ToString();
                //}



            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void gvMonetaryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objAmendment = new clsAmendment();

            //THIS IS FOR SETTING MONETARY LIMIT
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Set Monetary Limit";

                //CAPTURING PRIMARY ID
                HiddenFieldCompanyId.Value = e.CommandArgument.ToString();

                clsCompany objCompany = new clsCompany();
                hdnprovisionl.Value = objCompany.Company_Provisional_Status(Convert.ToInt32(HiddenFieldCompanyId.Value)).ToString();

                BindGrid(Convert.ToInt32(e.CommandArgument.ToString()));

                ModalPopupExtender1.Show();

            }

            if (e.CommandName == "ViewPaymentDetails")
            {

                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                string CompanyId = commandArgs[0];
                string PaymentId = commandArgs[1];

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyId = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptPaymentId = objDecEnc.Encrypt(PaymentId.ToString());


                Response.Redirect("Online_Renewal_VerifyPaymentDetails_View.aspx?cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);

                //////////    //TO BIND CHECKLIST ON CLICK OF ACCEPT BUTTON
                //////////    Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyIdPopup_Dataset(Convert.ToInt32(e.CommandArgument));

                ///////////*    ChkListFactory.DataSource = Myds;
                //////////    ChkListFactory.DataTextField = Myds.Tables[0].Columns["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                //////////    ChkListFactory.DataValueField = Myds.Tables[0].Columns["fGPtbl_CUnit_Id"].ToString();
                //////////    ChkListFactory.DataBind(); */

                //////////    rptFactory.DataSource = Myds;
                //////////    rptFactory.DataBind();

                //////////    //TO SHOW MODALPOPUP
                //////////    ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public void BindGrid(int Companyid)
    {
        objCompany = new clsCompany();
        DataTable dt = new DataTable();
        if (urlId == 1)
        {
            dt = objCompany.Company_Fetch_ByUnit_Step4(Companyid).Tables[0];
        }
        else if (urlId == 2)
        {
            dt = objCompany.Company_Fetch_ByUnit_Step4(Companyid).Tables[0];
        }
        else if (urlId == 3)
        {
            //dt = objAmendment.CompanyAmendment_Fetch_UnitChanged_Datatable(Companyid);
            dt = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(Companyid);
        }
        int count = (dt.Rows.Count);
        hdnfactcnt.Value = count.ToString();

        //int s = 0;

        DataTable gt = new DataTable();
        gt.Columns.Add("year", typeof(string));
        gt.Columns.Add("Fact1", typeof(string));
        gt.Columns.Add("Fact2", typeof(string));
        gt.Columns.Add("Fact3", typeof(string));
        gt.Columns.Add("Fact4", typeof(string));
        gt.Columns.Add("Fact5", typeof(string));

        int indx = 0;
        string unt1 = "", unt2 = "", unt3 = "", unt4 = "", unt5 = "";

        if (count == 1)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
        }

        if (count == 2)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
            unt2 = dt.Rows[1]["fGPtbl_CUnit_Id"].ToString();
        }
        if (count == 3)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
            unt2 = dt.Rows[1]["fGPtbl_CUnit_Id"].ToString();
            unt3 = dt.Rows[2]["fGPtbl_CUnit_Id"].ToString();
        }
        if (count == 3)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
            unt2 = dt.Rows[1]["fGPtbl_CUnit_Id"].ToString();
            unt3 = dt.Rows[2]["fGPtbl_CUnit_Id"].ToString();
        }
        if (count == 4)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
            unt2 = dt.Rows[1]["fGPtbl_CUnit_Id"].ToString();
            unt3 = dt.Rows[2]["fGPtbl_CUnit_Id"].ToString();
            unt4 = dt.Rows[3]["fGPtbl_CUnit_Id"].ToString();
        }
        if (count == 5)
        {
            unt1 = dt.Rows[0]["fGPtbl_CUnit_Id"].ToString();
            unt2 = dt.Rows[1]["fGPtbl_CUnit_Id"].ToString();
            unt3 = dt.Rows[2]["fGPtbl_CUnit_Id"].ToString();
            unt4 = dt.Rows[3]["fGPtbl_CUnit_Id"].ToString();
            unt5 = dt.Rows[4]["fGPtbl_CUnit_Id"].ToString();
        }
        while (indx < 4)
        {
            if (count == 1)
            {
                gt.Rows.Add("", unt1, "", "", "", "");
            }
            else if (count == 2)
            {
                gt.Rows.Add("", unt1, unt2, "", "", "");
            }
            else if (count == 3)
            {
                gt.Rows.Add("", unt1, unt2, unt3, "", "");
            }
            else if (count == 4)
            {
                gt.Rows.Add("", unt1, unt2, unt3, unt4, "");
            }
            else if (count == 5)
            {
                gt.Rows.Add("", unt1, unt2, unt3, unt4, unt5);
            }

            indx++;
        }

        grdTurnOver.DataSource = gt;
        grdTurnOver.DataBind();
        gt.Columns.Clear();
        gt.Rows.Clear();
        gt.Dispose();

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        try
        {
            objCompany = new clsCompany();

            //===============================================================================
            //CODE TO GET THE VALUES OF GRIDVIEW
            int rw, cl;
            rw = grdTurnOver.Rows.Count;
            cl = grdTurnOver.Columns.Count;
            string[,] grdvalue = new string[rw, cl];
            string strunit1 = "", strunit2 = "", strunit3 = "", strunit4 = "";
            string strunit1ex = string.Empty, strunit2ex = string.Empty, strunit3ex = string.Empty, strunit4ex = string.Empty;

            foreach (GridViewRow rowObj in grdTurnOver.Rows)
            {
                int k = rowObj.RowIndex;
                string a, stryear;

                TextBox txbx = (TextBox)grdTurnOver.Rows[k].FindControl("txt_year");
                stryear = txbx.Text.Trim();


                if (Convert.ToInt32(hdnfactcnt.Value) == 1)
                {
                    TextBox txfact1 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact1");
                    HiddenField hdn1 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact1");
                    if (k == 0 && txfact1.Text != "" && txbx.Text != "")
                    {
                        strunit1 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 1 && txfact1.Text != "" && txbx.Text != "")
                    {
                        strunit2 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 2 && txfact1.Text != "" && txbx.Text != "")
                    {
                        strunit3 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 3 && txfact1.Text != "")
                    {
                        strunit4 = hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim();
                    }


                }
                else if (Convert.ToInt32(hdnfactcnt.Value) == 2)
                {
                    TextBox txfact1 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact1");
                    TextBox txfact2 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact2");
                    HiddenField hdn1 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact1");
                    HiddenField hdn2 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact2");
                    if (k == 0 && txfact1.Text != "" && txfact2.Text != "" && txbx.Text != "")
                    {
                        strunit1 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+") + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+");
                    }
                    else if (k == 1 && txfact1.Text != "" && txfact2.Text != "" && txbx.Text != "")
                    {
                        strunit2 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 2 && txfact1.Text != "" && txfact2.Text != "" && txbx.Text != "")
                    {
                        strunit3 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 3 && txfact1.Text != "" && txfact2.Text != "")
                    {
                        strunit4 = hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim();
                    }

                }
                else if (Convert.ToInt32(hdnfactcnt.Value) == 3)
                {
                    TextBox txfact1 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact1");
                    TextBox txfact2 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact2");
                    TextBox txfact3 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact3");
                    HiddenField hdn1 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact1");
                    HiddenField hdn2 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact2");
                    HiddenField hdn3 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact3");
                    if (k == 0 && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txbx.Text != "")
                    {
                        strunit1 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                           ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 1 && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txbx.Text != "")
                    {
                        strunit2 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 2 && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txbx.Text != "")
                    {
                        strunit3 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 3 && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "")
                    {
                        strunit4 = hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim();

                    }

                }
                else if (Convert.ToInt32(hdnfactcnt.Value) == 4)
                {
                    TextBox txfact1 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact1");
                    TextBox txfact2 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact2");
                    TextBox txfact3 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact3");
                    TextBox txfact4 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact4");
                    HiddenField hdn1 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact1");
                    HiddenField hdn2 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact2");
                    HiddenField hdn3 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact3");
                    HiddenField hdn4 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact4");
                    if (k == 0 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txbx.Text != "")
                    {
                        strunit1 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                           ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 1 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txbx.Text != "")
                    {
                        strunit2 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 2 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txbx.Text != "")
                    {
                        strunit3 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 3 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "")
                    {
                        strunit4 = hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                    }
                }
                else if (Convert.ToInt32(hdnfactcnt.Value) == 5)
                {
                    TextBox txfact1 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact1");
                    TextBox txfact2 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact2");
                    TextBox txfact3 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact3");
                    TextBox txfact4 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact4");
                    TextBox txfact5 = (TextBox)grdTurnOver.Rows[k].FindControl("txtbx_fact5");
                    HiddenField hdn1 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact1");
                    HiddenField hdn2 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact2");
                    HiddenField hdn3 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact3");
                    HiddenField hdn4 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact4");
                    HiddenField hdn5 = (HiddenField)grdTurnOver.Rows[k].FindControl("hdnfact5");
                    if (k == 0 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txfact5.Text != "" && txbx.Text != "")
                    {
                        strunit1 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                            ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                        strunit1ex = stryear + "," + hdn5.Value + "," + txfact5.Text.Replace(".", "+").Trim();
                    }
                    else if (k == 1 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txfact5.Text != "" && txbx.Text != "")
                    {
                        strunit2 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                             ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                        strunit2ex = stryear + "," + hdn5.Value + "," + txfact5.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 2 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txfact5.Text != "" && txbx.Text != "")
                    {
                        strunit3 = stryear + "," + hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                             ":" + stryear + "," + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + stryear + "," + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                        strunit3ex = stryear + "," + hdn5.Value + "," + txfact5.Text.Replace(".", "+").Trim();

                    }
                    else if (k == 3 && txfact1.Text != "" && txfact1.Text != "" && txfact2.Text != "" && txfact3.Text != "" && txfact4.Text != "" && txfact5.Text != "")
                    {
                        strunit4 = hdn1.Value + "," + txfact1.Text.Replace(".", "+").Trim() + ":" + hdn2.Value + "," + txfact2.Text.Replace(".", "+").Trim() +
                             ":" + hdn3.Value + "," + txfact3.Text.Replace(".", "+").Trim() + ":" + hdn4.Value + "," + txfact4.Text.Replace(".", "+").Trim();

                        strunit4ex = hdn5.Value + "," + txfact5.Text.Replace(".", "+").Trim();

                    }

                }

            }

            if (urlId == 1)
            {
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(1, strunit1, strunit2, strunit3, strunit4, UserId, RoleId);
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(1, strunit1ex, strunit2ex, strunit3ex, strunit4ex, UserId, RoleId);
            }
            else if (urlId == 2)
            {
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(2, strunit1, strunit2, strunit3, strunit4, UserId, RoleId);
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(2, strunit1ex, strunit2ex, strunit3ex, strunit4ex, UserId, RoleId);
            }
            else if (urlId == 3)
            {
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(3, strunit1, strunit2, strunit3, strunit4, UserId, RoleId);
                objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(3, strunit1ex, strunit2ex, strunit3ex, strunit4ex, UserId, RoleId);
            }

            GridViewDataBind();
            sendmailzonalhead();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }

    //TO SEND MAIL TO ZONAL HEAD
    public void sendmailzonalhead()
    {
        try
        {

            clsMasterUser objUser = new clsMasterUser();
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            
            DataSet Myds = new DataSet();
            //TO BRANCH ACCOUNT MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(8); //Change Here
          //  FromMail = UserEmailID.ToString();
            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString(); // UserEmailID.ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            string bodyMail_data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            string bodystr = string.Empty;
            DataTable companydt = new DataTable();

            companydt = objCompany.Company_Detail_Fetch_ById(Convert.ToInt32(HiddenFieldCompanyId.Value));
            string str = string.Empty;
            if (companydt.Rows.Count > 0)
            {
                str = bodyMail_data.Replace("$$CompanyName$$", companydt.Rows[0]["fGPtbl_Company_Name"].ToString());
            }
            else
            {
                str = "";
            }


            clsMasterInspectionAgency objInspectionagency = new clsMasterInspectionAgency();
            DataTable branchdt = new DataTable();
            branchdt = objInspectionagency.Trans_InspectionagecnyBranch_Fetch_DataTableByUserId(UserId);
           
            if (branchdt.Rows.Count > 0)
            {
                bodystr = str.Replace("$$Branchname$$", branchdt.Rows[0]["BranchName"].ToString());
            }
            else
            {
                bodystr = "";
            }


            //TO FETCH BR ACCOUNT MAILER
            Myds = objUser.MasterUser_FetchZonalHeadEmailById_DataSet(UserId);
            ToMail = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
            string UserNameEmail = Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
            bodyMail = "Dear Sir/Madam <br/><br/>" + bodystr;
            sendmail(FromMail, ToMail, bccMail, ccMail, subject, bodyMail);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.StackTrace);
        }
    }

    protected void grdTurnOver_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int factcount = Convert.ToInt32(hdnfactcnt.Value);
        int i = 1;
        int j = e.Row.RowIndex;
        if (e.Row.RowIndex >= 0)
        {
            //while (i <= 5)
            //{
            TextBox txtamnt1 = (TextBox)e.Row.FindControl("txtbx_fact1");
            TextBox txtamnt2 = (TextBox)e.Row.FindControl("txtbx_fact2");
            TextBox txtamnt3 = (TextBox)e.Row.FindControl("txtbx_fact3");
            TextBox txtamnt4 = (TextBox)e.Row.FindControl("txtbx_fact4");
            TextBox txtamnt5 = (TextBox)e.Row.FindControl("txtbx_fact5");
            TextBox txtyear = (TextBox)e.Row.FindControl("txt_year");

            int chkprovisional = Convert.ToInt32(hdnprovisionl.Value);
            if (chkprovisional == 1)
            {
                txtamnt1.Text = "0.0";
                txtamnt2.Text = "0.0";
                txtamnt3.Text = "0.0";
                txtamnt4.Text = "0.0";
                txtamnt5.Text = "0.0";
                txtyear.Text = "0";

                txtyear.Enabled = false;
                txtamnt1.Enabled = false;
                txtamnt2.Enabled = false;
                txtamnt3.Enabled = false;
                txtamnt4.Enabled = false;
                txtamnt5.Enabled = false;
            }
            else
            {
                txtamnt1.Text = "";
                txtamnt2.Text = "";
                txtamnt3.Text = "";
                txtamnt4.Text = "";
                txtamnt5.Text = "";
                txtyear.Text = "";
                txtyear.Enabled = true;
                txtamnt1.Enabled = true;
                txtamnt2.Enabled = true;
                txtamnt3.Enabled = true;
                txtamnt4.Enabled = true;
                txtamnt5.Enabled = true;
            }

            if (factcount == 4)
            {
                txtamnt5.Visible = false;
                grdTurnOver.Columns[5].Visible = false;
            }
            else if (factcount == 3)
            {
                txtamnt5.Visible = false;
                txtamnt4.Visible = false;

                grdTurnOver.Columns[4].Visible = false;
                grdTurnOver.Columns[5].Visible = false;
            }
            else if (factcount == 2)
            {
                txtamnt5.Visible = false;
                txtamnt4.Visible = false;
                txtamnt3.Visible = false;
                grdTurnOver.Columns[3].Visible = false;
                grdTurnOver.Columns[4].Visible = false;
                grdTurnOver.Columns[5].Visible = false;
            }
            else if (factcount == 1)
            {
                txtamnt5.Visible = false;
                txtamnt4.Visible = false;
                txtamnt3.Visible = false;
                txtamnt2.Visible = false;
                grdTurnOver.Columns[2].Visible = false;
                grdTurnOver.Columns[3].Visible = false;
                grdTurnOver.Columns[4].Visible = false;
                grdTurnOver.Columns[5].Visible = false;
            }

        }




        if (Convert.ToInt32(e.Row.RowIndex) == 3)
        {
            int chkprovisional = Convert.ToInt32(hdnprovisionl.Value);
            TextBox txtyear = (TextBox)e.Row.FindControl("txt_year");

            Label lbllimit = (Label)e.Row.FindControl("lbl_limit");

            txtyear.Visible = false;

            lbllimit.Width = Unit.Pixel(150);

            lbllimit.Text = "Monetary Limit";
            lbllimit.Visible = true;

            TextBox txtamnt = (TextBox)e.Row.FindControl("txtbx_fact1");

            if (chkprovisional == 1)
            {
                txtamnt.Text = "5.0";
                txtamnt.Enabled = false;
            }
            else
            {
                try
                {

                    if (urlId == 2)
                    {
                        clsRenewal objrenew = new clsRenewal();
                        DataSet Myds = new DataSet();
                        int compaNYID;
                        compaNYID = Convert.ToInt32(HiddenFieldCompanyId.Value);
                        try
                        {
                            Myds = objrenew.Company_MonetaryLimit_FetchByCompId_Datasethistory(compaNYID);
                        }
                        catch { }
                        //     Myds = objrenew.Company_MonetaryLimit_FetchByCompId_Dataset(compaNYID);


                        if (Myds.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = Myds.Tables[0];
                            dt.Columns.RemoveAt(0);

                            gv_display.Visible = true;
                            gv_display.DataSource = dt;
                            gv_display.DataBind();

                        }
                        else
                        {
                            gv_display.Visible = false;
                            gv_display.DataSource = null;
                            gv_display.DataBind();
                        }
                    }
                }
                catch { }
            }//else


        }

    }
    // To send Mail


    public static void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailMessage message = new MailMessage();
            message.From = new MailAddress(MailFrom);  // MailFrom
            // Set the recepient address of the mail message
            message.To.Add(new MailAddress(MailTo));

            // Check if the bcc value is null or an empty string
            if ((bccMail != null) && (bccMail != string.Empty))
            {
                // Set the Bcc address of the mail message
                message.Bcc.Add(new MailAddress(bccMail));
            }      // Check if the cc value is null or an empty value
            if ((ccMail != null) && (ccMail != string.Empty))
            {
                // Set the CC address of the mail message
                message.CC.Add(new MailAddress(ccMail));
            }       // Set the subject of the mail message
            message.Subject = subject;
            // Set the body of the mail message
            message.Body = bodyMail;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25; 587;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, subject, bodyMail, 1);

        }
        catch (Exception ex)
        {
            //ex.Message;
        }
    }
}