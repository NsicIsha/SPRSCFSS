<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ConfirmPayment.aspx.cs" Inherits="appAdministrator_ConfirmPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">
    function FunResetMonetary()
    {
        document.getElementById("ctl00_ContentPlaceHolder1_txtMonetary").value = "";
    }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div align="left">
        <asp:Label ID="lblMonetary" runat="server" Text="List of Payments to be Confirmed" CssClass="headerAll"></asp:Label>
    </div>
    
    <div align="left" style="padding-left: 10px; padding-right: 10px;">
        <asp:MultiView ID="mvMonetary" runat="server" ActiveViewIndex="0">
            <asp:View ID="ViewMonetaryNotFound" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="center" valign="top" style="width:100%;">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                </td>
                            </tr>
                   </table>       
                  </asp:View>
                <asp:View ID="ViewMonetaryDetail" runat="server">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" style="border-color:#22518F;">
                            <tr align="left" valign="top" style="width:100%;">
                                <td align="right" valign="top" style="width:100%;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr align="left" valign="top" style="width:100%">
                                            <td class="Action_TD_1" style="text-align:center;">
                                               <%if(monetaryAction==1){ %>
                                                
                                                    <div id="divAction" runat="server" class="Action_Div" style="background-color:#ECF8FB; height:20px;">
                                                        <div id="Action_DivInner_1" align="center">
                                                            <asp:Label ID="LabelAction" runat="server" CssClass="MasterAction"></asp:Label>
                                                        </div>
                                                        <div id="Action_DivInner_2" align="center">
                                                            <img id="imgAction" onclick="FunZoneActionClose()" src="../Images/Admin/delete.png" border="0" width="16px" height="15px" alt="CLOSE" style="cursor:pointer;" />
                                                        </div>
                                                    </div>
                                                   
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
                                    <asp:HiddenField ID="HiddenFieldCompanyId" runat="server" />
                                    <asp:HiddenField ID="HFieldformtypeid" runat="server" />
                                    <asp:HiddenField ID="hdnfactcnt" runat="server" />
                                    <asp:GridView ID="gvUnconfirmedCompanies" runat="server" AutoGenerateColumns="false"
                                    Width="100%" BorderWidth="0px" CellPadding="0" CellSpacing="1" 
                                        onrowdatabound="gvUnconfirmedCompanies_RowDataBound" onrowcommand="gvUnconfirmedCompanies_RowCommand">
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No.">
                                            <ItemTemplate >
                                            <asp:Label ID="lblsn" runat="server"  ></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name" />
                                            <asp:BoundField DataField="fGPtbl_Company_HeadOffice_Address" HeaderText="Head Office" />
                                           <%-- <asp:BoundField DataField="fGPtbl_Company_PAN" HeaderText="PAN No."/>--%>
                                            <%--<asp:BoundField DataField="fGPtbl_MF_Name" HeaderText="Type" />--%>
                                            <asp:TemplateField HeaderText="View Transaction Details" ItemStyle-HorizontalAlign="Center"><ItemTemplate><asp:ImageButton ID="imbButton" runat="server" ImageUrl="~/Images/Admin/edit.png" CausesValidation="false"
                                                    AlternateText="SET" CommandName="trans" ToolTip="View transaction details" ImageAlign="Middle" CommandArgument='<%#Eval("fGPtbl_PD_Id") %>' /></ItemTemplate><HeaderStyle Width="120px" /><ItemStyle HorizontalAlign="Center" /></asp:TemplateField>
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

