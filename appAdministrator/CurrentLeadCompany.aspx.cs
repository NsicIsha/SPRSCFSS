using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;


public partial class appAdministrator_CurrentLeadCompany : System.Web.UI.Page
{
    int USERID, ROLEID;
    string USERFULLNAME;
    string SearchText = string.Empty;
    int BranchID = 0;
    int ZoneID = 0;
    clsMasterBranch objBranch;
    clsMasterZone objZone;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if ((ROLEID != 1 && ROLEID != 10) &&(ROLEID!=5) )
            {
                if (ROLEID == 2)
                {
                    if (!IsPostBack)
                    {
                        BranchID = 0;
                        ZoneID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);

                        ddl_zonewise.Items.Clear();
                        ddl_branch_wise.Items.Clear();

                        DropDownZoneBind();
                        ddl_zonewise.SelectedValue = Convert.ToString(ZoneID);
                        ddl_zonewise.Attributes.Add("Readonly", "Readonly");
                        DropDownBranchBind(ZoneID);
                        ddl_branch_wise.Visible = true;
                        ddl_zonewise.Visible = false;
                    }
                }
                else if (ROLEID == 3)
                {
                    if (!IsPostBack)
                    {
                        BranchID = 0;
                        ZoneID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
                        BranchID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                        ddl_zonewise.Items.Clear();
                        ddl_branch_wise.Items.Clear();
                        ddl_branch_wise.Visible = false;
                        ddl_zonewise.Visible = false;
                        DropDownZoneBind();
                        ddl_zonewise.SelectedValue = Convert.ToString(ZoneID);
                        ddl_zonewise.Attributes.Add("Readonly", "Readonly");
                        DropDownBranchBind(ZoneID);
                        ddl_branch_wise.SelectedValue = Convert.ToString(BranchID);
                        ddl_branch_wise.Attributes.Add("Readonly", "Readonly");
                    }
                }
             

                else
                {
                    BranchID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                    ZoneID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_MZ_Id"]);
                    ddl_zonewise.Items.Clear();
                    ddl_branch_wise.Items.Clear();
                    ddl_branch_wise.Visible = false;
                    ddl_zonewise.Visible = false;

                }
                    Bind_grid(BranchID, ZoneID);
               
               
            }
            else
            {
                if (!IsPostBack)
                {
                    ddl_branch_wise.Items.Clear();
                   ddl_branch_wise.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                    DropDownZoneBind();
                    Bind_grid(BranchID, ZoneID);
                }
            }
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
    }


    // Bind Gridview 
    public void Bind_grid( int BranchID, int Zoneid)
    {
        try
        {
            DataTable dt_grid = new DataTable();
            DataTable dt_filter = new DataTable();


            dt_grid = dt_result(BranchID, Zoneid);
            if (dt_grid != null && dt_grid.Rows.Count > 0)
            {
               
                //dt_filter=dt_grid.AsEnumerable().Where() 
                if (ddl_company_filled.SelectedValue != "0")
                {
                    string CompanyBranch = "";
                    CompanyBranch = ddl_company_filled.SelectedItem.Text;
                    var filter = dt_grid.AsEnumerable().Where(x => x.Field<string>("CompanyorBranch") == CompanyBranch.Trim().ToString());
                   dt_filter = filter.CopyToDataTable();
                }
                else
                {
                    var filter = dt_grid.AsEnumerable();
                    dt_filter =filter.CopyToDataTable();
                }


                gvDetails.DataSource = dt_filter; //dt_grid;
                gvDetails.DataBind();
            }
            else
            {
                gvDetails.DataBind();
                dt_grid.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public DataTable dt_result(int Branch_Id, int ZoneId)
    {
        DataTable dt;
        SqlDataAdapter MyADp;
        dt = new DataTable();
        try
        {

           
            MyADp = new SqlDataAdapter("GP_sp_Current_lead_Company", DBManager.ConnectToDB());
            MyADp.SelectCommand.CommandType = CommandType.StoredProcedure;
           if(Branch_Id!=0) MyADp.SelectCommand.Parameters.Add("@branchId", SqlDbType.Int).Value = Branch_Id;
           if (ZoneId != 0) MyADp.SelectCommand.Parameters.Add("@ZoneID", SqlDbType.Int).Value = ZoneId;
            MyADp.Fill(dt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
          
        }
        return dt;

    }

    // Bind zone  and Branch 
    public void DropDownZoneBind()
    {
      DataSet  Myds = new DataSet();
        objZone = new clsMasterZone();
        Myds = objZone.MasterZone_Fetch_DataSet_Activated();
        ddl_zonewise.Items.Clear();


        ddl_zonewise.DataSource = Myds.Tables[0];
        ddl_zonewise.DataTextField = Myds.Tables[0].Columns[1].Caption;
        ddl_zonewise.DataValueField = Myds.Tables[0].Columns[0].Caption;
        ddl_zonewise.DataBind();
        ddl_zonewise.Items.Insert(0, new ListItem("--All--", "0"));
     
    }


    public void DropDownBranchBind(int zoneid)
    {
        DataTable dt = new DataTable();

        try
        {
         
            dt = clsDDL.ddlBranch(zoneid);
            //REBINDING WITH NEW VALUE AS 'ALL'


            ddl_branch_wise.DataSource = dt;
            ddl_branch_wise.DataTextField = dt.Columns["fGPtbl_MB_Name"].ToString();
            ddl_branch_wise.DataValueField = dt.Columns["fGPtbl_MB_Id"].ToString();
            ddl_branch_wise.DataBind();
            ddl_branch_wise.Items.Insert(0, new ListItem("--Select Branch--", "0"));
              
          
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


  

    protected void gvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDetails.PageIndex = e.NewPageIndex;
        if (Convert.ToInt32(ddl_zonewise.SelectedValue) != 0 )
        {
            Bind_grid(Convert.ToInt32(ddl_branch_wise.SelectedValue), Convert.ToInt32(ddl_zonewise.SelectedValue));
        }
        else
        {
            Bind_grid(BranchID, ZoneID);
        }
    }


    protected void ddl_zonewise_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddl_branch_wise_SelectedIndexChanged(object sender, EventArgs e)
    {
        int zoneid_b = 0;
        int brancid_b = 0;
        zoneid_b = Convert.ToInt32(ddl_zonewise.SelectedValue);
        brancid_b = Convert.ToInt32(ddl_branch_wise.SelectedValue);
        if (brancid_b != 0)
        {
            ddl_branch_wise.SelectedValue = brancid_b.ToString();
            ddl_branch_wise.DataBind();
          //  DropDownBranchBind(zoneid_b);
            Bind_grid(brancid_b, zoneid_b);

        }
        else
        {
                  ddl_branch_wise.Items.Clear();
                 ddl_branch_wise.Items.Insert(0, new ListItem("--Select Branch--", "0"));
                 DropDownZoneBind();
            Bind_grid(BranchID, ZoneID);

        }
    }
    protected void gvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk_view = (LinkButton)e.Row.FindControl("lnkview");
                HiddenField hdn_company_id = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview");
                
                HiddenField Hdn_Branch_name = (HiddenField)e.Row.FindControl("HiddenFieldBranch");
                Label lbl_compnay_name = (Label)e.Row.FindControl("lbl_company_name");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyID = objDecEnc.Encrypt(hdn_company_id.Value.ToString());
                string EncryptCompany_Name = objDecEnc.Encrypt(lbl_compnay_name.Text.ToString());
                string EncryptID = objDecEnc.Encrypt("0");
                string EncryptBranchName = objDecEnc.Encrypt(Hdn_Branch_name.Value.ToString());
                string imageid = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + EncryptCompanyID + "&Com_Name=" + EncryptCompany_Name + "&id=" + EncryptID + "&BranchName=" + EncryptBranchName;
                lnk_view.Attributes.Add("onclick", "return FunOpenPopup('" + imageid + "');");
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }




    protected void ddl_zonewise_SelectedIndexChanged1(object sender, EventArgs e)
    {
        int zoneid_b = 0;
        int brancid_b = 0;
        zoneid_b = Convert.ToInt32(ddl_zonewise.SelectedValue);
        brancid_b = Convert.ToInt32(ddl_branch_wise.SelectedValue);
        if (zoneid_b != 0)
        {
            DropDownBranchBind(zoneid_b);
            Bind_grid(brancid_b, zoneid_b);

        }
        else if (brancid_b != 0)
        {
            ddl_branch_wise.SelectedValue = brancid_b.ToString();
            ddl_branch_wise.DataBind();
            Bind_grid(brancid_b, zoneid_b);
        }
        else
        {
            ddl_branch_wise.Items.Clear();
            ddl_branch_wise.Items.Insert(0, new ListItem("--Select Branch--", "0"));
            Bind_grid(BranchID, ZoneID);

        }
    }
    protected void ddl_company_filled_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((ROLEID == 1 ||  ROLEID == 10) || (ROLEID == 5 ) )
        {
        BranchID = Convert.ToInt32( ddl_branch_wise.SelectedValue);
        ZoneID = Convert.ToInt32( ddl_zonewise.SelectedValue);
        }
        else if (ROLEID == 2)
        {
            BranchID = Convert.ToInt32(ddl_branch_wise.SelectedValue);
        }
        else if(ROLEID==3)
        {
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            BranchID = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(USERID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
        }

        Bind_grid(BranchID, ZoneID);
    }
}
