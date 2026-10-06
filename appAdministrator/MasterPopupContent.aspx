<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/PopupAdmin.master" AutoEventWireup="true"
    CodeFile="MasterPopupContent.aspx.cs" Inherits="appAdministrator_MasterPopupContent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblHeader" runat="server" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height: 20px;">
    </div>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr align="left" valign="top" style="width: 100%;">
            <td align="left" valign="top" class="leftHeading" style="width: 10%;"><%--AD--%>
                NAME :
            </td>
            <td align="left" valign="top">
                <asp:Literal ID="ltName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                CONTENT :
            </td>
            <td align="left" valign="top" style="text-align: justify;">
                <asp:Label ID="lblContent" runat="server"></asp:Label>
            </td>
        </tr>
        <tr align="left" valign="top">
            <td align="left" valign="top">
            </td>
            <td align="right" valign="top">
                <%--<asp:Button ID="btnClose" runat="server" CssClass="btnCls" Text="CLOSE" />--%>
            </td>
        </tr>
    </table>
</asp:Content>
