<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminNew.master" AutoEventWireup="true" CodeFile="FeesAwaited.aspx.cs" Inherits="appAdministrator_FeesAwaited" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <br />
    <div align="left">
        <span class="headerAll">Documents are accepted but Fees is awaitied</span>
    </div>
 <br />
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvRegistrationDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1">
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSno" Text='<%# Container.DataItemIndex + 1 %>'  runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />               
                <asp:TemplateField HeaderText="Address">
                    <ItemTemplate>
                        <%# Eval("fGPtbl_Company_HeadOffice_Address") + " " + Eval("fGPtbl_MCty_Name") + " " + Eval("fGPtbl_MS_Name") + " " + Eval("fGPtbl_Company_HeadOffice_Zip")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Mobile" HeaderText="Mobile" />
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Phone" HeaderText="Landline" />
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Email" HeaderText="Email" />

            </Columns>
        </asp:GridView>

    </div>
</asp:Content>

