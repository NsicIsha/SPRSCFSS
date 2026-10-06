<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyDeleteByBranchHead.aspx.cs" Inherits="appAdministrator_CompanyDeleteByBranchHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll">Deleteed  Companies By Branch</asp:Label>
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
            Width="100%"
               AllowPaging="True"
                AllowSorting="True"  DataKeyNames ="fGPtbl_Company_Delete_Id"
                onpageindexchanging="gv_company_PageIndexChanging" onrowcommand="gv_company_RowCommand" 
               >      <%--onrowcommand="gv_company_RowCommand"--%>
                    <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> Record not found!!!</strong></div>
                           </EmptyDataTemplate>
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" CssClass="Pagging" />
           <Columns>
           
            <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ComapnyName" HeaderText="Company Name" />
                <asp:BoundField DataField="PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="Reson" HeaderText="Address" />
                
                
                 <asp:TemplateField HeaderText="Options" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                    <asp:LinkButton ID="lblchangepan" runat="server" CommandName="edp"
                            CommandArgument='<%#Eval("fGPtbl_Company_Delete_Id") %>' CausesValidation="false" ToolTip="Forward to Zonal head">Forward
                        </asp:LinkButton>
                           
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                
</asp:TemplateField>
               
               </Columns>
                
                
                </asp:GridView>
             
          
          </div>
          
          
            
        
        </div>
        


</asp:Content>

