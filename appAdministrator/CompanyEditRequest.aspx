<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyEditRequest.aspx.cs" Inherits="appAdministrator_CompanyEditRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> <%--test--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr valign="top" align="left" style="width: 100%;">
            <td valign="top" align="left" style="width: 100%;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%">
                        <td valign="top" align="right" colspan="2" style="padding-bottom: 10px; padding-right: 5px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top">
                                    <td align="center" valign="top" class="updateInnerHeader">
                                        <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                    </td>
                                    <td align="right" valign="top" style="width: 20px;">
                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                    </td>
                                </tr>
                            </table>

                        </td>
                    </tr>

                    <tr valign="top" align="left">
                        <td valign="top" align="left" class="updateInnerColumn" colspan="2">

                            <div class="txtlbl">
                                <b><span style="color: Red;">
                                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                </span></b>
                            </div>
                        </td>

                    </tr>

                    <%--<tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" colspan="2">
                                    <asp:RadioButtonList ID="rdbRequestType" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True" Text="For Print"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="For Editing Company Details"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="rfdrdbRequestType" runat="server" ErrorMessage="Please Select Valid Reason"
                                    SetFocusOnError="true" ControlToValidate="rdbRequestType" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>

                                </td>
                                 
                               </tr>--%>

                    <tr valign="top" align="left">
                        <td valign="top" align="left" class="updateInnerColumn">Reason for Request :
                        </td>
                        <td valign="top" align="left" class="updateInnerColumn">
                            <asp:TextBox ID="txtRequest" runat="server" MaxLength="5000"
                                CssClass="txt_bx" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtrequest" runat="server" ErrorMessage="Please Enter Reason for Request"
                                SetFocusOnError="true" ControlToValidate="txtRequest" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>


                    <tr valign="top" align="left">
                        <td align="left" valign="top"></td>
                    </tr>
                    <tr valign="top" align="center">
                        <td valign="top" align="left"></td>
                        <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                ValidationGroup="1" CssClass="btnCls" OnClick="btnSubmit_Click" />
                            &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" class="btnCls" onclick="return FunResetcertificate();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="RFValidatorRequest" runat="server" ErrorMessage="Please provide Reason for request"
        SetFocusOnError="true" ControlToValidate="txtRequest" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="CustomValidatorRequest" runat="server" ErrorMessage="Reason should be less than 5000 characters"
        Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txtRequest"
        ValidationGroup="1"></asp:CustomValidator>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFValidatorRequest">
    </asp:ValidatorCalloutExtender>
    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CustomValidatorRequest">
    </asp:ValidatorCalloutExtender>


</asp:Content>

