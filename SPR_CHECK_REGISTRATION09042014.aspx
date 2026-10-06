<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/LoginMaster.master" AutoEventWireup="true" CodeFile="SPR_CHECK_REGISTRATION.aspx.cs" Inherits="SPR_CHECK_REGISTRATION" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <table border = "0" cellpadding = "10px" cellspacing = "0" width = "70%" style="height:400px;">
          <tr>
            <td  valign ="middle" style = "font-family:Arial,Sans-Serif,Courier; font-size:12px; font-weight :bold; width:156px;" >
             Enter Registration&nbsp; No&nbsp; to search :
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
        <asp:GridView ID="gv_companies" runat="server" 
            Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" AutoGenerateColumns="False" 
                > 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
            <EmptyDataTemplate>
                <strong>Sorry No Record Found</strong>
            </EmptyDataTemplate>
            <Columns>
                 
                <asp:BoundField DataField="Compnay" HeaderText="Company Name" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12"/>
                <asp:BoundField DataField="Address" HeaderText="Company Address" HtmlEncode ="false" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />
                <asp:BoundField DataField="RegistrationNo" HeaderText="Registration No" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />
                <asp:BoundField DataField="validity" HeaderText="Validity" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />
                <asp:BoundField DataField="Product" HeaderText="Products" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="12" />
                 
               </Columns>
        </asp:GridView>    
        
</div>

</asp:Content>

