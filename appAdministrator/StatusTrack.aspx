<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="StatusTrack.aspx.cs" Inherits="appAdministrator_StatusTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  <script type="text/javascript" language="javascript">
      function validate() {
          var Ref_No = document.getElementById("<%=txt_Ref_no.ClientID %>").value;
         // alert(Ref_No);
          if (Ref_No == "" || Ref_No==null) {
              alert("Please Enter Ref No or Company Name");
              return false;
          }
        
      }
  
  </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="700px"  style="margin-top:10px;">
  
<tr>
            <td align="left"  colspan="3" class="headerAll" style="width: 170px;">
                 Track Status
            </td>
        </tr>
        
        <tr>
          <td colspan="3" style="height:10px;"></td>
        </tr>

<tr style="width:800px;">
  <td align="left" style="padding-left:4px; width:240px;"><b>Enter  Company Name or Ref Number </b> </td>
    <td align="left" style="width:475px;">
    <asp:DropDownList ID="ddl_list" runat="server" AutoPostBack="true" Width="150" 
            CssClass="clsDDL" onselectedindexchanged="ddl_list_SelectedIndexChanged">
      <asp:ListItem Selected="True" Value="1">Company Name</asp:ListItem>
      <asp:ListItem  Value="2">Reference Number  </asp:ListItem>
    </asp:DropDownList>
     <asp:TextBox ID="txt_Ref_no" runat="server" Text="" Width="220px"></asp:TextBox></td>
<td align="left"> &nbsp;<asp:Button ID="btn_status_track" runat="server" 
        Text=" Track Status" Width="120" CssClass="btnCls"  OnClientClick="javascript:return validate();" 
        onclick="btn_status_track_Click"  /></td>
   
</tr>
 <tr><td colspan="3" style="height:10px;"></td></tr>
 
  <%-- <tr><td colspan="3" style="width:750px; padding-left:10px;">
     <asp:GridView ID="grd_viewstatus" runat="server"  CellPadding="2" CellSpacing="2"
      AutoGenerateColumns="false" AllowPaging="false" AllowSorting="false" Width="650">
       <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                            <RowStyle CssClass="GrdviewRowOdd" />
                            <AlternatingRowStyle CssClass="GrdviewRowEven" />
        <Columns>
        <asp:TemplateField HeaderText="Company Name">
        
          <ItemTemplate>
           <asp:Label ID="lbl_Comapny_name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
           
          </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField HeaderText="SPR Refrence No">
           <ItemTemplate>
             <asp:Label ID="lbl_refrence_no" runat="server" Text='<%#Eval("GPRefrenceNo") %>'></asp:Label>
           
           </ItemTemplate>
         
         </asp:TemplateField>
         <asp:TemplateField HeaderText="Inspection Status">
         <ItemTemplate>
          <asp:Label ID="lbl_inspection_status" runat="server" Text='<%#Eval("InspectionStatus") %>'></asp:Label>
         </ItemTemplate>
         </asp:TemplateField> 
         <asp:TemplateField HeaderText="Monetary Limit">
         <ItemTemplate>
           <asp:Label ID="lbl_monetary_limit" runat="server" Text='<%#Eval("MonetaryLimit")%>'></asp:Label>
         </ItemTemplate>
         </asp:TemplateField>
         <asp:TemplateField  HeaderText="Zonal Accept Status">
           <ItemTemplate>
            <asp:Label ID="lbl_zonal_accept_staus" runat="server" Text='<%#Eval("ZonalAcceptStatus") %>'></asp:Label>
           
           </ItemTemplate>
         
         </asp:TemplateField>
          <asp:TemplateField HeaderText="Company Status">
            <ItemTemplate>
              <asp:Label ID="lbl_company_status" runat="server" Text='<%#Eval("CompanyStatus") %>'></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
     </Columns>
      
     </asp:GridView>
   
   </td></tr>
    <tr><td colspan="3" style="height:10px;"></td></tr>
   
   <tr><td colspan="3">
     <asp:Label ID="lbl_branch_account_chek" runat="server" Text=""></asp:Label>
   
   </td> </tr>--%>
   
   
   <tr>
     <td colspan="3">
       <asp:Label ID="lbl_report_status_track" runat="server" Text=""></asp:Label>
     </td>
   
   </tr>

</table>




</asp:Content>

