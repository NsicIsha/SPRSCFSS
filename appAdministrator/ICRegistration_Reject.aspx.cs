using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_ICRegistration_Reject : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    clsCompany objCompany;
    DataSet Myds;
    int successId;


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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            Myds = new DataSet();
            int formtypeid, cunit, companyid;
            string FromMail, ccMail, bccMail, subject, bodyMail, companymail;

            //ACCESSING FORMTYPEID, COMPANY UNIT ID, COMPANY EMAIL
            companyid = Convert.ToInt32(Request.QueryString["cid"]);

            Myds = objCompany.Company_WaitingCompany_Registration_FetchByCompanyId_Dataset(companyid);
            formtypeid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_FormtypeId"]);
            cunit = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);
            companymail = Myds.Tables[0].Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();

            successId = objCompany.Company_ICompleteRegistration_Rejection(cunit, formtypeid, txtReason.Text, UserId);

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
                Session["icregAction"] = "Status changed successfully!";
                
            }
            else
            {
                Session["icregAction"] = "Server is busy!";
            }
            Response.Redirect("ICRegistration.aspx", false);
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
            Response.Redirect("ICRegistration.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


}
