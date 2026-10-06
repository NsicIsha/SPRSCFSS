using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_UnitRenewalStep3Store : System.Web.UI.Page
{

    int CompanyId, UnitId, Status, BranchId;
    int UserId, RoleId, DepartmentId;
    string UserFullName;

    clsCompany objCompany;
    clsAmendment objAmendment;

    DataTable Dt;
    DataRow[] foundrow;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;


    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            checkButtonDoubleClick(btnSave);
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
        if (!IsPostBack)
        {
            //  RepeaterBind();
            GridViewBind();
        }

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
            UnitId = 0;
            Status = 0;
            BranchId = 0;
        }
    }

    private void GridViewBind()
    {
        Dt = new DataTable();
       // objAmendment = new clsAmendment();
        objCompany = new clsCompany();
        Dt = objCompany.Company_Fetch_StoreByCompanyId(CompanyId); 
        gvStoreDetails.DataSource = Dt;
        gvStoreDetails.DataBind();

      //  mvStore.SetActiveView(viewGrid);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt("2");
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        Response.Redirect("UnitRenewalStep4.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);


    }
    protected void btnBackstep3_Click(object sender, EventArgs e)
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        string EncryptCompany = objDecEnc.Encrypt(CompanyId.ToString());
        string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
        string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
        string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
        //Response.Redirect("UnitAmendmentStep3Store.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        Response.Redirect("UnitRenewalStep3.aspx?c=" + EncryptCompany + "&f=" + EncryptStatus + "&b=" + EncryptBranchId, false);
    }

    public DataTable Company_Fetch_FactoryLocation(int CompanyId)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("GPsp_Company_Fetch_CityNameOfFactory_ByCompanyId_For_Renewal", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_CUnit_CompanyId", SqlDbType.Int).Value = CompanyId;

            MyAdp.Fill(MyDt);
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
        return MyDt;
    }
    protected void gvStoreDetails_rowdatabound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Dt = new DataTable();
            objCompany = new clsCompany();

            //   Dt = objCompany.Company_Fetch_FactoryLocation(CompanyId);
            Dt = Company_Fetch_FactoryLocation(CompanyId);

            HiddenField hdnuntid = (HiddenField)e.Row.FindControl("hdnUnitid");
            if (hdnuntid != null)
            {
                Literal location = (Literal)e.Row.FindControl("ltlLocation");
                string expression = "fGPtbl_CUnit_Id=" + hdnuntid.Value.ToString();

                foundrow = Dt.Select(expression);
                location.Text = foundrow[0][1].ToString();
            }
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
