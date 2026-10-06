<%@ page language="C#" AutoEventWireup="true" CodeFile="HomeNew.aspx.cs" Inherits="HomeNew" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>SINGLE POINT REGISTRATION - SPRS</title>
    <link rel="SHORTCUT ICON" href="Images/ns.ico" />

    <!-- Bootstrap core CSS -->
    <link href="CSS/bootstrap.min.css" rel="stylesheet">

    <!-- Animation CSS -->
    <link href="CSS/animate.css" rel="stylesheet">
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="CSS/style.css" rel="stylesheet">
    <style>
        @media screen and (max-width: 600px) {
            div.example {
                display: none;
            }

            .btn {
                background-color: #1ab394;
                border-color: #1ab394;
                color: #FFFFFF;
                font-size: 10px;
                padding: 5px 5px;
                font-weight: 100;
            }
        }
    </style>
</head>
<body id="page-top" class="landing-page no-skin-config">
<%--<div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-hidden="true" style="display: block;">
        <div class="modal-dialog modal-lg">
            <div class="modal-content" >
                <div class="modal-body">
                    <div>                       
                        <span>
                             <button type="button" class="close" data-dismiss="modal">
                           <span aria-hidden="true">×</span></button>
                            
                        <img src="Images/AKAM.jpeg" id="imagepreview" class="img img-responsive">
						
							</img>
							</span>
                    </div>
                </div>
            </div>
        </div>
    </div>--%>
    <div class="navbar-wrapper">
        <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container">
                <div class="navbar-header page-scroll">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#navbar" aria-expanded="false" aria-controls="navbar">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand">
                        <asp:Label runat="server" ID="lblUnits" /></a>
                </div>
                <div id="navbar" class="navbar-collapse collapse">
                    <ul class="nav navbar-nav navbar-right">
                        <li><a class="page-scroll" href="#page-top">Home</a></li>
                        <li><a class="page-scroll" href="#eligibility">Eligibility</a></li>
                        <li><a class="page-scroll" href="appAdministrator/Tender.aspx">Tenders</a></li>
                        <li><a class="page-scroll" href="#Documents">Documents</a></li>
                        <%--  <li><a href="FeedbackForm.aspx">FeedBack</a></li>--%>
                        <li>
                            <a data-toggle="dropdown" class="dropdown-toggle" href="#" aria-expanded="true">
                                <!--<span>-->
                                <span class="block m-t-xs"></span><span class="text-xs block">SignIn <b class="caret"></b></span>
                                <!--</span>-->
                            </a>

                            <ul class="dropdown-menu animated fadeInRight m-t-xs">
                                <li><a style="color: dimgrey" href="appCompany/CompanyLogin.aspx">REGISTERED COMPANY</a></li>
                                <li><a style="color: dimgrey" href="appBuyingAgency/BuyingAgencyLoginNew.aspx">BUYING AGENCY</a></li>
                                <li><a style="color: dimgrey" href="appBuyingAgency/BuyingAgencyLogin.aspx">MINISTRY OF MSME/DC OFFICE</a></li>
                                <li class="divider"></li>
                                <li><a style="color: dimgrey" href="appAdministrator/Login_New_Encrypted.aspx">NSIC OFFICES</a></li>
                                <li><a style="color: dimgrey" href="appInspectionAgency/Login.aspx">INSPECTION AGENCY</a></li>

                            </ul>
                        </li>

                    </ul>
                </div>
            </div>
        </nav>
    </div>

    <div class="row">
        <div class="container">
            <div class="carousel-caption blank" style="margin-left: 15px">
                <div class="example">
                    <h2>We believe in MSEs<br />
                        and promote them
                        <br />
                        for better TOMORROW...</h2>
                </div>
                <p>
				<!--<a class="btn btn-primary" href="PointOfInterest.aspx" role="button">Apply Online</a>-->
                    <a class="btn btn-primary" href="appAdministrator/NewUnitRegistrationIntro.aspx" target="_blank" role="button">Apply Online</a>
					
					<a class="btn btn-primary" href="appAdministrator/RenewalOnlineIntro.aspx" target="_blank" role="button">Renewal Online</a>
					
					
                    <a class="btn btn-primary" href="Final_Verify.aspx" role="button">Verify Registration</a>
                    <!--<a class="btn btn-lg btn-primary" href="Document/Annexures/QuotationformatCTMS.pdf" target="_blank" role="button">Download Quotation Format</a>-->
                </p>
            </div>

        </div>
    </div>
	
	
	  
		
	
	
	
    <img src="Images/NEW-SPRSbackground.jpg" height="40%" width="100%" />
    <section id="Covid" class="container">
        <div class="row">
            <div class="col-lg-9">
				<div class="ibox">
				
				 <div class="ibox-header text-center" style='color:red'>
				 <!--
				 <marquee width="100%" direction="left" >
				 <b>
				 
Dear All,

Down Time for Single Point Registration Software Application is scheduled on 31.08.2021 from 10:00 am to 4:30 pm. 
All NSIC offices are requested to please align their SPRS certificate enlistment work accordingly. </b>
</marquee>


						 

-->
					</div>
					
					
				
                    <div class="ibox-header text-center">
						<h2><b>Benefits of Single Point Registration Scheme</b></h1>
					</div>
					<div class="ibox-content">
						<p class="text-center text-black">The units registered under Single Point Registration Scheme of NSIC are eligible to get the benefits under Public Procurement Policy for Micro & Small Enterprises (MSEs) Order 2012 as notified by the Government of India, Ministry of Micro Small & Medium Enterprises, New Delhi vide Gazette Notification dated 23.03.2012 and amendment vide order no. S.O. 5670(E) dated 9th November 2018.</p>

                <div class="col-lg-12">
                    <div class="col-sm-3">
                        <h3>Free of Cost Tender Info</h3>
                        <p style="text-align: justify">Issue of the Tender Sets free of cost.In addition to the above, 358 items are also reserved for exclusive purchase from SSI Sector </p>
                        <!--<p><a class="navy-link" href="#" role="button">Details &raquo;</a></p>-->
                    </div>
                    <div class="col-sm-3">
                        <h3>Exemption from EMD</h3>
                        <p style="text-align: justify">Exemption from payment of Earnest Money Deposit (EMD)</p>
                        <!--<p><a class="navy-link" href="#" role="button">Details &raquo;</a></p>-->
                    </div>
                    <div class="col-sm-3">
                        <h3>Advantage In Tender Participation</h3>
                        <p style="text-align: justify">
                            In tender participating MSEs quoting price within price band of L1+15 per cent shall also be allowed to supply a portion upto 25% of requirement by bringing down their price to L1 Price where L1 is non MSEs.
                        </p>
                        <!--<p><a class="navy-link" href="#" role="button">Details &raquo;</a></p>-->
                    </div>
                    <div class="col-sm-3">
                        <h3>Procurement from MSEs</h3>
                        <p style="text-align: justify">
                            Every Central Ministries/Departments/PSUs shall set an annual goal of minimum 25 per cent of the total annual purchases of the products or services produced or rendered by MSEs. Out of annual requirement of 25% procurement from MSEs, 4% is earmarked for units owned by Schedule Caste /Schedule Tribes and 3% is earmarked for the units owned by Women entrepreneurs.
                        </p>
                        <!--<p><a class="navy-link" href="#" role="button">Details &raquo;</a></p>-->
                    </div>
                </div>
            </div>
</div>
      </div>
      <div class="col-lg-3">
<div class="tabs-container">
                        <ul class="nav nav-tabs">
                            <li class=""><a data-toggle="tab" href="#tab-1"> Details</a></li>
                            <li class="active"><a data-toggle="tab" href="#tab-2">Notifications</a></li>
							 <li class=""><a data-toggle="tab" href="#tab-3">Circulars</a></li>
                        </ul>
                        <div class="tab-content">
                            <div id="tab-1" class="tab-pane ">
                                <div class="panel-body">
                                    <div class="ibox">
                    <div class="ibox-content">
                        <div class="carousel" id="carousel1">
                            <div class="carousel-inner">
                                <div class="item active" style="height: auto;">
                                    <a href="https://www.msmemart.com/member/login" target="_blank">
                                        <img alt="image" class="img-responsive" src="Images/B2B.png">
                                    </a>
                                </div>
                                <!--<div class="item " style="height: auto;">
                                    <a href="http://msmemart.com/" target="_blank">
                                        <img alt="image" class="img-responsive" src="Images/adverti.jpg">
                                    </a>
                                </div>-->
                            </div>
                            <a data-slide="prev" href="#carousel1" class="left carousel-control">
                                <span class="icon-prev"></span>
                            </a>
                            <a data-slide="next" href="#carousel1" class="right carousel-control">
                                <span class="icon-next"></span>
                            </a>
                        </div>
                    </div>
                </div>
                                </div>
                            </div>
                            <div id="tab-2" class="tab-pane active">
                                <div class="panel-body">
                                     <div class="ibox">
                    <div class="ibox-header text-danger text-center">
                        <h3><b>Notifications</b></h3>
                    </div>
                    <div class="ibox-content">
                        <table class="table table-bordered small">
                            <thead class="text-info">
                                <tr>
                                    <th class="text-center">Details</th>
                                </tr>
                            </thead>
                            <tbody>
							 <tr>
                                    <td><span class="label label-warning">New</span><a href="http://msme.gov.in/acts-and-rules/circular-orders" target="_blank">Link for circulars of Ministry of Micro,Small & Medium Enterprises</a></td>
                                </tr>
							 <!--<tr>
                                    <td><span class="label label-warning">New</span>Exclusive discount of 20% to women entrepreneurs is available on SPRS for Fresh Cases. (From 01-04-2022 to 31-03-2023) (Fee for Technical Inspections of MSEs will remain the same).</td>
                                </tr>-->
                                 <tr>
                                    <td><span class="label label-warning">New</span>Nominal fee on SPRS registration for Scheduled Caste and Scheduled Tribe at Rs.100/- + GST. (From 2021-22 to 2025-26)</td>
                                </tr>
                                 <tr>
                                    <td><span class="label label-warning">New</span>MSEs located in North Eastern Region are entitled for 20% concession in fee (Fee for Technical Inspections of MSEs will remain the same)</td>
                                </tr>
                                <tr>
                                    <td><span class="label label-warning">New</span>&nbsp;MSMEs can submit their Grievances pertaining to COVID-19 at covid19cell[@]nsic[dot]co[dot]in</td>
                                </tr>
                                <tr>
                                    <td><span class="label label-warning">New</span>&nbsp;COVID-19 Standard Operating Procedure (SOP) for MSMEs at workplace: <br/><a class="btn btn-warning btn-xs " href="Document/pdfs/SOP_English_20042020.pdf" target="_blank">View (In English)</a>&nbsp;<a class="btn btn-warning btn-xs " href="Document/pdfs/SOP_Hindi_20042020.pdf" target="_blank">View (In Hindi)</a></td>
                                </tr>
                                <tr>
                                    <td><span class="label label-warning">New</span>&nbsp;Message from Director (P & M) for MSMEs: <a class="btn btn-warning btn-xs " href="Document/pdfs/Message_of_Director_PM_ for_MSMEs200422.pdf" target="_blank">View Message</a> </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
                            </div>
                        </div>
<div id="tab-3" class="tab-pane">
                            <div class="panel-body">
                                <div class="ibox">
                                    <div class="ibox-header text-danger text-center">
                                        <h3><b>Office Circulars</b></h3>
                                    </div>
                                    <div class="ibox-content">
                                        <table class="table table-bordered small">
                                            <thead class="text-info">
                                                <tr>
                                                    <th class="text-center">Details</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td><span class="label label-warning">New</span><a href="Document/pdfs/Definition_MSME.pdf" target="_blank">Definition of Micro & Small Enterprises engaged in manufacturing & service activities.</a></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
               
            
                
            </div>
        </div>
    </section>

    <section class="gray-section team" id="eligibility" style="margin-top: 0px;">
        <div class="row">
            <div class="col-lg-12 text-center">
                <div class="navy-line"></div>
                <h1>Eligibility Criteria For SPRS Registration</h1>
                <br />
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-md-3 text-center wow fadeInLeft">
                <div>
                    <i class="fa fa-user-circle-o features-icon "></i>
                    <h2>Udyam Registration.</h2>
                    <p>All Micro & Small Enterprises having Udyam Registration are eligible for registration with NSIC under its Single Point Registration Scheme (SPRS).</p>
                </div>

            </div>
            <div class="col-md-6 text-center">
                <iframe src="https://www.youtube.com/embed/UZYhnG7nMCI" width="100%" height="300" frameborder="0" allowfullscreen=""></iframe>
            </div>
            <div class="col-md-3 text-center wow fadeInRight">
                <div>
                    <i class="fa fa-user-circle-o features-icon"></i>
                    <h2>For StartUps</h2>
                    <p>Micro & Small Enterprises who have already commenced their commercial production but not completed one year of existence. The Provisional Registration Certificate can be issued to such Micro & Small Enterprises under Single Point Registration scheme with monitory limit of Rs. 5.00 Lacs which shall be valid for the period of one year only from the date of issue after levying the registration fee and obtaining the requisite documents.</p>
                </div>

            </div>
        </div>
    </section>
    <section id="Registration" class="container services">
        <div class="row">
            <div class="col-lg-12 ">
                <div class="navy-line"></div>
                <div class="text-center">
                    <h1>How to Apply</h1>
                </div>
                <p style="text-align: justify">
                    <ol>
                        <%--<li>Micro & Small Enterprises may fill up 'Point of Interest' form available at our website www.nsicspronline.com or on the prescribed application form (in duplicate) either to be downloaded from website or avail from NSIC offices along-with requisite fee and documents to the Zonal/Branch/Sub Branch and Sub Office/Extension office of NSIC situated nearest to their location.</li>
                        <li>Duplicate copy of the G.P. Registration Application Form submitted by the Micro & Small Enterprise will be forwarded to the concerned Inspecting agency along with copies of required documents and requisite Proof of inspection charges remitted in favor of concerned Inspection Agency requesting for carrying out the Technical Inspection of Micro & Small Enterprise and forward their recommendations in this regard.</li>
                        <li>After receiving Inspection Report, NSIC will issue the SPRS Registration Certificate to Micro & Small Enterprise for items/stores as recommended.</li>
                        <li>REGISTRATION FEE:The registration Fee is based on the Net Sales Turnover as per latest audited Balance Sheet of the Micro & Small Enterprise for the Registration, Renewal and any other amendment etc. For detailed fee structure please see the Document section.
                                    The Registration Fee is mentioned in the Document section is excluding the Inspection charges. For details of inspecting agencies and their fee please see the Document section.
                        </li>
                        <li>List of Empanelled Inspection Agencies with complete Fees structure can be downloaded from below DOCUMENTS section under link "List of Empanelled Agencies along with Inspection Fees"</li>--%>
						<li>Please visit Udyam Registration website for obtaining Udyam Registartion Number.</li>
						<li>MSEs have to register themselves in MSMEDataBank using Udyam Registration
						No. and PAN, to get themslves registered for Single Point Registration.</li>
						<li>Single Point Registration has a 7 step form,each step can be filled staggeredly.Each step has save and continue option.This way form can be refilled and reviewed at any moment using your PAN and Udyam Registration combination.</li>
						<li>It is requested to go through Checklist and Download section to get to know about required forms,annexure and documents.</li>
						<li>It is always advisable to kindly place all required forms,annexure and documents on your desktop in a folder for fast uploading.</li>
						<li>Fees will be charged on the basis of Unit Enterprise category(Micro or Small).</li>
						<li>Third party inspection is also involved for store item inspection.Units are requested to kindly choose inspection agency according to their domain expertise and jurisdiction.</li>
						<li>Contact our NSIC field offices for any query by visiting https://nsic.co.in/Corporate/SearchBranch.aspx</li>
						
						
                    </ol>
                </p>
            </div>
            <div class="text-center">
                <a class="btn btn-lg btn-primary" href="appAdministrator/NewUnitRegistrationIntro.aspx" target="_blank" role="button">ONLINE REGISTRATION</a>
            </div>
        </div>
        <div class="row features-block">
            <div class="col-lg-12 features-text wow fadeInLeft">
                <!--<small>INSPINIA</small>-->
                <h2>Validity Period of Single Point Registration</h2>
                <p style="text-align: justify">
                    <ol>
                        <li>The Registration Certificate granted to the Micro & Small Enterprise under Single Point Registration Scheme (Revised), 2003 is valid for Two Years and will be reviewed and renewed after every two years by verifying continuous Commercial and Technical Competence of the registered Micro & Small Enterprise in manufacturing / producing the stores for which it has been registered by NSIC.</li>
                        <li>The Provisional Registration Certificate can be issued to those Micro & Small Enterprises under Single Point Registration scheme with monitory limit of Rs. 5.00 Lacs which shall be valid for the period of one year who have already commenced their commercial production but not completed one year of existence</li>
                    </ol>
                </p>
                <!--<a href="" class="btn btn-primary">Learn more</a>-->
            </div>
            <!-- <div class="col-lg-6 text-right wow fadeInRight"> -->
                <!-- <img src="Images/Presentation2.jpg" alt="Process" style="border: dashed thin green" class="img-responsive pull-right"> -->
            <!-- </div> -->
        </div>
    </section>

    <section id="Documents" class="gray-section team">
        <div class="container">
            <div class="row m-b-lg">
                <div class="col-lg-12 text-center">
                    <div class="navy-line"></div>
                    <h1>Neccesary Documents and Downloads</h1>
                    <p>Please refer below list for documents and downloads.</p>
                </div>
            </div>
            <div class="row">
                <table class="table">
                    <tbody>
                        <tr>


                          
 <td>1. Check-List of Fresh Registration/ Annexure</td>
                            <td><a href="Document/pdfs/1.Check_List_of_Fresh_Registration_23052023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                           <td>2. Check-List of Provisional Registration</td>
                            <td><a href="Document/pdfs/3.Check-List_of_Provisional_Registration_23052023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
							 

                        </tr>
                        <tr>
                            <td>3. Revised Check-List for Renewal of Registration</td>
                            <td><a href="Document/pdfs/2.Check-List-Renewal_23052023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                            <td>4. Check-List of Amendment In Registration</td>
                            <td><a href="Document/pdfs/4.Check-List_of_Amendment_Registration_23052023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                        </tr>



                        <tr>
                           
                            <td>5. List of Empanelled Agencies along with Inspection Fees</td>
                            <%--  <td><a href="Document/pdfs/list_29082018.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>--%>

                           <%--   <td><a href="Document/pdfs/Updated_Empanelled_Agency_Eng_21032022.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>--%>
							
							 <td><a href="Document/pdfs/English_Empanelled_Agencies_02112023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
							 
							 
							 
							 <td>6. Frequently Asked Questions (FAQs)</td>
                            <td><a href="Document/pdfs/FAQ-SPRS-4.8.2021.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>

                        </tr>
                     
                       
						<tr>
						 <td>7. List of 358 reserved items</td>
                            <td><a href="Document/pdfs/358LIST08102020.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                            <td>8. Download Quotation Format(CONSORTIA & TENDER MARKETING SCHEME)</td>
                            <td><a href="Document/Annexures/QuotationformatCTMS.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                           
                        </tr>
						 
						<tr>
						  <td>9. Guidelines along with NSIC Processing Fees</td>
                            <td><a href="Document/pdfs/Fees_2020.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
						
							  <td><a href="Fee_Calculator.aspx" target="_blank">10. Fee Calculator</a> </td>	
                          
                        </tr>	
						
						<tr>
						 <td>11. Performa of Inspection Report (for the reference of Inspecting Agency)</td>
                            <td><a href="Document/pdfs/8.FINAL_INSP_REPORT_FORMAT_23052023.pdf" target="_blank"><i class="fa fa-file-pdf-o features-icon"></i></a></td>
                         
<td></td>
						  
                        </tr>
					
                        
                    </tbody>
                </table>
            </div>
            <!--<div class="row">
                        <div class="col-lg-8 col-lg-offset-2 text-center m-t-lg m-b-lg">
                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Aut eaque, laboriosam veritatis, quos non quis ad perspiciatis, totam corporis ea, alias ut unde.</p>
                        </div>
                    </div>-->
        </div>
    </section>

    </div>

        <div class="panel panel-default">
            <div class="panel-heading">
                <p class="text-center">
                    <a class="text-info" href="http://www.nsic.co.in">Nsic Corporate Site</a> | 
						<a class="text-info" href="http://www.msmedatabank.in">MSME Databank</a> | 
                                    <a class="text-info" href="http://www.msmemart.com">MSMEmart B2B</a> | 
                                   	<a href="feedback.aspx">FeedBack</a>|
                                    <a class="text-info" href="http://www.nsic.co.in/Corporate/SearchBranch.aspx">Contact Us</a>


                </p>
            </div>
            <div class="panel-footer">
                <p class="text-center">
                    <span class="text-center"><small><strong>Copyright</strong> National Small Industries Corporation © 2018-2019</small></span>
                    <br>
                    <span class="text-center"><small>Website Design &amp; Content Managed by NSIC </small></span>
                </p>
            </div>
        </div>
    </div>



    <!-- Mainly scripts -->
    <script src="js/jquery-3.1.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>
    <script src="js/plugins/wow/wow.min.js"></script>


    <script>

        $(document).ready(function () {

            $('body').scrollspy({
                target: '.navbar-fixed-top',
                offset: 80
            });

            // Page scrolling feature
            $('a.page-scroll').bind('click', function (event) {
                var link = $(this);
                $('html, body').stop().animate({
                    scrollTop: $(link.attr('href')).offset().top - 50
                }, 500);
                event.preventDefault();
                $("#navbar").collapse('hide');
            });
        });

        var cbpAnimatedHeader = (function () {
            var docElem = document.documentElement,
                    header = document.querySelector('.navbar-default'),
                    didScroll = false,
                    changeHeaderOn = 200;
            function init() {
                window.addEventListener('scroll', function (event) {
                    if (!didScroll) {
                        didScroll = true;
                        setTimeout(scrollPage, 250);
                    }
                }, false);
            }
            function scrollPage() {
                var sy = scrollY();
                if (sy >= changeHeaderOn) {
                    $(header).addClass('navbar-scroll')
                }
                else {
                    $(header).removeClass('navbar-scroll')
                }
                didScroll = false;
            }
            function scrollY() {
                return window.pageYOffset || docElem.scrollTop;
            }
            init();

        })();

        // Activate WOW.js plugin for animation on scrol
        new WOW().init();
 $(window).on('load', function () {
            $('#imagemodal').modal('show');
        });
    </script>

</body>
</html>