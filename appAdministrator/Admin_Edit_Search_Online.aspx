<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Admin_Edit_Search_Online.aspx.cs" Inherits="appAdministrator_Admin_Edit_Search_Online" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        
        
        <table border = "0" cellpadding = "10px" cellspacing = "0" width = "550px">
          <tr>
            <td  valign ="middle" style = "font-family:Arial,Sans-Serif,Courier; font-size:12px; font-weight :bold; width:205px;" >
             Enter company name to search :
            </td>
            <%--<td align ="left"> 
             <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL" 
                    Width="200px">
            <asp:ListItem Selected="True" Text="Registration Process" Value="1"></asp:ListItem>
            <asp:ListItem  Text="Renewal Process" Value="2"></asp:ListItem>
              <asp:ListItem  Text="Amensdment Process" Value="3"></asp:ListItem>
            </asp:DropDownList>
            </td>--%>
            <td align ="left" style="width:210px;">
            <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox>
            </td>
            <td align ="left" style="width:100px;">
               <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click"  />
            </td>
          </tr>
          <tr>
           <td colspan ="3"></td>
          </tr>
        </table>
        
      <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gv_companies" runat="server" AutoGenerateColumns="False"
            Width="700px" BorderWidth="0px" CellPadding="3" CellSpacing="3" 
                AllowPaging="True" 
                AllowSorting="True" onrowcommand="gv_companies_RowCommand" 
                onrowdatabound="gv_companies_RowDataBound" 
              onpageindexchanging="gv_companies_PageIndexChanging"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <EmptyDataTemplate>
                <strong>Sorry No Record Found</strong>
            </EmptyDataTemplate>
            <Columns>
                  <asp:BoundField DataField="no" HeaderText="S. No." />
                <asp:BoundField DataField="comprefno" HeaderText="SPR No." />
             <%--   <asp:BoundField DataField="compname" HeaderText="Company Name" />--%>
                        <asp:TemplateField HeaderText="Company Name">
                                <ItemTemplate>
                                  <asp:Label ID="lbl_compname" runat="server" Text='<%#Eval("compname") %>'></asp:Label>
                                 
                                  
                                  <asp:Label ID="lbl_hs_code" runat="server" Text='<%#Eval("HSCode") %>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                <asp:BoundField DataField="comppan" HeaderText="PAN No."/>
                <asp:BoundField DataField="comadd" HeaderText="Address" />
                <asp:TemplateField HeaderText="Edit Store" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                     
                            <asp:LinkButton ID="lnkeditstore" runat="server" CommandArgument = '<%#Eval("compid") %>' CausesValidation ="false"  CommandName = "editstore" Text="Edit"></asp:LinkButton>
                        <%--<asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("compid") %>' />--%>
                     
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               
                    <asp:TemplateField HeaderText="Edit Monetary Limit" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                     
                            <asp:LinkButton ID="lnkeditmonlmt" runat="server" CommandArgument = '<%#Eval("compid") %>' CausesValidation ="false"  CommandName = "editmonlimit" Text="Edit"></asp:LinkButton>
                        <%--<asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("compid") %>' />--%>
                     
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                  
                <asp:TemplateField HeaderText="Edit Company" 
                    HeaderStyle-HorizontalAlign="Center"  ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                     
                            <asp:LinkButton ID="lnkeditcompany" runat="server" CausesValidation ="false" CommandArgument = '<%#Eval("compid") %>'  Text="Edit" CommandName = "editcompany" ></asp:LinkButton>
                            <asp:HiddenField ID="hdnPrintAccess" runat="server" Value='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess") %>' />
                            <%--<asp:HiddenField ID="hdnAdminAccept" runat="server" Value='<%#Eval("fGPtbl_CR_AcceptedByAdmin") %>' />--%>
                     
                    </ItemTemplate>

<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
               </Columns>
        </asp:GridView>    
        
</div>

</asp:Content>

