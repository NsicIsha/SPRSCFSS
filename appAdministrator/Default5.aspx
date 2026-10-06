<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Default5.aspx.cs" Inherits="appAdministrator_Default5" %>

<%@ MasterType VirtualPath="~/appMaster/Admin.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        function disableBackButton() {
            window.history.forward();
        }
        setTimeout("disableBackButton()", 0);
        $(function () {
            $("#progressbar").progressbar({ value: 15 });
        });
        function updateProgress() {
            var value = $("#progressbar").progressbar("option", "100");
            if (value < 100) {
                $("#progressbar").progressbar("value", value + 1);
            }
        }
        function UpdateProgressBar_DroDown() {
            $("#progressbar").progressbar({ value: 15 });
        }
    </script>

    <script language="javascript" type="text/javascript">
        var id = 0;
        function CaptchaRefresh(imgc, src) {

            imgc = "ctl00_ContentPlaceHolder1_" + imgc;

            //debugger;
            id++;
            document.getElementById(imgc).src = src + '?id=' + id;

        }

    </script>


    <%--<script type="text/javascript"> 
              //  $(document).ready( function() {              
                // When site loaded, load the Popupbox First         
                //loadPopupBox();              
                $('#popupBoxClose').click( function() {                         
                unloadPopupBox();         
                });                  
                
                $('#container').click( function() {
                             //unloadPopupBox(); 
                             loadPopupBox();                      
                   });                   
                function unloadPopupBox() {
                    // TO Unload the Popupbox             
                    $('#popup_box').fadeOut("slow");             
                    $("#container").css({ // this is just for style                         
                    "opacity": "1"               
                    });
        } 
               function loadPopupBox() {
                   // To Load the Popupbox             
                   $('#popup_box').fadeIn("slow");             
                   $("#container").css({ // this is just for style                 
                   "opacity": "0.3"               
                   });
               } 
               
              // });
              </script>--%>



    <style type="text/css">
        /* popup_box DIV-Styles*/

        #popup_box {
            display: none; /* Hide the DIV */
            position: fixed;
            _position: absolute; /* hack for internet explorer 6 */
            height: 334px;
            width: 634px;
            background: #FFFFFF;
            left: 300px;
            top: 150px;
            z-index: 100; /* Layering ( on-top of others), if you have lots of layers: I just maximized, you can change it yourself */
            margin-left: 15px; /* additional features, can be omitted */
            border: 2px solid #ff0000;
            padding: 15px;
            font-size: 15px;
            -moz-box-shadow: 0 0 5px #ff0000;
            -webkit-box-shadow: 0 0 5px #ff0000;
            background-color: #ECF8FB;
        }

        #container {
            /*background: #d2d2d2; /*Sample*/
            width: 100%;
            height: 100%;
            cursor: pointer;
        }

        a {
            font-size: 12px;
            cursor: pointer;
            text-decoration: none;
        }
        /* This is for the positioning of the Close Link */

        #popupBoxClose {
            font-size: 20px;
            line-height: 15px;
            right: 5px;
            top: 5px;
            position: absolute;
            color: #6fa5e2;
            font-weight: 500;
        }
    </style>




    <style type="text/css">
        .style4 {
            background-color: #DDDDDD;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 15px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
            color: #F7285C;
        }

        .style6 {
            height: 21px;
        }

        .style7 {
            height: 10px;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function checkTexts() {

        }
    </script>
    <style type="text/css">
        .custom-card {
            border: 5px solid #ddd;
            border-radius: 8px;
            padding: 20px;
            background: #fff;
            box-shadow: 0 2px 6px rgba(0,0,0,0.1);
        }

        .headerAll {
            font-size: 1.3rem;
            font-weight: bold;
            background: linear-gradient(90deg, #0d6efd, #0dcaf0);
            color: #fff;
            padding: 10px 15px;
            border-radius: 6px 6px 10px 10px;
            margin-bottom: 15px;
        }

        .options-table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }

            .options-table th {
                background-color: #f8f9fa;
                padding: 10px;
                text-align: center;
                font-weight: bold;
                border: 1px solid #dee2e6;
            }

            .options-table td {
                padding: 10px;
                border: 1px solid #dee2e6;
            }

        .form-group span {
            display: block;
            margin-bottom: 5px;
            font-weight: 500;
        }

        .proceed-btn {
            display: inline-block;
            padding: 6px 15px;
            background-color: #0b5ed7;
            color: white;           
            border-radius: 4px;
            //text-decoration: none;
        }

            .proceed-btn:hover {
                background-color: #0b5ed7;
                color: white;
            }
        .auto-style1 {
            height: 51px;
        }
    </style>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet"/>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%-- <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top" style="width: 100%;">
                <td align="left" valign="top" style="width: 100%;">
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr style="width: 100%;">
                            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Renewal Scenarios
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-top: 15px; border: 1px solid black; height: 25px" width="100%">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <span class=""><b>Unit Name:</b><span style="color: red">*</span></span>
                                                <asp:TextBox ID="txtUnitName" runat="server" Text="M/s. Shiv Shakti Enterprises" Style="text-transform: uppercase" CssClass="form-control" ValidationGroup="1" />

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <span class=""><b>PAN:</b><span style="color: red">*</span></span>
                                                <asp:TextBox ID="txtPAN" runat="server" Text="M/s. Shiv Shakti Enterprises" Style="text-transform: uppercase" CssClass="form-control" ValidationGroup="1" />

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <span class=""><b>Validity Start Date:</b><span style="color: red">*</span></span>
                                                <asp:TextBox ID="txtStartDate" runat="server" Text="M/s. Shiv Shakti Enterprises" Style="text-transform: uppercase" CssClass="form-control" ValidationGroup="1" />

                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <div class="form-group">
                                                <span class=""><b>Validity End Date:</b><span style="color: red">*</span></span>
                                                <asp:TextBox ID="txtEndDate" runat="server" Text="M/s. Shiv Shakti Enterprises" Style="text-transform: uppercase" CssClass="form-control" ValidationGroup="1" />

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <table border="1">
                                        <tr>
                                            <th>Option 1 </th>
                                            <th>Option 2</th>
                                        </tr>
                                        <tr>
                                            <td>This option will provide validity in continuity with previous certificate</td>
                                            <td>This option will provide validity as in fresh (from date of approval)</td>
                                        </tr>
                                        <tr>
                                            <td>Validity will be:<asp:Literal ID="lblop1" runat="server" /></td>
                                            <td>Validity will be:<asp:Literal ID="lblop2" runat="server" /></td>
                                        </tr>
                                        <tr>
                                            <td>50% discount (if you have applied till one month of expiry of certificate)</td>
                                            <td>Full Fees</td>
                                        </tr>
                                        <tr>
                                            <td><asp:LinkButton ID="lnkRenewal" runat="server" Text="Proceed" /></td>
                                            <td><asp:LinkButton ID="lnkFresh" runat="server" Text="Proceed" /></td>
                                        </tr>
                                    </table>

                                </div>
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>--%>

    <div class="custom-card">
        <div class="headerAll">
            <center>Renewal Scenarios</center>
        </div>

        <div class="row">
            <div class="col-md-6">              
                <span ><u>Unit Name </u>: </span>
                <asp:Label ID="txtUnitName" runat="server" Text="--"
                    Style="text-transform: uppercase;width:100%" CssClass="style6" ValidationGroup="1" />
            </div>
            <div class="row">
                <div class="col-md-6">               
                    <span><u>PAN </u>:  </span>
                    <asp:Label ID="txtPAN" runat="server" Text="--" class="style6"
                        Style="text-transform: uppercase;width:100%" CssClass="style6" ValidationGroup="1" />
                </div>
                <div class="col-md-6">
                    <span><u>Validity Start Date </u>: </span>
                    <asp:Label ID="txtStartDate" runat="server" Text="--" Style="text-transform: uppercase;width:100%"
                        CssClass="style6" ValidationGroup="1" />
                </div>
                <div class="col-md-6">
                    <span><u>Validity End Date </u>: </span>
                    <asp:Label ID="txtEndDate" runat="server" Text="--" Style="text-transform: uppercase;width:100%"
                        CssClass="style6" ValidationGroup="1" />
                </div>
            </div>
        </div>

        <table class="options-table">
            <tr>
                <th style="width: 50%;text-align: center;font-weight: bold; padding: 10px; border-radius: 5px;"><span style="color:red"> Option 1 (Choose this option to start the Renewal or Renewal-cum-Amendment process.)</span></th>
                 <th style="width: 50%;text-align: center;font-weight: bold; padding: 10px; border-radius: 5px;"><span style="color:red">Option 2 (Selecting this option will initiate a fresh registration for the unit.)</span></th>
            </tr>
            <tr>
                <td class="auto-style1">This option will provide validity in continuity with previous certificate</td>
                <td class="auto-style1">This option will provide validity as in fresh (from date of approval)</td>
            </tr>
            <tr>
                <td>Validity will be:
                        <asp:Literal ID="lblop1" runat="server" /></td>
                <td>Validity will be:
                        <asp:Literal ID="lblop2" runat="server" Text="From date of approval" /></td>
            </tr>
            <tr>
                <td>50% discount (if you have applied till one month of expiry of certificate)</td>
                <td>Full Fees</td>
            </tr>
            <tr>
                <td>
                    <center>
                        <asp:LinkButton ID="lnkRenewal" runat="server" Text="Proceed" CssClass="proceed-btn" style="color:white" />
                    </center>
                </td>
                <td>
                    <center>
                        <asp:LinkButton ID="lnkFresh" runat="server" Text="Proceed" CssClass="proceed-btn" style="color:white" OnClick="lnkFresh_Click" />
                    </center>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>

