using iTextSharp.text.pdf;
using iTextSharp.text;
using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Num2Wrd;
using Image = iTextSharp.text.Image;
using System.Drawing.Imaging;
//using System.Drawing;
using System.IO;
using ZXing;
using iTextSharp.text.pdf.codec;
using System.Text;
using System.Globalization;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;

public class companyDetails
{
    public string Validity { get; set; }
    public string CertificateKind { get; set; }
    public string SerialNo { get; set; }
    public string Reference { get; set; }
    public string DateOfIssue { get; set; }
    public string CompanyName { get; set; }
    public string PAN { get; set; }
    public string CompanyAddress { get; set; }
    public string BranchAddress { get; set; }
    public string BranchPhone { get; set; }
    public string BranchEmail { get; set; }
    public string BranchFax { get; set; }
    public string Website { get; set; }
    public List<string> FactoryAddress { get; set; }
    public List<string> Owners { get; set; }
    public string OwnerType { get; set; }
    public string CompanyConstitution { get; set; }
    public string CompanyUdyam { get; set; }
    public string SocialCategory { get; set; }
    public string SpecialCategory { get; set; }
    public string RegistrationNumber { get; set; }
    public string MLValue { get; set; }
    public string MLNumbers { get; set; }
    public string[] Turnover { get; set; }
    public string TotalStorecCount { get; set; }
    public string capacity { get; set; }
    public string FY1 { get; set; }
    public string FY2 { get; set; }
    public string FY3 { get; set; }

    public string Turnover1 { get; set; }
    public string Turnover2 { get; set; }
    public string Turnover3 { get; set; }

    public string Provisional { get; set; }

    public GridView grdStores { get; set; }

    public companyDetails()
    {
        FactoryAddress = new List<string>();
        Owners = new List<string>();
        Website = "www.nsic.co.in";
    }

}
public partial class appAdministrator_DownloadCertificate : System.Web.UI.Page
{
    public static string unitname, branchaddress, Email, fax, Website, phone, serialNo;
    public class Background : PdfPageEventHelper
    {
        // @Override
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            //PINK
            Rectangle rectangle1 = new Rectangle(document.PageSize);
            PdfContentByte canvas1 = writer.DirectContentUnder;
            float height = rectangle1.Height - (rectangle1.Height * 60 / 100);
            canvas1.Rectangle(0, 0, rectangle1.Width, height);
            canvas1.SetColorFill(new BaseColor(255, 223, 221));
            canvas1.Fill();
            //GREEN
            Rectangle rectangle2 = new Rectangle(document.PageSize);
            PdfContentByte canvas2 = writer.DirectContentUnder;
            float height2 = rectangle2.Height - (rectangle2.Height * 40 / 100);
            canvas2.Rectangle(0, height, rectangle2.Width, height2);
            canvas2.SetColorFill(new BaseColor(231, 244, 228));
            canvas2.Fill();

            //water mark
            int fontSize = 9;
            PdfContentByte wmark = writer.DirectContentUnder;
            BaseFont baseFont = BaseFont.CreateFont(BaseFont.COURIER, BaseFont.WINANSI, BaseFont.EMBEDDED);

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    wmark.BeginText();
                    if (j < 34)
                    {
                        wmark.SetColorFill(BaseColor.PINK);
                        wmark.SetFontAndSize(baseFont, fontSize);
                    }
                    else
                    {
                        wmark.SetColorFill(new BaseColor(141, 206, 129));
                        wmark.SetFontAndSize(baseFont, fontSize);
                    }

                    wmark.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "NSIC", i * 25, j * 10, 0);
                    wmark.EndText();
                }
            }
            //Close WaterMark
            //Add border to page
            PdfContentByte content = writer.DirectContent;
            Rectangle rectangle = new Rectangle(document.PageSize);
            content.SetLineWidth(2);
            content.SetColorStroke(BaseColor.BLACK);
            //content.Rectangle(rectangle.Left+5, rectangle.Bottom+5, PageSize.A4.Width-10, rectangle.Height-10);
            content.Rectangle(rectangle.Left, rectangle.Bottom, PageSize.A4.Width, rectangle.Height);
            content.Stroke();
            content.Fill();
            //Close border


            PdfPTable table = new PdfPTable(4);
            //float[] widths = new float[] { 10f, 10f, 10f,10f };  // Adjust column widths as necessary
            //table.SetWidths(widths);
            //table.WidthPercentage = 95;

            table.TotalWidth = 550f;
            table.LockedWidth = true;

            PdfPCell emptyCell = new PdfPCell();
            emptyCell.BorderWidth = 0;


            PdfPCell Logocell = ImageCell("~/Images/Logo.png", 5f, PdfPCell.ALIGN_LEFT);
            Logocell.BorderWidth = 0;
            Logocell.Colspan = 1;
            Logocell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            Logocell.VerticalAlignment = PdfPCell.ALIGN_TOP;

            PdfPCell Englishcell = ImageCell("~/Images/nsicImage.png", 30f, PdfPCell.ALIGN_CENTER);
            Englishcell.BorderWidth = 0;
            Englishcell.Colspan = 2;
            Englishcell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
            Englishcell.VerticalAlignment = PdfPCell.ALIGN_TOP;

            PdfPCell QRCell = ImageCell("~/images/QRImage.jpg", 50f, PdfPCell.ALIGN_RIGHT);
            QRCell.Colspan = 1;
            QRCell.BorderWidth = 0;
            QRCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            QRCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            table.AddCell(Logocell);
            table.AddCell(Englishcell);

            table.AddCell(QRCell);


            Phrase phrase = new Phrase();
            phrase.Add(new Phrase("(A Government Of India Enterprise)", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));
            PdfPCell cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            Phrase phraseCIN = new Phrase();
            phraseCIN.Add(new Phrase("CIN : U74140DL1955001002481", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));
            PdfPCell cellCIN = PhraseCell(phraseCIN, PdfPCell.ALIGN_CENTER);
            cellCIN.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cellCIN.Colspan = 4;
            cellCIN.BorderWidth = 0;
            table.AddCell(cellCIN);

            // Branch Address Details
            Phrase phrase_address = new Phrase();
            phrase_address.Add(new Phrase("Branch Office: " + appAdministrator_DownloadCertificate.branchaddress + "   Phone: " + appAdministrator_DownloadCertificate.phone, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            phrase_address.Add(new Phrase("\n" + "Email: " + appAdministrator_DownloadCertificate.Email + "  Fax: " + appAdministrator_DownloadCertificate.fax, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            phrase_address.Add(new Phrase("\n" + "Website: " + appAdministrator_DownloadCertificate.Website, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            PdfPCell cell1 = new PdfPCell(phrase_address);
            cell1.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell1.Colspan = 4;
            cell1.BorderWidth = 0;
            cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER; // Ensure text is centered
            table.AddCell(cell1);

            // Add the table to the document
            table.WriteSelectedRows(0, -1, 10, document.PageSize.Height - 70, writer.DirectContent);
            var FooterCompanyName = new Phrase();
            FooterCompanyName.Add(new Chunk(appAdministrator_DownloadCertificate.unitname, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

            PdfPCell CompanyNameCell = new PdfPCell(FooterCompanyName);
            CompanyNameCell.Colspan = 2;
            CompanyNameCell.BorderWidth = 0;
            CompanyNameCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

            PdfPTable Footertable = new PdfPTable(4);
            Footertable.TotalWidth = 550f;
            Footertable.LockedWidth = true;

            phrase = new Phrase();
            phrase.Add(new Phrase("Authenticity of the certificate can be checked through the webportal : www.nsicspronline.com", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 2;
            cell.BorderWidth = 0;

            Footertable.AddCell(CompanyNameCell);
            Footertable.AddCell(cell);

            Footertable.WriteSelectedRows(0, -1, 20, 30, writer.DirectContent);
        }

    }
    clsCompany objCompany;
    companyDetails objCompanyDetails;
    string[] Name;
    string[] SI;
    string NameOfPartner;
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 20;
    int UserID, BranchId, ComID;
    string SerialNumber;
    string RegistrationNumber = string.Empty;
    public string SerialNoAdd;
    string Branch_Code = string.Empty;
    string RegistrationDatefrom = "";
    public int FormtypeID { get; set; }

    string AmedmentChekRenewal = "";
    int UserId, RoleId, DepartmentId;
    string UserFullName, UserEmailID;
    int CompanyId;
    private string serial_NUMBER
    {
        get;
        set;
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            UserID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).Replace(" ", "+")).ToString();

        }
        catch
        {
            UserID = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
        }
    }

    public DataSet BranchIdbyCompanyId(int Companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_BranchId_by_CompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@Companyid", SqlDbType.Int).Value = Companyid;
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


    public DataSet BranchCodebybranchId(int BranchId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_BranchCode_by_branchId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (BranchId != 0) MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = BranchId;
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
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{

        FunRequest();
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["EncryptCompany_id"]).ToString().Replace(" ", "+")).ToString());
        DataSet dsBranchid = new DataSet();
        dsBranchid = BranchIdbyCompanyId(CompanyId);
        BranchId = Convert.ToInt32(dsBranchid.Tables[0].Rows[0]["fGPtbl_CUnit_BranchId"].ToString());

        DataSet dsBCode = new DataSet();
        dsBCode = BranchCodebybranchId(BranchId);
        Branch_Code = dsBCode.Tables[0].Rows[0]["fGPtbl_MB_Code"].ToString();



        //BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        //Branch_Code = objDecEnc.Decrypt(Request.QueryString["d"].Replace(" ", "+")).ToString();
        objCompany = new clsCompany();
        Hashtable hashtbl = new Hashtable();
        ComID = CompanyId;// Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
        hashtbl = objCompany.Company_Fetch_RegNo_ByCompanyId(ComID);
        RegistrationNumber = hashtbl["regno"].ToString();
        int prntaccess = Convert.ToInt32(hashtbl["pntaccess"]);

        if (!IsPostBack)
        {
            objCompanyDetails = new companyDetails();
            objCompanyDetails.RegistrationNumber = RegistrationNumber;
            BindSerialNumber();
            Show_Certificatcate_Registration();
            BindGridCertificate();
            BindAddress();
            btnGenerateQR_Click();
            btnGenDiff_Click();
        }
        //}
        //catch (Exception ex)
        //{
        //    ErrHandler.WriteError("Server Busy!!!");
        //}

    }
    public void Bind_Branch_office_details(int BranchId)
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds_branchofficedetails = new DataSet();
            ds_branchofficedetails = objCompany.Fetch_Branch_office_address_details(BranchId);
            if (ds_branchofficedetails.Tables[0].Rows.Count > 0)
            {
                objCompanyDetails.BranchAddress = ds_branchofficedetails.Tables[0].Rows[0]["BranchAddress"].ToString();
                objCompanyDetails.BranchPhone = ds_branchofficedetails.Tables[0].Rows[0]["Phone"].ToString();
                objCompanyDetails.BranchFax = ds_branchofficedetails.Tables[0].Rows[0]["Fax"].ToString();
                objCompanyDetails.BranchEmail = ds_branchofficedetails.Tables[0].Rows[0]["Email"].ToString();

            }

            else
            {
                ds_branchofficedetails.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Server Busy!!!!');", true);

        }
    }
    private void GenerateCode(string name)
    {
        var writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.QR_CODE;
        var result = writer.Write(name);
        string path = Server.MapPath("~/images/QRImage.jpg");
        var barcodeBitmap = new System.Drawing.Bitmap(result);
        using (MemoryStream memory = new MemoryStream())
        {
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.ReadWrite))
            {
                barcodeBitmap.Save(memory, ImageFormat.Jpeg);
                byte[] bytes = memory.ToArray();
                fs.Write(bytes, 0, bytes.Length);
            }
        }
        //imgQRCode.Visible = true;
        //imgQRCode.ImageUrl = "~/images/QRImage.jpg";

    }
    public DataSet Company_BranchId_By_CompanyId_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_BranchId_By_CompanyId_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    protected void btnGenerateQR_Click()
    {

        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        int CompanyID = ComID;// Convert.ToInt32(Request.QueryString["id"]);
        string EncryptCompany = objDecEnc.Encrypt(Convert.ToString(CompanyID));
        DataSet dsbranch = new DataSet();
        dsbranch = Company_BranchId_By_CompanyId_Online(CompanyID);
        int BranchId = Convert.ToInt32(dsbranch.Tables[0].Rows[0]["fGPtbl_CUnit_BranchId"].ToString());
        string BranchCo = Convert.ToString(dsbranch.Tables[0].Rows[0]["fGPtbl_MB_Code"].ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        string EncryptBranchCode = objDecEnc.Encrypt(BranchCo);
        GenerateCode("http://www.nsicspronline.com/appAdministrator/QRScan.aspx?c=" + EncryptCompany + "&b=" + EncryptBranchId + "&d=" + EncryptBranchCode + "");
        // GenerateCode("http://www.nsicspronline.com/appAdministrator/RegistrationCertificatePageViewQRScan.aspx?c=" + EncryptCompany + "&b=" + EncryptBranchId + "&d=" + EncryptBranchCode + "");

    }
    public void BindSerialNumber()
    {
        try
        {
            DataSet ds_serial_no = new DataSet();
            objCompany = new clsCompany();
            ds_serial_no = objCompany.Company_Registration_Certificate_fetch_serial_no(BranchId);
            if (ds_serial_no.Tables[0].Rows.Count > 0)
            {
                SerialNoAdd = ds_serial_no.Tables[0].Rows[0]["serial"].ToString();
                //  lbl_serail_add_no.Text = SerialNoAdd.ToString();
            }
            else
            {
                ds_serial_no.Dispose();
            }
        }
        catch (Exception ex)
        {
            if (ex.Message.Contains("Timeout expired"))
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ErrorAlert", "alert('Server Busy!!!!');", true);
            }

            ErrHandler.WriteError(ex.Message);

        }
    }


    public DataSet Company_WaitingCompany_Registration_CertificateForm_Dataset_PDF(int CompanyID)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPtblCertificate_Registration_pdf", DBManager.ConnectToDB());
            //MyAdp = new SqlDataAdapter("GPtblCertificate_Registration_onlinetesting", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
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

    public void Show_Certificatcate_Registration()
    {
        try
        {
            Bind_Branch_office_details(BranchId);
            string CompanyName = string.Empty;
            string PartenerName = string.Empty;

            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            System.Data.SqlClient.SqlDataReader MyDr;
            objCompany = new clsCompany();
            DataSet ds = new DataSet();

            //Label lblValidity = grdCommon.FindControl("lblValidity") as Label;




            string RefNo = RegistrationNumber;  //  this is Registration Number 
            int CompanyID = ComID;// Convert.ToInt32(Request.QueryString["id"]);
            //  hdn_company_id.Value = Request.QueryString["id"].ToString();
            //ds = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset(CompanyID);
            ds = Company_WaitingCompany_Registration_CertificateForm_Dataset_PDF(CompanyID);// objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset_PDF(CompanyID);
            DataSet dspartner = new DataSet();
            dspartner = objCompany.Company_Report_Registration_CertificateForm_PartnerName(CompanyID);
            DataTable dt = new DataTable();
            dt = dspartner.Tables[0];

            var qu = from n in dt.AsEnumerable()
                     select new
                     {
                         partnerName = n.Field<string>("DirectorName") + '@',

                     };

            foreach (var onl in qu)
            {

                if (dspartner.Tables[0].Rows.Count > 0)
                {

                    hdnlablName.Value += onl.partnerName.ToString();
                }
                else
                {
                    dspartner.Dispose();
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                objCompanyDetails.Reference = ds.Tables[0].Rows[0]["CompanyRefNumber"].ToString();
                objCompany = new clsCompany();
                DataSet ds_fetch_firstprint = new DataSet();
                ds_fetch_firstprint = objCompany.Fetch_printout_date_modified(Convert.ToInt32(CompanyID));
                if (ds_fetch_firstprint.Tables[0].Rows.Count > 0)
                {

                    //CultureInfo cii = new CultureInfo("en-GB");
                    //DateTime dati1 = Convert.ToDateTime(RegistrationDatefrom.ToString(), cii);
                    CultureInfo cii = new CultureInfo("en-GB");
                    if (lblDOI.Text == "01/01/1900")
                    {


                        // DateTime dati1 = Convert.ToDateTime(lblDOI.Text, cii);

                        //objCompanyDetails.DateOfIssue = Convert.ToDateTime(lblDOI.Text, cii).ToString();//  lblDOI.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                        DateTime doi = DateTime.ParseExact(lblDOI.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        objCompanyDetails.DateOfIssue = doi.ToString("dd/MM/yyyy");
                        // If you need a string, format it properly
                        lblDOI.Text = doi.ToString("dd/MM/yyyy");

                    }
                    else
                    {
                        //DateTime doi = Convert.ToDateTime(ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"]);
                        //objCompanyDetails.DateOfIssue = Convert.ToDateTime(doi, cii).ToString();// lblDOI.Text = doi.ToString("dd/MM/yyyy");//Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                        string dateee = ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString();
                        DateTime doi = DateTime.ParseExact(dateee, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        objCompanyDetails.DateOfIssue = doi.ToString("dd/MM/yyyy");
                        // If you need a string, format it properly
                        lblDOI.Text = doi.ToString("dd/MM/yyyy");

                        //lblDOI.Text= Convert.ToDateTime(doi, cii).ToString();
                    }

                }//1
                else
                {
                    ds_fetch_firstprint.Dispose();
                }

                if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "1")
                {
                    //AmedmentChekRenewal = "Fresh";
                }
                else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "2")
                {
                    objCompanyDetails.CertificateKind = "Renewal Certificate";
                }
                else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "3")
                {
                    objCompanyDetails.CertificateKind = "Amendment Certificate";
                }
                else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "0")
                {
                    // AmedmentChekRenewal = "Fresh";
                }

                RegistrationDatefrom = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
                objCompanyDetails.CompanyName = lblUnitName.Text = "M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();
                objCompanyDetails.CompanyAddress = lblAddress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString() + " " + ds.Tables[0].Rows[0]["CityName"].ToString() + "," + ds.Tables[0].Rows[0]["StateName"].ToString() + "-" + ds.Tables[0].Rows[0]["pinCode"].ToString();
                string ProvisinalCertificate=objCompanyDetails.Provisional = ds.Tables[0].Rows[0]["ProvisnalCertificate"].ToString();

                string PartnerName = hdnlablName.Value;
                CultureInfo ci = new CultureInfo("en-GB");
                DateTime dat1 = Convert.ToDateTime(RegistrationDatefrom.ToString(), ci);


                int IsGraduate = 0;
                DataSet dsIsGraduate = DataSetIsGraduate_Status_ByCompanyId(CompanyID);
                IsGraduate = Convert.ToInt32(dsIsGraduate.Tables[0].Rows[0]["fGPtbl_Isgraduate"].ToString());

                if (IsGraduate == 0)
                {
                    if (ProvisinalCertificate == "0")
                    {
                        objCompanyDetails.Validity = lblValidity.Text = dat1.ToString("dd/MM/yyyy") + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyyy");
                        BindTurnOver();
                    }
                    else
                    {
                        objCompanyDetails.Validity = lblValidity.Text = dat1.ToString("dd/MM/yyyy") + " to" + " " + dat1.AddYears(1).AddDays(-1).ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    objCompanyDetails.Validity = lblValidity.Text = Convert.ToString(dsIsGraduate.Tables[0].Rows[0]["fGPtbal_From_Date"].ToString()) + " to" + " " + dsIsGraduate.Tables[0].Rows[0]["fGPtbal_To_Date"].ToString();
                    BindTurnOver();
                }


                string sub = PartnerName.Substring(0, PartnerName.Length - 1);
                Name = sub.Split('@');

                int i = 0;
                while (i <= Convert.ToInt32(Name.Length.ToString()) - 1)
                {
                    if (dspartner.Tables[0].Rows.Count == 1)
                    {
                        lblpartnaerName.Text = Name[i].ToString();
                    }
                    else
                    {
                        lblpartnaerName.Text = lblpartnaerName.Text + Convert.ToString(i + 1) + '.' + Name[i] + "</br>";
                    }
                    objCompanyDetails.Owners.Add(Convert.ToString(i + 1) + '.' + Name[i]);
                    i++;
                }
                DataSet ds_monotory_limit = new DataSet();
                int CompanyIDMonitory = Convert.ToInt32(ComID);
                ds_monotory_limit = objCompany.Company_Certificate_MonoteryLimit(CompanyIDMonitory);
                int p = 0;
                if (ds_monotory_limit.Tables[0].Rows.Count > 0)
                {
                    for (int r = 0; r <= ds_monotory_limit.Tables[0].Rows.Count - 1; r++)
                    {
                        p = p + Convert.ToInt32(ds_monotory_limit.Tables[0].Rows[r]["Monetary Limit"].ToString());

                    }


                    objCompanyDetails.MLNumbers = lbltrunover.Text = " " + p + " " + "lakhs" + " ";

                }
                else
                {
                    lbltrunover.Text = ". 0 lakhs ";
                }
                string NumberText = p.ToString();
                NumberToEnglish numToeng = new NumberToEnglish();
                string rupeestext = string.Empty;
                if (NumberText.Contains('.'))
                {
                    string[] parts = NumberText.Split('.');
                    string i1 = parts[0];
                    string i2 = parts[1];
                    if (i2.Length == 1)
                    {
                        i2 = i2 + "0";
                    }
                    else
                    {
                    }
                    string numToeng_number = Convert.ToString(i1) + "." + Convert.ToString(i2);
                    //numToeng=numToeng_number.
                    objCompanyDetails.MLValue = rupeestext = numToeng.changeCurrencyToWords(numToeng_number);
                }
                else
                {
                    // rupeestext = numToeng.changeCurrencyToWords(NumberText.ToString());
                    objCompanyDetails.MLValue = rupeestext = numbertostring.ConvertMyword(NumberText.ToString());
                }

                objCompanyDetails.PAN = ds.Tables[0].Rows[0]["PAN"].ToString();
                objCompanyDetails.RegistrationNumber = lblRegistration.Text = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
                objCompanyDetails.SocialCategory = lblSocialClass.Text = ds.Tables[0].Rows[0]["CategoryCast"].ToString();
                objCompanyDetails.SpecialCategory = lblSpl.Text = ds.Tables[0].Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
                objCompanyDetails.CompanyConstitution = lblConstitution.Text = ds.Tables[0].Rows[0]["ConstitutionName1"].ToString();
                objCompanyDetails.CompanyUdyam = lblUdyam.Text = ds.Tables[0].Rows[0]["UAM"].ToString();
                string constitition_name = ds.Tables[0].Rows[0]["ConstitutionName"].ToString().Replace("<br>", "");//lblconstitutionCode.Text.ToString();
                if (constitition_name == "Public" || constitition_name == "Private")
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Directors";
                }
                else if (constitition_name == "Partnership" || constitition_name == "Limited")//dhiraj_nsic
                                                                                              // else if (constitition_name == "Partnership")//dhiraj_nsic
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Partners";
                }

                else if (constitition_name == "Proprietorship")
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Proprietor";
                }

                else if (constitition_name == "Hindu")
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Karta";
                }

                //Added by Dhiraj on 25.7.2019
                else if (constitition_name == "Trust")
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Trustee";
                }
                //Added by Dhiraj on 25.7.2019

                else
                {
                    objCompanyDetails.OwnerType = lbl_name_of_DIRECTOR.Text = "Directors";
                }

                objCompanyDetails.SerialNo = SerialNumber = Branch_Code + "-" + SerialNoAdd;

            }
        }
        catch (Exception ex)
        {

            ErrHandler.WriteError("Show_Certificatcate_Registration()  " + ex.StackTrace + "  " + ex.Message);
        }
    }
    public void BindTurnOver()
    {
        try
        {
            int CompanyID = ComID;// 3;
            objCompany = new clsCompany();
            DataSet dsTurnover = new DataSet();
            string Registration_No = RegistrationNumber;
            dsTurnover = objCompany.Company_ReportView_TUrnOver(CompanyID);
            var Rowcount = dsTurnover.Tables.Count;
            if (dsTurnover.Tables.Count != 0)
            {

                if (dsTurnover.Tables[0].Rows.Count > 0)
                {
                    if (dsTurnover.Tables[0].Rows.Count > 1)
                    {
                        if (dsTurnover.Tables[0].Columns.Count > 3)
                        {
                            int lastcolumnindex = dsTurnover.Tables[0].Columns.Count - 1;
                            dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[lastcolumnindex]);
                            grdTurnOver.DataSource = dsTurnover.Tables[0];
                            grdTurnOver.DataBind();
                            DataSet ds_consolidatedmonetrylimit = new DataSet();
                            ds_consolidatedmonetrylimit = objCompany.Fetch_ConsolidatedFor_MultipleMonetryLimit(CompanyID);
                            if (ds_consolidatedmonetrylimit.Tables[0].Rows.Count > 0)
                            {
                                string consolidate = ds_consolidatedmonetrylimit.Tables[0].Rows[0]["Cosolidated"].ToString();
                                lbltrunover.Text = "Consolidated Monetary Limit" + " " + "Rs." + consolidate.ToString() + " " + "Lakhs";
                            }
                            else
                            {
                                ds_consolidatedmonetrylimit.Dispose();
                            }
                        }
                        else
                        {
                            dsTurnover.Tables[0].Columns[1].ColumnName = "Annual Turnover";
                            dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[2]);
                            grdTurnOver.DataSource = dsTurnover.Tables[0];
                            grdTurnOver.DataBind();

                            objCompanyDetails.FY1 = dsTurnover.Tables[0].Rows[0][0].ToString();
                            objCompanyDetails.Turnover1 = dsTurnover.Tables[0].Rows[0][1].ToString();

                            objCompanyDetails.FY2 = dsTurnover.Tables[0].Rows[1][0].ToString();
                            objCompanyDetails.Turnover2 = dsTurnover.Tables[0].Rows[1][1].ToString();

                            objCompanyDetails.FY3 = dsTurnover.Tables[0].Rows[2][0].ToString();
                            objCompanyDetails.Turnover3 = dsTurnover.Tables[0].Rows[2][1].ToString();

                        }
                    }
                    else
                    {
                        grdTurnOver.Dispose();
                        grdTurnOver.DataBind();
                    }
                }
                else
                {
                    grdTurnOver.Dispose();
                    grdTurnOver.DataBind();
                }
            }
            else
            {
                dsTurnover.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }
    }
    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    public void BindAddress()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsAddress = new DataSet();
            int CompanyID = ComID;
            dsAddress = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset_AddressFactory(CompanyID);
            if (dsAddress.Tables[0].Rows.Count > 0)
            {
                if (dsAddress.Tables[0].Rows.Count == 1)
                {
                    string sno = dsAddress.Tables[0].Rows[0]["SNo"].ToString();

                    Rpt_Address.DataSource = dsAddress.Tables[0];
                    Rpt_Address.DataBind();
                }
                else
                {
                    Rpt_Address.DataSource = dsAddress.Tables[0];
                    Rpt_Address.DataBind();
                }
                Table tbl = new Table();


                for (int j = 0; j < dsAddress.Tables[0].Rows.Count; j++)
                {//for
                    string SNO = dsAddress.Tables[0].Rows[j]["SNo"].ToString();
                    string FactoryAddress = dsAddress.Tables[0].Rows[j]["AddressName"].ToString();
                    string StateName = dsAddress.Tables[0].Rows[j]["fGPtbl_MS_Name"].ToString();
                    string Zip = dsAddress.Tables[0].Rows[j]["fGPtbl_CUnit_1B_Factory_Zip"].ToString();
                    string CityName = dsAddress.Tables[0].Rows[j]["CityName"].ToString();
                    int UnitID = Convert.ToInt32(dsAddress.Tables[0].Rows[j]["UnitId"].ToString());

                    int enterprise = Convert.ToInt32(dsAddress.Tables[0].Rows[j]["fGPtbl_CUnit_Enterprise"].ToString());

                    Label lblUnitAddress = new Label();
                    lblUnitAddress.CssClass = "LabelPrintCertificate";  //

                    if (dsAddress.Tables[0].Rows.Count < 2)
                    {
                        lblUnitAddress.Text = "  " + FactoryAddress.Replace('.', ' ') + ',' + "  " + CityName + ' ' + StateName + Zip;
                        objCompanyDetails.FactoryAddress.Add(lblUnitAddress.Text);
                    }
                    else
                    {
                        // lblUnitAddress.Text = "<b>UNIT-</b>" + SNO + ')' + FactoryAddress.Replace('.', ' ') + ',' + " " + CityName + ' ' + StateName + Zip;
                        lblUnitAddress.Text = SNO + ')' + FactoryAddress.Replace('.', ' ') + ',' + " " + CityName + ' ' + StateName + Zip;
                        objCompanyDetails.FactoryAddress.Add(lblUnitAddress.Text);
                    }

                    DataSet dsstore = new DataSet();
                    DataTable dt = new DataTable();

                    //CODE TO ADD A GRIDVIEW
                    GridView gridviewNewStore;
                    Label htlable;
                    //CODE TO ADD A GRIDVIEW


                    Label tempHtmlTable = new Label();

                    dsstore = Fetch_company_Store_Certificate_Modified(UnitID);
                    dt = dsstore.Tables[0];
                    gridviewNewStore = new GridView();
                    gridviewNewStore.CssClass = "gridviewstore";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtFilteredTable = new DataTable();
                        var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).Count();
                        int countStatus = Convert.ToInt32(count);
                        string GvstorePMPS = string.Empty;
                        if (count > 0)
                        {// count if
                            dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).CopyToDataTable();
                            dtFilteredTable.Columns.Remove("UnitID");
                            GvstorePMPS = dtFilteredTable.Rows[0]["PMPS"].ToString().ToUpper();
                            //grdTestMan.Columns[4].HeaderText = "Quantitative Capacity" + "("+GvstorePMPS+")";
                            //grdTestServ.Columns[3].HeaderText= "Quantitative Capacity/Services" + "( "  + GvstorePMPS +  ")";
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[1].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[2].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());
                            HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Quantitative Capacity"].ToString());

                            string PMPSSTORE = "";
                            string PMPSSTORE2 = "";
                            if (GvstorePMPS == "PMPS" || GvstorePMPS == "pmps" || GvstorePMPS == "Pmps" || GvstorePMPS == "PMps")
                            {
                                PMPSSTORE = "P.M.P.S.";
                                PMPSSTORE2 = "PMPS";
                            }
                            else if (GvstorePMPS == "PM" || GvstorePMPS == "Pm" || GvstorePMPS == "pm")
                            {
                                PMPSSTORE = "P.M.";
                                PMPSSTORE2 = "PM";
                            }
                            else if (GvstorePMPS == "PA" || GvstorePMPS == "Pa" || GvstorePMPS == "pa")
                            {
                                PMPSSTORE = "P.A.";
                                PMPSSTORE2 = "PA";
                            }

                            dtFilteredTable.Columns[4].ColumnName = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString() + Environment.NewLine + PMPSSTORE.ToString()); //HttpUtility.HtmlDecode(lblquntitycapacity.Text.ToString());
                            string Quantitative_Capacity = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString());

                            dtFilteredTable.Columns.Remove("PMPS");
                            DataTable dtNEW = new DataTable();
                            if (enterprise == 1) //manufac
                            {
                                //grdTestMan.Visible = true;
                                //grdTestMan.DataSource = dt;
                                //grdTestMan.DataBind();

                                GridView objGV = new GridView();
                                objGV.AutoGenerateColumns = false;
                                objGV.CssClass = "table table-responsive table-striped table-bordered table-hover dataTables-example";

                                BoundField field = new BoundField();
                                field.HeaderText = "S.No";
                                field.DataField = "SNo";
                                objGV.Columns.Add(field);

                                BoundField field1 = new BoundField();
                                field1.HeaderText = "Store(s)/ Service(s) Name";
                                field1.DataField = "Store(s)/ Service(s) Name";
                                objGV.Columns.Add(field1);

                                BoundField field2 = new BoundField();
                                field2.HeaderText = "Specification(s)";
                                field2.DataField = "Specification(s)";
                                objGV.Columns.Add(field2);

                                BoundField field3 = new BoundField();
                                field3.HeaderText = "Qualitative Capacity";
                                field3.DataField = "Qualitative Capacity";
                                objGV.Columns.Add(field3);

                                BoundField field4 = new BoundField();
                                field4.HeaderText = "Quantitative Capacity" + GvstorePMPS;
                                field4.DataField = "Quantitative Capacity";
                                objGV.Columns.Add(field4);

                                BoundField field5 = new BoundField();
                                field5.HeaderText = "Remarks";
                                field5.DataField = "fGPtbl_CUnit_Store_ManRemarks";
                                objGV.Columns.Add(field5);


                                objGV.DataSource = dt;
                                objGV.DataBind();

                                plc_test.Controls.Add(lblUnitAddress);
                                // plc_test.Controls.Add(grdTestMan);
                                plc_test.Controls.Add(objGV);
                                objCompanyDetails.grdStores = objGV;

                            }

                            else if (enterprise == 2) //Service
                            {
                                //grdTestServ.Visible = true;
                                //grdTestServ.DataSource = dt;
                                //grdTestServ.DataBind();

                                GridView objGV = new GridView();
                                objGV.AutoGenerateColumns = false;
                                objGV.CssClass = "table table-responsive table-striped table-bordered table-hover dataTables-example";

                                BoundField field = new BoundField();
                                field.HeaderText = "S.No";
                                field.DataField = "SNo";
                                objGV.Columns.Add(field);

                                BoundField field1 = new BoundField();
                                field1.HeaderText = "Name of Service/Activities";
                                field1.DataField = "Store(s)/ Service(s) Name";
                                objGV.Columns.Add(field1);

                                BoundField field2 = new BoundField();
                                field2.HeaderText = "Definition of Activity/Services(s)";
                                field2.DataField = "Specification(s)";
                                objGV.Columns.Add(field2);

                                BoundField field4 = new BoundField();
                                field4.HeaderText = "Quantitative Capacity" + GvstorePMPS;
                                field4.DataField = "Quantitative Capacity";
                                objGV.Columns.Add(field4);

                                objGV.DataSource = dt;
                                objGV.DataBind();

                                plc_test.Controls.Add(lblUnitAddress);
                                //plc_test.Controls.Add(grdTestServ);
                                plc_test.Controls.Add(objGV);
                                objCompanyDetails.grdStores = objGV;
                            }


                            // gridviewNewStore.RowDataBound += new GridViewRowEventHandler(gridviewNewStore_RowDataBound);
                            gridviewNewStore.BorderWidth = Unit.Pixel(2);
                            gridviewNewStore.BorderColor = System.Drawing.Color.Black;
                            gridviewNewStore.DataSource = dtFilteredTable;
                            firstrowquantity = 0;
                            gridviewNewStore.DataBind();

                        }//count if

                        else
                        {
                            dtFilteredTable.Dispose();
                            dsAddress.Dispose();
                            gridviewNewStore.DataBind();
                            dsstore.Dispose();

                        }

                    }
                    else
                    {

                    }

                }
            }

            else
            {
                Rpt_Address.Dispose();
                Rpt_Address.DataBind();
                dsAddress.Dispose();
            }

        }
        catch (Exception ex)
        {

        }
    }
    SqlDataAdapter MyAdp;
    DataSet MyDs;
    public DataSet Fetch_company_Store_Certificate_Modified(int UnitID)
    {

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_CertificateResigaration_Store_Modified_QR", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UnitID", SqlDbType.Int).Value = UnitID;

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

    protected void grd_TurnOver_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int GrdTotalCount = 0;
            GrdTotalCount = grdTurnOver.Columns.Count;
            string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
            e.Row.Cells[0].Text = decodedText;
            if (GrdTotalCount > 0)
            {
                if (GrdTotalCount < 4)
                {
                    grdTurnOver.Columns[1].HeaderText = "Annual Turnover";
                    grdTurnOver.Columns[2].Visible = false;
                }

            }
            //e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "100px");
            // e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Width, "150px");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy");
        }
    }

    protected void grd_Turnover_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCell cell = e.Row.Cells[0];
                cell.Width = new Unit("100px");

                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {

                    TableCell cell2 = e.Row.Cells[i];
                    cell2.Width = new Unit("85px");
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public DataSet DataSetIsGraduate_Status_ByCompanyId(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_IsGraduatet_Details_ByCompanyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);

        }
        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    public void BindGridCertificate()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = RegistrationNumber;
            int CompanyID = Convert.ToInt32(ComID);
            dsCertificate_store = objCompany.Fetch_company_Store_Certificate(CompanyID);
            //Ayush-->
            string query = "select fGPtbl_IsAyush from [GPtbl_Company] where [fGPtbl_Company_Id]='" + CompanyID + "'";
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            bool result = false;
            while (reader.Read())
            {
                result = (reader[0].ToString().Equals("True")) ? true : false;
            }
            //lblAyush.Visible = lblAyushDisclaimer.Visible = result ? true : false;
            //Ayush<--

            if (dsCertificate_store.Tables[0].Rows.Count > 0)
            {

                objCompanyDetails.TotalStorecCount = " (" + dsCertificate_store.Tables[0].Rows.Count + " " + "item only" + ")";

                string PerMOnthPERShift = dsCertificate_store.Tables[0].Rows[0]["PMPs"].ToString().ToUpper();
                if (PerMOnthPERShift == "PMPS" || PerMOnthPERShift == "pmps" || PerMOnthPERShift == "Pmps")
                {
                    objCompanyDetails.capacity = "P.M.P.S.";
                }
                else if (PerMOnthPERShift == "P.M." || PerMOnthPERShift == "PM" || PerMOnthPERShift == "Pm" || PerMOnthPERShift == "pm")
                {
                    objCompanyDetails.capacity = "P.M.";
                }
                else if (PerMOnthPERShift == "P.A." || PerMOnthPERShift == "PA" || PerMOnthPERShift == "Pa")
                {
                    objCompanyDetails.capacity = "P.A.";
                }
            }
            else
            {
                // grdstores.Dispose();
                //   grdstores.DataBind();
                dsCertificate_store.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }

    }
    public string ConverthtmlCode(string input)
    {
        List<KeyValuePair<string, string>> htmlEntities = new List<KeyValuePair<string, string>>()
        {
        new KeyValuePair<string, string>("&nbsp;", " "),   // Non-breaking space
        new KeyValuePair<string, string>("&commat;", "@"),  // @ symbol
        new KeyValuePair<string, string>("&lt;", "<"),      // Less than symbol
        new KeyValuePair<string, string>("&gt;", ">"),      // Greater than symbol
        new KeyValuePair<string, string>("&#179;","³"),    // Superscript 3 (³)
        new KeyValuePair<string, string>("&#237;","i"),
        new KeyValuePair<string, string>("&amp;","&"),
     
        new KeyValuePair<string, string>("&quot;", "\""),
        new KeyValuePair<string, string>("&amp;amp;","&")
        };

        StringBuilder sb = new StringBuilder(input);
        //input = input.Replace("&amp;", "{ampersand}");
        // Perform replacements using StringBuilder for efficiency
        foreach (var entity in htmlEntities)
        {
            sb.Replace(entity.Key, entity.Value);
        }
        sb.Replace("&amp;", "&");
        // Output the updated text
        return (sb.ToString());
    }

    protected void btnGenDiff_Click()
    {
        //DataRow dr = null;//GetData("SELECT * FROM Employees where EmployeeId = " + ddlEmployees.SelectedItem.Value).Rows[0]; ;
        Document document = new Document(PageSize.A4, 88f, 88f, 100f, 100f);
        // Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
        Font NormalFont = FontFactory.GetFont("Arial", 10, BaseColor.BLACK);

        using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
        {
            PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
            unitname = objCompanyDetails.CompanyName;//lblCompanyName.Text;
            branchaddress = objCompanyDetails.BranchAddress;
            Email = objCompanyDetails.BranchEmail;
            fax = objCompanyDetails.BranchFax;
            phone = objCompanyDetails.BranchPhone;
            Website = objCompanyDetails.Website;
            serialNo = objCompanyDetails.SerialNo;

            Phrase phrase = null;
            PdfPCell cell = null;
            PdfPTable table = null;
            System.Drawing.Color color;
            PdfPCell emptyCell = new PdfPCell();
            emptyCell.BorderWidth = 0;
            document.Open();
            Background back1 = new Background();
            writer.PageEvent = (IPdfPageEvent)back1;
            //writer.PageEvent = new Background();
            //Header Table
            table = new PdfPTable(4);
            table.TotalWidth = 550f;
            table.LockedWidth = true;

            document.SetMargins(40, 40, 200, 40);//these margin decides from where content will start in a page.
            // this added to make table split if whole table is not coming in same page.
            table.SplitLate = false;
            table.SplitRows = true;


            phrase = new Phrase();
            phrase.Add(new Phrase("\n\nGOVERNMENT PURCHASE ENLISTMENT CERTIFICATE", FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.MAGENTA)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            Phrase phraseSno = new Phrase();
            phraseSno.Add(new Chunk("\nS.No.", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            phraseSno.Add(new Chunk(objCompanyDetails.SerialNo, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
            PdfPCell cellSno = PhraseCell(phraseSno, PdfPCell.ALIGN_RIGHT);
            cellSno.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cellSno.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;
            cellSno.Colspan = 4;
            cellSno.BorderWidth = 0;
            table.AddCell(cellSno);

            phrase = new Phrase();
            phrase.Add(new Chunk("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));

            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            phrase = new Phrase();
            phrase.Add(new Phrase(objCompanyDetails.CertificateKind + "(Valid From " + objCompanyDetails.Validity + ")", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            NewLine(out phrase, out cell, table);
            Reference_DOI(table, emptyCell);
            NewLine(out phrase, out cell, table);
            CompanyName_FactoryAddress(table);

            AddingDirectors(table, emptyCell);
            NewLine(out phrase, out cell, table);

            Constitution_UAM_Category(table);

            NewLine(out phrase, out cell, table);


            phrase = new Phrase();
            phrase.Add(new Phrase("GOVERNMENT PURCHASE REGISTRATION NO:" + objCompanyDetails.RegistrationNumber, FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            var Mon_Lang = new Phrase();
            Mon_Lang.Add(new Chunk("Monetary Limit: Rs ", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            Mon_Lang.Add(new Chunk(objCompanyDetails.MLNumbers + "(Rupees " + objCompanyDetails.MLValue + ")", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));

            PdfPCell Mon_Langcell = PhraseCell(Mon_Lang, PdfPCell.ALIGN_CENTER);
            Mon_Langcell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            Mon_Langcell.Colspan = 4;
            Mon_Langcell.BorderWidth = 0;
            table.AddCell(Mon_Langcell);

            NewLine(out phrase, out cell, table);

           

            if(objCompanyDetails.Provisional.Equals("0"))
            {
                phrase = new Phrase();
                phrase.Add(new Phrase("TURNOVER (Rupees in lakhs)", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
                cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
                cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                cell.Colspan = 4;
                cell.BorderWidth = 0;
                // cell.BorderColor = new BaseColor(0, 0, 0);
                table.AddCell(cell);

                MonetaryLimitTable(table, emptyCell);
            }
            else
            {
                NewLine(out phrase, out cell, table);
                NewLine(out phrase, out cell, table);
                NewLine(out phrase, out cell, table);
            }


                NewLine(out phrase, out cell, table);

            phrase = new Phrase();
            phrase.Add(new Phrase("MSEs registered with NSIC are exempted from deposit of Earnest Money irrespective of value of Monetary Limit. ", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;

            table.AddCell(cell);

            phrase = new Phrase();
            phrase.Add(new Phrase("Your name has been registered as a MSE Unit eligible for participation in the Central Government Store Purchase Programme as per the Single Point Registration Scheme for the following Item(s)/Store(s)/Service(s).", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_LEFT);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;

            table.AddCell(cell);

            NewLine(out phrase, out cell, table);

            StoresTable(table);
            NewLine(out phrase, out cell, table);
            phrase = new Phrase();
            phrase.Add(new Phrase("As per List Attached" + objCompanyDetails.TotalStorecCount, FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;

            table.AddCell(cell);

            Disclaimer(table);

            phrase = new Phrase();
            phrase.Add(new Chunk("\n", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));

            cell = PhraseCell(phrase, PdfPCell.ALIGN_RIGHT);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            document.NewPage();  // Forces a new page      

            phrase = new Phrase();
            phrase.Add(new Phrase("\n\n\nSTORE DETAILS", FontFactory.GetFont("Arial", 14, Font.BOLD, BaseColor.MAGENTA)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            NewLine(out phrase, out cell, table);

            phrase = new Phrase();
            phrase.Add(new Phrase(objCompanyDetails.CertificateKind + "(Valid From " + objCompanyDetails.Validity + ")", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
            cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
            cell.Colspan = 4;
            cell.BorderWidth = 0;
            table.AddCell(cell);

            NewLine(out phrase, out cell, table);

            Phrase StoreLine = new Phrase();
            StoreLine.Add(new Chunk("ANNEXURE TO GOVERNMENT PURCHASES ENLISTMENT CERTIFICATE NO.  ", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
            StoreLine.Add(new Chunk(objCompanyDetails.RegistrationNumber, FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
            StoreLine.Add(new Chunk("   D.T. ", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
            StoreLine.Add(new Chunk(objCompanyDetails.DateOfIssue, FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));

            PdfPCell StoreLineCell = new PdfPCell(StoreLine);
            StoreLineCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            StoreLineCell.BorderWidth = 0;
            StoreLineCell.Colspan = 4;
            table.AddCell(StoreLineCell);

            Phrase Store2Line = new Phrase();
            Store2Line.Add(new Chunk("ISSUED TO ", FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
            Store2Line.Add(new Chunk(objCompanyDetails.CompanyName, FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));

            PdfPCell Store2LineCell = new PdfPCell(Store2Line);
            Store2LineCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
            Store2LineCell.BorderWidth = 0;
            Store2LineCell.Colspan = 4;
            table.AddCell(Store2LineCell);


            phrase = new Phrase();

            // PdfPTable pdfTable = null;
            foreach (var v in plc_test.Controls)
            {
                if (v is GridView)
                {

                    // Get the GridView reference
                    GridView gridView = v as GridView;

                    // Create a table with the same number of columns as the GridView
                    PdfPTable pdfTable = new PdfPTable(gridView.Columns.Count);


                    // Set table width percentage (adjust as needed to avoid squeezing)
                    float[] columnWidths = new float[gridView.Columns.Count];
                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {
                        //columnWidths[i] = 1f;  // Adjust this value for better spacing

                        if (i == 0) // First column
                        {
                            columnWidths[i] = 0.3f;  // small column
                        }
                        else if (i == 1) // Second column
                        {
                            columnWidths[i] = 1f;  // normal width column
                        }
                        else if (i == 2) // Second column
                        {
                            columnWidths[i] = 1f;  // Medium width column
                        }
                        else if (i == 2) // Second column
                        {
                            columnWidths[i] = 2.5f;  // Medium width column
                        }
                        else // Other columns
                        {
                            columnWidths[i] = 0.75f;  // Default width for the rest
                        }
                    }
                    pdfTable.SetWidths(columnWidths);

                    // Add headers from the GridView
                    foreach (DataControlField cell1 in gridView.Columns)
                    {
                        PdfPCell pdfCell = new PdfPCell(new Phrase(cell1.HeaderText));
                        pdfCell.BackgroundColor = BaseColor.LIGHT_GRAY;
                        pdfCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        pdfTable.AddCell(pdfCell);
                    }

                    // Add data rows from the GridView
                    foreach (GridViewRow row in gridView.Rows)
                    {
                        foreach (TableCell cell1 in row.Cells)
                        {

                            string result = ConverthtmlCode(cell1.Text);
                            //PdfPCell pdfCell1 = new PdfPCell(new Phrase(cell1.Text, FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
                            PdfPCell pdfCell1 = new PdfPCell(new Phrase(result, FontFactory.GetFont("Arial", 11, Font.NORMAL, BaseColor.BLACK)));
                            pdfCell1.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                            pdfTable.AddCell(pdfCell1);

                        }
                    }

                    // Add the table directly to the document and ensure it doesn't sit side by side
                    PdfPCell tableCell = new PdfPCell(pdfTable)
                    {
                        Colspan = 4,  // Adjust this value based on how much space you want to allocate
                        Padding = 4,  // Adjust padding if needed
                        BorderWidth = 0  // Set border width to 0 if you want a borderless appearance
                    };

                    // Add the table cell to the document
                    table.AddCell(tableCell);

                    // Add an empty cell (to add space between this table and next content) – optional
                    //table.AddCell("");  // Empty cell for spacing
                }
                else if (v is Label)
                {
                    // Create a new Phrase for each Label
                    Phrase labelPhrase = new Phrase();

                    // Get the text from the Label control
                    string labelText = ((System.Web.UI.WebControls.Label)v).Text;

                    // Add the Label text to the Phrase (one label at a time)
                    labelPhrase.Add(new Chunk("\n" + labelText, FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));

                    // Create a PdfPCell for the label's text
                    PdfPCell labelCell = PhraseCell(labelPhrase, PdfPCell.ALIGN_LEFT);
                    labelCell.VerticalAlignment = PdfPCell.ALIGN_TOP;
                    labelCell.Colspan = 4;
                    labelCell.BorderWidth = 0;

                    // Add the label cell to the document in a new row
                    table.AddCell(labelCell);


                }
            }






            //PdfPCell cellExpand = new PdfPCell(STable);
            //cellExpand.Colspan = 4;
            //table.AddCell(cellExpand);



            // Footer(table, emptyCell);

            //document.Add(table1);
            document.Add(table);

            //watermark_Background_border(document, writer, out wmark, out baseFont, out content, out rectangle, out fontSize);





            //document.SetAccessibleAttribute
            document.Close();



            byte[] bytes = memoryStream.ToArray();
            memoryStream.Close();
            // Apply encryption and restrictions (make the certificate non-editable)
            using (MemoryStream encryptedMemoryStream = new MemoryStream())
            {
                PdfReader reader = new PdfReader(bytes);
                PdfStamper stamper = new PdfStamper(reader, encryptedMemoryStream);

                // Set user password and owner password
                string u1 = objCompanyDetails.CompanyUdyam.Substring(12, 7);
                string p1 = objCompanyDetails.PAN.Substring(0, 5);

                string userPassword = p1 + u1;  // Change to the user password
                string ownerPassword = "ownerPassword";  // Change to the owner password (for permissions)

                // Set permissions (no editing, no copying)
                int permissions = PdfWriter.AllowPrinting;

                stamper.SetEncryption(
                    Encoding.ASCII.GetBytes(userPassword),
                    Encoding.ASCII.GetBytes(ownerPassword),
                    permissions,
                    PdfWriter.ENCRYPTION_AES_128 | PdfWriter.DO_NOT_ENCRYPT_METADATA
                );
                stamper.FormFlattening = true;
                stamper.Close();

                //Return the encrypted PDF to the user
                byte[] encryptedBytes = encryptedMemoryStream.ToArray();
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=SPRSCertificate.pdf");
                Response.ContentType = "application/pdf";
                Response.Buffer = true;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.BinaryWrite(bytes);
                Response.BinaryWrite(encryptedBytes);
                Response.End();
                Response.Close();
            }
        }
    }

    public static void AddWaterMark(PdfContentByte dc, string text, BaseFont font, float fontSize, float angle, BaseColor color, iTextSharp.text.Rectangle realPageSize, iTextSharp.text.Rectangle rect = null)
    {
        var gstate = new PdfGState { FillOpacity = 0.1f, StrokeOpacity = 0.3f };
        dc.SaveState();
        dc.SetGState(gstate);
        dc.SetColorFill(color);
        dc.BeginText();
        dc.SetFontAndSize(font, fontSize);
        var ps = rect ?? realPageSize; /*dc.PdfDocument.PageSize is not always correct*/
        var x = (ps.Right + ps.Left) / 2;
        var y = (ps.Bottom + ps.Top) / 2;
        dc.ShowTextAligned(Element.ALIGN_CENTER, text, x, y, angle);
        dc.EndText();
        dc.RestoreState();
    }

    private static void Disclaimer(PdfPTable table)
    {
        var Disclaimer = new Phrase();
        Disclaimer.Add(new Chunk("Disclaimer:- ", FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK)));
        Disclaimer.Add(new Chunk("1. The purchasing agencies are advised to satisfy themselves with the store details in the certificate while doing the Technical Evaluation stage before placing the tender/order on the units, certified by NSIC. ", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        Disclaimer.Add(new Chunk("\n" + " 2. This certificate will be valid subject to compliance of definition (Composite criteria of investment and turnover for classification of Micro, Small Enterprises as per MSMED Act). ", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell DisclaimerCell = new PdfPCell(Disclaimer);
        DisclaimerCell.Colspan = 4;
        DisclaimerCell.BorderWidth = 0;
        DisclaimerCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        table.AddCell(DisclaimerCell);

        var Signatory = new Phrase();
        Signatory.Add(new Chunk("\n\nThis is a Digital Copy of Certificate, doesn't require any signature.", FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK)));
        PdfPCell SignatoryCell = new PdfPCell(Signatory);
        SignatoryCell.Colspan = 4;
        SignatoryCell.BorderWidth = 0;
        SignatoryCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        table.AddCell(SignatoryCell);
    }


    private static void NewLine(out Phrase phrase, out PdfPCell cell, PdfPTable table)
    {
        phrase = new Phrase();
        phrase.Add(new Phrase("\n", FontFactory.GetFont("Arial", 11, Font.BOLD, BaseColor.BLACK)));
        cell = PhraseCell(phrase, PdfPCell.ALIGN_CENTER);
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.Colspan = 4;
        cell.BorderWidth = 0;

        table.AddCell(cell);
    }

    private void Constitution_UAM_Category(PdfPTable table)
    {
        var CONSTITUTION = new Phrase();
        CONSTITUTION.Add(new Chunk("Constitution:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        CONSTITUTION.Add(new Chunk(objCompanyDetails.CompanyConstitution, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell CONSTITUTIONCell = new PdfPCell(CONSTITUTION);
        CONSTITUTIONCell.Colspan = 2;
        CONSTITUTIONCell.BorderWidth = 0;
        CONSTITUTIONCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

        var UAM = new Phrase();
        UAM.Add(new Chunk("Udyam Registration Number:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        UAM.Add(new Chunk(objCompanyDetails.CompanyUdyam, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell UAMCell = new PdfPCell(UAM);
        UAMCell.Colspan = 2;
        UAMCell.BorderWidth = 0;
        UAMCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

        var CATEGORY = new Phrase();
        CATEGORY.Add(new Chunk("Enterprise Social Class:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        CATEGORY.Add(new Chunk(objCompanyDetails.SocialCategory, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell CATEGORYCell = new PdfPCell(CATEGORY);
        CATEGORYCell.Colspan = 2;
        CATEGORYCell.BorderWidth = 0;
        CATEGORYCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

        var SPLCATEGORY = new Phrase();
        SPLCATEGORY.Add(new Chunk("Special Category:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        SPLCATEGORY.Add(new Chunk(objCompanyDetails.SpecialCategory, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell SPLCATEGORYCell = new PdfPCell(SPLCATEGORY);
        SPLCATEGORYCell.BorderWidth = 0;
        SPLCATEGORYCell.Colspan = 2;
        SPLCATEGORYCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;


        table.AddCell(CONSTITUTIONCell);
        table.AddCell(UAMCell);
        table.AddCell(CATEGORYCell);
        table.AddCell(SPLCATEGORYCell);
    }

    private void Reference_DOI(PdfPTable table, PdfPCell emptyCell)
    {
        var reference = new Phrase();
        reference.Add(new Chunk("Ref.No.:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        reference.Add(new Chunk(objCompanyDetails.Reference, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell referenceCell = new PdfPCell(reference);
        referenceCell.Colspan = 2;
        referenceCell.BorderWidth = 0;
        referenceCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;


        var Date = new Phrase();
        Date.Add(new Chunk("Date: ", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        Date.Add(new Chunk(lblDOI.Text, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        PdfPCell DateCell = new PdfPCell(Date);

        DateCell.BorderWidth = 0;
        DateCell.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;



        table.AddCell(referenceCell);
        table.AddCell(emptyCell);
        table.AddCell(DateCell);
    }

    private void MonetaryLimitTable(PdfPTable table, PdfPCell emptyCell)
    {
        PdfPCell MonetaryHeadingCell = new PdfPCell(new Phrase("Financial Year ", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        MonetaryHeadingCell.BorderColorLeft = BaseColor.BLACK;
        MonetaryHeadingCell.BorderColorRight = BaseColor.BLACK;
        MonetaryHeadingCell.BorderColorTop = BaseColor.BLACK;
        MonetaryHeadingCell.BorderColorBottom = BaseColor.BLACK;
        MonetaryHeadingCell.BorderWidthLeft = 1f;
        MonetaryHeadingCell.BorderWidthRight = 0.5f;
        MonetaryHeadingCell.BorderWidthTop = 1f;
        MonetaryHeadingCell.BorderWidthBottom = 0.5f;
        MonetaryHeadingCell.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell MonetaryHeadingCell1 = new PdfPCell(new Phrase("Annual Turnover", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        MonetaryHeadingCell1.BorderColorLeft = BaseColor.BLACK;
        MonetaryHeadingCell1.BorderColorRight = BaseColor.BLACK;
        MonetaryHeadingCell1.BorderColorTop = BaseColor.BLACK;
        MonetaryHeadingCell1.BorderColorBottom = BaseColor.BLACK;
        MonetaryHeadingCell1.BorderWidthLeft = 0.5f;
        MonetaryHeadingCell1.BorderWidthRight = 1f;
        MonetaryHeadingCell1.BorderWidthTop = 1f;
        MonetaryHeadingCell1.BorderWidthBottom = 0.5f;
        MonetaryHeadingCell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
        table.AddCell(emptyCell);
        table.AddCell(MonetaryHeadingCell);
        table.AddCell(MonetaryHeadingCell1);
        table.AddCell(emptyCell);

        PdfPCell FY1 = new PdfPCell(new Phrase(objCompanyDetails.FY1, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        FY1.BorderColorLeft = BaseColor.BLACK;
        FY1.BorderColorRight = BaseColor.BLACK;
        FY1.BorderColorTop = BaseColor.BLACK;
        FY1.BorderColorBottom = BaseColor.BLACK;
        FY1.BorderWidthLeft = 1f;
        FY1.BorderWidthRight = 0.5f;
        FY1.BorderWidthTop = 1f;
        FY1.BorderWidthBottom = 0.5f;
        FY1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell Turnover1 = new PdfPCell(new Phrase(objCompanyDetails.Turnover1, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        Turnover1.BorderColorLeft = BaseColor.BLACK;
        Turnover1.BorderColorRight = BaseColor.BLACK;
        Turnover1.BorderColorTop = BaseColor.BLACK;
        Turnover1.BorderColorBottom = BaseColor.BLACK;
        Turnover1.BorderWidthLeft = 0.5f;
        Turnover1.BorderWidthRight = 1f;
        Turnover1.BorderWidthTop = 1f;
        Turnover1.BorderWidthBottom = 0.5f;
        Turnover1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        table.AddCell(emptyCell);
        table.AddCell(FY1);
        table.AddCell(Turnover1);
        table.AddCell(emptyCell);


        if (objCompanyDetails.FY2 != null)
        {
            if (objCompanyDetails.FY2.Length == 7)
            {
                PdfPCell FY2 = new PdfPCell(new Phrase(objCompanyDetails.FY2, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
                FY2.BorderColorLeft = BaseColor.BLACK;
                FY2.BorderColorRight = BaseColor.BLACK;
                FY2.BorderColorTop = BaseColor.BLACK;
                FY2.BorderColorBottom = BaseColor.BLACK;
                FY2.BorderWidthLeft = 1f;
                FY2.BorderWidthRight = 0.5f;
                FY2.BorderWidthTop = 1f;
                FY2.BorderWidthBottom = 0.5f;
                FY2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell Turnover2 = new PdfPCell(new Phrase(objCompanyDetails.Turnover2, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
                Turnover2.BorderColorLeft = BaseColor.BLACK;
                Turnover2.BorderColorRight = BaseColor.BLACK;
                Turnover2.BorderColorTop = BaseColor.BLACK;
                Turnover2.BorderColorBottom = BaseColor.BLACK;
                Turnover2.BorderWidthLeft = 0.5f;
                Turnover2.BorderWidthRight = 1f;
                Turnover2.BorderWidthTop = 1f;
                Turnover2.BorderWidthBottom = 0.5f;
                Turnover2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(emptyCell);
                table.AddCell(FY2);
                table.AddCell(Turnover2);
                table.AddCell(emptyCell);
            }
        }

        if (objCompanyDetails.FY3 != null)
        {
            if (objCompanyDetails.FY3.Length == 7)
            {
                PdfPCell FY3 = new PdfPCell(new Phrase(objCompanyDetails.FY3, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
                FY3.BorderColorLeft = BaseColor.BLACK;
                FY3.BorderColorRight = BaseColor.BLACK;
                FY3.BorderColorTop = BaseColor.BLACK;
                FY3.BorderColorBottom = BaseColor.BLACK;
                FY3.BorderWidthLeft = 1f;
                FY3.BorderWidthRight = 0.5f;
                FY3.BorderWidthTop = 1f;
                FY3.BorderWidthBottom = 0.5f;
                FY3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                PdfPCell Turnover3 = new PdfPCell(new Phrase(objCompanyDetails.Turnover3, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
                Turnover3.BorderColorLeft = BaseColor.BLACK;
                Turnover3.BorderColorRight = BaseColor.BLACK;
                Turnover3.BorderColorTop = BaseColor.BLACK;
                Turnover3.BorderColorBottom = BaseColor.BLACK;
                Turnover3.BorderWidthLeft = 0.5f;
                Turnover3.BorderWidthRight = 1f;
                Turnover3.BorderWidthTop = 1f;
                Turnover3.BorderWidthBottom = 0.5f;
                Turnover3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

                table.AddCell(emptyCell);
                table.AddCell(FY3);
                table.AddCell(Turnover3);
                table.AddCell(emptyCell);
            }
        }


        PdfPCell MonetaryHeading = new PdfPCell(new Phrase("Monetary Limit", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        MonetaryHeading.BorderColorLeft = BaseColor.BLACK;
        MonetaryHeading.BorderColorRight = BaseColor.BLACK;
        MonetaryHeading.BorderColorTop = BaseColor.BLACK;
        MonetaryHeading.BorderColorBottom = BaseColor.BLACK;
        MonetaryHeading.BorderWidthLeft = 1f;
        MonetaryHeading.BorderWidthRight = 0.5f;
        MonetaryHeading.BorderWidthTop = 1f;
        MonetaryHeading.BorderWidthBottom = 1f;
        MonetaryHeading.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell TotalMonetary = new PdfPCell(new Phrase(objCompanyDetails.MLNumbers, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        TotalMonetary.BorderColorLeft = BaseColor.BLACK;
        TotalMonetary.BorderColorRight = BaseColor.BLACK;
        TotalMonetary.BorderColorTop = BaseColor.BLACK;
        TotalMonetary.BorderColorBottom = BaseColor.BLACK;
        TotalMonetary.BorderWidthLeft = 1f;
        TotalMonetary.BorderWidthRight = 0.5f;
        TotalMonetary.BorderWidthTop = 1f;
        TotalMonetary.BorderWidthBottom = 1f;
        TotalMonetary.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        table.AddCell(emptyCell);
        table.AddCell(MonetaryHeading);
        table.AddCell(TotalMonetary);
        table.AddCell(emptyCell);
    }

    private void StoresTable(PdfPTable table)
    {
        PdfPCell NameOfStore = new PdfPCell(new Phrase("Name of the Store(s)/Service(s)", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        NameOfStore.BorderColorLeft = BaseColor.BLACK;
        NameOfStore.BorderColorRight = BaseColor.BLACK;
        NameOfStore.BorderColorTop = BaseColor.BLACK;
        NameOfStore.BorderColorBottom = BaseColor.BLACK;
        NameOfStore.BorderWidthLeft = 1f;
        NameOfStore.BorderWidthRight = 1f;
        NameOfStore.BorderWidthTop = 1f;
        NameOfStore.BorderWidthBottom = 1f;
        NameOfStore.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell Specification = new PdfPCell(new Phrase("Specification(s)", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        Specification.BorderColorLeft = BaseColor.BLACK;
        Specification.BorderColorRight = BaseColor.BLACK;
        Specification.BorderColorTop = BaseColor.BLACK;
        Specification.BorderColorBottom = BaseColor.BLACK;
        Specification.BorderWidthLeft = 1f;
        Specification.BorderWidthRight = 1f;
        Specification.BorderWidthTop = 1f;
        Specification.BorderWidthBottom = 1f;
        Specification.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell Qualitative = new PdfPCell(new Phrase("Qualitative Capacity", FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        Qualitative.BorderColorLeft = BaseColor.BLACK;
        Qualitative.BorderColorRight = BaseColor.BLACK;
        Qualitative.BorderColorTop = BaseColor.BLACK;
        Qualitative.BorderColorBottom = BaseColor.BLACK;
        Qualitative.BorderWidthLeft = 1f;
        Qualitative.BorderWidthRight = 1f;
        Qualitative.BorderWidthTop = 1f;
        Qualitative.BorderWidthBottom = 1f;
        Qualitative.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        PdfPCell Quantitative = new PdfPCell(new Phrase("Quantitative Capacity " + "\n" + objCompanyDetails.capacity, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));
        Quantitative.BorderColorLeft = BaseColor.BLACK;
        Quantitative.BorderColorRight = BaseColor.BLACK;
        Quantitative.BorderColorTop = BaseColor.BLACK;
        Quantitative.BorderColorBottom = BaseColor.BLACK;
        Quantitative.BorderWidthLeft = 1f;
        Quantitative.BorderWidthRight = 1f;
        Quantitative.BorderWidthTop = 1f;
        Quantitative.BorderWidthBottom = 1f;
        Quantitative.HorizontalAlignment = PdfPCell.ALIGN_CENTER;

        table.AddCell(NameOfStore);
        table.AddCell(Specification);
        table.AddCell(Qualitative);
        table.AddCell(Quantitative);
    }

    private void AddingDirectors(PdfPTable table, PdfPCell emptyCell)
    {
        var Directores = new Phrase();
        PdfPCell DirectoresCell = null;
        Directores.Add(new Chunk("Name Of the " + objCompanyDetails.OwnerType + ":", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        foreach (string owner in objCompanyDetails.Owners)
        {
            Directores.Add(new Chunk("\n" + owner, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

            DirectoresCell = new PdfPCell(Directores);
            DirectoresCell.Colspan = 2;
            DirectoresCell.BorderWidth = 0;
            DirectoresCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        }
        table.AddCell(DirectoresCell);
        table.AddCell(emptyCell);
        table.AddCell(emptyCell);
    }

    private void CompanyName_FactoryAddress(PdfPTable table)
    {
        var phrase = new Phrase();
        phrase.Add(new Chunk(objCompanyDetails.CompanyName, FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        phrase.Add(new Chunk("\n" + objCompanyDetails.CompanyAddress, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

        // Add the heading cell with company name and address
        PdfPCell HeadingCell = new PdfPCell(phrase);
        HeadingCell.Colspan = 2;
        HeadingCell.BorderWidth = 0;
        HeadingCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        table.AddCell(HeadingCell);

        // Add a single cell for "Factory Address:"
        var phraseFactoryAddress = new Phrase();
        phraseFactoryAddress.Add(new Chunk("Factory Address:", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
        PdfPCell FactoryAddressLabelCell = new PdfPCell(phraseFactoryAddress);
        FactoryAddressLabelCell.Colspan = 2;
        FactoryAddressLabelCell.BorderWidth = 0;
        FactoryAddressLabelCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;


        // Loop to add factory addresses
        foreach (string factory in objCompanyDetails.FactoryAddress)
        {
            // Create a new Phrase for each factory address
            var phrase1 = new Phrase();
            //phraseFactoryAddress.Add(new Chunk("Unit", FontFactory.GetFont("Arial", 10, Font.BOLD, BaseColor.BLACK)));
            phraseFactoryAddress.Add(new Chunk("\n" + factory, FontFactory.GetFont("Arial", 10, Font.NORMAL, BaseColor.BLACK)));

            // Create a new PdfPCell for each factory address
            PdfPCell FactoryCell = new PdfPCell(phraseFactoryAddress);
            FactoryCell.Colspan = 2;
            FactoryCell.BorderWidth = 0;
            FactoryCell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;

            // Add the factory address cell to the table
            // table.AddCell(FactoryCell);
        }
        table.AddCell(FactoryAddressLabelCell);
    }

    private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
    {
        PdfContentByte contentByte = writer.DirectContent;
        contentByte.SetColorStroke(color);
        contentByte.MoveTo(x1, y1);
        contentByte.LineTo(x2, y2);
        contentByte.Stroke();
    }

    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = BaseColor.WHITE;
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }
    private static PdfPCell ImageCell(string path, float scale, int align)
    {
        iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
        image.ScalePercent(scale);
        PdfPCell cell = new PdfPCell(image);
        cell.BorderColor = BaseColor.WHITE;
        cell.VerticalAlignment = PdfPCell.ALIGN_TOP;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 0f;
        cell.PaddingTop = 0f;
        return cell;
    }

}
