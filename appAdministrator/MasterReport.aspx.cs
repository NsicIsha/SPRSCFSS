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
using System.Globalization;
//using TestWebMsgApp;


public partial class appAdministrator_MasterReport : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int m = 0;
    string NameID = "";
    string Role = "";
    string RoleDescription = string.Empty;
    int count = 1;
 int StrReportMaster ;
 string script = string.Empty;

    clsCompany objCompany;
     DataTable MYdt;

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

            if (RoleId == 1)
            {
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    //DropDownRoleBind();
                    BindMainGrid();
                    DropDownRoleBind();
                    lblSetValue.Text = "";
                   // BindGrid();
                    
                }
            }
            else
            {
               
            }
            //btn_submit.Attributes.Add("onclick", "return SetMessage();");
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
    }
    // Bind Main Grid

    public void BindMainGrid()
    {
        try
        {
            btn_Add_Grid.Visible = true;

            objCompany = new clsCompany();
            DataTable DTMain = new DataTable();
            DTMain = objCompany.Fetch_RoleName_Description();
            if (DTMain.Rows.Count > 0)
            {
                gvMasterDetails.DataSource = DTMain;
                gvMasterDetails.DataBind();

            }
            else
            {
                gvMasterDetails.Dispose();
                gvMasterDetails.DataBind();
                DTMain.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //
    public void DropDownRoleBind()
    {
        try
        {
            DataTable dt;
            dt = new DataTable();
            dt = clsDDL.ddlRoleExceptAdmin();
            DropDownListRole.DataSource = dt;
            DropDownListRole.DataTextField = dt.Columns["fGPtbl_MR_Name"].ToString();
            DropDownListRole.DataValueField = dt.Columns["fGPtbl_MR_Id"].ToString();
            DropDownListRole.DataBind();
            DropDownListRole.Items.Insert(0, new ListItem("--Select Role--", "0"));
            //DropDownListRole.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void BindGrid()
    {
        try
        {
            objCompany = new clsCompany();
            MYdt = new DataTable();
            MYdt = objCompany.Fetch_ReportName();
            if (MYdt.Rows.Count > 0)
            {
                grdchek_Report.DataSource = MYdt;
                grdchek_Report.DataBind();
            }
            else
            {
                grdchek_Report.Dispose();
                grdchek_Report.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
     

    protected void btn_submit_Click(object sender, EventArgs e)
    {
           
       // Role = txt_Role.Text.ToString();
      //  RoleDescription = txt_role_description.Text.ToString();
        CheckBox Chk = new CheckBox();
        GridViewRow D = default(GridViewRow);
        string NameID = "";
        foreach (GridViewRow row in grdchek_Report.Rows)
        {
            if (((CheckBox)row.FindControl("myCheckBox")).Checked)
            {
                Label lbl_id = (Label)row.FindControl("lbl_id");
                string ChekedRow = lbl_id.Text.Trim().ToString();
                NameID += ChekedRow + ',';
            }
        }
        string RoleNameID = NameID;
       
        objCompany = new clsCompany();


        if (btn_submit.Text == "UPDATE" || btn_submit.Text=="ADD")
            {
                int MR_ID=Convert.ToInt32(Session["RoleID"].ToString());
               
               // int statusId = 1;
                //DropDownListRole.SelectedItem.Text.ToString(), DropDownListRole.SelectedItem.Text.ToString(),
                StrReportMaster = objCompany.Report_Insert_Name_Description(MR_ID, RoleNameID,  UserId);
                if (StrReportMaster != 0)
                {
                    //lblMessage.Text = "Data Update Succesfully!!!";
                script = "javascript:return alert('Data Update Succesfully!!');";
                
                    BindMainGrid();
                   // WebMsgBox.Show("Data Update Succesfully!!!");
                    DivMain.Visible = false;

                    gvMasterDetails.Visible = true;

                    
                }
                else
                {
                  //  WebMsgBox.Show("Server Busy!!!");
                   // lblMessage.Text = "Server Busy!!!";
                     script = "javascript:return alert('Server Busy!!!');";
                    BindMainGrid();
                    
                    DivMain.Visible = false;
                   
                    gvMasterDetails.Visible = true;
                 
                   
                }
                btn_submit.Attributes.Add("onclick", script); 

            }
       // }
    }
    protected void gvReportDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edp")
            {
                ImageButton imgedit = (ImageButton)e.CommandSource;

                btn_submit.Text = "UPDATE";
                gvMasterDetails.Visible = true;
                DataTable dt_RoleID = new DataTable();
                objCompany = new clsCompany();
                Session["RoleID"] = Convert.ToInt32(e.CommandArgument);
                dt_RoleID = objCompany.Fetch_RoleName_Description_Accoroding_To_RoleID(Convert.ToInt32(e.CommandArgument));
                if (dt_RoleID.Rows.Count > 0)
                {
                    DropDownListRole.SelectedValue = dt_RoleID.Rows[0]["fGPtbl_MR_Id"].ToString();
                    DropDownListRole.SelectedItem.Text = dt_RoleID.Rows[0]["RoleName"].ToString();
                    DropDownListRole.Visible = false;
                    lblSetValue.Text = DropDownListRole.SelectedItem.Text.ToString();
                    gvMasterDetails.Visible = false;
                    DivMain.Visible = true;
                    BindGrid();
                }
                else
                {
                    dt_RoleID.Dispose();

                }
                //  Ds=

           
              
        }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void gvReptDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //FILLING SERIAL NO. IN GRIDVIEW USING LABEL
                Label lblSno = (Label)e.Row.FindControl("lblSno");
                lblSno.Text = count.ToString();
                count++;

              
                ImageButton imgedit = (ImageButton)e.Row.FindControl("imgButton");

                // SETTING ACTIVE AND DEACTIVE CAPTION BY CHECKING VALUE
              

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            count = 0;
        }
       

    }
    protected void btn_Add_Grid_Click(object sender, EventArgs e)
    {
        try
        {
            gvMasterDetails.Visible = false;
            DivMain.Visible = true;
            Session["RoleID"] = "";
            Session.Remove("RoleID");
            btn_Add_Grid.Visible = false;
            btn_submit.Text = "ADD";
            DropDownListRole.Visible = true;
            lblSetValue.Visible = false;
            //lblMessage.Visible = false;
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
   
    protected void grdchek_Report_onRowdataBound(object sender, GridViewRowEventArgs e)
    {

        Session["RoleID"] = DropDownListRole.SelectedValue.ToString();
        if (Session["RoleID"] != "" || Session["RoleID"]!="0")
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                   
                    objCompany = new clsCompany();
                    DataTable dt_Role = new DataTable();
                    int RoleID4 = Convert.ToInt32(Session["RoleID"].ToString());
                    dt_Role = objCompany.Fetch_Report_Name_reportId(RoleID4);
                    //if (dt_Role.Rows.Count > 0)
                    //{
                         
                         
                        Label lbl_id = (Label)e.Row.FindControl("lbl_id");
                        CheckBox chkbox = (CheckBox)e.Row.FindControl("myCheckBox");

                       
                            foreach (DataRow drrow in dt_Role.Rows)
                            {
                                string Rows =Convert.ToString(drrow["ReportNameId"].ToString());

                                if (lbl_id.Text == Rows.ToString())
                                {
                                  //  CheckBox chkbox1 = (CheckBox)e.Row.FindControl("myCheckBox");
                                    chkbox.Checked = true;

                                }
                            }
                      

                    
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError(ex.Message);
            }
        }
        else
        {

        }
    }
    protected void DropDownListRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
                      
    }
}
