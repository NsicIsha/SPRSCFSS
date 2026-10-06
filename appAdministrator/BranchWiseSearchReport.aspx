<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BranchWiseSearchReport.aspx.cs" Inherits="appAdministrator_BranchWiseSearchReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Dashboard Report" CssClass="headerAll"></asp:Label>
    </div>
    <div style="height:10px;"></div>
    
<table cellpadding="2" cellspacing="2" border="0" >
    <%--fdsfa--%><%--dsad--%>

  <tr>
    <td align="center" style="width:500px; padding-left:140px;">
    
      <asp:GridView ID="grd_branchwise" runat="server" CellPadding="5" CellSpacing="2" 
            onrowdatabound="grd_branchwise_RowDataBound" AutoGenerateColumns="false" Width="500">
      <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                    <Columns>
                    
                       <asp:TemplateField HeaderText="Branch">
                         <ItemTemplate>
                          <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                          <asp:Label ID="lbl_Branch_Id" runat="server" Text='<%#Eval("BranchID")%>' Visible="false"></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Live Units">
                         <ItemTemplate>
                         <asp:Label ID="lbl_live_unit" runat="server" Text='<%#Eval("LiveUNit")%>'></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="Expired Units">
                         <ItemTemplate>
                           <asp:Label ID="lbl_Expire_Unit" runat="server" Text='<%#Eval("ExpiredUnit")%>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                      <asp:TemplateField HeaderText="Underprocess Units">
                         <ItemTemplate>
                           <asp:Label ID="lbl_Underprocess_Unit" runat="server" Text='<%#Eval("UnderProcessUnit")%>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                     
                     <asp:TemplateField HeaderText="Total Units">
                     <ItemTemplate>
                      <asp:Label ID ="lbl_total_unit" runat="server" Text='<%#Eval("TotalCount")%>'></asp:Label>
                     </ItemTemplate>
                     </asp:TemplateField>
                     
 
                   </Columns>
      
      </asp:GridView>
        
    
    </td>
  
  </tr>
  <tr>
    <td style="height:10px;"></td>
  </tr>
  <tr>
    <td>
        <asp:Button ID="btnBackZonewiseSearch" runat="server" CausesValidation="false" 
            Text="Back" CssClass="btnCls" onclick="btnBackZonewiseSearch_Click" />
    </td>
  </tr>

</table>


</asp:Content>

