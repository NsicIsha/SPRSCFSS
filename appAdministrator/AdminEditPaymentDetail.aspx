<%@ Page Language="C#" MasterPageFile="~/appMaster/AdminUpload.master" AutoEventWireup="true" CodeFile="AdminEditPaymentDetail.aspx.cs" Inherits="appAdministrator_AdminEditPaymentDetail" Title="Payment Detail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javscript" language="javascript">
    
      function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
     </script>

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
        
        .style2
        {
            height: 22px;
        }
     </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px; padding-top:15px;">
                            Balance fees to be paid:
                        </td>
                        <td align="left" valign="top" style="padding-top:15px;">
                           <%--<asp:Label ID="lblUnitAmnt" runat="server" ></asp:Label>--%>
                           <asp:Label ID="lblTotAmnt" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    
                    <%--<tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Service Tax:
                        </td>
                        <td align="left" valign="top">
                           <asp:Label ID="lblServiceTax" runat="server" ></asp:Label>
                        </td>
                    </tr>--%>
                    
                     <%--<tr align="left" valign="top" style="padding-top :10px;">
                     
                        <td align="left" valign="top" class="leftHeading" style="width:200px;">
                            Total amount payable:
                        </td>
                        <td align="left" valign="top">
                           
                        </td>
                    </tr>--%>
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
                        <td colspan="3" align ="center" class="style2"  >
                            <asp:RadioButtonList ID="radPay" runat ="server" RepeatDirection="Horizontal"  
                                Width="70%" onselectedindexchanged="radPay_SelectedIndexChanged" 
                                AutoPostBack="True" >
                            <asp:ListItem Text="Cash" Value="1"></asp:ListItem>
                            <asp:ListItem Text="DD" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Fund Transfer" Value="4"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    
                <%--</table>--%>
                  <tr>
                    <td colspan="3" align="center">
                    
                  
                
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
        <asp:TextBox ID="txtReceipt" runat="server" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvReceipt" runat="server" ControlToValidate="txtReceipt" Display="Dynamic" SetFocusOnError="true"
            ErrorMessage="Please Enter Receipt no." ValidationGroup="2"></asp:RequiredFieldValidator>
        </td>
        </tr>
        <tr>
        <td align="center" colspan="2" >
            <asp:Button ID="btnCash" runat="server" ValidationGroup="2" Text="Submit" CssClass="btnCls" 
                onclick="btnCash_Click" />
        </td>
        </tr>
        </table>
        </asp:Panel>
           </td>
           </tr>
        <tr><td  colspan="3" align="center">
        <asp:Panel ID="pnlChqDD" runat="server" Visible="false">
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
                                                ValidationGroup="1" Display="Dynamic"
                                            ></asp:RequiredFieldValidator>
                                            
                                            
                           
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
                                                ValidationGroup="1" Display="Dynamic"
                                            ></asp:RequiredFieldValidator>
                                            
                           
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
                                                ValidationGroup="1" Display="Dynamic"
                                            ></asp:RequiredFieldValidator>
                                           
                           
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
                                                ValidationGroup="1" Display="Dynamic"
                                            ></asp:RequiredFieldValidator>
                                            
                           
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
                                ValidationGroup="1" MaxLength="50" ReadOnly="false" ></asp:TextBox>
                           <asp:CompareValidator ID="cvDate" runat="server" ControlToValidate="txt_chqddate" Operator="DataTypeCheck" Type="Date"
                            ErrorMessage="Please enter valid date as MM/DD/YYYY" Display="Dynamic" SetFocusOnError="true"></asp:CompareValidator>
                           
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
        <td align="center" colspan="2" >
        <asp:Button ID="btnCheqDd" runat="server" Text="Submit" CssClass="btnCls" 
                ValidationGroup="1" onclick="btnCheqDd_Click" />
        </td>
        </tr>
        </table>
        </asp:Panel>
                    </td>
                    </tr>
                   <tr>
                    
                   
                      <td colspan="3" align="center">
                     <asp:Panel ID="pnlFundTransfer" runat="server" Visible="false">
                                 <table width="80%">
                                    <tr>
                                      <td align="left" valign="top">
                                        <asp:RadioButtonList ID="rdl_fund_transfer" runat="server" 
                                              RepeatDirection="Horizontal" AutoPostBack="false" 
                                              >
                                          <asp:ListItem Value="1" Selected="True">ORTGS</asp:ListItem>
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
                                              <b>Date</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_date_utr" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="50" ></asp:TextBox>
                                
                                 <asp:RequiredFieldValidator ID="Req_date_url" runat="server"
                                                ErrorMessage="Please Enter Date MM/DD/YYY"
                                                ControlToValidate="txt_date_utr"
                                                SetFocusOnError="true"
                                                ValidationGroup="1"
                                            ></asp:RequiredFieldValidator>
                                             <%-- <asp:ValidatorCalloutExtender ID="Vali_Req_date_url" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="Req_date_url"
                                            ></asp:ValidatorCalloutExtender>--%>
                           
                           
                        </td>
                                            </tr>
                                            
                                                      <tr>
                                            <td>
                                              <b>Amount</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_amount" runat="server" CssClass="txt_bx" 
                                ValidationGroup="1" MaxLength="10" ReadOnly="true" ></asp:TextBox>
                           
                        </td>
                                            </tr>
                                            
                                             <tr>
        <td align="right"  valign="top" style="padding-bottom:10px;" >
        <asp:Button ID="btn_fundtransfer" runat="server" Text="Submit" CssClass="btnCls" 
                ValidationGroup="1" onclick="btn_fundtransfer_Click"  />
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
</asp:Content>

