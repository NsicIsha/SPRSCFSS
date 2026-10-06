using NSICGP_VBClassLibrary;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_ProductCompanySearch : System.Web.UI.Page
{
    int UserId, RoleId, DepartmentId;
    string UserFullName = string.Empty;
    string date = "";
    clsMasterInspectionAgency objInspAgency;
    clsCompany objcompany;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds_city = new DataSet();
        clsCompany objcompany;
        try
        {
            objcompany = new clsCompany();
            if (ddlState.SelectedValue != "")
            {
                int StateID = Convert.ToInt32(ddlState.SelectedValue);
                ds_city = objcompany.Fetch_Company_city_wise(StateID);
                if (ds_city.Tables[0].Rows.Count > 0)
                {
                    Ddl_City_Wise.DataTextField = ds_city.Tables[0].Columns[1].Caption;
                    Ddl_City_Wise.DataValueField = ds_city.Tables[0].Columns[0].Caption;
                    Ddl_City_Wise.DataSource = ds_city.Tables[0];
                    Ddl_City_Wise.DataBind();
                    //  Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                    Ddl_City_Wise.Items.Insert(0, new ListItem("--All District--", ""));
                }
                else
                {
                    ds_city.Tables[0].Dispose();
                    Ddl_City_Wise.Items.Clear();
                    Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                    Ddl_City_Wise.DataBind();
                }

            }
            else
            {

                Ddl_City_Wise.Items.Clear();
                Ddl_City_Wise.Items.Insert(0, new ListItem("--Select City--", ""));
                Ddl_City_Wise.DataBind();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {

        }

    }
    protected void Page_Load(object sender, EventArgs e)
    {

       // Roleid == 10 || Roleid == 1 || Roleid ==


        if (!IsPostBack)
        {
            System.Data.DataTable dt = new DataTable();

            dt = clsDDL.ddlState();
            ddlState.DataSource = dt;
            ddlState.DataTextField = dt.Columns["fGPtbl_MS_Name"].ToString(); ;
            ddlState.DataValueField = dt.Columns["fGPtbl_MS_Id"].ToString();
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--All State--", ""));

            dt.Clear();
            dt.Dispose();
            Ddl_City_Wise.Items.Clear();
            Ddl_City_Wise.Items.Insert(0, new ListItem("--All District--", ""));
           // Ddl_City_Wise.Items.Insert(999, new ListItem("--All District--", ""));



            dt.Dispose();
        }

        try
        {

            // btnTRSCST_Submit.Visible=false;
            FunRequest();
            // READ COOKIES
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            // END READ COOKIES
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            string SearchText = string.Empty;
            SearchText = txt_serch_Criteria.Text.Trim().ToString();
            if (SearchText.ToUpper().Trim() == "Enter text to search")
            {
                SearchText = "";
            }

            else
            {
                SearchText = txt_serch_Criteria.Text.Trim().ToString();
            }
            if (SearchText != "")
            {
                lbl_search_data_item.Text = ddlSearchCriteria.SelectedItem.Text + " " + ":" + "" + "" + SearchText.ToString();
            }
            else
            {
                lbl_search_data_item.Text = "";
            }

            //  btnTRSCST_Submit.Visible = true;

            // BindGridview(Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchText);

            int State_Id = 0;
            int District_Id = 0;
            if(ddlState.SelectedIndex > 0)
            {
                State_Id =Convert.ToInt32(ddlState.SelectedValue);
            }
            if (Ddl_City_Wise.SelectedIndex > 0)
            {
                District_Id = Convert.ToInt32(Ddl_City_Wise.SelectedValue);
            }

            BindGridview(Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchText, State_Id, District_Id);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void grd_search_result_dg_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk_view = (LinkButton)e.Row.FindControl("lnkview");
                HiddenField hdn_company_id = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview");
                HiddenField Hdn_Unit_id = (HiddenField)e.Row.FindControl("HiddenFieldUnitId");
                HiddenField Hdn_Branch_name = (HiddenField)e.Row.FindControl("HiddenFieldBranch");
                Label lbl_compnay_name = (Label)e.Row.FindControl("lbl_Company");
                // Label lbl_storedetails = (Label)e.Row.FindControl("lbl_store");
                //      lbl_storedetails.Text = System.Web.HttpUtility.HtmlDecode(lbl_storedetails.Text);
                /*   Label lbl_company_validity = (Label)e.Row.FindControl("lbl_validititytext");
                   Label lbl_company_validity_text_show = (Label)e.Row.FindControl("lbl_validity_show");
                   Label lbl_renewalontext = (Label)e.Row.FindControl("lbl_renewal_date");
                   Label lblrenewalontextshow = (Label)e.Row.FindControl("lbl_renewal_date_show");
                   Label lblrenewalvalidity = (Label)e.Row.FindControl("lbl_renwal_validity");
                   Label lblrenewalvaliditytextshow = (Label)e.Row.FindControl("lbl_renwal_validityshow");
                   Label lbl_last_upadte_on = (Label)e.Row.FindControl("lbl_last_validity_text");
                   Label lbl_last_updateontextshow = (Label)e.Row.FindControl("lbl_last_validity_showtext");*/

                Label lbl_validity = (Label)e.Row.FindControl("lbl_validity");
                Label lbl_renewalcheck = (Label)e.Row.FindControl("lbl_renewal_date");
                Label lblfreshprovisinalcheck = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                Label lbl_checkvalidity_text = (Label)e.Row.FindControl("lbl_validity_text");
                Label lbl_chevalidity_show = (Label)e.Row.FindControl("lbl_validity_text_show");

                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyID = objDecEnc.Encrypt(hdn_company_id.Value.ToString());
                string EncryptCompany_Name = objDecEnc.Encrypt(lbl_compnay_name.Text.ToString());
                string EncryptID = objDecEnc.Encrypt(Hdn_Unit_id.Value.ToString());
                string EncryptBranchName = objDecEnc.Encrypt(Hdn_Branch_name.Value.ToString());
                string imageid = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + EncryptCompanyID + "&Com_Name=" + EncryptCompany_Name + "&id=" + EncryptID + "&BranchName=" + EncryptBranchName;
                objcompany = new clsCompany();

                DataSet ds_chekvality = new DataSet();
                ds_chekvality = objcompany.fetch_company_valitiy(Convert.ToInt32(hdn_company_id.Value));
                if (ds_chekvality.Tables[0].Rows.Count > 0)
                {

                    lbl_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
                    lbl_renewalcheck.Text = ds_chekvality.Tables[0].Rows[0]["RenewalDate_Check"].ToString();
                    lblfreshprovisinalcheck.Text = ds_chekvality.Tables[0].Rows[0]["fresh_provisional"].ToString();
                    lbl_registration_date.Text = ds_chekvality.Tables[0].Rows[0]["RegistrationDate"].ToString();

                    if (lbl_renewalcheck.Text == "1/1/1900 12:00:00 AM")
                    {
                        lbl_checkvalidity_text.Visible = false;
                        lbl_chevalidity_show.Visible = false;

                    }
                    else
                    {
                        CultureInfo ci = new CultureInfo("en-GB");
                        DateTime RegistrationStartDate = Convert.ToDateTime(lbl_registration_date.Text);
                        if (lblfreshprovisinalcheck.Text == "0")
                        {
                            DateTime RegistrationendDate = RegistrationStartDate.AddYears(2).AddDays(-1);
                            DateTime now = System.DateTime.Today.Date;
                            if ((now > RegistrationStartDate) && (now < RegistrationendDate))
                            {
                                lbl_checkvalidity_text.Visible = true;
                                lbl_chevalidity_show.Visible = true;
                                lbl_chevalidity_show.Text = RegistrationendDate.ToString("dd/MM/yyyy");
                            }
                        }
                        else
                        {
                            DateTime RegistrationendDate_pro = RegistrationStartDate.AddYears(1).AddDays(-1);
                            DateTime now = System.DateTime.Today.Date;
                            if ((now > RegistrationStartDate) && (now < RegistrationendDate_pro))
                            {
                                lbl_checkvalidity_text.Visible = true;
                                lbl_chevalidity_show.Visible = true;
                                lbl_chevalidity_show.Text = RegistrationendDate_pro.ToString("dd/MM/yyyy");
                            }
                        }
                    }



                }
                else
                {
                    ds_chekvality.Dispose();
                }


                lnk_view.Attributes.Add("onclick", "return FunOpenPopup('" + imageid + "');");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void grd_search_result_dg_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        try
        {
            grd_search_result_dg.PageIndex = e.NewPageIndex;
            string SearchText = string.Empty;
            SearchText = txt_serch_Criteria.Text.Trim().ToString();
            if (SearchText.ToUpper().Trim() == "Enter text to search")
            {
                SearchText = "";
            }
            else
            {
                SearchText = txt_serch_Criteria.Text.Trim().ToString();
            }

            int State_Id = 0;
            int District_Id = 0;
            if (ddlState.SelectedIndex == 0)
            {
                State_Id = Convert.ToInt32(ddlState.SelectedValue);
            }
            if (Ddl_City_Wise.SelectedIndex == 0)
            {
                District_Id = Convert.ToInt32(Ddl_City_Wise.SelectedValue);
            }

            BindGridview(Convert.ToInt32(ddlSearchCriteria.SelectedValue), SearchText, State_Id, District_Id);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    protected void BindGridview(int ddlcriteria, string searchtext, int state, int district)
    {
        try
        {
            int k = 0;
            objInspAgency = new clsMasterInspectionAgency();
            DataTable Mydt = new DataTable();
            // System.Data.SqlClient.SqlDataReader Dr = objInspAgency.ProductCompany_Search(ddlcriteria, searchtext, RoleId, UserId);
            Mydt = ProductCompany_Search(ddlcriteria, searchtext, RoleId, UserId, state, district); //ProductCompany_SearchobjInspAgency.ProductCompany_Search(ddlcriteria, searchtext, RoleId, UserId);
                                                                                   // if(Dr.HasRows)
                                                                                   //{
            if (Mydt.Rows.Count > 0)
            {
                /// btnTRSCST_Submit.Visible = true;

                grd_search_result_dg.DataSource = Mydt;
                grd_search_result_dg.DataBind();
                lbl_count_grid.Text = "Total" + " Companies " + ":" + "     " + " " + Mydt.Rows.Count;
                //k = k + 1;
            }
            //lbl_count_grid.Text = "Total" +" Companies "+ ":" + "     "+" " +k.ToString();
            //            Dr.Close();

            else
            {
                //  btnTRSCST_Submit.Visible = false;


                grd_search_result_dg.Dispose();
                grd_search_result_dg.DataBind();
                Mydt.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btntrscstSubmit_Click(object sender, EventArgs e)
    {

        Response.Clear();

        Response.AddHeader("content-disposition", "attachment;filename = FileName.xls");



        Response.ContentType = "application/vnd.xls";

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();

        System.Web.UI.HtmlTextWriter htmlWrite =
        new HtmlTextWriter(stringWrite);

        grd_search_result_dg.RenderControl(htmlWrite);

        Response.Write(stringWrite.ToString());

        Response.End();


        //// BindGridview(Convert.ToInt32(ddlSearchCriteria.SelectedValue), txt_serch_Criteria.Text);


        // Response.Clear();
        // Response.Buffer = true;
        // Response.ClearContent();
        // Response.ClearHeaders();
        // Response.Charset = "";
        // string FileName = "Product_Search" + DateTime.Now + ".xls";
        // StringWriter strwritter = new StringWriter();
        // HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        // Response.Cache.SetCacheability(HttpCacheability.NoCache);
        // Response.ContentType = "application/vnd.ms-excel";
        // Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        // grd_search_result_dg.GridLines = GridLines.Both;
        // grd_search_result_dg.HeaderStyle.Font.Bold = true;
        // grd_search_result_dg.RenderControl(htmltextwrtter);
        // Response.Write(strwritter.ToString());
        // Response.End();



    }


    public DataTable ProductCompany_Search(int ddlvalue, string SearchText, int Roleid, int Userid, int state, int district)
    {
        try
        {
            //Changes done to show branches products in their login only.
            MyDt = new DataTable();
            if (Roleid == 10 || Roleid == 1 || Roleid == 5)
            {
                MyAdp = new SqlDataAdapter("GP_sp_MIC_report_search_service", DBManager.ConnectToDB());
                MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar, 1000).Value = SearchText;
                MyAdp.SelectCommand.Parameters.Add("@_ipAddress", SqlDbType.VarChar, 500).Value = " ";
                MyAdp.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int).Value = state;
                MyAdp.SelectCommand.Parameters.Add("@Districtid", SqlDbType.Int).Value = district;
                MyAdp.SelectCommand.CommandTimeout = 600000;
            }

            //else if(Roleid == 5) // for HO
            //{
            //    MyAdp = new SqlDataAdapter("GP_sp_Branch_Report_Search_Service_Ho", DBManager.ConnectToDB());
            //    MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //    MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar, 1000).Value = SearchText;
            //    //MyAdp.SelectCommand.Parameters.Add("@ID", SqlDbType.Int, 500).Value = UserId;
            //    MyAdp.SelectCommand.CommandTimeout = 600000;

            //}
            else
            {
                MyAdp = new SqlDataAdapter("GP_sp_Branch_Report_Search_Service", DBManager.ConnectToDB());
                MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
                MyAdp.SelectCommand.Parameters.Add("@Searchtext", SqlDbType.VarChar, 1000).Value = SearchText;
                MyAdp.SelectCommand.Parameters.Add("@ID", SqlDbType.Int, 500).Value = UserId;

                MyAdp.SelectCommand.Parameters.Add("@StateId", SqlDbType.Int).Value = state;
                MyAdp.SelectCommand.Parameters.Add("@Districtid", SqlDbType.Int).Value = district;


                MyAdp.SelectCommand.CommandTimeout = 600000;
            }
            //MyAdp = new SqlDataAdapter("GPsp_CompanyProduct_Search", DBManager.ConnectToDB());
            //MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            //MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = ddlvalue;
            //MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 1000).Value = SearchText;
            //MyAdp.SelectCommand.Parameters.Add("@Roleid", SqlDbType.Int).Value = Roleid;
            //MyAdp.SelectCommand.Parameters.Add("@Userid", SqlDbType.Int).Value = Userid;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }



    /*
        public string bindcheckvalidity(int companyid)
        {
            try
            {

                objcompany = new clsCompany();
                DataSet ds_chekvality = new DataSet();
                ds_chekvality = objcompany.fetch_company_valitiy(companyid);
                if (ds_chekvality.Tables[0].Rows.Count > 0)
                {
                    date = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
                }
                else
                {
                    ds_chekvality.Dispose();
                }
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace);
            }

            return date;
        }*/

}
