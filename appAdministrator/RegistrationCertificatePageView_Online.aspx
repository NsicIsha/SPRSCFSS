<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="RegistrationCertificatePageView_Online.aspx.cs" Inherits="appAdministrator_RegistrationCertificatePageView_Online" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>Company Certificate</title>

    <script src="../JScript/jquery-1.7.1.js" type="text/javascript"></script>

    <style type="text/css" media="print">
        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }

            h6 {
                page-break-before: always;
            }
        }
    </style>

    <style type="text/css">
        body {
            font-family: Arial,Helvetica, Sans-Serif;
            font-size: 10pt;
            margin-left: 5pt;
            margin-top: 0px;
            margin-right: 0px;
            margin-bottom: 0px;
            /*background-image: url(../Images/NSIC_Logo1.gif);*/
            background-color: #F8F8F8;
            background-repeat: repeat;
            background-position: 0px 0px;
            /*text-align:center;*/
        }

        body, th, p, div, span, td {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            vertical-align: top;
        }

        .label_Address {
            font-family: Arial, Sans-Serif;
            font-size: 10pt;
            font-weight: bold;
        }

        .leftHeading_printcertificate {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 11pt;
            padding-left: 5pt;
            padding-right: 5pt;
            font-weight: bold;
            text-align: right;
            padding-left: 550pt;
        }

        .leftHeading_printcertificate_first {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 9pt;
            height: 9pt;
            padding-left: 5pt;
            padding-right: 5pt;
            font-weight: bold;
        }

        .btnCls_CERTIFICATE {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            background-color: #22518F;
            color: #FFFFFF;
            font-weight: bold;
            width: 95pt;
            display: none;
        }

        .leftHeading_certificate_store {
            font-family: Arial;
            font-size: 10pt;
            height: 10pt;
            padding-left: 5pt;
            padding-right: 5pt;
            font-weight: bold;
        }

        .dynRowHeadercertificate_grid {
            border: solid 1pt #011e0f;
        }

        .RowClass {
            vertical-align: top;
        }

        .leftHeading_certificate1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            font-weight: bold;
        }

        .leftHeading_certificate1_ref {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 10pt;
            padding-left: 2pt;
            font-weight: bold;
        }

        .leftHeading1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 10pt;
            padding-left: 1pt;
            padding-right: 1pt;
            font-weight: bold;
        }

        .leftHeading2 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 10pt;
            height: 10pt;
            padding-left: 10pt;
            padding-right: 5pt;
            font-weight: bold;
        }

        .lblclass {
            font-size: 10pt;
            font-family: Arial, Helvetica, sans-serif;
            font-weight: bold;
        }

        .grdturnmrgtop {
            margin-top: 5pt;
        }

        .clslinethrough {
            text-decoration: line-through;
        }

        .gridviewstore {
            min-width: 16cm;
            table-layout: fixed;
            white-space: nowrap;
        }

            .gridviewstore th {
                white-space: pre;
            }


        @page {
            margin-top: 3.4cm;
            font-size: 10pt;
        }

        .thgridcolumname {
            white-space: pre;
        }
    </style>
    <script type="text/javascript" language="javascript">


        function GetHeaderText(gridID) {
            var grid = document.getElementById(grid);
            var HeaderText = grid.rows[0].cells[1].innerText;
            grid.rows[0].cells[1].innerText = "Store(s)/" + "\n" + "Service(s) Name";
            alert(HeaderText);
        }




        function Print_main_Page() {

            document.getElementById('<%=btnPrnt.ClientID%>').style.display = "none";
            // document.getElementById('btn_next').style.visibility = "hidden";

            $("#btn_nex").remove();
            $("#next_button_row").remove();

            document.getElementById('tr_store_deatail_desc').style.display = "block";
            document.getElementById('tbl_store_Tbl').style.display = "block";
            $("#td_storedetails").css("min-width", "16cm");
            $("#td_storedetails").css("overflow", "hidden");
            $("#td_storedetails").css("table-layout", "fixed");
            $("#tbl_store_Tbl").css("min-width", "16cm");
            var x = $("#tbl_store_Tbl").css("width");
            //  alert(x);
            //                if (x > 643+"px") {

            //                    $("#tbl_store_Tbl").removeAttr("style").attr("style", "width: "+ (x - 643) + "px !important;");
            //                     
            //                }
            //                
            $("#tbl_store_Tbl").css("table-layout", "fixed");
            $("#tbl_store_Tbl").css("overflow", "hidden");


            $(".gridviewstore").removeAttr("rules");
            $(".gridviewstore").removeAttr("style");
            $(".gridviewstore").css("min-width", "16cm");
            $(".gridviewstore").css("overflow", "hidden");

            $(".gridviewstore th").css("white-space", "pre");
            $(".gridviewstore th").css("text-align", "left");

            //                var y = $(".gridviewstore").css("width");
            //                if (y > 643 + "px") {
            //                    $(".gridviewstore").removeAttr("style").attr("style", "width: " + (x - 643) + "px !important;");    
            //                }

            //                $("#Referances").css("height", "20cm");
            //                $("#Referances").css("width", "18cm");



            // $("#td_refrence").css("height", "20cm");
            document.getElementById('Referances').style.display = "block";
            window.print();

            // window.close();


        }

        function showHideDiv() {
            document.getElementById('tr_store_deatail_desc').style.display = "block"
            document.getElementById('tbl_store_Tbl').style.display = "block";

            document.getElementById('Referances').style.display = "none";
            document.getElementById('div_print_button').style.display = "block";
        }
        $(document).ready(function () {
            var enterprise = "";
            var pmps = "";
            enterprise = $("#hdn_enterprise").val();
            pmps = $("#hdn_pmps").val();
            if (enterprise == "2") {   // For service  if you change column name like just change eq value
                //$(".gridviewstore thead th").eq(3).css("display", "none");
                $(".gridviewstore tr").find('td:eq(3),th:eq(3)').css("display", "none");
                var fourthColumn = "";
                var a = $(".gridviewstore thead th").eq(4).html();
                var k = a.split("<br>");
                //alert(k[1]);
                fourthColumn = "Quantitative capacity/services<br>" + " " + pmps;
                $(".gridviewstore thead th").eq(4).htmle(fourthColumn);
                $(".gridviewstore thead th").eq(1).text("Name of Services / Activities");
                $(".gridviewstore thead th").eq(2).html("Definition of activity<br> /service");


            }
            else {   // for other
                //$(".gridviewstore thead th").eq(3).css("display", "block");
                $(".gridviewstore tr").find('td:eq(3),th:eq(3)').css("display", "block");
            }

        });
        function AlertMe() {

            window.close();
        }


    </script>
    <script language="javascript" type="text/javascript">
        window.onbeforeunload = refreshParent;
        // window.onunload = refreshParent;
        function refreshParent() {
            // var retVal = confirm("Are you sure ?");
            //if (retVal == true) {
            window.opener.location.reload();
            //}
            // else
            //   return false;

        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <table cellpadding="2" cellspacing="2" border="0" width="100%">
            <%--  -------------------------------table head reapeat in every page----------------------------------%>
            <thead>
                <tr>
                    <td align="center">
                        <div id="HederText">
                            <%--style="width:100%; height:190px; margin-top:25px;"--%>
                            <table cellpadding="2" cellspacing="2" border="0" style="table-layout: fixed; width: 99%;">
                                <tr valign="bottom">
                                    <td align="right" colspan="4" valign="bottom" id="serial_no_padding"><b>S.No.</b>
                                        <asp:Label ID="lblserialNo" runat="server" Text=""></asp:Label></td>
                                    <asp:HiddenField ID="hdn_enterprise" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdn_pmps" runat="server" Value="pm/pa" />
                                </tr>

                                <tr>
                                    <td align="right" valign="top" style="padding-left: 50pt; font-size: 10pt; white-space: nowrap;"><b>Branch Office : </b></td>
                                    <td align="left" valign="top" colspan="3">
                                        <table cellpadding="1" cellspacing="1" border="0">
                                            <tr>
                                                <td colspan="2" valign="top"><b>
                                                    <asp:Label ID="lbl_branch_address" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="leftHeading_certificate1"><b>Ph :
                                                    <asp:Label ID="lbl_branch_phone" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
                                                <td align="left" class="leftHeading_certificate1"><b>Fax :
                                                    <asp:Label ID="lbl_branch_fax" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b></td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="leftHeading_certificate1"><b>Email :
                                                    <asp:Label ID="lbl_branch_email" runat="server" Text="" CssClass="leftHeading_certificate1"></asp:Label></b> </td>
                                                <td align="left" class="leftHeading_certificate1"><b>Website :
                                                    <asp:Label ID="Label1" runat="server" Text="www.nsic.co.in"></asp:Label></b> </td>
                                            </tr>

                                        </table>

                                    </td>
                                </tr>

                                <tr>
                                    <td align="left" valign="top" colspan="2">&nbsp;</td>
                                </tr>

                                <tr>
                                    <td align="left" valign="top" colspan="2">&nbsp;</td>
                                </tr>
                                <%--<tr >
  <td align="left" valign="top"  colspan="4"> &nbsp;</td>
</tr>--%>
                                <%--<tr >
  <td align="left" valign="top"  colspan="4"> &nbsp;</td>
</tr>--%>
                            </table>

                        </div>


                    </td>


                </tr>


            </thead>

            <%-- ----------------------------- End Of THead----------------------------------------------------------- --%>
            <%--  --------------------------------Start Of TBody--------------------------------------------%>
            <tbody>
                <tr>
                    <td style="width: 100%;" id="td_refrence">
                        <div id="Referances" style="width: 100%; clear: both;">
                            <table cellpadding="0" cellspacing="0" border="0" style="table-layout: fixed; width: 99%;">
                                <tr>

                                    <td colspan="4" align="center" valign="top"><%--class="style1"  margin-top:5pt;--%>
                                        <asp:Label ID="lblvalidToFrom" runat="server" Text=""></asp:Label>
                                        <%--<br />--%>  <%--changes--%>
                                    </td>
                                    <%--CssClass="label_Address" --%>
                                </tr>


                                <tr style="margin-top: 10pt;">
                                    <td colspan="4">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 99%;">
                                            <tr>
                                                <td class="leftHeading_certificate1_ref" align="left" colspan="2" style="white-space: nowrap;"><b>Ref.No</b>&nbsp;
                                                    <asp:Label ID="lblrefnumber" runat="server" Text=""></asp:Label><%-- CssClass="label_Address"--%>
                                                </td>
                                                <td class="leftHeading_certificate1" align="right" style="padding-right: 14pt;">
                                                    <asp:Label ID="lblDate" Text="Date:" runat="server"> </asp:Label>
                                                    <asp:Label ID="lbldateform" runat="server" Text=""></asp:Label>
                                                </td>

                                            </tr>

                                        </table>

                                    </td>


                                    <%--CssClass="label_Address"--%>
                                </tr>


                                 

                                <tr>
                                    <td colspan="2" align="left" valign="top" style="width: 39%;">
                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td align="left" valign="top" style="height: 5px;">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top"><b>
                                                    <asp:Label ID="lblNameCom" runat="server" Text=''></asp:Label></b></td>
                                                <%--CssClass="label_Address"--%>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <asp:Label ID="LblCompanyAdress" runat="server" Text=''></asp:Label></td>
                                                <%--CssClass="label_Address"--%>
                                            </tr>
                                            <tr>
                                                <td align="left" valign="top">
                                                    <asp:Label ID="lbl_city_name" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td valign="top" style="white-space: nowrap;">
                                                    <asp:Label ID="lblStateName" runat="server" Text=''></asp:Label>
                                                    <%--CssClass="label_Address"--%>

                                                    <asp:Label ID="lblcompanypin" runat="server" Text=''></asp:Label>
                                                </td>
                                            </tr>

                                        </table>
                                        <br />
                                        <br />
                                        <b>Name of the
                                            <asp:Label ID="lbl_name_of_DIRECTOR" runat="server" Text=""></asp:Label></b>

                                        <asp:HiddenField ID="hdnlablName" runat="server" />
                                        <br />

                                        <asp:Label ID="lblpartnaerName" runat="server" Text=''></asp:Label>



                                    </td>
                                    <td colspan="2" align="left" valign="top" style="width: 100%;">
                                        <table cellpadding="0" cellspacing="0" border="0" style="height: 3.5cm;">
                                            <%--style="height:10px;--%>

                                            <tr valign="top" style="border-spacing: 0; padding: 0;">
                                                <td style="vertical-align: top; padding-left: 10pt;" valign="top">
                                                    <b>Factory Address:</b><br />
                                                    <asp:Repeater ID="Rpt_Address" runat="server">

                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;" id="tbl_rept">
                                                                <tr valign="top" style="border-spacing: 0; padding: 0">
                                                                    <td align="left" colspan="2">
                                                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("SNo")%>'></asp:Label>
                                                                        <asp:Label ID="lblAddress" runat="server" Text='<%#Eval("AddressName")%>'></asp:Label></td>

                                                                </tr>
                                                                <tr valign="top" style="border-spacing: 0; padding: 0;">
                                                                    <td align="left" colspan="2" valign="top">
                                                                        <asp:Label ID="lbl_citynameUnit" runat="server" Text='<%#Eval("CityName")%>'></asp:Label>
                                                                        <asp:Label ID="lblState" runat="server" Text='<%#Eval("fGPtbl_MS_Name") %>'></asp:Label>
                                                                        <asp:Label ID="lbl_pin" runat="server" Text='<%#Eval("fGPtbl_CUnit_1B_Factory_Zip")%>'></asp:Label>
                                                                    </td>
                                                                </tr>


                                                            </table>
                                                        </ItemTemplate>
                                                        <SeparatorTemplate>

                                                            <table cellpadding="0" cellspacing="0" border="0">

                                                                <tr>
                                                                    <td colspan="3"></td>

                                                                </tr>

                                                            </table>

                                                        </SeparatorTemplate>



                                                    </asp:Repeater>

                                                </td>

                                            </tr>
                                        </table>
                                        <table cellpadding="0" cellspacing="0" border="0" style="margin-top: 10pt;">
                                            <tr>
                                                <td align="left" valign="top">

                                                    <%-- <p style="text-align:justify;"> Certificate of Registration under Single

Point Registration Scheme. Units registered

under this scheme are considered to
be at par with those 

registered directly with DGS & D.</p>--%>
                                                </td>

                                            </tr>

                                        </table>
                                    </td>



                                </tr>



                                <%--<tr style="margin-top: 10pt;">
                                    <td align="left" colspan="2"><b>Constitution of the firm-</b>
                                        <asp:Label ID="lblconstitutionCode" runat="server" Text=""></asp:Label></td>
                                    <td align="center">
                                        <b>UAM</b>
                                        <asp:Label ID="lblUAM" runat="server" Text=""></asp:Label></td>
                                    <td align="left">
                                        <b>Enterprise Social Class: </b>
                                        <asp:Label ID="lbl_Gen_sc" runat="server" Text=""></asp:Label></td>

                                </tr>--%>

                                 <tr style="margin-top: 5pt; width:100%">
                                      <td align="left"><b>Constitution:</b>                                    
                                        <asp:Label ID="lblconstitutionCode" runat="server" Text=""></asp:Label></td>
                                    <td align="center"  >
                                        <b><asp:Label ID="lblUAMDisplay" runat="server" ></asp:Label></b>
                                        <asp:Label ID="lblUAM" runat="server" Text="" Style="text-transform:uppercase"></asp:Label></td>
                                    <td align="left" >
                                        <b>Enterprise Social Class: </b>
                                        <asp:Label ID="lbl_Gen_sc" runat="server" Text=""></asp:Label>                                         
                                    </td>
                                      <td align="left" >
                                        <b>Special Category: </b>
                                        <asp:Label ID="lbl_Special" runat="server" Text="None"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 2pt;"></td>
                                </tr>
                                <tr style="margin-top: 5pt;">
                                    <td colspan="4" style="white-space: nowrap; font-size: 10pt;" align="center"><b>GOVERNMENT
&nbsp;PURCHASE REGISTRATION NO:</b>&nbsp;<b><asp:Label ID="lblRegisNo" runat="server" Text=""></asp:Label></b></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 2pt;"></td>
                                </tr>
                                <tr style="margin-top: 5pt;" id="old_registration_number" runat="server" visible="false">
                                    <td colspan="4" style="white-space: nowrap; font-size: 10pt;" align="center"><b>GOVERNMENT



&nbsp;PURCHASE OLD REGISTRATION NO:</b>&nbsp;<b><asp:Label ID="lbl_old_registration_no" runat="server" Text=""></asp:Label></b></td>


                                </tr>
                                <tr>
                                    <td colspan="4" style="height: 2pt;"></td>
                                </tr>
                                <tr style="margin-top: 10pt;">

                                    <td colspan="4" style="white-space: nowrap; margin-top: 12pt;" align="center">
                                        <b>Monetary Limit:</b>
                                        <img src="../Images/RupeeLogoSmall.jpg" alt="RS." /><asp:Label ID="lblMonLimit" runat="server" Text=""></asp:Label>
                                        <img src="../Images/RupeeLogoSmall.jpg" alt="RS." /><asp:Label ID="lblTextRupee" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <%--<tr> <td colspan="4" style="height:3pt"></td></tr>--%>


                                <tr style="margin-top: 5pt;">
                                    <td colspan="4" align="center" style="margin-top: 5pt;">
                                        <asp:Label ID="lbltrunover" Text="TURNOVER (Rupees in Lakhs)" CssClass="leftHeading1" runat="server"></asp:Label>
                                        <br />


                                        <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server"
                                            CellPadding="2" CellSpacing="2" BorderColor="Black" BorderStyle="Solid" CssClass="grdturnmrgtop"
                                            OnRowDataBound="grd_TurnOver_RowDataBound"
                                            OnRowCreated="grd_Turnover_RowCreated">
                                            <RowStyle />
                                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" style="margin-top: 3pt;"><b>
                                        <asp:Label ID="lbl_consolidate" runat="server" Text=""></asp:Label></b></td>
                                </tr>
                                <tr style="border-spacing: 0; padding: 0;">
                                    <td colspan="4" style="width: 100%;">
                                        <p style="text-align: justify;">
                                            MSEs registered with NSIC are exempted from deposit of Earnest Money irrespective of value of Monetary Limit.
                                        </p>
                                    </td>
                                </tr>
                                <br />
                                <tr style="border-spacing: 0; padding: 0;">
                                    <td colspan="4" align="left" style="width: 100%;">
                                        <p style="text-align: justify;">
                                            Your name has been registered as a MSE Unit eligible for participation in the<b> Central Government Store Purchase  Programme</b> as per the 
 <b>Single Point Registration Scheme</b> for the following Item(s)/Store(s)/Service(s).
                                        </p>
                                        <%--<br />--%>
                                    </td>
                                </tr>

                                <%--<tr style=" border-spacing:0; padding:0;">
<td colspan="4" align="left"  style="height:0px; border-spacing:0; padding:0;text-decoration:line-through;">


</td>


</tr>--%>
                                <tr style="border-spacing: 0; padding: 0;">
                                    <%-- style="margin-top:2px;"--%>
                                    <td colspan="4" align="left">
                                        <table cellpadding="0" cellspacing="0" border="1" style="width: 99%;" align="left">
                                            <tr>
                                                <td class="leftHeading1" align="center" style="white-space: nowrap;">Name of the Store(s)/<br />
                                                    Service(s) </td>
                                                <td class="leftHeading1" align="center" style="white-space: nowrap;">Specification(s)</td>
                                                <td class="leftHeading1" align="center" style="white-space: nowrap;">Qualitative Capacity</td>
                                                <td class="leftHeading1" align="center" style="white-space: nowrap;">Quantitative Capacity
                                                    <br />
                                                    <asp:Label ID="lbl_Quantitive_capacity" runat="server" Text=""></asp:Label></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center" class="leftHeading_printcertificate_first" style="font-size: 10pt;">"As per List Attached"
                                        <asp:Label ID="lblGridCount" runat="server" Text=''></asp:Label></td>
                                </tr>
                                <tr>
                                    <%--<td colspan="4" align="center" style=" border-spacing:0; padding:0; font-size:10pt;" class="leftHeading_printcertificate_first">---%>
                                    <td colspan="4" align="left" style="border-spacing: 0; padding: 0; font-size: 7pt; font-family: Arial, Helvetica, sans-serif; height: 9pt; padding-left: 5pt; padding-right: 5pt; font-weight: none;" <%--class="leftHeading_printcertificate_first"--%>>
                                        <%--<asp:Label ID="lblGridCount" runat="server" Text=''></asp:Label>--%>
                                        <p>
                                            <b>Disclaimer:-</b> The purchasing agencies are advised to satisfy themselves with the store details in the certificate while doing the Technical Evaluation stage before placing the tender/order on the units, certified by NSIC.
                                            <asp:Label ID="lblAyush" runat="server" Visible="false">For Terms & Conditions of AYU&H please refer last page of stores.</asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr id="next_button_row">
                                    <td colspan="4" align="center">
                                        <input type="button" id="btn_next" value="Next Page" onclick="Javascript: showHideDiv();" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                    </td>
                </tr>

                <%-- --------------------------------- Start  Store Details Page  --------------------------------- --%>

                <%--     <tr>
                 <td align="left" valign="top" style="width:100%; ">&nbsp;</td>
               
               </tr>
                 <tr>
                 <td align="left" valign="top" style="width:100%; ">&nbsp;</td>
               
               </tr>
                <tr>
                 <td align="left" valign="top" style="width:100%;">&nbsp;</td>
               
               </tr>
               <tr>
                 <td align="left" valign="top" style="width:100%;">&nbsp;</td>
               
               </tr>
                 <tr>
                 <td align="left" valign="top" style="width:100%;">&nbsp;</td>
               
               </tr>--%>
                <tr style="height: 0px;">
                    <td style="height: 0pt; width: 100%">
                        <h6 style="margin: 0; height: 0cm;"></h6>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">

                        <table cellpadding="2" cellspacing="2" border="0" id="tr_store_deatail_desc" style="display: none; width: 99%;">
                            <%--margin-top:40px;--%>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblvalidToFrom_store" runat="server" Text=""></asp:Label>

                                </td>

                            </tr>
                            <tr>
                                <td align="left" style="width: 100%;">ANNEXURE TO GOVERNMENT PURCHASES ENLISTMENT CERTIFICATE
                         
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" style="width: 100%">
                                    <table cellpadding="2" cellspacing="2" border="0">
                                        <tr>
                                            <td align="left">NO.&nbsp;
                                                <asp:Label ID="lblregistration_number" runat="server" Text=''
                                                    CssClass="lblclass"></asp:Label>&nbsp; D.T.&nbsp;<asp:Label ID="lbldatestore" runat="server"
                                                        Text=''></asp:Label>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td align="left">ISSUED TO&nbsp;
                                                <asp:Label ID="lblCompanyNameStore" runat="server" Text=''
                                                    CssClass="lblclass"></asp:Label>
                                                <asp:Label ID="lblstanameStore" runat="server" Text=''></asp:Label>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>


                        </table>


                    </td>


                </tr>

                <tr>
                    <td align="left" valign="top" style="width: 100%;" id="td_storedetails">
                        <%--  ------------------------- Label and Gridview Data    ---------------------%>
                        <table cellpadding="2" cellspacing="2" border="0" style="display: none; width: 100%;" id="tbl_store_Tbl">
                            <%-- table-layout:fixed;--%>
                            <tr>
                                <td align="left" valign="top" style="width: 100%;">
                                    <asp:PlaceHolder ID="plc_test" runat="server"></asp:PlaceHolder>
                                </td>

                            </tr>

                            <tr>
                                <td align="left" valign="top" style="margin-top: 10pt;"></td>
                            </tr>
                            <tr>
                                <td align="left" style="border-spacing: 0; padding: 0; font-size: 10pt; text-align: justify;">
                                    <asp:Label ID="lbl_Comment_text" runat="server" Text=""></asp:Label>
                                    <div id="lblAyushDisclaimer" runat="server" visible="false">
                                        <p>Disclaimer : This Enlistment Certificate has been issued under Single Point Registration Scheme (SPRS) of NSIC for the manufacturing Micro & Small Enterprises (MSEs) of ASU&H (Ayurveda, Siddha, Unani & Homeopathy) products.  The MSEs involved in manufacturing activities are inspected by NSIC empaneled reputed third party inspecting agency.  This Enlistment Certificate has been issued subject to the following:-</p>
                                        <ol>
                                            <li type="a">NSIC will not be responsible for product testing, licensing registration, quality assessment of ASU&H products as the same shall be carried out by the designated authorities i.e. Drug Licensing Authorities.</li>
                                            <li type="a">NSIC’s empaneled inspecting agency has assessed manufacturing capacity only which has been mentioned in quantitative column.</li>
                                            <li type="a">SPRS Registration of above products shall remain valid for two years or till the validity of Drug License of the particular product whichever is earlier.</li>
                                        </ol>
                                    </div>
                                </td>
                            </tr>
                        </table>



                        <%--  ------------------------- 
                           end of  Label and Gridview Data    ---------------------%>
                    </td>

                </tr>



            </tbody>




            <%--  --------------------------------End  Of TBody--------------------------------------------%>


            <%--  --------------------------------Start Of TFoot--------------------------------------------%>
            <tfoot>
                <tr>
                    <td valign="top" align="right" style="width: 100%; margin-right: 14pt;">
                        <table cellpadding="2" cellspacing="2" border="0" id="tbl_print_deatils" style="width: 99%;">

                            <tr>
                                <%--<td align="left"  style="width:425px;">&nbsp;</td>--%>
                                <td align="left">
                                    <br />
                                    <br />
                                    <br />
                                    <asp:Label ID="lbl_company_name_footer" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">
                                    <br />
                                    <br />
                                    <br />
                                    Authorised Signatory
     
    
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <b>&quot;This is a computer generated certificate. No signature is required. Authenticity of the certificate can be checked through the web portal: www.nsicspronline.com(http://www.nsicspronline.com/Final_Verify.aspx)"</b>

                                </td>
                            </tr>


                        </table>
                    </td>

                </tr>

            </tfoot>


            <%--  --------------------------------End  Of TFoot--------------------------------------------%>
        </table>



        <%-- -----------------------------------------------------------start Of Print Button-----------------------%>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">

            <tr>



                <td align="center">
                    <div id="div_print_button" style="display: none; vertical-align: top;" align="center">


                        <asp:Button runat="server" ID="btnPrnt" Text="Print" OnClick="btnPrnt_Click"
                            OnClientClick="javascript:Print_main_Page();" />
                        <asp:Label ID="lbl_serail_add_no" runat="server" Text="" Visible="false"></asp:Label>
                        <asp:HiddenField ID="hdn_company_id" runat="server" />
                        <asp:HiddenField ID="hdn_formtypeid" runat="server" />

                    </div>

                </td>

            </tr>

        </table>

        <%-- -----------------------------------------------------------End Of Print Button-----------------------%>
    </form>
</body>
</html>