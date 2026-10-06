<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Unit_Branchshift.aspx.cs"
 Inherits="appAdministrator_Unit_Branchshift" Title="NSIC::SPR Unit Branch shift" %>
 
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
         <div style="position:relative; text-align:left;">
            <asp:Label ID="lblBranch" runat="server" Text="Branch Shift" CssClass="headerAll"></asp:Label>
            <br />
        
        </div>
        <div>
                <asp:Label ID="lblmess" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
        </div>
        
        <table border = "0" cellpadding = "10px" cellspacing = "0" width = "600px" style="clear:both;">
        <tr align="left" valign="top">
           <td align ="left" valign="top" style="width:85px;">Enter company name to search:</td>
           <td valign="top" align="left" style="width:100px;">
            <asp:TextBox ID="txtSearchbox" runat="server" CssClass="clstxtbx"></asp:TextBox>
            </td>
            <td align ="left" style="width:40px;">
               <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btnClsdivSearch" onclick="btnSearch_Click" />
            </td>
        </tr>
        </table>
        
        
        <div align="center">
           
            <asp:GridView ID="grdCompanylist" runat="server" AutoGenerateColumns="false"
                CellPadding="3" CellSpacing="0" onrowcommand="grdCompanylist_RowCommand">
                <HeaderStyle CssClass="dynRowHeader" />
                <RowStyle CssClass="dynRowEven" />
                <AlternatingRowStyle CssClass="dynRowOdd" />
                
                <EmptyDataTemplate>
                    No record found
                </EmptyDataTemplate>
                
                    <Columns>
                        <asp:TemplateField HeaderText="S.No.">
                    <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField DataField="fGPtbl_Company_GPReferenceNo" HeaderText="SPR No." />
                        <%--<asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />--%>
                          <asp:TemplateField HeaderText="Company Name">
                             <ItemTemplate>
                             <asp:Label ID="lbl_company_name" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                             <br />
                             <asp:Label ID="lbl_hs_code" runat="server" Text='<%#Eval("HSCode") %>'></asp:Label>
                             </ItemTemplate>
                          </asp:TemplateField>
                        <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>
                        <asp:TemplateField >
                            <ItemTemplate>
                                <asp:Button ID="btnchange" CssClass="btnCls" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' CommandName="changbr" runat="server" Text="Change" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
        </div>
        
        <asp:Panel ID="pnlOut" runat="server" Style="display: block;" CssClass="outerPopup">
        <asp:Panel ID="pnlInner" runat="server" Width="400px" CssClass="innerPopup">
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
                                                    <asp:Label ID="lblAddEdit" runat="server" CssClass="header"></asp:Label>&nbsp;&nbsp;<asp:Label
                                                        ID="lblBranchName" runat="server" CssClass="header"></asp:Label>
                                                    <asp:HiddenField ID="hdnfieldcomid" runat="server" />
                                                </td>
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
                                <tr valign="top" align="center">
                                    <td align="left" valign="top" style="width:100px;" class="updateInnerColumn">
                                        Branch :
                                    </td>
                                    <td valign="top" align="left" class="updateInnerColumn">
                                        
                                        <asp:DropDownList ID="DropDownListBranch" runat="server" CssClass="clsDDL" AutoPostBack="False">
                                        </asp:DropDownList>
                                        
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
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" TabIndex="8" ValidationGroup="1"
                                            CssClass="btnCls"  OnClick="btnSubmit_Click"  />
                                        &nbsp;&nbsp;
                                        
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

