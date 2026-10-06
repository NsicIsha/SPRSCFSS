using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_MasterInstructionView : System.Web.UI.Page
{
    clsMasterInstruction objInst;
    DataSet Myds;
    int successId;
    int formId;

    int UserId, RoleId, DepartmentId;
    string UserFullName;
    // public int InstAction = 0;

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
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    try
                    {
                        if (Request.QueryString["id"] != null)
                        {
                            objInst = new clsMasterInstruction();
                            Myds = new DataSet();
                            Myds = objInst.MasterInstruction_Fetch_DataSet(Convert.ToInt32(Request.QueryString["id"]));

                            HiddenField1.Value = Myds.Tables[0].Rows[0][1].ToString();

                            lblName.Text = Myds.Tables[0].Rows[0][2].ToString();

                            RichTextEditor_1_1.txtRichTextBox_1 = Myds.Tables[0].Rows[0][3].ToString();
                        }

                    }
                    catch (Exception ex)
                    {
                        ErrHandler.WriteError(ex.Message);
                    }
                }
            }
            else
            {
                RichTextEditor_1_1.Visible = false;
                btnBack.Visible = false;
                btnSubmit.Visible = false;
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
    }

  
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objInst = new clsMasterInstruction();
            successId = objInst.MasterInstruction_InsertUpdate(Convert.ToInt32(Request.QueryString["id"]),Convert.ToInt32(HiddenField1.Value), RichTextEditor_1_1.txtRichTextBox_1, UserId);
            if (successId == 4)
                Session["InstAction"] = "Updated Successfully";
            else if (successId == 3)
                Session["InstAction"] = "Server is busy";
            else
                Session["InstAction"] = "Server is busy";

            Response.Redirect("../appAdministrator/MasterInstruction.aspx", false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("../appAdministrator/MasterInstruction.aspx", false);
    }
}
