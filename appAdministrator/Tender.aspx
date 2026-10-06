<%@ Page Language="C#" MasterPageFile="~/appMaster/Login.master" AutoEventWireup="true" CodeFile="Tender.aspx.cs" Inherits="Tender" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content2" ContentPlaceHolderID="placeholder" runat="Server">

     <div class="row wrapper border-bottom white-bg page-heading">
        <div class="wrapper wrapper-content animated fadeInRight">
            <div class="row">
                <div class="col-lg-12">
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>Tender Details</h5>
                            <%--<div class="ibox-tools">
                                    <a class="collapse-link">
                                        <i class="fa fa-chevron-up"></i>
                                    </a>
                                    <a class="dropdown-toggle" data-toggle="dropdown" href="#">
                                        <i class="fa fa-wrench"></i>
                                    </a>
                                    <ul class="dropdown-menu dropdown-user">
                                        <li><a href="#">Config option 1</a>
                                        </li>
                                        <li><a href="#">Config option 2</a>
                                        </li>
                                    </ul>
                                    <a class="close-link">
                                        <i class="fa fa-times"></i>
                                    </a>
                                </div>--%>
                        </div>
                        <div class="ibox-content">

                            
                            <asp:GridView ID="grdTenders" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered dataTables-example" ShowHeaderWhenEmpty="true" EmptyDataText="No Record Present in Database" UseAccessibleHeader="true">

                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tender Title">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTitle" Text='<%#DataBinder.Eval(Container.DataItem, "Title") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tender Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDesc" Text='<%#DataBinder.Eval(Container.DataItem, "Description") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NSIC Location">
                                        <ItemTemplate>
                                            <asp:Label ID="lblLocation" Text='<%#DataBinder.Eval(Container.DataItem, "Location") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tender Critical Dates">
                                        <ItemTemplate>
                                            <%--<span>Publishing date:
                                            <asp:Label ID="lbl1" Text='<%#DataBinder.Eval(Container.DataItem, "PublishDate") %>' runat="server" />
                                                <asp:Label ID="Label2" Text='<%#DataBinder.Eval(Container.DataItem, "PublishTime") %>' runat="server" />
                                            </span>
                                            <br />
                                            <span>Pre bid Meeting date:
                                            <asp:Label ID="Label1" Text='<%#DataBinder.Eval(Container.DataItem, "PreBidDate") %>' runat="server" />
                                                <asp:Label ID="Label3" Text='<%#DataBinder.Eval(Container.DataItem, "PreBidTime") %>' runat="server" />
                                            </span>
                                            <br />
                                            <span>Bid Submission Start Date
                                            <asp:Label ID="Label4" Text='<%#DataBinder.Eval(Container.DataItem, "StartDate") %>' runat="server" />
                                                <asp:Label ID="Label5" Text='<%#DataBinder.Eval(Container.DataItem, "StartTime") %>' runat="server" />
                                            </span>
                                            <br />--%>
                                            <span>Bid Submission End Date:
                                            <asp:Label ID="Label6" Text='<%#DataBinder.Eval(Container.DataItem, "EndDate") %>' runat="server" />
                                                <asp:Label ID="Label7" Text='<%#DataBinder.Eval(Container.DataItem, "EndTime") %>' runat="server" />
                                            </span>
                                            <br />
                                            <span>Bid Opening Date:
                                            <asp:Label ID="Label8" Text='<%#DataBinder.Eval(Container.DataItem, "OpenDate") %>' runat="server" />
                                                <asp:Label ID="Label9" Text='<%#DataBinder.Eval(Container.DataItem, "OpenTime") %>' runat="server" />
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkDownload" Text="Download" runat="server" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "TenderID") %>' OnClick="lnkDownload_Click"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


   
</asp:Content>

