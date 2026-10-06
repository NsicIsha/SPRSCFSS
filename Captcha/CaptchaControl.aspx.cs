using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using Radix.ErrorLog;

public partial class CaptchaControl : System.Web.UI.Page
{
    Random rand = new Random();
    StringBuilder randomText = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)           //when request for new captcha 
            Session["Code"] = null;

        CreateImage();
    }

    private void CreateImage()
    {
        //try
        //{
            string code = GetRandomText();
            Bitmap bitmap = new Bitmap(150, 60,
                   System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            Pen pen = new Pen(Color.Blue);
            
            Rectangle rect = new Rectangle(0, 0, 150, 60);
            SolidBrush b = new SolidBrush(Color.LightBlue);
            SolidBrush blue = new SolidBrush(Color.DarkBlue);
            int counter = 0;
            g.DrawRectangle(pen, rect);
            g.FillRectangle(b, rect);

            for (int i = 0; i < code.Length; i++)
            {
                //g.DrawString(code[i].ToString(),new Font("ComicSans",10

                g.DrawString(code[i].ToString(),
                   new Font("Comic Sans MS", 8 + rand.Next(14, 18)),
                   blue, new PointF(10 + counter, 10));
                counter += 20;
            }

            Response.ContentType = "image/gif";
            //bitmap.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            bitmap.Save(Response.OutputStream, ImageFormat.Gif);
            g.Dispose();
            bitmap.Dispose();
        //}
        //catch (Exception ex)
        //{
        //    ApplicationError.LogErrors(ex); 
        //}

    }
    private string GetRandomText()
    {
        //try
        //{
        if (Session["Code"] == null)
       // if (ViewState ["Code"] == null)
        {
            string alphabets = "AB3C4DEFG6HJKLM5NPQR2STUV7WX8YZ9";
            Random r = new Random();

            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
                //randomText.Append(" ");
            }
            Session["Code"] = randomText.ToString();
           // ViewState ["Code"] = randomText.ToString();
        }
        return Session["Code"] as String;
        //return ViewState ["Code"] as String;
        //}
        //catch (Exception ex)
        //{
        //    ApplicationError.LogErrors(ex);
        //}
    }
}