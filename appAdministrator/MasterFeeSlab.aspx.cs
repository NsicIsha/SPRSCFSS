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
using System.Collections.Generic;

public partial class appAdministrator_MasterFeeSlab : System.Web.UI.Page
{
    clsMasterFees objFees;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    public int feesAction = 0;
    int count = 1;
    int accessId;
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
        try
        {

            FunRequest();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            txtSlab.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtAmount.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtDescription.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txt_slab_increment.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txt_slab_increment_fee.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvFees.SetActiveView(ViewFeesDetail);
                    GridViewDataBind();
                    DropDownFormBind();
                }
            }
            else
            {
                btnAddFees.Visible = false;
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

    public void GridViewDataBind()
    {
        try
        {
            objFees = new clsMasterFees();

            gvFeesDetails.DataSource = objFees.MasterFees_Slab_Fetch_DataSet();
            gvFeesDetails.DataBind();

            if (gvFeesDetails.Rows.Count == 0)
            {
                mvFees.SetActiveView(ViewFeesNotFound);
                lblMessage.Text = "No Slab Found!!!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["feesAction"] != null)
            {
                LabelAction.Text = Session["feesAction"].ToString();
                feesAction = 1;
                Session["feesAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["feesAction"] = null;
        }
    }
    protected void btnAddFees_Click(object sender, EventArgs e)
    {
        lblAddEdit.Text = "Add New Fee Formula";
        txtSlab.Text = "";
        txtAmount.Text = "";
        txtDescription.Text = "";
        txt_slab_increment.Text = "";
        txt_slab_increment_fee.Text = "";
        hdnfield.Value = "add";
        ModalPopupExtender1.TargetControlID = "btnAddFees";
        ModalPopupExtender1.Show();
        DropDownListForm.Visible = true;
        lblForm.Visible = false;
    }
    
    protected void gvFeesDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
            mvFees.SetActiveView(ViewFeesNotFound);
            lblMessage.Text = "No Fees found!";
        }
    }
    protected void gvFeesDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objFees = new clsMasterFees();
            //THIS IS FOR EDITING
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Edit Fee Formula";

                Myds = new DataSet();
                //ImageButton imgedit = (ImageButton)e.CommandSource;

                //TO RECOGNIZE WHAT ACTION TO DO IN SUBMIT BUTTON CLICK
                hdnfield.Value = "edit";

                //CAPTURING PRIMARY ID
                HiddenField1.Value = e.CommandArgument.ToString();

                //FILLING DATASET AND TEXTBOXES USING LINQ
                Myds = objFees.MasterFeesFormula_Fetch_DataSet(Convert.ToInt32(e.CommandArgument));

                lblForm.Text = Myds.Tables[0].Rows[0]["FormTypeName"].ToString();
                txtSlab.Text = Myds.Tables[0].Rows[0]["fGPtbl_MFF_BaseSlab"].ToString();
                txtAmount.Text = Myds.Tables[0].Rows[0]["fGPtbl_MFF_BaseSlabFee"].ToString();
                txt_slab_increment.Text = Myds.Tables[0].Rows[0]["fGPtbl_MFF_ExceedSlab"].ToString();
                txt_slab_increment_fee.Text = Myds.Tables[0].Rows[0]["fGPtbl_MFF_ExceedSlabFee"].ToString();
                txtDescription.Text = Myds.Tables[0].Rows[0]["fGPtbl_MFee_Desc"].ToString();
                HdnFormtypeId.Value = Myds.Tables[0].Rows[0]["fGPtbl_MFee_FormTypeId"].ToString();

                DropDownListForm.Visible = false;
                lblForm.Visible = true;

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "dte")
            {
                successId = objFees.MasterFeeFormula_DeleteById(Convert.ToInt32(e.CommandArgument), UserId);

                if (successId == 1)
                {
                    Session["feesAction"] = "Deleted successfully";
                }
                else
                {
                    Session["feesAction"] = "Server is busy, please try later";
                }

                GridViewDataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objFees = new clsMasterFees();
        //ID= 1:DATA ALREADY EXIST 2:SUCCESSFULLY INSERTED 3:ERROR IN OPERATION 4:SUCCESSFULLY UPDATED
        int id = 0;
        try
        {
            //TO EDIT VALUE
            if (hdnfield.Value == "edit")
            {
              //  id = objFees.MasterFees_InsertUpdate(Convert.ToInt32(HiddenField1.Value.ToString()), Convert.ToInt32(HdnFormtypeId.Value),  txtSlab.Text, Convert.ToDouble(txtAmount.Text), txtDescription.Text, UserId);
                id = objFees.MasterFees_InsertUpdate_MasterFeeFormula(Convert.ToInt32(HiddenField1.Value.ToString()), Convert.ToInt32(HdnFormtypeId.Value), Convert.ToDouble(txtSlab.Text), Convert.ToDouble(txtAmount.Text), Convert.ToDouble(txt_slab_increment.Text), Convert.ToDouble(txt_slab_increment_fee.Text), txtDescription.Text, UserId);
            }
            //TO ADD NEW DATA
            else if (hdnfield.Value == "add")
            {
                id = objFees.MasterFees_InsertUpdate_MasterFeeFormula(0, Convert.ToInt32(DropDownListForm.SelectedValue.ToString()), Convert.ToDouble(txtSlab.Text), Convert.ToDouble(txtAmount.Text), Convert.ToDouble(txt_slab_increment.Text), Convert.ToDouble(txt_slab_increment_fee.Text), txtDescription.Text, UserId);
            }
            switch (id)
            {
                case 1://data already exist
                    Session["feesAction"] = "already exist";
                    break;
                case 2://data inserted successfully
                    Session["feesAction"] = "Added successfully";
                    break;
                case 3:// error in operation
                    Session["feesAction"] = "Server is busy, please try later";
                    break;
                case 4://data updated successfully
                    Session["feesAction"] = "Updated successfully";
                    break;
                default://for any error
                    Session["feesAction"] = "Server is busy, please try later";
                    break;
            }
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public void DropDownFormBind()
    {
        try
        {
            clsMasterFormType objFormtype = new clsMasterFormType();
            Myds = new DataSet();

            Myds = objFormtype.MasterFormType_Fetch_DataSet();
            DataTable dt = new DataTable();
            DataTable FirstDataTable=new DataTable();
             DataTable SecondDataTable=new DataTable();
         dt = Myds.Tables[0];

         if (dt.Rows.Count > 0)
         {

             foreach (DataRow dr in dt.Rows)
             {
                 if (dr["fGPtbl_MF_Name"].ToString() == "Competency Certificate" || dr["fGPtbl_MF_Name"].ToString() == "Capacity Verification")
                 {
                     dr.Delete();
                 }
                 //if (dr["fGPtbl_MF_Name"].ToString() == "Capacity Verification")
                 //{
                 //    dr.Delete();
                 //}
             }

             dt.AcceptChanges();

              }

         if (dt.Rows.Count > 0)
         {
             btnAddFees.Visible = true;
             DropDownListForm.DataSource = dt;//Myds.Tables[0];
             DropDownListForm.DataTextField = dt.Columns[1].Caption; //Myds.Tables[0].Columns[1].Caption;
             DropDownListForm.DataValueField = dt.Columns[0].Caption; //Myds.Tables[0].Columns[0].Caption;
             DropDownListForm.DataBind();
             DropDownListForm.Items.Insert(0, new ListItem("--Select form--", ""));
          
         }

         else
         {
             btnAddFees.Visible = false;
         }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

 

}

