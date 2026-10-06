using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ZXing;

public partial class appAdministrator_Default : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    clsCompany objclsCompany; 
    int UnitTotal = 0, LiveTotal = 0, ExpiredTotal = 0, UnderprocessTotal = 0;
    protected void Page_Load(object sender, EventArgs e)
    {     
    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        GenerateCode("http://www.nsicspronline.com/appAdministrator/RegistrationCertificatePageView.aspx?id=2220");
    }
    protected void btnRead_Click(object sender, EventArgs e)
    {
        ReadQRCode();
    }
    // Generate QRCode
    private void GenerateCode(string name)
    {
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        var result = writer.Write(name);
        string path = Server.MapPath("~/images/QRImage.jpg");
        var barcodeBitmap = new Bitmap(result);
        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        imgQRCode.Visible = true;
        imgQRCode.ImageUrl = "~/images/QRImage.jpg";

    }
    // Read Code from QR Image
    private void ReadQRCode()
    {
        var reader = new BarcodeReader();
        string filename = Path.Combine(Request.MapPath("~/images"), "QRImage.jpg");
        // Detect and decode the barcode inside the bitmap
        var result = reader.Decode(new Bitmap(filename));
        if (result != null)
        {
            lblQRCode.Text = "QR Code: " + result.Text;
        }
    }

    //protected void btnBackView_Click(object sender, EventArgs e)
    //{
    //    //ExportGridToExcel();
    //}

    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    /* Verifies that the control is rendered */
    //}

    //public static void ExportDataSetToExcel(DataTable dsInput)
    //{
    //    try
    //    {
    //        HttpResponse response = HttpContext.Current.Response;

    //        // first let's clean up the response.object 
    //        response.Clear();
    //        response.Charset = "";

    //        // set the response mime type for excel 
    //        response.ContentType = "application/vnd.ms-excel";
    //        response.AddHeader("Content-Disposition", "attachment;filename=\"" + "fddsfdsf.xls" + "\"");
    //        response.Cache.SetCacheability(HttpCacheability.NoCache);



    //        GridView dg1 = new GridView();
    //        dg1.DataSource = dsInput;
    //        dg1.DataBind();


    //        // create a string writer 
    //        using (StringWriter sw = new StringWriter())
    //        {
    //            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
    //            {
    //                // instantiate a datagrid

    //                htw.Write("<table><tr><td colspan='9' align='center'>" + "jj" + "</td></tr></table>");
    //                htw.Write("<table><tr><td colspan='9'></td></tr></table>");
    //                dg1.RenderControl(htw);
    //                response.Write(HttpUtility.HtmlDecode(sw.ToString()));
    //                //response.End();
    //                response.Close();
    //            }

    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }

    //}
    //protected void btnBackView_Click(object sender, EventArgs e)
    //{
    //    objclsCompany = new clsCompany();
    //    DataTable dt = new DataTable();
    //    dt = objclsCompany.Bind_Misc_data().Tables[0];
    //    ExportDataSetToExcel(dt);
    //}
}