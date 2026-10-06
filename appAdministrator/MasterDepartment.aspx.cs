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


public partial class appAdministrator_MasterDepartment : System.Web.UI.Page
{

    clsMasterDepartment objDept;
    int accessId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int deptAction = 0;

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
                    mvDept.SetActiveView(ViewDeptDetail);
                    GridViewDataBind();
                }
            }
            else
            {
                btnAddDept.Visible = false;
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

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        objDept = new clsMasterDepartment();
        try
        {

            gvDeptDetails.DataSource = objDept.MasterDepartment_Fetch_DataSet();
            gvDeptDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        if (gvDeptDetails.Rows.Count == 0)
        {
            mvDept.SetActiveView(ViewDeptNotFound);
            lblMessage.Text = "No Department found!";
        }
        try
        {
            if (Session["deptAction"] != null)
            {
                LabelAction.Text = Session["deptAction"].ToString();
                deptAction = 1;
                Session["deptAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                deptAction = 0;
                Session["deptAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["deptAction"] = null;
        }
    }

    protected void gvDeptDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;

                ImageButton imgAct = (ImageButton)e.Row.FindControl("imgAct");
                ImageButton imgedit = (ImageButton)e.Row.FindControl("imgButton");

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

                    imgedit.ToolTip = "DEPARTMENT IS DEACTIVE FOR THIS. IF YOU WISH TO EDIT THIS, YOU HAVE TO ACTIVATE THE DEPARTMENT FOR THIS FIRST.";
                    imgedit.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
        if ( count == 0)
        {
            mvDept.SetActiveView(ViewDeptNotFound);
            lblMessage.Text = "No Department found!";
        }
    }

    protected void gvDeptDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;

        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit Department";
                objDept = new clsMasterDepartment();
                DataSet Myds = new DataSet();
                ImageButton imgedit = (ImageButton)e.CommandSource;

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objDept.MasterDepartment_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             DeptName = n.Field<string>("fGPtbl_MD_Name"),
                             DeptDescription = n.Field<string>("fGPtbl_MD_Desc"),
                             StatusId = n.Field<int>("fGPtbl_MD_AccessId"),
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    txtDeptName.Text = onl.DeptName;
                    txtDescription.Text = onl.DeptDescription;
                    accessId = onl.StatusId;
                    HiddenField2.Value = onl.Userid.ToString();
                }

                //SHOWING MODALPOPUPEXTENDER
                //ModalPopupExtender1.Show();


                if (accessId == 0)
                {
                    ModalPopupExtender1.Show();
                }
                else
                {
                    imgedit.Attributes.Add("onclick", "alert('you cannot edit');");
                }


            }
            //THIS IS FOR ACTIVATE AND DEACTIVATE
            else if (e.CommandName == "Act")
            {
                objDept = new clsMasterDepartment();

                DataSet Myds = new DataSet();
                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();
                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = objDept.MasterDepartment_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                DataTable dt = Myds.Tables[0];
                var qu = from n in dt.AsEnumerable()
                         select new
                         {
                             StatusId = n.Field<int>("fGPtbl_MD_AccessId"),
                             Userid = n.Field<int>("UserId")
                         };
                foreach (var onl in qu)
                {
                    accessId = onl.StatusId;
                    HiddenField2.Value = onl.Userid.ToString();
                }
                if (accessId == 0)//TO DEACTIVATE
                {
                    act = objDept.MasterDepartment_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "active");
                    if (act == 4)
                        Session["deptAction"] = "Deactivated Successfully";
                    else
                        Session["deptAction"] = "Server is Busy";
                }
                else //TO ACTIVATE
                {
                    act = objDept.MasterDepartment_ActivateById(Convert.ToInt32(HiddenField1.Value), Convert.ToInt32(HiddenField2.Value), "deactive");
                    //TO SHOW THE MESSAGE
                    if (act == 4)
                        Session["deptAction"] = "Activated Successfully";
                    else
                        Session["deptAction"] = "Server is Busy";
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
    protected void btnAddDept_Click(object sender, EventArgs e)
    {
        lblAddEdit.Text = "Add New Department";
        txtDeptName.Text = "";
        txtDescription.Text = "";
        hdnfield.Value = "add";
        ModalPopupExtender1.TargetControlID = "btnAddDept";
        ModalPopupExtender1.Show();
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objDept = new clsMasterDepartment();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = new int();
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                id = objDept.MasterDepartment_InsertUpdate(Convert.ToInt32(HiddenField1.Value), txtDeptName.Text, txtDescription.Text, Convert.ToInt32(HiddenField2.Value));
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                id = objDept.MasterDepartment_InsertUpdate(0, txtDeptName.Text, txtDescription.Text, UserId);
            }
            switch (id)
            {
                case 1://data already exist
                    Session["deptAction"] = "NAME Already Exist";
                    break;
                case 2://data inserted successfully
                    Session["deptAction"] = "ADDED Successfully";
                    break;
                case 3:// error in operation
                    Session["deptAction"] = "SERVER is busy";
                    break;
                case 4://data updated successfully
                    Session["deptAction"] = "UPDATED successfully";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        GridViewDataBind();
    }
}
