<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintUserListDetails.aspx.cs" Inherits="appAdministrator_PrintUserListDetails" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

  

<html xmlns="http://www.w3.org/1999/xhtml">

<%@ Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<head runat="server">
   <script type="text/javascript"  language ="javascript">
  
  function CallPrint(strid)
   {
   var prtContent = document.getElementById(strid);
   var WinPrint = window.open('','','left=0,top=0,width=700px,height=600px,toolbar=0,scrollbars=1,status=0');
   WinPrint.document.write(prtContent.innerHTML);
   WinPrint.document.close();
   WinPrint.focus();
   WinPrint.print();
   WinPrint.close();
   }
  
  </script>
  
   <title>Print  Details userList Page </title>
      <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
<style media="print">

                .MyCssClass thead
                {
                    display: table-header-group;
                }
    .noPrint 
 { 
  display: none;
 }


</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Div_Main">
    
        <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
           <tr>
             
                    <td colspan="2">
                       <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>
                 <tr><td style="height:10px;"></td></tr>
                 <tr>
            <td>
            
     <asp:GridView ID="grd_user_list_report" runat="server" 
           AutoGenerateColumns="false"  Width="100%"
               AllowPrintPaging="true"   UseAccessibleHeader="true"
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"  
                      Visible="true"   
                     PageSize="4" onrowdatabound="grd_user_list_onRowDataBound">
                      <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    
                                    <Columns>
                                     <asp:TemplateField HeaderText="Zone">
                                       <ItemTemplate>
                                       <asp:Label ID="lbl_Zone_user" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                                       </ItemTemplate>
                                     
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Branch">
                                       <ItemTemplate>
                                       <asp:Label ID="lbl_Branch_user" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                       </ItemTemplate>
                                     
                                     </asp:TemplateField>
                                  
                                        <asp:TemplateField HeaderText="User Name">
                                          <ItemTemplate>
                                            <asp:Label ID="lbl_user_name" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>                                      
                                          </ItemTemplate>
                                        
                                        </asp:TemplateField>
                                     
                                      
                                    <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                    <asp:Label ID="lbl_username" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                    </ItemTemplate>
                                    
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone" >
                                    <ItemTemplate>
                                    <asp:Label ID="lbl_phone" runat="server" Text='<%#Eval("Phone") %>'></asp:Label>
                                    
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Mobile">
                                       <ItemTemplate>
                                       <asp:Label ID="lbl_mobile_user" runat="server" Text='<%#Eval("MObile") %>'></asp:Label>
                                       </ItemTemplate>
                                     
                                     </asp:TemplateField>
                                     
                                      <asp:TemplateField HeaderText="Email">
                                       <ItemTemplate>
                                       <asp:Label ID="lbl_Email_user" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                       </ItemTemplate>
                                     
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Role">
                                       <ItemTemplate>
                                       <asp:Label ID="lbl_Role_user" runat="server" Text='<%#Eval("Role") %>'></asp:Label>
                                       </ItemTemplate>
                                     
                                     </asp:TemplateField>
                                      
                                  
                                    </Columns>
                     
                     
                     
                     </asp:GridView>
              </td>
               </tr>
           
          
        
           
         
         </table>
       
       
         
    
    </div>
    
    <div  style="margin-top:10px;">
      <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
      <tr><td></td></tr>
     <tr><td></td></tr>
     <tr>
       <td align="center"> <input  type="button"  value="Print" onclick="javascript:CallPrint('Div_Main');" /></td>
       </tr>
     </table>
    
    </div>
    </form>
</body>
</html>
