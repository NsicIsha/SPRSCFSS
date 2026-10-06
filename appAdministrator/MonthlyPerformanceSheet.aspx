<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MonthlyPerformanceSheet.aspx.cs" Inherits="appAdministrator_MonthlyPerformanceSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .clsDDLhidden {
            width: 130px;
            background-color: #dddddd;
            border: #999999 solid 1px;
            padding: 3px;
            display: none;
        }

        .btnclsfincial {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 13px;
            background-color: White;
            color: Black;
            font-weight: bold;
            width: 160px;
        }
    </style>
    <%--sds--%>

    <script type="text/javascript" language="javascript">

        function validdatefrom() {
            var txt = "Please Fill Required Date";
            var opt = 0;
            var Datefrom = document.getElementById("<%=txt_fromdate.ClientID %>").value;
           var dateto = document.getElementById("<%=txt_todate.ClientID %>").value;
           var fromdateval = new Date(Datefrom);
           if (Datefrom == "") {
               txt += "\n - From date.";
               opt = 1;
           }

           if (fromdateval <= new Date('03/31/2015')) {
               txt += "\n- Incorrect date. Any date before 31 March, 2015 is not valid.";
               opt = 1;


           }
           if (dateto == "") {
               txt += "\n -  To Date.";
               opt = 1;
           }


           if (opt == "1") {
               alert(txt);
               document.getElementById("<%=txt_fromdate.ClientID %>").value = "";
               document.getElementById("<%=txt_fromdate.ClientID %>").focus();
                return false;
            }

        }






    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="Update_bton" runat="server">
                    <ContentTemplate>


    <div align="left">
        <asp:Label ID="lblDashboard" runat="server" Text=" Report" CssClass="headerAll"></asp:Label>
    </div>
    <table cellpadding="2" cellspacing="2" border="0" style="padding-left: 10px; width: 100%; margin-top: 20px;">
        <tr>
            <td colspan="4">
                <asp:HiddenField ID="Btn_value" runat="server" Value="" />
            </td>


        </tr>

        <tr>
            <td align="left" valign="top" colspan="3">Fromdate
                <asp:TextBox ID="txt_fromdate" runat="server" placeholder="MM/dd/yyyy"></asp:TextBox>
                  
                                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_fromdate"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender   ID="CalendarExtender2" TargetControlID="txt_fromdate" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />


                <%--<asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_fromdate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="txt_fromdate" ControlExtender="MaskedEditExtender1"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>--%>
                Todate
                <asp:TextBox ID="txt_todate" runat="server" placeholder="MM/dd/yyyy"></asp:TextBox>

                  <asp:RequiredFieldValidator ValidationGroup="withdate" ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
                                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />



               <%-- <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_todate" MaskType="Date"
                    Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                    ClearTextOnInvalid="true">
                </asp:MaskedEditExtender>
                <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_todate" ControlExtender="MaskedEditExtender3"
                    InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                    TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>--%>




            </td>
            <td align="left" valign="top">
            <%--    <asp:UpdatePanel ID="Update_bton" runat="server">
                    <ContentTemplate>--%>
                        <asp:Button ID="btn_search" runat="server" Text="Fetch Data"
                            OnClientClick="javascript:return validdatefrom();" OnClick="btn_search_Click" ValidationGroup="withdate" CssClass="btnCls" />

              <%--      </ContentTemplate>
                </asp:UpdatePanel>--%>



            </td>

        </tr>

        <tr>
            <td colspan="4" align="right">

                <asp:UpdateProgress ID="updProgress"
                    AssociatedUpdatePanelID="Update_bton"
                    runat="server">
                    <ProgressTemplate>
                        <img alt="progress" src="../Images/Admin/loading.gif" />
                        Processing...            
                    </ProgressTemplate>
                </asp:UpdateProgress>
               <%-- <asp:UpdateProgress ID="UpdateProgress1"
                    AssociatedUpdatePanelID="Update_bton"
                    runat="server">
                    <ProgressTemplate>
                        <img alt="progress" src="../Images/Admin/loading.gif" />
                        Processing...            
                    </ProgressTemplate>
                </asp:UpdateProgress>--%>



            </td>
        </tr>

        <tr>
            <td colspan="4" align="left">

                <asp:GridView ID="grd_report" runat="server" AutoGenerateColumns="false" Width="100%"
                    EnableViewState="False" AllowPrintPaging="true" UseAccessibleHeader="true"
                    BorderWidth="1" CellPadding="5" CellSpacing="1"  
                    PageSize="4"   CssClass="table">
                    <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

                    <RowStyle CssClass="dynRowEven" />
                    <AlternatingRowStyle CssClass="dynRowOdd" />
                    <HeaderStyle CssClass="dynRowHeader" />



                    <Columns>
                        <asp:BoundField DataField="FinancialYear" HeaderText="Financial Year" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                        <asp:BoundField DataField="No.ofUnits" HeaderText="No. of Units" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                        <asp:BoundField DataField="Value of Order" HeaderText="Value of Order" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                        <asp:BoundField DataField="Turnover" HeaderText="Turnover" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                        
                    </Columns>

                </asp:GridView>


            </td>

        </tr>




    </table>

 </ContentTemplate>
                </asp:UpdatePanel>
</asp:Content>

