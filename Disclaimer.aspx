<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Disclaimer.aspx.cs" Inherits="Disclaimer" %>

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
                <h2 class="text-danger">
                    <b>Disclaimer</b>
                </h2>
            </div>
            <div class="mail-box">
                <div class="mail-body">
                    <p>The contents of this web site are purely informative and are as collected by NSIC through various sources including but not limited to R&amp;D institutions, newsletters, publications, internet and visitors to this web site. No claim is made about the authenticity and currency of information hosted on this web site at any point of time. NSIC reserves the right to add, delete or modify the contents of this web site without notice. NSIC accepts no liability for Intellectual Property Rights, expectations for commercial returns and any kind of loss/damage to individual(s)/business(s) as a result of their using this web site or any information available on this web site. Although care has been taken to ensure trouble free uptime for this web site, NSIC does not take any responsibility for non-availability of this website on Internet for any unforeseen technical problem. </p>
                    <p>Please be informed that NSIC or any of its representatives will never send email/SMS or call you over phone to get personal information, password or credit card/accounts details. Any such e-mail/SMS or phone call is an attempt to fraudulently create confusion and get access to your account and personal information. Never respond to such email/SMS or phone call. NSIC shall not be responsible for misuse of your account, as a result of compromise of credentials on your part. </p>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
