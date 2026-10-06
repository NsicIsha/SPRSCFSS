<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeedbackForm.aspx.cs" Inherits="FeedbackForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::SPRS FEEDBACK FORM::</title>
    <style type="text/css">
        .form-style-9 {
            max-width: 450px;
            background: #FAFAFA;
            padding: 30px;
            margin: 50px auto;
            box-shadow: 1px 1px 25px rgba(0, 0, 0, 0.35);
            border-radius: 10px;
            border: 6px solid #305A72;
        }

            .form-style-9 ul {
                padding: 0;
                margin: 0;
                list-style: none;
            }

                .form-style-9 ul li {
                    display: block;
                    margin-bottom: 10px;
                    min-height: 35px;
                }

                    .form-style-9 ul li .field-style {
                        box-sizing: border-box;
                        -webkit-box-sizing: border-box;
                        -moz-box-sizing: border-box;
                        padding: 8px;
                        outline: none;
                        border: 1px solid #B0CFE0;
                        -webkit-transition: all 0.30s ease-in-out;
                        -moz-transition: all 0.30s ease-in-out;
                        -ms-transition: all 0.30s ease-in-out;
                        -o-transition: all 0.30s ease-in-out;
                    }

                        .form-style-9 ul li .field-style:focus {
                            box-shadow: 0 0 5px #B0CFE0;
                            border: 1px solid #B0CFE0;
                        }

                    .form-style-9 ul li .field-split {
                        width: 49%;
                    }

                    .form-style-9 ul li .field-full {
                        width: 100%;
                    }

                    .form-style-9 ul li input.align-left {
                        float: left;
                    }

                    .form-style-9 ul li input.align-right {
                        float: right;
                    }

                    .form-style-9 ul li textarea {
                        width: 100%;
                        height: 100px;
                    }

                    .form-style-9 ul li input[type="button"],
                    .form-style-9 ul li input[type="submit"] {
                        -moz-box-shadow: inset 0px 1px 0px 0px #3985B1;
                        -webkit-box-shadow: inset 0px 1px 0px 0px #3985B1;
                        box-shadow: inset 0px 1px 0px 0px #3985B1;
                        background-color: #216288;
                        border: 1px solid #17445E;
                        display: inline-block;
                        cursor: pointer;
                        color: #FFFFFF;
                        padding: 8px 18px;
                        text-decoration: none;
                        font: 12px Arial, Helvetica, sans-serif;
                    }

                        .form-style-9 ul li input[type="button"]:hover,
                        .form-style-9 ul li input[type="submit"]:hover {
                            background: linear-gradient(to bottom, #2D77A2 5%, #337DA8 100%);
                            background-color: #28739E;
                        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >

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
                            <b>Feedback under Single Point Registration Scheme</b>
                            <br />
                            <br />
                            <br />
                           <%-- <b>Form of Interest</b>--%>
                            <%--</div>
                    </div>--%>

                        </td>
                        <td style="width: 20%; text-align: left">
                            <img src="images/msme.png" height="100" width="150" /></td>
                    </tr>
                </table>
                 
            </div>
        <div class="form-style-9">
        <ul>
            <li>
                <asp:TextBox runat="server" ID="txtName" class="field-style field-split align-left" placeholder="--Name--" />
                <asp:RequiredFieldValidator ID="rfdName" runat="server" ControlToValidate="txtName" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtEmailID" type="email" class="field-style field-split align-right" placeholder="--Email--" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="email" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:TextBox runat="server" ID="txtPhone" class="field-style field-split align-left" placeholder="--Phone--" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPhone" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:TextBox runat="server" ID="txtWebsite" class="field-style field-split align-right" placeholder="--Website--" />
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtWebsite" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:TextBox ID="txtSubject" runat="server" class="field-style field-full align-none" placeholder="--Subject--" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtSubject" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>
            </li>
            <li>
                <asp:TextBox TextMode="MultiLine" class="field-style" placeholder="--Message--" ID="txtMessage" runat="server" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMessage" ErrorMessage="Reqired" ForeColor="Red"></asp:RequiredFieldValidator>

            </li>
            <li>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                <asp:Label runat="server" ID="lblMsg" Visible="false" />
            </li>
        </ul>
            </div>
    </form>
</body>
</html>
