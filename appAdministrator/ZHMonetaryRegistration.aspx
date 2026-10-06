<%@ Page Title="Monetary Limit Registration" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true"
    CodeFile="ZHMonetaryRegistration.aspx.cs" Inherits="appAdministrator_ZHMonetaryRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblMonetary" runat="server"
            Text="" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvMonetary" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewMonetaryNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="center" valign="top" style="width: 100%;">
                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewMonetaryDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color: #22518F;">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="right" valign="top" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top" style="width: 100%">
                                    <td class="Action_TD_1" style="text-align: center;">
                                        <%if (monetzonalAction == 1)
                                                                         { %><div id="divAction" runat="server" class="Action_Div" style="background-color: #ECF8FB; height: 20px;">
                                            <div id="Action_DivInner_1" align="center">
                                                <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                            </div>
                                            <div id="Action_DivInner_2" align="center">
                                                <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor: pointer;" />
                                            </div>
                                        </div>
                                        <cc1:RoundedCornersExtender
                                            ID="RoundedCornersExtender2" runat="server"
                                            TargetControlID="divAction"
                                            BorderColor="#22518F"
                                            Radius="10"
                                            Corners="All">
                                        </cc1:RoundedCornersExtender>
                                        <%} %></td>
                                    <td valign="top" align="right" style="width: 30%;"></td>
                                </tr>
                            </table>

                        </td>
                    </tr>
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%;"></td>
                    </tr>
                </table>
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        <tr align="left" valign="top" style="width: 100%;">
                            <td align="left" valign="top" style="width: 100%;">
                                <asp:HiddenField ID="HiddenFieldUnitid" runat="server" />
                                <asp:HiddenField ID="HFieldformtypeid" runat="server" />
                                <asp:HiddenField ID="HFMonetaryid" runat="server" />
                                <asp:HiddenField ID="HFMonetarylimit" runat="server" />
                                <asp:HiddenField ID="HDCOmpanyid" runat="server" />
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                    <ProgressTemplate>
                                        <asp:Panel ID="pnlMsg" runat="server">
                                            <table align="center" style="border: 2; background-repeat: no-repeat; background-position: center; position: absolute; left: 200px; top: 200px; height: 100px; width: 200px; z-index: 10000; background-color: #a9dceb;">
                                                <tr>
                                                    <td valign="top" align="center" style="color: Black; font-size: 12px; font-weight: bold;">Processing .........
                                                        <br />
                                                        <br />
                                                        Please wait.....
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMonetaryDetails" runat="server" AutoGenerateColumns="false"
                                            Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="1"
                                            OnRowCommand="gvMonetaryDetails_RowCommand"
                                            OnRowDataBound="gvMonetaryDetails_RowDataBound">
                                            <HeaderStyle CssClass="dynRowHeader" />
                                            <RowStyle CssClass="dynRowEven" />
                                            <AlternatingRowStyle CssClass="dynRowOdd" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No.">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                                                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />

                                                  <asp:BoundField DataField="fGPtbl_PD_Date" HeaderText="Fee Date" />

                                                <asp:BoundField DataField="fGPtbl_MB_Name" HeaderText="Branch Name" />
                                                
                                                <asp:TemplateField HeaderText="View<br>Details">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdncid" runat="server" Value='<%#Eval("fGPtbl_Company_Id")%>' />
                                                        <asp:HyperLink ID="lnkview" runat="server" Target="_blank" Text="View Form" NavigateUrl=""></asp:HyperLink>
                                                        <br />
                                                        <asp:HyperLink ID="hypviewdocumet" runat="server" Text="" Target="_blank"></asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Accept" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbButton" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                            AlternateText="Accept" CommandName="edp" ToolTip="Accept company" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Edit Monetary Limit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imbButton_edit" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                            AlternateText="Edit Monetary Limit" CommandName="editml " ToolTip="Edit Monetary Limit" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReject" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                            AlternateText="Reject" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CommandName="reject" ToolTip="Reject company" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="gvMonetaryDetails" EventName="Rowcommand" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left"></td>
                        </tr>

                        <tr>
                            <td align="left"></td>
                        </tr>
                    </table>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <div>
        <div style="text-align: justify; padding-left: 25px; font-weight: bold;">
            Monetary Limit calculation
        </div>
        <ul type="disc" style="margin-bottom: 0in">
            <li style="color: #333333; text-align: justify; line-height: normal">In case the Company/Partnership concern/Proprietorship unit is 
                    in loss for one year out of past three years, their monetary 
                    limit will be fixed at 40% of their average net sales turnover.<br />
            </li>
            <li style="color: #333333; text-align: justify; line-height: normal; padding-top: 5px;">Similarly, when the Micro &amp; Small Enterprise is in loss for two 
                    years out of the past three years, the monetary limit will be 
                    accordingly fixed at 30% of their average net sales turnover of 
                    the past three years.</li>
            <li style="color: #333333; text-align: justify; line-height: normal; padding-top: 5px;">In the event of Micro &amp; Small Enterprise being in loss 
                    throughout past three years, the monetary limit of the Unit will 
                    be fixed at 20% of the average net sales turnover of the Unit 
                    during the past three years In cases of initial registration 
                    where the firm has been in existence for less than 3 years MTL 
                    may be fixed at 50% of the average net sales value of the last 
                    year for which audited accounts are available.</li>
        </ul>
    </div>
    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="800px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width: 100%">
                                    <td valign="top" align="right" colspan="2" style="padding-bottom: 10px; padding-right: 5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="center" valign="top" class="updateInnerHeader">
                                                    <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top"></td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top">
                                        <asp:GridView ID="gv_display" Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="0" runat="server">
                                            <HeaderStyle CssClass="dynRowHeader" />
                                            <RowStyle CssClass="dynRowEven" />
                                            <AlternatingRowStyle CssClass="dynRowOdd" />
                                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center"
                                                CssClass="Pagging" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top"></td>
                                </tr>

                                <%--<tr valign="top" align="left" >
                               
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="right" valign="top" style="width:150px;">
                                                Monetary Limit:
                                            </td>
                                            <td align="left" valign="top" style="width:200px;">
                                                
                                                <asp:Repeater ID="MonetaryRepeater" runat="server" 
                                                    onitemdatabound="MonetaryRepeater_ItemDataBound" >
                                                <HeaderTemplate>
                                                    <table border="1" cellpadding="0" cellspacing="0" width="100%">        
                                                        <tr style="width:90%;">
                                                            <th style="width:12%;" align="center">
                                                                Location
                                                            </th>
                                                            <th>
                                                                Monetary Limit
                                                            </th>
                                                        </tr>    
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlFactory" Text='<%# Eval("fGPtbl_CUnit_1B_Factory_Address") %>' ></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlMonetary" Text='<%# Eval("fGPtbl_CUnit_ML_MonetoryLimit") %>' ></asp:Literal>
                                                    </td>
                                                </tr>
                                                 
                                                </ItemTemplate>
                
                                                <FooterTemplate>
                                                <tr>
                                                    <td>
                                                        Consolidated:  
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlTotalMonetary" ></asp:Literal>
                                                    </td>
                                                </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:5px;" colspan="2"></td>
                                        </tr>
                                        <%--<tr id="trproposal" runat="server">
                                            <td align="right" valign="top">
                                                Approved the proposal on:<br />MM/DD/YYYY 
                                            </td>
                                            <td align="left" valign="top" style="padding-left:5px;" >
                                                <asp:TextBox ID="txtApproved" runat="server" ValidationGroup="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFValidatortxtApproved" runat="server" ErrorMessage="Date is required"
                                                    ControlToValidate="txtApproved" Display="Dynamic" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CValidatortxtApproved" runat="server" ErrorMessage="Enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" Type="Date" Operator="DataTypeCheck" SetFocusOnError="true" ValidationGroup="1" ControlToValidate="txtApproved"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CExtendertxtApproved" runat="server" TargetControlID="txtApproved">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:2px;" colspan="2"></td>
                                        </tr>
                                        <%--<tr id="trvalidity" runat="server">
                                            <td align="right" valign="top">
                                                Start date of validity:<br />MM/DD/YYYY
                                            </td>
                                            <td align="left" valign="top" style="padding-left:5px;">
                                                <asp:TextBox ID="txtValidity" runat="server" ValidationGroup="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RFValidatortxtValidity" runat="server" ErrorMessage="Date is required"
                                                    ControlToValidate="txtValidity" Display="Dynamic" SetFocusOnError="true" ValidationGroup="1"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CValidatortxtValidity" runat="server" ErrorMessage="Enter date in MM/DD/YYYY format"
                                                    Display="Dynamic" Type="Date" Operator="DataTypeCheck" SetFocusOnError="true" ValidationGroup="1" ControlToValidate="txtValidity"></asp:CompareValidator>
                                                <cc1:CalendarExtender ID="CExtendertxtValidity" runat="server" TargetControlID="txtValidity">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                            
                                    
                                </td>
                              </tr>--%>

                                <tr valign="top" align="center">
                                    <td valign="top" align="center" colspan="2" class="updateInnerColumn">
                                        <asp:UpdatePanel ID="Update_bton" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                                    ValidationGroup="1" CssClass="btnCls" OnClick="btnSubmit_Click" />


                                                 <asp:Label ID="lblErrorMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="center" colspan="2">
                                        <asp:UpdateProgress ID="updProgress"
                                            AssociatedUpdatePanelID="Update_bton"
                                            runat="server">
                                            <ProgressTemplate>
                                                <img alt="progress" src="../Images/Admin/loading.gif" />
                                                Processing...            
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:Panel ID="Panelreason" runat="server" CssClass="outerPopup">
        <asp:Panel ID="Panelreason2" runat="server" Width="800px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width: 100%">
                                    <td valign="top" align="right" colspan="2" style="padding-bottom: 10px; padding-right: 5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="center" valign="top" class="updateInnerHeader">
                                                    <asp:Label ID="lblheader" runat="server" CssClass="header"></asp:Label>

                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImbtClose" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top"></td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top">
                                        <asp:GridView ID="gv_displayreject" Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="0" runat="server">
                                            <HeaderStyle CssClass="dynRowHeader" />
                                            <RowStyle CssClass="dynRowEven" />
                                            <AlternatingRowStyle CssClass="dynRowOdd" />
                                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        </asp:GridView>
                                    </td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top"></td>
                                </tr>

                                <%--<tr align="left" valign="top" style="width:100%;">
                                <td colspan="2" align="right" valign="top">
                                   <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="right" valign="top" style="width:150px;">
                                                Monetary Limit:
                                            </td>
                                            <td align="left" valign="top" style="width:200px;">
                                                
                                                <asp:Repeater ID="RepeaterReject" runat="server" 
                                                    onitemdatabound="MonetaryRepeater_ItemDataBound" >
                                                <HeaderTemplate>
                                                    <table border="1" cellpadding="0" cellspacing="0" width="100%">        
                                                        <tr style="width:90%;">
                                                            <th style="width:12%;" align="center">
                                                                Location
                                                            </th>
                                                            <th>
                                                                Monetary Limit
                                                            </th>
                                                        </tr>    
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlFactory" Text='<%# Eval("fGPtbl_CUnit_1B_Factory_Address") %>' ></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlMonetary" Text='<%# Eval("fGPtbl_CUnit_ML_MonetoryLimit") %>' ></asp:Literal>
                                                    </td>
                                                </tr>
                                                 
                                                </ItemTemplate>
                
                                                <FooterTemplate>
                                                <tr>
                                                    <td>
                                                        Consolidated:  
                                                    </td>
                                                    <td>
                                                        <asp:Literal runat="server" ID="ltlTotalMonetary" ></asp:Literal>
                                                    </td>
                                                </tr>
                                                    </table>
                                                </FooterTemplate>
                                            </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height:5px;" colspan="2"></td>
                                        </tr>
                                        
                                    </table>
                                </td>
                            </tr>--%>

                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">Reason to Reject :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtRejectreason" runat="server" MaxLength="5000"
                                            CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RFValidatortxtRejectreason" runat="server" ErrorMessage="Please provide Reason to reject"
                                            SetFocusOnError="true" ControlToValidate="txtRejectreason" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidatortxtRejectreason" runat="server" ErrorMessage="Reason should be less than 5000 characters"
                                            Display="Dynamic" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txtRejectreason"
                                            ValidationGroup="2"></asp:CustomValidator>
                                        <cc1:ValidatorCalloutExtender ID="VCtxtRejectreason" runat="server" TargetControlID="RFValidatortxtRejectreason">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td align="left" valign="top"></td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="btnRejectReason" runat="server" Text="Submit"
                                            ValidationGroup="2" CssClass="btnCls" OnClick="btnRejectReason_Click" />

                                        <input id="Button2" type="button" value="Reset" class="btnCls" onclick="return FunResetcertificate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </center>
        </asp:Panel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="MPExtenderRejectreason" runat="server" PopupControlID="Panelreason"
        TargetControlID="LinkButton2" CancelControlID="ImbtClose" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RCExtender" TargetControlID="Panelreason2"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none;"></asp:LinkButton>
</asp:Content>

