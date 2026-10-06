<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExcelAdd.aspx.cs" Inherits="TestApplications_ExcelAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:GridView ID="grdexcel" runat="server" Width="100%" CellPadding="3"
                CellSpacing="0" BorderColor="Azure"></asp:GridView>
        </div>
        <div style="height:10px;"></div>
        <div>
            <asp:Button ID="btnexcel" runat="server" Text="Show Excel" 
                onclick="btnexcel_Click" />
        </div>
    
    </div>
    </form>
</body>
</html>
