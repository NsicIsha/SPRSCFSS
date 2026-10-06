<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Convertprovisonaltofreshregistration.aspx.cs" Inherits="appAdministrator_Convertprovisonaltofreshregistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript" language="javascript">
      function confirmation() {
          if (confirm('Are you sure you want to Change Registration Type???')) {
              return true;
          } else {
              return false;
          }
      }
  </script>
<style type="text/css">
.highlight {text-decoration: none;color:black;background:yellow;}

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <table cellpadding="4" cellspacing="4" border="0">
     <tr>
       
           <td  align="left" style="font-size:25px; font-family:Arial Baltic; font-weight:bold; text-decoration:underline; color:Navy;"> Change Registration Type:</td>
    
     
     </tr>
     <tr>
       <td align="left" valign="top" style="height:10px;">
       
       
       </td>
     </tr>
     <tr>
       <td align="left" valign="top">
       <table cellpadding="2" cellspacing="2" border="0">
         <tr>
           <td align="left" valign="top"><b> Type: </b></td>
            <td align="left" valign="top">
             <asp:UpdatePanel ID="Update_bton"  runat="server" >
                        <ContentTemplate>
                        
            <asp:DropDownList ID="ddltype" runat="server" AutoPostBack="true" 
                    Width="220px" onselectedindexchanged="ddltype_SelectedIndexChanged">
              <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
              <asp:ListItem Text="Provisional" Value="1" ></asp:ListItem>
              <asp:ListItem Text="Regular" Value="2"></asp:ListItem>
            </asp:DropDownList>
               </ContentTemplate>
                        </asp:UpdatePanel>
             </td>
         
         </tr>
       </table>
       </td>
     
     </tr>
     
     <tr>
       <td align="left" valign="top" style="height:10px;">
          <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
       </td>
     </tr>
       <tr>
         <td align="left" valign="top"><b><asp:Label ID="lbl_prov_regcompanies" runat="server" Text=""></asp:Label></b></td>
       
       </tr>
       
       <tr>
         <td align="left" valign="top"  id="tbl_search" runat="server">
           <table cellpadding="2" cellspacing="2" border="0">
             <tr>
               <td align="left" valign="top" > Enter Company Name:</td>
                <td align="left" valign="top"><asp:TextBox ID="txt_search" runat="server" Text="" 
                        EnableTheming="True" Width="220px"></asp:TextBox></td>
                  <td align="left" valign="top"><asp:Button ID="btn_search" runat="server" 
                          Text="Search" CssClass="btnCls" onclick="btn_search_Click" /></td>
                  <td align="left" valign="top"><asp:Button ID="btn_clear" runat="server" 
                          Text="Clear"  CssClass="btnCls" onclick="btn_clear_Click" /></td>
             </tr>
           </table>
         
         </td>
       </tr>
          <tr>
       <td align="left" valign="top" style="height:10px;"></td>
     </tr>
     <tr>
        <td align="left" valign="top">
          <asp:GridView ID="grd_listofpro_reg_companies" runat="server" 
                AutoGenerateColumns="false"  PageSize="10"
             PageIndex="0" AllowSorting="true"   Width="100%" CellPadding="10" 
                CellSpacing="1" 
                onpageindexchanging="grd_listofpro_reg_companies_PageIndexChanging" 
                onrowdatabound="grd_listofpro_reg_companies_RowDataBound" 
                AllowPaging="True" onrowcommand="grd_listofpro_reg_companies_RowCommand"  > 
             <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                             <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No.">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                   <ItemTemplate>
                                     <table cellpadding="0" cellspacing="0" border="0">
                                       <tr>
                                         <td align="left" valign="top">
                                           <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                                         </td>
                                       </tr>
                                       <tr>
                                         <td align="left" valign="top">
                                           <asp:Label ID="lbl_Company_address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                                         </td>
                                       </tr>
                                      
                                        
                                      
                                     </table>
                                   </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Contact Details">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_Contact_Deatils" runat="server" Text='<%#Eval("ContactDetails") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SPR No/PAN No">
                                  <ItemTemplate>
                                     <asp:Label ID="lbl_registration" runat="server" Text='<%#Eval("CompanyPan") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Zone/Branch">
                                  <ItemTemplate>
                                     <asp:Label ID="lbl_Zone_Branch_name" runat="server" Text='<%#Eval("ZoneBranch") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                     <%-- <asp:Label ID="lbl_text_change" runat="server" Text=""></asp:Label>--%>
                                        <asp:LinkButton ID="linkbutton_show" runat="server" Text="" OnClientClick="javascript:return confirmation();" CommandName=""
                                          CommandArgument='<%#Eval("CompanyID").ToString()+","+Eval("CompanyStaus").ToString() %>'></asp:LinkButton> 
                                       <asp:Label ID="lbl_company_id" runat="server" Text='<%#Eval("CompanyID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                  </asp:TemplateField>
                                   <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_view_text" runat="server" Text=""></asp:Label>
                                       <asp:Label ID="lbl_branch_id" runat="server" Text='<%#Eval("BranchId") %>' Visible="false"></asp:Label>
                                       <asp:Label ID="lbl_branch_name" runat="server" Text='<%#Eval("BranchName") %>' Visible="false"></asp:Label> 
                                        <asp:Label ID="lbl_status_check" runat="server" Text='<%#Eval("CompanyStaus")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                
                                 
                                </Columns>
               
             
             </asp:GridView>
        
        </td>
     </tr>
   
   </table>


</asp:Content>

