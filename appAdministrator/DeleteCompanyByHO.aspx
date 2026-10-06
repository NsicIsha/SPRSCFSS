<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="DeleteCompanyByHO.aspx.cs" Inherits="appAdministrator_DeleteCompanyByHO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblDeleteCompany" runat="server"
            Text="Delete Company List" CssClass="headerAll"></asp:Label>
    </div>

    <div></div>
    <br />
    <div align="left">
        <asp:Label ID="Label1" runat="server"
            Text="Enter PAN of the unit to be deleted:"></asp:Label>
    </div>
    <br />
    <asp:TextBox ID="txtSearchbox" runat="server"></asp:TextBox>
    <asp:Button ID="btnSearch" runat="server" Text="--Search--" OnClick="btnSearch_Click" />
    <br />
    <div id="grd_display">

          <asp:HiddenField ID="HDCompanyid" runat="server" />
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grd_delete_company" runat="server" AutoGenerateColumns="False"
                Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="1"
                AllowPaging="True" PageIndex="0"
                AllowSorting="True">
                <%--OnRowCommand="gv_company_RowCommand"
                OnSelectedIndexChanged="gv_company_SelectedIndexChanged"
                OnSelectedIndexChanging="gv_company_SelectedIndexChanging"
                OnPageIndexChanging="gv_company_PageIndexChanging"
                OnRowDataBound="gv_company_RowDataBound1">--%>
                <EmptyDataTemplate>
                    <div align="center"><strong style="font-size: 18px; font-weight: bold; color: Red;">Record not found!!!</strong></div>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="fGPtbl_Company_Id" HeaderText="Company ID" />
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />
                    <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                    <asp:BoundField DataField="Registartionvalidity" HeaderText="Validity " />
                    <asp:BoundField DataField="fGtbl_Company_AdharNumber" HeaderText="UDYAM" />
                    <asp:BoundField DataField="Branch" HeaderText="Branch" />
                    <asp:TemplateField HeaderText="Delete company"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_companyname" runat="server" CommandName="editCompany" OnCommand="lbl_companyname_Command"
                                CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Editcompany Name">Delete                

                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>
        </div>
        <%--<asp:GridView ID="grd_delete_company" runat="server" AutoGenerateColumns="false"
            CellPadding="4" CellSpacing="2" OnRowCommand="onrowcommand">
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <EmptyDataTemplate>
                <asp:Label ID="lblNorecId" runat="server" Text=" No record found. "
                    Font-Bold="true" ForeColor="Brown" Font-Names="Arial" Font-Size="Medium"></asp:Label>
            </EmptyDataTemplate>



            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Name">
                    <ItemTemplate>
                        <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PAN No.">
                    <ItemTemplate>
                        <asp:Label ID="lbl_company_Pan" runat="server" Text='<%#Eval("CompanyPan") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Company_address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                    </ItemTemplate>

                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reason for Deletion">
                    <ItemTemplate>
                        <asp:Label ID="lbl_reson_for_delete" runat="server" Text='<%#Eval("ResonforDelete") %>'></asp:Label>
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
                <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnReject" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                            AlternateText="Reject" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CommandName="reject" ToolTip="Reject company" />
                    </ItemTemplate>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>


            </Columns>
        </asp:GridView>--%>
    </div>

    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width: 100%">
                                    <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top" style="width: 100%;">
                                                <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                                <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="right" valign="top">&nbsp;
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left" style="width: 100px;" class="updateInnerColumn">Reason for Deletion:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txt_Reason" runat="server" TextMode="MultiLine" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="req_validitydate" runat="server" ControlToValidate="txt_Reason"
                                            SetFocusOnError="true" ErrorMessage="Please fill in the reason for deletion" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>

                                        <cc1:ValidatorCalloutExtender ID="Vc_registarion" TargetControlID="req_validitydate"
                                            runat="server">
                                        </cc1:ValidatorCalloutExtender>

                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td align="left" valign="top" style="height: 10px;"></td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="--Delete--" TabIndex="3" ValidationGroup="1" CssClass="btnCls"
                                            OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp;&nbsp;
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
     <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>
</asp:Content>

