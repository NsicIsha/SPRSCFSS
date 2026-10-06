<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Company.master" AutoEventWireup="true" CodeFile="UnitRenewalStep1_Online.aspx.cs" Inherits="appAdministrator_UnitRenewalStep1_Online" %>

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
            <td align="left" class="headerAll" style="padding-left: 10px; padding-right: 10px;">Application to Renew Your Certification
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
                <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                    <tr>
                        <td align="center" style="width: 30%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnRenewalRange" runat="server" Text="Check for Renewal Range" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="1" OnClick="btnRenewalRange_Click" /></td>
                        <td align="center" style="width: 30%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnFeeDetails" runat="server" Text="Fee Details" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="3" OnClick="btnFeeDetails_Click" /></td>
                        <%--<td style="width: 40%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnAnnexureDetails" runat="server" Text="Annexure Required" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="3" /></td>--%>
                    </tr>
                </table>
            </td>
        </tr>


        <tr style="width: 95%;" runat="server" id="trCompanyValidityDetails" visible="false">
            <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">

                <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                    <tr>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;">Company Current Status
                        </td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">Validity
                        </td>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;">Expired In 
                        </td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">50% Discount on renewal fee if renewal done between below range
                        </td>
                        <td style="width: 30%; padding-left: 10px; padding-right: 10px;">No discount on renewal fee if renewal done from below date
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblCStatus" Font-Size="Larger" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblCValidity" Font-Size="Larger" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblExpiredIn" Font-Size="Larger" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblRenewalFeeWithDiscount" Font-Size="Larger" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 30%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblRenewalFeeWithoutDiscount" Font-Size="Larger" ForeColor="Red" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;"></td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;"></td>
                        <td style="width: 10%; padding-left: 10px; padding-right: 10px;"></td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnRenewalProcess" runat="server" Text="Initiate Renewal Process" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="1" OnClick="btnRenewalProcess_Click" />
                        </td>
                        <td style="width: 30%; padding-left: 10px; padding-right: 10px;"></td>
                    </tr>

                    <tr>
                        <td colspan="5" style="padding: 10px">
                            <span><b>Please refer below list for documents and downloads.</b></span>
                            <br />
                            <br />
                            <span>Annexure A</span>
                            <br />
                            <span>Annexure B</span>
                            <br />
                            <%-- <span><b>Annexure C</b></span>  <br />
                              <span><b>Annexure D</b></span>  <br />
                              <span><b>Annexure E</b></span>  <br />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>


        <tr style="width: 95%; padding-top: 30px" runat="server" id="trFeeCalculator" visible="false">
            <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">

                <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                    <tr>
                        <td style="width: 55%; padding-left: 10px; padding-right: 10px;">
                            <span style="color: black; font-weight: bold">Enter the turnover as per latest audited balance sheet: </span><span style="color: orangered"><b>(figures in lakh)</b>*</span>

                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-top: 25px; padding-right: 10px;">
                            <asp:TextBox ID="txtAmount" runat="server" />
                            <asp:RequiredFieldValidator ControlToValidate="txtAmount" ErrorMessage="Required" runat="server" SetFocusOnError="true" />

                        </td>
                        <td style="width: 25%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnf" runat="server" Text="Calculate" OnClick="btnf_Click" />
                            <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="padding-left: 10px; padding-right: 10px;" colspan="5">
                            <asp:Label ID="lblRenewalFee" Font-Size="Large" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>

        <tr style="width: 95%; padding-top: 30px" runat="server" id="trRenewalUpload" visible="false">
            <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">

                <table cellpadding="2" cellspacing="2" border="1" style="width: 100%;">
                    <tr>
                        <td align="center" style="width: 100%; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
                            <span style="color: green"><b>You may contact on below branch details if you have any queries.</b></span>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
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
                            <span style="color: green"><b>Please upload at least two Annexure to Initiate Renewal Process by Branch</b></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" style="width: 100%; padding-left: 10px; padding-right: 10px; padding-bottom: 10px; padding-top: 10px">
                            <table cellpadding="2" cellspacing="2" border="1" style="width: 100%;">
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b>Annexure A </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxA" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b>Annexure B </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxB" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b>Annexure C </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxC" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b>Annexure D </b></td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxD" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 50%; padding-left: 10px; padding-right: 30px;"><b>Annexure E </b>
                                    </td>
                                    <td style="width: 50%; padding-left: 10px; padding-right: 10px;">
                                        <asp:FileUpload ID="anxE" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center" style="padding: 10px">
                                        <asp:Button ID="Button1" runat="server" Text="Upload" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="1" />
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


