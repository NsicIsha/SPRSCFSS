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


public partial class appAdministrator_MasterRegDayLimit : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int regdayAction = 0;
    int count = 1;
    clsMasterRegDayLimit objRegday;
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

            txtReg.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvRegday.SetActiveView(ViewRegdayDetail);
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
            objRegday = new clsMasterRegDayLimit();

            gvRegdayDetails.DataSource = objRegday.MasterRegDayLimit_Fetch_DataSet();
            gvRegdayDetails.DataBind();

            if(gvRegdayDetails.Rows.Count==0)
            {
                mvRegday.SetActiveView(ViewRegdayNotFound);
                lblMessage.Text = "No RegistrationDays found";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["regdayAction"] != null)
            {
                LabelAction.Text = Session["regdayAction"].ToString();
                regdayAction = 1;
                Session["regdayAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["regdayAction"] = null;
        }
    }


    protected void gvRegdayDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            mvRegday.SetActiveView(ViewRegdayNotFound);
            lblMessage.Text = "No RegistrationDays found!";
        }
    }
    protected void gvRegdayDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objRegday = new clsMasterRegDayLimit();
            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit RegistrationDays";

                Myds = new DataSet();

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objRegday.MasterRegDayLimit_Fetch(Convert.ToInt32(e.CommandArgument));

                

                txtReg.Text = Myds.Tables[0].Rows[0]["fGPtbl_MRDL_Days"].ToString();
                txtReg.Visible = true;

                lblReg.Visible = false;

                btnSubmit.Visible = true;

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();
            }
           /* else if (e.CommandName == "view")
            {
                lblAddEdit.Text = "View RegistrationDays";

                Myds = new DataSet();

                Myds = objRegday.MasterRegDayLimit_Fetch(Convert.ToInt32(e.CommandArgument));

                lblReg.Text = Myds.Tables[0].Rows[0]["fGPtbl_MRDL_Days"].ToString();

                txtReg.Visible = false;
                lblReg.Visible = true;

                btnSubmit.Visible = false;

                ModalPopupExtender1.Show();
            }*/
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objRegday = new clsMasterRegDayLimit();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = 0;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                id = objRegday.MasterRegDayLimit_InsertUpdate(Convert.ToInt32(HiddenField1.Value.ToString()), Convert.ToInt32(txtReg.Text), UserId);
            }

            switch (id)
            {
                case 1://data already exist
                    Session["regdayAction"] = "Name already exist";
                    break;
                case 2://data inserted successfully
                    Session["regdayAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["regdayAction"] = "Server is busy, please try later";
                    break;
                case 4://data updated successfully
                    Session["regdayAction"] = "Updated successfully";
                    break;
                default://for any error
                    Session["regdayAction"] = "Server is busy, please try later";
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
