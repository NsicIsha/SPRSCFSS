<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" EnableEventValidation="true"
    CodeFile="Edit_Fees.aspx.cs" Inherits="appAdministrator_Admin_Edit_ValidityDate" Title="Edit validity date" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" language="javascript">
  
        function changeddropdown() {
            var k = document.getElementById('<%=ddl_type_ref_pan.ClientID %>').value;
            var j = $("#<%=txt_panno.ClientID %>").text();

            // alert(k);
            if (k == 1) {
                $("#ctl00_ContentPlaceHolder1_lbl_pan_change").text("PAN NO :");
                $("#<%=txt_panno.ClientID %>").attr("maxlength", "10");


            }
            else if (k == 2) {

                $("#ctl00_ContentPlaceHolder1_lbl_pan_change").text("REF NO :");
                $("#<%=txt_panno.ClientID%>").attr("maxlength", "50");

            }
            else if (k == 3) {
                $("#ctl00_ContentPlaceHolder1_lbl_pan_change").text("Old Reg No :");
                $("#<%=txt_panno.ClientID%>").attr("maxlength", "50");
            }
            else if (k == 4) {
                $("#ctl00_ContentPlaceHolder1_lbl_pan_change").text("New Reg No :");
                $("#<%=txt_panno.ClientID%>").attr("maxlength", "50");
            }
            else if (k == 5) {
                $("#ctl00_ContentPlaceHolder1_lbl_pan_change").text("UDYAM NO :");
                $("#<%=txt_panno.ClientID %>").attr("maxlength", "19");

            }
            callservermethod();
        }

        function callservermethod() {
            var Companyid = $("#ctl00_ContentPlaceHolder1_lbl_company_id_onpnl").val();
            var type = document.getElementById('<%=ddl_type_ref_pan.ClientID %>').value
            PageMethods.Bindtexbox(Companyid, type, OnSuccess, OnFailure);
        }
        function OnSuccess(result) {

            if (result != "") {
                //  alert(result);
                var h = result.toString();


                $("#<%=txt_panno.ClientID %>").val(h);
            }
            else {
                $("#<%=txt_panno.ClientID %>").val("");
            }
        }
        function OnFailure(error) {
            $("#<%=txt_panno.ClientID %>").val("");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hid_comapnyId" />
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll"></asp:Label>
        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
        </div>
        <div style="height: 10px;">
        </div>

        <div align="left" class="clsrowdiv">
            <div class="leftext">Search</div>
            <div class="coloncls">
                <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                    <asp:ListItem  Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Selected="True" Text="PAN" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Registration No." Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="txtdiv">
                <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox>
            </div>
            <div class="btndiv">
                <asp:Button ID="btnSearch" runat="server" Text="Search"
                    CssClass="btnClsdivSearch" OnClick="btnSearch_Click" />
            </div>
        </div>
        <div style="height: 10px;">
        </div>
        <span><b>COMPANY DETAILS</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="gv_company" runat="server" AutoGenerateColumns="False"
                Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="1"
                AllowPaging="True" PageIndex="0"
                AllowSorting="True" OnRowCommand="gv_company_RowCommand"
                OnSelectedIndexChanged="gv_company_SelectedIndexChanged"
                OnSelectedIndexChanging="gv_company_SelectedIndexChanging"
                OnPageIndexChanging="gv_company_PageIndexChanging"
                OnRowDataBound="gv_company_RowDataBound1">
                <EmptyDataTemplate>
                    <div align="center"><strong style="font-size: 18px; font-weight: bold; color: Red;">Record not found!!!</strong></div>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                <Columns>
                    <asp:TemplateField HeaderText="S.No.">
                        <ItemTemplate>
                            <%#Container.DataItemIndex+1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="fGPtbl_Company_Id" HeaderText="Company ID" />
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />
                    <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                    <asp:BoundField DataField="Registartionvalidity" HeaderText="Registration Date/ Validity " />
                    <asp:BoundField DataField="fGtbl_Company_AdharNumber" HeaderText="UAM/UDYAM" />
                    <asp:TemplateField HeaderText="Edit Validity" Visible="false"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbnDraftPrint" runat="server" CommandName="editvalidity"
                                CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Edit Validity">Edit Validity
                            </asp:LinkButton>
                            <asp:HiddenField runat="server" ID="hdn_chkrenewaed" Value='<%#Eval("oncerenewed") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit PAN No"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lblchangepan" runat="server" CommandName="editpan"
                                CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Edit PAN No">Edit PAN No
                            </asp:LinkButton>

                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit company name"
                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_companyname" runat="server" CommandName="editCompany"
                                CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Editcompany Name">Edit Company name                

                            </asp:LinkButton>

                        </ItemTemplate>
                    </asp:TemplateField>


                </Columns>
            </asp:GridView>

        </div>

        <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
            <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
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
                                    <tr valign="top" align="left">
                                        <td valign="top" align="left" style="width: 100px;" class="updateInnerColumn">RegistartionDate:
                                        </td>
                                        <td valign="top" align="left" class="updateInnerColumn">
                                            <asp:TextBox ID="txt_registartiondate" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CExtendertxt_registartiondate" runat="server" PopupButtonID="txt_registartiondate"
                                                TargetControlID="txt_registartiondate">
                                            </cc1:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="req_validitydate" runat="server" ControlToValidate="txt_registartiondate"
                                                SetFocusOnError="true" ErrorMessage="Please Fill Registartion Date" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>

                                            <cc1:ValidatorCalloutExtender ID="Vc_registarion" TargetControlID="req_validitydate"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>

                                        </td>
                                    </tr>
                                    <tr valign="top" align="left">
                                        <td align="left" valign="top" style="height: 10px;"></td>
                                    </tr>
                                    <tr valign="top" align="center">
                                        <td valign="top" align="left"></td>
                                        <td valign="top" align="left" class="updateInnerColumn">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="3" ValidationGroup="1" CssClass="btnCls"
                                                OnClick="btnSubmit_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>


        <asp:Panel ID="pnlpanOut" runat="server" CssClass="outerPopup">
            <asp:Panel ID="pnlpanInner" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton1" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="lblmsg" CssClass="red_link"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">

                                            <asp:HiddenField ID="lbl_company_id_onpnl" runat="server" />
                                            Change Type:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddl_type_ref_pan" runat="server" CssClass="clsDDL" onChange="javascript:return changeddropdown();">
                                                <asp:ListItem Selected="True" Value="1" Text="PAN NO"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Ref NO"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Old Reg No"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="New Reg No"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="Update Udyam"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr valign="top" align="left">
                                        <td valign="top" align="left" style="width: 100px;" class="updateInnerColumn">
                                            <b>
                                                <asp:Label ID="lbl_pan_change" runat="server" Text="PAN NO :"></asp:Label></b>
                                        </td>
                                        <td valign="top" align="left" class="updateInnerColumn">
                                            <asp:TextBox ID="txt_panno" runat="server" MaxLength="10" TabIndex="1" CssClass="txt_bx"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="req_pan" runat="server" ControlToValidate="txt_panno"
                                                SetFocusOnError="true" ErrorMessage="Please Fill PAN/REF No/Old Reg no/New Reg No" Display="None" ValidationGroup="2"></asp:RequiredFieldValidator>

                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="req_pan"
                                                runat="server">
                                            </cc1:ValidatorCalloutExtender>

                                        </td>
                                    </tr>
                                    <tr valign="top" align="left">
                                        <td align="left" valign="top" style="height: 10px;"></td>
                                    </tr>
                                    <tr valign="top" align="center">
                                        <td valign="top" align="left"></td>
                                        <td valign="top" align="left" class="updateInnerColumn">
                                            <asp:Button ID="btnSubmitpan" runat="server" Text="Submit" TabIndex="3"
                                                ValidationGroup="2" CssClass="btnCls" OnClick="btnSubmitpan_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>


        <asp:Panel ID="Panel_company" runat="server" CssClass="outerPopup">
            <asp:Panel ID="Panel_companychild" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton2" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="lbl_company" CssClass="red_link"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">COMPANY NAME  :
                                       <input type="hidden" id="hdncompanyId_Pop" runat="server" value="0" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txt_company_name" runat="server" Text=""></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="req_company_name" runat="server" ControlToValidate="txt_company_name" SetFocusOnError="true"
                                                ErrorMessage="Please Fill Company name" Display="None" ValidationGroup="3"></asp:RequiredFieldValidator>
                                            <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="req_company_name" runat="server">
                                            </cc1:ValidatorCalloutExtender>
                                        </td>
                                    </tr>


                                    <tr valign="top" align="center">
                                        <td valign="top" align="left"></td>
                                        <td valign="top" align="left" class="updateInnerColumn">
                                            <asp:Button ID="btn_CompanyName" runat="server" Text="Submit" TabIndex="3"
                                                ValidationGroup="3" CssClass="btnCls" OnClick="btn_CompanyName_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>



        <asp:Panel ID="Panel_FactAdd" runat="server" CssClass="outerPopup">
            <asp:Panel ID="Panel2" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton3" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="Label1" CssClass="red_link"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">Factory address  :
                                       <input type="hidden" id="Hidden1" runat="server" value="0" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtNewFacAdd" runat="server" TextMode="MultiLine" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <caption>
                                        <br />
                                        <tr>
                                            <td></td>
                                            <td><%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNewFacAdd" SetFocusOnError="true"
                                                ErrorMessage="Please Fill Factory Address" Display="None" ValidationGroup="3"></asp:RequiredFieldValidator>--%></td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top" class="updateInnerColumn">Pincode:
                                                <asp:TextBox ID="txtPincode" runat="server" MaxLength="6" Text=""></asp:TextBox>
                                            </td>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPincode" SetFocusOnError="true"
                                                ErrorMessage="Please add Pincode" Display="None" ValidationGroup="3"></asp:RequiredFieldValidator>--%>
                                        </tr>
                                        <caption>
                                            <br />
                                            <tr>
                                                <td align="left" valign="top" class="updateInnerColumn">ID:<asp:TextBox ID="txtCUID" runat="server" ReadOnly="true" Style="background-color: lightgray" Text="0" />
                                                </td>
                                                <td align="left" valign="top" class="updateInnerColumn">Type:<asp:TextBox ID="txtType" runat="server" ReadOnly="true" Style="background-color: lightgray" Text="0" />
                                                </td>
                                            </tr>
                                            <caption>
                                                <br />
                                                <tr>
                                                    <td align="left" valign="top" class="updateInnerColumn"><b>State:</b>
                                                        <asp:DropDownList ID="ddlState_a3" runat="server" AutoPostBack="True" Style="background-color: white" OnSelectedIndexChanged="ddlState_a3_SelectedIndexChanged" TabIndex="4" ValidationGroup="3">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="None"
                                                                ErrorMessage="Please Select State" ControlToValidate="ddlState_a3"
                                                                SetFocusOnError="true" ValidationGroup="3"></asp:RequiredFieldValidator>--%>

                                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="None"
                                                                ErrorMessage="Please Select State" ControlToValidate="ddlCity_a4"
                                                                SetFocusOnError="true" ValidationGroup="3"></asp:RequiredFieldValidator>--%>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top" class="updateInnerColumn"><b>City:</b>
                                                        <asp:DropDownList ID="ddlCity_a4" runat="server" Style="background-color: white" TabIndex="5" ValidationGroup="3">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr align="center" valign="top">
                                                    <td align="center" valign="top"></td>
                                                    <td align="center" class="updateInnerColumn" valign="top">
                                                        <asp:Button ID="btnFactAdd" runat="server" CssClass="btnCls" OnClick="btnFactAdd_Click" TabIndex="3" Text="Submit" />
                                                        &nbsp;&nbsp;&nbsp; </td>
                                                </tr>
                                            </caption>
                                        </caption>
                                    </caption>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>

        <cc1:ModalPopupExtender ID="ModalPopupExtender4" runat="server" PopupControlID="Panel_FactAdd"
            TargetControlID="lnkFactory" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender4" TargetControlID="Panel2"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>

        <asp:LinkButton ID="lnkFactory" runat="server" Style="display: none;"></asp:LinkButton>

        <asp:Panel ID="PanelFees" runat="server" CssClass="outerPopup">
            <asp:Panel ID="Panel3" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton5" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="Label3" CssClass="red_link"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">Unit Fees:
                                       <input type="hidden" id="hdFeeID" runat="server" value="0" />
                                       <%-- </td>
                                        <td align="left" valign="top">--%>
                                            <asp:TextBox ID="txtUnitFees" runat="server" Text="" AutoPostBack="true"  OnTextChanged="txtUnitST_TextChanged"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <caption>

                                        <tr>
                                            <td align="left" valign="top" class="updateInnerColumn">Unit SeviceTax:
                                                <asp:TextBox ID="txtUnitST" runat="server" MaxLength="6"></asp:TextBox>
                                            </td>

                                        </tr>
                                    </caption>
                                    <caption>

                                        <tr align="center" valign="top">
                                            <td align="center" valign="top"></td>
                                            <td align="center" class="updateInnerColumn" valign="top">
                                                <asp:Button ID="btnFeesUpdate" runat="server" CssClass="btnCls" OnClick="btnFeesUpdate_Click" TabIndex="3" Text="Submit" />
                                                &nbsp;&nbsp;&nbsp; </td>
                                        </tr>
                                    </caption>
                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>

        <cc1:ModalPopupExtender ID="ModalPopupExtender6" runat="server" PopupControlID="PanelFees"
            TargetControlID="lnkFees" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender6" TargetControlID="Panel3"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>

        <asp:LinkButton ID="lnkFees" runat="server" Style="display: none;"></asp:LinkButton>

        <asp:Panel ID="PanelTurnover" runat="server" CssClass="outerPopup">
            <asp:Panel ID="Panel4" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton6" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="Label4" CssClass="red_link"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">Turnover:
                                       <input type="hidden" id="hdTurnoverId" runat="server" value="0" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtTurnover" runat="server" Text=""></asp:TextBox>
                                        </td>
                                    </tr>

                                    <tr align="center" valign="top">
                                        <td align="center" valign="top"></td>
                                        <td align="center" class="updateInnerColumn" valign="top">
                                            <asp:Button ID="btnturnover" runat="server" CssClass="btnCls" OnClick="btnturnover_Click" TabIndex="3" Text="Submit" />
                                            &nbsp;&nbsp;&nbsp; </td>
                                    </tr>

                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>

        <cc1:ModalPopupExtender ID="ModalPopupExtender7" runat="server" PopupControlID="PanelTurnover"
            TargetControlID="lnkTurnover" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender7" TargetControlID="Panel4"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>

        <asp:LinkButton ID="lnkTurnover" runat="server" Style="display: none;"></asp:LinkButton>

        <asp:Panel ID="PanelDirectors" runat="server" CssClass="outerPopup">
            <asp:Panel ID="PanelID" runat="server" Width="400px" CssClass="innerPopup">
                <center>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr valign="top" align="left" style="width: 100%;">
                            <td valign="top" align="left" style="width: 100%;">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top" align="left" style="width: 100%">
                                        <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width: 100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">&nbsp;&nbsp;</td>
                                                    <td align="right" valign="top" style="width: 20px;">
                                                        <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton4" runat="server"
                                                            ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>

                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td colspan="2" align="center" valign="top">
                                            <asp:Label runat="server" ID="Label2" CssClass="red_link"></asp:Label>
                                        </td>
                                        <asp:HiddenField ID="hdDirectorID" runat="server" />
                                    </tr>

                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">Director Name  :                                     
                                            <asp:TextBox ID="txtDirName" runat="server" TextMode="MultiLine" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr align="left" valign="top">
                                        <td align="left" valign="top" class="updateInnerColumn">Director address  :                                     
                                            <asp:TextBox ID="txtDirAddress" runat="server" TextMode="MultiLine" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" class="updateInnerColumn">Pincode:
                                                <asp:TextBox ID="txtDirPincode" runat="server" MaxLength="6" Text=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" class="updateInnerColumn"><b>State:</b>
                                            <asp:DropDownList ID="ddlDirState" runat="server" AutoPostBack="True" Style="background-color: white" OnSelectedIndexChanged="ddlDirState_SelectedIndexChanged" TabIndex="4">
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" class="updateInnerColumn"><b>City:</b>
                                            <asp:DropDownList ID="ddlDirCity" runat="server" Style="background-color: white" TabIndex="5">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr align="center" valign="top">
                                        <td align="center" valign="top"></td>
                                        <td align="center" class="updateInnerColumn" valign="top">
                                            <asp:Button ID="btnDirAdd" runat="server" CssClass="btnCls" OnClick="btnDirAdd_Click" TabIndex="3" Text="Submit" />
                                            &nbsp;&nbsp;&nbsp; </td>
                                    </tr>


                                </table>
                            </td>
                        </tr>
                    </table>
                </center>

            </asp:Panel>
        </asp:Panel>

        <cc1:ModalPopupExtender ID="ModalPopupExtender5" runat="server" PopupControlID="PanelDirectors"
            TargetControlID="lnkDirectors" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender5" TargetControlID="PanelID"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>
        <asp:LinkButton ID="lnkDirectors" runat="server" Style="display: none;"></asp:LinkButton>

        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
            TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>
        <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>


        <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="pnlpanOut"
            TargetControlID="LinkButton2" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>



        <cc1:RoundedCornersExtender ID="RoundedCornersExtender2" TargetControlID="pnlpanInner"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>
        <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" PopupControlID="Panel_company"
            TargetControlID="LnkcompanyButton" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
        </cc1:ModalPopupExtender>
        <cc1:RoundedCornersExtender ID="RoundedCornersExtender3" TargetControlID="Panel_companychild"
            runat="server" BorderColor="#22518F" Radius="10" Corners="All">
        </cc1:RoundedCornersExtender>
        <asp:LinkButton ID="LinkButton2" runat="server" Style="display: none;"></asp:LinkButton>

        <asp:LinkButton ID="LnkcompanyButton" runat="server" Style="display: none;"></asp:LinkButton>
        <br />
        <asp:RadioButtonList ID="rdbFeeType" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdbFeeType_SelectedIndexChanged" RepeatDirection="Horizontal">
            <asp:ListItem Text="Fresh" Value="0" />
            <asp:ListItem Text="Renewal" Value="1" />
            <asp:ListItem Text="RenAmend" Value="2" />
            <asp:ListItem Text="Amendment" Value="3" />
        </asp:RadioButtonList>
        <asp:HiddenField ID="hdValue" runat="server" />
        <span id="s_Fresh" runat="server"><b>Fresh</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grdFresh" runat="server" AutoGenerateColumns="false" Visible="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_Company_Id" HeaderText="Company_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company_Name" />
                    <asp:BoundField DataField="fGPtbl_Company_Status" HeaderText="Status" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN" />
                    <asp:BoundField DataField="fGPtbl_Company_Display" HeaderText="Display" />
                    <asp:BoundField DataField="fGPtbl_Company_Delete" HeaderText="Delete" />
                    <asp:BoundField DataField="fGPtbl_Company_EDate" HeaderText="Edate" />
                </Columns>
            </asp:GridView>
        </div>

        <span id="span2" runat="server"><b>Company Renewal</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grdRenewal" runat="server" AutoGenerateColumns="false" Visible="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_Company_Renewal_Id" HeaderText="Amendment_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_Id" HeaderText="Company_Id" />
                     <asp:BoundField DataField="fGPtbl_PD_Id" HeaderText="fGPtbl_PD_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company_Name" />
                    <asp:BoundField DataField="fGPtbl_Company_Status" HeaderText="Status" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN" />
                    <asp:BoundField DataField="fGPtbl_Company_Display" HeaderText="Display" />
                    <asp:BoundField DataField="fGPtbl_Company_Delete" HeaderText="Delete" />
                    <asp:BoundField DataField="fGPtbl_Company_RenewalType" HeaderText="Ren_Type" />
                    <asp:BoundField DataField="fGPtbl_Company_EDate" HeaderText="Edate" />
                </Columns>
            </asp:GridView>
        </div>



        <span id="spanCompAmendment" runat="server"><b>Company Amendment</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grdCompAmendment" runat="server" AutoGenerateColumns="false" Visible="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_Company_Amendment_Id" HeaderText="Amendment_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_Id" HeaderText="Company_Id" />
                     <asp:BoundField DataField="fGPtbl_PD_Id" HeaderText="fGPtbl_PD_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company_Name" />
                    <asp:BoundField DataField="fGPtbl_Company_Status" HeaderText="Status" />
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN" />
                    <asp:BoundField DataField="fGPtbl_Company_Display" HeaderText="Display" />
                    <asp:BoundField DataField="fGPtbl_Company_Delete" HeaderText="Delete" />
                    <asp:BoundField DataField="fGPtbl_Company_EDate" HeaderText="Edate" />
                    <asp:BoundField DataField="fGPtbl_Company_Ren_Amnd_Type" HeaderText="Ren_Amnd_Type" />
                </Columns>
            </asp:GridView>
        </div>
        <span id="spanFactory" runat="server" visible="false"><b>FACTORY DETAILS</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grdFactory" runat="server" OnRowDataBound="grdFactory_RowDataBound" AutoGenerateColumns="false" Visible="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_CUnit_Id" HeaderText="CunitID" />
                    <asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" />
                    <asp:BoundField DataField="fGPtbl_CUnit_Enterprise" HeaderText="Enterprise Type" />
                    <asp:BoundField DataField="UnitType" HeaderText="Unit Size" />
                    <asp:TemplateField HeaderText="Check Fees">
                        <ItemTemplate>
                            <asp:LinkButton ID="AddM" runat="server" CommandName="AddM"
                                CommandArgument='<%#Eval("fGPtbl_CUnit_Id") %>' OnCommand="AddM_Command" CausesValidation="false">Show</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <br />



        <span id="span1" runat="server"><b>Fees Details</b></span>
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="grdfees" runat="server" AutoGenerateColumns="false" Visible="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_PD_Id" HeaderText="PD_Id" />
                    <asp:BoundField DataField="fGPtbl_PD_FormtypeId" HeaderText="FormTypeID" />
                    <asp:BoundField DataField="fGPtbl_PD_UnitId" HeaderText="UnitID" />
                    <asp:BoundField DataField="fGPtbl_PD_AgencyId" HeaderText="Agency_ID" />
                    <asp:BoundField DataField="fGPtbl_Company_Amendment_Id" HeaderText="Amendment_Id" />
                    <asp:BoundField DataField="fGPtbl_PD_UnitAmount" HeaderText="UnitAmt" />
                    <asp:BoundField DataField="fGPtbl_PD_UnitServiceTax" HeaderText="UnitTax" />
                    <asp:BoundField DataField="fGPtbl_PD_AgencyAmount" HeaderText="AgencyAmt" />
                    <asp:BoundField DataField="fGPtbl_PD_AgencyServiceTax" HeaderText="AgencySrvTax" />
                    <asp:TemplateField HeaderText="Add Director">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkFees" runat="server" CommandName="EditFees"
                                CommandArgument='<%#Eval("fGPtbl_PD_Id") %>' OnCommand="lnkEditFees_Command" CausesValidation="false">Edit fees</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <asp:GridView ID="grdTurnover" runat="server" AutoGenerateColumns="false">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                <Columns>
                    <asp:BoundField DataField="fGPtbl_Company_TO_Id" HeaderText="Turnover_Id" />
                    <asp:BoundField DataField="fGPtbl_Company_TO_FormTypeId" HeaderText="FormTypeID" />
                    <asp:BoundField DataField="fGPtbl_Company_TO_EDate" HeaderText="Edate" />
                    <asp:BoundField DataField="fGPtbl_Company_TO_Display" HeaderText="Display" />
                    <asp:BoundField DataField="fGPtbl_Company_TO_Delete" HeaderText="Delete" />
                    <asp:TemplateField HeaderText="Add Director">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkTurnover" runat="server" CommandName="EditTurnover"
                                CommandArgument='<%#Eval("fGPtbl_Company_TO_Id") %>' OnCommand="lnkTurnover_Command" CausesValidation="false">Edit Turnover</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>

