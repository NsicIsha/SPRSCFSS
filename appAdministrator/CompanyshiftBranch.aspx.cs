using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using NSICGP_VBClassLibrary;
using System.Collections;


public partial class appAdministrator_CompanyshiftBranch : System.Web.UI.Page
{
    int RoleId, UserId;
    clsCompany objcls;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    int   DepartmentId;
    string UserFullName = "";
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

        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            FunRequest();
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());

            if (!Page.IsPostBack)
            {
                Bind_drodown_parent();
                Bind_dropdown_child();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.Message);
        }
    }
    protected void ddl_branch_parent_SelectedIndexChanged(object sender, EventArgs e)
    {
        int BranchId_parent = Convert.ToInt32(ddl_branch_parent.SelectedValue.ToString());
        Bind_gridview(BranchId_parent);
        Bind_dropdown_child();
    }

    public void Bind_gridview(int BranchID)
    {
       // int BranchID = Convert.ToInt32(ddl_branch_parent.SelectedValue.ToString());
        objcls = new clsCompany();
        DataTable dt_fetch = new DataTable();
        //dt_fetch = objcls.Fetch_Company_Branchwise(BranchID);
        dt_fetch = Fetch_Company_Branchwise(BranchID);
        if (dt_fetch.Rows.Count > 0)
        {
            grd_bind_company.DataSource = dt_fetch;
            grd_bind_company.DataBind();
            ViewState["CompanyList"] = dt_fetch;
          
        }
        else
        {
            dt_fetch.Dispose();
            grd_bind_company.DataBind();
            ViewState["CompanyList"] = "";
        }
    }

    public void Bind_drodown_parent()
    {
        DataTable dt = new DataTable();
        dt = clsDDL.ddlBranch_Activated(0);
        if (dt.Rows.Count > 0)
        {
            ddl_branch_parent.DataSource = dt;
            ddl_branch_parent.DataTextField = "fGPtbl_MB_Name";
            ddl_branch_parent.DataValueField = "fGPtbl_MB_Id";
            ddl_branch_parent.DataBind();
            ddl_branch_parent.Items.Insert(0, new ListItem("--Select Branch--", ""));
        }
        else
        {
            dt.Dispose();
            ddl_branch_parent.Items.Insert(0, new ListItem("--Select Branch--", ""));
        }

    }

    public void Bind_dropdown_child()
    {
        DataTable dt_child = new DataTable();
        dt_child = clsDDL.ddlBranch_Activated(0);
        if (dt_child.Rows.Count > 0)
        {
            ddl_branch_child.DataSource = dt_child;
            ddl_branch_child.DataTextField="fGPtbl_MB_Name";
            ddl_branch_child.DataValueField = "fGPtbl_MB_Id";
            ddl_branch_child.DataBind();
            ddl_branch_child.Items.Insert(0, new ListItem("--Select Branch--", ""));
            
        }
        else
        {
            dt_child.Dispose();
            ddl_branch_child.Items.Insert(0, new ListItem("--Select Branch--", ""));
        }
    }
    protected void btn_tranfer_Click(object sender, EventArgs e)
    {
        updateCompany();
       
        
    }

    public void updateCompany()
    {
        try
        {
            int BranchId_child = Convert.ToInt32(ddl_branch_child.SelectedValue);
            objcls = new clsCompany();
            string companytext = Request.Form["conchk"].ToString();
            if (Request.Form["conchk"] != null)
            {
                string[] words = companytext.Split(',');
                foreach (var word in words)
                {
                    string splitdata = word.ToString();
                    string []splitcompany = splitdata.Split('/');
                    string Companyids = splitcompany[0].ToString();
                    string companystatus = splitcompany[1].ToString();

                  //  int k = objcls.Company_shift_Branch_update(Convert.ToInt32(Companyids.ToString()), BranchId_child, Convert.ToInt32(companystatus.ToString()));
                    int k = Company_shift_Branch_update(Convert.ToInt32(Companyids.ToString()), BranchId_child, Convert.ToInt32(companystatus.ToString()));
                    Companyids = "";
                    companystatus = "";

                }
            }


          /*  foreach (GridViewRow row in grd_bind_company.Rows)
            {
                CheckBox chk = row.Cells[0].Controls[0] as CheckBox;
                if (chk != null && chk.Checked)
                {
                   // Label hdn_companyid = (Label)row.Cells[0].FindControl("hiden_com_id");
                 //   int company_id_text = Convert.ToInt32(hdn_companyid.Value);
                       //  int companyid= Convert.ToInt32(row.Cells[0].Value.ToString());
                    int kk =  Convert.ToInt32(row.Cells[0].Text);              
                    // ...
                }
            }*/
              

        /*    if (ViewState["CompanyList"] != null)
            {
                DataTable dtcurrent = new DataTable();
                dtcurrent = (DataTable)ViewState["CompanyList"];
                if (dtcurrent.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtcurrent.Rows.Count - 1; i++)
                    {
                     //   int k = objcls.Company_shift_Branch_update(Convert.ToInt32(dtcurrent.Rows[i]["ComapnyID"].ToString()), BranchId_child, Convert.ToInt32(dtcurrent.Rows[i]["CompanyStatus"].ToString()));
                    }
                }
                else
                {
                    dtcurrent.Dispose();
                    ViewState["CompanyList"] = "";
                }
            }*/
         //   int branchid_child = Convert.ToInt32(ddl_branch_child.SelectedValue);
            Response.Redirect("CompanyshiftBranch.aspx", true);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }

    public DataTable Fetch_Company_Branchwise(int BranchID)
    {
        DataTable dt_product = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("NSIC_Fetch_Company", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@BranchID", SqlDbType.Int).Value = Convert.ToInt32(BranchID);
            //  MySqlComm.Parameters

            MyAdp.Fill(dt_product);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return dt_product;
    }

    public int Company_shift_Branch_update(int CompanyID, int BranchID, int StatusID)
    {
        int Returnvalue = 0;
        try
        {
            MySqlComm = new SqlCommand("GP_sp_Shift_Branch_Company", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@BranchID", SqlDbType.Int).Value = BranchID;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;
            MySqlComm.Parameters.Add("@status", SqlDbType.Int).Value = StatusID;

            SqlParameter outError = new SqlParameter("@Outvalue", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();

            Returnvalue = Convert.ToInt32(outError.Value.ToString());
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return Returnvalue;
    }
  
}
