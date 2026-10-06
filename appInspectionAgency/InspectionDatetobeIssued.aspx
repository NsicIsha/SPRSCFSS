<%@ Page Title="Inspection Date to be Issued" Language="C#" MasterPageFile="~/appMaster/Agency.master"
    AutoEventWireup="true" CodeFile="InspectionDatetobeIssued.aspx.cs" Inherits="appInspectionAgency_InspectionDatetobeIssued" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<meta http-equiv="x-ua-compatible" content="IE=8" />
    <script language="javascript" type="text/javascript">
        function FunResetDate() {
            document.getElementById("ctl00_ContentPlaceHolder1_txtDate").value = "";
            document.getElementById("ctl00_ContentPlaceHolder1_txtDate").focus();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td align="left" class="headerAll">
                Companies waiting for Inspection date
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="400" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0" OnActiveViewChanged="MultiView1_ActiveViewChanged">
                
                
                
                    <asp:View ID="viewMessage" runat="server">
                    
                    
                    
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="center" valign="top" style="width: 100%;">
                                    <asp:HiddenField ID="HiddenFieldUnit" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldFormtype" runat="server" />
                                    <asp:Label ID="lblMsg" runat="server" CssClass="reportCenterText"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="viewData" runat="server">
                    
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(issueAction==1){ %>
                                                
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
                    
                        <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width: 100%;">
                                <td align="left" valign="top" style="width: 100%;">
                              
                                 
                                    <asp:GridView ID="GridView1" runat="server" Width="100%" BorderWidth="0" CellPadding="3"
                                        CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false"
                                        DataKeyNames="fGPtbl_IS_UnitId"
                                        AllowSorting="false" AllowPaging="false" OnRowDataBound="GridView1_RowDataBound"
                                        OnRowCommand="GridView1_RowCommand" EnableModelValidation="false" 
                                        EnableSortingAndPagingCallbacks="True" >
                                        <HeaderStyle CssClass="GrdviewHeader" />
                                        <RowStyle CssClass="GrdviewRowOdd" />
                                        <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %> 
                                                    <asp:HiddenField runat="server" ID="hdnUnitId" Value='<%# Eval("fGPtbl_IS_UnitId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnAgencyId" Value='<%# Eval("fGPtbl_IS_AgencyId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnFormtypeId" Value='<%# Eval("fGPtbl_IS_FormtypeId") %>' />
                                                    <asp:HiddenField runat="server" ID="hdnCompanyId" Value='<%# Eval("fGPtbl_CUnit_CompanyId") %>' />
                                                </ItemTemplate>
                                             </asp:TemplateField>
                                             
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_MF_Name"
                                                HeaderText="Type" />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_Name"
                                                HeaderText="Company"  HeaderStyle-Wrap="false"/>
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_CUnit_SSIRegistrationNo"
                                                HeaderText="SSI Registration No." />
                                            <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_Company_PAN"
                                                HeaderText="PAN" />

                                           <%-- <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_PD_PaymentType_inspection"
                                                HeaderText="Payment Type" />--%>

                                            <asp:BoundField   
                                                DataField="fGPtbl_PD_ChequeDDNo_inspection"
                                                HeaderText="DD/Cheque/UTR No." />
                                            <asp:BoundField  
                                                DataField="fGPtbl_PD_BankName_inspection"
                                                HeaderText="Bank Name" />
                                            <asp:BoundField    
                                                DataField="fGPtbl_PD_BankBranch_inspection"
                                                HeaderText="Branch" />
                                            <asp:BoundField   HtmlEncodeFormatString="true"  
                                                DataField="fGPtbl_PD_Cheque_DD_Date_inspection"
                                                HeaderText="Date(MM/DD/YYYY)" />
                                            





                                            <asp:BoundField HtmlEncode="false" HtmlEncodeFormatString="true" ReadOnly="true"
                                                DataField="fGPtbl_IS_DateStatus2"
                                                HeaderText="Request<br>Generation<br>Date" />
                                            <asp:TemplateField HeaderText="View<br>Details">
                                                <ItemTemplate>
                                               <%-- <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/Admin/view.png" border="0" CommandName="ViewDetails"
                                                        AlternateText="VIEW UNIT DETAILS" CausesValidation="false" ToolTip="VIEW UNIT DETAILS" />--%>
                                             <asp:HiddenField ID="hdncid" runat="server" Value='<%#Eval("fGPtbl_CUnit_CompanyId")%>' />
                        <%--<asp:HiddenField ID="hdnbranch" runat="server" Value='<%#Eval("Branchname")%>' />--%>
                            <asp:HyperLink ID="lnkview" runat="server" Target="_blank" Text="View Form" NavigateUrl="" ></asp:HyperLink>
                            
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue<br>Inspection<br>Date">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnIssue" runat="server" ImageUrl="~/Images/Admin/Date-and-Time-icon.png" border="0"
                                                    CommandArgument='<%# Eval("fGPtbl_IS_UnitId") %>' CommandName="ISSUEDATE"  
                                                        AlternateText="ASSIGN INSPECTION DATE"
                                                        ToolTip="ASSIGN INSPECTION DATE"   CausesValidation="false"  EnableViewState="true"/>
                                                        
                                                       <asp:HiddenField ID="fgtbl_unit_id" runat="server" Value= '<%# Eval("fGPtbl_IS_UnitId") %>' />
                                                        
                                             </ItemTemplate>
                                             </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                 
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlOuterEdit" runat="server" Style="display: none;" CssClass="outerPopup">
        <asp:Panel ID="pnlInnerEdit" runat="server" CssClass="innerPopup" Width="400">
            <table cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                <tr align="center" valign="top" style="width: 100%;">
                    <td align="center" valign="top" style="width: 100%; padding-left: 5px; padding-right: 5px;
                        padding-top: 0px; padding-bottom: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                            <tr align="left" valign="top">
                                <td colspan="2" align="left" valign="top" style="width:100%; padding-bottom:10px;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top">
                                            <td align="center" valign="top" class="updateInnerHeader">
                                                <asp:Label ID="lblMesgPnl" runat="server" CssClass="header"></asp:Label>
                                            </td>
                                            <td align="right" valign="top" style="width:20px;">
                                                <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="imgCloseEdit"
                                                runat="server" ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD" style="width:100px;">
                                    Date:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass="txt_bx" MaxLength="200" ValidationGroup="12"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true" Display="None" 
                                        ValidationGroup="12"
                                        ErrorMessage="Please provide INSPECTION DATE"
                                        ControlToValidate="txtDate"
                                    ></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server"
                                        TargetControlID="RequiredFieldValidator4"
                                    ></asp:ValidatorCalloutExtender>
                                    
                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txtDate" MaskType="Date"
                                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true" ClearTextOnInvalid="true">
                                    </asp:MaskedEditExtender>
                                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txtDate" ControlExtender="MaskedEditExtender1"
                                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required" ></asp:MaskedEditValidator>
                    
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" style="font-weight: bold;">
                                </td>
                                <td align="left" class="modalTDbutton">
                                    <asp:Button ID="btnSubmitEdit" runat="server" Text="Submit" CssClass="btnCls" ValidationGroup="12" onclick="btnSubmitEdit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <input id="btnResetEdit" type="button" value="Reset" class="btnCls" onclick="FunResetDate()" />
                                </td>
                            </tr>
                            
                            
                            <tr align="left" valign="top">
                                <td colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" DropShadow="false"
        TargetControlID="lnkModalClick"
        PopupControlID="pnlOuterEdit"
        CancelControlID="imgCloseEdit"
    ></asp:ModalPopupExtender>
    <asp:RoundedCornersExtender ID="RoundedCornersExtender3" runat="server" Radius="10" Corners="All" BorderColor="#000000"
        TargetControlID="pnlInnerEdit"
    ></asp:RoundedCornersExtender>
    <asp:LinkButton ID="lnkModalClick" runat="server" style="display:none;">LinkButton</asp:LinkButton>
</asp:Content>
