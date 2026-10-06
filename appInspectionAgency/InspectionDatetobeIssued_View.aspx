<%@ Page Title="View-Inspection Date to be Issued" Language="C#" MasterPageFile="~/appMaster/Agency.master" AutoEventWireup="true" CodeFile="InspectionDatetobeIssued_View.aspx.cs" Inherits="appInspectionAgency_InspectionDatetobeIssued_View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">
    
    <div align="left" class="headerAll">
        View Companies waiting for Inspection date
    </div>  
    <div align="left" style="height:20px;">
    </div> 
    
    <div align="left" style="padding-left:10px; padding-right:10px;">
        <table border="0" cellpadding="0" cellspacing="0" width="500px">
            <tr align="left" valign="top" style="padding-bottom:10px;">
                <td align="right" valign="top" colspan="3">
                    <asp:LinkButton ID="lbBack" runat="server" CssClass="headerLink" 
                        onclick="lbBack_Click" style="padding-right:10px;">&lt;&lt; BACK</asp:LinkButton>
                </td>
                
            </tr>
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading" style="width:150px;">
                    Type 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top" style="width:320px;">
                    <asp:Label ID="lblType" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Company 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    SSI Registration No. 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblRegno" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    PAN 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblPan" runat="server"></asp:Label>
                </td>
            </tr>
            
            <tr align="left" valign="top">
                <td align="left" valign="top" class="leftHeading">
                    Request generation date 
                </td>
                <td align="left" valign="top" style="width:10px;">
                    :
                </td>
                <td align="left" valign="top">
                    <asp:Label ID="lblRequest" runat="server"></asp:Label>
                </td>
            </tr>
            
            
        </table>
    </div> 
    
</div>

</asp:Content>

