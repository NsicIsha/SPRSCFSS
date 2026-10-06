<%@ Page Title="GP::Instruction View Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterContentView.aspx.cs" EnableEventValidation="false" ValidateRequest="false" Inherits="appAdministrator_MasterContentView" %>
<%@ Register Src="~/appAdministrator/RichTextEditor/RichTextEditor_1.ascx" TagName="RichTextEditor_1"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <%-- dsad--%>
<div align="left">
    <div align="left">
        <asp:Label ID="lblList" runat="server" Text="Master Content View" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height:20px;"></div>
    <table border="0" cellpadding="1" cellspacing="0">
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading" >
                Name :
            </td>
            
            <td align="left" valign="top">
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:Label ID="lblName" runat="server" CssClass="updateInnerColumn"></asp:Label>
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
        <tr align="right" valign="top">
            <td align="left" valign="top">
            </td>
            <td align="right" valign="top" colspan="2">
            <asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" CssClass="btnCls" 
                    ValidationGroup="1" onclick="btnSubmit_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btnCls" onclick="btnBack_Click"
                     />
                 
            </td>
        </tr>
    </table>
</div>

</asp:Content>

