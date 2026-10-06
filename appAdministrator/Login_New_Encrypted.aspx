<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login_New_Encrypted.aspx.cs" Inherits="appAdministrator_Login_New_Encrypted" %>
<%--<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="rsv" %>--%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SINGLE POINT REGISTRATION - SPRS</title>

    <link href="../CSS/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
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


     <%-- <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>--%>
    <script src="../Scripts/MD5.js"></script>
    <%-- <script src="../Scripts/SHA512_Read.js"></script>--%>
    <script src="../Scripts/sha256.js"></script>

     <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {
            
            //debugger;
            id++;
            document.getElementById(imgc).src = src + '?id=' + id;
            
        }

    </script>

    <script language="javascript" type="text/javascript">

        function ValidateSeed(seed) {
            debugger;

            if ((document.getElementById('<%=txtUserName.ClientID %>').value != "") && (document.getElementById('<%=txtPassword.ClientID %>').value != "")) {
                  var Pass = document.getElementById("<%=txtPassword.ClientID %>").value;
                document.getElementById('<%=txtPassword.ClientID %>').value = "";
                var finalpass;

              //  alert(seed);
               
                var MD5New = sha256_digest(Pass + "d2f*jk#g&h");

               // alert(MD5New);

                finalpass = MD5New + seed;

               // alert(finalpass);


                var MD5 = sha256_digest(finalpass);

               // alert(MD5);


                document.getElementById('<%=hdnSetPassword.ClientID %>').value = MD5;
                document.getElementById('<%=txtPassword.ClientID %>').value = MD5;
                document.getElementById('<%=HiddenField1.ClientID %>').value = seed;
                

               
                return true;
            }
            else {
                document.getElementById('<%=txtUserName.ClientID %>').focus();
                return false;
            }
        }
    </script>


</head>
<body>
    <form id="form1" runat="server">
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
                        <div class="col-lg-7">
                            <div class="well">
                                <div class="form-group">
                                  <%--  <asp:Label Text="Username" CssClass="control-label" runat="server" />--%>
                                    <div class="form-group input-group">
                                        <div class="input-group-addon">
                                            <span class="fa fa-user"></span>
                                        </div>
                                        <asp:TextBox ID="txtUserName" class="form-control" runat="server" AutoCompleteType="None" Width="200px" CssClass="form-control" MaxLength="500"
                                            ValidationGroup="1" TabIndex="1" placeholder="--Username--"></asp:TextBox>



                                    </div>
                                </div>
                              
                                 <div class="form-group input-group">

                                    <div class="input-group-addon"><span class="fa fa-lock"></span></div>
                                    <asp:TextBox autocomplete="off" ID="txtPassword" class="form-control" runat="server" Width="200px" CssClass="form-control" MaxLength="100"
                                        TextMode="Password" ValidationGroup="1" placeholder="--Password--" TabIndex="2"></asp:TextBox>

                                    <asp:HiddenField ID="hdnSetPassword" runat="server" />
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPassword" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                    <span class="help-block"></span>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox autocomplete="off" runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                                    <%--<rsv:CaptchaControl ID="captcha1" runat="server" CaptchaLength="7" CaptchaHeight="50"
                                       CaptchaWidth="200" ValidationGroup="1" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                        ForeColor="#00FFCC" BackColor="White" CaptchaChars="ABCDEFGHJKMNPQRSTUVWXYZ123456789"
                                        FontColor="RED" placeholder="Verification Code" />--%>

                                    <br />
                                    <img src="../Captcha/CaptchaControl.aspx" alt="Verification code" id="imgCaptcha" runat="server" />
                                    <a href="javascript:CaptchaRefresh('imgCaptcha','../Captcha/CaptchaControl.aspx')">
                                        <img src="../Images/captcha.jpg" alt="Verification code" title="Refresh code" style="border: 0px; height: 30px; width: 30px; vertical-align: middle;" />
                                    </a>
                                    <br />
                                    <div>
                                       <%-- [ Verification code is not case sensitive ]--%>
                                    </div>
                                    <div id="divCaptcha" runat="server" visible="false">
                                        <asp:Label ID="lblCaptcha" runat="server" CssClass="msgRed" />
                                    </div>

                                    

                                </div>

                                 <%-- <div class="form-group">
                            <asp:TextBox runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                            <rsv:CaptchaControl ID="captcha1" runat="server" CaptchaLength="3" CaptchaHeight="50"
                                CaptchaWidth="200" CaptchaLineNoise="None" CaptchaMinTimeout="5" CaptchaMaxTimeout="240"
                                ForeColor="#00FFCC" BackColor="White" CaptchaChars="0123456789"
                                FontColor="RED" placeholder="Verification Code" />
                        </div>--%>
                                <div id="loginErrorMsg">
                                    <asp:Label ID="lblMessage" runat="server" Text="" class="text-danger"></asp:Label>
                                </div>
                                <p><small>HO / Zonal(s) / Branch head(s) / Branch Accounts / Branch User(s) may use their respective user id / Password to enter SPRS(GP) Application.</small></p>
                                <asp:Button ID="btnSubmit" runat="server" ValidationGroup="1" Text="Submit"
                                    class="btn btn-success btn-block" TabIndex="3" OnClick="btnSubmit_Click" />

                              <%--  <div class="text-center">
                                    <asp:LinkButton ID="lnkForgetPassword" CausesValidation="false" runat="server"
                                        Text="Forgot Password" Width="115px">
                                    </asp:LinkButton>

                                </div>--%>

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

                                     <a href="../Forgot_Pwd.aspx">
                                        <li class="media">
                                            <div class="media-body text-default text-semibold">
                                                <i class="fa fa-angle-double-right"></i>&nbsp; Forget Password
               
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
