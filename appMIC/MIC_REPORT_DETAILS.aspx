<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/SCST.master" AutoEventWireup="true" CodeFile="MIC_REPORT_DETAILS.aspx.cs" Inherits="appMIC_MIC_REPORT_DETAILS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <style type="text/css">
          .text-label 
          {
            color: #ddf1f7;
            font-weight: bold;
           }  
           
           .grdserchkk
           {
           	  vertical-align:top;
           	  text-align:left;
           }
  </style>

    

    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              
         <table cellpadding="0" cellspacing="0" border="0"  width="100%">
             <tr valign="top" align="center">
                 <td style="padding:10px">
               <span style="color:green"><strong> Searching of SPR Database for a product/store </strong></span>      
                 </td>
             </tr>
            
           <tr>
                  <td align="center"valign="top" style="height:10px;" >
                       <table cellpadding="2" cellspacing="0" border="1" width="100%">
         <tr align="center">
            <td align="right" colspan="2"> Enter the Products</td>
            <td align="left" colspan="2"><asp:TextBox ID="txtProductName" runat="server" Width="200px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
            </td>
                </tr>

                           <tr>
            <td align="center" colspan="4"><asp:CheckBoxList ID="chkOption" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="1" Text="Whether SC-ST?"></asp:ListItem>
                <asp:ListItem Value="2" Text="Whether Women Entrepreneur?"></asp:ListItem>
                <asp:ListItem Value="3" Text="All SC-ST Units Only"></asp:ListItem>
                <asp:ListItem Value="4" Text="Whether Service?"></asp:ListItem>
                                           </asp:CheckBoxList> </td>
            
                </tr>
                           <tr>
            <td align="center" colspan="4"> 
                <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="update1_grid">
                             <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
                          
                          </asp:UpdateProgress>
                        <asp:UpdatePanel ID="update1_grid" runat="server" >
                          <ContentTemplate>
                          
                       
                           <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btnCls" 
                                onclick="Btn_Search_Click"  />
                                
                                
                                   
                          </ContentTemplate>
                        
                        </asp:UpdatePanel>
            </td>
            
                </tr>

            
                       

      
                  </table>
                  </td>
               </tr>
             
           <tr>
           
           
             <td align="left" valign= "bottom" style="padding-left:20px; width:100%; margin-top:10px;">
             <table cellpadding="0" cellspacing="0" border="0" width="90%">
               <tr>
                  <td align="left" valign="top" style="padding-left:20px;">
                    <b><span style="color:Red;"> <asp:Label ID="lbl_searchcompany" runat="server" Text=""></asp:Label> </b>
                  </td>
                  <td align="right" valign="top" style="padding-right:50px;">
                     <b><span style="color:Red;"><asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label></b>
                  
                  
                      </td>
               
               </tr>
               
                
               
             </table>
             
              
             
             </td>
           
           </tr>
            
             
              <%--<tr>
             <td align="center"valign="top" style="padding-right:20px; width:100%;" ></td>
            
             
             
           </tr>--%>
           <tr><td style="height:10px;"></td></tr>
            
             <tr>
               <td  valign="top" style="padding:10px">
                <asp:Panel runat ="server" ID = "pnl1">
                
                  <asp:UpdatePanel ID="update1_DgD_agencies" runat="server" >
                  <ContentTemplate>
                  
                

                 <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="500"
                Width="100%" BorderWidth="0" onpageindexchanging="grdserch_pageindex_changing" 
                       
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
                        <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("Products") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     
                 </Columns>
                </asp:GridView>   
                
                  </ContentTemplate>
                  </asp:UpdatePanel>
                </asp:Panel>        
               
               </td>
             
             </tr>
            
            <tr>
               <td   valign="top">
               <asp:Panel runat ="server" ID = "pnl_dg">
                 <asp:UpdatePanel ID="update1_Buying_Agencies" runat="server">
                 <ContentTemplate>
                 
                 
               
               
                 <asp:GridView ID="grd_search_result_dg" runat="server"  CellPadding="3" 
                       CellSpacing="3" OnPageIndexChanging="grdserch_pageindex_changing" 
                  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                Width="100%" BorderWidth="0px" 
                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" 
                       >
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
                   <asp:TemplateField HeaderText="Company/Validity" >  <%-- ControlStyle-Width="40%"--%>
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                    <%--  <br />  <asp:Label ID="lbl_validity_dg" runat="server" Text=""></asp:Label>--%>
                    
                      <table cellpadding="1" cellspacing="1" border="0" >
                      <tr>
                           <td align="left" valign="top">
                            <asp:Label ID="lbl_validity_dg" runat="server" Text=""></asp:Label> <%-- '<%#Eval("validity") %>'--%>
                           </td>
                           <tr>
                               <td align="left" valign="top">
                                 <i><asp:Label ID="lbl_validity_text_dg" runat="server" Text="Last Validity of Cerificate was upto" Visible="false"></asp:Label></i>
                                 <asp:Label ID="lbl_validity_text_show_dg" runat="server" Text=""></asp:Label>
                               </td>
                             
                             </tr>
                          <tr>
                               <td align="left" valign="top">
                                 <asp:Label ID="lbl_registratondate_dg" runat="server" Text=""  Visible="false"></asp:Label>  <%--'<%#Eval("RegistrationDate") %>'--%>
                                  <asp:Label ID="lbl_provisinol_fresh_dg" runat="server" Text="" Visible="false"></asp:Label><%--  '<%#Eval("fresh_provisional") %>'--%>
                                  <asp:Label ID="lbl_renewal_date_dg" runat="server" Text="" Visible="false"> </asp:Label> <%--'<%#Eval("RenewalDate_Check") %>'--%>
                               </td>
                             
                             </tr>
                         </table>
                     </ItemTemplate>
                       <ControlStyle Width="250px" />
                   </asp:TemplateField>
                 <%--  <asp:TemplateField HeaderText="Address"  ControlStyle-Width="300">
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                    </ItemTemplate>
                   
                       <ControlStyle Width="300px" />
                   
                   </asp:TemplateField>--%>
                     <asp:BoundField HeaderText="Registration date" DataField = "Registration_date" DataFormatString="{0:dd-MM-yyyy}" ControlStyle-Width="10%" />
                   <asp:TemplateField ControlStyle-Width="200" HeaderText="Registration NO">
                         <ItemTemplate>
                             <asp:Label ID="lbl_product" runat="server" 
                                 Text='<%#Eval("[Registration no]") %>'></asp:Label>
                         </ItemTemplate>
                         <ControlStyle Width="200px" />
                     </asp:TemplateField>
                      <asp:TemplateField  HeaderText="Store details">
                         <ItemTemplate>
                             <asp:Literal ID="lbl_store" runat="server" 
                                 Text='<%#Eval("[Store details]") %>'></asp:Literal>
                         </ItemTemplate>
                         <ControlStyle Width="150px" />
                     </asp:TemplateField>
                    <%--  <asp:TemplateField HeaderText="Validity" ControlStyle-Width="15%">
                      <ItemTemplate>
                        <asp:Label ID="lbl_validity" runat="server" Text='<%#Convert.ToDateTime(Eval("[Registration_date]")).ToString("dd/MM/yyyy")+ " -" +Convert.ToDateTime(Eval("ExpiryDate")).ToString("dd/MM/yyyy")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                 <%--   <asp:TemplateField  HeaderText="Registration No" ControlStyle-Width="10%">
                      <ItemTemplate>
                         <asp:Label ID="lbl_registration_no"  runat="server" Text='<%#Eval("[Registration no]") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                 <%--   <asp:TemplateField HeaderText="OldRegistration No" ControlStyle-Width="10%">
                       <ItemTemplate>
                         <asp:Label  ID="lbl_old_registration_no_dg" runat="server" Text='<%#Eval("OldRegistrationNumber") %>'></asp:Label>
                       </ItemTemplate>
                    </asp:TemplateField>--%>
                    <%--<asp:TemplateField HeaderText="Refrence Number" ControlStyle-Width="10%">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_number_dg" runat="server" Text='<%#Eval("RefrenceNumber") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="" ControlStyle-Width="10%" Visible="true">
                      <ItemTemplate>
                         <asp:LinkButton ID="lnkview_dg" runat="server" CommandName="View" CommandArgument='<%#Eval("CompanyId") %>'
                       CausesValidation="false" ToolTip="View">View</asp:LinkButton>&nbsp;
                       <asp:HiddenField ID="HiddenFieldCompanyIdview_dg" runat="server" Value='<%#Eval("CompanyId") %>' />
                     <%--   <asp:Label ID="lbl_company_status" runat="server" Text='<%#Eval("CompanyStatus")%>' Visible="false"></asp:Label>--%>
                      </ItemTemplate>
                    </asp:TemplateField>
                    
                   
                 </Columns>
                </asp:GridView>   
                
                  </ContentTemplate>
                 
                 </asp:UpdatePanel>        
               </asp:Panel>
               </td>
             
             </tr>
            
          <tr><td style="height:80px;">&nbsp;</td></tr>
              <tr><td style="height:80px;">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="update1_Buying_Agencies">
                             <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
                          
                          </asp:UpdateProgress>
              
              
              </td></tr>
              
               <tr><td style="height:80px;">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="update1_DgD_agencies">
                             <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
                          
                          </asp:UpdateProgress>
              
              
              </td></tr>
          
         </table>
       
        
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
