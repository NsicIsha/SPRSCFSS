<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Header_Admin.ascx.cs" Inherits="UserControl_Header_Admin" %>

<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr valign="top" align="center" style="background-color:#DDF1F7;">
        <td valign="middle" align="left" style="width:162px; padding-left:5px; height:78px;">
            <a href="Home.aspx">
              
                  <img alt="NSIC MIS" height="100"   border="0"  src="../Images/newNS.png" />

                
            </a>
        </td>
        <td valign="middle" align="right" style="font-family:Verdana; font-size:25px; font-weight:bold; color:#373737; background-color:#DDF1F7; padding-right:5px;">
            Single Point Registration (<span style="font-family:Verdana; font-size:25px; font-weight:bold; color:#69C3DB;">SPR</span>)
        </td>
    </tr>
    <tr valign="top" align="center" style="background-color:#CBEAF3;">
        <td colspan="2" valign="top" align="left" style="width:100%;">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr align="left" valign="top">
                    <td valign="middle" align="left" style="width:500px; padding-left:5px; height:30px;">
                        <asp:Label ID="lblUser" runat="server" ForeColor="#22518F"></asp:Label>
                    </td>
                    <td valign="middle" align="right" style="padding-right:15px; width:500px;" class="headerLink">
                        <asp:Label ID="lblLink" runat="server" CssClass="headerLink" ></asp:Label>
                       
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr valign="top" align="center">
        <td colspan="2" valign="top" style="height:1px; background-image:url(../Images/blueline.JPG); background-repeat:repeat-x;">
        </td>
    </tr>
    <tr valign="top" align="center">
        <td colspan="2" valign="top" style="height:1px; background-color:#FFFFFF;">
        </td>
    </tr>
</table>