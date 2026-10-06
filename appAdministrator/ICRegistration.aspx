<%@ Page Title="Inspection Complete for Registration" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ICRegistration.aspx.cs" Inherits="appAdministrator_ICRegistration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" Text="Inspection Complete Registration" CssClass="headerAll"></asp:Label>
        </div>
        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <%if (icregAction == 1)
                { %>
            <div style="height: 30px;">
                <div id="divAction" runat="server" class="Action_Div" align="center" style="background-color: #ECF8FB; height: 30px;">
                    <div id="Action_DivInner_1" align="center">
                        <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                    </div>
                    <div id="Action_DivInner_2" align="center">
                        <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor: pointer;" />
                    </div>
                </div>
            </div>
            <cc1:RoundedCornersExtender
                ID="RoundedCornersExtender1" runat="server"
                TargetControlID="divAction"
                BorderColor="#22518F"
                Radius="10"
                Corners="All">
            </cc1:RoundedCornersExtender>
            <%} %>
        </div>

        <div align="left" style="padding-left: 10px; padding-right: 10px;">

            <asp:GridView ID="gvICRegistration" runat="server" AutoGenerateColumns="false"
                Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1"
                OnRowDataBound="gvICRegistration_RowDataBound">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />

                    <asp:TemplateField HeaderText="Accept" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" CausesValidation="false"
                                Text="Accept" Width="60px" CommandName="accept" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Button ID="btnReject" runat="server" CssClass="btnCls" CausesValidation="false" Text="Reject" Width="60px" CommandName="reject" CommandArgument='<%#Eval("fGPtbl_IS_UnitId")+","+Eval("fGPtbl_Company_Id") %>' OnCommand="btnReject_Command" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>

        <div align="center" style="padding-top: 10px;">
            <asp:Label ID="lblMessage" Style="forecolor:Red" runat="server"></asp:Label>
        </div>
        <asp:HiddenField ID="hdCMY" runat="server" />
        <asp:HiddenField ID="hdCUI" runat="server" />
        <div align="center" id="divReject" runat="server" visible="false" style="padding-top: 20px;">
            <table border="2">
                <tr>
                    <td>Reason:</td>
                    <td>
                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <br />
                <tr>
                    <td>Date of Rejection from Inspection agency(mm/dd/yyyy):</td>
                    <td>
                        <asp:TextBox ID="txtDate" runat="server"></asp:TextBox></td>
                </tr>



            </table>
            <div align="center">
                <asp:Button ID="btnInnerReject" Text="Submit" runat="server" CssClass="btnCls" OnClick="btnInnerReject_Click" />
            </div>
        </div>

    </div>
</asp:Content>

