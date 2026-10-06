<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="StateWiseCompanyRecord.aspx.cs" Inherits="appAdministrator_StateWiseCompanyRecord" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
        <asp:Label ID="lblDeleteCompany" runat="server" 
            Text="State Wise Company List" CssClass="headerAll"></asp:Label>
    </div>
    
     <div>
       <table cellpadding="2" cellspacing="2" border="0">
         <tr>
            <td align="left" > From date:</td>
            <td align="left"><asp:TextBox ID="txt_from_date" runat="server" Text=""></asp:TextBox> 
             <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
             TargetControlID="txt_from_date" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                    </asp:MaskedEditExtender>
                   <%-- <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_from_date" ControlExtender="MaskedEditExtender1"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>--%>
                  
             </td>
            <td align="left"> To date:</td>
            <td align="left"><asp:TextBox ID="txt_todate" runat="server" Text=""></asp:TextBox>
             <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                    </asp:MaskedEditExtender>
                  <%--  <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>--%>
                            
                            
                            
                            <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None" 
                                SetFocusOnError="true" 
                                ErrorMessage="Please enter valid date"
                                ControlToValidate="txt_todate"
                                 ControlToCompare="txt_from_date" Operator="GreaterThan" Type="Date"
                            ></asp:CompareValidator>
            
            </td>
         </tr>
           <tr>
                   <td align="left"> Company Status</td>
                   <td >
                   <asp:DropDownList ID="ddl_Company_Status" runat="server" CssClass="clsDDL" 
                           >
                   <asp:ListItem Value="0"   Selected="True">  All</asp:ListItem>
                   <asp:ListItem Value="1" >Confirm</asp:ListItem>

                        <asp:ListItem Value="2">Inprocess</asp:ListItem>
                        <asp:ListItem Value="3"> Waiting</asp:ListItem>
                        <asp:ListItem Value="4">Expired</asp:ListItem>
                        <asp:ListItem Value="5">Incomplete</asp:ListItem>
                        <asp:ListItem Value="6">Reject</asp:ListItem>
                        <asp:ListItem Value="7">Inspection Passed</asp:ListItem>
                       </asp:DropDownList>
                     
                   </td>

                <td align="left">Category</td>
                   <td>
                   <asp:DropDownList ID="ddlCategory" runat="server" CssClass="clsDDL" 
                          >
                   <asp:ListItem Value="0"   Selected="True">  All</asp:ListItem>
                   <asp:ListItem Value="1" >GENERAL</asp:ListItem>
                        <asp:ListItem Value="4">SC / ST</asp:ListItem>
                        <asp:ListItem Value="3">OBC</asp:ListItem>
                       </asp:DropDownList>
                     
                   </td>
                </tr>


           <tr>
                   <td align="left">Unit Type</td>
                   <td >
                   <asp:DropDownList ID="ddlUnitype" runat="server" CssClass="clsDDL" 
                           >
                   <asp:ListItem Value="0"   Selected="True">  All</asp:ListItem>
                   <asp:ListItem Value="1" >Micro</asp:ListItem>
                        <asp:ListItem Value="2">Small</asp:ListItem>
                        <asp:ListItem Value="3"> Medium</asp:ListItem>
                        
                       </asp:DropDownList>
                     
                   </td>

                <td align="left">Aadhaar Status</td>
                   <td>
                   <asp:DropDownList ID="ddlAadhaarStatus" runat="server" CssClass="clsDDL" 
                            >
                   <asp:ListItem Value="0"   Selected="True">  All</asp:ListItem>
                   <asp:ListItem Value="1" >With Aadhaar</asp:ListItem>
                   <asp:ListItem Value="2">Without Aadhaar</asp:ListItem>
                   </asp:DropDownList>
                     
                   </td>
                </tr>

                <tr>
                  <td colspan="4" >
                  <asp:Button ID="btn_search" runat="server" Text="Search" CssClass="btnCls" 
                          onclick="btn_search_Click" />
                  </td>
                </tr>
       </table>
     </div>
  <div id="grd_display">
     <asp:GridView ID="grd_delete_company" runat="server" AutoGenerateColumns="false"
        CellPadding="4" CellSpacing="2"  >
                               <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <EmptyDataTemplate  >
                                         <asp:Label ID="lblNorecId" runat="server" Text=" No record found. " 
                                          Font-Bold="true" ForeColor="Brown" Font-Names="Arial" Font-Size="Medium"></asp:Label>
            </EmptyDataTemplate>
                                        
                                        
                                        
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText ="No. of Registration">
                                              <ItemTemplate>
                                                <asp:Label ID="lbl_company_Pan" runat="server" Text='<%#Eval("Number_Of_Company") %>'></asp:Label>
                                              </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField  HeaderText="Address">
                                               <ItemTemplate>
                                                 <asp:Label ID="lbl_Company_address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                                               </ItemTemplate>
                                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason for Deletion">
                                               <ItemTemplate>
                                               <asp:Label ID="lbl_reson_for_delete" runat="server" Text='<%#Eval("ResonforDelete") %>'></asp:Label>
                                               </ItemTemplate>
  
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Deleted by">
                                                 <ItemTemplate>
                                                 <asp:Label ID="lbl_pending_with" runat="server" Text='<%#Eval("PendingWith") %>'></asp:Label>
                                                 
                                                 </ItemTemplate>
                                            </asp:TemplateField>--%>
                                          
                                            
                                            
                                            </Columns>
        </asp:GridView>
     
</div>
</asp:Content>

