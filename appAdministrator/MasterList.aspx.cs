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


public partial class appAdministrator_MasterList : System.Web.UI.Page
{
    clsMasterList objList;
    int accessId;
    string imageid;
    int count = 1;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int listAction = 0;


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

            if (UserId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvList.SetActiveView(ViewListDetail);
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
        objList = new clsMasterList();
        try
        {

            gvListDetails.DataSource = objList.MasterList_Fetch_DataSet();
            gvListDetails.DataBind();
            if (gvListDetails.Rows.Count == 0)
            {
                mvList.SetActiveView(ViewListNotFound);
                lblMessage.Text = "No List found!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["listAction"] != null)
            {
                LabelAction.Text = Session["listAction"].ToString();
                listAction = 1;
                Session["listAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                listAction = 0;
                Session["listAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["listAction"] = null;
        }
    }


    protected void gvListDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;
                
                

                //FINDING EDIT BUTTON TO MANAGE EDITING WHEN CONTENT IS PRESENT
                ImageButton imgedit = (ImageButton)e.Row.FindControl("imgButton");
                ImageButton imbview = (ImageButton)e.Row.FindControl("imbView");
                HiddenField hdnid = (HiddenField)e.Row.FindControl("HiddenFieldId");

                //id being passed
                imageid = "CommonPopup.aspx?id=" + hdnid.Value + "&name=list";

                imbview.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
                
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
        if (count == 0)
        {
            mvList.SetActiveView(ViewListNotFound);
            lblMessage.Text = "No List found!";
        }
    }
  
    protected void gvListDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objList = new clsMasterList();
        DataSet Myds = new DataSet();
        try
        {

            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                Response.Redirect("MasterListView.aspx?id=" + e.CommandArgument,false);
            }
        }
        catch (System.Threading.ThreadAbortException IException)
        { }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
