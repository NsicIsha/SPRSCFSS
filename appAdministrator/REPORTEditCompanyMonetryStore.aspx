<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="REPORTEditCompanyMonetryStore.aspx.cs" Inherits="appAdministrator_REPORTEditCompanyMonetryStore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
  .headerAllpage
    {
    font-family: Arial, Helvetica, sans-serif;
	font-size: 16px;
	font-weight:bold;
	font-style:normal;
	color:#22518F;
	padding-top:10px;
	padding-left:5px;
	text-decoration:underline;	
    }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="2" cellspacing="2" border="0">
 <tr>
    <td colspan="2" align="left" valign="top" Class="headerAllpage">
        History Company Wise Edit Store Monetry Limit:
    
    </td>
 </tr>
 <tr><td align="left" valign="top" colspan="2">
   
   <table cellpadding="2" cellspacing="2" border="0">
     <tr>
       <td align="left" valign="top"><b>Search:</b></td>
         <td align="left" valign="top">
         <asp:UpdatePanel ID="update_panel_selectcriteria" runat="server">
           <ContentTemplate>
          
         <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL" 
                   AutoPostBack="true" 
                   onselectedindexchanged="ddlSearchCriteria_SelectedIndexChanged" 
                   TabIndex="1">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
            </asp:DropDownList>
             </ContentTemplate>
         </asp:UpdatePanel>
         </td>
         <td align="left" valign="top"><asp:TextBox ID="txt_search" runat="server" Text="" 
                 CssClass="txt_bx" TabIndex="2"></asp:TextBox></td>
         <td align="left" valign="top">
         <asp:Button ID="btn_serch" runat="server" Text="Search"  CssClass="btnCls" 
                 onclick="btn_serch_Click" TabIndex="3"  />
         </td>
         
        
     </tr>
     
   
   </table>
   
 </td></tr>
  <tr>
   <td colspan="2" align="left" valign="top">
     <asp:Label ID="lbl_grid_view" runat="server" Text=""></asp:Label>
            
         <asp:GridView ID="grd_view_history" runat="server"  CellPadding="3" 
                       CellSpacing="3" AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                        Width="100%" BorderWidth="0px" PageSize="20" 
                       HeaderStyle-VerticalAlign="Middle" 
           HeaderStyle-HorizontalAlign="Center" 
           onrowdatabound="grd_view_history_RowDataBound" 
           onpageindexchanging="grd_view_history_PageIndexChanging" >
                        <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                  <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>
                           <Columns>
                           <asp:TemplateField  HeaderText="Company Name">
                             <ItemTemplate>
                               <table cellspacing="3" cellpadding="3" border="0">
                                 <tr>
                                   <td align="left" valign="top">
                                         <asp:Label ID="lbl_Company_Name" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                         <asp:Label ID="lbl_Company_id" runat="server" Text='<%#Eval("CompanyID") %>' Visible="false"></asp:Label>
                                         <asp:Label ID="lbl_comapny_name_text" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
                                         </td></tr>
                                         <tr><td align="left" valign="top">
                                           <asp:Label ID="lbl_Comapny_Address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                                        </td>
                                                 
                                         </tr> <tr><td align="left" valign="top">
                                           <asp:Label ID="lbl_phone" runat="server" Text='<%#Eval("Phone") %>'></asp:Label>
                                          </td>
                                                 
                                         </tr><tr><td align="left" valign="top">
                                           <asp:Label ID="lbl_email" runat="server" Text='<%#Eval("Email")%>'></asp:Label>
                                          </td></tr>
                                          <tr><td align="left" valign="top">
                                           <asp:Label ID="lbl_status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                           
                                   </td>
                                 </tr>
                                 <tr>
                                   <td align="left" valign="top">
                                     <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("Registrationno") %>'></asp:Label>
                                   </td>
                                 </tr>
                                 <tr> <td align="left" valign="top">
                                      <asp:Label ID="lbl_registration_date" runat="server" Text='<%#Eval("RegistrationDate") %>'></asp:Label>
                                      <asp:Label ID="lbl_Amendmet_id" runat="server" Text="" Visible="false"></asp:Label>
                                 </td></tr>
                                 
                                    
                                 
                                 <tr>
                                   <td align="left" valign="top">
                                     <asp:Label ID="lbl_EditDateWise_date" runat="server" Text=""></asp:Label>
                                     
                                   </td>
                                 </tr>
                                   
                               </table>
                             
                             </ItemTemplate>
                           
                           </asp:TemplateField>
                           
                           </Columns>
                       
                       </asp:GridView>   
            
   
   </td>
 
 </tr>
 </table>

</asp:Content>

