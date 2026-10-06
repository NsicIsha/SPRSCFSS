<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="ProRataRV.aspx.cs" Inherits="appAdministrator_ProRataRV" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%--<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
</head>
<body>
    <form runat="server">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>

    <div align="left" class="clsrowdiv">
        <div class="leftext">Search</div>
        <div class="coloncls">
            Fromdate
        <asp:TextBox ID="txt_fromdate" runat="server" Enabled="false" Text=""></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_fromdate"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txt_fromdate" Format="MM/dd/yyyy" PopupButtonID="imgbtnCalendar" runat="server" />
        </div>
        <div class="txtdiv">
            Todate
        <asp:TextBox ID="txt_todate" runat="server" Text="" Enabled="false"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfd1" runat="server" ErrorMessage="*" ForeColor="Red" Display="Dynamic" ControlToValidate="txt_todate"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/calendar.png" />
            <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txt_todate" Format="MM/dd/yyyy" PopupButtonID="ImageButton1" runat="server" />

        </div>
        <div class="btndiv">
               <asp:UpdatePanel ID="Update_bton"  runat="server" >
                        <ContentTemplate>
            <asp:Button ID="btnSearch" runat="server" CssClass="btn-warning" Text="--Search--" OnClick="btnSearch_Click" />
           </ContentTemplate>
                  </asp:UpdatePanel>   
                           
        </div>
    </div>
    <br />
     <asp:UpdateProgress ID="updProgress"
        AssociatedUpdatePanelID="Update_bton"
        runat="server">
            <ProgressTemplate>            
            <img alt="progress" src="../Images/Admin/loading.gif"/>
               Processing...            
            </ProgressTemplate>
        </asp:UpdateProgress>
        
    <div align="left">
        <rsweb:ReportViewer ID="rptVw80" runat="server" Font-Names="Verdana" Font-Size="10pt" AsyncRendering="false" ShowPrintButton="true"
            WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" ZoomMode="Percent" Width="100%" Height="550px">
        </rsweb:ReportViewer>
    </div>
</asp:Content>
<%--</form>
</body>
</html>--%>


