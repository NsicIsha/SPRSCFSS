<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="AdminEditPayment.aspx.cs" Inherits="appAdministrator_AdminEditPayment" Title="NSIC SPR :: Admin Edit Payment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="padding-left:10px;">
    <div class="headerAll" style="text-align:left; padding-left:5px;">
        Update Last Year Turnover
    </div>
    <div style="height:10px;">
        
    </div>
    
    <table border="0" cellpadding="0" cellspacing="0" width="90%">
        <tr valign="top" align="left" style="width:90%;">
            <td valign="top" align="left" style="width:40%;">
                Last Year Turnover (in lacs)
            </td>
            <td valign="top" align="left" style="width:60%; height:30px;">
                <asp:Label ID="lblLastYearTurn" CssClass="leftext" Width="100%" runat="server"></asp:Label>
            </td>
        </tr>
        <tr style="height:20px;">
            <td colspan="2"></td>
        </tr>
        <tr valign="top" align="left">
            <td valign="top" align="left">
                Revised Last Year Turnover (in lacs)
            </td>
            <td valign="top" align="left">
                <asp:TextBox ID="txtLastyearTurnover" runat="server" CssClass="txt_bx150"></asp:TextBox>
            </td>
        </tr>
    </table>
    
    <div style="height:10px;"></div>
    
    <div id="divcalculate" visible="false" runat="server" style="width:100%; text-align:left;">
        <div style="width:55%; text-align:left; float:left;">
            Fees to be paid on revised turnover
        </div>
        <div style="width:35%; text-align:left;float:left;">
            <asp:Literal ID="ltlcurrentturnfee" runat="server"></asp:Literal>
        </div>
        <div style="width:55%; text-align:left; float:left;">
            Fee paid (Last):
        </div>
        <div style="width:35%; text-align:left; float:left; ">
            <asp:Literal ID="ltllastFeepaid" runat="server" ></asp:Literal>
        </div>
        
        <div style="width:55%; text-align:left; float:left; color:Red; font-weight:bold;">
            <asp:Label ID="lblpayfee" runat="server"></asp:Label>
        </div>
        <div style="width:35%; text-align:left;float:left; font-weight:bold; color:Red;">
            <asp:Literal ID="ltlpayback" runat="server"></asp:Literal>
        </div>
        <div style="width:90%; text-align:left;">
            
        </div>0    
    </div>
  <div style="height:10px;"></div>
    
    <div style="padding-top:10px; padding-bottom:10px; float:none;">
        <asp:Button ID="btnSubmit" runat="server" CssClass="btnCls" Text="Show payment" 
            onclick="btnSubmit_Click" Width="125px" Visible="false" />
    </div>
    
    </div>
</asp:Content>

