<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BranchWiseSearch.aspx.cs" Inherits="appAdministrator_BranchWiseSearch" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<%@Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>


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
    <title>BrancWise Page</title>
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
                <tr>
                   
                    <td colspan="2" style="vertical-align: top; width: 100%;"> 
                      <table  id="tbl_button" width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
                        <tr> 
                          <td align="left">  <input type="button" id="btnback" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>
                          
                           <%--<asp:LinkButton ID="lnkBack" runat="server" Text="BACK" onclick="lnkBack_Click"></asp:LinkButton>--%></td>
                           <td align="right"><input type="button" id="Button1" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Branch_Wise.ClientID %>');window.print();"  /></td>
                        </tr>
                      
                      </table>
                        
                     <div id="Div ReportName"  style="margin-left:250px; font-size:25px; font-family:Arial Baltic; font-weight:bold; white-space:nowrap;"> Report:&nbsp;<b style="font-size:15px;">Zone Name:</b> &nbsp;<asp:Label ID="lblReportName" runat="server" Text=""></asp:Label>
                     </div> 
           
    <div   style="vertical-align:middle; width:100%;  margin-right :0px; page-break-after:always;" id="div_show">
       <asp:GridView ID="grd_Branch_Wise" runat="server"  AutoGenerateColumns="False"
             EnableViewState="False" AllowPaging="false"  Width="100%"  ShowFooter="true"
                                    BorderWidth="1" CellPadding="10" CellSpacing="1" 
                      onrowdatabound="grd_GridCompany_onRowdataBound"  PageSize="10"  
            Visible="true" 
                       >    <%--ondatabound="grdbranchWise_Data" --%>
                      
                         <HeaderStyle CssClass="dynRowHeader2" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    
                              <Columns>  
                            
                                  
                            <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                     
                                     <asp:Label ID="lnk_Branc_Name" runat="server" Text='<%#Eval("BranchName")%>'  Visible="true" ></asp:Label>
                                    <asp:Label ID="lbl_Branch_ID" runat="server" Text='<%#Eval("BranchID")%>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                     <FooterTemplate>
                                       <asp:Label ID="lblBranchNameTotal" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                     </FooterTemplate>
                                      
                                    </asp:TemplateField>
                                 <%--  <asp:BoundField DataField="RegistrationAmount" HeaderText="Registration Amount" 
                                          ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"  
                                          ItemStyle-Font-Size="14px"  >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField> --%>
                                      
                                          <asp:TemplateField>
                                          <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2" >Registration Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive Of  ServiceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                            <ItemTemplate>
                                              <asp:Label ID="lblregistrationAmmount" runat="server" Text='<%#Eval("RegistrationAmount") %>'></asp:Label>
                                          </ItemTemplate>
                                             <FooterTemplate>
                                              <asp:Label ID="lblRegistrationTotal" runat="server"></asp:Label>
                                           </FooterTemplate>
                                        </asp:TemplateField>
                                   
                                    <%--<asp:BoundField DataField="InspectionAmount" HeaderText="Inspection Amount" 
                                          ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"  
                                          ItemStyle-Font-Size="14px"  >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>  --%>
                                      
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
                                          
                                            <asp:Label ID="lblinspectionammount" runat="server" Text='<%#Eval("InspectionAmount") %>'></asp:Label>
                                          
                                          </ItemTemplate>
                                              <FooterTemplate>
                                              <asp:Label ID="lbltotalinspection" runat="server"></asp:Label>
                                            </FooterTemplate>
                                         
                                        
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderText="Renewal Amount">
                                        <ItemTemplate>
                                          <asp:Label ID="lbl_Renewal" runat="server" Text='<%#Eval("RenewalAmount") %>'></asp:Label>
                                        </ItemTemplate>
                                        
                                        <FooterTemplate>
                                        <asp:Label ID="lbl_renewal_total" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField  headertext="Amendment Amount">
                                       <ItemTemplate>
                                         <asp:Label ID="lbl_amedment" runat="server" Text='<%#Eval("AmedmentAmount") %>'></asp:Label>
                                       
                                       </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lbl_footer_amedment" runat="server" Text=""></asp:Label>
                                        
                                        </FooterTemplate>
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField  headertext="Total Amount">
                                      <ItemTemplate>
                                      <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>
                                      
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
    
     <div style="margin-top:20px;">
       <table width="700px" border="0" cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;" align="center">
              <tr>
                   <td colspan="2" align="center">
                   <input type="button" id="btnPrint" value="Print"  class="noPrint" runat="server"  onclick="javascript:AddTHEAD('<%=grd_Branch_Wise.ClientID %>');window.print();"  />
                  
                   </td>
                
                </tr>
                </table>
     </div>
    </form>
</body>
</html>
