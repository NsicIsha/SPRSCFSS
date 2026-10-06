<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PendingReport.aspx.cs" Inherits="appAdministrator_PendingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Pendency  Report" CssClass="headerAll"></asp:Label>
    </div>
     <table cellpadding="2" cellspacing="2" border="0" style="padding-left:10px; width:100%; margin-top:20px;">
       <tr>
        <td colspan="4"></td>
       
       
       </tr>
         <tr>
           <td colspan="4" align="center"><b>Registration Type:</b> &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:DropDownList ID="ddl_pending_report" runat="server"  AutoPostBack="true" 
                   onselectedindexchanged="ddl_pending_report_SelectedIndexChanged">
             
              <%-- <asp:ListItem  Value="0" Text="--Select--"  ></asp:ListItem>--%>
               <asp:ListItem Value="1" Text="Registrations"  Selected="True"></asp:ListItem>
                <asp:ListItem Value="2" Text="Amendments" ></asp:ListItem>
                   <asp:ListItem Value="3" Text="Renewals"></asp:ListItem>
             </asp:DropDownList>
           
           </td>
         </tr>
         <tr>
         <td colspan="4" align="center"><b>Pendency Stage:</b> &nbsp;&nbsp;&nbsp;&nbsp;
         <asp:DropDownList ID="ddl_Inspection" runat="server"  AutoPostBack="true" 
                 onselectedindexchanged="ddl_Inspection_SelectedIndexChanged">
       <asp:ListItem Value="0" Text="All"></asp:ListItem>
       <asp:ListItem Value="3" Text="Document awaited"></asp:ListItem>
         <asp:ListItem Value="2" Text="Inspection Pending"></asp:ListItem>
          <asp:ListItem Value="7" Text="Inspection Passed"></asp:ListItem>        
          </asp:DropDownList>
         
         </td>
         
         </tr>
         
         <tr>  
            <td colspan="4"  align="center">
            <asp:Label ID="lbl_text_msg" runat="server" Text=""></asp:Label>
            </td>
         </tr>
         <tr>
           <td colspan="4">
           
             <asp:Panel ID="pnl_grid_data" runat="server" BorderStyle="Solid">
               <asp:GridView ID="grd_pending_report" runat="server" CellPadding="2" CellSpacing="2"
               GridLines="Both" AutoGenerateColumns="false" Width="100%"
              BorderWidth="2" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left" 
            HeaderStyle-HorizontalAlign="Center"    ShowFooter="false" 
                     EmptyDataText ="No data." onrowdatabound="grd_pending_report_RowDataBound">
            
             <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <FooterStyle CssClass="dynRowHeader" />
                   <EditRowStyle BackColor="LightBlue" ForeColor="Red" />
                   
                   <Columns>
                   <asp:TemplateField HeaderText="Zone">
                      <ItemTemplate>
                        <asp:Label ID="lbl_zonename" runat="server" Text='<%#Eval("Zonename")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Branch">
                     <ItemTemplate>
                       <asp:Label ID="lbl_branchname" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                       <asp:Label ID="lbl_branch_id" runat="server" Text='<%#Eval("BranchID")%>' Visible="false"></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>
                   <asp:TemplateField HeaderText="< 30 days">
                      <ItemTemplate>
                        <asp:Label ID="lbl_less30" runat="server" Text='<%#Eval("<30")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="31-90  days">
                      <ItemTemplate>
                        <asp:Label ID="lbl_less60" runat="server" Text='<%#Eval("31-90")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="91-180 days">
                      <ItemTemplate>
                        <asp:Label ID="lbl_less90" runat="server" Text='<%#Eval("91-180")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                    
                       <asp:TemplateField HeaderText="180-365  days">
                      <ItemTemplate>
                        <asp:Label ID="lbl_less180" runat="server" Text='<%#Eval("181-365")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="> 365  days">
                      <ItemTemplate>
                        <asp:Label ID="lbl_less365" runat="server" Text='<%#Eval(">365")%>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                   </Columns>
            </asp:GridView>
             
             
             </asp:Panel>
           
           </td>
         </tr>
         
       
       </table>

</asp:Content>

