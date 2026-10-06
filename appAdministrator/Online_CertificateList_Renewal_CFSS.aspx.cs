using NSICGP_VBClassLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_Online_CertificateList_Renewal_CFSS :  System.Web.UI.Page
{
    clsCompany objCompany;
    DataTable Mydt;
    int count = 1;
    int UserId, RoleId, DepartmentId, Branchid;
    string UserFullName;
    string UserEmailID = string.Empty;
    DateTime dtold;
    int CFSSUNITID;

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;
    FormInputReplace objInputReplace;
    int ErrorId = 0;

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
        FunRequest();
    }

    protected void btnf_Click(object sender, EventArgs e)
    {

        try
        {
            SearchGridviewBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected DataTable IfDeletionIsRequired(string PAN)
    {
        try
        {
            MyDt = new DataTable();
            MyAdp = new SqlDataAdapter("ForDeletionMoreThanTwoYears", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar).Value = PAN;
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

    private void DeleteTheUnit(string compID, string reason)
    {
        using (SqlConnection sqlConn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"]))
        {
            sqlConn.Open();
            using (SqlTransaction transaction = sqlConn.BeginTransaction())
            {
                try
                {
                    SqlCommand command = sqlConn.CreateCommand();
                    command.Transaction = transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GP_companies_deletion_UsingQuery";
                    command.CommandTimeout = 120;

                    command.Parameters.AddWithValue("@Companyid", compID);
                    command.Parameters.AddWithValue("@ErrorId", 0);
                    command.Parameters.AddWithValue("@Reason", reason);

                    command.ExecuteNonQuery();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
                }
                finally
                {

                }
            }
        }
    }

    protected void SearchGridviewBind()
    {
        if (txtPan.Text != "" && txtUAM.Text != "")
        {

            string SearchCriteria = string.Empty;

            objCompany = new clsCompany();
            DataTable dt = new DataTable();
            dt = Company_Fetch_Certificate_Search(txtUAM.Text, txtPan.Text, UserId.ToString());

            int CompanyId =   Convert.ToInt32(dt.Rows[0]["fGPtbl_Company_Id"].ToString());

            #region Retrive CFSSUnitId and Update in Company Table
            CFSSService obj = new CFSSService();
            CFSSUNITID = obj.CallApi_CFSSUNITID_Company(txtUAM.Text, "", txtPan.Text);

            if (CFSSUNITID != 0)
            {
                bool isUpdated = obj.UpdateCFSSUnitID_IfNull(CompanyId, CFSSUNITID);
            }
            else
            {
                string json = CFSSService.GetUnitDetailJson_R_RA(CompanyId);

                string errorMessage;
                bool isSuccess = obj.PostUnitDetailsToCFSS(CompanyId, json, out errorMessage);

                if (!isSuccess)
                {
                    lblMsg.Text = errorMessage;
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }
            }


            #endregion



            DataTable dataResult = new DataTable();
            dataResult = IfDeletionIsRequired(txtPan.Text);
            int days = Convert.ToInt32(dataResult.Rows[0]["DaysOver"].ToString());
            if (days > 730)
            {
                int compID = Convert.ToInt32(dt.Rows[0]["fGPtbl_Company_Id"].ToString());
                DeleteTheUnit(compID.ToString(), "Expired for More than Two years. ");
                Response.Redirect("NewUnitRegistration_CFSS.aspx?Status=1", false);
            }



            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("fGPtbl_CUnit_RegistrationNo");
            dtNew.Columns.Add("fGPtbl_Company_Name");
            dtNew.Columns.Add("fGPtbl_Company_PAN");
            dtNew.Columns.Add("HSCODENPC");
            dtNew.Columns.Add("fGPtbl_Company_HeadOffice_Address");
            dtNew.Columns.Add("fGPtbl_CERTIFICATE_PrintAccess");
            dtNew.Columns.Add("fGPtbl_CR_AcceptedByAdmin");
            dtNew.Columns.Add("fGPtbl_CERTIFICATE_Id");
            dtNew.Columns.Add("fGPtbl_Company_Id");
            dtNew.Columns.Add("doicheck");
            dtNew.Columns.Add("RegistrationDate");
            dtNew.Columns.Add("fgtbl_Certificate_Amedment");
            dtNew.Columns.Add("RenFlag");
            ArrayList compIds = new ArrayList();
            foreach (DataRow dr in dt.Rows)
            {
                string id = dr["fGPtbl_Company_Id"].ToString();

                if (!compIds.Contains(id))
                {
                    compIds.Add(id);
                }
            }

            foreach (string id in compIds)
            {

                string fGPtbl_CUnit_RegistrationNo = ("fGPtbl_CUnit_RegistrationNo");
                string fGPtbl_Company_Name = ("fGPtbl_Company_Name");
                string fGPtbl_Company_PAN = ("fGPtbl_Company_PAN");
                string HSCODENPC = ("HSCODENPC");
                string fGPtbl_Company_HeadOffice_Address = ("fGPtbl_Company_HeadOffice_Address");
                string fGPtbl_CERTIFICATE_PrintAccess = ("fGPtbl_CERTIFICATE_PrintAccess");

                string fGPtbl_CR_AcceptedByAdmin = ("fGPtbl_CR_AcceptedByAdmin");
                string fGPtbl_CERTIFICATE_Id = ("fGPtbl_CERTIFICATE_Id");
                string fGPtbl_Company_Id = ("fGPtbl_Company_Id");
                string doicheck = ("doicheck");
                string RegistrationDate = ("RegistrationDate");
                string fgtbl_Certificate_Amedment = ("fgtbl_Certificate_Amedment");
                string RenFlag = ("RenFlag");

                string ss = Convert.ToString(dt.Select("fGPtbl_Company_Id=" + id).Length);
                if (dt.Select("fGPtbl_Company_Id=" + id).Length > 1)
                {


                    DataRow[] dr = dt.Select("fGPtbl_Company_Id=" + id);
                    fGPtbl_CUnit_RegistrationNo = dr[0]["fGPtbl_CUnit_RegistrationNo"].ToString();
                    fGPtbl_Company_Name = dr[0]["fGPtbl_Company_Name"].ToString();
                    fGPtbl_Company_PAN = dr[0]["fGPtbl_Company_PAN"].ToString();
                    HSCODENPC = dr[0]["HSCODENPC"].ToString();
                    HSCODENPC = HSCODENPC + "<br/>" + dr[1]["HSCODENPC"].ToString();
                    fGPtbl_Company_HeadOffice_Address = dr[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                    fGPtbl_CERTIFICATE_PrintAccess = dr[0]["fGPtbl_CERTIFICATE_PrintAccess"].ToString();
                    fGPtbl_CR_AcceptedByAdmin = dr[0]["fGPtbl_CR_AcceptedByAdmin"].ToString();
                    fGPtbl_CERTIFICATE_Id = dr[0]["fGPtbl_CERTIFICATE_Id"].ToString();
                    fGPtbl_Company_Id = dr[0]["fGPtbl_Company_Id"].ToString();
                    doicheck = dr[0]["doicheck"].ToString();
                    RegistrationDate = dr[0]["RegistrationDate"].ToString();
                    fgtbl_Certificate_Amedment = dr[0]["fgtbl_Certificate_Amedment"].ToString();
                    RenFlag = dr[0]["RenFlag"].ToString();
                    dtNew.Rows.Add(
                         fGPtbl_CUnit_RegistrationNo,
                         fGPtbl_Company_Name,
                         fGPtbl_Company_PAN,
                         HSCODENPC,
                         fGPtbl_Company_HeadOffice_Address,
                         fGPtbl_CERTIFICATE_PrintAccess,
                         fGPtbl_CR_AcceptedByAdmin,
                         fGPtbl_CERTIFICATE_Id,
                         fGPtbl_Company_Id,
                         doicheck,
                         RegistrationDate,
                         fgtbl_Certificate_Amedment, RenFlag
                         );



                }
                else
                {
                    DataRow[] dr = dt.Select("fGPtbl_Company_Id=" + id);
                    fGPtbl_CUnit_RegistrationNo = dr[0]["fGPtbl_CUnit_RegistrationNo"].ToString();
                    fGPtbl_Company_Name = dr[0]["fGPtbl_Company_Name"].ToString();
                    fGPtbl_Company_PAN = dr[0]["fGPtbl_Company_PAN"].ToString();
                    HSCODENPC = dr[0]["HSCODENPC"].ToString();
                    fGPtbl_Company_HeadOffice_Address = dr[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
                    fGPtbl_CERTIFICATE_PrintAccess = dr[0]["fGPtbl_CERTIFICATE_PrintAccess"].ToString();
                    fGPtbl_CR_AcceptedByAdmin = dr[0]["fGPtbl_CR_AcceptedByAdmin"].ToString();
                    fGPtbl_CERTIFICATE_Id = dr[0]["fGPtbl_CERTIFICATE_Id"].ToString();
                    fGPtbl_Company_Id = dr[0]["fGPtbl_Company_Id"].ToString();
                    doicheck = dr[0]["doicheck"].ToString();
                    RegistrationDate = dr[0]["RegistrationDate"].ToString();
                    fgtbl_Certificate_Amedment = dr[0]["fgtbl_Certificate_Amedment"].ToString();
                    RenFlag = dr[0]["RenFlag"].ToString();



                    if (dr[0]["RenFlag"].ToString() == "0")
                    {
                        lblMsg.Text = "Unit is not eligible for Renewal. Unit can be renewed only six months before expiry date.";
                    }
                    else if (dr[0]["RenFlag"].ToString() == "11")
                    {
                        lblMsg.Text = "Unit is not eligible for Renewal,as unit is coming 2 years after the expiry of validity of SPRS certificate.";
                    }
                    else
                    {
                        lblMsg.Text = "";
                    }


                    dtNew.Rows.Add(
                         fGPtbl_CUnit_RegistrationNo,
                         fGPtbl_Company_Name,
                         fGPtbl_Company_PAN,
                         HSCODENPC,
                         fGPtbl_Company_HeadOffice_Address,
                         fGPtbl_CERTIFICATE_PrintAccess,
                         fGPtbl_CR_AcceptedByAdmin,
                         fGPtbl_CERTIFICATE_Id,
                         fGPtbl_Company_Id,
                         doicheck,
                         RegistrationDate,
                         fgtbl_Certificate_Amedment, RenFlag
                         );
                }

            }
            gvCertificateDetails.DataSource = dtNew;
            gvCertificateDetails.DataBind();

            if (gvCertificateDetails.Rows.Count == 0)
            {
                ltlMessage.Text = "No Data Found";
            }
            else
            {
                ltlMessage.Text = " ";
            }
        }
    }
    protected void gvCertificateDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnPrint = (LinkButton)e.Row.FindControl("lbnPrint");
                LinkButton btnDraftPrint = (LinkButton)e.Row.FindControl("lbnDraftPrint");
                LinkButton btnRequest = (LinkButton)e.Row.FindControl("lbnRequest");
                LinkButton btnRequest1 = (LinkButton)e.Row.FindControl("lbnRequest1");
                Literal ltlprint1 = (Literal)e.Row.FindControl("ltlPrint1");
                Literal litamd = (Literal)e.Row.FindControl("lt_amendment");
                Literal ltlprint = (Literal)e.Row.FindControl("ltlPrint");
                HiddenField hdncompId = (HiddenField)e.Row.FindControl("HiddenFieldCompanyId");
                HiddenField hdnCompIdAmd = (HiddenField)e.Row.FindControl("HiddenFieldCompanyIdAmendment");
                //HiddenField hdnFormType = (HiddenField)e.Row.FindControl("HiddenFormType");
                string FormtypeCertificate = string.Empty;
                string imageid = string.Empty;
                string imgdraft = string.Empty;
                string imageid_Amedment = string.Empty;
                LinkButton amendment = (LinkButton)e.Row.FindControl("lnkamedment");

                //string checkvaluerequset = "";
                //DataTable dt_201415 = new DataTable();
                //dt_201415 = dt_fetch_companyrecord201415(Convert.ToInt32(hdncompId.Value));
                //if (dt_201415.Rows.Count > 0)
                //{
                //    checkvaluerequset = dt_201415.Rows[0]["CompanyId"].ToString();
                //   if (checkvaluerequset != "")
                //    {
                //        btnPrint.CommandArgument =checkvaluerequset.ToString() + "," + "2";
                //    }
                //}
                //else
                //{
                //    dt_201415.Dispose();
                //}

                // FormtypeCertificate = hdnFormType.Value;
                // if (FormtypeCertificate == "1")
                //  {
                // imageid = "RegistrationCertificate.aspx?id=" + hdncompId.Value;
                imageid = "RegistrationCertificatePageView.aspx?id=" + hdncompId.Value;
                //imgdraft = "CompanyCertificatePrintPreview.aspx?id=" + hdncompId.Value;
                //imgdraft = "testaspxkkk.aspx?id=" + hdncompId.Value;
                imgdraft = "CompanyCertificatePrintPreviewPage.aspx?id=" + hdncompId.Value;
                // }
                LinkButton btnAmd = (LinkButton)e.Row.FindControl("lnkamedment");
                LinkButton lnkamdement_print = (LinkButton)e.Row.FindControl("lnkamdement_print");
                DecryptEncrypt objDecEnc = new DecryptEncrypt();
                string Company_Id = objDecEnc.Encrypt(hdnCompIdAmd.Value);
                //  if (FormtypeCertificate== "3")
                // {
                imageid_Amedment = "AmedmentReport.aspx?id=" + hdncompId.Value;
                // }
                string Formtypeid = objDecEnc.Encrypt("3");
                string Formtypeid_renewal = objDecEnc.Encrypt("2");
                string BranchId = objDecEnc.Encrypt(Branchid.ToString());
                HiddenField hdnAmedment = (HiddenField)e.Row.FindControl("HiddenAmedment");
                string StrAmedment = hdnAmedment.Value;
                Literal ltlmess = (Literal)e.Row.FindControl("ltlrenmessage");
                LinkButton lnkrenewal = (LinkButton)e.Row.FindControl("lnkRenewal");
                HiddenField hdnRenewalOpenflag = (HiddenField)e.Row.FindControl("HFRenewalOpenFlag");

                //TO OPEN RENEWAL LINK
                if (hdnRenewalOpenflag.Value == "1")
                {
                    lnkrenewal.Visible = true;
                    ltlmess.Visible = false;
                    amendment.Visible = true;
                }
                if (hdnRenewalOpenflag.Value == "1" || hdnRenewalOpenflag.Value == "5" || hdnRenewalOpenflag.Value == "6")
                {
                    lnkrenewal.Visible = true;
                    ltlmess.Visible = false;
                    amendment.Visible = true;
                }

                //TO SHOW RENEWAL PROCESS START MESSAGE
                else if (hdnRenewalOpenflag.Value == "2")
                {
                    ltlmess.Visible = true;
                    amendment.Visible = false;
                    lnkrenewal.Visible = false;
                }
                else if (hdnRenewalOpenflag.Value == "4")
                {
                    // string ss = ltlmess.Text;
                    amendment.Visible = false;
                    ltlmess.Visible = false;
                    lnkrenewal.Visible = false;
                    //ltlmess.Text = "Amendment process started";
                    //  string sd = litamd.Text;
                    litamd.Visible = true;
                }
                else
                {
                    lnkrenewal.Visible = false;
                    ltlmess.Visible = false;
                }

                string imageidAmd = "UnitAmendmentStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid.ToString() + "&b=" + BranchId.ToString();
                //string imageidRenewal = "Online_UnitRenewalStep1.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + BranchId.ToString();
                string imageidRenewal = "Online_Renewal_RA_Confirmation.aspx?c=" + Company_Id.ToString() + "&f=" + Formtypeid_renewal.ToString() + "&b=" + BranchId.ToString();
                lnkamdement_print.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid_Amedment + "');");

                //btnPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imageid + "');");
                btnDraftPrint.Attributes.Add("onclick", "return FunOpenPopup950x600('" + imgdraft + "');");


                // btnAmd.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidAmd + "');");
                //lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");

                if (hdnRenewalOpenflag.Value == "5")
                {
                    btnAmd.Attributes.Add("onclick", "return Confirmrenewal('" + hdncompId.Value + "' , '" + imageidAmd + "');");
                }
                else
                {
                    btnAmd.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidAmd + "');");
                }
                if (hdnRenewalOpenflag.Value == "6")
                {
                    lnkrenewal.Attributes.Add("onclick", "return Confirmamendent('" + hdncompId.Value + "' , '" + imageidRenewal + "');");
                }
                else
                {
                    lnkrenewal.Attributes.Add("onclick", "return FunOpenPopup950x6001('" + imageidRenewal + "');");
                }
                //TO CAPTURE PRINT ACCESS AND ACCEPT BY ADMIN STATUS
                string cmd = btnPrint.CommandArgument.ToString();
                string cmd1 = lnkamdement_print.CommandArgument.ToString();
                string[] cmdargument = cmd.Split(',');
                if (cmdargument[0] != "1")//PRINT ALREADY TAKEN
                {
                    btnDraftPrint.Visible = false;
                    if (cmdargument[1] == "1")//WAITING STATUS
                    {
                        ltlprint.Text = "Waiting";
                        btnPrint.Visible = false;
                        btnRequest.Visible = false;
                    }
                    else if (cmdargument[1] == "2")//REJECTED STATUS
                    {
                        //ltlprint.Text = "Rejected";
                        ltlprint.Visible = false;
                        btnPrint.Visible = false;
                        btnRequest.Visible = true;
                    }
                    else if (cmdargument[1] == "3")//PRINT REQUEST ACCEPTED BY ADMIN
                    {
                        ltlprint.Visible = false;
                        btnPrint.Visible = true;
                        btnRequest.Visible = false;
                    }
                    else//NEW PRINT
                    {
                        btnPrint.Visible = false;
                        btnRequest.Visible = true;
                        ltlprint.Visible = false;
                    }
                }
                else
                {
                    btnPrint.Visible = true;
                    btnDraftPrint.Visible = true;
                    btnRequest.Visible = false;
                    ltlprint.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void rdbMsmeRegtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdbMsmeRegtype.SelectedValue == "1")
        {
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;

            // RegularExpressionValidatorAAdhar.Enabled = true;
            // ValidatorCalloutExtender9.Enabled = true;

            RegularExpressionValidator1.Enabled = false;
            // ValidatorCalloutExtender12.Enabled = false;

            //  Requ_addhar.Enabled = true;
            //  Validator_aadhar_number.Enabled = true;
            RegularExpressionValidator3.Enabled = true;

            RequiredFieldValidator18.Enabled = false;

            RequiredFieldValidator14.Enabled = true;
            //  ValidatorCalloutExtender15.Enabled = false;

            divUAMPAN.Visible = true;
            divUAMPAN1.Visible = true;
            lblUamNameDisplay.Visible = true;
            lblUamiReigistrationNameDisplay.Visible = false;
            RegularExpressionValidator3.Enabled = true;
            RegularExpressionValidator4.Enabled = false;

            //  trMainData.Visible = false;
        }
        else if (rdbMsmeRegtype.SelectedValue == "2")
        {
            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;

            //  RegularExpressionValidatorAAdhar.Enabled = false;
            // ValidatorCalloutExtender9.Enabled = false;

            RegularExpressionValidator1.Enabled = true;
            //  ValidatorCalloutExtender12.Enabled = true;
            RegularExpressionValidator3.Enabled = false;
            RegularExpressionValidator4.Enabled = true;

            //    Requ_addhar.Enabled = false;
            //Validator_aadhar_number.Enabled = false;

            RequiredFieldValidator18.Enabled = true;
            RequiredFieldValidator14.Enabled = false;
            //  ValidatorCalloutExtender15.Enabled = true;

            divUAMPAN.Visible = true;
            divUAMPAN1.Visible = true;
            lblUamNameDisplay.Visible = false;
            lblUamiReigistrationNameDisplay.Visible = true;
            //trMainData.Visible = false;
        }
    }
    protected void gvCertificateDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int optionvar = 0;
        objCompany = new clsCompany();
        try
        {
            if (e.CommandName == "request")//REQUEST FOR TAKING REQUEST
            {

            }
            else if (e.CommandName == "request1")
            {
            }

            else if (e.CommandName == "print")
            {

            }

            //Dupplicate print code start-->
            else if (e.CommandName.Equals("Duplicateprint"))
            {


            }

            //Dupplicate print code end<--
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }


    }


    public DataTable Company_Fetch_Certificate_Search(string UAM_UDYAM, string PAN, string UserId)
    {
        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_Company_Certificate_fetch_Search_Online_New_CFSS", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@UAM_UDYAM", SqlDbType.VarChar, 50).Value = UAM_UDYAM;
            MyAdp.SelectCommand.Parameters.Add("@PAN", SqlDbType.VarChar, 25).Value = PAN;
            MyAdp.SelectCommand.Parameters.Add("@UserId", SqlDbType.VarChar, 50).Value = UserId;
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
}