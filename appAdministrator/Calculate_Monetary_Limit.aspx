<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Calculate_Monetary_Limit.aspx.cs" Inherits="appAdministrator_Calculate_Monetary_Limit" %>

<script runat="server">

 
  
</script>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
  .noboder
  {
  	 border-width:0;
  	}
</style>

    <script src="../JScript/jquery-1.8.2.js" type="text/javascript"></script>
    
  <script type="text/javascript" language="javascript">

//      $(document).ready(function() {
//          
//          $("#yesnovalue").val("1");
//          $("#<%=rdo_plant_machinary.ClientID %>").change(function() {
//              //  var valueyesno = $("#<%=rdo_plant_machinary.ClientID %>").find(":checked").val();
//              var valueyesno = $("input[name='<%=rdo_plant_machinary.UniqueID%>']:radio:checked").val();
//              
//              if (valueyesno != "") {
//                  $("#yesnovalue").val(valueyesno);
//              }
//              else {
//                  $("#yesnovalue").val("1");
//              }
//          });

//      });

//      $(document).ready(function() {
//      $("#<%=rdo_options.ClientID%>").change(function() {
//      var tblObj = document.getElementById("<%=gvmonetary.ClientID%>");
//      alert(tblObj);

//      var txtObjlst = document.getElementById("<%=gvmonetary.ClientID%>").getElementsByTagName("INPUT");
//      alert(txtObjlst);
//      var txtCount;
//      txtCount = 0;
//      // alert(txtObjlst.length);
//      for (var i = 0; i < txtObjlst.length; i++) {
//          if (txtObjlst[i].type == "text") {
//              //Do your validations
//              //  alert(txtObjlst[i].value);
//              $(txtObjlst[i]).mask("9999-99");


//          }
//      }

//          });
//      });


//      function isNumber(evt) {
//          evt = (evt) ? evt : window.event;
//          var charCode = (evt.which) ? evt.which : evt.keyCode;
//          if (charCode > 31 && (charCode < 48 || charCode > 57) || (charCode!= 189)) {
//              alert('hi');
//            //  return false;
//          }
//          return true;
//      }


      function radiobutton_change() 
      {
  
  

      
    
  }
    </script>
    
    <script type ="text/javascript" language="javascript">
function hello() {
alert('Hi');
}
var list = [];
function Yearvalidation(txt, event) {
var charCode = (event.which) ? event.which : event.keyCode
list.push(charCode);
if (charCode != 8 || charCode != 9) {

}
for (var i = 0; i < list.length; i++) {
if (i == 4) {
if (charCode != 45) {
return false;
}
else {
return true;
}
}
else {
if ((charCode < 48 || charCode > 57)) {
return false;
}
else {
return true;
}

}

}


}



function onlyDotsAndNumbers(txt, event) {
var charCode = (event.which) ? event.which : event.keyCode
if (charCode == 46) {
if (txt.value.indexOf(".") < 0)
return true;
else
return false;
}

if (txt.value.indexOf(".") > 0) {
var txtlen = txt.value.length;
var dotpos = txt.value.indexOf(".");
if ((txtlen - dotpos) > 2)
return false;
}

if (charCode > 31 && (charCode < 48 || charCode > 57))
return false;

return true;
}

function masking(input, textbox, event) {
var charCode = (event.which) ? event.which : event.keyCode
if (input.length == 4) {
input = input + '-' + (parseInt(input.substring(2, 4)) + 1);
textbox.value = input;
}
if (charCode > 31 && (charCode < 48 || charCode > 57))
return false;
if (input.length > 6)
return false;

return true;
}


function valtxt(obj) {
str = "0123456789-"
l = obj.value.length;
for (i = 0; i <= l; i++) {
if (str.indexOf(obj.value.charAt(i)) == -1) {

alert("Invalid Year (ex. 12345-12).");
obj.value = "";
obj.focus();
return false;
}
else {
return true;
}

}

}


</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
<br />
    <asp:RadioButtonList ID="rdo_options" runat="server"
        RepeatDirection="Horizontal" RepeatLayout="Flow" AutoPostBack ="true" OnSelectedIndexChanged ="fchange" Font-Bold="True">
        <asp:ListItem Selected="True" Value="0">Company Wise</asp:ListItem>
        <asp:ListItem Value="1">Factory Wise</asp:ListItem>
    </asp:RadioButtonList> <br /> <br /> 
    <strong>"The turnover as per latest audited balance sheet. Enter the turnover in ascending order of the Financial Year(like : 2011-12 ).Turnover should be Only NUMERIC value ie 0-9 and dot(.)(Figures in lakhs) As per audited accounts."   
    </strong>
  <asp:Literal ID ="lit_message"  runat ="server"></asp:Literal>
<br />


   <strong> Decrease in Plant and Machinery for more than 10%</strong>
   <asp:RadioButtonList ID="rdo_plant_machinary" runat="server" 
            RepeatDirection="Horizontal" RepeatLayout="Flow"   Font-Bold="true" 
            AutoPostBack="True" onselectedindexchanged="rdo_plant_machinary_SelectedIndexChanged"
     >
       <asp:ListItem Selected="True" Value="1">YES</asp:ListItem>
       <asp:ListItem Value="2" >NO</asp:ListItem>
     </asp:RadioButtonList><%--<input type="text"   name="yesnovalue" id="yesnovalue" >--%>

<%--<input type ="button" onfocus--%>
<asp:Panel runat ="server"  ID ="pnl_content" ScrollBars ="Auto" Width ="700px">
 <asp:GridView ID="gv_display"  
         Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="0"
      runat="server"  
       
          
       >
      <HeaderStyle CssClass="dynRowHeader" />
        <RowStyle CssClass="dynRowEven" />
         <AlternatingRowStyle CssClass="dynRowOdd" />
         <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
 </asp:GridView>
 <br />
      <asp:GridView ID="gvmonetary"  
         Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="0"
      runat="server"  AutoGenerateColumns="False" 
         OnRowCreated ="gvmonetary_RowCreated"   
       >
      <HeaderStyle CssClass="dynRowHeader" />
        <RowStyle CssClass="dynRowEven" />
         <AlternatingRowStyle CssClass="dynRowOdd" />
         <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
 </asp:GridView>
  <asp:Literal runat ="server" ID ="lit_monetary" ></asp:Literal>

 </asp:Panel>
 
 <br /><br />
 
 <asp:Button runat ="server" ID ="btn_calculate" Text ="Calculate" 
        CssClass ="btnCls" onclick="btn_calculate_Click" />
        
  <br /><br />
  <asp:Button runat="server" ID="btnBack" Text="Submit" CssClass="btnCls" 
        onclick="btnBack_Click" Enabled="False" />
 
 <br /> <br /><br />
 <div>
<div style="text-align:justify; padding-left:10px; font-weight:bold;">
Monetary Limit calculation
</div>

 
<ul>
 <li><b>Monetary limit of the company is fixed on the basis of the unit's net sales turnover during the last three years reflected in the Audited Balance Sheets.</b>
    <ul type="disc" style="margin-bottom: 0in">
    <li style="color: #333333; text-align:justify; line-height: normal" >
    In case the Company/Partnership concern/Proprietorship unit is
    in loss for one year out of past three years, their monetary
    limit will be fixed at 40% of their average net sales turnover.<br />
    </li>
    <li style="color: #333333; text-align: justify; line-height: normal; padding-top:5px;">
    Similarly, when the Micro &amp; Small Enterprise is in loss for two
    years out of the past three years, the monetary limit will be
    accordingly fixed at 30% of their average net sales turnover of
    the past three years.</li>
    <li style="color: #333333; text-align: justify; line-height: normal; padding-top:5px;">
    In the event of Micro &amp; Small Enterprise being in loss
    throughout past three years, the monetary limit of the Unit will
    be fixed at 20% of the average net sales turnover of the Unit
    during the past three years.&nbsp; In cases of initial registration
    where the firm has been in existence for less than 3 years MTL
    may be fixed at 50% of the average net sales value of the last
    year for which audited accounts are available.</li>
    </ul>
 <li><b>
 Where the turnover of the Enterprise has steadily increased over the last three years and the unit is in profit continuously, the Monetary Limit may be fixed at 50% of net sales turnover achieved in the last year.
 </b>
 
 </ul>
</div>

 </div>
</asp:Content>

