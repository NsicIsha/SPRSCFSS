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

public partial class appAdministrator_MasterZone : System.Web.UI.Page
{
    clsMasterZone ObjZone;
    int accessId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int zoneAction=0;

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
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvZone.SetActiveView(ViewZoneDetail);
                    GridViewDataBind();
                }
            }
            else
            {
                btnAddZone.Visible = false;
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

    protected void GVZoneDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;

                //Button btnAct = (Button)e.Row.FindControl("btnActivate");
                ImageButton imgAct = (ImageButton)e.Row.FindControl("imgAct");
                ImageButton imgEdit = (ImageButton)e.Row.FindControl("imgButton");

                // SETTING ACTIVE AND DEACTIVE CAPTION BY CHECKING VALUE
                if (e.Row.Cells[3].Text == "0")
                {
                    e.Row.Cells[3].Text = "Active";
                    imgAct.ImageUrl = "../Images/Admin/Deactive.png";
                    imgAct.AlternateText = "Deactivate";
                    imgAct.ToolTip = "Deactivate This";

                }
                else if (e.Row.Cells[3].Text == "1")
                {
                    e.Row.Cells[3].Text = "Deactive";
                    //btnAct.Text = "Activate";
                    imgAct.ImageUrl = "../Images/Admin/Active.png";
                    imgAct.AlternateText = "Activate";
                    imgAct.ToolTip = "Activate This";
                    e.Row.CssClass = "dynRowDeactivate";
                    imgEdit.ToolTip = "ZONE IS DEACTIVE. IF YOU WISH TO EDIT, YOU HAVE TO ACTIVATE THE ZONE FIRST.";
                    imgEdit.Enabled = false;
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
            mvZone.SetActiveView(ViewZoneNotFound);
            lblMessage.Text="No zone found!";
        }
    }

    protected void GVZoneDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;

        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit Zone";
                ObjZone = new clsMasterZone();
                DataSet Myds = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = ObjZone.MasterZone_Fetch_Dataset(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             ZoneName = n.Field<string>("fGPtbl_MZ_Name"),
                             ZoneDescription = n.Field<string>("fGPtbl_MZ_Desc"),
                            // StatusId = n.Field<int>("fGPtbl_MZ_AccessId"),
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    lblZonename.Text = ": " + onl.ZoneName;
                    txtZoneName.Text = onl.ZoneName;
                    txtDescription.Text = onl.ZoneDescription;
                    //accessId = onl.StatusId;
                    HiddenField2.Value = onl.Userid.ToString();
                }

                //SHOWING MODALPOPUPEXTENDER
                    ModalPopupExtender1.Show(); 
                
                    
                

            }
            //THIS IS FOR ACTIVATE AND DEACTIVATE
            else if (e.CommandName == "Act")
            {
                ObjZone = new clsMasterZone();

                DataSet Myds = new DataSet();
                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();
                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = ObjZone.MasterZone_Fetch_Dataset(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             StatusId = n.Field<int>("fGPtbl_MZ_AccessId"),
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    accessId = onl.StatusId;
                    HiddenField2.Value = onl.Userid.ToString();
                }
                if (accessId == 0)//TO DEACTIVATE
                {
                    act = ObjZone.MasterZone_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "active");
                    Session["zoneAction"] = "Deactivated Successfully";
                }
                else //TO ACTIVATE
                {
                    act = ObjZone.MasterZone_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "deactive");
                    //TO SHOW THE MESSAGE
                    Session["zoneAction"] = "Activated Successfully";
                }
                //BINDING GRIDVIEW
                GridViewDataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        ObjZone = new clsMasterZone();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = new int();
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                id = ObjZone.MasterZone_InsertUpdate(Convert.ToInt32(HiddenField1.Value), txtZoneName.Text, txtDescription.Text, Convert.ToInt32(HiddenField2.Value));
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                //id = ObjZone.MasterZone_InsertUpdate(0, txtZoneName.Text, txtDescription.Text, Convert.ToInt32(HiddenField2.Value));
                id = ObjZone.MasterZone_InsertUpdate(0, txtZoneName.Text, txtDescription.Text, UserId);
            }
            switch (id)
            {
                case 1://data already exist
                    Session["zoneAction"] = "NAME ALREADY EXIST";
                    break;
                case 2://data inserted successfully
                    Session["zoneAction"] = "ADDED SUCCESSFULLY";
                    break;
                case 3:// error in operation
                    Session["zoneAction"] = "SERVER IS BUSY";
                    break;
                case 4://data updated successfully
                    Session["zoneAction"] = "UPDATED SUCCESSFULLY";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        GridViewDataBind();
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        ObjZone = new clsMasterZone();
        try
        {

            GVZoneDetails.DataSource = ObjZone.MasterZone_Fetch_DataSet();
            GVZoneDetails.DataBind();
            if (GVZoneDetails.Rows.Count == 0)
            {
                mvZone.SetActiveView(ViewZoneNotFound);
                lblMessage.Text = "No Zone found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["zoneAction"] != null)
            {
                LabelAction.Text = Session["zoneAction"].ToString();
                zoneAction = 1;
                Session["zoneAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                zoneAction = 0;
                Session["zoneAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["zoneAction"] = null;
        }
    }

    //TO SHOW MODALPOPUPEXTENDER
    protected void btnAddZone_Click(object sender, EventArgs e)
    {
        try
        {
            lblAddEdit.Text = "Add New Zone";
            lblZonename.Text = "";
            txtZoneName.Text = "";
            txtDescription.Text = "";
            hdnfield.Value = "add";
            ModalPopupExtender1.TargetControlID = "btnAddZone";
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
