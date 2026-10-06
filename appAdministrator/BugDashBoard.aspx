<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminNew.master" AutoEventWireup="true" CodeFile="BugDashBoard.aspx.cs" Inherits="appAdministrator_BugDashBoard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>SINGLE POINT REGISTRATION - SPRS</title>

    <link href="../CSS/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="panel panel-default" style="margin-top: 5px; margin-left: 5px; margin-right: 5px">
        <div class="ibox-title" style="border-color: mediumseagreen; border-width: thin">
            <div class="text-center">
                <h4>Dashboard For Tickets Raised</h4>
            </div>
        </div>
        <div class="ibox-content" style="border-color: mediumseagreen; border-width: thin;">
            <div class="ibox float-e-margins " id="divUnits" runat="server">
                <div class="ibox-content">
                    <div class="form-group input-group">
                        <span class="input-group-addon">Ticket Status:</span>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                            <asp:ListItem Text="--Select--" Value="NULL" />
                            <asp:ListItem Text="Open" Value="Open" Selected="True" />
                            <asp:ListItem Text="Close" Value="Close" />
                        </asp:DropDownList>
                    </div>

                    <asp:GridView ID="grdBugDetails" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered table-hover dataTables-example" UseAccessibleHeader="true"
                        ShowHeaderWhenEmpty="true" EmptyDataText="No Records Present in Database">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="2%">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:Label ID="lblTitle" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_Title") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Create Date" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblCreatedOn" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_LoggedOn") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Close Date" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblClosedOn" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_ClosedOn") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Branch" HeaderStyle-Width="12%">
                                <ItemTemplate>
                                    <asp:Label ID="lblReportedby" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_Reporter") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email" HeaderStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" Text='<%#DataBinder.Eval(Container.DataItem, "targetEmailID") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Document" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDownload" Text="Download" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Bug_ID") %>' OnCommand="lnkDownload_Command" />
                                </ItemTemplate>
                            </asp:TemplateField>
                           <%-- <asp:TemplateField HeaderText="Status" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssign" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_Status") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Comments" HeaderStyle-Width="25%">
                                <ItemTemplate>
                                    <asp:Label ID="lblAssign1" Text='<%#DataBinder.Eval(Container.DataItem, "CloseComments") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                    <%-- </div>--%>
                    <%--  <asp:GridView ID="grdViewClose" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered" UseAccessibleHeader="true"
                    ShowHeaderWhenEmpty="true" EmptyDataText="No Records Present in Database">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No." >
                            <ItemTemplate>
                                <asp:Label ID="lblRowNumber1" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bug Title" HeaderStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle1" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_Title") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                  
                        <asp:TemplateField HeaderText="Reported By">
                            <ItemTemplate>
                                <asp:Label ID="lblReport1" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_Reporter") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved On ">
                            <ItemTemplate>
                                <asp:Label ID="lblClose1" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_ClosedOn") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comments">
                            <ItemTemplate>
                                <asp:Label ID="lblComments1" Text='<%#DataBinder.Eval(Container.DataItem, "CloseComments") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved By">
                            <ItemTemplate>
                                <asp:Label ID="lblResolved1" Text='<%#DataBinder.Eval(Container.DataItem, "Bug_AssignedTo") %>' runat="server" Style="word-wrap: normal; word-break: break-all;" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="View Closed">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnCloseView1" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "Bug_ID") %>' OnCommand="btnView_Command" Text="View" CssClass="btn btn-xs btn-success" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>--%>
                </div>
            </div>
        </div>
    </div>
    <%--  <div class="modal" role="dialog" id="ViewDetails" style="position: center">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <h4 class="modal-title">Details of the Bug</h4>
                </div>

                <div class="modal-body">
                    <asp:Panel BorderStyle="Inset" BorderColor="LightGoldenrodYellow" runat="server">
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <div class="col-lg-4">
                                    <span><b>REPORTER:</b></span>
                                    <asp:Label ID="lblReporter" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                                <div class="col-lg-4">
                                    <span><b>PRIORITY:</b></span>
                                    <asp:Label ID="lblPriority" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                                <div class="col-lg-4">
                                    <span><b>SEVERITY:</b></span>
                                    <asp:Label ID="lblSeverity" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                            </div>
                            <hr />
                            <div class="col-lg-12">
                                <div class="col-lg-4">
                                    <span><b>PAN:</b></span>
                                    <asp:Label ID="lblPan" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                                <div class="col-lg-4">
                                    <span><b>ASSIGNED TO:</b></span>
                                    <asp:Label ID="lblAssignedTo" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                                <div class="col-lg-4">
                                    <span><b>SPECIAL NOTES:</b></span>
                                    <asp:Label ID="lblNotes" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                            </div>
                            <hr />
                            <div class="col-lg-12">
                                <div class="col-lg-4">
                                    <span><b>ACTUAL RESULT:</b></span>
                                    <asp:Label ID="lblActual" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>
                                <div class="col-lg-4">
                                    <span><b>EXPECTED RESULT:</b></span>
                                    <asp:Label ID="lblExpected" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                                </div>

                            </div>

                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel BorderStyle="Inset" BorderColor="LightGoldenrodYellow" runat="server">
                        <div class="panel-body">
                            <div class="col-lg-12">
                                <asp:HiddenField ID="hd_BugID" runat="server" />
                                <span><b>TITLE:</b></span>
                                <asp:Label ID="lblTitle" runat="server" Style="word-wrap: normal; word-break: break-all;" />

                            </div>
                            <hr />

                            <div class="col-lg-12">
                                <asp:LinkButton ID="LinkDownload" Text="Download" runat="server" OnCommand="lnkDownload_Command" />

                            </div>
                            <hr />

                            <div class="col-lg-12">
                                <span><b>DESCRIPTION:</b></span><br />
                                <asp:Label ID="lblDescription" runat="server" Style="word-wrap: normal; word-break: break-all;" />
                            </div>

                        </div>
                    </asp:Panel>
                    <br />
                    <asp:Panel BorderStyle="Inset" BorderColor="LightGoldenrodYellow" ID="close" runat="server">
                        <div class="panel-body">
                            <div class="col-lg-12" runat="server">
                                <span><b>CLOSING COMMENTS:</b></span>
                                <asp:TextBox ID="txtComments" runat="server" CssClass="form-control" TextMode="MultiLine" />

                            </div>
                            <asp:Button class="btn btn-md btn-warning pull-right" ID="btnCloseBug" runat="server" Text="Close Bug" OnClick="btnCloseBug_Click" Style="margin-top: 5px;" />
                        </div>
                    </asp:Panel>
                </div>
                <div class="modal-footer" id="divSubmit" runat="server">
                    <button type="button" class="btn btn-white" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>--%>

    <script src="../js/jquery-3.1.1.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <script src="../js/plugins/metisMenu/jquery.metisMenu.js"></script>
    <script src="../js/plugins/slimscroll/jquery.slimscroll.min.js"></script>

    <script src="../js/plugins/dataTables/datatables.min.js"></script>

    <!-- Custom and plugin javascript -->
    <script src="../js/inspinia.js"></script>
    <script src="../js/plugins/pace/pace.min.js"></script>

    <script>
        $(document).ready(function () {

            $('.dataTables-example').DataTable({
                pageLength: 25,
                responsive: true,
                /*    dom: '<"html5buttons"B>lTfgitp',*/
                buttons: [
                    { extend: 'excel', title: 'Dashboard Details' },
                    {
                        extend: 'print',
                        customize: function (win) {
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).addClass('white-bg');
                            $(win.document.body).css('font-size', '10px');
                            $(win.document.body).find('table')
                                .addClass('compact')
                                .css('font-size', 'inherit');
                        }
                    }
                ]
            });
            $("table[id$=grdEvents]").DataTable();
        });

    </script>

</asp:Content>

