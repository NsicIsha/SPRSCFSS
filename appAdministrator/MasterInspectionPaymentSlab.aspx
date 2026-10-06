<%@ Page Title="NSIC SPR ::Inspection Fees Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInspectionPaymentSlab.aspx.cs" Inherits="appAdministrator_MasterInspectionPaymentSlab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript">
        function FunActiveConfirm(obj) {
            if (obj.title == 'Delete this') {
                if (confirm('Are you sure you want to Delete this InspectionAgency Fee?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script><%--SADSA--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">
    <div align="left">
        <asp:Label ID="lblPayment" runat="server" Text="Inspection Fees Master" CssClass="headerAll"></asp:Label>
    </div>

    <div align="right">
        <asp:Button ID="btnAddIAFee" Text="Add" CausesValidation="false" 
        runat="server" CssClass="btnCls" onclick="btnAddIAFee_Click" />
    </div>
    <div align="left" style="height:10px;">
    &nbsp;
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
    
    <table border="0" cellpadding="0" cellspacing="1" width="100%" style="background-color: #C6E7F0;">
                <tr valign="top">
                    <td colspan="4" align="left" valign="middle" class="TopHeading">
                        Search Box
                    </td>
                </tr>
                <tr valign="top">
                    <td align="left" valign="middle" style="width:150px;" class="reportCell">
                        InspectionAgency Name :
                    </td>
                    <td align="left" valign="middle" style="width:100px;" class="reportCell">
                        <asp:DropDownList ID="DropDownListIAgency" runat="server" CssClass="clsDDL" Width="200px">
                        </asp:DropDownList>
                        
                    </td>
                    <td align="left" valign="middle" style="width:80px;" class="reportCell">
                        Form Name :
                    </td>
                    <td align="left" valign="middle" style="width:195px;" class="reportCell">
                        <asp:DropDownList ID="DropDownListFormName" runat="server" CssClass="clsDDL" Width="200px">
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr valign="top">
                    <td colspan="4" align="right" valign="middle" style="background-color: #DDF1F7; padding-right:5px;
                        height: 50px; padding-left: 5px; font-weight: bold; color: #22518F;">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnCls" 
                            onclick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
    
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvIAFee" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewIAFeeNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewIAFeeDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(iafeeAction==1){ %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                        </div>
                                                        <div id="Action_DivInner_2" align="center">
                                                            <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                                                        </div>
                                                    </div>
                                                    <cc1:RoundedCornersExtender
                                                        ID="RoundedCornersExtender2" runat="server"
                                                        TargetControlID="divAction"
                                                        BorderColor="#22518F"
                                                        Radius="10"
                                                        Corners="All"
                                                    ></cc1:RoundedCornersExtender>
                                                <%} %>
                                            </td>
                                            <td valign="top" align="right" style="width: 30%;">
                                            </td>
                                        </tr>
                                    </table>
                                    
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:GridView ID="gvIAFeeDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowdatabound="gvIAFeeDetails_RowDataBound" 
                                        onrowcommand="gvIAFeeDetails_RowCommand">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="ltlSno" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AgencyName" HeaderText="AGENCY NAME" />
                                            <asp:BoundField DataField="FormTypeName" HeaderText="FORM NAME" />
                                            <asp:BoundField DataField="fGPtbl_IPS_SlabName" HeaderText="SLAB NAME" />
                                            <asp:BoundField DataField="fGPtbl_IPS_Amount" HeaderText="AMOUNT" />
                                            <asp:BoundField DataField="fGPtbl_IPS_Instructions" HeaderText="INSTRUCTION" />
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit" CausesValidation="false" CommandName="edp" ToolTip="Edit Fee" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_IPS_Id") %>' />
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DELETE" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbDelete" runat="server" CommandName="dte" CausesValidation="false" ToolTip="Delete this" ImageUrl="~/Images/Admin/delete.png" OnClientClick="return FunActiveConfirm(this);" CommandArgument='<%#Eval("fGPtbl_IPS_Id") %>' />
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
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>  
                </div>
            </asp:View>
        </asp:MultiView>
        
    </div>
    
 </div>
 
 <asp:Panel ID="pnlOut" runat="server" style="display:none;" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr valign="top" align="left" style="width:100%;">
                    <td valign="top" align="left" style="width:100%;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr valign="top" align="left" style="width:100%">
                                <td valign="top" align="right" colspan="2" style="padding-bottom:10px; padding-right:5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                    <tr align="left" valign="top">
                                                        <td align="center" valign="top" class="updateInnerHeader">
                                                            <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>
                                                            <asp:HiddenField ID="hdnfield" runat="server" />
                                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                                        </td>
                                                        <td align="right" valign="top" style="width: 20px;">
                                                            <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="btnBack" runat="server"
                                                                ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                                        </td>
                                                    </tr>
                                                </table>
                                    
                                </td>
                            </tr>
                            
                            <tr align="left" valign="top" style="width:100%;">
                                <td colspan="2" align="right" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr valign="top" align="left" >
                                <td valign="top" align="left" class="updateInnerColumn">
                                    Agency Name :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:DropDownList ID="DropDownListAgency" runat="server" CssClass="clsDDL" ValidationGroup="1" Width="200px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblAgency" runat="server"></asp:Label>
                                </td>
                              </tr>
                              <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Form Name :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:DropDownList ID="DropDownListForm" runat="server" CssClass="clsDDL" Width="200px" ValidationGroup="1">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblForm" runat="server"></asp:Label>
                                 </td>
                               </tr>
                               
                               <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Slab Name :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtSlab" runat="server" MaxLength="500" CssClass="txt_bx" TabIndex="1"></asp:TextBox>
                                 </td>
                               </tr>
                               
                               <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Amount :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="txt_bx" TabIndex="2"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtAmount" FilterType="Numbers,Custom" ValidChars=".0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please fill decimal number" ValidationGroup="1" ValidationExpression="[0-9]+(\.[0-9][0-9]?)?"
                                    ControlToValidate="txtAmount" Display="None"></asp:RegularExpressionValidator>
                                 </td>
                               </tr>
                               
                               <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Instruction :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtInstruction" runat="server" TextMode="MultiLine" MaxLength="5000" CssClass="txt_bx" Rows="3" TabIndex="3"></asp:TextBox>
                                 </td>
                               </tr>
                               
                               <tr valign="top" align="left">
                                <td align="left" valign="top" >
                                </td>
                               </tr>
                               <tr valign="top" align="center" >
                                <td valign="top" align="left" >
                                </td>
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" 
                                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" tabindex="5" class="btnCls" onclick="return FunResetInspfee();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="RFValidatorSlab" runat="server" ControlToValidate="txtSlab"
                SetFocusOnError="true" ErrorMessage="Please Fill Slab Name" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RFValidatorAmount" runat="server" ErrorMessage="Please provide Amount"
                SetFocusOnError="true" ControlToValidate="txtAmount" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Instruction should less than 5000 characters"
                ClientValidationFunction="CharacterLimit5000" SetFocusOnError="true" Display="None" ControlToValidate="txtInstruction"></asp:CustomValidator>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidatoragencyddl" runat="server" ErrorMessage="Please select agency name"
                ControlToValidate="DropDownListAgency" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                
                <asp:RequiredFieldValidator ID="RFValidatorformddl" runat="server" ErrorMessage="Please select form name"
                ControlToValidate="DropDownListForm" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
             <cc1:ValidatorCalloutExtender ID="VCExtenderSlab" TargetControlID="RFValidatorSlab"
                runat="server"></cc1:ValidatorCalloutExtender><cc1:ValidatorCalloutExtender ID="VCExtenderAmount" TargetControlID="RFValidatorAmount"
                runat="server"> </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RegularExpressionValidator1" runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="CustomValidator1" runat="server">
                </cc1:ValidatorCalloutExtender>
                
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidatoragencyddl" runat="server">
                </cc1:ValidatorCalloutExtender>
                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RFValidatorformddl" runat="server">
                </cc1:ValidatorCalloutExtender>
            
           </center>
        </asp:Panel>
    </asp:Panel>
    
 <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton></asp:Content>