<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CurrentLeadCompany.aspx.cs" ValidateRequest="false"  Inherits="appAdministrator_CurrentLeadCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <div align="left">
        <asp:Label ID="lblCertificateRequest" runat="server" Text="Incomplete Application Forms" CssClass="headerAll"></asp:Label>
    </div>
    <div style="width:100%;">
       <asp:UpdatePanel ID="updat" runat="server" UpdateMode="Always">
       <ContentTemplate>
        <table  border="0" cellpadding="2" cellspacing="2" width="100%">
         <tr>
             <td  align="right">
                  <asp:DropDownList ID="ddl_zonewise" runat="server"  AutoPostBack="true"
                 
                     
                      CausesValidation="false" 
                      onselectedindexchanged="ddl_zonewise_SelectedIndexChanged1">
                      
                      </asp:DropDownList>
                
             
               </td>
               
               <td  align="left">
                   <asp:DropDownList ID="ddl_branch_wise" runat="server" AutoPostBack="true" 
                       onselectedindexchanged="ddl_branch_wise_SelectedIndexChanged"></asp:DropDownList>
                     
               </td>
               <td colspan="2" align="left">
            <span >Filled By </span>     <asp:DropDownList ID="ddl_company_filled" runat="server" AutoPostBack="true" 
                       onselectedindexchanged="ddl_company_filled_SelectedIndexChanged">
                   <asp:ListItem Selected="True" Value="0"> All</asp:ListItem>
                   <asp:ListItem Value="1">Company</asp:ListItem>
                   <asp:ListItem Value="2">Branch</asp:ListItem>
                  </asp:DropDownList>
               </td>
         </tr>
         </table>
       
       </ContentTemplate>
       
       </asp:UpdatePanel>
    </div>
    
    
    <div style="width:100%;">
      <table border="0" cellpadding="2" cellspacing="2" width="100%">
         
         <tr>
            <td colspan="4"  style="width:100%;">
               <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                         AllowPaging="True" AllowSorting="True" PageSize="50"  PageIndex="0"
                    EmptyDataText="No Reord Found" 
                    onpageindexchanging="gvDetails_PageIndexChanging" 
                    onrowdatabound="gvDetails_RowDataBound">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                          <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <EmptyDataRowStyle BackColor="#0099CC" ForeColor="#FF3300" />
                                        <Columns>
                                               <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Company Name">
                                               <ItemTemplate>
                                                  <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("ComapnyName") %>'></asp:Label>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                           <%-- <asp:TemplateField HeaderText="PAN Number">
                                            <ItemTemplate>
                                              <asp:Label ID="lbl_pan_number" runat="server" Text='<%#Eval("Pan_number") %>'></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText=" PAN/AADHAR Number">
                                               <ItemTemplate>
                                                   <asp:Label ID="lbl_adhar_number" runat="server" Text='<%#Eval("AAdhar_number") %>'></asp:Label>
                                               </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Zone/Branch">
                                                <ItemTemplate>
                                                  <asp:Label ID="lbl_zone_branch" runat="server" Text='<%#Eval("zonebranch") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lead Date">
                                                <ItemTemplate>
                                                  <asp:Label ID="lbl_create_date" runat="server" Text='<%#Eval("CreateDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            

                                            <asp:TemplateField HeaderText="Filled By">
                                                <ItemTemplate>
                                                  <asp:Label ID="lbl_filled_by" runat="server" Text='<%#Eval("CompanyorBranch") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                                                                        
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                  <asp:LinkButton ID="lnkview" runat="server" CommandName="View" CommandArgument='<%#Eval("Companyid") %>'
                       CausesValidation="false" ToolTip="View">View</asp:LinkButton>&nbsp;
                                    <asp:HiddenField ID="HiddenFieldCompanyIdview" runat="server" Value='<%#Eval("Companyid") %>' />
                       
                       <asp:HiddenField ID="HiddenFieldBranch" runat="server" Value='<%#Eval("BranchName") %>' />
                      
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        </asp:GridView>
            </td>
         </tr>
       </table>
        </div>
    
    </div>

</asp:Content>

