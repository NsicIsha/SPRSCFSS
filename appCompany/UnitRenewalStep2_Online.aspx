<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Company.master" AutoEventWireup="true" CodeFile="UnitRenewalStep2_Online.aspx.cs" Inherits="appCompany_UnitRenewalStep2_Online" %>

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
            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Upload Annexure to Initial Renewal Process
            </td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-right: 10px;">
                <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                    <tr style="width: 100%;">
                        <td style="width: 100%;"></td>
                    </tr>


                    <tr id="step2tr" runat="server" style="width: 100%;">
                        <td id="step2" style="width: 100%;">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%;">
                                <tr>
                                    <td id="progressbar" align="left"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="width: 95%;">
            <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">
                <table cellpadding="5" cellspacing="5" border="1" style="width: 100%;">
                    <tr>
                        <td align="center" style="width: 100%; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
                            <span style="color: green"><b>You may contact on below branch details if you have any queries.</b></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; " >
                            <asp:GridView ID="grdFactories" AutoGenerateColumns="false" runat="server" EmptyDataText="No Records Available">
                                <Columns>
                                    <asp:BoundField HeaderText="Name" DataField="fGPtbl_MB_Name" />
                                    <asp:BoundField HeaderText="Address" DataField="fGtbl_MB_Address" />
                                    <asp:BoundField HeaderText="Phone" DataField="fGtbl_MB_Phone" />
                                    <asp:BoundField HeaderText="Email" DataField="fGtbl_MB_Email" />

                                </Columns>
                            </asp:GridView>
                        </td>

                    </tr>

                    <tr>
                        <td align="center" style="width: 100%; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
                            <span style="color: green"><b></b></span>
                        </td>
                    </tr>


                    <tr>
                        <td align="center" style="width: 100%; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
                            <table cellpadding="5" cellspacing="5" border="1" style="width: 100%;">
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b> Annexure A </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxA" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                     <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b> Annexure B </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxB" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                     <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b> Annexure C </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxC" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                     <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b> Annexure D </b>                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxD" runat="server" />
                                    </td>
                                </tr>
                                 <tr>
                                     <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b> Annexure E </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxE" runat="server" />
                                    </td>
                                </tr>

                                <tr>
                                    <td colspan="2" align="center" style="padding:10px">
                                         <asp:Button ID="btnRenewalRange" runat="server" Text="Upload" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="1"  />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                </table>
            </td>
        </tr>









        <%--<tr>
            <td style="width: 100%; text-align: left;" class="MandatoryMessage">
                <span class="MandatoryField">*</span> Mandatory Fields
            </td>
        </tr>--%>
    </table>

</asp:Content>
