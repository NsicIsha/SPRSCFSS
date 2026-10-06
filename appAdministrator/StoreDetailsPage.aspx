<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreDetailsPage.aspx.cs" Inherits="appAdministrator_StoreDetailsPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Store Details</title>

<%--    <script src="../JScript/JSAdmin.js" type="text/javascript"></script>--%>

    <script src="../JScript/jquery-1.7.1.js" type="text/javascript"></script>

  
      <style type="text/css">
                 .leftHeading_certificate1_ref_branch
          {
          	  font-family:Arial, Helvetica, sans-serif;
	font-size: 14px;

	padding-left:2px;
	
	font-weight:bold;
          }
       
          .leftHeading_certificate1_ref
          {
          	  font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	
	padding-left:2px;
	
	font-weight:bold;
          }
       .leftHeading_certificate1
{
	font-family:Arial, Helvetica, sans-serif;
	font-size: 12px;
	height:20px;
	padding-left:5px;
	padding-right:5px;
	font-weight:bold;
}
         .lblclass
      {
      	 font-size:14px;
      	  font-family:Arial;
      	  font-weight:bold;
      } 
           .btnCls_CERTIFICATE
{
	font-family: Arial, Helvetica, sans-serif;
	font-size: 10px;
	background-color:#22518F;
	color:#FFFFFF;
	font-weight:bold;
	width:90px;
	display:none;
}
            table.gridviewNewStore
                 {
                 page-break-after:right;	 
                 }
    
             
             .heightimportantclass
               {
               	  
           /* border-style:solid;*/
                   border:1px solid black;
            
               }
                  @Page:first
             {
             	 
             	 margin-top:3.2cm;
             }
                @Page:left
                 {
                  
             	  margin-top:6.5cm;
             	  margin-bottom:4.5cm;  
             	             	  
             	  
             }
             
         
             
                 @Page:right
                 {
                  
             	  margin-top:6.5cm;
             	  margin-bottom:4.5cm;  
             	             	  
             	  
             }
             
            
             
            
      </style>
      <script type ="text/javascript" language="javascript">

        
      
      
      
          function externalClick() {

              //printWindow.open(pagename);



              document.getElementById('<%=btnPrnt.ClientID%>').style.display = "none";
              $('.gridviewstore').removeAttr('style');
              $('.gridviewstore').removeAttr('rules');
            //  $('.gridviewstore tr').addClass('heightimportantclass');
              $('.gridviewstore tr').addClass("heightimportantclass");
          //  $('.gridviewstore').css('BORDER-COLLAPSE', 'none;');
             
//              var $totalRowsHeight = 0;
//              var sn0_height = $('.gridviewstore tr td').eq(0).height();
//              var Store_name_Height = $('.gridviewstore tr td').eq(1).height();
//              var Specification_Height = $('.gridviewstore tr td').eq(2).height();
//              var QualityCapacity_height = $('.gridviewstore tr td').eq(3).height();
//              var PMPS_Height = $('.gridviewstore tr td').eq(4).height();
//              var $allRows = $('.gridviewstore tr');
//              var gridrowcount = $('.gridviewstore tr').length;
//              for (var i = 1; i < gridrowcount; i++) {
//                  $totalRowsHeight += $('.gridviewstore tr:eq(' + i + ')').height();
//                  $('.gridviewstore tr td:eq(' + i + ')').height($('.gridviewstore tr:eq(' + i + ')').height());
//              }
             
              
          
              window.print();
              window.close();
             
          }
      
      </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0" border="0">
       <tr>
  
 
  <td  style="width:15px;"></td>
      <td   colspan="2" align="center" valign="bottom" style="white-space:nowrap; text-decoration:line-through;">
      
      <asp:Label ID="lbl_validform_store_details" runat="server"  Text=""></asp:Label> <span style="text-align:right; padding-right:10px; padding-left:65px;"><b>
 S.No.</b> </span><asp:Label ID="lblserial_Store" runat="server" Text=""></asp:Label>
      
      
      
           <table cellpadding="0" cellspacing="0" border="0"  width="765px"  >  <%--id="tbl_branch_tbl"--%>
         <%--<tr><td colspan="2">&nbsp;</td></tr>--%>
         
   <tr><td colspan="3"  ></td></tr> <%-- id="td_height"--%>

           <tr align="left">
     <td  id="td_branch" align="left" valign="top" colspan="3" style="text-decoration:line-through;" class="leftHeading_certificate1_ref_branch">BRANCH OFFICE  <%-- id="td_branch"--%>
          <asp:Label ID="lbl_Branch_Name" runat="server" Text="" CssClass="leftHeading_certificate1_ref_branch"></asp:Label> 
     </td>

  </tr>
          <tr >
          <td colspan="3" valign="top" align="left" style="text-decoration:line-through;" ><b> <asp:Label ID="lbl_branchaddress" runat="server" 
Text="" CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
        </tr>
        
        
        <tr >
         <td align="left"   style="width:275px; text-decoration:line-through;" class="leftHeading_certificate1_ref" ><b>Ph : <asp:Label 
ID="lbl_phoneAddress" runat="server" Text="" 
CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
          <td align="left"   style="width:350px;text-decoration:line-through;" class="leftHeading_certificate1_ref"><b>Fax : <asp:Label 
ID="lbl_FaxAddress" runat="server" Text="" 
CssClass="leftHeading_certificate1_ref"></asp:Label></b></td>
    <td style="width:130px;"></td>
        </tr>
        
        
        
         <tr >
           <td align="left"    style="width:275px;text-decoration:line-through;" class="leftHeading_certificate1_ref" ><b>Email : <asp:Label 
ID="lbl_EmailAddress" runat="server" Text="" 
CssClass="leftHeading_certificate1"></asp:Label></b> </td>
           <td align="left"   style="width:350px;text-decoration:line-through;"  class="leftHeading_certificate1_ref" ><b>Website : <asp:Label 
ID="lbl_website" runat="server" Text="www.nsic.co.in" ></asp:Label></b> </td>
<td style="width:130px;"></td>
         </tr>
         </table>
         
         
         
         
         
         
         
      </td>
    
  
  </tr>

<tr >
<td colspan="2" style="height:50px;">&nbsp;

</td>
</tr>

<tr> <%-- margin-left:4px;--%>

 <td style="width:15px;" ></td>
<td align="left"  style="text-decoration:line-through;"> ANNEXURE TO GOVERNMENT PURCHASES ENLISTMENT CERTIFICATE </td>


</tr>
<tr>
 <td style="width:15px;"></td>
<td align="left" style="text-decoration:line-through;" > NO.&nbsp; <asp:Label ID="lblregistration_number" runat="server" Text= '' 
CssClass="lblclass"></asp:Label>&nbsp; D.T.&nbsp;<asp:Label ID="lbldatestore" runat="server" 
Text='' ></asp:Label> </td> <%--CssClass="Lblclass"--%>
</tr>
<tr><td colspan="2" style="height:1px;"></td></tr>
<tr>
 <td style="width:15px; text-decoration:line-through;"></td>
<td align="left" style=" text-decoration:line-through;"> ISSUED TO&nbsp; <asp:Label ID="lblCompanyNameStore" runat="server" Text='' 
CssClass="lblclass"></asp:Label> <asp:Label ID="lblstanameStore" runat="server" Text='' 
></asp:Label></td> <%--CssClass="Lblclass"--%>
</tr>
    
       
    
    </table>
        <table cellspacing="2" cellpadding="0" border="0"   style="text-decoration:line-through;">
                     <tr>
                       <td align="left" valign="top" style="text-decoration:line-through;" >
                       <asp:PlaceHolder ID="plc_test" runat="server"  >
                         
                       
                       </asp:PlaceHolder>
                       
                       </td>
                     
                     </tr>
                     
                     
                     
    
    
    </table>
      <table cellpadding="0" cellspacing="0" border="0"   width="750px" id="tbl_print_deatils"  style="">
           <tr><td  height="50px">&nbsp;</td></tr>    <%-- 50px--%>
    <tr>
      <%--<td align="left"  style="width:425px;">&nbsp;</td>--%>
        <td align="right" width="750px" style="  padding-right:5px; text-decoration:line-through;" class="Serial_No_StoreChrome"><br /><br />   Authorised Signatory</td>
    </tr>
    
      <tr>
  
  
  
    <td align="center" width="750px">
      <div  id="div_print_button" style="  vertical-align:top;" > 
   <%-- <input type="button" value="print"  

OnClientClick="javascript:print_showreportdiv()('Show_Report_Div');" 

onclick="javascript:CallPrint('Show_Report_Div');" />--%>
  
   <asp:Button runat="server" ID="btnPrnt"   Text="Print"    
 
              OnClientClick="javascript:externalClick();" />   <%--CssClass="btnCls_CERTIFICATE" --%>
   <asp:Label ID="lbl_serail_add_no" runat="server" Text="" Visible="false"></asp:Label>
   <asp:HiddenField ID="hdn_company_id" runat="server" />
      <%--<input type="button" value="print" onclick="PrintDiv();" />--%><%--  CssClass="btnCls_CERTIFICATE" --%>
 </div>
    
    </td>
    
  </tr>
    
    </table>
    
    </div>
    </form>
</body>
</html>
