<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="CompanyRequestList.aspx.cs" Inherits="appAdministrator_CompanyRequestList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %> <%--test--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function FunResetCertRequest() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtReason").value = "";
        }
    </script>

   <%-- sdfdsf--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <div align="left">
            <asp:label id="lblCertificateRequest" runat="server" text="Certificate Print Requests List" cssclass="headerAll"></asp:label>
        </div>

        <div align="left" style="padding-left: 10px; padding-right: 10px;">
            <asp:multiview id="mvCertRequest" runat="server" activeviewindex="0">
            <asp:View ID="ViewCertRequestNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                    
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewCertRequestDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if (requestAction == 1)
                                                { %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                        </div>
                                                        <div id="Action_DivInner_2" align="center">
                                                            <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                                                        </div>
                                                    </div>
                                                    <asp:RoundedCornersExtender
                                                        ID="RoundedCornersExtender2" runat="server"
                                                        TargetControlID="divAction"
                                                        BorderColor="#22518F"
                                                        Radius="10"
                                                        Corners="All"
                                                    ></asp:RoundedCornersExtender>
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
                                    <asp:HiddenField ID="HiddenCompanyName" runat="server" />
                                    <asp:HiddenField ID="HiddenCompanyId" runat="server" />
                                    <asp:GridView ID="gvCertRequestDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowcommand="gvCertRequestDetails_RowCommand" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ComapnyName" HeaderText="Company Name" />
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                            <asp:BoundField DataField="RequestDate" HeaderText="Request Date"/>

                                             <asp:BoundField DataField="RequestReason" HeaderText="Request Reason"/>


                                            <%--<asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/Admin/view.png"
                                                    AlternateText="View Reason" CommandName="view" ToolTip="View Reason" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_CR_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                           <%-- <asp:TemplateField HeaderText="ACCEPT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnAccept" runat="server" Text="Accept" CausesValidation="false" ToolTip="Accept This"
                                                    CommandArgument='<%#Eval("fGPtbl_CR_Id")+","+Eval("fGPtbl_CR_CertificateId") %>' CommandName="accept" CssClass="btnCls" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="REJECT" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnReject" runat="server" Text="Accept/Reject" CssClass="btnCls" ToolTip="Accept or Reject this request" CausesValidation="false"
                                                    CommandArgument='<%#Eval("Companyid")+","+Eval("ComapnyName") %>' CommandName="acceptreject" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           </Columns>
                                       </asp:GridView>
                                    </td>
                                </tr>
                        </table>
             
                </div>
            </asp:View>
        </asp:multiview>

        </div>
    </div>

    <asp:panel id="pnlOut" runat="server" cssclass="outerPopup">
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
                                   Company Name :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:Literal ID="ltlCertRequest" runat="server"></asp:Literal>
                                </td>
                              </tr>
                            <tr valign="top" align="left" >
                                <td valign="top" align="left" class="updateInnerColumn">
                                   Action :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:RadioButtonList ID="rdbAction" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="4" Text="Accept"></asp:ListItem>
                                          <asp:ListItem Value="3" Text="Reject"></asp:ListItem>
                                    </asp:RadioButtonList>

                                    <asp:RequiredFieldValidator ID="rfvrdbAction" runat="server" ControlToValidate="rdbAction"
                SetFocusOnError="true" ErrorMessage="Please select valid action" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>

                                    <asp:ValidatorCalloutExtender ID="VCOrfvrdbAction" TargetControlID="rfvrdbAction"
                runat="server"></asp:ValidatorCalloutExtender>


                                </td>
                              </tr>
                              <tr valign="top" align="left">
                                <td valign="top" align="left" class="updateInnerColumn" >
                                    Remarks :
                                </td>
                                <td valign="top" align="left" class="updateInnerColumn">
                                    <asp:TextBox ID="txtReason" runat="server" MaxLength="5000" 
                                        CssClass="txt_bx" TextMode="MultiLine"></asp:TextBox>
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
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click"/>
                                        &nbsp;&nbsp;
                                        <input id="btnReset" type="button" value="Reset" tabindex="4" class="btnCls" onclick="return FunResetCertRequest();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:RequiredFieldValidator ID="RFValidatorReason" runat="server" ControlToValidate="txtReason"
                SetFocusOnError="true" ErrorMessage="Please fill valid remarks" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidatorReason" runat="server" ErrorMessage="Remarks should be less than 5000 characters"
                Display="None" ClientValidationFunction="CharacterLimit5000" ControlToValidate="txtReason"
                ValidationGroup="1"></asp:CustomValidator>
             <asp:ValidatorCalloutExtender ID="VCExtenderRequired" TargetControlID="RFValidatorReason"
                runat="server"></asp:ValidatorCalloutExtender>
            <asp:ValidatorCalloutExtender ID="VCExtenderCustom" TargetControlID="CustomValidatorReason"
                runat="server"> </asp:ValidatorCalloutExtender>
           </center>
        </asp:Panel>
    </asp:panel>


    <asp:modalpopupextender id="ModalPopupExtender1" runat="server" popupcontrolid="pnlOut"
        targetcontrolid="LinkButton1" cancelcontrolid="btnBack" backgroundcssclass="modalBackground">
    </asp:modalpopupextender>
    <asp:roundedcornersextender id="RoundedCornersExtender1" targetcontrolid="pnlInner"
        runat="server" bordercolor="#22518F" radius="10" corners="All">
    </asp:roundedcornersextender>
    <asp:linkbutton id="LinkButton1" runat="server" style="display: none;"></asp:linkbutton>
</asp:Content>

