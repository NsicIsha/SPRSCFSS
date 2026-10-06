using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Data.SqlClient;

public partial class appAdministrator_Online_Renewal_AmendmentPaymentDetails : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    //DataSet MyDsInspUser = new DataSet();
    clsAmendment objAmendment;
    clsMasterMailer objMailer;
    clsRenewal objRenewal;
    clsMasterInspectionAgency objInsUser;// = new clsMasterInspectionAgency();
    string compname, compid, branchname;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    int successId, urlId = 0, CUnitAmendmentId = 0;
    string UserFullName;
    string emailid;
    public int companyAction = 0;
    public int AmdmentID = 0;
    string MailTo, MailFrom;

    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    Hashtable hashtbl = new Hashtable();
    string UserEmailID = string.Empty;
    public delegate Hashtable waitcompAccepDel(int unitid, int inspuserid, int userid);

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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            // checkButtonDoubleClick(btnSubmit);
            if (RoleId == 8)
            {
                if (Convert.ToInt32(Request.QueryString["i"]) == 1)
                {
                    lblRegistration.Text = "Pending Registration";
                    urlId = 1;//FOR WAITING COMPANY REGISTRATION
                }
                else if (Convert.ToInt32(Request.QueryString["i"]) == 2)
                {
                    lblRegistration.Text = "Renewal Payment Details";
                    urlId = 2;//FOR WAITING COMPANY RENEWAL
                }
                else if (Convert.ToInt32(Request.QueryString["i"]) == 3)
                {
                    lblRegistration.Text = "Amendment";
                    urlId = 3;//FOR WAITING COMPANY AMENDMENT
                }


                //MyDsInspUser = objInsUser.MasterInspectionAgency_User_FetchByBranchUserId_DataSet(UserId);

                if (Page.IsPostBack != true)
                {
                    if (urlId == 1)
                    {
                        //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                        GridViewDataBind(1);
                    }
                    else if (urlId == 2)
                    {
                        //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                        GridViewDataBind(2);
                    }
                    else if (urlId == 3)
                    {
                        //ACTIVATE THE VIEW AND FILLING GRIDVIEW

                        GridViewDataBind(3);
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
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }
    public DataSet Company_WaitingCompany_Registration_FetchAllCompanies_Dataset_Online(int userid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Monetary_Renewal_Fetch_All_Online_Renewal", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_TUB_UserId", SqlDbType.Int).Value = userid;

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
    public void GridViewDataBind(int id)
    {
        if (id == 1)
        {
            //try
            //{
            //    objCompany = new clsCompany();
            //    DataSet ds_waitingcompanies = new DataSet();
            //    ds_waitingcompanies = Company_WaitingCompany_Registration_FetchAllCompanies_Dataset_Online(UserId);
            //    if (ds_waitingcompanies.Tables[0].Rows.Count > 0)
            //    {
            //        gvRegistrationDetails.DataSource = ds_waitingcompanies.Tables[0];

            //        gvRegistrationDetails.DataBind();
            //    }
            //    else
            //    {
            //        gvRegistrationDetails.Dispose();
            //        gvRegistrationDetails.DataBind();
            //        ds_waitingcompanies.Dispose();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrHandler.WriteError(ex.Message);
            //}
        }

        else if (id == 2)
        {
            try
            {
                objAmendment = new clsAmendment();
                DataSet ds_renewal = new DataSet();
                ds_renewal = Company_WaitingCompany_Registration_FetchAllCompanies_Dataset_Online(UserId);
                if (ds_renewal.Tables[0].Rows.Count > 0)
                {
                    gvRegistrationDetails.DataSource = ds_renewal.Tables[0];
                    gvRegistrationDetails.DataBind();
                }
                else
                {
                    gvRegistrationDetails.Dispose();
                    gvRegistrationDetails.DataBind();
                    ds_renewal.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }

        else if (id == 3)
        {
            try
            {
                objAmendment = new clsAmendment();
                DataSet ds_amedment = new DataSet();
                ds_amedment = CompanyAmendment_WaitingCompany_FetchAllCompanies_Dataset(UserId, "Renewalamendment");
                if (ds_amedment.Tables[0].Rows.Count > 0)
                {
                    gvRegistrationDetails.DataSource = ds_amedment.Tables[0];
                    gvRegistrationDetails.DataBind();
                }
                else
                {
                    gvRegistrationDetails.Dispose();
                    gvRegistrationDetails.DataBind();
                    ds_amedment.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }

        try
        {
            if (Session["companyAction"] != null)
            {
                LabelAction.Text = Session["companyAction"].ToString();
                companyAction = 1;
                Session["companyAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                companyAction = 0;
                Session["companyAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["companyAction"] = null;
        }
        if (gvRegistrationDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }

    }

    //TO FETCH ALL DATA OF AMENDMENT WITH WAITING STATUS
    public DataSet CompanyAmendment_WaitingCompany_FetchAllCompanies_Dataset(int userid, string amdrenewal)
    {
        try
        {
            MyDs = new DataSet();
            if (amdrenewal == "Renewalamendment")
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyAmendment_Waiting_Fetch_Company_All_Online", DBManager.ConnectToDB());
            }
            else if (amdrenewal == "renewal")
            {
                MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_Company_All", DBManager.ConnectToDB());
            }

            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message) ;
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    protected void gvRegistrationDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Literal ltlsno = (Literal)e.Row.FindControl("ltlSno");
                ltlsno.Text = count.ToString();
                count++;

                DecryptEncrypt objDecr = new DecryptEncrypt();

                HiddenField hdn = (HiddenField)e.Row.FindControl("hdncid");
                HiddenField hdnbrn = (HiddenField)e.Row.FindControl("hdnbranch");
                HiddenField hdnBrEmail = (HiddenField)e.Row.FindControl("hdnbranchmail");
                HiddenField hdnCompEmail = (HiddenField)e.Row.FindControl("hdncompanyemail");

                compid = objDecr.Encrypt(hdn.Value.ToString());
                compname = objDecr.Encrypt(e.Row.Cells[2].Text.ToString());
                branchname = objDecr.Encrypt(hdnbrn.Value.ToString());
                ////string brnchmail = objDecr.Encrypt(hdnBrEmail.Value.ToString());
                ////string coemail = objDecr.Encrypt(hdnCompEmail.Value.ToString());
                //string EncryptCompanyid = objDecr.Encrypt(CompanyId.ToString());
                //string EncryptCompanyname = objDecr.Encrypt(UnitId.ToString());


                HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string CompanyID = Convert.ToString(hdn.Value);
                string EncryptCompany = objDecEnc.Encrypt(CompanyID.ToString());
                string EncryptUnit = objDecEnc.Encrypt("0");
                string EncryptStatus = objDecEnc.Encrypt("0");
                string EncryptBranchId = objDecEnc.Encrypt("0");
                string EncryptUrlId = objDecEnc.Encrypt("3");
                if (urlId == 3)
                {
                    hyplnk.NavigateUrl = "~/appAdministrator/UnitAmendment_ViewChanges.aspx?&c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlId;
                }
                else if (urlId == 2)
                {
                    EncryptUrlId = objDecEnc.Encrypt("2");
                    //hyplnk.NavigateUrl = "~/appAdministrator/UnitAmendment_ViewChanges.aspx?&c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlId;
                    hyplnk.NavigateUrl = "~/appAdministrator/Online_ReportViewPage_Renewal.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname;
                }
                else if (urlId == 1)
                {
                    hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname;
                }
                //HyperLink hyplnkmail = (HyperLink)e.Row.FindControl("lnkmail");
                //hyplnkmail.NavigateUrl = "~/appAdministrator/Sendmail.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname + "&Branchmail=" + brnchmail + "&Compemail=" + coemail;
                //HDNUnitAmendmentId = (HiddenField)e.Row.FindControl("HFCUAmendmentId");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public DataSet Fetch_ChangesType_Renewal_Amendment_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_ChangesType_Renewal_Amendment_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
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

    protected void gvRegistrationDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            Myds = new DataSet();
            if (urlId == 1)
            {
                if (e.CommandName == "view")
                {
                    Response.Redirect("WaitingCompanies_View.aspx?i=1&id=" + e.CommandArgument, false);
                }


                else if (e.CommandName == "accept")
                {

                    ////// Response.Redirect("WaitingCompanies_Accept.aspx?i=1&uid=" + e.CommandArgument, false);

                    //TO BIND CHECKLIST ON CLICK OF ACCEPT BUTTON
                    Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyIdPopup_Dataset(Convert.ToInt32(e.CommandArgument));

                    /*    ChkListFactory.DataSource = Myds;
                        ChkListFactory.DataTextField = Myds.Tables[0].Columns["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                        ChkListFactory.DataValueField = Myds.Tables[0].Columns["fGPtbl_CUnit_Id"].ToString();
                        ChkListFactory.DataBind(); */

                    //rptFactory.DataSource = Myds;
                    //rptFactory.DataBind();

                    ////TO SHOW MODALPOPUP
                    //ModalPopupExtender1.Show();
                }

                else if (e.CommandName == "ViewPaymentDetails")
                {

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string CompanyId = commandArgs[0];
                    string PaymentId = commandArgs[1];

                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompanyId = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptPaymentId = objDecEnc.Encrypt(PaymentId.ToString());


                    Response.Redirect("Online_RenewalPaymentDetails.aspx?cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);

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

                else if (e.CommandName == "reject")
                {

                    Response.Redirect("WaitingCompanies_Reject_Online.aspx?i=1&uid=" + e.CommandArgument, false);
                }
            }

            else if (urlId == 2)
            {
                //objRenewal = new clsRenewal();
                //successId = objRenewal.CompanyRenewal_WaitingCompanies_Accept(Convert.ToInt32(e.CommandArgument), UserId);
                //if (successId == 2) //FOR SUCCESS
                //{
                //    GridViewDataBind(2);
                //}

                if (e.CommandName == "view")
                {
                    Response.Redirect("WaitingCompanies_View.aspx?i=1&id=" + e.CommandArgument, false);
                }

                else if (e.CommandName == "ViewAppDetails")
                {
                    //Page.ClientScript.RegisterStartupScript(
                    // this.GetType(), "OpenWindow", "window.open('WaitingCompanies_View.aspx?i=1&id=" + e.CommandArgument + "','_newtab');", true);
                    Response.Redirect("Online_ReportViewPage_Renewal.aspx?i=1&id=" + e.CommandArgument + "");
                }



                else if (e.CommandName == "accept")
                {

                    ////// Response.Redirect("WaitingCompanies_Accept.aspx?i=1&uid=" + e.CommandArgument, false);

                    //TO BIND CHECKLIST ON CLICK OF ACCEPT BUTTON
                    Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyIdPopup_Dataset(Convert.ToInt32(e.CommandArgument));

                    /*    ChkListFactory.DataSource = Myds;
                        ChkListFactory.DataTextField = Myds.Tables[0].Columns["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                        ChkListFactory.DataValueField = Myds.Tables[0].Columns["fGPtbl_CUnit_Id"].ToString();
                        ChkListFactory.DataBind(); */

                    //rptFactory.DataSource = Myds;
                    //rptFactory.DataBind();

                    ////TO SHOW MODALPOPUP
                    //ModalPopupExtender1.Show();
                }

                else if (e.CommandName == "ViewPaymentDetails")
                {

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string CompanyId = commandArgs[0];
                    string PaymentId = commandArgs[1];

                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompanyId = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptPaymentId = objDecEnc.Encrypt(PaymentId.ToString());

                    DataSet dsf = new DataSet();
                    dsf = Fetch_ChangesType_Renewal_Amendment_Online(Convert.ToInt32(CompanyId.ToString()));
                    int InspectionfeeStatus = 0;
                    InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

                    // InspectionfeeStatus = 240;
                    if (InspectionfeeStatus == 240)
                    {
                        Response.Redirect("Online_Renewal_VerifyPaymentDetails.aspx?i=3&cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);
                    }
                    else // without Inspection fee
                    {
                        Response.Redirect("Online_Renewal_Amendment_PaymentDetails_WithoutInsp.aspx?i=3&cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);
                    }



                    //Response.Redirect("Online_Renewal_VerifyPaymentDetails.aspx?cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);
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

                else if (e.CommandName == "reject")
                {

                    Response.Redirect("WaitingCompanies_Reject_Online.aspx?i=1&uid=" + e.CommandArgument, false);
                }
            }

            else if (urlId == 3)
            {
                //TO FETCH
                objAmendment = new clsAmendment();
                //DataSet myCUnitAmendmentId=new DataSet();
                DataTable CUnitAmendmentList = new DataTable();

                //REDIRECT TO UnitAmendment_ViewChanges.aspx
                if (e.CommandName == "view")
                {
                    //Response.Redirect("WaitingCompanies_View.aspx?i=3&id="+e.CommandArgument+"&aid=" + CUnitAmendmentId, false);
                }
                else if (e.CommandName == "accept")
                {

                    CUnitAmendmentList = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(Convert.ToInt32(e.CommandArgument));

                    /*  ChkListFactory.DataSource = CUnitAmendmentList;
                      ChkListFactory.DataTextField = CUnitAmendmentList.Columns["fGPtbl_CUnit_1B_Factory_Address"].ToString();
                      ChkListFactory.DataValueField = CUnitAmendmentList.Columns["fGPtbl_CUnit_Id"].ToString();
                      ChkListFactory.DataBind();*/

                    ////rptFactory.DataSource = CUnitAmendmentList;
                    ////rptFactory.DataBind();


                    ////ModalPopupExtender1.Show();

                    //GridViewDataBind(3);
                }
                else if (e.CommandName == "reject")
                {
                    Response.Redirect("WaitingCompanies_Reject.aspx?i=3&uid=" + e.CommandArgument + "&aid=" + CUnitAmendmentId, false);
                }
                else if (e.CommandName == "ViewPaymentDetails")
                {

                    string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                    string CompanyId = commandArgs[0];
                    string PaymentId = commandArgs[1];

                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompanyId = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptPaymentId = objDecEnc.Encrypt(PaymentId.ToString());

                    DataSet dsf = new DataSet();
                    dsf = Fetch_ChangesType_Renewal_Amendment_Online(Convert.ToInt32(CompanyId.ToString()));
                    int InspectionfeeStatus = 0;
                    InspectionfeeStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);

                    // InspectionfeeStatus = 240;
                    if (InspectionfeeStatus == 240)
                    {
                        Response.Redirect("Online_Renewal_Amendment_PaymentDetails.aspx?i=3&cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);
                    }
                    else // without Inspection fee
                    {
                        Response.Redirect("Online_Renewal_Amendment_PaymentDetails_WithoutInsp.aspx?i=3&cid=" + EncryptCompanyId + "&pid=" + EncryptPaymentId, false);
                    }

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
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }




    public void AcceptCompanies(int Unitid, int InsUserId)
    {
        try
        {
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();
            //clsMasterMailer objMailer = new clsMasterMailer();
            waitcompAccepDel waitCompAccept = new waitcompAccepDel(objCompany.Company_WaitingCompany_Registration_Accept);

            //   string FromMail = string.Empty, ccMail = string.Empty, bccMail = string.Empty, subject = string.Empty, bodyMail = string.Empty;
            DataSet Myds = new DataSet();

            if (urlId == 1)
            {

                //hashtbl = objCompany.Company_WaitingCompany_Registration_Accept(Unitid, UserId);
                //calling through delegate
                hashtbl = waitCompAccept(Unitid, InsUserId, UserId);
                emailid = hashtbl["email"].ToString();
                successId = Convert.ToInt32(hashtbl["ErrorId"].ToString());
            }

            /*  if (urlId == 2)
              {
                  objRenewal = new clsRenewal();
                  successId = objRenewal.CompanyRenewal_WaitingCompanies_Accept(Unitid,InsUserId, UserId);
              }*/

            else if (urlId == 3)
            {
                hashtbl = objAmendment.CompanyAmendment_WaitingCompany_Accept(Unitid, InsUserId, UserId);
                emailid = hashtbl["email"].ToString();
                successId = Convert.ToInt32(hashtbl["ErrorId"].ToString());
            }

            //MAIL HAS TO HANDLE ACCORDING TO RENEWAL
            if (successId == 2)
            {

                Myds = new DataSet();
                objMailer = new clsMasterMailer();
                Myds = objMailer.MasterMailer_Fetch_DataSet(1); //Change Here
                MailTo = emailid; //Myds.Tables[0].Rows[0]["fGPtbl_MM_To"].ToString();  // hdnbranchmail  hdnBrEmail //
                MailFrom = UserEmailID; //Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
                ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
                bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
                subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
                bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +txtReason.Text + ".";
                string bodystr = string.Empty;
                if (hashtbl["CompanyName"].ToString() != null)
                {
                    string str = bodyMail.Replace("$$CompanyName$$", hashtbl["CompanyName"].ToString());
                    bodystr = str.Replace("$$Branchname$$", hashtbl["BranchName"].ToString());
                    //SendMail.Mail_WithOUTAttachment(emailid, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodystr, "", 0, "", "", "");
                }

                bodyMail = "Dear" + ' ' + "Sir/Mam," + "<br/><br/>" + bodystr;
                sendmail(MailFrom, MailTo, bccMail, ccMail, subject, bodyMail);
                Session["companyAction"] = "Status changed successfully!";
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //try
        //{
        //    /*foreach (ListItem item in ChkListFactory.Items)
        //    {
        //        if (item.Selected)
        //        {
        //            AcceptCompanies(Convert.ToInt32(item.Value));
        //        }
        //    }*/

        //    foreach (RepeaterItem itm in rptFactory.Items)
        //    {
        //        HiddenField hdnunitId = (HiddenField)itm.FindControl("hdnFactId");
        //        DropDownList ddlInsUserId = (DropDownList)itm.FindControl("ddlInspagency");
        //        AcceptCompanies(Convert.ToInt32(hdnunitId.Value), Convert.ToInt32(ddlInsUserId.SelectedValue));
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.WriteError(ex.Message);
        //}
        ////BINDING GRIDVIEW

        //if (urlId == 1)
        //{
        //    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
        //    GridViewDataBind(1);
        //}
        //else if (urlId == 3)
        //{
        //    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
        //    GridViewDataBind(3);
        //}

    }

    // Fetch Unit Id through Temporary Table Where Changes Occured(REPRESENTED BY Zero values)

    /*  public DataTable Bindtable(int companyid)
      {
          DataSet MydsChange = new DataSet();
          objAmendment = new clsAmendment();
          List<int> UnitIdchanged = new List<int>();
          DataTable dt, dt2 = new DataTable();
          try
          {
              //MydsChange = objAmendment.CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(85);
              MydsChange = objAmendment.CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(companyid);

              dt = MydsChange.Tables[1];
              if (dt.Rows.Count > 0)
              {
                  UnitIdchanged = (from r in dt.AsEnumerable()
                                   where r.Field<int>("fGPtbl_CUnit_3E") == 0 || r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_SSIRegistrationNo") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_SSIRegistrationDate") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_CategoryAreaId") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_CategoryCastId") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_CategoryUnitSizeId") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_1B_Factory_Address") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_1B_Factory_Zip") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_1B_Factory_StateId") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_1B_Factory_CityId") == 0 ||
                                   //r.Field<int>("fGPtbl_CUnit_1B_Factory_Phone") == 0 ||
                                   //r.Field<int>("fGPtbl_CUnit_1B_Factory_Fax") == 0 ||
                                   //r.Field<int>("fGPtbl_CUnit_1B_Factory_Email") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3A") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3C123_ConstitutionId") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3C5") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3D") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3F") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3G") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3H") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3I") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3J") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3K1") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3K2") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3L") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3M") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3N") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_3O") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_4A") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_4B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_5A2") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_5BA") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_5BB1") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_5BB2") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_5C") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_6A1") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_6A2") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_6B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_7A") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_7B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_7C") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_7D") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_7E") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_8") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_9") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_10") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_11") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_12A") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_12B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_13A") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_13B") == 0 ||
                                   r.Field<int>("fGPtbl_CUnit_13C") == 0
                                   select r.Field<int>("fGPtbl_CUnit_Id")).ToList();

                  /* foreach (var onl in UnitId)
                           {
                       unitid1 += onl.ToString() + ",";

                        }
              }
              dt2 = objAmendment.CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(companyid, UnitIdchanged);
          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }
          return dt2;

      }*/


    // Send Mail


    /// Send Mail Function
    // public void sendmail( )
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
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 1, 1, subject, bodyMail, 1);
        }
        catch (Exception ex)
        {
            //ex.Message("Server Busy!!!!!!!!!");
        }
    }

    protected void rptFactory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        objInsUser = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField hdnunit_Id = (HiddenField)e.Item.FindControl("hdnFactId");
                string untid = hdnunit_Id.Value;
                LinkButton lnk_Cover_letter = (LinkButton)e.Item.FindControl("lnk_cover");
                string imageid = string.Empty;
                imageid = "CoverLetterofInspectionAgency.aspx?id=" + untid + "&option=" + urlId;
                Myds = objInsUser.MasterInspectionAgency_User_FetchByUnit_DataSet(Convert.ToInt32(hdnunit_Id.Value), urlId);
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddlInspUser = (DropDownList)e.Item.FindControl("ddlInspagency");
                    ddlInspUser.DataSource = Myds;// MyDsInspUser;
                    ddlInspUser.DataValueField = Myds.Tables[0].Columns["AgencyUserId"].ToString();
                    ddlInspUser.DataTextField = Myds.Tables[0].Columns["AgencyUsername"].ToString();
                    ddlInspUser.DataBind();
                }
                lnk_Cover_letter.Attributes.Add("onclick", "return newdocumentopen('" + imageid + "');");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            Myds.Dispose();
        }
    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Please wait.';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }
}