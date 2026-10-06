<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs"
Inherits="appAdministrator_UpdateProfile" Title="GP::Update Profile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll" style="width: 170px;">
                Master Update Profile
            </td>
        </tr>
       <%-- dsasad--%>
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
                        <td align="right" style="padding-right: 10px; padding-top: 10px; width: 150px;" class="leftHeading">
                            *Role:
                        </td>
                        <td align="left" style="padding-top: 10px; width: 250px;">
                            <asp:Label ID="lblRole" runat="server" CssClass="bodytextbolditalic"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading" valign="top">
                            *Zone:
                        </td>
                        <td align="left" style="padding-top: 10px;" valign="top">
                            <asp:Label ID="lblZone" runat="server" CssClass="bodytextbolditalic"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Branch:
                        </td>
                        <td align="left" style="padding-top: 10px;" valign="top">
                            <asp:Label ID="lblBranch" runat="server" CssClass="bodytextbolditalic"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *User Name:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:Label ID="lblUserName" runat="server" CssClass="bodytextbolditalic"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *First Name:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="5"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                ErrorMessage="First name is required !" ControlToValidate="txtFirstName" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1"
                                Width="250px">
                            </cc1:ValidatorCalloutExtender>
                            
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            Middle Name:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtMiddleName" runat="server" CssClass="txt_bx" MaxLength="500"
                                TabIndex="6" ValidationGroup="1"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Last Name:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txt_bx" MaxLength="500" TabIndex="7"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None"
                                ErrorMessage="Last name is required !" ControlToValidate="txtLastName" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RequiredFieldValidator2"
                                Width="250px">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Date of Birth:
                        </td>
                        <td>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="left" style="padding-top: 10px;">
                                        <asp:TextBox ID="txtDOB" runat="server" CssClass="txt_bx" MaxLength="500" ValidationGroup="1"
                                            TabIndex="8"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="None"
                                            ErrorMessage="Date of Birth is required" ControlToValidate="txtDOB" SetFocusOnError="true"
                                            ValidationGroup="1">
                                        </asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Please provide valid date" ControlToValidate="txtDOB"
                                        Display="None" Type="Date" Operator="DataTypeCheck" ValidationGroup="1">
                                        </asp:CompareValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="txtDOB"
                                            TargetControlID="txtDOB">
                                        </cc1:CalendarExtender>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator4"
                                            Width="250px">
                                        </cc1:ValidatorCalloutExtender>
                                        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="CompareValidator1">
                                        </cc1:ValidatorCalloutExtender>
                                    </td>
                                    <td align="left" style="padding-left: 4px;">
                                        <asp:Literal ID="ltlDOB" runat="server" Text="MM/DD/YYYY"></asp:Literal>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>--%>
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
                            Phone No.:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="10"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhoneNo"
                                Display="None" ErrorMessage="Please provide valid phone no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" TargetControlID="RegularExpressionValidator2">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            *Mobile No.:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="11"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="None"
                                ErrorMessage="Mobile no. is required !" ControlToValidate="txtMobileNo" SetFocusOnError="true"
                                ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobileNo"
                                Display="None" ErrorMessage="Please provide valid mobile no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" TargetControlID="RegularExpressionValidator3">
                            </cc1:ValidatorCalloutExtender>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" TargetControlID="RequiredFieldValidator9"
                                Width="250px">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" class="leftHeading">
                            Fax No.:
                        </td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:TextBox ID="txtFax" runat="server" CssClass="txt_bx" MaxLength="12" TabIndex="12"
                                ValidationGroup="1"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtFax"
                                Display="None" ErrorMessage="Please provide valid fax no. !" SetFocusOnError="True"
                                ValidationExpression="\d+" ValidationGroup="1"></asp:RegularExpressionValidator>
                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" TargetControlID="RegularExpressionValidator4">
                            </cc1:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" style="padding-right: 10px; padding-top: 10px;" 
                            class="leftHeading" valign="middle">
                            Designation</td>
                        <td align="left" style="padding-top: 10px;">
                            <asp:DropDownList ID="ddldept" runat="server" CssClass="clsDDL" 
                                DataTextField="DNAME" DataValueField="DEPTID" Width="350px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
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
                &nbsp;
            </td>
        </tr>
    </table>

</asp:Content>

