using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterInspectionAgencyUser : System.Web.UI.Page
{
    int UserId, RoleId;
    string UserFullName;
    clsMasterInspectionAgency objAgency;
    DataSet Myds;
    int successId = 0;
    public int userAction=0;
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
                    GridViewDataBind(0);

                    DropDownAgencyBind();

                }
            }
            else
            {
                btnAddUser.Visible = false;
            }
            
            //if (Page.IsPostBack != true)
            //{

            //    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
            //    mvUser.SetActiveView(ViewUserDetail);
            //    GridViewDataBind(0);

            //    DropDownAgencyBind();

            //}
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            //DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    protected void GridViewDataBind(int agencyid)
    {
        objAgency=new clsMasterInspectionAgency();
        try
        {
            if (agencyid == 0)
            {
                gvUserDetails.DataSource = objAgency.MasterInspectionAgency_User_Fetch_DataSet();
                gvUserDetails.DataBind();
            }
            else
            {
                gvUserDetails.DataSource = objAgency.MasterInspectionAgency_User_FetchByAgencyId_DataSet(agencyid);
                gvUserDetails.DataBind();
            }

            /*if (gvUserDetails.Rows.Count == 0)
            {
                mvUser.SetActiveView(ViewUserNotFound);
                lblMessage.Text = "No Agency user found!";
            }*/
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

    protected void DropDownAgencyBind()
    {
        objAgency = new clsMasterInspectionAgency();
        Myds = new DataSet();
        Myds = objAgency.MasterInspectionAgency_Fetch_Activated_DataSet();

        ddlAgency.DataSource = Myds;
        DropDownListAgency.DataSource = Myds;
        ddlAgency.DataTextField = Myds.Tables[0].Columns["fGPtbl_MIA_Name"].ToString();
        DropDownListAgency.DataTextField = Myds.Tables[0].Columns["fGPtbl_MIA_Name"].ToString();
        ddlAgency.DataValueField = Myds.Tables[0].Columns["fGPtbl_MIA_Id"].ToString();
        DropDownListAgency.DataValueField = Myds.Tables[0].Columns["fGPtbl_MIA_Id"].ToString();
        ddlAgency.DataBind();
        DropDownListAgency.DataBind();
        ddlAgency.Items.Insert(0, new ListItem("--Select Agency--", "0"));
        DropDownListAgency.Items.Insert(0, new ListItem("--Select Agency--", "0"));
        //DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      

        objAgency = new clsMasterInspectionAgency();
        /*successId = objAgency.MasterInspectionAgency_User_InsertUpdate(0, Convert.ToInt32(ddlAgency.SelectedValue),
            txtName.Text.Trim(), txtEmail.Text.Trim(), txtInstr.Text.Trim(), txtLoginame.Text.Trim(), UserId);
        */
        if (HdnfAddEdit.Value == "add")
        {
            successId = objAgency.MasterInspectionAgency_User_InsertUpdate(0, Convert.ToInt32(DropDownListAgency.SelectedValue),
                txtName.Text.Trim(), txtEmail.Text.Trim(), txtInstr.Text.Trim(), txtLoginame.Text.Trim(), UserId);
        }
        else if (HdnfAddEdit.Value == "edit")
        {
            successId = objAgency.MasterInspectionAgency_User_InsertUpdate(Convert.ToInt32(HdnfUserId.Value),0,
                txtName.Text.Trim(), txtEmail.Text.Trim(), txtInstr.Text.Trim(), "", UserId);

        }

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
        GridViewDataBind(0);
        
    }


    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        showhide("add");
        HdnfAddEdit.Value = "add";
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

    protected void ddlAgency_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewDataBind(Convert.ToInt32(ddlAgency.SelectedValue));
    }

    protected void gvUserDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objAgency = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            if (e.CommandName == "edp")
            {
                HdnfUserId.Value = e.CommandArgument.ToString();
                HdnfAddEdit.Value = "edit";
                Myds = objAgency.MasterInspectionAgency_User_FetchById_DataSet(Convert.ToInt32(e.CommandArgument));
                showhide("edit");
                lblAgency.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString();
                lblLoginame.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIAU_UserName"].ToString();
                txtName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIAU_Name"].ToString();
                txtEmail.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIAU_Email"].ToString();
                txtInstr.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIAU_Description"].ToString();
                mvUser.SetActiveView(viewedituser);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void showhide(string addedit)
    {
        if (addedit == "edit")
        {
            DropDownListAgency.Visible = false;
            txtLoginame.Visible = false;
        }
        else if (addedit == "add")
        {
            DropDownListAgency.Visible = true;
            txtLoginame.Visible = true;
        }
    }
}
