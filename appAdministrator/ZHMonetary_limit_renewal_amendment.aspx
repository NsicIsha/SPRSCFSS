<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ZHMonetary_limit_renewal_amendment.aspx.cs" Inherits="appAdministrator_ZHMonetary_limit_renewal_amendment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <script type="text/javascript" language="javascript">
        function FunResetMonetary() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtMonetary").value = "";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div align="left">
        <asp:Label ID="lblMonetary" runat="server" 
            Text="Monetary Limit for Registration" CssClass="headerAll"></asp:Label>
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvMonetary" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewMonetaryNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
							<tr align="left" valign="top" style="width:100%;">
								<td align="center" valign="top" style="width:100%;">
								<asp:Label ID="lblMessage" runat="server"></asp:Label>
								</td>
							</tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewMonetaryDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(monetzonalAction==1){ %>
                                                
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
                                    <asp:HiddenField ID="HiddenFieldUnitid" runat="server" />
                                    <asp:HiddenField ID="HFieldformtypeid" runat="server" />
                                    <asp:HiddenField ID="HFMonetaryid" runat="server" />
                                    <asp:HiddenField ID="HFMonetarylimit" runat="server" />
                                    <asp:HiddenField ID="HDCOmpanyid" runat="server" />
                                    
                                    
                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlMsg" runat="server">
                                                <table align="center" style="border: 2; background-repeat: no-repeat; background-position: center; position:absolute;
                                                    left: 200px; top: 200px; height: 100px; width: 200px; z-index: 10000; background-color: #a9dceb;">
                                                    <tr>
                                                        <td valign="top" align="center" style="color: Black; font-size: 12px; font-weight: bold;">
                                                            Processing .........
                                                            <br /><br />
                                                            Please wait.....
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    
                                    
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        
                                    
                                    <asp:GridView ID="gvMonetaryDetails" runat="server" AutoGenerateColumns="false"
                                    Width="100%" BorderWidth="0px" CellPadding="1" CellSpacing="1" 
                                        onrowcommand="gvMonetaryDetails_RowCommand" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             
                                          <%--  <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
                                           <%--<asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" />--%>
                                            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                                            <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                                            <%--<asp:BoundField DataField="fGPtbl_MF_Name" HeaderText="Type" />--%>
                                            <%--<asp:BoundField DataField="fGPtbl_Company_ML_MonetoryLimit" HeaderText="Monetary limit" />--%>
                                            <asp:TemplateField HeaderText="Accept" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" Text="Accept"
                                                    CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CommandName="accept" ToolTip="Accept this unit" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Set Monetary Limit" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbButton" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                        AlternateText="SET" CommandName="edp" ToolTip="Set monetary limit" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="40px" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                           </Columns>
                                        
                                    </asp:GridView>
                                    
                                    
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvMonetaryDetails" EventName="Rowcommand" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    
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
    
    <asp:Panel ID="pnlOut" runat="server" CssClass="outerPopup">
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
                                <asp:GridView ID="grdTurnOver" runat="server" AutoGenerateColumns="false" 
                                    >
                               <Columns >
                               <asp:TemplateField HeaderText="Unit" ItemStyle-Width="80" ControlStyle-Width="80"  >
                               <ItemTemplate >
                               <asp:HiddenField ID="hdnuntid" runat="server" Value='<%# Bind("fGPtbl_CUnit_ML_UnitId") %>' />
                               <asp:Label ID="lbl_Unit" runat="server" Visible="true" Width="200" Font-Bold="true" 
                               Font-Size="XX-Small" Text='<%# Bind("fGPtbl_CUnit_1B_Factory_Address") %>' ></asp:Label>
                               </ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Monetary Limit" ItemStyle-Width="100" ControlStyle-Width="50">
                               <ItemTemplate >
                               <asp:TextBox ID="txt_limit" runat="server" Width="50" Text='<%# Bind("fGPtbl_CUnit_ML_MonetoryLimit") %>' ></asp:TextBox>
                              
                               </ItemTemplate>                                                              
                               </asp:TemplateField>
                                                      
                               </Columns>
                               <HeaderStyle Font-Size="XX-Small" Font-Bold="false" />
                              
                                                           
                            </asp:GridView>
                            
                                    
                                </td>
                              </tr>
                              
                               <tr valign="top" align="center" >
                               
                                <td valign="top" align="left" colspan="2" class="updateInnerColumn">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="2" 
                                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" tabindex="3" class="btnCls" onclick="return FunResetMonetary();" />
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
    <cc1:RoundedCornersExtender ID="RoundedCornersExtender1" TargetControlID="pnlInner"
        runat="server" BorderColor="#22518F" Radius="10" Corners="All">
    </cc1:RoundedCornersExtender>
    <asp:LinkButton ID="LinkButton1" runat="server" style="display:none;"></asp:LinkButton>

</asp:Content>

