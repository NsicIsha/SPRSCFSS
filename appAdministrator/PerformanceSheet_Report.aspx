<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PerformanceSheet_Report.aspx.cs" Inherits="appAdministrator_PerformanceSheet_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="center" valign="top" style="width: 100%; height: 10px;">
                        <table cellpadding="2" cellspacing="0" border="1">
                            <tr>
                                <td align="left">From</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_from_date" Enabled="false" runat="server" placeholder="MM/dd/yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_from_date"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_from_date" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
                                </td>
                                <td align="left">To</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_todate" runat="server" Enabled="false" placeholder="MM/dd/yyyy"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="rfd1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />
                                </td>
                                <%--<td>
                                    <asp:RadioButtonList ID="rdbStatus"  runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdbDuration_SelectedIndexChanged">
                     <asp:ListItem Text="Live" Value="1" ></asp:ListItem>
                     <asp:ListItem Text="Expired" Value="2"></asp:ListItem>
                                        <asp:ListItem  Selected="True" Text="Both" Value="3"></asp:ListItem>
                      </asp:RadioButtonList> </td>--%>
                                <td align="left">
                                    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <img alt="progress" src="../Images/Admin/loading.gif" />
                                            Processing...            
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:Button ID="btn_status_track" runat="server" ValidationGroup="withdate"
                                        Text="View Details" Width="120" CssClass="btnCls" OnClientClick="javascript:return validate();"
                                        OnClick="btn_status_track_Click" />
                                    <asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="left" valign="top" style="padding-left: 5px; padding-right: 5px;">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 20px">

                            <tr align="center">

                                <td colspan="5" class="leftHeadingReport" align="center" style="font-size: 20px; font-family: Arial Baltic; font-weight: bold; padding-top: 25px; padding-bottom: 15px">Status from:  <b>
                                    <asp:Label ID="lblCurrentStatus" Font-Size="20px" ForeColor="Green" runat="server"></asp:Label></b>
                                </td>
                            </tr>
                            <tr align="center">
                                <%--OnRowDataBound="gvEmp_RowDataBound"--%>
                                <td colspan="5" class="leftHeadingReport" align="center" style="font-size: 20px; font-family: Arial Baltic; font-weight: bold; padding-top: 25px; padding-bottom: 15px">
                                    <asp:GridView ID="grd_search_result" runat="server" CellPadding="3" OnRowDataBound="gvEmp_RowDataBound"
                                        CellSpacing="3" GridLines="Both" ShowFooter="true"
                                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="5000"
                                        Width="100%" BorderWidth="0" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <EmptyDataTemplate>
                                            <div align="center"><strong style="font-size: 18px; font-weight: bold; color: Red;">No  Data  Found !!!</strong></div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.NO">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAN">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("fGPtbl_Company_PAN")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_registration_fGPtbl_MB_Name" runat="server" Text='<%#Eval("fGPtbl_MB_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_registration_fGPtbl_MZ_Name" runat="server" Text='<%#Eval("fGPtbl_MZ_Name")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberAP" runat="server" Text='<%#Eval("fGPtbl_Company_HeadOffice_Mobile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberSC" runat="server" Text='<%#Eval("fGPtbl_Company_HeadOffice_Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Number/Authority">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberE" runat="server" Text='<%#Eval("OrderNumber_Date_Authority") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Receipt Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberM" runat="server" Text='<%#Eval("DateofReceipt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberState" runat="server" Text='<%#Eval("Description_Stores_Product_Services") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalQty" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numberDate" runat="server" Text='<%#Eval("Value") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalValue" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Consignee Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_Name_Consignee" runat="server" Text='<%#Eval("Name_Consignee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Due Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_DueDate" runat="server" Text='<%#Eval("DueDate_Delivery") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Delivery Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_Material_DeliveryDate" runat="server" Text='<%#Eval("Material_DeliveryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Desptching Particulars">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_DesptchingParticulars" runat="server" Text='<%#Eval("DesptchingParticulars") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_Remarks" runat="server" Text='<%#Eval("Remarks") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton CausesValidation="false" ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                    CommandName="ActionPOI" CommandArgument='<%#Eval("fGPtbl_POI_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

