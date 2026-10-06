using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterDaysLimit : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int dayinsAction = 0;
    int count = 1;
    clsMasterInspDayLimit objDayins;
    DataSet Myds;

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

            txtIssue.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtStart.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtSubmit.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvDayins.SetActiveView(ViewDayinsDetail);
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
        try
        {
            objDayins = new clsMasterInspDayLimit();
           

            gvDayinsDetails.DataSource = objDayins.MasterInspDayLimit_Fetch_DataSet();
            gvDayinsDetails.DataBind();

            if(gvDayinsDetails.Rows.Count==0)
            {
                mvDayins.SetActiveView(ViewDayinsNotFound);
                lblMessage.Text = "No InspectionDays found";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["dayinsAction"] != null)
            {
                LabelAction.Text = Session["dayinsAction"].ToString();
                dayinsAction = 1;
                Session["dayinsAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["dayinsAction"] = null;
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objDayins = new clsMasterInspDayLimit();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = 0;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                id = objDayins.MasterInspDayLimit_InsertUpdate(Convert.ToInt32(HiddenField1.Value.ToString()), Convert.ToInt32(txtIssue.Text), Convert.ToInt32(txtStart.Text), Convert.ToInt32(txtSubmit.Text), UserId);
            }
            
            switch (id)
            {
                case 1://data already exist
                    Session["dayinsAction"] = "Name already exist";
                    break;
                case 2://data inserted successfully
                    Session["dayinsAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["dayinsAction"] = "Server is busy, please try later";
                    break;
                case 4://data updated successfully
                    Session["dayinsAction"] = "Updated successfully";
                    break;
                default://for any error
                    Session["dayinsAction"] = "Server is busy, please try later";
                    break;
            }
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void gvDayinsDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Literal ltlSno = (Literal)e.Row.FindControl("ltlSno");
                ltlSno.Text = count.ToString();
                count++;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
        if (count == 0)
        {
            mvDayins.SetActiveView(ViewDayinsNotFound);
            lblMessage.Text = "No InspectionDays found!";
        }
    }

    protected void gvDayinsDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objDayins = new clsMasterInspDayLimit();
            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit InspectionDays";

                Myds = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES
                Myds = objDayins.MasterInspDayLimit_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));

                txtIssue.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIDL_DateIssue"].ToString();
                txtStart.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIDL_ReportStart"].ToString();
                txtSubmit.Text = Myds.Tables[0].Rows[0]["fGPtbl_MIDL_ReportSubmit"].ToString();

                txtIssue.Visible = true;
                txtStart.Visible = true;
                txtSubmit.Visible = true;

                lblIssue.Visible = false;
                lblStart.Visible = false;
                lblSubmit.Visible = false;

                btnSubmit.Visible = true;

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

}
