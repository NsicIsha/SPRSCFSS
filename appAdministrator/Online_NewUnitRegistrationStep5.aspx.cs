using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

public partial class appAdministrator_Online_NewUnitRegistrationStep5 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId, FormType;
    string UserFullName;

    clsCompany objCompany;
    int ErrorId;
    int FileStatus_1, FileStatus_2, FileStatus_3, FileStatus_4, FileStatus_5, FileStatus_6, FileStatus_7, FileStatus_8, FileStatus_9, FileStatus_10;
    string OriginalFilePath;
    string filePath;
    string urlid;
    string RAType;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;



    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        FunRequest();

        BindGrid(UnitId);
        //  int cntt = 0;
        //  cntt = objCompany.CountMasterDoc();
        //int  ii = 0;
        //  while (ii < cntt)
        //  {
        //      if(ii==0)
        //      {
        //          rfd
        //      }
        //  }

        if (FormType !=3)
        {
            OriginalFilePath = "./Document/Upload/Registration/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
        }
        //else if(FormType == 2)
        //{
        //    OriginalFilePath = "./Document/Upload/Renewal/";
        //    filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
        //}
        else
        {
            OriginalFilePath = "./Document/Upload/Amendment/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Amendment/";
        }


    }



    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            FormType = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());

            RAType= Convert.ToString(objDecEnc.Decrypt(Request.QueryString["ra"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }


    public void BindGrid(int UnitId)
    {
        objCompany = new clsCompany();
        GrdUpload.DataSource = Fetch_MasterDoc(UnitId);
        GrdUpload.DataBind();
    }


    public DataSet Fetch_MasterDoc(int UnitId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_MasterUploadDocument_Online_Renewal", DBManager.ConnectToDB());
            MyAdp.SelectCommand.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

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
    protected bool ValidUploads(int count)
    {
        int p = 0;
        bool isvalid = true;
        Decimal sizeOfDoc;

        int cnt, i;
        string DocName;
        string uploadid;
        i = 0;


        while (p < count)
        {
            FileUpload fileUpload = GrdUpload.Rows[p].FindControl("FileUpload1") as FileUpload;
            DocName = GrdUpload.Rows[p].Cells[1].Text.ToString();


            if (!fileUpload.HasFile)
            {
                isvalid = false;
                lblMsg.Text = "Please upload scanned copy of " + GrdUpload.Rows[p].Cells[1].Text;
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload scanned copy of PAN.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + lblMsg.Text + "');", true);

                return isvalid;


            }
            else
            {
                string ssss = GrdUpload.Rows[p].Cells[1].Text;

             sizeOfDoc = Convert.ToDecimal(GrdUpload.Rows[p].Cells[3].Text.ToString());

               // sizeOfDoc=Convert.ToInt32(GrdUpload.SelectedDataKey["Link"].ToString());

                // sizeOfDoc = sizeOfDoc * 125000;

                sizeOfDoc = sizeOfDoc * 1048576;

                // Get the size in bytes of the file to upload.
                int fileSize = fileUpload.PostedFile.ContentLength;

                if (fileSize > sizeOfDoc)
                {

                    isvalid = false;   //"size of " + GrdUpload.Rows[p].Cells[1].Text + +  "." 
                    lblMsg.Text = "Please upload " + GrdUpload.Rows[p].Cells[2].Text;// + GrdUpload.Rows[p].Cells[3].Text;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload scanned copy of PAN.');", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + lblMsg.Text + "');", true);

                    return isvalid;
                }
                else
                {
                    isvalid = true;
                }


            }

            p++;
        }
        return IsValid;
    }
    public void BindGrid()
    {
        objCompany = new clsCompany();
        GrdUpload.DataSource = objCompany.Fetch_MasterDoc();
        GrdUpload.DataBind();

    }

    public int CountMasterDoc(int UnitId)
    {
        int MaxId = 0;
        try
        {
            MyDs = new DataSet();
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterUploadDocument_Online_Renewal_Count", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@UnitId", SqlDbType.Int).Value = UnitId;
            MaxId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
           // MyAdp.Dispose();
        }
        return MaxId;

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int cnt, i;
            string DocName;
            string uploadid;
            i = 0;
            string fileexten, imagesize, pdffilesize;
            fileexten = System.Configuration.ConfigurationManager.AppSettings["filefilter"].ToString();
            imagesize = System.Configuration.ConfigurationManager.AppSettings["imagsize"].ToString();
            pdffilesize = System.Configuration.ConfigurationManager.AppSettings["pdfsize"].ToString();

            objCompany = new clsCompany();
            cnt = CountMasterDoc(UnitId);
            bool isValid = ValidUploads(cnt);// true;// ValidUploads(cnt);
            if (isValid)
            {
                while (i < cnt)
            {
                FileUpload fileUpload = GrdUpload.Rows[i].FindControl("FileUpload1") as FileUpload;
                DocName = GrdUpload.Rows[i].Cells[1].Text.ToString();

                if (i == 0)
                {
                    if (!fileUpload.HasFile)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload scanned copy of PAN.');", true);
                        return;
                    }
                }

                if (fileUpload.HasFile)
                {
                    object postedFile = fileUpload.PostedFile;
                    string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(fileUpload.PostedFile.FileName);
                    string fileExtn = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName).ToLower();
                    string newFileName = "";
                    if (!fileexten.Contains(fileExtn))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only .jpg, .jpeg, .gif, .png, .pdf are allowed  ');", true);
                        return;
                    }
                    if (filenameWithoutExtn != "")
                    {
                        string contentType = fileUpload.PostedFile.ContentType.ToString();
                        int contentLength = Convert.ToInt32(fileUpload.PostedFile.ContentLength.ToString());
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


                        //int AllowedContentLengthInMB = 3;
                        //= AllowedContentLengthInMB * 1024 * 1024;


                        if (contentLength > AllowedContentLengthInBytes)
                        {
                             
                            ////GrdUpload.Rows[i].Cells[1].FindControl("Label1").Visible = true;
                            ////if (filerecognige == 1)
                            ////{
                            ////    ((Label)GrdUpload.Rows[i].Cells[1].FindControl("Label1")).Text = "File size can't be greater than 50kb ";
                            ////}
                            ////else
                            ////{
                            ////    ((Label)GrdUpload.Rows[i].Cells[1].FindControl("Label1")).Text = "File size can't be greater than 1MB ";
                            ////}

                           FileStatus_1 = 2;
                            ////return;
                        }
                        else
                        {
                            // Label1.Text = "";
                            FileStatus_1 = 2;
                        }

                        if (FileStatus_1 == 2)
                        {

                            fileUpload.PostedFile.SaveAs(filePath + newFileName);

                            //2.SUCCESSSFULLY INSERTED, 3. ERROR, 4. SUCCESSFULLY UPDATED
                            ErrorId = 0;

                            objCompany = new clsCompany();

                            System.Data.DataTable dt = new System.Data.DataTable();
                            dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);

                            foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                            {

                                // Console.WriteLine("--- Row ---"); // Print separator.

                                int a = 0;


                                foreach (var item in row.ItemArray) // Loop over the items.
                                {
                                    if (a == 0)
                                    {
                                        UnitId = Convert.ToInt32(item);
                                        if (FormType == 1)
                                        {
                                            ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 1, DocName, newFileName, OriginalFilePath, UserId);
                                        }
                                        else if (FormType == 2)
                                        {
                                            ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 2, DocName, newFileName, OriginalFilePath, UserId);
                                        }
                                        else if (FormType == 3)
                                        {
                                            ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 3, DocName, newFileName, OriginalFilePath, UserId);
                                        }
                                    }
                                    a++;
                                }
                            }
                            dt.Clear();
                            dt.Dispose();
                        }
                    }
                }
                i = i + 1;
            }

                if (FormType == 1)
                {
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                    string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                    string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                    Response.Redirect("NewUnitRegistrationStep6.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
                }
                else if (FormType == 2)
                {
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                    string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                    string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                    urlid = "5";
                    string EncryptUrlId = objDecEnc.Encrypt(urlid.ToString());

                    if(RAType.ToString()=="RA")
                    {

                    }
                    else
                    { 
                    Response.Redirect("Online_UnitRenewalStep6.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlId, false);

                    }
                }
                else if (FormType == 3)
                {
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                    string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                    string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                    string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                    urlid = "5";
                    string EncryptUrlId = objDecEnc.Encrypt(urlid.ToString());

                    Response.Redirect("UnitAmendment_ViewChanges.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&i=" + EncryptUrlId, false);
                }

            }      //IF VALID CLOSE  
        } //try close

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void btn_back_click(object sender, EventArgs e)
    {
        if (FormType == 1)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();

            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptyUnitId = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptFormType = objDecEnc.Encrypt(FormType.ToString());
            string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();


            Response.Redirect("NewUnitRegistrationStep4.aspx?c=" + EncryptCompany + "&u=" + EncryptyUnitId + "&s=" + EncryptStatus + "&f=" + EncryptFormType + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
        }
        else if (FormType == 2)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());

            string EncryptStatus = objDecEnc.Encrypt(FormType.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());

            string EncryptRAType = objDecEnc.Encrypt(RAType.ToString());

            if (RAType.ToString() == "RA")
            {

            }
            else
            {
                Response.Redirect("Online_UnitRenewalStep4.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId + "ra=" + EncryptRAType, false);

            }


            
        }
        else if (FormType == 3)
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());

            string EncryptStatus = objDecEnc.Encrypt(FormType.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            Response.Redirect("UnitAmendmentStep4.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }

    }
    protected void GrdUpload_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        objCompany = new clsCompany();

        //  int i = (objCompany.CountMasterDoc()-1);
        int i = (CountMasterDoc(UnitId) - 1);
        int j = e.Row.RowIndex;

        
        if (j == i)
        {
            e.Row.Cells[1].FindControl("btnSubmit").Visible = true;
            e.Row.Cells[1].FindControl("btnBack").Visible = true;
        }

    }


    //public int CountMasterDoc()
    //{
    //    int MaxId = 0;
    //    try
    //    {
    //        MySqlComm = new SqlCommand("select count(*) From dbo.GPtbl_MasterDocuments Where fGPtbl_MD_Delete=0 and fGPtbl_MD_DocummentStatus=0  ", DBManager.ConnectToDB());
    //        MySqlComm.CommandType = CommandType.Text;

    //        MaxId = Convert.ToInt32(MySqlComm.ExecuteScalar().ToString());
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError(ex.Message);
    //    }
    //    finally
    //    {
    //        DBManager.CloseConnection();
    //        MySqlComm.Dispose();
    //    }

    //    return MaxId;

    //}
    protected void GrdUpload_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Upload")
        {
            try
            {
                int cnt, i;
                string DocName;
                string uploadid;
                i = 0;
                objCompany = new clsCompany();

                string fileexten, imagesize, pdffilesize;
                fileexten = System.Configuration.ConfigurationManager.AppSettings["filefilter"].ToString();
                imagesize = System.Configuration.ConfigurationManager.AppSettings["imagsize"].ToString();
                pdffilesize = System.Configuration.ConfigurationManager.AppSettings["pdfsize"].ToString();

                cnt = CountMasterDoc(UnitId);// objCompany.CountMasterDoc();
                while (i < cnt)
                {
                    FileUpload fileUpload = GrdUpload.Rows[i].FindControl("FileUpload1") as FileUpload;
                    DocName = GrdUpload.Rows[i].Cells[1].Text.ToString();

                    if (fileUpload.HasFile)
                    {
                        object postedFile = fileUpload.PostedFile;
                        string filenameWithoutExtn = System.IO.Path.GetFileNameWithoutExtension(fileUpload.PostedFile.FileName);
                        string fileExtn = System.IO.Path.GetExtension(fileUpload.PostedFile.FileName);
                        string newFileName = "";

                        if (!fileexten.Contains(fileExtn))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Only .jpg, .jpeg, .gif, .png, .doc, .docx, .xls, .xlsx, .pdf, .txt are allowed  ');", true);
                            FileStatus_1 = 1;
                            return;
                        }

                        if (filenameWithoutExtn != "")
                        {
                            string contentType = fileUpload.PostedFile.ContentType.ToString();
                            int contentLength = Convert.ToInt32(fileUpload.PostedFile.ContentLength.ToString());
                            newFileName = FormInputReplace.RenameFile(filenameWithoutExtn, fileExtn, 1);    //1=Registration

                            int AllowedContentLengthInBytes;
                            if (fileExtn == ".jpg" || fileExtn == ".jpeg" || fileExtn == ".gif" || fileExtn == ".png")
                            {
                                AllowedContentLengthInBytes = Convert.ToInt32(imagesize);
                            }
                            else
                            {
                                AllowedContentLengthInBytes = Convert.ToInt32(pdffilesize);
                            }
                            //int AllowedContentLengthInMB = 3;
                            //int AllowedContentLengthInBytes = AllowedContentLengthInMB * 1024 * 1024;
                            if (contentLength > AllowedContentLengthInBytes)
                            {
                                // Label1.Text = "Document size can't be greater then 1 MB.";
                                GrdUpload.Rows[i].Cells[1].FindControl("Label1").Visible = true;
                                FileStatus_1 = 1;
                            }
                            else
                            {
                                // Label1.Text = "";
                                FileStatus_1 = 2;
                            }

                            if (FileStatus_1 == 2)
                            {

                                fileUpload.PostedFile.SaveAs(filePath + newFileName);
                                //2.SUCCESSSFULLY INSERTED, 3. ERROR, 4. SUCCESSFULLY UPDATED
                                ErrorId = 0;
                                objCompany = new clsCompany();
                                System.Data.DataTable dt = new System.Data.DataTable();
                                dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);
                                foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
                                {

                                    // Console.WriteLine("--- Row ---"); // Print separator.

                                    int a = 0;
                                    foreach (var item in row.ItemArray) // Loop over the items.
                                    {
                                        if (a == 0)
                                        {
                                            UnitId = Convert.ToInt32(item);
                                            if (FormType != 3)
                                            {
                                                ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 1, DocName, newFileName, OriginalFilePath, UserId);
                                            }
                                            else
                                            {
                                                ErrorId = objCompany.Company_Insert_Step5_Documents(0, UnitId, 3, DocName, newFileName, OriginalFilePath, UserId);
                                            }
                                        }

                                        a++;
                                    }
                                }

                                dt.Clear();
                                dt.Dispose();
                            }
                        }
                    }
                    i = i + 1;
                }
            }

            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);

            }

        }

    }
}