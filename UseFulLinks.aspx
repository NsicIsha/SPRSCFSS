<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="UseFulLinks.aspx.cs" Inherits="UseFulLinks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: SPRS Useful links</title>


   <%-- <link href="https://www.nsic.co.in/css/bootstrap.min.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/font-awesome/css/font-awesome.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/css/plugins/slick/slick.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/css/plugins/slick/slick-theme.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/css/plugins/dataTables/datatables.min.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/css/animate.css?ver=2018" rel="stylesheet" />
    <link href="https://www.nsic.co.in/css/style.css?ver=2018" rel="stylesheet" />--%>


    <link href="CSS/Linkbootstrap2018.css" rel="stylesheet" />
    <link href="CSS/LinkFonts2018.css" rel="stylesheet" />
    <link href="CSS/LinkSlick.css" rel="stylesheet" />
    <link href="CSS/LinkSlickTheme.css" rel="stylesheet" />
    <link href="CSS/LinkDatatable.css" rel="stylesheet" />
    <link href="CSS/Linkanimate.css" rel="stylesheet" />
    <link href="CSS/LinkStyle.css" rel="stylesheet" />


    <link href='http://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'>
    <style type="text/css">
        body {
            background: #AEE4FF;
        }

        .form-style-10 {
            max-width: 800px;
            padding: 30px;
            margin: 40px auto;
            background: #FFF;
            border-radius: 10px;
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            -webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
        }

            .form-style-10 .inner-wrap {
                padding: 30px;
                background: #F8F8F8;
                border-radius: 6px;
                margin-bottom: 15px;
            }

            .form-style-10 h1 {
                background: #2A88AD;
                padding: 20px 30px 15px 30px;
                margin: -30px -30px 30px -30px;
                border-radius: 10px 10px 0 0;
                -webkit-border-radius: 10px 10px 0 0;
                -moz-border-radius: 10px 10px 0 0;
                color: #fff;
                text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                font: normal 30px 'Bitter', serif;
                -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                border: 1px solid #257C9E;
            }

                .form-style-10 h1 > span {
                    display: block;
                    margin-top: 2px;
                    font: 13px Arial, Helvetica, sans-serif;
                }

            .form-style-10 label {
                display: block;
                font: 13px Arial, Helvetica, sans-serif;
                color: #888;
                margin-bottom: 15px;
            }

            .form-style-10 input[type="text"],
            .form-style-10 input[type="date"],
            .form-style-10 input[type="datetime"],
            .form-style-10 input[type="email"],
            .form-style-10 input[type="number"],
            .form-style-10 input[type="search"],
            .form-style-10 input[type="time"],
            .form-style-10 input[type="url"],
            .form-style-10 input[type="password"],
            .form-style-10 textarea,
            .form-style-10 select {
                display: block;
                box-sizing: border-box;
                -webkit-box-sizing: border-box;
                -moz-box-sizing: border-box;
                width: 90%;
                padding: 8px;
                border-radius: 6px;
                -webkit-border-radius: 6px;
                -moz-border-radius: 6px;
                border: 2px solid #fff;
                box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                -moz-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                -webkit-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                height: 50%;
            }

            .form-style-10 .section {
                font: normal 20px 'Bitter', serif;
                color: #2A88AD;
                margin-bottom: 5px;
            }

                .form-style-10 .section span {
                    background: #2A88AD;
                    padding: 5px 10px 5px 10px;
                    position: absolute;
                    border-radius: 50%;
                    -webkit-border-radius: 50%;
                    -moz-border-radius: 50%;
                    border: 4px solid #fff;
                    font-size: 14px;
                    margin-left: -45px;
                    color: #fff;
                    margin-top: -3px;
                }

            .form-style-10 input[type="button"],
            .form-style-10 input[type="submit"] {
                background: #2A88AD;
                padding: 8px 20px 8px 20px;
                border-radius: 5px;
                -webkit-border-radius: 5px;
                -moz-border-radius: 5px;
                color: #fff;
                text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                font: normal 30px 'Bitter', serif;
                -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                border: 1px solid #257C9E;
                font-size: 15px;
            }

                .form-style-10 input[type="button"]:hover,
                .form-style-10 input[type="submit"]:hover {
                    background: #2A6881;
                    -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                    -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                    box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                }

            .form-style-10 .privacy-policy {
                float: right;
                width: 250px;
                font: 12px Arial, Helvetica, sans-serif;
                color: #4D4D4D;
                margin-top: 10px;
                text-align: right;
            }

        .two-col {
            overflow: hidden; /* Makes this div contain its floats */
        }

            .two-col .col1,
            .two-col .col2 {
                width: 50%;
            }

            .two-col .col1 {
                float: left;
            }

            .two-col .col2 {
                float: right;
            }

        /*.two-col label {
                    display: block;
                }*/
    </style>

</head>
<body>




    <div class="form-style-10">






        <div style="width: 98%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: left">
                        <img src="../images/newNS.png" height="100" /></td>
                    <td style="width: 60%; text-align: center">
                        <strong style="color: #0098DA">National Small Industries Corporation</strong>
                        <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>
                        <%-- </div>
                        <div>--%>
                        <b>Single Point Registration Software</b>
                        <br />
                        <br />
                        <br />
                      
                      

                    </td>
                    <td style="width: 20%; text-align: left">                  
                    </td>
                </tr>
            </table>            
        </div>
        <form runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
              <div class="mail-box-header">
                <h2 class="text-danger">
                    <b>Useful Links</b>
                </h2>
            </div>
             <div class="mail-box">
                    <div class="mail-body">
            <table class="table table-bordered" style="border-width:thick">
				<thead>
					<tr>
						<th>S.N.</th>
						<th>Description</th>
						<th>Link</th>
					</tr>
				</thead>
				<tbody>
				
				
				    <tr>
						<td>1</td>
						<td>MSME DataBank</td>
						<td><a href="http://www.msmedatabank.in/" target="_blank">www.msmedatabank.in</a></td>
					</tr>
					
					<tr>
						<td>2</td>
						<td>Single Point Registration</td>
						<td><a href="http://www.nsicspronline.com/" target="_blank">www.nsicspronline.com</a></td>
					</tr>
					
					<tr>
						<td>3</td>
						<td>B2B</td>
						<td><a href="http://www.msmemart.com/" target="_blank">www.msmemart.com</a></td>
					</tr>
					
					<tr>
						<td>4</td>
						<td>Finance Facilitation Centre </td>
						<td><a href="http://www.nsicffconline.in" target="_blank">www.nsicffconline.in</a></td>
					</tr>
					

					
					<tr>
						<td>5</td>
						<td>MSME Research</td>
						<td><a href="http://www.msmeresearch.com/" target="_blank">www.msmeresearch.com</a></td>
					</tr>
				
					
					<tr>
						<td>6</td>
						<td>NSIC Venture Capital Fund Limited</td>
						<td><a href="http://www.nvcfl.co.in" target="_blank">www.nvcfl.co.in</a></td>
					</tr>
					
									
					<tr>
						<td>7</td>
						<td>NSIC TV</td>
						<td><a href="http://nsictv.com/" target="_blank">www.nsictv.com</a></td>
					</tr>
					
				    <tr>
						<td>8</td>
						<td>National SC ST HUB</td>
						<td><a href="https://www.scsthub.in/" target="_blank">www.scsthub.in</a></td>
					</tr>
					<tr>
						<td>9</td>
						<td>Indo-Korea Technology Exchange Centre</td>
						<td><a href="http://iktec.in/" target="_blank">www.iktec.in</a></td>
					</tr>
					
					<tr>
						<td>10</td>
						<td>National Portal</td>
						<td><a href="https://www.india.gov.in/" target="_blank">National Portal</a></td>
					</tr>
					<tr>
						<td>11</td>
						<td>Centralized Public Grievance Redress And Monitoring System
						</td>
						<td><a href="https://pgportal.gov.in/" target="_blank">CPGRMS</a></td>
					</tr>
					<tr>
						<td>12</td>
						<td>TReDS Platform Link (Receivables Exchange of India Limited)
						</td>
						<td><a href="https://www.rxil.in/Home/Index" target="_blank">RxIL</a></td>
					</tr>

                                         <tr>
						<td>13</td>
						<td>Link for taking Integrity Pledge 
						</td>
						<td><a href="https://pledge.cvc.nic.in/" target="_blank">CVC Website</a></td>
					</tr>
				
			</tbody>
			</table>
        </div></div>
                        </form>
    </div>
</body>
</html>

