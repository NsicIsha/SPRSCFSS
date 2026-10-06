using System;
using System.Collections;
using System.Collections.Generic;
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


public partial class appAdministrator_MasterUser : System.Web.UI.Page
{
    DataSet Myds;
    DataTable dt;
    clsMasterZone objZone;
    clsMasterBranch objBranch;
    clsMasterUser objUser;
    clsTrans_UserBranch objTub;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int userAction = 0;
    int count = 1;
    
    int accessId;
    int id;
    string FromMail, ccMail, bccMail, subject, bodyMail;
    ArrayList resetarr = new ArrayList();

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

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    mvUser.SetActiveView(ViewUserDetail);
                    DropDownRoleBind();
                    GridViewDataBind(0, "");

                }
            }
            else
            {
                btnAddUser.Visible = false;
                txtUser.Visible = false;
                DropDownListRole.Visible = false;
                btnSearch.Visible = false;
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

  

    public void GridViewDataBind(int RoleId, string UserName)
     {
         objUser = new clsMasterUser();

         try
         {
                 gvUserDetails.DataSource = objUser.MasterUser_FetchByRoleUserName_DataSet(RoleId, UserName);
                 gvUserDetails.DataBind();
                 mvUser.SetActiveView(ViewUserDetail);

             if (gvUserDetails.Rows.Count == 0)
             {
                 mvUser.SetActiveView(ViewUserNotFound);
                 lblMessage.Text = "No User found!";
             }
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         try
         {
             if (Session["userAction"] != null)
             {
                 LabelAction.Text = Session["userAction"].ToString();
                 userAction = 1;
                 Session["userAction"] = null;
             }
             else
             {
                 LabelAction.Text = "";
                 userAction = 0;
                 Session["userAction"] = null;
             }
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message);
         }
         finally
         {
             Session["userAction"] = null;
         }
     }

    public void DropDownRoleBind()
    {
        dt = new DataTable();
        dt = clsDDL.ddlRole();
        DropDownListRole.DataSource = dt;
        DropDownListRole.DataTextField = dt.Columns["fGPtbl_MR_Name"].ToString();
        DropDownListRole.DataValueField = dt.Columns["fGPtbl_MR_Id"].ToString();
        DropDownListRole.DataBind();
        DropDownListRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
        DropDownListRole.SelectedValue = "0";
    }



    protected void gvUserDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;

                ImageButton imbact = (ImageButton)e.Row.FindControl("imbAct");
                ImageButton imbedit = (ImageButton)e.Row.FindControl("imbEdit");
                ImageButton imbview = (ImageButton)e.Row.FindControl("imbView");
                ImageButton imbreset = (ImageButton)e.Row.FindControl("imbReset");
                
                HiddenField hdnroleid = (HiddenField)e.Row.FindControl("hdnRoleId");

                

                // SETTING ACTIVE AND DEACTIVE CAPTION BY CHECKING VALUE
                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "Active";
                    imbact.ImageUrl = "../Images/Admin/Deactive.png";
                    imbact.AlternateText = "Deactivate";
                    imbact.ToolTip = "Deactivate This";
                    imbreset.Visible = true;
                    imbreset.Attributes.Add("onClick", "return confirm('Are you sure you want to reset password?');");

                }
                else if (e.Row.Cells[5].Text == "1")
                {
                    e.Row.Cells[5].Text = "Deactive";
                    imbact.ImageUrl = "../Images/Admin/Active.png";
                    imbact.AlternateText = "Activate";
                    imbact.ToolTip = "Activate This";
                    e.Row.CssClass = "dynRowDeactivate";

                    imbedit.ToolTip = "USER IS DEACTIVE FOR THIS. IF YOU WISH TO EDIT THIS, YOU HAVE TO ACTIVATE THE USER FOR THIS FIRST.";
                    imbedit.Enabled = false;
                    imbreset.Visible = false;
                }

                if (hdnroleid.Value == "1" || hdnroleid.Value == "5")
                {
                    imbview.Visible = false;
                    imbact.Visible = false;
                    imbedit.Visible = false;
                    imbreset.Visible = false;
                }


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
        if (count == 0)
        {
            mvUser.SetActiveView(ViewUserNotFound);
            lblMessage.Text = "No User found!";
        }
    }
    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;

        try
        {

            //THIS IS FOR ACTIVATE AND DEACTIVATE
            if (e.CommandName == "Act")
            {
                objUser = new clsMasterUser();

                DataSet Myds = new DataSet();

                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = objUser.MasterUser_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                
                accessId = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MU_AccessId"]);

                if (accessId == 0)//TO DEACTIVATE
                {
                    act = objUser.MasterUser_ActivateById(Convert.ToInt32(e.CommandArgument), UserId, "active");
                    if (act == 4)
                        Session["userAction"] = "Deactivated Successfully";
                    else
                        Session["userAction"] = "Server is Busy";
                }
                else //TO ACTIVATE
                {
                    act = objUser.MasterUser_ActivateById(Convert.ToInt32(e.CommandArgument), UserId, "deactive");
                    //TO SHOW THE MESSAGE
                    if (act == 4)
                        Session["userAction"] = "Activated Successfully";
                    else
                        Session["userAction"] = "Server is Busy";
                }
                //BINDING GRIDVIEW
                GridViewDataBind(0, "");
            }
            else if (e.CommandName == "Reset")  // TO RESET PASSWORD
            {
                objUser = new clsMasterUser();
                
                resetarr = objUser.MasterUser_ForgotPassword(e.CommandArgument.ToString());
                id = Convert.ToInt32(resetarr[0]);

                FromMail = ConfigurationManager.AppSettings["FP_From"];
                ccMail = ConfigurationManager.AppSettings["FP_Cc"];
                bccMail = ConfigurationManager.AppSettings["FP_Bcc"];
                subject = ConfigurationManager.AppSettings["FP_Subject"];
                bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +arl[2].ToString() + ".";

                string bodystr = bodyMail.Replace("$$password$$", resetarr[2].ToString());

                switch (id)
                {
                    case 2://Password successfully changed
                        //SendMail.Mail_WithOUTAttachment(resetarr[1].ToString(), 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0,"","","");
                        Session["userAction"] = "Password reset successfully";
                        break;
                    case 3://Error in operation
                        Session["userAction"] = "Server is busy please try later";
                        break;
                }
                GridViewDataBind(0, "");

            }
            else if (e.CommandName == "edt")
            {
                Response.Redirect("MasterUserProfile.aspx?act=2&actid=" + e.CommandArgument, false);
            }
            else if (e.CommandName == "view")
            {
                Response.Redirect("MasterUserProfile.aspx?act=3&actid=" + e.CommandArgument, false);
            }




        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GridViewDataBind(Convert.ToInt32(DropDownListRole.SelectedValue) , txtUser.Text);
    }





    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterUserProfile.aspx?act=1&actid=0", false);
    }
}
