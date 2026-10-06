<%@ Page Title="GP::Inspection Agency Branch Master" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInspectionAgencyBranch.aspx.cs" Inherits="appAdministrator_MasterInspectionAgencyBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript">
        function FunDeleteConfirm(obj) {
            if (obj.title == 'Delete this') {
                if (confirm('Are you sure you want to DELETE this InspectionAgency?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
  <%--  dsadsd--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div align="left">

    <br />

    <asp:Label ID="lblInspbr" runat="server" Text="Master InspectionAgency Branch" CssClass="headerAll"></asp:Label>
    <div align="right">
        <asp:Button ID="btnAddInspbr" Text="Add" CausesValidation="false" 
        runat="server" CssClass="btnCls" onclick="btnAddInspbr_Click" />
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvInspbr" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewInspbrNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="center" valign="top" style="width: 100%;">
                            <asp:Label ID="lblMessage" runat="server" CssClass="MasterAction"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewInspbrDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color: #22518F;">
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="right" valign="top" style="width: 100%;">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr align="left" valign="top" style="width: 100%">
                                    <td class="Action_TD_1" style="width: 80%; text-align: center;">
                                        <%if (inspbrAction == 1){ %>
                                        <div id="divAction" runat="server" class="Action_Div" style="background-color: #ECF8FB;
                                            height: 20px;">
                                            <div id="Action_DivInner_1">
                                                <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                            </div>
                                            <div id="Action_DivInner_2">
                                                <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png"
                                                    border="0" width="16px" height="15px" alt="CLOSE" style="cursor: pointer;" />
                                            </div>
                                        </div>
                                        <cc1:RoundedCornersExtender ID="RoundedCornersExtender2" runat="server" TargetControlID="divAction"
                                            BorderColor="#22518F" Radius="10" Corners="All"></cc1:RoundedCornersExtender>
                                        <%} %>
                                    </td>
                                    <td valign="top" align="right" style="width: 30%;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr align="left" valign="top" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%;">
                            &nbsp;
                        </td>
                    </tr>
                    
                    <tr valign="top" align="left" style="width: 100%;">
                        <td align="left" valign="top" style="width: 100%; height: 10px;">
                        </td>
                    </tr>
                </table>
                <div align="center">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        <tr align="left" valign="top" style="width: 100%;">
                            <td align="left" valign="top" style="width: 100%;">
                                <asp:GridView ID="gvInspbr" runat="server" AutoGenerateColumns="false" Width="100%"
                                    BorderWidth="0" CellPadding="5" CellSpacing="1" 
                                     onrowcommand="gvInspbr_RowCommand">
                                    <HeaderStyle CssClass="dynRowHeader" />
                                    <RowStyle CssClass="dynRowEven" />
                                    <AlternatingRowStyle CssClass="dynRowOdd" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No."><ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate></asp:TemplateField>
                                        <%--<asp:BoundField DataField="ZoneName" HeaderText="ZONE NAME" />--%>
                                        <%--<asp:BoundField DataField="BranchName" HeaderText="BRANCH NAME" />--%>
                                        <asp:BoundField DataField="AgencyName" HeaderText="INSPECTION AGENCY" />
                                        <asp:BoundField DataField="fGPtbl_TIAB_UserName" HeaderText="USER NAME" />
                                        <asp:BoundField DataField="fGPtbl_TIAB_EmailId" HeaderText="EMAIL-ID" />
                                        <asp:TemplateField HeaderText="VIEW">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbView" runat="server" CommandArgument='<%#Eval("fGPtbl_TIAB_Id") %>' CommandName="view" ToolTip="View this"
                                                ImageUrl="~/Images/Admin/view.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="DELETE">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbDelete" runat="server" CommandArgument='<%#Eval("fGPtbl_TIAB_Id") %>' CommandName="dte"
                                                OnClientClick="return FunDeleteConfirm(this)" ToolTip="Delete this" ImageUrl="~/Images/Admin/delete.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="RESET">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imbReset" runat="server" CommandArgument='<%#Eval("fGPtbl_TIAB_Id") %>' CommandName="reset"
                                                    ToolTip="Reset password" ImageUrl="~/Images/Admin/reset.png" />
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
                        <%--<tr>
                            <td align="left" style="padding-left: 10px;">
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
                                            Shows deactivated InspectionAgency Branches.
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>--%>
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


</asp:Content>

