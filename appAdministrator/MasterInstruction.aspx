<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterInstruction.aspx.cs" Inherits="appAdministrator_MasterInstruction" Title="NSIC SPR ::Instructions/ Guidelines Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div align="left">
        <asp:Label ID="lblList" runat="server" Text="Instructions/ Guidelines Master" CssClass="headerAll"></asp:Label>
    </div>
    
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvInst" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewInstNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewInstDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                                <%if(InstAction==1){ %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" Text="Label" CssClass="MasterAction"></asp:Label>
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
                                                        Corners="All"></cc1:RoundedCornersExtender>
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
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:GridView ID="gvInstDetails" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowcommand="gvInstDetails_RowCommand" 
                                        onrowdatabound="gvInstDetails_RowDataBound" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="FormName" HeaderText="NAME" />
                                            <asp:TemplateField HeaderText="VIEW CONTENT" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" CommandArgument='<%#Eval("fGPtbl_MI_Id") %>'
                                                    CommandName="view" CausesValidation="false" ToolTip="view content" />
                                                    <asp:HiddenField ID="HiddenFieldId" runat="server" Value='<%#Eval("fGPtbl_MI_Id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit" ToolTip="Edit instruction"
                                                    CommandName="edp" CausesValidation="false" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_MI_Id") %>' />
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

</asp:Content>

