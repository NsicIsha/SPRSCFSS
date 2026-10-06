<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CoverLetterofInspectionAgency.aspx.cs" Inherits="appAdministrator_CoverLetterofInspectionAgency" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    <script type="text/javascript" language="javascript">

        function PrintPage() {
            var sOption = "toolbar=yes,location=no,directories=yes,menubar=yes,scrollbars=yes,width=900,height=300,left=100,top=25";
            var sWinHTML = document.getElementById('divPrint').innerHTML;
            var winprint = window.open("", "", sOption);
            winprint.document.open();
            winprint.document.write('<html><head><style type="text/css">');
            winprint.document.write('body,td,th{font-family:Arial, Helvetica, sans-serif;font-size:14px;color:#000000}');
            winprint.document.write('.style1 {font-size:11px; font-weight:bold; color:#000000; }');
            winprint.document.write('.style2 {font-size:11px; font-weight:bold; color:#FFFFFF; }');
            winprint.document.write('</style></head><body onload="window.print();">');
            winprint.document.write(sWinHTML);
            winprint.document.write('</body></html>');
            winprint.document.close();
           // winprint.focus();
        }
    
    </script>
    <style type="text/css">
     .textClass
       {
       	  font-family:Arial;
       	  font-size:14pt;
       	  
       }
       .textclassinternal
         {
         	 font-family:Arial;
         	 font-size:12pt;
         }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divPrint">
    <table cellpadding="2" cellspacing="2" border="0" width="100%">
    <thead>
       <tr>
       <td align="center" colspan="2" class="textClass">
         THE  NATIONAL SMALL INDUSTRIES CORPORATION LIMITED 
              <br />
              (A GOVERNMENT OF INDIA ENTERPRISE)
               <br />
                BRANCH OFFICE, &nbsp; <asp:Label ID="lbl_branch_office_name" runat="server" Text=""></asp:Label> 
                <br />
                <span style="width:400px;">   <asp:Label ID="lbl_branch_address" runat="server" Text=""></asp:Label></span>
       </td>  </tr>
      
        
      
    
    </thead>
    
    <tbody>
         <tr>
            <td colspan="2" style="margin-top:12pt;">
            <table cellpadding="2"  cellspacing="2" border="0" width="100%">
            <tr>
              <td align="left" valign="top" class="textClass" style="width:490px;">
               Ref No :<asp:Label  ID="lbl_ref_no" runat="server" Text=""></asp:Label>
              
              
              </td>
              
               <td align="right" class="textClass" valign="top">
                 Dated:
               </td>
               <td align="left" class="textClass" valign="top">
                 <asp:Label ID="lbl_date_time" runat="server" Text="07/05/2014"></asp:Label>
               </td>
            </tr>
            </table>
            </td>
            </tr>
            <tr><td colspan="2" style="height:20pt;"></td></tr>
            <tr>
            
              <td  class="textClass" align="left" style="width:20%;">
               The Director
                 <br />
                  <asp:Label ID="lbl_inspection_agengy_address" runat="server" Text=""></asp:Label>
                 
              </td>
              <td style="width:80%;"></td>
            
            </tr>
            <tr><td colspan="2" style="height:20pt;"></td></tr>
            <tr>
            <td colspan="2"  align="left" class="textClass" style="padding-left:40pt;">
               Sub :  Registration of M/s. <asp:Label ID="lbl_Company_name" runat="server" Text=""></asp:Label> 
            </td>
            </tr>
            <tr><td colspan="2" style="height:20pt;"></td></tr>
            
            <tr>
               <td colspan="2" class="textClass" align="left">
                   Dear Sir, </td>
            </tr>
            <tr>
              <td colspan="2" style="height:10pt;"></td>
            </tr>
            
            <tr>
              <td  align="left" colspan="2" style="padding-left:10pt;" class="textclassinternal" >
                 &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;  Captioned Unit has submitted its application for Fresh Registration/Renewal of
               Registration/Addition of Stores/Amedment under single Point Registration Scheme.
               We are enclosing herewith one set of application, alongwith all relevant documents and 
               Demand Draft/Pay Order No. <asp:Label ID="lbl_pay_order_no" runat="server" Text="12345"> </asp:Label>   dated  <asp:Label ID="lbl_paydate" runat="server" Text="02/20/2012"></asp:Label>   drawn On
               <asp:Label ID="lbl_drawn_on" runat="server" Text="Delhi"></asp:Label>  for Rs., <asp:Label ID="lbl_ruppees" runat="server" Text="22"></asp:Label>
               &nbsp; with a  request to Kindly arrange inspection of the Unit and forward the report to this office at the
               earliest, to enable us to take further action in the matter.
               <br />
               
               <br />
               
               &nbsp;&nbsp;&nbsp;&nbsp; Thanking you,
              
              </td>
            
            </tr>
               <tr>
              <td colspan="2" style="height:50pt;"></td>
            </tr>
            
             <tr>
               <td colspan="2" align="right" class="textClass">
                     Yours faithfully,
                     <br />
                     <br />
                     
                   
                     <asp:Label ID="lbl_Manager_name" runat="server" Text="(Mrs)V.Choudhary"></asp:Label>
                     <br />
                     <asp:Label ID="lbl_desgignation" runat="server" Text=""></asp:Label> 
                     
               
               </td>
             
             </tr>

         
        <%-- </tr>--%>
    
    </tbody>
    
    <tfoot>
      <tr>
        <td colspan="2" style="height:10pt"></td>
      
      </tr>
    <tr>
      <td style="width:100%" colspan="2"><h5><hr /></h5></td>
    
    </tr>
    
    <tr>
      <td colspan="2"  style="width:100%" class="textClass" align="center">
        <table cellpadding="2" cellspacing="2" border="0">
          <tr>
            <td align="left" valign="top">
            Phone:<asp:Label ID="lbl_phone_text" runat="server" Text="26382568,26382569"></asp:Label>
            &nbsp; Fax<asp:Label ID="lbl_fax_text" runat="server" Text="26382427"></asp:Label>
            </td>
            <td align="left" valign="top">
              Telegram :SMALCORP
            
            </td>
          </tr>
          <tr>
            <td align="left" valign="top" class="textclassinternal">
             Website:http://www.nsic.co.in &nbsp;
            </td>
            <td align="left" valign="top" class="textclassinternal">
              Email: <asp:Label ID="lbl_email_text" runat="server" Text="delhinsic@nsic.co.in"></asp:Label>
            </td>
          </tr>
        </table>
      </td>
    </tr>
    </tfoot>
    
    </table>
     </div>
    
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
          <td align="center" >
            <input type="button" value="Print"  onclick="javascript:PrintPage();"/>
          </td>
        </tr>
    
    </table>
    
    
    
   
    </form>
</body>
</html>
