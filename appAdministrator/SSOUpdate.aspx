<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="SSOUpdate.aspx.cs" Inherits="appAdministrator_SSOUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlOuterEdit" runat="server" Style="display: block;" CssClass="outerPopup">
        <asp:Panel ID="pnlInnerEdit" runat="server" CssClass="innerPopup" Width="100%">
            <table cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                <tr align="center" valign="top" style="width: 100%;">
                    <td align="center" valign="top" style="width: 100%; padding-left: 5px; padding-right: 5px; padding-top: 0px; padding-bottom: 0px;">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%" style="vertical-align: top;">
                            <tr align="left" valign="top">
                                <td colspan="2" align="left" valign="top" style="width: 100%; padding-bottom: 10px;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top">
                                            <td align="center" valign="top" class="updateInnerHeader">
                                                <asp:Label ID="lblMesgPnl" runat="server" CssClass="header"></asp:Label>
                                            </td>
                                            <td align="right" valign="top" style="width: 20px;">
                                                <asp:ImageButton AlternateText="CLOSE" ToolTip="CLOSE" ID="imgCloseEdit" runat="server"
                                                    ImageUrl="~/Images/Admin/closeblue.png" Width="16px" Height="16px" BorderWidth="0" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD" style="width: 100px;">Branch:
                                </td>
                                <td align="left" class="modalTDtextbox">

                                    <asp:DropDownList ID="ddlbranchEdit" runat="server" CssClass="clsDDL" AutoPostBack="false" ValidationGroup="12"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
                                        Display="Dynamic" ValidationGroup="12" ErrorMessage="Please select Branch" ControlToValidate="ddlbranchEdit"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD" style="width: 100px;">User Type:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:DropDownList ID="ddlUserType" runat="server" CssClass="clsDDL">
                                        <asp:ListItem Value="0" Text="--Select User Type--"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="Branch User"></asp:ListItem>
                                        <asp:ListItem Value="4" Text=" Accounts "></asp:ListItem>
                                        <asp:ListItem Value="3" Text="Head"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Zonal Head"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" SetFocusOnError="true"
                                        Display="None" ValidationGroup="12" ErrorMessage="Please provide City" InitialValue="0" ControlToValidate="ddlUserType"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" TargetControlID="RequiredFieldValidator4">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" class="modalTDtextBOLD">Employe Code:
                                </td>
                                <td align="left" class="modalTDtextbox">
                                    <asp:TextBox ID="txtEmpCode" runat="server" MaxLength="4" CssClass="txt_bx" TextMode="SingleLine"></asp:TextBox>

                                    <asp:RegularExpressionValidator ID="revLength" runat="server" ValidationGroup="12"
                                        ControlToValidate="txtEmpCode"
                                        ValidationExpression="^.{4,4}$"
                                        ErrorMessage="Enter valid PF code of 4 digit"
                                        Display="Dynamic"
                                        ForeColor="Red" />

                                    <asp:RegularExpressionValidator ID="revInteger" runat="server" ValidationGroup="12"
                                        ControlToValidate="txtEmpCode"
                                        ValidationExpression="^\d+$"
                                        ErrorMessage="Please enter a valid integer"
                                        ForeColor="Red"
                                        Display="Dynamic" />

                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" SetFocusOnError="true"
                                        Display="Dynamic" ValidationGroup="12" ErrorMessage="Please provide PF Code" ControlToValidate="txtEmpCode"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" TargetControlID="RequiredFieldValidator4"></asp:ValidatorCalloutExtender>


                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender42" runat="server" TargetControlID="revInteger"></asp:ValidatorCalloutExtender>

                                    <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender692" runat="server" TargetControlID="revLength"></asp:ValidatorCalloutExtender>


                                </td>
                            </tr>
                            <tr align="left" valign="top">
                                <td align="left" style="font-weight: bold;"></td>
                                <td align="left" class="modalTDbutton">
                                    <asp:Button ID="btnSubmitEdit" runat="server" Text="Submit" CssClass="btnCls" ValidationGroup="12"
                                        OnClick="btnSubmitEdit_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                                   
                                </td>
                            </tr>
                            <tr align="left" valign="top" style="padding-top: 50px">
                                <td colspan="2" style="padding-top: 50px"> 

                                      <table style="padding-top: 10px" border="1" cellpadding="0" cellspacing="0" width="100%" class="clsOutlineDyntblBorder">
                                    <tr align="left" valign="top" style="width: 100%;">
                                        <td align="left" valign="top" style="width: 100%;">
                                            <asp:GridView ID="GridView1" runat="server" Width="100%" BorderWidth="0" CellPadding="3"
                                                CellSpacing="1" BackColor="#FFFFFF" AutoGenerateColumns="false" DataKeyNames="fGPtbl_MU_Id"
                                                AllowSorting="false" AllowPaging="true" PageSize="25" >
                                                <HeaderStyle CssClass="GrdviewHeader" />
                                                <RowStyle CssClass="GrdviewRowOdd" />
                                                <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No.">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="fGPtbl_MU_Name"
                                                        HeaderText="User Name"   />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="fGPtbl_MB_Name"
                                                        HeaderText="Branch"  />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="EMPID"
                                                        HeaderText="PF Code/EMPID"   />
                                                    <asp:BoundField HtmlEncode="true" HtmlEncodeFormatString="true" ReadOnly="true" DataField="UserType"
                                                        HeaderText="User Type"  />
                                                   
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>


                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </asp:Panel>

              

            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

