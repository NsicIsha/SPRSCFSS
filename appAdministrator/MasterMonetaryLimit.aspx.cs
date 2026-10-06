using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_MasterMonetaryLimit : System.Web.UI.Page
{
    clsMasterMailer objMailer;
    int successId;

    int UserId, RoleId, DepartmentId;

    string UserFullName;


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

            //SETTING SUBMIT BUTTON AS DEFAULT BUTTON
            //txtBrancHead.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            //txtZonalHead.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            txtHO.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");
            if (!Page.IsPostBack)
            {
                Bindrole();
                BindMonetarylimitGrid();
            }
            if (RoleId != 1)
            {
                btnSubmit.Visible = false;
            }
            else
            {
                btnSubmit.Visible = true;
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

    private void BindMonetarylimitGrid()
    {
        try
        {
            clsMasterApplication objMaster = new clsMasterApplication();
            DataTable dt = new DataTable();
            dt = objMaster.Fetch_Monetarylimit();
            if (dt != null)
            {
                pnl_grid.Visible = true;
                pnladd.Visible = false;
                gv_mon_limit.DataSource = dt;
                gv_mon_limit.DataBind();
            }
            else
            {
                gv_mon_limit.DataSource = null;
                gv_mon_limit.DataBind();
                gv_mon_limit.EmptyDataText = "No Record,s";
            }
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void Bindrole()
    {
        try
        {
            clsMasterApplication objMaster = new clsMasterApplication();
            DataTable DT = new DataTable();
            DT = objMaster.Fetch_Userrole();
            ddlrole.DataSource = DT;
            ddlrole.DataBind();
            ddlrole.Items.Insert(0, new ListItem("--Select Role--", "0"));
            
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void BindDesignation( int roleid)
    {
        try
        {
            clsMasterUser objUser = new clsMasterUser();
            DataTable dgdt;
            dgdt = objUser.Master_FETCH_Department(roleid);
            ddldisg.DataSource = dgdt;
            ddldisg.DataBind();
            ddldisg.Items.Insert(0, new ListItem("--Select Designation--", "0"));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //TO UPDATE MONETARY LIMIT
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        clsMasterApplication objMaster = new clsMasterApplication();
        float BranchHead, ZonalHead, HO;
        BranchHead = 0;
        ZonalHead = 0;
        HO = 0;
        try
        {
            int roleid, desigid;
            double amount;
            roleid = Convert.ToInt32(ddlrole.SelectedValue);
            desigid = Convert.ToInt32(ddldisg.SelectedValue);

            

            bool chkinput = objMaster.IsValidmONETARY_iNPUT(roleid, desigid);
            if (chkinput == true)
            {
                ltlMessage.Text = " Already exists";
                return;
            }
            
            amount = 0.0;
            //if (txtBrancHead.Text.Trim() != "")
            //{
            //    BranchHead = Convert.ToSingle(txtBrancHead.Text.Trim());
            //}

            //if (txtZonalHead.Text.Trim() != "")
            //{
            //    ZonalHead = Convert.ToSingle(txtZonalHead.Text.Trim());
            //}

            if (txtHO.Text.Trim() != "")
            {
                amount = Convert.ToDouble(txtHO.Text.Trim());
            }

            successId = objMaster.MasterMonetaryLimit_Insert_update(roleid,desigid,amount,UserId);

            if (successId == 1 || successId == 3)
            {
                ltlMessage.Text = "Added /Updated Successfully";
                txtHO.Text = "";
                pnl_grid.Visible = true;
                pnladd.Visible = false;
                BindMonetarylimitGrid();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlrole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int roleid = Convert.ToInt32(ddlrole.SelectedValue);
            BindDesignation(roleid);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        pnl_grid.Visible = false;
        Bindrole();
        BindDesignation(Convert.ToInt32(ddlrole.SelectedValue));
        txtHO.Text = "";
        ddlrole.Enabled = true;
        ddldisg.Enabled = true;
        txtHO.Enabled = true;
    }
    protected void gv_mon_limit_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "view")
            {
                clsMasterApplication objMaster = new clsMasterApplication();
                DataTable dt = new DataTable();
                int id = Convert.ToInt32(e.CommandArgument);
                dt = objMaster.Fetch_Monetarylimit_byID(id);
                if (dt != null)
                {
                    pnl_grid.Visible = false;
                    pnladd.Visible = true;
                    ddlrole.SelectedValue = dt.Rows[0]["fGPtbl_MML_SanctionAuthority_Id"].ToString();
                    BindDesignation(Convert.ToInt32(ddlrole.SelectedValue));
                    ddldisg.SelectedValue = dt.Rows[0]["fGPtbl_MML_departmentId"].ToString();
                    txtHO.Text = dt.Rows[0]["fGPtbl_MML_MonetaryUpperLimit"].ToString();
                    ddlrole.Enabled = false;
                    ddldisg.Enabled = false;
                    txtHO.Enabled = false;
                }
            }
            else if (e.CommandName == "edt")
            {
                clsMasterApplication objMaster = new clsMasterApplication();
                DataTable dt = new DataTable();
                int id = Convert.ToInt32(e.CommandArgument);
                dt = objMaster.Fetch_Monetarylimit_byID(id);
                if (dt != null)
                {
                    pnl_grid.Visible = false;
                    pnladd.Visible = true;
                    ddlrole.SelectedValue = dt.Rows[0]["fGPtbl_MML_SanctionAuthority_Id"].ToString();
                    BindDesignation(Convert.ToInt32(ddlrole.SelectedValue));
                    ddldisg.SelectedValue = dt.Rows[0]["fGPtbl_MML_departmentId"].ToString();
                    txtHO.Text = dt.Rows[0]["fGPtbl_MML_MonetaryUpperLimit"].ToString();
                    ddlrole.Enabled = true;
                    ddldisg.Enabled = true;
                    txtHO.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        ltlMessage.Text = "";
        pnladd.Visible = false;
        pnl_grid.Visible = true;
    }
}
