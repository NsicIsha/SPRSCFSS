using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Delete_Agency_User : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId, Branchid;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
          
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            
        }
        if (!Page.IsPostBack)
        {
            BindZone_Agency_User();
        }

        if (RoleId != 1)
        {
            Response.Redirect("Home.aspx");
        }
       
    }

    private void BindZone_Agency_User()
    {
      DataSet  Myds = new DataSet();
      clsMasterZone objZone = new clsMasterZone();
        Myds = objZone.MasterZone_Fetch_DataSet();

        DropDownListZone.DataTextField = Myds.Tables[0].Columns[1].Caption;
        DropDownListZone.DataValueField = Myds.Tables[0].Columns[0].Caption;
        DropDownListZone.DataSource = Myds.Tables[0];
        DropDownListZone.DataBind();
        DropDownListZone.Items.Insert(0, new ListItem("--Select Zone--", ""));

        DataSet MydsInsp = new DataSet();
        try
        {
            clsMasterInspectionAgency objInspbr = new clsMasterInspectionAgency();


            MydsInsp = objInspbr.MasterInspectionAgency_Fetch_DataSet();

            DropDownListInsp.DataSource = MydsInsp.Tables[0];
            DropDownListInsp.DataTextField = MydsInsp.Tables[0].Columns[1].Caption;
            DropDownListInsp.DataValueField = MydsInsp.Tables[0].Columns[0].Caption;
            DropDownListInsp.DataBind();
            DropDownListInsp.Items.Insert(0, new ListItem("--Select agency--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MydsInsp.Dispose();
        }

        try
        {
            DataSet Mydsbranch = new DataSet();
            clsMasterBranch objBranch = new clsMasterBranch();
            clsTrans_UserBranch objTUB = new clsTrans_UserBranch();

            Mydsbranch = objBranch.MasterBranch_Fetch_DataSet();

            DropDownListBranch.DataSource = Mydsbranch.Tables[0];
            DropDownListBranch.DataTextField = Mydsbranch.Tables[0].Columns[4].Caption;
            DropDownListBranch.DataValueField = Mydsbranch.Tables[0].Columns[0].Caption;
            DropDownListBranch.DataBind();
            DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", ""));
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    //protected void DropDownListZone_SelectedIndexChanged(object sender, EventArgs e)
    //{
        
    //}

    public void DropDownBranchBind(int zoneid)
    {
        DataTable dt = new DataTable();

        try
        {
            dt = clsDDL.ddlBranch(zoneid);
            //REBINDING WITH NEW VALUE AS 'ALL'
            

                DropDownListBranch.DataSource = dt;
                DropDownListBranch.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
                DropDownListBranch.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
                DropDownListBranch.DataBind();
                DropDownListBranch.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                DropDownListZone.AutoPostBack = true;
          

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void DropDownListZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownBranchBind(Convert.ToInt32(DropDownListZone.SelectedValue));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {

    }
}
