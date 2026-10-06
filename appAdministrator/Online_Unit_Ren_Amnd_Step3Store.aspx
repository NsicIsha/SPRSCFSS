<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Online_Unit_Ren_Amnd_Step3Store.aspx.cs" Inherits="appAdministrator_Online_Unit_Ren_Amnd_Step3Store" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />

    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $("#progressbar").progressbar({ value: 45 });
        });

        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }
        function CheckNumericKeyInfo(char1, mozChar) {
            if (mozChar != null) { // Look for a Mozilla-compatible browser
                if ((mozChar >= 48 && mozChar <= 57) || char1 == 8)
                    RetVal = true;
                else {
                    RetVal = false;
                }
            }
            else { // Must be an IE-compatible Browser
                if ((char1 >= 48 && char1 <= 57) || char1 == 8) RetVal = true;
                else {
                    RetVal = false;
                }
            }
            return RetVal;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <div align="left" class="headerAll">
            Factory Store
        </div>
        <div align="left" style="height: 50px;">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr style="width: 95%;">
                    <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                            <tr style="width: 100%;">
                                <td style="width: 100%;">
                                    <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                        <tr>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-4</td>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-5</td>
                                            <td style="width: 15%; padding-left: 10px; padding-right: 10px;">Step-6</td>
                                            <td style="width: 10%; padding-left: 10px; padding-right: 10px;">Step-7</td>
                                        </tr>

                                    </table>

                                </td>
                            </tr>


                            <tr style="width: 100%;">



                                <td id="step2" style="width: 100%;">

                                    <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                        <tr>
                                            <td id="progressbar" align="left"></td>
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
            <asp:MultiView ID="mvStore" runat="server" ActiveViewIndex="0">
                <asp:View ID="viewRepeater" runat="server">
                    <asp:Repeater ID="StoreRepeater" runat="server"
                        OnItemDataBound="StoreRepeater_ItemDataBound">

                        <HeaderTemplate>
                            <table border="1" cellpadding="0" cellspacing="0">
                                <tr style="width: 700px;">
                                    <th style="width: 50px;">S. No. /Store Code
                                    </th>
                                    <th style="width: 50px;" align="center">Location
                                    </th>
                                    <th>Description/ Stores/ Products/Services<br />
                                        भंडारों/ उत्पादों /सेवा का विवरण
                                    </th>
                                    <th>Ref. of BIS Specification or other Specification<br />
                                        बी.आई.एस. विशिष्ट का सन्दर्भ या अन्य विशिष्टता
                                    </th>
                                    <th>Model/ Brand<br />
                                        माडल/ ब्रांड
                                    </th>
                                    <th>Limiting Size Capacity/ Rating<br />
                                        सीमित आकार/ क्षमता/ रेटिंग
                                    </th>
                                    <th>Monthly Production Capacity per shift<br />
                                        महीने के उत्पादन क्षमता प्रति पारि</div>
                                    </th>
                                    <th>No. of shifts factory normally works<br />
                                        पारियों की सं. जो प्राय: कारखाने में चलती है
                                    </th>


                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <tr>
                                <td>
                                    <asp:TextBox ID="txtSnoCode" runat="server" CssClass="txt_bx" Width="50px" OnKeyPress="javascript:return CheckNumericKeyInfo(event.keyCode, event.which)">0</asp:TextBox>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFactory" runat="server" CssClass="clsDDL" OnSelectedIndexChanged="ddl_location_changeR" AutoPostBack="true"></asp:DropDownList></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" Text="0"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtBis" runat="server" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" Text="0"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtModel" runat="server" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" Text="0"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtLimiting" runat="server" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" Text="0"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtAnnual" runat="server" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" Text="0"></asp:TextBox></td>
                                <td style="width: 100px;">
                                    <asp:TextBox ID="txtShift" runat="server" CssClass="txt_bx150" Width="100px" Text="0"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtShift" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <%--<td><asp:DropDownList ID="ddlHSCode" runat="server" CssClass="clsDDL"></asp:DropDownList></td>--%>
                            </tr>

                        </ItemTemplate>

                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <div align="right" style="padding-top: 10px; padding-right: 10px;">
                        <asp:Button ID="btnBack" runat="server" CssClass="btnCls" Text="Back" OnClick="btnBack_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnStoreSubmit" runat="server" CssClass="btnCls"
                    Text="Save&amp;Continue" OnClick="btnStoreSubmit_Click" Width="120px" />
                    </div>
                </asp:View>

                <asp:View ID="viewGrid" runat="server">

                    <asp:GridView ID="gvStoreDetails" runat="server"
                        AutoGenerateColumns="False" BackColor="#FFFFFF"
                        BorderWidth="0" CellPadding="0" CellSpacing="1" Width="100%"
                        OnRowCancelingEdit="gvStoreDetails_RowCancelingEdit"
                        OnRowEditing="gvStoreDetails_RowEditing"
                        OnRowUpdating="gvStoreDetails_RowUpdating"
                        OnRowDataBound="gvStoreDetails_RowDataBound"
                        OnRowDeleting="gvStoreDetails_RowDeleting">
                        <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        <EmptyDataTemplate>Click on Add Store button to add stores</EmptyDataTemplate>
                        <Columns>
                            <asp:CommandField ShowEditButton="true" HeaderStyle-Width="50px" />
                            <asp:CommandField />
                            <asp:CommandField ShowDeleteButton="false" HeaderStyle-Width="50px" />
                            <asp:CommandField />
                            <asp:TemplateField HeaderText="S no. /Code">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlsnocode" runat="server" Text='<%# Bind("fGPtbl_CUnit_Store_SNo") %>'></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSnoCode" runat="server" Text="0" CssClass="txt_bx" Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdnUnitid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_UnitId") %>' />
                                    <asp:HiddenField ID="hdnStoreid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_Id") %>' />
                                    <asp:HiddenField ID="hdnEnterPrise" runat="server" Value='<%# Bind("fGPtbl_CUnit_Enterprise") %>' />

                                    <asp:Literal ID="ltlLocation" runat="server"></asp:Literal>
                                    <asp:HiddenField ID="hdnAmendmentId_del" runat="server" Value='<%# Bind("fGPtbl_CUnitAmendment_Store_Id") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:HiddenField ID="hfUnitid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_UnitId") %>' />
                                    <asp:HiddenField ID="hfStoreid" runat="server" Value='<%# Bind("fGPtbl_CUnit_Store_Id") %>' />
                                    <asp:HiddenField ID="hdnAmendmentId" runat="server" Value='<%# Bind("fGPtbl_CUnitAmendment_Store_Id") %>' />
                                    <asp:DropDownList ID="ddlFactory" runat="server" CssClass="clsDDL" OnSelectedIndexChanged="ddl_location_changeG" AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:HiddenField ID="hfEnterPrise" runat="server" Value='<%# Bind("fGPtbl_CUnit_Enterprise") %>' />

                                </EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description/ Stores/ Products/Services (भंडारों/ उत्पादों /सेवा का विवरण)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlDescription" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:Literal></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" CssClass="txt_bx150" Width="100px" TextMode="MultiLine" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_DescriptionStoresProducts") %>'></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Ref. of BIS Specification or other Specification (बी.आई.एस. विशिष्ट का सन्दर्भ या अन्य विशिष्टता)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlBis" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:Literal></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBis" runat="server" CssClass="txt_bx150" Width="100px" Text='<%#Bind("fGPtbl_CUnit_Store_RefOfBISSpecification") %>'></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Model/ Brand (माडल/ ब्रांड)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlModel" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_ModelBrand") %>'></asp:Literal></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtModel" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_ModelBrand") %>' CssClass="txt_bx150" Width="100px"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Limiting Size Capacity/ Rating (सीमित आकार/ क्षमता/ रेटिंग)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlSize" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>'></asp:Literal></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtSize" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_LimitingSizeCapacityRating") %>' CssClass="txt_bx150" Width="100px"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Monthly Production Capacity per shift (महीने के उत्पादन क्षमता प्रति पारि)">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlProduction" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>'></asp:Literal></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProduction" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_AnnualProductionCapacity") %>' CssClass="txt_bx150" Width="100px"></asp:TextBox></EditItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No. of shifts factory normally works  (पारियों की सं. जो प्राय: कारखाने में चलती है)" ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Literal ID="ltlShift" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_NoOfShifts") %>'></asp:Literal>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtShift" runat="server" Text='<%#Bind("fGPtbl_CUnit_Store_NoOfShifts") %>' CssClass="txt_bx150" Width="100px"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtShift" FilterType="Numbers">
                                    </asp:FilteredTextBoxExtender>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle ForeColor="#CCFF99" Font-Bold="True"
                            BackColor="#009999"></SelectedRowStyle>
                        <RowStyle ForeColor="#003399" BackColor="White"></RowStyle>
                    </asp:GridView>
                    <div align="right">
                        <asp:Button ID="btnAddStore" runat="server" Text="AddStore" CssClass="btnCls"
                            OnClick="btnAddStore_Click" />&nbsp;&nbsp;
                            <%--<input type="button" id="btnbackhist" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>&nbsp;&nbsp;--%>
                        <asp:Button ID="Button1" runat="server" Text="Back" CausesValidation="false"
                            OnClick="btnBackstep3_Click" CssClass="btnCls" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnSave" runat="server" Text="Save&Continue" CssClass="btnCls"
                            Width="120px" OnClick="btnSave_Click" />
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>

    </div>
</asp:Content>
