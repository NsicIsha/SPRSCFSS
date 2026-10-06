using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_CertificateList : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;


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

 

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);

            //RangeValidator1.MinimumValue = DateTime.Now.Date.ToString("MM/dd/yyyy");
            //RangeValidator1.MaximumValue = DateTime.Now.Date.AddDays(30).ToString("MM/dd/yyyy");

            //RangeValidator1.MinimumValue = System.DateTime.Now.AddMonths(-1).ToShortDateString();
            //RangeValidator1.MaximumValue = System.DateTime.Now.ToShortDateString();

            //RangeValidator1.MinimumValue = Convert.ToDateTime(txtFIRDate.Text).ToShortDateString();
            //RangeValidator1.MaximumValue = Convert.ToDateTime(txtFIRDate.Text).AddDays(30).ToShortDateString();


            if (RoleId == 8)
            {
                dtold = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["fixeddate"]);
                if (Page.IsPostBack != true)
                {
                    //FILLING GRIDVIEW
                    GridViewDataBind();


                    if (!Page.IsPostBack)
                    {
                        rdl_inspectionmode_payment.Items.Clear();
                        rdl_inspectionmode_payment.Visible = true;
                        ListItem item;
                        item = new ListItem("DD", "DD");
                        rdl_inspectionmode_payment.Items.Add(item);

                        item = new ListItem("CHEQUE", "CHEQUE");
                        rdl_inspectionmode_payment.Items.Add(item);

                        item = new ListItem("NEFT", "NEFT");
                        rdl_inspectionmode_payment.Items.Add(item);


                        rdl_inspectionmode_payment.Items.FindByValue("DD").Selected = true;
                        PanelChequeDD.Visible = true;
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


    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        try
        {
            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objCompany.Company_Fetch_CertificateAll(Branchid);
            //if (hid_val.Value == "1")
            //{
            //    gvCertificateDetails.DataSource = dt;
            //    gvCertificateDetails.DataBind();
            //}
            int k = 0;
            if (dt.Rows.Count > 0)
            {
                k = Convert.ToInt32(dt.Rows[0]["regiscount"].ToString());
            }


            lbl_count_text.Text = "Registered Companies :" + Convert.ToString(k);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (gvCertificateDetails.Rows.Count == 0)
        {
            if (hid_val.Value == "0")
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
                LinkButton btnPrint = (LinkButton)e.Row.FindControl("lbnPrint");
                LinkButton btnDraftPrint = (LinkButton)e.Row.FindControl("lbnDraftPrint");
                LinkButton btnRequest = (LinkButton)e.Row.FindControl("lbnRequest");
                LinkButton btnRequest1 = (LinkButton)e.Row.FindControl("lbnRequest1");
                Literal ltlprint1 = (Literal)e.Row.FindControl("ltlPrint1");
                Literal litamd = (Literal)e.Row.FindControl("lt_amendment");
                Literal ltlprint = (Literal)e.Row.FindControl("ltlPrint");
                HiddenField hdncompId = (HiddenField)e.Row.FindControl("HiddenFieldCompanyId");
                HiddenField hdnCompIdAmd = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdAmendment");
                //HiddenField hdnFormType = (HiddenField)e.Row.FindControl("HiddenFormType");
                string FormtypeCertificate = string.Empty;
                string imageid = string.Empty;
                string imgdraft = string.Empty;
                string imageid_Amedment = string.Empty;
                LinkButton amendment = (LinkButton)e.Row.FindControl("lnkamedment");

                //string checkvaluerequset = "";
                //DataTable dt_201415 = new DataTable();
                //dt_201415 = dt_fetch_companyrecord201415(Convert.ToInt32(hdncompId.Value));
                //if (dt_201415.Rows.Count > 0)
                //{
                //    checkvaluerequset = dt_201415.Rows[0]["CompanyId"].ToString();
                //   if (checkvaluerequset != "")
                //    {
                //        btnPrint.CommandArgument =checkvaluerequset.ToString() + "," + "2";
                //    }
                //}
                //else
                //{
                //    dt_201415.Dispose();
                //}

                // FormtypeCertificate = hdnFormType.Value;
                // if (FormtypeCertificate == "1")
                //  {
                // imageid = "RegistrationCertificate.aspx?id=" + hdncompId.Value;
                imageid = "RegistrationCertificatePageView.aspx?id=" + hdncompId.Value;
                //imgdraft = "CompanyCertificatePrintPreview.aspx?id=" + hdncompId.Value;
                //imgdraft = "testaspxkkk.aspx?id=" + hdncompId.Value;
                imgdraft = "CompanyCertificatePrintPreviewPage.aspx?id=" + hdncompId.Value;
                // }
                LinkButton btnAmd = (LinkButton)e.Row.FindControl("lnkamedment");
                LinkButton lnkamdement_print = (LinkButton)e.Row.FindControl("lnkamdement_print");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string Company_Id = objDecEnc.Encrypt(hdnCompIdAmd.Value);
                //  if (FormtypeCertificate== "3")
                // {
                imageid_Amedment = "AmedmentReport.aspx?id=" + hdncompId.Value;
                // }
                string Formtypeid = objDecEnc.Encrypt("3");
                string Formtypeid_renewal = objDecEnc.Encrypt("2");
                string BranchId = objDecEnc.Encrypt(Branchid.ToString());
                HiddenField hdnAmedment = (HiddenField)e.Row.FindControl("HiddenAmedment");
                string StrAmedment = hdnAmedment.Value;
                Literal ltlmess = (Literal)e.Row.FindControl("ltlrenmessage");
                LinkButton lnkrenewal = (LinkButton)e.Row.FindControl("lnkRenewal");
                HiddenField hdnRenewalOpenflag = (HiddenField)e.Row.FindControl("HFRenewalOpenFlag");

                //TO OPEN RENEWAL LINK
                if (hdnRenewalOpenflag.Value == "1")
                {
                    lnkrenewal.Visible = true;
                    ltlmess.Visible = false;
                    amendment.Visible = true;
                }
                if (hdnRenewalOpenflag.Value == "1" || hdnRenewalOpenflag.Value == "5" || hdnRenewalOpenflag.Value == "6")
                {
                    lnkrenewal.Visible = true;
                    ltlmess.Visible = false;
                    amendment.Visible = true;
                }

                //TO SHOW RENEWAL PROCESS START MESSAGE
                else if (hdnRenewalOpenflag.Value == "2")
                {
                    ltlmess.Visible = true;
                    amendment.Visible = false;
                    lnkrenewal.Visible = false;
                }
                else if (hdnRenewalOpenflag.Value == "4")
                {
                    // string ss = ltlmess.Text;
                    amendment.Visible = false;
                    ltlmess.Visible = false;
                    lnkrenewal.Visible = false;
                    //ltlmess.Text = "Amendment process started";
                    //  string sd = litamd.Text;
                    litamd.Visible = true;
                }
                else
                {
                    lnkrenewal.Visible = false;
                    ltlmess.Visible = false;
                }

                string imageidAmd = "UnitAmendmentStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid.ToString() + "&b=" + BranchId.ToString();
                string imageidRenewal = "UnitRenewalStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + BranchId.ToString();
                lnkamdement_print.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid_Amedment + "');");

                //btnPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
                btnDraftPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imgdraft + "');");


                // btnAmd.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidAmd + "');");
                //lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");

                if (hdnRenewalOpenflag.Value == "5")
                {
                    btnAmd.Attributes.Add("onclick", "return Confirmrenewal('" + hdncompId.Value + "' , '" + imageidAmd + "');");
                }
                else
                {
                    btnAmd.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidAmd + "');");
                }
                if (hdnRenewalOpenflag.Value == "6")
                {
                    lnkrenewal.Attributes.Add("onclick", "return Confirmamendent('" + hdncompId.Value + "' , '" + imageidRenewal + "');");
                }
                else
                {
                    lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");
                }
                //TO CAPTURE PRINT ACCESS AND ACCEPT BY ADMIN STATUS
                string cmd = btnPrint.CommandArgument.ToString();
                string cmd1 = lnkamdement_print.CommandArgument.ToString();
                string[] cmdargument = cmd.Split(',');
                if (cmdargument[0] != "1")//PRINT ALREADY TAKEN
                {
                    btnDraftPrint.Visible = false;
                    if (cmdargument[1] == "1")//WAITING STATUS
                    {
                        ltlprint.Text = "Waiting";
                        btnPrint.Visible = false;
                        btnRequest.Visible = false;
                    }
                    else if (cmdargument[1] == "2")//REJECTED STATUS
                    {
                        //ltlprint.Text = "Rejected";
                        ltlprint.Visible = false;
                        btnPrint.Visible = false;
                        btnRequest.Visible = true;
                    }
                    else if (cmdargument[1] == "3")//PRINT REQUEST ACCEPTED BY ADMIN
                    {
                        ltlprint.Visible = false;
                        btnPrint.Visible = true;
                        btnRequest.Visible = false;
                    }
                    else//NEW PRINT
                    {
                        btnPrint.Visible = false;
                        btnRequest.Visible = true;
                        ltlprint.Visible = false;
                    }
                }
                else
                {
                    btnPrint.Visible = true;
                    btnDraftPrint.Visible = true;
                    btnRequest.Visible = false;
                    ltlprint.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public DataSet Company_PrintDate_Comment_For_certificate(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Fetch_PrintDate_Comment_For_certificate", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;

            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDs;
    }

    public void setDateANDComment(int CompanyID)
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds_fetch_comment = new DataSet();
            ds_fetch_comment = Company_PrintDate_Comment_For_certificate(CompanyID);
            if (ds_fetch_comment.Tables[0].Rows.Count > 0)
            {
                txt_bx_comments.Text = ds_fetch_comment.Tables[0].Rows[0]["Name_Comment"].ToString();
                txtdateofissue.Text = ds_fetch_comment.Tables[0].Rows[0]["CERTIFICATE_FirstPrintDate"].ToString();
                //txt_bx_comments.Enabled = false;
                //txtdateofissue.Enabled = false;

            }
            else
            {
                ds_fetch_comment.Dispose();
                //txt_bx_comments.Enabled = true;
                //txtdateofissue.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    private void ShowExistingUam()
    {
        try
        {
            SqlCommand MySqlComm = new SqlCommand("sp_ExistingUdyam", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int, 500).Value = HiddenFieldCompanyID.Value;
            MySqlComm.Parameters.Add("@Existing", SqlDbType.VarChar, 500);
            MySqlComm.Parameters["@Existing"].Direction = ParameterDirection.Output;
            MySqlComm.ExecuteNonQuery();
            TextBoxExisting.Text = (string)MySqlComm.Parameters["@Existing"].Value;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
        }
    }
    protected void gvCertificateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int optionvar = 0;
        objCompany = new clsCompany();
        try
        {
            if (e.CommandName == "request")//REQUEST FOR TAKING REQUEST
            {
                //get company id
                GridViewRow viewRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                HiddenField hdncompId = (HiddenField)viewRow.FindControl("HiddenFieldCompanyId");
                hdnfieldcompanyid.Value = hdncompId.Value;
                hdnfieldCertificateId.Value = e.CommandArgument.ToString();


                ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "request1")
            {
                hdnfieldCertificateId.Value = e.CommandArgument.ToString();
                ModalPopupExtender1.Show();
            }


            else if (e.CommandName.Equals("Udyam"))
            {


                HiddenFieldCompanyID.Value = e.CommandArgument.ToString();
                TextBoxExisting.Text = string.Empty;
                TextBoxUdyam.Text = string.Empty;
                ShowExistingUam();
                ModalPopupUdyam.Show();

            }


            else if (e.CommandName.Equals("Blacklistunit"))
            {
                lblMsgBlacklisted.Text = "";

                HiddenFieldCompanyID.Value = e.CommandArgument.ToString();
                TextBoxExisting.Text = string.Empty;
                TextBoxUdyam.Text = string.Empty;
               // ShowExistingUam();
                ModalPopupExtenderBlackList.Show();
                DataSet ds = new DataSet();
                clsMasterApplication objMApplication = new clsMasterApplication();

                ds = objMApplication.Fetch_Blacklistcompany_Dataset(HiddenFieldCompanyID.Value, null, null, null, null, null);

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

            else if (e.CommandName == "print")
            {//5

                GridViewRow viewRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;

                HiddenField hdncompId = (HiddenField)viewRow.FindControl("HiddenFieldCompanyId");

                // To Diplay First Print Date and Comment 1.12.2017

                setDateANDComment(Convert.ToInt32(hdncompId.Value));

                // To Diplay First Print Date and Comment 1.12.2017

                LinkButton btnPrintcommand = (LinkButton)viewRow.FindControl("lbnPrint");
                hidpopupcompanyid.Value = hdncompId.Value;
                HiddenField hdfregflag = (HiddenField)viewRow.FindControl("HiddenFormPrintFlag");
                string[] arrcomargflag = hdfregflag.Value.Split(',');
                if (Convert.ToDateTime(arrcomargflag[1]) > dtold)//COMPANY IS NEW
                //    if (1==1)//COMPANY IS NEW
                {
                    //switch("6")
                    switch (arrcomargflag[0])
                    {
                        case "5"://ALL 3 DATES WILL COME
                            modprintpopup.Show();
                            optionvar = 3;
                            break;
                        case "6": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;
                        case "7": //DATE OF ISSUE WILL COME
                            modprintpopup.Show();
                            optionvar = 1;
                            tr_proposal.Visible = false;
                            tr_validity.Visible = false;
                            break;
                        case "8": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;
                        case "9": //DATE OF ISSUE WILL COME
                            modprintpopup.Show();
                            optionvar = 1;
                            tr_proposal.Visible = false;
                            tr_validity.Visible = false;
                            break;
                        case "10": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;

                    }
                    ;
                }

                else //OLD COMPANY
                {
                    switch (arrcomargflag[0])
                    {
                        case "5"://DATE OF ISSUE WILL COME
                            modprintpopup.Show();
                            tr_proposal.Visible = false;
                            tr_validity.Visible = false;
                            optionvar = 1;
                            break;
                        case "6": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;
                        case "7": //DATE OF ISSUE WILL COME
                            modprintpopup.Show();
                            optionvar = 1;
                            tr_proposal.Visible = false;
                            tr_validity.Visible = false;
                            break;
                        case "8": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;
                        case "9": //DATE OF ISSUE WILL COME
                            modprintpopup.Show();
                            optionvar = 1;
                            tr_proposal.Visible = false;
                            tr_validity.Visible = false;
                            break;
                        case "10": //DATE OF ISSUE WILL NOT COME
                            funopen(hidpopupcompanyid.Value);
                            break;
                    }
                    ;
                }

                /*
                                DataSet ds = new DataSet();
                                objCompany = new clsCompany();
                                ds = objCompany.Get_PopUpPagedata(Convert.ToInt32(hdncompId.Value));
                                if (ds != null)
                                {//4
                                    if (ds.Tables.Count > 0)
                                    {//3
                                        if (ds.Tables[0].Rows.Count > 0)
                                        {//2
                                            if (ds.Tables[0].Rows[0]["printchk"].ToString() == "2")
                                            {
                                                string imageid = "RegistrationCertificatePageView.aspx?id=" + hidpopupcompanyid.Value;
                                                btnPrintcommand.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
                                            }
                                            else
                                            {//1
                                                modprintpopup.Show();
                                                if (ds.Tables[0].Rows[0]["printchk"].ToString() == "1")
                                                {
                                                    tr_proposal.Visible = false;
                                                    tr_validity.Visible = false;
                                                }
                                                else
                                                {
                                                    tr_proposal.Visible = true;
                                                    tr_validity.Visible = true;

                                                }
                                            }//1
                                        }//2
                                    }//3
                                }//4*/

            }//5




            //Dupplicate print code start-->
            else if (e.CommandName.Equals("Duplicateprint"))
            {
                // modalDuplicate.Show();
                GridViewRow grdviewRow = (GridViewRow)((LinkButton)e.CommandSource).Parent.Parent;
                HiddenField Company_ID = (HiddenField)grdviewRow.FindControl("HiddenFieldCompanyId");
                setDateANDComment(Convert.ToInt32(Company_ID.Value));
                hidpopupcompanyid.Value = Company_ID.Value;

                //funopen(hidpopupcompanyid.Value);

                txtFIRDate.Text = "";
                txt_chqddate_inspection.Text = "";
                txt_chqddNo_inspection.Text = "";
                txt_BankNam_inspection.Text = "";
                txt_BankBranch_inspection.Text = "";
                txt_chqddate_inspection.Text = "";
                txt_chqddNo_inspection.Text = "";
                lblRedMsg.Text = "";

                modalDuplicate.Show();
                optionvar = 1;
                tr_proposal.Visible = false;
                tr_validity.Visible = false;

            }

            //Dupplicate print code end<--
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


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
            Convert.ToInt32(hdn_previousstatus.Value),Convert.ToDateTime(txtBlacklistDate.Text));
        txtAuthorityName.Text = "";
        txtIOM.Text = "";
        txtBlackListReason.Text = "";
        txtBlacklistDate.Text = "";

        lblMsgBlacklisted.Text = "The company has been blacklisted successfully and the changes have been applied.";


        try
        {
            ltlMessage.Text = "";
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            //int successId = objCompany.Company_CertificateRequest_Insert(Convert.ToInt32(hdnfieldCertificateId.Value), txtRequest.Text, UserId);
            //    string svalue = ((HiddenField)gvCertificateDetails.SelectedRow.FindControl("HiddenFieldCompanyId")).Value;
            int Companyid = Convert.ToInt32(hdnfieldcompanyid.Value.ToString());
            string check2014_15 = "";
            DataTable ds_2014 = new DataTable();
            ds_2014 = dt_fetch_companyrecord201415(Companyid);
            if (ds_2014.Rows.Count > 0)
            {
                check2014_15 = ds_2014.Rows[0]["CompanyId"].ToString();
            }
            else
            {
                check2014_15 = "";
                ds_2014.Dispose();
            }

            //  if (check2014_15 == "")
            //{
            if (UserId == 8)
            {
                int successId = objCompany.Company_CertificateRequest_Insert(0, Convert.ToInt32(hdnfieldCertificateId.Value), txtRequest.Text, "0", 2, UserId);
            }
            else
            {
                int successId1 = Company_EditRequest_Insert(Companyid, Convert.ToInt32(rdbRequestType.SelectedValue), txtRequest.Text.ToString(), UserId);
            }
            //}
            // else
            //    {
            //  int successId = Company_CertificateRequest_Insert_201415(Convert.ToInt32(check2014_15), 0, Convert.ToInt32(hdnfieldCertificateId.Value), txtRequest.Text, "0", 1, UserId);
            // }
            txtSearchbox.Text = "";
            SearchGridviewBind();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public int Company_EditRequest_Insert(int CompanyId, int ReasonId, string Reason, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_CompanyDetailsEditRequest_Insert", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@ReasonId", SqlDbType.Int).Value = ReasonId;
            MySqlComm.Parameters.Add("@Reason", SqlDbType.VarChar, 5000).Value = Reason;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
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
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }
        return ErrorId;
    }

    public int Company_CertificateRequest_Insert_201415(int CompanyId, int fGPtbl_CR_Id, int fGPtbl_CR_CertificateId, string fGPtbl_CR_Request, string fGPtbl_CR_RejectionReason, int fGPtbl_CR_AcceptedByAdmin, int UserId)
    {
        SqlCommand MySqlComm = new SqlCommand();
        int ErrorId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_CertificateRequest_Insert_201415", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Companyid", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@fGPtbl_CR_Id", SqlDbType.Int).Value = fGPtbl_CR_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CR_CertificateId", SqlDbType.Int).Value = fGPtbl_CR_CertificateId;
            MySqlComm.Parameters.Add("@fGPtbl_CR_Request", SqlDbType.VarChar, 5000).Value = fGPtbl_CR_Request;
            MySqlComm.Parameters.Add("@fGPtbl_CR_RejectionReason", SqlDbType.VarChar, 5000).Value = fGPtbl_CR_RejectionReason;
            MySqlComm.Parameters.Add("@fGPtbl_CR_AcceptedByAdmin", SqlDbType.Int).Value = fGPtbl_CR_AcceptedByAdmin;

            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
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
            MySqlComm.Dispose();
            DBManager.CloseConnection();
        }
        return ErrorId;
    }
    protected void CertificateListPageindexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvCertificateDetails.PageIndex = e.NewPageIndex;
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsgBlacklisted.Text = "";
        try
        {
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void SearchGridviewBind()
    {
        string SearchCriteria = string.Empty;
        SearchCriteria = txtSearchbox.Text.Trim();
        objCompany = new clsCompany();
        DataTable dt = new DataTable();
        dt = objCompany.Company_Fetch_Certificate_Search(Branchid, Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchCriteria);

        DataTable dtNew = new DataTable();
        dtNew.Columns.Add("fGPtbl_CUnit_RegistrationNo");
        dtNew.Columns.Add("fGPtbl_Company_Name");
        dtNew.Columns.Add("fGPtbl_Company_PAN");
        dtNew.Columns.Add("HSCODENPC");
        dtNew.Columns.Add("fGPtbl_Company_HeadOffice_Address");
        dtNew.Columns.Add("fGPtbl_CERTIFICATE_PrintAccess");
        dtNew.Columns.Add("fGPtbl_CR_AcceptedByAdmin");
        dtNew.Columns.Add("fGPtbl_CERTIFICATE_Id");
        dtNew.Columns.Add("fGPtbl_Company_Id");
        dtNew.Columns.Add("doicheck");
        dtNew.Columns.Add("RegistrationDate");
        dtNew.Columns.Add("fgtbl_Certificate_Amedment");
        dtNew.Columns.Add("RenFlag");
        ArrayList compIds = new ArrayList();
        foreach (DataRow dr in dt.Rows)
        {
            string id = dr["fGPtbl_Company_Id"].ToString();

            if (!compIds.Contains(id))
            {
                compIds.Add(id);
            }
        }

        foreach (string id in compIds)
        {

            string fGPtbl_CUnit_RegistrationNo = ("fGPtbl_CUnit_RegistrationNo");
            string fGPtbl_Company_Name = ("fGPtbl_Company_Name");
            string fGPtbl_Company_PAN = ("fGPtbl_Company_PAN");
            string HSCODENPC = ("HSCODENPC");
            string fGPtbl_Company_HeadOffice_Address = ("fGPtbl_Company_HeadOffice_Address");
            string fGPtbl_CERTIFICATE_PrintAccess = ("fGPtbl_CERTIFICATE_PrintAccess");

            string fGPtbl_CR_AcceptedByAdmin = ("fGPtbl_CR_AcceptedByAdmin");
            string fGPtbl_CERTIFICATE_Id = ("fGPtbl_CERTIFICATE_Id");
            string fGPtbl_Company_Id = ("fGPtbl_Company_Id");
            string doicheck = ("doicheck");
            string RegistrationDate = ("RegistrationDate");
            string fgtbl_Certificate_Amedment = ("fgtbl_Certificate_Amedment");
            string RenFlag = ("RenFlag");

            string ss = Convert.ToString(dt.Select("fGPtbl_Company_Id=" + id).Length);
            if (dt.Select("fGPtbl_Company_Id=" + id).Length > 1)
            {


                DataRow[] dr = dt.Select("fGPtbl_Company_Id=" + id);
                fGPtbl_CUnit_RegistrationNo = dr[0]["fGPtbl_CUnit_RegistrationNo"].ToString();
                fGPtbl_Company_Name = dr[0]["fGPtbl_Company_Name"].ToString();
                fGPtbl_Company_PAN = dr[0]["fGPtbl_Company_PAN"].ToString();
                HSCODENPC = dr[0]["HSCODENPC"].ToString();
                HSCODENPC = HSCODENPC + "<br/>" + dr[1]["HSCODENPC"].ToString();
                fGPtbl_Company_HeadOffice_Address = dr[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                fGPtbl_CERTIFICATE_PrintAccess = dr[0]["fGPtbl_CERTIFICATE_PrintAccess"].ToString();
                fGPtbl_CR_AcceptedByAdmin = dr[0]["fGPtbl_CR_AcceptedByAdmin"].ToString();
                fGPtbl_CERTIFICATE_Id = dr[0]["fGPtbl_CERTIFICATE_Id"].ToString();
                fGPtbl_Company_Id = dr[0]["fGPtbl_Company_Id"].ToString();
                doicheck = dr[0]["doicheck"].ToString();
                RegistrationDate = dr[0]["RegistrationDate"].ToString();
                fgtbl_Certificate_Amedment = dr[0]["fgtbl_Certificate_Amedment"].ToString();
                RenFlag = dr[0]["RenFlag"].ToString();
                dtNew.Rows.Add(
                     fGPtbl_CUnit_RegistrationNo,
                     fGPtbl_Company_Name,
                     fGPtbl_Company_PAN,
                     HSCODENPC,
                     fGPtbl_Company_HeadOffice_Address,
                     fGPtbl_CERTIFICATE_PrintAccess,
                     fGPtbl_CR_AcceptedByAdmin,
                     fGPtbl_CERTIFICATE_Id,
                     fGPtbl_Company_Id,
                     doicheck,
                     RegistrationDate,
                     fgtbl_Certificate_Amedment, RenFlag
                     );



            }
            else
            {
                DataRow[] dr = dt.Select("fGPtbl_Company_Id=" + id);
                fGPtbl_CUnit_RegistrationNo = dr[0]["fGPtbl_CUnit_RegistrationNo"].ToString();
                fGPtbl_Company_Name = dr[0]["fGPtbl_Company_Name"].ToString();
                fGPtbl_Company_PAN = dr[0]["fGPtbl_Company_PAN"].ToString();
                HSCODENPC = dr[0]["HSCODENPC"].ToString();
                fGPtbl_Company_HeadOffice_Address = dr[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                fGPtbl_CERTIFICATE_PrintAccess = dr[0]["fGPtbl_CERTIFICATE_PrintAccess"].ToString();
                fGPtbl_CR_AcceptedByAdmin = dr[0]["fGPtbl_CR_AcceptedByAdmin"].ToString();
                fGPtbl_CERTIFICATE_Id = dr[0]["fGPtbl_CERTIFICATE_Id"].ToString();
                fGPtbl_Company_Id = dr[0]["fGPtbl_Company_Id"].ToString();
                doicheck = dr[0]["doicheck"].ToString();
                RegistrationDate = dr[0]["RegistrationDate"].ToString();
                fgtbl_Certificate_Amedment = dr[0]["fgtbl_Certificate_Amedment"].ToString();
                RenFlag = dr[0]["RenFlag"].ToString();
                dtNew.Rows.Add(
                     fGPtbl_CUnit_RegistrationNo,
                     fGPtbl_Company_Name,
                     fGPtbl_Company_PAN,
                     HSCODENPC,
                     fGPtbl_Company_HeadOffice_Address,
                     fGPtbl_CERTIFICATE_PrintAccess,
                     fGPtbl_CR_AcceptedByAdmin,
                     fGPtbl_CERTIFICATE_Id,
                     fGPtbl_Company_Id,
                     doicheck,
                     RegistrationDate,
                     fgtbl_Certificate_Amedment, RenFlag
                     );
            }

        }
        gvCertificateDetails.DataSource = dtNew;
        gvCertificateDetails.DataBind();

        if (gvCertificateDetails.Rows.Count == 0)
        {
            ltlMessage.Text = "No Data Found";
        }
        else
        {
            ltlMessage.Text = " ";
        }
    }

    protected void btnSubmit_print_Click(object sender, EventArgs e)
    {
        objCompany = new clsCompany();

        //  int options = 3; Earlier option was set as 3 by developer but due to this issue in fresh count for datastatus live report was coming
        //as on option 3 in SP cUnit_Registration date was getting updated by date of issue resulting in count change


        int options = 1;//Change done by NSIC 13/02/2018


        if (txtvaliditydate.Text == string.Empty && txtpropasaldate.Text == string.Empty && txtdateofissue.Text != string.Empty)
        {
            options = 1;
        }
        //int errorid = objCompany.Update_ComapnY_Validityproposaldate(Convert.ToInt32(hidpopupcompanyid.Value), txtdateofissue.Text, txtvaliditydate.Text, txtpropasaldate.Text, options, Convert.ToString(txt_bx_comments.Text.ToString()),txtCertificateNumber.Text);

        int errorid = objCompany.Update_ComapnY_Validityproposaldate(Convert.ToInt32(hidpopupcompanyid.Value), txtdateofissue.Text, txtvaliditydate.Text, txtpropasaldate.Text, options, Convert.ToString(txt_bx_comments.Text.ToString()));

        //int errorid = 1;
        if (errorid == 1)
        {
            modprintpopup.Hide();
            funopen(hidpopupcompanyid.Value);
        }
        else
        {
            modprintpopup.Hide();
        }
    }

    public int InsertDupliate_Certificate_PayentDetails(string UTR, int comopanyId, string firdate, string PaymentType, string ChequeNumber, string BankName, string BankBranch, decimal Amount, string chequedate, int options, string CertificateNumber)//
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_Dupicate_Payment_Details", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;


            MySqlComm.Parameters.Add("@UTRNumber", SqlDbType.VarChar, 500).Value = UTR;

            MySqlComm.Parameters.Add("@companyid", SqlDbType.Int).Value = comopanyId;
            MySqlComm.Parameters.Add("@firdate", SqlDbType.VarChar, 50).Value = firdate;
            MySqlComm.Parameters.Add("@PaymentType", SqlDbType.VarChar, 50).Value = PaymentType;
            MySqlComm.Parameters.Add("@ChequeNumber", SqlDbType.VarChar, 50).Value = ChequeNumber;

            MySqlComm.Parameters.Add("@BankName", SqlDbType.VarChar, 50).Value = BankName;
            MySqlComm.Parameters.Add("@BankBranch", SqlDbType.VarChar, 50).Value = BankBranch;


            MySqlComm.Parameters.Add("@Amount", SqlDbType.Decimal).Value = Amount;
            MySqlComm.Parameters.Add("@chequedate", SqlDbType.VarChar, 50).Value = chequedate;

            MySqlComm.Parameters.Add("@options", SqlDbType.Int).Value = options;

            MySqlComm.Parameters.Add("@CertificateNumber", SqlDbType.VarChar, 500).Value = CertificateNumber;




            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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


    protected void btnSubmit_duplicateprint_Click(object sender, EventArgs e)
    {

        int result = 0;

        if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "DD" || Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "CHEQUE")
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
            result = DateTime.Compare(DateTime.ParseExact(txt_chqddate_inspection.Text, "MM/dd/yyyy", theCultureInfo), DateTime.ParseExact(txtFIRDate.Text, "MM/dd/yyyy", theCultureInfo));

        }
        else if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "NEFT")
        {
            IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
            result = DateTime.Compare(DateTime.ParseExact(txtDate_InspectionAgency.Text, "MM/dd/yyyy", theCultureInfo), DateTime.ParseExact(txtFIRDate.Text, "MM/dd/yyyy", theCultureInfo));

        }

        DateTime fir_date;
        DateTime DD_date;
        if (result >= 0)
        {

            objCompany = new clsCompany();


            int options = 1;//Change done by NSIC 13/02/2018       
            int totaldays = 0;


            if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "DD" || Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "CHEQUE")
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                fir_date = DateTime.ParseExact(txtFIRDate.Text, "MM/dd/yyyy", theCultureInfo);
                DD_date = DateTime.ParseExact(txt_chqddate_inspection.Text, "MM/dd/yyyy", theCultureInfo);
                totaldays = Convert.ToInt32(((DD_date - fir_date).TotalDays).ToString());

            }
            else if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "NEFT")
            {
                IFormatProvider theCultureInfo = new System.Globalization.CultureInfo("en-GB", true);
                fir_date = DateTime.ParseExact(txtFIRDate.Text, "MM/dd/yyyy", theCultureInfo);
                DD_date = DateTime.ParseExact(txtDate_InspectionAgency.Text, "MM/dd/yyyy", theCultureInfo);
                totaldays = Convert.ToInt32(((DD_date - fir_date).TotalDays).ToString());
            }

            //  if (!(Convert.ToInt32(diff2) > Convert.ToInt32(30)))
            if (totaldays <= 30)
            {

                //if (Convert.ToDateTime(txtFIRDate.Text).Date <= Convert.ToDateTime(checkDate2.ToShortDateString()) && Convert.ToDateTime(txtFIRDate.Text) <= Convert.ToDateTime(myDateTime.ToShortDateString()))
                //{     //is between the 2 dates
                int errorid = 0;
                if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "DD" || Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "CHEQUE")
                {
                    errorid = InsertDupliate_Certificate_PayentDetails("",
                       Convert.ToInt32(hidpopupcompanyid.Value), txtFIRDate.Text, rdl_inspectionmode_payment.SelectedValue, txt_chqddNo_inspection.Text, txt_BankNam_inspection.Text, txt_BankBranch_inspection.Text, Convert.ToDecimal(txt_DD_Inspection.Text), txt_chqddate_inspection.Text, options, txtCertificateNumber_Duplicate.Text);//, 

                }
                else if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "NEFT")
                {
                    errorid = InsertDupliate_Certificate_PayentDetails(txtUTRNO_InspectionAgency.Text,
                        Convert.ToInt32(hidpopupcompanyid.Value), txtFIRDate.Text, rdl_inspectionmode_payment.SelectedValue, txt_chqddNo_inspection.Text, txtBankName_InspectionAgency.Text, txtBranchName_InspectionAgency.Text, Convert.ToDecimal(txtAmount_InspectionAgency.Text), txtDate_InspectionAgency.Text, options, txtCertificateNumber_Duplicate.Text);//, txtCertificateNumber_Duplicate.Text

                }

                //int errorid = 1;
                if (errorid >= 1)
                {
                    modprintpopup.Hide();
                    funopenDuplicate(hidpopupcompanyid.Value);
                }
                else
                {
                    // modprintpopup.Hide();
                }
            }
            else
            {
                lblRedMsg.Text = "Duplicate certificate will be generated only whithin 1 Month from FIR Date";
                modalDuplicate.Show();

            }
        }
        else
        {
            lblRedMsg.Text = "DD/NEFT Date can not be before FIR Date";
            modalDuplicate.Show();
        }



    }


    protected void rbl_selectindexchanged_InspectionAgency(object sender, EventArgs e)
    {
        //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
        if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "DD" || Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "CHEQUE")
        {
            PanelChequeDD.Visible = true;
            PanelInspectionOnlieRites.Visible = false;

            modalDuplicate.Show();

            //Cheque DD Validation
            RequiredFieldValidator5.Enabled = true;
            RequiredFieldValidator6.Enabled = true;
            RequiredFieldValidator7.Enabled = true;
            //   RequiredFieldValidator8.Enabled = true;
            RequiredFieldValidator9454.Enabled = true;

            //NEFT validation
            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator11.Enabled = false;
            RequiredFieldValidator12.Enabled = false;
            RequiredFieldValidator13.Enabled = false;
        }
        else if (Convert.ToString(rdl_inspectionmode_payment.SelectedItem.Value.ToString()) == "NEFT")
        {
            PanelInspectionOnlieRites.Visible = true;
            PanelChequeDD.Visible = false;
            modalDuplicate.Show();


            //Cheque DD Validation
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator6.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
            //  RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9454.Enabled = false;

            //NEFT validation
            RequiredFieldValidator10.Enabled = true;
            RequiredFieldValidator11.Enabled = true;
            RequiredFieldValidator12.Enabled = true;
            RequiredFieldValidator13.Enabled = true;
        }

    }


    ////protected void rdbInspectionPaymentType_Online_selectindexchanged(object sender, EventArgs e)
    ////{

    ////    int AgencyId = 0;
    ////   // DataSet dss = DataSetAgency_Details_ByUnitId(UnitId);
    ////  //  AgencyId = Convert.ToInt32(dss.Tables[0].Rows[0]["fGPtbl_PD_AgencyId"].ToString());

    ////       AgencyId = 10;
    ////    modalDuplicate.Show();
    ////    if (AgencyId == 9)
    ////    {
    ////        HyperLink1.Visible = true;
    ////        //  int a = Convert.ToInt32(rdl_inspectionmode_payment.SelectedItem.Value.ToString());
    ////        if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 1)
    ////        {
    ////            lbl_OnlineMsg.Visible = true;

    ////            lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Online_Payment_Form.aspx";

    ////            HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
    ////            HyperLink1.Target = "_blank";

    ////            lblOnlineNorthernMsg.Visible = true;
    ////            lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";

    ////        }
    ////        else if (Convert.ToInt32(rdbInspectionPaymentType_Online.SelectedItem.Value.ToString()) == 2)
    ////        {
    ////            lbl_OnlineMsg.Visible = true;


    ////            lbl_OnlineMsg.Text = "http://ritesinsp.com/rbs/Bank%20Details%20NR.PDF";

    ////            HyperLink1.NavigateUrl = lbl_OnlineMsg.Text.ToString();
    ////            HyperLink1.Target = "_blank";

    ////            lblOnlineNorthernMsg.Visible = false;
    ////            // lblOnlineNorthernMsg.Text = "Other region can use only NEFT/RTGS option for payment";

    ////        }
    ////    }

    ////    else
    ////    {
    ////        HyperLink1.Text = ""; lbl_OnlineMsg.Text.ToString();
    ////        HyperLink1.Target = "_blank";

    ////        HyperLink1.Visible = false;

    ////        lblOnlineNorthernMsg.Visible = true;
    ////        lblOnlineNorthernMsg.Text = "Northern region can use both Online Payment Gateway or NEFT/RTGS option for payment";
    ////        lblOnlineNorthernMsg.Visible = false;
    ////    }

    ////}

    private void funopen(string companyid)
    {
        string imageid = "RegistrationCertificatePageView.aspx?id=" + companyid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + imageid + "', null, 'height=700,width=760,status=yes,toolbar=yes,scrollbars=yes,menubar=no,resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }

    private void funopenDuplicate(string companyid)
    {
        string imageid = "RegistrationCertificatePageView_Dup.aspx?id=" + companyid;
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open( '" + imageid + "', null, 'height=700,width=760,status=yes,toolbar=yes,scrollbars=yes,menubar=no,resizable=yes,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);

    }

    [WebMethod]
    public static int CleanUncompletedata(int companyId, int fomtyeId)
    {
        int errorid = 0;
        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);

        try
        {
            SqlCommand cmd = new SqlCommand("GP_sp_Rollback_incompletetransaction", con);
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@companyId", SqlDbType.Int).Value = companyId;
            cmd.Parameters.Add("@FormtypeId", SqlDbType.Int).Value = fomtyeId;
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(outError);
            cmd.ExecuteNonQuery();
            errorid = Convert.ToInt32(outError.Value);
            con.Close();
            con.Dispose();
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError(EX.Message);

        }
        finally
        {
            con.Close();
            con.Dispose();

        }
        return errorid;

    }


    protected void lnk_show_Click(object sender, EventArgs e)
    {
        hid_val.Value = "1";
        txtSearchbox.Text = "";
        SearchGridviewBind();
    }


    public DataTable dt_fetch_companyrecord201415(int companyid)
    {
        DataTable dt = new DataTable();
        SqlDataAdapter sqldap = new SqlDataAdapter();
        try
        {

            sqldap = new SqlDataAdapter("gp_sp_Companyid201415which_not_take_print", DBManager.ConnectToDB());
            sqldap.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqldap.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
            sqldap.Fill(dt);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            sqldap.Dispose();
        }
        return dt;
    }

    protected void btnUpdateUdyam_Click(object sender, EventArgs e)
    {
        try
        {
            SqlCommand MySqlComm;
            MySqlComm = new SqlCommand("sp_UpdateUdyam", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int, 500).Value = HiddenFieldCompanyID.Value;
            MySqlComm.Parameters.Add("@Udyam", SqlDbType.VarChar, 500).Value = TextBoxUdyam.Text;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int, 500).Value = UserId;

            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
        }
    }
}
