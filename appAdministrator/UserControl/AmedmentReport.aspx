<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AmedmentReport.aspx.cs" Inherits="appAdministrator_AmedmentReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Amedment Report  Page</title>
      <link type="text/css" rel="Stylesheet" href="../CSS/StyleAdmin.css" />
      
           <style  type="text/css">
         .lblRpt
           {
           	 white-space:normal;
           	  word-spacing :20px;
           	
           }
           .tblrowcolor
             {
              background-color:#0055FF;
              
             }
              .lblclass
             {
      	 font-size:14px;
      	  font-family:Arial;
      	  font-weight:bold;
             }
        .lblclassrpt
          {
          	 font-size:14px;
      	  font-family:Arial;
      	  font-weight:bold;
      	  vertical-align:middle;
          }
          
           .RptSNO
             {
            font-size:4px;
      	  font-family:Arial;
      	  font-weight:bold;
      	  vertical-align:middle; 
      	   width:1px; 
             }
             
            .partnername
              {
              	  font-family: Arial,Helvetica,sans-serif;
                       font-size: 11px;
              }
                @media print
        {                
            tHead
            {    
                display : table-header-group;    
                border:solid 1px #000000;
            }
            
            .Logo 
            {
             font-family:Verdana;
             font-size:16px;
             font-weight:bold;
             color:#000000;
            }
            
           
        
        </style>
      
       <script type="text/javascript" language="javascript">
       
          function printing()
        {
            window.print();
         
        }
        function CallPrint(strid) 
        {
            document.getElementById('MainDiv').style.display = "block";
            document.getElementById('PrintDiv_Button').style.display = "none";

            var prtContent = document.getElementById(strid);

            var WinPrint = window.open('', '', 'left=0,top=0,width=700px,height=800px,toolbar=0,scrollbars=0,status=0, directories=no,menubar=no, copyhistory=no ,location=no');
            document.getElementById('div_print_button').style.display = "none";
            WinPrint.document.write(prtContent.innerHTML);
             WinPrint.document.close();
            WinPrint.focus();
         
             WinPrint.print();
               WinPrint.close();
              document.getElementById('MainDiv').style.display = "none";
            document.getElementById('PrintDiv_Button').style.display = "none";
            document.getElementById('div_print_button').style.display = "none";
            }

function showHideDiv()
     {
//     document.getElementById('Print_div').style.display="none";
//       document.getElementById('Print_Stores_Details').style.display="block";
//    document.getElementById('div_print_button').style.display="block";


 document.getElementById('MainDiv').style.display="none";
       document.getElementById('Print_Stores_Details').style.display="block";
    document.getElementById('div_print_button').style.display="block";
      
     }

       </script>
</head>
<body>
    <form id="form1" runat="server" style="width:700px;">
           <div id="Show_Report_Div"  align="center" style=" width:700px;"  >
    <div id="MainDiv" align="center" style=" height:1100px; width:700px; margin-left:3px; margin-right: 0px;">
           
             <div id="Print_div">
             
         <div id="HederText" style="width:100%; height:250px">
<table cellpadding="0" cellspacing="0" border="0" width="80%" >
<tr><td align="right" colspan="3"> <b>SL.No.</b> <asp:Label ID="lblserialNo" runat="server" Text=""></asp:Label></td>
<td></td>

</tr>

</table>

</div>
<div id="Referances" style="width:700px;">
<table cellpadding ="0" cellspacing="0" border="0">
<tr>

<td colspan="4" align="center" class="leftHeading"><asp:Label ID="lblvalidToFrom" runat="server" Text="" ></asp:Label></td>
</tr>


<tr style="margin-top:10px ;">
<td class="leftHeading" align="left" colspan="2" style="white-space:nowrap;"><b>Ref.No</b>&nbsp;<asp:Label ID="lblrefnumber" runat="server" Text=""></asp:Label>
</td>

<td class="leftHeading" align="right" style="padding-right:20px;"><asp:Label ID="lblDate" Text="Date:" runat="server"></asp:Label><asp:Label ID="lbldateform" runat="server" Text=""></asp:Label> </td>
<td class="leftHeading" align="left">
<p class="leftHeading">&nbsp;</p>


</td>
</tr>
<tr><td colspan="4" style="height:10px;"></td></tr>
<tr style="margin-top:10px;">
<td colspan="2" align="left" valign="top">
<table cellpadding="0" cellspacing="0" border="0" style="width:200px;">
<tr>
<td align="left" valign="top" style="white-space:nowrap;"><b><asp:Label ID="lblNameCom" runat="server" Text=''></asp:Label></b></td>

</tr>
<tr>
<td valign="top" style="white-space:nowrap;" ><asp:Label ID="LblCompanyAdress" runat="server" Text='' ></asp:Label></td>
</tr>
<tr>
<td valign="top" style="white-space:nowrap;"> <asp:Label ID="lblStateName" runat="server" Text=''></asp:Label>

<asp:Label ID="lblcompanypin" runat="server" Text=''></asp:Label> </td>
</tr>

</table>


</td>
<td colspan="2" align="left" valign="top" style="height:200px;">
<table cellpadding="0" cellspacing="0" border="0" style="width:200px; height:100px;">
<tr><td class="leftHeading" align="left"><b>Factory Address:</b></td></tr>
<tr>
<td style="vertical-align:top;" valign="top" align="left">
<asp:Repeater ID="Rpt_Address" runat="server" >
<HeaderTemplate>

</HeaderTemplate>
<ItemTemplate>
<table cellpadding="0" cellspacing="0" border="0" style="height:40px;">



<tr>

<td align="left" colspan="2" style="white-space:nowrap;"> <asp:Label ID="Label2" runat="server" Text='<%#Eval("SNo")%>' ></asp:Label><asp:Label ID="lblAddress" runat="server" Text='<%#Eval("AddressName")%>'></asp:Label></td>

</tr>
<tr>
<td align="left"><asp:Label ID="lblState" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label> </td>
<td align="left"><asp:Label ID="lbl_pin" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Zip")%>'></asp:Label></td>

</tr>


</table>

</ItemTemplate>
<SeparatorTemplate>

<table cellpadding="0" cellspacing="0" border="0">

<tr>
<td colspan="3" ></td>

</tr>

</table>

</SeparatorTemplate>



</asp:Repeater>

</td>

</tr>
</table>
<%-- <asp:Label ID="lblFactoryAddress" runat="server" Text='' Visible="false"></asp:Label>--%>

</td>



</tr>
<tr><td colspan="4" style="height:10px;"></td></tr>
<tr style="margin-top:10px;">
<td colspan="2" align="left" ><b>Name of the Directors</b>
<br />
<asp:HiddenField ID="hdnlablName" runat="server" />
<br />

<asp:Label ID="lblpartnaerName" runat="server" Text='' CssClass="partnername"></asp:Label>
</td>
<td colspan="2" class="leftHeading" align="left"><p> Certificate of Registration under Single

Point Resigstration Scheme. Units <br />registered

under this scheme are considered to
be at par with those <br />

registered directly with DGS & D.</p> </td> </tr>
<tr> <td colspan="4" style="height:10px"></td></tr>
<tr style="margin-top:10px;">
<td align="left" colspan="4" ><b>Constitution of the firm-</b>
<asp:Label ID="lblconstitutionCode" runat="server" Text="" CssClass="partnername"></asp:Label></td>

</tr>
<tr> <td colspan="4" style="height:10px"></td></tr>
<tr style="margin-top:10px;">
<td colspan="4" style="white-space:nowrap;" align="center"><b>GOVERNMENT<asp:GridView ID="grd_print11" runat="server" AutoGenerateColumns="false"
EnableViewState="False" Width="100%" UseAccessibleHeader="true" Visible="false"
BorderWidth="2px" CellPadding="3" CellSpacing="1"
 >

<HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

<RowStyle CssClass="dynRowEven" />
<AlternatingRowStyle CssClass="dynRowOdd" />
<HeaderStyle CssClass="dynRowHeader" />


<Columns>

<asp:TemplateField HeaderText="S.NO">
<ItemTemplate>
<%#Container.DataItemIndex+1 %>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="StoresName" HeaderText="Name of the Store" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" HeaderStyle-Width="200px" />
<asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" HeaderStyle-Width="250px" />
<asp:BoundField DataField="QualitativeCapacity" HeaderText="Qualitative Capacity" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />

<asp:BoundField DataField="QuantitativeCapcity" HeaderText="QUANTITATIVE CAPACITY PER MONTH/SHIFT" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" /> <%--FooterText="SENIOR BRANCH MANAGER"--%>




</Columns>





</asp:GridView>


&nbsp;PURCHASE REEGISTRATION NO:</b>&nbsp;<asp:Label ID="lblRegisNo" runat="server" Text=""></asp:Label></td>


</tr>
<tr> <td colspan="4" style="height:10px"></td></tr>
<tr style="margin-top:10px;">

<td colspan="4" style="white-space:nowrap;" align="center">
<b>Monetry Limit:</b>
Rs<asp:Label ID="lblMonLimit" runat="server" Text=""></asp:Label>
Rs.
<asp:Label ID="lblTextRupee" runat="server" Text=""></asp:Label>
</td>
</tr>
<%--<tr> <td colspan="4" style="height:10px"></td></tr>--%>
<tr style="margin-top:3px;">
<td colspan="4">&nbsp;&nbsp;&nbsp;</td>
</tr>

<tr style="margin-top:10px;">
<td colspan="4" align="center" >
<asp:Label ID="lbltrunover" Text="TURNOVER (Rupees in Lakhs)" CssClass="leftHeading" runat="server"></asp:Label>


 <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server" 
        Width="100%"  CellPadding="10" CellSpacing="1" 
        onrowdatabound="grd_TurnOver_RowDataBound" >
                                <HeaderStyle CssClass="GrdviewHeader" Height="50" />
                                <RowStyle CssClass="GrdviewRowOdd" />
                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                <Columns>
                                   
                                    
                                </Columns>
                             </asp:GridView>


</td>

</tr>
<tr> <td colspan="4" style="height:10px"></td></tr>

<tr >
<td colspan="4" align="left">
<p>

Your name has been registered as a SSI Unit eligible for participation in the <b> Central Government Store Purchase Programme</b> as <br />
the <b>Single Point Registration Scheme</b> for the following Items/Stores.

</p>

</td>
</tr>
<tr> <td colspan="4" style="height:10px"></td></tr>
<tr>
<td colspan="4" align="left">
</td>


</tr>
<tr style="margin-top:10px;">
<td colspan="4" align="left">
<table cellpadding="0" cellspacing="0" border="1" style="width:700px;"align="left">
<tr>
<td class="leftHeading" align="center">Name of the stores </td>
<td class="leftHeading" align="center">Specification</td>
<td class="leftHeading" align="center">Qualitative Capcity</td>
<td class="leftHeading" align="center">Quantitatative Capacity P.M</td>

</tr>
</table>

</td>


</tr>
<tr> <td colspan="4" style="height:10px"></td></tr>
<tr>
<td colspan="4" align="center" class="leftHeading">"As per List Attached"</td>

</tr>
<tr>
<td colspan="4" align="center" class="leftHeading">
<asp:Label ID="lblGridCount" runat="server" Text=''></asp:Label>

</td>


</tr>
<tr><td colspan="4" style="height:7px;"></td></tr>
<tr valign="top">

<td colspan="4" align="right" class="leftHeading"> Autorised Signatory</td>


</tr>



</table>


</div>
<div id="PrintDiv_Button" align="center" >
<table cellpadding="0" cellspacing="0" border="0">
<tr>
<td>
    &nbsp;</td>
<td>
<%-- <asp:Button ID="btn_next" runat="server" Text="Next" onclick="btn_next_Click" />--%>
<input type="button" value="Next Page" onclick="Javascript:showHideDiv();" />
</td>
</tr>

</table>

</div>

</div>

     

    

    
    
    </div>
    <br />
    <br />
    <br />
    <br />
    <br />
    
    
       <div id="Print_Stores_Details" style="margin-left:15px; display:None;" >
<div id="DivHeaderText2" style="width:700px; height:250px">
<table cellpadding="0" cellspacing="0" border="0" width="80%" >
<tr><td align="right" colspan="3"> <b>SL.No.</b> <asp:Label ID="lblserial_Store" runat="server" Text=""></asp:Label></td>
<td></td>

</tr>

</table>

</div>

<div id="Main_Heads" style="width:700px; font-size:15px; font-family:Arial; font-weight:bold; margin-left:104px;" align="left">
THE NATIONAL SMALL INDUSTRIES CORPORATION LIMITED
<br style="vertical-align:middle"/>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
BRANCH OFFICE DELHI


</div>


<div id="Div_print" style="width:700px;  font-size:15px; font-family:Arial; page-break-after:avoid; font-weight:bold; margin-top:15px;" align="left" >

<table cellpadding="0" cellspacing="0" border="0" id="tablshow" runat="server">

<tr>
<td align="left" style="margin-left:4px;" > ANNEXURE TO GOVERNMENT PURCHASES ENLISTMENT CERTIFICATE </td>


</tr>
<tr>
<td align="left" style="margin-left:4px;" > NO.&nbsp; <asp:Label ID="lblregistration_number" runat="server" Text= '' CssClass="Lblclass"></asp:Label>&nbsp; D.T.&nbsp;<asp:Label ID="lbldatestore" runat="server" Text='' CssClass="Lblclass"></asp:Label> </td>
</tr>
<tr><td style="height:4px;"></td></tr>
<tr>
<td align="left" style="margin-left:4px;"> ISSUED TO&nbsp; <asp:Label ID="lblCompanyNameStore" runat="server" Text='' CssClass="Lblclass"></asp:Label> <asp:Label ID="lblstanameStore" runat="server" Text='' CssClass="Lblclass"></asp:Label></td>
</tr>
<tr><td style="height:4px;"></td></tr>

</table>




<table cellpadding="0" cellspacing="0" border="0" style="width:700px;  font-family:Arial; font-weight:bold; margin-top:10px">

  <tr style="width:700px;">

       
    <td style="margin-top:10px; width:100%;"><asp:placeholder id="PlaceHolder1" runat="server" ></asp:placeholder></td>
 
  </tr>
    





</table>

<br />
<br />
<table cellpadding="0" cellspacing="0" width="700px">
<tr style="margin-top:50px;">
<td align="right" class="leftHeading">SENIOR BRANCH MANAGER </td>

</tr>

</table>


</div>




</div>   
             <div  id="div_print_button" align="center"  style="display:none; vertical-align:top;" > 
<%--  <input type="button" value="print"  onclick="javascript:CallPrint('Show_Report_Div');" style="z-index: 105; position: relative;top: 55px; left:245px;width: 50px;" /> --%>
   <asp:Button ID="Button"  runat="server"  Text="Print"
                     OnClientClick="javascript:CallPrint('Show_Report_Div');" 
                     style="z-index: 105; position: relative;top: 55px; left:245px;width: 50px;" 
                     onclick="Button_Click" />
    
     
              <%--OnClientClick="javascript:window.print();" --%>
 <%--   <asp:Button ID="btnPrnt" runat="server" Text="Print"  
onclick="btnPrnt_Click" />--%>

      <%--<input type="button" value="print" onclick="PrintDiv();" />--%>
     </div>
       
 </div>
    </form>
</body>
</html>
