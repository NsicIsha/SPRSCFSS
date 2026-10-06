<%@ Page Title="Certificate" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CertificateList.aspx.cs" Inherits="appAdministrator_CertificateList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../JScript/jquery-1.8.2.js"></script>

    <script language="javascript" type="text/javascript">
        function FunResetcertificate() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtRequest").value = "";
        }


        function Confirmrenewal(companyId, Url) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var c = confirm("Renewal is under process (Click on OK to continue Amendment) .........??  ");
            if (c == true) {

                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CertificateList.aspx/CleanUncompletedata",
                    data: "{'companyId':'" + companyId + "','fomtyeId':'2'}",
                    dataType: "json",
                    success: function (data) {
                        var obj = data.d;
                        if (obj == 1) {

                            window.location = Url;
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });

                return true;

            }
            else {

                return false;

            }
            // document.forms[0].appendChild(confirm_value);
        }

        function Confirmamendent(companyId, Url) {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            var c = confirm("Amendment is under process (Click on OK to continue Renewal) .........??  ");
            if (c == true) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "CertificateList.aspx/CleanUncompletedata",
                    data: "{'companyId':'" + companyId + "','fomtyeId':'3'}",
                    dataType: "json",
                    success: function (data) {
                        var obj = data.d;
                        if (obj == 1) {

                            window.location = Url;
                        }
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });

                return true;

            }
            else {

                return false;

            }
            // document.forms[0].appendChild(confirm_value);
        }

        function validDate() {
            var textboxval = $("#ctl00_ContentPlaceHolder1_txtvaliditydate").val();
            var hiddenprintflag = $("#ctl00_ContentPlaceHolder1_gvCertificateDetails_ctl02_HiddenFormPrintFlag").val();
            var hiddenprint = hiddenprintflag.split(',');
            alert(hiddenprint[0].val());
            alert(hiddenprintflag);
            if (hiddenprint[0].val() == 5) {
            }
            else {
                var fromdate = new Date(textboxval);
                if (fromdate <= new Date('04/01/2015')) {
                    alert("Please Enter another Date");
                    $("#ctl00_ContentPlaceHolder1_txtvaliditydate").val("");
                    return false;
                }
            }

        }



    </script>
    <style type="text/css">
        .txtlbl {
            height: 20px;
            width: 20%;
            float: right;
            white-space: nowrap;
            vertical-align: top;
            padding-left: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hid_val" Value="0" />
    <div align="left">
        <div align="left">
            <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll" Text="Registered Companies List"></asp:Label>
        </div>

        <div align="left" style="height: 10px;">
        </div>

        <div align="center">
            <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>

            <asp:Label ID="lblMsgBlacklisted" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
        </div>
        <div style="height: 10px;">
        </div>

        <div align="left" class="clsrowdiv">
            <div class="leftext" style="width: 7%;">Search</div>
            <div class="coloncls" style="width: 22%;">
                <asp:DropDownList ID="ddlSearchCriteria" runat="server" CssClass="clsDDL">
                    <asp:ListItem Selected="True" Text="Company Name" Value="1"></asp:ListItem>
                    <asp:ListItem Text="PAN" Value="2"></asp:ListItem>
                    <asp:ListItem Text="Reference No." Value="3"></asp:ListItem>
                    <asp:ListItem Text="Registration No." Value="4"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="txtdiv" style="width: 25%;">
                <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox>
            </div>
            <div class="btndiv" style="width: 40%;">
                <table cellspacing="2" cellpadding="2" border="0">
                    <tr>
                        <td align="left" valign="top">
                            <asp:Button ID="btnSearch" runat="server" Text="Search"
                                CssClass="btnClsdivSearch" OnClick="btnSearch_Click" />
                        </td>

                        <td align="left" valign="top">

                            <div id="container">
                                <asp:LinkButton runat="server" ID="lnk_show" Font-Bold="True" Visible="false"
                                    Font-Underline="True" OnClick="lnk_show_Click"> Show All </asp:LinkButton>
                                &nbsp; &nbsp;  <a href="ViewPrintChekList.aspx" target="_blank"><b><u>View Print CheckList</u></b></a>
                            </div>
                        </td>
                    </tr>
                </table>





            </div>


            <div class="txtlbl">
                <b><span style="color: Red;">
                    <asp:Label ID="lbl_count_text" runat="server" Text=""></asp:Label>
                </span></b>
            </div>
            <div style="height: 10px;">
            </div>

            <div align="left" style="padding-left: 10px; padding-right: 10px;">
                <asp:GridView ID="gvCertificateDetails" runat="server" AutoGenerateColumns="false"
                    Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1"
                    OnRowDataBound="gvCertificateDetails_RowDataBound"
                    OnRowCommand="gvCertificateDetails_RowCommand" OnPageIndexChanging="CertificateListPageindexChanging" AllowPaging="True" AllowSorting="True" PageSize="10">
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
                        <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                        <%--    <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>--%>
                        <asp:TemplateField HeaderText="PAN No.">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pan_text" runat="server" Text='<%#Eval("fGPtbl_Company_PAN") %>'></asp:Label>
                                <br />
                                <asp:Label ID="lbl_hscode_pncode" runat="server" Text='<%#Eval("HSCODENPC") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Address" />
                        <asp:TemplateField HeaderText="Print<br>Certificate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnPrint" runat="server" CommandName="print"
                                    CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fGPtbl_CR_AcceptedByAdmin").ToString() %>' CausesValidation="false" ToolTip="Print Certificate">Print</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbnRequest" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                            CommandName="request" ToolTip="Make request for print" Width="60px">Request</asp:LinkButton>&nbsp;
                        <asp:Literal ID="ltlPrint" runat="server"></asp:Literal>
                                <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                <asp:HiddenField ID="HiddenFormPrintFlag" runat="server" Value='<%#Eval("doicheck").ToString()+","+Eval("RegistrationDate").ToString() %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Draft<br>Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnDraftPrint" runat="server" CommandName="draftprint"
                                    CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Draft print">Draft Print
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Duplicate<br>Print" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbnDuplicatePrint" runat="server" CommandName="Duplicateprint"
                                    CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CausesValidation="false" ToolTip="Duplicate print">Duplicate Print
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amendment" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkamedment" runat="server" CommandName="Amendment" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                    CausesValidation="false" ToolTip="Amendment">Amendment</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HiddenFieldCompanyIdAmendment" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                <asp:Literal ID="lt_amendment" Visible="false" runat="server" Text="Amendment process started"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkamdement_print" runat="server" CommandName="APrint" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_PrintAccess").ToString()+","+Eval("fgtbl_Certificate_Amedment").ToString() %>'
                                    CausesValidation="false" ToolTip="Amendment Print">APrint</asp:LinkButton>
                                <asp:LinkButton ID="lbnRequest1" runat="server" CausesValidation="false" CommandArgument='<%#Eval("fGPtbl_CERTIFICATE_Id")%>'
                                    CommandName="request1" ToolTip="Make request for print" Width="60px" Visible="false">Request</asp:LinkButton>&nbsp;
                        <asp:Literal ID="ltlPrint1" runat="server"></asp:Literal>
                                <asp:HiddenField ID="HiddenFieldCompanyId1" runat="server" Value='<%#Eval("fGPtbl_Company_Id") %>' />
                                <asp:HiddenField ID="HiddenAmedment" runat="server" Value='<%#Eval("fgtbl_Certificate_Amedment")%>' />
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Renewal" ItemStyle-HorizontalAlign="Center" Visible="false">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkRenewal" runat="server" CommandName="Renewal" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                    CausesValidation="false" ToolTip="Renewal">Renewal</asp:LinkButton>&nbsp;
                   <asp:HiddenField ID="HFRenewalOpenFlag" runat="server" Value='<%#Eval("RenFlag") %>' />
                                <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Udyam" ItemStyle-HorizontalAlign="Center" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkUdyam" runat="server" CommandName="Udyam" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>'
                                    CausesValidation="false" ToolTip="Update Udyam">Udyam Registration</asp:LinkButton>&nbsp;
                 <%--  <asp:HiddenField ID="HFRenewalOpenFlag" runat="server" Value='<%#Eval("RenFlag") %>' />
                   <asp:Literal ID="ltlrenmessage" runat="server" Text="Renewal process started"></asp:Literal>--%>
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

            </div>
        </div>


    </div>

    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
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
                                                    <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                                    <asp:HiddenField ID="hdnfieldCertificateId" runat="server" />
                                                    <asp:HiddenField ID="hdnfieldcompanyid" runat="server" />
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>

                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn" colspan="2">
                                        <asp:RadioButtonList ID="rdbRequestType" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="1" Selected="True" Text="For Print"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="For Editing Company Details"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <asp:RequiredFieldValidator ID="rfdrdbRequestType" runat="server" ErrorMessage="Please Select Valid Reason"
                                            SetFocusOnError="true" ControlToValidate="rdbRequestType" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>

                                    </td>

                                </tr>

                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">Reason for Request :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtRequest" runat="server" MaxLength="5000"
                                            CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtrequest" runat="server" ErrorMessage="Please Enter Reason for Request"
                                            SetFocusOnError="true" ControlToValidate="txtRequest" Display="Dynamic" ValidationGroup="1"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>


                                <tr valign="top" align="left">
                                    <td align="left" valign="top"></td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                            ValidationGroup="1" CssClass="btnCls" OnClick="btnSubmit_Click" />
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" class="btnCls" onclick="return FunResetcertificate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:RequiredFieldValidator ID="RFValidatorRequest" runat="server" ErrorMessage="Please provide Reason for request"
                    SetFocusOnError="true" ControlToValidate="txtRequest" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidatorRequest" runat="server" ErrorMessage="Reason should be less than 5000 characters"
                    Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txtRequest"
                    ValidationGroup="1"></asp:CustomValidator>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RFValidatorRequest">
                </asp:ValidatorCalloutExtender>
                <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="CustomValidatorRequest">
                </asp:ValidatorCalloutExtender>
            </center>
        </asp:Panel>
    </asp:Panel>


    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>

    <asp:Panel ID="pnlOutprint" runat="server" CssClass="outerPopup">
        <asp:Panel ID="pnlInnerprint" runat="server" Width="400px" CssClass="innerPopup">
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
                                                    <asp:Label ID="Label1" runat="server" CssClass="header" Visible="False"></asp:Label>
                                                    <asp:HiddenField ID="hidpopupcompanyid" runat="server" />
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnClose" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>



                                <tr valign="top" align="left" id="tr_validity" runat="server" visible="false">
                                    <td valign="top" align="left" class="updateInnerColumn">VALIDITY START DATE :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtvaliditydate" runat="server"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" ID="calvaliditydate" TargetControlID="txtvaliditydate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RFVtxtvaliditydate" runat="server" ErrorMessage="Validity date is required"
                                            SetFocusOnError="true" ControlToValidate="txtvaliditydate" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>

                                <tr valign="top" align="left" id="tr_proposal" runat="server" style='display: none;'>
                                    <td valign="top" align="left" class="updateInnerColumn">PROPOSAL DATE:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtpropasaldate" runat="server"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <%-- <asp:CalendarExtender runat ="server" ID ="calProposal" TargetControlID ="txtpropasaldate">
                                        </asp:CalendarExtender>
                                          <asp:RangeValidator runat="server" id="rngDate" controltovalidate="txtpropasaldate" type="Date" minimumvalue="04/01/2014" maximumvalue="01/01/3018" errormessage="Please enter a valid date within 2014!" />
                                        <asp:RequiredFieldValidator ID="RFVtxtpropasaldate" runat="server" ErrorMessage="Proposal date is required"
                                    SetFocusOnError="true" ControlToValidate="txtpropasaldate" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>--%>
                                    </td>
                                </tr>

                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">DATE OF ISSUE:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtdateofissue" runat="server"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <asp:CalendarExtender runat="server" ID="caldateofissue" TargetControlID="txtdateofissue">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RFVtxtdateofissue" runat="server" ErrorMessage="Date of issue is required"
                                            SetFocusOnError="true" ControlToValidate="txtdateofissue" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>


                                <%-- <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                Certificate Number:
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtCertificateNumber" runat="server" 
                                        CssClass="txt_bx" ></asp:TextBox>
                                         
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Certifiate Number is Required"
                                    SetFocusOnError="true" ControlToValidate="txtCertificateNumber" Display="Dynamic" ValidationGroup="2"></asp:RequiredFieldValidator>
                                 </td>
                               </tr>		--%>



                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <%--   Comments:--%>
                                Inspection agency<br />
                                        comments / Note<br />
                                        (Optional):
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txt_bx_comments" runat="server" CssClass="txt_bx" TextMode="MultiLine" MaxLength="150" Height="100px">
                                 
                                        </asp:TextBox>
                                    </td>
                                </tr>

                                <tr valign="top" align="left">
                                    <td align="left" valign="top"></td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit_print" runat="server" Text="Submit"
                                            CssClass="btnCls" ValidationGroup="2"
                                            OnClick="btnSubmit_print_Click" />
                                        &nbsp;&nbsp;
                                        <input id="Button1" type="button" causesvalidation="false" value="Reset" class="btnCls" onclick="return FunResetcertificate();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </center>
        </asp:Panel>
    </asp:Panel>


    <asp:ModalPopupExtender ID="modprintpopup" runat="server" PopupControlID="pnlOutprint"
        TargetControlID="lnkprint" CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender2" TargetControlID="pnlInnerprint"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>
    <asp:LinkButton ID="lnkprint" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:LinkButton ID="lnkDuplicatePrint" runat="server" Style="display: none;"></asp:LinkButton>

    <!--Duplicate code-->
    <asp:Panel ID="panelDuplicate" runat="server" CssClass="outerPopup">
        <asp:Panel ID="panelInnerDuplicate" runat="server" Height="500px" Width="400px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width: 100%">
                                    <td valign="top" align="right" colspan="2" style="padding-bottom: 10px; padding-right: 5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <%--  <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="Label2" runat="server" CssClass="header"></asp:Label>
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            <asp:HiddenField ID="HiddenField2" runat="server" />
                                                        </td>--%>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton1" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>



                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">DATE OF FIR:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtFIRDate" runat="server"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <asp:CalendarExtender Format="MM/dd/yyyy" runat="server" ID="CalendarExtender1" TargetControlID="txtFIRDate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Date of FIR is required"
                                            SetFocusOnError="true" ControlToValidate="txtFIRDate" Display="Dynamic" ValidationGroup="11"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>

                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">Certificate Number:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtCertificateNumber_Duplicate" runat="server"
                                            CssClass="txt_bx"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Certificate Number is required"
                                            SetFocusOnError="true" ControlToValidate="txtCertificateNumber_Duplicate" Display="Dynamic" ValidationGroup="11"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>


                                <tr valign="top" align="left">

                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">

                                        <asp:Panel ID="Panel1" runat="server">
                                            <table width="80%">
                                                <%--<tr>
        <td  colspan="2" class="style1">   
        <b>Option for Inspection Fees </b>
        
        </td>
        </tr>--%>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:RadioButtonList ID="rdl_inspectionmode_payment" runat="server" RepeatDirection="Horizontal"
                                                            AutoPostBack="True" OnSelectedIndexChanged="rbl_selectindexchanged_InspectionAgency">
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px; margin-top: 0px;" colspan="3">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Panel ID="PanelChequeDD" runat="server" Visible="false">
                                                                        <table width="80%">





                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <b>Cheque / DD No.</b>
                                                                                </td>
                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                    <asp:TextBox ID="txt_chqddNo_inspection" runat="server" CssClass="txt_bx"
                                                                                        MaxLength="50"></asp:TextBox>

                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                                                        ErrorMessage="Please Enter Cheque or DD Number"
                                                                                        ControlToValidate="txt_chqddNo_inspection"
                                                                                        SetFocusOnError="true"
                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>



                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <b>Bank Name</b>
                                                                                </td>
                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                    <asp:TextBox ID="txt_BankNam_inspection" runat="server" CssClass="txt_bx"
                                                                                        MaxLength="50"></asp:TextBox>

                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                                                        ErrorMessage="Please Enter Bank Name"
                                                                                        ControlToValidate="txt_BankNam_inspection"
                                                                                        SetFocusOnError="true"
                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>



                                                                                </td>
                                                                            </tr>

                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <b>Bank Branch</b>
                                                                                </td>
                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                    <asp:TextBox ID="txt_BankBranch_inspection" runat="server" CssClass="txt_bx"
                                                                                        MaxLength="50"></asp:TextBox>

                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                                                        ErrorMessage="Please Enter Bank Branch"
                                                                                        ControlToValidate="txt_BankBranch_inspection"
                                                                                        SetFocusOnError="true"
                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>

                                                                                </td>
                                                                            </tr>

                                                                            <%--  <tr>
        <td valign="top">
        <b>Payable At</b>
        </td>
         <td align="left" valign="top" style="padding-bottom:10px;">
                            <asp:TextBox ID="txt_PayableAt_inspection" runat="server" CssClass="txt_bx" 
                                  MaxLength="50"></asp:TextBox>
                                
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                ErrorMessage="Please Enter Payable At"
                                                ControlToValidate="txt_PayableAt_inspection"
                                                SetFocusOnError="true"
                                                ValidationGroup="11"
                                            ></asp:RequiredFieldValidator>
                                            
                                         
                           
                        </td>
        </tr>--%>

                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <b>Cheque / DD Amount (Including 18% GST)</b>
                                                                                </td>
                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                    <asp:TextBox ID="txt_DD_Inspection" runat="server" CssClass="txt_bx"
                                                                                        MaxLength="50" Text="1180" ReadOnly="true"></asp:TextBox>

                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <b>Cheque / DD Date (as MM/DD/YYYY)</b>
                                                                                </td>
                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                    <asp:TextBox ID="txt_chqddate_inspection" runat="server" CssClass="txt_bx"
                                                                                        MaxLength="50" ReadOnly="false"></asp:TextBox>


                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9454" runat="server"
                                                                                        ErrorMessage="Please Enter Cheque / DD Date"
                                                                                        ControlToValidate="txt_chqddate_inspection"
                                                                                        SetFocusOnError="true" Display="Dynamic"
                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>




                                                                                    <asp:CompareValidator ID="CValidatortxt_cheq1" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                                                        Display="Dynamic" ControlToValidate="txt_chqddate_inspection" Operator="DataTypeCheck" Type="Date">
                                                                                    </asp:CompareValidator>



                                                                                </td>
                                                                            </tr>


                                                                        </table>
                                                                    </asp:Panel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>




                                                <tr>
                                                    <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px; margin-top: 0px;" colspan="3">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:Panel ID="PanelInspectionOnlieRites" runat="server" Visible="false">
                                                                        <table width="80%">
                                                                            <tr>
                                                                                <td align="left" valign="top">
                                                                                    <%-- <asp:RadioButtonList ID="rdbInspectionPaymentType_Online" runat="server" 
                                              RepeatDirection="Horizontal" AutoPostBack="true" onselectedindexchanged="rdbInspectionPaymentType_Online_selectindexchanged"
                                              >
                                          <asp:ListItem Value="1">NEFT</asp:ListItem>
                                           <asp:ListItem Value="2">RTGS</asp:ListItem>
                                        </asp:RadioButtonList>
                                          
                                        <asp:RequiredFieldValidator   
            ID="RequiredFieldValidatorrdbInspectionPaymentType_Online"  
            runat="server"  
            ControlToValidate="rdbInspectionPaymentType_Online"  
            ErrorMessage="Select Payment Option." 
             ValidationGroup="1" 
               SetFocusOnError="true"
            >  
        </asp:RequiredFieldValidator> --%>
             
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2" style="padding-bottom: 10px">

                                                                                    <asp:Label ID="lblOnlineNorthernMsg" Visible="false" Font-Bold="true" ForeColor="red" runat="server" />
                                                                                    <br />



                                                                                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="true" ForeColor="Green" Font-Size="Large" Target="_blank">
                                                                                        <asp:Label ID="lbl_OnlineMsg" runat="server" Font-Size="Small"></asp:Label>
                                                                                    </asp:HyperLink>


                                                                                </td>

                                                                            </tr>

                                                                            <tr>




                                                                                <td align="left" valign="top">
                                                                                    <asp:Panel ID="Panel3" runat="server">


                                                                                        <table width="80%">



                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <b>UTR NO</b>
                                                                                                </td>
                                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                    <asp:TextBox ID="txtUTRNO_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                        MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                                                                        ErrorMessage="Please Enter Utr Number"
                                                                                                        ControlToValidate="txtUTRNO_InspectionAgency"
                                                                                                        SetFocusOnError="true"
                                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>



                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <b>Bank Name</b>
                                                                                                </td>
                                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                    <asp:TextBox ID="txtBankName_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                        MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                                                                        ErrorMessage="Please Enter Bank Name"
                                                                                                        ControlToValidate="txtBankName_InspectionAgency"
                                                                                                        SetFocusOnError="true"
                                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>

                                                                                                </td>
                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <b>Branch Name</b>
                                                                                                </td>
                                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                    <asp:TextBox ID="txtBranchName_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                        MaxLength="50" ReadOnly="false"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                                                                        ErrorMessage="Please Enter Branch Name"
                                                                                                        ControlToValidate="txtBranchName_InspectionAgency"
                                                                                                        SetFocusOnError="true"
                                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>

                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <b>Date (as MM/DD/YYYY)</b>
                                                                                                </td>
                                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                    <asp:TextBox ID="txtDate_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                        MaxLength="50"></asp:TextBox>

                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server"
                                                                                                        ErrorMessage="Please Enter Date"
                                                                                                        ControlToValidate="txtDate_InspectionAgency"
                                                                                                        SetFocusOnError="true"
                                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>

                                                                                                    <asp:CompareValidator ID="CompareValidator112" runat="server" ErrorMessage="Please enter date in MM/DD/YYYY format"
                                                                                                        Display="Dynamic" ControlToValidate="txtDate_InspectionAgency" Operator="DataTypeCheck" Type="Date"
                                                                                                        ValidationGroup="1">
                                                                                                    </asp:CompareValidator>



                                                                                                </td>
                                                                                            </tr>

                                                                                            <tr>
                                                                                                <td>
                                                                                                    <b>Amount (Including 18% GST)</b>
                                                                                                </td>
                                                                                                <td align="left" valign="top" style="padding-bottom: 10px;">
                                                                                                    <asp:TextBox ID="txtAmount_InspectionAgency" runat="server" CssClass="txt_bx"
                                                                                                        MaxLength="50" ReadOnly="true" Text="1180"></asp:TextBox>
                                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                                                                        ErrorMessage="Please Enter Amount"
                                                                                                        ControlToValidate="txtDate_InspectionAgency"
                                                                                                        SetFocusOnError="true"
                                                                                                        ValidationGroup="11"></asp:RequiredFieldValidator>
                                                                                                </td>
                                                                                            </tr>

                                                                                        </table>
                                                                                    </asp:Panel>

                                                                                </td>





                                                                            </tr>


                                                                        </table>

                                                                    </asp:Panel>

                                                                </td>
                                                            </tr>

                                                        </table>

                                                    </td>
                                                </tr>

                                            </table>
                                        </asp:Panel>











                                    </td>
                                </tr>

                                <tr valign="top" align="left">
                                    <td align="left" valign="top"></td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="Button2" runat="server" Text="Submit" ValidationGroup="11"
                                            CssClass="btnCls" OnClick="btnSubmit_duplicateprint_Click" />
                                        &nbsp;&nbsp;
                                        <input id="btnDReset" type="button" value="Reset" class="btnCls" onclick="return FunResetcertificate();" />

                                        <asp:Label ID="lblRedMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </center>
        </asp:Panel>
    </asp:Panel>

    <!--Duplicate code-->
    <asp:ModalPopupExtender ID="modalDuplicate" runat="server" PopupControlID="panelDuplicate" TargetControlID="lnkDuplicatePrint"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender3" TargetControlID="panelInnerDuplicate"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>

















    <asp:ModalPopupExtender ID="ModalPopupUdyam" runat="server" PopupControlID="PanelUdyam" TargetControlID="LinkUdyam"
        CancelControlID="btnClose" BackgroundCssClass="modalBackground">
    </asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender4" TargetControlID="PanelUdyamInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </asp:RoundedCornersExtender>

    <asp:LinkButton ID="LinkUdyam" runat="server" Style="display: none;"></asp:LinkButton>
    <asp:Panel ID="PanelUdyam" runat="server" CssClass="outerPopup">
        <asp:Panel ID="PanelUdyamInner" runat="server" Width="400px" CssClass="innerPopup">
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
                                                    <asp:Label ID="Label2" runat="server" CssClass="header" Visible="False"></asp:Label>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </td>
                                                <td align="right" valign="top" style="width: 20px;">
                                                    <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="ImageButton2" runat="server"
                                                        ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                </td>
                                            </tr>
                                        </table>

                                    </td>
                                </tr>



                                <tr valign="top" align="left" id="tr1" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">Existing UAM/UDYAM :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="TextBoxExisting" runat="server" ReadOnly="true" Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                        <asp:HiddenField ID="HiddenFieldCompanyID" runat="server" />
                                    </td>
                                </tr>

                                <tr valign="top" align="left" id="tr2" runat="server">
                                    <td valign="top" align="left" class="updateInnerColumn">UDYAM To be Added:
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="TextBoxUdyam" runat="server" Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                    </td>
                                </tr>


                                <tr valign="top" align="center">
                                    <td valign="top" align="left"></td>
                                    <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                        <asp:Button ID="btnUpdateUdyam" runat="server" Text="Submit"
                                            CssClass="btnCls" OnClick="btnUpdateUdyam_Click" />


                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </center>
        </asp:Panel>
    </asp:Panel>






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
                                        <asp:TextBox ID="txtAuthorityName" runat="server" ReadOnly="true" Style="text-transform: uppercase;"
                                            CssClass="txt_bx"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Authority Name is required"
SetFocusOnError="true" ControlToValidate="txtAuthorityName" Display="Dynamic" ValidationGroup="BL"></asp:RequiredFieldValidator>
                                        <asp:HiddenField runat ="server" ID ="hdn_previousstatus" /> 
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
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
</asp:Content>

