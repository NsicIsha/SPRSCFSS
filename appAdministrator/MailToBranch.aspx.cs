using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Web.Mail;

public partial class appAdministrator_MailToBranch : System.Web.UI.Page
{
    clsScheduleMail objschudul;
    DataTable dt;
    DataSet ds;
    string mailto=string.Empty;
    string mailfrom=string.Empty;
    string MailSubject = string.Empty;
    string MailBody=string.Empty;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string mailtoString;
    string Mailto_One;


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
        try
        {
            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());

        }
        catch (Exception ex)
        {
            UserId = 11;
            RoleId = 8;
        }
      //  bindData();
    }

    //public void bindData()
    //{
    //    ds = new DataSet();
    //    objschudul=new clsScheduleMail();
    //    ds = objschudul.Branch_Send_mail();
    //    dt=new DataTable();
    //    dt = ds.Tables[0];
    //    if (dt.Rows.Count > 0)
    //    {
    //        DataTable dtcopytable = new DataTable();
    //        dtcopytable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["RoleId"].ToString()) == RoleId).CopyToDataTable();

    //        if (dtcopytable.Rows.Count > 0)
    //        {
    //            var qu = from n in dtcopytable.AsEnumerable()
    //                     select new
    //                     {
    //                         mailtoString = n.Field<string>("fGPtbl_TIAB_EmailId") + ',',
    //                     };
    //            foreach (var onl in qu)
    //            {
    //                Mailto_One += onl.mailtoString.ToString();
    //            }

    //            string MailToAll = Mailto_One.ToString();
    //            mailto = MailToAll.Substring(0, MailToAll.Length - 1);
    //            mailfrom = dtcopytable.Rows[0]["fGPtbl_MU_Email"].ToString();
    //            sendmail();

    //        }
    //    }
    //    else
    //    {
    //        ds.Dispose();
    //        dt.Dispose();
            
    //    }



    //}


    // Sending Mail 
    //public void sendmail()
    //{
    //    MailMessage msgMail = new MailMessage();
    //    clsMasterMailer objMailer = new clsMasterMailer();
    //    MailSubject = "Not  Approved date limit";
    //    msgMail.BodyFormat = MailFormat.Html;
    //    string strBody = "<html><body><b>Dear User</b>" + "<br/>" +
    //        "<font color=\"red\"> You are Not approved Day Limit</font> <br/> Thanks& Regards</body></html>";
    //    msgMail.Body = strBody;
    //   // SendMail.Mail_WithOUTAttachment(mailto, UserId, 0, mailfrom, 0, 0, null, null, MailSubject, strBody, null, 0, null, null, null);
    //    objschudul = new clsScheduleMail();
    //    int result = objschudul.Mail_WITHAttachmentPractic(MailSubject, strBody, mailfrom  , mailto);

    //   if (result == 1)
    //   {
    //       Response.Write("Not Send Mail Plese you use hard code!!!");
    //   }
           
    //}

  
}
