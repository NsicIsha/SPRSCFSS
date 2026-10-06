<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="WaitingCompaniesPaymentDetailsOnline.aspx.cs" Inherits="appAdministrator_WaitingCompaniesPaymentDetailsOnline" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    function newdocumentopen(x) {
        alert("sss");
     window.open(x, "NewWindow", "resizable=yes,scrollbars=yes");
 }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">
    <div align="left">
        <asp:Label ID="lblRegistration" runat="server" CssClass="headerAll"></asp:Label>
    </div>
    <div align="left" style="height:10px;">
        
    </div>
    <div align="center">
        <%if(companyAction==1){ %>
        <div style="height:30px;">
            <div id="divAction" runat="server" class="Action_Div" align="center" style="background-color:#ECF8FB; height:30px;">
                <div id="Action_DivInner_1" align="center">
                    <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                </div>
                <div id="Action_DivInner_2" align="center">
                    <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                </div>
            </div>
        </div>
        <cc1:RoundedCornersExtender
            ID="RoundedCornersExtender2" runat="server"
            TargetControlID="divAction"
            BorderColor="#22518F"
            Radius="10"
            Corners="All">
         </cc1:RoundedCornersExtender>
         <%} %>
    </div>
    <div align="center">
        <asp:Literal ID="ltlMessage" runat="server"></asp:Literal>
    </div>
     <div align="center">
    </div>
    
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:GridView ID="gvRegistrationDetails" runat="server" AutoGenerateColumns="false"
            Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
            onrowdatabound="gvRegistrationDetails_RowDataBound" 
            onrowcommand="gvRegistrationDetails_RowCommand"> 
            <HeaderStyle CssClass="dynRowHeader" />
            <RowStyle CssClass="dynRowEven" />
            <AlternatingRowStyle CssClass="dynRowOdd" />
            <Columns>
                <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <asp:Literal ID="ltlSno" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="GP Referenceno." />--%>
                <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
				 <asp:BoundField DataField="fGPtbl_Company_ProposalDate" HeaderText="Proposal Date"/>
				 
                
                 
                      


                <%--<asp:BoundField DataField="fGPtbl_CUnit_1B_Factory_Address" HeaderText="Factory Address" />--%>
                <%--<asp:TemplateField HeaderText="SEND MAIL" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                    <ItemTemplate>
                         
                       <asp:HiddenField ID="hdnbranchmail" runat="server" Value='<%#Eval("fGPtbl_MU_Email")%>' />
                            <asp:HiddenField ID="hdncompanyemail" runat="server" Value='<%#Eval("fGPtbl_Company_HeadOffice_Email")%>' />
                            
                            
                            <asp:HyperLink ID="lnkmail" runat="server" Text="Send Mail" Target="_blank" NavigateUrl=""></asp:HyperLink>
                        
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="View Payment Details" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Button ID="btnAccepttt" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="View" Width="200px" CommandName="ViewPaymentDetails" CommandArgument='<%# Eval("fGPtbl_Company_Id") + "," + Eval("fGPtbl_PD_Id")%>'   />

                         <asp:HiddenField ID="hdncid" runat="server" Value='<%#Eval("fGPtbl_Company_Id")%>' />
                 
                       <asp:HiddenField ID="hdnbranch" runat="server" Value='<%#Eval("Branchname")%>' />
                 
                       <asp:HiddenField ID="hdnbranchmail" runat="server" Value='<%#Eval("fGPtbl_MU_Email")%>' />
                 <asp:HiddenField ID="hdncompanyemail" runat="server" Value='<%#Eval("fGPtbl_Company_HeadOffice_Email")%>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reject" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <%-- <asp:Button ID="btnAccept" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Accept" Width="60px" CommandName="accept" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />--%>

                        <asp:Button ID="btnReject" runat="server" CssClass="btnCls" CausesValidation="false"
                            Text="Reject" Width="60px" CommandName="reject" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>


                
               </Columns>
        </asp:GridView>    
        
    </div>
    
     <asp:Panel ID="pnlOut" runat="server" Style="display: block;" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup" Height="300px">
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
                                                    <asp:HiddenField ID="hdncunitid" runat="server" />
                                                    &nbsp;&nbsp;</td>
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
                                <tr>
                                <td colspan="2" class="updateInnerColumn" > List of Factories</td>
                                </tr>
                                <tr valign="top" align="center">
                                
                                    <td align="left" colspan="2" valign="top" style="width:480px;" class="updateInnerColumn" >
                                       
                                        <asp:CheckBoxList ID="ChkListFactory" runat="server" Visible="false">
                                        </asp:CheckBoxList>
                                        
                                        <asp:Repeater ID="rptFactory" runat="server" 
                                            onitemdatabound="rptFactory_ItemDataBound" >
                                            <HeaderTemplate>
                                                <table cellpadding="3" cellspacing="0" border="1">
                                                    <tr>
                                                        <th style="width:150px;">
                                                            Factory
                                                        </th>
                                                        <th style="width:200px;">
                                                            Inspection agency
                                                        </th>
                                                          <th>
                                                        
                                                        </th>
                                                    </tr>
                                                 </HeaderTemplate>
                                                 <ItemTemplate>
                                                    <tr>
                                                        <td align="center" style="width:150px; padding-left:20px;">
                                                            <asp:Literal ID="ltlFactname" runat="server" Text='<%# Eval("fGPtbl_CUnit_1B_Factory_Address") %>' ></asp:Literal>
                                                            <asp:HiddenField ID="hdnFactId" runat="server" Value='<%#Eval("fGPtbl_CUnit_Id") %>' />
                                                        </td>
                                                        <td align="center" style="width:200px; padding-left:20px;">
                                                            <asp:DropDownList ID="ddlInspagency" CssClass="clsDDL" runat="server" ></asp:DropDownList>
                                                        </td>
                                                          <td align="center" style="width:130px;" >
                                                         <asp:LinkButton ID="lnk_cover" runat="server" Text="Cover Letter"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                 </ItemTemplate>
                                                 <FooterTemplate>
                                                    </table>
                                                 </FooterTemplate>
                                                

                                            
                                        </asp:Repeater>
                                        
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="4" ValidationGroup="1"
                                            CssClass="btnCls" OnClick="btnSubmit_Click" />
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
    <asp:LinkButton ID="LinkButton1" runat="server" Style="display: none;"></asp:LinkButton>    
    
    
    
    
</div>

</asp:Content>


