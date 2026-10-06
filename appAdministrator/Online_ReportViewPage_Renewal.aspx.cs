using System;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Globalization;

public partial class appAdministrator_Online_ReportViewPage_Renewal : System.Web.UI.Page
{
    clsCompany objCompany;
    DataSet Myds;
    int UserId, RoleId, DepartmentId;
    int successId;
    int Unitid, Formtypeid, count = 0;
    string filepath;
    int urlId = 1;
    string UserFullName;

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
    protected void Page_Load(object sender, EventArgs e)
    {
        //Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            FunRequest();
            //UserId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //RoleId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            lblhiden.Text = RoleId.ToString();

            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();

            //filepath=Server.MapPath(".");
            filepath = Request.FilePath[1].ToString();

            Master.Page.ClientScript.RegisterStartupScript
                  (this.GetType(), "", "hideMaster_Page();", true);  // For hide Master Pages Control

            /* Uttam
            DecryptEncrypt objDecEnc1 = new DecryptEncrypt();
            Showdata(Convert.ToInt32(objDecEnc1.Decrypt(Request.QueryString["Company_ID"])));
             */


            if (RoleId == 1 || RoleId == 5 || RoleId == 3 || RoleId == 2 || RoleId == 10)
            {
                if (Page.IsPostBack != true)
                {
                    //if (Request.QueryString["id"] != null)
                    if (Request.QueryString["Company_ID"] != null)
                    {
                        DecryptEncrypt objDecEnc = new DecryptEncrypt();
                        //Showdata(Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["id"])));
                        lbl_Company_Name.Text = objDecEnc.Decrypt(Request.QueryString["Com_name"].Replace(" ", "+")).ToString();
                        lblCompany_ID.Text = objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(" ", "+")).ToString();

                        //lblCompany_ID.Text = 35748.ToString();


                        Showdata(Convert.ToInt32(lblCompany_ID.Text));
                        BindStatus_Date();  // // for show status  admin

                        bind_turn_over();
                        bindcheckvalidity(Convert.ToInt32(lblCompany_ID.Text.ToString()));
                        // Bindstatus_ByRole();
                    }
                }
            }
            else if (RoleId == 7 || RoleId == 8 || RoleId == 6 || RoleId == 4 || RoleId == 9 || RoleId == 0)
            {
                if (Page.IsPostBack != true)
                {
                    //if (Request.QueryString["id"] != null)
                    if (Request.QueryString["Company_ID"] != null)
                    {
                        DecryptEncrypt objDecEnc = new DecryptEncrypt();
                        //Showdata(Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["id"])));
                        lbl_Company_Name.Text = objDecEnc.Decrypt(Request.QueryString["Com_name"].Replace(' ', '+').ToString()).ToString();
                        lblCompany_ID.Text = objDecEnc.Decrypt(Request.QueryString["Company_ID"].Replace(' ', '+').ToString()).ToString();
                        Showdata(Convert.ToInt32(lblCompany_ID.Text));
                        Bindstatus_ByRole(); // for show status without admin
                        bind_turn_over();
                        bindcheckvalidity(Convert.ToInt32(lblCompany_ID.Text.ToString()));
                    }
                }


            }





            else
            {

            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
        }
        //End Read Cookies++++
    }
    public void Showdata(int Companyid)
    {
        try
        {
            Myds = new DataSet();
            System.Data.SqlClient.SqlDataReader MydrSSI;
            System.Data.SqlClient.SqlDataReader MydrDIR;

            DataTable Mydt, Mydt2, Mydt3 = new DataTable();

            objCompany = new clsCompany();


            Myds = objCompany.Company_UnitDetails_Fetch_ByCompanyId(Companyid);


            Mydt = Myds.Tables[0];

            Mydt2 = Myds.Tables[2];
            Mydt3 = Myds.Tables[3];

            //GETTING UNIT ID   // Comment by Ravish Kumar Pandey
            if (Myds.Tables[0].Rows.Count > 0)
            {
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
                gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid,2);//formtypeId);
                gvDocumentDetail.DataBind();
            }
            catch { }

            //BINDING FACTORY GRIDVIEW
            try
            {
                GridFactory.DataSource = Mydt3;// Myds.Tables[3];
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
                MydrSSI = objCompany.Company_Fetch_ByCompany_Step2(Companyid);
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
                MydrDIR = objCompany.Company_FetchDirectorByUnit_Step2(Companyid);
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
            if (Request.QueryString["BranchName"].ToString() != "")
            {
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                lbl_branch_Name.Text = objDecEnc.Decrypt(Request.QueryString["BranchName"].Replace(' ', '+')).ToString();
            }
            else
            {
                lbl_branch_Name.Text = Mydt.Rows[0]["BranchName"].ToString();
            }



            //TO SHOW IN LABEL
            if (Mydt.Rows.Count > 0)
            {

                if (Mydt.Rows[0]["fGPtbl_Company_GPReferenceNo"].ToString() != null)
                {
                    lblGpref.Text = Mydt.Rows[0]["fGPtbl_Company_GPReferenceNo"].ToString();
                    hdn_accessid.Value = Mydt.Rows[0]["fGPtbl_Company_AccessId"].ToString();
                }
                else
                {
                    lblGpref.Text = string.Empty;
                }
            }
            lblPAN.Text = Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString();
            lbl_udyog_aadhar_number.Text = Mydt.Rows[0]["fGtbl_Company_AdharNumber"].ToString();
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
            if (Mydt.Rows[0]["fgtbl_Company_Status"].ToString() == "1")
            {
                tr_registration.Visible = true;
                tr_refrence_number.Visible = false;
                lbl_registration_number.Text = Mydt.Rows[0]["RegistrationNumber"].ToString();
            }
            else
            {
                tr_refrence_number.Visible = true;
                tr_registration.Visible = false;
            }
            if (RoleId == 9)
            {
                /* tbl_show_multiple_record_tr.Visible = false;
                 tbl_show_single_record.Visible = false;*/
                tr_register_date.Visible = true;
                tr_upload_document.Visible = false;
                gvDocumentDetail.Visible = false;

            }
            else
            {

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

    public void BindStatus_Date()
    {
        int UserID = 0;
        int CompanyID = Convert.ToInt32(lblCompany_ID.Text);
        objCompany = new clsCompany();
        DataSet dsstatus_Date = new DataSet();
        dsstatus_Date = objCompany.Company_StatusTrack_Status(CompanyID);

        if (dsstatus_Date.Tables[0].Rows.Count > 0)
        {
            // For CONFIRM
            if (dsstatus_Date.Tables[0].Rows[0]["FormTypeID"].ToString() == "2")
            {
                lbl_waiting.Visible = false;
                lbl_inprocess.Visible = false;

                lbl_inprocess_inspectionpassed.Visible = false;
                lbl_Registration_Date_status.Text = "Renewal Date";
            }
            else if (dsstatus_Date.Tables[0].Rows[0]["FormTypeID"].ToString() == "3")
            {
                lbl_waiting.Visible = true;
                lbl_inprocess.Visible = true;
                lbl_inprocess_inspectionpassed.Visible = true;
                lbl_Registration_Date_status.Text = "Amendment Date";
            }
            else if (dsstatus_Date.Tables[0].Rows[0]["FormTypeID"].ToString() == "1")
            {
                lbl_waiting.Visible = true;
                lbl_inprocess.Visible = true;
                lbl_inprocess_inspectionpassed.Visible = true;

            }
            if (dsstatus_Date.Tables[0].Rows[0]["Statustrackmain"].ToString() == "0")
            {
                tbl_show_single_record.Visible = false;
                tbl_show_multiple_record_tr.Visible = true;


                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate1"].ToString() != "")
                {

                    lbl_Confirm_Date.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate1"].ToString();
                    // lbl_Confirm_Date.Text = dsstatus_Date.Tables[0].Rows[0]["RegistrationDate"].ToString();
                    hdnstauserid1.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID1"].ToString();

                    if (hdnstauserid1.Value != "0" || hdnstauserid1.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID1 = new DataSet();
                        UserID = Convert.ToInt32(hdnstauserid1.Value);
                        DSUserID1 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID1.Tables[0].Rows.Count > 0)
                        {
                            lbl_Confirm_User.Text = DSUserID1.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID1.Dispose();
                            // lbl_Confirm_Date.Text = "";
                        }
                    }
                    else
                    {
                        //lbl_Confirm_Date.Text = "";
                        hdnstauserid1.Value = "";
                    }

                }

                else
                {
                    lbl_Confirm_Date.Text = "";
                }
                // End OF CONFIRM

                // Start Inprocess
                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate2"].ToString() != "")
                {
                    lbl_inprocess_date.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate2"].ToString();
                    hdn_userid_inprocess.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID2"].ToString();
                    if (hdn_userid_inprocess.Value != "0" || hdn_userid_inprocess.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID2 = new DataSet();
                        UserID = Convert.ToInt32(hdn_userid_inprocess.Value);
                        DSUserID2 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID2.Tables[0].Rows.Count > 0)
                        {
                            lbl_inprocess_User.Text = DSUserID2.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID2.Dispose();
                            lbl_inprocess_User.Text = "";

                        }
                    }
                    else
                    {
                        hdn_userid_inprocess.Value = "";
                        lbl_inprocess_User.Text = "";
                    }
                }
                else
                {
                    hdn_userid_inprocess.Value = "";
                    lbl_inprocess_User.Text = "";
                }
                // // End INprocess    
                // Start Waiting
                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate3"].ToString() != "")
                {
                    lbl_waiting_Date.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate3"].ToString();
                    hdn_userid_waiting.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID3"].ToString();
                    if (hdn_userid_waiting.Value != "0" || hdn_userid_waiting.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID3 = new DataSet();
                        UserID = Convert.ToInt32(hdn_userid_waiting.Value);
                        DSUserID3 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID3.Tables[0].Rows.Count > 0)
                        {
                            lbl_waiting_user.Text = DSUserID3.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID3.Dispose();
                            //  lbl_waiting_user.Text = "";

                        }
                    }
                    else
                    {
                        hdn_userid_waiting.Value = "";
                        lbl_waiting_user.Text = "";
                    }
                }
                else
                {
                    hdn_userid_waiting.Value = "";
                    lbl_waiting_user.Text = "";
                }
                // End Waiting 
                //sTART Expired

                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate6"].ToString() != "")
                {
                    lbl_expired_Date.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate6"].ToString();
                    hdn_userid_expired.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID6"].ToString();
                    if (hdn_userid_expired.Value != "0" || hdn_userid_expired.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID6 = new DataSet();
                        UserID = Convert.ToInt32(hdn_userid_expired.Value);
                        DSUserID6 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID6.Tables[0].Rows.Count > 0)
                        {
                            lbl_expired_user.Text = DSUserID6.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID6.Dispose();
                            lbl_expired_user.Text = "";

                        }
                    }
                    else
                    {
                        hdn_userid_expired.Value = "";
                        lbl_expired_user.Text = "";
                    }
                }
                else
                {
                    hdn_userid_expired.Value = "";
                    lbl_expired_user.Text = "";
                }




                // eND Expired

                // start  inComplte
                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate5"].ToString() != "")
                {
                    lbl_status_DATE.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate5"].ToString();
                    hdnstauserid1.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID5"].ToString();
                    if (hdnstauserid1.Value != "0" || hdnstauserid1.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID5 = new DataSet();
                        UserID = Convert.ToInt32(hdnstauserid1.Value);
                        DSUserID5 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID5.Tables[0].Rows.Count > 0)
                        {
                            lbl_sttusIncomplte_User.Text = DSUserID5.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID5.Dispose();
                            lbl_sttusIncomplte_User.Text = "";

                        }
                    }
                    else
                    {
                        hdnstauserid1.Value = "";
                        lbl_sttusIncomplte_User.Text = "";
                    }





                }
                else
                {
                    hdnstauserid1.Value = "";
                    lbl_sttusIncomplte_User.Text = "";
                }


                // End INCOMPLETE


                // Start  Insepection Passed
                if (dsstatus_Date.Tables[0].Rows[0]["StatusDate7"].ToString() != "")
                {
                    lbl_inspection_Passed_Date.Text = dsstatus_Date.Tables[0].Rows[0]["StatusDate7"].ToString();
                    hdn_userid_inseprction_Passed.Value = dsstatus_Date.Tables[0].Rows[0]["SatusUserrID5"].ToString();
                    if (hdn_userid_inseprction_Passed.Value != "0" || hdn_userid_inseprction_Passed.Value != null)
                    {
                        objCompany = new clsCompany();
                        DataSet DSUserID6 = new DataSet();
                        UserID = Convert.ToInt32(hdn_userid_inseprction_Passed.Value);
                        DSUserID6 = objCompany.Company_StatusTrack_UserName(UserID);
                        if (DSUserID6.Tables[0].Rows.Count > 0)
                        {
                            lbl_InspectionPassed_user.Text = DSUserID6.Tables[0].Rows[0]["UName"].ToString();
                        }
                        else
                        {
                            DSUserID6.Dispose();
                            lbl_InspectionPassed_user.Text = "";

                        }
                    }
                    else
                    {
                        hdn_userid_inseprction_Passed.Value = "";
                        lbl_InspectionPassed_user.Text = "";
                    }





                }
                else
                {
                    hdn_userid_inseprction_Passed.Value = "";
                    lbl_InspectionPassed_user.Text = "";
                }

                //End  Insepection Passed

            }
            else
            {
                // if (dsstatus_Date.Tables[0].Rows[0]["Statustrack"].ToString() == "Registerd")
                // {
                tbl_show_single_record.Visible = true;
                tbl_show_multiple_record_tr.Visible = false;
                // tbl_show_single_record.Visible = true;

                lblstatusdate.Visible = true;


                string format = "dd/MM/yyyy";

                DateTime dt_reg = Convert.ToDateTime(dsstatus_Date.Tables[0].Rows[0]["RegistrationDate"].ToString());
                lblstatusdate.Text = dsstatus_Date.Tables[0].Rows[0]["Statustrack"].ToString();
                lblusernameID.Text = dt_reg.ToString(format, DateTimeFormatInfo.InvariantInfo);

                // }
            }


        }// Main if Count 
        else
        {
            tbl_show_single_record.Visible = true;
            tbl_show_multiple_record_tr.Visible = false;

            dsstatus_Date.Dispose();
        }



    }
    public void Bindstatus_ByRole()
    {
        try
        {
            int CompanyID = Convert.ToInt32(lblCompany_ID.Text);
            objCompany = new clsCompany();
            DataSet dsstatus_Date = new DataSet();
            dsstatus_Date = objCompany.Company_StatusTrack_Status(CompanyID);
            DataTable dt = new DataTable();

            dt = dsstatus_Date.Tables[0];
            if (dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["Statustrackmain"].ToString() == "0")
                {

                    DataTable dtfilter = new DataTable();
                    var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["CompanyID"].ToString()) == CompanyID).Count();
                    if (count > 0)
                    {
                        dtfilter = dt.AsEnumerable().Where(a => Convert.ToInt32(a["CompanyID"].ToString()) == CompanyID).CopyToDataTable();
                        if (dtfilter.Rows.Count > 0)
                        {
                            if (dtfilter.Rows[0]["StatusDate1"].ToString() != "")
                            {
                                if (dtfilter.Rows[0]["FormTypeID"].ToString() == "2")
                                {
                                    lblstatusdate.Text = "Renewal Date";
                                }
                                else if (dtfilter.Rows[0]["FormTypeID"].ToString() == "3")
                                {
                                    lblstatusdate.Text = "Amendment Date";
                                }
                                else if (dtfilter.Rows[0]["FormTypeID"].ToString() == "1")
                                {
                                    lblstatusdate.Text = "Registration Date";
                                }
                                lblusernameID.Text = dtfilter.Rows[0]["StatusDate1"].ToString();
                                lbl_registration_Date.Text = lblusernameID.Text;

                            }
                            else if (dtfilter.Rows[0]["StatusDate2"].ToString() != "")
                            {
                                if (lblstatusdate.Text == "")
                                {
                                    if (dtfilter.Rows[0]["FormTypeID"] == "2")
                                    {
                                        lblstatusdate.Visible = false;
                                        lblstatusdate.Text = "";   //"Inprocess";
                                        lblusernameID.Text = "";//dtfilter.Rows[0]["StatusDate2"].ToString();
                                        lblusernameID.Visible = false;
                                    }
                                    else if (dtfilter.Rows[0]["FormTypeID"] == "3")
                                    {
                                        lblstatusdate.Visible = true;
                                        lblusernameID.Visible = true;
                                        lblstatusdate.Text = "Inprocess";
                                        lblusernameID.Text = dtfilter.Rows[0]["StatusDate2"].ToString();
                                    }
                                    else if (dtfilter.Rows[0]["FormTypeID"] == "1")
                                    {
                                        lblstatusdate.Visible = true;
                                        lblusernameID.Visible = true;
                                        lblstatusdate.Text = "Inprocess";
                                        lblusernameID.Text = dtfilter.Rows[0]["StatusDate2"].ToString();
                                    }

                                }
                            }

                            else if (dtfilter.Rows[0]["StatusDate3"].ToString() != "")
                            {
                                if (lblstatusdate.Text == "")
                                {
                                    if (dtfilter.Rows[0]["FormTypeID"] == "2")
                                    {
                                        lblstatusdate.Visible = false;
                                        lblstatusdate.Text = "";       //"Inprocess";
                                        lblusernameID.Text = "";//dtfilter.Rows[0]["StatusDate2"].ToString();
                                        lblusernameID.Visible = false;
                                    }
                                    else if (dtfilter.Rows[0]["FormTypeID"] == "3")
                                    {
                                        lblstatusdate.Visible = true;
                                        lblusernameID.Visible = true;
                                        lblstatusdate.Text = "Waiting";
                                        lblusernameID.Text = dtfilter.Rows[0]["StatusDate3"].ToString();
                                    }
                                    else if (dtfilter.Rows[0]["FormTypeID"] == "1")
                                    {
                                        lblstatusdate.Visible = true;
                                        lblusernameID.Visible = true;
                                        lblstatusdate.Text = "Waiting";
                                        lblusernameID.Text = dtfilter.Rows[0]["StatusDate3"].ToString();
                                    }



                                }
                            }
                            else if (dtfilter.Rows[0]["StatusDate7"].ToString() != "")
                            {
                                if (lblstatusdate.Text == "")
                                {
                                    lblstatusdate.Text = "Inspection Passed";
                                    lblusernameID.Text = dtfilter.Rows[0]["StatusDate7"].ToString();
                                }
                            }
                            else if (dtfilter.Rows[0]["StatusDate6"].ToString() != "")
                            {
                                if (lblstatusdate.Text == "")
                                {
                                    lblstatusdate.Text = "Expired";
                                    lblusernameID.Text = dtfilter.Rows[0]["StatusDate6"].ToString();
                                }
                            }

                            else if (dtfilter.Rows[0]["StatusDate5"].ToString() != "")
                            {
                                if (lblstatusdate.Text == "")
                                {
                                    lblstatusdate.Text = "Application Date ";
                                    lblusernameID.Text = dtfilter.Rows[0]["StatusDate5"].ToString();
                                }
                            }


                        }
                    }

                }
                else
                {
                    tbl_show_multiple_record_tr.Visible = false;
                    tbl_show_single_record.Visible = true;
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["FormTypeID"].ToString() == "2")
                        {
                            lblstatusdate.Text = "Renewal Date";
                        }
                        else if (dt.Rows[0]["FormTypeID"].ToString() == "3")
                        {
                            lblstatusdate.Text = "Amendment Date";
                        }
                        else if (dt.Rows[0]["FormTypeID"].ToString() == "1")
                        {
                            lblstatusdate.Text = "Registration Date";
                        }
                    }
                    else
                    {
                        dt.Dispose();
                    }

                    // lblstatusdate.Visible = true;
                    string format = "dd/MM/yyyy";
                    // lblstatusdate.Text = dsstatus_Date.Tables[0].Rows[0]["Statustrack"].ToString();
                    DateTime dt_reg = Convert.ToDateTime(dsstatus_Date.Tables[0].Rows[0]["RegistrationDate"].ToString());
                    lblusernameID.Text = dt_reg.ToString(format, DateTimeFormatInfo.InvariantInfo);

                }

            }

            else
            {
                tbl_show_multiple_record_tr.Visible = false;
                tbl_show_single_record.Visible = true;
            }
        }
        catch { }

    }

    public void bind_turn_over()
    {
        try
        {
            int CompanyID2 = Convert.ToInt32(lblCompany_ID.Text.ToString());
            objCompany = new clsCompany();
            DataSet dsTurnover = new DataSet();
            // dsTurnover = objCompany.Company_ReportView_TUrnOver(CompanyID2);
            dsTurnover = objCompany.FetchTurnover_history(CompanyID2, 1);
            var Rowcount = dsTurnover.Tables.Count;
            if (dsTurnover.Tables.Count != 0)
            {

                if (dsTurnover.Tables[1].Rows.Count > 0)
                {
                    if (dsTurnover.Tables[1].Rows.Count > 1)
                    {
                        //if (dsTurnover.Tables[0].Columns.Count > 3)
                        //{
                        //  int lastcolumnindex = dsTurnover.Tables[0].Columns.Count - 1;
                        // dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[lastcolumnindex]);
                        grdTurnOver.DataSource = dsTurnover.Tables[1];
                        grdTurnOver.DataBind();
                        DataSet ds_consolidatedmonetrylimit = new DataSet();
                        ds_consolidatedmonetrylimit = objCompany.Fetch_ConsolidatedFor_MultipleMonetryLimit(CompanyID2);
                        if (ds_consolidatedmonetrylimit.Tables[0].Rows.Count > 0)
                        {
                            string consolidate = ds_consolidatedmonetrylimit.Tables[0].Rows[0]["Cosolidated"].ToString();
                            lbl_consolidate.Text = "Consolidated Monetary Limit" + " " + "Rs." + consolidate.ToString() + " " + "Lakhs";
                        }
                        else
                        {
                            ds_consolidatedmonetrylimit.Dispose();

                        }


                    }
                    /*  else
                      {
                          dsTurnover.Tables[0].Columns[1].ColumnName = "Annual Turnover";
                          dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[2]);
                          grdTurnOver.DataSource = dsTurnover.Tables[0];
                          grdTurnOver.DataBind();
                      }*/
                    /* }
                     else
                     {
                         grdTurnOver.Dispose();
                         grdTurnOver.DataBind();
                     }*/
                }
                else
                {
                    grdTurnOver.Dispose();
                    grdTurnOver.DataBind();
                }
            }
            else
            {
                dsTurnover.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }


        /*
       try
       {

           objCompany = new clsCompany();
           int CompanyID2 = Convert.ToInt32(lblCompany_ID.Text.ToString());//3;
           DataSet ds_report = new DataSet();
           ds_report = objCompany.Company_ReportView_TUrnOver(CompanyID2);
           if (ds_report.Tables.Count > 0)
           {
               if (ds_report.Tables[0].Rows.Count > 1)
               {
                   grdTurnOver.DataSource = ds_report.Tables[0];
                   grdTurnOver.DataBind();
               }
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
       }*/

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

    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    protected void gvStoredetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
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
                lbl_validity_show.Text = ds_chekvality.Tables[0].Rows[0]["validity"].ToString();
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
}