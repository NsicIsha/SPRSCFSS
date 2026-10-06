<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TermOfUse.aspx.cs" Inherits="TermOfUse" %>


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
            <b>Term of Use</b>
        </h2>
    </div>
    <div class="mail-box">
        <div class="mail-body">
            <p class="text-center"><b>IMPORTANT-READ CAREFULLY:</b></p>
            <p>The TERMS OF USE ("TOU") is a legal agreement between you (either as an individual or any other legal entity) ("You" or "Your") and NSIC ("NSIC"). Your access to, and use of NSIC websites ("Website") is subject to, and governed by, these TOU. By accessing the Website, you confirm your agreement to and acceptance of these terms. NSIC reserves the right to update the TOU at any time without notice to You. You may view the current applicable version of the TOU at any time by clicking on the "Terms of Use" link at the bottom of each Website page.</p>
            <p>NOTWITHSTANDING ANYTHING TO THE CONTRARY CONTAINED HEREIN, CERTAIN NSIC WEBSITES INCLUDING, BUT NOT LIMITED TO PASSWORD PROTECTED SITES, CONTAIN TERMS OF USE IN ADDITION TO, OR DIFFERENT FROM THE TOU AND THE TERMS OF USE POSTED ON SUCH WEBSITES SHALL APPLY.</p>
            <p><b>Access.</b> NSIC has the right, but not the duty, to terminate or suspend Your access to the Website, without notice, for any conduct that NSIC, in its sole discretion, believes is in violation of any applicable law or is harmful to the interests of another user, a third-party provider, a service provider or NSIC. NSIC makes no representation that the information contained in the Website is appropriate or available for use throughout the world. It is forbidden to access the Website from territories where the legislation provides that the content or use of the Website is illegal. You choose to use the Website on Your own initiative, and it is Your responsibility to ensure that You conform to all applicable local laws.</p>
            <p><b>Copyright.</b> NSIC, its suppliers or the original creator of the material own all copyrights to material on the Website and all other intellectual property rights related to the Website. To the extent that NSIC has the right to do so without compensation to third parties and except for material specifically provided under other terms, NSIC grants You permission to copy materials on the Website solely for Your non-commercial use in support of NSIC products. You agree that any copies of material shall retain all copyright and other proprietary notices in the same form and manner as the original. You may not, without NSIC's permission, "mirror" any material contained on the Website or any other server. Except as specified above, nothing contained herein shall be construed as conferring by implication, estoppels or otherwise any license or right under any trademark, patent, copyright, mask work protection right or any other intellectual property right of NSIC or any third party.</p>
            <p><b>Trademarks.</b> NSIC and the NSIC logo are trademarks or service marks, registered or not, of NSIC. Nothing in the Website may be interpreted so as to grant to You, directly or indirectly, the use of a trademark or service mark reproduced on the Website, whether belonging to NSIC or third party suppliers, without the written permission of NSIC.</p>
            <p><b>Information Provided to NSIC.</b> </p>
            <ol>
                <li>NSIC does not want You to, and You should not, send any confidential or proprietary information to NSIC via the Website. Any submission by You to NSIC, including but not limited to questions, comments, suggestions or the like shall be deemed to be non-confidential and shall become the property of NSIC. Furthermore, by Your submission You grant NSIC an unrestricted, irrevocable license to use, reproduce, display, perform, modify, transmit, and distribute such information. NSIC shall be free to use any ideas, concepts, know-how or techniques contained in such information for any purpose whatsoever, including but not limited to developing, manufacturing, and marketing products incorporating such information.
                </li>
            </ol>
            <p><b>Links.</b> THE LINKS ON THE WEBSITE WILL LET YOU LEAVE NSIC'S SITE. THE LINKED SITES ARE NOT UNDER THE CONTROL OF NSIC AND NSIC IS NOT RESPONSIBLE FOR THE CONTENTS OF ANY LINKED SITE OR ANY LINK CONTAINED IN A LINKED SITE, OR ANY CHANGES OR UPDATES TO SUCH SITES. NSIC IS NOT RESPONSIBLE FOR WEBCASTING OR ANY OTHER FORM OF TRANSMISSION RECEIVED FROM ANY LINKED SITE. NSIC IS PROVIDING THESE LINKS TO YOU ONLY AS A CONVENIENCE, AND THE INCLUSION OF ANY LINK DOES NOT IMPLY ENDORSEMENT BY NSIC OF THE SITE.</p>
            <p><b>Product Availability.</b> The availability of the products described on the Website, and the product descriptions, may vary from the actual.</p>
            <p><b>Export of Information.</b> The United States Export Controls laws prohibit the export of certain technical data and software to certain territories. No content from the Website may be downloaded or otherwise exported in violation of United States law or any other local law from where You may be accessing the Website. You agree to comply with all United States and foreign Federal, State and local laws and regulations applicable to the use of the Website.</p>
            <p><b>Limitation of Liability.</b> NSIC, ITS RELATED COMPANIES, DIRECTORS, EMPLOYEES, REPRESENTATIVES OR AGENTS MAY NOT, UNDER ANY CIRCUMSTANCES BE HELD LIABLE FOR ANY DIRECT, INDIRECT, SPECIAL, PUNITIVE, EXEMPLARY, INCIDENTAL OR CONSEQUENTIAL DAMAGES ARISING OUT OF THE USE OF (OR THE INABILITY TO USE) THE WEBSITE OR ANY MATERIAL ON THE WEBSITE. THIS INCLUDES, BUT IS NOT LIMITED TO, THE LOSS OF DATA OR LOSS OF PROFIT, EVEN IF NSIC WAS ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.</p>
            <p><b>Assignment.</b> The TOU may not be assigned or transferred by You without the prior written consent of NSIC. NSIC may assign the TOU to any affiliate or third party in part or in whole.</p>
            <p><b>Disputes.</b> This TOU shall be governed by, construed and enforced in accordance with the laws of India. Disputes arising here-from are exclusively subject to the jurisdiction of the Courts of Delhi, India. </p>
            <p><b>Access to Password Protected/Secure Areas.</b> Access to and use of password protected and/or secure areas of the Website is restricted to authorized users only. Unauthorized individuals attempting to access these areas of the Website may be subject to prosecution.</p>
            <p><b>Forward-Looking Statements.</b> Some of the information on this Website may contain forward-looking statements. These statements are only predictions based on our current expectations and projections about future events. Because these forward-looking statements involve risks and uncertainties, there are important factors that could cause NSIC's actual results, level of activity, performance or achievements to differ materially from the results, level of activity, performance or achievements expressed or implied by the forward-looking statements.</p>
            <p><b>Disclaimers.</b></p>
            <ol>
                <li>WHILE NSIC ATTEMPTS TO PROVIDE ACCURATE INFORMATION ON THE WEBSITE, IT ASSUMES NO RESPONSIBILITY FOR THE ACCURACY OF THE INFORMATION. NSIC MAY CHANGE THE INFORMATION ON THE WEBSITE, OR THE PRODUCTS MENTIONED, AT ANY TIME WITHOUT NOTICE. MATERIALS ON THE WEBSITE AND THE WEBSITE ITSELF IS PROVIDED "AS IS" AND NSIC DOES NOT PROVIDE ANY WARRANTY OF ANY KIND, EXPRESS OR IMPLIED. NSIC SPECIFICALLY DISCLAIMS ANY WARRANTIES OF MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE WITH RESPECT TO THE WEBSITE OR ANY CONTENT OR MATERIAL ON THE WEBSITE.</li>
                <li>NSIC MAKES NO WARRANTY THAT (i) THE WEBSITE WILL MEET YOUR REQUIREMENTS, (ii) ACCESS TO THE WEBSITE WILL BE UNINTERRUPTED, TIMELY, SECURE, OR ERROR-FREE, (iii) THE RESULTS THAT MAY BE OBTAINED FROM THE USE OF THE WEBSITE WILL BE ACCURATE OR RELIABLE, AND (iv) ANY ERRORS IN THE SOFTWARE WILL BE CORRECTED.</li>
                <li>ANY MATERIAL OR SOFTWARE DOWNLOADED OR THIRD PARTY LINK ACCESSED OR OTHERWISE OBTAINED THROUGH THE USE OF THE WEBSITE IS DONE AT YOUR OWN DISCRETION AND RISK AND YOU WILL BE SOLELY RESPONSIBLE FOR ANY DAMAGE TO YOUR COMPUTER SYSTEM OR LOSS OF DATA THAT RESULTS FROM THE DOWNLOAD OF ANY SUCH MATERIAL OR SOFTWARE OR ACCESS TO ANY SUCH THIRD PARTY WEBSITE.</li>
                <li>NO ADVICE OR INFORMATION, WHETHER ORAL OR WRITTEN, OBTAINED BY YOU FROM NSIC OR THROUGH OR FROM THE WEBSITE SHALL CREATE ANY WARRANTY NOT EXPRESSLY STATED IN THE TOU.</li>
            </ol>
            <p><b>This constitutes the entire agreement between the parties with respect to the Website and its use and, except with respect to NSIC Websites containing additional or different terms of use, supersedes all prior agreements, proposals, communications between the parties and understandings, whether written or oral.</b></p>
        </div>
    </div>
    </form>
    </div>
</body>
</html>
