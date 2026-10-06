<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" 
CodeFile="Branch_Change_Address.aspx.cs" Title="GP::Change Address"
Inherits="appAdministrator_Branch_Change_Address" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll" style="width: 170px;">
               Change Branch Address
            </td>
        </tr>
        
        <tr>
            <td align="center" style="padding-top: 10px;">
                <asp:Label ID="lblMessage" runat="server" CssClass="MasterAction"></asp:Label>
            </td>
        </tr>      
        <tr>
            <td colspan="2" align="left" height="400" valign="top" style="padding-left: 50px;">
                <table width="400" border="0" align="left" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" align="center" class="header">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Address:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="5"
                                ValidationGroup="1" Height="50px" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="Address is required !" ControlToValidate="txtAddress" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                                Width="250px">
                            </cc1:ValidatorCalloutExtender>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Email Id:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <div style="float: left; height: 35px; padding-top: 15px;">
                                <asp:TextBox ID="txtEmailId" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="9"
                                    ValidationGroup="1"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                ErrorMessage="Email Id is required !" ControlToValidate="txtEmailId" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmailId"
                                Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                            </asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidator3"
                                Width="250px">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator1">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Phone No:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="10"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhoneNo"
                                Display="None" ErrorMessage="Please Enter phone no. !" SetFocusOnError="True"
                                 ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RegularExpressionValidator2">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Fax No:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtFax" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="12"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFax"
                                Display="None" ErrorMessage="Please Enter Fax no. !" SetFocusOnError="True"
                                 ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator4">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                        <asp:HiddenField runat ="server" ID ="hfbranchid" />
                        </td>
                        <td height="10" style="padding-top: 10px; padding-left: 20px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btnCls" TabIndex="13"
                                ValidationGroup="1" onclick="btnSubmit_Click"/>
                            &nbsp;&nbsp;<asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnCls"
                                TabIndex="14" CausesValidation="false" onclick="btnBack_Click"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width: 100%; text-align: right;" class="MandatoryMessage">
                * Mandatory Fields
            </td>
        </tr>
        <tr>
            <td colspan="2">
             
            </td>
        </tr>
    </table>
</asp:Content>

