<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="OnlineDashboard.aspx.cs" Inherits="appAdministrator_OnlineDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:Label ID="lblDate" runat="server" Font-Size="Large" />
    </div>
    <%--<div style="margin-left: auto; margin-right: auto; text-align: left;">
       <asp:Button CssClass="btnClsdivSearch" Text="DownloadInExcel" ID="btnprint" runat="server" OnClick="btnprint_Click" />
    </div>--%>

    <div style="margin-left: auto; margin-right: auto; text-align: center;">
        <asp:RadioButton ID="rdFresh" runat="server" CssClass="reportCell" AutoPostBack="true" Text="Fresh" GroupName="FormCheck" OnCheckedChanged="rdFresh_CheckedChanged" />
        <asp:RadioButton ID="rdRenewal" runat="server" CssClass="reportCell" Text="Renewal" AutoPostBack="true" GroupName="FormCheck" OnCheckedChanged="rdFresh_CheckedChanged" />
    </div>

    <div style="margin-left: auto; margin-right: auto; text-align: center;">
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
    </div>
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

                                <asp:BoundField HeaderText="Appl.Date" DataField="ApplDate"  DataFormatString="{0:d}">
</asp:BoundField>
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

                <asp:BoundField HeaderText="Appl.Date (MM/DD/YYYY)" DataField="RDate"  DataFormatString="{0:d}">
</asp:BoundField>


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
</asp:Content>

