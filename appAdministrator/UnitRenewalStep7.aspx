<%@ Page Language="C#" MasterPageFile="~/appMaster/AdminUpload.master" AutoEventWireup="true" CodeFile="UnitRenewalStep7.aspx.cs" Inherits="appAdministrator_UnitRenewalStep7" Title="Renewal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <style type="text/css">
        .style1
        {
            background-color: #DDDDDD;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 15px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
        }
        
        .ErrMsgLbl
        {
        	font-weight:bold;
        	font-style:italic;
        	text-decoration:underline;
        	color:Red;
        	}
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Renewal - Step 7
            </td>
        </tr>
       <tr style="width:95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                   <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                   <tr style="width:100%;">
                   <td style="width:100%;">
                     <table cellpadding="0" cellspacing="0" border="1" style="width:100%;">
                       <tr>
                       <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                        <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                         <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
                          <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-4</b></td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-5</b></td>
                            <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-6</b></td>
                            <td style="width:10%;padding-left: 10px; padding-right: 10px;"><b>Step-7</b></td>
                       </tr>
                     
                     </table>
                   
                   </td>
                   </tr>
                         
                   
                     <tr style="width:100%;">
                    
                     
                       
                         <td id="step2" style="width:100%;">
                      
                         <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                         <tr>
                         <td id="progressbar" align="left" ></td>
                         </tr>
                         
                         
                         
                         </table>
                       
                       
                     </td>
                     </tr>
                       
                   
                   </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="rowStep">
                            STEP 7 of 7
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="padding-top :10px;">
                        <td colspan="3" align="left" valign="top" class="style1">
                            Payment Detail
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Registration Fees Slab:
                        </td>
                        <td align="left" valign="top">
                           <%--<asp:Label ID="lblUnitSlabName" runat="server" ></asp:Label>--%>
                        </td>
                    </tr>
                    
                   <%-- <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Inspection Fees Slab:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblAgencySlabName" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                 
                     <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Registration Fees:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblUnitAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                   <%--  <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Inspection Fees:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblAgencyAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Sub Total:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblSubTot" runat="server" ></asp:Label>
                        </td>
                    </tr><tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Service Tax:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblServiceTax" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    
                     <tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Total amount payable:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblTotAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="style1">
                            Select Payment Option
                        </td>
                    </tr>
                    <tr align="right" valign="top">
                        <td colspan="3" align ="center"  >
                            <asp:RadioButtonList ID="radPay" runat ="server" RepeatDirection="Horizontal"  
                                Width="70%" onselectedindexchanged="radPay_SelectedIndexChanged" 
                                AutoPostBack="True" >
                            <%--<asp:ListItem Text="Cash" Value=1></asp:ListItem>--%>
                            <asp:ListItem Text="DD"  Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>

                                 <asp:ListItem Text="Fund Transfer" Value="4"></asp:ListItem>
                            
                            </asp:RadioButtonList>


                             <asp:RequiredFieldValidator ID="RequiredFieldValidatorfdf5" runat="server"
                                                ErrorMessage="Please Select Payment Type Option"
                                                ControlToValidate="radPay"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>


                        </td>
                    </tr>

                   




                    
                </table>
             </td> 
         </tr> 
          <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>                   
                   
              
        <tr>
        <td  valign="top" style="padding-left:10px; padding-right:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td align="left" >
        <asp:Panel ID="pnlCash" runat="server" Visible="false"  >
        <table width="80%">
        <tr>
        <td class="rowStep" colspan="2" >
        <b>Cash Payment Details</b>
        </td>
        </tr>
        <tr>
        <td>
        <b>Amount</b>
        </td>
        <td align="left" >
        <asp:Label ID="lblCashAmnt" runat="server"  ></asp:Label>
        </td>
        </tr>
         <tr id="receipt" runat="server" >
        <td>
        <b>Receipt No.</b>
        </td>
        <td align="left" >
        <%--<asp:Label ID="Label2" runat="server"  ></asp:Label>--%>
        <asp:TextBox ID="txtReceipt" runat="server" ></asp:TextBox>
        </td>
        </tr>
        <tr>
        <td align="right" >
        <asp:Button ID="btnCash" runat="server" Text="Submit" CssClass="btnCls" 
                onclick="btnCash_Click" />
        </td>
        </tr>
        </table>
        </asp:Panel>
        </td>
        </tr>
        
        </table> 
        </td>
        </tr>
          <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>        
        <tr>
        <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
        <td >
        <asp:Panel ID="pnlChqDD" runat="server" Visible="false"   >
        <table width="80%" >
        <tr>
        <td class="rowStep" colspan="2">
        <b>CHEQUE / DD PAYMENT DETAILS </b>
        </td>
        </tr>
        <tr>
        <td >
        <b>Cheque / DD No.</b>
        </td>
        <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ErrorMessage="Please Enter Cheque or DD Number"
                                                ControlToValidate="txt_chqddNo"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                           <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator1"
                                            ></asp:ValidatorCalloutExtender>--%>
                                            
                           
                        </td>
        </tr>
        <tr>
        <td >
        <b>Bank Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ErrorMessage="Please Enter Bank Name"
                                                ControlToValidate="txt_BankName"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                          <%--  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator2"
                                            ></asp:ValidatorCalloutExtender>--%>
                           
                        </td>
        </tr>
       
       <tr>
        <td >
        <b>Bank Branch</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                ErrorMessage="Please Enter Bank Branch"
                                                ControlToValidate="txt_BankBranch"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator4"
                                            ></asp:ValidatorCalloutExtender>--%>
                           
                        </td>
        </tr>
	  
        <tr>
        <td >
        <b>Payable At</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_PayableAt" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                ErrorMessage="Please Enter Payable At"
                                                ControlToValidate="txt_PayableAt"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                          <%--  <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator2"
                                            ></asp:ValidatorCalloutExtender>--%>
                           
                        </td>
        </tr>
        
         <tr>
        <td >
        <b>Cheque / DD Amount</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="true" ></asp:TextBox>
                           
                        </td>
        </tr>
        
        <tr>
        <td >
        <b>Cheque / DD Date</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_chqddate" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" 
                              ></asp:TextBox>
                              
                           
                                    
                            <asp:RequiredFieldValidator ID="RFValidatortxt_chqddate" runat="server" Display="None"
                                ErrorMessage="Please Enter Cheque/DD Date"
                                ControlToValidate="txt_chqddate"
                                SetFocusOnError="true"
                                ValidationGroup="1"
                            ></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="VCExtendertxt_chqddate" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="RFValidatortxt_chqddate"></asp:ValidatorCalloutExtender>
                            <asp:CalendarExtender ID="CExtendertxt_chqddate" runat="server"
                                TargetControlID="txt_chqddate" >
                            </asp:CalendarExtender>
                            
                            <asp:CompareValidator ID="CValidatortxt_chqddate" runat="server" Display="Dynamic" 
                                SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Please enter date in MM/DD/YYYY format"
                                ControlToValidate="txt_chqddate"
                                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                
                                
                        </td>
        </tr>
        
        <tr>
        <td >
            <b>Proposal date</b>
        </td>
        <td>
                <asp:TextBox ID="txtpropdate" runat="server" Enabled="false" CssClass="txt_bx"  ></asp:TextBox>
                <asp:CompareValidator ID="CVpropcompare" runat="server" Display="Dynamic" SetFocusOnError="true" ValidationGroup="1"
                                ErrorMessage="Cheque/DD date must be less than date of proposal" ControlToCompare="txt_chqddate"
                                ControlToValidate="txtpropdate" Operator="GreaterThanEqual" Type="Date" ></asp:CompareValidator>
        </td>
        </tr>
        
        <tr>
        <td >
        <b>Upload Scanned Copy of Cheque / DD </b>
        </td>
        <td align="left" >        
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl"></asp:Label>
                        
        </td>
        </tr>


            <tr>
        <td >
        <b>Payment Remarks </b>
        </td>
        <td align="left" >        
           <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                ValidationGroup="1" MaxLength="500" ReadOnly="false" ></asp:TextBox>

            <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  SetFocusOnError="true"
                                                ValidationGroup="1" runat="server" ErrorMessage="Please enter remarks" ControlToValidate="txtChequeDDPaymentRemarks"
                Display="Dynamic" ></asp:RequiredFieldValidator>


            
                        
        </td>
        </tr>
       
        </table>
        </asp:Panel>
        </td>
        </tr>
        
        </table> 
        </td>
        </tr>
        

         <tr>
                      <td align="left" valign="top" style="padding-left:10px; padding-right:10px;  margin-top:0px;"  colspan="3">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                          <tr>
                             <td valign="top">
                               <asp:Panel ID="pnlFundTransfer" runat="server" Visible="false">
                                 <table width="80%">
                                    <tr>
                                      <td align="left" valign="top">
                                        <asp:RadioButtonList ID="rdl_fund_transfer" runat="server" 
                                              RepeatDirection="Horizontal" AutoPostBack="false" 
                                              >
                                          <asp:ListItem Value="1" Selected="True">RTGS</asp:ListItem>
                                           <asp:ListItem Value="2">NEFT</asp:ListItem>
                                        </asp:RadioButtonList>
                                        
                                        <asp:RequiredFieldValidator   
            ID="Req_rdl_fund_transfer"  
            runat="server"  
            ControlToValidate="rdl_fund_transfer"  
            ErrorMessage="Select Payment Option." 
             ValidationGroup="1" 
               SetFocusOnError="true"
            >  
        </asp:RequiredFieldValidator> 
             <%-- <asp:ValidatorCalloutExtender ID="Vali_Req_rdl_fund_transfer" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="Req_rdl_fund_transfer"
                                            ></asp:ValidatorCalloutExtender> --%>
                                      </td>
                                    </tr>
                                    
                                    <%--<tr>
                                      <td style="height:5px;">&nbsp;</td>
                                    </tr>--%>
                                    <tr>
                                      
                                      
                                      <td align="left" valign="top">
                                        <asp:Panel ID="pnlOrtgs" runat="server">
                                        
          
                                          <table width="80%">
                                       <tr>
                                        <td class="rowStep" colspan="2">
                                                  <b>Fund Transfer Details </b> </td>
                                                           
                                        </tr>
                                            <tr>
                                            <td valign="top">
                                              <b>UTR NO</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_utr_no" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="req_utrno" runat="server"
                                                ErrorMessage="Please Enter Utr Number"
                                                ControlToValidate="txt_utr_no"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                         <%--<asp:ValidatorCalloutExtender ID="Vali_req_utrno" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="req_utrno"
                                            ></asp:ValidatorCalloutExtender>--%> 
                                
                           
                        </td>
                                            </tr>
                                                  <tr>
                                            <td valign="top">
                                              <b>Bank Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_bank_name" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="Req_bank_name" runat="server"
                                                ErrorMessage="Please Enter Bank Name"
                                                ControlToValidate="txt_bank_name"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            <%-- <asp:ValidatorCalloutExtender ID="Vali_Req_bank_name" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="Req_bank_name"
                                            ></asp:ValidatorCalloutExtender>--%>
                        </td>
                                            </tr>
                                            
                                                    <tr>
                                            <td valign="top">
                                              <b>Branch Name</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_branch_name" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="Req_branc_name" runat="server"
                                                ErrorMessage="Please Enter Branch Name"
                                                ControlToValidate="txt_branch_name"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            
                                           <%--  <asp:ValidatorCalloutExtender ID="Vali_Req_branc_name" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="Req_branc_name"
                                            ></asp:ValidatorCalloutExtender>
                           --%>
                        </td>
                                            </tr>
                                                   <tr>
                                            <td valign="top">
                                              <b>Date (as MM/DD/YYYY)</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_date_utr" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="Req_date_url" runat="server"
                                                ErrorMessage="Please Enter Date"
                                                ControlToValidate="txt_date_utr"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                            
                                 <asp:CompareValidator ID="CValidatortxt_cheq3" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                    Display="Dynamic" ControlToValidate="txt_date_utr" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="1">
                                   </asp:CompareValidator>
                                
                           
                           
                        </td>
                                            </tr>
                                            
                                                      <tr>
                                            <td>
                                              <b>Amount</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_amount" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ReadOnly="true" ></asp:TextBox>
                           
                        </td>
                                            </tr>


                                               <tr>
        <td >
        <b>Payment Remarks </b>
        </td>
        <td align="left" >        
           <asp:TextBox ID="txtNeftPaymentRemarks" runat="server" CssClass="txt_bx"  TextMode="MultiLine"
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" ></asp:TextBox>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                ErrorMessage="Please Enter Remarks"
                                                ControlToValidate="txtNeftPaymentRemarks"
                                                SetFocusOnError="true" Display="Dynamic"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                        
        </td>
        </tr>
                                            
                                        </table>
                                        </asp:Panel>
                                      
                                      </td>
                                      
                                      
                                      
                                      
                                    
                                    </tr>
                                 
                                 
                                 </table>
                               
                               </asp:Panel>
                             
                             </td>
                          </tr>
                        
                        </table>
                      
                      </td>
                    </tr>  
    
    
     <tr>
        <td align="center"  >
        <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnCls" 
                ValidationGroup="1" onclick="Button1_Click" />
        </td>
        </tr>    
        
    </table>
</asp:Content>


