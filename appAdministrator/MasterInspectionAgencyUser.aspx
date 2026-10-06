<%@ Page Title="NSIC SPR :: Inspection Agencies Users Profile" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInspectionAgencyUser.aspx.cs" Inherits="appAdministrator_MasterInspectionAgencyUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    function FunActiveConfirm(obj) {
        if (obj.title == 'Activate This') {
            if (confirm('Are you sure you want to ACTIVATE this User?') == true) {
                return true;
            }
            else {
                return false;
            }
        }
        else {

            if (confirm('Are you sure you want to DEACTIVATE this User?') == true) {
                return true;
            }
            else {
                return false;
            }
        }
    }/*DASD*/
        
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div align="left">
        <div align="left">
            <asp:Label ID="lblUser" runat="server" Text="Inspection Agencies Users Profile" CssClass="headerAll"></asp:Label>
        </div>
        <div align="right" style="padding-right:10px; padding-bottom:10px;">
            <asp:Button ID="btnAddUser" Text="Add" CausesValidation="false" 
            runat="server" CssClass="btnCls" onclick="btnAddUser_Click" 
                UseSubmitBehavior="False"/>
        </div>
        
        <div align="left" style="padding-left: 10px; padding-right: 10px;">
        
            <asp:MultiView ID="mvUser" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewUserNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewUserDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%; padding-top:10px;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(userAction==1){ %>
                                                
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
                <div align="center" style="vertical-align:top; padding-bottom:10px;"><strong>Agency:</strong>
                    <asp:DropDownList ID="ddlAgency" runat="server" CssClass="clsDDL" 
                        onselectedindexchanged="ddlAgency_SelectedIndexChanged" 
                        AutoPostBack="True">
                    </asp:DropDownList>
                </div>
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:HiddenField ID="HdnfUserId" runat="server" />
                                    <asp:HiddenField ID="HdnfAddEdit" runat="server" />
                                    <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="2" CellSpacing="1" 
                                        AllowPaging="true" onrowcommand="gvUserDetails_RowCommand" >
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <EmptyDataTemplate>
                                            <div align="center">Nodata found</div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate><%#Container.DataItemIndex+1 %>
                                                    <%--<asp:Label ID="lblSno" runat="server"></asp:Label>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="AgencyName" HeaderText="Agency name" />
                                            <asp:BoundField DataField="fGPtbl_MIAU_Name" HeaderText="User name" />
                                            <asp:BoundField DataField="fGPtbl_MIAU_UserName" HeaderText="Login name" />
                                            <asp:BoundField DataField="fGPtbl_TIAB_Instructions" HeaderText="Description" />
                                           
                                            <%--<asp:BoundField DataField="AccessId" HeaderText="Active/ Deactive" />--%>
                                            <%--<asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="View"
                                                    CommandName="view" CommandArgument='<%#Eval("fGPtbl_MIAU_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    CommandName="edp" CommandArgument='<%#Eval("fGPtbl_MIAU_Id")%>' ToolTip="Edit User" ImageAlign="Middle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:TemplateField HeaderText="Activate/ Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbAct" runat="server" CommandName="Act" ImageAlign="Middle"
                                                    OnClientClick="return FunActiveConfirm(this);" CommandArgument='<%#Eval("fGPtbl_MIAU_Id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:TemplateField HeaderText="Reset" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbReset" runat="server" CommandName="Reset" CommandArgument='<%#Eval("fGPtbl_MIAU_Id")%>' ToolTip="Reset password" ImageUrl="~/Images/Admin/reset.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
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
                                            Shows deactivated User.
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
            <asp:View ID="viewedituser" runat="server">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr valign="top" align="left" >
                            <td align="left" valign="top" class="updateInnerColumn">
                                Inspection Agency :
                            </td>
                            <td align="left" valign="top" class="updateInnerColumn">
                               
                                <asp:DropDownList ID="DropDownListAgency" runat="server" CssClass="clsDDL" 
                                     TabIndex="1" Width="200px">
                                </asp:DropDownList>
                                        
                                <asp:RequiredFieldValidator ID="RFValidatorAgency" runat="server" ErrorMessage="Please select Agency"
                                ControlToValidate="DropDownListAgency" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" TargetControlID="RFValidatorAgency">
                                </cc1:ValidatorCalloutExtender>
                                
                                <asp:Label ID="lblAgency" runat="server" CssClass="header"></asp:Label>
                            </td>
                        </tr>
                       
                         <tr valign="top" align="left">
                            <td align="left" valign="top" class="updateInnerColumn">
                                Login name :
                            </td>
                            <td valign="top" align="left" class="updateInnerColumn">
                                <asp:TextBox ID="txtLoginame" runat="server" CssClass="txt_bx" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RFValidatorLogin" runat="server" ErrorMessage="Please provide login name"
                                ControlToValidate="txtLoginame" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" TargetControlID="RFValidatorLogin">
                                </cc1:ValidatorCalloutExtender>
                                <asp:Label ID="lblLoginame" runat="server" CssClass="header"></asp:Label>
                            </td>
                         </tr>
                         
                         <tr valign="top" align="left">
                            <td align="left" valign="top" class="updateInnerColumn">
                                Name :
                            </td>
                            <td valign="top" align="left" class="updateInnerColumn">
                                <asp:TextBox ID="txtName" runat="server" CssClass="txt_bx" TabIndex="3"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide login name"
                                ControlToValidate="txtName" ValidationGroup="1" SetFocusOnError="true" Display="None"></asp:RequiredFieldValidator>
                                <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" TargetControlID="RequiredFieldValidator1">
                                </cc1:ValidatorCalloutExtender>
                                <%--<asp:Label ID="lblLoginame" runat="server" CssClass="header"></asp:Label>--%>
                            </td>
                         </tr>
                         
                         <tr valign="top" align="left">
                            <td align="left" valign="top" class="updateInnerColumn">
                                Email-id :
                            </td>
                            <td valign="top" align="left" class="updateInnerColumn">
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="txt_bx" TabIndex="4"></asp:TextBox>
                                <%--<asp:Label ID="lblEmail" runat="server" CssClass="header"></asp:Label>--%>
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
                                Instruction :
                            </td>
                            <td align="left" valign="top" class="updateInnerColumn">
                                <asp:TextBox ID="txtInstr" runat="server" CssClass="txt_bx" TabIndex="5" TextMode="MultiLine" Rows="3" Width="250px"></asp:TextBox>
                                <%--<asp:Label ID="lblInstr" runat="server"></asp:Label>--%>
                                <asp:CustomValidator ID="CustomValidatorInstr" runat="server" ErrorMessage="Instruction should be less than 5000 characters"
                                    Display="None" ValidationGroup="1" ControlToValidate="txtInstr" ClientValidationFunction="CharacterLimit5000"
                                    SetFocusOnError="true" ></asp:CustomValidator>
                               <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="CustomValidatorInstr">
                                </cc1:ValidatorCalloutExtender>
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
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="6" 
                                    ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click" />
                                    &nbsp;&nbsp;
                                <asp:Button ID="btnBack" runat="server" CausesValidation="false" Text="BACK" TabIndex="7"
                                CssClass="btnCls" onclick="btnBack_Click" UseSubmitBehavior="False" />    
                                    <%--<input id="btnReset" type="button" value="Reset" tabindex="4" class="btnCls" onclick="return FunResetInspbr();" />--%>
                            </td>
                        </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        </div>
    </div>
</asp:Content>

