using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using NSICGP_VBClassLibrary;
using System.Data.SqlClient;
public partial class appAdministrator_RenewalformFieldRA : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string filepath;
    int urlId = 1;
    string UserFullName;
    int CompanyId;
    int BranchId;
    string BranchName = "";

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

    public void FunRequest_load()
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
        try
        {
            FunRequest_load();

            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            lblhiden.Text = RoleId.ToString();


            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            if (RoleId != 1 && RoleId != 10 && RoleId != 5)
            {
                BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
                BranchName = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString());
            }
            else
            {
                //BranchId = 0;
                //BranchName = "";
            }


            //filepath=Server.MapPath(".");
            filepath = Request.FilePath[1].ToString();

            Master.Page.ClientScript.RegisterStartupScript
                  (this.GetType(), "", "hideMaster_Page();", true);  // For hide Master Pages Control

            if (!IsPostBack)
            {
                if (Request.QueryString["Company_ID"] != null)
                {
                    DecryptEncrypt objDecEnc = new DecryptEncrypt();
                    //Showdata(Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["id"])));
                    lbl_Company_Name.Text = objDecEnc.Decrypt(Request.QueryString["Com_name"].Replace(' ', '+').ToString()).ToString();
                    lblCompany_ID.Text = objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(' ', '+').ToString()).ToString();
                    CompanyId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(' ', '+').ToString()).ToString());

                    ShowData(CompanyId);
                    bindcheckvalidity(Convert.ToInt32(lblCompany_ID.Text.ToString()));
                }
                else
                {

                }
            }
        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }

    }


    //-----------------------   Bind Validity of company      --------------

    public void bindcheckvalidity(int companyid)
    {
        try
        {

            objCompany = new clsCompany();
            DataSet ds_chekvality = new DataSet();
            ds_chekvality = objCompany.fetch_company_valitiy(companyid);
            if (ds_chekvality.Tables[0].Rows.Count > 0)
            {
                lbl_validity.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
            }
            else
            {
                ds_chekvality.Dispose();
            }
        }
        catch (Exception ex)
        {
            //  ErrHandler.WriteError("" + ex.Message.ToString() + "" + ex.StackTrace);
        }


    }


    public DataSet fetchRenewalViewPageThroughCompanyId(int companyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_CompanyRenewal_Waiting_Fetch_ForView_ByCompanyid_RA", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@fGPtbl_Company_Id", SqlDbType.Int).Value = companyId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }

    //---------------------------------------------------ShowData Of The Company----------
    public void ShowData(int CompanyId)
    {
        try
        {
            bind_turn_over();
            objCompany = new clsCompany();
            Myds = new DataSet();
            System.Data.SqlClient.SqlDataReader MydrSSI;
            System.Data.SqlClient.SqlDataReader MydrDIR;
            DataTable Mydt, Mydt2, Mydt3 = new DataTable();
            Myds = fetchRenewalViewPageThroughCompanyId(CompanyId);
            Mydt = Myds.Tables[0];

            Mydt2 = Myds.Tables[2];// Myds.Tables[3];
            Mydt3 = Myds.Tables[3];// Myds.Tables[4];
            if (Myds.Tables[0].Rows.Count > 0)
            {
                BranchId = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_BranchId"].ToString());
                BranchName = Convert.ToString(Myds.Tables[0].Rows[0]["BranchName"].ToString());
                if (Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"].ToString() != null)
                {
                    Unitid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);
                }
                else
                {

                }
            }

            try
            {
                //BINDING STORE DETAILS GRIDVIEW
                gvStoredetails.DataSource = Myds.Tables[1];
                gvStoredetails.DataBind();
            }
            catch { }
            try
            {
                //BINDING UPLOADED GRIDVIEW
                gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid, 2);//formtypeId);
                gvDocumentDetail.DataBind();
            }
            catch { }
            //BINDING FACTORY GRIDVIEW
            try
            {
                GridFactory.DataSource = Mydt3;//Mydt3, Myds.Tables[3];
                GridFactory.DataBind();
            }
            catch { }

            //BINDING HORSE POWER GRIDVIEW
            try
            {
                gvFactoryDetails.DataSource = Mydt2;// Myds.Tables[2];
                gvFactoryDetails.DataBind();
            }
            catch { }
            //BINDING CAPITAL DETAILS GRIDVIEW
            try
            {
                gvCapitalDetails.DataSource = Mydt2;// Myds.Tables[2];
                gvCapitalDetails.DataBind();
            }
            catch { }

            //BINDING TECHNICAL DETAILS GRIDVIEW
            try
            {
                gvTechnicalDetails.DataSource = Mydt2;// Myds.Tables[2];
                gvTechnicalDetails.DataBind();
            }
            catch { }
            //BINDING SSI GRIDVIEW
            try
            {
                MydrSSI = objCompany.Company_Fetch_ByCompany_Step2(CompanyId);
                if (MydrSSI.HasRows)
                {
                    grdSSI.DataSource = MydrSSI;
                    grdSSI.DataBind();
                }
                MydrSSI.Close();
                MydrSSI.Dispose();
            }
            catch { }

            //BINDING DIRCTORS GRIDVIEW
            try
            {
                MydrDIR = objCompany.Company_FetchDirectorByUnit_Step2(CompanyId);
                if (MydrDIR.HasRows)
                {
                    grdDirector.DataSource = MydrDIR;
                    grdDirector.DataBind();
                }
                MydrDIR.Close();
                MydrDIR.Dispose();
            }
            catch { }

            trbarancid.Visible = true;
            /*  if (Request.QueryString["BranchName"].ToString() != null)
              {
                  DecryptEncrypt objDecEnc = new DecryptEncrypt();
                  lbl_branch_Name.Text = objDecEnc.Decrypt(Request.QueryString["BranchName"].Replace(' ', '+')).ToString();
              }
              else
              {
                  lbl_branch_Name.Text = Mydt.Rows[0]["BranchName"].ToString();
              }*/
            lbl_branch_Name.Text = BranchName.ToString();

            //TO SHOW IN LABEL
            if (Mydt.Rows.Count > 0)
            {
                tr_refrence_number.Visible = true;
                DataSet ds_refreence_number = new DataSet();
                ds_refreence_number = objCompany.Fetch_RegistrationnumberthroughCompanyid(CompanyId);
                if (ds_refreence_number.Tables[0].Rows.Count > 0)
                {
                    lblGpref.Text = ds_refreence_number.Tables[0].Rows[0]["RefrenceNumber"].ToString();
                    lbl_registration_number.Text = ds_refreence_number.Tables[0].Rows[0]["RegistrationNumber"].ToString();
                }
                else
                {
                    lblGpref.Text = "";
                    lbl_registration_number.Text = "";
                    ds_refreence_number.Dispose();

                }
                if (Mydt.Rows[0]["fGPtbl_Company_GPReferenceNo"].ToString() != null)
                {
                    lblGpref.Text = Mydt.Rows[0]["fGPtbl_Company_GPReferenceNo"].ToString();
                }
                else
                {
                    lblGpref.Text = string.Empty;
                }
                //  }



                lbl_registration_number.Text = Mydt.Rows[0]["fGPtbl_CUnit_RegistrationNo"].ToString();
                lblPAN.Text = Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString();
                lblCompany.Text = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();
                lbl_Company_Name.Text = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();
                lblAddress.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                lblPin.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
                lblState.Text = Mydt.Rows[0]["CompanyState"].ToString();
                lblCity.Text = Mydt.Rows[0]["CompanyCity"].ToString();
                lblPhone.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
                lblFax.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
                lblEmail.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();

                lblCatarea.Text = Mydt.Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
                lblCatcast.Text = Mydt.Rows[0]["fGPtbl_CUnit_CC_Name"].ToString();
                lblCatusize.Text = Mydt.Rows[0]["fGPtbl_CUnit_CUS_Name"].ToString();
                lblConst.Text = Mydt.Rows[0]["fGPtbl_CUnit_Constitution_Name"].ToString();
                lblIncorp.Text = Mydt.Rows[0]["IncDate"].ToString();
                lblBuss.Text = Mydt.Rows[0]["ProdDate"].ToString();
                lbl3c5Part.Text = Mydt.Rows[0]["fGPtbl_CUnit_3C5"].ToString();
                lbl3dregunder.Text = Mydt.Rows[0]["fGPtbl_CUnit_RegisteredUnder_Name"].ToString();
                lbl3eAssociate.Text = Mydt.Rows[0]["fGPtbl_CUnit_3E"].ToString();
                lbl3flParent.Text = Mydt.Rows[0]["fGPtbl_CUnit_3F"].ToString();
                lbl3gDirector.Text = Mydt.Rows[0]["fGPtbl_CUnit_3G"].ToString();
                lbl3hCapital.Text = Mydt.Rows[0]["fGPtbl_CUnit_3H"].ToString();
                lbl3iInter.Text = Mydt.Rows[0]["fGPtbl_CUnit_3I"].ToString();
                lbl3jLoans.Text = Mydt.Rows[0]["fGPtbl_CUnit_3J"].ToString();
                lbl3k1Icircle.Text = Mydt.Rows[0]["fGPtbl_CUnit_3K1"].ToString();
                lbl3lSalestax.Text = Mydt.Rows[0]["fGPtbl_CUnit_3L"].ToString();
                lbl3mNsicreg.Text = Mydt.Rows[0]["fGPtbl_CUnit_3M"].ToString();
                lbl3nnsicbuz.Text = Mydt.Rows[0]["fGPtbl_CUnit_3N"].ToString();
                lbl3oProsecute.Text = Mydt.Rows[0]["fGPtbl_CUnit_3O"].ToString();
                lbl4aDGSD.Text = Mydt.Rows[0]["fGPtbl_CUnit_4A"].ToString();
                lbl4bDGSDsupplied.Text = Mydt.Rows[0]["fGPtbl_CUnit_4B"].ToString();

                lbl8Typetest.Text = Mydt.Rows[0]["fGPtbl_CUnit_8"].ToString();
                lbl9Serviceprod.Text = Mydt.Rows[0]["fGPtbl_CUnit_9"].ToString();
                lbl10Threeyrs.Text = Mydt.Rows[0]["fGPtbl_CUnit_10"].ToString();
                lbl11Banker.Text = Mydt.Rows[0]["fGPtbl_CUnit_11"].ToString();
                lbl12aBis.Text = Mydt.Rows[0]["fGPtbl_CUnit_12A"].ToString();
                lbl12bIso.Text = Mydt.Rows[0]["fGPtbl_CUnit_12B"].ToString();
                lbl13aSignatory.Text = Mydt.Rows[0]["fGPtbl_CUnit_13A"].ToString();
                lbl13bDesignation.Text = Mydt.Rows[0]["fGPtbl_CUnit_13B"].ToString();

                if (RoleId == 9)
                {

                    tr_register_date.Visible = true;
                    tr_upload_document.Visible = false;
                    gvDocumentDetail.Visible = false;

                }
                else
                {

                }

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }




    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            Myds = new DataSet();

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");

                //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                hlname.NavigateUrl = "../" + hfpath.Value.ToString() + hfname.Value.ToString();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void gvDocumentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    public void bind_turn_over()
    {
        try
        {

            objCompany = new clsCompany();
            int CompanyID2 = Convert.ToInt32(lblCompany_ID.Text.ToString());//3;
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
        //  e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "250px");

        // grdTurnOver.Columns.Add["ravish"];

    }

    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    protected void gvStoredetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        /* try
         {
             int cellcount = 0;
             if (e.Row.RowType == DataControlRowType.DataRow)
             {

                 //===========CODE FOR MERGING START=========================
                 DataRowView drv = (DataRowView)e.Row.DataItem;

                 if (drv["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString().Contains("\n"))
                 {
                     e.Row.Cells[2].Text = drv["fGPtbl_CUnit_Store_DescriptionStoresProducts"].ToString().Replace("\n", "<br/>");
                 }

                 if (drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString().Contains("\n"))
                 {
                     e.Row.Cells[3].Text = drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString().Replace("\n", "<br/>");
                 }

                 if (drv["fGPtbl_CUnit_Store_QualitativeCapacity"].ToString().Contains("\n"))
                 {
                     e.Row.Cells[8].Text = drv["fGPtbl_CUnit_Store_QualitativeCapacity"].ToString().Replace("\n", "<br/>");
                 }

                 if (drv["fGPtbl_CUnit_Store_QuantitativeCapacity"].ToString().Contains("\n"))
                 {
                     e.Row.Cells[9].Text = drv["fGPtbl_CUnit_Store_QuantitativeCapacity"].ToString().Replace("\n", "<br/>");
                 }

                 if (e.Row.RowIndex > 0)//FIRST TIME ROWSPAN NOT REQUIRED
                 {
                     if (drv["fGPtbl_CUnit_Store_RefOfBISSpecification"].ToString() == "0")
                     {
                         //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                         if (gvStoredetails.Rows[firstrowspecification].Cells[3].RowSpan == 0)
                         {
                             gvStoredetails.Rows[firstrowspecification].Cells[3].RowSpan = 2;
                         }
                         else
                         {
                             gvStoredetails.Rows[firstrowspecification].Cells[3].RowSpan += 1;
                         }
                         //Remove the cell
                         e.Row.Cells[3].Visible = false;
                     }

                     else //IT'S NEW VALUE
                     {
                         //Set the vertical alignment to top
                         e.Row.VerticalAlign = VerticalAlign.Top;
                         firstrowspecification = e.Row.RowIndex;
                     }

                     if (drv["fGPtbl_CUnit_Store_QualitativeCapacity"].ToString() == "0")
                     {
                         //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                         if (gvStoredetails.Rows[firstrowquality].Cells[8].RowSpan == 0)
                         {
                             gvStoredetails.Rows[firstrowquality].Cells[8].RowSpan = 2;
                         }
                         else
                         {
                             gvStoredetails.Rows[firstrowquality].Cells[8].RowSpan += 1;
                         }
                         //Remove the cell
                         cellcount = e.Row.Cells.Count;
                         e.Row.Cells[cellcount - 3].Visible = false;
                     }

                     else //IT'S NEW VALUE
                     {
                         //Set the vertical alignment to top
                         e.Row.VerticalAlign = VerticalAlign.Top;
                         firstrowquality = e.Row.RowIndex;
                     }

                     if (drv["fGPtbl_CUnit_Store_QuantitativeCapacity"].ToString() == "0")
                     {
                         //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                         if (gvStoredetails.Rows[firstrowquantity].Cells[9].RowSpan == 0)
                         {
                             gvStoredetails.Rows[firstrowquantity].Cells[9].RowSpan = 2;
                         }
                         else
                         {
                             gvStoredetails.Rows[firstrowquantity].Cells[9].RowSpan += 1;
                         }
                         //Remove the cell
                         cellcount = e.Row.Cells.Count;
                         e.Row.Cells[cellcount - 2].Visible = false;


                     }

                     else //IT'S A NEW VALUE
                     {
                         //Set the vertical alignment to top
                         e.Row.VerticalAlign = VerticalAlign.Top;
                         firstrowquantity = e.Row.RowIndex;
                     }
                 }

                 //===========CODE FOR MERGING END
             }
         }
         catch (Exception ex)
         {
             ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
         }*/
    }

}