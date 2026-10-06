<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="AnnualTargetUpdate.aspx.cs" Inherits="appAdministrator_AnnualTargetUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnlOuts" runat="server" CssClass="outerPopup">
                <asp:Panel ID="pnlInners" runat="server" Width="600px" CssClass="innerPopup">
                    <center>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width: 100%;">
                                <td valign="top" align="left" style="width: 100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top">
                                            <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px; padding-right: 5px;">
                                                <span style="font-size: large">Update Annual Target of Branches for Financial Year</span>

                                            </td>
                                        </tr>
                                        <%-- <tr align="left" valign="top" style="width: 100%;">
                                    <td colspan="2" align="left" valign="top">&nbsp;
                                        Annual Target of Branhes for Financial Year
                                    </td>
                                </tr>--%>
                                        <tr valign="top" align="center">
                                            <td align="left" valign="top" style="width: 100px;" class="updateInnerColumn">Zone :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">

                                                <asp:DropDownList TabIndex="1" ID="ddlZone" runat="server" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                                                    CssClass="clsDDL" AutoPostBack="true">
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ValidationGroup="1" ID="RFVZone" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Please Select Zone" InitialValue="0" ControlToValidate="ddlZone"></asp:RequiredFieldValidator>

                                            </td>
                                        </tr>
                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Brach Name :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:DropDownList TabIndex="2" ID="ddlBranch" runat="server" CssClass="clsDDL"></asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ValidationGroup="1" ID="RFVBranch" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Please Select Branch" InitialValue="0" ControlToValidate="ddlBranch"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Financial Year :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:DropDownList TabIndex="3" ID="ddlFY" runat="server" CssClass="clsDDL" AutoPostBack="true" OnSelectedIndexChanged="ddlFY_SelectedIndexChanged">
                                                    <asp:ListItem Value="0">--Select FY--</asp:ListItem>
                                                    <asp:ListItem Value="1">FY 2025-26</asp:ListItem>
                                                </asp:DropDownList>
                                                <br />
                                                <asp:RequiredFieldValidator ValidationGroup="1" ID="RFVFY" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Please Select Financial Year" InitialValue="0" ControlToValidate="ddlFY"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>


                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Annual Target(<span style="color: red"> In Lacs</span>):
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:TextBox TabIndex="4" ID="txtAnnualTarget" runat="server" CssClass="txt_bxUpper"></asp:TextBox>

                                                <br />
                                                <asp:RequiredFieldValidator ValidationGroup="1" ID="RequiredFieldValidator32" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Please provide Annual Target" ControlToValidate="txtAnnualTarget"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:RegularExpressionValidator ValidationGroup="1" ID="revInteger" runat="server"
                                                    ControlToValidate="txtAnnualTarget"
                                                    ValidationExpression="^\d+(\.\d{1,3})?$"
                                                    ErrorMessage="Please enter a valid number (e.g., 10, 0.5, .75). Only three digit after decimal will be allowed"
                                                    ForeColor="Red"
                                                    Display="Dynamic" />
                                            </td>
                                        </tr>




                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn"></td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="1"
                                                    CssClass="btnCls" OnClick="btnSubmit_Click" />
                                                &nbsp;&nbsp;
                                     <input id="btnResets" type="button" value="Reset" tabindex="9" class="btnCls" onclick="return FunResetBranch();" />
                                            </td>
                                        </tr>



                                        <tr align="left" valign="top" style="padding-top: 50px">
                                            <td colspan="2" style="padding-top: 50px">

                                                <table style="padding-top: 10px" border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                                                    <tr align="left" valign="top" style="width: 100%;">
                                                        <td align="left" valign="top" style="width: 100%;">
                                                            <%--OnRowDataBound="gvBranch_RowDataBound"  OnRowCommand="gvBranch_RowCommand"--%>
                                                            <asp:GridView ID="grd_report" runat="server" CellPadding="5"
                                                                CellSpacing="1" GridLines="Both" AutoGenerateColumns="false" Width="96%"
                                                                BorderWidth="0" HeaderStyle-VerticalAlign="Middle" FooterStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Center" ShowFooter="false" AllowPaging="false"
                                                                OnRowDataBound="gvBranch_RowDataBound"
                                                                OnRowCommand="gvBranch_RowCommand"
                                                                EmptyDataText="No data found!">
                                                                <HeaderStyle CssClass="dynRowHeader" />
                                                                <RowStyle CssClass="dynRowEven" />
                                                                <AlternatingRowStyle CssClass="dynRowOdd" />
                                                                <FooterStyle CssClass="dynRowHeader" />

                                                                <Columns>

                                                                    <asp:TemplateField HeaderText="Zone">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Zonename" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>

                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Branch">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_Branch_name" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                                                            <asp:HiddenField ID="hdn_branch_id" runat="server" Value='<%#Eval("BranchID") %>' />
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Financial Year">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbl_register_Company" runat="server" Text='<%#Eval("FinancialYear") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>



                                                                    <asp:TemplateField HeaderText="Annual Target ">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblAnnualBudget" runat="server" Text='<%#Eval("AnnualBudget") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>


                                                                    <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                                                CommandName="edt" CommandArgument='<%# Eval("BranchID") + "|" + Eval("FYIds")  %>' ToolTip="Edit Annual Budget" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>



                                                                </Columns>


                                                            </asp:GridView>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>
                        </table>




                    </center>
                </asp:Panel>
            </asp:Panel>




            <asp:Panel ID="pnlOut" runat="server" Style="display: none;" CssClass="outerPopup">
                <asp:Panel ID="pnlInner" runat="server" Width="550px" CssClass="innerPopup">
                    <center>
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width: 100%;">
                                <td valign="top" align="left" style="width: 100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top">
                                            <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px; padding-right: 5px;">
                                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                        <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                                ID="lblBranchName" runat="server" CssClass="header"></asp:Label>
                                                            <asp:HiddenField ID="hdnBranchIdEdit" runat="server" />
                                                            <asp:HiddenField ID="hdnFYIdEdit" runat="server" />
                                                        </td>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr align="left" valign="top" style="width: 100%;">
                                            <td colspan="2" align="right" valign="top">&nbsp;
                                            </td>
                                        </tr>
                                        <tr valign="top" align="center">
                                            <td align="left" valign="top" style="width: 100px;" class="updateInnerColumn">Zone :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Label ID="lblZoneDisplay" runat="server" CssClass="txt_bx"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Brach Name :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Label ID="lblBranchDisplay" runat="server" CssClass="txt_bx"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Financial Year :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Label ID="lblFYDisplay" runat="server" CssClass="txt_bx"></asp:Label>
                                            </td>
                                        </tr>

                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn">Annual Budget:
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">Existing Annual Target(<span style="color: red"> In Lacs</span>) (
                                                <asp:Label ID="AnnualBudgetDisplay" runat="server" ForeColor="Red" Font-Bold="true" Font-Size="Larger"></asp:Label>)
                                                <asp:TextBox ID="txtAnnualBudgetEdit" runat="server" CssClass="txt_bx"></asp:TextBox>
                                                <asp:RequiredFieldValidator ValidationGroup="11" ID="RequiredFieldVaslidator1" runat="server" SetFocusOnError="true"
                                                    Display="Dynamic" ErrorMessage="Please provide Annual Target" ControlToValidate="txtAnnualBudgetEdit"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:RegularExpressionValidator ValidationGroup="11" ID="RegularExpresssionValidator1" runat="server"
                                                    ControlToValidate="txtAnnualBudgetEdit"
                                                    ValidationExpression="^\d+(\.\d{1,3})?$"
                                                    ErrorMessage="Please enter a valid number (e.g., 10, 0.5, .75). Only three digit after decimal will be allowed"
                                                    ForeColor="Red"
                                                    Display="Dynamic" />
                                            </td>
                                        </tr>

                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" colspan="2" style="height: 10px;"></td>
                                        </tr>


                                        <tr align="left" valign="top">
                                            <td align="left" valign="top" colspan="2" style="height: 10px;"></td>
                                        </tr>


                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" class="updateInnerColumn"></td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Button ID="Button1" runat="server" Text="Submit" TabIndex="8" ValidationGroup="11"
                                                    CssClass="btnCls" OnClick="btnSubmitEdit_Click" />
                                                &nbsp;&nbsp;
                                      
                                            </td>

                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:Button ID="Button2" runat="server" Text="Cencel" TabIndex="9"
                                                    CssClass="btnCls" OnClick="btnCancel_Click" />


                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>

                    </center>
                </asp:Panel>
            </asp:Panel>


            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
                TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

