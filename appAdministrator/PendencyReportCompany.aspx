<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PendencyReportCompany.aspx.cs" Inherits="appAdministrator_PendencyReportCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text="Pendency Report - Details" CssClass="headerAll"></asp:Label>
    </div><br />
    
      <div style="width:100%; margin-left:2%;"  >
            <asp:GridView ID="grd_company_wise" runat="server" CellPadding="2" CellSpacing="2"
               GridLines="Both" AutoGenerateColumns="false" Width="100%"
              BorderWidth="2" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left" 
            HeaderStyle-HorizontalAlign="Center"    ShowFooter="false" 
                     EmptyDataText ="No data.">
                     <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <FooterStyle CssClass="dynRowHeader" />
                   <EditRowStyle BackColor="LightBlue" ForeColor="Red" />
                   <Columns>
                      <asp:TemplateField HeaderText="SNO">
                         <ItemTemplate>
                           <asp:Label ID="lbl_Sno" runat="server" Text='<%#Eval("SNO") %>'></asp:Label>
                          </ItemTemplate>
                     </asp:TemplateField>
                       <asp:TemplateField HeaderText="COMPANY">
                       <ItemTemplate>
                       <asp:Label ID="lbl_Company_Name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                       </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="BRANCH NAME">
                       <ItemTemplate>
                       <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                       </ItemTemplate>
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="APPLICATION DATE">
                       <ItemTemplate>
                       <asp:Label ID="lbl_application_date" runat="server" Text='<%#Eval("ApplicationDate")%>'></asp:Label>
                       </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="STATUS">
                       <ItemTemplate>
                       <asp:Label ID="lbl_status_date" runat="server" Text='<%#Eval("Status ")%>'></asp:Label>
                       </ItemTemplate>
                       </asp:TemplateField>
                       <asp:TemplateField HeaderText="PENDENCY DAY">
                         <ItemTemplate>
                         <asp:Label ID="lbl_pendency_day" runat="server" Text='<%#Eval("pendency_date") %>'></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                       
                       <asp:TemplateField HeaderText="PENDENCY DATE">
                         <ItemTemplate>
                         <asp:Label ID="lbl_pendency_date" runat="server" Text='<%#Eval("CompanystatusDate") %>'></asp:Label>
                         </ItemTemplate>
                       </asp:TemplateField>
                   </Columns>
                
            </asp:GridView>
          
          </div>
</asp:Content>

