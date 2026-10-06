using System;
using NSICGP_VBClassLibrary;
using System.Data;

public partial class appAdministrator_Unit_Branchshift : System.Web.UI.Page
{
    clsMasterBranch objBranch;
    clsCompany objCompany;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    DataSet myDs;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            Branchid = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);

           
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }


  

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        funSearch();
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int comid, brid, success;
        try
        {
            objBranch = new clsMasterBranch();
            comid = Convert.ToInt32(hdnfieldcomid.Value);
            brid = Convert.ToInt32(DropDownListBranch.SelectedValue);
            success = objBranch.Update_ShiftBranch(comid, brid, UserId);
            if (success == 2)
            {
                lblmess.Text = "Shifted successfully";
            }
            else
            {
                lblmess.Text = "Server Busy";
            }
            funSearch();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void grdCompanylist_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        myDs = new DataSet();
        try
        {
            if (e.CommandName == "changbr")
            {
                objBranch = new clsMasterBranch();
                myDs = objBranch.MasterBranch_FetchActivated_DataSet();

                DropDownListBranch.DataSource = myDs.Tables[0];
                DropDownListBranch.DataTextField = myDs.Tables[0].Columns["fGPtbl_MB_Name"].ToString();
                DropDownListBranch.DataValueField = myDs.Tables[0].Columns["fGPtbl_MB_Id"].ToString();
                DropDownListBranch.DataBind();

                hdnfieldcomid.Value = e.CommandArgument.ToString();
                ModalPopupExtender1.Show();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            myDs.Dispose();
        }
    }

    protected void funSearch()
    {
        myDs = new DataSet();
        
        try
        {
            objBranch = new clsMasterBranch();
            myDs = objBranch.Fetch_Companydetails_ByBranchid(Branchid, txtSearchbox.Text.Trim());
          /*  if (myDs.Tables[0].Rows.Count > 0)
            {*/
                grdCompanylist.DataSource = myDs;
                grdCompanylist.DataBind();
            //}
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            myDs.Dispose();
        }
    }


}
