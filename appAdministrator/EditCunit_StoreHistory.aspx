<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Report.master" AutoEventWireup="true" CodeFile="EditCunit_StoreHistory.aspx.cs" Inherits="appAdministrator_EditCunit_StoreHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

 <style type="text/css">
    .class
      {
      	 color:Red;
      	  color:Blue;
      }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellspacing="4"  cellpadding="4" border="0">
    <tr>
            <td align="left" class="headerAll">
              History  Unit store Changes
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
          <tr>
            <td align="right" valign="top">
              <div align="right" style="width:100%;">
                               <%-- <div style="float:right;">
                                    Shows Changes
                                    <div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;"></div>
                                    <div style="float:right;">Uttam</div>
                                </div>--%>
                                  <table cellpadding="0" cellspacing="0" border="0">
                                  <tr>
                                      
                                    <td align="right"><b>Shows Changes: &nbsp;&nbsp;</b></td>
                                      
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Red;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                        <td><b>&nbsp;&nbsp;&nbsp;&nbsp;New Value&nbsp;&nbsp;&nbsp;&nbsp;</b></td>
                                      <td><div class="clsLegends_Left_Deactivate" style="background-color:Blue;float:right;">&nbsp;&nbsp;&nbsp;</div></td>
                                      <td><b>&nbsp;&nbsp;&nbsp;OLD Value&nbsp; </b></td> 
                                      
                                  </tr>
                                  
                                  </table>
                            </div>
            
            </td>
          </tr>
            
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="rowHeading">
                            (4.c) उन भंडारों/ उत्पादों का ब्यौरा जिनके लिए पंजीकरण प्रार्थित है<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Details of the stores/products for which the registration is sought
                        </td>
                    </tr>
                    <%--DataKeyNames="fGPtbl_CUnit_Store_Id"--%>
                    <tr align="left" valign="top">
                        <td colspan="3" align="left" valign="top" class="leftHeading">
                             <asp:GridView ID="gvStoredetails_new" runat="server" Width="100%" 
                            BorderWidth="0" CellPadding="3"
                            CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                            
                            AllowSorting="false"
                            AllowPaging="false" > <%--onrowdatabound="gvStoredetails_RowDataBound" --%>
                            <HeaderStyle CssClass="GrdviewHeader_new" Height="100" />
                            <RowStyle CssClass="GrdviewRowOdd_new" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven_new" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <Columns>
                                <%--  <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <div style="height:90px;">S.No./ क्र. सं.</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <%#Container.DataItemIndex+1 %>
                                      <asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_UnitId") %>' />
                                        <asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_Id") %>' />
                                        <asp:HiddenField ID="HDFAmdStoreId" runat="server" Value='<%#Eval("fGPtbl_CUnitAmendment_Store_Id") %>' />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                           <%--      <asp:TemplateField>
                                        <HeaderTemplate>
                                      S.no./<br/>Store Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lbl_fGPtbl_CUnit_Store_SNo" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_SNo") %>'></asp:Label>
                                        </ItemTemplate>
                                
                                </asp:TemplateField>--%>
              <%--                  <asp:BoundField DataField="fGPtbl_CUnit_Store_SNo" HtmlEncode="false" HeaderText="S.no./<br/>Store Code" />--%>
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory" />
                                
                                <asp:TemplateField>
                                        <HeaderTemplate>
                                        Description/ Stores/ Products<br />भंडारों/ उत्पादों का विवरण<br/>(2)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lbl_fGPtbl_CUnit_Store_DescriptionStoresProducts" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Label>
                                        </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField>
                                  <HeaderTemplate>
                                  Ref. of BIS Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ<br />(3)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_fGPtbl_CUnit_Store_RefOfBISSpecification" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:BoundField DataField="fGPtbl_CUnit_Store_DescriptionStoresProducts" HtmlEncode="false" HeaderText="Description/ Stores/ Products<br />भंडारों/ उत्पादों का विवरण<br/>(2)" HeaderStyle-HorizontalAlign="Center" />--%>
                                
                             <%--   <asp:BoundField DataField="fGPtbl_CUnit_Store_RefOfBISSpecification" HtmlEncode="false" HeaderText="Ref. of BIS Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ<br />(3)" HeaderStyle-HorizontalAlign="Center" />--%>
                             <asp:TemplateField>
                               <HeaderTemplate>
                                          Model/ Brand<br />माडल/ ब्रांड<br />(4)
                               </HeaderTemplate>
                               <ItemTemplate>
                                 <asp:Label ID="lbl_fGPtbl_CUnit_Store_ModelBrand_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_ModelBrand") %>'></asp:Label>
                               </ItemTemplate>
                             </asp:TemplateField>
                                
                               <%-- <asp:BoundField DataField="fGPtbl_CUnit_Store_ModelBrand" HtmlEncode="false" HeaderText="Model/ Brand<br />माडल/ ब्रांड<br />(4)" HeaderStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField>
                                  <HeaderTemplate>
                                    Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग<br />(5)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_CUnit_Store_LimitingSizeCapacityRating_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                
                               <%-- <asp:BoundField DataField="fGPtbl_CUnit_Store_LimitingSizeCapacityRating" HtmlEncode="false" HeaderText="Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग<br />(5)" HeaderStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                       Annual Production Capacity per shift<br />वार्षिक उत्पादन क्षमता प्रति पारि<br />(6)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_fGPtbl_CUnit_Store_AnnualProductionCapacity_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:BoundField DataField="fGPtbl_CUnit_Store_AnnualProductionCapacity" HtmlEncode="false" HeaderText="Annual Production Capacity per shift<br />वार्षिक उत्पादन क्षमता प्रति पारि<br />(6)" HeaderStyle-HorizontalAlign="Center" />--%>
                              <asp:TemplateField>
                                  <HeaderTemplate>
                                     No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है<br />(7)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_fGPtbl_CUnit_Store_NoOfShifts_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField>
                                  <HeaderTemplate>
                                    HS Code
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_hscode_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_HsCode") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField>
                               <HeaderTemplate>
                                   Store Header
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_fGPtbl_CUnit_Store_Pmps_new" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_Pmps") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              
                              
                          <%--     <asp:BoundField DataField="fGPtbl_CUnit_Store_HsCode" HeaderText="HS Code" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="fGPtbl_CUnit_Store_Pmps" HeaderText="Store Header" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                              
                                
<%--                                <asp:BoundField DataField="fGPtbl_CUnit_Store_NoOfShifts" HtmlEncode="false" HeaderText="No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है<br />(7)" HeaderStyle-HorizontalAlign="Center" />--%>
                                
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="gvStoredetails" runat="server" Width="100%" 
                            BorderWidth="0" CellPadding="3"
                            CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                            
                            AllowSorting="false"
                            AllowPaging="false" > <%--onrowdatabound="gvStoredetails_RowDataBound" --%>
                            <HeaderStyle CssClass="GrdviewHeader" Height="100" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <Columns>
                                <%--  <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <div style="height:90px;">S.No./ क्र. सं.</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <%#Container.DataItemIndex+1 %>
                                      <asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_UnitId") %>' />
                                        <asp:HiddenField ID="HDFUnitid" runat="server" Value='<%#Eval("fGPtbl_CUnit_Store_Id") %>' />
                                        <asp:HiddenField ID="HDFAmdStoreId" runat="server" Value='<%#Eval("fGPtbl_CUnitAmendment_Store_Id") %>' />
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                 <asp:TemplateField>
                                        <HeaderTemplate>
                                      S.no./<br/>Store Code
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lbl_fGPtbl_CUnit_Store_SNo" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_SNo") %>'></asp:Label>
                                        </ItemTemplate>
                                
                                </asp:TemplateField>
              <%--                  <asp:BoundField DataField="fGPtbl_CUnit_Store_SNo" HtmlEncode="false" HeaderText="S.no./<br/>Store Code" />--%>
                                <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory" />
                                
                                <asp:TemplateField>
                                        <HeaderTemplate>
                                        Description/ Stores/ Products<br />भंडारों/ उत्पादों का विवरण<br/>(2)
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lbl_fGPtbl_CUnit_Store_DescriptionStoresProducts" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Label>
                                        </ItemTemplate>
                                
                                </asp:TemplateField>
                                <asp:TemplateField>
                                  <HeaderTemplate>
                                  Ref. of BIS Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ<br />(3)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_fGPtbl_CUnit_Store_RefOfBISSpecification" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:BoundField DataField="fGPtbl_CUnit_Store_DescriptionStoresProducts" HtmlEncode="false" HeaderText="Description/ Stores/ Products<br />भंडारों/ उत्पादों का विवरण<br/>(2)" HeaderStyle-HorizontalAlign="Center" />--%>
                                
                             <%--   <asp:BoundField DataField="fGPtbl_CUnit_Store_RefOfBISSpecification" HtmlEncode="false" HeaderText="Ref. of BIS Specification<br />बी.आई.एस. विशिष्ट का सन्दर्भ<br />(3)" HeaderStyle-HorizontalAlign="Center" />--%>
                             <asp:TemplateField>
                               <HeaderTemplate>
                                          Model/ Brand<br />माडल/ ब्रांड<br />(4)
                               </HeaderTemplate>
                               <ItemTemplate>
                                 <asp:Label ID="lbl_fGPtbl_CUnit_Store_ModelBrand" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_ModelBrand") %>'></asp:Label>
                               </ItemTemplate>
                             </asp:TemplateField>
                                
                               <%-- <asp:BoundField DataField="fGPtbl_CUnit_Store_ModelBrand" HtmlEncode="false" HeaderText="Model/ Brand<br />माडल/ ब्रांड<br />(4)" HeaderStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField>
                                  <HeaderTemplate>
                                    Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग<br />(5)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_CUnit_Store_LimitingSizeCapacityRating" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Label>
                                  </ItemTemplate>
                                </asp:TemplateField>
                                
                               <%-- <asp:BoundField DataField="fGPtbl_CUnit_Store_LimitingSizeCapacityRating" HtmlEncode="false" HeaderText="Limiting Size Capacity/ Rating<br />सीमित आकार/ क्षमता/ रेटिंग<br />(5)" HeaderStyle-HorizontalAlign="Center" />--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                       Annual Production Capacity per shift<br />वार्षिक उत्पादन क्षमता प्रति पारि<br />(6)
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                      <asp:Label ID="lbl_fGPtbl_CUnit_Store_AnnualProductionCapacity" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <%--  <asp:BoundField DataField="fGPtbl_CUnit_Store_AnnualProductionCapacity" HtmlEncode="false" HeaderText="Annual Production Capacity per shift<br />वार्षिक उत्पादन क्षमता प्रति पारि<br />(6)" HeaderStyle-HorizontalAlign="Center" />--%>
                              <asp:TemplateField>
                                  <HeaderTemplate>
                                     No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है<br />(7)
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                    <asp:Label ID="lbl_fGPtbl_CUnit_Store_NoOfShifts" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField>
                                  <HeaderTemplate>
                                    HS Code
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_hscode" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_HsCode") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                               <asp:TemplateField>
                               <HeaderTemplate>
                                   Store Header
                                  </HeaderTemplate>
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_fGPtbl_CUnit_Store_Pmps" runat="server" Text='<%#Eval("fGPtbl_CUnit_Store_Pmps") %>'></asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              
                              
                          <%--     <asp:BoundField DataField="fGPtbl_CUnit_Store_HsCode" HeaderText="HS Code" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                  <asp:BoundField DataField="fGPtbl_CUnit_Store_Pmps" HeaderText="Store Header" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:BoundField>--%>
                              
                                
<%--                                <asp:BoundField DataField="fGPtbl_CUnit_Store_NoOfShifts" HtmlEncode="false" HeaderText="No. of shifts factory normally works<br />पारियों की सं. जो प्राय: कारखाने में चलती है<br />(7)" HeaderStyle-HorizontalAlign="Center" />--%>
                                
                            </Columns>
                        </asp:GridView>
                                    
                        </td>
                    </tr>
          
          
          </table>



</asp:Content>

