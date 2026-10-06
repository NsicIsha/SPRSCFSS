using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class MasterBranch : System.Web.UI.Page
{
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    clsTrans_UserBranch objTUB;
    DataSet Myds;
    int accessId;
    int count = 1;
    //int Zoneid;
    //int Byid;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int branchAction = 0;



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
        Myds = new DataSet();
        objZone = new clsMasterZone();
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            if (RoleId == 1 || RoleId == 2)
            {
                if (Page.IsPostBack != true)
                {
                    
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvBranch.SetActiveView(ViewBranchDetail);
                    GridViewDataBind();

                    DropDownZoneBind(ddlZone);

                    if (RoleId == 2)  //AS ZONAL HEAD
                    {
                        DropDownZoneDataBind(ddlZone,UserId);
                    }

                }
            }
            else
            {
                btnAddBranch.Visible = false;
            }

            
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


    public void GridViewDataBind()
    {
        objBranch=new clsMasterBranch();
        try
        {
            gvBranch.DataSource = objBranch.MasterBranch_Fetch_DataSet();
            gvBranch.DataBind();

            if (gvBranch.Rows.Count == 0)
            {
                mvBranch.SetActiveView(ViewBranchNotFound);
                lblMessage.Text = "No Branch found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        try
        {
            if (Session["branchAction"] != null)
            {
                LabelAction.Text = Session["branchAction"].ToString();
                branchAction = 1;
                Session["branchAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                branchAction = 0;
                Session["branchAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["branchAction"] = null;
        }
    }

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        objBranch = new clsMasterBranch();
        
        gvBranch.DataSource = objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(ddlZone.SelectedItem.Value));
        gvBranch.DataBind();
        

        if (gvBranch.Rows.Count == 0)
        {
            mvBranch.SetActiveView(ViewBranchNotFound);
            lblMessage.Text = "No Branch found!";
        }
        
    }

    protected void gvBranch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                Literal ltSno = (Literal)e.Row.FindControl("ltlSno");
                ltSno.Text = count.ToString();
                count++;
                
                ImageButton imgAct = (ImageButton)e.Row.FindControl("imgAct");
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgEdit");
                HiddenField hdnzone = (HiddenField)e.Row.FindControl("HiddenField3");
                
                if (e.Row.Cells[5].Text == "0")
                {
                    e.Row.Cells[5].Text = "Active";
                    imgAct.ImageUrl = "../Images/Admin/Deactive.png";
                    imgAct.AlternateText = "Deactivate";
                    imgAct.ToolTip = "Deactivate This";
                }
                else
                {
                    e.Row.Cells[5].Text = "Deactive";
                    imgAct.ImageUrl = "../Images/Admin/Active.png";
                    imgAct.AlternateText = "Activate";
                    imgAct.ToolTip = "Activate This";
                    e.Row.CssClass = "dynRowDeactivate";

                    imgEdit.Enabled = false;
                    imgEdit.ToolTip = "BRANCH IS DEACTIVE FOR THIS. IF YOU WISH TO EDIT THIS BRANCH, YOU HAVE TO ACTIVATE BRANCH FIRST.";

                    if (hdnzone.Value == "1")
                    {
                        imgAct.Enabled = false;
                        imgAct.ToolTip = "ZONE IS DEACTIVE FOR THIS BRANCH. IF YOU WISH TO ACTIVATE THIS BRANCH, YOU HAVE TO ACTIVATE ZONE FOR THIS BRANCH FIRST.";
                        imgEdit.Enabled = false;
                        imgEdit.ToolTip = "ZONE IS DEACTIVE FOR THIS BRANCH. IF YOU WISH TO EDIT THIS BRANCH, YOU HAVE TO ACTIVATE ZONE FOR THIS BRANCH FIRST.";
                    }
                  
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
            mvBranch.SetActiveView(ViewBranchNotFound);
            lblMessage.Text = "No Branch found!";
        }
    }

    //COMMON FUNCTION TO BIND BOTH ZONE DDL
   public void DropDownZoneBind(DropDownList ddl)
   {
       Myds = new DataSet();
       objZone = new clsMasterZone();
       Myds = objZone.MasterZone_Fetch_DataSet_Activated();

       ddl.DataTextField = Myds.Tables[0].Columns[1].Caption;
       ddl.DataValueField = Myds.Tables[0].Columns[0].Caption;
       ddl.DataSource = Myds.Tables[0];
       ddl.DataBind();
       ddl.Items.Insert(0, new ListItem("--Select Zone--", "0"));
   }   

    protected void gvBranch_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;

        try
        {
            
            //THIS IS FOR EDITING
            if (e.CommandName == "edt")
            {
                lblAddEdit.Text = "Edit Branch";
                objBranch = new clsMasterBranch();
                DataSet Myds = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objBranch.MasterBranch_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             BranchName = n.Field<string>("fGPtbl_MB_Name"),
                             Code=n.Field<string>("fGPtbl_MB_Code"),
                             BranchDescription = n.Field<string>("fGPtbl_MB_Desc"),
                             ZoneId=n.Field<int>("fGPtbl_MB_ZoneId"),
                             Zonename=n.Field<string>("ZoneName"),
                             StatusId = n.Field<int>("fGPtbl_MB_AccessId"),
                             
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    lblBranchName.Text =": " + onl.BranchName;
                    txtBranchName.Text = onl.BranchName;
                    txtCode.Text = onl.Code;
                    txtDescription.Text = onl.BranchDescription;
                    HiddenField2.Value = onl.Userid.ToString();
                    HiddenField4.Value=onl.ZoneId.ToString();
                    lblZonename.Text = onl.Zonename;
                    accessId = onl.StatusId;
                    
                }

                DropDownZoneBind(DropDownListZone);
                DropDownListZone.SelectedValue = HiddenField4.Value;

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();

            }
            //THIS IS FOR ACTIVATE AND DEACTIVATE
            else if (e.CommandName == "Act")
            {
                objBranch = new clsMasterBranch();
                ImageButton imgact = (ImageButton)e.CommandSource;
                DataSet Myds = new DataSet();
                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();
                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = objBranch.MasterBranch_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             StatusId = n.Field<int>("fGPtbl_MB_AccessId"),
                            // ZoneStatusId = n.Field<int>("fGPtbl_MZ_AccessId"),
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    accessId = onl.StatusId;
                    //zoneaccessId = onl.ZoneStatusId;
                    HiddenField2.Value = onl.Userid.ToString();
                }

                    if (accessId == 0)//TO DEACTIVATE
                    {
                        act = objBranch.MasterBranch_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "active");
                        ddlZone.SelectedValue = "0";
                        Session["branchAction"] = "Deactivated Successfully";
                     
                    }
                    else //TO ACTIVATE
                    {
                        act = objBranch.MasterBranch_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "deactive");
                        //TO SHOW THE MESSAGE
                        ddlZone.SelectedValue = "0";
                        Session["branchAction"] = "Activated Successfully";
                      
                    }
                    
               // }
                
            }
            //BINDING GRIDVIEW
            GridViewDataBind();
          
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void btnAddBranch_Click(object sender, EventArgs e)
    {
        lblAddEdit.Text = "Add New Branch";
        txtBranchName.Text = "";
        txtCode.Text = "";
        txtDescription.Text = "";
        txt_branch_address.Text = "";
        txt_email.Text = "";
        txt_fax.Text = "";
        txt_phone.Text = "";

        hdnfield.Value = "add";
        ModalPopupExtender1.TargetControlID = "btnAddBranch";
        ModalPopupExtender1.Show();
        DropDownZoneBind(DropDownListZone);
        if (RoleId == 2)
        {
            DropDownZoneDataBind(DropDownListZone, UserId);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objBranch = new clsMasterBranch();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = new int();
        string upper;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                upper = txtCode.Text.ToUpper();
                //id = objBranch.MasterBranch_InsertUpdate(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField4.Value), txtBranchName.Text, upper, txtDescription.Text, Convert.ToInt32(HiddenField2.Value), txt_branch_address.Text, txt_phone.Text.ToString(), txt_fax.Text.ToString(), txt_email.Text.ToString());

                //TO CHANGE THE ZONE OF THE BRANCH
                id = objBranch.MasterBranch_InsertUpdate(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(DropDownListZone.SelectedValue), txtBranchName.Text, upper, txtDescription.Text, Convert.ToInt32(HiddenField2.Value), txt_branch_address.Text, txt_phone.Text.ToString(), txt_fax.Text.ToString(), txt_email.Text.ToString());
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                upper = txtCode.Text.ToUpper();
                id = objBranch.MasterBranch_InsertUpdate(0, Convert.ToInt32(DropDownListZone.SelectedItem.Value), txtBranchName.Text, upper, txtDescription.Text, UserId, txt_branch_address.Text.ToString(), txt_phone.Text.ToString(), txt_fax.Text.ToString(), txt_email.Text.ToString());
            }
            switch (id)
            {
                case 1://data already exist
                    Session["branchAction"] = "BRANCH already exist";
                    break;
                case 2://data inserted successfully
                    Session["branchAction"] = "Added Successfully";
                    break;
                case 3:// error in operation
                    Session["branchAction"] = "SERVER Is Busy";
                    break;
                case 4://data updated successfully
                    Session["branchAction"] = "Updated Successfully";
                    break;
                default:// error in operation
                    Session["branchAction"] = "SERVER Is Busy";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        GridViewDataBind();

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //DropDownZoneBind();
        if (RoleId == 2)
        {
            DropDownZoneDataBind(ddlZone, UserId);
        }
    }
   
    //TO BIND DDL ZONE WHEN ROLE-ID IS AS ZONE HEAD
    public void DropDownZoneDataBind(DropDownList ddl, int userid)
    {
        objTUB = new clsTrans_UserBranch();
        objBranch = new clsMasterBranch();
        Myds = new DataSet();

        Myds = objTUB.Trans_UserBranch_FetchZoneId_DataSet(userid);

        ddl.DataSource = Myds.Tables[0];
        ddl.DataTextField = Myds.Tables[0].Columns[2].Caption;
        ddl.DataValueField = Myds.Tables[0].Columns[1].Caption;
        ddl.DataBind();

        ddl.Enabled = false;

        gvBranch.DataSource = objBranch.MasterBranch_FetchByZoneId_DataSet(Convert.ToInt32(Myds.Tables[0].Rows[0][1]));
        gvBranch.DataBind();

        if (gvBranch.Rows.Count == 0)
        {
            mvBranch.SetActiveView(ViewBranchNotFound);
            lblMessage.Text = "No Branch found!";
        }
    }


}
