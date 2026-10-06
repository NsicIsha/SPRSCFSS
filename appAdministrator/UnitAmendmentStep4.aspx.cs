using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;


public partial class appAdministrator_UnitAmendmentStep4 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;
    int rows;

    clsAmendment objAmendment;
    DataSet Myds;

    int ErrorId;
    short tabindx = 1;

    public void FunRequest_Load()
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
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            FunRequest_Load();

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
      checkButtonDoubleClick(btnSubmit);
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
           // UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());

            // Check changes of change unit id

        }
        catch
        {
            CompanyId = 0;
            //UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }


    private void FunFillData()
    {
        try
        {
            objAmendment = new clsAmendment();
            Myds = new DataSet();
            Myds = objAmendment.CompanyAmendment_Fetch_ByCompany_Step4(CompanyId);

            rows = Myds.Tables[0].Rows.Count;

            gvFactoryDetails.DataSource = Myds;// objCompany.CompanyAmendment_Fetch_ByUnit_Step4(CompanyId);
            gvFactoryDetails.DataBind();

            gvCapitalDetails.DataSource = Myds; //objCompany.CompanyAmendment_Fetch_ByUnit_Step4(CompanyId);
            gvCapitalDetails.DataBind();

            gvTechnicalDetails.DataSource = Myds; //objCompany.CompanyAmendment_Fetch_ByUnit_Step4(CompanyId);
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
        }
        catch { }

    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objAmendment = new clsAmendment();

        float bs6a1 = 0;
        float bs6a2 = 0;
        float bs6b = 0;
        float bs6a3 = 0;
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
                bstxt_12c = txt_12c.Text.ToString().Trim();
                bstxt_13a = txt_13a.Text.ToString().Trim();
                bstxt_13b = txt_13b.Text.ToString().Trim();


                ErrorId = objAmendment.CompanyAmendment_Insert_Step4(UnitId, CompanyId, bsTxt_5a1,
                            bsTxt_5a2, bsTxt_5b1, bstxt_5b2, bstxt_5c, bs6a1, bs6a2,bs6a3,
                            bs6b,bs6b2, bstxt_7a, bstxt_7b, bstxt_7c, bstxt_7d,
                            bstxt_7e, bstxt_8, bstxt_9, bstxt_10, bstxt_11,
                            bstxt_12a, bstxt_12b, bstxt_12c,bstxt_13a, bstxt_13b, UserId, "amendment");

                i++;
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        //string  fGPtbl_CUnit_5A1, string fGPtbl_CUnit_5A2, string fGPtbl_CUnit_5BB1, string fGPtbl_CUnit_5BB2, string fGPtbl_CUnit_5C, float fGPtbl_CUnit_6A1, float fGPtbl_CUnit_6A2, float fGPtbl_CUnit_6B, string fGPtbl_CUnit_7A, string fGPtbl_CUnit_7B, string fGPtbl_CUnit_7C, string fGPtbl_CUnit_7D, string fGPtbl_CUnit_7E, string fGPtbl_CUnit_8, string fGPtbl_CUnit_9, string fGPtbl_CUnit_10, string fGPtbl_CUnit_11,string fGPtbl_CUnit_12A, string fGPtbl_CUnit_12B, string fGPtbl_CUnit_13A, string fGPtbl_CUnit_13B, int UserId


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
                string EncryptFormtype = objDecEnc.Encrypt("3");

                Response.Redirect("NewUnitRegistrationStep5.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
            }
            else               //INSERT/UPDATE FAIL
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
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
                txttot.Text = Convert.ToString(Convert.ToDecimal(txt6a1.Text) + Convert.ToDecimal(txt6a2.Text) + Convert.ToDecimal(txt6a3.Text) + Convert.ToDecimal(txt6b.Text) + Convert.ToDecimal(txt6b2.Text));
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
        txt_13a.TabIndex = (short)(tabindx + 7);
        txt_13b.TabIndex = (short)(tabindx + 8);
        btnSubmit.TabIndex = (short)(tabindx + 9);

    }

    protected void btnback_Click(object sender, EventArgs e)
    {

        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        //Response.Redirect("UnitAmendmentStep3Store.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        Response.Redirect("UnitAmendmentStep3Store.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
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
}

