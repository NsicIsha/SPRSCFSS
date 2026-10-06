<%@ Page Language="C#" AutoEventWireup="true" CodeFile="test_repeat.aspx.cs" Inherits="test_repeat" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
    <script type="text/javascript">
        $(".view").live("click", function () {
            var row = $(this).closest("tr");
            alert("Name: " + jQuery.trim($(".name", row).text()) + " Age: " + jQuery.trim($(".age", row).text()));
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <asp:Repeater ID="rpt" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td class="name">
                        <%# Eval("Name") %>
                    </td>
                    <td class="age">
                        <%# Eval("Age") %>
                    </td>
                    <td>
                        <a class="view" href="javascript:;">View</a>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
    </form>
</body>
</html>
