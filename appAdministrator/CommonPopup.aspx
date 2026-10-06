<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/PopupAdmin.master" AutoEventWireup="true" CodeFile="CommonPopup.aspx.cs" Inherits="CommonPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     
    <div align="left">
        <asp:Label ID="lblHeader" runat="server" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height: 20px;">
    </div>
    <table border="0" cellpadding="0" cellspacing="1" width="100%">
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading" style="width: 10%;">
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
            </td>
        </tr>
    </table>

</asp:Content>

