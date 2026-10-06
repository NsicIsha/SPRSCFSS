<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyMissingInformation.aspx.cs" Inherits="appAdministrator_CompanyMissingInformation" Title="Missing Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.5.1/jquery.min.js"></script>
    
    <style type="text/css">
        
    .leftHeading
    {
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:35px;
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
    }

body {
  margin: 0px;
  padding: 0px;
  background:#d6cbcf;
}

#header {
  background: #0f0;
  width: 800px;
  height: 100px;
}

.leftext
{
	
	font-family:Arial, Sans-Serif, Courier;
	font-size:12px;
	font-weight:bold;
	float:left;
	height:14px;
	width:40%;
	
}

.coloncls
{
	font-size:12px;
	font-weight:bold;
	float:left;
	height:14px;
	width:5%;
	
}

.clstxtbx
{
	background:#afacbc;
	font-family:Courier;
	font-size:12px;
	padding:1px;
	height:14px;
	float:left;
	width:100%;
}

.txtdiv
{
	height:14px;
	width:50%;
	float:left;
}

.clsrowdiv
{
	position:static;
	left:50px;
	height:20px;
	width:250px;
	padding-bottom:5px;
}

.tbldiv
{
	
	left:10px;
	top:100px;
	float:left;
	width:300px;
	height:500px;
}

#leftcol {
  background: #f00;
  float: left;
  width: 20%;
  height: 500px;
}
#rightcol {
  background: #f00;
  float: right;
  width: 20%;
  height: 500px;
}
#content {
  background: #fff;
  float: left;
  width: 59%;
  height: 500px;
}
#footer {
  background: #0f0;
  clear: both;
  width: 100%;
}

</style>

<script type="text/javascript" language="javascript">

       $(document).ready(function() {

           // var currentTablerow = $("<%=dyntblControl.ClientID %>").closest('tr');
           //  var inputs = currentTablerow.find("input");
           //  alert(inputs.filter(".name").val());      

           $("#ctl00_ContentPlaceHolder1_fGPtbl_CUnit_3DD").hide();
           $("#ctl00_ContentPlaceHolder1_fGPtbl_CUnit_CategoryUnitSizeIdD").hide();

           $("#ctl00_ContentPlaceHolder1_fGPtbl_CUnit_CategoryCastIdD").hide();

           $("#ctl00_ContentPlaceHolder1_fGPtbl_CUnit_CategoryAreaIdD").hide();


           $("#ctl00_ContentPlaceHolder1_fGPtbl_Company_HeadOffice_CityIdD").hide();

           $("#ctl00_ContentPlaceHolder1_fGPtbl_Company_HeadOffice_StateIdD").hide();

           $("#ctl00_ContentPlaceHolder1_fGPtbl_Company_Directors_CityIdD").hide();

           $("#ctl00_ContentPlaceHolder1_fGPtbl_Company_Directors_StateIdD").hide();

           var rowcount = $("#ctl00_ContentPlaceHolder1_dyntblControl tr").length;
           if (rowcount == 0) {
               $("#ctl00_ContentPlaceHolder1_btnclick").hide();
              // $("#ctl00_UpdatePanel1_PlzWait").hide();
              $("#trname").hide();
           }
           else {

               $("#ctl00_ContentPlaceHolder1_btnclick").show();
               $("#ctl00_UpdatePanel1_PlzWait").show();
           }

       });

       function validate_textbox() {


           if ($(':text:visible').filter('[value=""]').length > 0) {
               alert(" Entering data for all the visbile textboxes is mandatory");
               return false;
           }
           else {
               return true;
           }
       }
   
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Company_id" runat="server" Text="" Visible="false"></asp:Label>
   
       <div align="center"> <asp:Label ID="lblMessage" runat="server" CssClass="errMessage" Text="" ></asp:Label></div>
        
        <table cellspacing="0" cellpadding="0" width="100%"  border="0">
        
            <tr id="trname" align="left" valign="top">
                <th align="left" valign="top" style="padding-left:100px;">Name of field</th>
                <th align="center" valign="top"> Values</th>
            </tr>
            <tr>
                <td colspan="2" align="right"><asp:Label ID="lblPANmsg" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr align="left" valign="top">
                <td align="left" valign="top" style="width:65%;">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </td>
                <td align="left" valign="top" style="width:20%;">
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr align="left" valign="top" >
              <td valign="top" align="left" style="height:20px;">
                
              </td>
            </tr>
            <tr align="left" valign="top" >
              <td valign="top" align="left" colspan="2">
                <asp:Table ID="dyntblControl" runat="server" border="0" CellPadding="2" CellSpacing="2" Width="100%">
                </asp:Table>
              </td>
            </tr>
            <tr align="left" valign="top">
                <td align="left" valign="top">
                </td>
                <td align="left" valign="top">
                    <asp:Button ID="btnclick" runat="server" Text="Submit" 
                        onclick="btnclick_Click" CssClass="btnCls" OnClientClick="javascript:return validate_textbox();" />
                </td>
            </tr>
            
        </table>
</asp:Content>

