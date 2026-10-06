<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ProductCompanySearch.aspx.cs" Inherits="appAdministrator_ProductCompanySearch" Title="NSIC SPR::Company Product Search" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="width: 990px;">
        <table cellpadding="0" cellspacing="0" border="0" style="margin-top: 10px;">
            <tr>
                <td align="left" class="headerAll">Product search Report:
                </td>
            </tr>
            <tr>
                <td style="height: 10px;"></td>
            </tr>

            <tr>
                <td style="padding-left: 100px;">
                    <table cellspacing="3" cellpadding="3" border="0">
                        <tr>
                            <td align="left" class="leftext">Search:</td>
                            <td style="width: 2px;"></td>
                            <td align="left" class="coloncls">
                                <asp:DropDownList ID="ddlSearchCriteria" runat="server" AutoPostBack="false" CssClass="clsDDL" Width="220px">
                                    <asp:ListItem Text="Company Name" Value="1"></asp:ListItem>
                                    <asp:ListItem Selected="True" Text="Product" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Registration No." Value="3"></asp:ListItem>
                                    <asp:ListItem Text="HS/NPC Code" Value="4"></asp:ListItem>
                                </asp:DropDownList>

                            </td>
                            <td style="width: 2px;"></td>
                            <td align="left" class="coloncls">
                                <asp:TextBox ID="txt_serch_Criteria" runat="server" CssClass="clstxtbx" Width="220"></asp:TextBox>
                                <cc1:TextBoxWatermarkExtender ID="txtw" runat="server" TargetControlID="txt_serch_Criteria"
                                    WatermarkText="Enter text to search" WatermarkCssClass="text-label ">
                                </cc1:TextBoxWatermarkExtender>
                            </td>
                            <td style="width: 2px;">

                                <asp:UpdateProgress ID="updProgress"
                                    AssociatedUpdatePanelID="Update_bton"
                                    runat="server">
                                    <ProgressTemplate>
                                        <img alt="progress" src="../Images/Admin/loading.gif" />
                                        Processing...            
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td align="left" class="coloncls">

                                <asp:UpdatePanel ID="Update_bton" runat="server">
                                    <ContentTemplate>



                                        <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btnCls"
                                            OnClick="Btn_Search_Click" />


                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </td>
                        </tr>


                        <tr style="padding-top: 0px">
                            <td align="left" class="leftext" colspan="6" style="padding-top: 1px"></td>
                        </tr>


                        <tr style="padding-top: 10px">
                            <td align="left" class="leftext">State:</td>
                            <td style="width: 2px;"></td>
                            <td align="left" class="coloncls">
                                 <asp:UpdatePanel ID="Update_Panel_Statewise" runat="server">
                                <ContentTemplate>

                                <asp:DropDownList ID="ddlState" runat="server"
                                    CssClass="clsDDlReport" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>    
                            
                            </td>
                            <td style="width: 2px;">District</td>
                            <td align="left" class="coloncls">
                                 <asp:UpdatePanel ID="Update_City_Wise" runat="server">
                                <ContentTemplate>

                                <asp:DropDownList ID="Ddl_City_Wise" runat="server"
                                    CssClass="clsDDlReport" AutoPostBack="True">
                                </asp:DropDownList>
                                 </ContentTemplate>
                            </asp:UpdatePanel>


                            </td>
                            <td style="width: 2px;">&nbsp;</td>
                            <td align="left" class="coloncls">&nbsp;</td>
                        </tr>


                        <%--  <tr>
                            <td align="center" class="leftext" colspan="5"> <asp:Button ID="btnTRSCST_Submit" Width="200px"  runat="server" CssClass="btnCls" Text="Download Excel" OnClick="btntrscstSubmit_Click" /> </td>
                             
                            
                        </tr>--%>
                    </table>
                </td>

            </tr>

            <tr>
                <td style="height: 10px;"></td>
            </tr>
            <tr>
                <td align="right" style="padding-right: 100px;">
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="left">
                                <b><span style="color: Red;">
                                    <asp:Label ID="lbl_text_search" runat="server" Text=""></asp:Label></span></b>
                            </td>

                        </tr>

                    </table>

                    <b><span style="color: Red;">
                        <asp:Label ID="lbl_count_grid" runat="server" Text=""></asp:Label></span></b>

                </td>


            </tr>
            <tr>
                <td align="left" valign="top" style="padding-left: 50px;">
                    <b><span style="color: Red;">
                        <asp:Label ID="lbl_search_data_item" runat="server" Text=""></asp:Label>
                    </span></b>

                </td>

            </tr>
            <tr>
                <td style="height: 10px;"></td>
            </tr>
            <tr>
                <td style="padding-left: 20px; width: 100%;">
                    <asp:Panel runat="server" ID="pnl_dg">


                        <asp:GridView ID="grd_search_result_dg" runat="server" CellPadding="3"
                            CellSpacing="3" AutoGenerateColumns="False"
                            Width="100%" BorderWidth="0px" PageSize="20"
                            HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">

                            <HeaderStyle CssClass="dynRowHeader" />
                            <RowStyle CssClass="dynRowEven" />
                            <AlternatingRowStyle CssClass="dynRowOdd" />
                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%#""+Eval("Address")+""+Eval("city")+""+Eval("state") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="State">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameState" runat="server" Text='<%#Eval("state") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNameDistrict" runat="server" Text='<%#Eval("city") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Email">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%#Eval("fGPtbl_Company_HeadOffice_Email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("fGPtbl_Company_HeadOffice_Mobile") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcat" runat="server" Text='<%#Eval("fGPtbl_CUnit_CategoryCastId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" runat="server" Text='<%#Eval("Products") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                        <br />



                    </asp:Panel>
                </td>

            </tr>

            <tr>
                <td style="height: 80px;">&nbsp;</td>
            </tr>
        </table>

    </div>

</asp:Content>
