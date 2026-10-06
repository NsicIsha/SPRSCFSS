<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgot_Pwd.aspx.cs" Inherits="Forgot_Pwd" %>



<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="rsv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SINGLE POINT REGISTRATION - SPRS</title>

    <link href="../CSS/bootstrap.min.css" rel="stylesheet" />
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <%--
    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    --%>
    <script src="Scripts/SHA512_Read.js"></script>
    <script src="Scripts/MD5.js"></script>
    <style>
        body {
            background-image: url("../Images/NewCommonImg.jpg");
            /* Full height */
            height: 100%;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }
    </style>

<%--       <script src="../Scripts/MD5.js"></script>
    <script src="../Scripts/SHA512_Read.js"></script>--%>


    <script src="Scripts/sha256.js"></script>
     <script language="javascript" type="text/javascript">


        

        
        function ValidateNew() {

            var NewPass = document.getElementById('<%=txtNewPassword.ClientID %>').value;
            


            var regExp = /^.*(?=.{8,})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+=]).*$/
            var chkValue = NewPass.search(regExp)
             

            if (NewPass == "") {

                return false
            }
                else if (chkValue == -1) {
                
                document.getElementById('<%=txtNewPassword.ClientID %>').value = "";
                return false;
            }
          
            else {

                var NewMD5 = sha256_digest(NewPass + "d2f*jk#g&h");
                //var newMd6 = NewMD5 + "A@*"
                //alert(newMd6)
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
          
            else {

                var NewMD5 = sha256_digest(ConNewPass + "d2f*jk#g&h");
                 
                //var newMd6 = NewMD5 + "A@*"
                //alert(newMd6)

                document.getElementById('<%=txtConfirmNewPassword.ClientID %>').value = NewMD5 + "A@*";
            }
    } 

    </script>


     <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {
            
            //debugger;
            id++;
            document.getElementById(imgc).src = src + '?id=' + id;
            
        }

    </script>

</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <div class="row center-block">
            <img class="center-block" src="../Images/newNS.png" alt="National Small Industries Corporation" style="width: 200px; height: 140px;">
        </div>
        <div id="login-overlay" class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title text-primary text-uppercase text-center" id="myModalLabel"><b>Welcome to Single Point Registration</b></h4>

                </div>
                <div class="modal-body">
                    <div class="row">
                        <div  id="forgetpwd" runat="server">
                            <div class="col-lg-7">

                            
                            <div class="well">
                                <div class="form-group">

                                    <div class="form-group input-group">
                                        <div class="input-group-addon">
                                            <span class="fa fa-user"></span>
                                        </div>
                                        <asp:TextBox autocomplete="off" ID="txtUserName" class="form-control" runat="server" AutoCompleteType="None" Width="200px" CssClass="form-control" MaxLength="500"
                                            ValidationGroup="1" TabIndex="1" placeholder="--Username--"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="1" ID="rfvusername" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>


                                    </div>
                                </div>
                                <div class="form-group input-group">

                                    <div class="input-group-addon"><span class="fa fa-lock"></span></div>
                                    <asp:TextBox autocomplete="off" ID="txtRegEmailID" class="form-control" runat="server" Width="200px" CssClass="form-control" MaxLength="100"
                                        ValidationGroup="1" placeholder="--Reg. Email ID--" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRegEmailID" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                    <span class="help-block"></span>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox autocomplete="off" runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                                    <rsv:CaptchaControl ID="captcha1" runat="server" CaptchaLength="5" CaptchaHeight="50"
                                        CaptchaWidth="250" ValidationGroup="1" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                        ForeColor="#00FFCC" BackColor="White" CaptchaChars="ABCDEFGHJKMNPQRSTUVWXYZ123456789"
                                        FontColor="RED" placeholder="Verification Code" />

                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" CssClass="validation-error"
                                            ControlToValidate="txtCaptcha" Display="Dynamic"
                                            ErrorMessage="Enter Captcha Value."
                                            ToolTip="Enter Captcha Value." ValidationGroup="1"></asp:RequiredFieldValidator>

                                </div>




                                <div id="loginErrorMsg">
                                    <asp:Label ID="lblMessage" runat="server" Visible="false" Text="Invalid User Name or Password !" class="text-danger"></asp:Label>
                                </div>
                               <%-- <p><small>HO / Zonal(s) / Branch head(s) / Branch Accounts / Branch User(s) may use their respective user id / Password to enter SPRS(GP) Application.</small></p>--%>
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="1" Text="Submit"
                                    class="btn btn-success btn-block" TabIndex="3" OnClick="btnSubmit_Click" />


                            </div>
                            </div>
                            <div class="col-lg-5">
                            <div class="well">
                                <p class="legend">Services</p>
                                <ul class="media-list border-btm">

                                    <a target="_blank" href="http://www.nsicffconline.in/">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; FFC
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://msmemart.com/">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; B2B
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://nsic.co.in/NTSC/NTSC-Okhla.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; Student Training
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://nsic.co.in/Schemes/MarketingAssistance.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; RMA BG
               
                                            </div>
                                        </li>
                                    </a>

                                     <a target="_blank" href="../Forgot_Pwd.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; Forgot Password
               
                                            </div>
                                        </li>
                                    </a>
                                </ul>
                                <p style="margin-top: 12px">
                                    <a target="_blank" href="http://www.nsic.co.in/" class="btn btn-info btn-home btn-block">Corporate
            Website
                                    </a>
                                </p>
                            </div>
                            <%--<div class="well">
                                <p><b>NOTE:</b><br /><br /></p>
                            </div>--%>
                        </div>

                        </div>

                        <div style="padding-top: 6px;" id="pwdcontinue" runat="server" visible="false">
                            <div class="col-lg-7" >
                                <div class="form-group">
                                    <input class="form-control" id="txtNewPassword" type="password" placeholder="--New Password--" autocomplete="off" runat="server" onblur="ValidateNew()" /><br />
                                    <input id="inpHide" type="hidden" runat="server" />
                                    <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" CssClass="validation-error"
                                        ControlToValidate="txtNewPassword" ErrorMessage="New Password is required." Display="Dynamic"
                                        ToolTip="New Password is required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revNewPwd" ControlToValidate="txtNewPassword" ValidationGroup="ChangePassword1" CssClass="validation-error" Display="dynamic" SetFocusOnError="True" runat="server"
                                        ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,150}$" ErrorMessage="Password must be at least 8 characters long having atleast one lower case, one upper case, one numeric and one special character such as [@#$%^&+=]"></asp:RegularExpressionValidator>

                                    <asp:HiddenField ID="hdn_Pwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_ConPwd" runat="server" />
                                                    <asp:HiddenField ID="hdn_Clr_Pwd" runat="server" />

                                </div>
                                <div class="form-group">
                                 
                                        <input class="form-control" placeholder="--Confirm New Password--" id="txtConfirmNewPassword" type="password" autocomplete="off" runat="server" onblur="ValidateConfirmnew()" /><br />
                                        <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" CssClass="validation-error"
                                            ControlToValidate="txtConfirmNewPassword" Display="Dynamic"
                                            ErrorMessage="Confirm New Password is required."
                                            ToolTip="Confirm New Password is required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>
                                    
                               
                                            <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"
                                                Display="Dynamic" CssClass="validation-error"
                                                ErrorMessage="*"
                                                ValidationGroup="ChangePassword1"></asp:CompareValidator>
                                       
                                   
                             
                        </div>

                                 <div class="form-group">
                                 
                                        <input class="form-control" placeholder="--Enter OTP Received on Email--" id="txtOTP"  autocomplete="off" runat="server"  /><br />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" CssClass="validation-error"
                                            ControlToValidate="txtOTP" Display="Dynamic"
                                            ErrorMessage="OTP Received on your email id is Required."
                                            ToolTip="OTP Received on your email id is Required." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>
                                    
                               
                                           <%-- <asp:CompareValidator ID="NewPasswordCompare" runat="server"
                                                ControlToCompare="txtNewPassword" ControlToValidate="txtConfirmNewPassword"
                                                Display="Dynamic" CssClass="validation-error"
                                                ErrorMessage="New Password & Confirm Password not match."
                                                ValidationGroup="ChangePassword1"></asp:CompareValidator>--%>
                                       
                                
                                
                             
                        </div>

                                <div class="form-group">
                                     <asp:TextBox autocomplete="off" runat="server" ID="txtCaptchaOTP" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                                    

                                    <br />
                                    <img src="Captcha/CaptchaControl.aspx" alt="Verification code" id="imgCaptcha" runat="server" />
                                   <a href="javascript:CaptchaRefresh('imgCaptcha','Captcha/CaptchaControl.aspx')">
                                        <img src="Images/captcha.jpg" alt="Verification code" title="Refresh code" style="border: 0px; height: 30px; width: 30px; vertical-align: middle;" />
                                    </a>
                                    <br />
                                    <div>
                                        
                                    </div>
                                    <div id="divCaptcha" runat="server" visible="false">
                                        <asp:Label ID="lblCaptcha" runat="server" CssClass="msgRed" />
                                    </div>

                                   <%-- <asp:TextBox autocomplete="off" runat="server" ID="txtCaptchaOTP" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                                    <rsv:CaptchaControl ID="CaptchaControl11" runat="server" CaptchaLength="7" CaptchaHeight="50"
                                        CaptchaWidth="200" ValidationGroup="ChangePassword1" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240" 
                                        ForeColor="#00FFCC" BackColor="White" CaptchaChars="ABCDEFGHJKMNPQRSTUVWXYZ123456789"
                                        FontColor="RED" placeholder="Verification Code" />
                                      <asp:RequiredFieldValidator ID="RequiredFieldValida55tor3" runat="server" CssClass="validation-error"
                                            ControlToValidate="txtCaptchaOTP" Display="Dynamic"
                                            ErrorMessage="Enter Captcha Value."
                                            ToolTip="Enter Captcha Value." ValidationGroup="ChangePassword1"></asp:RequiredFieldValidator>
                                    --%>

                                </div>


                       <div class="form-group">
                                     <span style="color:red"> <asp:Literal ID="FailureText"  runat="server" EnableViewState="False"></asp:Literal></span>
                                        <asp:Button ID="btnContinue" runat="server" ValidationGroup="ChangePassword1"
                                            class="btn btn-success btn-block" Text="Continue" OnClick="btnContinue_Click" />   

                                </div>
                                 
                       
                                </div>
                               <div class="col-lg-5">
                            <div class="well">
                                <p class="legend">Services</p>
                                <ul class="media-list border-btm">

                                    <a target="_blank" href="http://www.nsicffconline.in/">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; FFC
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://msmemart.com/">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; B2B
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://nsic.co.in/NTSC/NTSC-Okhla.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; Student Training
               
                                            </div>
                                        </li>
                                    </a>
                                    <a target="_blank" href="http://nsic.co.in/Schemes/MarketingAssistance.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; RMA BG
               
                                            </div>
                                        </li>
                                    </a>
                                </ul>

                                <p style="margin-top: 12px">
                                    <a href="appAdministrator/Login_New.aspx" class="btn btn-info btn-home btn-block"> Login 
                                    </a>
                                </p>


                                <p style="margin-top: 12px">
                                    <a target="_blank" href="http://www.nsic.co.in/" class="btn btn-info btn-home btn-block">Corporate
            Website
                                    </a>
                                </p>
                            </div>
                             
                        </div>
                            </div>
                        
                       
                    </div>
                </div>
                <div class="modal-footer">
                    <div class="row">
                       <div class="col-lg-3">
                            <a href="../UseFulLinks.aspx" target="_blank">Related Links</a>
                        </div>
                        <div class="col-lg-3">
                            <a href="../Disclaimer.aspx" target="_blank">Disclaimers</a>
                        </div>
                        <div class="col-lg-3">
                            <a href="../PrivacyPolicy.aspx" target="_blank">Privacy Policy</a>
                        </div>
                        <div class="col-lg-3">
                            <a href="../TermOfUse.aspx" target="_blank">Terms of Use</a>
                        </div>
                    </div>
                    <div class="row text-center">
                        <p>
                            <b>Copyright : </b>National Small Industries Corporation
                    © 2019-2020 
                            <br />
                            Website Design &amp; Content Managed by NSIC
               
                        </p>
                    </div>

                </div>
            </div>
        </div>




    </form>
</body>
</html>
