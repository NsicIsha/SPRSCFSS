using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using NSICGP_VBClassLibrary;
using System.Data;
using System.Data.SqlClient;

public partial class appAdministrator_Online_UnitRenewalStep4 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int rows;

    clsRenewal objRenewal;
    clsAmendment objAmendment;
    clsCompany objCompany;
    DataSet Myds;

    DataSet AllstepchangeDs = new DataSet();
    DataRow[] AllstepchangeRow;

    int ErrorId;

    short tabindx = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        //List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        //if (gv_PerformanceSheet.Rows.Count == 1)
        //{
        //    if (mydir[0].OrderNumber == "" && mydir[0].Date_of_ReceiptOrder == "" && mydir[0].Store_Produt_Service_Desc == "" && mydir[0].Quantity == "" && mydir[0].Value == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Performance Statement');", true);
        //        return;
        //    }
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
        //    //return;
        //}
        //else
        //{
        //    RequiredFieldValidator rfdval = ((RequiredFieldValidator)gv_PerformanceSheet.FooterRow.FindControl("reqpisncodValue"));
        //    rfdval.Enabled = false;
        //}


        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            checkButtonDoubleClick(btnSubmit);
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            //UserId = 0;
            //RoleId = 0;
            //DepartmentId = 0;
            //UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        FunRequest();
        if (!Page.IsPostBack)
        {
            FunFillData();





            //if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")




        }
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            // UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            //UnitId = 0;
            Status = 0;
            BranchId = 0;
        }

      //  CompanyId = 56249;
    }

    private void FunFillData()
    {
        try
        {

            //////UNIT TABLE
            ////objCompany = new clsCompany();
            ////Myds = new DataSet();

            ////Myds = objCompany.Company_Fetch_ByUnit_Step4(CompanyId);
            ////if (Myds != null)
            ////{
            ////    if (Myds.Tables[0].Rows.Count > 0)
            ////    {
            ////        hdn_enterprise.Value = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
            ////        hdn_enterprise_size.Value = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
            ////    }
            ////}



            //UNIT TABLE
            objRenewal = new clsRenewal();
            Myds = new DataSet();

            Myds = objRenewal.CompanyRenewal_Fetch_Step4_ByCompanyId(CompanyId);

            if (Myds != null)
            {
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    
                    hdn_enterprise_size.Value = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
                }
            }
           


            rows = Myds.Tables[0].Rows.Count;

            gvFactoryDetails.DataSource = Myds;// objCompany.Company_Fetch_ByUnit_Step4(CompanyId);
            gvFactoryDetails.DataBind();

            gvCapitalDetails.DataSource = Myds; //objCompany.Company_Fetch_ByUnit_Step4(CompanyId);
            gvCapitalDetails.DataBind();

            gvTechnicalDetails.DataSource = Myds; //objCompany.Company_Fetch_ByUnit_Step4(CompanyId);
            gvTechnicalDetails.DataBind();

            FunTab();


            txt_8.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_8"].ToString();
            txt_9.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_9"].ToString();
            txt_10.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_10"].ToString();
            txt_11.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_11"].ToString();
            txt_12a.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_12A"].ToString();
            txt_12b.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_12B"].ToString();
            txt_13a.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_13A"].ToString();
            txt_13b.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_13B"].ToString();

            FunctionShowHide();

            cls_director objdir1 = new cls_director();
            List<PerformanceSheet> director1 = new List<PerformanceSheet>();
            director1 = objdir1.FetchPerformanceSheet(CompanyId);

            Bind_PerformanceSheet_gridview(director1);


        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnback_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt("2");
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        Response.Redirect("Online_UnitRenewalStep3Store.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objAmendment = new clsAmendment();

        float bs6a1 = 0;
        float bs6a2 = 0;
        float bs6a3 = 0;
        float bs6b = 0;
        float bs6b2 = 0;
        string bsTxt_5a1 = "";
        string bsTxt_5a2 = "";
        string bsTxt_5b1 = "";
        string bstxt_5b2 = "";
        string bstxt_5c = "";
        string bstxt_7a = "";
        string bstxt_7b = "";
        string bstxt_7c = "";
        string bstxt_7d = "";
        string bstxt_7e = "";
        string bstxt_8 = "";
        string bstxt_9 = "";
        string bstxt_10 = "";
        string bstxt_11 = "";
        string bstxt_12a = "";
        string bstxt_12b = "";
        string bstxt_12c = "";
        string bstxt_13a = "";
        string bstxt_13b = "";

        int rowcount = gvCapitalDetails.Rows.Count;

        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];


        //if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")

        if (gv_PerformanceSheet.Rows.Count == 1)
        {
            if (mydir[0].OrderNumber == "" && mydir[0].Date_of_ReceiptOrder == "" && mydir[0].Store_Produt_Service_Desc == "" && mydir[0].Quantity == "" && mydir[0].Value == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Performance Statement');", true);
                return;
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
            //return;
        }



        //ListtoXml_PerFormanceSheet();


        try
        {
            int i = 0;
            while (i < rowcount)
            {
                try
                {
                    // bsRd3d = Convert.ToInt32(rd3d.SelectedValue.ToString());

                    TextBox txt_6a1 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a1");
                    bs6a1 = float.Parse(txt_6a1.Text);
                    TextBox txt_6a2 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a2");
                    bs6a2 = float.Parse(txt_6a2.Text);

                    TextBox txt_6a3 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a3");
                    bs6a3 = float.Parse(txt_6a2.Text);

                    TextBox txt_6b = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6b");
                    bs6b = float.Parse(txt_6b.Text);

                    TextBox txt_6b2 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6b2");
                    bs6b2 = float.Parse(txt_6b2.Text);
                }
                catch { }

                int j = 0;
                float sum = 0;
                while (j < rowcount)
                {
                    TextBox txt_enterprise = (TextBox)gvCapitalDetails.Rows[j].FindControl("txt_6a2");
                    sum = sum + float.Parse(txt_enterprise.Text);
                    j = j + 1;
                }



                //if (hdn_enterprise_size.Value == "3")
                //{
                //    if (sum > 100 || sum == 0)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Micro :P & M cannot be 0 or Investment in Plant and Machinery or Equipment can not be more than Rs.1 crore ');", true);
                //        return;

                //    }
                //}

                //if (hdn_enterprise_size.Value == "4")
                //{
                //    if (sum > 1000 || sum < 100)
                //    {
                //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Small :P & M cannot be 0 or Investment in Plant and Machinery or Equipment can not be more than Rs.10 crore');", true);
                //        return;
                //    }
                //    //else
                //    //{
                //    //    ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: More than 25 lakh rupee but not exceed 5 core');", true);
                //    //    return;
                //    //}
                //}







                HiddenField hdnunit = (HiddenField)gvFactoryDetails.Rows[i].FindControl("hdnunit");
                UnitId = Convert.ToInt32(hdnunit.Value);
                TextBox txt_5a1 = (TextBox)gvFactoryDetails.Rows[i].FindControl("txt_5a1");
                bsTxt_5a1 = txt_5a1.Text.ToString().Trim();
                TextBox txt_5a2 = (TextBox)gvFactoryDetails.Rows[i].FindControl("txt_5a2");
                bsTxt_5a2 = txt_5a2.Text.ToString().Trim();
                TextBox txt_5b1 = (TextBox)gvFactoryDetails.Rows[i].FindControl("txt_5b1");
                bsTxt_5b1 = txt_5b1.Text.ToString().Trim();
                TextBox txt_5b2 = (TextBox)gvFactoryDetails.Rows[i].FindControl("txt_5b2");
                bstxt_5b2 = txt_5b2.Text.ToString().Trim();
                TextBox txt_5c = (TextBox)gvFactoryDetails.Rows[i].FindControl("txt_5c");
                bstxt_5c = txt_5c.Text.ToString().Trim();

                TextBox txt_7a = (TextBox)gvTechnicalDetails.Rows[i].FindControl("txt_7a");
                bstxt_7a = txt_7a.Text.ToString().Trim();
                TextBox txt_7b = (TextBox)gvTechnicalDetails.Rows[i].FindControl("txt_7b");
                bstxt_7b = txt_7b.Text.ToString().Trim();
                TextBox txt_7c = (TextBox)gvTechnicalDetails.Rows[i].FindControl("txt_7c");
                bstxt_7c = txt_7c.Text.ToString().Trim();
                TextBox txt_7d = (TextBox)gvTechnicalDetails.Rows[i].FindControl("txt_7d");
                bstxt_7d = txt_7d.Text.ToString().Trim();
                TextBox txt_7e = (TextBox)gvTechnicalDetails.Rows[i].FindControl("txt_7e");
                bstxt_7e = txt_7e.Text.ToString().Trim();
                bstxt_8 = txt_8.Text.ToString().Trim();
                bstxt_9 = txt_9.Text.ToString().Trim();
                bstxt_10 = txt_10.Text.ToString().Trim();
                bstxt_11 = txt_11.Text.ToString().Trim();
                bstxt_12a = txt_12a.Text.ToString().Trim();
                bstxt_12b = txt_12b.Text.ToString().Trim();
                bstxt_12c = txt_12c.Text.ToString().Trim();
                bstxt_13a = txt_13a.Text.ToString().Trim();
                bstxt_13b = txt_13b.Text.ToString().Trim();


                ErrorId = objAmendment.CompanyAmendment_Insert_Step4(UnitId, CompanyId, bsTxt_5a1,
                            bsTxt_5a2, bsTxt_5b1, bstxt_5b2, bstxt_5c, bs6a1, bs6a2, bs6a3,
                            bs6b, bs6b2, bstxt_7a, bstxt_7b, bstxt_7c, bstxt_7d,
                            bstxt_7e, bstxt_8, bstxt_9, bstxt_10, bstxt_11,
                            bstxt_12a, bstxt_12b, bstxt_12c, bstxt_13a, bstxt_13b, UserId, "renewal");

                i++;
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        //string  fGPtbl_CUnit_5A1, string fGPtbl_CUnit_5A2, string fGPtbl_CUnit_5BB1, string fGPtbl_CUnit_5BB2, string fGPtbl_CUnit_5C, float fGPtbl_CUnit_6A1, float fGPtbl_CUnit_6A2, float fGPtbl_CUnit_6B, string fGPtbl_CUnit_7A, string fGPtbl_CUnit_7B, string fGPtbl_CUnit_7C, string fGPtbl_CUnit_7D, string fGPtbl_CUnit_7E, string fGPtbl_CUnit_8, string fGPtbl_CUnit_9, string fGPtbl_CUnit_10, string fGPtbl_CUnit_11,string fGPtbl_CUnit_12A, string fGPtbl_CUnit_12B, string fGPtbl_CUnit_13A, string fGPtbl_CUnit_13B, int UserId


        ListtoXml_PerFormanceSheet();

        try
        {
            if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
            {
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
                string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                //Server.Transfer("NewUnitRegistrationStep2.aspx?c=" + CompanyId.ToString() + "&u=" + UnitId.ToString(), true);
                string EncryptFormtype = objDecEnc.Encrypt("2");
                string EncryptUploadType = objDecEnc.Encrypt("1");

                string EncryptRenewalType = objDecEnc.Encrypt("R");


                // +"&ut=" + EncryptUploadType

                Response.Redirect("Online_NewUnitRegistrationStep5.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&ra=" + EncryptRenewalType, false);
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void gvCapitalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txttot = (TextBox)e.Row.FindControl("txt_tot");
                txttot.Attributes.Add("onblur", "return AddText(" + rows + ");");

                TextBox txt6a1 = (TextBox)e.Row.FindControl("txt_6a1");
                TextBox txt6a2 = (TextBox)e.Row.FindControl("txt_6a2");
                TextBox txt6b = (TextBox)e.Row.FindControl("txt_6b");

                if (txt6a1.Text != null && txt6a2.Text != null && txt6b.Text != null)
                {
                    txttot.Text = Convert.ToString(Convert.ToDecimal(txt6a1.Text) + Convert.ToDecimal(txt6a2.Text) + Convert.ToDecimal(txt6b.Text));
                    //Math.Round(2,2)
                }

                txt6a1.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt6a2.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt6b.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txttot.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);


                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_6A1' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    // txt_6a1.ReadOnly = false;
                    txt6a1.ReadOnly = false;
                }
                else
                {
                    //txt_6a1.ReadOnly = true;
                    txt6a1.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_6A2' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt6a2.ReadOnly = false;
                }
                else
                {
                    txt6a2.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_6B' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt6b.ReadOnly = false;
                }
                else
                {
                    txt6b.ReadOnly = true;
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void gvFactoryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtadd = (TextBox)e.Row.FindControl("txt_5a1");
                TextBox txttel = (TextBox)e.Row.FindControl("txt_5a2");
                TextBox txthpa = (TextBox)e.Row.FindControl("txt_5b1");
                TextBox txthpi = (TextBox)e.Row.FindControl("txt_5b2");
                TextBox txtdesc = (TextBox)e.Row.FindControl("txt_5c");

                txtadd.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txttel.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txthpa.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txthpi.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txtdesc.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);

                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_5A1 ' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txtadd.ReadOnly = false;
                }
                else
                {
                    txtadd.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_5A2' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txttel.ReadOnly = false;
                }
                else
                {
                    txttel.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_5BB1' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txthpa.ReadOnly = false;
                }
                else
                {
                    txthpa.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_5BB2' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txthpi.ReadOnly = false;
                }
                else
                {
                    txthpi.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_5C' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txtdesc.ReadOnly = false;
                }
                else
                {
                    txtdesc.ReadOnly = true;
                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void gvTechnicalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txt7a = (TextBox)e.Row.FindControl("txt_7a");
                TextBox txt7b = (TextBox)e.Row.FindControl("txt_7b");
                TextBox txt7c = (TextBox)e.Row.FindControl("txt_7c");
                TextBox txt7d = (TextBox)e.Row.FindControl("txt_7d");
                TextBox txt7e = (TextBox)e.Row.FindControl("txt_7e");

                txt7a.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt7b.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt7c.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt7d.TabIndex = (short)tabindx;
                tabindx = (short)(tabindx + 1);
                txt7e.TabIndex = (short)tabindx;
                //tabindx = (short)(tabindx + 1);

                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_7A' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt7a.ReadOnly = false;
                }
                else
                {
                    txt7a.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_7B ' and colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt7b.ReadOnly = false;
                }
                else
                {
                    txt7b.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_7C' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt7c.ReadOnly = false;
                }
                else
                {
                    txt7c.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_7D' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt7d.ReadOnly = false;
                }
                else
                {
                    txt7d.ReadOnly = true;
                }
                AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_7E' and Colval = 1 ");
                if (AllstepchangeRow.Length > 0 && RoleId == 8)
                {
                    txt7e.ReadOnly = false;
                }
                else
                {
                    txt7e.ReadOnly = true;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void FunTab()
    {
        try
        {
            txt_8.TabIndex = (short)(tabindx + 1);
            txt_9.TabIndex = (short)(tabindx + 2);
            txt_10.TabIndex = (short)(tabindx + 3);
            txt_11.TabIndex = (short)(tabindx + 4);
            txt_12a.TabIndex = (short)(tabindx + 5);
            txt_12b.TabIndex = (short)(tabindx + 6);
            txt_13a.TabIndex = (short)(tabindx + 7);
            txt_13b.TabIndex = (short)(tabindx + 8);
            btnSubmit.TabIndex = (short)(tabindx + 9);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    private void FunctionShowHide()
    {
        try
        {
            AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_8' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_8.ReadOnly = false;
            }
            else
            {
                txt_8.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_9' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_9.ReadOnly = false;
            }
            else
            {
                txt_9.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_10' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_10.ReadOnly = false;
            }
            else
            {
                txt_10.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_11' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_11.ReadOnly = false;
            }
            else
            {
                txt_11.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_12A' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_12a.ReadOnly = false;
            }
            else
            {
                txt_12a.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_12B' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_12b.ReadOnly = false;
            }
            else
            {
                txt_12b.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_13A' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_13a.ReadOnly = false;
            }
            else
            {
                txt_13a.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_13B' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_13b.ReadOnly = false;
            }
            else
            {
                txt_13b.ReadOnly = true;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Save & Continue >>';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }
    protected void AddnewPerformanceSheet(object sender, EventArgs e)
    {
        string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


        ordernumber = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_OrderNumber")).Text;
        Date_of_ReceiptOrder = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Date_of_ReceiptOrder")).Text;
        Store_Produt_Service_Desc = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Store_Produt_Service_Desc")).Text;
        Quantity = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Quantity")).Text;
        Value = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Value")).Text;
        Name_Consignee = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_Name_Consignee")).Text;
        DeliveryDueDate = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_DeliveryDueDate")).Text;
        MaterialDeliveryDate = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_MaterialDeliveryDate")).Text;
        DespatchingParticulars = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_DespatchingParticulars")).Text;
        Remarks = ((TextBox)gv_PerformanceSheet.FooterRow.FindControl("txt_foot_Remarks")).Text.Trim();


        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

        //Added by Dhiraj 28 March 19 to avoid blank entry
        //if (gv_PerformanceSheet.Rows.Count == 1)
        //{
        if (ordernumber.Trim() == "" || Date_of_ReceiptOrder.Trim() == "" || Store_Produt_Service_Desc.Trim() == "" || Quantity.Trim() == "" || Value.Trim() == "")
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Performance Statement with order number, receipt date, product description , quantity & value');", true);
            return;
        }
        //ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Please add atleast one Director Details');", true);
        //return;
        //}

        //Added by Dhiraj 28 March 19 to avoid blank entry


        if (mydir == null)
        {
            List<PerformanceSheet> mydir1 = new List<PerformanceSheet>();
            PerformanceSheet dir = new PerformanceSheet();
            dir.PrimaryId = 1;
            dir.OrderNumber = ordernumber;
            dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
            dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
            dir.Quantity = Quantity;
            dir.Value = Value;
            dir.Name_Consignee = Name_Consignee;
            dir.DeliveryDueDate = DeliveryDueDate;
            dir.MaterialDeliveryDate = MaterialDeliveryDate;
            dir.DespatchingParticulars = DespatchingParticulars;
            dir.Remarks = Remarks;

            mydir1.Add(dir);
            Bind_PerformanceSheet_gridview(mydir1);


        }
        else
        {
            PerformanceSheet dir = new PerformanceSheet();
            dir.PrimaryId = mydir.Count + 1;
            dir.OrderNumber = ordernumber;
            dir.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
            dir.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
            dir.Quantity = Quantity;
            dir.Value = Value;
            dir.Name_Consignee = Name_Consignee;
            dir.DeliveryDueDate = DeliveryDueDate;
            dir.MaterialDeliveryDate = MaterialDeliveryDate;
            dir.DespatchingParticulars = DespatchingParticulars;
            dir.Remarks = Remarks;
            mydir.Add(dir);
            Bind_PerformanceSheet_gridview(mydir);

            //RequiredFieldValidator rfdval = ((RequiredFieldValidator)gv_PerformanceSheet.FooterRow.FindControl("reqpisncodValue"));
            //rfdval.Enabled = false;
        }

    }

    private void Bind_PerformanceSheet_gridview(List<PerformanceSheet> PerformanceSheet)
    {

        gv_PerformanceSheet.DataSource = PerformanceSheet;
        gv_PerformanceSheet.DataBind();
        ViewState["PerformanceSheet"] = PerformanceSheet;

        RequiredFieldValidator rfdval = ((RequiredFieldValidator)gv_PerformanceSheet.FooterRow.FindControl("reqpisncodValue"));
        rfdval.Enabled = false;


        if (PerformanceSheet[0].OrderNumber == "" && PerformanceSheet[0].Date_of_ReceiptOrder == "" && PerformanceSheet[0].Store_Produt_Service_Desc == "" && PerformanceSheet[0].Quantity == "" && PerformanceSheet[0].Value == "")
        {
            gv_PerformanceSheet.Rows[0].Visible = false;
        }
    }

    protected void gv_PerformanceSheet_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gv_PerformanceSheet.EditIndex = e.NewEditIndex;
        List<PerformanceSheet> myPS = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        gv_PerformanceSheet.DataSource = myPS;
        gv_PerformanceSheet.DataBind();


    }
    protected void gv_PerformanceSheet_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int userid = Convert.ToInt32(gv_PerformanceSheet.DataKeys[e.RowIndex].Value.ToString());

        // Response.Write(userid);
        // Fin Gridview Record 
        string ordernumber, Date_of_ReceiptOrder, Store_Produt_Service_Desc, Quantity, Value, Name_Consignee, DeliveryDueDate, MaterialDeliveryDate, DespatchingParticulars, Remarks;


        ordernumber = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_OrderNumber")).Text.Trim();
        Date_of_ReceiptOrder = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Date_of_ReceiptOrder")).Text.Trim();
        Store_Produt_Service_Desc = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Store_Produt_Service_Desc")).Text.Trim();
        Quantity = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Quantity")).Text.Trim();
        Value = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Value")).Text.Trim();
        Name_Consignee = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Name_Consignee")).Text.Trim();
        DeliveryDueDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DeliveryDueDate")).Text.Trim();
        MaterialDeliveryDate = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_MaterialDeliveryDate")).Text.Trim();
        DespatchingParticulars = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_DespatchingParticulars")).Text.Trim();
        Remarks = ((TextBox)gv_PerformanceSheet.Rows[e.RowIndex].FindControl("txt_Remarks")).Text.Trim();



        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];

        if (mydir != null)
        {
            var item = mydir.Where(d => d.PrimaryId == userid).FirstOrDefault();
            if (item != null)
            {
                item.OrderNumber = ordernumber;
                item.Date_of_ReceiptOrder = Date_of_ReceiptOrder;
                item.Store_Produt_Service_Desc = Store_Produt_Service_Desc;
                item.Quantity = Quantity;
                item.Value = Value;
                item.Name_Consignee = Name_Consignee;
                item.DeliveryDueDate = DeliveryDueDate;
                item.MaterialDeliveryDate = MaterialDeliveryDate;
                item.DespatchingParticulars = DespatchingParticulars;
                item.Remarks = Remarks;
            }
        }
        gv_PerformanceSheet.EditIndex = -1;
        Bind_PerformanceSheet_gridview(mydir);

    }
    private void ListtoXml_PerFormanceSheet()
    {
        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        if (mydir != null)
        {
            var xDir = new XElement("PerformanceSheets", from dir in mydir
                                                         select new XElement("PerformanceSheet", new XAttribute("ID", dir.PrimaryId),
                                                             new XElement("OrderNumber", dir.OrderNumber),
                                                             new XElement("Date_of_ReceiptOrder", dir.Date_of_ReceiptOrder),
                                                             new XElement("Store_Produt_Service_Desc", dir.Store_Produt_Service_Desc),
                                                             new XElement("Quantity", dir.Quantity),
                                                             new XElement("Value", dir.Value),
                                                             new XElement("Name_Consignee", dir.Name_Consignee),
                                                             new XElement("DeliveryDueDate", dir.DeliveryDueDate),
                                                             new XElement("MaterialDeliveryDate", dir.MaterialDeliveryDate),
                     new XElement("DespatchingParticulars", dir.DespatchingParticulars),
                     new XElement("Remarks", dir.Remarks)));
            objCompany = new clsCompany();
            objCompany.Company_PerformanceSheet_insert(xDir.ToString(), CompanyId);
        }

    }

    public int Company_PerformanceSheet_insert(string xmldata, int companyid)
    {
        SqlConnection con = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        try
        {
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            con = new SqlConnection(connectionstring);
            con.Open();
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_RA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            ErrorId = cmd.ExecuteNonQuery();
            // ErrorId = Convert.ToInt32(outError.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
        }

        return ErrorId;
    }
    protected void gv_PerformanceSheet_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int userid = Convert.ToInt32(gv_PerformanceSheet.DataKeys[e.RowIndex].Value.ToString());
        List<PerformanceSheet> mydir = (List<PerformanceSheet>)ViewState["PerformanceSheet"];
        PerformanceSheet dir = mydir.Where(s => s.rowno == userid).Single();
        if (mydir != null)
        {
            mydir.Remove(dir);
        }
        Bind_PerformanceSheet_gridview(mydir);
    }
}