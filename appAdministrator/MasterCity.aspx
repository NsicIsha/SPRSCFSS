<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true"
    CodeFile="MasterCity.aspx.cs" Inherits="appAdministrator_MasterCity" Title="NSIC SPR ::City Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">
        function FunResetCity()
        {
            document.getElementById("ctl00_ContentPlaceHolder1_txtStateEdit").value="";
            document.getElementById("ctl00_ContentPlaceHolder1_txtDescEdit").value="";
        }
    </script>
   <%-- fasf--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="left" class="headerAll">
                        City Master
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" height="400" valign="top" style="padding-left: 10px; padding-right: 10px;">
                        <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                            <asp:View ID="viewMessage" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td align="center" valign="top" style="width: 100%;">
                                            <asp:Label ID="lblMsg" runat="server" CssClass="reportCenterText"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                            <asp:View ID="viewData" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="left" valign="top">
                                        <td align="center" valign="top" style="width: 100%;">
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="clsDDL" 
                                                AutoPostBack="true" onselectedindexchanged="ddlState_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="right" valign="top">
                                            
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color: #22518F;">
                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td align="right" valign="top" style="width: 100%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%">
                                                    <td class="Action_TD_1">
                                                        <%if (mstAction == 1)
                                                          {%>
                                                        <div id="divAction" runat="server" class="Action_Div" style="background-color: #ECF8FB;">
                                                            <div id="Action_DivInner_1">
                                                                <asp:Label ID="lblAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                            </div>
                                                            <div id="Action_DivInner_2">
                                                                <img id="imgAction" onclick="FunActionClose('ctl00_ContentPlaceHolder1_divAction')"
                                                                    src="../Images/Admin/delete_icon.gif" border="0" width="16px" height="15px" alt="CLOSE"
                                                                    style="cursor: pointer;" />
                                                            </div>
                                                        </div>
                                                        <asp:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="divAction"
                                                            BorderColor="#22518F" Radius="10" Corners="All">
                                                        </asp:RoundedCornersExtender>
                                                        <%} %>
                                                    </td>
                                                    <td class="Action_TD_2">
                                                        <asp:Button ID="btnAdd" runat="server" CausesValidation="false" Text="Add" CssClass="btnCls"
                                                            OnClick="btnAdd_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td align="left" valign="top" style="width: 100%;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                                <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td align="left" valign="top" style="width: 100%;">
                                            <asp:GridView ID="GridView1" runat="server" Width="100%" BorderWidth="0" CellPadding="3"
                                                CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false" DataKeyNames="fGPtbl_MCty_Id"
                                                AllowSorting="false" AllowPaging="true" PageSize="25" OnRowDataBound="GridView1_RowDataBound"
                                                OnRowCommand="GridView1_RowCommand" 
                                                onpageindexchanging="GridView1_PageIndexChanging">
                                                <HeaderStyle CssClass="GrdviewHeader" />
                                                <RowStyle CssClass="GrdviewRowOdd" />
                                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            <asp:HiddenField ID="hdnActiveFlag" runat="server" Value='<%# Eval("fGPtbl_MCty_AccessId") %>' />
                                                            <asp:HiddenField ID="hdnStateActiveFlag" runat="server" Value='<%#Eval("fGPtbl_MS_AccessId") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="fGPtbl_MCty_Name"
                                                        HeaderText="City" SortExpression="City" />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="fGPtbl_MCty_Desc"
                                                        HeaderText="Description" SortExpression="Description" />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="SateName"
                                                        HeaderText="State" SortExpression="State" />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="fGPtbl_MU_Name"
                                                        HeaderText="Last edit user" SortExpression="Last edit user" visible="false"/>
                                                    <asp:TemplateField HeaderText="Active/ Deactive">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActiveDeactive" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" border="0"
                                                                CommandArgument='<%#Eval("fGPtbl_MCty_Id") %>' CommandName="CityEdit" AlternateText="Edit City"
                                                                ToolTip="Edit City" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Activate/ Deactivate">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnActivateDe" runat="server" border="0" CommandArgument='<%#Eval("fGPtbl_MCty_Id") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </asp:View>
                        </asp:MultiView>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="padding-left: 10px;">
                        <table border="0" cellpadding="0" cellspacing="1" width="350px">
                            <tr valign="top" align="left">
                                <td colspan="2" class="clsLegends">
                                    LEGENDS:-
                                </td>
                            </tr>
                            <tr valign="top" align="left">
                                <td class="clsLegends_Left_Deactivate">
                                </td>
                                <td class="clsLegends_Right">
                                    Shows deactivated cities.
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
            </table>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <asp:Panel ID="pnlPopup_PlzWait" runat="server" CssClass="modalPopup_plzwait">
                        <img src="../Images/Admin/loading.gif" border="0" width="43px" height="11px" alt="Loading......" />
                    </asp:Panel>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground_plzwait"
                DropShadow="false" TargetControlID="UpdateProgress1" PopupControlID="UpdateProgress1">
            </asp:ModalPopupExtender>
            
            
            
            
            
            
            
            
            
            <asp:Panel ID="pnlOuterEdit" runat="server" Style="display: none;" CssClass="outerPopup">
        <asp:Panel ID="pnlInnerEdit" runat="server" CssClass="innerPopup" Width="400">
            <table cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                <tr align="center" valign="top" style="width: 100%;">
                    <td align="center" valign="top" style="width: 100%; padding-left: 5px; padding-right: 5px;
                        padding-top: 0px; padding-bottom: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                            <tr align="left" valign="top">
                                <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top">
                                            <td align="center" valign="top" class="updateInnerHeader">
                                                <asp:Label ID="lblMesgPnl" runat="server" CssClass="header"></asp:Label>
                                            </td>
                                            <td align="right" valign="top" style="width: 20px;">
                                                <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="imgCloseEdit" runat="server"
                                                    ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD" style="width: 100px;">
                                    State:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:Label ID="lblStateEdit" runat="server"></asp:Label>
                                    <asp:DropDownList ID="ddlStateEdit" runat="server" CssClass="clsDDL" AutoPostBack="false" ValidationGroup="12"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                        Display="None" ValidationGroup="12" ErrorMessage="Please select State" ControlToValidate="ddlStateEdit"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD" style="width: 100px;">
                                    City:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:TextBox ID="txtCityEdit" runat="server" CssClass="txt_bx" MaxLength="200" ValidationGroup="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                        Display="None" ValidationGroup="12" ErrorMessage="Please provide City" ControlToValidate="txtCityEdit"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator4">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD">
                                    Description:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:TextBox ID="txtDescEdit" runat="server" CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" SetFocusOnError="true"
                                        Display="None" ValidationGroup="12" ErrorMessage="Description must be upto 2000 characters"
                                        ControlToValidate="txtDescEdit" ClientValidationFunction="CharacterLimit2000"></asp:CustomValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="CustomValidator2">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" style="font-weight: bold;">
                                </td>
                                <td align="left" class="modalTDbutton">
                                    <asp:Button ID="btnSubmitEdit" runat="server" Text="Submit" CssClass="btnCls" ValidationGroup="12"
                                        OnClick="btnSubmitEdit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="btnResetEdit" type="button" value="Reset" class="btnCls" onclick="FunResetState()" />
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground"
        DropShadow="false" TargetControlID="lnkModalClick" PopupControlID="pnlOuterEdit"
        CancelControlID="imgCloseEdit">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" Radius="10"
        Corners="All" BorderColor="#000000" TargetControlID="pnlInnerEdit">
    </asp:RoundedCornersExtender>
    <asp:LinkButton ID="lnkModalClick" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
    <%--<asp:HiddenField ID="hdnCityId" runat="server" />--%>
    <asp:HiddenField ID="hdnStateId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
