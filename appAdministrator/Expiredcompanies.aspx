<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Expiredcompanies.aspx.cs" Inherits="appAdministrator_Expiredcompanies" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .txtlbl {
            height: 20px;
            width: 20%;
            float: right;
            white-space: nowrap;
            vertical-align: top;
        }
    </style>

    <script type="text/javascript" language="javascript">
        function mydt() {
            var now = new Date();
            var Target_date = new Date(now.setMonth(now.getMonth() - 24)).format("MM/dd/yyyy");
            // alert(Target_date);
            var val = $("#<%=txt_registartiondate.ClientID%>");
     //   var val = $("#<%=txt_registartiondate.ClientID%>").Val();
        //   var dateString = new Date(val).format("MM/dd/yyyy");
        var inputdate = val.val();
        if (new Date(inputdate) > new Date(Target_date)) {
            $("#<%=txt_registartiondate.ClientID%>").val('');
                alert('You have entered an invalid Validity Start Date. \n System will accept only the date 2 years prior to the curent date.');
            }


        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hdn_show" Value="0" />
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Expired Company List "></asp:Label>
        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>

             <asp:Label ID="lblMsgBlacklisted" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>

        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="left" class="clsrowdiv">
            <div class="leftext">Search</div>
            <div class="coloncls">
                <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                    <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                    <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="txtdiv">
                <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox></div>
            <div class="btndiv">
                <asp:Button ID="btnSearch" runat="server" Text="Search"
                    CssClass="btnCls" OnClick="btnSearch_Click" />
                <asp:LinkButton runat="server" ID="lnk_showall"
                    OnClick="lnk_showall_Click" Font-Bold="True" Font-Underline="True"> Show All </asp:LinkButton>
            </div>


        </div>
        <div class="txtlbl">
            <b><span style="color: Red;">
                <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label></span></b>
        </div>

        <div align="left" style="height: 10px;"></div>

        <asp:HiddenField runat="server" ID="hid_company_id" />
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
                Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1"
                OnRowDataBound="gvCertificateDetails_RowDataBound"
                OnPageIndexChanging="CertificateListPageindexChanging" AllowPaging="True"
                AllowSorting="True" PageSize="10"
                OnRowCommand="gvCertificateDetails_RowCommand">
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
                    <asp:BoundField DataField="fGPtbl_CUnit_RegistrationNo" HeaderText="SPR No." />
                    <%-- <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />--%>
                    <asp:TemplateField HeaderText="Company Name">
                        <ItemTemplate>
                            <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                            <br />
                            <asp:Label ID="lbl_HS_code" runat="server" Text='<%#Eval("HSCode") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No." />
                    <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />



                    <asp:TemplateField HeaderText="Renewal" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkRenewal" runat="server" CommandName="Renewal" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                CausesValidation="false" ToolTip="Renewal">Renewal</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HFRenewalFlag" runat="server" Value='<%#Eval("RenFlag") %>' />
                            <asp:HiddenField ID="hdn_request" runat="server" Value='<%#Eval("request") %>' />
                            <asp:HiddenField ID="hdn_approve" runat="server" Value='<%#Eval("approved") %>' />
                            <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Validity start date request" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" ID="lnk_requestvalidity" CommandName="validityreq" CommandArgument='<%#Eval("fGPtbl_Company_Id") +";" +Eval("RegistrationDate") +";"+Eval("fGPtbl_Company_Provisional") %>'
                                CausesValidation="false" Visible="false"> Validity Request </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Blacklist Company" ItemStyle-HorizontalAlign="Center" Visible="true">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkBlacklist" runat="server" CommandName="Blacklistunit" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                CausesValidation="false" ToolTip="Black List">Blacklist Company </asp:LinkButton>&nbsp;

                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>

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
                                            <td colspan="2" align="center" valign="top">
                                                <span>Existing Validity Period :: </span>
                                                <asp:Label runat="server" ID="lbl_existing"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr valign="top" align="left">
                                            <td valign="top" align="left" style="width: 100px;" class="updateInnerColumn">Validity Start Date :
                                            </td>
                                            <td valign="top" align="left" class="updateInnerColumn">
                                                <asp:TextBox ID="txt_registartiondate" runat="server" onChange="mydt()" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CExtendertxt_registartiondate" runat="server" PopupButtonID="txt_registartiondate"
                                                    TargetControlID="txt_registartiondate">
                                                </cc1:CalendarExtender>
                                                <asp:RequiredFieldValidator ID="req_validitydate" runat="server" ControlToValidate="txt_registartiondate"
                                                    SetFocusOnError="true" ErrorMessage="Please Fill Validity Date" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>

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
                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="3"
                                                    ValidationGroup="1" CssClass="btnCls" OnClick="btnSubmit_Click" />
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


            <cc1:ModalPopupExtender ID="popup_editvalidity" runat="server" PopupControlID="pnlOut"
                TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
                runat="server" BorderColor="#22518F" Radius="10" Corners="All">
            </cc1:RoundedCornersExtender>
            <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>








                <asp:ModalPopupExtender ID="ModalPopupExtenderBlackList" runat="server" PopupControlID="pnlBlackList" TargetControlID="btnBlackList"
        CancelControlID="ImageButtonBlackList" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtenderBlackList" TargetControlID="pnlBlackListInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>


    <asp:LinkButton ID="btnBlackList" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:Panel ID="pnlBlackList" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlBlackListInner" runat="server" Width="600px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width: 100%">
                                    <td valign="top" align="right" colspan="2" style="padding-bottom: 10px; padding-right: 5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="center" valign="top" class="updateInnerHeader">
                                                    <asp:Label ID="Label3" runat="server" CssClass="header" Visible="False"></asp:Label>
                                                    <asp:HiddenField ID="HiddenField2" runat="server" />
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButtonBlackList" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>



                                <tr valign="top" align="left" id="tr3" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">Authority Name :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtAuthorityName" runat="server"   Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Authority Name is required"
SetFocusOnError="true" ControlToValidate="txtAuthorityName" Display="Dynamic" ValidationGroup="BL"></asp:RequiredFieldValidator>
                                        <asp:HiddenField runat ="server" ID ="hdn_previousstatus" /> 
                                        <asp:HiddenField ID="HiddenFieldCompanyID" runat="server" />
                                    </td>
                                </tr>

                                <tr valign="top" align="left" id="tr4" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">IOM/Certificate no:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtIOM" runat="server" Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="IOM/Certificate no. is required"
SetFocusOnError="true" ControlToValidate="txtIOM" Display="Dynamic" ValidationGroup="BL"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr valign="top" align="left" id="tr5" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">BlackList Reason:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtBlackListReason" TextMode="MultiLine" runat="server" Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Reason is required"
     SetFocusOnError="true" ControlToValidate="txtBlackListReason" Display="Dynamic" ValidationGroup="BL"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr valign="top" align="left" id="tr6" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">BlackList Date:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtBlacklistDate" runat="server" Enabled="false"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                        <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtBlacklistDate" Format="dd/MM/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorBl" runat="server" ErrorMessage="BlackList Date is required"
                                            SetFocusOnError="true" ControlToValidate="txtBlacklistDate" Display="Dynamic" ValidationGroup="BL"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>


                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="Button3" runat="server" Text="Submit" ValidationGroup="BL"
                                            CssClass="btnCls" OnClick="btnBlackList_Click" />


                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </center>
        </asp:Panel>
    </asp:Panel>









        </div>
    </div>
</asp:Content>

