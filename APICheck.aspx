';'n<%@ Page Language="C#" AutoEventWireup="true" CodeFile="APICheck.aspx.cs" Inherits="APICheck" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Button ID="Button1" runat="server" Text="Get Unique Code" OnClick="Button1_Click" />
        <br />
          <asp:Button ID="Button2" runat="server" Text="Get Unique Code2" OnClick="Button2_Click" />
        <br />

        <asp:Label ID="lblUniqueVCode" runat="server"></asp:Label>
        <br />
    <asp:Label ID="lblMsg" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
