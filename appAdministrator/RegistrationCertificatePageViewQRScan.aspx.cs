using NSICGP_VBClassLibrary;
using Num2Wrd;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

public partial class appAdministrator_RegistrationCertificatePageViewQRScan : System.Web.UI.Page
{
    clsCompany objCompany;
    string[] Name;
    string[] SI;
    string NameOfPartner;
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 20;
    int UserID, BranchId, ComID;
    string SerialNumber;
    string RegistrationNumber = string.Empty;
    public string SerialNoAdd;
    string Branch_Code = string.Empty;
    string RegistrationDatefrom = "";
    public int FormtypeID { get; set; }

    string AmedmentChekRenewal = "";
    int UserId, RoleId, DepartmentId;
    string UserFullName, UserEmailID;

    private string serial_NUMBER
    {
        get;
        set;
    }

    public void FunRequest()
    {
        DecryptEncrypt objDecEnc = new DecryptEncrypt();
        try
        {
            #region Basic Details
            UserID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).Replace(" ", "+")).ToString();
            #endregion
        }
        catch
        {
            #region Basic Details
            UserID = 0;
            RoleId = 0;
            DepartmentId = 0;
            UserFullName = "";
            //UserEmailID = "";
            #endregion
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {

            FunRequest();

            DecryptEncrypt objDecEnc = new DecryptEncrypt();

            // UserID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            //clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            //BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
            // lbl_Branch_Name.Text = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["BranchName"]);
            // Branch_Code = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["fGPtbl_MB_Code"]);
            Branch_Code = objDecEnc.Decrypt(Request.QueryString["d"].Replace(" ", "+")).ToString();
            objCompany = new clsCompany();
            Hashtable hashtbl = new Hashtable();

            ComID = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            hashtbl = objCompany.Company_Fetch_RegNo_ByCompanyId(ComID);
            RegistrationNumber = hashtbl["regno"].ToString();
            int prntaccess = Convert.ToInt32(hashtbl["pntaccess"]);
            // gvstore_details.UseAccessibleHeader = true;
            //  gvstore_details.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (!IsPostBack)
            {
                BindSerialNumber();
                Bind_Branch_office_details();
                BindGridCertificate();
                BindAddress();
                Show_Certificatcate_Registration();
                // BindTurnOver();
                BindGrid_Store();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }

    }

    protected void btnPrnt_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            int Output = 0;
            string serialnumberaddonly = lblserialNo.Text.ToString();
            string[] strArr = serialnumberaddonly.Split('-');
            string serialnoadd_only_number = strArr[1].ToString();

            string datetimelabel = lbldateform.Text.ToString();
            IFormatProvider enUsDateFormat = new CultureInfo("nl-BE").DateTimeFormat;
            DateTime date = DateTime.Parse(datetimelabel, enUsDateFormat);
            try
            {
                //Output = objCompany.Company_Certificate_Insert(Convert.ToInt32(hdn_formtypeid.Value), Convert.ToInt32(Request.QueryString["id"]), serialnoadd_only_number.ToString(), UserID, date);
                Output = objCompany.Company_Certificate_Insert(1, Convert.ToInt32(ComID), serialnoadd_only_number.ToString(), UserID, date);
            }
            catch (Exception ex)
            {
                ErrHandler.WriteError("Error in objCompany.Company_Certificate_Insert()");
            }
            //ErrHandler.WriteError("Error in output btnprint"+Output.ToString());


            int K = objCompany.Track_Registraton_Certificate(Convert.ToInt32(ComID), serialnoadd_only_number.ToString(), Branch_Code.ToString(), BranchId);
            btnPrnt.Visible = false;

            // Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.close(); </script>");
            //     string script = "<script>this.window.opener.location=this.window.opener.location;this.window.close();</script>";
            //  if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
            //  ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);   

            if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
                ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", "AlertMe();", true);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message + "  " + ex.StackTrace);
        }
    }


    public DataSet DataSetIsGraduate_Status_ByCompanyId(int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("GPsp_Fetch_IsGraduatet_Details_ByCompanyId", DBManager.ConnectToDB()); // this procedure if pass Between date than show error so please Modified again and again
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

    public void Show_Certificatcate_Registration()
    {
        try
        {

            string CompanyName = string.Empty;
            string PartenerName = string.Empty;

            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            System.Data.SqlClient.SqlDataReader MyDr;
            objCompany = new clsCompany();
            DataSet ds = new DataSet();
            string RefNo = RegistrationNumber;  //  this is Registration Number 
            int CompanyID = ComID;// Convert.ToInt32(Request.QueryString["id"]);
            //  hdn_company_id.Value = Request.QueryString["id"].ToString();
            ds = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset(CompanyID);
            DataSet dspartner = new DataSet();
            dspartner = objCompany.Company_Report_Registration_CertificateForm_PartnerName(CompanyID);
            DataTable dt = new DataTable();
            dt = dspartner.Tables[0];

            var qu = from n in dt.AsEnumerable()
                     select new
                     {
                         partnerName = n.Field<string>("DirectorName") + '@',

                     };

            foreach (var onl in qu)
            {

                if (dspartner.Tables[0].Rows.Count > 0)
                {

                    hdnlablName.Value += onl.partnerName.ToString();
                }
                else
                {
                    dspartner.Dispose();
                }
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblrefnumber.Text = ds.Tables[0].Rows[0]["CompanyRefNumber"].ToString();

                objCompany = new clsCompany();
                DataSet ds_fetch_firstprint = new DataSet();
                ds_fetch_firstprint = objCompany.Fetch_printout_date_modified(Convert.ToInt32(CompanyID));
                if (ds_fetch_firstprint.Tables[0].Rows.Count > 0)
                {//1
                    // if (ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString() != "01/01/1900")
                    // {//2

                    if (lbldateform.Text == "01/01/1900")
                    {

                        lbldateform.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                    }

                    if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "1")
                    {
                        // AmedmentChekRenewal = "Fresh";
                    }
                    else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "2")
                    {
                        AmedmentChekRenewal = "Renewal Certificate";
                    }
                    else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "3")
                    {
                        AmedmentChekRenewal = "Amendment Certificate";
                    }
                    else if (ds_fetch_firstprint.Tables[0].Rows[0]["FormTypeFlag"].ToString() == "0")
                    {
                        //  AmedmentChekRenewal = "Fresh";
                    }
                    //---- Compare two date 
                    /* int FormtypeId_com = Convert.ToInt32(ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString() );
                       DateTime AmednentDate=DateTime.ParseExact(ds_fetch_firstprint.Tables[0].Rows[0]["AmedmentDate"].ToString(),"dd/MM/yyyy",null );
                         DateTime RenewalDate=DateTime.ParseExact(ds_fetch_firstprint.Tables[0].Rows[0]["Renewaldate"].ToString(),"dd/MM/yyyy",null);
                         DateTime FirstPrintDate=DateTime.ParseExact(ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString(),"dd/MM/yyyy",null);
                         if (FormtypeId_com == 3)
                         {
                             if (AmednentDate >= FirstPrintDate)
                             {
                                 if ((FirstPrintDate <= DateTime.ParseExact(System.DateTime.Today.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null)) && (AmednentDate >= FirstPrintDate))
                                    {

                                        lbldateform.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                                        hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();
                                         
                                    }
                                    else
                                    {
                                        lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                                        hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();      
                                    }
                             }
                             else
                             {
                                 lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                                 hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();   
                             }
                         }
                         else if (FormtypeId_com == 2)
                         {
                             if (RenewalDate >= FirstPrintDate)
                             {
                                 if ((FirstPrintDate <= DateTime.ParseExact(System.DateTime.Today.Date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null)) && (RenewalDate >= FirstPrintDate))
                                 {
                                     lbldateform.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                                     hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();       
                                 }
                                 else
                                 {
                                     lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                                     hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();  

                                       
                                 }
                             }
                             else
                             {
                                 lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                                 hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();      
                             }
                         }
                         else if (FormtypeId_com == 1)
                         {
                             lbldateform.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                             hdn_formtypeid.Value = ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();         
                         }*/

                    // }//2
                    /* else
                     {
                         lbldateform.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                         hdn_formtypeid.Value = "1";// ds_fetch_firstprint.Tables[0].Rows[0]["fromTypeid_company"].ToString();
                         AmedmentChekRenewal = "Fresh";
                     }*/
                }//1
                else
                {

                    lbldateform.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                    hdn_formtypeid.Value = "1";
                    //  AmedmentChekRenewal = "Fresh";
                    ds_fetch_firstprint.Dispose();//
                }



                RegistrationDatefrom = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
                // lblFactoryAddress.Text = ds.Tables[0].Rows[0]["FactoryAddress"].ToString();
                lblNameCom.Text = " M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();
                lbl_company_name_footer.Text = lblNameCom.Text.ToString();
                LblCompanyAdress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                lbl_city_name.Text = ds.Tables[0].Rows[0]["CityName"].ToString() + ",";
                lblStateName.Text = ds.Tables[0].Rows[0]["StateName"].ToString() + "-";
                lblcompanypin.Text = ds.Tables[0].Rows[0]["pinCode"].ToString();
                string ProvisinalCertificate = ds.Tables[0].Rows[0]["ProvisnalCertificate"].ToString();

                string PartnerName = hdnlablName.Value;
                CultureInfo ci = new CultureInfo("en-GB");

                DateTime dat1 = Convert.ToDateTime(RegistrationDatefrom.ToString(), ci);


                int IsGraduate = 0;
                DataSet dsIsGraduate = DataSetIsGraduate_Status_ByCompanyId(CompanyID);
                IsGraduate = Convert.ToInt32(dsIsGraduate.Tables[0].Rows[0]["fGPtbl_Isgraduate"].ToString());

                if (IsGraduate == 0)
                {
                    if (ProvisinalCertificate == "0")
                    {
                        lblvalidToFrom.Text = AmedmentChekRenewal + "  " + "" + " " + " " + "<b>" + "(" + "Valid From " + Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyyy") + ")" + "</b>";
                        lblvalidToFrom_store.Text = lblvalidToFrom.Text;
                        BindTurnOver();
                        //  lbl_validform_store_details.Text = lblvalidToFrom.Text;
                    }
                    else
                    {
                        lblvalidToFrom.Text = AmedmentChekRenewal + "  " + "Certificate" + " " + " " + "<b>" + "(" + "Valid From " + Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(1).AddDays(-1).ToString("dd/MM/yyyy") + ")" + "</b>";
                        lbltrunover.Visible = false;
                        lblvalidToFrom_store.Text = lblvalidToFrom.Text;
                    }
                }

                else
                {
                    lblvalidToFrom.Text = AmedmentChekRenewal + "  " + "" + " " + " " + "<b>" + "(" + "Valid From " + Convert.ToString(dsIsGraduate.Tables[0].Rows[0]["fGPtbal_From_Date"].ToString()) + " to" + " " + dsIsGraduate.Tables[0].Rows[0]["fGPtbal_To_Date"].ToString() + ")" + "</b>";
                    lblvalidToFrom_store.Text = lblvalidToFrom.Text;
                    BindTurnOver();

                }

                //    if (ProvisinalCertificate == "0")
                //{
                //    lblvalidToFrom.Text = AmedmentChekRenewal + "  " + "" + " " + " " + "<b>" + "(" + "Valid From " + Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyyy") + ")" + "</b>";
                //    lblvalidToFrom_store.Text = lblvalidToFrom.Text;
                //    BindTurnOver();
                //    //  lbl_validform_store_details.Text = lblvalidToFrom.Text;
                //}
                //else
                //{
                //    lblvalidToFrom.Text = AmedmentChekRenewal + "  " + "Certificate" + " " + " " + "<b>" + "(" + "Valid From " + Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(1).AddDays(-1).ToString("dd/MM/yyyy") + ")" + "</b>";
                //    lbltrunover.Visible = false;
                //    lblvalidToFrom_store.Text = lblvalidToFrom.Text;
                //}







                string sub = PartnerName.Substring(0, PartnerName.Length - 1);
                Name = sub.Split('@');

                int i = 0;
                while (i <= Convert.ToInt32(Name.Length.ToString()) - 1)
                {
                    if (dspartner.Tables[0].Rows.Count == 1)
                    {
                        lblpartnaerName.Text = Name[i].ToString();
                    }
                    else
                    {
                        lblpartnaerName.Text = lblpartnaerName.Text + Convert.ToString(i + 1) + '.' + Name[i] + "</br>";
                    }
                    i++;
                }
                DataSet ds_monotory_limit = new DataSet();
                int CompanyIDMonitory = Convert.ToInt32(ComID);
                ds_monotory_limit = objCompany.Company_Certificate_MonoteryLimit(CompanyIDMonitory);
                int p = 0;
                if (ds_monotory_limit.Tables[0].Rows.Count > 0)
                {
                    for (int r = 0; r <= ds_monotory_limit.Tables[0].Rows.Count - 1; r++)
                    {
                        p = p + Convert.ToInt32(ds_monotory_limit.Tables[0].Rows[r]["Monetary Limit"].ToString());

                    }

                    //  lblMonLimit.Text = " " + ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString() + "  " + "lakhs" + " " + "(";
                    lblMonLimit.Text = " " + p + "  " + "lakhs" + " " + "(";
                    //  string NumberText = ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString();
                    string NumberText = p.ToString();
                    NumberToEnglish numToeng = new NumberToEnglish();
                    string rupeestext = string.Empty;
                    if (NumberText.Contains('.'))
                    {
                        string[] parts = NumberText.Split('.');
                        string i1 = parts[0];
                        string i2 = parts[1];
                        if (i2.Length == 1)
                        {
                            i2 = i2 + "0";
                        }
                        else
                        {
                        }
                        string numToeng_number = Convert.ToString(i1) + "." + Convert.ToString(i2);
                        //numToeng=numToeng_number.
                        rupeestext = numToeng.changeCurrencyToWords(numToeng_number);
                    }
                    else
                    {
                        //   rupeestext = numToeng.changeCurrencyToWords(NumberText.ToString());
                        rupeestext = numbertostring.ConvertMyword(NumberText.ToString());
                    }
                    lblTextRupee.Text = " " + rupeestext + ")";
                }
                else
                {
                    lblMonLimit.Text = ". 0 lakhs (";
                    lblTextRupee.Text = " Zero lakhs Only )";
                    ds_monotory_limit.Dispose();
                }

                lblRegisNo.Text = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
                /*  lbl_old_registration_no.Text = ds.Tables[0].Rows[0]["old_RegistrationNumber"].ToString();

                 if (lbl_old_registration_no.Text != "")
                 {
                     old_registration_number.Visible = true;
                 }
                 else
                 {
                     old_registration_number.Visible = false;
                 }

                 if (ds.Tables[0].Rows[0]["RegistrationNumber"].ToString() == ds.Tables[0].Rows[0]["old_RegistrationNumber"].ToString())
                 {
                     old_registration_number.Visible = false;
                 }

                 */


                RegistrationNumber = lblRegisNo.Text;
                SI = lblRegisNo.Text.ToString().Split('/');
                //  string sampleNum = "00000000"+ (incNumber++).ToString();
                SerialNumber = Branch_Code + "-" + SerialNoAdd; //"SPR-"+generateID("www.nsicspronline.com");//SI[3] + "/" + SI[4];
                lblserialNo.Text = SerialNumber.ToString();
                //lblserial_Store.Text = SerialNumber.ToString();
                serial_NUMBER = SerialNumber;
                lbl_Gen_sc.Text = ds.Tables[0].Rows[0]["CategoryCast"].ToString();
                // lblconstitutionCode.Text = ds.Tables[0].Rows[0]["ConstitutionName"].ToString().Replace("<br>", "");
                lbl_Special.Text = ds.Tables[0].Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
                lblconstitutionCode.Text = ds.Tables[0].Rows[0]["ConstitutionName1"].ToString().Replace("<br>", "");



                if (ds.Tables[0].Rows[0]["UAM"].ToString().ToUpper().Contains("UDYAM"))
                {
                    lblUAMDisplay.Text = "Udyam Registration Number";
                }
                else
                {
                    lblUAMDisplay.Text = "UAM";
                }


                lblUAM.Text = ds.Tables[0].Rows[0]["UAM"].ToString().Replace("<br>", "");


                string constitition_name = ds.Tables[0].Rows[0]["ConstitutionName"].ToString().Replace("<br>", "");//lblconstitutionCode.Text.ToString();
                if (constitition_name == "Public" || constitition_name == "Private")
                {
                    lbl_name_of_DIRECTOR.Text = "Directors";
                }
                else if (constitition_name == "Partnership" || constitition_name == "Limited")//dhiraj_nsic
                                                                                              // else if (constitition_name == "Partnership")//dhiraj_nsic
                {
                    lbl_name_of_DIRECTOR.Text = "Partners";
                }
                //else if (constitition_name == "Proprietorship" || constitition_name == "Hindu Undivided Family")
                else if (constitition_name == "Proprietorship")
                {
                    lbl_name_of_DIRECTOR.Text = "Proprietor";
                }

                else if (constitition_name == "Hindu")
                {
                    lbl_name_of_DIRECTOR.Text = "Karta";
                }

                //Added by Dhiraj on 25.7.2019
                else if (constitition_name == "Trust")
                {
                    lbl_name_of_DIRECTOR.Text = "Trustee";
                }
                //Added by Dhiraj on 25.7.2019
                //Dhiraj
                //else if (constitition_name == "Limited")
                //{
                //    lbl_name_of_DIRECTOR.Text = "Partners";
                //}
                else
                {
                    lbl_name_of_DIRECTOR.Text = "Directors";
                }

                BindGridCertificate();
            }
            else
            {
                ds.Dispose();
                dspartner.Dispose();

            }
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError("Server Busy!!!!");
            ErrHandler.WriteError("Show_Certificatcate_Registration()  " + ex.StackTrace + "  " + ex.Message);
        }

    }



    public void BindGridCertificate()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = RegistrationNumber;
            int CompanyID = ComID;// Convert.ToInt32(Request.QueryString["id"]);
            dsCertificate_store = objCompany.Fetch_company_Store_Certificate(CompanyID);
            //Ayush-->
            string query = "select fGPtbl_IsAyush from [GPtbl_Company] where [fGPtbl_Company_Id]='" + CompanyID + "'";
            string connectionstring = System.Configuration.ConfigurationManager.AppSettings["DBConnectionString"].ToString();
            SqlConnection conn = new SqlConnection(connectionstring);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            bool result = false;
            while (reader.Read())
            {
                result = (reader[0].ToString().Equals("True")) ? true : false;
            }
            lblAyush.Visible = lblAyushDisclaimer.Visible = result ? true : false;
            //Ayush<--
            if (dsCertificate_store.Tables[0].Rows.Count > 0)
            {

                lblGridCount.Text = "(" + dsCertificate_store.Tables[0].Rows.Count + "&nbsp; " + "item only" + " )";

                string PerMOnthPERShift = dsCertificate_store.Tables[0].Rows[0]["PMPs"].ToString().ToUpper();
                if (PerMOnthPERShift == "PMPS" || PerMOnthPERShift == "pmps" || PerMOnthPERShift == "Pmps")
                {
                    lbl_Quantitive_capacity.Text = "P.M.P.S.";
                }
                else if (PerMOnthPERShift == "P.M." || PerMOnthPERShift == "PM" || PerMOnthPERShift == "Pm" || PerMOnthPERShift == "pm")
                {
                    lbl_Quantitive_capacity.Text = "P.M.";
                }
                else if (PerMOnthPERShift == "P.A." || PerMOnthPERShift == "PA" || PerMOnthPERShift == "Pa")
                {
                    lbl_Quantitive_capacity.Text = "P.A.";
                }
            }
            else
            {
                //grdstores.Dispose();
                // grdstores.DataBind();
                dsCertificate_store.Dispose();
            }
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError("Server Busy!!!");
            ErrHandler.WriteError("BindGridCertificate()  " + ex.StackTrace + "  " + ex.Message);
        }

    }

    public void BindAddress()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsAddress = new DataSet();
            int CompanyID = ComID;//Convert.ToInt32(Request.QueryString["id"]);
            // setComment(CompanyID);

            setComment(BranchId, CompanyID);

            dsAddress = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset_AddressFactory(CompanyID);
            if (dsAddress.Tables[0].Rows.Count > 0)
            {//if ds address
             ////if (dsAddress.Tables[0].Rows.Count > 5)
             ////{
             ////    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<script type=\"text/javascript\"  language=\"javascript\">function showMsg(){return confirm(\"They have Contain More Than Five Address?\");}</script>", true);
             ////}
             ////else
             ////{//else

                if (dsAddress.Tables[0].Rows.Count == 1)
                {
                    string sno = dsAddress.Tables[0].Rows[0]["SNo"].ToString();

                    Rpt_Address.DataSource = dsAddress.Tables[0];
                    Rpt_Address.DataBind();
                }
                else
                {
                    Rpt_Address.DataSource = dsAddress.Tables[0];
                    Rpt_Address.DataBind();
                }
                Table tbl = new Table();


                for (int j = 0; j < dsAddress.Tables[0].Rows.Count; j++)
                {//for
                    string SNO = dsAddress.Tables[0].Rows[j]["SNo"].ToString();
                    string FactoryAddress = dsAddress.Tables[0].Rows[j]["AddressName"].ToString();
                    string StateName = dsAddress.Tables[0].Rows[j]["fGPtbl_MS_Name"].ToString();
                    string Zip = dsAddress.Tables[0].Rows[j]["fGPtbl_CUnit_1B_Factory_Zip"].ToString();
                    string CityName = dsAddress.Tables[0].Rows[j]["CityName"].ToString();
                    int UnitID = Convert.ToInt32(dsAddress.Tables[0].Rows[j]["UnitId"].ToString());

                    int enterprise = Convert.ToInt32(dsAddress.Tables[0].Rows[j]["fGPtbl_CUnit_Enterprise"].ToString());
                    hdn_enterprise.Value = enterprise.ToString();
                    Label lblUnitAddress = new Label();
                    lblUnitAddress.CssClass = "LabelPrintCertificate";  //

                    if (dsAddress.Tables[0].Rows.Count < 2)
                    {
                        lblUnitAddress.Text = "<br/>" + "  " + FactoryAddress.Replace('.', ' ') + ',' + "  " + CityName + ' ' + StateName + Zip + "<br/><br/>";
                    }
                    else
                    {
                        lblUnitAddress.Text = "<br/>" + "<b>UNIT-</b>" + SNO + ')' + FactoryAddress.Replace('.', ' ') + ',' + " " + CityName + ' ' + StateName + Zip + "<br/><br/>";
                    }

                    DataSet dsstore = new DataSet();
                    DataTable dt = new DataTable();

                    //CODE TO ADD A GRIDVIEW
                    GridView gridviewNewStore;
                    Label htlable;
                    //CODE TO ADD A GRIDVIEW

                    // GridView gvstore_details = new GridView();
                    Label tempHtmlTable = new Label();

                    dsstore = objCompany.Fetch_company_Store_Certificate_Modified(UnitID);
                    dt = dsstore.Tables[0];
                    gridviewNewStore = new GridView();
                    gridviewNewStore.CssClass = "gridviewstore";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtFilteredTable = new DataTable();
                        var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).Count();
                        int countStatus = Convert.ToInt32(count);
                        if (count > 0)
                        {// count if
                            dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).CopyToDataTable();
                            dtFilteredTable.Columns.Remove("UnitID");
                            string GvstorePMPS = dtFilteredTable.Rows[0]["PMPS"].ToString().ToUpper();
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[1].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[2].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());

                            //  HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());
                            HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Quantitative Capacity"].ToString());

                            //sRA

                            //Server.HtmlDecode(dtFilteredTable.Rows[0]["Model/ Brand"].ToString());
                            //Server.HtmlDecode(dtFilteredTable.Rows[0]["Capacity/ Rating"].ToString());
                            //Server.HtmlDecode(dtFilteredTable.Rows[0]["Production Capacity"].ToString());
                            //Server.HtmlDecode(dtFilteredTable.Rows[0]["No. of shifts"].ToString());


                            //eRA


                            string PMPSSTORE = "";
                            string PMPSSTORE2 = "";
                            if (GvstorePMPS == "PMPS" || GvstorePMPS == "pmps" || GvstorePMPS == "Pmps" || GvstorePMPS == "PMps")
                            {
                                PMPSSTORE = "P.M.P.S.";
                                PMPSSTORE2 = "PMPS";
                            }
                            else if (GvstorePMPS == "PM" || GvstorePMPS == "Pm" || GvstorePMPS == "pm")
                            {
                                PMPSSTORE = "P.M.";
                                PMPSSTORE2 = "PM";
                            }
                            else if (GvstorePMPS == "PA" || GvstorePMPS == "Pa" || GvstorePMPS == "pa")
                            {
                                PMPSSTORE = "P.A.";
                                PMPSSTORE2 = "PA";
                            }
                            hdn_pmps.Value = PMPSSTORE;
                            dtFilteredTable.Columns[4].ColumnName = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString() + Environment.NewLine + PMPSSTORE.ToString()); //HttpUtility.HtmlDecode(lblquntitycapacity.Text.ToString());
                            string Quantitative_Capacity = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString());

                            dtFilteredTable.Columns.Remove("PMPS");
                            DataTable dtNEW = new DataTable();
                            if (enterprise == 1) //manufac
                            {

                                trDisclaimerGem.Visible = true;


                                Table tab = new Table();
                                tab.BorderStyle = BorderStyle.Inset;
                                tab.CellPadding = 2;
                                tab.CellSpacing = 2;
                                tab.BorderWidth = 2;
                                tab.GridLines = GridLines.Both;
                                TableRow tabRow = new TableRow();
                                TableCell tcSno = new TableCell();
                                TableCell tcStrore = new TableCell();
                                TableCell tcSpecs = new TableCell();
                                TableCell tcQualitative = new TableCell();
                                TableCell tcQuantitative = new TableCell();

                                TableCell tcRemarks = new TableCell();

                                tcSno.Text = "<b>Sno<b/>";
                                tcStrore.Text = "<b>Store(s)/ <br/> Service(s) Name<b/>";
                                tcSpecs.Text = "<b>Specification(s)<b/>";
                                tcQualitative.Text = "<b>Qualitative Capacity<b/>";
                                tcQuantitative.Text = "<b>Quantitative Capacity <br/>" + PMPSSTORE2 + "<b/></td>";

                                tcRemarks.Text = "<b>Remarks<b/>";


                                tabRow.Cells.Add(tcSno);
                                tabRow.Cells.Add(tcStrore);
                                tabRow.Cells.Add(tcSpecs);
                                tabRow.Cells.Add(tcQualitative);
                                tabRow.Cells.Add(tcQuantitative);

                                tabRow.Cells.Add(tcRemarks);

                                tabRow.HorizontalAlign = HorizontalAlign.Center;
                                tab.Rows.Add(tabRow);
                                //StringBuilder sb = new StringBuilder();
                                //sb.Append("<table border='1' style='border:thin 2px black' cellpadding='2' cellspacing='2'> ");
                                //sb.Append("<tr style='text-align:center'>");
                                //sb.Append("<td><b>Sno<b/></td>");
                                //sb.Append("<td><b>Store(s)/ <br/> Service(s) Name<b/></td>");
                                //sb.Append("<td><b>Specification(s)<b/></td>");
                                //sb.Append("<td><b>Qualitative Capacity<b/></td>");
                                //sb.Append("<td><b>Quantitative Capacity <br/>" + PMPSSTORE2 + "<b/></td>");
                                //sb.Append("</tr>");
                                //int c = 1;
                                int spanSpes = 0;
                                int spanQual = 0;
                                int spanQuat = 0;
                                firstrowspecification = 0;
                                firstrowquality = 0;
                                firstrowquantity = 0;
                                foreach (DataRow dr in dtFilteredTable.Rows)
                                {


                                    string store = dr[1].ToString();
                                    string specifation = dr[2].ToString();
                                    string qualitative = dr[3].ToString();
                                    string quatitative = dr[4].ToString();

                                    string remarks = dr[5].ToString();


                                    if (store.Contains("\n"))
                                        store = store.Replace("\n", "<br/>");
                                    if (specifation.Contains("\n"))
                                        specifation = specifation.Replace("\n", "<br/>");
                                    if (qualitative.Contains("\n"))
                                        qualitative = qualitative.Replace("\n", "<br/>");
                                    if (quatitative.Contains("\n"))
                                        quatitative = quatitative.Replace("\n", "<br/>");

                                    if (remarks.Contains("\n"))
                                        remarks = remarks.Replace("\n", "<br/>");


                                    tabRow = new TableRow();
                                    tcSno = new TableCell();
                                    tcStrore = new TableCell();
                                    tcSpecs = new TableCell();
                                    tcQualitative = new TableCell();
                                    tcQuantitative = new TableCell();

                                    tcRemarks = new TableCell();


                                    tcSno.Text = dr[0].ToString();
                                    tcStrore.Text = store;
                                    tabRow.Cells.Add(tcSno);
                                    tabRow.Cells.Add(tcStrore);


                                    tcSpecs.Text = specifation;
                                    tcQualitative.Text = qualitative;
                                    tcQuantitative.Text = quatitative;

                                    tcRemarks.Text = remarks;


                                    tabRow.Cells.Add(tcSpecs);
                                    tabRow.Cells.Add(tcQualitative);
                                    tabRow.Cells.Add(tcQuantitative);

                                    tabRow.Cells.Add(tcRemarks);

                                    tab.Rows.Add(tabRow);
                                    //c++;
                                }

                                //quantative
                                int lastRowIndex = -1;
                                int span = 2;
                                for (int r = 0; r < tab.Rows.Count; r++)
                                {
                                    string quantitative = tab.Rows[r].Cells[4].Text;
                                    if ((quantitative == "" || quantitative == "0") && r > 1)
                                    {
                                        if (lastRowIndex == -1)
                                        {
                                            lastRowIndex = r - 1;
                                            span = 2;
                                        }
                                        else
                                        {
                                            span++;
                                        }
                                        tab.Rows[r].Cells.RemoveAt(4);

                                    }
                                    else
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[4].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }

                                    if (r + 1 == tab.Rows.Count)
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[4].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }
                                }

                                //qualitativr
                                lastRowIndex = -1;
                                span = 2;
                                for (int r = 0; r < tab.Rows.Count; r++)
                                {
                                    string qualitative = tab.Rows[r].Cells[3].Text;
                                    if ((qualitative == "" || qualitative == "0") && r > 1)
                                    {
                                        if (lastRowIndex == -1)
                                        {
                                            lastRowIndex = r - 1;
                                            span = 2;
                                        }
                                        else
                                        {
                                            span++;
                                        }
                                        tab.Rows[r].Cells.RemoveAt(3);

                                    }
                                    else
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[3].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }

                                    if (r + 1 == tab.Rows.Count)
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[3].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }
                                }


                                //specs
                                lastRowIndex = -1;
                                span = 2;
                                for (int r = 0; r < tab.Rows.Count; r++)
                                {
                                    string specs = tab.Rows[r].Cells[2].Text;
                                    if ((specs == "" || specs == "0") && r > 1)
                                    {
                                        if (lastRowIndex == -1)
                                        {
                                            lastRowIndex = r - 1;
                                            span = 2;
                                        }
                                        else
                                        {
                                            span++;
                                        }
                                        tab.Rows[r].Cells.RemoveAt(2);

                                    }
                                    else
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[2].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }

                                    if (r + 1 == tab.Rows.Count)
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[2].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }
                                }




                                plc_test.Controls.Add(lblUnitAddress);
                                plc_test.Controls.Add(tab);
                                continue;


                            }

                            else if (enterprise == 2) //Service
                            {

                                trDisclaimerGem.Visible = false;


                                int spanSpes = 0;
                                int spanQuat = 0;
                                firstrowspecification = 0;
                                firstrowquantity = 0;
                                Table tab = new Table();
                                tab.BorderStyle = BorderStyle.Inset;
                                tab.CellPadding = 2;
                                tab.CellSpacing = 2;
                                tab.BorderWidth = 2;
                                tab.GridLines = GridLines.Both;
                                TableRow tabRow = new TableRow();
                                TableCell tcSno = new TableCell();
                                TableCell tcStrore = new TableCell();
                                TableCell tcSpecs = new TableCell();
                                TableCell tcQuantitative = new TableCell();
                                tcSno.Text = "<b>Sno<b/>";
                                tcStrore.Text = "<b>Name of Service/ Activities<b/>";
                                tcSpecs.Text = "<b>Definition of Activity/ <br/> Service(s)<b/>";
                                tcQuantitative.Text = "<b>Quantitative Capacity/Services <br/>" + PMPSSTORE2 + "<b/>";

                                tabRow.Cells.Add(tcSno);
                                tabRow.Cells.Add(tcStrore);
                                tabRow.Cells.Add(tcSpecs);
                                tabRow.Cells.Add(tcQuantitative);
                                tabRow.HorizontalAlign = HorizontalAlign.Center;
                                tab.Rows.Add(tabRow);


                                dtFilteredTable.Columns.RemoveAt(3);


                                //int c = 1;
                                foreach (DataRow dr in dtFilteredTable.Rows)
                                {

                                    string store = dr[1].ToString();
                                    string specifation = dr[2].ToString();
                                    string quatitative = dr[3].ToString();


                                    if (store.Contains("\n"))
                                        store = store.Replace("\n", "<br/>");
                                    if (specifation.Contains("\n"))
                                        specifation = specifation.Replace("\n", "<br/>");
                                    if (quatitative.Contains("\n"))
                                        quatitative = quatitative.Replace("\n", "<br/>");
                                    store = HttpUtility.HtmlDecode(store);
                                    specifation = HttpUtility.HtmlDecode(specifation);
                                    quatitative = HttpUtility.HtmlDecode(quatitative);
                                    tabRow = new TableRow();
                                    tcSno = new TableCell();
                                    tcStrore = new TableCell();
                                    tcSpecs = new TableCell();
                                    tcQuantitative = new TableCell();

                                    tcSno.Text = dr[0].ToString();
                                    tcStrore.Text = store;
                                    tabRow.Cells.Add(tcSno);
                                    tabRow.Cells.Add(tcStrore);

                                    tcSpecs.Text = specifation;
                                    tcQuantitative.Text = quatitative;

                                    tabRow.Cells.Add(tcSpecs);
                                    tabRow.Cells.Add(tcQuantitative);




                                    tab.Rows.Add(tabRow);



                                    //c++;
                                }



                                int lastRowIndex = -1;
                                int span = 2;

                                lastRowIndex = -1;
                                span = 2;
                                for (int r = 0; r < tab.Rows.Count; r++)
                                {
                                    string qualitative = tab.Rows[r].Cells[3].Text;
                                    if ((qualitative == "" || qualitative == "0") && r > 1)
                                    {
                                        if (lastRowIndex == -1)
                                        {
                                            lastRowIndex = r - 1;
                                            span = 2;
                                        }
                                        else
                                        {
                                            span++;
                                        }
                                        tab.Rows[r].Cells.RemoveAt(3);

                                    }
                                    else
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[3].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }

                                    if (r + 1 == tab.Rows.Count)
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[3].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }
                                }


                                //specs
                                lastRowIndex = -1;
                                span = 2;
                                for (int r = 0; r < tab.Rows.Count; r++)
                                {
                                    string specs = tab.Rows[r].Cells[2].Text;
                                    if ((specs == "" || specs == "0") && r > 1)
                                    {
                                        if (lastRowIndex == -1)
                                        {
                                            lastRowIndex = r - 1;
                                            span = 2;
                                        }
                                        else
                                        {
                                            span++;
                                        }
                                        tab.Rows[r].Cells.RemoveAt(2);

                                    }
                                    else
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[2].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }

                                    if (r + 1 == tab.Rows.Count)
                                    {
                                        if (lastRowIndex != -1)
                                        {
                                            tab.Rows[lastRowIndex].Cells[2].RowSpan = span;
                                            lastRowIndex = -1;
                                            span = 2;
                                        }
                                    }
                                }




                                plc_test.Controls.Add(lblUnitAddress);
                                plc_test.Controls.Add(tab);
                                continue;




                            }


                            gridviewNewStore.RowDataBound += new GridViewRowEventHandler(gridviewNewStore_RowDataBound);
                            gridviewNewStore.BorderWidth = Unit.Pixel(2);
                            gridviewNewStore.BorderColor = System.Drawing.Color.Black;
                            gridviewNewStore.DataSource = dtFilteredTable;
                            firstrowquantity = 0;
                            gridviewNewStore.DataBind();
                            //  gridviewNewStore.Columns[3].Visible=false;
                            // double height = gridviewNewStore.Height.Value;

                        }//count if

                        else
                        {
                            dtFilteredTable.Dispose();
                            dsAddress.Dispose();
                            gridviewNewStore.DataBind();
                            dsstore.Dispose();

                        }
                        /* tableOutput.Append("</table>");
                         tempHtmlTable.Text = tableOutput.ToString();*/
                        plc_test.Controls.Add(lblUnitAddress);
                        plc_test.Controls.Add(gridviewNewStore);
                        // PlaceHolder1.Controls.Add(lblUnitAddress);
                        //PlaceHolder1.Controls.Add(tempHtmlTable);
                        //  PlaceHolder1.Controls.Add(gridviewNewStore);

                    }//dt.rows.count
                    else
                    {
                        plc_test.Controls.Add(lblUnitAddress);
                        plc_test.Controls.Add(gridviewNewStore);
                    }
                }//for
                 //Count 5 else part   }//else


            }//if ds address
            //}

            else
            {
                Rpt_Address.Dispose();
                Rpt_Address.DataBind();
                dsAddress.Dispose();
            }
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError("Server Busy!!!");
            ErrHandler.WriteError("BindAddress()  " + ex.StackTrace + "  " + ex.Message);
        }
    }

    public void BindTurnOver()
    {
        try
        {
            int CompanyID = ComID;// 3;
            objCompany = new clsCompany();
            DataSet dsTurnover = new DataSet();
            string Registration_No = RegistrationNumber;
            dsTurnover = objCompany.Company_ReportView_TUrnOver(CompanyID);
            var Rowcount = dsTurnover.Tables.Count;
            if (dsTurnover.Tables.Count != 0)
            {

                if (dsTurnover.Tables[0].Rows.Count > 0)
                {
                    if (dsTurnover.Tables[0].Rows.Count > 1)
                    {
                        if (dsTurnover.Tables[0].Columns.Count > 3)
                        {
                            int lastcolumnindex = dsTurnover.Tables[0].Columns.Count - 1;
                            dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[lastcolumnindex]);
                            grdTurnOver.DataSource = dsTurnover.Tables[0];
                            grdTurnOver.DataBind();
                            DataSet ds_consolidatedmonetrylimit = new DataSet();
                            ds_consolidatedmonetrylimit = objCompany.Fetch_ConsolidatedFor_MultipleMonetryLimit(CompanyID);
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
                        else
                        {
                            dsTurnover.Tables[0].Columns[1].ColumnName = "Annual Turnover";
                            dsTurnover.Tables[0].Columns.Remove(dsTurnover.Tables[0].Columns[2]);
                            grdTurnOver.DataSource = dsTurnover.Tables[0];
                            grdTurnOver.DataBind();
                        }
                    }
                    else
                    {
                        grdTurnOver.Dispose();
                        grdTurnOver.DataBind();
                    }
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
    }

    public void BindGrid_Store()
    {
        try
        {
            string Registration_num = RegistrationNumber;
            lblregistration_number.Text = Registration_num.ToString();

            lblstanameStore.Text = lblStateName.Text.ToString().Replace('-', ' ');
            lblCompanyNameStore.Text = " M/s. " + lblNameCom.Text.ToString().Replace(" M/s. ", "") + ',';
            // lbldatestore.Text = Convert.ToString(RegistrationDatefrom.ToString());  //lbldateform.Text.ToString();
            lbldatestore.Text = Convert.ToString(lbldateform.Text.ToString()); //RegistrationDatefrom.ToString();

            int CompanyID = ComID;
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = Registration_num.ToString(); ;
        }
        catch (Exception ex)
        {
            //ErrHandler.WriteError(ex.Message);
            ErrHandler.WriteError("BindGrid_Store()  " + ex.StackTrace + "  " + ex.Message);
        }

    }

    protected void grd_TurnOver_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int GrdTotalCount = 0;
            GrdTotalCount = grdTurnOver.Columns.Count;
            string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
            e.Row.Cells[0].Text = decodedText;
            if (GrdTotalCount > 0)
            {
                if (GrdTotalCount < 4)
                {
                    grdTurnOver.Columns[1].HeaderText = "Annual Turnover";
                    grdTurnOver.Columns[2].Visible = false;
                }

            }
            //e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "100px");
            // e.Row.Cells[3].Style.Add(HtmlTextWriterStyle.Width, "150px");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy");
        }
    }

    protected void grd_Turnover_RowCreated(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                TableCell cell = e.Row.Cells[0];
                cell.Width = new Unit("100px");

                for (int i = 1; i <= e.Row.Cells.Count - 1; i++)
                {

                    TableCell cell2 = e.Row.Cells[i];
                    cell2.Width = new Unit("85px");
                }
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    //Insert Into Registration certificate number into certificate
    public void BindSerialNumber()
    {
        try
        {
            DataSet ds_serial_no = new DataSet();
            objCompany = new clsCompany();
            ds_serial_no = objCompany.Company_Registration_Certificate_fetch_serial_no(BranchId);
            if (ds_serial_no.Tables[0].Rows.Count > 0)
            {
                SerialNoAdd = ds_serial_no.Tables[0].Rows[0]["serial"].ToString();
                // lbl_serail_add_no.Text = SerialNoAdd.ToString();
            }
            else
            {
                ds_serial_no.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("BindSerialNumber()  " + ex.StackTrace + "  " + ex.Message);
        }
    }

    public void Bind_Branch_office_details()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds_branchofficedetails = new DataSet();
            ds_branchofficedetails = objCompany.Fetch_Branch_office_address_details(BranchId);
            if (ds_branchofficedetails.Tables[0].Rows.Count > 0)
            {
                lbl_branch_address.Text = ds_branchofficedetails.Tables[0].Rows[0]["BranchAddress"].ToString();
                lbl_branch_phone.Text = ds_branchofficedetails.Tables[0].Rows[0]["Phone"].ToString();
                lbl_branch_fax.Text = ds_branchofficedetails.Tables[0].Rows[0]["Fax"].ToString();
                lbl_branch_email.Text = ds_branchofficedetails.Tables[0].Rows[0]["Email"].ToString();


                //lbl_branchaddress.Text = lbl_branch_address.Text;
                //lbl_phoneAddress.Text = lbl_branch_phone.Text;
                //lbl_FaxAddress.Text = lbl_branch_fax.Text;
                //lbl_EmailAddress.Text = lbl_branch_email.Text;


            }

            else
            {
                ds_branchofficedetails.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Bind_Branch_office_details()  " + ex.StackTrace + "  " + ex.Message);
        }
    }

    public void BindstoreDetailsinsb(int UnitID)
    {
        try
        {
            DataSet dsstore = new DataSet();
            DataTable dt = new DataTable();
            GridView gvstore_details = new GridView();

            dsstore = objCompany.Fetch_company_Store_Certificate_Modified(UnitID);
            dt = dsstore.Tables[0];

            if (dt.Rows.Count > 0)
            {
                DataTable dtFilteredTable = new DataTable();
                var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).Count();
                int countStatus = Convert.ToInt32(count);
                if (count > 0)
                {
                    dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).CopyToDataTable();
                    dtFilteredTable.Columns.Remove("UnitID");
                    string GvstorePMPS = dtFilteredTable.Rows[0]["PMPS"].ToString().ToUpper();
                    Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[1].ToString()].ToString());
                    Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[2].ToString()].ToString());
                    Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());

                    //  HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());
                    HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Quantitative Capacity"].ToString());
                    string PMPSSTORE = "";
                    if (GvstorePMPS == "PMPS" || GvstorePMPS == "pmps" || GvstorePMPS == "Pmps" || GvstorePMPS == "PMps")
                    {
                        PMPSSTORE = "P.M.P.S.";
                    }
                    else if (GvstorePMPS == "PM" || GvstorePMPS == "Pm" || GvstorePMPS == "pm")
                    {
                        PMPSSTORE = "P.M.";
                    }
                    else if (GvstorePMPS == "PA" || GvstorePMPS == "Pa" || GvstorePMPS == "pa")
                    {
                        PMPSSTORE = "P.A.";
                    }

                    dtFilteredTable.Columns[4].ColumnName = dtFilteredTable.Columns[4].ColumnName + ' ' + PMPSSTORE.ToString();

                    dtFilteredTable.Columns.Remove("PMPS");

                    //Label tempHtmlTable = new Label();
                    //StringBuilder tableOutput = new StringBuilder("<table class='tblrowcolor'  width='670' border='1'  id='tbltest'");
                    //tableOutput.Append("<tr><td rowspan ='2'><b>DYNAMIC TABLE.</b></td></tr>");
                    //tableOutput.Append("<tr ><td>SNo </td><td>Store Name</td> <td> Specification </td> <td> Qualitative Capacity </td> <td> Quantitative Capacity </td></tr>");

                    //string dtlengh = string.Empty;
                    //if (dtFilteredTable.Rows.Count > 0)
                    //{

                    //    foreach (DataRow dr in dtFilteredTable.Rows)
                    //    {
                    //         tableOutput.Append("<tr class='RowClass'><td>"
                    //                                   +  dr["SNo"].ToString()+
                    //                                 "</td><td>"+ Server.HtmlDecode(dr["Store Name"].ToString())+
                    //                                "</td><td>"+  Server.HtmlDecode(dr["Specification"].ToString())+
                    //                                "</td><td>"  + Server.HtmlDecode(dr["Qualitative Capacity"].ToString())
                    //                                  + "</td><td>"+Server.HtmlDecode(dr["Quantitative Capacity"].ToString()) +"</td></tr>");
                    //         int length = tableOutput.Length;
                    //         if (length > 1224)
                    //         {
                    //             tableOutput.Append("<tr><td colspan='5' style='height:300px;'>&nbsp</td></tr>");
                    //         }
                    //    }


                    //}
                    //else
                    //{
                    //    dtFilteredTable.Dispose();
                    //}

                }
            }

        }
        catch (Exception ex)
        {
        }
    }


    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    protected void gridviewNewStore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridvwStoreRBound = sender as GridView;
            int cellcount = 0;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                // e.Row.Attributes.Add("onlcick", "Javascript:GetHeaderText('" + gridvwStoreRBound.ClientID + "'");
                e.Row.Cells[1].Text = "Store(s)/" + Environment.NewLine + "Service(s) Name";
                string columnone = e.Row.Cells[1].Text.ToString();
                string strarrcolumn1 = columnone.Replace("\\r\n", "<br/>");
                e.Row.Cells[1].Text = strarrcolumn1.ToString();
                string columnfour = e.Row.Cells[4].Text.ToString();
                string strarr = columnfour.Replace("\r\n", "<br/>");
                e.Row.Cells[4].Text = strarr.ToString();

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //===========CODE FOR MERGING START=========================
                DataRowView drv = (DataRowView)e.Row.DataItem;


                if (drv["Store(s)/ Service(s) Name"].ToString().Contains("\n"))
                {
                    e.Row.Cells[1].Text = drv["Store(s)/ Service(s) Name"].ToString().Replace("\n", "<br/>");
                }

                if (drv["Specification(s)"].ToString().Contains("\n"))
                {
                    e.Row.Cells[2].Text = drv["Specification(s)"].ToString().Replace("\n", "<br/>");
                }

                if (e.Row.Cells.Count > 4 && drv["Qualitative Capacity"].ToString().Contains("\n"))  // ra CHECK THE COLUMN
                {
                    e.Row.Cells[3].Text = drv["Qualitative Capacity"].ToString().Replace("\n", "<br/>");
                }

                if (e.Row.Cells.Count > 4 && drv[drv.DataView.Table.Columns[4].ToString()].ToString().Contains("\n"))
                {
                    e.Row.Cells[4].Text = drv[drv.DataView.Table.Columns[4].ToString()].ToString().Replace("\n", "<br/>");
                }
                else
                {
                    e.Row.Cells[3].Text = drv[drv.DataView.Table.Columns[3].ToString()].ToString().Replace("\n", "<br/>");
                }

                if (e.Row.RowIndex > 0)
                {
                    if (drv["Specification(s)"].ToString() == "0")
                    {
                        //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                        if (gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan == 0)
                        {
                            gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan = 2;
                        }
                        else
                        {
                            gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan += 1;
                        }

                        //Remove the cell
                        //e.Row.Cells.RemoveAt(3);
                        e.Row.Cells[2].Visible = false;
                    }

                    else //IT'S NEW VALUE
                    {
                        //Set the vertical alignment to top
                        e.Row.VerticalAlign = VerticalAlign.Top;
                        firstrowspecification = e.Row.RowIndex;
                    }

                    if (e.Row.Cells.Count > 4 && drv["Qualitative Capacity"].ToString() == "0")
                    {
                        //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                        if (gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan == 0)
                        {
                            gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan = 2;
                        }
                        else
                        {
                            gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan += 1;
                        }
                        //Remove the cell
                        cellcount = e.Row.Cells.Count;
                        //e.Row.Cells.RemoveAt(cellcount - 4);
                        e.Row.Cells[cellcount - 2].Visible = false;
                    }

                    else //IT'S NEW VALUE
                    {
                        //Set the vertical alignment to top
                        e.Row.VerticalAlign = VerticalAlign.Top;
                        firstrowquality = e.Row.RowIndex;
                    }

                    if (e.Row.Cells.Count > 4 && drv[drv.DataView.Table.Columns[4].ToString()].ToString() == "0")
                    {
                        //drv["Quantitative Capacity P.M.P.S."].ToString()
                        //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

                        if (gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan == 0)
                        {
                            gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan = 2;
                        }
                        else
                        {
                            gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan += 1;
                        }
                        //Remove the cell

                        cellcount = e.Row.Cells.Count;
                        //e.Row.Cells.RemoveAt(cellcount-3);
                        e.Row.Cells[cellcount - 1].Visible = false;


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
            MakeGridViewPrinterFriendly(gridvwStoreRBound);




            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }


    //int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    //protected void gridviewNewStore_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        GridView gridvwStoreRBound = sender as GridView;
    //        int cellcount = 0;
    //        if (e.Row.RowType == DataControlRowType.Header)
    //        {
    //            //e.Row.Attributes.Add("onclick", "Javascript:GetHeaderText('" + gridvwStoreRBound.ClientID + "'");
    //            e.Row.Cells[1].Text = HttpUtility.HtmlDecode("Store(s)/" + Environment.NewLine + "Service(s) Name");
    //            e.Row.Cells[1].Text = "Store(s)/" + Environment.NewLine + "Service(s) Name";
    //            string columnone = e.Row.Cells[1].Text.ToString();
    //            string strarrcolumn1 = columnone.Replace("\\r\n", "<br/>");
    //            e.Row.Cells[1].Text = strarrcolumn1.ToString();
    //            string columnfour = e.Row.Cells[4].Text.ToString();
    //            string strarr = columnfour.Replace("\r\n", "<br/>");
    //            e.Row.Cells[4].Text = strarr.ToString();
    //        }

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //===========CODE FOR MERGING START=========================
    //            DataRowView drv = (DataRowView)e.Row.DataItem;


    //            if (drv["Store(s)/ Service(s) Name"].ToString().Contains("\n"))
    //            {
    //                e.Row.Cells[1].Text = drv["Store(s)/ Service(s) Name"].ToString().Replace("\n", "<br/>");
    //            }

    //            if (drv["Specification(s)"].ToString().Contains("\n"))
    //            {
    //                e.Row.Cells[2].Text = drv["Specification(s)"].ToString().Replace("\n", "<br/>");
    //            }

    //            if (drv["Qualitative Capacity"].ToString().Contains("\n"))
    //            {
    //                e.Row.Cells[3].Text = drv["Qualitative Capacity"].ToString().Replace("\n", "<br/>");
    //            }

    //            if (drv[drv.DataView.Table.Columns[4].ToString()].ToString().Contains("\n"))
    //            {
    //                // drv[drv.DataView.Table.Columns[4].ToString()].ToString().Contains("\n") 
    //                //drv["Quantitative Capacity P.M.P.S."]
    //                // drvd["Quantitative Capacity P.M.P.S."].ToString()
    //                e.Row.Cells[4].Text = drv[drv.DataView.Table.Columns[4].ToString()].ToString().Replace("\n", "<br/>");
    //            }

    //            if (e.Row.RowIndex > 0)
    //            {
    //                if (drv["Specification(s)"].ToString() == "0")
    //                {
    //                    //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

    //                    if (gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan == 0)
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan = 2;
    //                    }
    //                    else
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowspecification].Cells[2].RowSpan += 1;
    //                    }

    //                    //Remove the cell
    //                    //e.Row.Cells.RemoveAt(3);
    //                    e.Row.Cells[2].Visible = false;
    //                }

    //                else //IT'S NEW VALUE
    //                {
    //                    //Set the vertical alignment to top
    //                    e.Row.VerticalAlign = VerticalAlign.Top;
    //                    firstrowspecification = e.Row.RowIndex;
    //                }

    //                if (drv["Qualitative Capacity"].ToString() == "0")
    //                {
    //                    //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

    //                    if (gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan == 0)
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan = 2;
    //                    }
    //                    else
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowquality].Cells[3].RowSpan += 1;
    //                    }
    //                    //Remove the cell
    //                    cellcount = e.Row.Cells.Count;
    //                    //e.Row.Cells.RemoveAt(cellcount - 4);
    //                    e.Row.Cells[cellcount - 2].Visible = false;
    //                }

    //                else //IT'S NEW VALUE
    //                {
    //                    //Set the vertical alignment to top
    //                    e.Row.VerticalAlign = VerticalAlign.Top;
    //                    firstrowquality = e.Row.RowIndex;
    //                }

    //                //if (drv[drv.DataView.Table.Columns[4].ToString()].ToString() == "0")
    //                //{
    //                //    //drv["Quantitative Capacity P.M.P.S."].ToString()
    //                //    //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

    //                //    if (gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan == 0)
    //                //    {
    //                //        gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan = 2;

    //                //    }
    //                //    else
    //                //    {
    //                //        gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan += 1;

    //                //    }
    //                if (drv[drv.DataView.Table.Columns[4].ToString()].ToString() == "0")
    //                {
    //                    //drv["Quantitative Capacity P.M.P.S."].ToString()
    //                    //IF IT'S VALUE=0 INCREMENT THE ROWSPAN

    //                    if (gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan == 0)
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan = 2;
    //                    }
    //                    else
    //                    {
    //                        gridvwStoreRBound.Rows[firstrowquantity].Cells[4].RowSpan += 1;
    //                    }
    //                    //Remove the cell

    //                    cellcount = e.Row.Cells.Count;
    //                    //e.Row.Cells.RemoveAt(cellcount-3);
    //                    e.Row.Cells[cellcount - 1].Visible = false;


    //                }

    //                else //IT'S A NEW VALUE
    //                {
    //                    //Set the vertical alignment to top
    //                    e.Row.VerticalAlign = VerticalAlign.Top;
    //                    firstrowquantity = e.Row.RowIndex;
    //                }
    //            }

    //            //===========CODE FOR MERGING END

    //        }
    //        MakeGridViewPrinterFriendly(gridvwStoreRBound);




    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError("" + ex.Message.ToString());
    //    }

    //}
    private void MakeGridViewPrinterFriendly(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            gridView.UseAccessibleHeader = true;
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridView.FooterRow.TableSection = TableRowSection.TableFooter;

            //  gridView.HeaderRow.Style["display"] = "table-header-group";
            // gridView.FooterRow.Style["display"] = "table-header-group";
        }
    }


    public void setComment(int BranchId, int CompanyID)
    {
        try
        {
            DataSet ds = new DataSet();
            ds = Company_Fetch_Certificate_Search_OneMonth(BranchId, CompanyID);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["FormtypeId"]) == 1 || Convert.ToInt32(ds.Tables[0].Rows[0]["FormtypeId"]) == 3)
                {
                    string InspectionAgencyName = "";
                    string InspectionAgencyNameCheck = "";
                    int countCheck = 1;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if (countCheck == 1)
                        {
                            InspectionAgencyName = dr["Name_Comment"].ToString();
                            if (countCheck == 1)
                            {
                                InspectionAgencyNameCheck = dr["Name_Comment"].ToString();
                            }
                            countCheck = countCheck + 1;

                            //  InspectionAgencyNameCheck= dr["Name_Comment"].ToString();
                        }
                        else
                        {
                            InspectionAgencyName += "," + dr["Name_Comment"].ToString();// + ',' + InspectionAgencyName;
                        }

                        if (ds.Tables[0].Rows.Count == 2)
                        {
                            if (InspectionAgencyNameCheck == dr["Name_Comment"].ToString())
                            {
                                lbl_Comment_text.Text = dr["Name_Comment"].ToString() + "<br /> Comments / Note<br />(Optional):" + dr["Comment"].ToString() + " ";
                            }
                        }
                        else
                        {
                            lbl_Comment_text.Text = InspectionAgencyName.ToString() + "<br /> Comments / Note<br />(Optional):" + dr["Comment"].ToString() + " ";
                        }



                        //if (InspectionAgencyName != "")

                    }


                    //if (ds.Tables[0].Rows.Count > 1)
                    //{
                    //    lbl_Comment_text.Text = InspectionAgencyName.ToString() + "<br /> Comments / Note<br />(Optional):" + " ";
                    //}
                    //else
                    //{ 

                    //    lbl_Comment_text.Text = ds.Tables[0].Rows[0]["Name_Comment"].ToString() + "<br /> Comments / Note<br />(Optional):" + " "    ;
                    //}

                }
                else
                {
                    lbl_Comment_text.Text = " Comments / Note<br />(Optional):" + " " + " " + ds.Tables[0].Rows[0]["Name_Comment"].ToString();
                }


            }
            else
            {
                ds.Dispose();
                lbl_Comment_text.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }


    public void setComment(int CompanyID)
    {
        try
        {
            objCompany = new clsCompany();
            DataSet ds_fetch_comment = new DataSet();
            ds_fetch_comment = objCompany.Fetch_Comment_on_certificatetable(CompanyID);
            if (ds_fetch_comment.Tables[0].Rows.Count > 0)
            {
                lbl_Comment_text.Text = "Inspection agency<br /> comments / Note<br />(Optional):" + " " + " " + ds_fetch_comment.Tables[0].Rows[0]["Coment"].ToString();
            }
            else
            {
                ds_fetch_comment.Dispose();
                lbl_Comment_text.Text = "";
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString() + "" + ex.StackTrace.ToString());
        }
    }

    public DataSet Company_Fetch_Certificate_Search_OneMonth(int branchid, int CompanyId)
    {
        SqlCommand MySqlComm;
        SqlDataAdapter MyAdp = new SqlDataAdapter();
        SqlDataReader MyDr;
        DataSet MyDs = new DataSet();
        DataTable MyDt;

        MyDt = new DataTable();
        try
        {
            MyAdp = new SqlDataAdapter("GPsp_InsepectionDone_By_Name_For_certificate", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.CommandTimeout = 240;
            MyAdp.SelectCommand.Parameters.Add("@BranchId", SqlDbType.Int).Value = branchid;
            MyAdp.SelectCommand.Parameters.Add("@CompanyId", SqlDbType.Int).Value = CompanyId;
            MyAdp.Fill(MyDs);
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
        return MyDs;
    }
}
