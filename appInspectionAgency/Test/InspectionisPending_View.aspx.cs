using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;
using System.Configuration;
using System.Globalization;
using System.Xml.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Image = System.Drawing.Image;
//using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

public partial class appInspectionAgency_InspectionisPending_View : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID;
    string USERFULLNAME, UserEmailID, BRANCHNAME;
    clsMasterInspectionAgency ObjAgency;
    string unit, company, formtype;
    public int uploadAction = 0;
    DataSet Myds;

    int FileStatus_1;
    int successId, companyId;
    string OriginalFilePath;
    string filePath;
    string file1;
    int unitid1, formtypeid1, amendmentid = 0;
    string MailTo = string.Empty;
    string MailFrom = string.Empty;
    string MailSubject = string.Empty;
    string MailBody = string.Empty;
    string ccMail = string.Empty;
    string bccMail = string.Empty;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;

    int ErrorId = 0;
    string strMasterPathFresh;
    string strMasterPathRA;

    string strMasterPathFreshTest;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            AGENCYID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["AGENCYID"]).ToString());
            BRANCHID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            BRANCHNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["BRANCHNAME"]).ToString();
            UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["EmailID"]).ToString();
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            AGENCYID = 0;
            BRANCHID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        OriginalFilePath = "~/Document/Upload/Registration/";
        filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";

         strMasterPathFresh = System.Configuration.ConfigurationManager.AppSettings["MasterPathFresh"];
         strMasterPathRA = System.Configuration.ConfigurationManager.AppSettings["MasterPathRA"];

        strMasterPathFreshTest= System.Configuration.ConfigurationManager.AppSettings["MasterPathFreshTest"];


        DecryptEncrypt objde = new DecryptEncrypt();

        try
        {
            unit = Request.QueryString["u"].Replace(" ", "+").ToString();
            company = Request.QueryString["c"].Replace(" ", "+").ToString();
            companyId = Convert.ToInt32(objde.Decrypt(company));
            formtype = Request.QueryString["f"].Replace(" ", "+").ToString();

            if (Request.QueryString["id"] != null)
                uploadAction = 1;

            DecryptEncrypt objd = new DecryptEncrypt();


            unitid1 = Convert.ToInt32(objd.Decrypt(unit).ToString());
            formtypeid1 = Convert.ToInt32(objd.Decrypt(formtype));

            Myds = new DataSet();
            Myds = CheckUploadedDocument_Count_IA(unitid1, formtypeid1, AGENCYID);
            int intGetStatus = Convert.ToInt16(Myds.Tables[0].Rows[0]["CheckStatus"].ToString());

            if (intGetStatus == 120)
            {
                btnSubmitUpload.Visible = true;
                lblMsgbtn.Visible = false;
            }
            else
            {
                btnSubmitUpload.Visible = false;
                lblMsgbtn.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        if (!Page.IsPostBack)
        {
            try
            {
                FunfillGridView(unit, company, formtype);

            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }

    }


    private void FunfillGridView(string unit, string company, string formtype)
    {
        try
        {
            ObjAgency = new clsMasterInspectionAgency();
            DecryptEncrypt objd = new DecryptEncrypt();
            int unitid = Convert.ToInt32(objd.Decrypt(unit.Replace(" ", "+")).ToString());
            int companyid = Convert.ToInt32(objd.Decrypt(company.Replace(" ", "+")));
            int formtypeid = Convert.ToInt32(objd.Decrypt(formtype.Replace(" ", "+")));

            System.Data.DataTable Dt = new System.Data.DataTable();

            //TO FETCH AMENDMENT ID
            clsAmendment objAmendment = new clsAmendment();
            amendmentid = objAmendment.CompanyAmendment_WaitingCompany_FetchAmendmentId(unitid);

            Dt = ObjAgency.Agency_Fetch_InspectionisPendingByUnitCompanyFormtype(amendmentid, unitid, companyid, formtypeid);
            if (Dt.Rows.Count != 0)
            {
                lblType.Text = Dt.Rows[0]["fGPtbl_MF_Name"].ToString();
                lblCompany.Text = Dt.Rows[0]["fGPtbl_Company_Name"].ToString();
                lblRegno.Text = Dt.Rows[0]["fGPtbl_CUnit_SSIRegistrationNo"].ToString();
                lblPan.Text = Dt.Rows[0]["fGPtbl_Company_PAN"].ToString();
                lblRequest.Text = Dt.Rows[0]["fGPtbl_IS_DateStatus4"].ToString();
                hdnuserid.Value = Dt.Rows[0]["fGPtbl_IS_CUser"].ToString();
            }
            else
            {
                lblType.Text = "";
                lblCompany.Text = "";
                lblRegno.Text = "";
                lblPan.Text = "";
                lblRequest.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    //TO INSERT DOCUMENT UPLOADED BY AGENCY

    public int Agency_Update_InspectionAgency_Documents_IA(int unitid, int agencyid,
int formtypeid, string reportname, string reportpath, int userid, int fGPtbl_IS_RecomendedStatus, string fGPtbl_IS_Remarks, string StrURLInspectionVideo)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Agency_Update_InspectionisPending_Documents_IA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_IS_UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_AgencyId", SqlDbType.Int).Value = agencyid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_FormtypeId", SqlDbType.Int).Value = formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_ReportName", SqlDbType.VarChar, 1000).Value = reportname;
            MySqlComm.Parameters.Add("@fGPtbl_IS_ReportPath", SqlDbType.VarChar, 2000).Value = reportpath;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

            MySqlComm.Parameters.Add("@fGPtbl_IS_RecomendedStatus", SqlDbType.Int).Value = fGPtbl_IS_RecomendedStatus;
            MySqlComm.Parameters.Add("@fGPtbl_IS_Remarks", SqlDbType.VarChar, 2000).Value = fGPtbl_IS_Remarks;
            MySqlComm.Parameters.Add("@StrURLInspectionVideo", SqlDbType.VarChar, 5000).Value = StrURLInspectionVideo;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }


    public int Agency_Update_InspectionAgency_Documents(int unitid, int agencyid,
int formtypeid, string reportname, string reportpath, int userid, int fGPtbl_IS_RecomendedStatus, string fGPtbl_IS_Remarks, string StrURLInspectionVideo)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Agency_Update_InspectionisPending_Documents", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_IS_UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_AgencyId", SqlDbType.Int).Value = agencyid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_FormtypeId", SqlDbType.Int).Value = formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_ReportName", SqlDbType.VarChar, 1000).Value = reportname;
            MySqlComm.Parameters.Add("@fGPtbl_IS_ReportPath", SqlDbType.VarChar, 2000).Value = reportpath;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;

            MySqlComm.Parameters.Add("@fGPtbl_IS_RecomendedStatus", SqlDbType.Int).Value = fGPtbl_IS_RecomendedStatus;
            MySqlComm.Parameters.Add("@fGPtbl_IS_Remarks", SqlDbType.VarChar, 2000).Value = fGPtbl_IS_Remarks;
            MySqlComm.Parameters.Add("@StrURLInspectionVideo", SqlDbType.VarChar, 5000).Value = StrURLInspectionVideo;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }


    public int Agency_Update_InspectionAgency_GeoTagImage(int unitid, int agencyid,
int formtypeid, string reportname, int ImageNumber, int userid, Decimal fGPtbl_IS_GeoTag_Lat, Decimal fGPtbl_IS_GeoTag_Long)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Agency_Update_InspectionAgency_GeoTagImage_IA", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_IS_UnitId", SqlDbType.Int).Value = unitid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_AgencyId", SqlDbType.Int).Value = agencyid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_FormtypeId", SqlDbType.Int).Value = formtypeid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_ReportName", SqlDbType.VarChar, 1000).Value = reportname;
            MySqlComm.Parameters.Add("@ImageNumber", SqlDbType.Int).Value = ImageNumber;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@fGPtbl_IS_GeoTag_Lat", SqlDbType.Decimal).Value = fGPtbl_IS_GeoTag_Lat;
            MySqlComm.Parameters.Add("@fGPtbl_IS_GeoTag_Long", SqlDbType.Decimal).Value = fGPtbl_IS_GeoTag_Long;

            SqlParameter outVal = new SqlParameter("@Output", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outVal.Value);
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
        return ErrorId;
    }

    public DataSet CheckUploadedDocument_Count_IA(int unitid, int FormType, int AgencyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CheckUploadedDocument_Count_IA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@unitid", SqlDbType.Int).Value = unitid;
            MyAdp.SelectCommand.Parameters.Add("@FormType", SqlDbType.Int).Value = FormType;
            MyAdp.SelectCommand.Parameters.Add("@AgencyId", SqlDbType.Int).Value = AgencyId;
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
    protected void btnGeoTagImg1_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload2.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload2.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload2.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload2.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(strMasterPathFreshTest + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


           // FileUpload2.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            FileUpload2.PostedFile.SaveAs(strMasterPathFreshTest + CompanyPAN + "/" + newFileName);

         //Image image = Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");

             Image image = Image.FromFile(strMasterPathFreshTest + CompanyPAN + "/" + newFileName);

            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag1.Visible = true;
                btnGeoTag1.Visible = true;
               // btnGeoTag1.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                btnGeoTag1.NavigateUrl = strMasterPathFreshTest + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 1, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));

            }
            else
            {


                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }

        }



    }

    protected void btnGeoTagImg2_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload3.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload3.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload3.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload3.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload3.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag2.Visible = true;
                btnGeoTag2.Visible = true;
                btnGeoTag2.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 2, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));

            }
            else
            {


                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }
    protected void btnGeoTagImg3_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload4.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload4.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload4.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload4.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag3.Visible = true;
                btnGeoTag3.Visible = true;
                btnGeoTag3.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 3, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));

                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);

            }
            else
            {


                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }
    protected void btnGeoTagImg4_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload5.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload5.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload5.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload5.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag4.Visible = true;
                btnGeoTag4.Visible = true;
                btnGeoTag4.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 4, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);
            }
            else
            {
                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }

    protected void btnGeoTagImg5_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload6.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload6.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload6.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload6.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag5.Visible = true;
                btnGeoTag5.Visible = true;
                btnGeoTag5.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 5, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);
            }
            else
            {
                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }

    protected void btnGeoTagImg6_Click(object sender, EventArgs e)
    {
        if (FileUpload7.HasFile)
        {
            Decimal sizeOfDoc;
            int contentLength = Convert.ToInt32(FileUpload7.PostedFile.ContentLength.ToString());
            string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload7.PostedFile.FileName);
            string fileExtn = System.IO.Path.GetExtension(FileUpload7.PostedFile.FileName).ToLower();

            string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

            sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
            int fileSize = FileUpload4.PostedFile.ContentLength;

            if (fileSize > sizeOfDoc)
            {

                Label1.Text = "File size can't be greater than 2.5 MB ";
            }
            else
            {
                //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
                //OriginalFilePath = "~/Document/Upload/Registration/";

                var folder = Server.MapPath(OriginalFilePath);
                string Companyname = "";
                string CompanyPAN = "";
                System.Data.DataSet mydsComName = new System.Data.DataSet();
                mydsComName = CompanyName_by_CompanyId(companyId);
                Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

                if (!Directory.Exists(folder + CompanyPAN))
                {
                    Directory.CreateDirectory(folder + CompanyPAN);
                }


                FileUpload7.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
                Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
                float? dd_latitude = GetLatitude(image);

                float? dd_longitude = GetLongitude(image);

                if (dd_longitude != null && dd_latitude != null)
                {
                    trgeotagimag6.Visible = true;
                    btnGeoTag6.Visible = true;
                    btnGeoTag6.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                    //1 Means Image 1 , 2 Means Image 2 snd so on 
                    int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 6, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);
                }
                else
                {
                    //string file_name = filePath + Companyname + "/" + newFileName;
                    string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)//check file exsit or not  
                    {
                        image.Dispose();
                        file.Delete();
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                    }
                    //else
                    //{

                    //}
                }
            }
        }
    }

    protected void btnGeoTagImg7_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload8.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload8.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload8.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload8.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag7.Visible = true;
                btnGeoTag7.Visible = true;
                btnGeoTag7.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 7, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);
            }
            else
            {
                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }

    protected void btnGeoTagImg8_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload9.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload9.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload9.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(2.5) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 2.5 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload9.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);
            Image image = Image.FromFile(filePath + CompanyPAN + "/" + newFileName);// Image.FromFile(@"E:\Code\GIT_SPRS_LIVE_Application_WORKING\SPRS\Images\" + "TimePhoto_20240403_133112.jpg");
            float? dd_latitude = GetLatitude(image);

            float? dd_longitude = GetLongitude(image);

            if (dd_longitude != null && dd_latitude != null)
            {
                trgeotagimag8.Visible = true;
                btnGeoTag8.Visible = true;
                btnGeoTag8.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
                //1 Means Image 1 , 2 Means Image 2 snd so on 
                int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 8, USERID, Convert.ToDecimal(dd_latitude), Convert.ToDecimal(dd_longitude));
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Image has been uploaded successfully.');", true);
            }
            else
            {
                //string file_name = filePath + Companyname + "/" + newFileName;
                string path = Server.MapPath(OriginalFilePath + CompanyPAN + "/" + newFileName);
                FileInfo file = new FileInfo(path);
                if (file.Exists)//check file exsit or not  
                {
                    image.Dispose();
                    file.Delete();
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Uploaded Image doesnot seems to be Geotag Image. Only valid GeoTag Image will be uploaded.');", true);

                }
                //else
                //{

                //}
            }
        }
    }

    protected void btnVideoUpload_Click(object sender, EventArgs e)
    {

        Decimal sizeOfDoc;
        int contentLength = Convert.ToInt32(FileUpload10.PostedFile.ContentLength.ToString());
        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload10.PostedFile.FileName);
        string fileExtn = System.IO.Path.GetExtension(FileUpload10.PostedFile.FileName).ToLower();

        string newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);

        sizeOfDoc = Convert.ToDecimal(20) * 1000000;
        int fileSize = FileUpload4.PostedFile.ContentLength;

        if (fileSize > sizeOfDoc)
        {

            Label1.Text = "File size can't be greater than 20 MB ";
        }
        else
        {
            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
            //OriginalFilePath = "~/Document/Upload/Registration/";

            var folder = Server.MapPath(OriginalFilePath);
            string Companyname = "";
            string CompanyPAN = "";
            System.Data.DataSet mydsComName = new System.Data.DataSet();
            mydsComName = CompanyName_by_CompanyId(companyId);
            Companyname = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
            CompanyPAN = mydsComName.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            if (!Directory.Exists(folder + CompanyPAN))
            {
                Directory.CreateDirectory(folder + CompanyPAN);
            }


            FileUpload10.PostedFile.SaveAs(filePath + CompanyPAN + "/" + newFileName);

            trVideo9.Visible = true;
            btnGeoTag9.Visible = true;
            btnGeoTag9.NavigateUrl = OriginalFilePath + CompanyPAN + "/" + newFileName;
            //1 Means Image 1 , 2 Means Image 2 snd so on 
            int result = Agency_Update_InspectionAgency_GeoTagImage(unitid1, AGENCYID, formtypeid1, newFileName, 9, USERID, Convert.ToDecimal(0), Convert.ToDecimal(0));
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Video has been uploaded successfully.');", true);

        }
    }
    public DataSet CompanyName_by_CompanyId(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyName_by_CompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = companyid;
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

    protected void btnInspectionReport_Click(object sender, EventArgs e)
    {
        try
        {
            string fileexten, imagesize, pdffilesize;
            fileexten = System.Configuration.ConfigurationManager.AppSettings["filefilterIA"].ToString();
            imagesize = System.Configuration.ConfigurationManager.AppSettings["imagsize"].ToString();
            pdffilesize = System.Configuration.ConfigurationManager.AppSettings["pdfsizeIA"].ToString();

            //if (FileUpload1.PostedFile!= null)
            if (FileUpload1.HasFile)
            {
                object postedFile = FileUpload1.PostedFile;
                string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                string fileExtn = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                string newFileName = "";
                if (!fileexten.Contains(fileExtn))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only .pdf are allowed  ');", true);
                    return;
                }
                if (filenameWithoutExtn != "")
                {



                    string contentType = FileUpload1.PostedFile.ContentType.ToString();
                    int contentLength = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());
                    newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration
                    int AllowedContentLengthInBytes = 0;
                    int filerecognige = 0;
                    //if (fileExtn == ".jpg" || fileExtn == ".jpeg" || fileExtn == ".gif" || fileExtn == ".png")
                    //{
                    //    AllowedContentLengthInBytes = Convert.ToInt32(imagesize);
                    //    filerecognige = 1;
                    //}
                    if (fileExtn == ".pdf")
                    {
                        AllowedContentLengthInBytes = Convert.ToInt32(pdffilesize);
                        filerecognige = 2;
                    }

                    //Decimal sizeOfDoc;

                    //sizeOfDoc = 20 * 1000000;

                    if (contentLength > AllowedContentLengthInBytes)
                    {
                        //if (filerecognige == 1)
                        //{
                        //    Label1.Text = "File size can't be greater than 50kb ";
                        //}
                        //else
                        //{
                        //    Label1.Text = "File size can't be greater than 20 MB ";
                        //}

                        if (filerecognige == 1)
                        {
                            Label1.Text = "File size can't be greater than 20 MB ";
                        }
                        //else
                        //{
                        //    Label1.Text = "File size can't be greater than 20 MB ";
                        //}

                        FileStatus_1 = 1;
                    }
                    else
                    {
                        Label1.Text = "";
                        FileStatus_1 = 2;
                    }

                    if (FileStatus_1 == 2)
                    {

                        FileUpload1.PostedFile.SaveAs(filePath + newFileName);


                        ObjAgency = new clsMasterInspectionAgency();
                        successId = 0;
                        string strRemarks = "";
                        if (txtRemarks.Text != "")
                        {
                            strRemarks = txtRemarks.Text;
                        }
                        else
                        {
                            strRemarks = "";
                        }

                        string strURLInspectionVideo = "";
                        //if (txtInspectionvideo.Text != "")
                        //{
                        //    strURLInspectionVideo = txtInspectionvideo.Text;
                        //}
                        //else
                        //{
                        //    strURLInspectionVideo = "";
                        //}

                        successId = Agency_Update_InspectionAgency_Documents_IA(unitid1, AGENCYID, formtypeid1, newFileName, OriginalFilePath, USERID, Convert.ToInt32(rdbInspectionStatus.SelectedValue), strRemarks, strURLInspectionVideo);



                        // successId = ObjAgency.Agency_Update_InspectionAgency_Documents(unitid1, AGENCYID, formtypeid1, newFileName, OriginalFilePath, USERID, Convert.ToInt32(rdbInspectionStatus.SelectedValue), strRemarks);

                        //successId = Agency_Update_InspectionAgency_Documents(unitid1, AGENCYID, formtypeid1, newFileName, OriginalFilePath, USERID, Convert.ToInt32(rdbInspectionStatus.SelectedValue), strRemarks, strURLInspectionVideo);

                        if (successId == 2)
                        {

                            trUploadedDocument.Visible = true;
                            HLName.Visible = true;


                            //OriginalFilePath = "~/Document/Upload/Registration/";
                            //filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/" + newFileName;


                            //Response.ContentType = "Application/pdf";
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=Test_PDF.pdf");
                            //Response.TransmitFile(filePath);
                            //Response.End();


                            //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                            HLName.NavigateUrl = OriginalFilePath + newFileName;


                            Session["pendingAction"] = "Document uploaded successfully!";
                            // SendMailBranchUnit();
                        }
                        else
                        {
                            Session["pendingAction"] = "Server is busy!";
                        }

                        //if (formtypeid1 == 1)
                        //{
                        //    Response.Redirect("InspectionisPending.aspx?i=1", false);
                        //}
                        //else if (formtypeid1 == 3)
                        //{
                        //    Response.Redirect("InspectionisPending.aspx?i=3", false);
                        //}
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnSubmitUpload_Click(object sender, EventArgs e)
    {
        try
        {
            string fileexten, imagesize, pdffilesize;
            fileexten = System.Configuration.ConfigurationManager.AppSettings["filefilter"].ToString();
            imagesize = System.Configuration.ConfigurationManager.AppSettings["imagsize"].ToString();
            pdffilesize = System.Configuration.ConfigurationManager.AppSettings["pdfsize"].ToString();

            //if (FileUpload1.PostedFile!= null)
            if (FileUpload1.HasFile)
            {
                object postedFile = FileUpload1.PostedFile;
                string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.PostedFile.FileName);
                string fileExtn = System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToLower();
                string newFileName = "";
                if (!fileexten.Contains(fileExtn))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only .jpg, .jpeg, .gif, .png,  .pdf are allowed  ');", true);
                    return;
                }
                if (filenameWithoutExtn != "")
                {



                    string contentType = FileUpload1.PostedFile.ContentType.ToString();
                    int contentLength = Convert.ToInt32(FileUpload1.PostedFile.ContentLength.ToString());
                    newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration
                    int AllowedContentLengthInBytes;
                    int filerecognige;
                    if (fileExtn == ".jpg" || fileExtn == ".jpeg" || fileExtn == ".gif" || fileExtn == ".png")
                    {
                        AllowedContentLengthInBytes = Convert.ToInt32(imagesize);
                        filerecognige = 1;
                    }
                    else
                    {
                        AllowedContentLengthInBytes = Convert.ToInt32(pdffilesize);
                        filerecognige = 2;
                    }

                    if (contentLength > AllowedContentLengthInBytes)
                    {
                        if (filerecognige == 1)
                        {
                            Label1.Text = "File size can't be greater than 50kb ";
                        }
                        else
                        {
                            Label1.Text = "File size can't be greater than 1MB ";
                        }
                        FileStatus_1 = 1;
                    }
                    else
                    {
                        Label1.Text = "";
                        FileStatus_1 = 2;
                    }

                    if (FileStatus_1 == 2)
                    {

                        FileUpload1.PostedFile.SaveAs(filePath + newFileName);
                        ObjAgency = new clsMasterInspectionAgency();
                        successId = 0;
                        string strRemarks = "";
                        if (txtRemarks.Text != "")
                        {
                            strRemarks = txtRemarks.Text;
                        }
                        else
                        {
                            strRemarks = "";
                        }

                        string strURLInspectionVideo = "";
                        //if (txtInspectionvideo.Text != "")
                        //{
                        //    strURLInspectionVideo = txtInspectionvideo.Text;
                        //}
                        //else
                        //{
                        //    strURLInspectionVideo = "";
                        //}


                        // successId = ObjAgency.Agency_Update_InspectionAgency_Documents(unitid1, AGENCYID, formtypeid1, newFileName, OriginalFilePath, USERID, Convert.ToInt32(rdbInspectionStatus.SelectedValue), strRemarks);

                        successId = Agency_Update_InspectionAgency_Documents(unitid1, AGENCYID, formtypeid1, newFileName, OriginalFilePath, USERID, Convert.ToInt32(rdbInspectionStatus.SelectedValue), strRemarks, strURLInspectionVideo);

                        if (successId == 2)
                        {
                            Session["pendingAction"] = "Document uploaded successfully!";
                            SendMailBranchUnit();
                        }
                        else
                        {
                            Session["pendingAction"] = "Server is busy!";
                        }

                        if (formtypeid1 == 1)
                        {
                            Response.Redirect("InspectionisPending.aspx?i=1", false);
                        }
                        else if (formtypeid1 == 3)
                        {
                            Response.Redirect("InspectionisPending.aspx?i=3", false);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //TO SEND MAIL TO UNIT AND BRANCH
    public void SendMailBranchUnit()
    {
        //1. SEND MAIL TO BRANCH FOR INSPECTION DATE ISSUED TO UNIT
        // 2. SEND MAIL TO UNIT WITH INSPECTION DATE
        try
        {

            string ToMail = string.Empty;
            string FromMail, ccMail, bccMail, subject, bodyMail;
            string MailUser = string.Empty;
            clsMasterUser objUser = new clsMasterUser();
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            ObjAgency = new clsMasterInspectionAgency();
            //SqlDataReader Mydr;
            DataTable Mydt = new DataTable();
            Mydt = objCompany.Company_Detail_Fetch_ById(companyId);

            Myds = new DataSet();

            //TO BRANCH MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(5); // Change Here
            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +"Inspection Report has been submitted" + ".";

            string str = bodyMail.Replace("$$Companyname$$", Mydt.Rows[0]["fGPtbl_Company_Name"].ToString());
            str = str.Replace("$$InspectionAgency$$", ObjAgency.MasterInspectionAgency_Fetch_DataSet(AGENCYID).Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString());
            DataSet DsAgencyName = new DataSet();
            //ObjAgency = new clsMasterInspectionAgency();
            DsAgencyName = ObjAgency.MasterInspectionAgency_Fetch_DataSet(AGENCYID);
            string agencyname = string.Empty;
            if (DsAgencyName.Tables[0].Rows.Count > 0)
            {
                agencyname = DsAgencyName.Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString();
            }
            else
            {
                agencyname = "";
            }
            //TO FETCH BRANCH(USER) EMAIL ID
            if (hdnuserid.Value != "")
            {

                Myds = objUser.MasterUser_Fetch_DataSet(Convert.ToInt32(hdnuserid.Value));
                ToMail = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                MailUser = Myds.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
            }

            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];

            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


            MailFrom = FromMail;  //"nsicclient@nsic.co.in";
            MailTo = ToMail.ToString();
            MailSubject = subject;
            bodyMail = "Dear" + ' ' + MailUser + "<br/><br>" + str;  // "Inspection report has been uploaded for " + ' ' + lblCompany.Text.ToString() + "<br><br>" + "Thanks" + "<br><br>" + "Regards" + "<br/><br>" + agencyname;
            MailMessage message = new MailMessage();
            message.From = new MailAddress(MailFrom);  // MailFrom
            // Set the recepient address of the mail message
            message.To.Add(new MailAddress(MailTo));

            // Check if the bcc value is null or an empty string
            if ((bccMail != null) && (bccMail != string.Empty))
            {
                // Set the Bcc address of the mail message
                message.Bcc.Add(new MailAddress(bccMail));
            }      // Check if the cc value is null or an empty value
            if ((ccMail != null) && (ccMail != string.Empty))
            {
                // Set the CC address of the mail message
                message.CC.Add(new MailAddress(ccMail));
            }       // Set the subject of the mail message
            message.Subject = MailSubject;
            // Set the body of the mail message
            message.Body = bodyMail;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);

            //INSERT INTO SENT MAILER
            clsDDL.Insert_MailDetails(MailTo, 8, 1, MailFrom, 6, 2, MailSubject, bodyMail, 2);


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


        // SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, str, "", 0, "", "", "");

        //TO SEND THE MAIL TO UNIT
        /*
        Mydr = objCompany.Company_Fetch_ByUnit_Step2(unitid1);

        while (Mydr.Read())
        {
            ToMail = Mydr["fGPtbl_CUnit_1B_Factory_Email"].ToString();
        }
        Mydr.Close();

        //TO UNIT
        Myds = objMailer.MasterMailer_Fetch_DataSet(7);

        FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
        ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
        bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
        subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
        bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString() + "Inspection Report has been submitted" + ".";

        //SEND MAIL TO UNIT
        SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
        */
    }

    protected void lbBack_Click(object sender, EventArgs e)
    {
        if (formtypeid1 == 1)
        {
            Response.Redirect("InspectionisPending.aspx?i=1", false);
        }
        else if (formtypeid1 == 3)
        {
            Response.Redirect("InspectionisPending.aspx?i=3", false);
        }
    }

    protected void rdbInspectionStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbInspectionStatus.SelectedValue == "2")
        {
            trRemarks.Visible = true;
            Rfdremarks.Enabled = true;
        }
        else
        {
            trRemarks.Visible = false;
            Rfdremarks.Enabled = false;
        }
    }


    private void ReadImageLatitudeAndLongitude()
    {
        string filePath = @"Images\" + "TimePhoto_20240403_133112.jpg";
        string physicalLocation = Server.MapPath(filePath);
        if (File.Exists(physicalLocation))
        {
            Bitmap bmp = new Bitmap(physicalLocation);
            // set Variable Values
            double? latitdue = null;
            double? longitude = null;

            foreach (PropertyItem propItem in bmp.PropertyItems)
            {
                switch (propItem.Type)
                {
                    case 5:
                        if (propItem.Id == 2) // Latitude Array
                            latitdue = GetLatitudeAndLongitude(propItem);
                        if (propItem.Id == 4) //Longitude Array
                            longitude = GetLatitudeAndLongitude(propItem);
                        break;

                }
            }

            Response.Write("Latitude :- " + latitdue);
            Response.Write("<br/>");
            Response.Write("Latitude :- " + longitude);
        }
    } // ReadImageLatitudeAndLongitude

    private static double? GetLatitudeAndLongitude(PropertyItem propItem)
    {
        Double final;
        try
        {
            uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
            uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);

            uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
            uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);

            uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
            uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);


            //   float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);


            // final = Convert.ToDouble(Convert.ToDouble(degreesNumerator) + Convert.ToDouble(Convert.ToDouble(minutesNumerator) + Convert.ToDouble(secondsNumerator));



            //return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
            //       (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            //(Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator))

            //(Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +

            double f1, d1, d2, d3;

            d1 = (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator));
            d2 = (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60);
            d3 = (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            f1 = d1 + d2 + d3;

            //(Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));

            return (Convert.ToDouble(degreesNumerator) / Convert.ToDouble(degreesDenominator)) + (Convert.ToDouble(Convert.ToDouble(minutesNumerator) / Convert.ToDouble(minutesDenominator)) / 60) +
                   (Convert.ToDouble((Convert.ToDouble(secondsNumerator) / Convert.ToDouble(secondsDenominator)) / 3600));


        }
        catch (Exception)
        {

            return null;
        }
    } // GetLatitudeAndLongitude

    public static float? GetLatitude(Image targetImg)
    {
        try
        {
            //Property Item 0x0001 - PropertyTagGpsLatitudeRef
            PropertyItem propItemRef = targetImg.GetPropertyItem(1);
            //Property Item 0x0002 - PropertyTagGpsLatitude
            PropertyItem propItemLat = targetImg.GetPropertyItem(2);
            return ExifGpsToFloat(propItemRef, propItemLat);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    public static float? GetLongitude(Image targetImg)
    {
        try
        {
            ///Property Item 0x0003 - PropertyTagGpsLongitudeRef
            PropertyItem propItemRef = targetImg.GetPropertyItem(3);
            //Property Item 0x0004 - PropertyTagGpsLongitude
            PropertyItem propItemLong = targetImg.GetPropertyItem(4);
            return ExifGpsToFloat(propItemRef, propItemLong);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
    private static float ExifGpsToFloat(PropertyItem propItemRef, PropertyItem propItem)
    {
        uint degreesNumerator = BitConverter.ToUInt32(propItem.Value, 0);
        uint degreesDenominator = BitConverter.ToUInt32(propItem.Value, 4);
        float degrees = degreesNumerator / (float)degreesDenominator;

        uint minutesNumerator = BitConverter.ToUInt32(propItem.Value, 8);
        uint minutesDenominator = BitConverter.ToUInt32(propItem.Value, 12);
        float minutes = minutesNumerator / (float)minutesDenominator;

        uint secondsNumerator = BitConverter.ToUInt32(propItem.Value, 16);
        uint secondsDenominator = BitConverter.ToUInt32(propItem.Value, 20);
        float seconds = secondsNumerator / (float)secondsDenominator;

        float coorditate = degrees + (minutes / 60f) + (seconds / 3600f);
        string gpsRef = System.Text.Encoding.ASCII.GetString(new byte[1] { propItemRef.Value[0] }); //N, S, E, or W
        if (gpsRef == "S" || gpsRef == "W")
            coorditate = 0 - coorditate;
        return coorditate;
    }


}
