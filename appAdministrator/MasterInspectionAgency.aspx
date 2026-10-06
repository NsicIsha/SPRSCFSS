<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInspectionAgency.aspx.cs" Inherits="appAdministrator_MasterInspectionAgency" Title="NSIC SPR ::Inspection Agencies Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript">
    function FunActiveConfirm(obj) {
            if (obj.title == 'Activate This') {
                if (confirm('Are you sure you want to ACTIVATE this InspectionAgency?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {

                if (confirm('Are you sure you want to DEACTIVATE this InspectionAgency?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>

</asp:Content><%--dAD--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">
    <asp:Label ID="lblInsp" runat="server" Text="Inspection Agencies Master" CssClass="headerAll"></asp:Label>
    <div align="right">
        <asp:Button ID="btnAddInsp" Text="Add" CausesValidation="false" 
        runat="server" CssClass="btnCls" onclick="btnAddInsp_Click" />
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvInsp" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewInspNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewInspDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(inspAction==1){ %>
                                                
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
                                    <asp:GridView ID="gvInspDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowcommand="gvInspDetails_RowCommand" 
                                        onrowdatabound="gvInspDetails_RowDataBound" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fGPtbl_MIA_Name" HeaderText="NAME" />
                                            <asp:BoundField DataField="fGPtbl_MIA_Desc" HeaderText="DESCRIPTION" />
                                            <asp:BoundField DataField="fGPtbl_MIA_AccessId" HeaderText="Active/ Deactive" HeaderStyle-Width="60px"/>
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit" CommandName="edp" ToolTip="Edit InspectionAgency" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_MIA_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activate/ Deactivate" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbAct" runat="server" CommandName="Act" CommandArgument='<%#Eval("fGPtbl_MIA_Id") %>' ImageAlign="Middle" OnClientClick="return FunActiveConfirm(this);" />
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
            <td align="left" style="padding-left:10px;">
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
                            Shows deactivated InspectionAgency.
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
    
</div>

<asp:Panel ID="pnlOut" runat="server" style="display:none;" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
            <center>
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr valign="top" align="left" style="width:100%;">
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width:100%">
                                    <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width:100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">
                                                        <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;
                                                        <asp:Label ID="lblInspname" runat="server" CssClass="header"></asp:Label>
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
                                
                                 <tr align="left" valign="top" style="width:100%;">
                                    <td colspan="2" align="right" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr valign="top" align="left" >
                                    <td valign="top" align="left" style="width:100px;" class="updateInnerColumn">
                                        Name :
                                    </td>
                                    <td valign="top" align="left"class="updateInnerColumn">
                                        <asp:TextBox ID="txtInspName" runat="server" MaxLength="200" TabIndex="1" CssClass="txt_bx"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr valign="top" align="left">
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        Description :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:TextBox ID="txtDescription" runat="server" MaxLength="2000" TabIndex="2" 
                                            CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                 </tr>
                                 <tr valign="top" align="left">
                                    <td align="left" valign="top" style="height:10px;">
                                    </td>
                                 </tr>
                                 <tr valign="top" align="center" >
                                    <td valign="top" align="left">
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="3" 
                                            ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                            &nbsp;&nbsp;
                                            <input id="btnReset" type="button" value="Reset" tabindex="4" class="btnCls" onclick="return FunResetInsp();" />
                                    </td>
                                </tr>
                        </table>
                    </td>
                </tr>
                </table>
            </center>
            <asp:RequiredFieldValidator ID="RFValidatorInspName" runat="server" ControlToValidate="txtInspName"
                SetFocusOnError="true" ErrorMessage="Please Fill InspectionAgency Name" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidatorInspName" runat="server" ErrorMessage="InspectionAgency Name should be less than 200 characters"
                Display="None" ControlToValidate="txtInspName" ClientValidationFunction="CharacterLimit200"
                SetFocusOnError="true" ValidationGroup="1"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidatorDescription" runat="server" ErrorMessage="Description should be less than 2000 characters"
                Display="None" ClientValidationFunction="CharacterLimit2000" ControlToValidate="txtDescription"
                ValidationGroup="1"></asp:CustomValidator>
                <cc1:ValidatorCalloutExtender ID="VCExtenderInspName" TargetControlID="RFValidatorInspName"
                runat="server">
            </cc1:ValidatorCalloutExtender>
            <cc1:ValidatorCalloutExtender ID="VCExtenderCustomName" TargetControlID="CustomValidatorInspName"
                runat="server">
            </cc1:ValidatorCalloutExtender>
            <cc1:ValidatorCalloutExtender ID="VCExtenderCustomDescription" TargetControlID="CustomValidatorDescription"
                runat="server">
            </cc1:ValidatorCalloutExtender>
        </asp:Panel>
    </asp:Panel>
    
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut" 
    TargetControlID="LinkButton1" BackgroundCssClass="modalBackground" CancelControlID="btnBack">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" runat="server" TargetControlID="pnlInner"
    BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    
    
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;">LinkButton</asp:LinkButton>
</asp:Content>

