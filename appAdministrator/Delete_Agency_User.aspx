<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="Delete_Agency_User.aspx.cs" Inherits="appAdministrator_Delete_Agency_User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div align="left" style="height:20px;"></div>
    <table border="0" cellpadding="5" cellspacing="0" width="400px">
        <tr valign="top" align="left" >
                <td align="left" valign="top" style="width:160px;" class="updateInnerColumn">
                    Zone :
                </td>
                <td align="left" valign="top" style="width:225px;" class="updateInnerColumn">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                    <asp:DropDownList ID="DropDownListZone" runat="server" CssClass="clsDDL" 
                        TabIndex="1"  Width="200px" AutoPostBack="True" 
                        onselectedindexchanged="DropDownListZone_SelectedIndexChanged">
                    </asp:DropDownList>
                               </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
            </tr>
            <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    Branch :
                </td>
                <td align="left" valign="top" class="updateInnerColumn">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                    <asp:DropDownList ID="DropDownListBranch" runat="server" TabIndex="2" CssClass="clsDDL" AutoPostBack="false" Width="200px"></asp:DropDownList>
                   </ContentTemplate>
                            </asp:UpdatePanel>
                </td>
             </tr>
             
              
             <tr valign="top" align="left">
                <td align="left" valign="top" class="updateInnerColumn">
                    InspectionAgency :
                </td>
                <td valign="top" align="left" class="updateInnerColumn">
                    <asp:DropDownList ID="DropDownListInsp" runat="server" TabIndex="1" CssClass="clsDDL" AutoPostBack="false" Width="200px"></asp:DropDownList>
                </td>
             </tr>
             
             
             <tr valign="top" align="center" >
                <td valign="top" align="left">
                </td>
                <td valign="top" align="left" class="updateInnerColumn">
                    <asp:Button ID="btnSubmit" runat="server" Text="Search" TabIndex="8" 
                        ValidationGroup="1" CssClass="btnCls" onclick="btnSubmit_Click" />
                        &nbsp;&nbsp;
                        <%--<input id="btnReset" type="button" value="Reset" tabindex="4" class="btnCls" onclick="return FunResetInspbr();" />--%>
                </td>
            </tr>
    </table>
</asp:Content>

