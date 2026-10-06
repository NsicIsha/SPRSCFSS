<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Reportsearch.aspx.cs"  EnableViewState ="false" Inherits="appAdministrator_Reportsearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
          .text-label 
          {
            color: #ddf1f7;
            font-weight: bold;
           }  
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div  > <%--style="width:990px;"--%>
         <table cellpadding="0" cellspacing="0" border="0" style="margin-top:10px;">
            
          <tr>
            <td align="left" class="headerAll" style="width: 170px;">
                Advance Search Report
            </td>
        </tr>
        <tr><td style="height:10px;"></td></tr>
          
          
           <tr>
             <td  style="padding-left:100px;">
               <table  cellspacing="3" cellpadding="3" border="0">
                  <tr>
                  <td align="left" class="leftext">Search:</td>
                    <td style="width:2px;"></td>
                    <td align="left" class="coloncls">
                      <asp:DropDownList ID="ddlSearchCriteria" runat="server" AutoPostBack="false" CssClass="clsDDL" Width="220px">
                        <asp:ListItem Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Product" Selected="True"  Value="2"></asp:ListItem>
         
                    <asp:ListItem Text="Registration No." Value="3"></asp:ListItem>
                    <asp:ListItem Text="HS/NPC Code" Value="4"></asp:ListItem>
                         
                      </asp:DropDownList>
                    
                    </td>
                   <td style="width:2px;"></td>
                     <td align="left" class="coloncls">
                        <asp:TextBox ID="txt_serch_Criteria" runat="server" CssClass="clstxtbx" Width="220"></asp:TextBox>
                        <cc1:TextBoxWatermarkExtender ID = "txtw" runat ="server"  TargetControlID ="txt_serch_Criteria"
                         WatermarkText="Enter text to search" WatermarkCssClass ="text-label "></cc1:TextBoxWatermarkExtender>
                     </td>  
                      <td style="width:2px;">
                         <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
                      
                      </td>
                        <td align="left" class="coloncls">
                         <asp:UpdatePanel ID="Update_bton"  runat="server" >
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
            
             <tr><td style="height:10px;"></td></tr>
             <tr><td align="right" valign="middle" style="padding-right:20px;">
             
                <b><span style="color:Red;"><asp:Label ID="lbl_company_count" runat="server" Text=""></asp:Label></b>
                 </td>
                 </tr>
                 <tr>
                   <td align="left" valign="top" style="padding-left:20px;">
                     <b> <span style="color:Red;"> <asp:Label ID="lbl_search_company" runat="server" Text=""></asp:Label> </b>
                   </td>
                 
                 </tr>
             
              <tr><td style="height:10px;"></td></tr>
             
             <tr>
               <td   align="center" valign="top" style="padding-left:20px;">
                <%--<asp:Panel runat ="server" ID = "pnl1">--%>
                
                <asp:UpdatePanel ID="update_panel_grid" runat="server"  UpdateMode="Conditional" >
                <ContentTemplate>
               
                
                <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both"  PageIndex="0" EnableViewState="false" EnableSortingAndPagingCallbacks="true"
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="20"
                Width="100%" BorderWidth="0" onpageindexchanging="grdserch_pageindex_changing" 
                       onrowdatabound="grd_search_onrowdatabound" 
                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                  <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven"  VerticalAlign="Top"/>
                   <AlternatingRowStyle CssClass="dynRowOdd" VerticalAlign="Top"/>
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
                 
                 
                   <asp:TemplateField HeaderText="Company/Reg No/Validity"  ControlStyle-Width="200">
                     <ItemTemplate>
                     <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                     <br />
                         <table cellpadding="2" cellspacing="2" border="0">
                      <tr>
                           <td align="left" valign="top">
                            <asp:Label ID="lbl_validity" runat="server" Text=""></asp:Label> <%-- '<%#Eval("validity") %>'--%>
                           </td>
                           <tr>
                               <td align="left" valign="top">
                                <i> <asp:Label ID="lbl_validity_text" runat="server" Text="Last Validity of Cerificate was upto " Visible="false"></asp:Label></i>
                                 <asp:Label ID="lbl_validity_text_show" runat="server" Text=""></asp:Label>
                               </td>
                             
                             </tr>
                             <tr>
                                  <td align="left" valign="top">
                                      <asp:Label ID="lbl_hs_code_text" runat="server" Text='<%#Eval("HSCode") %>'></asp:Label>
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
<%--                       <asp:Label ID="lbl_validity" runat="server" Text=""></asp:Label>--%>
                       
                     </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Location"  ControlStyle-Width="200">
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                    </ItemTemplate>
                   
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product" ControlStyle-Width="200">
                      <ItemTemplate>
                      <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("Product_Description") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="">
                       <ItemTemplate>
                      <asp:LinkButton ID="lnkview" runat="server" CommandName="View" CommandArgument='<%#Eval("CompanyId") %>'
                       CausesValidation="false" ToolTip="View">View</asp:LinkButton>&nbsp;
                       <asp:HiddenField ID="HiddenFieldCompanyIdview" runat="server" Value='<%#Eval("CompanyId") %>' />
                       <asp:HiddenField ID="HiddenFieldUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                       <asp:HiddenField ID="HiddenFieldBranch" runat="server" Value='<%#Eval("BranchName") %>' />
                      </ItemTemplate>
                      
                     </asp:TemplateField>
                 </Columns>
                </asp:GridView>  
                
                 </ContentTemplate>
                </asp:UpdatePanel>
                
                <br />
                 <asp:UpdateProgress ID="Update_grid"
        AssociatedUpdatePanelID="update_panel_grid"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
                
              <%--  </asp:Panel>    --%>    
               
               </td>
             
             </tr>
            
          
            
          <tr><td style="height:80px;">&nbsp;</td></tr>
         </table>
       
       </div>
       </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

