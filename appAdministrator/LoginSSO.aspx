<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginSSO.aspx.cs" Inherits="appAdministrator_LoginSSO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html>
<head runat="server">

    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>SINGLE SIGN-ON - SPRS</title>
    <link rel="SHORTCUT ICON" href="../Images/ns.ico" />

    <link href="../CSS/bootstrap.min.css" rel="stylesheet">
    <link href="../font-awesome/css/font-awesome.css" rel="stylesheet">

    <link href="../CSS/animate.css" rel="stylesheet">
    <link href="../CSS/style.css" rel="stylesheet">
</head>

<body style="background: #ECECEC">

    <div style="padding: 30px 10px 10px 10px">
        <div class="col-md-12">
            <img src="../Images/Logo.png" width="80" height="80" alt="logo" />
        </div>
        <div class="col-md-12">

            <div class="col-md-6">
                <div class="ibox float-e-margins " style="background: #FAFAFA; border: solid">

                    <br />

                    <div class="ibox-title">

                        <h4 class="font-bold">
                            <center>Welcome to Single Sign On for SPRS</center>
                        </h4>
                    </div>
                    <hr />
                    <div class="ibox-content">
                        <p>
                            This page facilitates user authentication and authorization across multiple branches and roles within the system. A user may be associated with one or more branches, and within each branch, may hold distinct roles.<br />
                            <br />
                            By selecting the appropriate branch and corresponding role during login, the system ensures that the user is redirected to the precise functional area and set of permissions applicable to that context.
                            <br />
                            <br />
                            This mechanism enables accurate access control, minimizes errors, and provides a seamless navigation experience for users managing multiple responsibilities.
                        </p>
                        <br />
                        <br />
                        <br />
                        <br />

                    </div>
                </div>


            </div>
            <div class="col-md-6">
                <div class="ibox float-e-margins " style="background: #FAFAFA; border: solid">
                    <br />
                    <div class="ibox-title">
                        <h4 class="font-bold">
                            <center>
                                Login Selection
                            </center>
                        </h4>
                    </div>
                    <hr />
                    <div class="ibox-content">
                        <form id="form1" runat="server">
                            <div class="row col-md-12 text-center" runat="server" id="divErrorMsg">
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group input-group">
                                    <span class="input-group-addon">Name:</span>
                                    <asp:Label ID="lblName" CssClass="form-control" runat="server"></asp:Label>
                                </div>
                            </div>
                            <%--<div class="col-lg-12">
                                <div class="form-group input-group">
                                    <span class="input-group-addon">User Name:</span>
                                    <asp:Label ID="lblNamee" CssClass="form-control" Text="PRASHANT KUMAR SRIVASTAVA" runat="server"></asp:Label>
                                </div>
                            </div>--%>
                            <div class="col-lg-12">
                                <div class="form-group input-group">
                                    <span class="input-group-addon">Branch:</span>
                                    <asp:DropDownList ID="rdbBranch" CssClass="form-control" runat="server">
                                       <%-- <asp:ListItem Text="Vishakhapatnam" />--%>
                                    </asp:DropDownList>

                                    <asp:RequiredFieldValidator
                                        ID="rfvRDBBranch"
                                        runat="server"
                                        InitialValue="0"
                                        ControlToValidate="rdbBranch"
                                        ErrorMessage="⚠ Please select a branch to continue."
                                        Display="Dynamic"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group input-group">
                                    <span class="input-group-addon">Role:</span>
                                    <asp:DropDownList ID="ddlRole" CssClass="form-control" runat="server">
                                        <%--<asp:ListItem Text="Accounts" />--%>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator
                                        ID="RequiredFieldValidator1"
                                        runat="server"
                                        ControlToValidate="ddlRole"
                                        InitialValue="0"
                                        ErrorMessage="⚠ Please select your role."
                                        Display="Dynamic"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:Button ID="Button1" CssClass="btn-primary" runat="server" Text="Proceed to Login" OnClick="btnGoAhead_Click" />
                                <br />
                                <br />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-6">
                Copyright NSIC
            </div>
            <div class="col-md-6 text-right">
                <small>© 2025-2026</small>
            </div>
        </div>
    </div>
</body>
</html>
