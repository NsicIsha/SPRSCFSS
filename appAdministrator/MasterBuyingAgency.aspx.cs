using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterBuyingAgency : System.Web.UI.Page
{
    int UserId, RoleId;
    string UserFullName;
    DataSet Myds;
    int successId = 0;
    public int userAction = 0;
    clsMasterInspectionAgency objAgency;
    int DepartmentId = 0;
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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            ////DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {

                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvUser.SetActiveView(ViewUserDetail);
                    GridViewDataBind();

                }
            }
            else
            {
                btnAddUser.Visible = false;
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    private void GridViewDataBind()
    {
        try
        {
            objAgency = new clsMasterInspectionAgency();
            gvUserDetails.DataSource = objAgency.MasterBuyingAgency_User_Fetch_DataSet();
            gvUserDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["agencyuserAction"] != null)
            {
                LabelAction.Text = Session["agencyuserAction"].ToString();
                userAction = 1;
                Session["agencyuserAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                userAction = 0;
                Session["agencyuserAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["agencyuserAction"] = null;
            Session.Remove("agencyuserAction");
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objAgency = new clsMasterInspectionAgency();
            successId = objAgency.MasterBuyingAgency_User_InsertUpdate(txtName.Text.Trim(), txtEmail.Text.Trim(), txtInstr.Text.Trim(), txtLoginame.Text.Trim(), UserId);

            switch (successId)
            {
                case 1://data already exist
                    Session["agencyuserAction"] = "Agency user already exist";
                    break;
                case 2://data inserted successfully
                    Session["agencyuserAction"] = "Added Successfully";
                    break;
                case 3:// error in operation
                    Session["agencyuserAction"] = "Server is busy";
                    break;
                case 4://data updated successfully
                    Session["agencyuserAction"] = "Updated Successfully";
                    break;
                default:// error in operation
                    Session["agencyuserAction"] = "Server is busy";
                    break;
            }

            mvUser.SetActiveView(ViewUserDetail);
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        mvUser.SetActiveView(viewedituser);
        txtLoginame.Text = "";
        txtName.Text = "";
        txtEmail.Text = "";
        txtInstr.Text = "";
        btnAddUser.Visible = false;
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        mvUser.SetActiveView(ViewUserDetail);
        btnAddUser.Visible = true;
    }
}
