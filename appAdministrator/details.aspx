<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="details.aspx.cs" Inherits="appAdministrator_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top">
                <td align="center" valign="top" style="width: 100%;">

                    <asp:Label ID="lblTitle" runat="server" Font-Bold="true" Font-Size="Large"></asp:Label>
                    <br />
                    <br />
                    <asp:GridView ID="detailsGrid" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>                                    
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Company_Name" HeaderText="Compan Name"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>

                             <asp:BoundField DataField="PAN" HeaderText="PAN"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Social_Category" HeaderText="Category"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NSIC_fee" HeaderText="NSIC Fee"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="NSIC_fee" HeaderText="Service Tax"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Servie_Tax" HeaderText="Service Tax"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Unit_Type" HeaderText="Unit Type"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Agency_Name" HeaderText="Agency Name"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Agency_Fee" HeaderText="Agency Fee"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Agency_Service_Tax" HeaderText="Service Tax"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Application_Type" HeaderText="Application Type"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Month" HeaderText="Month"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                             <asp:BoundField DataField="Branch" HeaderText="Branch"
                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                 Visible="true">
                                <HeaderStyle Wrap="False" />
                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>

                </td>

            </tr>
        </table>

    </div>
</asp:Content>

