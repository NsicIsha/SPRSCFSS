<%@ Page Title="Report" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="TraceSubPageDataStatusReport.aspx.cs" Inherits="appAdministrator_TraceSubPageDataStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .text {
            text-decoration: none;
        }
    </style><%--test--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text=" Report" CssClass="headerAll"></asp:Label>
    </div>
    <table cellpadding="2" cellspacing="3" border="0" width="100%">
        <tr style="width: 100%; display: none;">
            <td align="left" style="padding-left: 4px; width: 240px;"><b>Enter  Company Name or Ref Number </b></td>
            <td align="left" style="width: 475px;">
                <asp:DropDownList ID="ddl_list" runat="server" AutoPostBack="true" Width="150"
                    CssClass="clsDDL">
                    <asp:ListItem Selected="True" Value="1">Company Name</asp:ListItem>
                    <asp:ListItem Value="2">Reference Number  </asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="txt_Ref_no" runat="server" Text="" Width="220px" MaxLength="250"></asp:TextBox></td>
            <td align="left">&nbsp;<asp:Button ID="btn_status_track" runat="server"
                Text=" Track Status" Width="120" CssClass="btnCls"
                OnClientClick="javascript:return validate();" OnClick="btn_status_track_Click" /></td>
        </tr>
        <tr>
            <td colspan="3" style="height: 10px;">
                <%--<asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                                <ProgressTemplate>
                                                    <img alt="progress" src="../images/loading.gif" />
                                                 Please Wait While We are Processing Your Request...            
                                                </ProgressTemplate>
</asp:UpdateProgress>--%>
            </td>
        </tr>

        <%-- <tr><td colspan="3" style="height:10px;"><asp:Button ID="btn_chek" runat="server" Text="Genarate Excel" OnClick="btn_chek_Click"
                 /></td></tr>--%>

        <tr>
            <td colspan="3">
                <%--  OnRowDataBound="grd_search_result_RowDataBound" --%>
                <asp:GridView ID="grd_search_result" runat="server" ShowFooter="true" CellPadding="3" OnRowDataBound="grd_search_result_RowDataBound"
                    CellSpacing="3" GridLines="Both"
                    AutoGenerateColumns="false" PageSize="1000"
                    Width="100%" BorderWidth="0" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
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

                        <asp:TemplateField HeaderText="PAN">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("PAN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Social Category">
                            <ItemTemplate>
                                <asp:Label ID="lbl_SocialCategory" runat="server" Text='<%#Eval("SocialCategory") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%-- <asp:TemplateField HeaderText="Validity" >  
                      <ItemTemplate>
                        <asp:Label ID="lbl_registration_number" runat="server" Text='<%#Eval("RegistrationDate")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>--%>

                        <%-- <asp:BoundField DataField="UnitAmount" HeaderText="NSIC Fee(In Rs.)" ItemStyle-Width="60" DataFormatString="{0:N2}"
        ItemStyle-HorizontalAlign="Right" />--%>

                        <asp:TemplateField HeaderText="NSIC Fee(In Rs.)">
                            <ItemTemplate>
                                <asp:Label ID="lbl_refrence_numberAP" runat="server" DataFormatString="{0:N2}" Text='<%#Eval("UnitAmount") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalNSICValue" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Servie Tax(In Rs.)">
                            <ItemTemplate>
                                <asp:Label ID="lbl_refrence_numberSC" runat="server" Text='<%#Eval("UnitST") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblTotalNSICValue_ST" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit Type">
                            <ItemTemplate>
                                <asp:Label ID="lbl_UnitType" runat="server" Text='<%#Eval("UnitType") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Agency Name">
                            <ItemTemplate>
                                <asp:Label ID="lblAgencyNames" runat="server" Text='<%#Eval("AgencyName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Agency Fee(In Rs.)">
                            <ItemTemplate>
                                <asp:Label ID="lbl_refrence_numberE" runat="server" Text='<%#Eval("AgencyAmount") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblTotalAgency" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Agency Service Tax(In Rs.)">
                            <ItemTemplate>
                                <asp:Label ID="lbl_refrence_numberM" runat="server" Text='<%#Eval("AgencyST") %>'></asp:Label>
                            </ItemTemplate>

                            <FooterTemplate>
                                <asp:Label ID="lblTotalAgency_ST" runat="server" />
                            </FooterTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </td>

        </tr>


        <tr>
            <td colspan="3">
                <asp:Label ID="lbl_report_status_track" runat="server" Text=""></asp:Label>
            </td>
        </tr>
    </table>

    <%-- </ContentTemplate>      
    </asp:UpdatePanel>--%>
</asp:Content>

