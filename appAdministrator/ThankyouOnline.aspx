<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="ThankyouOnline.aspx.cs" Inherits="appAdministrator_ThankyouOnline" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table width="100%">
<tr>
            <td align="left" valign="top" style="padding-left:10px; padding-right:10px;">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" align="center" >
                   <tr>
                   <td align="center" >
                   <asp:Label ID="lblMsg" runat="server" Font-Bold="true"  ></asp:Label>
                   </td>
                   </tr>
                   <tr>
                        <td style="height:10px;"></td>
                   </tr>
                   <tr>
                        <td align="center" valign="top" style="font-size:14px; font-weight:bold;">
                            For further help contact 
                        </td>
                   </tr>
                   <tr>
                        <td align="center" valign="top" style="text-align:left; padding-left:50px; padding-top:10px; font-weight:bold; font-size:14px;">
                            Branch office: &nbsp;<asp:Literal ID="ltlBranchAddress" runat="server" ></asp:Literal>
                        </td>
                   </tr>
                   <tr>
                        <td align="center" valign="top" style="text-align:left; padding-left:50px; font-weight:bold; font-size:14px;">
                            Phone: &nbsp;<asp:Literal ID="ltlBranchPhone" runat="server" ></asp:Literal>
                        </td>
                   </tr>
                   <tr>
                        <td align="center" valign="top" style="text-align:left; padding-left:50px; font-weight:bold; font-size:14px;">
                            Fax: &nbsp;<asp:Literal ID="ltlBranchFax" runat="server"></asp:Literal>
                        </td>
                   </tr>
                   <tr>
                        <td align="center" valign="top" style="text-align:left; padding-left:50px; font-weight:bold; font-size:14px;">
                            Email: &nbsp;<asp:Literal ID="ltlBranchEmail" runat="server"></asp:Literal>
                        </td>
                   </tr>
                   
                    
                </table>
             </td> 
         </tr>
 </table>
         
</asp:Content>

