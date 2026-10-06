<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ZoneBranchwiseReport.aspx.cs" Inherits="appAdministrator_ZoneBranchwiseReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div align="center">
  
  <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Dashboard Report" CssClass="headerAll"></asp:Label>
    </div>
    <div style="height:10px;"></div>
  <asp:MultiView ActiveViewIndex="0" ID="mvZoneListing" runat="server">
  
 <%-- edit--%>
    <asp:View ID="viewZoneListing" runat="server" >
        <asp:GridView ID="grdZonelisting" runat="server" CellPadding="5" 
              CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="70%"
              BorderWidth="0" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left" 
            HeaderStyle-HorizontalAlign="Center" onrowcommand="grdZonelisting_RowCommand" 
            onrowdatabound="grdZonelisting_RowDataBound" ShowFooter="true" >
                <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <FooterStyle CssClass="dynRowHeader" />
                   
                   <Columns>
                        <asp:TemplateField HeaderText="Zone">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnZone" runat="server" CausesValidation="false" CommandArgument='<%#Eval("ZoneId") %>' CommandName="zone"
                                    Text='<%#Eval("ZoneName") %>' ></asp:LinkButton>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Literal ID="ltlzone" runat="server" Text=""></asp:Literal>
                            </FooterTemplate>
                            
                        </asp:TemplateField>
                        
                        <asp:BoundField HeaderText="Live Units" DataField="Livecount" /> 
                        <asp:BoundField HeaderText="Expired Units" DataField="Expiredcount" /> 
                        <asp:BoundField HeaderText="Underprocess Units" DataField="Underprocesscount"  />
                        <asp:BoundField  HeaderText="Total Units" DataField="TotalCount" /> 
                        
                   </Columns>
                   
              </asp:GridView>
         </asp:View>
         
         
         
         
         
    <asp:View ID="viewBranchListing" runat="server">
        <div align="center" style="width:700px;">
    <table cellspacing="2" cellpadding="2" border="0" width="700">
    
    <tr>
      <td align="center" valign="top" colspan="3">
        <asp:GridView ID="grd_zonewisedata" runat="server" CellPadding="3" 
                       CellSpacing="5" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="20"
                Width="70%" BorderWidth="0"  HeaderStyle-VerticalAlign="Middle" 
              HeaderStyle-HorizontalAlign="Center" ShowFooter="true"
              
              onrowdatabound="grd_Zonewise_onRowDataBound" 
              ondatabound="grd_Zonewise_Data_Bound" >
                <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <FooterStyle CssClass="dynRowHeader" />
                   <Columns>
                    
                        <asp:BoundField  HeaderText="Zone"    DataField="ZoneName"  
                           ControlStyle-CssClass="dynRowEven"  HtmlEncode="False">
                            <ControlStyle CssClass="dynRowEven"></ControlStyle>
                       </asp:BoundField>
                       <asp:TemplateField HeaderText="Branch">
                         <ItemTemplate>
                          <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                          <asp:Label ID="lbl_Branch_Id" runat="server" Text='<%#Eval("BranchID")%>' Visible="false"></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                        
                       <asp:TemplateField HeaderText="Live Units">
                         <ItemTemplate>
                         <asp:Label ID="lbl_live_unit" runat="server" Text='<%#Eval("LiveUNit")%>'></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="Expired Units">
                         <ItemTemplate>
                           <asp:Label ID="lbl_Expire_Unit" runat="server" Text='<%#Eval("ExpiredUnit")%>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Underprocess Units">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Underprocess_Unit" runat="server" Text='<%#Eval("UnderProcessUnit") %>'></asp:Label>
                        </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Total Units">
                     <ItemTemplate>
                      <asp:Label ID ="lbl_total_unit" runat="server" Text='<%#Eval("TotalCount")%>'></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>
                     
 
                   </Columns>
               
        
        
        </asp:GridView>
      
      </td>
    
    </tr>
    <tr>
        <td style="height:10px;">
        </td>
    </tr>
    
    <tr>
        <td >
            <asp:Button ID="btnBackView" runat="server" Text="Back" 
                CausesValidation="false" CssClass="btnCls" onclick="btnBackView_Click" />
        </td>
    </tr>
  
  </table>
  </div>
    </asp:View>
  </asp:MultiView>
    
    
  
  </div>

</asp:Content>

