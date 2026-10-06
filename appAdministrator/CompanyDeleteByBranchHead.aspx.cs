using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
using System.IO;


public partial class appAdministrator_CompanyDeleteByBranchHead : System.Web.UI.Page
{
    int RoleId, UserId, Branch_ID, Zone_ID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());


            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId == 1 || RoleId == 8 || RoleId == 10 || RoleId == 3)
            {
                Branch_ID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                Zone_ID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
                if (!Page.IsPostBack)
                {
                    BindGridview();
                }

            }
            else
            {
                // Bind_Gridview();
                // btnSearch.Visible = false;
                Response.Redirect("logout.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }

    }


    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int Fw_id = 0;
        if (e.CommandName.ToUpper().Trim().ToString() == "EDP")
        {

            Fw_id =  Convert.ToInt32( e.CommandArgument.ToString());


     
            // Bindtexbox(Convert.ToInt32(hid_comapnyId.Value));
     
        }
    }

    public void UpdatedeleteTable(int Id)
    {
        try
        {
              
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
          
    }

    protected void gv_company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_company.PageIndex = e.NewPageIndex;
        BindGridview();
    }

    public void BindGridview()
    {
        try
        {
            DataSet ds = new DataSet();
            ds = GetData(Branch_ID, Zone_ID);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                gv_company.DataSource = ds.Tables[0];
                gv_company.DataBind();
            }
            else
            {
                gv_company.DataBind();
                ds.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }
    }

    public DataSet GetData(  int BranchID,  int ZoneID)
        {
            DataSet MyDs = new DataSet();
          SqlDataAdapter MyAdp;
          try
          {
              MyAdp = new SqlDataAdapter("Sp_Delete_Company_By_Branch", DBManager.ConnectToDB());
              MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
              if (BranchID!=0) MyAdp.SelectCommand.Parameters.Add("@BranchID ", SqlDbType.Int).Value = BranchID;
              if (ZoneID != 0) MyAdp.SelectCommand.Parameters.Add("@ZoneID ", SqlDbType.Int).Value = ZoneID;
              MyAdp.Fill(MyDs);

          }
          catch (Exception ex)
          {
              ErrHandler.WriteError(ex.Message);
          }

          return MyDs;

    }
}
