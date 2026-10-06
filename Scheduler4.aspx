<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scheduler4.aspx.cs" Inherits="Scheduler4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--<style type="text/css">
    .dir
    {
        text-align:center;
         white-space:nowrap;
          font-family:Arial;
          background-color:Gray;
          
          
          
    }
    .rowHeader
    {
       color:Blue;
       
    }
    .tblborder
    {
        border-color:Maroon;
         border-width:1px thick;
        }
        
     </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
    <table align="center">
          <tr>
             <td><%--<b>List of companies whose forms are submitted but date of inspection not issued.</b>--%>
             <%--This mail is regarding to issue inspection date of the companies whose forms are submitted but there inspection date is not issued.</br>
             List of companies are :--%>
             <br/><br/>
               <asp:Label ID="lblcompany" runat="server" Text="" CssClass="dir" Visible="false"></asp:Label>
               <br/>
               <%--<b>From</b><br/>
               <b>Admin</b><br />
               <b>NSIC GP</b><br />--%>
               
             </td>
          </tr>
           <tr style="width:10px";>
             <td style="width:10px";></td>
          </tr>
    </table>
    
        
    </div>
    </form>
</body>
</html>
