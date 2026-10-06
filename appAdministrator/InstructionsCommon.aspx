<%@ Page Title="NSIC SPR :: Instructions to fill forms" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="InstructionsCommon.aspx.cs" Inherits="appAdministrator_InstructionsCommon" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Instructions to Fill <asp:Label ID="lblHeader" runat="server"></asp:Label> Form
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="400" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <asp:Label ID="lblInstruction" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

