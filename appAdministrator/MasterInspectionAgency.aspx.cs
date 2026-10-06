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

public partial class appAdministrator_MasterInspectionAgency : System.Web.UI.Page
{

    clsMasterInspectionAgency objInsp;
    int accessId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int inspAction = 0;
    dalVariable objDal = new dalVariable();

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

            if (RoleId==1 || RoleId==2 || RoleId==3)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvInsp.SetActiveView(ViewInspDetail);
                    GridViewDataBind();
                }
            }
            else
            {
                btnAddInsp.Visible = false;
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
    protected void btnAddInsp_Click(object sender, EventArgs e)
    {

        try
        {
            lblAddEdit.Text = "Add New InspectionAgency";
            txtInspName.Text = "";
            txtDescription.Text = "";
            hdnfield.Value = "add";
            ModalPopupExtender1.TargetControlID = "btnAddInsp";
            ModalPopupExtender1.Show();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        objInsp = new clsMasterInspectionAgency();
        try
        {

            gvInspDetails.DataSource = objInsp.MasterInspectionAgency_Fetch_DataSet();
            gvInspDetails.DataBind();
            if (gvInspDetails.Rows.Count == 0)
            {
                mvInsp.SetActiveView(ViewInspNotFound);
                lblMessage.Text = "No InspectionAgency found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["inspAction"] != null)
            {
                LabelAction.Text = Session["inspAction"].ToString();
                inspAction = 1;
                Session["inspAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                inspAction = 0;
                Session["inspAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["inspAction"] = null;
        }
    }


    protected void gvInspDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                ImageButton imbact = (ImageButton)e.Row.FindControl("imbAct");
                ImageButton imbedit = (ImageButton)e.Row.FindControl("imbEdit");

                // SETTING ACTIVE AND DEACTIVE CAPTION BY CHECKING VALUE
                if (e.Row.Cells[3].Text == "0")
                {
                    e.Row.Cells[3].Text = "Active";
                    imbact.ImageUrl = "../Images/Admin/Deactive.png";
                    imbact.AlternateText = "Deactivate";
                    imbact.ToolTip = "Deactivate This";
                    imbedit.Visible = true;

                }
                else if (e.Row.Cells[3].Text == "1")
                {
                    e.Row.Cells[3].Text = "Deactive";
                    //btnAct.Text = "Activate";
                    imbact.ImageUrl = "../Images/Admin/Active.png";
                    imbact.AlternateText = "Activate";
                    imbact.ToolTip = "Activate This";
                    e.Row.CssClass = "dynRowDeactivate";
                    //imbEdit.ToolTip = " IS DEACTIVE. IF YOU WISH TO EDIT, YOU HAVE TO ACTIVATE THE ZONE FIRST.";
                    imbedit.Visible = false;


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
            mvInsp.SetActiveView(ViewInspNotFound);
            lblMessage.Text = "No InspectionAgency found!";
        }
    }


    protected void gvInspDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;

        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit";
                objInsp = new clsMasterInspectionAgency();
                DataSet Myds = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objInsp.MasterInspectionAgency_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));

                lblInspname.Text = ": " + Myds.Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString();
                txtInspName.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIA_Name"].ToString();
                txtDescription.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIA_Desc"].ToString();
                

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();

            }
            //THIS IS FOR ACTIVATE AND DEACTIVATE
            else if (e.CommandName == "Act")
            {
                objInsp = new clsMasterInspectionAgency();

                DataSet Myds = new DataSet();
                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();
                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = objInsp.MasterInspectionAgency_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                accessId = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MIA_AccessId"]);
                
                if (accessId == 0)//TO DEACTIVATE
                {
                   
                    act = objInsp.MasterInspectionAgency_ActivateById(Convert.ToInt32(HiddenField1.Value), UserId, "active");
                    //Deactivated successfully
                    if (act == 4)
                    {
                        Session["inspAction"] = "Deactivated successfully";
                    }
                    else  //Error message
                    {
                        Session["inspAction"] = "Server is busy, please try later";
                    }

                }
                else //TO ACTIVATE
                {

                    act = objInsp.MasterInspectionAgency_ActivateById(Convert.ToInt32(HiddenField1.Value), UserId, "deactive");
                    //Activated successfully
                    if (act == 4)
                    {
                        Session["inspAction"] = "Activated successfully";
                    }
                    else //Error message
                    {
                        Session["inspAction"] = "Server is busy, please try later";
                    }
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
        objInsp = new clsMasterInspectionAgency();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = new int();
        string description;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                //id = objInsp.MasterInspectionAgency_InsertUpdate(Convert.ToInt32(HiddenField1.Value), txtInspName.Text, txtDescription.Text, UserId);
                id = objInsp.MasterInspectionAgency_InsertUpdate(Convert.ToInt32(HiddenField1.Value), txtInspName.Text, txtDescription.Text, UserId);
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                description = txtDescription.Text;
                //description = description.Replace(Char(13), "<br />");
                description = description.Replace("\r\n", "<br />");
                //id = objInsp.MasterInspectionAgency_InsertUpdate(0, txtInspName.Text.Trim(), txtDescription.Text, UserId);
                id = objInsp.MasterInspectionAgency_InsertUpdate(0, txtInspName.Text.Trim(), txtDescription.Text, UserId);
            }
            switch (id)
            {
                case 1://data already exist
                    Session["inspAction"] = "Name already exist";
                    break;
                case 2://data inserted successfully
                    Session["inspAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["inspAction"] = "Server is busy";
                    break;
                case 4://data updated successfully
                    Session["inspAction"] = "Updated successfully";
                    break;
            }

            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        
    }
}
