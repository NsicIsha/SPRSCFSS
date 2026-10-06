<%@ Page Title="Inspection Complete" Language="C#" MasterPageFile="~/appMaster/Agency.master" AutoEventWireup="true" CodeFile="InspectionComplete.aspx.cs" Inherits="appInspectionAgency_InspectionComplete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
    <div align="left">
        <asp:Label ID="lblRegistration" runat="server" Text="Inspection Complete" CssClass="headerAll" ></asp:Label>
    </div>
    <div align="left" style="height:10px;">
        
    </div>
    
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            
            <asp:GridView ID="gvIComplete" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                AllowPaging="true" onpageindexchanging="gvIComplete_PageIndexChanging"
                EmptyDataText="No Data Found">
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
            <asp:TemplateField HeaderText="S.No.">
            <ItemTemplate><%#Container.DataItemIndex+1 %>

            </ItemTemplate>
            </asp:TemplateField>

            <%--<asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
            <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
            <asp:BoundField DataField="fGPtbl_CUnit_SSIRegistrationNo" HeaderText="SSI Reg No." />
            <asp:BoundField DataField="fGPtbl_IS_DateStatus6" HeaderText="Completion Date" />
            
            </Columns>
            </asp:GridView>
            
        </div>

    <div align="center" style="padding-top:10px;">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
     </div>
     
    
</div>

</asp:Content>

