<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Registrationfees.aspx.cs" Inherits="Registrationfees" %>
<%@ Register Src="UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title>Single Point Registration scheme (SPRS)</title>

    <script language="javascript" type="text/javascript" src="../JScript/jquery-1.7.1.js"></script>

    <link type="text/css" rel="Stylesheet" href="CSS/Style.css" />
     <script src="SlidShow/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="SlidShow/JScript.js" type="text/javascript"></script>
    
     <style type="text/css">
        #slideshow
        {
            position: relative;
            height: 223px;
            width: 975px;
        }
        #slideshow IMG
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 8;
            opacity: 0.0;
        }
        #slideshow IMG.active
        {
            z-index: 10;
            opacity: 1.0;
        }
        #slideshow IMG.last-active
        {
            z-index: 9;
        }
    </style>
    

</head>
<body>
    <center>
        <form id="form1" runat="server">
      
        <div>
            <table width="880" border="0" align="center"  cellpadding="0" cellspacing="0" style="background-color: #FFFFFF;">
              <tr><td>
                <img src="Images/header1.gif" />
                </td>
                </tr>
                 <tr>
                  <td style="vertical-align: top; width: 880px;" colspan="2">
                  <table class="bodytext">
                  <tr>
                  <td style="width:70%;" >
                  <p class="MsoNormal" style="line-height: normal" align="left" ><b>
              <span style="font-family:&quot;Verdana&quot;;color:#333333">
              <font size="2">REGISTRATION FEE</font></span></b></p>
              <p class="NoSpacing" style="text-align:justify">
              <span style="font-family:&quot;Verdana&quot;;color:#333333">
              <font size="2">The registration Fee is based on </font> </span><b>
              <span style="font-family:
&quot;Verdana&quot;"><font size="2">the Turnover</font></span></b><span style="font-family:&quot;Verdana&quot;"><font size="2"> <b>as per latest audited Balance Sheet 
              of the </b><span style="color:#333333">Micro &amp; Small Enterprise</span> 
              for the Registration, Renewal and any other amendment etc<span style="color:#333333">. 
              The Fee structure is indicated in the table as below:</span> &nbsp;</font></span></p>
              <table class="MsoNormalTable" border="1" cellspacing="0" cellpadding="0" style="border-collapse: collapse; border: medium none; margin-left: 4.4pt">
                <tr style="height: 22.7pt">
                  <td width="73" valign="top" style="width: 54.8pt; height: 22.7pt; border: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt">
                  <b>
                  <span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">S.No.</font></span></b></td>
                  <td width="265" colspan="4" valign="top" style="width: 198.4pt; height: 22.7pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="text-align: justify"><b>
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">Category (Turnover Based)</font></span></b></td>
                  <td width="247" valign="top" style="width: 185.45pt; height: 22.7pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: 1.0pt solid windowtext; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="text-align: justify"><b>
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">Proposed Fee</font></span></b></td>
                </tr>
                <tr style="height: 31.2pt">
                  <td width="73" valign="top" style="width: 54.8pt; height: 31.2pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">&nbsp;&nbsp; <b>I.</b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  </font> <b><font size="2">&nbsp;</font></b></span></td>
                  <td width="265" colspan="4" valign="top" style="width: 198.4pt; height: 31.2pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">Fee for Fresh Registration of Micro &amp; Small Enterprises:</font></span></p>
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">a)&nbsp; Turnover up-to Rs. 100 Lacs </font> </span></p>
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">b)&nbsp;Turnover exceeding &nbsp;Rs. 100 Lacs
                  </font> </span></td>
                  <td width="247" valign="top" style="width: 185.45pt; height: 31.2pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">a)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Rs. 5000/-
                  </font> </span></p>
                  <p class="MsoNormal" style="text-align: justify">
                  <span lang="EN-IN" style="line-height: 115%; font-family: Verdana">
                  <font size="2">b)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Rs.5,000/- plus Rs. 2000/- for every additional 
                  turnover of &nbsp;Rs. 100 Lac.</font></span></td>
                </tr>
                <tr style="height: 81.85pt">
                  <td width="73" valign="top" style="width: 54.8pt; height: 81.85pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">II.&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</font></span></td>
                  <td width="265" colspan="4" valign="top" style="width: 198.4pt; height: 81.85pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">a)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fee for issuance of amendments in stores (Qualitative 
                  &amp; Quantity); Fee for issuance of change in monetary limit or 
                  any other amendment in the certificate, and</font></span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">b)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Fee for issuance of Renewal of G.P. Registration 
                  (Every Two Years)</font></span></td>
                  <td width="247" valign="top" style="width: 185.45pt; height: 81.85pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">50% of the charges </font> </span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&nbsp;</font></span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">as mentioned at </font> </span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&nbsp;</font></span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Sr. No. I above</font></span></td>
                </tr>
                <tr style="height: 81.0pt">
                  <td width="585" colspan="6" valign="top" style="width: 438.65pt; height: 81.0pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">The Registration Fee as mentioned in the table above is <b><u>
                  exclusive</u></b> of the Inspection charges as levied by the 
                  inspecting agency.&nbsp; Such charges as decided by the Inspecting 
                  Agency are borne by the unit. Till such time the Inspecting 
                  Agency makes any amendments in their charges, the present 
                  Inspection Fee <b>by MSMEDI &amp; NSICs Technical Service Centre</b> 
                  shall be charged as reproduced below:-</font></span></td>
                </tr>
                <tr style="height: 13.5pt">
                  <td width="585" colspan="6" valign="top" style="width: 438.65pt; height: 13.5pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><b><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">A.</font></span></b><span style="line-height:115%;font-family:&quot;Verdana&quot;"><font size="2">&nbsp;&nbsp;
                  <b>Inspection Fees for MSME-DI / NTSCs</b> </font> </span></td>
                </tr>
                <tr style="height: 18.45pt">
                  <td width="131" colspan="2" valign="top" style="width: 98.5pt; height: 18.45pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><b><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">S.No.</font></span></b></td>
                  <td width="205" colspan="2" valign="top" style="width: 153.5pt; height: 18.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><b><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Investment in Plant 
                  and Machinery</font></span></b></td>
                  <td width="249" colspan="2" valign="top" style="width: 186.65pt; height: 18.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><b><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Inspection Fees for 
                  MSME-DI / NTSCs</font></span></b></td>
                </tr>
                <tr style="height: 24.45pt">
                  <td width="131" colspan="2" valign="top" style="width: 98.5pt; height: 24.45pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">1</font></span></td>
                  <td width="205" colspan="2" valign="top" style="width: 153.5pt; height: 24.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Up to Rs. 5 Lacs</font></span></td>
                  <td width="249" colspan="2" valign="top" style="width: 186.65pt; height: 24.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 1250/-</font></span></td>
                </tr>
                <tr style="height: 26.45pt">
                  <td width="131" colspan="2" valign="top" style="width: 98.5pt; height: 26.45pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">2</font></span></td>
                  <td width="205" colspan="2" valign="top" style="width: 153.5pt; height: 26.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&gt;Rs. 5 Lacs&nbsp; Up to 25 Lacs</font></span></td>
                  <td width="249" colspan="2" valign="top" style="width: 186.65pt; height: 26.45pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 2000/-</font></span></td>
                </tr>
                <tr style="height: 25.15pt">
                  <td width="131" colspan="2" valign="top" style="width: 98.5pt; height: 25.15pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">3</font></span></td>
                  <td width="205" colspan="2" valign="top" style="width: 153.5pt; height: 25.15pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&gt;Rs. 25 Lacs Up to 100 Lacs</font></span></td>
                  <td width="249" colspan="2" valign="top" style="width: 186.65pt; height: 25.15pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 3500/-</font></span></td>
                </tr>
                <tr style="height: 25.15pt">
                  <td width="131" colspan="2" valign="top" style="width: 98.5pt; height: 25.15pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">4</font></span></td>
                  <td width="205" colspan="2" valign="top" style="width: 153.5pt; height: 25.15pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&gt;Rs. 100 Lacs and upto Rs.500 Lacs</font></span></td>
                  <td width="249" colspan="2" valign="top" style="width: 186.65pt; height: 25.15pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 5000/-</font></span></p>
                  <p class="MsoNormal" style="margin-bottom:0in;margin-bottom:.0001pt;text-align:
  justify"><span style="line-height:115%;font-family:&quot;Verdana&quot;">
                  <font size="2">&nbsp;</font></span></td>
                </tr>
                <tr style="height: 60.75pt">
                  <td width="585" colspan="6" valign="top" style="width: 438.65pt; height: 60.75pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal" style="line-height: normal"><b>
                  <span style="font-family:&quot;Verdana&quot;">
                  <font size="2">B.</font></span></b><span style="font-family:&quot;Verdana&quot;"><font size="2">&nbsp;&nbsp; 
                  The Professional fee payable by MSEs to <b>M/s RITES Ltd., and 
                  M/s Consultancy Development Centre</b> for undertaking 
                  physical inspection will remain unchanged which are: </font> </span>
                  </td>
                </tr>
                <tr style="height: 29.3pt">
                  <td width="139" colspan="3" valign="top" style="width: 103.9pt; height: 29.3pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal"><font face="Verdana">By RITES</font></p>
                  <p class="ListParagraphCxSpFirst" style="margin-left: 30.0pt">
                  &nbsp;</p>
                  <p class="ListParagraphCxSpLast" style="margin-left: 30.0pt">&nbsp;</td>
                  <td width="446" colspan="3" valign="top" style="width: 334.75pt; height: 29.3pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="NoSpacing">
                  <span style="font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 6000/- incl. service tax and travel expenses for Micro 
                  Enterprises, and&nbsp;&nbsp; </font> </span></p>
                  <p class="NoSpacing">
                  <span style="font-family:&quot;Verdana&quot;">
                  <font size="2">Rs. 8000/- incl. service tax &nbsp;and travel expenses for Small 
                  Enterprises.</font></span></td>
                </tr>
                <tr style="height: 39.4pt">
                  <td width="139" colspan="3" valign="top" style="width: 103.9pt; height: 39.4pt; border-left: 1.0pt solid windowtext; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="MsoNormal"><font face="Verdana">By CDC</font></td>
                  <td width="446" colspan="3" valign="top" style="width: 334.75pt; height: 39.4pt; border-left: medium none; border-right: 1.0pt solid windowtext; border-top: medium none; border-bottom: 1.0pt solid windowtext; padding-left: 5.4pt; padding-right: 5.4pt; padding-top: 0in; padding-bottom: 0in">
                  <p class="NoSpacing" style="text-align:justify">
                  <span style="font-family:&quot;Verdana&quot;"><font size="2">Rs. 3000/- excl. service tax (incl. 
                  Travel expenses) for Micro Enterprises and</font></span></p>
                  <p class="NoSpacing" style="text-align:justify">
                  <span style="font-family:&quot;Verdana&quot;"><font size="2">Rs. 4495/- excl. service tax &nbsp;(incl. 
                  Travel expenses) for Small Enterprises.</font></span></td>
                </tr>
                <![if !supportMisalignedColumns]>
                <tr height="0">
                  <td width="73" style="border:none">&nbsp;</td>
                  <td width="58" style="border:none">&nbsp;</td>
                  <td width="7" style="border:none">&nbsp;</td>
                  <td width="197" style="border:none">&nbsp;</td>
                  <td width="2" style="border:none"></td>
                  <td width="247" style="border:none">&nbsp;</td>
                </tr>
                <![endif]>
              </table>

                  </p>
                  
                  </td>
                  <td style="width:30%; vertical-align :top;margin-left:50px;padding-left:50px;" align="left" >
                  <table>
                  <tr>
                  <td><font color="#1570a7" ><strong>Quick</font><font color="#dc0015"> Link</font></strong></td>
                  </tr>
                  <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsicspronline.com/index.htm" class="leftlink">Single Point Registration</a>
                  </td>
                  </tr>
                 <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="Registrationfees.aspx" class="leftlink">Registration Fees</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="RegistrationProcess.aspx" class="leftlink">Registration Process</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="RegistrationValidity.aspx" class="leftlink">Validity of GP Certificate</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/gp.asp" target ="_blank"  class="leftlink">Guidlines for filling Application Form</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/gp.asp" target="_blank"  class="leftlink">Download Application Form</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/contactus.asp" target="_blank"  class="leftlink">List of NSIC Branch Offices</a> 
                  </td>
                  </tr>
                  
                  <tr>
                  <td style="height:10px;"></td>
                  </tr>
                  <tr>
                  <td><font color="#1570a7" ><strong>Useful Links</font><font color="#dc0015"> for SPR Application</font></strong></td>
                  </tr>
                  <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="PointOfInterest.aspx" target="_blank" class="leftlink">Online Registration for GP</a>
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appCompany/CompanyStatusTrack.aspx" class="leftlink">Track status of your application</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appCompany/CompanyLogin.aspx" class="leftlink">Registered Company</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appAdministrator/Login.aspx" class="leftlink">NSIC Branch Office</a> 
                  </td>
                  </tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appInspectionAgency/Login.aspx" class="leftlink">Inspection Agency</a> 
                  </td>
                  </tr>
                  
                                    
                  </table>
                  </td>
                  </tr>
                  </table>
                  
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <uc2:Footer_Admin ID="Footer_Admin1" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        </form>
    </center>
    
</body>
</html>
