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

public partial class appAdministrator_MasterCoverLetter : System.Web.UI.Page
{
    clsMasterCoverLetter objCover;
    int accessId;
    ////string imageid;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int coverAction = 0;


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
                    mvCover.SetActiveView(ViewCoverDetail);
                    GridViewDataBind();
                }
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

    //BINDING GRIDVIEW
    public void GridViewDataBind()
    {
        objCover = new clsMasterCoverLetter();
        try
        {

            gvCoverDetails.DataSource = objCover.MasterCoverLetter_Fetch_DataSet();
            gvCoverDetails.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        if (gvCoverDetails.Rows.Count == 0)
        {
            mvCover.SetActiveView(ViewCoverNotFound);
            lblMessage.Text = "No Cover letter found!";
        }
        try
        {
            if (Session["coverAction"] != null)
            {
                LabelAction.Text = Session["coverAction"].ToString();
                coverAction = 1;
                Session["coverAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                coverAction = 0;
                Session["coverAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["coverAction"] = null;
        }
    }

    protected void gvCoverDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;



                //FINDING IMAGE BUTTONS TO MANAGE CORRESPONDING ACTION WHEN CONTENT IS PRESENT
                ImageButton imbedit = (ImageButton)e.Row.FindControl("imbEdit");
                ImageButton imbact = (ImageButton)e.Row.FindControl("imbAct");

                // SETTING ACTIVE AND DEACTIVE CAPTION BY CHECKING VALUE
                if (e.Row.Cells[2].Text == "1")
                {
                    e.Row.Cells[2].Text = "Active";
                    imbact.ImageUrl = "../Images/Admin/Deactive.png";
                    imbact.AlternateText = "Deactivate";
                    imbact.ToolTip = "Deactivate This";
                    imbedit.Visible = true;

                }
                else if (e.Row.Cells[2].Text == "0")
                {
                    e.Row.Cells[2].Text = "Deactive";
                    imbact.ImageUrl = "../Images/Admin/Active.png";
                    imbact.AlternateText = "Activate";
                    imbact.ToolTip = "Activate This";
                    e.Row.CssClass = "dynRowDeactivate";
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
            mvCover.SetActiveView(ViewCoverNotFound);
            lblMessage.Text = "No Cover letter found!";
        }
    }

    protected void gvCoverDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int act;
        objCover = new clsMasterCoverLetter();
        DataSet Myds = new DataSet();
        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                Response.Redirect("MasterCoverLetterView.aspx?id=" + e.CommandArgument, false);
            }


            //THIS IS FOR ACTIVATE AND DEACTIVATE
            else if (e.CommandName == "Act")
            {

                //FILLING DATASET AND CAPTURING STATUS ID USING LINQ
                Myds = objCover.MasterCoverLetter_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));
                accessId = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_MCL_Active"]);

                if (accessId == 1)//TO DEACTIVATE
                {
                    act = objCover.MasterCoverLetter_ActivateById(Convert.ToInt32(e.CommandArgument), UserId, "active");
                    if (act == 4)
                        Session["coverAction"] = "Deactivated Successfully";
                    else
                        Session["coverAction"] = "Server is busy";
                }
                else //TO ACTIVATE
                {
                    act = objCover.MasterCoverLetter_ActivateById(Convert.ToInt32(e.CommandArgument), UserId, "deactive");
                    //TO SHOW THE MESSAGE
                    if (act == 4)
                        Session["coverAction"] = "Activated Successfully";
                    else
                        Session["coverAction"] = "Server is busy";
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


}
