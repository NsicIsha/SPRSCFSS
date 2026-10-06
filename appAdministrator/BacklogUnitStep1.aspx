<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BacklogUnitStep1.aspx.cs" 
Inherits="appAdministrator_BacklogUnitStep1" Title="Backlog Unit Step 1" %>
<%@ MasterType VirtualPath="~/appMaster/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="center">
        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top" style="width: 100%;">
                <td align="left" valign="top" style="width: 100%;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr style="width: 100%;">
                            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">
                                Backlog Unit - Step 1   ``                                                                                                                 
                            </td>
                        </tr>
                        <tr style="width: 95%;">
                            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                    <tr style="width: 100%;">
                                        <td style="width: 100%;">
                                            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                                <tr>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        <b>Step-1</b>
                                                    </td>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        Step-2
                                                    </td>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        Step-3
                                                    </td>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        Step-4
                                                    </td>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        Step-5
                                                    </td>
                                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;">
                                                        Step-6
                                                    </td>
                                                    <td style="width: 10%; padding-left: 10px; padding-right: 10px;">
                                                        Step-7
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="step2tr" runat="server" style="width: 100%;">
                                        <td id="step2" style="width: 100%;">
                                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                                <tr>
                                                    <td id="progressbar" align="left">
                                                    </td>
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
                                        <td colspan="3" align="right" valign="top" class="style4">
                                            STEP 1 of 7
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr id="trbranch2" runat="server" align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                   
                                    <tr id="trbranch3" runat="server" align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                            1
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
                                                                ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;" OnTextChanged="txt_1_TextChanged"
                                                                AutoCompleteType="Disabled"></asp:TextBox>
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
                                                                Visible="false"></asp:Label><br />
                                                                 <asp:Label ID="lbl_branchName_chek" runat="server" CssClass="errMessage" Text=""
                                                                Visible="false"></asp:Label>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                            1.1
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>आवेदक फर्म का नाम/ Name of Applicant firm:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:TextBox ID="txt_1p1" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
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
                                        <td colspan="3" align="left" valign="top" class="rowHeading">
                                            (1.a) मुख्य कार्यालय/ HEAD OFFICE
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" class="style7">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.1)
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.2)
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.3)
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
                                                                OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged" TabIndex="4" ValidationGroup="1">
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.4)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <span class="MandatoryField">*</span>ज़िला/शहर/District/ City:
                                        </td>
                                        <td align="left" valign="top" class="style6">
                                            <asp:DropDownList ID="ddlCity_a4" runat="server" CssClass="clsDDL" TabIndex="5" ValidationGroup="1">
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.5)
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
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_1a5"
                                Display="None" ErrorMessage="Please provide valid phone no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>--%>
                                            <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RegularExpressionValidator1">
                            </asp:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    
                                     <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.5)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            मोबाइल नंबर/ Mobile No:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_mob" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="6"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 9871234567)</span>
                                          
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.6)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            फैक्स/ Fax:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1a6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="7"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000;">(eg: 011-1234567)</span>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_fax" runat="server" FilterMode="ValidChars"
                                                FilterType="Custom" TargetControlID="txt_1a6" ValidChars="1234567890,() .-+">
                                            </asp:FilteredTextBoxExtender>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txt_1a6"
                                Display="None" ErrorMessage="Please provide valid fax no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender25" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RegularExpressionValidator8">
                            </asp:ValidatorCalloutExtender>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.a.7)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <%--<span class="MandatoryField">*</span>--%>ई-मेल/ E-mail:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1a7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="8"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000;">(eg: free123@nsic.co.in)</span>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="None"
                                ErrorMessage="Please Enter HEAD OFFICE E-mail" ControlToValidate="txt_1a7" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" PopupPosition="BottomLeft"
                                TargetControlID="RequiredFieldValidator8">
                            </asp:ValidatorCalloutExtender>--%>
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
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">
                                            (1.b) कारखाना/ FACTORY ADDRESSES
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="leftHeading">
                                            <asp:GridView ID="GridFactory" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridFactory_RowDataBound">
                                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                                <RowStyle CssClass="GrdviewRowOdd" />
                                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Address" ControlStyle-Width="150px" ItemStyle-Width="180px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_1b1" runat="server" ValidationGroup="1" TextMode="MultiLine"
                                                                Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Address") %>'></asp:TextBox>
                                                            <asp:HiddenField ID="hdnunit" runat="server" Value='<%# Bind("fGPtbl_CUnit_Id") %>' />
                                                            <asp:HiddenField ID="hdnstate" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_StateId") %>' />
                                                            <asp:HiddenField ID="hdncity" runat="server" Value='<%# Bind("fGPtbl_CUnit_1B_Factory_CityId") %>' />
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
                                                    <asp:TemplateField HeaderText="Pin" ControlStyle-Width="80px" ItemStyle-Width="100px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt_1b2" runat="server" MaxLength="500" ValidationGroup="1" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Zip") %>'></asp:TextBox>
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
                                                    <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                                                            <div>
                                                                <div style="float: left;">
                                                                    <asp:DropDownList ID="ddlState_b3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlState_b3_SelectedIndexChanged"
                                                                        ValidationGroup="1">
                                                                    </asp:DropDownList>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" Display="None"
                                                                        ErrorMessage="Please Select FACTORY State" ControlToValidate="ddlState_b3" SetFocusOnError="true"
                                                                        ValidationGroup="1"></asp:RequiredFieldValidator>
                                                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" PopupPosition="BottomLeft"
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
                                                    <asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlCity_b4" runat="server" TabIndex="12" ValidationGroup="1">
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
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px;">
                                        </td>
                                    </tr>
                                    <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>--%>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">
                                            (1.c) शाखाएँ/ BRANCHES ADDRESS (optional)
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.1)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            पता/ Address:
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.2)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            पिन कोड/ Pin Code:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c2" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="45"
                                                ValidationGroup="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.3)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            राज्य/ State:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div style="float: left;">
                                                            <asp:DropDownList ID="ddlState_c3" runat="server" CssClass="clsDDL" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlState_c3_SelectedIndexChanged" TabIndex="46">
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
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.4)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            ज़िला/शहर/District/ City:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlCity_c4" runat="server" CssClass="clsDDL" TabIndex="47">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.5)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            फोन/ Telephone:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c5" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="48"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567, 9871234567)</span>
                                            <%-- <div style="float: left;">
                                            <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>--%>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender" runat="server" FilterMode="ValidChars"
                                                FilterType="Custom" ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.6)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            फैक्स/ Fax:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_1c6" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="49"
                                                ValidationGroup="1"></asp:TextBox>
                                            <br />
                                            <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567)</span>
                                            <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender_branchfax" runat="server"
                                                FilterMode="ValidChars" FilterType="Custom" ValidChars="1234567890,() .-+" TargetControlID="txt_1c5">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            (1.c.7)
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            ई-मेल/ E-mail:
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
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="left" valign="top" class="rowHeading">
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                            1.d
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>Category Area:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:DropDownList ID="ddl_1d" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                TabIndex="51" ValidationGroup="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="None"
                                                ErrorMessage="Please Select Category Area" ControlToValidate="ddl_1d" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator15">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                            1.e
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>Category Cast:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                TabIndex="52" ValidationGroup="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="None"
                                                ErrorMessage="Please Select Category Cast" ControlToValidate="ddl_1e" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator16">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" style="width: 5%;" class="leftHeading">
                                            1.f
                                        </td>
                                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                            <span class="MandatoryField">*</span>Category UnitSize:
                                        </td>
                                        <td align="left" valign="top" style="width: 50%;">
                                            <asp:DropDownList ID="ddl_1f" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                TabIndex="53" ValidationGroup="1">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="None"
                                                ErrorMessage="Please Select Category Unit Size" ControlToValidate="ddl_1f" SetFocusOnError="true"
                                                ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" PopupPosition="BottomLeft"
                                                TargetControlID="RequiredFieldValidator17">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                    
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            1.g
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            <span class="MandatoryField">*</span>Enterprise Class :
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
                                    </tr>
                                   
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="leftHeading">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" class="leftHeading">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr align="right" valign="top">
                                        <td colspan="3">
                                            <asp:UpdatePanel ID="updSubmt" runat="server">
                                                <ContentTemplate>
                                                    <div>
                                                        <div style="float: right;">
                                                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                                                ValidationGroup="1" Width="150px" OnClick="btnSubmit_Click" TabIndex="54" />
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
                                            <%--(1.c) शाखाएँ/ BRANCHES--%>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
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
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

