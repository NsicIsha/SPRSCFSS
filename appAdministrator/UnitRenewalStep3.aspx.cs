using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_UnitRenewalStep3 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    clsRenewal objRenewal;
    clsAmendment objAmendment;
    int ErrorId;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            checkButtonDoubleClick(btnSubmit);
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        FunRequest();
        FunFillControls();



    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            // UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["f"].Replace(" ", "+")).ToString()); // Formtype id
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
        }
        catch
        {
            CompanyId = 0;
            //UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }
    public void FunFillControls()
    {
        try
        {
            if (!Page.IsPostBack)
            {
                //FILL RADIOBUTTON LIST
                objCompany = new clsCompany();
                System.Data.DataTable Dt = new System.Data.DataTable();
                Dt = objCompany.Company_Fetch_RegisteredUnder();
                rd3d.DataTextField = "fGPtbl_CUnit_RegisteredUnder_Name";
                rd3d.DataValueField = "fGPtbl_CUnit_RegisteredUnder_Id";
                rd3d.DataSource = Dt;
                rd3d.DataBind();

                rd3d.SelectedIndex = 0;
            }
            if (!Page.IsPostBack)
            {

                FunFillData();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        //FunfillDropdown();
    }


    private void FunFillData()
    {
        
        //UNIT TABLE
        objRenewal = new clsRenewal();
        System.Data.SqlClient.SqlDataReader MyDr;
        MyDr = objRenewal.CompanyRenewal_Fetch_Step3_ByCompanyId(CompanyId);
        try
        {
            while (MyDr.Read())
            {
                try
                {
                    rd3d.SelectedValue = MyDr["fGPtbl_CUnit_3D"].ToString();
                }
                catch { }

                txt_3e.Text = MyDr["fGPtbl_CUnit_3E"].ToString();
                txt_3f.Text = MyDr["fGPtbl_CUnit_3F"].ToString();
                txt_3g.Text = MyDr["fGPtbl_CUnit_3G"].ToString();
                txt_3h.Text = MyDr["fGPtbl_CUnit_3H"].ToString();
                txt_3i.Text = MyDr["fGPtbl_CUnit_3I"].ToString();
                txt_3j.Text = MyDr["fGPtbl_CUnit_3J"].ToString();
                txt_3k.Text = MyDr["fGPtbl_CUnit_3K1"].ToString();
                txt_3l.Text = MyDr["fGPtbl_CUnit_3L"].ToString();
                txt_3m.Text = MyDr["fGPtbl_CUnit_3M"].ToString();
                txt_3n.Text = MyDr["fGPtbl_CUnit_3N"].ToString();
                txt_3o.Text = MyDr["fGPtbl_CUnit_3O"].ToString();
                txt_4a.Text = MyDr["fGPtbl_CUnit_4A"].ToString();
                txt_4b.Text = MyDr["fGPtbl_CUnit_4B"].ToString();
            }
            MyDr.Close();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        FunctionShowHide();
        
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        int bsRd3d = 0;
        string bsTxt_3e = "";
        string bsTxt_3f = "";
        string bsTxt_3g = "";
        string bstxt_3h = "";
        string bstxt_3i = "";
        string bstxt_3j = "";
        string bstxt_3k = "";
        string bstxt_3l = "";
        string bstxt_3m = "";
        string bstxt_3n = "";
        string bstxt_3o = "";
        string bstxt_4a = "";
        string bstxt_4b = "";

        try
        {
            bsRd3d = Convert.ToInt32(rd3d.SelectedValue.ToString());
        }
        catch { }

        try
        {
            bsTxt_3e = txt_3e.Text.ToString().Trim();
            bsTxt_3f = txt_3f.Text.ToString().Trim();
            bsTxt_3g = txt_3g.Text.ToString().Trim();
            bstxt_3h = txt_3h.Text.ToString().Trim();
            bstxt_3i = txt_3i.Text.ToString().Trim();
            bstxt_3j = txt_3j.Text.ToString().Trim();
            bstxt_3k = txt_3k.Text.ToString().Trim();
            bstxt_3l = txt_3l.Text.ToString().Trim();
            bstxt_3m = txt_3m.Text.ToString().Trim();
            bstxt_3n = txt_3n.Text.ToString().Trim();
            bstxt_3o = txt_3o.Text.ToString().Trim();
            bstxt_4a = txt_4a.Text.ToString().Trim();
            bstxt_4b = txt_4b.Text.ToString().Trim();


            objAmendment = new clsAmendment();
            ErrorId = objAmendment.Company_Insert_Step3(CompanyId, bsRd3d, bsTxt_3e,
                        bsTxt_3f, bsTxt_3g, bstxt_3h, bstxt_3i,
                        bstxt_3j, bstxt_3k, bstxt_3l,
                        bstxt_3m, bstxt_3n, bstxt_3o, bstxt_4a,
                        bstxt_4b, UserId, "renewal");


            if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
            {
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
                string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
                string EncryptStatus = objDecEnc.Encrypt("2");
                string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
                Response.Redirect("UnitRenewalStep3Store.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            //Response.Redirect("UnitAmendmentStep3Store.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
            Response.Redirect("UnitRenewalStep2.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void FunctionShowHide()
    {
        DataSet AllstepchangeDs = new DataSet();
        DataRow[] AllstepchangeRow;
        try
        {
            AllstepchangeDs = clsDDL.Allfieldsbyrows_Fetch_Dataset(CompanyId);
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3J' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3j.ReadOnly = false;
            }
            else
            {
                txt_3j.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3K1' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3k.ReadOnly = false;
            }
            else
            {
                txt_3k.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3L' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3l.ReadOnly = false;
            }
            else
            {
                txt_3l.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3M' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3m.ReadOnly = false;
            }
            else
            {
                txt_3m.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3N' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3n.ReadOnly = false;
            }
            else
            {
                txt_3n.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3O' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3o.ReadOnly = false;
            }
            else
            {
                txt_3o.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_4A' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_4a.ReadOnly = false;
            }
            else
            {
                txt_4a.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_4B' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_4b.ReadOnly = false;
            }
            else
            {
                txt_4b.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3D' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                rd3d.Enabled = true;
            }
            else
            {
                rd3d.Enabled = false;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3E' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3e.ReadOnly = false;
            }
            else
            {
                txt_3e.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3F' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3f.ReadOnly = false;
            }
            else
            {
                txt_3f.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3G' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3g.ReadOnly = false;

            }
            else
            {
                txt_3g.ReadOnly = true;
            }
            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3H' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3h.ReadOnly = false;
            }
            else
            {
                txt_3h.ReadOnly = true;
            }

            AllstepchangeRow = AllstepchangeDs.Tables[1].Select("Colname='fGPtbl_CUnit_3I' and Colval = 1 ");
            if (AllstepchangeRow.Length > 0 && RoleId == 8)
            {
                txt_3i.ReadOnly = false;
            }
            else
            {
                txt_3i.ReadOnly = true;
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
}
