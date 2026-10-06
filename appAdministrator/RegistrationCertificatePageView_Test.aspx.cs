using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;

public partial class appAdministrator_RegistrationCertificatePageView_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }
    protected void btnShow_OnClick(object sender, EventArgs e)

    {

        Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);

        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

        pdfDoc.Open();

        pdfDoc.Add(new Paragraph("Welcome to dotnetfox"));

        pdfDoc.Close();

        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition", "attachment;" +

                                       "filename=sample.pdf");

        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        Response.Write(pdfDoc);

        Response.End();

    }
}