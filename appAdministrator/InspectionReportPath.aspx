<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="InspectionReportPath.aspx.cs" Inherits="appAdministrator_InspectionReportPath" Title="Inspection Report" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="2" cellspacing="2" border="0" width="100%">
    <tr>
      <td align="left" valign="top"  class="headerAll">
     Inspection Report:
      
      </td>
    
    </tr>
    
      <tr>
        <td  style="height:10px;" >


            <table style="width: 100%">

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">From Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:TextBox ID="txt_fromdate" runat="server" Text=""></asp:TextBox>
                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                                Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                ClearTextOnInvalid="true">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                                InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

                        </td>

                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">To Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                             <asp:TextBox ID="txt_todate" runat="server" Text="" ></asp:TextBox>

                <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

                        </td>

                    </tr>

                <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">Inspection Agency:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddl_InspectionAgency" runat="server" AutoPostBack="false"
                                TabIndex="52" ValidationGroup="1">
                            </asp:DropDownList>

                        </td>
                    </tr>

                 <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading"></td>
                        <td align="left" valign="top">
                             <asp:UpdatePanel ID="Update_bton"  runat="server" >
                        <ContentTemplate>
                    <asp:Button ID="btn_search" runat="server" Text="Search" 
           OnClientClick="javascript:return validdatefrom();"       onclick="btn_search_Click" CausesValidation="false" CssClass="btnCls" />
          
                  
                  </ContentTemplate>
                  </asp:UpdatePanel>
                  
                     <br />
                     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
        
                   
                            
                        </td>
                    </tr>

                 <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">&nbsp;</td>
                        <td align="left" valign="top">
                             <asp:Button ID="btn_Excel" runat="server" Text="Generate Excel"
                                  OnClick="btn_Excel_Click" CausesValidation="false"/>
        
                   
                            
                        </td>
                    </tr>
                </table>


        </td>
         </tr>
         
           <tr>
             <td style="width:100%; padding-left:2px;">
               <asp:GridView ID="grd_inspection" runat="server" CellPadding="5"   CellSpacing="5"
               
               GridLines="Both"  PageIndex="0" EnableViewState="false" EnableSortingAndPagingCallbacks="true"
                  AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" 
                    Width="100%" > <%-- onpageindexchanging="grd_inspection_PageIndexChanging"--%>
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
                    
                      <asp:TemplateField HeaderText="Companyname/UnitAddress">
                       <ItemTemplate>
                       <asp:Label ID="lbl_company" runat="server" Text='<%#Eval("CompanyUnitAddress") %>'></asp:Label>
                       
                       </ItemTemplate>
                      
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Zone/Branch">
                        <ItemTemplate>
                        <asp:Label ID="lbl_zone_branch" runat="server" Text='<%#Eval("zonebranch") %>'></asp:Label>
                        
                        </ItemTemplate>
                      
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="StatusDate">
                        <ItemTemplate>
                          <asp:Label ID="lbl_username_address" runat="server" Text='<%#Eval("UserNameStatusDate") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="Type">
                        <ItemTemplate>
                          <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="Inspection Agency/UserName">
                        <ItemTemplate>
                          <asp:Label ID="lbl_inspection_agency" runat="server" Text='<%#Eval("InspectionAgencyUserName") %>'></asp:Label>
                        
                        </ItemTemplate>
                      
                      </asp:TemplateField>


                      <asp:TemplateField HeaderText="No. of Days Pendency">
                        <ItemTemplate>
                          <asp:Label ID="lbl_PendencyDays" runat="server" Text='<%# SetDays(Eval("UserNameStatusDate").ToString()) %>'></asp:Label>
                        
                        </ItemTemplate>
                      
                      </asp:TemplateField>


                 </Columns>
                
                  
               </asp:GridView>
             
             </td>
           
           </tr>
        <tr>

            <td align="right">

                 <asp:Button ID="btnSendMail" runat="server" Text="Send Notification Mail to Pending Commanies (More Then 30 Days)" 
            CssClass="btnCls" OnClick="btnSendMail_Click" Width="474px" />
          
                  

            </td>
        </tr>
  
  </table>


</asp:Content>

