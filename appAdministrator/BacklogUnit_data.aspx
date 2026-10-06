<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="BacklogUnit_data.aspx.cs" Inherits="BacklogUnit_data" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.2.js"></script>
 <style type="text/css">
                
        .ErrMsgLbl
        {
        	font-weight:bold;
        	font-style:italic;
        	text-decoration:underline;
        	color:Red;
        	}
        
    </style>
    <script type="text/javascript">
function Showalert() {
    alert('Please fill atleast one entry for monetary & turnover');
}


function checkNumeric(event) {
var kCode = event.keyCode || event.charCode; // for cross browser check
if (!(kCode==8 )&&(kCode < 48 || kCode > 57))
{
return false;
}
}
function checkDECIMAL(event) {
var kCode = event.keyCode || event.charCode; // for cross browser check
if (!(kCode==8 || kCode==46 )&&(kCode < 48 || kCode > 57))
{
return false;
}
}


var specialKeys = new Array();
        specialKeys.push(8); //Backspace
        function IsNumeric(e) {
 alert('hi');
//            var keyCode = e.which ? e.which : e.keyCode
//            var ret = ((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1);
//         //  document.getElementById("error").style.display = ret ? "none" : "inline";
//            return false;
        }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


        <table border ="0" cellpadding ="0" cellspacing ="0" width ="100%">
        <tr> 
          <td colspan ="4" class ="rowHeading">  Payment Details  </td>
        </tr>
        
       <tr> <td colspan ="4"></td> </tr>
         <tr>
        <td align ="left" class="leftHeading">
        <b>Basic Fee *</b>
        </td>
        <td align="left" >
        <%--<asp:Label ID="Label2" runat="server"  ></asp:Label>--%>
        <asp:TextBox ID="txt_basicfee" runat="server" ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rf_basicpayment" runat="server" ErrorMessage="Please enter basic fee"
                                                    ControlToValidate="txt_basicfee" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_basic" runat="server" TargetControlID="rf_basicpayment">
                                                </asp:ValidatorCalloutExtender>
        </td>
        <td align ="left" class="leftHeading">
         <b> Service Tax * </b>
        </td>
        <td align ="left">
           <asp:TextBox ID="txt_servicetax" runat="server" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="req_servicetax" runat="server" ErrorMessage="Please enter service tax"
                                                    ControlToValidate="txt_servicetax" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_servicetax" runat="server" TargetControlID="req_servicetax">
                                                </asp:ValidatorCalloutExtender>
        </td>
        </tr>
         <tr>
        <td align ="left" class="leftHeading">
            <b> Total * </b></td>
        <td align="left" >
            <asp:TextBox runat ="server" ID ="txt_total"></asp:TextBox> </td>
                <asp:RequiredFieldValidator ID="req_total" runat="server" ErrorMessage="Please enter toatl amount"
                                                    ControlToValidate="txt_total" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_tot" runat="server" TargetControlID="req_total">
                                                </asp:ValidatorCalloutExtender>
        <td align ="left">
            &nbsp;</td>
        <td align ="left">
            &nbsp;</td>
        </tr>
        <tr>
        <td align="right" colspan ="4" >
            &nbsp;</td>
        </tr>
        
         <tr> 
          <td colspan ="4" class ="rowHeading">  Registration Details  </td>
        </tr>
        
         <tr>
        <td align ="left" class="leftHeading">
            <b> Registration Date * </b></td>
        <td align="left" >
            <asp:TextBox runat ="server" ID ="txt_registrationdate"></asp:TextBox> </td>
                <asp:RequiredFieldValidator ID="req_registariondate" runat="server" ErrorMessage="Please enter registration date"
                                                    ControlToValidate="txt_registrationdate" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                <asp:ValidatorCalloutExtender ID="vc_registration" runat="server" TargetControlID="req_registariondate">
                                                </asp:ValidatorCalloutExtender>
                                                
                              
 <asp:CalendarExtender ID="CExtendertxt_3a" runat="server" TargetControlID="txt_registrationdate">
                            </asp:CalendarExtender>                  
                                                
        <td align ="left" class="leftHeading">
          Reference No</td>
        <td align ="left">
           <asp:TextBox runat ="server" ID ="txt_referenceno"></asp:TextBox> </td>
        </tr>
        
        
         <tr>
        <td align ="left" class="leftHeading">
           Registration No </td>
        <td align="left" >
            <asp:TextBox runat ="server" ID ="txt_registrationno"></asp:TextBox> </td>
            <asp:RequiredFieldValidator ID="req_registrationno" runat="server" ErrorMessage="Please enter registration number"
                                                    ControlToValidate="txt_registrationno" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                                       <asp:ValidatorCalloutExtender ID="vc_registrationo" runat="server" TargetControlID="req_registrationno">
                                                </asp:ValidatorCalloutExtender>
        <td align ="left" class="leftHeading">
            &nbsp;</td>
        <td align ="left">
            &nbsp;</td>
        </tr>
        
        
         <tr>
        <td align ="center" class="leftHeading" colspan="4">
            <asp:Label ID="lblMessage" runat="server" CssClass="MasterAction" 
                Font-Bold="True" Font-Size="Larger"></asp:Label></td>
        </tr>
        
          <tr> 
          <td colspan ="4" class ="rowHeading">  Turnover /Monetary  Limit Details  </td>
        </tr>
          <tr>
        <td align="right" colspan ="4" >
            <span id="error" style="color: Red; display: none">* Input digits (0 - 9)</span></td>
        </tr>
        
        <tr>
          <td colspan ="4">
            <asp:GridView ID="gvmonetary"  
         Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="5"
      runat="server"  AutoGenerateColumns="False" 
       >
      <HeaderStyle CssClass="dynRowHeader" />
        <RowStyle CssClass="dynRowEven" />
         <AlternatingRowStyle CssClass="dynRowOdd" />
         <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
 </asp:GridView>
          </td>
        </tr>
        
        <tr>
          <td align ="center" colspan ="4" >
           <asp:Button runat ="server" ID ="btn_find" Text ="Submit" onclick="btn_find_Click" 
         ValidationGroup ="1" CssClass="btnCls"  />
          </td>
        </tr>
        
        <tr>
          <td align ="center" colspan ="4" >
              &nbsp;</td>
        </tr>
        
        </table>
     

 
 
       

</asp:Content>

