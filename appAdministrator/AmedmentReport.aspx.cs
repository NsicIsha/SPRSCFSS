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

public partial class appAdministrator_AmedmentReport : System.Web.UI.Page
{

    clsCompany objCompany;
    clsAmendment objAmedment;
    string[] Name;
    string[] SI;
    string NameOfPartner;
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 20;
    int UserID;
    string SerialNumber;
    string RegistrationNumber = string.Empty;
    private string serial_NUMBER
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UserID = Convert.ToInt32(Server.HtmlEncode(Request.Cookies["OSUYULUKR8"]["USERID"]).ToString());
        objCompany = new clsCompany();
        Hashtable hashtbl = new Hashtable();

        hashtbl = objCompany.Company_Fetch_RegNo_ByCompanyId(Convert.ToInt32(Request.QueryString["id"]));
        RegistrationNumber = hashtbl["regno"].ToString();
        if (!IsPostBack)
        {
            BindAddress();
            Show_Certificatcate_Registration();
            BindGrid_Store();
            BindTurnOver();
             
        }
    }

    public void BindAddress()
    {


    
        objAmedment = new clsAmendment();
        DataSet dsAddress = new DataSet();
        int CompanyID = Convert.ToInt32(Request.QueryString["id"]);//102;//Convert.ToInt32(Request.QueryString["id"]);

        dsAddress = objAmedment.Company_AmmedMent_CertificateForm_Dataset_AddressFactory(CompanyID);
        if (dsAddress.Tables[0].Rows.Count > 0)
        {
            Rpt_Address.DataSource = dsAddress.Tables[0];
            Rpt_Address.DataBind();



            for (int j = 0; j < dsAddress.Tables[0].Rows.Count; j++)
            {
                string SNO = dsAddress.Tables[0].Rows[j]["SNo"].ToString();
                string FactoryAddress = dsAddress.Tables[0].Rows[j]["AddressName"].ToString();
                string StateName = dsAddress.Tables[0].Rows[j]["fGPtbl_MS_Name"].ToString();
                string Zip = dsAddress.Tables[0].Rows[j]["fGPtbl_CUnit_1B_Factory_Zip"].ToString();
                int UnitID = Convert.ToInt32(dsAddress.Tables[0].Rows[j]["UnitId"].ToString());
                Label lblUnitAddress = new Label();
                lblUnitAddress.CssClass = "LabelPrintCertificate";


                lblUnitAddress.Text = "<br/>" + "<b>UNIT-</b>" + SNO + ')' + FactoryAddress.Replace('.', ' ') + ',' + ' ' + StateName + Zip + "<br/>";


                DataSet dsstore = new DataSet();
                DataTable dt = new DataTable();
              //  dsstore = objCompany.Fetch_company_Store_Certificate_Modified(UnitID);
                dsstore = objAmedment.Fetch_Amedment_StoreReport_Modified(UnitID);
                if (dsstore.Tables[0].Rows.Count > 0)
                {
                    DataSet dscount = new DataSet();
                    dscount = objAmedment.Fetch_Amedment_StoreReport_Modified_Count(Convert.ToInt32(Request.QueryString["id"]));///Convert.ToInt32(Request.QueryString["id"]);
                                                                                         ///  
                    if (dscount.Tables[0].Rows.Count > 0)
                    {
                        lblGridCount.Text = "(" + dscount.Tables[0].Rows.Count + "&nbsp; " + "item only" + " )";
                    }
                    else
                    {
                        lblGridCount.Text = "(" + "Zero" + "&nbsp; " + "item only" + " )";
                        dscount.Tables[0].Dispose();
                    }
                }
               
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
                        GridView gvstore_details = new GridView();
                        gvstore_details.DataSource = dtFilteredTable;

                        gvstore_details.DataBind();


                        gvstore_details.CellSpacing = 1;
                        gvstore_details.CellPadding = 5;
                        gvstore_details.BorderWidth = 1;
                        gvstore_details.BorderStyle = BorderStyle.Dashed;

                        gvstore_details.HeaderRow.CssClass = "dynRowHeadercertificate";
                        gvstore_details.RowStyle.CssClass = "dynRowEven1";
                        gvstore_details.AlternatingRowStyle.CssClass = "dynRowOdd1";

                        gvstore_details.AutoGenerateColumns = false;
                        gvstore_details.Width = Unit.Percentage(100);
                        PlaceHolder1.Controls.Add(lblUnitAddress);
                        PlaceHolder1.Controls.Add(gvstore_details);





                    }
                }
            }
        }



        else
        {
            Rpt_Address.Dispose();
            Rpt_Address.DataBind();
        }
  
    }
        



    protected void btnPrnt_Click(object sender, EventArgs e)
    {

        ClientScript.RegisterStartupScript(this.GetType(), "PrintOperation", "printing()", true);

    }

    public void Show_Certificatcate_Registration()
     {

        string CompanyName = string.Empty;
        string PartenerName = string.Empty;
        int CompanyID = Convert.ToInt32(Request.QueryString["id"]); //102;//Convert.ToInt32(Request.QueryString["id"]);
        DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
        System.Data.SqlClient.SqlDataReader MyDr;
       // objCompany = new clsCompany();
        objAmedment = new clsAmendment();
        DataSet ds = new DataSet();
        string RefNo = RegistrationNumber;  //  this is Registration Number 
        ds = objAmedment.Company_AmedmentReport_Dataset(CompanyID);
      
        DataSet dspartner = new DataSet();
        //dspartner = objCompany.Company_Report_Registration_CertificateForm_PartnerName(RefNo);
        dspartner = objAmedment.Company_AmedmentReport_PartnerName(CompanyID);
        DataTable dt = new DataTable();
        dt = dspartner.Tables[0];

        var qu = from n in dt.AsEnumerable()
                 select new
                 {
                     partnerName = n.Field<string>("DirectorName") + ',',

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
            lblNameCom.Text = "M/s." + ds.Tables[0].Rows[0]["CompanyName"].ToString();
            LblCompanyAdress.Text = ds.Tables[0].Rows[0]["CompanyAddress"].ToString();
            lblStateName.Text = ds.Tables[0].Rows[0]["StateName"].ToString() + "-";
            lblcompanypin.Text = ds.Tables[0].Rows[0]["pinCode"].ToString();

            string PartnerName = hdnlablName.Value;
            CultureInfo ci = new CultureInfo("en-GB");

            DateTime dat1 = Convert.ToDateTime(lbldateform.Text, ci);




            lblvalidToFrom.Text = "(" + "Valid From " + lbldateform.Text + " to" + " " + dat1.AddDays(729).ToString("dd/MM/yyy") + ")";
            string sub = PartnerName.Substring(0, PartnerName.Length - 1);
            Name = sub.Split(',');

            int i = 0;
            while (i <= Convert.ToInt32(Name.Length.ToString()) - 1)
            {

                lblpartnaerName.Text = lblpartnaerName.Text + Convert.ToString(i + 1) + '.' + Name[i] + "</br>";

                i++;
            }
            objAmedment = new clsAmendment();
            DataSet ds_monotory_limit = new DataSet();
            int CompanyIDMonitory = Convert.ToInt32(Request.QueryString["id"]);//102;//Convert.ToInt32(Request.QueryString["id"]);
            ds_monotory_limit = objAmedment.Company_AmedmentReport_MonoteryLimit(CompanyIDMonitory);
           
            if (ds_monotory_limit.Tables[0].Rows.Count > 0)
            {

                lblMonLimit.Text = "." + ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString() + "  " + "(";

                string NumberText = ds_monotory_limit.Tables[0].Rows[0]["Monetary Limit"].ToString();
                NumberToEnglish numToeng = new NumberToEnglish();
                string rupeestext = numToeng.changeCurrencyToWords(NumberText);
                lblTextRupee.Text = "." + rupeestext + ")";
            }
            else
            {
                ds_monotory_limit.Dispose();
            }

            lblRegisNo.Text = ds.Tables[0].Rows[0]["Registration Number"].ToString();
            if (lblRegisNo.Text == "")
            {
            }
            else
            {

                SI = lblRegisNo.Text.ToString().Split('/');
                SerialNumber = SI[3] + "/" + SI[4];
                lblserialNo.Text = SerialNumber.ToString();
                lblserial_Store.Text = SerialNumber.ToString();
                serial_NUMBER = SerialNumber;
            }
            lblconstitutionCode.Text = ds.Tables[0].Rows[0]["ConstitutionName"].ToString().Replace("<br>", "");

           /// BindGridCertificate();

        }
        else
        {

            ds.Dispose();
            dspartner.Dispose();

        }

    }
    public void BindGrid_Store()
    {

        string Registration_num = lblRegisNo.Text;
        lblregistration_number.Text = Registration_num.ToString();

        lblstanameStore.Text = lblStateName.Text.ToString().Replace('-', ' ');
        lblCompanyNameStore.Text = "MS." + lblNameCom.Text + ',';
        lbldatestore.Text = lbldateform.Text.ToString();
        int CompanyID = Convert.ToInt32(Request.QueryString["id"]);
        objCompany = new clsCompany();
        DataSet dsCertificate_store = new DataSet();
        string Registratyion_Number = lblregistration_number.Text.ToString();

    }


    // Bind TurnOver

    public void BindTurnOver()
     {
        int CompanyID = Convert.ToInt32(Request.QueryString["id"]); //102;//Convert.ToInt32(Request.QueryString["id"]);// 3;
       
         objAmedment=new clsAmendment();
        DataSet dsTurnover = new DataSet();
      
         dsTurnover = objAmedment.Company_ReportView_TUrnOver_Amdment_Report(CompanyID);
        var Rowcount = dsTurnover.Tables.Count;
        if (dsTurnover.Tables.Count != 0)
        {

            if (dsTurnover.Tables[0].Rows.Count > 0)
            {
                if (dsTurnover.Tables[0].Rows.Count > 1)
                {
                    grdTurnOver.DataSource = dsTurnover.Tables[0];
                    grdTurnOver.DataBind();
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

    protected void grd_TurnOver_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        int GrdTotalCount = 0;
        GrdTotalCount = grdTurnOver.Columns.Count;
        string decodedText = HttpUtility.HtmlDecode(e.Row.Cells[0].Text);
        e.Row.Cells[0].Text = decodedText;
        e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Width, "250px");
    }
    protected void Button_Click(object sender, EventArgs e)
    {
        try
        {
            objCompany = new clsCompany();
            int Output;
            Output = objCompany.Company_Certificate_Insert_Amedment(3, Convert.ToInt32(Request.QueryString["id"]), lblserialNo.Text, UserID,1);

            Button.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>window.opener.location.reload(true); window.close();</script>");

        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
}
