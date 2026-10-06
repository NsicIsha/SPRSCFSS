<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrivacyPolicy.aspx.cs" Inherits="PrivacyPolicy" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: SPRS Useful links</title>


    <link href="http://www.nsic.co.in/css/bootstrap.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/font-awesome/css/font-awesome.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick-theme.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/dataTables/datatables.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/animate.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/style.css?ver=2018" rel="stylesheet" />


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
                    <td style="width: 20%; text-align: left"></td>
                </tr>
            </table>
        </div>
        <form runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>         

                <div class="mail-box-header">
                    <h2>
                        <b>Privacy Policy</b>
                    </h2>
                </div>
                <div class="mail-box">
                    <div class="mail-body">
                        <p class="text-danger"><b>Privacy Statement </b></p>
                        <p>Your privacy is important to NSIC. This statement discloses the information practices, including what type of information is gathered and tracked, how the information is being used, and with whom the information is being shared. If you have questions or concerns or suggestions regarding NSIC's privacy policy, please contact: info[at]nsic[dot]co[dot]in NSIC has long recognized that individuals with whom we conduct business value their privacy. However, in order to conduct global business in this increasingly electronic economy, the collection of Personal Data is often necessary and desirable. It is NSIC's goal to balance the benefits of e-business with the right of individuals to prevent the misuse of their Personal Data. </p>
                        <p class="text-danger"><b>The Collection of Personal Data </b></p>
                        <p>Please be aware that most parts of our website are available and open to you without having to provide any Personal Data (defined below). However, in order to access and use certain parts of NSIC website, NSIC may request User Details such as user name and passwords. NSIC uses this information to customize your experience on our web site and provide you relevant information for the members section. NSIC uses this information to customize your experience on our website. You have the right to refuse to provide this information, however access to certain secure portions of the website(s) will not be provided. By registering on the NSIC web site, you will be deemed to have consented to the use, transfer, export and retention of your Personal Data as set out in this privacy statement. Consequently, after registering, NSIC may use this submitted information for the purposes for which you and NSIC have intended and to provide you with additional information, such as alerting you to products and services that can assist you in your business, promoting site registration, and assisting in order processing. NSIC will maintain Personal Data in a secure environment through a secure connection/encryption to ensure confidentiality. NSIC aims to ensure that any Personal Data held by NSIC is accurate. You may update or amend your Personal Data held by NSIC by changing your profile information on the site(s) for which you have registered. </p>
                        <p class="text-danger"><b>Transfer of Personal Data </b></p>
                        <p>When you submit Personal Data to NSIC, you acknowledge and agree that NSIC may transfer Personal Data to, and retain and process Personal Data in, any NSIC corporate site(s) including but not limited to its parent companies and affiliates. NSIC may also transfer and/or retain the Personal Data you submitted to a country outside the country in which you are located or from where such Personal Data was collected. If you do not approve of such transfer, processing and/or retention, do not submit Personal Data to NSIC. </p>
                        <p class="text-danger"><b>Collecting Domain Information </b></p>
                        <p>NSIC also collects domain information (e.g. IP address and referring URLs) as part of its analysis of the use of this site. This data enables us to become more familiar with which customers visit our site, how often they visit and what parts of the site they visit most often. NSIC uses this information to improve its web-based offerings. This information is collected automatically and requires no action on your part. We do not tie Personal Data to domain information, except as necessary to prevent abuse or fraud on our site. </p>
                    </div>
                </div>

             </form>
    </div>
</body>
</html>
