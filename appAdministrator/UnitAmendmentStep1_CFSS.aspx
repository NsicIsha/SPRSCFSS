<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="UnitAmendmentStep1_CFSS.aspx.cs" Inherits="appAdministrator_UnitAmendmentStep1_CFSS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        //TO SHOW HIDE MAIL LINK
        $(document).ready(function () {
            var roleid = $("#ctl00_ContentPlaceHolder1_hfrole").val();
            if (roleid == 8) {
                $("#ctl00_ContentPlaceHolder1_lnkmail").hide();
            }
            else {
                $("#ctl00_ContentPlaceHolder1_lnkmail").show();
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hfrole" />
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>


            <table cellpadding="0" cellspacing="0" border="0" width="100%">

                <tr style="width: 100%;">
                    <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Amendment Process 
                    </td>
                </tr>

                <tr>
                    <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%">

                            <tr id="missingvalue" runat="server" align="left" valign="top">
                                <td align="left" valign="top" colspan="3">
                                    <asp:Label ID="lblMissingvalues" runat="server" CssClass="missingValues"></asp:Label>
                                </td>
                            </tr>
                            <%--  <tr align="left" valign="top">
                                <td align="left" valign="top" colspan="3" style="height: 8px;">&nbsp;
                                </td>
                            </tr>--%>

                            <%--<tr align="left" valign="top">
                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                            <asp:HiddenField ID="HdnCatarea" runat="server" />
                                    <asp:HiddenField ID="HdnCatunitsize" runat="server" />
                                </td>
                            </tr>--%>




                            <tr align="left" style="border: thin">
                                <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                                <td align="center" style="width: 92%;" class="leftHeading" colspan="2">
                                    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                        <ProgressTemplate>
                                            <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                            <span style="color: red; font-size: medium">Please wait while we are processing your request <%--Please wait, while your request is being processed--%></span>
                                        </ProgressTemplate>

                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr align="left" style="border: thin">
                                <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                                <td align="center" style="width: 92%;" class="leftHeading" colspan="2">
                                    <asp:Label ID="lblErrorMeg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" CssClass="errMessage"
                                        Visible="false"></asp:Label>
                                </td>
                            </tr>



                            <tr align="left" valign="top" runat="server" visible="true" id="trMainData">
                                <td colspan="3" align="right" valign="top" runat="server" visible="true" id="trWithoutChangesrenewal">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                        <tr id="trbranch1" visible="true" runat="server" align="left" valign="middle">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.0</td>

                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>शाखा/Branch:
                                            </td>


                                            <td align="left" valign="middle" style="height: 10px;">
                                                <asp:DropDownList ID="DropDownListBranch" runat="server" CssClass="clsDDL"
                                                    CausesValidation="false" ValidationGroup="1" AutoPostBack="false">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RFValidatorBranch" runat="server" ErrorMessage="Please select branch"
                                                    ValidationGroup="1" ControlToValidate="DropDownListBranch" Display="None" SetFocusOnError="true"> </asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="VCExtenderBranch" runat="server" TargetControlID="RFValidatorBranch">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.1</td>
                                            <td align="left" valign="top" class="leftHeading">
                                                <span class="MandatoryField">*</span>फर्म की सरंचना/ Constitution of the MSE:
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlConstitution" runat="server" CssClass="clsDDL"
                                                    CausesValidation="false" ValidationGroup="1">
                                                </asp:DropDownList>

                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11ss" runat="server" Display="Dynamic"
                                                    ErrorMessage="Please Constitution of the MSE" ControlToValidate="ddlConstitution" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>



                                            </td>
                                        </tr>
                                        <tr align="left" valign="top">


                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.2
                                            </td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">

                                                <div>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txt_1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1" Enabled="false"
                                                            ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;"
                                                            AutoCompleteType="Disabled"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Valid Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                            ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomRight"
                                                            TargetControlID="RegularExpressionValidator2">
                                                        </asp:ValidatorCalloutExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                            ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomRight"
                                                            TargetControlID="RequiredFieldValidator1">
                                                        </asp:ValidatorCalloutExtender>
                                                        &nbsp;
                                                    </div>
                                                    <div style="float: left;">

                                                        <asp:Label ID="lblPANmsg" runat="server" CssClass="errMessage" Text="PAN already exists !"
                                                            Visible="false"></asp:Label>
                                                        <%-- <br />--%>
                                                        <asp:Label ID="lbl_branchName_chek" runat="server" CssClass="errMessage" Text=""
                                                            Visible="true"></asp:Label>
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.3
                                            </td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField"></span>उद्योग आधार संख्या/Udyam Registration Number:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">

                                                <div>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txt_aahar" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="1"
                                                            ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;"
                                                            AutoCompleteType="Disabled"></asp:TextBox>
                                                        <%--<asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txt_aahar" ErrorMessage="Please Enter Valid Pan Number" CssClass="valField"  ClientValidationFunction="checkTexts" ValidationGroup="reg" Display="Dynamic" ></asp:CustomValidator >--%>

                                                        <%--<asp:RegularExpressionValidator ID="RegularExpressionValidatorAAdhar" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Valid aadher Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                            ValidationExpression="^UDYAM-[A-Z]{2}-\d{2}-\d{7}$" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" PopupPosition="BottomRight"
                                                            TargetControlID="RegularExpressionValidatorAAdhar">
                                                        </asp:ValidatorCalloutExtender>--%>

                                                        <asp:RequiredFieldValidator ID="Requ_addhar" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter aadhar Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                            ValidationGroup="1"></asp:RequiredFieldValidator>
                                                        <%--  <asp:ValidatorCalloutExtender ID="Validator_aadhar_number" runat="server" PopupPosition="BottomRight"
                                                                           TargetControlID="Requ_addhar">
                                                                       </asp:ValidatorCalloutExtender>--%>
                                                    </div>


                                                    <div style="float: left;">



                                                        <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                            Visible="false"></asp:Label>
                                                    </div>

                                                    <%-- <div style="float: left;">
                                                           <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                               Visible="false"></asp:Label>

                                                       </div>--%>
                                            </td>
                                        </tr>





                                        <!-- Isha: Ayush addition start-->
                                        <!-- Isha: Ayush addition end-->
                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">&nbsp;</td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">&nbsp;</td>
                                            <td align="left" valign="top" style="width: 50%;">&nbsp;</td>
                                        </tr>




                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.4
                                            </td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>आवेदक फर्म का नाम/ Name of Applicant MSE:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:TextBox ID="txt_1p1" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                    ValidationGroup="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                    ErrorMessage="Please Enter Name of Applicant MSE" ControlToValidate="txt_1p1"
                                                    SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="RequiredFieldValidator2">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.5</td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>आवेदन की तारीख/ Date of Application:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:TextBox ID="txtproposaldate" Enabled="false" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                    ValidationGroup="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                                                    Display="Dynamic" ControlToValidate="txtproposaldate" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" ControlToValidate="txtproposaldate" Operator="DataTypeCheck" Type="Date"
                                                    ValidationGroup="1"></asp:CompareValidator>
                                                <asp:CalendarExtender ID="CExtendertxt_22" SelectedDate="<%# DateTime.Now %>" runat="server" PopupButtonID="txt_proposal_date"
                                                    TargetControlID="txtproposaldate">
                                                </asp:CalendarExtender>
                                                <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                                                    PopupPosition="TopLeft">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>


                                        <tr>
                                            <td style="height: 10px;"></td>
                                        </tr>


                                        <tr align="left" valign="top">
                                            <td colspan="3" align="left" valign="top" class="rowHeading">(2.a) उद्यम विवरण/ENTERPRISE DETAILS
                                            </td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td colspan="3" align="left" valign="top" class="rowHeading"></td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.a
                                            </td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                    TabIndex="52" ValidationGroup="validateFee">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="None"
                                                    ErrorMessage="Please Select Enterprise Social Category" ControlToValidate="ddl_1e" SetFocusOnError="true"
                                                    ValidationGroup="validateFee"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="RequiredFieldValidator16">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.b
                                            </td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>उद्यमी की विशेष श्रेणी/ Any Special Category of Enterpreneur:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:DropDownList ID="ddl_1d" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                    TabIndex="51" ValidationGroup="validateFee">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="None"
                                                    ErrorMessage="Please Select Special Category of Enterpreneur" ControlToValidate="ddl_1d" SetFocusOnError="true"
                                                    ValidationGroup="validateFee"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="RequiredFieldValidator15">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>
                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.
                                                       c</td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                <span class="MandatoryField">*</span>श्रेणी यूनिट का आकार/Category Unit Size:
                                            </td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:DropDownList ID="ddl_1f" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                    TabIndex="53" ValidationGroup="validateFee">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="None"
                                                    ErrorMessage="Please Select Category Unit Size" ControlToValidate="ddl_1f" SetFocusOnError="true"
                                                    ValidationGroup="validateFee"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" PopupPosition="BottomLeft"
                                                    TargetControlID="RequiredFieldValidator17">
                                                </asp:ValidatorCalloutExtender>
                                            </td>
                                        </tr>






                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.d</td>
                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">Enter the turnover as per latest<br />
                                                audited balance sheet:</td>
                                            <td align="left" valign="top" style="width: 50%;">
                                                <asp:TextBox ID="txtTurnover" runat="server" CssClass="txt_bx" MaxLength="200"></asp:TextBox>
                                                (figures in lakh) 
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true" Display="Dynamic"
                                                    ValidationGroup="validateFee"
                                                    ErrorMessage="Please provide turnover as per latest audited balance sheet"
                                                    ControlToValidate="txtTurnover"></asp:RequiredFieldValidator>


                                                <asp:RegularExpressionValidator
                                                    ID="revAmount" ValidationGroup="validateFee" SetFocusOnError="true" Display="Dynamic"
                                                    runat="server"
                                                    ControlToValidate="txtTurnover"
                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                    ErrorMessage="Enter valid amount in lakhs (up to 2 decimals)"
                                                    ForeColor="Red" />


                                            </td>
                                        </tr>


                                        <tr>
                                            <td align="center" colspan="3" class="header">

                                                <asp:Button ID="btnCalculateFeess" ValidationGroup="validateFee" runat="server" Text="Calcualte Fee" CssClass="btnCls" Width="200px"
                                                    OnClick="btnCalculateFeess_Click" />

                                                <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                        Processing...            
                                                    </ProgressTemplate>

                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>



                                        <tr>
                                            <td align="center" colspan="3" class="header">

                                                <asp:Table Visible="false" ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%">
                                                </asp:Table>

                                            </td>
                                        </tr>
                                        <asp:Panel ID="pnlMain" runat="server" Width="100%" Visible="false">

                                            <tr>
                                                <td align="left" colspan="3" class="header">NSIC Payment Details:<asp:Label ID="lbl_payment_details_nsic" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top" style="padding-top: 10px;">

                                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">
                                                    <asp:Label ID="lbl_registration_fees_text" runat="server" Text="NSIC"></asp:Label>
                                                    Fees:
                                                </td>
                                                <td align="left" valign="top">
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
                                                <td colspan="3" align="left" valign="top" class="style1">Select Payment Option
                                                </td>
                                            </tr>
                                            <tr align="right" valign="top">
                                                <td colspan="3" align="center" class="style2">
                                                    <asp:RadioButtonList ID="radPay" runat="server" RepeatDirection="Horizontal"
                                                        Width="70%" OnSelectedIndexChanged="radPay_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                        <%-- <asp:ListItem Text="Cash" Value="1"></asp:ListItem>--%>
                                                        <asp:ListItem Text="DD" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="Online" Selected="True" Value="4"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" style="height: 10px;"></td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                        <tr>
                                                            <td>
                                                                <asp:Panel ID="pnlChqDD" runat="server" Visible="true">
                                                                    <table width="80%">
                                                                        <tr>
                                                                            <td class="rowStep" colspan="2">
                                                                                <b>CHEQUE / DD / ONLINE PAYMENT DETAILS </b>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Cheque / DD No. / UTR or IMPS No.</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                                    ErrorMessage="Please Enter Cheque or DD Number or UTR or IMPS No."
                                                                                    ControlToValidate="txt_chqddNo"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                                    PopupPosition="BottomRight"
                                                                                    TargetControlID="RequiredFieldValidator9">
                                                                                </asp:ValidatorCalloutExtender>


                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Bank Name</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                                    ErrorMessage="Please Enter Bank Name"
                                                                                    ControlToValidate="txt_BankName"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                                                    PopupPosition="BottomRight"
                                                                                    TargetControlID="RequiredFieldValidator10">
                                                                                </asp:ValidatorCalloutExtender>

                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td>
                                                                                <b>Bank Branch</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                                    ErrorMessage="Please Enter Bank Branch"
                                                                                    ControlToValidate="txt_BankBranch"
                                                                                    SetFocusOnError="true"
                                                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                                    PopupPosition="BottomRight"
                                                                                    TargetControlID="RequiredFieldValidator11">
                                                                                </asp:ValidatorCalloutExtender>

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
                                                                                <b>Cheque / DD / UTR or IMPS Amount</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Cheque / DD / UTR or IMPS Date</b>
                                                                            </td>
                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                <asp:TextBox ID="txt_chqddate" runat="server" CssClass="txt_bx"
                                                                                    ValidationGroup="1" MaxLength="50" ReadOnly="false"></asp:TextBox>


                                                                                <asp:RequiredFieldValidator ID="RFValidatortxt_chqddate" runat="server" ErrorMessage="Please enter date" ControlToValidate="txt_chqddate"
                                                                                    Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                                                <asp:ValidatorCalloutExtender ID="VCExtendertxt_chqddate" runat="server"
                                                                                    TargetControlID="RFValidatortxt_chqddate">
                                                                                </asp:ValidatorCalloutExtender>

                                                                                <asp:CompareValidator ID="CValidatortxt_chqddate" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                                                    Display="Dynamic" ControlToValidate="txt_chqddate" Operator="DataTypeCheck" Type="Date"
                                                                                    ValidationGroup="1"> </asp:CompareValidator>

                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                <b>Upload Scanned Copy of Cheque / DD </b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                <asp:Label ID="Label2" runat="server" CssClass="ErrMsgLbl"></asp:Label>

                                                                            </td>
                                                                        </tr>


                                                                        <tr>
                                                                            <td>
                                                                                <b>Payment Remarks </b>
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                                                                    ValidationGroup="1" MaxLength="500" ReadOnly="false"></asp:TextBox>

                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                                                                    ValidationGroup="1" runat="server" ErrorMessage="Please enter remarks" ControlToValidate="txtChequeDDPaymentRemarks"
                                                                                    Display="Dynamic"></asp:RequiredFieldValidator>




                                                                            </td>
                                                                        </tr>




                                                                        <tr>
                                                                            <td align="right">
                                                                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnCls"
                                                                                    ValidationGroup="1" OnClick="Button1_Click" />
                                                                            </td>
                                                                        </tr>

                                                                        <tr>
                                                                            <td align="center">

                                                                                <asp:Label ID="lblErrorMsgs" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                                            </td>
                                                                        </tr>

                                                                    </table>
                                                                </asp:Panel>
                                                            </td>
                                                        </tr>





                                                    </table>
                                                </td>
                                            </tr>





                                        </asp:Panel>



                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

