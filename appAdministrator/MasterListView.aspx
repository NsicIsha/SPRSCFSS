<%@ Page Title="GP::List Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterListView.aspx.cs" Inherits="appAdministrator_MasterListView" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/appAdministrator/RichTextEditor/RichTextEditor_1.ascx" TagName="RichTextEditor_1"
    TagPrefix="uc1" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
    <div align="left">
        <asp:Label ID="lblList" runat="server" Text="Master ListView" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height:20px;"></div>
    <table border="0" cellpadding="1" cellspacing="0">
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                Name :
            </td>
            
            <td align="left" valign="top">
                <%if (listAction==0){%>
                    <asp:TextBox ID="txtName" runat="server" CssClass="txt_bx" Width="497px" 
                    ValidationGroup="1" MaxLength="2000"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFValidatortxtName" runat="server" ValidationGroup="1" ErrorMessage="Please provide Name" ControlToValidate="txtName" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderName" TargetControlID="RFValidatortxtName" runat="server">
                </cc1:ValidatorCalloutExtender>
                <%}%>
                <%else {%>
                    <asp:Label ID="lblName" runat="server" CssClass="updateInnerColumn"></asp:Label>
                <%}%>
            </td>
        </tr>
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                Content :
            </td>
            
            <td align="left" valign="top">
            <uc1:RichTextEditor_1 ID="RichTextEditor_1_1" runat="server" />
            </td>
        </tr>
       
        <tr align="left" valign="top">
            <td align="left" valign="top" colspan="2" style="height:10px;">
            </td>
        </tr>
        
            
        <tr>
        <td align="left" valign="top">
            </td>
            <td align="right" valign="top" colspan="2">
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btnCls" 
                    ValidationGroup="1" onclick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btnCls" onclick="btnBack_Click" 
                     />
                 
            </td>
        </tr>
    </table>
</div>
</asp:Content>

