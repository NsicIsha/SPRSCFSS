<%@ Page Title="Logout" Language="C#" MasterPageFile="~/appMaster/LoginMaster.master" AutoEventWireup="true" CodeFile="Logout.aspx.cs" Inherits="appInspectionAgency_Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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

