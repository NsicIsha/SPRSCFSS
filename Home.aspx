<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControl/Header_Admin.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<%@ Register Src="UserControl/TreeMenu_Admin.ascx" TagName="TreeMenu_Admin" TagPrefix="uc3" %>

<%@ Register Src="UserControl/HeaderImage.ascx" TagName="HeaderImage" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="description" content="nsic certificate,government purchase,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />

 

<meta name="keywords" content="nsic certificate,gp,government purchase,spr,SPRS Certificate,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />


     <title>Single Point Registration Scheme (SPRS)</title>

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
                   <%--<img src="Images/NSICLOGO_NEW.png" />--%>
                  
                </td>
                </tr>
                 <tr>
                  <td style="vertical-align: top; width: 880px;" colspan="2">
                  <table class="bodytext">
                  <tr>
                  <td style="width:70%;" >
                  <p align="left"  style="color: #333333; font-family:Verdana; text-align: justify; line-height: normal"><strong>Single Point Registration Scheme (SPRS)</strong><br /><br />The Government is the single largest buyer of a variety of goods. With a view to increase the share of purchases from the small-scale sector, the Government Stores Purchase Programme was launched in 1955-56. NSIC registers Micro & small Enterprises (MSEs) under Single Point Registration scheme (SPRS) for participation in Government Purchases.</p>
                  <p align="left" style="text-align:justify;">
                  <strong>Benefits of Registration</strong>
                  <p align="left" style="text-align:justify; font-family:Verdana; ">The units registered under Single Point Registration Scheme of NSIC are eligible to get the benefits under “Public Procurement Policy for Micro & Small Enterprises (MSEs) Order 2012” as notified by the Government of India, Ministry of Micro, Small & Medium Enterprises, New Delhi vide Gazette Notification dated 26.03.2012</p>
                  <ul style="text-align:left ; font-family:Verdana;">
                 <li>Issue of tender sets free of cost,</li>
                 <li>Exemption from payment of Earnest Money Deposit (EMD),</li>
                 <li>In tender participating MSEs quoting price within price band of L1+15 per cent shall also be allowed to supply a portion upto 20% of requirement by bringing down their price to L1 Price where L1 is non MSEs.</li>
                  </ul>
                  </p>
                  <p align="left" style="text-align:justify; font-family:Verdana;"><strong>Eligibility</strong><br /><br />
                   <p align="left" style="text-align:justify; font-family:Verdana;"> 
                  All industries which are registered with the Director of Industries (DI)/District Industries Centre (DIC) as manufacturing/service enterprises or having Acknowledgement of Entrepreneurs Memorandum (EM Part-II) are eligible for registration with NSIC under its Single Point Registration Scheme (SPRS).
                  </p>  
                  <p align="left" style="text-align:justify; font-family:Verdana;"> 
                  The SPRS Certificate is granted to the Micro & Small Enterprise under Single Point Registration Scheme (Revised), 2003 is valid for Two Years and will be reviewed and renewed after every two years by verifying continuous Commercial and Technical Competence of the registered Micro & Small Enterprise in manufacturing / producing the stores for which it has been registered by NSIC
                  </p>
                 <p align="left" style="text-align:justify; font-family:Verdana;"> 
                <u> Provisional Registration Certificate</u> with Monetary Limit of Rs.5.00 lakhs is issued to new micro and small enterprises subject to the followings: 
                 <br />
                      <li align="left" style="text-align:justify; font-family:Verdana;">
                          For the Micro & Small Enterprises who have already commenced their commercial production but not completed one year of existence. The Provisional Registration Certificate can be issued to such Micro & Small Enterprises under Single Point Registration scheme which shall be valid for the period of one year only from the date of issue after levying the registration fee and obtaining the requisite documents.
                    <br /><br /> <br />Micro & Small Enterprises can either apply online or contact the nearest NSIC office for registration.
                 </li>
                 </p>
                  </p>
                       <p align="left" style="text-align:justify; font-family:Verdana;">
                       The buying agencies & other institutions can verify the certificates through this website. 
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
                  <td ><img src="Images/icon-arrow-new.gif" />  <a href="MsmeDBVisit.aspx" class="leftlink">Visit MSME DataBank Portal</a><%--<img src="Images/new.png" height="30" width="40"/>--%>
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

                      <tr>
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="http://www.nsicspronline.com/feedback.aspx" target="_blank"  class="leftlink">
                    <span style="color:red">Feedback</span>  


                                                               </a> 
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
                  <td>
                      <%--<img src="Images/icon-arrow-new.gif" />  <a href="appAdministrator/Unitregistration.aspx" class="leftlink">Online Registration for GP</a>--%>

                      <img src="Images/icon-arrow-new.gif" />  <a href="PointOfInterest.aspx" target="_blank" class="leftlink">Online Registration for GP</a>
                      
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
                  <td><img src="Images/icon-arrow-new.gif" />  <a href="Final_Verify.aspx" class="leftlink" target="_blank">Verification of SPRS Certificates</a> <br /><br />
                  </td>
                  </tr>
                  <tr><td height="4px"></td></tr>
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
