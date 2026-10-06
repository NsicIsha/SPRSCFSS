<%@ Page Title="Company Status" Language="C#" MasterPageFile="~/appMaster/Company.master" AutoEventWireup="true" CodeFile="CompanyStatusTrack.aspx.cs" Inherits="appCompany_CompanyStatusTrack" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <script type="text/javascript" language="javascript" >
        function hide() {
         //   document.getElementById('masterdiv').style.visibility = 'hidden';
               }


//        var jq = jQuery.noConflict();
//        jq(document).ready(function() {
//            jq("#masterdiv").hide();
//        });


</script>
    <div align="left">
        <div align="left" class="headerAll">
            Application Status
        </div>
        <div align="left">
            &nbsp;
        </div>
            <div align="left">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr align="left">
                        <td align="left" style="width:200px;">
                            <asp:Label ID="lblReference" runat="server" Text="Enter SPR reference number : " CssClass="leftHeading"></asp:Label>
                        </td>
                        
                        <td align="left" style="width:100px;">
                            <asp:TextBox ID="txtReference" runat="server" CssClass="txt_bx"></asp:TextBox>
                            <asp:Label ID="Label1" runat="server" CssClass="errMessage" Visible="false"></asp:Label>
                            
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please provide GP reference number"
                            ControlToValidate="txtReference" SetFocusOnError="true" Display="None" ValidationGroup="1"></asp:RequiredFieldValidator>
                            <asp:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" TargetControlID="RequiredFieldValidator1">
                            </asp:ValidatorCalloutExtender>
                        </td>
                    </tr>
                    <tr align="left">
                        <td align="left"></td>
                        <td align="right">
                           <asp:Button ID="btnReference" runat="server" Text="Submit" CssClass="btnCls" 
                                onclick="btnReference_Click" ValidationGroup="1"  />
                          <%--<asp:HyperLink ID="lnkReference" runat="server" Text="Submit" CssClass="btnCls" ValidationGroup="1" ></asp:HyperLink>--%>
                                
                        </td>
                    </tr>
                </table>
                    
                    
            </div>
            
            <%--<div align="left" id="company" runat="server">
                <table border="0" cellpadding="0" cellspacing="0">
                    <tr align="left">
                        <td align="left" style="width:200px;">
                            <asp:Label ID="lblCompanyname" runat="server" Text="Company name" CssClass="leftHeading"></asp:Label>
                        </td>
                        <td align="left"><strong>:</strong></td>
                        <td align="left" style="width:100px;">
                            <asp:Literal ID="ltlCompanyname" runat="server" ></asp:Literal>
                        </td>
                    </tr>
                    <tr align="right" style="height:10px;">
                    </tr>
                    <tr align="left">
                        <td align="left" style="width:200px;">
                            <asp:Label ID="lblStatus" runat="server" Text="Status" CssClass="leftHeading"></asp:Label>
                        </td>
                        <td align="left"><strong>:</strong></td>
                        <td align="left" style="width:100px;">
                            <asp:Literal ID="ltlStatus" runat="server" ></asp:Literal>
                        </td>
                    </tr>
                </table>
            </div>--%>
            
           
            
                
    </div>
</asp:Content>

