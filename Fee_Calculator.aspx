<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fee_Calculator.aspx.cs" Inherits="Fee_Calculator" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: FEE CALCULATOR</title>


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
                            <img src="images/newNS.png" height="100" /></td>
                        <td style="width: 60%; text-align: center">
                            <%-- <div class="col-sm-8 col-xs-12">
                        <div class="text-success">--%>
                            <strong style="color: #0098DA">National Small Industries Corporation</strong>

                            <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>
                            <%-- </div>
                        <div>--%>
                            <b>Registration under Single Point Registration Scheme</b>
                            <br />
                            <br />
                            <br />
                            <b>FEE CALCULATOR</b>
                            <%--</div>
                    </div>--%>

                        </td>
                        <td style="width: 20%; text-align: left">
                            <img src="images/msme.png" height="100" width="150" /></td>
                    </tr>
                </table>
                <%--<div class="text-center">
                    <div class="col-sm-2 col-xs-12">
                        <img src="images/newNS.png" height="100" />
                    </div>
                    <div class="col-sm-8 col-xs-12">
                        <div class="text-success">
                            <strong>National Small Industries Corporation</strong>

                           <h4> (A Government of India Enterprise)</h4>
                        </div>
                        <div>
                       <b>   Registration in Single Point Registration Scheme</b>
                        </div>
                    </div>

                    <div  class="col-md-1 col-xs-10 media-n hidden-xs" >
                        <img src="images/msme.png" height="100" width="150" />
                    </div>
                </div>--%>
            </div>







            <form runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdatePanel ID="updSubmt" runat="server">
                    <ContentTemplate>

                        <div class="section" style="padding-top: 50px">
                            <span>1</span>CRITERIA          
                        </div>
                        <div class="inner-wrap">
                            <ol>
                                <li>Micro and Small Enterprises (MSEs) involved in manufacturing & Services are eligible for registration. </li>
                                <li>Traders are not eligible for registration under this Scheme. </li>
                                <li>MSEs already commenced their commercial production (Start Ups) but not completed one year of existence are eligible for Provisional Registration for one year.</li>
                            </ol>
                        </div>


                       <div class="section"><span>2</span>COMPANY TURNOVER DETAILS</div>
                        <div class="inner-wrap">
                            <div class="col-lg-12">
                                <div class="col1">

                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">Category Unit Size:</span> <span style="color: orangered">*</span>
                                         <asp:DropDownList ID="ddl_1f" runat="server" CssClass="clsDDL" AutoPostBack="false">                                              
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic"
                                                ErrorMessage="Please Select Category Unit Size" ControlToValidate="ddl_1f" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                    </label>



                                </div>
                             

                            </div>
                             <div class="col-lg-12">
                                    <div class="col2">
                                    <label for="txtPan">
                                        <span style="color: black; font-weight: bold">Enter the turnover as per latest audited balance sheet: </span><span style="color: orangered"><b>(figures in lakh)</b>*</span>
                                        <asp:TextBox ID="txtAmount" runat="server"  />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAmount" ErrorMessage="Required" runat="server" SetFocusOnError="true"/>
                                       
                                       
                                    </label>
                                </div>
                                 </div>

                             <div class="text-center">
                                            <asp:Button ID="btnf" runat="server" Text="Calculate" OnClick="btnf_Click"  />

                                            <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="updSubmt">

                                                <ProgressTemplate>
                                                    <img alt="progress" src="Images/Admin/loading.gif" />
                                                    Processing...            
                                                </ProgressTemplate>

                                            </asp:UpdateProgress>
                                        </div>
                            <div class="two-col">
                                <div class="col1">
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                </div>
                            </div>

                            

                        </div>

                         
                          <div class="two-col">
                                <div class="two-col">
                                    <label for="ddlNsicState">
                                          <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
                                        <asp:HiddenField ID="HdnFieldTurnover" runat="server" />
                                    </label>
                                </div>
                                
                            </div>

                        <div runat="server" id="divAfterResult" visible="false">
                        <div class="section"><span>3</span>FURTHER INFORMATION</div>
                         
                            
                            <div class="button-section">
                                <p><a class="btn btn-primary" href="PointOfInterest.aspx" role="button">Apply Online</a>
				 
            </p>

                            </div>
                            </div>
                            
                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>
        </div>

    </body>


    </html>

