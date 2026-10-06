<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="UnitRenewalStep3Store.aspx.cs" Inherits="appAdministrator_UnitRenewalStep3Store" Title="UnitAmendmentStep3Store" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div align="left">
        <div align="left" class="headerAll">
            Factory Store
        </div>
        <div align="left" style="height:50px;">
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
             <tr style="width:95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                   <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                   <tr style="width:100%;">
                   <td style="width:100%;">
                     <table cellpadding="0" cellspacing="0" border="1" style="width:100%;">
                       <tr>
                       <td style="width:15%; padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                        <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                         <td style="width:15%;padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
                          <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-4</td>
                           <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-5</td>
                            <td style="width:15%;padding-left: 10px; padding-right: 10px;">Step-6</td>
                            <td style="width:10%;padding-left: 10px; padding-right: 10px;">Step-7</td>
                       </tr>
                     
                     </table>
                   
                   </td>
                   </tr>
                         
                   
                     <tr style="width:100%;">
                    
                     
                       
                         <td id="step2" style="width:100%;">
                      
                         <table cellpadding="0" cellspacing="0" border="0" style="width:100%;">
                         <tr>
                         <td id="progressbar" align="left" ></td>
                         </tr>
                         
                         
                         
                         </table>
                       
                       
                     </td>
                     </tr>
                       
                   
                   </table>
            </td>
        </tr>
        </table>
        </div>
        <div align="left">
        
                <asp:GridView ID="gvStoreDetails" runat="server" 
                        AutoGenerateColumns="False" BackColor="#FFFFFF"  
                        BorderWidth="3px" CellPadding="0" CellSpacing="1" Width="100%" 
                    onrowdatabound="gvStoreDetails_rowdatabound">
                       
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <EmptyDataTemplate>No Data Found</EmptyDataTemplate>
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUnitid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_UnitId") %>' />
                                    <asp:HiddenField ID="hdnStoreid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_Id") %>' />
                                    <asp:Literal ID="ltlLocation" runat="server"></asp:Literal>
                                </ItemTemplate>
                                
                             </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Description/ Stores/ Products/Services(भंडारों/ उत्पादों /सेवा का विवरण)" ><ItemTemplate><asp:Literal ID="ltlDescription" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Literal></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtDescription" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText=" Ref. of BIS Specification or other Specification (बी.आई.एस. विशिष्ट का सन्दर्भ या अन्य विशिष्टता)"><ItemTemplate><asp:Literal ID="ltlBis" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Literal></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtBis" runat="server" CssClass="txt_bx150" Width="100px" Text='<%#Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:TextBox></EditItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Model/ Brand (माडल/ ब्रांड)" ><ItemTemplate><asp:Literal ID="ltlModel" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_ModelBrand") %>' ></asp:Literal></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtModel" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_ModelBrand") %>' CssClass="txt_bx150" Width="100px"></asp:TextBox></EditItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Limiting Size Capacity/ Rating (सीमित आकार/ क्षमता/ रेटिंग)" ><ItemTemplate><asp:Literal ID="ltlSize" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Literal></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtSize" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>' CssClass="txt_bx150" Width="100px" ></asp:TextBox></EditItemTemplate></asp:TemplateField>
                            
                            <asp:TemplateField HeaderText=" Monthly Production Capacity per shift<br />(महीने के उत्पादन क्षमता प्रति पारि)" ><ItemTemplate><asp:Literal ID="ltlProduction" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Literal></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtProduction" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>' CssClass="txt_bx150" Width="100px" ></asp:TextBox></EditItemTemplate></asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="No. of shifts factory normally works  (पारियों की सं. जो प्राय: कारखाने में चलती है)" ItemStyle-Width="50px" >
                                <ItemTemplate>
                                <asp:Literal ID="ltlShift" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtShift" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_NoOfShifts") %>' CssClass="txt_bx150" Width="100px" ></asp:TextBox>
                                   <%-- <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtShift" FilterType="Numbers">
                                        

                                    </asp:FilteredTextBoxExtender>--%>
                                </EditItemTemplate>
                             </asp:TemplateField>
                        </Columns>
                      <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True" 
                        BackColor="#009999"></SelectedRowStyle>
                        <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>
                   </asp:GridView>
                   <div align="right">
                       
                            <%--<input type="button" id="btnbackhist" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>&nbsp;&nbsp;--%>
                            <asp:Button ID="Button1" runat="server" Text="Back" CausesValidation="false"
onclick="btnBackstep3_Click" CssClass="btnCls" /> &nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save&Continue" CssClass="btnCls" 
                            Width="120px" onclick="btnSave_Click" />
                   </div>
           
        </div>
        
    </div>


</asp:Content>

