<%@ Page Title="" Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="PointOfInterest_View_Zone.aspx.cs" Inherits="appAdministrator_PointOfInterest_View_Zone" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <link href="http://www.nsic.co.in/css/bootstrap.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/font-awesome/css/font-awesome.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/slick/slick-theme.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/plugins/dataTables/datatables.min.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/animate.css?ver=2018" rel="stylesheet" />
    <link href="http://www.nsic.co.in/css/style.css?ver=2018" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href='http://fonts.googleapis.com/css?family=Bitter' rel='stylesheet' type='text/css'>
        <style type="text/css">
            body {
                background: #FFFFFF;
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
        <div class="form-style-10">






            







            
            
                <asp:UpdatePanel ID="updSubmt" runat="server">
                    <ContentTemplate>
                       
                      

                        



                        <%--<div class="section"><span>3</span>COMPANY DETAILS</div>--%>
                        <div class="inner-wrap">
                             <div class="two-col" style="text-align:center">
                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnCls"
                                 CausesValidation="false" OnClick="btnBack_Click" />
                                 </div>

                            <div class="two-col">
                                <div class="col1">

                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">UAM:</span><asp:TextBox ID="txtUAM" runat="server" MaxLength="12" ValidationGroup="validate" />
                                        
                                    </label>



                                </div>
                                <div class="col2">
                                    <label for="txtPan">
                                        <span style="color: black; font-weight: bold">PAN: </span><span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtPan" runat="server" MaxLength="10" ValidationGroup="validate" />
                                        
                                    </label>
                                </div>
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
                                       
                                    </label>
                                </div>

                                <div class="col2">
                                    <label for="ddlOperation">
                                        <span style="color: black; font-weight: bold">Nature of Operation:</span> <span style="color: orangered">*</span>
                                       <asp:TextBox ID="txtNatureofOperation" runat="server" />
                                        
                                    </label>
                                </div>
                            </div>

                            <div class="two-col">

                                <div class="col1">
                                    <label for="txtUAM">
                                        <span style="color: black; font-weight: bold">EM-II : </span>
                                        <asp:TextBox ID="txtEM2" runat="server" MaxLength="12" />
                                        
                                    </label>
                                </div>

                                <div class="col2">
                                    <label for="txtAuthorizedName">
                                        <span style="color: black; font-weight: bold">Authorized Person Name</span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtAuthorizedName" runat="server" />
                                        
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">

                                <div class="col1">
                                    <label for="ddlConstitution">
                                        <span style="color: black; font-weight: bold">Constitution: </span><span style="color: orangered">*</span>

                                       
                                        <asp:TextBox ID="txtConstituion" runat="server" />

                                        
                                    </label>
                                </div>
                                <div class="col1">
                                    <label for="ddlCategory">
                                        <span style="color: black; font-weight: bold">Enterprise Social Category:</span>  <span style="color: orangered">*</span>

                                       <asp:TextBox ID="txtSocialCategory" runat="server" />
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">


                                <div class="col1">
                                    <label for="txtGST">
                                        <span style="color: black; font-weight: bold">GST No:</span>
                                        <asp:TextBox ID="txtGST" runat="server" MaxLength="15" />
                                       
                                    </label>
                                </div>
                                <div class="col2">
                                    <label for="txtEmail">
                                        <span style="color: black; font-weight: bold">Email ID:</span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtEmail" runat="server" />
                                        
                                    </label>
                                </div>
                            </div>
                            <div class="two-col">
                                <div class="col1">
                                    <label for="txtMobile">
                                        <span style="color: black; font-weight: bold">Mobile Number:</span>  <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" />
                                       
                                    </label>
                                </div>
                                  <div class="col2">
                                    <label for="ddlNsicState">
                                        <span style="color: black; font-weight: bold">Select State:</span> <span style="color: orangered">*</span>
                                         <asp:TextBox ID="txtState" runat="server" />
                                    </label>
                                </div>
                            </div>

                             <div class="two-col">
                                <div class="col1" id="selector_cat">
                                    <label for="ddlNsicDistrict">
                                        <span style="color: black; font-weight: bold">Select your nearby Branch:</span> <span style="color: orangered">*</span>
                                       <asp:TextBox ID="txtBranch" runat="server"  />
                                    </label>
                                </div>
                                  <div class="col2">
                                    <label for="ddlNsicDistrict">
                                        <span style="color: black; font-weight: bold">Select Inspection Agency:</span> <span style="color: orangered">*</span>:

                                  <asp:TextBox ID="txtInspectionAgency" runat="server"  />
                                    </label>

                                </div>
                            </div>

                                 <div class="two-col">
                                <div class="two-col">
                                    <label>
                                        <span style="color: black; font-weight: bold">Major Product Details: </span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtProduct" runat="server" TextMode="MultiLine" placeholder="--Add your factory & product details--" />
                                    </label>
                                </div>
                            </div>


                             <div class="two-col">
                                <div class="two-col">
                                    <label>
                                        <span style="color: black; font-weight: bold">Remarks: </span> <span style="color: orangered">*</span>
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" placeholder="--Add your remarks--" />
                                        <asp:RequiredFieldValidator runat="server" ID="rfdRemarks" ControlToValidate="txtRemarks" ErrorMessage="Required" ValidationGroup="Enroll"></asp:RequiredFieldValidator>
                                    </label>
                                </div>
                            </div>


                        </div>
                        </div>
                         
                        <div class="button-section">
                                <center> <asp:Button ID="btnEnroll" runat="server" Text="Close" CausesValidation="true" ValidationGroup="Enroll" OnClick="btnEnroll_Click"  />

                                    <asp:Button ID="btnConverted" runat="server" Text="Converted" CausesValidation="true" ValidationGroup="Enroll" OnClick="Converted_Click"  />

                                    <asp:Button ID="Button1" runat="server" Text="Back" CssClass="btnCls"
                                 CausesValidation="false" OnClick="btnBack_Click" />
           

                                </center>

                            </div>
                            <br />
                            <br />

                      
                    
                           
                          
                            

                    
                            
                          
                    </ContentTemplate>
                </asp:UpdatePanel>
           
        </div>
        
      
</asp:Content>

