<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="UnitRenewalStep1_Online.aspx.cs" Inherits="appAdministrator_UnitRenewalStep1_Online" %>

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
                        <td style="width: 30%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnRenewalRange" runat="server" Text="Check for Renewal Range" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="1" OnClick="btnRenewalRange_Click" /></td>
                        <td style="width: 30%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnFeeDetails" runat="server" Text="Fee Details" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="3" OnClick="btnFeeDetails_Click" /></td>
                        <td style="width: 40%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnAnnexureDetails" runat="server" Text="Annexure Required" CssClass="btnCls" ValidationGroup="1" Width="250px" TabIndex="3" /></td>
                    </tr>
                </table>
            </td>
        </tr>


        <tr style="width: 95%;" runat="server" id="trCompanyValidityDetails" visible="false">
          <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">

                <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                    <tr>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            Company Current Status
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            Validity
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            Expired In(No. of Days)
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            Renewal Fee(@Discounted)
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            Renewal Fee(@Without Discount)
                        </td>
                    </tr>

                     <tr>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                          <asp:Label ID="lblCStatus" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblCValidity" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                           <asp:Label ID="lblExpiredIn" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                           <asp:Label ID="lblRenewalFeeWithDiscount" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                            <asp:Label ID="lblRenewalFeeWithoutDiscount" runat="server" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>


         <tr style="width: 95%; padding-top:30px" runat="server" id="trFeeCalculator" visible="false">
          <td align="left" valign="top" style="padding-left: 10px; padding-top: 30px; padding-right: 10px;">

                <table cellpadding="0" cellspacing="0" border="1" style="width: 100%;">
                    <tr>
                        <td style="width: 55%; padding-left: 10px; padding-right: 10px;">
                            
                                        <span style="color: black; font-weight: bold">Enter the turnover as per latest audited balance sheet: </span><span style="color: orangered"><b>(figures in lakh)</b>*</span>
                                        
                                       
                                   
                        </td>
                        <td style="width: 20%; padding-left: 10px; padding-right: 10px;">
                           <asp:TextBox ID="txtAmount" runat="server"  />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAmount" ErrorMessage="Required" runat="server" SetFocusOnError="true"/>
                                       
                        </td>
                        <td style="width:25%; padding-left: 10px; padding-right: 10px;">
                            <asp:Button ID="btnf" runat="server" Text="Calculate" OnClick="btnf_Click"  />
                             <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                        </td>
                        
                    </tr>

                     <tr>
                        <td style="padding-left: 10px; padding-right: 10px;" colspan="5">
                          <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
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


