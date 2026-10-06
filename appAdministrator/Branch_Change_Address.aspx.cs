using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Branch_Change_Address : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName, Userpwd;
    clsMasterBranch objbranch;
    DataTable dt;
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
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            FunRequest();
            if (!Page.IsPostBack)
            {
                objbranch = new clsMasterBranch();
                dt = new DataTable();
                dt = objbranch.MasterBranch_Fetch_Address(UserId);
                if (dt.Rows.Count > 0)
                {
                    txtAddress.Text = dt.Rows[0]["ADDRESS"].ToString();
                    txtEmailId.Text = dt.Rows[0]["EMAILID"].ToString();
                    txtFax.Text = dt.Rows[0]["FAXNO"].ToString();
                    txtPhoneNo.Text = dt.Rows[0]["PHONE"].ToString();
                    hfbranchid.Value = dt.Rows[0]["BranchID"].ToString();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            F_UPDATE_BRANCH_ADDRESS();
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError(EX.Message);
        }
    }

    private void F_UPDATE_BRANCH_ADDRESS()
    {
        try
        {
            int RESULTID;
            int BRANCAHid = Convert.ToInt32(hfbranchid.Value);
            objbranch = new clsMasterBranch();
            RESULTID = objbranch.F_BRANCH_ADDRESSUPDATE(BRANCAHid, txtAddress.Text.Trim(),
                txtPhoneNo.Text.Trim(), txtFax.Text.Trim(), txtEmailId.Text.Trim());

            if (RESULTID == 1)
            {
                lblMessage.Text = "Address updated successfully";
                lblMessage.Focus();
               
            }
            else
            {
                lblMessage.Text = "Server is busy please try later";
                lblMessage.Focus();
            }
           
        }
        catch (Exception EX)
        {
            ErrHandler.WriteError(EX.Message);
        }
    }

    

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx", false);
    }
}
