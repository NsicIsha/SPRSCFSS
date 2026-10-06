<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SCST.ascx.cs" Inherits="appSCST_SCST" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr valign="top" align="center" style="background-color:#DDF1F7;">
        <td valign="middle" align="left" style="width:162px; padding-left:5px; height:78px;">
            <a href="Home.aspx">
                <img alt="NSIC MIS" width="162" height="73" border="0" src="../Images/nsic-logo.gif" />
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
                        <%--<a href="./Home.aspx">Home</a>&nbsp;|&nbsp;<a href="./UpdateProfile.aspx">Update Profile</a>&nbsp;|&nbsp;<a href="./ChangePassword.aspx">Change Password</a>&nbsp;|&nbsp;<a href="./Logout.aspx" onclick="return confirm('Are you sure you want to leave !');">Logout</a>--%>
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