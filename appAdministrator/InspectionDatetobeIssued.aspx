<%@ Page Title="Inspection Date to be Issued" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="InspectionDatetobeIssued.aspx.cs" Inherits="appAdministrator_InspectionDatetobeIssued" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Companies waiting for Inspection date
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="400" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                
                    <asp:View ID="viewMessage" runat="server">
                    
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="center" valign="top" style="width: 100%;">
                                    <asp:Label ID="lblMsg" runat="server" CssClass="reportCenterText"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="viewData" runat="server">
                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="left" valign="top" style="width: 100%;">
                                    <asp:GridView ID="gvAcceptedcompanies" runat="server" Width="100%" BorderWidth="0" CellPadding="3"
                                        CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                                        DataKeyNames="fGPtbl_IS_UnitId"
                                        AllowSorting="false" AllowPaging="false" >
                                        <HeaderStyle CssClass="GrdviewHeader" />
                                        <RowStyle CssClass="GrdviewRowOdd" />
                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %> 
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_MF_Name"
                                                HeaderText="Type" />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_Name"
                                                HeaderText="Company" />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_CUnit_SSIRegistrationNo"
                                                HeaderText="SSI Registration No." />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_PAN"
                                                HeaderText="PAN" />
                                            <asp:BoundField HtmlEncode="false" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_IS_DateStatus2"
                                                HeaderText="Request<br>Generation<br>Date" />
                                            <asp:BoundField HtmlEncode="false" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_MIA_Name"
                                                HeaderText="Inspection<br>Agency<br>name" />
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

