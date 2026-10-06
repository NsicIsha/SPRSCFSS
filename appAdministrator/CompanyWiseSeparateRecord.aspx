<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyWiseSeparateRecord.aspx.cs" Inherits="appAdministrator_CompanyWiseSeparateRecord"%>
<%@Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>


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

<script type="text/javascript" src='http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js'></script>
 <script type="text/javascript" language="javascript">
   $(document).ready(function()
     {
         var Registration_Amount = $('#Registration_amount').text();
           
        
     });
 
 </script>

<head runat="server">
    <title>Company Wise Separate Record</title>
     <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
      <style   type="text/css">

                .MyCssClass thead
                {
                    display: table-header-group;
                }
    .noPrint 
 { 
  display: none;
 }
 .Headingclass
  {
  	 font-size:18px;
  	 
  	 font-style:normal;
  	 color:#fbedbb;
  	
  	 font-weight:bold;
  
  }
  .lblclass
   {
   	 width:700px;
   	 float:left;
   	 display:block;
   	 
   }


</style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="Div_main" style="margin-left:150px; float:left;">
       
         <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                <tr>
                    <td colspan="2">
                   <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>
                <tr>
                  
                    <td colspan="2" style="vertical-align: top; width: 700px;"> 
                      <table id="tbl_button" width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                        <tr>
                          <td align="left">
                         <%-- <asp:LinkButton ID="lnkBack" runat="server" Text="BACK" onclick="lnkBack_Click"></asp:LinkButton>--%>
                          <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>
                          </td>
                          <td align="right"> <input type="button" id="Button1" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Company_wise.ClientID %>');window.print();"  /></td>
                        
                        </tr>
                      </table>
                        
                     <div id="Div ReportName"  style="margin-left:250px; font-size:25px; font-family:Arial Baltic; font-weight:bold; white-space:nowrap;"> Report: &nbsp;<asp:Label ID="lblReportName" runat="server" Text=""></asp:Label></div> 
           <br />
          <br />
    <div    style="vertical-align:middle; width:100%;  margin-right :0px; page-break-after:always;" id="div_show1">
  
    <asp:GridView ID="grd_total" runat="server" AutoGenerateColumns="false" Width="100%"
             EnableViewState="False"  AllowPrintPaging="true"   
            UseAccessibleHeader="true" 
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"   
                     PageSize="20" >
                        <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader2" />
                                    
                               <Columns>
                                  <asp:TemplateField  HeaderText="Fee Type">
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_Form_Type" runat="server" Text='<%#Eval("FormTYpe") %>'></asp:Label>
                                    
                                    </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField>
                                   <HeaderTemplate>
                                     <table cellspacing="2" cellpadding="2">
                                     <tr class="dynRowHeader2"><td> Registration </td></tr>
                                     <tr class="dynRowHeader2"><td> Amount</td>  </tr>
                                     </table>
                                   </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_reg_amount" runat="server" Text='<%#Eval("RegistrationAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                   </asp:TemplateField> 
                                   <asp:TemplateField >
                                       <HeaderTemplate>
                                     <table cellspacing="2" cellpadding="2">
                                     <tr class="dynRowHeader2"><td> Registration </td></tr>
                                     <tr class="dynRowHeader2"><td> service tax</td>  </tr>
                                     </table>
                                   </HeaderTemplate>
                                     <ItemTemplate>
                                       <asp:Label ID="lbl_reg_service_tax" runat="server" Text='<%#Eval("RegistrationServiceTax") %>'></asp:Label>
                                     </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField >
                                    <HeaderTemplate>
                                     <table cellspacing="2" cellpadding="2">
                                     <tr class="dynRowHeader2"><td> Inspection </td></tr>
                                     <tr class="dynRowHeader2"><td> Amount</td>  </tr>
                                     </table>
                                   </HeaderTemplate>
                                     <ItemTemplate>
                                       <asp:Label ID="lbl_inspection_amount" runat="server" Text='<%#Eval("InspectionAmount") %>'></asp:Label>
                                     
                                     </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField >
                                     <HeaderTemplate>
                                     <table cellspacing="2" cellpadding="2">
                                     <tr class="dynRowHeader2"><td> Inspection </td></tr>
                                     <tr class="dynRowHeader2"><td> service tax</td>  </tr>
                                     </table>
                                   </HeaderTemplate>
                                     <ItemTemplate>
                                       <asp:Label ID="lbl_inspection_service_tax" runat="server" Text='<%#Eval("InspectionserviceTax") %>'></asp:Label>
                                     </ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField >
                                   <HeaderTemplate>
                                     <table cellspacing="2" cellpadding="2">
                                     <tr class="dynRowHeader2"><td> Payment </td></tr>
                                     <tr class="dynRowHeader2"><td> Date</td>  </tr>
                                     </table>
                                   </HeaderTemplate>
                                      <ItemTemplate>
                                        <asp:Label ID="lbl_payment_date" runat="server" Text='<%#Eval("PaymentDate") %>'></asp:Label>
                                       </ItemTemplate>
                                      
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Payment Mode">
                                     <ItemTemplate>
                                       <asp:Label ID="lbl_payment_mode" runat="server" Text='<%#Eval("PaymentType") %>'></asp:Label>
                                     
                                     </ItemTemplate>
                                   
                                   </asp:TemplateField>
                                  
                               
                               </Columns>
                                </asp:GridView>
  
  </div> 
      <asp:Label ID="lbl_summary" runat="server" Text="" CssClass="lblclass" >
      </asp:Label>
        
    
                    </td>
                </tr>
               <tr>
                 <td>
              
                <asp:Label ID="lblid" runat="server" Text='' Visible="false"></asp:Label>
                 <asp:Label ID="lbldatefrom" runat="server" Visible="false" Text=""></asp:Label>
                  <asp:Label ID="lblto" runat="server" Text="" Visible="false"></asp:Label>
                
                </td>
                </tr>
                
                  
            
              
               
            </table>
    
    
    </div>
    
      <div style="vertical-align:top; width:100%;  margin-right :10px; page-break-after:avoid; white-space:nowrap;" id="div_show">
         <table cellpadding="0" cellspacing="0" border="0" width="100%">
         <tr>
           <td style="white-space:nowrap; width:95%;"> </td>
         </tr>
         </table>
         
        
      </div>
     <div style="margin-top:20px;">
        <table cellpadding="0" cellspacing="0" style="margin-left:700px;">
              <tr>
              <td >
                 <input type="button" id="btnPrint" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Company_wise.ClientID %>');window.print();"  />
                    <%-- <asp:Button ID="Button1" runat="server" Text="Print" OnClick="Print   <td colspan="2" align="center">
                  "  />--%>
                   </td>
                
                </tr>
                </table>
     
    </div>
    
    </form>
</body>
</html>
