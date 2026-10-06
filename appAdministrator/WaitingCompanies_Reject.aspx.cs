using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Collections;
public partial class appAdministrator_WaitingCompanies_Reject : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;


    int count = 1;
    int successId, urlId = 0, amendmentid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string fromAddress;
    string toAddress;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string strOriginalMailBody = string.Empty, strMailBody = string.Empty;
    string Mailto_One, strExpiringDate = string.Empty;

    string MailBodyBranch = string.Empty;
    string strOriginalMailBodyBranch = string.Empty, strMailBodyBranch = string.Empty;
    string MailSubjectBranch = string.Empty;


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
        //txtInstr.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 8)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
                }

                if(urlId==3 || urlId==2)
                {
                    trRejectionReason.Visible = false;
                }
                else
                {
                    trRejectionReason.Visible = true;
                }

                if (Request.QueryString["aid"] != null)
                {
                    amendmentid = Convert.ToInt32(Request.QueryString["aid"].ToString());
                }

                if (Page.IsPostBack != true)
                {
                    txtReason.Visible = true;
                    btnSubmit.Visible = true;
                }
            }
            else
            {
                txtReason.Visible = false;
                btnSubmit.Visible = false;
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    public static string SendMailToUnit(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailfrom, "Support-SinglePointRegistration");
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));
            // message.Bcc.Add(new MailAddress("ctms@nsic.co.in"));
           // message.Bcc.Add(new MailAddress("itsupport2@nsic.co.in"));
            //'Isha Singh' <>

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;587;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);

            return "1";

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
            return ex.Message;
        }
    }
    public DataSet Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_Waiting_Fetch_CUnit_ByCompanyid_Online", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = Companyid;

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

    public void SendEmail(int CompanyId, string Reason, string RejectedFileName)
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();

        DataSet MyDsBranch = new DataSet();

        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        //  hashtbl = objCompany.Company_ForgotPassword(txtUserNameFGP.Text, "none");

        MyDs = objMailer.MasterMailer_Fetch_DataSet(29);
        string CompName = string.Empty, BrName = string.Empty, BrAddress = string.Empty, BrPhone = string.Empty, BrEmail = string.Empty;

        strOriginalMailBody = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
        MailFrom = MyDs.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        MailSubject = MyDs.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
        System.Data.DataTable MyDt = new System.Data.DataTable();
        DataSet dss = new DataSet();
        dss = Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(CompanyId);
        MyDt = dss.Tables[0];


        ////MailTo = Convert.ToString("sdasad");
        //////strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
        ////strMailBody = strOriginalMailBody.Replace("$$Reason$$", Reason);

        ////strMailBody = strOriginalMailBody.Replace("$$RejectedDocument$$", RejectedFileName);

        ////// BrEmail = "itsupport2@nsic.co.in";
        ////if (MailTo != "")
        ////{
        ////    SendMailToUnit(MailTo, strMailBody, MailFrom, MailSubject);
        ////}



        if (MyDt.Rows.Count > 0)
        {
            foreach (DataRow drIA in MyDt.Rows)
            {
                MailTo = Convert.ToString(drIA["fGPtbl_Company_HeadOffice_Email"]);
                //strMailBody = strOriginalMailBody.Replace("$$branchname$$", BrName);
                strMailBody = strOriginalMailBody.Replace("$$Reason$$", Reason);

                strMailBody = strMailBody.Replace("$$RejectedDocument$$", RejectedFileName);

                // BrEmail = "itsupport2@nsic.co.in";
                if (MailTo != "")
                {
                    SendMailToUnit(MailTo, strMailBody, MailFrom, MailSubject);
                }
            }//foreach
        }//if

    }

    public int Company_Update_Unit_DocumentUpload_Fresh_Rejection(int CompanyId)
    {
        int ErrorId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Unit_DocumentUpload_Fresh_Rejection", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;


            ErrorId = MySqlComm.ExecuteNonQuery();
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

    //TO MAKE ENTRIES IN REJECTION TABLE
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (rdbRejectionType.SelectedValue == "2")
        {
            int CunitId = 0;
            int FormType = 0;
            int fGPtbl_DU_Id = 0;
            string fGPtbl_DU_Name = "";
            string fGPtbl_DU_FileName = "";
            string fGPtbl_DU_FilePath = "";
            string RejectionReason = "";
            int CompanyId = 0;

            string RejectedFileName = "";
            CompanyId = Convert.ToInt32(Request.QueryString["uid"]);

            //CompanyId = 111;
            RejectionReason = txtReason.Text;
            foreach (GridViewRow gridrows in gvDocumentDetail.Rows)
            {
                CheckBox chkbox = (CheckBox)gridrows.FindControl("chkRow");
                if (chkbox != null & chkbox.Checked)
                {
                    CunitId = Convert.ToInt32(gvDocumentDetail.DataKeys[gridrows.RowIndex].Value.ToString());
                    FormType = Convert.ToInt32(gridrows.Cells[2].Text);
                    fGPtbl_DU_Id = Convert.ToInt32(gridrows.Cells[3].Text);

                    fGPtbl_DU_FileName = gridrows.Cells[4].Text;
                    fGPtbl_DU_FilePath = gridrows.Cells[5].Text;
                    fGPtbl_DU_Name = gridrows.Cells[6].Text;

                    if(RejectedFileName!="")
                    {
                        RejectedFileName = fGPtbl_DU_Name;
                    }
                    else
                    {
                        RejectedFileName += "<br>" + fGPtbl_DU_Name;
                    }

                    RejectionReason = txtReason.Text;
                    int errorid = Company_Certificate_Insert(CompanyId, CunitId, FormType, fGPtbl_DU_Id, fGPtbl_DU_Name, fGPtbl_DU_FileName, fGPtbl_DU_FilePath, RejectionReason);
                }
            }

            #region Email
            SendEmail(CompanyId, RejectionReason, RejectedFileName);

            int aa = Company_Update_Unit_DocumentUpload_Fresh_Rejection(CompanyId);
            #endregion

        }



        try
        {

            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            int formtypeid, cunit, companyid;
            string FromMail, ccMail, bccMail, subject, bodyMail, companymail;

            //ACCESSING FORMTYPEID, COMPANY ID, COMPANY EMAIL
            cunit = Convert.ToInt32(Request.QueryString["uid"]);

            objCompany = new clsCompany();


            if (urlId == 1)//FOR REGISTRATION
            {
               // objCompany = new clsCompany();
                Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(cunit);
            }
            else if (urlId == 3)//FOR AMENDMENT
            {
                objAmendment = new clsAmendment();
                // Myds = objAmendment.CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(amendmentid);
            }

            //formtypeid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_FormtypeId"]);
            companyid = Convert.ToInt32(Request.QueryString["uid"]);// Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_Company_Id"]);
            companymail = "";// Myds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
            
            successId = objCompany.Company_WaitingCompany_Registration_Rejection_(cunit, urlId, txtReason.Text, UserId);

            //TO FETCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(1);
            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString() + txtReason.Text + ".";

            if (successId == 2)
            {
                SendMail.Mail_WithOUTAttachment(companymail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
                Session["companyAction"] = "Status changed successfully!";
                if (urlId == 1)
                {
                    Response.Redirect("WaitingCompaniesOnline.aspx?i=1", false);
                }
                else if (urlId == 3)
                {
                    Response.Redirect("WaitingCompaniesOnline.aspx?i=3", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public int Company_Certificate_Insert(int CompanyId, int CunitId, int FormType, int fGPtbl_DU_Id, string fGPtbl_DU_Name, string fGPtbl_DU_FileName, string fGPtbl_DU_FilePath, string RejectionReason)
    {
        int ErrorId = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_Insert_DocumentVerifyDetails_Fresh", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@CunitId", SqlDbType.Int).Value = CunitId;
            MySqlComm.Parameters.Add("@fGPtbl_DU_Id", SqlDbType.Int).Value = fGPtbl_DU_Id;
            MySqlComm.Parameters.Add("@FormType", SqlDbType.Int).Value = FormType;
            MySqlComm.Parameters.Add("@fGPtbl_DU_Name", SqlDbType.VarChar, 7000).Value = fGPtbl_DU_Name;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FileName", SqlDbType.VarChar, 7000).Value = fGPtbl_DU_FileName;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FilePath", SqlDbType.VarChar, 7000).Value = fGPtbl_DU_FilePath;
            MySqlComm.Parameters.Add("@RejectionReason", SqlDbType.VarChar, 7000).Value = RejectionReason;
            ErrorId = MySqlComm.ExecuteNonQuery();
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
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (urlId == 1)
            {
                Response.Redirect("WaitingCompaniesOnline.aspx?i=1", false);
            }
            else if (urlId == 3)
            {
                Response.Redirect("WaitingCompaniesOnline.aspx?i=3", false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void rdbRejectionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbRejectionType.SelectedValue == "2")
        {
            trUploads.Visible = true;
            //BINDING UPLOADED GRIDVIEW
            int Companyid = Convert.ToInt32(Request.QueryString["uid"]);
            int Unitid = 0;
            clsCompany objCompany = new clsCompany();

            Myds = objCompany.Company_UnitDetails_Fetch_ByCompanyId(Companyid);
            //Mydt = Myds.Tables[0];

            //Mydt2 = Myds.Tables[2];
            //Mydt3 = Myds.Tables[3];

            //GETTING UNIT ID   // Comment by Ravish Kumar Pandey
            if (Myds.Tables[0].Rows.Count > 0)
            {
                if (Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString() != null)
                {
                    Unitid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);
                }
                else
                {

                }
            }
            gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid, 1);//formtypeId);
            gvDocumentDetail.DataBind();
        }
        else
        {
            trUploads.Visible = false;
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            MyDs = new DataSet();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");
                hlname.NavigateUrl = "../" + hfpath.Value.ToString() + hfname.Value.ToString();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        ////objCompany = new clsCompany();

        ////int i = (CountMasterDoc(Unitid) - 1);
        ////int j = e.Row.RowIndex;

        ////if (j == i)
        ////{
        ////    e.Row.Cells[1].FindControl("btnSubmit").Visible = true;
        ////    //e.Row.Cells[1].FindControl("btnBack").Visible = true;
        ////}
    }
}
