<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/LoginMaster.master" AutoEventWireup="true" CodeFile="SPR_CHECK_REGISTRATION.aspx.cs" Inherits="SPR_CHECK_REGISTRATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border = "0" cellpadding = "10px" cellspacing = "0" width = "70%" style="height:400px;">
          <tr>
            <td  valign ="middle" style = "font-family:Arial,Sans-Serif,Courier; font-size:12px; font-weight :bold; width:156px;" >
             Enter Registration&nbsp; No&nbsp; to search :
            </td>
            <%--<td align ="left"> 
             <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL" 
                    Width="200px">
            <asp:ListItem Selected="True" Text="Registration Process" Value="1"></asp:ListItem>
            <asp:ListItem  Text="Renewal Process" Value="2"></asp:ListItem>
              <asp:ListItem  Text="Amensdment Process" Value="3"></asp:ListItem>
            </asp:DropDownList>
            </td>--%>
            <td align ="left" style="width:210px;">
            <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox>
            </td>
            <td align ="left" style="width:100px;">
               <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click"  />
            </td>
          </tr>
          <tr>
           <td colspan ="3">
            <%-- <asp:Label ID="lbl_last_validity" runat="server" Text="Last Validity:" Visible="false"></asp:Label>
               <asp:Label ID="lbl_last_validity_show" runat="server" Text=""></asp:Label>--%>
           
           </td>
          </tr>
        </table>
        
      <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gv_companies" runat="server" 
            Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" 
              AutoGenerateColumns="False" onrowdatabound="grd_gvcompaniesonrowdatabound" 
                > 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <EmptyDataTemplate>
                <strong>Sorry No Record Found</strong>
            </EmptyDataTemplate>
            <Columns>
                 
                <%--<asp:BoundField DataField="Compnay" HeaderText="Company Name" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12"/>--%>
                   <asp:TemplateField HeaderText="Company Name">
                         <ItemTemplate>
                           <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("Compnay") %>'></asp:Label>
                         </ItemTemplate>
                   </asp:TemplateField>
                <%--<asp:BoundField DataField="Address" HeaderText="Company Address" HtmlEncode ="false" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />--%>
                   <asp:TemplateField HeaderText="Company Address">
                      <ItemTemplate>
                        <asp:Label ID="lbl_company_address" runat="server" Text='<%#Eval("Address") %>'></asp:Label>
                      
                      </ItemTemplate>
                   </asp:TemplateField>
               <%-- <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />--%>
                   <asp:TemplateField HeaderText="Registration No">
                     <ItemTemplate>
                        <table cellpadding="0" cellspacing="0" border="0">
                          <tr>
                            <td align="left" valign="top">
                              <asp:Label ID="lbl_new_registration_number"  runat="server" Text='<%#Eval("RegistrationNo")%>' ></asp:Label>
                            </td>
                          </tr>
                          <tr>
                             <td align="left" valign="top">
                               <asp:Label ID="lbl_old_registration_number" runat="server" Text='<%#Eval("Old_RegistrationNo") %>'></asp:Label>
                             
                             </td>
                          </tr>
                        
                        </table>
                     
                     </ItemTemplate>
                   </asp:TemplateField>
            <%--      <asp:BoundField DataField="RegistrationNo_old" HeaderText="Registration old" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />--%>
               <%-- <asp:BoundField DataField="validity" HeaderText="Validity" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />--%>
                  <asp:TemplateField HeaderText="Validity">
                     <ItemTemplate>
                       <table cellpadding="2" cellspacing="2" border="0">
                         <tr>
                           <td align="left" valign="top">
                            <asp:Label ID="lbl_validity" runat="server" Text='<%#Eval("validity") %>'></asp:Label>
                           </td>
                           </tr>
                             <tr>
                               <td align="left" valign="top">
                                <i> <asp:Label ID="lbl_validity_text" runat="server" Text="Last Validity of Cerificate was upto " Visible="false"></asp:Label></i>
                                 <asp:Label ID="lbl_last_validity_text" runat="server" Text=""></asp:Label>
                               </td>
              
                             </tr>
                             <tr>
                               <td align="left" valign="top">
                                 <asp:Label ID="lbl_registratondate" runat="server" Text='<%#Eval("RegistrationDate") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lbl_provisinol_fresh" runat="server" Text='<%#Eval("fresh_provisional") %>' Visible="false"></asp:Label>
                                  <asp:Label ID="lbl_renewal_date" runat="server" Text='<%#Eval("RenewalDate_Check") %>' Visible="false"> </asp:Label>
                                  <asp:Label ID="lbl_Accessid_type" runat="server" Text='<%#Eval("Accessid")%>' Visible="false"></asp:Label>
                               </td>
                             </tr>
                             
                             
                          
                       </table>
                     
                     </ItemTemplate>
                  </asp:TemplateField>
                
                
                
               <%-- <asp:BoundField DataField="Product" HeaderText="Products" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />--%>
                  <asp:TemplateField HeaderText="Products">
                    <ItemTemplate>
                       <asp:Label ID="lbl_product_name" runat="server" Text='<%#Eval("Product")%>'></asp:Label>
                    </ItemTemplate>
                  </asp:TemplateField>
                 
               </Columns>
        </asp:GridView>    
        
</div>

</asp:Content>

