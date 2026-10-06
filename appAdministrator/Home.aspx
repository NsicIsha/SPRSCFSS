<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="appAdministrator_Home" Title="NSIC SPR :: Home" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="ecmascript" type="text/javascript">
        window.onbeforeunload = function() {
        clrcook(event);
        };
        function clrcook(e) {
        var evt = window.event ? event : e;
        if (evt.clientY < 0) {
        //alert("ravish");

        var thecookie = document.cookie.split(";")
        for (var i = 0; i < thecookie.length; i++) {
        var expires = new Date();
        expires.setUTCFullYear(expires.getUTCFullYear() - 1);
        document.cookie = thecookie[i] + '; expires=' + expires.toUTCString() + '; path=/';

        }
        alert(thecookie);
        }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height:435px;">
  
       
        <tr>
            <td align="center" valign="middle" style="height:45px; font-family:Verdana; font-size:15px; font-style:italic; color:#22518F;">
                Welcome to <strong>Single Point Registration</strong>
            </td>
        </tr>
        
             <tr id="tr_delete" runat="server" visible="false">
        <td align="center" valign="middle" style=" font-family:Verdana; font-size:16px; font-style:bold; color:#f00000;font-weight:700;">
                Pending Delete Company :: <strong><asp:Label ID="lbl_delete_company" runat="server" Text=""></asp:Label></strong><br />
            </td>
       </tr>
       <tr id="tr_print_request" runat="server" visible="false">
        <td align="center" valign="middle" style=" font-family:Verdana; font-size:16px; font-style:bold; color:#f00000;font-weight:700;">
                Pending Print Request Company :: <strong><asp:Label ID="lbl_print_request" runat="server" Text=""></asp:Label></strong>
            </td>
       </tr>
    </table>
</asp:Content>

