using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_ZHMonetary_limit_renewal_amendment : System.Web.UI.Page
{

    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    string UserFullName, password;
    public int monetzonalAction = 0;
    int successId, amendmentid = 0, urlid;
    string ToMail = string.Empty;
    string FromMail = string.Empty;
    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    string UserEmailID = string.Empty;
    Hashtable hashtbl = new Hashtable();

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();

            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 2 || RoleId == 3 || RoleId == 5)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlid = Convert.ToInt32(Request.QueryString["i"].ToString());
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
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        objCompany = new clsCompany();
        Myds = new DataSet();
        try
        {
            if (urlid == 1)
            {
                Myds = objCompany.Company_MonetaryLimit_FetchZonalHeadRegistration_Dataset(UserId, RoleId, DepartmentId);
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }
            else if (urlid == 2)
            {
                clsRenewal objrenew = new clsRenewal();
                Myds = objrenew.Company_MonetaryLimit_FetchZonalHeadRenew_Dataset(UserId, RoleId, DepartmentId);
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }
            else if (urlid == 3)
            {
                objAmendment = new clsAmendment();
                Myds = objAmendment.CompanyAmendment_MonetaryLimit_FetchZonalHead_Dataset(UserId, RoleId,DepartmentId);

                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }

            if (Myds.Tables[0].Rows.Count == 0)
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
            if (Session["monetzonalAction"] != null)
            {
                LabelAction.Text = Session["monetzonalAction"].ToString();
                monetzonalAction = 1;
                Session["monetzonalAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                monetzonalAction = 0;
                Session["monetzonalAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["monetzonalAction"] = null;
        }
    }

    protected void gvMonetaryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            //THIS IS FOR SETTING MONETARY LIMIT
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Set Monetary Limit";

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();

                Monetarydetail(Convert.ToInt32(e.CommandArgument));

                HDCOmpanyid.Value = Convert.ToString(e.CommandArgument);

                //TO CHECK PROVISIONAL COMPANY
                objCompany = new clsCompany();
                int provisionalstatus = objCompany.Company_Provisional_Status(Convert.ToInt32(HDCOmpanyid.Value));
                if (provisionalstatus == 1)
                {
                    disabletextbox();
                }

            }
            else if (e.CommandName == "accept")
            {
                //  Monetarydetail(Convert.ToInt32(e.CommandArgument));

                //TO UPDATE INTO MONETARY LIMIT
                //inserttomonetary();
                objCompany = new clsCompany();
                int comidentity = Convert.ToInt32(e.CommandArgument);

                if (urlid == 1)
                {
                   // password = objCompany.Company_InsertUpdate_Zone(comidentity, "", UserId);


                }
                else if (urlid == 2)
                {
                    clsRenewal objrenew = new clsRenewal();
                    password = objrenew.Company_InsertUpdate_Zone(comidentity, "", UserId);


                }
                else if (urlid == 3)
                {
                    password = objCompany.Company_InsertUpdate_ZoneAmedment(comidentity, UserId);
                }
                //HDCOmpanyid.Value = Convert.ToString( e.CommandArgument);
                sendmailcompany(comidentity);
            }



        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        GridViewDataBind();
    }

    //TO FETCH ALL DETAILS OF MONETARY LIMIT
    public void Monetarydetail(int monetaryid)
    {
        try
        {
            objCompany = new clsCompany();
            //objAmendment = new clsAmendment();
            Myds = new DataSet();
            if (urlid == 1)
            {
                Myds = objCompany.Company_MonetaryLimit_FetchByCompId_Dataset(monetaryid);
            }
            if (urlid == 2)
            {
                clsRenewal objrenew = new clsRenewal();

                Myds = objrenew.Company_MonetaryLimit_FetchByCompId_Dataset(monetaryid);
            }
            else if (urlid == 3)
            {
                Myds = objCompany.Company_MonetaryLimit_FetchByCompId_Dataset_Amedment_Process(monetaryid);
            }

            if (Myds.Tables[0].Rows.Count > 0)
            {
                grdTurnOver.DataSource = Myds;
                grdTurnOver.DataBind();
            }
            else
            {
                grdTurnOver.Dispose();
                grdTurnOver.DataBind();
                Myds.Dispose();
            }

            //HiddenFieldUnitid.Value = Myds.Tables[0].Rows[0]["fGPtbl_Company_ML_CompanyId"].ToString();
            //HFieldformtypeid.Value = Myds.Tables[0].Rows[0]["fGPtbl_Company_ML_FormtypeId"].ToString();
            //HFMonetaryid.Value = Myds.Tables[0].Rows[0]["fGPtbl_Company_ML_Id"].ToString();
            //HFMonetarylimit.Value = Myds.Tables[0].Rows[0]["fGPtbl_Company_ML_MonetoryLimit"].ToString();


            //amendmentid = objAmendment.CompanyAmendment_WaitingCompany_FetchAmendmentId(Convert.ToInt32(HiddenFieldUnitid.Value));
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
            inserttomonetary();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public void inserttomonetary()
    {
        try
        {
            objAmendment = new clsAmendment();
            objCompany = new clsCompany();
            int cnt, i;
            string strlimit, strmonetary, strunt;
            strlimit = "";
            cnt = grdTurnOver.Rows.Count;
            i = 0;
            while (i < cnt)
            {
                TextBox txtlimit = (TextBox)grdTurnOver.Rows[i].FindControl("txt_limit");
                HiddenField hdnunt = (HiddenField)grdTurnOver.Rows[i].FindControl("hdnuntid");
                strmonetary = txtlimit.Text.Trim();
                strunt = hdnunt.Value;


                if (i == 0)
                {
                    strlimit = strunt + "," + strmonetary;
                }
                else
                {
                    strlimit = strlimit + ":" + strunt + "," + strmonetary;

                }
                i = i + 1;

            }

            if (urlid == 1)
            {
                //password = objCompany.Company_InsertUpdate_Zone(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                //if (password != "" || password != null)
                //    successId = 2;
                ////successId = objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(1, "", "", "", strlimit, UserId, RoleId);
            }
            if (urlid == 2)
            {
                clsRenewal objrenew = new clsRenewal();
                password = objrenew.Company_InsertUpdate_Zone(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                if (password != "" || password != null)
                    successId = 2;
                //successId = objCompany.Company_InsertUpdate_TurnoverMonetaryLimit(1, "", "", "", strlimit, UserId, RoleId);
            }
            if (urlid == 3)
            {
                successId = objAmendment.CompanyAmendment_BranchAccount_ChangecompanyStatus(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
            }

            switch (successId)
            {
                case 1://DATA ALREADY EXISTS
                    Session["monetzonalAction"] = "Successfully updated";
                    sendmailcompany(Convert.ToInt32(HDCOmpanyid.Value));
                    break;
                case 2://SUCCESSFULLY UPDATED
                    Session["monetzonalAction"] = "Successfully updated";
                    sendmailcompany(Convert.ToInt32(HDCOmpanyid.Value));
                    break;
                case 3://ERROR IN OPERATION
                    Session["monetzonalAction"] = "Server is busy";
                    break;
                default://ERROR IN OPERATION
                    Session["monetzonalAction"] = "Server is busy";
                    break;
            }


            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    //TO SEND MAIL TO COMPANY
    public void sendmailcompany(int compIdentity)
    {
        try
        {
            //string ToMail, FromMail, ccMail, bccMail, subject, bodyMail;
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            clsMasterBranch objBranch = new clsMasterBranch();

            DataSet Myds = new DataSet();
            DataTable Mydt, MydtBranchEmail = new DataTable();
            MydtBranchEmail = objBranch.Fetch_BranchAddress_ByCompanyId(compIdentity);

            //TO BRANCH ACCOUNT MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(2); //Change Here
            //FromMail = UserEmailID.ToString(); //Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            if (MydtBranchEmail.Rows.Count > 0)
            {
                FromMail = MydtBranchEmail.Rows[0]["EMAILID"].ToString();
            }
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            string bodyMail_Data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            string str = string.Empty;

            //objCompany = new clsCompany();
            //TO FETCH COMPANY E-MAIL
            Mydt = objCompany.Company_Detail_Fetch_ById(compIdentity);

            if (Mydt.Rows.Count > 0)
            {
                str = bodyMail_Data.Replace("$$CompanyName$$", Mydt.Rows[0]["fGPtbl_Company_Username"].ToString());
                str = str.Replace("$$username$$", Mydt.Rows[0]["fGPtbl_Company_Username"].ToString());
                str = str.Replace("$$password$$", Mydt.Rows[0]["fGPtbl_Company_Pwd"].ToString());
                str = str.Replace("$$url $$", "http://www.nsicspronline.com/appCompany/CompanyLogin.aspx");
                ToMail = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString(); //"anupam.sen@Nagsoft.com";
            }
            else
            {
                str = "";
            }
            string UserName_Data = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();
            bodyMail = "Dear" + ' ' + UserName_Data + str;
            sendmail(FromMail, ToMail, bccMail, ccMail, subject, bodyMail);
            // SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //TO DISABLE TEXT BOXES IN CASE OF PROVISIONAL COMPANY
    private void disabletextbox()
    {
        foreach (GridViewRow mrow in grdTurnOver.Rows)
        {
            TextBox txtcon;
            txtcon = (TextBox)mrow.FindControl("txt_limit");
            txtcon.Enabled = false;
            btnSubmit.Enabled = false;
        }
    }

    // Send Mail 
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
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);

            //INSERT INTO SENT MAILER
            clsDDL.Insert_MailDetails(MailTo, 7, 3, MailFrom, 1, 1, subject, bodyMail, 2);


            // smtpClient.Send(message);
            // smtpClient.Timeout = 500;




        }
        catch (Exception ex)
        {
            //ex.Message;
        }
    }



}
