using Newtonsoft.Json.Linq;
using NSICGP_VBClassLibrary;
using RestSharp;
using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class appAdministrator_ZHMonetaryRegistration : System.Web.UI.Page
{

    SqlCommand MySqlComm;
    SqlDataAdapter MyAdp;
    SqlDataReader MyDr;
    DataSet MyDs;
    DataTable MyDt;


    clsCompany objCompany;
    clsAmendment objAmendment;
    DataSet Myds, ds;
    int UserId, RoleId, DepartmentId;

    string UserFullName, password;
    public int monetzonalAction = 0;
    int successId, amendmentid = 0, urlid;
    string ToMail = string.Empty;
    string FromMail = string.Empty;
    string ccMail = string.Empty;
    string bccMail = string.Empty;
    string subject = string.Empty;
    string bodyMail = string.Empty;
    string UserEmailID = string.Empty;
    Hashtable hashtbl = new Hashtable();

    int BranchId;
    string Branch_Name = "";


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

            clsTrans_UserBranch objtub = new clsTrans_UserBranch();

            if (UserId != 231)
            {
                Branch_Name = objtub.Trans_UserBranch_FetchZoneId_DataSet(UserId).Tables[0].Rows[0]["BranchName"].ToString();
            }
            else
            {
                Branch_Name = "text";
            }
            // txtMonetary.Attributes.Add("onkeydown", "return FunDefaultButtonClick('ctl00_ContentPlaceHolder1_btnSubmit');");

            if (RoleId == 2 || RoleId == 3 || RoleId == 5)
            {
                if (Request.QueryString["i"] != null)
                {
                    urlid = Convert.ToInt32(Request.QueryString["i"].ToString());
                }
                if (Page.IsPostBack != true)
                {
                    //ACTIVATE THE VIEW AND FILLING GRIDVIEW
                    mvMonetary.SetActiveView(ViewMonetaryDetail);
                    GridViewDataBind();
                    if (RoleId == 3)
                    {
                        gvMonetaryDetails.Columns[7].Visible = false;
                    }
                    else
                    {
                        gvMonetaryDetails.Columns[7].Visible = false;// Earlier It was True but commented on 04.09.19
                    }
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
    public void GridViewDataBind()
    {
        objCompany = new clsCompany();
        Myds = new DataSet();
        try
        {
            if (urlid == 1)
            {
                lblMonetary.Text = "Monetary Limit for Registration";
                Myds = objCompany.Company_MonetaryLimit_FetchZonalHeadRegistration_Dataset(UserId, RoleId, DepartmentId);
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }
            else if (urlid == 2)
            {
                lblMonetary.Text = "Monetary Limit for Renewal";
                clsRenewal objrenew = new clsRenewal();
                Myds = objrenew.Company_MonetaryLimit_FetchZonalHeadRenew_Dataset(UserId, RoleId, DepartmentId);
                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }
            else if (urlid == 3)
            {
                lblMonetary.Text = "Monetary Limit for Ammendment";
                objAmendment = new clsAmendment();
                Myds = objAmendment.CompanyAmendment_MonetaryLimit_FetchZonalHead_Dataset(UserId, RoleId, DepartmentId);

                if (Myds.Tables[0].Rows.Count > 0)
                {
                    gvMonetaryDetails.DataSource = Myds;
                    gvMonetaryDetails.DataBind();
                }
            }

            if (Myds.Tables[0].Rows.Count == 0)
            {
                mvMonetary.SetActiveView(ViewMonetaryNotFound);
                lblMessage.Text = "No Monetary limit to be issued!";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        try
        {
            if (Session["monetzonalAction"] != null)
            {
                LabelAction.Text = Session["monetzonalAction"].ToString();
                monetzonalAction = 1;
                Session["monetzonalAction"] = null;
            }
            else
            {
                LabelAction.Text = "";
                monetzonalAction = 0;
                Session["monetzonalAction"] = null;
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            Session["monetzonalAction"] = null;
        }
    }
    protected void gvMonetaryDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            //THIS IS FOR SETTING MONETARY LIMIT
            if (e.CommandName == "edp")
            {
                lblAddEdit.Text = "Monetary Limit";

                //SHOWING MODALPOPUPEXTENDER
                ModalPopupExtender1.Show();

                //TO CHECK PROVISIONAL COMPANY
                /* objCompany = new clsCompany();
                 int provisionalstatus = objCompany.Company_Provisional_Status(Convert.ToInt32(HDCOmpanyid.Value));
                 if (provisionalstatus == 1)
                 {
                     disabletextbox();
                 }*/

            }

            else if (e.CommandName.ToUpper().Trim() == "EDITML")
            {
                // 
                string companyId = e.CommandArgument.ToString();
                if (Request.QueryString["i"] != null)
                {
                    urlid = Convert.ToInt32(Request.QueryString["i"].ToString());
                    Response.Redirect("Admin_Edit_monetary.aspx?id=" + companyId + "&urlid=" + urlid + "");
                }
            }

            else if (e.CommandName == "reject")
            {
                HDCOmpanyid.Value = Convert.ToString(e.CommandArgument);
                MPExtenderRejectreason.Show();
                /*
                if (urlid == 1)
                {
                    password = objCompany.Company_InsertUpdate_Zone(comidentity, "", UserId);
                }
                else if (urlid == 2)
                {
                    clsRenewal objrenew = new clsRenewal();
                    password = objrenew.Company_InsertUpdate_Zone(comidentity, "", UserId);
                }
                else if (urlid == 3)
                {
                    password = objCompany.Company_InsertUpdate_ZoneAmedment(comidentity, UserId);
                }
                //HDCOmpanyid.Value = Convert.ToString( e.CommandArgument);
                sendmailcompany(comidentity);
                 */
            }

            HDCOmpanyid.Value = Convert.ToString(e.CommandArgument);
            ShowHistory();
            //Monetarydetail(Convert.ToInt32(e.CommandArgument), e.CommandName);

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        GridViewDataBind();
    }
    //TO FETCH ALL DETAILS OF MONETARY LIMIT
    /*public void Monetarydetail(int monetaryid, string popuptype)
    {
        ShowHistory();
        try
        {
            objCompany = new clsCompany();
            //objAmendment = new clsAmendment();
            Myds = new DataSet();
            if (urlid == 1)
            {
                Myds = objCompany.Company_MonetaryLimit_FetchByCompId_Dataset(monetaryid);
            }
            if (urlid == 2)
            {
                clsRenewal objrenew = new clsRenewal();

                Myds = objrenew.Company_MonetaryLimit_FetchByCompId_Dataset(monetaryid);
                //VALIDITY AND PROPOSAL DATE MADE VISIBLE FALSE FOR RENEWAL AND AMENDMENT
                /*trproposal.Visible = false;
                trvalidity.Visible = false;**** /
             }
            else if (urlid == 3)
            {
                Myds = objCompany.Company_MonetaryLimit_FetchByCompId_Dataset_Amedment_Process(monetaryid);
                /*trproposal.Visible = false;
                trvalidity.Visible = false;* /
            }

            if (Myds.Tables[0].Rows.Count > 0)
            {
                if (popuptype == "edp")
                {
                    MonetaryRepeater.DataSource = Myds;
                    MonetaryRepeater.DataBind();
                }
                else if (popuptype == "reject")
                {
                    RepeaterReject.DataSource = Myds;
                    RepeaterReject.DataBind();
                }
            }
            else
            {
                MonetaryRepeater.Dispose();
                MonetaryRepeater.DataBind();
                RepeaterReject.Dispose();
                RepeaterReject.DataBind();
                Myds.Dispose();
            }

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }*/

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            inserttomonetary();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    public void inserttomonetary()
    {
        try
        {
            objCompany = new clsCompany();
            objAmendment = new clsAmendment();

            /* int cnt,i;
             string strlimit,strmonetary,strunt;
             strlimit = "";
             cnt = grdTurnOver.Rows.Count;
             i = 0;
             while (i < cnt)
             {
                 TextBox txtlimit = (TextBox)grdTurnOver.Rows[i].FindControl("txt_limit");
                 HiddenField hdnunt = (HiddenField)grdTurnOver.Rows[i].FindControl("hdnuntid");
                 strmonetary = txtlimit.Text.Trim() ;
                 strunt = hdnunt.Value ;
                 if (i == 0)
                 {
                     strlimit = strunt + "," + strmonetary;
                 }
                 else
                 {
                     strlimit = strlimit + ":" + strunt + "," + strmonetary;
                 }
                 i = i + 1;
             }*/


            string strlimit = "a";
            if (urlid == 1)
            {
                //password = objCompany.Company_InsertUpdate_Zone_Validity(Convert.ToInt32(HDCOmpanyid.Value), Convert.ToString(txtApproved.Text.Trim()), Convert.ToString(txtValidity.Text.Trim()), UserId);
                ////password = objCompany.Company_InsertUpdate_Zone_Validity(Convert.ToInt32(HDCOmpanyid.Value), UserId);


                ////if (password != "" || password != null)
                ////    successId = 2;



                #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB

                int FormType = 1;
                int status;
                string msg;
                CFSSService obj = new CFSSService();


                string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
                CFSSService service = new CFSSService();
                string results = service.ProcessPayment(Convert.ToInt32(HDCOmpanyid.Value), urlid, apiUrl, "FRESH_R_RA_HEAD_APPROVAL");
                if (results == "SUCCESS")
                {
                    password = objCompany.Company_InsertUpdate_Zone_Validity(Convert.ToInt32(HDCOmpanyid.Value), UserId);


                    if (password != "" || password != null)
                        successId = 2;

                   // ScriptManager.RegisterStartupScript(this.Update_bton, typeof(string), "Alert", "alert('Data has been updated successfully.');", true);
                    
                }
                else
                {
                    lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
                    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                }

                #endregion




                #region Generate Unique Number and Update in Company Table by Dhiraj 04/12/2019

                try
                {
                    MyDs = new DataSet();
                    MyDs = Company_DetailsBy_CompanyId(Convert.ToInt32(HDCOmpanyid.Value));
                    if (MyDs.Tables.Count > 0)
                    {
                        RestClient client = new RestClient("http://api.nsicnet.in/KYCAPI/api/KYC");
                        RestRequest request = new RestRequest("login", Method.POST);
                        request.AddHeader("Accept", "application/json");
                        string bCode = MyDs.Tables[0].Rows[0]["fGtbl_MB_UniCode"].ToString();
                        string strKYCNo = MyDs.Tables[0].Rows[0]["fGPtbl_Company_PAN"].ToString().Trim();  // PAN;
                        string strKycDocName = "pan";
                        string strLastName = MyDs.Tables[0].Rows[0]["fGPtbl_Company_Name"].ToString();
                        string strSocialCategory = MyDs.Tables[0].Rows[0]["Enterprise_Social_Category"].ToString(); //"general";
                        string strEnterpriseCategory = MyDs.Tables[0].Rows[0]["Category_Unit_Size"].ToString(); //"micro";
                        string strSchemeCode = "SPR";
                        string SchemeStartDate = MyDs.Tables[0].Rows[0]["ValidityFrom"].ToString(); //"01-01-2019";
                        string SchemeEndDate = MyDs.Tables[0].Rows[0]["ValidityTo"].ToString();


                        request.AddParameter(
                    "application/json", "{\"BranchCode\": \"" + bCode + "\", \"KYCNo\": \" " + strKYCNo + "\",\"KycDocName\": \"" + strKycDocName + "\",\"LastName\": \"" + strLastName + "\",\"SocialCategory\": \"" + strSocialCategory + "\",\"EnterpriseCategory\": \"" + strEnterpriseCategory + "\",\"SchemeCode\": \"" + strSchemeCode + "\",\"SchemeStartDate\": \"" + SchemeStartDate + "\",\"SchemeEndDate\": \"" + SchemeEndDate + "\" }", // <- your JSON string
                    ParameterType.RequestBody);

                        //              request.AddParameter(
                        //"application/json", "{ \"BranchCode\": \"" + bCode + "\", \"KYCNo\": \" " + strKYCNo + "\",\"KycDocName\": \"" + strKycDocName + "\",\"LastName\": \"" + strLastName + "\",\"SocialCategory\": \"" + strSocialCategory + "\",\"EnterpriseCategory\": \"" + strEnterpriseCategory + "\",\"SchemeCode\": \"" + strSchemeCode + "\",\"SchemeStartDate\": \"" + SchemeStartDate + "\"}", ParameterType.RequestBody);



                        IRestResponse response = client.Execute(request);
                        HttpStatusCode statusCode = response.StatusCode;
                        int numericStatusCode = (int)statusCode;
                        string statusDescription = response.StatusDescription;
                        string rawResponse = response.Content;
                        var content = response.Content;

                        if (numericStatusCode.ToString() != "400" && numericStatusCode.ToString() != "500")
                        {
                            var Message = JObject.Parse(content).SelectToken("Message");
                            var UniqueCode = JObject.Parse(content).SelectToken("UniqueCode");
                            Company_Update_Unique_Code(Convert.ToInt32(HDCOmpanyid.Value), UniqueCode.ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrHandler.WriteError(ex.Message + "DUE TO UNIQUE CODE GENERATION");
                }
                #endregion
            }
            if (urlid == 2)
            {
                clsRenewal objrenew = new clsRenewal();
                ////password = objrenew.Company_InsertUpdate_Zone(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                ////if (password != "" || password != null)
                ////    successId = 2;



                #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB

                int FormType = 2;
                int status;
                string msg;
                CFSSService obj = new CFSSService();


                string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
                CFSSService service = new CFSSService();
                string results = service.ProcessPayment(Convert.ToInt32(HDCOmpanyid.Value), urlid, apiUrl, "FRESH_R_RA_HEAD_APPROVAL");
                if (results == "SUCCESS")
                {
                    password = objrenew.Company_InsertUpdate_Zone(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                    if (password != "" || password != null)
                        successId = 2;

                }
                else
                {
                    lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
                    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                }

                #endregion


            }
            if (urlid == 3)
            {
                ////int RenewalCumAmendment_AmendmentStatus = 0;
                ////DataSet dsf = new DataSet();
                ////dsf = Fetch_ChangesType_Renewal_Amendment_Online(Convert.ToInt32(HDCOmpanyid.Value));

                ////RenewalCumAmendment_AmendmentStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);
                ////int int_Compaany_id = 0;
                ////int_Compaany_id = Convert.ToInt32(HDCOmpanyid.Value);
                ////if (RenewalCumAmendment_AmendmentStatus == 240) //--Renewal Cum Amendment Type Flag
                ////{
                ////    successId = Company_InsertUpdate_Zone_Renewal_Cum_Amendment(int_Compaany_id, strlimit, Convert.ToInt32(UserId)); // objAmendment.CompanyAmendment_BranchAccount_ChangecompanyStatus(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                ////}
                ////else if (RenewalCumAmendment_AmendmentStatus == 420)//--Only Amendment Type Flag  
                ////{
                ////    successId = objAmendment.CompanyAmendment_BranchAccount_ChangecompanyStatus(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                ////}
                ////else
                ////{
                ////    successId = Company_Insert_FailureTrack(Convert.ToInt32(HDCOmpanyid.Value), "Neither 240 flag nor 420 flag", "GPsp_CompanyAmendment_ConditionCheck_Failure");
                ////}


                #region CFSS Code Payment Response API Call to fetch Payment response from CFSS and update in DB

                int FormType = 3;
                int status;
                string msg;
                CFSSService obj = new CFSSService();


                string apiUrl = ConfigurationManager.AppSettings["CFSS_PaymentPostingAPIUrl"];
                CFSSService service = new CFSSService();
                string results = service.ProcessPayment(Convert.ToInt32(HDCOmpanyid.Value), urlid, apiUrl, "FRESH_R_RA_HEAD_APPROVAL");
                if (results == "SUCCESS")
                {
                    int RenewalCumAmendment_AmendmentStatus = 0;
                    DataSet dsf = new DataSet();
                    dsf = Fetch_ChangesType_Renewal_Amendment_Online(Convert.ToInt32(HDCOmpanyid.Value));

                    RenewalCumAmendment_AmendmentStatus = Convert.ToInt32(dsf.Tables[0].Rows[0]["ResultType"]);
                    int int_Compaany_id = 0;
                    int_Compaany_id = Convert.ToInt32(HDCOmpanyid.Value);
                    if (RenewalCumAmendment_AmendmentStatus == 240) //--Renewal Cum Amendment Type Flag
                    {
                        successId = Company_InsertUpdate_Zone_Renewal_Cum_Amendment(int_Compaany_id, strlimit, Convert.ToInt32(UserId)); // objAmendment.CompanyAmendment_BranchAccount_ChangecompanyStatus(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                    }
                    else if (RenewalCumAmendment_AmendmentStatus == 420)//--Only Amendment Type Flag  
                    {
                        successId = objAmendment.CompanyAmendment_BranchAccount_ChangecompanyStatus(Convert.ToInt32(HDCOmpanyid.Value), strlimit, UserId);
                    }
                    else
                    {
                        successId = Company_Insert_FailureTrack(Convert.ToInt32(HDCOmpanyid.Value), "Neither 240 flag nor 420 flag", "GPsp_CompanyAmendment_ConditionCheck_Failure");
                    }

                }
                else
                {
                    lblErrorMsg.Text = "Payment Response failed from CFSS Server. Please try again or contact support.";
                    lblErrorMsg.ForeColor = System.Drawing.Color.Red;
                }

                #endregion

            }
            switch (successId)
            {
                case 1://DATA ALREADY EXISTS
                    Session["monetzonalAction"] = "Successfully updated";
                    //if (urlid == 1)
                    {
                        sendmailcompany(Convert.ToInt32(HDCOmpanyid.Value));
                    }
                    break;
                case 2://SUCCESSFULLY UPDATED
                    Session["monetzonalAction"] = "Successfully updated";
                    // if (urlid == 1)
                    {
                        sendmailcompany(Convert.ToInt32(HDCOmpanyid.Value));
                    }
                    break;
                case 3://ERROR IN OPERATION
                    Session["monetzonalAction"] = "Server is busy";
                    break;
                default://ERROR IN OPERATION
                    Session["monetzonalAction"] = "Server is busy";
                    break;
            }
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "ERROR ZHMONETRY REGESTRATION ISHA DHIRAJ");
        }
    }
    public int Company_Insert_FailureTrack(int CompanyId, string Msg, string SPName)
    {
        int Primary_Id = 0;
        try
        {
            MySqlComm = new SqlCommand("GPsp_CompanyAmendment_ConditionCheck_Failure", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MySqlComm.Parameters.Add("@Msg", SqlDbType.VarChar, 1500).Value = Msg;
            MySqlComm.Parameters.Add("@SPName", SqlDbType.VarChar, 1500).Value = SPName;

            //SqlParameter fGPtbl_POI_Id = new SqlParameter("@Id", SqlDbType.Int);
            //fGPtbl_POI_Id.Direction = ParameterDirection.Output;
            //MySqlComm.Parameters.Add(fGPtbl_POI_Id);
            MySqlComm.ExecuteNonQuery();
            // Primary_Id = Convert.ToInt32(fGPtbl_POI_Id.Value);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return Primary_Id;
    }
    public int Company_InsertUpdate_Zone_Renewal_Cum_Amendment(int companyid, string monetarylimit, int userid)
    {
        string passwd = "";
        int aaaaa = 2;
        try
        {
            RandomKeyGenerator objkey = new RandomKeyGenerator();
            passwd = objkey.Generate();
            MySqlComm = new SqlCommand("GPsp_Company_Monetary_Renewal_Update_ById_zone_Renewal_Amendment_Online" +
                "", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@fGPtbl_Company_ML_CompanyId", SqlDbType.Int).Value = companyid;
            MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = userid;
            if (monetarylimit != "a") MySqlComm.Parameters.Add("@MonetaryLimit", SqlDbType.VarChar, 1000).Value = monetarylimit;
            MySqlComm.Parameters.Add("@fGPtbl_Company_Pwd", SqlDbType.NVarChar, 4000).Value = passwd;
            //MySqlComm.Parameters.Add("@fGPtbl_Company_ML_MonetoryLimit", SqlDbType.Float).Value = Monetarylimit;
            //MySqlComm.Parameters.Add("@Userid", SqlDbType.Int).Value = UserId;
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
        return aaaaa;
    }
    public DataSet Fetch_ChangesType_Renewal_Amendment_Online(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Renewal_Amendment_Amendment_Type_Online", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 60000;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
        finally
        {
            DBManager.CloseConnection();
            MyAdp.Dispose();
        }
        return MyDs;
    }
    //TO SEND MAIL TO COMPANY
    public void sendmailcompany(int compIdentity)
    {
        try
        {
            //string ToMail, FromMail, ccMail, bccMail, subject, bodyMail;
            clsCompany objCompany = new clsCompany();
            clsMasterMailer objMailer = new clsMasterMailer();
            clsMasterBranch objBranch = new clsMasterBranch();

            DataSet Myds = new DataSet();
            DataTable Mydt, MydtBranchEmail = new DataTable();
            MydtBranchEmail = objBranch.Fetch_BranchAddress_ByCompanyId(compIdentity);

            //TO BRANCH ACCOUNT MAILER
            Myds = objMailer.MasterMailer_Fetch_DataSet(2); //Change Here
            //FromMail = UserEmailID.ToString(); //Myds.Tables[0].Rows[0]["fGPtbl_MM_From"].ToString();
            if (MydtBranchEmail.Rows.Count > 0)
            {
                FromMail = MydtBranchEmail.Rows[0]["EMAILID"].ToString();
            }
            ccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Cc"].ToString();
            bccMail = Myds.Tables[0].Rows[0]["fGPtbl_MM_Bcc"].ToString();
            subject = Myds.Tables[0].Rows[0]["fGPtbl_MM_Subject"].ToString();
            string bodyMail_Data = Myds.Tables[0].Rows[0]["fGPtbl_MM_Body"].ToString();
            string str = string.Empty;

            //objCompany = new clsCompany();
            //TO FETCH COMPANY E-MAIL
            Mydt = objCompany.Company_Detail_Fetch_ById(compIdentity);
            if (Mydt.Rows.Count > 0)
            {
                str = bodyMail_Data.Replace("$$CompanyName$$", Mydt.Rows[0]["fGPtbl_Company_Name"].ToString());
                str = str.Replace("$$username$$", Mydt.Rows[0]["fGPtbl_Company_Username"].ToString());
                str = str.Replace("$$password$$", Mydt.Rows[0]["fGPtbl_Company_Pwd"].ToString());
                str = str.Replace("$$url $$", "http://www.nsicspronline.com/appCompany/CompanyLogin.aspx");
                str = str.Replace("$$Branchname$$", Branch_Name);
                ToMail = Mydt.Rows[0]["fGPtbl_Company_HeadOffice_Email"].ToString(); //"anupam.sen@Nagsoft.com";
            }
            else
            {
                str = "";
            }
            string UserName_Data = Mydt.Rows[0]["fGPtbl_Company_Name"].ToString();
            //  bodyMail = "Dear" + ' ' + UserName_Data + str;
            bodyMail = str;
            sendmail(FromMail, ToMail, bccMail, ccMail, subject, bodyMail);
            // SendMail.Mail_WithOUTAttachment(ToMail, 0, 0, FromMail, 0, 0, ccMail, bccMail, subject, bodyMail, "", 0, "", "", "");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    //TO DISABLE TEXT BOXES IN CASE OF PROVISIONAL COMPANY
    /* private void disabletextbox()
     {
         foreach (GridViewRow mrow in grdTurnOver.Rows)
         {
             TextBox txtcon;
             txtcon = (TextBox)mrow.FindControl("txt_limit");
             txtcon.Enabled = false;
             btnSubmit.Enabled = false;
         }
     }*/

    // Send Mail 
    public static void sendmail(string MailFrom, string MailTo, string bccMail, string ccMail, string subject, string bodyMail)
    {
        try
        {
            string SmtpServerName = System.Configuration.ConfigurationManager.AppSettings["SmtpName"];
            string SmtpUser = System.Configuration.ConfigurationManager.AppSettings["SmtpUser"];
            string SmtpPwd = System.Configuration.ConfigurationManager.AppSettings["SmtpPwd"];
            string SmtpPortNumber = System.Configuration.ConfigurationManager.AppSettings["SPRSPortNumber"];


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
            }

            message.Bcc.Add(new MailAddress("ishasingh@nsic.co.in"));

            // Set the subject of the mail message
            message.Subject = subject;
            // Set the body of the mail message
            message.Body = bodyMail;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = SmtpServerName;
            message.IsBodyHtml = true;
            smtpClient.Port = Convert.ToInt32(SmtpPortNumber); // 25; 25;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(SmtpUser, SmtpPwd);
            smtpClient.Send(message);
            //INSERT INTO SENT MAILER
            clsDDL.Insert_MailDetails(MailTo, 7, 3, MailFrom, 1, 1, subject, bodyMail, 2);
            // smtpClient.Send(message);
            // smtpClient.Timeout = 500;
        }
        catch (Exception ex)
        {
            //ex.Message;
        }
    }
    protected void gvMonetaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string compname = string.Empty, compid = string.Empty; string branchname = string.Empty;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DecryptEncrypt objDecr = new DecryptEncrypt();
                HiddenField hdn = (HiddenField)e.Row.FindControl("hdncid");
                compid = objDecr.Encrypt(hdn.Value.ToString());
                compname = objDecr.Encrypt(e.Row.Cells[2].Text.ToString());
                branchname = objDecr.Encrypt(Branch_Name.ToString());
                HyperLink hyplnk = (HyperLink)e.Row.FindControl("lnkview");
                HyperLink lbl_view_documet = (HyperLink)e.Row.FindControl("hypviewdocumet");
                if (urlid == 1)
                {
                    hyplnk.NavigateUrl = "~/appAdministrator/ReportViewPage.aspx?Company_ID=" + compid + "&Com_name=" + compname + "&BranchName=" + branchname;
                }
                else if (urlid == 2)
                {
                    hyplnk.NavigateUrl = "~/appAdministrator/RenewalformField.aspx?Company_ID=" + compid + "&Com_name=" + compname;
                    clsRenewal objrenew_get = new clsRenewal();
                    DataSet ds_viewdocumnt = new DataSet();
                    ds_viewdocumnt = objrenew_get.Company_Upload_DocumentView(Convert.ToInt32(hdn.Value.ToString()));
                    if (ds_viewdocumnt.Tables[0].Rows.Count > 0)
                    {
                        if (ds_viewdocumnt.Tables[0].Rows[0]["DocumentName"].ToString() == "")
                        {
                            lbl_view_documet.Visible = false;
                        }
                        else
                        {
                            lbl_view_documet.NavigateUrl = "../Document/InspectionMonetry/" + ds_viewdocumnt.Tables[0].Rows[0]["DocumentName"].ToString();
                            lbl_view_documet.Text = "View Upload";
                            lbl_view_documet.ToolTip = "View Upload";
                            e.Row.BackColor = System.Drawing.Color.Red;
                            e.Row.ForeColor = System.Drawing.Color.White;
                        }
                    }
                    else
                    {
                        ds_viewdocumnt.Dispose();
                    }
                }
                else if (urlid == 3)
                {
                    string EncryptStatus = objDecr.Encrypt("1");
                    string EncryptUrlId = objDecr.Encrypt("999");
                    string EncryptID = objDecr.Encrypt("1");
                    string EncryptBranchid = objDecr.Encrypt(Convert.ToString(BranchId));
                    hyplnk.NavigateUrl = "UnitAmendment_ViewChanges.aspx?c=" + compid + "&u=" + EncryptID + "&s=" + EncryptStatus + "&b=" + EncryptBranchid + "&i=" + EncryptUrlId;
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }
    protected void btnRejectReason_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            objCompany.Company_Update_reasontoreject(Convert.ToInt32(HDCOmpanyid.Value), txtRejectreason.Text.Trim(), urlid, UserId);
            GridViewDataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }
    void ShowHistory()
    {
        try
        {
            clsCompany objcom = new clsCompany();
            ds = new DataSet();
            ds = objcom.FetchTurnover_history(Convert.ToInt32(HDCOmpanyid.Value), Convert.ToInt32(urlid));
            if (ds != null)
            {
                if (ds.Tables.Count > 0)
                {
                    ds.Tables[1].Columns.Remove("id");
                    gv_display.DataSource = ds.Tables[1];
                    gv_displayreject.DataSource = ds.Tables[1];
                    gv_display.DataBind();
                    gv_displayreject.DataBind();
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    Single montotal = 0;
    protected void MonetaryRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        try
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                montotal += Convert.ToSingle(DataBinder.Eval(e.Item.DataItem, "fGPtbl_CUnit_ML_MonetoryLimit"));
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal totmonet = (Literal)(e.Item.FindControl("ltlTotalMonetary"));
                totmonet.Text = montotal.ToString();
            }
        }
        catch { }
    }
    public DataSet Company_DetailsBy_CompanyId(int CompanyId)
    {
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_Company_Details_For_Unique_Code_New", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }
    public void Company_Update_Unique_Code(int CompanyId, string UniqueCode)
    {
        try
        {
            MySqlComm = new SqlCommand("GPsp_Update_Unique_Code", DBManager.ConnectToDB());
            MySqlComm.CommandType = CommandType.StoredProcedure;
            MySqlComm.Parameters.Add("@CompanyId", SqlDbType.VarChar, 5000).Value = CompanyId;
            MySqlComm.Parameters.Add("@UniqueCode", SqlDbType.VarChar, 5000).Value = UniqueCode;
            MySqlComm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            DBManager.CloseConnection();
            MySqlComm.Dispose();
        }
    }
}