<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Feedback.aspx.cs" Inherits="Feedback" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE HTML>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
    <title>:: Feedback ::</title>


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
                    <td style="width: 10%; text-align: left">
                        <img src="images/newNS.png" height="100" /></td>
                    <td style="text-align: center">

                        <strong style="color: #0098DA">National Small Industries Corporation</strong>

                        <h4 style="color: #0098DA">(A Government of India Enterprise)</h4>

                        <b>Feedback for Single Point Registration Scheme</b>
                        <br />
                        <br />
                        <br />
                        <b>Feedback Form </b>


                    </td>
                    <%--<td style="width: 20%; text-align: left">
                            <img src="images/msme.png" height="100" width="150" /></td>--%>
                </tr>
            </table>

        </div>







        <form runat="server">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
            <asp:UpdatePanel ID="updSubmt" runat="server">
                <ContentTemplate>
                    <div class="inner-wrap">
                        <div class="two-col">
                            <div class="col1">

                                <label for="txtUAM">
                                    <span style="color: black; font-weight: bold">Name:</span> <span style="color: orangered">*</span><asp:TextBox ID="txtName" runat="server" placeholder="--Name--" ValidationGroup="validate" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtName" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>



                            </div>
                            <div class="col2">
                                <label for="txtPan">
                                    <span style="color: black; font-weight: bold">Email: </span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtEmailid" runat="server" ValidationGroup="validate" placeholder="--Email--" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtEmailid" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />

                                    <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="Enroll" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailid" ErrorMessage="Invalid Email Format"></asp:RegularExpressionValidator>

                                </label>
                            </div>
                        </div>

                        <div class="two-col">
                            <div class="col1">

                                <label for="txtCompanyName">
                                    <span style="color: black; font-weight: bold">Phone:</span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtPhone" runat="server" placeholder="--Phone--" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtPhone" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="Enroll" ValidationExpression="^[789]\d{9}$" ControlToValidate="txtPhone" ErrorMessage="Invalid mobile number"></asp:RegularExpressionValidator>

                                </label>
                            </div>

                            <div class="col2">
                                <label for="ddlOperation">
                                    <span style="color: black; font-weight: bold">Website:</span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtWebsite" runat="server" placeholder="--Website--" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtWebsite" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />


                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate="txtWebsite"
                                        Text="Invalid URL" ValidationExpression="(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$" runat="server" />


                                </label>
                            </div>
                        </div>

                        <div class="two-col">

                            <div class="two-col">
                                <label for="txtUAM">
                                    <span style="color: black; font-weight: bold">Product Profile : </span> <span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtSubject" runat="server" placeholder="--Product Profile--" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtSubject" ForeColor="Red" ErrorMessage="Required"  runat="server" ValidationGroup="Enroll" />

                                </label>
                            </div>
                        </div>
                        <div class="two-col">

                            <div class="two-col">
                                <label for="txtUAM">
                                    <span style="color: black; font-weight: bold">How do you rate Single Point Registration Scheme overall?<br />
                                        5-Excellent, 4-VeryGood, 3-Average, 2-Fair, 1-Poor </span>
                                    <br />

                                </label>
                            </div>
                        </div>
                        <div class="two-col">
                            <div class="two-col">
                                <label for="rbdUseful">
                                    <span style="color: black; font-weight: bold">1.How would you rate the usefulness of the Scheme ?</span> <span style="color: orangered">*</span>
                                    <asp:RadioButtonList ID="rbdUseful" runat="server" RepeatDirection="Horizontal" CssClass="field-full">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rbdUseful" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>
                            </div>
                        </div>
                        <div class="two-col">
                            <div class="two-col">
                                <label for="rbdRateOnline">
                                    <span style="color: black; font-weight: bold">2.How would you rate the online process ?</span> <span style="color: orangered">*</span>
                                    <asp:RadioButtonList ID="rbdRate" runat="server" RepeatDirection="Horizontal" CssClass="field-full">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rbdRate" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>
                            </div>
                        </div>

                        <div class="two-col">
                            <div class="two-col">
                                <label for="rbdKnow">
                                    <span style="color: black; font-weight: bold">3.How would you rate the Nodal Officer's knowledge in the subject  ?</span> <span style="color: orangered">*</span>
                                    <asp:RadioButtonList ID="rbdNodal" runat="server" RepeatDirection="Horizontal" CssClass="field-full">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rbdNodal" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>
                            </div>
                        </div>

                        <div class="two-col">
                            <div class="two-col">
                                <label for="rbdProcess">
                                    <span style="color: black; font-weight: bold">4.SPRS Scheme/process on Portal as stated was clearly met ?</span> <span style="color: orangered">*</span>
                                    <asp:RadioButtonList ID="rbdClear" runat="server" RepeatDirection="Horizontal" CssClass="field-full">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                        <asp:ListItem>3</asp:ListItem>
                                        <asp:ListItem>4</asp:ListItem>
                                        <asp:ListItem>5</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rbdClear" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>
                            </div>
                        </div>

                        <div class="two-col">
                            <div class="two-col">
                                <label for="rbdReckon">
                                    <span style="color: black; font-weight: bold">5. Will you recommend other MSEs for Single Point Registartion Scheme ?</span> <span style="color: orangered">*</span>
                                    <asp:RadioButtonList ID="rdbRecommend" runat="server" RepeatDirection="Horizontal" CssClass="field-full">
                                        <asp:ListItem Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ControlToValidate="rdbRecommend" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />
                                </label>
                            </div>
                        </div>


                        <div class="two-col">

                            <div class="two-col">
                                <label for="ddlConstitution">
                                    <span style="color: black; font-weight: bold">How this scheme has been helpful in the growth of your unit: </span><span style="color: orangered">*</span>
                                    <asp:TextBox ID="txtMessage" runat="server" placeholder="--Message--" TextMode="MultiLine" />
                                    <asp:RequiredFieldValidator ControlToValidate="txtMessage" ForeColor="Red" ErrorMessage="Required" runat="server" ValidationGroup="Enroll" />

                                </label>
                            </div>

                        </div>


                    </div>



                    <div class="button-section">
                        <center> <asp:Button ID="btnEnroll" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="Enroll" OnClick="btnEnroll_Click" /></center>
                    </div>


                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>

</body>

</html>
