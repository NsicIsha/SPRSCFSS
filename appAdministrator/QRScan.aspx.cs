using Newtonsoft.Json.Linq;
using NSICGP_VBClassLibrary;
using Num2Wrd;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing.QrCode.Internal;


public partial class appAdministrator_QRScan : System.Web.UI.Page
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
            UserID = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).Replace(" ", "+")).ToString());
            RoleId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["ROLEID"]).ToString());
            DepartmentId = Convert.ToInt32(objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).Replace(" ", "+")).ToString());// Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["DEPARTMENTID"]).ToString());
            UserFullName = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERFULLNAME"]).Replace(" ", "+")).ToString();
            UserEmailID = objDecEnc.Decrypt(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["UserEmailId"]).Replace(" ", "+")).ToString();

        }
        catch
        {
            UserID = 0;
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

            FunRequest();

            DecryptEncrypt objDecEnc = new DecryptEncrypt();
            BranchId = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["b"].Replace(" ", "+")).ToString());
            Branch_Code = objDecEnc.Decrypt(Request.QueryString["d"].Replace(" ", "+")).ToString();
            objCompany = new clsCompany();
            Hashtable hashtbl = new Hashtable();
            ComID = Convert.ToInt32(objDecEnc.Decrypt(Request.QueryString["c"].Replace(" ", "+")).ToString());
            hashtbl = objCompany.Company_Fetch_RegNo_ByCompanyId(ComID);
            RegistrationNumber = hashtbl["regno"].ToString();
            int prntaccess = Convert.ToInt32(hashtbl["pntaccess"]);

            if (!IsPostBack)
            {
                Show_Certificatcate_Registration();
                BindAddress();

            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }

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

            //Label lblValidity = grdCommon.FindControl("lblValidity") as Label;




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
                objCompany = new clsCompany();
                DataSet ds_fetch_firstprint = new DataSet();
                ds_fetch_firstprint = objCompany.Fetch_printout_date_modified(Convert.ToInt32(CompanyID));
                if (ds_fetch_firstprint.Tables[0].Rows.Count > 0)
                {

                    if (lblDOI.Text == "01/01/1900")
                    {

                        lblDOI.Text = System.DateTime.Today.Date.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        lblDOI.Text = Convert.ToString((ds_fetch_firstprint.Tables[0].Rows[0]["FirstPrintDate"].ToString()));
                    }                    

                }//1
                else
                {
                    ds_fetch_firstprint.Dispose();
                }



                RegistrationDatefrom = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
                lblUnitName.Text = " M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();              
                lblAddress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString() + " " + ds.Tables[0].Rows[0]["CityName"].ToString() + "," + ds.Tables[0].Rows[0]["StateName"].ToString() + "-" + ds.Tables[0].Rows[0]["pinCode"].ToString();
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
                        lblValidity.Text = Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyyy");
                        BindTurnOver();                        
                    }
                    else
                    {
                        lblValidity.Text = Convert.ToString(RegistrationDatefrom.ToString()) + " to" + " " + dat1.AddYears(1).AddDays(-1).ToString("dd/MM/yyyy");
                    }
                }
                else
                {
                    lblValidity.Text = Convert.ToString(dsIsGraduate.Tables[0].Rows[0]["fGPtbal_From_Date"].ToString()) + " to" + " " + dsIsGraduate.Tables[0].Rows[0]["fGPtbal_To_Date"].ToString();
                    BindTurnOver();

                }


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


                    lbltrunover.Text = " " + p + "  " + "lakhs" + " ";

                }
                else
                {
                    lbltrunover.Text = ". 0 lakhs ";                    
                }

                lblRegistration.Text = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
                lblSocialClass.Text = ds.Tables[0].Rows[0]["CategoryCast"].ToString();
                lblSpl.Text = ds.Tables[0].Rows[0]["fGPtbl_CUnit_CA_Name"].ToString();
                lblConstitution.Text = ds.Tables[0].Rows[0]["ConstitutionName1"].ToString();
                lblUdyam.Text = ds.Tables[0].Rows[0]["UAM"].ToString();
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

                else
                {
                    lbl_name_of_DIRECTOR.Text = "Directors";
                }
            }
        }
        catch (Exception ex)
        {

            ErrHandler.WriteError("Show_Certificatcate_Registration()  " + ex.StackTrace + "  " + ex.Message);
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
                                lbltrunover.Text = "Consolidated Monetary Limit" + " " + "Rs." + consolidate.ToString() + " " + "Lakhs";
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
    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    public void BindAddress()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsAddress = new DataSet();
            int CompanyID = ComID;
            dsAddress = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset_AddressFactory(CompanyID);
            if (dsAddress.Tables[0].Rows.Count > 0)
            {
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

                    
                    Label tempHtmlTable = new Label();

                    dsstore = Fetch_company_Store_Certificate_Modified(UnitID);
                    dt = dsstore.Tables[0];
                    gridviewNewStore = new GridView();
                    gridviewNewStore.CssClass = "gridviewstore";
                    if (dt.Rows.Count > 0)
                    {
                        DataTable dtFilteredTable = new DataTable();
                        var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).Count();
                        int countStatus = Convert.ToInt32(count);
                        string GvstorePMPS = string.Empty;
                        if (count > 0)
                        {// count if
                            dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).CopyToDataTable();
                            dtFilteredTable.Columns.Remove("UnitID");
                            GvstorePMPS = dtFilteredTable.Rows[0]["PMPS"].ToString().ToUpper();
                            //grdTestMan.Columns[4].HeaderText = "Quantitative Capacity" + "("+GvstorePMPS+")";
                            //grdTestServ.Columns[3].HeaderText= "Quantitative Capacity/Services" + "( "  + GvstorePMPS +  ")";
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[1].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0][dtFilteredTable.Columns[2].ToString()].ToString());
                            Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());                           
                            HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Quantitative Capacity"].ToString());

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
                            
                            dtFilteredTable.Columns[4].ColumnName = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString() + Environment.NewLine + PMPSSTORE.ToString()); //HttpUtility.HtmlDecode(lblquntitycapacity.Text.ToString());
                            string Quantitative_Capacity = HttpUtility.HtmlDecode(dtFilteredTable.Columns[4].ColumnName.ToString());

                            dtFilteredTable.Columns.Remove("PMPS");
                            DataTable dtNEW = new DataTable();
                            if (enterprise == 1) //manufac
                            {
                                //grdTestMan.Visible = true;
                                //grdTestMan.DataSource = dt;
                                //grdTestMan.DataBind();

                                GridView objGV = new GridView();
                                objGV.AutoGenerateColumns = false;
                                objGV.CssClass = "table table-responsive table-striped table-bordered table-hover dataTables-example";

                                BoundField field = new BoundField();
                                field.HeaderText = "S.No";
                                field.DataField = "SNo";
                                objGV.Columns.Add(field);

                                BoundField field1 = new BoundField();
                                field1.HeaderText = "Store(s)/ Service(s) Name";
                                field1.DataField = "Store(s)/ Service(s) Name";
                                objGV.Columns.Add(field1);

                                BoundField field2 = new BoundField();
                                field2.HeaderText = "Specification(s)";
                                field2.DataField = "Specification(s)";
                                objGV.Columns.Add(field2);

                                BoundField field3 = new BoundField();
                                field3.HeaderText = "Qualitative Capacity";
                                field3.DataField = "Qualitative Capacity";
                                objGV.Columns.Add(field3);

                                BoundField field4 = new BoundField();
                                field4.HeaderText = "Quantitative Capacity" + GvstorePMPS;
                                field4.DataField = "Quantitative Capacity";
                                objGV.Columns.Add(field4);

                                BoundField field5 = new BoundField();
                                field5.HeaderText = "Remarks";
                                field5.DataField = "fGPtbl_CUnit_Store_ManRemarks";
                                objGV.Columns.Add(field5);
                                

                                objGV.DataSource = dt;
                                objGV.DataBind();

                                plc_test.Controls.Add(lblUnitAddress);
                               // plc_test.Controls.Add(grdTestMan);
                                plc_test.Controls.Add(objGV);                             

                            }

                            else if (enterprise == 2) //Service
                            {
                                //grdTestServ.Visible = true;
                                //grdTestServ.DataSource = dt;
                                //grdTestServ.DataBind();

                                GridView objGV = new GridView();
                                objGV.AutoGenerateColumns = false;
                                objGV.CssClass = "table table-responsive table-striped table-bordered table-hover dataTables-example";

                                BoundField field = new BoundField();
                                field.HeaderText = "S.No";
                                field.DataField = "SNo";
                                objGV.Columns.Add(field);

                                BoundField field1 = new BoundField();
                                field1.HeaderText = "Name of Service/Activities";
                                field1.DataField = "Store(s)/ Service(s) Name";
                                objGV.Columns.Add(field1);

                                BoundField field2 = new BoundField();
                                field2.HeaderText = "Definition of Activity/Services(s)";
                                field2.DataField = "Specification(s)";
                                objGV.Columns.Add(field2);
                               
                                BoundField field4 = new BoundField();
                                field4.HeaderText = "Quantitative Capacity" + GvstorePMPS;
                                field4.DataField = "Quantitative Capacity";
                                objGV.Columns.Add(field4);

                                objGV.DataSource = dt;
                                objGV.DataBind();

                                plc_test.Controls.Add(lblUnitAddress);
                                //plc_test.Controls.Add(grdTestServ);
                                plc_test.Controls.Add(objGV);
                            }


                           // gridviewNewStore.RowDataBound += new GridViewRowEventHandler(gridviewNewStore_RowDataBound);
                            gridviewNewStore.BorderWidth = Unit.Pixel(2);
                            gridviewNewStore.BorderColor = System.Drawing.Color.Black;
                            gridviewNewStore.DataSource = dtFilteredTable;
                            firstrowquantity = 0;
                            gridviewNewStore.DataBind();
                            
                        }//count if

                        else
                        {
                            dtFilteredTable.Dispose();
                            dsAddress.Dispose();
                            gridviewNewStore.DataBind();
                            dsstore.Dispose();

                        }                        

                    }
                    else
                    {
                        
                    }                   

                }
            }

            else
            {
                Rpt_Address.Dispose();
                Rpt_Address.DataBind();
                dsAddress.Dispose();
            }

        }
        catch (Exception ex)
        {
            
        }
    }
    SqlDataAdapter MyAdp;
    DataSet MyDs;
    public DataSet Fetch_company_Store_Certificate_Modified(int UnitID)
    {
       
        try
        {
            MyDs = new DataSet();
            MyAdp = new SqlDataAdapter("Gps_CertificateResigaration_Store_Modified_QR", DBManager.ConnectToDB());
            MyAdp.SelectCommand.CommandType = CommandType.StoredProcedure;
            MyAdp.SelectCommand.Parameters.Add("@UnitID", SqlDbType.Int).Value = UnitID;

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
    
   }



