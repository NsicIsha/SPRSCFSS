<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PointOfInterest.aspx.cs" Inherits="PointOfInterest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>NSIC:: POINT OF INTEREST</title>


    <link href="http://www.nsic.co.in/css/bootstrap.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/font-awesome/css/font-awesome.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick-theme.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/dataTables/datatables.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/animate.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/style.css?ver=2018" rel="stylesheet" />


    <link href='http://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'>
        <style type="text/css">
            body {
                background: #AEE4FF;
            }

            .form-style-10 {
                max-width: 800px;
                padding: 30px;
                margin: 40px auto;
                background: #FFF;
                border-radius: 10px;
                -webkit-border-radius: 10px;
                -moz-border-radius: 10px;
                box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
                -moz-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
                -webkit-box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.13);
            }

                .form-style-10 .inner-wrap {
                    padding: 30px;
                    background: #F8F8F8;
                    border-radius: 6px;
                    margin-bottom: 15px;
                }

                .form-style-10 h1 {
                    background: #2A88AD;
                    padding: 20px 30px 15px 30px;
                    margin: -30px -30px 30px -30px;
                    border-radius: 10px 10px 0 0;
                    -webkit-border-radius: 10px 10px 0 0;
                    -moz-border-radius: 10px 10px 0 0;
                    color: #fff;
                    text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                    font: normal 30px 'Bitter', serif;
                    -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    border: 1px solid #257C9E;
                }

                    .form-style-10 h1 > span {
                        display: block;
                        margin-top: 2px;
                        font: 13px Arial, Helvetica, sans-serif;
                    }

                .form-style-10 label {
                    display: block;
                    font: 13px Arial, Helvetica, sans-serif;
                    color: #888;
                    margin-bottom: 15px;
                }

                .form-style-10 input[type="text"],
                .form-style-10 input[type="date"],
                .form-style-10 input[type="datetime"],
                .form-style-10 input[type="email"],
                .form-style-10 input[type="number"],
                .form-style-10 input[type="search"],
                .form-style-10 input[type="time"],
                .form-style-10 input[type="url"],
                .form-style-10 input[type="password"],
                .form-style-10 textarea,
                .form-style-10 select {
                    display: block;
                    box-sizing: border-box;
                    -webkit-box-sizing: border-box;
                    -moz-box-sizing: border-box;
                    width: 90%;
                    padding: 8px;
                    border-radius: 6px;
                    -webkit-border-radius: 6px;
                    -moz-border-radius: 6px;
                    border: 2px solid #fff;
                    box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    -moz-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    -webkit-box-shadow: inset 0px 1px 1px rgba(0, 0, 0, 0.33);
                    height: 50%;
                }

                .form-style-10 .section {
                    font: normal 20px 'Bitter', serif;
                    color: #2A88AD;
                    margin-bottom: 5px;
                }

                    .form-style-10 .section span {
                        background: #2A88AD;
                        padding: 5px 10px 5px 10px;
                        position: absolute;
                        border-radius: 50%;
                        -webkit-border-radius: 50%;
                        -moz-border-radius: 50%;
                        border: 4px solid #fff;
                        font-size: 14px;
                        margin-left: -45px;
                        color: #fff;
                        margin-top: -3px;
                    }

                .form-style-10 input[type="button"],
                .form-style-10 input[type="submit"] {
                    background: #2A88AD;
                    padding: 8px 20px 8px 20px;
                    border-radius: 5px;
                    -webkit-border-radius: 5px;
                    -moz-border-radius: 5px;
                    color: #fff;
                    text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.12);
                    font: normal 30px 'Bitter', serif;
                    -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.17);
                    border: 1px solid #257C9E;
                    font-size: 15px;
                }

                    .form-style-10 input[type="button"]:hover,
                    .form-style-10 input[type="submit"]:hover {
                        background: #2A6881;
                        -moz-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                        -webkit-box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                        box-shadow: inset 0px 2px 2px 0px rgba(255, 255, 255, 0.28);
                    }

                .form-style-10 .privacy-policy {
                    float: right;
                    width: 250px;
                    font: 12px Arial, Helvetica, sans-serif;
                    color: #4D4D4D;
                    margin-top: 10px;
                    text-align: right;
                }

            .two-col {
                overflow: hidden; /* Makes this div contain its floats */
            }

                .two-col .col1,
                .two-col .col2 {
                    width: 50%;
                }

                .two-col .col1 {
                    float: left;
                }

                .two-col .col2 {
                    float: right;
                }

            /*.two-col label {
                    display: block;
                }*/
        </style>

</head>
<body>
     

     

        <div class="form-style-10">






            <div style="width: 98%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 20%; text-align: left">
                            <img src="images/newNS.png" height="100" /></td>
                        <td style="width: 60%; text-align: center">
                            <%-- <div class="col-sm-8 col-xs-12">
                        <div class="text-success">--%>
                            <strong style="color: #0098DA">National Small Industries Corporation</strong>

                            <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>
                            <%-- </div>
                        <div>--%>
                            <b>Registration under Single Point Registration Scheme</b>
                            <br />
                            <br />
                            <br />
                            
                            <%--</div>
                    </div>--%>

                        </td>
                        <td style="width: 20%; text-align: left">
                            <img src="images/msme.png" height="100" width="150" />



                        </td>
                    </tr>
                </table>
               <%-- <div class="text-center">
                    <div class="col-sm-2 col-xs-12">
                        <img src="images/newNS.png" height="100" />
                    </div>
                    <div class="col-sm-8 col-xs-12">
                        <div class="text-success">
                            <strong>National Small Industries Corporation</strong>

                           <h4> (A Government of India Enterprise)</h4>
                        </div>
                        <div>
                       <b>   Registration in Single Point Registration Scheme</b>
                        </div>
                    </div>

                    <div  class="col-md-1 col-xs-10 media-n hidden-xs" >
                        <img src="images/msme.png" height="100" width="150" />
                    </div>
                </div>--%>
            </div>

             <div style="width: 100%; padding-top: 5px; padding-bottom: 5px;" class="container top-logo-panel">
                  <table border="0" cellpadding="0" cellspacing="0" style="background-color:#2596be; color:#ffffff">
                <tr align="left" valign="top">
                    <td valign="middle" align="left" style="width:500px; 

padding-left:5px; height:30px;">
                        <asp:Label ID="lblUser" runat="server"  Text="fdsfasd"

ForeColor="#22518F"></asp:Label>
                    </td>
                    <td valign="middle" align="right" style="padding-right:5px; 

width:400px;" class="headerLink">
                        <asp:Label ID="lblLink" runat="server"  Text="fdsfasd"

CssClass="headerLink" ></asp:Label>
                     
                    </td>
                </tr>
            </table>
                 </div>





            <form runat="server">
                <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </asp:ToolkitScriptManager>
                <asp:UpdatePanel ID="updSubmt" runat="server">
                    <ContentTemplate>
                        <div class="section" style="padding-top: 50px">
                            <span>1</span>REGISTRATION CRITERIA          
                        </div>
                        <div class="inner-wrap">
                            <ol>
                                <li>Micro and Small Enterprises (MSEs) involved in manufacturing & Services are eligible for registration. </li>
                                <li>Traders are not eligible for registration under this Scheme. </li>
                                <li>MSEs already commenced their commercial production (Start Ups) but not completed one year of existence are eligible for Provisional Registration for one year.</li>
                            </ol>
                        </div>


                        <div class="section">
                            <span>2</span>REGISTRATION PROCESS         
                        </div>
                        <div class="inner-wrap">
                            <ol>
                                <li>Once this form of interest is submitted, you will receive e- mail regarding contact details of NSIC branch selected by you. </li>
                                <li>The branch can be contacted to complete requisite formalities & documentation.</li>
                                <li><span style="color: red">For your ready reference, the application format along with checklist of requisite documents & fee is placed at <a href="http://www.nsic.co.in/Schemes/Single-Point-Registration.aspx" target="_blank">http://www.nsic.co.in/Schemes/Single-Point-Registration.aspx</a> under link DOCUMENTS TO BE SUBMITTED BY THE MICRO & SMALL ENTERPRISES AT THE TIME OF FRESH REGISTRATION</span></li>
                            </ol>

                            <span style="text-decoration-line: underline">P.S.: Submission of following information is a merely interest shown by you for registration under Single Point Registration Scheme (SPRS).</span>

                        </div>



                        <div class="section"><span>3</span>COMPANY DETAILS</div>
                        <div class="inner-wrap">
                            <div class="two-col">
                                <div class="col1">

                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">UAM:</span><%-- <span style="color: orangered">*</span>--%><asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        <%--<asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />--%>
                                    </label>



                                </div>
                                <div class="col2">
                                    <label for="txtPan">
                                        <span style="color: black; font-weight: bold">PAN: </span><span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtPan" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPan"  
                    Display="Dynamic" ForeColor="Red" ErrorMessage="InValid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}" ValidationGroup="Enroll" ></asp:RegularExpressionValidator>  
                                       
                                    </label>
                                </div>
                            </div>

                             <div class="text-center">
                                            <asp:Button ID="btnf" runat="server" Text="Fetch" OnClick="btnf_Click" ValidationGroup="validate" />

                                            <asp:UpdateProgress ID="update_progress_search" runat="server" AssociatedUpdatePanelID="updSubmt">

                                                <ProgressTemplate>
                                                    <img alt="progress" src="Images/Admin/loading.gif" />
                                                    Processing...            
                                                </ProgressTemplate>

                                            </asp:UpdateProgress>
                                        </div>
                            <div class="two-col">
                                <div class="col1">
                                </div>
                                <div class="col2">
                                    <asp:Label ID="lblMsg" ForeColor="Red" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="two-col">
                                <div class="col1">

                                    <label for="txtCompanyName">
                                        <span style="color: black; font-weight: bold">Company Name:</span><span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtCompanyName" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtCompanyName" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>

                                <div class="col2">
                                    <label for="ddlOperation">
                                        <span style="color: black; font-weight: bold">Nature of Operation:</span> <span style="color: orangered">*</span>
                                        <asp:DropDownList ID="ddlOperation" runat="server">
                                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Value="1">Manufacturing</asp:ListItem>
                                            <asp:ListItem Value="2">Service</asp:ListItem>
                                            <asp:ListItem Value="3">Both</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlOperation" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>
                            </div>

                            <div class="two-col">

                                <div class="col1">
                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">EM-II : </span>
                                        <asp:TextBox ID="txtEM2" runat="server" MaxLength="12" />
                                        <%--<asp:RequiredFieldValidator ControlToValidate="txtUAM" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />--%>
                                    </label>
                                </div>

                                <div class="col2">
                                    <label for="txtAuthorizedName">
                                        <span style="color: black; font-weight: bold">Authorized Person Name</span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtAuthorizedName" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAuthorizedName" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">

                                <div class="col1">
                                    <label for="ddlConstitution">
                                        <span style="color: black; font-weight: bold">Constitution: </span><span style="color: orangered">*</span>

                                        <%-- <asp:RadioButtonList ID="rdConstitution" runat="server" RepeatLayout="Table" RepeatDirection="Vertical"
                                TabIndex="5">
                            </asp:RadioButtonList>--%>


                                        <asp:DropDownList ID="ddlConstitution" runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlConstitution" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>
                                <div class="col1">
                                    <label for="ddlCategory">
                                        <span style="color: black; font-weight: bold">Enterprise Social Category:</span>  <span style="color: orangered">*</span>

                                        <%--  <asp:DropDownList ID="ddl_1e" runat="server" CssClass="clsDDL" AutoPostBack="false"
                                                TabIndex="52" ValidationGroup="1">
                                            </asp:DropDownList>--%>


                                        <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="false">
                                            <%-- <asp:ListItem Text="--Select--" Value="NULL"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlCategory" InitialValue="NULL" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">


                                <div class="col1">
                                    <label for="txtGST">
                                        <span style="color: black; font-weight: bold">GST No:</span>
                                        <asp:TextBox ID="txtGST" runat="server" MaxLength="15" />
                                        <%--<asp:RequiredFieldValidator ControlToValidate="txtGST" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />--%>
                                    </label>
                                </div>
                                <div class="col2">
                                    <label for="txtEmail">
                                        <span style="color: black; font-weight: bold">Email ID:</span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtEmail" runat="server" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtEmail" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                   <asp:RegularExpressionValidator ID="validateEmail"     runat="server" ErrorMessage="Invalid email."   ControlToValidate="txtEmail" ValidationGroup="Enroll"   ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ></asp:RegularExpressionValidator>
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">


                                <div class="col1">
                                    <label for="txtMobile">
                                        <span style="color: black; font-weight: bold">Mobile Number:</span>  <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtMobile" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3"     runat="server" ErrorMessage="Invalid Mobile."   ControlToValidate="txtMobile" ValidationGroup="Enroll"   ValidationExpression="^[7-9][0-9]{9}$"></asp:RegularExpressionValidator>

                                    </label>
                                </div>
                            </div>

                        </div>

                        <%--       <div class="section"><span>3</span>FACTORY DETAILS</div>
                <div class="inner-wrap">                   
                    <div class="two-col">
                        <div class="col1">
                           
                        </div>
                        <div class="col2">                        
                        </div>
                    </div>

<asp:GridView ID="gv_director" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    OnRowDataBound="gv_director_RowDataBound" DataKeyNames="rowno" OnRowEditing="gv_director_RowEditing"
                                    OnRowUpdating="gv_director_RowUpdating" 
                                    onrowdeleting="gv_director_RowDeleting">
                                    <HeaderStyle CssClass="GrdviewHeader" Height="30" />
                                    <RowStyle CssClass="GrdviewRowOdd" />
                                    <AlternatingRowStyle CssClass="GrdviewRowEven" />
                                    <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                    <Columns>
                                    
                                        <asp:TemplateField HeaderText="State">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_state" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lbledit_state" runat="server" Visible="false" Text='<%# Bind("stateid") %>'></asp:Label>
                                                <asp:DropDownList ID="ddl_director_state" OnSelectedIndexChanged="bind_city_edit_statechange"
                                                    AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                              
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_footer_state" AutoPostBack="true" runat="server" ValidationGroup="gridd"
                                                    OnSelectedIndexChanged="bind_city_footer_statechange" Width="150px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqstate" runat="server" Display="Dynamic" ErrorMessage="Please Select State"
                                                    ControlToValidate="ddl_footer_state" InitialValue="0" SetFocusOnError="true"
                                                    ValidationGroup="gridd"></asp:RequiredFieldValidator>
                                               
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="District/ City">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_director_city" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label ID="lbledit_city" runat="server" Visible="false" Text='<%# Bind("cityId") %>'></asp:Label>
                                                <asp:DropDownList ID="ddl_director_city" AutoPostBack="true" runat="server">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_footer_city" ValidationGroup="gridd" runat="server" Width="150px">
                                                </asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="reqcity" runat="server" Display="Dynamic" ErrorMessage="Please Select City"
                                                    ControlToValidate="ddl_footer_city" SetFocusOnError="true" ValidationGroup="gridd"
                                                    InitialValue="0"></asp:RequiredFieldValidator>
                                              
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                                                      
                                                                        
                                        <asp:TemplateField HeaderText="Options">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_edit" runat="server" CommandArgument='<%# Bind("Name") %>'
                                                    CommandName="edit" CausesValidation="false">
                                                   Edit
                                                </asp:LinkButton>
                                                &nbsp;&nbsp;
                                                 <asp:LinkButton ID="lnk_delete" runat="server" CommandArgument='<%# Bind("Name") %>'
                                                  Visible="false"   OnClientClick="return confirm('Are you want to delete ?');"   CommandName="delete" CausesValidation="false">
                                                   Delete
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnk_update" runat="server" CommandName="update" CausesValidation="false">
                                                   Update
                                                </asp:LinkButton>
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:Button ID="btn_add" runat="server" Text="Add New" ValidationGroup="gridd" OnClick="AddnewDirector" />
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>



                        
                </div>--%>

                        <div class="section"><span>4</span>FURTHER INFORMATION</div>
                        <div class="inner-wrap">
                            <label>
                                <span style="color: black; font-weight: bold">NSIC Branch with which you want registration</span>
                            </label>
                            <div class="two-col">
                                <div class="col1">
                                    <label for="ddlNsicState">
                                        <span style="color: black; font-weight: bold">Select State:</span> <span style="color: orangered">*</span>
                                        <asp:DropDownList ID="ddlNsicState" runat="server" AutoPostBack="true" placeholder="State" OnSelectedIndexChanged="ddlNsicState_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select State--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlNsicState" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>

                                <div class="col2" id="selector_cat">
                                    <label for="ddlNsicDistrict">
                                        <span style="color: black; font-weight: bold">Select your nearby Branch:</span> <span style="color: orangered">*</span>
                                        <asp:DropDownList ID="ddlNsicDistrict" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNsicDistrict_SelectedIndexChanged">
                                            <asp:ListItem Text="--Select Branch--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlNsicDistrict" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>
                                </div>
                            </div>



                            <div class="two-col">
                                <div class="two-col">
                                    <label for="ddlNsicState">
                                        <asp:GridView ID="grdFactories" AutoGenerateColumns="false" runat="server" EmptyDataText="No Records Available">
                                            <Columns>
                                                <asp:BoundField HeaderText="Name" DataField="fGPtbl_MB_Name" />
                                                <asp:BoundField HeaderText="Address" DataField="fGtbl_MB_Address" />
                                                <asp:BoundField HeaderText="Phone" DataField="fGtbl_MB_Phone" />
                                                <asp:BoundField HeaderText="Email" DataField="fGtbl_MB_Email" />

                                            </Columns>
                                        </asp:GridView>
                                    </label>
                                </div>
                                <%--<div class="col2">
                            <label for="ddlNsicDistrict">
                                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="ddlNsicDistrict_SelectedIndexChanged">
                                     <asp:ListItem Text="--Select Branch--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlNsicDistrict" InitialValue="NULL" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                            </label>
                        </div>--%>
                            </div>

                            <div class="two-col">
                                <div class="col1">
                                    <label for="ddlNsicDistrict">
                                        <span style="color: black; font-weight: bold">Select Inspection Agency:</span> <span style="color: orangered">*</span>:

                                <asp:DropDownList ID="ddlInspectionAgency" runat="server">
                                    <asp:ListItem Text="--Select Agency--" Value="0"></asp:ListItem>
                                </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlInspectionAgency" InitialValue="0" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                    </label>

                                </div>


                            </div>

                            <div class="two-col">
                                <div class="two-col">
                                    <label>
                                        <span style="color: black; font-weight: bold">Major Product Details:(Limit 200 words)</span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtProduct" runat="server" TextMode="MultiLine" placeholder="--Add your factory & product details--" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtProduct" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />

                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                            ControlToValidate="txtProduct"
                                            ValidationExpression="^(\b\S+\b\s*){0,10}$"
                                            Display="Dynamic"
                                            ErrorMessage="You can use Maximum 200 Words">
                                        </asp:RegularExpressionValidator>


                                    </label>
                                </div>
                            </div>
                            <div class="button-section">
                                <center> <asp:Button ID="btnEnroll" runat="server" Text="REGISTER" CausesValidation="true" ValidationGroup="Enroll" OnClick="btnEnroll_Click" /></center>
                            </div>
                            <br />
                            <br />
                             


                             


                    </ContentTemplate>
                </asp:UpdatePanel>
            </form>


             <div class="panel-footer">
                <p class="text-center">
                    <span class="text-center"><small><strong>Copyright</strong> National Small Industries Corporation © 2018-2019</small></span>
                    <br>
                    <span class="text-center"><small>Website Design &amp; Content Managed by NSIC </small></span>
                </p>
            </div>
        </div>

    </body>


    </html>

 
