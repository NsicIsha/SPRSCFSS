<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="RejectedCompanies.aspx.cs" Inherits="appAdministrator_RejectedCompanies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Rejected Company List "></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                onrowdatabound="gvCertificateDetails_RowDataBound" 
                onpageindexchanging="CertificateListPageindexChanging" AllowPaging="True" AllowSorting="True" PageSize="10"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_CUnit_RegistrationNo" HeaderText="GP Reg. No." />
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
               
               
                
                  <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
               <ItemTemplate>
                  <asp:LinkButton ID="lnkRenewal" runat="server" CommandName="Renewal" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                   CausesValidation="false" ToolTip="Renewal">Renewal</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HiddenFieldCompanyIdRenewal" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                  </ItemTemplate>
              </asp:TemplateField>
                  
               
               </Columns>
        </asp:GridView>    
        
    </div>
    </div>
</asp:Content>

