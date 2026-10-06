<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/BuyingAgencyNew.master" AutoEventWireup="true" CodeFile="BuyingAgencySearchGem.aspx.cs" Inherits="appBuyingAgency_BuyingAgencySearchGem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .text-label {
            color: #ddf1f7;
            font-weight: bold;
        }

        .grdserchkk {
            vertical-align: top;
            text-align: left;
        }
    </style>


    <!-- Bootstrap core CSS -->
    <link href="../CSS/bootstrap.min.css" rel="stylesheet">

    <!-- Animation CSS -->
    <link href="../CSS/animate.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.min.css" rel="stylesheet">

    <!-- Custom styles for this template -->
    <link href="../CSS/Style_Verify.css" rel="stylesheet">


    <script type="text/javascript" language="javascript">
        function FunOpenPopup950x600(x) {
            window.open(x, '', 'height=600,width=950,resizable=yes,scrollbars=yes');
            return false;
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width: 100%;">
                <div class="section">
                    <span>1.</span>Key Points
                </div>
                <div class="inner-wrap">
                    <ol>
                        <li>The Certificate shown in link is the draft version of the original Certificate issued.</li>
                    </ol>
                </div>
                <table cellpadding="0" cellspacing="0" border="0" style="margin-top: 10px;">

                    <tr>
                        <td style="padding-left: 100px;">
                            <table cellspacing="3" cellpadding="3" border="0">
                                <tr>
                                    <td align="left" class="leftext">Search:</td>
                                    <td style="width: 2px;"></td>
                                    <td align="left" class="coloncls">
                                        <asp:DropDownList ID="ddlSearchCriteria" runat="server" AutoPostBack="false" CssClass="clsDDL" Width="220px">
                                            <%--<asp:ListItem Visible="false" Text="Company Name" Value="1"></asp:ListItem>--%>
                                            <asp:ListItem Selected="True" Text="Registration No." Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 2px;"></td>
                                    <td align="left" class="coloncls">
                                        <asp:TextBox ID="txt_serch_Criteria" runat="server" CssClass="clstxtbx" Width="220"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfdCompanyname" runat="server" ErrorMessage="Either Company Name of Registration Number Required" ControlToValidate="txt_serch_Criteria" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <cc1:TextBoxWatermarkExtender ID="txtw" runat="server" TargetControlID="txt_serch_Criteria"
                                            WatermarkText="Enter text to search" WatermarkCssClass="text-label ">
                                        </cc1:TextBoxWatermarkExtender>
                                    </td>
                                    <td style="width: 2px;"></td>
                                    <td align="left" class="coloncls">
                                        <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="update1_grid">
                                            <ProgressTemplate>
                                                <img alt="progress" src="../Images/Admin/loading.gif" />
                                                Processing...            
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="update1_grid" runat="server">
                                            <ContentTemplate>
                                                <asp:Button ID="Btn_Search" runat="server" Text="Search" CssClass="btnCls"
                                                    OnClick="Btn_Search_Click" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                    <tr>
                        <td valign="top">

                            <asp:Panel runat="server" ID="pnl1" HorizontalAlign="Center" Width="860px">
                                <asp:UpdatePanel ID="update1_DgD_agencies" runat="server">
                                    <ContentTemplate>
                                        <div class="row table-responsive">

                                            <%--  <asp:GridView ID="grd_search_result" runat="server"
                                                Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" CssClass="table table-striped table-bordered"
                                                AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" PageSize="10"
                                                OnPageIndexChanging="grdserch_pageindex_changing"
                                                OnRowDataBound="grd_search_onrowdatabound">
                                                <HeaderStyle CssClass="dynRowHeader" />
                                                <RowStyle CssClass="dynRowEven" />
                                                <AlternatingRowStyle CssClass="dynRowOdd" />
                                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                              <EmptyDataTemplate>
                                                    <strong>No Record Found, it seems unit is not registered with Single Point registration Scheme of NSIC.</strong>
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.NO">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Company" runat="server" Text='<%#Eval("CompanyName") %>'></asp:Label>
                                                            <br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Address" runat="server" Text='<%#Eval("CompanyAddress") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Social Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_SCategory" runat="server" Text='<%#Eval("SocialCategory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="EmailID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_EmailID" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_EmailIDddd" runat="server" Text='<%#Eval("Phone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inspection Report">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HLName" runat="server" ImageUrl="~/Images/Admin/view.png" Visible="true" Target="_blank"></asp:HyperLink>
                                                            <asp:HiddenField ID="HiddenFieldpath" runat="server" Value='<%#Eval("InspectionreportPath") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Certificate" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="lnkview" runat="server" ImageUrl="~/Images/Admin/view.png" CommandName="View" CommandArgument='<%#Eval("CompanyId") %>' CausesValidation="false" ToolTip="View" />
                                                            <asp:HiddenField ID="HiddenFieldCompanyIdview" runat="server" Value='<%#Eval("CompanyId") %>' />
                                                            <asp:HiddenField ID="HiddenFieldUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                                                            <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("CompanyId") %>' />
                                                            <asp:HiddenField ID="HiddenFieldBranch" runat="server" Value='<%#Eval("BranchName") %>' />

                                                            <asp:HiddenField ID="HiddenFieldInspectionVideo" runat="server" Value='<%#Eval("InspectionVideo") %>' />

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="URL of Inspection Video">
                                                        <ItemTemplate>
                                                            <asp:HyperLink CssClass="dynRowEven" ID="hl_inspectionvideoD" runat="server" Target="_blank" NavigateUrl='<%#Eval("InspectionVideo") %>' ForeColor="Black" Text='<%#Eval("InspectionVideo") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>--%>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                    <%--            <tr>
                        <td style="height: 80px;">&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="height: 80px;">
                          
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 80px;">
                            <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="update1_DgD_agencies">
                                <ProgressTemplate>
                                    <img alt="progress" src="../Images/Admin/loading.gif" />
                                    Processing...            
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </td>
                    </tr>--%>
                </table>



                <div class="section">
                    <span>2.</span>Company Details
                </div>
                <div class="inner-wrap">
                    <div class="row" id="rowCompany" runat="server" visible="false">
                        <div class="col-md-6">
                            <span><b>Name:</b></span><asp:Label ID="lblCompanyName" runat="server" CssClass="box-warning" />
                        </div>
                        <div class="col-md-6">
                            <span><b>EmailID:</b></span><asp:Label ID="lblEmail" runat="server" CssClass="box-warning" />
                        </div>
                        <div class="col-md-6">
                            <span><b>Category:</b></span><asp:Label ID="lblCategory" runat="server" CssClass="box-warning" />
                        </div>

                        <div class="col-md-6">
                            <span><b>Mobile:</b></span><asp:Label ID="lblMobile" runat="server" CssClass="box-warning" />
                        </div>
                        <div class="col-md-6">
                            <span><b>Address:</b></span><asp:Label ID="lblAddress" runat="server" CssClass="box-warning" />
                        </div>

                    </div>
                </div>

                <div class="section">
                    <span>3.</span>Factory Details
                </div>
                <div class="inner-wrap">
                    <div class="row">
                        <asp:GridView ID="grdFactories" runat="server" AutoGenerateColumns="false" Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" CssClass="table table-striped table-bordered">
                            <Columns>
                                <asp:TemplateField HeaderText="S.NO">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit Address">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_UnitAddress" runat="server" Text='<%#Eval("UnitAddress") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Enterprise Category">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_SCategory" runat="server" Text='<%#Eval("EnterpriseCategory") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="View Inspection" Visible="true">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HdCompanyId" runat="server" Value='<%#Eval("CompanyId") %>' />
                                        <asp:HiddenField ID="HdUnitId" runat="server" Value='<%#Eval("UnitId") %>' />
                                        <asp:LinkButton ID="btnView" runat="server" Text="View" ForeColor="SkyBlue" OnCommand="btnView_Command" CommandArgument='<%#DataBinder.Eval(Container.DataItem, "CompanyId") +","+ DataBinder.Eval(Container.DataItem, "UnitId") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </div>
                </div>

                <div class="section">
                    <span>4.</span>Inspection Details
                </div>
                <div class="inner-wrap">
                    <div class="row" id="rowInspection" runat="server" visible="false">
                        <div class="col-md-12">
                            <span><b>Report:</b></span>
                            <asp:HyperLink CssClass="dynRowEven" ID="hl_Report" runat="server" Target="_blank" ForeColor="Black"></asp:HyperLink>
                        </div>
                        <div class="col-md-12">
                            <span><b>Video:</b></span><asp:HyperLink CssClass="dynRowEven" ID="hl_Video" runat="server" Target="_blank" ForeColor="Black"></asp:HyperLink>
                        </div>
                        <div class="col-md-12">
                            <span><b>Certificate:</b></span><asp:LinkButton ID="lnkCertificate" Text="Draft Certificate" runat="server" ForeColor="Black" CssClass="box-warning" Target="_blank" />
                        </div>
                        <div class="col-md-4">
                            <span><b>GeoTag Image1:</b></span><asp:HyperLink ID="btnGeoTag1" runat="server" Text="Image1" Target="_blank"></asp:HyperLink>
                        </div>
                        <div class="col-md-4">
                            <span><b>GeoTag Image2:</b></span>
                            <asp:HyperLink ID="btnGeoTag2" runat="server" Text="Image2" Target="_blank"></asp:HyperLink>
                        </div>
                        <div class="col-md-4">
                            <span><b>GeoTag Image3:</b></span>
                            <asp:HyperLink ID="btnGeoTag3" runat="server" Text="Image3" Target="_blank"></asp:HyperLink>
                        </div>
                        <div class="col-md-4">
                            <span><b>GeoTag Image4:</b></span>
                            <asp:HyperLink ID="btnGeoTag4" runat="server" Text="Image4" Target="_blank"></asp:HyperLink>
                        </div>
                        <div class="col-md-4">
                            <span><b>GeoTag Image5:</b></span>
                            <asp:HyperLink ID="btnGeoTag5" runat="server" Text="Image5" Target="_blank"></asp:HyperLink>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
