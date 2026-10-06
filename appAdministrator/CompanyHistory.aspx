<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyHistory.aspx.cs" Inherits="appAdministrator_CompanyHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" language="javascript">
      function validate() {
          var Ref_No = document.getElementById("<%=txt_Ref_no.ClientID %>").value;
         // alert(Ref_No);
          if (Ref_No == "" || Ref_No==null) {
              alert("Please Enter Ref No or Company Name");
              return false;
          }
        
      }
  
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

<table cellpadding="0" cellspacing="0" width="100%"  style="margin-top:10px;">
  
<tr>
            <td align="left"  colspan="3" class="headerAll" style="width: 170px;">
                 Company History Details
            </td>
        </tr>
        
       

<tr style="width:800px;">
  <td align="left" style="padding-left:4px; width:240px;"><b>Enter  Company Name or Ref Number </b> </td>
    <td align="left" style="width:475px;" colspan="2">
    <asp:DropDownList ID="ddl_list" runat="server" AutoPostBack="true" Width="150" 
            CssClass="clsDDL" onselectedindexchanged="ddl_list_SelectedIndexChanged">
      <asp:ListItem Selected="True" Value="1">PAN </asp:ListItem>
     <%-- <asp:ListItem  Value="2">Reference Number  </asp:ListItem>--%>
    </asp:DropDownList>
       
     <asp:TextBox ID="txt_Ref_no" runat="server" Text="" Width="220px"></asp:TextBox></td>

<td align="left"> &nbsp;

    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="update1_grid">
                             <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
                          
                          </asp:UpdateProgress>
                        <asp:UpdatePanel ID="update1_grid" runat="server" >
                          <ContentTemplate>
                          
                       <asp:Button ID="btn_status_track" runat="server" 
        Text="View Details" Width="120" CssClass="btnCls"  OnClientClick="javascript:return validate();" 
        onclick="btn_status_track_Click"  />
                           
                                
                                
                                   
                          </ContentTemplate>
                        
                        </asp:UpdatePanel>

</td>
   
</tr>
  
 
   

      <tr id="tbl_show_multiple_record_tr" runat="server" visible="false">
            <td colspan="4"  align="left" valign="top" style="padding-left:5px; padding-right:5px;" >
             <table border="1" id="tbl_show_multiple_record" cellpadding="0" cellspacing="0" width="100%" style="margin-top:20px" >
              
                
                  <tr>
                <td colspan="5" class="leftHeadingReport" align="left" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px">
                  <b>BASIC DETAILS:</b>
                </td>
               </tr>

                 
                  <tr align="center">
                       
                <td colspan="5" class="leftHeadingReport" align="center" style="font-size:20px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px">
               Current Status:  <b><asp:Label ID="lblCurrentStatus" Font-Size="20px" ForeColor="Green" runat="server"></asp:Label></b>
                </td>
               </tr>
                
              
                 <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     1.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblUnitName" runat="server" Text="Unit Name"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblUnitNameValue" runat="server" Text=""></asp:Label>
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblConstitution" runat="server" Text="Constitution"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblConstitutionValue" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>                 
                  <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     2.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblPlantMachinery" runat="server" Text="Plant & machinery (in Lakh)"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblPlantMachineryValue" runat="server" Text=""></asp:Label>
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblSocialCategory" runat="server" Text="Social Category"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblSocialCategoryValue" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>                  
                  <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     3.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblEnterprisecategory" runat="server" Text="Enterprise category"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblEnterprisecategoryValue" runat="server" Text=""></asp:Label>
                   
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblApplicationdate" runat="server" Text="Initial Application date"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblApplicationdateValue" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>
                 <tr align="left" valign="top">                   
                 <td align="left" valign="top" class="leftHeading">
                     4.
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblPan" runat="server" Text="PAN"></asp:Label></b>
                 </td>
                 <td align="left" valign="top" class="rightHeadingBottom">
                   <asp:Label ID="lblPanValue" runat="server" Text=""></asp:Label>
                 </td>
                     <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="lblUAM" runat="server" Text="UAM"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="lblUAMValue" runat="server" Text=""></asp:Label>
                   </td>
                 </tr>

                  
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">5. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblValidityRange" runat="server" Text="Validity Range"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblValidityRangeValue" runat="server" Text=""></asp:Label>
                          </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblMonetaryLimit" runat="server" Text="Monetary Limit (in Lakh)"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblMonetaryLimitValue" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">6. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblUnitSizeText" runat="server" Text="Unit Size"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblUnitSizes" runat="server" ></asp:Label>
                          </td>
                           
                      </tr>
                      <%-- <tr align="left" valign="top">
                       <td colspan="4" class="leftHeadingReport" align="left" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px">
                  <b>PAYMENT DETAILS:</b>
                </td>
                      <td colspan="4">
                            <asp:GridView ID="gvRegPaymentDetails" runat="server"  CellPadding="3" 
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
                 
                   <asp:TemplateField HeaderText="Amount Paid">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AmountPaid" runat="server" Text='<%#Eval("fGPtbl_PD_PaidAmount") %>'></asp:Label>
                     </ItemTemplate>
                       </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="Payment Type" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_PType" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField  HeaderText="Cheque/DD/UTR No.">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberCDUNo" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField  HeaderText="Bank Name">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBN" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField  HeaderText="Branch">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Payment Date">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Cheque/DD/NEFT Date ">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                </asp:GridView>   
                      </td>

                      </tr>--%>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:12px; font-family:Arial Baltic; font-weight:bold; padding-top:10px; padding-bottom:5px"><b>PAYMENT DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" colspan="5" valign="top">
                              <asp:GridView ID="gvRegPaymentDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="0" CellPadding="3" CellSpacing="3" class="grdserchkk" GridLines="Both" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" PageSize="500" Width="100%">
                                  <HeaderStyle CssClass="dynRowHeader" />
                                  <RowStyle CssClass="dynRowEven" />
                                  <AlternatingRowStyle CssClass="dynRowOdd" />
                                  <PagerStyle CssClass="Pagging" HorizontalAlign="Center" VerticalAlign="Middle" />
                                  <EmptyDataTemplate>
                                      <div align="center">
                                          <strong style="font-size:18px; font-weight:bold; color:Red;">No Data Found !!!</strong></div>
                                  </EmptyDataTemplate>
                                  <Columns>
                                      <asp:TemplateField HeaderText="S.NO">
                                          <ItemTemplate>
                                              <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount Paid">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_AmountPaid" runat="server" Text='<%#Eval("fGPtbl_PD_UnitAmount") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Type">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_registration_PType" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/UTR No.">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberCDUNo" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Bank Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBN" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Branch">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Date">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/NEFT Date ">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                              </asp:GridView>
                          </td>
                      </tr>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px"><b>RENEWAL DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">1. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblRenewalCOunt" runat="server" Text="Total Renewal Count"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblRenewalCOuntValue" runat="server" Text=""></asp:Label>
                          </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblProposalDate" runat="server" Text="Proposal Date"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblProposalDateValue" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:12px; font-family:Arial Baltic; font-weight:bold; padding-top:10px; padding-bottom:5px"><b>RENEWAL PAYMENT DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" colspan="5" valign="top">
                              <asp:GridView ID="gvRenewalPaymentDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="0" CellPadding="3" CellSpacing="3" class="grdserchkk" GridLines="Both" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" PageSize="500" Width="100%">
                                  <HeaderStyle CssClass="dynRowHeader" />
                                  <RowStyle CssClass="dynRowEven" />
                                  <AlternatingRowStyle CssClass="dynRowOdd" />
                                  <PagerStyle CssClass="Pagging" HorizontalAlign="Center" VerticalAlign="Middle" />
                                  <%-- <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>--%>
                                  <Columns>
                                      <asp:TemplateField HeaderText="S.NO">
                                          <ItemTemplate>
                                              <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount Paid">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_AmountPaidRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_UnitAmount") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Type">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_registration_PTypeRenewal" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/UTR No.">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberCDUNoRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Bank Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBNRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Branch">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranchRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Date">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/NEFT Date ">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranchRenewal" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                              </asp:GridView>
                          </td>
                      </tr>
                      <%-- <tr align="left" valign="top">
                     
                      <td colspan="5">
                            <asp:GridView ID="gvRenewalPaymentDetails" runat="server"  CellPadding="3" 
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
                 
                   <asp:TemplateField HeaderText="Amount Paid">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AmountPaidRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_PaidAmount") %>'></asp:Label>
                     </ItemTemplate>
                       </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="Payment Type" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_PTypeRenewal" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField  HeaderText="Cheque/DD/UTR No.">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberCDUNoRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField  HeaderText="Bank Name">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBNRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField  HeaderText="Branch">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranchRenewal" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Payment Date">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Cheque/DD/NEFT Date ">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranchRenewal" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                </asp:GridView>   
                      </td>

                      </tr>--%>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px"><b>AMENDMENT DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">1. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblAmendmentCount" runat="server" Text="Total Amendment Count"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblAmendmentCountValue" runat="server" Text=""></asp:Label>
                          </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblAmendmentPDate" runat="server" Text="Proposal Date"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblAmendmentPDateValue" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:12px; font-family:Arial Baltic; font-weight:bold; padding-top:10px; padding-bottom:5px"><b>AMENDMENT PAYMENT DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" colspan="5" valign="top">
                              <asp:GridView ID="gvAmendmentPaymentDetails" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="0" CellPadding="3" CellSpacing="3" class="grdserchkk" GridLines="Both" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" PageSize="500" Width="100%">
                                  <HeaderStyle CssClass="dynRowHeader" />
                                  <RowStyle CssClass="dynRowEven" />
                                  <AlternatingRowStyle CssClass="dynRowOdd" />
                                  <PagerStyle CssClass="Pagging" HorizontalAlign="Center" VerticalAlign="Middle" />
                                  <%--<EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>--%>
                                  <Columns>
                                      <asp:TemplateField HeaderText="S.NO">
                                          <ItemTemplate>
                                              <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Amount Paid">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_AmountPaidAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_UnitAmount") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Type">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_registration_PTypeAMendment" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/UTR No.">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberCDUNoAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Bank Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBNAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Branch">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Payment Date">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Cheque/DD/NEFT Date ">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                              </asp:GridView>
                          </td>
                      </tr>
                      <%--   <tr align="left" valign="top">
                   
                      <td colspan="5">
                            <asp:GridView ID="gvAmendmentPaymentDetails" runat="server"  CellPadding="3" 
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
                 
                   <asp:TemplateField HeaderText="Amount Paid">  
                     <ItemTemplate>
                     <asp:Label ID="lbl_AmountPaidAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_PaidAmount") %>'></asp:Label>
                     </ItemTemplate>
                       </asp:TemplateField>
                 
                    <asp:TemplateField HeaderText="Payment Type" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_PTypeAMendment" runat="server" Text='<%#Eval("PaymentType")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField  HeaderText="Cheque/DD/UTR No.">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberCDUNoAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_ChequeDDNo") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                       <asp:TemplateField  HeaderText="Bank Name">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBNAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_BankName") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField  HeaderText="Branch">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("fGPtbl_PD_BankBranch") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Payment Date">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("Payment Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                     <asp:TemplateField  HeaderText="Cheque/DD/NEFT Date ">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberBranchAMendment" runat="server" Text='<%#Eval("Cheque/DD Date") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                </asp:GridView>   
                      </td>

                      </tr>--%>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px"><b>INSPECTION AGENCY DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">1. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblAgencyName" runat="server" Text="Agency Name"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblAgencyNameValue" runat="server" Text=""></asp:Label>
                          </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblInspectionAllotmentDate" runat="server" Text="Inspection Allotment Date"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblInspectionAllotmentDateValue" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">2. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblInspectionPassedDate" runat="server" Text="Inspection Passed Date"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblInspectionPassedDateValue" runat="server" Text=""></asp:Label>
                          </td>
                          <%--   <td align="left" valign="top" class="rightHeadingBottom">
                  <b><asp:Label ID="Label4" runat="server" Text="Inspection Allotment Date"></asp:Label></b>
                 </td>
                   <td align="left" valign="top" class="rightHeadingBottom">
                      <asp:Label ID="Label5" runat="server" Text=""></asp:Label>
                   </td>--%>
                      </tr>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px"><b>SHIFTING DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" valign="top">1. </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblFromBranch" runat="server" Text="From Branch"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblFromBranchValue" runat="server" Text=""></asp:Label>
                          </td>
                          <td align="left" class="rightHeadingBottom" valign="top"><b>
                              <asp:Label ID="lblToBranch" runat="server" Text="To Branch"></asp:Label>
                              </b></td>
                          <td align="left" class="rightHeadingBottom" valign="top">
                              <asp:Label ID="lblToBranchValue" runat="server" Text=""></asp:Label>
                          </td>
                      </tr>
                      <tr>
                          <td align="left" class="leftHeadingReport" colspan="5" style="font-size:15px; font-family:Arial Baltic; font-weight:bold; padding-top:25px; padding-bottom:15px"><b>PRODUCT DETAILS:</b> </td>
                      </tr>
                      <tr align="left" valign="top">
                          <td align="left" class="leftHeading" colspan="5" valign="top">
                              <asp:GridView ID="grd_search_result" runat="server" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" BorderWidth="0" CellPadding="3" CellSpacing="3" class="grdserchkk" GridLines="Both" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" PageSize="500" Width="100%">
                                  <HeaderStyle CssClass="dynRowHeader" />
                                  <RowStyle CssClass="dynRowEven" />
                                  <AlternatingRowStyle CssClass="dynRowOdd" />
                                  <PagerStyle CssClass="Pagging" HorizontalAlign="Center" VerticalAlign="Middle" />
                                  <EmptyDataTemplate>
                                      <div align="center">
                                          <strong style="font-size:18px; font-weight:bold; color:Red;">No Data Found !!!</strong></div>
                                  </EmptyDataTemplate>
                                  <Columns>
                                      <asp:TemplateField HeaderText="S.NO">
                                          <ItemTemplate>
                                              <%#Container.DataItemIndex+1 %>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Company Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="HS Code/NPC Code">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("HSCode")%>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Product">
                                          <ItemTemplate>
                                              <asp:Label ID="lbl_refrence_numberAP" runat="server" Text='<%#Eval("Product") %>'></asp:Label>
                                          </ItemTemplate>
                                      </asp:TemplateField>
                                  </Columns>
                              </asp:GridView>
                          </td>
                      </tr>
                  </caption>
                         
             </table>
            </td>
        </tr>
        
       
   
   
  

</table>


   </ContentTemplate>
                  </asp:UpdatePanel>

</asp:Content>

