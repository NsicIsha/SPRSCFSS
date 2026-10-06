<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyWiseRecord.aspx.cs" Inherits="appAdministrator_CompanyWiseRecord" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
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
    <title>Company Page</title>
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
        <asp:Label ID="lblZoneName" runat="server" Text="" Visible="false"></asp:Label>
        <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                <tr>
                    <td colspan="2">
                       <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>
                <tr>
                   
                    <td colspan="2" style="vertical-align: top; width: 100%;"> 
                       <table id="tbl_button" width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                          <tr>
                           <td align="left">
                           <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>
                           <%--<asp:LinkButton ID="lnkBack" runat="server" Text="BACK" onclick="lnkBack_Click"></asp:LinkButton>--%>
                           
                           </td>
                            <td align="right"> <input type="button" id="Button1" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Company_wise.ClientID %>');window.print();"  /></td>
                         </tr>
                      <%--   <tr>
                           <td style="height:10px"></td>
                         </tr>--%>
                       </table>
                        
                     <div id="Div ReportName"  style="margin-left:250px; font-size:25px; font-family:Arial Baltic; font-weight:bold; white-space:nowrap;"> Report:&nbsp;<b style="font-size:15px;">Branch Name:</b> &nbsp;<asp:Label ID="lblReportName" runat="server" Text="" ></asp:Label></div> 
           
    <div   style="vertical-align:middle; width:100%;  margin-right :0px; page-break-after:always;  white-space:nowrap;" id="div_show">
     <asp:GridView ID="grd_Company_wise" runat="server"  AutoGenerateColumns="false"  Width="100%"
             EnableViewState="False"  AllowPrintPaging="true"   UseAccessibleHeader="true"
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"    ShowFooter="true"
                     PageSize="4" onrowdatabound="Grd_Report_companyWise" 
           >   <%-- ondatabound="company_DataBound"--%>
                                     <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader2" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader2" />
                    
                                         
             
              <Columns>
                                     <asp:TemplateField HeaderText="Company name">
                                     <ItemTemplate>
                                     <asp:Label ID="lblCompanyName" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                                      <asp:Label ID="lblCompanyId" runat="server" Text='<%#Eval("fGPtbl_Company_Id") %>' Visible="false"></asp:Label>
                                     
                                     </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lblCompanyTotal" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                      </FooterTemplate>
                                     
                                     </asp:TemplateField>
                                        
                                          <asp:TemplateField >
                                            <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2" >Registration Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive of ServiceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                            <ItemTemplate>
                                             <asp:Label ID="lblregistration" runat="server" Text='<%#Eval("RegistrationAmmount") %>'></asp:Label>
                                            
                                            </ItemTemplate>
                                             <FooterTemplate>
                                             <asp:Label ID="lblTotalRegistration" runat="server" ></asp:Label>
                                             
                                             </FooterTemplate>
                                          
                                          </asp:TemplateField>
                                            
                                       
                                        <%--<asp:BoundField DataField="RegistrationAmmount" HeaderText="Registration Amount" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>--%>
                                            <asp:TemplateField>
                                             <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2"  >Inspection Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                              
                                             <ItemTemplate>
                                               <asp:Label ID="lblInspection" runat="server" Text='<%#Eval("InspectionAmmount")%>'></asp:Label>
                                             </ItemTemplate>
                                              <FooterTemplate>
                                               <asp:Label ID="lblinspectionTotal" runat="server"></asp:Label>
                                              
                                              </FooterTemplate>
                                           </asp:TemplateField>
                                           
                                         <asp:TemplateField HeaderText="Renewal Amount">
                                          <ItemTemplate>
                                           <asp:Label ID="lblrenewalamount" runat="server" Text='<%#Eval("RenewalAmount")%>'></asp:Label>
                                          </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lbl_renewal_footer" runat="server" Text=""></asp:Label>
                                         
                                         </FooterTemplate>
                                         </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Amendment Amount">
                                          <ItemTemplate>
                                           <asp:Label ID="lbl_amedment_amount" runat="server" Text='<%#Eval("AmedmentAmmount")%>'></asp:Label>
                                          </ItemTemplate>
                                             <FooterTemplate>
                                               <asp:Label ID="lbl_amedment_footer" runat="server"></asp:Label>
                                             
                                             </FooterTemplate>
                                         </asp:TemplateField>
                                      <%--  <asp:BoundField DataField="InspectionAmmount" HeaderText="Insepection Amount" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>--%>
                                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false"/>
                                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:MMM,dd,yyyy }" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false"/>
                                         <asp:TemplateField  HeaderText="Total Amount">
                                            <ItemTemplate>
                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%#Eval("TotalAmount")%>'></asp:Label>
                                            
                                            
                                            </ItemTemplate>
                                            <FooterTemplate>
                                              <asp:Label ID="lbltotalregistration_inspaction" runat="server"></asp:Label>
                                            </FooterTemplate>
                                         
                                         </asp:TemplateField>
                                 
                                 
                                    </Columns>
             
             </asp:GridView>
    </div>
                    </td>
                </tr>
               <tr>
                 <td>
                
                <asp:Label ID="lblid" runat="server" Text='' Visible="false"></asp:Label>
                 <asp:Label ID="lbldatefrom" runat="server" Visible="false" Text=""></asp:Label>
                  <asp:Label ID="lblto" runat="server" Text="" Visible="false"></asp:Label>
                
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
    
    <div  style="margin-top:20px;">
   <input type="button" id="btnPrint" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Company_wise.ClientID %>');window.print();"  />
    
    </div>
    </form>
</body>
</html>
