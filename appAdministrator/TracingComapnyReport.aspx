<%@ Page Title="Tracing Company Report" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="TracingComapnyReport.aspx.cs" Inherits="appAdministrator_TracingComapnyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <table cellpadding="3" cellspacing="3" border="0">
  <tr>
    <td align="left"  style="height:20px;"></td>
  
  </tr>
  
  <tr>
            <td align="left" class="headerAll" style="width: 170px;">
               Continuous Report
            </td>
        </tr>
        <tr><td style="height:10px;"></td></tr>
    <tr>
      <td align="center">
        <div align="left" class="clsrowdiv" >
            <div class="leftext">Search</div>
            <div class="coloncls"><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="Zonewise" Value="2"></asp:ListItem>
               <asp:ListItem Text="Branchwise" Value="3"></asp:ListItem>
            </asp:DropDownList></div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv"><asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" /></div>
        </div>
         <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
      </td>
    </tr>
  
    <tr>
      <td  style="width:100%; padding-left:20px;">
         <asp:GridView ID="grd_trace_company" runat="server"  CellPadding="3" 
                       CellSpacing="5" GridLines="Both" 
                  AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="20"
                Width="100%" BorderWidth="0"  HeaderStyle-VerticalAlign="Middle"  
              HeaderStyle-HorizontalAlign="Center" 
              onpageindexchanging="grdtrace_pageindexchanging" 
              onrowdatabound="grdtrace_on_Rowdatabound" >
              
              <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                   <Columns>
                    
                         <asp:TemplateField HeaderText ="Company Name">
                          <ItemTemplate  >
                          <asp:Label ID="lbl_companyname" runat="server" Text='<%#Eval("Company_Name")%>'></asp:Label>
                          </ItemTemplate>
                         
                         </asp:TemplateField>
                         <%--<asp:TemplateField HeaderText="Company Pan">
                         <ItemTemplate >
                         <asp:Label ID="lbl_Company_Pan" runat="server" Text='<%#Eval("Company_Pan")%>'></asp:Label>
                         
                         </ItemTemplate>
                           
                         </asp:TemplateField>--%>
                   <%--      <asp:TemplateField HeaderText="Registration Number">
                         <ItemTemplate>
                          <asp:Label ID="lbl_Registration_number" runat="server" Text='<%#Eval("RegistrationNumber") %>'></asp:Label>
                         </ItemTemplate>
                         
                         </asp:TemplateField>--%>
                         <asp:TemplateField HeaderText="Registration Date">
                         <ItemTemplate>
                           <asp:Label ID="lbl_registration_date" runat="server" Text='<%#Eval("RegistratonDate")%>'></asp:Label>
                         </ItemTemplate>
                         
                         </asp:TemplateField>
                         <%-- <asp:TemplateField HeaderText="TogetherTillDate">
                            <ItemTemplate>
                             <asp:Label ID="lbl_togetether_till_date" runat="server" Text='<%#Eval("Duration")%>'></asp:Label>
                            </ItemTemplate>
                          </asp:TemplateField>--%>
                      <asp:TemplateField HeaderText="Continuous Period">
                             <ItemTemplate>
                           <asp:Label ID="lbl_duration" runat="server" Text='<%#Eval("DurationTime")%>'></asp:Label>
                             </ItemTemplate>
                           </asp:TemplateField>
                      
                         
                     
                   
                   
                   </Columns>
            
         
         </asp:GridView>
      
      </td>
    
    </tr>
  
  </table>

</asp:Content>

