<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="DownloadCertificate.aspx.cs" Inherits="appAdministrator_DownloadCertificate" %>

<!DOCTYPE html>
<html>

<head>

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>SPRS | QR-Verification</title>

    <link href="../cssQR/bootstrap.min.css" rel="stylesheet">
    <link href="../cssQR/font-awesome.css" rel="stylesheet">
    <link href="../cssQR/animate.css" rel="stylesheet">
    <link href="../cssQR/style.css" rel="stylesheet">
</head>

<body class="top-navigation">

    <div id="wrapper">
        <div id="page-wrapper" class="gray-bg">
            <div class="row border-bottom white-bg">
                <nav class="navbar navbar-static-top" role="navigation">
                    <div class="navbar-header">
                    </div>
                    <div class="navbar" id="navbar">
                        <ul class="nav navbar-top-links navbar-left">
                            <li>
                                <img src="../Images/newNS.png" height="10" alt="NSIC Logo" />
                            </li>
                        </ul>
                        <h2>
                            <center>
                                National Small Industries Corporation
                                <br />
                                <h5>(A Government of India Enterprise)</h5>
                            </center>
                        </h2>
                        <div class="text-center">
                            <h2 class="text-info">Single Point Registration Scheme<br />
                                <small><u>SPRS Unit Verification Details </u></></small>
                            </h2>
                        </div>
                        <%--  <h4><center><u> SPRS Unit Verification Details </u></center></h4>--%>
                    </div>
                </nav>
            </div>


            <form runat="server">
                <div class="wrapper wrapper-content">
                    <div class="row animated fadeInRight">
                        <div class="col-md-4">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h3>
                                        <asp:Label CssClass="headerLink" runat="server" ID="lblUnitName" /></h3>
                                </div>
                                <div>
                                    <div class="ibox-content no-padding border-left-right">
                                    </div>
                                    <div class="ibox-content profile-content">
                                        <table class="table" style="margin-top: 0">
                                            <%--   <thead>
                                                <tr>
                                                    <th>Parameters
                                    </th>
                                                    <th>Details
                                    </th>
                                                </tr>
                                            </thead>--%>
                                            <tbody>
                                                <tr>
                                                    <th>Validity</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblValidity" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Date of issue</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblDOI" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Constitution</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblConstitution" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Udyam Registration</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblUdyam" Style="text-transform:capitalize"/></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Social Class</th>
                                                    <td><span class="text-primary ">
                                                        <asp:Label runat="server" ID="lblSocialClass" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Special category</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblSpl" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Office Address</th>
                                                    <td><span class="text-primary">
                                                        <asp:Label runat="server" ID="lblAddress" /></span></td>
                                                </tr>
                                                <tr>
                                                    <th>Name of the
                                                <asp:Label ID="lbl_name_of_DIRECTOR" runat="server" Text=""></asp:Label></th>
                                                    <td><span class="text-primary">
                                                        <asp:HiddenField ID="hdnlablName" runat="server" />

                                                        <asp:Label ID="lblpartnaerName" runat="server" Text=''></asp:Label></span></td>
                                                </tr>
                                            </tbody>
                                        </table>

                                        <div class="user-button">
                                            <div class="row">
                                                <%--<div class="col-md-6">
                                                    <button type="button" class="btn btn-primary btn-sm btn-block"><i class="fa fa-envelope"></i>Send Message</button>
                                                </div>
                                                <div class="col-md-6">
                                                    <button type="button" class="btn btn-default btn-sm btn-block"><i class="fa fa-coffee"></i>Buy a coffee</button>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>GOVERNMENT PURCHASE REGISTRATION NO:<asp:Label ID="lblRegistration" runat="server" /></h5>
                                    <div class="ibox-tools">
                                        <%-- <a class="collapse-link">
                                            <i class="fa fa-chevron-up"></i>
                                        </a>--%>
                                    </div>
                                </div>
                                <div class="ibox-content">

                                    <div>
                                        <div class="feed-activity-list">

                                            <div class="feed-element">

                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <strong>TURNOVER (Rupees in Lakhs):</strong>
                                                        <asp:Label ID="lbltrunover" Text="" CssClass="leftHeading1" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:GridView ID="grdTurnOver" AutoGenerateColumns="true" runat="server" CssClass="table table-responsive table-striped table-bordered table-hover dataTables-example"
                                                            CellPadding="2" CellSpacing="2" BorderColor="Black" BorderStyle="Solid"
                                                            OnRowDataBound="grd_TurnOver_RowDataBound"
                                                            OnRowCreated="grd_Turnover_RowCreated">
                                                            <RowStyle />
                                                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" />
                                                            <Columns>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="col-lg-6">

                                                        <asp:Repeater ID="Rpt_Address" runat="server">
                                                            <HeaderTemplate>
                                                                <strong>Factory Address</strong>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <table class="table table-striped table-bordered" cellpadding="0" cellspacing="0" style="vertical-align: top;" id="tbl_rept">
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


                                                    </div>
                                                </div>
                                            </div>

                                            <div class="feed-element">
                                                <div class="col-sm-12" style="overflow-x: auto; width: 100%;">
                                                    <asp:PlaceHolder ID="plc_test" runat="server"></asp:PlaceHolder>
                                                  <%--  <asp:GridView ID="grdTestMan" AutoGenerateColumns="false" Visible="false" runat="server" CssClass="table table-responsive table-striped table-bordered table-hover dataTables-example">
                                                        <Columns>
                                                            <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                                            <asp:BoundField DataField="Store(s)/ Service(s) Name" HeaderText="Store(s)/ Service(s) Name" />
                                                            <asp:BoundField DataField="Specification(s)" HeaderText="Specification(s)" />
                                                            <asp:BoundField DataField="Qualitative Capacity" HeaderText="Qualitative Capacity" />
                                                            <asp:BoundField DataField="Quantitative Capacity"  />
                                                            <asp:BoundField DataField="fGPtbl_CUnit_Store_ManRemarks" HeaderText="Remarks" />
                                                        </Columns>
                                                    </asp:GridView>--%>
                                                   <%-- <asp:GridView ID="grdTestServ" Visible="false" runat="server" AutoGenerateColumns="false" CssClass="table table-responsive table-striped table-bordered table-hover dataTables-example">
                                                        <Columns>
                                                            <asp:BoundField DataField="SNo" HeaderText="SNo" />
                                                            <asp:BoundField DataField="Store(s)/ Service(s) Name" HeaderText="Name of Service/Activities" />
                                                            <asp:BoundField DataField="Specification(s)" HeaderText="Definition of Activity/Services(s)" />
                                                            <asp:BoundField DataField="Quantitative Capacity"  />
                                                        </Columns>
                                                    </asp:GridView>--%>
                                                </div>
                                            </div>


                                        </div>



                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div class="footer">
                    <div>
                        <strong>Copyright</strong> Copyright National Small Industries Corporation &copy; 2024
                        <br />
                        Website Design & Content Managed by NSIC 
                    </div>
                </div>
            </form>
        </div>
    </div>


    <!-- Mainly scripts -->
    <script src="../cssQR/jquery-3.1.1.min.js"></script>
    <script src="../cssQR/bootstrap.min.js"></script>
    <script src="../cssQR/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../cssQR/plugins/slimscroll/jquery.slimscroll.min.js"></script>
    <script src="../cssQR/plugins/dataTables/datatables.min.js"></script>
    <!-- Custom and plugin javascript -->
    <script src="../cssQR/inspinia.js"></script>
    <script src="../cssQR/plugins/pace/pace.min.js"></script>
    <!-- Custom and plugin javascript -->
    <%--    <script src="js/inspinia.js"></script>
    <script src="js/plugins/pace/pace.min.js"></script>

    <!-- Peity -->
    <script src="js/plugins/peity/jquery.peity.min.js"></script>

    <!-- Peity -->
    <script src="js/demo/peity-demo.js"></script>--%>
</body>

</html>
