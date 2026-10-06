<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sendmail.aspx.cs" Inherits="appAdministrator_Sendmail" %>

<%@ Register Src="../UserControl/headerReport.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title>
    
    </title>
    
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
<script type="text/javascript" language="javascript">


    function CheckEmpty() {
        var opt = 0;
        var validSummary = "Required to fill following fields";
       
        if (document.getElementById('txtSub').value == "") {
            validSummary += "\n -- Enter Subject";
            opt = 1;
        }

        if (document.getElementById('txtfrom').value == "--Company Email--") {
            validSummary += "\n -- Enter From Email";
            opt = 1;
        }
        if (document.getElementById('txtTo').value == " ") {
            validSummary += "\n -- Enter To Email";
            opt = 1;
        }
        if (document.getElementById('txtbody').value == "") {
            validSummary+="\n -- Enter Body Text";
            opt=1;
        }

        if (opt == 1) {
            alert(validSummary);
            return false;
        }
        return true;
    }


    $(document).ready(function() {
        var emailPat = /^([a-zA-Z0-9_.-])+@([a-zA-Z0-9_.-])+\.([a-zA-Z])+([a-zA-Z])+/;
        $("#txtTo").blur(function() {
            var tostr = $("#txtTo").val();
            if (!emailPat.test(tostr)) {
                $("#txtTo").after("<span class='EmailField'>Invalid Email</span");
            }
            else {
                $(".EmailField").hide();
            }
        });

        $("#txtfrom").blur(function() {
            var fromstr = $("#txtfrom").val();
            if (!emailPat.test(fromstr)) {
                $("#txtfrom").after("<span class='EmailField'>Invalid Email</span");
            }
            else {
                $(".EmailField").hide();
            }
        });

        $("#btnsendmail").click(function() {
            var haserror = false;
            $(".EmailField").hide();
            var tostr = $("#txtTo").val();
            if (!emailPat.test(tostr)) {
                $("#txtTo").after("<span class='EmailField'>Invalid Email</span");
                haserror = true;
            }

            var fromstr = $("#txtfrom").val();
            if (!emailPat.test(fromstr)) {
                $("#txtfrom").after("<span class='EmailField'>Invalid Email</span");
                haserror = true;
            }
            if (haserror == true) {
                return false;
            }
        });

    });
       
   </script>
   <style type="text/css">
   .EmailField
    {
	    color:#FF0000;
	    font-weight:bold;
    }
</style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
       
    
    <table cellpadding="0" cellspacing="0" border="0" align="center">
    <tr>
      <td  colspan="2" >
        <uc1:Header_Admin ID="HeaderAdmin1" runat="server" /> 
      
       </td>
    
    
    </tr>
    <tr>
     <td colspan="2" style ="height:20px;"></td>
    
    </tr>
    <tr>
    <td><b>From : </b></td>
    <td align="left">
        <%--<asp:Label ID="lblfrom" runat="server" ></asp:Label>--%>
        <asp:TextBox ID="txtfrom" runat="server" ></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td><b>To : </b></td>
    <td align="left">
        <%--<asp:Label ID="lblto" runat="server" ></asp:Label>--%>
        <asp:TextBox ID="txtTo" runat="server"></asp:TextBox>
     </td>
    </tr>
    <tr>
    <td><b>Subject :</b></td>
    <td  align="left"><asp:TextBox ID="txtSub" runat="server" Width="400" ></asp:TextBox></td>
    </tr>
    <tr>
    <td colspan="2">
     <asp:TextBox ID="txtbody" runat="server" 
            style="margin-bottom: 0px" TextMode="MultiLine" Height="200px" Width="700px"></asp:TextBox>
    </td>
    </tr>
    <tr>
    <td align="left" colspan="2"><asp:Button ID="btnsendmail" runat="server" Text="Send Mail" 
            OnClientClick="javascript:return CheckEmpty();" onclick="btnsendmail_Click" /></td>
    </tr>
    </table>
           
    
    </div>
    </form>
</body>
</html>
