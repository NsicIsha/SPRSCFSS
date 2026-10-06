<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="DeleteCompanybyBranchHead.aspx.cs" Inherits="appAdministrator_DeleteCompanybyBranchHead" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="left">
        <asp:Label ID="lblDeleteCompany" runat="server" 
            Text="Delete Company List" CssClass="headerAll"></asp:Label>
    </div>
    
     <div></div>
  <div id="grd_display">
     <asp:GridView ID="grd_delete_company" runat="server" AutoGenerateColumns="false"
        CellPadding="4" CellSpacing="2" onrowcommand="onrowcommand" >
                               <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <EmptyDataTemplate  >
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
                                            <asp:TemplateField HeaderText ="PAN No.">
                                              <ItemTemplate>
                                                <asp:Label ID="lbl_company_Pan" runat="server" Text='<%#Eval("CompanyPan") %>'></asp:Label>
                                              </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField  HeaderText="Address">
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
        </asp:GridView>
     
     
     
  </div>   
</asp:Content>

