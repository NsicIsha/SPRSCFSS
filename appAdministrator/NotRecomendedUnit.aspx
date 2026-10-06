<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="NotRecomendedUnit.aspx.cs" Inherits="appAdministrator_NotRecomendedUnit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<meta http-equiv="x-ua-compatible" content="IE=8" />
<script type="text/javascript" language="javascript">
    
    //AABCE0803B
    
    function onlyNumbers(e) {
        
         var unicode = e.charCode ? e.charCode : e.keyCode;
        if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57) || unicode == 46) {
           return true;
        }
        else {
          return false;
        }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
        <asp:Label ID="lblMonetary" runat="server" Text="Not Recomended Unit Details" CssClass="headerAll"></asp:Label>
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
                                               <%if(monetaryAction==1){ %>
                                                
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
                                    <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" />
                                    <asp:HiddenField ID="HFieldformtypeid" runat="server" />
                                    
                                    <asp:HiddenField ID="hdnfactcnt" runat="server" />
                                    <asp:HiddenField ID="hdnprovisionl" runat="server" />

                                      <asp:UpdatePanel ID="UpdatePanelmonetary" runat="server">
                                        <ContentTemplate>
                                  <asp:GridView ID="gvMonetaryDetails" runat="server" AutoGenerateColumns="false"
                                    Width="100%" BorderWidth="0px" CellPadding="5" CellSpacing="2"  
                                        
                                        EnableModelValidation="false" 
                                        EnableSortingAndPagingCallbacks="True" OnRowCommand="gvMonetaryDetails_RowCommand">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                           <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="6%">
                                    <ItemTemplate><%#Container.DataItemIndex+1 %>
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>
                                             
                                            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" HeaderStyle-Width="35%" />
                                            <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderStyle-Width="10%" HeaderText="PAN No."/>
                                             <asp:BoundField DataField="fGPtbl_PD_UnitAmount" HeaderStyle-Width="10%" HeaderText="Nsic Fee"/>
                                                                                
                                             <asp:BoundField DataField="fGPtbl_IS_Remarks" HeaderStyle-Width="35%" HeaderText="Inspection Agency Remarks" />

                                             <asp:BoundField DataField="fGPtbl_TIAB_Name" HeaderStyle-Width="35%" HeaderText="Inspection Done By" />

                                            <asp:TemplateField HeaderText="Action" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbcalculate" runat="server" CommandName="Close" CommandArgument='<%# Eval("fGPtbl_CUnit_Id") %>' CausesValidation="false">Close</asp:LinkButton>
                                                </ItemTemplate>
                                             </asp:TemplateField>

                                           </Columns>
                                        
                                    </asp:GridView>
                                    
                                     </ContentTemplate>
                                    </asp:UpdatePanel>
                                    
                                    </td>
                                </tr>
                        <tr>
                            <td>
                                 <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelmonetary">
                                        <ProgressTemplate>
                                            <asp:Panel ID="pnlMsg" runat="server">
                                                <table align="center" style="border: 2; background-repeat: no-repeat; background-position: center; position:absolute;
                                                    left: 450px; top: 250px; height: 10px; width: 200px; z-index: 10000; background-color: #a9dceb;">
                                                    <tr>
                                                        <td valign="top" align="center" style="color: Black; font-size: 12px; font-weight: bold;">
                                                             Please wait.....
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                            </td>
                        </tr>
                        </table>
                      <table style="padding:15px" cellpadding="5" cellspacing="5" border="1" width="100%"   runat="server" id="tableRemarks" visible="false">
                                <tr >
                                <td align="right">
                                   Company Name :
                                </td>
                                    <td align="left">
                                   <asp:Label ID="lblCompanyName" runat="server"></asp:Label>
                                </td>
                            </tr>
                          <tr>
                                <td align="right">
                                   PAN :
                                </td>
                                    <td align="left">
                                   <asp:Label ID="lblPAN" runat="server"></asp:Label>
                                </td>
                            </tr>

                          <tr>
                                <td align="right">
                                   NSIC Fee :
                                </td>
                                    <td align="left">
                                   <asp:Label ID="lblNsicFee" runat="server"></asp:Label>
                                </td>
                            </tr>
                          <tr>
                                <td align="right">
                                  Status :
                                </td>
                                    <td align="left">
                                   <asp:RadioButtonList  ID="rdbForfeitStatus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Text="Forfeited "></asp:ListItem>
                    <asp:ListItem Value="2" Text="Not Forfeited"></asp:ListItem>
                </asp:RadioButtonList>

                                         <asp:RequiredFieldValidator ValidationGroup="1"  ID="Rfdremarks" Display="Dynamic" runat="server" ControlToValidate="rdbForfeitStatus" ErrorMessage="Forfeit Status Required"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="center" colspan="2">
                                   <asp:Button ID="btnSubmitUpload" UseSubmitBehavior="false" runat="server" 
                            Text="Submit" CssClass="btnCls" 
                            ValidationGroup="1" onclick="btnSubmitUpload_Click" />

                                    <asp:Button ID="Button1" CausesValidation="false" runat="server" 
                            Text="Back" CssClass="btnCls" OnClick="Button1_Click"/>

                                </td>
                            </tr>
                       </table>  
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>
