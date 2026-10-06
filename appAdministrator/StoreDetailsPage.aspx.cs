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

public partial class appAdministrator_StoreDetailsPage : System.Web.UI.Page
{
    clsCompany objCompany;

    string SerialNumber;
    string RegistrationNumber = string.Empty;
    public string SerialNoAdd;
    int UserID, BranchId;
    string Branch_Code = string.Empty;
    string lblRegisNo = string.Empty;
    string old_Registration_number = string.Empty;
    string Registration_date = string.Empty;

    string StateNameStore = string.Empty;
    string companyNamestore = string.Empty;
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
            lbl_Branch_Name.Text = Convert.ToString(objtub.Trans_UserBranch_FetchZoneId_DataSet(UserID).Tables[0].Rows[0]["BranchName"]);
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


                Bind_store_deatils();
                    // BindTurnOver();
                  //  BindGrid_Store();

                BindSerialNumber();
                Bind_Branch_office_details();
                Show_Certificatcate_Registration();
                BindGrid_Store();
               
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }
    }
       // Bind_store_deatils();


    public void Bind_store_deatils()
    {
        try
        {
            objCompany = new clsCompany();
            DataSet dsAddress = new DataSet();
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);

            dsAddress = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset_AddressFactory(CompanyID);
            if (dsAddress.Tables[0].Rows.Count > 0)
            {//if ds address
                if (dsAddress.Tables[0].Rows.Count > 5)
                {
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Msg", "<script type=\"text/javascript\"  language=\"javascript\">function showMsg(){return confirm(\"They have Contain More Than Five Address?\");}</script>", true);
                }
                else
                {//else


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

                        //CODE TO ADD A GRIDVIEW

                        // GridView gvstore_details = new GridView();
                        Label tempHtmlTable = new Label();

                        dsstore = objCompany.Fetch_company_Store_Certificate_Modified(UnitID);
                        dt = dsstore.Tables[0];

                        if (dt.Rows.Count > 0)
                        {//dt.rows.count
                            //
                            gridviewNewStore = new GridView();
                            gridviewNewStore.ID = "GridView1";
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
                                if (GvstorePMPS == "PMPS")
                                {
                                    PMPSSTORE = "P.M.P.S.";
                                }
                                else if (PMPSSTORE == "PM")
                                {
                                    PMPSSTORE = "P.M.";
                                }
                                else if (PMPSSTORE == "PA")
                                {
                                    PMPSSTORE = "P.A.";
                                }
                                dtFilteredTable.Columns[4].ColumnName = (dtFilteredTable.Columns[4].ColumnName + ' ' + PMPSSTORE.ToString());
                                string Quantitative_Capacity = dtFilteredTable.Columns[4].ColumnName;

                                dtFilteredTable.Columns.Remove("PMPS");





                                gridviewNewStore.RowDataBound += new GridViewRowEventHandler(gridviewNewStore_RowDataBound);
                                gridviewNewStore.BorderWidth = Unit.Pixel(2);
                              
                              

                                gridviewNewStore.BorderColor = System.Drawing.Color.Black;
                                gridviewNewStore.DataSource = dtFilteredTable;
                                gridviewNewStore.DataBind();
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
                            plc_test.Controls.Add(lblUnitAddress);
                            plc_test.Controls.Add(gridviewNewStore);
                            // PlaceHolder1.Controls.Add(lblUnitAddress);
                            //PlaceHolder1.Controls.Add(tempHtmlTable);
                            //  PlaceHolder1.Controls.Add(gridviewNewStore);

                        }//dt.rows.count
                    }//for
                }//else


            }//if ds address
            //}

            else
            {

                dsAddress.Dispose();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!");
        }
    }

    int firstrowspecification = 0, firstrowquality = 0, firstrowquantity = 0;
    protected void gridviewNewStore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            GridView gridvwStoreRBound = sender as GridView;
            int cellcount = 0;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //===========CODE FOR MERGING START=========================
                DataRowView drv = (DataRowView)e.Row.DataItem;


                if (drv["Store name"].ToString().Contains("\n"))
                {
                    e.Row.Cells[1].Text = drv["Store name"].ToString().Replace("\n", "<br/>");
                }

                if (drv["Specification"].ToString().Contains("\n"))
                {
                    e.Row.Cells[2].Text = drv["Specification"].ToString().Replace("\n", "<br/>");
                }

                if (drv["Qualitative Capacity"].ToString().Contains("\n"))
                {
                    e.Row.Cells[3].Text = drv["Qualitative Capacity"].ToString().Replace("\n", "<br/>");
                }

                if (drv[drv.DataView.Table.Columns[4].ToString()].ToString().Contains("\n"))
                {
                    // drv[drv.DataView.Table.Columns[4].ToString()].ToString().Contains("\n") 
                    //drv["Quantitative Capacity P.M.P.S."]
                    // drvd["Quantitative Capacity P.M.P.S."].ToString()
                    e.Row.Cells[4].Text = drv[drv.DataView.Table.Columns[4].ToString()].ToString().Replace("\n", "<br/>");
                }

                if (e.Row.RowIndex > 0)
                {
                    if (drv["Specification"].ToString() == "0")
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
                        e.Row.VerticalAlign = VerticalAlign.Top;
                   
                    }

                    else //IT'S NEW VALUE
                    {
                        //Set the vertical alignment to top
                        e.Row.VerticalAlign = VerticalAlign.Top;
                        firstrowspecification = e.Row.RowIndex;
                    }

                    if (drv["Qualitative Capacity"].ToString() == "0")
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
                        e.Row.VerticalAlign = VerticalAlign.Top;
                    }

                    else //IT'S NEW VALUE
                    {
                        //Set the vertical alignment to top
                        e.Row.VerticalAlign = VerticalAlign.Top;
                        firstrowquality = e.Row.RowIndex;
                    }

                    if (drv[drv.DataView.Table.Columns[4].ToString()].ToString() == "0")
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
                        e.Row.VerticalAlign = VerticalAlign.Top;

                    }

                    else //IT'S A NEW VALUE
                    {
                        //Set the vertical alignment to top
                        e.Row.VerticalAlign = VerticalAlign.Top;
                        firstrowquantity = e.Row.RowIndex;
                    }
                }

                //===========CODE FOR MERGING END
                MakeGridViewPrinterFriendly(gridvwStoreRBound);
            }


           
         
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("" + ex.Message.ToString());
        }

    }
    public void BindGrid_Store()
    {
        try
        {
            string Registration_num = RegistrationNumber;
            lblregistration_number.Text =  lblRegisNo.ToString();

            lblstanameStore.Text =  StateNameStore.ToString().Replace('-', ' ');
            lblCompanyNameStore.Text = " M/s. " + companyNamestore.ToString().Replace(" M/s. ", "") + ',';
            lbldatestore.Text = Registration_date.ToString();
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
            objCompany = new clsCompany();
            DataSet dsCertificate_store = new DataSet();
            string Registratyion_Number = lblregistration_number.Text.ToString();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

    }


    //------------------------------------------------------------------------
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
          
         //   string RefNo = RegistrationNumber;  //  this is Registration Number 
            int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
            hdn_company_id.Value = Request.QueryString["id"].ToString();
            ds = objCompany.Company_WaitingCompany_Registration_CertificateForm_Dataset(CompanyID);
          

            if (ds.Tables[0].Rows.Count > 0)
            {
             
               // lbldateform.Text = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
               Registration_date = ds.Tables[0].Rows[0]["RegistrationDate"].ToString();
                // lblFactoryAddress.Text = ds.Tables[0].Rows[0]["FactoryAddress"].ToString();
                //lblNameCom.Text = " M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();
               companyNamestore = " M/s. " + ds.Tables[0].Rows[0]["CompanyName"].ToString();
                //LblCompanyAdress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
                //lbl_city_name.Text = ds.Tables[0].Rows[0]["CityName"].ToString() + ",";
                //lblStateName.Text = ds.Tables[0].Rows[0]["StateName"].ToString() + "-";
               StateNameStore = ds.Tables[0].Rows[0]["StateName"].ToString() + "-";
                //lblcompanypin.Text = ds.Tables[0].Rows[0]["pinCode"].ToString();
                string ProvisinalCertificate = ds.Tables[0].Rows[0]["ProvisnalCertificate"].ToString();

              
                CultureInfo ci = new CultureInfo("en-GB");

                DateTime dat1 = Convert.ToDateTime(Registration_date, ci);
                if (ProvisinalCertificate == "0")
                {
                    lbl_validform_store_details.Text = "<b>" + "(" + "Valid From " + Registration_date + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyy") + ")" + "</b>";
                   // BindTurnOver();
                  //  lbl_validform_store_details.Text = lblvalidToFrom.Text;
                }
                else
                {
                    lbl_validform_store_details.Text = "<b>" + "(" + "Valid From " + Registration_date + " to" + " " + dat1.AddYears(2).AddDays(-1).ToString("dd/MM/yyy") + ")" + "</b>";
                   
                }
              
              //lblRegisNo.Text = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
               lblRegisNo = ds.Tables[0].Rows[0]["RegistrationNumber"].ToString();
              //  lbl_old_registration_no.Text = ds.Tables[0].Rows[0]["old_RegistrationNumber"].ToString();

             //   string SI = lblRegisNo.ToString().Split('/');
                //  string sampleNum = "00000000"+ (incNumber++).ToString();
                SerialNumber = Branch_Code + "-" + SerialNoAdd; //"SPR-"+generateID("www.nsicspronline.com");//SI[3] + "/" + SI[4];
             
                lblserial_Store.Text = SerialNumber.ToString();
                serial_NUMBER = SerialNumber;
              
               // BindGridCertificate();

            }
            else
            {

                ds.Dispose();
              
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError("Server Busy!!!!");
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
                lbl_branchaddress.Text = ds_branchofficedetails.Tables[0].Rows[0]["BranchAddress"].ToString();
                lbl_phoneAddress.Text = ds_branchofficedetails.Tables[0].Rows[0]["Phone"].ToString();
                lbl_FaxAddress.Text = ds_branchofficedetails.Tables[0].Rows[0]["Fax"].ToString();
                lbl_EmailAddress.Text = ds_branchofficedetails.Tables[0].Rows[0]["Email"].ToString();


               

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
                lbl_serail_add_no.Text = SerialNoAdd.ToString();
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

    private void MakeGridViewPrinterFriendly(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            gridView.UseAccessibleHeader = true;
            gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
            gridView.HeaderRow.Style["display"] = "table-header-group";
        }
    }

}
