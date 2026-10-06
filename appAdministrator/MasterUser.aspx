<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterUser.aspx.cs" Inherits="appAdministrator_MasterUser" Title="NSIC SPR::Users Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script type="text/javascript" language="javascript"> /*df*/
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
        }
        
        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
        <asp:Label ID="lblUser" runat="server" Text="Users Master" CssClass="headerAll"></asp:Label>
    </div>
    <div align="right" style="padding-right:10px; padding-bottom:10px;">
        <asp:Button ID="btnAddUser" Text="Add" CausesValidation="false" 
        runat="server" CssClass="btnCls" onclick="btnAddUser_Click" />
    </div>
    
     <div align="left" style="padding-left: 10px; padding-right: 10px;">
     
        <table border="0" cellpadding="0" cellspacing="1" width="100%" style="background-color: #C6E7F0;">
                                        <tr valign="top">
                                            <td colspan="4" align="left" valign="middle" class="TopHeading">
                                                Search Box
                                            </td>
                                        </tr>
                                        <tr valign="top">
                                            <td align="left" valign="middle" style="width:80px;" class="reportCell">
                                                User :
                                            </td>
                                            <td align="left" valign="middle" style="width:195px;" class="reportCell">
                                                <asp:TextBox ID="txtUser" runat="server" autocomplete="off" CssClass="txt_bx" MaxLength="50" Width="123px"></asp:TextBox>
                                                <%--..........................................................................--%>    
                                                <%--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--%>
                                                <%--||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||--%>
                                                <%--Start:::AutoComplete Code--%>
                                                <%--..........................................................................--%>
                                                <cc1:AutoCompleteExtender
                                                    runat="server" 
                                                    BehaviorID="AutoCompleteEx"
                                                    ID="AutoCompleteExtender1" 
                                                    TargetControlID="txtUser"
                                                    ServicePath="~/appAdministrator/WebAdministrator.asmx"
                                                    ServiceMethod="GetCompletionList"
                                                    MinimumPrefixLength="1" 
                                                    CompletionInterval="500"
                                                    EnableCaching="true"
                                                    FirstRowSelected="true"
                                                    CompletionSetCount="8"
                                                    CompletionListCssClass="autocomplete_completionListElement" 
                                                    CompletionListItemCssClass="autocomplete_listItem" 
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                                    DelimiterCharacters=";, :">
                                                    <Animations>
                                                        <OnShow>
                                                            <Sequence>
                                                                <%-- Make the completion list transparent and then show it --%>
                                                                <OpacityAction Opacity="0" />
                                                                <HideAction Visible="true" />
                                                                
                                                                <%--Cache the original size of the completion list the first time
                                                                    the animation is played and then set it to zero --%>
                                                                <ScriptAction Script="
                                                                    // Cache the size and setup the initial size
                                                                    var behavior = $find('AutoCompleteEx');
                                                                    if (!behavior._height) {
                                                                        var target = behavior.get_completionList();
                                                                        behavior._height = target.offsetHeight - 2;
                                                                        target.style.height = '0px';
                                                                    }" />
                                                                
                                                                <%-- Expand from 0px to the appropriate size while fading in --%>
                                                                <Parallel Duration=".4">
                                                                    <FadeIn />
                                                                    <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                                                                </Parallel>
                                                            </Sequence>
                                                        </OnShow>
                                                        <OnHide>
                                                            <%-- Collapse down to 0px and fade out --%>
                                                            <Parallel Duration=".4">
                                                                <FadeOut />
                                                                <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                                                            </Parallel>
                                                        </OnHide>
                                                    </Animations>
                                                </cc1:AutoCompleteExtender>
                                                <%--<script type="text/javascript">
                                                    // Work around browser behavior of "auto-submitting" simple forms
                                                    var frm = document.getElementById("aspnetForm");
                                                    if (frm) {
                                                        frm.onsubmit = function() { return false; };
                                                    }
                                                </script>--%>
                                                <%-- Prevent enter in textbox from causing the collapsible panel from operating --%>
                                                <%--<input type="submit" style="display:none;" />--%>
                                                <%--..........................................................................--%>
                                                <%--Start:::AutoComplete Code--%>
                                                <%--||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||--%>
                                                <%--++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++--%>
                                                <%--..........................................................................--%>
                                            </td>
                                            <td align="left" valign="middle" style="width:80px;" class="reportCell">
                                                Role :
                                            </td>
                                            <td align="left" valign="middle" style="width:195px;" class="reportCell">
                                                <asp:DropDownList ID="DropDownListRole" runat="server" CssClass="clsDDL" CausesValidation="True" TabIndex="1" ValidationGroup="1">
                                                    
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        
                                        <tr valign="top">
                                            <td colspan="4" align="right" valign="middle" style="background-color: #DDF1F7; padding-right:5px;
                                                height: 50px; padding-left: 5px; font-weight: bold; color: #22518F;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnCls" 
                                                    TabIndex="9" ValidationGroup="1" onclick="btnSearch_Click" />
                                            </td>
                                        </tr>
                                    </table>
        
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
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="false"
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowdatabound="gvUserDetails_RowDataBound" 
                                        onrowcommand="gvUserDetails_RowCommand" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ZoneName" HeaderText="ZONE" />
                                            <asp:BoundField DataField="BranchName" HeaderText="BRANCH" />
                                            <asp:BoundField DataField="fGPtbl_MU_Name" HeaderText="LOGIN ID" />
                                            <asp:BoundField DataField="RoleName" HeaderText="ROLE" />
                                           
                                            <asp:BoundField DataField="fGPtbl_MU_AccessId" HeaderText="Active/ Deactive" />
                                            <asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                    CommandName="view" CommandArgument='<%#Eval("fGPtbl_MU_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                    <asp:HiddenField ID="hdnRoleId" Value='<%#Eval("RoleId")%>' runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    CommandName="edt" CommandArgument='<%#Eval("fGPtbl_MU_Id")%>' ToolTip="Edit User" ImageAlign="Middle" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activate/ Deactivate" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbAct" runat="server" CommandName="Act" ImageAlign="Middle"
                                                    OnClientClick="return FunActiveConfirm(this);" CommandArgument='<%#Eval("fGPtbl_MU_Id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reset" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbReset" runat="server" CommandName="Reset" CommandArgument='<%#Eval("fGPtbl_MU_Name")%>' ToolTip="Reset password" ImageUrl="~/Images/Admin/reset.png" />
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
        </asp:MultiView>
        
    </div>

        

</asp:Content>

