using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.Data.SqlClient;
using System.Web.Services;
using System.Globalization;

public partial class appAdministrator_NewUnitRegistrationOnline_UploadDocument : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;
    int Unitid = 0;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;
    int CompanyId, UnitId, Status, BranchId;
    int FormType;
    //int ErrorId;
    int FileStatus_1, FileStatus_2, FileStatus_3, FileStatus_4, FileStatus_5, FileStatus_6, FileStatus_7, FileStatus_8, FileStatus_9, FileStatus_10;
    string OriginalFilePath;
    string filePath;
    string urlid;
    int ConstitutionId = 0;

    protected void Page_Load(object sender, EventArgs e)
    {


        //string strMale = "FeMale";
        //DropDownList1.Items.FindByText(strMale).Selected = true;


        Page.Form.Attributes.Add("enctype", "multipart/form-data");

        FormType = 1;
        if (FormType != 3)
        {
            OriginalFilePath = "./Document/Upload/Registration/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Registration/";
        }
        else
        {
            OriginalFilePath = "./Document/Upload/Amendment/";
            filePath = Server.MapPath(".").ToString() + "/../Document/Upload/Amendment/";
        }
    }
    void FillGrid()
    {
        //BINDING UPLOADED GRIDVIEW
        int Companyid = Convert.ToInt32(Request.QueryString["uid"]);

        clsCompany objCompany = new clsCompany();
        MyDs = new DataSet();
        MyDs = objCompany.Company_UnitDetails_Fetch_ByCompanyId(Companyid);
        //Mydt = Myds.Tables[0];

        //Mydt2 = Myds.Tables[2];
        //Mydt3 = Myds.Tables[3];

        //GETTING UNIT ID   // Comment by Ravish Kumar Pandey
        if (MyDs.Tables[0].Rows.Count > 0)
        {
            if (MyDs.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString() != null)
            {
                Unitid = Convert.ToInt32(MyDs.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);
            }
            else
            {

            }
        }

        gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid, 1);//formtypeId);
        gvDocumentDetail.DataBind();
    }
    protected void rdbMsmeRegtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMsmeRegtype.SelectedValue == "1")
        {
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;

            // RegularExpressionValidatorAAdhar.Enabled = true;
            // ValidatorCalloutExtender9.Enabled = true;

            RegularExpressionValidator1.Enabled = false;
            // ValidatorCalloutExtender12.Enabled = false;

            //  Requ_addhar.Enabled = true;
            //  Validator_aadhar_number.Enabled = true;
            RegularExpressionValidator3.Enabled = true;

            RequiredFieldValidator18.Enabled = false;

            RequiredFieldValidator14.Enabled = true;
            //  ValidatorCalloutExtender15.Enabled = false;

            divUAMPAN.Visible = true;

            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;
            RegularExpressionValidator3.Enabled = true;
            RegularExpressionValidator4.Enabled = false;

            //  trMainData.Visible = false;
        }
        else if (rdbMsmeRegtype.SelectedValue == "2")
        {
            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;

            //  RegularExpressionValidatorAAdhar.Enabled = false;
            // ValidatorCalloutExtender9.Enabled = false;

            RegularExpressionValidator1.Enabled = true;
            //  ValidatorCalloutExtender12.Enabled = true;
            RegularExpressionValidator3.Enabled = false;
            RegularExpressionValidator4.Enabled = true;

            //    Requ_addhar.Enabled = false;
            //Validator_aadhar_number.Enabled = false;

            RequiredFieldValidator18.Enabled = true;
            RequiredFieldValidator14.Enabled = false;
            //  ValidatorCalloutExtender15.Enabled = true;

            divUAMPAN.Visible = true;

            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;
            //trMainData.Visible = false;
        }
    }    
    public DataTable Company_Fetch_Certificate_Search(string UAM_UDYAM, string PAN)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_Unit_DocumentUpload_Fresh", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@UAM_UDYAM", SqlDbType.VarChar, 50).Value = UAM_UDYAM;
            MyAdp.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar, 25).Value = PAN;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }
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

                cnt = CountMasterDoc(Unitid);

                while (i < cnt)
                {
                    FileUpload fileUpload = gvDocumentDetail.Rows[i].FindControl("FileUpload1") as FileUpload;
                    DocName = gvDocumentDetail.Rows[i].Cells[1].Text.ToString();
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
                                gvDocumentDetail.Rows[i].Cells[1].FindControl("Label1").Visible = true;
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
    public int CountMasterDoc(int UnitId)
    {
        int MaxId = 0;
        try
        {
            MyDs = new DataSet();
            MySqlComm = new SqlCommand("GPsp_Fetch_MasterUploadDocument_Reject_Fresh", DBManager.ConnectToDB());
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
            MyAdp.Dispose();
        }
        return MaxId;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            MyDs = new DataSet();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");
                hlname.NavigateUrl = "../" + hfpath.Value.ToString() + hfname.Value.ToString();
            }
        }


        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        objCompany = new clsCompany();

        int i = (CountMasterDoc(Unitid) - 1);
        int j = e.Row.RowIndex;

        if (j == i)
        {
            e.Row.Cells[1].FindControl("btnSubmit").Visible = true;
            //e.Row.Cells[1].FindControl("btnBack").Visible = true;
        }
    }
    protected void SearchGridviewBind()
    {
        if (txtPan.Text != "" && txtUAM.Text != "")
        {
            string SearchCriteria = string.Empty;
            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = Company_Fetch_Certificate_Search(txtUAM.Text, txtPan.Text);
            ds.Tables.Add(dt);
            Unitid = Convert.ToInt32(ds.Tables[0].Rows[0]["fgptbl_du_unitid"].ToString());

            gvDocumentDetail.DataSource = dt;// objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(88534, 1);//formtypeId);
            gvDocumentDetail.DataBind();



            //GrdUpload.DataSource = dt;
            //GrdUpload.DataBind();
            if (gvDocumentDetail.Rows.Count == 0)
            {
                ltlMessage.Text = "No Data Found";
            }
            else
            {
                ltlMessage.Text = " ";
            }
        }
    }
    protected bool ValidUploads(int count)
    {
        int p = 0;
        bool isvalid = true;
        Decimal sizeOfDoc;
        while (p < count)
        {
            FileUpload fileUpload = gvDocumentDetail.Rows[p].FindControl("FileUpload1") as FileUpload;

            if (!fileUpload.HasFile)
            {
                isvalid = false;
                lblMsg.Text = "Please upload scanned copy of " + gvDocumentDetail.Rows[p].Cells[1].Text;
                //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload scanned copy of PAN.');", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('" + lblMsg.Text + "');", true);

                return isvalid;


            }
            else
            {
                string ssss = gvDocumentDetail.Rows[p].Cells[1].Text;

                // sizeOfDoc = Convert.ToDecimal(gvDocumentDetail.Rows[p].Cells[1].Text);
                sizeOfDoc = 15;
                sizeOfDoc = sizeOfDoc * 125000;

                sizeOfDoc = sizeOfDoc * 1000000;

                // Get the size in bytes of the file to upload.
                int fileSize = fileUpload.PostedFile.ContentLength;

                if (fileSize > sizeOfDoc)
                {

                    isvalid = false;   //"size of " + GrdUpload.Rows[p].Cells[1].Text + +  "." 
                    lblMsg.Text = "Please upload " + gvDocumentDetail.Rows[p].Cells[1].Text + gvDocumentDetail.Rows[p].Cells[3].Text;
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        if (txtPan.Text != "" && txtUAM.Text != "")
        {
            string SearchCriteria = string.Empty;
            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            dt = Company_Fetch_Certificate_Search(txtUAM.Text, txtPan.Text);
            ds.Tables.Add(dt);
            Unitid = Convert.ToInt32(ds.Tables[0].Rows[0]["fgptbl_du_unitid"].ToString());
            CompanyId= Convert.ToInt32(ds.Tables[0].Rows[0]["CompanyId"].ToString());
        }
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
            //cnt = CountMasterDoc();

            cnt = CountMasterDoc(Unitid);


            bool isValid =ValidUploads(cnt);// true;// ValidUploads(cnt);
            if (isValid)
            {
                while (i < cnt)
                {
                    FileUpload fileUpload = gvDocumentDetail.Rows[i].FindControl("FileUpload1") as FileUpload;
                    DocName = gvDocumentDetail.Rows[i].Cells[1].Text.ToString();
                    //UDYAM-UP-02-0000424    ASSPC7703L
                    if (i == 0)
                    {
                        if (!fileUpload.HasFile)
                        {
                            lblMsg.Text = "Please upload all scanned documents.";
                            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please upload all scanned documents.');", true);
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
                                // Label1.Text = "Document size can't be greater then 1 MB.";
                                gvDocumentDetail.Rows[i].Cells[1].FindControl("Label1").Visible = true;
                                if (filerecognige == 1)
                                {
                                    // ((Label)GrdUpload.Rows[i].Cells[1].FindControl("Label1")).Text = "File size can't be greater than 50kb ";
                                }
                                else
                                {
                                    // ((Label)GrdUpload.Rows[i].Cells[1].FindControl("Label1")).Text = "File size can't be greater than 1MB ";
                                }

                                FileStatus_1 = 2;
                                // return;
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
                                                ErrorId = Company_Insert_Step5_Documents(0, UnitId, 1, DocName, newFileName, OriginalFilePath, UserId);
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




                int aa = Company_Update_Status_After_Documents_Upload(CompanyId);
                lblMsg.Text = "Documents Updated Successfully";
                string ReferenceNo, compstr = string.Empty;
                if (FormType == 1)
                {
                    //ReferenceNo = Company_Insert_Step5_Online(CompanyId, UnitId, 1);
                    //DecryptEncrypt objdecrypt = new DecryptEncrypt();
                    //compstr = objdecrypt.Encrypt(Convert.ToString(CompanyId));
                    //Session["ReferenceNo"] = ReferenceNo;
                    //DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    //string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                    //Response.Redirect("ThankyouOnline.aspx?id=" + compstr, false);
                }


            }      //IF VALID CLOSE    


        } // TRY CLOSE

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    public int Company_Insert_Step5_Documents(int fGPtbl_DU_RenewalAmendmentId, int fGPtbl_DU_UnitId, int fGPtbl_DU_FormtypeId, string fGPtbl_DU_Name, string fGPtbl_DU_FileName, string fGPtbl_DU_FilePath, int UserId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step5_Documents", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_DU_RenewalAmendmentId", SqlDbType.Int).Value = fGPtbl_DU_RenewalAmendmentId;
            MySqlComm.Parameters.Add("@fGPtbl_DU_UnitId", SqlDbType.Int).Value = fGPtbl_DU_UnitId;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FormtypeId", SqlDbType.Int).Value = fGPtbl_DU_FormtypeId;

            MySqlComm.Parameters.Add("@fGPtbl_DU_Name", SqlDbType.VarChar, 7000).Value = fGPtbl_DU_Name;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FileName", SqlDbType.VarChar, 1000).Value = fGPtbl_DU_FileName;
            MySqlComm.Parameters.Add("@fGPtbl_DU_FilePath", SqlDbType.VarChar, 2000).Value = fGPtbl_DU_FilePath;

            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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
    public int Company_Update_Status_After_Documents_Upload(int CompanyId)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Update_Unit_DocumentUpload_Fresh", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
             
           
            ErrorId = MySqlComm.ExecuteNonQuery();
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
    protected void btnf_Click(object sender, EventArgs e)
    {
        try
        {
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}