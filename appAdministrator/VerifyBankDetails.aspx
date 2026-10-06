<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VerifyBankDetails.aspx.cs" Inherits="appAdministrator_VerifyBankDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Header_Admin.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: POINT OF INTEREST</title>


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
            overflow: hidden; 
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
    </style>
</head>
<body>
    <div class="form-style-10">
        <div style="width: 98%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
            <table style="width: 100%">
                <tr>
                    <td style="width: 20%; text-align: left">
                        <img src="../Images/newNS.png" height="100" /></td>
                    <td style="width: 60%; text-align: center">
                        <strong style="color: #0098DA">National Small Industries Corporation</strong>
                        <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>
                        <b>Registration under Single Point Registration Scheme</b>
                        <br />
                        <br />
                        <br />
                    </td>
                    <td style="width: 20%; text-align: left">
                        <img src="../images/msme.png" height="100" width="150" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
            <table width="100%" border="0" cellpadding="5" cellspacing="5" style="background-color: #2596be; color: #ffffff">
                <tr align="Centre" valign="top">
                    <td align="Centre">Bank Details for NSIC Fee Payment - SPRS Registration</td>
                </tr>
            </table>
        </div>
        <form runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="updSubmt" runat="server">
                <ContentTemplate>
                    <div class="section"><span>1</span>Branch Contact Person Details</div>
                    <div class="inner-wrap">
                        <div class="two-col">
                            <div class="col1">
                                <label for="txtUAM">
                                    <span style="color: black; font-weight: bold">Name of Contact Person:</span><asp:TextBox ID="txtContactPersonName" runat="server" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ID="rfvContact" ControlToValidate="txtContactPersonName" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                            <div class="col2">
                                <label for="txtPan">
                                    <span style="color: black; font-weight: bold">Address: </span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtAddress" TextMode="MultiLine" runat="server" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ID="rfvAddress" ControlToValidate="txtAddress" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                        </div>
                        <div class="two-col">
                            <div class="col1">
                                <label for="txtCompanyName">
                                    <span style="color: black; font-weight: bold">Mobile No.:</span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtContactNumber" MaxLength="10" runat="server" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtContactNumber" ErrorMessage="Required" runat="server" Display="Dynamic" ValidationGroup="validate" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="validate" Display="Dynamic"
                                        ControlToValidate="txtContactNumber" ErrorMessage="Provide Valid Mobile Number"
                                        ValidationExpression="[6-9][0-9]{9}"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                            <div class="col2">
                                <%-- <label for="txtPan">
                                    <span style="color: black; font-weight: bold">Address: </span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="TextBox7" runat="server" MaxLength="10" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan"
                                        Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ValidationGroup="Enroll"></asp:RegularExpressionValidator>
                                </label>--%>
                            </div>
                        </div>
                    </div>
                    <div class="section"><span>2</span>Bank Details</div>
                    <div class="inner-wrap">
                        <div class="two-col">
                            <div class="col1">
                                <label for="ddlNsicState">
                                    <span style="color: black; font-weight: bold">Bank Name:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtBankName" runat="server" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvBankName" ControlToValidate="txtBankName" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                            <div class="col2">
                                <label for="ddlNsicDistrict">
                                    <span style="color: black; font-weight: bold">Bank Branch:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtBankbranch" runat="server" MaxLength="10" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtBankbranch" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                        </div>
                        <div class="two-col">
                            <div class="col1">
                                <label for="ddlNsicState">
                                    <span style="color: black; font-weight: bold">Bank Address:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtBankAddress" TextMode="MultiLine" runat="server" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator Display="Dynamic" ControlToValidate="txtBankAddress" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="section"><span>3</span>Account Details</div>
                    <div class="inner-wrap">
                        <div class="two-col">
                            <div class="col1">
                                <label for="ddlNsicState">
                                    <span style="color: black; font-weight: bold">Account Type:</span> <span style="color: orangered">*</span>
                                   <asp:DropDownList ID="ddlAccountType" runat="server">
                                       <asp:ListItem Value="Current Account" Selected="True">Current Account</asp:ListItem>
                                   </asp:DropDownList>
                                    <asp:RequiredFieldValidator InitialValue="0" ID="rfvAccounttype" Display="Dynamic" ControlToValidate="ddlAccountType" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                </label>
                            </div>
                            <div class="col2" id="selector_cat">
                                <label for="ddlNsicDistrict">
                                    <span style="color: black; font-weight: bold">IFSC Code:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtIFSCCODE" runat="server" MaxLength="11" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ID="rfvIFSCCODE" Display="Dynamic" ControlToValidate="txtIFSCCODE" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                    <asp:RegularExpressionValidator ID="rfvIFSCCODEss" runat="server" ValidationGroup="validate" Display="Dynamic"
                                        ControlToValidate="txtIFSCCODE" ErrorMessage="Provide Valid IFSC Code"
                                        ValidationExpression="^[A-Za-z]{4}0[A-Z0-9a-z]{6}$"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                        </div>
                        <div class="two-col">
                            <div class="col1">
                                <label for="ddlNsicState">
                                    <span style="color: black; font-weight: bold">Account Number:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtAccountNumber" runat="server" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ID="rfvAccountNumber" Display="Dynamic" ControlToValidate="txtAccountNumber" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                    <asp:RegularExpressionValidator ID="rfvvAccountNumber" runat="server" ValidationGroup="validate" Display="Dynamic"
                                        ControlToValidate="txtAccountNumber" ErrorMessage="Provide Valid Bank Account Number"
                                        ValidationExpression="[1-9][0-9]{9,18}$"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                            <div class="col2">
                                <label for="ddlNsicDistrict">
                                    <span style="color: black; font-weight: bold">MICR:</span> <span style="color: orangered">*</span>
                                      <asp:TextBox ID="txtMICRCode" runat="server" MaxLength="9" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtMICRCode" Display="Dynamic"  ErrorMessage="Required" runat="server" ValidationGroup="validate" />

                                     <asp:RegularExpressionValidator ID="rfvMICR" runat="server" ValidationGroup="validate" Display="Dynamic"
                                        ControlToValidate="txtMICRCode" ErrorMessage="Provide Valid MICR"
                                        ValidationExpression="[1-9][0-9]{9}$"></asp:RegularExpressionValidator>
                                </label>
                            </div>
                        </div>
                        <div class="button-section">
                            <center>
                                <asp:Button ID="btnEnroll" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="validate" OnClick="btnEnroll_Click" />
                            </center>
                        </div>
                        <br />
                        <br />
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        <div class="panel-footer">
            <p class="text-center">
                <span class="text-center"><small><strong>Copyright</strong> National Small Industries Corporation © 2018-2019</small></span>
                <br>
                <span class="text-center"><small>Website Design &amp; Content Managed by NSIC </small></span>
            </p>
        </div>
    </div>
</body>
</html>
