<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationValidity.aspx.cs" Inherits="RegistrationValidity" %>
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
                  <td style="width:70%;" valign ="top" >
                  <p align="left"  style="color: #333333; font-family:Verdana; text-align: justify; line-height: normal"><strong>Validity Period  of G. P. Registration</strong><br /><br />The G. P. Registration Certificate granted to the Micro & Small Enterprise under Single Point Registration Scheme (Revised), 2003 is valid for Two Years and will be reviewed and renewed after every two years by verifying continuous Commercial and Technical Competence of the registered Micro & Small Enterprise in manufacturing / producing the stores for which it has been registered by NSIC.
                     </p>
                  </p>
                  
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
                  </tr>                  <tr><td height="4px"></td></tr>
                   <tr>
                  <tr>
                    <td>
                        <table border="1" style="border-color:Red; border-width:thick; border-collapse:collapse; height:50px;" >
                            <tr>
                              <td style="padding-left:5px;"> <asp:Label ID="lblCount" runat="server"  class="leftlink" ForeColor="Red" Font-Bold="true" Font-Size="14px"/> 
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
