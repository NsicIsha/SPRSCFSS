using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;

public partial class appAdministrator_WaitingCompanies_View : System.Web.UI.Page
{
    clsCompany objCompany;
//    clsAmendment objAmendment;
    DataSet Myds;
    int UserId, RoleId, DepartmentId, companyId;
  //  int successId, urlId = 0, amendmentid;
    int Unitid,urlId=1;
    string filepath;
    string UserEmailID = string.Empty;
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
            //DepartmentId = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            //UserFullName = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).ToString();
            //UserEmailID = Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).ToString();
            //filepath=Server.MapPath(".");
            filepath = Request.FilePath[1].ToString();
            if (RoleId == 8)
            {
                Showdata(73, 1);
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
            UserEmailID = "";
        }
        //End Read Cookies+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }

    //TO SHOW INFORMATION ON LABEL
    public void Showdata(int Companyid, int formtypeId)
    {
        try
        {
            
            Myds=new DataSet();

            DataTable Mydt, Mydt2, Mydt3 = new DataTable();

            if (formtypeId == 1)
            {
                objCompany = new clsCompany();

                //TO FETCH DATA BY UNIT ID
                Myds = objCompany.Company_UnitDetails_Fetch_ByCompanyId(Companyid);

            }  

                Mydt = Myds.Tables[0];

                Mydt2 = Myds.Tables[2];
                Mydt3 = Myds.Tables[3];
            //}
            /*else if (formtypeId == 3)
            {
                objAmendment = new clsAmendment();
                Mydt = objAmendment.CompanyAmendment_WaitingCompany_FetchByCompanyId_Dataset(Companyid).Tables[0];
            }*/

            //GETTING UNIT ID
            Unitid = Convert.ToInt32(Myds.Tables[0].Rows[0]["fGPtbl_CUnit_Id"]);

            //GETTING FORMTYPE ID
            //Formtypeid=Convert.ToInt32(Mydt.Rows[0]["fGPtbl_CUnit_FormtypeId"]);


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
                gvDocumentDetail.DataSource = objCompany.Company_WaitingCompany_Registration_Documents_FetchByUnitid_Dataset(Unitid, formtypeId);
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


            //TO SHOW IN LABEL
            lblGpref.Text = Mydt.Rows[0]["fGPtbl_Company_GPReferenceNo"].ToString();
            lblPAN.Text = Mydt.Rows[0]["fGPtbl_Company_PAN"].ToString();
            lblCompany.Text = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();
            lblAddress.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Address"].ToString();
            lblPin.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Zip"].ToString();
            lblState.Text = Mydt.Rows[0]["CompanyState"].ToString();
            lblCity.Text = Mydt.Rows[0]["CompanyCity"].ToString();
            lblPhone.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Phone"].ToString();
            lblFax.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Fax"].ToString();
            lblEmail.Text = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString();
            //lblBranchname.Text = Mydt.Rows[0]["fGPtbl_MB_Name"].ToString();
           /* lblUAddress.Text = Mydt.Rows[0]["fGPtbl_CUnit_1B_Factory_Address"].ToString();
            lblUPin.Text = Mydt.Rows[0]["fGPtbl_CUnit_1B_Factory_Zip"].ToString();
            lblUState.Text = Mydt.Rows[0]["CUnitState"].ToString();
            lblUCity.Text = Mydt.Rows[0]["CUnitCity"].ToString();
            lblUPhone.Text = Mydt.Rows[0]["fGPtbl_CUnit_1B_Factory_Phone"].ToString();
            lblUFax.Text = Mydt.Rows[0]["fGPtbl_CUnit_1B_Factory_Fax"].ToString();
            lblUEmail.Text = Mydt.Rows[0]["fGPtbl_CUnit_1B_Factory_Email"].ToString();
            * */
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
            /*
            lbl5a1Address.Text = Mydt.Rows[0]["fGPtbl_CUnit_5A1"].ToString();
            lbl5a2Phone.Text = Mydt.Rows[0]["fGPtbl_CUnit_5A2"].ToString();
            lbl5baPower.Text = Mydt.Rows[0]["fGPtbl_CUnit_5BA"].ToString();
            lbl5bb1Horsepower.Text = Mydt.Rows[0]["fGPtbl_CUnit_5BB1"].ToString();
            lbl5bb2Installed.Text = Mydt.Rows[0]["fGPtbl_CUnit_5BB2"].ToString();
            lbl5cCovered.Text = Mydt.Rows[0]["fGPtbl_CUnit_5C"].ToString();
            lbl6a1Land.Text = Mydt.Rows[0]["fGPtbl_CUnit_6A1"].ToString();
            lbl6a2Plant.Text = Mydt.Rows[0]["fGPtbl_CUnit_6A2"].ToString();
            lbl6bCurrentasset.Text = Mydt.Rows[0]["fGPtbl_CUnit_6B"].ToString();
            lbl7amachinedetails.Text = Mydt.Rows[0]["fGPtbl_CUnit_7A"].ToString();
            lbl7bStores.Text = Mydt.Rows[0]["fGPtbl_CUnit_7B"].ToString();
            lbl7cTechnic.Text = Mydt.Rows[0]["fGPtbl_CUnit_7C"].ToString();
            lbl7dQuality.Text = Mydt.Rows[0]["fGPtbl_CUnit_7D"].ToString();
            lbl7eTesting.Text = Mydt.Rows[0]["fGPtbl_CUnit_7E"].ToString();
             * */
            lbl8Typetest.Text = Mydt.Rows[0]["fGPtbl_CUnit_8"].ToString();
            lbl9Serviceprod.Text = Mydt.Rows[0]["fGPtbl_CUnit_9"].ToString();
            lbl10Threeyrs.Text = Mydt.Rows[0]["fGPtbl_CUnit_10"].ToString();
            lbl11Banker.Text = Mydt.Rows[0]["fGPtbl_CUnit_11"].ToString();
            lbl12aBis.Text = Mydt.Rows[0]["fGPtbl_CUnit_12A"].ToString();
            lbl12bIso.Text = Mydt.Rows[0]["fGPtbl_CUnit_12B"].ToString();
            lbl13aSignatory.Text = Mydt.Rows[0]["fGPtbl_CUnit_13A"].ToString();
            lbl13bDesignation.Text = Mydt.Rows[0]["fGPtbl_CUnit_13B"].ToString();

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            int companyid = Convert.ToInt32(Request.QueryString["id"]);
            Response.Redirect("WaitingCompanies_Reject.aspx?cid="+companyid, false);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        try
        {
            /* clsMasterMailer objMailer = new clsMasterMailer();
             Hashtable hashtbl = new Hashtable();
             string FromMail, ccMail, bccMail, subject, bodyMail;
             string emailid = "";
             DataSet Myds = new DataSet();

             int unitid = Convert.ToInt32(Request.QueryString["id"]);

             if (urlId == 1)
             {
                 objCompany = new clsCompany();
                 hashtbl = objCompany.Company_WaitingCompany_Registration_Accept(Unitid, UserId);
             }
             else if (urlId == 3)
             {
                 objAmendment = new clsAmendment();
                 hashtbl = objAmendment.CompanyAmendment_WaitingCompany_Accept(amendmentid, unitid, UserId);
             }
            
             successId = Convert.ToInt32(hashtbl["ErrorId"].ToString());
             emailid = hashtbl["email"].ToString();


             //TO FETCH MAILER
             Myds = objMailer.MasterMailer_Fetch_DataSet(1);
             FromMail =  UserEmailID.ToString();//Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
             ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
             bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
             subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
             bodyMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();// +txtReason.Text + ".";

             if (successId == 2)
             {
                 SendMail.Mail_WithOUTAttachment(emailid, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0,"","","");
                 Session["companyAction"] = "Status changed successfully!";
                 */
            System.Threading.Thread.Sleep(10000);
                if (urlId == 1)
                {
                    Response.Redirect("WaitingCompanies.aspx?i=1", false);
                }
                else if (urlId == 3)
                {
                    Response.Redirect("WaitingCompanies.aspx?i=3", false);
                }
           // }
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
                HyperLink hlname=(HyperLink) e.Row.FindControl("HLName");
                HiddenField hfpath=(HiddenField) e.Row.FindControl("HiddenFieldpath");
                HiddenField hfname=(HiddenField) e.Row.FindControl("HiddenFieldname");

                //hlname.NavigateUrl = filepath + "/." + hfpath.Value.ToString() + hfname.Value.ToString();
                hlname.NavigateUrl = "../"+hfpath.Value.ToString() + hfname.Value.ToString();
                //hlname.NavigateUrl="C:/Documents and Settings/abhishek.jain.WSDP29/My Documents/Visual Studio 2008/Projects/NSICGP/Document/Upload/Registration/1_3-d146h_2012130_21549_406_D5tB60t83dbX.jpg";
                
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
    }
}
