<%@ Page Language="C#" MasterPageFile="~/appMaster/Login.master" AutoEventWireup="true" CodeFile="Final_Verify.aspx.cs" Inherits="Final_Verify" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>




<asp:Content ID="Content2" ContentPlaceHolderID="placeholder" runat="Server">
    <script type="text/javascript" language="javascript">
        function FunOpenPopup950x600(x) {
            window.open(x, '', 'height=600,width=950,resizable=yes,scrollbars=yes');
            return false;
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePageMethods="true">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="update_adhar" runat="server">
        <ContentTemplate>
            <div class="wrapper wrapper-content fadeInRight ">
                <div class="container">
                    <div class="row ">
                        <div class="col-lg-7 fa-border border-size-md">
                            <div class="ibox ">
                                <div class="ibox-title">

                                    <span class="pull-right text-right">
                                        <small>Units registrered with Nsic's Single Point Registration scheme can be verified from here</small>
                                    </span>
                                    <h4 class="font-bold ">Verification of Registration Number</h4>
                                </div>
                                <div class="ibox-content">
                                    <span>Enter Organisation name(<span style="color: red">Who is Verifying GP Certificate</span>):</span>
                                    <asp:TextBox ID="txtVAgencyName" MaxLength="50" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="rfvd1" runat="server" ControlToValidate="txtVAgencyName" ErrorMessage="Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtVAgencyName" ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{5,}$" runat="server" SetFocusOnError="true" ForeColor="Red" ErrorMessage="Minimum 5 characters required."></asp:RegularExpressionValidator>

                                    <br />
                                    <span>Enter Registration number:</span>
                                    <asp:TextBox ID="txtSearchbox" runat="server" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSearchbox" ErrorMessage="Required" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>

                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-info" OnClick="btnSearch_Click" />
                                    <br />
                                     <asp:Label ID="lblMsg" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>

                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="update_adhar">
                                        <ProgressTemplate>
                                            <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>

                                </div>
                                <div class="ibox-footer">
                                    <div class="m-t-md">
                                        <strong>The purchasing agencies are advised to satisfy themselves with the store details in the certificate while doing the Technical Evaluation stage before placing the tender/ order on the units, certified by NSIC.</strong>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="col-lg-1"></div>
                        <div class="col-lg-4 fa-border border-size-sm">
                            <div class="ibox float-e-margins">
                                <div class="ibox-title">
                                    <h5>Portion where Registration number is placed in Certificate</h5>
                                </div>
                                <div class="ibox-content">
                                    <div class="row">
                                        <p>NSIC REGISTRATION NO. is present just over monetary limit area.</p>
                                        <%-- <div class="col-lg-12">
                                    <span>
                                        <img src="Images/NSICcerti.jpg" alt="Certificate" width="200" height="100" />
                                    </span>
                                </div>--%>
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                    <br />
                    <div class="row fa-border border-size-sm">


                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Details of verified unit </h5>

                            </div>
                            <div class="ibox-content">

                                <div class="table-responsive">
                                    <asp:GridView ID="gv_companies" runat="server"
                                        Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" CssClass="table table-striped table-bordered" UseAccessibleHeader="true"
                                        ShowHeaderWhenEmpty="true" OnRowCommand="gvCertificateDetails_RowCommand"
                                        AutoGenerateColumns="False" OnRowDataBound="grd_gvcompaniesonrowdatabound">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                           
                                        <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" />
                                        <EmptyDataTemplate>
                                            <strong>No Record Found, it seems unit is not registered with Single Point registration Scheme of NSIC.</strong>
                                        </EmptyDataTemplate>
                                        <Columns>


                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("Compnay") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Company Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_company_address" runat="server" Text='<%#Eval("Address") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Registration No">
                                                <ItemTemplate>
                                                    <table border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_new_registration_number" runat="server" Text='<%#Eval("RegistrationNo")%>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_old_registration_number" runat="server" Text='<%#Eval("Old_RegistrationNo") %>'></asp:Label>

                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_old_Validity" runat="server" Text='<%#Eval("Old_RegistrationNo") %>'></asp:Label>

                                                            </td>
                                                        </tr>

                                                    </table>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Validity">
                                                <ItemTemplate>
                                                    <table border="0">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_validity" runat="server" Text='<%#Eval("validity") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <i>
                                                                    <asp:Label ID="lbl_validity_text" runat="server" Text="Last Validity of Cerificate was upto " Visible="false"></asp:Label></i>
                                                                <asp:Label ID="lbl_last_validity_text" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lbl_registratondate" runat="server" Text='<%#Eval("RegistrationDate") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_provisinol_fresh" runat="server" Text='<%#Eval("fresh_provisional") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lbl_renewal_date" runat="server" Text='<%#Eval("RenewalDate_Check") %>' Visible="false"> </asp:Label>
                                                                <asp:Label ID="lbl_Accessid_type" runat="server" Text='<%#Eval("Accessid")%>' Visible="false"></asp:Label>
                                                                <asp:Label ID="Lbl_last_validity" runat="server" Text='<%#Eval("last_validity")%>' Visible="false"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Products">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_product_name" runat="server" Text='<%#Eval("Product")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View Certificate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbnDraftPrint" runat="server" CommandName="verifycertificate"
                                                        CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="View Certificate">
                                                        Certificate
                                                         <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                                    </asp:LinkButton>
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
        </ContentTemplate>

    </asp:UpdatePanel>

</asp:Content>










<%--</html>--%>
