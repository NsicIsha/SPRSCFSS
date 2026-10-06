using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Online_Payment_Reject_Fresh : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds;
    int count = 1;
    int successId, urlId = 0, amendmentid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    protected void Page_Load(object sender, EventArgs e)
    {
        //txtInstr.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 8)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
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
    //TO MAKE ENTRIES IN REJECTION TABLE
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {

            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            int formtypeid, cunit, companyid;
            string FromMail, ccMail, bccMail, subject, bodyMail, companymail;

            //ACCESSING FORMTYPEID, COMPANY ID, COMPANY EMAIL
            cunit = Convert.ToInt32(Request.QueryString["uid"]);
            objCompany = new clsCompany();
            Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(cunit);


            if (urlId == 1)//FOR REGISTRATION
            {

            }
            else if (urlId == 3)//FOR AMENDMENT
            {
                objAmendment = new clsAmendment();
                //   Myds = objAmendment.CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(amendmentid);
            }

            //formtypeid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_FormtypeId"]);
            companyid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_Company_Id"]);
            companymail = Myds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();

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
                    Response.Redirect("WaitingCompanies.aspx?i=1", false);
                }
                else if (urlId == 3)
                {
                    Response.Redirect("WaitingCompanies.aspx?i=3", false);
                }
                else if (urlId == 2)
                {
                    Response.Redirect("Online_RenewalPaymentDetails.aspx?i=2", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            if (urlId == 1)
            {
                Response.Redirect("WaitingCompaniesPaymentDetailsOnline.aspx?i=1", false);
            }
            else if (urlId == 3)
            {
                Response.Redirect("WaitingCompaniesPaymentDetailsOnline.aspx?i=3", false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void rdbRejectionType_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}