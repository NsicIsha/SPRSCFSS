<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/AdminOnline.master" AutoEventWireup="true" CodeFile="Online_Renewal_ChangesType.aspx.cs" Inherits="appAdministrator_Online_Renewal_ChangesType" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit.HTMLEditor" tagprefix="cc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        //TO SHOW HIDE MAIL LINK
        $(document).ready(function () {
            var roleid = $("#ctl00_ContentPlaceHolder1_hfrole").val();
            if (roleid == 8) {
                $("#ctl00_ContentPlaceHolder1_lnkmail").hide();
            }
            else {
                $("#ctl00_ContentPlaceHolder1_lnkmail").show();
            }
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField runat="server" ID="hfrole" />
    <table cellpadding="0" cellspacing="0" border="0" width="100%">

        <tr style="width: 100%;">
            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">
            </td>
        </tr>

        <tr>
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table border="1" cellpadding="0" cellspacing="0" width="100%">
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" class="style4">STEP 1 of 7
                        </td>
                    </tr>
                    <tr id="missingvalue" runat="server" align="left" valign="top">
                        <td align="left" valign="top" colspan="3">
                            <asp:Label ID="lblMissingvalues" runat="server" CssClass="missingValues"></asp:Label>
                        </td>
                    </tr>
                    <tr align="left" valign="top">
                        <td align="left" valign="top" colspan="3" style="height: 8px;">&nbsp;
                        </td>
                    </tr>
                    <%-- <tr id="missingvalueSendMail" runat="server" align="right" valign="top" visible="false">
                        <td align="right" valign="top" colspan="3">
                            <asp:HyperLink ID="lnkmail" runat="server" Text="Mail Missing Info To Admin" Target="_blank" CssClass="missingEmail"></asp:HyperLink>
                        </td>
                    </tr>--%>
                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" style="height: 10px;">&nbsp;
                            <asp:HiddenField ID="HdnCatarea" runat="server" />
                            <asp:HiddenField ID="HdnCatunitsize" runat="server" />
                        </td>
                    </tr>

                    <tr align="left" style="border: thin">
                        <td align="right" valign="top" style="width: 5%;">&nbsp;</td>
                        <td align="left" style="width: 9%;" class="leftHeading"></td>
                        <td align="left" valign="top" style="width: 86%;"></td>
                    </tr>


                    <tr align="left" valign="top">
                        <td colspan="3" align="right" valign="top" runat="server" visible="true" id="trWithoutChangesrenewal">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td align="center">
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                <td class="leftHeading">Changes where only NSIC fee involved</td>
                                                <td class="leftHeading">Changes where NSIC & Inspection Agency fee involved</td>
                                            </tr>

                                            <tr>
                                                <td class="leftHeading"> 
                                                    <asp:CheckBoxList ID="chkNsicChanges" runat="server" RepeatDirection="Vertical">
                                                        <asp:ListItem Value="1" Text="Changes in Constitution Type"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Addition / Deletion Stake Holder"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Changes in Head Office Address / Name / Mobile / Email Id"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Monetry Limit"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Change in Category"></asp:ListItem>

                                                    </asp:CheckBoxList>

                                                </td>
                                                <td class="leftHeading"> 
                                                      <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Vertical">
                                                        <asp:ListItem Value="1" Text="Changes in Constitution Type"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Addition / Deletion Stake Holder"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Changes in Head Office Address / Name / Mobile / Email Id"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Monetry Limit"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Change in Category"></asp:ListItem>

                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>

                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>








</asp:Content>
