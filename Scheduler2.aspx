<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scheduler2.aspx.cs" Inherits="Scheduler2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
      <table>
            <tr>
                <td >companies whose next renewal date nears up
                <asp:Label ID="lbl_regDate" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
      </table>    
    </div>
    </form>
</body>
</html>
