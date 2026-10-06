<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UploadImage_1.aspx.vb" Inherits="RichTextEditor_EditorImageAppPath_UploadImage_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Upload Image</title>
    <link type="text/css" rel="Stylesheet" href="../EditorCSS/StyleSheet_1.css" />
    <script language="javascript" type="text/javascript" src="../EditorScripts/GlobalVar_1.js"></script>
    <script language="javascript" type="text/javascript" src="../EditorScripts/HtmlInsert_1.js"></script>
</head>
<body bgcolor="#EEEEEE">
    <form id="form1" runat="server">
        <table border="0" cellpadding="0" cellspacing="0" style="padding: 10px;">
            <tr>
                <td valign="top">
                    <span style="font-family: arial, verdana, helvetica; font-size: 11px; font-weight: bold;">
                        Insert Image:</span>
                    <table width="380" border="0" cellpadding="0" cellspacing="0" style="background-color: #F7F7F7;
                        border: 2px solid #FFFFFF; padding: 5px;">
                        <tr>
                            <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                font-size: 11px;" width="80">
                                Image URL:</td>
                            <td style="padding-bottom: 2px; padding-top: 0px;" width="300">
                                <asp:FileUpload ID="flupldImage" runat="server" CssClass="bodytext_1" />
                                <asp:RegularExpressionValidator ID="chkuploadfile" runat="Server"
                                 ControlToValidate="flupldImage"
                                 ErrorMessage="Only .jpg or .gif or .swf file format allowed on image uploading"
                                 ValidationExpression="(.*\.jpg)|(.*\.JPG)|(.*\.Jpg)|(.*\.JPEG)|(.*\.Jpeg)|(.*\.jpeg)|(.*\.gif)|(.*\.GIF)|(.*\.Gif)|(.*\.SWF)|(.*\.Swf)|(.*\.swf)"
                                 CssClass="bodytext_1"
                                 Display="None" SetFocusOnError="True" ValidationGroup="1024"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                 ControlToValidate="flupldImage"
                                 ErrorMessage="Select a file to upload"
                                 Display="None" CssClass="bodytext_1" SetFocusOnError="True" ValidationGroup="1024"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                font-size: 11px;">
                                Alternate Text:</td>
                            <td style="padding-bottom: 2px; padding-top: 0px;">
                                <asp:TextBox ID="txtAlt" runat="server" CssClass="bodytext_1" Width="180px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table width="380" border="0" cellpadding="0" cellspacing="0" style="margin-top: 10px;">
                        <tr>
                            <td style="vertical-align: top;">
                                <span style="font-family: arial, verdana, helvetica; font-size: 11px; font-weight: bold;">
                                    Layout:</span>
                                <table width="180" border="0" cellpadding="0" cellspacing="0" style="background-color: #F7F7F7;
                                    border: 2px solid #FFFFFF; padding: 5px;">
                                    <tr>
                                        <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;">
                                            Width:</td>
                                        <td style="width: 60px; padding-bottom: 2px; padding-top: 0px;">
                                            <asp:TextBox ID="txtWidth" runat="server" CssClass="bodytext_1" Width="68px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;">
                                            Height:</td>
                                        <td style="padding-bottom: 2px; padding-top: 0px;">
                                            <asp:TextBox ID="txtHeight" runat="server" CssClass="bodytext_1" Width="68px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;">
                                            Border:</td>
                                        <td style="padding-bottom: 2px; padding-top: 0px;">
                                            <asp:TextBox ID="txtBorder" runat="server" CssClass="bodytext_1" Width="68px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                            <td width="10">
                                &nbsp;</td>
                            <td style="vertical-align: top;">
                                <span style="font-family: arial, verdana, helvetica; font-size: 11px; font-weight: bold;">
                                    &nbsp;</span>
                                <table width="200" border="0" cellpadding="0" cellspacing="0" style="background-color: #F7F7F7;
                                    border: 2px solid #FFFFFF; padding: 5px;">
                                    <tr>
                                        <td style="width: 115px; padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;" width="100">
                                            Alignment:</td>
                                        <td style="width: 85px; padding-bottom: 2px; padding-top: 0px;">
                                            <asp:DropDownList ID="ddlAlignment" runat="server" CssClass="bodytext_1">
                                                <asp:ListItem Value="">Not Set</asp:ListItem>
                                                <asp:ListItem Value="left">Left</asp:ListItem>
                                                <asp:ListItem Value="right">Right</asp:ListItem>
                                                <asp:ListItem Value="texttop">Texttop</asp:ListItem>
                                                <asp:ListItem Value="absmiddle">Absmiddle</asp:ListItem>
                                                <asp:ListItem Value="baseline">Baseline</asp:ListItem>
                                                <asp:ListItem Value="absbottom">Absbottom</asp:ListItem>
                                                <asp:ListItem Value="bottom">Bottom</asp:ListItem>
                                                <asp:ListItem Value="middle">Middle</asp:ListItem>
                                                <asp:ListItem Value="top">Top</asp:ListItem>                                                
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;">
                                            Horizontal Space:</td>
                                        <td style="padding-bottom: 2px; padding-top: 0px;">
                                            <asp:TextBox ID="txtHSpace" runat="server" CssClass="bodytext_1" Width="68px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                            font-size: 11px;">
                                            Vertical Space:</td>
                                        <td style="padding-bottom: 2px; padding-top: 0px;">
                                            <asp:TextBox ID="txtVSpace" runat="server" CssClass="bodytext_1" Width="68px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div align="right" style="padding-top: 5px;">
                        &nbsp;<asp:ValidationSummary ID="ValidationSummary1" runat="server" CssClass="bodytext_1"
                            HeaderText="Plz follow below instructions" ShowMessageBox="True" ShowSummary="False"
                            ValidationGroup="1024" /> 
                        <asp:Button ID="btnSubmit" runat="server" CssClass="bodytext_1" Text="Submit" ValidationGroup="1024" />
                        <asp:Button ID="btnCancel" runat="server" CssClass="bodytext_1" Text="Cancel" CausesValidation="False" />
                    </div>
                </td>
                <td valign="top" style="padding-top:15px;">
                    <span style="font-family: arial, verdana, helvetica; font-size: 11px; font-weight: bold;">
                        Uploaded Image:
                    </span>
                    <asp:Panel ID="pnlImage" runat="server" Width="315px" Height="180px" BorderWidth="0" ScrollBars="Auto">
                        <table width="280" border="0" cellpadding="0" cellspacing="0" style="background-color: #F7F7F7;
                            border: 2px solid #FFFFFF;">
                            <tr>
                                <td style="padding-bottom: 2px; padding-top: 0px; font-family: arial, verdana, helvetica;
                                    font-size: 11px; vertical-align:top;">
                                    <asp:Table ID="tblimage" runat ="server"></asp:Table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
