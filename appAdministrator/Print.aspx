<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false" ValidateRequest="false"
     CodeFile="Print.aspx.cs" Inherits="appAdministrator_Print" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<%@ Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<script type="text/javascript" language="javascript">
   


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
function btnPrint_onclick() {

}

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

          function PrintGridData() {
              var prtGrid = document.getElementById('<%=DetailsView2.ClientID %>');
              prtGrid.border = 0;
              var prtwin = window.open('', '', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
              prtwin.document.write(prtGrid.outerHTML);
              prtwin.document.close();
              prtwin.focus();
              prtwin.print();
              prtwin.close();
          }  

   </script>



 
<head runat="server">
    <title>Print Page</title>
    
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
.table{
    width: 100%;
}
.thead {
    display: table-header-group;
    text-align: center;
}
.tbody {
    display: table-row-group;
}
tr.vcard{
    page-break-inside: avoid;
    margin-top: 10px;
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
                <tr>
                   
                    <td colspan="2" style="vertical-align: top; width: 100%;">
                     <div id="Div ReportName"  style="margin-left:250px; font-size:25px; font-family:Arial Baltic; font-weight:bold; white-space:nowrap;"> Report:&nbsp; <asp:Label ID="lblReportName" runat="server" Text=""></asp:Label></div> 
           
    <div  class="MyCssClass" style="vertical-align:middle; width:100%;  margin-right :0px; page-break-after:always;" id="div_show">
     <asp:GridView ID="DetailsView2" runat="server"  AutoGenerateColumns="false"  Width="100%"
             EnableViewState="False"  AllowPrintPaging="true"   UseAccessibleHeader="true"
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"   onrowdatabound="MainPrint_Databound" 
                     PageSize="4" onprerender="detailsview_prerender" CssClass="table">
                                     <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader" />
                    
                                         
             
              <Columns>
                               
                                       
                                        <asp:BoundField DataField="Company" HeaderText="Company"  ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                        <asp:BoundField DataField="Contact_Details"   HeaderText="Contact_Details" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                        <asp:BoundField DataField="ApplicationDate" HeaderText="Registration No" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
                                        <asp:BoundField DataField="Zone/Branch" HeaderText="ZONE/Branch" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
                                        <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>
                                 
                                 
                                 
                                    </Columns>
             
             </asp:GridView>
    </div>
                    </td>
                </tr>
                
                  
              <%--  <tr>
                    <td colspan="2">
                        <uc2:Footer_Admin ID="Footer_Admin1" runat="server" />
                    </td>
                </tr>
                --%>
              
               
            </table>
     
     
    
      
       
    </div>
    
   
                     
   

   
     <div>
        <table cellpadding="0" cellspacing="0" style="margin-left:200px;">
              <tr>
                   <td colspan="2" align="left">
                   
                   <asp:Button ID="btnword" runat="server" OnClick="btnword_Click" Text="Export to Word"  Visible="false"/>
                    <asp:Button ID="btnexcel" runat="server" OnClick="btnexcel_Click" Text="Export to Excel"  Visible="false"/>
                    <asp:Button ID="btnpdf" runat="server" OnClick="btnpdf_Click" Text="Export to PDF" Visible="false" />
                 <%--  <input type="button" id="btnPrint" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=DetailsView2.ClientID %>');window.print();"  />--%>
                   <input type="button" id="btnPrint" value="Print"  class="noPrint" runat="server"  onclick="javascript:PrintGridData();"  />
                    <%-- <asp:Button ID="Button1" runat="server" Text="Print" OnClick="Print"  />--%>
                   </td>
                
                </tr>
                </table>
     
    </div>
     </form>
    
</body>
</html>
