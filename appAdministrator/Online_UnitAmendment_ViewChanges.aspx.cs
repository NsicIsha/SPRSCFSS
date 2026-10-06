using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_Online_UnitAmendment_ViewChanges : System.Web.UI.Page
{
    clsCompany objCompany;
    clsAmendment objAmendment;
    int UserId, RoleId, companyId, DepartmentId, UnitId, Status, BranchId, FormType;
    //  int successId, urlId = 0, amendmentid;
    int Unitid, urlId = 1;
    string filepath;
    int storecount = 0, GridFactoryCount = 0, FactoryDetailsCount = 0,
        CapitalDetailsCount = 0, TechnicalDetailsCount = 0;

    string UserFullName;
    DataTable MydtChangeFactory, MydtChangeStore, MydtChangeTechnical, MydtChangeDirectors, MydtChangeCompany, MydtChangesAllowed = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            // DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            //filepath=Server.MapPath(".");
            filepath = Request.FilePath[1].ToString();
            FunRequest();
            if (RoleId == 8 || RoleId == 6)
            {
                //urlId = Convert.ToInt32(Request.QueryString["i"].ToString());
                if (Page.IsPostBack != true)
                {
                    /* if (Request.QueryString["Comp_id"] != null)
                     {*/

                    /*if (Request.QueryString["aid"] != null)
                    {
                        amendmentid = Convert.ToInt32(Request.QueryString["aid"].ToString());
                    }*/
                    //Showdata(Convert.ToInt32(Request.QueryString["Comp_id"]),1);//, urlId);urlid is formtypeid
                    //, urlId);urlid is formtypeid
                    //}
                }
            }
            /* else
             {
                 btnAccept.Visible = false;
                 btnReject.Visible = false;
             }*/

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


        Showdata(companyId);
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            if (Request.QueryString["c"] != null)
                companyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            if (Request.QueryString["u"] != null)
                UnitId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["u"].Replace(" ", "+")).ToString());
            if (Request.QueryString["s"] != null)
                Status = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["s"].Replace(" ", "+")).ToString());
            if (Request.QueryString["b"] != null)
                BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            if (Request.QueryString["i"] != null)
                urlId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["i"].Replace(" ", "+")).ToString());
            if (urlId == 999)
            {
                btn.Visible = false; //IN SPECIAL CASE BACK BUTTON IS NOT VISIBLE
                btn1.Visible = false;
            }

            if (urlId == 2)//FOR RENWAL CASE MAKING NEXT BUTTON HIDDEN
            {
                btnNext.Visible = false;
                btnNext_bottom.Visible = false;
                btn.Visible = false; //IN SPECIAL CASE BACK BUTTON IS NOT VISIBLE
                btn1.Visible = false;
            }

            if (UnitId == 0 || Status == 0 || BranchId == 0)
            {
                btnNext.Visible = false;
                btnNext_bottom.Visible = false;
                // lnkNext.Visible = false;
                lbBack.Visible = true;
                //lnknextBottom.Visible = false;
            }
            else
            {
                lbBack.Visible = true;
                btnNext.Visible = true;
                btnNext_bottom.Visible = true;
                //lnkNext.Visible = true;
                //lnknextBottom.Visible = true;
            }

        }
        catch
        {
            companyId = 0;

            BranchId = 0;
        }
    }

    //TO SHOW INFORMATION ON LABEL
    public void Showdata(int ComapnyId)
    {
        try
        {

            DataSet MydsView, MydsChange = new DataSet();

            DataTable MydtViewFactory, MydtViewStore, MydtViewTechnical, MydtViewDirectors, MydtViewCompany = new DataTable();
            //DataTable MydtChangeFactory, MydtChangeStore, MydtChangeTechnical, MydtChangeDirectors, MydtChangeCompany, MydtChangesAllowed = new DataTable();

            objAmendment = new clsAmendment();
            objCompany = new clsCompany();

            //TO FETCH DATA BY COMPANY ID
            if (urlId == 2)//FETCHING FOR RENEWAL
            {
                MydsView = objAmendment.CompanyAmendment_CompanyDetails_Fetch_ByCompanyId(ComapnyId, 2);
            }
            else//FETCHING FOR AMENDMENT
            {
                MydsView = objAmendment.CompanyAmendment_CompanyDetails_Fetch_ByCompanyId(ComapnyId, 3);
            }

            if (urlId == 2)//FETCHING FOR RENEWAL
            {
                MydsChange = objAmendment.CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(ComapnyId, 2);
                MydtChangeDirectors = MydsChange.Tables[2];
            }
            else//FETCHING FOR AMENDMENT
            {
                MydsChange = objAmendment.CompanyAmendment_CompanyChangesDetails_Fetch_ByCompanyId(ComapnyId, 3);
                MydtChangeStore = MydsChange.Tables[2];
                MydtChangeDirectors = MydsChange.Tables[3];
            }

            MydtChangesAllowed = objAmendment.CompanyAmendment_CompanyChangesAllowed_Fetch();


            MydtViewFactory = MydsView.Tables[3];
            MydtViewStore = MydsView.Tables[1];
            MydtViewTechnical = MydsView.Tables[2];
            MydtViewDirectors = MydsView.Tables[4];
            MydtViewCompany = MydsView.Tables[0];

            MydtChangeCompany = MydsChange.Tables[0];
            MydtChangeFactory = MydsChange.Tables[1];//IT HOLD DATA FOR TCHNICAL AND OTHERS
            /*MydtChangeStore = MydsChange.Tables[2];
            MydtChangeDirectors = MydsChange.Tables[3];*/


            //GETTING UNIT ID
            Unitid = Convert.ToInt32(MydtViewFactory.Rows[0]["fGPtbl_CUnit_Id"]);

            //GETTING FORMTYPE ID
            //Formtypeid=Convert.ToInt32(Mydt.Rows[0]["fGPtbl_CUnit_FormtypeId"]);


            try
            {
                //BINDING STORE DETAILS GRIDVIEW
                gvStoredetails.DataSource = MydtViewStore;
                gvStoredetails.DataBind();
            }
            catch { }


            try
            {
                //BINDING UPLOADED GRIDVIEW
                gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid, 3);
                gvDocumentDetail.DataBind();
            }
            catch { }

            //BINDING FACTORY GRIDVIEW
            try
            {
                GridFactory.DataSource = MydtViewFactory;
                GridFactory.DataBind();
            }
            catch { }
            //BINDING HORSE POWER GRIDVIEW
            try
            {
                gvFactoryDetails.DataSource = MydtViewTechnical;
                gvFactoryDetails.DataBind();
            }
            catch { }

            //BINDING CAPITAL DETAILS GRIDVIEW
            try
            {
                gvCapitalDetails.DataSource = MydtViewTechnical;// Myds.Tables[2];
                gvCapitalDetails.DataBind();
            }
            catch { }

            //BINDING TECHNICAL DETAILS GRIDVIEW
            try
            {
                gvTechnicalDetails.DataSource = MydtViewTechnical;// Myds.Tables[2];
                gvTechnicalDetails.DataBind();
            }
            catch { }


            //TO SHOW IN LABEL
            lblPAN.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_PAN"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_PAN"].ToString(), "0"))
                lblPAN.ForeColor = Color.Red;
            lbl_udyog_aadhar.Text = MydtViewCompany.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGtbl_Company_AdharNumber"].ToString(), "0"))
                lbl_udyog_aadhar.ForeColor = Color.Red;
            lblCompany.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_Name"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_Name"].ToString(), "0"))
                lblCompany.ForeColor = Color.Red;

            lblAddress.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString(), "0"))
                lblAddress.ForeColor = Color.Red;

            lblPin.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString(), "0"))
                lblPin.ForeColor = Color.Red;

            lblState.Text = MydtViewCompany.Rows[0]["CompanyState"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_StateId"].ToString(), "0"))
                lblState.ForeColor = Color.Red;

            lblCity.Text = MydtViewCompany.Rows[0]["CompanyCity"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_CityId"].ToString(), "0"))
                lblCity.ForeColor = Color.Red;

            lblPhone.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString(), "0"))
                lblPhone.ForeColor = Color.Red;

            lblFax.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString(), "0"))
                lblFax.ForeColor = Color.Red;

            lblEmail.Text = MydtViewCompany.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
            if (Equals(MydtChangeCompany.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString(), "0"))
                lblEmail.ForeColor = Color.Red;

            lblCatarea.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_CategoryAreaId"].ToString(), "0"))
                lblCatarea.ForeColor = Color.Red;

            lblCatcast.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_CC_Name"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_CategoryCastId"].ToString(), "0"))
                lblCatcast.ForeColor = Color.Red;

            lblCatusize.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_CategoryUnitSizeId"].ToString(), "0"))
                lblCatusize.ForeColor = Color.Red;

            lblConst.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3C123_ConstitutionId"].ToString(), "0"))
                lblConst.ForeColor = Color.Red;

            lblIncorp.Text = MydtViewCompany.Rows[0]["IncDate"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3A"].ToString(), "0"))
                lblIncorp.ForeColor = Color.Red;

            lblBuss.Text = MydtViewCompany.Rows[0]["ProdDate"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3B"].ToString(), "0"))
                lblBuss.ForeColor = Color.Red;

            lbl3c5Part.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3C5"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3C5"].ToString(), "0"))
                lbl3c5Part.ForeColor = Color.Red;

            lbl3dregunder.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3D"].ToString(), "0"))
                lbl3dregunder.ForeColor = Color.Red;

            lbl3eAssociate.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3E"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3E"].ToString(), "0"))
                lbl3eAssociate.ForeColor = Color.Red;

            lbl3flParent.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3F"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3F"].ToString(), "0"))
                lbl3flParent.ForeColor = Color.Red;

            lbl3gDirector.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3G"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3G"].ToString(), "0"))
                lbl3gDirector.ForeColor = Color.Red;

            lbl3hCapital.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3H"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3H"].ToString(), "0"))
                lbl3hCapital.ForeColor = Color.Red;

            lbl3iInter.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3I"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3I"].ToString(), "0"))
                lbl3iInter.ForeColor = Color.Red;

            lbl3jLoans.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3J"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3J"].ToString(), "0"))
                lbl3jLoans.ForeColor = Color.Red;

            lbl3k1Icircle.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3K1"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3K1"].ToString(), "0"))
                lbl3k1Icircle.ForeColor = Color.Red;

            lbl3lSalestax.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3L"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3L"].ToString(), "0"))
                lbl3lSalestax.ForeColor = Color.Red;

            lbl3mNsicreg.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3M"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3M"].ToString(), "0"))
                lbl3mNsicreg.ForeColor = Color.Red;

            lbl3nnsicbuz.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3N"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3N"].ToString(), "0"))
                lbl3nnsicbuz.ForeColor = Color.Red;

            lbl3oProsecute.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_3O"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_3O"].ToString(), "0"))
                lbl3oProsecute.ForeColor = Color.Red;

            lbl4aDGSD.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_4A"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_4A"].ToString(), "0"))
                lbl4aDGSD.ForeColor = Color.Red;

            lbl4bDGSDsupplied.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_4B"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_4B"].ToString(), "0"))
                lbl4bDGSDsupplied.ForeColor = Color.Red;

            lbl8Typetest.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_8"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_8"].ToString(), "0"))
                lbl8Typetest.ForeColor = Color.Red;

            lbl9Serviceprod.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_9"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_9"].ToString(), "0"))
                lbl9Serviceprod.ForeColor = Color.Red;

            lbl10Threeyrs.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_10"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_10"].ToString(), "0"))
                lbl10Threeyrs.ForeColor = Color.Red;

            lbl11Banker.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_11"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_11"].ToString(), "0"))
                lbl11Banker.ForeColor = Color.Red;

            lbl12aBis.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_12A"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_12A"].ToString(), "0"))
                lbl12aBis.ForeColor = Color.Red;

            lbl12bIso.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_12B"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_12B"].ToString(), "0"))
                lbl12bIso.ForeColor = Color.Red;

            lbl13aSignatory.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_13A"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_13A"].ToString(), "0"))
                lbl13aSignatory.ForeColor = Color.Red;

            lbl13bDesignation.Text = MydtViewCompany.Rows[0]["fGPtbl_CUnit_13B"].ToString();
            if (Equals(MydtChangeFactory.Rows[0]["fGPtbl_CUnit_13B"].ToString(), "0"))
                lbl13bDesignation.ForeColor = Color.Red;
            bind_turn_over();
            //BINDING DIRECTORS INFORMATION
            if (MydtViewDirectors.Rows.Count > 0)
            {
                lbl_3ca1Name.Text = MydtViewDirectors.Rows[0]["fGPtbl_CUnit_Directors_Name"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_Name"].ToString(), "0"))
                    lbl_3ca1Name.ForeColor = Color.Red;
                lbl_3ca1Address.Text = MydtViewDirectors.Rows[0]["fGPtbl_CUnit_Directors_Address"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_Address"].ToString(), "0"))
                    lbl_3ca1Address.ForeColor = Color.Red;
                lbl_3ca1Pin.Text = MydtViewDirectors.Rows[0]["fGPtbl_CUnit_Directors_Zip"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_Zip"].ToString(), "0"))
                    lbl_3ca1Pin.ForeColor = Color.Red;
                lbl_3ca1Tele.Text = MydtViewDirectors.Rows[0]["fGPtbl_CUnit_Directors_Phone"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_Phone"].ToString(), "0"))
                    lbl_3ca1Tele.ForeColor = Color.Red;
                lblState_3ca1.Text = MydtViewDirectors.Rows[0]["fGPtbl_MS_Name"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_StateId"].ToString(), "0"))
                    lblState_3ca1.ForeColor = Color.Red;
                lblCity_3ca1.Text = MydtViewDirectors.Rows[0]["fGPtbl_MCty_Name"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_CityId"].ToString(), "0"))
                    lblCity_3ca1.ForeColor = Color.Red;
                lbl_3ca1Share.Text = MydtViewDirectors.Rows[0]["fGPtbl_CUnit_Directors_Shareholder"].ToString();
                if (Equals(MydtChangeDirectors.Rows[0]["fGPtbl_Company_Directors_Shareholder"].ToString(), "0"))
                    lbl_3ca1Share.ForeColor = Color.Red;
            }

            if (MydtViewDirectors.Rows.Count > 1)
            {
                lbl_3ca2Name.Text = MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Name"].ToString();
                lbl_3ca2Address.Text = MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Address"].ToString();
                lbl_3ca2Pin.Text = MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Zip"].ToString();
                lbl_3ca2Tele.Text = MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Phone"].ToString();
                lblState_3ca2.Text = MydtViewDirectors.Rows[1]["fGPtbl_MS_Name"].ToString();
                lblCity_3ca2.Text = MydtViewDirectors.Rows[1]["fGPtbl_MCty_Name"].ToString();
                lbl_3ca2Share.Text = MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Shareholder"].ToString();

                if (Math.Sign(Convert.ToInt32(MydtViewDirectors.Rows[1]["fGPtbl_CUnit_Directors_Id"].ToString())) == 1)
                {
                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_Name"].ToString(), "0"))
                        lbl_3ca2Name.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_Address"].ToString(), "0"))
                        lbl_3ca2Address.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_Zip"].ToString(), "0"))
                        lbl_3ca2Pin.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_Phone"].ToString(), "0"))
                        lbl_3ca2Tele.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_StateId"].ToString(), "0"))
                        lblState_3ca2.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_CityId"].ToString(), "0"))
                        lblCity_3ca2.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[1]["fGPtbl_Company_Directors_Shareholder"].ToString(), "0"))
                        lbl_3ca2Share.ForeColor = Color.Red;
                }
                else
                {
                    lbl_3ca2Name.ForeColor = Color.Blue;
                    lbl_3ca2Address.ForeColor = Color.Blue;
                    lbl_3ca2Pin.ForeColor = Color.Blue;
                    lbl_3ca2Tele.ForeColor = Color.Blue;
                    lblState_3ca2.ForeColor = Color.Blue;
                    lblCity_3ca2.ForeColor = Color.Blue;
                    lbl_3ca2Share.ForeColor = Color.Blue;
                }
            }

            if (MydtViewDirectors.Rows.Count > 2)
            {
                lbl_3ca3Name.Text = MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Name"].ToString();
                lbl_3ca3Address.Text = MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Address"].ToString();
                lbl_3ca3Pin.Text = MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Zip"].ToString();
                lbl_3ca3Tele.Text = MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Phone"].ToString();
                lblState_3ca3.Text = MydtViewDirectors.Rows[2]["fGPtbl_MS_Name"].ToString();
                lblCity_3ca3.Text = MydtViewDirectors.Rows[2]["fGPtbl_MCty_Name"].ToString();
                lbl_3ca3Share.Text = MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Shareholder"].ToString();

                if (Math.Sign(Convert.ToInt32(MydtViewDirectors.Rows[2]["fGPtbl_CUnit_Directors_Id"].ToString())) == 1)
                {

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_Name"].ToString(), "0"))
                        lbl_3ca3Name.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_Address"].ToString(), "0"))
                        lbl_3ca3Address.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_Zip"].ToString(), "0"))
                        lbl_3ca3Pin.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_Phone"].ToString(), "0"))
                        lbl_3ca3Tele.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_StateId"].ToString(), "0"))
                        lblState_3ca3.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_CityId"].ToString(), "0"))
                        lblCity_3ca3.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[2]["fGPtbl_Company_Directors_Shareholder"].ToString(), "0"))
                        lbl_3ca3Share.ForeColor = Color.Red;
                }
                else
                {
                    lbl_3ca3Name.ForeColor = Color.Blue;
                    lbl_3ca3Address.ForeColor = Color.Blue;
                    lbl_3ca3Pin.ForeColor = Color.Blue;
                    lbl_3ca3Tele.ForeColor = Color.Blue;
                    lblState_3ca3.ForeColor = Color.Blue;
                    lblCity_3ca3.ForeColor = Color.Blue;
                    lbl_3ca3Share.ForeColor = Color.Blue;
                }
            }

            if (MydtViewDirectors.Rows.Count > 3)
            {
                lbl_3ca4Name.Text = MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Name"].ToString();
                lbl_3ca4Address.Text = MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Address"].ToString();
                lbl_3ca4Pin.Text = MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Zip"].ToString();
                lbl_3ca4Tele.Text = MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Phone"].ToString();
                lblState_3ca4.Text = MydtViewDirectors.Rows[3]["fGPtbl_MS_Name"].ToString();
                lblCity_3ca4.Text = MydtViewDirectors.Rows[3]["fGPtbl_MCty_Name"].ToString();
                lbl_3ca4Share.Text = MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Shareholder"].ToString();

                if (Math.Sign(Convert.ToInt32(MydtViewDirectors.Rows[3]["fGPtbl_CUnit_Directors_Id"].ToString())) == 1)
                {

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_Name"].ToString(), "0"))
                        lbl_3ca4Name.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_Address"].ToString(), "0"))
                        lbl_3ca4Address.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_Zip"].ToString(), "0"))
                        lbl_3ca4Pin.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_Phone"].ToString(), "0"))
                        lbl_3ca4Tele.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_StateId"].ToString(), "0"))
                        lblState_3ca4.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_CityId"].ToString(), "0"))
                        lblCity_3ca4.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[3]["fGPtbl_Company_Directors_Shareholder"].ToString(), "0"))
                        lbl_3ca4Share.ForeColor = Color.Red;
                }
                else
                {
                    lbl_3ca4Name.ForeColor = Color.Blue;
                    lbl_3ca4Address.ForeColor = Color.Blue;
                    lbl_3ca4Pin.ForeColor = Color.Blue;
                    lbl_3ca4Tele.ForeColor = Color.Blue;
                    lblState_3ca4.ForeColor = Color.Blue;
                    lblCity_3ca4.ForeColor = Color.Blue;
                    lbl_3ca4Share.ForeColor = Color.Blue;
                }
            }

            if (MydtViewDirectors.Rows.Count > 4)
            {
                lbl_3ca5Name.Text = MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Name"].ToString();
                lbl_3ca5Address.Text = MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Address"].ToString();
                lbl_3ca5Pin.Text = MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Zip"].ToString();
                lbl_3ca5Tele.Text = MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Phone"].ToString();
                lblState_3ca5.Text = MydtViewDirectors.Rows[4]["fGPtbl_MS_Name"].ToString();
                lblCity_3ca5.Text = MydtViewDirectors.Rows[4]["fGPtbl_MCty_Name"].ToString();
                lbl_3ca5Share.Text = MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Shareholder"].ToString();

                if (Math.Sign(Convert.ToInt32(MydtViewDirectors.Rows[4]["fGPtbl_CUnit_Directors_Id"].ToString())) == 1)
                {

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_Name"].ToString(), "0"))
                        lbl_3ca5Name.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Ccompany_Directors_Address"].ToString(), "0"))
                        lbl_3ca5Address.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_Zip"].ToString(), "0"))
                        lbl_3ca5Pin.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_Phone"].ToString(), "0"))
                        lbl_3ca5Tele.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_StateId"].ToString(), "0"))
                        lblState_3ca5.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_CityId"].ToString(), "0"))
                        lblCity_3ca5.ForeColor = Color.Red;

                    if (Equals(MydtChangeDirectors.Rows[4]["fGPtbl_Company_Directors_Shareholder"].ToString(), "0"))
                        lbl_3ca5Share.ForeColor = Color.Red;
                }
                else
                {
                    lbl_3ca5Name.ForeColor = Color.Blue;
                    lbl_3ca5Address.ForeColor = Color.Blue;
                    lbl_3ca5Pin.ForeColor = Color.Blue;
                    lbl_3ca5Tele.ForeColor = Color.Blue;
                    lblState_3ca5.ForeColor = Color.Blue;
                    lblCity_3ca5.ForeColor = Color.Blue;
                    lbl_3ca5Share.ForeColor = Color.Blue;
                }
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void lbBack_Click(object sender, EventArgs e)
    {
        if (urlId == 1)
        {
            Response.Redirect("WaitingCompanies.aspx?i=1", false);
        }
        else if (urlId == 3)
        {
            Response.Redirect("WaitingCompanies.aspx?i=3", false);
        }
        else if (urlId == 5)
        {
            Response.Redirect("NewUnitRegistrationStep5.aspx", false);
        }
    }


    protected void gvDocumentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //objCompany = new clsCompany();
            //DataSet Myds = new DataSet();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");

                //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                hlname.NavigateUrl = "../" + hfpath.Value.ToString() + hfname.Value.ToString();
                //hlname.NavigateUrl="C:/Documents and Settings/abhishek.jain.WSDP29/My Documents/Visual Studio 2008/Projects/NSICGP/Document/Upload/Registration/1_3-d146h_2012130_21549_406_D5tB60t83dbX.jpg";

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void GridFactory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //DataTable MydtChangeFactory1=new DataTable();
        //MydtChangeFactory1 = MydtChangeFactory;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnUnitid = (HiddenField)e.Row.FindControl("HDFunitid");

                if (MydtChangeFactory.Rows.Count > GridFactoryCount && MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_Id"].ToString() == hdnUnitid.Value)
                {
                    //WRITE YOUR CODE TO TRACK CHANGES
                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_Address"].ToString(), "0"))
                    {
                        e.Row.Cells[1].ForeColor = Color.Red;
                        e.Row.Cells[1].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_Zip"].ToString(), "0"))
                    {
                        e.Row.Cells[2].ForeColor = Color.Red;
                        e.Row.Cells[2].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_StateId"].ToString(), "0"))
                    {
                        e.Row.Cells[3].ForeColor = Color.Red;
                        e.Row.Cells[3].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_CityId"].ToString(), "0"))
                    {
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[4].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_Phone"].ToString(), "0"))
                    {
                        e.Row.Cells[5].ForeColor = Color.Red;
                        e.Row.Cells[5].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_Fax"].ToString(), "0"))
                    {
                        e.Row.Cells[6].ForeColor = Color.Red;
                        e.Row.Cells[6].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[GridFactoryCount]["fGPtbl_CUnit_1B_Factory_Email"].ToString(), "0"))
                    {
                        e.Row.Cells[7].ForeColor = Color.Red;
                        e.Row.Cells[7].Font.Bold = true;
                    }


                }
                else
                {
                    e.Row.ForeColor = Color.FromArgb(255);
                    e.Row.Font.Bold = true;
                    //GridFactoryCount = 0;
                }

                GridFactoryCount++;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    protected void gvStoredetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (urlId != 2)
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    HiddenField hdnunit = (HiddenField)e.Row.FindControl("HDFUnitid");
                    HiddenField hdnAmdstoreId = (HiddenField)e.Row.FindControl("HDFAmdStoreId");
                    if (Convert.ToInt32(hdnunit.Value) > 0)
                    {
                        if (MydtChangeStore.Rows[storecount]["fGPtbl_CUnitAmendment_Store_Id"].ToString() == hdnAmdstoreId.Value)
                        {
                            //WRITE YOUR CODE TO TRACK CHANGES
                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString(), "0"))
                            {
                                e.Row.Cells[1].ForeColor = Color.Red;
                                e.Row.Cells[1].Font.Bold = true;
                            }

                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString(), "0"))
                            {
                                e.Row.Cells[2].ForeColor = Color.Red;
                                e.Row.Cells[2].Font.Bold = true;
                            }

                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_ModelBrand"].ToString(), "0"))
                            {
                                e.Row.Cells[3].ForeColor = Color.Red;
                                e.Row.Cells[3].Font.Bold = true;
                            }

                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_LimitingSizeCapacityRating"].ToString(), "0"))
                            {
                                e.Row.Cells[4].ForeColor = Color.Red;
                                e.Row.Cells[4].Font.Bold = true;
                            }

                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_AnnualProductionCapacity"].ToString(), "0"))
                            {
                                e.Row.Cells[5].ForeColor = Color.Red;
                                e.Row.Cells[5].Font.Bold = true;
                            }

                            if (Equals(MydtChangeStore.Rows[storecount]["fGPtbl_CUnit_Store_NoOfShifts"].ToString(), "0"))
                            {
                                e.Row.Cells[6].ForeColor = Color.Red;
                                e.Row.Cells[6].Font.Bold = true;
                            }


                        }
                    }
                    else
                    {
                        e.Row.ForeColor = Color.FromArgb(255);
                        e.Row.Font.Bold = true;
                    }

                    storecount++;
                }
            }
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError(ex.Message);
        }

    }


    protected void gvFactoryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdnfUnitid = (HiddenField)e.Row.FindControl("HDNFUnit");

                if (MydtChangeFactory.Rows.Count > FactoryDetailsCount && MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_Id"].ToString() == hdnfUnitid.Value)
                {
                    //WRITE YOUR CODE TO TRACK CHANGES
                    if (Equals(MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_5A1"].ToString(), "0"))
                    {
                        e.Row.Cells[2].ForeColor = Color.Red;
                        e.Row.Cells[2].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_5A2"].ToString(), "0"))
                    {
                        e.Row.Cells[3].ForeColor = Color.Red;
                        e.Row.Cells[3].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_5BB1"].ToString(), "0"))
                    {
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[4].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_5BB2"].ToString(), "0"))
                    {
                        e.Row.Cells[5].ForeColor = Color.Red;
                        e.Row.Cells[5].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[FactoryDetailsCount]["fGPtbl_CUnit_5C"].ToString(), "0"))
                    {
                        e.Row.Cells[6].ForeColor = Color.Red;
                        e.Row.Cells[6].Font.Bold = true;
                    }



                }
                else
                {
                    e.Row.ForeColor = Color.FromArgb(255);
                    e.Row.Font.Bold = true;
                    // FactoryDetailsCount = 0;
                }

                FactoryDetailsCount++;
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
                HiddenField hdnfUnitid = (HiddenField)e.Row.FindControl("HiddenFieldUnit");

                if (MydtChangeFactory.Rows.Count > CapitalDetailsCount && MydtChangeFactory.Rows[CapitalDetailsCount]["fGPtbl_CUnit_Id"].ToString() == hdnfUnitid.Value)
                {
                    //WRITE YOUR CODE TO TRACK CHANGES
                    if (Equals(MydtChangeFactory.Rows[CapitalDetailsCount]["fGPtbl_CUnit_6A1"].ToString(), "0"))
                    {
                        e.Row.Cells[2].ForeColor = Color.Red;
                        e.Row.Cells[2].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[CapitalDetailsCount]["fGPtbl_CUnit_6A2"].ToString(), "0"))
                    {
                        e.Row.Cells[3].ForeColor = Color.Red;
                        e.Row.Cells[3].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[CapitalDetailsCount]["fGPtbl_CUnit_6B"].ToString(), "0"))
                    {
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[4].Font.Bold = true;
                    }


                }
                else
                {
                    e.Row.ForeColor = Color.FromArgb(255);
                    e.Row.Font.Bold = true;
                    //CapitalDetailsCount = 0;
                }

                CapitalDetailsCount++;
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
                HiddenField hdnfUnitid = (HiddenField)e.Row.FindControl("HFTechnicalUnit");

                if (MydtChangeFactory.Rows.Count > TechnicalDetailsCount && MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_Id"].ToString() == hdnfUnitid.Value)
                {
                    //WRITE YOUR CODE TO TRACK CHANGES
                    if (Equals(MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_7A"].ToString(), "0"))
                    {
                        e.Row.Cells[2].ForeColor = Color.Red;
                        e.Row.Cells[2].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_7B"].ToString(), "0"))
                    {
                        e.Row.Cells[3].ForeColor = Color.Red;
                        e.Row.Cells[3].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_7C"].ToString(), "0"))
                    {
                        e.Row.Cells[4].ForeColor = Color.Red;
                        e.Row.Cells[4].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_7D"].ToString(), "0"))
                    {
                        e.Row.Cells[5].ForeColor = Color.Red;
                        e.Row.Cells[5].Font.Bold = true;
                    }

                    if (Equals(MydtChangeFactory.Rows[TechnicalDetailsCount]["fGPtbl_CUnit_7E"].ToString(), "0"))
                    {
                        e.Row.Cells[5].ForeColor = Color.Red;
                        e.Row.Cells[5].Font.Bold = true;
                    }

                }
                else
                {
                    e.Row.ForeColor = Color.FromArgb(255);
                    e.Row.Font.Bold = true;
                    //TechnicalDetailsCount = 0;
                }

                TechnicalDetailsCount++;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }



    protected void lnkNext_Click(object sender, EventArgs e)
    {
        try
        {
            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            string EncryptCompany = objDecEnc.Encrypt(companyId.ToString());
            string EncryptUnit = objDecEnc.Encrypt(UnitId.ToString());
            string EncryptStatus = objDecEnc.Encrypt(Status.ToString());
            string EncryptBranchId = objDecEnc.Encrypt(BranchId.ToString());
            Response.Redirect("Online_Unit_Ren_Amnd_Step6.aspx?c=" + EncryptCompany + "&u=" + EncryptUnit + "&s=" + EncryptStatus + "&b=" + EncryptBranchId, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    public void bind_turn_over()
    {
        try
        {

            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(companyId);//3;
            DataSet ds_report = new DataSet();
            // objCompany.FetchTurnover_history(CompanyID2, 1);
            // ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            ds_report = objCompany.FetchTurnover_history(CompanyID2, 3);
            if (ds_report.Tables[1].Rows.Count > 0)
            {
                //if (ds_report.Tables[0].Rows.Count > 1)
                // {
                grdTurnOver.DataSource = ds_report.Tables[1];
                grdTurnOver.DataBind();
                // }
            }
            else
            {
                grdTurnOver.Dispose();
                grdTurnOver.DataBind();
                ds_report.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }

    }

    protected void grdturnover_rowDatbound(object sender, GridViewRowEventArgs e)
    {
        int GrdTotalCount = 0;
        GrdTotalCount = grdTurnOver.Columns.Count;

        string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
        e.Row.Cells[0].Text = decodedText;
        e.Row.Cells[0].Visible = false;
        //e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "250px");

        // grdTurnOver.Columns.Add["ravish"];


    }
}