<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="printpagecount.aspx.cs" Inherits="appAdministrator_printpagecount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css" >
     .txtlbl
{
	height:20px;
	width:20%;
	float:right;
	white-space:nowrap;
	vertical-align:top;
	padding-left:600px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Companies List"></asp:Label>
        </div>
        
        <div align="left" style="height:10px;">
            
        </div>
        
        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
           <div style="height:10px;">
        
        </div>
        
             <div align="left" class="clsrowdiv" >
            <div class="leftext">Search</div>
            <div class="coloncls"><asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
               
            </asp:DropDownList></div>
            <div class="txtdiv"><asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv"><asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" /></div>
                    
         
        </div>
        
         <div style="height:10px;">
        
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
         <asp:GridView ID="grd_printcount_certificate" runat="server" 
                AutoGenerateColumns="false" AllowPaging="true"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                onpageindexchanging="grd_view_pageindexchanging" 
                onrowdatabound="grd_view_onrowdatabound" >
          <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            
             <Columns>
                 <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                  
                      <%#Container.DataItemIndex + 1 %>
 
                    </ItemTemplate>
                </asp:TemplateField>  
                    <asp:TemplateField HeaderText="Company Name">
                      <ItemTemplate>
                       <table cellpadding="0" cellspacing="0" border="0">
                         <tr>
                           <td align="left" valign="top">
                            <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                              <asp:Label ID="lbl_comapny_id" runat ="server" Text='<%#Eval("CompanyID") %>' Visible="false"></asp:Label>
                           </td>
                         </tr>
                         <tr>
                           <td align="left" valign="top">
                             <asp:Label ID="lbl_companyAddress" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                           </td>
                         </tr>
                       </table>
                       
                        
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Conatct Details">
                       <ItemTemplate>
                           <asp:Label ID="lbl_conatct_details" runat="server" Text='<%#Eval("ContactDetails")%>'></asp:Label>
                       </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="PAN No.">
                        <ItemTemplate>
                          <asp:Label ID="lbl_pan" runat="server" Text='<%#Eval("CompanyPan")%>'></asp:Label>
                        </ItemTemplate>
                      </asp:TemplateField>
                    <asp:TemplateField HeaderText="Zone/Branch">
                      <ItemTemplate>
                        <asp:Label ID="lbl_Zone_branch_id" runat="server" Text='<%#Eval("ZoneBranch") %>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Print Count">
                      <ItemTemplate>
                        <asp:Label ID="lbl_print_count" runat="server" Text='<%#Eval("PrintCount") %>'></asp:Label>
                      
                      </ItemTemplate>
                    
                    </asp:TemplateField>
                    
                    <asp:TemplateField  HeaderText="UserName">
                      <ItemTemplate>
                        <asp:Label ID="lbl_user_name" runat="server" Text='<%#Eval("UserName")%>'></asp:Label>
                      </ItemTemplate>
                    </asp:TemplateField>
             
             </Columns>
         
         </asp:GridView>
        
        
        </div>
   

</asp:Content>

