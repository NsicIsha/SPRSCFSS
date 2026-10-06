<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="UnitRenewalStep6.aspx.cs" Inherits="appAdministrator_UnitRenewalStep6" Title="UnitRenewalStep6" %>

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
        // <!CDATA[

        function FunResetRegfee() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtRegfee").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtRegfee").focus();
        }

        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
        function SetRadioButton() {
            if ($("input[@name=rdReg]:checked").is(':checked') == true) {
                return true;
            }
            else {
                alert("Please Select Renewal Amount!!!");
                return false;
            }
        }


        // ]]>
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">New Unit Renewal - Step 6
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
                    <tr align="left" valign="top">
                        <td colspan="4" align="right" valign="top" style="height: 10px;">&nbsp;
                            <asp:HiddenField ID="HDNFormtype" runat="server" />
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td id="tdReg" colspan="4" align="left" valign="top" class="rowHeadingwithTable">SELECT  RENEWAL PAYMENT SLAB FOR COMPANY
                        </td>
                    </tr>

                      <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" class="leftHeading">
                            <asp:Label ID="lblMsg_Covid19" runat="server" ForeColor="Green" Font-Bold="true" ></asp:Label>
                        </td>
                    </tr>


                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top">
                            <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
                            <asp:HiddenField ID="HdnFieldAmount" runat="server" />
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td colspan="4" align="left" valign="top" class="leftHeading"></td>
                    </tr>
                    <tr align="right" valign="top">
                        <td colspan="3">
                            <input type="button" id="btnback" class="btnCls" value="Back" onclick="return btnback_onclick()" />
                        </td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Save &amp; Continue &gt;&gt;"
                                CssClass="btnCls" TabIndex="37"
                                ValidationGroup="1" Width="150px"
                                OnClientClick="javascript:SetRadioButton();"
                                OnClick="btnSubmit_Click" />
                            <%--OnClientClick="javascript:return Btn_chekModified();"--%>                                  
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
                                <td align="center" valign="top" class="modalTDtextBOLD">ENTER TURNOVER OF LAST FINANCIAL YEAR(in lakh) :
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td align="center" class="modalTDtextbox">
                                    <asp:TextBox ID="txtRegfee" runat="server" CssClass="txt_bx" MaxLength="200" ValidationGroup="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true" Display="None"
                                        ValidationGroup="12"
                                        ErrorMessage="Please provide last registration fee paid"
                                        ControlToValidate="txtRegfee"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server"
                                        TargetControlID="RequiredFieldValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="center" valign="top">
                                <td align="center" class="modalTDbutton">
                                    <asp:Button ID="btnSubmitEdit" runat="server" Text="Submit" CssClass="btnCls"
                                        ValidationGroup="12" OnClick="btnSubmitEdit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="btnResetEdit" type="button" value="Reset" class="btnCls" onclick="FunResetRegfee()" />
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
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" DropShadow="false"
        TargetControlID="lnkModalClick"
        PopupControlID="pnlOuterEdit">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" Radius="10" Corners="All" BorderColor="#000000"
        TargetControlID="pnlInnerEdit">
    </asp:RoundedCornersExtender>
    <asp:LinkButton ID="lnkModalClick" runat="server" Style="display: none;">LinkButton</asp:LinkButton>
</asp:Content>

