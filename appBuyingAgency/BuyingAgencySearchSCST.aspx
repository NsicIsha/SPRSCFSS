<%--<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyingAgencySearchSCST.aspx.cs" Inherits="appBuyingAgency_BuyingAgencySearchSCST" %>--%>
<%@ Page Language="C#" MasterPageFile="~/appMaster/BuyingAgency.master" AutoEventWireup="true" CodeFile="BuyingAgencySearchSCST.aspx.cs" Inherits="appBuyingAgency_BuyingAgencySearchSCST" Title="NSIC SPR::BUYING AGENCY SEARCH" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 



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
             <div  style="width:990px;">
         <table cellpadding="0" cellspacing="0" border="0" style="margin-top:10px;">
            
           <tr>
             <td  style="padding-left:100px; width:100%">
               <table  cellspacing="3" cellpadding="3" border="0">
                  <tr>
                  <td align="left" class="leftext">Search:</td>
                    <td style="width:2px;"></td>
                    <td align="left" class="coloncls">
                      <asp:DropDownList ID="ddlSearchCriteria" runat="server" AutoPostBack="false" CssClass="clsDDL" Width="220px">
                        <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Product" Value="2"></asp:ListItem>
         
                    <asp:ListItem Text="Registration No." Value="3"></asp:ListItem>
                         
                      </asp:DropDownList>
                    
                    </td>
                   <td style="width:2px;"></td>
                     <td align="left" class="coloncls">
                        <asp:TextBox ID="txt_serch_Criteria" runat="server" CssClass="clstxtbx" Width="220"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID = "txtw" runat ="server"  TargetControlID ="txt_serch_Criteria"
                         WatermarkText="Enter text to search" WatermarkCssClass ="text-label "></cc1:TextBoxWatermarkExtender>
                     </td>  
                      <td style="width:2px;"></td>
                        <td align="left" class="coloncls">
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
             <td align="center"valign="top" style="padding-right:20px; width:100%; height:10px;" ></td>
            
             
             
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
               
                <tr   style="height:10px;" ><td align="left"  style="white-space:nowrap;" colspan="2">

             <table   cellspacing="2" cellpadding="2" border="0" style="width:200px;" >
                  <tr  >
                    <td align="left">
                         <asp:Image ID="red_box" runat="server" ImageUrl="~/Images/redbutton.gif" Height="30" Width="30" ToolTip=" BlackList Company"  Visible="false"/> 
                    </td>
                      <td align="left"><b><asp:Label ID="lbl_blacklist_company" runat="server" Text =" BlackList Company" Visible="false" ></asp:Label></b>
                      </td>
                  </tr>
             </table>
            </td>
             </tr>
               
             </table>
             
              
             
             </td>
           
           </tr>
            
             
              <tr>
             <td align="center"valign="top" style="padding-right:20px; width:100%;" ></td>
            
             
             
           </tr>
           <tr><td style="height:10px;"></td></tr>
            
             <tr>
               <td  valign="top">
                <asp:Panel runat ="server" ID = "pnl1">
                
                  <asp:UpdatePanel ID="update1_DgD_agencies" runat="server" >
                  <ContentTemplate>
                  
                

                 <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="10"
                Width="100%" BorderWidth="0" onpageindexchanging="grdserch_pageindex_changing" 
                       onrowdatabound="grd_search_onrowdatabound" 
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
                 
                   <asp:TemplateField HeaderText="Company/Validity">   <%-- ControlStyle-Width="40%"--%>
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                     <br />
                 <%--   <asp:Label ID="lbl_validity" runat="server" Text=""></asp:Label>--%>
                   <table cellpadding="1" cellspacing="1" border="0" >
                      <tr>
                           <td align="left" valign="top">
                            <asp:Label ID="lbl_validity" runat="server" Text=""></asp:Label> <%-- '<%#Eval("validity") %>'--%>
                           </td>
                           <tr>
                               <td align="left" valign="top">
                                 <i><asp:Label ID="lbl_validity_text" runat="server" Text="Last Validity of Cerificate was upto" Visible="false"></asp:Label></i>
                                 <asp:Label ID="lbl_validity_text_show" runat="server" Text=""></asp:Label>
                               </td>
                             
                             </tr>
                          <tr>
                               <td align="left" valign="top">
                                 <asp:Label ID="lbl_registratondate" runat="server" Text=""  Visible="false"></asp:Label>  <%--'<%#Eval("RegistrationDate") %>'--%>
                                  <asp:Label ID="lbl_provisinol_fresh" runat="server" Text="" Visible="false"></asp:Label><%--  '<%#Eval("fresh_provisional") %>'--%>
                                  <asp:Label ID="lbl_renewal_date" runat="server" Text="" Visible="false"> </asp:Label> <%--'<%#Eval("RenewalDate_Check") %>'--%>
                               </td>
                             
                             </tr>
                         </table>
                     </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Location"  >  <%--ControlStyle-Width="20%"--%>
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                    </ItemTemplate>
                   
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product">
                      <ItemTemplate>
                      <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("Product_Description") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                   <%-- <asp:TemplateField HeaderText="Validity" ControlStyle-Width="10%">
                      <ItemTemplate>
                        <asp:Label ID="lbl_validity" runat="server" Text='<%#Convert.ToDateTime(Eval("RegistrationDate")).ToString("dd/MM/yyyy")+ " -" +Convert.ToDateTime(Eval("ExpiryDate")).ToString("dd/MM/yyyy")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Registration No" >  <%--ControlStyle-Width="10%"--%>
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("RegistrationNumber")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                 <%--   <asp:TemplateField HeaderText="Old Registration No" ControlStyle-Width="10%">
                      <ItemTemplate>
                      <asp:Label ID="lbl_old_registration_no" runat="server" Text='<%#Eval("OldRegistrationNumber") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                   <%-- <asp:TemplateField  HeaderText="Refrence Number" ControlStyle-Width="10%">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("RefrenceNumber") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                     <asp:TemplateField HeaderText=""   Visible="true">  <%--ControlStyle-Width="10%"--%>
                       <ItemTemplate>
                       
                      <asp:LinkButton ID="lnkview" runat="server" CommandName="View" CommandArgument='<%#Eval("CompanyId") %>'
                       CausesValidation="false" ToolTip="View" Text="View"></asp:LinkButton>&nbsp;
                       <asp:HiddenField ID="HiddenFieldCompanyIdview" runat="server" Value='<%#Eval("CompanyId") %>' />
                       <asp:HiddenField ID="HiddenFieldUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                       <asp:HiddenField ID="HiddenFieldBranch" runat="server" Value='<%#Eval("BranchName") %>' />
                  <asp:Label ID="lbl_company_status" runat="server" Text='<%#Eval("CompanyStatus") %>' Visible="false"></asp:Label>
                    
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
                       CellSpacing="3" OnPageIndexChanging="grd_search_result_dg_PageIndexChanging" 
                  AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True"
                Width="100%" BorderWidth="0px" 
                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" 
                       onrowdatabound="grd_search_result_dg_RowDataBound">
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
       
       </div>
       </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
