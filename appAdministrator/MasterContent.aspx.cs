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


public partial class appAdministrator_MasterContent : System.Web.UI.Page
{
    clsMasterContent objContent;
    int accessId;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    string popupurl;
    public int contentAction = 0;


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
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();


            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvContent.SetActiveView(ViewContentDetail);
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
        objContent = new clsMasterContent();
        try
        {
            gvContentDetails.DataSource = objContent.MasterContent_Fetch_DataSet();
            gvContentDetails.DataBind();
            if (gvContentDetails.Rows.Count == 0)
            {
                mvContent.SetActiveView(ViewContentNotFound);
                lblMessage.Text = "No Content found";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["contentAction"] != null)
            {
                LabelAction.Text = Session["contentAction"].ToString();
                contentAction = 1;
                Session["contentAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                contentAction = 0;
                Session["contentAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["contentAction"] = null;
        }
    }

    protected void gvContentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
                //HiddenField hdnid = (HiddenField)e.Row.FindControl("HiddenFieldId");

                //FINDING EDIT BUTTON AND VIEW BUTTON
                ImageButton imbedit = (ImageButton)e.Row.FindControl("imgButton");
                ImageButton imbview = (ImageButton)e.Row.FindControl("imbView");
                //TO STORE URL FOR POPUP
                popupurl = "CommonPopup.aspx?id=" + imbview.CommandArgument.ToString() + "&name=cont";

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
            mvContent.SetActiveView(ViewContentNotFound);
            lblMessage.Text = "No Content found!";
        }
    }





    
    protected void  gvContentDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                Response.Redirect("MasterContentView.aspx?id=" + e.CommandArgument, false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

        






}
