using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_MasterInspectionPaymentSlab : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int iafeeAction = 0;
    clsMasterInspectionAgency objInspfee;
    DataSet Myds;
    DataTable dt;
    int count=1;
    int successId;


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

            txtAmount.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtSlab.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 1 || RoleId==6)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvIAFee.SetActiveView(ViewIAFeeDetail);
                    GridViewDataBind(0,0);
                    DropDownInspBind(DropDownListIAgency);
                    DropDownFormBind(DropDownListFormName);
                }
            }
            else
            {
                btnAddIAFee.Visible = false;
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

    public void GridViewDataBind(int agencyid, int formid)
    {
        objInspfee = new clsMasterInspectionAgency();
        Myds = new DataSet();
        try
        {
            if (agencyid == 0 && formid == 0)
            {
                Myds = objInspfee.InspectionPaymentSlab_Fetch_DataSet();

                gvIAFeeDetails.DataSource = Myds.Tables[0];
                gvIAFeeDetails.DataBind();
            }
            else
            {
                Myds = objInspfee.InspectionPaymentSlab_FetchByIARoleName_DataSet(agencyid, formid);

                gvIAFeeDetails.DataSource = Myds.Tables[0];
                gvIAFeeDetails.DataBind();
            }
            mvIAFee.SetActiveView(ViewIAFeeDetail);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        if (gvIAFeeDetails.Rows.Count == 0)
        {
            mvIAFee.SetActiveView(ViewIAFeeNotFound);
            lblMessage.Text = "No InspectionAgency Fee found!";
        }

        try
        {
            if (Session["iafeeAction"] != null)
            {
                LabelAction.Text = Session["iafeeAction"].ToString();
                iafeeAction = 1;
                Session["iafeeAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                iafeeAction = 0;
                Session["iafeeAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["iafeeAction"] = null;
        }
    }


    protected void gvIAFeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label ltlSno = (Label)e.Row.FindControl("ltlSno");
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
            mvIAFee.SetActiveView(ViewIAFeeNotFound);
            lblMessage.Text = "No InspectionAgency Fee found!";
        }
    }


    protected void gvIAFeeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int act;

        try
        {
            objInspfee = new clsMasterInspectionAgency();
            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit InspectionAgency Fee";
                
                Myds = new DataSet();
                //ImageButton imgedit = (ImageButton)e.CommandSource;

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objInspfee.InspectionPaymentSlab_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));

                lblAgency.Text = Myds.Tables[0].Rows[0]["AgencyName"].ToString();
                lblForm.Text = Myds.Tables[0].Rows[0]["FormTypeName"].ToString();
                txtSlab.Text = Myds.Tables[0].Rows[0]["fGPtbl_IPS_SlabName"].ToString();
                txtAmount.Text = Myds.Tables[0].Rows[0]["fGPtbl_IPS_Amount"].ToString();
                txtInstruction.Text = Myds.Tables[0].Rows[0]["fGPtbl_IPS_Instructions"].ToString();

                DropDownListForm.Visible = false;
                DropDownListAgency.Visible = false;
                lblAgency.Visible = true;
                lblForm.Visible = true;

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "dte")
            {
                successId = objInspfee.InspectionPaymentSlab_DeleteById(Convert.ToInt32(e.CommandArgument), UserId);

                if (successId == 1)
                {
                    Session["iafeeAction"] = "Deleted successfully";
                }
                else
                {
                    Session["iafeeAction"] = "Server is busy, please try later";
                }

                GridViewDataBind(0,0);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objInspfee = new clsMasterInspectionAgency();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id=0;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
                id = objInspfee.InspectionPaymentSlab_InsertUpdate(Convert.ToInt32(HiddenField1.Value.ToString()), 0, 0, txtSlab.Text, Convert.ToDouble(txtAmount.Text.ToString()), txtInstruction.Text, UserId);
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                //id = objDept.MasterDepartment_InsertUpdate(0, txtDeptName.Text, txtDescription.Text, UserId);
                id = objInspfee.InspectionPaymentSlab_InsertUpdate(0, Convert.ToInt32(DropDownListAgency.SelectedValue.ToString()), Convert.ToInt32(DropDownListForm.SelectedValue.ToString()), txtSlab.Text, Convert.ToDouble(txtAmount.Text.ToString()), txtInstruction.Text, UserId);
            }
            switch (id)
            {
                case 1://data already exist
                    Session["iafeeAction"] = "Name already exist";
                    break;
                case 2://data inserted successfully
                    Session["iafeeAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["iafeeAction"] = "Server is busy, please try later";
                    break;
                case 4://data updated successfully
                    Session["iafeeAction"] = "Updated successfully";
                    break;
                default://for any error
                    Session["iafeeAction"] = "Server is busy, please try later";
                    break;
            }
            GridViewDataBind(0,0);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
          
    }


    protected void btnAddIAFee_Click(object sender, EventArgs e)
    {
        lblAddEdit.Text = "Add New InspectionAgency Fee";
        txtSlab.Text = "";
        txtAmount.Text = "";
        txtInstruction.Text = "";
        hdnfield.Value = "add";
        DropDownInspBind(DropDownListAgency);
        DropDownFormBind(DropDownListForm);
        ModalPopupExtender1.TargetControlID = "btnAddIAFee";
        ModalPopupExtender1.Show();
        DropDownListAgency.Visible = true;
        DropDownListForm.Visible = true;
        lblAgency.Visible = false;
        lblForm.Visible = false;
    }

    public void DropDownInspBind(DropDownList ddlInsp)
    {
        try
        {
            objInspfee = new clsMasterInspectionAgency();
            Myds = new DataSet();

            Myds = objInspfee.MasterInspectionAgency_Fetch_DataSet();
            
            ddlInsp.DataSource = Myds.Tables[0];
            ddlInsp.DataTextField = Myds.Tables[0].Columns[1].Caption;
            ddlInsp.DataValueField = Myds.Tables[0].Columns[0].Caption;
            ddlInsp.DataBind();
            //ddlInsp.Items.Insert(0, new ListItem("--Select agency--", ""));
            ddlInsp.Items.Insert(0, new ListItem("--Select agency--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    
    public void DropDownFormBind(DropDownList ddlForm)
    {
        try
        {
            clsMasterFormType objFormtype = new clsMasterFormType();
            Myds = new DataSet();

            Myds = objFormtype.MasterFormType_Fetch_DataSet();

            ddlForm.DataSource = Myds.Tables[0];
            ddlForm.DataTextField = Myds.Tables[0].Columns[1].Caption;
            ddlForm.DataValueField = Myds.Tables[0].Columns[0].Caption;
            ddlForm.DataBind();
            //ddlForm.Items.Insert(0, new ListItem("--Select form--", "0"));
            ddlForm.Items.Insert(0, new ListItem("--Select form--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int ddlvalueAgency = 0, ddlvalueForm = 0;

            if (DropDownListIAgency.SelectedIndex != 0)
            {
                ddlvalueAgency = Convert.ToInt32(DropDownListIAgency.SelectedValue);
            }
            if (DropDownListFormName.SelectedIndex != 0)
            {
                ddlvalueForm = Convert.ToInt32(DropDownListFormName.SelectedValue);
            }
            GridViewDataBind(ddlvalueAgency, ddlvalueForm);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
