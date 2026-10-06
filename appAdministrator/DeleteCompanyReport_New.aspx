<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="DeleteCompanyReport_New.aspx.cs" Inherits="appAdministrator_DeleteCompanyReport_New" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
        <asp:Label ID="lblDeleteCompany" runat="server" 
            Text="Delete Company List" CssClass="headerAll"></asp:Label>
    </div>
    
     <div>
       <table cellpadding="2" cellspacing="2" border="0">
         <tr>
            <td align="left" > From date:</td>
            <td align="left"><asp:TextBox ID="txt_from_date" runat="server" Text=""></asp:TextBox> 
               <asp:ImageButton ID="imgPopup" ImageUrl="../images/calendar.png" ImageAlign="Bottom"
    runat="server" />
<cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txt_from_date"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
             </td>
            <td align="left"> To date:</td>
            <td align="left"><asp:TextBox ID="txt_todate" runat="server" Text=""></asp:TextBox>

                 <asp:ImageButton ID="imgPopupTo" ImageUrl="../images/calendar.png" ImageAlign="Bottom"
    runat="server" />
<cc1:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopupTo" runat="server" TargetControlID="txt_todate"
    Format="dd/MM/yyyy">
</cc1:CalendarExtender>
               
             </td>
            
         </tr>
           <tr>
                   <td align="left"> Reason </td>
                   <td colspan="3" >
                   <asp:DropDownList ID="ddl_reson" runat="server" CssClass="clsDDL" 
                           onselectedindexchanged="ddl_reson_SelectedIndexChanged" >
                   <asp:ListItem Value="0"   Selected="True">  Selects</asp:ListItem>
                   <asp:ListItem Value="1" >Inspection Failed</asp:ListItem>

                        <asp:ListItem Value="2">Closed</asp:ListItem>
                        <asp:ListItem Value="3"> Pending Documents</asp:ListItem>
                        <asp:ListItem Value="4">Unit Expired</asp:ListItem>
                        <asp:ListItem Value="5">Typo Error</asp:ListItem>
                        <asp:ListItem Value="6">Double Entry</asp:ListItem>
                        <asp:ListItem Value="7">Inspection Pending</asp:ListItem>

                        <asp:ListItem Value="8">Case Rejected</asp:ListItem>
                        <asp:ListItem Value="9">Other</asp:ListItem>




                   </asp:DropDownList>
                     
                   </td>
                </tr>
                <tr>
                  <td colspan="4" >
                  <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btnCls" 
                          onclick="btn_search_Click" />
                      
                      <asp:Label ID="lblCountMsg" runat="server" Font-Bold="true" ForeColor="Green"></asp:Label>
                  </td>
                </tr>
       </table>
     </div>
  <div id="grd_display">
     <asp:GridView ID="grd_delete_company" runat="server" AutoGenerateColumns="false"
        CellPadding="4" CellSpacing="2"  >
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
                                           <asp:TemplateField HeaderText="Reason for Deletion">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_reson_for_delete" runat="server" Text='<%#Eval("ResonforDelete") %>'></asp:Label>
                                               </ItemTemplate>
                                               </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Branch">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_Branch" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="Entry in Software">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_EntryDate" runat="server" Text='<%#Eval("EntryDate") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Deletion Date">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_EntryDate" runat="server" Text='<%#Eval("Deletiondate") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>


                                             <asp:TemplateField HeaderText="NSIC Fees(Including Service Tax)">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_NSICFee" runat="server" Text='<%#Eval("NSICFee") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Agency Amount(Including Service Tax)">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_AmountPaidByAgency" runat="server" Text='<%#Eval("AgencyFee") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount Paid by Unit">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_AmountPaidByUnit" runat="server" Text='<%#Eval("TotalUnitFee") %>'></asp:Label>
                                               </ItemTemplate>  
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Deleted by">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lbl_pending_with" runat="server" Text='<%#Eval("PendingWith") %>'></asp:Label>
                                                 
                                                 </ItemTemplate>
                                            </asp:TemplateField>
                                          
                                            
                                            
                                            </Columns>
        </asp:GridView>
     
</div>
</asp:Content>

