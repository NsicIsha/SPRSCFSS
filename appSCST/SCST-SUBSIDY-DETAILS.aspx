<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/SCST.master" AutoEventWireup="true" CodeFile="SCST-SUBSIDY-DETAILS.aspx.cs" Inherits="appSCST_SCST_SUBSIDY_DETAILS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .text-label {
            color: #ddf1f7;
            font-weight: bold;
        }

        .grdserchkk {
            vertical-align: top;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td align="center" valign="top" style="width: 100%; height: 10px;">
                        <table cellpadding="2" cellspacing="0" border="1">
                            <tr>
                                <td align="left">From</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_from_date" Enabled="false" runat="server" Text=""></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_from_date"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_from_date" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
                                              
                                </td>
                                <td align="left">To</td>
                                <td align="left">
                                    <asp:TextBox ID="txt_todate" runat="server" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfd1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />
                                   
                                </td>
                                <td>Form Type :
                                    <asp:DropDownList ID="ddlFormType" runat="server">
                                        <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Fresh"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Renewal"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Ammendement"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" InitialValue="0" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="ddlFormType"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left">
                                    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <img alt="progress" src="../Images/Admin/loading.gif" />
                                            Processing...            
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                  <%--  <asp:UpdatePanel ID="update1_grid" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btnCls"
                                                OnClick="Btn_Search_Click" />
                                       <%-- </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </td>
                                <td>
                                     <asp:Button ID="btn_chek" runat="server" Text="Genarate Excel" OnClick="btn_chek_Click"
                 /> 
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="bottom" style="padding-left: 20px; width: 100%; margin-top: 10px;">
                        <table cellpadding="0" cellspacing="0" border="0" width="90%">
                            <tr>
                                <td align="left" valign="top" style="padding-left: 20px;">
                                    <b><span style="color: Red;">
                                        <asp:Label ID="lbl_searchcompany" runat="server" Text=""></asp:Label> </b>
                                </td>
                                <td align="right" valign="top" style="padding-right: 50px;">
                                    <b><span style="color: Red;">
                                        <asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label></b>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 10px;"></td>
                </tr>
                <tr>
                    <td valign="top">
                      
                            
                                    <asp:GridView   ID="grd_search_result" runat="server" CellPadding="3"
                                        CellSpacing="3" GridLines="Both"
                                        AutoGenerateColumns="false" AllowPaging="true"   PageSize="500"
                                        Width="100%" BorderWidth="0" OnPageIndexChanging="grdserch_pageindex_changing"
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
                                                    <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PANNumber">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_location" runat="server" Text='<%#Eval("PANNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UAM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_product" runat="server" Text='<%#Eval("AadhaarNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration With MSME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("DBStattus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_old_registration_no" runat="server" Text='<%#Eval("CategoryStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("MSMEStatus") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NSIC Fee (With ServiceTax)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("NSICFee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Agency Fee (With ServiceTax)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("AgencyFee") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Token Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("FeeToken") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Subsidy Claimed">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("SubsidyClaimed") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("ZoneBranchName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("RegistrationNumber") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_number" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="State">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numbercc" runat="server" Text='<%#Eval("StateName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="District">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numbeDistrict" runat="server" Text='<%#Eval("DistrictName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_refrence_numbeProduct" runat="server" Text='<%#Eval("ProductDesc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                 
                        
                    </td>
                </tr>
             
               
                
                 
            </table>
        </ContentTemplate>
         <Triggers>
        <asp:PostBackTrigger ControlID="btn_chek" />
    </Triggers>


    </asp:UpdatePanel>
</asp:Content>
