<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MIC_REPORT_DETAILS_Details.aspx.cs" Inherits="appMIC_MIC_REPORT_DETAILS_Details" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Header_Admin.ascx" TagName="Header_Admin" TagPrefix="uc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Single Point Registration (SPR)</title>

   

    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
    <script language="javascript" type="text/javascript" src="../JScript/jquery-1.7.1.js"></script>

    <script src="../JScript/jquery-1.3.2-vsdoc.js" type="text/javascript"></script>

    <script src="../JScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JScript/ui.core.js" type="text/javascript"></script>

    <script src="../JScript/ui.progressbar.js" type="text/javascript"></script>
    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
    
    <link href="../CSS/ui.all.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.base.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.progressbar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.theme.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../JScript/JSAdmin.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePageMethods="true"  >
        </asp:ToolkitScriptManager>


        <table cellpadding="10" cellspacing="10" border="0"  width="100%" >
            <tr>
                <td>

               

         <table cellpadding="0" cellspacing="0" border="0"  width="100%" >
            
          <tr>
              <td>

                   
                        <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    
 
        </td>
    </tr>

       <tr valign="top" align="center">
                 <td style="padding:2px">
               
                 </td>
             </tr>
    <tr valign="top" align="center">
        <td colspan="2" valign="top" style="height:1px; background-image:url(../Images/blueline.JPG); background-repeat:repeat-x;">
        </td>
    </tr>
    <tr valign="top" align="center">
        <td colspan="2" valign="top" style="height:1px; background-color:#FFFFFF;">
        </td>
    </tr>
</table>

                    </td>
                </tr>
               
           <tr>
           
           
             <td align="centre" valign= "bottom" style="padding-left:20px; width:100%;">
             <table cellpadding="0" cellspacing="0" border="0" width="90%">
                   <tr>
                       <td colspan="2" align="centre">
                 <span style="color:green"><strong> Searching Result from SPR Database for <b><span style="color:Red;"> <asp:Label ID="lbl_searchcompany" runat="server" Text=""></asp:Label> </b>   product/store </strong></span>      
</td>
                       </tr>
               <tr>
                  
                  <td align="right" valign="top" style="padding-right:50px;" colspan="2">
                      <asp:Button ID="Btn_Search" runat="server" Text="Back" CssClass="btnCls"  OnClientClick="JavaScript: window.history.back(1); return false;"
                                  />

                     <b><span style="color:Red;"><asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label></b>
                  
                  
                      </td>
               
               </tr>
               
                
               
             </table>
             
              
             
             </td>
           
           </tr> 
             <tr>
               <td  valign="top" style="padding:50px">
                
             
                  
                

                 <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="500"
                Width="100%" BorderWidth="0" 
                       
                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
                  <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                  <EmptyDataTemplate>
                 <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>
                 <Columns>
                                        <asp:TemplateField HeaderText="S.NO">
                      <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                      
                      </ItemTemplate>
                    </asp:TemplateField>
                 
                   <asp:TemplateField HeaderText="NSIC Office">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                     </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Category"  >  
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("fGPtbl_CUnit_CategoryCastId") %>'></asp:Label>
                    </ItemTemplate>
                   
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Company Name">
                      <ItemTemplate>
                      <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                  
                    <asp:TemplateField HeaderText="Address" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("address")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                      <ItemTemplate>
                      <asp:Label ID="lbl_old_registration_no" runat="server" Text='<%#Eval("fGPtbl_Company_Headoffice_Phone") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField  HeaderText="Email">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("fGPtbl_Company_HeadOffice_Email") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Products">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberhh" runat="server" Text='<%#Eval("Products") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                 </Columns>
                </asp:GridView>   
                
                 
                    
               
               </td>
             
             </tr>
            
          
          
            </table>
          
    </form>
</body>
</html>
