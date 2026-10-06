<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master"   EnableEventValidation="false"  AutoEventWireup="true" CodeFile="ApproveValidityDate.aspx.cs" Inherits="appAdministrator_ApproveValidityDate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

  <asp:HiddenField runat ="server" ID ="hid_comapnyId" />
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        </div>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gv_company" runat="server" AutoGenerateColumns="False"
            Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="1" 
               AllowPaging="True" 
                AllowSorting="True" onrowcommand="gv_company_RowCommand" 
                onselectedindexchanging="gv_company_SelectedIndexChanging" 
                onrowdatabound="gv_company_RowDataBound"> 
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
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                <asp:BoundField DataField="fGPtbl_Request_validity_date" 
                    HeaderText="Validity date " />
                     <asp:BoundField DataField="fGPtbl_MB_Name" 
                    HeaderText="Branch " />
                    <asp:TemplateField  HeaderText ="View">
                       <ItemTemplate>
                         <asp:LinkButton runat ="server" id ="lnk_view" 
                         CommandArgument='<%# Eval("fGPtbl_Company_Id") + ";" + Eval("fGPtbl_Company_Name") + ";" + Eval("fGPtbl_MB_Name") %>'
             CommandName ="view" OnClientClick="aspnetForm.target ='_blank';" >View</asp:LinkButton>
                       </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField HeaderText="Approve Validity" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="lbnDraftPrint" runat="server" CommandName="approve"
                         CssClass ="btnCls"   CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="true" Text = "Approve ">  
                        </asp:Button>
                         <%--OnClientClick="return confirm('Do you want to Approve')"--%>
                    </ItemTemplate>
                    </asp:TemplateField>
               </Columns>
        </asp:GridView>    
    </div>
    </div>
</asp:Content>

