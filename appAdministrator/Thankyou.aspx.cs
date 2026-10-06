using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;
using System.Net;

public partial class appAdministrator_Thankyou : System.Web.UI.Page
{
    int UserId, RoleId,Companyid;

    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string sub;
    string BranchName = string.Empty, CompanyName=string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            DecryptEncrypt objencrypt = new DecryptEncrypt();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            if (Request.QueryString["id"] != null)
            {
                Companyid = Convert.ToInt32(objencrypt.Decrypt(Request.QueryString["id"].ToString()));
            }
        }
        catch {}
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        string msg;
      if (Session["ReferenceNo"].ToString() == "1")
        {
            msg = "Ammendment form successfully submited<br>";
            lblMsg.Text = msg;
        }
      else if (Session["ReferenceNo"].ToString() == "2")
      {
          msg = "Renewal form successfully submited<br>";
          lblMsg.Text = msg;
      }
      else if (Session["ReferenceNo"].ToString() == "8" || Session["ReferenceNo"].ToString() == "7")
      {
          lblMsg.Text = "Application data successfully updated";
      }
      

      else
      {
          msg = "Thank You for registering with us.<br>";
          msg = msg + "Your unique reference no. is <i>'" + Session["ReferenceNo"].ToString() + "'</i><br>";
          msg = msg + "Please remember this for future use";

          lblMsg.Text = msg;
      }

      clsMasterBranch objBranch = new clsMasterBranch();
      DataTable Mydt = new DataTable();
      Mydt = objBranch.Fetch_BranchAddress_ByCompanyId(Companyid);
      try
      {
          if (Mydt.Rows.Count > 0)
          {
              ltlBranchAddress.Text = Mydt.Rows[0]["ADDRESS"].ToString();
              ltlBranchPhone.Text = Mydt.Rows[0]["PHONE"].ToString();
              ltlBranchFax.Text = Mydt.Rows[0]["FAXNO"].ToString();
              ltlBranchEmail.Text = Mydt.Rows[0]["EMAILID"].ToString();
              BranchName = Mydt.Rows[0]["BranchName"].ToString();
              CompanyName = Mydt.Rows[0]["CompName"].ToString();
          }
          if (RoleId == 7 || RoleId==0)
          {
              SendMail();
          }
      }
      catch (Exception ex)
      {
          ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
      }

        
    }//page load
    public void SendMail()
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailTo = ltlBranchEmail.Text;// txtTo.Text.ToString();
            MailFrom = SmtpUser;// "itsupport@nsic.co.in";

            MailSubject = "New company has applied for SPR certificate.";//txtSub.Text;

            MailBody = " Dear " + BranchName.ToString() + " Branch user, <br/><br/>A new company " + CompanyName
                +" has applied for SPR certificate.<br/>Please login at http://www.nsicspronline.com/appadministrator/login.aspx to process it."+
                "<br/><br/>Thanks & regards<br/>SPR Administrator.";            
                   
                                // ((txtbody.Text.Replace(Environment.NewLine, "<br />")));
            MailMessage message = new MailMessage(MailFrom, MailTo, MailSubject, MailBody);
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            smtpClient.Send(message);
            //INSERT INTO SENT MAILER (MAY BE SOME INTEGER VALUE ARE WRONG BUT MAIL BODY COULD GUIDE)
            clsDDL.Insert_MailDetails(MailTo, 1, 1, MailFrom, 3, 1, MailSubject, MailBody, 3);

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Send SuccessFully to Branch....'); window.opener.location.reload(true); window.close();</script>");
        }
        catch (Exception ex)
        {
            // ex.Message = "Server Busy.....................";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Your Mail Does Not Send SuccessFully. Because Server Busy ..........'); window.opener.location.reload(true); window.close();</script>");
        }
    }
  }

