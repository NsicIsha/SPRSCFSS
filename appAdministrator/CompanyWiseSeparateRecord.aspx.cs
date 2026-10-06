using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Diagnostics;
using NSICGP_VBClassLibrary;
using System.Globalization;

public partial class appAdministrator_CompanyWiseSeparateRecord : System.Web.UI.Page
{
    clsCompany  objcompany;
    clsDDL objddl;
    DataSet Myds;
    int accessId;
    int count = 1;
    decimal registration_amount;
    decimal Amedment_amount;
    decimal Inspection_amount;
    decimal Renewal_amount;
    decimal Total_amount;
    int FormTypeId;
    int PaymentTypeId;
    int Feed_ID;
    decimal SubTotal_Amount;
    decimal Total_Amount;
    decimal UnitAmmount_Registration;
    decimal UnitServicetax_Registration;
    decimal InspectionAgencyFee_registration;
    decimal Inspection_servicetax_Registration;
    decimal total_registration;
    decimal total_inspection;
    StringBuilder sb = new StringBuilder();
    private int intCurrentRecordNumber = 0;
    private int intRecordsPerPage = 5;
    string Type_user = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblReportName.Text = "PAYMENT DETAILS of " +"     " + Request.QueryString["cname"].ToString().ToUpper().ToString();
           
            RuntimeGridviewgenarate();

        }
    }


    public void BindFormTypeAndPaymentTypeId()
    {
        try
        {
            objcompany = new clsCompany();
            DataSet ds_paymenttype = new DataSet();
             ds_paymenttype=objcompany.Fetch_Branch_Payment_Type_andFormTypeId(Convert.ToInt32(Request.QueryString["cid"].ToString()));
             if(ds_paymenttype.Tables[0].Rows.Count>0)
             {
                   for(int i=0; i<=ds_paymenttype.Tables[0].Rows.Count;i++)
                   {
                        FormTypeId= Convert.ToInt32(ds_paymenttype.Tables[0].Rows[i]["fGPtbl_PD_FormtypeId"]);
                        PaymentTypeId=Convert.ToInt32(ds_paymenttype.Tables[0].Rows[i]["fGPtbl_PD_Id"]);
                         Feed_ID=Convert.ToInt32(ds_paymenttype.Tables[0].Rows[i]["fGPtbl_PD_UnitFeeId"]);
                    

                   }
             }
             else
             {
               ds_paymenttype.Dispose();   
             }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }


    public void BindDaataInlabel()
    {
        try
        {
            objcompany = new clsCompany();
            DataSet ds_bindData = new DataSet();
           // ds_bindData = objcompany.Fetch_Branch_Payment_Type_andFormTypeIdList(FormTypeId, PaymentTypeId);
           ds_bindData = objcompany.Fetch_Branch_Payment_Type_andFormTypeIdList_Modified(Convert.ToInt32(Request.QueryString["cid"].ToString()));
           if (ds_bindData.Tables[0].Rows.Count > 0)
           {


           }
           else
           {
               ds_bindData.Dispose();

           }
               
            
            // ds_bindData = objcompany.Fetch_Branch_Payment_Type_andFormTypeIdList_Modified_List(Convert.ToInt32(Request.QueryString["cid"].ToString()));

           //if (ds_bindData.Tables[0].Rows.Count > 0)
           //     {

           //         sb.Append("<table border='0' cellpadding='3'cellspacing='3' style='background-color: #ddf1f3; margin-left:0px; width:700px; '  WIDTH='700'>");
                   

           //         if (ds_bindData.Tables[0].Rows.Count > 0)
           //         {
           //             for (int i = 0; i < ds_bindData.Tables[0].Rows.Count; i++)
           //             {
           //                 // this point you add inspection agency only feed id basis means pass the value of feed id

           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "1" )
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; border:1px solid black; width:700px; '  WIDTH='700'>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3'> <b>Registration Fees</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                    // sb.Append("<br/>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3' style='height:5px;'> ");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");                                        //decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero))
           //                     sb.Append("<td aliign='left' style='white-space:nowrap ; vertical-align:top;' ><span class='Headingclass'> Registration Fees: </span>" + "Rs." + DecimalTowardZero((Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString(), CultureInfo.InvariantCulture)), 2));
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
                                
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Payment Date: </span>" + ds_bindData.Tables[0].Rows[i]["PaymentDate"].ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     UnitAmmount_Registration = decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero);
           //                     UnitServicetax_Registration = decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero);
           //                     total_registration = decimal.Add(UnitAmmount_Registration, UnitServicetax_Registration);
           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Service Tax: </span> " + "Rs." + decimal.Round(Convert.ToDecimal(String.Format("{0:0.00}", ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString())), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>Inspection Fee: </span> " + "Rs." + decimal.Round(Convert.ToDecimal(String.Format("{0:0.00}", ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString())), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Total Amount: </span> " + "<b>" + "Rs." + total_registration + "</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Service Tax: </span> " + "Rs." + decimal.Round(Convert.ToDecimal(String.Format("{0:0.00}", ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString())), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Payment Mode: </span> " + "" + ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Total Amount: </span> " + "<b>" + "Rs." + ((decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString()), 2, MidpointRounding.AwayFromZero)) + (decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero))) + "</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td  colspan='3' align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; width:700px; '  WIDTH='700'>");
           //                     if (ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "DEMAND DRAFT" || ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "CHEQUE")
           //                     {
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap;vertical-align:top;'> <span class='Headingclass'> Bank Name: </span> " + ds_bindData.Tables[0].Rows[i]["BankName"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'>  <span class='Headingclass'> Branch: </span> " + ds_bindData.Tables[0].Rows[i]["BranchName"].ToString() + " </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Number:  </span> " + ds_bindData.Tables[0].Rows[i]["DDNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Date:  </span>" + ds_bindData.Tables[0].Rows[i]["ChequeddDate"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'> <span class='Headingclass'> Receipt Number: </span> " + ds_bindData.Tables[0].Rows[i]["CashReciptNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                     }
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='paid_amount' aliign='left' style='white-space:nowrap; vertical-align:top;'> <span class='Headingclass'> Paid Amount:  </span> " +"<b style='color:red'>" +"Rs." + ds_bindData.Tables[0].Rows[i]["PaidAmount"].ToString()  +"</b>" +"</td>");

           //                     sb.Append("</tr>");

           //                     sb.Append("</table>");
           //                     sb.Append("</td>");

           //                     sb.Append("</tr>");
           //                     sb.Append("</table>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                 }

           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "2")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; border:1px solid black; width:700px;'  WIDTH='700'>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3'> <b>Renewal Fees</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3' style='height:5px;'> ");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Renewal Fees: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Payment Date: </span>" + ds_bindData.Tables[0].Rows[i]["PaymentDate"].ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;' colspan='3'><span class='Headingclass'> Service Tax: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
                                
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;' colspan='3'><span class='Headingclass'> Total Amount: </span>" + "<b>" + "Rs." + (decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero)) + "</b>");
           //                     sb.Append("</td>");
                                
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td  aliign='left' style='white-space:nowrap; vertical-align:top;' colspan='3'><b > Payment Mode: </span>" + " " + ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString());
           //                     sb.Append("</td>");
                               
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td  colspan='3' align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; width:700px; '  WIDTH='700'>");
           //                     if (ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "DEMAND DRAFT" || ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "CHEQUE")
           //                     {
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap;  aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Bank Name: </span> " + ds_bindData.Tables[0].Rows[i]["BankName"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap ; vertical-align:top;'>  <span class='Headingclass'> Branch: </span> " + ds_bindData.Tables[0].Rows[i]["BranchName"].ToString() + " </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Number:  </b>" + ds_bindData.Tables[0].Rows[i]["DDNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Date:  </span>" + ds_bindData.Tables[0].Rows[i]["ChequeddDate"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'> <span class='Headingclass'> Receipt Number: </span> " + ds_bindData.Tables[0].Rows[i]["CashReciptNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                     }
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='paid_amount' aliign='left' style='white-space:nowrap; vertical-align:top;' > <span class='Headingclass'> Paid Amount:  </span>" +"<b style='color:red;'>"+"Rs."+ ds_bindData.Tables[0].Rows[i]["PaidAmount"].ToString() +"</b>"+ "  </td>");

           //                     sb.Append("</tr>");

           //                     sb.Append("</table>");
           //                     sb.Append("</td>");

           //                     sb.Append("</tr>");
           //                     sb.Append("</table>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");

           //                 }



           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "3")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; border:1px solid black; width:700px;'  WIDTH='700'>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3'> <b >Amendment Fees</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td colspan='3' style='height:5px;'> ");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Amendment Fees: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Payment Date: </span>" + ds_bindData.Tables[0].Rows[i]["PaymentDate"].ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Service Tax: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Inspection Fee: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Total Amount: </span>" + "<b>" + "Rs." + ((decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero)) + (decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString()), 2, MidpointRounding.AwayFromZero))) + "</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Service Tax: </span>" + "Rs." + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero).ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");

           //                     sb.Append("<tr>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Payment Mode: </span>" + "" + ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString());
           //                     sb.Append("</td>");
           //                     sb.Append("<td style='width:2px;'>");
           //                     sb.Append("</td>");
           //                     sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'> Total Amount: </span>" + "<b>" + "Rs." + (decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString()), 2, MidpointRounding.AwayFromZero) + decimal.Round(Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString()), 2, MidpointRounding.AwayFromZero)) + "</b>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td  colspan='3' align='left'>");
           //                     sb.Append("<table border='0' cellpadding='5'cellspacing='5' style='background-color: #FFFFFF; margin-left:0px; width:700px; '  WIDTH='700'>");
           //                     if (ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "DEMAND DRAFT" || ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "CHEQUE")
           //                     {
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'> <span class='Headingclass'> Bank Name: </span> " + ds_bindData.Tables[0].Rows[i]["BankName"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'>  <span class='Headingclass'> Branch: </span> " + ds_bindData.Tables[0].Rows[i]["BranchName"].ToString() + " </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Number:  </span>" + ds_bindData.Tables[0].Rows[i]["DDNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'><span class='Headingclass'>  Cheque/DD Date:  </span>" + ds_bindData.Tables[0].Rows[i]["ChequeddDate"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                         sb.Append("<tr>");
           //                         sb.Append("<td aliign='left' style='white-space:nowrap; vertical-align:top;'> <span class='Headingclass'> Receipt Number: </span> " + ds_bindData.Tables[0].Rows[i]["CashReciptNumber"].ToString() + "  </td>");
           //                         sb.Append("</tr>");
           //                     }
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='paid_amount' aliign='left' style='white-space:nowrap; vertical-align:top;' colspan='3'> <span class='Headingclass'> Paid Amount:  </span>" +"<b style='color:red;'>" +"Rs." + ds_bindData.Tables[0].Rows[i]["PaidAmount"].ToString() + "</b>"+"  </td>");

           //                     sb.Append("</tr>");

           //                     sb.Append("</table>");
           //                     sb.Append("</td>");

           //                     sb.Append("</tr>");
           //                     sb.Append("</table>");
           //                     sb.Append("</td>");
           //                     sb.Append("</tr>");
           //                 }
           //                 /*
           //                 sb.Append("<tr>");
           //                 sb.Append("<td > <span class='leftHeading'> Registration Amount: </span> " + ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString() + " </td>");
           //                 sb.Append("</tr>");
           //                 sb.Append("<tr>");
           //                 sb.Append("<td> <span class='leftHeading'>  Service Tax: </span> " + ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString() + " </td>");
           //                 sb.Append("</tr>");
           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "1")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='Registration_amount'> <span class='leftHeading'>Total Amount: </span> " + (Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()) + Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString())) + " </td>");
                                 
           //                     sb.Append("</tr>");
           //                 }
           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "2")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='renewal_amount'> <span class='leftHeading'>Renewal Amount:</span> " + (Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()) + Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString())) + " </td>");
           //                     sb.Append("</tr>");
           //                 }
           //                 if (ds_bindData.Tables[0].Rows[i]["FormtypeId"].ToString() == "3")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td id='Amedment_amount'><span class='leftHeading'> Amedment Amount: </span> " + (Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitServiceTax"].ToString()) + Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["UnitAmmount"].ToString())) + " </td>");
           //                     sb.Append("</tr>");
           //                 }
           //                 sb.Append("<tr>");
           //                 sb.Append("<td> <span class='leftHeading'>Inspection Amount: </span> " + ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString() + "  </td>");
           //                 sb.Append("</tr>");
           //                 sb.Append("<tr>");
           //                 sb.Append("<td> <span class='leftHeading'> Inspection Service Tax:</span> " + ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString() + "  </td>");
           //                 sb.Append("</tr>");
           //                 sb.Append("<td> <span class='leftHeading'> Total Inspection Amount:</span> " + (Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyServiceTax"].ToString()) + Convert.ToDecimal(ds_bindData.Tables[0].Rows[i]["AgencyAmount"].ToString())) + "  </td>");
                          
           //                 sb.Append("</tr>");
           //                 sb.Append("<tr>");
           //                 sb.Append("<td><span class='leftHeading'> Payment Date: </span> " + ds_bindData.Tables[0].Rows[i]["PaymentDate"].ToString() + " </td>");
           //                 sb.Append("</tr>");
           //                 sb.Append("<tr>");
           //                 sb.Append("<td> <span class='leftHeading'> Payment Type: </span>" + ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() + " </td>");
           //                 sb.Append("</tr>");
           //                 if (ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "DD" || ds_bindData.Tables[0].Rows[i]["fGPtbl_PD_PaymentType"].ToString() == "CHEQUE")
           //                 {
           //                     sb.Append("<tr>");
           //                     sb.Append("<td> <span class='leftHeading'> Bank Name: </span> " + ds_bindData.Tables[0].Rows[i]["BankName"].ToString() + "  </td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td>  <span class='leftHeading'> Branch Name: </span> " + ds_bindData.Tables[0].Rows[i]["BranchName"].ToString() + " </td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td><span class='leftHeading'>  DD Number:  </span>" + ds_bindData.Tables[0].Rows[i]["DDNumber"].ToString() + "  </td>");
           //                     sb.Append("</tr>");
           //                     sb.Append("<tr>");
           //                     sb.Append("<td> <span class='leftHeading'> Cash Recipt Number: </span> " + ds_bindData.Tables[0].Rows[i]["CashReciptNumber"].ToString() + "  </td>");
           //                     sb.Append("</tr>");
           //                 }
           //                 sb.Append("<tr>");
           //                 sb.Append("<td id='paid_amount'> <span class='leftHeading'> Paid Amount:  </span>" + ds_bindData.Tables[0].Rows[i]["PaidAmount"].ToString() + "  </td>");
                           
           //                 sb.Append("</tr>");

           //                 sb.Append("<tr>");
           //                 sb.Append("<td id='total_amount'> <span class='leftHeading'> Total Amount: </span>" +" " +"</td>");
           //                  sb.Append("</tr>");

           //                  sb.Append("<tr>");
           //                  sb.Append("<td id='Remaining_amount'> <span class='leftHeading'> Remaining  Amount: </span>" + " " + "</td>");*/
           //                 // sb.Append("</tr>"); 
           //                 sb.Append("<tr>");
           //                 sb.Append("<td  style='height:25px;'> ");
           //                 sb.Append("</td>");
           //                 sb.Append("</tr>");
           //                sb.Append("</table>");
           //               // sb.Append("<br/>");
                          
                  
                           
           //             }
           //              //Response.Write("<table>");
           //             lbl_summary.Text = sb.ToString();


           //         }
           //     }*/
           //     else
           //     {
           //         ds_bindData.Dispose();
           //     }
            }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

// Commented by Ravish Kuamr pandey 
   
    protected void lnkBack_Click(object sender, EventArgs e)
    {
        try
        {
            //Request.QueryString["Page1"].ToString() == "Comapnywise" ||
            if (Request.QueryString["Page"].ToString() == "Comapny")
            {
                //string page1 = Request.QueryString["Page1"].ToString();

                //if (page1 != "")
                //{
                    int ZoneID = Convert.ToInt32(Request.QueryString["ZoneID"]);
                    int BranchID = Convert.ToInt32(Request.QueryString["id"]);
                     Response.Redirect("CompanyWiseRecord.aspx?id=" + BranchID + "&ZoneID=" + ZoneID);
               // }
            }
            else if (Request.QueryString["Page1"].ToString() == "SeparateRecord")
            {
                Response.Redirect("SpecificListOfReportPage.aspx");
            }
            else if (Request.QueryString["Page1"].ToString() == "")
            {
                Response.Redirect("ListOfReport.aspx");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
          }
    }
    


    static decimal DecimalTowardZero(decimal value, int decimals)
    {
        // rounding away from zero
        var rounded = decimal.Round(value, decimals, MidpointRounding.AwayFromZero);

        // if the absolute rounded result is greater 
        // than the absolute source number we need to correct result
        if (Math.Abs(rounded) > Math.Abs(value))
        {
            return rounded - new decimal(1, 0, 0, value < 0, (byte)decimals);
        }
        else
        {
            return rounded;
        }
    }





public void RuntimeGridviewgenarate()
{
    try
    {

        int CompanyId_1 = Convert.ToInt32(Request.QueryString["cid"].ToString());
        clsCompany objcompany = new clsCompany();
        DataSet ds = new DataSet();
        ds = objcompany.Fetch_Branch_Payment_Type_CompanywiseSeparateRecord(CompanyId_1);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grd_total.DataSource = ds.Tables[0];
            grd_total.DataBind();
        }
        else
        {
            ds.Dispose();
            grd_total.Dispose();
        }
    }

    catch (Exception ex)
    {
    }
}
             

      
}
