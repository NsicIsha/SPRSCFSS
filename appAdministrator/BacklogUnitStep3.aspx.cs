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

public partial class appAdministrator_BacklogUnitStep3 : System.Web.UI.Page
{
    int CompanyId, UnitId, Status, BranchId, Edit;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany; 
    int ErrorId;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
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
            UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            Edit = Convert.ToInt32(Request.QueryString["Edit"].Replace(" ", "+"));
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

    public void FunFillControls()
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

    private void FunFillData()
    {
        if (Status == 5 || Edit == 1)    //INCOMPLETE
        {
            //UNIT TABLE
            objCompany = new clsCompany();
            System.Data.SqlClient.SqlDataReader MyDr;
            MyDr = objCompany.Company_Fetch_ByUnit_Step3(UnitId);
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
                txt_3k.Text = MyDr["fGPtbl_CUnit_3K1"].ToString() != "" ? MyDr["fGPtbl_CUnit_3K1"].ToString() : "N.A.";
                txt_3l.Text = MyDr["fGPtbl_CUnit_3L"].ToString();
                txt_3m.Text = MyDr["fGPtbl_CUnit_3M"].ToString();
                txt_3n.Text = MyDr["fGPtbl_CUnit_3N"].ToString();
                txt_3o.Text = MyDr["fGPtbl_CUnit_3O"].ToString();
                txt_4a.Text = MyDr["fGPtbl_CUnit_4A"].ToString();
                txt_4b.Text = MyDr["fGPtbl_CUnit_4B"].ToString();
            }
            MyDr.Close();
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        objCompany = new clsCompany();

        System.Data.DataTable dt = new System.Data.DataTable();
        dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);

        foreach (System.Data.DataRow row in dt.Rows) // Loop over the rows.
        {

            int i = 0;

            foreach (var item in row.ItemArray) // Loop over the items.
            {
                if (i == 0)
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


                    //fGPtbl_CUnit_Id
                    UnitId = Convert.ToInt32(item);
                    objCompany = new clsCompany();
                    ErrorId = objCompany.Company_Insert_Step3(UnitId, CompanyId, bsRd3d, bsTxt_3e,
                                bsTxt_3f, bsTxt_3g, bstxt_3h, bstxt_3i,
                                bstxt_3j, bstxt_3k, bstxt_3l,
                                bstxt_3m, bstxt_3n, bstxt_3o, bstxt_4a,
                                bstxt_4b, UserId);
                }

                i++;

            }
        }

        dt.Clear();
        dt.Dispose();



        if (ErrorId == 1)   //INSERT/UPDATE SUCCESSFULL
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();

            Response.Redirect("BacklogUnitStep4.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&p=" + EncryptPan + "&Edit=0", false);
            

        }
        else               //INSERT/UPDATE FAIL
        {

        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());

        string EncryptPan = objDecEnc.Encrypt(objDecEnc.Decrypt(Request.QueryString["p"].Replace(" ", "+"))).ToString();
        string EncryptFormtype = objDecEnc.Encrypt("1");

        Response.Redirect("BacklogUnitStep2.aspx?c=" + EncryptCompany + "&f=" + EncryptFormtype + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId + "&PanCard=" + EncryptPan + "&Edit=0", false);
       
    }



}
