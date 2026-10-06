<%@ Page Language="VB" AutoEventWireup="false" CodeFile="test2.aspx.vb" Inherits="test2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>4545 Page</title>
     <style type="text/css">
        .clsTxt {
            width: 200px;
            min-height: 25px;
            max-height: 100%;
            resize: none;
        }
    </style>
    <script  language="javascript" type="text/jscript">
        function resizeTextBox(txt) {
            txt.style.height = "1px";
            txt.style.height = (1 + txt.scrollHeight) + "px";
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table>
                <tr>
                    <td>Description: </td>
                    <td>                       
                        <asp:TextBox ID="txtDescription" CssClass="clsTxt" runat="server" onkeyup="resizeTextBox(this)" TextMode="MultiLine"></asp:TextBox>
                        <%--<textarea class="clsTxt" onkeyup="resizeTextBox(this)"></textarea>--%>
                    </td>
                </tr> 
            </table> 
        </div>
    </form>
</body>
</html>
