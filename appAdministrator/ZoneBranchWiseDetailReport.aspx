<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ZoneBranchWiseDetailReport.aspx.cs" Inherits="appAdministrator_ZoneBranchWiseDetailReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
  
  
    </script>
    <style type="text/css">
        .text {
            text-decoration: none;
        }
    </style>
</asp:Content><%--test--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Dashboard Report" CssClass="headerAll"></asp:Label>
    </div>
    <table cellpadding="2" cellspacing="2" border="0" style="padding-left: 10px; width: 100%; margin-top: 20px;">

        <tr>
            <td colspan="2">

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
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddl_1e" runat="server" AutoPostBack="false"
                                TabIndex="52" ValidationGroup="1">
                            </asp:DropDownList>

                        </td>
                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">उद्यमी की विशेष श्रेणी/ Any Special Category of Enterpreneur:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="ddl_1d" runat="server" AutoPostBack="false"
                                TabIndex="51" ValidationGroup="1">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">श्रेणी यूनिट का आकार/Category Unit Size:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:DropDownList ID="DdlCategoryUnitSize" runat="server" AutoPostBack="false"
                                TabIndex="53">
                            </asp:DropDownList>

                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">
                            <span class="MandatoryField">*</span>कार्रवाई की प्रकृति/Nature of Operation :
                        </td>
                        <td align="left" valign="top">
                            <asp:RadioButtonList ID="rdo_manufacturlist" runat="server" RepeatDirection="Horizontal">
                               <%-- <asp:ListItem Selected="True" Value="-1">None</asp:ListItem>--%>
                                <asp:ListItem  Value="1" Selected="True">Manufacturing</asp:ListItem>
                                <asp:ListItem Value="2">Service</asp:ListItem>
                                <asp:ListItem Value="-1">Both</asp:ListItem>
                            </asp:RadioButtonList>


                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">State:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:UpdatePanel ID="Update_Panel_Statewise" runat="server">
                                <ContentTemplate>

                                    <asp:DropDownList ID="ddlState" runat="server"
                                        CssClass="clsDDlReport" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>


                        </td>
                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">City:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                            <asp:UpdatePanel ID="Update_City_Wise" runat="server">
                                <ContentTemplate>

                                    <asp:DropDownList ID="Ddl_City_Wise" runat="server"
                                        CssClass="clsDDlReport" AutoPostBack="True">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>


                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading"></td>
                        <td align="left" valign="top">
                            <asp:UpdatePanel ID="Update_bton"  runat="server" >
                        <ContentTemplate>
                            <asp:Button ID="btn_search" runat="server" Text="Search"
                                OnClick="btn_search_Click" CausesValidation="false" />

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
                </table>




            </td>
        </tr>



        <tr>
            <td align="right" valign="top" colspan="2"></td>

        </tr>
        <tr>
            <td align="right" colspan="2">

                <asp:Button ID="btn_chek" runat="server" Text="Genarate Excel"
                    CausesValidation="false" OnClick="btn_chek_Click" />

            </td>
        </tr>

        <tr>
            <td colspan="2">
                <asp:Panel ID="pnl_grid_data" runat="server" BorderStyle="Solid">
                    <br />
                    <span style="padding-left: 12px;"><b>Dashboard Report</b>
                        <asp:Label ID="lbl_fromcdate" runat="server" Text="" CssClass="leftHeading"></asp:Label>
                        <asp:Label ID="lbl_tcdate" runat="server" Text="" CssClass="leftHeading"></asp:Label>
                    </span>
                    &nbsp;&nbsp;&nbsp;&nbsp; <span style="text-align: right; padding-left: 200px; color: Red;" id="span_total_com" runat="server"><b>Total Companies:</b>
                        <b>
                            <asp:Label ID="lbl_labelcount" runat="server" Text=""></asp:Label></b></span>
                    <br />
                    <br />

                    <asp:GridView ID="grd_excel_type" runat="server" OnRowDataBound="grd_exceltype_rowdatabound"
                        CellPadding="5"
                        CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="70%"
                        BorderWidth="2" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Center" ShowFooter="false" EmptyDataText="No data in the data source.">
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <FooterStyle CssClass="dynRowHeader" />
                        <EditRowStyle BackColor="LightBlue" ForeColor="Red" />

                        <Columns>
                            <asp:TemplateField HeaderText="ZoneName">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_zonename" runat="server" Text='<%#Eval("Zonename")%>'></asp:Label>
                                     <asp:Label ID="lbl_zonename_id" runat="server" Text='<%#Eval("zoneid")%>' Visible="false"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_branchname" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                    <asp:Label ID="lbl_branch_id" runat="server" Text='<%#Eval("BranchID")%>' Visible="false"></asp:Label>
                                </ItemTemplate>

                                <%--  <FooterTemplate>
                       <asp:Label ID="lbl_total" runat="server" Text="Total"></asp:Label>
                        
                     
                     </FooterTemplate>--%>
                    
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Applied<br>(a+b+c+d)">

                                <ItemTemplate>
                                    <asp:Label ID="lbl_applied" runat="server" Text='<%#Eval("Applied ") %>'></asp:Label>

                                </ItemTemplate>
                                <%--<FooterTemplate>
                      <asp:Label ID="lbl_applied_total" runat="server" Text=""></asp:Label>
                    
                    </FooterTemplate>--%>
                    
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Registered<br>(a)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_registerd" runat="server" Text='<%#Eval("Registerd") %>'></asp:Label>

                                </ItemTemplate>

                                <%-- <FooterTemplate>
                      <asp:Label ID="lbl_registerd_total" runat="server" Text=""></asp:Label>
                      
                      
                      </FooterTemplate>--%>
                    
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Incomplete<br>(b)" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Incomplete" runat="server" Text="0"></asp:Label><%-- '<%#Eval("Incomplete") %>'--%>
                                </ItemTemplate>
                                <%--  <FooterTemplate>
                        <asp:Label ID="lbl_incomplete_total" runat="server" Text=""></asp:Label>
                        
                        </FooterTemplate>--%>
                    
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Documents Awaited<br>(b)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_waiting" runat="server" Text='<%#Eval("waiting") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <FooterTemplate>
                    <asp:Label ID="lbl_waiting_total" runat="server" Text=""></asp:Label>
                    
                    </FooterTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Inspection Pending<br>(c)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_inprocess" runat="server" Text='<%#Eval("Inprocess") %>'></asp:Label>

                                </ItemTemplate>

                                <%--   <FooterTemplate>
                        <asp:Label ID="lbl_inprocess_total" runat="server" Text=""></asp:Label>
                      
                      </FooterTemplate>--%>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Inspection Passed<br>(d)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_inspection_passed" runat="server" Text='<%#Eval("InspectionPassed") %>'></asp:Label>
                                </ItemTemplate>
                                <%-- <FooterTemplate>
                        <asp:Label ID="lbl_inspection_passed_total" runat="server" Text=""></asp:Label>
                      
                      </FooterTemplate>--%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Under Process<br>(b+c+d)">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_underproces" runat="server" Text='<%#Eval("UnderprocessUnit") %>'></asp:Label>

                                </ItemTemplate>
                                <%--  <FooterTemplate>
                          <asp:Label ID="lbl_underprocess_total"  runat="server" Text=""></asp:Label>
                      </FooterTemplate>
                                --%>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Expired">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_expired" runat="server" Text='<%#Eval("expired") %>'></asp:Label>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Total Units <br/> (Registered+Expired)">
                                <ItemTemplate>
                                    <b><span style="color: Red;">
                                        <asp:Label ID="lbl_reg_expired" runat="server" Text=""></asp:Label></span></b>

                                </ItemTemplate>

                            </asp:TemplateField>


                        </Columns>

                    </asp:GridView>
                    <br />

                    <table cellpadding="2" cellspacing="2" border="0">

                        <tr>
                            <td align="right" style="color: Red; padding-left: 550px;" id="td_total_company" runat="server"><b>Total Companies:</b><b><asp:Label ID="lbl_total_new_company" runat="server" Text=""></asp:Label></b> </td>

                        </tr>

                    </table>


                </asp:Panel>


            </td>

        </tr>
    </table>

</asp:Content>

