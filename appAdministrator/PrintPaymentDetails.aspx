<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintPaymentDetails.aspx.cs" Inherits="appAdministrator_PrintPaymentDetails" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
--%>
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

    <title>Print Preview Page</title>
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
            
            
             <asp:GridView ID="grd_report_paymentDetails" runat="server"      AutoGenerateColumns="False"
             EnableViewState="False" AllowPaging="false"  Width="100%" ShowFooter="true"
                                    BorderWidth="1" CellPadding="10" CellSpacing="1" 
                      onrowdatabound="grd_Report_onRowdataBound"  PageSize="10"  Visible="true" 
                      onrowcommand="grd_report_onRowCommand" 
                      ondatabound="grd_report_paymentDetails_DataBound">
                      
                            <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                     
                                  <Columns>
                                 <asp:TemplateField HeaderText="Company Name">
                                      <ItemTemplate>
                                      <asp:Label ID="lblcomapnyName" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                                        <asp:Label ID="lblCompanyID" runat="server" Text='<%#Eval("fGPtbl_Company_Id") %>' Visible="false"></asp:Label>
                                      </ItemTemplate>
                                       
                                           <FooterTemplate>
                                                  <asp:Label ID="lbltotalComapny" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                       </FooterTemplate>
                                      </asp:TemplateField>
                                 
                                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode"  
                                          HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" 
                                          ItemStyle-Font-Size="14px"  Visible="false">
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>
                                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" 
                                          ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"  
                                          ItemStyle-Font-Size="14px"  Visible="false" >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>
                                     
                                     
                                       <asp:TemplateField>
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
                                           <asp:Label ID="lblRegiatrationAmmount" runat="server" Text='<%#Eval("RegistrationAmount")%>'></asp:Label>
                                          
                                          </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="lblRegistrationTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                       </asp:TemplateField>
                                      
                                        
                                     <asp:TemplateField>
                                     <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2"  >Insepection Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                       <ItemTemplate>
                                         <asp:Label ID="lbl_unit_ammount" runat="server" Text='<%#Eval("InspectionAmount")%>'></asp:Label>
                                          
                                       </ItemTemplate>
                                       
                                                 
                                          
                                     <FooterTemplate>
                                <asp:Label ID="lblTotal" Text="" runat="server" />
                            </FooterTemplate>
                                     </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderText="AmedmentAmount">
                                           <ItemTemplate>
                                              <asp:Label ID="lblAmedmentamount" runat="server" Text='<%#Eval("AmedmentAmount")%>'></asp:Label>
                                           </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lbl_amendment_total" runat="server" Text=""></asp:Label>
                                         
                                         </FooterTemplate>
                                       </asp:TemplateField>
                                         <asp:TemplateField HeaderText="RenewalAmount">
                                           <ItemTemplate>
                                             <asp:Label ID="lblRenewalAmount" runat="server" Text='<%#Eval("RenewalAmount")%>'></asp:Label>
                                           
                                           </ItemTemplate>
                                          <FooterTemplate>
                                            <asp:Label ID="lbl_renewal_amountFooter" runat="server" Text=""></asp:Label> 
                                          
                                          </FooterTemplate>
                                         </asp:TemplateField>
                                   
                                    <asp:TemplateField  HeaderText="Total Amount">
                                              <ItemTemplate>
                                              <asp:Label ID="lbltotalamount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                              
                                              </ItemTemplate>
                                               <FooterTemplate>
                                                 <asp:Label ID="lbltotalfooter" runat="server" Text=""></asp:Label>
                                               
                                               </FooterTemplate>
                                    
                                    </asp:TemplateField>
                                    
                                      
                                    
                                  </Columns> 
                                    
                      
                      
                      
                      
                      </asp:GridView>
                      
                      <asp:GridView ID="grd_Branch_Wise" runat="server"  AutoGenerateColumns="False"
             EnableViewState="False" AllowPaging="false"  Width="100%" ShowFooter="true"
                                    BorderWidth="1" CellPadding="10" CellSpacing="1"  
                      onrowdatabound="grd_GridCompany_onRowdataBound"  PageSize="10"  Visible="true" 
                      onrowcommand="onRowGrid_RowCommand" 
                      ondatabound="grdBranch_wise_dataBound" >
                      
                         <HeaderStyle CssClass="dynRowHeader1" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    
                              <Columns>  
                              <asp:TemplateField   HeaderText="Zone Name">
                                <ItemTemplate>
                                
                                 <asp:Label ID="lnkZone_Name" runat="server" Text='<%#Eval("ZoneName") %>' Visible="true"></asp:Label>
                                <asp:Label ID="lnkZone_ID" runat="server" Text='<%#Eval("ZoneID") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                    <asp:Label ID="lblZoneTotalAmount" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                 </FooterTemplate>
                              </asp:TemplateField>
                                  
                            <asp:TemplateField HeaderText="Branch Name">
                                    <ItemTemplate>
                                     
                                     <asp:Label ID="lnk_Branc_Name" runat="server" Text='<%#Eval("BranchName")%>'  Visible="true" ></asp:Label>
                                    <asp:Label ID="lbl_Branch_ID" runat="server" Text='<%#Eval("BranchID") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                      <FooterTemplate>
                                       <asp:Label ID="lblTotalBranchName" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                      
                                      </FooterTemplate>
                                    </asp:TemplateField>
                                    
                                   
                                
                                      
                                       <asp:TemplateField>
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
                                           <asp:Label ID="lblRegiatrationAmmount1" runat="server" Text='<%#Eval("RegistrationAmount")%>'></asp:Label>
                                          
                                          </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="lblRegistrationTotal1" runat="server"></asp:Label>
                                        </FooterTemplate>
                                       </asp:TemplateField> 
                                      
                                      
                                 
                                      
                                       <asp:TemplateField >
                                       <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2"  >Insepection Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                       <ItemTemplate>
                                         <asp:Label ID="lbl_unit_ammount1" runat="server" Text='<%#Eval("InspectionAmount")%>'></asp:Label>
                                          
                                       </ItemTemplate>
                                       
                                                 
                                          
                                     <FooterTemplate>
                                <asp:Label ID="lblTotal1" Text="" runat="server" />
                            </FooterTemplate>
                                     </asp:TemplateField>
                                      
                                        <asp:TemplateField  HeaderText="Renewal Amount">
                                        <ItemTemplate>
                                          <asp:Label ID="lbl_Renewal" runat="server" Text='<%#Eval("RenewalAmount")%>'></asp:Label>
                                        </ItemTemplate>
                                        
                                          <FooterTemplate>
                                             <asp:Label ID="lbl_footer_renewal" runat="server"></asp:Label>
                                          
                                          </FooterTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField  headertext="Amedment Amount">
                                       <ItemTemplate>
                                         <asp:Label ID="lbl_amedment" runat="server" Text='<%#Eval("AmedmentAmount")%>'></asp:Label>
                                       
                                       </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lbl_amedment_footer" runat="server"></asp:Label>
                                         
                                         </FooterTemplate>
                                     
                                     </asp:TemplateField>
                                     
                                     <asp:TemplateField  HeaderText="Total Amount">
                                     <ItemTemplate>
                                     <asp:Label ID="lblTotalAmmount" runat="server" Text='<%#Eval("TotalAmmount") %>'></asp:Label>
                                     
                                     </ItemTemplate>
                                     <FooterTemplate>
                                            <asp:Label ID="lblTotalAmmountbranchwise" runat="server"></asp:Label>
                                            
                                          </FooterTemplate>
                                     
                                     </asp:TemplateField>
                                     
                                
                      
                      
                      </Columns>
                      </asp:GridView>
                      
                      
                      <%-- for Design If select Only Branch And Zone ID----%>
                      
                        <asp:GridView ID="grd_Company_wiseData" runat="server"  
                      AutoGenerateColumns="false"  Width="100%"
             EnableViewState="False"  AllowPrintPaging="true"   UseAccessibleHeader="true"
                                    BorderWidth="1" CellPadding="5" CellSpacing="1"    
                      Visible="true"  ShowFooter="true" 
                     PageSize="4" onrowdatabound="grdCompany_Wise_Data" 
                      ondatabound="grdcompany_branchDatabound" >
                                     <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />
                                     
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <HeaderStyle CssClass="dynRowHeader1" />
                    
                                         
             
              <Columns>
                               
                         <asp:TemplateField HeaderText="Comapny Name">
                         <ItemTemplate>
                         <asp:Label ID="LBl_company_Name_Data" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                           <asp:Label ID="LBL_Company_Id_Data" runat="server" Text='<%#Eval("fGPtbl_Company_Id")%>' Visible="false"></asp:Label>
                         </ItemTemplate>
                          <FooterTemplate>
                            <asp:Label ID="lbltotalCompany2" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                          
                          </FooterTemplate>
                         </asp:TemplateField>              
                                      
                                         <asp:TemplateField>
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
                                           <asp:Label ID="lblRegiatrationAmmount2" runat="server" Text='<%#Eval("RegistrationAmmount")%>'></asp:Label>
                                          
                                          </ItemTemplate>
                                        <FooterTemplate>
                                          <asp:Label ID="lblRegistrationTotal2" runat="server"></asp:Label>
                                        </FooterTemplate>
                                       </asp:TemplateField> 
                                        
                                       
                                         
    <asp:TemplateField>
     <HeaderTemplate>
                                               <table cellpadding="0" cellspacing="0" border="0">
                                                <tr>
                                                 <td class="dynRowHeader2"  >Insepection Amount</td>
                                                </tr>
                                                  <tr> 
                                                    <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                  </tr>
                                               </table>
                                           </HeaderTemplate>
                                       <ItemTemplate>
                                         <asp:Label ID="lbl_unit_ammount2" runat="server" Text='<%#Eval("InspectionAmmount")%>'></asp:Label>
                                          
                                       </ItemTemplate>
                                        <FooterTemplate>
                                <asp:Label ID="lblTotal2" Text="" runat="server" />
                            </FooterTemplate>
                                     </asp:TemplateField>
                                        <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false"/>
                                        <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:MMM,dd,yyyy }" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false"/>
                                 <asp:TemplateField HeaderText="Amedment Amount">
                                    
                                         <ItemTemplate>
                                         
                                            <asp:Label ID="lbl_amedment_amount" runat="server" Text='<%#Eval("AmedmentAmmount") %>'></asp:Label>
                                                                                  </ItemTemplate>
                                              <FooterTemplate>
                                                <asp:Label ID="lbl_footer_amedment" runat="server"></asp:Label>
                                              </FooterTemplate>                                     
                                    
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Renewal Amount">
                                         <ItemTemplate>
                                           <asp:Label ID="lbl_renewal" runat="server" Text='<%#Eval("RenewalAmount") %>'></asp:Label>
                                           
                                         </ItemTemplate>
                                      <FooterTemplate>
                                        <asp:Label ID="lbl_footer_renewal" runat="server" ></asp:Label>
                                      
                                      </FooterTemplate>
                                      </asp:TemplateField>
                                 <asp:TemplateField  HeaderText=" Total Ammount">
                                 
                                 <ItemTemplate>
                                   <asp:Label ID="lbl_total_amount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                 
                                 </ItemTemplate>
                                   <FooterTemplate>
                                   
                                     <asp:Label ID="lblTotal_companywise" runat="server"></asp:Label>
                                   
                                   </FooterTemplate>
                                 
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
