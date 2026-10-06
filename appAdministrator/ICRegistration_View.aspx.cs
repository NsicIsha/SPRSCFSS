using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Net.Mail;

using System.Web.Services;
using System.Text;
using System.Data.SqlClient;
using System.Web.Script.Services;

[System.Web.Script.Services.ScriptService]
public partial class appAdministrator_ICRegistration_View : System.Web.UI.Page
{

    clsCompany objCompany;
    clsAmendment objAmendment;
 
    int UserId, RoleId, DepartmentId;
    DataTable Mydt;
    int successId;
    string UserFullName;
    string emailid;
    string filepath;
    string expression = string.Empty;
    int urlid = 0;//USED FOR FORM TYPE ID 1 OR 3 FOR REGISTRATION OR AMENDMENT
    int SuccessInspectionComplete, companyid;
    DataRow[] factoryrow;
 
    string MailTo, MailFrom;

    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    string UserEmailID = string.Empty;


    public delegate DataTable RepeaterDelegate(int compid);

    public delegate DataTable GridviewDelgate(int compid, int userid);

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
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            filepath = Server.MapPath(".");
            //filepath = Request.FilePath[1].ToString();
            checkButtonDoubleClick(btnAccept);
            if (RoleId == 8)
            {
                if (Request.QueryString["id"] != null)
                {
                    companyid = Convert.ToInt32(Request.QueryString["id"].ToString());
                    urlid = Convert.ToInt32(Request.QueryString["urlid"].ToString());
                }

                

                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    //mvICReg.SetActiveView(viewGrid);
                    if (urlid == 1)
                    {
                        GridViewDataBind(1);
                    }
                    else if (urlid == 3)
                    {
                        GridViewDataBind(3);
                    }
                    
                    objCompany = new clsCompany();
                    DataSet Myds = new DataSet();
                    Myds = objCompany.Fetch_MasterDoc();

                    //POPULATING CHECKLIST BOX FOR DOCUMENTS
                    ChkListDoc.DataSource = Myds;
                    ChkListDoc.DataTextField = Myds.Tables[0].Columns[1].ToString();
                    ChkListDoc.DataValueField = (Myds.Tables[0].Columns[0].ToString());
                    ChkListDoc.DataBind();

                    //TO BIND REPEATERS
                    FunFillRepeater();

              
                    //lbl.Visible = false;

                }
            }

        }
        catch (Exception ex)
        {
            UserId = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    }

    //BINDING GRIDVIEW
    public void GridViewDataBind(int formtype)
    {
        try
        {
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();

            //ADDING FUNCTIONS TO DELEGATE
            GridviewDelgate grdDelegateReg = new GridviewDelgate(objCompany.Company_ICompleteRegistration_Dataset);
            GridviewDelgate grdDelegateAmd=new GridviewDelgate(objAmendment.CompanyAmendment_ICompleteAmendment_FetchByCompanyId);    
            

            mvICReg.SetActiveView(viewGrid);
            //FOR REGISTRATION
            if (formtype == 1)
            {
                gvICRegistration.DataSource = objCompany.Company_ICompleteRegistration_Dataset(companyid, UserId);
                //CALLING FUNCTION THROUGH DELEGATE
                DataTable dtdelegate = new DataTable();
                dtdelegate = grdDelegateReg(companyid, UserId);
                gvICRegistration.DataSource = grdDelegateReg(companyid, UserId);
               // branchid.Value = UserId.ToString();
                hdn_enterprise.Value = dtdelegate.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
                gvICRegistration.DataBind();

                F_Check_enterprise();

            }
                //FOR AMENDMENT
            else if (formtype == 3)
            {
                gvICRegistration.DataSource = objAmendment.CompanyAmendment_ICompleteAmendment_FetchByCompanyId(UserId, companyid);
                gvICRegistration.DataSource = grdDelegateAmd(companyid, UserId);
                DataTable dtdelegate = new DataTable();
                dtdelegate = grdDelegateAmd(companyid, UserId);
                hdn_enterprise.Value = dtdelegate.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
               // branchid.Value = UserId.ToString();
                gvICRegistration.DataBind();

                DataTable dtcompanyprofilecode = new DataTable();
                dtcompanyprofilecode = objAmendment.Company_ProfileHscodeAmendment(companyid);
                txt_hscode1.Text = dtcompanyprofilecode.Rows[0]["hscode1"].ToString();
                txt_hscode2.Text = dtcompanyprofilecode.Rows[0]["hscode2"].ToString();
                txt_hscode3.Text = dtcompanyprofilecode.Rows[0]["hscode3"].ToString();
                txt_hscode4.Text = dtcompanyprofilecode.Rows[0]["hscode4"].ToString();

                F_Check_enterprise();

                if (hdn_enterprise.Value != "3")
                {
                    //rdo_option1.Visible = false;
                    //rdo_option2.Visible = false;
                    //rdo_option3.Visible = false;
                    //rdo_option4.Visible = false;
                }
                else
                {
                    //rdo_option1.Visible = true;
                    //rdo_option2.Visible = true;
                    //rdo_option3.Visible = true;
                    //rdo_option4.Visible = true;

                    rdo_option1.SelectedValue = dtcompanyprofilecode.Rows[0]["type1"].ToString();
                    rdo_option2.SelectedValue = dtcompanyprofilecode.Rows[0]["type2"].ToString();
                    rdo_option3.SelectedValue = dtcompanyprofilecode.Rows[0]["type3"].ToString();
                    rdo_option4.SelectedValue = dtcompanyprofilecode.Rows[0]["type4"].ToString();

                }

            }

            if (gvICRegistration.Rows.Count == 0)
            {
                lblMessage.Text = "No company found!";
            }
            else
                lblMessage.Text = "";

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
   
    }


    public void sendmailbranchaccount()
    {
        try
        {
            string ToMail, FromMail, ccMail, bccMail, subject, bodyMail;
            clsMasterUser objUser = new clsMasterUser();
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            clsMasterInspectionAgency objAgency = new clsMasterInspectionAgency();

            DataSet Myds = new DataSet();
            //TO BRANCH ACCOUNT MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(3);
            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            string str = bodyMail.Replace("$$CompanyName$$", objCompany.Company_Detail_Fetch_ById(companyid).Rows[0]["fGPtbl_Company_Name"].ToString());
            bodyMail = str.Replace("$$Branchname$$", objAgency.Trans_InspectionagecnyBranch_Fetch_DataTableByUserId(UserId).Rows[0]["BranchName"].ToString());

            //TO FETCH BRANCH ACCOUNT MAILER
            Myds = objUser.MasterUser_Fetch_DataSet(UserId);
            ToMail = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();


            //SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void F_Check_enterprise()
    {
        var val = hdn_enterprise.Value;
        if (val == "0")
        {
            hdn_enterprise.Value = "1";
        }
        if (val == "1" || val == "0")
        {
            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile HS Code";
        }
        else if (val == "2")
        {

            rdo_option1.Visible = false;
            rdo_option2.Visible = false;
            rdo_option3.Visible = false;
            rdo_option4.Visible = false;
            lbl_heading.Text = "Profile NPC Code";
        }
        else
        {
            rdo_option1.Visible = true;
            rdo_option2.Visible = true;
            rdo_option3.Visible = true;
            rdo_option4.Visible = true;
            lbl_heading.Text = "Profile HS/NPC Code";
        }
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        // TO SHOW REPEATER VIEW
          if (Request.QueryString["id"] != null)
                {
                    companyid = Convert.ToInt32(Request.QueryString["id"].ToString());
                    urlid = Convert.ToInt32(Request.QueryString["urlid"].ToString());
                }

        objCompany = new clsCompany();
        DataTable dtenterpriseval = new DataTable();
        dtenterpriseval = objCompany.Fetch_Unit_Enterprise(companyid.ToString());
        string enterpriseval = dtenterpriseval.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
        hdn_enterprise.Value = enterpriseval;
        F_Check_enterprise();
     
      
        mvICReg.SetActiveView(viewRepeater);
        sendmail(MailFrom, MailTo, bccMail, ccMail, subject, bodyMail);


    }


    protected void gvICRegistration_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // FOR VIEWING REPORT OF INSPECTION AGENCY
                HyperLink hlname = (HyperLink)e.Row.FindControl("HLName");
                HiddenField hfpath = (HiddenField)e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname = (HiddenField)e.Row.FindControl("HiddenFieldname");
                //HiddenField hfcompany = (HiddenField)e.Row.FindControl("HiddenField1");

                //HiddenFieldCompany.Value = hfcompany.Value;

                //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                hlname.NavigateUrl = hfpath.Value.ToString() + hfname.Value.ToString();
                //hlname.NavigateUrl = "~/Document/Upload/Registration/Mist Valley Marchants-HP.doc";


                //HiddenField hfCompanyId = (HiddenField)e.Row.FindControl("HiddenfieldCompanyId");
                //hiddenfieldCompanyId.Value = hfCompanyId.Value;

            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void BackButton_Click(object sender, EventArgs e)
    {
        // REDIRECTING TO MAIN PAGE
        if (urlid == 1)
        {
            Response.Redirect("ICRegistration.aspx?urlid=1", false);
        }
        else if (urlid == 3)
        {
            Response.Redirect("ICRegistration.aspx?urlid=3", false);
        }
    }

    //BINDING STORE REPEATER
    private void FunFillRepeater()
    {
        //FILL REPEATER++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        try
        {
            // BINDING STORE REPEATER
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();
            Mydt = new DataTable();

            //DECLARING MULTIPLE DELEGATES
            RepeaterDelegate repeatdelreg1,repeatdelreg2,repeatdelamd1,repeatdelamd2;

            GridviewDelgate grdDelegateReg = new GridviewDelgate(objCompany.Company_ICompleteRegistration_Dataset);
            //System.Data.DataTable Dt = new System.Data.DataTable();

            if (urlid == 1)
            {
                //ASSIGNING FUNCTION TO DELEGATE
                repeatdelreg1 = objCompany.Company_Fetch_StoreByCompanyId;
                //Mydt = objCompany.Company_Fetch_StoreByCompanyId(companyid);
                //Dt = objCompany.Company_Fetch_StoreByCompanyId(45);
                Mydt = repeatdelreg1(companyid);
                StoreRepeaterBranch.DataSource = Mydt;
                StoreRepeaterBranch.DataBind();

                //ASSIGNING FUNCTION TO DELEGATE
                repeatdelreg2=objCompany.Company_Fetch_FactoryLocation;

                //Mydt = objCompany.Company_Fetch_FactoryLocation(companyid);
                //CALLING FUNCTION THROUGH DELEGATE
                Mydt=repeatdelreg2(companyid);
               
                repeaterPmps.DataSource = Mydt;
                repeaterPmps.DataBind();

               
            }

            else if (urlid == 3)
            {

                Mydt = objAmendment.CompanyAmendment_Fetch_UnitChanged_ForInspectionAgency_Datatable(companyid);
                //Mydt = repeatdelamd2(companyid);
                hdn_enterprise.Value = Mydt.Rows[0]["fGPtbl_CUnit_Enterprise"].ToString();
                repeaterPmps.DataSource = Mydt;
                repeaterPmps.DataBind();

                repeatdelamd1 = objAmendment.CompanyAmendment_Fetch_StoreByCompanyId_Inspectionpassed;
                //Mydt = objAmendment.Company_Fetch_StoreByCompanyId(companyid);
                //Dt = objCompany.Company_Fetch_StoreByCompanyId(45);
              
                    Mydt = repeatdelamd1(companyid);
                    if (Mydt.Rows.Count > 0)
                    {
                    // mvICReg.Visible = true;
                        lbl_show_message_text.Text = "";
                        listofdocument.Visible = true;
                        permonthshift.Visible = true;
                        ChkListDoc.Visible = true;
                    StoreRepeaterBranch.DataSource = Mydt;
                    StoreRepeaterBranch.DataBind();

                    //ASSIGNING FUNCTION TO DELEGATE
                    // repeatdelamd2 = objAmendment.CompanyAmendment_Fetch_UnitChanged_Datatable;
                   
                }
                else
                {

                    lbl_show_message_text.Text = "<br><br><br><b>" + "There are no changes in the Stores. Please click on Submit button to proceed with the Amendment." + "</b>";
                    listofdocument.Visible = false;
                    permonthshift.Visible = false;
                    ChkListDoc.Visible = false;
                    //mvICReg.Visible = false;
                   
                   // lbl_message.Text = "<b>" + "There are No store" + "</b>";

                }
            }

        }
        catch { }
       
    }


    

    public void btnSubmit_Click1(object sender, EventArgs e)
    {
        try
        {

            


            objCompany = new clsCompany();
           
            objAmendment = new clsAmendment();
            string documentstr = string.Empty, docitem = string.Empty, docname = string.Empty;

            //ListItem li = new ListItem();
            // int chekboxcheked;
            foreach (ListItem item in ChkListDoc.Items)
            {
                if (item.Selected)
                {
                    // TO SEND DOCUMENT SUBMITTED NAME THROUGH STRING
                    docitem = item.Value;
                    docname = item.Text;
                    documentstr += docitem + "," + docname + "@";

                    //SuccessInspectionComplete = objCompany.Company_WaitingCompany_Insert_DocumentSubmitted(chkid, Convert.ToString(ChekboxName.ToString()), companyid, 1, UserId, UserId);

                }
            }

            //-=====================================================
            if (documentstr != null)
            {
                // TO INSERT DOCUMENT SUBMITTED
                if (urlid == 1)//FOR REGISTRATION
                {
               SuccessInspectionComplete = objCompany.Company_WaitingCompany_Insert_DocumentSubmitted(documentstr, companyid, 1, UserId);
                }
                else if (urlid == 3)//FOR AMENDMENT
                {
                 SuccessInspectionComplete = objCompany.Company_WaitingCompany_Insert_DocumentSubmitted(documentstr, companyid, 3, UserId);
                }
            }
            

            TextBox description, specification, brand, rating, production, shift, quality, quantity, storecode,nic;
            TextBox Remarks_Manufacturing;
            string strRemarks_Man = "";


            DropDownList ddlhscode;
           
            HiddenField hfstoreid, hdnhscode, hdnnpcorhs;
            string allvalue = string.Empty;
            //DICTIONARY TO ADD ALL ITEMS OF REPEATER
            Dictionary<int, string> vall = new Dictionary<int, string>();
            int key = 0;

            foreach (RepeaterItem item in StoreRepeaterBranch.Items)
            {

                //Hscode = (DropDownList)(item.FindControl("ddlHSCode"));

                hfstoreid = (HiddenField)(item.FindControl("hfStoreid"));
                description = (TextBox)(item.FindControl("txtDescription"));
                specification = (TextBox)(item.FindControl("txtBis"));
                brand = (TextBox)(item.FindControl("txtModel"));
                rating = (TextBox)(item.FindControl("txtLimiting"));
                production = (TextBox)(item.FindControl("txtAnnual"));
                shift = (TextBox)(item.FindControl("txtShift"));
                quality = (TextBox)(item.FindControl("txtQuality"));
                quantity = (TextBox)(item.FindControl("txtQuantity"));
               // ddlhscode = (DropDownList)(item.FindControl("ddlHSCode"));
                storecode = (TextBox)(item.FindControl("txt_serial_no"));
                nic = (TextBox)(item.FindControl("txtNICcode"));
                Remarks_Manufacturing = (TextBox)(item.FindControl("txtRemarks_Manufacturing"));

                if(Remarks_Manufacturing.Text !="")
                {
                    strRemarks_Man = Remarks_Manufacturing.Text;
                }
                else
                {
                    strRemarks_Man = "";
                }
               

                hdnhscode = (HiddenField)(item.FindControl("hdn_hstextbox"));
                string hscode = hdnhscode.Value;
                hdnnpcorhs = (HiddenField)(item.FindControl("hdn_hsornpc"));
                string hsornpc = "0";
                if (hdn_enterprise.Value == "3")
                {
                    if (hdnnpcorhs.Value != "")
                    {
                        hsornpc = hdnnpcorhs.Value;
                    }
                }
                else
                {
                    hsornpc = hdn_enterprise.Value;
                }
                //if (ddlhscode.SelectedIndex > 0)
                //{
                //    hscode = ddlhscode.SelectedValue.ToString();
                //}


                // CONCATENATE ALL VALUES IN A STRING
                //DELIMITER CHANGES FROM ',' TO '^'
                allvalue = hfstoreid.Value + "^" + description.Text.Trim() + "^"
                    + specification.Text.Trim() + "^" + brand.Text.Trim() + "^" + rating.Text.Trim()
                    + "^" + production.Text.Trim() + "^" + shift.Text.Trim() + "^" + quality.Text.Trim()
                    + "^" + quantity.Text.Trim() + "^" + storecode.Text.Trim() + "^" + "^" + hscode + "^" + hsornpc + "^"+ nic.Text.Trim() +"^" + strRemarks_Man + "@";

                vall.Add(key, allvalue);
                key++;
            }
            allvalue = string.Empty;
            HiddenField Hfunitidpmps;
            RadioButtonList  rdlist;
            //DICTIONARY TO ADD ALL ITEMS OF REPEATER
            Dictionary<int, string> vallpmps = new Dictionary<int, string>();
            key = 0;

            foreach (RepeaterItem item in repeaterPmps.Items)
            {

                //Hscode = (DropDownList)(item.FindControl("ddlHSCode"));

                Hfunitidpmps = (HiddenField)(item.FindControl("hfUnitidpmps"));
              
                rdlist = (RadioButtonList)(item.FindControl("radlist"));

                // CONCATENATE CHECKBOX VALUES IN A STRING
                if (rdlist.SelectedItem.Value == "Ps")
                {
                    allvalue = Hfunitidpmps.Value + ",ps@";
                }
                else if (rdlist.SelectedItem.Value  == "Pmps")
                {
                    allvalue = Hfunitidpmps.Value + ",pmps@";
                }
                else if (rdlist.SelectedItem.Value == "PA")
                {
                    allvalue = Hfunitidpmps.Value + ",PA@";
                }
                else if (rdlist.SelectedItem.Value.ToUpper() == "PM")
                {
                    allvalue = Hfunitidpmps.Value + ",PM@";
                }

                vallpmps.Add(key, allvalue);
                key++;
            }
            string comid = "0";
            StringBuilder str = new StringBuilder();
            if (Request.QueryString.HasKeys())
            {
                comid = Request.QueryString["id"].ToString();
                str.Append("<CompanyHSData>");

                //if (Request.Form[txt_hscode1.UniqueID] != "")
                //{
                    str.Append("<Company>");
                    str.Append("<ID>1</ID>");
                    str.Append("<HSCODE>" + Request.Form[txt_hscode1.UniqueID] + "</HSCODE>");
                    if (hdn_enterprise.Value == "3")
                    {
                        str.Append("<HSTYPE>" + rdo_option1.SelectedValue + "</HSTYPE>");
                    }
                    else
                    {
                        str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                    }
                    str.Append("</Company>");
               // }

                str.Append("<Company>");
                str.Append("<ID>2</ID>");
                str.Append("<HSCODE>" + Request.Form[txt_hscode2.UniqueID] + "</HSCODE>");
                if (hdn_enterprise.Value == "3")
                {
                    str.Append("<HSTYPE>" + rdo_option2.SelectedValue + "</HSTYPE>");
                }
                else
                {
                    str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                }
                str.Append("</Company>");

                str.Append("<Company>");
                str.Append("<ID>3</ID>");
                str.Append("<HSCODE>" + Request.Form[txt_hscode3.UniqueID] + "</HSCODE>");
                if (hdn_enterprise.Value == "3")
                {
                    str.Append("<HSTYPE>" + rdo_option3.SelectedValue + "</HSTYPE>");
                }
                else
                {
                    str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                }
                str.Append("</Company>");

                str.Append("<Company>");
                str.Append("<ID>4</ID>");
                str.Append("<HSCODE>" + Request.Form[txt_hscode4.UniqueID] + "</HSCODE>");
                if (hdn_enterprise.Value == "3")
                {
                    str.Append("<HSTYPE>" + rdo_option4.SelectedValue + "</HSTYPE>");
                }
                else
                {
                    str.Append("<HSTYPE>" + hdn_enterprise.Value + "</HSTYPE>");
                }
                str.Append("</Company>");

                str.Append("</CompanyHSData>");

            }

            if (vall.Values != null)
            {
                if (urlid == 1)//FOR REGISTRATION
                {
                 //objCompany.Store_Insert(vall.Values, vallpmps.Values, 2, UserId);
                    Store_Insert_Modified(vall.Values, vallpmps.Values, 2, UserId, str.ToString(), comid,"1", strRemarks_Man);
                    successId = objCompany.Company_ICompleteRegistration_Accept(companyid, UserId);
                }
                else if (urlid == 3)//FOR AMENDMENT
                {
               // objAmendment.Store_Insert(vall.Values, vallpmps.Values, 2, UserId);
                Store_Insert_Modified(vall.Values, vallpmps.Values, 2, UserId, str.ToString(), comid, "3", strRemarks_Man);
                  successId = objAmendment.CompanyAmendment_ICompleteAmendment_Accept(companyid, UserId);
                }
            }

            //=======================================================

            
            
            
            if (successId == 2)
            {
                /* Mail code commented */
               sendmailbranchaccount();
                Session["icregAction"] = "Status changed successfully!";
                //REDIRECT WITH URLID OPTION 1:REGISTRATION 2:AMENDMENT 
                if(urlid==1)
                {
                    Response.Redirect("ICRegistration.aspx?urlid=1", false);
                }
                else if(urlid==3)
                {
                    Response.Redirect("ICRegistration.aspx?urlid=3", false);
                }
            }
            else
            {
                Session["icregAction"] = "Server is busy!";
                //REDIRECT WITH URLID OPTION 1:REGISTRATION 2:AMENDMENT
                 if(urlid==1)
                {
                    Response.Redirect("ICRegistration.aspx?urlid=1", false);
                }
                else if(urlid==3)
                {
                    Response.Redirect("ICRegistration.aspx?urlid=3", false);
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        //FOR AMENDMENT
        //GridViewDataBind(3);

    }



    protected void repeaterPmps_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      // BINDING LITERAL TO FACTORY NAME
      /*  if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            Literal ltlfactory = (Literal)e.Item.FindControl("ltlFactory");
            HiddenField hfunitidpmps = (HiddenField)e.Item.FindControl("hfUnitidpmps");

            expression = "fGPtbl_CUnit_Id=" + hfunitidpmps.Value;

            factoryrow = Mydt.Select(expression);

            ltlfactory.Text = factoryrow[0][1].ToString();
        }*/
    }

    protected void ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //{
        //    Label activeLabel = (Label)e.Item.FindControl("lblMarks3");
        //    string s = activeLabel.Text;
        //    if (s == "0")
        //    {
        //        activeLabel.Visible = false;
        //        marks.Visible = false;
        //    }
        //    else
        //    {
        //        activeLabel.Visible = true;
        //        marks.Visible = true;
        //    }
       // }
    }

    protected void StoreRepeaterBranch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        System.Data.DataTable dt = new System.Data.DataTable();
        dt = clsDDL.ddlHscode();
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType== ListItemType.AlternatingItem )
        {
           // DropDownList ddlhscode = (DropDownList)e.Item.FindControl("ddlHSCode");

            TextBox description, specification, brand, rating, production, shift, quality, quantity, storecode;
            TextBox Remarks_Manufacturing;
            RequiredFieldValidator rfdRemarks;
           // Label lblRemarks;

            DropDownList ddlhscode;
            HiddenField hdnEnterprise = (HiddenField)e.Item.FindControl("hdnEnterprise");
            if (hdnEnterprise.Value == "2")
            {
                description = (TextBox)(e.Item.FindControl("txtDescription"));
                specification = (TextBox)(e.Item .FindControl("txtBis"));
                brand = (TextBox)(e.Item.FindControl("txtModel"));
                rating = (TextBox)(e.Item.FindControl("txtLimiting"));
                production = (TextBox)(e.Item.FindControl("txtAnnual"));
                shift = (TextBox)(e.Item.FindControl("txtShift"));
                quality = (TextBox)(e.Item.FindControl("txtQuality"));
                quantity = (TextBox)(e.Item.FindControl("txtQuantity"));


                Remarks_Manufacturing= (TextBox)(e.Item.FindControl("txtRemarks_Manufacturing"));

                Remarks_Manufacturing.Visible = false;
                rfdRemarks = (RequiredFieldValidator)(e.Item.FindControl("RequiredFieldValidator141"));
                rfdRemarks.Enabled = false;
                
                
                //lblRemarks = (Label)(e.Item.FindControl("lblRemarks"));
                //lblRemarks.Visible = false;

                brand.ReadOnly=true;  
                rating.ReadOnly=true; 
                production.ReadOnly=true; 
                shift.ReadOnly=true; 
                quality .ReadOnly=true; 
                
            }

            else
            {
                Remarks_Manufacturing = (TextBox)(e.Item.FindControl("txtRemarks_Manufacturing"));

                Remarks_Manufacturing.Visible = true;
                rfdRemarks = (RequiredFieldValidator)(e.Item.FindControl("RequiredFieldValidator141"));
                rfdRemarks.Enabled = true;

                //lblRemarks = (Label)(e.Item.FindControl("lblRemarks"));
                //lblRemarks.Visible = true;

            }
            //System.Data.DataTable dt = new System.Data.DataTable();
            //dt = clsDDL.ddlHscode();

           // ddlhscode.DataSource = dt;
         //   ddlhscode.DataTextField = dt.Columns["fGPtbl_HS_HScode"].ToString();
          //  ddlhscode.DataValueField = dt.Columns["fGPtbl_HS_HScode"].ToString();
           // ddlhscode.DataBind();
          //  ddlhscode.Items.Insert(0, new ListItem("--Select--", ""));

           // dt.Clear();
          //  dt.Dispose();

           // ddlHscode
            RadioButtonList rdo = (RadioButtonList)e.Item.FindControl("rdooption_repeater");
            //Button btn = (Button)e.Item.FindControl("btn_search_repeater");
            //btn.Attributes.Add("onClick", "javascript:ShowPopup();");

            if (hdn_enterprise.Value != "3")
            {
                rdo.Visible = false;

            }
            else
            {
                //if (hdn_enterprise.Value == "1")
                //{
                //    rdo.SelectedValue = "1";
                //}
                //else
                //{
                //    rdo.SelectedValue = "2";
                //}
                rdo.Visible = true;
            }

        }


    }


    // send Mail.............................................................
    public void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {

        try
        {
            string ToMail, FromMail;
            clsMasterUser objUser = new clsMasterUser();
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            clsMasterInspectionAgency objAgency = new clsMasterInspectionAgency();

            DataSet Myds = new DataSet();
            //TO BRANCH ACCOUNT MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(3);  // changes hear
            FromMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            string str = bodyMail.Replace("$$CompanyName$$", objCompany.Company_Detail_Fetch_ById(companyid).Rows[0]["fGPtbl_Company_Name"].ToString());
            bodyMail = str.Replace("$$Branchname$$", objAgency.Trans_InspectionagecnyBranch_Fetch_DataTableByUserId(UserId).Rows[0]["BranchName"].ToString());
            string MailName = string.Empty;
            string MailFromName = string.Empty;
            //TO FETCH BRANCH ACCOUNT MAILER
            DataTable dsMailFrom = new DataTable();
            string Branchid = string.Empty;
            dsMailFrom = objAgency.Trans_InspectionagecnyBranch_Fetch_DataTableByUserId(UserId);
            if (dsMailFrom.Rows.Count > 0)
            {
                MailFromName = dsMailFrom.Rows[0]["BranchName"].ToString();
                Branchid = dsMailFrom.Rows[0]["fGPtbl_MB_Id"].ToString();
            }
            else
            {
                MailFromName = "";
                dsMailFrom.Dispose();
            }

            //TO FETCH BRANCH ACCOUNT RECORD
            Myds = objUser.MasterUser_FetchUser_ByBranchIdRole(Convert.ToInt32(Branchid), 4); 
            if (Myds.Tables[0].Rows.Count > 0)
            {

                MailName = Myds.Tables[0].Rows[0]["fGPtbl_MU_FName"].ToString();
            }
            else
            {
                MailName = "";
                Myds.Dispose();
            }


            // ToMail = "anupam.sen@nagsoft.com";  
            //Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
            //    MailTo = ToMail;
            MailFrom = FromMail;//"nsicclient@nsic.co.in"; 

            subject = subject;



            DataTable dtCompanyname = new DataTable();
            dtCompanyname = objCompany.Company_Detail_Fetch_ById(companyid);
            string CompanyName = string.Empty;
            if (dtCompanyname.Rows.Count > 0)
            {
                CompanyName = dtCompanyname.Rows[0]["fGPtbl_Company_Name"].ToString();
            }
            else
            {
                CompanyName = "";
                dtCompanyname.Dispose();
            }
            DataSet dsuser = new DataSet();
            string MailUser = string.Empty;
            dsuser = objUser.MasterUser_Fetch_DataSet(UserId);
            if (dsuser.Tables[0].Rows.Count > 0)
            {
                MailUser = dsuser.Tables[0].Rows[0]["fGPtbl_MU_Name"].ToString();
                ToMail = Myds.Tables[0].Rows[0]["fGPtbl_MU_Email"].ToString();
                MailTo = ToMail;
            }
            else
            {
                MailUser = string.Empty;
                dsuser.Dispose();
            }

            bodyMail = "Dear" + ' ' + MailName + "<br/><br>" + bodyMail;// "Please set the Monetary Limit(s) for the company" + ' ' +"(" + CompanyName +")" + "<br><br>" + "Thanks" + "<br><br>" + "Regards" + "<br/><br>" + MailUser;

            //SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);

        }



        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];



            //"nsicclient@nsic.co.in";
            MailMessage message = new MailMessage();
            message.From = new MailAddress(MailFrom);  // MailFrom
            // Set the recepient address of the mail message

            message.To.Add(new MailAddress(MailTo));

            // Check if the bcc value is null or an empty string
            if ((bccMail != null) && (bccMail != string.Empty))
            {
                // Set the Bcc address of the mail message
                message.Bcc.Add(new MailAddress(bccMail));
            }      // Check if the cc value is null or an empty value
            if ((ccMail != null) && (ccMail != string.Empty))
            {
                // Set the CC address of the mail message
                message.CC.Add(new MailAddress(ccMail));
            }       // Set the subject of the mail message
            message.Subject = subject;
            // Set the body of the mail message
            message.Body = bodyMail;


            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;

            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);

            smtpClient.Send(message);

            //INSERT INTO SENT MAILER
            clsDDL.Insert_MailDetails(MailTo, 8, 1, MailFrom, 6, 2, subject, bodyMail, 2);


        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('Server Busy...........'); window.close();</script>");
        }
    }
private void Store_Insert_Modified(Dictionary<int, string>.ValueCollection vall1, Dictionary<int, string>.ValueCollection vallpmps, int Insertupdate, int userid, string str, string companyID,string formtypeid, string Remarks_Man)
    {
        SqlCommand MySqlComm;
        MySqlComm = null;
        try
        {
            string allval = string.Empty, allvalpmps = string.Empty;//, all1;
            foreach (string s in vall1)
            {
                allval += s;
            }
            //all1 = allval;

            foreach (string s1 in vallpmps)
            {
                allvalpmps += s1;
            }
         
            if (formtypeid == "1")
            {
                MySqlComm = new SqlCommand("GPsp_Company_Insert_Store_Step3_All_Modified_04Oct22", DBManager.ConnectToDB());
            }
            else if (formtypeid == "3")
            {
                MySqlComm = new SqlCommand("GPsp_CompanyAmendment_Insert_Store_Step3_All", DBManager.ConnectToDB());
            }
            
            MySqlComm.CommandType = CommandType.StoredProcedure;

            MySqlComm.Parameters.Add("@AllStores", SqlDbType.Text).Value = allval;
            MySqlComm.Parameters.Add("@AllPmps", SqlDbType.VarChar, 1000).Value = allvalpmps;
            MySqlComm.Parameters.Add("@InsertUpdateFlag", SqlDbType.Int).Value = Insertupdate;
            MySqlComm.Parameters.Add("@UserId", SqlDbType.Int).Value = userid;
            MySqlComm.Parameters.Add("@xmldata", SqlDbType.Xml).Value = str;
            MySqlComm.Parameters.Add("@CompanyID", SqlDbType.Int).Value = Convert.ToInt32(companyID);

            MySqlComm.Parameters.Add("@Remarks_Man", SqlDbType.VarChar, 12000).Value = Remarks_Man;

            

            SqlParameter outVal = new SqlParameter("@ErrorId", SqlDbType.Int);
            outVal.Direction = ParameterDirection.Output;
            MySqlComm.Parameters.Add(outVal);

            MySqlComm.ExecuteNonQuery();

            int errorid = Convert.ToInt32(outVal.Value);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
        //    MySqlComm.Dispose();
        }

    }

    public void checkButtonDoubleClick(Button button)
    {
        System.Text.StringBuilder sbValid = new System.Text.StringBuilder();
        sbValid.Append("if (typeof(Page_ClientValidate) == 'function') { ");
        sbValid.Append("if (Page_ClientValidate() == false) { return false; }} ");
        sbValid.Append("this.value = 'Please Wait....';");
        sbValid.Append("this.disabled = true;");
        sbValid.Append(this.Page.ClientScript.GetPostBackEventReference(button, ""));
        sbValid.Append(";");
        button.Attributes.Add("onclick", sbValid.ToString());
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static HsCodeList[] Bind_Searchgrid(string searchitem, string codetype)
    {
        DataTable dt = new DataTable();
        List<HsCodeList> details = new List<HsCodeList>();
        //    clsCompanyBacklog objback = new clsCompanyBacklog();
        string connectionstr = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
        SqlConnection con = new SqlConnection(connectionstr);
        try
        {
            SqlCommand cmd = new SqlCommand("GP_sp_searchHSCode", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Product", SqlDbType.VarChar, 5000).Value = searchitem == "" ? null : searchitem;
            cmd.Parameters.Add("@tppe", SqlDbType.Int).Value = codetype == "" ? null : codetype;
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dt);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dtrow in dt.Rows)
                    {
                        HsCodeList hs = new HsCodeList();
                        hs.HSID = dtrow["Id"].ToString();
                        hs.HSCode = dtrow["Productcode"].ToString();
                        hs.HSCodedescription = dtrow["Productdescription"].ToString();
                        details.Add(hs);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
        return details.ToArray();

    }


    public class HsCodeList
    {
        public string HSID { get; set; }
        public string HSCode { get; set; }
        public string HSCodedescription { get; set; }
    }
   
}

