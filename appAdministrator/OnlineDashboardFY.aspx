<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="OnlineDashboardFY.aspx.cs" Inherits="appAdministrator_OnlineDashboardFY" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:Label ID="lblDate" runat="server" Font-Size="Large" />
    </div>
    <%--<div style="margin-left: auto; margin-right: auto; text-align: left;">
       <asp:Button CssClass="btnClsdivSearch" Text="DownloadInExcel" ID="btnprint" runat="server" OnClick="btnprint_Click" />
    </div>--%>

    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <table cellpadding="2" cellspacing="0" border="1" width="100%">
            <tr>
                <td colspan="6">
                    <asp:RadioButton ID="rdFresh" Checked="true" runat="server" CssClass="reportCell"  Text="Fresh" GroupName="FormCheck" AutoPostBack="true" OnCheckedChanged="rdFresh_CheckedChanged" />
                    <asp:RadioButton ID="rdRenewal" runat="server" CssClass="reportCell" Text="Renewal"  GroupName="FormCheck" AutoPostBack="true" OnCheckedChanged="rdFresh_CheckedChanged" />
                </td>
            </tr>

            <tr>
                <td align="left">From</td>
                <td align="left">
                    <asp:TextBox ID="txt_from_date" Enabled="false" runat="server" placeholder="MM/dd/yyyy"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_from_date"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_from_date" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
                </td>
                <td align="left">To</td>
                <td align="left">
                    <asp:TextBox ID="txt_todate" runat="server" Enabled="false" placeholder="MM/dd/yyyy"></asp:TextBox>
                    <asp:RequiredFieldValidator ValidationGroup="withdate" ID="rfd1" runat="server" ErrorMessage="Required" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />
                </td>

                <td align="left">
                    <span><b>Status:</b></span><%--AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"--%><asp:DropDownList ID="ddlStatus" Visible="true" runat="server" >
                        <asp:ListItem Text="--Select--" Value="1000" />
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="Confirm(Registered)" Value="1" />
                        <asp:ListItem Text="Incomplete" Value="15" />
                        <asp:ListItem Text="InProcess(InspectionPending)" Value="2" />
                        <asp:ListItem Text="Inspection Passed" Value="7" />
                        <asp:ListItem Text="Reject" Value="6" />
                        <asp:ListItem Text="Waiting(Branch to verify documents)" Value="3" />
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator InitialValue="1000" ValidationGroup="withdate" ID="rfdDDlfresh" ControlToValidate="ddlStatus" ErrorMessage="Required" SetFocusOnError="true" runat="server"></asp:RequiredFieldValidator>
                    <asp:DropDownList ID="ddlStatusRenewal" Visible="false" runat="server" ><%--AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"--%>
                        <asp:ListItem Text="--Select--" Value="1000" />
                        <asp:ListItem Text="All" Value="0" />
                        <asp:ListItem Text="Applied by MSE" Value="1" />
                        <asp:ListItem Text="Examined by Branch" Value="2" />
                        <asp:ListItem Text="Reject" Value="3" />

                        <asp:ListItem Text="Renewal Completed" Value="7" />

                    </asp:DropDownList>
                     <asp:RequiredFieldValidator InitialValue="1000" ValidationGroup="withdate" ID="rfdddlRenewalVal" ControlToValidate="ddlStatusRenewal" ErrorMessage="Required R" SetFocusOnError="true" Display="Dynamic" runat="server"></asp:RequiredFieldValidator>
                </td>


            </tr>

            <tr>

                <td align="center" colspan="6">
                    <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                        <ProgressTemplate>
                            <img alt="progress" src="../Images/Admin/loading.gif" />
                            Processing...            
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:Button ID="btn_status_track" runat="server" ValidationGroup="withdate"
                        Text="View Details" Width="120" CssClass="btnCls" 
                        OnClick="btn_status_track_Click" />
                    <asp:Label ID="lbl_totalcount_buying" runat="server" Text=""></asp:Label>
                </td>

            </tr>
        </table>
    </div>




    <%-- <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:RadioButton ID="rdFresh" runat="server" CssClass="reportCell" AutoPostBack="true" Text="Fresh" GroupName="FormCheck" OnCheckedChanged="rdFresh_CheckedChanged" />
        <asp:RadioButton ID="rdRenewal" runat="server" CssClass="reportCell" Text="Renewal" AutoPostBack="true" GroupName="FormCheck" OnCheckedChanged="rdFresh_CheckedChanged" />
    </div>--%>

    <%--<div style="margin-left: auto; margin-right: auto; text-align: center;">
        <span><b>Status:</b></span>
        <asp:DropDownList ID="ddlStatus" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="--Select--" Value="Null" />
            <asp:ListItem Text="All" Value="0" />
            <asp:ListItem Text="Confirm(Registered)" Value="1" />
            <asp:ListItem Text="Incomplete" Value="15" />
            <asp:ListItem Text="InProcess(InspectionPending)" Value="2" />
            <asp:ListItem Text="Inspection Passed" Value="7" />
            <asp:ListItem Text="Reject" Value="6" />
            <asp:ListItem Text="Waiting(Branch to verify documents)" Value="3" />
        </asp:DropDownList>
        <asp:DropDownList ID="ddlStatusRenewal" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
            <asp:ListItem Text="--Select--" Value="Null" />
            <asp:ListItem Text="All" Value="0" />
            <asp:ListItem Text="Applied by MSE" Value="1" />
            <asp:ListItem Text="Examined by Branch" Value="2" />
            <asp:ListItem Text="Reject" Value="3" />

            <asp:ListItem Text="Renewal Completed" Value="7" />

        </asp:DropDownList>
    </div>--%>
    <div style="margin-left: auto; margin-right: auto; text-align: right;">
        <asp:Label runat="server" ID="lblCount" Font-Size="Medium" />
    </div>
    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:GridView ID="grdOnline" runat="server"
            AutoGenerateColumns="false" Visible="false"
            Width="100%" BorderWidth="1px" CellPadding="5" CellSpacing="1">
            <Columns>
                <asp:TemplateField HeaderText="SNO">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Sno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COMPANY NAME">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Company_Name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PAN">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("PAN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UAM/UDYAM">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("UAM")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Appl.Date" DataField="ApplDate" DataFormatString="{0:d}"></asp:BoundField>
                <%--<asp:TemplateField HeaderText="Appl.Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_ADate" runat="server" Text='<%# Eval("ApplDate", "{0:dd MMM yyyy}")%></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="BRANCH NAME">
                    <ItemTemplate>
                        <asp:Label ID="lbl_application_date" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField HeaderText="AGENCY NAME">
                    <ItemTemplate>
                        <asp:Label ID="lbl_application_date" runat="server" Text='<%#Eval("Agency")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="INSPECTION ALLOTMENT DATE" DataField="InspectionAllotmentDate"  DataFormatString="{0:d}">
</asp:BoundField>
                 <asp:TemplateField HeaderText="AGEING">
                    <ItemTemplate>
                        <asp:Label ID="lbl_application_date" runat="server" Text='<%#Eval("AgeFromIA")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:Label ID="lbl_status_date" runat="server" Text='<%#Eval("Status ")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>

        <asp:GridView ID="grdRenewal" runat="server"
            AutoGenerateColumns="false" Visible="false"
            Width="100%" BorderWidth="1px" CellPadding="5" CellSpacing="1">
            <Columns>
                <asp:TemplateField HeaderText="SNO">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Sno" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COMPANY NAME">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Company_Name" runat="server" Text='<%#Eval("CompanyName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PAN">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Branch_Name" runat="server" Text='<%#Eval("PAN")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="Appl.Date (MM/DD/YYYY)" DataField="RDate" DataFormatString="{0:d}"></asp:BoundField>


                <%--<asp:TemplateField HeaderText="Appl.Date">
                    <ItemTemplate>
                        <asp:Label ID="lbl_RDate" runat="server" Text='<%#Eval("RDate", "{0:dd MMM yyyy}")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="BRANCH NAME">
                    <ItemTemplate>
                        <asp:Label ID="lbl_application_date" runat="server" Text='<%#Eval("BranchName")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="STATUS">
                    <ItemTemplate>
                        <asp:Label ID="lbl_status_date" runat="server" Text='<%#Eval("Status ")%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


            </Columns>
        </asp:GridView>
    </div>


            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

