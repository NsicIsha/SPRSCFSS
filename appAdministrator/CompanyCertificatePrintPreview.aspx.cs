using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Num2Wrd;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using NSICGP_VBClassLibrary;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Text;

public partial class CompanyCertificatePrintPreview : System.Web.UI.Page
{
    clsCompany objCompany;
    string[] Name;
    string[] SI;
    string NameOfPartner;
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 20;
    int UserID, BranchId;
    string SerialNumber;
    string RegistrationNumber = string.Empty;
    public string SerialNoAdd;
    string Branch_Code = string.Empty;

    private string serial_NUMBER
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            UserID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
            clsTrans_UserBranch objtub = new clsTrans_UserBranch();
            BranchId = Convert.ToInt32(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["fGPtbl_TUB_BrabchId"]);
           // lbl_Branch_Name.Text = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["BranchName"]);
            Branch_Code = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["fGPtbl_MB_Code"]);
            objCompany = new clsCompany();
            Hashtable hashtbl = new Hashtable();

            hashtbl = objCompany.Company_Fetch_RegNo_ByCompanyId(Convert.ToInt32(Request.QueryString["id"]));
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
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
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
                lbldateform.Text = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
                // lblFactoryAddress.Text = ds.Tables[0].Rows[0]["FactoryAddress"].ToString();
                lblNameCom.Text = " M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();
                LblCompanyAdress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                lbl_city_name.Text = ds.Tables[0].Rows[0]["CityName"].ToString() + ",";
                lblStateName.Text = ds.Tables[0].Rows[0]["StateName"].ToString() + "-";
                lblcompanypin.Text = ds.Tables[0].Rows[0]["pinCode"].ToString();
                lbl_sc_st.Text = ds.Tables[0].Rows[0]["CategoryCast "].ToString();
                string ProvisinalCertificate = ds.Tables[0].Rows[0]["ProvisnalCertificate"].ToString();

                string PartnerName = hdnlablName.Value;
                CultureInfo ci = new CultureInfo("en-GB");

                DateTime dat1 = Convert.ToDateTime(lbldateform.Text, ci);
                if (ProvisinalCertificate == "0")
                {
                    lblvalidToFrom.Text = "<b>" + "(" + "Valid From " + lbldateform.Text + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyy") + ")" + "</b>";
                    BindTurnOver();
                  //  lbl_validform_store_details.Text = lblvalidToFrom.Text;
                }
                else
                {
                    lblvalidToFrom.Text = "<b>" + "(" + "Valid From " + lbldateform.Text + " to" + " " + dat1.AddYears(1).AddDays(-1).ToString("dd/MM/yyy") + ")" + "</b>";
                    lbltrunover.Visible = false;
                  //  lbl_validform_store_details.Text = lblvalidToFrom.Text;
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
                int CompanyIDMonitory = Convert.ToInt32(Request.QueryString["id"]);
                ds_monotory_limit = objCompany.Company_Certificate_MonoteryLimit(CompanyIDMonitory);
                if (ds_monotory_limit.Tables[0].Rows.Count > 0)
                {

                    lblMonLimit.Text = " " + ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString() + "  " + "lakhs" + " " + "(";

                    string NumberText = ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString();
                    NumberToEnglish numToeng = new NumberToEnglish();
                    string rupeestext = numToeng.changeCurrencyToWords(NumberText);
                    lblTextRupee.Text = " " + rupeestext + ")";
                }
                else
                {
                    lblMonLimit.Text = ". 0 lakhs (";
                    lblTextRupee.Text = " Zero lakhs Only )";
                    ds_monotory_limit.Dispose();
                }

                lblRegisNo.Text = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
                lbl_old_registration_no.Text = ds.Tables[0].Rows[0]["old_RegistrationNumber"].ToString();

                SI = lblRegisNo.Text.ToString().Split('/');
                //  string sampleNum = "00000000"+ (incNumber++).ToString();
                SerialNumber = Branch_Code + "-" + SerialNoAdd; //"SPR-"+generateID("www.nsicspronline.com");//SI[3] + "/" + SI[4];
                lblserialNo.Text = SerialNumber.ToString();
                //lblserial_Store.Text = SerialNumber.ToString();
                serial_NUMBER = SerialNumber;
                lblconstitutionCode.Text = ds.Tables[0].Rows[0]["ConstitutionName"].ToString().Replace("<br>", "");
                string constitition_name = lblconstitutionCode.Text.ToString();
                if (constitition_name == "Public" || constitition_name == "Private")
                {
                    lbl_name_of_DIRECTOR.Text = "Directors";
                }
                else if (constitition_name == "Partnership")
                {
                    lbl_name_of_DIRECTOR.Text = "Partners";
                }
                else if (constitition_name == "Proprietorship" || constitition_name == "Hindu Undivided Family")
                {
                    lbl_name_of_DIRECTOR.Text = "Proprietor";
                }
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
            ErrHandler.WriteError("Server Busy!!!!");
        }

    }

    public void BindGridCertificate()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = RegistrationNumber;
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
            dsCertificate_store = objCompany.Fetch_company_Store_Certificate(CompanyID);
            if (dsCertificate_store.Tables[0].Rows.Count > 0)
            {

                lblGridCount.Text = "(" + dsCertificate_store.Tables[0].Rows.Count + "&nbsp; " + "item only" + " )";

                string PerMOnthPERShift = dsCertificate_store.Tables[0].Rows[0]["PMPs"].ToString().ToUpper();
                if (PerMOnthPERShift == "PMPS")
                {
                    lbl_Quantitive_capacity.Text = "P.M.P.S.";
                }
                else if(PerMOnthPERShift == "P.M.")
                {
                    lbl_Quantitive_capacity.Text = "P.M.";
                }
                else if (PerMOnthPERShift == "P.A.")
                {
                    lbl_Quantitive_capacity.Text = "P.A.";
                }
            }
            else
            {
                grdstores.Dispose();
                grdstores.DataBind();
                dsCertificate_store.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }

    }

    public void BindAddress()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsAddress = new DataSet();
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);

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
                        Label lblUnitAddress = new Label();
                        lblUnitAddress.CssClass = "LabelPrintCertificate";  //

                        if (dsAddress.Tables[0].Rows.Count < 2)
                        {
                            lblUnitAddress.Text = "<br/>" + "  " + FactoryAddress.Replace('.', ' ') + ','+ "  " + CityName + ' '  +StateName + Zip + "<br/><br/>";
                        }
                        else
                        {
                            lblUnitAddress.Text = "<br/>" + "<b>UNIT-</b>" + SNO + ')' + FactoryAddress.Replace('.', ' ') + ',' +" " + CityName + ' ' + StateName + Zip + "<br/><br/>";
                        }

                        DataSet dsstore = new DataSet();
                        DataTable dt = new DataTable();

                        //CODE TO ADD A GRIDVIEW
                        GridView gridviewNewStore;
                       
                        //CODE TO ADD A GRIDVIEW

                        // GridView gvstore_details = new GridView();
                        Label tempHtmlTable = new Label();

                        dsstore = objCompany.Fetch_company_Store_Certificate_Modified(UnitID);
                        dt = dsstore.Tables[0];

                        if (dt.Rows.Count > 0)
                        {//dt.rows.count
                            //
                            gridviewNewStore = new GridView();
                            gridviewNewStore.CssClass = "gridviewstore";
                            //

                            DataTable dtFilteredTable = new DataTable();
                            var count = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).Count();
                            int countStatus = Convert.ToInt32(count);
                            if (count > 0)
                            {// count if
                                dtFilteredTable = dt.AsEnumerable().Where(a => Convert.ToInt32(a["UnitID"].ToString()) == UnitID).CopyToDataTable();
                                dtFilteredTable.Columns.Remove("UnitID");
                                string GvstorePMPS = dtFilteredTable.Rows[0]["PMPS"].ToString().ToUpper();
                                Server.HtmlDecode(dtFilteredTable.Rows[0]["Store Name"].ToString());
                                Server.HtmlDecode(dtFilteredTable.Rows[0]["Specification"].ToString());
                                Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());

                                //  HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());
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
                                dtFilteredTable.Columns[4].ColumnName = (dtFilteredTable.Columns[4].ColumnName + ' ' + PMPSSTORE.ToString());
                                string Quantitative_Capacity = dtFilteredTable.Columns[4].ColumnName;

                                dtFilteredTable.Columns.Remove("PMPS");

                                /* StringBuilder tableOutput = new StringBuilder("<table   border='1' cellspacing='1'   cellpadding='2'  id='tbltest'>");
                                 //  tableOutput.Append("<tr><td rowspan ='2'><b>DYNAMIC TABLE.</b></td></tr>"); align='center' style='width:100%; overflow: hidden;style='display:inline-block;'
                                 tableOutput.Append("<tr  ><td style='width:30px;' >SNo </td><td   >Store Name </td> <td > Specification </td> <td > Qualitative Capacity </td> <td > " + Quantitative_Capacity + "   </td></tr>");
                                 int RowSpan_specification=0;
                                 int Rowspan_QualityCapacity=0;
                                 int Rowspan_Quntitativecapacity=0;
                                 string dtlengh = string.Empty;
                                 if (dtFilteredTable.Rows.Count > 0)
                                 {
                                     foreach (DataRow dr in dtFilteredTable.Rows)
                                     {
                                         if (dr["Specification"].ToString() == "0")
                                         {
                                             RowSpan_specification = RowSpan_specification+1;
                                         }
                                         if (dr["Qualitative Capacity"].ToString() == "0")
                                         {
                                             Rowspan_QualityCapacity = Rowspan_QualityCapacity + 1;
                                         }
                                         if (dr["Qualitative Capacity"].ToString() == "0")
                                         {
                                             Rowspan_Quntitativecapacity = Rowspan_Quntitativecapacity + 1;
                                         }

                                         //dr.Table.Rows.IndexOf(dr);
                                            
                                           
                                         //style='width:100%;white-space:pre;white-space:pre; width:25%;  width:25%;    style='width:120px; width:120px; width:120px;width:70px;
                                         tableOutput.Append("<tr class='RowClass'><td style='width:30px;font-family:Arial; font-size:12px;'><pre>"
                                                                   + dr["SNo"].ToString() +
                                                            "</pre></td><td  style=' font-size:12px; font-family:Arial;' ><pre>" + Server.HtmlDecode(dr["Store Name"].ToString()) +
                                                                "</pre></td><td style=  'font-size:12px; font-family:Arial;' rowspan='"+ RowSpan_specification +"'><pre>" + Server.HtmlDecode(dr["Specification"].ToString()) +
                                                                "</pre></td><td style=' font-size:12px;  font-family:Arial;'><pre>" + Server.HtmlDecode(dr["Qualitative Capacity"].ToString())
                                                                  + "</pre></td><td style=' font-size:12px; font-family:Arial;'><pre>" + Server.HtmlDecode(dr[4].ToString()) + "</pre></td></tr>");
                                         int length = tableOutput.Length;
                                                
                                         tableOutput.Append("<tr id='td_space_pixel' class='classtrspace'  style='visible:false'><td colspan='5' ><div id='divspace' style='visible:false' >&nbsp;</div></td></tr>");*/
                                //}




                               // gridviewNewStore.RowDataBound += new GridViewRowEventHandler(gridviewNewStore_RowDataBound);
                              //  gridviewNewStore.BorderWidth = Unit.Pixel(2);
                             //   gridviewNewStore.BorderColor = System.Drawing.Color.Black;
                              //  gridviewNewStore.DataSource = dtFilteredTable;
                              //  gridviewNewStore.DataBind();
                               // double height = gridviewNewStore.Height.Value;

                            }//count if

                            else
                            {
                                dtFilteredTable.Dispose();
                                dsAddress.Dispose();
                                gridviewNewStore.Dispose();
                                dsstore.Dispose();
                                
                            }
                            /* tableOutput.Append("</table>");
                             tempHtmlTable.Text = tableOutput.ToString();*/
                           // td_placeholder.Controls.Add(lblUnitAddress);
                           // td_placeholder.Controls.Add(gridviewNewStore);
                            // PlaceHolder1.Controls.Add(lblUnitAddress);
                            //PlaceHolder1.Controls.Add(tempHtmlTable);
                            //  PlaceHolder1.Controls.Add(gridviewNewStore);

                        }//dt.rows.count
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
            ErrHandler.WriteError("Server Busy!!!");
        }
    }

    public void BindTurnOver()
    {
        try
        {
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);// 3;
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
                            grdTurnOver.DataSource = dsTurnover.Tables[0];
                            grdTurnOver.DataBind();
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
            //lblregistration_number.Text = Registration_num.ToString();

            //lblstanameStore.Text = lblStateName.Text.ToString().Replace('-', ' ');
           // lblCompanyNameStore.Text = " M/s. " + lblNameCom.Text.ToString().Replace(" M/s. ", "") + ',';
          //  lbldatestore.Text = lbldateform.Text.ToString();
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = Registration_num.ToString(); ;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }

    // This Is unneccesary Code which are not used but not deleted it is depend on other function
    protected void grd_printRowDataBond(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                /*
                this.intCurrentRecordNumber++;
                if (this.intCurrentRecordNumber >= this.intRecordsPerPage)
                {

                    e.Row.Attributes.Add("style", @"page-break-after:always;");

                    this.intCurrentRecordNumber = 0;

                }*/
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
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
              //  lbl_serail_add_no.Text = SerialNoAdd.ToString();
            }
            else
            {
                ds_serial_no.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
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
            ErrHandler.WriteError(ex.Message);
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
                    Server.HtmlDecode(dtFilteredTable.Rows[0]["Store Name"].ToString());
                    Server.HtmlDecode(dtFilteredTable.Rows[0]["Specification"].ToString());
                    Server.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());

                    //  HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Qualitative Capacity"].ToString());
                    HttpUtility.HtmlDecode(dtFilteredTable.Rows[0]["Quantitative Capacity"].ToString());
                    string PMPSSTORE = "";
                    if (GvstorePMPS == "PMPS")
                    {
                        PMPSSTORE = "P.M.P.S.";
                    }
                    else if(GvstorePMPS == "PM")
                    {
                        PMPSSTORE = "P.M.";
                    }
                    else if (GvstorePMPS == "PA")
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

    //int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    //protected void gridviewNewStore_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        GridView gridvwStoreRBound = sender as GridView;
    //        int cellcount = 0;

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //===========CODE FOR MERGING START=========================
    //            DataRowView drv = (DataRowView)e.Row.DataItem;


    //            if (drv["Store name"].ToString().Contains("\n"))
    //            {
    //                e.Row.Cells[1].Text = drv["Store name"].ToString().Replace("\n", "<br/>");
    //            }

    //            if (drv["Specification"].ToString().Contains("\n"))
    //            {
    //                e.Row.Cells[2].Text = drv["Specification"].ToString().Replace("\n", "<br/>");
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
    //                if (drv["Specification"].ToString() == "0")
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


    //        //   for  breaking gridview gridview

    //       // gridvwStoreRBound.UseAccessibleHeader = true;
    //       // gridvwStoreRBound.HeaderRow.TableSection = TableRowSection.TableHeader;
    //      //  gridvwStoreRBound.FooterRow.TableSection = TableRowSection.TableFooter;
         
    //        //foreach (GridViewRow row in gridvwStoreRBound.Rows)
    //        //{
    //        //    if (row.RowIndex % 20 == 0 && row.RowIndex != 0)
    //        //    {
    //        //      //  row.Attributes["style"] = "margin-top:7cm; page-break-after:always;  ";
    //        //        row.Attributes.Add("class", "test");

    //        //    }
    //        //}

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //DataRowView drv = (DataRowView)e.Row.DataItem;
    //            //  int row=
    //            //if (e.Row.RowIndex != e.Row.Cells.Count)
    //            //{

    //       //     //}
    //       //string k=  Convert.ToString( gridvwStoreRBound.Height.Value);
    //       //  if (k == "400")
    //       //  {
    //       //      Label lbl_name = new Label();
    //       //      lbl_name.Text = "pagebreak";
    //       //      gridvwStoreRBound.Controls.Add(lbl_name);
    //       //  }
                
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ErrHandler.WriteError("" + ex.Message.ToString());
    //    }
           
    //}
   
    //protected void btnPrnt_Click(object sender, EventArgs e)
    //{



    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.opener.location.reload(true); window.close();</script>");
    //}

  
}
