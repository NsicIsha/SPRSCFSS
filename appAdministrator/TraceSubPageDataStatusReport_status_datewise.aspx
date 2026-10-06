<%@ Page Title="Track" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="TraceSubPageDataStatusReport_status_datewise.aspx.cs" Inherits="appAdministrator_TraceSubPageDataStatusReport_status_datewise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
   .text
     {
            text-decoration:none;
     }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text=" Report" CssClass="headerAll"></asp:Label>
    </div>
    <table cellpadding="2" cellspacing="3" border="0" width="100%">
         <tr style="width:100%;  display:none;">
  <td align="left" style="padding-left:4px; width:240px;"><b>Enter  Company Name or Ref Number </b> </td>
    <td align="left" style="width:475px;">
    <asp:DropDownList ID="ddl_list" runat="server" AutoPostBack="true" Width="150" 
            CssClass="clsDDL" >
      <asp:ListItem Selected="True" Value="1">Company Name</asp:ListItem>
      <asp:ListItem  Value="2">Reference Number  </asp:ListItem>
    </asp:DropDownList>
     <asp:TextBox ID="txt_Ref_no" runat="server" Text="" Width="220px" MaxLength="250"></asp:TextBox></td>
<td align="left"> &nbsp;<asp:Button ID="btn_status_track" runat="server" 
        Text=" Track Status" Width="120" CssClass="btnCls"  
        
       /></td>
   
</tr>
 <tr><td colspan="3" style="height:10px;">

     <asp:Button ID="btn_chek" runat="server" Text="Genarate Excel" OnClick="btn_chek_Click"
                 />
     </td></tr>
 
  <tr>
     <td colspan="3">
       <asp:Label ID="lbl_report_status_track" runat="server" Text=""></asp:Label>
     </td>
   
   </tr>
    
    </table>
</asp:Content>

