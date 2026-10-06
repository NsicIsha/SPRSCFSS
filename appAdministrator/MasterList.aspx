<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterList.aspx.cs" Inherits="appAdministrator_MasterList" Title="NSIC SPR ::List Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script language="javascript" type="text/javascript">
        function FunActiveConfirm(obj) {
            if (obj.title == 'Activate This') {
                if (confirm('Are you sure you want to ACTIVATE this List?') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
            
                if (confirm('Are you sure you want to DEACTIVATE this List?!') == true) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
   <%-- DSAD--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <div align="left">
    <asp:Label ID="lblList" runat="server" Text="Useful Information Lists Master" CssClass="headerAll"></asp:Label>
    </div>

    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvList" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewListNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewListDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                                <%if(listAction==1){ %>
                                                
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
                                    <asp:GridView ID="gvListDetails" runat="server" AutoGenerateColumns="false" 
                                        Width="100%" BorderWidth="0" CellPadding="0" CellSpacing="1" 
                                        onrowdatabound="gvListDetails_RowDataBound" 
                                        onrowcommand="gvListDetails_RowCommand">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" ></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fGPtbl_ML_Name" HeaderText="NAME" />
                                            <asp:TemplateField HeaderText="VIEW CONTENT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" CommandArgument="fGPtbl_ML_Id" CommandName="view" ToolTip="view content" />
                                                    <asp:HiddenField ID="HiddenFieldId" runat="server" Value='<%#Eval("fGPtbl_ML_Id")%>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="fGPtbl_ML_AccessId" HeaderText="Active/ Deactive" HeaderStyle-Width="60px" />--%>
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgButton" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"  AlternateText="Edit" ToolTip="Edit list" CommandName="edp" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_ML_Id") %>' />
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

