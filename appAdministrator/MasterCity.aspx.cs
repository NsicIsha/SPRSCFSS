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

public partial class appAdministrator_MasterCity : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int mstAction;

    clsMasterCity objCity;

    int StateId;


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
        try
        {
            StateId = Convert.ToInt32(ddlState.SelectedValue);
        }
        catch(Exception ex)
        {
            StateId = 0;
        }
        
        
        if (!Page.IsPostBack)
        {
            FunFillDDL();
            FunFillGridView(StateId);
        }
        
    }

    private void FunFillDDL()
    {
        System.Data.DataTable dt = new DataTable();
        dt = clsDDL.ddlState();
        ddlState.DataSource = dt;
        ddlState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        ddlState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState.DataBind();
        ddlState.Items.Insert(0, new ListItem("--Select State--", ""));

        ddlStateEdit.DataSource = dt;
        ddlStateEdit.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
        ddlStateEdit.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlStateEdit.DataBind();
        ddlStateEdit.Items.Insert(0, new ListItem("--Select State--", ""));

        dt.Clear();
        dt.Dispose();
    }

    private void FunFillGridView(int grdStateID)
    {
            FunActionMessage();

            try
            {
                ddlState.SelectedValue = grdStateID.ToString();
            }
            catch { }

            objCity = new clsMasterCity();
            System.Data.DataSet ds = new DataSet();
            if (grdStateID == 0)
            {
                ds = objCity.MasterCity_Fetch_DataSet();
            }
            else
            {
                ds = objCity.MasterCity_FetchByStateId_DataSet(grdStateID);
            }

            GridView1.DataSource = null;
            GridView1.DataBind();
            if (ds.Tables[0].Rows.Count != 0)
            {
                MultiView1.SetActiveView(viewData);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            else
            {
                //MultiView1.SetActiveView(viewMessage);
                //lblMsg.Text = "No city found !";
            }
    }

    private void FunActionMessage()
    {
        try
        {
            if (Session["CityAction"] != null)
            {
                lblAction.Text = Session["CityAction"].ToString();
                mstAction = 1;
                Session["CityAction"] = null;
            }
            else
            {
                lblAction.Text = "";
                mstAction = 0;

                Session["CityAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
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
                    imgActive.AlternateText = "ACTIVATE THIS CITY";
                    imgActive.ToolTip = "ACTIVATE THIS CITY";
                    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to ACTIVATE this city?');");
                    imgActive.CommandName = "ACTIVATE";
                }
                else
                {
                    ((Label)(e.Row.FindControl("lblActiveDeactive"))).Text = "Active";

                    imgActive.ImageUrl = "../Images/Admin/Deactive.png";
                    imgActive.AlternateText = "DEACTIVATE THIS CITY";
                    imgActive.ToolTip = "DEACTIVATE THIS CITY";
                    imgActive.Attributes.Add("onclick", "return confirm('Are you sure you want to DEACTIVATE this city?');");
                    imgActive.CommandName = "DEACTIVATE";
                }

                int StateAccessId = 9;
                StateAccessId = Convert.ToInt32(((HiddenField)(e.Row.FindControl("hdnStateActiveFlag"))).Value);
                if (StateAccessId == 1) //State Deactivated
                {
                    imgActive.AlternateText = "STATE IS DEACTIVE FOR THIS CITY.";
                    if (AccessId == 1)
                    {
                        imgActive.ToolTip = "STATE IS DEACTIVE FOR THIS CITY. IF YOU WISH TO ACTIVATE THIS CITY, YOU HAVE TO ACTIVATE THE STATE FOR THIS CITY FIRST.";
                    }
                    else
                    {
                        imgActive.ToolTip = "STATE IS DEACTIVE FOR THIS CITY. IF YOU WISH TO DEACTIVE THIS CITY, YOU HAVE TO ACTIVATE THE STATE FOR THIS CITY FIRST.";
                    }
                    
                    imgActive.Enabled = false;
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
            if (e.CommandName == "CityEdit")
            {
                int cityidedit = 0;
                cityidedit = Convert.ToInt32(e.CommandArgument);

                objCity = new clsMasterCity();
                System.Data.DataSet ds = new DataSet();
                ds = objCity.MasterCity_Fetch_DataSet(cityidedit);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    lblStateEdit.Visible = true;
                    ddlStateEdit.Visible = false;

                    //hdnCityId.Value = cityidedit.ToString();
                    btnSubmitEdit.CommandArgument = cityidedit.ToString();
                    lblStateEdit.Text = ds.Tables[0].Rows[0]["SateName"].ToString();
                    txtCityEdit.Text = ds.Tables[0].Rows[0]["fGPtbl_MCty_Name"].ToString();
                    txtDescEdit.Text = ds.Tables[0].Rows[0]["fGPtbl_MCty_Desc"].ToString();
                    lblMesgPnl.Text = "UPDATE CITY : " + txtCityEdit.Text.ToString();
                    hdnStateId.Value = ds.Tables[0].Rows[0]["fGPtbl_MCty_StateId"].ToString();
                    ModalPopupExtender2.Show();
                }
            }
            else if (e.CommandName == "ACTIVATE")
            {
                int AccessId = 0;
                int cityidact = 0;
                cityidact = Convert.ToInt32(e.CommandArgument);

                objCity = new clsMasterCity();
                int ErrorId = 0;
                ErrorId = objCity.MasterCity_Update_ActiveById(cityidact, UserId, AccessId);
                switch (ErrorId)
                {
                    case 1:     //Successful
                        Session["CityAction"] = "City activated successfully.";
                        break;
                    case 2:     //Unsuccessful
                        Session["CityAction"] = "Server error.";
                        break;
                }
                FunFillGridView(StateId);
            }
            else if (e.CommandName == "DEACTIVATE")
            {
                //System.Threading.Thread.Sleep(5000);
                int AccessId = 1;
                int cityidact = 0;
                cityidact = Convert.ToInt32(e.CommandArgument);

                objCity = new clsMasterCity();
                int ErrorId = 0;
                ErrorId = objCity.MasterCity_Update_ActiveById(cityidact, UserId, AccessId);
                switch (ErrorId)
                {
                    case 1:     //Successful
                        Session["CityAction"] = "City deactivated successfully.";
                        break;
                    case 2:     //Unsuccessful
                        Session["CityAction"] = "Server error.";
                        break;
                }
                FunFillGridView(StateId);
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
            lblStateEdit.Visible = false;
            ddlStateEdit.Visible = true;
            ddlStateEdit.SelectedValue = "";
            txtCityEdit.Text = "";
            txtDescEdit.Text = "";
            lblMesgPnl.Text = "ADD NEW CITY";
            ModalPopupExtender2.Show();
            /*string s1 = "0";
            int val = 1 / Convert.ToInt16(s1);*/
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmitEdit_Click(object sender, EventArgs e)
    {
        int CityIdSubmit = 0;
        int StateIdEdit = 0;
        try
        {
            Button btnSubmitfun;
            btnSubmitfun = ((Button)(sender));
            CityIdSubmit = Convert.ToInt32(btnSubmitfun.CommandArgument.ToString());
        }
        catch (Exception ex)
        {
            CityIdSubmit = 0;
        }

        try
        {
            string Cityname = "";
            string CityDesc = "";
            int ErrorId = 0;
            if (CityIdSubmit == 0)
            {
                try
                {
                    StateIdEdit = Convert.ToInt32(ddlStateEdit.SelectedValue.ToString());
                }
                catch (Exception ex)
                {
                    StateIdEdit = 0;
                }
            }
            else
            {
                try
                {
                    StateIdEdit = Convert.ToInt32(hdnStateId.Value.ToString());
                }
                catch (Exception ex)
                {
                    StateIdEdit = 0;
                }
            }
            if (StateIdEdit != 0)
            {
                Cityname = txtCityEdit.Text.ToString().Trim();
                CityDesc = txtDescEdit.Text.ToString().Trim();
                objCity = new clsMasterCity();
                ErrorId = objCity.MasterCity_InsertUpdate(CityIdSubmit, StateIdEdit, Cityname, CityDesc, UserId);
                switch (ErrorId)
                {
                    case 1:     //Data already exists
                        //if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                        //{
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertFalse('City already exists !');", true);
                        //}
                        //ModalPopupExtender2.Show();
                        Session["CityAction"] = "City already exists !";
                        break;
                    case 2:     //Successfully Inserted
                        Session["CityAction"] = "City inserted successfully.";
                        break;
                    case 3:     //Error in operation
                        if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertFalse('System is unable to process your request. Sorry for inconvenience. Please try after some time !');", true);
                        }
                        ModalPopupExtender2.Show();
                        break;
                    case 4:     //Successfully Updated
                        Session["CityAction"] = "City updated successfully.";
                        break;
                }
            }
            else
            {
                if (Page.ClientScript.IsStartupScriptRegistered("alert") != true)
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "FunAlertFalse('System is unable to process your request. Sorry for inconvenience. Please try after some time !');", true);
                }
                ModalPopupExtender2.Show();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        FunFillGridView(StateIdEdit);
    }
   
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);
        int ddlStateId = 0;
        string str = "";
        try
        {
            str = ddlState.SelectedValue.ToString();
            ddlStateId = Convert.ToInt32(ddlState.SelectedValue);
        }
        catch (Exception ex)
        {
            ddlStateId = 0;
        }
        FunFillGridView(ddlStateId);
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FunFillGridView(StateId);
    }
}
