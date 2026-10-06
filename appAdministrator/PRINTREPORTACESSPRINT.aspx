<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PRINTREPORTACESSPRINT.aspx.cs" Inherits="appAdministrator_PRINTREPORTACESSPRINT" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%@ Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Print Acess  Page</title>
    <script type="text/javascript"  language ="javascript">
  
  function CallPrint(strid)
   {
    var prtGrid = document.getElementById('<%=grd_print_certificate.ClientID %>');
    prtGrid.border = 0;
    var prtwin = window.open('', '', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
   }
  
  </script>
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

            .GridCss
                {
  	            white-space:nowrap;
                 }
</style>
    
</head>
<body>
    <form id="form1" runat="server">
      <div  id="Div_Main" >
        <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
           <tr>
             
                    <td colspan="2">
                       <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>
                 <tr><td style="height:10px;"></td></tr>
                 <tr>
            <td>
            
                      
                      
                      <%-- for Design If select Only Branch And Zone ID----%>
                      
                     <asp:GridView ID="grd_print_certificate" runat="server"
                                     AutoGenerateColumns="false"  Width="100%"
               AllowPrintPaging="true"   UseAccessibleHeader="true"
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"  
                      Visible="true"   
                     PageSize="10" onrowdatabound="grd_print_certificate_onrowDataBound" >
                     <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <Columns>
                                      
                                         <asp:TemplateField HeaderText="Company Name">
                                         <ItemTemplate>
                                       <asp:Label id="lbl_comapny_print_name" runat="server" Text='<%#Eval("ComapnyName") %>'></asp:Label> 
                                       <asp:Label id="lbl_companyId_print" runat="server" Text='<%#Eval("ComapnyID") %>' Visible="false"></asp:Label>
                                         </ItemTemplate>
                                         </asp:TemplateField>
                                        
                                      <%-- <asp:BoundField DataField="Zone/Branch" HeaderText=" Zone / Branch  " ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"  ControlStyle-Width="100px"/>--%>
                                       <asp:TemplateField>
                                              <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                               <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >Zone/</td>        
                                                </tr>
                                                <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >Branch</td>        
                                                </tr>
                                              </table>
                                                 
                                              </HeaderTemplate>
                                              <ItemTemplate>
                                                 <asp:Label ID="lbl_Zone_Branch" runat="server" Text='<%#Eval("Zone/Branch") %>'></asp:Label>
                                              
                                              </ItemTemplate>
                                         
                                         </asp:TemplateField>
                                         
                                       
                                       <%-- <asp:BoundField DataField="UserName/RequestedDate" HeaderText="RequestedBy/RequestDate" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-Width="20px" />--%>
                                        <asp:TemplateField>
                                              <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                               <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >RequestedBy/</td>        
                                                </tr>
                                                <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >RequestDate</td>        
                                                </tr>
                                              </table>
                                                 
                                              </HeaderTemplate>
                                              <ItemTemplate>
                                                 <asp:Label ID="lbl_request_date" runat="server" Text='<%#Eval("UserName/RequestedDate") %>'></asp:Label>
                                              
                                              </ItemTemplate>
                                         
                                         </asp:TemplateField>
                                         
                                         <asp:BoundField DataField="Rquest" HeaderText="Reason " ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"  ControlStyle-Width="250px"/>
                                         <%-- <asp:BoundField DataField="ApprovedBy/AcceptedDate" HeaderText="Approved By/ Accepted Date" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />--%>
                                          <asp:TemplateField>
                                              <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                               <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >Approved By/</td>        
                                                </tr>
                                                <tr>
                                               <td align="left" valign="top" class="dynRowHeader1" >Accepted Date</td>        
                                                </tr>
                                              </table>
                                                 
                                              </HeaderTemplate>
                                              <ItemTemplate>
                                                 <asp:Label ID="lbl_RequsetDate_userName" runat="server" Text='<%#Eval("UserName/RequestedDate") %>'></asp:Label>
                                              
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
