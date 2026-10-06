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

public partial class appAdministrator_MasterState : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int mstAction;

    clsMasterState objState;


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
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        if (!Page.IsPostBack) {
            try
            {
                FunfillGridView();
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
        mstAction = 0;
        lblAction.Text = "";
    }

    void FunActionMessage()
    {
        try
        {
            if (Session["StateAction"] != null)
            {
                lblAction.Text = Session["StateAction"].ToString();
                mstAction = 1;
                Session["StateAction"] = null;
            }
            else
            {
                lblAction.Text = "";
                mstAction = 0;

                Session["StateAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    void FunfillGridView()
    {
        FunActionMessage();

        objState = new clsMasterState();
        System.Data.DataSet ds = new DataSet();
        ds = objState.MasterState_Fetch_DataSet();
        if (ds.Tables[0].Rows.Count != 0)
        {
            MultiView1.SetActiveView(viewData);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        else
        {
            MultiView1.SetActiveView(viewMessage);
            lblMsg.Text = "No state found !";
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                int AccessId = 9;
                AccessId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnActiveFlag"))).Value);
                ImageButton imgActive = (ImageButton)(e.Row.FindControl("btnActivateDe"));
                if (AccessId == 1)  //Deactivated
                {
                    e.Row.CssClass = "GrdviewRowDeactive";
                    ((Label)(e.Row.FindControl("lblActiveDeactive"))).Text = "Deactive";
                    ((ImageButton)(e.Row.FindControl("btnEdit"))).Enabled = false;

                    imgActive.ImageUrl = "../Images/Admin/Active.png";
                    imgActive.AlternateText = "ACTIVATE THIS STATE";
                    imgActive.ToolTip = "ACTIVATE THIS STATE";
                    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to ACTIVATE this state? This will also ACTIVATE ALL CITIES associated with this state !');");
                    imgActive.CommandName = "ACTIVATE";
                }
                else
                {
                    ((Label)(e.Row.FindControl("lblActiveDeactive"))).Text = "Active";

                    imgActive.ImageUrl = "../Images/Admin/Deactive.png";
                    imgActive.AlternateText = "DEACTIVATE THIS STATE";
                    imgActive.ToolTip = "DEACTIVATE THIS STATE";
                    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to DEACTIVATE this state? This will also DEACTIVATE ALL CITIES associated with this state !');");
                    imgActive.CommandName = "DEACTIVATE";
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "StateEdit")
            {
                int stateidedit = 0;
                stateidedit = Convert.ToInt32(e.CommandArgument);

                objState = new clsMasterState();
                System.Data.DataSet ds = new DataSet();
                ds = objState.MasterState_Fetch_DataSet(stateidedit);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    hdnStateId.Value = stateidedit.ToString();
                    btnSubmitEdit.CommandArgument = stateidedit.ToString();
                    txtStateEdit.Text = ds.Tables[0].Rows[0]["fGPtbl_MS_Name"].ToString();
                    txtDescEdit.Text = ds.Tables[0].Rows[0]["fGPtbl_MS_Desc"].ToString();
                    lblMesgPnl.Text = "UPDATE STATE : " + txtStateEdit.Text.ToString();
                    ModalPopupExtender2.Show();
                }
            }
            else if (e.CommandName == "ACTIVATE")
            {
                int AccessId = 0;
                int stateidact = 0;
                stateidact = Convert.ToInt32(e.CommandArgument);

                objState = new clsMasterState();
                int ErrorId = 0;
                ErrorId = objState.MasterState_Update_ActiveById(stateidact, UserId, AccessId);
                switch (ErrorId)
                {
                    case 1:     //Successful
                        Session["StateAction"] = "State activated successfully.";
                        break;
                    case 2:     //Unsuccessful
                        Session["StateAction"] = "Server error.";
                        break;
                }
                FunfillGridView();
            }
            else if (e.CommandName == "DEACTIVATE")
            {
                int AccessId = 1;
                int stateidact = 0;
                stateidact = Convert.ToInt32(e.CommandArgument);

                objState = new clsMasterState();
                int ErrorId = 0;
                ErrorId = objState.MasterState_Update_ActiveById(stateidact, UserId, AccessId);
                switch (ErrorId)
                {
                    case 1:     //Successful
                        Session["StateAction"] = "State deactivated successfully.";
                        break;
                    case 2:     //Unsuccessful
                        Session["StateAction"] = "Server error.";
                        break;
                }
                FunfillGridView();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            btnSubmitEdit.CommandArgument = "0";
            txtStateEdit.Text = "";
            txtDescEdit.Text = "";
            lblMesgPnl.Text = "ADD NEW STATE";
            ModalPopupExtender2.Show();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        int StateIdSubmit = 0;
        try
        {
            Button btnSubmitfun;
            btnSubmitfun = ((Button)(sender));
            StateIdSubmit = Convert.ToInt32(btnSubmitfun.CommandArgument.ToString());
        }
        catch (Exception ex)
        {
            StateIdSubmit = 0;
        }

        try
        {
            string Statename = "";
            string StateDesc = "";
            int ErrorId = 0;
            Statename = txtStateEdit.Text.ToString().Trim();
            StateDesc = txtDescEdit.Text.ToString().Trim();
            objState = new clsMasterState();
            ErrorId = objState.MasterState_InsertUpdate(StateIdSubmit, Statename, StateDesc, UserId);
            switch (ErrorId)
            {
                case 1:     //Data already exists
                    //if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                    //{
                    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertFalse('State already exists !');", true);
                    //}
                    //ModalPopupExtender2.Show();
                    Session["StateAction"] = "State already exists !";
                    break;
                case 2:     //Successfully Inserted
                    Session["StateAction"] = "State inserted successfully.";
                    break;
                case 3:     //Error in operation
                    if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertFalse('System is unable to process your request. Sorry for inconvenience. Please try after some time !');", true);
                    }
                    ModalPopupExtender2.Show();
                    break;
                case 4:     //Successfully Updated
                    Session["StateAction"] = "State updated successfully.";
                    break;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        FunfillGridView();
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FunfillGridView();
    }
}
