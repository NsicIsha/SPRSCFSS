using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
 
using System.Data.SqlClient;


 
using System.Xml.Linq;
 


public partial class appAdministrator_NewUnitRegistrationStep4Online : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int rows;
    clsCompany objCompany;
    DataSet Myds;
    int ErrorId;
    short tabindx = 1;
    int ConstitutionId = 0;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
 


    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        //try
        //{
        //    UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        //    RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
        //    DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
        //    UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        //}
        //catch (Exception ex)
        //{
        //    UserId = 0;
        //    RoleId = 0;
        //    DepartmentId = 0;
        //    UserFullName = "";
        //}
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        FunRequest();
        if (!Page.IsPostBack)
        {
            FunFillData();

        }

    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            Edit = Convert.ToInt32(Request.QueryString["Edit"].Replace(" ", "+"));
           // string ss= Request.QueryString["con"].Replace(" ", "+").ToString();
            ConstitutionId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["con"].Replace(" ", "+")).ToString());
           

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Error in FunRequest() " + ex.StackTrace);
            CompanyId = 0;
            UnitId = 0;
            Status = 0;
            BranchId = 0;
            Edit = 0;
        }
    }


    private void FunFillData()
    {
        try
        {

            if (Status == 5 || Status == 123 || Edit == 1)    //INCOMPLETE
            {
                //UNIT TABLE
                objCompany = new clsCompany();
                Myds = new DataSet();

                Myds = objCompany.Company_Fetch_ByUnit_Step4(CompanyId);
                if (Myds != null)
                {
                    if (Myds.Tables[0].Rows.Count > 0)
                    {
                        hdn_enterprise.Value = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
                        hdn_enterprise_size.Value = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString();
                    }
                }
                ArrayList strarr = new ArrayList(5);

                rows = Myds.Tables[0].Rows.Count;

                gvFactoryDetails.DataSource = Myds;
                gvFactoryDetails.DataBind();

                gvCapitalDetails.DataSource = Myds;
                gvCapitalDetails.DataBind();

                gvTechnicalDetails.DataSource = Myds;
                gvTechnicalDetails.DataBind();

                FunTab();

                txt_8.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_8"].ToString();
                txt_9.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_9"].ToString();
                txt_10.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_10"].ToString();
                txt_11.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_11"].ToString();
                txt_12a.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_12A"].ToString();
                txt_12b.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_12B"].ToString();
             //   txt_12c.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_12C"].ToString();
                txt_13a.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_13A"].ToString();
                txt_13b.Text = Myds.Tables[0].Rows[0]["fGPtbl_CUnit_13B"].ToString();

                //string strCertificate = "";
                //strCertificate= Myds.Tables[0].Rows[0]["Certificate_Proprietor_Partner_Directors"].ToString();

                //if (rdbCertificateProprietor.Items.FindByValue(strCertificate.ToString()) != null)
                //{
                //    rdbCertificateProprietor.SelectedValue = strCertificate.ToString();
                //}

                cls_director objdir1 = new cls_director();
                List<PerformanceSheet> director1 = new List<PerformanceSheet>();
                director1 = FetchPerformanceSheet(CompanyId);// objdir1.FetchPerformanceSheet(CompanyId);

                Bind_PerformanceSheet_gridview(director1);




            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    public DataTable Company_FetchPerformanceSheetList(int companyid)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Company_FetchPerformanceSheetByComapny_Step3_Fresh_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;

            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Directors_CompanyId", SqlDbType.Int).Value = companyid;
            // MyDr = MySqlComm.ExecuteReader();
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
        }
        return MyDs.Tables[0];
    }
    public List<PerformanceSheet> FetchPerformanceSheet(int CompanyId)
    {
        MyDt = new DataTable();
        clsCompany objcompany = new clsCompany();
        MyDt = Company_FetchPerformanceSheetList(CompanyId);// objcompany.Company_FetchPerformanceSheetList(CompanyId);
        List<PerformanceSheet> DList = new List<PerformanceSheet>();
        DList.Clear();
        if (MyDt != null)
        {

            if (MyDt.Rows.Count > 0)
            {
                foreach (DataRow drow in MyDt.Rows)
                {
                    PerformanceSheet drct = new PerformanceSheet();
                    drct.PrimaryId = Convert.ToInt32(drow["PrimaryId"].ToString());
                    drct.CompanyId = CompanyId.ToString(); ;
                    drct.OrderNumber = drow["OrderNumber_Date_Authority"].ToString();
                    drct.Date_of_ReceiptOrder = drow["DateofReceipt"].ToString();
                    drct.Store_Produt_Service_Desc = drow["Description_Stores_Product_Services"].ToString();
                    //   drct.Pincode = drow["fGPtbl_Company_Directors_Zip"].ToString();
                    drct.Quantity = drow["Quantity"].ToString();
                    drct.Value = drow["Value"].ToString();
                    drct.Name_Consignee = drow["ConsigneeName"].ToString();
                    drct.DeliveryDueDate = drow["DeliveryDueDate"].ToString();
                    drct.MaterialDeliveryDate = drow["MaterialDeliveryDate"].ToString();
                    drct.DespatchingParticulars = drow["DespatchingParticulars"].ToString();
                    drct.Remarks = drow["Remarks"].ToString();

                    DList.Add(drct);

                }
            }
            else
            {
                PerformanceSheet drct = new PerformanceSheet();
                drct.rowno = 1;
                drct.CompanyId = CompanyId.ToString();
                drct.OrderNumber = "";
                drct.Date_of_ReceiptOrder = "";
                drct.Store_Produt_Service_Desc = "";
                drct.Quantity = "";
                drct.Value = "";
                drct.Name_Consignee = "";
                drct.DeliveryDueDate = "";
                drct.MaterialDeliveryDate = "";
                drct.DespatchingParticulars = "";
                drct.Remarks = "";

                DList.Add(drct);
            }
        }
        return DList;
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {


        objCompany = new clsCompany();

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




        float sum = 0;

        int j = 0;

        while (j < rowcount)
        {
            TextBox txt_enterprise = (TextBox)gvCapitalDetails.Rows[j].FindControl("txt_6a2");
            sum = sum + float.Parse(txt_enterprise.Text);
            j = j + 1;
        }

       // sum = sum * 100; 
       
        //if (hdn_enterprise_size.Value == "3")
        //{
        //    if (sum > 100 || sum==0)
        //    {
        //        ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('For Micro :P & M cannot be 0 or Investment in Plant and Machinery or Equipment can not be more than Rs.1 crore ');", true);
        //        return;

        //    }
        //}

        //if (hdn_enterprise_size.Value == "4")
        //{
        //    if (sum > 1000 || sum < 100 )
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


       
        


        //  Check Manufacturing Sector Sum   
        /*
        if ((hdn_enterprise.Value == "1" || hdn_enterprise.Value == "3") && hdn_enterprise_size.Value == "3")
        {
            if (sum > 25)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: Only 25 lacs are allowed');", true);
                return;

            }
        }

        if ((hdn_enterprise.Value == "1" || hdn_enterprise.Value == "3") && hdn_enterprise_size.Value == "4")
        {
            if (sum >= 25 && sum <= 500)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: More than 25 lakh rupee but not exceed 5 core');", true);
                return;
            }
        }


        if ((hdn_enterprise.Value == "2") && hdn_enterprise_size.Value == "3")
        {
            if (sum > 10)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: Does not exceed 10 lakh');", true);
                return;
            }
        }
        if ((hdn_enterprise.Value == "2") && hdn_enterprise_size.Value == "4")
        {
            if (sum > 10 && sum <= 200)
            {

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Plant and Machinery: More than 10  lakh rupee but not exceed 2 core');", true);
                return;
            }
        }

        */
        try
        {
            int i = 0;
            while (i < rowcount)
            {
                try
                {
                    TextBox txt_6a1 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a1");
                    bs6a1 = float.Parse(txt_6a1.Text);
                    TextBox txt_6a2 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a2");
                    bs6a2 = float.Parse(txt_6a2.Text);

                    TextBox txt_6a3 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6a3");
                    bs6a3 = float.Parse(txt_6a3.Text);

                    TextBox txt_6b = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6b");
                    bs6b = float.Parse(txt_6b.Text);

                    TextBox txt_6b2 = (TextBox)gvCapitalDetails.Rows[i].FindControl("txt_6b2");
                    bs6b2 = float.Parse(txt_6b2.Text);

                }
                catch { }
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
                bstxt_12c ="N.A.";// txt_12c.Text.ToString().Trim();
                bstxt_13a = txt_13a.Text.ToString().Trim();
                bstxt_13b = txt_13b.Text.ToString().Trim();






                //foreach (GridViewRow grow in gvFactoryDetails.Rows)
                //{
                //    TextBox txtsum = (TextBox)grow.FindControl("txt_6a2");

                ////    sum += sum + Convert.ToInt32(txtsum.Text);
                //}












                ErrorId = objCompany.Company_Insert_Step4(UnitId, CompanyId, bsTxt_5a1,
                            bsTxt_5a2, bsTxt_5b1, bstxt_5b2, bstxt_5c, bs6a1, bs6a2, bs6a3,
                            bs6b, bs6b, bstxt_7a, bstxt_7b, bstxt_7c, bstxt_7d,
                            bstxt_7e, bstxt_8, bstxt_9, bstxt_10, bstxt_11,
                            bstxt_12a, bstxt_12b, bstxt_12c, bstxt_13a, bstxt_13b, UserId);

                i++;
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        int Result = 0;
        string Certificate_Status = "N.A.";
        //if(rdbCertificateProprietor.SelectedValue=="Yes")
        //{
        //    Certificate_Status = "Yes";
        //}
        //else
        //{
        //    Certificate_Status = "No";
        //}

        Result = Company_Insert_Step4_Certificate_Proprietor_Partner_Directors_Status(UnitId, CompanyId, Certificate_Status);

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
                string EncryptFormtype = objDecEnc.Encrypt("1");
                string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();
                string EncryConstitutionId = objDecEnc.Encrypt(ConstitutionId.ToString()); //+ "&con=" + EncryConstitutionId + //"&con=" + EncryConstitutionId +

                if (Edit == 1) //TO SKIP STEP 5 IF EDITED BY BRANCH
                {
                    Response.Redirect("AdminEditPayment.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=1", false);
                }
                else
                {
                    Response.Redirect("NewUnitRegistrationStep5Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&con=" + EncryConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    public int Company_Insert_Step4_Certificate_Proprietor_Partner_Directors_Status(int fGPtbl_CUnit_Id, int fGPtbl_CUnit_CompanyId, string Certificate_Proprietor_Partner_Directors)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Company_Insert_Step4_RA_Certificate_Proprietor_Partner_Directors_Status", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@fGPtbl_CUnit_Id", SqlDbType.Int).Value = fGPtbl_CUnit_Id;
            MySqlComm.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = fGPtbl_CUnit_CompanyId;

            MySqlComm.Parameters.Add("@Certificate_Proprietor_Partner_Directors", SqlDbType.VarChar, 5000).Value = Certificate_Proprietor_Partner_Directors;
             

            SqlParameter outError = new SqlParameter("@ErrorId", SqlDbType.Int);
            outError.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outError);

            MySqlComm.ExecuteNonQuery();
            ErrorId = Convert.ToInt32(outError.Value);
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

        return ErrorId;
    }


    protected void gvCapitalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txttot = (TextBox)e.Row.FindControl("txt_tot");
            txttot.Attributes.Add("onblur", "return AddText(" + rows + ");");

            TextBox txt6a1 = (TextBox)e.Row.FindControl("txt_6a1");
            TextBox txt6a2 = (TextBox)e.Row.FindControl("txt_6a2");
            TextBox txt6a3 = (TextBox)e.Row.FindControl("txt_6a3");
            TextBox txt6b = (TextBox)e.Row.FindControl("txt_6b");
            TextBox txt6b2 = (TextBox)e.Row.FindControl("txt_6b2");

            if (txt6a1.Text != null && txt6a2.Text != null && txt6a3.Text != null && txt6b.Text != null && txt6b2.Text != null)
            {
                txttot.Text = Convert.ToString(Convert.ToDecimal(txt6a1.Text) + Convert.ToDecimal(txt6a2.Text) + Convert.ToDecimal(txt6a3.Text)
                    + Convert.ToDecimal(txt6b.Text) + Convert.ToDecimal(txt6b2.Text));
                //Math.Round(2,2)
            }

            txt6a1.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);
            txt6a2.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);
            txt6a3.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);
            txt6b.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);
            txt6b2.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);
            txttot.TabIndex = (short)tabindx;
            tabindx = (short)(tabindx + 1);

        }
    }

    protected void gvFactoryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }

    }

    protected void gvTechnicalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
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
        }
    }

    private void FunTab()
    {

        txt_8.TabIndex = (short)(tabindx + 1);
        txt_9.TabIndex = (short)(tabindx + 2);
        txt_10.TabIndex = (short)(tabindx + 3);
        txt_11.TabIndex = (short)(tabindx + 4);
        txt_12a.TabIndex = (short)(tabindx + 5);
        txt_12b.TabIndex = (short)(tabindx + 6);
       // txt_12c.TabIndex = (short)(tabindx + 7);
        txt_13a.TabIndex = (short)(tabindx + 7);
        txt_13b.TabIndex = (short)(tabindx + 8);
        btnSubmit.TabIndex = (short)(tabindx + 9);

    }

    protected void btn_back_click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        string EncryptFormtype = objDecEnc.Encrypt("1");

        string EncryptConstitutionId = objDecEnc.Encrypt(ConstitutionId.ToString());

        string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();
        if (Edit == 1)
        {
            Response.Redirect("NewUnitRegistrationStep3Online.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=1", false);
        }
        else
        {
            Response.Redirect("NewUnitRegistrationStep3StoreOnline.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&con=" + EncryptConstitutionId + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
        }

    }


    //private bool gvCheckValidation()
    //{
    //    bool chkmanufacture;
    //    chkmanufacture = false;
 
    //}

    protected void gvCapitalDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

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
           // objCompany.Company_PerformanceSheet_insert(xDir.ToString(), CompanyId);

        Company_PerformanceSheet_insert(xDir.ToString(), CompanyId);
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
            cmd = new SqlCommand("GP_sp_Insert_PerformanceSheetdata_RA_FreshRegistration", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Xmldata", xmldata);
            cmd.Parameters.AddWithValue("@CompanyId", companyid);
            ErrorId = cmd.ExecuteNonQuery();
            ErrorId = 1;
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
    /*
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
    */
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