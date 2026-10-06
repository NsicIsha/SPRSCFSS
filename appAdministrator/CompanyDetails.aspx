<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyDetails.aspx.cs" Inherits="appAdministrator_CompanyDetails" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

  <html xmlns="http://www.w3.org/1999/xhtml">
<%@Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<script type="text/javascript"  language="javascript">
       function AddTHEAD(tableName) 
         {    
            var table = document.getElementById(tableName);
            if (table != null) 
            {
               var head = document.createElement("THEAD");
               head.style.display = "table-header-group";        
               head.appendChild(table.rows[0]);
               table.insertBefore(head, table.childNodes[0]);
              }
          }   

  
  </script>


<head runat="server">
    <title>Company Details Page</title>
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
    <div id="DivMain">
           <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                <tr>
                    <td colspan="2">
                       <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>
                <tr>
                   
                    <td colspan="2" style="vertical-align: top; width: 100%;"> 
                      <table  id="tbl_button" width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                        <tr> 
                          <td align="left">  <asp:LinkButton ID="lnkBack" runat="server" Text="BACK" onclick="lnkBack_Click"></asp:LinkButton></td>
                           <td align="right"> <input type="button" id="Button1" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_print_company.ClientID %>');window.print();"  />   </td>
                        </tr>
                      
                      </table>
                        
                     <div id="Div ReportName"  style="margin-left:100px; font-size:25px; font-family:Arial Baltic; font-weight:bold; white-space:nowrap;"> Report: Print Certificate&nbsp;<b style="font-size:15px;">Company Name:</b> &nbsp;<asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b style="font-size:15px;">No. of prints:</b> &nbsp;<asp:Label ID="lbl_number_ofprint" runat="server" Text=""></asp:Label>
                     
                     </div> 
           
    <div   style="vertical-align:middle; width:100%;  margin-right :0px; page-break-after:always;" id="div_show">
    
      <asp:GridView ID="grd_print_company" runat="server" AutoGenerateColumns="False"
             EnableViewState="False" AllowPaging="false"  Width="100%" 
                BorderWidth="1" CellPadding="10" CellSpacing="1" ShowFooter="false" 
            ondatabound="grdDataBound">
                
                  <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    
                              <Columns> 
                                <asp:BoundField  DataField="Request" HeaderText="Reason" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                  <asp:BoundField DataField="RequestDate"  HeaderText="Request Date"  ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
                                    <asp:BoundField DataField="AcceptedDate" HeaderText="Accepted Date"  ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                      <asp:TemplateField HeaderText="TotalCount" Visible="false">
                                      <ItemTemplate>
                                         <asp:Label ID="lbl_TotalText" runat="server" Text='<%#Eval("totalCount") %>'> </asp:Label>
                                      
                                      
                                      </ItemTemplate>
                                      
                                            <FooterTemplate>
                                            <asp:Label ID="totalAmmount" runat="server"></asp:Label>
                                       
                                       
                                       </FooterTemplate>
                                      
                                      </asp:TemplateField>
                                      
                                      
                                 
                              </Columns>
                
                
                
                
                </asp:GridView>
           
      
    </div>
                    </td>
                </tr>
               
            </table>
      
    
    </div>
    </form>
</body>
</html>
