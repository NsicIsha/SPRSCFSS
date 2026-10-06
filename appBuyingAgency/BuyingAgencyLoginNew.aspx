<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyingAgencyLoginNew.aspx.cs" Inherits="appBuyingAgency_BuyingAgencyLoginNew" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
            height: 100%;
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }
    </style>



    <script src="../Scripts/MD5.js"></script>

    <script src="../Scripts/sha256.js"></script>

    <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {


            id++;
            document.getElementById(imgc).src = src + '?id=' + id;

        }

    </script>

    <script language="javascript" type="text/javascript">
        function ValidateCheckBox(sender, args) {

            if (document.getElementById('<%#chkprivacy.ClientID %>').checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }


        



    </script>






    <noscript>
        <h3 style="color: #FF3300;">Javascript is not currently enabled in your browser.<br />
            You must <a href="http://support.google.com/bin/answer.py?hl=en&answer=23852" target="_blank">enable Javascript </a>to run this application correctly.
        </h3>
    </noscript>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager runat="server"></asp:ToolkitScriptManager>
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

                                    <div class="form-group input-group">
                                        <div class="input-group-addon">
                                            <span class="fa fa-user"></span>
                                        </div>
                                        <asp:TextBox ID="txtUserName" class="form-control" runat="server" AutoCompleteType="None" Width="200px" CssClass="form-control" MaxLength="500"
                                            ValidationGroup="1" TabIndex="1" placeholder="--Username--"></asp:TextBox>
                                        <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator5ssss" runat="server" ControlToValidate="txtUserName" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>




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

                                    <br />
                                </div>

                                <div class="form-group input-group">
                                    The information produced in this login contains only those Registered Msmes whose registeration cetificate with NSIC is valid till date.
                                </div>
                                <div class="form-group">
                                    <asp:TextBox autocomplete="off" runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />


                                    <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCaptcha" Display="Dynamic" ErrorMessage="Required"></asp:RequiredFieldValidator>


                                    <br />
                                    <img src="../Captcha/CaptchaControl.aspx" alt="Verification code" id="imgCaptcha" runat="server" />
                                    <a href="javascript:CaptchaRefresh('imgCaptcha','../Captcha/CaptchaControl.aspx')">
                                        <img src="../Images/captcha.jpg" alt="Verification code" title="Refresh code" style="border: 0px; height: 30px; width: 30px; vertical-align: middle;" />
                                    </a>
                                    <br />
                                    <div>
                                    </div>
                                    <div id="divCaptcha" runat="server" visible="false">
                                        <asp:Label ID="lblCaptcha" runat="server" CssClass="msgRed" />
                                    </div>


                                </div>


                                <div id="loginErrorMsg">
                                    <asp:Label ID="lblMessage" runat="server" Text="" class="text-danger"></asp:Label>
                                </div>
                                <p>

                                     


                                    <asp:CheckBox  ID="chkprivacy" Font-Size="Smaller" runat="server" Text="I agree to treat the information of this database in confidential manner." />

                                    <%--<asp:CustomValidator ValidationGroup="1" ClientValidationFunction="ValidateCheckBox" ID="RequiredFieldValidator2" runat="server" ControlToValidate="chkprivacy" Display="Dynamic" ErrorMessage="Required"></asp:CustomValidator>--%>


                                </p>
                                <asp:Button ID="btnSubmit" CausesValidation="true" runat="server" ValidationGroup="1" Text="Submit"
                                    class="btn btn-success btn-block" TabIndex="3" OnClick="btnSubmit_Click" />

                                <div class="text-center" style="display: none;">
                                    <asp:LinkButton ID="lnkForgetPasswordd" CausesValidation="false" runat="server"
                                        Text="Forgot Password" Width="115px">
                                    </asp:LinkButton>

                                </div>

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



        <asp:Panel ID="pnlForgotPassword" runat="server" Style="display: none;">
            <asp:Panel ID="pnlInner" runat="server" Width="450px" Height="160px" BackColor="#ECF8FB">
                <table width="400" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3" align="left" class="header" style="height: 15px; padding-left: 15px; padding-top: 5px;">Forgot Password</td>
                    </tr>
                    <tr>
                        <td valign="top" align="left"></td>
                        <td height="25" colspan="2" align="left">
                            <asp:Label Visible="false" ID="lblMsgFGP" CssClass="errMessage" runat="server"></asp:Label>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px; padding-right: 5px;" align="right" class="leftHeading">User Name:</td>
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
                        <td height="10" align="left"></td>
                        <td height="10" colspan="2"></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3" style="padding-top: 5px; padding-left: 20px;">
                            <asp:Button ID="btnSubmitFGP" runat="server" ValidationGroup="3" Text="Submit"
                                CssClass="btnCls" TabIndex="2" OnClick="btnSubmitFGP_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnCanclFGP" runat="server" Text="Cancel" CausesValidation="false" CssClass="btnCls" TabIndex="3" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="right"></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td align="center"></td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </asp:Panel>
        </asp:Panel>
        <%-- <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkForgetPasswordd"
        PopupControlID="pnlForgotPassword" CancelControlID="btnCanclFGP" 
        BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
        BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>--%>
        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
        Display="None" ErrorMessage="Please Provide User Name" SetFocusOnError="True"
        ValidationGroup="1">Please Provide User Name</asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPassword"
        Display="None" ErrorMessage="Please Provide Password" SetFocusOnError="True"
        ValidationGroup="1">Please Provide Password</asp:RequiredFieldValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
    </asp:ValidatorCalloutExtender>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2">
    </asp:ValidatorCalloutExtender>--%>

        <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>
    </form>
</body>
</html>
