<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PointOfInterest_Zonal.aspx.cs" Inherits="appAdministrator_PointOfInterest_Zonal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="2" cellspacing="2" border="0" width="100%">
        <tr>
            <td align="left" valign="top" class="headerAll">Point of Interest Report:
      
            </td>

        </tr>

        <tr>
            <td style="height: 10px;">


                <table style="width: 100%">

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">From Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">
                            <asp:textbox enabled="false" id="txt_fromdate" runat="server" text=""></asp:textbox>
                            <asp:imagebutton id="imgPopup" imageurl="~/images/calendar.png" imagealign="Bottom"
                                runat="server" />
                            <asp:calendarextender id="Calendar1" popupbuttonid="imgPopup" runat="server" targetcontrolid="txt_fromdate"
                                format="dd/MM/yyyy">
</asp:calendarextender>
                            <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" errormessage="Required" controltovalidate="txt_fromdate" setfocusonerror="true" display="Dynamic"></asp:requiredfieldvalidator>
                            <%-- <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                                Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                ClearTextOnInvalid="true">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                                InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>--%>

                        </td>

                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" style="width: 45%;" class="leftHeading">To Date:
                        </td>
                        <td align="left" valign="top" style="width: 50%;">

                            <asp:textbox enabled="false" id="txt_todate" runat="server" text=""></asp:textbox>


                            <asp:imagebutton id="ImageButton2" imageurl="~/Images/calendar.png" imagealign="Bottom"
                                runat="server" />
                            <asp:calendarextender id="CalendarExtender2" popupbuttonid="ImageButton2" runat="server" targetcontrolid="txt_todate"
                                format="dd/MM/yyyy">
</asp:calendarextender>

                            <asp:requiredfieldvalidator id="rfdTodate" runat="server" errormessage="Required" controltovalidate="txt_todate" setfocusonerror="true" display="Dynamic"></asp:requiredfieldvalidator>

                            <%--<asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                                Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                ClearTextOnInvalid="true">
                            </asp:MaskedEditExtender>
                            <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                                InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>--%>

                        </td>

                    </tr>

                    <tr>


                        <td align="left" valign="bottom" style="padding-left: 20px; width: 100%; margin-top: 10px;" colspan="2">
                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                <tr>
                                    <%-- <td align="left" valign="top" style="padding-left:20px;">
                    <b><span style="color:Red;"> <asp:Label ID="lbl_searchcompany" runat="server" Text=""></asp:Label> </b>
                  </td>--%>
                                    <td align="right" valign="top" style="padding-right: 50px;">
                                        <b><span style="color: Red;">
                                            <asp:label id="lbl_totalcount_buying" runat="server" text=""></asp:label></b>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading"></td>
                        <td align="left" valign="top">
                            <asp:updatepanel id="Update_bton" runat="server">
                                <ContentTemplate>
                                    <asp:Button ID="btn_search" runat="server" Text="Search"  OnClick="Btn_Search_Click"  CssClass="btnCls" />
                                 </ContentTemplate>
                            </asp:updatepanel>

                            <br />
                            <asp:updateprogress id="updProgress"
                                associatedupdatepanelid="Update_bton"
                                runat="server">
                                <ProgressTemplate>
                                    <img alt="progress" src="../Images/Admin/loading.gif" />
                                    Processing...            
                                </ProgressTemplate>
                            </asp:updateprogress>
                        </td>
                    </tr>

                    <tr align="left" valign="top">
                        <td align="left" valign="top" class="leftHeading">&nbsp;</td>
                        <td align="left" valign="top">
                            <%--<asp:Button ID="btn_Excel" runat="server" Text="Generate Excel"
                                  OnClick="btn_Excel_Click" CausesValidation="false" />--%>

                            <%--                            <asp:Button ID="Button1" runat="server" Text="Generate Excel"
                                OnClick="btnDownload_Click" CausesValidation="false" CssClass="btndiv" OnInit="Button1_Init" />--%>

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="width: 100%; padding-left: 2px;">
                <asp:updatepanel id="update1_DgD_agencies" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd_search_result" runat="server" CellPadding="3"
                            CellSpacing="3" GridLines="Both"
                            AutoGenerateColumns="false" AllowPaging="false" AllowSorting="true"  
                            Width="100%" BorderWidth="0" OnPageIndexChanging="grdserch_pageindex_changing"
                            OnRowCommand="gv_company_RowCommand"
                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
                            <HeaderStyle CssClass="dynRowHeader" />
                            <RowStyle CssClass="dynRowEven" />
                            <AlternatingRowStyle CssClass="dynRowOdd" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <EmptyDataTemplate>
                                <div align="center"><strong style="font-size: 18px; font-weight: bold; color: Red;">No  Data  Found !!!</strong></div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:TemplateField HeaderText="S.NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("fGPtbl_POI_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="PAN Number"  >  
                    <ItemTemplate>
                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("fGPtbl_POI_PAN") %>'></asp:Label>
                    </ItemTemplate>
                   
                   </asp:TemplateField>
                    <asp:TemplateField HeaderText="UAM">
                      <ItemTemplate>
                      <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("fGPtbl_POI_UAM") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>


                                <%-- <asp:TemplateField HeaderText="GST">
                      <ItemTemplate>
                      <asp:Label ID="lbl_productG" runat="server" Text='<%#Eval("fGPtbl_POI_GST") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Nature of Operation">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("NatureOperation")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Firm Constitution">
                      <ItemTemplate>
                      <asp:Label ID="lbl_old_registration_no" runat="server" Text='<%#Eval("FirmConstitution") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Authorised Person">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberAP" runat="server" Text='<%#Eval("fGPtbl_POI_PersonName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Social Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberSC" runat="server" Text='<%#Eval("SocialCategory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberE" runat="server" Text='<%#Eval("fGPtbl_POI_Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberM" runat="server" Text='<%#Eval("fGPtbl_POI_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberState" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branch">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberBranch" runat="server" Text='<%#Eval("fGPtbl_MB_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Zone">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberZone" runat="server" Text='<%#Eval("fGPtbl_MZ_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Point of Interest Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_refrence_numberDate" runat="server" Text='<%#Eval("fGPtbl_POI_CDate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Point of Interest Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_POI_Status" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <%--  <asp:TemplateField  HeaderText="Product">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberProduct" runat="server" Text='<%#Eval("fGPtbl_POI_ProductDetails") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                                <%--  <asp:TemplateField  HeaderText="EM-II">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberEM2" runat="server" Text='<%#Eval("fGPtbl_POI_EM2") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>

                      <asp:TemplateField  HeaderText="Inspection Agency">
                      <ItemTemplate>
                        <asp:Label ID="lbl_refrence_numberIA" runat="server" Text='<%#Eval("fGPtbl_MIA_Name") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                                <%-- <asp:TemplateField HeaderText="Action">
                           <ItemTemplate>
                               <asp:LinkButton   ID="btnAction" runat="server" Text="Actionn" CommandName="ActionPOI"  CommandArgument='<%#Eval("fGPtbl_POI_Id") %>'></asp:LinkButton>
                           </ItemTemplate>
                       </asp:TemplateField>--%>

                                <%--  <asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton CausesValidation="false" ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                    CommandName="ActionPOI" CommandArgument='<%#Eval("fGPtbl_POI_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                     
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>

                    </ContentTemplate>
                </asp:updatepanel>
            </td>
        </tr>
    </table>
</asp:Content>

