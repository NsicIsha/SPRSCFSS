<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Login.master" AutoEventWireup="true" CodeFile="GP_Application_Status.aspx.cs" Inherits="GP_Application_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

 


<asp:Content ID="Content2" ContentPlaceHolderID="placeholder" runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300" EnablePageMethods="true"  >
        </asp:ToolkitScriptManager>
     <asp:UpdatePanel ID="update_adhar" runat="server">
                                                <ContentTemplate>
    <div class="wrapper wrapper-content fadeInRight ">
        <div class="container">
            <div class="row ">
                <div class="col-lg-7 fa-border border-size-md">
                    <div class="ibox ">
                        <div class="ibox-title">

                           
                            <h4 class="font-bold ">Check Application Status</h4>
                        </div>
                        <div class="ibox-content">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" class="style4">STEP 1 of 7
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td colspan="3" align="right" valign="top" style="height: 10px;"> 
                                        </td>
                                    </tr>


                                    <tr align="left" valign="top" width="100%" id="trMSMEDBSEARCH" runat="server">
                                        <td colspan="3" style="padding: 50px">
                                               <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                   <td align="left" valign="top" class="leftHeading">
                                                        <span class="MandatoryField">*</span>उद्योग आधार संख्या/Udyog Aadhar Number: 
                                                        <asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="validate" />

                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading">
                                                         <span class="MandatoryField">*</span>पैन कार्ड संख्या/ PAN Card Number:   
                                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" style="text-transform:uppercase;" />
                                                        <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="validate" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPan" 
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="^[a-zA-Z]{5}[0-9]{4}[a-zA-Z]{1}$" ValidationGroup="validate"></asp:RegularExpressionValidator>
                                                    </td>
                                                    <td align="left" valign="top" class="leftHeading">
                                                        <asp:Button ID="btnf" runat="server" Text="Check Status" CssClass="btnCls" OnClick="btnf_Click" ValidationGroup="validate" />

                                                        <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel3">

                                                            <ProgressTemplate>
                                                                <img alt="progress" src="../Images/loader.gif" width="22" height="22" border="0" />
                                                                Processing...            
                                                            </ProgressTemplate>

                                                        </asp:UpdateProgress>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                        </ContentTemplate>
                                                   </asp:UpdatePanel>
                                        </td>
                                    </tr>


                                </table>
                            


                         <%--   <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-info" OnClick="btnSearch_Click" />
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="update_adhar">
                                                                <ProgressTemplate>
                                                                    <img alt="wait..." src="../Images/Admin/wait.gif" width="22" height="22" border="0" />
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>--%>

                        </div>
                         
                    </div>

                </div>
                
                

            </div>
            <br />
            <div class="row fa-border border-size-sm">

           
                    <div class="ibox float-e-margins">
                        <div class="ibox-title">
                            <h5>GP Application Status </h5>

                        </div>
                        <div class="ibox-content">

                            <div class="table-responsive">
                                <asp:GridView ID="gv_companies" runat="server"
                                    Width="100%" BorderWidth="0px" CellPadding="3" CellSpacing="3" CssClass="table table-striped table-bordered" UseAccessibleHeader="true"
                                    ShowHeaderWhenEmpty="true"
                                    AutoGenerateColumns="False"  >
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
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
                                                                <asp:Label ID="lbl_validity_text" runat="server" Text="Last Validity of Cerificate was upto " Visible="true"></asp:Label></i>
                                                            <asp:Label ID="lbl_last_validity_text" runat="server" ></asp:Label>

                                                             
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