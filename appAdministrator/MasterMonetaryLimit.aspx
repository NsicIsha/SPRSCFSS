<%@ Page Title="NSIC SPR :: Set Monetary Limit Slabs" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MasterMonetaryLimit.aspx.cs" Inherits="appAdministrator_MasterMonetaryLimit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <div align="left">
            <asp:Label ID="lblList" runat="server" Text="Monetary Limit Slabs Master" CssClass="headerAll"></asp:Label>
        </div>
        <div align="right" style="padding-right:10px; padding-bottom:10px;">
        <asp:Button ID="btnAddUser" Text="Add" CausesValidation="false" 
        runat="server" CssClass="btnCls" onclick="btnAddUser_Click"  />
    </div>
        <div align="left">
        <asp:Panel ID ="pnladd" Visible ="false" runat ="server" Width ="100%">
            <table border="0" cellpadding="2" cellspacing="0">
                <tr align="left" valign="top">
                    <td align="left" valign="top" colspan="2" style="height:10px;"></td>
                    <td align="left" valign="top" style="height:10px;">&nbsp;</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="center" valign="top" colspan="2" style="height:10px;">
                        <asp:Literal ID="ltlMessage"  runat="server"></asp:Literal>
                    </td>
                    <td align="right" valign="top" style="height:10px;">
                        &nbsp;</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="left" valign="top" colspan="2" style="height:10px;"></td>
                    <td align="left" valign="top" style="height:10px;">&nbsp;</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="left" valign="middle" class="leftHeading">
                        Role :
                    </td>
                    <td align="left" valign="middle">
                        <%--<asp:TextBox ID="txtBrancHead" runat="server" CssClass="txt_bx"></asp:TextBox>--%>
                  
                        <asp:DropDownList ID="ddlrole" runat="server" AutoPostBack="True" 
                            CssClass="clsDDL" DataTextField="fGPtbl_MR_Name" DataValueField="fGPtbl_MR_Id" 
                            onselectedindexchanged="ddlrole_SelectedIndexChanged" Width="350px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="regrole" ControlToValidate ="ddlrole" Display="None"
                        runat="server"  ValidationGroup ="1" ErrorMessage="Please select Role" InitialValue ="0"></asp:RequiredFieldValidator>
                        <cc1:ValidatorCalloutExtender ID="validemdate" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="regrole"
                            ></cc1:ValidatorCalloutExtender>
                  
                    </td>
                    <td align="left" valign="top">
                        &nbsp;</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="left" valign="middle" class="leftHeading">
                        Designation :
                    </td>
                    <td align="left" valign="middle">
                      <%--  <asp:TextBox ID="txtZonalHead" runat="server" CssClass="txt_bx"></asp:TextBox>--%>
                        <asp:DropDownList ID="ddldisg" runat="server" CssClass="clsDDL" 
                            DataTextField="DNAME" DataValueField="DEPTID" Width="350px">
                        </asp:DropDownList>
                          <asp:RequiredFieldValidator ID="regdesg" ControlToValidate ="ddldisg" Display="None"
                        runat="server" ValidationGroup ="1"  ErrorMessage="Please select Designation" InitialValue ="0"></asp:RequiredFieldValidator>
                    <cc1:ValidatorCalloutExtender ID="validatedesg" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="regdesg"
                            ></cc1:ValidatorCalloutExtender>
                    </td>
                    <td align="left" valign="top">
                        &nbsp;</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="left" valign="middle" class="leftHeading">
                        Monetary Limit:
                    </td>
                    <td align="left" valign="middle">
                        <asp:TextBox ID="txtHO" runat="server" CssClass="txt_bx" Width="350px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="regmlimit" ControlToValidate ="txtHO" Display="None"
                        runat="server" ValidationGroup ="1"  ErrorMessage="Please Enter Limit" ></asp:RequiredFieldValidator>
                      <cc1:ValidatorCalloutExtender ID="validatelimit" runat="server"
                                PopupPosition="BottomLeft"
                                TargetControlID="regmlimit"
                            ></cc1:ValidatorCalloutExtender>
                      
                        <cc1:FilteredTextBoxExtender TargetControlID ="txtHO" runat ="server" ValidChars = "0123456789"></cc1:FilteredTextBoxExtender>
                    </td>
                    <td align="left" valign="middle" class="leftHeading">
                        (In lakhs)</td>
                </tr>
                <tr align="left" valign="top">
                    <td align="left" valign="top" class="leftHeading">
                       
                    </td>
                    <td align="center" valign="top">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btnCls" Text="Submit"  ValidationGroup ="1"
                            onclick="btnSubmit_Click" /> &nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnback" runat="server" CssClass="btnCls" Text="Back" 
                            onclick="btnback_Click" />
                    </td>
                    <td align="center" valign="top">
                        &nbsp;</td>
                </tr>
             </table>
             </asp:Panel>
        </div>
        
          <div align="center">
          <asp:Panel runat ="server" ID = "pnl_grid" Visible = "false">
                    <table border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                        
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="left" valign="top" style="width:100%;">
                                    <asp:GridView ID="gv_mon_limit" runat="server" AutoGenerateColumns="False"
                                        Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="1" 
                                        onrowcommand="gv_mon_limit_RowCommand" >
                                     
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:BoundField DataField="SNO" HeaderText="S.No." />
                                            <asp:BoundField DataField="mrolename" HeaderText="Role" />
                                            <asp:BoundField DataField="desig" HeaderText="Designation" />
                                            <asp:BoundField DataField="mlimit" HeaderText="Monetary Limit" />
                                            <asp:TemplateField HeaderText="VIEW" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbView" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                    CommandName="view" CommandArgument='<%#Eval("fGPtbl_MML_Id")%>' ToolTip="View content" ImageAlign="Middle"/>
                                                   
                                                </ItemTemplate>

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="EDIT" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="40px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imbEdit" runat="server" ImageUrl="~/Images/Admin/edit.png" AlternateText="Edit"
                                                    CommandName="edt" CommandArgument='<%#Eval("fGPtbl_MML_Id")%>' ToolTip="Edit User" ImageAlign="Middle" />
                                                </ItemTemplate>

<HeaderStyle Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:TemplateField>
                                           </Columns>
                                       </asp:GridView>
                                    </td>
                                </tr>
                        </table>
                        
                        </asp:Panel>
                       
                </div>
        

</asp:Content>

