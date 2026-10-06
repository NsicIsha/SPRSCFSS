<%@ Page Title="GP::Inspection Agency Branch Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInspectionAgencyBranchAdd.aspx.cs" Inherits="appAdministrator_MasterInspectionAgencyBranchAdd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> <%--test--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div align="left">
    <asp:Label ID="lblInspbr" runat="server" Text="Master InspectionAgency Branch" CssClass="headerAll"></asp:Label>
    <div align="left" style="height:20px;"></div>
    <table border="0" cellpadding="0" cellspacing="0" width="400px">
        <tr valign="top" align="left" >
                <td align="left" valign="top" style="width:160px;" class="updateInnerColumn">
                    Zone :
                </td>
                <td align="left" valign="top" style="width:225px;" class="updateInnerColumn">
                   
                    <asp:DropDownList ID="DropDownListZone" runat="server" CssClass="clsDDL" 
                        AutoPostBack="true" TabIndex="1" onselectedindexchanged="DropDownListZone_SelectedIndexChanged" Width="200px">
                    </asp:DropDownList>
                            
                    <asp:RequiredFieldValidator ID="RFValidatorZone" runat="server" ErrorMessage="Please select zone"
                    ControlToValidate="DropDownListZone" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RFValidatorZone">
                    </cc1:ValidatorCalloutExtender>
                    
                    <asp:Label ID="lblZone" runat="server" CssClass="header"></asp:Label>
                </td>
            </tr>
            <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    Branch :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:DropDownList ID="DropDownListBranch" runat="server" TabIndex="2" CssClass="clsDDL" AutoPostBack="false" Width="200px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFValidatorBranch" runat="server" ErrorMessage="Please select branch"
                    ControlToValidate="DropDownListBranch" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RFValidatorBranch">
                    </cc1:ValidatorCalloutExtender>
                    <asp:Label ID="lblBranch" runat="server" CssClass="header"></asp:Label>
                </td>
             </tr>
            <%-- SD--%>
              
             <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    InspectionAgency :
                </td>
                <td valign="top" align="left" class="updateInnerColumn">
                    <asp:DropDownList ID="DropDownListInsp" runat="server" TabIndex="1" CssClass="clsDDL" AutoPostBack="false" Width="200px"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RFValidatorInsp" runat="server" ErrorMessage="Please select InspectionAgency"
                    ControlToValidate="DropDownListInsp" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RFValidatorInsp">
                    </cc1:ValidatorCalloutExtender>
                    <asp:Label ID="lblInspagency" runat="server" CssClass="header"></asp:Label>
                </td>
             </tr>
             
              
             <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    Inspection Agency Name / Location </td>
                <td valign="top" align="left" class="updateInnerColumn">
                     <asp:TextBox ID="txtIAN_Location" runat="server" CssClass="txt_bx" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatoar1" runat="server" ErrorMessage="Please provide Inspection Agency Name / Location" ControlToValidate="txtIAN_Location"
                    SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidatoar1">
                    </cc1:ValidatorCalloutExtender>

                    &nbsp;</td>
             </tr>
             
             <tr align="left" valign="top">
                <td align="left" valign="top" class="updateInnerColumn">
                    User Name :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:TextBox ID="txtUserid" runat="server" CssClass="txt_bx" TabIndex="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFValidatortxtUserid" runat="server" ErrorMessage="Please provide user name" ControlToValidate="txtUserid"
                    SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCExtendertxtUserid" runat="server" TargetControlID="RFValidatortxtUserid">
                    </cc1:ValidatorCalloutExtender>
                </td>
             </tr>
             
             <tr align="left" valign="top">
                <td align="left" valign="top" class="updateInnerColumn">
                    Password :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="txt_bx" TabIndex="3" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFValidatortxtPassword" runat="server" ErrorMessage="Please provide password" ControlToValidate="txtPassword"
                    SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCExtendertxtPassword" runat="server" TargetControlID="RFValidatortxtPassword">
                    </cc1:ValidatorCalloutExtender>
                </td>
             </tr>
             
             <tr align="left" valign="top">
                <td align="left" valign="top" class="updateInnerColumn">
                    Phone :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="txt_bx" TabIndex="4" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFValidatortxtPhone" runat="server" ErrorMessage="Please provide phone" ControlToValidate="txtPhone"
                    SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCExtendertxtPhone" runat="server" TargetControlID="RFValidatortxtPhone">
                    </cc1:ValidatorCalloutExtender>
                </td>
             </tr>
             
             <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    Email-id :
                </td>
                <td valign="top" align="left" class="updateInnerColumn">
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="txt_bx" TabIndex="5"></asp:TextBox>
                    <asp:Label ID="lblEmail" runat="server" CssClass="header"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" Display="None"
                        ErrorMessage="Email Id is required !" ControlToValidate="txtEmail" SetFocusOnError="true"
                        ValidationGroup="1"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                        Display="None" ErrorMessage="Please provide valid Email Id !" SetFocusOnError="True"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="1">
                    </asp:RegularExpressionValidator>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" TargetControlID="RequiredFieldValidatorEmail"
                        Width="250px">
                    </cc1:ValidatorCalloutExtender>
                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" TargetControlID="RegularExpressionValidator1">
                    </cc1:ValidatorCalloutExtender>
                    
                </td>
             </tr>
             
             <tr align="left" valign="top">
                <td align="left" valign="top" class="updateInnerColumn">
                    Address :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:TextBox ID="txtAddress" runat="server" CssClass="txt_bx" TextMode="MultiLine" Rows="3" Width="250px" TabIndex="6"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RFValidatortxtAddress" runat="server" ErrorMessage="Address is required" ControlToValidate="txtAddress"
                    SetFocusOnError="true" ValidationGroup="1" Display="Dynamic"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="VCExtender" runat="server" TargetControlID="RFValidatortxtAddress">
                    </cc1:ValidatorCalloutExtender>
                </td>
             </tr>
             
             <tr align="left" valign="top">
                <td align="left" valign="top" colspan="2" style="height:5px;"></td>
             </tr>
             
             <tr align="left" valign="top" >
                <td align="left" valign="top" class="updateInnerColumn">
                    Instruction :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    <asp:TextBox ID="txtInstr" runat="server" CssClass="txt_bx" TabIndex="7" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox>
                    <asp:Label ID="lblInstr" runat="server"></asp:Label>
                    <asp:CustomValidator ID="CustomValidatorInstr" runat="server" ErrorMessage="Instruction should be less than 5000 characters"
                        Display="None" ValidationGroup="1" ControlToValidate="txtInstr" ClientValidationFunction="CharacterLimit5000"
                        SetFocusOnError="true" ></asp:CustomValidator>
                   <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="CustomValidatorInstr">
                    </cc1:ValidatorCalloutExtender>
                </td>
             </tr>
             
               <tr align="left" valign="top" >
                <td align="left" valign="top" class="updateInnerColumn">
                    Service tax applicable :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                    
                   <asp:RadioButtonList ID="rblservicetax" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem Selected="True" Text="Yes" Value="Yes"></asp:ListItem>
                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                   </asp:RadioButtonList>
                   
                </td>
             </tr>
             
             
             <tr valign="top" align="left" class="updateInnerColumn">
                <td align="left" valign="top" style="height:5px;">
                </td>
             </tr>
             <tr valign="top" align="center" >
                <td valign="top" align="left">
                </td>
                <td valign="top" align="left" class="updateInnerColumn">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" 
                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                        &nbsp;&nbsp;
                    <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="BACK" TabIndex="8"
                    CssClass="btnCls" onclick="btnBack_Click" />    
                        <%--<input id="btnReset" type="button" value="Reset" tabindex="4" class="btnCls" onclick="return FunResetInspbr();" />--%>
                </td>
            </tr>
    </table>

</div>

</asp:Content>

