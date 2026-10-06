<%@ Page Title="GP::Certificate Request View" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CertificateRequestList_View.aspx.cs" Inherits="appAdministrator_CertificateRequestList_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"> <%--check--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
        <div align="left">
            <asp:Label ID="lblCertificate" runat="server" Text="Certificate Request View" CssClass="headerAll"></asp:Label>
        </div>
        <div align="left" style="padding-left:10px; padding-right:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="500px">
            <tr align="left" valign="top" style="padding-bottom:10px;">
                <td align="right" valign="top" colspan="3">
                    <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="~/appAdministrator/CertificateRequestList.aspx"
                        style="padding-right:10px;">&lt;&lt;BACK</asp:HyperLink>
                </td>
                
            </tr>
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading" style="width:110px;">
                    Company name 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top" style="width:320px;">
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Request date
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblReqdate" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Reason for request
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblReason" runat="server"></asp:Label>
                </td>
            </tr>
           
            
        </table>
    </div>
    </div>
</asp:Content>

