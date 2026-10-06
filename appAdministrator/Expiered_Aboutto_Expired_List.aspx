<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Expiered_Aboutto_Expired_List.aspx.cs" Inherits="appAdministrator_Expiered_Aboutto_Expired_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table cellpadding="0" cellspacing="0" width="700px"  style="margin-top:10px;">
  
<tr>
            <td align="left"  colspan="5" class="headerAll" style="width: 170px;">
                 
            </td>
        </tr>
        
        <tr>
          <td colspan="5" style="height:10px;"></td>
        </tr>

<tr style="width:800px;">
  <td style="padding-left:10px;" align="left" ><b>Month </b> </td>
    <td align="left" >
    <asp:DropDownList ID="ddl_month" runat="server"  Width="150" 
            CssClass="clsDDL" >
    </asp:DropDownList>
    </td>
      <td align="left" ><b>Year </b> </td>
    <td align="left" >
    <asp:DropDownList ID="ddl_year" runat="server"  Width="150" 
            CssClass="clsDDL" >
    </asp:DropDownList>
    </td>
     <td align="left"> &nbsp;<asp:Button ID="btn_submit" runat="server" 
        Text="Submit" Width="120" CssClass="btnCls" onclick="btn_submit_Click"  
         /></td>
</tr>
 <tr><td colspan="5" style="height:10px;"></td></tr>
 
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
     <td colspan="5"  style="padding-left:10px;" >
    <%--   <asp:Label ID="lbl_report_status_track" runat="server" Text=""></asp:Label>--%>
    
      <asp:GridView ID="gv_display" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
               AllowPaging="True" AllowSorting="True" PageSize="10" 
             onpageindexchanging="gv_display_PageIndexChanging"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="SPR No." />
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                 <asp:BoundField DataField="regdate" HeaderText="Registration Date" />
               
               </Columns>
        </asp:GridView>    
    
     </td>
   
   </tr>

</table>
</asp:Content>

