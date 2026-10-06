<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PointOfInterest_View_Admin_Dwn.aspx.cs" Inherits="appAdministrator_PointOfInterest_View_Admin_Dwn" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="grd_search_result" runat="server"  CellPadding="3" 
                       CellSpacing="3" GridLines="Both" 
                  AutoGenerateColumns="true" AllowPaging="true" AllowSorting="true" PageSize="500"
                Width="100%" BorderWidth="0" 
                      
                   


                       HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" class="grdserchkk">
                  <HeaderStyle CssClass="dynRowHeader" />
                   <RowStyle CssClass="dynRowEven" />
                   <AlternatingRowStyle CssClass="dynRowOdd" />
                   <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                  <EmptyDataTemplate>
                                                <div align="center"><strong style="font-size:18px; font-weight:bold; color:Red;"> No  Data  Found !!!</strong></div>
                           </EmptyDataTemplate>
                 
                </asp:GridView>   
 <asp:Button ID="btn_chek" CssClass="btn-primary" runat="server" Text="Generate Excel" OnClick="btn_chek_Click"  />

        <asp:Button ID="ffsdfsf" Text="SSSSS" runat="server" OnClick="ffsdfsf_Click" />
    </div>
    </form>
</body>
</html>
