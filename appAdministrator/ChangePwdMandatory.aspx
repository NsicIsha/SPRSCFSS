<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePwdMandatory.aspx.cs" Inherits="appAdministrator_ChangePwdMandatory" %>

 

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Header_Admin.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Single Point Registration (SPR)</title>

    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
    <script language="javascript" type="text/javascript" src="../JScript/jquery-1.7.1.js"></script>

    <script src="../JScript/jquery-1.3.2-vsdoc.js" type="text/javascript"></script>

    <script src="../JScript/jquery-1.3.2.js" type="text/javascript"></script>

    <script src="../JScript/ui.core.js" type="text/javascript"></script>

    <script src="../JScript/ui.progressbar.js" type="text/javascript"></script>
    <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
    
    <link href="../CSS/ui.all.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.base.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.core.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.progressbar.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/ui.theme.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript" src="../JScript/JSAdmin.js"></script>


    <!-- Global site tag (gtag.js) - Google Analytics -->
<script async src="https://www.googletagmanager.com/gtag/js?id=UA-178516803-1"></script>
<script>
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
    gtag('js', new Date());

    gtag('config', 'UA-178516803-1');
</script>




    <script src="../Scripts/MD5.js"></script>
    <script src="../Scripts/SHA512_Read.js"></script>


    <script src="../Scripts/sha256.js"></script>


    <script language="javascript" type="text/javascript">

        function ValidateOldPwdd() {

            if ((document.getElementById('<%=txtCurrentPassword.ClientID %>').value != "")) {
                var Pass = document.getElementById('<%=txtCurrentPassword.ClientID %>').value;

                var finalpass;
                var MD5New = sha256_digest(Pass + "d2f*jk#g&h");
               // alert(MD5New)
                //finalpass = MD5New + seed;
                //var MD5 = sha256_digest(finalpass);
               // alert(MD5New);
              <%--  document.getElementById('<%=hdnSetPassword.ClientID %>').value = MD5;--%>
                document.getElementById('<%=txtCurrentPassword.ClientID %>').value = MD5New ;
                return true;
            }
            else {
             <%--   document.getElementById('<%=txtUserName.ClientID %>').focus();--%>
                return false;
            }
        }
        function ValidateNew() {

            var NewPass = document.getElementById('<%=txtNewPassword.ClientID %>').value;
            //document.getElementById('<%=hdn_Clr_Pwd.ClientID %>').value = NewPass;


            var regExp = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/
            var chkValue = NewPass.search(regExp)
            if (NewPass == "") {

                return false
            }
           <%-- else if (chkValue == -1) {
                
                document.getElementById('<%=txtNewPassword.ClientID %>').value = "";
                return false;
            }--%>
            else {

                var NewMD5 = sha256_digest(NewPass + "d2f*jk#g&h");

                //alert(NewMD5)
                // document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
               /* alert(NewMD5 + "A@*");*/
                document.getElementById('<%=txtNewPassword.ClientID %>').value = NewMD5 + "A@*";
            }

    }



    function ValidateConfirmnew() {
        var ConNewPass = document.getElementById('<%=txtConfirmNewPassword.ClientID %>').value;

            var regExp = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/
            var chkValue = ConNewPass.search(regExp)
            if (ConNewPass == "") {

                return false
            }
          <%--  else if (chkValue == -1) {
                document.getElementById('<%=txtConfirmNewPassword.ClientID %>').value = "";
                return false;
            }--%>
            else {

                var NewMD5 = sha256_digest(ConNewPass + "d2f*jk#g&h");
               /* alert(NewMD5 + "A@*");*/
                document.getElementById('<%=txtConfirmNewPassword.ClientID %>').value = NewMD5 + "A@*";
            }
    }

    </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ToolkitScriptManager EnablePartialRendering="true"
        LoadScriptsBeforeUI="true" ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePageMethods="true"  >
        </asp:ToolkitScriptManager>
       <%--CHANGE PASSWORD--%>
    <div id="dvContent2" style="height: 100%; width: 100%;">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top" align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table border="0" cellpadding="1" cellspacing="0" width="100%"
                                style="border-collapse: collapse;">
                               <tr>
                    <td colspan="2">
                        <uc1:Header_Admin ID="Header_Admin1" runat="server" />
                    </td>
                </tr>

   <tr>
                                    <td>
                                        <table border="0" cellpadding="8" cellspacing="0" align="center" width="900px">
                                            <tr>
                                                <td align="right" style="width: 35%"><span class="mandatory">*</span>Old Password :
                                                </td>
                                                <td align="left" colspan="2">
                                                    <input id="txtCurrentPassword" type="password" autocomplete="off" runat="server" onblur="ValidateOldPwdd()" />
                                                    <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server"
                                                        ControlToValidate="txtCurrentPassword" ErrorMessage="Password is required."
                                                        ToolTip="Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="mandatory">*</span>New Password :
                                                </td>
                                                <td align="left">
                                                    <input id="txtNewPassword" type="password" autocomplete="off" runat="server" onblur="ValidateNew()" />
                                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                                        ControlToValidate="txtNewPassword" ErrorMessage="New Password is required."
                                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    <br />
                                                    
                                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtNewPassword" ValidationGroup="ChangePassword1" CssClass="validation-error" Display="dynamic" SetFocusOnError="True" runat="server"
                                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,150}$" ErrorMessage="Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=]"></asp:RegularExpressionValidator>


                                                   
                                                    <asp:HiddenField ID="hdn_Pwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_ConPwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_Clr_Pwd" runat="server" />

                                                    <%--<asp:CustomValidator ID="CVNewPassword" runat="server" ClientValidationFunction="NewPasswordCheck"  
                                                                              Display="dynamic" ErrorMessage="New password can not be same as current password" Font-Size="Small" ValidationGroup="ChangePassword1" SetFocusOnError="True"></asp:CustomValidator>--%>
                                                </td>
                                                <td style="width:90%">
                                                    <asp:Label ID="pwdmessage" runat="server" Font-Bold="true" ForeColor="Red" Text=" Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=]"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right">
                                                    <span class="mandatory">*</span>Confirm New Password :
                                                </td>
                                                <td align="left" colspan="2">
                                                    <input id="txtConfirmNewPassword" type="password" autocomplete="off" runat="server" onblur="ValidateConfirmnew()" />
                                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                                        ControlToValidate="txtConfirmNewPassword"
                                                        ErrorMessage="Confirm New Password is required."
                                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>

                                                    <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"
                                                Display="Dynamic" CssClass="validation-error"
                                                ErrorMessage="*"
                                                ValidationGroup="ChangePassword1"></asp:CompareValidator>

                                                   <%-- <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                                        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"
                                                        Display="Dynamic"
                                                        ErrorMessage="*"
                                                        ValidationGroup="ChangePassword1"></asp:CompareValidator>--%>

                                                </td>
                                            </tr>

                                           
                                            <tr>
                                                <td align="center" colspan="3" style="color: Red;">
                                                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>

                                                   
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="left" colspan="2">
                                                    <asp:Button ID="btnPassword" runat="server"
                                                        Text="Change Password" SkinID="btnLarge" OnClick="btnPassword_Click"
                                                        ValidationGroup="ChangePassword1" />
                                                    &nbsp;&nbsp;
                                                                                     
                                                                                <input id="Reset1" type="reset" value="Clear" class="btn-normal" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                               
                <tr>
                    <td colspan="2">
                        <uc2:Footer_Admin ID="Footer_Admin1" runat="server" />
                    </td>
                </tr>



                             
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
