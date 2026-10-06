<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TestApplications_Default" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Page</title>
    
    <style type="text/css">

body {
  margin: 0px;
  padding: 0px;
  background:#d6cbcf;
}

#header {
  background: #0f0;
  width: 800px;
  height: 100px;
}

.leftext
{
	
	font-family:Arial, Sans-Serif, Courier;
	font-size:12px;
	font-weight:bold;
	float:left;
	height:14px;
	width:40%;
	
}

.coloncls
{
	font-size:12px;
	font-weight:bold;
	float:left;
	height:14px;
	width:5%;
	
}

.clstxtbx
{
	background:#afacbc;
	font-family:Courier;
	font-size:12px;
	padding:1px;
	height:30px;
	float:left;
	width:100%;
}

.txtdiv
{
	height:40px;
	width:50%;
	float:left;
}

.clsrowdiv
{
	position:static;
	left:50px;
	height:40px;
	width:250px;
	padding-bottom:5px;
}

.tbldiv
{
	
	left:10px;
	top:100px;
	float:left;
	width:300px;
	height:500px;
}

#leftcol {
  background: #f00;
  float: left;
  width: 20%;
  height: 500px;
}
#rightcol {
  background: #f00;
  float: right;
  width: 20%;
  height: 500px;
}
#content {
  background: #fff;
  float: left;
  width: 59%;
  height: 500px;
}
#footer {
  background: #0f0;
  clear: both;
  width: 100%;
}

</style>

</head>
<body>
    <form id="form1" runat="server">
       <div align="left" style="width:700px;">
        <div id="header"></div>
            <div class="tbldiv">
            
                <div class="clsrowdiv">
                    <div class="leftext">User Description </div>
                    <div class="coloncls">:</div>
                    <div class="txtdiv">
                        <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server" 
                            CssClass="clstxtbx" Wrap="False"></asp:TextBox>
                    </div>
                 </div>
                 <div class="clsrowdiv">
                    <div class="leftext">User Description </div>
                    <div class="coloncls">:</div>
                    <div class="txtdiv">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="clstxtbx" 
                            TextMode="MultiLine" Wrap="False"></asp:TextBox>
                    </div>
                 </div>
                 
              </div>
             <div class="tbldiv">
            
                <div class="clsrowdiv">
                    <div class="leftext">User Description </div>
                    <div class="coloncls">:</div>
                    <div class="txtdiv">
                        <asp:TextBox ID="TextBox3" runat="server" CssClass="clstxtbx"></asp:TextBox>
                    </div>
                 </div>
                 <div class="clsrowdiv">
                    <div class="leftext">User Description </div>
                    <div class="coloncls">:</div>
                    <div class="txtdiv">
                        <asp:TextBox ID="TextBox4" runat="server" CssClass="clstxtbx"></asp:TextBox>
                    </div>
                 </div>
                 <div>
                    <asp:Button ID="btntext" runat="server" Text="to show" onclick="btntext_Click" />
                 </div>
                 
              </div>

        </div>
    </form>
</body>
</html>
