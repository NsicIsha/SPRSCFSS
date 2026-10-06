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

public partial class appAdministrator_MasterInstruction : System.Web.UI.Page
{

    clsMasterInstruction objInst;
    int accessId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string popupurl;
    public int InstAction = 0;

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
                    mvInst.SetActiveView(ViewInstDetail);
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
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        
    }

    public void GridViewDataBind()
    {
        objInst = new clsMasterInstruction();
        try
        {

            gvInstDetails.DataSource = objInst.MasterInstruction_Fetch_DataSet();
            gvInstDetails.DataBind();
            if (gvInstDetails.Rows.Count == 0)
            {
                mvInst.SetActiveView(ViewInstNotFound);
                lblMessage.Text = "No Instruction found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["InstAction"] != null)
            {
                LabelAction.Text = Session["InstAction"].ToString();
                InstAction = 1;
                Session["InstAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                InstAction = 0;
                Session["InstAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["InstAction"] = null;
        }
    }






    protected void gvInstDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                Response.Redirect("MasterInstructionView.aspx?id=" + e.CommandArgument, false);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void gvInstDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;

                //FINDING LABEL AND ASSIGNING VIEW MORE
               
                HiddenField hdnid = (HiddenField)e.Row.FindControl("HiddenFieldId");

                //FINDING EDIT BUTTON TO MANAGE EDITING WHEN CONTENT IS PRESENT
                ImageButton imbedit = (ImageButton)e.Row.FindControl("imgButton");
                ImageButton imbview = (ImageButton)e.Row.FindControl("imbView");
                //TO STORE URL FOR POPUP
                popupurl = "CommonPopup.aspx?id=" + hdnid.Value + "&name=inst";

                //TO OPEN POPUP
                imbview.Attributes.Add("onclick", "return FunOpenPopup950x600('" + popupurl + "');");
              

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
        if (count == 0)
        {
            mvInst.SetActiveView(ViewInstNotFound);
            lblMessage.Text = "No Instruction found!";
        }
    }
}
