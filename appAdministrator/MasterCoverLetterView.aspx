<%@ Page Title="GP::Cover Letter Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterCoverLetterView.aspx.cs" Inherits="appAdministrator_MasterCoverLetterView" EnableEventValidation="false" ValidateRequest="false" %>
<%@ Register Src="~/appAdministrator/RichTextEditor/RichTextEditor_1.ascx" TagName="RichTextEditor_1" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left"><%--dsad--%>
    <div align="left">
        <asp:Label ID="lblList" runat="server" Text="Master CoverLetter View" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height:20px;"></div>
    <table border="0" cellpadding="1" cellspacing="0">
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                From :
            </td>
            
            <td align="left" valign="top">
                    <asp:TextBox ID="txtFrom" runat="server" CssClass="txt_bx" Width="497px" 
                    ValidationGroup="1" MaxLength="2000"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFValidatortxtFrom" runat="server" ValidationGroup="1" ErrorMessage="Please provide From mailer" ControlToValidate="txtFrom" Display="None" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                
            </td>
        </tr>
        
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                Subject :
            </td>
            
            <td align="left" valign="top">
                    <asp:TextBox ID="txtSubject" runat="server" CssClass="txt_bx" Width="497px" 
                    ValidationGroup="1" MaxLength="2000"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RFValidatorSubject" runat="server" ValidationGroup="1" ErrorMessage="Please provide subject" ControlToValidate="txtSubject" Display="None" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                
            </td>
        </tr>
        
        <tr align="left" valign="top">
            <td align="left" valign="top" class="leftHeading">
                Body :
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
                    ValidationGroup="1" onclick="btnSubmit_Click"/>&nbsp;&nbsp;
                <asp:Button ID="btnBack" runat="server" Text="BACK" CssClass="btnCls" 
                    onclick="btnBack_Click"/>
                 
            </td>
        </tr>
    </table>
</div>

</asp:Content>

