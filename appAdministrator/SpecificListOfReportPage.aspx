<%@ Page Language="C#" MasterPageFile="~/appMaster/Admin.master" AutoEventWireup="true" CodeFile="SpecificListOfReportPage.aspx.cs" Inherits="appAdministrator_SpecificListOfReportPage" Title="Report Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .style1 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
            width: 263px;
        }
        .style2 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
            width: 282px;
        }

        .style3 {
            width: 282px;
            white-space: nowrap;
        }

        .style4 {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            height: 35px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: bold;
            width: 282px;
            margin-left: 300px;
        }


        .Message {
            font-family: Arial;
            text-align: center;
            font: 26px;
            font-weight: bold;
        }

        .Alignright {
            text-align: right;
        }

        .clsDDlReport {
            width: 175px;
            background-color: #dddddd;
            border: #999999 solid 1px;
            padding: 3px;
        }

        .imagetext {
            border: 0;
            content: attr(title);
        }
    </style>
    <style type="text/css">
        div.appear {
            width: 400px;
            border: #000 2px solid;
            background: #F8F8F8;
            position: relative;
            top: 5px;
            left: 15px;
            display: none;
            padding: 0 20px 20px 20px;
            z-index: 1000000;
        }

        div.hover {
            cursor: pointer;
            width: 5px;
            vertical-align: top;
        }

            div.hover:hover div.appear {
                display: block;
            }
    </style>
    <script type="text/javascript" language="javascript">


        function jsIsUserFriendlyChar(val, step) {
            // Backspace, Tab, Enter, Insert, and Delete  
            if (val == 8 || val == 9 || val == 13 || val == 45 || val == 46) {
                return true;
            }
            // Ctrl, Alt, CapsLock, Home, End, and Arrows  
            if ((val > 16 && val < 21) || (val > 34 && val < 41)) {
                return true;
            }
            if (step == "Decimals") {
                if (val == 190 || val == 110) {  //Check dot key code should be allowed
                    return true;
                }
            }
            // The rest  
            return false;
        }

        function jsDecimals(e) {

            var evt = (e) ? e : window.event;
            var key = (evt.keyCode) ? evt.keyCode : evt.which;
            if (key != null) {
                key = parseInt(key, 10);
                if ((key < 48 || key > 57) && (key < 96 || key > 105)) {
                    if (!jsIsUserFriendlyChar(key, "Decimals")) {
                        return false;
                    }
                }
                else {
                    if (evt.shiftKey) {
                        return false;
                    }
                }
            }
            return true;
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="MainDiv" style="width: 100%; height: 100%; margin-left: 30px">
        <table cellpadding="2" cellspacing="2" border="0">
            <tr>
                <td>


                    <div id="Select_Option" style="width: 100%">
                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lblCertificateRequest" runat="server" Text="Reports" CssClass="headerAll"></asp:Label>
                                </td>
                            </tr>


                            <tr>
                                <td class="leftHeading" align="left" style="margin-left: 0px; white-space: nowrap;">LIST OF REPORTS:&nbsp;<asp:DropDownList ID="ddlSelectCriteria" runat="server" AutoPostBack="true" Width="220px"
                                    OnSelectedIndexChanged="ddlSelectCriteria_SelectedIndexChanged">
                                </asp:DropDownList>





                                </td>
                            </tr>

                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="lst_report_type" runat="server">


                        <table cellpadding="0" cellspacing="0" border="1">

                            <tr id="Selection_crietria" runat="server">
                                <td class="leftHeading" style="margin-left: 0px" align="left">SELECTION CRITERIA:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblselected_criteria" Text="" runat="server"></asp:Label></td>

                            </tr>
                            <tr id="tr_search_company" runat="server">
                                <td align="left">
                                    <table cellpadding="2" cellspacing="2" border="0">
                                        <tr>
                                            <td align="left" valign="top" class="leftHeading">Search Company Name:</td>
                                            <td style="width: 10px;"></td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="txt_search_company" runat="server" Text=""></asp:TextBox></td>

                                        </tr>

                                    </table>
                                </td>
                            </tr>


                            <tr id="lst_statuswise" runat="server" style="height: 20px;">
                                <td>
                                    <div id="lst_stauswise_div">
                                        <table cellpadding="2" cellspacing="2" border="0" style="width: 100%">
                                            <tr>
                                                <td class="leftHeading" align="left" valign="top" style="width: 150px;">StatusWise:</td>

                                                <td align="left" valign="top" style="width: 120px;">
                                                    <asp:DropDownList ID="ddlstatuswise" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlstatuswise_SelectedIndexChanged">
                                                        <%--<asp:ListItem Selected="True">--Select--</asp:ListItem>
<asp:ListItem>Registered</asp:ListItem>
<asp:ListItem>Inprocess</asp:ListItem>
<asp:ListItem>Waiting</asp:ListItem>
<asp:ListItem>Expired</asp:ListItem>
<asp:ListItem>Incomplete</asp:ListItem>--%>
                                                        <%-- <asp:ListItem>Rejected</asp:ListItem>--%>
                                                        <%-- <asp:ListItem>Inspection Passed</asp:ListItem>--%>
                                                        <%-- <asp:ListItem>Expired list company</asp:ListItem>--%>
                                                    </asp:DropDownList>


                                                </td>
                                                <td align="left" valign="top">
                                                    <div class="hover" align="left">
                                                        <img id="image_help_id" src="../Images/imagesCARED60B.jpg" />
                                                        <div class="appear">
                                                            <b>Registered </b>: List of registered companies.<br />
                                                            <b>Inprocess</b> : Application forwarded to Inspection Agency for inspection report.<br />
                                                            <b>Waiting</b> : Application completed but waiting for branch to forward to Inspection Agency.<br />
                                                            <b>Expired</b> : Company whose registration validity has expired.<br>
                                                            <b>Incomplete</b> : Application form incomplete.<br />
                                                            <b>Inspection Passed</b> : Application waiting for Monetary Limit to be set.<br />
                                                        </div>
                                                    </div>

                                                </td>

                                            </tr>

                                        </table>

                                    </div>
                                </td>


                            </tr>


                            <tr id="lst_type" runat="server" style="height: 20px;">
                                <td>
                                    <div id="lst_TypeDiv" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td class="leftHeading" valign="top" align="left" style="width: 80px;">Type:</td>
                                                <%--  <td></td>--%>



                                                <td align="left" style="width: 120px;" valign="top">
                                                    <asp:UpdatePanel ID="Update2_" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlType" runat="server" CssClass="clsDDlReport">
                                                                <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                                                <asp:ListItem>Registration</asp:ListItem>
                                                                <asp:ListItem>Renewal</asp:ListItem>
                                                                <asp:ListItem>Amendment</asp:ListItem>
                                                                <asp:ListItem>Inspection</asp:ListItem>

                                                            </asp:DropDownList>


                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>


                                            </tr>

                                        </table>
                                    </div>

                                </td>

                            </tr>
                            <tr id="provisionalReport" runat="server" style="height: 20px;" visible="false">

                                <td>
                                    <div id="div_provisional_Report" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td class="leftHeading" align="left" style="width: 31%">Provisional:</td>

                                                <td align="left" valign="middle">
                                                    <asp:UpdatePanel ID="UpdatePanel_provisional" runat="server">
                                                        <ContentTemplate>


                                                            <asp:DropDownList ID="DdlProvisinal" runat="server" CssClass="clsDDlReport">
                                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                                                <asp:ListItem Value="1">Provisional</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>

                                            </tr>

                                        </table>

                                    </div>
                                </td>

                            </tr>

                            <tr id="DateWise" runat="server" style="height: 20px;">

                                <td>
                                    <div id="DateWise_div" runat="server">

                                        <table cellpadding="0" cellspacing="0" border="0">
                                            <tr>

                                                <td align="right" id="DateWiseText" class="leftHeading">DateWise:</td>
                                                <%--  <td></td>--%>
                                                <td align="left" class="leftHeading">From: 
                                                    <asp:TextBox ID="TextBox1" runat="server"
                                                        ValidationGroup="1" AutoCompleteType="Disabled"></asp:TextBox>
                                                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox1" MaskType="Date"
                                                        Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                                        ClearTextOnInvalid="true">
                                                    </asp:MaskedEditExtender>
                                                    <asp:MaskedEditValidator ID="MaskedEditValidator1" runat="server" ControlToValidate="TextBox1" ControlExtender="MaskedEditExtender1"
                                                        InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                                        TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>

                                                    <%--</td>
               <td class="leftHeading">--%>TO:
                <asp:TextBox ID="txt_21" runat="server"
                    ValidationGroup="1" AutoCompleteType="Disabled"></asp:TextBox>

                                                    <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_21" MaskType="Date"
                                                        Mask="99/99/9999" CultureName="en-US" InputDirection="LeftToRight" MessageValidatorTip="true"
                                                        ClearTextOnInvalid="true">
                                                    </asp:MaskedEditExtender>
                                                    <asp:MaskedEditValidator ID="MaskedEditValidator3" runat="server" ControlToValidate="txt_21" ControlExtender="MaskedEditExtender3"
                                                        InvalidValueMessage="Date format should be MM/DD/YYYY" IsValidEmpty="false" Display="Dynamic"
                                                        TooltipMessage="Input date as MM/DD/YYYY" SetFocusOnError="true" EmptyValueMessage="Date is required"></asp:MaskedEditValidator>



                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" Display="None"
                                                        SetFocusOnError="true" ValidationGroup="1"
                                                        ErrorMessage="Please enter valid date"
                                                        ControlToValidate="txt_21"
                                                        ControlToCompare="TextBox1" Operator="GreaterThan" Type="Date">
                                                    </asp:CompareValidator>


                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </td>


                            </tr>
                            <tr id="Zonewise" runat="server" style="height: 20px;">
                                <td>
                                    <div id="Zonwise_Div" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 38%" class="style1">ZoneWise: </td>
                                                <td style="width: 1px"></td>

                                                <td align="left">&nbsp;
                      
                       
                    
                    <asp:DropDownList ID="ddlZonewise" runat="server"
                        AutoPostBack="true" CssClass="clsDDlReport" Visible="true"
                        OnSelectedIndexChanged="ddlZonewise_SelectedIndexChanged">
                    </asp:DropDownList>

                                                </td>



                                            </tr>
                                        </table>

                                    </div>
                                </td>



                            </tr>

                            <tr id="BranchWise" runat="server" style="height: 20px;">
                                <td>
                                    <div id="Branchwise_div" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>


                                                <td align="left" style="width: 40%; vertical-align: middle;" class="style1">BranchWise:</td>

                                                <td style="width: 1px"></td>

                                                <td align="left" valign="middle">


                                                    <asp:DropDownList ID="ddlBranchWise" runat="server"
                                                        CssClass="clsDDlReport" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchWise_SelectedIndexChanged">
                                                    </asp:DropDownList>


                                                </td>

                                            </tr>


                                        </table>

                                    </div>


                                </td>


                            </tr>

                            <%------------------------------------------State wise ----------------------------------------%>
                            <tr id="State_wise_Report" runat="server">

                                <td>
                                    <div id="Div_statewise" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>


                                                <td align="left" style="width: 40%; vertical-align: middle;" class="style1">StateWise:</td>

                                                <td style="width: 1px"></td>

                                                <td align="left" valign="middle">
                                                    <asp:UpdatePanel ID="Update_Panel_Statewise" runat="server">
                                                        <ContentTemplate>

                                                            <asp:DropDownList ID="ddl_state_wise" runat="server"
                                                                CssClass="clsDDlReport" AutoPostBack="True" OnSelectedIndexChanged="ddl_state_wise_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>

                                            </tr>


                                        </table>

                                    </div>


                                </td>



                            </tr>

                            <tr id="City_wise_report" runat="server">
                                <td>
                                    <div id="Div_city_wise_report" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>


                                                <td align="left" style="width: 40%; vertical-align: middle;" class="style1">CityWise:</td>

                                                <td style="width: 1px"></td>

                                                <td align="left" valign="middle">
                                                    <asp:UpdatePanel ID="Update_City_Wise" runat="server">
                                                        <ContentTemplate>

                                                            <asp:DropDownList ID="Ddl_City_Wise" runat="server"
                                                                CssClass="clsDDlReport" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>

                                            </tr>


                                        </table>

                                    </div>


                                </td>



                            </tr>

                            <tr id="Turn_over_wise_tr" runat="server">
                                <td>
                                    <div id="Div_turn_over_report" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>

                                                <td align="left" style="width: 40%; vertical-align: middle;" class="style1">TurnOverwise:</td>

                                                <td style="width: 1px"></td>

                                                <td align="left" valign="middle">
                                                    <asp:UpdatePanel ID="Update_Turn_over_wise" runat="server">
                                                        <ContentTemplate>
                                                            <table cellpadding="2" cellspacing="2" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top" style="white-space: nowrap;">
                                                                        <b>from:</b></td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txt_turn_over_wise" runat="server"
                                                                            CssClass="clstxtbx" Width="60px" MaxLength="6" onkeydown="javascript:return jsDecimals(event);">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                    <td align="left" valign="top" style="white-space: nowrap;">
                                                                        <b>to</b> </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txt_turnover_wise_to" runat="server" CssClass="clstxtbx" Width="60px" MaxLength="6" onkeydown="javascript:return jsDecimals(event);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>


                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>

                                            </tr>


                                        </table>

                                    </div>


                                </td>

                            </tr>

                            <tr id="tr_monetary_limit" runat="server">
                                <td>
                                    <div id="div_tr_monetary_limit" runat="server">
                                        <table cellpadding="0" cellspacing="0" style="width: 100%">
                                            <tr>

                                                <td align="left" style="width: 40%; vertical-align: middle;" class="style1">MonetaryLimit:</td>

                                                <td style="width: 1px"></td>

                                                <td align="left" valign="middle">
                                                    <asp:UpdatePanel ID="Up_Monetary_limit_wise" runat="server">
                                                        <ContentTemplate>
                                                            <table cellpadding="2" cellspacing="2" border="0">
                                                                <tr>
                                                                    <td align="left" valign="top" style="white-space: nowrap;">
                                                                        <b>from:</b></td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txt_monetary_limit_from" runat="server"
                                                                            CssClass="clstxtbx" Width="60px" MaxLength="6" onkeydown="javascript:return jsDecimals(event);">
                                                                        </asp:TextBox>
                                                                    </td>
                                                                    <td align="left" valign="top" style="white-space: nowrap;">
                                                                        <b>to</b> </td>
                                                                    <td align="left" valign="top">
                                                                        <asp:TextBox ID="txt_monetarylimit_to" runat="server" CssClass="clstxtbx" Width="60px" MaxLength="6" onkeydown="javascript:return jsDecimals(event);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                </td>

                                            </tr>


                                        </table>

                                    </div>


                                </td>

                            </tr>


                            <%-- --------------------------Company Wise Data-----------------------------------------------------%>
                            <tr id="CompanyWise" runat="server" style="height: 20px;">
                                <td>
                                    <div id="Div_companyWise" runat="server">
                                        <table cellpadding="4" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 31%" class="leftHeading">CompanyWise:</td>
                                                <%--  <td></td>--%>
                                                <td align="left" style="margin-left: 10px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                             
                            <asp:DropDownList ID="ddlcomapnywise" runat="server" CssClass="clsDDlReport" Width="250px">
                            </asp:DropDownList></td>



                                            </tr>


                                        </table>

                                    </div>
                                </td>

                            </tr>


                            <%-- --------------------------Categorieswise data--------------------%>

                            <tr id="Cateteogry_wise" runat="server">

                                <td>
                                    <div id="DivCategoryWise" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 38%" class="style1">Category Area:: </td>
                                                <td style="width: 1px"></td>
                                                <td align="left">&nbsp;
                  <asp:UpdatePanel ID="UpdatePanel_category_wise" runat="server">
                      <ContentTemplate>


                          <asp:DropDownList ID="ddl_category_wise_report" runat="server"
                              AutoPostBack="false" CssClass="clsDDlReport">
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>

                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </td>

                            </tr>

                            <tr id="Sub_Category_wise_ddl" runat="server">
                                <td>
                                    <div id="Div_SubCategoryWise" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 38%" class="style1">Category Cast: </td>
                                                <td style="width: 1px"></td>
                                                <td align="left">&nbsp;
                  <asp:UpdatePanel ID="Update_category_cast" runat="server">
                      <ContentTemplate>




                          <asp:DropDownList ID="Ddl_category_cast" runat="server"
                              AutoPostBack="false" CssClass="clsDDlReport">
                          </asp:DropDownList>


                      </ContentTemplate>
                  </asp:UpdatePanel>
                                                </td>

                                            </tr>



                                        </table>

                                    </div>
                                </td>



                            </tr>

                            <tr id="Category_unit_size" runat="server">
                                <td>
                                    <div id="Div_Category_unit_size" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 38%" class="style1">Category UnitSize: </td>
                                                <td style="width: 1px"></td>
                                                <td align="left">&nbsp;
                  <asp:UpdatePanel ID="UpdatePanl_category_unitSize" runat="server">
                      <ContentTemplate>




                          <asp:DropDownList ID="DdlCategoryUnitSize" runat="server"
                              AutoPostBack="false" CssClass="clsDDlReport">
                          </asp:DropDownList>



                      </ContentTemplate>
                  </asp:UpdatePanel>
                                                </td>

                                            </tr>



                                        </table>

                                    </div>
                                </td>


                            </tr>
                            <tr id="tr_order_by" runat="server">
                                <td>
                                    <div id="Div1" runat="server">
                                        <table cellpadding="0" cellspacing="0" border="0" style="width: 100%">
                                            <tr>
                                                <td align="left" style="width: 38%" class="style1">Order By: </td>
                                                <td style="width: 1px"></td>
                                                <td align="left">&nbsp;
                  <asp:UpdatePanel ID="UpdatePanel_orderby" runat="server">
                      <ContentTemplate>




                          <asp:DropDownList ID="DDl_Order_BY" runat="server"
                              AutoPostBack="true" CssClass="clsDDlReport">
                              <asp:ListItem Selected="True" Value="1">Companywise</asp:ListItem>
                              <asp:ListItem Value="2">Registration  Date</asp:ListItem>
                              <asp:ListItem Value="3">ZoneWise</asp:ListItem>
                              <asp:ListItem Value="4">Transfer Cases</asp:ListItem>
                          </asp:DropDownList>
                      </ContentTemplate>
                  </asp:UpdatePanel>
                                                </td>

                                            </tr>




                                        </table>

                                    </div>
                                </td>


                            </tr>

                        </table>


                    </div>
                </td>
            </tr>
            <tr>
                <td>

                    <div id="btn_Report" style="margin-top: 25px; margin-left: 200px;">


                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr>
                                <td align="center">
                                    <asp:UpdateProgress ID="updProgress"
                                        AssociatedUpdatePanelID="Update_bton"
                                        runat="server">
                                        <ProgressTemplate>
                                            <img alt="progress" src="../Images/Admin/loading.gif" />
                                            Processing...            
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:UpdatePanel ID="Update_bton" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_report_ReportPage" runat="server"
                                                Text="Report" ValidationGroup="1"
                                                OnClick="btn_report_ReportPage_Click" />
                                            <br />
                                            <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>

            <tr>
                <td>
                    <div id="Report_Page" style="width: 90%" runat="server">
                        <asp:Label ID="lblMessage" runat="server" Text="" CssClass="Message"></asp:Label>
                        <table cellpadding="0" cellspacing="0" border="1" align="center" width="100%">
                            <tr id="Report_criteria">
                                <td class="style2" align="left">Report: &nbsp;<asp:Label ID="Report_Type" runat="server" Text=""></asp:Label>
                                    <span id="Spn1" class="style4">Total Record: &nbsp;<asp:Label ID="lbl_total_record" runat="server" Text=""></asp:Label>
                                    </span></td>
                            </tr>
                            <tr style="width: 90%">
                                <td align="left">
                                    <table cellpadding="4" cellspacing="4" border="0" style="padding-left: 50px;">
                                        <tr>
                                            <td align="left" valign="top">
                                                <b>
                                                    <asp:Label ID="lbl_search_company_name" runat="server" Text="Search Company Name:"></asp:Label></b>
                                                <%-- <span id="lbl_search_company_name" runat="server"> Search Company Name: </span>--%>
                                                <asp:TextBox ID="txtName" runat="server" Width="100"></asp:TextBox>
                                                <asp:Button ID="cmdSearch" runat="server" Text="Search" OnClick="cmdSearch_Click"
                                                    CausesValidation="False" />
                                            </td>

                                            <td align="left" valign="top">

                                                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePane_all">
                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/Admin/loading.gif" />
                                                        Processing...            
                                                    </ProgressTemplate>


                                                </asp:UpdateProgress>
                                                <asp:UpdatePanel ID="UpdatePane_all" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Button ID="cmdClear" runat="server" Text="All" CausesValidation="False"
                                                            OnClick="cmdClear_Click" />

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </td>


                                            <td align="left" valign="top" style="width: 10%;">&nbsp;</td>

                                            <td align="left" valign="top" id="Detailedviewid" runat="server">

                                                <b><i>Page
        <%=DetailsView1.PageIndex + 1%>
        of
        <%=DetailsView1.PageCount%>
                                                </i></b>



                                            </td>

                                            <td align="left" valign="top" style="width: 13%;">&nbsp;</td>

                                            <td align="left" valign="top">
                                                <asp:UpdateProgress ID="update_show_loading_image" runat="server" AssociatedUpdatePanelID="update_panel_ddl">
                                                    <ProgressTemplate>
                                                        <img alt="progress" src="../Images/Admin/loading.gif" />
                                                        Processing...            
                                                    </ProgressTemplate>


                                                </asp:UpdateProgress>
                                                <asp:UpdatePanel ID="update_panel_ddl" runat="server">
                                                    <ContentTemplate>

                                                        <b>
                                                            <asp:Label ID="lbl_show_text" runat="server" Text="Show"></asp:Label>
                                                        </b>
                                                        <asp:DropDownList ID="ddl_dropdownlist_custompageing" runat="server" AutoPostBack="true"
                                                            CssClass="Alignright"
                                                            Width="50px"
                                                            OnSelectedIndexChanged="ddl_dropdownlist_custompageing_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>




                                        </tr>

                                    </table>






                                    <hr />

                                    <asp:UpdatePanel ID="udtedate_griddview" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <asp:GridView ID="DetailsView1" runat="server" AutoGenerateColumns="false" EnableViewState="true"
                                                AllowPaging="true" Width="777px" AllowSorting="true" PageIndex="0"
                                                BorderWidth="0" CellPadding="10" CellSpacing="1"
                                                OnRowDataBound="Report_onRowdataBound" EnableSortingAndPagingCallbacks="True"
                                                OnPageIndexChanging="DetailsViewPageindexChanging" OnSorting="detailsview_sorting">
                                                <HeaderStyle CssClass="dynRowHeader" />
                                                <RowStyle CssClass="dynRowEven" />
                                                <AlternatingRowStyle CssClass="dynRowOdd" />
                                                <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />

                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ItemStyle-Width="250px" />
                                                    <asp:BoundField DataField="Contact_Details" HeaderText="Contact Details" SortExpression="Contact_Details" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                                    <asp:BoundField DataField="ApplicationDate" HeaderText="Registration No" SortExpression="ApplicationDate" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />
                                                    <asp:BoundField DataField="RegistraionDate" HeaderText="Registration Date" HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false" />
                                                    <asp:BoundField DataField="Zone/Branch" HeaderText="ZONE/Branch" SortExpression="Zone/Branch" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false" ItemStyle-Font-Size="14px" />
                                                    <asp:BoundField DataField="TransferedFrom" HeaderText="Transfer Case" SortExpression="Zone/Branch" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false" ItemStyle-Font-Size="14px" />
                                                    <asp:BoundField DataField="STATUS" HeaderText="Status" ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false" ItemStyle-Font-Size="14px" />

                                                    <asp:TemplateField HeaderText=" ">
                                                        <ItemTemplate>

                                                            <asp:ImageButton ID="imbView" Visible="false" runat="server" ImageUrl="~/Images/Admin/view.png" AlternateText="VIEW"
                                                                CommandName="view" CommandArgument='<%#Eval("fGPtbl_Company_Id") %>' ToolTip="View content" ImageAlign="Middle" />
                                                            <asp:Label ID="lblunitid" runat="server" Visible="false" Text='<%#Eval("fGPtbl_Company_Id") %>'></asp:Label>
                                                            <asp:Label ID="LblComapnY_Name" runat="server" Text='<%#Eval("fGPtbl_Company_Name")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblCompany_Id" runat="server" Text='<%#Eval("fGPtbl_Company_Id") %>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblBranch_Name" runat="server" Text='<%#Eval("fGPtbl_MB_Name") %>' Visible="false"></asp:Label>
                                                            <asp:HyperLink ID="HLName" runat="server" Text='<%#Eval("fGPtbl_Company_Id") %>' Target="_blank"></asp:HyperLink>
                                                            <%--  <asp:Label ID="lblFormtype" runat="server" Text='<%#Eval("fGPtbl_CUnit_FormtypeId") %>' Visible="false"></asp:Label>--%>
                                                            <asp:Label ID="lblBranchId" runat="server" Text='<%#Eval("fgptbl_CUnit_BranchID") %>' Visible="false"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                </Columns>

                                            </asp:GridView>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <%--           
             ------------------------------------Bind Data Zone Wise And Branch Wise for the list odf Payment Report---------------%>


                                    <asp:GridView ID="grd_Branch_Wise" runat="server" AutoGenerateColumns="False"
                                        EnableViewState="False" AllowPaging="true" Width="100%" ShowFooter="true"
                                        BorderWidth="0" CellPadding="10" CellSpacing="1"
                                        OnRowDataBound="grd_GridCompany_onRowdataBound" PageSize="10" Visible="false"
                                        OnRowCommand="onRowGrid_RowCommand"
                                        OnDataBound="grdBranch_wise_dataBound"
                                        OnPageIndexChanging="Branch_wise_Paymeny_report_pageindexchange"
                                        OnSelectedIndexChanging="paymentDetailsZone">

                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zone Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lnkZone_Name" runat="server" Text='<%#Eval("ZoneName") %>' Visible="true"></asp:Label>
                                                    <asp:Label ID="lnkZone_ID" runat="server" Text='<%#Eval("ZoneID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotalzoneName" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>

                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Branch Name">
                                                <ItemTemplate>

                                                    <asp:Label ID="lnk_Branc_Name" runat="server" Text='<%#Eval("BranchName")%>' Visible="true"></asp:Label>
                                                    <asp:Label ID="lbl_Branch_ID" runat="server" Text='<%#Eval("BranchID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblBranchNameTotal" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <%--  <asp:BoundField DataField="RegistrationAmount" HeaderText="Registration Amount" 
                                          ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"  
                                          ItemStyle-Font-Size="14px"  >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>--%>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Registration Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of ServiceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegiatrationAmmount1" runat="server" Text='<%#Eval("RegistrationAmount")%>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblRegistrationTotal1" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <%--
                                    <asp:BoundField DataField="InspectionAmount" HeaderText="Inspection Amount" 
                                          ItemStyle-VerticalAlign="Top"  HeaderStyle-Wrap="false"  
                                          ItemStyle-Font-Size="14px"  >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>  --%>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Insepection Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_unit_ammount1" runat="server" Text='<%#Eval("InspectionAmount")%>'></asp:Label>

                                                </ItemTemplate>



                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal1" Text="" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Renewal Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Renewal" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amendment Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_amedment" runat="server" Text=""></asp:Label>

                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalAmmount" runat="server" Text='<%#Eval("TotalAmmount") %>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalAmmountbranchwise" runat="server"></asp:Label>

                                                </FooterTemplate>

                                            </asp:TemplateField>




                                        </Columns>
                                    </asp:GridView>

                                    <%-- for Design If select Only Branch And Zone ID----%>

                                    <asp:GridView ID="grd_Company_wiseData" runat="server"
                                        AutoGenerateColumns="false" Width="100%"
                                        EnableViewState="False" AllowPrintPaging="true" UseAccessibleHeader="true"
                                        BorderWidth="1" CellPadding="5" CellSpacing="1"
                                        Visible="false" ShowFooter="true" AllowPaging="true" AllowSorting="true"
                                        PageSize="10" OnRowDataBound="grdCompany_Wise_Data"
                                        OnDataBound="grdcompany_branchDatabound"
                                        OnPageIndexChanging="grd_Company_wise_page_Index_changing">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />



                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Comapny Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="LBl_company_Name_Data" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label>
                                                    <asp:Label ID="LBL_Company_Id_Data" runat="server" Text='<%#Eval("fGPtbl_Company_Id")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblComanyNameTotal" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name"  ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />--%>
                                            <%--<asp:BoundField DataField="RegistrationAmmount" HeaderText="Registratio Date" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Registration Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of ServiceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegiatrationAmmount2" runat="server" Text='<%#Eval("RegistrationAmmount")%>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblRegistrationTotal2" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <%-- <asp:BoundField DataField="InspectionAmmount" HeaderText="Insepection Amount" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"/>--%>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Insepection Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_unit_ammount2" runat="server" Text='<%#Eval("InspectionAmmount")%>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal2" Text="" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Amendment Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_amedment" runat="server" Text='<%#Eval("AmedmentAmmount")%>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_amedment_footer" runat="server"></asp:Label>

                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Renewal Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_renewal" runat="server" Text='<%#Eval("RenewalAmount")%>'></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_Renewal_footer" runat="server"></asp:Label>

                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false" />
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:MMM,dd,yyyy }" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" Visible="false" />

                                            <asp:TemplateField HeaderText="Total Ammount">

                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_total_amount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblTotal_companywise" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%--   --------------Bind Comapny To separate Record------------------------%>

                                    <asp:GridView ID="grd_report_paymentDetails" runat="server" AutoGenerateColumns="False"
                                        EnableViewState="False" AllowPaging="True" Width="100%" ShowFooter="true"
                                        BorderWidth="0" CellPadding="10" CellSpacing="1"
                                        OnRowDataBound="grd_Report_onRowdataBound" PageSize="10" Visible="false"
                                        OnRowCommand="grd_report_onRowCommand"
                                        OnDataBound="grd_report_paymentDetails_DataBound"
                                        OnPageIndexChanging="grd_report_payment_Details_Page_Index_changing"
                                        AllowSorting="True">

                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblcomapnyName" runat="server" Text='<%#Eval("fGPtbl_Company_Name") %>'></asp:Label><asp:Label ID="lblCompanyID" runat="server" Text='<%#Eval("fGPtbl_Company_Id") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFooter" runat="server" Text="Total Amount" CssClass="leftHeading"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="fGPtbl_Company_Name" HeaderText="Company Name"  
                                          HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top" 
                                          ItemStyle-Font-Size="14px" >
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>
                                            --%>
                                            <asp:BoundField DataField="PaymentMode" HeaderText="Payment Mode"
                                                HeaderStyle-Wrap="false" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px" Visible="false">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date"
                                                ItemStyle-VerticalAlign="Top" HeaderStyle-Wrap="false"
                                                ItemStyle-Font-Size="14px" Visible="false">
                                                <HeaderStyle Wrap="False" />
                                                <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                            </asp:BoundField>
                                            <%--  <asp:BoundField DataField="RegistrationAmount" 
                                          HeaderText="Registration Amount"   ItemStyle-VerticalAlign="Top"  
                                          HeaderStyle-Wrap="false" ItemStyle-Font-Size="14px" >
                                        
                                            <HeaderStyle Wrap="False" />
                                            <ItemStyle Font-Size="14px" VerticalAlign="Top" />
                                      </asp:BoundField>--%>

                                            <asp:TemplateField>

                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Registration Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of ServiceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegiatrationAmmount" runat="server" Text='<%#Eval("RegistrationAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblRegistrationTotal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Insepection Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_unit_ammount" runat="server" Text='<%#Eval("InspectionAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal" Text="" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Amendment Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Amedment_amount" runat="server" Text='<%#Eval("AmedmentAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_amedment" Text="" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td class="dynRowHeader2">Renewal Amount</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="dynRowHeader3">(Inclusive of serviceTax) </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Renewal_Amount" runat="server" Text='<%#Eval("RenewalAmount")%>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotal_Renewal" Text="" runat="server" />
                                                </FooterTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotalamount" runat="server" Text='<%#Eval("TotalAmount") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotalfooter" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:GridView ID="grd_user_list_report" runat="server"
                                        AutoGenerateColumns="false" Width="100%" AllowPaging="true"
                                        AllowPrintPaging="true" UseAccessibleHeader="true"
                                        BorderWidth="1" CellPadding="5" CellSpacing="1"
                                        Visible="true"
                                        PageSize="10">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Zone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Zone_user" runat="server" Text='<%#Eval("ZoneName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Branch_user" runat="server" Text='<%#Eval("BranchName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_user_name1" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_name" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Phone">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_phone" runat="server" Text='<%#Eval("Phone") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_mobile_user" runat="server" Text='<%#Eval("MObile") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Email_user" runat="server" Text='<%#Eval("Email") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Role">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Role_user" runat="server" Text='<%#Eval("Role") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%-- -------------------------Bind Insepction Agency List-------------------------------------------------%>
                                    <asp:GridView ID="grd_inspectionAgency" runat="server"
                                        AutoGenerateColumns="false" Width="100%"
                                        AllowPrintPaging="true" UseAccessibleHeader="true"
                                        BorderWidth="1" CellPadding="5" CellSpacing="1"
                                        Visible="true"
                                        PageSize="10" OnRowDataBound="grd_inspectionagency_onrowDataBound"
                                        OnPageIndexChanging="grd_inspectionAgency_changing" AllowPaging="True"
                                        AllowSorting="True">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Zone" HeaderText="Zone"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">
                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="BranchName" HeaderText="Branch"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">
                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="UserName" HeaderText="User Name"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">

                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:BoundField DataField="AgencyName" HeaderText="Agency Name"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">
                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CreateDate" HeaderText="Create Date"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">
                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="EmailID" HeaderText="Email"
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px">

                                                <ControlStyle Font-Size="X-Large"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>

                                            <asp:TemplateField HeaderText="Address">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Address" runat="server" Text='<%#Eval("Address")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <%------------------------------------Bind Print Certificate in Gridview------------------%>

                                    <asp:GridView ID="grd_print_certificate" runat="server"
                                        AutoGenerateColumns="false" Width="100%"
                                        AllowPrintPaging="true" UseAccessibleHeader="true"
                                        BorderWidth="1" CellPadding="5" CellSpacing="1"
                                        Visible="true"
                                        PageSize="10" OnRowDataBound="gridprint_certificate_ON_RowDataBound"
                                        OnPageIndexChanging="grd_print_certificate_changing" AllowPaging="True"
                                        AllowSorting="True">
                                        <HeaderStyle HorizontalAlign="Left" CssClass="dynRowHeader" />

                                        <RowStyle CssClass="dynRowEven" />
                                        <AlternatingRowStyle CssClass="dynRowOdd" />
                                        <HeaderStyle CssClass="dynRowHeader" />
                                        <PagerStyle VerticalAlign="Middle" HorizontalAlign="Center" CssClass="Pagging" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <%--  <asp:Label ID="lblSerial" runat="server"></asp:Label><br />--%>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Company Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_comapny_print_name" runat="server" Text='<%#Eval("ComapnyName") %>'></asp:Label><asp:Label ID="lbl_companyId_print" runat="server" Text='<%#Eval("ComapnyID") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Zone/Branch" HeaderText=" Zone / Branch  " ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px"  ControlStyle-Width="100px"/>--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">Zone/</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">Branch</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_Zone_Branch" runat="server" Text='<%#Eval("Zone/Branch") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="UserName/RequestedDate" HeaderText="RequestedBy/RequestDate" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" ControlStyle-Width="20px" />--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">RequestedBy/</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">RequestDate</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_request_date" runat="server" Text='<%#Eval("UserName/RequestedDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Rquest" HeaderText="Reason "
                                                ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top"
                                                ItemStyle-Font-Size="14px" ControlStyle-Width="250px">
                                                <ControlStyle Font-Size="X-Large" Width="250px"></ControlStyle>

                                                <ItemStyle VerticalAlign="Top" Font-Size="14px"></ItemStyle>
                                            </asp:BoundField>
                                            <%-- <asp:BoundField DataField="ApprovedBy/AcceptedDate" HeaderText="Approved By/ Accepted Date" ControlStyle-Font-Size="X-Large" ItemStyle-VerticalAlign="Top" ItemStyle-Font-Size="14px" />--%>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table cellpadding="0" cellspacing="0" border="0">
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">Approved By/</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left" valign="top" class="dynRowHeader1">Accepted Date</td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_RequsetDate_userName" runat="server" Text='<%#Eval("ApprovedBy/AcceptedDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>


            <tr id="tr_page" runat="server">
                <td align="right" style="padding-right: 50px;">
                    <div id="pageindexchange">
                        <table cellpadding="2" cellspacing="2" border="0">
                            <tr>
                                <td align="left" style="padding-right: 15px; width: 100%;">
                                    <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                        <tr>
                                            <td align="center" id="detailview1_td" runat="server">
                                                <table cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td align="right" id="detailviewpage_run" runat="server" visible="false">
                                                            <i>&nbsp;<asp:Label ID="lbl_pageindex" runat="server" Text="" Visible="false"></asp:Label>
                                                                &nbsp;  &nbsp;
                                                                <asp:Label ID="lbl_page_count_deatailsview" runat="server" Text="" Visible="false"></asp:Label>
                                                            </i>
                                                        </td>
                                                        <td align="right" id="dtailview1_static" runat="server">
                                                            <asp:UpdateProgress ID="Updateprogress_detailedview" runat="server" AssociatedUpdatePanelID="udtedate_griddview">
                                                                <ProgressTemplate>
                                                                    <img alt="progress" src="../Images/Admin/loading.gif" />
                                                                    Processing...            
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td align="right" style="white-space: nowrap;">
                                                <table cellpadding="2" cellspacing="2" border="0">
                                                    <tr>
                                                        <td align="right" style="width: 80%;">
                                                            <b><i>Page
       <%=DetailsView1.PageIndex + 1%>
        of
        <%=DetailsView1.PageCount%>
                                                            </i></b>
                                                        </td>


                                                        <td align="left">
                                                            <asp:UpdateProgress ID="UpdateProgress1"
                                                                AssociatedUpdatePanelID="updaytedropdown_pages"
                                                                runat="server">
                                                                <ProgressTemplate>
                                                                    <img alt="progress" src="../Images/Admin/loading.gif" />
                                                                    Processing...            
                                                                </ProgressTemplate>
                                                            </asp:UpdateProgress>
                                                            <asp:UpdatePanel ID="updaytedropdown_pages" runat="server">
                                                                <ContentTemplate>
                                                                    <b>
                                                                        <asp:Label ID="lbl_textshow" runat="server" Text="Show"></asp:Label>
                                                                    </b>
                                                                    <asp:DropDownList ID="ddl_custom_paging" runat="server" AutoPostBack="true"
                                                                        CssClass="Alignright"
                                                                        OnSelectedIndexChanged="ddl_custom_paging_SelectedIndexChanged" Width="50px">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>

                                                </table>



                                            </td>

                                        </tr>
                                    </table>




                                </td>

                            </tr>
                        </table>

                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="print_button" runat="server" align="center">
                        <table cellpadding="0" cellspacing="0" border="0">
                            <tr id="tr_print_button" runat="server">
                                <td align="center"><%--onclick="btn_print_Click"--%>



                                    <input type="button" id="btn_print" onclick="javascript:OpenPrintPage1();" value="Print Preview" />

                                    <asp:Button ID="btn_grid_print" runat="server" Text="Print All Pages"
                                        OnClick="btn_grid_print_Click" Visible="False" />

                                </td>

                            </tr>

                        </table>

                    </div>
                </td>
            </tr>

        </table>
    </div>

    <div align="center">
        <asp:Label ID="lbl_show_message" runat="server" Text=""></asp:Label>

    </div>

</asp:Content>

