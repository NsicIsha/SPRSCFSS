<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="NewUnitRegistrationStep6Online.aspx.cs" Inherits="appAdministrator_NewUnitRegistrationStep6Online" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .clstblCell {
            vertical-align: top;
            text-align: justify;
            padding-right: 5px;
        }

        .clstblCellHead {
            vertical-align: top;
            font-weight: bold;
            text-transform: uppercase;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function FunResetTurnover() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtTurnover").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtTurnover").focus();

        }
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);



        function Btn_chekModified() {
            var id = 1;
            var i = 0;
            var count = 0
            var j;
            var unitId = $("#ctl00_ContentPlaceHolder1_hdn_UnitID").val();
            var CountRadio = $("#ctl00_ContentPlaceHolder1_hdn_unitID_Count").val();
            var nradio = 0;
            ok = false;
            if ($("input[@name=rdReg]:checked").is(':checked') == true) {

                isSelected = true;
                var Mystring = new Array();
                Mystring = unitId.split(',');
                // alert(Mystring);
                var newid = new Array();
                newid = Mystring;
                for (var j = 0; j < newid.length - 1; j++) {

                    if ($("input:radio[name=rd_" + newid[j] + "]").is(':checked') == true) {

                    }
                    else {
                        //alert(newid[j]);
                        alert("Please select registration both payment slab.");
                        return false;
                    }
                }
            }
            else {
                alert("Please select registration both payment slab.");
                return false;
            }
        }
        $(document).ready(function () {
            $("#progressbar").progressbar({ value: 90 });
        });

        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">New Unit Registration - Step 6
            </td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                                <tr>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-1</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-2</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-3</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-4</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-5</b></td>
                                    <td style="width: 15%; padding-left: 10px; padding-right: 10px;"><b>Step-6</b></td>
                                    <td style="width: 10%; padding-left: 10px; padding-right: 10px;">Step-7</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="width: 100%;">
                        <td id="step2" style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td id="progressbar" align="left"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="4" align="right" valign="top" class="rowStep">STEP 6 of 7
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="4" class="headerAll" style="width: 170px;">NSIC & Inspection Agency Fee Details
                        </td>
                    </tr>
                    <%--<tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" style="height:10px; padding-top:20px" class="leftHeading">
                           <asp:Label ID="lblMsgPayment" ForeColor="Red" Font-Bold="true" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" style="height: 10px; padding-top: 20px" class="leftHeading">Use Combination of UDYAM Registration Number & PAN to Retrive Fee Details
                        </td>
                    </tr>
                    <tr align="left" valign="top" width="100%" id="trMSMEDBSEARCH" runat="server">
                        <td colspan="4" style="padding: 10px">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <table border="1" cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td align="left" valign="top" class="leftHeading"><span class="MandatoryField">*</span>आपके पास उदयम पंजीकरण संख्या / <%--Do--%> You have UDYAM Registration Number</td>
                                            <td align="left" valign="top" class="leftHeading" colspan="2">
                                                <asp:RadioButtonList ID="rdbMsmeRegtype" Width="250px" AutoPostBack="true" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbMsmeRegtype_SelectedIndexChanged">
                                               <%--     <asp:ListItem Value="1" Text="UAM"></asp:ListItem>--%>
                                                    <asp:ListItem Value="2" Text="Udyam Registration"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                                                    ErrorMessage="Please Select Unit have UAM or Udyami Registration Number" ControlToValidate="rdbMsmeRegtype" SetFocusOnError="true"
                                                    ValidationGroup="1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr id="divUAMPAN" runat="server" visible="false" style="padding-top: 25px">
                                            <td align="left" valign="top" class="leftHeading">
                                                <span class="MandatoryField"></span>
                                                <asp:Label ID="lblUamNameDisplay" runat="server" Text="उद्योग आधार संख्या /Udyog Aadhar Number:" Visible="false"></asp:Label>
                                                <asp:Label ID="lblUamiReigistrationNameDisplay" runat="server" Text="उदयम पंजीकरण संख्या/Udyam Registration Number:" Visible="false"></asp:Label>
                                                <div>
                                                    <div style="float: left;">
                                                        <asp:TextBox ID="txtUAM" runat="server" MaxLength="19" TabIndex="1"
                                                            ValidationGroup="validate" AutoPostBack="true" Style="text-transform: uppercase;"></asp:TextBox>

                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Valid Aadher Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                            ValidationExpression="[A-Za-z]{2}\d{2}[A-Za-z]{1}\d{7}" ValidationGroup="validate"></asp:RegularExpressionValidator>



                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Valid Udyam Registration Number (e.g. UDYAM-UP-02-0000100)" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                            ValidationExpression="[A-Za-z]{5}(-)[A-Za-z]{2}(-)\d{2}(-)\d{7}" ValidationGroup="validate"></asp:RegularExpressionValidator>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter aadhar Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                            ValidationGroup="validate"></asp:RequiredFieldValidator>



                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                            ErrorMessage="Please Enter Udyam Registration Number" ControlToValidate="txtUAM" SetFocusOnError="true"
                                                            ValidationGroup="validate"></asp:RequiredFieldValidator>


                                                    </div>


                                                    <div style="float: left;">
                                                        <%--<asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="update_adhar">
                                                                <ProgressTemplate>
                                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>--%>


                                                        <asp:Label ID="Label1" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                            Visible="false"></asp:Label>
                                                    </div>

                                                    <%-- <div style="float: left;">
                                                            <asp:Label ID="lbl_aadharnumber" runat="server" CssClass="errMessage" Text="Aahar already exists !"
                                                                Visible="false"></asp:Label>

                                                        </div>--%>
                                                    <%-- </ContentTemplate>

                                            </asp:UpdatePanel>--%>

                                                    <%-- <asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="validate" />--%>
                                            </td>
                                            <td align="left" valign="top" class="leftHeading">
                                                <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" Style="text-transform: uppercase;" />
                                                <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                                                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                            </td>
                                            <td align="left" valign="top" class="leftHeading" style="padding-top: 15px;">
                                                <asp:Button ID="btnf" runat="server" Text="Fetch" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />

                                                <%-- <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                            <ProgressTemplate>
                                                                <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                Processing...            
                                                            </ProgressTemplate>

                                                        </asp:UpdateProgress>--%>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="center" class="leftHeading">
                                                <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">
                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                        <span style="color: red; font-size: medium">Please wait while we are processing your request <%--Please wait, while your request is being processed--%></span>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                            </td>
                                        </tr>
                                    </table>

                                    <%--<table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                   <td align="left" valign="top" class="leftHeading">
                                                        <span class="MandatoryField">*</span>उद्योग आधार संख्या/Udyog Aadhar Number: 
                                                        <asp:TextBox ID="txtUAM" runat="server" MaxLength="50" ValidationGroup="validate" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="validate" />

                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading">
                                                         <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" style="text-transform:uppercase;" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan" 
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading">
                                                        <asp:Button ID="btnf" runat="server" Text="Fetch" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />

                                                        <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                            <ProgressTemplate>
                                                                <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                Processing...            
                                                            </ProgressTemplate>
                                                        </asp:UpdateProgress>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="padding-top:75px">
                                                        <asp:Label ID="lblMsg" ForeColor="Red" Font-Bold="true" Font-Size="Large" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>--%>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                    <tr align="left" valign="top" runat="server" id="trPaymentSlab" visible="false">
                        <td id="tdReg" colspan="4" align="left" valign="top" class="rowHeadingwithTable">SELECT REGISTRATION PAYMENT SLAB FOR COMPANY
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top">
                            <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
                            <asp:HiddenField ID="HdnFieldTurnover" runat="server" />
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="right" valign="top" style="height: 10px;">&nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top" runat="server" id="trInspectionAgency" visible="false">
                        <td colspan="4" align="left" valign="top" class="rowHeadingwithTable">SELECT INSPECTION AGENCY
                            <asp:HiddenField ID="hdn_UnitID" runat="server" />
                            <asp:HiddenField ID="hdn_unitID_Count" runat="server" />
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top">
                            <script type="text/javascript" src="../JScript/wz_tooltip/wz_tooltip.js"></script>
                            <asp:Table ID="Table1" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" class="leftHeading"></td>
                    </tr>
                    <tr align="right" valign="top" id="trButton" runat="server" visible="false">
                        <td colspan="3">
                            <%--<input type="button" id="btnback" style="display:block"  visible="false" class="btnCls" value="Back"   onclick ="Javascript:window.history.back(); return false;"/>--%>
                        </td>
                        <%-- <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" 
                                CssClass="btnCls" TabIndex="37"
                                ValidationGroup="1" Width="150px" onclick="btnSubmit_Click" OnClientClick="javascript:return Btn_chekModified();" />
                        </td>--%>
                        <td>
                            <asp:UpdatePanel ID="updSubmt" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div style="float: right;">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;"
                                                CssClass="btnCls" TabIndex="37"
                                                ValidationGroup="1" Width="150px" OnClick="btnSubmit_Click" OnClientClick="javascript:return Btn_chekModified();" />
                                         </div>
                                        <div style="float: right;">
                                            <asp:UpdateProgress ID="UpdateProgress6" runat="server" AssociatedUpdatePanelID="updSubmt">
                                                <ProgressTemplate>
                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <%--<asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;" CssClass="btnCls"
                                ValidationGroup="1" Width="150px" OnClick="btnSubmit_Click" TabIndex="54" />--%>
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
    <asp:Panel ID="pnlOuterEdit" runat="server" Style="display: block;" CssClass="outerPopup">
        <asp:Panel ID="pnlInnerEdit" runat="server" CssClass="innerPopup" Width="325px">
            <table cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                <tr align="center" valign="top" style="width: 100%;">
                    <td align="center" valign="top" style="width: 100%; padding-left: 5px; padding-right: 5px; padding-top: 0px; padding-bottom: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                            <tr align="center" valign="top">
                                <td align="center" valign="top" class="modalTDtextBOLD">Enter the turnover as per latest<br />
                                    audited balance sheet:
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td align="center" class="modalTDtextbox">
                                    <asp:TextBox ID="txtTurnover" runat="server" CssClass="txt_bx" MaxLength="200"></asp:TextBox>
                                    (figures in lakh) 
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" Display="Dynamic"
                                        ValidationGroup="12"
                                        ErrorMessage="Please provide last registration fee paid"
                                        ControlToValidate="txtTurnover"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td align="center" class="modalTDbutton">
                                    <asp:Label runat="server" ID="lblpopmsg" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td align="center" class="modalTDbutton">
                                    <asp:Button ID="btnSubmitEdit" ValidationGroup="12" runat="server" Text="Submit" CssClass="btnCls"
                                        OnClick="btnSubmitEdit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="btnResetEdit" type="button" value="Reset" class="btnCls" onclick="FunResetTurnover()" />
                                    <asp:Button ID="Button1" runat="server" Text="Close" CssClass="btnCls"
                                        OnClick="btnClose_Click" />
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td colspan="2">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" DropShadow="false"
        TargetControlID="lnkModalClick"
        PopupControlID="pnlOuterEdit">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" Radius="10" Corners="All" BorderColor="#000000"
        TargetControlID="pnlInnerEdit">
    </asp:RoundedCornersExtender>
    <asp:LinkButton ID="lnkModalClick" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
</asp:Content>

