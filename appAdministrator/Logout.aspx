<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true"
    CodeFile="Logout.aspx.cs" Inherits="appAdministrator_Logout" Title="Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 435px;">
        <tr>
            <td align="center" valign="middle" style="height: 435px;">
                <table border="0" cellpadding="0" cellspacing="1" width="400" style="background-color: #FFFFFF;">
                    <tr>
                        <td align="center" class="bodytext">
                            <font color="#22518F">You have successfully logged out.</font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <a href="Login.aspx">Login Again</a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
