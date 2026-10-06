<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="NewUnitRegistration_CFSS.aspx.cs" Inherits="appAdministrator_NewUnitRegistration_CFSS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
        $(function () {
            $("#progressbar").progressbar({ value: 15 });
        });
        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }
        function UpdateProgressBar_DroDown() {
            $("#progressbar").progressbar({ value: 15 });
        }
    </script>

    <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {

            imgc = "ctl00_ContentPlaceHolder1_" + imgc;

            //debugger;
            id++;
            document.getElementById(imgc).src = src + '?id=' + id;

        }





        <%--function ValidateGridRadio(sender, args) {

            var grid = document.getElementById("<%= GridView1.ClientID %>");
            var radios = grid.getElementsByTagName("input");

            for (var i = 0; i < radios.length; i++) {

                if (radios[i].type == "radio" && radios[i].checked) {
                    args.IsValid = true;
                    return;
                }

            }

            args.IsValid = false;
        }--%>



    </script>


    <%--<script type="text/javascript"> 
              //  $(document).ready( function() {              
                // When site loaded, load the Popupbox First         
                //loadPopupBox();              
                $('#popupBoxClose').click( function() {                         
                unloadPopupBox();         
                });                  
                
                $('#container').click( function() {
                             //unloadPopupBox(); 
                             loadPopupBox();                      
                   });                   
                function unloadPopupBox() {
                    // TO Unload the Popupbox             
                    $('#popup_box').fadeOut("slow");             
                    $("#container").css({ // this is just for style                         
                    "opacity": "1"               
                    });
        } 
               function loadPopupBox() {
                   // To Load the Popupbox             
                   $('#popup_box').fadeIn("slow");             
                   $("#container").css({ // this is just for style                 
                   "opacity": "0.3"               
                   });
               } 
               
              // });
              </script>--%>



    <style type="text/css">
        /* popup_box DIV-Styles*/

        #popup_box {
            display: none; /* Hide the DIV */
            position: fixed;
            _position: absolute; /* hack for internet explorer 6 */
            height: 334px;
            width: 634px;
            background: #FFFFFF;
            left: 300px;
            top: 150px;
            z-index: 100; /* Layering ( on-top of others), if you have lots of layers: I just maximized, you can change it yourself */
            margin-left: 15px; /* additional features, can be omitted */
            border: 2px solid #ff0000;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px #ff0000;
            -webkit-box-shadow: 0 0 5px #ff0000;
            background-color: #ECF8FB;
        }

        #container {
            /*background: #d2d2d2; /*Sample*/
            width: 100%;
            height: 100%;
            cursor: pointer;
        }

        a {
            font-size: 12px;
            cursor: pointer;
            text-decoration: none;
        }
        /* This is for the positioning of the Close Link */

        #popupBoxClose {
            font-size: 20px;
            line-height: 15px;
            right: 5px;
            top: 5px;
            position: absolute;
            color: #6fa5e2;
            font-weight: 500;
        }
    </style>




    <style type="text/css">
        .style4 {
            background-color: #DDDDDD;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 15px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
            color: #F7285C;
        }

        .style6 {
            height: 21px;
        }

        .style7 {
            height: 10px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function checkTexts() {
            var PAN = getelementbyid('<%="ctl00_ContentPlaceHolder1_" + txt_1.ClientID %>');
            var Aadhaar = getelementbyid('<%="ctl00_ContentPlaceHolder1_" + txt_aahar.ClientID %>');
            alert(PAN);
            alert(Aadhaar);

            if (PAN != '' && Aadhaar != '') {
                return true;
                alert('hi');
            }
            else {
                return false;
                alert('hiffffff');
            }
            // do stuff
            // return true or false
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

            <div align="center">
                <table id="TableMain" runat="server" border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%;">

                            <table cellpadding="0" cellspacing="0" border="0" width="100%">


                                <tr>
                                    <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                            <tr align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" style="height: 10px;"></td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td align="right" valign="top" style="height: 10px;">Application Type</td>
                                                <td colspan="2" align="left" valign="top" style="height: 10px;">
                                                    <asp:DropDownList ID="ddlApplicationType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlApplicationType_SelectedIndexChanged">
                                                        <asp:ListItem Value="0" Text="--Select Application Type--"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Fresh"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Renewal / Renewal Cum Amendment Process"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Amendment"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>


                                            <tr align="left" valign="top" width="100%" id="trMSMEDBSEARCH" runat="server" visible="false">
                                                <td colspan="3" style="padding-top: 15px; border: 1px solid black; height: 25px" width="100%">

                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                        <tr id="divUAMPAN" runat="server" visible="false" style="padding-top: 25px">
                                                            <td align="left" valign="top" class="leftHeading">


                                                                <span class="MandatoryField"></span>
                                                                <asp:Label ID="lblUamNameDisplay" runat="server" Text="उद्योग आधार संख्या /Udyog Aadhar Number:" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblUamiReigistrationNameDisplay" runat="server" Text="उदयम पंजीकरण संख्या/Udyam Registration Number:" Visible="false"></asp:Label>



                                                                <div>
                                                                    <div style="float: left;">
                                                                        <asp:TextBox ID="txtUAM" runat="server" MaxLength="19" TabIndex="1"
                                                                            ValidationGroup="validate" Style="text-transform: uppercase;">
                                                                        </asp:TextBox>

                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Valid Aadher Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                            ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="validate">
                                                                        </asp:RegularExpressionValidator>



                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Valid Udyam Registration Number (e.g. UDYAM-UP-02-0000100)" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                            ValidationExpression="[A-Za-z]{5}(-)[A-Za-z]{2}(-)\d{2}(-)\d{7}" ValidationGroup="validate">
                                                                        </asp:RegularExpressionValidator>

                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter aadhar Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                            ValidationGroup="validate">
                                                                        </asp:RequiredFieldValidator>



                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Udyam Registration Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                                            ValidationGroup="validate">
                                                                        </asp:RequiredFieldValidator>


                                                                    </div>


                                                                    <div style="float: left;">



                                                                        <asp:Label ID="Label1" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                                            Visible="false"></asp:Label>
                                                                    </div>
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>
                                                                पैन कार्ड संख्या/
                                                                <br />
                                                                PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" Style="text-transform: uppercase;" />
                                                                <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate">
                                                                </asp:RegularExpressionValidator>
                                                            </td>



                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>उद्यम पंजीकृत मोबाइल नंबर /
                                                                <br />
                                                                Udyam Registered Mobile Number:
                                                                <br />
                                                                <asp:TextBox ID="txtUAMMobile" runat="server" MaxLength="10" ValidationGroup="validate" Style="text-transform: uppercase;" />
                                                                <asp:RequiredFieldValidator ControlToValidate="txtUAMMobile" ErrorMessage="Required" runat="server" ValidationGroup="validate" />

                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidatoUAMMobile" runat="server"
                                                                    ControlToValidate="txtUAMMobile" ErrorMessage="Please Enter Valid 10 Digit Mobile Number" ValidationGroup="validate"
                                                                    ValidationExpression="[0-9]{10}">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtenderUAMMobile" runat="server" FilterMode="ValidChars"
                                                                    FilterType="Custom" TargetControlID="txtUAMMobile" ValidChars="1234567890">
                                                                </asp:FilteredTextBoxExtender>

                                                            </td>


                                                        </tr>

                                                        <%--<tr id="divUAMPANMobileButton" runat="server" visible="false" style="padding-top: 25px">
                                                            <td align="left" valign="top" class="leftHeading" style="padding-top: 15px;">

                                                                <span class="MandatoryField">*</span>कॅप्चा /Captcha:
                                                                <br />
                                                                <asp:TextBox autocomplete="off" runat="server" ID="txtCaptcha" CssClass="form-control" placeholder="Captcha" AutoCompleteType="None" />
                                                                <br />
                                                                <asp:RequiredFieldValidator ControlToValidate="txtCaptcha" ErrorMessage="Required" runat="server" ValidationGroup="validate" />

                                                            </td>
                                                            <td>
                                                                <img src="../Captcha/CaptchaControl.aspx" alt="Verification code" id="imgCaptcha" runat="server" style="border: 0px; height: 40px; vertical-align: middle;" />
                                                                <a href="javascript:CaptchaRefresh('imgCaptcha','../Captcha/CaptchaControl.aspx')">
                                                                    <img src="../Images/captcha.jpg" alt="Verification code" title="Refresh code" style="border: 0px; height: 30px; width: 30px; vertical-align: middle;" />
                                                                </a>
                                                                <br />
                                                                <div id="divCaptcha" runat="server" visible="false">
                                                                    <asp:Label ID="lblCaptcha" runat="server" CssClass="msgRed" />
                                                                </div>
                                                            </td>

                                                        </tr>--%>



                                                        <tr id="divUAMPANMobileButton1" runat="server" visible="false" style="padding-top: 5px">


                                                            <td colspan="3" align="center" valign="top" class="leftHeading" style="padding-top: 5px;">
                                                                <asp:Button ID="btnf" runat="server" Text="Fetch" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />
                                                                <asp:HiddenField ID="hdnCFFSSUnitId" runat="server" />
                                                                <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                                    <ProgressTemplate>
                                                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                        Processing...            
                                                                    </ProgressTemplate>

                                                                </asp:UpdateProgress>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" align="center">
                                                                <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>


                                                </td>
                                            </tr>

                                            <tr align="left" valign="top">
                                                <td colspan="3" align="center" valign="top" style="height: 10px; font-size: 14px; font-weight: bold; color: Red;">
                                                    <asp:Literal ID="Literal1" runat="server" Text=""></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr id="tr1" runat="server" align="left" valign="top">
                                                <td colspan="3" align="left" valign="top" class="rowHeading">&nbsp;
                                                </td>
                                            </tr>


                                            <tr id="trbranch2" runat="server" align="left" valign="top">
                                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                                </td>
                                            </tr>
                                            <tr align="left" valign="top">
                                                <td colspan="3" align="center" valign="top" style="height: 10px; font-size: 14px; font-weight: bold; color: Red;">
                                                    <asp:Literal ID="ltlProvisionalNotice" runat="server" Text=""></asp:Literal>
                                                </td>
                                            </tr>



                                            <tr align="left" valign="top" runat="server" visible="true" id="trMainData">
                                                <td colspan="3" style="padding: 50px">
                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">

                                                        <tr id="trbranch1" visible="false" runat="server" align="left" valign="middle">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.0</td>

                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>शाखा/Branch:
                                                            </td>


                                                            <td align="left" valign="middle" style="height: 10px;">
                                                                <asp:DropDownList ID="DropDownListBranch" runat="server" CssClass="clsDDL"
                                                                    CausesValidation="false" ValidationGroup="1" AutoPostBack="false">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RFValidatorBranch" runat="server" ErrorMessage="Please select branch"
                                                                    ValidationGroup="1" ControlToValidate="DropDownListBranch" Display="None" SetFocusOnError="true">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="VCExtenderBranch" runat="server" TargetControlID="RFValidatorBranch">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.1</td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>फर्म की सरंचना/ Constitution of the MSE:
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:DropDownList ID="ddlConstitution" runat="server" CssClass="clsDDL"
                                                                    CausesValidation="false" ValidationGroup="1" AutoPostBack="false" OnSelectedIndexChanged="ddlConstitution_SelectedIndexChanged">
                                                                </asp:DropDownList>

                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11ss" runat="server" Display="Dynamic"
                                                                    ErrorMessage="Please Constitution of the MSE" ControlToValidate="ddlConstitution" InitialValue="0" SetFocusOnError="true"
                                                                    ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>



                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">


                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.2
                                                            </td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">

                                                                <div>
                                                                    <div style="float: left;">
                                                                        <asp:TextBox ID="txt_1" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="1" Enabled="false"
                                                                            ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;" OnTextChanged="txt_1_TextChanged"
                                                                            AutoCompleteType="Disabled">
                                                                        </asp:TextBox>

                                                                        <%--<asp:CustomValidator ID="regPAN" runat="server" ControlToValidate="txt_1" ErrorMessage="Please Enter Valid Pan Number" CssClass="valField"  ClientValidationFunction="checkTexts" ValidationGroup="reg" Display="Dynamic" ></asp:CustomValidator >--%>


                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Valid Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                            ValidationExpression="[A-Za-z]{5}\d{4}[A-Za-z]{1}" ValidationGroup="1">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="BottomRight"
                                                                            TargetControlID="RegularExpressionValidator2">
                                                                        </asp:ValidatorCalloutExtender>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Pan Number" ControlToValidate="txt_1" SetFocusOnError="true"
                                                                            ValidationGroup="1">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" PopupPosition="BottomRight"
                                                                            TargetControlID="RequiredFieldValidator1">
                                                                        </asp:ValidatorCalloutExtender>
                                                                        &nbsp;
                                                                    </div>
                                                                    <div style="float: left;">

                                                                        <asp:Label ID="lblPANmsg" runat="server" CssClass="errMessage" Text="PAN already exists !"
                                                                            Visible="false"></asp:Label>
                                                                        <%-- <br />--%>
                                                                        <asp:Label ID="lbl_branchName_chek" runat="server" CssClass="errMessage" Text=""
                                                                            Visible="true"></asp:Label>
                                                                    </div>
                                                                </div>

                                                            </td>
                                                        </tr>

                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.3
                                                            </td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField"></span>उद्योग आधार संख्या/Udyam Registration Number:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">

                                                                <div>
                                                                    <div style="float: left;">
                                                                        <asp:TextBox ID="txt_aahar" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="1"
                                                                            ValidationGroup="1" AutoPostBack="true" Style="text-transform: uppercase;"
                                                                            AutoCompleteType="Disabled" OnTextChanged="txt_aahar_TextChanged">
                                                                        </asp:TextBox>
                                                                        <%--<asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="txt_aahar" ErrorMessage="Please Enter Valid Pan Number" CssClass="valField"  ClientValidationFunction="checkTexts" ValidationGroup="reg" Display="Dynamic" ></asp:CustomValidator >--%>

                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidatorAAdhar" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter Valid aadher Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                                            ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="1">
                                                                        </asp:RegularExpressionValidator>
                                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" PopupPosition="BottomRight"
                                                                            TargetControlID="RegularExpressionValidatorAAdhar">
                                                                        </asp:ValidatorCalloutExtender>

                                                                        <asp:RequiredFieldValidator ID="Requ_addhar" runat="server" Display="Dynamic"
                                                                            ErrorMessage="Please Enter aadhar Number" ControlToValidate="txt_aahar" SetFocusOnError="true"
                                                                            ValidationGroup="1">
                                                                        </asp:RequiredFieldValidator>
                                                                        <%--  <asp:ValidatorCalloutExtender ID="Validator_aadhar_number" runat="server" PopupPosition="BottomRight"
                                                                            TargetControlID="Requ_addhar">
                                                                        </asp:ValidatorCalloutExtender>--%>
                                                                    </div>


                                                                    <div style="float: left;">



                                                                        <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                                            Visible="false"></asp:Label>
                                                                    </div>

                                                                    <%-- <div style="float: left;">
                                                            <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                                Visible="false"></asp:Label>

                                                        </div>--%>
                                                            </td>
                                                        </tr>





                                                        <!-- Isha: Ayush addition start-->
                                                        <!-- Isha: Ayush addition end-->
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">&nbsp;</td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">&nbsp;</td>
                                                            <td align="left" valign="top" style="width: 50%;">&nbsp;</td>
                                                        </tr>




                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.4
                                                            </td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>आवेदक फर्म का नाम/ Name of Applicant MSE:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:TextBox ID="txt_1p1" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                                                    ErrorMessage="Please Enter Name of Applicant MSE" ControlToValidate="txt_1p1"
                                                                    SetFocusOnError="true" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator2">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>

                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">1.5</td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>आवेदन की तारीख/ Date of Application:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:TextBox ID="txt_proposal_date" Enabled="false" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RFValidatortxt_22" runat="server" ErrorMessage="Date is required"
                                                                    Display="Dynamic" ControlToValidate="txt_proposal_date" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <%--<asp:CompareValidator ID="CValidatortxt_22" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                                    Display="Dynamic" ControlToValidate="txt_proposal_date" Operator="DataTypeCheck" Type="Date"
                                                                    ValidationGroup="1"></asp:CompareValidator>
                                                                <asp:CalendarExtender ID="CExtendertxt_22" SelectedDate="<%# DateTime.Now %>" runat="server" PopupButtonID="txt_proposal_date"
                                                                    TargetControlID="txt_proposal_date">
                                                                </asp:CalendarExtender>--%>
                                                                <asp:ValidatorCalloutExtender ID="VCExtendertxt22" runat="server" TargetControlID="RFValidatortxt_22"
                                                                    PopupPosition="TopLeft">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" class="leftHeading" style="width: 5%;" valign="top">1.6</td>
                                                            <td align="left" class="leftHeading" style="width: 45%;" valign="top">संस्थापन की तारीख/ Date of Incorporation:</td>
                                                            <td align="left" style="width: 50%;" valign="top">
                                                                <asp:TextBox ID="txtIncorporationDate" Enabled="true" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="1"  ValidationGroup="1">
                                                                    
                                                                    </asp:TextBox>


                                                                 <asp:RequiredFieldValidator ValidationGroup="1" ID="RFVDOI" runat="server" ErrorMessage="Please enter date" ControlToValidate="txtIncorporationDate"
     Display="Dynamic" SetFocusOnError="true">
 </asp:RequiredFieldValidator> 
 <%--<asp:ValidatorCalloutExtender ID="VCODOI" runat="server"
     TargetControlID="RFVDOI">
 </asp:ValidatorCalloutExtender>

 

 <asp:ImageButton OnClientClick="return false;" ID="IMGDOI" runat="server" ImageUrl="~/Images/calendar.png" />
 <asp:CalendarExtender ID="CEDOI" TargetControlID="txtIncorporationDate" Format="MM/dd/yyyy" PopupButtonID="IMGDOI" runat="server" />--%>




                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td colspan="3" align="left" valign="top" class="rowHeading">(1.a) मुख्य कार्यालय/ HEAD OFFICE
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td colspan="3" align="right" valign="top" class="style7">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.1)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>पता/ Address:
                                                            </td>
                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                <asp:TextBox ID="txt_1a1" runat="server" CssClass="txt_bx" TabIndex="2" ValidationGroup="1"
                                                                    TextMode="MultiLine">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                                    ErrorMessage="Please Enter HEAD OFFICE Address" ControlToValidate="txt_1a1" SetFocusOnError="true"
                                                                    ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator3">
                                                                </asp:ValidatorCalloutExtender>
                                                                <asp:CustomValidator ID="CustomValidatorHeadOfficeAddress" runat="server" ErrorMessage="Address should be less than 5000 characters"
                                                                    Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txt_1a1"
                                                                    ValidationGroup="1">
                                                                </asp:CustomValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="CustomValidatorHeadOfficeAddress">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.2)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>पिन कोड/ Pin Code:
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txt_1a2" runat="server" CssClass="txt_bx" MaxLength="6" TabIndex="3"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                                                    ErrorMessage="Please Enter HEAD OFFICE Pin Code" ControlToValidate="txt_1a2"
                                                                    SetFocusOnError="true" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>


                                                                <asp:RegularExpressionValidator
                                                                    ID="revPssIN"
                                                                    runat="server"
                                                                    ControlToValidate="txt_1a2" ValidationGroup="1"
                                                                    ErrorMessage="Enter a valid 6-digit PIN"
                                                                    ValidationExpression="^[1-9][0-9]{5}$">
                                                                </asp:RegularExpressionValidator>



                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator4">
                                                                </asp:ValidatorCalloutExtender>
                                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                                    TargetControlID="txt_1a2" />
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.3)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>राज्य/ State:
                                                            </td>
                                                            <td align="left" valign="top">

                                                                <div>
                                                                    <div style="float: left;">
                                                                        <asp:DropDownList ID="ddlState_a3" runat="server" CssClass="clsDDL" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged" TabIndex="4" ValidationGroup="1">
                                                                        </asp:DropDownList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                                            ErrorMessage="Please Select HEAD OFFICE State" ControlToValidate="ddlState_a3"
                                                                            SetFocusOnError="true" ValidationGroup="1">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" PopupPosition="BottomLeft"
                                                                            TargetControlID="RequiredFieldValidator5">
                                                                        </asp:ValidatorCalloutExtender>
                                                                    </div>
                                                                    <div style="float: left;">
                                                                    </div>
                                                                </div>

                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.4)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>ज़िला/शहर/District/ City:
                                                            </td>
                                                            <td align="left" valign="top" class="style6">
                                                                <asp:DropDownList ID="ddlCity_a4" runat="server" CssClass="clsDDL" TabIndex="5" ValidationGroup="1">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="None"
                                                                    ErrorMessage="Please Select HEAD OFFICE City" ControlToValidate="ddlCity_a4"
                                                                    SetFocusOnError="true" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator6">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.5)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>फोन/ Telephone:
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txt_1a5" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="6"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <br />
                                                                <span style="color: #FF0000; padding-top: 5px;">(eg: 011-1234567)</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="None"
                                                                    ErrorMessage="Please Enter HEAD OFFICE Telephone" ControlToValidate="txt_1a5"
                                                                    SetFocusOnError="true" ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator7">
                                                                </asp:ValidatorCalloutExtender>
                                                                <asp:FilteredTextBoxExtender ID="filtertexboxextender" runat="server" FilterMode="ValidChars"
                                                                    FilterType="Custom" TargetControlID="txt_1a5" ValidChars="1234567890,() .-+">
                                                                </asp:FilteredTextBoxExtender>

                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.6)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">मोबाइल नंबर/ Mobile No:
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txt_mob" runat="server" CssClass="txt_bx" MaxLength="10" TabIndex="6"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <asp:FilteredTextBoxExtender ID="fitmob" runat="server" FilterMode="ValidChars"
                                                                    FilterType="Custom" TargetControlID="txt_mob" ValidChars="1234567890">
                                                                </asp:FilteredTextBoxExtender>
                                                                <br />
                                                                <span style="color: #FF0000; padding-top: 5px;">(eg: 9871234567)</span>

                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">&nbsp;
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="3">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" class="leftHeading">(1.a.7)
                                                            </td>
                                                            <td align="left" valign="top" class="leftHeading">
                                                                <span class="MandatoryField">*</span>ई-मेल/ E-mail:
                                                            </td>
                                                            <td align="left" valign="top">
                                                                <asp:TextBox ID="txt_1a7" runat="server" CssClass="txt_bxEmail" MaxLength="500" TabIndex="8"
                                                                    ValidationGroup="1">
                                                                </asp:TextBox>
                                                                <br />
                                                                <span style="color: #FF0000;">(eg: free123@nsic.co.in)</span>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatoremailmain" runat="server" Display="None"
                                                                    ErrorMessage="Please Enter HEAD OFFICE E-mail" ControlToValidate="txt_1a7" SetFocusOnError="true"
                                                                    ValidationGroup="1">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtenderemailmain" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidatoremailmain">
                                                                </asp:ValidatorCalloutExtender>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionvalidoremail" runat="server" ControlToValidate="txt_1a7"
                                                                    Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender22" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RegularExpressionvalidoremail">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">&nbsp;
                                                            </td>
                                                        </tr>




                                                        <tr align="left" valign="top">
                                                            <td colspan="3" align="left" valign="top" class="rowHeading">(2.a) उद्यम विवरण/ENTERPRISE DETAILS
                                                            </td>
                                                        </tr>

                                                        <tr align="left" valign="top">
                                                            <td colspan="3" align="left" valign="top" class="rowHeading"></td>
                                                        </tr>

                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.a
                                                            </td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>सामाजिक श्रेणी की उद्यम/ Enterprise Social Category:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                                    TabIndex="52" ValidationGroup="validateFee">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="None"
                                                                    ErrorMessage="Please Select Enterprise Social Category" ControlToValidate="ddl_1e" SetFocusOnError="true"
                                                                    ValidationGroup="validateFee">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator16">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>

                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.b
                                                            </td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>उद्यमी की विशेष श्रेणी/ Any Special Category of Enterpreneur:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:DropDownList ID="ddl_1d" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                                    TabIndex="51" ValidationGroup="validateFee">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="None"
                                                                    ErrorMessage="Please Select Special Category of Enterpreneur" ControlToValidate="ddl_1d" SetFocusOnError="true"
                                                                    ValidationGroup="validateFee">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender28" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator15">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>
                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.
                                                        c</td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">
                                                                <span class="MandatoryField">*</span>श्रेणी यूनिट का आकार/Category Unit Size:
                                                            </td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:DropDownList ID="ddl_1f" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                                    TabIndex="53" ValidationGroup="validateFee">
                                                                </asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="None"
                                                                    ErrorMessage="Please Select Category Unit Size" ControlToValidate="ddl_1f" SetFocusOnError="true"
                                                                    ValidationGroup="validateFee">
                                                                </asp:RequiredFieldValidator>
                                                                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" PopupPosition="BottomLeft"
                                                                    TargetControlID="RequiredFieldValidator17">
                                                                </asp:ValidatorCalloutExtender>
                                                            </td>
                                                        </tr>






                                                        <tr align="left" valign="top">
                                                            <td align="left" valign="top" style="width: 5%;" class="leftHeading">2.d</td>
                                                            <td align="left" valign="top" style="width: 45%;" class="leftHeading">Enter the turnover as per latest<br />
                                                                audited balance sheet:</td>
                                                            <td align="left" valign="top" style="width: 50%;">
                                                                <asp:TextBox ID="txtTurnover" runat="server" CssClass="txt_bx" MaxLength="200"></asp:TextBox>
                                                                (figures in lakh) 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" SetFocusOnError="true" Display="Dynamic"
     ValidationGroup="validateFee"
     ErrorMessage="Please provide turnover as per latest audited balance sheet"
     ControlToValidate="txtTurnover">
 </asp:RequiredFieldValidator>
                                                               


                                                                <asp:RegularExpressionValidator
                                                                    ID="revAmount" ValidationGroup="validateFee" SetFocusOnError="true" Display="Dynamic"
                                                                    runat="server"
                                                                    ControlToValidate="txtTurnover"
                                                                    ValidationExpression="^\d+(\.\d{1,2})?$"
                                                                    ErrorMessage="Enter valid amount in lakhs (up to 2 decimals)"
                                                                    ForeColor="Red" />


                                                            </td>
                                                        </tr>
                                                        <tr runat="server" id="trMLMessage"><td colspan="3">  <asp:Label ID="lblMsgMultiplePayment" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label> 
                                                            <br />
                                                            <asp:Label ID="lblMsgMultiplePaymentAmount" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>

                                                                                            </td></tr>


                                                        <tr>
                                                            <td align="center" colspan="3" class="header">

                                                                <asp:Button ID="btnCalculateFeess" ValidationGroup="validateFee" runat="server" Text="Calcualte Fee" CssClass="btnCls" Width="200px"
                                                                    OnClick="btnCalculateFeess_Click" />

                                                                <asp:UpdateProgress ID="UpdateProgress11" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                                    <ProgressTemplate>
                                                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                        Processing...            
                                                                    </ProgressTemplate>

                                                                </asp:UpdateProgress>
                                                            </td>
                                                        </tr>



                                                        <tr>
                                                            <td align="center" colspan="3" class="header">

                                                                <asp:Table Visible="false" ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%">
                                                                </asp:Table>

                                                            </td>
                                                        </tr>

                                                        <asp:Panel ID="pnlMain" runat="server" Width="100%" Visible="false">



                                                            <tr>
                                                                <td align="left" colspan="3" class="header">NSIC Payment Details:<asp:Label ID="lbl_payment_details_nsic" runat="server" Text=""></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left" valign="top" style="padding-top: 10px;">

                                                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">
                                                                    <asp:Label ID="lbl_registration_fees_text" runat="server" Text="NSIC"></asp:Label>
                                                                    Fees:
                                                                </td>
                                                                <td align="left" valign="top">
                                                                    <asp:Label ID="lblUnitAmnt" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left" valign="top" style="padding-top: 10px;">

                                                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Service Tax:
                                                                </td>
                                                                <td align="left" valign="top">
                                                                    <asp:Label ID="lblServiceTax" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left" valign="top" style="padding-top: 10px;">

                                                                <td align="left" valign="top" class="leftHeading" style="width: 200px;" colspan="2">Total amount payable:
                                                                </td>
                                                                <td align="left" valign="top">
                                                                    <asp:Label ID="lblTotAmnt" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr align="left" valign="top">
                                                                <td colspan="3" align="left" valign="top" class="style1">Select Payment Option
                                                                </td>
                                                            </tr>
                                                            <tr align="right" valign="top">
                                                                <td colspan="3" align="center" class="style2">
                                                                    <asp:RadioButtonList ID="radPay" runat="server" RepeatDirection="Horizontal"
                                                                        Width="70%" OnSelectedIndexChanged="radPay_SelectedIndexChanged"
                                                                        AutoPostBack="True">
                                                                        <%-- <asp:ListItem Text="Cash" Value="1"></asp:ListItem>--%>
                                                                        <asp:ListItem Text="DD" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Cheque" Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text="Online" Selected="True" Value="4"></asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </td>
                                                            </tr>




                                                            <tr align="left" valign="top">
                                                                <td colspan="3" align="right" valign="top" style="height: 10px;"></td>
                                                            </tr>

                                                            <tr align="left" valign="top">
                                                                <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                                                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Panel ID="pnlChqDD" runat="server" Visible="true">
                                                                                    <table width="80%">
                                                                                        <tr>
                                                                                            <td class="rowStep" colspan="2">
                                                                                                <b>CHEQUE / DD / ONLINE PAYMENT DETAILS </b>
                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Cheque / DD No. / UTR or IMPS No.</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txt_chqddNo" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50">
                                                                                                </asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                                                                    ErrorMessage="Please Enter Cheque or DD Number or UTR or IMPS No."
                                                                                                    ControlToValidate="txt_chqddNo"
                                                                                                    SetFocusOnError="true"
                                                                                                    ValidationGroup="1">
                                                                                                </asp:RequiredFieldValidator>
                                                                                               <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server"
                                                                                                    PopupPosition="BottomRight"
                                                                                                    TargetControlID="RequiredFieldValidator9">
                                                                                                </asp:ValidatorCalloutExtender>--%>


                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Bank Name</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txt_BankName" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50">
                                                                                                </asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                                                    ErrorMessage="Please Enter Bank Name"
                                                                                                    ControlToValidate="txt_BankName"
                                                                                                    SetFocusOnError="true"
                                                                                                    ValidationGroup="1">
                                                                                                </asp:RequiredFieldValidator>
                                                                                                <%--<asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server"
                                                                                                    PopupPosition="BottomRight"
                                                                                                    TargetControlID="RequiredFieldValidator2">
                                                                                                </asp:ValidatorCalloutExtender>--%>

                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Bank Branch</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txt_BankBranch" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50">
                                                                                                </asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                                                    ErrorMessage="Please Enter Bank Branch"
                                                                                                    ControlToValidate="txt_BankBranch"
                                                                                                    SetFocusOnError="true"
                                                                                                    ValidationGroup="1">
                                                                                                </asp:RequiredFieldValidator>
                                                                                               <%-- <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server"
                                                                                                    PopupPosition="BottomRight"
                                                                                                    TargetControlID="RequiredFieldValidator4">
                                                                                                </asp:ValidatorCalloutExtender>--%>

                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Payable At</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txt_PayableAt" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50">
                                                                                                </asp:TextBox>



                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Cheque / DD / UTR or IMPS Amount <span class="MandatoryField">(With Service Tax)</span></b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox ID="txt_chqddAmnt" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="50" > <%--ReadOnly="true"--%>
                                                                                                </asp:TextBox>
                                                                                                <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator19" runat="server" ErrorMessage="Please enter Amount" ControlToValidate="txt_chqddAmnt" Display="Dynamic" SetFocusOnError="true"></asp:RequiredFieldValidator>

                                                                                                <asp:RegularExpressionValidator ValidationGroup="1" ID="RFVStxtAmount" runat="server" ControlToValidate="txt_chqddAmnt"  ValidationExpression="^\d+$" ErrorMessage="Please enter a valid whole amount." ForeColor="Red" Display="Dynamic"> </asp:RegularExpressionValidator>


                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Cheque / DD / UTR or IMPS Date</b>
                                                                                            </td>
                                                                                            <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                <asp:TextBox onkeydown="return false;" onpaste="return false;" ID="txt_chqddate" runat="server" CssClass="txt_bx"
                                                                                                    ValidationGroup="1" MaxLength="15">
                                                                                                </asp:TextBox> 
                                                                                                <asp:RequiredFieldValidator ID="RFValidatortxt_chqddate" runat="server" ErrorMessage="Please enter date" ControlToValidate="txt_chqddate"
                                                                                                    Display="None" SetFocusOnError="true">
                                                                                                </asp:RequiredFieldValidator>
                                                                                               <%-- <asp:ValidatorCalloutExtender ID="VCExtendertxt_chqddate" runat="server"
                                                                                                    TargetControlID="RFValidatortxt_chqddate">
                                                                                                </asp:ValidatorCalloutExtender> --%>
                                                                                                <asp:ImageButton OnClientClick="return false;" ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                                                                                <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_chqddate" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />


                                                                                            </td>
                                                                                        </tr>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Upload Scanned Copy of Cheque / DD </b>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                                                <asp:Label ID="Label2" runat="server" CssClass="ErrMsgLbl"></asp:Label>

                                                                                            </td>
                                                                                        </tr>


                                                                                        <tr>
                                                                                            <td>
                                                                                                <b>Payment Remarks </b>
                                                                                            </td>
                                                                                            <td align="left">
                                                                                                <asp:TextBox ID="txtChequeDDPaymentRemarks" runat="server" CssClass="txt_bx" TextMode="MultiLine"
                                                                                                    ValidationGroup="1" MaxLength="500" ReadOnly="false">
                                                                                                </asp:TextBox>

                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" SetFocusOnError="true"
                                                                                                    ValidationGroup="1" runat="server" ErrorMessage="Please enter remarks" ControlToValidate="txtChequeDDPaymentRemarks"
                                                                                                    Display="Dynamic">
                                                                                                </asp:RequiredFieldValidator>




                                                                                            </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                            <td><b>Bank Details </b></td>
                                                                                            <td>
                                                                                                <%-- CssClass="clsDDL" --%>
                                                                                                <asp:DropDownList ID="ddlBank" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlBank_SelectedIndexChanged"></asp:DropDownList>
                                                                                                <asp:HiddenField ID="hdnBranchID" runat="server" />
                                                                                                <asp:HiddenField ID="hdnAccountNumber" runat="server" />
                                                                                                <asp:HiddenField ID="hdnBankName" runat="server" />
                                                                                                <asp:HiddenField ID="hdnIFSCCode" runat="server" />
                                                                                                <asp:HiddenField ID="hdnBranchName" runat="server" />
                                                                                                <asp:HiddenField ID="hdnLedgerID" runat="server" />
                                                                                                <asp:HiddenField ID="hdnLedgerName" runat="server" />
                                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" SetFocusOnError="true"
                                                                                                    ValidationGroup="1" runat="server" ErrorMessage="Please Select Bank" ControlToValidate="ddlBank" InitialValue="0"
                                                                                                    Display="Dynamic">
                                                                                                </asp:RequiredFieldValidator>

                                                                                                <%--<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="BranchID,BankAccountNo,Bank_Name,Bank_IFSC_Code,BranchName,LedgerID,Ledger_Name">
                                                                                                    <columns>
                                                                                                        <asp:TemplateField HeaderText="Select">
                                                                                                            <itemtemplate>
                                                                                                                <asp:RadioButton
                                                                                                                    ID="rbSelect"
                                                                                                                    runat="server"
                                                                                                                    onclick="SelectSingleRadio(this)" />
                                                                                                            </itemtemplate>
                                                                                                        </asp:TemplateField>

                                                                                                        

                                                                                                        <asp:BoundField DataField="BranchID" HeaderText="Branch ID" />
                                                                                                        <asp:BoundField DataField="BankAccountNo" HeaderText="Account Number" />
                                                                                                        <asp:BoundField DataField="Bank_Name" HeaderText="Bank Name" />
                                                                                                        <asp:BoundField DataField="Bank_IFSC_Code" HeaderText="IFSC Code" />
                                                                                                        <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                                                                                        <asp:BoundField DataField="LedgerID" HeaderText="Ledger ID" />
                                                                                                        <asp:BoundField DataField="Ledger_Name" HeaderText="Ledger Name" />
                                                                                                    </columns>>
                                                                                                </asp:GridView>
                                                                                                <br />
                                                                                                <br />

                                                                                                <asp:CustomValidator 
                                                                                                    ID="cvGridRadio"
                                                                                                    runat="server"
                                                                                                    ErrorMessage="Please select one bank record"
                                                                                                    ClientValidationFunction="ValidateGridRadio"
                                                                                                    ValidationGroup="1"
                                                                                                    Display="Dynamic">
                                                                                                </asp:CustomValidator>

                                                                                                <br />--%>

                                                                                            </td>
                                                                                        </tr


                                                                                        <tr>
                                                                                            <td align="right">
                                                                                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="btnCls"
                                                                                                    ValidationGroup="1" OnClick="Button1_Click" />
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </asp:Panel>
                                                                            </td>
                                                                        </tr>





                                                                    </table>
                                                                </td>
                                                            </tr>





                                                        </asp:Panel>



                                                    </table>
                                                </td>
                                            </tr>

                                        </table>
                                    </td>

                                </tr>












                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: left;" class="MandatoryMessage">
                            <span class="MandatoryField">*</span> Mandatory Fields
                        </td>
                    </tr>

                </table>
                <asp:HiddenField ID="hdnCompany" runat="server" />

                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                </table>
            </div>





        </ContentTemplate>

        <Triggers>
            <asp:PostBackTrigger ControlID="Button1" />
            <asp:PostBackTrigger ControlID="imgbtnCalendar" />
        </Triggers>


    </asp:UpdatePanel>




</asp:Content>

