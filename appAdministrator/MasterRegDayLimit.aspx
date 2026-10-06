<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterRegDayLimit.aspx.cs" Inherits="appAdministrator_MasterRegDayLimit" Title="GP::RegDays Limit Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">

<asp:Label ID="lblRegday1" runat="server" Text="Master Registration DaysLimit" 
        CssClass="headerAll"></asp:Label>
        
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvRegday" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewRegdayNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewRegdayDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if (regdayAction == 1){ %>
                                                
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
                                    <asp:GridView ID="gvRegdayDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowcommand="gvRegdayDetails_RowCommand" 
                                        onrowdatabound="gvRegdayDetails_RowDataBound" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Literal ID="ltlSno" runat="server"></asp:Literal>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fGPtbl_MRDL_Days" HeaderText="REGISTRATION DAYS" />
                                           
                                            
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                    AlternateText="Edit" CommandName="edp" ToolTip="Edit Days" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_MRDL_Id") %>' />
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
                        <td valign="top" align="left" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr valign="top" align="left" style="width:100%">
                                    <td valign="top" align="right" colspan="2" style="padding-right: 5px;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr align="left" valign="top" style="width:100%;">
                                                    <td align="center" valign="top" class="updateInnerHeader">
                                                        <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;
                                                        <asp:Label ID="lblRegday" runat="server" CssClass="header"></asp:Label>
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
                                <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Registration Days :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtReg" runat="server" CssClass="txt_bx" TabIndex="1" ValidationGroup="1"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtReg" FilterType="Numbers">
                                    </cc1:FilteredTextBoxExtender>
                                    <asp:Label ID="lblReg" runat="server"></asp:Label>
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="2" 
                                            ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click" />
                                            &nbsp;&nbsp;
                                            <input id="btnReset" type="button" value="Reset" tabindex="3" class="btnCls" onclick="return FunResetReg();" />
                                    </td>
                                </tr>
                        </table>
                    </td>
                </tr>
                </table>
            </center>
             <asp:RequiredFieldValidator ID="RFValidatorReg" runat="server" ControlToValidate="txtReg"
                SetFocusOnError="true" ErrorMessage="Please Registration Days" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                
            <cc1:ValidatorCalloutExtender ID="VCExtenderReg" TargetControlID="RFValidatorReg"
                runat="server"></cc1:ValidatorCalloutExtender>
            
            
        </asp:Panel>
    </asp:Panel>
    
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOut"
        TargetControlID="LinkButton1" CancelControlID="btnBack" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>


</asp:Content>

