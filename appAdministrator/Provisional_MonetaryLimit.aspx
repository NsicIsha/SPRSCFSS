<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Provisional_MonetaryLimit.aspx.cs" Inherits="appAdministrator_Provisional_MonetaryLimit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div align="left">
        <asp:Label ID="lblMonetary" runat="server" Text="Monetary limit for Provisional Company" CssClass="headerAll"></asp:Label>
    </div> <br /><%--dsad--%>
       <div align="left" style="padding-left: 10px; padding-right: 10px;">
       
        <asp:GridView ID="gvmonetary"  
         Width="90%" BorderWidth="0px" CellPadding="0"
      runat="server"  AutoGenerateColumns="False" 
        
       >
      <HeaderStyle CssClass="dynRowHeader" />
        <RowStyle CssClass="dynRowEven" />
         <AlternatingRowStyle CssClass="dynRowOdd" />
            <Columns>
                <asp:BoundField DataField="Year" HeaderText="Year" HtmlEncode="False" />
                <asp:BoundField DataField="Turnover" HeaderText="Turnover" HtmlEncode="False" />
            </Columns>
         <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
 </asp:GridView>
 <br /><br />
 <center><asp:Button runat ="server" Text ="Submit" ID ="btn_submit" 
         CssClass="btnCls" onclick="btn_submit_Click" />
   </center>     <br /><br />
    </div>
</asp:Content>

