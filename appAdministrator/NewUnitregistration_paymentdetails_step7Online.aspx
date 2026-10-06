<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="NewUnitregistration_paymentdetails_step7Online.aspx.cs" Inherits="appAdministrator_NewUnitregistration_paymentdetails_step7Online" %>

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



        .gridview {
            width: 100%;
            border-collapse: collapse;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 14px;
        }


            .gridview th {
                background: #343a40;
                color: #fff;
            }


            .gridview th {
                background-color: #2f75b5;
                color: white;
                padding: 8px;
                text-align: left;
                border: 1px solid #dcdcdc;
            }

            .gridview th {
                background: #343a40;
                color: #fff;
            }

            .gridview td {
                padding: 8px;
                border: 1px solid #dcdcdc;
            }

            .gridview tr:nth-child(even) {
                background-color: #f4f6f9;
            }

            .gridview tr:hover {
                background-color: #e6f2ff;
            }



        .gridview-modern {
            width: 100%;
            border-collapse: collapse;
            font-family: 'Segoe UI', Arial, sans-serif;
            font-size: 14px;
            background: #fff;
            border-radius: 6px;
            overflow: hidden;
            box-shadow: 0px 2px 8px rgba(0,0,0,0.08);
        }

            /* Header */
            .gridview-modern th {
                background: #1f4e79;
                color: #ffffff;
                padding: 10px;
                text-align: left;
                font-weight: 600;
                border-bottom: 2px solid #dee2e6;
            }

            /* Rows */
            .gridview-modern td {
                padding: 9px;
                border-bottom: 1px solid #e6e6e6;
            }

            /* Alternate Row Color */
            .gridview-modern tr:nth-child(even) {
                background-color: #f8f9fc;
            }

            /* Hover Effect */
            .gridview-modern tr:hover {
                background-color: #e9f2ff;
                transition: 0.2s;
            }

            /* Pager */
            .gridview-modern .pager {
                background: #f1f1f1;
                text-align: center;
                padding: 8px;
            }

                .gridview-modern .pager a {
                    margin: 3px;
                    padding: 6px 12px;
                    background: #1f4e79;
                    color: white;
                    text-decoration: none;
                    border-radius: 4px;
                }

                .gridview-modern .pager span {
                    margin: 3px;
                    padding: 6px 12px;
                    background: #6c757d;
                    color: white;
                    border-radius: 4px;
                }






        /* Page Container */
        .payment-container {
            width: 500px;
            margin: 30px auto;
            background: #ffffff;
            /* padding: 25px;*/
            border-radius: 10px;
            box-shadow: 0px 4px 15px rgba(0,0,0,0.08);
            font-family: 'Segoe UI', Arial;
        }

        /* Section Header */
        .header {
            font-size: 20px;
            font-weight: 600;
            color: #2c3e50;
            padding-bottom: 10px;
            border-bottom: 2px solid #e6e6e6;
        }

        /* Label Headings */
        .leftHeading {
            font-weight: 600;
            color: #444;
            font-size: 14px;
        }

        /* Textbox */
        .txt_bx {
            width: 260px;
            padding: 8px 10px;
            border: 1px solid #dcdcdc;
            border-radius: 5px;
            font-size: 14px;
            transition: 0.2s;
        }

            .txt_bx:focus {
                border-color: #0078d4;
                outline: none;
                box-shadow: 0 0 4px rgba(0,120,212,0.2);
            }

        /* Dropdown */
        .form-control {
            width: 260px;
            padding: 8px;
            border-radius: 5px;
            border: 1px solid #ccc;
        }

        /* Section step heading */
        .rowStep {
            background: #f4f7fb;
            padding: 10px;
            font-size: 15px;
            font-weight: 600;
            border-left: 4px solid #0078d4;
        }

        /* GridView Styling */
        .gridview-modern {
            width: 100%;
            border-collapse: collapse;
            margin-top: 10px;
            font-size: 14px;
        }

            .gridview-modern th {
                background: #0078d4;
                color: white;
                padding: 10px;
                text-align: left;
            }

            .gridview-modern td {
                padding: 10px;
                border-bottom: 1px solid #e6e6e6;
            }

            .gridview-modern tr:nth-child(even) {
                background: #f9fbfd;
            }

        /* Radio button list */
        input[type="radio"] {
            margin-right: 6px;
        }

        /* File Upload */
        input[type="file"] {
            padding: 6px;
        }

        /* Error Label */
        .ErrMsgLbl {
            color: red;
            font-size: 12px;
        }

        /* Button */
        .btnSubmit {
            background: #0078d4;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 5px;
            font-size: 15px;
            cursor: pointer;
        }

            .btnSubmit:hover {
                background: #005ea6;
            }

        .summary-row td {
            font-size: 15px;
            padding: 8px 0;
        }

        .summary-row:last-child {
            font-weight: 700;
            color: #0078d4;
            font-size: 16px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="div_payment_details">
        <table cellpadding="2" cellspacing="2" border="0">

            <tr>
                <td align="left" class="headerAll" colspan="3">New Unit
                    <asp:Label ID="lbl_registration_amedment" runat="server" Text="" class="headerAll"></asp:Label>
                    - Step 7
                </td>
            </tr>

            <%-- <tr>
                <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;" colspan="3">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr align="left" valign="top">
                            <td colspan="3" align="right" valign="top" class="rowStep">STEP 7 of 7
                            </td>
                        </tr>
                         
                    </table>
                </td>
            </tr>--%>

            <tr>
                <td align="left" valign="top">
                    <div class="payment-container">
                        <%--  --------------------------Table for nsic--------------------------%>
                        <table cellpadding="2" cellspacing="2" border="0" style="width: 100%;">
                            <tr>
                                <td align="left" colspan="3" class="header">NSIC Payment Details:<asp:Label ID="lbl_payment_details_nsic" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="padding-top: 10px;">
                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">
                                    <asp:Label Visible="false" ID="lbl_registration_fees_text" runat="server" Text=""></asp:Label>
                                    Registration Fees:
                                </td>
                                <td align="right" valign="top" style="padding-right: 30px">
                                    <asp:Label ID="lblUnitAmnt" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="padding-top: 10px;">
                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Service Tax:
                                </td>
                                <td align="right" valign="top" style="padding-right: 30px">
                                    <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="padding-top: 10px;">
                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Total amount payable:
                                </td>
                                <td align="right" valign="top" style="padding-right: 30px">
                                    <asp:Label ID="lblTotAmnt" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Bank Details</td>
                                <td align="right" valign="top" style="padding-right: 30px">
                                    <!-- CFSS Bank Details -->
                                    <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged"></asp:DropDownList>
                                    <br />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" SetFocusOnError="true"
                                        ValidationGroup="1" runat="server" ErrorMessage="Please Select Bank" ControlToValidate="ddlBank" InitialValue="0"
                                        Display="Dynamic">
                                    </asp:RequiredFieldValidator>
                                </td>
                            </tr>


                            <tr align="left" valign="top">
                                <td colspan="3" align="center" valign="top" style="height: 10px;">
                                    <!-- CFSS Bank Details -->
                                    <asp:GridView ID="GridView1" CssClass="gridview-modern" runat="server" AutoGenerateColumns="false" DataKeyNames="BranchID,BankAccountNo,Bank_Name,Bank_IFSC_Code,BranchName,LedgerID,Ledger_Name">
                                        <Columns>
                                            <%-- <asp:BoundField DataField="BranchID" Visible="false" HeaderText="Branch ID" />--%>
                                            <asp:BoundField DataField="BankAccountNo" HeaderText="Account Number" />
                                            <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                            <asp:BoundField DataField="Bank_IFSC_Code" HeaderText="IFSC Code" />
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                            <%-- <asp:BoundField DataField="LedgerID" Visible="false"  HeaderText="Ledger ID" />
                                                                                                        <asp:BoundField DataField="Ledger_Name" Visible="false"  HeaderText="Ledger Name" />--%>
                                        </Columns>
                                    </asp:GridView>

                                </td>
                            </tr>


                            <tr align="left" valign="top">
                                <td colspan="3" align="left" valign="top" class="style1">Select Payment Option
                                <asp:Label ID="lbl_select_payment" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr align="right" valign="top">
                                <td colspan="3" align="left" class="style2">
                                    <asp:RadioButtonList ID="radPay" runat="server" RepeatDirection="Horizontal"
                                        Width="70%"
                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_selectindexchanged">
                                        <%--onselectedindexchanged="radPay_SelectedIndexChanged" --%>
                                        <%-- <asp:ListItem Text="Cash" Value="1"></asp:ListItem>--%>
                                        <asp:ListItem Text="DD" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="Fund Transfer" Selected="True" Value="4"></asp:ListItem>
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
                                                                <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx" TabIndex="1"
                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                                    ErrorMessage="Please Enter Cheque or DD Number"
                                                                    ControlToValidate="txt_chqddNo"
                                                                    SetFocusOnError="true"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <%-- <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator1" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator1"
                                            ></asp:ValidatorCalloutExtender>--%>
                                            
                           
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Bank Name</b>
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx" TabIndex="2"
                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                    ErrorMessage="Please Enter Bank Name"
                                                                    ControlToValidate="txt_BankName"
                                                                    SetFocusOnError="true"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <%-- <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator2" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator2"
                                            ></asp:ValidatorCalloutExtender>--%>
                                          
                           
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <b>Bank Branch</b>
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx" TabIndex="3"
                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                                    ErrorMessage="Please Enter Bank Branch"
                                                                    ControlToValidate="txt_BankBranch"
                                                                    SetFocusOnError="true"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <%--   <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator4" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator4"
                                            ></asp:ValidatorCalloutExtender>--%>
                                        
                           
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <b>Payable At</b>
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_PayableAt" runat="server" CssClass="txt_bx" TabIndex="4"
                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                                    ErrorMessage="Please Enter Payable At"
                                                                    ControlToValidate="txt_PayableAt"
                                                                    SetFocusOnError="true"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                <%--   <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator3" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator3"
                                            ></asp:ValidatorCalloutExtender>--%>
                                          
                           
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <b>Cheque / DD Amount</b>
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx" TabIndex="5"
                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Cheque / DD Date (as MM/DD/YYYY) </b>
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_chqddate" runat="server" CssClass="txt_bx" TabIndex="6"
                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator559" runat="server"
                                                                    ErrorMessage="Please Enter Cheque / DD Date"
                                                                    ControlToValidate="txt_chqddate"
                                                                    SetFocusOnError="true" Display="Dynamic"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                                                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                                                    ControlToValidate="txt_chqddate" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                                                    ValidationGroup="1">
                                                                </asp:RegularExpressionValidator>

                                                                <asp:ImageButton ID="ImageButtonNSIC" runat="server" ImageUrl="~/Images/calendar.png" />
                                                                <asp:CalendarExtender ID="CalendarExtenderNSICFEE" TargetControlID="txt_chqddate" Format="MM/dd/yyyy" PopupButtonID="ImageButtonNSIC" runat="server" />
                                                                 
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Upload Scanned Copy of Cheque / DD </b>
                                                            </td>
                                                            <td align="left">
                                                                <asp:FileUpload ID="FileUpload1" runat="server"  TabIndex="7"/>
                                                                <asp:Label ID="Label1" runat="server" CssClass="ErrMsgLbl"></asp:Label>

                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>
                                                                <b>Payment Remarks </b>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TabIndex="8" TextMode="MultiLine"
                                                                    ValidationGroup="1" MaxLength="500" ReadOnly="false"></asp:TextBox>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                    ErrorMessage="Please Enter Remarks"
                                                                    ControlToValidate="txtChequeDDPaymentRemarks"
                                                                    SetFocusOnError="true" Display="Dynamic"
                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>

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
                                                <asp:Panel ID="pnlFundTransfer" runat="server" Visible="true">
                                                    <table width="80%">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <asp:RadioButtonList ID="rdl_fund_transfer" runat="server"
                                                                    RepeatDirection="Horizontal" AutoPostBack="false">
                                                                    <asp:ListItem Value="1" Selected="True">NEFT</asp:ListItem>
                                                                    <asp:ListItem Value="2">RTGS</asp:ListItem>
                                                                </asp:RadioButtonList>

                                                                <asp:RequiredFieldValidator
                                                                    ID="Req_rdl_fund_transfer"
                                                                    runat="server"
                                                                    ControlToValidate="rdl_fund_transfer"
                                                                    ErrorMessage="Select Payment Option."
                                                                    ValidationGroup="1"
                                                                    SetFocusOnError="true">  
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
                                                                                <b>Fund Transfer Details </b></td>

                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>UTR NO</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_utr_no" runat="server" CssClass="txt_bx" TabIndex="9"
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
                                                                                <asp:TextBox ID="txt_bank_name" runat="server" CssClass="txt_bx" TabIndex="10"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="Req_bank_name" runat="server" Display="Dynamic" 
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
                                                                                <asp:TextBox ID="txt_branch_name" runat="server" CssClass="txt_bx" TabIndex="11"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="Req_branc_name" runat="server"
                                                                                    ErrorMessage="Please Enter Branch Name"
                                                                                    ControlToValidate="txt_branch_name"
                                                                                    SetFocusOnError="true" Display="Dynamic"
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
                                                                                <asp:TextBox ID="txt_date_utr" runat="server" CssClass="txt_bx" TabIndex="12"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="Req_date_url" runat="server"
                                                                                    ErrorMessage="Please Enter Date"
                                                                                    ControlToValidate="txt_date_utr" Display="Dynamic"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>

                                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2"
                                                                                    ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                                                                    ControlToValidate="txt_date_utr" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                                                                    ValidationGroup="1">
                                                                                </asp:RegularExpressionValidator>

                                                                                <asp:ImageButton ID="ImageButtonNSICUTR" runat="server" ImageUrl="~/Images/calendar.png" />
                                                                                <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_date_utr" Format="MM/dd/yyyy" PopupButtonID="ImageButtonNSICUTR" runat="server" />



                                                                                <%-- <asp:ImageButton ID="imgBtnNSICUTR" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtenderimgBtnNSICUTR" TargetControlID="txt_date_utr" Format="MM/dd/yyyy" PopupButtonID="imgBtnNSICUTR" runat="server" />--%>







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
                                                                                <asp:TextBox ID="txt_amount" runat="server" CssClass="txt_bx" TabIndex="13"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>

                                                                            </td>
                                                                        </tr>


                                                                        <tr>
                                                                            <td>
                                                                                <b>Payment Remarks </b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtNeftPaymentRemarks" TabIndex="14" runat="server" CssClass="txt_bx" TextMode="MultiLine"
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
                    </div>



                    <%--  --------------------------End Table for nsic--------------------------%>
                </td>
                <td style="width: 4px;"></td>
                <td align="left" valign="top">
                    <%--  --------------------------Table for Inspection Agency payment Details--------------------------%>

                    <div class="payment-container">

                        <table cellpadding="2" cellspacing="2" border="0" style="width: 100%;">
                            <tr>
                                <td align="left" colspan="3" class="header">Inspection Agency Payment Details: </td>
                            </tr>


                            <tr align="left" valign="top" style="padding-top: 10px;">

                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Inspection Fees:
                                </td>
                                <td align="center" valign="top">
                                    <asp:Label ID="lblAgencyAmnt" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr align="left" valign="top" style="padding-top: 10px;">

                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Service Tax:
                                </td>
                                <td align="center" valign="top">
                                    <asp:Label ID="lbl_service_tax" runat="server"></asp:Label>
                                </td>
                            </tr>

                            <tr align="left" valign="top" style="padding-top: 10px;">

                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Total amount payable:
                                </td>
                                <td align="center" valign="top">
                                    <asp:Label ID="lbl_total_amount_payable" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;    
                                </td>
                            </tr>

                            <%--<tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="style3">
                            &nbsp;</td>
                    </tr>--%>
                            <%--   <tr align="right" valign="top">
                        <td colspan="3" align ="center" class="style2"  >
                            &nbsp;</td>
                    </tr>--%>


                            <%--<tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height:10px;">
                            &nbsp;
                        </td>
                    </tr>  --%>
                            <tr>
                                <td colspan="3" style="height: 100%;">
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="80%">
                                            <tr>
                                                <td colspan="2" class="style1"><%--class="rowStep"--%>
                                                    <b>Option for Inspection Fees </b>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:RadioButtonList ID="rdl_inspectionmode_payment" runat="server" RepeatDirection="Horizontal"
                                                        AutoPostBack="True" OnSelectedIndexChanged="rbl_selectindexchanged_InspectionAgency">
                                                        <%--  <asp:ListItem  Selected="True" Value="2">DD</asp:ListItem>
              <asp:ListItem Value="3">Cheque</asp:ListItem>--%>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px; margin-top: 0px;" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Panel ID="PanelChequeDD" runat="server" Visible="false">
                                                                    <table width="80%">
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Cheque / DD No.</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_chqddNo_inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                    ErrorMessage="Please Enter Cheque or DD Number"
                                                                                    ControlToValidate="txt_chqddNo_inspection"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <%-- <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator5" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator5"
                                            ></asp:ValidatorCalloutExtender>--%>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Bank Name</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_BankNam_inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                                    ErrorMessage="Please Enter Bank Name"
                                                                                    ControlToValidate="txt_BankNam_inspection"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <%-- <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator6" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator6"
                                            ></asp:ValidatorCalloutExtender>--%>
                                        
                           
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Bank Branch</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_BankBranch_inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                                    ErrorMessage="Please Enter Bank Branch"
                                                                                    ControlToValidate="txt_BankBranch_inspection"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <%-- <asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator7" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator7"
                                            ></asp:ValidatorCalloutExtender>--%>
                           
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Payable At</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_PayableAt_inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                                                    ErrorMessage="Please Enter Payable At"
                                                                                    ControlToValidate="txt_PayableAt_inspection"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <%--<asp:ValidatorCalloutExtender ID="Vali_RequiredFieldValidator8" runat="server"
                                                PopupPosition="BottomRight"
                                                TargetControlID="RequiredFieldValidator8"
                                            ></asp:ValidatorCalloutExtender>--%>
                                         
                           
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Cheque / DD Amount</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_DD_Inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Cheque / DD Date (as MM/DD/YYYY)</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox Enabled="false" ID="txt_chqddate_inspection" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>


                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9454" runat="server"
                                                                                    ErrorMessage="Please Enter Cheque / DD Date"
                                                                                    ControlToValidate="txt_chqddate_inspection"
                                                                                    SetFocusOnError="true" Display="Dynamic"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>


                                                                                <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                                                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_chqddate_inspection" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />




                                                                                <%-- <asp:RegularExpressionValidator ID="CValidatortxt_cheq1"
                                                                                ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                                                                ControlToValidate="txt_chqddate_inspection" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                                                                ValidationGroup="1">
                                                                            </asp:RegularExpressionValidator>--%>



                                                                                <%--  <asp:CompareValidator ID="CValidatortxt_cheq1" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                    Display="Dynamic" ControlToValidate="txt_chqddate_inspection" Operator="DataTypeCheck" Type="Date"
                    ValidationGroup="1">
           </asp:CompareValidator>--%>
            
                           
 
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="top">
                                                                                <b>Upload Scanned Copy of Cheque / DD </b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:FileUpload ID="FileUpload2" runat="server" />
                                                                                <asp:Label ID="Label2" runat="server" CssClass="ErrMsgLbl"></asp:Label>

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
                                                <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px; margin-top: 0px;" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td valign="top">
                                                                <asp:Panel ID="PanelInspectionOnlieRites" runat="server" Visible="false">
                                                                    <table width="80%">
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <asp:RadioButtonList ID="rdbInspectionPaymentType_Online" runat="server"
                                                                                    RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbInspectionPaymentType_Online_selectindexchanged">
                                                                                    <asp:ListItem Selected="True" Value="1">Online Payment Gateway</asp:ListItem>
                                                                                    <asp:ListItem Value="2">RTGS/NEFT</asp:ListItem>
                                                                                </asp:RadioButtonList>

                                                                                <asp:RequiredFieldValidator
                                                                                    ID="RequiredFieldValidatorrdbInspectionPaymentType_Online"
                                                                                    runat="server"
                                                                                    ControlToValidate="rdbInspectionPaymentType_Online"
                                                                                    ErrorMessage="Select Payment Option."
                                                                                    ValidationGroup="1"
                                                                                    SetFocusOnError="true">  
                                                                                </asp:RequiredFieldValidator>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2" style="padding-bottom: 10px">

                                                                                <asp:Label ID="lblOnlineNorthernMsg" Visible="false" Font-Bold="true" ForeColor="red" runat="server" />
                                                                                <br />

                                                                                <%--<asp:Label ID="lbl_OnlineMsg" visible="false" Font-Bold="true" ForeColor="Green" Font-Size="Large"  runat="server" />--%>

                                                                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Large" Target="_blank">
                                                                                    <asp:Label ID="lbl_OnlineMsg" runat="server" Font-Size="Small"></asp:Label>
                                                                                </asp:HyperLink>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="left" valign="top">
                                                                                <asp:Panel ID="Panel3" runat="server">
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
                                                                                                <asp:TextBox ID="txtUTRNO_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                    MaxLength="50" ValidationGroup="1" ReadOnly="false"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                                                    ErrorMessage="Please Enter Utr Number"
                                                                                                    ControlToValidate="txtUTRNO_InspectionAgency"
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
                                                                                                <asp:TextBox ID="txtBankName_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                                                    ErrorMessage="Please Enter Bank Name"
                                                                                                    ControlToValidate="txtBankName_InspectionAgency"
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
                                                                                                <asp:TextBox ID="txtBranchName_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                                                    ErrorMessage="Please Enter Branch Name"
                                                                                                    ControlToValidate="txtBranchName_InspectionAgency"
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
                                                                                                <asp:TextBox Enabled="false" ID="txtDate_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="11" MaxLength="50"></asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                                                    ErrorMessage="Please Enter Date"
                                                                                                    ControlToValidate="txtDate_InspectionAgency"
                                                                                                    SetFocusOnError="true"
                                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>

                                                                                                <asp:ImageButton ID="ImageButton11" runat="server" ImageUrl="~/Images/calendar.png" />
                                                                                                <asp:CalendarExtender ID="CalendarExtender11" TargetControlID="txtDate_InspectionAgency" Format="MM/dd/yyyy" PopupButtonID="ImageButton11" runat="server" />



                                                                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                                                                                                ValidationExpression="^([0-9]|0[1-9]|1[012])\/([0-9]|0[1-9]|[12][0-9]|3[01])\/(19|20)\d\d$"
                                                                                                ControlToValidate="txtDate_InspectionAgency" ErrorMessage="Invalid Format. Use MM/DD/YYYY" runat="server"
                                                                                                ValidationGroup="1">
                                                                                            </asp:RegularExpressionValidator>--%>

                                                                                                <%--<asp:CompareValidator ID="CompareValidator112" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                    Display="Dynamic" ControlToValidate="txtDate_InspectionAgency" Operator="DataTypeCheck" Type="Date"
                                    ValidationGroup="1">
                                   </asp:CompareValidator>--%>
                                
                           
                           
                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Amount</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txtAmount_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="11" MaxLength="50" ReadOnly="true"></asp:TextBox>

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
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                        <%--  --------------------------End Table for Inspection Agency payment Details --------------------------%>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
                    <asp:Button ID="Button2" runat="server" Text="Submit" CssClass="btnCls"
                        ValidationGroup="1" OnClick="Button2_Click" />
                    <%--onclick="Button1_Click"--%>
                    <asp:Label ID="lblDateMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
