using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Collections;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;

using System.ComponentModel.Design;

public partial class appAdministrator_Admin_Edit_ValidityDate : System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    string PageUrl = string.Empty;
    public static string Text_pan_number { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            PageUrl = GetCurrentPageName();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //string roleidstr =RoleId.ToString();
        //if (!roleidstr.Contains("1") || roleidstr.Contains("8"))
        //{
        //    Response.Redirect("Home.aspx");
        //}

        //cHANGE bY vINAY RESON ACCESS THIS PAGE TO BRANCH USER ALSO 21FEB 2014
        if (RoleId != 1 || RoleId != 8)
        {

        }
        else
        {
            Response.Redirect("Home.aspx");
        }



    }

    private void Bind_GridView()
    {
        try
        {
            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = objCompany.Company_Fetch_EditValiditydate_SearchByAdmin(Convert.ToInt32(ddlSearchCriteria.SelectedValue), txtSearchbox.Text.Trim());
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    gv_company.DataSource = dt;
                    gv_company.DataBind();
                }
                else
                {
                    dt.Dispose();
                    gv_company.DataBind();
                }
            }
            if (Convert.ToInt32(ddlSearchCriteria.SelectedValue) == 2)
            {
                UpdatefactoryGrid();
                FillRollbackGrid(txtSearchbox.Text.Trim());
                FillDirectorGrid();
                FillUnitRevivalGrid(txtSearchbox.Text.Trim());


                spanDirector.Visible = true;
                spanFactory.Visible = true;
                spanRollback.Visible = true;
                spanRevival.Visible = true;
            }
            else
            {
                resetbox();
                spanDirector.Visible = false;
                spanFactory.Visible = false;
                spanRollback.Visible = false;
                spanRevival.Visible = false;
            }




        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    private void FillRollbackGrid(string pan)
    {
        DataTable dataTable = new DataTable();
        string query = "select fGPtbl_Company_Id,fGPtbl_Company_Name,fGPtbl_Company_PAN,fGPtbl_Company_HeadOffice_Address from GPtbl_Company where [fGPtbl_Company_PAN]='" + pan + "' and  fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 60000;
                        sda.SelectCommand = cmd;
                        sda.Fill(dataTable);
                    }
                    grdRollback.DataSource = dataTable;
                    grdRollback.DataBind();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }



    private void FillUnitRevivalGrid(string pan)
    {
        DataTable dataTable = new DataTable();
        string query = "select fGPtbl_Company_Id,fGPtbl_Company_Name,CASE fGPtbl_Company_Status\r\n        WHEN 1 THEN 'Live'\r\n        WHEN 3 THEN 'Waiting'\r\n        WHEN 6 THEN 'Expired'\r\n        WHEN 5 THEN 'In Complete'\r\n    ELSE 'Unknown'\r\n    END AS Company_Status, fGPtbl_Company_PAN_Tracking,fGPtbl_Company_PAN,FORMAT(fGPtbl_Company_DDate, 'dd/MM/yy') AS fGPtbl_Company_DDate from GPtbl_Company where fGPtbl_Company_PAN_Tracking='" + pan + "' and  fGPtbl_Company_Display=0 and fGPtbl_Company_Delete=1";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 60000;
                        sda.SelectCommand = cmd;
                        sda.Fill(dataTable);
                    }
                    gvUnitRevival.DataSource = dataTable;
                    gvUnitRevival.DataBind();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
    private void FillDirectorGrid()
    {
        string companyID = ExtractCompanyID();
        DataTable dataTable = new DataTable();
        string query = "select fGPtbl_Company_Directors_Id,fGPtbl_Company_Directors_CompanyId,fGPtbl_Company_Directors_Name,fGPtbl_Company_Directors_Address from GPtbl_Company_Directors where [fGPtbl_Company_Directors_CompanyId]='" + companyID + "' and  fGPtbl_Company_Directors_Display=1 and fGPtbl_Company_Directors_Delete=0 order by fGPtbl_Company_Directors_Id ASC";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 60000;
                        sda.SelectCommand = cmd;
                        sda.Fill(dataTable);
                    }
                    grdDirectors.DataSource = dataTable;
                    grdDirectors.DataBind();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
    private void UpdatefactoryGrid()
    {
        string companyID = ExtractCompanyID();
        DataTable dataTable = new DataTable();
        string query = "select fGPtbl_CUnit_Id, case when fGPtbl_CUnit_Enterprise=1 then 'Manufacturing' when fGPtbl_CUnit_Enterprise=2 then 'Services' else 'nothing'  end as fGPtbl_CUnit_Enterprise,  fGPtbl_CUnit_1B_Factory_Address from GPtbl_CUnit where [fGPtbl_CUnit_CompanyId]='" + companyID + "' and  fGPtbl_CUnit_Display=1 and fGPtbl_CUnit_Delete=0 order by fGPtbl_CUnit_Id ASC";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        cmd.CommandTimeout = 60000;
                        sda.SelectCommand = cmd;
                        sda.Fill(dataTable);
                    }
                    grdFactory.DataSource = dataTable;
                    grdFactory.DataBind();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
    private string ExtractCompanyID()
    {

        string querystring = "Select fGPtbl_Company_Id from GPtbl_Company where fGPtbl_Company_PAN='" + txtSearchbox.Text.Trim() + "' and fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(querystring, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    return reader[0].ToString();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        return constr;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        Bind_GridView();
    }
    protected void gv_company_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "editvalidity")
        {
            hid_comapnyId.Value = e.CommandArgument.ToString();
            txt_registartiondate.Text = "";
            ModalPopupExtender3.Hide();
            ModalPopupExtender2.Hide();
            ModalPopupExtender1.Show();
            ModalPopupExtender4.Hide();
            ModalPopupExtender5.Hide();
            ModalPopupExtender6.Hide();
        }
        else if (e.CommandName.ToUpper().Trim().ToString() == "EDITPAN")
        {
            lblmsg.Text = string.Empty;
            hid_comapnyId.Value = e.CommandArgument.ToString();
            txt_panno.Text = "";
            ddl_type_ref_pan.SelectedValue = "1";
            lbl_company_id_onpnl.Value = hid_comapnyId.Value;
            // Bindtexbox(Convert.ToInt32(hid_comapnyId.Value));
            ModalPopupExtender3.Hide();
            ModalPopupExtender1.Hide();
            ModalPopupExtender2.Show();
            ModalPopupExtender4.Hide();
            ModalPopupExtender5.Hide();
            ModalPopupExtender6.Hide();
        }

        else if (e.CommandName == "editCompany")
        {
            hid_comapnyId.Value = e.CommandArgument.ToString();
            txt_company_name.Text = "";
            ModalPopupExtender1.Hide();
            ModalPopupExtender2.Hide();
            ModalPopupExtender3.Show();
            ModalPopupExtender4.Hide();
            ModalPopupExtender5.Hide();
            ModalPopupExtender6.Hide();

            BindComapnyName(Convert.ToInt32(hid_comapnyId.Value));
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objCompany = new clsCompany();
        int outid;
        outid = objCompany.Update_ComapnY_Registartion(Convert.ToInt32(hid_comapnyId.Value), txt_registartiondate.Text);
        Bind_GridView();
    }
    protected void btnSubmitpan_Click(object sender, EventArgs e)
    {
        objCompany = new clsCompany();

        string PANREFNumber = string.Empty;
        int ddltype = Convert.ToInt32(ddl_type_ref_pan.SelectedValue);
        if (ddl_type_ref_pan.SelectedValue == "1")
        {
            PANREFNumber = txt_panno.Text.Trim().ToString();
        }
        if (ddl_type_ref_pan.SelectedValue == "2")
        {
            PANREFNumber = txt_panno.Text.Trim().ToString();
        }
        if (ddl_type_ref_pan.SelectedValue == "3")
        {
            PANREFNumber = txt_panno.Text.Trim().ToString();
        }
        if (ddl_type_ref_pan.SelectedValue == "4")
        {
            PANREFNumber = txt_panno.Text.Trim().ToString();
        }
        if (ddl_type_ref_pan.SelectedValue == "5")
        {
            PANREFNumber = txt_panno.Text.Trim().ToString();
        }
        int outId = objCompany.f_Update_Comapny_PAN(Convert.ToInt32(hid_comapnyId.Value), txt_panno.Text.Trim().ToString(), ddltype, PageUrl);
        if (outId == 3)
        {
            if (ddl_type_ref_pan.SelectedValue == "1")
            {
                lblmsg.Text = "PAN No   <b> <i> " + txt_panno.Text + " </i></b>    Already exists";
                lbl_pan_change.Text = "PAN NO :";
                txt_panno.MaxLength = 10;
            }
            else if (ddl_type_ref_pan.SelectedValue == "2")
            {
                lblmsg.Text = "REF No   <b> <i> " + txt_panno.Text + " </i></b>    Already exists";
                lbl_pan_change.Text = "REF NO :";
                txt_panno.MaxLength = 50;
            }
            else if (ddl_type_ref_pan.SelectedValue == "3")
            {
                lblmsg.Text = "Old Reg No   <b> <i> " + txt_panno.Text + " </i></b>    Already exists";
                lbl_pan_change.Text = "REF NO :";
                txt_panno.MaxLength = 50;
            }
            else if (ddl_type_ref_pan.SelectedValue == "4")
            {
                lblmsg.Text = "New Reg No   <b> <i> " + txt_panno.Text + " </i></b>    Already exists";
                lbl_pan_change.Text = "REF NO :";
                txt_panno.MaxLength = 50;
            }
            ModalPopupExtender2.Show();

        }
        else if (outId == 2)
        {
            ModalPopupExtender2.Show();
        }
        else
        {
            ModalPopupExtender2.Hide();
            Bind_GridView();
        }
    }
    protected void gv_company_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void gv_company_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    [System.Web.Services.WebMethod]
    public static string Bindtexbox(string Companyid, string type)
    {
        try
        {
            clsCompany objCompany = new clsCompany();
            DataSet ds_bind_textbox = new DataSet();
            ds_bind_textbox = objCompany.Fetch_PanRegistration_old(Convert.ToInt32(Companyid));
            if (ds_bind_textbox.Tables[0].Rows.Count > 0)
            {
                if (type == "1")
                {
                    Text_pan_number = ds_bind_textbox.Tables[0].Rows[0]["PanNumber"].ToString();

                }
                else if (type == "2")
                {
                    Text_pan_number = ds_bind_textbox.Tables[0].Rows[0]["Refrencenumber"].ToString();
                }
                else if (type == "3")
                {
                    Text_pan_number = ds_bind_textbox.Tables[0].Rows[0]["RegistrationOld"].ToString();
                }
                else if (type == "4")
                {
                    Text_pan_number = ds_bind_textbox.Tables[0].Rows[0]["NewregistrationNo"].ToString();
                }
                else
                {
                    Text_pan_number = "";
                }
            }
            else
            {
                ds_bind_textbox.Dispose();
                // txt_panno.Text = "";
            }
            // Bind_pannumbertext();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
        return Text_pan_number.ToString();
    }
    public void Bind_pannumbertext()
    {
        try
        {
            txt_panno.Text = Text_pan_number.ToString();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    protected void gv_company_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv_company.PageIndex = e.NewPageIndex;
        Bind_GridView();
    }
    public string GetCurrentPageName()
    {
        string urlPath = Request.Url.AbsolutePath;
        FileInfo fileInfo = new FileInfo(urlPath);
        string pageName = fileInfo.Name;
        return pageName;
    }


    protected void gv_company_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //  Find hidden filed containing 0,1 flag value that company is renewed or not 
            string hidval = ((HiddenField)e.Row.FindControl("hdn_chkrenewaed")).Value;
            LinkButton lnkvalidity = (LinkButton)(e.Row.FindControl("lbnDraftPrint"));
            if (hidval == "0")
            {
                lnkvalidity.Enabled = true;
            }
            else
            {
                lnkvalidity.Enabled = false;
            }
        }
    }


    // Bind CompanyName  With Cmapny ID

    public void BindComapnyName(int CompanyID)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlDataAdapter MyAdp;
            MyAdp = new SqlDataAdapter("GP_Sp_FetchCompanyNameWithCompanyID", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyID", SqlDbType.Int).Value = CompanyID;

            MyAdp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= ds.Tables[0].Rows.Count; i++)
                {
                    hdncompanyId_Pop.Value = Convert.ToString(ds.Tables[0].Rows[0]["ComapnyID"].ToString());
                    txt_company_name.Text = ds.Tables[0].Rows[0]["ComapnyName"].ToString();
                }
            }
            else
            {
                ds.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }

    }
    protected void btn_CompanyName_Click(object sender, EventArgs e)
    {
        int ComapnyID = Convert.ToInt32(hdncompanyId_Pop.Value);
        string CompanyName = Convert.ToString(txt_company_name.Text);

        UpdateCompanyData(ComapnyID, CompanyName);
    }

    public void UpdateCompanyData(int ComapnyID, string ComapnyName)
    {
        DataSet ds = new DataSet();
        int result = 0;
        try
        {
            SqlCommand MySqlComm;
            int Status = 0;

            MySqlComm = new SqlCommand("GP_SP_CompanyNameUpdate", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@ComapnayID", SqlDbType.Int).Value = ComapnyID;
            MySqlComm.Parameters.Add("@CompanyName", SqlDbType.VarChar, 500).Value = ComapnyName;
            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);
            MySqlComm.ExecuteNonQuery();
            Status = Convert.ToInt32(outError.Value);
            if (Status == 1)
            {
                ModalPopupExtender3.Hide();
                Bind_GridView();
            }
            else
            {
                ModalPopupExtender3.Show();
                lbl_company.Text = "Your Comapny Name not Update";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + " " + ex.StackTrace.ToString());
        }

    }
    int counter = 0;
    protected void grdFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (counter == 0)
            {
                var linkButton = e.Row.FindControl("DeleteButton") as LinkButton;
                linkButton.Visible = false;
                counter++;
            }
        }


    }

    protected void DeleteButton_Command(object sender, CommandEventArgs e)
    {
        string querystring = "update GPtbl_CUnit set fGPtbl_CUnit_Display=2 ,fGPtbl_CUnit_Delete=2 where fGPtbl_CUnit_Id='" + e.CommandArgument.ToString() + "' and  fGPtbl_CUnit_Display=1 and fGPtbl_CUnit_Delete=0";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(querystring, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                UpdatefactoryGrid();
            }
        }



    }

    protected void AddM_Command(object sender, CommandEventArgs e)
    {
        funfill_ddlState();
        txtCUID.Text = e.CommandArgument.ToString();
        txtType.Text = "Manufacturing";
        ModalPopupExtender3.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender4.Show();
        ModalPopupExtender5.Hide();
    }

    protected void AddS_Command(object sender, CommandEventArgs e)
    {
        funfill_ddlState();
        txtCUID.Text = e.CommandArgument.ToString();
        txtType.Text = "Services";
        ModalPopupExtender3.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender4.Show();
        ModalPopupExtender5.Hide();
    }

    protected void ddlState_a3_SelectedIndexChanged(object sender, EventArgs e)
    {

        ModalPopupExtender4.Show();
        //System.Threading.Thread.Sleep(5000);
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlState_a3.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlCity_a4(fStateId);


    }

    private void funfill_ddlCity_a4(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlCity_a4.DataSource = dt_c;
            ddlCity_a4.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlCity_a4.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlCity_a4.DataBind();
            ddlCity_a4.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();


        }
        else
        {
            ddlCity_a4.Items.Clear();
        }

    }
    private void funfill_ddlDirState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlDirState.DataSource = dt;
        ddlDirState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlDirState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlDirState.DataBind();
        ddlDirState.Items.Insert(0, new ListItem("--Select State--", ""));

        dt.Clear();
        dt.Dispose();
    }
    private void funfill_ddlDirCity(int fStateId)
    {
        //CITY DROPDOWNLIST
        if (fStateId != 0)
        {
            System.Data.DataTable dt_c = new System.Data.DataTable();
            dt_c = clsDDL.ddlCity_Activated(fStateId);
            ddlDirCity.DataSource = dt_c;
            ddlDirCity.DataTextField = dt_c.Columns["fGPtbl_MCty_Name"].ToString();
            ddlDirCity.DataValueField = dt_c.Columns["fGPtbl_MCty_Id"].ToString();
            ddlDirCity.DataBind();
            ddlDirCity.Items.Insert(0, new ListItem("--Select City--", ""));
            dt_c.Clear();
            dt_c.Dispose();


        }
        else
        {
            ddlDirCity.Items.Clear();
        }

    }
    private void funfill_ddlState()
    {
        //STATE DROPDOWNLIST
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlState_Activated();
        ddlState_a3.DataSource = dt;
        ddlState_a3.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString();
        ddlState_a3.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
        ddlState_a3.DataBind();
        ddlState_a3.Items.Insert(0, new ListItem("--Select State--", ""));

        dt.Clear();
        dt.Dispose();
    }
    void resetbox()
    {
        grdDirectors.DataSource = null;
        grdDirectors.DataBind();
        grdFactory.DataSource = null;
        grdFactory.DataBind();
        grdRollback.DataSource = null;
        grdRollback.DataBind();
        ddlCity_a4.DataSource = null;
        ddlDirCity.DataSource = null;
        ddlDirState.DataSource = null;
        ddlState_a3.DataSource = null;
        txtCUID.Text = string.Empty;
        txtDirAddress.Text = string.Empty;
        txtDirName.Text = string.Empty;
        txtDirPincode.Text = string.Empty;
        txtNewFacAdd.Text = string.Empty;
        txtPincode.Text = string.Empty;
        txtType.Text = string.Empty;

    }

    protected void btnFactAdd_Click(object sender, EventArgs e)
    {
        string query = string.Empty;
        switch (txtType.Text)
        {
            case "Manufacturing":
                query = "insert into [dbo].[GPtbl_CUnit] ([fGPtbl_CUnit_CompanyId], [fGPtbl_CUnit_RegistrationNo_OLD]," +
    "[fGPtbl_CUnit_RegistrationNo], [fGPtbl_CUnit_RegistrationStatus], [fGPtbl_CUnit_SSIRegistrationNo], [fGPtbl_CUnit_SSIRegistrationDate], " +
    "[fGPtbl_CUnit_FormtypeId], [fGPtbl_CUnit_RegistrationDate], [fGPtbl_CUnit_RenewalDate], [fGPtbl_CUnit_CategoryAreaId], [fGPtbl_CUnit_CategoryCastId]," +
    " [fGPtbl_CUnit_CategoryUnitSizeId], [fGPtbl_CUnit_BranchId], [fGPtbl_CUnit_1B_Factory_Address], [fGPtbl_CUnit_1B_Factory_Zip], [fGPtbl_CUnit_1B_Factory_StateId]," +
    " [fGPtbl_CUnit_1B_Factory_CityId], [fGPtbl_CUnit_1B_Factory_Phone], [fGPtbl_CUnit_1B_Factory_Fax], [fGPtbl_CUnit_1B_Factory_Email], [fGPtbl_CUnit_3A], [fGPtbl_CUnit_3B]," +
    " [fGPtbl_CUnit_3C123_ConstitutionId], [fGPtbl_CUnit_3C5], [fGPtbl_CUnit_3D], [fGPtbl_CUnit_3E], [fGPtbl_CUnit_3F], [fGPtbl_CUnit_3G], [fGPtbl_CUnit_3H]," +
    " [fGPtbl_CUnit_3I], [fGPtbl_CUnit_3J], [fGPtbl_CUnit_3K1], [fGPtbl_CUnit_3K2], [fGPtbl_CUnit_3L], [fGPtbl_CUnit_3M], [fGPtbl_CUnit_3N], [fGPtbl_CUnit_3O]," +
    " [fGPtbl_CUnit_4A], [fGPtbl_CUnit_4B], [fGPtbl_CUnit_5A1], [fGPtbl_CUnit_5A2], [fGPtbl_CUnit_5BA], [fGPtbl_CUnit_5BB1], [fGPtbl_CUnit_5BB2]," +
    " [fGPtbl_CUnit_5C], [fGPtbl_CUnit_6A1], [fGPtbl_CUnit_6A2], [fGPtbl_CUnit_6B], [fGPtbl_CUnit_7A], [fGPtbl_CUnit_7B], [fGPtbl_CUnit_7C], [fGPtbl_CUnit_7D]," +
    " [fGPtbl_CUnit_7E], [fGPtbl_CUnit_8], [fGPtbl_CUnit_9], [fGPtbl_CUnit_10], [fGPtbl_CUnit_11], [fGPtbl_CUnit_12A], [fGPtbl_CUnit_12B], [fGPtbl_CUnit_13A]," +
    " [fGPtbl_CUnit_13B], [fGPtbl_CUnit_13C], [fGPtbl_CUnit_AccessId], [fGPtbl_CUnit_StatusId], [fGPtbl_CUnit_CUser], [fGPtbl_CUnit_EUser], [fGPtbl_CUnit_DUser]," +
    " [fGPtbl_CUnit_CDate], [fGPtbl_CUnit_EDate], [fGPtbl_CUnit_DDate], [fGPtbl_CUnit_Display], [fGPtbl_CUnit_Delete], [fGPtbl_CUnit_Enterprise], [fGPtbl_CUnit_ApprovalDate], " +
    "[fGPtbl_CUnit_ValidityStartDate], [fGPtbl_CUnit_6A3], [fGPtbl_CUnit_6B2], [fGPtbl_CUnit_12C], [fGPtbl_Registration_Status_ChangeDate], [fGPtbl_Isgraduate], [fGPtbal_From_Date], [fGPtbal_To_Date], [fGPtbl_CUnit_RegistrationNo_111018], [Certificate_Proprietor_Partner_Directors])" +
    " select [fGPtbl_CUnit_CompanyId], [fGPtbl_CUnit_RegistrationNo_OLD], [fGPtbl_CUnit_RegistrationNo], [fGPtbl_CUnit_RegistrationStatus], [fGPtbl_CUnit_SSIRegistrationNo], [fGPtbl_CUnit_SSIRegistrationDate], [fGPtbl_CUnit_FormtypeId], [fGPtbl_CUnit_RegistrationDate], [fGPtbl_CUnit_RenewalDate], [fGPtbl_CUnit_CategoryAreaId], [fGPtbl_CUnit_CategoryCastId], [fGPtbl_CUnit_CategoryUnitSizeId], [fGPtbl_CUnit_BranchId], '" + txtNewFacAdd.Text + "','" + txtPincode.Text + "' ,'" + ddlState_a3.SelectedValue + "' ,'" + ddlCity_a4.SelectedValue + "', [fGPtbl_CUnit_1B_Factory_Phone], [fGPtbl_CUnit_1B_Factory_Fax], [fGPtbl_CUnit_1B_Factory_Email], [fGPtbl_CUnit_3A], [fGPtbl_CUnit_3B], [fGPtbl_CUnit_3C123_ConstitutionId], [fGPtbl_CUnit_3C5], [fGPtbl_CUnit_3D], [fGPtbl_CUnit_3E], [fGPtbl_CUnit_3F], [fGPtbl_CUnit_3G], [fGPtbl_CUnit_3H], [fGPtbl_CUnit_3I], [fGPtbl_CUnit_3J], [fGPtbl_CUnit_3K1], [fGPtbl_CUnit_3K2], [fGPtbl_CUnit_3L], [fGPtbl_CUnit_3M], [fGPtbl_CUnit_3N], [fGPtbl_CUnit_3O], [fGPtbl_CUnit_4A], [fGPtbl_CUnit_4B], [fGPtbl_CUnit_5A1], [fGPtbl_CUnit_5A2], [fGPtbl_CUnit_5BA], [fGPtbl_CUnit_5BB1], [fGPtbl_CUnit_5BB2], [fGPtbl_CUnit_5C], [fGPtbl_CUnit_6A1], [fGPtbl_CUnit_6A2], [fGPtbl_CUnit_6B], [fGPtbl_CUnit_7A], [fGPtbl_CUnit_7B], [fGPtbl_CUnit_7C], [fGPtbl_CUnit_7D], [fGPtbl_CUnit_7E], [fGPtbl_CUnit_8], [fGPtbl_CUnit_9], [fGPtbl_CUnit_10], [fGPtbl_CUnit_11], [fGPtbl_CUnit_12A], [fGPtbl_CUnit_12B], [fGPtbl_CUnit_13A], [fGPtbl_CUnit_13B], [fGPtbl_CUnit_13C], [fGPtbl_CUnit_AccessId], [fGPtbl_CUnit_StatusId], [fGPtbl_CUnit_CUser], [fGPtbl_CUnit_EUser], [fGPtbl_CUnit_DUser], [fGPtbl_CUnit_CDate], [fGPtbl_CUnit_EDate], [fGPtbl_CUnit_DDate], 1, 0, 1, [fGPtbl_CUnit_ApprovalDate], [fGPtbl_CUnit_ValidityStartDate], [fGPtbl_CUnit_6A3], [fGPtbl_CUnit_6B2], [fGPtbl_CUnit_12C], [fGPtbl_Registration_Status_ChangeDate], [fGPtbl_Isgraduate], [fGPtbal_From_Date], [fGPtbal_To_Date], [fGPtbl_CUnit_RegistrationNo_111018], [Certificate_Proprietor_Partner_Directors] from [dbo].[GPtbl_CUnit] where [fGPtbl_CUnit_Id]='" + txtCUID.Text + "'";
                break;
            case "Services":
                query = "insert into [dbo].[GPtbl_CUnit] ([fGPtbl_CUnit_CompanyId], [fGPtbl_CUnit_RegistrationNo_OLD]," +
   "[fGPtbl_CUnit_RegistrationNo], [fGPtbl_CUnit_RegistrationStatus], [fGPtbl_CUnit_SSIRegistrationNo], [fGPtbl_CUnit_SSIRegistrationDate], " +
   "[fGPtbl_CUnit_FormtypeId], [fGPtbl_CUnit_RegistrationDate], [fGPtbl_CUnit_RenewalDate], [fGPtbl_CUnit_CategoryAreaId], [fGPtbl_CUnit_CategoryCastId]," +
   " [fGPtbl_CUnit_CategoryUnitSizeId], [fGPtbl_CUnit_BranchId], [fGPtbl_CUnit_1B_Factory_Address], [fGPtbl_CUnit_1B_Factory_Zip], [fGPtbl_CUnit_1B_Factory_StateId]," +
   " [fGPtbl_CUnit_1B_Factory_CityId], [fGPtbl_CUnit_1B_Factory_Phone], [fGPtbl_CUnit_1B_Factory_Fax], [fGPtbl_CUnit_1B_Factory_Email], [fGPtbl_CUnit_3A], [fGPtbl_CUnit_3B]," +
   " [fGPtbl_CUnit_3C123_ConstitutionId], [fGPtbl_CUnit_3C5], [fGPtbl_CUnit_3D], [fGPtbl_CUnit_3E], [fGPtbl_CUnit_3F], [fGPtbl_CUnit_3G], [fGPtbl_CUnit_3H]," +
   " [fGPtbl_CUnit_3I], [fGPtbl_CUnit_3J], [fGPtbl_CUnit_3K1], [fGPtbl_CUnit_3K2], [fGPtbl_CUnit_3L], [fGPtbl_CUnit_3M], [fGPtbl_CUnit_3N], [fGPtbl_CUnit_3O]," +
   " [fGPtbl_CUnit_4A], [fGPtbl_CUnit_4B], [fGPtbl_CUnit_5A1], [fGPtbl_CUnit_5A2], [fGPtbl_CUnit_5BA], [fGPtbl_CUnit_5BB1], [fGPtbl_CUnit_5BB2]," +
   " [fGPtbl_CUnit_5C], [fGPtbl_CUnit_6A1], [fGPtbl_CUnit_6A2], [fGPtbl_CUnit_6B], [fGPtbl_CUnit_7A], [fGPtbl_CUnit_7B], [fGPtbl_CUnit_7C], [fGPtbl_CUnit_7D]," +
   " [fGPtbl_CUnit_7E], [fGPtbl_CUnit_8], [fGPtbl_CUnit_9], [fGPtbl_CUnit_10], [fGPtbl_CUnit_11], [fGPtbl_CUnit_12A], [fGPtbl_CUnit_12B], [fGPtbl_CUnit_13A]," +
   " [fGPtbl_CUnit_13B], [fGPtbl_CUnit_13C], [fGPtbl_CUnit_AccessId], [fGPtbl_CUnit_StatusId], [fGPtbl_CUnit_CUser], [fGPtbl_CUnit_EUser], [fGPtbl_CUnit_DUser]," +
   " [fGPtbl_CUnit_CDate], [fGPtbl_CUnit_EDate], [fGPtbl_CUnit_DDate], [fGPtbl_CUnit_Display], [fGPtbl_CUnit_Delete], [fGPtbl_CUnit_Enterprise], [fGPtbl_CUnit_ApprovalDate], " +
   "[fGPtbl_CUnit_ValidityStartDate], [fGPtbl_CUnit_6A3], [fGPtbl_CUnit_6B2], [fGPtbl_CUnit_12C], [fGPtbl_Registration_Status_ChangeDate], [fGPtbl_Isgraduate], [fGPtbal_From_Date], [fGPtbal_To_Date], [fGPtbl_CUnit_RegistrationNo_111018], [Certificate_Proprietor_Partner_Directors])" +
   " select [fGPtbl_CUnit_CompanyId], [fGPtbl_CUnit_RegistrationNo_OLD], [fGPtbl_CUnit_RegistrationNo], [fGPtbl_CUnit_RegistrationStatus], [fGPtbl_CUnit_SSIRegistrationNo], [fGPtbl_CUnit_SSIRegistrationDate], [fGPtbl_CUnit_FormtypeId], [fGPtbl_CUnit_RegistrationDate], [fGPtbl_CUnit_RenewalDate], [fGPtbl_CUnit_CategoryAreaId], [fGPtbl_CUnit_CategoryCastId], [fGPtbl_CUnit_CategoryUnitSizeId], [fGPtbl_CUnit_BranchId], '" + txtNewFacAdd.Text + "','" + txtPincode.Text + "' ,'" + ddlState_a3.SelectedValue + "' ,'" + ddlCity_a4.SelectedValue + "', [fGPtbl_CUnit_1B_Factory_Phone], [fGPtbl_CUnit_1B_Factory_Fax], [fGPtbl_CUnit_1B_Factory_Email], [fGPtbl_CUnit_3A], [fGPtbl_CUnit_3B], [fGPtbl_CUnit_3C123_ConstitutionId], [fGPtbl_CUnit_3C5], [fGPtbl_CUnit_3D], [fGPtbl_CUnit_3E], [fGPtbl_CUnit_3F], [fGPtbl_CUnit_3G], [fGPtbl_CUnit_3H], [fGPtbl_CUnit_3I], [fGPtbl_CUnit_3J], [fGPtbl_CUnit_3K1], [fGPtbl_CUnit_3K2], [fGPtbl_CUnit_3L], [fGPtbl_CUnit_3M], [fGPtbl_CUnit_3N], [fGPtbl_CUnit_3O], [fGPtbl_CUnit_4A], [fGPtbl_CUnit_4B], [fGPtbl_CUnit_5A1], [fGPtbl_CUnit_5A2], [fGPtbl_CUnit_5BA], [fGPtbl_CUnit_5BB1], [fGPtbl_CUnit_5BB2], [fGPtbl_CUnit_5C], [fGPtbl_CUnit_6A1], [fGPtbl_CUnit_6A2], [fGPtbl_CUnit_6B], [fGPtbl_CUnit_7A], [fGPtbl_CUnit_7B], [fGPtbl_CUnit_7C], [fGPtbl_CUnit_7D], [fGPtbl_CUnit_7E], [fGPtbl_CUnit_8], [fGPtbl_CUnit_9], [fGPtbl_CUnit_10], [fGPtbl_CUnit_11], [fGPtbl_CUnit_12A], [fGPtbl_CUnit_12B], [fGPtbl_CUnit_13A], [fGPtbl_CUnit_13B], [fGPtbl_CUnit_13C], [fGPtbl_CUnit_AccessId], [fGPtbl_CUnit_StatusId], [fGPtbl_CUnit_CUser], [fGPtbl_CUnit_EUser], [fGPtbl_CUnit_DUser], [fGPtbl_CUnit_CDate], [fGPtbl_CUnit_EDate], [fGPtbl_CUnit_DDate], 1, 0, 2, [fGPtbl_CUnit_ApprovalDate], [fGPtbl_CUnit_ValidityStartDate], [fGPtbl_CUnit_6A3], [fGPtbl_CUnit_6B2], [fGPtbl_CUnit_12C], [fGPtbl_Registration_Status_ChangeDate], [fGPtbl_Isgraduate], [fGPtbal_From_Date], [fGPtbal_To_Date], [fGPtbl_CUnit_RegistrationNo_111018], [Certificate_Proprietor_Partner_Directors] from [dbo].[GPtbl_CUnit] where [fGPtbl_CUnit_Id]='" + txtCUID.Text + "'";
                break;
        }

        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        using (SqlConnection conn = new SqlConnection(constr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                UpdatefactoryGrid();
            }
        }
    }

    protected void btnRenewalRollback_Click(object sender, EventArgs e)
    {

    }

    protected void btnCompanyRevival_Command(object sender, CommandEventArgs e)
    {
        hdnCompanyId.Value = e.CommandArgument.ToString();
        // txtType.Text = "Manufacturing";
        ModalPopupExtender3.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender4.Hide();
        ModalPopupExtender5.Hide();

        ModalPopupExtender6.Show();
    }
    protected void btnRenewalRollback_Command(object sender, CommandEventArgs e)
    {
        SqlCommand MySqlComm;
       // string querystring, queryCunit, queryRaCombi = string.Empty;
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        switch (e.CommandName.ToString())
        {
            case "renewalRollback":
                //querystring = "update GPtbl_CompanyRenewal set fGPtbl_Company_Display=2 ,fGPtbl_Company_Delete=2 where fGPtbl_Company_Id='" + e.CommandArgument.ToString() + "' and  fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0";
                //queryCunit = "update GPtbl_CUnitRenewal set fGPtbl_CUnit_Display=2,fGPtbl_CUnit_Delete=2 where fGPtbl_CUnit_CompanyId='" + e.CommandArgument.ToString() + "' and fGPtbl_CUnit_Display =1 and fGPtbl_CUnit_Delete = 0";

                using (SqlConnection conn = new SqlConnection(constr))
                {
                    try
                    {
                        conn.Open();
                        //SqlCommand cmd = new SqlCommand(querystring, conn);
                        //SqlCommand cmdCunit = new SqlCommand(queryCunit, conn);
                        //cmd.ExecuteNonQuery();
                        //cmdCunit.ExecuteNonQuery();

                        MySqlComm = new SqlCommand("RollbackRenewalProcess", DBManager.ConnectToDB());
                        MySqlComm.CommandType = CommandType.StoredProcedure;
                        MySqlComm.Parameters.Add("@Company_ID", SqlDbType.Int).Value = e.CommandArgument.ToString();
                        MySqlComm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
                break;
            case "renewalAmendmentRollback":
                
                using (SqlConnection conn = new SqlConnection(constr))
                {
                    try
                    {
                        conn.Open();
                        MySqlComm = new SqlCommand("RollbackRenewalAmendmentProcess", DBManager.ConnectToDB());
                        MySqlComm.CommandType = CommandType.StoredProcedure;
                        MySqlComm.Parameters.Add("@Company_ID", SqlDbType.Int).Value = e.CommandArgument.ToString();

                        MySqlComm.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    finally
                    {
                        conn.Close();
                    }
                }

                //querystring = "update GPtbl_CompanyAmendment set fGPtbl_Company_Display=2 ,fGPtbl_Company_Delete=2 where fGPtbl_Company_Id='" + e.CommandArgument.ToString() + "' and  fGPtbl_Company_Display=1 and fGPtbl_Company_Delete=0";
                //queryCunit = "update GPtbl_CUnitAmendment set fGPtbl_CUnit_Display=2,fGPtbl_CUnit_Delete=2 where fGPtbl_CUnit_CompanyId='" + e.CommandArgument.ToString() + "' and fGPtbl_CUnit_Display =1 and fGPtbl_CUnit_Delete = 0";
                //queryRaCombi = "update Gptbl_RenAmendCombination set RA_Display=2,RA_Delete=2 where CompanyID='" + e.CommandArgument.ToString() + "' and RA_Display=1 and RA_Delete=0";
                //using (SqlConnection conn = new SqlConnection(constr))
                //{
                //    try
                //    {
                //        conn.Open();
                //        SqlCommand cmd = new SqlCommand(querystring, conn);
                //        SqlCommand cmdCunit = new SqlCommand(queryCunit, conn);
                //        SqlCommand cmdraCombi = new SqlCommand(queryRaCombi, conn);
                //        cmd.ExecuteNonQuery();
                //        cmdCunit.ExecuteNonQuery();
                //        cmdraCombi.ExecuteNonQuery();
                //    }
                //    catch (SqlException ex)
                //    {
                //        Console.WriteLine(ex.ToString());
                //    }
                //    finally
                //    {
                //        conn.Close();
                //    }
                //}
                break;
        }
    }

    protected void lnkdeleteDirector_Command(object sender, CommandEventArgs e)
    {
        string querystring = "update GPtbl_Company_Directors set fGPtbl_Company_Directors_Display=2 ,fGPtbl_Company_Directors_Delete=2 where fGPtbl_Company_Directors_Id='" + e.CommandArgument.ToString() + "' and  fGPtbl_Company_Directors_Display=1 and fGPtbl_Company_Directors_Delete=0";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();

        using (SqlConnection conn = new SqlConnection(constr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(querystring, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                FillDirectorGrid();
            }
        }
    }

    protected void lnkAddDirector_Command(object sender, CommandEventArgs e)
    {
        funfill_ddlDirState();
        hdDirectorID.Value = e.CommandArgument.ToString();
        // txtType.Text = "Manufacturing";
        ModalPopupExtender3.Hide();
        ModalPopupExtender2.Hide();
        ModalPopupExtender1.Hide();
        ModalPopupExtender4.Hide();
        ModalPopupExtender5.Show();

        ModalPopupExtender6.Hide();
    }

    protected void ddlDirState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ModalPopupExtender5.Show();
        //System.Threading.Thread.Sleep(5000);
        int fStateId = 0;
        try
        {
            fStateId = Convert.ToInt32(ddlDirState.SelectedValue.ToString());
        }
        catch (Exception ex)
        {
            fStateId = 0;
        }
        funfill_ddlDirCity(fStateId);
    }

    protected void btnDirAdd_Click(object sender, EventArgs e)
    {
        string addDir = "Insert into [dbo].[GPtbl_Company_Directors] ([fGPtbl_Company_Directors_CompanyId], [fGPtbl_Company_Directors_Name], [fGPtbl_Company_Directors_Address], [fGPtbl_Company_Directors_Zip], [fGPtbl_Company_Directors_StateId], [fGPtbl_Company_Directors_CityId], [fGPtbl_Company_Directors_Phone], [fGPtbl_Company_Directors_Shareholder], [fGPtbl_Company_Directors_Fax], [fGPtbl_Company_Directors_Email], [fGPtbl_Company_Directors_AccessId], [fGPtbl_Company_Directors_StatusId], [fGPtbl_Company_Directors_CUser], [fGPtbl_Company_Directors_EUser], [fGPtbl_Company_Directors_DUser], [fGPtbl_Company_Directors_CDate], [fGPtbl_Company_Directors_EDate], [fGPtbl_Company_Directors_DDate], [fGPtbl_Company_Directors_Display], [fGPtbl_Company_Directors_Delete], [fGPtbl_Company_Directors_Experience], [fGPtbl_Company_Directors_Adhar]) " +
            "select [fGPtbl_Company_Directors_CompanyId],'" + txtDirName.Text + "', '" + txtDirAddress.Text + "', '" + txtDirPincode.Text + "','" + ddlDirState.SelectedValue + "', '" + ddlDirCity.SelectedValue + "', [fGPtbl_Company_Directors_Phone], [fGPtbl_Company_Directors_Shareholder], [fGPtbl_Company_Directors_Fax], [fGPtbl_Company_Directors_Email], [fGPtbl_Company_Directors_AccessId], [fGPtbl_Company_Directors_StatusId], [fGPtbl_Company_Directors_CUser], [fGPtbl_Company_Directors_EUser], [fGPtbl_Company_Directors_DUser], [fGPtbl_Company_Directors_CDate], [fGPtbl_Company_Directors_EDate], [fGPtbl_Company_Directors_DDate], [fGPtbl_Company_Directors_Display], [fGPtbl_Company_Directors_Delete], [fGPtbl_Company_Directors_Experience], [fGPtbl_Company_Directors_Adhar] from GPtbl_Company_Directors where fGPtbl_Company_Directors_Id='" + hdDirectorID.Value + "'";
        string constr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        using (SqlConnection conn = new SqlConnection(constr))
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(addDir, conn);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conn.Close();
                FillDirectorGrid();
            }
        }
    }
}
