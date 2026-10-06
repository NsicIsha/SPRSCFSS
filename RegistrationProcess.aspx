<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationProcess.aspx.cs" Inherits="RegistrationProcess" %>
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
                  <p class="MsoNormal" style="line-height: normal"><b>
              <span style="font-family:&quot;Verdana&quot;;color:#333333">
              <font size="2">REGISTRATION PROCESS</font></span></b></p>
              <ol start="1" type="1" style="margin-bottom: 0in">
                <li class="MsoNormal" style="color: #333333; text-align: justify; line-height: normal">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">The Micro &amp; Small Enterprise has to submit the 
                application form (in duplicate) along-with requisite fee and 
                documents to the Zonal/Branch and Sub Office/Extension office of 
                NSIC situated nearest to their location.</font></span></li>
                <li class="MsoNormal" style="color: #333333; text-align: justify; line-height: normal">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">Duplicate copy of the G.P. Registration Application Form 
                submitted by the Micro &amp; Small Enterprise will be forwarded to 
                the concerned MSME-DI RITES / CDC &nbsp;along with copies of required 
                documents and requisite Draft/Pay Order of inspection charges</font></span><span style="font-family:&quot;Verdana&quot;;color:windowtext"><font size="2"> 
                in favor of concerned Inspection Agency</font></span><span style="font-family:&quot;Verdana&quot;"><font size="2"> requesting for carrying out the 
                Technical Inspection of Micro &amp; Small Enterprise and forward 
                their recommendations in this regard.</font></span></li>
                <li class="MsoNormal" style="color: #333333; text-align: justify; line-height: normal">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">After receiving MSME-DI Inspection Report, NSIC will issue the 
                GP Registration Certificate to Micro &amp; Small Enterprise for 
                items/stores as recommended.<br>
                &nbsp;<br>
                Procedure for calculation &amp; fixation of Monetary Limits of Micro 
                &amp; Small Enterprises.</font></span></li>
              </ol>
              <p class="MsoNormal" style="line-height: normal" align="left" >
              <span style="font-family:&quot;Verdana&quot;;
color:#333333"><font size="2"><b>A.&nbsp; Monetary limit of the company is fixed on the basis of 
              the unit's&nbsp;net sales turnover during the last three years 
              reflected in the Audited Balance Sheets.</b><br>
              &nbsp;<br>
              Sample calculation:<br>
              Net Sales = Total sales - All Duties, Taxes, returns etc<br>
              Details of the net sales of the company during last three years:-<br>
              2007-08 - Rs. 50 Lacs<br>
              2008-09 - Rs. 75 Lacs<br>
              2009-10 - Rs. 80 Lacs<br>
              Therefore, Monetary Limit = Rs. (50+75+80)/3 X 50%<br>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; = Rs. 34.26 Lac.<br>
              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Say = Rs. 34 Lac.</font></span></p>
              <ul type="disc" style="margin-bottom: 0in" >
                <li class="MsoNormal" style="color: #333333; line-height: normal;text-align:left;">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">In case the Company/Partnership concern/Proprietorship unit is 
                in loss for one year out of past three years, their monetary 
                limit will be fixed at 40% of their average net sales turnover.
                </font>
                </span></li>
                <li class="MsoNormal" style="color: #333333; text-align: justify; line-height: normal;text-align:left;">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">Similarly, when the Micro &amp; Small Enterprise is in loss for two 
                years out of the past three years, the monetary limit will be 
                accordingly fixed at 30% of their average net sales turnover of 
                the past three years.</font></span></li>
                <li class="MsoNormal" style="color: #333333; text-align: justify; line-height: normal;text-align:left;">
                <span style="font-family:&quot;Verdana&quot;">
                <font size="2">In the event of Micro &amp; Small Enterprise being in loss 
                throughout past three years, the monetary limit of the Unit will 
                be fixed at 20% of the average net sales turnover of the Unit 
                during the past three years&nbsp;&nbsp;&nbsp; In cases of initial registration 
                where the firm has been in existence for less than 3 years MTL 
                may be fixed at 50% of the average net sales value of the last 
                year for which audited accounts are available.</font></span></li>
              </ul>
              <p class="MsoNormal" style="text-align: justify; line-height: normal">
              <span style="font-family:&quot;Verdana&quot;;color:#333333; font-weight:700">
              <font size="2">B.&nbsp; Where the turnover of 
              the Enterprise has steadily increased over the last three years 
              and the unit is in profit continuously, the Monetary Limit may be 
              fixed at 50% of net sales turnover achieved in the last year.</font></span></p>
              <p class="MsoNormal" style="text-align: justify; line-height: normal; margin-left: .5in">
              <b>
              <span style="font-family:&quot;Verdana&quot;;color:#333333">
              <font size="2">For Example</font></span></b><span style="font-family:&quot;Verdana&quot;;
color:#333333"><font size="2"> : If during the last three years 2007-08 , 2008-09 and 2009-10 
              the turnover had grown steadily and was highest in 2009-10, in 
              this scenario the Monetary Limit should be fixed by taking the 
              basis of highest net sales / turnover achieved in the year 
              2009-10. The other terms and conditions will remain unchanged.</font></span></p>

                  </td>
                  <td style="width:30%; vertical-align :top;margin-left:50px;padding-left:50px;" align="left" >
                  <table width="100%" cellpadding="0px" cellspacing="0px" class="quicklink">
                  <tr>
                  <td ><span class="quicklink"><font color="#1570a7" >Quick</font><font color="#dc0015"> Links</font></span></td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                  <tr>
                  <td ><img src="Images/icon-arrow-new.gif" />  <a href="Home.aspx" class="leftlink">Single Point Registration</a>
                  </td>
                  </tr>
				   <tr><td height="4px"></td></tr>
				  				  <tr>
                  <td ><img src="Images/icon-arrow-new.gif" />  <a href="MsmeDBVisit.aspx" class="leftlink">Visit MSME DataBank Portal</a><img src="http://nsic.co.in/new_in.gif"/>
                  </td>
                  </tr>

                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/gp.asp" class="leftlink" target="_blank">Registration Fees</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="RegistrationProcess.aspx" class="leftlink">Registration Process</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="./document/Annexures/ProcessFlowChart.jpg" target="_blank" class="leftlink">Process Flow Chart</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="RegistrationValidity.aspx" class="leftlink">Validity of GP Certificate</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="Checklists_of_documents.aspx" class="leftlink">Checklists of documents</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/gp.asp" target ="_blank"  class="leftlink">Guidelines for filling Application Form</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/gp.asp" target="_blank"  class="leftlink">Download Application Form</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="Annexures.aspx" target="_blank"  class="leftlink">Download Annexures/ Formats</a> 
                  </td>
                  </tr>                  
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsic.co.in/pdfs/FAQ-SPRS102016.pdf" target="_blank"  class="leftlink">FAQs</a> 
                  </td>
                  </tr>
                  
                  <tr><td height="4px"></td></tr>
                  
                  
                  <tr>
                  <td style="height:10px;"></td>
                  </tr>
                  <tr>
                  <td><font color="#1570a7" ><strong>Useful Links</font><font color="#dc0015"> for SPR Application</font></strong></td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                  <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="PointOfInterest.aspx" target="_blank" class="leftlink">Online Registration for GP</a>
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appCompany/CompanyStatusTrack.aspx" class="leftlink">Track status of your application</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appCompany/CompanyLogin.aspx" class="leftlink">Registered Company</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appAdministrator/Login.aspx" class="leftlink">NSIC Offices</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appInspectionAgency/Login.aspx" class="leftlink">Inspection Agency</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                  <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appBuyingAgency/BuyingAgencyLogin.aspx" class="leftlink">Buying Agency</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appBuyingAgency/BuyingAgencyLogin.aspx" class="leftlink">DGS&D Login</a> 
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="appBuyingAgency/BuyingAgencyLogin.aspx" class="leftlink">Ministry of MSME/ DC Office</a> 
                  </td>
                  </tr>                  
                  <tr><td height="10px"></td></tr>
                   <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="spr_check_registration.aspx" class="leftlink" target="_blank">Verification of SPRS Certificates</a> <br /><br />
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                   <tr>
                  <tr>
                    <td>
                        <table border="1" style="border-color:Red; border-width:thick; border-collapse:collapse; height:50px;" >
                            <tr>
                              <td style="padding-left:5px;">  <asp:Label ID="lblCount" runat="server"  class="leftlink" ForeColor="Red" Font-Bold="true" Font-Size="14px"/> 
                              </td>
                              </tr>
                        </table>
                    </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
                  <tr>
                    <td class="bodytext" style="font-weight:normal; font-family:Verdana;">
                        (Authenticity of the certificate can be checked through this web portal. 
                        For detailed information, respective branch offices may be contacted.)
                    </td></tr>
                                    
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
