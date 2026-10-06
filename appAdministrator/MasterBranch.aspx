<%@ Page Title="NSIC SPR::Branch Master" Language="C#" MasterPageFile="~/appMaster/Admin.master"
    AutoEventWireup="true" CodeFile="MasterBranch.aspx.cs" Inherits="MasterBranch"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script language="javascript" type="text/javascript">/*fdf*/
        function FunActiveConfirm(obj) {
            if (obj.title == 'Activate This') {
                if (confirm('Are you sure you want to ACTIVATE this Branch? This will also ACTIVATE ALL USERS associated with this Branch!') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {

                if (confirm('Are you sure you want to DEACTIVATE this Branch? This will also DEACTIVATE ALL USERS associated with this Branch!') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        $(document).ready(function() {
            $("#ctl00_ContentPlaceHolder1_btnSubmit").click(function() {
                var objZone = document.getElementById("ctl00_ContentPlaceHolder1_DropDownListZone").value;

                if (objZone == "") {
                    alert("Please Select Zone");
                    return false;
                }

            });
        });
        
        function IsValidEmail(email) {
            var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
            return expr.test(email);
        };
        function ValidateEmail() {
            var email = document.getElementById("<%=txt_email.ClientID%>").value;
            if (!IsValidEmail(email)) {
                alert("Invalid email address.");
                return false;
            }
            else {
               // alert("Valid email address.");
                return true;
            }
        }
        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblBranch" runat="server" Text="Branch Master" CssClass="headerAll"></asp:Label>
    </div>
    <div align="right">
        <asp:Button ID="btnAddBranch" Text="Add" CausesValidation="false" runat="server"
            CssClass="btnCls" OnClick="btnAddBranch_Click" />
    </div>
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvBranch" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewBranchNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="center" valign="top" style="width: 100%;">
                            <asp:Label ID="lblMessage" runat="server" CssClass="MasterAction"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewBranchDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color: #22518F;">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="right" valign="top" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top" style="width: 100%">
                                    <td class="Action_TD_1" style="width: 80%; text-align: center;">
                                        <%if (branchAction == 1)
                                          { %>
                                        <div id="divAction" runat="server" class="Action_Div" style="background-color: #ECF8FB;
                                            height: 20px;">
                                            <div id="Action_DivInner_1">
                                                <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                            </div>
                                            <div id="Action_DivInner_2">
                                                <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png"
                                                    border="0" width="16px" height="15px" alt="CLOSE" style="cursor: pointer;" />
                                            </div>
                                        </div>
                                        <cc1:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="divAction"
                                            BorderColor="#22518F" Radius="10" Corners="All">
                                        </cc1:RoundedCornersExtender>
                                        <%} %>
                                    </td>
                                    <td valign="top" align="right" style="width: 30%;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="center" valign="top" style="width: 100%;">
                            <div align="center" style="vertical-align: top;">
                                Zone :
                                <asp:DropDownList ID="ddlZone" runat="server" Width="150px" OnSelectedIndexChanged="ddlZone_SelectedIndexChanged"
                                    CssClass="clsDDL" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </td>
                    </tr>
                    <tr valign="top" align="left" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%; height: 10px;">
                        </td>
                    </tr>
                </table>
                <div align="center">
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="HiddenField4" runat="server" />
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        <tr align="left" valign="top" style="width: 100%;">
                            <td align="left" valign="top" style="width: 100%;">
                                <asp:GridView ID="gvBranch" runat="server" AutoGenerateColumns="false" Width="100%"
                                    BorderWidth="0" CellPadding="0" CellSpacing="1" OnRowDataBound="gvBranch_RowDataBound"
                                    OnRowCommand="gvBranch_RowCommand">
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate>
                                                <asp:Literal ID="ltlSno" runat="server"></asp:Literal></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="fGPtbl_MB_Name" HeaderText="BRANCH NAME" />
                                        <asp:BoundField DataField="fGPtbl_MB_Code" HeaderText="CODE" />
                                        <asp:BoundField DataField="ZoneName" HeaderText="ZONE NAME" />
                                        <asp:BoundField DataField="fGPtbl_MB_Desc" HeaderText="DESCRIPTION" />
                                        <asp:BoundField DataField="fGPtbl_MB_AccessId" HeaderText="Active/ Deactive" HeaderStyle-Width="60px" />
                                        <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    CommandName="edt" CommandArgument='<%#Eval("fGPtbl_MB_Id") %>' ToolTip="Edit branch" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activate/ Deactivate" HeaderStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgAct" runat="server" CommandName="Act" CommandArgument='<%#Eval("fGPtbl_MB_Id") %>'
                                                    OnClientClick="return FunActiveConfirm(this);" />
                                                <asp:HiddenField ID="HiddenField3" runat="server" Value='<%#Eval("fGPtbl_MZ_AccessId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                        <tr>
                            <td align="left">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" style="padding-left: 10px;">
                                <table border="0" cellpadding="0" cellspacing="1" width="350px">
                                    <tr valign="top" align="left">
                                        <td colspan="2" class="clsLegends">
                                            LEGENDS:-
                                        </td>
                                    </tr>
                                    <tr valign="top" align="left">
                                        <td class="clsLegends_Left_Deactivate">
                                        </td>
                                        <td class="clsLegends_Right">
                                            Shows deactivated Branches.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
    <asp:Panel ID="pnlOut" runat="server" Style="display: none;" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width: 100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top">
                                    <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px; padding-right:5px;">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr align="left" valign="top">
                                                <td align="center" valign="top" class="updateInnerHeader">
                                                    <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                        ID="lblBranchName" runat="server" CssClass="header"></asp:Label>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                    <asp:HiddenField ID="hdnfield" runat="server" />
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
                                    <td colspan="2" align="right" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top" align="center">
                                    <td align="left" valign="top" style="width:100px;" class="updateInnerColumn">
                                        Zone :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <%if (hdnfield.Value == "add" || hdnfield.Value=="edit")
                                          { %>
                                        <asp:DropDownList ID="DropDownListZone" runat="server" CssClass="clsDDL" AutoPostBack="False">
                                        </asp:DropDownList>
                                        <%} %>
                                        <%else
                                            { %>
                                        <asp:Label ID="lblZonename" runat="server" CssClass="updateInnerColumn"></asp:Label>
                                        <%} %>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        Brach Name :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txtBranchName" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        Code :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txtCode" runat="server" MaxLength="3" TabIndex="2" CssClass="txt_bxUpper"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        Branch Description :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="2000" TabIndex="3" CssClass="txt_bx"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td align="left" valign="top" colspan="2" style="height: 10px;">
                                    </td>
                                </tr>
                                    <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        Branch Address :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_branch_address" runat="server" MaxLength="2000" TabIndex="4" CssClass="txt_bx"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        Phone :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_phone" runat="server" MaxLength="50" TabIndex="5" CssClass="txt_bx"
                                            TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>                                
                                 <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                       Fax :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_fax" runat="server" MaxLength="50" TabIndex="6" CssClass="txt_bx"
                                            TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                     Email :
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:TextBox ID="txt_email" runat="server" MaxLength="50" TabIndex="7" CssClass="txt_bx"
                                            TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr align="left" valign="top">
                                    <td align="left" valign="top" colspan="2" style="height: 10px;">
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                    </td>
                                    <td valign="top" align="left"  class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="1"
                                            CssClass="btnCls" OnClientClick="javascript:return ValidateEmail();" OnClick="btnSubmit_Click"  />
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" tabindex="9" class="btnCls" onclick="return FunResetBranch();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <asp:RequiredFieldValidator ID="RFValidatorBranchName" runat="server" ControlToValidate="txtBranchName"
                    SetFocusOnError="true" ErrorMessage="Please Fill Branch Name" Display="None"
                    ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RFValidatorZonelist" runat="server" ErrorMessage="Please Select Zone"
                    ValidationGroup="1" Display="None" SetFocusOnError="true" ControlToValidate="DropDownListZone"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RFValidatorCode" runat="server" ErrorMessage="Please provide code" ControlToValidate="txtCode"
                ValidationGroup="1" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RFValidatiorsAddress" runat="server" ErrorMessage="Plese Provide Address" ControlToValidate="txt_branch_address" 
                 ValidationGroup="1" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                 <asp:RequiredFieldValidator ID="RFValidatiorsPhone" runat="server" ErrorMessage="Please Provide Phone" ControlToValidate="txt_phone"
                  ValidationGroup="1" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                  <asp:RequiredFieldValidator ID="RFValidatiorsEmail" runat="server" ErrorMessage="Please Provide valid Email" ControlToValidate="txt_email"
                    ValidationGroup="1" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
                 
                <asp:CustomValidator ID="CustomValidatorBranchName" runat="server" ErrorMessage="Branch Name should be less than 200 characters"
                    Display="None" ControlToValidate="txtBranchName" ClientValidationFunction="CharacterLimit200"
                    SetFocusOnError="true" ValidationGroup="1"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidatorDescription" runat="server" ErrorMessage="Description should be less than 2000 characters"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txtDescription"
                    ValidationGroup="1"></asp:CustomValidator>
                        <asp:CustomValidator ID="CustomValidatorAddress" runat="server" ErrorMessage="Address should be less than 2000 characters"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txt_branch_address"
                    ValidationGroup="1"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidatorPhone" runat="server" ErrorMessage="phone should be less than 50 characters"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txt_phone"
                    ValidationGroup="1"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidatorEmail" runat="server" ErrorMessage="Email should be less than 50 characters"
                    Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txt_email"
                    ValidationGroup="1"></asp:CustomValidator>
                <cc1:ValidatorCalloutExtender ID="VCExtenderBranchName" TargetControlID="RFValidatorBranchName"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderCode" TargetControlID="RFValidatorCode" runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderZonelist" TargetControlID="RFValidatorZonelist"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderCustomName" TargetControlID="CustomValidatorBranchName"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="VCExtenderCustomDescription" TargetControlID="CustomValidatorDescription"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                
                
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderCode" runat="server" FilterMode="ValidChars"
                FilterType="Numbers,LowercaseLetters,UppercaseLetters" TargetControlID="txtCode">
                </cc1:FilteredTextBoxExtender>
                
               
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderAddress" TargetControlID="RFValidatiorsAddress"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                 <cc1:ValidatorCalloutExtender ID="ValidatorCalloutPhone" TargetControlID="RFValidatiorsPhone"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                  <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtenderemail" TargetControlID="RFValidatiorsEmail"
                    runat="server">
                </cc1:ValidatorCalloutExtender>
                
                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderPhone" runat="server" 
                 TargetControlID="txt_phone" FilterType="Custom, Numbers" ValidChars="+-=/*().">
                </cc1:FilteredTextBoxExtender>
                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtenderEmail" runat="server" 
                 TargetControlID="txt_email" FilterType="Custom, Numbers,LowercaseLetters" ValidChars=".@">
                </cc1:FilteredTextBoxExtender>
            </center>
        </asp:Panel>
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>
</asp:Content>
