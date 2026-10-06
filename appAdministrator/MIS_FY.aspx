<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="MIS_FY.aspx.cs" Inherits="appAdministrator_MIS_FY" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="left">
        <asp:Label ID="lblBranch" runat="server" Text="Financial Year Report" CssClass="headerAll"></asp:Label>
    </div>
    <div>
        <table border="1" cellpadding="10" cellspacing="0" width="100%"  style="border-color: #22518F; padding-top:20px">
            <tr align="left" valign="top" >
                <td align="center" valign="top" style="width: 50%;">
                    <div align="center" style="vertical-align: top;">
                        <label for="YearList">Choose a financial year:</label>


                        <asp:DropDownList ID="YearList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="YearList_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="2024_25">2024-25</asp:ListItem>
                            <asp:ListItem Value="2025_26">2025-26</asp:ListItem>
                        </asp:DropDownList>

                    </div>
                </td>

                <td align="center" valign="top" style="width: 50%;">
                    <div align="center" style="vertical-align: top;">

                        <label for="MonthList" style="margin-left: 4rem">Choose a financial month:</label>
                        <asp:DropDownList ID="MonthList" runat="server" AutoPostBack="True" Enabled="false" OnSelectedIndexChanged="MonthList_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="April">April</asp:ListItem>
                            <asp:ListItem Value="May">May</asp:ListItem>
                            <asp:ListItem Value="June">June</asp:ListItem>
                            <asp:ListItem Value="July">July</asp:ListItem>
                            <asp:ListItem Value="August">August</asp:ListItem>
                            <asp:ListItem Value="September">September</asp:ListItem>
                            <asp:ListItem Value="October">October</asp:ListItem>
                            <asp:ListItem Value="November">November</asp:ListItem>
                            <asp:ListItem Value="December">December</asp:ListItem>
                            <asp:ListItem Value="January">January</asp:ListItem>
                            <asp:ListItem Value="February">February</asp:ListItem>
                            <asp:ListItem Value="March">March</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>

            </tr>

            <tr>
                <td colspan="2">

                     <table border="0" cellpadding="0" cellspacing="0"  width="100%" class="clsOutlineDyntblBorder">
            <tr align="left" valign="top" style="width: 100%;">
                <td align="center" valign="top" style="width: 100%;">
                    <asp:GridView Width="100%" ID="grdView1"  runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="true" EmptyDataText="No Record present" OnRowDataBound="grdView1_RowDataBound">
                   <HeaderStyle CssClass="dynRowHeader" />
                        <RowStyle CssClass="dynRowEven" Font-Size="14px" />
                        <AlternatingRowStyle CssClass="dynRowOdd" />
                        
                           
                        </asp:GridView>

                </td>
            </tr>
        </table>
                </td>
            </tr>
        </table>

       
    </div>


</asp:Content>

