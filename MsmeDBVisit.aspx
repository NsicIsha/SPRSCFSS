<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="MsmeDBVisit.aspx.cs" Inherits="MsmeDBVisit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="UserControl/Header_Admin.ascx" TagName="Header_Admin" TagPrefix="uc1" %>
<%@ Register Src="UserControl/Footer_Admin.ascx" TagName="Footer_Admin" TagPrefix="uc2" %>
<%@ Register Src="UserControl/TreeMenu_Admin.ascx" TagName="TreeMenu_Admin" TagPrefix="uc3" %>

<%@ Register Src="UserControl/HeaderImage.ascx" TagName="HeaderImage" TagPrefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta name="description" content="nsic certificate,government purchase,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />

 

<meta name="keywords" content="nsic certificate,gp,government purchase,spr,SPRS Certificate,single point registration scheme,

government stores purchase programme,public procurement policy,gazette notification,free tenders,

exemption from emd,micro & small enterprises (MSEs) registration,provisional registration certificate,

single point registration scheme certificate validity is for the period of one year,

government purchase enlistment certificate" />


     <title>Single Point Registration Scheme (SPRS)</title>

    <script language="javascript" type="text/javascript" src="../JScript/jquery-1.7.1.js"></script>

    <link type="text/css" rel="Stylesheet" href="CSS/Style.css" />
     <script src="SlidShow/jquery-1.2.6.min.js" type="text/javascript"></script>

    <script src="SlidShow/JScript.js" type="text/javascript"></script>
    
     <style type="text/css">
        #slideshow
        {
            position: relative;
            height: 223px;
            width: 975px;
        }
        #slideshow IMG
        {
            position: absolute;
            top: 0;
            left: 0;
            z-index: 8;
            opacity: 0.0;
        }
        #slideshow IMG.active
        {
            z-index: 10;
            opacity: 1.0;
        }
        #slideshow IMG.last-active
        {
            z-index: 9;
        }
    </style>
    

</head>
<body>
    <center>
        <form id="form1" runat="server">
      
       <label for="ddlNsicState">
                                          <asp:Table ID="Table2" runat="server" border="1" CellPadding="0" CellSpacing="0" Width="100%" CssClass="clsOutlineDyntblBorder">
                            </asp:Table>
        </form>
    </center>
    
</body>
</html>

