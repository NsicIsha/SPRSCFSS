using AjaxControlToolkit;
using NSICGP_VBClassLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class appAdministrator_RegisterIssue : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName, Userpwd, Branch_Name;
    int BranchId = 0;

    protected void Page_Init(object sender, EventArgs e)
    {
        //UpdatePanel UpdatePanel1_PlzWait = (this.Master.FindControl("UpdatePanel1_PlzWait")) as UpdatePanel;
        //if (UpdatePanel1_PlzWait != null)
        //{
        //    AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
        //    trigger.ControlID = btnSubmit.UniqueID;
        //    trigger.EventName = "Click";

        //    UpdatePanel1_PlzWait.Triggers.Add(trigger);
        //}
    }

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
        FunRequest();

        UpdatePanel updatePanel = Page.Master.FindControl("UpdatePanel1_PlzWait") as UpdatePanel;
        UpdatePanelControlTrigger trigger = new PostBackTrigger();
        trigger.ControlID = btnSubmit.UniqueID;
        updatePanel.Triggers.Add(trigger);

        if (!Page.IsPostBack)
        {
            clsMasterBranch objbranch = new clsMasterBranch();
            DataTable dt = new DataTable();
            dt = objbranch.MasterBranch_Fetch_Address(UserId);
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());
            Branch_Name = objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString();

            if (dt.Rows.Count > 0)
            {
                // txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                txtEmail.Text = dt.Rows[0]["EMAILID"].ToString();
                txtBugReporter.Text = Branch_Name;
                // txtFax.Text = dt.Rows[0]["FAXNO"].ToString();
                // txtPhoneNo.Text = dt.Rows[0]["PHONE"].ToString();
                // hfbranchid.Value = dt.Rows[0]["BranchID"].ToString();
            }

        }
    }
    private void MsgBox(object sender, string message)
    {
        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlConnection sqlConn = null;
        SqlTransaction transaction = null;

        try
        {
            if (Valid())
            {
                sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString());
                sqlConn.Open();
                transaction = sqlConn.BeginTransaction();

                SqlCommand command = sqlConn.CreateCommand();
                command.Transaction = transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "InsertInBugTracker";

                // Parameters
                command.Parameters.AddWithValue("@Bug_Title", txtBugTitle.Text);
                command.Parameters.AddWithValue("@ProjectName", ddlProjectName.SelectedValue);
                command.Parameters.AddWithValue("@Bug_Reporter", txtBugReporter.Text);
                command.Parameters.AddWithValue("@Bug_Status", "Open");
                command.Parameters.AddWithValue("@Bug_Priority", ddlBugPriority.SelectedValue);
                command.Parameters.AddWithValue("@Bug_STR", txtSTR.Text);
                command.Parameters.AddWithValue("@ExpectedResult", string.Empty);
                command.Parameters.AddWithValue("@ActualResult", string.Empty);
                command.Parameters.AddWithValue("@Notes", txtNotes.Text);
                command.Parameters.AddWithValue("@Bug_PAN", txtBugPan.Text);
                command.Parameters.AddWithValue("@Bug_Severity", string.Empty);
                command.Parameters.AddWithValue("@Bug_AssignedTo", ddlAssignedTo.SelectedValue);
                command.Parameters.AddWithValue("@targetEmailID", txtEmail.Text);

                // Determine BranchId
                clsTrans_UserBranch objtub = new clsTrans_UserBranch();
                int BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MB_Id"].ToString());
                command.Parameters.AddWithValue("@BranchID", BranchId);

                // File handling
                string fName = string.Empty;
                if (!string.IsNullOrEmpty(fileuploadAnnexureD.PostedFile.FileName))
                {
                    Random random = new Random();
                    int num = random.Next();

                    var folder = System.Configuration.ConfigurationManager.AppSettings["MasterPath"].ToString();
                    string fileextension = Path.GetExtension(fileuploadAnnexureD.PostedFile.FileName);
                    fName = DateTime.Now.ToFileTime() + fileextension;
                    int iFileSize = fileuploadAnnexureD.PostedFile.ContentLength;
                    if (iFileSize > 6048576)
                    {
                        string sizeMsg = "alert('Upload size should not greater than 5MB')";
                        MsgBox(sender, sizeMsg);
                        return;
                    }
                    fileuploadAnnexureD.SaveAs(Path.Combine(folder, fName));
                }
                command.Parameters.AddWithValue("@FileName", fName ?? string.Empty);

                // Execute
                command.ExecuteNonQuery();
                transaction.Commit();
                string message = "alert('Bug Added Successfully')";
                MsgBox(sender, message);
            }
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            // log or handle error
        }
        finally
        {
            if (sqlConn != null)
            {
                sqlConn.Close();              
                ResetBugForm();
            }
        }
    }
    
    private void ResetBugForm()
    {
        txtBugTitle.Text = string.Empty;
        ddlProjectName.SelectedIndex = 0;       
        ddlBugPriority.SelectedIndex = 0;
        txtSTR.Text = string.Empty;
        txtNotes.Text = string.Empty;
        txtBugPan.Text = string.Empty;       
    }

    private bool Valid()
    {
        bool result = false;
        int  lenTitle = txtBugTitle.Text.Length;
        if (lenTitle>500||string.IsNullOrEmpty(txtBugTitle.Text) || ddlProjectName.SelectedIndex == -1 || ddlBugPriority.SelectedIndex == -1 || string.IsNullOrEmpty(txtSTR.Text)
          || string.IsNullOrEmpty(txtBugPan.Text))
        {
            result = false;
        }
        else
        {
            result = true;
        }
        return result;
    }

}