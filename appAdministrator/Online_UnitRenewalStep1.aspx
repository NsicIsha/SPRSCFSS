<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Online_UnitRenewalStep1.aspx.cs" Inherits="appAdministrator_Online_UnitRenewalStep1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>--%>

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
    <table cellpadding="0" cellspacing="0" border="0" width="100%">

        <tr style="width: 100%;">
            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Unit Renewal - Step 1
            </td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-2</td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-3</td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-4</td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-5</td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-6</td>
                                    <td style="width: 10%; padding-left: 10px; padding-right: 10px;">Step-7</td>
                                </tr>

                            </table>

                        </td>
                    </tr>


                    <tr id="step2tr" runat="server" style="width: 100%;">
                        <td id="step2" style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td id="progressbar" align="left"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="style4">STEP 1 of 7
                        </td>
                    </tr>
                    <tr id="missingvalue" runat="server" align="left" valign="top">
                        <td align="left" valign="top" colspan="3">
                            <asp:Label ID="lblMissingvalues" runat="server" CssClass="missingValues"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" colspan="3" style="height: 8px;">&nbsp;
                        </td>
                    </tr>
                    <%-- <tr id="missingvalueSendMail" runat="server" align="right" valign="top" visible="false">
                        <td align="right" valign="top" colspan="3">
                            <asp:HyperLink ID="lnkmail" runat="server" Text="Mail Missing Info To Admin" Target="_blank" CssClass="missingEmail"></asp:HyperLink>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                            <asp:HiddenField ID="HdnCatarea" runat="server" />
                            <asp:HiddenField ID="HdnCatunitsize" runat="server" />
                        </td>
                    </tr>

                    <tr align="left" style="border: thin">
                        <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                        <td align="left" style="width: 9%;" class="leftHeading">Renewal Type
                        </td>
                        <td align="left" valign="top" style="width: 86%;"><%--OnSelectedIndexChanged="rdbMsmeRegtype_SelectedIndexChanged"--%>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>



                                    <asp:RadioButtonList Width="800px" ID="rdbRenewaltype" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbRenewaltype_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Text="<b>Renewal </b> <br/>(Renewal for only service units)"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="<b>Renewal Cum Amendment </b><br/>(Renewal for units having either manufacturing or manufacturing & service both)"></asp:ListItem>
                                    </asp:RadioButtonList>




                                    <%-- <asp:RadioButtonList Width="800px" ID="rdbRenewaltype" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbRenewaltype_SelectedIndexChanged">
                                <asp:ListItem Value="1"  Text="<b>Only Renewal </b> <br/>(Third Party inspection is involved in case the unit is engaged in manufacturing or manfucaturing and services both.<br/>Units are requested to choose Inspection Agnecy according to their domain expertise and jurisdiction.)"></asp:ListItem>
                                <asp:ListItem Value="2" Text="<b>Renewal Cum Ammendment </b><br/>(Change Required in Factory Address or Stores, Constitution or PAN)"></asp:ListItem>
                            </asp:RadioButtonList>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>


                    <tr align="left" style="border: thin">
                        <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                        <td align="center" style="width: 92%;" class="leftHeading" colspan="2">
                            <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel6">

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
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" runat="server" visible="false" id="trWithoutChangesrenewal">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">Branch
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:Label ID="lblBranch" runat="server" CssClass="updateTableLeft"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>आवेदक फर्म का नाम/ Name of Applicant MSE:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txt_1p1" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="2"
                                                        ValidationGroup="1"></asp:TextBox><%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                ErrorMessage="Please Enter Name of Applicant firm" ControlToValidate="txt_1p1"
                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator2">
                            </asp:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.2
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                        <ContentTemplate>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txt_1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1"
                                                                        ValidationGroup="1" Style="text-transform: uppercase;"
                                                                        AutoCompleteType="Disabled" OnTextChanged="txt_1_TextChanged"
                                                                        AutoPostBack="True" EnableTheming="True"></asp:TextBox>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                                                                        ErrorMessage="Please Enter Valid Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                        ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomRight"
                                                                        TargetControlID="RegularExpressionValidator2">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                                                        ErrorMessage="Please Enter Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomRight"
                                                                        TargetControlID="RequiredFieldValidator1">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    &nbsp;
                                                                </div>
                                                                <div style="float: left;">
                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                                        <ProgressTemplate>
                                                                            <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                    <asp:Label ID="lblPANmsg" runat="server" CssClass="errMessage" Text="PAN already exists !"
                                                                        Visible="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.3</td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">Declaration of having made the entry in MSME Data Bank</td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:RadioButtonList ID="rdo_declarationMSME" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                        <asp:ListItem Value="2">No</asp:ListItem>
                                                    </asp:RadioButtonList></td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.4</td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField"></span>उद्योग आधार संख्या/Udyog Aadhar number:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:UpdatePanel ID="update_adhar" runat="server">
                                                        <ContentTemplate>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:TextBox ID="txt_aahar" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="1"
                                                                        ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;"
                                                                        AutoCompleteType="Disabled" OnTextChanged="txt_aahar_TextChanged"></asp:TextBox>

                                                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidatorAAdhar" runat="server" Display="None"
                                                                        ErrorMessage="Please Enter Valid aadher Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                                        ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" PopupPosition="BottomRight"
                                                                        TargetControlID="RegularExpressionValidatorAAdhar">
                                                                    </asp:ValidatorCalloutExtender>--%>
                                                                    <%--  <asp:RequiredFieldValidator ID="Requ_addhar" runat="server" Display="None"
                                                ErrorMessage="Please Enter aadhar Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="Validator_aadhar_number" runat="server" PopupPosition="BottomRight"
                                                TargetControlID="Requ_addhar">
                                            </asp:ValidatorCalloutExtender>--%>
                                                                </div>
                                                            </div>
                                                            <asp:Label ID="lbl_adhar_number" runat="server" Visible="false" CssClass="errMessage" Text="Aadhar already exists !"></asp:Label>
                                                            </div>
                                               </div>   
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.5</td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>प्रस्ताव की तारीख/Proposal Date (<span style="color: red">On basis of this date Renewal fee will be calculated </span>):
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <div>
                                                        <div style="float: left;">
                                                            <asp:TextBox ID="txtproposaldate" runat="server" CssClass="txt_bx" MaxLength="80" TabIndex="1" Enabled="false"
                                                                ValidationGroup="1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                                                                Display="Dynamic" ControlToValidate="txtproposaldate" ValidationGroup="1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                                            <%-- <asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                        Display="Dynamic" ControlToValidate="txtproposaldate" Operator="DataTypeCheck" Type="Date"
                                        ValidationGroup="1"></asp:CompareValidator>
                                    <asp:CalendarExtender ID="CExtendertxt_22" runat="server" PopupButtonID="txtproposaldate"
                                        TargetControlID="txtproposaldate">
                                    </asp:CalendarExtender>
                                    <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                                        PopupPosition="TopLeft">
                                    </asp:ValidatorCalloutExtender>--%>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top" runat="server" id="trDiarynumber" visible="false">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading"></td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>Diary Number:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <div>
                                                        <div style="float: left;">

                                                            <asp:TextBox ID="txtproposalnumber" runat="server" CssClass="txt_bx" MaxLength="50"
                                                                ValidationGroup="1" Style="text-transform: uppercase;"
                                                                AutoCompleteType="Disabled"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="Req_proposal_number" runat="server" Display="None"
                                                                ErrorMessage="Please Enter Diary Number" ControlToValidate="txtproposalnumber" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="Vcq_proposalnumber" runat="server" PopupPosition="BottomRight"
                                                                TargetControlID="Req_proposal_number">
                                                            </asp:ValidatorCalloutExtender>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading">(1.a) मुख्य कार्यालय/ HEAD OFFICE
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" class="leftHeading">&nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.1)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>पता/ Address:
                                                </td>
                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                    <asp:TextBox ID="txt_1a1" runat="server" CssClass="txt_bx" TabIndex="3" ValidationGroup="1"
                                                        TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                            ErrorMessage="Please Enter HEAD OFFICE Address" ControlToValidate="txt_1a1" SetFocusOnError="true"
                                                            ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                                                TargetControlID="RequiredFieldValidator3">
                                                            </asp:ValidatorCalloutExtender>
                                                    <asp:CustomValidator ID="CustomValidatorHeadOfficeAddress" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                        Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1a1"
                                                        ValidationGroup="1"></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="CustomValidatorHeadOfficeAddress">
                                                        </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.2)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>पिन कोड/ Pin Code:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1a2" runat="server" CssClass="txt_bx" MaxLength="6" TabIndex="4"
                                                        ValidationGroup="1"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                                            ErrorMessage="Please Enter HEAD OFFICE Pin Code" ControlToValidate="txt_1a2"
                                                            SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>


                                                    <asp:RegularExpressionValidator
                                                        ID="revPINFadffc"
                                                        runat="server"
                                                        ControlToValidate="txt_1a2" ValidationGroup="1"
                                                        ErrorMessage="Enter a valid 6-digit PIN"
                                                        ValidationExpression="^[1-9][0-9]{5}$">
                                                    </asp:RegularExpressionValidator>

                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RequiredFieldValidator4">
                                                    </asp:ValidatorCalloutExtender>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                        TargetControlID="txt_1a2">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.3)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>राज्य/ State:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="ddlState_a3" runat="server" CssClass="clsDDL" AutoPostBack="True"
                                                                        TabIndex="5" ValidationGroup="1"
                                                                        OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                                        ErrorMessage="Please Select HEAD OFFICE State" ControlToValidate="ddlState_a3"
                                                                        SetFocusOnError="true" ValidationGroup="1" InitialValue="--Select State--"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
                                                                            TargetControlID="RequiredFieldValidator5">
                                                                        </asp:ValidatorCalloutExtender>
                                                                </div>
                                                                <div style="float: left;">
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                                        <ProgressTemplate>
                                                                            <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.4)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>ज़िला/शहर/District/ City:
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlCity_a4" runat="server" CssClass="clsDDL" TabIndex="6"
                                                                ValidationGroup="1">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                                        ErrorMessage="Please Select HEAD OFFICE City" ControlToValidate="ddlCity_a4"
                                                        SetFocusOnError="true" ValidationGroup="1" InitialValue="--Select City--"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="RequiredFieldValidator6">
                                                        </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.5)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>फोन/ Telephone:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1a5" runat="server" CssClass="txt_bx" MaxLength="6" TabIndex="7"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567, 9871234567)</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                                        ErrorMessage="Please Enter HEAD OFFICE Telephone" ControlToValidate="txt_1a5"
                                                        SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="RequiredFieldValidator7">
                                                        </asp:ValidatorCalloutExtender>
                                                    <asp:FilteredTextBoxExtender ID="filtertexboxextender" runat="server" FilterMode="ValidChars"
                                                        FilterType="Custom" TargetControlID="txt_1a5" ValidChars="1234567890,() .-+">
                                                    </asp:FilteredTextBoxExtender>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_1a5"
                                Display="None" ErrorMessage="Please provide valid phone no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                                    <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RegularExpressionValidator1">
                            </asp:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.5)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading"><span class="MandatoryField">*</span>मोबाइल नंबर/ Mobile No:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_mob" ValidationGroup="1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="6"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ValidationGroup="1" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" ID="RequiredFieldValidator33" runat="server" ControlToValidate="txt_mob" ErrorMessage="Required"></asp:RequiredFieldValidator>

                                                    <asp:RegularExpressionValidator ValidationGroup="1" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Mobile Number."
                                                        ValidationExpression="^([0|\+[0-9]{1,5})?([6-9][0-9]{9})$" ControlToValidate="txt_mob"
                                                        ForeColor="Red" Display="Dynamic"></asp:RegularExpressionValidator>
                                                    <asp:FilteredTextBoxExtender ID="fitmob" runat="server" FilterMode="ValidChars"
                                                        FilterType="Custom" TargetControlID="txt_mob" ValidChars="1234567890">
                                                    </asp:FilteredTextBoxExtender>
                                                    <br />
                                                    <span style="color: #FF0000; padding-top: 5px;">(eg:&nbsp; 9871234567)</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr align="left" valign="top" runat="server" id="trfax" visible="false">
                                                <td align="left" valign="top" class="leftHeading">(1.a.6)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">फैक्स/ Fax:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1a6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="8"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000;">(eg: 011-1234567)</span>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_fax" runat="server" FilterMode="ValidChars"
                                                        FilterType="Custom" TargetControlID="txt_1a6" ValidChars="1234567890,() .-+">
                                                    </asp:FilteredTextBoxExtender>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.a.7)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">
                                                    <span class="MandatoryField">*</span>  ई-मेल/ E-mail:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1a7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="9"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000;">(Please provide only one email id e.g.: free123@nsic.co.in)</span>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                                                        ErrorMessage="Please Enter HEAD OFFICE E-mail" ControlToValidate="txt_1a7" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender91" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="RequiredFieldValidator8">
                                                        </asp:ValidatorCalloutExtender>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_1a7"
                                                        Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RegularExpressionValidator4">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">&nbsp;</td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading">(1.b) कारखाना/ FACTORY ADDRESSES
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="leftHeading">
                                                    <asp:GridView ID="GridFactory" runat="server" AutoGenerateColumns="False"
                                                        OnRowDataBound="GridFactory_RowDataBound">
                                                        <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                                        <RowStyle CssClass="GrdviewRowOdd" />
                                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="*Address" ControlStyle-Width="150px" ItemStyle-Width="180px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_1b1" runat="server" ValidationGroup="1"
                                                                        TextMode="MultiLine" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Address") %>'></asp:TextBox><asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                                                                    <asp:HiddenField ID="hdnstate" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_StateId") %>' />
                                                                    <asp:HiddenField ID="hdncity" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_CityId") %>' />

                                                                    <asp:HiddenField ID="hdnNatureOfOperation" runat="server" Value='<%# Bind("fGPtbl_CUnit_Enterprise") %>' />

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                                                        ErrorMessage="Please Enter FACTORY Address" ControlToValidate="txt_1b1" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" PopupPosition="BottomLeft"
                                                                            TargetControlID="RequiredFieldValidator9">
                                                                        </asp:ValidatorCalloutExtender>
                                                                    <asp:CustomValidator ID="CustomValidatorFactoryAddress" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                                        Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1b1"
                                                                        ValidationGroup="1"></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" PopupPosition="BottomLeft"
                                                                            TargetControlID="CustomValidatorFactoryAddress">
                                                                        </asp:ValidatorCalloutExtender>
                                                                </ItemTemplate>

                                                                <ControlStyle Width="150px"></ControlStyle>

                                                                <ItemStyle Width="180px"></ItemStyle>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="*Pin" ControlStyle-Width="80px" ItemStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_1b2" runat="server" MaxLength="500"
                                                                        ValidationGroup="1" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Zip") %>'></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None"
                                                                            ErrorMessage="Please Enter FACTORY Pin Code" ControlToValidate="txt_1b2" SetFocusOnError="true"
                                                                            ValidationGroup="1"></asp:RequiredFieldValidator>


                                                                    <asp:RegularExpressionValidator
                                                                        ID="revPINFactoryPIN"
                                                                        runat="server"
                                                                        ControlToValidate="txt_1b2" ValidationGroup="1"
                                                                        ErrorMessage="Enter a valid 6-digit PIN"
                                                                        ValidationExpression="^[1-9][0-9]{5}$">
                                                                    </asp:RegularExpressionValidator>




                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" PopupPosition="BottomLeft"
                                                                        TargetControlID="RequiredFieldValidator10">
                                                                    </asp:ValidatorCalloutExtender>
                                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1B_Factory_Zip" runat="server" FilterType="Numbers"
                                                                        TargetControlID="txt_1b2">
                                                                    </asp:FilteredTextBoxExtender>
                                                                </ItemTemplate>

                                                                <ControlStyle Width="80px"></ControlStyle>

                                                                <ItemStyle Width="100px"></ItemStyle>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="*State">
                                                                <ItemTemplate>
                                                                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>--%>
                                                                    <div>
                                                                        <div style="float: left;">
                                                                            <asp:DropDownList ID="ddlState_b3" runat="server" AutoPostBack="True"
                                                                                ValidationGroup="1">
                                                                            </asp:DropDownList>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="None"
                                                                                ErrorMessage="Please Select FACTORY State" ControlToValidate="ddlState_b3" SetFocusOnError="true"
                                                                                ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" PopupPosition="BottomLeft"
                                                                                    TargetControlID="RequiredFieldValidator19">
                                                                                </asp:ValidatorCalloutExtender>
                                                                        </div>
                                                                        <%-- <div style="float: left;">
                                                        <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                            <ProgressTemplate>
                                                                <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </div>--%>
                                                                    </div>
                                                                    <%--</ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="*District/City">
                                                                <ItemTemplate>
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlCity_b4" runat="server" TabIndex="12" ValidationGroup="1">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="None"
                                                                        ErrorMessage="Please Select FACTORY City" ControlToValidate="ddlCity_b4" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" PopupPosition="BottomLeft"
                                                                            TargetControlID="RequiredFieldValidator12">
                                                                        </asp:ValidatorCalloutExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tel." ControlStyle-Width="80px" ItemStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_1b5" runat="server" MaxLength="500" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Phone") %>'></asp:TextBox>

                                                                </ItemTemplate>

                                                                <ControlStyle Width="80px"></ControlStyle>

                                                                <ItemStyle Width="100px"></ItemStyle>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Fax" ControlStyle-Width="80px" ItemStyle-Width="100px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_1b6" runat="server" MaxLength="500" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Fax") %>'></asp:TextBox>

                                                                </ItemTemplate>

                                                                <ControlStyle Width="80px"></ControlStyle>

                                                                <ItemStyle Width="100px"></ItemStyle>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Email" ControlStyle-Width="100px" ItemStyle-Width="120px">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txt_1b7" runat="server" MaxLength="500" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Email") %>'
                                                                        ValidationGroup="1" CssClass="txt_bxEmail"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="None"
                                            ErrorMessage="Please Enter FACTORY E-mail" ControlToValidate="txt_1b7" SetFocusOnError="true"
                                            ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" PopupPosition="BottomLeft"
                                            TargetControlID="RequiredFieldValidator14">
                                        </asp:ValidatorCalloutExtender>--%>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_1b7"
                                                                        Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender23" runat="server" PopupPosition="BottomRight"
                                                                        TargetControlID="RegularExpressionValidator6">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </ItemTemplate>

                                                                <ControlStyle Width="100px"></ControlStyle>

                                                                <ItemStyle Width="120px"></ItemStyle>
                                                            </asp:TemplateField>

                                                            <%--sRA--%>

                                                            <asp:TemplateField HeaderText="*Nature of Operation">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlNatureOfOperation_b8" runat="server" TabIndex="12" ValidationGroup="1">
                                                                        <asp:ListItem Selected="True" Value="1">Manufacturing</asp:ListItem>
                                                                        <asp:ListItem Value="2">Service</asp:ListItem>
                                                                        <%--<asp:ListItem Value="3">Both</asp:ListItem>--%>
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="None"
                                                                        ErrorMessage="Please Select Nature of Operation" ControlToValidate="ddlNatureOfOperation_b8" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" PopupPosition="BottomLeft"
                                                                        TargetControlID="RequiredFieldValidator13">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                        </Columns>
                                                    </asp:GridView>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td style="height: 10px;"></td>
                                            </tr>

                                            <%--(1.c) शाखाएँ/ BRANCHES--%>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading">(1.c) शाखाएँ/ BRANCHES ADDRESS (optional)
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.1)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">पता/ Address:
                                                </td>
                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                    <asp:TextBox ID="txt_1c1" runat="server" CssClass="txt_bx" TabIndex="44" ValidationGroup="1"
                                                        TextMode="MultiLine"></asp:TextBox></td>
                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                    Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1c1"
                                                    ValidationGroup="1"></asp:CustomValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="CustomValidator1">
                                                    </asp:ValidatorCalloutExtender>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.2)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">पिन कोड/ Pin Code:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1c2" runat="server" CssClass="txt_bx" MaxLength="6" TabIndex="45"
                                                        ValidationGroup="1"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FTBExtendertxt_1c2" runat="server" FilterType="Numbers"
                                                        TargetControlID="txt_1c2">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.3)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">राज्य/ State:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                        <ContentTemplate>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="ddlState_c3" runat="server" CssClass="clsDDL" AutoPostBack="True"
                                                                        TabIndex="46" OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div style="float: left;">
                                                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server" AssociatedUpdatePanelID="UpdatePanel4">
                                                                        <ProgressTemplate>
                                                                            <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.4)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">ज़िला/शहर/District/ City:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlCity_c4" runat="server" CssClass="clsDDL" TabIndex="47">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="None" ValidationGroup="1"
                                                        ErrorMessage="Please Select City" SetFocusOnError="true" ControlToValidate="ddlCity_c4" InitialValue="--Select City--"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RequiredFieldValidator11"></asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.5)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">फोन/ Telephone:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1c5" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="48"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567, 9871234567)</span>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txt_1c5"
                                Display="None" ErrorMessage="Please provide valid phone no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender21" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RegularExpressionValidator5">
                            </asp:ValidatorCalloutExtender>--%>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" FilterType="Custom"
                                                        ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.6)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">फैक्स/ Fax:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1c6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="49"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567)</span>
                                                    <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                ControlToValidate="txt_1c6" Display="None" ErrorMessage="Please provide valid fax no. !"
                                SetFocusOnError="True" ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender27" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RegularExpressionValidator10">
                            </asp:ValidatorCalloutExtender>--%>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_branchfax" runat="server" FilterMode="ValidChars" FilterType="Custom"
                                                        ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                                    </asp:FilteredTextBoxExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading">(1.c.7)
                                                </td>
                                                <td align="left" valign="top" class="leftHeading">ई-मेल/ E-mail:
                                                </td>
                                                <td align="left" valign="top">
                                                    <asp:TextBox ID="txt_1c7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="50"
                                                        ValidationGroup="1"></asp:TextBox><br />
                                                    <span style="color: #FF0000;">(eg: free123@nsic.co.in)</span>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_1c7"
                                                        Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1"> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </asp:RegularExpressionValidator>
                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RegularExpressionValidator7">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading"></td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.d
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>सामाजिक श्रेणी की उद्यम/ Enterprise Social Category::
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false" TabIndex="52"
                                                        ValidationGroup="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="None"
                                                        ErrorMessage="Please Select Enterprise Social Category" ControlToValidate="ddl_1e" SetFocusOnError="true"
                                                        ValidationGroup="1" InitialValue="--Select Category Cast--"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="RequiredFieldValidator16">
                                                        </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.e
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>उद्यमी की विशेष श्रेणी/Any Special Category of Enterpreneur:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:DropDownList ID="ddl_1d" runat="server" CssClass="clsDDL" AutoPostBack="false" TabIndex="51"
                                                        ValidationGroup="1">
                                                    </asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="None"
                                ErrorMessage="Please Select Category Area" ControlToValidate="ddl_1d" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator15">
                            </asp:ValidatorCalloutExtender>--%>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.f
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>श्रेणी यूनिट का आकार/Category Unit Size:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:DropDownList ID="ddl_1f" runat="server" CssClass="clsDDL" AutoPostBack="false" TabIndex="53"
                                                        ValidationGroup="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="None"
                                                        ErrorMessage="Please Select Category Unit Size" ControlToValidate="ddl_1f" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator><asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" PopupPosition="BottomLeft"
                                                            TargetControlID="RequiredFieldValidator17">
                                                        </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>


                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading">बैंक विवरण (धन वापसी के मामले में आवश्यक) / Bank Details (Required in case of refund)</td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.1
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>बैंक का प्रकार / Bank Type:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:DropDownList ID="ddlBankType" Width="200px" runat="server" CssClass="clsDDL" AutoPostBack="true"
                                                        TabIndex="54" ValidationGroup="1" OnSelectedIndexChanged="ddlBankType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0">--Select Bank Type--</asp:ListItem>
                                                        <asp:ListItem Value="1">Private Sector Banks</asp:ListItem>
                                                        <asp:ListItem Value="2">Public Sector Banks</asp:ListItem>
                                                        <asp:ListItem Value="3">Regional Rural Banks</asp:ListItem>
                                                        <asp:ListItem Value="4">Small Finance Banks </asp:ListItem>

                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvbanktype" runat="server" Display="Dynamic" InitialValue="0"
                                                        ErrorMessage="please select bank type" ControlToValidate="ddlbanktype" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="vcebanktype" runat="server" PopupPosition="bottomleft"
                                                        TargetControlID="rfvbanktype">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.2
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>बैंक का नाम / Bank Name:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                        TabIndex="55" ValidationGroup="1">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RFVBankName" runat="server" Display="Dynamic" InitialValue="0"
                                                        ErrorMessage="Please Select Bank Name" ControlToValidate="ddlBankName" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="VCEBankName" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVBankName">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>


                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.3
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>
                                                    आईएफएससी कोड /IFSC Code:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="txt_bxEmail" MaxLength="11" TabIndex="56"
                                                        ValidationGroup="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVIFSCCODE" runat="server" Display="Dynamic"
                                                        ErrorMessage="Please Enter IFSC Code" ControlToValidate="txtIFSCCode" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>


                                                    <asp:RegularExpressionValidator ID="rfvIFSCCODEss" runat="server" ValidationGroup="1" Display="Dynamic"
                                                        ControlToValidate="txtIFSCCODE" ErrorMessage="Provide Valid IFSC Code"
                                                        ValidationExpression="^[A-Za-z]{4}0[A-Z0-9a-z]{6}$"></asp:RegularExpressionValidator>



                                                    <asp:ValidatorCalloutExtender ID="VCERFVIFSCCODE" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVIFSCCODE">
                                                    </asp:ValidatorCalloutExtender>
                                                    <asp:ValidatorCalloutExtender ID="VCErfvIFSCCODEss" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="rfvIFSCCODEss">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.4
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>शाखा का नाम / Branch Name:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="txt_bxEmail" MaxLength="100" TabIndex="57"
                                                        ValidationGroup="1"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RFVBranchName" runat="server" Display="Dynamic"
                                                        ErrorMessage="Please Enter Branch Name" ControlToValidate="txtBranchName" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="VCERFVBranchName" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVBranchName">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.5
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>शाखा का पता /Branch Address:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txtBranchAddress" runat="server" CssClass="txt_bxEmail" MaxLength="250" TabIndex="58" TextMode="MultiLine"
                                                        ValidationGroup="1"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="RFVBranchAddress" runat="server" Display="Dynamic"
                                                        ErrorMessage="Please Enter Branch Address" ControlToValidate="txtBranchAddress" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="VCERFVBranchAddress" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVBranchAddress">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.6
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>खाते का प्रकार / Account Type:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%; padding-top: 10px; padding-bottom: 10px">
                                                    <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                        TabIndex="59" ValidationGroup="1">
                                                        <asp:ListItem Value="0">-- Account Type--</asp:ListItem>
                                                        <asp:ListItem Value="1">Current Account</asp:ListItem>
                                                        <asp:ListItem Value="2">Saving Account</asp:ListItem>
                                                        <asp:ListItem Value="3">Others</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator ID="RFVAccountType" runat="server" Display="Dynamic" InitialValue="0"
                                                        ErrorMessage="Please Select Account Type" ControlToValidate="ddlAccountType" SetFocusOnError="true"
                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                    <asp:ValidatorCalloutExtender ID="VCERFVAccountType" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVAccountType">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>

                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.7
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>खाता संख्या / Account Number:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txtAccountNumber" runat="server" CssClass="txt_bxEmail" MaxLength="20" TabIndex="60"
                                                        ValidationGroup="1"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ID="rfvAccountNumber" Display="Dynamic" ControlToValidate="txtAccountNumber" ErrorMessage="Account Number Required" runat="server" ValidationGroup="1" />

                                                    <asp:RegularExpressionValidator ID="rfvvAccountNumber" runat="server" ValidationGroup="1" Display="Dynamic"
                                                        ControlToValidate="txtAccountNumber" ErrorMessage="Provide Valid Bank Account Number"
                                                        ValidationExpression="[1-9][0-9]{8,18}$"></asp:RegularExpressionValidator>


                                                    <asp:ValidatorCalloutExtender ID="VCErfvAccountNumber" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="rfvAccountNumber">
                                                    </asp:ValidatorCalloutExtender>
                                                    <asp:ValidatorCalloutExtender ID="VCErfvvAccountNumber" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="rfvvAccountNumber">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>


                                            <tr align="left" valign="top" style="padding: 10px">
                                                <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.8
                                                </td>
                                                <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                    <span class="MandatoryField">*</span>खाता धारक का नाम / Account Holder Name:
                                                </td>
                                                <td align="left" valign="top" style="width: 50%;">
                                                    <asp:TextBox ID="txtAccountHolderName" runat="server" CssClass="txt_bxEmail" MaxLength="200" TabIndex="61"
                                                        ValidationGroup="1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RFVtxtAccountHolderName" Display="Dynamic" ControlToValidate="txtAccountHolderName" ErrorMessage="Account Holder Name Required" runat="server" ValidationGroup="1" />
                                                    <asp:ValidatorCalloutExtender ID="VCERFVtxtAccountHolderName" runat="server" PopupPosition="BottomLeft"
                                                        TargetControlID="RFVtxtAccountHolderName">
                                                    </asp:ValidatorCalloutExtender>
                                                </td>
                                            </tr>


                                            <tr align="left" valign="top">
                                                <td align="left" valign="top" class="leftHeading"></td>
                                                <td align="left" valign="top" class="leftHeading"></td>
                                                <td align="left" valign="top"></td>
                                            </tr>


                                            <tr align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading"></td>
                                            </tr>

                                            <tr align="right" valign="top">
                                                <td colspan="2"></td>
                                                <td>
                                                    <asp:UpdatePanel ID="updSubmt" runat="server">
                                                        <ContentTemplate>
                                                            <div>
                                                                <div style="float: right;">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                                                        ValidationGroup="1" Width="150px" TabIndex="54"
                                                                        OnClick="btnSubmit_Click" />
                                                                </div>
                                                                <div style="float: right;">
                                                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updSubmt">
                                                                        <ProgressTemplate>
                                                                            <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>








</asp:Content>

