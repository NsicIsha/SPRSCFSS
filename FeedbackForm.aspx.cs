using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FeedbackForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
    }

    private void EmailFeedback()

    {
        //create the mail message
        MailMessage mail = new MailMessage();
        //set the addresses
        mail.From = new MailAddress(txtEmailID.Text.Trim());
        mail.To.Add("ctms@nsic.co.in");
       // mail.To.Add("ishasingh@nsic.co.in");
        //sprsonlinesupport @nsic.co.in



        //set the content
        mail.Subject = "Feedback Form";
        //Body to be displayed
        mail.Body = "<h2>" + "Feedback from " + " " + txtName.Text + "</h2>" + "<br><br>" +

                "Subject: " + txtSubject.Text + "<br>" +

                "Email : " + txtEmailID.Text.Trim() + "<br>" +

                "has raised issue stating that: " + txtMessage.Text.Trim() + "<br>" +
                "Thank You";
        mail.IsBodyHtml = true;
        // smtp settings

        SmtpClient smtp = new SmtpClient("mail.csharpexample.com");
        NetworkCredential SMTPUserInfo = new NetworkCredential("anwar@csharpexample.com", "Aic12234@");
        smtp.Credentials = SMTPUserInfo;
        smtp.Send(mail);
    }



    private void Clear()
    {
        txtName.Text = string.Empty;
        txtPhone.Text = string.Empty;
        txtSubject.Text = string.Empty;
        txtWebsite.Text = string.Empty;
        txtEmailID.Text = string.Empty;
        txtMessage.Text = string.Empty;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //EmailFeedback();
        //lblMsg.Visible = true;
        //lblMsg.ForeColor = System.Drawing.Color.Green;

        //Clear();

        //lblMsg.Text = "Thank You for Your Inquiry";
    }
}