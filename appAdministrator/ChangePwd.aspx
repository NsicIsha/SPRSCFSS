<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ChangePwd.aspx.cs" Inherits="appAdministrator_ChangePwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../Scripts/MD5.js"></script>
    <script src="../Scripts/SHA512_Read.js"></script>


    <script src="../Scripts/sha256.js"></script>
    <script language="javascript" type="text/javascript">

        function ValidateOldPwdd() {

            if ((document.getElementById('<%=txtCurrentPassword.ClientID %>').value != "")) {
                var Pass = document.getElementById('<%=txtCurrentPassword.ClientID %>').value;

                var finalpass;
                var MD5New = sha256_digest(Pass + "d2f*jk#g&h");
                //finalpass = MD5New + seed;
                //var MD5 = sha256_digest(finalpass);
              <%--  document.getElementById('<%=hdnSetPassword.ClientID %>').value = MD5;--%>
                document.getElementById('<%=txtCurrentPassword.ClientID %>').value = MD5New;
                return true;
            }
            else {
             <%--   document.getElementById('<%=txtUserName.ClientID %>').focus();--%>
                return false;
            }
        }
        function ValidateNew() {

            var NewPass = document.getElementById('<%=txtNewPassword.ClientID %>').value;
           
            var regExp = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/
            var chkValue = NewPass.search(regExp)
            if (NewPass == "") {

                return false
            }
            else if (chkValue == -1) {
               
                alert('Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=] e.g. Test@1234')
                document.getElementById('<%=txtNewPassword.ClientID %>').value = "";
                return false;
            }
            else {

                var NewMD5 = sha256_digest(NewPass + "d2f*jk#g&h");
                // document.getElementById("<%=txtNewPassword.ClientID %>").value = "";
                document.getElementById('<%=txtNewPassword.ClientID %>').value = NewMD5;
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
                 
                document.getElementById('<%=txtConfirmNewPassword.ClientID %>').value = NewMD5;
            }
    }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <td>
                                        <table border="0" cellpadding="8" cellspacing="0" align="center" width="600px">
                                            <tr>
                                                <td align="right" style="width: 35%"><span class="mandatory">*</span>Old Password :
                                                </td>
                                                <td align="left">
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
                                                    <br />
                                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server"
                                                        ControlToValidate="txtNewPassword" ErrorMessage="New Password is required."
                                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    
                                                    <%--<asp:RegularExpressionValidator ID="revNewPwd" ControlToValidate="txtNewPassword" ValidationGroup="ChangePassword1" CssClass="validation-error" Display="dynamic" SetFocusOnError="True" runat="server"
                                                        ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@.#*$=)!,&(^+%-]).{8,40})" ErrorMessage="Password must be 8 character long and contain at least one number, one lower case letter,one upper case letter and one special character"></asp:RegularExpressionValidator>--%>

                                                    <asp:HiddenField ID="hdn_Pwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_ConPwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_Clr_Pwd" runat="server" />

                                                    <%--<asp:CustomValidator ID="CVNewPassword" runat="server" ClientValidationFunction="NewPasswordCheck"  
                                                                              Display="dynamic" ErrorMessage="New password can not be same as current password" Font-Size="Small" ValidationGroup="ChangePassword1" SetFocusOnError="True"></asp:CustomValidator>--%>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="right">
                                                    <span class="mandatory">*</span>Confirm New Password :
                                                </td>
                                                <td align="left">
                                                    <input id="txtConfirmNewPassword" type="password" autocomplete="off" runat="server" onblur="ValidateConfirmnew()" />
                                                    <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server"
                                                        ControlToValidate="txtConfirmNewPassword"
                                                        ErrorMessage="Confirm New Password is required."
                                                        ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1">*</asp:RequiredFieldValidator>
                                                    <%--<asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                                        ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"
                                                        Display="Dynamic"
                                                        ErrorMessage="The confirm new password must match the new password entry."
                                                        ValidationGroup="ChangePassword1"></asp:CompareValidator>--%>

                                                </td>
                                            </tr>

                                            <tr>
                                                <td colspan="2">
                                                    <asp:Label ID="lblPwdmsg" runat="server" ForeColor="Red" Font-Bold="true" Text="Note : Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=] e.g. Test@1234"></asp:Label>
                                                </td>
                                            </tr>

                                            <%-- <tr>
                                                                            <td align="right" valign="top"><span class="mandatory">*</span>  Verification Code :</td>

                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtCaptcha" SkinID="txtDob" MaxLength="6" runat="server" AutoComplete="Off"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ValidationGroup="ChangePassword1"
                                                                                    ControlToValidate="txtCaptcha" Display="Dynamic" ErrorMessage="Verification Code Required" ForeColor="Red">*</asp:RequiredFieldValidator>
                                                                                <br />
                                                                                <img src="~/Captcha/CaptchaControl.aspx" alt="Verification code" id="imgCaptcha" runat="server" />
                                                                                <a href="javascript:CaptchaRefresh('ContentPlaceHolder1_imgCaptcha','Captcha/CaptchaControl.aspx')">
                                                                                    <img src="images/captcha.jpg" alt="Verification code" title="Refresh code" style="border: 0px; height: 30px; width: 30px; vertical-align: middle;" />
                                                                                </a>
                                                                                <br />
                                                                                <div>
                                                                                    [ Verification code is not case sensitive ]
                                                                                </div>
                                                                                <div id="divCaptcha" runat="server" visible="false">
                                                                                    <asp:Label ID="lblCaptcha" runat="server" CssClass="msgRed" />
                                                                                </div>


                                                                            </td>
                                                                        </tr>--%>
                                            <tr>
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                                <td align="left">
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
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

