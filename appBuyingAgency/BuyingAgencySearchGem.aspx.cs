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
using System.Drawing.Design;
using System.Drawing;
using System.Globalization;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using iTextSharp.text;
using System.ComponentModel.Design;
using System.IO;

public partial class appBuyingAgency_BuyingAgencySearchGem : System.Web.UI.Page
{
    int USERID, ROLEID, AGENCYID, BRANCHID, urlId;
    string USERFULLNAME, BRANCHNAME;

    clsMasterInspectionAgency objAgency;
    string SearchText = string.Empty;
    DataTable dt;
    clsCompany objclscompany;
    DataSet ds;
    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    string strMasterPathFresh;

    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            USERID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            ROLEID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            USERFULLNAME = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            strMasterPathFresh = System.Configuration.ConfigurationManager.AppSettings["MasterPathFresh"];
        }
        catch (Exception ex)
        {
            USERID = 0;
            ROLEID = 0;
            USERFULLNAME = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }


    protected void Btn_Search_Click(object sender, EventArgs e)
    {

        if (USERID == 12)
        {
            pnl1.Visible = true;
            rowCompany.Visible = true;
            rowInspection.Visible = false;

            int crit;
            crit = Convert.ToInt32(ddlSearchCriteria.SelectedValue);


            SearchText = txt_serch_Criteria.Text.Trim().ToString();
            if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
            {
                SearchText = "";
            }
            else
            {
                SearchText = txt_serch_Criteria.Text.Trim().ToString();
            }
            if (SearchText != "")
            {
                // lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + "" + ":" + "" + SearchText.ToString();
            }

            Bind_Buying_Agencies_DG(crit, SearchText);
            Bind_Factory(crit, SearchText);


        }
        else
        {
            //pnl1.Visible = true;
            //pnl_dg.Visible = false;
            //Bind_Buying_Agencies();

        }
    }

    private void Bind_Factory(int searchcriteria, string searchtext)
    {
        try
        {

            if (ROLEID == 1)
            {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();
                dt = FillFactoryGrid(searchcriteria, searchtext);
                if (dt.Rows.Count > 0)
                {

                    grdFactories.DataSource = dt;
                    grdFactories.DataBind();
                }
                else
                {
                    txt_serch_Criteria.Text = "";
                    dt.Dispose();

                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void Bind_Buying_Agencies_DG(int searchcriteria, string searchtext)
    {
        try
        {

            if (ROLEID == 1)
            {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();


                // dt = objAgency.DG_Fetch_Search(searchcriteria, searchtext);
                dt = DG_Fetch_Search_modified(searchcriteria, searchtext);
                if (dt.Rows.Count > 0)
                {

                    //grd_search_result.DataSource = dt;
                    //grd_search_result.DataBind();
                    //grd_search_result.PageIndex = 0;
                    // lbl_totalcount_buying.Text = "Total Companies  :" + dt.Rows.Count.ToString();
                    lblCompanyName.Text = dt.Rows[0][1].ToString();
                    lblAddress.Text = dt.Rows[0][2].ToString();
                    lblCategory.Text = dt.Rows[0][9].ToString();
                    lblEmail.Text = dt.Rows[0][4].ToString();
                    lblMobile.Text = dt.Rows[0][20].ToString();





                }
                else
                {
                    // Page.ClientScript.RegisterStartupScript(this.GetType(), " ","<script>alert('No Data Found !!!'); return false;</script>");
                    //  ClientScript.RegisterClientScriptBlock(GetType(), "sas", "<script> alert('No Data Found !!!');</script>", false);
                    txt_serch_Criteria.Text = "";
                    //  grd_search_result.Dispose();
                    dt.Dispose();
                    //  grd_search_result.DataBind();
                    // lbl_totalcount_buying.Text = "";


                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    public DataSet FetchCompanyPAN_By_CompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gpsp_FetchCompanyPAN_By_CompanyId", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;


            MyAdp.Fill(MyDs);
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
        return MyDs;
    }
    private void Bind_Buying_Agencies()
    {
        try
        {
            if (ROLEID == 1)
            {
                objAgency = new clsMasterInspectionAgency();
                dt = new DataTable();
                SearchText = txt_serch_Criteria.Text.Trim().ToString();

                if (SearchText.ToUpper().Trim() == "ENTER TEXT TO SEARCH")
                {
                    SearchText = "";
                }
                else
                {
                    SearchText = txt_serch_Criteria.Text.Trim().ToString();
                }

                if (SearchText != "")
                {
                    //  lbl_searchcompany.Text = ddlSearchCriteria.SelectedItem.Text + "" + ":" + "" + SearchText.ToString();
                }
                else
                {
                    //  lbl_searchcompany.Text = "";
                }
                //  System.Data.SqlClient.SqlDataReader dr = objAgency.BuyingAgency_Fetch_Search_test(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
                dt = objAgency.BuyingAgency_Fetch_Search(Convert.ToInt32(ddlSearchCriteria.SelectedItem.Value), SearchText);
                //dt.Load(dr);
                if (dt.Rows.Count > 0)
                {

                    //grd_search_result.DataSource = dt;
                    //grd_search_result.DataBind();

                }
                else
                {

                    txt_serch_Criteria.Text = "";

                    //grd_search_result.DataBind();
                    //grd_search_result.PageIndex = 0;
                    dt.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }
    protected void grd_search_onrowdatabound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");

                string str1 = hfpath.Value.ToString();
                //string str2 = hfname.Value.ToString();

                //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                //hlname.NavigateUrl = "../" + hfpath.Value.ToString() + hfname.Value.ToString();
                ImageButton lnk_view = (ImageButton)e.Row.FindControl("lnkview");

                // LinkButton lnk_view = (LinkButton)e.Row.FindControl("lnkview");
                HiddenField hdn_company_id = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview");
                HiddenField Hdn_Unit_id = (HiddenField)e.Row.FindControl("HiddenFieldUnitId");
                HiddenField Hdn_Branch_name = (HiddenField)e.Row.FindControl("HiddenFieldBranch");

                HiddenField HiddenFieldInspectionVideo = (HiddenField)e.Row.FindControl("HiddenFieldInspectionVideo");

                HyperLink hl_inspectionvideoD = (HyperLink)e.Row.FindControl("hl_inspectionvideoD");


                Label lbl_compnay_name = (Label)e.Row.FindControl("lbl_Company");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompanyID = objDecEnc.Encrypt(hdn_company_id.Value.ToString());
                string EncryptCompany_Name = objDecEnc.Encrypt(lbl_compnay_name.Text.ToString());
                string EncryptID = objDecEnc.Encrypt(Hdn_Unit_id.Value.ToString());
                string EncryptBranchName = objDecEnc.Encrypt(Hdn_Branch_name.Value.ToString());
                Label lbl_companystatus = (Label)e.Row.FindControl("lbl_company_status");


                //if (Convert.ToInt32(lbl_companystatus.Text) == 6)
                //{
                //    e.Row.BackColor = System.Drawing.Color.Red;
                //    e.Row.ForeColor = System.Drawing.Color.White;
                //    lnk_view.Text = "View" + "<br/>" + "<b>" + "BlackList" + "</b>";
                //}
                //else
                //{
                //    lnk_view.Text = "View";
                //}
                //objclscompany = new clsCompany();
                //Label lbl_validity = (Label)e.Row.FindControl("lbl_validity");
                //Label lbl_renewalcheck = (Label)e.Row.FindControl("lbl_renewal_date");
                //Label lblfreshprovisinalcheck = (Label)e.Row.FindControl("lbl_provisinol_fresh");
                //Label lbl_registration_date = (Label)e.Row.FindControl("lbl_registratondate");
                //Label lbl_checkvalidity_text = (Label)e.Row.FindControl("lbl_validity_text");
                //Label lbl_chevalidity_show = (Label)e.Row.FindControl("lbl_validity_text_show");

                DataSet ds_IR = new DataSet();
                ds_IR = DG_Fetch_InspectionReport_CUnitId(Convert.ToInt32(Hdn_Unit_id.Value));



                if (ds_IR.Tables[0].Rows.Count > 0)
                {

                    string IAFileName = "";
                    string IAFile_Path = "";
                    string IAVideo_File = "";
                    IAFileName = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_ReportName"].ToString();
                    IAFile_Path = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_ReportPath"].ToString();
                    //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                    IAVideo_File = ds_IR.Tables[0].Rows[0]["InspectionVideo"].ToString();

                    hlname.NavigateUrl = "../" + "Document/Upload/Registration/" + IAFileName.ToString();

                    hl_Report.Text = IAFileName.ToString();
                    hl_Report.NavigateUrl = "../" + "Document/Upload/Registration/" + IAFileName.ToString(); ;

                    //if (HiddenFieldInspectionVideo.Value == "No file Uploaded")
                    if (IAVideo_File.ToString() == "No file Uploaded")
                    {
                        hl_inspectionvideoD.NavigateUrl = "../" + "Document/Upload/Registration/" + "NFU.pdf".ToString();
                    }
                    else
                    {
                        hl_inspectionvideoD.Text = IAVideo_File;
                        hl_inspectionvideoD.NavigateUrl = IAVideo_File;
                        hl_Video.Text = IAVideo_File;
                        hl_Video.NavigateUrl = IAVideo_File;
                    }


                }
                //else
                //{
                //    ds_chekvality.Dispose();
                //}


                //string imageid = "../appAdministrator/ReportViewPage.aspx?Company_ID=" + EncryptCompanyID + "&Com_Name=" + EncryptCompany_Name + "&id=" + EncryptID + "&BranchName=" + EncryptBranchName;

                // lnk_view.Attributes.Add("onclick", "return FunOpenPopup('" + imageid + "');");


                string imageid = "../appAdministrator/CompanyCertificatePrintPreviewPageVerify.aspx?id=" + hdn_company_id.Value;
                // string imageid = "../appAdministrator/CompanyCertificatePrintPreviewPage.aspx?id=" + hdn_company_id.Value;
                lnk_view.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
                lnkCertificate.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");


            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void grdserch_pageindex_changing(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //grd_search_result.PageIndex = e.NewPageIndex;
            Bind_Buying_Agencies();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    public DataTable DG_Fetch_Search_modified(int Inputtext, string SearchText)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Agency_GeM_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = Inputtext;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 1000).Value = SearchText;
            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public DataTable FillFactoryGrid(int Inputtext, string SearchText)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_GeM_Factory_Search", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@DdlSearchCriteria", SqlDbType.Int).Value = Inputtext;
            MyAdp.SelectCommand.Parameters.Add("@SearchCriteria", SqlDbType.VarChar, 1000).Value = SearchText;

            MyAdp.Fill(MyDt);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return MyDt;
    }

    public DataSet DG_Fetch_InspectionReport_CUnitId(int CUnitId)
    {
        try
        {
            ds = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Agency_GeM_Fetch_IA_Report", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CunitId", SqlDbType.Int).Value = CUnitId;
            MyAdp.Fill(ds);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            MyAdp.Dispose();
            DBManager.CloseConnection();
        }
        return ds;
    }



    protected void grdFactories_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void btnView_Command(object sender, CommandEventArgs e)
    {
        try
        {
            rowInspection.Visible = true;
            string[] result = e.CommandArgument.ToString().Split(',');
            string companyID = result[0].ToString();
            string unitID = result[1].ToString();

            //string companyID = dt.Rows[0][5].ToString();
            //string unitID = dt.Rows[0][0].ToString();
            DataSet dSet = new DataSet();
            // HiddenField hdn_company_id=(HiddenField)e.Row.FindControl("HiddenFieldCompanyIdview");
            dSet = FetchCompanyPAN_By_CompanyId(Convert.ToInt32(companyID));
            string panFolder = dSet.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString();

            DataSet ds_IR = new DataSet();
            ds_IR = DG_Fetch_InspectionReport_CUnitId(Convert.ToInt32(unitID));

            if (ds_IR.Tables[0].Rows.Count > 0)
            {

                string IAFileName = "";
                string IAFile_Path = "";
                IAFileName = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_ReportName"].ToString();
                IAFile_Path = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_ReportPath"].ToString();
                hl_Report.Text = IAFileName.ToString();
                hl_Report.NavigateUrl = "../" + "Document/Upload/Registration/" + IAFileName.ToString(); ;

            }
            string imageid = "../appAdministrator/CompanyCertificatePrintPreviewPageVerify.aspx?id=" + companyID;
            lnkCertificate.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");

            string imgGeo1 = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_GeoTag_Image1"].ToString();
            if (string.IsNullOrEmpty(imgGeo1))
            {
                btnGeoTag1.NavigateUrl = "";
                btnGeoTag1.Text = "No Image";
                btnGeoTag1.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                btnGeoTag1.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + imgGeo1;
                btnGeoTag1.Text = "Image 1";
                btnGeoTag1.ForeColor = System.Drawing.Color.Blue;
            }


            string imgGeo2 = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_GeoTag_Image2"].ToString();
            if (string.IsNullOrEmpty(imgGeo2))
            {
                btnGeoTag2.NavigateUrl = "";
                btnGeoTag2.Text = "No Image";
                btnGeoTag2.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                btnGeoTag2.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + imgGeo2;
                btnGeoTag2.Text = "Image 2";
                btnGeoTag2.ForeColor = System.Drawing.Color.Blue;
            }

            string imgGeo3 = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_GeoTag_Image3"].ToString();
            if (string.IsNullOrEmpty(imgGeo3))
            {
                btnGeoTag3.NavigateUrl = "";
                btnGeoTag3.Text = "No Image";
                btnGeoTag3.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                btnGeoTag3.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + imgGeo3;
                btnGeoTag3.Text = "Image 3";
                btnGeoTag3.ForeColor = System.Drawing.Color.Blue;
            }

            string imgGeo4 = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_GeoTag_Image4"].ToString();

            if (string.IsNullOrEmpty(imgGeo4))
            {
                btnGeoTag4.NavigateUrl = "";
                btnGeoTag4.Text = "No Image";
                btnGeoTag4.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                btnGeoTag4.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + imgGeo4;
                btnGeoTag4.Text = "Image 4";
                btnGeoTag4.ForeColor = System.Drawing.Color.Blue;
            }

            string imgGeo5 = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_GeoTag_Image5"].ToString();
            if (string.IsNullOrEmpty(imgGeo5))
            {
                btnGeoTag5.NavigateUrl = "";
                btnGeoTag5.Text = "No Image";
                btnGeoTag5.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                btnGeoTag5.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + imgGeo5;
                btnGeoTag5.Text = "Image 5";
                btnGeoTag5.ForeColor = System.Drawing.Color.Blue;
            }

            string videoName = ds_IR.Tables[0].Rows[0]["fGPtbl_IS_Video"].ToString();
            if (string.IsNullOrEmpty(videoName))
            {
                hl_Video.NavigateUrl = "";
                hl_Video.Text = "No Video";
                hl_Video.ForeColor = System.Drawing.Color.MediumVioletRed;
            }
            else
            {
                hl_Video.NavigateUrl = "~\\geoimage1\\" + panFolder + "\\" + videoName;
                hl_Video.Text = videoName;
                hl_Video.ForeColor = System.Drawing.Color.Blue;
            }



        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}