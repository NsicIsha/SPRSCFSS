<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterMailer.aspx.cs" Inherits="appAdministrator_MasterMailer" Title="NSIC SPR ::Auto Mailers Content Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript">
        function FunActiveConfirm(obj) {
            if (obj.title == 'Activate This') {
                if (confirm('Are you sure you want to ACTIVATE this Mailer?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
            
                if (confirm('Are you sure you want to DEACTIVATE this Mailer?!') == true) {
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
    <asp:Label ID="lblMailer" runat="server" Text="Auto Mailers Content Master" CssClass="headerAll"></asp:Label>
</div>


<div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvMailer" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewMailerNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewMailerDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                                <%if(mailerAction==1){ %>
                                                
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
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:GridView ID="gvMailerDetails" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowdatabound="gvMailerDetails_RowDataBound" 
                                        onrowcommand="gvMailerDetails_RowCommand" >
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fGPtbl_MM_MailerName" HeaderText="MAILER NAME" />
                                            <asp:BoundField DataField="fGPtbl_MM_Active" HeaderText="Active/ Deactive" HeaderStyle-Width="60px" />
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    ToolTip="Edit Mailer" CausesValidation="false" CommandName="edp" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_MM_Id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Activate/ Deactivate" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                <ItemTemplate>
                                                        <asp:ImageButton ID="imbAct" runat="server" CommandName="Act" CommandArgument='<%#Eval("fGPtbl_MM_Id")%>' ImageAlign="Middle"
                                                        OnClientClick="return FunActiveConfirm(this);" CausesValidation="false" />
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
                            Shows deactivated Mailer.
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

