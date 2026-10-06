<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Online_Renewal_VerifyPaymentDetails_View.aspx.cs" Inherits="appAdministrator_Online_Renewal_VerifyPaymentDetails_View" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">

        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);

        $(document).ready(function () {
            $("#progressbar").progressbar({ value: 100 });
        });

        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }

        function UpdateProgressBar_DroDown() {
            $("#progressbar").progressbar({ value: 100 });
        }

    </script>
    <style type="text/css">
        .style1 {
            background-color: #DDDDDD;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 15px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
        }

        .ErrMsgLbl {
            font-weight: bold;
            font-style: italic;
            text-decoration: underline;
            color: Red;
        }

        .style2 {
            height: 12px;
        }
        .auto-style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 16px;
            font-weight: bold;
            font-style: normal;
            color: #22518F;
            height: 25px;
        }
        .auto-style2 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            font-weight: bold;
            width: 200px;
            padding-left: 5px;
            padding-right: 5px;
        }
        .auto-style3 {
            height: 35px;
        }
    </style>

    <script type="text/javascript" language="javascript">
 function newdocumentopen(x) {
     window.open(x, "NewWindow", "resizable=yes,scrollbars=yes");
 }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_payment_details">
        <table cellpadding="2" cellspacing="2" border="0">

            <%--<tr>
            <td align="left" class="headerAll" colspan="3">
                New Unit <asp:Label ID="lbl_registration_amedment" runat="server" text="" class="headerAll"></asp:Label> - Step 7
            </td>
        </tr>--%>

            <tr>
                <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;" colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr align="left" valign="top">
                            <td colspan="3" align="right" valign="top" class="rowStep"> 

                                 <asp:Button ID="btnAccept" runat="server" OnClick="btnAccept_Click" CssClass="btnCls" CausesValidation="false"
                            Text="Back" Width="60px"   />

                            </td>
                        </tr>
                        <%--  <tr align="left" valign="top" style="padding-top :10px;">
                        <td colspan="3" align="left" valign="top" class="style1">
                            Payment Detail
                        </td>
                 </tr>  --%>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="left" valign="top" colspan="3">
                    <%--  --------------------------Table for nsic--------------------------%>
                    <table cellpadding="2" cellspacing="2" border="0" style="width: 700px;">

                        <tr>
                            <td align="left" colspan="3" class="auto-style1">NSIC Payment Details:<asp:Label ID="lbl_payment_details_nsic" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr align="left" valign="top" style="padding-top: 10px;">

                            <td align="left" valign="top" class="auto-style2" colspan="2">
                                <asp:Label ID="lbl_registration_fees_text" runat="server" Text=""></asp:Label>
                                Fees:
                            </td>
                            <td align="left" valign="top" class="auto-style3">
                                <asp:Label ID="lblUnitAmnt" runat="server"></asp:Label>
                            </td>
                        </tr>


                        <tr align="left" valign="top" style="padding-top: 10px;">

                            <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Service Tax:
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                            </td>
                        </tr>

                        <tr align="left" valign="top" style="padding-top: 10px;">

                            <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Total amount payable:
                            </td>
                            <td align="left" valign="top">
                                <asp:Label ID="lblTotAmnt" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr align="left" valign="top">
                            <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                            </td>
                        </tr>

                        <tr align="left" valign="top">
                            <td colspan="3" align="left" valign="top" class="style1">Payment Option
                                <asp:Label ID="lbl_select_payment" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr align="right" valign="top">
                            <td colspan="3" align="center" class="style2">
                                <asp:RadioButtonList ID="radPay" runat="server" RepeatDirection="Horizontal"
                                    Width="70%"
                                    AutoPostBack="True" OnSelectedIndexChanged="rbl_selectindexchanged">
                                    <%--onselectedindexchanged="radPay_SelectedIndexChanged" --%>
                                    <%-- <asp:ListItem Text="Cash" Value="1"></asp:ListItem>--%>
                                    <asp:ListItem Text="DD" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Fund Transfer" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>

                                <asp:RequiredFieldValidator
                                    ID="Req_radpay"
                                    runat="server"
                                    ControlToValidate="radPay"
                                    ErrorMessage="Select Payment Option."
                                    ValidationGroup="1"
                                    SetFocusOnError="true">  
                                </asp:RequiredFieldValidator>

                                <%--  <asp:ValidatorCalloutExtender ID="Vali_Req_radpay" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="Req_radpay"
                                            ></asp:ValidatorCalloutExtender>--%>
                            </td>
                        </tr>


                        <%--<tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            ravish</td>
                    </tr>--%>

                        <tr>
                            <td valign="top" style="padding-left: 10px; padding-right: 10px;" colspan="3">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:Panel ID="pnlCash" runat="server" Visible="false">
                                                <table width="80%">
                                                    <tr>
                                                        <td class="rowStep" colspan="2">
                                                            <b>Cash Payment Details</b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Amount</b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="lblCashAmnt" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr id="receipt" runat="server">
                                                        <td>
                                                            <b>Receipt No.</b>
                                                        </td>
                                                        <td align="left">
                                                            <%--<asp:Label ID="Label2" runat="server"  ></asp:Label>--%>
                                                            <asp:TextBox ID="txtReceipt" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <%--  <asp:Button ID="btnCash" runat="server" Text="Submit" CssClass="btnCls" 
               /> --%>   <%--onclick="btnCash_Click"--%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                        <%--<tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            ravishj</td>
                    </tr> --%>
                        <tr>
                            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;" colspan="3">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="pnlChqDD" runat="server" Visible="false">
                                                <table width="80%">
                                                    <tr>
                                                        <td class="rowStep" colspan="2">
                                                            <b>CHEQUE / DD PAYMENT DETAILS </b>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Cheque / DD No.</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Bank Name</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <b>Bank Branch</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <b>Payable At</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_PayableAt" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <b>Cheque / DD Amount</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <b>Cheque / DD Date (as MM/DD/YYYY)</b>
                                                        </td>
                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                            <asp:TextBox ID="txt_chqddate" runat="server" CssClass="txt_bx"
                                                                ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                   <%-- <tr>
                                                        <td>
                                                            <b>Upload Scanned Copy of Cheque / DD </b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl"></asp:Label>

                                                        </td>
                                                    </tr>--%>


                                                    <tr>
                                                        <td>
                                                            <b>Payment Remarks </b>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                                                ValidationGroup="1" MaxLength="500" ReadOnly="false"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <%-- <tr>
        <td align="right"  >
       
            ravish</td>
        </tr>--%>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>

                        <%-- <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>--%>

                        <tr>
                            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px; margin-top: 0px;" colspan="3">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td valign="top">
                                            <asp:Panel ID="pnlFundTransfer" runat="server" Visible="false">
                                                <table width="80%">
                                                    <tr>
                                                        <td align="left" valign="top">
                                                          <%--  <asp:RadioButtonList ID="rdl_fund_transfer" runat="server"
                                                                RepeatDirection="Horizontal" AutoPostBack="false">
                                                                <asp:ListItem Value="1" Selected="True">RTGS</asp:ListItem>
                                                                <asp:ListItem Value="2">NEFT</asp:ListItem>
                                                            </asp:RadioButtonList>--%>

                                                             
                                                        </td>
                                                    </tr>

                                                     
                                                    <tr>


                                                        <td align="left" valign="top">
                                                            <asp:Panel ID="pnlOrtgs" runat="server">


                                                                <table width="80%">
                                                                    <tr>
                                                                        <td class="rowStep" colspan="2">
                                                                            <b>Fund Transfer Details </b></td>

                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="top">
                                                                            <b>UTR NO</b>
                                                                        </td>
                                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                            <asp:TextBox ID="txt_utr_no" runat="server" CssClass="txt_bx"
                                                                                ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="req_utrno" runat="server"
                                                                                ErrorMessage="Please Enter Utr Number"
                                                                                ControlToValidate="txt_utr_no"
                                                                                SetFocusOnError="true"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
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
                                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                            <asp:TextBox ID="txt_bank_name" runat="server" CssClass="txt_bx"
                                                                                ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="Req_bank_name" runat="server"
                                                                                ErrorMessage="Please Enter Bank Name"
                                                                                ControlToValidate="txt_bank_name"
                                                                                SetFocusOnError="true"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
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
                                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                            <asp:TextBox ID="txt_branch_name" runat="server" CssClass="txt_bx"
                                                                                ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="Req_branc_name" runat="server"
                                                                                ErrorMessage="Please Enter Branch Name"
                                                                                ControlToValidate="txt_branch_name"
                                                                                SetFocusOnError="true"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator>

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
                                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                            <asp:TextBox ID="txt_date_utr" runat="server" CssClass="txt_bx"
                                                                                ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                            <asp:RequiredFieldValidator ID="Req_date_url" runat="server"
                                                                                ErrorMessage="Please Enter Date"
                                                                                ControlToValidate="txt_date_utr"
                                                                                SetFocusOnError="true"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator>

                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                                ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                                                                ControlToValidate="txt_date_utr" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                                                                ValidationGroup="1">
                                                                            </asp:RegularExpressionValidator>

                                                                            <%-- <asp:CompareValidator ID="CValidatortxt_cheq3" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                    Display="Dynamic" ControlToValidate="txt_date_utr" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="1">
                                   </asp:CompareValidator>--%>
                                
                           
                           
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td>
                                                                            <b>Amount</b>
                                                                        </td>
                                                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                            <asp:TextBox ID="txt_amount" runat="server" CssClass="txt_bx"
                                                                                ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>

                                                                        </td>
                                                                    </tr>


                                                                    <tr>
                                                                        <td>
                                                                            <b>Payment Remarks </b>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txtNeftPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                                                                ValidationGroup="1" MaxLength="500" ReadOnly="false"></asp:TextBox>

                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                                                ErrorMessage="Please Enter Remarks"
                                                                                ControlToValidate="txtNeftPaymentRemarks"
                                                                                SetFocusOnError="true" Display="Dynamic"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
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

                    </table>




                    <%--  --------------------------End Table for nsic--------------------------%>
                </td>
                
            </tr>

           <tr>
               <td colspan="4">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                 
                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                               
                                
                                <tr align="left" valign="top">
                                    <td align="left" valign="top" colspan="2" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Accept" TabIndex="4" ValidationGroup="1" Visible="false"
                                            CssClass="btnCls" OnClick="btnSubmit_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

               </td>
           </tr>
            <%--<tr>
        <td align="center" colspan="3"  >
        <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btnCls" 
                ValidationGroup="1" onclick="Button2_Click"  />  
            <asp:Label ID="lblDateMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </td>
        </tr>--%>
        </table>

    </div>

</asp:Content>
