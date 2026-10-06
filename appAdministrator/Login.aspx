<%@ Page Language="C#" MasterPageFile="~/appMaster/LoginMaster.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="appAdministrator_Login" Title="Single Point Registration scheme (SPRS)" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <noscript> <h3 style="color: #FF3300;">
Javascript is not currently enabled in your browser.<br /> You must <a href="http://support.google.com/bin/answer.py?hl=en&answer=23852" target="_blank" >enable Javascript </a> to run this application correctly.
</h3></noscript>

<script type="text/javascript" language ="javascript">
      function getInternetExplorerVersion()
       // Returns the version of Windows Internet Explorer or a -1
       // (indicating the use of another browser).
       {
           var rv = -1; // Return value assumes failure.
           if (navigator.appName == 'Microsoft Internet Explorer') {
               var ua = navigator.userAgent;
               var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
               if (re.exec(ua) != null)
                   rv = parseFloat(RegExp.$1);
           }
           return rv;
       }

       function checkVersion() {
           var msg = "Single Point Registration Scheme system is optimised for Internet Explorer 8 or above. Some of the features may not work in this browser.";
           var ver = getInternetExplorerVersion();
           if (ver > -1) {
               if (ver >= 8.0) {
                   // msg = "You're using a recent copy of Windows Internet Explorer."
               }
               else if (ver <= 8.0) {
               msg = "Single Point Registration Scheme system is optimised for Internet Explorer 8 or above. Some of the features may not work in this browser. Please upgarde your browser.";
                   alert(msg);
               }
           }
           else {
               alert(msg);
           }
       }

//       $(window).load(function() {
//          
//           checkVersion();

       //       });

     
 </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="height: 435px;">
        <tr>
            <td align="center" valign="middle" style="height: 435px;">
                <table border="0" cellpadding="0" cellspacing="1" width="400" style="background-color: #C6E7F0;">
                    <tr valign="top">
                        <td colspan="2" align="left" valign="middle" style="background-color: #22518F; height: 35px;
                            padding-left: 5px; font-weight: bold; color: #FFFFFF;">
                            Login
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2" align="left" valign="middle" style="background-color: #DDF1F7; height:35px;
                            padding-left: 5px;" class="errMessage">
                            <asp:Label ID="lblMessage" runat="server" Text="Invalid User Name or Password !"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" valign="middle" style="width: 100px; background-color: #DDF1F7;" class="leftHeading">
                            User Name :
                        </td>
                        <td align="left" valign="middle" style="background-color: #DDF1F7;" class="leftHeading">
                            <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="txt_bx" MaxLength="500"
                                ValidationGroup="1" TabIndex="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="left" valign="middle" style="width: 100px; background-color: #DDF1F7;" class="leftHeading">
                            Password :
                        </td>
                        <td align="left" valign="middle" style="background-color: #DDF1F7;" class="leftHeading">
                            <asp:TextBox ID="txtPassword" runat="server" Width="200px" CssClass="txt_bx" MaxLength="100"
                                TextMode="Password" ValidationGroup="1" TabIndex="2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2"  valign="middle" 
                            style="width: 100%; background-color: #DDF1F7;" align="left" 
                            class="leftHeading" >
                           HO/Zonal(s)/Branch head(s)/Branch Accounts/Branch User(s) may use their respective user id / Password to enter SPRS(GP) Application.</td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2" align="right" valign="middle" style="width: 100%; background-color: #DDF1F7;" class="leftHeading">
                            <asp:LinkButton ID="lnkForgetPassword" CausesValidation="false" runat="server" 
                                Text="Forgot Password" Width="115px">
                            </asp:LinkButton>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td colspan="2" align="center" valign="middle" style="background-color: #DDF1F7;
                            height: 50px; padding-left: 5px; font-weight: bold; color: #22518F;">
                            <asp:Button ID="btnSubmit" runat="server" ValidationGroup="1" Text="Submit" 
                                CssClass="btnCls" TabIndex="3" onclick="btnSubmit_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlForgotPassword" runat="server" style="display:none;">
        <asp:Panel ID="pnlInner" runat="server" Width="450px" Height="160px" BackColor="#ECF8FB">
            <table width="400" border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="3" align="left" class="header" style="height: 15px; padding-left: 15px;
                        padding-top: 5px;">
                        Forgot Password</td>
                </tr>
                <tr>
                    <td valign="top" align="left">
                    </td>
                    <td height="25" colspan="2" align="left">
                        <asp:Label Visible="false" ID="lblMsgFGP" CssClass="errMessage" runat="server"></asp:Label>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 120px; padding-right: 5px;" align="right" class="leftHeading">
                        User Name:</td>
                    <td colspan="2" align="left" style="width: 280px;">
                        <asp:TextBox ID="txtUserNameFGP" runat="server" ValidationGroup="3" CssClass="txt_bx" MaxLength="500"
                            TabIndex="1"></asp:TextBox>
                        <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator3" runat="server"
                            Display="None" ErrorMessage="User name is required" ControlToValidate="txtUserNameFGP"
                            ValidationGroup="3"></asp:RequiredFieldValidator>
                        <asp:ValidatorCalloutExtender
                                Width="250px" ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RequiredFieldValidator3">
                        </asp:ValidatorCalloutExtender>                        
                    </td>
                </tr>
                <tr>
                    <td height="10" align="left">
                    </td>
                    <td height="10" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="padding-top: 5px; padding-left: 20px;">
                        <asp:Button ID="btnSubmitFGP" runat="server" ValidationGroup="3" Text="Submit" 
                            CssClass="btnCls" TabIndex="2" onclick="btnSubmitFGP_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCanclFGP" runat="server" Text="Cancel" CausesValidation="false" CssClass="btnCls" TabIndex="3" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="right">
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td align="center">
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkForgetPassword"
        PopupControlID="pnlForgotPassword" CancelControlID="btnCanclFGP" 
        BackgroundCssClass="modalBackground" >
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName"
        Display="None" ErrorMessage="Please Provide User Name" SetFocusOnError="True"
        ValidationGroup="1">Please Provide User Name</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
        Display="None" ErrorMessage="Please Provide Password" SetFocusOnError="True"
        ValidationGroup="1">Please Provide Password</asp:RequiredFieldValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
    </asp:ValidatorCalloutExtender>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
    </asp:ValidatorCalloutExtender>
    
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>
</asp:Content>

