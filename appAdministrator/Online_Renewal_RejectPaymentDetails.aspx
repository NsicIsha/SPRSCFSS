<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Online_Renewal_RejectPaymentDetails.aspx.cs" Inherits="appAdministrator_Online_Renewal_RejectPaymentDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left" style="padding-left:10px; padding-right:10px;">

    <div align="left" class="headerAll">
        Reason of Rejection :
    </div>
    <div align="left" style="height:20px;">
        
    </div>
    <div align="left">
        <table border="0" cellpadding="0" cellspacing="1" width="100%">
            <tr align="left" valign="top" style="width:100%;">
                <td align="left" valign="top" class="leftHeading">
                    Reason of Rejection :
                </td>
                <td align="left" valign="top">
            <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" MaxLength="5000" 
                CssClass="txt_bx" ValidationGroup="1" Height="122px" Width="396px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFValidatorReason" runat="server" ValidationGroup="1"
            ErrorMessage="Please provide reason to reject" ControlToValidate="txtReason" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" PopupPosition="TopRight" TargetControlID="RFValidatorReason">
            </cc1:ValidatorCalloutExtender>
            </td>
            </tr>
        </table>
    </div>
    <div align="left" style="height:20px;">
        
    </div>
    <div align="right" style="padding-right:110px;">
        <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btnCls" 
            ValidationGroup="1" onclick="btnSubmit_Click" />&nbsp;&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btnCls" 
            CausesValidation="false" onclick="btnBack_Click" />
    </div>

</div>
</asp:Content>


