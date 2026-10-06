<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintInspectionAgencyList.aspx.cs" Inherits="appAdministrator_PrintInspectionAgencyList" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<head runat="server">
    <title>Inspection Agency List</title>
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
                      
                       <asp:GridView ID="grd_inspectionAgency" runat="server" 
           AutoGenerateColumns="false"  Width="100%"
               AllowPrintPaging="true"   UseAccessibleHeader="true"
                                 BorderWidth="1" CellPadding="5" CellSpacing="1"  
                      Visible="true"   
                     PageSize="5" onrowdatabound="grd_inspection_Agency_List">
                      <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <Columns>
                                    <asp:BoundField  DataField="Zone" HeaderText="Zone" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-CssClass="GridCss"/>
                                    <asp:BoundField  DataField="BranchName" HeaderText="Branch" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"  ControlStyle-CssClass="GridCss"/>
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-CssClass="GridCss" />
                                    <asp:BoundField DataField="AgencyName" HeaderText=" Inspection Agency" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-CssClass="GridCss" />
                                    <asp:BoundField DataField="CreateDate" HeaderText="Create Date" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"  ControlStyle-CssClass="GridCss"/>
                                    <asp:BoundField DataField="EmailID" HeaderText="Email" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-CssClass="GridCss" />
                                     <asp:TemplateField  HeaderText="Address" ControlStyle-CssClass="GridCss">
                                       <ItemTemplate>
                                            <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("Address")%>'></asp:Label>
                                       
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
