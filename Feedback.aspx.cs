using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.IO;
using System.Net;
//using System.Web.Mail;
using System.Net.Mail; 
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml;


 



public partial class Feedback : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    string prevPage = String.Empty;
    string pagename;
    short tabindx = 1;
    int Primary_Id;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
     

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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
             

        }
    }

    protected void btnEnroll_Click(object sender, EventArgs e)
    {
        int POI_Id = Company_Insert_Feedback(txtName.Text, txtEmailid.Text,  txtPhone.Text, txtWebsite.Text,txtSubject.Text, txtMessage.Text, Convert.ToInt32(rbdUseful.SelectedValue), Convert.ToInt32(rbdRate.SelectedValue), Convert.ToInt32(rbdNodal.SelectedValue), Convert.ToInt32(rbdClear.SelectedValue), Convert.ToBoolean(rdbRecommend.SelectedValue));
       // SendEmail();


        ScriptManager.RegisterStartupScript(this.updSubmt, typeof(string), "Alert", "alert('Thanks for filling out our feedback form. We will look over your message and get back to you shortly.');", true);
        Clear();

    }

    public void SendEmail()
    {
        clsMasterMailer objMailer = new clsMasterMailer();
        DataSet MyDs = new DataSet();

        DataSet MyDsBranch = new DataSet();

        Hashtable hashtbl = new Hashtable();
        int successId;
        string FromMail, ccMail, bccMail, subject, bodyMail;

        

       

        string CompName = string.Empty, BrName = string.Empty, BrAddress = string.Empty, BrPhone = string.Empty, BrEmail = string.Empty;


        string AuthPName = string.Empty, AuthPMobile = string.Empty, AuthPEmail = string.Empty, ProductDetails = string.Empty;


        string body = string.Empty;
        using (StreamReader reader = new StreamReader(Server.MapPath("Mailor/Feedback.htm")))
        {
            body = reader.ReadToEnd();
        }

        body = body.Replace("$$AName$$",txtName.Text.ToString().Trim());
        body = body.Replace("$$Email$$", txtEmailid.Text.ToString().Trim());
        body = body.Replace("$$Mobile$$", txtPhone.Text.ToString().Trim());
        body = body.Replace("$$Website$$", txtWebsite.Text.ToString().Trim());
        body = body.Replace("$$Subject$$",txtSubject.Text.ToString().Trim());
        body = body.Replace("$$Message$$", txtMessage.Text.ToString().Trim());

        SendMailToBranch("sprs@nsic.co.in", body, txtEmailid.Text.ToString().Trim(), txtSubject.Text.ToString().Trim());

    }


    public static string SendMailToBranch(string tomail, string mailbody, string mailfrom, string mailsubject)
    {
        try
        {

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



            MailMessage message = new MailMessage();
            message.From = new MailAddress(mailfrom);
            // Set the recepient address of the mail message
            /*message.To.Add(new MailAddress("uttam.kumar@nagsoft.com"));
            message.Bcc.Add(new MailAddress("kuldeep.singh@nagsoft.com"));*/
            message.To.Add(new MailAddress(tomail));
             message.Bcc.Add(new MailAddress("ctms@nsic.co.in"));
            //message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));
            //'Isha Singh' <>

            message.Subject = mailsubject;
            // Set the body of the mail message
            message.Body = mailbody;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
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


    void Clear()
    {
        txtMessage.Text = string.Empty;
        txtEmailid.Text = string.Empty;
        txtName.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        rbdClear.SelectedIndex = -1;
        rbdNodal.SelectedIndex = -1;
        rbdRate.SelectedIndex = -1;
        rbdUseful.SelectedIndex = -1;
        rdbRecommend.SelectedIndex = -1;

    }
    public int Company_Insert_Feedback(string Name, string EmailId, string Phone, string Website, string Subject,  string Message,int useful,int Rate,int Nodal,int Clarity,bool Recommend)
    {
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Insert_Feedback_Details", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@Name", SqlDbType.VarChar, 5000).Value = Name;
            MySqlComm.Parameters.Add("@Email", SqlDbType.VarChar, 150).Value = EmailId;
            MySqlComm.Parameters.Add("@Phone", SqlDbType.VarChar, 150).Value = Phone;
            MySqlComm.Parameters.Add("@Website", SqlDbType.VarChar, 150).Value = Website;
            MySqlComm.Parameters.Add("@Subject", SqlDbType.VarChar, 500).Value = Subject;
            MySqlComm.Parameters.Add("@Message", SqlDbType.VarChar, 5000).Value = Message;
            MySqlComm.Parameters.Add("@Useful", SqlDbType.Int).Value = useful;
            MySqlComm.Parameters.Add("@Rate", SqlDbType.Int).Value = Rate;
            MySqlComm.Parameters.Add("@Nodal", SqlDbType.Int).Value = Nodal;
            MySqlComm.Parameters.Add("@Clarity", SqlDbType.Int).Value = Clarity;
            MySqlComm.Parameters.Add("@Recommend", SqlDbType.Bit).Value = Recommend;

            SqlParameter fGPtbl_POI_Id = new SqlParameter("@Id", SqlDbType.Int);
            fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(fGPtbl_POI_Id);
            MySqlComm.ExecuteNonQuery();
            Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
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

        return Primary_Id;
    }
}