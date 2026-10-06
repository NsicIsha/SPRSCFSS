<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="AdminEditRegistration.aspx.cs" Inherits="appAdministrator_AdminEditRegistration" Title="NSIC SPR :: Edit Registration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        /* popup_box DIV-Styles*/

        #popup_box {
            display: none; /* Hide the DIV */
            position: fixed;
            _position: absolute; /* hack for internet explorer 6 */
            height: 334px;
            width: 634px;
            background: #FFFFFF;
            left: 300px;
            top: 150px;
            z-index: 100; /* Layering ( on-top of others), if you have lots of layers: I just maximized, you can change it yourself */
            margin-left: 15px; /* additional features, can be omitted */
            border: 2px solid #ff0000;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px #ff0000;
            -webkit-box-shadow: 0 0 5px #ff0000;
            background-color: #ECF8FB;
        }

        #container {
            /*background: #d2d2d2; /*Sample*/
            width: 100%;
            height: 100%;
            cursor: pointer;
        }

        a {
            font-size: 12px;
            cursor: pointer;
            text-decoration: none;
        }
        /* This is for the positioning of the Close Link */
        #popupBoxClose {
            font-size: 20px;
            line-height: 15px;
            right: 5px;
            top: 5px;
            position: absolute;
            color: #6fa5e2;
            font-weight: 500;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div align="center">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top" style="width: 100%;">
                <td align="left" valign="top" style="width: 100%;">



                    <table cellpadding="0" cellspacing="0" border="0" width="100%">

                        <tr style="width: 100%;">
                            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">New Unit Registration - Step 1
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
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" class="style4">STEP 1 of 7
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1</td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <span class="MandatoryField">*</span>फर्म की सरंचना/ Constitution of the firm:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlConstitution" runat="server" CssClass="clsDDL"
                                                CausesValidation="false" ValidationGroup="1" AutoPostBack="true" OnSelectedIndexChanged="ddlConstitution_SelectedIndexChanged">
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11ss" runat="server" Display="Dynamic"
                                                ErrorMessage="Please Constitution of the firm" ControlToValidate="ddlConstitution" InitialValue="0" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.0
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>आवेदक फर्म का नाम/ Name of Applicant firm:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <%--Isha:removed readonly to make comapny name edit possible--%>
                                            <asp:TextBox ID="txt_1p1" runat="server" MaxLength="500" TabIndex="1"
                                                ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                ErrorMessage="Please Enter Name of Applicant firm" ControlToValidate="txt_1p1"
                                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator2">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.1
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txt_1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1"
                                                ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;"
                                                AutoCompleteType="Disabled" Enabled="false"></asp:TextBox>
                                            <div>
                                                <asp:Label ID="lblPANmsg" runat="server" Visible="false" CssClass="errMessage" Text="PAN already exists !"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.2</td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">Declaration of having made the entry in MSME Data Bank</td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:RadioButtonList ID="rdo_declarationMSME" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.3</td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">Under IT Act 2000, section 43(A) do you agree to share your information like contact details, unit and product related information with buying agencies for procurement under public procurement policy of government of India :</td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:RadioButtonList ID="rdoShareInfo" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
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

                                                            <%--   <asp:RegularExpressionValidator ID="RegularExpressionValidatorAAdhar" runat="server" Display="None"
                                                                ErrorMessage="Please Enter Valid aadher Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                                ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="1"></asp:RegularExpressionValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" PopupPosition="BottomRight"
                                                                TargetControlID="RegularExpressionValidatorAAdhar">
                                                            </asp:ValidatorCalloutExtender>--%>
                                                            <%--<asp:RequiredFieldValidator ID="Requ_addhar" runat="server" Display="None"
                                                                ErrorMessage="Please Enter aadhar Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="Validator_aadhar_number" runat="server" PopupPosition="BottomRight"
                                                                TargetControlID="Requ_addhar">
                                                            </asp:ValidatorCalloutExtender>--%>
                                                        </div>
                                                        <asp:Label ID="lbl_adhar_number" runat="server" Visible="false" CssClass="errMessage" Text="Aadhar already exists !"></asp:Label>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.5
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">जीएसटी संख्या / GST Number:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txtGstNumber" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                ValidationGroup="1"></asp:TextBox>

                                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3331" runat="server" ControlToValidate="txtGstNumber"   SetFocusOnError="true" ValidationGroup="1"
                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid GST Number" ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"></asp:RegularExpressionValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1332" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RegularExpressionValidator3331">
                                            </asp:ValidatorCalloutExtender>--%>                                              
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.6
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">क्या यह पंजीकरण अनुसूचित जाति / जनजाति पहल द्वारा किया गया है / Is Registration Done by SC/ST Hub Initiative :
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">

                                            <asp:RadioButtonList ID="rdbSCSTInitiative" runat="server" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                            </asp:RadioButtonList>

                                            <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator3331" runat="server" ControlToValidate="txtGstNumber"   SetFocusOnError="true" ValidationGroup="1"
                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid GST Number" ValidationExpression="^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$"></asp:RegularExpressionValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1332" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RegularExpressionValidator3331">
                                            </asp:ValidatorCalloutExtender>--%>
                                              
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.7</td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>प्रस्ताव की तारीख/ Proposal Date:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txt_proposal_date" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                                                Display="Dynamic" ControlToValidate="txt_proposal_date" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                Display="Dynamic" ControlToValidate="txt_proposal_date" Operator="DataTypeCheck" Type="Date"
                                                ValidationGroup="1"></asp:CompareValidator>
                                            <asp:CalendarExtender ID="CExtendertxt_22" runat="server" PopupButtonID="txt_proposal_date"
                                                TargetControlID="txt_proposal_date">
                                            </asp:CalendarExtender>
                                            <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                                                PopupPosition="TopLeft">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">(1.a) मुख्य कार्यालय/ HEAD OFFICE
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" class="style7">&nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.a.1)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <span class="MandatoryField">*</span>पता/ Address:
                                        </td>
                                        <td align="left" valign="top" style="padding-bottom: 10px;">
                                            <asp:TextBox ID="txt_1a1" runat="server" CssClass="txt_bx" TabIndex="2" ValidationGroup="1"
                                                TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                ErrorMessage="Please Enter HEAD OFFICE Address" ControlToValidate="txt_1a1" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator3">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:CustomValidator ID="CustomValidatorHeadOfficeAddress" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1a1"
                                                ValidationGroup="1"></asp:CustomValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" PopupPosition="BottomLeft"
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
                                            <asp:TextBox ID="txt_1a2" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="3"
                                                ValidationGroup="1"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                                ErrorMessage="Please Enter HEAD OFFICE Pin Code" ControlToValidate="txt_1a2"
                                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator4">
                                            </asp:ValidatorCalloutExtender>
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
                                                                TabIndex="4" ValidationGroup="1"
                                                                OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                                ErrorMessage="Please Select HEAD OFFICE State" ControlToValidate="ddlState_a3"
                                                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
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
                                        <td align="left" valign="top" class="style6">
                                            <asp:DropDownList ID="ddlCity_a4" runat="server" CssClass="clsDDL" TabIndex="5"
                                                ValidationGroup="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                                ErrorMessage="Please Select HEAD OFFICE City" ControlToValidate="ddlCity_a4"
                                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator6">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.a.5)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <span class="MandatoryField">*</span>फोन/ Telephone:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1a5" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="6"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567, 9871234567)</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                                ErrorMessage="Please Enter HEAD OFFICE Telephone" ControlToValidate="txt_1a5"
                                                SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator7">
                                            </asp:ValidatorCalloutExtender>
                                            <asp:FilteredTextBoxExtender ID="filtertexboxextender" runat="server" FilterMode="ValidChars"
                                                FilterType="Custom" TargetControlID="txt_1a5" ValidChars="1234567890,() .-+">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.a.5)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">मोबाइल नंबर/ Mobile No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_mob" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="6"
                                                ValidationGroup="1"></asp:TextBox>
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
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.a.6)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">फैक्स/ Fax:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1a6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="7"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
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
                                        <td align="left" valign="top" class="leftHeading">ई-मेल/ E-mail:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1a7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="8"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000;">(eg: free123@nsic.co.in)</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txt_1a7"
                                                Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
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
                                                                TextMode="MultiLine" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Address") %>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                                                            <asp:HiddenField ID="hdnstate" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_StateId") %>' />
                                                            <asp:HiddenField ID="hdncity" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_CityId") %>' />
                                                            <asp:HiddenField ID="hdnenterprise" runat="server" Value='<%# Bind("fGPtbl_CUnit_Enterprise") %>' />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                                                ErrorMessage="Please Enter FACTORY Address" ControlToValidate="txt_1b1" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" PopupPosition="BottomLeft"
                                                                TargetControlID="RequiredFieldValidator9">
                                                            </asp:ValidatorCalloutExtender>
                                                            <asp:CustomValidator ID="CustomValidatorFactoryAddress" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                                Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1b1"
                                                                ValidationGroup="1"></asp:CustomValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" PopupPosition="BottomLeft"
                                                                TargetControlID="CustomValidatorFactoryAddress">
                                                            </asp:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="150px"></ControlStyle>
                                                        <ItemStyle Width="180px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="*Pin" ControlStyle-Width="80px" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_1b2" runat="server" MaxLength="500"
                                                                ValidationGroup="1" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Zip") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="None"
                                                                ErrorMessage="Please Enter FACTORY Pin Code" ControlToValidate="txt_1b2" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" PopupPosition="BottomLeft"
                                                                TargetControlID="RequiredFieldValidator10">
                                                            </asp:ValidatorCalloutExtender>
                                                        </ItemTemplate>
                                                        <ControlStyle Width="80px"></ControlStyle>
                                                        <ItemStyle Width="100px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="*State">
                                                        <ItemTemplate>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="ddlState_b3" runat="server" OnSelectedIndexChanged="ddlState_b3_SelectedIndexChanged" AutoPostBack="True"
                                                                        ValidationGroup="1">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="None"
                                                                        ErrorMessage="Please Select FACTORY State" ControlToValidate="ddlState_b3" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" PopupPosition="BottomLeft"
                                                                        TargetControlID="RequiredFieldValidator19">
                                                                    </asp:ValidatorCalloutExtender>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="*District/ City">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlCity_b4" runat="server" TabIndex="12"
                                                                ValidationGroup="1">
                                                            </asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="None"
                                                                ErrorMessage="Please Select FACTORY City" ControlToValidate="ddlCity_b4" SetFocusOnError="true"
                                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" PopupPosition="BottomLeft"
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
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txt_1b7"
                                                                Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                                                            </asp:RegularExpressionValidator>
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
                                                    <%--eRA--%>
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
                                                TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                            Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1c1"
                                            ValidationGroup="1"></asp:CustomValidator>
                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" PopupPosition="BottomLeft"
                                            TargetControlID="CustomValidator1">
                                        </asp:ValidatorCalloutExtender>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.c.2)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">पिन कोड/ Pin Code:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c2" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="45"
                                                ValidationGroup="1"></asp:TextBox>                                        </td>
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
                                                                TabIndex="46" OnSelectedIndexChanged="ddlState_c3_SelectedIndexChanged">
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
                                            <asp:DropDownList ID="ddlCity_c4" runat="server" CssClass="clsDDL" TabIndex="47">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.c.5)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">फोन/ Telephone:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c5" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="48"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567, 9871234567)</span>

                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" FilterMode="ValidChars" FilterType="Custom"
                                                ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.c.6)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">फैक्स/ Fax:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="49"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567)</span>

                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_branchfax" runat="server" FilterMode="ValidChars" FilterType="Custom"
                                                ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">(1.c.7)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">ई-मेल/ E-mail:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="50"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000;">(eg: free123@nsic.co.in)</span>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txt_1c7"
                                                Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                                            </asp:RegularExpressionValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender24" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RegularExpressionValidator7">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">&nbsp;</td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading"></td>
                                    </tr>

                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.d
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false" TabIndex="52"
                                                ValidationGroup="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="None"
                                                ErrorMessage="Please Select Enterprise Social Category" ControlToValidate="ddl_1e" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" PopupPosition="BottomLeft"
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="None"
                                                ErrorMessage="Please Select Special Category of Enterpreneur" ControlToValidate="ddl_1d" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator15">
                                            </asp:ValidatorCalloutExtender>
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
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator17">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>

                                    <%--<tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            1.g
                        </td>
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>कार्रवाई की प्रकृति/Nature of Operation :  
                        </td>
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rdo_manufacturlist" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="1">Manufacturing</asp:ListItem>
                                <asp:ListItem Value="2">Service</asp:ListItem>
                                <asp:ListItem Value="3">Both</asp:ListItem>
                            </asp:RadioButtonList>
                                
                                <div id="container">
                                    <a href="http://www.nsicspronline.com/Plant_Machinery.htm" target="_blank"> <b><u>View Limit for Investment in Plant and machinery</u></b></a>
                                </div>
                        </td>
                    </tr>--%>

                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading"></td>
                                        <td align="left" valign="top" class="leftHeading"></td>
                                        <td align="left" valign="top"></td>
                                    </tr>
                                    <tr align="right" valign="top">
                                        <td colspan="3">
                                            <asp:UpdatePanel ID="updSubmt" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                                                ValidationGroup="1" Width="150px" TabIndex="54"
                                                                OnClick="btnSubmit_Click" />
                                                        </div>
                                                        <div style="float: right;">
                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updSubmt">
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
                        <tr>
                            <td align="left">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%; text-align: left;" class="MandatoryMessage">
                                <span class="MandatoryField">*</span> Mandatory Fields
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdnCompany" runat="server" />
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="left">&nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">&nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

